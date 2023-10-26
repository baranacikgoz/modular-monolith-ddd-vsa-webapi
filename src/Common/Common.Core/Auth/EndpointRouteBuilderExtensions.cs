using Microsoft.AspNetCore.Builder;

namespace Common.Core.Auth;

public static class EndpointRouteBuilderExtensions
{
    public static IEndpointConventionBuilder MustHavePermission(this IEndpointConventionBuilder builder, string action, string resource)
    {
        return builder
                .RequireAuthorization(CustomPermission.NameFor(action, resource));
    }
}
