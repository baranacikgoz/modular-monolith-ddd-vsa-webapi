using Appointments.Features.Appointments.Domain.Errors;
using Common.Core.Contracts;
using Microsoft.Extensions.Localization;

namespace Appointments.ModuleSetup.ErrorLocalization;

public static class ErrorsAndMessages
{
    public static Dictionary<string, Func<IStringLocalizer<IErrorTranslator>, string>> Get()
    {
        return new Dictionary<string, Func<IStringLocalizer<IErrorTranslator>, string>>
        {
            { nameof(AppointmentErrors.DummyErrorForTesting), (localizer) => localizer["DummyErrorForTesting"] }
        };
    }
}
