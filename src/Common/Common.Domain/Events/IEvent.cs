namespace Common.Domain.Events;

public interface IEvent
{
    DateTimeOffset CreatedOn { get; set; } // It is better to re-assign it right before persisting to db, to set the exact same time with other events those are going to be persisted together.
}
