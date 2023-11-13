using IdentityAndAuth.Features.Identity.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityAndAuth.Features.Identity;

internal static class Setup
{
    public static IServiceCollection AddIdentityFeature(this IServiceCollection services)
        => services
            .AddIdentityInfrastructure();
    public static RouteGroupBuilder MapUsersEndpoints(this RouteGroupBuilder rootGroup)
    {
        var usersApiGroup = rootGroup
            .MapGroup("/users")
            .WithTags("Users");

        UseCases.Users.SelfRegister.Endpoint.MapEndpoint(usersApiGroup);
        UseCases.Users.Get.Endpoint.MapEndpoint(usersApiGroup);
        UseCases.Users.InitiatePhoneOwnershipProcess.Endpoint.MapEndpoint(usersApiGroup);
        UseCases.Users.ProvePhoneOwnership.Endpoint.MapEndpoint(usersApiGroup);

        var currentUserApiGroup = usersApiGroup
            .MapGroup("/current");

        UseCases.Users.Current.Get.Endpoint.MapEndpoint(currentUserApiGroup);

        return rootGroup;
    }
}
