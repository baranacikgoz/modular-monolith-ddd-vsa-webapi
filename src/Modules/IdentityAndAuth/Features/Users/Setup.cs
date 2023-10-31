using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using IdentityAndAuth.Features.Users.Services;

namespace IdentityAndAuth.Features.Users;

public static class Setup
{
    public static IServiceCollection AddUsersFeatures(this IServiceCollection services)
        => services
            .AddUsersServices();
    public static RouteGroupBuilder MapUsersEndpoints(this RouteGroupBuilder rootGroup)
    {
        var usersApiGroup = rootGroup
            .MapGroup("/users")
            .WithTags("Users");

        SelfRegister.Endpoint.MapEndpoint(usersApiGroup);
        Get.Endpoint.MapEndpoint(usersApiGroup);
        InitiatePhoneOwnershipProcess.Endpoint.MapEndpoint(usersApiGroup);
        ProvePhoneOwnership.Endpoint.MapEndpoint(usersApiGroup);

        var currentUserApiGroup = usersApiGroup
            .MapGroup("/current");

        Current.Get.Endpoint.MapEndpoint(currentUserApiGroup);

        return rootGroup;
    }
}
