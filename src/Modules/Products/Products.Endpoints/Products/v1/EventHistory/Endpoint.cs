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
using Products.Domain.Products;

namespace Products.Endpoints.Products.v1.EventHistory;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder productsApiGroup)
    {
        productsApiGroup
            .MapGet("{id}/event-history", GetProductEventHistoryAsync)
            .WithDescription("Get Product.")
            .MustHavePermission(CustomActions.Read, CustomResources.Products)
            .Produces<PaginationResponse<EventDto>>()
            .TransformResultTo<PaginationResponse<EventDto>>();
    }

    private static async Task<Result<PaginationResponse<EventDto>>> GetProductEventHistoryAsync(
        [AsParameters] Request request,
        [FromServices] IProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext
            .GetEventHistoryAsync<Product, ProductId>(
                nameof(Products),
                request.Id,
                request,
                cancellationToken);
    }
}
