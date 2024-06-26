using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Inventory.Domain.Stores;
using Inventory.Domain.Products;
using Microsoft.Extensions.DependencyInjection;
using Common.Application.Auth;
using Common.Domain.ResultMonad;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Ardalis.Specification;
using Common.Domain.StronglyTypedIds;
using Common.Application.ModelBinders;

namespace Inventory.Application.StoreProducts.v1.AddToStore;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myStoresApiGroup)
    {
        myStoresApiGroup
            .MapPost("", AddProductToStore)
            .WithDescription("Add product to store.")
            .MustHavePermission(CustomActions.Create, CustomResources.StoreProducts)
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private sealed class StoreByIdSpec : SingleResultSpecification<Store>
    {
        public StoreByIdSpec(StoreId storeId)
            => Query
                .Where(s => s.Id == storeId);
    }

    private sealed class ProductByIdSpec : SingleResultSpecification<Product>
    {
        public ProductByIdSpec(ProductId id)
            => Query.Where(p => p.Id == id);
    }

    private static async Task<Result<Response>> AddProductToStore(
        [FromBody] Request request,
        [FromServices] IRepository<Store> storesRepository,
        [FromServices] IRepository<Product> productsRepository,
        [FromKeyedServices(nameof(Inventory))] IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var storeResult = await storesRepository.SingleOrDefaultAsResultAsync(new StoreByIdSpec(request.StoreId), cancellationToken);
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
