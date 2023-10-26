using MassTransit;

namespace Common.Eventbus;

public interface IModuleConfiguration
{
    string QueueName { get; }
    void Configure(IBusRegistrationContext context, IRabbitMqReceiveEndpointConfigurator endpoint);
}
