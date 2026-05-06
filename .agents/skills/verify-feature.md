---
description: Run the final quality gate on an implemented feature — tests, architecture audit, code review checklist.
---

Run the final quality gate on an implemented feature before marking it done.

1. **Run module tests**: `make test-{module}` (or `make test` for multi-module features). Fix any compilation or logic errors until exit code is 0.

2. **Architecture audit**: run the `audit-architecture` skill. Resolve every FAIL before continuing.

3. **Boundary check**: inspect all `.csproj` files touched during implementation and confirm zero cross-module project references were added.

4. **Code review checklist**:
   - All reads use `.AsNoTracking()`
   - No mapping library usage — only inline manual mapping
   - All logging uses `LoggerMessage` source generation
   - All localized strings use `IResxLocalizer`, not magic keys
   - No imperative `if (result.IsFailure)` blocks where functional pipeline applies
   - New endpoints are registered in the feature's `Setup.cs`

5. **Report**: summarize what was verified, the test results, and confirm the feature is ready for review.
