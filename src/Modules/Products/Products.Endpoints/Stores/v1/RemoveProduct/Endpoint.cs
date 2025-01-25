using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.ModelBinders;
using Common.Domain.ResultMonad;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Products.Application.Stores.Features.RemoveProduct;
using Products.Domain.Products;
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.RemoveProduct;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder storesApiGroup)
    {
        storesApiGroup
            .MapDelete("{id}/products/{productId}", RemoveProductAsync)
            .WithDescription("Remove product from a store.")
            .MustHavePermission(CustomActions.Delete, CustomResources.Products)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> RemoveProductAsync(
        [FromRoute, ModelBinder<StronglyTypedIdBinder<StoreId>>] StoreId id,
        [FromRoute, ModelBinder<StronglyTypedIdBinder<ProductId>>] ProductId productId,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        => await sender.Send(new RemoveProductCommand(
                StoreId: id,
                ProductId: productId),
            cancellationToken);
}
