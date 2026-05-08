namespace Common.Domain.Events;

public interface IEvent
{
    DefaultIdType Id { get; init; }

    DateTimeOffset CreatedOn { get; set; }
}
