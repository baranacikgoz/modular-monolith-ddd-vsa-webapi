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

        var type = ResolveType(typeString);
        if (type is null)
        {
            return default!;
        }

        var eventData = jsonDocument.RootElement.GetProperty(EventDataFieldName).GetRawText();
        var obj = JsonSerializer.Deserialize(eventData, type, options);

        // Rows written before the refactoring may contain non-T events; return null so callers can handle gracefully.
        if (obj is not T result)
        {
            return default!;
        }

        return result;
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
