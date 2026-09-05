using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Notifications;
using IAM.Application.Keycloak;
using IAM.Infrastructure.Telemetry;
using MassTransit;
using Microsoft.Extensions.Logging;
using Polly.CircuitBreaker;
using Polly.Timeout;

namespace IAM.Endpoints.Tokens;

/// <summary>
///     Second half of every login: bind the new Keycloak session to the calling device (Notifications module)
///     and, when that device already held a session, revoke the old one so one device never has two live sessions.
/// </summary>
internal static partial class LoginCompletion
{
    internal static async Task<Result<KeycloakTokens>> BindDeviceAsync(
        KeycloakTokens tokens,
        Guid deviceId,
        string clientId,
        string? deviceName,
        string? pushToken,
        IInterModuleRequestClient<BindDeviceSessionRequest, BindDeviceSessionResponse> deviceClient,
        IKeycloakAdminClient adminClient,
        ILogger logger,
        CancellationToken cancellationToken)
    {
        BindDeviceSessionResponse bound;
        try
        {
            bound = await deviceClient.SendAsync(
                new BindDeviceSessionRequest(tokens.UserId, tokens.SessionId, deviceId, clientId, deviceName, pushToken),
                cancellationToken);
        }
        catch (MassTransitException ex)
        {
            // Notifications is down, disabled in modules.json, timed out, or its handler faulted. Keycloak already
            // issued the tokens, so the login stands; only push delivery and the supersede rule skip this session.
            LogDeviceBindingFailed(logger, tokens.UserId, tokens.SessionId, ex);
            return tokens;
        }

        if (bound.SupersededSessionId is not { } supersededSessionId)
        {
            return tokens;
        }

        // Best effort: the new login already succeeded. A stale session that outlives this attempt still
        // expires on its own (SSO idle timeout) and its refresh token cannot be used by this device anymore.
        try
        {
            await adminClient.DeleteSessionAsync(supersededSessionId, cancellationToken);
            IamTelemetry.RecordSessionRevoked(SessionRevokedReasons.SupersededByNewLogin);
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException
                                       or BrokenCircuitException or TimeoutRejectedException)
        {
            LogSupersededSessionRevocationFailed(logger, supersededSessionId, ex);
        }

        return tokens;
    }

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "Could not revoke superseded Keycloak session {SessionId}; it will expire on its own.")]
    private static partial void LogSupersededSessionRevocationFailed(ILogger logger, string sessionId, Exception ex);

    [LoggerMessage(Level = LogLevel.Error,
        Message = "Could not bind Keycloak session {SessionId} of user {UserId} to its device; login continues without device metadata.")]
    private static partial void LogDeviceBindingFailed(ILogger logger, ApplicationUserId userId, string sessionId, Exception ex);
}
