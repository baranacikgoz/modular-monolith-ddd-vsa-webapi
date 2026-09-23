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
    [InlineData("/health/dependencies")]
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

    // Keycloak is a dependency, not a readiness gate: a blip must not pull every instance out of rotation.
    [Fact]
    public async Task HealthCheck_Keycloak_OnDependenciesNotOnReady()
    {
        using var ready = JsonDocument.Parse(await _client.GetStringAsync(new Uri("/health/ready", UriKind.Relative)));
        using var dependencies = JsonDocument.Parse(await _client.GetStringAsync(new Uri("/health/dependencies", UriKind.Relative)));

        Assert.False(ready.RootElement.GetProperty("entries").TryGetProperty("keycloak", out _));
        Assert.True(dependencies.RootElement.GetProperty("entries").TryGetProperty("keycloak", out var keycloak));
        Assert.Equal("Healthy", keycloak.GetProperty("status").GetString());
    }
}
