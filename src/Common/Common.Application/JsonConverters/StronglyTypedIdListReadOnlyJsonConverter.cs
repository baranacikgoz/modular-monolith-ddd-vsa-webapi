using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Domain.StronglyTypedIds;

namespace Common.Application.JsonConverters;

public sealed class StronglyTypedIdListReadOnlyJsonConverter<T> : JsonConverter<IReadOnlyList<T>>
    where T : IStronglyTypedId, new()
{
    public override IReadOnlyList<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }

        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected JSON array.");
        }

        var list = new List<T>();
        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
        {
            var value = reader.GetString() ?? throw new JsonException("Expected string element in array.");
            list.Add(new T { Value = DefaultIdType.Parse(value) });
        }

        return list;
    }

    public override void Write(Utf8JsonWriter writer, IReadOnlyList<T> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        foreach (var item in value)
        {
            writer.WriteStringValue(item.Value.ToString());
        }

        writer.WriteEndArray();
    }
}
