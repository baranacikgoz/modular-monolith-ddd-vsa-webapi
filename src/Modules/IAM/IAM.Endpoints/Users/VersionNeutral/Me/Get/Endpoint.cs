using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using IAM.Application.Keycloak;
using IAM.Infrastructure.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace IAM.Endpoints.Users.VersionNeutral.Me.Get;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapGet("me", GetMeAsync)
            .WithDescription("Get the current user with their roles and effective permissions.")
            .RequireScope(KeycloakScopes.Users.ViewOwn)
            .Produces<Response>()
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> GetMeAsync(
        ICurrentUser currentUser,
        IKeycloakAdminClient adminClient,
        IKeycloakPermissionClient permissionClient,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var roles = currentUser.Roles.ToList();

        return await adminClient
            .GetUserAsync(currentUser.Id, cancellationToken)
            .CombineAsync(async _ =>
            {
                var accessToken = await AccessTokenReader.ReadAsync(httpContext);
                var granted = accessToken is null
                    ? []
                    : await permissionClient.ListPermissionsAsync(accessToken, cancellationToken);

                return Result<IReadOnlyCollection<string>>.Success(
                    granted.SelectMany(p => p.Scopes).Distinct(StringComparer.Ordinal).Order(StringComparer.Ordinal).ToList());
            })
            .MapAsync(pair => new Response
            {
                Id = pair.Item1.Id,
                Username = pair.Item1.Username,
                FirstName = pair.Item1.FirstName,
                LastName = pair.Item1.LastName,
                Email = pair.Item1.Email,
                PhoneNumber = pair.Item1.PhoneNumber,
                BirthDate = pair.Item1.BirthDate,
                CreatedOn = pair.Item1.CreatedOn,
                Roles = roles,
                Permissions = pair.Item2
            });
    }
}
