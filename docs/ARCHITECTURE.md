# Architecture Decision Record

This document explains the *why* behind the architectural decisions in this codebase — not just what was built, but what problem each decision solves and what tradeoffs it carries. It is intended for engineers evaluating the design, contributing to the project, or using it as a starting point.

---

## The Core Problem

The standard industry advice is: *"Build a modular monolith first, extract modules to microservices later when you need to scale them independently."*

In practice, "extract later" is a multi-month infrastructure project disguised as a refactor:

- New Git repository per extracted service
- New CI/CD pipeline per service
- Rewrite every in-process call into an HTTP or gRPC client
- Version the API
- Add retry, timeout, and circuit-breaker logic at every new call site
- Write consumer-driven contract tests
- Wire distributed tracing from scratch across service boundaries
- Handle partial failures that did not exist when the call was in-process
- Onboard new engineers across N repositories instead of one

Teams spend months on this infrastructure. The business feature that triggered the extraction ships late. The codebase that was easy to understand as a monolith is now spread across a dozen repositories that require context-switching to trace a single request.

This architecture removes "extract later" from the equation entirely — because there is nothing to extract.

---

## Design Goals

1. **Develop as a monolith, deploy as microservices** — same codebase, same binary, one environment variable controls the process topology.
2. **Boundaries enforced by the compiler, not convention** — a developer cannot accidentally create a cross-module dependency that the build does not catch.
3. **Communication contracts that survive split deployment without rewriting** — the call site that works in-process must work cross-process identically.
4. **Complexity arrives when the business earns it** — shared database with differentschemas per module, single binary, and single pipeline are defaults. Splitting any of them is a config change, not a code change.

---

## Key Decisions

### 1. Configuration-driven module loading

**Decision:** The host scans all loaded assemblies at startup for `IModule` implementations and activates only the ones listed in `ModulesOptions.EnabledModules`. No module registers itself via a hardcoded `services.AddXModule()` call.

**Why:** Hardcoded registration means the process topology is a code decision. Making it a configuration decision means the same binary can run as a full monolith (`"*"`), a two-module subset (`["IAM", "Products"]`), or a single-module isolated process — with zero code changes, at deploy time.

---

### 2. Two module tiers: `IModule` and `ICoreModule`

**Decision:** Modules that are infrastructure dependencies for all other modules (Outbox, BackgroundJobs) implement `ICoreModule` and load on every instance regardless of configuration. All other modules implement `IModule` and are opt-in.

**Why:** Outbox and BackgroundJobs are not business modules — they are the platform. Every instance that performs database writes uses `BaseDbContext`, which atomically writes domain events to `OutboxMessages` in the same transaction as the aggregate state change. For that to work, the Outbox module's EF configuration, `IOutboxDbContext`, and interceptors must be registered in DI on every writing instance — not because the Outbox processor needs to run everywhere, but because the transactional write infrastructure is a dependency of the module's own `DbContext`. BackgroundJobs manages scheduled infrastructure work. Requiring operators to remember to include both in every deployment configuration would be an error surface that serves no purpose.

---

### 3. `IInterModuleRequestClient<TRequest, TResponse>` as the only synchronous cross-module communication path

**Decision:** All synchronous requests between modules must go through `IInterModuleRequestClient<TRequest, TResponse>`. Direct method calls, shared services, or HTTP between modules are not permitted.

**Why:** There is exactly one implementation: `MassTransitInterModuleRequestClient`. It always routes through MassTransit over RabbitMQ — regardless of whether the handler is registered in the same process or a remote one. MassTransit picks up the message from the queue whichever consumer is listening, local or remote. The caller's code is identical in both topologies. When the decision is made to run a module as a separate process, nothing at the call site changes. The communication contract was already remote-ready from the first line of code written.

---

### 4. IntegrationEvents

**Decision:** When a significant state change occurs in a module, a `DomainEventHandler` in that module publishes an `IntegrationEvent` record declared in `Common.IntegrationEvents`. Other modules that care about this fact implement `IConsumer<TIntegrationEvent>` and react independently. No module calls into another module to trigger this — it fires an event and forgets.

**Why:** This is a reactive pattern, not a communication pattern. The publishing module does not know or care who (if anyone) consumes the event. Consumers are decoupled from producers at every level: compile time, runtime, and deployment. A new module can start reacting to an existing event without any change to the publishing module.

**Contrast with `IInterModuleRequestClient`:** Use `IInterModuleRequestClient` when the caller needs a response and drives the interaction. Use `IntegrationEvents` when a module announces that something happened and other modules decide what to do about it.

---

### 5. Transactional Outbox

**Decision:** No application code calls `IPublishEndpoint` directly. Domain events are written to `OutboxMessages` by `BaseDbContext` in the same database transaction as the aggregate state change. The `OutboxProcessor` background service polls the table and publishes via MassTransit over RabbitMQ.

**Why:** Calling `IPublishEndpoint` directly from application code creates a dual-write problem — the database write and the publish are two separate operations. If the process crashes between them, the state is saved but the event is never published, or the event is published but the state change is rolled back. `BaseDbContext` eliminates this by writing to `OutboxMessages` atomically with the aggregate. Either both commit or neither does. The `OutboxProcessor` then polls with `SELECT ... FOR UPDATE SKIP LOCKED`, publishes via `IPublishEndpoint.Publish()`, and marks each message processed — all in a single transaction. Multiple processor instances running concurrently claim disjoint batches; no duplicate publishing.

---

### 6. Each module owns its own `DbContext`

**Decision:** Each module defines its own `DbContext` interface (`IIAMDbContext`, `IProductsDbContext`, etc.) and implementation. No module accesses another module's `DbContext`.

**Why:** This enforces data boundary ownership at the application layer. When a module is split into its own process, its `DbContext` connection string can be pointed to a separate database with a single configuration change — no code migration required. The data isolation exists architecturally before it exists physically.

**Default:** The PoC and development setup use a single shared Postgres database for convenience. This is a configuration default, not an architectural constraint.

**Tradeoff:** Cross-module queries that would be trivial with a shared `DbContext` (a JOIN across module tables) must instead be satisfied via `IInterModuleRequestClient` or event-driven denormalization. This adds friction to reads that span module boundaries — intentionally, because that friction signals that the read is crossing a bounded context and should be reconsidered.

---

### 7. DDD on writes, VSA on reads

**Decision:** Write operations follow Domain-Driven Design: `Endpoint → Aggregate.Method() → RaiseEvent() → SaveChangesAsync()`. Read operations follow Vertical Slice Architecture: `Endpoint → DbContext.AsNoTracking().Select(dto).SingleAsResultAsync()`.

**Why:** Writes need invariant protection. Aggregates enforce business rules, raise domain events, and make the state transition explicit. Reads have no invariants to protect — they just need to be fast and return the right shape. Forcing reads through an aggregate adds indirection with no benefit. Direct projection to DTOs via `.Select()` is more readable, more performant, and easier to tune.

**Rule:** Reads always use `.AsNoTracking()`. Reads never load an aggregate and project from it. Writes never use `.AsNoTracking()`.

---

### 8. Compiler-enforced module boundaries

**Decision:** Module `.csproj` files must not reference other module `.csproj` files. Architecture tests (NetArchTest) assert this and fail the build if violated. Cross-module contracts are declared in `Common.IntegrationEvents` and `Common.InterModuleRequests`, which both modules depend on, rather than one module depending on the other.

**Why:** Convention-based boundaries ("don't import from other modules") collapse under deadline pressure. The compiler does not have deadlines. If a cross-module reference is introduced, the build fails immediately, locally, before it reaches CI. This removes an entire class of architecture review comments.

---

### 9. Functional pipeline (Railway-Oriented)

**Decision:** Endpoint handlers chain operations using `BindAsync`, `TapAsync`, `MapAsync`, and `CombineAsync` rather than imperative `if (result.IsFailure) return ...` checks.

**Why:** Imperative early-return chains require reading every branch to understand the happy path. The functional pipeline makes the happy path the linear reading of the chain. Errors propagate automatically — a failed `BindAsync` short-circuits the rest of the chain without explicit checks at each step. The result is code where the reader understands the domain operation first and the error handling second.

---

## Honest Opinion

**This is not full microservices**. True microservices give you independent deployment cadences, independent data stores per team, independent release trains, and true team autonomy at the binary level. This architecture gives you *deployment isolation on demand* and *development simplicity always*. If your team needs truly independent deployment cadences, you will eventually want separate repositories. This architecture solves everything before that point — which is where most teams spend most of their careers.

---

## What "split deployment" actually means

Running a module as an isolated process requires:

1. **Process split:** Set `ModulesOptions__EnabledModules__0=ModuleName` on the instance. Done.
2. **Data split (optional):** Set the module's `DbContext` connection string to a separate database. Done.
3. **Communication:** Already works. `IInterModuleRequestClient` routes via RabbitMQ when the handler is not in the same process. No call sites change.
4. **Observability:** Already works. MassTransit propagates `TraceId` through RabbitMQ. The distributed trace spans both processes under a single ID in Aspire Dashboard / Jaeger with no additional wiring.

See [`split-deployment-poc.md`](split-deployment-poc.md) for a live walkthrough.
