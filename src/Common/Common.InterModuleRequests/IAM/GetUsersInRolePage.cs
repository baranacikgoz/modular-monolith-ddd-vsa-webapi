using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;

namespace Common.InterModuleRequests.IAM;

/// <summary>
///     One page of the enabled users holding a realm role, for a module that keeps a local projection of them and heals it
///     on a schedule (a reconciliation pager, like GetStoresPage), never for a per-request lookup.
///     <see cref="Skip" /> and <see cref="Take" /> page over Keycloak's own role-member order.
/// </summary>
public sealed record GetUsersInRolePageRequest(string RoleName, int Skip, int Take) : IInterModuleRequest<GetUsersInRolePageResponse>;

/// <summary><see cref="HasMore" /> is true when the role has members past this page.</summary>
public sealed record GetUsersInRolePageResponse(ICollection<RoleUserSummary> Users, bool HasMore);

public sealed record RoleUserSummary(ApplicationUserId Id, string? Email);
