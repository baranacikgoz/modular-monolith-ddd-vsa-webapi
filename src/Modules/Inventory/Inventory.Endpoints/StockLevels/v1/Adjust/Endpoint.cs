using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Inventory.Application.Persistence;
using Inventory.Domain.StockLevels;
using Inventory.Infrastructure.Telemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Endpoints.StockLevels.v1.Adjust;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder stockLevelsApiGroup)
    {
        stockLevelsApiGroup
            .MapPost("{id}/adjust", AdjustStockAsync)
            .WithDescription("Correct the on-hand quantity by a signed amount, with a reason. Appends an Adjusted movement to the stock level's ledger.")
            .RequireScope(KeycloakScopes.StockLevels.Update)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> AdjustStockAsync(
        [AsParameters] Request request,
        IInventoryDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext.StockLevels
            .TagWith(nameof(AdjustStockAsync), request.Id)
            .Where(s => s.Id == request.Id)
            .SingleAsResultAsync(nameof(StockLevel), cancellationToken)
            .TapAsync(stockLevel => stockLevel.Adjust(request.Body.Quantity, request.Body.Reason))
            .TapAsync(_ => dbContext.SaveChangesAsync(cancellationToken))
            .TapAsync(_ => InventoryTelemetry.StockMovementsRecorded.Add(
                1, new KeyValuePair<string, object?>("type", nameof(StockMovementType.Adjusted))));
    }
}
