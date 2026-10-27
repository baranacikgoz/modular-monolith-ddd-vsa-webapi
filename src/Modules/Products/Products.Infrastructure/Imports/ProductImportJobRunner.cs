using System.Globalization;
using Common.Application.Auth;
using Common.Application.EventBus;
using Common.Application.Jobs;
using Common.Application.Options;
using Common.Application.Storage;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Common.IntegrationEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Products.Application.Imports;
using Products.Application.Persistence;
using Products.Domain.Products;
using Products.Domain.Products.Errors;
using Products.Domain.Stores;
using Products.Infrastructure.Persistence;

namespace Products.Infrastructure.Imports;

/// <summary>
///     Runs one message of a queued <see cref="ProductImportJob" />. Claims the job atomically (a redelivered message
///     finds it no longer Queued and does nothing), reads the stored workbook and saves it in batches of
///     <see cref="BulkImportOptions.SaveChunkSize" /> rows: each batch commits its products together with the job's
///     cursor in one transaction, then the change tracker is cleared. A message handles at most
///     <see cref="ProductsOptions.ImportBatchesPerMessage" /> batches; when rows remain the job goes back to Queued and
///     collects a fresh queue message for itself in the same transaction, so no message runs towards the broker's
///     consumer timeout whatever the file size.
///     Nothing here throws for a bad row or a failed file: the job row carries Failed and a failure key, otherwise the
///     bus would retry the whole file.
/// </summary>
public sealed partial class ProductImportJobRunner(
    IProductsDbContext dbContext,
    IPrivateObjectStore objectStore,
    IIntegrationEventOutbox outbox,
    IOptions<ProductsOptions> productsOptions,
    IOptions<BulkImportOptions> bulkImportOptions,
    IBackgroundUserContext backgroundUser,
    TimeProvider timeProvider,
    ILogger<ProductImportJobRunner> logger)
{
    public async Task RunAsync(ProductImportJobId jobId, CancellationToken cancellationToken)
    {
        if (!await dbContext.ImportJobs.TryClaimAsync(jobId, timeProvider.GetUtcNow(), cancellationToken))
        {
            LogNotClaimable(logger, jobId.Value);
            return;
        }

        try
        {
            var job = await dbContext.ImportJobs
                .AsNoTracking()
                .TagWith(nameof(RunAsync), jobId)
                .Where(j => j.Id == jobId)
                .SingleAsync(cancellationToken);

            // No HttpContext here: the products this run writes are attributed to the user who uploaded the workbook.
            backgroundUser.Set(job.RequestedBy);

            await RunUnderUploaderCultureAsync(job.UiCulture, () => RunMessageAsync(job, cancellationToken));
        }
        catch (OperationCanceledException)
        {
            // Shutdown mid-file. The consumer's inbox row may already be committed with the first batch, so a redelivery of
            // this message can be dropped as a duplicate: the job is handed back with a message of its own instead.
            dbContext.ChangeTracker.Clear();
            await ContinueAsync(jobId);
            throw;
        }
        catch (Exception ex)
        {
            LogJobCrashed(logger, jobId.Value, ex);
            dbContext.ChangeTracker.Clear();
            await FinishAsync(jobId, JobStatus.Failed, ProductImportErrors.UnexpectedError);
        }
    }

    /// <summary>
    ///     A queue consumer has no request, so the search language that <c>ApplySearchLanguageInterceptor</c> stamps on a new
    ///     product (it reads <see cref="CultureInfo.CurrentUICulture" />) would be the process default. The message runs under the
    ///     culture the uploader had when the job was queued instead, and the previous culture is put back afterwards.
    /// </summary>
    private static async Task RunUnderUploaderCultureAsync(string cultureName, Func<Task> work)
    {
        var previous = CultureInfo.CurrentUICulture;
        CultureInfo.CurrentUICulture = CultureOf(cultureName);

        try
        {
            await work();
        }
        finally
        {
            CultureInfo.CurrentUICulture = previous;
        }
    }

    private static CultureInfo CultureOf(string cultureName)
    {
        try
        {
            return CultureInfo.GetCultureInfo(cultureName);
        }
        catch (CultureNotFoundException)
        {
            // A name this process's culture data does not know: the invariant culture resolves to the default search config.
            return CultureInfo.InvariantCulture;
        }
    }

    private async Task RunMessageAsync(ProductImportJob job, CancellationToken cancellationToken)
    {
        using var workbook = await DownloadAsync(job.ObjectKey, cancellationToken);

        await ReadSlice(workbook, job).Match(
            slice => ProcessSliceAsync(job, slice, cancellationToken),
            error => FinishAsync(job.Id, JobStatus.Failed, error.Key));
    }

    private async Task ProcessSliceAsync(ProductImportJob job, Slice slice, CancellationToken cancellationToken)
    {
        if (job.TotalRows == 0)
        {
            await dbContext.ImportJobs
                .TagWith(nameof(ProcessSliceAsync), job.Id)
                .Where(j => j.Id == job.Id)
                .ExecuteUpdateAsync(s => s.SetProperty(j => j.TotalRows, slice.TotalRows), cancellationToken);
        }

        var storedErrors = job.RowErrors.ToList();
        var outcome = Result.Success;

        // A failed batch (the store is gone) skips the batches after it.
        foreach (var batch in slice.Rows.Chunk(bulkImportOptions.Value.SaveChunkSize))
        {
            outcome = await outcome.BindAsync(() => ImportBatchAsync(job, batch, storedErrors, cancellationToken));
        }

        await outcome.Match(
            () => slice.HasMore ? ContinueAsync(job.Id) : FinishAsync(job.Id, JobStatus.Succeeded, null),
            error => FinishAsync(job.Id, JobStatus.Failed, error.Key));
    }

    /// <summary>Rows after the job's cursor, at most one message's worth, plus whether more follow. The first message
    /// counts the whole file (stopping at the row cap) and refuses it when it is over the cap.</summary>
    private Result<Slice> ReadSlice(MemoryStream workbook, ProductImportJob job)
    {
        var limit = bulkImportOptions.Value.SaveChunkSize * productsOptions.Value.ImportBatchesPerMessage;

        try
        {
            var totalRows = job.TotalRows;
            if (totalRows == 0)
            {
                totalRows = WorkbookReader.ReadRows(workbook).Take(bulkImportOptions.Value.MaxRows + 1).Count();
                workbook.Position = 0;
            }

            if (totalRows > bulkImportOptions.Value.MaxRows)
            {
                return ProductImportErrors.TooManyRows;
            }

            var rows = WorkbookReader.ReadRows(workbook)
                .Where(row => row.RowNumber > job.LastProcessedRow)
                .Take(limit + 1)
                .ToList();

            return new Slice(rows.Take(limit).ToList(), rows.Count > limit, totalRows);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            LogFileUnreadable(logger, ex);
            return ProductImportErrors.FileUnreadable;
        }
    }

    private async Task<Result> ImportBatchAsync(
        ProductImportJob job, SheetRow[] batch, List<ProductImportRowError> storedErrors, CancellationToken cancellationToken)
    {
        var parsed = batch.Select(row => (row.RowNumber, Parsed: RowParser.Parse(row))).ToList();
        var names = parsed
            .Where(p => !p.Parsed.IsFailure)
            .Select(p => p.Parsed.Value!.Name)
            .Distinct()
            .ToList();

        var skus = parsed
            .Where(p => !p.Parsed.IsFailure && p.Parsed.Value!.Sku is not null)
            .Select(p => p.Parsed.Value!.Sku!)
            .Distinct()
            .ToList();

        // One bounded IN per batch, by name and by SKU. Names are not unique in the table, so every product of a name is
        // loaded and a row that matches several is rejected; a SKU is unique per store, so it names at most one product.
        var loaded = await dbContext.Products
            .TagWith(nameof(ImportBatchAsync), job.StoreId)
            .Where(p => p.StoreId == job.StoreId && (names.Contains(p.Name) || (p.Sku != null && skus.Contains(p.Sku))))
            .OrderBy(p => p.Id)
            .ToListAsync(cancellationToken);
        var byName = loaded
            .Where(p => names.Contains(p.Name))
            .GroupBy(p => p.Name, StringComparer.Ordinal)
            .ToDictionary(g => g.Key, g => g.ToList(), StringComparer.Ordinal);
        var bySku = loaded
            .Where(p => p.Sku is not null)
            .ToDictionary(p => p.Sku!, StringComparer.Ordinal);

        return await dbContext.Stores
            .TagWith(nameof(ImportBatchAsync), job.StoreId)
            .Where(s => s.Id == job.StoreId)
            .SingleAsResultAsync(nameof(Store), cancellationToken)
            .MapAsync(store => parsed
                .Select(p => p.Parsed.Match(
                    row => Apply(job, store, byName, bySku, row),
                    error => RowResult.Rejected(p.RowNumber, error)))
                .ToList())
            .BindAsync(results => SaveBatchAsync(job, batch, results, storedErrors, cancellationToken));
    }

    /// <summary>
    ///     A row with a SKU is matched by it first: the SKU names one product, which settles which of several same-named
    ///     products the row means. Without a SKU the name decides, and a name shared by several products is ambiguous, so the
    ///     row is rejected instead of updating one of them by chance.
    /// </summary>
    private static RowResult Apply(
        ProductImportJob job, Store store, Dictionary<string, List<Product>> byName, Dictionary<string, Product> bySku, ProductRow row)
    {
        if (row.Sku is not null && bySku.TryGetValue(row.Sku, out var holder))
        {
            return string.Equals(holder.Name, row.Name, StringComparison.Ordinal)
                ? Update(holder, row)
                : RowResult.Rejected(row.RowNumber, ProductErrors.SkuAlreadyExists);
        }

        return byName.GetValueOrDefault(row.Name) switch
        {
            { Count: > 1 } => RowResult.Rejected(row.RowNumber, ProductImportRowErrors.AmbiguousProductName),

            // A SKU is set when the product is created and never changes, so a row cannot name another one for it (the
            // branch above already took the product that holds the row's SKU).
            [var named] when row.Sku is not null && !string.Equals(named.Sku, row.Sku, StringComparison.Ordinal)
                => RowResult.Rejected(row.RowNumber, ProductImportRowErrors.SkuDiffers),
            [var named] => Update(named, row),

            _ => Create(job, store, byName, bySku, row)
        };
    }

    private static RowResult Update(Product product, ProductRow row)
    {
        // The aggregate raises nothing for a row that changes nothing, so the event count tells an update from a no-op.
        var eventsBefore = product.Events.Count;
        product.Update(null, row.Description, row.Quantity, row.Price);
        return product.Events.Count > eventsBefore ? RowResult.Updated : RowResult.Unchanged;
    }

    private static RowResult Create(
        ProductImportJob job, Store store, Dictionary<string, List<Product>> byName, Dictionary<string, Product> bySku, ProductRow row)
    {
        var created = Product.Create(job.StoreId, job.ProductTemplateId, row.Name, row.Description, row.Quantity, row.Price, row.Sku);

        // A store that is not Active takes no new product: the row is rejected with the store's status error.
        return store.AddProduct(created).Match(
            () =>
            {
                byName[row.Name] = [created];
                if (row.Sku is not null)
                {
                    bySku[row.Sku] = created;
                }

                return RowResult.Created;
            },
            error => RowResult.Rejected(row.RowNumber, error));
    }

    /// <summary>Saves the batch's products and the job's cursor in one transaction. A concurrent writer on the same
    /// store or product, or one that took a SKU of this batch first, fails only this batch: its rows are reported and the
    /// file goes on.</summary>
    private async Task<Result> SaveBatchAsync(
        ProductImportJob job, SheetRow[] batch, List<RowResult> results, List<ProductImportRowError> storedErrors,
        CancellationToken cancellationToken)
    {
        var summary = BatchSummary.From(results);

        try
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            await RecordBatchAsync(job.Id, batch, summary, storedErrors, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (DbUpdateException ex) when (ex is DbUpdateConcurrencyException || ex.IsUniqueViolation())
        {
            LogBatchConflict(logger, job.Id.Value, ex);
            dbContext.ChangeTracker.Clear();
            summary = BatchSummary.AllRejected(batch, ProductImportErrors.ConcurrentUpdate);
            await RecordBatchAsync(job.Id, batch, summary, storedErrors, cancellationToken);
        }

        storedErrors.AddRange(summary.Rejections.Take(Math.Max(0, productsOptions.Value.ImportMaxStoredRowErrors - storedErrors.Count)));
        dbContext.ChangeTracker.Clear();
        return Result.Success;
    }

    private Task<int> RecordBatchAsync(
        ProductImportJobId jobId, SheetRow[] batch, BatchSummary summary, List<ProductImportRowError> storedErrors,
        CancellationToken cancellationToken)
    {
        var lastRow = batch[^1].RowNumber;
        IReadOnlyList<ProductImportRowError> errors = storedErrors
            .Concat(summary.Rejections)
            .Take(productsOptions.Value.ImportMaxStoredRowErrors)
            .ToList();

        return dbContext.ImportJobs
            .TagWith(nameof(RecordBatchAsync), jobId)
            .Where(j => j.Id == jobId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(j => j.LastProcessedRow, lastRow)
                .SetProperty(j => j.Progress, j => j.Progress + batch.Length)
                .SetProperty(j => j.ProductsCreated, j => j.ProductsCreated + summary.Created)
                .SetProperty(j => j.ProductsUpdated, j => j.ProductsUpdated + summary.Updated)
                .SetProperty(j => j.RowsRejected, j => j.RowsRejected + summary.Rejections.Count)
                .SetProperty(j => j.RowErrors, errors), cancellationToken);
    }

    /// <summary>The file has more rows than this message took: the job goes back to Queued and the message that resumes
    /// it is collected in the same transaction, so a crash between the two can never leave a Queued job without one.</summary>
    private async Task ContinueAsync(ProductImportJobId jobId)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync(CancellationToken.None);

        if (await dbContext.ImportJobs.RequeueAsync(jobId, CancellationToken.None))
        {
            outbox.Collect(new ProductImportRequestedIntegrationEvent(jobId.Value));
            await dbContext.SaveChangesAsync(CancellationToken.None);
        }

        await transaction.CommitAsync(CancellationToken.None);
        LogJobContinues(logger, jobId.Value);
    }

    // CancellationToken.None: once the file is done (or refused), its outcome must land even if shutdown has begun.
    private async Task FinishAsync(ProductImportJobId jobId, JobStatus status, string? failureKey)
    {
        var now = timeProvider.GetUtcNow();
        await dbContext.ImportJobs
            .TagWith(nameof(FinishAsync), jobId)
            .Where(j => j.Id == jobId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(j => j.Status, status)
                .SetProperty(j => j.FailureKey, failureKey)
                .SetProperty(j => j.FinishedOn, now), CancellationToken.None);

        await DeleteUploadAsync(jobId);
    }

    /// <summary>The workbook is input, not a record; an empty <c>ObjectKey</c> says it is gone. A failed delete keeps the key,
    /// and housekeeping removes the object of a finished job later.</summary>
    private async Task DeleteUploadAsync(ProductImportJobId jobId)
    {
        try
        {
            var objectKey = await dbContext.ImportJobs
                .AsNoTracking()
                .TagWith(nameof(DeleteUploadAsync), jobId)
                .Where(j => j.Id == jobId)
                .Select(j => j.ObjectKey)
                .SingleAsync(CancellationToken.None);

            await objectStore.DeleteAsync(objectKey, CancellationToken.None);

            await dbContext.ImportJobs
                .TagWith(nameof(DeleteUploadAsync), jobId)
                .Where(j => j.Id == jobId)
                .ExecuteUpdateAsync(s => s.SetProperty(j => j.ObjectKey, string.Empty), CancellationToken.None);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            LogUploadDeleteFailed(logger, ex, jobId.Value);
        }
    }

    private async Task<MemoryStream> DownloadAsync(string objectKey, CancellationToken cancellationToken)
    {
        // ExcelDataReader needs a seekable stream; the file is capped by BulkImportOptions.MaxFileBytes at upload.
        var buffer = new MemoryStream();
        await using var source = await objectStore.DownloadAsync(objectKey, cancellationToken);
        await source.CopyToAsync(buffer, cancellationToken);
        buffer.Position = 0;
        return buffer;
    }

    private sealed record Slice(IReadOnlyList<SheetRow> Rows, bool HasMore, int TotalRows);

    private enum RowOutcome
    {
        Created,
        Updated,
        Unchanged,
        Rejected
    }

    private sealed record RowResult(RowOutcome Outcome, ProductImportRowError? Rejection = null)
    {
        public static RowResult Created { get; } = new(RowOutcome.Created);
        public static RowResult Updated { get; } = new(RowOutcome.Updated);
        public static RowResult Unchanged { get; } = new(RowOutcome.Unchanged);

        public static RowResult Rejected(int rowNumber, Error error)
        {
            return new RowResult(RowOutcome.Rejected, new ProductImportRowError(rowNumber, error.Key));
        }
    }

    private sealed record BatchSummary(int Created, int Updated, IReadOnlyList<ProductImportRowError> Rejections)
    {
        public static BatchSummary From(IReadOnlyCollection<RowResult> results)
        {
            return new BatchSummary(
                results.Count(r => r.Outcome == RowOutcome.Created),
                results.Count(r => r.Outcome == RowOutcome.Updated),
                results.Where(r => r.Rejection is not null).Select(r => r.Rejection!).ToList());
        }

        public static BatchSummary AllRejected(IEnumerable<SheetRow> batch, Error error)
        {
            return new BatchSummary(0, 0, batch.Select(row => new ProductImportRowError(row.RowNumber, error.Key)).ToList());
        }
    }

    [LoggerMessage(Level = LogLevel.Debug, Message = "Product import job {JobId} is not Queued (already claimed, finished, or gone); nothing to do.")]
    private static partial void LogNotClaimable(ILogger logger, Guid jobId);

    [LoggerMessage(Level = LogLevel.Information, Message = "Product import job {JobId} continues in a later message.")]
    private static partial void LogJobContinues(ILogger logger, Guid jobId);

    [LoggerMessage(Level = LogLevel.Warning, Message = "Product import workbook could not be read.")]
    private static partial void LogFileUnreadable(ILogger logger, Exception exception);

    [LoggerMessage(Level = LogLevel.Warning, Message = "Product import job {JobId}: a batch lost to a concurrent writer; its rows are reported and the file goes on.")]
    private static partial void LogBatchConflict(ILogger logger, Guid jobId, Exception exception);

    [LoggerMessage(Level = LogLevel.Warning, Message = "Product import job {JobId}: the uploaded workbook could not be deleted; housekeeping removes it later.")]
    private static partial void LogUploadDeleteFailed(ILogger logger, Exception exception, Guid jobId);

    [LoggerMessage(Level = LogLevel.Error, Message = "Product import job {JobId} crashed outside the per-row failure handling.")]
    private static partial void LogJobCrashed(ILogger logger, Guid jobId, Exception exception);
}
