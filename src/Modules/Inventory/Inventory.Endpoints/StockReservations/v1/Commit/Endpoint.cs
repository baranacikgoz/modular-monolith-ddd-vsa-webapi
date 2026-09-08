using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Inventory.Application.Persistence;
using Inventory.Domain.StockReservations;
using Inventory.Infrastructure.Telemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Inventory.Endpoints.StockReservations.v1.Commit;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder stockReservationsApiGroup)
    {
        stockReservationsApiGroup
            .MapPost("{id}/commit", CommitStockReservationAsync)
            .WithDescription("Records the warehouse system's confirmation for a stock reservation.")
            .RequireScope(KeycloakScopes.StockReservations.Update)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> CommitStockReservationAsync(
        [AsParameters] Request request,
        IInventoryDbContext dbContext,
        TimeProvider timeProvider,
        CancellationToken cancellationToken)
    {
        return await dbContext
            .StockReservations
            .TagWith(nameof(CommitStockReservationAsync), request.Id)
            .Where(r => r.Id == request.Id)
            .SingleAsResultAsync(nameof(StockReservation), cancellationToken)
            .TapAsync(reservation => reservation.Commit(request.Body.ProviderReference, timeProvider.GetUtcNow()))
            .TapAsync(async _ => await dbContext.SaveChangesAsync(cancellationToken))
            .TapAsync(_ => InventoryTelemetry.StockReservationsCommitted.Add(1));
    }
}
