using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Inventory.Infrastructure.Telemetry;

/// <summary>
///     Centralized telemetry definitions for the Inventory module.
///     ActivitySource and Meter are thread-safe singletons by design.
///     Names derived from nameof(): no hardcoded magic strings.
/// </summary>
public static class InventoryTelemetry
{
    private const string Prefix = "ModularMonolith";
    private const string ModuleName = "Inventory";

    /// <summary>
    ///     ActivitySource name: "ModularMonolith.Inventory"
    /// </summary>
    public const string ActivitySourceName = Prefix + "." + ModuleName;

    /// <summary>
    ///     Meter name: "ModularMonolith.Inventory"
    /// </summary>
    public const string MeterName = Prefix + "." + ModuleName;

    // ── Tracing ──────────────────────────────────────────────────────
    public static readonly ActivitySource ActivitySource = new(ActivitySourceName);

    // ── Metrics ──────────────────────────────────────────────────────
    public static readonly Meter Meter = new(MeterName);

    // ── Counters ─────────────────────────────────────────────────────
    public static readonly Counter<long> StockReservationsReserved =
        Meter.CreateCounter<long>("inventory.stock_reservations_reserved.total", description: "Total stock reservations created");

    public static readonly Counter<long> StockReservationsCommitted =
        Meter.CreateCounter<long>("inventory.stock_reservations_committed.total", description: "Total stock reservations committed");

    public static readonly Counter<long> StockReservationsReleased =
        Meter.CreateCounter<long>("inventory.stock_reservations_released.total", description: "Total stock reservations released");

    public static readonly Counter<long> ReadOnlyReservationsResolved =
        Meter.CreateCounter<long>("inventory.read_only_reservations_resolved.total", description: "Total read-only stock reservations resolved by a person, tagged by target status");

    public static readonly Counter<long> StockReleaseReconciliations =
        Meter.CreateCounter<long>("inventory.stock_release_reconciliations.total", description: "Total release attempts whose unknown outcome was resolved with the warehouse system, tagged by outcome");

    public static readonly Counter<long> ReservationSeriesCreated =
        Meter.CreateCounter<long>("inventory.reservation_series_created.total", description: "Total reservation series created");

    public static readonly Counter<long> ReservationsExpired =
        Meter.CreateCounter<long>("inventory.stock_reservations_expired.total", description: "Total stock reservations expired by the sweep job");

    public static readonly Counter<long> StockMovementsRecorded =
        Meter.CreateCounter<long>("inventory.stock_movements_recorded.total", description: "Total ledger movements appended to stock levels, tagged by movement type");

    public static readonly Counter<long> StockPushItemsSent =
        Meter.CreateCounter<long>("inventory.stock_push_items_sent.total", description: "Total stock levels whose quantity a warehouse accepted");

    public static readonly Counter<long> WebhookDeliveries =
        Meter.CreateCounter<long>("inventory.warehouse_webhook_deliveries.total", description: "Total authenticated warehouse webhook deliveries, tagged by outcome (applied, duplicate, ignored, mirrored, unrecognized, stale)");
}
