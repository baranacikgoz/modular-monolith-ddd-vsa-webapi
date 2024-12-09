using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Application.Auth;
using Ardalis.Specification;
using Common.Application.Persistence;
using Common.Application.Pagination;
using Products.Domain.Stores;

namespace Products.Application.Stores.v1.Search;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myStoresApiGroup)
    {
        myStoresApiGroup
            .MapPost("search", SearchStoresAsync)
            .WithDescription("Search stores.")
            .MustHavePermission(CustomActions.Search, CustomResources.Stores)
            .Produces<PaginationResult<Response>>(StatusCodes.Status200OK);
    }

    private sealed class SearchStoresSpec : PaginationSpec<Store, Response>
    {
        public SearchStoresSpec(Request request)
            : base(request)
            => Query
                .Select(s => new Response
                {
                    Id = s.Id,
                    OwnerId = s.OwnerId,
                    Name = s.Name,
                    Description = s.Description,
                    LogoUrl = s.LogoUrl,
                    ProductCount = s.StoreProducts.Count,
                    CreatedBy = s.CreatedBy,
                    CreatedOn = s.CreatedOn,
                    LastModifiedBy = s.LastModifiedBy,
                    LastModifiedOn = s.LastModifiedOn
                })
                .Search(s => s.Name, $"%{request.Name!}%", condition: request.Name is not null)
                .Search(s => s.Description, $"%{request.Description!}%", condition: request.Description is not null);
    }

    private static async Task<PaginationResult<Response>> SearchStoresAsync(
        [FromBody] Request request,
        [FromServices] IRepository<Store> repository,
        CancellationToken cancellationToken)
        => await repository
            .PaginateAsync(new SearchStoresSpec(request), cancellationToken);
}
