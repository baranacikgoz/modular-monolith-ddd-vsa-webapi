---
description: Quality gate: architecture audit, architecture tests, module or full test suite. Fix failures before reporting done.
argument-hint: "[Module]"
allowed-tools: Read, Bash, Glob, Grep
---

Verify: $ARGUMENTS

1. `/audit-architecture`. Resolve every FAIL.
2. Architecture tests: `dotnet test --project src/Common/Common.Tests/Common.Tests.csproj --filter-class "*Architecture*"`.
3. `make test-{module}` if a module is given, else `make test` (sequential, avoids Docker exhaustion).
4. A failing test means the implementation is wrong. Fix the code, never the test.
5. If migrations changed: `make check-migration-drift`.
6. Report: pass/fail counts per module, exit code, and any issues found per CLAUDE.md §10.
