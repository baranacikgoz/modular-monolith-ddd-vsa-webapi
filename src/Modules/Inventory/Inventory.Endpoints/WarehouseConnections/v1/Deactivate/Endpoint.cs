using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Inventory.Application.Persistence;
using Inventory.Domain.WarehouseConnections;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Inventory.Endpoints.WarehouseConnections.v1.Deactivate;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder warehouseConnectionsApiGroup)
    {
        warehouseConnectionsApiGroup
            .MapPost("{id}/deactivate", DeactivateAsync)
            .WithDescription("Deactivate a connection. Its webhooks are acknowledged and ignored from then on, and no release call goes through it. The row stays for history; the store connects again by creating a new connection.")
            .RequireScope(KeycloakScopes.WarehouseConnections.Update)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> DeactivateAsync(
        [AsParameters] Request request,
        IInventoryDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext.WarehouseConnections
            .TagWith(nameof(DeactivateAsync), request.Id)
            .Where(c => c.Id == request.Id)
            .SingleAsResultAsync(nameof(WarehouseConnection), cancellationToken)
            .TapAsync(connection => connection.Deactivate())
            .TapAsync(_ => dbContext.SaveChangesAsync(cancellationToken));
    }
}
