using Common.Core.Auth;
using IdentityAndAuth.Features.Users.Services;
using Microsoft.AspNetCore.Authorization;

namespace IdentityAndAuth.Auth;

internal class PermissionAuthorizationHandler(
    ICurrentUser currentUser,
    IUserService userService
) : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if (await userService.HasPermissionAsync(currentUser.Id, requirement.PermissionName, default))
        {
            context.Succeed(requirement);
        }
    }
}
