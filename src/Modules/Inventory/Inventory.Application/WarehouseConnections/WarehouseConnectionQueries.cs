using Common.Domain.StronglyTypedIds;
using Inventory.Application.Persistence;
using Inventory.Domain.WarehouseConnections;

namespace Inventory.Application.WarehouseConnections;

public static class WarehouseConnectionQueries
{
    /// <summary>
    ///     The active connection of the store that owns <paramref name="productId" />, found through the local product copy.
    ///     A store has at most one active connection, so a single-row fetch is safe; a product that is not projected yet, or a
    ///     store that is not connected, yields no row.
    /// </summary>
    public static IQueryable<WarehouseConnection> ActiveConnectionOfProduct(this IInventoryDbContext dbContext, DefaultIdType productId)
    {
        return dbContext.WarehouseConnections
            .Where(c => c.Status == WarehouseConnectionStatus.Active
                        && dbContext.ProductSummaries.Any(p => p.ProductId == productId && p.StoreId == c.StoreId));
    }

    /// <summary>
    ///     The connections of the stores <paramref name="ownerId" /> owns and that are active, found by a join on the local store access
    ///     copy, so no request waits on Products. A store the copy has not seen yet, or that is no longer active, yields no row.
    /// </summary>
    public static IQueryable<WarehouseConnection> OfActiveStoresOwnedBy(this IInventoryDbContext dbContext, ApplicationUserId ownerId)
    {
        return dbContext.WarehouseConnections.Join(
            dbContext.StoreAccess.Where(a => a.OwnerId == ownerId && a.IsActive),
            connection => connection.StoreId,
            access => access.StoreId,
            (connection, _) => connection);
    }
}
