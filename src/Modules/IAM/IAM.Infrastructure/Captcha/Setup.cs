using Common.Application.Caching;
using Common.Application.Options;
using IAM.Application.Captcha.Services;
using IAM.Infrastructure.Captcha.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IAM.Infrastructure.Captcha;

internal static class Setup
{
    public static IServiceCollection AddCaptchaInfrastructure(this IServiceCollection services)
    {
        return services
            .AddSingleton<ICaptchaService, ReCaptchaService>()
            .AddHttpClient<ICaptchaService, ReCaptchaService>((sp, httpClient) =>
            {
                var captchaOptions = sp.GetRequiredService<IOptions<CaptchaOptions>>().Value;

                httpClient.BaseAddress = new Uri(captchaOptions.BaseUrl);
            })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new SocketsHttpHandler { PooledConnectionLifetime = TimeSpan.FromMinutes(15) };
            })
            .SetHandlerLifetime(Timeout.InfiniteTimeSpan)
            .Services
            .Decorate<ICaptchaService>((decoree, sp) => new CachedCaptchaService(
                decoree,
                sp.GetRequiredService<ICacheService>(),
                sp.GetRequiredService<IOptions<OtpOptions>>()));
    }
}
