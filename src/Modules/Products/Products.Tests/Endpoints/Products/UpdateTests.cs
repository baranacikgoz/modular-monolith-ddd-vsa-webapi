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
using Products.Endpoints.Products.v1.Update;
using Xunit;

namespace Products.Tests.Endpoints.Products;

[Collection("IntegrationTestCollection")]
public class UpdateTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public UpdateTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Update_WithValidPayload_ReturnsNoContentAndUpdatesProduct()
    {
        // Arrange — admin endpoint: no owner filtering
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();

        var store = Store.Create(new ApplicationUserId(Guid.NewGuid()), _faker.Company.CompanyName(), _faker.Lorem.Sentence(), _faker.Address.FullAddress());
        var template = ProductTemplate.Create(_faker.Company.CompanyName(), _faker.Commerce.ProductName(), _faker.Commerce.Color());
        var product = Product.Create(store.Id, template.Id, "Original", "OriginalDesc", 5, 50m);
        store.AddProduct(product);
        db.Stores.Add(store);
        db.ProductTemplates.Add(template);
        await db.SaveChangesAsync();

        var body = new Request.RequestBody
        {
            Name = _faker.Commerce.ProductName(),
            Quantity = _faker.Random.Int(1, 200),
            Price = _faker.Random.Decimal(1m, 999m)
        };

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.PutAsJsonAsync(
            new Uri($"/v1/products/{product.Id}", UriKind.Relative),
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
        Assert.Equal(body.Quantity, updated.Quantity);
        Assert.Equal(body.Price, updated.Price);
    }

    [Fact]
    public async Task Update_WithNonExistentId_ReturnsNotFound()
    {
        // Arrange
        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");
        var body = new Request.RequestBody { Name = "New Name" };

        // Act
        var response = await client.PutAsJsonAsync(
            new Uri($"/v1/products/{ProductId.New()}", UriKind.Relative),
            body
        );

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Update_WithoutAuthentication_ReturnsUnauthorized()
    {
        // Arrange
        var client = Factory.CreateClient();
        var body = new Request.RequestBody { Name = "Name" };

        // Act
        var response = await client.PutAsJsonAsync(
            new Uri($"/v1/products/{ProductId.New()}", UriKind.Relative),
            body
        );

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
