---
description: Scaffold failing Red-phase tests before writing any implementation (enforces Red → Green cycle).
argument-hint: "<Module>"
allowed-tools: Read, Edit, Write, Bash, Glob, Grep
---

Scaffold Red-phase tests for module: $ARGUMENTS

1. **Review scope**: read the approved plan or issue to understand which endpoints and domain logic will be built.

2. **Unit tests**: in `src/Modules/{Module}/{Module}.Tests/`, write unit tests for pure domain logic and aggregate state mutations. Inherit from `AggregateTests` if applicable.

3. **Integration tests**: write slice integration tests implementing `IClassFixture<{Module}TestFactory>`. Retrieve services via `_factory.Services.CreateScope()`.

4. **Confirm Red state**: run `make test-{module}`. The new tests **must fail** — because the implementation does not yet exist. If they pass, the tests are not testing the planned behavior; revise them.

Do NOT touch production code. The sole goal is a confirmed Red baseline.
