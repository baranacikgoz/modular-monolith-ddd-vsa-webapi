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
| [Dirty set and lease](#dirty-set-and-lease) | Pushing local changes to a third party in batches |
| [Pagination and search](#pagination-and-search) | Listing and finding |
| [Authorization](#authorization) | Protecting an endpoint |
| [Rate limiting and resilience](#rate-limiting-and-resilience) | Protecting the API and calling third parties safely |
| [Webhooks](#webhooks) | Accepting signed calls from partners |
| [Secret protection](#secret-protection) | Storing a credential you must read back |
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
| `Match` | Fold a result into a plain value: `Result<T>.Match(onSuccess, onFailure)` and `Result.Match(onSuccess, onFailure)`. For one row's outcome in a batch where a failed row must not stop the others (`ProductImportJobRunner`) |

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
- **Races become results**: a write that can lose a race on a unique index or a foreign key checks first for the common case and then saves through `SaveChangesDetectingUniqueViolationAsync` (`Common.Infrastructure`) or `SaveChangesDetectingConflictAsync` (`Products.Infrastructure/Persistence/ConstraintViolationExtensions.cs`), which map the database violation to the `Error` you pass. The index is what is race-safe. Any other failure still throws.
- **Row lock**: an event-driven writer of a contended aggregate loads it with `SELECT ... FOR UPDATE` inside the consumer's explicit transaction, so concurrent movements queue on the lock instead of failing the version check and waiting for a bus retry (`StockLevelRowLock`). The inbox row commits with the same `SaveChangesAsync`.
- **Atomic counter**: a counter row created on first use is written by one writer class with a single `INSERT ... ON CONFLICT DO UPDATE` inside the consumer's transaction (`StockCounterWriter`, table `StoreDailyStockTotals`), so two consumers never lose an increment and a redelivered event rolls its change back with the inbox row.
- **Immutable ledger**: a ledger row (`StockMovement`) has no mutator, is created only by its aggregate and is never updated or deleted. The aggregate's collection holds only the rows added in the current unit of work.
- **jsonb list**: a list always read and written whole is one jsonb column through `HasJsonbList` (`JsonbListExtensions`). It is compared by its serialized form, so a rebuilt equal list does not mark the row modified. Not for data a query filters or joins on.
- **Owned value object**: mapped with `OwnsOne` into the owner's row (`PackageDimensions` on `Product`).
- **Joins**: native `.LeftJoin(...)` and `.RightJoin(...)`, never `GroupJoin` plus `SelectMany` (`Inventory.Endpoints/StockLevels/v1/Search/Endpoint.cs`).
- **Business days**: `TimeProviderExtensions` (`Common.Application/Extensions`) turns a time zone's calendar days into exact UTC bounds, including the 23 and 25 hour days of a daylight saving change, for daily counters and date-range reads. `GET /v1/stock-levels/daily-totals` is the worked read: the range is business days, `to` defaults to `GetToday(zone)`, and its length is capped by `InventoryOptions.DailyTotalsMaxRangeDays` in the request validator, so one call returns a bounded number of rows without paging.

## Transactional outbox and inbox

**Publishing.** A domain event handler (`DomainEventHandlerBase<T>`) calls `outbox.Collect(new SomethingIntegrationEvent(...))`. During `SaveChangesAsync`, `BaseDbContext` writes the outbox rows in the same transaction as the aggregate. Application code never calls `IPublishEndpoint`.

`OutboxProcessor` then:

1. Claims up to `BatchSize` rows with a single `UPDATE ... RETURNING` that uses `FOR UPDATE SKIP LOCKED` and pushes `NextRetryAt` into the future by `ClaimLeaseSeconds`. The statement commits immediately, so no lock is held while publishing.
2. Publishes the claimed rows in parallel, `PublishConcurrency` at a time, with no open transaction.
3. Marks each row processed, or schedules a retry with exponential backoff (`BaseBackoffSeconds` up to `MaxBackoffSeconds`, `MaxRetryCount` attempts).
4. Stops attempting the rest of a batch once `MaxConsecutiveFailures` publishes have failed, so a downed broker is not hammered.

A crash between claiming and publishing leaves a leased row that any instance picks up again after the lease expires. That can publish an event twice, which is acceptable by design because every consumer is idempotent. Several processors can run at once and claim disjoint rows. Lag and backlog are exported as metrics, the trace context travels with the message, and old rows are cleaned up.

**Coalescing.** When one save can raise many events whose handlers each collect the same "re-check this target" trigger, implement `ICoalescableIntegrationEvent`. `CoalesceKey` carries what makes two payloads differ; the outbox then writes one row per type and key per save. Use it only for triggers whose consumer recomputes from current state. `StockPushDueIntegrationEvent` is the example: its key is the connection id, so a save that changes many stock levels queues one push per connection.

**A module's own queue.** An integration event may be consumed by the module that raised it, as a queue message: `ProductImportRequestedIntegrationEvent`, `StockBulkAdjustRequestedIntegrationEvent` and `StockPushDueIntegrationEvent` carry only an id, and their consumers do the work. A `ConsumerDefinition<T>` sets prefetch count and concurrent message limit to one option value (`SetConcurrency(options.XConcurrentMessageLimit)`).

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

The base class first checks a FusionCache key `processed_event:{consumer}:{eventId}` as a fast pre-filter. It then tracks a `ProcessedMessage` row in your module's `DbContext`, so your own `SaveChangesAsync` commits the inbox mark and the side effects together. A redelivery after commit hits the composite key and is acknowledged as a duplicate. The cache key is written only after the commit. `InboxCleanupJob` prunes old rows. Never implement `IConsumer<T>` directly. A module with no database (IAM) injects `StatelessInboxStore` instead: nothing is marked, only the cache pre-filter remains, and the consumer must be idempotent on its own (`AssignStoreOwnerRoleOnStoreApprovedHandler` relies on Keycloak's idempotent role mapping).

**Raising an event from a consumer.** A consumer may collect an integration event itself. The base class saves after `ProcessAsync`, and that save writes the outbox row and the inbox row in one transaction, so a redelivery cannot raise the event twice (`RaiseCriticalAlertOnStockLowThresholdCrossedHandler` turns a low stock fact into a critical alert this way).

**Queue names.** A consumer's queue is named after its class name alone: MassTransit's default endpoint name formatter ignores the namespace, and the idempotency cache key uses the class name too. Two classes with one name in different modules would share a queue and a key, so a second module that consumes an event another module already consumes needs a differently named consumer.

**Ordering.** Delivery order is never guaranteed: the processor publishes in parallel and consumers run concurrently. A consumer must not depend on arrival order. When order matters, use a [projection](#projections-and-reconciliation).

## Inter-module requests

Use a request when the caller cannot proceed without the answer and the request is meaningless while the source module is down. Use a projection when the request path merely reads another module's data, and an event when you trigger a side effect there.

1. Declare `XRequest : IInterModuleRequest<XResponse>` and `XResponse` in `Common.InterModuleRequests/{Source}/X.cs`.
2. Implement `InterModuleRequestHandler<XRequest, XResponse>` in `{Source}.Infrastructure/InterModuleRequestHandlers`. It is registered automatically.
3. Inject `IInterModuleRequestClient<XRequest, XResponse>` in the caller and call `SendAsync`.

The client is one implementation, `MassTransitInterModuleRequestClient`, which always goes through RabbitMQ, so the call is identical in-process and across processes. Each handler gets its own receive endpoint with no retry, a timeout equal to what the caller waits (`InterModuleRequestOptions.TimeoutSecondsFor(request)`, overridable per request in `Timeouts`) and concurrency from `HandlerPrefetchCount` and `HandlerConcurrentMessageLimit`. A timed-out request reaches the HTTP client as `503` with `Retry-After` (`DependencyUnavailable`), a faulted one as `502` (`DependencyFaulted`).

**Bulk user lookup.** To show who made a change, resolve a page of ids with `ActorNames.ResolveAsync(client, ids, ct)` (`Common.Endpoints`). It sends one `GetUsersRequest` for the distinct ids of the whole page, never one per row, and asks nobody when the page has no ids. IAM answers from a per-user cache (`user_display:{id}`, in-process and, when Redis is configured, Redis; a negative entry for an unknown id, fail-safe on while Keycloak is down) and calls Keycloak only for the misses, at most `KeycloakOptions.UserLookupConcurrency` at once. An id IAM does not know is left out of the answer and the caller falls back to the raw id. `Inventory.Endpoints/StockLevels/v1/Movements/Endpoint.cs` is the example.

**Bounded answers.** A request that returns a list of ids to another module is capped from options and says so: `SearchProductsByTermRequest` returns at most `FullTextSearchOptions.MaxMatchResults` ids with an `IsTruncated` flag, so an unbounded list never crosses a module boundary. The answering module can cache it under module-local keys (`ProductsCacheKeys`, `ProductsOptions.SearchCacheSeconds`).

## Projections and reconciliation

A projection is a local read-model table fed by another module's events.

- Derive the row from `ProjectionEntity`. It stores `SourceVersion`, the version of the source aggregate it last reflected.
- Apply events with `set.UpsertIfNewerAsync(key, incomingVersion, create, apply, ct)`. It drops stale or duplicate events, and resolves two consumers racing to insert the same missing key.
- `ProjectionReconciliationJob` walks a source module's pager (for example the IAM `GetUsersInRolePage` request) to heal rows that missed an event, and warns when it had to heal. Settings: `projectionReconciliation.json`.

Three worked examples. `StoreAccess` (Inventory) is a projection used as an authorization gate: it keeps each store's owner and active flag, so a route that answers "my stores'" data joins it in Inventory's own schema and never asks Products on the request path (`WarehouseConnectionQueries.OfActiveStoresOwnedBy`, used by `GET /v1/warehouse-connections/my`). Products raises `StoreAccessChangedIntegrationEvent` from every store event that opens or closes the store, with the domain event's own `Version`; `UpdateStoreAccessOnStoreAccessChangedHandler` applies it with `UpsertIfNewerAsync`, so a late event cannot reopen a store a later one closed. `StoreAccessReconciliationJob` pages Products' `GetStoresPage` keyset pager (newest first, capped by `ProductsOptions.StoresPageMaxSize`) on `InventoryOptions.StoreAccessReconciliationCron`, and its registrar queues one run at start-up when the table is empty. A store with no row counts as not accessible. `ProductSummary` (Inventory) keeps the store id and name of each product: Products raises `ProductChangedIntegrationEvent` with the aggregate `Version`, `UpdateProductSummaryOnProductChangedHandler` applies it with `UpsertIfNewerAsync`, and `ProductSummariesReconciliationJob` pages Products' `GetProductsPage` keyset pager (newest first, page size capped by `ProductsOptions.ProductsPageMaxSize`) on `InventoryOptions.ProductSummaryReconciliationCron`. A request path reads the copy and never asks Products. `AlertRecipient` (Notifications) keeps the emails of the members of a role: IAM has no event for a user gaining or losing a role, so `AlertRecipientsReconciliationJob` is the only writer, and it removes unseen rows only after a run that read the whole role and found at least one member. `ProductSummariesReconciliationJob` and `StoreAccessReconciliationJob` read existing rows with `FindAsync`, which consults the change tracker, so they also see rows added earlier in the same unsaved page.

## Job rows

For work that is accepted now and processed later, with the client polling the outcome. The full shape, with its limits, is `CLAUDE.md` section 12; the reference implementations are the Products import (`ProductImportJob`, `ProductImportJobEnqueuer`, `ProductImportJobRunner`, `RunProductImportJobHandler`) and the Inventory bulk adjustment (`StockBulkAdjustJob*`).

- `JobRow<TId>` carries `Status` (`Queued`, `Running`, `Succeeded`, `Failed`), `RequestedBy`, timestamps, a machine-readable `FailureKey` and a progress counter. It is bookkeeping, so it writes no audit or outbox rows.
- `TryClaimAsync` is one atomic `UPDATE ... WHERE Status = Queued`: it returns true for exactly one caller, so a redelivered message cannot run the work twice. `RequeueAsync` hands a job back when its consumer was cancelled, or when a job larger than one message continues: the job stores its cursor, goes back to `Queued` and collects a fresh queue message for itself in the same transaction.
- The endpoint answers 202 (`TransformResultToAcceptedResponse<T>`) with the job to poll. A client-chosen `ClientRequestId` and a partial unique index on the active statuses make a repeat or a double click return the existing job instead of queueing another.
- The runner never throws for a failed unit of work: the row carries `Failed` and a `FailureKey`, otherwise the bus would retry the whole job. It works in batches from options, saving and clearing the change tracker after each.
- `JobHousekeepingJob<TJob, TId>` fails `Running` rows nobody finished (`JobHousekeepingOptions.StaleAfterMinutes`) and deletes old finished rows (`RetentionHours`, in pages of `PageSize`). Subclass it per table and register it as a recurring job (`jobHousekeeping.json`); `ProductJobsHousekeeping` also deletes the files a finished row still points at before the row goes.
- A consumer that died leaves a `Running` row, and its redelivered message may be dropped as a duplicate by the inbox. `StockJobRequeueService` is the sweep for that: it hands such a job back to the queue and fails it after `StockJobMaxRequeues` hands-back without progress.
- An authenticated background job can act for the user who requested it (`IBackgroundUserContext`): the runner sets it from the job's `RequestedBy`, so the audit columns of what it writes name that user.

## Dirty set and lease

For pushing local changes to a third party without a message per change (`Inventory.Infrastructure/StockPushes`):

- A change marks a row in a dirty table in its own transaction (`StockPushMarks`, table `StockPushDirtyItems`, one row per stock level). Each mark draws a new, ever larger version from a database sequence, so a push clears only the version it read and a change made while it was pushing survives.
- The same transaction collects a coalescing integration event, so one message per target stands for all the changes of a save.
- The queue consumer takes a lease on the target, a Postgres session advisory lock (`StockPushLease`). A run that finds the lease held does nothing, because the holder reads the marks fresh. The server drops the lock when the session ends, whatever killed the process.
- It sends absolute values in batches (`StockPushBatchSize`), at most `StockPushBatchesPerMessage` calls per message, and queues a continuation after releasing the lease. A refusal, a throttle or an unknown outcome stops the run with the marks in place; the values are simply sent again.
- A recurring sweep publishes the event again for targets with a mark older than `StockPushDirtyMinAgeSeconds`, a page of `SweepPageSize` at a time. It never calls the third party.

## Pagination and search

- Derive a request from `PaginationRequest`: `PageNumber` and `PageSize`, plus an optional `After` cursor and `IncludeTotal`. With `After` the page starts right after the last row of the previous page and `PageNumber` is ignored. `IncludeTotal=false` skips the `COUNT(*)` and `TotalCount` is `-1`.
- `PaginationCursor` is an opaque, base64url keyset cursor holding the sort value and a tiebreaker, so tied rows keep a stable order. `PaginationResponse<T>` returns `NextCursor`. Responses wrap rows in `data`. Examples: the stock ledger (`Inventory.Endpoints/StockLevels/v1/Movements/Endpoint.cs`, newest first) and the product export, which reads its whole source as keyset pages without `COUNT(*)` (`ProductExportJobRunner`).
- A list built on a join projection is not an auditable entity, so it pages by offset directly and rejects `After` in its validator (`Inventory.Endpoints/StockLevels/v1/Search`).
- **Prose search**: a generated `tsvector` column with a GIN index, queried with `WebSearchToTsQuery` in two configurations through `ISearchLanguageResolver`. The entity implements `ISearchLocalized`. See [`full-text-search.md`](full-text-search.md).
- **Short identifier-like columns** (name, code, barcode) use `HasTrigramIndex` (serves `ILIKE '%term%'`) or `HasPatternIndex` (serves `LIKE 'term%'`) from `TrigramIndexExtensions`, with `modelBuilder.HasTrigramExtension()` so the migration creates `pg_trgm`. Query with `LikePattern.Contains` or `LikePattern.StartsWith`, never a hand-built pattern.

## Authorization

- Protect an endpoint with `.RequireScope(KeycloakScopes.Products.View)`. Scope constants are strings of the form `resource:action`, with `-own` variants for ownership.
- Add a new scope in `keycloak/realm-modular-monolith.json` first. `PermissionCoverageTests` fails when an endpoint scope is missing from the realm or a realm scope is unused.
- The API validates the bearer token (`sub`, `sid`, `roles`) and asks Keycloak for a `resource#scope` decision with the same token. Decisions are cached per token id for at most `DecisionCacheMaxDurationSeconds`. Keycloak REST calls live only in `IAM.Infrastructure/Keycloak`.
- A `-own` scope is decided against the caller's own resource in the query, not by role, so every `-own` permission applies `Any Authenticated User Policy` (`PermissionCoverageTests.AllOwnScopePermissions_ApplyAnyAuthenticatedUserPolicy`). The `store-owner` realm role, granted when a store is approved, therefore has no realm policy of its own. The ownership check of `GET /v1/warehouse-connections/my` is a join on the local store access copy.
- `ICurrentUser` gives the caller's id inside a handler.
- Machine callers use the client credentials grant. See [`keycloak/README.md`](../keycloak/README.md).

## Rate limiting and resilience

**Inbound rate limiting.** Policies are named fixed windows defined in `rateLimiting.json` (`CustomRateLimitingOptions`) and declared per module through `IModule.RateLimitingPolicies`. Attach one with `.RequireRateLimiting(name)`. Every policy goes through `RateLimitPartitions`:

- Without Redis an in-process limiter is used. With Redis (`CachingOptions.UseRedis`) all replicas share one counter through `RedisFixedWindowRateLimiter`.
- `FailOpen` decides what happens when Redis is unreachable: `true` lets traffic through, `false` rejects it. The SMS, registration, warehouse webhook and store application policies fail open; the email and OTP verification policies fail closed.
- `FixedWindowByRouteValue` partitions by a route value and falls back to the client IP. The warehouse webhook policy partitions by connection id, so one store's burst does not throttle the others behind the same provider address. An anonymous endpoint partitions by client IP (`StoreApplication`: 5 per hour by default).
- A global limiter buckets anonymous traffic per IP. `ExemptPathPrefixes` excludes paths such as `/hubs`.

**Outbound resilience.** Typed `HttpClient`s get timeouts, retries and a circuit breaker from `ResiliencyOptions`. `KeyedResiliencePipelines` builds one pipeline per key (a partner account, a tenant credential) with its own rate limiter, circuit breaker and attempt timeout, so one noisy key cannot open the circuit of another. With Redis configured a queue-less profile counts its quota in Redis, so every replica draws from one shared budget. Profiles live under `ResiliencyOptions.Keyed`. The Inventory warehouse gateway builds one pipeline per connection (key `Warehouse{Provider}:{connectionId}`, profile `WarehouseJsonApi` or `WarehouseFormApi`) with a rate limiter, a circuit breaker and an attempt timeout, and no retry, because a call that changes state at the partner must not be sent twice. A definite failure (the partner's answer, our own quota, an open circuit) comes back as a failed `Result`; a timeout or dropped connection is ambiguous and surfaces as `WarehouseOutcomeUnknownException`, which the caller reconciles. Assume third parties fail: every outbound call needs a timeout, bounded retries (or, for a state-changing call, none and a reconcile step) and a breaker, and a failure must not cascade.

## Webhooks

- `HmacSignatureVerifier.VerifySha256(secret, bodyBytes, header)` is a constant-time check. It accepts hex (either case) or base64, with or without a `sha256=` prefix, and returns false for anything malformed. Verify the raw bytes, never a decoded string.
- `RequestBodyLimitFilter` and `RequestBodyLimitMiddleware` cap the body before it is read.
- The Inventory warehouse webhook is the worked example (`POST /v1/stock-reservations/webhooks/warehouse/{connectionId}`): anonymous route, signature header `X-Warehouse-Signature`, the connection id in the route and a secret per connection, not one global secret. The secret is generated by `SecretGenerator`, stored encrypted by `SecretProtector`, shown to the caller once and rotatable. The handler loads the connection, decrypts its secret and verifies the raw body bytes before it parses anything; the body limit is `InventoryOptions.WebhookMaxBodyBytes` and the rate limit is per connection (`WarehouseWebhook`).
- Deliveries are at least once, so each is applied once per (connection, delivery id): `WarehouseWebhookIntake` looks the id up first, and the unique index on `WebhookDeliveries`, saved in the same transaction as the delivery's effect, decides two identical deliveries that arrive together (the loser rolls back as a whole, effect included, and answers as a duplicate). `WebhookDeliveryRaceTests` makes a competing request commit just before the save under test. A recurring job deletes recorded ids after `WebhookDeliveryRetentionDays`, which has to outlast the longest span in which the partner may redeliver.
- The body may carry the partner's own status word. `ProviderStatusMap` is the one place that spells the partner's vocabulary, and `ReservationStatusTransitions` is the one table of legal status changes; a mapped word reaches the reservation through the commands a local caller uses. A word nobody mapped is acknowledged and recorded, never refused, and the reservation turns read-only: it takes no local command (409 `ReservationReadOnly`) and `providerStatus` keeps mirroring what the partner reports. See [`modules.md`](modules.md#inventory).

## Secret protection

A secret the system must read back in clear (a third party's API credential, a webhook secret it checks) is stored with `SecretProtector` (`Common.Infrastructure/Security`), AES-GCM with a random nonce per value. One value is one self-contained `byte[]` column (nonce, then tag, then ciphertext), and a tampered, truncated or wrongly keyed value fails `Decrypt` with a `CryptographicException`. The master key is `SecretProtectionOptions.EncryptionKey` (`secretProtection.json`): base64 of exactly 32 random bytes, delivered by the deployment's secret store and never stored next to the ciphertext. The checked-in key is accepted only in the Development environment; anywhere else startup validation rejects it. Changing the key makes every value encrypted under the previous one undecryptable. A secret that is only ever compared (a password, an inbound token) is hashed, never encrypted.

`SecretGenerator` invents the credentials a caller presents back: `GenerateSecret` (URL-safe base64 of 32 random bytes by default, at least 16) and `GenerateIdentifier(prefix, byteLength)` for a public handle. `WarehouseConnection` is the worked example: it holds `EncryptedApiCredential` and `EncryptedWebhookSecret`, and no response ever carries either.

## Object storage

`Common.Application/Storage` defines two ports over any S3-compatible service (`objectStorage.json`):

- `IPublicObjectStore` for unsigned, CDN-cacheable objects. Key objects by something stable such as a content hash, so a changed file is a new key and the cache never needs busting. `GetPublicUrl` is deterministic and makes no network call. Product images use it: `ImageSignature` reads the type (JPEG, PNG or WebP) from the file's first bytes, because the bucket is public and a mislabelled file must not get in.
- `IPrivateObjectStore` for documents, exports and user uploads. Access is by presigned URL (`CreatePresignedUploadUrl`, `CreatePresignedDownloadUrl`), so bytes go straight between the client and storage, or by server-side calls. The product import stores its uploaded workbook here and the export its CSV, and `GET .../exports/{id}/download` streams the file through the API with a `Result` check first.

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

`ResxLocalizationOptions` sets the default culture (`tr`) and the supported ones (`tr`, `en`). Messages come from `IResxLocalizer`, whose members are generated from `ResxLocalizer.resx` by `Aigamo.ResXGenerator`, so a missing key is a compile error. The request culture decides the language; send `Accept-Language: en` for English. Code that runs outside a request (a consumer) has no request culture, so it sets `CultureInfo.CurrentUICulture` from `ResxLocalizationOptions.DefaultCulture` before it reads the localizer (`RaiseCriticalAlertOnStockLowThresholdCrossedHandler`). Adding a culture is a new `ResxLocalizer.{culture}.resx` file plus an entry in `SupportedCultures`.

## Observability

- **Logging** uses Serilog with `[LoggerMessage]` source-generated methods only. Sinks: console, a rolling compact-JSON file, and optionally Seq, Elasticsearch or OTLP (`ObservabilityOptions.LogSink`). Logs are enriched with user, trace and span ids. `RequestResponseBodyLoggingMiddleware` logs size-capped bodies and redacts the sensitive paths listed in `requestLogging.json`.
- **Tracing and metrics** use OpenTelemetry over OTLP with ASP.NET Core, HTTP client, EF Core, Npgsql, runtime and process instrumentation. Each module has a static `{M}Telemetry` class with an `ActivitySource` and a `Meter` named `ModularMonolith.{M}`, registered through `IModule.ActivitySourceNames` and `MeterNames`. Start a span only when it adds insight (`StartActivityForCaller`), and record metrics inside `.TapAsync` so they fire on success only.
- **Health**: `/health/live`, `/health/ready`, `/health/startup` and `/health/dependencies`. PostgreSQL, RabbitMQ and Redis (when configured) gate readiness. Keycloak is on the dependency probe only, and only where IAM runs.
- **Security headers, forwarded headers and CORS** are configured in `securityHeaders.json`, `reverseProxy.json` and `cors.json`.
- **Aspire Dashboard** receives traces, metrics and logs in local development (`http://localhost:18888`).
