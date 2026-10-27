using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Common.Domain.StronglyTypedIds;
using Common.IntegrationEvents;
using Common.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.IntegrationEventHandlers;
using Products.Application.Persistence;
using Products.Domain.Products;
using Products.Domain.Products.Errors;
using Products.Tests.Support;
using Xunit;
using AddImageResponse = Products.Endpoints.Products.v1.AddImage.Response;

namespace Products.Tests.Endpoints.Products;

public class ImageTests(IntegrationTestWebAppFactory factory) : ProductsIntegrationTest(factory)
{
    private static readonly ApplicationUserId Owner = DefaultUser;

    private static readonly byte[] PngHeader = [0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A];

    private static byte[] Png(int extraBytes = 16)
    {
        return [.. PngHeader, .. Enumerable.Repeat((byte)7, extraBytes)];
    }

    private static async Task<HttpResponseMessage> UploadAsync(HttpClient client, ProductId productId, byte[] bytes, string fileName = "image.png")
    {
        using var content = new MultipartFormDataContent();
        using var file = new ByteArrayContent(bytes);
        file.Headers.ContentType = new MediaTypeHeaderValue("image/png");
        content.Add(file, "File", fileName);
        return await client.PostAsync(new Uri($"/v1/products/{productId}/images", UriKind.Relative), content);
    }

    [Fact]
    public async Task AddImage_WithPng_StoresTheObjectAndTheRow()
    {
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, Owner, "Lamp");
        var product = seeded.Products[0];
        var bytes = Png();

        using var response = await UploadAsync(AuthorizedClient(), product.Id, bytes);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<AddImageResponse>(JsonSerializerOptions);
        Assert.NotNull(body);

        using var checkScope = Factory.Services.CreateScope();
        var image = await checkScope.ServiceProvider.GetRequiredService<IProductsDbContext>().Set<ProductImage>()
            .AsNoTracking().SingleAsync(i => i.ProductId == product.Id);
        Assert.Equal(body.Id, image.Id);
        Assert.Equal("image/png", image.ContentType);
        Assert.Equal(bytes.Length, image.SizeBytes);
        Assert.StartsWith($"products/{product.Id}/images/", image.ObjectKey, StringComparison.Ordinal);
        Assert.EndsWith(".png", image.ObjectKey, StringComparison.Ordinal);
        Assert.Equal(bytes, PublicStore.Read(image.ObjectKey));
        Assert.Equal(PublicStore.GetPublicUrl(image.ObjectKey), body.Url);
    }

    [Fact]
    public async Task AddImage_FileThatIsNotAnImageDespiteItsName_Returns415AndStoresNothing()
    {
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, Owner, "Lamp");

        using var response = await UploadAsync(AuthorizedClient(), seeded.Products[0].Id, "<html></html>"u8.ToArray(), "image.png");

        Assert.Equal(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
        Assert.Contains(ProductErrors.ImageUnsupportedType.Key, await response.Content.ReadAsStringAsync(), StringComparison.Ordinal);
        Assert.Empty(PublicStore.Keys);
    }

    [Fact]
    public async Task AddImage_FileOverTheSizeLimit_Returns413AndStoresNothing()
    {
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, Owner, "Lamp");
        var limit = Scope.ServiceProvider.GetRequiredService<Microsoft.Extensions.Options.IOptions<Common.Application.Options.ProductsOptions>>().Value.ImageMaxFileBytes;

        using var response = await UploadAsync(AuthorizedClient(), seeded.Products[0].Id, Png((int)limit));

        Assert.Equal(HttpStatusCode.RequestEntityTooLarge, response.StatusCode);
        Assert.Empty(PublicStore.Keys);
    }

    [Fact]
    public async Task AddImage_SameFileTwice_SecondUploadIsAConflict()
    {
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, Owner, "Lamp");
        var client = AuthorizedClient();
        var bytes = Png();

        using var first = await UploadAsync(client, seeded.Products[0].Id, bytes);
        using var second = await UploadAsync(client, seeded.Products[0].Id, bytes);

        Assert.Equal(HttpStatusCode.Created, first.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, second.StatusCode);
        Assert.Single(PublicStore.Keys);
    }

    [Fact]
    public async Task AddImage_ProductOfAnotherOwner_Returns404()
    {
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, new ApplicationUserId(Guid.NewGuid()), "Lamp");

        using var response = await UploadAsync(AuthorizedClient(), seeded.Products[0].Id, Png());

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Empty(PublicStore.Keys);
    }

    [Fact]
    public async Task RemoveImage_ExistingImage_DeletesTheRowAndQueuesTheObjectForDeletionInTheSameSave()
    {
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, Owner, "Lamp");
        var product = seeded.Products[0];
        var client = AuthorizedClient();
        using var upload = await UploadAsync(client, product.Id, Png());
        var uploaded = await upload.Content.ReadFromJsonAsync<AddImageResponse>(JsonSerializerOptions);
        var key = Assert.Single(PublicStore.Keys);

        using var response = await client.DeleteAsync(new Uri($"/v1/products/{product.Id}/images/{uploaded!.Id}", UriKind.Relative));

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        using var checkScope = Factory.Services.CreateScope();
        Assert.False(await checkScope.ServiceProvider.GetRequiredService<IProductsDbContext>().Set<ProductImage>()
            .AsNoTracking().AnyAsync(i => i.ProductId == product.Id));
        var queued = Assert.Single((await OutboxEventsAsync()).OfType<ProductImageObjectsOrphanedIntegrationEvent>());
        Assert.Equal(product.Id.Value, queued.ProductId);
        Assert.Equal([key], queued.ObjectKeys);

        // The object goes when the queue consumer runs, which these slice tests leave off: nothing deletes it in the request.
        Assert.Equal([key], PublicStore.Keys);
        await ConsumerHarness.DeliverAsync<DeleteOrphanedProductImageObjectsHandler, ProductImageObjectsOrphanedIntegrationEvent>(Factory.Services, queued);
        Assert.Empty(PublicStore.Keys);
    }

    [Fact]
    public async Task RemoveImage_ProductOfAnotherOwner_Returns404()
    {
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, new ApplicationUserId(Guid.NewGuid()), "Lamp");

        using var response = await AuthorizedClient().DeleteAsync(
            new Uri($"/v1/products/{seeded.Products[0].Id}/images/{ProductImageId.New()}", UriKind.Relative));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
