using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Pagination;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Inventory.Application.Persistence;
using Inventory.Application.WarehouseConnections;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Endpoints.WarehouseConnections.v1.My.Search;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder warehouseConnectionsApiGroup)
    {
        warehouseConnectionsApiGroup
            .MapGet("my", SearchMyWarehouseConnectionsAsync)
            .WithDescription(
                "List the warehouse connections of the active stores I own, newest first. Reads Inventory's own copy of store access, " +
                "so a store that is not active, or one nobody has told Inventory about yet, does not appear. Never carries a credential " +
                "or a webhook secret.")
            .RequireScope(KeycloakScopes.WarehouseConnections.ViewOwn)
            .Produces<PaginationResponse<WarehouseConnectionResponse>>()
            .TransformResultTo<PaginationResponse<WarehouseConnectionResponse>>();
    }

    private static async Task<Result<PaginationResponse<WarehouseConnectionResponse>>> SearchMyWarehouseConnectionsAsync(
        [AsParameters] Request request,
        ICurrentUser currentUser,
        IInventoryDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext
            .OfActiveStoresOwnedBy(currentUser.Id)
            .AsNoTracking()
            .TagWith(nameof(SearchMyWarehouseConnectionsAsync), currentUser.Id)
            .PaginateAsync(
                request: request,
                selector: c => new WarehouseConnectionResponse
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
                },
                orderByDescending: c => c.CreatedOn,
                tiebreaker: c => c.Id,
                cancellationToken: cancellationToken);
    }
}
