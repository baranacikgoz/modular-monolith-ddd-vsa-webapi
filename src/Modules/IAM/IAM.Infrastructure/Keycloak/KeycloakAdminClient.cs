using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Common.Application.Options;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using IAM.Application.Keycloak;
using IAM.Domain.Errors;
using IAM.Infrastructure.Keycloak.Representations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IAM.Infrastructure.Keycloak;

internal sealed partial class KeycloakAdminClient(
    HttpClient httpClient,
    IServiceAccountTokenProvider tokenProvider,
    IOptions<KeycloakOptions> keycloakOptionsProvider,
    ILogger<KeycloakAdminClient> logger
) : IKeycloakAdminClient
{
    private const string UsersResource = "users";

    public async Task<Result<ApplicationUserId>> CreateUserAsync(CreateKeycloakUser user,
        CancellationToken cancellationToken)
    {
        var representation = new UserRepresentation
        {
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Enabled = true,
            Attributes = new Dictionary<string, List<string>>
            {
                [UserAttributes.PhoneNumber] = [user.PhoneNumber],
                [UserAttributes.PhoneNumberVerified] = ["true"],
                [UserAttributes.BirthDate] = [user.BirthDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)]
            }
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
                return IdentityErrors.PhoneNumberAlreadyRegistered;
            case HttpStatusCode.BadRequest:
                var error = await ReadErrorAsync(response, cancellationToken);
                LogUserRejected(logger, error?.Field, error?.ErrorMessage);
                return IdentityErrors.IdentityProviderRejectedUser;
            default:
                response.EnsureSuccessStatusCode();
                throw new HttpRequestException($"Unexpected Keycloak status {(int)response.StatusCode} creating a user.");
        }
    }

    public async Task AssignRealmRoleAsync(ApplicationUserId userId, string roleName, CancellationToken cancellationToken)
    {
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
        mappingResponse.EnsureSuccessStatusCode();
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

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return;
        }

        response.EnsureSuccessStatusCode();
    }

    public async Task LogoutUserAsync(ApplicationUserId userId, CancellationToken cancellationToken)
    {
        using var response = await SendAsync(
            () => new HttpRequestMessage(HttpMethod.Post, AdminUri($"{UsersResource}/{userId}/logout")),
            cancellationToken);
        response.EnsureSuccessStatusCode();
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

        return new KeycloakUser(
            new ApplicationUserId(DefaultIdType.Parse(user.Id!)),
            user.Username ?? string.Empty,
            user.Email,
            user.FirstName,
            user.LastName,
            attributes?.GetValueOrDefault(UserAttributes.PhoneNumber)?.FirstOrDefault(),
            birthDate,
            user.Enabled,
            DateTimeOffset.FromUnixTimeMilliseconds(user.CreatedTimestamp ?? 0));
    }

    private static async Task<ErrorRepresentation?> ReadErrorAsync(HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        try
        {
            return await response.Content.ReadFromJsonAsync<ErrorRepresentation>(cancellationToken);
        }
        catch (JsonException)
        {
            return null;
        }
    }

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "Keycloak rejected the user representation (field {Field}): {Message}.")]
    private static partial void LogUserRejected(ILogger logger, string? field, string? message);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "Keycloak rejected the cached service-account token; refreshing and retrying once.")]
    private static partial void LogServiceAccountTokenRejected(ILogger logger);
}
