---
description: Execute a planned refactoring incrementally with zero regressions.
argument-hint: "<Module>"
allowed-tools: Read, Edit, Write, Bash, Glob, Grep
---

Execute refactor for module: $ARGUMENTS

1. **Review plan**: read the approved plan from the current conversation.

2. **Green baseline**: run `make test-{module}` to confirm all existing tests pass before changing anything. If there are no tests for the legacy code being changed, warn the user and pause.

3. **Execute incrementally**: apply changes file by file as defined in the plan. Convert imperative logic to functional pipelines, remove mapping libraries, enforce modular boundaries, etc.

4. **Verify continuously**: after each logical batch of changes, run `make build` to catch compilation errors early.

5. **Final quality gate**: run `make test-{module}` (or `make test`) to confirm zero regressions.

6. **Architecture audit**: inspect all modified `.csproj` files — confirm no cross-module references were introduced. Confirm `.AsNoTracking()` on reads and no mapping library usage.

7. **Summary**: report which files changed and the successful test output.
