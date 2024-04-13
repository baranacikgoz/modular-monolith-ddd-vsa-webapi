using IdentityAndAuth.Features.Identity.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace IdentityAndAuth.Persistence.Seeding;

internal sealed partial class Seeder(
    ILogger<Seeder> logger,
    RoleManager<IdentityRole<ApplicationUserId>> roleManager,
    UserManager<ApplicationUser> userManager)
{

    public async Task SeedDbAsync()
    {
        await SeedRolesAsync();
        await SeedUsersAsync();
    }
}
