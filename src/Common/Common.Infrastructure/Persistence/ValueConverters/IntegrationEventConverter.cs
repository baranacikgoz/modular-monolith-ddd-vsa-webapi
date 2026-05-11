using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Application.JsonConverters;
using Common.IntegrationEvents;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Common.Infrastructure.Persistence.ValueConverters;

public class IntegrationEventConverter : ValueConverter<IntegrationEvent, string>
{
    private static readonly JsonSerializerOptions _writeOptions = new()
    {
        WriteIndented = false,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters =
        {
            new PolymorphicEventConverter<IntegrationEvent>(),
            new JsonStringEnumConverter(),
            new StronglyTypedIdWriteOnlyJsonConverter()
        }
    };

    private static readonly JsonSerializerOptions _readOptions = new()
    {
        WriteIndented = false,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters =
        {
            new PolymorphicEventConverter<IntegrationEvent>(),
            new JsonStringEnumConverter(),
            new StronglyTypedIdReadOnlyJsonConverter()
        }
    };

    public IntegrationEventConverter() : base(
        eventItem => JsonSerializer.Serialize(eventItem, _writeOptions),
        json => JsonSerializer.Deserialize<IntegrationEvent>(json, _readOptions)!
    )
    {
    }
}
