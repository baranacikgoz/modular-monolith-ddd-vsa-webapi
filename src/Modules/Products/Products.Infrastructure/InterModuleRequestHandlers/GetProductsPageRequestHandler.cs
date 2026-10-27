using Common.Application.Options;
using Common.Infrastructure.Persistence.Extensions;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Products.Application.Persistence;
using Products.Domain.Products;

namespace Products.Infrastructure.InterModuleRequestHandlers;

/// <summary>
///     Reconciliation source for another module's product read model: one keyset page of every product, newest first
///     (ProductId descending). The page is capped at <see cref="ProductsOptions.ProductsPageMaxSize" /> whatever size the
///     caller asks for, so a caller that expects a full page checks the response's next cursor, never its own page size.
///     Never on a request path.
/// </summary>
public class GetProductsPageRequestHandler(IProductsDbContext dbContext, IOptions<ProductsOptions> options)
    : InterModuleRequestHandler<GetProductsPageRequest, GetProductsPageResponse>
{
    public override async Task<GetProductsPageResponse> HandleAsync(GetProductsPageRequest request, CancellationToken cancellationToken)
    {
        var pageSize = Math.Min(request.PageSize, options.Value.ProductsPageMaxSize);

        // Every real id is a v7 uuid, so descending ProductId walks the newest product first.
        var after = new ProductId(request.AfterProductId ?? DefaultIdType.Empty);

        var page = await dbContext.Products
            .AsNoTracking()
            .TagWith(nameof(GetProductsPageRequestHandler))
            .WhereIf(p => p.Id < after, request.AfterProductId is not null)
            .OrderByDescending(p => p.Id)
            .Take(pageSize)
            .Select(p => new { p.Id, p.StoreId, p.Name, p.Version })
            .ToListAsync(cancellationToken);

        // .Value unwrapped only after materialization (Common.Tests/Architecture/StronglyTypedIdQueryTests.cs).
        var products = page
            .Select(row => new ProductPageItem(row.Id.Value, row.StoreId.Value, row.Name, row.Version))
            .ToList();

        var nextAfterProductId = products.Count == pageSize ? products[^1].ProductId : (DefaultIdType?)null;

        return new GetProductsPageResponse(products, nextAfterProductId);
    }
}
