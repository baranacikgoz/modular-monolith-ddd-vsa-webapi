using Appointments.Features.Appointments.Domain.Errors;
using Common.Core.Contracts;
using Microsoft.Extensions.Localization;

namespace Appointments.ModuleSetup.ErrorLocalization;

public static class ErrorsAndLocalizations
{
    public static IEnumerable<KeyValuePair<string, Func<IStringLocalizer<IErrorLocalizer>, string>>> Get()
    {
        yield return new(nameof(AppointmentErrors.DummyErrorForTesting), (localizer) => localizer["DummyErrorForTesting"]);
    }
}
