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

    // ── Lag gauge (updated by Hangfire recurring job) ────────────────
    private static long _lagCount;

    public static void SetLagCount(long count) => Interlocked.Exchange(ref _lagCount, count);

    public static readonly ObservableGauge<long> OutboxLagCount =
        Meter.CreateObservableGauge<long>(
            "outbox.lag.count",
            ObserveLagCount,
            description: "Number of unprocessed outbox messages beyond lag threshold");

    private static Measurement<long> ObserveLagCount() => new(Interlocked.Read(ref _lagCount));

    // ── Counters ─────────────────────────────────────────────────────
    public static readonly Counter<long> MessagesProcessed =
        Meter.CreateCounter<long>("outbox.messages_processed.total", description: "Total outbox messages processed");

    public static readonly Counter<long> MessagesPublished =
        Meter.CreateCounter<long>("outbox.messages_published.total", description: "Total outbox messages published to Kafka");

    public static readonly Counter<long> MessagesFailed =
        Meter.CreateCounter<long>("outbox.messages_failed.total", description: "Total outbox messages that failed processing");

    public static readonly Counter<long> MessagesDlqProduced =
        Meter.CreateCounter<long>("outbox.messages_dlq_produced.total", description: "Total outbox messages sent to DLQ");

    public static readonly Counter<long> MessagesDlqFailed =
        Meter.CreateCounter<long>("outbox.messages_dlq_failed.total", description: "Total outbox messages that failed DLQ production");

    // ── Histograms ───────────────────────────────────────────────────
    public static readonly Histogram<double> ProcessingDuration =
        Meter.CreateHistogram<double>("outbox.processing.duration", "ms", "Time to process an outbox message batch");
}
