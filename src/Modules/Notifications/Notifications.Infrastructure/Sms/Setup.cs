using Common.Application.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Application;
using Notifications.Application.Sms;

namespace Notifications.Infrastructure.Sms;

internal static class Setup
{
    public static IServiceCollection AddNotificationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ISmsService, DummySmsService>();

        var provider = configuration.GetSection(nameof(SmsOptions)).GetValue<SmsProvider>(nameof(SmsOptions.Provider));

        return provider switch
        {
            SmsProvider.Dummy => services.AddSingleton<ISmsGateway, DummySmsGateway>(),
            // ponytail: no real gateway implemented yet; add one when Provider=Real is actually needed.
            SmsProvider.Real => throw new NotImplementedException(
                $"{nameof(SmsProvider)}.{nameof(SmsProvider.Real)} is configured but no real {nameof(ISmsGateway)} implementation exists yet."),
            _ => throw new ArgumentOutOfRangeException(nameof(configuration), provider, "Unknown SmsProvider.")
        };
    }
}
