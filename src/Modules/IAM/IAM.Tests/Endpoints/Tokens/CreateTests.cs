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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using ZiggyCreatures.Caching.Fusion;
using RefreshRequest = IAM.Endpoints.Tokens.VersionNeutral.Refresh.Request;

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
        await cache.SetAsync(cacheKey, new OtpCacheEntry(otp, 0, DateTimeOffset.UtcNow.AddMinutes(5)),
            new FusionCacheEntryOptions { Duration = TimeSpan.FromMinutes(5) });

        var client = Factory.CreateClient();
        var request = new Request
        {
            PhoneNumber = phoneNumber, Otp = otp, DeviceId = Guid.NewGuid(), ClientId = "mobile-app-1"
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
        await cache.SetAsync(cacheKey, new OtpCacheEntry(correctOtp, 0, DateTimeOffset.UtcNow.AddMinutes(5)),
            new FusionCacheEntryOptions { Duration = TimeSpan.FromMinutes(5) });

        var client = Factory.CreateClient();
        var request = new Request
        {
            PhoneNumber = phoneNumber, Otp = wrongOtp, // deliberately wrong
            DeviceId = Guid.NewGuid(), ClientId = "mobile-app-1"
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
        await cache.SetAsync(cacheKey, new OtpCacheEntry(correctOtp, 0, DateTimeOffset.UtcNow.AddMinutes(5)),
            new FusionCacheEntryOptions { Duration = TimeSpan.FromMinutes(5) });

        var client = Factory.CreateClient();

        // Act — 3 wrong attempts
        for (var i = 0; i < 3; i++)
        {
            var badRequest = new Request
            {
                PhoneNumber = phoneNumber, Otp = wrongOtp, DeviceId = Guid.NewGuid(), ClientId = "mobile-app-1"
            };
            var badResponse = await client.PostAsJsonAsync(new Uri("/tokens", UriKind.Relative), badRequest);
            Assert.False(badResponse.IsSuccessStatusCode);
        }

        // Act — correct OTP after lockout
        var goodRequest = new Request
        {
            PhoneNumber = phoneNumber, Otp = correctOtp, DeviceId = Guid.NewGuid(), ClientId = "mobile-app-1"
        };
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
        await cache.SetAsync(cacheKey, new OtpCacheEntry(otp, 0, DateTimeOffset.UtcNow.AddMinutes(5)),
            new FusionCacheEntryOptions { Duration = TimeSpan.FromMinutes(5) });

        var client = Factory.CreateClient();
        var request = new Request
        {
            PhoneNumber = phoneNumber, Otp = otp, DeviceId = Guid.NewGuid(), ClientId = "mobile-app-1"
        };

        // Act
        var response = await client.PostAsJsonAsync(new Uri("/tokens", UriKind.Relative), request);

        // Assert — no user exists, expect a 4xx error
        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task CreateTokens_NewDeviceAppPair_CreatesIndependentSession()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var cache = scope.ServiceProvider.GetRequiredService<IFusionCache>();

        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(CultureInfo.InvariantCulture);
        const string otp = "123456";

        var user = ApplicationUser.Create(
            _faker.Name.FullName(), phoneNumber, DateOnly.FromDateTime(_faker.Date.Past(30)));
        db.Users.Add(user);
        await db.SaveChangesAsync();

        var cacheKey = CacheKeys.For.Otp(phoneNumber, "login");
        var client = Factory.CreateClient();

        // Act — log in twice with different (DeviceId, ClientId) pairs.
        await cache.SetAsync(cacheKey, new OtpCacheEntry(otp, 0, DateTimeOffset.UtcNow.AddMinutes(5)),
            new FusionCacheEntryOptions { Duration = TimeSpan.FromMinutes(5) });
        var responseA = await client.PostAsJsonAsync(new Uri("/tokens", UriKind.Relative),
            new Request { PhoneNumber = phoneNumber, Otp = otp, DeviceId = Guid.NewGuid(), ClientId = "mobile-app-1" });
        responseA.EnsureSuccessStatusCode();

        await cache.SetAsync(cacheKey, new OtpCacheEntry(otp, 0, DateTimeOffset.UtcNow.AddMinutes(5)),
            new FusionCacheEntryOptions { Duration = TimeSpan.FromMinutes(5) });
        var responseB = await client.PostAsJsonAsync(new Uri("/tokens", UriKind.Relative),
            new Request { PhoneNumber = phoneNumber, Otp = otp, DeviceId = Guid.NewGuid(), ClientId = "web-app-1" });
        responseB.EnsureSuccessStatusCode();

        // Assert — two independent sessions exist.
        using var verifyScope = Factory.Services.CreateScope();
        var verifyDb = verifyScope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var sessions = await verifyDb.Sessions.AsNoTracking().Where(s => s.UserId == user.Id).ToListAsync();
        Assert.Equal(2, sessions.Count);

        var sessionB = sessions.Single(s => s.ClientId == "web-app-1");
        var sessionBLastUsedAtBefore = sessionB.LastUsedAt;

        // Act — refresh session A only.
        var jsonA = await responseA.Content.ReadAsStringAsync();
        var refreshTokenA = JsonDocument.Parse(jsonA).RootElement.GetProperty("refreshToken").GetString();
        var refreshResponse = await client.PostAsJsonAsync(
            new Uri("/tokens/refresh", UriKind.Relative), new RefreshRequest { RefreshToken = refreshTokenA! });
        refreshResponse.EnsureSuccessStatusCode();

        // Assert — session B is completely untouched by session A's refresh.
        using var verifyScope2 = Factory.Services.CreateScope();
        var verifyDb2 = verifyScope2.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var sessionBAfter = await verifyDb2.Sessions.AsNoTracking().SingleAsync(s => s.Id == sessionB.Id);
        Assert.Equal(sessionBLastUsedAtBefore, sessionBAfter.LastUsedAt);
        Assert.Null(sessionBAfter.RevokedAt);
    }

    [Fact]
    public async Task CreateTokens_SameTriple_ReusesSessionAndSupersedesOldToken()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var cache = scope.ServiceProvider.GetRequiredService<IFusionCache>();

        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(CultureInfo.InvariantCulture);
        const string otp = "123456";
        var deviceId = Guid.NewGuid();

        var user = ApplicationUser.Create(
            _faker.Name.FullName(), phoneNumber, DateOnly.FromDateTime(_faker.Date.Past(30)));
        db.Users.Add(user);
        await db.SaveChangesAsync();

        var cacheKey = CacheKeys.For.Otp(phoneNumber, "login");
        var client = Factory.CreateClient();

        // Act — log in twice with the SAME (DeviceId, ClientId) pair.
        await cache.SetAsync(cacheKey, new OtpCacheEntry(otp, 0, DateTimeOffset.UtcNow.AddMinutes(5)),
            new FusionCacheEntryOptions { Duration = TimeSpan.FromMinutes(5) });
        var firstResponse = await client.PostAsJsonAsync(new Uri("/tokens", UriKind.Relative),
            new Request { PhoneNumber = phoneNumber, Otp = otp, DeviceId = deviceId, ClientId = "mobile-app-1" });
        firstResponse.EnsureSuccessStatusCode();
        var firstJson = await firstResponse.Content.ReadAsStringAsync();
        var firstRefreshToken = JsonDocument.Parse(firstJson).RootElement.GetProperty("refreshToken").GetString();

        await cache.SetAsync(cacheKey, new OtpCacheEntry(otp, 0, DateTimeOffset.UtcNow.AddMinutes(5)),
            new FusionCacheEntryOptions { Duration = TimeSpan.FromMinutes(5) });
        var secondResponse = await client.PostAsJsonAsync(new Uri("/tokens", UriKind.Relative),
            new Request { PhoneNumber = phoneNumber, Otp = otp, DeviceId = deviceId, ClientId = "mobile-app-1" });
        secondResponse.EnsureSuccessStatusCode();

        // Assert — still exactly one session for that (DeviceId, ClientId) pair.
        using var verifyScope = Factory.Services.CreateScope();
        var verifyDb = verifyScope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var sessions = await verifyDb.Sessions.AsNoTracking().Where(s => s.UserId == user.Id).ToListAsync();
        Assert.Single(sessions);

        // Assert — the first login's refresh token no longer works (superseded, not just orphaned).
        var replayResponse = await client.PostAsJsonAsync(
            new Uri("/tokens/refresh", UriKind.Relative), new RefreshRequest { RefreshToken = firstRefreshToken! });
        Assert.False(replayResponse.IsSuccessStatusCode);
    }
}
