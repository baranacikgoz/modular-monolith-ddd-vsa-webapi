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

    public static readonly Counter<long> ReservationSeriesCreated =
        Meter.CreateCounter<long>("inventory.reservation_series_created.total", description: "Total reservation series created");

    public static readonly Counter<long> ReservationsExpired =
        Meter.CreateCounter<long>("inventory.stock_reservations_expired.total", description: "Total stock reservations expired by the sweep job");
}
