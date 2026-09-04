using System.Text.Json.Serialization;

namespace IAM.Infrastructure.Keycloak.Representations;

internal sealed class TokenResponseRepresentation
{
    [JsonPropertyName("access_token")] public string AccessToken { get; set; } = string.Empty;
    [JsonPropertyName("expires_in")] public int ExpiresIn { get; set; }
    [JsonPropertyName("refresh_token")] public string? RefreshToken { get; set; }
    [JsonPropertyName("refresh_expires_in")] public int RefreshExpiresIn { get; set; }
    [JsonPropertyName("session_state")] public string? SessionState { get; set; }
}

internal sealed class TokenErrorRepresentation
{
    [JsonPropertyName("error")] public string? Error { get; set; }
    [JsonPropertyName("error_description")] public string? ErrorDescription { get; set; }
}

internal sealed class DecisionRepresentation
{
    [JsonPropertyName("result")] public bool Result { get; set; }
}

internal sealed class PermissionRepresentation
{
    [JsonPropertyName("rsname")] public string ResourceName { get; set; } = string.Empty;
    [JsonPropertyName("scopes")] public List<string>? Scopes { get; set; }
}
