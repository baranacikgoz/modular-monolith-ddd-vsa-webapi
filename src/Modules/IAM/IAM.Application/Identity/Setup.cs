using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace IAM.Application.Identity;

public static class Setup
{
    public static void MapIdentityEndpoints(this RouteGroupBuilder rootGroup)
    {
        var usersApiGroup = rootGroup
            .MapGroup("/users")
            .WithTags("Users");

        VersionNeutral.Users.Register.Endpoint.MapEndpoint(usersApiGroup);
        VersionNeutral.Users.Get.Endpoint.MapEndpoint(usersApiGroup);
        VersionNeutral.Users.SendVerificationOtp.Endpoint.MapEndpoint(usersApiGroup);
        VersionNeutral.Users.VerifyOtp.Endpoint.MapEndpoint(usersApiGroup);
        VersionNeutral.Users.CheckRegistration.Endpoint.MapEndpoint(usersApiGroup);

        var currentUserApiGroup = usersApiGroup
            .MapGroup("/me");

        VersionNeutral.Users.Me.Get.Endpoint.MapEndpoint(currentUserApiGroup);
    }
}
