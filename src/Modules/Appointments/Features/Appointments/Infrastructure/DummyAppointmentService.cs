using Appointments.Features.Appointments.Domain;

namespace Appointments.Features.Appointments.Infrastructure;

internal sealed class DummyAppointmentService : IDummyAppointmentService
{
    public void DoNothing()
    {
        throw new NotImplementedException();
    }
}
