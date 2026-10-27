using Common.Application.Jobs;
using Common.Application.Options;
using Common.Application.Storage;
using Inventory.Application.BulkAdjustments;
using Inventory.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Inventory.Infrastructure.BulkAdjustments;

/// <summary>Fails bulk adjustment jobs whose worker died and deletes finished ones after the retention window.</summary>
public sealed class StockBulkAdjustJobHousekeeping(
    IInventoryDbContext dbContext,
    IOptions<JobHousekeepingOptions> options,
    TimeProvider timeProvider,
    ILogger<StockBulkAdjustJobHousekeeping> logger)
    : JobHousekeepingJob<StockBulkAdjustJob, StockBulkAdjustJobId>(dbContext, options, timeProvider, logger);

/// <summary>
///     The recurring job behind the bulk adjustment table. Before a finished row is deleted, the workbook it still points at
///     (one whose own cleanup failed) is deleted from the private object store in pages of
///     <see cref="JobHousekeepingOptions.PageSize" />, so deleting the row never strands a file. Registered as a recurring
///     job by <see cref="StockJobsHousekeepingRegistrar" />.
/// </summary>
public sealed class StockJobsHousekeeping(
    StockBulkAdjustJobHousekeeping rows,
    IInventoryDbContext dbContext,
    IPrivateObjectStore objectStore,
    IOptions<JobHousekeepingOptions> options,
    TimeProvider timeProvider)
{
    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        await DeleteExpiredUploadsAsync(timeProvider.GetUtcNow().AddHours(-options.Value.RetentionHours), cancellationToken);

        await rows.RunAsync(cancellationToken);
    }

    /// <summary>An upload's key is never null; an empty key means the workbook is already gone.</summary>
    private async Task DeleteExpiredUploadsAsync(DateTimeOffset retentionCutoff, CancellationToken cancellationToken)
    {
        var pageSize = options.Value.PageSize;
        int count;
        do
        {
            var page = await dbContext.StockBulkAdjustJobs
                .AsNoTracking()
                .TagWith(nameof(DeleteExpiredUploadsAsync))
                .Where(j => (j.Status == JobStatus.Succeeded || j.Status == JobStatus.Failed)
                    && j.FinishedOn < retentionCutoff && j.ObjectKey != string.Empty)
                .OrderBy(j => j.FinishedOn)
                .Take(pageSize)
                .Select(j => new { j.Id, j.ObjectKey })
                .ToListAsync(cancellationToken);

            count = page.Count;
            if (count == 0)
            {
                break;
            }

            await objectStore.DeleteManyAsync(page.Select(p => p.ObjectKey).ToList(), cancellationToken);

            var ids = page.Select(p => p.Id).ToList();
            await dbContext.StockBulkAdjustJobs
                .TagWith(nameof(DeleteExpiredUploadsAsync))
                .Where(j => ids.Contains(j.Id))
                .ExecuteUpdateAsync(s => s.SetProperty(j => j.ObjectKey, string.Empty), cancellationToken);
        } while (count == pageSize);
    }
}
