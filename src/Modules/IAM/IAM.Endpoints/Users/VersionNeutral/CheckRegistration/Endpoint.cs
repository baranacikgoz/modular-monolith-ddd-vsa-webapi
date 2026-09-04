using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using IAM.Application.Keycloak;
using IAM.Infrastructure.RateLimiting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace IAM.Endpoints.Users.VersionNeutral.CheckRegistration;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapGet("check-registration", IsRegisteredAsync)
            .WithDescription("Check if a user is registered by given phone number.")
            .AllowAnonymous()
            .RequireRateLimiting(Constants.CheckRegistration)
            .Produces<Response>()
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> IsRegisteredAsync(
        [AsParameters] Request request,
        IKeycloakAdminClient adminClient,
        CancellationToken cancellationToken)
    {
        // Phone users are registered with their digits-only number as the Keycloak username.
        var user = await adminClient.FindUserByUsernameAsync(request.PhoneNumber, cancellationToken);

        return new Response { IsRegistered = user is not null };
    }
}
