using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Inventory.Application.Gateway;
using Inventory.Application.Persistence;
using Inventory.Domain.StockReservations;
using Inventory.Infrastructure.Telemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Inventory.Endpoints.StockReservations.v1.Release;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder stockReservationsApiGroup)
    {
        stockReservationsApiGroup
            .MapPost("{id}/release", ReleaseStockReservationAsync)
            .WithDescription("Releases a stock reservation's hold.")
            .RequireScope(KeycloakScopes.StockReservations.Update)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    /// <summary>
    ///     Deliberately imperative, not a fluent Result pipeline: the attempt marker must be persisted
    ///     BEFORE the external warehouse call (crash-safety) and the outcome persisted in a separate save
    ///     AFTER it, with a real network call in between - mirrors Payments' DepositOutcomeService for the
    ///     same reason. Wires <see cref="StockReservation.BeginReleaseAttempt"/> /
    ///     <see cref="StockReservation.AbandonReleaseAttempt"/> to <see cref="IWarehouseGateway"/>: without
    ///     this the two-phase attempt-marker pattern and the gateway abstraction would exist only as unused
    ///     demo code.
    /// </summary>
    private static async Task<Result> ReleaseStockReservationAsync(
        [AsParameters] Request request,
        IInventoryDbContext dbContext,
        IWarehouseGateway gateway,
        TimeProvider timeProvider,
        CancellationToken cancellationToken)
    {
        var loadResult = await dbContext
            .StockReservations
            .TagWith(nameof(ReleaseStockReservationAsync), request.Id)
            .Where(r => r.Id == request.Id)
            .SingleAsResultAsync(nameof(StockReservation), cancellationToken);

        if (loadResult.IsFailure)
        {
            return loadResult.Error!;
        }

        var reservation = loadResult.Value!;

        // Already resolved: idempotent no-op, no external call needed - mirrors DepositOutcomeService's
        // FindHeldItem-based skip, never re-attempt an outcome that already landed at the provider.
        if (reservation.Status == ReservationStatus.Released)
        {
            return Result.Success;
        }

        var attemptResult = reservation.BeginReleaseAttempt(timeProvider.GetUtcNow());
        if (attemptResult.IsFailure)
        {
            return attemptResult.Error!;
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        var gatewayResult = await gateway.RequestReleaseAsync(
            reservation.ProductId.ToString(), reservation.Quantity, cancellationToken);

        if (gatewayResult.IsFailure)
        {
            // A definite rejection (the provider was reached and responded) - not a timeout, those throw
            // out of the resiliency pipeline and leave the marker in place for manual reconciliation via
            // IWarehouseGateway.ReconcileReleaseAsync. Safe to abandon the marker here so a genuine retry
            // can proceed.
            reservation.AbandonReleaseAttempt(timeProvider.GetUtcNow());
            await dbContext.SaveChangesAsync(cancellationToken);
            return gatewayResult.Error!;
        }

        var releaseResult = reservation.Release(timeProvider.GetUtcNow());
        if (releaseResult.IsFailure)
        {
            return releaseResult.Error!;
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        InventoryTelemetry.StockReservationsReleased.Add(1);
        return Result.Success;
    }
}
