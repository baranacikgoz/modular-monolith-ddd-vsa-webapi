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
using Products.Application.Stores.Features.AddProduct;
using Products.Domain.Products;
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.AddProduct;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder storesApiGroup)
    {
        storesApiGroup
            .MapPost("{id}/products", AddProductAsync)
            .WithDescription("Add product to a store.")
            .MustHavePermission(CustomActions.Create, CustomResources.Products)
            .Produces(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> AddProductAsync(
        [FromRoute, ModelBinder<StronglyTypedIdBinder<StoreId>>] StoreId id,
        [FromBody] Request request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        => await sender
                .Send(new AddProductCommand(
                    StoreId: id,
                    ProductTemplateId: request.ProductTemplateId,
                    Name: request.Name,
                    Description: request.Description,
                    Quantity: request.Quantity,
                    Price: request.Price),
                    cancellationToken)
                .MapAsync(productId => new Response { Id = productId });
}
