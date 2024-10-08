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

namespace Inventory.Application.StoreProducts.v1.My.Update;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myProductsApiGroup)
    {
        myProductsApiGroup
            .MapPut("{id}", UpdateMyStoreProductAsync)
            .WithDescription("Update my store product.")
            .MustHavePermission(CustomActions.UpdateMy, CustomResources.StoreProducts)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private sealed class StoreWithStoreProductByOwnerIdSpec : SingleResultSpecification<Store>
    {
        public StoreWithStoreProductByOwnerIdSpec(ApplicationUserId ownerId, StoreProductId productId)
            => Query
                .Where(s => s.OwnerId == ownerId)
                .Include(s => s.StoreProducts
                                .Where(p => p.Id == productId));
    }

    private static async Task<Result> UpdateMyStoreProductAsync(
        [FromRoute, ModelBinder<StronglyTypedIdBinder<StoreProductId>>] StoreProductId id,
        [FromBody] Request request,
        [FromServices] ICurrentUser currentUser,
        [FromServices] IRepository<Store> storeRepository,
        [FromKeyedServices(nameof(Inventory))] IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => await storeRepository
            .SingleOrDefaultAsResultAsync(new StoreWithStoreProductByOwnerIdSpec(currentUser.Id, id), cancellationToken)
            .TapAsync(store => store.UpdateProduct(id, request.Quantity, request.Price))
            .TapAsync(async _ => await unitOfWork.SaveChangesAsync(cancellationToken));
}
