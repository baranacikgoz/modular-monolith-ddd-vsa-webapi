using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.ModelBinders;
using Common.Domain.ResultMonad;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Products.Application.ProductTemplates.Features.ActivateDeactivate;
using Products.Domain.ProductTemplates;

namespace Products.Endpoints.ProductTemplates.v1.Activate;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder productTemplatesApiGroup)
    {
        productTemplatesApiGroup
            .MapGet("{id}/activate", ActivateProductTemplateAsync)
            .WithDescription("Activate a product template.")
            .MustHavePermission(CustomActions.Update, CustomResources.ProductTemplates)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> ActivateProductTemplateAsync(
        [FromRoute, ModelBinder<StronglyTypedIdBinder<ProductTemplateId>>] ProductTemplateId id,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        => await sender.Send(new ActivateDeactivateProductTemplateCommand(id, Activate: true), cancellationToken);
}
