using Common.Application.Auth;
using Common.Application.EventHistory;
using Common.Application.Extensions;
using Common.Application.Pagination;
using Common.Domain.ResultMonad;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Products.Application.Persistence;
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.EventHistory;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder storeProductsApiGroup)
    {
        storeProductsApiGroup
            .MapGet("{id}/history", GetStoreHistoryAsync)
            .WithDescription("Get Store's history.")
            .MustHavePermission(CustomActions.Read, CustomResources.Stores)
            .Produces<PaginationResponse<EventDto>>()
            .TransformResultTo<PaginationResponse<EventDto>>();
    }

    private static async Task<Result<PaginationResponse<EventDto>>> GetStoreHistoryAsync(
        [AsParameters] Request request,
        [FromServices] IProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext
            .GetEventHistoryAsync<Store, StoreId>(
                nameof(Products),
                request.Id,
                request,
                cancellationToken);
    }
}
