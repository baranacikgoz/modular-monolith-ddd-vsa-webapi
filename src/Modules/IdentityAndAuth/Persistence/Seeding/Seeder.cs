using IdentityAndAuth.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using IdentityAndAuth.Features.Users.Domain;

namespace IdentityAndAuth.Persistence.Seeding;

internal sealed partial class Seeder(ILogger<Seeder> logger, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
{
    public async Task SeedDb()
    {
        await SeedRolesAsync();
        await SeedAdminUserAsync();
    }
}
