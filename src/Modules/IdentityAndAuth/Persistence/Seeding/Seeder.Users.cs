using IdentityAndAuth.Features.Identity.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IdentityAndAuth.Persistence.Seeding;

internal partial class Seeder
{
    private async Task SeedAdminUserAsync()
    {
        const string baransPhoneNumber = "905380718209";

        if (await userManager.Users.SingleOrDefaultAsync(x => x.PhoneNumber == baransPhoneNumber) is ApplicationUser admin)
        {
            await SeedAdminToAllRolesAsync(admin);
            return;
        }

        const string adminName = "Baran";
        const string adminSurname = "Açıkgöz";

        admin = ApplicationUser.Create(
            new()
            {
                FirstName = adminName,
                LastName = adminSurname,
                PhoneNumber = baransPhoneNumber,
                NationalIdentityNumber = "15112312123",
                BirthDate = new DateOnly(2001, 06, 20)
            });

        LogSeedingAdmingUser(logger, admin.UserName!);

        await userManager.CreateAsync(admin);

        // Assign role to user
        await SeedAdminToAllRolesAsync(admin);
    }

    [LoggerMessage(
            Level = LogLevel.Information,
            Message = "Seeding the {UserName} user.")]
    private static partial void LogSeedingAdmingUser(ILogger logger, string userName);

    private async Task SeedAdminToAllRolesAsync(ApplicationUser admin)
    {
        var roles = await roleManager.Roles.ToListAsync();

        foreach (var roleName in roles.Select(x => x.Name))
        {
            if (!await userManager.IsInRoleAsync(admin, roleName!))
            {
                await userManager.AddToRoleAsync(admin, roleName!);
            }
        }
    }
}
