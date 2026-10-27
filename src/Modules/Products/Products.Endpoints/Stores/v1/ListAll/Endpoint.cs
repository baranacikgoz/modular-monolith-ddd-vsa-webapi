using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Pagination;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Products.Application.Persistence;

namespace Products.Endpoints.Stores.v1.ListAll;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder storesApiGroup)
    {
        storesApiGroup
            .MapGet("administration", ListAllStoresAsync)
            .WithDescription("List stores in any status (pending approval, active, suspended or closed), newest first, optionally of one status. The public search lists active stores only.")
            .RequireScope(KeycloakScopes.Stores.ViewAll)
            .Produces<PaginationResponse<Response>>()
            .TransformResultTo<PaginationResponse<Response>>();
    }

    private static async Task<Result<PaginationResponse<Response>>> ListAllStoresAsync(
        [AsParameters] Request request,
        IProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext
            .Stores
            .AsNoTracking()
            .TagWith(nameof(ListAllStoresAsync))
            .WhereIf(s => s.Status == request.Status!.Value, request.Status is not null)
            .PaginateAsync(
                request: request,
                selector: s => new Response
                {
                    Id = s.Id,
                    OwnerId = s.OwnerId,
                    Name = s.Name,
                    Description = s.Description,
                    Address = s.Address,
                    ProductCount = s.Products.Count,
                    Status = s.Status,
                    CreatedBy = s.CreatedBy,
                    CreatedOn = s.CreatedOn,
                    LastModifiedBy = s.LastModifiedBy,
                    LastModifiedOn = s.LastModifiedOn
                },
                orderByDescending: s => s.CreatedOn,
                tiebreaker: s => s.Id,
                cancellationToken: cancellationToken);
    }
}
