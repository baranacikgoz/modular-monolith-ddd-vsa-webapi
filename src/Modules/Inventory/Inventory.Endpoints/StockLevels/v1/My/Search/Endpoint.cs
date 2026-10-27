using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Products;
using Inventory.Application.Persistence;
using Inventory.Application.StockLevels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using AllStockLevels = Inventory.Endpoints.StockLevels.v1.Search;

namespace Inventory.Endpoints.StockLevels.v1.My.Search;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder stockLevelsApiGroup)
    {
        stockLevelsApiGroup
            .MapGet("my", SearchMyStockLevelsAsync)
            .WithDescription("List the stock levels of the active stores I own, least stock first, with the same filters, paging and " +
                             "cursor as the administration list. Reads Inventory's own copy of store access, so a store that is " +
                             "not active, or one nobody has told Inventory about yet, contributes nothing; a caller who owns no " +
                             "active store gets an empty page.")
            .RequireScope(KeycloakScopes.StockLevels.ViewOwn)
            .Produces<AllStockLevels.PageResponse>()
            .TransformResultTo<AllStockLevels.PageResponse>();
    }

    private static Task<Result<AllStockLevels.PageResponse>> SearchMyStockLevelsAsync(
        [AsParameters] AllStockLevels.Request request,
        ICurrentUser currentUser,
        IInventoryDbContext dbContext,
        IInterModuleRequestClient<SearchProductsByTermRequest, SearchProductsByTermResponse> productSearchClient,
        CancellationToken cancellationToken)
    {
        return AllStockLevels.Endpoint.SearchAsync(
            dbContext.StockLevelsOfActiveStoresOwnedBy(currentUser.Id), dbContext, request, productSearchClient, cancellationToken);
    }
}
