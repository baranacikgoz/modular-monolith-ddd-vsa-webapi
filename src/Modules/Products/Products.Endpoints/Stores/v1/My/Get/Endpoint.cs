using Common.Application.Auth;
using Common.Domain.ResultMonad;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Products.Infrastructure.Persistence;

namespace Products.Endpoints.Stores.v1.My.Get;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder v1StoresApiGroup)
    {
        v1StoresApiGroup
            .MapGet("my", GetMyStoreAsync)
            .WithDescription("Get my store.")
            .MustHavePermission(CustomActions.ReadMy, CustomResources.Stores)
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> GetMyStoreAsync(
        [FromServices] ICurrentUser currentUser,
        [FromServices] ProductsDbContext dbContext,
        CancellationToken cancellationToken)
        => await dbContext
            .Stores
            .AsNoTracking()
            .TagWith(nameof(GetMyStoreAsync), currentUser.Id)
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
                LastModifiedOn = store.LastModifiedOn,
            })
            .SingleAsResultAsync(cancellationToken);
}
