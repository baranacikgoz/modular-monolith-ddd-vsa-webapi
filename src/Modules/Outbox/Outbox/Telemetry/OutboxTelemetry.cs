using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Outbox.Telemetry;

internal static class OutboxTelemetry
{
    private const string Prefix = "ModularMonolith";

    public const string ActivitySourceName = Prefix + "." + nameof(Outbox);
    public const string MeterName = Prefix + "." + nameof(Outbox);

    public static readonly ActivitySource ActivitySource = new(ActivitySourceName);
    public static readonly Meter Meter = new(MeterName);

    // ── Lag gauge (updated by OutboxMetricsJob) ──────────────────────
    private static long _lagCount;
    public static void SetLagCount(long count) => Interlocked.Exchange(ref _lagCount, count);

    public static readonly ObservableGauge<long> OutboxLagCount =
        Meter.CreateObservableGauge<long>(
            "outbox.lag.count",
            () => new Measurement<long>(Interlocked.Read(ref _lagCount)),
            description: "Number of unprocessed outbox messages beyond lag threshold");

    // ── Stuck gauge (FailedOn IS NOT NULL — permanent failures) ─────
    private static long _stuckCount;
    public static void SetStuckCount(long count) => Interlocked.Exchange(ref _stuckCount, count);

    public static readonly ObservableGauge<long> OutboxStuckCount =
        Meter.CreateObservableGauge<long>(
            "outbox.stuck.count",
            () => new Measurement<long>(Interlocked.Read(ref _stuckCount)),
            description: "Outbox messages permanently failed (FailedOn IS NOT NULL) — never self-heal, require inspection");

    // ── Counters ─────────────────────────────────────────────────────
    public static readonly Counter<long> MessagesPublished =
        Meter.CreateCounter<long>("outbox.messages_published.total",
            description: "Total outbox messages successfully published to RabbitMQ broker (publisher-confirmed)");

    public static readonly Counter<long> MessagesPermanentlyFailed =
        Meter.CreateCounter<long>("outbox.messages_permanently_failed.total",
            description: "Total outbox messages that exhausted retry attempts and were permanently failed");

    // ── Histograms ───────────────────────────────────────────────────
    public static readonly Histogram<double> ProcessingDuration =
        Meter.CreateHistogram<double>("outbox.processing.duration", "ms",
            "Time to process an outbox message batch");

    public static readonly Histogram<int> PollBatchSize =
        Meter.CreateHistogram<int>("outbox.poll_batch_size",
            description: "Number of outbox messages picked up per poll cycle");
}
