using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Common.Domain.ResultMonad;
using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.Infrastructure.Persistence;

namespace Products.Endpoints.ProductTemplates.v1.Get;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder productTemplatesApiGroup)
    {
        productTemplatesApiGroup
            .MapGet("{id}", GetProductTemplateAsync)
            .WithDescription("Get a product template.")
            .MustHavePermission(CustomActions.Read, CustomResources.ProductTemplates)
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> GetProductTemplateAsync(
        [AsParameters] Request request,
        [FromServices] ProductsDbContext dbContext,
        CancellationToken cancellationToken)
        => await dbContext
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
                LastModifiedOn = pt.LastModifiedOn,
            })
            .SingleAsResultAsync(cancellationToken);
}
