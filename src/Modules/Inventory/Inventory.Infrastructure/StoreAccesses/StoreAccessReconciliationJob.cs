using Common.Application.Options;
using Common.Application.Persistence.Projections;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Products;
using Inventory.Application.Persistence;
using Inventory.Application.StoreAccesses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Inventory.Infrastructure.StoreAccesses;

/// <summary>Keyset cursor over Products' store list, walked newest first: the last (oldest so far) store id of the previous page.</summary>
public sealed record StoreAccessReconciliationCursor(DefaultIdType AfterStoreId);

/// <summary>
///     Nightly heal of <see cref="StoreAccess" /> from Products, page by page through <see cref="GetStoresPageRequest" />: the
///     one place Inventory asks Products for every store, never on a request path. It creates rows the events never delivered
///     and corrects an owner or an active flag a lost event left behind, each guarded by the store's version.
/// </summary>
public sealed class StoreAccessReconciliationJob(
    IInventoryDbContext dbContext,
    IInterModuleRequestClient<GetStoresPageRequest, GetStoresPageResponse> storesClient,
    IOptions<ProjectionReconciliationOptions> optionsProvider,
    ILogger<StoreAccessReconciliationJob> logger)
    : ProjectionReconciliationJob<StorePageItem, StoreAccessReconciliationCursor>(dbContext, optionsProvider, logger)
{
    protected override async Task<(IReadOnlyList<StorePageItem> Items, StoreAccessReconciliationCursor? Next)> FetchPageAsync(
        StoreAccessReconciliationCursor? cursor, int pageSize, CancellationToken cancellationToken)
    {
        var response = await storesClient.SendAsync(new GetStoresPageRequest(cursor?.AfterStoreId, pageSize), cancellationToken);

        return (response.Stores,
            response.NextAfterStoreId is { } next ? new StoreAccessReconciliationCursor(next) : null);
    }

    protected override async Task<int> ApplyPageAsync(IReadOnlyList<StorePageItem> items, CancellationToken cancellationToken)
    {
        var healed = 0;
        foreach (var source in items)
        {
            // FindAsync, not a query: it also sees a row added earlier in this same unsaved page, which a SELECT cannot.
            var row = await dbContext.StoreAccess.FindAsync([source.StoreId], cancellationToken);
            var created = row is null;
            if (row is null)
            {
                row = StoreAccess.Create(source.StoreId);
                dbContext.StoreAccess.Add(row);
            }

            var changed = row.ApplyIfNewer(source.OwnerId, source.IsActive, source.Version);
            if (changed || created)
            {
                healed++;
            }
        }

        return healed;
    }
}
