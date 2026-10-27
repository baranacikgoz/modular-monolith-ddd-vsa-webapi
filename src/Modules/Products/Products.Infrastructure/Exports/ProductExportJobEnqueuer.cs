using Common.Application.EventBus;
using Common.Application.Jobs;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Persistence.Extensions;
using Common.IntegrationEvents;
using Microsoft.EntityFrameworkCore;
using Products.Application.Exports;
using Products.Application.Persistence;
using Products.Domain.Stores;

namespace Products.Infrastructure.Exports;

/// <summary>
///     The only way a product export starts: writes the <see cref="ProductExportJob" /> row and the
///     <see cref="ProductExportRequestedIntegrationEvent" /> queue message in one transaction (outbox) and returns without
///     reading a single product.
/// </summary>
public sealed class ProductExportJobEnqueuer(
    IProductsDbContext dbContext,
    IIntegrationEventOutbox outbox,
    TimeProvider timeProvider)
{
    /// <summary>Returns the user's already active export when there is one (a double click, a second tab), so the caller
    /// always gets a job to poll and a second full scan is never queued. The unique index on the active statuses is the
    /// race-safe backstop for two requests that both pass the lookup.</summary>
    public async Task<Result<ProductExportJobId>> EnqueueAsync(
        ApplicationUserId requestedBy, StoreId? storeId, CancellationToken cancellationToken)
    {
        if (await FindActiveAsync(requestedBy, cancellationToken) is { } active)
        {
            return active;
        }

        var job = ProductExportJob.Queue(requestedBy, timeProvider.GetUtcNow(), storeId);
        dbContext.ExportJobs.Add(job);
        outbox.Collect(new ProductExportRequestedIntegrationEvent(job.Id.Value));

        var saved = await dbContext.SaveChangesDetectingUniqueViolationAsync(ProductExportErrors.AlreadyQueued, cancellationToken);
        if (!saved.IsFailure)
        {
            return job.Id;
        }

        // Lost the race on the active-job index. The failed save dropped the collected event with the row, so only the
        // entity is detached.
        dbContext.Entry(job).State = EntityState.Detached;

        var winner = await FindActiveAsync(requestedBy, cancellationToken);
        return winner is { } winnerId ? Result<ProductExportJobId>.Success(winnerId) : ProductExportErrors.AlreadyQueued;
    }

    private Task<ProductExportJobId?> FindActiveAsync(ApplicationUserId requestedBy, CancellationToken cancellationToken)
    {
        return dbContext.ExportJobs
            .AsNoTracking()
            .TagWith(nameof(FindActiveAsync), requestedBy)
            .Where(j => j.RequestedBy == requestedBy && (j.Status == JobStatus.Queued || j.Status == JobStatus.Running))
            .Select(j => (ProductExportJobId?)j.Id)
            .SingleOrDefaultAsync(cancellationToken);
    }
}
