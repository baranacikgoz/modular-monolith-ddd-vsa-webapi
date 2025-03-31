using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Application.Auth;
using Common.Application.Queries.Pagination;
using Products.Application.Stores.DTOs;
using Products.Application.Stores.Features.Search;
using Common.Domain.ResultMonad;
using Common.Application.Extensions;
using MediatR;

namespace Products.Endpoints.Stores.v1.Search;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder storesApiGroup)
    {
        storesApiGroup
            .MapGet("search", SearchStoresAsync)
            .WithDescription("Search stores.")
            .MustHavePermission(CustomActions.Search, CustomResources.Stores)
            .Produces<PaginationResult<StoreDto>>(StatusCodes.Status200OK)
            .TransformResultTo<PaginationResult<StoreDto>>();
    }

    private static async Task<Result<PaginationResult<StoreDto>>> SearchStoresAsync(
        [AsParameters] Request request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        => await sender.Send(new SearchStoresQuery
        {
            Name = request.Name,
            Description = request.Description,
            Address = request.Address,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            OrderBy = null,
            OrderByDescending = s => s.CreatedOn
        },
        cancellationToken);
}
