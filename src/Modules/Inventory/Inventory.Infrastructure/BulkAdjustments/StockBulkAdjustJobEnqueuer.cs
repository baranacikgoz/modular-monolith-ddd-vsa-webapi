using Common.Application.EventBus;
using Common.Application.Jobs;
using Common.Application.Storage;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Persistence.Extensions;
using Common.IntegrationEvents;
using Inventory.Application.BulkAdjustments;
using Inventory.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Inventory.Infrastructure.BulkAdjustments;

/// <summary>
///     The only way a bulk stock adjustment starts: stores the uploaded workbook, then writes the
///     <see cref="StockBulkAdjustJob" /> row and the <see cref="StockBulkAdjustRequestedIntegrationEvent" /> queue message in
///     one transaction (outbox) and returns without reading a single row of the file.
/// </summary>
public sealed partial class StockBulkAdjustJobEnqueuer(
    IInventoryDbContext dbContext,
    IPrivateObjectStore objectStore,
    IIntegrationEventOutbox outbox,
    TimeProvider timeProvider,
    ILogger<StockBulkAdjustJobEnqueuer> logger)
{
    public const string WorkbookContentType = "application/octet-stream";

    /// <summary>
    ///     Returns the job to poll: the first job of a repeated <paramref name="clientRequestId" />, else the user's
    ///     already active job (a double click with a fresh id, a second tab), else a new one. The unique indexes on
    ///     (user, client request id) and on the active statuses are the race-safe backstop for two requests that both
    ///     pass the lookup.
    /// </summary>
    public async Task<Result<StockBulkAdjustJobId>> EnqueueAsync(
        ApplicationUserId requestedBy, string reason, string clientRequestId, string fileName, Stream content,
        CancellationToken cancellationToken)
    {
        if (await FindExistingAsync(requestedBy, clientRequestId, cancellationToken) is { } existing)
        {
            return existing;
        }

        var jobId = StockBulkAdjustJobId.New();
        var extension = string.Equals(Path.GetExtension(fileName), ".xls", StringComparison.OrdinalIgnoreCase) ? ".xls" : ".xlsx";
        var objectKey = $"inventory/bulk-adjustments/{jobId}{extension}";
        await objectStore.UploadAsync(new UploadObjectRequest(objectKey, content, WorkbookContentType), cancellationToken);

        var job = StockBulkAdjustJob.Queue(jobId, requestedBy, timeProvider.GetUtcNow(), clientRequestId, fileName, objectKey, reason);
        dbContext.StockBulkAdjustJobs.Add(job);
        outbox.Collect(new StockBulkAdjustRequestedIntegrationEvent(jobId.Value));

        var saved = await dbContext.SaveChangesDetectingUniqueViolationAsync(StockBulkAdjustErrors.AlreadyQueued, cancellationToken);
        if (!saved.IsFailure)
        {
            return jobId;
        }

        // Lost the race on a unique index. The failed save dropped the collected event with the row, so only the entity
        // is detached and the file this attempt stored is removed.
        dbContext.Entry(job).State = EntityState.Detached;
        await DeleteUploadAsync(objectKey);

        var winner = await FindExistingAsync(requestedBy, clientRequestId, cancellationToken);
        return winner is { } winnerId ? Result<StockBulkAdjustJobId>.Success(winnerId) : StockBulkAdjustErrors.AlreadyQueued;
    }

    private Task<StockBulkAdjustJobId?> FindExistingAsync(
        ApplicationUserId requestedBy, string clientRequestId, CancellationToken cancellationToken)
    {
        // Not a lookup by id: the job of this client request if there is one, else the active one.
        return dbContext.StockBulkAdjustJobs
            .AsNoTracking()
            .TagWith(nameof(FindExistingAsync), requestedBy)
            .Where(j => j.RequestedBy == requestedBy
                && (j.ClientRequestId == clientRequestId || j.Status == JobStatus.Queued || j.Status == JobStatus.Running))
            .OrderByDescending(j => j.ClientRequestId == clientRequestId)
            .Select(j => (StockBulkAdjustJobId?)j.Id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    private async Task DeleteUploadAsync(string objectKey)
    {
        try
        {
            await objectStore.DeleteAsync(objectKey, CancellationToken.None);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            LogUploadDeleteFailed(logger, ex, objectKey);
        }
    }

    [LoggerMessage(Level = LogLevel.Warning, Message = "Bulk adjustment upload {ObjectKey} could not be deleted; it stays unreferenced in the bucket.")]
    private static partial void LogUploadDeleteFailed(ILogger logger, Exception exception, string objectKey);
}
