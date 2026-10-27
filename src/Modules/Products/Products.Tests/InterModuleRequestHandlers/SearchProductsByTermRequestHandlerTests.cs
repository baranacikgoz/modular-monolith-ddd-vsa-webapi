using Common.Application.Search;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Products.Infrastructure.InterModuleRequestHandlers;
using Products.Tests.Support;
using Xunit;
using ZiggyCreatures.Caching.Fusion;

namespace Products.Tests.InterModuleRequestHandlers;

public class SearchProductsByTermRequestHandlerTests(IntegrationTestWebAppFactory factory) : ProductsIntegrationTest(factory)
{
    // A word of its own per test: the cache lives as long as the host, so a shared term would answer from another test.
    private static string UniqueWord() => $"zq{Guid.NewGuid():N}";

    private SearchProductsByTermRequestHandler Handler(
        int cacheSeconds = 0, int maxMatchResults = 200)
    {
        return new SearchProductsByTermRequestHandler(
            Db,
            Scope.ServiceProvider.GetRequiredService<ISearchLanguageResolver>(),
            TestOptions.FullTextSearch(o => o.MaxMatchResults = maxMatchResults),
            Scope.ServiceProvider.GetRequiredService<IFusionCache>(),
            TestOptions.Products(o => o.SearchCacheSeconds = cacheSeconds));
    }

    [Fact]
    public async Task SendAsync_TermInTheName_ReturnsOnlyTheMatchingProducts()
    {
        var word = UniqueWord();
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, DefaultUser, $"{word} desk lamp", "Office chair");
        var client = Scope.ServiceProvider.GetRequiredService<IInterModuleRequestClient<SearchProductsByTermRequest, SearchProductsByTermResponse>>();

        var response = await client.SendAsync(new SearchProductsByTermRequest(word), CancellationToken.None);

        Assert.Equal([seeded.Products[0].Id.Value], response.ProductIds);
        Assert.False(response.IsTruncated);
    }

    [Fact]
    public async Task HandleAsync_BlankTerm_ReturnsNothing()
    {
        var response = await Handler().HandleAsync(new SearchProductsByTermRequest("   "), CancellationToken.None);

        Assert.Empty(response.ProductIds);
        Assert.False(response.IsTruncated);
    }

    [Fact]
    public async Task HandleAsync_MoreMatchesThanTheCap_ReturnsTheCapAndSaysTruncated()
    {
        var word = UniqueWord();
        await ProductsSeed.StoreWithProductsAsync(Db, DefaultUser, $"{word} one", $"{word} two", $"{word} three");
        // The cut keeps the highest ids in Postgres' own order, so the same matches come back on every call.
        var expected = (await Db.Products.AsNoTracking().OrderByDescending(p => p.Id).Select(p => p.Id).Take(2).ToListAsync())
            .Select(id => id.Value).ToList();

        var response = await Handler(maxMatchResults: 2).HandleAsync(new SearchProductsByTermRequest(word), CancellationToken.None);

        Assert.True(response.IsTruncated);
        Assert.Equal(expected, response.ProductIds);
    }

    [Fact]
    public async Task HandleAsync_RepeatedWithinTheCacheWindow_AnswersFromTheCache()
    {
        var word = UniqueWord();
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, DefaultUser, $"{word} first");
        var handler = Handler(cacheSeconds: 60);

        var first = await handler.HandleAsync(new SearchProductsByTermRequest(word), CancellationToken.None);
        await Db.Products.Where(p => p.Id == seeded.Products[0].Id).ExecuteDeleteAsync();
        var cached = await handler.HandleAsync(new SearchProductsByTermRequest(word), CancellationToken.None);

        Assert.Single(first.ProductIds);
        Assert.Equal(first.ProductIds, cached.ProductIds);
    }

    [Fact]
    public async Task HandleAsync_CacheDisabled_AlwaysQueries()
    {
        var word = UniqueWord();
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, DefaultUser, $"{word} first");
        var handler = Handler(cacheSeconds: 0);

        var first = await handler.HandleAsync(new SearchProductsByTermRequest(word), CancellationToken.None);
        await Db.Products.Where(p => p.Id == seeded.Products[0].Id).ExecuteDeleteAsync();
        var fresh = await handler.HandleAsync(new SearchProductsByTermRequest(word), CancellationToken.None);

        Assert.Single(first.ProductIds);
        Assert.Empty(fresh.ProductIds);
    }
}
