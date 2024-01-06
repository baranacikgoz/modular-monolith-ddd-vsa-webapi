using Common.Core.Contracts.Results;

namespace Appointments.Features.Venues.Domain.Errors;

public static class CoordinatesErrors
{
    public static Error LatitudeMustBeBetweenMinus90And90 { get; } = new(nameof(LatitudeMustBeBetweenMinus90And90));
    public static Error LongitudeMustBeBetweenMinus180And180 { get; } = new(nameof(LongitudeMustBeBetweenMinus180And180));
}
