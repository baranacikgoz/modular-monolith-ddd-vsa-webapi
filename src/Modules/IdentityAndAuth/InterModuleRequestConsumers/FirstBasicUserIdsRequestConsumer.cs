using Common.InterModuleRequests.IdentityAndAuth;
using IdentityAndAuth.Features.Auth.Domain;
using IdentityAndAuth.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace IdentityAndAuth.InterModuleRequestConsumers;

/// <summary>
/// First users are the seed users.
/// This query is usefull for seeding other modules with some basic seed users, where a userId is required.
/// </summary>
/// <param name="dbContext"></param>
public class FirstBasicUserIdsRequestConsumer(
    IdentityDbContext dbContext
    ) : IConsumer<FirstBasicUserIdsRequest>
{
    public async Task Consume(ConsumeContext<FirstBasicUserIdsRequest> context)
    {
        var userCount = context.Message.Count;

        var roleId = await dbContext
                               .Roles
                               .Where(r => r.Name == CustomRoles.Basic)
                               .Select(r => r.Id)
                               .SingleOrDefaultAsync(context.CancellationToken);

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
            .Take(userCount)
            .ToListAsync(context.CancellationToken);

        await context.RespondAsync(new FirstBasicUserIdsResponse(firstBasicUsers));
    }
}
