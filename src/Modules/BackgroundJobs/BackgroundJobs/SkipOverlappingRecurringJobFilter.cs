using Hangfire.Server;
using Hangfire.Storage;
using Microsoft.Extensions.Logging;

namespace BackgroundJobs;

/// <summary>
///     Hangfire server filter: a recurring job never runs twice at once. The scheduler enqueues a new run at every cron
///     tick whether or not the previous run has finished, and every replica runs a Hangfire server, so a run that
///     outlasts its interval (or a tick two replicas both fire) would overlap itself. The run takes a distributed lock
///     named after the recurring job id without waiting; when the lock is taken the new run is cancelled (the next tick
///     tries again) instead of queueing behind it. Only jobs the recurring scheduler created carry the
///     <c>RecurringJobId</c> parameter, so fire-and-forget and continuation jobs are untouched.
/// </summary>
internal sealed partial class SkipOverlappingRecurringJobFilter(ILogger<SkipOverlappingRecurringJobFilter> logger) : IServerFilter
{
    /// <summary>Set on the context of a run this filter cancelled, so metrics can tell "skipped" from "failed".</summary>
    internal const string SkippedItemKey = nameof(SkipOverlappingRecurringJobFilter) + ".Skipped";

    private const string LockItemKey = nameof(SkipOverlappingRecurringJobFilter) + ".Lock";
    private const string RecurringJobIdParameter = "RecurringJobId";

    public void OnPerforming(PerformingContext context)
    {
        var recurringJobId = context.GetJobParameter<string>(RecurringJobIdParameter);
        if (string.IsNullOrEmpty(recurringJobId))
        {
            return;
        }

        try
        {
            context.Items[LockItemKey] = context.Connection.AcquireDistributedLock($"recurring-job:{recurringJobId}", TimeSpan.Zero);
        }
        catch (DistributedLockTimeoutException)
        {
            context.Items[SkippedItemKey] = true;
            context.Canceled = true;
            LogSkipped(logger, recurringJobId);
        }
    }

    public void OnPerformed(PerformedContext context)
    {
        if (context.Items.TryGetValue(LockItemKey, out var held) && held is IDisposable distributedLock)
        {
            distributedLock.Dispose();
        }
    }

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Recurring job {RecurringJobId} skipped: its previous run is still in progress.")]
    private static partial void LogSkipped(ILogger logger, string recurringJobId);
}
