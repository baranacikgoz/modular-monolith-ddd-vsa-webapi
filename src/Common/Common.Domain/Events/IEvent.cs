using NimbleMediator.Contracts;

namespace Common.Domain.Events;

public interface IEvent : INotification
{
    DateTime CreatedOn { get; }
}
