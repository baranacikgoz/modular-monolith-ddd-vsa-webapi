using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Pagination;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Inventory.Application.Persistence;
using Inventory.Domain.StockLevels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Endpoints.StockLevels.v1.Movements;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder stockLevelsApiGroup)
    {
        stockLevelsApiGroup
            .MapGet("{id}/movements", GetMovementsAsync)
            .WithDescription("A stock level's ledger, newest first. Pass the previous page's NextCursor as 'after' to continue without an offset.")
            .RequireScope(KeycloakScopes.StockLevels.View)
            .Produces<PaginationResponse<Response>>()
            .TransformResultTo<PaginationResponse<Response>>();
    }

    private static async Task<Result<PaginationResponse<Response>>> GetMovementsAsync(
        [AsParameters] Request request,
        IInventoryDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext.StockLevels
            .AsNoTracking()
            .TagWith(nameof(GetMovementsAsync), request.Id)
            .Where(s => s.Id == request.Id)
            .Select(s => new { s.Id })
            .SingleAsResultAsync(nameof(StockLevel), cancellationToken)
            .BindAsync(stockLevel => dbContext.StockMovements
                .AsNoTracking()
                .TagWith(nameof(GetMovementsAsync), request.Id)
                .Where(m => m.StockLevelId == stockLevel.Id)
                .PaginateAsync(
                    request: request,
                    selector: m => new Response
                    {
                        Id = m.Id,
                        Type = m.Type,
                        Quantity = m.Quantity,
                        QuantityOnHandAfter = m.QuantityOnHandAfter,
                        Reason = m.Reason,
                        ReservationId = m.ReservationId,
                        ActingUserId = m.CreatedBy,
                        CreatedOn = m.CreatedOn
                    },
                    orderByDescending: m => m.CreatedOn,
                    tiebreaker: m => m.Id,
                    cancellationToken: cancellationToken));
    }
}
