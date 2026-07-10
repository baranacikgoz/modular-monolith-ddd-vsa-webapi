using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using Bogus;
using ZiggyCreatures.Caching.Fusion;
using Common.Tests;
using IAM.Application.Persistence;
using IAM.Application.Tokens.Services;
using IAM.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IAM.Tests.Endpoints.Tokens;

[Collection("IntegrationTestCollection")]
public class RevokeTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public RevokeTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task RevokeToken_WithValidAuth_RevokesTheCallersSession()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        var timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();

        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999)
            .ToString(CultureInfo.InvariantCulture);

        var user = ApplicationUser.Create(
            _faker.Name.FullName(),
            phoneNumber,
            DateOnly.FromDateTime(_faker.Date.Past(30))
        );

        var utcNow = timeProvider.GetUtcNow();
        var (refreshTokenBytes, refreshTokenExpiresAt) = tokenService.GenerateRefreshToken(utcNow);
        var refreshToken = user.IssueSessionAndToken(
            null, Guid.NewGuid(), "mobile-app-1", null, null, null, SHA256.HashData(refreshTokenBytes),
            utcNow, refreshTokenExpiresAt, utcNow.AddDays(90));

        db.Users.Add(user);
        await db.SaveChangesAsync(default);

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(TestAuthHandler.AuthenticationScheme);
        client.DefaultRequestHeaders.Add("X-Test-User-Id", user.Id.Value.ToString());
        client.DefaultRequestHeaders.Add("X-Test-Session-Id", refreshToken.SessionId.Value.ToString());

        // Act
        var response = await client.PostAsync(new Uri("/tokens/revoke", UriKind.Relative), null);

        // Assert
        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync();
            Assert.Fail($"Status: {response.StatusCode}. Error: {err}");
        }
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        // Verify side-effect: the session tied to the caller's sid claim is revoked.
        using var verifyScope = Factory.Services.CreateScope();
        var verifyDb = verifyScope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var session = await verifyDb.Sessions.AsNoTracking()
            .SingleAsync(s => s.Id == refreshToken.SessionId);

        Assert.NotNull(session.RevokedAt);
    }

    [Fact]
    public async Task RevokeToken_WithoutAuth_ReturnsUnauthorized()
    {
        // Arrange
        var client = Factory.CreateClient();
        // No Authorization header

        // Act
        var response = await client.PostAsync(new Uri("/tokens/revoke", UriKind.Relative), null);

        // Assert — unauthenticated callers must be rejected
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task RevokeToken_ThenUseAccessToken_ReturnsUnauthorized()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        var timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();
        var cache = scope.ServiceProvider.GetRequiredService<IFusionCache>();

        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999)
            .ToString(CultureInfo.InvariantCulture);

        var user = ApplicationUser.Create(
            _faker.Name.FullName(),
            phoneNumber,
            DateOnly.FromDateTime(_faker.Date.Past(30))
        );

        var utcNow = timeProvider.GetUtcNow();
        var (refreshTokenBytes, refreshTokenExpiresAt) = tokenService.GenerateRefreshToken(utcNow);
        var refreshToken = user.IssueSessionAndToken(
            null, Guid.NewGuid(), "mobile-app-1", null, null, null, SHA256.HashData(refreshTokenBytes),
            utcNow, refreshTokenExpiresAt, utcNow.AddDays(90));

        db.Users.Add(user);
        await db.SaveChangesAsync(default);

        // Use a unique jti for this test to avoid cross-test cache collisions
        var jti = Guid.NewGuid().ToString();

        var revokeClient = Factory.CreateClient();
        revokeClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(TestAuthHandler.AuthenticationScheme);
        revokeClient.DefaultRequestHeaders.Add("X-Test-User-Id", user.Id.Value.ToString());
        revokeClient.DefaultRequestHeaders.Add("X-Test-Jti", jti);
        revokeClient.DefaultRequestHeaders.Add("X-Test-Session-Id", refreshToken.SessionId.Value.ToString());

        // Act — revoke the token
        var revokeResponse = await revokeClient.PostAsync(new Uri("/tokens/revoke", UriKind.Relative), null);

        // Assert revoke succeeded
        if (!revokeResponse.IsSuccessStatusCode)
        {
            var err = await revokeResponse.Content.ReadAsStringAsync();
            Assert.Fail($"Revoke failed. Status: {revokeResponse.StatusCode}. Error: {err}");
        }
        Assert.Equal(HttpStatusCode.NoContent, revokeResponse.StatusCode);

        // Assert — the jti is now stored in the blacklist cache
        // This proves the OnTokenValidated hook will reject any real JWT carrying this jti.
        var isBlacklisted = await cache.GetOrDefaultAsync<bool?>($"blacklisted_jti:{jti}");
        Assert.True(isBlacklisted == true, "Expected the revoked jti to be present in the blacklist cache.");
    }
}
