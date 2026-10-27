using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Pagination;
using Common.Domain.ResultMonad;
using Common.Endpoints;
using Common.Infrastructure.Persistence.Extensions;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using Inventory.Application.Persistence;
using Inventory.Domain.StockLevels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
        IInterModuleRequestClient<GetUsersRequest, GetUsersResponse> usersClient,
        ILogger<Request> logger,
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
                    cancellationToken: cancellationToken))
            .MapAsync(page => NameActingUsersAsync(page, usersClient, logger, cancellationToken));
    }

    /// <summary>One IAM lookup for the whole page; a page whose rows have no acting user asks nobody.</summary>
    private static async Task<PaginationResponse<Response>> NameActingUsersAsync(
        PaginationResponse<Response> page,
        IInterModuleRequestClient<GetUsersRequest, GetUsersResponse> usersClient,
        ILogger logger,
        CancellationToken cancellationToken)
    {
        var names = await ActorNames.ResolveAsync(usersClient, page.Data.Select(m => m.ActingUserId), logger, cancellationToken);

        return page with
        {
            Data = page.Data
                .Select(m => m with { ActingUserName = m.ActingUserId is { } actor && names.TryGetValue(actor, out var name) ? name : null })
                .ToList()
        };
    }
}
