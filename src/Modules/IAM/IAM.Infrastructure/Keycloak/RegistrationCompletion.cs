using Common.Application.Auth;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using IAM.Application.Keycloak;
using IAM.Infrastructure.Telemetry;
using Microsoft.Extensions.Logging;
using Polly.CircuitBreaker;
using Polly.Timeout;

namespace IAM.Infrastructure.Keycloak;

/// <summary>
///     Second half of every user creation, shared by the phone and email self-registration endpoints and by the
///     <c>ProvisionUserRequest</c> handler (mirrors <c>LoginCompletion</c>).
/// </summary>
public static partial class RegistrationCompletion
{
    /// <summary>
    ///     The user already exists in Keycloak from <c>CreateUserAsync</c>; role assignment is the second of two
    ///     non-transactional Admin REST calls. On failure, delete the user so a retry sees a clean "not registered"
    ///     state instead of a roleless account that 409s forever.
    /// </summary>
    public static async Task<Result<ApplicationUserId>> AssignBasicRoleOrRollbackAsync(
        ApplicationUserId userId, IKeycloakAdminClient adminClient, ILogger logger, CancellationToken cancellationToken)
    {
        var assigned = await adminClient.AssignRealmRoleAsync(userId, KeycloakRoles.Basic, cancellationToken);
        if (assigned.Error is not { } error)
        {
            return userId;
        }

        try
        {
            await adminClient.DeleteUserAsync(userId, cancellationToken);
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException
                                       or BrokenCircuitException or TimeoutRejectedException)
        {
            LogRegistrationRollbackFailed(logger, userId, ex);
            IamTelemetry.RecordOrphanedUserLeftRoleless();
        }

        return Result<ApplicationUserId>.Failure(error);
    }

    [LoggerMessage(Level = LogLevel.Error,
        Message = "Role assignment failed for newly created Keycloak user {UserId} and the compensating delete also failed; the user is left roleless.")]
    private static partial void LogRegistrationRollbackFailed(ILogger logger, ApplicationUserId userId, Exception ex);
}
