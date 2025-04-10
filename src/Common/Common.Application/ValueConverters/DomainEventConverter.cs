using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Application.JsonConverters;
using Common.Domain.Events;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Common.Application.ValueConverters;

public partial class DomainEventConverter : ValueConverter<DomainEvent, string>
{
    private static readonly JsonSerializerOptions _writeOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters =
        {
            new PolymorphicDomainEventConverter(),
            new JsonStringEnumConverter(),
            new StronglyTypedIdWriteOnlyJsonConverter()
        }
    };

    private static readonly JsonSerializerOptions _readOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters =
        {
            new PolymorphicDomainEventConverter(),
            new JsonStringEnumConverter(),
            new StronglyTypedIdReadOnlyJsonConverter()
        }
    };

    public DomainEventConverter() : base(
            eventItem => JsonSerializer.Serialize(eventItem, _writeOptions),
            json => JsonSerializer.Deserialize<DomainEvent>(json, _readOptions)!
        )
    {
    }

    private const string EventTypeFullNameFieldName = "eventTypeFullName";
    private const string EventDataFieldName = "eventData";
    private sealed class PolymorphicDomainEventConverter : JsonConverter<DomainEvent>
    {
        public override DomainEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonDocument = JsonDocument.ParseValue(ref reader);
            var typeString = jsonDocument.RootElement.GetProperty(EventTypeFullNameFieldName).GetString()
                ?? throw new InvalidOperationException($"Event type information is missing or incorrect in JSON.");
            var type = Type.GetType(typeString)
                ?? throw new InvalidOperationException($"Type {typeString} not found.");

            // EventData is in "EventData" property.
            var eventData = jsonDocument.RootElement.GetProperty(EventDataFieldName).GetRawText();
            return (DomainEvent)JsonSerializer.Deserialize(eventData, type, options)!;
        }

        public override void Write(Utf8JsonWriter writer, DomainEvent value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();  // Start the enclosing object.
            writer.WriteString(EventTypeFullNameFieldName, value.GetType().AssemblyQualifiedName); // Write the type information.

            writer.WritePropertyName(EventDataFieldName);
            JsonSerializer.Serialize(writer, value, value.GetType(), options);

            writer.WriteEndObject();  // End the enclosing object.
        }

    }
}
