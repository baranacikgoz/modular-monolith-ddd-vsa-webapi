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
using Products.Domain.Stores;

namespace Products.Endpoints.Stores.v1.Create;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myStoresApiGroup)
    {
        myStoresApiGroup
            .MapPost("", CreateStoreAsync)
            .WithDescription("Create a store.")
            .MustHavePermission(CustomActions.Create, CustomResources.Stores)
            .Produces<Response>()
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> CreateStoreAsync(
        [FromBody] Request request,
        [FromServices] IProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext
            .Stores
            .AsNoTracking()
            .TagWith(nameof(CreateStoreAsync), "GetStoreByOwnerId", request.OwnerId)
            .Where(s => s.OwnerId == request.OwnerId)
            .AnyAsResultAsync(cancellationToken)
            .TapAsync(any => any ? Error.ViolatesUniqueConstraint(nameof(Store)) : Result.Success)
            .BindAsync(_ => Store.Create(request.OwnerId, request.Name, request.Description, request.Address))
            .TapAsync(store => dbContext.Stores.Add(store))
            .TapAsync(_ => dbContext.SaveChangesAsync(cancellationToken))
            .MapAsync(store => new Response { Id = store.Id });
    }
}
