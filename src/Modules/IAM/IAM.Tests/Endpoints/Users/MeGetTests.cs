using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;
using Bogus;
using Common.Application.Auth;
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
            _faker.Name.FullName(),
            phoneNumber,
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
        Assert.Equal(user.FullName, root.GetProperty("fullName").GetString());
        Assert.Equal(user.PhoneNumber, root.GetProperty("phoneNumber").GetString());
    }

    [Fact]
    public async Task MeGet_WithRoles_ReturnsRolesAndDerivedPermissions()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();

        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(CultureInfo.InvariantCulture);
        var user = ApplicationUser.Create(
            _faker.Name.FullName(),
            phoneNumber,
            DateOnly.FromDateTime(_faker.Date.Past(30))
        );

        db.Users.Add(user);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(TestAuthHandler.AuthenticationScheme);
        client.DefaultRequestHeaders.Add("X-Test-User-Id", user.Id.Value.ToString());
        client.DefaultRequestHeaders.Add("X-Test-Roles", CustomRoles.Basic);

        // Act
        var response = await client.GetAsync(new Uri("/users/me", UriKind.Relative));

        // Assert
        response.EnsureSuccessStatusCode();

        var rawJson = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(rawJson);
        var root = doc.RootElement;

        var roles = root.GetProperty("roles").EnumerateArray().Select(x => x.GetString()!).ToList();
        Assert.Equal([CustomRoles.Basic], roles);

        var permissions = root.GetProperty("permissions").EnumerateArray().Select(x => x.GetString()!).ToHashSet();
        // Permissions are exactly those derived from the Basic role — order-independent.
        Assert.True(permissions.SetEquals(CustomPermissions.Basic));
        // Basic users get no admin-only permissions.
        Assert.DoesNotContain(CustomPermission.NameFor(CustomActions.Delete, CustomResources.ApplicationUsers), permissions);
    }
}
