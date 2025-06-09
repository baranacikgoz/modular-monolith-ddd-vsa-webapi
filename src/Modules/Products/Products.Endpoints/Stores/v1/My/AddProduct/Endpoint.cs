using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Products.Application.Persistence;
using Products.Domain.Products;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.My.AddProduct;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder v1StoresApiGroup)
    {
        v1StoresApiGroup
            .MapPost("my/products", AddProductToMyStoreAsync)
            .WithDescription("Add product to my store.")
            .MustHavePermission(CustomActions.CreateMy, CustomResources.Products)
            .Produces(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> AddProductToMyStoreAsync(
        [FromBody] Request request,
        [FromServices] ICurrentUser currentUser,
        [FromServices] IProductsDbContext dbContext,
        CancellationToken cancellationToken)
        => await dbContext
            .Stores
            .TagWith(nameof(AddProductToMyStoreAsync), "StoreByOwner", currentUser.Id)
            .Where(s => s.OwnerId == currentUser.Id)
            .SingleAsResultAsync(cancellationToken)
            .CombineAsync(store => dbContext
                .ProductTemplates
                .TagWith(nameof(AddProductToMyStoreAsync), "ActiveProductTemplateById", request.ProductTemplateId)
                .Where(pt => pt.IsActive)
                .Where(pt => pt.Id == request.ProductTemplateId)
                .SingleAsResultAsync(cancellationToken))
            .CombineAsync<Store, ProductTemplate, Product>(tuple =>
            {
                var (store, productTemplate) = tuple;
                return Product.Create(store.Id, productTemplate.Id, request.Name, request.Description, request.Quantity, request.Price);
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
