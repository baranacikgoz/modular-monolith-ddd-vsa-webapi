# Refactor Handover Protocol

> **INSTRUCTION FOR AGENTS**: This file is the Source of Truth for the Refactor progress. When you start a session, READ THIS FILE first. When you finish, UPDATE THIS FILE with your exact progress.

## Current Status
**Phase**: `Phase 1: Foundation & Audit`
**Active Module**: `None`
**Last Action**: `Plan Created`

## Refactor Checklist

### Phase 1: Foundation (Common & Host)
- [x] Audit `src/Common` for Business Logic leaks
- [x] Verify `BaseDbContext` Outbox implementation
- [x] Verify `Host/Setup.Modules.cs` registration
- [x] Enforce `TreatWarningsAsErrors` globally
- [x] Fix all build warnings

### Phase 2: Module Standardization

#### Module: Products
- [x] **Endpoints**: REPR Pattern Check (No Controllers, FastEndpoints used)
- [x] **DTOs**: `required` properties on Request/Response
- [x] **Persistence**: `.AsNoTracking()` on Reads
- [x] **Domain**: Isolation check (No cross-module refs)
- [/] **Testing**: Unit Tests Restored. Testcontainers setup Pending.

#### Module: IAM
- [x] **Endpoints**: REPR Pattern Check
- [x] **DTOs**: `required` properties
- [x] **Persistence**: `.AsNoTracking()`
- [x] **Domain**: Isolation check
- [x] **Testing**: Testcontainers setup (Unit Tests restored)

#### Module: Notifications
- [x] **Endpoints**: REPR Pattern Check
- [x] **DTOs**: `required` properties
- [x] **Persistence**: `.AsNoTracking()`
- [x] **Domain**: Isolation check
- [x] **Testing**: Testcontainers setup (Unit Tests restored)

#### Module: BackgroundJobs
- [x] **Endpoints**: REPR Pattern Check
- [x] **DTOs**: `required` properties
- [x] **Persistence**: `.AsNoTracking()`
- [x] **Domain**: Isolation check
- [x] **Testing**: Testcontainers setup (Unit Tests restored)

#### Module: Outbox
- [x] **Verify**: Implementation matches System Rules (Context Map)

### Phase 3: Communication Audit
- [x] Verify `IInterModuleRequest` usage for Sync
- [x] Verify `MassTransit` Outbox usage (No direct Publish in handlers)

## Next Steps for Agent
1.  Pick the first unchecked item in the Checklist.
2.  Switch to **EXECUTION** mode.
3.  Perform the refactor/check.
4.  Run tests.
5.  Update this checklist with `[x]`.
6.  If session ends, save state here.
