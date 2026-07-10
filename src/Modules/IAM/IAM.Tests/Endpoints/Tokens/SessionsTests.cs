using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text.Json;
using Bogus;
using Common.Tests;
using IAM.Application.Persistence;
using IAM.Application.Tokens.Services;
using IAM.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using RefreshRequest = IAM.Endpoints.Tokens.VersionNeutral.Refresh.Request;

namespace IAM.Tests.Endpoints.Tokens;

[Collection("IntegrationTestCollection")]
public class SessionsTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public SessionsTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    private ApplicationUser CreateUser()
    {
        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(CultureInfo.InvariantCulture);
        return ApplicationUser.Create(_faker.Name.FullName(), phoneNumber, DateOnly.FromDateTime(_faker.Date.Past(30)));
    }

    [Fact]
    public async Task ListSessions_ReturnsOwnSessionsWithIsCurrentFlag()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        var timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();

        var user = CreateUser();
        var utcNow = timeProvider.GetUtcNow();

        var (tokenABytes, expiresA) = tokenService.GenerateRefreshToken(utcNow);
        var refreshTokenA = user.IssueSessionAndToken(
            null, Guid.NewGuid(), "mobile-app-1", "iPhone", null, null, SHA256.HashData(tokenABytes),
            utcNow, expiresA, utcNow.AddDays(90));

        var (tokenBBytes, expiresB) = tokenService.GenerateRefreshToken(utcNow);
        user.IssueSessionAndToken(
            null, Guid.NewGuid(), "web-app-1", "Chrome", null, null, SHA256.HashData(tokenBBytes),
            utcNow, expiresB, utcNow.AddDays(90));

        db.Users.Add(user);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(TestAuthHandler.AuthenticationScheme);
        client.DefaultRequestHeaders.Add("X-Test-User-Id", user.Id.Value.ToString());
        client.DefaultRequestHeaders.Add("X-Test-Session-Id", refreshTokenA.SessionId.Value.ToString());

        // Act
        var response = await client.GetAsync(new Uri("/tokens/sessions", UriKind.Relative));

        // Assert
        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync();
            Assert.Fail($"Status: {response.StatusCode}. Error: {err}");
        }

        var rawJson = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(rawJson);
        var items = doc.RootElement.EnumerateArray().ToList();

        Assert.Equal(2, items.Count);
        Assert.Single(items, i => i.GetProperty("isCurrent").GetBoolean());
        var current = items.Single(i => i.GetProperty("isCurrent").GetBoolean());
        Assert.Equal(refreshTokenA.SessionId.Value.ToString(), current.GetProperty("id").GetGuid().ToString());
        Assert.Equal("mobile-app-1", current.GetProperty("clientId").GetString());
    }

    [Fact]
    public async Task RevokeSession_NotOwnedByCaller_ReturnsNotFound()
    {
        // Arrange — session belongs to a DIFFERENT user than the caller.
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        var timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();

        var owner = CreateUser();
        var caller = CreateUser();
        var utcNow = timeProvider.GetUtcNow();

        var (tokenBytes, expiresAt) = tokenService.GenerateRefreshToken(utcNow);
        var ownerSession = owner.IssueSessionAndToken(
            null, Guid.NewGuid(), "mobile-app-1", null, null, null, SHA256.HashData(tokenBytes),
            utcNow, expiresAt, utcNow.AddDays(90));

        db.Users.AddRange(owner, caller);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(TestAuthHandler.AuthenticationScheme);
        client.DefaultRequestHeaders.Add("X-Test-User-Id", caller.Id.Value.ToString());

        // Act — caller tries to revoke a session that belongs to someone else.
        var response = await client.DeleteAsync(
            new Uri($"/tokens/sessions/{ownerSession.SessionId.Value}", UriKind.Relative));

        // Assert — resolves as not-found, never leaks that the session exists for another user.
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        using var verifyScope = Factory.Services.CreateScope();
        var verifyDb = verifyScope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var session = await verifyDb.Sessions.AsNoTracking().SingleAsync(s => s.Id == ownerSession.SessionId);
        Assert.Null(session.RevokedAt);
    }

    [Fact]
    public async Task RevokeAllSessions_SignsOutEverywhere_AllPriorRefreshTokensFail()
    {
        // Arrange — two independent sessions for the same user.
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        var timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();

        var user = CreateUser();
        var utcNow = timeProvider.GetUtcNow();

        var (tokenABytes, expiresA) = tokenService.GenerateRefreshToken(utcNow);
        user.IssueSessionAndToken(
            null, Guid.NewGuid(), "mobile-app-1", null, null, null, SHA256.HashData(tokenABytes),
            utcNow, expiresA, utcNow.AddDays(90));

        var (tokenBBytes, expiresB) = tokenService.GenerateRefreshToken(utcNow);
        user.IssueSessionAndToken(
            null, Guid.NewGuid(), "web-app-1", null, null, null, SHA256.HashData(tokenBBytes),
            utcNow, expiresB, utcNow.AddDays(90));

        db.Users.Add(user);
        await db.SaveChangesAsync();

        var client = Factory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(TestAuthHandler.AuthenticationScheme);
        client.DefaultRequestHeaders.Add("X-Test-User-Id", user.Id.Value.ToString());

        // Act
        var response = await client.DeleteAsync(new Uri("/tokens/sessions", UriKind.Relative));
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        // Assert — both prior refresh tokens now fail.
        var responseA = await client.PostAsJsonAsync(
            new Uri("/tokens/refresh", UriKind.Relative), new RefreshRequest { RefreshToken = Convert.ToBase64String(tokenABytes) });
        var responseB = await client.PostAsJsonAsync(
            new Uri("/tokens/refresh", UriKind.Relative), new RefreshRequest { RefreshToken = Convert.ToBase64String(tokenBBytes) });

        Assert.Equal(HttpStatusCode.Unauthorized, responseA.StatusCode);
        Assert.Equal(HttpStatusCode.Unauthorized, responseB.StatusCode);
    }
}
