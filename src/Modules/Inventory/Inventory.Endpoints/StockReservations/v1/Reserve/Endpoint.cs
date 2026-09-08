using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Extensions;
using Inventory.Application.Persistence;
using Inventory.Domain.StockReservations;
using Inventory.Infrastructure.Telemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Inventory.Endpoints.StockReservations.v1.Reserve;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder stockReservationsApiGroup)
    {
        stockReservationsApiGroup
            .MapPost("", ReserveStockAsync)
            .WithDescription("Reserve stock for a pending order.")
            .RequireScope(KeycloakScopes.StockReservations.Create)
            .Produces<Response>()
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> ReserveStockAsync(
        Request request,
        IInventoryDbContext dbContext,
        CancellationToken cancellationToken)
    {
        using var activity = InventoryTelemetry.ActivitySource.StartActivityForCaller();

        var reservation = StockReservation.Reserve(
            request.Body.ProductId, request.Body.Quantity, request.Body.ReservationDeadline);

        return await Result<StockReservation>.Success(reservation)
            .Tap(r => dbContext.StockReservations.Add(r))
            .TapAsync(_ => dbContext.SaveChangesAsync(cancellationToken))
            .TapAsync(_ => InventoryTelemetry.StockReservationsReserved.Add(1))
            .MapAsync(r => new Response { Id = r.Id })
            .TapActivityAsync(activity);
    }
}
