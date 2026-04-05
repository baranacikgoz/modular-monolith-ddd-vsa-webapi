using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Bogus;
using Common.Application.Pagination;
using Common.Domain.StronglyTypedIds;
using Common.Tests;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Persistence;
using Products.Domain.ProductTemplates;
using Products.Domain.Products;
using Products.Domain.Stores;
using Products.Endpoints.Products.v1.My.Search;
using Xunit;

namespace Products.Tests.Endpoints.Products;

[Collection("IntegrationTestCollection")]
public class MySearchTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public MySearchTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task MySearch_ReturnsOnlyOwnerProducts()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();

        // Seed the test user's store with 2 products
        var ownerId = new ApplicationUserId(TestAuthHandler.DefaultUserId);
        var myStore = Store.Create(ownerId, _faker.Company.CompanyName(), _faker.Lorem.Sentence(), _faker.Address.FullAddress());
        var template = ProductTemplate.Create(_faker.Company.CompanyName(), _faker.Commerce.ProductName(), _faker.Commerce.Color());
        var myProduct1 = Product.Create(myStore.Id, template.Id, "My Product 1", "Desc 1", 10, 10m);
        var myProduct2 = Product.Create(myStore.Id, template.Id, "My Product 2", "Desc 2", 20, 20m);
        myStore.AddProduct(myProduct1);
        myStore.AddProduct(myProduct2);

        // Seed another user's store with 1 product (should NOT appear in results)
        var otherStore = Store.Create(new ApplicationUserId(Guid.NewGuid()), "Other Store", "Desc", "Address");
        var otherProduct = Product.Create(otherStore.Id, template.Id, "Other Product", "Desc", 5, 5m);
        otherStore.AddProduct(otherProduct);

        db.Stores.AddRange(myStore, otherStore);
        db.ProductTemplates.Add(template);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.GetAsync(new Uri("/v1/products/my/search?PageNumber=1&PageSize=10", UriKind.Relative));

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<PaginationResponse<Response>>(JsonSerializerOptions);

        Assert.NotNull(result);
        Assert.Equal(2, result.TotalCount);
        Assert.All(result.Data, p => Assert.True(
            p.Name == "My Product 1" || p.Name == "My Product 2",
            $"Unexpected product name: {p.Name}"));
    }

    [Fact]
    public async Task MySearch_WithNameFilter_ReturnsFilteredResults()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();

        var ownerId = new ApplicationUserId(TestAuthHandler.DefaultUserId);
        var store = Store.Create(ownerId, _faker.Company.CompanyName(), _faker.Lorem.Sentence(), _faker.Address.FullAddress());
        var template = ProductTemplate.Create(_faker.Company.CompanyName(), _faker.Commerce.ProductName(), _faker.Commerce.Color());

        var targetProduct = Product.Create(store.Id, template.Id, "SpecialWidgetZZZ", "Desc", 10, 10m);
        var otherProduct = Product.Create(store.Id, template.Id, "Regular Product", "Desc", 5, 5m);
        store.AddProduct(targetProduct);
        store.AddProduct(otherProduct);

        db.Stores.Add(store);
        db.ProductTemplates.Add(template);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.GetAsync(new Uri("/v1/products/my/search?PageNumber=1&PageSize=10&Name=SpecialWidgetZZZ", UriKind.Relative));

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<PaginationResponse<Response>>(JsonSerializerOptions);

        Assert.NotNull(result);
        Assert.Equal(1, result.TotalCount);
        Assert.Equal("SpecialWidgetZZZ", result.Data.First().Name);
    }

    [Fact]
    public async Task MySearch_WithoutAuthentication_ReturnsUnauthorized()
    {
        // Arrange
        var client = Factory.CreateClient();

        // Act
        var response = await client.GetAsync(new Uri("/v1/products/my/search?PageNumber=1&PageSize=10", UriKind.Relative));

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task MySearch_WithFtsSearchTerm_ReturnsMatchingOwnProducts()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();

        var ownerId = new ApplicationUserId(TestAuthHandler.DefaultUserId);
        var store = Store.Create(ownerId, "My FTS Store", "Store for FTS test", "123 Test Street");
        var template = ProductTemplate.Create("TestBrand", "TestModel", "TestColor");

        // Matching product: has FTS-matchable word "velvet" in its name
        var matchingProduct = Product.Create(store.Id, template.Id, "Velvet Curtains", "decorative window treatment", 10, 50m);
        // Non-matching product: does not contain "velvet"
        var otherProduct = Product.Create(store.Id, template.Id, "Cotton Pillow", "comfortable sleeping accessory", 5, 20m);
        store.AddProduct(matchingProduct);
        store.AddProduct(otherProduct);

        db.Stores.Add(store);
        db.ProductTemplates.Add(template);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.GetAsync(new Uri("/v1/products/my/search?PageNumber=1&PageSize=10&SearchTerm=velvet", UriKind.Relative));

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<PaginationResponse<Response>>(JsonSerializerOptions);

        Assert.NotNull(result);
        Assert.Equal(1, result.TotalCount);
        Assert.Equal("Velvet Curtains", result.Data.First().Name);
    }

    [Fact]
    public async Task MySearch_WithSearchTermExceedingMaxLength_ReturnsBadRequest()
    {
        // Arrange
        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");
        var tooLongSearchTerm = new string('a', 257); // MaxLength is 256

        // Act
        var response = await client.GetAsync(new Uri($"/v1/products/my/search?PageNumber=1&PageSize=10&SearchTerm={tooLongSearchTerm}", UriKind.Relative));

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
