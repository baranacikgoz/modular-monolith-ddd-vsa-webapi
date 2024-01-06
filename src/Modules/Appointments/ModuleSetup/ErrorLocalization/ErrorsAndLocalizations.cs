using Appointments.Features.Appointments.Domain.Errors;
using Appointments.Features.Venues.Domain.Errors;
using Common.Core.Contracts;
using Common.Core.Interfaces;
using Microsoft.Extensions.Localization;

namespace Appointments.ModuleSetup.ErrorLocalization;

public static class ErrorsAndLocalizations
{
    public static IEnumerable<KeyValuePair<string, Func<IStringLocalizer<IErrorLocalizer>, string>>> Get()
    {
        yield return new(
            nameof(CoordinatesErrors.LatitudeMustBeBetweenMinus90And90),
            (localizer) => localizer["Enlem -90 ile 90 arasında olmalı."]);

        yield return new(
            nameof(CoordinatesErrors.LongitudeMustBeBetweenMinus180And180),
            (localizer) => localizer["Boylam -180 ile 180 arasında olmalı."]);
    }
}
