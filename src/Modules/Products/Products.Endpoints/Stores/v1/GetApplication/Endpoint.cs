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

namespace Products.Endpoints.Stores.v1.GetApplication;

/// <summary>One store application waiting for a decision. A store that is anything but PendingApproval is not an application: it answers NotFound here.</summary>
internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder storesApiGroup)
    {
        storesApiGroup
            .MapGet("applications/{id}", GetApplicationAsync)
            .WithDescription("Get one store application that is waiting for a decision.")
            .RequireScope(KeycloakScopes.Stores.Approve)
            .Produces<Response>()
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> GetApplicationAsync(
        [AsParameters] Request request,
        IProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext
            .Stores
            .AsNoTracking()
            .TagWith(nameof(GetApplicationAsync), request.Id)
            .Where(store => store.Id == request.Id && store.Status == StoreStatus.PendingApproval)
            .Select(store => new Response
            {
                Id = store.Id,
                OwnerId = store.OwnerId,
                Name = store.Name,
                Description = store.Description,
                Address = store.Address,
                Status = store.Status,
                CreatedBy = store.CreatedBy,
                CreatedOn = store.CreatedOn,
                LastModifiedBy = store.LastModifiedBy,
                LastModifiedOn = store.LastModifiedOn
            })
            .SingleAsResultAsync(nameof(Store), cancellationToken);
    }
}
