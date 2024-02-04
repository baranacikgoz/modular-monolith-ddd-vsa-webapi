using Common.EventBus.Contracts;

namespace Common.InterModuleRequests.IdentityAndAuth;

public sealed record FirstBasicUserIdsRequest(int Count);
public sealed record FirstBasicUserIdsResponse(ICollection<Guid> UserIds);
