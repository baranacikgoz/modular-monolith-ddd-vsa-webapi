using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text.Json;
using Bogus;
using Common.Application.Caching;
using Common.Tests;
using EntityFramework.Exceptions.Common;
using IAM.Application.Persistence;
using IAM.Application.Tokens.Services;
using IAM.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using ZiggyCreatures.Caching.Fusion;
using RefreshRequest = IAM.Endpoints.Tokens.VersionNeutral.Refresh.Request;

namespace IAM.Tests.Endpoints.Tokens;

// Extreme/security-relevant edge cases for the multi-session refresh-token feature that go beyond
// the happy-path coverage in CreateTests/RefreshTests/RevokeTests/SessionsTests.
[Collection("IntegrationTestCollection")]
public class SessionEdgeCasesTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public SessionEdgeCasesTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    private ApplicationUser CreateUser()
    {
        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(CultureInfo.InvariantCulture);
        return ApplicationUser.Create(_faker.Name.FullName(), phoneNumber, DateOnly.FromDateTime(_faker.Date.Past(30)));
    }

    [Fact]
    public async Task CreateTokens_ReLoginOnRevokedSession_ReactivatesAndWorksNormally()
    {
        // Arrange — sign out this device via the dedicated endpoint, THEN log back in on the same
        // (DeviceId, ClientId). The user must not be permanently locked out of that device.
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var cache = scope.ServiceProvider.GetRequiredService<IFusionCache>();
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        var timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();

        var user = CreateUser();
        var deviceId = Guid.NewGuid();
        var utcNow = timeProvider.GetUtcNow();
        var (tokenBytes, expiresAt) = tokenService.GenerateRefreshToken(utcNow);
        var refreshToken = user.IssueSessionAndToken(
            null, deviceId, "mobile-app-1", null, null, null, SHA256.HashData(tokenBytes), utcNow, expiresAt, utcNow.AddDays(90));

        db.Users.Add(user);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(TestAuthHandler.AuthenticationScheme);
        client.DefaultRequestHeaders.Add("X-Test-User-Id", user.Id.Value.ToString());

        var revokeResponse = await client.DeleteAsync(
            new Uri($"/tokens/sessions/{refreshToken.SessionId.Value}", UriKind.Relative));
        Assert.Equal(HttpStatusCode.NoContent, revokeResponse.StatusCode);

        // Act — log back in on the SAME device/app.
        var phoneNumber = user.PhoneNumber!;
        const string otp = "123456";
        var cacheKey = CacheKeys.For.Otp(phoneNumber, "login");
        await cache.SetAsync(cacheKey, new OtpCacheEntry(otp, 0, DateTimeOffset.UtcNow.AddMinutes(5)),
            new FusionCacheEntryOptions { Duration = TimeSpan.FromMinutes(5) });

        var loginClient = Factory.CreateClient();
        var loginResponse = await loginClient.PostAsJsonAsync(new Uri("/tokens", UriKind.Relative),
            new IAM.Endpoints.Tokens.VersionNeutral.Create.Request
            {
                PhoneNumber = phoneNumber, Otp = otp, DeviceId = deviceId, ClientId = "mobile-app-1"
            });

        // Assert — re-login succeeds and the session is un-revoked, not stuck dead forever.
        loginResponse.EnsureSuccessStatusCode();

        using var verifyScope = Factory.Services.CreateScope();
        var verifyDb = verifyScope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var session = await verifyDb.Sessions.AsNoTracking().SingleAsync(s => s.Id == refreshToken.SessionId);
        Assert.Null(session.RevokedAt);

        // And the newly issued refresh token actually works.
        var loginJson = await loginResponse.Content.ReadAsStringAsync();
        var newRefreshToken = JsonDocument.Parse(loginJson).RootElement.GetProperty("refreshToken").GetString();
        var refreshResponse = await loginClient.PostAsJsonAsync(
            new Uri("/tokens/refresh", UriKind.Relative), new RefreshRequest { RefreshToken = newRefreshToken! });
        Assert.True(refreshResponse.IsSuccessStatusCode);
    }

    [Fact]
    public async Task RefreshToken_AfterSessionAbsoluteExpiry_ReturnsExpiredError()
    {
        // Arrange — the refresh TOKEN itself is still within its per-token expiry, but the SESSION's
        // hard absolute-lifetime cap has passed. This must still be rejected even though rotation
        // would otherwise be perfectly valid — proves the absolute cap is actually enforced.
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        var timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();

        var user = CreateUser();
        var utcNow = timeProvider.GetUtcNow();
        var (tokenBytes, _) = tokenService.GenerateRefreshToken(utcNow);
        var tokenExpiresAt = utcNow.AddDays(14); // token itself is far from expired
        var sessionAbsoluteExpiresAt = utcNow.AddMinutes(-1); // but the session's hard cap already passed
        user.IssueSessionAndToken(
            null, Guid.NewGuid(), "mobile-app-1", null, null, null, SHA256.HashData(tokenBytes),
            utcNow, tokenExpiresAt, sessionAbsoluteExpiresAt);

        db.Users.Add(user);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();

        // Act
        var response = await client.PostAsJsonAsync(
            new Uri("/tokens/refresh", UriKind.Relative), new RefreshRequest { RefreshToken = Convert.ToBase64String(tokenBytes) });

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        var json = await response.Content.ReadAsStringAsync();
        Assert.Equal("RefreshTokenExpired", JsonDocument.Parse(json).RootElement.GetProperty("errorKey").GetString());
    }

    [Fact]
    public async Task Sessions_RevokeThenRefresh_FailsWithSessionRevoked()
    {
        // Arrange — proves the dedicated "sign out this device" endpoint is a REAL server-side
        // invalidation: the session's refresh token must stop working immediately afterward, not
        // just have a flag flip in the DB that nothing actually enforces.
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        var timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();

        var user = CreateUser();
        var utcNow = timeProvider.GetUtcNow();
        var (tokenBytes, expiresAt) = tokenService.GenerateRefreshToken(utcNow);
        var refreshToken = user.IssueSessionAndToken(
            null, Guid.NewGuid(), "mobile-app-1", null, null, null, SHA256.HashData(tokenBytes),
            utcNow, expiresAt, utcNow.AddDays(90));

        db.Users.Add(user);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(TestAuthHandler.AuthenticationScheme);
        client.DefaultRequestHeaders.Add("X-Test-User-Id", user.Id.Value.ToString());

        // Act
        var revokeResponse = await client.DeleteAsync(
            new Uri($"/tokens/sessions/{refreshToken.SessionId.Value}", UriKind.Relative));
        Assert.Equal(HttpStatusCode.NoContent, revokeResponse.StatusCode);

        var refreshResponse = await client.PostAsJsonAsync(
            new Uri("/tokens/refresh", UriKind.Relative), new RefreshRequest { RefreshToken = Convert.ToBase64String(tokenBytes) });

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, refreshResponse.StatusCode);
        var json = await refreshResponse.Content.ReadAsStringAsync();
        Assert.Equal("SessionRevoked", JsonDocument.Parse(json).RootElement.GetProperty("errorKey").GetString());
    }

    [Fact]
    public async Task CreateTokens_SameDeviceIdDifferentUsers_CreatesSeparateSessions()
    {
        // Arrange — the uniqueness key is (UserId, DeviceId, ClientId), not (DeviceId, ClientId) alone.
        // A shared/kiosk device (or a spoofed DeviceId) used by two different accounts must not
        // collide, corrupt, or leak one user's session into the other's.
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var cache = scope.ServiceProvider.GetRequiredService<IFusionCache>();

        var userA = CreateUser();
        var userB = CreateUser();
        db.Users.AddRange(userA, userB);
        await db.SaveChangesAsync();

        var sharedDeviceId = Guid.NewGuid();
        const string otp = "123456";
        var client = Factory.CreateClient();

        await cache.SetAsync(CacheKeys.For.Otp(userA.PhoneNumber!, "login"),
            new OtpCacheEntry(otp, 0, DateTimeOffset.UtcNow.AddMinutes(5)),
            new FusionCacheEntryOptions { Duration = TimeSpan.FromMinutes(5) });
        var responseA = await client.PostAsJsonAsync(new Uri("/tokens", UriKind.Relative),
            new IAM.Endpoints.Tokens.VersionNeutral.Create.Request
            {
                PhoneNumber = userA.PhoneNumber!, Otp = otp, DeviceId = sharedDeviceId, ClientId = "mobile-app-1"
            });
        responseA.EnsureSuccessStatusCode();

        await cache.SetAsync(CacheKeys.For.Otp(userB.PhoneNumber!, "login"),
            new OtpCacheEntry(otp, 0, DateTimeOffset.UtcNow.AddMinutes(5)),
            new FusionCacheEntryOptions { Duration = TimeSpan.FromMinutes(5) });
        var responseB = await client.PostAsJsonAsync(new Uri("/tokens", UriKind.Relative),
            new IAM.Endpoints.Tokens.VersionNeutral.Create.Request
            {
                PhoneNumber = userB.PhoneNumber!, Otp = otp, DeviceId = sharedDeviceId, ClientId = "mobile-app-1"
            });

        // Assert — both logins succeed independently; no unique-constraint collision on DeviceId alone.
        responseB.EnsureSuccessStatusCode();

        using var verifyScope = Factory.Services.CreateScope();
        var verifyDb = verifyScope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var sessionA = await verifyDb.Sessions.AsNoTracking().SingleAsync(s => s.UserId == userA.Id);
        var sessionB = await verifyDb.Sessions.AsNoTracking().SingleAsync(s => s.UserId == userB.Id);
        Assert.NotEqual(sessionA.Id, sessionB.Id);
        Assert.Equal(sharedDeviceId, sessionA.DeviceId);
        Assert.Equal(sharedDeviceId, sessionB.DeviceId);
    }

    [Fact]
    public async Task ListSessions_ExcludesRevokedSessions()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        var timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();

        var user = CreateUser();
        var utcNow = timeProvider.GetUtcNow();

        var (activeBytes, activeExpiresAt) = tokenService.GenerateRefreshToken(utcNow);
        var activeToken = user.IssueSessionAndToken(
            null, Guid.NewGuid(), "mobile-app-1", null, null, null, SHA256.HashData(activeBytes),
            utcNow, activeExpiresAt, utcNow.AddDays(90));

        var (revokedBytes, revokedExpiresAt) = tokenService.GenerateRefreshToken(utcNow);
        var revokedToken = user.IssueSessionAndToken(
            null, Guid.NewGuid(), "web-app-1", null, null, null, SHA256.HashData(revokedBytes),
            utcNow, revokedExpiresAt, utcNow.AddDays(90));
        var revokedSession = user.Sessions.Single(s => s.Id == revokedToken.SessionId);
        user.RevokeSession(revokedSession, IAM.Domain.Identity.Sessions.SessionRevokedReason.UserSignedOut, utcNow);

        db.Users.Add(user);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(TestAuthHandler.AuthenticationScheme);
        client.DefaultRequestHeaders.Add("X-Test-User-Id", user.Id.Value.ToString());
        client.DefaultRequestHeaders.Add("X-Test-Session-Id", activeToken.SessionId.Value.ToString());

        // Act
        var response = await client.GetAsync(new Uri("/tokens/sessions", UriKind.Relative));

        // Assert — only the active session is listed; the revoked one is gone from the caller's view.
        var json = await response.Content.ReadAsStringAsync();
        var items = JsonDocument.Parse(json).RootElement.EnumerateArray().ToList();
        Assert.Single(items);
        Assert.Equal("mobile-app-1", items[0].GetProperty("clientId").GetString());
    }

    [Fact]
    public async Task RevokeSession_NonExistentSessionId_ReturnsNotFound()
    {
        // Arrange — a SessionId that never existed at all (not just "belongs to someone else").
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();

        var user = CreateUser();
        db.Users.Add(user);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(TestAuthHandler.AuthenticationScheme);
        client.DefaultRequestHeaders.Add("X-Test-User-Id", user.Id.Value.ToString());

        // Act
        var response = await client.DeleteAsync(new Uri($"/tokens/sessions/{Guid.NewGuid()}", UriKind.Relative));

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task RefreshToken_ConcurrentRotationOfSameToken_BothSucceedWithinGrace()
    {
        // Arrange — two requests hit /tokens/refresh with the SAME still-valid token at (almost)
        // the same instant (e.g. a client-side timeout retry racing its slow first attempt).
        // Rotation must not punish this legitimate race: the loser re-resolves through the reuse
        // grace window and rotates the winner's successor — both succeed, no 500, no revocation.
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        var timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();

        var user = CreateUser();
        var utcNow = timeProvider.GetUtcNow();
        var (tokenBytes, expiresAt) = tokenService.GenerateRefreshToken(utcNow);
        var refreshToken = user.IssueSessionAndToken(
            null, Guid.NewGuid(), "mobile-app-1", null, null, null, SHA256.HashData(tokenBytes),
            utcNow, expiresAt, utcNow.AddDays(90));

        db.Users.Add(user);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        var request = new RefreshRequest { RefreshToken = Convert.ToBase64String(tokenBytes) };

        // Act — fire both requests concurrently.
        var responses = await Task.WhenAll(
            client.PostAsJsonAsync(new Uri("/tokens/refresh", UriKind.Relative), request),
            client.PostAsJsonAsync(new Uri("/tokens/refresh", UriKind.Relative), request));
        using var response1 = responses[0];
        using var response2 = responses[1];

        // Assert — both succeeded; no 500, no revoked session.
        foreach (var r in responses)
        {
            Assert.True(r.IsSuccessStatusCode,
                $"Unexpected status: {r.StatusCode}. Body: {await r.Content.ReadAsStringAsync()}");
        }

        // Assert — a clean rotation chain: original -> winner's token -> loser's grace rotation.
        // Exactly one live token at the end; every predecessor is consumed, never orphaned.
        using var verifyScope = Factory.Services.CreateScope();
        var verifyDb = verifyScope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var tokens = await verifyDb.RefreshTokens.AsNoTracking()
            .Where(rt => rt.SessionId == refreshToken.SessionId)
            .ToListAsync();
        Assert.Equal(3, tokens.Count);
        Assert.Single(tokens, t => t.ConsumedAt == null);

        var session = await verifyDb.Sessions.AsNoTracking().SingleAsync(s => s.Id == refreshToken.SessionId);
        Assert.Null(session.RevokedAt);
    }

    [Fact]
    public async Task RefreshToken_EmptyString_ReturnsBadRequest()
    {
        var client = Factory.CreateClient();

        var response = await client.PostAsJsonAsync(
            new Uri("/tokens/refresh", UriKind.Relative), new RefreshRequest { RefreshToken = "" });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RefreshToken_ExceedingMaxLength_ReturnsBadRequest()
    {
        // Arrange — a real refresh token base64-encodes to exactly 44 chars; nothing legitimate is longer.
        var oversized = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        var client = Factory.CreateClient();

        var response = await client.PostAsJsonAsync(
            new Uri("/tokens/refresh", UriKind.Relative), new RefreshRequest { RefreshToken = oversized });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RefreshToken_WellFormedButNeverIssued_ReturnsGenericInvalidError()
    {
        // Arrange — syntactically valid base64, correct length, but no matching hash was ever persisted.
        // Distinct from the "malformed base64" case: proves the DB-miss path also fails safely, not just decode failure.
        var neverIssued = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        var client = Factory.CreateClient();

        var response = await client.PostAsJsonAsync(
            new Uri("/tokens/refresh", UriKind.Relative), new RefreshRequest { RefreshToken = neverIssued });

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        var json = await response.Content.ReadAsStringAsync();
        Assert.Equal("InvalidRefreshToken", JsonDocument.Parse(json).RootElement.GetProperty("errorKey").GetString());
    }

    [Fact]
    public async Task RefreshTokens_DuplicateTokenHash_ViolatesUniqueConstraint()
    {
        // Arrange — proves the DB actually enforces hash uniqueness now. Without it, two rows sharing a
        // hash would make the endpoint's SingleOrDefaultAsync throw and 500 every future request on that hash.
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();

        var user = CreateUser();
        var utcNow = timeProvider.GetUtcNow();
        var collidingHash = SHA256.HashData(RandomNumberGenerator.GetBytes(32));

        user.IssueSessionAndToken(
            null, Guid.NewGuid(), "mobile-app-1", null, null, null, collidingHash,
            utcNow, utcNow.AddDays(14), utcNow.AddDays(90));
        user.IssueSessionAndToken(
            null, Guid.NewGuid(), "web-app-1", null, null, null, collidingHash,
            utcNow, utcNow.AddDays(14), utcNow.AddDays(90));

        db.Users.Add(user);

        // Act & Assert
        await Assert.ThrowsAsync<UniqueConstraintException>(() => db.SaveChangesAsync());
    }
}
