using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Extensions;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Notifications;
using IAM.Application.Keycloak;
using IAM.Domain.Errors;
using IAM.Infrastructure.Telemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder sessionsApiGroup)
    {
        sessionsApiGroup
            .MapDelete("{id}", RevokeSession)
            .WithDescription("Sign out one specific session (device/app).")
            .RequireScope(KeycloakScopes.Sessions.RevokeOwn)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> RevokeSession(
        [AsParameters] Request request,
        ICurrentUser currentUser,
        IKeycloakAdminClient adminClient,
        IInterModuleRequestClient<DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse> deviceClient,
        CancellationToken cancellationToken)
    {
        using var activity = IamTelemetry.ActivitySource.StartActivityForCaller();

        // Ownership check against the caller's own session list: another user's session id resolves as
        // not-found, never revealing that it exists.
        var sessions = await adminClient.GetUserSessionsAsync(currentUser.Id, cancellationToken);

        return await Result<string>.Success(request.Id)
            .Bind(sid => sessions.Any(s => string.Equals(s.Id, sid, StringComparison.Ordinal))
                ? Result<string>.Success(sid)
                : TokenErrors.SessionNotFound)
            .TapAsync(sid => adminClient.DeleteSessionAsync(sid, cancellationToken))
            .TapAsync(sid => deviceClient.SendAsync(
                new DeactivateDeviceSessionsRequest(currentUser.Id, [sid]), cancellationToken))
            .TapAsync(_ => IamTelemetry.RecordSessionRevoked(SessionRevokedReasons.RevokedByUser))
            .TapActivityAsync(activity)
            .MapAsync(_ => Result.Success);
    }
}
