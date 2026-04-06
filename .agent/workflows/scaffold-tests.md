---
description: Scaffold failing slice tests (Red-Phase)
---
# Workflow: Scaffold Tests

This workflow enforces the Red-to-Green cycle by generating tests that explicitly fail before any business logic is written.

1. **Review Plan**: Read the `implementation_plan.md` artifact to understand the exact endpoints and domain logic being built.
2. **Unit Tests**: In the targeted module's `Tests` folder, write the Unit Tests mapping out the pure Domain logic and Aggregate state mutations. Ensure these inherit from `AggregateTests` if applicable.
3. **Integration Tests**: Write the Slice Integration Tests for the REPR Endpoints by implementing `IClassFixture<IntegrationTestFactory>` (or the module-specific factory like `OutboxTestWebAppFactory`) to automatically utilize the `Testcontainers` lifecycle. Retrieve services via scoped injection `_factory.Services.CreateScope()`.
4. **Confirm Red State**: Run `make test-[module]` (e.g., `make test-iam`) and inspect the output. You MUST confirm that the tests fail (due to unimplemented logic) to establish a valid baseline.
