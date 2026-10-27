using Common.Domain.StronglyTypedIds;
using Inventory.Application.Persistence;
using Inventory.Domain.StockCounters;
using Inventory.Domain.StockLevels;

namespace Inventory.Application.StockLevels;

/// <summary>
///     The stock data a store owner may read, found by one query on Inventory's own tables: the local product copy says which
///     store a stock level belongs to and the local store access copy says who owns that store and whether it is active, so no
///     request waits on Products. A stock level whose product is not projected yet, and a store the access copy has never
///     seen or that is no longer active, yield no row: an owner who owns nothing sees nothing, never someone else's rows.
/// </summary>
public static class StockLevelQueries
{
    /// <summary>The stock levels of the products of the active stores <paramref name="ownerId" /> owns.</summary>
    public static IQueryable<StockLevel> StockLevelsOfActiveStoresOwnedBy(this IInventoryDbContext dbContext, ApplicationUserId ownerId)
    {
        return dbContext.StockLevels.Where(stockLevel => dbContext.ProductSummaries.Any(product =>
            product.ProductId == stockLevel.ProductId
            && dbContext.StoreAccess.Any(access => access.StoreId == product.StoreId && access.OwnerId == ownerId && access.IsActive)));
    }

    /// <summary>The daily totals of the active stores <paramref name="ownerId" /> owns.</summary>
    public static IQueryable<StoreDailyStockTotal> DailyTotalsOfActiveStoresOwnedBy(this IInventoryDbContext dbContext, ApplicationUserId ownerId)
    {
        return dbContext.StoreDailyStockTotals.Where(total => dbContext.StoreAccess.Any(access =>
            access.StoreId == total.StoreId && access.OwnerId == ownerId && access.IsActive));
    }
}
