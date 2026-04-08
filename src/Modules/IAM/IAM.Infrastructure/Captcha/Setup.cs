using IAM.Application.Captcha.Services;
using IAM.Infrastructure.Captcha.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IAM.Infrastructure.Captcha;

public static class Setup
{
    public static IServiceCollection AddCaptchaInfrastructure(this IServiceCollection services)
    {
#pragma warning disable S125 // Left for later production use
        // return services
        //     .AddSingleton<ICaptchaService, ReCaptchaService>()
        //     .AddHttpClient<ICaptchaService, ReCaptchaService>((sp, httpClient) =>
        //     {
        //         var captchaOptions = sp.GetRequiredService<IOptions<CaptchaOptions>>().Value;
        //
        //         httpClient.BaseAddress = new Uri(captchaOptions.BaseUrl);
        //     })
        //     .ConfigurePrimaryHttpMessageHandler(() =>
        //     {
        //         return new SocketsHttpHandler { PooledConnectionLifetime = TimeSpan.FromMinutes(15) };
        //     })
        //     .SetHandlerLifetime(Timeout.InfiniteTimeSpan)
        //     .Services
        //     .Decorate<ICaptchaService>((decoree, sp) => new CachedCaptchaService(
        //         decoree,
        //         sp.GetRequiredService<ICacheService>(),
        //         sp.GetRequiredService<IOptions<OtpOptions>>()));
#pragma warning restore S125

        return services.AddSingleton<ICaptchaService, DummyCaptchaService>();
    }
}
