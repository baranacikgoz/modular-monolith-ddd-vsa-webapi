using System.Reflection;
using Common.Options;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NimbleMediator.NotificationPublishers;
using NimbleMediator.ServiceExtensions;

namespace Common.EventBus;

public static class Setup
{
    public static IServiceCollection AddEventBus(
        this IServiceCollection services,
        params Assembly[] assembliesToRegister)
        => services
            .AddSingleton<IEventBus, MassTransitEventBus>()
            .AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                foreach (var assembly in assembliesToRegister)
                {
                    x.AddConsumersFromAssembly(assembly);
                    x.AddRequestClientsFromAssembly(assembly);
                }

                x.UsingRabbitMq((context, configurator) =>
                {
                    var options = context.GetRequiredService<IOptions<MessageBrokerOptions>>().Value;
                    var host = options.Host;
                    var port = options.Port;
                    var uri = new Uri($"rabbitmq://{host}:{port}");

                    configurator.Host(uri, hostConfigurator =>
                    {
                        hostConfigurator.Username(options.Username);
                        hostConfigurator.Password(options.Password);
                    });

                    configurator.ConfigureEndpoints(context);
                });

            });

}
