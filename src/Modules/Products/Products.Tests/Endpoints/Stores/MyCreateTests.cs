using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Bogus;
using Common.Domain.StronglyTypedIds;
using Common.Tests;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Persistence;
using Products.Domain.Stores;
using Products.Endpoints.Stores.v1.My.Create;
using Xunit;

namespace Products.Tests.Endpoints.Stores;

[Collection("IntegrationTestCollection")]
public class MyCreateTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public MyCreateTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task MyCreate_WithValidPayload_ReturnsOkAndPersistsStore()
    {
        // Arrange
        var request = new Request(
            Name: _faker.Company.CompanyName(),
            Description: _faker.Lorem.Sentence(),
            Address: _faker.Address.FullAddress()
        );

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.PostAsJsonAsync(new Uri("/v1/stores/my", UriKind.Relative), request);

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
        var store = await db.Stores.FindAsync(responseBody.Id);

        Assert.NotNull(store);
        Assert.Equal(request.Name, store.Name);
        Assert.Equal(request.Description, store.Description);
        Assert.Equal(request.Address, store.Address);
        // OwnerId should be set to the test user's ID
        Assert.Equal(new ApplicationUserId(TestAuthHandler.DefaultUserId), store.OwnerId);
    }

    [Fact]
    public async Task MyCreate_WhenStoreAlreadyExists_ReturnsConflict()
    {
        // Arrange — pre-seed a store for the default test user
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();
        var ownerId = new ApplicationUserId(TestAuthHandler.DefaultUserId);
        db.Stores.Add(Store.Create(ownerId, "Existing Store", "Desc", "Address"));
        await db.SaveChangesAsync();

        var request = new Request("Another Store", "Desc 2", "Address 2");
        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");

        // Act
        var response = await client.PostAsJsonAsync(new Uri("/v1/stores/my", UriKind.Relative), request);

        // Assert — unique constraint violation → 409 Conflict
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    public async Task MyCreate_WithoutAuthentication_ReturnsUnauthorized()
    {
        // Arrange
        var request = new Request(_faker.Company.CompanyName(), _faker.Lorem.Sentence(), _faker.Address.FullAddress());
        var client = Factory.CreateClient();

        // Act
        var response = await client.PostAsJsonAsync(new Uri("/v1/stores/my", UriKind.Relative), request);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
