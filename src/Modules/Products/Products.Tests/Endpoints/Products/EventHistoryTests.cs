using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Bogus;
using Common.Application.EventHistory;
using Common.Application.Pagination;
using Common.Domain.StronglyTypedIds;
using Common.Tests;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Persistence;
using Products.Domain.ProductTemplates;
using Products.Domain.Products;
using Products.Domain.Stores;
using Xunit;

namespace Products.Tests.Endpoints.Products;

[Collection("IntegrationTestCollection")]
public class EventHistoryTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public EventHistoryTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task EventHistory_WithExistingProduct_ReturnsOkWithPaginationResponse()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();

        var store = Store.Create(new ApplicationUserId(Guid.NewGuid()),
            _faker.Company.CompanyName(), _faker.Lorem.Sentence(), _faker.Address.FullAddress());
        var template = ProductTemplate.Create(_faker.Company.CompanyName(), _faker.Commerce.ProductName(), _faker.Commerce.Color());
        var product = Product.Create(store.Id, template.Id, "Product", "Desc", 10, 10m);
        store.AddProduct(product);
        db.Stores.Add(store);
        db.ProductTemplates.Add(template);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.GetAsync(new Uri($"/v1/products/{product.Id}/event-history?PageNumber=1&PageSize=10", UriKind.Relative));

        // Assert
        // The event history response should be 200 OK even if no events exist yet
        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync();
            Assert.Fail($"Status: {response.StatusCode}. Error: {err}");
        }

        var result = await response.Content.ReadFromJsonAsync<PaginationResponse<EventDto>>(JsonSerializerOptions);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task EventHistory_WithoutAuthentication_ReturnsUnauthorized()
    {
        // Arrange
        var client = Factory.CreateClient();

        // Act
        var response = await client.GetAsync(new Uri($"/v1/products/{ProductId.New()}/event-history?PageNumber=1&PageSize=10", UriKind.Relative));

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
