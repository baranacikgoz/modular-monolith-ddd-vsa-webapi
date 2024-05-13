using IdentityAndAuth.Application.Captcha.Services;
using IdentityAndAuth.Application.Captcha.Infrastructure;
using IdentityAndAuth.Infrastructure.Captcha.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Common.Infrastructure.Options;
using Common.Application.Caching;

namespace IdentityAndAuth.Infrastructure.Captcha;

internal static class Setup
{
    public static IServiceCollection AddCaptchaInfrastructure(this IServiceCollection services)
        => services
            .AddHttpClient<ReCaptchaService>((sp, httpClient) =>
                {
                    var captchaOptions = sp.GetRequiredService<IOptions<CaptchaOptions>>().Value;

                    httpClient.BaseAddress = new Uri(captchaOptions.CaptchaEndpoint);
                })
            .Services
            .AddSingleton<ICaptchaService, ReCaptchaService>()
            .AddSingleton<ICaptchaService, CachedCaptchaService>(
                sp =>
                {
                    var cacheCaptchaForMinutes = sp.GetRequiredService<IOptions<OtpOptions>>().Value.ExpirationInMinutes;

                    return new CachedCaptchaService(
                        sp.GetRequiredService<ICaptchaService>(),
                        sp.GetRequiredService<ICacheService>(),
                        cacheCaptchaForMinutes);
                });
}
