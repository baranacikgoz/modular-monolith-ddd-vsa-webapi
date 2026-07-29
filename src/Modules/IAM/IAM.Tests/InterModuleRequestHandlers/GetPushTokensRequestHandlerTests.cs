using System.Globalization;
using System.Security.Cryptography;
using Bogus;
using Common.InterModuleRequests.IAM;
using Common.Tests;
using IAM.Application.Persistence;
using IAM.Application.Tokens.Services;
using IAM.Domain.Identity;
using IAM.Domain.Identity.Sessions;
using IAM.Infrastructure.InterModuleRequestHandlers;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IAM.Tests.InterModuleRequestHandlers;

[Collection("IntegrationTestCollection")]
public class GetPushTokensRequestHandlerTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public GetPushTokensRequestHandlerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    private ApplicationUser CreateUser()
    {
        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(CultureInfo.InvariantCulture);
        return ApplicationUser.Create(_faker.Name.FullName(), phoneNumber, DateOnly.FromDateTime(_faker.Date.Past(30)));
    }

    private static Session AddSessionWithPushToken(
        ITokenService tokenService, DateTimeOffset now, ApplicationUser user, string? pushToken)
    {
        var (tokenBytes, expiresAt) = tokenService.GenerateRefreshToken(now);
        var refreshToken = user.IssueSessionAndToken(
            null, Guid.NewGuid(), "mobile-app-1", null, null, null, SHA256.HashData(tokenBytes),
            now, expiresAt, now.AddDays(90));
        var session = user.Sessions.Single(s => s.Id == refreshToken.SessionId);

        if (pushToken is not null)
        {
            user.UpdateSessionPushToken(session, pushToken, now);
        }

        return session;
    }

    [Fact]
    public async Task HandleAsync_LiveSessionWithPushToken_ReturnsTarget()
    {
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        var timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();
        var now = timeProvider.GetUtcNow();

        var user = CreateUser();
        AddSessionWithPushToken(tokenService, now, user, "live-token");
        db.Users.Add(user);
        await db.SaveChangesAsync();

        var handler = new GetPushTokensRequestHandler(db, timeProvider);
        var response = await handler.HandleAsync(new GetPushTokensRequest([user.Id]), CancellationToken.None);

        var target = Assert.Single(response.Targets);
        Assert.Equal(user.Id, target.UserId);
        Assert.Equal("live-token", target.Token);
    }

    [Fact]
    public async Task HandleAsync_SessionWithoutPushToken_ExcludesUser()
    {
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        var timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();
        var now = timeProvider.GetUtcNow();

        var user = CreateUser();
        AddSessionWithPushToken(tokenService, now, user, pushToken: null);
        db.Users.Add(user);
        await db.SaveChangesAsync();

        var handler = new GetPushTokensRequestHandler(db, timeProvider);
        var response = await handler.HandleAsync(new GetPushTokensRequest([user.Id]), CancellationToken.None);

        Assert.Empty(response.Targets);
    }

    [Fact]
    public async Task HandleAsync_RevokedSession_ExcludesUser()
    {
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        var timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();
        var now = timeProvider.GetUtcNow();

        var user = CreateUser();
        var session = AddSessionWithPushToken(tokenService, now, user, "revoked-token");
        user.RevokeSession(session, SessionRevokedReason.UserSignedOut, now);
        db.Users.Add(user);
        await db.SaveChangesAsync();

        var handler = new GetPushTokensRequestHandler(db, timeProvider);
        var response = await handler.HandleAsync(new GetPushTokensRequest([user.Id]), CancellationToken.None);

        Assert.Empty(response.Targets);
    }

    [Fact]
    public async Task HandleAsync_ExpiredSession_ExcludesUser()
    {
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
        var timeProvider = scope.ServiceProvider.GetRequiredService<TimeProvider>();
        var now = timeProvider.GetUtcNow();

        var user = CreateUser();
        var (tokenBytes, expiresAt) = tokenService.GenerateRefreshToken(now);
        var refreshToken = user.IssueSessionAndToken(
            null, Guid.NewGuid(), "mobile-app-1", null, null, null, SHA256.HashData(tokenBytes),
            now, expiresAt, now.AddDays(-1)); // already-expired session
        var session = user.Sessions.Single(s => s.Id == refreshToken.SessionId);
        user.UpdateSessionPushToken(session, "expired-token", now);
        db.Users.Add(user);
        await db.SaveChangesAsync();

        var handler = new GetPushTokensRequestHandler(db, timeProvider);
        var response = await handler.HandleAsync(new GetPushTokensRequest([user.Id]), CancellationToken.None);

        Assert.Empty(response.Targets);
    }
}
