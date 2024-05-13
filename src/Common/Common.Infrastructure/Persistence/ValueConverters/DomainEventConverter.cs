using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Domain.Events;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Common.Infrastructure.Persistence.ValueConverters;

public partial class DomainEventConverter : ValueConverter<DomainEvent, string>
{
    private static readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        Converters =
        {
            new PolymorphicDomainEventConverter(),
            new JsonStringEnumConverter()
        }
    };

    public DomainEventConverter() : base(
            eventItem => JsonSerializer.Serialize(eventItem, _options),
            json => JsonSerializer.Deserialize<DomainEvent>(json, _options)!
        )
    {
    }

    private const string EventTypeFullNameFieldName = "EventTypeFullName";
    private sealed class PolymorphicDomainEventConverter : JsonConverter<DomainEvent>
    {
        public override DomainEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonDocument = JsonDocument.ParseValue(ref reader);
            var typeString = jsonDocument.RootElement.GetProperty(EventTypeFullNameFieldName).GetString()
                ?? throw new InvalidOperationException($"Event type information is missing or incorrect in JSON.");
            var type = Type.GetType(typeString)
                ?? throw new InvalidOperationException($"Type {typeString} not found.");

            return (DomainEvent)JsonSerializer.Deserialize(jsonDocument.RootElement.GetRawText(), type, options)!;
        }

        public override void Write(Utf8JsonWriter writer, DomainEvent value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();  // Start the enclosing object.
            writer.WriteString(EventTypeFullNameFieldName, value.GetType().AssemblyQualifiedName); // Write the type information.

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
