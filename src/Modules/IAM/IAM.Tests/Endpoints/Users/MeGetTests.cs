using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;
using Bogus;
using Common.Tests;
using IAM.Application.Persistence;
using IAM.Domain.Identity;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IAM.Tests.Endpoints.Users;

[Collection("IntegrationTestCollection")]
public class MeGetTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public MeGetTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task MeGet_WithValidAuth_ReturnsCurrentUser()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();

        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(CultureInfo.InvariantCulture);
        var user = ApplicationUser.Create(
            _faker.Name.FirstName(),
            _faker.Name.LastName(),
            phoneNumber,
            _faker.Random.Long(10000000000L, 99999999999L).ToString(CultureInfo.InvariantCulture),
            DateOnly.FromDateTime(_faker.Date.Past(30))
        );

        db.Users.Add(user);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(TestAuthHandler.AuthenticationScheme);
        client.DefaultRequestHeaders.Add("X-Test-User-Id", user.Id.Value.ToString());

        // Act
        var response = await client.GetAsync(new Uri("/users/me", UriKind.Relative));

        // Assert
        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync();
            Assert.Fail($"Status: {response.StatusCode}. Error: {err}");
        }

        response.EnsureSuccessStatusCode();

        var rawJson = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(rawJson);
        var root = doc.RootElement;

        Assert.Equal(user.Id.Value.ToString(), root.GetProperty("id").GetString());
        Assert.Equal(user.Name, root.GetProperty("name").GetString());
        Assert.Equal(user.LastName, root.GetProperty("lastName").GetString());
        Assert.Equal(user.PhoneNumber, root.GetProperty("phoneNumber").GetString());
    }
}
