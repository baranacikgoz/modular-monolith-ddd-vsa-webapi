using Common.Domain.Entities;
using Common.Domain.StronglyTypedIds;

namespace IAM.Domain.Identity.Sessions;

public readonly record struct RefreshTokenId(DefaultIdType Value) : IStronglyTypedId
{
    public static RefreshTokenId New()
    {
        return new RefreshTokenId(DefaultIdType.CreateVersion7());
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static bool TryParse(string str, out RefreshTokenId id)
    {
        return StronglyTypedIdHelper.TryDeserialize(str, out id);
    }
}

#pragma warning disable CA1819 // Properties should not return arrays
public sealed class RefreshToken : AuditableEntity<RefreshTokenId>
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.
    private RefreshToken() : base(new RefreshTokenId(DefaultIdType.Empty))
    {
    } // EF Core needs a parameterless ctor
#pragma warning restore CS8618

    public SessionId SessionId { get; private init; }
    public byte[] TokenHash { get; private init; } = [];
    public DateTimeOffset ExpiresAt { get; private init; }
    public DateTimeOffset? ConsumedAt { get; private set; }
    public RefreshTokenId? ReplacedByTokenId { get; private set; }

    internal static RefreshToken Create(SessionId sessionId, byte[] tokenHash, DateTimeOffset expiresAt)
    {
        return new RefreshToken
        {
            Id = RefreshTokenId.New(),
            SessionId = sessionId,
            TokenHash = tokenHash,
            ExpiresAt = expiresAt
        };
    }

    internal void Consume(DateTimeOffset now, RefreshTokenId? replacedByTokenId = null)
    {
        ConsumedAt = now;
        ReplacedByTokenId = replacedByTokenId;
    }
}
#pragma warning restore CA1819
