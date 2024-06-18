using Common.Domain.Events;
using MassTransit;

namespace Common.Application.EventBus;

#pragma warning disable CA1711
public interface IEventHandler<in TEvent> : IConsumer<TEvent>
    where TEvent : class, IEvent
{
}

#pragma warning restore CA1711
