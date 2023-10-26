using Common.Options;
using IdentityAndAuth.Features.Users;
using IdentityAndAuth.Features.Users.Domain;
using IdentityAndAuth.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IdentityAndAuth.Identity;

internal static class Setup
{
    internal static IServiceCollection AddCustomIdentity(this IServiceCollection services)
    {
        services.AddDbContext<IdentityContext>((sp, options) =>
        {
            var connectionString = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value.ConnectionString;
            options
                .UseNpgsql(connectionString,
                    o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, ModuleConstants.ModuleName));
        });

        services
            .AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.SignIn.RequireConfirmedPhoneNumber = true;
                options.SignIn.RequireConfirmedAccount = true;

                // We use PhoneNumber as UserName
                options.User.AllowedUserNameCharacters = "0123456789";
            })
            .AddEntityFrameworkStores<IdentityContext>()
            .AddErrorDescriber<LocalizedIdentityErrorDescriber>()
            .AddDefaultTokenProviders();

        return services;
    }
}
