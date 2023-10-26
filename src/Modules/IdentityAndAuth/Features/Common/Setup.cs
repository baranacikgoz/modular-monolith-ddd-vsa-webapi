using Common.Caching;
using Common.Options;
using IdentityAndAuth.Features.Captcha;
using IdentityAndAuth.Features.Users.Services;
using IdentityAndAuth.Features.Users.Services.Otp;
using IdentityAndAuth.Features.Users.Services.PhoneVerificationToken;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IdentityAndAuth.Features.Common;

internal static class Setup
{
    internal static IServiceCollection AddCommonFeatures(this IServiceCollection services)
    {

#pragma warning disable S125
        // services.AddSingleton<IOtpService, OtpService>((sp) => new OtpService(
        //     sp.GetRequiredService<ICacheService>(),
        //     sp.GetRequiredKeyedService<ICacheKeyService>(ModuleConstants.ModuleName),
        //     sp.GetRequiredService<IOptions<OtpOptions>>()
        // ));
#pragma warning restore S125

        services.AddSingleton<IOtpService, DummyOtpService>();
        services.AddSingleton<IPhoneVerificationTokenService, DummyPhoneVerificationTokenService>();

        services.AddScoped<IUserService, UserService>();

        services.AddHttpClient<ReCaptchaService>((sp, httpClient) =>
        {
            var captchaOptions = sp.GetRequiredService<IOptions<CaptchaOptions>>().Value;

            httpClient.BaseAddress = new Uri(captchaOptions.CaptchaEndpoint);
        });

        services.AddSingleton<ICaptchaService, ReCaptchaService>();
        services.AddSingleton<ICaptchaService, CachedCaptchaService>(
            sp =>
            {
                var cacheCaptchaForMinutes = sp.GetRequiredService<IOptions<OtpOptions>>().Value.ExpirationInMinutes;

                return new CachedCaptchaService(
                    sp.GetRequiredService<ICaptchaService>(),
                    sp.GetRequiredService<ICacheService>(),
                    cacheCaptchaForMinutes);
            });

        return services;
    }
}
