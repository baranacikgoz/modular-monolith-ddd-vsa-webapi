using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;
using IAM.Domain.Identity.Sessions;

namespace IAM.Domain.Identity.DomainEvents.v1;

public sealed record V1AllSessionsRevokedDomainEvent(
    ApplicationUserId UserId,
    V1AllSessionsRevokedDomainEvent.ReasonSnapshot Reason
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

internal static class V1AllSessionsRevokedDomainEventExtensions
{
    public static V1AllSessionsRevokedDomainEvent.ReasonSnapshot ToAllRevokedSnapshot(this SessionRevokedReason reason)
    {
        return reason switch
        {
            SessionRevokedReason.UserSignedOut => V1AllSessionsRevokedDomainEvent.ReasonSnapshot.UserSignedOut,
            SessionRevokedReason.SignedOutEverywhere => V1AllSessionsRevokedDomainEvent.ReasonSnapshot.SignedOutEverywhere,
            SessionRevokedReason.TokenReuseDetected => V1AllSessionsRevokedDomainEvent.ReasonSnapshot.TokenReuseDetected,
            SessionRevokedReason.Expired => V1AllSessionsRevokedDomainEvent.ReasonSnapshot.Expired,
            _ => throw new ArgumentOutOfRangeException(nameof(reason), reason, null)
        };
    }
}
