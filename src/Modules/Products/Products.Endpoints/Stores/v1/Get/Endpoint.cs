using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Products.Application.Persistence;

namespace Products.Endpoints.Stores.v1.Get;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myStoresApiGroup)
    {
        myStoresApiGroup
            .MapGet("{id}", GetStoreAsync)
            .WithDescription("Get store.")
            .MustHavePermission(CustomActions.Read, CustomResources.Stores)
            .Produces<Response>()
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> GetStoreAsync(
        [AsParameters] Request request,
        [FromServices] IProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext
            .Stores
            .AsNoTracking()
            .TagWith(nameof(GetStoreAsync), request.Id)
            .Select(store => new Response
            {
                Id = store.Id,
                OwnerId = store.OwnerId,
                Name = store.Name,
                Description = store.Description,
                Address = store.Address,
                ProductCount = store.Products.Count,
                CreatedBy = store.CreatedBy,
                CreatedOn = store.CreatedOn,
                LastModifiedBy = store.LastModifiedBy,
                LastModifiedOn = store.LastModifiedOn
            })
            .SingleAsResultAsync(cancellationToken);
    }
}
