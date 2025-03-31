using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Application.Auth;
using Common.Application.Queries.Pagination;
using Products.Application.Products.Features.Search;
using Products.Application.Stores.Features.GetStoreIdByOwnerId;
using Common.Domain.ResultMonad;
using MediatR;
using Products.Application.Products.DTOs;

namespace Products.Endpoints.Products.v1.My.Search;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myStoresApiGroup)
    {
        myStoresApiGroup
            .MapGet("my/search", SearchMyProductsAsync)
            .WithDescription("Search my store's products.")
            .MustHavePermission(CustomActions.SearchMy, CustomResources.Products)
            .Produces<PaginationResult<PaginationResult<ProductDto>>>(StatusCodes.Status200OK);
    }

    private static async Task<Result<PaginationResult<ProductDto>>> SearchMyProductsAsync(
        [AsParameters] Request request,
        [FromServices] ICurrentUser currentUser,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        => await sender
                .Send(new GetStoreIdByOwnerIdQuery(currentUser.Id), cancellationToken)
                .BindAsync(storeId => sender.Send(new SearchProductsQuery
                {
                    StoreId = storeId,
                    Name = request.Name,
                    Description = request.Description,
                    MinQuantity = request.MinQuantity,
                    MaxQuantity = request.MaxQuantity,
                    MinPrice = request.MinPrice,
                    MaxPrice = request.MaxPrice,

                    OrderBy = p => p.Id,
                    OrderByDescending = null,
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,

                }, cancellationToken));
}
