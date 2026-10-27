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
}
