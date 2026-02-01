# ARCHITECTURE & AGI CONTEXT MAP
> **CRITICAL**: Read this file to understand boundaries, patterns, and constraints before generating code.

## 1. High-Level Summary
- **Architecture**: Modular Monolith (.NET 10) with hybrid DDD (Writes) / VSA (Reads).
- **Core Principles**:
    - **Coupling**: Modules communicate *only* via `IntegrationEvents` (Async) or `Common.InterModuleRequests` (Sync).
    - **Consistency**: Transactional Outbox (Atomic Save + Publish).
    - **Observability**: OpenTelemetry Tracing/Metrics are mandatory.

## 2. Directory Structure Strategy
| Path | Responsibility | Rules |
| :--- | :--- | :--- |
| `/src/Host` | Composition Root | Configures DI, middleware, mounts modules. |
| `/src/Common` | Shared Kernel | **NO Business Logic**. Base classes only. |
| `/src/Modules` | Business Logic | Strict boundaries. No cross-module referencing. |
| `/src/Modules/*/Endpoints` | **REPR Pattern** | Minimal APIs. One class per file. |
| `/src/Modules/*/Infrastructure` | Persistence | EF Core, Repositories, ModuleInstaller. |

## 3. The "Magic" (Do Not Re-implement)
The system handles these cross-cutting concerns automatically. **Do not write manual code for these:**

1.  **Transactional Outbox**:
    -   *Dev Action*: Call `Aggregate.RaiseEvent(new MyEvent())`.
    -   *System Action*: `BaseDbContext` intercepts save -> Writes to `OutboxMessages` table -> Transaction commits.
2.  **CDC & Messaging**:
    -   *System Action*: **Debezium** reads Postgres WAL -> Pushes to Kafka.
    -   *System Action*: `MassTransit` consumes Kafka.
    -   *Rule*: **NEVER** write a Kafka producer in C# Application code.
3.  **Auditing**:
    -   *System Action*: `AuditingInterceptor` sets `CreatedOn`, `ModifiedBy`, etc.

## 4. Infrastructure Services
-   **mm.postgres**: Logical WAL enabled (Source of Truth).
-   **mm.kafka**: KRaft mode (Event Bus).
-   **mm.debezium**: The Bridge (Postgres -> Kafka).

## 5. Testing Strategy (The Safety Net)
*   **Framework**: xUnit + FluentAssertions.
*   **Mocking**: NSubstitute (Only for external 3rd party APIs).
*   **Data**: Bogus (for generating fake test data).
*   **Integration**: **Testcontainers**. We run tests against a REAL Postgres instance.
    *   *Mechanism*: `Respawn` resets the DB checkpoint after every test (milliseconds overhead).
*   **Assertion Rule**:
    *   **Writes**: Verify side-effects (Entity in DB? Outbox Message in DB?).
    *   **Reads**: Verify Response DTO matches expectation.
