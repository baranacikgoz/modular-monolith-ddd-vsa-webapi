using Common.Core.Contracts;

namespace Common.DomainEvents;

public static partial class Events
{
    public static class FromAppointments
    {
        public record AppointmentCreatedEvent(Guid AppointmentId) : DomainEvent;
    }
}
