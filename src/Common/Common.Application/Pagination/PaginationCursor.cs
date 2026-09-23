using System.Buffers.Text;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;

namespace Common.Application.Pagination;

/// <summary>
///     Opaque keyset cursor: the sort value and the tiebreaker of the last row of a page, encoded as base64url JSON
///     with a type tag per value so the next page can rebuild typed comparison bounds. Supported value types:
///     <see cref="DateTimeOffset" />, <see cref="DateTime" />, <see cref="long" />, <see cref="int" />,
///     <see cref="decimal" />, <see cref="double" />, <see cref="float" />, <see cref="string" />, <see cref="Guid" />
///     and any <see cref="IStronglyTypedId" /> (stored by its <see cref="IStronglyTypedId.Value" />).
/// </summary>
public sealed record PaginationCursor(object SortValue, object Tiebreaker)
{
    public const string ParameterName = "PaginationRequest.After";

    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.General)
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public static string Encode(object sortValue, object tiebreaker)
    {
        ArgumentNullException.ThrowIfNull(sortValue);
        ArgumentNullException.ThrowIfNull(tiebreaker);
        var payload = new Payload(TaggedValue.From(sortValue), TaggedValue.From(tiebreaker));
        var json = JsonSerializer.SerializeToUtf8Bytes(payload, SerializerOptions);
        return Base64Url.EncodeToString(json);
    }

    /// <summary>Fails with a <c>Validation</c> error on <see cref="ParameterName" /> when the cursor is malformed.</summary>
    public static Result<PaginationCursor> Decode(string cursor)
    {
        if (!TryDecode(cursor, out var decoded))
        {
            return Error.Validation([ParameterName]);
        }

        return decoded;
    }

    public static bool TryDecode(string? cursor, out PaginationCursor decoded)
    {
        decoded = null!;
        if (string.IsNullOrWhiteSpace(cursor))
        {
            return false;
        }

        try
        {
            var json = Base64Url.DecodeFromChars(cursor);
            var payload = JsonSerializer.Deserialize<Payload>(json, SerializerOptions);
            if (payload?.Sort is null || payload.Tie is null)
            {
                return false;
            }

            decoded = new PaginationCursor(payload.Sort.ToValue(), payload.Tie.ToValue());
            return true;
        }
        catch (Exception ex) when (ex is FormatException or JsonException or ArgumentException or OverflowException)
        {
            return false;
        }
    }

    /// <summary>Rebuilds the value as <paramref name="targetType" /> (a strongly-typed id from its Guid, otherwise as is).</summary>
    public static object Materialize(object value, Type targetType)
    {
        if (typeof(IStronglyTypedId).IsAssignableFrom(targetType))
        {
            if (value is not Guid guid)
            {
                throw new ArgumentException($"A {targetType.Name} cursor value must be a Guid.", nameof(value));
            }

            var id = Activator.CreateInstance(targetType)
                     ?? throw new InvalidOperationException($"Cannot instantiate {targetType.Name}.");
            targetType.GetProperty(nameof(IStronglyTypedId.Value))!.SetValue(id, guid);
            return id;
        }

        var underlying = Nullable.GetUnderlyingType(targetType) ?? targetType;
        return underlying.IsInstanceOfType(value)
            ? value
            : throw new ArgumentException($"Cursor value of type {value.GetType().Name} does not match {targetType.Name}.", nameof(value));
    }

    private sealed record Payload(TaggedValue Sort, TaggedValue Tie);

    /// <summary>One value with its type tag, serialized as a string so no precision is lost in transit.</summary>
    private sealed record TaggedValue(string T, string V)
    {
        public static TaggedValue From(object value)
        {
            return value switch
            {
                IStronglyTypedId id => new TaggedValue("g", id.Value.ToString("D")),
                DateTimeOffset dto => new TaggedValue("dto", dto.ToString("O", CultureInfo.InvariantCulture)),
                DateTime dt => new TaggedValue("dt", dt.ToString("O", CultureInfo.InvariantCulture)),
                long l => new TaggedValue("l", l.ToString(CultureInfo.InvariantCulture)),
                int i => new TaggedValue("i", i.ToString(CultureInfo.InvariantCulture)),
                decimal m => new TaggedValue("m", m.ToString(CultureInfo.InvariantCulture)),
                double d => new TaggedValue("d", d.ToString("R", CultureInfo.InvariantCulture)),
                float f => new TaggedValue("f", f.ToString("R", CultureInfo.InvariantCulture)),
                string s => new TaggedValue("s", s),
                Guid g => new TaggedValue("g", g.ToString("D")),
                _ => throw new ArgumentException($"Unsupported cursor value type {value.GetType().Name}.", nameof(value))
            };
        }

        public object ToValue()
        {
            return T switch
            {
                "g" => Guid.ParseExact(V, "D"),
                "dto" => DateTimeOffset.ParseExact(V, "O", CultureInfo.InvariantCulture),
                "dt" => DateTime.ParseExact(V, "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                "l" => long.Parse(V, CultureInfo.InvariantCulture),
                "i" => int.Parse(V, CultureInfo.InvariantCulture),
                "m" => decimal.Parse(V, CultureInfo.InvariantCulture),
                "d" => double.Parse(V, CultureInfo.InvariantCulture),
                "f" => float.Parse(V, CultureInfo.InvariantCulture),
                "s" => V,
                _ => throw new FormatException($"Unknown cursor type tag '{T}'.")
            };
        }
    }
}
