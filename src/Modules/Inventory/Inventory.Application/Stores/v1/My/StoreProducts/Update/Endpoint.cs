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

namespace Inventory.Application.Stores.v1.My.StoreProducts.Update;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myProductsApiGroup)
    {
        myProductsApiGroup
            .MapPut("{id}", UpdateMyProductAsync)
            .WithDescription("Update my product.")
            .MustHavePermission(CustomActions.UpdateMy, CustomResources.StoreProducts)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private sealed class StoreWithProductByOwnerIdSpec : SingleResultSpecification<Store>
    {
        public StoreWithProductByOwnerIdSpec(ApplicationUserId ownerId, StoreProductId productId)
            => Query
                .Where(s => s.OwnerId == ownerId)
                .Include(s => s.StoreProducts
                                .Where(p => p.Id == productId));
    }

    private static async Task<Result> UpdateMyProductAsync(
        [FromRoute, ModelBinder<StronglyTypedIdBinder<StoreProductId>>] StoreProductId id,
        [FromBody] Request request,
        [FromServices] ICurrentUser currentUser,
        [FromServices] IRepository<Store> storeRepository,
        [FromKeyedServices(nameof(Inventory))] IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => await storeRepository
            .SingleOrDefaultAsResultAsync(new StoreWithProductByOwnerIdSpec(currentUser.Id, id), cancellationToken)
            .TapAsync(store => store.UpdateProduct(id, request.Quantity, request.Price))
            .TapAsync(async _ => await unitOfWork.SaveChangesAsync(cancellationToken));
}
