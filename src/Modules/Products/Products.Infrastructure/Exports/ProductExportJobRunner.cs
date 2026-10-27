using System.Globalization;
using System.Text;
using Common.Application.Auth;
using Common.Application.Jobs;
using Common.Application.Options;
using Common.Application.Pagination;
using Common.Application.Storage;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Products.Application.Exports;
using Products.Application.Persistence;
using Products.Domain.Products;
using Products.Domain.Stores;

namespace Products.Infrastructure.Exports;

/// <summary>
///     Runs one queued <see cref="ProductExportJob" /> to a terminal status: the CSV of every product the job's filter
///     matches, built one keyset page at a time (no tracking, memory bounded by <see cref="ProductsOptions.ExportMaxRows" />),
///     uploaded to the private object store, its key recorded on the job. Never throws for a failed export: an exception
///     escaping the consumer would make the bus retry the whole scan, so the job row carries the failure key instead.
/// </summary>
public sealed partial class ProductExportJobRunner(
    IProductsDbContext dbContext,
    IPrivateObjectStore objectStore,
    IOptions<ProductsOptions> options,
    IBackgroundUserContext backgroundUser,
    TimeProvider timeProvider,
    ILogger<ProductExportJobRunner> logger)
{
    // Carries every column the product import reads (name, description, quantity, price, SKU) next to the export's own.
    private static readonly string[] Header = ["Id", "StoreId", "Name", "Description", "Quantity", "Price", "Sku", "CreatedOn"];

    public async Task RunAsync(ProductExportJobId jobId, CancellationToken cancellationToken)
    {
        if (!await dbContext.ExportJobs.TryClaimAsync(jobId, timeProvider.GetUtcNow(), cancellationToken))
        {
            LogNotClaimable(logger, jobId.Value);
            return;
        }

        try
        {
            var job = await dbContext.ExportJobs
                .AsNoTracking()
                .TagWith(nameof(RunAsync), jobId)
                .Where(j => j.Id == jobId)
                .SingleAsync(cancellationToken);

            // No HttpContext here: the file this run stores is attributed to the user who asked for the export.
            backgroundUser.Set(job.RequestedBy);

            await BuildAsync(job, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            // Shutdown mid-export: nothing is written until the end, so handing the job back for a rerun from page one is safe.
            await dbContext.ExportJobs.RequeueAsync(jobId, CancellationToken.None);
            throw;
        }
        catch (Exception ex)
        {
            LogJobCrashed(logger, jobId.Value, ex);
            await FailAsync(jobId, ProductExportErrors.UnexpectedError);
        }
    }

    private async Task BuildAsync(ProductExportJob job, CancellationToken cancellationToken)
    {
        var storeId = job.StoreId.GetValueOrDefault();

        // CreatedOn <= QueuedOn freezes the set against products added while the export pages through it.
        var query = dbContext.Products
            .AsNoTracking()
            .TagWith(nameof(BuildAsync), job.Id)
            .Where(p => p.CreatedOn <= job.QueuedOn)
            .WhereIf(p => p.StoreId == storeId, job.StoreId is not null);

        var total = await query.CountAsync(cancellationToken);
        if (total > options.Value.ExportMaxRows)
        {
            LogRowLimitExceeded(logger, job.Id.Value, total, options.Value.ExportMaxRows);
            await FailAsync(job.Id, ProductExportErrors.RowLimitExceeded.Key);
            return;
        }

        var csv = new StringBuilder();
        CsvWriter.AppendRow(csv, Header);
        var written = 0;
        string? cursor = null;

        // Keyset pages without OFFSET or a per-page count; a page that is not full carries no cursor, which ends the loop.
        do
        {
            var request = new ExportPageRequest { PageNumber = 1, PageSize = options.Value.ExportPageSize, After = cursor, IncludeTotal = false };
            var page = (await query.PaginateAsync(
                    p => new ExportRow(p.Id, p.StoreId, p.Name, p.Description, p.Quantity, p.Price, p.Sku, p.CreatedOn),
                    request,
                    orderBy: p => p.CreatedOn,
                    tiebreaker: p => p.Id,
                    cancellationToken: cancellationToken))
                .Match(rows => rows, error => throw new InvalidOperationException($"Export keyset page failed: {error.Key}."));

            foreach (var row in page.Data)
            {
                CsvWriter.AppendRow(csv, ToFields(row));
            }

            written += page.Data.Count;
            await ReportProgressAsync(job.Id, written, cancellationToken);
            cursor = page.NextCursor;
        } while (cursor is not null);

        var fileName = $"products-{timeProvider.GetUtcNow():yyyyMMddHHmmss}.csv";
        var objectKey = $"products/exports/{job.Id}.csv";

        // With a byte order mark: Excel on Windows reads a BOM-less UTF-8 file as ANSI and breaks accented characters.
        byte[] content = [.. Encoding.UTF8.GetPreamble(), .. Encoding.UTF8.GetBytes(csv.ToString())];
        using var upload = new MemoryStream(content, writable: false);
        await objectStore.UploadAsync(new UploadObjectRequest(objectKey, upload, ProductExportJob.CsvContentType), cancellationToken);

        await FinishSucceededAsync(job.Id, written, fileName, objectKey);
    }

    private sealed record ExportPageRequest : PaginationRequest;

    private sealed record ExportRow(ProductId Id, StoreId StoreId, string Name, string Description, int Quantity, decimal Price, string? Sku, DateTimeOffset CreatedOn);

    private static string?[] ToFields(ExportRow row)
    {
        return
        [
            row.Id.ToString(),
            row.StoreId.ToString(),
            row.Name,
            row.Description,
            row.Quantity.ToString(CultureInfo.InvariantCulture),
            row.Price.ToString(CultureInfo.InvariantCulture),
            row.Sku,
            CsvWriter.FormatDate(row.CreatedOn)
        ];
    }

    private Task<int> ReportProgressAsync(ProductExportJobId jobId, int written, CancellationToken cancellationToken)
    {
        return dbContext.ExportJobs
            .TagWith(nameof(ReportProgressAsync), jobId)
            .Where(j => j.Id == jobId)
            .ExecuteUpdateAsync(s => s.SetProperty(j => j.Progress, written), cancellationToken);
    }

    // CancellationToken.None: once the export itself is over, its outcome must land even if shutdown has begun.
    private Task<int> FinishSucceededAsync(ProductExportJobId jobId, int written, string fileName, string objectKey)
    {
        var now = timeProvider.GetUtcNow();
        return dbContext.ExportJobs
            .TagWith(nameof(FinishSucceededAsync), jobId)
            .Where(j => j.Id == jobId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(j => j.Status, JobStatus.Succeeded)
                .SetProperty(j => j.Progress, written)
                .SetProperty(j => j.FileName, fileName)
                .SetProperty(j => j.ObjectKey, objectKey)
                .SetProperty(j => j.FinishedOn, now), CancellationToken.None);
    }

    private Task<int> FailAsync(ProductExportJobId jobId, string failureKey)
    {
        var now = timeProvider.GetUtcNow();
        return dbContext.ExportJobs
            .TagWith(nameof(FailAsync), jobId)
            .Where(j => j.Id == jobId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(j => j.Status, JobStatus.Failed)
                .SetProperty(j => j.FailureKey, failureKey)
                .SetProperty(j => j.FinishedOn, now), CancellationToken.None);
    }

    [LoggerMessage(Level = LogLevel.Debug, Message = "Product export job {JobId} is not Queued (already claimed, finished, or gone); nothing to do.")]
    private static partial void LogNotClaimable(ILogger logger, Guid jobId);

    [LoggerMessage(Level = LogLevel.Warning, Message = "Product export job {JobId} matches {Total} products, above ExportMaxRows {MaxRows}; failing with RowLimitExceeded.")]
    private static partial void LogRowLimitExceeded(ILogger logger, Guid jobId, int total, int maxRows);

    [LoggerMessage(Level = LogLevel.Error, Message = "Product export job {JobId} crashed.")]
    private static partial void LogJobCrashed(ILogger logger, Guid jobId, Exception exception);
}
