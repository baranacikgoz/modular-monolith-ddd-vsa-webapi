using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using IAM.Application.Keycloak;

namespace IAM.Infrastructure.InterModuleRequestHandlers;

/// <summary>
///     Reads one page of a realm role's members from Keycloak for a projection's scheduled reconciliation. Disabled users are
///     left out: they cannot log in, so a module addressing "everyone in this role" must not address them.
/// </summary>
public class GetUsersInRolePageRequestHandler(IKeycloakAdminClient adminClient)
    : InterModuleRequestHandler<GetUsersInRolePageRequest, GetUsersInRolePageResponse>
{
    public override async Task<GetUsersInRolePageResponse> HandleAsync(
        GetUsersInRolePageRequest request, CancellationToken cancellationToken)
    {
        // One extra row tells whether another page exists without a count call.
        var members = await adminClient.GetUsersInRoleAsync(request.RoleName, request.Skip, request.Take + 1, cancellationToken);
        var hasMore = members.Count > request.Take;

        var users = members
            .Take(request.Take)
            .Where(u => u.Enabled)
            .Select(u => new RoleUserSummary(u.Id, u.Email))
            .ToList();

        return new GetUsersInRolePageResponse(users, hasMore);
    }
}
