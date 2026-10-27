using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Common.Application.Options;
using Common.Domain.StronglyTypedIds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Products.Domain.Products;
using Products.Domain.Products.DomainEvents.v1;
using Products.Tests.Support;
using Xunit;
using MyGetResponse = Products.Endpoints.Products.v1.My.Get.Response;

namespace Products.Tests.Endpoints.Products;

public class MySetAttributesTests(IntegrationTestWebAppFactory factory) : ProductsIntegrationTest(factory)
{
    private static object Body(params (string Name, string Value)[] attributes)
    {
        return new { attributes = attributes.Select(a => new { name = a.Name, value = a.Value }).ToArray() };
    }

    private async Task<Product> SeedProductAsync(ApplicationUserId? owner = null)
    {
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, owner ?? DefaultUser, "Lamp");
        return seeded.Products[0];
    }

    private static Uri Route(Product product) => new($"/v1/products/my/{product.Id}/attributes", UriKind.Relative);

    private static async Task<string?> ErrorKeyAsync(HttpResponseMessage response)
    {
        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        return json.RootElement.GetProperty("errorKey").GetString();
    }

    [Fact]
    public async Task MySetAttributes_WithAList_ReplacesTheWholeListAndRecordsTheEvent()
    {
        var product = await SeedProductAsync();
        product.SetAttributes([new ProductAttribute("Old", "Value")], maxAttributes: 10);
        await Db.SaveChangesAsync();

        using var response = await AuthorizedClient().PutAsJsonAsync(Route(product), Body((" Color ", " Red "), ("Size", "L")));

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        var stored = await Db.Products.AsNoTracking().SingleAsync(p => p.Id == product.Id);
        Assert.Equal([new ProductAttribute("Color", "Red"), new ProductAttribute("Size", "L")], stored.Attributes);
        var audited = await Db.AuditLog.AsNoTracking().Where(e => e.AggregateId == product.Id.Value && e.Version == stored.Version).SingleAsync();
        Assert.IsType<V1ProductAttributesReplacedDomainEvent>(audited.Event);
    }

    [Fact]
    public async Task MySetAttributes_OnAnotherOwnersProduct_ReturnsNotFound()
    {
        var product = await SeedProductAsync(new ApplicationUserId(Guid.NewGuid()));

        using var response = await AuthorizedClient().PutAsJsonAsync(Route(product), Body(("Color", "Red")));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task MySetAttributes_BeyondTheConfiguredMaximum_ReturnsBadRequest()
    {
        var product = await SeedProductAsync();
        var max = Factory.Services.GetRequiredService<IOptions<ProductsOptions>>().Value.MaxAttributesPerProduct;
        var tooMany = Enumerable.Range(0, max + 1).Select(i => ($"Name{i}", "Value")).ToArray();

        using var response = await AuthorizedClient().PutAsJsonAsync(Route(product), Body(tooMany));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("TooManyAttributes", await ErrorKeyAsync(response));
    }

    [Fact]
    public async Task MySetAttributes_WithTheSameNameTwice_ReturnsBadRequest()
    {
        var product = await SeedProductAsync();

        using var response = await AuthorizedClient().PutAsJsonAsync(Route(product), Body(("Color", "Red"), ("COLOR", "Blue")));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("DuplicateAttributeName", await ErrorKeyAsync(response));
    }

    [Fact]
    public async Task MySetAttributes_WithABlankName_ReturnsBadRequest()
    {
        var product = await SeedProductAsync();

        using var response = await AuthorizedClient().PutAsJsonAsync(Route(product), Body(("  ", "Red")));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task MyGet_ReturnsTheAttributesOfTheProduct()
    {
        var product = await SeedProductAsync();
        using var set = await AuthorizedClient().PutAsJsonAsync(Route(product), Body(("Color", "Red")));

        using var response = await AuthorizedClient().GetAsync(new Uri($"/v1/products/my/{product.Id}", UriKind.Relative));

        var body = await response.Content.ReadFromJsonAsync<MyGetResponse>(JsonSerializerOptions);
        Assert.Equal([new ProductAttribute("Color", "Red")], body!.Attributes);
    }
}
