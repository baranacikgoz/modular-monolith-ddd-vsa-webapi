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

namespace IAM.Endpoints.Tokens.VersionNeutral.Sessions.RevokeAll;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder sessionsApiGroup)
    {
        sessionsApiGroup
            .MapPost("revoke-all", RevokeAllSessions)
            .WithDescription("Sign out everywhere: revoke every session of the caller, including the current one.")
            .RequireScope(KeycloakScopes.Sessions.RevokeOwn)
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> RevokeAllSessions(
        ICurrentUser currentUser,
        IKeycloakAdminClient adminClient,
        IInterModuleRequestClient<DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse> deviceClient,
        CancellationToken cancellationToken)
    {
        using var activity = IamTelemetry.ActivitySource.StartActivityForCaller();

        return await Result<bool>.Success(true)
            .TapAsync(_ => adminClient.LogoutUserAsync(currentUser.Id, cancellationToken))
            .TapAsync(_ => deviceClient.SendAsync(
                new DeactivateDeviceSessionsRequest(currentUser.Id, SessionIds: null), cancellationToken))
            .TapAsync(_ => IamTelemetry.RecordSessionRevoked(SessionRevokedReasons.RevokedAllByUser))
            .TapActivityAsync(activity)
            .MapAsync(_ => Result.Success);
    }
}
