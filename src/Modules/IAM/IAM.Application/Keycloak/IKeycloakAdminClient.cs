using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;

namespace IAM.Application.Keycloak;

/// <summary>Keycloak Admin REST API, authenticated with the resource client's service account.</summary>
public interface IKeycloakAdminClient
{
    Task<Result<ApplicationUserId>> CreateUserAsync(CreateKeycloakUser user, CancellationToken cancellationToken);

    Task AssignRealmRoleAsync(ApplicationUserId userId, string roleName, CancellationToken cancellationToken);

    Task<Result<KeycloakUser>> GetUserAsync(ApplicationUserId userId, CancellationToken cancellationToken);

    Task<KeycloakUser?> FindUserByUsernameAsync(string username, CancellationToken cancellationToken);

    /// <summary>Substring search across username, first name, last name and email; service accounts excluded by Keycloak.</summary>
    Task<KeycloakUserPage> SearchUsersAsync(string? searchTerm, int skip, int take, CancellationToken cancellationToken);

    Task<IReadOnlyList<ApplicationUserId>> GetUserIdsInRoleAsync(string roleName, int max, CancellationToken cancellationToken);

    Task<IReadOnlyList<KeycloakUserSession>> GetUserSessionsAsync(ApplicationUserId userId, CancellationToken cancellationToken);

    /// <summary>Revokes one session. A session that no longer exists is not an error.</summary>
    Task DeleteSessionAsync(string sessionId, CancellationToken cancellationToken);

    /// <summary>Revokes every session of the user.</summary>
    Task LogoutUserAsync(ApplicationUserId userId, CancellationToken cancellationToken);
}
