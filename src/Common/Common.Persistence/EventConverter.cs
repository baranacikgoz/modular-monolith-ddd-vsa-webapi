using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Core.Contracts;
using Common.Core.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;

namespace Common.Persistence;

public partial class EventConverter : ValueConverter<IEvent, string>
{
    private static readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        Converters =
        {
            new PolymorphicIEventConverter(),
            new JsonStringEnumConverter()
        }
    };

    public EventConverter() : base(
            eventItem => JsonSerializer.Serialize(eventItem, _options),
            json => JsonSerializer.Deserialize<IEvent>(json, _options)!
        )
    {
    }

    private const string EventTypeFieldName = "EventType";
    private sealed class PolymorphicIEventConverter : JsonConverter<IEvent>
    {
        public override IEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonDocument = JsonDocument.ParseValue(ref reader);
            var typeString = jsonDocument.RootElement.GetProperty(EventTypeFieldName).GetString()
                ?? throw new InvalidOperationException($"Event type information is missing or incorrect in JSON.");
            var type = Type.GetType(typeString)
                ?? throw new InvalidOperationException($"Type {typeString} not found.");

            return (IEvent)JsonSerializer.Deserialize(jsonDocument.RootElement.GetRawText(), type, options)!;
        }

        public override void Write(Utf8JsonWriter writer, IEvent value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();  // Start the enclosing object.
            writer.WriteString(EventTypeFieldName, value.GetType().AssemblyQualifiedName); // Write the type information.

            // Serialize the 'value' object properties directly into the current JSON object.
            using (var doc = JsonDocument.Parse(JsonSerializer.Serialize(value, value.GetType(), options)))
            {
                foreach (var prop in doc.RootElement.EnumerateObject())
                {
                    prop.WriteTo(writer); // This writes each property from the value object into the current JSON object.
                }
            }

            writer.WriteEndObject();  // End the enclosing object.
        }

    }
}
