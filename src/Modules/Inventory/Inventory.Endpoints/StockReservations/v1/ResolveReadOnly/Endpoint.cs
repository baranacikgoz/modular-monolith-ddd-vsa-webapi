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
using Microsoft.EntityFrameworkCore;

namespace Inventory.Endpoints.StockReservations.v1.ResolveReadOnly;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder stockReservationsApiGroup)
    {
        stockReservationsApiGroup
            .MapPost("{id}/resolve-read-only", ResolveReadOnlyStockReservationAsync)
            .WithDescription(
                "Resolves a read-only stock reservation, one whose warehouse system reported a status this system could not map. " +
                "The caller states what the reservation really is at the warehouse system (Released, Committed or Expired); the " +
                "reservation then takes local commands again.")
            .RequireScope(KeycloakScopes.StockReservations.Update)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> ResolveReadOnlyStockReservationAsync(
        [AsParameters] Request request,
        IInventoryDbContext dbContext,
        TimeProvider timeProvider,
        CancellationToken cancellationToken)
    {
        return await dbContext
            .StockReservations
            .TagWith(nameof(ResolveReadOnlyStockReservationAsync), request.Id)
            .Where(r => r.Id == request.Id)
            .SingleAsResultAsync(nameof(StockReservation), cancellationToken)
            .TapAsync(reservation => reservation.ResolveReadOnly(
                request.Body.TargetStatus, request.Body.ProviderReference, timeProvider.GetUtcNow()))
            .TapAsync(async _ => await dbContext.SaveChangesAsync(cancellationToken))
            .TapAsync(_ => InventoryTelemetry.ReadOnlyReservationsResolved.Add(
                1, new KeyValuePair<string, object?>("target", request.Body.TargetStatus.ToString())));
    }
}
