using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Domain.ResultMonad;
using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Common.Application.ModelBinders;
using Ardalis.Specification;
using Products.Domain.Products;

namespace Products.Application.Products.v1.Update;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder productsApiGroup)
    {
        productsApiGroup
            .MapPut("{id}", UpdateProductAsync)
            .WithDescription("Update a product.")
            .MustHavePermission(CustomActions.Create, CustomResources.Products)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private sealed class ProductByIdSpec : SingleResultSpecification<Product>
    {
        public ProductByIdSpec(ProductId id)
            => Query.Where(p => p.Id == id);
    }

    private static async Task<Result> UpdateProductAsync(
        [FromRoute, ModelBinder<StronglyTypedIdBinder<ProductId>>] ProductId id,
        [FromBody] Request request,
        [FromServices] IRepository<Product> repository,
        [FromKeyedServices(nameof(Products))] IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
        => await repository
            .SingleOrDefaultAsResultAsync(new ProductByIdSpec(id), cancellationToken)
            .TapAsync(product => product.Update(request.Name, request.Description))
            .TapAsync(async _ => await unitOfWork.SaveChangesAsync(cancellationToken));
}
