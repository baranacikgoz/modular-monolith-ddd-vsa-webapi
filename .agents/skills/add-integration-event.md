---
description: Add a new IntegrationEvent for async cross-module communication and scaffold the DomainEventHandler + consumer.
---

Add a new IntegrationEvent for async cross-module communication. Ask for source module name, event name, and payload fields if not provided.

**Step 1 — Declare the event** in `src/Common/Common.IntegrationEvents/{SourceModule}.cs`:
```csharp
public sealed record {EventName}IntegrationEvent(
    // payload fields — use strongly-typed IDs where applicable
) : IntegrationEvent;
```
If the file doesn't exist yet, create it with the appropriate `using` statements.

**Step 2 — Create the DomainEventHandler** in `src/Modules/{SourceModule}/{SourceModule}.Application/{Aggregate}/DomainEventHandlers/v1/V1{DomainEvent}DomainEventHandler.cs`:
```csharp
public class V1{DomainEvent}DomainEventHandler(IEventBus eventBus)
    : EventHandlerBase<V1{DomainEvent}DomainEvent>
{
    protected override async Task HandleAsync(
        ConsumeContext<V1{DomainEvent}DomainEvent> context,
        V1{DomainEvent}DomainEvent @event,
        CancellationToken cancellationToken)
    {
        await eventBus.PublishAsync(
            new {EventName}IntegrationEvent(/* map fields from @event */),
            cancellationToken);
    }
}
```

**Step 3 — Scaffold the consumer** (if a target module is known) in `src/Modules/{TargetModule}/{TargetModule}.Application/{Consumer}.cs`:
```csharp
public class {EventName}IntegrationEventConsumer(/* deps */)
    : IConsumer<{EventName}IntegrationEvent>
{
    public async Task Consume(ConsumeContext<{EventName}IntegrationEvent> context)
    {
        var @event = context.Message;
        // handle
    }
}
```

**Step 4 — Register the consumer** in the target module's `ModuleInstaller.cs` via MassTransit configuration.

**Step 5** — run `make build` to confirm zero warnings.
