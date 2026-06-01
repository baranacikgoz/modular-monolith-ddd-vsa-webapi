using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Host.Tests;

// Guards the riskiest behavior of the body-logging refactor: RequestResponseBodyLoggingMiddleware
// swaps Response.Body for a BoundedCaptureStream tee. The tee must pass every byte through to
// the real response while copying only the first N into a bounded log buffer. If the tee ever
// truncated the live stream, clients would receive corrupted/short responses.
//
// Own factory + "Host" collection (not ICollectionFixture) — same pattern as DynamicModuleTests:
// each test boots an isolated host with body logging forced on, and the collection serializes
// boots so parallel factory startup can't corrupt global Serilog/OTel state.
[Collection("Host")]
public class RequestBodyLoggingTests
{
    private const int CaptureLimitBytes = 8;
    private const string CaptureLimitBytesText = "8";

    private sealed class BodyLoggingHostFactory : HostTestFactory
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureAppConfiguration((_, config) =>
                config.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["RequestLoggingOptions:LogRequestBody"] = "true",
                    ["RequestLoggingOptions:LogResponseBody"] = "true",
                    ["RequestLoggingOptions:RequestBodyLogLimitBytes"] = CaptureLimitBytesText,
                    ["RequestLoggingOptions:ResponseBodyLogLimitBytes"] = CaptureLimitBytesText,
                }));
        }
    }

    [Fact]
    public async Task ResponseCapture_WithBodyLargerThanLogLimit_StreamsFullResponseToClient()
    {
        await using var factory = new BodyLoggingHostFactory();
        await factory.InitializeAsync();
        var client = factory.CreateClient();

        var response = await client.GetAsync(new Uri("/health/ready", UriKind.Relative));

        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();

        // The health JSON is far larger than the 8-byte capture limit. If the tee truncated the
        // live stream we'd get a short, unparseable body. Full valid JSON proves pass-through.
        Assert.True(content.Length > CaptureLimitBytes);
        using var doc = JsonDocument.Parse(content);
        Assert.Equal("Healthy", doc.RootElement.GetProperty("status").GetString());
    }

    [Fact]
    public async Task RequestCapture_WithSensitivePath_DoesNotBreakRoundTrip()
    {
        await using var factory = new BodyLoggingHostFactory();
        await factory.InitializeAsync();
        var client = factory.CreateClient();

        // /tokens POST is a SensitiveRequestBodyPath: capture is skipped but EnableBuffering and the
        // redaction branch still run. The request must complete normally (validation 400, not 500).
        var response = await client.PostAsJsonAsync(
            new Uri("/tokens", UriKind.Relative),
            new { phoneNumber = "", password = "" });

        Assert.True(
            (int)response.StatusCode < 500,
            $"Sensitive-path request body handling crashed the pipeline: {(int)response.StatusCode}");
    }
}
