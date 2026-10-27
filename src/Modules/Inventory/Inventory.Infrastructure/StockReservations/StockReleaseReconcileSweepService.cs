using Common.Application.EventBus;
using Common.Application.Options;
using Common.Infrastructure.Persistence.Extensions;
using Common.IntegrationEvents;
using Inventory.Application.Persistence;
using Inventory.Domain.StockReservations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Inventory.Infrastructure.StockReservations;

/// <summary>
///     The recurring job body behind the release endpoint's unknown outcome: a release attempt marker that is older than
///     <see cref="InventoryOptions.StockReleaseReconcileMinAgeSeconds" /> on a reservation that is still active and writable
///     means the attempt ended without recording what the warehouse did (a timeout, a crash between the call and the save). For
///     each such reservation one <see cref="StockReleaseReconcileRequestedIntegrationEvent" /> is published,
///     <see cref="InventoryOptions.SweepPageSize" /> reservations at a time. It never calls a warehouse and never touches a
///     reservation: the queue consumer reconciles. A read-only reservation is left to the person who resolves it. A marker
///     that carries no time (set before the time was kept) counts as old.
/// </summary>
public sealed partial class StockReleaseReconcileSweepService(
    IInventoryDbContext dbContext,
    IIntegrationEventOutbox outbox,
    TimeProvider timeProvider,
    IOptions<InventoryOptions> options,
    ILogger<StockReleaseReconcileSweepService> logger)
{
    public async Task SweepAsync(CancellationToken cancellationToken = default)
    {
        var cutoff = timeProvider.GetUtcNow().AddSeconds(-options.Value.StockReleaseReconcileMinAgeSeconds);
        var pageSize = options.Value.SweepPageSize;
        var published = 0;

        // Publishing changes no reservation, so the set is stable while it is paged through.
        while (true)
        {
            var page = await dbContext.StockReservations
                .AsNoTracking()
                .TagWith(nameof(SweepAsync), published)
                .Where(r => r.Status == ReservationStatus.Active
                            && !r.IsReadOnly
                            && r.LastReleaseAttemptReference != null
                            && (r.LastReleaseAttemptOn == null || r.LastReleaseAttemptOn <= cutoff))
                .OrderBy(r => r.Id)
                .Skip(published)
                .Take(pageSize)
                .Select(r => r.Id)
                .ToListAsync(cancellationToken);

            foreach (var reservationId in page)
            {
                outbox.Collect(new StockReleaseReconcileRequestedIntegrationEvent(reservationId.Value));
            }

            if (page.Count > 0)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }

            published += page.Count;
            if (page.Count < pageSize)
            {
                break;
            }
        }

        LogSweepComplete(logger, published);
    }

    [LoggerMessage(Level = LogLevel.Information, Message = "Stock release reconcile sweep queued {ReservationCount} reservations with an unrecorded release attempt.")]
    private static partial void LogSweepComplete(ILogger logger, int reservationCount);
}
