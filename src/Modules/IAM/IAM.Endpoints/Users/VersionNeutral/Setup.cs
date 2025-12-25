using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Endpoint = IAM.Endpoints.Users.VersionNeutral.SelfRegister.Endpoint;

namespace IAM.Endpoints.Users.VersionNeutral;

public static class Setup
{
    public static void MapUsersEndpoints(this RouteGroupBuilder rootGroup)
    {
        var usersApiGroup = rootGroup
            .MapGroup("/users")
            .WithTags("Users");

        Endpoint.MapEndpoint(usersApiGroup);
        Get.Endpoint.MapEndpoint(usersApiGroup);
        CheckRegistration.Endpoint.MapEndpoint(usersApiGroup);
        Me.Get.Endpoint.MapEndpoint(usersApiGroup);
    }
}
