using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Common.Application.Caching;
using Common.Application.Options;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using IAM.Application.Keycloak;
using IAM.Domain.Errors;
using IAM.Infrastructure.Keycloak.Representations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace IAM.Infrastructure.Keycloak;

/// <summary>
///     Keycloak Admin REST API. Session revocation here (<see cref="DeleteSessionAsync" />,
///     <see cref="LogoutUserAsync" />) also purges the cached authorization decisions tagged with that session or
///     user, so a revoked-but-unexpired access token stops authorizing on its next request instead of when the
///     decision cache entry expires. Every revocation path in the module goes through these two methods.
/// </summary>
internal sealed partial class KeycloakAdminClient(
    HttpClient httpClient,
    IServiceAccountTokenProvider tokenProvider,
    IOptions<KeycloakOptions> keycloakOptionsProvider,
    IOptions<IdentitySchemeOptions> identitySchemeOptionsProvider,
    IFusionCache cache,
    ILogger<KeycloakAdminClient> logger
) : IKeycloakAdminClient
{
    private const string UsersResource = "users";

    public async Task<Result<ApplicationUserId>> CreateUserAsync(CreateKeycloakUser user,
        CancellationToken cancellationToken)
    {
        var attributes = new Dictionary<string, List<string>>();
        if (user.PhoneNumber is { } phoneNumber)
        {
            attributes[UserAttributes.PhoneNumber] = [phoneNumber];
            attributes[UserAttributes.PhoneNumberVerified] = [user.PhoneNumberVerified ? "true" : "false"];
        }
        if (user.BirthDate is { } birthDate)
        {
            attributes[UserAttributes.BirthDate] = [birthDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)];
        }

        var representation = new UserRepresentation
        {
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            EmailVerified = user.Email is null ? null : user.EmailVerified,
            Enabled = true,
            Attributes = attributes,
            Credentials = user.Password is null
                ? null
                : [new CredentialRepresentation { Value = user.Password, Temporary = false }]
        };

        using var response = await SendAsync(
            () => new HttpRequestMessage(HttpMethod.Post, AdminUri(UsersResource))
            {
                Content = JsonContent.Create(representation)
            },
            cancellationToken);

        switch (response.StatusCode)
        {
            case HttpStatusCode.Created:
                var location = response.Headers.Location
                               ?? throw new HttpRequestException("Keycloak created the user without a Location header.");
                var idSegment = location.Segments[^1].TrimEnd('/');
                return DefaultIdType.TryParse(idSegment, out var id)
                    ? new ApplicationUserId(id)
                    : throw new HttpRequestException($"Keycloak returned a non-UUID user id '{idSegment}'.");
            case HttpStatusCode.Conflict:
                var conflict = await response.Content.TryReadFromJsonAsync<ErrorRepresentation>(cancellationToken);
                return MapConflict(conflict?.ErrorMessage);
            case HttpStatusCode.BadRequest:
                var error = await response.Content.TryReadFromJsonAsync<ErrorRepresentation>(cancellationToken);
                var rejection = error?.ErrorMessage ?? error?.ErrorDescription;
                LogUserRejected(logger, error?.Field, rejection);
                return IdentityErrors.IdentityProviderRejectedUserField(error?.Field, rejection);
            default:
                response.EnsureSuccessStatusCode();
                throw new HttpRequestException($"Unexpected Keycloak status {(int)response.StatusCode} creating a user.");
        }
    }

    /// <summary>
    ///     Keycloak's message text is the only way to tell which unique field collided; it returns no
    ///     machine-readable field on 409. Its messages are "User exists with same email", "User exists with
    ///     same username" and, from the create-race path, "User exists with same username or email". A
    ///     username collision means whatever the deployment provisions usernames from (phone or email), so
    ///     everything that is not explicitly the email field resolves through the identity scheme.
    /// </summary>
    private Error MapConflict(string? keycloakMessage)
    {
        if (keycloakMessage?.Contains("same email", StringComparison.OrdinalIgnoreCase) == true)
        {
            return IdentityErrors.EmailAlreadyRegistered;
        }

        return identitySchemeOptionsProvider.Value.Scheme == IdentityScheme.Email
            ? IdentityErrors.EmailAlreadyRegistered
            : IdentityErrors.PhoneNumberAlreadyRegistered;
    }

    public async Task<Result> AssignRealmRoleAsync(ApplicationUserId userId, string roleName, CancellationToken cancellationToken)
    {
        // The role itself is a realm deployment invariant (declared in realm-modular-monolith.json), not
        // caller input, so a missing role is a config error worth throwing on rather than a Result failure.
        using var roleResponse = await SendAsync(
            () => new HttpRequestMessage(HttpMethod.Get, AdminUri($"roles/{Uri.EscapeDataString(roleName)}")),
            cancellationToken);
        roleResponse.EnsureSuccessStatusCode();
        var role = await roleResponse.Content.ReadFromJsonAsync<RoleRepresentation>(cancellationToken)
                   ?? throw new HttpRequestException($"Keycloak returned no representation for role '{roleName}'.");

        using var mappingResponse = await SendAsync(
            () => new HttpRequestMessage(HttpMethod.Post, AdminUri($"{UsersResource}/{userId}/role-mappings/realm"))
            {
                Content = JsonContent.Create(new[] { role })
            },
            cancellationToken);

        if (mappingResponse.IsSuccessStatusCode)
        {
            return Result.Success;
        }

        LogRoleAssignmentFailed(logger, userId, roleName, (int)mappingResponse.StatusCode);

        // Keycloak answers 404 on the role-mapping resource of an id it does not know: the user is gone, not the
        // identity provider. Consumers distinguish the two (a missing user is skipped, an outage is retried).
        return mappingResponse.StatusCode == HttpStatusCode.NotFound
            ? Error.NotFound(nameof(KeycloakUser), userId)
            : IdentityErrors.IdentityProviderUnavailable;
    }

    public async Task<Result> UpdateUserAsync(ApplicationUserId userId, UpdateKeycloakUser update,
        CancellationToken cancellationToken)
    {
        using var read = await SendAsync(
            () => new HttpRequestMessage(HttpMethod.Get, AdminUri($"{UsersResource}/{userId}")),
            cancellationToken);

        if (read.StatusCode == HttpStatusCode.NotFound)
        {
            return Error.NotFound(nameof(KeycloakUser), userId);
        }

        read.EnsureSuccessStatusCode();
        var representation = await read.Content.ReadFromJsonAsync<UserRepresentation>(cancellationToken)
                             ?? throw new HttpRequestException("Keycloak returned an empty user representation.");

        if (update.FirstName is not null)
        {
            representation.FirstName = update.FirstName;
        }

        if (update.LastName is not null)
        {
            representation.LastName = update.LastName;
        }

        if (update.Attributes is { Count: > 0 })
        {
            representation.Attributes ??= [];
            foreach (var (name, value) in update.Attributes)
            {
                if (value is null)
                {
                    representation.Attributes.Remove(name);
                }
                else
                {
                    representation.Attributes[name] = [value];
                }
            }
        }

        using var write = await SendAsync(
            () => new HttpRequestMessage(HttpMethod.Put, AdminUri($"{UsersResource}/{userId}"))
            {
                Content = JsonContent.Create(representation)
            },
            cancellationToken);

        switch (write.StatusCode)
        {
            case HttpStatusCode.NoContent:
                // The bulk display lookup caches the name for up to a day; drop it so the change shows now.
                await cache.RemoveAsync(CacheKeys.For.UserDisplay(userId), token: cancellationToken);
                return Result.Success;
            case HttpStatusCode.NotFound:
                return Error.NotFound(nameof(KeycloakUser), userId);
            case HttpStatusCode.BadRequest:
                var error = await write.Content.TryReadFromJsonAsync<ErrorRepresentation>(cancellationToken);
                var rejection = error?.ErrorMessage ?? error?.ErrorDescription;
                LogUserRejected(logger, error?.Field, rejection);
                return IdentityErrors.IdentityProviderRejectedUserField(error?.Field, rejection);
            default:
                write.EnsureSuccessStatusCode();
                throw new HttpRequestException($"Unexpected Keycloak status {(int)write.StatusCode} updating a user.");
        }
    }

    public async Task DeleteUserAsync(ApplicationUserId userId, CancellationToken cancellationToken)
    {
        using var response = await SendAsync(
            () => new HttpRequestMessage(HttpMethod.Delete, AdminUri($"{UsersResource}/{userId}")),
            cancellationToken);

        if (response.StatusCode != HttpStatusCode.NotFound)
        {
            response.EnsureSuccessStatusCode();
        }

        // The bulk display lookup (GetUsersRequestHandler) caches this user's name for up to a day; drop it now so a
        // deleted user is not listed by name until the entry expires.
        await cache.RemoveAsync(CacheKeys.For.UserDisplay(userId), token: cancellationToken);
    }

    public async Task<Result<KeycloakUser>> GetUserAsync(ApplicationUserId userId, CancellationToken cancellationToken)
    {
        using var response = await SendAsync(
            () => new HttpRequestMessage(HttpMethod.Get, AdminUri($"{UsersResource}/{userId}")),
            cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return Error.NotFound(nameof(KeycloakUser), userId);
        }

        response.EnsureSuccessStatusCode();
        var user = await response.Content.ReadFromJsonAsync<UserRepresentation>(cancellationToken)
                   ?? throw new HttpRequestException("Keycloak returned an empty user representation.");

        return ToUser(user);
    }

    public async Task<KeycloakUser?> FindUserByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        using var response = await SendAsync(
            () => new HttpRequestMessage(HttpMethod.Get,
                AdminUri($"{UsersResource}?username={Uri.EscapeDataString(username)}&exact=true&max=1")),
            cancellationToken);
        response.EnsureSuccessStatusCode();

        var users = await response.Content.ReadFromJsonAsync<List<UserRepresentation>>(cancellationToken);
        var user = users?.FirstOrDefault();

        return user is null ? null : ToUser(user);
    }

    public async Task<KeycloakUser?> FindUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        using var response = await SendAsync(
            () => new HttpRequestMessage(HttpMethod.Get,
                AdminUri($"{UsersResource}?email={Uri.EscapeDataString(email)}&exact=true&max=1")),
            cancellationToken);
        response.EnsureSuccessStatusCode();

        var users = await response.Content.ReadFromJsonAsync<List<UserRepresentation>>(cancellationToken);
        var user = users?.FirstOrDefault();

        return user is null ? null : ToUser(user);
    }

    public async Task<KeycloakUserPage> SearchUsersAsync(string? searchTerm, int skip, int take,
        CancellationToken cancellationToken)
    {
        var search = string.IsNullOrWhiteSpace(searchTerm) ? string.Empty : $"search={Uri.EscapeDataString(searchTerm)}&";

        using var pageResponse = await SendAsync(
            () => new HttpRequestMessage(HttpMethod.Get,
                AdminUri($"{UsersResource}?{search}first={skip}&max={take}&briefRepresentation=false")),
            cancellationToken);
        pageResponse.EnsureSuccessStatusCode();
        var users = await pageResponse.Content.ReadFromJsonAsync<List<UserRepresentation>>(cancellationToken) ?? [];

        using var countResponse = await SendAsync(
            () => new HttpRequestMessage(HttpMethod.Get, AdminUri($"{UsersResource}/count?{search.TrimEnd('&')}")),
            cancellationToken);
        countResponse.EnsureSuccessStatusCode();
        var total = await countResponse.Content.ReadFromJsonAsync<int>(cancellationToken);

        return new KeycloakUserPage(users.Select(ToUser).ToList(), total);
    }

    public async Task<IReadOnlyList<ApplicationUserId>> GetUserIdsInRoleAsync(string roleName, int max,
        CancellationToken cancellationToken)
    {
        using var response = await SendAsync(
            () => new HttpRequestMessage(HttpMethod.Get,
                AdminUri($"roles/{Uri.EscapeDataString(roleName)}/users?first=0&max={max}&briefRepresentation=true")),
            cancellationToken);
        response.EnsureSuccessStatusCode();

        var users = await response.Content.ReadFromJsonAsync<List<UserRepresentation>>(cancellationToken) ?? [];

        return users
            .Where(u => u.Id is not null && DefaultIdType.TryParse(u.Id, out _))
            .Select(u => new ApplicationUserId(DefaultIdType.Parse(u.Id!)))
            .ToList();
    }

    public async Task<IReadOnlyList<KeycloakUser>> GetUsersInRoleAsync(string roleName, int skip, int take,
        CancellationToken cancellationToken)
    {
        using var response = await SendAsync(
            () => new HttpRequestMessage(HttpMethod.Get,
                AdminUri($"roles/{Uri.EscapeDataString(roleName)}/users?first={skip}&max={take}&briefRepresentation=true")),
            cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return [];
        }

        response.EnsureSuccessStatusCode();

        var users = await response.Content.ReadFromJsonAsync<List<UserRepresentation>>(cancellationToken) ?? [];
        return users.Select(ToUser).ToList();
    }

    public async Task<IReadOnlyList<string>> GetRolesContainingAsync(string roleName, CancellationToken cancellationToken)
    {
        var childrenOf = await GetRoleGraphAsync(cancellationToken);

        var parentsOf = new Dictionary<string, List<string>>(StringComparer.Ordinal);
        foreach (var (composite, children) in childrenOf)
        {
            foreach (var child in children)
            {
                if (!parentsOf.TryGetValue(child, out var parents))
                {
                    parents = [];
                    parentsOf[child] = parents;
                }

                parents.Add(composite);
            }
        }

        var containing = new HashSet<string>(StringComparer.Ordinal);
        var pending = new Queue<string>();
        pending.Enqueue(roleName);
        while (pending.TryDequeue(out var current))
        {
            foreach (var parent in parentsOf.GetValueOrDefault(current, []))
            {
                if (parent != roleName && containing.Add(parent))
                {
                    pending.Enqueue(parent);
                }
            }
        }

        return containing.Order(StringComparer.Ordinal).ToList();
    }

    /// <summary>
    ///     Every composite realm role with the realm roles it includes. Keycloak has no reverse lookup, so building it costs
    ///     one listing plus one call per composite role; it is kept for <see cref="KeycloakOptions.RoleGraphCacheSeconds" />
    ///     and a failed read is never cached. Remove <see cref="CacheKeys.For.KeycloakRoleGraph" /> to force a re-read.
    /// </summary>
    private ValueTask<Dictionary<string, string[]>> GetRoleGraphAsync(CancellationToken cancellationToken)
    {
        // A fresh options instance on purpose (same reason as the bulk user lookup): the configured defaults carry factory
        // timeouts that would turn a slow Keycloak into a synthetic timeout mid-walk.
        var entryOptions = new FusionCacheEntryOptions
        {
            Duration = TimeSpan.FromSeconds(keycloakOptionsProvider.Value.RoleGraphCacheSeconds),
            IsFailSafeEnabled = true
        };

        return cache.GetOrSetAsync<Dictionary<string, string[]>>(
            CacheKeys.For.KeycloakRoleGraph,
            (_, ct) => ReadRoleGraphAsync(ct),
            options: entryOptions,
            token: cancellationToken);
    }

    private async Task<Dictionary<string, string[]>> ReadRoleGraphAsync(CancellationToken cancellationToken)
    {
        using var rolesResponse = await SendAsync(
            () => new HttpRequestMessage(HttpMethod.Get, AdminUri("roles?briefRepresentation=true")),
            cancellationToken);
        rolesResponse.EnsureSuccessStatusCode();
        var roles = await rolesResponse.Content.ReadFromJsonAsync<List<RoleSummaryRepresentation>>(cancellationToken) ?? [];

        var childrenOf = new Dictionary<string, string[]>(StringComparer.Ordinal);
        foreach (var composite in roles.Where(r => r.Composite && !r.ClientRole))
        {
            var children = await GetCompositeChildrenAsync(composite.Name, cancellationToken);
            childrenOf[composite.Name] = children.Where(c => !c.ClientRole).Select(c => c.Name).ToArray();
        }

        return childrenOf;
    }

    public async Task<IReadOnlyList<KeycloakUser>> GetUsersInGroupsHoldingRolesAsync(
        IReadOnlyCollection<string> roleNames, int pageSize, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(pageSize, 1);

        var visited = new HashSet<string>(StringComparer.Ordinal);
        var pending = new Queue<string>();
        foreach (var roleName in roleNames)
        {
            var groups = await ReadEveryPageAsync<GroupRepresentation>(
                $"roles/{Uri.EscapeDataString(roleName)}/groups?briefRepresentation=true", pageSize, cancellationToken);
            foreach (var group in groups.Where(g => visited.Add(g.Id)))
            {
                pending.Enqueue(group.Id);
            }
        }

        var members = new Dictionary<ApplicationUserId, KeycloakUser>();
        while (pending.TryDequeue(out var groupId))
        {
            var users = await ReadEveryPageAsync<UserRepresentation>(
                $"groups/{groupId}/members?briefRepresentation=true", pageSize, cancellationToken);
            foreach (var user in users.Select(ToUser))
            {
                members.TryAdd(user.Id, user);
            }

            var subGroups = await ReadEveryPageAsync<GroupRepresentation>($"groups/{groupId}/children", pageSize, cancellationToken);
            foreach (var subGroup in subGroups.Where(g => visited.Add(g.Id)))
            {
                pending.Enqueue(subGroup.Id);
            }
        }

        return members.Values.ToList();
    }

    /// <summary>Reads a paged Admin REST list to its end. A resource that does not exist (deleted meanwhile) is an empty list.</summary>
    private async Task<List<T>> ReadEveryPageAsync<T>(string path, int pageSize, CancellationToken cancellationToken)
    {
        var separator = path.Contains('?', StringComparison.Ordinal) ? '&' : '?';
        var all = new List<T>();
        while (true)
        {
            var first = all.Count;
            using var response = await SendAsync(
                () => new HttpRequestMessage(HttpMethod.Get, AdminUri($"{path}{separator}first={first}&max={pageSize}")),
                cancellationToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return all;
            }

            response.EnsureSuccessStatusCode();
            var page = await response.Content.ReadFromJsonAsync<List<T>>(cancellationToken) ?? [];
            all.AddRange(page);

            if (page.Count < pageSize)
            {
                return all;
            }
        }
    }

    private async Task<List<RoleSummaryRepresentation>> GetCompositeChildrenAsync(string roleName, CancellationToken cancellationToken)
    {
        using var response = await SendAsync(
            () => new HttpRequestMessage(HttpMethod.Get, AdminUri($"roles/{Uri.EscapeDataString(roleName)}/composites")),
            cancellationToken);

        // The role can be deleted between the listing and this read.
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return [];
        }

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<RoleSummaryRepresentation>>(cancellationToken) ?? [];
    }

    public async Task<IReadOnlyList<KeycloakUserSession>> GetUserSessionsAsync(ApplicationUserId userId,
        CancellationToken cancellationToken)
    {
        using var response = await SendAsync(
            () => new HttpRequestMessage(HttpMethod.Get, AdminUri($"{UsersResource}/{userId}/sessions")),
            cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return [];
        }

        response.EnsureSuccessStatusCode();
        var sessions = await response.Content.ReadFromJsonAsync<List<UserSessionRepresentation>>(cancellationToken) ?? [];

        return sessions
            .Select(s => new KeycloakUserSession(
                s.Id,
                s.IpAddress,
                DateTimeOffset.FromUnixTimeMilliseconds(s.Start),
                DateTimeOffset.FromUnixTimeMilliseconds(s.LastAccess)))
            .ToList();
    }

    public async Task DeleteSessionAsync(string sessionId, CancellationToken cancellationToken)
    {
        using var response = await SendAsync(
            () => new HttpRequestMessage(HttpMethod.Delete, AdminUri($"sessions/{Uri.EscapeDataString(sessionId)}")),
            cancellationToken);

        if (response.StatusCode != HttpStatusCode.NotFound)
        {
            response.EnsureSuccessStatusCode();
        }

        // 404 = already gone in Keycloak; a decision cached before that still needs to go.
        await cache.RemoveByTagAsync(CacheKeys.For.AuthorizationDecisionSessionTag(sessionId), token: cancellationToken);
    }

    public async Task LogoutUserAsync(ApplicationUserId userId, CancellationToken cancellationToken)
    {
        using var response = await SendAsync(
            () => new HttpRequestMessage(HttpMethod.Post, AdminUri($"{UsersResource}/{userId}/logout")),
            cancellationToken);
        response.EnsureSuccessStatusCode();

        await cache.RemoveByTagAsync(CacheKeys.For.AuthorizationDecisionUserTag(userId), token: cancellationToken);
    }

    /// <summary>
    ///     Sends with the service-account bearer token. A 401 means Keycloak no longer knows the cached token
    ///     (restart, session purge), so the token is refreshed and the request replayed once.
    /// </summary>
    private async Task<HttpResponseMessage> SendAsync(Func<HttpRequestMessage> requestFactory,
        CancellationToken cancellationToken)
    {
        var response = await SendWithTokenAsync(requestFactory, forceRefresh: false, cancellationToken);
        if (response.StatusCode != HttpStatusCode.Unauthorized)
        {
            return response;
        }

        response.Dispose();
        LogServiceAccountTokenRejected(logger);
        return await SendWithTokenAsync(requestFactory, forceRefresh: true, cancellationToken);
    }

    private async Task<HttpResponseMessage> SendWithTokenAsync(Func<HttpRequestMessage> requestFactory,
        bool forceRefresh, CancellationToken cancellationToken)
    {
        var token = await tokenProvider.GetAccessTokenAsync(forceRefresh, cancellationToken);
        using var request = requestFactory();
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return await httpClient.SendAsync(request, cancellationToken);
    }

    private Uri AdminUri(string relative)
    {
        return new Uri(KeycloakPaths.Admin(keycloakOptionsProvider.Value.Realm, relative), UriKind.Relative);
    }

    private static KeycloakUser ToUser(UserRepresentation user)
    {
        var attributes = user.Attributes;
        var birthDateRaw = attributes?.GetValueOrDefault(UserAttributes.BirthDate)?.FirstOrDefault();
        DateOnly? birthDate = birthDateRaw is not null &&
                              DateOnly.TryParseExact(birthDateRaw, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                                  DateTimeStyles.None, out var parsed)
            ? parsed
            : null;

        if (!DefaultIdType.TryParse(user.Id, out var id))
        {
            throw new HttpRequestException($"Keycloak returned a non-UUID user id '{user.Id}'.");
        }

        return new KeycloakUser(
            new ApplicationUserId(id),
            user.Username ?? string.Empty,
            user.Email,
            user.FirstName,
            user.LastName,
            attributes?.GetValueOrDefault(UserAttributes.PhoneNumber)?.FirstOrDefault(),
            birthDate,
            user.Enabled,
            DateTimeOffset.FromUnixTimeMilliseconds(user.CreatedTimestamp ?? 0),
            string.Equals(attributes?.GetValueOrDefault(UserAttributes.PhoneNumberVerified)?.FirstOrDefault(), "true",
                StringComparison.OrdinalIgnoreCase));
    }

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "Keycloak rejected the user representation (field {Field}): {Message}.")]
    private static partial void LogUserRejected(ILogger logger, string? field, string? message);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "Keycloak rejected role mapping {RoleName} for user {UserId}: status {StatusCode}.")]
    private static partial void LogRoleAssignmentFailed(ILogger logger, ApplicationUserId userId, string roleName, int statusCode);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "Keycloak rejected the cached service-account token; refreshing and retrying once.")]
    private static partial void LogServiceAccountTokenRejected(ILogger logger);
}
