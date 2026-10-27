using Common.Application.Persistence.Projections;
using Common.Domain.StronglyTypedIds;

namespace Inventory.Application.StoreAccesses;

/// <summary>
///     Inventory's local copy of who owns a store and whether the store is active, so a route that lists or acts on "my
///     store's" data decides access with a join on its own schema instead of asking Products. Fed by Products'
///     <c>StoreAccessChangedIntegrationEvent</c>, healed nightly by <c>StoreAccessReconciliationJob</c>. Keyed by the Products
///     store id. A store with no row yet (its first event not consumed, the nightly run not made) counts as not accessible.
/// </summary>
public sealed class StoreAccess : ProjectionEntity
{
    private StoreAccess()
    {
    } // EF Core needs a parameterless ctor

    public DefaultIdType StoreId { get; private init; }
    public ApplicationUserId OwnerId { get; private set; }

    /// <summary>True only while the store is Active at Products; pending, suspended and deactivated stores are not.</summary>
    public bool IsActive { get; private set; }

    public static StoreAccess Create(DefaultIdType storeId)
    {
        return new StoreAccess { StoreId = storeId };
    }

    /// <summary>True when the row now differs from what it held before.</summary>
    public bool Apply(ApplicationUserId ownerId, bool isActive)
    {
        var changed = OwnerId != ownerId || IsActive != isActive;

        OwnerId = ownerId;
        IsActive = isActive;

        return changed;
    }

    /// <summary>
    ///     Applies the source only when <paramref name="version" /> is newer than the row's (a stale event or a stale
    ///     reconciliation page must not reopen a store a later event closed), and reports whether the row changed.
    /// </summary>
    public bool ApplyIfNewer(ApplicationUserId ownerId, bool isActive, long version)
    {
        return AcceptVersion(version) && Apply(ownerId, isActive);
    }
}
