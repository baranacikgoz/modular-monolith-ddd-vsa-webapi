using Common.Core.Contracts;

namespace Common.DomainEvents;

public static partial class Events
{
    public static class Appointments
    {
        public record AppointmentCreatedEvent(Guid AppointmentId) : DomainEvent;
    }
}
