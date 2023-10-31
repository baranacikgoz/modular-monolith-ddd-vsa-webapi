using Appointments.Features.Appointments.Domain.Errors;
using Common.Core.Contracts;
using Microsoft.Extensions.Localization;

namespace Appointments;

public static class ErrorsToLocalize
{
    public static Dictionary<string, Func<IStringLocalizer<IErrorTranslator>, string>> GetErrorsAndMessages()
    {
        return new Dictionary<string, Func<IStringLocalizer<IErrorTranslator>, string>>
        {
            { nameof(AppointmentErrors.DummyErrorForTesting), (localizer) => localizer["DummyErrorForTesting"] }
        };
    }
}
