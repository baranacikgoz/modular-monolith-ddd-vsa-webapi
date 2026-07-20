---
description: Run the full quality gate — architecture audit then complete test suite. Fix failures before reporting completion.
argument-hint: "[Module]"
allowed-tools: Read, Bash, Glob, Grep
---

Run quality gate: $ARGUMENTS

1. **Architecture audit**: run `/audit-architecture`. Resolve all FAILs before proceeding.

2. **Architecture tests**: run the NetArchTest boundary suite first for fast feedback — `dotnet test src/Common/Common.Tests/Common.Tests.csproj --filter FullyQualifiedName~Architecture` (`src/Common/Common.Tests/Architecture/ModuleBoundaryTests.cs`). Also covered by `make test-common` in step 3, but failing fast here saves a full suite run.

3. **Tests** — run module-specific if arg provided, full suite otherwise:
   ```bash
   make test-{module}   # if module specified
   make test            # full suite (runs sequentially to prevent Docker resource exhaustion)
   ```

4. **Failure handling**: if any test fails, analyze the stack trace and fix the implementation. Do NOT change the test to make it pass.

5. **ReSharper/Rider inspections** (catches what the Roslyn build does not — same verdicts Rider shows):
   ```bash
   make inspect INCLUDE="<glob of changed files>"   # scope to the diff
   make inspect                                      # whole solution
   ```
   Findings land in `jb-report.sarif`. Fix real findings in changed files. Known noise to ignore/suppress, not hand-edit: `NotAccessedPositionalProperty.Global` + `UnusedAutoPropertyAccessor.Global` (DTO/record props serialized by reflection — false positives).

6. **Report**: pass/fail counts per module, inspection findings count, overall exit code.
