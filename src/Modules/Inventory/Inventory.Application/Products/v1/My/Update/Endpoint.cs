using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Inventory.Domain.Stores;
using Microsoft.Extensions.DependencyInjection;
using Inventory.Application.Stores.Specs;
using Inventory.Domain.StoreProducts;
using Common.Application.ModelBinders;
using Common.Application.Auth;
using Common.Domain.ResultMonad;
using Common.Application.Extensions;
using Common.Application.Persistence;

namespace Inventory.Application.Products.v1.My.Update;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myProductsApiGroup)
    {
        myProductsApiGroup
            .MapPut("{productId}", UpdateMyProductAsync)
            .WithDescription("Update my product.")
            .MustHavePermission(CustomActions.UpdateMy, CustomResources.StoreProducts)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> UpdateMyProductAsync(
        [FromRoute, ModelBinder<StronglyTypedIdBinder<StoreProductId>>] StoreProductId productId,
        [FromBody] Request request,
        [FromServices] ICurrentUser currentUser,
        [FromServices] IRepository<Store> storeRepository,
        [FromKeyedServices(nameof(Inventory))] IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => await storeRepository
            .SingleOrDefaultAsResultAsync(new StoreWithProductByOwnerIdSpec(currentUser.Id, productId), cancellationToken)
            .TapAsync(store => store.UpdateProduct(productId, request.Quantity, request.Price))
            .TapAsync(async _ => await unitOfWork.SaveChangesAsync(cancellationToken));
}
