using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Products.Application.Exports;
using Products.Application.Persistence;
using ExportResponse = Products.Endpoints.Products.v1.Export.Response;

namespace Products.Endpoints.Products.v1.GetExport;

/// <summary>Polled after the 202 of POST /products/exports. Ownership is the requester: another user's job is 404, never
/// 403.</summary>
internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder productsApiGroup)
    {
        productsApiGroup
            .MapGet("exports/{id}", GetExportAsync)
            .WithDescription("Status and progress of one of my product export jobs (poll).")
            .RequireScope(KeycloakScopes.Products.Export)
            .Produces<ExportResponse>()
            .TransformResultTo<ExportResponse>();
    }

    private static async Task<Result<ExportResponse>> GetExportAsync(
        [AsParameters] Request request,
        ICurrentUser currentUser,
        IProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext.ExportJobs
            .AsNoTracking()
            .TagWith(nameof(GetExportAsync), request.Id)
            .Where(j => j.Id == request.Id && j.RequestedBy == currentUser.Id)
            .Select(ExportResponse.Projection)
            .SingleAsResultAsync(nameof(ProductExportJob), cancellationToken);
    }
}
