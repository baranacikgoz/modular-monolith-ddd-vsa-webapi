using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Common.Infrastructure.Security;
using Inventory.Application.Persistence;
using Inventory.Application.StoreAccesses;
using Inventory.Domain.WarehouseConnections.Errors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using AllCreate = Inventory.Endpoints.WarehouseConnections.v1.Create;

namespace Inventory.Endpoints.WarehouseConnections.v1.My.Create;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder warehouseConnectionsApiGroup)
    {
        warehouseConnectionsApiGroup
            .MapPost("my", CreateMyWarehouseConnectionAsync)
            .WithDescription("Connect one of the active stores I own to its warehouse system. Takes the same body as the administration " +
                             "route and answers the same way: the webhook secret is shown exactly once, the API credential and the secret " +
                             "are stored encrypted and never returned. The store is checked against Inventory's own copy of store access; " +
                             "a store that is not mine, not active, or unknown to Inventory is answered as not found.")
            .RequireScope(KeycloakScopes.WarehouseConnections.Connect)
            .Produces<AllCreate.Response>()
            .TransformResultTo<AllCreate.Response>();
    }

    private static Task<Result<AllCreate.Response>> CreateMyWarehouseConnectionAsync(
        AllCreate.Request request,
        ICurrentUser currentUser,
        IInventoryDbContext dbContext,
        SecretProtector protector,
        CancellationToken cancellationToken)
    {
        // The role in the token only says the caller may ask; which store they may connect is decided here, by the local
        // store access copy, never by a role read.
        return dbContext
            .ActiveStoresOwnedBy(currentUser.Id)
            .AsNoTracking()
            .TagWith(nameof(CreateMyWarehouseConnectionAsync), currentUser.Id, request.StoreId)
            .Where(access => access.StoreId == request.StoreId)
            .AnyAsResultAsync(cancellationToken)
            .TapAsync(owned => owned ? Result.Success : WarehouseConnectionErrors.StoreNotAccessible)
            .BindAsync(_ => AllCreate.Endpoint.CreateAsync(request, dbContext, protector, cancellationToken));
    }
}
