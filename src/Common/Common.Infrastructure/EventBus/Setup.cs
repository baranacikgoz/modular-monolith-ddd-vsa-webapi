using System.Reflection;
using Common.Application.EventBus;
using Common.Application.Options;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Common.Infrastructure.EventBus;

public static class Setup
{
    public static IServiceCollection AddCommonEventBus(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment,
        params Assembly[] assemblies)
    {
        return services
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

                if (useInMemoryEventBus && environment.IsProduction())
                {
                    throw new InvalidOperationException(
                        "UseInMemoryEventBus=true is not allowed in Production. " +
                        "The InMemory transport has no persistence — a process crash between OutboxKafkaProcessor " +
                        "committing the Kafka offset and MassTransit dispatching the message will permanently lose domain events. " +
                        "Configure RabbitMQ via EventBusOptions:MessageBroker.");
                }

                if (useInMemoryEventBus)
                {
                    x.UsingInMemory((context, configurator) => { configurator.ConfigureEndpoints(context); });
                }
                else
                {
                    var messageBrokerOptions = eventBusOptions.MessageBroker
                                               ?? throw new InvalidOperationException(
                                                   $"{nameof(useInMemoryEventBus)} is false but {nameof(MessageBroker)} is null.");

                    x.UseAppropriateMessageBroker(messageBrokerOptions);
                }
            });
    }

    private static void UseAppropriateMessageBroker(
        this IBusRegistrationConfigurator busRegistrationConfigurator,
        MessageBroker messageBrokerOptions)
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
                throw new InvalidOperationException(
                    "Kafka is not a valid MassTransit transport in this architecture. " +
                    "Domain events reach MassTransit consumers via CDC (Debezium → Kafka → OutboxKafkaProcessor). " +
                    "Set UseInMemoryEventBus=true for development, or use RabbitMQ for production.");
            case MessageBrokerType.AzureServiceBus:
                throw new NotImplementedException();
            case MessageBrokerType.AmazonSQS:
                throw new NotImplementedException();
            default:
                throw new InvalidOperationException(
                    $"No registration set for the message broker {nameof(messageBrokerOptions.MessageBrokerType)}.");
        }
    }
}
