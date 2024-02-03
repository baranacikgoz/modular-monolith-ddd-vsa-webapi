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

internal class GetFirstAdminUserIdRequestHandler(IdentityDbContext dbContext)
    : InterModuleRequestHandler<GetFirstAdminUserId.Request, GetFirstAdminUserId.Response>
{
    protected override async Task<GetFirstAdminUserId.Response> HandleAsync(GetFirstAdminUserId.Request request, CancellationToken cancellationToken)
    {
        var roleId = await dbContext
                            .Roles
                            .Where(r => r.Name == CustomRoles.SystemAdmin)
                            .Select(r => r.Id)
                            .SingleOrDefaultAsync(cancellationToken);

        var userId = await dbContext
                            .UserRoles
                            .Where(ur => ur.RoleId == roleId)
                            .Select(ur => ur.UserId)
                            .FirstOrDefaultAsync(cancellationToken);

        return new GetFirstAdminUserId.Response(userId);
    }
}
