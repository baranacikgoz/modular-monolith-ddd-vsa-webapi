using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Application.Auth;
using Ardalis.Specification;
using Inventory.Domain.Stores;
using Common.Application.Persistence;
using Common.Application.Pagination;
using Inventory.Application.Stores.v1.Search;
using Inventory.Domain.StoreProducts;

namespace Inventory.Application.StoreProducts.v1.Search;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myStoresApiGroup)
    {
        myStoresApiGroup
            .MapPost("search", SearchStoreProductsAsync)
            .WithDescription("Search store products.")
            .MustHavePermission(CustomActions.Search, CustomResources.StoreProducts)
            .Produces<PaginationResult<Response>>(StatusCodes.Status200OK);
    }

    private sealed class SearchStoreProductsSpec : PaginationSpec<StoreProduct, Response>
    {
        public SearchStoreProductsSpec(Request request)
            : base(request)
            => Query
                .Select(s => new Response(s.Id, s.Product.Name, s.Product.Description, s.Quantity, s.Price))
                .Include(s => s.Product)
                .Search(s => s.Product.Name, $"%{request.Name!}%", condition: request.Name is not null)
                .Search(s => s.Product.Description, $"%{request.Description!}%", condition: request.Description is not null)
                .Where(s => s.Quantity >= request.MinQuantity, condition: request.MinQuantity is not null)
                .Where(s => s.Quantity <= request.MaxQuantity, condition: request.MaxQuantity is not null)
                .Where(s => s.Price >= request.MinPrice, condition: request.MinPrice is not null)
                .Where(s => s.Price <= request.MaxPrice, condition: request.MaxPrice is not null);
    }

    private static async Task<PaginationResult<Response>> SearchStoreProductsAsync(
        [FromBody] Request request,
        [FromServices] IRepository<StoreProduct> repository,
        CancellationToken cancellationToken)
        => await repository
            .PaginateAsync(new SearchStoreProductsSpec(request), cancellationToken);
}
