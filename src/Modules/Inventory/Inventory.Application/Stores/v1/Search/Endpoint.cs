using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Application.Auth;
using Common.Domain.ResultMonad;
using Common.Application.Extensions;
using Common.Domain.StronglyTypedIds;
using Common.Application.ModelBinders;
using Ardalis.Specification;
using Inventory.Domain.Stores;
using Microsoft.Extensions.DependencyInjection;
using Common.Application.Persistence;
using Common.Application.Pagination;

namespace Inventory.Application.Stores.v1.Search;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myStoresApiGroup)
    {
        myStoresApiGroup
            .MapPost("search", SearchStoresAsync)
            .WithDescription("Search stores.")
            .MustHavePermission(CustomActions.Search, CustomResources.Stores)
            .Produces<Response>(StatusCodes.Status200OK);
    }

    private sealed class SearchStoresSpec : PaginationSpec<Store, Response>
    {
        public SearchStoresSpec(Request request)
            : base(request)
            => Query
                .Select(s => new Response(s.Id, s.Name, s.Description, s.LogoUrl, s.StoreProducts.Count))
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
