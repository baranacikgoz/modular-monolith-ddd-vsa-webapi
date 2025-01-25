using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.ModelBinders;
using Common.Domain.ResultMonad;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Products.Application.Products.Features.GetById;
using Products.Domain.Products;

namespace Products.Endpoints.Products.v1.Get;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myProductsApiGroup)
    {
        myProductsApiGroup
            .MapGet("{id}", GetProductAsync)
            .WithDescription("Get Product.")
            .MustHavePermission(CustomActions.Read, CustomResources.Products)
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> GetProductAsync(
        [FromRoute, ModelBinder<StronglyTypedIdBinder<ProductId>>] ProductId id,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        => await sender
                .Send(new GetProductByIdQuery(id), cancellationToken)
                .MapAsync(productDto => new Response
                {
                    Id = productDto.Id,
                    Name = productDto.Name,
                    Description = productDto.Description,
                    Quantity = productDto.Quantity,
                    Price = productDto.Price,
                    CreatedBy = productDto.CreatedBy,
                    CreatedOn = productDto.CreatedOn,
                    LastModifiedBy = productDto.LastModifiedBy,
                    LastModifiedOn = productDto.LastModifiedOn
                });
}
