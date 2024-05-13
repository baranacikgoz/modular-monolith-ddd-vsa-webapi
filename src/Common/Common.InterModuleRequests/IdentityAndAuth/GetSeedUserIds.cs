using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;

namespace Common.InterModuleRequests.IdentityAndAuth;

public sealed record GetSeedUserIdsRequest(int Count) : IInterModuleRequest<GetSeedUserIdsResponse>;
public sealed record GetSeedUserIdsResponse(ICollection<ApplicationUserId> UserIds);
