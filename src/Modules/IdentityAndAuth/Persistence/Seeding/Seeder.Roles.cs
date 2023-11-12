using IdentityAndAuth.Features.Auth.Domain;
using IdentityAndAuth.Features.Identity.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
            LogRoleCreation(logger, roleName);

            // Create the role
            role = new ApplicationRole(roleName, $"{roleName} Role.");
            await roleManager.CreateAsync(role);
        }

        return role;
    }

    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Creating the {RoleName} role.")]
    private static partial void LogRoleCreation(ILogger logger, string roleName);
}
