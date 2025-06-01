using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Products.Infrastructure.Persistence;

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
        [FromServices] ProductsDbContext dbContext,
        CancellationToken cancellationToken)
        => await dbContext
            .Stores
            .TagWith(nameof(RemoveProductAsync), "StoreById", request.Id)
            .Where(s => s.Id == request.Id)
            .Include(s => s.Products.Where(p => p.Id == request.ProductId))
            .SingleAsResultAsync(cancellationToken)
            .CombineAsync(store => store.Products.SingleAsResult(p => p.Id == request.ProductId))
            .TapAsync(tuple =>
            {
                var (store, product) = tuple;
                store.RemoveProduct(product);
            })
            .TapAsync(_ => dbContext.SaveChangesAsync(cancellationToken));
}
