# Architecture Decision Record

This document explains the *why* behind the architectural decisions in this codebase: not just what was built, but what problem each decision solves and what tradeoffs it carries. It is intended for engineers evaluating the design, contributing to the project, or using it as a starting point.

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

This architecture removes "extract later" from the equation entirely, because there is nothing to extract.

---

## Design Goals

1. **Develop as a monolith, deploy as microservices**: same codebase, same binary, one environment variable controls the process topology.
2. **Boundaries enforced by the compiler, not convention**: a developer cannot accidentally create a cross-module dependency that the build does not catch.
3. **Communication contracts that survive split deployment without rewriting**: the call site that works in-process must work cross-process identically.
4. **Complexity arrives when the business earns it**: shared database with different schemas per module, single binary, and single pipeline are defaults. Splitting any of them is a config change, not a code change.

---

## Key Decisions

### 1. Configuration-driven module loading

**Decision:** The host scans all loaded assemblies at startup for `IModule` implementations and activates only the ones listed in `ModulesOptions.EnabledModules`. No module registers itself via a hardcoded `services.AddXModule()` call.

**Why:** Hardcoded registration means the process topology is a code decision. Making it a configuration decision means the same binary can run as a full monolith (`"*"`), a two-module subset (`["IAM", "Products"]`), or a single-module isolated process, with zero code changes, at deploy time.

---

### 2. Two module tiers: `IModule` and `ICoreModule`

**Decision:** Modules that are infrastructure dependencies for all other modules (Outbox, BackgroundJobs) implement `ICoreModule` and load on every instance regardless of configuration. All other modules implement `IModule` and are opt-in.

**Why:** Outbox and BackgroundJobs are not business modules, they are the platform. Every instance that performs database writes uses `BaseDbContext`, which atomically writes domain events to `OutboxMessages` in the same transaction as the aggregate state change. For that to work, the Outbox module's EF configuration, `IOutboxDbContext`, and interceptors must be registered in DI on every writing instance, not because the Outbox processor needs to run everywhere, but because the transactional write infrastructure is a dependency of the module's own `DbContext`. BackgroundJobs manages scheduled infrastructure work. Requiring operators to remember to include both in every deployment configuration would be an error surface that serves no purpose.

---

### 3. `IInterModuleRequestClient<TRequest, TResponse>` as the only synchronous cross-module communication path

**Decision:** All synchronous requests between modules must go through `IInterModuleRequestClient<TRequest, TResponse>`. Direct method calls, shared services, or HTTP between modules are not permitted.

**Why:** There is exactly one implementation: `MassTransitInterModuleRequestClient`. It always routes through MassTransit over RabbitMQ, regardless of whether the handler is registered in the same process or a remote one. MassTransit picks up the message from the queue whichever consumer is listening, local or remote. The caller's code is identical in both topologies. When the decision is made to run a module as a separate process, nothing at the call site changes. The communication contract was already remote-ready from the first line of code written.

**Failure semantics:** because the call can always cross a process boundary, it is built to fail like one. Each handler has its own receive endpoint with no retry, and its timeout equals what the caller waits (`InterModuleRequestOptions`, overridable per request). A timed-out request reaches the HTTP client as `503` with `Retry-After`, a handler that threw as `502`. A caller that only enriches a response with another module's answer (the stock level on a product read) degrades instead of failing.

---

### 4. IntegrationEvents

**Decision:** When a significant state change occurs in a module, a `DomainEventHandler` in that module collects an `IntegrationEvent` record declared in `Common.IntegrationEvents` into the outbox. Other modules that care about this fact inherit `IntegrationEventHandlerBase<TIntegrationEvent>` and react independently. No module calls into another module to trigger this, it fires an event and forgets. Consumers never implement `IConsumer<T>` directly, because the base class carries the idempotency guarantee (see decision 10).

**Why:** This is a reactive pattern, not a communication pattern. The publishing module does not know or care who (if anyone) consumes the event. Consumers are decoupled from producers at every level: compile time, runtime, and deployment. A new module can start reacting to an existing event without any change to the publishing module.

**Contrast with `IInterModuleRequestClient`:** Use `IInterModuleRequestClient` when the caller needs a response and drives the interaction. Use `IntegrationEvents` when a module announces that something happened and other modules decide what to do about it.

**A third option, the projection:** when a module reads another module's data on its request path, keep a local read model fed by that module's events instead of calling it. Reserve the synchronous request for calls where the answer is needed before the caller can proceed and where the request makes no sense while the source module is down.

---

### 5. Transactional Outbox

**Decision:** No application code calls `IPublishEndpoint` directly. Domain events are written to `OutboxMessages` by `BaseDbContext` in the same database transaction as the aggregate state change. The `OutboxProcessor` background service polls the table and publishes via MassTransit over RabbitMQ.

**Why:** Calling `IPublishEndpoint` directly from application code creates a dual-write problem: the database write and the publish are two separate operations. If the process crashes between them, the state is saved but the event is never published, or the event is published but the state change is rolled back. `BaseDbContext` eliminates this by writing to `OutboxMessages` atomically with the aggregate. Either both commit or neither does.

The `OutboxProcessor` then claims a batch with a single `UPDATE ... RETURNING` that selects rows `FOR UPDATE SKIP LOCKED` and pushes each row's `NextRetryAt` into the future (a lease). That statement commits at once, so no database lock or transaction is held while publishing. The processor publishes the claimed rows to RabbitMQ in parallel (`PublishConcurrency`), then records each outcome in one save: processed, or retry with exponential backoff. Multiple processors running concurrently claim disjoint batches.

**Tradeoff:** if a processor crashes after claiming and before publishing, the lease expires and another instance publishes the row, which can deliver an event twice. That is acceptable by design because every consumer is idempotent (decision 10), and in exchange a slow or down broker can never hold database connections or locks. For the same reason delivery order is not guaranteed (decision 11).

---

### 6. Each module owns its own `DbContext`

**Decision:** Each module defines its own `DbContext` interface (`IProductsDbContext`, `INotificationsDbContext`, etc.; IAM owns no database, it brokers Keycloak) and implementation. No module accesses another module's `DbContext`.

**Why:** This enforces data boundary ownership at the application layer. When a module is split into its own process, its `DbContext` connection string can be pointed to a separate database with a single configuration change, no code migration required. The data isolation exists architecturally before it exists physically.

**Default:** The PoC and development setup use a single shared Postgres database for convenience. This is a configuration default, not an architectural constraint.

**Tradeoff:** Cross-module queries that would be trivial with a shared `DbContext` (a JOIN across module tables) must instead be satisfied via `IInterModuleRequestClient` or event-driven denormalization. This adds friction to reads that span module boundaries, intentionally, because that friction signals that the read is crossing a bounded context and should be reconsidered.

---

### 7. DDD on writes, VSA on reads

**Decision:** Write operations follow Domain-Driven Design: `Endpoint → Aggregate.Method() → RaiseEvent() → SaveChangesAsync()`. Read operations follow Vertical Slice Architecture: `Endpoint → DbContext.AsNoTracking().Select(dto).SingleAsResultAsync()`.

**Why:** Writes need invariant protection. Aggregates enforce business rules, raise domain events, and make the state transition explicit. Reads have no invariants to protect, they just need to be fast and return the right shape. Forcing reads through an aggregate adds indirection with no benefit. Direct projection to DTOs via `.Select()` is more readable, more performant, and easier to tune.

**Rule:** Reads always use `.AsNoTracking()`. Reads never load an aggregate and project from it. Writes never use `.AsNoTracking()`.

---

### 8. Compiler-enforced module boundaries

**Decision:** Module `.csproj` files must not reference other module `.csproj` files. Architecture tests (NetArchTest) assert this and fail the build if violated. Cross-module contracts are declared in `Common.IntegrationEvents` and `Common.InterModuleRequests`, which both modules depend on, rather than one module depending on the other.

**Why:** Convention-based boundaries ("don't import from other modules") collapse under deadline pressure. The compiler does not have deadlines. If a cross-module reference is introduced, the build fails immediately, locally, before it reaches CI. This removes an entire class of architecture review comments.

---

### 9. Functional pipeline (Railway-Oriented)

**Decision:** Endpoint handlers chain operations using `BindAsync`, `TapAsync` (and `TapWhenAsync` for conditional side effects), `MapAsync`, `CombineAsync` and `TapActivityAsync` rather than imperative `if (result.IsFailure) return ...` checks.

**Why:** Imperative early-return chains require reading every branch to understand the happy path. The functional pipeline makes the happy path the linear reading of the chain. Errors propagate automatically, a failed `BindAsync` short-circuits the rest of the chain without explicit checks at each step. The result is code where the reader understands the domain operation first and the error handling second.

**Where it stops:** two cases stay imperative, and each is explained in the endpoint's own documentation. A compensation after a failed external call, and a flow that must save once before an external call and again after it, because the attempt marker has to be durable before the network call (see `Inventory.Endpoints/StockReservations/v1/Release`).

---

### 10. Idempotent consumers: a cache pre-filter and a transactional inbox

**Decision:** Every integration event consumer inherits `IntegrationEventHandlerBase<T>`. It first checks a FusionCache key (`processed_event:{consumer}:{eventId}`) as a fast, best-effort pre-filter. It then tracks a `ProcessedMessage` row in the consuming module's own `DbContext` before `ProcessAsync` runs, so the handler's own `SaveChangesAsync` commits the inbox mark and the side effects together. A redelivery after commit, or a second instance, hits the composite key and is acknowledged as a duplicate. The cache key is written only after the commit. `InboxCleanupJob` prunes old rows.

**Why:** The outbox gives at-least-once delivery (decision 5), so duplicates are expected, not exceptional. A cache alone is not a guarantee: it can be evicted, and distributed cache errors are swallowed. Putting the inbox mark in the same transaction as the side effects is what makes "process exactly once" true for state in the module's own database. Because the inbox table lives in the module's own schema, it moves with the module when it is split into its own process.

**Tradeoff:** A handler can also need a domain-level duplicate check on top, for two distinct messages announcing the same fact, which no per-message inbox can tell apart.

---

### 11. No ordering guarantee, projections for the cases that need order

**Decision:** Consumers never assume events arrive in the order they were raised. When order matters (a read model), the source version travels with the event and the consumer applies it with `UpsertIfNewerAsync`, which drops a stale or duplicate event. A reconciliation job can heal projection rows that missed an event.

**Why:** The processor publishes a claimed batch in parallel and consumers run concurrently, so order is not preserved. Pretending otherwise produces bugs that only appear under load. Making staleness a first-class, tested outcome removes the whole class.

---

### 12. Keycloak owns identity, the API brokers it

**Decision:** Users, roles, sessions, refresh-token rotation and fine-grained permissions live in Keycloak, versioned as code in `keycloak/realm-modular-monolith.json`. Clients only ever talk to this API. The API verifies a one-time code itself, then asks Keycloak for tokens through a dedicated `trusted-login` flow, and checks each request with a `resource#scope` decision from Keycloak Authorization Services, cached per token. IAM has no database of its own.

**Why:** Passwords, sessions, token rotation and a permission model are expensive to build and easy to get wrong. A policy change in Keycloak takes effect without a deploy, the realm file reviews like code, and tests boot the same realm in a container. `PermissionCoverageTests` keeps endpoint scopes and the realm in sync.

**Tradeoff:** Keycloak is a runtime dependency of sign-in and of permission decisions. A cached decision does not need Keycloak, a new one does. Its health is therefore reported on a separate dependency probe rather than gating readiness. The `trusted-login` client secret is as sensitive as every user's password and must stay on the API's network (see [`../keycloak/README.md`](../keycloak/README.md)).

---

### 13. Domain events are versioned and frozen

**Decision:** A shipped `V{n}...DomainEvent` record never changes: no added, removed, renamed or retyped property. A new shape ships as `V{n+1}`, raised from the command method, and `V{n}` stays in the tree. Event payloads carry only primitives, ids, snapshots and nested records, never entities. `IntegrationEvent`s are exempt, because they are contracts between deployments rather than history.

**Why:** Domain events are stored in the audit log by their CLR type name. If an old event changes shape, the history that was written under the old shape can no longer be read. A contract test (`DomainEventContractTests`) enforces the allowed payload types.

---

### 14. Rules are tests and startup checks, not conventions

**Decision:** Whatever can be checked mechanically is. Architecture tests cover module boundaries, event contracts, strongly typed id queries and aggregate method signatures. `PermissionCoverageTests` ties endpoint scopes to the realm. Every options class has a validator that runs at startup, and in Production those validators reject placeholder providers (`Dummy` SMS, email, push, captcha and warehouse), a fixed OTP code, an in-memory-only cache and SignalR without a backplane. `MigrationGuard` refuses to start against an unmigrated database, and CI fails when a model migration has no committed SQL.

**Why:** Prose rules are followed when someone remembers them. A failing build is followed always, including by contributors and AI agents who never read the document.

---

### 15. Schema changes are reviewed SQL, configuration is a deploy-time artifact

**Decision:** The application never migrates the database itself. Migrations are scripted to idempotent SQL under `migrations/{Module}`, reviewed, and applied by a DBA or deploy step. Configuration files are excluded from the published image and materialized at deploy time under the same file names, loaded with `optional: false` so a missing file stops the host. Environment variables override any value.

**Why:** One image is promoted unchanged from dev to production and only the mounted configuration differs. Secrets never live in the image, and a deploy that forgets its configuration fails loudly instead of booting on placeholders. The cost is a deliberate local step (`make ef-update-all`) to create a development database.

---

### 16. Failure is assumed at every external edge

**Decision:** Outbound HTTP clients get a timeout, bounded retries and a circuit breaker from `ResiliencyOptions`, and a third party with per-key quotas gets a keyed pipeline (own rate limiter, breaker and timeout per key). Inbound endpoints that cost money or enable abuse (SMS and email sends, OTP verification, registration, token creation) have named rate limit policies that share one counter across replicas when Redis is configured.

**Why:** In a split deployment every module boundary is a network. Resilience that is added per call site later is the work this architecture exists to avoid, so it is part of the platform from the start.

---

## Honest Opinion

**This is not full microservices**. True microservices give you independent deployment cadences, independent data stores per team, independent release trains, and true team autonomy at the binary level. This architecture gives you *deployment isolation on demand* and *development simplicity always*. If your team needs truly independent deployment cadences, you will eventually want separate repositories. This architecture solves everything before that point, which is where most teams spend most of their careers.

---

## What "split deployment" actually means

Running a module as an isolated process requires:

1. **Process split:** Set `ModulesOptions__EnabledModules__0=ModuleName` on the instance. Done.
2. **Data split (optional):** Set the module's `DbContext` connection string to a separate database. Done.
3. **Communication:** Already works. `IInterModuleRequestClient` routes via RabbitMQ when the handler is not in the same process. No call sites change.
4. **Observability:** Already works. MassTransit propagates `TraceId` through RabbitMQ. The distributed trace spans both processes under a single ID in Aspire Dashboard / Jaeger with no additional wiring.

See [`split-deployment-poc.md`](split-deployment-poc.md) for a live walkthrough.

---

## Related documents

- [`getting-started.md`](getting-started.md): run the system, make the first requests, run the tests and the load test.
- [`modules.md`](modules.md): every module's routes, permissions, events, requests, jobs and settings.
- [`platform.md`](platform.md): the shared building blocks and how to use them.
- [`full-text-search.md`](full-text-search.md): multilingual, accent-insensitive, relevance-ranked search: design decisions, the write→query mechanism, and how to extend and maintain it.
- [`split-deployment-poc.md`](split-deployment-poc.md): a runnable two-process deployment with no HTTP between the processes.
- [`ai-workflow.md`](ai-workflow.md): the rules, commands and guards that let AI agents work in this repository.
- [`../keycloak/README.md`](../keycloak/README.md): the realm as code, the permission model and secrets.
- [`../src/Host/Host/Configurations/README.md`](../src/Host/Host/Configurations/README.md): the deploy-time configuration model.
