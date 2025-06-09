using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Application.Auth;
using Common.Domain.ResultMonad;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Products.Domain.Stores;
using Microsoft.EntityFrameworkCore;
using Products.Application.Persistence;

namespace Products.Endpoints.Stores.v1.My.Create;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder v1StoresApiGroup)
    {
        v1StoresApiGroup
            .MapPost("my", CreateMyStoreAsync)
            .WithDescription("Create my store.")
            .MustHavePermission(CustomActions.CreateMy, CustomResources.Stores)
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> CreateMyStoreAsync(
        [FromBody] Request request,
        [FromServices] ICurrentUser currentUser,
        [FromServices] IProductsDbContext dbContext,
        CancellationToken cancellationToken)
        => await dbContext
            .Stores
            .AsNoTracking()
            .TagWith(nameof(CreateMyStoreAsync), "GetStoreByOwnerId", currentUser.Id)
            .Where(s => s.OwnerId == currentUser.Id)
            .AnyAsResultAsync(cancellationToken)
            .TapAsync(any => any ? Error.ViolatesUniqueConstraint(nameof(Store)) : Result.Success)
            .BindAsync(_ => Store.Create(currentUser.Id, request.Name, request.Description, request.Address))
            .TapAsync(store => dbContext.Stores.Add(store))
            .TapAsync(_ => dbContext.SaveChangesAsync(cancellationToken))
            .MapAsync(store => new Response
            {
                Id = store.Id,
            });
}
