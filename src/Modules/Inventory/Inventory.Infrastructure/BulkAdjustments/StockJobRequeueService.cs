using Common.Application.EventBus;
using Common.Application.Jobs;
using Common.Application.Options;
using Common.Infrastructure.Persistence.Extensions;
using Common.IntegrationEvents;
using Inventory.Application.BulkAdjustments;
using Inventory.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Inventory.Infrastructure.BulkAdjustments;

/// <summary>
///     The recurring job body for jobs a consumer died on. A crash mid-message leaves the job Running with nobody working on
///     it, and its redelivered message may be dropped as a duplicate by the consumer inbox, so nothing would ever pick it up.
///     This finds every job Running for longer than <see cref="InventoryOptions.StockJobRunningLeaseMinutes" />, sets it back
///     to Queued and collects a fresh queue message for it in the same transaction, <see cref="InventoryOptions.SweepPageSize" />
///     jobs at a time. The consumer then resumes it from its stored cursor. A job already handed back
///     <see cref="InventoryOptions.StockJobMaxRequeues" /> times without saving a batch is failed instead
///     (<see cref="StockBulkAdjustErrors.JobAbandoned" />). It never runs a job.
///     <para>
///         Each change carries the lease test itself (<c>Status = Running AND StartedOn &lt; cutoff</c>), which the cutoff
///         overload of <c>RequeueAsync</c> in <see cref="JobClaimExtensions" /> applies in the same update: a job a consumer
///         claimed again a moment ago, or one another overlapping run of this sweep just requeued, is no longer stuck and
///         must not be requeued over the consumer working on it.
///     </para>
/// </summary>
public sealed partial class StockJobRequeueService(
    IInventoryDbContext dbContext,
    IIntegrationEventOutbox outbox,
    TimeProvider timeProvider,
    IOptions<InventoryOptions> options,
    ILogger<StockJobRequeueService> logger)
{
    public async Task SweepAsync(CancellationToken cancellationToken = default)
    {
        var now = timeProvider.GetUtcNow();
        var leaseExpiredBefore = now.AddMinutes(-options.Value.StockJobRunningLeaseMinutes);
        var pageSize = options.Value.SweepPageSize;
        var requeued = 0;
        var abandoned = 0;

        int handled;
        do
        {
            // A handled job is no longer Running, so the next read is the next page; a page nothing in changed state ends the sweep.
            var stuck = await dbContext.StockBulkAdjustJobs
                .AsNoTracking()
                .TagWith(nameof(SweepAsync), requeued + abandoned)
                .Where(j => j.Status == JobStatus.Running && j.StartedOn < leaseExpiredBefore)
                .OrderBy(j => j.StartedOn)
                .Take(pageSize)
                .Select(j => new { j.Id, j.RequeueCount })
                .ToListAsync(cancellationToken);

            handled = 0;
            foreach (var job in stuck)
            {
                if (job.RequeueCount >= options.Value.StockJobMaxRequeues)
                {
                    var failed = await AbandonAsync(job.Id, leaseExpiredBefore, now, cancellationToken);
                    abandoned += failed ? 1 : 0;
                    handled += failed ? 1 : 0;
                    continue;
                }

                var handedBack = await RequeueAsync(job.Id, leaseExpiredBefore, cancellationToken);
                requeued += handedBack ? 1 : 0;
                handled += handedBack ? 1 : 0;
            }
        } while (handled > 0 && handled == pageSize);

        LogSweepComplete(logger, requeued, abandoned);
    }

    /// <summary>True when this call changed the job; false when it was no longer stuck.</summary>
    internal async Task<bool> RequeueAsync(StockBulkAdjustJobId jobId, DateTimeOffset leaseExpiredBefore, CancellationToken cancellationToken)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        var requeued = await dbContext.StockBulkAdjustJobs.RequeueAsync(
            jobId, leaseExpiredBefore, cancellationToken, s => s.SetProperty(j => j.RequeueCount, j => j.RequeueCount + 1));

        if (requeued)
        {
            outbox.Collect(new StockBulkAdjustRequestedIntegrationEvent(jobId.Value));
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        await transaction.CommitAsync(cancellationToken);
        return requeued;
    }

    internal async Task<bool> AbandonAsync(
        StockBulkAdjustJobId jobId, DateTimeOffset leaseExpiredBefore, DateTimeOffset now, CancellationToken cancellationToken)
    {
        var changed = await dbContext.StockBulkAdjustJobs
            .TagWith(nameof(AbandonAsync), jobId)
            .Where(j => j.Id == jobId && j.Status == JobStatus.Running && j.StartedOn < leaseExpiredBefore)
            .ExecuteUpdateAsync(s => s
                .SetProperty(j => j.Status, JobStatus.Failed)
                .SetProperty(j => j.FailureKey, StockBulkAdjustErrors.JobAbandoned)
                .SetProperty(j => j.FinishedOn, now), cancellationToken);

        if (changed == 1)
        {
            LogJobAbandoned(logger, jobId.Value);
        }

        return changed == 1;
    }

    [LoggerMessage(Level = LogLevel.Information, Message = "Stuck job sweep handed {RequeuedCount} bulk adjustment jobs back to the queue and failed {AbandonedCount}.")]
    private static partial void LogSweepComplete(ILogger logger, int requeuedCount, int abandonedCount);

    [LoggerMessage(Level = LogLevel.Warning, Message = "Bulk adjustment job {JobId} was stuck again after its last allowed requeue; it is failed as abandoned.")]
    private static partial void LogJobAbandoned(ILogger logger, Guid jobId);
}
