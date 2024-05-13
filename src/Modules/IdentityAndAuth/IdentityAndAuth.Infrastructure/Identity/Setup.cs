using Common.Domain.StronglyTypedIds;
using IdentityAndAuth.Application.Identity.Services;
using IdentityAndAuth.Domain.Identity;
using IdentityAndAuth.Infrastructure.Identity.Services;
using IdentityAndAuth.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityAndAuth.Infrastructure.Identity;

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
        services
            .AddIdentity<ApplicationUser, IdentityRole<ApplicationUserId>>(options =>
            {
                options.SignIn.RequireConfirmedPhoneNumber = true;
                options.SignIn.RequireConfirmedAccount = true;

                // We use PhoneNumber as UserName
                options.User.AllowedUserNameCharacters = "0123456789";
            })
            .AddEntityFrameworkStores<IdentityAndAuthDbContext>()
            .AddErrorDescriber<LocalizedIdentityErrorDescriber>()
            .AddDefaultTokenProviders();

        return services;
    }
}
