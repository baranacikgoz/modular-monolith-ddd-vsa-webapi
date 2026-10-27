using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Inventory.Application.Persistence;
using Inventory.Domain.StockLevels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Endpoints.StockLevels.v1.ChangeLowStockThreshold;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder stockLevelsApiGroup)
    {
        stockLevelsApiGroup
            .MapPut("{id}/low-stock-threshold", ChangeLowStockThresholdAsync)
            .WithDescription("Set the quantity below which the stock level raises its low stock alert. 0 turns the alert off.")
            .RequireScope(KeycloakScopes.StockLevels.Update)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> ChangeLowStockThresholdAsync(
        [AsParameters] Request request,
        IInventoryDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext.StockLevels
            .TagWith(nameof(ChangeLowStockThresholdAsync), request.Id)
            .Where(s => s.Id == request.Id)
            .SingleAsResultAsync(nameof(StockLevel), cancellationToken)
            .TapAsync(stockLevel => stockLevel.ChangeLowStockThreshold(request.Body.Threshold))
            .TapAsync(_ => dbContext.SaveChangesAsync(cancellationToken));
    }
}
