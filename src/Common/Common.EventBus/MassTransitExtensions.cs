using System.Reflection;
using MassTransit;

namespace Common.EventBus;

#pragma warning disable S6605
public static class MassTransitExtensions
{
    public static void AddConsumersFromAssembly(
        this IBusRegistrationConfigurator configurator,
        Assembly assembly)
    {
        var consumerTypes = assembly.GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract && type
                                                                .GetInterfaces()
                                                                .Any(i => i.IsGenericType
                                                                                &&
                                                                            i.GetGenericTypeDefinition() == typeof(IConsumer<>)));

        foreach (var consumerType in consumerTypes)
        {
            configurator.AddConsumer(consumerType);
        }
    }

    public static void AddRequestClientsFromAssembly(
        this IBusRegistrationConfigurator configurator,
        Assembly assembly)
    {
        var requestClientTypes = assembly.GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract && type
                                                                .GetInterfaces()
                                                                .Any(i => i.IsGenericType
                                                                                &&
                                                                            i.GetGenericTypeDefinition() == typeof(IRequestClient<>)));

        foreach (var requestClientType in requestClientTypes)
        {
            configurator.AddRequestClient(requestClientType);
        }
    }
}

#pragma warning restore S6605

