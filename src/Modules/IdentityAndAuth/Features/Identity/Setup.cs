using IdentityAndAuth.Features.Identity.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Endpoint = IdentityAndAuth.Features.Identity.UseCases.Users.SelfRegister.Endpoint;

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

        Endpoint.MapEndpoint(usersApiGroup);
        UseCases.Users.Get.Endpoint.MapEndpoint(usersApiGroup);
        UseCases.Users.InitiatePhoneOwnershipProcess.Endpoint.MapEndpoint(usersApiGroup);
        UseCases.Users.ProvePhoneOwnership.Endpoint.MapEndpoint(usersApiGroup);

        var currentUserApiGroup = usersApiGroup
            .MapGroup("/current");

        UseCases.Users.Current.Get.Endpoint.MapEndpoint(currentUserApiGroup);

        return rootGroup;
    }
}
