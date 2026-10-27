using Common.Domain.StronglyTypedIds;
using Inventory.Application.Persistence;
using Inventory.Domain.StockReservations;

namespace Inventory.Application.StockReservations;

/// <summary>
///     The reservations a store owner may read, found by one query on Inventory's own tables: the local product copy says which
///     store a reservation's product belongs to and the local store access copy says who owns that store and whether it is
///     active, so no request waits on Products. A reservation whose product is not projected yet, and a store the access copy
///     has never seen or that is no longer active, yield no row: an owner who owns nothing sees nothing, never someone else's
///     reservations.
/// </summary>
public static class StockReservationQueries
{
    /// <summary>The reservations of the products of the active stores <paramref name="ownerId" /> owns.</summary>
    public static IQueryable<StockReservation> StockReservationsOfActiveStoresOwnedBy(
        this IInventoryDbContext dbContext, ApplicationUserId ownerId)
    {
        return dbContext.StockReservations.Where(reservation => dbContext.ProductSummaries.Any(product =>
            product.ProductId == reservation.ProductId
            && dbContext.StoreAccess.Any(access => access.StoreId == product.StoreId && access.OwnerId == ownerId && access.IsActive)));
    }
}
