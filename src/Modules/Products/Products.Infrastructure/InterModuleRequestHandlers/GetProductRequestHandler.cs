using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Products;
using Microsoft.EntityFrameworkCore;
using Products.Application.Persistence;
using Products.Domain.Products;
using Products.Domain.Stores;

namespace Products.Infrastructure.InterModuleRequestHandlers;

public class GetProductRequestHandler(IProductsDbContext dbContext)
    : InterModuleRequestHandler<GetProductRequest, GetProductResponse>
{
    public override async Task<GetProductResponse> HandleAsync(GetProductRequest request, CancellationToken cancellationToken)
    {
        // Degrades gracefully instead of throwing on a missing product - mirrors GetStockLevelRequestHandler's
        // convention (Inventory's counterpart), since this base class has no Result-wrapped failure path.
        // Wraps the raw id before the query: see StronglyTypedIdValueConverter's remarks and
        // Common.Tests/Architecture/StronglyTypedIdQueryTests.cs.
        var productId = new ProductId(request.ProductId);
        var result = await dbContext.Products
            .AsNoTracking()
            .Where(p => p.Id == productId)
            .Select(p => new { p.Name, StoreIsActive = p.Store.Status == StoreStatus.Active })
            .SingleOrDefaultAsync(cancellationToken);

        return result is null
            ? new GetProductResponse(string.Empty, false)
            : new GetProductResponse(result.Name, result.StoreIsActive);
    }
}
