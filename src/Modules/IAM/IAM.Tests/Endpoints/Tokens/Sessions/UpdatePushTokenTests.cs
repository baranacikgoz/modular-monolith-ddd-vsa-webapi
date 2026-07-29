using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using Bogus;
using Common.Tests;
using IAM.Application.Persistence;
using IAM.Application.Tokens.Services;
using IAM.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Request = IAM.Endpoints.Tokens.VersionNeutral.Sessions.UpdatePushToken.Request;

namespace IAM.Tests.Endpoints.Tokens.Sessions;

[Collection("IntegrationTestCollection")]
public class UpdatePushTokenTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public UpdatePushTokenTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    private ApplicationUser CreateUser()
    {
        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(CultureInfo.InvariantCulture);
        return ApplicationUser.Create(_faker.Name.FullName(), phoneNumber, DateOnly.FromDateTime(_faker.Date.Past(30)));
    }

    [Fact]
    public async Task UpdatePushToken_ValidSession_SetsTokenOnlyOnThatSession()
    {
        // Arrange — two sessions, only the current one (matching X-Test-Session-Id) should be updated.
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        var timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();
        var now = timeProvider.GetUtcNow();

        var user = CreateUser();
        var (tokenABytes, expiresA) = tokenService.GenerateRefreshToken(now);
        var refreshTokenA = user.IssueSessionAndToken(
            null, Guid.NewGuid(), "mobile-app-1", null, null, null, SHA256.HashData(tokenABytes),
            now, expiresA, now.AddDays(90));

        var (tokenBBytes, expiresB) = tokenService.GenerateRefreshToken(now);
        var refreshTokenB = user.IssueSessionAndToken(
            null, Guid.NewGuid(), "web-app-1", null, null, null, SHA256.HashData(tokenBBytes),
            now, expiresB, now.AddDays(90));

        db.Users.Add(user);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(TestAuthHandler.AuthenticationScheme);
        client.DefaultRequestHeaders.Add("X-Test-User-Id", user.Id.Value.ToString());
        client.DefaultRequestHeaders.Add("X-Test-Session-Id", refreshTokenA.SessionId.Value.ToString());

        // Act
        var response = await client.PutAsJsonAsync(
            new Uri("/tokens/sessions/current/push-token", UriKind.Relative),
            new Request { PushToken = "fcm-token-a" });

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        using var verifyScope = Factory.Services.CreateScope();
        var verifyDb = verifyScope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var sessionA = await verifyDb.Sessions.AsNoTracking().SingleAsync(s => s.Id == refreshTokenA.SessionId);
        var sessionB = await verifyDb.Sessions.AsNoTracking().SingleAsync(s => s.Id == refreshTokenB.SessionId);
        Assert.Equal("fcm-token-a", sessionA.PushToken);
        Assert.Null(sessionB.PushToken);
    }

    [Fact]
    public async Task UpdatePushToken_CalledAgain_RotatesToken()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        var timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();
        var now = timeProvider.GetUtcNow();

        var user = CreateUser();
        var (tokenBytes, expiresAt) = tokenService.GenerateRefreshToken(now);
        var refreshToken = user.IssueSessionAndToken(
            null, Guid.NewGuid(), "mobile-app-1", null, null, null, SHA256.HashData(tokenBytes),
            now, expiresAt, now.AddDays(90));
        var session = user.Sessions.Single(s => s.Id == refreshToken.SessionId);
        user.UpdateSessionPushToken(session, "old-token", now);
        db.Users.Add(user);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(TestAuthHandler.AuthenticationScheme);
        client.DefaultRequestHeaders.Add("X-Test-User-Id", user.Id.Value.ToString());
        client.DefaultRequestHeaders.Add("X-Test-Session-Id", refreshToken.SessionId.Value.ToString());

        // Act
        var response = await client.PutAsJsonAsync(
            new Uri("/tokens/sessions/current/push-token", UriKind.Relative),
            new Request { PushToken = "rotated-token" });

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        using var verifyScope = Factory.Services.CreateScope();
        var verifyDb = verifyScope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var updated = await verifyDb.Sessions.AsNoTracking().SingleAsync(s => s.Id == refreshToken.SessionId);
        Assert.Equal("rotated-token", updated.PushToken);
    }

    [Fact]
    public async Task UpdatePushToken_SessionIdNotFound_ReturnsNotFound()
    {
        // Arrange — caller's SessionId claim matches no session in the DB.
        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(TestAuthHandler.AuthenticationScheme);

        // Act — default X-Test-User-Id/X-Test-Session-Id are random, no matching user/session exists.
        var response = await client.PutAsJsonAsync(
            new Uri("/tokens/sessions/current/push-token", UriKind.Relative),
            new Request { PushToken = "orphaned-token" });

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
