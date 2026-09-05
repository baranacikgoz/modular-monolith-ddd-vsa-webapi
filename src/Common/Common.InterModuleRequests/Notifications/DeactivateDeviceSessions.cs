using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;

namespace Common.InterModuleRequests.Notifications;

/// <summary>Marks device registrations inactive after their Keycloak sessions were revoked. Null <paramref name="SessionIds" /> = all of the user's sessions.</summary>
public sealed record DeactivateDeviceSessionsRequest(
    ApplicationUserId UserId,
    IReadOnlyList<string>? SessionIds
) : IInterModuleRequest<DeactivateDeviceSessionsResponse>;

public sealed record DeactivateDeviceSessionsResponse;
