using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using EntityFramework.Exceptions.Common;
using Inventory.Application.Persistence;
using Inventory.Domain.StockReservations;
using Inventory.Domain.WarehouseConnections;
using Inventory.Domain.WebhookDeliveries;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Webhooks;

/// <summary>What became of an authenticated delivery.</summary>
public enum WebhookDeliveryOutcome
{
    /// <summary>First time this delivery id was seen: its effect was applied and the delivery recorded, in one save.</summary>
    Applied,

    /// <summary>The delivery id was recorded before (or by a request that won the race): acknowledged, nothing applied.</summary>
    Duplicate,

    /// <summary>The connection is deactivated: acknowledged without reading the body. Decided by the endpoint, never by the intake.</summary>
    Ignored
}

/// <summary>
///     Applies one authenticated warehouse delivery exactly once per (connection, delivery id). The warehouse delivers at least
///     once, so the same delivery can arrive again later or at the same moment. A quick lookup answers the usual repeat; the
///     unique index on (connection, delivery id) decides the rest, because the delivery row is saved in the same transaction as
///     the reservation's commit: the request that loses the race rolls back as a whole, effect included, and finds the winner's
///     row. A recorded delivery is acknowledged whatever state the reservation has moved on to, which applying it again would
///     refuse.
/// </summary>
public sealed class WarehouseWebhookIntake(IInventoryDbContext dbContext, TimeProvider timeProvider)
{
    public async Task<Result<WebhookDeliveryOutcome>> ReceiveAsync(
        WarehouseConnection connection,
        string deliveryId,
        StockReservationId reservationId,
        string providerReference,
        CancellationToken cancellationToken)
    {
        if (await IsRecordedAsync(connection.Id, deliveryId, cancellationToken))
        {
            return WebhookDeliveryOutcome.Duplicate;
        }

        var receivedOn = timeProvider.GetUtcNow();

        // A reservation of another store is not found: a connection's secret only reaches the reservations of its own store.
        return await dbContext.StockReservations
            .TagWith(nameof(ReceiveAsync), reservationId)
            .Where(r => r.Id == reservationId
                        && dbContext.ProductSummaries.Any(p => p.ProductId == r.ProductId && p.StoreId == connection.StoreId))
            .SingleAsResultAsync(nameof(StockReservation), cancellationToken)
            .TapAsync(reservation => reservation.Commit(providerReference, receivedOn))
            .TapAsync(_ => dbContext.WebhookDeliveries.Add(WebhookDelivery.Receive(connection.Id, deliveryId, reservationId, receivedOn)))
            .MapAsync(_ => SaveAsync(connection.Id, deliveryId, cancellationToken));
    }

    private async Task<WebhookDeliveryOutcome> SaveAsync(
        WarehouseConnectionId connectionId, string deliveryId, CancellationToken cancellationToken)
    {
        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
            return WebhookDeliveryOutcome.Applied;
        }
        catch (Exception ex) when (ex is UniqueConstraintException or DbUpdateConcurrencyException)
        {
            // Another request got in between the lookup and this save. Its transaction committed first and ours rolled back,
            // so nothing of this delivery was applied. If it was the same delivery, that request did the work.
            dbContext.ChangeTracker.Clear();

            if (await IsRecordedAsync(connectionId, deliveryId, cancellationToken))
            {
                return WebhookDeliveryOutcome.Duplicate;
            }

            // A different delivery changed the same reservation: not a duplicate, so the conflict goes back to the warehouse,
            // which delivers this one again.
            throw;
        }
    }

    private Task<bool> IsRecordedAsync(WarehouseConnectionId connectionId, string deliveryId, CancellationToken cancellationToken)
    {
        return dbContext.WebhookDeliveries
            .AsNoTracking()
            .TagWith(nameof(IsRecordedAsync), connectionId)
            .AnyAsync(d => d.ConnectionId == connectionId && d.DeliveryId == deliveryId, cancellationToken);
    }
}
