using System.Reflection;
using Common.Application.EventBus;
using Common.Infrastructure.Options;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.EventBus;

public static class Setup
{
    public static IServiceCollection AddCommonEventBus(
        this IServiceCollection services,
        IConfiguration configuration,
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

            var eventBusOptions = configuration.GetSection(nameof(EventBusOptions)).Get<EventBusOptions>()
                ?? throw new InvalidOperationException($"{nameof(EventBusOptions)} is null");

            var useInMemoryEventBus = eventBusOptions.UseInMemoryEventBus;

            if (useInMemoryEventBus)
            {
                x.UsingInMemory((context, configurator) =>
                {
                    configurator.ConfigureEndpoints(context);
                });
            }
            else
            {
                var messageBrokerOptions = eventBusOptions.MessageBrokerOptions
                    ?? throw new InvalidOperationException($"{nameof(useInMemoryEventBus)} is false but {nameof(MessageBrokerOptions)} is null.");

                x.UseAppropriateMessageBroker(messageBrokerOptions);
            }

        });

    private static void UseAppropriateMessageBroker(
        this IBusRegistrationConfigurator busRegistrationConfigurator,
        MessageBrokerOptions messageBrokerOptions)
    {
        switch (messageBrokerOptions.MessageBrokerType)
        {
            case MessageBrokerType.RabbitMQ:
                busRegistrationConfigurator.UsingRabbitMq((context, configurator) =>
                {

                    configurator.Host(messageBrokerOptions.Uri, h =>
                    {
                        h.Username(messageBrokerOptions.Username);
                        h.Password(messageBrokerOptions.Password);
                    });

                    configurator.ConfigureEndpoints(context);
                });
                break;
            case MessageBrokerType.Kafka:
                throw new NotImplementedException();
            case MessageBrokerType.AzureServiceBus:
                throw new NotImplementedException();
            case MessageBrokerType.AmazonSQS:
                throw new NotImplementedException();
            default:
                throw new InvalidOperationException($"No registration set for the message broker {nameof(messageBrokerOptions.MessageBrokerType)}.");
        }
    }

}
