using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Application.ModelBinders;
using Common.Application.Auth;
using Common.Domain.ResultMonad;
using Common.Application.Extensions;
using Products.Domain.Stores;
using Products.Domain.Products;
using Products.Application.Products.Features.Update;
using MediatR;

namespace Products.Endpoints.Products.v1.Update;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder productsApiGroup)
    {
        productsApiGroup
            .MapPut("{id}", UpdateProductAsync)
            .WithDescription("Update a product.")
            .MustHavePermission(CustomActions.Update, CustomResources.Products)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> UpdateProductAsync(
        [FromRoute, ModelBinder<StronglyTypedIdBinder<StoreId>>] ProductId id,
        [FromBody] Request request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        => await sender.Send(new UpdateProductCommand(
            Id: id,
            Name: request.Name,
            Description: request.Description,
            Quantity: request.Quantity,
            Price: request.Price),
            cancellationToken);
}
