using Common.Domain.StronglyTypedIds;
using IAM.Application.Otp.Services;
using IAM.Infrastructure.Identity.Services;
using IAM.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using IAM.Infrastructure.Persistence;

namespace IAM.Infrastructure.Identity;

internal static class Setup
{
    public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services)
        => services
            //.AddSingleton<IOtpService, OtpService>()
            .AddSingleton<IOtpService, DummyOtpService>()
            .AddIdentity<ApplicationUser, IdentityRole<ApplicationUserId>>(options =>
            {
                options.SignIn.RequireConfirmedPhoneNumber = true;
                options.SignIn.RequireConfirmedAccount = true;

                // We use PhoneNumber as UserName
                options.User.AllowedUserNameCharacters = "0123456789";
            })
            .AddEntityFrameworkStores<IAMDbContext>()
            .AddErrorDescriber<LocalizedIdentityErrorDescriber>()
            .AddDefaultTokenProviders()
            .Services;
}
