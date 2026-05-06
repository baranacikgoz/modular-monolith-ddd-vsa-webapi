using Common.Application.Caching;
using Common.Application.Options;
using Common.Infrastructure.Resiliency;
using IAM.Application.Captcha.Services;
using IAM.Infrastructure.Captcha.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IAM.Infrastructure.Captcha;

public static class Setup
{
    public static IServiceCollection AddCaptchaInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var captchaOptions = configuration.GetSection(nameof(CaptchaOptions)).Get<CaptchaOptions>();

        if (captchaOptions is null || string.IsNullOrWhiteSpace(captchaOptions.BaseUrl))
        {
            // Fallback to dummy service for local dev / test when CaptchaOptions is not configured
            return services.AddSingleton<ICaptchaService, DummyCaptchaService>();
        }

        services.AddResilientHttpClient<ICaptchaService, DummyCaptchaService>(
            httpClient =>
            {
                httpClient.BaseAddress = new Uri(captchaOptions.BaseUrl);
            },
            resilience =>
            {
                // reCAPTCHA is a fast API — tighten per-attempt timeout
                resilience.AttemptTimeout.Timeout = TimeSpan.FromSeconds(5);
                resilience.TotalRequestTimeout.Timeout = TimeSpan.FromSeconds(15);
            })
            .Services
            .Decorate<ICaptchaService>((decoree, sp) => new CachedCaptchaService(
                decoree,
                sp.GetRequiredService<ICacheService>(),
                sp.GetRequiredService<IOptions<OtpOptions>>()));

        return services;
    }
}
