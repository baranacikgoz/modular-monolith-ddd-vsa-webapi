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
using Common.Domain.StronglyTypedIds;

namespace Inventory.Application.Stores.v1.My.StoreProducts.Search;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myStoresApiGroup)
    {
        myStoresApiGroup
            .MapPost("search", SearchMyStoresStoreProductsAsync)
            .WithDescription("Search my store's StoreProducts.")
            .MustHavePermission(CustomActions.Search, CustomResources.StoreProducts)
            .Produces<PaginationResult<Response>>(StatusCodes.Status200OK);
    }

    private sealed class SearchMyStoresStoreProductsSpec : PaginationSpec<StoreProduct, Response>
    {
        public SearchMyStoresStoreProductsSpec(Request request, ApplicationUserId ownerId)
            : base(request)
            => Query
                .Select(s => new Response(s.Id, s.Product.Name, s.Product.Description, s.Quantity, s.Price))
                .Include(s => s.Product)
                .Search(s => s.Product.Name, $"%{request.Name!}%", condition: request.Name is not null)
                .Search(s => s.Product.Description, $"%{request.Description!}%", condition: request.Description is not null)
                .Where(s => s.Store.OwnerId == ownerId)
                .Where(s => s.Quantity >= request.MinQuantity, condition: request.MinQuantity is not null)
                .Where(s => s.Quantity <= request.MaxQuantity, condition: request.MaxQuantity is not null)
                .Where(s => s.Price >= request.MinPrice, condition: request.MinPrice is not null)
                .Where(s => s.Price <= request.MaxPrice, condition: request.MaxPrice is not null);
    }

    private static async Task<PaginationResult<Response>> SearchMyStoresStoreProductsAsync(
        [FromBody] Request request,
        [FromServices] ICurrentUser currentUser,
        [FromServices] IRepository<StoreProduct> repository,
        CancellationToken cancellationToken)
        => await repository
            .PaginateAsync(new SearchMyStoresStoreProductsSpec(request, currentUser.Id), cancellationToken);
}
