using Common.Domain.StronglyTypedIds;

namespace Common.IntegrationEvents;

/// <summary>
///     A condition that needs a human right now. Addressed by realm role name: Notifications pushes it to every
///     connected user holding <paramref name="RoleName" /> and emails the projected recipients of that role.
/// </summary>
public sealed record CriticalAlertRaisedIntegrationEvent(string RoleName, string Title, string Message) : IntegrationEvent;

/// <summary>
///     A sign-in bound a freshly issued session to the (device, client app) pair that signed in. Raised on every
///     sign-in, the first on a device and every later one. The push token is deliberately not part of it. A consumer
///     compares <c>CreatedOn</c> with its own timestamps to tell whether a sign-in happened before or after a fact.
/// </summary>
public sealed record UserSignedInIntegrationEvent(
    ApplicationUserId UserId,
    string SessionId,
    Guid DeviceId,
    string ClientId
) : IntegrationEvent;
