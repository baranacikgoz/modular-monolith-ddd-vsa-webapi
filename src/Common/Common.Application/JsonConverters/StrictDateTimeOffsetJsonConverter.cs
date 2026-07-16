using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common.Application.JsonConverters;

/// <summary>
///     Rejects ISO-8601 datetime strings that omit an explicit UTC offset (or "Z").
///     The default <see cref="DateTimeOffset"/> converter silently assumes UTC for such
///     strings, which turns a client's forgotten offset into a wrong-but-valid instant
///     (e.g. a naive local time gets stored as if it were UTC) instead of a 400 response.
/// </summary>
public sealed class StrictDateTimeOffsetJsonConverter : JsonConverter<DateTimeOffset>
{
    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (value is null || !HasExplicitOffset(value) || !DateTimeOffset.TryParse(
                value,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var result))
        {
            throw new JsonException(
                $"Invalid DateTimeOffset '{value}'. An explicit UTC offset (or 'Z') is required.");
        }

        return result;
    }

    /// <summary>
    ///     "K" (and DateTimeOffset.TryParse in general) treats a missing offset as valid and
    ///     silently assumes UTC, which is the exact ambiguity this converter exists to reject.
    ///     So the offset marker has to be located explicitly, in the time segment only —
    ///     the date segment's own '-' separators (e.g. "2026-07-17") must not count.
    /// </summary>
    private static bool HasExplicitOffset(string value)
    {
        if (value.Length == 0)
        {
            return false;
        }

        if (value[^1] is 'Z' or 'z')
        {
            return true;
        }

        var timeSegment = value.Length > 10 ? value.AsSpan(10) : ReadOnlySpan<char>.Empty;
        return timeSegment.Contains('+') || timeSegment.Contains('-');
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }
}
