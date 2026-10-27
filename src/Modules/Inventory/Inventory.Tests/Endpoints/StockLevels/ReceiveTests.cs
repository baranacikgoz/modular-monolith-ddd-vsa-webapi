using System.Net;
using System.Net.Http.Json;
using Common.IntegrationEvents;
using Common.Tests;
using Inventory.Domain.StockLevels;
using Inventory.Tests.Support;
using Xunit;

namespace Inventory.Tests.Endpoints.StockLevels;

[Collection("IntegrationTestCollection")]
public class ReceiveTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private static Uri RouteOf(StockLevelId id) => new($"/v1/stock-levels/{id}/receive", UriKind.Relative);

    [Fact]
    public async Task Receive_ValidRequest_ReturnsNoContentAndAppendsALedgerRowForTheActingUser()
    {
        var stockLevel = await StockLevelTestData.SeedStockLevelAsync(Factory.Services, quantityOnHand: 10);
        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.PostAsJsonAsync(RouteOf(stockLevel.Id), new { quantity = 5, reason = "Supplier delivery" });

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(15, (await StockLevelTestData.ReloadStockLevelAsync(Factory.Services, stockLevel.Id)).QuantityOnHand);
        var movement = Assert.Single(await StockLevelTestData.LedgerOfAsync(Factory.Services, stockLevel.Id));
        Assert.Equal(StockMovementType.Received, movement.Type);
        Assert.Equal(5, movement.Quantity);
        Assert.Equal(15, movement.QuantityOnHandAfter);
        Assert.Equal(TestAuthHandler.DefaultUserId, movement.CreatedBy?.Value);
    }

    [Fact]
    public async Task Receive_RecoveringAboveTheThreshold_QueuesTheRecoveredIntegrationEvent()
    {
        var stockLevel = await StockLevelTestData.SeedStockLevelAsync(Factory.Services, quantityOnHand: 3, lowStockThreshold: 5);
        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.PostAsJsonAsync(RouteOf(stockLevel.Id), new { quantity = 4 });

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var queued = await StockLevelTestData.QueuedEventsAsync<StockLowThresholdRecoveredIntegrationEvent>(Factory.Services);
        var recovered = Assert.Single(queued, e => e.ProductId == stockLevel.ProductId);
        Assert.Equal(7, recovered.QuantityOnHand);
        Assert.Equal(5, recovered.LowStockThreshold);
    }

    [Fact]
    public async Task Receive_UnknownStockLevel_ReturnsNotFound()
    {
        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.PostAsJsonAsync(RouteOf(StockLevelId.New()), new { quantity = 5 });

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public async Task Receive_NonPositiveQuantity_ReturnsBadRequest(int quantity)
    {
        var stockLevel = await StockLevelTestData.SeedStockLevelAsync(Factory.Services);
        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.PostAsJsonAsync(RouteOf(stockLevel.Id), new { quantity });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Empty(await StockLevelTestData.LedgerOfAsync(Factory.Services, stockLevel.Id));
    }

    [Fact]
    public async Task Receive_WithoutAuthentication_ReturnsUnauthorized()
    {
        var client = Factory.CreateClient();

        var response = await client.PostAsJsonAsync(RouteOf(StockLevelId.New()), new { quantity = 5 });

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
