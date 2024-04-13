using NimbleMediator.Contracts;

namespace Common.Core.Contracts;

public interface IEvent : INotification
{
    DateTime CreatedOn { get; }
}
