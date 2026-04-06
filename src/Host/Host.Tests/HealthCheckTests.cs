using System.Net;
using System.Text.Json;

namespace Host.Tests;

public class HealthCheckTests
{
    [Theory]
    [InlineData("/health/live")]
    [InlineData("/health/ready")]
    [InlineData("/health/startup")]
    public async Task HealthCheck_Endpoint_ReturnsHealthy(string endpoint)
    {
        // Arrange
        await using var factory = new HostTestFactory();
        await factory.InitializeAsync();
        using var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync(new Uri(endpoint, UriKind.Relative));

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(content);
        var status = doc.RootElement.GetProperty("status").GetString();
        Assert.Equal("Healthy", status);
    }

    [Fact]
    public async Task HealthCheck_ReadyEndpoint_ReturnsPostgresqlEntry()
    {
        // Arrange
        await using var factory = new HostTestFactory();
        await factory.InitializeAsync();
        using var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync(new Uri("/health/ready", UriKind.Relative));

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(content);

        var entries = doc.RootElement.GetProperty("entries");
        Assert.True(entries.TryGetProperty("postgresql", out var pgEntry));
        Assert.Equal("Healthy", pgEntry.GetProperty("status").GetString());
    }
}
