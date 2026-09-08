using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Inventory.Application.Persistence;
using Inventory.Domain.StockReservations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Endpoints.StockReservations.v1.Get;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder stockReservationsApiGroup)
    {
        stockReservationsApiGroup
            .MapGet("{id}", GetStockReservationAsync)
            .WithDescription("Get a stock reservation by id.")
            .RequireScope(KeycloakScopes.StockReservations.View)
            .Produces<Response>()
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> GetStockReservationAsync(
        [AsParameters] Request request,
        IInventoryDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext.StockReservations
            .AsNoTracking()
            .TagWith(nameof(GetStockReservationAsync), request.Id)
            .Where(r => r.Id == request.Id)
            .Select(r => new Response
            {
                Id = r.Id,
                ProductId = r.ProductId,
                Quantity = r.Quantity,
                Status = r.Status,
                ReservationDeadline = r.ReservationDeadline,
                ProviderReference = r.ProviderReference
            })
            .SingleAsResultAsync(nameof(StockReservation), cancellationToken);
    }
}
