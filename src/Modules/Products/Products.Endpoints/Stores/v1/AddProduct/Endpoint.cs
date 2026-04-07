using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Extensions;
using Common.Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Products.Application.Persistence;
using Products.Domain.Products;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;
using Products.Infrastructure.Telemetry;

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
        IProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        using var activity = ProductsTelemetry.ActivitySource.StartActivityForCaller();
        activity?.SetTag("store.id", request.Id.Value);

        return await dbContext
            .Stores
            .TagWith(nameof(AddProductAsync), "StoreById", request.Id)
            .Where(s => s.Id == request.Id)
            .SingleAsResultAsync(resourceName: nameof(Store), cancellationToken)

            .CombineAsync(store => dbContext
                .ProductTemplates
                .TagWith(nameof(AddProductAsync), "ActiveProductTemplateById", request.Body.ProductTemplateId)
                .Where(pt => pt.IsActive)
                .Where(pt => pt.Id == request.Body.ProductTemplateId)
                .SingleAsResultAsync(resourceName: nameof(ProductTemplate), cancellationToken))

            .CombineAsync<Store, ProductTemplate, Product>(tuple =>
            {
                var (store, productTemplate) = tuple;
                return Product.Create(store.Id, productTemplate.Id, request.Body.Name, request.Body.Description,
                    request.Body.Quantity, request.Body.Price);
            })
            .TapAsync(triple =>
            {
                var (store, _, product) = triple;
                store.AddProduct(product);
            })
            .TapAsync(_ => dbContext.SaveChangesAsync(cancellationToken))
            .TapAsync(triple =>
            {
                ProductsTelemetry.ProductsAddedToStore.Add(1);
            })
            .MapAsync(triple =>
            {
                var (_, _, product) = triple;
                return new Response { Id = product.Id };
            })
            .TapActivityAsync(activity);
    }
}
