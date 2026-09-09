# Project Instructions

You are the Principal .NET 10 Architect for this repo: Modular Monolith, DDD on writes, VSA on reads. Every rule below is mandatory unless the user says otherwise. Slash commands in `.claude/commands/` reference these rules by section number; they do not restate them.

## 0. Discovery: graphify first

Search order, no exceptions:
1. `graphify query "<q>"` (BFS), `--dfs` (trace a path), `--budget N` (cap tokens), `graphify path "<A>" "<B>"`, `graphify explain "<concept>"`. Run the CLI via Bash; do not load the Skill for queries.
2. `graphify-out/GRAPH_REPORT.md` for god nodes and communities.
3. grep/find/glob only when graphify returns nothing useful.

## 1. Architecture

- Modules talk only via `IntegrationEvents` (async, MassTransit over RabbitMQ) or `Common.InterModuleRequests` (sync). No module `.csproj` references another module `.csproj` (hooks and `ModuleBoundaryTests` enforce this).
- `src/Common` is shared kernel only: zero business logic.
- Modules load from `src/Host/Host/Configurations/modules.json` (`ModulesOptions.EnabledModules`). Never hardcode `.Add{Module}()` in `Setup.Modules.cs`.
- Infra containers: `mm.postgres`, `mm.rabbitmq`, `mm.redis`, `mm.keycloak`, `mm.aspire-dashboard`.

| Module | Test target | Notes |
| :-- | :-- | :-- |
| IAM | `make test-iam` | Keycloak broker: OTP, token proxy, Admin REST queries, JwtBearer + Authorization Services. No DB. Tests boot Keycloak Testcontainer with `keycloak/realm-modular-monolith.json` |
| Products | `make test-products` | Standard DDD module. Use as the exemplar |
| Inventory | `make test-inventory` | Advanced patterns: saga-shaped aggregate (`StockReservation`), sweep job, provider-switch gateway with resiliency, HMAC webhook, cross-module wiring |
| Outbox | `make test-outbox` | Transactional outbox worker, single project |
| Notifications | `make test-notifications` | SMS/OTP, push (FCM), SignalR, device registry. Endpoints live in `Infrastructure/Devices` |
| BackgroundJobs | `make test-backgroundjobs` | Quartz/Hangfire jobs, single project |

Full module layout: `src/Modules/{M}/{M}.Domain` (aggregates, `DomainEvents/v1`, errors, ids), `{M}.Application` (`I{M}DbContext`, `DomainEventHandlers/v1`, `IntegrationEventHandlers`), `{M}.Endpoints` (`{Aggregate}/Setup.cs`, `{Aggregate}/v1/{Feature}/`, `{M}Module.cs`), `{M}.Infrastructure` (DbContext, EF config, `InterModuleRequestHandlers`, `Telemetry`, migrations), `{M}.Tests`. Each project has an empty `IAssemblyReference` marker.

Host: `src/Host/Host` (composition root). Contracts: `src/Common/Common.IntegrationEvents/{SourceModule}.cs`, `src/Common/Common.InterModuleRequests/{SourceModule}/{Name}.cs`.

### Platform services (never re-implement)

| Concern | Mechanism | Rule |
| :-- | :-- | :-- |
| Outbox | `Aggregate.RaiseEvent(e)`; `BaseDbContext` writes `OutboxMessages` + `AuditLog` atomically; `OutboxProcessor` publishes | Never call `IPublishEndpoint` from application code |
| Consumer idempotency | `IntegrationEventHandlerBase<T>` dedupes on FusionCache key `processed_event:{event.Id}` | Inherit it, override `ProcessAsync`. Never implement `IConsumer<T>` directly |
| Auditing | `ApplyAuditingInterceptor` sets `CreatedOn`, `ModifiedBy`, etc. `AuditLogRetentionService` prunes | Never set audit fields or delete `AuditLog` rows by hand |
| DomainEvent audit | `AuditLog` stores events by CLR type name (`PolymorphicEventConverter`) | Shipped `V{n}` events are frozen (§5) |
| Identity | Keycloak owns users, roles, sessions, permissions (client `backend-api`). Realm as code: `keycloak/realm-modular-monolith.json`, see `keycloak/README.md`. API validates JWT (`sub`, `sid`, `roles`, `MapInboundClaims=false`), asks Keycloak for `resource#scope` decisions cached per jti | Protect endpoints with `.RequireScope(KeycloakScopes.X.Y)`. Add new scopes in the realm JSON first (`PermissionCoverageTests`). Keycloak REST calls only inside `IAM.Infrastructure/Keycloak` |

## 2. Exemplars (copy the shape, do not invent)

| Need | File |
| :-- | :-- |
| Write endpoint + Request with inline validator | `src/Modules/Products/Products.Endpoints/ProductTemplates/v1/Activate/{Endpoint,Request}.cs` |
| Read endpoint (projection) | `src/Modules/Products/Products.Endpoints/ProductTemplates/v1/Get/Endpoint.cs` |
| Paginated search request | `src/Modules/Products/Products.Endpoints/Products/v1/Search/Request.cs` |
| Feature `Setup.cs` registration | `src/Modules/Products/Products.Endpoints/ProductTemplates/Setup.cs` |
| `IModule` | `src/Modules/Products/Products.Endpoints/ProductsModule.cs` |
| Persistence Setup, DbContext, seeder | `src/Modules/Products/Products.Infrastructure/Persistence/{Setup,ProductsDbContext}.cs`, `Persistence/Seeding/` |
| Aggregate + versioned events | `src/Modules/Products/Products.Domain/Stores/Store.cs`, `Stores/DomainEvents/v1/V1ProductAddedToStoreDomainEvent.cs` (snapshot pattern) |
| DomainEventHandler publishing an IntegrationEvent | `src/Modules/Products/Products.Application/Products/DomainEventHandlers/v1/V1ProductCreatedDomainEventHandlers.cs` |
| IntegrationEvent consumer | `src/Modules/Inventory/Inventory.Application/IntegrationEventHandlers/CreateStockLevelOnProductCreatedHandler.cs` |
| InterModuleRequest contract + handler | `src/Common/Common.InterModuleRequests/Inventory/GetStockLevel.cs`, `src/Modules/Inventory/Inventory.Infrastructure/InterModuleRequestHandlers/GetStockLevelRequestHandler.cs` |
| Options + validator | `src/Common/Common.Application/Options/InventoryOptions.cs` |
| Telemetry class | `src/Modules/Products/Products.Infrastructure/Telemetry/ProductsTelemetry.cs` |
| Integration test (write, asserts outbox) | `src/Modules/Products/Products.Tests/Endpoints/ProductTemplates/CreateTests.cs` |
| Test factory + collection | `src/Modules/Products/Products.Tests/{IntegrationTestWebAppFactory,IntegrationTestCollection}.cs` |
| Aggregate unit test | `src/Modules/Products/Products.Tests/Stores/StoreTests.cs` |

## 3. Functional pipeline

No imperative `if (result.IsFailure)`; chain instead. Handlers return `Task<Result>` or `Task<Result<Response>>`.

`BindAsync` (chain fallible op), `TapAsync`/`Tap` (side effect), `TapWhenAsync`/`TapWhen` (conditional side effect), `MapAsync`/`Map` (project), `CombineAsync` (two dependent results), `TapActivityAsync(activity)` (record span status at pipeline end).

Write shape: `db.Set.TagWith(nameof(HandleAsync), id).Where(...).SingleAsResultAsync(nameof(Entity), ct).TapAsync(e => e.Method(...)).TapAsync(_ => db.SaveChangesAsync(ct))`.
Read shape: `db.Set.AsNoTracking().TagWith(...).Where(...).Select(x => new Response { ... }).SingleAsResultAsync(nameof(Entity), ct)`.

## 4. Persistence

- Reads: `.AsNoTracking()` always, project to DTO in `.Select`.
- Single fetch: `.TagWith(nameof(HandleAsync), id).SingleAsResultAsync(nameof(Entity), ct)`. Never `Find`/`FirstOrDefault`.
- Conditional filter: `.WhereIf(pred, cond)`. Joins: native `.LeftJoin`/`.RightJoin`, never `GroupJoin` + `SelectMany`.
- Writes: Endpoint calls aggregate method, aggregate mutates and `RaiseEvent`s, endpoint saves.
- Every persisted type derives from `AggregateRoot<TId>`, `AuditableEntity<TId>`, non-generic `AuditableEntity` (natural/composite key), or is a `ValueObject`/owned type. No bare POCO. Only exception: `OutboxMessage`.

## 5. Endpoints (REPR) and C#

- Minimal APIs only, no controllers. Per feature folder: `Endpoint.cs`, `Request.cs` (record + `RequestValidator : CustomValidator<Request>` in the same file, no separate validator file), `Response.cs` (omit for no-content writes).
- Register in `{Aggregate}/Setup.cs`: `versionedApiGroup.MapGroup("/things").WithTags("Things").MapToApiVersion(1)` then `v1.Feature.Endpoint.MapEndpoint(group)`. `{M}Module.MapEndpoints` owns `/v{version:apiVersion}`, `AddFluentValidationAutoValidation()`, `RequireAuthorization()`.
- Zero warnings, nullable enforced. Primary constructors. `required` on DTOs. `using` directives, never inline full qualifiers.
- Logging: `[LoggerMessage]` `static partial` methods only. Localization: `IResxLocalizer` only. Mapping: inline `.Select` only, no libraries.
- `Result<T>`: rely on implicit operators; inside async lambdas use `Result<T>.Success(v)`. Never cast.
- Typed parameters: never branch on raw `string` route/query values; use `enum`/`bool`/typed value.
- Tunables (timeouts, limits, intervals, cron, templates) live in Options (§9), never literals.
- Every `Request` is validated. Assume third parties fail; use resiliency (retry, circuit breaker); failures must not cascade.
- Never call `services.BuildServiceProvider()` in DI registration (corrupts OTel providers). Scan `IServiceCollection` and use `Activator.CreateInstance`.

## 6. Cross-module and events

- Async: `IntegrationEvent` record in `Common.IntegrationEvents/{Source}.cs`. Publish from a `DomainEventHandlerBase<V1X>` via `outbox.Collect(...)`. Consume via `IntegrationEventHandlerBase<T>`. Consumers auto-register by assembly scan (`Setup.MassTransit.cs`), no manual step.
- Sync: `XRequest : IInterModuleRequest<XResponse>` + `XResponse` in `Common.InterModuleRequests/{Source}/X.cs`. Handler `InterModuleRequestHandler<Req,Res>` in `{Source}.Infrastructure/InterModuleRequestHandlers/` (also a MassTransit consumer, auto-registered). Caller injects `IInterModuleRequestClient<Req,Res>` and calls `SendAsync`.
- DomainEvent versioning: shipped `V{n}...DomainEvent` records never change (no add/remove/rename/retype, not even nullable-widening). Add `V{n+1}` and raise it from the command method; keep `V{n}` in the tree. `IntegrationEvent`s are exempt.
- Event property types: primitives, `string`, `decimal`, `Guid`/`DefaultIdType`, date/time types, `TimeSpan`, `Uri`, `IStronglyTypedId`, or a record/enum nested inside the event (`{Name}Snapshot` mapped by an extension method in the same file, one per event, never shared). Never an entity, aggregate, `ValueObject`, or domain enum. `DomainEventContractTests` enforces this.
- Command methods: build event(s) from pre-mutation state, mutate fields, then `RaiseEvent`. Always in that order. No `Apply`/`ApplyEvent` dispatch; aggregates are not event-sourced.

## 7. Observability

`static {M}Telemetry` in `Infrastructure/Telemetry/`, names `ModularMonolith.{M}`, registered via `IModule.ActivitySourceNames`/`MeterNames`. Span only when it adds insight: `using var activity = {M}Telemetry.ActivitySource.StartActivityForCaller();` then `.TapActivityAsync(activity)`. Metrics inside `.TapAsync` so they fire on success only.

## 8. Testing

- xUnit, NSubstitute (external APIs only), Bogus, real Postgres via Testcontainers, Respawn resets. `Assert.*` only, no FluentAssertions. Names: `Method_Scenario_Expectation`.
- Default fixture: `[Collection("IntegrationTestCollection")]` + `BaseIntegrationTest`, factory `IntegrationTestWebAppFactory : IntegrationTestFactory` overriding `GetActiveModules()`. Call `Factory.CreateClient()` lazily inside each test.
- `IClassFixture<T>` only when a single class uses that factory type; then call `CreateClient()` eagerly in the constructor. Two `IClassFixture<T>` classes in one assembly boot in parallel and corrupt Serilog/OTel statics.
- Writes: assert entity in DB and row in `OutboxMessages`. Never mock MassTransit.
- DB-round-tripped timestamps: `Assert.Equal(expected, actual, TimeSpan.FromSeconds(1))` (Postgres microsecond precision truncates ticks).
- Test config from `AddInMemoryCollection` reaches runtime `IOptions<T>` only, not registration-time `configuration.Get<T>()`. Registration-time values go through `builder.UseSetting(...)` (like `TestModuleOverride`) or env vars.
- Bugs: Red test first, then fix, then Green. Never edit a test to make it pass. CI-only failure: instrument the failing path to dump real state (for DB locks: `pg_stat_activity`, `pg_blocking_pids`, effective options) into the exception message, push, read the CI log, then fix. Local repro misleads.

## 9. Options pattern

1. `src/Common/Common.Application/Options/{Name}Options.cs` with `required` props and `{Name}OptionsValidator : CustomValidator<{Name}Options>` in the same file.
2. `src/Host/Host/Configurations/{name}.json`, top-level key = class name.
3. Add the file to `AddJsonFile(...)` in `src/Host/Host/Configurations/Setup.cs`.
4. Inject `IOptions<{Name}Options>`. `AddCommonOptions` auto-binds and validates every `*Options`; never hand-roll `services.Configure<T>`.

## 10. Report every issue you see

Bug, boundary violation, security hole, dead code, footgun: flag it with `file:line`, one line, severity, even if unrelated to the task. Never silently fix, never skip as "pre-existing". Review skills report through their own format instead.

## 11. Make targets

`make build`, `make test`, `make test-{common,host,iam,products,inventory,outbox,notifications,backgroundjobs}`, `make ef-add-{Notifications,Products,Inventory,Outbox} name=X`, `make ef-script-{Module} from=A to=B`, `make ef-script-all from=A to=B`, `make check-migration-drift`.
