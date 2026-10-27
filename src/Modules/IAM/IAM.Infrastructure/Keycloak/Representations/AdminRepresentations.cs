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
    [JsonPropertyName("credentials")] public List<CredentialRepresentation>? Credentials { get; set; }
}

internal sealed class CredentialRepresentation
{
    [JsonPropertyName("type")] public string Type { get; set; } = "password";
    [JsonPropertyName("value")] public string Value { get; set; } = string.Empty;
    [JsonPropertyName("temporary")] public bool Temporary { get; set; }
}

internal sealed class RoleRepresentation
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
}

/// <summary>The fields of a role listing needed to walk composite roles.</summary>
internal sealed class RoleSummaryRepresentation
{
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
    [JsonPropertyName("composite")] public bool Composite { get; set; }
    [JsonPropertyName("clientRole")] public bool ClientRole { get; set; }
}

/// <summary>The fields of a group listing needed to walk the group tree.</summary>
internal sealed class GroupRepresentation
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;
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

    /// <summary>Set instead of <see cref="ErrorMessage" /> by password policy rejections.</summary>
    [JsonPropertyName("error_description")] public string? ErrorDescription { get; set; }
}
