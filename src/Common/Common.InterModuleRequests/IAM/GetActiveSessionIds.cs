using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;

namespace Common.InterModuleRequests.IAM;

/// <summary>Live Keycloak session ids for a batch of users, used to reconcile Notifications' device registry
/// against sessions Keycloak revoked on its own (idle expiry, admin console, reuse detection).</summary>
public sealed record GetActiveSessionIdsRequest(
    IReadOnlyList<ApplicationUserId> UserIds
) : IInterModuleRequest<GetActiveSessionIdsResponse>;

public sealed record UserSessionIds(ApplicationUserId UserId, IReadOnlyList<string> SessionIds);

public sealed record GetActiveSessionIdsResponse(IReadOnlyList<UserSessionIds> Users);
