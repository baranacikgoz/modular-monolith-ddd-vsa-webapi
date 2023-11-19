using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Common.Core.Auth;

public static class RouteHandlerBuilderExtensions
{
    public static RouteHandlerBuilder MustHavePermission(this RouteHandlerBuilder builder, string action, string resource)
        => builder.RequireAuthorization(CustomPermission.NameFor(action, resource));
}
