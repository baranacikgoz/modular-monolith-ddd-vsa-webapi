using Common.Application.Auth;
using IAM.Application.Identity.Services;
using Microsoft.AspNetCore.Authorization;

namespace IAM.Infrastructure.Auth;

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
