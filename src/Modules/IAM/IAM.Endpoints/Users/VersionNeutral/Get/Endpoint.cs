using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using IAM.Application.Keycloak;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace IAM.Endpoints.Users.VersionNeutral.Get;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapGet("{id}", GetAsync)
            .WithDescription("Get a user by id.")
            .RequireScope(KeycloakScopes.Users.View)
            .Produces<Response>()
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> GetAsync(
        [AsParameters] Request request,
        IKeycloakAdminClient adminClient,
        CancellationToken cancellationToken)
    {
        return await adminClient
            .GetUserAsync(request.Id, cancellationToken)
            .MapAsync(user => new Response
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                BirthDate = user.BirthDate,
                Enabled = user.Enabled,
                CreatedOn = user.CreatedOn
            });
    }
}
