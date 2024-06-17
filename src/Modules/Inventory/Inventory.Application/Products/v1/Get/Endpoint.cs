using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Common.Domain.ResultMonad;
using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Ardalis.Specification;
using Inventory.Domain.Products;
using Microsoft.AspNetCore.Mvc;
using Common.Application.ModelBinders;

namespace Inventory.Application.Products.v1.Get;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder inventoryApiGroup)
    {
        inventoryApiGroup
            .MapGet("{id}", GetProductAsync)
            .WithDescription("Get a product.")
            .MustHavePermission(CustomActions.Create, CustomResources.Products)
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    public class ProductByIdSpec : SingleResultSpecification<Product, Response>
    {
        public ProductByIdSpec(ProductId id)
            => Query
                .Select(p => new Response(p.Id, p.Name, p.Description))
                .Where(p => p.Id == id);
    }

    private static async Task<Result<Response>> GetProductAsync(
        [FromRoute, ModelBinder<StronglyTypedIdBinder<ProductId>>] ProductId id,
        [FromServices] IRepository<Product> repository,
        CancellationToken cancellationToken)
        => await repository
            .SingleOrDefaultAsResultAsync(new ProductByIdSpec(id), cancellationToken);
}
