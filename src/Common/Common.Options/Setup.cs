using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Options;

public static class Setup
{
    public static IServiceCollection AddCommonOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<ResxLocalizationOptions>()
            .Bind(configuration.GetSection(nameof(ResxLocalizationOptions)))
            .ValidateDataAnnotations()
            .Validate(o => o.SupportedCultures.Count > 0, "SupportedCultures must contain at least one culture.")
            .ValidateOnStart();

        services
            .AddOptions<DatabaseOptions>()
            .Bind(configuration.GetSection(nameof(DatabaseOptions)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddOptions<CustomLoggingOptions>()
            .Bind(configuration.GetSection(nameof(CustomLoggingOptions)))
            .ValidateDataAnnotations()
            .Validate(o => o.ResponseTimeThresholdInMs > 0, "ResponseTimeThresholdInMs must be greater than 0.")
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

        // Even though we won't be using RateLimitingOptions via IOptions<RateLimitingOptions> in our code,
        // (because we don't have an overload of the AddRateLimiter() that has ServiceProvider as parameter)
        // we are validating it here to make sure that the options are valid.
        // After this point, it is safe to use it like "Configuration.GetSection(nameof(RateLimitingOptions)).Get<RateLimitingOptions>();"
        services
            .AddOptions<CustomRateLimitingOptions>()
            .Bind(configuration.GetSection(nameof(CustomRateLimitingOptions)))
            .ValidateDataAnnotations()
            .Validate(o => o.Global?.Limit > 0, "Global.Limit must be greater than 0.")
            .Validate(o => o.Global?.PeriodInMs > 0, "Global.Period must be greater than 0.")
            .Validate(o => o.Sms?.Limit > 0, "Sms.Limit must be greater than 0.")
            .Validate(o => o.Sms?.PeriodInMs > 0, "Sms.Period must be greater than 0.")
            .Validate(o => o.SearchAppointments?.Limit > 0, "SearchAppointments.Limit must be greater than 0.")
            .Validate(o => o.SearchAppointments?.PeriodInMs > 0, "SearchAppointments.Period must be greater than 0.")
            .ValidateOnStart();

        services
            .AddOptions<MonitoringTracingOptions>()
            .Bind(configuration.GetSection(nameof(MonitoringTracingOptions)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddOptions<OpenApiOptions>()
            .Bind(configuration.GetSection(nameof(OpenApiOptions)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
}
