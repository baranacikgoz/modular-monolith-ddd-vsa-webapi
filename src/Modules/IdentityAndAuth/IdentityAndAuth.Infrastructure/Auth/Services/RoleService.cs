using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityAndAuth.Application.Auth;
using IdentityAndAuth.Application.Auth.Services;
using IdentityAndAuth.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IdentityAndAuth.Infrastructure.Auth.Services;
public class RoleService(
    IdentityAndAuthDbContext dbContext
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
