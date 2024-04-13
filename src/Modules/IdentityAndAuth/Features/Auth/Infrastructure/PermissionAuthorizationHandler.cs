using Common.Core.Auth;
using IdentityAndAuth.Features.Identity.Domain;
using Microsoft.AspNetCore.Authorization;

namespace IdentityAndAuth.Features.Auth.Infrastructure;

internal class PermissionAuthorizationHandler(
    ICurrentUser currentUser,
    IUserService userService
) : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if (await userService.HasPermissionAsync(new(currentUser.Id), requirement.PermissionName, default))
        {
            context.Succeed(requirement);
        }
    }
}
