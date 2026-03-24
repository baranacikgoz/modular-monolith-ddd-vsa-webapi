using System.Net.Http.Json;
using Bogus;
using Common.Application.Caching;
using Common.Tests;
using IAM.Domain.Identity;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IAM.Tests.Endpoints.Tokens;

[Collection("IntegrationTestCollection")]
public class CreateTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public CreateTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task CreateTokens_WithValidOtp_ReturnsTokens()
    {

        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IAM.Application.Persistence.IIAMDbContext>();
        var cache = scope.ServiceProvider.GetRequiredService<ICacheService>();

        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(System.Globalization.CultureInfo.InvariantCulture);
        var otp = "123456";

        var user = ApplicationUser.Create(
            _faker.Name.FirstName(),
            _faker.Name.LastName(),
            phoneNumber,
            _faker.Random.Long(10000000000L, 99999999999L).ToString(System.Globalization.CultureInfo.InvariantCulture),
            DateOnly.FromDateTime(_faker.Date.Past(30))
        );

        db.Users.Add(user);
        await db.SaveChangesAsync(default);

        // Pre-seed cache to bypass SMS OTP check
        var cacheKey = Common.Application.Caching.CacheKeys.For.Otp(phoneNumber);
        await cache.SetAsync(cacheKey, otp, absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(5));

        var client = Factory.CreateClient();
        var request = new IAM.Endpoints.Tokens.VersionNeutral.Create.Request
        {
            PhoneNumber = phoneNumber,
            Otp = otp
        };

        // Act
        var response = await client.PostAsJsonAsync(new Uri("/tokens", UriKind.Relative), request);

        // Assert
        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync();
            Assert.Fail($"Status: {response.StatusCode}. Error: {err}");
        }
        response.EnsureSuccessStatusCode();

        var rawJson = await response.Content.ReadAsStringAsync();
        using var doc = System.Text.Json.JsonDocument.Parse(rawJson);
        var root = doc.RootElement;

        Assert.True(root.TryGetProperty("accessToken", out var accessToken));
        Assert.False(string.IsNullOrWhiteSpace(accessToken.GetString()));

        Assert.True(root.TryGetProperty("refreshToken", out var refreshToken));
        Assert.False(string.IsNullOrWhiteSpace(refreshToken.GetString()));
    }
}
