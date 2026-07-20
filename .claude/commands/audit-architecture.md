---
description: Audit for architectural violations — cross-module refs, outbox misuse, AsNoTracking gaps, localization drift, mapping library usage, bare EF POCOs, request validation, consumer idempotency, test fixture isolation, hardcoded tunables.
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

6. **AsNoTracking coverage**: scan read query paths (`.Select(...)` chains) for missing `.AsNoTracking()`.

7. **Module registration**: confirm no `.Add{Module}()` calls are hardcoded in `Setup.Modules.cs`. Module list must come from `src/Host/Host/Configurations/modules.json` (`ModulesOptions.EnabledModules`).

8. **Functional pipeline (Golden Path)**: search Endpoint handlers for imperative `if (result.IsFailure)` / `if (result.IsSuccess)` branching. FAIL if found where `BindAsync`/`TapAsync`/`MapAsync`/`CombineAsync` chaining could apply instead.

9. **Persistence patterns**: FAIL if any read uses `.Find(...)`/`.FirstOrDefault(...)` instead of `.TagWith(...).SingleAsResultAsync(...)`; FAIL if a conditional filter uses `if (condition) query = query.Where(...)` instead of `.WhereIf(...)`; FAIL if `.GroupJoin(...).SelectMany(...)` is used instead of native `.LeftJoin`/`.RightJoin`.

10. **Logging**: search for interpolated/string-concatenated log calls (`logger.LogInformation($"...")` or `logger.LogInformation("..." + x)`). FAIL if found — only `[LoggerMessage]` source-generated `static partial` methods are permitted.

11. **DomainEvent versioning**: for any diff touching a shipped `V{n}...DomainEvent` record (or a VO nested in one), FAIL if properties were added/removed/renamed/retyped in place instead of introducing `V{n+1}...DomainEvent`.

12. **DI registration safety**: search for `services.BuildServiceProvider()` inside any DI registration code path. FAIL if found — corrupts OTel `TracerProvider`/`MeterProvider` singletons on GC finalization.

13. **Bare EF-mapped POCOs**: scan persisted types (referenced via `DbSet<T>` / EF `IEntityTypeConfiguration<T>`). FAIL if a persisted type is a naked `class` with hand-rolled properties instead of deriving from `AggregateRoot<TId>` (raises events), `AuditableEntity<TId>` (has `IStronglyTypedId` key, no events), non-generic `AuditableEntity` (natural/composite key), or being a `ValueObject`/owned type.

14. **Typed parameters over raw strings**: search Endpoint handlers for branching on raw `string` query/route parameters (e.g. `filter == "ARCHIVED"`). FAIL if found — must use a dedicated `enum`, `bool`, or strongly-typed value bound via ASP.NET Core model binding instead.

15. **Request validation coverage**: for every `Request.cs` under `Endpoints/*/Feature/`, confirm it contains an inline `RequestValidator` class implementing `CustomValidator<T>` (`src/Common/Common.Application/Validation/CustomValidator.cs`) at the bottom of the same file — not a separate `RequestValidator.cs`. FAIL if a Request has no validator.

16. **Consumer idempotency**: search for classes implementing `IConsumer<T>` directly. FAIL if found — all consumers must inherit `IntegrationEventHandlerBase<T>` (`src/Common/Common.Application/EventBus/IntegrationEventHandlerBase.cs`) and override `ProcessAsync` instead, so the FusionCache `processed_event:{id}` dedup applies.

17. **Test fixture isolation**: for test classes using `IClassFixture<TFactory>`, confirm `factory.CreateClient()` is called eagerly (field initializer/constructor), never lazily inside a test method body. FAIL if two or more test classes share the same factory type via separate `IClassFixture<T>` instead of `ICollectionFixture<T>` + `[Collection(...)]` — parallel boot corrupts shared static state (Serilog logger, OTel `ActivitySource`).

18. **Hardcoded tunables**: search new/changed code for a literal timeout, retry count, threshold, duration, limit, interval, or repeated template string embedded directly in application code. FAIL if found — it must be a property on an `Options` class instead (see CLAUDE.md "Tunable Values — Options Pattern Only"). PASS if the literal is structural (array size tied to an enum, a protocol magic number) rather than something an operator might reasonably want to change.
