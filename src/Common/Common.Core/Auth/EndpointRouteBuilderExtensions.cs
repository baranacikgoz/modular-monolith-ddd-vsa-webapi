using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Common.Core.Auth;

public static class EndpointRouteBuilderExtensions
{
    public static RouteHandlerBuilder MustHavePermission(this RouteHandlerBuilder builder, string action, string resource)
    {
        return builder
                .RequireAuthorization(CustomPermission.NameFor(action, resource));
    }
}
