using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Bogus;
using Common.Tests;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Persistence;
using Products.Domain.ProductTemplates;
using Products.Endpoints.ProductTemplates.v1.Get;
using Xunit;

namespace Products.Tests.Endpoints.ProductTemplates;

[Collection("IntegrationTestCollection")]
public class GetTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public GetTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Get_WithExistingId_ReturnsOkAndTemplate()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();

        var template = ProductTemplate.Create(_faker.Company.CompanyName(), _faker.Commerce.ProductName(), _faker.Commerce.Color());
        db.ProductTemplates.Add(template);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.GetAsync(new Uri($"/v1/product-templates/{template.Id}", UriKind.Relative));

        // Assert
        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync();
            Assert.Fail($"Status: {response.StatusCode}. Error: {err}");
        }

        var result = await response.Content.ReadFromJsonAsync<Response>(JsonSerializerOptions);

        Assert.NotNull(result);
        Assert.Equal(template.Id, result.Id);
        Assert.Equal(template.Brand, result.Brand);
        Assert.Equal(template.Model, result.Model);
        Assert.Equal(template.Color, result.Color);
    }

    [Fact]
    public async Task Get_WithNonExistentId_ReturnsNotFound()
    {
        // Arrange
        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.GetAsync(new Uri($"/v1/product-templates/{ProductTemplateId.New()}", UriKind.Relative));

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        // A NotFound raised without a value (every SingleAsResultAsync miss) must not leak the resource
        // string's unformatted placeholder into the title.
        var body = await response.Content.ReadFromJsonAsync<JsonDocument>(JsonSerializerOptions);
        Assert.DoesNotContain("{0}", body!.RootElement.GetProperty("title").GetString(), StringComparison.Ordinal);
    }

    [Fact]
    public async Task Search_WithUnbindableQueryParameter_ReturnsBadRequestInTheCommonErrorEnvelope()
    {
        // Arrange
        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act: model binding fails before FluentValidation runs, so the body comes from the exception middleware.
        var response = await client.GetAsync(new Uri("/v1/product-templates/search?PageNumber=abc&PageSize=10", UriKind.Relative));

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<JsonDocument>(JsonSerializerOptions);
        Assert.Equal("BadHttpRequestException", body!.RootElement.GetProperty("errorKey").GetString());
        Assert.True(body.RootElement.GetProperty("errors").TryGetProperty(string.Empty, out _));
    }

    [Fact]
    public async Task Get_WithoutAuthentication_ReturnsUnauthorized()
    {
        // Arrange
        var client = Factory.CreateClient();

        // Act
        var response = await client.GetAsync(new Uri($"/v1/product-templates/{ProductTemplateId.New()}", UriKind.Relative));

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
