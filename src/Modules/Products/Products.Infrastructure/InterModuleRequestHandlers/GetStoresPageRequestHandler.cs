using Common.Application.Options;
using Common.Infrastructure.Persistence.Extensions;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Products.Application.Persistence;
using Products.Domain.Stores;

namespace Products.Infrastructure.InterModuleRequestHandlers;

/// <summary>
///     Reconciliation source for another module's store access read model: one keyset page of every store, newest first
///     (StoreId descending). The page is capped at <see cref="ProductsOptions.StoresPageMaxSize" /> whatever size the
///     caller asks for, so a caller that expects a full page checks the response's next cursor, never its own page size.
///     A store is active only while its status is Active, the same rule the live access events follow. Never on a request path.
/// </summary>
public class GetStoresPageRequestHandler(IProductsDbContext dbContext, IOptions<ProductsOptions> options)
    : InterModuleRequestHandler<GetStoresPageRequest, GetStoresPageResponse>
{
    public override async Task<GetStoresPageResponse> HandleAsync(GetStoresPageRequest request, CancellationToken cancellationToken)
    {
        var pageSize = Math.Min(request.PageSize, options.Value.StoresPageMaxSize);

        // Every real id is a v7 uuid, so descending StoreId walks the newest store first.
        var after = new StoreId(request.AfterStoreId ?? DefaultIdType.Empty);

        var page = await dbContext.Stores
            .AsNoTracking()
            .TagWith(nameof(GetStoresPageRequestHandler))
            .WhereIf(s => s.Id < after, request.AfterStoreId is not null)
            .OrderByDescending(s => s.Id)
            .Take(pageSize)
            .Select(s => new { s.Id, s.OwnerId, IsActive = s.Status == StoreStatus.Active, s.Version })
            .ToListAsync(cancellationToken);

        // .Value unwrapped only after materialization (Common.Tests/Architecture/StronglyTypedIdQueryTests.cs).
        var stores = page
            .Select(row => new StorePageItem(row.Id.Value, row.OwnerId, row.IsActive, row.Version))
            .ToList();

        var nextAfterStoreId = stores.Count == pageSize ? stores[^1].StoreId : (DefaultIdType?)null;

        return new GetStoresPageResponse(stores, nextAfterStoreId);
    }
}
