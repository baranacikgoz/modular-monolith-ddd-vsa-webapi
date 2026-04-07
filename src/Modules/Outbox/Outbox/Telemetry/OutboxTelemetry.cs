using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Outbox.Telemetry;

/// <summary>
///     Centralized telemetry definitions for the Outbox module.
///     ActivitySource and Meter are thread-safe singletons by design.
///     Names derived from nameof() — no hardcoded magic strings.
/// </summary>
internal static class OutboxTelemetry
{
    private const string Prefix = "ModularMonolith";

    /// <summary>
    ///     ActivitySource name: "ModularMonolith.Outbox"
    /// </summary>
    public const string ActivitySourceName = Prefix + "." + nameof(Outbox);

    /// <summary>
    ///     Meter name: "ModularMonolith.Outbox"
    /// </summary>
    public const string MeterName = Prefix + "." + nameof(Outbox);

    // ── Tracing ──────────────────────────────────────────────────────
    public static readonly ActivitySource ActivitySource = new(ActivitySourceName);

    // ── Metrics ──────────────────────────────────────────────────────
    public static readonly Meter Meter = new(MeterName);

    // ── Counters ─────────────────────────────────────────────────────
    public static readonly Counter<long> MessagesProcessed =
        Meter.CreateCounter<long>("outbox.messages_processed.total", description: "Total outbox messages processed");

    public static readonly Counter<long> MessagesPublished =
        Meter.CreateCounter<long>("outbox.messages_published.total", description: "Total outbox messages published to Kafka");

    public static readonly Counter<long> MessagesFailed =
        Meter.CreateCounter<long>("outbox.messages_failed.total", description: "Total outbox messages that failed processing");

    // ── Histograms ───────────────────────────────────────────────────
    public static readonly Histogram<double> ProcessingDuration =
        Meter.CreateHistogram<double>("outbox.processing.duration", "ms", "Time to process an outbox message batch");
}
