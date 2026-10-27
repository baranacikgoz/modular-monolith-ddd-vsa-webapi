using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;

namespace Common.InterModuleRequests.IAM;

/// <summary>Bulk display lookup by user ids (audit "who" columns). Ids the identity provider does not know are left out of the response.</summary>
public sealed record GetUsersRequest(ICollection<ApplicationUserId> UserIds) : IInterModuleRequest<GetUsersResponse>;

public sealed record GetUsersResponse(ICollection<UserSummary> Users);

public sealed record UserSummary(ApplicationUserId Id, string? Email, string? FirstName, string? LastName);
