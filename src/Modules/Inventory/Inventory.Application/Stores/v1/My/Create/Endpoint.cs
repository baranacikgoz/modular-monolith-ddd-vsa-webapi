using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Application.Auth;
using Common.Domain.ResultMonad;
using Common.Application.Extensions;

namespace Inventory.Application.Stores.v1.My.Create;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder inventoryApiGroup)
    {
        inventoryApiGroup
            .MapPost("", CreateMyStoreAsync)
            .WithDescription("Create my store.")
            .MustHavePermission(CustomActions.CreateMy, CustomResources.Stores)
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<Response>();
    }

    private static ValueTask<Result<Response>> CreateMyStoreAsync(
        [FromBody] Request request,
        [FromServices] ICurrentUser currentUser,
        CancellationToken cancellationToken)
        => throw new NotImplementedException(); // sadece 1 taneye izin ver
}
