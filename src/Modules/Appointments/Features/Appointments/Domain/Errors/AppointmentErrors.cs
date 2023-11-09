using Common.Core.Contracts.Results;

namespace Appointments.Features.Appointments.Domain.Errors;

internal static class AppointmentErrors
{
    public static readonly Error DummyErrorForTesting = new(nameof(DummyErrorForTesting));
}
