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
using ImportResponse = Products.Endpoints.Products.v1.Import.Response;

namespace Products.Endpoints.Products.v1.ListImports;

/// <summary>My import jobs, newest first. Cursor paging: pass a page's <c>nextCursor</c> back as <c>after</c> to continue
/// right after its last job with <c>LIMIT</c> only, so a deep page costs the same as the first.</summary>
internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder productsApiGroup)
    {
        productsApiGroup
            .MapGet("imports", ListImportsAsync)
            .WithDescription("List my product import jobs, newest first.")
            .RequireScope(KeycloakScopes.Products.ImportOwn)
            .Produces<PaginationResponse<ImportResponse>>()
            .TransformResultTo<PaginationResponse<ImportResponse>>();
    }

    private static async Task<Result<PaginationResponse<ImportResponse>>> ListImportsAsync(
        [AsParameters] Request request,
        ICurrentUser currentUser,
        IProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext.ImportJobs
            .AsNoTracking()
            .TagWith(nameof(ListImportsAsync), currentUser.Id)
            .Where(j => j.RequestedBy == currentUser.Id)
            .PaginateAsync(
                request: request,
                selector: ImportResponse.Projection,
                orderByDescending: j => j.QueuedOn,
                tiebreaker: j => j.Id,
                cancellationToken: cancellationToken);
    }
}
