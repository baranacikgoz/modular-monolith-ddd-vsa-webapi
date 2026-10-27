using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Inventory.Application.Persistence;
using Inventory.Application.StockReservations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using AllReservations = Inventory.Endpoints.StockReservations.v1.Get;

namespace Inventory.Endpoints.StockReservations.v1.My.Get;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder stockReservationsApiGroup)
    {
        stockReservationsApiGroup
            .MapGet("my/{id}", GetMyStockReservationAsync)
            .WithDescription("Get one reservation of a product of an active store I own. Reads Inventory's own copy of store access, so " +
                             "a reservation of anyone else's store, of a store that is not active, or of a product nobody has told " +
                             "Inventory about yet is answered as not found.")
            .RequireScope(KeycloakScopes.StockReservations.ViewOwn)
            .Produces<AllReservations.Response>()
            .TransformResultTo<AllReservations.Response>();
    }

    private static Task<Result<AllReservations.Response>> GetMyStockReservationAsync(
        [AsParameters] AllReservations.Request request,
        ICurrentUser currentUser,
        IInventoryDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return AllReservations.Endpoint.ReadAsync(
            dbContext.StockReservationsOfActiveStoresOwnedBy(currentUser.Id), request.Id, cancellationToken);
    }
}
