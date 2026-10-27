using System.Net;
using System.Net.Http.Json;
using Common.IntegrationEvents;
using Common.Tests;
using Inventory.Domain.StockLevels;
using Inventory.Tests.Support;
using Xunit;

namespace Inventory.Tests.Endpoints.StockLevels;

[Collection("IntegrationTestCollection")]
public class AdjustTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private static Uri RouteOf(StockLevelId id) => new($"/v1/stock-levels/{id}/adjust", UriKind.Relative);

    [Fact]
    public async Task Adjust_ValidRequest_ReturnsNoContentAndRecordsTheReasonOnTheLedger()
    {
        var stockLevel = await StockLevelTestData.SeedStockLevelAsync(Factory.Services, quantityOnHand: 10);
        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.PostAsJsonAsync(RouteOf(stockLevel.Id), new { quantity = -4, reason = "Damaged in storage" });

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(6, (await StockLevelTestData.ReloadStockLevelAsync(Factory.Services, stockLevel.Id)).QuantityOnHand);
        var movement = Assert.Single(await StockLevelTestData.LedgerOfAsync(Factory.Services, stockLevel.Id));
        Assert.Equal(StockMovementType.Adjusted, movement.Type);
        Assert.Equal(-4, movement.Quantity);
        Assert.Equal("Damaged in storage", movement.Reason);
    }

    [Fact]
    public async Task Adjust_CrossingBelowTheThreshold_QueuesTheCrossedIntegrationEvent()
    {
        var stockLevel = await StockLevelTestData.SeedStockLevelAsync(Factory.Services, quantityOnHand: 10, lowStockThreshold: 5);
        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.PostAsJsonAsync(RouteOf(stockLevel.Id), new { quantity = -6, reason = "Count correction" });

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var queued = await StockLevelTestData.QueuedEventsAsync<StockLowThresholdCrossedIntegrationEvent>(Factory.Services);
        var crossed = Assert.Single(queued, e => e.ProductId == stockLevel.ProductId);
        Assert.Equal(4, crossed.QuantityOnHand);
        Assert.Equal(5, crossed.LowStockThreshold);
    }

    [Fact]
    public async Task Adjust_BelowZero_ReturnsInsufficientStockAndWritesNothing()
    {
        var stockLevel = await StockLevelTestData.SeedStockLevelAsync(Factory.Services, quantityOnHand: 3);
        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.PostAsJsonAsync(RouteOf(stockLevel.Id), new { quantity = -4, reason = "Count correction" });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Contains("InsufficientStock", await response.Content.ReadAsStringAsync(), StringComparison.Ordinal);
        Assert.Empty(await StockLevelTestData.LedgerOfAsync(Factory.Services, stockLevel.Id));
    }

    [Fact]
    public async Task Adjust_UnknownStockLevel_ReturnsNotFound()
    {
        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.PostAsJsonAsync(RouteOf(StockLevelId.New()), new { quantity = 1, reason = "Count correction" });

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Theory]
    [InlineData(0, "Count correction")]
    [InlineData(1, "")]
    public async Task Adjust_ZeroQuantityOrBlankReason_ReturnsBadRequest(int quantity, string reason)
    {
        var stockLevel = await StockLevelTestData.SeedStockLevelAsync(Factory.Services);
        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.PostAsJsonAsync(RouteOf(stockLevel.Id), new { quantity, reason });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Empty(await StockLevelTestData.LedgerOfAsync(Factory.Services, stockLevel.Id));
    }
}
