using Common.Core.Contracts.Results;
using Common.Events;
using IdentityAndAuth.Features.Auth.Domain;
using IdentityAndAuth.Features.Identity.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IdentityAndAuth.Persistence.Seeding;

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
        const string name = "Baran";
        const string surname = "Açıkgöz";
        const string nationalIdentityNumber = "11111111111";

        var admin = await SeedUser(phoneNumber, name, surname, nationalIdentityNumber, new(2001, 6, 20), LogSeedingAdmingUser);
        await SeedAdminToAllRolesAsync(admin);
    }

    private async Task SeedBasicUsersAsync()
    {

        const string phoneNumber = "901111111112";
        const string name = "John";
        const string surname = "Doe";
        const string nationalIdentityNumber = "11111111112";

        var basicUser = await SeedUser(phoneNumber, name, surname, nationalIdentityNumber, new(2001, 6, 20), LogSeedingBasicUser);
        await SeedBasicUserToBasicRoleAsync(basicUser);

        const string phoneNumber2 = "901111111113";
        const string name2 = "Michael";
        const string surname2 = "Scott";
        const string nationalIdentityNumber2 = "11111111113";

        basicUser = await SeedUser(phoneNumber2, name2, surname2, nationalIdentityNumber2, new(2001, 6, 20), LogSeedingBasicUser);
        await SeedBasicUserToBasicRoleAsync(basicUser);

        const string phoneNumber3 = "901111111114";
        const string name3 = "Pam";
        const string surname3 = "Beesly";
        const string nationalIdentityNumber3 = "11111111114";

        basicUser = await SeedUser(phoneNumber3, name3, surname3, nationalIdentityNumber3, new(2001, 6, 20), LogSeedingBasicUser);
        await SeedBasicUserToBasicRoleAsync(basicUser);

        const string phoneNumber4 = "901111111115";
        const string name4 = "Dwight";
        const string surname4 = "Schrute";
        const string nationalIdentityNumber4 = "11111111115";

        basicUser = await SeedUser(phoneNumber4, name4, surname4, nationalIdentityNumber4, new(2001, 6, 20), LogSeedingBasicUser);
        await SeedBasicUserToBasicRoleAsync(basicUser);

        const string phoneNumber5 = "901111111116";
        const string name5 = "Jim";
        const string surname5 = "Halpert";
        const string nationalIdentityNumber5 = "11111111116";

        basicUser = await SeedUser(phoneNumber5, name5, surname5, nationalIdentityNumber5, new(2001, 6, 20), LogSeedingBasicUser);
        await SeedBasicUserToBasicRoleAsync(basicUser);
    }

    private async Task<ApplicationUser> SeedUser(
        string phoneNumber,
        string name,
        string surname,
        string nationalIdentityNumber,
        DateOnly birthDate,
        Action<ILogger, string> logSeedingUser)
    {
        if (await userManager.Users.SingleOrDefaultAsync(x => x.PhoneNumber == phoneNumber) is ApplicationUser user)
        {
            return user;
        }

        logSeedingUser(logger, $"{name} {surname}");

        user = ApplicationUser.Create(
            name,
            surname,
            phoneNumber,
            nationalIdentityNumber,
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
                LogSeedingRoleToAdmin(logger, roleName!, FullName(admin));
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
            LogSeedingRoleToBasic(logger, basicRole.Name!, FullName(basicUser));
            await userManager.AddToRoleAsync(basicUser, basicRole.Name!);
        }
    }

    private static string FullName(ApplicationUser user)
        => $"{user.Name} {user.LastName}";

    [LoggerMessage(
            Level = LogLevel.Information,
            Message = "Seeding the admin {Name} user.")]
    private static partial void LogSeedingAdmingUser(ILogger logger, string name);

    [LoggerMessage(
            Level = LogLevel.Information,
            Message = "Seeding the admin {Name} user to {RoleName} role.")]
    private static partial void LogSeedingRoleToAdmin(ILogger logger, string roleName, string name);

    [LoggerMessage(
            Level = LogLevel.Information,
            Message = "Seeding the basic {Name} user.")]
    private static partial void LogSeedingBasicUser(ILogger logger, string name);

    [LoggerMessage(
            Level = LogLevel.Information,
            Message = "Seeding the basic {Name} user to {RoleName} role.")]
    private static partial void LogSeedingRoleToBasic(ILogger logger, string roleName, string name);

}
