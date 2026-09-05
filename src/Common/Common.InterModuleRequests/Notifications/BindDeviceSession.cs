using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;

namespace Common.InterModuleRequests.Notifications;

/// <summary>
///     Binds a freshly issued Keycloak session to the (device, client app) pair that signed in, replacing whatever
///     session that pair held before. Sent by IAM right after a login.
/// </summary>
public sealed record BindDeviceSessionRequest(
    ApplicationUserId UserId,
    string SessionId,
    Guid DeviceId,
    string ClientId,
    string? DeviceName,
    string? PushToken
) : IInterModuleRequest<BindDeviceSessionResponse>;

/// <param name="SupersededSessionId">The Keycloak session the same device held before this login, if any. IAM revokes it.</param>
public sealed record BindDeviceSessionResponse(string? SupersededSessionId);
