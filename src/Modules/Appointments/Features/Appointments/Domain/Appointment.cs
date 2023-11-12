using Appointments.Features.Venues.Domain;
using Common.Core.Contracts;
using Common.DomainEvents;

namespace Appointments.Features.Appointments.Domain;

public sealed record AppointmentId(Guid Value);
internal class Appointment : AggregateRoot<AppointmentId>
{
    private Appointment(VenueId venueId)
        : base(new(Guid.NewGuid()))
    {
        VenueId = venueId;
    }

    public VenueId VenueId { get; }
    public virtual Venue? Venue { get; }

    public static Appointment Create(VenueId venueId)
    {
        var appointment = new Appointment(venueId);
        appointment.AddDomainEvent(new Events
                                        .FromAppointments
                                        .AppointmentCreatedEvent(appointment.VenueId.Value));
        return appointment;
    }
}
