using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAM.Application.Auth.Services;
public interface IRoleService
{
    Task<Guid?> GetRoleIdByName(string roleName, CancellationToken cancellationToken);
}
