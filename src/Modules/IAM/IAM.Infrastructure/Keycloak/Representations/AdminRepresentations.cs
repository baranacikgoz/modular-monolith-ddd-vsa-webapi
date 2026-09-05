using System.Text.Json.Serialization;

namespace IAM.Infrastructure.Keycloak.Representations;

internal sealed class UserRepresentation
{
    [JsonPropertyName("id")] public string? Id { get; set; }
    [JsonPropertyName("username")] public string? Username { get; set; }
    [JsonPropertyName("email")] public string? Email { get; set; }
    [JsonPropertyName("firstName")] public string? FirstName { get; set; }
    [JsonPropertyName("lastName")] public string? LastName { get; set; }
    [JsonPropertyName("enabled")] public bool Enabled { get; set; }
    [JsonPropertyName("emailVerified")] public bool? EmailVerified { get; set; }
    [JsonPropertyName("createdTimestamp")] public long? CreatedTimestamp { get; set; }
    [JsonPropertyName("attributes")] public Dictionary<string, List<string>>? Attributes { get; set; }
}

internal sealed class RoleRepresentation
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
}

internal sealed class UserSessionRepresentation
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;
    [JsonPropertyName("ipAddress")] public string? IpAddress { get; set; }
    [JsonPropertyName("start")] public long Start { get; set; }
    [JsonPropertyName("lastAccess")] public long LastAccess { get; set; }
}

internal sealed class ErrorRepresentation
{
    [JsonPropertyName("errorMessage")] public string? ErrorMessage { get; set; }
    [JsonPropertyName("error")] public string? Error { get; set; }
    [JsonPropertyName("field")] public string? Field { get; set; }
}
