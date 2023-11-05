using Common.Options;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Common.Eventbus;

public static class Setup
{
    public static IServiceCollection AddEventBus(
        this IServiceCollection services,
        MassTransitOptions massTransitOptions,
        IEnumerable<IModuleEventBusConfigurator> moduleEventBusConfigurators)
    {
        services.AddMassTransit(conf =>
        {
            // If you are planning to scale out the application,
            // this DbContext should be configured to connect to the same database for all application instances.
            // Read the comments in "UsingInMemory" method below as well.
            conf.AddEntityFrameworkOutbox<OutboxDbContext>(o =>
            {
                o.UsePostgres();
                o.UseBusOutbox();
                o.DuplicateDetectionWindow = TimeSpan.FromSeconds(massTransitOptions.DuplicateDetectionWindowInSeconds);
            });

            conf.SetKebabCaseEndpointNameFormatter();

            // Allow each module to configure their own consumers in a decoupled way.
            foreach (var configurator in moduleEventBusConfigurators)
            {
                configurator.Configure(conf);
            }

            // If you are planning to scale out the application,
            // you should use a distributed message broker like RabbitMQ.
            // Read the comments in "AddEntityFrameworkOutbox" method above as well.
            conf.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
