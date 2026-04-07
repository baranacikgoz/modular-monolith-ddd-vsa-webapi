using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Extensions;
using Common.Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Products.Application.Persistence;
using Products.Domain.Stores;
using Products.Infrastructure.Telemetry;

namespace Products.Endpoints.Stores.v1.RemoveProduct;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder storesApiGroup)
    {
        storesApiGroup
            .MapDelete("{id}/products/{productId}", RemoveProductAsync)
            .WithDescription("Remove product from a store.")
            .MustHavePermission(CustomActions.Delete, CustomResources.Products)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> RemoveProductAsync(
        [AsParameters] Request request,
        IProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        using var activity = ProductsTelemetry.ActivitySource.StartActivityForCaller();
        activity?.SetTag("store.id", request.Id.Value);

        var result = await dbContext
            .Stores
            .TagWith(nameof(RemoveProductAsync), "StoreById", request.Id)
            .Where(s => s.Id == request.Id)
            .Include(s => s.Products.Where(p => p.Id == request.ProductId))
            .SingleAsResultAsync(resourceName: nameof(Store), cancellationToken)

            .CombineAsync(store => store.Products.SingleAsResult(p => p.Id == request.ProductId))
            .TapAsync(tuple =>
            {
                var (store, product) = tuple;
                store.RemoveProduct(product);
            })
            .TapAsync(_ => dbContext.SaveChangesAsync(cancellationToken))
            .TapAsync(tuple =>
            {
                ProductsTelemetry.ProductsRemovedFromStore.Add(1);
            });

        Result nonGenericResult = result;
        return nonGenericResult.TapActivity(activity);
    }
}
