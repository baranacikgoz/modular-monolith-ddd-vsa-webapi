using Common.Core.Contracts;

namespace Common.DomainEvents;

public static partial class Events
{
    public static partial class Published
    {
        public static partial class From
        {
            public static class Appointments
            {
                public record AppointmentCreated(Guid AppointmentId) : DomainEvent;
            }
        }
    }
}
