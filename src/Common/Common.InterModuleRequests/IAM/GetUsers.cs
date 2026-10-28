using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;

namespace Common.InterModuleRequests.IAM;

/// <summary>Bulk lookup by user ids (audit "who" columns, and the contact data of users a module has to message). Ids the identity provider does not know are left out of the response.</summary>
public sealed record GetUsersRequest(ICollection<ApplicationUserId> UserIds) : IInterModuleRequest<GetUsersResponse>;

public sealed record GetUsersResponse(ICollection<UserSummary> Users);

public sealed record UserSummary(ApplicationUserId Id, string? Email, string? FirstName, string? LastName, string? PhoneNumber);
