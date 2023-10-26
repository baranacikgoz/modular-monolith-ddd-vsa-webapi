using Common.Options;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Common.Eventbus;

public static class Setup
{
    public static IServiceCollection AddEventBus(this IServiceCollection services, IEnumerable<IModuleConfiguration> moduleConfigurations)
    {
        services.AddMassTransit(conf =>
        {
            conf.SetKebabCaseEndpointNameFormatter();

            conf.UsingRabbitMq((context, cfg) =>
            {
                var options = context.GetRequiredService<IOptions<RabbitMqOptions>>().Value;

                cfg.Host(options.Host, host =>
                {
                    host.Username(options.Username);
                    host.Password(options.Password);
                });

                foreach (var moduleConfiguration in moduleConfigurations)
                {
                    cfg.ReceiveEndpoint(moduleConfiguration.QueueName, endpoint =>
                    {
                        moduleConfiguration.Configure(context, endpoint);
                    });
                }
            });
        });

        return services;
    }
}
