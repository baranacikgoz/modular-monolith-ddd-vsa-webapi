using IdentityAndAuth.Features.Users.Services.Otp;
using IdentityAndAuth.Features.Users.Services.PhoneVerificationToken;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityAndAuth.Features.Users.Services;

public static class Setup
{
    public static IServiceCollection AddUsersServices(this IServiceCollection services)
        => services
#pragma warning disable S125
            // services.AddSingleton<IOtpService, OtpService>((sp) => new OtpService(
            //     sp.GetRequiredService<ICacheService>(),
            //     sp.GetRequiredKeyedService<ICacheKeyService>(ModuleConstants.ModuleName),
            //     sp.GetRequiredService<IOptions<OtpOptions>>()
            // ));
#pragma warning restore S125
            .AddSingleton<IOtpService, DummyOtpService>()
            .AddSingleton<IPhoneVerificationTokenService, DummyPhoneVerificationTokenService>()
            .AddScoped<IUserService, UserService>();
}
