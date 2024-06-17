using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Inventory.Domain.Stores;
using Inventory.Domain.Products;
using Microsoft.Extensions.DependencyInjection;
using Inventory.Application.Stores.Specs;
using Inventory.Application.Products.Specs;
using Common.Application.Auth;
using Common.Domain.ResultMonad;
using Common.Application.Extensions;
using Common.Application.Persistence;

namespace Inventory.Application.Stores.v1.My.Products.Add;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myStoresApiGroup)
    {
        myStoresApiGroup
            .MapPost("", AddProductToMyStore)
            .WithDescription("Add product to my store.")
            .MustHavePermission(CustomActions.CreateMy, CustomResources.StoreProducts)
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> AddProductToMyStore(
        [FromBody] Request request,
        [FromServices] ICurrentUser currentUser,
        [FromServices] IRepository<Store> storesRepository,
        [FromServices] IRepository<Product> productsRepository,
        [FromKeyedServices(nameof(Inventory))] IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var storeResult = await storesRepository.SingleOrDefaultAsResultAsync(new StoreByOwnerIdSpec(currentUser.Id), cancellationToken);
        if (storeResult.IsFailure)
        {
            return storeResult.Error!;
        }
        var store = storeResult.Value!;

        var productResult = await productsRepository.SingleOrDefaultAsResultAsync(new ProductByIdSpec(request.ProductId), cancellationToken);
        if (productResult.IsFailure)
        {
            return productResult.Error!;
        }
        var product = productResult.Value!;

        var storeProduct = store.AddProduct(product.Id, request.Quantity, request.Price);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new Response(storeProduct.Id);
    }
}
