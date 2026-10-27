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
4. `IStringLocalizer` or raw string localization keys (§5). PASS exception: `StringLocalizerExtensions.LocalizeFromError` and its caller `ResultToResponseTransformer` (a runtime `Error.Key` lookup).
5. AutoMapper, Mapster, or `.Map<>()` outside `.Select` (§5).
6. Read queries (`.Select` projections) missing `.AsNoTracking()` (§4).
7. Hardcoded `.Add{Module}()` in `Setup.Modules.cs` (§1).
8. `if (result.IsFailure)` / `IsSuccess` branching in endpoints where a pipeline extension applies (§3). PASS exception: a compensation after a failed external call, or a save before and after one, when the endpoint's doc says so.
9. `.Find(` / `.FirstOrDefault(` on entity fetches; `if (cond) query = query.Where`; `.GroupJoin(...).SelectMany(` (§4). PASS exception: `FindAsync` in the keyed projection upsert helpers and in `ProductSummariesReconciliationJob` and `StoreAccessReconciliationJob`, and a commented `FirstOrDefaultAsync` that is not an id lookup.
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
21. Aggregate methods or constructors taking one of their own child entities' ids (bare, nullable, or inside a collection) instead of the child entity (§4). Run `AggregateChildEntityParameterTests` for the verdict.
22. Heavy work shape (§12): a `JobRow<TId>` subclass that raises events or derives from `AggregateRoot`; a `*JobRunner.cs` with neither `TryClaimAsync` nor a lease (`StockPushLease`); a runner that loads tracked rows in a loop without `ChangeTracker.Clear()`; the `ConsumerDefinition<T>` of a queue consumer that does not call `SetConcurrency(options.X)` with one value for both prefetch and concurrency; an endpoint taking an `IFormFile` that queues work and does not end in `.TransformResultToAcceptedResponse<`. PASS exceptions: `InterModuleRequestHandlerDefinition` (two options on purpose), `ProductExportJobRunner` (`AsNoTracking` keyset pages, nothing tracked), `AddImage` (one bounded object, no job).
23. Ledger single write path (§4): `grep -rn 'StockMovement.Create(\|new StockMovement' src/` hits outside `StockLevel.cs` and `StockMovement.cs`; `StockMovements.Add`/`AddRange`/`Update`/`Remove`/`ExecuteUpdate`/`ExecuteDelete`; a raw SQL string naming `"StockMovements"`. Run `Inventory.Tests/Architecture/StockMovementWriteGuardTests` for the verdict, and give every new ledger the same test.
24. Projection writes (§4, §6): a class deriving `ProjectionEntity` (`grep -rn ': ProjectionEntity' src/`) that is written other than through `UpsertIfNewerAsync` in an `IntegrationEventHandlerBase` or by its `ProjectionReconciliationJob`; a pager request (`grep -rn 'Get[A-Za-z]*PageRequest' src/Modules/*/*.Endpoints`) used on a request path.
25. Secrets (§1 Secret protection): `grep -rn 'AesGcm\|Aes.Create\|ProtectedData\|Rfc2898' src/` hits outside `Common.Infrastructure/Security`; a persisted property named `*Secret*`, `*Credential*`, `*ApiKey*` or `*Password*` in a `*.Domain` project that is not `Encrypted`-prefixed, filled from `SecretProtector.Encrypt` or a hash; a `VerifySha256(` call whose secret comes from `IOptions` or a constant instead of a per-connection value decrypted through `SecretProtector`.
26. Raw SQL (§4): `grep -rln 'ExecuteSql\|FromSql\|SqlQuery<' src/` hits outside migrations and tests other than the named single writers (`OutboxSaveHelper`, `OutboxProcessor`, `StockCounterWriter`, `StockPushMarks`, `StockLevelRowLock`, `StockPushLease`). WARNING for any other hit unless a doc comment explains why.
27. Actor names (§1 Bulk user lookup): `new GetUsersRequest(` outside `Common.Endpoints/ActorNames.cs` and tests (one IAM request per page, never per row).
28. Constraint violations (§4): `DbUpdateException` or `UniqueConstraintException` caught in a `*.Endpoints` project (`grep -rn 'DbUpdateException\|UniqueConstraintException' src/Modules/*/*.Endpoints`); endpoints save through `SaveChangesDetectingUniqueViolationAsync` or `SaveChangesDetectingConflictAsync` and return the mapped `Error`.
29. Anonymous endpoints (§5): `.AllowAnonymous()` on an endpoint that writes or costs money without `.RequireRateLimiting(`; an anonymous body that carries a credential whose path is missing from `SensitiveRequestBodyPaths` in `requestLogging.json`. PASS exceptions: the captcha client key and the split-deployment probe (read-only).
30. Status transition table (§4): `grep -rn 'CanTransition(' src/ --include='*.cs'` hits outside `ReservationStatusTransitions.cs`, `StockReservation.CanMoveTo` and tests; `grep -rn 'Status = ReservationStatus\.' src/ --include='*.cs'` hits outside `StockReservation.cs`; inside it, an assignment that is neither `Reserve` (the initial status) nor preceded by a `CanMoveTo` check or the `Committed` branch of `Commit`; a provider status word (`grep -rniE '"(held|confirmed|released|expired)"' src/Modules/Inventory --include='*.cs'`) spelled outside `ProviderStatusMap.cs` and tests. Run `ReservationStatusTransitionsTests` and `StockReservationTests` for the verdict, and give every other status an outside system reports the same table, map and tests.
