using Common.Application.Auth;
using Common.Application.CQS;
using Common.Application.Extensions;
using Common.Application.ModelBinders;
using Common.Domain.ResultMonad;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Products.Application.Stores.Features.GetStoreIdByOwnerId;
using Products.Application.Stores.Features.RemoveProduct;
using Products.Domain.Products;
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.My.RemoveProduct;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder storesApiGroup)
    {
        storesApiGroup
            .MapDelete("products/{productId}", RemoveProductAsync)
            .WithDescription("Remove product from my store.")
            .MustHavePermission(CustomActions.DeleteMy, CustomResources.Products)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> RemoveProductAsync(
        [FromRoute, ModelBinder<StronglyTypedIdBinder<ProductId>>] ProductId productId,
        [FromServices] ICurrentUser currentUser,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        => await sender
            .Send(new GetStoreIdByOwnerIdQuery(currentUser.Id), cancellationToken)
            .BindAsync(storeId => sender.Send(new RemoveProductCommand(storeId, productId), cancellationToken));
}
