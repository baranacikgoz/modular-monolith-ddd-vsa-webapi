using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;

namespace Common.InterModuleRequests.Notifications;

/// <summary>Device metadata for the user's active registrations, keyed by Keycloak session id. Used to enrich the session list.</summary>
public sealed record GetDeviceSessionsRequest(ApplicationUserId UserId) : IInterModuleRequest<GetDeviceSessionsResponse>;

public sealed record DeviceSession(string SessionId, string ClientId, string? DeviceName);

public sealed record GetDeviceSessionsResponse(IReadOnlyList<DeviceSession> Sessions);
