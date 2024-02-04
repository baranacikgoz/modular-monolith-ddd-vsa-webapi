using Common.EventBus.Contracts;

namespace Common.InterModuleRequests.IdentityAndAuth;

public sealed record FirstBasicUserIdRequest;
public sealed record FirstBasicUserIdResponse(Guid UserId);
