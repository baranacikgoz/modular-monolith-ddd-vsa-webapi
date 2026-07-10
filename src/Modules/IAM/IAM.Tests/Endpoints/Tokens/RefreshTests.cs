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
using Microsoft.EntityFrameworkCore;
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
    public async Task RefreshToken_Valid_IssuesAccessTokenAndKeepsSameRefreshToken()
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
        var tokenHash = SHA256.HashData(refreshTokenBytes);
        user.IssueSessionAndToken(
            null, Guid.NewGuid(), "mobile-app-1", "iPhone", "1.1.1.1", "UA", tokenHash,
            utcNow, refreshTokenExpiresAt, utcNow.AddDays(90));

        db.Users.Add(user);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        var sentRefreshToken = Convert.ToBase64String(refreshTokenBytes);
        var request = new Request { RefreshToken = sentRefreshToken };

        // Act
        var response = await client.PostAsJsonAsync(new Uri("/tokens/refresh", UriKind.Relative), request);

        // Assert
        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync();
            Assert.Fail($"Status: {response.StatusCode}. Error: {err}");
        }

        var rawJson = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(rawJson);
        var root = doc.RootElement;

        Assert.True(root.TryGetProperty("accessToken", out var accessToken));
        Assert.False(string.IsNullOrWhiteSpace(accessToken.GetString()));

        Assert.True(root.TryGetProperty("accessTokenExpiresAt", out var expiresAt));
        Assert.True(expiresAt.GetDateTimeOffset() > utcNow);

        // No rotation: the exact same refresh token comes back, with a slid-forward expiry.
        Assert.True(root.TryGetProperty("refreshToken", out var returnedRefreshToken));
        Assert.Equal(sentRefreshToken, returnedRefreshToken.GetString());

        Assert.True(root.TryGetProperty("refreshTokenExpiresAt", out var refreshExpiresAt));
        Assert.True(refreshExpiresAt.GetDateTimeOffset() >= refreshTokenExpiresAt);

        // Verify side-effect: still exactly one live token, not consumed, expiry slid forward.
        using var verifyScope = Factory.Services.CreateScope();
        var verifyDb = verifyScope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var storedToken = await verifyDb.RefreshTokens.AsNoTracking().SingleAsync(rt => rt.TokenHash == tokenHash);
        Assert.Null(storedToken.ConsumedAt);
        Assert.Null(storedToken.ReplacedByTokenId);
        Assert.True(storedToken.ExpiresAt >= refreshTokenExpiresAt);
    }

    [Fact]
    public async Task RefreshToken_RepeatedUseOfSameToken_KeepsWorking()
    {
        // Arrange — a lost response or client-side timeout retry replays the same token moments
        // later. Without rotation this is a plain idempotent-style retry and must always succeed —
        // never a theft signal, never a revoked session.
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        var timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();

        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(CultureInfo.InvariantCulture);
        var user = ApplicationUser.Create(
            _faker.Name.FullName(), phoneNumber, DateOnly.FromDateTime(_faker.Date.Past(30)));

        var utcNow = timeProvider.GetUtcNow();
        var (tokenBytes, tokenExpiresAt) = tokenService.GenerateRefreshToken(utcNow);
        user.IssueSessionAndToken(
            null, Guid.NewGuid(), "mobile-app-1", null, null, null, SHA256.HashData(tokenBytes),
            utcNow, tokenExpiresAt, utcNow.AddDays(90));

        db.Users.Add(user);
        await db.SaveChangesAsync();

        var sessionId = user.Sessions.Single().Id;
        var client = Factory.CreateClient();
        var request = new Request
        {
            RefreshToken = Convert.ToBase64String(tokenBytes)
        };

        // Act — use the same token twice in a row.
        var firstResponse = await client.PostAsJsonAsync(new Uri("/tokens/refresh", UriKind.Relative), request);
        var secondResponse = await client.PostAsJsonAsync(new Uri("/tokens/refresh", UriKind.Relative), request);

        // Assert — both succeed, session intact.
        Assert.True(firstResponse.IsSuccessStatusCode);
        Assert.True(secondResponse.IsSuccessStatusCode);

        using var verifyScope = Factory.Services.CreateScope();
        var verifyDb = verifyScope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var session = await verifyDb.Sessions.AsNoTracking().SingleAsync(s => s.Id == sessionId);
        Assert.Null(session.RevokedAt);
    }

    [Fact]
    public async Task RefreshToken_SupersededByNewLogin_ReturnsInvalidWithoutRevokingSession()
    {
        // Arrange — logging in again on the same (DeviceId, ClientId) supersedes the session's
        // previous token. Replaying the superseded token must fail with a plain 401 — but the
        // session (and the new login's token) must keep working: a stale token on a reinstalled
        // device is normal life, not a theft signal worth killing the session over.
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        var timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();

        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(CultureInfo.InvariantCulture);
        var user = ApplicationUser.Create(
            _faker.Name.FullName(), phoneNumber, DateOnly.FromDateTime(_faker.Date.Past(30)));

        var deviceId = Guid.NewGuid();
        var utcNow = timeProvider.GetUtcNow();

        var (oldBytes, oldExpiresAt) = tokenService.GenerateRefreshToken(utcNow);
        user.IssueSessionAndToken(
            null, deviceId, "mobile-app-1", null, null, null, SHA256.HashData(oldBytes),
            utcNow, oldExpiresAt, utcNow.AddDays(90));

        // Second login on the same (DeviceId, ClientId) — supersedes the first token.
        var (newBytes, newExpiresAt) = tokenService.GenerateRefreshToken(utcNow);
        user.IssueSessionAndToken(
            user.Sessions.Single(), deviceId, "mobile-app-1", null, null, null, SHA256.HashData(newBytes),
            utcNow.AddMinutes(1), newExpiresAt, utcNow.AddDays(90));

        db.Users.Add(user);
        await db.SaveChangesAsync();

        var sessionId = user.Sessions.Single().Id;
        var client = Factory.CreateClient();

        // Act — replay the superseded token.
        var replayResponse = await client.PostAsJsonAsync(
            new Uri("/tokens/refresh", UriKind.Relative),
            new Request { RefreshToken = Convert.ToBase64String(oldBytes) });

        // Assert — plain 401, no session revocation.
        Assert.Equal(HttpStatusCode.Unauthorized, replayResponse.StatusCode);
        var replayJson = await replayResponse.Content.ReadAsStringAsync();
        Assert.Equal(
            "InvalidRefreshToken",
            JsonDocument.Parse(replayJson).RootElement.GetProperty("errorKey").GetString());

        using var verifyScope = Factory.Services.CreateScope();
        var verifyDb = verifyScope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var session = await verifyDb.Sessions.AsNoTracking().SingleAsync(s => s.Id == sessionId);
        Assert.Null(session.RevokedAt);

        // Assert — the current token still refreshes fine.
        var currentResponse = await client.PostAsJsonAsync(
            new Uri("/tokens/refresh", UriKind.Relative),
            new Request { RefreshToken = Convert.ToBase64String(newBytes) });
        Assert.True(currentResponse.IsSuccessStatusCode);
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
        var utcNow = timeProvider.GetUtcNow();
        var (refreshTokenBytes, _) = tokenService.GenerateRefreshToken(utcNow);
        var expiredAt = utcNow.AddDays(-1); // already expired
        user.IssueSessionAndToken(
            null, Guid.NewGuid(), "mobile-app-1", null, null, null, SHA256.HashData(refreshTokenBytes),
            utcNow, expiredAt, utcNow.AddDays(90));

        db.Users.Add(user);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        var request = new Request
        {
            RefreshToken = Convert.ToBase64String(refreshTokenBytes)
        };

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
        var request = new Request
        {
            RefreshToken = "this-is-not!!-valid-base64-%%"
        };

        // Act
        var response = await client.PostAsJsonAsync(new Uri("/tokens/refresh", UriKind.Relative), request);

        // Assert — an unusable token is an auth failure (401), not a validation error (400)
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
