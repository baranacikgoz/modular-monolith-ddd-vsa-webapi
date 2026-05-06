---
description: Final quality gate on an implemented feature — tests, architecture audit, code review checklist.
argument-hint: "<Module>"
allowed-tools: Read, Bash, Glob, Grep
---

Verify feature for module: $ARGUMENTS

1. **Run module tests**: `make test-{module}`. Fix compilation or logic errors until exit code is 0.

2. **Architecture audit**: run `audit-architecture`. Resolve every FAIL before continuing.

3. **Boundary check**: inspect all `.csproj` files touched during implementation — zero cross-module project references.

4. **Code review checklist**:
   - All reads use `.AsNoTracking()`
   - No mapping library — only inline `.Select(x => new Response { ... })`
   - All logging uses `LoggerMessage` source generation
   - All localized strings use `IResxLocalizer`, not magic keys
   - No imperative `if (result.IsFailure)` blocks where functional pipeline applies
   - New endpoints registered in the feature's `Setup.cs`

5. **Report**: what was verified, test results, ready-for-review confirmation.
