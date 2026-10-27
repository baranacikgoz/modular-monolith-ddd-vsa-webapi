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
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.ListApplications;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder storesApiGroup)
    {
        storesApiGroup
            .MapGet("applications", ListApplicationsAsync)
            .WithDescription("Store applications waiting for a decision, oldest first.")
            .RequireScope(KeycloakScopes.Stores.Approve)
            .Produces<PaginationResponse<Response>>()
            .TransformResultTo<PaginationResponse<Response>>();
    }

    private static async Task<Result<PaginationResponse<Response>>> ListApplicationsAsync(
        [AsParameters] Request request,
        IProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext
            .Stores
            .AsNoTracking()
            .TagWith(nameof(ListApplicationsAsync))
            .Where(s => s.Status == StoreStatus.PendingApproval)
            .PaginateAsync(
                request: request,
                selector: s => new Response
                {
                    Id = s.Id,
                    OwnerId = s.OwnerId,
                    Name = s.Name,
                    Description = s.Description,
                    Address = s.Address,
                    CreatedOn = s.CreatedOn
                },
                orderBy: s => s.CreatedOn,
                tiebreaker: s => s.Id,
                cancellationToken: cancellationToken);
    }
}
