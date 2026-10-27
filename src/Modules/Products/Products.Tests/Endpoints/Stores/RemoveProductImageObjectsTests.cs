using System.Net;
using Common.IntegrationEvents;
using Microsoft.EntityFrameworkCore;
using Products.Domain.Products;
using Products.Tests.Support;
using Xunit;

namespace Products.Tests.Endpoints.Stores;

/// <summary>
///     Removing a product deletes its image rows in the same save, and that save collects the event that deletes the stored
///     objects. The objects themselves go when the queue consumer runs, which these slice tests leave off.
/// </summary>
public class RemoveProductImageObjectsTests(IntegrationTestWebAppFactory factory) : ProductsIntegrationTest(factory)
{
    private static readonly string[] ImageKeys = ["products/lamp/images/a.png", "products/lamp/images/b.jpg"];

    private async Task<(Domain.Stores.Store Store, Product Product)> SeedAsync(params string[] imageKeys)
    {
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, DefaultUser, "Lamp");
        var product = seeded.Products.Single();
        foreach (var key in imageKeys)
        {
            product.AddImage(key, "image/png", 10, 8);
        }

        await Db.SaveChangesAsync();
        return (seeded.Store, product);
    }

    private async Task AssertObjectKeysQueuedAsync(ProductId productId)
    {
        var queued = Assert.Single((await OutboxEventsAsync()).OfType<ProductImageObjectsOrphanedIntegrationEvent>());
        Assert.Equal(productId.Value, queued.ProductId);
        Assert.Equal(ImageKeys, queued.ObjectKeys);
        Assert.False(await Db.Products.AsNoTracking().AnyAsync(p => p.Id == productId));
    }

    [Fact]
    public async Task RemoveProduct_ProductWithImages_QueuesTheObjectKeysForDeletion()
    {
        var (store, product) = await SeedAsync(ImageKeys);

        using var response = await AuthorizedClient().DeleteAsync(
            new Uri($"/v1/stores/{store.Id}/products/{product.Id}", UriKind.Relative));

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        await AssertObjectKeysQueuedAsync(product.Id);
    }

    [Fact]
    public async Task MyRemoveProduct_ProductWithImages_QueuesTheObjectKeysForDeletion()
    {
        var (_, product) = await SeedAsync(ImageKeys);

        using var response = await AuthorizedClient().DeleteAsync(new Uri($"/v1/stores/my/products/{product.Id}", UriKind.Relative));

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        await AssertObjectKeysQueuedAsync(product.Id);
    }

    [Fact]
    public async Task MyRemoveProduct_ProductWithoutImages_QueuesNoObjectDeletion()
    {
        var (_, product) = await SeedAsync();

        using var response = await AuthorizedClient().DeleteAsync(new Uri($"/v1/stores/my/products/{product.Id}", UriKind.Relative));

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Empty((await OutboxEventsAsync()).OfType<ProductImageObjectsOrphanedIntegrationEvent>());
    }
}
