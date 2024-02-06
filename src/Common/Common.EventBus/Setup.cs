using System.Reflection;
using Common.EventBus.Contracts;
using Common.Options;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NimbleMediator.NotificationPublishers;
using NimbleMediator.ServiceExtensions;
using Microsoft.Extensions.Hosting;

namespace Common.EventBus;

public static class Setup
{
    public static IServiceCollection AddEventBus(
        this IServiceCollection services,
        IWebHostEnvironment env,
        params Assembly[] assemblies)
    => services
        .AddSingleton<IEventBus, MassTransitEventBus>()
        .AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            foreach (var assembly in assemblies)
            {
                x.AddConsumers(assembly);
            }

            x.UsingRabbitMq((context, configurator) =>
            {
                var options = context.GetRequiredService<IOptions<MessageBrokerOptions>>().Value;
                var host = options.Host;
                var port = options.Port;
                var uri = new Uri($"rabbitmq://{host}:{port}");

                configurator.Host(uri, h =>
                {
                    h.Username(options.Username);
                    h.Password(options.Password);
                });

                configurator.ConfigureEndpoints(context);
            });
        });

}
