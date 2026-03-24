using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Bogus;
using Common.Domain.StronglyTypedIds;
using Common.Tests;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Persistence;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;
using Products.Endpoints.Stores.v1.My.AddProduct;
using Xunit;

namespace Products.Tests.Endpoints.Stores;

[Collection("IntegrationTestCollection")]
public class MyAddProductTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public MyAddProductTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    /// <summary>
    /// Serializes the request body as a plain JSON object with productTemplateId as a raw Guid,
    /// because <see cref="StronglyTypedIdReadOnlyJsonConverter{TId}"/> does not support Write.
    /// </summary>
    private static string BuildJson(Guid templateId, string name, string description, int quantity, decimal price)
        => JsonSerializer.Serialize(new { productTemplateId = templateId, name, description, quantity, price });

    [Fact]
    public async Task MyAddProduct_WithValidPayload_ReturnsOkAndPersistsProduct()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();
        var ownerId = new ApplicationUserId(TestAuthHandler.DefaultUserId);

        var store = Store.Create(ownerId, _faker.Company.CompanyName(), _faker.Lorem.Sentence(), _faker.Address.FullAddress());
        db.Stores.Add(store);

        var template = ProductTemplate.Create(_faker.Company.CompanyName(), _faker.Commerce.ProductName(), _faker.Commerce.Color());
        db.ProductTemplates.Add(template);
        await db.SaveChangesAsync();

        var name = _faker.Commerce.ProductName();
        var description = _faker.Lorem.Sentence();
        var quantity = _faker.Random.Int(1, 100);
        var price = _faker.Random.Decimal(1m, 1000m);

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        HttpResponseMessage response;
        using (var content = new StringContent(BuildJson(template.Id.Value, name, description, quantity, price), Encoding.UTF8, "application/json"))
        {
            response = await client.PostAsync(new Uri("/v1/stores/my/products", UriKind.Relative), content);
        }

        // Assert
        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync();
            Assert.Fail($"Status: {response.StatusCode}. Error: {err}");
        }

        var responseBody = await response.Content.ReadFromJsonAsync<Response>(JsonSerializerOptions);
        Assert.NotNull(responseBody);

        using var checkScope = Factory.Services.CreateScope();
        var checkDb = checkScope.ServiceProvider.GetRequiredService<IProductsDbContext>();
        var product = await checkDb.Products.FindAsync(responseBody.Id);

        Assert.NotNull(product);
        Assert.Equal(store.Id, product.StoreId);
        Assert.Equal(template.Id, product.ProductTemplateId);
        Assert.Equal(name, product.Name);
        Assert.Equal(quantity, product.Quantity);
        Assert.Equal(price, product.Price);
    }

    [Fact]
    public async Task MyAddProduct_WithNoStore_ReturnsNotFound()
    {
        // Arrange - no store, but seed a template
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();
        var template = ProductTemplate.Create(_faker.Company.CompanyName(), _faker.Commerce.ProductName(), _faker.Commerce.Color());
        db.ProductTemplates.Add(template);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        HttpResponseMessage response;
        using (var content = new StringContent(BuildJson(template.Id.Value, "Product", "Desc", 5, 99.99m), Encoding.UTF8, "application/json"))
        {
            response = await client.PostAsync(new Uri("/v1/stores/my/products", UriKind.Relative), content);
        }

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task MyAddProduct_WithInactiveTemplate_ReturnsNotFound()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();
        var ownerId = new ApplicationUserId(TestAuthHandler.DefaultUserId);

        var store = Store.Create(ownerId, _faker.Company.CompanyName(), _faker.Lorem.Sentence(), _faker.Address.FullAddress());
        db.Stores.Add(store);

        var inactiveTemplate = ProductTemplate.Create(_faker.Company.CompanyName(), _faker.Commerce.ProductName(), _faker.Commerce.Color());
        inactiveTemplate.Deactivate();
        db.ProductTemplates.Add(inactiveTemplate);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        HttpResponseMessage response;
        using (var content = new StringContent(BuildJson(inactiveTemplate.Id.Value, "Product", "Desc", 1, 10m), Encoding.UTF8, "application/json"))
        {
            response = await client.PostAsync(new Uri("/v1/stores/my/products", UriKind.Relative), content);
        }

        // Assert - template is inactive, so it won't be found
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task MyAddProduct_WithoutAuthentication_ReturnsUnauthorized()
    {
        // Arrange
        var client = Factory.CreateClient();

        HttpResponseMessage response;
        using (var content = new StringContent(BuildJson(ProductTemplateId.New().Value, "Product", "Desc", 1, 10m), Encoding.UTF8, "application/json"))
        {
            response = await client.PostAsync(new Uri("/v1/stores/my/products", UriKind.Relative), content);
        }

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
