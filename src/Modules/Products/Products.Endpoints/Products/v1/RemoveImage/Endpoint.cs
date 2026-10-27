using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Storage;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Extensions;
using Common.Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Products.Application.Persistence;
using Products.Domain.Products;
using Products.Infrastructure.Telemetry;

namespace Products.Endpoints.Products.v1.RemoveImage;

/// <summary>Removes the row first, then the stored file. A failed file delete only leaves an unreferenced object (logged),
/// never a row pointing at a missing file. The object key is a content hash scoped to the product, so no other row can
/// share the file.</summary>
internal static partial class Endpoint
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
        IPublicObjectStore objectStore,
        ILogger<Product> logger,
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
            .TapAsync(image => DeleteObjectAsync(objectStore, logger, image.ObjectKey, cancellationToken))
            .TapActivityAsync(activity);
    }

    private static async Task DeleteObjectAsync(
        IPublicObjectStore objectStore, ILogger logger, string objectKey, CancellationToken cancellationToken)
    {
        try
        {
            await objectStore.DeleteAsync(objectKey, cancellationToken);
        }
        catch (Exception exception) when (exception is not OperationCanceledException)
        {
            LogObjectDeleteFailed(logger, exception, objectKey);
        }
    }

    [LoggerMessage(Level = LogLevel.Warning, Message = "Product image row removed but object {ObjectKey} could not be deleted; it stays unreferenced in the bucket.")]
    private static partial void LogObjectDeleteFailed(ILogger logger, Exception exception, string objectKey);
}
