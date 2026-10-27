using System.Net;
using System.Net.Http.Json;
using Common.Application.Pagination;
using Common.Tests;
using Inventory.Domain.StockLevels;
using Inventory.Tests.Support;
using Xunit;
using Response = Inventory.Endpoints.StockLevels.v1.Movements.Response;

namespace Inventory.Tests.Endpoints.StockLevels;

[Collection("IntegrationTestCollection")]
public class MovementsTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private static Uri ReceiveRouteOf(StockLevelId id) => new($"/v1/stock-levels/{id}/receive", UriKind.Relative);

    private static Uri MovementsRouteOf(StockLevelId id, string query) =>
        new($"/v1/stock-levels/{id}/movements?{query}", UriKind.Relative);

    private async Task<PaginationResponse<Response>> GetPageAsync(HttpClient client, StockLevelId id, string query)
    {
        var response = await client.GetAsync(MovementsRouteOf(id, query));
        Assert.True(response.IsSuccessStatusCode, await response.Content.ReadAsStringAsync());
        var page = await response.Content.ReadFromJsonAsync<PaginationResponse<Response>>(JsonSerializerOptions);
        return page!;
    }

    // Three receives made through the endpoint, so every ledger row carries the acting user.
    private async Task<StockLevel> SeedLedgerAsync(HttpClient client)
    {
        var stockLevel = await StockLevelTestData.SeedStockLevelAsync(Factory.Services, quantityOnHand: 0);
        foreach (var quantity in new[] { 1, 2, 3 })
        {
            var response = await client.PostAsJsonAsync(ReceiveRouteOf(stockLevel.Id), new { quantity, reason = $"Delivery {quantity}" });
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        return stockLevel;
    }

    [Fact]
    public async Task GetMovements_ReturnsTheLedgerNewestFirstWithTheActingUser()
    {
        var client = StockLevelTestData.AuthedClient(Factory);
        var stockLevel = await SeedLedgerAsync(client);

        var page = await GetPageAsync(client, stockLevel.Id, "pageNumber=1&pageSize=10");

        Assert.Equal([3, 2, 1], page.Data.Select(m => m.Quantity).ToArray());
        Assert.Equal([6, 3, 1], page.Data.Select(m => m.QuantityOnHandAfter).ToArray());
        Assert.All(page.Data, m => Assert.Equal(StockMovementType.Received, m.Type));
        Assert.All(page.Data, m => Assert.Equal(TestAuthHandler.DefaultUserId, m.ActingUserId?.Value));
        Assert.Equal("Delivery 3", page.Data.First().Reason);
        Assert.Null(page.NextCursor);
    }

    [Fact]
    public async Task GetMovements_FollowingTheCursor_WalksEveryRowOnce()
    {
        var client = StockLevelTestData.AuthedClient(Factory);
        var stockLevel = await SeedLedgerAsync(client);

        var first = await GetPageAsync(client, stockLevel.Id, "pageNumber=1&pageSize=2");
        var second = await GetPageAsync(client, stockLevel.Id, $"pageNumber=1&pageSize=2&after={first.NextCursor}");

        Assert.Equal([3, 2], first.Data.Select(m => m.Quantity).ToArray());
        Assert.NotNull(first.NextCursor);
        Assert.Equal([1], second.Data.Select(m => m.Quantity).ToArray());
        Assert.Null(second.NextCursor);
    }

    [Fact]
    public async Task GetMovements_UnknownStockLevel_ReturnsNotFound()
    {
        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.GetAsync(MovementsRouteOf(StockLevelId.New(), "pageNumber=1&pageSize=10"));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetMovements_MalformedCursor_ReturnsBadRequest()
    {
        var stockLevel = await StockLevelTestData.SeedStockLevelAsync(Factory.Services);
        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.GetAsync(MovementsRouteOf(stockLevel.Id, "pageNumber=1&pageSize=10&after=not-a-cursor"));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
