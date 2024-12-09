using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Common.Domain.ResultMonad;
using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Ardalis.Specification;
using Microsoft.AspNetCore.Mvc;
using Common.Application.ModelBinders;
using Products.Domain.Products;

namespace Products.Application.Products.v1.Get;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder productsApiGroup)
    {
        productsApiGroup
            .MapGet("{id}", GetProductAsync)
            .WithDescription("Get a product.")
            .MustHavePermission(CustomActions.Read, CustomResources.Products)
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private sealed class ProductByIdSpec : SingleResultSpecification<Product, Response>
    {
        public ProductByIdSpec(ProductId id)
            => Query
                .Select(p => new Response
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CreatedBy = p.CreatedBy,
                    CreatedOn = p.CreatedOn,
                    LastModifiedBy = p.LastModifiedBy,
                    LastModifiedOn = p.LastModifiedOn
                })
                .Where(p => p.Id == id);
    }

    private static async Task<Result<Response>> GetProductAsync(
        [FromRoute, ModelBinder<StronglyTypedIdBinder<ProductId>>] ProductId id,
        [FromServices] IRepository<Product> repository,
        CancellationToken cancellationToken)
        => await repository
            .SingleOrDefaultAsResultAsync(new ProductByIdSpec(id), cancellationToken);
}
