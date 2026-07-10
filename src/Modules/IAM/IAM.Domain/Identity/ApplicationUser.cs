using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Domain.Aggregates;
using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;
using IAM.Domain.Identity.DomainEvents.v1;
using IAM.Domain.Identity.Sessions;
using Microsoft.AspNetCore.Identity;

namespace IAM.Domain.Identity;

#pragma warning disable CA1819 // Properties should not return arrays
public sealed partial class ApplicationUser : IdentityUser<ApplicationUserId>, IAggregateRoot
{
    private readonly List<Session> _sessions = [];

    public string FullName { get; private set; } = string.Empty;
    public DateOnly BirthDate { get; private set; }
    public Uri? ImageUrl { get; private set; }

    public IReadOnlyCollection<Session> Sessions => _sessions.AsReadOnly();

    [ConcurrencyCheck] public long Version { get; set; }

    [NotMapped] IStronglyTypedId IAggregateRoot.Id => Id;

    public static ApplicationUser Create(
        string fullName,
        string phoneNumber,
        DateOnly birthDate,
        Uri? imageUrl = null)
    {
        var id = ApplicationUserId.New();
        var @event = new V1UserRegisteredDomainEvent(
            id,
            fullName.Trim(),
            phoneNumber,
            birthDate,
            imageUrl);

        var user = new ApplicationUser();
        user.RaiseEvent(@event);
        return user;
    }

    public void UpdateImageUrl(Uri imageUrl)
    {
        var @event = new V1UserImageUrlUpdatedDomainEvent(Id, imageUrl);
        RaiseEvent(@event);
    }

    // Intentionally does not follow the RaiseEvent-then-Apply replay pattern: events here are persisted
    // (AuditLog) and must never carry token hashes, so state is mutated directly and the event is a marker.

    /// <summary>
    ///     Issues a new refresh token for the (DeviceId, ClientId) session pair. Pass the <paramref name="existingSession" />
    ///     the caller already resolved for that pair (e.g. via a filtered EF <c>Include</c>) — <c>null</c> if this is its
    ///     first login, in which case a new session is created; otherwise it is reused/superseded (re-login on same device+app).
    /// </summary>
    public RefreshToken IssueSessionAndToken(
        Session? existingSession, Guid deviceId, string clientId, string? deviceName, string? ip, string? userAgent,
        byte[] refreshTokenHash, DateTimeOffset now, DateTimeOffset tokenExpiresAt,
        DateTimeOffset sessionAbsoluteExpiresAt)
    {
        Session session;
        if (existingSession is null)
        {
            session = Session.Create(Id, deviceId, clientId, deviceName, ip, userAgent, now, sessionAbsoluteExpiresAt);
            _sessions.Add(session);
            RaiseEvent(new V1SessionCreatedDomainEvent(Id, session.Id, deviceId, clientId, deviceName));
        }
        else
        {
            session = existingSession;

            // Same (DeviceId, ClientId) logging in again supersedes prior un-consumed tokens on that
            // session — otherwise "reuse the same session" would leave old tokens usable.
            session.SupersedeUnconsumedTokens(now);
            session.Reactivate(deviceName, ip, userAgent, now, sessionAbsoluteExpiresAt);
            RaiseEvent(new V1SessionRefreshedDomainEvent(Id, session.Id));
        }

        return session.IssueToken(refreshTokenHash, tokenExpiresAt);
    }

    /// <summary>
    ///     Rotates a refresh token. Pass the <paramref name="session" />/<paramref name="current" /> instances the
    ///     caller already resolved (e.g. via a filtered EF <c>Include</c>). Caller must have already verified the
    ///     session is not revoked and <paramref name="current" /> is not already consumed.
    /// </summary>
    public RefreshToken RotateRefreshToken(
        Session session, RefreshToken current, byte[] newHash, string? ip, string? userAgent,
        DateTimeOffset now, DateTimeOffset newExpiresAt)
    {
        var newToken = session.IssueToken(newHash, newExpiresAt);
        current.Consume(now, newToken.Id);
        session.Touch(ip, userAgent, now);

        RaiseEvent(new V1SessionRefreshedDomainEvent(Id, session.Id));
        return newToken;
    }

    /// <summary>
    ///     Revokes a session. Pass the <paramref name="session" /> instance the caller already resolved
    ///     (e.g. via a filtered EF <c>Include</c>).
    /// </summary>
    public void RevokeSession(Session session, SessionRevokedReason reason, DateTimeOffset now)
    {
        session.Revoke(reason, now);

        RaiseEvent(new V1SessionRevokedDomainEvent(Id, session.Id, reason));
    }

    public void RevokeAllSessions(SessionRevokedReason reason, DateTimeOffset now)
    {
        var activeSessions = _sessions.Where(s => s.RevokedAt is null).ToList();
        if (activeSessions.Count == 0)
        {
            return;
        }

        foreach (var session in activeSessions)
        {
            session.Revoke(reason, now);
        }

        RaiseEvent(new V1AllSessionsRevokedDomainEvent(Id, reason));
    }

    private void ApplyEvent(IEvent @event)
    {
        switch (@event)
        {
            case V1UserRegisteredDomainEvent e:
                Apply(e);
                break;
            case V1UserImageUrlUpdatedDomainEvent e:
                Apply(e);
                break;
            case V1RefreshTokenUpdatedDomainEvent e:
                Apply(e);
                break;
            case V1RefreshTokenRevokedDomainEvent e:
                Apply(e);
                break;
            case V1SessionCreatedDomainEvent e:
                Apply(e);
                break;
            case V1SessionRefreshedDomainEvent e:
                Apply(e);
                break;
            case V1SessionRevokedDomainEvent e:
                Apply(e);
                break;
            case V1AllSessionsRevokedDomainEvent e:
                Apply(e);
                break;
            default:
                throw new InvalidOperationException($"Unknown event {@event.GetType().Name}");
        }
    }

    private void Apply(V1UserRegisteredDomainEvent @event)
    {
        Id = @event.UserId;
        FullName = @event.FullName;
        PhoneNumber = @event.PhoneNumber;
        UserName = @event.PhoneNumber.TrimStart('+'); // UserName must be digits-only per AllowedUserNameCharacters
        BirthDate = @event.BirthDate;
    }

    private void Apply(V1UserImageUrlUpdatedDomainEvent @event)
    {
        ImageUrl = @event.ImageUrl;
    }

#pragma warning disable CA1822, S1186, IDE0060
    private void Apply(V1RefreshTokenUpdatedDomainEvent @event)
    {
        // Nothing to do here — frozen no-op, kept only so historical AuditLog rows still replay (see versioning rule).
    }

    private void Apply(V1RefreshTokenRevokedDomainEvent @event)
    {
        // Nothing to do here — frozen no-op, kept only so historical AuditLog rows still replay (see versioning rule).
    }

    private void Apply(V1SessionCreatedDomainEvent @event)
    {
        // Nothing to do here — Session is added directly in IssueSessionAndToken(); see explanation above.
    }

    private void Apply(V1SessionRefreshedDomainEvent @event)
    {
        // Nothing to do here — Session/RefreshToken state mutated directly in IssueSessionAndToken()/RotateRefreshToken().
    }

    private void Apply(V1SessionRevokedDomainEvent @event)
    {
        // Nothing to do here — Session state mutated directly in RevokeSession().
    }

    private void Apply(V1AllSessionsRevokedDomainEvent @event)
    {
        // Nothing to do here — Session state mutated directly in RevokeAllSessions().
    }
#pragma warning restore CA1822, S1186, IDE0060

    // EF Core and ASP.NET Identity materialise entities via the inherited parameterless constructor
    // from IdentityUser<ApplicationUserId>. No explicit constructor needed here.
#pragma warning restore CA1819 // Properties should not return arrays
}
