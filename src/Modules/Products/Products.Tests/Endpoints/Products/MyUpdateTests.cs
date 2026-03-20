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
using Products.Endpoints.Products.v1.My.Update;
using Xunit;

namespace Products.Tests.Endpoints.Products;

[Collection("IntegrationTestCollection")]
public class MyUpdateTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public MyUpdateTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    private async Task<(Store store, Product product)> SeedOwnerProductAsync(IProductsDbContext db)
    {
        var ownerId = new ApplicationUserId(TestAuthHandler.DefaultUserId);
        var store = Store.Create(ownerId, _faker.Company.CompanyName(), _faker.Lorem.Sentence(), _faker.Address.FullAddress());
        var template = ProductTemplate.Create(_faker.Company.CompanyName(), _faker.Commerce.ProductName(), _faker.Commerce.Color());
        var product = Product.Create(store.Id, template.Id, "Original Name", "Original Desc", 5, 50m);
        store.AddProduct(product);

        db.Stores.Add(store);
        db.ProductTemplates.Add(template);
        await db.SaveChangesAsync();

        return (store, product);
    }

    [Fact]
    public async Task MyUpdate_WithValidPayload_ReturnsNoContentAndUpdatesProduct()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();
        var (_, product) = await SeedOwnerProductAsync(db);

        var body = new Request.RequestBody
        {
            Name = _faker.Commerce.ProductName(),
            Description = _faker.Lorem.Sentence(),
            Quantity = _faker.Random.Int(1, 50),
            Price = _faker.Random.Decimal(10m, 500m)
        };

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.PutAsJsonAsync(
            new Uri($"/v1/products/my/{product.Id}", UriKind.Relative),
            body
        );

        // Assert
        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync();
            Assert.Fail($"Status: {response.StatusCode}. Error: {err}");
        }

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        using var checkScope = Factory.Services.CreateScope();
        var checkDb = checkScope.ServiceProvider.GetRequiredService<IProductsDbContext>();
        var updated = await checkDb.Products.FindAsync(product.Id);

        Assert.NotNull(updated);
        Assert.Equal(body.Name, updated.Name);
        Assert.Equal(body.Description, updated.Description);
        Assert.Equal(body.Quantity, updated.Quantity);
        Assert.Equal(body.Price, updated.Price);
    }

    [Fact]
    public async Task MyUpdate_WithAnotherUserProduct_ReturnsNotFound()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();

        var otherStore = Store.Create(new ApplicationUserId(Guid.NewGuid()), "Other Store", "Desc", "Addr");
        var template = ProductTemplate.Create(_faker.Company.CompanyName(), _faker.Commerce.ProductName(), _faker.Commerce.Color());
        var otherProduct = Product.Create(otherStore.Id, template.Id, "Other Product", "Desc", 5, 5m);
        otherStore.AddProduct(otherProduct);

        db.Stores.Add(otherStore);
        db.ProductTemplates.Add(template);
        await db.SaveChangesAsync();

        var body = new Request.RequestBody { Name = "Updated Name" };

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.PutAsJsonAsync(
            new Uri($"/v1/products/my/{otherProduct.Id}", UriKind.Relative),
            body
        );

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task MyUpdate_WithoutAuthentication_ReturnsUnauthorized()
    {
        // Arrange
        var client = Factory.CreateClient();
        var body = new Request.RequestBody { Name = "Name" };

        // Act
        var response = await client.PutAsJsonAsync(
            new Uri($"/v1/products/my/{ProductId.New()}", UriKind.Relative),
            body
        );

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
