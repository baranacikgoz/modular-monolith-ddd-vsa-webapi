using System.Net;
using System.Text.Json;

namespace Host.Tests;

[Collection("Host")]
public class HealthCheckTests(HostTestFactory factory)
{
    private readonly HttpClient _client = factory.CreateClient();

    [Theory]
    [InlineData("/health/live")]
    [InlineData("/health/ready")]
    [InlineData("/health/startup")]
    public async Task HealthCheck_Endpoint_ReturnsHealthy(string endpoint)
    {
        var response = await _client.GetAsync(new Uri(endpoint, UriKind.Relative));

        var content = await response.Content.ReadAsStringAsync();
        Assert.True(response.StatusCode == HttpStatusCode.OK, $"{endpoint} returned {response.StatusCode}: {content}");

        using var doc = JsonDocument.Parse(content);
        var status = doc.RootElement.GetProperty("status").GetString();
        Assert.Equal("Healthy", status);
    }

    [Fact]
    public async Task HealthCheck_ReadyEndpoint_ReturnsPostgresqlEntry()
    {
        var response = await _client.GetAsync(new Uri("/health/ready", UriKind.Relative));

        var content = await response.Content.ReadAsStringAsync();
        Assert.True(response.StatusCode == HttpStatusCode.OK, $"/health/ready returned {response.StatusCode}: {content}");

        using var doc = JsonDocument.Parse(content);

        var entries = doc.RootElement.GetProperty("entries");
        Assert.True(entries.TryGetProperty("postgresql", out var pgEntry));
        Assert.Equal("Healthy", pgEntry.GetProperty("status").GetString());
    }
}
