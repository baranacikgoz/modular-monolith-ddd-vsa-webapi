using Common.Core.Contracts;
using MassTransit;

namespace Common.EventBus.Contracts;

#pragma warning disable CA1711
public interface IEventHandler<in TEvent> : IConsumer<TEvent>
    where TEvent : class, IEvent
{
}

#pragma warning restore CA1711
