using IAM.Application.Auth;
using IAM.Application.Auth.Services;
using IAM.Application.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IAM.Infrastructure.Auth.Services;

public class RoleService(IIAMDbContext dbContext) : IRoleService
{
    public async Task<DefaultIdType?> GetRoleIdByName(string roleName, CancellationToken cancellationToken)
    {
        var roleId = await dbContext
            .Roles
            .Where(r => r.Name == CustomRoles.Basic)
            .Select(r => r.Id)
            .SingleOrDefaultAsync(cancellationToken);

        return roleId == default ? null : roleId.Value;
    }
}
