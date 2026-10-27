using System.Globalization;
using Common.Application.EventBus;
using Common.Application.Jobs;
using Common.Application.Storage;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Persistence.Extensions;
using Common.IntegrationEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Products.Application.Imports;
using Products.Application.Persistence;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;

namespace Products.Infrastructure.Imports;

/// <summary>
///     The only way a product import starts: stores the uploaded workbook, then writes the
///     <see cref="ProductImportJob" /> row and the <see cref="ProductImportRequestedIntegrationEvent" /> queue message in
///     one transaction (outbox) and returns without reading a single row of the file. The request culture is recorded on the
///     job, because the consumer that reads the file has no request of its own.
/// </summary>
public sealed partial class ProductImportJobEnqueuer(
    IProductsDbContext dbContext,
    IPrivateObjectStore objectStore,
    IIntegrationEventOutbox outbox,
    TimeProvider timeProvider,
    ILogger<ProductImportJobEnqueuer> logger)
{
    public const string WorkbookContentType = "application/octet-stream";

    /// <summary>
    ///     Returns the job to poll: the first job of a repeated <paramref name="clientRequestId" />, else the user's
    ///     already active job (a double click with a fresh id, a second tab), else a new one. The unique indexes on
    ///     (user, client request id) and on the active statuses are the race-safe backstop for two requests that both
    ///     pass the lookup.
    /// </summary>
    public async Task<Result<ProductImportJobId>> EnqueueAsync(
        ApplicationUserId requestedBy, StoreId storeId, ProductTemplateId productTemplateId,
        string clientRequestId, string fileName, Stream content, CancellationToken cancellationToken)
    {
        if (await FindExistingAsync(requestedBy, clientRequestId, cancellationToken) is { } existing)
        {
            return existing;
        }

        var jobId = ProductImportJobId.New();
        var extension = string.Equals(Path.GetExtension(fileName), ".xls", StringComparison.OrdinalIgnoreCase) ? ".xls" : ".xlsx";
        var objectKey = $"products/imports/{jobId}{extension}";
        await objectStore.UploadAsync(new UploadObjectRequest(objectKey, content, WorkbookContentType), cancellationToken);

        var job = ProductImportJob.Queue(
            jobId, requestedBy, timeProvider.GetUtcNow(), storeId, productTemplateId, clientRequestId, fileName, objectKey,
            CultureInfo.CurrentUICulture.Name);
        dbContext.ImportJobs.Add(job);
        outbox.Collect(new ProductImportRequestedIntegrationEvent(jobId.Value));

        var saved = await dbContext.SaveChangesDetectingUniqueViolationAsync(ProductImportErrors.AlreadyQueued, cancellationToken);
        if (!saved.IsFailure)
        {
            return jobId;
        }

        // Lost the race on a unique index. The failed save dropped the collected event with the row, so only the entity
        // is detached and the file this attempt stored is removed.
        dbContext.Entry(job).State = EntityState.Detached;
        await DeleteUploadAsync(objectKey);

        var winner = await FindExistingAsync(requestedBy, clientRequestId, cancellationToken);
        return winner is { } winnerId ? Result<ProductImportJobId>.Success(winnerId) : ProductImportErrors.AlreadyQueued;
    }

    private Task<ProductImportJobId?> FindExistingAsync(
        ApplicationUserId requestedBy, string clientRequestId, CancellationToken cancellationToken)
    {
        // Not a lookup by id: the job of this client request if there is one, else the active one.
        return dbContext.ImportJobs
            .AsNoTracking()
            .TagWith(nameof(FindExistingAsync), requestedBy)
            .Where(j => j.RequestedBy == requestedBy
                && (j.ClientRequestId == clientRequestId || j.Status == JobStatus.Queued || j.Status == JobStatus.Running))
            .OrderByDescending(j => j.ClientRequestId == clientRequestId)
            .Select(j => (ProductImportJobId?)j.Id)
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

    [LoggerMessage(Level = LogLevel.Warning, Message = "Import upload {ObjectKey} could not be deleted; it stays unreferenced in the bucket.")]
    private static partial void LogUploadDeleteFailed(ILogger logger, Exception exception, string objectKey);
}
