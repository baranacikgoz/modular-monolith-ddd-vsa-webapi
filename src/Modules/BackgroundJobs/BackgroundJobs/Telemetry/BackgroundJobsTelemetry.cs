using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace BackgroundJobs.Telemetry;

internal static class BackgroundJobsTelemetry
{
    private const string Prefix = "ModularMonolith";

    public const string ActivitySourceName = Prefix + "." + nameof(BackgroundJobs);
    public const string MeterName = Prefix + "." + nameof(BackgroundJobs);

    public static readonly ActivitySource ActivitySource = new(ActivitySourceName);
    public static readonly Meter Meter = new(MeterName);

    public static readonly Counter<long> JobsExecuted =
        Meter.CreateCounter<long>("backgroundjobs.executions.total",
            description: "Total Hangfire job executions, tagged by job name and outcome — a recurring job that stops appearing or flips to failure is broken silently");

    public static readonly Histogram<double> JobDuration =
        Meter.CreateHistogram<double>("backgroundjobs.execution.duration", "ms",
            "Duration of a Hangfire job execution, tagged by job name");

    public static void RecordJobExecuted(string jobName, bool succeeded, double durationMs)
    {
        JobsExecuted.Add(1,
            new KeyValuePair<string, object?>("job.name", jobName),
            new KeyValuePair<string, object?>("job.outcome", succeeded ? "success" : "failure"));
        JobDuration.Record(durationMs, new KeyValuePair<string, object?>("job.name", jobName));
    }
}
