using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Domain.Events;

namespace Common.Application.ValueConverters;

public sealed class PolymorphicEventConverter<T> : JsonConverter<T>
{
    private const string EventTypeFullNameFieldName = "eventTypeFullName";
    private const string EventDataFieldName = "eventData";

    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var jsonDocument = JsonDocument.ParseValue(ref reader);
        var typeString = jsonDocument.RootElement.GetProperty(EventTypeFullNameFieldName).GetString()
                         ?? throw new InvalidOperationException($"Event type information is missing or incorrect in JSON.");
        var type = Type.GetType(typeString)
                   ?? throw new InvalidOperationException($"Type {typeString} not found.");

        // EventData is in "EventData" property.
        var eventData = jsonDocument.RootElement.GetProperty(EventDataFieldName).GetRawText();
        return (T)JsonSerializer.Deserialize(eventData, type, options)!;
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();  // Start the enclosing object.
        writer.WriteString(EventTypeFullNameFieldName, value!.GetType().AssemblyQualifiedName); // Write the type information.

        writer.WritePropertyName(EventDataFieldName);
        JsonSerializer.Serialize(writer, value, value.GetType(), options);

        writer.WriteEndObject();  // End the enclosing object.
    }

}
