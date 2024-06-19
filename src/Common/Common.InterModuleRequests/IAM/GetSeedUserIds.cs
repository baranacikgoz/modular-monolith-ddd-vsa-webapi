using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;

namespace Common.InterModuleRequests.IAM;

public sealed record GetSeedUserIdsRequest(int Count) : IInterModuleRequest<GetSeedUserIdsResponse>;
public sealed record GetSeedUserIdsResponse(ICollection<ApplicationUserId> UserIds);
public sealed record HasManageHangfireDashboardPermissonRequest(ApplicationUserId UserId) : IInterModuleRequest<HasManageHangfireDashboardPermissonResponse>;
public sealed record HasManageHangfireDashboardPermissonResponse(bool HasPermission);
