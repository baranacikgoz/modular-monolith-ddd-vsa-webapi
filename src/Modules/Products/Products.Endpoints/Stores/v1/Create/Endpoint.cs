using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Application.Auth;
using Common.Domain.ResultMonad;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Products.Domain.Stores;
using Products.Infrastructure.Persistence;

namespace Products.Endpoints.Stores.v1.Create;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myStoresApiGroup)
    {
        myStoresApiGroup
            .MapPost("", CreateStoreAsync)
            .WithDescription("Create a store.")
            .MustHavePermission(CustomActions.Create, CustomResources.Stores)
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> CreateStoreAsync(
        [FromBody] Request request,
        [FromServices] ProductsDbContext dbContext,
        CancellationToken cancellationToken)
        => await dbContext
            .Stores
            .AsNoTracking()
            .TagWith(nameof(CreateStoreAsync), "GetStoreByOwnerId", request.OwnerId)
            .Where(s => s.OwnerId == request.OwnerId)
            .AnyAsResultAsync(cancellationToken)
            .TapAsync(any => any ? Error.ViolatesUniqueConstraint(nameof(Store)) : Result.Success)
            .BindAsync(_ => Store.Create(request.OwnerId, request.Name, request.Description, request.Address))
            .TapAsync(store => dbContext.Stores.Add(store))
            .TapAsync(_ => dbContext.SaveChangesAsync(cancellationToken))
            .MapAsync(store => new Response
            {
                Id = store.Id,
            });
}
