using Common.Application.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Endpoint = IAM.Endpoints.Users.VersionNeutral.SelfRegister.Endpoint;

namespace IAM.Endpoints.Users.VersionNeutral;

public static class Setup
{
    public static void MapUsersEndpoints(this RouteGroupBuilder rootGroup, IdentityScheme identityScheme)
    {
        var usersApiGroup = rootGroup
            .MapGroup("/users")
            .WithTags("Users");

        // Self-service registration is scheme-exclusive: the phone variant (plus its "am I registered"
        // pre-check) or the email variant, never both. Everything below is scheme-independent.
        if (identityScheme == IdentityScheme.PhoneNumber)
        {
            Endpoint.MapEndpoint(usersApiGroup);
            CheckRegistration.Endpoint.MapEndpoint(usersApiGroup);
        }
        else
        {
            SelfRegisterByEmail.Endpoint.MapEndpoint(usersApiGroup);
        }

        Get.Endpoint.MapEndpoint(usersApiGroup);
        Search.Endpoint.MapEndpoint(usersApiGroup);
        Me.Get.Endpoint.MapEndpoint(usersApiGroup);
    }
}
