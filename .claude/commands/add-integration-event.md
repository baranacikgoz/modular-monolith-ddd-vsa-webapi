---
description: Declare an IntegrationEvent in Common, scaffold the DomainEventHandler in the source module, and scaffold the consumer in the target module.
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
Create the file if it doesn't exist yet.

**Step 2 — DomainEventHandler** in `src/Modules/{SourceModule}/{SourceModule}.Application/{Aggregate}/DomainEventHandlers/v1/V1{DomainEvent}DomainEventHandler.cs`:
```csharp
public class V1{DomainEvent}DomainEventHandler(IIntegrationEventOutbox outbox)
    : DomainEventHandlerBase<V1{DomainEvent}DomainEvent>
{
    public override Task HandleAsync(
        V1{DomainEvent}DomainEvent @event,
        CancellationToken cancellationToken)
    {
        outbox.Collect(new {EventName}IntegrationEvent(/* map fields from @event */));
        return Task.CompletedTask;
    }
}
```
If the payload needs fields beyond what's on `@event`, pull the aggregate from the `ChangeTracker` instead of querying the DB — it's already tracked in this same `SaveChanges` transaction:
```csharp
public class V1{DomainEvent}DomainEventHandler(
    IIntegrationEventOutbox outbox,
    I{SourceModule}DbContext db
) : DomainEventHandlerBase<V1{DomainEvent}DomainEvent>
{
    public override Task HandleAsync(V1{DomainEvent}DomainEvent @event, CancellationToken cancellationToken)
    {
        var entity = db
            .ChangeTracker
            .Entries<{Aggregate}>()
            .First(e => e.Entity.Id == @event.{Aggregate}Id)
            .Entity;

        outbox.Collect(new {EventName}IntegrationEvent(/* map fields from @event and entity */));
        return Task.CompletedTask;
    }
}
```

**Step 3 — Consumer** (if target module provided). Location varies by module — check whether the target module already has an `IntegrationEventHandlers/` folder under `{TargetModule}.Application/` or `{TargetModule}.Infrastructure/` and follow that convention:
```csharp
public class {EventName}IntegrationEventHandler(/* deps */)
    : IntegrationEventHandlerBase<{EventName}IntegrationEvent>
{
    protected override async Task ProcessAsync(
        {EventName}IntegrationEvent @event,
        CancellationToken cancellationToken)
    {
        // handle
    }
}
```
Never implement `IConsumer<T>` directly — `IntegrationEventHandlerBase<T>` provides idempotency (FusionCache `processed_event:{event.Id}` check) for free.

**Step 4 — No manual registration.** Consumers auto-register via assembly scan (`x.AddConsumers(moduleAssemblies)` in `src/Host/Host/Infrastructure/Setup.MassTransit.cs`). There is no `ModuleInstaller.cs` file in this repo — skip this step entirely once the handler class exists.

**Step 5** — `make build` — zero warnings.
