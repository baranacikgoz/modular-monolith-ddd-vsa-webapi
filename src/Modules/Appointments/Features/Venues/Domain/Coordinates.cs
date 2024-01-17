using Appointments.Features.Venues.Domain.Errors;
using Common.Core.Contracts;
using Common.Core.Contracts.Results;

namespace Appointments.Features.Venues.Domain;

public sealed class Coordinates : ValueObject
{
    private Coordinates(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public double Latitude { get; }
    public double Longitude { get; }

    public static Result<Coordinates> Create(double latitude, double longitude)
    {
        if (latitude < -90 || latitude > 90)
        {
            return CoordinatesErrors.LatitudeMustBeBetweenMinus90And90;
        }

        if (longitude < -180 || longitude > 180)
        {
            return CoordinatesErrors.LongitudeMustBeBetweenMinus180And180;
        }

        return new Coordinates(latitude, longitude);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Latitude;
        yield return Longitude;
    }

    // Orms need parameterless constructors
    private Coordinates() { }

}
