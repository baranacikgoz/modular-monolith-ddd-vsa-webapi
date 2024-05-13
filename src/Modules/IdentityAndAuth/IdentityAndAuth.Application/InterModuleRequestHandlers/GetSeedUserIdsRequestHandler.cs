using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IdentityAndAuth;
using IdentityAndAuth.Application.Auth;
using IdentityAndAuth.Application.Auth.Services;
using IdentityAndAuth.Application.Identity.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityAndAuth.Application.InterModuleRequestHandlers;

/// <summary>
/// This query is for seeding other modules requiring some basic seed users, where a userId is required.
/// </summary>
/// <param name="dbContext"></param>
public class GetSeedUserIdsRequestHandler(
    IRoleService roleService,
    IUserService userService
    ) : InterModuleRequestHandler<GetSeedUserIdsRequest, GetSeedUserIdsResponse>
{
    protected override async Task<GetSeedUserIdsResponse> HandleAsync(GetSeedUserIdsRequest request, CancellationToken cancellationToken)
    {
        var requestedUserCount = request.Count;
        var roleName = CustomRoles.Basic;

        var roleId = await roleService.GetRoleIdByName(roleName, cancellationToken)
            ?? throw new InvalidOperationException($"Role with the name {roleName} not found. This should not happen!");

        var seedUsers = await userService.GetSeedUserIdsByRoleId(roleId, requestedUserCount, cancellationToken);

        return new GetSeedUserIdsResponse(seedUsers);
    }
}
