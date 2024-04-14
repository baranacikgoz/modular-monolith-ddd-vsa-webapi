using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Common.Core.Auth;
using Common.Core.Contracts.Results;
using Microsoft.AspNetCore.Mvc;
using Common.Core.Extensions;
using Sales.Persistence;
using Sales.Features.Stores.Domain;
using Microsoft.EntityFrameworkCore;

namespace Sales.Features.Stores.UseCases.v1.My.Create;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder salesApiGroup)
    {
        salesApiGroup
            .MapPost("", CreateMyStoreAsync)
            .WithDescription("Create my store.")
            .MustHavePermission(CustomActions.CreateMy, CustomResources.Stores)
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private static async ValueTask<Result<Response>> CreateMyStoreAsync(
        [FromBody] Request request,
        [FromServices] ICurrentUser currentUser,
        [FromServices] SalesDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var ownerId = currentUser.Id;
        var store = Store.Create(ownerId, request.Name);

        dbContext.Stores.Add(store);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<Response>.Success(new Response(store.Id.Value));
    }
}
