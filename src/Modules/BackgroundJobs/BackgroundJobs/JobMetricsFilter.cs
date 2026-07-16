using System.Diagnostics;
using BackgroundJobs.Telemetry;
using Hangfire.Server;

namespace BackgroundJobs;

/// <summary>
///     Hangfire server filter recording execution count (by job name and outcome) and duration
///     for every job — the only signal that a recurring job (OTP sweep, cleanup, ...) is silently failing.
/// </summary>
internal sealed class JobMetricsFilter : IServerFilter
{
    private const string StopwatchKey = nameof(JobMetricsFilter) + ".Stopwatch";

    public void OnPerforming(PerformingContext context) =>
        context.Items[StopwatchKey] = Stopwatch.StartNew();

    public void OnPerformed(PerformedContext context)
    {
        var durationMs = context.Items.TryGetValue(StopwatchKey, out var value) && value is Stopwatch stopwatch
            ? stopwatch.Elapsed.TotalMilliseconds
            : 0d;

        BackgroundJobsTelemetry.RecordJobExecuted(
            context.BackgroundJob.Job.Type.Name,
            succeeded: context.Exception is null && !context.Canceled,
            durationMs);
    }
}
