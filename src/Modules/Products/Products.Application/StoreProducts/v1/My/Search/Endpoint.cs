using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Application.Auth;
using Ardalis.Specification;
using Common.Application.Persistence;
using Common.Application.Pagination;
using Common.Domain.StronglyTypedIds;
using Products.Domain.StoreProducts;

namespace Products.Application.StoreProducts.v1.My.Search;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myStoresApiGroup)
    {
        myStoresApiGroup
            .MapGet("search", SearchMyStoreProductsAsync)
            .WithDescription("Search my store's StoreProducts.")
            .MustHavePermission(CustomActions.Search, CustomResources.StoreProducts)
            .Produces<PaginationResult<Response>>(StatusCodes.Status200OK);
    }

    private sealed class SearchMyStoresStoreProductsSpec : PaginationSpec<StoreProduct, Response>
    {
        public SearchMyStoresStoreProductsSpec(Request request, ApplicationUserId ownerId)
            : base(request)
            => Query
                .Select(sp => new Response
                {
                    Id = sp.Id,
                    Name = sp.Product.Name,
                    Description = sp.Product.Description,
                    Quantity = sp.Quantity,
                    Price = sp.Price,
                    CreatedBy = sp.CreatedBy,
                    CreatedOn = sp.CreatedOn,
                    LastModifiedBy = sp.LastModifiedBy,
                    LastModifiedOn = sp.LastModifiedOn
                })
                .Include(s => s.Product)
                .Search(s => s.Product.Name, $"%{request.Name!}%", condition: request.Name is not null)
                .Search(s => s.Product.Description, $"%{request.Description!}%", condition: request.Description is not null)
                .Where(s => s.Store.OwnerId == ownerId)
                .Where(s => s.Quantity >= request.MinQuantity, condition: request.MinQuantity is not null)
                .Where(s => s.Quantity <= request.MaxQuantity, condition: request.MaxQuantity is not null)
                .Where(s => s.Price >= request.MinPrice, condition: request.MinPrice is not null)
                .Where(s => s.Price <= request.MaxPrice, condition: request.MaxPrice is not null);
    }

    private static async Task<PaginationResult<Response>> SearchMyStoreProductsAsync(
        [AsParameters] Request request,
        [FromServices] ICurrentUser currentUser,
        [FromServices] IRepository<StoreProduct> repository,
        CancellationToken cancellationToken)
        => await repository
            .PaginateAsync(new SearchMyStoresStoreProductsSpec(request, currentUser.Id), cancellationToken);
}
