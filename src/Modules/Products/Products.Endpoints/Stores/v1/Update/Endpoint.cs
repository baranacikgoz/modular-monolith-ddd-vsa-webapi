using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Application.Auth;
using Common.Domain.ResultMonad;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Products.Infrastructure.Persistence;

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
        [FromServices] ProductsDbContext dbContext,
        CancellationToken cancellationToken)
        => await dbContext
            .Stores
            .TagWith(nameof(UpdateStoreAsync), request.Id)
            .Where(s => s.Id == request.Id)
            .SingleAsResultAsync(cancellationToken)
            .TapAsync(store => store.Update(request.Body.Name, request.Body.Description, request.Body.Address))
            .TapAsync(async _ => await dbContext.SaveChangesAsync(cancellationToken));
}
