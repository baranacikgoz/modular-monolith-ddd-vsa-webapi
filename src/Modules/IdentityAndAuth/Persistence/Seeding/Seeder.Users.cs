using Common.Core.Contracts.Results;
using IdentityAndAuth.Features.Auth.Domain;
using IdentityAndAuth.Features.Identity.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IdentityAndAuth.Persistence.Seeding;

internal partial class Seeder
{
    private static readonly DummyPhoneVerificationTokenService _dummyPhoneVerificationTokenService = new();

    private async Task SeedUsersAsync()
    {
        await SeedAdminUserAsync();
        await SeedBasicUsersAsync();
    }

    private async Task SeedAdminUserAsync()
    {
        const string phoneNumber = "905380718209";
        const string name = "Baran";
        const string surname = "Açıkgöz";
        const string nationalIdentityNumber = "12312312123";

        var admin = await SeedUser(phoneNumber, name, surname, nationalIdentityNumber, new(2001, 6, 20), LogSeedingAdmingUser);
        await SeedAdminToAllRolesAsync(admin);
    }

    private async Task SeedBasicUsersAsync()
    {
        const string phoneNumber = "903211235577";
        const string name = "John";
        const string surname = "Doe";
        const string nationalIdentityNumber = "12312312124";

        var basicUser = await SeedUser(phoneNumber, name, surname, nationalIdentityNumber, new(2001, 6, 20), LogSeedingBasicUser);
        await SeedBasicUserToBasicRoleAsync(basicUser);

        const string phoneNumber2 = "903211236688";
        const string name2 = "Michael";
        const string surname2 = "Scott";
        const string nationalIdentityNumber2 = "12312312125";

        basicUser = await SeedUser(phoneNumber2, name2, surname2, nationalIdentityNumber2, new(2001, 6, 20), LogSeedingBasicUser);
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

        var userCreationResult = await ApplicationUser.CreateAsync(
            new()
            {
                FirstName = name,
                LastName = surname,
                PhoneNumber = phoneNumber,
                NationalIdentityNumber = nationalIdentityNumber,
                BirthDate = birthDate
            },
            _dummyPhoneVerificationTokenService,
            "dummyCode",
            CancellationToken.None
            );

        user = userCreationResult.Value!;
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

    private sealed class DummyPhoneVerificationTokenService : IPhoneVerificationTokenService
    {
        public Task<string> GetTokenAsync(string phoneNumber, CancellationToken cancellationToken)
            => Task.FromResult("dummyCode");
        public Task<Result> ValidateTokenAsync(string phoneNumber, string token, CancellationToken cancellationToken)
            => Task.FromResult(Result.Success);
    }

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
