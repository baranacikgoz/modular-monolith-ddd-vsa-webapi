using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Application.JsonConverters;
using Common.Domain.Events;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Common.Infrastructure.Persistence.ValueConverters;

public class EventConverter : ValueConverter<IEvent, string>
{
    public static readonly JsonSerializerOptions WriteOptions = new()
    {
        WriteIndented = false,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters =
        {
            new PolymorphicEventConverter<IEvent>(),
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
            new PolymorphicEventConverter<IEvent>(),
            new JsonStringEnumConverter(),
            new StronglyTypedIdReadOnlyJsonConverter()
        }
    };

    public EventConverter() : base(
        eventItem => JsonSerializer.Serialize(eventItem, WriteOptions),
        json => JsonSerializer.Deserialize<IEvent>(json, _readOptions)!)
    {
    }
}
