using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Common.IntegrationEvents;
using Microsoft.EntityFrameworkCore;
using Products.Domain.Products;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;
using Products.Tests.Stores;
using Products.Tests.Support;
using Xunit;

namespace Products.Tests.Endpoints.Products;

#pragma warning disable CA1707 // Identifiers should not contain underscores

/// <summary>
///     Every product write endpoint answers 409 with the key of the owning store's status when the store is not Active,
///     and changes nothing. Each endpoint is driven on its own: a handler that dropped the aggregate's failed result would
///     still compile.
/// </summary>
public class ProductStatusGuardTests(IntegrationTestWebAppFactory factory) : ProductsIntegrationTest(factory)
{
    private const string ImageKey = "products/lamp/images/a.png";

    private static readonly Dictionary<StoreStatus, string> ExpectedKeys = new()
    {
        [StoreStatus.PendingApproval] = "StoreIsPendingApproval",
        [StoreStatus.Suspended] = "StoreIsSuspended",
        [StoreStatus.Inactive] = "StoreIsInactive"
    };

    private static readonly byte[] Png = [0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A, 7, 7, 7, 7];

    private static async Task<string?> ErrorKeyAsync(HttpResponseMessage response)
    {
        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        return json.RootElement.GetProperty("errorKey").GetString();
    }

    private async Task<Product> SeedAsync(StoreStatus status, bool withImage = false)
    {
        var store = StoreStatusGuardTests.StoreIn(status, DefaultUser);
        var template = ProductTemplate.Create("Brand", "Model", "Color");
        var product = Product.Create(store.Id, template.Id, "Lamp", "Description", 5, 10m);
        if (withImage)
        {
            product.AddImage(ImageKey, "image/png", 10, 8);
        }

        Db.Stores.Add(store);
        Db.ProductTemplates.Add(template);
        Db.Products.Add(product);
        await Db.SaveChangesAsync();
        return product;
    }

    private async Task AssertRefusedAndUnchangedAsync(HttpResponseMessage response, StoreStatus status, Product product, long versionBefore)
    {
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        Assert.Equal(ExpectedKeys[status], await ErrorKeyAsync(response));

        var stored = await Db.Products.AsNoTracking().Include(p => p.Images).SingleAsync(p => p.Id == product.Id);
        Assert.Equal(versionBefore, stored.Version);
        Assert.Equal("Lamp", stored.Name);
        Assert.Equal(5, stored.Quantity);
        Assert.Empty(stored.Attributes);
        Assert.Null(stored.PackageDimensions);
        Assert.Equal(product.Images.Count, stored.Images.Count);
    }

    [Theory]
    [MemberData(nameof(StoreStatusGuardTests.NonActiveStatuses), MemberType = typeof(StoreStatusGuardTests))]
    public async Task Update_StoreNotActive_ReturnsConflictWithTheStatusKey(StoreStatus status, string _)
    {
        var product = await SeedAsync(status);

        using var response = await AuthorizedClient().PutAsJsonAsync(
            new Uri($"/v1/products/{product.Id}", UriKind.Relative), new { name = "Renamed", quantity = 9 });

        await AssertRefusedAndUnchangedAsync(response, status, product, product.Version);
    }

    [Theory]
    [MemberData(nameof(StoreStatusGuardTests.NonActiveStatuses), MemberType = typeof(StoreStatusGuardTests))]
    public async Task MyUpdate_StoreNotActive_ReturnsConflictWithTheStatusKey(StoreStatus status, string _)
    {
        var product = await SeedAsync(status);

        using var response = await AuthorizedClient().PutAsJsonAsync(
            new Uri($"/v1/products/my/{product.Id}", UriKind.Relative), new { name = "Renamed", quantity = 9 });

        await AssertRefusedAndUnchangedAsync(response, status, product, product.Version);
    }

    [Theory]
    [MemberData(nameof(StoreStatusGuardTests.NonActiveStatuses), MemberType = typeof(StoreStatusGuardTests))]
    public async Task MySetAttributes_StoreNotActive_ReturnsConflictWithTheStatusKey(StoreStatus status, string _)
    {
        var product = await SeedAsync(status);
        var body = new { attributes = new[] { new { name = "Color", value = "Red" } } };

        using var response = await AuthorizedClient().PutAsJsonAsync(
            new Uri($"/v1/products/my/{product.Id}/attributes", UriKind.Relative), body);

        await AssertRefusedAndUnchangedAsync(response, status, product, product.Version);
    }

    [Theory]
    [MemberData(nameof(StoreStatusGuardTests.NonActiveStatuses), MemberType = typeof(StoreStatusGuardTests))]
    public async Task MySetPackageDimensions_StoreNotActive_ReturnsConflictWithTheStatusKey(StoreStatus status, string _)
    {
        var product = await SeedAsync(status);
        var body = new { lengthCm = 10m, widthCm = 5m, heightCm = 4m, weightKg = 1.5m };

        using var response = await AuthorizedClient().PutAsJsonAsync(
            new Uri($"/v1/products/my/{product.Id}/package-dimensions", UriKind.Relative), body);

        await AssertRefusedAndUnchangedAsync(response, status, product, product.Version);
    }

    [Theory]
    [MemberData(nameof(StoreStatusGuardTests.NonActiveStatuses), MemberType = typeof(StoreStatusGuardTests))]
    public async Task AddImage_StoreNotActive_ReturnsConflictWithTheStatusKeyAndStoresNothing(StoreStatus status, string _)
    {
        var product = await SeedAsync(status);
        using var content = new MultipartFormDataContent();
        using var file = new ByteArrayContent(Png);
        file.Headers.ContentType = new MediaTypeHeaderValue("image/png");
        content.Add(file, "File", "image.png");

        using var response = await AuthorizedClient().PostAsync(new Uri($"/v1/products/{product.Id}/images", UriKind.Relative), content);

        await AssertRefusedAndUnchangedAsync(response, status, product, product.Version);
        Assert.Empty(PublicStore.Keys);
    }

    [Theory]
    [MemberData(nameof(StoreStatusGuardTests.NonActiveStatuses), MemberType = typeof(StoreStatusGuardTests))]
    public async Task RemoveImage_StoreNotActive_ReturnsConflictWithTheStatusKeyAndQueuesNoObjectDeletion(StoreStatus status, string _)
    {
        var product = await SeedAsync(status, withImage: true);
        var imageId = product.Images.Single().Id;

        using var response = await AuthorizedClient().DeleteAsync(
            new Uri($"/v1/products/{product.Id}/images/{imageId}", UriKind.Relative));

        await AssertRefusedAndUnchangedAsync(response, status, product, product.Version);
        Assert.Empty((await OutboxEventsAsync()).OfType<ProductImageObjectsOrphanedIntegrationEvent>());
    }
}

#pragma warning restore CA1707
