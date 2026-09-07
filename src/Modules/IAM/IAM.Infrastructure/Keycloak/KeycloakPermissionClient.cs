using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Common.Application.Options;
using IAM.Application.Keycloak;
using IAM.Infrastructure.Keycloak.Representations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IAM.Infrastructure.Keycloak;

internal sealed partial class KeycloakPermissionClient(
    HttpClient httpClient,
    IOptions<KeycloakOptions> keycloakOptionsProvider,
    ILogger<KeycloakPermissionClient> logger
) : IKeycloakPermissionClient
{
    private const string UmaTicketGrant = "urn:ietf:params:oauth:grant-type:uma-ticket";

    public async Task<bool> DecideAsync(string accessToken, string permission, CancellationToken cancellationToken)
    {
        using var response = await PostAsync(accessToken,
            new Dictionary<string, string>
            {
                ["grant_type"] = UmaTicketGrant,
                ["audience"] = keycloakOptionsProvider.Value.ResourceClientId,
                ["permission"] = permission,
                ["response_mode"] = "decision"
            },
            cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var decision = await response.Content.ReadFromJsonAsync<DecisionRepresentation>(cancellationToken);
            return decision?.Result == true;
        }

        // 403 access_denied: policies evaluated to deny. 400/401 invalid_grant: Keycloak no longer accepts the
        // bearer token (session revoked or logged out). Both are a definitive "no", not an outage.
        if (response.StatusCode is HttpStatusCode.Forbidden or HttpStatusCode.Unauthorized or HttpStatusCode.BadRequest)
        {
            var error = await response.Content.TryReadFromJsonAsync<TokenErrorRepresentation>(cancellationToken);

            // Anything other than the two expected denial codes (e.g. invalid_resource, invalid_scope from a
            // typo'd resource/scope name) is a realm/config mismatch, not a normal deny: worth a Warning so it
            // does not read as routine traffic (PR #149 #7).
            if (error?.Error is OAuthErrors.AccessDenied or OAuthErrors.InvalidGrant)
            {
                LogDenied(logger, permission, error.Error, error.ErrorDescription);
            }
            else
            {
                LogUnexpectedDenial(logger, permission, error?.Error, error?.ErrorDescription);
            }

            return false;
        }

        response.EnsureSuccessStatusCode();
        return false;
    }

    public async Task<IReadOnlyList<GrantedPermission>> ListPermissionsAsync(string accessToken,
        CancellationToken cancellationToken)
    {
        using var response = await PostAsync(accessToken,
            new Dictionary<string, string>
            {
                ["grant_type"] = UmaTicketGrant,
                ["audience"] = keycloakOptionsProvider.Value.ResourceClientId,
                ["response_mode"] = "permissions"
            },
            cancellationToken);

        // A user with no permission at all gets 403, which is a valid empty answer here.
        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            return [];
        }

        response.EnsureSuccessStatusCode();
        var permissions = await response.Content.ReadFromJsonAsync<List<PermissionRepresentation>>(cancellationToken);

        return permissions is null
            ? []
            : permissions.Select(p => new GrantedPermission(p.ResourceName, p.Scopes ?? [])).ToList();
    }

    private async Task<HttpResponseMessage> PostAsync(string accessToken, Dictionary<string, string> form,
        CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post,
            new Uri(KeycloakPaths.Token(keycloakOptionsProvider.Value.Realm), UriKind.Relative));
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        request.Content = new FormUrlEncodedContent(form);

        return await httpClient.SendAsync(request, cancellationToken);
    }

    [LoggerMessage(Level = LogLevel.Debug,
        Message = "Keycloak denied {Permission}: {Error} {Description}.")]
    private static partial void LogDenied(ILogger logger, string permission, string? error, string? description);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "Keycloak rejected a permission check for {Permission} with an unexpected error {Error}: {Description}. This usually means the resource or scope name is misconfigured.")]
    private static partial void LogUnexpectedDenial(ILogger logger, string permission, string? error, string? description);
}
