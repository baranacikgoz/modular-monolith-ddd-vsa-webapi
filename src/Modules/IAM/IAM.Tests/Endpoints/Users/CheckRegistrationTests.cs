using System.Globalization;
using System.Text.Json;
using Bogus;
using Common.Tests;
using IAM.Application.Persistence;
using IAM.Domain.Identity;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IAM.Tests.Endpoints.Users;

[Collection("IntegrationTestCollection")]
public class CheckRegistrationTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public CheckRegistrationTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task CheckRegistration_WithRegisteredPhoneNumber_ReturnsTrue()
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

        // Act
        var response =
            await client.GetAsync(new Uri($"/users/check-registration?phoneNumber={phoneNumber}", UriKind.Relative));

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

        Assert.True(root.GetProperty("isRegistered").GetBoolean());
    }

    [Fact]
    public async Task CheckRegistration_WithUnregisteredPhoneNumber_ReturnsFalse()
    {
        // Arrange
        var client = Factory.CreateClient();
        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(CultureInfo.InvariantCulture);

        // Act
        var response =
            await client.GetAsync(new Uri($"/users/check-registration?phoneNumber={phoneNumber}", UriKind.Relative));

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

        Assert.False(root.GetProperty("isRegistered").GetBoolean());
    }
}
