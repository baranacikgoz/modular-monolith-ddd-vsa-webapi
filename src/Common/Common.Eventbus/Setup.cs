using Common.Options;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Common.Eventbus;

public static class Setup
{
    public static IServiceCollection AddEventBus(this IServiceCollection services, IEnumerable<IModuleEventBusConfigurator> moduleConfigurations)
    {
        services.AddMassTransit(conf =>
        {
            conf.SetKebabCaseEndpointNameFormatter();

        });

        return services;
    }
}
