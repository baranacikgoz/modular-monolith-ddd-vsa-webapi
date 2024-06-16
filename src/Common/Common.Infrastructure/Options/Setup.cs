using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Options;

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
            .AddOptions<LoggingMonitoringOptions>()
            .Bind(configuration.GetSection(nameof(LoggingMonitoringOptions)))
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
            .Validate(o => o.Global?.QueueLimit >= 0, "Global.QueueLimit must be greater than or equal to 0.")
            .Validate(o => o.Sms?.Limit > 0, "Sms.Limit must be greater than 0.")
            .Validate(o => o.Sms?.PeriodInMs > 0, "Sms.Period must be greater than 0.")
            .Validate(o => o.Sms?.QueueLimit >= 0, "Sms.QueueLimit must be greater than or equal to 0.")
            .Validate(o => o.CreateStore?.Limit > 0, "CreateStore.Limit must be greater than 0.")
            .Validate(o => o.CreateStore?.PeriodInMs > 0, "CreateStore.Period must be greater than 0.")
            .Validate(o => o.CreateStore?.QueueLimit >= 0, "CreateStore.QueueLimit must be greater than or equal to 0.")
            .ValidateOnStart();

        services
            .AddOptions<OpenApiOptions>()
            .Bind(configuration.GetSection(nameof(OpenApiOptions)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddOptions<EventBusOptions>()
            .Bind(configuration.GetSection(nameof(EventBusOptions)))
            .ValidateDataAnnotations()
            .Validate(o => !(!o.UseInMemoryEventBus && o.MessageBrokerOptions is null), "Both UseInMemory false and MessageBrokerOptions are null. Use in memory or provide message broker options.")
            .ValidateOnStart();

        services
            .AddOptions<OutboxOptions>()
            .Bind(configuration.GetSection(nameof(OutboxOptions)))
            .ValidateDataAnnotations()
            .Validate(o => o.BackgroundJobPeriodInSeconds >= 0, "BackgroundJobPeriodInSeconds must be greater than 0.")
            .Validate(o => o.BatchSizePerExecution >= 0, "BatchSizePerExecution must be greater than 0.")
            .Validate(o => o.MaxFailCountBeforeSentToDeadLetter >= 0, "MaxFailCountBeforeSentToDeadLetter must be greater than 0.")
            .ValidateOnStart();

        return services;
    }
}
