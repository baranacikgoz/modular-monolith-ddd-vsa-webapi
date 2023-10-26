using IdentityAndAuth.Features.Users.Current;
using IdentityAndAuth.Features;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityAndAuth.Features.Users;

public static class Setup
{
    public static RouteGroupBuilder MapUsersEndpoints(this RouteGroupBuilder rootGroup)
    {
        var usersApiGroup = rootGroup
            .MapGroup("/users")
            .WithTags("Users");

        SelfRegisterUser.MapEndpoint(usersApiGroup);
        GetUser.MapEndpoint(usersApiGroup);
        InitiatePhoneOwnershipProcess.MapEndpoint(usersApiGroup);
        ProvePhoneOwnership.MapEndpoint(usersApiGroup);

        var currentUserApiGroup = usersApiGroup
            .MapGroup("/current");

        GetCurrentUser.MapEndpoint(currentUserApiGroup);

        return rootGroup;
    }
}
