using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Application.Auth;
using Ardalis.Specification;
using Common.Application.Persistence;
using Products.Domain.Products;
using Common.Application.Queries.Pagination;
using Common.Application.CQS;
using Products.Application.Products.Features.Search;
using Products.Application.Products.DTOs;
using Common.Domain.ResultMonad;
using Common.Application.Extensions;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Products.Endpoints.Products.v1.Search;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder productsApiGroup)
    {
        productsApiGroup
            .MapGet("search", SearchStoreProductsAsync)
            .WithDescription("Search products.")
            .MustHavePermission(CustomActions.Search, CustomResources.Products)
            .Produces<PaginationResult<ProductDto>>(StatusCodes.Status200OK)
            .TransformResultTo<PaginationResult<ProductDto>>();
    }

    private static async Task<Result<PaginationResult<ProductDto>>> SearchStoreProductsAsync(
        [AsParameters] Request request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        => await sender.Send(new SearchProductsQuery
        {
            Name = request.Name,
            Description = request.Description,
            MinQuantity = request.MinQuantity,
            MaxQuantity = request.MaxQuantity,
            MinPrice = request.MinPrice,
            MaxPrice = request.MaxPrice,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        }, cancellationToken);
}
