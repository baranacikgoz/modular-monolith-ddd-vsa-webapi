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
        int quantity = 50,
        decimal price = 100m)
    {
        var ownerId = new ApplicationUserId(Guid.NewGuid());
        var store = Store.Create(ownerId, _faker.Company.CompanyName(), _faker.Lorem.Sentence(), _faker.Address.FullAddress());
        var template = ProductTemplate.Create(_faker.Company.CompanyName(), _faker.Commerce.ProductName(), _faker.Commerce.Color());
        var product = DomainProduct.Create(store.Id, template.Id, productName, _faker.Lorem.Sentence(), quantity, price);
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
}
