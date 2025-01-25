using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Application.Auth;
using Common.Domain.ResultMonad;
using Common.Application.Extensions;
using Products.Domain.Stores;
using Products.Application.Stores.Features.Create;
using MediatR;

namespace Products.Endpoints.Stores.v1.My.Create;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myStoresApiGroup)
    {
        myStoresApiGroup
            .MapPost("my", CreateMyStoreAsync)
            .WithDescription("Create my store.")
            .MustHavePermission(CustomActions.CreateMy, CustomResources.Stores)
            .Produces<Response>(StatusCodes.Status200OK)
            .TransformResultTo<StoreId>();
    }

    private static async Task<Result<StoreId>> CreateMyStoreAsync(
        [FromBody] Request request,
        [FromServices] ICurrentUser currentUser,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        => await sender.Send(new CreateStoreCommand(
            OwnerId: currentUser.Id,
            Name: request.Name,
            Description: request.Description,
            Address: request.Address),
            cancellationToken);
}
