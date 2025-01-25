using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace IAM.Endpoints.Users.VersionNeutral;

public static class Setup
{
    public static void MapUsersEndpoints(this RouteGroupBuilder rootGroup)
    {
        var usersApiGroup = rootGroup
            .MapGroup("/users")
            .WithTags("Users");

        SelfRegister.Endpoint.MapEndpoint(usersApiGroup);
        Get.Endpoint.MapEndpoint(usersApiGroup);
        CheckRegistration.Endpoint.MapEndpoint(usersApiGroup);
        Me.Get.Endpoint.MapEndpoint(usersApiGroup);
    }
}
