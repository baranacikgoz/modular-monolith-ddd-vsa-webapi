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
using Products.Domain.Stores;
using Products.Endpoints.Products.v1.Search;
using DomainProduct = Products.Domain.Products.Product;
using Xunit;

namespace Products.Tests.Endpoints.Products;

[Collection("IntegrationTestCollection")]
public class SearchTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public SearchTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    private async Task<(Store store, DomainProduct product)> SeedStoreWithProductAsync(
        IProductsDbContext db,
        string productName = "TestProduct",
        string? productDescription = null,
        int quantity = 50,
        decimal price = 100m)
    {
        var ownerId = new ApplicationUserId(Guid.NewGuid());
        var store = Store.Create(ownerId, _faker.Company.CompanyName(), _faker.Lorem.Sentence(), _faker.Address.FullAddress());
        var template = ProductTemplate.Create(_faker.Company.CompanyName(), _faker.Commerce.ProductName(), _faker.Commerce.Color());
        var product = DomainProduct.Create(store.Id, template.Id, productName, productDescription ?? _faker.Lorem.Sentence(), quantity, price);
        store.AddProduct(product);

        db.Stores.Add(store);
        db.ProductTemplates.Add(template);
        await db.SaveChangesAsync();

        return (store, product);
    }

    [Fact]
    public async Task Search_WithNameFilter_ReturnsFilteredResults()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();

        var targetName = "UniqueProductNameABC";
        await SeedStoreWithProductAsync(db, productName: _faker.Commerce.ProductName());
        await SeedStoreWithProductAsync(db, productName: targetName);

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.GetAsync(new Uri($"/v1/products/search?PageNumber=1&PageSize=10&Name={targetName}", UriKind.Relative));

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<PaginationResponse<Response>>(JsonSerializerOptions);

        Assert.NotNull(result);
        Assert.Equal(1, result.TotalCount);
        Assert.Equal(targetName, result.Data.First().Name);
    }

    [Fact]
    public async Task Search_WithPriceRange_ReturnsFilteredResults()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();

        await SeedStoreWithProductAsync(db, price: 50m);
        await SeedStoreWithProductAsync(db, price: 150m);
        await SeedStoreWithProductAsync(db, price: 200m);

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.GetAsync(new Uri("/v1/products/search?PageNumber=1&PageSize=10&MinPrice=100&MaxPrice=180", UriKind.Relative));

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<PaginationResponse<Response>>(JsonSerializerOptions);

        Assert.NotNull(result);
        Assert.Equal(1, result.TotalCount);
        Assert.All(result.Data, p =>
        {
            Assert.True(p.Price >= 100m);
            Assert.True(p.Price <= 180m);
        });
    }

    [Fact]
    public async Task Search_WithoutAuthentication_ReturnsUnauthorized()
    {
        // Arrange
        var client = Factory.CreateClient();

        // Act
        var response = await client.GetAsync(new Uri("/v1/products/search", UriKind.Relative));

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Search_WithFtsSearchTerm_ReturnsMatchingProducts()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();

        // Seed one product whose name contains a unique FTS-matchable English word
        await SeedStoreWithProductAsync(db, productName: "Handcrafted Titanium Boots", productDescription: "durable outdoor footwear");
        // Seed other products that should NOT match
        await SeedStoreWithProductAsync(db, productName: "Plastic Chair", productDescription: "indoor furniture");
        await SeedStoreWithProductAsync(db, productName: "Ceramic Mug", productDescription: "morning beverage container");

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act — "titanium boots" targets only the first product via FTS
        var response = await client.GetAsync(new Uri("/v1/products/search?PageNumber=1&PageSize=10&SearchTerm=titanium+boots", UriKind.Relative));

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<PaginationResponse<Response>>(JsonSerializerOptions);

        Assert.NotNull(result);
        Assert.Equal(1, result.TotalCount);
        Assert.Equal("Handcrafted Titanium Boots", result.Data.First().Name);
    }

    [Fact]
    public async Task Search_WithFtsSearchTermMatchingDescription_ReturnsMatchingProducts()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();

        // FTS covers both Name and Description columns; seed a product whose description contains a unique word
        await SeedStoreWithProductAsync(db, productName: "Generic Item", productDescription: "crafted from recyclable bamboo material");
        await SeedStoreWithProductAsync(db, productName: "Another Item", productDescription: "made of standard plastic components");

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act — "bamboo" only exists in the first product's description
        var response = await client.GetAsync(new Uri("/v1/products/search?PageNumber=1&PageSize=10&SearchTerm=bamboo", UriKind.Relative));

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<PaginationResponse<Response>>(JsonSerializerOptions);

        Assert.NotNull(result);
        Assert.Equal(1, result.TotalCount);
        Assert.Equal("Generic Item", result.Data.First().Name);
    }

    [Fact]
    public async Task Search_WithSearchTermExceedingMaxLength_ReturnsBadRequest()
    {
        // Arrange
        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");
        var tooLongSearchTerm = new string('a', 257); // MaxLength is 256

        // Act
        var response = await client.GetAsync(new Uri($"/v1/products/search?PageNumber=1&PageSize=10&SearchTerm={tooLongSearchTerm}", UriKind.Relative));

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Search_WithSearchTermAndNameFilter_ReturnsOnlyIntersection()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();

        // Matches FTS but not the Name filter
        await SeedStoreWithProductAsync(db, productName: "Wireless Keyboard", productDescription: "ergonomic bluetooth device");
        // Matches both FTS and Name filter — only this one should appear
        await SeedStoreWithProductAsync(db, productName: "Wireless Mouse", productDescription: "ergonomic bluetooth device");
        // Matches Name filter but not FTS
        await SeedStoreWithProductAsync(db, productName: "Wireless Charger", productDescription: "fast inductive power supply");

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act — FTS matches "bluetooth" (first two); Name filter matches "Mouse" (only second)
        var response = await client.GetAsync(new Uri("/v1/products/search?PageNumber=1&PageSize=10&SearchTerm=bluetooth&Name=Wireless+Mouse", UriKind.Relative));

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<PaginationResponse<Response>>(JsonSerializerOptions);

        Assert.NotNull(result);
        Assert.Equal(1, result.TotalCount);
        Assert.Equal("Wireless Mouse", result.Data.First().Name);
    }
}
