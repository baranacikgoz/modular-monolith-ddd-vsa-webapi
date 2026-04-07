using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Products.Infrastructure.Telemetry;

/// <summary>
///     Centralized telemetry definitions for the Products module.
///     ActivitySource and Meter are thread-safe singletons by design.
///     Names derived from nameof() — no hardcoded magic strings.
/// </summary>
public static class ProductsTelemetry
{
    private const string Prefix = "ModularMonolith";

    /// <summary>
    ///     ActivitySource name: "ModularMonolith.Products"
    /// </summary>
    public const string ActivitySourceName = Prefix + "." + nameof(Products);

    /// <summary>
    ///     Meter name: "ModularMonolith.Products"
    /// </summary>
    public const string MeterName = Prefix + "." + nameof(Products);

    // ── Tracing ──────────────────────────────────────────────────────
    public static readonly ActivitySource ActivitySource = new(ActivitySourceName);

    // ── Metrics ──────────────────────────────────────────────────────
    public static readonly Meter Meter = new(MeterName);

    // ── Counters ─────────────────────────────────────────────────────
    public static readonly Counter<long> ProductsCreated =
        Meter.CreateCounter<long>("products.created.total", description: "Total products created");

    public static readonly Counter<long> ProductsAddedToStore =
        Meter.CreateCounter<long>("products.added_to_store.total", description: "Total products added to stores");

    public static readonly Counter<long> ProductsRemovedFromStore =
        Meter.CreateCounter<long>("products.removed_from_store.total", description: "Total products removed from stores");

    public static readonly Counter<long> StoresCreated =
        Meter.CreateCounter<long>("products.stores_created.total", description: "Total stores created");
}
