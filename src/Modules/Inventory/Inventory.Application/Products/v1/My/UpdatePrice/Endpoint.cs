using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Inventory.Domain.Stores;
using Microsoft.Extensions.DependencyInjection;
using Inventory.Domain.Stores.Specs;
using Inventory.Domain.StoreProducts;
using Common.Application.ModelBinders;
using Common.Application.Auth;
using Common.Domain.ResultMonad;
using Common.Application.Extensions;
using Common.Application.Persistence;

namespace Inventory.Application.Products.v1.My.UpdatePrice;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder inventoryApiGroup)
    {
        inventoryApiGroup
            .MapPatch("{productId}/update-price", UpdateMyProductPriceAsync)
            .WithDescription("Update my product's price.")
            .MustHavePermission(CustomActions.UpdateMy, CustomResources.Products)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async ValueTask<Result> UpdateMyProductPriceAsync(
    [FromRoute, ModelBinder<StronglyTypedIdBinder<StoreProductId>>] StoreProductId productId,
    [FromBody] Request request,
    [FromServices] ICurrentUser currentUser,
    [FromServices] IRepository<Store> storeRepository,
    [FromKeyedServices(nameof(Inventory))] IUnitOfWork unitOfWork,
    CancellationToken cancellationToken)
    => await storeRepository
        .SingleOrDefaultAsResultAsync(new StoreWithProductByOwnerIdSpec(currentUser.Id, productId), cancellationToken)
        .TapAsync(store => store.UpdateProductPrice(productId, request.Price))
        .TapAsync(async _ => await unitOfWork.SaveChangesAsync(cancellationToken));
}
