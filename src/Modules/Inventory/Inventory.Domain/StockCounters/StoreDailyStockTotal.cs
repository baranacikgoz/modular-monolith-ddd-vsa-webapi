using Common.Domain.Entities;

namespace Inventory.Domain.StockCounters;

/// <summary>
///     Per-store, per-day totals of committed stock reservations, so a store's daily figures are one keyed read and
///     never an aggregation over reservations or the ledger. Not an aggregate: no invariant, no domain event, no audit
///     trail beyond the base columns. Written only by <c>StockCounterWriter</c>'s atomic SQL from the
///     <c>StockReservationCommittedIntegrationEvent</c> consumer (the hot counter exception in CLAUDE.md §4); read with
///     AsNoTracking. A row exists from the first reservation a store commits on that day.
/// </summary>
// Rows are only ever inserted and updated by StockCounterWriter's SQL and materialized by EF via reflection, so the
// private constructor and setters have no C# caller by design.
#pragma warning disable S3453, S1144
public sealed class StoreDailyStockTotal : AuditableEntity
{
    private StoreDailyStockTotal()
    {
    } // EF Core needs a parameterless ctor

    public DefaultIdType StoreId { get; private init; }

    /// <summary>The calendar day in the business time zone (<c>InventoryOptions.BusinessTimeZoneId</c>) the reservations were committed on.</summary>
    public DateOnly Day { get; private init; }

    public int CommittedReservations { get; private set; }

    public long CommittedUnits { get; private set; }
}
#pragma warning restore S3453, S1144
