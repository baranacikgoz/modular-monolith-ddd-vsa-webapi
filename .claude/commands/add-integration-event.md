---
description: Declare an IntegrationEvent, publish it from a DomainEventHandler in the source module, and scaffold the consumer in the target module.
argument-hint: "<SourceModule> <EventName> [TargetModule]"
allowed-tools: Read, Edit, Write, Bash, Glob, Grep
---

Add integration event: $ARGUMENTS

Rules in CLAUDE.md §6. Exemplars in §2 (publishing handler, consumer).

1. `src/Common/Common.IntegrationEvents/{SourceModule}.cs`: `public sealed record {EventName}IntegrationEvent(...) : IntegrationEvent;` with strongly-typed ids where applicable. Create the file if missing.
2. `src/Modules/{Source}/{Source}.Application/{Aggregate}/DomainEventHandlers/v1/V1{DomainEvent}DomainEventHandlers.cs`: a `DomainEventHandlerBase<V1{DomainEvent}DomainEvent>` that calls `outbox.Collect(new {EventName}IntegrationEvent(...))`. If the payload needs fields not on the event, read the aggregate from `db.ChangeTracker.Entries<{Aggregate}>()`; it is already tracked in the same `SaveChanges`. Do not query the DB.
3. Target module (if given): `{Target}.Application/IntegrationEventHandlers/{Verb}On{EventName}Handler.cs` inheriting `IntegrationEventHandlerBase<{EventName}IntegrationEvent>`, override `ProcessAsync`. Mutating consumers add a domain-level existence check on top of the cache dedupe.
4. No registration step; assembly scan picks up consumers.
5. `make build`, then a test that asserts the `OutboxMessages` row (`/scaffold-test`).
