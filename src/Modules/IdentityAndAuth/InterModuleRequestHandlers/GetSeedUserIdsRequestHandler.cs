using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IdentityAndAuth;
using IdentityAndAuth.Features.Auth.Domain;
using IdentityAndAuth.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace IdentityAndAuth.InterModuleRequestConsumers;

/// <summary>
/// This query is for seeding other modules requiring some basic seed users, where a userId is required.
/// </summary>
/// <param name="dbContext"></param>
public class GetSeedUserIdsRequestHandler(
    IdentityDbContext dbContext
    ) : InterModuleRequestHandler<GetSeedUserIdsRequest, GetSeedUserIdsResponse>
{
    protected override async Task<GetSeedUserIdsResponse> HandleAsync(GetSeedUserIdsRequest request, CancellationToken cancellationToken)
    {
        var requestedUserCount = request.Count;

        var roleId = await dbContext
                               .Roles
                               .Where(r => r.Name == CustomRoles.Basic)
                               .Select(r => r.Id)
                               .SingleOrDefaultAsync(cancellationToken);

        var firstBasicUsers = await dbContext
            .Users
            .Join(
                dbContext.UserRoles,
                u => u.Id,
                ur => ur.UserId,
                (u, ur) => new { u, ur.RoleId }
            )
            .Where(uur => uur.RoleId == roleId)
            .OrderBy(uur => uur.u.CreatedOn)
            .Select(uur => uur.u.Id)
            .Take(requestedUserCount)
            .ToListAsync(cancellationToken);

        return new GetSeedUserIdsResponse(firstBasicUsers);
    }
}
