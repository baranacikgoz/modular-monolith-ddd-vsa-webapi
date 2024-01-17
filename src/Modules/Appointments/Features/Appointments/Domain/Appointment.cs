using Appointments.Features.Venues.Domain;
using Common.Core.Contracts;
using Common.DomainEvents;

namespace Appointments.Features.Appointments.Domain;

public sealed record AppointmentId(Guid Value);
internal class Appointment : AggregateRoot<AppointmentId>
{
    protected Appointment(VenueId venueId, Guid userId)
        : base(new(Guid.NewGuid()))
    {
        VenueId = venueId;
        UserId = userId;
        State = AppointmentState.Scheduled;
    }

    public Guid UserId { get; private set; }
    public AppointmentState State { get; private set; }
    public VenueId VenueId { get; private set; }
    public virtual Venue? Venue { get; private set; }

    public static Appointment Create(Venue venue, Guid userId)
    {
        var appointment = new Appointment(venue.Id, userId);

        appointment
            .AddDomainEvent(new Events
                                .Published
                                .From
                                .Appointments
                                .AppointmentCreated(appointment.Id.Value));

        return appointment;
    }

    // Orms need parameterless constructors
#pragma warning disable CS8618
    private Appointment() : base(new(Guid.NewGuid())) { }
#pragma warning restore CS8618

}
