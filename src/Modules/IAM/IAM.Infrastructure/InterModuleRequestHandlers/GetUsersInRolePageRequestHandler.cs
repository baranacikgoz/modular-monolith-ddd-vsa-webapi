using Common.Application.Caching;
using Common.Application.Options;
using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using IAM.Application.Keycloak;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace IAM.Infrastructure.InterModuleRequestHandlers;

/// <summary>
///     Reads one page of a realm role's effective members from Keycloak for a projection's scheduled reconciliation: the
///     users who hold the role directly, the users who hold a realm role that includes it (directly or through other
///     composite roles: a user holding only <c>system-admin</c> is a member of <c>staff</c>), and the members of every group
///     that carries the role or such a containing role, subgroups included. Disabled users are left out: they cannot log in,
///     so a module addressing "everyone in this role" must not address them.
///     <para>
///         The members are one deduplicated sequence in a fixed order: first the users who hold the role only indirectly
///         (through a containing role or a group), sorted by user id, then the users holding only the role itself, in
///         Keycloak's own order. Keycloak cannot list members through composites or groups in one call, and a user is placed
///         by whether anyone else lists them, so the sequence cannot be paged natively.
///     </para>
///     <para>
///         A role with no indirect holders is paged straight through Keycloak. A role with indirect holders is built once
///         into a list held in memory for <see cref="KeycloakOptions.RoleGraphCacheSeconds" /> (key
///         <see cref="CacheKeys.For.KeycloakRoleMembers" />), and every page of a walk is a slice of that list, so the cost of
///         a walk grows with the role's size and not with its size times its page count. Nothing evicts the list but its
///         expiry: a member added meanwhile appears in the next window.
///     </para>
/// </summary>
public class GetUsersInRolePageRequestHandler(
    IKeycloakAdminClient adminClient,
    IFusionCache cache,
    IOptions<KeycloakOptions> keycloakOptions)
    : InterModuleRequestHandler<GetUsersInRolePageRequest, GetUsersInRolePageResponse>
{
    public override async Task<GetUsersInRolePageResponse> HandleAsync(
        GetUsersInRolePageRequest request, CancellationToken cancellationToken)
    {
        // One extra row tells whether another page exists without a count call.
        var members = await ReadMembersAsync(request.RoleName, request.Skip, request.Take + 1, cancellationToken);
        var hasMore = members.Count > request.Take;

        var users = members
            .Take(request.Take)
            .Where(m => m.Enabled)
            .Select(m => new RoleUserSummary(m.Id, m.Email))
            .ToList();

        return new GetUsersInRolePageResponse(users, hasMore);
    }

    private async Task<IReadOnlyList<RoleMember>> ReadMembersAsync(
        string roleName, int skip, int take, CancellationToken cancellationToken)
    {
        var snapshot = await GetSnapshotAsync(roleName, take, cancellationToken);
        if (!snapshot.HasIndirectHolders)
        {
            var direct = await adminClient.GetUsersInRoleAsync(roleName, skip, take, cancellationToken);
            return direct.Select(ToMember).ToList();
        }

        return snapshot.Members.Skip(skip).Take(take).ToList();
    }

    private ValueTask<RoleMembersSnapshot> GetSnapshotAsync(string roleName, int batchSize, CancellationToken cancellationToken)
    {
        // A fresh options instance on purpose (same reason as the role graph read): the configured defaults carry factory
        // timeouts that would turn a slow Keycloak into a synthetic timeout mid-walk, and fail-safe would serve an old list for
        // a day while Keycloak is down. Memory only: the list can be as large as the role and is cheap to rebuild.
        var entryOptions = new FusionCacheEntryOptions
        {
            Duration = TimeSpan.FromSeconds(keycloakOptions.Value.RoleGraphCacheSeconds),
            SkipDistributedCacheRead = true,
            SkipDistributedCacheWrite = true,
            SkipBackplaneNotifications = true
        };

        return cache.GetOrSetAsync<RoleMembersSnapshot>(
            CacheKeys.For.KeycloakRoleMembers(roleName),
            (_, ct) => BuildSnapshotAsync(roleName, batchSize, ct),
            options: entryOptions,
            token: cancellationToken);
    }

    private async Task<RoleMembersSnapshot> BuildSnapshotAsync(string roleName, int batchSize, CancellationToken cancellationToken)
    {
        var containingRoles = await adminClient.GetRolesContainingAsync(roleName, cancellationToken);
        var indirect = await ReadEveryMemberAsync(containingRoles, batchSize, cancellationToken);
        var throughGroups = await adminClient.GetUsersInGroupsHoldingRolesAsync([roleName, .. containingRoles], batchSize, cancellationToken);
        foreach (var user in throughGroups)
        {
            indirect.TryAdd(user.Id, user);
        }

        if (indirect.Count == 0)
        {
            return RoleMembersSnapshot.PassThrough;
        }

        var members = indirect.Values
            .OrderBy(u => u.Id.Value)
            .Select(ToMember)
            .ToList();

        // Direct holders that a containing role or a group already lists were placed above, so they are skipped here.
        var listed = indirect.Keys.ToHashSet();
        var offset = 0;
        IReadOnlyList<KeycloakUser> batch;
        do
        {
            batch = await adminClient.GetUsersInRoleAsync(roleName, offset, batchSize, cancellationToken);
            offset += batch.Count;
            members.AddRange(batch.Where(u => listed.Add(u.Id)).Select(ToMember));
        }
        while (batch.Count == batchSize);

        return new RoleMembersSnapshot(HasIndirectHolders: true, members);
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

    private static RoleMember ToMember(KeycloakUser user)
    {
        return new RoleMember(user.Id, user.Email, user.Enabled);
    }

    /// <summary>The three fields a page needs of a member; <see cref="Enabled" /> is kept so a disabled user still takes its place in the sequence.</summary>
    private sealed record RoleMember(ApplicationUserId Id, string? Email, bool Enabled);

    /// <summary>
    ///     What one role's members look like for a window: either "nobody holds the role indirectly, ask Keycloak for the page"
    ///     or the whole sequence. The cached verdict is why a role without indirect holders costs one read per page too.
    /// </summary>
    private sealed record RoleMembersSnapshot(bool HasIndirectHolders, IReadOnlyList<RoleMember> Members)
    {
        public static readonly RoleMembersSnapshot PassThrough = new(HasIndirectHolders: false, []);
    }
}
