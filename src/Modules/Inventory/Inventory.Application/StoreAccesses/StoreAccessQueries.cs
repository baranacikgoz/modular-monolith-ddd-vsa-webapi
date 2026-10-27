using Common.Domain.StronglyTypedIds;
using Inventory.Application.Persistence;

namespace Inventory.Application.StoreAccesses;

public static class StoreAccessQueries
{
    /// <summary>
    ///     The store access rows of the active stores <paramref name="ownerId" /> owns: the answer to "may this caller act on this
    ///     store" read from Inventory's own copy, so no request waits on Products. A store the copy has not seen yet, or that is no
    ///     longer active, has no row here.
    /// </summary>
    public static IQueryable<StoreAccess> ActiveStoresOwnedBy(this IInventoryDbContext dbContext, ApplicationUserId ownerId)
    {
        return dbContext.StoreAccess.Where(access => access.OwnerId == ownerId && access.IsActive);
    }
}
