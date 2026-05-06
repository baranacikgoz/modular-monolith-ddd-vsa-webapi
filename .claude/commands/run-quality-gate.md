---
description: Run the full quality gate — architecture audit then complete test suite. Fix failures before reporting completion.
argument-hint: "[Module]"
allowed-tools: Read, Bash, Glob, Grep
---

Run quality gate: $ARGUMENTS

1. **Architecture audit**: run `/audit-architecture`. Resolve all FAILs before proceeding.

2. **Architecture tests**: if a `Tests/Architecture.Tests` project exists, run it first (NetArchTest rules).

3. **Tests** — run module-specific if arg provided, full suite otherwise:
   ```bash
   make test-{module}   # if module specified
   make test            # full suite (runs sequentially to prevent Docker resource exhaustion)
   ```

4. **Failure handling**: if any test fails, analyze the stack trace and fix the implementation. Do NOT change the test to make it pass.

5. **Report**: pass/fail counts per module and overall exit code.
