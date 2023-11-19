using Common.Options;
using IdentityAndAuth.Features.Identity.Domain;
using IdentityAndAuth.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IdentityAndAuth.Features.Identity.Infrastructure;

internal static class Setup
{
    public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services)
        => services
#pragma warning disable S125
            // services.AddSingleton<IOtpService, OtpService>((sp) => new OtpService(
            //     sp.GetRequiredService<ICacheService>(),
            //     sp.GetRequiredKeyedService<ICacheKeyService>(ModuleConstants.ModuleName),
            //     sp.GetRequiredService<IOptions<OtpOptions>>()
            // ));
#pragma warning restore S125
            .AddSingleton<IOtpService, DummyOtpService>()
            .AddSingleton<IPhoneVerificationTokenService, PhoneVerificationTokenService>()
            .AddScoped<IUserService, UserService>()
            .AddCustomIdentity();

    private static IServiceCollection AddCustomIdentity(this IServiceCollection services)
    {
        services.AddDbContext<IdentityContext>((sp, options) =>
        {
            var connectionString = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value.ConnectionString;
            options
                .UseNpgsql(connectionString,
                    o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, nameof(IdentityAndAuth)));
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
