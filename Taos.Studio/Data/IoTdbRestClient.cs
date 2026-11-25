using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Taos.Studio.Data
{
    public enum IoTdbModel
    {
        Tree,
        Table
    }

    internal sealed class IoTdbRestClient : IDisposable
    {
        private const int MaximumResponseBytes = 64 * 1024 * 1024;
        private readonly HttpClient _httpClient;
        private readonly Uri _baseUri;
        private readonly IoTdbModel _model;

        public IoTdbRestClient(Uri baseUri, string username, string password, TimeSpan timeout,
            HttpMessageHandler handler = null, IoTdbModel model = IoTdbModel.Tree)
        {
            if (baseUri == null || !baseUri.IsAbsoluteUri || (baseUri.Scheme != Uri.UriSchemeHttp && baseUri.Scheme != Uri.UriSchemeHttps))
            {
                throw new ArgumentException("An absolute HTTP or HTTPS IoTDB REST URL is required.", nameof(baseUri));
            }
            if (!string.IsNullOrEmpty(baseUri.UserInfo) || !string.IsNullOrEmpty(baseUri.Query) || !string.IsNullOrEmpty(baseUri.Fragment))
            {
                throw new ArgumentException("Put credentials in the user name and password fields, not in the REST URL.", nameof(baseUri));
            }

            if (timeout <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(timeout), "The REST timeout must be positive.");
            }

            _baseUri = NormalizeBaseUri(baseUri);
            _model = model;
            _httpClient = handler == null ? new HttpClient() : new HttpClient(handler, true);
            _httpClient.Timeout = timeout;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(Encoding.UTF8.GetBytes((username ?? "") + ":" + (password ?? ""))));
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public DataTable Execute(string sql, string database = null)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentException("SQL cannot be empty.", nameof(sql));
            }

            var query = IsQuery(sql);
            var endpoint = _model == IoTdbModel.Table
                ? (query ? "rest/table/v1/query" : "rest/table/v1/nonQuery")
                : (query ? "rest/v1/query" : "rest/v1/nonQuery");
            using var request = new HttpRequestMessage(HttpMethod.Post, new Uri(_baseUri, endpoint));
            var requestBody = _model == IoTdbModel.Table
                ? JsonSerializer.Serialize(new { sql, database = database ?? string.Empty })
                : JsonSerializer.Serialize(new { sql });
            request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            using var response = _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).GetAwaiter().GetResult();
            var body = ReadBoundedBody(response.Content);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"IoTDB REST request failed with HTTP {(int)response.StatusCode}: {GetErrorMessage(body) ?? response.ReasonPhrase}");
            }

            if (string.IsNullOrWhiteSpace(body))
            {
                return new DataTable();
            }

            using var document = JsonDocument.Parse(body);
            var root = document.RootElement;
            ThrowIfServerError(root);
            var data = TryGetProperty(root, "data", out var nestedData) && nestedData.ValueKind == JsonValueKind.Object
                ? nestedData
                : root;
            return ToDataTable(data);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        internal static bool IsQuery(string sql)
        {
            var keyword = ReadFirstKeyword(sql);
            return keyword == "SELECT" || keyword == "SHOW" || keyword == "LIST" || keyword == "DESCRIBE" ||
                keyword == "DESC" || keyword == "EXPLAIN";
        }

        internal static Uri NormalizeBaseUri(Uri uri)
        {
            var path = uri.AbsolutePath.TrimEnd('/');
            var apiSuffixes = new[]
            {
                "/rest/table/v1/nonQuery",
                "/rest/table/v1/insertTablet",
                "/rest/table/v1/query",
                "/rest/table/v1",
                "/rest/v1/insertTablet",
                "/rest/v1/nonQuery",
                "/rest/v1/query",
                "/rest/v1"
            };

            foreach (var suffix in apiSuffixes)
            {
                if (path.EndsWith(suffix, StringComparison.OrdinalIgnoreCase))
                {
                    path = path.Substring(0, path.Length - suffix.Length);
                    break;
                }
            }

            var builder = new UriBuilder(uri)
            {
                Path = string.IsNullOrEmpty(path) ? "/" : path + "/"
            };
            return builder.Uri;
        }

        private static string ReadFirstKeyword(string sql)
        {
            var position = 0;
            while (position < sql.Length)
            {
                while (position < sql.Length && char.IsWhiteSpace(sql[position])) position++;
                if (position + 1 < sql.Length && sql[position] == '-' && sql[position + 1] == '-')
                {
                    position = sql.IndexOf('\n', position);
                    if (position < 0) return "";
                    position++;
                    continue;
                }
                if (position + 1 < sql.Length && sql[position] == '/' && sql[position + 1] == '*')
                {
                    var end = sql.IndexOf("*/", position + 2, StringComparison.Ordinal);
                    if (end < 0) return "";
                    position = end + 2;
                    continue;
                }
                break;
            }

            var start = position;
            while (position < sql.Length && char.IsLetter(sql[position])) position++;
            return sql.Substring(start, position - start).ToUpperInvariant();
        }

        private static string ReadBoundedBody(HttpContent content)
        {
            using var stream = content.ReadAsStreamAsync().GetAwaiter().GetResult();
            using var buffer = new MemoryStream();
            var chunk = new byte[81920];
            int read;
            while ((read = stream.Read(chunk, 0, chunk.Length)) > 0)
            {
                if (buffer.Length + read > MaximumResponseBytes)
                {
                    throw new InvalidDataException($"IoTDB REST response exceeds the {MaximumResponseBytes / (1024 * 1024)} MiB client limit.");
                }

                buffer.Write(chunk, 0, read);
            }

            return Encoding.UTF8.GetString(buffer.ToArray());
        }

        private static void ThrowIfServerError(JsonElement root)
        {
            if (!TryGetProperty(root, "code", out var code)) return;

            if (code.ValueKind == JsonValueKind.Number && code.TryGetInt32(out var numericCode) && numericCode != 200)
            {
                throw new InvalidOperationException($"IoTDB returned error {numericCode}: {GetErrorMessage(root.GetRawText()) ?? "Unknown server error."}");
            }

            if (code.ValueKind == JsonValueKind.String && int.TryParse(code.GetString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out numericCode) && numericCode != 200)
            {
                throw new InvalidOperationException($"IoTDB returned error {numericCode}: {GetErrorMessage(root.GetRawText()) ?? "Unknown server error."}");
            }
        }

        private static string GetErrorMessage(string body)
        {
            if (string.IsNullOrWhiteSpace(body)) return null;

            try
            {
                using var document = JsonDocument.Parse(body);
                if (TryGetProperty(document.RootElement, "message", out var message) && message.ValueKind == JsonValueKind.String)
                {
                    return message.GetString();
                }
            }
            catch (JsonException)
            {
                return null;
            }

            return null;
        }

        private static DataTable ToDataTable(JsonElement payload)
        {
            var table = new DataTable("IoTDB");
            var columns = ReadStringArray(payload, "columnNames", "column_names", "columns");
            var types = ReadStringArray(payload, "columnTypes", "column_types", "dataTypes", "data_types", "dataTypeList", "data_type_list");
            var rows = TryGetProperty(payload, "values", out var values) ? values :
                TryGetProperty(payload, "rows", out var rowValues) ? rowValues : default;
            var timestamps = TryGetProperty(payload, "timestamps", out var timestampValues) && timestampValues.ValueKind == JsonValueKind.Array
                ? timestampValues.EnumerateArray().ToList()
                : new List<JsonElement>();

            var rowElements = rows.ValueKind == JsonValueKind.Array ? rows.EnumerateArray().ToList() : new List<JsonElement>();
            if (columns.Count == 0 && rowElements.Count > 0 && rowElements[0].ValueKind == JsonValueKind.Object)
            {
                columns.AddRange(rowElements[0].EnumerateObject().Select(property => property.Name));
            }

            var valueColumnCount = rowElements.Count > 0 && rowElements[0].ValueKind == JsonValueKind.Array
                ? rowElements[0].GetArrayLength()
                : columns.Count;
            var hasTimeColumn = columns.Any(column => column.Equals("Time", StringComparison.OrdinalIgnoreCase));
            var prependTime = timestamps.Count > 0 && rowElements.Count > 0 && rowElements[0].ValueKind == JsonValueKind.Array &&
                (columns.Count == 0 || !hasTimeColumn || columns.Count == valueColumnCount + 1);
            if (columns.Count == 0 && valueColumnCount > 0)
            {
                if (prependTime) columns.Add("Time");
                for (var index = 0; index < valueColumnCount; index++) columns.Add("Column" + (index + 1));
            }
            else if (prependTime && !hasTimeColumn)
            {
                columns.Insert(0, "Time");
            }
            if (prependTime && types.Count == columns.Count - 1) types.Insert(0, "TIMESTAMP");

            for (var index = 0; index < columns.Count; index++)
            {
                var name = MakeUniqueColumnName(columns[index], table);
                var typeName = index < types.Count ? types[index] : "";
                table.Columns.Add(name, GetClrType(typeName));
            }

            for (var rowIndex = 0; rowIndex < rowElements.Count; rowIndex++)
            {
                var row = rowElements[rowIndex];
                var dataRow = table.NewRow();
                if (row.ValueKind == JsonValueKind.Array)
                {
                    var cells = row.EnumerateArray().ToList();
                    var offset = 0;
                    if (prependTime && rowIndex < timestamps.Count)
                    {
                        dataRow[0] = ConvertJsonValue(timestamps[rowIndex], table.Columns[0].DataType);
                        offset = 1;
                    }
                    for (var cellIndex = 0; cellIndex < cells.Count && cellIndex + offset < table.Columns.Count; cellIndex++)
                    {
                        dataRow[cellIndex + offset] = ConvertJsonValue(cells[cellIndex], table.Columns[cellIndex + offset].DataType);
                    }
                }
                else if (row.ValueKind == JsonValueKind.Object)
                {
                    for (var cellIndex = 0; cellIndex < table.Columns.Count; cellIndex++)
                    {
                        if (TryGetProperty(row, table.Columns[cellIndex].ColumnName, out var cell))
                        {
                            dataRow[cellIndex] = ConvertJsonValue(cell, table.Columns[cellIndex].DataType);
                        }
                    }
                }
                table.Rows.Add(dataRow);
            }

            return table;
        }

        private static List<string> ReadStringArray(JsonElement payload, params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                if (!TryGetProperty(payload, propertyName, out var values) || values.ValueKind != JsonValueKind.Array) continue;
                return values.EnumerateArray()
                    .Select(value => value.ValueKind == JsonValueKind.String ? value.GetString() : value.ToString())
                    .ToList();
            }

            return new List<string>();
        }

        private static Type GetClrType(string typeName)
        {
            switch ((typeName ?? "").ToUpperInvariant())
            {
                case "BOOLEAN": return typeof(bool);
                case "INT32":
                case "INTEGER": return typeof(int);
                case "INT64":
                case "TIMESTAMP":
                case "BIGINT": return typeof(long);
                case "FLOAT": return typeof(float);
                case "DOUBLE": return typeof(double);
                case "DATE":
                case "DATETIME": return typeof(DateTime);
                default: return typeof(string);
            }
        }

        private static object ConvertJsonValue(JsonElement value, Type type)
        {
            if (value.ValueKind == JsonValueKind.Null || value.ValueKind == JsonValueKind.Undefined)
            {
                return DBNull.Value;
            }

            if (type == typeof(string))
            {
                return value.ValueKind == JsonValueKind.String ? value.GetString() : value.ToString();
            }

            if (type == typeof(bool))
            {
                return value.ValueKind == JsonValueKind.True || (value.ValueKind == JsonValueKind.String && bool.Parse(value.GetString()));
            }

            var text = value.ValueKind == JsonValueKind.String ? value.GetString() : value.ToString();
            if (type == typeof(int)) return int.Parse(text, CultureInfo.InvariantCulture);
            if (type == typeof(long)) return long.Parse(text, CultureInfo.InvariantCulture);
            if (type == typeof(float)) return float.Parse(text, CultureInfo.InvariantCulture);
            if (type == typeof(double)) return double.Parse(text, CultureInfo.InvariantCulture);
            if (type == typeof(DateTime)) return DateTime.Parse(text, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            return text;
        }

        private static string MakeUniqueColumnName(string name, DataTable table)
        {
            var candidate = string.IsNullOrWhiteSpace(name) ? "Column" + (table.Columns.Count + 1) : name;
            if (!table.Columns.Contains(candidate)) return candidate;

            var suffix = 2;
            while (table.Columns.Contains(candidate + "_" + suffix)) suffix++;
            return candidate + "_" + suffix;
        }

        private static bool TryGetProperty(JsonElement element, string name, out JsonElement value)
        {
            if (element.ValueKind == JsonValueKind.Object)
            {
                foreach (var property in element.EnumerateObject())
                {
                    if (property.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    {
                        value = property.Value;
                        return true;
                    }
                }
            }

            value = default;
            return false;
        }
    }
}
