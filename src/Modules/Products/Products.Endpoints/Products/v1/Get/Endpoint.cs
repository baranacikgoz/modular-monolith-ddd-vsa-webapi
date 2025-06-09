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

namespace Products.Endpoints.Products.v1.Get;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder productsApiGroup)
    {
        productsApiGroup
            .MapGet("{id}", GetProductAsync)
            .WithDescription("Get Product.")
            .MustHavePermission(CustomActions.Read, CustomResources.Products)
            .TransformResultTo<Response>()
            .Produces<Response>(StatusCodes.Status200OK);
    }

    private static async Task<Result<Response>> GetProductAsync(
        [AsParameters] Request request,
        [FromServices] IProductsDbContext dbContext,
        CancellationToken cancellationToken)
        => await dbContext
            .Products
            .AsNoTracking()
            .TagWith(nameof(GetProductAsync), request.Id)
            .Where(p => p.Id == request.Id)
            .Select(p => new Response
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Quantity = p.Quantity,
                Price = p.Price,
                CreatedBy = p.CreatedBy,
                CreatedOn = p.CreatedOn,
                LastModifiedBy = p.LastModifiedBy,
                LastModifiedOn = p.LastModifiedOn
            })
            .SingleAsResultAsync(cancellationToken);
}
