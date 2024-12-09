using Ardalis.Specification;
using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.ModelBinders;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Products.Domain.StoreProducts;
using Products.Domain.Stores;

namespace Products.Application.StoreProducts.v1.My.Delete;
internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myProductsApiGroup)
    {
        myProductsApiGroup
            .MapDelete("{id}", DeleteMyStoreProductAsync)
            .WithDescription("Delete my store product.")
            .MustHavePermission(CustomActions.DeleteMy, CustomResources.StoreProducts)
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

    private static async Task<Result> DeleteMyStoreProductAsync(
        [FromRoute, ModelBinder<StronglyTypedIdBinder<StoreProductId>>] StoreProductId id,
        [FromServices] ICurrentUser currentUser,
        [FromServices] IRepository<Store> storeRepository,
        [FromKeyedServices(nameof(Products))] IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => await storeRepository
            .SingleOrDefaultAsResultAsync(new StoreWithStoreProductByOwnerIdSpec(currentUser.Id, id), cancellationToken)
            .TapAsync(store => store.RemoveProductFromStore(id))
            .TapAsync(async _ => await unitOfWork.SaveChangesAsync(cancellationToken));
}
