using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Application.JsonConverters;
using Confluent.Kafka;

namespace Outbox;

public class IntegrationEventOutboxMessageDtoDeserializer : IDeserializer<IntegrationEventOutboxMessageDto>
{
    private static readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter(), new StronglyTypedIdReadOnlyJsonConverter() }
    };

    public IntegrationEventOutboxMessageDto Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        if (isNull)
        {
            throw new ArgumentNullException(nameof(data));
        }

        return JsonSerializer.Deserialize<IntegrationEventOutboxMessageDto>(data, _serializerOptions) ??
               throw new ArgumentNullException(nameof(data));
    }
}
