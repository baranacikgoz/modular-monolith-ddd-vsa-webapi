# Platform building blocks

Everything in this document lives in `src/Common` or is wired by `src/Host`. It is shared, contains no business logic, and is the part you reuse instead of re-implementing. [`modules.md`](modules.md) covers what each module does with it, and [`ARCHITECTURE.md`](ARCHITECTURE.md) explains why it is shaped this way.

| Area | Use it for |
| :-- | :-- |
| [Results and errors](#results-and-errors) | Returning failures without exceptions |
| [Persistence conventions](#persistence-conventions) | Reads, writes, auditing, migrations |
| [Transactional outbox and inbox](#transactional-outbox-and-inbox) | Publishing and consuming integration events |
| [Inter-module requests](#inter-module-requests) | A synchronous answer from another module |
| [Projections](#projections-and-reconciliation) | A local read model fed by another module's events |
| [Job rows](#job-rows) | Accept now, process later, let clients poll |
| [Pagination and search](#pagination-and-search) | Listing and finding |
| [Authorization](#authorization) | Protecting an endpoint |
| [Rate limiting and resilience](#rate-limiting-and-resilience) | Protecting the API and calling third parties safely |
| [Webhooks](#webhooks) | Accepting signed calls from partners |
| [Object storage](#object-storage) | Files |
| [Caching](#caching) | FusionCache |
| [Options and configuration](#options-and-configuration) | Adding a setting |
| [Feature flags](#feature-flags) | Turning an endpoint on and off |
| [Localization](#localization) | Messages in more than one language |
| [Observability](#observability) | Logs, traces, metrics, health |

## Results and errors

Handlers return `Task<Result>` or `Task<Result<TResponse>>` and chain steps instead of testing `IsFailure`:

```csharp
return await dbContext.Stores
    .TagWith(nameof(HandleAsync), request.Id)
    .Where(s => s.Id == request.Id)
    .SingleAsResultAsync(nameof(Store), cancellationToken)   // NotFound becomes a failed Result
    .TapAsync(store => store.Deactivate())                    // aggregate method
    .TapAsync(_ => dbContext.SaveChangesAsync(cancellationToken));
```

| Operator | Use |
| :-- | :-- |
| `BindAsync` | A step that can fail and produces the next value |
| `TapAsync`, `Tap` | A side effect on success |
| `TapWhenAsync`, `TapWhen` | A conditional side effect |
| `MapAsync`, `Map` | Project the value |
| `CombineAsync` | Two dependent results, kept together as a tuple |
| `TapActivityAsync` | Record the span status at the end of the pipeline |

An `Error` carries a key, an optional parameter name and value, sub-errors and an HTTP status. The response transformer turns it into a localized `ProblemDetails` with extra members:

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.10",
  "title": "Store ViolatesUniqueConstraint",
  "status": 409,
  "instance": "POST /v1/stores/my",
  "errorKey": "ViolatesUniqueConstraint",
  "errors": { "Store": [] },
  "traceId": "00-...",
  "environment": "Development"
}
```

Unhandled exceptions reach `GlobalExceptionHandlingMiddleware`, which produces the same shape. Request bodies are validated with FluentValidation (`CustomValidator<T>`) before the handler runs.

## Persistence conventions

- **Reads** use `.AsNoTracking()` and project to a DTO in `.Select`. A single row comes from `.TagWith(...).SingleAsResultAsync(...)`, never `Find` or `FirstOrDefault`.
- **Writes** call an aggregate method, which mutates state and raises a domain event, then `SaveChangesAsync`. An aggregate method takes its child entity, not the child's id.
- **Ids** are strongly typed. In a query lambda compare the id whole (`.Where(m => m.Id == typedId)`) because EF cannot translate `.Value`.
- **Auditing** is automatic. `ApplyAuditingInterceptor` sets `CreatedOn`, `CreatedBy`, `LastModifiedOn` and `LastModifiedBy`. `BaseDbContext` stores every raised domain event in `AuditLog` (event type, version, JSON payload and actor) in the same transaction, and an endpoint can page that history. `AuditLogRetentionService` prunes old rows (`auditLog.json`, with `PerSchemaRetentionDays` overrides).
- **Schemas**: each module sets `HasDefaultSchema` to its own name. Every persisted type is an `AggregateRoot<TId>`, an `AuditableEntity`, or a value object.
- **Migrations** are generated per module (`make ef-add-*`), scripted to idempotent SQL (`make ef-script-*`) and committed under `migrations/{Module}`. `MigrationGuard` makes the app refuse to start with pending migrations, except in test runs. `make check-migration-drift` fails CI when a migration has no script.
- **Seeding**: implement `IDatabaseSeeder` with a `Priority`. `DatabaseSeederOrchestrator` runs seeders in order after the host has started.
- **Search indexes**: see [Pagination and search](#pagination-and-search).

## Transactional outbox and inbox

**Publishing.** A domain event handler (`DomainEventHandlerBase<T>`) calls `outbox.Collect(new SomethingIntegrationEvent(...))`. During `SaveChangesAsync`, `BaseDbContext` writes the outbox rows in the same transaction as the aggregate. Application code never calls `IPublishEndpoint`.

`OutboxProcessor` then:

1. Claims up to `BatchSize` rows with a single `UPDATE ... RETURNING` that uses `FOR UPDATE SKIP LOCKED` and pushes `NextRetryAt` into the future by `ClaimLeaseSeconds`. The statement commits immediately, so no lock is held while publishing.
2. Publishes the claimed rows in parallel, `PublishConcurrency` at a time, with no open transaction.
3. Marks each row processed, or schedules a retry with exponential backoff (`BaseBackoffSeconds` up to `MaxBackoffSeconds`, `MaxRetryCount` attempts).
4. Stops attempting the rest of a batch once `MaxConsecutiveFailures` publishes have failed, so a downed broker is not hammered.

A crash between claiming and publishing leaves a leased row that any instance picks up again after the lease expires. That can publish an event twice, which is acceptable by design because every consumer is idempotent. Several processors can run at once and claim disjoint rows. Lag and backlog are exported as metrics, the trace context travels with the message, and old rows are cleaned up.

**Coalescing.** When one save can raise many events whose handlers each collect the same "re-check this target" trigger, implement `ICoalescableIntegrationEvent`. `CoalesceKey` carries what makes two payloads differ; the outbox then writes one row per type and key per save. Use it only for triggers whose consumer recomputes from current state.

**Consuming.** Inherit `IntegrationEventHandlerBase<TEvent>`, override `ProcessAsync` and inject the inbox store of your own module:

```csharp
public sealed class CreateStockLevelOnProductCreatedHandler(
    IInventoryDbContext dbContext, IFusionCache cache, IOptions<CachingOptions> cachingOptions,
    IInboxStore<IInventoryDbContext> inbox, ILogger<CreateStockLevelOnProductCreatedHandler> logger)
    : IntegrationEventHandlerBase<ProductCreatedIntegrationEvent>(cache, cachingOptions, inbox, logger)
{
    protected override async Task ProcessAsync(ProductCreatedIntegrationEvent @event, CancellationToken ct) { /* ... */ }
}
```

The base class first checks a FusionCache key `processed_event:{consumer}:{eventId}` as a fast pre-filter. It then tracks a `ProcessedMessage` row in your module's `DbContext`, so your own `SaveChangesAsync` commits the inbox mark and the side effects together. A redelivery after commit hits the composite key and is acknowledged as a duplicate. The cache key is written only after the commit. `InboxCleanupJob` prunes old rows. Never implement `IConsumer<T>` directly.

**Ordering.** Delivery order is never guaranteed: the processor publishes in parallel and consumers run concurrently. A consumer must not depend on arrival order. When order matters, use a [projection](#projections-and-reconciliation).

## Inter-module requests

Use a request when the caller cannot proceed without the answer and the request is meaningless while the source module is down. Use a projection when the request path merely reads another module's data, and an event when you trigger a side effect there.

1. Declare `XRequest : IInterModuleRequest<XResponse>` and `XResponse` in `Common.InterModuleRequests/{Source}/X.cs`.
2. Implement `InterModuleRequestHandler<XRequest, XResponse>` in `{Source}.Infrastructure/InterModuleRequestHandlers`. It is registered automatically.
3. Inject `IInterModuleRequestClient<XRequest, XResponse>` in the caller and call `SendAsync`.

The client is one implementation, `MassTransitInterModuleRequestClient`, which always goes through RabbitMQ, so the call is identical in-process and across processes. Each handler gets its own receive endpoint with no retry, a timeout equal to what the caller waits (`InterModuleRequestOptions.TimeoutSecondsFor(request)`, overridable per request in `Timeouts`) and concurrency from `HandlerPrefetchCount` and `HandlerConcurrentMessageLimit`. A timed-out request reaches the HTTP client as `503` with `Retry-After` (`DependencyUnavailable`), a faulted one as `502` (`DependencyFaulted`).

## Projections and reconciliation

A projection is a local read-model table fed by another module's events.

- Derive the row from `ProjectionEntity`. It stores `SourceVersion`, the version of the source aggregate it last reflected.
- Apply events with `set.UpsertIfNewerAsync(key, incomingVersion, create, apply, ct)`. It drops stale or duplicate events, and resolves two consumers racing to insert the same missing key.
- `ProjectionReconciliationJob` walks a source module's pager (for example the IAM `GetUsersInRolePage` request) to heal rows that missed an event, and warns when it had to heal. Settings: `projectionReconciliation.json`.

## Job rows

For work that is accepted now and processed later, with the client polling the outcome:

- `JobRow<TId>` carries `Status` (`Queued`, `Running`, `Succeeded`, `Failed`), `RequestedBy`, timestamps, a machine-readable `FailureKey` and a progress counter. It is bookkeeping, so it writes no audit or outbox rows.
- `TryClaimAsync` is one atomic `UPDATE ... WHERE Status = Queued`: it returns true for exactly one caller, so a redelivered message cannot run the work twice. `RequeueAsync` hands a job back when its consumer was cancelled.
- `JobHousekeepingJob<TJob, TId>` fails `Running` rows nobody finished and deletes old finished rows. Subclass it per table and register it as a recurring job (`jobHousekeeping.json`).
- An authenticated background job can act for the user who requested it.

## Pagination and search

- Derive a request from `PaginationRequest`: `PageNumber` and `PageSize`, plus an optional `After` cursor and `IncludeTotal`. With `After` the page starts right after the last row of the previous page and `PageNumber` is ignored. `IncludeTotal=false` skips the `COUNT(*)` and `TotalCount` is `-1`.
- `PaginationCursor` is an opaque, base64url keyset cursor holding the sort value and a tiebreaker, so tied rows keep a stable order. `PaginationResponse<T>` returns `NextCursor`. Responses wrap rows in `data`.
- **Prose search**: a generated `tsvector` column with a GIN index, queried with `WebSearchToTsQuery` in two configurations through `ISearchLanguageResolver`. The entity implements `ISearchLocalized`. See [`full-text-search.md`](full-text-search.md).
- **Short identifier-like columns** (name, code, barcode) use `HasTrigramIndex` (serves `ILIKE '%term%'`) or `HasPatternIndex` (serves `LIKE 'term%'`) from `TrigramIndexExtensions`, with `modelBuilder.HasTrigramExtension()` so the migration creates `pg_trgm`. Query with `LikePattern.Contains` or `LikePattern.StartsWith`, never a hand-built pattern.

## Authorization

- Protect an endpoint with `.RequireScope(KeycloakScopes.Products.View)`. Scope constants are strings of the form `resource:action`, with `-own` variants for ownership.
- Add a new scope in `keycloak/realm-modular-monolith.json` first. `PermissionCoverageTests` fails when an endpoint scope is missing from the realm or a realm scope is unused.
- The API validates the bearer token (`sub`, `sid`, `roles`) and asks Keycloak for a `resource#scope` decision with the same token. Decisions are cached per token id for at most `DecisionCacheMaxDurationSeconds`. Keycloak REST calls live only in `IAM.Infrastructure/Keycloak`.
- `ICurrentUser` gives the caller's id inside a handler.
- Machine callers use the client credentials grant. See [`keycloak/README.md`](../keycloak/README.md).

## Rate limiting and resilience

**Inbound rate limiting.** Policies are named fixed windows defined in `rateLimiting.json` (`CustomRateLimitingOptions`) and declared per module through `IModule.RateLimitingPolicies`. Attach one with `.RequireRateLimiting(name)`. Every policy goes through `RateLimitPartitions`:

- Without Redis an in-process limiter is used. With Redis (`CachingOptions.UseRedis`) all replicas share one counter through `RedisFixedWindowRateLimiter`.
- `FailOpen` decides what happens when Redis is unreachable: `true` lets traffic through, `false` rejects it. The SMS and registration policies fail open; the email and OTP verification policies fail closed.
- `FixedWindowByRouteValue` partitions by a route value, such as a webhook account id, and falls back to the client IP.
- A global limiter buckets anonymous traffic per IP. `ExemptPathPrefixes` excludes paths such as `/hubs`.

**Outbound resilience.** Typed `HttpClient`s get timeouts, retries and a circuit breaker from `ResiliencyOptions`. `KeyedResiliencePipelines` builds one pipeline per key (a partner account, a tenant credential) with its own rate limiter, circuit breaker and attempt timeout, so one noisy key cannot open the circuit of another. With Redis configured a queue-less profile counts its quota in Redis, so every replica draws from one shared budget. Profiles live under `ResiliencyOptions.Keyed`. Assume third parties fail: every outbound call needs a timeout, bounded retries and a breaker, and a failure must not cascade.

## Webhooks

- `HmacSignatureVerifier.VerifySha256(secret, bodyBytes, header)` is a constant-time check. It accepts hex (either case) or base64, with or without a `sha256=` prefix, and returns false for anything malformed. Verify the raw bytes, never a decoded string.
- `RequestBodyLimitFilter` and `RequestBodyLimitMiddleware` cap the body before it is read.
- The Inventory warehouse webhook is the worked example: anonymous route, signature header `X-Warehouse-Signature`, shared secret in `InventoryOptions.WebhookSharedSecret`, body limit in `WebhookMaxBodyBytes`.

## Object storage

`Common.Application/Storage` defines two ports over any S3-compatible service (`objectStorage.json`):

- `IPublicObjectStore` for unsigned, CDN-cacheable objects. Key objects by something stable such as a content hash, so a changed file is a new key and the cache never needs busting. `GetPublicUrl` is deterministic and makes no network call.
- `IPrivateObjectStore` for documents, exports and user uploads. Access is by presigned URL (`CreatePresignedUploadUrl`, `CreatePresignedDownloadUrl`), so bytes go straight between the client and storage, or by server-side calls.

Both support exists, metadata, delete, batch delete (1000 keys per request) and paged listing. Retries, a circuit breaker, per-attempt timeouts and the multipart threshold are configured, not hard-coded.

## Caching

FusionCache with a memory L1 bounded by `MemoryCacheSizeLimit` entries. Set `CachingOptions.UseRedis` to add a distributed L2 and a backplane; Production refuses an in-memory-only cache unless `AllowInMemoryOnlyInProduction` is set. `EntryDefaults` configures durations, fail-safe and factory timeouts. Idempotency keys use `IdempotencyL1Duration` in memory and `IdempotencyKeyDuration` in Redis.

## Options and configuration

Every setting is an `{Name}Options` class in `Common.Application/Options` with a `{Name}OptionsValidator` in the same file, bound from `src/Host/Host/Configurations/{name}.json` (top-level key is the class name). `AddCommonOptions` binds and validates all of them at startup, and a bad value stops the host. Inject `IOptions<{Name}Options>`.

- Mark properties `required`. Because the binder leaves a missing key at the CLR default, numeric properties need a rule the default fails (`> 0`), and a numeric, `bool` or `double` whose default is valid is declared nullable with `NotNull`.
- Tunables (timeouts, limits, intervals, cron expressions, templates) live in options, never as literals.
- A new options file must be added to the `AddJsonFile(...)` list in `Configurations/Setup.cs`.
- Adding a field to a deployed options class means every environment's live configuration needs the field before the next deploy, because a config that predates it fails validation.

The deployment model is in [`Configurations/README.md`](../src/Host/Host/Configurations/README.md).

## Feature flags

`Microsoft.FeatureManagement` reads the `FeatureManagement` section of `featureFlags.json`, with targeting and variants. Flag names are constants in `FeatureFlags` (never raw strings). Gate an endpoint with `.RequireFeature(FeatureFlags.IAM.Captcha)`. The `manage-feature-flag` command creates, toggles and applies flags.

## Localization

`ResxLocalizationOptions` sets the default culture (`tr`) and the supported ones (`tr`, `en`). Messages come from `IResxLocalizer`, whose members are generated from `ResxLocalizer.resx` by `Aigamo.ResXGenerator`, so a missing key is a compile error. The request culture decides the language; send `Accept-Language: en` for English. Adding a culture is a new `ResxLocalizer.{culture}.resx` file plus an entry in `SupportedCultures`.

## Observability

- **Logging** uses Serilog with `[LoggerMessage]` source-generated methods only. Sinks: console, a rolling compact-JSON file, and optionally Seq, Elasticsearch or OTLP (`ObservabilityOptions.LogSink`). Logs are enriched with user, trace and span ids. `RequestResponseBodyLoggingMiddleware` logs size-capped bodies and redacts the sensitive paths listed in `requestLogging.json`.
- **Tracing and metrics** use OpenTelemetry over OTLP with ASP.NET Core, HTTP client, EF Core, Npgsql, runtime and process instrumentation. Each module has a static `{M}Telemetry` class with an `ActivitySource` and a `Meter` named `ModularMonolith.{M}`, registered through `IModule.ActivitySourceNames` and `MeterNames`. Start a span only when it adds insight (`StartActivityForCaller`), and record metrics inside `.TapAsync` so they fire on success only.
- **Health**: `/health/live`, `/health/ready`, `/health/startup` and `/health/dependencies`. PostgreSQL, RabbitMQ and Redis (when configured) gate readiness. Keycloak is on the dependency probe only, and only where IAM runs.
- **Security headers, forwarded headers and CORS** are configured in `securityHeaders.json`, `reverseProxy.json` and `cors.json`.
- **Aspire Dashboard** receives traces, metrics and logs in local development (`http://localhost:18888`).
