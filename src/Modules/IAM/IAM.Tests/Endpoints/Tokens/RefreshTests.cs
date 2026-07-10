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
using IAM.Domain.Identity.Sessions;
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
    public async Task RefreshToken_ValidRotation_IssuesNewTokenAndConsumesOld()
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
        var oldTokenHash = SHA256.HashData(refreshTokenBytes);
        user.IssueSessionAndToken(
            null, Guid.NewGuid(), "mobile-app-1", "iPhone", "1.1.1.1", "UA", oldTokenHash,
            utcNow, refreshTokenExpiresAt, utcNow.AddDays(90));

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

        // Verify side-effect: the old token is consumed (not deleted), linked to its replacement.
        using var verifyScope = Factory.Services.CreateScope();
        var verifyDb = verifyScope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var oldToken = await verifyDb.RefreshTokens.AsNoTracking().SingleAsync(rt => rt.TokenHash == oldTokenHash);
        Assert.NotNull(oldToken.ConsumedAt);
        Assert.NotNull(oldToken.ReplacedByTokenId);
    }

    [Fact]
    public async Task RefreshToken_ReuseOfConsumedToken_OutsideGraceWindow_RevokesOnlyThatSessionAndReturnsGenericError()
    {
        // Arrange — two independent sessions for the same user.
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        var timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();

        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(CultureInfo.InvariantCulture);
        var user = ApplicationUser.Create(
            _faker.Name.FullName(), phoneNumber, DateOnly.FromDateTime(_faker.Date.Past(30)));

        var utcNow = timeProvider.GetUtcNow();
        var (targetBytes, targetExpiresAt) = tokenService.GenerateRefreshToken(utcNow);
        var targetTokenHash = SHA256.HashData(targetBytes);
        user.IssueSessionAndToken(
            null, Guid.NewGuid(), "mobile-app-1", null, null, null, targetTokenHash,
            utcNow, targetExpiresAt, utcNow.AddDays(90));

        var (otherBytes, otherExpiresAt) = tokenService.GenerateRefreshToken(utcNow);
        user.IssueSessionAndToken(
            null, Guid.NewGuid(), "web-app-1", null, null, null, SHA256.HashData(otherBytes),
            utcNow, otherExpiresAt, utcNow.AddDays(90));

        db.Users.Add(user);
        await db.SaveChangesAsync();

        var targetSessionId = user.Sessions.Single(s => s.ClientId == "mobile-app-1").Id;
        var otherSessionId = user.Sessions.Single(s => s.ClientId == "web-app-1").Id;

        var client = Factory.CreateClient();
        var request = new Request { RefreshToken = Convert.ToBase64String(targetBytes) };

        // First use — rotates it away (valid).
        var firstResponse = await client.PostAsJsonAsync(new Uri("/tokens/refresh", UriKind.Relative), request);
        Assert.True(firstResponse.IsSuccessStatusCode);
        var firstJson = await firstResponse.Content.ReadAsStringAsync();
        var currentRefreshToken = JsonDocument.Parse(firstJson).RootElement.GetProperty("refreshToken").GetString();

        // Push the consumed timestamp back past the reuse grace window — a real request retried
        // seconds later must NOT be indistinguishable from a genuine delayed replay/theft attempt.
        using (var mutateScope = Factory.Services.CreateScope())
        {
            var mutateDb = mutateScope.ServiceProvider.GetRequiredService<IIAMDbContext>();
            var consumedToken = await mutateDb.RefreshTokens.SingleAsync(rt => rt.TokenHash == targetTokenHash);
            mutateDb.Entry(consumedToken).Property(nameof(RefreshToken.ConsumedAt)).CurrentValue =
                timeProvider.GetUtcNow().AddMinutes(-5);
            await mutateDb.SaveChangesAsync();
        }

        // Act — replay the OLD (now-consumed, long past grace) token: this is the theft signal.
        var secondResponse = await client.PostAsJsonAsync(new Uri("/tokens/refresh", UriKind.Relative), request);

        // Assert — same generic shape as an ordinary invalid/expired token, no leaked detection.
        Assert.Equal(HttpStatusCode.Unauthorized, secondResponse.StatusCode);
        var secondJson = await secondResponse.Content.ReadAsStringAsync();
        Assert.Equal("InvalidRefreshToken", JsonDocument.Parse(secondJson).RootElement.GetProperty("errorKey").GetString());

        // Assert — only the target session was revoked; the unrelated session is untouched.
        using var verifyScope = Factory.Services.CreateScope();
        var verifyDb = verifyScope.ServiceProvider.GetRequiredService<IIAMDbContext>();

        var targetSession = await verifyDb.Sessions.AsNoTracking().SingleAsync(s => s.Id == targetSessionId);
        Assert.NotNull(targetSession.RevokedAt);
        Assert.Equal(SessionRevokedReason.TokenReuseDetected, targetSession.RevokedReason);

        var otherSession = await verifyDb.Sessions.AsNoTracking().SingleAsync(s => s.Id == otherSessionId);
        Assert.Null(otherSession.RevokedAt);

        // Assert — even the CURRENT valid token for the now-revoked session fails, distinctly, going forward.
        var thirdResponse = await client.PostAsJsonAsync(
            new Uri("/tokens/refresh", UriKind.Relative), new Request { RefreshToken = currentRefreshToken! });
        Assert.Equal(HttpStatusCode.Unauthorized, thirdResponse.StatusCode);
        var thirdJson = await thirdResponse.Content.ReadAsStringAsync();
        Assert.Equal("SessionRevoked", JsonDocument.Parse(thirdJson).RootElement.GetProperty("errorKey").GetString());
    }

    [Fact]
    public async Task RefreshToken_ReplayOfJustConsumedToken_WithinGraceWindow_RotatesSuccessorInstead()
    {
        // Arrange — simulates a lost-response retry: the client rotated successfully but the app
        // died before it saw the response, so it retries moments later with the now-dead token.
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        var timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();

        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(CultureInfo.InvariantCulture);
        var user = ApplicationUser.Create(
            _faker.Name.FullName(), phoneNumber, DateOnly.FromDateTime(_faker.Date.Past(30)));

        var utcNow = timeProvider.GetUtcNow();
        var (deadBytes, deadExpiresAt) = tokenService.GenerateRefreshToken(utcNow);
        user.IssueSessionAndToken(
            null, Guid.NewGuid(), "mobile-app-1", null, null, null, SHA256.HashData(deadBytes),
            utcNow, deadExpiresAt, utcNow.AddDays(90));

        db.Users.Add(user);
        await db.SaveChangesAsync();

        var sessionId = user.Sessions.Single().Id;

        var client = Factory.CreateClient();
        var request = new Request { RefreshToken = Convert.ToBase64String(deadBytes) };

        // First use — rotates it away (this is the response the client never received).
        var firstResponse = await client.PostAsJsonAsync(new Uri("/tokens/refresh", UriKind.Relative), request);
        Assert.True(firstResponse.IsSuccessStatusCode);

        // Act — retry with the dead token moments later, well within the grace window.
        var retryResponse = await client.PostAsJsonAsync(new Uri("/tokens/refresh", UriKind.Relative), request);

        // Assert — treated as a legitimate retry: fresh tokens, no reuse detection, session intact.
        Assert.True(retryResponse.IsSuccessStatusCode);
        var retryJson = await retryResponse.Content.ReadAsStringAsync();
        var retryRoot = JsonDocument.Parse(retryJson).RootElement;
        Assert.False(string.IsNullOrWhiteSpace(retryRoot.GetProperty("accessToken").GetString()));
        Assert.False(string.IsNullOrWhiteSpace(retryRoot.GetProperty("refreshToken").GetString()));

        using var verifyScope = Factory.Services.CreateScope();
        var verifyDb = verifyScope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var session = await verifyDb.Sessions.AsNoTracking().SingleAsync(s => s.Id == sessionId);
        Assert.Null(session.RevokedAt);
    }

    [Fact]
    public async Task RefreshToken_ReplayWithinGraceWindow_ButSuccessorExpired_FailsClosedAsReuse()
    {
        // Arrange — the dead token's direct successor exists but is itself expired (edge of the
        // sliding-session window). The grace bypass must fail closed to reuse detection here, not
        // silently accept an expired successor. The real endpoint always mints a 14-day-out token,
        // so this state is only reachable by rotating directly through the domain method.
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        var timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();

        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(CultureInfo.InvariantCulture);
        var user = ApplicationUser.Create(
            _faker.Name.FullName(), phoneNumber, DateOnly.FromDateTime(_faker.Date.Past(30)));

        var utcNow = timeProvider.GetUtcNow();
        var (deadBytes, deadExpiresAt) = tokenService.GenerateRefreshToken(utcNow);
        user.IssueSessionAndToken(
            null, Guid.NewGuid(), "mobile-app-1", null, null, null, SHA256.HashData(deadBytes),
            utcNow, deadExpiresAt, utcNow.AddDays(90));

        db.Users.Add(user);
        await db.SaveChangesAsync();

        var sessionId = user.Sessions.Single().Id;

        // Rotate directly through the domain to a successor whose expiry is already in the past.
        using (var mutateScope = Factory.Services.CreateScope())
        {
            var mutateDb = mutateScope.ServiceProvider.GetRequiredService<IIAMDbContext>();
            var trackedUser = await mutateDb.Users
                .Include(u => u.Sessions.Where(s => s.Id == sessionId))
                .ThenInclude(s => s.RefreshTokens)
                .SingleAsync(u => u.Id == user.Id);
            var trackedSession = trackedUser.Sessions.Single();
            var trackedToken = trackedSession.RefreshTokens.Single();
            var (successorBytes, _) = tokenService.GenerateRefreshToken(utcNow);

            trackedUser.RotateRefreshToken(
                trackedSession, trackedToken, SHA256.HashData(successorBytes), null, null, utcNow,
                utcNow.AddSeconds(-1));
            await mutateDb.SaveChangesAsync();
        }

        var client = Factory.CreateClient();
        var request = new Request { RefreshToken = Convert.ToBase64String(deadBytes) };

        // Act — replay the dead token immediately (within grace), but its successor is expired.
        var response = await client.PostAsJsonAsync(new Uri("/tokens/refresh", UriKind.Relative), request);

        // Assert — treated as reuse: generic 401, session revoked.
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        var json = await response.Content.ReadAsStringAsync();
        Assert.Equal("InvalidRefreshToken", JsonDocument.Parse(json).RootElement.GetProperty("errorKey").GetString());

        using var verifyScope = Factory.Services.CreateScope();
        var verifyDb = verifyScope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var session = await verifyDb.Sessions.AsNoTracking().SingleAsync(s => s.Id == sessionId);
        Assert.NotNull(session.RevokedAt);
        Assert.Equal(SessionRevokedReason.TokenReuseDetected, session.RevokedReason);
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
