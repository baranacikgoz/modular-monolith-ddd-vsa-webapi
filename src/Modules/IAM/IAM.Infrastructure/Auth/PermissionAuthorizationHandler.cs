using Common.Application.Auth;
using Microsoft.AspNetCore.Authorization;

namespace IAM.Infrastructure.Auth;

internal class PermissionAuthorizationHandler(ICurrentUser currentUser) : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var permissionName = requirement.PermissionName;

        if (currentUser.HasPermission(permissionName))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
