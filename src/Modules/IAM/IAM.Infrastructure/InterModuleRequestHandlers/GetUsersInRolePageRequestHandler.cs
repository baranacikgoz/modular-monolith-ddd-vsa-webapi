using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using IAM.Application.Keycloak;

namespace IAM.Infrastructure.InterModuleRequestHandlers;

/// <summary>
///     Reads one page of a realm role's effective members from Keycloak for a projection's scheduled reconciliation: the
///     users who hold the role directly and the users who hold a realm role that includes it, directly or through other
///     composite roles (a user holding only <c>system-admin</c> is a member of <c>staff</c>). Disabled users are left out:
///     they cannot log in, so a module addressing "everyone in this role" must not address them.
///     <para>
///         The members are one deduplicated sequence in a fixed order: first the users holding any containing role, sorted
///         by user id, then the users holding only the role itself, in Keycloak's own order. Keycloak cannot list members
///         through composites, so the containing roles' members are read in full (those roles are administrative and
///         small), while the role's own members are read only as far as the page needs.
///     </para>
/// </summary>
public class GetUsersInRolePageRequestHandler(IKeycloakAdminClient adminClient)
    : InterModuleRequestHandler<GetUsersInRolePageRequest, GetUsersInRolePageResponse>
{
    public override async Task<GetUsersInRolePageResponse> HandleAsync(
        GetUsersInRolePageRequest request, CancellationToken cancellationToken)
    {
        // One extra row tells whether another page exists without a count call.
        var members = await ReadEffectiveMembersAsync(request.RoleName, request.Skip, request.Take + 1, cancellationToken);
        var hasMore = members.Count > request.Take;

        var users = members
            .Take(request.Take)
            .Where(u => u.Enabled)
            .Select(u => new RoleUserSummary(u.Id, u.Email))
            .ToList();

        return new GetUsersInRolePageResponse(users, hasMore);
    }

    private async Task<IReadOnlyList<KeycloakUser>> ReadEffectiveMembersAsync(
        string roleName, int skip, int take, CancellationToken cancellationToken)
    {
        var containingRoles = await adminClient.GetRolesContainingAsync(roleName, cancellationToken);
        if (containingRoles.Count == 0)
        {
            return await adminClient.GetUsersInRoleAsync(roleName, skip, take, cancellationToken);
        }

        var inherited = await ReadEveryMemberAsync(containingRoles, take, cancellationToken);
        var page = inherited.Values
            .OrderBy(u => u.Id.Value)
            .Skip(skip)
            .Take(take)
            .ToList();

        // Direct holders that a containing role already lists were placed above, so they are skipped here.
        var toSkip = Math.Max(0, skip - inherited.Count);
        var offset = 0;
        while (page.Count < take)
        {
            var batch = await adminClient.GetUsersInRoleAsync(roleName, offset, take, cancellationToken);
            offset += batch.Count;

            foreach (var user in batch.Where(u => !inherited.ContainsKey(u.Id)))
            {
                if (toSkip > 0)
                {
                    toSkip--;
                }
                else if (page.Count < take)
                {
                    page.Add(user);
                }
            }

            if (batch.Count < take)
            {
                break;
            }
        }

        return page;
    }

    private async Task<Dictionary<ApplicationUserId, KeycloakUser>> ReadEveryMemberAsync(
        IReadOnlyList<string> roleNames, int batchSize, CancellationToken cancellationToken)
    {
        var members = new Dictionary<ApplicationUserId, KeycloakUser>();

        foreach (var roleName in roleNames)
        {
            var offset = 0;
            IReadOnlyList<KeycloakUser> batch;
            do
            {
                batch = await adminClient.GetUsersInRoleAsync(roleName, offset, batchSize, cancellationToken);
                offset += batch.Count;

                foreach (var user in batch)
                {
                    members.TryAdd(user.Id, user);
                }
            }
            while (batch.Count == batchSize);
        }

        return members;
    }
}
