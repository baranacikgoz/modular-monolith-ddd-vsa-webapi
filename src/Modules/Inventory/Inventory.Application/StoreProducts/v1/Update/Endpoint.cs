using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Inventory.Domain.Stores;
using Microsoft.Extensions.DependencyInjection;
using Inventory.Domain.StoreProducts;
using Common.Application.ModelBinders;
using Common.Application.Auth;
using Common.Domain.ResultMonad;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Ardalis.Specification;
using Common.Domain.StronglyTypedIds;

namespace Inventory.Application.StoreProducts.v1.Update;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myProductsApiGroup)
    {
        myProductsApiGroup
            .MapPut("", UpdateProductAsync)
            .WithDescription("Update product.")
            .MustHavePermission(CustomActions.Update, CustomResources.StoreProducts)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
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
        [FromBody] Request request,
        [FromServices] IRepository<Store> storeRepository,
        [FromKeyedServices(nameof(Inventory))] IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => await storeRepository
            .SingleOrDefaultAsResultAsync(new StoreWithStoreProductByIdSpec(request.StoreId, request.StoreProductId), cancellationToken)
            .TapAsync(store => store.UpdateProduct(request.StoreProductId, request.Quantity, request.Price))
            .TapAsync(async _ => await unitOfWork.SaveChangesAsync(cancellationToken));
}
