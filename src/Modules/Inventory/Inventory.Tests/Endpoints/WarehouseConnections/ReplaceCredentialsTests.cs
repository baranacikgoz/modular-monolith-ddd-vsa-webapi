using System.Net;
using System.Net.Http.Json;
using Common.Tests;
using Inventory.Domain.WarehouseConnections;
using Inventory.Tests.Support;
using Xunit;

namespace Inventory.Tests.Endpoints.WarehouseConnections;

[Collection("IntegrationTestCollection")]
public class ReplaceCredentialsTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private static Uri RouteOf(WarehouseConnectionId id) => new($"/v1/warehouse-connections/{id}/credentials", UriKind.Relative);

    [Fact]
    public async Task ReplaceCredentials_ActiveConnection_StoresTheNewCredentialEncryptedAndKeepsTheWebhookSecret()
    {
        var (connection, webhookSecret) = await WarehouseConnectionTestData.SeedAsync(Factory.Services);
        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.PutAsJsonAsync(RouteOf(connection.Id), new { apiCredential = "replacement-credential" });

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var stored = await WarehouseConnectionTestData.ReloadAsync(Factory.Services, connection.Id);
        Assert.Equal("replacement-credential", WarehouseConnectionTestData.Decrypt(Factory.Services, stored.EncryptedApiCredential));
        Assert.Equal(webhookSecret, WarehouseConnectionTestData.Decrypt(Factory.Services, stored.EncryptedWebhookSecret));
    }

    [Fact]
    public async Task ReplaceCredentials_UnknownConnection_ReturnsNotFound()
    {
        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.PutAsJsonAsync(RouteOf(WarehouseConnectionId.New()), new { apiCredential = "replacement-credential" });

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task ReplaceCredentials_DeactivatedConnection_ReturnsBadRequestAndKeepsTheCredential()
    {
        var (connection, _) = await WarehouseConnectionTestData.SeedAsync(Factory.Services, deactivated: true);
        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.PutAsJsonAsync(RouteOf(connection.Id), new { apiCredential = "replacement-credential" });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var stored = await WarehouseConnectionTestData.ReloadAsync(Factory.Services, connection.Id);
        Assert.Equal(WarehouseConnectionTestData.ApiCredential, WarehouseConnectionTestData.Decrypt(Factory.Services, stored.EncryptedApiCredential));
    }

    [Fact]
    public async Task ReplaceCredentials_EmptyCredential_ReturnsBadRequest()
    {
        var (connection, _) = await WarehouseConnectionTestData.SeedAsync(Factory.Services);
        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.PutAsJsonAsync(RouteOf(connection.Id), new { apiCredential = string.Empty });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
