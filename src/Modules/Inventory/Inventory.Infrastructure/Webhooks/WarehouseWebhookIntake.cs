using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using EntityFramework.Exceptions.Common;
using Inventory.Application.Persistence;
using Inventory.Domain.StockReservations;
using Inventory.Domain.WarehouseConnections;
using Inventory.Domain.WebhookDeliveries;
using Inventory.Infrastructure.Telemetry;
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
    Ignored,

    /// <summary>The reservation is read-only: the status was taken into its provider status mirror and nothing else changed.</summary>
    Mirrored,

    /// <summary>
    ///     The status word has no mapping: acknowledged, never refused. It is recorded in the audit trail and the reservation is
    ///     read-only from now on.
    /// </summary>
    Unrecognized,

    /// <summary>
    ///     The delivery carries a provider timestamp earlier than one already applied to the reservation: acknowledged and
    ///     recorded as received, nothing applied.
    /// </summary>
    Stale
}

/// <summary>
///     Applies one authenticated warehouse delivery exactly once per (connection, delivery id). The warehouse delivers at least
///     once, so the same delivery can arrive again later or at the same moment. A quick lookup answers the usual repeat; the
///     unique index on (connection, delivery id) decides the rest, because the delivery row is saved in the same transaction as
///     the reservation's commit: the request that loses the race rolls back as a whole, effect included, and finds the winner's
///     row. A recorded delivery is acknowledged whatever state the reservation has moved on to, which applying it again would
///     refuse. The status word of the delivery is mapped once, here: a mapped one reaches the reservation as the commands it
///     already has (or, for a read-only reservation, only as a mirror), and a word nobody mapped is recorded and acknowledged.
///     A delivery that carries the provider's timestamp is applied in the order of that timestamp: one older than the latest
///     applied to the reservation is recorded as received and applies nothing, so a status feed that arrives out of order
///     cannot move a reservation back.
/// </summary>
public sealed class WarehouseWebhookIntake(IInventoryDbContext dbContext, TimeProvider timeProvider)
{
    public async Task<Result<WebhookDeliveryOutcome>> ReceiveAsync(
        WarehouseConnection connection,
        string deliveryId,
        StockReservationId reservationId,
        string? providerReference,
        string? rawStatus,
        DateTimeOffset? occurredOn,
        CancellationToken cancellationToken)
    {
        if (await IsRecordedAsync(connection.Id, deliveryId, cancellationToken))
        {
            return WebhookDeliveryOutcome.Duplicate;
        }

        var receivedOn = timeProvider.GetUtcNow();
        var resolution = ProviderStatusMap.Resolve(rawStatus, out var reported);

        // A reservation of another store is not found: a connection's secret only reaches the reservations of its own store.
        return await dbContext.StockReservations
            .TagWith(nameof(ReceiveAsync), reservationId)
            .Where(r => r.Id == reservationId
                        && dbContext.ProductSummaries.Any(p => p.ProductId == r.ProductId && p.StoreId == connection.StoreId))
            .SingleAsResultAsync(nameof(StockReservation), cancellationToken)
            .MapAsync(reservation => (Reservation: reservation, Outcome: OutcomeOf(resolution, reservation, occurredOn)))
            .TapAsync(pending => pending.Outcome == WebhookDeliveryOutcome.Stale
                ? Result.Success
                : Apply(pending.Reservation, resolution, reported, rawStatus, providerReference, receivedOn, occurredOn))
            .TapAsync(_ => dbContext.WebhookDeliveries.Add(WebhookDelivery.Receive(connection.Id, deliveryId, reservationId, receivedOn)))
            .MapAsync(pending => SaveAsync(connection.Id, deliveryId, pending.Outcome, reported, cancellationToken));
    }

    /// <summary>
    ///     Decided before the status is applied: a delivery older than one already applied is stale whatever it says, and an
    ///     unrecognized word turns the reservation read-only, which is not what mirrored means.
    /// </summary>
    private static WebhookDeliveryOutcome OutcomeOf(
        ProviderStatusResolution resolution, StockReservation reservation, DateTimeOffset? occurredOn)
    {
        if (reservation.IsStale(occurredOn))
        {
            return WebhookDeliveryOutcome.Stale;
        }

        if (resolution == ProviderStatusResolution.Unrecognized)
        {
            return WebhookDeliveryOutcome.Unrecognized;
        }

        return reservation.IsReadOnly ? WebhookDeliveryOutcome.Mirrored : WebhookDeliveryOutcome.Applied;
    }

    private static Result Apply(
        StockReservation reservation,
        ProviderStatusResolution resolution,
        ReservationStatus reported,
        string? rawStatus,
        string? providerReference,
        DateTimeOffset receivedOn,
        DateTimeOffset? occurredOn)
    {
        if (resolution == ProviderStatusResolution.Unrecognized)
        {
            // Unrecognized means the word is not blank.
            reservation.RecordUnrecognizedProviderStatus(rawStatus!, receivedOn, occurredOn);
            return Result.Success;
        }

        return reservation.ReceiveProviderStatus(reported, providerReference, receivedOn, occurredOn);
    }

    private async Task<WebhookDeliveryOutcome> SaveAsync(
        WarehouseConnectionId connectionId,
        string deliveryId,
        WebhookDeliveryOutcome outcome,
        ReservationStatus reported,
        CancellationToken cancellationToken)
    {
        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
            RecordEffect(outcome, reported);
            return outcome;
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

    private static void RecordEffect(WebhookDeliveryOutcome outcome, ReservationStatus reported)
    {
        if (outcome != WebhookDeliveryOutcome.Applied)
        {
            return;
        }

        var counter = reported switch
        {
            ReservationStatus.Committed => InventoryTelemetry.StockReservationsCommitted,
            ReservationStatus.Released => InventoryTelemetry.StockReservationsReleased,
            _ => null
        };
        counter?.Add(1);
    }

    private Task<bool> IsRecordedAsync(WarehouseConnectionId connectionId, string deliveryId, CancellationToken cancellationToken)
    {
        return dbContext.WebhookDeliveries
            .AsNoTracking()
            .TagWith(nameof(IsRecordedAsync), connectionId)
            .AnyAsync(d => d.ConnectionId == connectionId && d.DeliveryId == deliveryId, cancellationToken);
    }
}
