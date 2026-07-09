using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text.Json;
using Bogus;
using Common.Tests;
using IAM.Application.Persistence;
using IAM.Application.Tokens.Services;
using IAM.Domain.Identity;
using IAM.Endpoints.Tokens.VersionNeutral.Refresh;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IAM.Tests.Endpoints.Tokens;

[Collection("IntegrationTestCollection")]
public class RefreshTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public RefreshTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task RefreshToken_WithValidToken_ReturnsNewAccessToken()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        var timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();

        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(CultureInfo.InvariantCulture);

        var user = ApplicationUser.Create(
            _faker.Name.FullName(),
            phoneNumber,
            DateOnly.FromDateTime(_faker.Date.Past(30))
        );

        var utcNow = timeProvider.GetUtcNow();
        var (refreshTokenBytes, refreshTokenExpiresAt) = tokenService.GenerateRefreshToken(utcNow);

        user.UpdateRefreshToken(SHA256.HashData(refreshTokenBytes), refreshTokenExpiresAt);

        db.Users.Add(user);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        var request = new Request { RefreshToken = Convert.ToBase64String(refreshTokenBytes) };

        // Act
        var response = await client.PostAsJsonAsync(new Uri("/tokens/refresh", UriKind.Relative), request);

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

        Assert.True(root.TryGetProperty("accessTokenExpiresAt", out var expiresAt));
        Assert.True(expiresAt.GetDateTimeOffset() > utcNow);

        Assert.True(root.TryGetProperty("refreshToken", out var newRefreshToken));
        Assert.False(string.IsNullOrWhiteSpace(newRefreshToken.GetString()));

        Assert.True(root.TryGetProperty("refreshTokenExpiresAt", out var refreshExpiresAt));
        Assert.True(refreshExpiresAt.GetDateTimeOffset() > utcNow);
    }

    [Fact]
    public async Task RefreshToken_ReusingOldToken_ReturnsError()
    {
        // Arrange — after a successful refresh, the old refresh token must be invalidated.
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        var timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();

        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(CultureInfo.InvariantCulture);

        var user = ApplicationUser.Create(
            _faker.Name.FullName(),
            phoneNumber,
            DateOnly.FromDateTime(_faker.Date.Past(30))
        );

        var utcNow = timeProvider.GetUtcNow();
        var (refreshTokenBytes, refreshTokenExpiresAt) = tokenService.GenerateRefreshToken(utcNow);

        user.UpdateRefreshToken(SHA256.HashData(refreshTokenBytes), refreshTokenExpiresAt);

        db.Users.Add(user);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        var request = new Request { RefreshToken = Convert.ToBase64String(refreshTokenBytes) };

        // First use — must succeed
        var firstResponse = await client.PostAsJsonAsync(new Uri("/tokens/refresh", UriKind.Relative), request);
        if (!firstResponse.IsSuccessStatusCode)
        {
            var err = await firstResponse.Content.ReadAsStringAsync();
            Assert.Fail($"First refresh failed unexpectedly. Status: {firstResponse.StatusCode}. Error: {err}");
        }

        // Act — reuse the SAME old refresh token
        var secondResponse = await client.PostAsJsonAsync(new Uri("/tokens/refresh", UriKind.Relative), request);

        // Assert — old token is now invalid
        Assert.False(secondResponse.IsSuccessStatusCode);
    }

    [Fact]
    public async Task RefreshToken_WithExpiredToken_ReturnsError()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        var timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();

        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(CultureInfo.InvariantCulture);

        var user = ApplicationUser.Create(
            _faker.Name.FullName(),
            phoneNumber,
            DateOnly.FromDateTime(_faker.Date.Past(30))
        );

        // Generate a refresh token but set expiry in the PAST
        var (refreshTokenBytes, _) = tokenService.GenerateRefreshToken(timeProvider.GetUtcNow());
        var expiredAt = timeProvider.GetUtcNow().AddDays(-1); // already expired
        user.UpdateRefreshToken(SHA256.HashData(refreshTokenBytes), expiredAt);

        db.Users.Add(user);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        var request = new Request { RefreshToken = Convert.ToBase64String(refreshTokenBytes) };

        // Act
        var response = await client.PostAsJsonAsync(new Uri("/tokens/refresh", UriKind.Relative), request);

        // Assert — expired token must be rejected as an auth failure (401), not a validation error (400)
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task RefreshToken_WithMalformedBase64_ReturnsUnauthorized()
    {
        // Arrange — this specifically tests that the endpoint does NOT throw a 500
        // due to the FormatException that Convert.FromBase64String throws on bad input.
        var client = Factory.CreateClient();
        var request = new Request { RefreshToken = "this-is-not!!-valid-base64-%%" };

        // Act
        var response = await client.PostAsJsonAsync(new Uri("/tokens/refresh", UriKind.Relative), request);

        // Assert — an unusable token is an auth failure (401), not a validation error (400)
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
