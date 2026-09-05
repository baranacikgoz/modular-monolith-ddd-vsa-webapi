using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Common.Application.Auth;
using Common.Application.Options;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using IAM.Application.Keycloak;
using IAM.Domain.Errors;
using IAM.Infrastructure.Keycloak.Representations;
using IAM.Infrastructure.Telemetry;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Polly.CircuitBreaker;
using Polly.Timeout;

namespace IAM.Infrastructure.Keycloak;

internal sealed partial class KeycloakTokenClient(
    HttpClient httpClient,
    IKeycloakAdminClient adminClient,
    IOptions<KeycloakOptions> keycloakOptionsProvider,
    TimeProvider timeProvider,
    ILogger<KeycloakTokenClient> logger
) : IKeycloakTokenClient
{
    private const string InvalidGrant = "invalid_grant";
    private const string RefreshTokenGrant = "refresh_token";

    // Keycloak's exact error_description values for a replayed refresh token (TokenManager.validateTokenReuse):
    // the current token used more than refreshTokenMaxReuse times, or an older rotated-away token replayed.
    // Both are raised only after the signature was verified, so a forged token can never trigger them.
    private const string ReuseExceededDescription = "Maximum allowed refresh token reuse exceeded";
    private const string StaleTokenDescription = "Stale token";

    private static readonly JsonWebTokenHandler TokenHandler = new();

    public Task<Result<KeycloakTokens>> TrustedLoginAsync(string username, CancellationToken cancellationToken)
    {
        var options = keycloakOptionsProvider.Value;
        return RequestTokensAsync(
            new Dictionary<string, string>
            {
                ["grant_type"] = "password",
                ["client_id"] = options.TrustedLoginClientId,
                ["client_secret"] = options.TrustedLoginClientSecret,
                ["username"] = username
            },
            IdentityErrors.InvalidCredentials,
            cancellationToken);
    }

    public Task<Result<KeycloakTokens>> PasswordLoginAsync(string username, string password,
        CancellationToken cancellationToken)
    {
        var options = keycloakOptionsProvider.Value;
        return RequestTokensAsync(
            new Dictionary<string, string>
            {
                ["grant_type"] = "password",
                ["client_id"] = options.ResourceClientId,
                ["client_secret"] = options.ResourceClientSecret,
                ["username"] = username,
                ["password"] = password
            },
            IdentityErrors.InvalidCredentials,
            cancellationToken);
    }

    public Task<Result<KeycloakTokens>> RefreshAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var options = keycloakOptionsProvider.Value;

        // A refresh token is only accepted by the client that issued it, and the two login clients bind
        // different flows. The (unverified) azp claim tells us which secret to present; Keycloak validates
        // the signature, so a forged azp buys nothing beyond an invalid_grant.
        if (!TryReadAuthorizedParty(refreshToken, out var authorizedParty))
        {
            return Task.FromResult(Result<KeycloakTokens>.Failure(TokenErrors.InvalidRefreshToken));
        }

        string clientId;
        string clientSecret;
        if (string.Equals(authorizedParty, options.TrustedLoginClientId, StringComparison.Ordinal))
        {
            (clientId, clientSecret) = (options.TrustedLoginClientId, options.TrustedLoginClientSecret);
        }
        else if (string.Equals(authorizedParty, options.ResourceClientId, StringComparison.Ordinal))
        {
            (clientId, clientSecret) = (options.ResourceClientId, options.ResourceClientSecret);
        }
        else
        {
            return Task.FromResult(Result<KeycloakTokens>.Failure(TokenErrors.InvalidRefreshToken));
        }

        return RequestTokensAsync(
            new Dictionary<string, string>
            {
                ["grant_type"] = RefreshTokenGrant,
                ["client_id"] = clientId,
                ["client_secret"] = clientSecret,
                ["refresh_token"] = refreshToken
            },
            TokenErrors.InvalidRefreshToken,
            cancellationToken);
    }

    private async Task<Result<KeycloakTokens>> RequestTokensAsync(
        Dictionary<string, string> form, Error errorOnInvalidGrant, CancellationToken cancellationToken)
    {
        var realm = keycloakOptionsProvider.Value.Realm;

        try
        {
            using var content = new FormUrlEncodedContent(form);
            using var response = await httpClient.PostAsync(
                new Uri(KeycloakPaths.Token(realm), UriKind.Relative), content, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadFromJsonAsync<TokenResponseRepresentation>(cancellationToken);
                return token is null ? IdentityErrors.IdentityProviderUnavailable : ToTokens(token);
            }

            if (response.StatusCode is HttpStatusCode.BadRequest or HttpStatusCode.Unauthorized)
            {
                var error = await response.Content.TryReadFromJsonAsync<TokenErrorRepresentation>(cancellationToken);
                if (string.Equals(error?.Error, InvalidGrant, StringComparison.Ordinal))
                {
                    LogGrantRejected(logger, form["grant_type"], error?.ErrorDescription);

                    if (IsRefreshTokenReuse(form, error?.ErrorDescription))
                    {
                        await RevokeReplayedSessionAsync(form[RefreshTokenGrant], cancellationToken);
                    }

                    return errorOnInvalidGrant;
                }

                LogUnexpectedTokenResponse(logger, (int)response.StatusCode, error?.Error, error?.ErrorDescription);
                return IdentityErrors.IdentityProviderUnavailable;
            }

            LogUnexpectedTokenResponse(logger, (int)response.StatusCode, null, null);
            return IdentityErrors.IdentityProviderUnavailable;
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or JsonException
                                       or BrokenCircuitException or TimeoutRejectedException)
        {
            // Transport failures, timeouts and open circuits all mean the same thing to the caller: try again later.
            LogTokenEndpointUnreachable(logger, ex);
            return IdentityErrors.IdentityProviderUnavailable;
        }
    }

    private Result<KeycloakTokens> ToTokens(TokenResponseRepresentation token)
    {
        if (string.IsNullOrEmpty(token.RefreshToken))
        {
            LogUnexpectedTokenResponse(logger, 200, "missing_refresh_token", null);
            return IdentityErrors.IdentityProviderUnavailable;
        }

        var jwt = new JsonWebToken(token.AccessToken);
        var sessionId = jwt.TryGetClaim(JwtClaimNames.SessionId, out var sidClaim) ? sidClaim.Value : token.SessionState;

        if (string.IsNullOrEmpty(sessionId) || !DefaultIdType.TryParse(jwt.Subject, out var subject))
        {
            LogUnexpectedTokenResponse(logger, 200, "missing_sid_or_sub", null);
            return IdentityErrors.IdentityProviderUnavailable;
        }

        var now = timeProvider.GetUtcNow();
        return new KeycloakTokens(
            token.AccessToken,
            now.AddSeconds(token.ExpiresIn),
            token.RefreshToken,
            now.AddSeconds(token.RefreshExpiresIn),
            new ApplicationUserId(subject),
            sessionId);
    }

    private static bool IsRefreshTokenReuse(Dictionary<string, string> form, string? errorDescription)
    {
        return string.Equals(form["grant_type"], RefreshTokenGrant, StringComparison.Ordinal)
               && errorDescription is ReuseExceededDescription or StaleTokenDescription;
    }

    /// <summary>
    ///     Keycloak only detaches the client session on reuse, which leaves the thief's already-rotated token
    ///     alive until the SSO session idles out. Revoking the whole user session closes that window. Best
    ///     effort: the caller gets the same 401 either way, and the session still expires on its own.
    /// </summary>
    private async Task RevokeReplayedSessionAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var jwt = new JsonWebToken(refreshToken);
        var sessionId = jwt.TryGetClaim(JwtClaimNames.SessionId, out var sidClaim) ? sidClaim.Value : null;

        IamTelemetry.RecordRefreshTokenReuseDetected();
        LogRefreshTokenReuseDetected(logger, jwt.Subject, sessionId);

        if (string.IsNullOrEmpty(sessionId))
        {
            return;
        }

        try
        {
            await adminClient.DeleteSessionAsync(sessionId, cancellationToken);
            IamTelemetry.RecordSessionRevoked(SessionRevokedReasons.TokenReuseDetected);
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException
                                       or BrokenCircuitException or TimeoutRejectedException)
        {
            LogReplayedSessionRevocationFailed(logger, sessionId, ex);
        }
    }

    private static bool TryReadAuthorizedParty(string refreshToken, out string authorizedParty)
    {
        authorizedParty = string.Empty;
        if (!TokenHandler.CanReadToken(refreshToken))
        {
            return false;
        }

        var jwt = new JsonWebToken(refreshToken);
        if (!jwt.TryGetClaim(JwtClaimNames.AuthorizedParty, out var claim) || string.IsNullOrEmpty(claim.Value))
        {
            return false;
        }

        authorizedParty = claim.Value;
        return true;
    }

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Keycloak rejected a {GrantType} grant: {Description}.")]
    private static partial void LogGrantRejected(ILogger logger, string grantType, string? description);

    [LoggerMessage(Level = LogLevel.Error,
        Message = "Unexpected Keycloak token endpoint response {StatusCode}: {Error} {Description}.")]
    private static partial void LogUnexpectedTokenResponse(ILogger logger, int statusCode, string? error, string? description);

    [LoggerMessage(Level = LogLevel.Error, Message = "Keycloak token endpoint unreachable.")]
    private static partial void LogTokenEndpointUnreachable(ILogger logger, Exception ex);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "Refresh token reuse detected for user {UserId} session {SessionId}: possible token theft. Revoking the session.")]
    private static partial void LogRefreshTokenReuseDetected(ILogger logger, string? userId, string? sessionId);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "Could not revoke Keycloak session {SessionId} after refresh token reuse; it will expire on its own.")]
    private static partial void LogReplayedSessionRevocationFailed(ILogger logger, string sessionId, Exception ex);
}
