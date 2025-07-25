using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Application.JsonConverters;
using Confluent.Kafka;

namespace Outbox;

public class OutboxMessageDtoDeserializer : IDeserializer<OutboxMessageDto>
{
    private static readonly JsonSerializerOptions _serializerOptions =
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

        return JsonSerializer.Deserialize<OutboxMessageDto>(data, _serializerOptions) ?? throw new ArgumentNullException(nameof(data));
    }
}
