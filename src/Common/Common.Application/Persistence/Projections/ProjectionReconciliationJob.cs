using Common.Application.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Common.Application.Persistence.Projections;

/// <summary>
///     Recurring job shape for re-reading a source of truth page by page and re-applying it to a projection, to heal
///     rows that missed an event. Each page is applied, saved and the change tracker cleared, so memory stays bounded
///     by <see cref="ProjectionReconciliationOptions.PageSize" />; <see cref="ProjectionReconciliationOptions.MaxPages" />
///     caps one run so a looping pager or a huge source cannot pin a worker. Register the concrete job with
///     <c>IRecurringBackgroundJobs</c> exactly like any other recurring job.
/// </summary>
public abstract partial class ProjectionReconciliationJob<TItem, TCursor>(
    IDbContext dbContext,
    IOptions<ProjectionReconciliationOptions> optionsProvider,
    ILogger logger)
    where TCursor : class
{
    /// <summary>Reads one page after <paramref name="cursor" /> (null: the first page). Null <c>Next</c> ends the run.</summary>
    protected abstract Task<(IReadOnlyList<TItem> Items, TCursor? Next)> FetchPageAsync(
        TCursor? cursor, int pageSize, CancellationToken cancellationToken);

    /// <summary>
    ///     Applies one page to the projection; the base saves after it returns. Returns how many rows it actually created or
    ///     changed: a healed row means the normal event flow missed it, so the base warns when the total is above zero.
    /// </summary>
    protected abstract Task<int> ApplyPageAsync(IReadOnlyList<TItem> items, CancellationToken cancellationToken);

    /// <summary>Runs one reconciliation and returns the number of source items read and applied (healed or not).</summary>
    public async Task<int> RunAsync(CancellationToken cancellationToken = default)
    {
        var options = optionsProvider.Value;
        var jobName = GetType().Name;
        var applied = 0;
        var healed = 0;
        var pages = 0;
        TCursor? cursor = null;

        LogStart(logger, jobName, options.PageSize, options.MaxPages);

        while (pages < options.MaxPages)
        {
            var (items, next) = await FetchPageAsync(cursor, options.PageSize, cancellationToken);
            pages++;

            if (items.Count > 0)
            {
                healed += await ApplyPageAsync(items, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                dbContext.ChangeTracker.Clear();
                applied += items.Count;
            }

            LogPage(logger, jobName, pages, items.Count);

            if (next is null || items.Count == 0)
            {
                LogFinished(logger, jobName, pages, applied, healed);
                return applied;
            }

            cursor = next;
        }

        LogMaxPagesReached(logger, jobName, options.MaxPages, applied);
        LogFinished(logger, jobName, pages, applied, healed);
        return applied;
    }

    private static void LogFinished(ILogger logger, string jobName, int pages, int applied, int healed)
    {
        if (healed > 0)
        {
            LogHealed(logger, jobName, healed, applied);
            return;
        }

        LogComplete(logger, jobName, pages, applied);
    }

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Projection reconciliation {JobName} starting: page size {PageSize}, at most {MaxPages} pages.")]
    private static partial void LogStart(ILogger logger, string jobName, int pageSize, int maxPages);

    [LoggerMessage(Level = LogLevel.Debug,
        Message = "Projection reconciliation {JobName} applied page {Page} with {ItemCount} items.")]
    private static partial void LogPage(ILogger logger, string jobName, int page, int itemCount);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Projection reconciliation {JobName} complete after {Pages} pages, {AppliedCount} items applied.")]
    private static partial void LogComplete(ILogger logger, string jobName, int pages, int appliedCount);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "Projection reconciliation {JobName} healed {HealedCount} of {AppliedCount} items: the normal event flow missed them, look for a lost or failed event.")]
    private static partial void LogHealed(ILogger logger, string jobName, int healedCount, int appliedCount);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "Projection reconciliation {JobName} stopped at the {MaxPages} page cap with {AppliedCount} items applied; the rest is NOT reconciled, the cursor is not kept and the next run starts again from the first page.")]
    private static partial void LogMaxPagesReached(ILogger logger, string jobName, int maxPages, int appliedCount);
}
