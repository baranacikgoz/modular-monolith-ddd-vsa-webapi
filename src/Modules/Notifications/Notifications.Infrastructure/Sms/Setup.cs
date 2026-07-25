using System.Net.Http.Headers;
using System.Text;
using Common.Application.Options;
using Common.Infrastructure.Resiliency;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Notifications.Application.Sms;
using Notifications.Infrastructure.Sms.NetGsm;
using ZiggyCreatures.Caching.Fusion;

namespace Notifications.Infrastructure.Sms;

internal static class Setup
{
    private const long MaxResponseContentBufferBytes = 64 * 1024;

    public static IServiceCollection AddNotificationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var smsOptions = configuration.GetSection(nameof(SmsOptions)).Get<SmsOptions>()
            ?? throw new InvalidOperationException($"Configuration for {nameof(SmsOptions)} is null.");

        return smsOptions.Provider switch
        {
            SmsProvider.Dummy => services.AddSingleton<ISmsGateway, DummySmsGateway>(),
            SmsProvider.NetGsm => services.AddNetGsm(smsOptions),
            _ => throw new ArgumentOutOfRangeException(nameof(configuration), smsOptions.Provider, "Unknown SmsProvider.")
        };
    }

    private static IServiceCollection AddNetGsm(this IServiceCollection services, SmsOptions smsOptions)
    {
        services.AddResilientHttpClient<NetGsmSmsGateway, NetGsmSmsGateway>(
            httpClient =>
            {
                // Trailing slash is REQUIRED: HttpClient drops the last BaseAddress segment
                // when combining with a relative URI otherwise.
#pragma warning disable S1075
                httpClient.BaseAddress = new Uri(smsOptions.BaseUrl!.TrimEnd('/') + '/');
#pragma warning restore S1075
                httpClient.MaxResponseContentBufferSize = MaxResponseContentBufferBytes;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes($"{smsOptions.UserCode}:{smsOptions.Password}")));
            },
            resilience =>
            {
                resilience.AttemptTimeout.Timeout = TimeSpan.FromSeconds(smsOptions.AttemptTimeoutSeconds);
                resilience.TotalRequestTimeout.Timeout = TimeSpan.FromSeconds(smsOptions.TotalRequestTimeoutSeconds);
                resilience.Retry.MaxRetryAttempts = smsOptions.MaxRetryAttempts;
            });

        return services.AddSingleton<ISmsGateway>(sp => new ThrottledSmsGateway(
            sp.GetRequiredService<NetGsmSmsGateway>(),
            sp.GetRequiredService<IFusionCache>(),
            sp.GetRequiredService<IOptions<SmsOptions>>()));
    }
}
