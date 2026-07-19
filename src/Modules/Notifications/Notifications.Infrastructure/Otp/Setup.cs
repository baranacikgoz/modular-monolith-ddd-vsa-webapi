using Common.Application.Extensions;
using Common.Application.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Application.Otp;
using Notifications.Application.Sms;
using Notifications.Infrastructure.Sms;

namespace Notifications.Infrastructure.Otp;

internal static class Setup
{
    public static IServiceCollection AddOtpServices(this IServiceCollection services, IConfiguration configuration)
    {
        // DummySmsGateway is a no-op: OTPs are generated and stored but never reach the user.
        // In Production that silently bricks every OTP flow (registration included), so fail fast
        // until a real ISmsGateway implementation exists and is registered here.
        if (services.IsProductionEnvironment())
        {
            throw new InvalidOperationException(
                $"{nameof(ISmsGateway)} is {nameof(DummySmsGateway)} (no-op) — OTPs would never reach users in Production. " +
                "Implement and register a real SMS gateway before deploying the Notifications module.");
        }

        var useRedis = configuration.GetSection(nameof(CachingOptions)).GetValue<bool>(nameof(CachingOptions.UseRedis));

        if (useRedis)
        {
            services.AddSingleton<IOtpService, RedisOtpService>();
        }
        else
        {
            services
                //.AddSingleton<IOtpService, OtpService>()
                .AddSingleton<IOtpService, DummyOtpService>();
        }

        return services.AddSingleton<ISmsGateway, DummySmsGateway>();
    }
}
