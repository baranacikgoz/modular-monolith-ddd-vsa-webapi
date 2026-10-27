using System.Net;
using Common.Tests;
using Inventory.Domain.WarehouseConnections;
using Inventory.Tests.Support;
using Xunit;

namespace Inventory.Tests.Endpoints.WarehouseConnections;

[Collection("IntegrationTestCollection")]
public class DeactivateTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private static Uri RouteOf(WarehouseConnectionId id) => new($"/v1/warehouse-connections/{id}/deactivate", UriKind.Relative);

    [Fact]
    public async Task Deactivate_ActiveConnection_ReturnsNoContentAndKeepsTheRow()
    {
        var (connection, _) = await WarehouseConnectionTestData.SeedAsync(Factory.Services);
        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.PostAsync(RouteOf(connection.Id), null);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var stored = await WarehouseConnectionTestData.ReloadAsync(Factory.Services, connection.Id);
        Assert.Equal(WarehouseConnectionStatus.Deactivated, stored.Status);
    }

    [Fact]
    public async Task Deactivate_AlreadyDeactivated_IsAnIdempotentNoOp()
    {
        var (connection, _) = await WarehouseConnectionTestData.SeedAsync(Factory.Services, deactivated: true);
        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.PostAsync(RouteOf(connection.Id), null);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Deactivate_UnknownConnection_ReturnsNotFound()
    {
        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.PostAsync(RouteOf(WarehouseConnectionId.New()), null);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
