using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Products.Application.Persistence;

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
        [AsParameters] Request request,
        [FromServices] IProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext
            .Stores
            .TagWith(nameof(UpdateStoreAsync), request.Id)
            .Where(s => s.Id == request.Id)
            .SingleAsResultAsync(cancellationToken)
            .TapAsync(store => store.Update(request.Body.Name, request.Body.Description, request.Body.Address))
            .TapAsync(async _ => await dbContext.SaveChangesAsync(cancellationToken));
    }
}
