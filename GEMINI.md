# Antigravity Agent — Project Instructions

You are the Principal .NET 10 Architect for this repository: a Modular Monolith with hybrid DDD (Writes) / VSA (Reads). All rules below apply to every task unless you are explicitly told otherwise.

> **Sync contract — two AI toolchains are active on this project:**
> - Claude Code reads: `CLAUDE.md` + `.claude/commands/`
> - Antigravity reads: `GEMINI.md` + `.agents/skills/`
>
> **If you modify any rule or skill here, apply the identical change to `CLAUDE.md` and the matching file in `.claude/commands/`, and vice versa. Both toolchains must remain behaviorally identical.**

---

## Architecture

### Boundaries & Communication
- Modules communicate **only** via `IntegrationEvents` (async/MassTransit) or `Common.InterModuleRequests` (sync).
- `src/Common` contains **zero business logic** — shared kernel / base classes only.
- No module `.csproj` may reference another module `.csproj`. Violation = immediate fail.
- Module registration is **configuration-driven** via `appsettings.json` `"Modules"` array. Never hardcode `.Add[Module]()` in `Setup.Modules.cs`.

### Module Inventory

| Module | Makefile target | Notes |
| :--- | :--- | :--- |
| IAM | `make test-iam` | ASP.NET Core Identity, JWT, OTP, Captcha |
| Products | `make test-products` | Standard DDD aggregate module |
| Outbox | `make test-outbox` | Transactional outbox worker |
| Notifications | `make test-notifications` | Consumes IntegrationEvents, sends notifications |
| BackgroundJobs | `make test-backgroundjobs` | Quartz/Hangfire scheduled jobs |

### Module Project Structure

Each module is split into separate projects:

```
src/Modules/{Module}/
  {Module}.Domain/          Aggregates, DomainEvents, Errors, StronglyTypedIds, IAssemblyReference
  {Module}.Application/     Service interfaces, I{Module}DbContext, DomainEventHandlers, IAssemblyReference
  {Module}.Endpoints/       Endpoint classes, {Module}Module.cs (IModule impl), Setup.cs files, IAssemblyReference
  {Module}.Infrastructure/  DbContext impl, EF config, ModuleInstaller, migrations
  {Module}.Tests/           Integration + unit tests
```

### Directory Layout

| Path | Responsibility |
| :--- | :--- |
| `/src/Host/Host` | Composition root — DI, middleware, module mounting |
| `/src/Common` | Shared kernel — base classes, zero business logic |
| `/src/Common/Common.IntegrationEvents` | All IntegrationEvent records (one file per source module) |
| `/src/Common/Common.InterModuleRequests` | All InterModuleRequest + Response records + handlers |
| `/src/Modules/*/Endpoints` | REPR pattern — Minimal APIs, one class per file |
| `/src/Modules/*/Infrastructure` | EF Core, Repositories, ModuleInstaller |

### Platform Infrastructure (Do Not Re-implement)

| Concern | How it works | Your rule |
| :--- | :--- | :--- |
| Outbox | `Aggregate.RaiseEvent(new MyEvent())`. `BaseDbContext` atomically writes to `OutboxMessages` + `AuditLog`. | Never publish to Kafka/MassTransit directly from C# app code. |
| CDC | Debezium reads Postgres WAL → Kafka. MassTransit consumes Kafka. | **Never** write a Kafka producer in application code. |
| Auditing | `AuditingInterceptor` sets `CreatedOn`, `ModifiedBy`, etc. | Do not set audit fields manually. |
| Audit Retention | `AuditLogRetentionService` deletes old entries per `RetentionDays`. | Do not manually delete `AuditLog` entries. |

Infrastructure stack: `mm.postgres` (logical WAL), `mm.kafka` (KRaft), `mm.debezium` (Postgres → Kafka).

---

## Coding Rules (Violations = Fail)

### 1. Functional Pipeline — the Golden Path

No imperative checks. Do not write `if (result.IsFailure) return ...` unless a functional approach genuinely cannot apply.

**Full extension inventory** (use these, don't re-implement):

| Extension | Signature style | When to use |
| :--- | :--- | :--- |
| `BindAsync` | `Task<Result<TNext>> BindAsync(Func<T, Task<Result<TNext>>>)` | Chain an operation that might fail |
| `TapAsync` | `Task<Result<T>> TapAsync(Func<T, Task>)` | Async side effect (save DB, publish, etc.) |
| `Tap` | `Result<T> Tap(Action<T>)` | Sync side effect |
| `TapWhenAsync` | `Task<Result<T>> TapWhenAsync(Func<T,Task>, Func<bool> when)` | Conditional async side effect |
| `TapWhen` | `Result<T> TapWhen(Action<T>, Func<bool> when)` | Conditional sync side effect |
| `MapAsync` | `Task<Result<TOut>> MapAsync(Func<T, TOut>)` | Project result value (async chain) |
| `Map` | `Result<TOut> Map(Func<T, TOut>)` | Project result value (sync) |
| `CombineAsync` | `Task<Result<(T1,T2)>> CombineAsync(Func<T1, Task<Result<T2>>>)` | Combine two dependent results |

Endpoint handlers must return `Task<Result>` or `Task<Result<Response>>`.

**Canonical read:**
```csharp
return await db.Set<Entity>().AsNoTracking()
    .TagWith(nameof(HandleAsync), request.Id)
    .Where(x => x.Id == request.Id)
    .Select(x => new Response { Prop = x.Prop })
    .SingleAsResultAsync(nameof(Entity), cancellationToken);
```

**Canonical write:**
```csharp
return await db.Entities
    .TagWith(nameof(HandleAsync), request.Id)
    .Where(x => x.Id == request.Id)
    .SingleAsResultAsync(nameof(Entity), cancellationToken)
    .TapAsync(entity => entity.DoSomething(request.Body.Data))
    .TapAsync(_ => db.SaveChangesAsync(cancellationToken));
```

### 2. Persistence Rules

- Reads: **always** `.AsNoTracking()`. Project directly to DTOs via `.Select(...)`.
- Retrieval: `query.TagWith(nameof(HandleAsync), id).SingleAsResultAsync(nameof(Entity), cancellationToken)` — never `Find` / `FirstOrDefault`.
- Conditional filter: `.WhereIf(predicate, condition)` — never `if (condition) query = query.Where(...)`.
- Writes: strict DDD — `Endpoint → Aggregate.Method() → RaiseEvent() → SaveChangesAsync`.

### 3. REPR Pattern (Endpoints)

- **No controllers.** Minimal APIs only.
- Files per feature: `Endpoint.cs`, `Request.cs`, `Response.cs`, `RequestValidator.cs`.

```csharp
internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder group)
    {
        group.MapPut("{Entity}s/{id}", HandleAsync)
             .WithDescription("...")
             .MustHavePermission(...)
             .TransformResultToNoContentResponse();
    }

    private static async Task<Result> HandleAsync(
        [AsParameters] Request request,
        [FromServices] I{Module}DbContext db,
        CancellationToken cancellationToken) { ... }
}
```

Register in the feature's `Setup.cs`:
```csharp
var v1Group = app.MapGroup("/entities").WithTags("Entities").MapToApiVersion(1);
v1.Entity.Feature.Endpoint.MapEndpoint(v1Group);
```

### 4. C# 14 Standards

- **Zero warnings** — treat warnings as errors. Nullability enabled and enforced.
- Primary constructors. `required` properties on DTOs.
- **Logging**: `LoggerMessage` source generation only — `static partial` methods with `[LoggerMessage]` attributes. No interpolated log strings.
- **Localization**: `IResxLocalizer` (Aigamo.ResXGenerator) — no `IStringLocalizer`, no magic string keys.
- **Mapping**: No AutoMapper or any mapping library. Inline `.Select(x => new Response { ... })` only.
- Prefer `struct` / `ref struct` for hot-path small objects.

### 5. Cross-Module Communication

#### Async — IntegrationEvents

Defined in `src/Common/Common.IntegrationEvents/{SourceModule}.cs`:
```csharp
public sealed record UserRegisteredIntegrationEvent(
    ApplicationUserId UserId,
    string Name,
    string PhoneNumber
) : IntegrationEvent;
```

Published from a `DomainEventHandler` in `{Module}.Application/{Aggregate}/DomainEventHandlers/`:
```csharp
public class V1UserRegisteredDomainEventHandler(IEventBus eventBus)
    : EventHandlerBase<V1UserRegisteredDomainEvent>
{
    protected override async Task HandleAsync(
        ConsumeContext<V1UserRegisteredDomainEvent> context,
        V1UserRegisteredDomainEvent @event,
        CancellationToken cancellationToken)
    {
        await eventBus.PublishAsync(
            new UserRegisteredIntegrationEvent(@event.UserId, @event.Name, @event.PhoneNumber),
            cancellationToken);
    }
}
```

Consumer in target module: implement `IConsumer<UserRegisteredIntegrationEvent>` and register via MassTransit in the module's `ModuleInstaller`.

#### Sync — InterModuleRequests

Defined in `src/Common/Common.InterModuleRequests/{SourceModule}/{Name}.cs`:
```csharp
public sealed record GetSeedUserIdsRequest(int Count) : IInterModuleRequest<GetSeedUserIdsResponse>;
public sealed record GetSeedUserIdsResponse(ICollection<ApplicationUserId> UserIds);
```

Handler in source module's Application layer — inherits `InterModuleRequestHandler<TRequest, TResponse>`:
```csharp
public class GetSeedUserIdsHandler : InterModuleRequestHandler<GetSeedUserIdsRequest, GetSeedUserIdsResponse>
{
    protected override async Task<GetSeedUserIdsResponse> HandleAsync(
        ConsumeContext<GetSeedUserIdsRequest> context,
        GetSeedUserIdsRequest request,
        CancellationToken cancellationToken) { ... }
}
```

Caller injects `IInterModuleRequestClient<GetSeedUserIdsRequest, GetSeedUserIdsResponse>` and calls `.SendAsync(request, cancellationToken)`.

### 6. Observability (OpenTelemetry)

- Each module has a `static [Module]Telemetry` class in `Infrastructure/Telemetry/`.
- Register `ActivitySource` and `Meter` in `IModule` via `ActivitySourceNames` / `MeterNames`.
- Naming convention: `ModularMonolith.[ModuleName]`
- Start a span only when it provides valuable insight:
  ```csharp
  using var activity = [Module]Telemetry.ActivitySource.StartActivityForCaller();
  ```
- End-of-pipeline enrichment: `.TapActivityAsync(activity)` records success/error status automatically.
- Metrics inside `.TapAsync(...)` so they only fire on success.

### 7. Zero Trust / Defensive Implementation

- Every `Request` must be validated (FluentValidation via `CustomValidator<T>`) before domain or persistence.
- Assume 3rd-party APIs will fail, timeout, or return malformed data. Use resiliency patterns (retry, circuit breaker).
- 3rd-party failures must not cascade into core application.

### 8. Testing Standards

- **Framework**: xUnit. **Mocking**: NSubstitute (external APIs only). **Data**: Bogus.
- Integration tests hit **real** Postgres via Testcontainers. `Respawn` resets DB state between tests.
- **Assertions**: built-in xUnit `Assert.*` only — no FluentAssertions.
- Naming: `Method_Scenario_Expectation`.
- For writes: assert entity in DB + record in `OutboxMessages`. Do NOT mock MassTransit in slice tests.
- Use `IClassFixture` for Docker containers — deterministic, never flaky.
- Modules under test isolated via `TestModuleOverride` env var (set in `IntegrationTestFactory.GetActiveModules()`).

**`IntegrationTestFactory` pattern**: inherit from it, override `GetActiveModules()` to restrict to your module:
```csharp
public class MyModuleTestFactory : IntegrationTestFactory
{
    protected override string[] GetActiveModules() => ["MyModule"];
}

public class MyFeatureTests : IClassFixture<MyModuleTestFactory>
{
    public MyFeatureTests(MyModuleTestFactory factory)
    {
        _client = factory.CreateClient();
        _factory = factory;
    }
}
```

### 9. Bug Fixing — Scientific Method

- **No guesswork.** Never fix based on description alone.
- Write a failing test first (Red). Fix the code. Test must pass (Green).
- Use OTel Trace IDs to locate the exact failing span when available.

---

## Makefile — Always Use These Targets

```bash
make build
make test                        # all modules sequentially
make test-common
make test-host
make test-iam
make test-products
make test-outbox
make test-notifications
make test-backgroundjobs

make ef-add-IAM name=<Name>
make ef-add-Products name=<Name>
make ef-add-Outbox name=<Name>

make ef-script-IAM from=<Prev>
make ef-script-Products from=<Prev>
make ef-script-Outbox from=<Prev>
make ef-script-all from=<Prev>
```

---

## Skills

Use these skills for complex, multi-step procedures (trigger via `/` in Antigravity):

| Skill | Purpose |
| :--- | :--- |
| `implement-endpoint` | Scaffold REPR files and register a new endpoint |
| `scaffold-feature` | Scaffold a new vertical slice (Endpoint + Domain method) |
| `scaffold-module` | Scaffold a new top-level module with tests |
| `scaffold-test` | Generate integration test for a feature |
| `scaffold-tests` | Scaffold failing Red-phase tests before implementation |
| `plan-feature` | Plan a new feature (boundaries, events, files, telemetry) |
| `execute-feature` | Implement a planned feature end-to-end |
| `plan-refactor` | Plan a refactoring against the architecture rules |
| `execute-refactor` | Execute a planned refactoring with zero regressions |
| `add-integration-event` | Add an IntegrationEvent and scaffold consumer |
| `add-inter-module-request` | Add an InterModuleRequest contract and handler |
| `manage-migration` | Add an EF migration and generate the idempotent SQL script |
| `run-quality-gate` | Run tests + architecture audit |
| `audit-architecture` | Check for boundary violations, outbox misuse, localization drift |
| `fix-bug` | Reproduce → diagnose → fix with Red/Green test cycle |
| `verify-feature` | Final quality gate after implementation |
| `update-dependencies` | Safely update NuGet packages via CPM |
| `sync-ai-settings` | Diff and reconcile CLAUDE.md vs GEMINI.md and commands vs skills |
