using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Common.Domain.StronglyTypedIds;
using Microsoft.EntityFrameworkCore;
using Products.Domain.Products;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;
using Products.Tests.Support;
using Xunit;

namespace Products.Tests.Endpoints.Stores;

/// <summary>The SKU is unique within one store, enforced by a unique index, so a duplicate is a conflict answer and never a 500.</summary>
public class AddProductSkuTests(IntegrationTestWebAppFactory factory) : ProductsIntegrationTest(factory)
{
    // productTemplateId is written as a raw Guid because the strongly typed id converter only reads.
    private static object Body(ProductTemplate template, string? sku, string name = "Lamp")
    {
        return new { productTemplateId = template.Id.Value, name, description = "Description", quantity = 1, price = 10m, sku };
    }

    private static async Task<string?> ErrorKeyAsync(HttpResponseMessage response)
    {
        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        return json.RootElement.GetProperty("errorKey").GetString();
    }

    private async Task<string?> SkuOfAsync(string name, StoreId storeId)
    {
        return await Db.Products.AsNoTracking().Where(p => p.StoreId == storeId && p.Name == name).Select(p => p.Sku).SingleAsync();
    }

    [Fact]
    public async Task MyAddProduct_WithSku_StoresTheSku()
    {
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, DefaultUser);

        using var response = await AuthorizedClient().PostAsJsonAsync(new Uri("/v1/stores/my/products", UriKind.Relative), Body(seeded.Template, "SKU-1"));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("SKU-1", await SkuOfAsync("Lamp", seeded.Store.Id));
    }

    [Fact]
    public async Task MyAddProduct_WithASkuTheStoreAlreadyUses_ReturnsConflict()
    {
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, DefaultUser);
        var client = AuthorizedClient();
        using var first = await client.PostAsJsonAsync(new Uri("/v1/stores/my/products", UriKind.Relative), Body(seeded.Template, "SKU-1", "First"));

        using var second = await client.PostAsJsonAsync(new Uri("/v1/stores/my/products", UriKind.Relative), Body(seeded.Template, "SKU-1", "Second"));

        Assert.Equal(HttpStatusCode.OK, first.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, second.StatusCode);
        Assert.Equal("SkuAlreadyExists", await ErrorKeyAsync(second));
        Assert.Equal(1, await Db.Products.CountAsync(p => p.StoreId == seeded.Store.Id));
    }

    [Fact]
    public async Task MyAddProduct_WithASkuAnotherStoreUses_Succeeds()
    {
        var other = await ProductsSeed.StoreWithProductsAsync(Db, new ApplicationUserId(Guid.NewGuid()));
        Db.Products.Add(Product.Create(other.Store.Id, other.Template.Id, "Taken", "Description", 1, 10m, "SKU-1"));
        await Db.SaveChangesAsync();
        var mine = await ProductsSeed.StoreWithProductsAsync(Db, DefaultUser);

        using var response = await AuthorizedClient().PostAsJsonAsync(new Uri("/v1/stores/my/products", UriKind.Relative), Body(mine.Template, "SKU-1"));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task MyAddProduct_WithBlankSkuText_ReturnsBadRequest()
    {
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, DefaultUser);

        using var response = await AuthorizedClient().PostAsJsonAsync(new Uri("/v1/stores/my/products", UriKind.Relative), Body(seeded.Template, "  "));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task AddProduct_WithASkuTheStoreAlreadyUses_ReturnsConflict()
    {
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, DefaultUser);
        Db.Products.Add(Product.Create(seeded.Store.Id, seeded.Template.Id, "Taken", "Description", 1, 10m, "SKU-1"));
        await Db.SaveChangesAsync();

        using var response = await AuthorizedClient().PostAsJsonAsync(new Uri($"/v1/stores/{seeded.Store.Id}/products", UriKind.Relative), Body(seeded.Template, "SKU-1"));

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        Assert.Equal("SkuAlreadyExists", await ErrorKeyAsync(response));
    }
}
