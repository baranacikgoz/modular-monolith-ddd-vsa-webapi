using Common.InterModuleRequests;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IdentityAndAuth;
using IdentityAndAuth.Features.Auth.Domain;
using IdentityAndAuth.Features.Identity.Domain;
using IdentityAndAuth.Persistence;
using MassTransit.Internals;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityAndAuth.IntermoduleRequestHandlers;

internal class GetFirstBasicUserIdRequestHandler(IdentityDbContext dbContext)
    : InterModuleRequestHandler<GetFirstBasicUserId.Request, GetFirstBasicUserId.Response>
{
    protected override async Task<GetFirstBasicUserId.Response> HandleAsync(GetFirstBasicUserId.Request request, CancellationToken cancellationToken)
    {
        var roleId = await dbContext
                            .Roles
                            .Where(r => r.Name == CustomRoles.Basic)
                            .Select(r => r.Id)
                            .SingleOrDefaultAsync(cancellationToken);

        var userId = await dbContext
                            .UserRoles
                            .Where(ur => ur.RoleId == roleId)
                            .Select(ur => ur.UserId)
                            .FirstOrDefaultAsync(cancellationToken);

        return new GetFirstBasicUserId.Response(userId);
    }
}
