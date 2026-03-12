using System.Net;
using System.Net.Http.Json;
using Bogus;
using Common.Domain.StronglyTypedIds;
using Common.Tests;
using IAM.Domain.Identity;
using IAM.Endpoints.Users.VersionNeutral.Get;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IAM.Tests.Endpoints.Users;

[Collection("IntegrationTestCollection")]
public class GetTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public GetTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetUser_WithValidId_ReturnsUserResponse()
    {

        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IAM.Application.Persistence.IIAMDbContext>();

        var user = ApplicationUser.Create(
            _faker.Name.FirstName(),
            _faker.Name.LastName(),
            "555" + _faker.Random.Number(1000000, 9999999), // Valid looking TR phone
            _faker.Random.Long(10000000000L, 99999999999L).ToString(System.Globalization.CultureInfo.InvariantCulture), // valid looking TC NO
            DateOnly.FromDateTime(_faker.Date.Past(30))
        );

        db.Users.Add(user);
        await db.SaveChangesAsync(default);

        var client = Factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("TestScheme");

        // Simulating the auth context since we bypass it for now or assume internal/authorized
        // We'd typically inject an admin token but since permission is required (CustomActions.Read, CustomResources.ApplicationUsers), 
        // we might need to mock ClaimsPrincipal or use a test auth handler if the permission checks are strictly enforced in tests.
        // For simplicity and to see if the pipeline requires it, let's just make the call first.

        // Act
        var response = await client.GetAsync(new Uri($"/users/{user.Id}", UriKind.Relative));

        // Assert
        // Let pipeline tell us if we miss auth or if it passes
        if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden)
        {
            Assert.Fail($"Endpoint requires authentication/authorization. Received: {response.StatusCode}");
        }

        response.EnsureSuccessStatusCode();

        var rawJson = await response.Content.ReadAsStringAsync();
        using var doc = System.Text.Json.JsonDocument.Parse(rawJson);
        var root = doc.RootElement;

        Assert.Equal(user.Id.Value.ToString(), root.GetProperty("id").GetString());
        Assert.Equal(user.Name, root.GetProperty("name").GetString());
        Assert.Equal(user.LastName, root.GetProperty("lastName").GetString());
        Assert.Equal(user.PhoneNumber, root.GetProperty("phoneNumber").GetString());
    }
}
