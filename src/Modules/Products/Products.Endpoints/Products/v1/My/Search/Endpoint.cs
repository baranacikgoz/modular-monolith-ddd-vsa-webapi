using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Pagination;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Microsoft.EntityFrameworkCore;
using Products.Application.Persistence;

namespace Products.Endpoints.Products.v1.My.Search;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myStoresApiGroup)
    {
        myStoresApiGroup
            .MapGet("my/search", SearchMyProductsAsync)
            .WithDescription("Search my store's products.")
            .MustHavePermission(CustomActions.SearchMy, CustomResources.Products)
            .Produces<PaginationResponse<Response>>(StatusCodes.Status200OK)
            .TransformResultTo<PaginationResponse<Response>>();
    }

    private static async Task<Result<PaginationResponse<Response>>> SearchMyProductsAsync(
        [AsParameters] Request request,
        [FromServices] ICurrentUser currentUser,
        [FromServices] IProductsDbContext dbContext,
        CancellationToken cancellationToken)
        => await dbContext
            .Products
            .TagWith(nameof(SearchMyProductsAsync))
            .Where(p => p.Store.OwnerId == currentUser.Id)
            .WhereIf(p => EF.Functions.ILike(p.Name, $"%{request.Name}%"), condition: !string.IsNullOrWhiteSpace(request.Name))
            .WhereIf(p => EF.Functions.ILike(p.Description, $"%{request.Description}%"), condition: !string.IsNullOrWhiteSpace(request.Description))
            .WhereIf(p => p.Quantity >= request.MinQuantity!, condition: request.MinQuantity is not null)
            .WhereIf(p => p.Quantity <= request.MaxQuantity!, condition: request.MaxQuantity is not null)
            .WhereIf(p => p.Price >= request.MinPrice!, condition: request.MinPrice is not null)
            .WhereIf(p => p.Price <= request.MaxPrice!, condition: request.MaxPrice is not null)
            .PaginateAsync(
                request: request,
                selector: p => new Response
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Quantity = p.Quantity,
                    Price = p.Price,
                    CreatedBy = p.CreatedBy,
                    CreatedOn = p.CreatedOn,
                    LastModifiedBy = p.LastModifiedBy,
                    LastModifiedOn = p.LastModifiedOn
                },
                orderByDescending: p => p.CreatedOn,
                cancellationToken: cancellationToken);
}
