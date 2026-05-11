using Common.Application.Auth;
using IAM.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IAM.Infrastructure.Persistence.Seeding;

internal partial class Seeder
{
    private async Task SeedUsersAsync()
    {
        await SeedAdminUserAsync();
        await SeedBasicUsersAsync();
    }

    private async Task SeedAdminUserAsync()
    {
        const string phoneNumber = "901111111111";
        const string fullName = "Baran Açıkgöz";

        var admin = await SeedUser(phoneNumber, fullName, new DateOnly(2001, 6, 20),
            LogSeedingAdminUser);
        await SeedAdminToAllRolesAsync(admin);
    }

    private async Task SeedBasicUsersAsync()
    {
        const string phoneNumber = "901111111112";
        const string fullName = "John Doe";

        var basicUser = await SeedUser(phoneNumber, fullName, new DateOnly(2001, 6, 20),
            LogSeedingBasicUser);
        await SeedBasicUserToBasicRoleAsync(basicUser);

        const string phoneNumber2 = "901111111113";
        const string fullName2 = "Michael Scott";

        basicUser = await SeedUser(phoneNumber2, fullName2, new DateOnly(2001, 6, 20),
            LogSeedingBasicUser);
        await SeedBasicUserToBasicRoleAsync(basicUser);

        const string phoneNumber3 = "901111111114";
        const string fullName3 = "Pam Beesly";

        basicUser = await SeedUser(phoneNumber3, fullName3, new DateOnly(2001, 6, 20),
            LogSeedingBasicUser);
        await SeedBasicUserToBasicRoleAsync(basicUser);

        const string phoneNumber4 = "901111111115";
        const string fullName4 = "Dwight Schrute";

        basicUser = await SeedUser(phoneNumber4, fullName4, new DateOnly(2001, 6, 20),
            LogSeedingBasicUser);
        await SeedBasicUserToBasicRoleAsync(basicUser);

        const string phoneNumber5 = "901111111116";
        const string fullName5 = "Jim Halpert";

        basicUser = await SeedUser(phoneNumber5, fullName5, new DateOnly(2001, 6, 20),
            LogSeedingBasicUser);
        await SeedBasicUserToBasicRoleAsync(basicUser);
    }

    private async Task<ApplicationUser> SeedUser(
        string phoneNumber,
        string fullName,
        DateOnly birthDate,
        Action<ILogger, string> logSeedingUser)
    {
        if (await userManager.Users.SingleOrDefaultAsync(x => x.PhoneNumber == phoneNumber) is ApplicationUser user)
        {
            return user;
        }

        logSeedingUser(logger, fullName);

        user = ApplicationUser.Create(
            fullName,
            phoneNumber,
            birthDate);

        await userManager.CreateAsync(user);
        return user;
    }

    private async Task SeedAdminToAllRolesAsync(ApplicationUser admin)
    {
        var roles = await roleManager.Roles.ToListAsync();

        foreach (var roleName in roles.Select(x => x.Name))
        {
            if (!await userManager.IsInRoleAsync(admin, roleName!))
            {
                LogSeedingRoleToAdmin(logger, roleName!, admin.FullName);
                await userManager.AddToRoleAsync(admin, roleName!);
            }
        }
    }

    private async Task SeedBasicUserToBasicRoleAsync(ApplicationUser basicUser)
    {
        var basicRole = await roleManager.Roles.SingleOrDefaultAsync(x => x.Name == CustomRoles.Basic)
                        ?? throw new InvalidOperationException("Basic role not found.");

        if (!await userManager.IsInRoleAsync(basicUser, basicRole.Name!))
        {
            LogSeedingRoleToBasic(logger, basicRole.Name!, basicUser.FullName);
            await userManager.AddToRoleAsync(basicUser, basicRole.Name!);
        }
    }

    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Seeding the admin {FullName} user.")]
    private static partial void LogSeedingAdminUser(ILogger logger, string fullName);

    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Seeding the admin {FullName} user to {RoleName} role.")]
    private static partial void LogSeedingRoleToAdmin(ILogger logger, string roleName, string fullName);

    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Seeding the basic {FullName} user.")]
    private static partial void LogSeedingBasicUser(ILogger logger, string fullName);

    [LoggerMessage(
        Level = LogLevel.Information,
        Message = "Seeding the basic {FullName} user to {RoleName} role.")]
    private static partial void LogSeedingRoleToBasic(ILogger logger, string roleName, string fullName);
}
