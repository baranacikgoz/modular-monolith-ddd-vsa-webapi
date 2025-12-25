using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Products.Application.Persistence;

namespace Products.Endpoints.Stores.v1.My.Update;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder v1StoresApiGroup)
    {
        v1StoresApiGroup
            .MapPut("my", UpdateMyStoreAsync)
            .WithDescription("Update my store.")
            .MustHavePermission(CustomActions.UpdateMy, CustomResources.Stores)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> UpdateMyStoreAsync(
        [FromBody] Request request,
        [FromServices] ICurrentUser currentUser,
        [FromServices] IProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext
            .Stores
            .TagWith(nameof(UpdateMyStoreAsync), currentUser.Id)
            .Where(s => s.OwnerId == currentUser.Id)
            .SingleAsResultAsync(cancellationToken)
            .TapAsync(store => store.Update(request.Name, request.Description, request.Address))
            .TapAsync(async _ => await dbContext.SaveChangesAsync(cancellationToken));
    }
}
