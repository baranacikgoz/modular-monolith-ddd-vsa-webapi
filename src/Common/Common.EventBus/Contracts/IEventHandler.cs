using Common.Core.Contracts;
using NimbleMediator.Contracts;

namespace Common.EventBus.Contracts;

#pragma warning disable CA1711
public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : DomainEvent
{
}

#pragma warning restore CA1711
