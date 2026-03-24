using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Bogus;
using Common.Domain.StronglyTypedIds;
using Common.Tests;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Persistence;
using Products.Domain.Stores;
using Products.Endpoints.Stores.v1.My.Update;
using Xunit;

namespace Products.Tests.Endpoints.Stores;

[Collection("IntegrationTestCollection")]
public class MyUpdateTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public MyUpdateTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task MyUpdate_WithValidPayload_ReturnsNoContentAndUpdatesStore()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();
        var ownerId = new ApplicationUserId(TestAuthHandler.DefaultUserId);

        var store = Store.Create(ownerId, "Original Name", "Original Desc", "Original Address");
        db.Stores.Add(store);
        await db.SaveChangesAsync();

        var request = new Request
        {
            Name = _faker.Company.CompanyName(),
            Description = _faker.Lorem.Sentence(),
            Address = _faker.Address.FullAddress()
        };

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.PutAsJsonAsync(new Uri("/v1/stores/my", UriKind.Relative), request);

        // Assert
        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync();
            Assert.Fail($"Status: {response.StatusCode}. Error: {err}");
        }

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        using var checkScope = Factory.Services.CreateScope();
        var checkDb = checkScope.ServiceProvider.GetRequiredService<IProductsDbContext>();
        var updated = await checkDb.Stores.FindAsync(store.Id);

        Assert.NotNull(updated);
        Assert.Equal(request.Name, updated.Name);
        Assert.Equal(request.Description, updated.Description);
        Assert.Equal(request.Address, updated.Address);
    }

    [Fact]
    public async Task MyUpdate_WithNoStore_ReturnsNotFound()
    {
        // Arrange - no store seeded for the test user
        var request = new Request
        {
            Name = _faker.Company.CompanyName()
        };
        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.PutAsJsonAsync(new Uri("/v1/stores/my", UriKind.Relative), request);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task MyUpdate_WithoutAuthentication_ReturnsUnauthorized()
    {
        // Arrange
        var request = new Request { Name = _faker.Company.CompanyName() };
        var client = Factory.CreateClient();

        // Act
        var response = await client.PutAsJsonAsync(new Uri("/v1/stores/my", UriKind.Relative), request);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
