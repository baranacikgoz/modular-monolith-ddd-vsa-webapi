using Common.Domain.ResultMonad;

namespace IAM.Application.Keycloak;

/// <summary>Keycloak token endpoint. Failures come back as <see cref="Result" /> errors, never exceptions.</summary>
public interface IKeycloakTokenClient
{
    /// <summary>
    ///     Issues tokens for <paramref name="username" /> through the trusted-login client, whose direct grant flow has no
    ///     credential step. Call only after this API verified a one-time code for that user.
    /// </summary>
    Task<Result<KeycloakTokens>> TrustedLoginAsync(string username, CancellationToken cancellationToken);

    /// <summary>Email (or username) + password direct grant on the resource client.</summary>
    Task<Result<KeycloakTokens>> PasswordLoginAsync(string username, string password, CancellationToken cancellationToken);

    /// <summary>Rotates a refresh token. The issuing client is derived from the token's <c>azp</c> claim.</summary>
    Task<Result<KeycloakTokens>> RefreshAsync(string refreshToken, CancellationToken cancellationToken);
}
