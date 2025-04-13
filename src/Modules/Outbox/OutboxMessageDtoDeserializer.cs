using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Application.JsonConverters;
using Common.Application.ValueConverters;
using Common.Domain.Events;
using Common.Infrastructure.Persistence.Outbox;
using Confluent.Kafka;

namespace Outbox;

public class OutboxMessageDtoDeserializer : IDeserializer<OutboxMessageDto>
{
    private static readonly JsonSerializerOptions _erializerOptions =
        new()
        {
            PropertyNameCaseInsensitive = true,
            Converters =
            {
                new JsonStringEnumConverter(),
                new StronglyTypedIdReadOnlyJsonConverter(),
            }
        };

    public OutboxMessageDto Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        if (isNull)
        {
            throw new ArgumentNullException(nameof(data));
        }

        return JsonSerializer.Deserialize<OutboxMessageDto>(data, _erializerOptions) ?? throw new ArgumentNullException(nameof(data));
    }
}
