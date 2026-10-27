using Common.Application.Options;
using Common.Application.Search;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NpgsqlTypes;
using Products.Application.Persistence;
using Products.Infrastructure.Caching;
using ZiggyCreatures.Caching.Fusion;

namespace Products.Infrastructure.InterModuleRequestHandlers;

/// <summary>
///     Backs another module's free-text product filter. Uses the product's generated full-text vector with the same dual
///     query as the product search endpoint (universal layer plus the caller's language). The answer is cut at
///     <see cref="FullTextSearchOptions.MaxMatchResults" /> and says so in <c>IsTruncated</c>, and it is cached for
///     <see cref="ProductsOptions.SearchCacheSeconds" />: a product edit shows up once the entry expires.
/// </summary>
public class SearchProductsByTermRequestHandler(
    IProductsDbContext dbContext,
    ISearchLanguageResolver searchLanguageResolver,
    IOptions<FullTextSearchOptions> fullTextSearchOptions,
    IFusionCache cache,
    IOptions<ProductsOptions> productsOptions)
    : InterModuleRequestHandler<SearchProductsByTermRequest, SearchProductsByTermResponse>
{
    public override async Task<SearchProductsByTermResponse> HandleAsync(SearchProductsByTermRequest request, CancellationToken cancellationToken)
    {
        var term = request.Term.Trim();
        if (term.Length == 0)
        {
            return new SearchProductsByTermResponse([], false);
        }

        var universalConfig = searchLanguageResolver.UniversalConfig;
        var proseConfig = searchLanguageResolver.ResolveConfig();
        var cap = fullTextSearchOptions.Value.MaxMatchResults;

        return await cache.GetOrSetSearchAsync(
            ProductsCacheKeys.ProductSearch(term, proseConfig, cap),
            productsOptions.Value,
            ct => SearchAsync(term, universalConfig, proseConfig, cap, ct),
            cancellationToken);
    }

    private async Task<SearchProductsByTermResponse> SearchAsync(
        string term, string universalConfig, string proseConfig, int cap, CancellationToken cancellationToken)
    {
        // Cap + 1 rows are read, so IsTruncated comes from the rows themselves and no extra COUNT query is needed.
        // Newest first keeps the cut deterministic.
        var ids = await dbContext.Products
            .AsNoTracking()
            .TagWith(nameof(SearchProductsByTermRequestHandler))
            .Where(p =>
                EF.Property<NpgsqlTsVector>(p, FullTextSearchOptions.SearchVectorColumn)
                    .Matches(EF.Functions.WebSearchToTsQuery(universalConfig, term))
                || EF.Property<NpgsqlTsVector>(p, FullTextSearchOptions.SearchVectorColumn)
                    .Matches(EF.Functions.WebSearchToTsQuery(proseConfig, term)))
            .OrderByDescending(p => p.Id)
            .Select(p => p.Id)
            .Take(cap + 1)
            .ToListAsync(cancellationToken);

        var isTruncated = ids.Count > cap;

        // .Value unwrapped only after materialization (Common.Tests/Architecture/StronglyTypedIdQueryTests.cs).
        return new SearchProductsByTermResponse(ids.Take(cap).Select(id => id.Value).ToList(), isTruncated);
    }
}
