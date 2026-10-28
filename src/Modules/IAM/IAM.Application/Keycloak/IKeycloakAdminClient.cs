using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;

namespace IAM.Application.Keycloak;

/// <summary>Keycloak Admin REST API, authenticated with the resource client's service account.</summary>
public interface IKeycloakAdminClient
{
    Task<Result<ApplicationUserId>> CreateUserAsync(CreateKeycloakUser user, CancellationToken cancellationToken);

    /// <summary>Assigns a realm role. An already-held role is accepted. A user Keycloak does not know is <c>NotFound</c>; any other rejection is <c>IdentityProviderUnavailable</c>.</summary>
    Task<Result> AssignRealmRoleAsync(ApplicationUserId userId, string roleName, CancellationToken cancellationToken);

    /// <summary>
    ///     Changes a user's names and attributes. Keycloak's update replaces what the realm's user profile manages, so the
    ///     current representation is read, the change applied and the whole of it written back: a field the call does not
    ///     name keeps its value. A user Keycloak does not know is <c>NotFound</c>; a field the user profile rejects is
    ///     <c>IdentityProviderRejectedUserField</c>. The cached display entry of the user is dropped.
    /// </summary>
    Task<Result> UpdateUserAsync(ApplicationUserId userId, UpdateKeycloakUser update, CancellationToken cancellationToken);

    /// <summary>Deletes the user. A user that no longer exists is not an error.</summary>
    Task DeleteUserAsync(ApplicationUserId userId, CancellationToken cancellationToken);

    Task<Result<KeycloakUser>> GetUserAsync(ApplicationUserId userId, CancellationToken cancellationToken);

    Task<KeycloakUser?> FindUserByUsernameAsync(string username, CancellationToken cancellationToken);

    /// <summary><paramref name="email" /> must already be lowercased by the caller (Keycloak stores emails lowercased).</summary>
    Task<KeycloakUser?> FindUserByEmailAsync(string email, CancellationToken cancellationToken);

    /// <summary>Substring search across username, first name, last name and email; service accounts excluded by Keycloak.</summary>
    Task<KeycloakUserPage> SearchUsersAsync(string? searchTerm, int skip, int take, CancellationToken cancellationToken);

    Task<IReadOnlyList<ApplicationUserId>> GetUserIdsInRoleAsync(string roleName, int max, CancellationToken cancellationToken);

    /// <summary>One page of the users holding a realm role directly (<paramref name="skip" />, <paramref name="take" />), enabled or not, in Keycloak's own order. Users who hold only a composite role that includes it are not listed: see <see cref="GetRolesContainingAsync" />. An unknown role is an empty page.</summary>
    Task<IReadOnlyList<KeycloakUser>> GetUsersInRoleAsync(string roleName, int skip, int take, CancellationToken cancellationToken);

    /// <summary>
    ///     Names of the realm roles that include <paramref name="roleName" />, directly or through other composite roles. The
    ///     role itself and client roles are not listed. Keycloak has no reverse lookup, so this reads the children of every
    ///     composite realm role, and keeps that role graph for <c>KeycloakOptions.RoleGraphCacheSeconds</c>: a composite role
    ///     changed in Keycloak shows up after that window. An unknown role, or one no composite role includes, gives an empty list.
    /// </summary>
    Task<IReadOnlyList<string>> GetRolesContainingAsync(string roleName, CancellationToken cancellationToken);

    /// <summary>
    ///     Every user who holds one of <paramref name="roleNames" /> through a group: members of the groups that carry the role
    ///     and of all their subgroups (a subgroup inherits its parent's role mappings), deduplicated, enabled or not. Users who
    ///     hold the role directly are not listed: see <see cref="GetUsersInRoleAsync" />. Keycloak is read in pages of
    ///     <paramref name="pageSize" />. An unknown role contributes nothing.
    /// </summary>
    Task<IReadOnlyList<KeycloakUser>> GetUsersInGroupsHoldingRolesAsync(IReadOnlyCollection<string> roleNames, int pageSize, CancellationToken cancellationToken);

    Task<IReadOnlyList<KeycloakUserSession>> GetUserSessionsAsync(ApplicationUserId userId, CancellationToken cancellationToken);

    /// <summary>Revokes one session. A session that no longer exists is not an error.</summary>
    Task DeleteSessionAsync(string sessionId, CancellationToken cancellationToken);

    /// <summary>Revokes every session of the user.</summary>
    Task LogoutUserAsync(ApplicationUserId userId, CancellationToken cancellationToken);
}
