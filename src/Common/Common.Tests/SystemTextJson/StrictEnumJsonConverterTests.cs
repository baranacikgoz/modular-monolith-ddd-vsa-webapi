using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace Common.Tests.SystemTextJson;

#pragma warning disable CA1515, CA1707

public sealed class StrictEnumJsonConverterTests
{
    private static readonly JsonSerializerOptions Options = new()
    {
        Converters = { new JsonStringEnumConverter(allowIntegerValues: false) },
    };

    public enum Status
    {
        None = 0,
        Active = 1,
        Closed = 2,
    }

    [Theory]
    [InlineData("\"Active\"", Status.Active)]
    [InlineData("\"None\"", Status.None)]
    public void Read_DefinedStringName_Parses(string json, Status expected)
    {
        var result = JsonSerializer.Deserialize<Status>(json, Options);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("1")]
    [InlineData("99")]
    [InlineData("\"1\"")]
    public void Read_NumericValue_Throws(string json)
    {
        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Status>(json, Options));
    }

    [Fact]
    public void Read_UndefinedStringName_Throws()
    {
        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Status>("\"Bogus\"", Options));
    }

    [Fact]
    public void Write_RoundTrips()
    {
        var json = JsonSerializer.Serialize(Status.Active, Options);
        var result = JsonSerializer.Deserialize<Status>(json, Options);

        Assert.Equal(Status.Active, result);
    }
}
