using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Products.Application.Persistence;
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.Deactivate;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder storesApiGroup)
    {
        storesApiGroup
            .MapPost("{id}/deactivate", DeactivateStoreAsync)
            .WithDescription("Deactivate a store.")
            .RequireScope(KeycloakScopes.Stores.Update)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> DeactivateStoreAsync(
        [AsParameters] Request request,
        IProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        // Store.Deactivate counts the products with stock to pick its event, so exactly those must be loaded.
        return await dbContext
            .Stores
            .TagWith(nameof(DeactivateStoreAsync), request.Id)
            .Where(s => s.Id == request.Id)
            .Include(s => s.Products.Where(p => p.Quantity > 0))
            .SingleAsResultAsync(nameof(Store), cancellationToken)
            .TapAsync(store => store.Deactivate())
            .TapAsync(async _ => await dbContext.SaveChangesAsync(cancellationToken));
    }
}
