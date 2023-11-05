using MassTransit;

namespace Common.Eventbus;

public interface IModuleEventBusConfigurator
{
    void Configure(IBusRegistrationConfigurator configurator);
}
