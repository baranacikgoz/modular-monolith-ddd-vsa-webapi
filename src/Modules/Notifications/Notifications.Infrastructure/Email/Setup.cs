using System.Net.Http.Headers;
using Common.Application.Options;
using Common.Infrastructure.Resiliency;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Notifications.Infrastructure.Otp;
using Notifications.Application.Email;
using Notifications.Infrastructure.Email.Brevo;
using ZiggyCreatures.Caching.Fusion;

namespace Notifications.Infrastructure.Email;

internal static class Setup
{
    private const long MaxResponseContentBufferBytes = 64 * 1024;

    public static IServiceCollection AddEmailServices(this IServiceCollection services, IConfiguration configuration)
    {
        var emailOptions = configuration.GetSection(nameof(EmailOptions)).Get<EmailOptions>()
            ?? throw new InvalidOperationException($"Configuration for {nameof(EmailOptions)} is null.");
        configuration.RequireOtpTemplateForDefaultCulture(() => emailOptions.Templates.Otp.Keys, nameof(EmailOptions));

        return emailOptions.Provider switch
        {
            EmailProvider.Dummy => services.AddSingleton<IEmailGateway, DummyEmailGateway>(),
            EmailProvider.Brevo => services.AddBrevo(emailOptions),
            _ => throw new ArgumentOutOfRangeException(nameof(configuration), emailOptions.Provider, "Unknown EmailProvider.")
        };
    }

    private static IServiceCollection AddBrevo(this IServiceCollection services, EmailOptions emailOptions)
    {
        services.AddResilientHttpClient<BrevoEmailGateway, BrevoEmailGateway>(
            httpClient =>
            {
                // Trailing slash is REQUIRED: HttpClient drops the last BaseAddress segment
                // when combining with a relative URI otherwise.
#pragma warning disable S1075
                httpClient.BaseAddress = new Uri(emailOptions.BaseUrl!.TrimEnd('/') + '/');
#pragma warning restore S1075
                httpClient.MaxResponseContentBufferSize = MaxResponseContentBufferBytes;
                httpClient.DefaultRequestHeaders.Add("api-key", emailOptions.ApiKey);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            },
            resilience =>
            {
                resilience.AttemptTimeout.Timeout = TimeSpan.FromSeconds(emailOptions.AttemptTimeoutSeconds);
                resilience.TotalRequestTimeout.Timeout = TimeSpan.FromSeconds(emailOptions.TotalRequestTimeoutSeconds);
                resilience.Retry.MaxRetryAttempts = emailOptions.MaxRetryAttempts!.Value;
            });

        // Transient, not singleton: a singleton would pin one typed HttpClient for the process lifetime
        // and defeat HttpClientFactory's handler rotation (stale DNS). The throttle itself is stateless,
        // its counters live in FusionCache.
        return services.AddTransient<IEmailGateway>(sp => new ThrottledEmailGateway(
            sp.GetRequiredService<BrevoEmailGateway>(),
            sp.GetRequiredService<IFusionCache>(),
            sp.GetRequiredService<IOptions<EmailOptions>>()));
    }
}
