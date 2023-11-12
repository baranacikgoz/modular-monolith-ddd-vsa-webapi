using Common.Caching;
using Common.Options;
using IdentityAndAuth.Features.Captcha.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IdentityAndAuth.Features.Captcha.Infrastructure;

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
