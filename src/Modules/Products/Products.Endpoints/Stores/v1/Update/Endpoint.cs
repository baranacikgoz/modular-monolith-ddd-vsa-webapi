using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Application.Auth;
using Common.Domain.ResultMonad;
using Common.Application.Extensions;
using Common.Application.ModelBinders;
using Products.Domain.Stores;
using Common.Application.CQS;
using Products.Application.Stores.Features.Update;
using MediatR;

namespace Products.Endpoints.Stores.v1.Update;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder storesApiGroup)
    {
        storesApiGroup
            .MapPut("{id}", UpdateStoreAsync)
            .WithDescription("Update a store.")
            .MustHavePermission(CustomActions.Update, CustomResources.Stores)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> UpdateStoreAsync(
        [FromRoute, ModelBinder<StronglyTypedIdBinder<StoreId>>] StoreId id,
        [FromBody] Request request,
        [FromServices] ISender sender,
        CancellationToken cancellationToken)
        => await sender.Send(new UpdateStoreCommand(
                Id: id,
                Name: request.Name,
                Description: request.Description,
                Address: request.Address),
            cancellationToken);
}
