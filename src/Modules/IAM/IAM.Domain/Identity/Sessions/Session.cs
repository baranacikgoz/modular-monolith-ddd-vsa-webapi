using Common.Domain.Entities;
using Common.Domain.StronglyTypedIds;

namespace IAM.Domain.Identity.Sessions;

public readonly record struct SessionId(DefaultIdType Value) : IStronglyTypedId
{
    public static SessionId New()
    {
        return new SessionId(DefaultIdType.CreateVersion7());
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static bool TryParse(string str, out SessionId id)
    {
        return StronglyTypedIdHelper.TryDeserialize(str, out id);
    }
}

public sealed class Session : AuditableEntity<SessionId>
{
    private readonly List<RefreshToken> _refreshTokens = [];

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.
    private Session() : base(new SessionId(DefaultIdType.Empty))
    {
    } // EF Core needs a parameterless ctor
#pragma warning restore CS8618

    public ApplicationUserId UserId { get; private init; }
    public Guid DeviceId { get; private init; }
    public string ClientId { get; private init; } = string.Empty;
    public string? DeviceName { get; private set; }
    public DateTimeOffset LastUsedAt { get; private set; }
    public string? LastIp { get; private set; }
    public string? LastUserAgent { get; private set; }
    public DateTimeOffset AbsoluteExpiresAt { get; private set; }
    public DateTimeOffset? RevokedAt { get; private set; }
    public SessionRevokedReason? RevokedReason { get; private set; }

    public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

    internal static Session Create(
        ApplicationUserId userId, Guid deviceId, string clientId, string? deviceName,
        string? ip, string? userAgent, DateTimeOffset now, DateTimeOffset absoluteExpiresAt)
    {
        return new Session
        {
            Id = SessionId.New(),
            UserId = userId,
            DeviceId = deviceId,
            ClientId = clientId,
            DeviceName = deviceName,
            LastUsedAt = now,
            LastIp = ip,
            LastUserAgent = userAgent,
            AbsoluteExpiresAt = absoluteExpiresAt
        };
    }

    internal RefreshToken IssueToken(byte[] tokenHash, DateTimeOffset expiresAt)
    {
        var token = RefreshToken.Create(Id, tokenHash, expiresAt);
        _refreshTokens.Add(token);
        return token;
    }

    internal void Reactivate(string? deviceName, string? ip, string? userAgent, DateTimeOffset now, DateTimeOffset absoluteExpiresAt)
    {
        RevokedAt = null;
        RevokedReason = null;

        // Only overwrite the friendly label when a new one is actually supplied — a re-login/refresh
        // that omits DeviceName must not silently wipe a name the user set on a prior login.
        if (!string.IsNullOrWhiteSpace(deviceName))
        {
            DeviceName = deviceName;
        }

        AbsoluteExpiresAt = absoluteExpiresAt;
        Touch(ip, userAgent, now);
    }

    internal void SupersedeUnconsumedTokens(DateTimeOffset now)
    {
        foreach (var token in _refreshTokens.Where(t => t.ConsumedAt is null))
        {
            token.Consume(now);
        }
    }

    internal void Touch(string? ip, string? userAgent, DateTimeOffset now)
    {
        LastUsedAt = now;
        LastIp = ip;
        LastUserAgent = userAgent;
    }

    internal void Revoke(SessionRevokedReason reason, DateTimeOffset now)
    {
        RevokedAt = now;
        RevokedReason = reason;
    }
}
