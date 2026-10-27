using System.Net;
using System.Net.Http.Json;
using Common.Application.Pagination;
using Common.InterModuleRequests.Products;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Application.ProductSummaries;
using Inventory.Domain.StockLevels;
using Inventory.Endpoints.StockLevels.v1.Search;
using Inventory.Tests.Support;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace Inventory.Tests.Endpoints.StockLevels;

[Collection("IntegrationTestCollection")]
public class SearchTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private static readonly DefaultIdType StoreId = DefaultIdType.CreateVersion7();

    public override async ValueTask InitializeAsync()
    {
        await base.InitializeAsync();
        factory.ProductSearchClient.ClearReceivedCalls();
    }

    private async Task<PageResponse> GetPageAsync(string query)
    {
        var client = StockLevelTestData.AuthedClient(Factory);
        var response = await client.GetAsync(new Uri($"/v1/stock-levels?{query}", UriKind.Relative));
        Assert.True(response.IsSuccessStatusCode, await response.Content.ReadAsStringAsync());
        return (await response.Content.ReadFromJsonAsync<PageResponse>(JsonSerializerOptions))!;
    }

    private async Task SeedProductSummaryAsync(DefaultIdType productId, string name)
    {
        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        var summary = ProductSummary.Create(productId);
        summary.ApplyIfNewer(StoreId, name, 1);
        db.ProductSummaries.Add(summary);
        await db.SaveChangesAsync();
    }

    private void ProductSearchReturns(bool isTruncated, params DefaultIdType[] productIds)
    {
        factory.ProductSearchClient
            .SendAsync(Arg.Any<SearchProductsByTermRequest>(), Arg.Any<CancellationToken>())
            .Returns(new SearchProductsByTermResponse(productIds, isTruncated));
    }

    // A stock level whose product has not been projected yet must still be listed: that is the LEFT in the join.
    [Fact]
    public async Task Search_StockLevelWithoutProductCopy_IsListedWithANullName()
    {
        var projected = await StockLevelTestData.SeedStockLevelAsync(Factory.Services, quantityOnHand: 1);
        var notProjected = await StockLevelTestData.SeedStockLevelAsync(Factory.Services, quantityOnHand: 2);
        await SeedProductSummaryAsync(projected.ProductId, "Widget");

        var page = await GetPageAsync("pageNumber=1&pageSize=10");

        Assert.Equal(2, page.TotalCount);
        var named = Assert.Single(page.Data, r => r.ProductId == projected.ProductId);
        Assert.Equal("Widget", named.ProductName);
        Assert.Equal(StoreId, named.StoreId);
        var nameless = Assert.Single(page.Data, r => r.ProductId == notProjected.ProductId);
        Assert.Null(nameless.ProductName);
        Assert.Null(nameless.StoreId);
        Assert.False(page.IsTruncated);
        await factory.ProductSearchClient.DidNotReceive()
            .SendAsync(Arg.Any<SearchProductsByTermRequest>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Search_Paged_OrdersByLeastStockFirstAndCountsEveryRow()
    {
        await StockLevelTestData.SeedStockLevelAsync(Factory.Services, quantityOnHand: 5);
        await StockLevelTestData.SeedStockLevelAsync(Factory.Services, quantityOnHand: 1);
        await StockLevelTestData.SeedStockLevelAsync(Factory.Services, quantityOnHand: 3);

        var first = await GetPageAsync("pageNumber=1&pageSize=2");
        var second = await GetPageAsync("pageNumber=2&pageSize=2");

        Assert.Equal(3, first.TotalCount);
        Assert.Equal([1, 3], first.Data.Select(r => r.QuantityOnHand).ToArray());
        Assert.Equal([5], second.Data.Select(r => r.QuantityOnHand).ToArray());
    }

    [Fact]
    public async Task Search_ProductNameFilter_KeepsOnlyMatchedProductsAndReportsTruncation()
    {
        var matched = await StockLevelTestData.SeedStockLevelAsync(Factory.Services, quantityOnHand: 1);
        var other = await StockLevelTestData.SeedStockLevelAsync(Factory.Services, quantityOnHand: 2);
        await SeedProductSummaryAsync(matched.ProductId, "Blue widget");
        await SeedProductSummaryAsync(other.ProductId, "Red gadget");
        ProductSearchReturns(isTruncated: true, matched.ProductId, DefaultIdType.CreateVersion7());

        var page = await GetPageAsync("pageNumber=1&pageSize=10&productName=widget");

        var row = Assert.Single(page.Data);
        Assert.Equal(matched.ProductId, row.ProductId);
        Assert.Equal(1, page.TotalCount);
        Assert.True(page.IsTruncated);
        await factory.ProductSearchClient.Received(1)
            .SendAsync(Arg.Is<SearchProductsByTermRequest>(r => r.Term == "widget"), Arg.Any<CancellationToken>());
    }

    // The filter keeps stock levels whose product Products matched even when the local copy has no name for them yet.
    [Fact]
    public async Task Search_ProductNameFilter_ListsAMatchedProductThatIsNotProjectedYet()
    {
        var matched = await StockLevelTestData.SeedStockLevelAsync(Factory.Services, quantityOnHand: 1);
        ProductSearchReturns(isTruncated: false, matched.ProductId);

        var page = await GetPageAsync("pageNumber=1&pageSize=10&productName=widget");

        var row = Assert.Single(page.Data);
        Assert.Null(row.ProductName);
        Assert.False(page.IsTruncated);
    }

    [Fact]
    public async Task Search_ProductNameMatchesNothing_ReturnsAnEmptyPageNotEveryStockLevel()
    {
        await StockLevelTestData.SeedStockLevelAsync(Factory.Services, quantityOnHand: 1);
        ProductSearchReturns(isTruncated: false);

        var page = await GetPageAsync("pageNumber=1&pageSize=10&productName=nothing");

        Assert.Empty(page.Data);
        Assert.Equal(0, page.TotalCount);
    }

    [Fact]
    public async Task Search_BlankProductName_DoesNotAskProducts()
    {
        await StockLevelTestData.SeedStockLevelAsync(Factory.Services);

        var page = await GetPageAsync("pageNumber=1&pageSize=10&productName=%20");

        Assert.Single(page.Data);
        await factory.ProductSearchClient.DidNotReceive()
            .SendAsync(Arg.Any<SearchProductsByTermRequest>(), Arg.Any<CancellationToken>());
    }

    // Several rows share a quantity, so the page boundary falls inside a tie: the cursor must continue after exactly that row.
    [Fact]
    public async Task Search_FollowingTheCursor_WalksEveryRowOnceInTheOffsetOrder()
    {
        foreach (var quantity in new[] { 3, 1, 3, 2, 3, 5, 1, 3 })
        {
            await StockLevelTestData.SeedStockLevelAsync(Factory.Services, quantityOnHand: quantity);
        }

        var everything = await GetPageAsync("pageNumber=1&pageSize=50");
        var walked = new List<Response>();
        var pages = 0;
        string? cursor = null;
        do
        {
            var page = await GetPageAsync($"pageNumber=1&pageSize=3{(cursor is null ? string.Empty : $"&after={cursor}")}");
            walked.AddRange(page.Data);
            cursor = page.NextCursor;
            pages++;
        }
        while (cursor is not null && pages < 10);

        Assert.Equal(everything.Data.Select(r => r.Id).ToArray(), walked.Select(r => r.Id).ToArray());
        Assert.Equal(8, walked.Select(r => r.Id).Distinct().Count());
        Assert.Equal(everything.Data.Select(r => r.QuantityOnHand).Order().ToArray(), walked.Select(r => r.QuantityOnHand).ToArray());
        Assert.Null(everything.NextCursor);
    }

    [Fact]
    public async Task Search_CursorWithTheProductNameFilter_ContinuesInsideTheMatchedRows()
    {
        var matched = new List<StockLevel>();
        foreach (var quantity in new[] { 4, 2, 4, 6 })
        {
            matched.Add(await StockLevelTestData.SeedStockLevelAsync(Factory.Services, quantityOnHand: quantity));
        }

        await StockLevelTestData.SeedStockLevelAsync(Factory.Services, quantityOnHand: 1);
        ProductSearchReturns(isTruncated: false, [.. matched.Select(s => s.ProductId)]);

        var first = await GetPageAsync("pageNumber=1&pageSize=3&productName=widget&includeTotal=false");
        var second = await GetPageAsync($"pageNumber=1&pageSize=3&productName=widget&includeTotal=false&after={first.NextCursor}");

        Assert.Equal(-1, first.TotalCount);
        Assert.Equal(3, first.Data.Count);
        Assert.Single(second.Data);
        Assert.Equal(6, second.Data.Single().QuantityOnHand);
        Assert.Equal(matched.Select(s => s.ProductId).Order().ToArray(), first.Data.Concat(second.Data).Select(r => r.ProductId).Order().ToArray());
    }

    [Fact]
    public async Task Search_MalformedCursor_ReturnsBadRequest()
    {
        var client = StockLevelTestData.AuthedClient(Factory);

        var response = await client.GetAsync(new Uri("/v1/stock-levels?pageNumber=1&pageSize=10&after=not-a-cursor", UriKind.Relative));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    // A well-formed cursor minted for another sort key type is user input too: a failed result, not an exception.
    [Fact]
    public async Task Search_CursorOfAnotherKeyType_ReturnsBadRequest()
    {
        var client = StockLevelTestData.AuthedClient(Factory);
        var cursor = PaginationCursor.Encode("Widget", DefaultIdType.CreateVersion7());

        var response = await client.GetAsync(new Uri($"/v1/stock-levels?pageNumber=1&pageSize=10&after={cursor}", UriKind.Relative));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Search_WithoutAuthentication_ReturnsUnauthorized()
    {
        var client = Factory.CreateClient();

        var response = await client.GetAsync(new Uri("/v1/stock-levels?pageNumber=1&pageSize=10", UriKind.Relative));

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
