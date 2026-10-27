using Common.Application.Jobs;
using Common.Application.Options;
using Common.Application.Storage;
using Common.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Products.Application.Exports;
using Products.Application.Imports;
using Products.Application.Persistence;

namespace Products.Infrastructure.Jobs;

/// <summary>Fails import jobs whose worker died and deletes finished ones after the retention window.</summary>
public sealed class ProductImportJobHousekeeping(
    IProductsDbContext dbContext,
    IOptions<JobHousekeepingOptions> options,
    TimeProvider timeProvider,
    ILogger<ProductImportJobHousekeeping> logger)
    : JobHousekeepingJob<ProductImportJob, ProductImportJobId>(dbContext, options, timeProvider, logger);

/// <summary>Fails export jobs whose worker died and deletes finished ones after the retention window.</summary>
public sealed class ProductExportJobHousekeeping(
    IProductsDbContext dbContext,
    IOptions<JobHousekeepingOptions> options,
    TimeProvider timeProvider,
    ILogger<ProductExportJobHousekeeping> logger)
    : JobHousekeepingJob<ProductExportJob, ProductExportJobId>(dbContext, options, timeProvider, logger);

/// <summary>
///     The one recurring job behind both job tables. Before a finished row is deleted, the object it still points at (the
///     uploaded workbook of an import whose own cleanup failed, the finished file of an export) is deleted from the
///     private object store in pages of <see cref="JobHousekeepingOptions.PageSize" />, so deleting the row never strands
///     a file. Registered as a recurring job by <see cref="ProductJobsHousekeepingRegistrar" />.
/// </summary>
public sealed class ProductJobsHousekeeping(
    ProductImportJobHousekeeping importRows,
    ProductExportJobHousekeeping exportRows,
    IProductsDbContext dbContext,
    IPrivateObjectStore objectStore,
    IOptions<JobHousekeepingOptions> options,
    TimeProvider timeProvider)
{
    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        var retentionCutoff = timeProvider.GetUtcNow().AddHours(-options.Value.RetentionHours);

        await DeleteExpiredImportUploadsAsync(retentionCutoff, cancellationToken);
        await DeleteExpiredExportFilesAsync(retentionCutoff, cancellationToken);

        await importRows.RunAsync(cancellationToken);
        await exportRows.RunAsync(cancellationToken);
    }

    /// <summary>An import's key is never null; an empty key means the workbook is already gone.</summary>
    private async Task DeleteExpiredImportUploadsAsync(DateTimeOffset retentionCutoff, CancellationToken cancellationToken)
    {
        var pageSize = options.Value.PageSize;
        int count;
        do
        {
            var page = await dbContext.ImportJobs
                .AsNoTracking()
                .TagWith(nameof(DeleteExpiredImportUploadsAsync))
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
            await dbContext.ImportJobs
                .TagWith(nameof(DeleteExpiredImportUploadsAsync))
                .Where(j => ids.Contains(j.Id))
                .ExecuteUpdateAsync(s => s.SetProperty(j => j.ObjectKey, string.Empty), cancellationToken);
        } while (count == pageSize);
    }

    /// <summary>An export's key is null until it succeeds and again once its file is deleted.</summary>
    private async Task DeleteExpiredExportFilesAsync(DateTimeOffset retentionCutoff, CancellationToken cancellationToken)
    {
        var pageSize = options.Value.PageSize;
        int count;
        do
        {
            var page = await dbContext.ExportJobs
                .AsNoTracking()
                .TagWith(nameof(DeleteExpiredExportFilesAsync))
                .Where(j => (j.Status == JobStatus.Succeeded || j.Status == JobStatus.Failed)
                    && j.FinishedOn < retentionCutoff && j.ObjectKey != null)
                .OrderBy(j => j.FinishedOn)
                .Take(pageSize)
                .Select(j => new { j.Id, ObjectKey = j.ObjectKey! })
                .ToListAsync(cancellationToken);

            count = page.Count;
            if (count == 0)
            {
                break;
            }

            await objectStore.DeleteManyAsync(page.Select(p => p.ObjectKey).ToList(), cancellationToken);

            var ids = page.Select(p => p.Id).ToList();
            await dbContext.ExportJobs
                .TagWith(nameof(DeleteExpiredExportFilesAsync))
                .Where(j => ids.Contains(j.Id))
                .ExecuteUpdateAsync(s => s.SetProperty(j => j.ObjectKey, (string?)null), cancellationToken);
        } while (count == pageSize);
    }
}
