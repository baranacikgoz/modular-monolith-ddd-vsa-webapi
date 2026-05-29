using Common.Domain.StronglyTypedIds;
using IAM.Domain.Identity;
using IAM.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace IAM.Infrastructure.Identity;

public static class Setup
{
    public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services)
    {
        return services
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
}
