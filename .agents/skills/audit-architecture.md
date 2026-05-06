---
description: Audit for architectural violations — cross-module refs, outbox misuse, AsNoTracking gaps, localization drift, mapping library usage.
argument-hint: ""
allowed-tools: Read, Bash, Glob, Grep
---

ultrathink

Audit the codebase for architectural violations. Report PASS / FAIL / WARNING per check with file paths and line numbers.

1. **Cross-module references**: scan all `.csproj` files in `src/Modules/`. FAIL if any module project references another module project (only `Common.*` references are permitted).

2. **Outbox violations**: search for `IPublishEndpoint.Publish` or `IBus.Publish` inside `Endpoints/` or write-side handlers. FAIL if found — Write paths must use `Aggregate.RaiseEvent(...)`. PASS if found only in `IntegrationEventHandler` (relaying is allowed).

3. **Controller usage**: confirm no class inherits `ControllerBase`. All HTTP handling must use Minimal APIs.

4. **Localization drift**: search for `IStringLocalizer` usage or raw string keys in error messages. FAIL if found — only `IResxLocalizer` (Aigamo.ResXGenerator) is permitted.

5. **Mapping library usage**: search for AutoMapper, Mapster, or `.Map<>()` calls outside LINQ `.Select(...)`. FAIL if found.

6. **AsNoTracking coverage**: scan read query paths for missing `.AsNoTracking()`.

7. **Module registration**: confirm no `.Add{Module}()` calls are hardcoded in `Setup.Modules.cs`. Module list must come from `appsettings.json`.
