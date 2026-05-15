using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Bogus;
using Common.Application.Caching;
using Common.Tests;
using IAM.Application.Persistence;
using IAM.Domain.Identity;
using IAM.Endpoints.Tokens.VersionNeutral.Create;
using IAM.Infrastructure.Identity.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using ZiggyCreatures.Caching.Fusion;

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
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var cache = scope.ServiceProvider.GetRequiredService<IFusionCache>();

        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(CultureInfo.InvariantCulture);
        var otp = "123456";

        var user = ApplicationUser.Create(
            _faker.Name.FullName(),
            phoneNumber,
            DateOnly.FromDateTime(_faker.Date.Past(30))
        );

        db.Users.Add(user);
        await db.SaveChangesAsync();

        // Pre-seed cache to bypass SMS OTP check
        var cacheKey = CacheKeys.For.Otp(phoneNumber, "login");
        await cache.SetAsync(cacheKey, new OtpCacheEntry(otp, 0),
            new FusionCacheEntryOptions { Duration = TimeSpan.FromMinutes(5) });

        var client = Factory.CreateClient();
        var request = new Request { PhoneNumber = phoneNumber, Otp = otp };

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
        using var doc = JsonDocument.Parse(rawJson);
        var root = doc.RootElement;

        Assert.True(root.TryGetProperty("accessToken", out var accessToken));
        Assert.False(string.IsNullOrWhiteSpace(accessToken.GetString()));

        Assert.True(root.TryGetProperty("refreshToken", out var refreshToken));
        Assert.False(string.IsNullOrWhiteSpace(refreshToken.GetString()));
    }

    [Fact]
    public async Task CreateTokens_WithInvalidOtp_ReturnsBadRequest()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var cache = scope.ServiceProvider.GetRequiredService<IFusionCache>();

        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(CultureInfo.InvariantCulture);
        const string correctOtp = "123456";
        const string wrongOtp = "000000";

        var user = ApplicationUser.Create(
            _faker.Name.FullName(),
            phoneNumber,
            DateOnly.FromDateTime(_faker.Date.Past(30))
        );

        db.Users.Add(user);
        await db.SaveChangesAsync();

        // Pre-seed cache with the CORRECT otp
        var cacheKey = CacheKeys.For.Otp(phoneNumber, "login");
        await cache.SetAsync(cacheKey, new OtpCacheEntry(correctOtp, 0),
            new FusionCacheEntryOptions { Duration = TimeSpan.FromMinutes(5) });

        var client = Factory.CreateClient();
        var request = new Request
        {
            PhoneNumber = phoneNumber, Otp = wrongOtp // deliberately wrong
        };

        // Act
        var response = await client.PostAsJsonAsync(new Uri("/tokens", UriKind.Relative), request);

        // Assert — expect an error response, not 200
        Assert.NotEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task CreateTokens_AfterThreeWrongOtpAttempts_RejectsCorrectOtp()
    {
        // Arrange — OTP must be invalidated after 3 failed attempts, even if the correct code is used later.
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var cache = scope.ServiceProvider.GetRequiredService<IFusionCache>();

        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(CultureInfo.InvariantCulture);
        const string correctOtp = "123456";
        const string wrongOtp = "000000";

        var user = ApplicationUser.Create(
            _faker.Name.FullName(),
            phoneNumber,
            DateOnly.FromDateTime(_faker.Date.Past(30))
        );

        db.Users.Add(user);
        await db.SaveChangesAsync();

        var cacheKey = CacheKeys.For.Otp(phoneNumber, "login");
        await cache.SetAsync(cacheKey, new OtpCacheEntry(correctOtp, 0),
            new FusionCacheEntryOptions { Duration = TimeSpan.FromMinutes(5) });

        var client = Factory.CreateClient();

        // Act — 3 wrong attempts
        for (var i = 0; i < 3; i++)
        {
            var badRequest = new Request { PhoneNumber = phoneNumber, Otp = wrongOtp };
            var badResponse = await client.PostAsJsonAsync(new Uri("/tokens", UriKind.Relative), badRequest);
            Assert.False(badResponse.IsSuccessStatusCode);
        }

        // Act — correct OTP after lockout
        var goodRequest = new Request { PhoneNumber = phoneNumber, Otp = correctOtp };
        var response = await client.PostAsJsonAsync(new Uri("/tokens", UriKind.Relative), goodRequest);

        // Assert — OTP must be dead after 3 failed attempts
        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task CreateTokens_WithNonExistentUser_ReturnsNotFound()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var cache = scope.ServiceProvider.GetRequiredService<IFusionCache>();

        // A phone number that has no user in the DB
        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(CultureInfo.InvariantCulture);
        const string otp = "123456";

        // Seed cache so OTP verification passes
        var cacheKey = CacheKeys.For.Otp(phoneNumber, "login");
        await cache.SetAsync(cacheKey, new OtpCacheEntry(otp, 0),
            new FusionCacheEntryOptions { Duration = TimeSpan.FromMinutes(5) });

        var client = Factory.CreateClient();
        var request = new Request { PhoneNumber = phoneNumber, Otp = otp };

        // Act
        var response = await client.PostAsJsonAsync(new Uri("/tokens", UriKind.Relative), request);

        // Assert — no user exists, expect a 4xx error
        Assert.False(response.IsSuccessStatusCode);
    }
}
