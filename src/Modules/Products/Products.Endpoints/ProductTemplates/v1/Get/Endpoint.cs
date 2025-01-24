using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Common.Domain.ResultMonad;
using Common.Application.Auth;
using Common.Application.Extensions;
using Microsoft.AspNetCore.Mvc;
using Common.Application.ModelBinders;
using Products.Domain.ProductTemplates;
using Products.Application.ProductTemplates.DTOs;
using Common.Application.CQS;
using Products.Application.ProductTemplates.Features.GetById;
using MediatR;

namespace Products.Endpoints.ProductTemplates.v1.Get;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder productTemplatesApiGroup)
    {
        productTemplatesApiGroup
            .MapGet("{id}", GetProductAsync)
            .WithDescription("Get a product template.")
            .MustHavePermission(CustomActions.Read, CustomResources.ProductTemplates)
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> GetProductAsync(
        [FromRoute, ModelBinder<StronglyTypedIdBinder<ProductTemplateId>>] ProductTemplateId id,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        => await sender
                .Send(new GetProductTemplateByIdQuery(id), cancellationToken)
                .MapAsync(x => new Response
                {
                    Id = x.Id,
                    Brand = x.Brand,
                    Model = x.Model,
                    Color = x.Color,
                    CreatedOn = x.CreatedOn,
                    CreatedBy = x.CreatedBy,
                    LastModifiedOn = x.LastModifiedOn,
                    LastModifiedBy = x.LastModifiedBy
                });
}
