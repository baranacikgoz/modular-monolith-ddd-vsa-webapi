using Common.Application.Extensions;
using Xunit;

namespace Common.Tests;

#pragma warning disable CA1707 // Remove the underscores from member name
public class TimeProviderExtensionsTests
{
    // Fixed +03:00, no daylight saving time.
    private static readonly TimeZoneInfo FixedOffsetZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Istanbul");

    // Daylight saving time begins on 2026-03-08 and ends on 2026-11-01.
    private static readonly TimeZoneInfo DaylightSavingZone = TimeZoneInfo.FindSystemTimeZoneById("America/New_York");

    // The clock jumps from 00:00 to 01:00 on 2026-03-08, so that day has no midnight.
    private static readonly TimeZoneInfo MidnightGapZone = TimeZoneInfo.FindSystemTimeZoneById("America/Havana");

    private sealed class FixedTimeProvider(DateTimeOffset now) : TimeProvider
    {
        public override DateTimeOffset GetUtcNow()
        {
            return now;
        }
    }

    [Fact]
    public void GetTodayBoundsUtc_MidDayMoment_ReturnsLocalMidnightToMidnightAsUtc()
    {
        var provider = new FixedTimeProvider(new DateTimeOffset(2026, 9, 23, 14, 30, 0, TimeSpan.FromHours(3)));

        var bounds = provider.GetTodayBoundsUtc(FixedOffsetZone);

        Assert.Equal(new DateTimeOffset(2026, 9, 22, 21, 0, 0, TimeSpan.Zero), bounds.FromUtc);
        Assert.Equal(new DateTimeOffset(2026, 9, 23, 21, 0, 0, TimeSpan.Zero), bounds.ToUtc);
    }

    [Fact]
    public void GetTodayBoundsUtc_JustBeforeLocalMidnight_StillBelongsToThatDay()
    {
        var provider = new FixedTimeProvider(new DateTimeOffset(2026, 9, 23, 23, 59, 59, TimeSpan.FromHours(3)));

        var bounds = provider.GetTodayBoundsUtc(FixedOffsetZone);

        Assert.Equal(new DateTimeOffset(2026, 9, 22, 21, 0, 0, TimeSpan.Zero), bounds.FromUtc);
        Assert.Equal(new DateTimeOffset(2026, 9, 23, 21, 0, 0, TimeSpan.Zero), bounds.ToUtc);
    }

    [Fact]
    public void GetTodayBoundsUtc_WindowIsHalfOpen()
    {
        var provider = new FixedTimeProvider(new DateTimeOffset(2026, 9, 23, 0, 0, 0, TimeSpan.FromHours(3)));

        var bounds = provider.GetTodayBoundsUtc(FixedOffsetZone);

        Assert.True(bounds.FromUtc <= provider.GetUtcNow());
        Assert.True(bounds.ToUtc > provider.GetUtcNow());
        Assert.Equal(TimeSpan.FromDays(1), bounds.ToUtc - bounds.FromUtc);
    }

    [Fact]
    public void ToDate_AroundLocalMidnight_SwitchesAtTheLocalDayBoundary()
    {
        Assert.Equal(new DateOnly(2026, 9, 22), new DateTimeOffset(2026, 9, 22, 20, 59, 59, TimeSpan.Zero).ToDate(FixedOffsetZone));
        Assert.Equal(new DateOnly(2026, 9, 23), new DateTimeOffset(2026, 9, 22, 21, 0, 0, TimeSpan.Zero).ToDate(FixedOffsetZone));
    }

    [Fact]
    public void GetToday_MatchesTheDayGetTodayBoundsUtcBounds()
    {
        var provider = new FixedTimeProvider(new DateTimeOffset(2026, 9, 23, 1, 0, 0, TimeSpan.FromHours(3)));

        Assert.Equal(new DateOnly(2026, 9, 23), provider.GetToday(FixedOffsetZone));
        Assert.Equal(provider.GetToday(FixedOffsetZone), provider.GetTodayBoundsUtc(FixedOffsetZone).FromUtc.ToDate(FixedOffsetZone));
    }

    [Fact]
    public void ToBoundsUtc_TwoDays_SpansFromFirstMidnightToDayAfterLast()
    {
        var bounds = new DateOnly(2026, 9, 22).ToBoundsUtc(new DateOnly(2026, 9, 23), FixedOffsetZone);

        Assert.Equal(new DateTimeOffset(2026, 9, 21, 21, 0, 0, TimeSpan.Zero), bounds.FromUtc);
        Assert.Equal(new DateTimeOffset(2026, 9, 23, 21, 0, 0, TimeSpan.Zero), bounds.ToUtc);
    }

    [Fact]
    public void ToBoundsUtc_DaylightSavingStartDay_IsTwentyThreeHoursLong()
    {
        var day = new DateOnly(2026, 3, 8);

        var bounds = day.ToBoundsUtc(day, DaylightSavingZone);

        Assert.Equal(new DateTimeOffset(2026, 3, 8, 5, 0, 0, TimeSpan.Zero), bounds.FromUtc);
        Assert.Equal(new DateTimeOffset(2026, 3, 9, 4, 0, 0, TimeSpan.Zero), bounds.ToUtc);
        Assert.Equal(TimeSpan.FromHours(23), bounds.ToUtc - bounds.FromUtc);
    }

    [Fact]
    public void ToBoundsUtc_DaylightSavingEndDay_IsTwentyFiveHoursLong()
    {
        var day = new DateOnly(2026, 11, 1);

        var bounds = day.ToBoundsUtc(day, DaylightSavingZone);

        Assert.Equal(new DateTimeOffset(2026, 11, 1, 4, 0, 0, TimeSpan.Zero), bounds.FromUtc);
        Assert.Equal(new DateTimeOffset(2026, 11, 2, 5, 0, 0, TimeSpan.Zero), bounds.ToUtc);
        Assert.Equal(TimeSpan.FromHours(25), bounds.ToUtc - bounds.FromUtc);
    }

    [Fact]
    public void ToBoundsUtc_DayWithoutMidnight_StartsAtTheFirstLocalTimeThatExists()
    {
        var day = new DateOnly(2026, 3, 8);

        var bounds = day.ToBoundsUtc(day, MidnightGapZone);

        Assert.Equal(new DateTimeOffset(2026, 3, 8, 5, 0, 0, TimeSpan.Zero), bounds.FromUtc);
        Assert.Equal(TimeSpan.FromHours(23), bounds.ToUtc - bounds.FromUtc);
    }
}
