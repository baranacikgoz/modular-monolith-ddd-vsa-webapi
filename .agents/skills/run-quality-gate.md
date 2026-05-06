---
description: Run the full quality gate — architecture audit then test suite. Fix failures before reporting completion.
argument-hint: "[Module]"
allowed-tools: Read, Bash, Glob, Grep
---

Run quality gate: $ARGUMENTS

1. **Architecture audit**: run `audit-architecture`. Resolve all FAILs before proceeding.

2. **Architecture tests**: if a `Tests/Architecture.Tests` project exists, run it first.

3. **Tests** — module-specific if arg provided, full suite otherwise:
   ```bash
   make test-{module}   # if module specified
   make test            # full suite
   ```

4. **Failure handling**: analyze stack traces and fix the implementation. Do NOT change the test to make it pass.

5. **Report**: pass/fail counts per module and overall exit code.
