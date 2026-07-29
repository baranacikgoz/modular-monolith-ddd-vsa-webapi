using Common.Application.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Application.Push;
using Notifications.Infrastructure.Push.Firebase;

namespace Notifications.Infrastructure.Push;

internal static class Setup
{
    public static IServiceCollection AddPushServices(this IServiceCollection services, IConfiguration configuration)
    {
        var pushOptions = configuration.GetSection(nameof(PushOptions)).Get<PushOptions>()
            ?? throw new InvalidOperationException($"Configuration for {nameof(PushOptions)} is null.");

        return pushOptions.Provider switch
        {
            PushProvider.Dummy => services.AddSingleton<IPushGateway, DummyPushGateway>(),
            PushProvider.Firebase => services.AddSingleton<IPushGateway, FirebasePushGateway>(),
            _ => throw new ArgumentOutOfRangeException(nameof(configuration), pushOptions.Provider, "Unknown PushProvider.")
        };
    }
}
