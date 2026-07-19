using Common.Application.Options;
using Common.Infrastructure.Resiliency;
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
        var captchaOptions = configuration.GetSection(nameof(CaptchaOptions)).Get<CaptchaOptions>()
            ?? throw new InvalidOperationException($"Configuration for {nameof(CaptchaOptions)} is null.");

        if (string.Equals(captchaOptions.Provider, "Dummy", StringComparison.OrdinalIgnoreCase))
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (string.Equals(environment, "Production", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "CaptchaOptions.Provider is 'Dummy' in Production. Dummy captcha always passes; " +
                    "set Provider to 'ReCaptcha' (with real keys) before deploying.");
            }

            return services.AddSingleton<ICaptchaService, DummyCaptchaService>();
        }

        services.AddResilientHttpClient<ICaptchaService, ReCaptchaService>(
            httpClient =>
            {
                // Trailing slash is REQUIRED: HttpClient drops the last BaseAddress segment
                // when combining with a relative URI otherwise.
#pragma warning disable S1075
                httpClient.BaseAddress = new Uri(captchaOptions.BaseUrl.TrimEnd('/') + '/');
#pragma warning restore S1075
            },
            resilience =>
            {
                // reCAPTCHA is a fast API — tighten timeouts.
                resilience.AttemptTimeout.Timeout = TimeSpan.FromSeconds(captchaOptions.AttemptTimeoutSeconds);
                resilience.TotalRequestTimeout.Timeout =
                    TimeSpan.FromSeconds(captchaOptions.TotalRequestTimeoutSeconds);
            });

        return services;
    }
}
