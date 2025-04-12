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
            new PolymorphicEventConverter<DomainEvent>(),
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
            new PolymorphicEventConverter<DomainEvent>(),
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
}
