using IdentityAndAuth.Auth;
using IdentityAndAuth.Identity;
using IdentityAndAuth.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System.Linq;

namespace IdentityAndAuth.Persistence.Seeding;

internal partial class Seeder
{
    private async Task SeedRolesAsync()
    {
        foreach (var role in CustomRoles.All)
        {
            await SeedRoleAsync(role);
        }
    }

    private async Task<ApplicationRole> SeedRoleAsync(string roleName)
    {
        if (await roleManager.Roles.SingleOrDefaultAsync(r => r.Name == roleName)
                is not ApplicationRole role)
        {
            logger.LogInformation("Creating the {RoleName} role.", roleName);

            // Create the role
            role = new ApplicationRole(roleName, $"{roleName} Role.");
            await roleManager.CreateAsync(role);
        }

        return role;
    }
}
