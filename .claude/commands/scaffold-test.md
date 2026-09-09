---
description: Scaffold integration and unit tests for a feature. Use before implementation for a Red baseline, or after for coverage.
argument-hint: "<Module> <Feature> READ|WRITE [red]"
allowed-tools: Read, Edit, Write, Bash, Glob, Grep
---

Scaffold test: $ARGUMENTS

Rules in CLAUDE.md §8. Copy the shape from `CreateTests.cs` and `StoreTests.cs` (§2 exemplars).

1. Integration test in `src/Modules/{Module}/{Module}.Tests/Endpoints/{Aggregate}/{Feature}Tests.cs`: `[Collection("IntegrationTestCollection")]`, inherit `BaseIntegrationTest`, `Factory.CreateClient()` inside each test.
   - WRITE: arrange with Bogus, `PostAsJsonAsync`/`PutAsJsonAsync`, assert status, entity via `Factory.Services.CreateScope()` + `{Module}DbContext`, and a row in `OutboxMessages` with the expected event type.
   - READ: seed via scoped `DbContext`, `GetAsync`, assert the deserialized response matches the seed.
2. Unit test for new aggregate methods in `{Module}.Tests/{Aggregate}/{Aggregate}Tests.cs` (inherit `AggregateTests<TAggregate, TId>` when it fits): state after the call plus the raised event.
3. If a module has no factory yet: `IntegrationTestWebAppFactory : IntegrationTestFactory` overriding `GetActiveModules()` and an `IntegrationTestCollection` definition (Products has both).
4. Run `make test-{module}`. With `red`: the new tests must fail, and production code stays untouched. Without `red`: they must pass.
