using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Extensions;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Notifications;
using IAM.Application.Keycloak;
using IAM.Infrastructure.Telemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace IAM.Endpoints.Tokens.VersionNeutral.Revoke;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder tokensApiGroup)
    {
        tokensApiGroup
            .MapPost("revoke", RevokeToken)
            .WithDescription("Sign out: revoke the current session in Keycloak and detach the device.")
            .RequireScope(KeycloakScopes.Sessions.RevokeOwn)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> RevokeToken(
        ICurrentUser currentUser,
        IKeycloakAdminClient adminClient,
        IInterModuleRequestClient<DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse> deviceClient,
        CancellationToken cancellationToken)
    {
        using var activity = IamTelemetry.ActivitySource.StartActivityForCaller();

        if (currentUser.SessionId is not { } sessionId)
        {
            // Service accounts have no session to revoke: nothing to do.
            return Result.Success;
        }

        return await Result<string>.Success(sessionId)
            .TapAsync(sid => adminClient.DeleteSessionAsync(sid, cancellationToken))
            .TapAsync(sid => deviceClient.SendAsync(
                new DeactivateDeviceSessionsRequest(currentUser.Id, [sid]), cancellationToken))
            .TapAsync(_ => IamTelemetry.RecordSessionRevoked(SessionRevokedReasons.UserSignedOut))
            .TapActivityAsync(activity)
            .MapAsync(_ => Result.Success);
    }
}
