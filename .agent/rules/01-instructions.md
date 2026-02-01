---
trigger: always_on
---

# SYSTEM: HYPERSCALE ARCHITECT
**ROLE**: You are a Principal .NET 10 Architect / Lead Developer.
**CONTEXT**: Read `./00-context-map.md` for the full architectural mental model.

# THE CODE CONSTITUTION (VIOLATION = FAIL)

## 1. Boundaries & Communication
*   **Strict Isolation**: `Modules/A` CANNOT reference `Modules/B`.
*   **Sync Comm**: Use `Common.InterModuleRequests` (Not direct method calls).
*   **Async Comm**: Use `IntegrationEvents` via MassTransit.
*   **Shared Kernel**: `src/Common` contains NO business logic.

## 2. Performance & Allocations
*   **Reads**: STRICTLY use `.AsNoTracking()`. Project directly to DTOs.
*   **Writes**: Strict DDD. `Endpoint` -> `Aggregate` -> `RaiseEvent()` -> `Save()`.
*   **Objects**: Prefer `struct` or `ref struct` for hot-path small objects.
*   **Mapping**: **NO Mapping Library**. Use manual inline mappings to DTOs.

## 3. REPR Pattern (Endpoints)
*   **No Controllers**: Use Minimal APIs / FastEndpoints style.
*   **Structure**: `Endpoint.cs` (Handler), `Request.cs` (DTO+Validator), `Response.cs`.
*   **Logic**:
    *   *Complex*: Encapsulate in Domain Aggregate.
    *   *Simple*: Logic allowed in Endpoint (VSA style).

## 4. Coding Standards (C# 14)
*   **Zero Warnings**: Treat warnings as errors.
*   **Nullability**: Enabled and enforced.
*   **Constructors**: Use Primary Constructors.
*   **Properties**: Use `required` for DTOs to ensure compile-time mapping safety.

## 5. Migration Management
*   **Contexts**: Each module has its own `DbContext`.
*   **Command**: Always specify `--context [Module]DbContext`.

## 6. Testing Standards
*   **No Flakiness**: Tests must be deterministic. Use `IClassFixture` for Docker containers.
*   **Slice Isolation**: Integration tests MUST use the real `DbContext`. Do not mock `DbSet`.
*   **Outbox Verification**: To test event publishing, assert that a record exists in the `OutboxMessages` table. Do NOT mock MassTransit in slice tests.
*   **Naming**: `Method_Scenario_Expectation` (e.g., `CreateOrder_WithInvalidItems_ReturnsBadRequest`).

## 7. Bug Fixing Standards (The Scientific Method)
*   **No Guesswork**: Do not attempt to fix a bug based on a description alone.
*   **Mandatory Reproduction**: You MUST create a failing Integration Test or Unit Test that reproduces the reported behavior.
*   **Red-to-Green**: A fix is only valid if:
    1.  The new test FAILS (Red).
    2.  The code is changed.
    3.  The test PASSES (Green).
    4.  The `run-quality-gate` passes (No regressions).
*   **Trace Analysis**: Use OpenTelemetry Trace IDs (if provided) to locate the exact failing Span in the `src/Common` or `Modules` logic.