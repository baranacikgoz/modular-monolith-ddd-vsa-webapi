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
using Products.Infrastructure.Exports;

namespace Products.Endpoints.Products.v1.Export;

/// <summary>Accept, queue, poll. Only writes a <see cref="ProductExportJob" /> and queues a message, then answers 202 with
/// the job to poll at GET /products/exports/{id}; the CSV is built by the queue consumer and downloaded from
/// GET /products/exports/{id}/download. An export already active for the caller is returned instead of queueing another.</summary>
internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder productsApiGroup)
    {
        productsApiGroup
            .MapPost("exports", ExportAsync)
            .WithDescription("Queue a CSV export of products, optionally of one store. Returns 202 with the export job; poll GET /products/exports/{id}, then download the file.")
            .RequireScope(KeycloakScopes.Products.Export)
            .Produces<Response>(StatusCodes.Status202Accepted)
            .TransformResultToAcceptedResponse<Response>();
    }

    private static async Task<Result<Response>> ExportAsync(
        [AsParameters] Request request,
        ICurrentUser currentUser,
        IProductsDbContext dbContext,
        ProductExportJobEnqueuer enqueuer,
        CancellationToken cancellationToken)
    {
        return await enqueuer.EnqueueAsync(currentUser.Id, request.StoreId, cancellationToken)
            .BindAsync(jobId => dbContext.ExportJobs
                .AsNoTracking()
                .TagWith(nameof(ExportAsync), jobId)
                .Where(j => j.Id == jobId)
                .Select(Response.Projection)
                .SingleAsResultAsync(nameof(ProductExportJob), cancellationToken));
    }
}
