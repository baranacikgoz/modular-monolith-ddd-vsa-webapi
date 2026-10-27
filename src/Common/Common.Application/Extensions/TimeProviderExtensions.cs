namespace Common.Application.Extensions;

/// <summary>The UTC bounds [FromUtc, ToUtc) of one or more calendar days, so a caller gets one interpretable type.</summary>
public readonly record struct DayBoundsUtc(DateTimeOffset FromUtc, DateTimeOffset ToUtc);

/// <summary>
///     Calendar-day arithmetic in a given time zone (a "business day" that is not the UTC day), for daily counters and
///     date-range reads. Every bound is computed from the zone's own rules per day, so a day that has 23 or 25 hours
///     because of daylight saving time still gets exact bounds.
/// </summary>
public static class TimeProviderExtensions
{
    /// <summary>UTC bounds of "today" in <paramref name="zone" />.</summary>
    public static DayBoundsUtc GetTodayBoundsUtc(this TimeProvider timeProvider, TimeZoneInfo zone)
    {
        var today = timeProvider.GetToday(zone);

        return today.ToBoundsUtc(today, zone);
    }

    /// <summary>UTC bounds of the days <paramref name="fromDay" /> to <paramref name="toDay" />, both included.</summary>
    public static DayBoundsUtc ToBoundsUtc(this DateOnly fromDay, DateOnly toDay, TimeZoneInfo zone)
    {
        return new DayBoundsUtc(fromDay.StartOfDayUtc(zone), toDay.AddDays(1).StartOfDayUtc(zone));
    }

    /// <summary>The calendar day in <paramref name="zone" /> that <paramref name="instant" /> falls on.</summary>
    public static DateOnly ToDate(this DateTimeOffset instant, TimeZoneInfo zone)
    {
        return DateOnly.FromDateTime(TimeZoneInfo.ConvertTime(instant, zone).DateTime);
    }

    /// <summary>Today in <paramref name="zone" />; the same day <see cref="GetTodayBoundsUtc" /> bounds.</summary>
    public static DateOnly GetToday(this TimeProvider timeProvider, TimeZoneInfo zone)
    {
        return timeProvider.GetUtcNow().ToDate(zone);
    }

    private static DateTimeOffset StartOfDayUtc(this DateOnly day, TimeZoneInfo zone)
    {
        var local = day.ToDateTime(TimeOnly.MinValue, DateTimeKind.Unspecified);

        // A zone whose clock skips midnight (a daylight saving gap) starts that day at the first local time that exists.
        while (zone.IsInvalidTime(local))
        {
            local = local.AddMinutes(30);
        }

        // A zone whose clock repeats midnight (an overlap) starts that day at the first of the two midnights.
        var offset = zone.IsAmbiguousTime(local)
            ? zone.GetAmbiguousTimeOffsets(local).Max()
            : zone.GetUtcOffset(local);

        return new DateTimeOffset(local, offset).ToUniversalTime();
    }
}
