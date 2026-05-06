---
description: Execute a planned refactoring incrementally with zero regressions.
---

Execute a planned refactoring with zero regressions. The plan must already exist and be approved.

1. **Review plan**: read the approved plan to understand exact scope and each file change.

2. **Green baseline**: run `make test-{module}` (or `make test` for multi-module) to confirm all existing tests pass before changing anything. If there are no tests for legacy code being changed, warn the user and pause.

3. **Execute incrementally**: apply changes file by file as defined in the plan. Convert imperative logic to functional pipelines, remove mapping libraries, enforce modular boundaries, etc.

4. **Verify continuously**: after each logical batch of changes, run `make build` to catch compilation errors early.

5. **Final quality gate**: run `make test-{module}` (or `make test`) to confirm zero regressions (Green state).

6. **Architecture audit**: inspect all modified `.csproj` files — confirm no cross-module references were introduced. Confirm `.AsNoTracking()` on reads and no mapping library usage.

7. **Summary**: report what changed, which files were modified, and the successful test output.
