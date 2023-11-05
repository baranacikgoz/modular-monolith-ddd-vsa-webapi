using Common.Core.Contracts;
using MassTransit;

namespace Common.Eventbus;

// Just "IConsumer" does not make sense in an event-driven architecture in my opinion.
// Additionaly, events should be Domain Events, we've forced this by using the "DomainEvent" base class.
public interface IEventConsumer<in TEvent> : IConsumer<TEvent>
    where TEvent : DomainEvent
{

}
