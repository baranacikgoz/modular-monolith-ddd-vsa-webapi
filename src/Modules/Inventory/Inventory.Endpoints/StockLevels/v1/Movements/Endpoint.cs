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

namespace Inventory.Endpoints.StockLevels.v1.Movements;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder stockLevelsApiGroup)
    {
        stockLevelsApiGroup
            .MapGet("{id}/movements", GetMovementsAsync)
            .WithDescription("A stock level's ledger, newest first. Pass the previous page's NextCursor as 'after' to continue without an offset. " +
                             "Administration only; a store owner reads the ledger of their own stock level through /my/{id}/movements.")
            .RequireScope(KeycloakScopes.StockLevels.ViewAll)
            .Produces<PaginationResponse<Response>>()
            .TransformResultTo<PaginationResponse<Response>>();
    }

    private static Task<Result<PaginationResponse<Response>>> GetMovementsAsync(
        [AsParameters] Request request,
        IInventoryDbContext dbContext,
        IInterModuleRequestClient<GetUsersRequest, GetUsersResponse> usersClient,
        CancellationToken cancellationToken)
    {
        return PageMovementsAsync(dbContext.StockLevels, dbContext, request, usersClient, cancellationToken);
    }

    /// <summary>
    ///     The ledger of the stock level <paramref name="request" /> names, looked up inside <paramref name="stockLevels" />:
    ///     every stock level for the administration route, the caller's own for the store owner's route, where one outside
    ///     the set is simply not found.
    /// </summary>
    internal static async Task<Result<PaginationResponse<Response>>> PageMovementsAsync(
        IQueryable<StockLevel> stockLevels,
        IInventoryDbContext dbContext,
        Request request,
        IInterModuleRequestClient<GetUsersRequest, GetUsersResponse> usersClient,
        CancellationToken cancellationToken)
    {
        return await stockLevels
            .AsNoTracking()
            .TagWith(nameof(PageMovementsAsync), request.Id)
            .Where(s => s.Id == request.Id)
            .Select(s => new { s.Id })
            .SingleAsResultAsync(nameof(StockLevel), cancellationToken)
            .BindAsync(stockLevel => dbContext.StockMovements
                .AsNoTracking()
                .TagWith(nameof(PageMovementsAsync), request.Id)
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
            .MapAsync(page => NameActingUsersAsync(page, usersClient, cancellationToken));
    }

    /// <summary>One IAM lookup for the whole page; a page whose rows have no acting user asks nobody.</summary>
    private static async Task<PaginationResponse<Response>> NameActingUsersAsync(
        PaginationResponse<Response> page,
        IInterModuleRequestClient<GetUsersRequest, GetUsersResponse> usersClient,
        CancellationToken cancellationToken)
    {
        var names = await ActorNames.ResolveAsync(usersClient, page.Data.Select(m => m.ActingUserId), cancellationToken);

        return page with
        {
            Data = page.Data
                .Select(m => m with { ActingUserName = m.ActingUserId is { } actor && names.TryGetValue(actor, out var name) ? name : null })
                .ToList()
        };
    }
}
