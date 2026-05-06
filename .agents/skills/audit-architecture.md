---
description: Audit for architectural violations — cross-module refs, outbox misuse, missing AsNoTracking, localization drift.
---

Audit the codebase for architectural violations. Report all findings.

1. **Cross-module references**: scan all `.csproj` files in `src/Modules/`. FAIL if any module project references another module project (only `Common` references are permitted).

2. **Outbox violations**: search for `IPublishEndpoint.Publish` or direct `IBus.Publish` calls inside `Endpoints/` or write-side handlers. FAIL if found — Write paths must use `Aggregate.RaiseEvent(...)`. PASS if found only in `IntegrationEventHandler` (relaying is allowed).

3. **Controller usage**: confirm no class inherits `ControllerBase`. All HTTP handling must use Minimal APIs.

4. **Localization drift**: check for `IStringLocalizer` usage or raw string keys passed to error messages. FAIL if found — only `IResxLocalizer` (Aigamo.ResXGenerator) is permitted.

5. **Mapping library usage**: search for AutoMapper, Mapster, or any `.Map<>()` call that is not a LINQ projection. FAIL if found — only inline manual mapping is permitted.

6. **AsNoTracking coverage**: scan read query paths for missing `.AsNoTracking()`.

7. **Module registration**: confirm no `.Add{Module}()` calls are hardcoded in `Setup.Modules.cs`. Module list must come from `appsettings.json`.

Output: a categorized list of PASS / FAIL / WARNING for each check, with file paths and line numbers for every violation.
