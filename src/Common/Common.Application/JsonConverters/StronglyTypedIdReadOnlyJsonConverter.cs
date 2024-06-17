using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Domain.StronglyTypedIds;

namespace Common.Application.JsonConverters;

public class StronglyTypedIdReadOnlyJsonConverter<T> : JsonConverter<T>
    where T : IStronglyTypedId, new()
{
    public override bool CanConvert(Type typeToConvert)
        => typeof(IStronglyTypedId).IsAssignableFrom(typeToConvert);

    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString() ?? string.Empty;
        var guid = Guid.Parse(value);
        return new T { Value = guid };
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        => throw new NotImplementedException("Direct serialization is not supported, use [JsonConverter(typeof(StronglyTypedIdWriteOnlyJsonConverter))]");
}
