using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Domain.Events;

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

        var rawEventData = jsonDocument.RootElement.GetProperty(EventDataFieldName).GetRawText();

        var type = ResolveType(typeString);
        if (type is null)
        {
            // Defining assembly not loaded (e.g. module disabled via modules.json). Surface a
            // sentinel instead of null, so a DomainEvent-typed caller does not NRE on GetType().
            return AsUnknownEvent(typeString, rawEventData);
        }

        var obj = JsonSerializer.Deserialize(rawEventData, type, options);

        // Rows written before the refactoring may contain non-T events; surface a sentinel so
        // callers can handle it gracefully instead of dereferencing a null DomainEvent.
        if (obj is not T result)
        {
            return AsUnknownEvent(typeString, rawEventData);
        }

        return result;
    }

    private static T AsUnknownEvent(string typeName, string rawEventData)
    {
        // Only meaningful for T = DomainEvent. For any other T (e.g. IntegrationEvent) the
        // pattern match fails and this falls through to the previous default! behaviour.
        return new UnknownDomainEvent(typeName, rawEventData) is T sentinel ? sentinel : default!;
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
