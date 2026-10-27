using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Pagination;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Inventory.Application.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Endpoints.WarehouseConnections.v1.Search;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder warehouseConnectionsApiGroup)
    {
        warehouseConnectionsApiGroup
            .MapGet("", SearchWarehouseConnectionsAsync)
            .WithDescription("List warehouse connections, newest first, optionally of one store or one status. Never carries a credential or a webhook secret.")
            .RequireScope(KeycloakScopes.WarehouseConnections.View)
            .Produces<PaginationResponse<WarehouseConnectionResponse>>()
            .TransformResultTo<PaginationResponse<WarehouseConnectionResponse>>();
    }

    private static async Task<Result<PaginationResponse<WarehouseConnectionResponse>>> SearchWarehouseConnectionsAsync(
        [AsParameters] Request request,
        IInventoryDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext.WarehouseConnections
            .AsNoTracking()
            .TagWith(nameof(SearchWarehouseConnectionsAsync))
            .WhereIf(c => c.StoreId == request.StoreId!.Value, request.StoreId is not null)
            .WhereIf(c => c.Status == request.Status!.Value, request.Status is not null)
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
