using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Products.Application.Persistence;
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.Suspend;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder storesApiGroup)
    {
        storesApiGroup
            .MapPost("{id}/suspend", SuspendStoreAsync)
            .WithDescription("Suspend an active store. A suspended store can be reinstated or deactivated.")
            .RequireScope(KeycloakScopes.Stores.Suspend)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> SuspendStoreAsync(
        [AsParameters] Request request,
        IProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext
            .Stores
            .TagWith(nameof(SuspendStoreAsync), request.Id)
            .Where(s => s.Id == request.Id)
            .SingleAsResultAsync(nameof(Store), cancellationToken)
            .TapAsync(store => store.Suspend())
            .TapAsync(async _ => await dbContext.SaveChangesAsync(cancellationToken));
    }
}
