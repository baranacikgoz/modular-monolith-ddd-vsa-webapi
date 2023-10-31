using Appointments.Features.Appointments.Domain;
using Common.Core.Contracts;

namespace Appointments.Features.Venues.Domain;

public sealed record VenueId(Guid Value);
public class Venue : AggregateRoot<VenueId>
{
    private Venue(string name)
        : base(new(Guid.NewGuid()))
    {

        Name = name;
    }

    public string Name { get; private set; }
    private readonly List<Appointment> _appointments = new();
    public virtual IReadOnlyCollection<Appointment> Appointments => _appointments.AsReadOnly();

    public static Venue Create(string name)
    {
        return new(name.Trim());
    }

}
