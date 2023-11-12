using Appointments.Features.Venues.Domain;

namespace Appointments.Features.Venues.Infrastructure;

internal sealed class DummyVenueService : IDummyVenueService
{
    public void DoNothing()
    {
        throw new NotImplementedException();
    }
}
