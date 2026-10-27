using System.Net;
using System.Net.Http.Json;
using Common.Tests;
using Inventory.Domain.WarehouseConnections;
using Inventory.Endpoints.WarehouseConnections;
using Inventory.Tests.Support;
using Xunit;

namespace Inventory.Tests.Endpoints.WarehouseConnections;

[Collection("IntegrationTestCollection")]
public class GetTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private static Uri RouteOf(WarehouseConnectionId id) => new($"/v1/warehouse-connections/{id}", UriKind.Relative);

    [Fact]
    public async Task Get_ExistingConnection_ReturnsItsFactsAndNoSecretInAnyForm()
    {
        var storeId = DefaultIdType.CreateVersion7();
        var (connection, webhookSecret) = await WarehouseConnectionTestData.SeedAsync(Factory.Services, storeId, WarehouseProvider.FormApi);
        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.GetAsync(RouteOf(connection.Id));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var body = await response.Content.ReadAsStringAsync();
        var parsed = (await response.Content.ReadFromJsonAsync<WarehouseConnectionResponse>(JsonSerializerOptions))!;
        Assert.Equal(connection.Id, parsed.Id);
        Assert.Equal(storeId, parsed.StoreId);
        Assert.Equal(WarehouseProvider.FormApi, parsed.Provider);
        Assert.Equal(WarehouseConnectionStatus.Active, parsed.Status);
        Assert.Equal(WarehouseConnectionTestData.BaseUrl, parsed.BaseUrl);
        Assert.DoesNotContain(webhookSecret, body, StringComparison.Ordinal);
        Assert.DoesNotContain(WarehouseConnectionTestData.ApiCredential, body, StringComparison.Ordinal);
        Assert.DoesNotContain("secret", body, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("credential", body, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("encrypted", body, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task Get_UnknownConnection_ReturnsNotFound()
    {
        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.GetAsync(RouteOf(WarehouseConnectionId.New()));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
