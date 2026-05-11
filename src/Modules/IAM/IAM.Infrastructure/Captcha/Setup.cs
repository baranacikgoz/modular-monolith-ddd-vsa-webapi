using IAM.Application.Captcha.Services;
using IAM.Infrastructure.Captcha.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IAM.Infrastructure.Captcha;

public static class Setup
{
    public static IServiceCollection AddCaptchaInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.AddSingleton<ICaptchaService, DummyCaptchaService>();

        // services.AddResilientHttpClient<ICaptchaService, ReCaptchaService>(
        //     httpClient =>
#pragma warning disable S125
        //     {
#pragma warning restore S125
        //         httpClient.BaseAddress = new Uri(captchaOptions.BaseUrl);
        //     },
        //     resilience =>
        //     {
        //         // reCAPTCHA is a fast API — tighten per-attempt timeout
        //         resilience.AttemptTimeout.Timeout = TimeSpan.FromSeconds(5);
        //         resilience.TotalRequestTimeout.Timeout = TimeSpan.FromSeconds(15);
        //     })
        //     .Services
        //     .Decorate<ICaptchaService>((decoree, sp) => new CachedCaptchaService(
        //         decoree,
        //         sp.GetRequiredService<IFusionCache>(),
        //         sp.GetRequiredService<IOptions<OtpOptions>>()));
    }
}
