using NimbleMediator.Contracts;

namespace Common.Core.Interfaces;

public interface IEvent : INotification
{
    DateTime CreatedOn { get; }
}
