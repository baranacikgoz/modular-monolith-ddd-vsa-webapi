using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Products.Application.Persistence;

namespace Products.Endpoints.ProductTemplates.v1.Deactivate;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder productTemplatesApiGroup)
    {
        productTemplatesApiGroup
            .MapGet("{id}/deactivate", DeactivateProductTemplateAsync)
            .WithDescription("Deactivate a product template.")
            .MustHavePermission(CustomActions.Update, CustomResources.ProductTemplates)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> DeactivateProductTemplateAsync(
        [AsParameters] Request request,
        [FromServices] IProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext
            .ProductTemplates
            .TagWith(nameof(DeactivateProductTemplateAsync), request.Id)
            .Where(p => p.Id == request.Id)
            .SingleAsResultAsync(cancellationToken)
            .TapAsync(productTemplate => productTemplate.Deactivate())
            .TapAsync(async _ => await dbContext.SaveChangesAsync(cancellationToken));
    }
}
