using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Products.Application.Persistence;
using Products.Domain.Products;

namespace Products.Endpoints.Products.v1.My.SetPackageDimensions;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder myProductsApiGroup)
    {
        myProductsApiGroup
            .MapPut("my/{id}/package-dimensions", SetMyProductPackageDimensionsAsync)
            .WithDescription("Set the package size (cm) and weight (kg) of my product.")
            .RequireScope(KeycloakScopes.Products.UpdateOwn)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> SetMyProductPackageDimensionsAsync(
        [AsParameters] Request request,
        ICurrentUser currentUser,
        IProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await PackageDimensions
            .Create(request.Body.LengthCm, request.Body.WidthCm, request.Body.HeightCm, request.Body.WeightKg)
            .BindAsync(dimensions => dbContext
                .Products
                .Include(p => p.Store)
                .TagWith(nameof(SetMyProductPackageDimensionsAsync), request.Id)
                .Where(p => p.Store.OwnerId == currentUser.Id && p.Id == request.Id)
                .SingleAsResultAsync(nameof(Product), cancellationToken)
                .TapAsync(product => product.Store.SetProductPackageDimensions(product, dimensions))
                .TapAsync(async _ => await dbContext.SaveChangesAsync(cancellationToken)));
    }
}
