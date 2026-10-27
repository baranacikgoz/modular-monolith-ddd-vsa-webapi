using Common.Application.Auth;
using Common.Application.EventBus;
using Common.Application.Jobs;
using Common.Application.Options;
using Common.Application.Storage;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Common.IntegrationEvents;
using Inventory.Application.BulkAdjustments;
using Inventory.Application.Persistence;
using Inventory.Domain.StockLevels;
using Inventory.Infrastructure.Telemetry;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Inventory.Infrastructure.BulkAdjustments;

/// <summary>
///     Runs one message of a queued <see cref="StockBulkAdjustJob" />. Claims the job atomically (a redelivered message
///     finds it no longer Queued and does nothing), reads the stored workbook and applies it in batches of
///     <see cref="BulkImportOptions.SaveChunkSize" /> rows: every row is one <c>StockLevel.Adjust</c>, so it appends its own
///     ledger row and raises its own events, and each batch commits its stock levels together with the job's cursor in one
///     transaction, then the change tracker is cleared. A message handles at most
///     <see cref="InventoryOptions.BulkAdjustBatchesPerMessage" /> batches; when rows remain the job goes back to Queued and
///     collects a fresh queue message for itself in the same transaction, so no message runs towards the broker's
///     consumer timeout whatever the file size.
///     Nothing here throws for a bad row or a failed file: the job row carries Failed and a failure key, otherwise the
///     bus would retry the whole file.
/// </summary>
public sealed partial class StockBulkAdjustJobRunner(
    IInventoryDbContext dbContext,
    IPrivateObjectStore objectStore,
    IIntegrationEventOutbox outbox,
    IOptions<InventoryOptions> inventoryOptions,
    IOptions<BulkImportOptions> bulkImportOptions,
    IBackgroundUserContext backgroundUser,
    TimeProvider timeProvider,
    ILogger<StockBulkAdjustJobRunner> logger)
{
    public async Task RunAsync(StockBulkAdjustJobId jobId, CancellationToken cancellationToken)
    {
        if (!await dbContext.StockBulkAdjustJobs.TryClaimAsync(jobId, timeProvider.GetUtcNow(), cancellationToken))
        {
            LogNotClaimable(logger, jobId.Value);
            return;
        }

        try
        {
            var job = await dbContext.StockBulkAdjustJobs
                .AsNoTracking()
                .TagWith(nameof(RunAsync), jobId)
                .Where(j => j.Id == jobId)
                .SingleAsync(cancellationToken);

            // No HttpContext here: the ledger rows this run writes are attributed to the user who uploaded the workbook.
            backgroundUser.Set(job.RequestedBy);

            await RunMessageAsync(job, cancellationToken);
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
            await FinishAsync(jobId, JobStatus.Failed, StockBulkAdjustErrors.UnexpectedError);
        }
    }

    private async Task RunMessageAsync(StockBulkAdjustJob job, CancellationToken cancellationToken)
    {
        using var workbook = await DownloadAsync(job.ObjectKey, cancellationToken);

        await ReadSlice(workbook, job).Match(
            slice => ProcessSliceAsync(job, slice, cancellationToken),
            error => FinishAsync(job.Id, JobStatus.Failed, error.Key));
    }

    private async Task ProcessSliceAsync(StockBulkAdjustJob job, Slice slice, CancellationToken cancellationToken)
    {
        if (job.TotalRows == 0)
        {
            await dbContext.StockBulkAdjustJobs
                .TagWith(nameof(ProcessSliceAsync), job.Id)
                .Where(j => j.Id == job.Id)
                .ExecuteUpdateAsync(s => s.SetProperty(j => j.TotalRows, slice.TotalRows), cancellationToken);
        }

        var storedErrors = job.RowErrors.ToList();
        var outcome = Result.Success;

        // A failed batch (the save itself broke) skips the batches after it.
        foreach (var batch in slice.Rows.Chunk(bulkImportOptions.Value.SaveChunkSize))
        {
            outcome = await outcome.BindAsync(() => AdjustBatchAsync(job, batch, storedErrors, cancellationToken));
        }

        await outcome.Match(
            () => slice.HasMore ? ContinueAsync(job.Id) : FinishAsync(job.Id, JobStatus.Succeeded, null),
            error => FinishAsync(job.Id, JobStatus.Failed, error.Key));
    }

    /// <summary>Rows after the job's cursor, at most one message's worth, plus whether more follow. The first message
    /// counts the whole file (stopping at the row cap) and refuses it when it is over the cap.</summary>
    private Result<Slice> ReadSlice(MemoryStream workbook, StockBulkAdjustJob job)
    {
        var limit = bulkImportOptions.Value.SaveChunkSize * inventoryOptions.Value.BulkAdjustBatchesPerMessage;

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
                return StockBulkAdjustErrors.TooManyRows;
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
            return StockBulkAdjustErrors.FileUnreadable;
        }
    }

    private async Task<Result> AdjustBatchAsync(
        StockBulkAdjustJob job, SheetRow[] batch, List<StockBulkAdjustRowError> storedErrors, CancellationToken cancellationToken)
    {
        var parsed = batch.Select(row => (row.RowNumber, Parsed: RowParser.Parse(row))).ToList();
        var productIds = parsed
            .Where(p => !p.Parsed.IsFailure)
            .Select(p => p.Parsed.Value!.ProductId)
            .Distinct()
            .ToList();

        // One bounded IN per batch, tracked: the aggregates change in memory and are saved with the batch.
        var stockLevels = await dbContext.StockLevels
            .TagWith(nameof(AdjustBatchAsync), job.Id)
            .Where(s => productIds.Contains(s.ProductId))
            .ToDictionaryAsync(s => s.ProductId, cancellationToken);

        var results = parsed
            .Select(p => p.Parsed.Match(
                row => Apply(job, stockLevels, row),
                error => RowResult.Rejected(p.RowNumber, error)))
            .ToList();

        return await SaveBatchAsync(job, batch, results, storedErrors, cancellationToken);
    }

    private static RowResult Apply(StockBulkAdjustJob job, Dictionary<DefaultIdType, StockLevel> stockLevels, AdjustmentRow row)
    {
        return stockLevels.TryGetValue(row.ProductId, out var stockLevel)
            ? stockLevel.Adjust(row.Quantity, job.Reason).Match(() => RowResult.Adjusted, error => RowResult.Rejected(row.RowNumber, error))
            : RowResult.Rejected(row.RowNumber, StockBulkAdjustRowErrors.UnknownProduct);
    }

    /// <summary>Saves the batch's stock levels (and the ledger rows and events they raised) and the job's cursor in one
    /// transaction. A concurrent writer on one of the stock levels fails only this batch: its rows are reported and the
    /// file goes on.</summary>
    private async Task<Result> SaveBatchAsync(
        StockBulkAdjustJob job, SheetRow[] batch, List<RowResult> results, List<StockBulkAdjustRowError> storedErrors,
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
        catch (DbUpdateConcurrencyException ex)
        {
            LogBatchConflict(logger, job.Id.Value, ex);
            dbContext.ChangeTracker.Clear();
            summary = BatchSummary.AllRejected(batch, StockBulkAdjustErrors.ConcurrentUpdate);
            await RecordBatchAsync(job.Id, batch, summary, storedErrors, cancellationToken);
        }

        InventoryTelemetry.StockMovementsRecorded.Add(
            summary.Adjusted, new KeyValuePair<string, object?>("type", nameof(StockMovementType.Adjusted)));
        storedErrors.AddRange(summary.Rejections.Take(Math.Max(0, inventoryOptions.Value.BulkAdjustMaxStoredRowErrors - storedErrors.Count)));
        dbContext.ChangeTracker.Clear();
        return Result.Success;
    }

    private Task<int> RecordBatchAsync(
        StockBulkAdjustJobId jobId, SheetRow[] batch, BatchSummary summary, List<StockBulkAdjustRowError> storedErrors,
        CancellationToken cancellationToken)
    {
        var lastRow = batch[^1].RowNumber;
        IReadOnlyList<StockBulkAdjustRowError> errors = storedErrors
            .Concat(summary.Rejections)
            .Take(inventoryOptions.Value.BulkAdjustMaxStoredRowErrors)
            .ToList();

        return dbContext.StockBulkAdjustJobs
            .TagWith(nameof(RecordBatchAsync), jobId)
            .Where(j => j.Id == jobId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(j => j.LastProcessedRow, lastRow)
                .SetProperty(j => j.RequeueCount, 0)
                .SetProperty(j => j.Progress, j => j.Progress + batch.Length)
                .SetProperty(j => j.RowsAdjusted, j => j.RowsAdjusted + summary.Adjusted)
                .SetProperty(j => j.RowsRejected, j => j.RowsRejected + summary.Rejections.Count)
                .SetProperty(j => j.RowErrors, errors), cancellationToken);
    }

    /// <summary>The file has more rows than this message took: the job goes back to Queued and the message that resumes
    /// it is collected in the same transaction, so a crash between the two can never leave a Queued job without one.</summary>
    private async Task ContinueAsync(StockBulkAdjustJobId jobId)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync(CancellationToken.None);

        if (await dbContext.StockBulkAdjustJobs.RequeueAsync(jobId, CancellationToken.None))
        {
            outbox.Collect(new StockBulkAdjustRequestedIntegrationEvent(jobId.Value));
            await dbContext.SaveChangesAsync(CancellationToken.None);
        }

        await transaction.CommitAsync(CancellationToken.None);
        LogJobContinues(logger, jobId.Value);
    }

    // CancellationToken.None: once the file is done (or refused), its outcome must land even if shutdown has begun.
    private async Task FinishAsync(StockBulkAdjustJobId jobId, JobStatus status, string? failureKey)
    {
        var now = timeProvider.GetUtcNow();
        await dbContext.StockBulkAdjustJobs
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
    private async Task DeleteUploadAsync(StockBulkAdjustJobId jobId)
    {
        try
        {
            var objectKey = await dbContext.StockBulkAdjustJobs
                .AsNoTracking()
                .TagWith(nameof(DeleteUploadAsync), jobId)
                .Where(j => j.Id == jobId)
                .Select(j => j.ObjectKey)
                .SingleAsync(CancellationToken.None);

            await objectStore.DeleteAsync(objectKey, CancellationToken.None);

            await dbContext.StockBulkAdjustJobs
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
        Adjusted,
        Rejected
    }

    private sealed record RowResult(RowOutcome Outcome, StockBulkAdjustRowError? Rejection = null)
    {
        public static RowResult Adjusted { get; } = new(RowOutcome.Adjusted);

        public static RowResult Rejected(int rowNumber, Error error)
        {
            return new RowResult(RowOutcome.Rejected, new StockBulkAdjustRowError(rowNumber, error.Key));
        }
    }

    private sealed record BatchSummary(int Adjusted, IReadOnlyList<StockBulkAdjustRowError> Rejections)
    {
        public static BatchSummary From(IReadOnlyCollection<RowResult> results)
        {
            return new BatchSummary(
                results.Count(r => r.Outcome == RowOutcome.Adjusted),
                results.Where(r => r.Rejection is not null).Select(r => r.Rejection!).ToList());
        }

        public static BatchSummary AllRejected(IEnumerable<SheetRow> batch, Error error)
        {
            return new BatchSummary(0, batch.Select(row => new StockBulkAdjustRowError(row.RowNumber, error.Key)).ToList());
        }
    }

    [LoggerMessage(Level = LogLevel.Debug, Message = "Bulk adjustment job {JobId} is not Queued (already claimed, finished, or gone); nothing to do.")]
    private static partial void LogNotClaimable(ILogger logger, Guid jobId);

    [LoggerMessage(Level = LogLevel.Information, Message = "Bulk adjustment job {JobId} continues in a later message.")]
    private static partial void LogJobContinues(ILogger logger, Guid jobId);

    [LoggerMessage(Level = LogLevel.Warning, Message = "Bulk adjustment workbook could not be read.")]
    private static partial void LogFileUnreadable(ILogger logger, Exception exception);

    [LoggerMessage(Level = LogLevel.Warning, Message = "Bulk adjustment job {JobId}: a batch lost to a concurrent writer; its rows are reported and the file goes on.")]
    private static partial void LogBatchConflict(ILogger logger, Guid jobId, Exception exception);

    [LoggerMessage(Level = LogLevel.Warning, Message = "Bulk adjustment job {JobId}: the uploaded workbook could not be deleted; housekeeping removes it later.")]
    private static partial void LogUploadDeleteFailed(ILogger logger, Exception exception, Guid jobId);

    [LoggerMessage(Level = LogLevel.Error, Message = "Bulk adjustment job {JobId} crashed outside the per-row failure handling.")]
    private static partial void LogJobCrashed(ILogger logger, Guid jobId, Exception exception);
}
