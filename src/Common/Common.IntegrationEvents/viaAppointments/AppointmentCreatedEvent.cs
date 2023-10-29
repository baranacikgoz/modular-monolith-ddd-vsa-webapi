using Common.Core.Contracts;

namespace Common.IntegrationEvents.viaAppointments;

public static partial class Events
{
    public static partial class Appointments
    {
        public record AppointmentCreatedEvent(Guid AppointmentId) : DomainEvent;
    }
}
