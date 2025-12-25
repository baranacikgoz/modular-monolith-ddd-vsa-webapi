using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Products.Application.Persistence;

namespace Products.Endpoints.Products.v1.My.Update;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myProductsApiGroup)
    {
        myProductsApiGroup
            .MapPut("my/{id}", UpdateMyProductAsync)
            .WithDescription("Update my product.")
            .MustHavePermission(CustomActions.UpdateMy, CustomResources.Products)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> UpdateMyProductAsync(
        [AsParameters] Request request,
        [FromServices] ICurrentUser currentUser,
        [FromServices] IProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext
            .Products
            .TagWith(nameof(UpdateMyProductAsync), request.Id)
            .Where(p => p.Store.OwnerId == currentUser.Id && p.Id == request.Id)
            .SingleAsResultAsync(cancellationToken)
            .TapAsync(product => product.Update(request.Body.Name, request.Body.Description, request.Body.Quantity,
                request.Body.Price))
            .TapAsync(async _ => await dbContext.SaveChangesAsync(cancellationToken));
    }
}
