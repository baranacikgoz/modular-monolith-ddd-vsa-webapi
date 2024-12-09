using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Application.Auth;
using Ardalis.Specification;
using Common.Application.Persistence;
using Common.Application.Pagination;
using Products.Domain.Products;

namespace Products.Application.Products.v1.Search;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myProductsApiGroup)
    {
        myProductsApiGroup
            .MapPost("search", SearchProductsAsync)
            .WithDescription("Search products.")
            .MustHavePermission(CustomActions.Search, CustomResources.Products)
            .Produces<PaginationResult<Response>>(StatusCodes.Status200OK);
    }

    private sealed class SearchProductsSpec : PaginationSpec<Product, Response>
    {
        public SearchProductsSpec(Request request)
            : base(request)
            => Query
                .Select(p => new Response
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CreatedBy = p.CreatedBy,
                    CreatedOn = p.CreatedOn,
                    LastModifiedBy = p.LastModifiedBy,
                    LastModifiedOn = p.LastModifiedOn
                })
                .Search(s => s.Name, $"%{request.Name!}%", condition: request.Name is not null)
                .Search(s => s.Description, $"%{request.Description!}%", condition: request.Description is not null);
    }

    private static async Task<PaginationResult<Response>> SearchProductsAsync(
        [FromBody] Request request,
        [FromServices] IRepository<Product> repository,
        CancellationToken cancellationToken)
        => await repository
            .PaginateAsync(new SearchProductsSpec(request), cancellationToken);
}
