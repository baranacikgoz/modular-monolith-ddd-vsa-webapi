using Common.Domain.StronglyTypedIds;
using IAM.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace IAM.Persistence.Seeding;

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
