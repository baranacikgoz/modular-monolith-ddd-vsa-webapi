using Common.Domain.StronglyTypedIds;
using Common.Tests;
using IAM.Domain.Identity;
using IAM.Domain.Identity.DomainEvents.v1;
using IAM.Domain.Identity.Sessions;
using Xunit;

namespace IAM.Tests.Identity;

public class ApplicationUserTests : AggregateTests<ApplicationUser, ApplicationUserId>
{
    private const string FullName = "John Doe";
    private const string PhoneNumber = "1234567890";
    private static readonly DateOnly BirthDate = new(1990, 1, 1);
    private static readonly Uri ImageUrl = new("https://example.com/image.png");

    [Fact]
    public void CreateUserShouldRaiseUserRegisteredDomainEvent()
    {
        Given(() => ApplicationUser.Create(FullName, PhoneNumber, BirthDate, ImageUrl))
            .Then<V1UserRegisteredDomainEvent>(
                @event => Assert.Equal(FullName.Trim(), @event.FullName),
                @event => Assert.Equal(PhoneNumber, @event.PhoneNumber),
                @event => Assert.Equal(BirthDate, @event.BirthDate),
                @event => Assert.Equal(ImageUrl, @event.ImageUrl));
    }

    [Fact]
    public void UpdateImageUrlShouldRaiseUserImageUrlUpdatedDomainEvent()
    {
        var newImageUrl = new Uri("https://example.com/new-image.png");

        Given(() => ApplicationUser.Create(FullName, PhoneNumber, BirthDate, ImageUrl))
            .When(user => user.UpdateImageUrl(newImageUrl))
            .Then(user => Assert.Equal(newImageUrl, user.ImageUrl))
            .Then<V1UserImageUrlUpdatedDomainEvent>(@event => Assert.Equal(newImageUrl, @event.ImageUrl));
    }

    [Fact]
    public void IssueSessionAndToken_NewTriple_CreatesSessionAndRaisesEvent()
    {
        var deviceId = Guid.NewGuid();
        var now = DateTimeOffset.UtcNow;

        Given(() => ApplicationUser.Create(FullName, PhoneNumber, BirthDate, ImageUrl))
            .When(user =>
            {
                user.IssueSessionAndToken(
                    null, deviceId, "mobile-app-1", "iPhone 15", "127.0.0.1", "UA", [1, 2, 3],
                    now, now.AddDays(14), now.AddDays(90));
            })
            .Then(user => Assert.Single(user.Sessions))
            .Then(user => Assert.Equal(deviceId, user.Sessions.Single().DeviceId))
            .Then(user => Assert.Single(user.Sessions.Single().RefreshTokens))
            .Then<V1SessionCreatedDomainEvent>(
                @event => Assert.Equal(deviceId, @event.DeviceId),
                @event => Assert.Equal("mobile-app-1", @event.ClientId),
                @event => Assert.Equal("iPhone 15", @event.DeviceName));
    }

    [Fact]
    public void IssueSessionAndToken_ExistingTriple_ReusesSessionAndSupersedesOldTokens()
    {
        var deviceId = Guid.NewGuid();
        var now = DateTimeOffset.UtcNow;

        Given(() => ApplicationUser.Create(FullName, PhoneNumber, BirthDate, ImageUrl))
            .When(user =>
            {
                user.IssueSessionAndToken(
                    null, deviceId, "mobile-app-1", "iPhone", "1.1.1.1", "UA1", [1], now, now.AddDays(14), now.AddDays(90));
            })
            .When(user =>
            {
                user.IssueSessionAndToken(
                    user.Sessions.Single(), deviceId, "mobile-app-1", "iPhone 15", "2.2.2.2", "UA2", [2],
                    now.AddMinutes(1), now.AddDays(14), now.AddDays(90));
            })
            .Then(user => Assert.Single(user.Sessions))
            .Then(user => Assert.Equal("iPhone 15", user.Sessions.Single().DeviceName))
            .Then(user =>
            {
                var tokens = user.Sessions.Single().RefreshTokens;
                Assert.Equal(2, tokens.Count);
                Assert.Single(tokens, t => t.ConsumedAt == null);
                Assert.Single(tokens, t => t.ConsumedAt != null);
            })
            .Then<V1SessionRefreshedDomainEvent>(_ => { });
    }

    [Fact]
    public void RotateRefreshToken_ConsumesOldAndLinksReplacedBy()
    {
        var deviceId = Guid.NewGuid();
        var now = DateTimeOffset.UtcNow;

        Given(() => ApplicationUser.Create(FullName, PhoneNumber, BirthDate, ImageUrl))
            .When(user =>
            {
                user.IssueSessionAndToken(
                    null, deviceId, "mobile-app-1", null, "1.1.1.1", "UA1", [1], now, now.AddDays(14), now.AddDays(90));
            })
            .When(user =>
            {
                var session = user.Sessions.Single();
                var current = session.RefreshTokens.Single();
                user.RotateRefreshToken(session, current, [2], "2.2.2.2", "UA2", now.AddMinutes(1), now.AddDays(14));
            })
            .Then(user =>
            {
                var tokens = user.Sessions.Single().RefreshTokens;
                Assert.Equal(2, tokens.Count);
                var oldToken = tokens.Single(t => t.TokenHash.SequenceEqual((byte[]) [1]));
                var newToken = tokens.Single(t => t.TokenHash.SequenceEqual((byte[]) [2]));
                Assert.NotNull(oldToken.ConsumedAt);
                Assert.Equal(newToken.Id, oldToken.ReplacedByTokenId);
                Assert.Null(newToken.ConsumedAt);
            })
            .Then(user => Assert.Equal("2.2.2.2", user.Sessions.Single().LastIp))
            .Then<V1SessionRefreshedDomainEvent>(_ => { });
    }

    [Fact]
    public void RevokeSession_SetsRevokedAtAndReason()
    {
        var deviceId = Guid.NewGuid();
        var now = DateTimeOffset.UtcNow;
        var revokedAt = now.AddMinutes(5);

        Given(() => ApplicationUser.Create(FullName, PhoneNumber, BirthDate, ImageUrl))
            .When(user =>
            {
                user.IssueSessionAndToken(
                    null, deviceId, "mobile-app-1", null, null, null, [1], now, now.AddDays(14), now.AddDays(90));
            })
            .When(user =>
            {
                user.RevokeSession(user.Sessions.Single(), SessionRevokedReason.UserSignedOut, revokedAt);
            })
            .Then(user =>
            {
                var session = user.Sessions.Single();
                Assert.Equal(SessionRevokedReason.UserSignedOut, session.RevokedReason);
                Assert.Equal(revokedAt, session.RevokedAt);
            })
            .Then<V1SessionRevokedDomainEvent>(@event => Assert.Equal(SessionRevokedReason.UserSignedOut, @event.Reason));
    }

    [Fact]
    public void RevokeAllSessions_RevokesEveryActiveSession_RaisesOneEvent()
    {
        var now = DateTimeOffset.UtcNow;

        Given(() => ApplicationUser.Create(FullName, PhoneNumber, BirthDate, ImageUrl))
            .When(user =>
            {
                user.IssueSessionAndToken(
                    null, Guid.NewGuid(), "mobile-app-1", null, null, null, [1], now, now.AddDays(14), now.AddDays(90));
                user.IssueSessionAndToken(
                    null, Guid.NewGuid(), "web-app-1", null, null, null, [2], now, now.AddDays(14), now.AddDays(90));
            })
            .When(user => user.RevokeAllSessions(SessionRevokedReason.SignedOutEverywhere, now.AddMinutes(1)))
            .Then(user => Assert.All(
                user.Sessions, s => Assert.Equal(SessionRevokedReason.SignedOutEverywhere, s.RevokedReason)))
            .Then<V1AllSessionsRevokedDomainEvent>(
                @event => Assert.Equal(SessionRevokedReason.SignedOutEverywhere, @event.Reason));
    }

    [Fact]
    public void RevokeAllSessions_WithNoActiveSessions_RaisesNoEvent()
    {
        // A user who has never logged in (or whose only session is already revoked) must not produce
        // a no-op AuditLog entry / bump Version for nothing.
        var now = DateTimeOffset.UtcNow;

        Given(() => ApplicationUser.Create(FullName, PhoneNumber, BirthDate, ImageUrl))
            .When(user => user.RevokeAllSessions(SessionRevokedReason.SignedOutEverywhere, now))
            .ThenNoEventsOfType<V1AllSessionsRevokedDomainEvent>();
    }

    [Fact]
    public void IssueSessionAndToken_ExistingTripleWithNullDeviceName_PreservesPriorDeviceName()
    {
        // A refresh/re-login that omits DeviceName must not silently wipe a friendly name the user
        // set on a prior login (e.g. via a client that never sends it).
        var deviceId = Guid.NewGuid();
        var now = DateTimeOffset.UtcNow;

        Given(() => ApplicationUser.Create(FullName, PhoneNumber, BirthDate, ImageUrl))
            .When(user =>
            {
                user.IssueSessionAndToken(
                    null, deviceId, "mobile-app-1", "Barbara's iPhone", "1.1.1.1", "UA1", [1],
                    now, now.AddDays(14), now.AddDays(90));
            })
            .When(user =>
            {
                user.IssueSessionAndToken(
                    user.Sessions.Single(), deviceId, "mobile-app-1", null, "2.2.2.2", "UA2", [2],
                    now.AddMinutes(1), now.AddDays(14), now.AddDays(90));
            })
            .Then(user => Assert.Equal("Barbara's iPhone", user.Sessions.Single().DeviceName));
    }

    [Fact]
    public void IssueSessionAndToken_OnRevokedSession_ReactivatesIt()
    {
        // "Sign out this device" then logging back in on the SAME device/app must revive the
        // session, not leave the user permanently locked out of that (DeviceId, ClientId) pair.
        var deviceId = Guid.NewGuid();
        var now = DateTimeOffset.UtcNow;

        Given(() => ApplicationUser.Create(FullName, PhoneNumber, BirthDate, ImageUrl))
            .When(user =>
            {
                user.IssueSessionAndToken(
                    null, deviceId, "mobile-app-1", null, null, null, [1], now, now.AddDays(14), now.AddDays(90));
            })
            .When(user => user.RevokeSession(user.Sessions.Single(), SessionRevokedReason.UserSignedOut, now.AddMinutes(1)))
            .When(user =>
            {
                user.IssueSessionAndToken(
                    user.Sessions.Single(), deviceId, "mobile-app-1", null, null, null, [2],
                    now.AddMinutes(2), now.AddDays(14), now.AddDays(90));
            })
            .Then(user =>
            {
                var session = user.Sessions.Single();
                Assert.Null(session.RevokedAt);
                Assert.Null(session.RevokedReason);
            });
    }

    [Fact]
    public void RotateRefreshToken_DoesNotExtendSessionAbsoluteExpiry()
    {
        // A plain token rotation must never reset the hard session-lifetime cap — only a fresh
        // login (IssueSessionAndToken) may do that. Otherwise the absolute-expiry cap is pointless.
        var deviceId = Guid.NewGuid();
        var now = DateTimeOffset.UtcNow;
        var originalAbsoluteExpiry = now.AddDays(90);

        Given(() => ApplicationUser.Create(FullName, PhoneNumber, BirthDate, ImageUrl))
            .When(user =>
            {
                user.IssueSessionAndToken(
                    null, deviceId, "mobile-app-1", null, null, null, [1], now, now.AddDays(14), originalAbsoluteExpiry);
            })
            .When(user =>
            {
                var session = user.Sessions.Single();
                var current = session.RefreshTokens.Single();
                user.RotateRefreshToken(session, current, [2], null, null, now.AddDays(13), now.AddDays(27));
            })
            .Then(user => Assert.Equal(originalAbsoluteExpiry, user.Sessions.Single().AbsoluteExpiresAt));
    }
}
