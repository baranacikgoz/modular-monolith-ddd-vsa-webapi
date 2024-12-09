using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Common.Application.ModelBinders;
using Common.Application.Auth;
using Common.Domain.ResultMonad;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Ardalis.Specification;
using Products.Domain.Stores;
using Products.Domain.StoreProducts;

namespace Products.Application.StoreProducts.v1.Update;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myProductsApiGroup)
    {
        myProductsApiGroup
            .MapPut("{id}", UpdateProductAsync)
            .WithDescription("Update a store product.")
            .MustHavePermission(CustomActions.Update, CustomResources.StoreProducts)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private sealed class StoreIdByStoreProductIdSpec : SingleResultSpecification<StoreProduct, StoreId>
    {
        public StoreIdByStoreProductIdSpec(StoreProductId storeProductId)
            => Query
                .Select(sp => sp.StoreId)
                .Where(sp => sp.Id == storeProductId);
    }

    private sealed class StoreWithStoreProductByIdSpec : SingleResultSpecification<Store>
    {
        public StoreWithStoreProductByIdSpec(StoreId storeId, StoreProductId storeProductId)
            => Query
                .Where(s => s.Id == storeId)
                .Include(s => s.StoreProducts
                                .Where(p => p.Id == storeProductId));
    }

    private static async Task<Result> UpdateProductAsync(
        [FromRoute, ModelBinder<StronglyTypedIdBinder<StoreId>>] StoreProductId id,
        [FromBody] Request request,
        [FromServices] IRepository<StoreProduct> storeProductRepository,
        [FromServices] IRepository<Store> storeRepository,
        [FromKeyedServices(nameof(Products))] IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => await storeProductRepository
            .SingleOrDefaultAsResultAsync(new StoreIdByStoreProductIdSpec(id), cancellationToken)
            .BindAsync(async storeId => await storeRepository.SingleOrDefaultAsResultAsync(new StoreWithStoreProductByIdSpec(storeId, id), cancellationToken)
            .TapAsync(store => store.UpdateProduct(id, request.Quantity, request.Price))
            .TapAsync(async _ => await unitOfWork.SaveChangesAsync(cancellationToken)));
}
