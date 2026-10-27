using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Products.Application.Persistence;
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.Approve;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder storesApiGroup)
    {
        storesApiGroup
            .MapPost("{id}/approve", ApproveStoreAsync)
            .WithDescription("Approve a store application. The store-owner role is granted by IAM from the StoreApprovedIntegrationEvent, not here.")
            .RequireScope(KeycloakScopes.Stores.Approve)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> ApproveStoreAsync(
        [AsParameters] Request request,
        IProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext
            .Stores
            .TagWith(nameof(ApproveStoreAsync), request.Id)
            .Where(s => s.Id == request.Id)
            .SingleAsResultAsync(nameof(Store), cancellationToken)
            .TapAsync(store => store.Approve())
            .TapAsync(async _ => await dbContext.SaveChangesAsync(cancellationToken));
    }
}
