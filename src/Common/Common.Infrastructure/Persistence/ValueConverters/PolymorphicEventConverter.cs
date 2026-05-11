using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common.Infrastructure.Persistence.ValueConverters;

public sealed class PolymorphicEventConverter<T> : JsonConverter<T>
{
    private const string EventTypeFullNameFieldName = "eventTypeFullName";
    private const string EventDataFieldName = "eventData";

    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var jsonDocument = JsonDocument.ParseValue(ref reader);
        var typeString = jsonDocument.RootElement.GetProperty(EventTypeFullNameFieldName).GetString()
                         ?? throw new InvalidOperationException(
                             "Event type information is missing or incorrect in JSON.");
        var type = ResolveType(typeString)
                   ?? throw new InvalidOperationException($"Type {typeString} not found.");

        var eventData = jsonDocument.RootElement.GetProperty(EventDataFieldName).GetRawText();
        return (T)JsonSerializer.Deserialize(eventData, type, options)!;
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString(EventTypeFullNameFieldName, value!.GetType().FullName);

        writer.WritePropertyName(EventDataFieldName);
        JsonSerializer.Serialize(writer, value, value.GetType(), options);

        writer.WriteEndObject();
    }

    private static Type? ResolveType(string typeName)
    {
        return Type.GetType(typeName)
               ?? AppDomain.CurrentDomain.GetAssemblies()
                   .Select(a => a.GetType(typeName))
                   .FirstOrDefault(t => t != null);
    }
}
