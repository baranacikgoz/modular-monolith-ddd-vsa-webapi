using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Inventory.Application.Persistence;
using Inventory.Domain.WarehouseConnections;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Endpoints.WarehouseConnections.v1.Get;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder warehouseConnectionsApiGroup)
    {
        warehouseConnectionsApiGroup
            .MapGet("{id}", GetWarehouseConnectionAsync)
            .WithDescription("Get a warehouse connection by id. Never carries the API credential or the webhook secret.")
            .RequireScope(KeycloakScopes.WarehouseConnections.View)
            .Produces<WarehouseConnectionResponse>()
            .TransformResultTo<WarehouseConnectionResponse>();
    }

    private static async Task<Result<WarehouseConnectionResponse>> GetWarehouseConnectionAsync(
        [AsParameters] Request request,
        IInventoryDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext.WarehouseConnections
            .AsNoTracking()
            .TagWith(nameof(GetWarehouseConnectionAsync), request.Id)
            .Where(c => c.Id == request.Id)
            .Select(c => new WarehouseConnectionResponse
            {
                Id = c.Id,
                StoreId = c.StoreId,
                Provider = c.Provider,
                BaseUrl = c.BaseUrl,
                Status = c.Status,
                CreatedOn = c.CreatedOn,
                CreatedBy = c.CreatedBy,
                LastModifiedOn = c.LastModifiedOn,
                LastModifiedBy = c.LastModifiedBy
            })
            .SingleAsResultAsync(nameof(WarehouseConnection), cancellationToken);
    }
}
