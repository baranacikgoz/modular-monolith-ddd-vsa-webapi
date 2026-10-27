using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Common.Infrastructure.Security;
using Inventory.Application.Persistence;
using Inventory.Domain.WarehouseConnections;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Inventory.Endpoints.WarehouseConnections.v1.ReplaceCredentials;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder warehouseConnectionsApiGroup)
    {
        warehouseConnectionsApiGroup
            .MapPut("{id}/credentials", ReplaceCredentialsAsync)
            .WithDescription("Replace the API credential this system presents to the warehouse. The new credential is stored encrypted and never returned.")
            .RequireScope(KeycloakScopes.WarehouseConnections.Update)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> ReplaceCredentialsAsync(
        [AsParameters] Request request,
        IInventoryDbContext dbContext,
        SecretProtector protector,
        CancellationToken cancellationToken)
    {
        return await dbContext.WarehouseConnections
            .TagWith(nameof(ReplaceCredentialsAsync), request.Id)
            .Where(c => c.Id == request.Id)
            .SingleAsResultAsync(nameof(WarehouseConnection), cancellationToken)
            .TapAsync(connection => connection.ReplaceCredentials(protector.Encrypt(request.Body.ApiCredential)))
            .TapAsync(_ => dbContext.SaveChangesAsync(cancellationToken));
    }
}
