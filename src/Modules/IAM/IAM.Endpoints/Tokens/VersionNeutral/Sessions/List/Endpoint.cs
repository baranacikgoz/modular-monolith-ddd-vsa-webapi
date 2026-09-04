using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Notifications;
using IAM.Application.Keycloak;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace IAM.Endpoints.Tokens.VersionNeutral.Sessions.List;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder sessionsApiGroup)
    {
        sessionsApiGroup
            .MapGet("", ListSessions)
            .WithDescription("List the caller's active sessions (devices/apps currently signed in).")
            .RequireScope(KeycloakScopes.Sessions.ViewOwn)
            .Produces<IReadOnlyCollection<Response>>()
            .TransformResultTo<IReadOnlyCollection<Response>>();
    }

    private static async Task<Result<IReadOnlyCollection<Response>>> ListSessions(
        ICurrentUser currentUser,
        IKeycloakAdminClient adminClient,
        IInterModuleRequestClient<GetDeviceSessionsRequest, GetDeviceSessionsResponse> deviceClient,
        CancellationToken cancellationToken)
    {
        // Keycloak owns the sessions; the Notifications device registry only adds the device metadata
        // Keycloak never sees (client app, friendly name). Sessions without a registry row (e.g. staff who
        // signed in from a tool that skipped device binding) are still listed.
        var sessions = await adminClient.GetUserSessionsAsync(currentUser.Id, cancellationToken);
        var devices = await deviceClient.SendAsync(new GetDeviceSessionsRequest(currentUser.Id), cancellationToken);
        var devicesBySession = devices.Sessions.ToDictionary(d => d.SessionId, StringComparer.Ordinal);

        var response = sessions
            .OrderByDescending(s => s.LastAccessAt)
            .Select(s => new Response
            {
                Id = s.Id,
                ClientId = devicesBySession.GetValueOrDefault(s.Id)?.ClientId,
                DeviceName = devicesBySession.GetValueOrDefault(s.Id)?.DeviceName,
                IpAddress = s.IpAddress,
                StartedAt = s.StartedAt,
                LastAccessAt = s.LastAccessAt,
                IsCurrent = string.Equals(s.Id, currentUser.SessionId, StringComparison.Ordinal)
            })
            .ToList();

        return Result<IReadOnlyCollection<Response>>.Success(response);
    }
}
