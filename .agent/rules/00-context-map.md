# ARCHITECTURE & AGI CONTEXT MAP
> **CRITICAL**: Read this file to understand boundaries, patterns, and constraints before generating code.

## 1. High-Level Summary
- **Architecture**: Modular Monolith (.NET 10) with hybrid DDD (Writes) / VSA (Reads).
- **Core Principles**:
    - **Coupling**: Modules communicate *only* via `IntegrationEvents` (Async) or `Common.InterModuleRequests` (Sync).
    - **Consistency**: Transactional Outbox (Atomic Save + Publish).
    - **Observability**: Per-module Tracing (ActivitySource) and Metrics (Meter) are mandatory and registered via `IModule`.
- **Coding Style**: **Functional / Railway Oriented Programming**.
    -   We use a custom `Result<T>` Monad.
    -   We prefer **Method Chaining** over imperative `if/else` blocks.
    -   We use `Common.Infrastructure.Persistence.Extensions` for DB interactions.

## 2. The Result Monad Pipeline
The application flow is a pipeline of operations. If any step fails, the pipeline stops and returns the error.
*   **Query**: `db.Set<T>().SingleAsResultAsync(...)` (Returns `Result<T>`)
*   **Validate/Logic**: `.BindAsync(...)` or `.TapAsync(...)`
*   **Side Effect**: `.TapAsync(async _ => await db.SaveChangesAsync())`
*   **Response**: `.TransformResultTo...Response()`

## 3. Directory Structure Strategy
| Path | Responsibility | Rules |
| :--- | :--- | :--- |
| `/src/Host` | Composition Root | Configures DI, middleware, mounts modules. |
| `/src/Common` | Shared Kernel | **NO Business Logic**. Base classes only. |
| `/src/Modules` | Business Logic | Strict boundaries. No cross-module referencing. |
| `/src/Modules/*/Endpoints` | **REPR Pattern** | Minimal APIs. One class per file. |
| `/src/Modules/*/Infrastructure` | Persistence | EF Core, Repositories, ModuleInstaller. |

## 4. The "Magic" (Do Not Re-implement)
The system handles these cross-cutting concerns automatically. **Do not write manual code for these:**

1.  **Transactional Outbox & Auditing**:
    -   *Dev Action*: Call `Aggregate.RaiseEvent(new MyEvent())`.
    -   *System Action*: `BaseDbContext` intercepts save -> Atomically writes to `OutboxMessages` (for CDC) AND `AuditLog` (for history) tables -> Transaction commits.
2.  **CDC & Messaging**:
    -   *System Action*: **Debezium** reads Postgres WAL -> Pushes to Kafka.
    -   *System Action*: `MassTransit` consumes Kafka.
    -   *Rule*: **NEVER** write a Kafka producer in C# Application code.
3.  **Auditing**:
    -   *System Action*: `AuditingInterceptor` sets `CreatedOn`, `ModifiedBy`, etc.
4.  **Audit Log Retention**:
    -   *System Action*: `AuditLogRetentionService` (Background Job) periodically deletes entries older than the configured `RetentionDays` (default 30).
    -   *Rule*: Do NOT manually delete or modify `AuditLog` entries.

## 5. Infrastructure Services
-   **mm.postgres**: Logical WAL enabled (Source of Truth).
-   **mm.kafka**: KRaft mode (Event Bus).
-   **mm.debezium**: The Bridge (Postgres -> Kafka).

## 6. Testing Strategy (The Safety Net)
*   **Framework**: xUnit.
*   **Mocking**: NSubstitute (Only for external 3rd party APIs).
*   **Data**: Bogus (for generating fake test data).
*   **Integration**: **Testcontainers**. We run tests against REAL Postgres (and Kafka where applicable) instances.
    *   *Mechanism*: `Respawn` resets the Postgres DB checkpoint after every test (milliseconds overhead), while Kafka topics isolate messages to specific consumer groups.
*   **Assertion Rule**:
    *   **Writes**: Verify side-effects (Entity in DB? Outbox Message in DB?).
    *   **Reads**: Verify Response DTO matches expectation.

## 7. Observability in Debugging
- **Traceparent/TraceID**: Every request carries a TraceID. When a bug is reported via logs, the Agent should search for this ID in the `Host` or `Common` logs.
- **Span Events**: Look for 'Exception' events in OpenTelemetry spans. These contain the stack trace and the state of the local variables at the time of failure.
- **Database Logs**: Use the EF Core 'SensitiveDataLogging' (in Dev only) to see the exact SQL generated that caused the failure.
