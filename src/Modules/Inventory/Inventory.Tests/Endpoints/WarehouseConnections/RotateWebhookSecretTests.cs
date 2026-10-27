using System.Net;
using System.Net.Http.Json;
using Common.Tests;
using Inventory.Domain.WarehouseConnections;
using Inventory.Tests.Support;
using Xunit;
using Response = Inventory.Endpoints.WarehouseConnections.v1.RotateWebhookSecret.Response;

namespace Inventory.Tests.Endpoints.WarehouseConnections;

[Collection("IntegrationTestCollection")]
public class RotateWebhookSecretTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private static Uri RouteOf(WarehouseConnectionId id) => new($"/v1/warehouse-connections/{id}/rotate-webhook-secret", UriKind.Relative);

    [Fact]
    public async Task RotateWebhookSecret_ActiveConnection_ShowsTheNewSecretOnceAndStoresItEncrypted()
    {
        var (connection, oldSecret) = await WarehouseConnectionTestData.SeedAsync(Factory.Services);
        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.PostAsync(RouteOf(connection.Id), null);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var rotated = (await response.Content.ReadFromJsonAsync<Response>(JsonSerializerOptions))!;
        Assert.NotEqual(oldSecret, rotated.WebhookSecret);
        Assert.EndsWith($"/webhooks/warehouse/{connection.Id}", rotated.WebhookPath, StringComparison.Ordinal);
        var stored = await WarehouseConnectionTestData.ReloadAsync(Factory.Services, connection.Id);
        Assert.Equal(rotated.WebhookSecret, WarehouseConnectionTestData.Decrypt(Factory.Services, stored.EncryptedWebhookSecret));
        Assert.Equal(WarehouseConnectionTestData.ApiCredential, WarehouseConnectionTestData.Decrypt(Factory.Services, stored.EncryptedApiCredential));
    }

    [Fact]
    public async Task RotateWebhookSecret_UnknownConnection_ReturnsNotFound()
    {
        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.PostAsync(RouteOf(WarehouseConnectionId.New()), null);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task RotateWebhookSecret_DeactivatedConnection_ReturnsBadRequestAndKeepsTheSecret()
    {
        var (connection, secret) = await WarehouseConnectionTestData.SeedAsync(Factory.Services, deactivated: true);
        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.PostAsync(RouteOf(connection.Id), null);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var stored = await WarehouseConnectionTestData.ReloadAsync(Factory.Services, connection.Id);
        Assert.Equal(secret, WarehouseConnectionTestData.Decrypt(Factory.Services, stored.EncryptedWebhookSecret));
    }
}
