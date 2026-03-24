using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Bogus;
using Common.Domain.StronglyTypedIds;
using Common.Tests;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Persistence;
using Products.Domain.ProductTemplates;
using Products.Domain.Products;
using Products.Domain.Stores;
using Products.Endpoints.Products.v1.My.Get;
using Xunit;

namespace Products.Tests.Endpoints.Products;

[Collection("IntegrationTestCollection")]
public class MyGetTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public MyGetTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task MyGet_WithExistingOwnerProduct_ReturnsProductResponse()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();

        var ownerId = new ApplicationUserId(TestAuthHandler.DefaultUserId);
        var store = Store.Create(ownerId, _faker.Company.CompanyName(), _faker.Lorem.Sentence(), _faker.Address.FullAddress());
        var template = ProductTemplate.Create(_faker.Company.CompanyName(), _faker.Commerce.ProductName(), _faker.Commerce.Color());
        var product = Product.Create(store.Id, template.Id, "My Product", "My Desc", 10, 99.99m);
        store.AddProduct(product);

        db.Stores.Add(store);
        db.ProductTemplates.Add(template);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.GetAsync(new Uri($"/v1/products/my/{product.Id}", UriKind.Relative));

        // Assert
        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync();
            Assert.Fail($"Status: {response.StatusCode}. Error: {err}");
        }

        var result = await response.Content.ReadFromJsonAsync<Response>(JsonSerializerOptions);
        Assert.NotNull(result);
        Assert.Equal(product.Id, result.Id);
        Assert.Equal("My Product", result.Name);
        Assert.Equal(10, result.Quantity);
        Assert.Equal(99.99m, result.Price);
    }

    [Fact]
    public async Task MyGet_WithAnotherUserProduct_ReturnsNotFound()
    {
        // Arrange — product belongs to a different user's store
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();

        var otherStore = Store.Create(new ApplicationUserId(Guid.NewGuid()), "Other Store", "Desc", "Addr");
        var template = ProductTemplate.Create(_faker.Company.CompanyName(), _faker.Commerce.ProductName(), _faker.Commerce.Color());
        var otherProduct = Product.Create(otherStore.Id, template.Id, "Other Product", "Desc", 5, 5m);
        otherStore.AddProduct(otherProduct);

        db.Stores.Add(otherStore);
        db.ProductTemplates.Add(template);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act — authenticated as test user, but the product belongs to another user
        var response = await client.GetAsync(new Uri($"/v1/products/my/{otherProduct.Id}", UriKind.Relative));

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task MyGet_WithoutAuthentication_ReturnsUnauthorized()
    {
        // Arrange
        var client = Factory.CreateClient();

        // Act
        var response = await client.GetAsync(new Uri($"/v1/products/my/{ProductId.New()}", UriKind.Relative));

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
