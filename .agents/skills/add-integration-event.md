---
description: Declare an IntegrationEvent in Common, scaffold the DomainEventHandler in the source module, and scaffold the consumer in the target module.
model: sonnet
argument-hint: "<SourceModule> <EventName> [TargetModule]"
allowed-tools: Read, Edit, Write, Bash, Glob, Grep
---

Add integration event: $ARGUMENTS

**Step 1 — Declare the event** in `src/Common/Common.IntegrationEvents/{SourceModule}.cs`:
```csharp
public sealed record {EventName}IntegrationEvent(
    // payload — use strongly-typed IDs where applicable
) : IntegrationEvent;
```

**Step 2 — DomainEventHandler** in `src/Modules/{SourceModule}/{SourceModule}.Application/{Aggregate}/DomainEventHandlers/v1/V1{DomainEvent}DomainEventHandler.cs`:
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

**Step 3 — Consumer** (if target module provided) in `src/Modules/{TargetModule}/{TargetModule}.Application/`:
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

**Step 4 — Register** the consumer in the target module's `ModuleInstaller.cs` via MassTransit.

**Step 5** — `make build` — zero warnings.
