using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;

namespace Common.InterModuleRequests.Notifications;

public enum SecurityAlertType
{
    /// <summary>A rotated-away refresh token was replayed; the whole session was revoked as a precaution.</summary>
    SessionRevokedTokenReuse
}

/// <summary>Best-effort security notification: a SignalR push to any connected client plus an FCM push to every
/// active device with a token. The caller (IAM) has no database of its own, so this is fire-and-forget from its
/// side; failures here must never fail the caller's request.</summary>
public sealed record SendSecurityAlertRequest(
    ApplicationUserId UserId,
    SecurityAlertType Type
) : IInterModuleRequest<SendSecurityAlertResponse>;

public sealed record SendSecurityAlertResponse;
