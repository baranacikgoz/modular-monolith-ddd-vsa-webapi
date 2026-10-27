using System.Net;
using System.Net.Http.Json;
using Common.Application.Pagination;
using Common.Domain.StronglyTypedIds;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Domain.StockLevels;
using Inventory.Tests.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Response = Inventory.Endpoints.StockLevels.v1.Movements.Response;

namespace Inventory.Tests.Endpoints.StockLevels;

/// <summary>The ledger of one of the caller's own stock levels. A stock level of any other store is answered as not found.</summary>
[Collection("IntegrationTestCollection")]
public class MyMovementsTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private static ApplicationUserId NewOwner() => new(DefaultIdType.CreateVersion7());

    private static Uri RouteOf(StockLevelId id, string query) => new($"/v1/stock-levels/my/{id}/movements?{query}", UriKind.Relative);

    private async Task<StockLevel> SeedLedgerOfStoreAsync(DefaultIdType storeId, params int[] receipts)
    {
        var stockLevel = await StockLevelTestData.SeedStockLevelOfStoreAsync(Factory.Services, storeId, quantityOnHand: 0);
        foreach (var quantity in receipts)
        {
            await using var scope = Factory.Services.CreateAsyncScope();
            var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
            var tracked = await db.StockLevels.SingleAsync(s => s.Id == stockLevel.Id);
            tracked.Receive(quantity, $"Delivery {quantity}");
            await db.SaveChangesAsync();
        }

        return stockLevel;
    }

    [Fact]
    public async Task GetMyMovements_OwnStockLevel_ReturnsTheLedgerNewestFirst()
    {
        var owner = NewOwner();
        var storeId = DefaultIdType.CreateVersion7();
        await StockLevelTestData.GrantStoreAccessAsync(Factory.Services, storeId, owner);
        var stockLevel = await SeedLedgerOfStoreAsync(storeId, 1, 2, 3);

        var response = await StockLevelTestData.AuthedClientOf(Factory, owner).GetAsync(RouteOf(stockLevel.Id, "pageNumber=1&pageSize=10"));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var page = (await response.Content.ReadFromJsonAsync<PaginationResponse<Response>>(JsonSerializerOptions))!;
        Assert.Equal([3, 2, 1], page.Data.Select(m => m.Quantity).ToArray());
    }

    [Fact]
    public async Task GetMyMovements_FollowingTheCursor_WalksEveryRowOnce()
    {
        var owner = NewOwner();
        var storeId = DefaultIdType.CreateVersion7();
        await StockLevelTestData.GrantStoreAccessAsync(Factory.Services, storeId, owner);
        var stockLevel = await SeedLedgerOfStoreAsync(storeId, 1, 2, 3);
        var client = StockLevelTestData.AuthedClientOf(Factory, owner);

        var first = (await (await client.GetAsync(RouteOf(stockLevel.Id, "pageNumber=1&pageSize=2")))
            .Content.ReadFromJsonAsync<PaginationResponse<Response>>(JsonSerializerOptions))!;
        var second = (await (await client.GetAsync(RouteOf(stockLevel.Id, $"pageNumber=1&pageSize=2&after={first.NextCursor}")))
            .Content.ReadFromJsonAsync<PaginationResponse<Response>>(JsonSerializerOptions))!;

        Assert.Equal([3, 2], first.Data.Select(m => m.Quantity).ToArray());
        Assert.Equal([1], second.Data.Select(m => m.Quantity).ToArray());
    }

    // Cross-store negative: the id of another owner's stock level answers exactly like an id that does not exist.
    [Fact]
    public async Task GetMyMovements_StockLevelOfAnotherOwnersStore_ReturnsNotFound()
    {
        var storeId = DefaultIdType.CreateVersion7();
        await StockLevelTestData.GrantStoreAccessAsync(Factory.Services, storeId, NewOwner());
        var theirs = await SeedLedgerOfStoreAsync(storeId, 1);

        var response = await StockLevelTestData.AuthedClientOf(Factory, NewOwner()).GetAsync(RouteOf(theirs.Id, "pageNumber=1&pageSize=10"));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetMyMovements_StoreThatIsNoLongerActive_ReturnsNotFound()
    {
        var owner = NewOwner();
        var storeId = DefaultIdType.CreateVersion7();
        await StockLevelTestData.GrantStoreAccessAsync(Factory.Services, storeId, owner, isActive: false);
        var stockLevel = await SeedLedgerOfStoreAsync(storeId, 1);

        var response = await StockLevelTestData.AuthedClientOf(Factory, owner).GetAsync(RouteOf(stockLevel.Id, "pageNumber=1&pageSize=10"));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetMyMovements_WithoutAuthentication_ReturnsUnauthorized()
    {
        var response = await Factory.CreateClient().GetAsync(RouteOf(StockLevelId.New(), "pageNumber=1&pageSize=10"));

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
