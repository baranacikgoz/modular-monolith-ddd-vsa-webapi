---
description: Execute a planned refactoring incrementally with zero regressions.
argument-hint: "<Module>"
allowed-tools: Read, Edit, Write, Bash, Glob, Grep
---

Execute refactor for module: $ARGUMENTS

1. **Review plan**: read the approved plan from the current conversation.

2. **Green baseline**: run `make test-{module}` before changing anything. If there are no tests for the legacy code being changed, warn the user and pause.

3. **Execute incrementally**: apply changes file by file. Convert imperative logic to functional pipelines, remove mapping libraries, enforce modular boundaries, etc.

4. **Verify continuously**: after each logical batch, run `make build` to catch compilation errors early.

5. **Final quality gate**: run `make test-{module}` (or `make test`) to confirm zero regressions.

6. **Architecture audit**: inspect all modified `.csproj` files — no cross-module references. `.AsNoTracking()` on reads, no mapping library usage.

7. **Summary**: report which files changed and the successful test output.
