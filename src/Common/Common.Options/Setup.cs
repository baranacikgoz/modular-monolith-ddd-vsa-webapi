using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Options;

public static class Setup
{
    public static IServiceCollection AddCommonOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<RabbitMqOptions>()
            .Bind(configuration.GetSection(nameof(RabbitMqOptions)))
            .ValidateDataAnnotations()
            .Validate(o => o.Port > 0, "Port must be greater than 0.")
            .ValidateOnStart();

        services
            .AddOptions<CustomLocalizationOptions>()
            .Bind(configuration.GetSection(nameof(CustomLocalizationOptions)))
            .ValidateDataAnnotations()
            .Validate(o => o.SupportedCultures.Count > 0, "SupportedCultures must contain at least one culture.")
            .ValidateOnStart();

        services
            .AddOptions<DatabaseOptions>()
            .Bind(configuration.GetSection(nameof(DatabaseOptions)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddOptions<LoggerOptions>()
            .Bind(configuration.GetSection(nameof(LoggerOptions)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddOptions<JwtOptions>()
            .Bind(configuration.GetSection(nameof(JwtOptions)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddOptions<OtpOptions>()
            .Bind(configuration.GetSection(nameof(OtpOptions)))
            .Validate(o => o.ExpirationInMinutes > 0, "ExpirationInMinutes must be greater than 0.")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddOptions<CaptchaOptions>()
            .Bind(configuration.GetSection(nameof(CaptchaOptions)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
}
