using System.Net;
using System.Net.Http.Json;
using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Products;
using Common.Tests;
using Inventory.Domain.StockLevels;
using Inventory.Endpoints.StockLevels.v1.Search;
using Inventory.Tests.Support;
using NSubstitute;
using Xunit;

namespace Inventory.Tests.Endpoints.StockLevels;

/// <summary>
///     The stock levels of the caller's own active stores, found through the local product copy and store access copy. The
///     rows of any other store never appear, whatever the caller asks for.
/// </summary>
[Collection("IntegrationTestCollection")]
public class MyStockLevelsSearchTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private static ApplicationUserId NewOwner() => new(DefaultIdType.CreateVersion7());

    public override async ValueTask InitializeAsync()
    {
        await base.InitializeAsync();
        factory.ProductSearchClient.ClearReceivedCalls();
    }

    private async Task<PageResponse> GetMineAsync(ApplicationUserId caller, string query = "pageNumber=1&pageSize=10")
    {
        var client = StockLevelTestData.AuthedClientOf(Factory, caller);

        var response = await client.GetAsync(new Uri($"/v1/stock-levels/my?{query}", UriKind.Relative));

        Assert.True(response.IsSuccessStatusCode, await response.Content.ReadAsStringAsync());
        return (await response.Content.ReadFromJsonAsync<PageResponse>(JsonSerializerOptions))!;
    }

    private async Task<(StockLevel[] Mine, StockLevel[] Theirs)> SeedTwoStoresAsync(ApplicationUserId owner, ApplicationUserId otherOwner)
    {
        var mineStore = DefaultIdType.CreateVersion7();
        var theirStore = DefaultIdType.CreateVersion7();
        await StockLevelTestData.GrantStoreAccessAsync(Factory.Services, mineStore, owner);
        await StockLevelTestData.GrantStoreAccessAsync(Factory.Services, theirStore, otherOwner);
        StockLevel[] mine =
        [
            await StockLevelTestData.SeedStockLevelOfStoreAsync(Factory.Services, mineStore, quantityOnHand: 4),
            await StockLevelTestData.SeedStockLevelOfStoreAsync(Factory.Services, mineStore, quantityOnHand: 2)
        ];
        StockLevel[] theirs =
        [
            await StockLevelTestData.SeedStockLevelOfStoreAsync(Factory.Services, theirStore, quantityOnHand: 1),
            await StockLevelTestData.SeedStockLevelOfStoreAsync(Factory.Services, theirStore, quantityOnHand: 3)
        ];
        return (mine, theirs);
    }

    [Fact]
    public async Task GetMine_CallerWithAnActiveStore_ListsOnlyTheStockLevelsOfThatStore()
    {
        var owner = NewOwner();
        var (mine, theirs) = await SeedTwoStoresAsync(owner, NewOwner());

        var page = await GetMineAsync(owner);

        Assert.Equal(mine.OrderBy(s => s.QuantityOnHand).Select(s => s.Id).ToArray(), page.Data.Select(r => r.Id).ToArray());
        Assert.Equal(2, page.TotalCount);
        Assert.DoesNotContain(page.Data, row => theirs.Any(s => s.Id == row.Id));
    }

    // Cross-store negative: asking by the other owner's store or product name never widens the answer.
    [Fact]
    public async Task GetMine_ProductNameMatchingAnotherStoresProduct_StillListsOnlyMine()
    {
        var owner = NewOwner();
        var (mine, theirs) = await SeedTwoStoresAsync(owner, NewOwner());
        factory.ProductSearchClient
            .SendAsync(Arg.Any<SearchProductsByTermRequest>(), Arg.Any<CancellationToken>())
            .Returns(new SearchProductsByTermResponse([mine[0].ProductId, .. theirs.Select(s => s.ProductId)], false));

        var page = await GetMineAsync(owner, "pageNumber=1&pageSize=10&productName=widget");

        var row = Assert.Single(page.Data);
        Assert.Equal(mine[0].Id, row.Id);
    }

    [Fact]
    public async Task GetMine_CallerWithoutAStore_GetsAnEmptyPageNotSomeoneElsesRows()
    {
        await SeedTwoStoresAsync(NewOwner(), NewOwner());

        var page = await GetMineAsync(NewOwner());

        Assert.Empty(page.Data);
        Assert.Equal(0, page.TotalCount);
        Assert.Null(page.NextCursor);
    }

    [Fact]
    public async Task GetMine_StoreThatIsNoLongerActive_DisappearsFromTheList()
    {
        var owner = NewOwner();
        var storeId = DefaultIdType.CreateVersion7();
        await StockLevelTestData.GrantStoreAccessAsync(Factory.Services, storeId, owner, isActive: false);
        await StockLevelTestData.SeedStockLevelOfStoreAsync(Factory.Services, storeId);

        var page = await GetMineAsync(owner);

        Assert.Empty(page.Data);
    }

    [Fact]
    public async Task GetMine_StockLevelWhoseProductIsNotProjectedYet_IsNotListed()
    {
        var owner = NewOwner();
        await StockLevelTestData.GrantStoreAccessAsync(Factory.Services, DefaultIdType.CreateVersion7(), owner);
        await StockLevelTestData.SeedStockLevelAsync(Factory.Services);

        var page = await GetMineAsync(owner);

        Assert.Empty(page.Data);
    }

    [Fact]
    public async Task GetMine_FollowingTheCursor_StaysInsideTheCallersStores()
    {
        var owner = NewOwner();
        var otherOwner = NewOwner();
        var mineStore = DefaultIdType.CreateVersion7();
        var theirStore = DefaultIdType.CreateVersion7();
        await StockLevelTestData.GrantStoreAccessAsync(Factory.Services, mineStore, owner);
        await StockLevelTestData.GrantStoreAccessAsync(Factory.Services, theirStore, otherOwner);
        var mine = new List<StockLevel>();
        foreach (var quantity in new[] { 5, 1, 5, 3, 5 })
        {
            mine.Add(await StockLevelTestData.SeedStockLevelOfStoreAsync(Factory.Services, mineStore, quantity));
            await StockLevelTestData.SeedStockLevelOfStoreAsync(Factory.Services, theirStore, quantity);
        }

        var walked = new List<Response>();
        string? cursor = null;
        var pages = 0;
        do
        {
            var page = await GetMineAsync(owner, $"pageNumber=1&pageSize=2{(cursor is null ? string.Empty : $"&after={cursor}")}");
            walked.AddRange(page.Data);
            cursor = page.NextCursor;
            pages++;
        }
        while (cursor is not null && pages < 10);

        Assert.Equal(mine.Select(s => s.Id.Value).Order().ToArray(), walked.Select(r => r.Id.Value).Order().ToArray());
        Assert.Equal(5, walked.Select(r => r.Id).Distinct().Count());
        Assert.Equal([1, 3, 5, 5, 5], walked.Select(r => r.QuantityOnHand).ToArray());
    }

    [Fact]
    public async Task GetMine_WithoutAuthentication_ReturnsUnauthorized()
    {
        var response = await Factory.CreateClient().GetAsync(new Uri("/v1/stock-levels/my?pageNumber=1&pageSize=10", UriKind.Relative));

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
