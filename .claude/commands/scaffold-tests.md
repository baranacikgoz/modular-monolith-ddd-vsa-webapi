Scaffold failing tests (Red phase) before writing implementation. This enforces the Red → Green cycle.

1. **Review scope**: read the approved plan or issue to understand which endpoints and domain logic will be built.

2. **Unit tests**: in `src/Modules/{Module}/{Module}.Tests/`, write unit tests for pure domain logic and aggregate state mutations. Inherit from `AggregateTests` if applicable.

3. **Integration tests**: write slice integration tests implementing `IClassFixture<IntegrationTestFactory>` (or the module-specific factory). Retrieve services via `_factory.Services.CreateScope()`.

4. **Confirm Red state**: run `make test-{module}`. The new tests **must fail** (because the implementation does not yet exist). If they pass, the tests are not actually testing the planned behavior — revise them.

Do NOT touch production code during this command. The sole goal is a confirmed Red baseline.
