# Refactor: Kafka/Debezium → RabbitMQ + Polling Outbox

## Context & Decisions

### Why Kafka is wrong here
Kafka/Debezium is used solely as an outbox trigger. `OutboxKafkaProcessor` consumes the CDC event, immediately discards the Kafka payload, and re-loads the `OutboxMessage` by ID from Postgres. None of Kafka's actual features are used:
- Consumer group offset management → unused (one processor, one group)
- Log retention / replay → unused (DB row is source of truth)
- High throughput → irrelevant for modular monolith workload
- Multiple consumers per topic → unused (outbox is point-to-point)

The CDC chain (WAL → Debezium → Kafka → processor) is 4 infrastructure layers for what is fundamentally `SELECT WHERE IsProcessed = false`.

### Why the two-phase outbox was dropped
The original design had:
- **Phase 1:** domain event in OutboxMessages → polled → handler runs → writes integration event to OutboxMessages
- **Phase 2:** integration event in OutboxMessages → polled → published to RabbitMQ

Domain events are ephemeral triggers, not durable messages. Persisting them to the outbox adds a full polling cycle of latency and a discriminator branching problem. Dispatching domain events in-process inside `OutboxSaveHelper` eliminates phase 1 entirely and is the industry-standard DDD approach.

### Why RabbitMQ — introducing MassTransit from scratch
There is **no MassTransit in the codebase**. Integration event handlers are `IEventHandler<T>` dispatched in-process by `EventDispatcher` — same mechanism as domain event handlers. We are introducing MassTransit + RabbitMQ from scratch to give integration events a real broker:
- Management UI at `:15672` — visibility into queues, message rates, DLQ inspection
- Future module extraction — no code change needed, just topology change
- Native DLQ (`_error` queues) with tooling for inspection and replay
- Message-level monitoring and replay

### Why NOT MassTransit built-in EF Core outbox (`UseEntityFrameworkOutbox`)
- Different table schema — would require migrating or running two outbox tables
- Loses custom features: W3C TraceId/ParentSpanId correlation, `OutboxLagJob`, `OutboxCleanupJob` with configurable retention, custom OTel telemetry in `OutboxTelemetry`

### IsProcessed = true semantics (critical)
`IsProcessed = true` means **"integration event successfully delivered to RabbitMQ broker (publish confirmed via publisher confirms)"**. It does NOT mean the consumer processed it. Consumer failures after delivery are RabbitMQ's retry/DLQ responsibility.

Consequence: **at-least-once delivery**. If the app crashes after `IPublishEndpoint.Publish()` succeeds but before `SaveChangesAsync()` marks `IsProcessed = true`, the same integration event is published again on the next poll. All `IConsumer<T>` implementations MUST be idempotent. `EventHandlerBase<T>` already provides FusionCache-based dedup for domain event handlers — the same pattern must be applied to integration event consumers.

### Domain event dispatch BEFORE the transaction (key design decision)
Domain events are dispatched in-process BEFORE the DB transaction opens. Handlers collect integration events to the in-memory `IIntegrationEventOutbox`. Only then does the transaction open, commit business state, and bulk-insert the collected integration events atomically.

This is strictly better than dispatching inside the transaction:
- **Transaction is minimal duration** — only pure DB operations, no handler execution time
- **No business entity locks held during handler execution** — handlers run freely
- **Handler failure = nothing saved** — exception propagates before transaction opens → consistent
- **Transaction failure after dispatch** = business state not committed, in-memory integration events discarded → consistent

### BaseDbContext coupling to EventDispatcher + IntegrationEventOutbox (known tradeoff)
`BaseDbContext` injects `EventDispatcher` (concrete) and `IntegrationEventOutbox` (concrete). This couples the persistence layer to the event dispatch infrastructure. It is a known tradeoff, accepted because the alternative — using `IOutboxDbContext` (a separate DbContext) with a shared transaction via `UseTransactionAsync` — would require passing Npgsql connections across DbContext boundaries, which is fragile and complex. The raw SQL bulk INSERT for outbox rows must use the business DbContext's own connection so it participates in the same transaction. Injecting directly is the pragmatic solution.

### Sequential domain event dispatch (mandatory)
`EventDispatcher.DispatchAsync()` dispatches handlers sequentially (existing `foreach + await`). This is mandatory — handlers share the same `IIntegrationEventOutbox` collector instance which is not thread-safe. Concurrent handlers would race on the collector's internal list.

### OutboxProcessor batch: sequential per message
Messages within a polling batch are processed sequentially. Simpler, avoids concurrent `IOutboxDbContext` usage within a batch.

### Drain() decision
`IntegrationEventOutbox.Drain()` is `internal` — no `IDrainableOutbox` interface. `OutboxSaveHelper` receives `IntegrationEventOutbox` as the concrete type and calls `Drain()` directly. Both are in the same assembly (`Common.Infrastructure`), so `internal` access works.

### EventType column removed entirely
Since domain events are never written to `OutboxMessages`, all rows are integration events. There is no need to discriminate by type. `EventType` property, `EventTypeDomain`/`EventTypeIntegration` constants, and the constructor switch are all deleted. `OutboxMessage.Create()` accepts only `IntegrationEvent`. `OutboxMessageTests` that tested `EventType` discrimination are deleted and replaced.

### RabbitMQ credentials (local dev only)
`guest`/`guest` is restricted to localhost in RabbitMQ by default. Since this is local dev only and credentials are safe to commit, use `RABBITMQ_LOOPBACK_USERS=` (empty) to allow the guest user from any host. Or use a dedicated user (`mm`/`mm`).

### Publisher confirms (critical)
MassTransit with RabbitMQ does NOT enable publisher confirms by default. Without them, `IPublishEndpoint.Publish()` returns after writing to socket buffer — not after broker acknowledgment. If RabbitMQ crashes at that moment, the message is silently lost. Must configure `cfg.PublisherConfirmation = true` explicitly.

---

## Target Architecture

```
Command handler:
  aggregate.DoSomething()
  await db.SaveChangesAsync(ct)
         ↓
  OutboxSaveHelper.SaveWithOutboxAsync() [called from BaseDbContext.SaveChangesAsync]:
    1. collect domain events from ChangeTracker (aggregates with Events.Count > 0)
    2. create AuditLogEntry for each domain event  ← UNCHANGED, still written
    3. clear events from aggregates
    4. for each domain event (sequential) — NO TRANSACTION YET:
         EventDispatcher.DispatchAsync(event, ct)
           → DomainEventHandlers run
           → handlers call IIntegrationEventOutbox.Collect(integrationEvent)
             → collected in in-memory List<IntegrationEvent>
    [if dispatch throws → exception propagates, transaction never opened, nothing saved]
    5. drain IIntegrationEventOutbox → collected integration events
    6. BEGIN TRANSACTION
    7. baseSaveAsync()  — commits business state + audit log
    8. bulk INSERT collected integration events as OutboxMessages via raw SQL (same transaction)
    9. COMMIT — atomically: business state + audit log + integration event outbox rows
    [if steps 6-9 throw → ROLLBACK → in-memory events discarded, nothing persisted]

OutboxProcessor (BackgroundService, polls every PollIntervalMs):
  all OutboxMessages rows are integration events — no discriminator needed
  SELECT ... FOR UPDATE SKIP LOCKED → deserialize → IPublishEndpoint.Publish() → mark IsProcessed=true
```

---

## File-by-File Changes

### A. OutboxMessage — src/Common/Common.Application/Persistence/Outbox/OutboxMessage.cs

**Remove entirely:**
- `EventType` property
- `EventTypeDomain = "DomainEvent"` constant
- `EventTypeIntegration = "IntegrationEvent"` constant
- The `switch (@event) { DomainEvent → ..., IntegrationEvent → ... }` in constructor

**Add:**
- `public int RetryCount { get; private set; }` (default 0)
- `public DateTimeOffset? FailedOn { get; private set; }`
- `public void MarkAsFailed(DateTimeOffset failedOn) { FailedOn = failedOn; }`
- `public void IncrementRetryCount() { RetryCount++; }`

**Simplify `Create()`** — only accepts `IntegrationEvent`, not `IEvent`:
```csharp
public static OutboxMessage Create(DateTimeOffset createdOn, IntegrationEvent @event) { ... }
```

`IOutboxMessage` interface has no `EventType` field — no change needed there.

---

### B. OutboxMessageConfig — src/Modules/Outbox/Outbox/Persistence/OutboxMessageConfig.cs

**Remove:** `builder.Property(x => x.EventType)` configuration block.

**Add:**
```csharp
builder.Property(x => x.RetryCount).IsRequired().HasDefaultValue(0);
builder.Property(x => x.FailedOn).IsRequired(false);
```

**Replace index** (current: `CreatedOn, IsProcessed`):
```csharp
builder.HasIndex(x => new { x.IsProcessed, x.FailedOn, x.CreatedOn });
// For best Postgres performance, override in migration with a partial index:
// CREATE INDEX ix_outbox_unprocessed ON "Outbox"."OutboxMessages" (\"CreatedOn\")
// WHERE "IsProcessed" = false AND "FailedOn" IS NULL
```

---

### C. EF Migration — Outbox module

`make ef-add-Outbox name=ReplaceKafkaWithPollingOutbox`

Migration must:
- DROP COLUMN `EventType`
- ADD COLUMN `RetryCount` INT NOT NULL DEFAULT 0
- ADD COLUMN `FailedOn` TIMESTAMPTZ NULL
- DROP old index on `(CreatedOn, IsProcessed)`
- CREATE new index (partial preferred — see Section B)

No data migration needed — no production data exists at time of this refactor.

---

### D. IntegrationEventOutbox — src/Common/Common.Infrastructure/EventBus/IntegrationEventOutbox.cs

**Current:** writes to `IOutboxDbContext.OutboxMessages.Add(message)`.

**New:** in-memory scoped collector. Must be **scoped** — same instance shared between `BaseDbContext` and all `DomainEventHandlers` within a request.

```csharp
public sealed class IntegrationEventOutbox : IIntegrationEventOutbox
{
    private readonly List<IntegrationEvent> _events = [];

    public void Collect<TEvent>(TEvent @event) where TEvent : IntegrationEvent
        => _events.Add(@event);

    internal IReadOnlyList<IntegrationEvent> Drain()
    {
        var snapshot = _events.ToList();
        _events.Clear();
        return snapshot;
    }
}
```

`IIntegrationEventOutbox` interface — rename `Write<TEvent>` → `Collect<TEvent>`, signature otherwise unchanged:
```csharp
public interface IIntegrationEventOutbox
{
    void Collect<TEvent>(TEvent @event) where TEvent : IntegrationEvent;
}
```
Update all callers (domain event handlers calling `_outbox.Write(...)` → `_outbox.Collect(...)`).

`Drain()` is `internal` — called directly from `OutboxSaveHelper` (same assembly). No `IDrainableOutbox` interface needed.

---

### E. BaseDbContext — src/Common/Common.Infrastructure/Persistence/BaseDbContext.cs

**Add two constructor parameters:**
```csharp
public abstract partial class BaseDbContext(
    DbContextOptions options,
    TimeProvider timeProvider,
    ICurrentUser currentUser,
    ILogger<BaseDbContext> logger,
    EventDispatcher eventDispatcher,               // concrete — IEventDispatcher interface deleted
    IntegrationEventOutbox integrationEventOutbox  // concrete — needed for Drain()
) : DbContext(options)
```

Forward both to `OutboxSaveHelper.SaveWithOutboxAsync()`.

**Cascade:** find all subclasses and add params + pass-through:
```bash
grep -r "BaseDbContext" src --include="*.cs" -l
```
Expected: **`ProductsDbContext` only.** `IAMDbContext` extends `IdentityDbContext` (not `BaseDbContext`). `OutboxDbContext` extends plain `DbContext`. Neither needs updating.

---

### F. OutboxSaveHelper — src/Common/Common.Infrastructure/Persistence/OutboxSaveHelper.cs

**Signature change:**
```csharp
public static async Task<int> SaveWithOutboxAsync(
    DbContext context,
    TimeProvider timeProvider,
    ICurrentUser currentUser,
    ILogger logger,
    EventDispatcher eventDispatcher,
    IntegrationEventOutbox integrationEventOutbox,
    Func<CancellationToken, Task<int>> baseSaveAsync,
    CancellationToken cancellationToken)
```

**Logic change — new execution order:**
```
1. collect domain events from ChangeTracker (same as now)
2. create AuditLogEntries, add to EF context (same as now)
3. clear events from aggregates (same as now)
4. if no domain events → call baseSaveAsync() directly, return (same as now)
5. [NEW] dispatch each domain event in-process (NO transaction open yet):
     await eventDispatcher.DispatchAsync(domainEvent, ct)
     → handlers collect integration events to integrationEventOutbox
6. [NEW] drain: var integrationEvents = integrationEventOutbox.Drain()
7. BEGIN TRANSACTION  (same as now, but now comes AFTER dispatch)
8. baseSaveAsync()   (same)
9. [CHANGED] bulk INSERT integrationEvents as OutboxMessages (not domain events)
   → remove EventType from column list (column deleted)
   → RetryCount always 0 on insert (DB default handles it)
10. COMMIT
[catch: RollbackAsync() explicitly, rethrow]
```

AuditLogEntry creation — **unchanged**, still happens before dispatch (step 2).

**Raw SQL INSERT change:** remove `"EventType"` from column list and value arrays. The UNNEST call no longer includes the eventTypes array.

**TraceId/ParentSpanId capture — location change (critical):** Currently captured in `IntegrationEventOutbox.Write()` from `Activity.Current`. After refactor `Write()` is a plain list append. Capture must move to `OutboxSaveHelper` — capture `Activity.Current?.TraceId` and `Activity.Current?.SpanId` **before** the dispatch loop (step 5), then use those captured values when building OutboxMessage rows in the UNNEST insert (step 9). `Activity.Current` at that point still reflects the request span.

---

### G. EventDispatcher registration — src/Common/Common.Infrastructure/EventBus/Setup.cs

**Current:**
```csharp
services.AddScoped<IEventDispatcher, EventDispatcher>();
services.AddScoped<IIntegrationEventOutbox, IntegrationEventOutbox>();
```

**New** (`IEventDispatcher` interface deleted):
```csharp
services.AddScoped<EventDispatcher>();
services.AddScoped<IntegrationEventOutbox>();
// Expose same scoped instance via the interface — handlers inject IIntegrationEventOutbox,
// OutboxSaveHelper injects IntegrationEventOutbox (concrete) — SAME instance per scope
services.AddScoped<IIntegrationEventOutbox>(sp => sp.GetRequiredService<IntegrationEventOutbox>());
```

`IEventHandler<T>` DI scan registration — **unchanged**.

---

### H. OutboxProcessor — src/Modules/Outbox/Outbox/OutboxProcessor.cs (NEW FILE)

```
loop every PollIntervalMs:

  using scope = scopeFactory.CreateScope()
  db = scope.GetRequiredService<IOutboxDbContext>()
  publishEndpoint = scope.GetRequiredService<IPublishEndpoint>()

  await using tx = await db.Database.BeginTransactionAsync(ct)
  try:
    messages = SELECT ... FOR UPDATE SKIP LOCKED
      WHERE IsProcessed = false AND FailedOn IS NULL AND RetryCount < MaxRetryCount
      ORDER BY CreatedOn
      LIMIT BatchSize

    for each message (sequential):
      try:
        event = deserialize message.Event as IntegrationEvent
        await publishEndpoint.Publish(event, event.GetType(), ct)
        // publisher confirms enabled — awaiting guarantees broker received it
        message.IsProcessed = true
        message.ProcessedOn = now
      catch:
        message.IncrementRetryCount()
        if message.RetryCount >= MaxRetryCount:
          message.MarkAsFailed(now)

    await db.SaveChangesAsync(ct)
    await tx.CommitAsync(ct)       // ← locks released here
  catch:
    await tx.RollbackAsync(ct)     // ← MUST be explicit — not relying on dispose/GC
    log error, swallow             // loop continues on next PollIntervalMs tick

  await Task.Delay(PollIntervalMs, ct)
```

**Lock lifecycle:**

| Event | Lock fate |
|-------|-----------|
| `CommitAsync()` succeeds | Released immediately |
| `RollbackAsync()` called (any exception path) | Released immediately |
| App crash / connection drop | Postgres auto-rollbacks, releases all locks |
| `SaveChangesAsync()` throws → explicit `RollbackAsync()` | Released — RetryCount increments lost, messages re-picked up next poll |
| Per-message `Publish()` throws (inner catch) | Lock still held, batch continues, RetryCount++ committed on COMMIT |

**Why explicit `RollbackAsync()` and not relying on dispose:**
Transaction dispose triggers rollback but is non-deterministic — lock release may be delayed by seconds (TCP keepalive). Explicit rollback in `catch` is immediate.

**At-least-once edge case (SaveChanges fails):**
Some/all messages already published to RabbitMQ. ROLLBACK → rows stay `IsProcessed = false`. Next poll: same rows re-published. Consumers receive duplicates — idempotency handles correctness. If SaveChanges consistently fails: `OutboxMetricsJob` lag gauge rises → alerting fires.

**`FOR UPDATE SKIP LOCKED` — EF Core pattern (Npgsql):**
EF Core has no native `FOR UPDATE SKIP LOCKED`. Use `FromSqlRaw` inside the open transaction:
```csharp
var messages = await dbContext.OutboxMessages
    .FromSqlRaw("""
        SELECT * FROM "Outbox"."OutboxMessages"
        WHERE "IsProcessed" = false
          AND "FailedOn" IS NULL
          AND "RetryCount" < {0}
        ORDER BY "CreatedOn"
        LIMIT {1}
        FOR UPDATE SKIP LOCKED
        """, maxRetryCount, batchSize)
    .ToListAsync(ct);
```
Must execute inside `BeginTransactionAsync` — the transaction must already be open before the `FromSqlRaw` call so Postgres applies the row locks within it.

**Per-message activity tracing (mandatory):**
For each message in the batch, restore the original request trace context. Guard against null — trace fields are nullable (messages written without an active span have no context to restore):
```csharp
Activity? activity;
if (message.TraceId is not null && message.ParentSpanId is not null)
{
    var parentContext = new ActivityContext(
        ActivityTraceId.CreateFromString(message.TraceId),
        ActivitySpanId.CreateFromString(message.ParentSpanId),
        ActivityTraceFlags.Recorded);
    activity = OutboxTelemetry.ActivitySource.StartActivity(
        "outbox.publish", ActivityKind.Producer, parentContext);
}
else
{
    activity = OutboxTelemetry.ActivitySource.StartActivity(
        "outbox.publish", ActivityKind.Producer);
}
using var _ = activity;

activity?.SetTag("outbox.message_id", message.Id);
activity?.SetTag("outbox.retry_count", message.RetryCount);
activity?.SetTag("event.type", message.Event?.GetType().Name);
```
On publish success: `activity?.SetStatus(ActivityStatusCode.Ok)`.
On catch: `activity?.SetStatus(ActivityStatusCode.Error, ex.Message); activity?.RecordException(ex)`.

**Logging — `[LoggerMessage]` source generation mandatory** (project-wide rule). All log calls in `OutboxProcessor` must use `static partial` methods with `[LoggerMessage]` attributes — no interpolated strings.

**Delete:**
- `KafkaOutboxProcessorBase.cs`
- `OutboxKafkaProcessor.cs`
- `OutboxMessageDto.cs`
- `IOutboxMessageDto.cs`
- `OutboxMessageDtoDeserializer.cs`
- `DlqMessage.cs`

---

### I. OutboxModule — src/Modules/Outbox/Outbox/OutboxModule.cs

Replace `services.AddHostedService<OutboxKafkaProcessor>()` → `services.AddHostedService<OutboxProcessor>()`.

---

### J. OutboxCleanupJob — src/Modules/Outbox/Outbox/OutboxCleanupJob.cs

```csharp
// Before
.Where(m => m.IsProcessed && m.ProcessedOn < cutoff)

// After — also clean permanently failed messages
.Where(m => (m.IsProcessed && m.ProcessedOn < cutoff)
         || (m.FailedOn != null && m.FailedOn < cutoff))
```

---

### K. OutboxLagJob → **OutboxMetricsJob** — src/Modules/Outbox/Outbox/OutboxMetricsJob.cs

**Rename** `OutboxLagJob` → `OutboxMetricsJob`. The job now samples two distinct health metrics from DB on a schedule — "lag" only names one of them.

**Also rename in `OutboxModule.cs`:** Hangfire job key `"outbox-lag"` → `"outbox-metrics"`, registration `OutboxLagJob.ExecuteAsync` → `OutboxMetricsJob.ExecuteAsync`.

**Also rename in `OutboxOptions`:** `LagCronSchedule` → `MetricsCronSchedule`.

**WHERE clause change (lag query):**
```csharp
// Before
.CountAsync(m => !m.IsProcessed && m.CreatedOn < cutoff)

// After — exclude permanently failed (will never be processed, not real lag)
.CountAsync(m => !m.IsProcessed && m.FailedOn == null && m.CreatedOn < cutoff)
```

**Add stuck count query (new responsibility):**
```csharp
// After existing lag count:
var stuckCount = await db.OutboxMessages
    .CountAsync(m => !m.IsProcessed && m.FailedOn != null, cancellationToken);

OutboxTelemetry.SetStuckCount(stuckCount);
```

---

### L. OutboxOptions — src/Common/Common.Application/Options/OutboxOptions.cs

**Remove fields:** `KafkaConsumer`, `KafkaDlqProducer`, `SetupRetryDelaySeconds`, `ConsumeErrorDelaySeconds`, `ProcessingErrorDelaySeconds`, `ProcessTimeoutSeconds`, `ProcessingErrorMaxRetryCount`, `MaxConsecutiveDlqFailures`.

**Remove classes/validators:** `KafkaConsumer`, `KafkaProducer`, `KafkaConsumerValidator`, `KafkaProducerValidator`.

**Remove validator cross-field rule:** `ProcessTimeoutSeconds < KafkaConsumer.MaxPollIntervalMs / 1000` — no longer meaningful.

**Remove test file:** `src/Common/Common.Tests/KafkaConsumerValidatorTests.cs`

**Rename field:** `LagCronSchedule` → `MetricsCronSchedule` (job renamed to `OutboxMetricsJob`).

**Add fields:**
```csharp
public required int PollIntervalMs { get; set; }   // recommended: 500
public required int BatchSize { get; set; }         // recommended: 50
public required int MaxRetryCount { get; set; }     // recommended: 3
```

Update `OutboxOptionsValidator` to validate new fields only.

**Update all references to `LagCronSchedule`:**
- `appsettings.json` — rename key `"LagCronSchedule"` → `"MetricsCronSchedule"`
- `docker-compose.yml` — rename env var `OutboxOptions__LagCronSchedule` → `OutboxOptions__MetricsCronSchedule`
- Any test factory config overrides for this key

---

### M. MassTransit Transport

**MassTransit does not exist in the codebase — introduce from scratch.** Add `AddMassTransit()` in `Host` (composition root). Configure transport and enable publisher confirms:

```csharp
cfg.UsingRabbitMq((ctx, cfg) =>
{
    cfg.PublisherConfirmation = true;  // ← REQUIRED — guarantees Publish() awaits broker ack

    cfg.Host(rabbitMqOptions.Host, rabbitMqOptions.VirtualHost, h =>
    {
        h.Username(rabbitMqOptions.Username);
        h.Password(rabbitMqOptions.Password);
    });

    cfg.ConfigureEndpoints(ctx);
});
```

Add `RabbitMqOptions` class: `Host`, `VirtualHost`, `Username`, `Password`. Wire to appsettings.

All `IConsumer<T>` registrations in every `ModuleInstaller` — **unchanged**. Transport is transparent.

---

### N. EventHandlerBase split + Interface Cleanup

**`IEventBus` and `MassTransitEventBus` do not exist** — graph was stale. Nothing to delete there.

**Delete:**
- `src/Common/Common.Application/EventBus/IEventDispatcher.cs`

**Split `EventHandlerBase<T>`** into two base classes (same idempotency + stale + logging logic in both). Current class is `EventHandlerBase<T>` defined in **`EventHandler.cs`** (not `EventHandlerBase.cs`) — rename the file when splitting.

`DomainEventHandlerBase<T>` — rename/replace current `EventHandlerBase<T>` (file: `EventHandler.cs` → `DomainEventHandler.cs`):
- Implements `IEventHandler<T>` + `IEventHandlerWrapper`
- Used by all domain event handlers (`V1UserRegisteredDomainEventHandler`, `V1StoreCreatedDomainEventHandlers.*`)
- Dispatched in-process by `EventDispatcher` in `OutboxSaveHelper`

`IntegrationEventHandlerBase<T>` — new class in `Common.Application/EventBus/`:
- Implements `IConsumer<T>` (MassTransit)
- Same idempotency (FusionCache `processed_event:{eventId}`), stale detection, structured logging
- Abstract `ProcessAsync(T, CancellationToken)` — same contract as before
- `Consume(ConsumeContext<T> context)` entry point → runs idempotency check → calls `ProcessAsync()`
- Used by all integration event handlers

**Convert integration event handlers** — change base class only, `ProcessAsync()` body unchanged:
```csharp
// Before
public class UserRegisteredIntegrationEventHandler(...)
    : EventHandlerBase<UserRegisteredIntegrationEvent>(cache, cachingOptions, logger)

// After
public class UserRegisteredIntegrationEventHandler(...)
    : IntegrationEventHandlerBase<UserRegisteredIntegrationEvent>(cache, cachingOptions, logger)
```

Find all integration event handlers:
```bash
find src -path "*/IntegrationEventHandlers*" -name "*.cs" | grep -v obj | grep -v Tests
```

**Register consumers in MassTransit config — centralized only.** `AddMassTransit()` may only be called once. Call it in `Host` and discover consumers via assembly scanning across all module assemblies:
```csharp
// Host/Setup.MassTransit.cs
services.AddMassTransit(x =>
{
    // Scan all module application assemblies for IConsumer<T> implementations
    x.AddConsumers(
        typeof(IAM.Application.IAssemblyReference).Assembly,
        typeof(Notifications.Application.IAssemblyReference).Assembly
        // ... add each module's Application assembly
    );

    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.PublisherConfirmation = true;
        cfg.UseOpenTelemetry();  // ← automatic publish/consume spans; links to OutboxProcessor's parent activity
        cfg.Host(rabbitMqOptions.Host, rabbitMqOptions.VirtualHost, h =>
        {
            h.Username(rabbitMqOptions.Username);
            h.Password(rabbitMqOptions.Password);
        });
        cfg.ConfigureEndpoints(ctx);
    });
});
```
Do NOT call `AddMassTransit` inside individual `ModuleInstaller`s — each call replaces the previous registration.

**Register MassTransit ActivitySource in Host OTel builder** so Aspire Dashboard / OTLP exporter captures publish and consume spans:
```csharp
// In Host OTel setup (wherever AddOpenTelemetry().WithTracing() is configured)
builder.AddSource("MassTransit");
```

**`IntegrationEventHandlerBase<T>` must explicitly preserve `event.outcome` Activity tagging** from `EventHandlerBase<T>` — tag values `processed` / `duplicate` / `stale` on `Activity.Current` are the primary diagnostic signal in traces for at-least-once delivery. Do not drop this when implementing the new base class.

**Keep unchanged:**
- `EventDispatcher.cs` — concrete, injected into `BaseDbContext`, dispatches domain events only
- `IEventHandler<T>`, `IEventHandlerWrapper` — used by `DomainEventHandlerBase<T>` and `EventDispatcher`
- `IIntegrationEventOutbox` — interface unchanged

**Update stale comment in `IOutboxDbContext`:**
```csharp
// Before: "used by OutboxKafkaProcessor for read/update operations"
// After:  "used by OutboxProcessor for read/update operations"
```

---

### O. Infrastructure — docker-compose.yml

**Remove services:** `mm.kafka`, `mm.kafka-ui`, `mm.debezium`, `init-kafka-topics`, `init-debezium-connector`, `init-db-publication`.

**Remove from `host` service:** all `OutboxOptions__Kafka*` env vars, `depends_on` for Kafka/Debezium services.

**Remove directories:** `kafka-init/`, `debezium-init/`, `db-init/`.

**Add RabbitMQ service:**
```yaml
mm.rabbitmq:
  container_name: mm.rabbitmq
  image: rabbitmq:4-management
  networks:
    - local_shared_network
  ports:
    - "5672:5672"
    - "15672:15672"   # Management UI
  volumes:
    - ./.containers/mm.rabbitmq:/var/lib/rabbitmq
  environment:
    - RABBITMQ_DEFAULT_USER=mm
    - RABBITMQ_DEFAULT_PASS=mm
    # Allow non-localhost connections (guest is localhost-only by default)
    - RABBITMQ_LOOPBACK_USERS=
  healthcheck:
    test: ["CMD", "rabbitmq-diagnostics", "ping"]
    interval: 10s
    timeout: 5s
    retries: 5
    start_period: 15s
```

Add `mm.rabbitmq` to `host` `depends_on` with `condition: service_healthy`.

**Postgres:** remove `wal_level = logical` — no WAL config, no publication needed.

**Health checks — swap Kafka for RabbitMQ:**
- Remove `AspNetCore.HealthChecks.Kafka` from `Host.csproj`
- Add `AspNetCore.HealthChecks.RabbitMQ` to `Host.csproj`
- In Host health check registration: remove `.AddKafka(...)`, add `.AddRabbitMQ(rabbitMqConnectionString)` (or use the options-based overload matching `RabbitMqOptions`)

Net: 6 services removed, 1 added.

---

### P. NuGet Packages

**Remove:** `Confluent.Kafka` from `src/Modules/Outbox/Outbox/Outbox.csproj`

**Add (MassTransit introduced from scratch):**
- `MassTransit` → `Common.Application` (for `IConsumer<T>`, `ConsumeContext<T>` used in `IntegrationEventHandlerBase<T>`)
- `MassTransit` → `Common.Infrastructure` (for `IPublishEndpoint` used in `OutboxSaveHelper` / `OutboxProcessor`)
- `MassTransit` → `Outbox/Outbox.csproj` (for `IPublishEndpoint` used directly in `OutboxProcessor`)
- `MassTransit.RabbitMQ` → `Host/Host.csproj` (transport — `UsingRabbitMq()` configured in composition root only)

---

### R. OutboxTelemetry — src/Modules/Outbox/Outbox/Telemetry/OutboxTelemetry.cs

**Remove (Kafka/DLQ-specific — no longer applicable):**
- `MessagesPublished` counter — description referenced Kafka; rename + redescribe (see below)
- `MessagesDlqProduced` counter — DLQ producer gone; no RabbitMQ equivalent in app code
- `MessagesDlqFailed` counter — same, obsolete

**Rename/update:**
```csharp
// Before
public static readonly Counter<long> MessagesPublished =
    Meter.CreateCounter<long>("outbox.messages_published.total",
        description: "Total outbox messages published to Kafka");

// After
public static readonly Counter<long> MessagesPublished =
    Meter.CreateCounter<long>("outbox.messages_published.total",
        description: "Total outbox messages successfully published to RabbitMQ broker (publisher-confirmed)");
```

**Add new metrics:**
```csharp
// Fired when MarkAsFailed() is called — message permanently stuck, needs manual intervention
public static readonly Counter<long> MessagesPermanentlyFailed =
    Meter.CreateCounter<long>("outbox.messages_permanently_failed.total",
        description: "Total outbox messages that exhausted retry attempts and were permanently failed");

// Gauge: current count of stuck rows (FailedOn IS NOT NULL AND IsProcessed = false)
// Updated by OutboxLagJob alongside lag count — same polling cadence
private static long _stuckCount;
public static void SetStuckCount(long count) => Interlocked.Exchange(ref _stuckCount, count);
public static readonly ObservableGauge<long> OutboxStuckCount =
    Meter.CreateObservableGauge<long>(
        "outbox.stuck.count",
        () => new Measurement<long>(Interlocked.Read(ref _stuckCount)),
        description: "Outbox messages permanently failed (FailedOn IS NOT NULL) — never self-heal, require inspection");

// Histogram: messages per poll batch — signal for tuning BatchSize and PollIntervalMs
public static readonly Histogram<int> PollBatchSize =
    Meter.CreateHistogram<int>("outbox.poll_batch_size",
        description: "Number of outbox messages picked up per poll cycle");
```

**`OutboxMetricsJob`** (renamed from `OutboxLagJob`) calls both `SetLagCount()` and `SetStuckCount()`:
```csharp
var stuckCount = await db.OutboxMessages
    .CountAsync(m => !m.IsProcessed && m.FailedOn != null, cancellationToken);
OutboxTelemetry.SetStuckCount(stuckCount);
```

**Emit `PollBatchSize` and `MessagesPermanentlyFailed` in `OutboxProcessor`:**
```csharp
OutboxTelemetry.PollBatchSize.Record(messages.Count);
// inside per-message catch, after MarkAsFailed():
OutboxTelemetry.MessagesPermanentlyFailed.Add(1);
```

---

### Q. Tests

**Delete entirely:**
- `src/Modules/Outbox/Outbox.Tests/OutboxKafkaProcessorTests.cs` — tests Kafka consumer, DLQ, spy dispatcher; all obsolete
- `src/Common/Common.Tests/OutboxMessageTests.cs` — tests `EventType` discrimination (`Create_DomainEvent_SetsEventTypeDomain`, `Create_IntegrationEvent_SetsEventTypeIntegration`); both facts deleted since `EventType` is removed

**Rewrite `OutboxTestWebAppFactory`** — `src/Modules/Outbox/Outbox.Tests/OutboxTestWebAppFactory.cs`:
- Remove `KafkaContainer` and all Kafka config overrides
- Add `RabbitMqContainer` (Testcontainers.RabbitMq) with same credentials as docker-compose (`mm`/`mm`)
- Override `OutboxOptions:PollIntervalMs`, `OutboxOptions:BatchSize`, `OutboxOptions:MaxRetryCount`
- Remove `SpyEventDispatcher` override (no longer needed — OutboxProcessor uses `IPublishEndpoint`, not `IEventDispatcher`)
- Wire MassTransit test harness or real RabbitMQ for end-to-end consumer verification

**Rewrite Notifications integration event handler tests** — `src/Modules/Notifications/Notifications.Tests/IntegrationEventHandlers/UserRegisteredIntegrationEventHandlerTests.cs`:
- Currently calls `_handler.HandleAsync(@event, ct)` directly — this method no longer exists on `IntegrationEventHandlerBase<T>`
- New entry point is `IConsumer<T>.Consume(ConsumeContext<T>)`
- Use MassTransit `InMemoryTestHarness` or `ServiceCollection` + `ITestHarness` to publish and assert consumption:
```csharp
// After
await harness.Bus.Publish(new UserRegisteredIntegrationEvent(...));
Assert.True(await harness.Consumed.Any<UserRegisteredIntegrationEvent>());
await _smsService.Received(1).SendWelcomeAsync(...);
```
- Idempotency test (same event ID twice → processed once) stays valid — FusionCache dedup preserved in `IntegrationEventHandlerBase<T>`

**Update `OutboxOptionsValidatorTests`** — `src/Common/Common.Tests/OutboxOptionsValidatorTests.cs`:
- Delete all Kafka-field validation tests
- Add validation tests for `PollIntervalMs`, `BatchSize`, `MaxRetryCount`

**Create `OutboxMessageTests` (replacement)** — `src/Common/Common.Tests/OutboxMessageTests.cs`:
```
- Create_IntegrationEvent_CreatesMessage()        → OutboxMessage.Create() with IntegrationEvent, verify Event set correctly
- IncrementRetryCount_IncrementsCounter()         → call 3x, assert RetryCount == 3
- MarkAsFailed_SetsFailedOn()                     → call MarkAsFailed(now), assert FailedOn == now
- MarkAsProcessed_SetsIsProcessedAndProcessedOn() → existing method, keep test
```

**Create `OutboxProcessorTests`** — `src/Modules/Outbox/Outbox.Tests/OutboxProcessorTests.cs`:

Tests are integration tests against real Postgres (Testcontainers) + real RabbitMQ (Testcontainers):

```
- ProcessBatch_IntegrationEvent_PublishesToRabbitMq()
  → seed OutboxMessage row with IntegrationEvent
  → wait for OutboxProcessor to poll
  → assert IConsumer<TestIntegrationEvent> received the message
  → assert OutboxMessage.IsProcessed == true in DB

- ProcessBatch_PublishFails_IncrementsRetryCount()
  → seed OutboxMessage row
  → configure RabbitMQ to be unavailable (stop container)
  → wait for poll cycle
  → assert RetryCount == 1 in DB, IsProcessed == false

- ProcessBatch_ExceedsMaxRetry_MarksAsFailed()
  → seed OutboxMessage with RetryCount = MaxRetryCount - 1
  → configure publish to fail
  → wait for poll
  → assert FailedOn IS NOT NULL, IsProcessed == false

- ProcessBatch_SkipsAlreadyFailed()
  → seed OutboxMessage with FailedOn set
  → wait for poll cycle
  → assert IsProcessed still false (processor skipped it)
  → assert no publish attempt
```

**Create `OutboxSaveHelperTests`** — `src/Common/Common.Tests/OutboxSaveHelperTests.cs` (highest-risk step):

Tests verify the domain event dispatch + integration event outbox write path:

```
- SaveChangesAsync_DomainEvent_DispatchesHandlerAndWritesIntegrationEventToOutbox()
  → create aggregate with domain event
  → SaveChangesAsync via module DbContext
  → assert OutboxMessages table has 1 row (the integration event, NOT domain event)
  → assert OutboxMessage.Event deserializes as IntegrationEvent
  → assert business entity persisted

- SaveChangesAsync_HandlerThrows_RollsBackBusinessState()
  → configure EventDispatcher to throw
  → create aggregate with domain event
  → SaveChangesAsync → assert throws
  → assert business entity NOT in DB
  → assert OutboxMessages table empty

- SaveChangesAsync_NoDomainEvents_SavesNormally()
  → entity with no domain events
  → SaveChangesAsync → assert saves, no OutboxMessages rows

- SaveChangesAsync_DomainEvent_NoDomainEventRowInOutbox()
  → explicitly assert OutboxMessages has 0 rows with domain event payload
  → (guard against regression where domain events leak to outbox table)
```

---

## What Does NOT Change

- All domain aggregates, domain events, domain services
- All application layer handlers and validators
- `EventDispatcher` logic (same in-process sequential `foreach + await` DI scan)
- `IIntegrationEventOutbox` interface
- `ProcessAsync()` body in all event handlers (domain and integration) — only base class changes for integration handlers
- `AuditLogEntry` creation in `OutboxSaveHelper` (one entry per domain event)
- `OutboxCleanupJob` Hangfire scheduling (only WHERE clause changes)
- `AuditingInterceptor`

## What Changes That Was Previously Marked "Unchanged"

- `EventHandlerBase<T>` — **split** into `DomainEventHandlerBase<T>` (keeps `IEventHandler<T>`) and `IntegrationEventHandlerBase<T>` (new, implements `IConsumer<T>`)
- Integration event handler tests — currently call `_handler.HandleAsync()` directly; must migrate to MassTransit test harness (`InMemoryTestHarness` or `ITestHarness`) since `Consume(ConsumeContext<T>)` is the new entry point
- `OutboxTelemetry` — DLQ counters removed, `MessagesPublished` description updated, three new metrics added (`MessagesPermanentlyFailed`, `OutboxStuckCount`, `PollBatchSize`)
- `OutboxLagJob` **renamed** to `OutboxMetricsJob` — now reports both lag count and stuck count; `LagCronSchedule` option renamed to `MetricsCronSchedule`; Hangfire key `"outbox-lag"` → `"outbox-metrics"`

---

## Migration Steps (ordered)

| # | Action | Files | Risk |
|---|--------|-------|------|
| 1 | Add `mm.rabbitmq` to docker-compose | `docker-compose.yml` | Low |
| 2 | Add `MassTransit.RabbitMQ` NuGet, add `RabbitMqOptions`, configure `UsingRabbitMq()` with `PublisherConfirmation = true` | Host/Common.Infrastructure | Low |
| 3 | Change `IntegrationEventOutbox` to in-memory collector with `internal Drain()`, update DI registration | `IntegrationEventOutbox.cs`, `Setup.cs` | Low |
| 4 | Update `OutboxMessage`: remove `EventType` + constants + switch, add `RetryCount`/`FailedOn`/`MarkAsFailed()`/`IncrementRetryCount()`, change `Create()` to accept `IntegrationEvent` | `OutboxMessage.cs` | Medium |
| 5 | Update `OutboxMessageConfig`: remove `EventType`, add `RetryCount`/`FailedOn`, update index | `OutboxMessageConfig.cs` | Low |
| 6 | Add EF migration | `make ef-add-Outbox name=ReplaceKafkaWithPollingOutbox` | Low |
| 7 | Inject `EventDispatcher` + `IntegrationEventOutbox` into `BaseDbContext`, cascade to all module DbContexts | `BaseDbContext.cs` + all subclasses | Medium — mechanical cascade |
| 8 | Update `OutboxSaveHelper`: dispatch BEFORE transaction, drain collector, bulk INSERT integration events only, remove `EventType` from raw SQL | `OutboxSaveHelper.cs` | **High — core save path** |
| 9 | Write `OutboxSaveHelperTests` for step 8 and run them green | `OutboxSaveHelperTests.cs` | — |
| 10 | Write `OutboxProcessor.cs`, register in `OutboxModule.cs` alongside `OutboxKafkaProcessor` temporarily | `OutboxProcessor.cs`, `OutboxModule.cs` | Low |
| 11 | Rewrite `OutboxTestWebAppFactory` (RabbitMQ container, new config), write `OutboxProcessorTests`, run green | test files | — |
| 12 | Update `OutboxCleanupJob` WHERE clause | `OutboxCleanupJob.cs` | Low |
| 13 | Rename `OutboxLagJob` → `OutboxMetricsJob`, update WHERE clause, add stuck count query, rename `LagCronSchedule` → `MetricsCronSchedule`, update Hangfire key | `OutboxLagJob.cs` → `OutboxMetricsJob.cs`, `OutboxModule.cs`, `OutboxOptions.cs` | Low |
| 14 | Update `OutboxOptions`: strip Kafka fields, add polling fields, update validator | `OutboxOptions.cs` | Low |
| 15 | Update `OutboxOptionsValidatorTests` | `OutboxOptionsValidatorTests.cs` | Low |
| 16 | Delete `OutboxMessageTests.cs` (EventType tests), create replacement with RetryCount/FailedOn tests | `OutboxMessageTests.cs` | Low |
| 17 | Verify polling processor picks up messages end-to-end | run `make test-outbox` | — |
| 18 | Remove `OutboxKafkaProcessor` registration from `OutboxModule.cs` | `OutboxModule.cs` | Low |
| 19 | Delete all Kafka files: `KafkaOutboxProcessorBase`, `OutboxKafkaProcessor`, DTOs, `KafkaConsumerValidatorTests.cs` | 7 files | Low |
| 20 | Remove 6 docker-compose services + init directories | `docker-compose.yml`, dirs | Medium |
| 21 | Remove Postgres WAL config | `docker-compose.yml` | Low |
| 22 | ~~N/A~~ `IEventBus`/`MassTransitEventBus` do not exist — skip | — | — |
| 23 | Delete `IEventDispatcher` interface | `IEventDispatcher.cs` | Low |
| 24 | Update `IOutboxDbContext` stale comment | `IOutboxDbContext.cs` | Low |
| 25 | Run full quality gate | `make test` | — |

---

## Tradeoffs

| Concern | Before | After |
|---------|--------|-------|
| Outbox types | Two (DomainEvent, IntegrationEvent) | One (IntegrationEvent only) |
| Processing phases | Two polling cycles per business op | One polling cycle |
| Domain event durability | Outbox-backed retry | Dispatched in SaveChanges — failure rolls back transaction |
| Transaction duration | Business state + dispatch + outbox insert | Business state + outbox insert only (dispatch is pre-transaction) |
| Processor complexity | Branching discriminator, offset mgmt, DLQ producer (~600 LOC) | Single publish path (~100 LOC) |
| Infrastructure containers | 11 | 5 (postgres, rabbitmq, redis, aspire-dashboard, host) |
| DLQ | Custom Kafka producer + topic | `FailedOn` column + RabbitMQ native `_error` queue |
| Horizontal scale safety | Kafka partition assignment | `FOR UPDATE SKIP LOCKED` |
| Postgres requirement | `wal_level = logical` + publication + replication slot | Standard config |
| Startup wait | Kafka + Debezium + topic init + connector deploy | RabbitMQ healthcheck only |

**Latency note:** polling at 500ms is fine for a modular monolith. If sub-100ms needed later: add PostgreSQL `LISTEN/NOTIFY` as a wake signal — processor wakes immediately on INSERT. 10-line enhancement, zero architecture change.
