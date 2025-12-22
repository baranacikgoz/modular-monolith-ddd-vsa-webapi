using Common.Application.Auth;
using Common.Application.EventHistory;
using Common.Application.Extensions;
using Common.Application.Pagination;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Products.Application.Persistence;
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.My.History;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder v1StoresApiGroup)
    {
        v1StoresApiGroup
            .MapGet("my/history", GetMyStoreHistoryAsync)
            .WithDescription("Get my store's history.")
            .MustHavePermission(CustomActions.ReadMy, CustomResources.Stores)
            .Produces<PaginationResponse<EventDto>>(StatusCodes.Status200OK)
            .TransformResultTo<PaginationResponse<EventDto>>();
    }

    private static async Task<Result<PaginationResponse<EventDto>>> GetMyStoreHistoryAsync(
        [AsParameters] Request request,
        [FromServices] ICurrentUser currentUser,
        [FromServices] IProductsDbContext dbContext,
        CancellationToken cancellationToken)
        => await dbContext
            .Stores
            .TagWith(nameof(GetMyStoreHistoryAsync), currentUser.Id)
            .AsNoTracking()
            .Where(x => x.OwnerId == currentUser.Id)
            .Select(x => x.Id)
            .SingleAsResultAsync(cancellationToken)
            .BindAsync(async id => await dbContext
                .GetEventHistoryAsync<Store, StoreId>(
                    moduleName: nameof(Products),
                    id: id,
                    request: request,
                    cancellationToken: cancellationToken));

}
