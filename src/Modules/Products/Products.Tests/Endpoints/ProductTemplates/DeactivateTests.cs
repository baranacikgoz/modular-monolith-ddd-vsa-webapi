using System.Net;
using System.Net.Http.Headers;
using Bogus;
using Common.Tests;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Persistence;
using Products.Domain.ProductTemplates;
using Xunit;

namespace Products.Tests.Endpoints.ProductTemplates;

[Collection("IntegrationTestCollection")]
public class DeactivateTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public DeactivateTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Deactivate_WithExistingActiveTemplate_ReturnsNoContentAndDeactivatesTemplate()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();

        var template = ProductTemplate.Create(_faker.Company.CompanyName(), _faker.Commerce.ProductName(), _faker.Commerce.Color());
        // Templates start Active by default
        db.ProductTemplates.Add(template);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.GetAsync(new Uri($"/v1/product-templates/{template.Id}/deactivate", UriKind.Relative));

        // Assert
        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync();
            Assert.Fail($"Status: {response.StatusCode}. Error: {err}");
        }

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        using var checkScope = Factory.Services.CreateScope();
        var checkDb = checkScope.ServiceProvider.GetRequiredService<IProductsDbContext>();
        var updated = await checkDb.ProductTemplates.FindAsync(template.Id);

        Assert.NotNull(updated);
        Assert.False(updated.IsActive);
    }

    [Fact]
    public async Task Deactivate_WithNonExistentTemplate_ReturnsNotFound()
    {
        // Arrange
        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.GetAsync(new Uri($"/v1/product-templates/{ProductTemplateId.New()}/deactivate", UriKind.Relative));

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Deactivate_WithoutAuthentication_ReturnsUnauthorized()
    {
        // Arrange
        var client = Factory.CreateClient();

        // Act
        var response = await client.GetAsync(new Uri($"/v1/product-templates/{ProductTemplateId.New()}/deactivate", UriKind.Relative));

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
