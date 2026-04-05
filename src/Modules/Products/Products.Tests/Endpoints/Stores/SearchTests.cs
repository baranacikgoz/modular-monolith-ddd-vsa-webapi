using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Bogus;
using Common.Application.Pagination;
using Common.Domain.StronglyTypedIds;
using Common.Tests;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Persistence;
using Products.Domain.Stores;
using Products.Endpoints.Stores.v1.Search;
using Xunit;

namespace Products.Tests.Endpoints.Stores;

[Collection("IntegrationTestCollection")]
public class SearchTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public SearchTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Search_WithNameFilter_ReturnsFilteredResults()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();

        var targetName = "UniqueStoreName123";

        db.Stores.AddRange(
            Store.Create(new ApplicationUserId(Guid.NewGuid()), _faker.Company.CompanyName(), _faker.Lorem.Sentence(), _faker.Address.FullAddress()),
            Store.Create(new ApplicationUserId(Guid.NewGuid()), _faker.Company.CompanyName(), _faker.Lorem.Sentence(), _faker.Address.FullAddress()),
            Store.Create(new ApplicationUserId(Guid.NewGuid()), targetName, "Desc", "Address"),
            Store.Create(new ApplicationUserId(Guid.NewGuid()), targetName + " Branch", "Desc 2", "Address 2")
        );
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.GetAsync(new Uri($"/v1/stores/search?PageNumber=1&PageSize=50&Name={targetName}", UriKind.Relative));

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<PaginationResponse<Response>>(JsonSerializerOptions);

        Assert.NotNull(result);
        Assert.Equal(2, result.TotalCount);
        Assert.All(result.Data, item => Assert.Contains(targetName, item.Name, StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public async Task Search_WithoutFilters_ReturnsPaginatedResults()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();

        for (var i = 0; i < 12; i++)
        {
            db.Stores.Add(Store.Create(new ApplicationUserId(Guid.NewGuid()),
                _faker.Company.CompanyName(), _faker.Lorem.Sentence(), _faker.Address.FullAddress()));
        }
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.GetAsync(new Uri("/v1/stores/search?PageNumber=1&PageSize=10", UriKind.Relative));

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<PaginationResponse<Response>>(JsonSerializerOptions);

        Assert.NotNull(result);
        Assert.True(result.TotalCount >= 12);
        Assert.Equal(10, result.Data.Count);
    }

    [Fact]
    public async Task Search_WithoutAuthentication_ReturnsUnauthorized()
    {
        // Arrange
        var client = Factory.CreateClient();

        // Act
        var response = await client.GetAsync(new Uri("/v1/stores/search?PageNumber=1&PageSize=10", UriKind.Relative));

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Search_WithFtsSearchTerm_ReturnsMatchingStores()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();

        // Matching store: name contains FTS-matchable unique word "artisan"
        db.Stores.Add(Store.Create(new ApplicationUserId(Guid.NewGuid()), "Artisan Bakery", "freshly baked goods daily", "10 Maple Avenue"));
        // Non-matching stores
        db.Stores.Add(Store.Create(new ApplicationUserId(Guid.NewGuid()), "Tech Supplies", "electronic components and gadgets", "20 Silicon Road"));
        db.Stores.Add(Store.Create(new ApplicationUserId(Guid.NewGuid()), "Garden Center", "plants and gardening tools", "30 Green Lane"));
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act — "artisan" only matches the first store
        var response = await client.GetAsync(new Uri("/v1/stores/search?PageNumber=1&PageSize=10&SearchTerm=artisan", UriKind.Relative));

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<PaginationResponse<Response>>(JsonSerializerOptions);

        Assert.NotNull(result);
        Assert.Equal(1, result.TotalCount);
        Assert.Equal("Artisan Bakery", result.Data.First().Name);
    }

    [Fact]
    public async Task Search_WithFtsSearchTermMatchingAddress_ReturnsMatchingStores()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();

        // FTS covers Name + Description + Address; seed a store whose address has a unique word
        db.Stores.Add(Store.Create(new ApplicationUserId(Guid.NewGuid()), "General Store", "everyday products", "45 Cobblestone Marketplace"));
        db.Stores.Add(Store.Create(new ApplicationUserId(Guid.NewGuid()), "City Shop", "urban goods", "99 Downtown Boulevard"));
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act — "cobblestone" only exists in the first store's address
        var response = await client.GetAsync(new Uri("/v1/stores/search?PageNumber=1&PageSize=10&SearchTerm=cobblestone", UriKind.Relative));

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<PaginationResponse<Response>>(JsonSerializerOptions);

        Assert.NotNull(result);
        Assert.Equal(1, result.TotalCount);
        Assert.Equal("General Store", result.Data.First().Name);
    }

    [Fact]
    public async Task Search_WithSearchTermExceedingMaxLength_ReturnsBadRequest()
    {
        // Arrange
        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");
        var tooLongSearchTerm = new string('a', 257); // MaxLength is 256

        // Act
        var response = await client.GetAsync(new Uri($"/v1/stores/search?PageNumber=1&PageSize=10&SearchTerm={tooLongSearchTerm}", UriKind.Relative));

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
