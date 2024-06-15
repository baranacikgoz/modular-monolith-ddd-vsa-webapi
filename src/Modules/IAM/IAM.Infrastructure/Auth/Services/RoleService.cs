using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAM.Application.Auth;
using IAM.Application.Auth.Services;
using IAM.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IAM.Infrastructure.Auth.Services;
public class RoleService(
    IAMDbContext dbContext
    ) : IRoleService
{
    public async Task<Guid?> GetRoleIdByName(string roleName, CancellationToken cancellationToken)
    {
        var roleId = await dbContext
                               .Roles
                               .Where(r => r.Name == CustomRoles.Basic)
                               .Select(r => r.Id)
                               .SingleOrDefaultAsync(cancellationToken);

        return roleId == default ? null : roleId.Value;
    }
}
