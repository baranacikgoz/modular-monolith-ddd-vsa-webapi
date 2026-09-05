using Common.Domain.StronglyTypedIds;

namespace IAM.Application.Keycloak;

/// <summary>Token pair issued by Keycloak plus the identity it is bound to, read from the access token.</summary>
public sealed record KeycloakTokens(
    string AccessToken,
    DateTimeOffset AccessTokenExpiresAt,
    string RefreshToken,
    DateTimeOffset RefreshTokenExpiresAt,
    ApplicationUserId UserId,
    string SessionId);
