using Microsoft.AspNetCore.Builder;

namespace Common.Application.Auth;

public static class RouteHandlerBuilderExtensions
{
    public static RouteHandlerBuilder MustHavePermission(this RouteHandlerBuilder builder, string action, string resource)
        => builder.RequireAuthorization(CustomPermission.NameFor(action, resource));
}
