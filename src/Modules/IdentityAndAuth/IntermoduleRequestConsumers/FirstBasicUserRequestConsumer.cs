using Common.InterModuleRequests;
using Common.InterModuleRequests.IdentityAndAuth;
using IdentityAndAuth.Features.Auth.Domain;
using IdentityAndAuth.Features.Identity.Domain;
using IdentityAndAuth.Persistence;
using MassTransit;
using MassTransit.Internals;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityAndAuth.IntermoduleRequestHandlers;

public class FirstBasicUserIdRequestConsumer(
    IdentityDbContext dbContext
    ) : IConsumer<FirstBasicUserIdRequest>
{
    public async Task Consume(ConsumeContext<FirstBasicUserIdRequest> context)
    {
        var roleId = await dbContext
                               .Roles
                               .Where(r => r.Name == CustomRoles.Basic)
                               .Select(r => r.Id)
                               .SingleOrDefaultAsync(context.CancellationToken);

        var userId = await dbContext
                            .UserRoles
                            .Where(ur => ur.RoleId == roleId)
                            .Select(ur => ur.UserId)
                            .FirstOrDefaultAsync(context.CancellationToken);

        await context.RespondAsync(new FirstBasicUserIdResponse(userId));
    }
}
