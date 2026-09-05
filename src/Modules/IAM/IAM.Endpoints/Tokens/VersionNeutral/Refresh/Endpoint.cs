using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Extensions;
using IAM.Application.Keycloak;
using IAM.Infrastructure.Telemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Constants = IAM.Infrastructure.RateLimiting.Constants;

namespace IAM.Endpoints.Tokens.VersionNeutral.Refresh;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder tokensApiGroup)
    {
        tokensApiGroup
            .MapPost("refresh", RefreshToken)
            .WithDescription("Rotate a refresh token. One retry of the same token is tolerated (lost response); replaying it again is treated as theft and the whole session is revoked.")
            .AllowAnonymous()
            .RequireRateLimiting(Constants.TokenRefresh)
            .Produces<Response>()
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> RefreshToken(
        Request request,
        IKeycloakTokenClient tokenClient,
        CancellationToken cancellationToken)
    {
        using var activity = IamTelemetry.ActivitySource.StartActivityForCaller();

        return await tokenClient
            .RefreshAsync(request.RefreshToken, cancellationToken)
            .TapAsync(tokens => activity?.SetTag("session.id", tokens.SessionId))
            .MapAsync(tokens => new Response
            {
                AccessToken = tokens.AccessToken,
                AccessTokenExpiresAt = tokens.AccessTokenExpiresAt,
                RefreshToken = tokens.RefreshToken,
                RefreshTokenExpiresAt = tokens.RefreshTokenExpiresAt
            })
            .TapActivityAsync(activity);
    }
}
