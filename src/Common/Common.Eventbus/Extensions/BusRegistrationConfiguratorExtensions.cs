using System.Reflection;
using MassTransit;

namespace Common.Eventbus.Extensions;

public static class BusRegistrationConfiguratorExtensions
{
    public static void AddConsumersFromAssembly(this IBusRegistrationConfigurator configurator, Assembly assembly)
    {
        var consumerTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsAssignableTo(typeof(IConsumer)));

        foreach (var consumerType in consumerTypes)
        {
            configurator.AddConsumer(consumerType);
        }
    }
}
