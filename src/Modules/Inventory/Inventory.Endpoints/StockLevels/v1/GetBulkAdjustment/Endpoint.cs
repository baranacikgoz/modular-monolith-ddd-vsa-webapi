using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Inventory.Application.BulkAdjustments;
using Inventory.Application.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using BulkAdjustResponse = Inventory.Endpoints.StockLevels.v1.BulkAdjust.Response;

namespace Inventory.Endpoints.StockLevels.v1.GetBulkAdjustment;

/// <summary>Polled after the 202 of POST /stock-levels/bulk-adjustments. Ownership is the requester: another user's job is
/// 404, never 403, so a job id leaks nothing.</summary>
internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder stockLevelsApiGroup)
    {
        stockLevelsApiGroup
            .MapGet("bulk-adjustments/{id}", GetBulkAdjustmentAsync)
            .WithDescription("Status, progress, counters and rejected rows of one of my bulk adjustment jobs (poll).")
            .RequireScope(KeycloakScopes.StockLevels.Update)
            .Produces<BulkAdjustResponse>()
            .TransformResultTo<BulkAdjustResponse>();
    }

    private static async Task<Result<BulkAdjustResponse>> GetBulkAdjustmentAsync(
        [AsParameters] Request request,
        ICurrentUser currentUser,
        IInventoryDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext.StockBulkAdjustJobs
            .AsNoTracking()
            .TagWith(nameof(GetBulkAdjustmentAsync), request.Id)
            .Where(j => j.Id == request.Id && j.RequestedBy == currentUser.Id)
            .Select(BulkAdjustResponse.Projection)
            .SingleAsResultAsync(nameof(StockBulkAdjustJob), cancellationToken);
    }
}
