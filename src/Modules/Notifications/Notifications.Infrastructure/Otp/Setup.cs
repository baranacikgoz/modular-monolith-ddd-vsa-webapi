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
