using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Products;
using Inventory.Application.Persistence;
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
            .WithDescription("List stock levels, least stock first, with the name of each product from the local product copy. " +
                             "ProductName narrows the list to the products Products matches; IsTruncated says that match list was cut. " +
                             "Pages by offset only.")
            .RequireScope(KeycloakScopes.StockLevels.View)
            .Produces<PageResponse>()
            .TransformResultTo<PageResponse>();
    }

    private static async Task<Result<PageResponse>> SearchStockLevelsAsync(
        [AsParameters] Request request,
        IInventoryDbContext dbContext,
        IInterModuleRequestClient<SearchProductsByTermRequest, SearchProductsByTermResponse> productSearchClient,
        CancellationToken cancellationToken)
    {
        var (matchedProductIds, isTruncated) = await ResolveProductNameAsync(productSearchClient, request.ProductName, cancellationToken);

        var rows = dbContext.StockLevels
            .AsNoTracking()
            .TagWith(nameof(SearchStockLevelsAsync))
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

        var totalCount = request.ShouldIncludeTotal ? await rows.CountAsync(cancellationToken) : -1;

        // PaginateAsync pages an auditable entity; the joined projection is not one, so this pages by offset directly.
        var data = await rows
            .OrderBy(r => r.QuantityOnHand)
            .ThenBy(r => r.Id)
            .Skip(request.Skip)
            .Take(request.Take)
            .ToListAsync(cancellationToken);

        return new PageResponse(data, totalCount, request.PageNumber, request.PageSize, isTruncated);
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
