using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Bogus;
using Common.Tests;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Persistence;
using Products.Endpoints.ProductTemplates.v1.Create;
using Xunit;

namespace Products.Tests.Endpoints.ProductTemplates;

[Collection("IntegrationTestCollection")]
public class CreateTests : BaseIntegrationTest
{
    private readonly Faker<Request> _faker;

    public CreateTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
        _faker = new Faker<Request>()
            .CustomInstantiator(f => new Request
            {
                Brand = f.Company.CompanyName(),
                Model = f.Commerce.ProductName(),
                Color = f.Commerce.Color()
            });
    }

    [Fact]
    public async Task Create_WithValidPayload_ReturnsOkAndPersistsProductTemplate()
    {
        // Arrange
        var request = _faker.Generate();
        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.PostAsJsonAsync(new Uri("/v1/product-templates", UriKind.Relative), request);

        // Assert
        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync();
            Assert.Fail($"Status: {response.StatusCode}. Error: {err}");
        }

        var responseBody = await response.Content.ReadFromJsonAsync<Response>(JsonSerializerOptions);
        Assert.NotNull(responseBody);

        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();
        var template = await db.ProductTemplates.FindAsync(responseBody.Id);

        Assert.NotNull(template);
        Assert.Equal(request.Brand, template.Brand);
        Assert.Equal(request.Model, template.Model);
        Assert.Equal(request.Color, template.Color);
        Assert.True(template.IsActive);
    }

    [Fact]
    public async Task Create_WithoutAuthentication_ReturnsUnauthorized()
    {
        // Arrange
        var request = _faker.Generate();
        var client = Factory.CreateClient();
        // NOT setting Authorization header

        // Act
        var response = await client.PostAsJsonAsync(new Uri("/v1/product-templates", UriKind.Relative), request);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
