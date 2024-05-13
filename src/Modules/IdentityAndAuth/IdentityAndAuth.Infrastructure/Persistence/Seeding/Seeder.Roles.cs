using Common.Domain.StronglyTypedIds;
using IdentityAndAuth.Application.Auth;
using Microsoft.AspNetCore.Identity;
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

    private async Task<IdentityRole<ApplicationUserId>> SeedRoleAsync(string roleName)
    {
        if (await roleManager.Roles.SingleOrDefaultAsync(r => r.Name == roleName)
                is not IdentityRole<ApplicationUserId> role)
        {
            LogRoleCreation(logger, roleName);

            // Create the role
            role = new IdentityRole<ApplicationUserId>(roleName) { Id = ApplicationUserId.New() };
            await roleManager.CreateAsync(role);
        }

        return role;
    }

    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Creating the {RoleName} role.")]
    private static partial void LogRoleCreation(ILogger logger, string roleName);
}
