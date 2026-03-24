using System.Net;
using System.Net.Http.Headers;
using Bogus;
using Common.Domain.StronglyTypedIds;
using Common.Tests;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Persistence;
using Products.Domain.ProductTemplates;
using Products.Domain.Products;
using Products.Domain.Stores;
using Xunit;

namespace Products.Tests.Endpoints.Stores;

[Collection("IntegrationTestCollection")]
public class MyRemoveProductTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public MyRemoveProductTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task MyRemoveProduct_WithExistingProduct_ReturnsNoContentAndRemovesProduct()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();
        var ownerId = new ApplicationUserId(TestAuthHandler.DefaultUserId);

        // Seed store, template, and product
        var store = Store.Create(ownerId, _faker.Company.CompanyName(), _faker.Lorem.Sentence(), _faker.Address.FullAddress());
        db.Stores.Add(store);

        var template = ProductTemplate.Create(_faker.Company.CompanyName(), _faker.Commerce.ProductName(), _faker.Commerce.Color());
        db.ProductTemplates.Add(template);

        var product = Product.Create(store.Id, template.Id, "Product", "Desc", 10, 9.99m);
        store.AddProduct(product);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.DeleteAsync(new Uri($"/v1/stores/my/products/{product.Id}", UriKind.Relative));

        // Assert
        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync();
            Assert.Fail($"Status: {response.StatusCode}. Error: {err}");
        }

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        using var checkScope = Factory.Services.CreateScope();
        var checkDb = checkScope.ServiceProvider.GetRequiredService<IProductsDbContext>();
        var removedProduct = await checkDb.Products.FindAsync(product.Id);
        Assert.Null(removedProduct); // product should be removed
    }

    [Fact]
    public async Task MyRemoveProduct_WithNoStore_ReturnsNotFound()
    {
        // Arrange - no store seeded for the test user
        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.DeleteAsync(new Uri($"/v1/stores/my/products/{ProductId.New()}", UriKind.Relative));

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task MyRemoveProduct_WithoutAuthentication_ReturnsUnauthorized()
    {
        // Arrange
        var client = Factory.CreateClient();

        // Act
        var response = await client.DeleteAsync(new Uri($"/v1/stores/my/products/{ProductId.New()}", UriKind.Relative));

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
