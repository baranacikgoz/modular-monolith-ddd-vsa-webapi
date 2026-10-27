using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Options;
using Common.Domain.ResultMonad;
using Common.Endpoints.Webhooks;
using Common.Infrastructure.Persistence.Extensions;
using Inventory.Application.BulkAdjustments;
using Inventory.Application.Persistence;
using Inventory.Infrastructure.BulkAdjustments;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Inventory.Endpoints.StockLevels.v1.BulkAdjust;

/// <summary>
///     Accept, queue, poll. Only the size and the extension are checked here: the workbook is stored in the private object
///     store, a job row and one queue message are written in the same transaction, and the answer is 202 with the job to
///     poll at GET /stock-levels/bulk-adjustments/{id}. The rows are read and applied by the queue consumer, in batches,
///     each as a signed adjustment with the one reason of the request. A repeated <c>ClientRequestId</c>, or an adjustment
///     already active for the caller, returns that job instead of queueing another.
/// </summary>
internal static class Endpoint
{
    /// <summary>Multipart framing (boundary and part headers) around the file itself. The body cap is the file limit plus this.</summary>
    private const long MultipartFramingBytes = 64 * 1024;

    internal static void MapEndpoint(RouteGroupBuilder stockLevelsApiGroup)
    {
        stockLevelsApiGroup
            .MapPost("bulk-adjustments", BulkAdjustAsync)
            .WithDescription("Queue a signed quantity correction per product from a spreadsheet (.xlsx or .xls: product id, signed quantity), all with one reason. Returns 202 with the job; poll GET /stock-levels/bulk-adjustments/{id} for progress, counters and rejected rows.")
            .RequireScope(KeycloakScopes.StockLevels.Update)
            .DisableAntiforgery()
            .LimitRequestBody(httpContext =>
                httpContext.RequestServices.GetRequiredService<IOptions<BulkImportOptions>>().Value.MaxFileBytes + MultipartFramingBytes)
            .Produces<Response>(StatusCodes.Status202Accepted)
            .TransformResultToAcceptedResponse<Response>();
    }

    private static async Task<Result<Response>> BulkAdjustAsync(
        [FromForm] Request request,
        ICurrentUser currentUser,
        IInventoryDbContext dbContext,
        StockBulkAdjustJobEnqueuer enqueuer,
        IOptions<BulkImportOptions> options,
        CancellationToken cancellationToken)
    {
        return await EnsureWithinSizeLimit(request.File, options.Value.MaxFileBytes)
            .BindAsync(() => enqueuer.EnqueueAsync(
                currentUser.Id, request.Reason, request.ClientRequestId, request.File.FileName, request.File.OpenReadStream(), cancellationToken))
            .BindAsync(jobId => dbContext.StockBulkAdjustJobs
                .AsNoTracking()
                .TagWith(nameof(BulkAdjustAsync), jobId)
                .Where(j => j.Id == jobId)
                .Select(Response.Projection)
                .SingleAsResultAsync(nameof(StockBulkAdjustJob), cancellationToken));
    }

    private static Result EnsureWithinSizeLimit(IFormFile file, long maxFileBytes)
    {
        return file.Length > maxFileBytes ? StockBulkAdjustErrors.FileTooLarge : Result.Success;
    }
}
