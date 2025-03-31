using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Application.ModelBinders;
using Common.Application.Auth;
using Common.Domain.ResultMonad;
using Common.Application.Extensions;
using Products.Domain.Products;
using Products.Application.Products.Features.Update;
using Products.Application.Stores.Features.GetStoreIdByOwnerId;
using MediatR;

namespace Products.Endpoints.Products.v1.My.Update;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myProductsApiGroup)
    {
        myProductsApiGroup
            .MapPut("my/{id}", UpdateMyProductAsync)
            .WithDescription("Update my product.")
            .MustHavePermission(CustomActions.UpdateMy, CustomResources.Products)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> UpdateMyProductAsync(
        [FromRoute, ModelBinder<StronglyTypedIdBinder<ProductId>>] ProductId id,
        [FromBody] Request request,
        [FromServices] ICurrentUser currentUser,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        => await sender
                .Send(new GetStoreIdByOwnerIdQuery(currentUser.Id), cancellationToken)
                .BindAsync(storeId => sender.Send(new UpdateProductCommand(
                    Id: id,
                    Name: request.Name,
                    Description: request.Description,
                    Quantity: request.Quantity,
                    Price: request.Price)
                {
                    EnsureOwnership = product => product.StoreId == storeId
                },
                    cancellationToken));
}
