using Common.Application.Options;
using Common.Application.Persistence;
using Common.Domain.StronglyTypedIds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Common.Application.Jobs;

/// <summary>
///     Recurring housekeeping for one job table: Running rows older than
///     <see cref="JobHousekeepingOptions.StaleAfterMinutes" /> are failed with
///     <see cref="JobClaimExtensions.StaleFailureKey" /> (their worker died mid-run), and finished rows older than
///     <see cref="JobHousekeepingOptions.RetentionHours" /> are deleted in pages of
///     <see cref="JobHousekeepingOptions.PageSize" />. Subclass per job table and register it as a recurring job.
/// </summary>
public abstract partial class JobHousekeepingJob<TJob, TId>(
    IDbContext dbContext,
    IOptions<JobHousekeepingOptions> optionsProvider,
    TimeProvider timeProvider,
    ILogger logger)
    where TJob : JobRow<TId>
    where TId : IStronglyTypedId
{
    public async Task<(int StaleFailed, int Deleted)> RunAsync(CancellationToken cancellationToken = default)
    {
        var options = optionsProvider.Value;
        var now = timeProvider.GetUtcNow();
        var jobs = dbContext.Set<TJob>();

        var staleFailed = await jobs.MarkStaleFailedAsync<TJob, TId>(
            now.AddMinutes(-options.StaleAfterMinutes), JobClaimExtensions.StaleFailureKey, now, cancellationToken);

        var retentionCutoff = now.AddHours(-options.RetentionHours);
        var deleted = 0;
        int batch;
        do
        {
            var expiredIds = jobs
                .Where(j => (j.Status == JobStatus.Succeeded || j.Status == JobStatus.Failed) && j.FinishedOn < retentionCutoff)
                .OrderBy(j => j.FinishedOn)
                .Take(options.PageSize)
                .Select(j => j.Id);

            batch = await jobs
                .TagWith(nameof(JobHousekeepingJob<TJob, TId>))
                .Where(j => expiredIds.Contains(j.Id))
                .ExecuteDeleteAsync(cancellationToken);
            deleted += batch;
        } while (batch == options.PageSize);

        LogComplete(logger, typeof(TJob).Name, staleFailed, deleted);
        return (staleFailed, deleted);
    }

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Job housekeeping for {JobTable}: {StaleFailedCount} stale Running rows failed, {DeletedCount} finished rows deleted.")]
    private static partial void LogComplete(ILogger logger, string jobTable, int staleFailedCount, int deletedCount);
}
