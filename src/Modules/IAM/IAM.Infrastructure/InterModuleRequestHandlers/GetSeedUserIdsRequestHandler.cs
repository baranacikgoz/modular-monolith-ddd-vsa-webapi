using Common.Application.Auth;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using IAM.Application.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IAM.Infrastructure.InterModuleRequestHandlers;

/// <summary>
///     This query is for seeding other modules requiring some basic seed users, where a userId is required.
/// </summary>
public class GetSeedUserIdsRequestHandler(IIAMDbContext dbContext)
    : InterModuleRequestHandler<GetSeedUserIdsRequest, GetSeedUserIdsResponse>
{
    public override async Task<GetSeedUserIdsResponse> HandleAsync(
        GetSeedUserIdsRequest request, CancellationToken cancellationToken)
    {
        var requestedUserCount = request.Count;
        var roleName = CustomRoles.Basic;

        var roleId = await dbContext
            .Roles
            .Where(r => r.Name == roleName)
            .Select(r => r.Id)
            .FirstOrDefaultAsync(cancellationToken);

        var userIds = await dbContext.UserRoles
            .Where(ur => ur.RoleId == roleId)
            .OrderBy(ur => ur.UserId)
            .Select(ur => ur.UserId)
            .Take(requestedUserCount)
            .ToListAsync(cancellationToken);

        return new GetSeedUserIdsResponse(userIds);
    }
}
