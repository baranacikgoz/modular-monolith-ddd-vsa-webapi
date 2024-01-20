using Appointments.Features.Appointments.Domain;
using Common.Core.Contracts;

namespace Appointments.Features.Venues.Domain;

internal record struct VenueId(Guid Value);
internal class Venue : AggregateRoot<VenueId>
{
    private Venue(string name, Coordinates coordinates)
        : base(new(Guid.NewGuid()))
    {
        Name = name;
        Coordinates = coordinates;
    }

    public string Name { get; private set; }
    public Coordinates Coordinates { get; private set; }
    private readonly List<Appointment> _appointments = [];
    public virtual IReadOnlyCollection<Appointment> Appointments => _appointments.AsReadOnly();

    public static Venue Create(string name, Coordinates coordinates)
    {
        return new(name.Trim(), coordinates);
    }

    // Orms need parameterless constructors
#pragma warning disable CS8618
    private Venue() : base(default) { }
#pragma warning restore CS8618
}
