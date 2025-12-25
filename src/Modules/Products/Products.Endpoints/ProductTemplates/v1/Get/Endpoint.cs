using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Products.Application.Persistence;

namespace Products.Endpoints.ProductTemplates.v1.Get;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder productTemplatesApiGroup)
    {
        productTemplatesApiGroup
            .MapGet("{id}", GetProductTemplateAsync)
            .WithDescription("Get a product template.")
            .MustHavePermission(CustomActions.Read, CustomResources.ProductTemplates)
            .Produces<Response>()
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> GetProductTemplateAsync(
        [AsParameters] Request request,
        [FromServices] IProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext
            .ProductTemplates
            .AsNoTracking()
            .TagWith(nameof(GetProductTemplateAsync), request.Id)
            .Where(p => p.Id == request.Id)
            .Select(pt => new Response
            {
                Id = pt.Id,
                Brand = pt.Brand,
                Model = pt.Model,
                Color = pt.Color,
                CreatedBy = pt.CreatedBy,
                CreatedOn = pt.CreatedOn,
                LastModifiedBy = pt.LastModifiedBy,
                LastModifiedOn = pt.LastModifiedOn
            })
            .SingleAsResultAsync(cancellationToken);
    }
}
