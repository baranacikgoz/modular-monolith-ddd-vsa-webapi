using System.Net;
using System.Net.Http.Json;
using Common.IntegrationEvents;
using Common.Tests;
using Inventory.Domain.StockLevels;
using Inventory.Tests.Support;
using Xunit;

namespace Inventory.Tests.Endpoints.StockLevels;

[Collection("IntegrationTestCollection")]
public class ChangeLowStockThresholdTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private static Uri RouteOf(StockLevelId id) => new($"/v1/stock-levels/{id}/low-stock-threshold", UriKind.Relative);

    [Fact]
    public async Task ChangeLowStockThreshold_AboveOnHand_ReturnsNoContentAndQueuesTheCrossedIntegrationEvent()
    {
        var stockLevel = await StockLevelTestData.SeedStockLevelAsync(Factory.Services, quantityOnHand: 10);
        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.PutAsJsonAsync(RouteOf(stockLevel.Id), new { threshold = 12 });

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(12, (await StockLevelTestData.ReloadStockLevelAsync(Factory.Services, stockLevel.Id)).LowStockThreshold);
        var queued = await StockLevelTestData.QueuedEventsAsync<StockLowThresholdCrossedIntegrationEvent>(Factory.Services);
        Assert.Single(queued, e => e.ProductId == stockLevel.ProductId);
    }

    [Fact]
    public async Task ChangeLowStockThreshold_UnknownStockLevel_ReturnsNotFound()
    {
        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.PutAsJsonAsync(RouteOf(StockLevelId.New()), new { threshold = 5 });

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task ChangeLowStockThreshold_NegativeThreshold_ReturnsBadRequest()
    {
        var stockLevel = await StockLevelTestData.SeedStockLevelAsync(Factory.Services);
        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.PutAsJsonAsync(RouteOf(stockLevel.Id), new { threshold = -1 });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(0, (await StockLevelTestData.ReloadStockLevelAsync(Factory.Services, stockLevel.Id)).LowStockThreshold);
    }
}
