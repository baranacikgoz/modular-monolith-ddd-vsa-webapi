using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Pagination;
using Common.Domain.ResultMonad;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using Inventory.Application.Persistence;
using Inventory.Application.StockLevels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using AllMovements = Inventory.Endpoints.StockLevels.v1.Movements;

namespace Inventory.Endpoints.StockLevels.v1.My.Movements;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder stockLevelsApiGroup)
    {
        stockLevelsApiGroup
            .MapGet("my/{id}/movements", GetMyMovementsAsync)
            .WithDescription("The ledger of one of my stock levels, newest first, with the same paging and cursor as the " +
                             "administration ledger. A stock level that belongs to no active store of mine is answered as not found.")
            .RequireScope(KeycloakScopes.StockLevels.ViewOwn)
            .Produces<PaginationResponse<AllMovements.Response>>()
            .TransformResultTo<PaginationResponse<AllMovements.Response>>();
    }

    private static Task<Result<PaginationResponse<AllMovements.Response>>> GetMyMovementsAsync(
        [AsParameters] AllMovements.Request request,
        ICurrentUser currentUser,
        IInventoryDbContext dbContext,
        IInterModuleRequestClient<GetUsersRequest, GetUsersResponse> usersClient,
        ILogger<AllMovements.Request> logger,
        CancellationToken cancellationToken)
    {
        return AllMovements.Endpoint.PageMovementsAsync(
            dbContext.StockLevelsOfActiveStoresOwnedBy(currentUser.Id), dbContext, request, usersClient, logger, cancellationToken);
    }
}
