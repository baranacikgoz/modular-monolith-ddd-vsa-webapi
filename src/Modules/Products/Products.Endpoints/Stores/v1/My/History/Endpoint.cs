using Common.Application.Auth;
using Common.Application.DTOs;
using Common.Application.Extensions;
using Common.Application.Queries.Pagination;
using Common.Domain.ResultMonad;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Products.Application.Stores.Features.GetStoreIdByOwnerId;
using Products.Application.Stores.Features.History;

namespace Products.Endpoints.Stores.v1.My.History;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder storeProductsApiGroup)
    {
        storeProductsApiGroup
            .MapGet("my/history", GetStoreHistoryAsync)
            .WithDescription("Get my store's history.")
            .MustHavePermission(CustomActions.ReadMy, CustomResources.Stores)
            .Produces<PaginationResult<EventDto>>(StatusCodes.Status200OK)
            .TransformResultTo<PaginationResult<EventDto>>();
    }

    private static async Task<Result<PaginationResult<EventDto>>> GetStoreHistoryAsync(
        [AsParameters] Request request,
        [FromServices] ICurrentUser currentUser,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        => await sender
            .Send(new GetStoreIdByOwnerIdQuery(currentUser.Id), cancellationToken)
            .BindAsync(storeId => sender.Send(new GetStoreEventHistoryQuery
            {
                AggregateId = storeId,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            }, cancellationToken));
}
