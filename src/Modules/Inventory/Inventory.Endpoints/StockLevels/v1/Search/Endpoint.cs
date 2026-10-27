using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Products;
using Inventory.Application.Persistence;
using Inventory.Domain.StockLevels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Endpoints.StockLevels.v1.Search;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder stockLevelsApiGroup)
    {
        stockLevelsApiGroup
            .MapGet("", SearchStockLevelsAsync)
            .WithDescription("List the stock levels of every store, least stock first, with the name of each product from the local product copy. " +
                             "ProductName narrows the list to the products Products matches; IsTruncated says that match list was cut. " +
                             "Pass the previous page's NextCursor as 'after' to continue without an offset. Administration only; a store owner reads their own through /my.")
            .RequireScope(KeycloakScopes.StockLevels.ViewAll)
            .Produces<PageResponse>()
            .TransformResultTo<PageResponse>();
    }

    private static Task<Result<PageResponse>> SearchStockLevelsAsync(
        [AsParameters] Request request,
        IInventoryDbContext dbContext,
        IInterModuleRequestClient<SearchProductsByTermRequest, SearchProductsByTermResponse> productSearchClient,
        CancellationToken cancellationToken)
    {
        return SearchAsync(dbContext.StockLevels, dbContext, request, productSearchClient, cancellationToken);
    }

    /// <summary>
    ///     The list over <paramref name="stockLevels" />, which is every stock level for the administration route and the
    ///     caller's own for the store owner's route: the only thing the two routes differ in.
    /// </summary>
    internal static async Task<Result<PageResponse>> SearchAsync(
        IQueryable<StockLevel> stockLevels,
        IInventoryDbContext dbContext,
        Request request,
        IInterModuleRequestClient<SearchProductsByTermRequest, SearchProductsByTermResponse> productSearchClient,
        CancellationToken cancellationToken)
    {
        var (matchedProductIds, isTruncated) = await ResolveProductNameAsync(productSearchClient, request.ProductName, cancellationToken);

        var rows = stockLevels
            .AsNoTracking()
            .TagWith(nameof(SearchAsync))
            // null: no ProductName given, no filter. An empty list: nothing matched, so zero rows.
            .WhereIf(s => matchedProductIds!.Contains(s.ProductId), matchedProductIds is not null)
            // LEFT JOIN: a stock level whose product is not projected yet is still listed, with a null name.
            .LeftJoin(
                dbContext.ProductSummaries.AsNoTracking(),
                stockLevel => stockLevel.ProductId,
                product => product.ProductId,
                (stockLevel, product) => new Response
                {
                    Id = stockLevel.Id,
                    ProductId = stockLevel.ProductId,
                    ProductName = product == null ? null : product.Name,
                    StoreId = product == null ? null : product.StoreId,
                    QuantityOnHand = stockLevel.QuantityOnHand,
                    LowStockThreshold = stockLevel.LowStockThreshold
                });

        // The joined projection is not an auditable entity, so it pages through the keyed overload: least stock first,
        // the stock level id as the unique tiebreaker the cursor carries.
        return await rows
            .PaginateKeyedAsync(
                selector: r => r,
                request: request,
                sortKey: r => r.QuantityOnHand,
                tiebreaker: r => r.Id,
                cancellationToken: cancellationToken)
            .MapAsync(page => new PageResponse(
                page.Data, page.TotalCount, page.PageNumber, page.PageSize, isTruncated, page.NextCursor));
    }

    private static async Task<(IReadOnlyCollection<DefaultIdType>? ProductIds, bool IsTruncated)> ResolveProductNameAsync(
        IInterModuleRequestClient<SearchProductsByTermRequest, SearchProductsByTermResponse> client,
        string? productName,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(productName))
        {
            return (null, false);
        }

        var response = await client.SendAsync(new SearchProductsByTermRequest(productName.Trim()), cancellationToken);
        return (response.ProductIds, response.IsTruncated);
    }
}
