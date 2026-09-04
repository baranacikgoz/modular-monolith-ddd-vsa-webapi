using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;
using IAM.Domain.Identity.Sessions;

namespace IAM.Domain.Identity.DomainEvents.v1;

public sealed record V1SessionRevokedDomainEvent(
    ApplicationUserId UserId,
    SessionId SessionId,
    V1SessionRevokedDomainEvent.ReasonSnapshot Reason
) : DomainEvent
{
    public enum ReasonSnapshot
    {
        UserSignedOut,
        SignedOutEverywhere,
        TokenReuseDetected,
        Expired
    }
}

internal static class V1SessionRevokedDomainEventExtensions
{
    public static V1SessionRevokedDomainEvent.ReasonSnapshot ToRevokedSnapshot(this SessionRevokedReason reason)
    {
        return reason switch
        {
            SessionRevokedReason.UserSignedOut => V1SessionRevokedDomainEvent.ReasonSnapshot.UserSignedOut,
            SessionRevokedReason.SignedOutEverywhere => V1SessionRevokedDomainEvent.ReasonSnapshot.SignedOutEverywhere,
            SessionRevokedReason.TokenReuseDetected => V1SessionRevokedDomainEvent.ReasonSnapshot.TokenReuseDetected,
            SessionRevokedReason.Expired => V1SessionRevokedDomainEvent.ReasonSnapshot.Expired,
            _ => throw new ArgumentOutOfRangeException(nameof(reason), reason, null)
        };
    }
}
