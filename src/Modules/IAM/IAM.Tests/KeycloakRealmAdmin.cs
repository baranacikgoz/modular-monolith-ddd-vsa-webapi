using System.Net.Http.Json;
using System.Text.Json;
using Common.Domain.StronglyTypedIds;
using Testcontainers.Keycloak;

namespace IAM.Tests;

/// <summary>
///     Realm changes the API's own service account is not allowed to make (creating roles, composing them, disabling a
///     user), done with the Keycloak container's bootstrap administrator so a test can shape the realm it needs.
/// </summary>
internal sealed class KeycloakRealmAdmin : IDisposable
{
    private readonly HttpClient _http;
    private readonly string _realm;

    private KeycloakRealmAdmin(HttpClient http, string realm)
    {
        _http = http;
        _realm = realm;
    }

    public static async Task<KeycloakRealmAdmin> ConnectAsync(string realm)
    {
        var container = await SharedKeycloak.GetAsync();
        var http = new HttpClient { BaseAddress = new Uri(container.GetBaseAddress().TrimEnd('/') + "/") };

        using var tokenRequest = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["grant_type"] = "password",
            ["client_id"] = "admin-cli",
            ["username"] = KeycloakBuilder.DefaultUsername,
            ["password"] = KeycloakBuilder.DefaultPassword
        });
        using var tokenResponse = await http.PostAsync(
            new Uri("realms/master/protocol/openid-connect/token", UriKind.Relative), tokenRequest);
        tokenResponse.EnsureSuccessStatusCode();
        var token = await tokenResponse.Content.ReadFromJsonAsync<JsonElement>();
        http.DefaultRequestHeaders.Authorization = new("Bearer", token.GetProperty("access_token").GetString());

        return new KeycloakRealmAdmin(http, realm);
    }

    public async Task CreateRoleAsync(string name)
    {
        using var response = await _http.PostAsJsonAsync(AdminUri("roles"), new { name });
        response.EnsureSuccessStatusCode();
    }

    /// <summary>Makes <paramref name="parent" /> a composite role that includes <paramref name="child" />.</summary>
    public async Task AddCompositeAsync(string parent, string child)
    {
        var childRepresentation = await _http.GetFromJsonAsync<JsonElement>(AdminUri($"roles/{Uri.EscapeDataString(child)}"));

        using var response = await _http.PostAsJsonAsync(
            AdminUri($"roles/{Uri.EscapeDataString(parent)}/composites"),
            new[] { childRepresentation });
        response.EnsureSuccessStatusCode();
    }

    /// <summary>Creates a top-level group, or a subgroup of <paramref name="parentId" />, and returns its id.</summary>
    public async Task<string> CreateGroupAsync(string name, string? parentId = null)
    {
        using var response = await _http.PostAsJsonAsync(
            AdminUri(parentId is null ? "groups" : $"groups/{parentId}/children"), new { name });
        response.EnsureSuccessStatusCode();
        return response.Headers.Location!.Segments[^1].TrimEnd('/');
    }

    /// <summary>Maps a realm role onto a group, so every member of it (and of its subgroups) holds the role.</summary>
    public async Task AddRealmRoleToGroupAsync(string groupId, string roleName)
    {
        var role = await _http.GetFromJsonAsync<JsonElement>(AdminUri($"roles/{Uri.EscapeDataString(roleName)}"));

        using var response = await _http.PostAsJsonAsync(AdminUri($"groups/{groupId}/role-mappings/realm"), new[] { role });
        response.EnsureSuccessStatusCode();
    }

    public async Task AddUserToGroupAsync(ApplicationUserId userId, string groupId)
    {
        using var response = await _http.PutAsync(AdminUri($"users/{userId}/groups/{groupId}"), content: null);
        response.EnsureSuccessStatusCode();
    }

    public async Task SetEnabledAsync(ApplicationUserId userId, bool enabled)
    {
        using var response = await _http.PutAsJsonAsync(AdminUri($"users/{userId}"), new { enabled });
        response.EnsureSuccessStatusCode();
    }

    public void Dispose()
    {
        _http.Dispose();
    }

    private Uri AdminUri(string relative)
    {
        return new Uri($"admin/realms/{_realm}/{relative}", UriKind.Relative);
    }
}
