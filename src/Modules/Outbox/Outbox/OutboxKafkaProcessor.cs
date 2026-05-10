using Common.Application.EventBus;
using Common.Application.Options;
using Common.Application.Persistence.Outbox;
using Common.Domain.Events;
using Common.Infrastructure.Persistence.Extensions;
using Common.Infrastructure.Persistence.Outbox;
using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Outbox;

public sealed class OutboxKafkaProcessor(
    IOptions<OutboxOptions> outboxOptionsProvider,
    IServiceScopeFactory serviceScopeFactory,
    TimeProvider timeProvider,
    ILogger<OutboxKafkaProcessor> logger)
    : KafkaOutboxProcessorBase<OutboxMessageDto>(
        outboxOptionsProvider,
        serviceScopeFactory,
        timeProvider,
        logger)
{
    protected override IDeserializer<OutboxMessageDto> CreateDeserializer()
        => new OutboxMessageDtoDeserializer();

    protected override KafkaConsumer GetConsumerConfig(OutboxOptions options)
        => options.KafkaConsumer;

    protected override KafkaProducer GetDlqConfig(OutboxOptions options)
        => options.KafkaDlqProducer;

    protected override async Task<IOutboxMessage?> LoadEntityAsync(IOutboxDbContext db, int id, CancellationToken ct)
    {
        var message = await db.OutboxMessages
            .TagWith(nameof(OutboxKafkaProcessor), id)
            .SingleOrDefaultAsync(x => x.Id == id, ct);
        return message;
    }

    protected override Task DispatchEventAsync(IEvent @event, IServiceProvider sp, CancellationToken ct)
        => sp.GetRequiredService<IEventDispatcher>().DispatchAsync(@event, ct);
}
