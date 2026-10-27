using Common.Application.Persistence.Projections;

namespace Inventory.Application.ProductSummaries;

/// <summary>
///     Inventory's local copy of the facts it keeps about one product (owning store, name), so a stock list can show
///     and filter by product without asking Products on the request path. Fed by Products'
///     <c>ProductChangedIntegrationEvent</c>, healed nightly by <c>ProductSummariesReconciliationJob</c>. Keyed by the
///     Products product id. Products are never deleted, so rows are never removed either.
/// </summary>
public sealed class ProductSummary : ProjectionEntity
{
    // Wider than Products' own limit so a source that widens its column never makes a copy fail to save.
    public const int NameMaxLength = 256;

    private ProductSummary()
    {
    } // EF Core needs a parameterless ctor

    public DefaultIdType ProductId { get; private init; }
    public DefaultIdType StoreId { get; private set; }
    public string Name { get; private set; } = string.Empty;

    public static ProductSummary Create(DefaultIdType productId)
    {
        return new ProductSummary { ProductId = productId };
    }

    /// <summary>True when the row now differs from what it held before.</summary>
    public bool Apply(DefaultIdType storeId, string name)
    {
        var changed = StoreId != storeId || Name != name;

        StoreId = storeId;
        Name = name;

        return changed;
    }

    /// <summary>
    ///     Applies the source only when <paramref name="version" /> is newer than the row's (a stale event or a stale
    ///     reconciliation page must not undo a newer state), and reports whether the row changed.
    /// </summary>
    public bool ApplyIfNewer(DefaultIdType storeId, string name, long version)
    {
        return AcceptVersion(version) && Apply(storeId, name);
    }
}
