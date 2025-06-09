using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Application.Auth;
using Common.Domain.ResultMonad;
using Common.Application.Extensions;
using Common.Application.Pagination;
using Common.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Products.Application.Persistence;

namespace Products.Endpoints.Products.v1.Search;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder productsApiGroup)
    {
        productsApiGroup
            .MapGet("search", SearchStoreProductsAsync)
            .WithDescription("Search products.")
            .MustHavePermission(CustomActions.Search, CustomResources.Products)
            .Produces<PaginationResponse<Response>>(StatusCodes.Status200OK)
            .TransformResultTo<PaginationResponse<Response>>();
    }

    private static async Task<Result<PaginationResponse<Response>>> SearchStoreProductsAsync(
        [AsParameters] Request request,
        [FromServices] IProductsDbContext dbContext,
        CancellationToken cancellationToken)
        => await dbContext
            .Products
            .TagWith(nameof(SearchStoreProductsAsync))
            .WhereIf(p => p.StoreId == request.StoreId, condition: request.StoreId is not null)
            .WhereIf(p => p.Store.OwnerId == request.OwnerId, condition: request.OwnerId is not null)
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
