using Common.Application.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Application.Otp;

namespace Notifications.Infrastructure.Otp;

internal static class Setup
{
    /// <summary>
    /// Picks the OTP store only. Whether the code is random or the fixed <see cref="OtpOptions.DummyCode"/>
    /// is decided by <see cref="OtpCodeGenerator"/> and is independent of the store.
    /// </summary>
    public static IServiceCollection AddOtpServices(this IServiceCollection services, IConfiguration configuration)
    {
        var useRedis = configuration.GetSection(nameof(CachingOptions)).GetValue<bool>(nameof(CachingOptions.UseRedis));

        return useRedis
            ? services.AddSingleton<IOtpService, RedisOtpService>()
            : services.AddSingleton<IOtpService, OtpService>();
    }
}
