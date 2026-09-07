using Microsoft.AspNetCore.Builder;

namespace Common.Application.Auth;

public static class RouteHandlerBuilderExtensions
{
    /// <summary>
    ///     Requires the caller to hold <paramref name="scope" /> (a <see cref="KeycloakScopes" /> value such as
    ///     <c>stores:create-own</c>) according to Keycloak Authorization Services. The resource is the scope's
    ///     prefix. Evaluated by the IAM module's permission policy provider.
    /// </summary>
    public static RouteHandlerBuilder RequireScope(this RouteHandlerBuilder builder, string scope)
    {
        return builder.RequireAuthorization(KeycloakPermission.FromScope(scope).PolicyName());
    }
}
