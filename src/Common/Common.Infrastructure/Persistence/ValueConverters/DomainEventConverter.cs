using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Application.JsonConverters;
using Common.Domain.Events;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Common.Infrastructure.Persistence.ValueConverters;

public class DomainEventConverter : ValueConverter<DomainEvent, string>
{
    internal static readonly JsonSerializerOptions WriteOptions = new()
    {
        WriteIndented = false,
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
        WriteIndented = false,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters =
        {
            new PolymorphicEventConverter<DomainEvent>(),
            new JsonStringEnumConverter(),
            new StronglyTypedIdReadOnlyJsonConverter()
        }
    };

    public DomainEventConverter() : base(
        eventItem => JsonSerializer.Serialize(eventItem, WriteOptions),
        json => JsonSerializer.Deserialize<DomainEvent>(json, _readOptions)!
    )
    {
    }
}
