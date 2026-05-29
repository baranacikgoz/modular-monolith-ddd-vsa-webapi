using Microsoft.Extensions.DependencyInjection;
using Notifications.Application.Otp;
using Notifications.Application.Sms;
using Notifications.Infrastructure.Sms;

namespace Notifications.Infrastructure.Otp;

internal static class Setup
{
    public static IServiceCollection AddOtpServices(this IServiceCollection services)
        => services
            //.AddSingleton<IOtpService, OtpService>()
            .AddSingleton<IOtpService, DummyOtpService>()
            .AddSingleton<ISmsGateway, DummySmsGateway>();
}
