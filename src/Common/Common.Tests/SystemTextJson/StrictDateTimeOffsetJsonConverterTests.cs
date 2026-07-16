using System.Text.Json;
using Common.Application.JsonConverters;
using Xunit;

namespace Common.Tests.SystemTextJson;

#pragma warning disable CA1515, CA1707

public sealed class StrictDateTimeOffsetJsonConverterTests
{
    private static readonly JsonSerializerOptions Options = new()
    {
        Converters = { new StrictDateTimeOffsetJsonConverter() },
    };

    [Theory]
    [InlineData("\"2026-07-17T13:00:00Z\"")]
    [InlineData("\"2026-07-17T13:00:00+03:00\"")]
    [InlineData("\"2026-07-17T13:00:00.1234567+03:00\"")]
    public void Read_ExplicitOffset_Parses(string json)
    {
        var result = JsonSerializer.Deserialize<DateTimeOffset>(json, Options);

        Assert.Equal(13, result.Hour);
    }

    [Theory]
    [InlineData("\"2026-07-17T13:00:00\"")]
    [InlineData("\"not-a-date\"")]
    [InlineData("\"2026-07-17\"")]
    public void Read_MissingOrInvalidOffset_Throws(string json)
    {
        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<DateTimeOffset>(json, Options));
    }

    [Fact]
    public void Write_RoundTrips()
    {
        var value = new DateTimeOffset(2026, 7, 17, 13, 0, 0, TimeSpan.FromHours(3));

        var json = JsonSerializer.Serialize(value, Options);
        var result = JsonSerializer.Deserialize<DateTimeOffset>(json, Options);

        Assert.Equal(value, result);
    }
}
