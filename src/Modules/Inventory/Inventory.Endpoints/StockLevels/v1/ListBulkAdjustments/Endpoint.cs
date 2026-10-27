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
using BulkAdjustResponse = Inventory.Endpoints.StockLevels.v1.BulkAdjust.Response;

namespace Inventory.Endpoints.StockLevels.v1.ListBulkAdjustments;

/// <summary>My bulk adjustment jobs, newest first. Cursor paging: pass a page's <c>nextCursor</c> back as <c>after</c> to
/// continue right after its last job with <c>LIMIT</c> only, so a deep page costs the same as the first.</summary>
internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder stockLevelsApiGroup)
    {
        stockLevelsApiGroup
            .MapGet("bulk-adjustments", ListBulkAdjustmentsAsync)
            .WithDescription("List my bulk adjustment jobs, newest first.")
            .RequireScope(KeycloakScopes.StockLevels.Update)
            .Produces<PaginationResponse<BulkAdjustResponse>>()
            .TransformResultTo<PaginationResponse<BulkAdjustResponse>>();
    }

    private static async Task<Result<PaginationResponse<BulkAdjustResponse>>> ListBulkAdjustmentsAsync(
        [AsParameters] Request request,
        ICurrentUser currentUser,
        IInventoryDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext.StockBulkAdjustJobs
            .AsNoTracking()
            .TagWith(nameof(ListBulkAdjustmentsAsync), currentUser.Id)
            .Where(j => j.RequestedBy == currentUser.Id)
            .PaginateAsync(
                request: request,
                selector: BulkAdjustResponse.Projection,
                orderByDescending: j => j.QueuedOn,
                tiebreaker: j => j.Id,
                cancellationToken: cancellationToken);
    }
}
