using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Options;
using Common.Domain.ResultMonad;
using Common.Endpoints.Webhooks;
using Common.Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Products.Application.Imports;
using Products.Application.Persistence;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;
using Products.Infrastructure.Imports;

namespace Products.Endpoints.Products.v1.Import;

/// <summary>
///     Accept, queue, poll. Only the size and the extension are checked here: the workbook is stored in the private object
///     store, a job row and one queue message are written in the same transaction, and the answer is 202 with the job to
///     poll at GET /products/imports/{id}. The rows are read and saved by the queue consumer, in batches. Imports into the
///     caller's own store, which has to be Active: any other status answers 409 with that status's error before the file is
///     stored. A repeated <c>ClientRequestId</c>, or an import already active for the caller, returns that job
///     instead of queueing another.
/// </summary>
internal static class Endpoint
{
    /// <summary>Multipart framing (boundary and part headers) around the file itself. The body cap is the file limit plus this.</summary>
    private const long MultipartFramingBytes = 64 * 1024;

    internal static void MapEndpoint(RouteGroupBuilder productsApiGroup)
    {
        productsApiGroup
            .MapPost("imports", ImportAsync)
            .WithDescription("Queue an upsert of products into my store from a spreadsheet (.xlsx or .xls: name, description, quantity, price). Returns 202 with the import job; poll GET /products/imports/{id} for progress, counters and rejected rows.")
            .RequireScope(KeycloakScopes.Products.ImportOwn)
            .DisableAntiforgery()
            .LimitRequestBody(httpContext =>
                httpContext.RequestServices.GetRequiredService<IOptions<BulkImportOptions>>().Value.MaxFileBytes + MultipartFramingBytes)
            .Produces<Response>(StatusCodes.Status202Accepted)
            .TransformResultToAcceptedResponse<Response>();
    }

    private static async Task<Result<Response>> ImportAsync(
        [FromForm] Request request,
        ICurrentUser currentUser,
        IProductsDbContext dbContext,
        ProductImportJobEnqueuer enqueuer,
        IOptions<BulkImportOptions> options,
        CancellationToken cancellationToken)
    {
        var productTemplateId = new ProductTemplateId(request.ProductTemplateId);

        return await EnsureWithinSizeLimit(request.File, options.Value.MaxFileBytes)
            .BindAsync(() => dbContext.Stores
                .AsNoTracking()
                .TagWith(nameof(ImportAsync), "StoreByOwner", currentUser.Id)
                .Where(s => s.OwnerId == currentUser.Id)
                .SingleAsResultAsync(nameof(Store), cancellationToken))
            .TapAsync(store => store.EnsureActive())
            .CombineAsync(store => dbContext.ProductTemplates
                .AsNoTracking()
                .TagWith(nameof(ImportAsync), "ActiveProductTemplateById", productTemplateId)
                .Where(pt => pt.IsActive && pt.Id == productTemplateId)
                .SingleAsResultAsync(nameof(ProductTemplate), cancellationToken))
            .BindAsync(pair => enqueuer.EnqueueAsync(
                currentUser.Id, pair.Item1.Id, pair.Item2.Id, request.ClientRequestId, request.File.FileName, request.File.OpenReadStream(), cancellationToken))
            .BindAsync(jobId => dbContext.ImportJobs
                .AsNoTracking()
                .TagWith(nameof(ImportAsync), jobId)
                .Where(j => j.Id == jobId)
                .Select(Response.Projection)
                .SingleAsResultAsync(nameof(ProductImportJob), cancellationToken));
    }

    private static Result EnsureWithinSizeLimit(IFormFile file, long maxFileBytes)
    {
        return file.Length > maxFileBytes ? ProductImportErrors.FileTooLarge : Result.Success;
    }
}
