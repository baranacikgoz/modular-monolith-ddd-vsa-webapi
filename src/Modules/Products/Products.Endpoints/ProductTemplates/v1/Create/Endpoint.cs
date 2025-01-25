using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Domain.ResultMonad;
using Common.Application.Auth;
using Common.Application.Extensions;
using Products.Application.ProductTemplates.Features.Create;
using MediatR;

namespace Products.Endpoints.ProductTemplates.v1.Create;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder productTemplatesApiGroup)
    {
        productTemplatesApiGroup
            .MapPost("", CreateProductTemplateAsync)
            .WithDescription("Create a product template.")
            .MustHavePermission(CustomActions.Create, CustomResources.ProductTemplates)
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> CreateProductTemplateAsync(
        [FromBody] Request request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        => await sender
                .Send(new CreateProductTemplateCommand(request.Brand, request.Model, request.Color), cancellationToken)
                .MapAsync(id => new Response { Id = id });
}
