---
description: Final quality gate on an implemented feature — tests, architecture audit, code review checklist.
argument-hint: "<Module>"
allowed-tools: Read, Bash, Glob, Grep
---

Verify feature for module: $ARGUMENTS

1. **Run module tests**: `make test-{module}`. Fix compilation or logic errors until exit code is 0.

2. **Architecture audit**: run `/audit-architecture`. Resolve every FAIL before continuing.

3. **Boundary check**: inspect all `.csproj` files touched during implementation — zero cross-module project references.

4. **Code review checklist**:
   - All reads use `.AsNoTracking()`
   - No mapping library — only inline `.Select(x => new Response { ... })`
   - All logging uses `LoggerMessage` source generation
   - All localized strings use `IResxLocalizer`, not magic keys
   - No imperative `if (result.IsFailure)` blocks where functional pipeline applies
   - No branching on raw `string` query/route parameters — use a typed `enum`/`bool`/value instead
   - New endpoints registered in the feature's `Setup.cs`

5. **ReSharper/Rider inspections**:
   ```bash
   make inspect INCLUDE="<glob of changed files>"
   ```
   Fix real findings; known false positives (`NotAccessedPositionalProperty.Global`, `UnusedAutoPropertyAccessor.Global` on DTO/record props) can be ignored.

6. **Report**: what was verified, test results, ready-for-review confirmation.
