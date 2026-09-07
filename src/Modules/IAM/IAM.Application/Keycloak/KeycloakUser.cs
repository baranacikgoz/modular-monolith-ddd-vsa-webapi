using Common.Domain.StronglyTypedIds;

namespace IAM.Application.Keycloak;

public sealed record KeycloakUser(
    ApplicationUserId Id,
    string Username,
    string? Email,
    string? FirstName,
    string? LastName,
    string? PhoneNumber,
    DateOnly? BirthDate,
    bool Enabled,
    DateTimeOffset CreatedOn);

public sealed record KeycloakUserSession(
    string Id,
    string? IpAddress,
    DateTimeOffset StartedAt,
    DateTimeOffset LastAccessAt);

public sealed record CreateKeycloakUser(
    string Username,
    string FirstName,
    string LastName,
    string PhoneNumber,
    DateOnly BirthDate);

public sealed record KeycloakUserPage(IReadOnlyList<KeycloakUser> Users, int TotalCount);

/// <summary>Scopes the caller holds on one resource, as evaluated by Keycloak.</summary>
public sealed record GrantedPermission(string Resource, IReadOnlyList<string> Scopes);
