using Appointments.Features.Venues.Domain;
using Common.Core.Contracts;
using Common.DomainEvents.viaAppointments;

namespace Appointments.Features.Appointments.Domain;

public sealed record AppointmentId(Guid Value);
public class Appointment : AggregateRoot<AppointmentId>
{
    private Appointment(VenueId venueId)
        : base(new(Guid.NewGuid()))
    {
        VenueId = venueId;
        AddDomainEvent(new Events.Appointments.AppointmentCreatedEvent(Id.Value));
    }

    public VenueId VenueId { get; }
    public virtual Venue? Venue { get; }

    public static Appointment Create(VenueId venueId)
    {
        return new(venueId);
    }
}
