---
description: Audit the codebase against CLAUDE.md rules. Reports PASS, FAIL, or WARNING per check with file:line.
argument-hint: ""
allowed-tools: Read, Bash, Glob, Grep
---

ultrathink

Audit the codebase. One line per check: PASS, FAIL, or WARNING, with `file:line` for every hit. Rule references point to CLAUDE.md.

1. Cross-module `ProjectReference` in any `src/Modules/**/*.csproj` (§1).
2. `IPublishEndpoint.Publish` or `IBus.Publish` outside `IntegrationEventHandler`s (§1 Outbox).
3. Any class deriving `ControllerBase` (§5).
4. `IStringLocalizer` or raw string localization keys (§5).
5. AutoMapper, Mapster, or `.Map<>()` outside `.Select` (§5).
6. Read queries (`.Select` projections) missing `.AsNoTracking()` (§4).
7. Hardcoded `.Add{Module}()` in `Setup.Modules.cs` (§1).
8. `if (result.IsFailure)` / `IsSuccess` branching in endpoints where a pipeline extension applies (§3).
9. `.Find(` / `.FirstOrDefault(` on entity fetches; `if (cond) query = query.Where`; `.GroupJoin(...).SelectMany(` (§4).
10. Interpolated or concatenated log strings (§5).
11. DomainEvent versioning: diff touching a shipped `V{n}` record in place; event property types off the allow-list; entity, aggregate, ValueObject, or domain enum used directly (§6). Run `DomainEventContractTests` for the verdict.
12. `services.BuildServiceProvider()` inside DI registration (§5).
13. Persisted types (`DbSet<T>`, `IEntityTypeConfiguration<T>`) that are bare classes; `OutboxMessage` is the only PASS exception (§4).
14. Endpoint branching on raw `string` route/query values (§5).
15. Any `Request.cs` without an inline `RequestValidator : CustomValidator<T>`; separate `RequestValidator.cs` files (§5).
16. Classes implementing `IConsumer<T>` directly instead of `IntegrationEventHandlerBase<T>` or `InterModuleRequestHandler<,>` (§1).
17. Test fixtures: two `IClassFixture<T>` on the same factory type; lazy `CreateClient()` under `IClassFixture` (§8).
18. Literal tunables (timeout, retry, threshold, duration, limit, interval, cron, template) in application code instead of Options (§9). Structural constants PASS.
19. `grep -rn 'ApplyEvent\|LoadFromHistory' --include='*.cs' src/` must be empty (§6).
20. Endpoints without `.RequireScope(...)` on a `RequireAuthorization` group; scopes missing from `keycloak/realm-modular-monolith.json` (§1 Identity).
