using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Inventory.Application.Gateway;
using Inventory.Application.Persistence;
using Inventory.Application.WarehouseConnections;
using Inventory.Domain.StockReservations;
using Inventory.Domain.WarehouseConnections;
using Inventory.Infrastructure.Telemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Endpoints.StockReservations.v1.Release;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder stockReservationsApiGroup)
    {
        stockReservationsApiGroup
            .MapPost("{id}/release", ReleaseStockReservationAsync)
            .WithDescription("Releases a stock reservation's hold through the warehouse connection of the product's store.")
            .RequireScope(KeycloakScopes.StockReservations.Update)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    /// <summary>
    ///     Deliberately imperative, not a fluent Result pipeline: the attempt marker must be persisted
    ///     BEFORE the external warehouse call (crash-safety) and the outcome persisted in a separate save
    ///     AFTER it, with a real network call in between. Wires <see cref="StockReservation.BeginReleaseAttempt"/> /
    ///     <see cref="StockReservation.AbandonReleaseAttempt"/> to <see cref="IWarehouseGateway"/>: without
    ///     this the two-phase attempt-marker pattern and the gateway abstraction would exist only as unused
    ///     demo code. The gateway is the one of the product's store, resolved through its active warehouse connection
    ///     before the marker is saved, so a store without a connection leaves nothing behind.
    /// </summary>
    private static async Task<Result> ReleaseStockReservationAsync(
        [AsParameters] Request request,
        IInventoryDbContext dbContext,
        IWarehouseGatewayFactory gatewayFactory,
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

        // Already resolved: idempotent no-op, no external call needed. Never re-attempt an outcome that
        // already landed at the provider. A read-only reservation takes no local command, this one included.
        if (reservation is { Status: ReservationStatus.Released, IsReadOnly: false })
        {
            return Result.Success;
        }

        // In memory only until the save below: a failure before it (state refused, no connection) persists no marker.
        var attemptResult = reservation.BeginReleaseAttempt(timeProvider.GetUtcNow());
        if (attemptResult.IsFailure)
        {
            return attemptResult.Error!;
        }

        var gatewayResolution = await ResolveGatewayAsync(dbContext, gatewayFactory, reservation.ProductId, cancellationToken);
        if (gatewayResolution.IsFailure)
        {
            return gatewayResolution.Error!;
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        Result<string> gatewayResult;
        try
        {
            gatewayResult = await gatewayResolution.Value!.RequestReleaseAsync(
                reservation.ProductId.ToString(), reservation.Quantity, cancellationToken);
        }
        catch (WarehouseOutcomeUnknownException)
        {
            // Ambiguous: the release may or may not have happened. The marker stays so that nothing releases again blindly;
            // the idempotent reconcile call resolves it.
            return WarehouseGatewayErrors.OutcomeUnknown;
        }

        if (gatewayResult.IsFailure)
        {
            // A definite outcome: the warehouse refused, or our own quota or open circuit stopped the call before anything
            // was sent. Safe to abandon the marker so a genuine retry can proceed.
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

    private static Task<Result<IWarehouseGateway>> ResolveGatewayAsync(
        IInventoryDbContext dbContext,
        IWarehouseGatewayFactory gatewayFactory,
        DefaultIdType productId,
        CancellationToken cancellationToken)
    {
        return dbContext
            .ActiveConnectionOfProduct(productId)
            .AsNoTracking()
            .TagWith(nameof(ResolveGatewayAsync), productId)
            .SingleAsResultAsync(nameof(WarehouseConnection), cancellationToken)
            .BindAsync(connection => Task.FromResult(gatewayFactory.Create(connection)));
    }
}
