using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Inventory.Application.Gateway;
using Inventory.Application.Persistence;
using Inventory.Application.WarehouseConnections;
using Inventory.Domain.StockReservations;
using Inventory.Domain.StockReservations.Errors;
using Inventory.Domain.WarehouseConnections;
using Inventory.Infrastructure.Telemetry;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.StockReservations;

/// <summary>What the warehouse system said became of a release whose outcome this system never recorded.</summary>
public enum ReleaseReconcileOutcome
{
    /// <summary>The warehouse released the hold: the release was completed locally.</summary>
    Released,

    /// <summary>The warehouse never released it: the attempt marker was abandoned, so a release can be tried again.</summary>
    NotReleased,

    /// <summary>The reservation was released already: nothing was asked and nothing changed.</summary>
    AlreadyReleased
}

/// <summary>
///     Resolves a release attempt whose outcome is unknown (the warehouse did not answer in time, or the request ended between
///     the call and the save): it asks the warehouse of the product's store, through the gateway's idempotent reconcile call,
///     what became of the release and applies the answer. A confirmed release completes locally exactly as the release
///     endpoint would have (<see cref="StockReservation.ConfirmReleaseAttempt" />); a warehouse that has no such release lets
///     the marker go (<see cref="StockReservation.AbandonReleaseAttempt" />), so a genuine retry can proceed. Any other answer,
///     a timeout included, is returned as a failed result and leaves the marker in place for the next reconcile: an unknown
///     outcome is never guessed. The gateway is asked with the product reference the release call used, because that is the
///     only reference the warehouse was given.
///     <para>
///         Shared by the admin endpoint and by the queue consumer a recurring sweep feeds. Every expected outcome is a
///         <see cref="Result" />; only a fault of this system (the database) is an exception.
///     </para>
/// </summary>
public sealed class StockReleaseReconciler(
    IInventoryDbContext dbContext,
    IWarehouseGatewayFactory gatewayFactory,
    TimeProvider timeProvider)
{
    public Task<Result<ReleaseReconcileOutcome>> ReconcileAsync(StockReservationId id, CancellationToken cancellationToken)
    {
        return dbContext.StockReservations
            .TagWith(nameof(ReconcileAsync), id)
            .Where(r => r.Id == id)
            .SingleAsResultAsync(nameof(StockReservation), cancellationToken)
            .BindAsync(reservation => ReconcileAsync(reservation, cancellationToken))
            .TapAsync(outcome => InventoryTelemetry.StockReleaseReconciliations.Add(
                1, new KeyValuePair<string, object?>("outcome", outcome.ToString())));
    }

    private Task<Result<ReleaseReconcileOutcome>> ReconcileAsync(StockReservation reservation, CancellationToken cancellationToken)
    {
        if (reservation is { Status: ReservationStatus.Released, IsReadOnly: false })
        {
            return Task.FromResult<Result<ReleaseReconcileOutcome>>(ReleaseReconcileOutcome.AlreadyReleased);
        }

        if (reservation.LastReleaseAttemptReference is null)
        {
            return Task.FromResult<Result<ReleaseReconcileOutcome>>(StockReservationErrors.NoReleaseAttempt);
        }

        return ResolveGatewayAsync(reservation.ProductId, cancellationToken)
            .BindAsync(gateway => AskTheWarehouseAsync(gateway, reservation.ProductId, cancellationToken))
            .BindAsync(released => ApplyAsync(reservation, released, cancellationToken));
    }

    /// <summary>True when the warehouse released the hold, false when it has no such release; every other answer is a failure.</summary>
    private static async Task<Result<bool>> AskTheWarehouseAsync(
        IWarehouseGateway gateway, DefaultIdType productId, CancellationToken cancellationToken)
    {
        try
        {
            var answer = await gateway.ReconcileReleaseAsync(productId.ToString(), cancellationToken);
            if (!answer.IsFailure)
            {
                return true;
            }

            if (answer.Error!.Key == nameof(Error.NotFound))
            {
                return false;
            }

            return answer.Error;
        }
        catch (WarehouseOutcomeUnknownException)
        {
            return WarehouseGatewayErrors.OutcomeUnknown;
        }
    }

    private Task<Result<ReleaseReconcileOutcome>> ApplyAsync(
        StockReservation reservation, bool released, CancellationToken cancellationToken)
    {
        var now = timeProvider.GetUtcNow();

        return dbContext
            .ApplyAndSaveAsync(
                reservation, r => released ? r.ConfirmReleaseAttempt(now) : r.AbandonReleaseAttempt(now), cancellationToken)
            .BindAsync(() => released ? ReleaseReconcileOutcome.Released : ReleaseReconcileOutcome.NotReleased);
    }

    private Task<Result<IWarehouseGateway>> ResolveGatewayAsync(DefaultIdType productId, CancellationToken cancellationToken)
    {
        return dbContext
            .ActiveConnectionOfProduct(productId)
            .AsNoTracking()
            .TagWith(nameof(ResolveGatewayAsync), productId)
            .SingleAsResultAsync(nameof(WarehouseConnection), cancellationToken)
            .BindAsync(connection => Task.FromResult(gatewayFactory.Create(connection)));
    }
}
