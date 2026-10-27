using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Extensions;
using Common.Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Products.Application.Persistence;
using Products.Domain.Products;
using Products.Infrastructure.Telemetry;

namespace Products.Endpoints.Products.v1.RemoveImage;

/// <summary>
///     Removes the image row and, in the same save, collects the message that deletes the stored file
///     (<c>ProductImageObjectsOrphanedIntegrationEvent</c>, raised from the image's domain event). The request never touches
///     the object store, so a crash or a storage failure cannot leave an object behind that nothing will delete. The object
///     key is a content hash scoped to the product, so no other row shares the file.
/// </summary>
internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder productsApiGroup)
    {
        productsApiGroup
            .MapDelete("{id}/images/{imageId}", RemoveImageAsync)
            .WithDescription("Remove one image of a product of my store.")
            .RequireScope(KeycloakScopes.Products.UpdateOwn)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> RemoveImageAsync(
        [AsParameters] Request request,
        ICurrentUser currentUser,
        IProductsDbContext dbContext,
        CancellationToken cancellationToken)
    {
        using var activity = ProductsTelemetry.ActivitySource.StartActivityForCaller();

        return await dbContext.Products
            .Include(p => p.Images)
            .Include(p => p.Store)
            .TagWith(nameof(RemoveImageAsync), request.Id)
            .Where(p => p.Id == request.Id && p.Store.OwnerId == currentUser.Id)
            .SingleAsResultAsync(nameof(Product), cancellationToken)
            .BindAsync(product => Task.FromResult(product.Images
                .SingleAsResult(i => i.Id == request.ImageId)
                .Tap(image => product.Store.RemoveProductImage(product, image))))
            .TapAsync(_ => dbContext.SaveChangesAsync(cancellationToken))
            .TapActivityAsync(activity);
    }
}
