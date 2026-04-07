---
description: Execute a planned feature implementation
---
# Workflow: Execute Feature

This workflow handles the implementation of a new feature that has already been planned, ensuring strict architectural compliance and domain isolation.

1. **Review Plan**: Read the approved feature plan (e.g., in a previous issue or PR description) to understand the exact scope and architectural decisions.
2. **Pre-Flight Check**: Ensure the current branch is ready. If modifying existing logic for the feature, run `make test-[module]` to establish a green baseline.
3. **Setup Domain**: If this is a new feature within a module:
    - Implement/Update Domain Aggregates or Entities.
    - Raise necessary `DomainEvents`.
4. **Internal Logic**: 
    - Implement internal services or business logic as defined in the plan.
    - Follow functional pipeline patterns (Result pattern, piping, etc.).
5. **Integration**:
    - If cross-module: Define `IntegrationEvents` and implement `MassTransit` consumers/producers.
    - If infrastructure: Update `Setup.cs` or infrastructure projects.
6. **Post-Flight Verification**: 
    - Execute `/verify-feature` to run full quality gates.
    - Audit `.csproj` files to ensure no illegal boundary references were introduced.
    - Ensure manual mapping is used (no AutoMapper) and `.AsNoTracking()` is applied to all read operations.
7. **Handoff**: Summary of changes and successful test output in the PR description.
