using Common.Application.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Endpoint = IAM.Endpoints.Users.VersionNeutral.SelfRegister.Endpoint;

namespace IAM.Endpoints.Users.VersionNeutral;

public static class Setup
{
    public static void MapUsersEndpoints(this RouteGroupBuilder rootGroup, UsernameSource usernameSource)
    {
        var usersApiGroup = rootGroup
            .MapGroup("/users")
            .WithTags("Users");

        if (usernameSource == UsernameSource.PhoneNumber)
        {
            Endpoint.MapEndpoint(usersApiGroup);
            CheckRegistration.Endpoint.MapEndpoint(usersApiGroup);
        }

        Get.Endpoint.MapEndpoint(usersApiGroup);
        Search.Endpoint.MapEndpoint(usersApiGroup);
        Me.Get.Endpoint.MapEndpoint(usersApiGroup);
    }
}
