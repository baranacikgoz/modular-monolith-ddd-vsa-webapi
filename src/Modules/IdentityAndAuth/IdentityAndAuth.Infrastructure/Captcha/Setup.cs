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
            .AddSingleton<ICaptchaService, ReCaptchaService>()
            .AddHttpClient<ICaptchaService, ReCaptchaService>((sp, httpClient) =>
                {
                    var captchaOptions = sp.GetRequiredService<IOptions<CaptchaOptions>>().Value;

                    httpClient.BaseAddress = new Uri(captchaOptions.BaseUrl);
                })
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    return new SocketsHttpHandler()
                    {
                        PooledConnectionLifetime = TimeSpan.FromMinutes(15)
                    };
                })
                .SetHandlerLifetime(Timeout.InfiniteTimeSpan)
            .Services
            .Decorate<ICaptchaService>((decoree, sp) => new CachedCaptchaService(
                                                          decoree,
                                                          sp.GetRequiredService<ICacheService>(),
                                                          sp.GetRequiredService<IOptions<OtpOptions>>()));
}
