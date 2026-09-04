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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Polly.CircuitBreaker;
using Polly.Timeout;

namespace IAM.Infrastructure.Keycloak;

internal sealed partial class KeycloakTokenClient(
    HttpClient httpClient,
    IOptions<KeycloakOptions> keycloakOptionsProvider,
    TimeProvider timeProvider,
    ILogger<KeycloakTokenClient> logger
) : IKeycloakTokenClient
{
    private const string InvalidGrant = "invalid_grant";
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
                ["grant_type"] = "refresh_token",
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
                var error = await ReadErrorAsync(response, cancellationToken);
                if (string.Equals(error?.Error, InvalidGrant, StringComparison.Ordinal))
                {
                    LogGrantRejected(logger, form["grant_type"], error?.ErrorDescription);
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

    private static async Task<TokenErrorRepresentation?> ReadErrorAsync(HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        try
        {
            return await response.Content.ReadFromJsonAsync<TokenErrorRepresentation>(cancellationToken);
        }
        catch (JsonException)
        {
            return null;
        }
    }

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Keycloak rejected a {GrantType} grant: {Description}.")]
    private static partial void LogGrantRejected(ILogger logger, string grantType, string? description);

    [LoggerMessage(Level = LogLevel.Error,
        Message = "Unexpected Keycloak token endpoint response {StatusCode}: {Error} {Description}.")]
    private static partial void LogUnexpectedTokenResponse(ILogger logger, int statusCode, string? error, string? description);

    [LoggerMessage(Level = LogLevel.Error, Message = "Keycloak token endpoint unreachable.")]
    private static partial void LogTokenEndpointUnreachable(ILogger logger, Exception ex);
}
