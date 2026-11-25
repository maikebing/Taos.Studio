using IoTSharp.Data.Taos;
using System;
using System.Text.Json;

namespace Taos.Studio.Data
{
    public enum DatabaseProvider
    {
        Native,
        IoTDB
    }

    public sealed class DatabaseConnectionOptions
    {
        private const string IoTdbStoragePrefix = "iotdb-rest:";
        private TaosConnectionStringBuilder _taosConnectionString;

        public DatabaseProvider Provider { get; set; } = DatabaseProvider.Native;
        public string Host { get; set; } = "";
        public int Port { get; set; } = 6030;
        public string Username { get; set; } = new TaosConnectionStringBuilder().Username;
        public string Password { get; set; } = new TaosConnectionStringBuilder().Password;
        public string Charset { get; set; } = System.Text.Encoding.Default.EncodingName;
        public int TimeoutSeconds { get; set; } = 30;
        public IoTdbModel Model { get; set; } = IoTdbModel.Tree;

        public static DatabaseConnectionOptions FromPersistedString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            if (value.StartsWith(IoTdbStoragePrefix, StringComparison.OrdinalIgnoreCase))
            {
                var options = JsonSerializer.Deserialize<DatabaseConnectionOptions>(value.Substring(IoTdbStoragePrefix.Length));
                if (options == null || options.Provider != DatabaseProvider.IoTDB)
                {
                    throw new FormatException("The saved IoTDB connection settings are invalid.");
                }

                return options;
            }

            var taos = new TaosConnectionStringBuilder(value);
            return new DatabaseConnectionOptions
            {
                Provider = DatabaseProvider.Native,
                Host = taos.DataSource,
                Port = taos.Port,
                Username = taos.Username,
                Password = taos.Password,
                Charset = taos.Charset,
                _taosConnectionString = taos
            };
        }

        public TaosConnectionStringBuilder CreateTaosConnectionString()
        {
            var builder = _taosConnectionString ?? new TaosConnectionStringBuilder();
            builder.DataSource = Host;
            builder.Port = Port;
            builder.Username = Username;
            builder.Password = Password;
            builder.Charset = Charset;
            return builder.UseWebSocket();
        }

        public Uri CreateIoTdbBaseUri()
        {
            var host = (Host ?? "").Trim();
            if (host.Length == 0)
            {
                throw new FormatException("Enter an IoTDB REST host or URL.");
            }

            if (Uri.TryCreate(host, UriKind.Absolute, out var absoluteUri))
            {
                if (absoluteUri.Scheme != Uri.UriSchemeHttp && absoluteUri.Scheme != Uri.UriSchemeHttps)
                {
                    throw new FormatException("The IoTDB REST URL must use HTTP or HTTPS.");
                }

                if (!string.IsNullOrEmpty(absoluteUri.UserInfo) || !string.IsNullOrEmpty(absoluteUri.Query) || !string.IsNullOrEmpty(absoluteUri.Fragment))
                {
                    throw new FormatException("Put credentials in the user name and password fields, not in the REST URL.");
                }

                return IoTdbRestClient.NormalizeBaseUri(absoluteUri);
            }

            if (host.Contains("://", StringComparison.Ordinal))
            {
                throw new FormatException("The IoTDB REST URL must use HTTP or HTTPS.");
            }

            if (Port < 1 || Port > 65535)
            {
                throw new FormatException("The server port must be between 1 and 65535.");
            }

            return new UriBuilder(Uri.UriSchemeHttp, host, Port).Uri;
        }

        public string ToPersistedString()
        {
            if (Provider == DatabaseProvider.IoTDB)
            {
                return IoTdbStoragePrefix + JsonSerializer.Serialize(this);
            }

            return CreateTaosConnectionString().ConnectionString;
        }
    }
}
