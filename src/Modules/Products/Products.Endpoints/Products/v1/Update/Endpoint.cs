using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Common.Application.Auth;
using Common.Domain.ResultMonad;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Products.Application.Persistence;

namespace Products.Endpoints.Products.v1.Update;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder productsApiGroup)
    {
        productsApiGroup
            .MapPut("{id}", UpdateProductAsync)
            .WithDescription("Update a product.")
            .MustHavePermission(CustomActions.Update, CustomResources.Products)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> UpdateProductAsync(
        [AsParameters] Request request,
        [FromServices] IProductsDbContext dbContext,
        CancellationToken cancellationToken)
        => await dbContext
            .Products
            .TagWith(nameof(UpdateProductAsync), request.Id)
            .Where(p => p.Id == request.Id)
            .SingleAsResultAsync(cancellationToken)
            .TapAsync(product => product.Update(request.Body.Name, request.Body.Description, request.Body.Quantity, request.Body.Price))
            .TapAsync(async _ => await dbContext.SaveChangesAsync(cancellationToken));
}
