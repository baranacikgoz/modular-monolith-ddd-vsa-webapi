using System.Security.Cryptography;
using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Options;
using Common.Application.Storage;
using Common.Domain.ResultMonad;
using Common.Endpoints.Webhooks;
using Common.Infrastructure.Extensions;
using Common.Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Products.Application.Persistence;
using Products.Domain.Products;
using Products.Domain.Products.Errors;
using Products.Infrastructure.Telemetry;

namespace Products.Endpoints.Products.v1.AddImage;

/// <summary>
///     Adds one image to a product of the caller's own store. The type comes from the file's first bytes, never from its
///     name; the size and the number of images per product are limits from <see cref="ProductsOptions" />. The file is
///     stored first, then the row is saved: the object key is the content hash, so a save that fails after the upload leaves
///     only an unreferenced object that a retry of the same file reuses, never a row pointing at a missing file. Two uploads
///     racing on the same product cannot pass the image limit together: the aggregate's version is a concurrency token and
///     the second save fails.
/// </summary>
internal static class Endpoint
{
    /// <summary>Multipart framing (boundary and part headers) around the file itself. The body cap is the file limit plus this.</summary>
    private const long MultipartFramingBytes = 64 * 1024;

    internal static void MapEndpoint(RouteGroupBuilder productsApiGroup)
    {
        productsApiGroup
            .MapPost("{id}/images", AddImageAsync)
            .WithDescription("Upload one image (JPEG, PNG or WebP) for a product of my store.")
            .RequireScope(KeycloakScopes.Products.UpdateOwn)
            .DisableAntiforgery()
            .LimitRequestBody(httpContext =>
                httpContext.RequestServices.GetRequiredService<IOptions<ProductsOptions>>().Value.ImageMaxFileBytes + MultipartFramingBytes)
            .Produces<Response>(StatusCodes.Status201Created)
            .TransformResultToCreatedResponse<Response>();
    }

    private static async Task<Result<Response>> AddImageAsync(
        [AsParameters] Request request,
        ICurrentUser currentUser,
        IProductsDbContext dbContext,
        IPublicObjectStore objectStore,
        IOptions<ProductsOptions> options,
        CancellationToken cancellationToken)
    {
        using var activity = ProductsTelemetry.ActivitySource.StartActivityForCaller();

        return await ReadImageAsync(request.File, options.Value.ImageMaxFileBytes, cancellationToken)
            .BindAsync(image => dbContext.Products
                .Include(p => p.Images)
                .TagWith(nameof(AddImageAsync), request.Id)
                .Where(p => p.Id == request.Id && p.Store.OwnerId == currentUser.Id)
                .SingleAsResultAsync(nameof(Product), cancellationToken)
                .BindAsync(product => Task.FromResult(product.AddImage(
                    image.ObjectKey(request.Id), image.ContentType, image.Content.Length, options.Value.MaxImagesPerProduct)))
                .TapAsync(_ => UploadAsync(objectStore, image, request.Id, cancellationToken))
                .TapAsync(_ => dbContext.SaveChangesAsync(cancellationToken))
                .MapAsync(saved => new Response { Id = saved.Id, Url = objectStore.GetPublicUrl(saved.ObjectKey) }))
            .TapActivityAsync(activity);
    }

    private static async Task<Result<ImageUpload>> ReadImageAsync(
        IFormFile file, long maxFileBytes, CancellationToken cancellationToken)
    {
        if (file.Length > maxFileBytes)
        {
            return ProductErrors.ImageFileTooLarge;
        }

        var content = new byte[file.Length];
        await using (var source = file.OpenReadStream())
        {
            await source.ReadExactlyAsync(content, cancellationToken);
        }

        return ImageSignature.TryDetect(content, out var contentType, out var extension)
            ? new ImageUpload(content, contentType, extension)
            : ProductErrors.ImageUnsupportedType;
    }

    private static async Task UploadAsync(
        IPublicObjectStore objectStore, ImageUpload image, ProductId productId, CancellationToken cancellationToken)
    {
        using var stream = new MemoryStream(image.Content, writable: false);
        await objectStore.UploadAsync(new UploadObjectRequest(image.ObjectKey(productId), stream, image.ContentType), cancellationToken);
    }

    private sealed record ImageUpload(byte[] Content, string ContentType, string Extension)
    {
        internal string ObjectKey(ProductId productId)
        {
            return $"products/{productId}/images/{Convert.ToHexStringLower(SHA256.HashData(Content))}{Extension}";
        }
    }
}
