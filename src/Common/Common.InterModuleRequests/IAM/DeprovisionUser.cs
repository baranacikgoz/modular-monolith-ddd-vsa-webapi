using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;

namespace Common.InterModuleRequests.IAM;

/// <summary>Compensation for a registration whose local write failed after the identity-provider user was created. A user that no longer exists is not an error.</summary>
public sealed record DeprovisionUserRequest(ApplicationUserId UserId) : IInterModuleRequest<DeprovisionUserResponse>;

public sealed record DeprovisionUserResponse;
