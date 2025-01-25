using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Products.Application.Stores.Features.AddProduct;
using Products.Application.Stores.Features.GetStoreIdByOwnerId;

namespace Products.Endpoints.Stores.v1.My.AddProduct;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder storesApiGroup)
    {
        storesApiGroup
            .MapPost("my/products", AddProductAsync)
            .WithDescription("Add product to my store.")
            .MustHavePermission(CustomActions.CreateMy, CustomResources.Products)
            .Produces(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> AddProductAsync(
        [FromBody] Request request,
        [FromServices] ICurrentUser currentUser,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        => await sender
                .Send(new GetStoreIdByOwnerIdQuery(currentUser.Id), cancellationToken)
                .BindAsync(storeId => sender.Send(new AddProductCommand(
                    StoreId: storeId,
                    ProductTemplateId: request.ProductTemplateId,
                    Name: request.Name,
                    Description: request.Description,
                    Quantity: request.Quantity,
                    Price: request.Price),
                    cancellationToken))
                .MapAsync(productId => new Response { Id = productId });
}
