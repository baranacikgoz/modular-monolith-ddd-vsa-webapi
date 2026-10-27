using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Products.Application.Persistence;
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.Reinstate;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder storesApiGroup)
    {
        storesApiGroup
            .MapPost("{id}/reinstate", ReinstateStoreAsync)
            .WithDescription("Reinstate a suspended store. A deactivated store stays closed.")
            .RequireScope(KeycloakScopes.Stores.Reinstate)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> ReinstateStoreAsync(
        [AsParameters] Request request,
        IProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext
            .Stores
            .TagWith(nameof(ReinstateStoreAsync), request.Id)
            .Where(s => s.Id == request.Id)
            .SingleAsResultAsync(nameof(Store), cancellationToken)
            .TapAsync(store => store.Reinstate())
            .TapAsync(async _ => await dbContext.SaveChangesAsync(cancellationToken));
    }
}
