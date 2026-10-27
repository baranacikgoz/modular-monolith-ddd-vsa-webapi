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

/// <summary>
///     <paramref name="PhoneNumberVerified" /> and <paramref name="EmailVerified" /> say whether the caller proved
///     ownership of <paramref name="PhoneNumber" /> or <paramref name="Email" /> (an OTP round trip) before asking for the
///     user. Both default to unverified, so a flow that did not verify an address cannot mark it verified by omission.
///     Each is ignored when its address is absent.
/// </summary>
public sealed record CreateKeycloakUser(
    string Username,
    string FirstName,
    string LastName,
    string? PhoneNumber,
    DateOnly? BirthDate = null,
    string? Email = null,
    string? Password = null,
    bool PhoneNumberVerified = false,
    bool EmailVerified = false);

public sealed record KeycloakUserPage(IReadOnlyList<KeycloakUser> Users, int TotalCount);

/// <summary>Scopes the caller holds on one resource, as evaluated by Keycloak.</summary>
public sealed record GrantedPermission(string Resource, IReadOnlyList<string> Scopes);
