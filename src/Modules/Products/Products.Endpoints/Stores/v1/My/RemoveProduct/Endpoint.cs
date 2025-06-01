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

namespace Products.Endpoints.Stores.v1.My.RemoveProduct;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder v1StoresApiGroup)
    {
        v1StoresApiGroup
            .MapDelete("my/products/{id}", RemoveMyProductAsync)
            .WithDescription("Remove product from my store.")
            .MustHavePermission(CustomActions.DeleteMy, CustomResources.Products)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> RemoveMyProductAsync(
        [AsParameters] Request request,
        [FromServices] ICurrentUser currentUser,
        [FromServices] ProductsDbContext dbContext,
        CancellationToken cancellationToken)
        => await dbContext
            .Stores
            .TagWith(nameof(RemoveMyProductAsync), "StoreByOwner", currentUser.Id)
            .Where(s => s.OwnerId == currentUser.Id)
            .Include(s => s.Products.Where(p => p.Id == request.Id))
            .SingleAsResultAsync(cancellationToken)
            .CombineAsync(store => store.Products.SingleAsResult(p => p.Id == request.Id))
            .TapAsync(tuple =>
            {
                var (store, product) = tuple;
                store.RemoveProduct(product);
            })
            .TapAsync(_ => dbContext.SaveChangesAsync(cancellationToken));
}
