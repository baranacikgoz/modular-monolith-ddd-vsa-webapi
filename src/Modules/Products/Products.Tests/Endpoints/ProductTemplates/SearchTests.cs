using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Bogus;
using Common.Application.Pagination;
using Common.Tests;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Persistence;
using Products.Domain.ProductTemplates;
using Products.Endpoints.ProductTemplates.v1.Search;
using Xunit;

namespace Products.Tests.Endpoints.ProductTemplates;

[Collection("IntegrationTestCollection")]
public class SearchTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public SearchTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    private static ProductTemplate MakeTemplate(string brand, string model, string color)
        => ProductTemplate.Create(brand, model, color);

    [Fact]
    public async Task Search_WithBrandFilter_ReturnsFilteredResults()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();

        var targetBrand = "TargetBrandXYZ";
        db.ProductTemplates.AddRange(
            MakeTemplate(_faker.Company.CompanyName(), _faker.Commerce.ProductName(), _faker.Commerce.Color()),
            MakeTemplate(_faker.Company.CompanyName(), _faker.Commerce.ProductName(), _faker.Commerce.Color()),
            MakeTemplate(targetBrand, "Model A", "Red"),
            MakeTemplate(targetBrand, "Model B", "Blue")
        );
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.GetAsync(new Uri($"/v1/product-templates/search?PageNumber=1&PageSize=10&Brand={targetBrand}", UriKind.Relative));

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<PaginationResponse<Response>>(JsonSerializerOptions);

        Assert.NotNull(result);
        Assert.Equal(2, result.TotalCount);
        Assert.All(result.Data, item => Assert.Equal(targetBrand, item.Brand));
    }

    [Fact]
    public async Task Search_WithColorFilter_ReturnsFilteredResults()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();

        var targetColor = "UniqueColorZZZ";
        db.ProductTemplates.AddRange(
            MakeTemplate(_faker.Company.CompanyName(), _faker.Commerce.ProductName(), _faker.Commerce.Color()),
            MakeTemplate("Brand Z", "Model C", targetColor)
        );
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.GetAsync(new Uri($"/v1/product-templates/search?PageNumber=1&PageSize=10&Color={targetColor}", UriKind.Relative));

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<PaginationResponse<Response>>(JsonSerializerOptions);

        Assert.NotNull(result);
        Assert.Equal(1, result.TotalCount);
        Assert.Equal(targetColor, result.Data.First().Color);
    }

    [Fact]
    public async Task Search_WithoutAuthentication_ReturnsUnauthorized()
    {
        // Arrange
        var client = Factory.CreateClient();

        // Act
        var response = await client.GetAsync(new Uri("/v1/product-templates/search?PageNumber=1&PageSize=10", UriKind.Relative));

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
