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

namespace Inventory.Endpoints.StockLevels.v1.Receive;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder stockLevelsApiGroup)
    {
        stockLevelsApiGroup
            .MapPost("{id}/receive", ReceiveStockAsync)
            .WithDescription("Record stock that arrived. Appends a Received movement to the stock level's ledger.")
            .RequireScope(KeycloakScopes.StockLevels.Update)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> ReceiveStockAsync(
        [AsParameters] Request request,
        IInventoryDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext.StockLevels
            .TagWith(nameof(ReceiveStockAsync), request.Id)
            .Where(s => s.Id == request.Id)
            .SingleAsResultAsync(nameof(StockLevel), cancellationToken)
            .TapAsync(stockLevel => stockLevel.Receive(request.Body.Quantity, request.Body.Reason))
            .TapAsync(_ => dbContext.SaveChangesAsync(cancellationToken))
            .TapAsync(_ => InventoryTelemetry.StockMovementsRecorded.Add(
                1, new KeyValuePair<string, object?>("type", nameof(StockMovementType.Received))));
    }
}
