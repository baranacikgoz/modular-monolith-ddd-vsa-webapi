using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Application.Auth;
using Common.Domain.ResultMonad;
using Common.Application.Extensions;
using Products.Application.Stores.Features.Update;
using Products.Application.Stores.Features.GetStoreIdByOwnerId;
using MediatR;

namespace Products.Endpoints.Stores.v1.My.Update;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myStoresApiGroup)
    {
        myStoresApiGroup
            .MapPut("my", UpdateMyStoreAsync)
            .WithDescription("Update my store.")
            .MustHavePermission(CustomActions.UpdateMy, CustomResources.Stores)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> UpdateMyStoreAsync(
        [FromBody] Request request,
        [FromServices] ICurrentUser currentUser,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        => await sender
                .Send(new GetStoreIdByOwnerIdQuery(currentUser.Id), cancellationToken)
                .BindAsync(storeId => sender.Send(new UpdateStoreCommand(
                    Id: storeId,
                    Name: request.Name,
                    Description: request.Description,
                    Address: request.Address),
                    cancellationToken));
}
