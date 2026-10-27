using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Products.Domain.Products;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;
using Products.Tests.Stores;
using Products.Tests.Support;
using Xunit;

namespace Products.Tests.Endpoints.Stores;

/// <summary>
///     Every store write endpoint answers 409 with the key of the store's status when the store is not Active, and changes
///     nothing. Each endpoint is driven on its own: a handler that dropped the aggregate's failed result would still compile.
/// </summary>
public class StatusGuardTests(IntegrationTestWebAppFactory factory) : ProductsIntegrationTest(factory)
{
    private static readonly Dictionary<StoreStatus, string> ExpectedKeys = new()
    {
        [StoreStatus.PendingApproval] = "StoreIsPendingApproval",
        [StoreStatus.Suspended] = "StoreIsSuspended",
        [StoreStatus.Inactive] = "StoreIsInactive"
    };

    private static async Task<string?> ErrorKeyAsync(HttpResponseMessage response)
    {
        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        return json.RootElement.GetProperty("errorKey").GetString();
    }

    private async Task<(Store Store, ProductTemplate Template, Product Product)> SeedAsync(StoreStatus status)
    {
        var store = StoreStatusGuardTests.StoreIn(status, DefaultUser);
        var template = ProductTemplate.Create("Brand", "Model", "Color");
        var product = Product.Create(store.Id, template.Id, "Lamp", "Description", 5, 10m);
        Db.Stores.Add(store);
        Db.ProductTemplates.Add(template);
        Db.Products.Add(product);
        await Db.SaveChangesAsync();
        return (store, template, product);
    }

    private static async Task AssertRefusedAsync(HttpResponseMessage response, StoreStatus status)
    {
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        Assert.Equal(ExpectedKeys[status], await ErrorKeyAsync(response));
    }

    private async Task AssertStoreUnchangedAsync(StoreId id)
    {
        var store = await Db.Stores.AsNoTracking().Include(s => s.Products).SingleAsync(s => s.Id == id);
        Assert.Equal("Sample Store", store.Name);
        Assert.Single(store.Products);
    }

    [Theory]
    [MemberData(nameof(StoreStatusGuardTests.NonActiveStatuses), MemberType = typeof(StoreStatusGuardTests))]
    public async Task Update_StoreNotActive_ReturnsConflictWithTheStatusKey(StoreStatus status, string _)
    {
        var seeded = await SeedAsync(status);

        using var response = await AuthorizedClient().PutAsJsonAsync(
            new Uri($"/v1/stores/{seeded.Store.Id}", UriKind.Relative), new { name = "Renamed" });

        await AssertRefusedAsync(response, status);
        await AssertStoreUnchangedAsync(seeded.Store.Id);
    }

    [Theory]
    [MemberData(nameof(StoreStatusGuardTests.NonActiveStatuses), MemberType = typeof(StoreStatusGuardTests))]
    public async Task MyUpdate_StoreNotActive_ReturnsConflictWithTheStatusKey(StoreStatus status, string _)
    {
        var seeded = await SeedAsync(status);

        using var response = await AuthorizedClient().PutAsJsonAsync(new Uri("/v1/stores/my", UriKind.Relative), new { name = "Renamed" });

        await AssertRefusedAsync(response, status);
        await AssertStoreUnchangedAsync(seeded.Store.Id);
    }

    [Theory]
    [MemberData(nameof(StoreStatusGuardTests.NonActiveStatuses), MemberType = typeof(StoreStatusGuardTests))]
    public async Task AddProduct_StoreNotActive_ReturnsConflictWithTheStatusKey(StoreStatus status, string _)
    {
        var seeded = await SeedAsync(status);
        var body = new { productTemplateId = seeded.Template.Id.Value, name = "Desk", description = "Description", quantity = 1, price = 10m };

        using var response = await AuthorizedClient().PostAsJsonAsync(
            new Uri($"/v1/stores/{seeded.Store.Id}/products", UriKind.Relative), body);

        await AssertRefusedAsync(response, status);
        await AssertStoreUnchangedAsync(seeded.Store.Id);
    }

    [Theory]
    [MemberData(nameof(StoreStatusGuardTests.NonActiveStatuses), MemberType = typeof(StoreStatusGuardTests))]
    public async Task MyAddProduct_StoreNotActive_ReturnsConflictWithTheStatusKey(StoreStatus status, string _)
    {
        var seeded = await SeedAsync(status);
        var body = new { productTemplateId = seeded.Template.Id.Value, name = "Desk", description = "Description", quantity = 1, price = 10m };

        using var response = await AuthorizedClient().PostAsJsonAsync(new Uri("/v1/stores/my/products", UriKind.Relative), body);

        await AssertRefusedAsync(response, status);
        await AssertStoreUnchangedAsync(seeded.Store.Id);
    }

    [Theory]
    [MemberData(nameof(StoreStatusGuardTests.NonActiveStatuses), MemberType = typeof(StoreStatusGuardTests))]
    public async Task RemoveProduct_StoreNotActive_ReturnsConflictWithTheStatusKey(StoreStatus status, string _)
    {
        var seeded = await SeedAsync(status);

        using var response = await AuthorizedClient().DeleteAsync(
            new Uri($"/v1/stores/{seeded.Store.Id}/products/{seeded.Product.Id}", UriKind.Relative));

        await AssertRefusedAsync(response, status);
        await AssertStoreUnchangedAsync(seeded.Store.Id);
    }

    [Theory]
    [MemberData(nameof(StoreStatusGuardTests.NonActiveStatuses), MemberType = typeof(StoreStatusGuardTests))]
    public async Task MyRemoveProduct_StoreNotActive_ReturnsConflictWithTheStatusKey(StoreStatus status, string _)
    {
        var seeded = await SeedAsync(status);

        using var response = await AuthorizedClient().DeleteAsync(
            new Uri($"/v1/stores/my/products/{seeded.Product.Id}", UriKind.Relative));

        await AssertRefusedAsync(response, status);
        await AssertStoreUnchangedAsync(seeded.Store.Id);
    }
}
