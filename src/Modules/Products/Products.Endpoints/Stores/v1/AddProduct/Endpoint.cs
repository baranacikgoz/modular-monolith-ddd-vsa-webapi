using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Products.Domain.Products;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;
using Products.Infrastructure.Persistence;

namespace Products.Endpoints.Stores.v1.AddProduct;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder storesApiGroup)
    {
        storesApiGroup
            .MapPost("{id}/products", AddProductAsync)
            .WithDescription("Add product to a store.")
            .MustHavePermission(CustomActions.Create, CustomResources.Products)
            .Produces(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> AddProductAsync(
        [AsParameters] Request request,
        [FromServices] ProductsDbContext dbContext,
        CancellationToken cancellationToken)
        => await dbContext
            .Stores
            .TagWith(nameof(AddProductAsync), "StoreById", request.Id)
            .Where(s => s.Id == request.Id)
            .SingleAsResultAsync(cancellationToken)
            .CombineAsync(store => dbContext
                .ProductTemplates
                .TagWith(nameof(AddProductAsync), "ActiveProductTemplateById", request.Body.ProductTemplateId)
                .Where(pt => pt.IsActive)
                .Where(pt => pt.Id == request.Body.ProductTemplateId)
                .SingleAsResultAsync(cancellationToken))
            .CombineAsync<Store, ProductTemplate, Product>(tuple =>
            {
                var (store, productTemplate) = tuple;
                return Product.Create(store.Id, productTemplate.Id, request.Body.Name, request.Body.Description, request.Body.Quantity, request.Body.Price);
            })
            .TapAsync(triple =>
            {
                var (store, _, product) = triple;
                store.AddProduct(product);
            })
            .TapAsync(_ => dbContext.SaveChangesAsync(cancellationToken))
            .MapAsync(triple =>
            {
                var (_, _, product) = triple;
                return new Response
                {
                    Id = product.Id,
                };
            });
}
