using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace IdentityAndAuth.Application.Identity;

public static class Setup
{
    public static void MapIdentityEndpoints(this RouteGroupBuilder rootGroup)
    {
        var usersApiGroup = rootGroup
            .MapGroup("/users")
            .WithTags("Users");

        VersionNeutral.Users.SelfRegister.Endpoint.MapEndpoint(usersApiGroup);
        VersionNeutral.Users.Get.Endpoint.MapEndpoint(usersApiGroup);
        VersionNeutral.Users.InitiatePhoneOwnershipProcess.Endpoint.MapEndpoint(usersApiGroup);
        VersionNeutral.Users.ProvePhoneOwnership.Endpoint.MapEndpoint(usersApiGroup);
        VersionNeutral.Users.CheckExistenceWithEmail.Endpoint.MapEndpoint(usersApiGroup);

        var currentUserApiGroup = usersApiGroup
            .MapGroup("/current");

        VersionNeutral.Users.Current.Get.Endpoint.MapEndpoint(currentUserApiGroup);
    }
}
