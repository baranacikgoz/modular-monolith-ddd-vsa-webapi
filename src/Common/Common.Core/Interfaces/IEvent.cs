using NimbleMediator.Contracts;

namespace Common.Core.Interfaces;

public interface IEvent : INotification
{
    Guid EventId { get; }
    DateTime CreatedOn { get; }
}
