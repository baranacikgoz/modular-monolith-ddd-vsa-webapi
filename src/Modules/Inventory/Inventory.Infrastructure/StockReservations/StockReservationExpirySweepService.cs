using Common.Application.Options;
using Inventory.Application.Persistence;
using Inventory.Domain.StockReservations;
using Inventory.Infrastructure.Telemetry;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Inventory.Infrastructure.StockReservations;

/// <summary>Expires stock reservations past their deadline. Each reservation is saved individually: one
/// poisoned row's failed save is logged and the row detached, instead of aborting the whole sweep or leaving
/// stale tracked state to contaminate the next row in the same batch.</summary>
public sealed partial class StockReservationExpirySweepService(
    IInventoryDbContext dbContext,
    TimeProvider timeProvider,
    IOptions<InventoryOptions> inventoryOptionsProvider,
    ILogger<StockReservationExpirySweepService> logger)
{
    public async Task SweepAsync(CancellationToken cancellationToken = default)
    {
        var utcNow = timeProvider.GetUtcNow();
        var batchSize = inventoryOptionsProvider.Value.StockReservationExpirySweepBatchSize;

        var expired = await dbContext.StockReservations
            .TagWith(nameof(StockReservationExpirySweepService))
            .Where(r => r.Status == ReservationStatus.Active && r.ReservationDeadline < utcNow)
            .Take(batchSize)
            .ToListAsync(cancellationToken);

        var expiredCount = 0;
        foreach (var reservation in expired)
        {
            try
            {
                reservation.Expire(utcNow);
                await dbContext.SaveChangesAsync(cancellationToken);
                expiredCount++;
            }
            catch (Exception ex)
            {
                LogExpireFailed(logger, reservation.Id.Value, ex);
                dbContext.Entry(reservation).State = EntityState.Detached;
            }
        }

        InventoryTelemetry.ReservationsExpired.Add(expiredCount);
        LogSweepComplete(logger, expired.Count, expiredCount);
    }

    [LoggerMessage(Level = LogLevel.Error,
        Message = "Failed to expire stock reservation {ReservationId}; detaching and continuing with the rest of the batch.")]
    private static partial void LogExpireFailed(ILogger logger, Guid reservationId, Exception exception);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Stock reservation expiry sweep examined {ExaminedCount} reservations, expired {ExpiredCount}.")]
    private static partial void LogSweepComplete(ILogger logger, int examinedCount, int expiredCount);
}
