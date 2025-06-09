using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Domain.ResultMonad;
using Common.Application.Auth;
using Common.Application.Extensions;
using Products.Application.Persistence;
using Products.Domain.ProductTemplates;

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
        [FromServices] IProductsDbContext dbContext,
        CancellationToken cancellationToken)
        => await Result<ProductTemplate>
            .Create(() => ProductTemplate.Create(request.Brand, request.Model, request.Color))
            .Tap(product => dbContext.ProductTemplates.Add(product))
            .TapAsync(async _ => await dbContext.SaveChangesAsync(cancellationToken))
            .MapAsync(product => new Response { Id = product.Id });
}
