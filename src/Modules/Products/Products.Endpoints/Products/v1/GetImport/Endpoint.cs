using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Products.Application.Imports;
using Products.Application.Persistence;
using ImportResponse = Products.Endpoints.Products.v1.Import.Response;

namespace Products.Endpoints.Products.v1.GetImport;

/// <summary>Polled after the 202 of POST /products/imports. Ownership is the requester: another user's job is 404, never
/// 403, so a job id leaks nothing.</summary>
internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder productsApiGroup)
    {
        productsApiGroup
            .MapGet("imports/{id}", GetImportAsync)
            .WithDescription("Status, progress, counters and rejected rows of one of my product import jobs (poll).")
            .RequireScope(KeycloakScopes.Products.ImportOwn)
            .Produces<ImportResponse>()
            .TransformResultTo<ImportResponse>();
    }

    private static async Task<Result<ImportResponse>> GetImportAsync(
        [AsParameters] Request request,
        ICurrentUser currentUser,
        IProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext.ImportJobs
            .AsNoTracking()
            .TagWith(nameof(GetImportAsync), request.Id)
            .Where(j => j.Id == request.Id && j.RequestedBy == currentUser.Id)
            .Select(ImportResponse.Projection)
            .SingleAsResultAsync(nameof(ProductImportJob), cancellationToken);
    }
}
