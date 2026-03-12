# Refactor Plan: Sync Codebase with Agent Standards

## Goal Description
The objective is to strictly align the existing codebase with the architectural standards defined in `.agent/rules/`. This ensures a "fresh start" for future development, guaranteeing strict modular boundaries, correct REPR pattern usage, and robust testing infrastructure.

## User Review Required
> [!IMPORTANT]
> This plan assumes the current structure is "mostly" correct but needs strict enforcement of rules (e.g., `AsNoTracking`, `Testcontainers`, `Zero Warnings`).

## Proposed Changes

### Phase 1: Foundation & Audit (Common & Host)
- **Objective**: Ensure the core structure holds no business logic and enforces strict rules.
1.  **Audit `src/Common`**:
    -   Verify NO domain logic exists (only base classes, exceptions, extensions).
    -   Ensure `BaseDbContext` correctly handles the Outbox pattern.
2.  **Audit `src/Host`**:
    -   Verify `Setup.Modules.cs` explicitly registers modules (no reflection scanning).
    -   Verify `Program.cs` is minimal and delegates to `Setup.*`.
3.  **Global Linting**:
    -   Enable `TreatWarningsAsErrors` in `Directory.Build.props` (if exists) or all `.csproj` files.
    -   Fix all resulting warnings (Nullability, standard violations).

### Phase 2: Module Standardization (Iterative per Module)
*Execute this block for EACH module (Identity, Products, etc.)*

#### 1. API Layer (Endpoints)
-   **Structure Check**: Ensure `src/Modules/[Name]/Endpoints` contains specific Endpoint classes (FastEndpoints/Minimal API).
-   **Controller Ban**: Delete any `Controller` classes.
-   **DTOs**: Verify `Request` and `Response` records use `required` properties.

#### 2. Domain & Application
-   **Isolation**: Verify no references to other modules.
-   **Logic**: ensure complex logic is in Aggregates, not Services.

#### 3. Persistence (Infrastructure)
-   **Queries**: STRICTLY enforce `.AsNoTracking()` on all read queries in Endpoints/Queries.
-   **Commands**: Ensure `Aggregate.RaiseEvent` -> `Save` flow.
-   **Context**: Ensure `[Module]DbContext` exists and mimics `BaseDbContext`.

### Phase 3: Communication & Events
-   **Sync**: Verify usage of `IInterModuleRequest`.
-   **Async**: Verify `IntegrationEvents` are published via Outbox.
    -   Run `audit-architecture` check: ensure `IPublishEndpoint` is NOT used directly in Command handlers (must use Outbox).

### Phase 4: Testing & Reliability
-   **Infrastructure**: Verify `IntegrationTestWebAppFactory` uses `Testcontainers` (Postgres).
-   **Tests**:
    -   Update all integration tests to asserting on Side-Effects (DB state / Outbox message).
    -   Remove any mocks for `DbContext` or `MassTransit`.
    -   Ensure deterministic seeding for random data.

## Verification Plan

### Automated Verification
1.  **Architecture Audit**:
    -   Manually run the steps from `audit-architecture.md` (Check references, Check Outbox usage).
2.  **Build**:
    -   `dotnet build` must pass with **0 warnings**.
3.  **Tests**:
    -   `dotnet test` must pass all tests reliably.

### Handover Protocol
A specialized `refactor-handover.md` artifact will be maintained to track exactly which module is being worked on and the status of each checklist item.
