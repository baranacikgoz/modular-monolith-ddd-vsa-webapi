using Common.Application.Auth;
using Common.Application.CQS;
using Common.Application.DTOs;
using Common.Application.Extensions;
using Common.Application.ModelBinders;
using Common.Application.Queries.Pagination;
using Common.Domain.ResultMonad;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Products.Application.Stores.Features.History;
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.History;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder storeProductsApiGroup)
    {
        storeProductsApiGroup
            .MapGet("{id}/history", GetStoreHistoryAsync)
            .WithDescription("Get Store's history.")
            .MustHavePermission(CustomActions.Read, CustomResources.Stores)
            .Produces<PaginationResult<EventDto>>(StatusCodes.Status200OK)
            .TransformResultTo<PaginationResult<EventDto>>();
    }

    private static async Task<Result<PaginationResult<EventDto>>> GetStoreHistoryAsync(
        [FromRoute, ModelBinder<StronglyTypedIdBinder<StoreId>>] StoreId id,
        [AsParameters] Request request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        => await sender.Send(new GetStoreEventHistoryQuery
        {
            AggregateId = id,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        }, cancellationToken);
}
