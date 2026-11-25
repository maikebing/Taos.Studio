using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Taos.Studio.Data;
using Xunit;

namespace Taos.Studio.IoTdb.Tests
{
    public sealed class IoTdbRestClientTests
    {
        [Fact]
        public void QueryUsesBasicAuthAndMapsNestedResults()
        {
            var capture = new RequestCapture();
            using var client = CreateClient(capture, HttpStatusCode.OK,
                "{\"code\":200,\"message\":\"SUCCESS\",\"data\":{\"columnNames\":[\"Time\",\"root.sg.d1.temperature\",\"note\"],\"columnTypes\":[\"TIMESTAMP\",\"FLOAT\",\"TEXT\"],\"values\":[[1700000000000,21.5,\"warm\"],[1700000001000,22.25,\"steady\"]]}}");

            var result = client.Execute("SELECT * FROM root.sg.d1 LIMIT 2");

            Assert.Equal("/proxy/rest/v1/query", capture.Path);
            Assert.Equal("Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes("tester:test-only-password")), capture.Authorization);
            Assert.Contains("SELECT * FROM root.sg.d1 LIMIT 2", capture.Body);
            Assert.Equal(2, result.Rows.Count);
            Assert.Equal(typeof(long), result.Columns[0].DataType);
            Assert.Equal(1700000000000L, result.Rows[0]["Time"]);
            Assert.Equal(21.5f, result.Rows[0]["root.sg.d1.temperature"]);
            Assert.Equal("steady", result.Rows[1]["note"]);
        }

        [Fact]
        public void NonQueryUsesNonQueryEndpoint()
        {
            var capture = new RequestCapture();
            using var client = CreateClient(capture, HttpStatusCode.OK, "{\"code\":200,\"message\":\"SUCCESS\"}");

            var result = client.Execute("CREATE DATABASE root.integration_test");

            Assert.Equal("/proxy/rest/v1/nonQuery", capture.Path);
            Assert.Empty(result.Columns);
            Assert.Empty(result.Rows);
        }

        [Theory]
        [InlineData("https://iotdb.example/proxy/rest/v1")]
        [InlineData("https://iotdb.example/proxy/rest/v1/query")]
        public void ConfiguredApiPathIsNotDuplicated(string configuredUrl)
        {
            var capture = new RequestCapture();
            using var client = new IoTdbRestClient(new Uri(configuredUrl), "tester", "test-only-password", TimeSpan.FromSeconds(2),
                new CaptureHandler(capture, HttpStatusCode.OK, "{\"code\":200,\"message\":\"SUCCESS\"}"));

            client.Execute("SHOW DATABASES");

            Assert.Equal("/proxy/rest/v1/query", capture.Path);
        }

        [Fact]
        public void TableQueryUsesTableEndpointAndDatabaseContext()
        {
            var capture = new RequestCapture();
            using var client = new IoTdbRestClient(new Uri("https://iotdb.example/proxy/rest/table/v1"),
                "tester", "test-only-password", TimeSpan.FromSeconds(2),
                new CaptureHandler(capture, HttpStatusCode.OK,
                    "{\"code\":200,\"column_names\":[\"time\",\"temperature\"],\"data_types\":[\"TIMESTAMP\",\"FLOAT\"],\"values\":[[1700000000000,21.5]]}"),
                IoTdbModel.Table);

            var result = client.Execute("SELECT time, temperature FROM sensor_data LIMIT 10", "telemetry");

            Assert.Equal("/proxy/rest/table/v1/query", capture.Path);
            using var request = JsonDocument.Parse(capture.Body);
            Assert.Equal("telemetry", request.RootElement.GetProperty("database").GetString());
            Assert.Equal("SELECT time, temperature FROM sensor_data LIMIT 10", request.RootElement.GetProperty("sql").GetString());
            Assert.Equal(1, result.Rows.Count);
            Assert.Equal(21.5f, result.Rows[0]["temperature"]);
        }

        [Fact]
        public void TableNonQueryUsesTableEndpoint()
        {
            var capture = new RequestCapture();
            using var client = new IoTdbRestClient(new Uri("https://iotdb.example/proxy/"),
                "tester", "test-only-password", TimeSpan.FromSeconds(2),
                new CaptureHandler(capture, HttpStatusCode.OK, "{\"code\":200,\"message\":\"SUCCESS\"}"),
                IoTdbModel.Table);

            client.Execute("CREATE DATABASE telemetry");

            Assert.Equal("/proxy/rest/table/v1/nonQuery", capture.Path);
            using var request = JsonDocument.Parse(capture.Body);
            Assert.Equal(string.Empty, request.RootElement.GetProperty("database").GetString());
        }

        [Fact]
        public void ConfiguredTableEndpointIsNotDuplicated()
        {
            var capture = new RequestCapture();
            using var client = new IoTdbRestClient(new Uri("https://iotdb.example/proxy/rest/table/v1/query"),
                "tester", "test-only-password", TimeSpan.FromSeconds(2),
                new CaptureHandler(capture, HttpStatusCode.OK, "{\"code\":200,\"column_names\":[],\"values\":[]}"),
                IoTdbModel.Table);

            client.Execute("SHOW TABLES", "telemetry");

            Assert.Equal("/proxy/rest/table/v1/query", capture.Path);
        }

        [Fact]
        public void QueryIncludesSeparateTimestampsWhenRowsOmitTimeColumn()
        {
            var capture = new RequestCapture();
            using var client = CreateClient(capture, HttpStatusCode.OK,
                "{\"code\":200,\"data\":{\"columns\":[\"temperature\"],\"dataTypeList\":[\"DOUBLE\"],\"timestamps\":[100,200],\"values\":[[20.5],[21.5]]}}");

            var result = client.Execute("SELECT temperature FROM root.sg.d1");

            Assert.Equal(new[] { "Time", "temperature" }, new[] { result.Columns[0].ColumnName, result.Columns[1].ColumnName });
            Assert.Equal(100L, result.Rows[0]["Time"]);
            Assert.Equal(21.5d, result.Rows[1]["temperature"]);
        }

        [Fact]
        public void ServerErrorCodeIsReported()
        {
            var capture = new RequestCapture();
            using var client = CreateClient(capture, HttpStatusCode.OK, "{\"code\":600,\"message\":\"SQL parse error\"}");

            var error = Assert.Throws<InvalidOperationException>(() => client.Execute("SELECT broken"));

            Assert.Contains("600", error.Message);
            Assert.Contains("SQL parse error", error.Message);
        }

        [Fact]
        public void HttpAuthenticationFailureReportsStatusWithoutCredentials()
        {
            var capture = new RequestCapture();
            using var client = CreateClient(capture, HttpStatusCode.Unauthorized, "{\"message\":\"Authentication failed\"}");

            var error = Assert.Throws<HttpRequestException>(() => client.Execute("SHOW DATABASES"));

            Assert.Contains("HTTP 401", error.Message);
            Assert.Contains("Authentication failed", error.Message);
            Assert.DoesNotContain("test-only-password", error.Message);
        }

        [Fact]
        public void RequestUsesConfiguredTimeout()
        {
            using var client = new IoTdbRestClient(new Uri("http://iotdb.example/"), "tester", "test-only-password",
                TimeSpan.FromMilliseconds(25), new BlockingHandler());

            Assert.ThrowsAny<OperationCanceledException>(() => client.Execute("SHOW DATABASES"));
        }

        [Fact]
        public void LeadingSqlCommentsDoNotChangeEndpointSelection()
        {
            Assert.True(IoTdbRestClient.IsQuery("-- list databases\n SHOW DATABASES"));
            Assert.True(IoTdbRestClient.IsQuery("/* metadata */ DESCRIBE root.sg.d1.s1"));
            Assert.False(IoTdbRestClient.IsQuery("/* insert */ INSERT INTO root.sg.d1(timestamp, s1) VALUES(1, 2)"));
        }

        private static IoTdbRestClient CreateClient(RequestCapture capture, HttpStatusCode statusCode, string response)
        {
            var handler = new CaptureHandler(capture, statusCode, response);
            return new IoTdbRestClient(new Uri("https://iotdb.example/proxy/"), "tester", "test-only-password", TimeSpan.FromSeconds(2), handler);
        }

        private sealed class RequestCapture
        {
            public string Path { get; set; }
            public string Authorization { get; set; }
            public string Body { get; set; }
        }

        private sealed class CaptureHandler : HttpMessageHandler
        {
            private readonly RequestCapture _capture;
            private readonly HttpStatusCode _statusCode;
            private readonly string _response;

            public CaptureHandler(RequestCapture capture, HttpStatusCode statusCode, string response)
            {
                _capture = capture;
                _statusCode = statusCode;
                _response = response;
            }

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                _capture.Path = request.RequestUri.AbsolutePath;
                _capture.Authorization = request.Headers.Authorization?.ToString();
                _capture.Body = await request.Content.ReadAsStringAsync();
                return new HttpResponseMessage(_statusCode)
                {
                    Content = new StringContent(_response, Encoding.UTF8, "application/json")
                };
            }
        }

        private sealed class BlockingHandler : HttpMessageHandler
        {
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                await Task.Delay(Timeout.Infinite, cancellationToken);
                throw new InvalidOperationException("The request should have been cancelled by its timeout.");
            }
        }
    }
}
