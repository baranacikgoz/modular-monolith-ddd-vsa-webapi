# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-11-01)

## Corpus Check
- 882 files · ~192,947 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 5955 nodes · 17925 edges · 333 communities (220 shown, 113 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 871 edges (avg confidence: 0.87)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `1b41f299`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- StockMovement
- DateTimeOffset
- microsoft_extensions_logging
- Products.Endpoints
- List
- ServiceAccountTokenCache
- ProductImportJobRunner.cs
- TimeProvider
- DomainEvent
- Result
- Common.Domain.ResultMonad
- ProductExportJobRunner
- ProductId
- Response
- CurrentUser
- EmailOptions
- OtpOptions
- Response
- microsoft_extensions_dependencyinjection
- FullTextSearchOptions
- BoundedRequestCaptureStream
- DeviceRegistration
- .ReserveSeriesAsync
- IInterModuleRequestClient
- InventoryOptions
- Response
- CustomRateLimitingOptions
- StockLevel
- DefaultIdType
- Error
- Projections and reconciliation
- IDbContext
- Response
- Response
- OutboxMessage
- IModule
- AdminRepresentations.cs
- microsoft_entityframeworkcore
- ObservabilityOptions
- IResxLocalizer
- IInventoryDbContext
- BoundedCaptureStream
- PaginationQueryableExtensions
- system_diagnostics
- OutboxOptions
- ProcessedMessage
- Outbox Misuse Check
- ReservationStatus
- Add Integration Event Command
- host
- Response
- CaptchaOptions
- ResiliencePipeline
- RedisFixedWindowRateLimiter
- RowParser.Parse
- AuditLogEntry
- Cross-Module Reference Violation
- PaginationCursor
- Request
- Bogus Test Data
- BrevoEmailGateway
- WarehouseConnection
- Setup.AddCommonObjectStorage
- Host.Swagger
- RequestBody
- .CreateAsync
- PackageDimensions
- .AddWarehouseGatewayInfrastructure
- Type
- Getting started
- Seeder
- FormApiWarehouseGateway
- AsNoTracking Coverage Check
- IIntegrationEventOutbox
- ProvisionUserRequestHandler
- InterModuleRequestHandler
- KeycloakOptions
- Response
- Request
- Request
- CachingOptions
- common_application_localization_resources
- EntityTypeBuilder
- RequestResponseBodyLoggingMiddleware
- WarehouseSecretReprotectSweepService
- RequestLoggingOptions
- Configuration-Driven Module Registration
- Response
- StoreAccessReconciliationJob
- InventoryDbContext
- BackgroundJobsTelemetry
- DatabaseSeederOrchestrator
- SecretProtector
- DefaultResponsesOperationFilter
- ILogger
- Commit/Request.cs
- Response
- ReplaceCredentials/Request.cs
- Task
- ResiliencyOptions
- NotificationsTelemetry
- StockBulkAdjustJob
- Common.Domain.StronglyTypedIds
- Response
- Common.InterModuleRequests.Contracts
- WarehouseConnectionResponse
- VerifyPhoneOtpResponse
- Response
- V1ProductAddedToStoreDomainEvent
- WarehouseHttpGateway
- Captcha/VersionNeutral/Setup.cs
- CustomValidator
- Response
- InterModuleRequestOptions
- Receive/Request.cs
- ApplicationUserId
- NotificationPayload
- depends_on
- healthcheck
- mm.keycloak
- mm.redis
- Response
- Response
- ProductsOptions
- ChangeLowStockThreshold/Request.cs
- StockReservationId
- .DownloadExportAsync
- ProductImportJobRunner
- docker-compose.yml (Base Stack)
- IServiceCollection
- ObjectStorageOptions
- AuditLogRetentionService
- PushOptions
- IAuditableEntity
- HttpContext
- services
- mm.postgres
- AuditableEntityResponse
- IStronglyTypedId
- mm.rabbitmq
- AssignStoreOwnerRoleOnStoreApprovedHandler
- OutboxOptions.cs
- Response
- Response
- StockPushDirtyItem
- Response
- Request
- OpenApiOptions
- Response
- README (Boilerplate Overview)
- NetGsmSmsGateway
- KeycloakPermissionAuthorizationHandler
- DateOnly
- CachingOptions.cs
- ResultTelemetryExtensions
- Stores/v1/My/Update/Request.cs
- StatusSnapshot
- BackgroundJobsOptions
- ConfigureSwaggerOptions
- SendRequestBody
- IOptions
- ProductSummariesReconciliationJob
- fluentvalidation
- .SaveBatchAsync
- IProductsDbContext
- ApplyAuditingInterceptor
- Response
- Notifications.Application/IAssemblyReference.cs
- WarehouseConnections/v1/Create/Request.cs
- Product
- Response
- ProductImportJob
- Response
- IInventoryDbContext
- CorsOptions
- SendForEmail/Request.cs
- V1ProductAttributesReplacedDomainEvent
- SmsOptions
- RabbitMqOptions
- IReceiveEndpointConfigurator
- JobRow
- ChunkEncodingFreeS3Client
- SecretProtectionOptions
- Uri
- NotificationsHub
- RemoveImage/Request.cs
- AuditableEntity
- AI-assisted development workflow
- Endpoint.AddProductAsync
- v1/RemoveProduct/Request.cs
- Response
- Keycloak realm as code
- Response
- Response
- SetAttributes/Request.cs
- StronglyTypedIdBinder.cs
- Common.InterModuleRequests.Products
- EmailOtpDispatchOutcome
- KeycloakScopes
- ReCaptchaService
- Response
- Your first requests
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- Response
- JwtBearerConfigureOptions
- ProviderStatusSnapshot
- ProductTemplates/v1/Search/Request.cs
- StronglyTypedIdSchemaFilter.cs
- Response
- .SearchAsync
- Platform building blocks
- fluentvalidation
- BulkAdjust/Request.cs
- AuditLogOptions
- ReleaseReconcileOutcome
- SmsCategory
- Response
- IServiceProvider
- Response
- Split-Deployment PoC
- Import/Request.cs
- Key decisions
- ProductTemplates/v1/Create/Request.cs
- ProductTemplates/v1/Deactivate/Request.cs
- Response
- Setup
- Approve/Request.cs
- FirebasePushGateway
- Activate/Request.cs
- WarehouseAddressRefusedException
- Common.InterModuleRequests/Setup.cs
- JsonbListExtensions
- CachingEntryDefaults
- ConstraintViolationExtensions
- ResxLocalizationOptions
- Response
- ProjectionReconciliationOptions
- RequestBody
- DevicesOptions
- system_globalization
- How it works
- IAM.Domain
- WarehouseAddressPolicy
- SignalROptions
- Response
- RequestBody
- Products.Domain/IAssemblyReference.cs
- IAM.Application/IAssemblyReference.cs
- Products.Infrastructure/IAssemblyReference.cs
- Products.Application/IAssemblyReference.cs
- Common.Infrastructure/IAssemblyReference.cs
- Notifications.Domain/IAssemblyReference.cs
- IAM.Infrastructure/IAssemblyReference.cs
- .VerifySha256
- Infrastructure/StringExtensions.cs
- Inventory.Application/IAssemblyReference.cs
- Inventory.Infrastructure/IAssemblyReference.cs
- Inventory.Domain/IAssemblyReference.cs
- RouteGroupBuilder
- v1/Request.cs
- ReverseProxyOptions
- .ReadRows
- Common.Domain.Events
- microsoft_aspnetcore_mvc
- DomainEventHandler
- IEventBus
- IntegrationEvent
- MassTransit IConsumer
- ModuleInstaller
- IInterModuleRequestClient
- Add Inter-Module Request Command
- InterModuleRequestHandler
- InterModuleRequest
- Audit Architecture Command
- Localization Drift Check (IResxLocalizer)
- Mapping Library Usage Check
- REPR Minimal API (No Controllers)
- Execute Feature Command
- Aggregate RaiseEvent
- Functional Result Pipeline
- Execute Refactor Command
- Fix Bug Command
- OTel Trace ID Diagnosis
- Scientific Red/Green Bug-Fix Method
- Manage Feature Flag Command
- FeatureManagement Config
- RequireFeature Endpoint Gate
- Manage Migration Command
- Debezium CDC Connector
- EF Core Migration
- Idempotent SQL Script
- Plan Feature Command
- Module Boundary Identification
- Telemetry Plan (ActivitySource/Meter)
- Plan Refactor Command
- Scaffold Feature Command
- TapAsync Result Extension
- Vertical Slice (VSA)
- BaseDbContext
- Scaffold Module Command
- IModule Implementation
- IntegrationTestFactory
- Split-Project DDD Layering
- Module Telemetry Class
- Scaffold Test Command
- IClassFixture Test Pattern
- OutboxMessages DB Assertion
- Update Dependencies Command
- Central Package Management
- Verify Feature Command
- InterModuleRequests (Sync Cross-Module)
- BackgroundJobs Module
- Outbox Module
- REPR Pattern (Minimal API Endpoints)

## God Nodes (most connected - your core abstractions)
1. `Result` - 313 edges
2. `Common.Application.Options` - 205 edges
3. `Common.Domain.ResultMonad` - 181 edges
4. `ApplicationUserId` - 126 edges
5. `CustomValidator` - 122 edges
6. `Common.Application.Auth` - 113 edges
7. `IInventoryDbContext` - 108 edges
8. `Common.Application.Extensions` - 107 edges
9. `Common.Domain.StronglyTypedIds` - 106 edges
10. `Common.Application.Validation` - 105 edges

## Surprising Connections (you probably didn't know these)
- `Authorization` --references--> `ICurrentUser`  [INFERRED]
  docs/platform.md → src/Common/Common.Application/Auth/ICurrentUser.cs
- `Queued work: accept, queue, poll` --references--> `ObjectStorageOptions`  [INFERRED]
  docs/getting-started.md → src/Common/Common.Application/Options/ObjectStorageOptions.cs
- `The commands` --references--> `IModule`  [INFERRED]
  docs/ai-workflow.md → src/Common/Common.Infrastructure/Modules/IModule.cs
- `Adding a module or a feature` --references--> `IModule`  [INFERRED]
  docs/modules.md → src/Common/Common.Infrastructure/Modules/IModule.cs
- `Watch two modules cooperate` --references--> `ProductCreatedIntegrationEvent`  [INFERRED]
  docs/getting-started.md → src/Common/Common.IntegrationEvents/Products.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Local Infrastructure Stack** — docker_compose_postgres, docker_compose_rabbitmq, docker_compose_redis, docker_compose_aspire_dashboard [EXTRACTED 1.00]

## Communities (333 total, 113 thin omitted)

### Community 0 - "StockMovement"
Cohesion: 0.12
Nodes (14): StockMovement, Quantity, QuantityOnHandAfter, Reason, ReservationId, Sequence, StockLevelId, Type (+6 more)

### Community 1 - "DateTimeOffset"
Cohesion: 0.07
Nodes (24): UtcDateTimeOffsetConverter, V1StockReservationCommitConflictDetectedDomainEvent, V1StockReservationExpiredDomainEvent, V1StockReservationReleaseAttemptAbandonedDomainEvent, V1StockReservationReleaseAttemptStartedDomainEvent, V1StockReservationReleasedDomainEvent, ReservationStatus, Active (+16 more)

### Community 2 - "microsoft_extensions_logging"
Cohesion: 0.05
Nodes (38): Inventory.Application.WarehouseConnections, Inventory.Infrastructure.IntegrationEventHandlers, Common.Application.Persistence.Inbox, Notifications.Infrastructure.Email, Inventory.Application.Persistence, Inventory.Domain.StockCounters, Inventory.Application.ProductSummaries, Inventory.Application.IntegrationEventHandlers (+30 more)

### Community 5 - "List"
Cohesion: 0.06
Nodes (35): ApplicationUserId, IsEmpty, Value, GetActiveSessionIdsRequest, GetActiveSessionIdsResponse, UserSessionIds, GetUsersInRolePageRequest, GetUsersInRolePageResponse (+27 more)

### Community 6 - "ServiceAccountTokenCache"
Cohesion: 0.16
Nodes (6): IServiceAccountTokenProvider, ServiceAccountTokenCache, AccessToken, ExpiresAt, ServiceAccountTokenProvider, Setup

### Community 7 - "ProductImportJobRunner.cs"
Cohesion: 0.06
Nodes (11): Products.Infrastructure.Spreadsheets, Products.Infrastructure.Imports, Products.Application.Imports, Common.Infrastructure.Storage, Host.Middlewares, Common.Application.Storage, Products.Infrastructure.Jobs, Products.Infrastructure.Exports (+3 more)

### Community 8 - "TimeProvider"
Cohesion: 0.07
Nodes (26): BackgroundJobs, Job rows, JobHousekeepingJob, JobHousekeepingOptions, PageSize, RetentionHours, StaleAfterMinutes, IPrivateObjectStore (+18 more)

### Community 9 - "DomainEvent"
Cohesion: 0.06
Nodes (27): Inventory.Domain.StockReservations.DomainEvents.v1, Inventory.Application.StockReservations.DomainEventHandlers.v1, Inventory, AggregateRoot, Events, Id, Version, IAggregateRoot (+19 more)

### Community 10 - "Result"
Cohesion: 0.04
Nodes (12): IBackgroundJobs, CollectionExtensions, FeatureFlagResultExtensions, Result, Error, IsFailure, Success, AsyncExtensions (+4 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.07
Nodes (32): Inventory.Application.StockLevels, Inventory.Endpoints.StockLevels.v1.Search, Common.Endpoints.Webhooks, Inventory.Application.StockReservations, Common.Application.Search, Products.Domain.Products.Errors, Inventory.Endpoints.StockReservations.v1.WebhookCallback, Common.Application.AuditLog (+24 more)

### Community 12 - "ProductExportJobRunner"
Cohesion: 0.10
Nodes (15): IBackgroundUserContext, UserId, BackgroundUserContext, UserId, Setup, ProductExportRequestedIntegrationEvent, ProductExportJob, FileName (+7 more)

### Community 13 - "ProductId"
Cohesion: 0.05
Nodes (22): Products.Domain.Products.DomainEvents.v1, Products.Application.Products.DomainEventHandlers.v1, StronglyTypedIdHelper, V1ProductImageRemovedDomainEventHandlers, ProductChangedIntegrationEventPublishingHandler, V1ProductNameUpdatedDomainEventHandlers, V1ProductImageAddedDomainEvent, V1ProductNameUpdatedDomainEvent (+14 more)

### Community 14 - "Response"
Cohesion: 0.13
Nodes (13): IAM.Endpoints.Users.VersionNeutral.Me.Get, Endpoint, Response, BirthDate, CreatedOn, Email, FirstName, Id (+5 more)

### Community 15 - "CurrentUser"
Cohesion: 0.15
Nodes (9): CurrentUser, BackgroundUserIdAsString, Id, IdAsString, IsAuthenticated, Principal, Roles, SessionId (+1 more)

### Community 16 - "EmailOptions"
Cohesion: 0.10
Nodes (23): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+15 more)

### Community 17 - "OtpOptions"
Cohesion: 0.06
Nodes (25): OtpOptions, DummyCode, EmailQuotaWindowMinutes, ExpirationInMinutes, Length, MaxSendsPerEmailPerWindow, MaxSendsPerPhonePerWindow, PhoneQuotaWindowMinutes (+17 more)

### Community 18 - "Response"
Cohesion: 0.15
Nodes (12): Response, ActingUserId, ActingUserName, CreatedOn, Id, Quantity, QuantityOnHandAfter, Reason (+4 more)

### Community 19 - "microsoft_extensions_dependencyinjection"
Cohesion: 0.03
Nodes (33): Common.Infrastructure.Modules, Notifications.Infrastructure.Devices, Notifications.Application.Push, Inventory.Endpoints, Products.Endpoints.Stores, Common.Endpoints.Versioning, Products.Endpoints.Probe, Common.Infrastructure.RateLimiting (+25 more)

### Community 20 - "FullTextSearchOptions"
Cohesion: 0.15
Nodes (15): Configuration, Full-Text Search, Gotchas, Non-goals, Per-entity strategy, Testing, The problem, TL;DR: what the feature gives you (+7 more)

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.13
Nodes (6): BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 22 - "DeviceRegistration"
Cohesion: 0.12
Nodes (12): DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive, LastReconciledOn, PushToken, PushTokenUpdatedOn (+4 more)

### Community 23 - ".ReserveSeriesAsync"
Cohesion: 0.80
Nodes (3): GetProductRequest, GetProductResponse, GetProductRequestHandler

### Community 24 - "IInterModuleRequestClient"
Cohesion: 0.05
Nodes (32): IInterModuleRequest, IInterModuleRequestClient, MassTransitInterModuleRequestClient, DeprovisionUserRequest, DeprovisionUserResponse, GetUsersRequest, GetUsersResponse, UserSummary (+24 more)

### Community 25 - "InventoryOptions"
Cohesion: 0.05
Nodes (36): InventoryOptions, AllowPrivateWarehouseAddresses, BulkAdjustBatchesPerMessage, BulkAdjustJobConcurrentMessageLimit, BulkAdjustMaxStoredRowErrors, BusinessTimeZoneId, DailyTotalsMaxRangeDays, MaxOccurrencesPerSeries (+28 more)

### Community 26 - "Response"
Cohesion: 0.29
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, Endpoint, Response, EmailVerificationToken, IsRegistered

### Community 27 - "CustomRateLimitingOptions"
Cohesion: 0.05
Nodes (34): StringLocalizerExtensions, CustomRateLimitingOptions, CheckRegistration, CreateStore, Email, ExemptPathPrefixes, Global, OtpVerify (+26 more)

### Community 28 - "StockLevel"
Cohesion: 0.11
Nodes (15): Inventory.Application.StockLevels.DomainEventHandlers.v1, StockLowThresholdRecoveredIntegrationEvent, StockLowThresholdCrossedIntegrationEventPublishingHandler, V1StockLowThresholdCrossedDomainEventHandlers, StockLowThresholdRecoveredIntegrationEventPublishingHandler, V1StockLowThresholdRecoveredDomainEventHandlers, V1StockLowThresholdCrossedDomainEvent, V1StockLowThresholdRecoveredDomainEvent (+7 more)

### Community 29 - "DefaultIdType"
Cohesion: 0.18
Nodes (6): IntegrationEventHandlerBase, MaxEventAge, ProductCreatedIntegrationEvent, StoreCreatedIntegrationEvent, CreateStockLevelOnProductCreatedHandler, LogProductCatalogChangeHandler

### Community 30 - "Error"
Cohesion: 0.05
Nodes (25): Error, Key, ParameterName, StatusCode, SubErrors, Value, IResult, Error (+17 more)

### Community 31 - "Projections and reconciliation"
Cohesion: 0.16
Nodes (11): Projections and reconciliation, ProjectionEntity, SourceVersion, ProductChangedIntegrationEvent, UpdateProductSummaryOnProductChangedHandler, ProductSummary, Name, ProductId (+3 more)

### Community 32 - "IDbContext"
Cohesion: 0.18
Nodes (7): IdBound, Value, JobClaimExtensions, IDbContext, AuditLog, ChangeTracker, Database

### Community 33 - "Response"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.Refresh, Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 34 - "Response"
Cohesion: 0.09
Nodes (20): StoreStatus, Active, Inactive, PendingApproval, Suspended, Endpoint, Response, Address (+12 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.07
Nodes (23): Common.Application.Persistence.Outbox, IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, OutboxMessage (+15 more)

### Community 36 - "IModule"
Cohesion: 0.04
Nodes (41): Cross-process call path, How it works, Setup, ICoreModule, IModule, ActivitySourceNames, MeterNames, Name (+33 more)

### Community 37 - "AdminRepresentations.cs"
Cohesion: 0.08
Nodes (23): CredentialRepresentation, Temporary, Type, Value, ErrorRepresentation, Error, ErrorDescription, ErrorMessage (+15 more)

### Community 38 - "microsoft_entityframeworkcore"
Cohesion: 0.07
Nodes (18): Common.Infrastructure.Persistence.Inbox, Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Products.Infrastructure.Persistence.Seeding, Products.Application.Exports, Common.Application.Persistence, Inventory.Infrastructure.Persistence.EntityConfigurations, Notifications.Domain.Devices (+10 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.08
Nodes (18): ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics, EnableTracing, LogSink, MinimumLevel (+10 more)

### Community 40 - "IResxLocalizer"
Cohesion: 0.12
Nodes (3): ProblemDetailsServiceExtensions, RateLimitingMiddleware, JwtBearerConfigureOptions

### Community 41 - "IInventoryDbContext"
Cohesion: 0.06
Nodes (18): PersistenceQueryableExtensions, IInventoryDbContext, ProductSummaries, StockBulkAdjustJobs, StockLevels, StockMovements, StockPushDirtyItems, StockReservations (+10 more)

### Community 42 - "BoundedCaptureStream"
Cohesion: 0.12
Nodes (8): Object storage, ImageSignature, BoundedCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 43 - "PaginationQueryableExtensions"
Cohesion: 0.12
Nodes (8): CursorBound, Value, KeyedRow, Item, Sort, Tie, PaginationQueryableExtensions, ParameterReplacer

### Community 44 - "system_diagnostics"
Cohesion: 0.05
Nodes (11): BackgroundJobs.Telemetry, Products.Endpoints.Products.v1.Import, BackgroundJobs, IAM.Infrastructure.Auth, Notifications.Infrastructure.Hubs, Inventory.Endpoints.StockLevels.v1.BulkAdjust, Notifications.Application.Hubs, Products.Endpoints.Products.v1.Export (+3 more)

### Community 45 - "OutboxOptions"
Cohesion: 0.07
Nodes (25): Outbox, OutboxOptions, BaseBackoffSeconds, BatchSize, ClaimLeaseSeconds, Cleanup, IsProcessor, LagThresholdMinutes (+17 more)

### Community 46 - "ProcessedMessage"
Cohesion: 0.12
Nodes (10): ProcessedMessage, ConsumerName, MessageId, ProcessedOn, ProjectionUpsertExtensions, ProjectionUpsertOutcome, Inserted, Stale (+2 more)

### Community 48 - "ReservationStatus"
Cohesion: 0.07
Nodes (23): Webhooks, RequestBodyLimitFilter, ProviderStatusMap, ProviderStatusResolution, Mapped, Unrecognized, ReservationStatusTransitions, Request (+15 more)

### Community 50 - "host"
Cohesion: 0.18
Nodes (11): context, dockerfile, build, container_name, environment, image, networks, ports (+3 more)

### Community 51 - "Response"
Cohesion: 0.14
Nodes (12): Endpoint, Response, FailureKey, FileName, FinishedOn, JobId, ProductsWritten, QueuedOn (+4 more)

### Community 52 - "CaptchaOptions"
Cohesion: 0.16
Nodes (13): CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey, Provider, ScoreThreshold, SecretKey (+5 more)

### Community 54 - "RedisFixedWindowRateLimiter"
Cohesion: 0.20
Nodes (6): FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration, RetryAfterHeaderExtensions

### Community 56 - "AuditLogEntry"
Cohesion: 0.13
Nodes (9): AuditLogEntry, AggregateId, AggregateType, Event, EventType, Version, AuditableEntityConfiguration, AuditLogEntryConfiguration (+1 more)

### Community 58 - "PaginationCursor"
Cohesion: 0.43
Nodes (3): PaginationCursor, Payload, TaggedValue

### Community 59 - "Request"
Cohesion: 0.15
Nodes (13): Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken (+5 more)

### Community 61 - "BrevoEmailGateway"
Cohesion: 0.11
Nodes (15): BrevoEmailGateway, SendContact, Email, Name, SendErrorBody, Code, Message, SendRequestBody (+7 more)

### Community 63 - "WarehouseConnection"
Cohesion: 0.15
Nodes (12): WarehouseConnection, BaseUrl, EncryptedApiCredential, EncryptedWebhookSecret, Provider, Status, StoreId, WarehouseProvider (+4 more)

### Community 65 - "Host.Swagger"
Cohesion: 0.18
Nodes (4): Host, Host.Swagger, Program, Setup

### Community 66 - "RequestBody"
Cohesion: 0.18
Nodes (10): RequestBody, FirstDeadline, IntervalDays, OccurrenceCount, ProductId, QuantityPerOccurrence, Warehouses, WarehouseAllocation (+2 more)

### Community 67 - ".CreateAsync"
Cohesion: 0.18
Nodes (8): Inventory.Endpoints.WarehouseConnections.v1.Create, WebhookRoute, Endpoint, Response, Id, WebhookPath, WebhookSecret, Endpoint

### Community 68 - "PackageDimensions"
Cohesion: 0.11
Nodes (12): Common.Domain, Persistence conventions, ValueObject, DimensionsSnapshot, V1ProductPackageDimensionsChangedDomainEvent, V1ProductPackageDimensionsChangedDomainEventExtensions, PackageDimensions, HeightCm (+4 more)

### Community 69 - ".AddWarehouseGatewayInfrastructure"
Cohesion: 0.27
Nodes (4): Setup, IWarehouseHostResolver, SystemWarehouseHostResolver, WarehouseConnectGuard

### Community 70 - "Type"
Cohesion: 0.27
Nodes (3): StronglyTypedIdReadOnlyJsonConverter, InterModuleRequestHandlerDefinition, RequestType

### Community 71 - "Getting started"
Cohesion: 0.18
Nodes (11): Configuration, Getting started, Prerequisites, Run the API from the command line instead, Run the load test, Run the tests, Run the whole system, Seeded data (+3 more)

### Community 72 - "Seeder"
Cohesion: 0.27
Nodes (4): GetSeedUserIdsRequest, GetSeedUserIdsResponse, GetSeedUserIdsRequestHandler, Seeder

### Community 73 - "FormApiWarehouseGateway"
Cohesion: 0.19
Nodes (9): FormApiWarehouseGateway, HoldEnvelope, HoldResult, ErrorResponseBody, JsonApiWarehouseGateway, PushStockItem, PushStockRequestBody, ReleaseRequestBody (+1 more)

### Community 75 - "IIntegrationEventOutbox"
Cohesion: 0.05
Nodes (46): DomainEventHandlerBase, IIntegrationEventOutbox, ProductImageObjectsOrphanedIntegrationEvent, StoreAccessChangedIntegrationEvent, MovementTypeSnapshot, Adjusted, Received, ReservationCommitted (+38 more)

### Community 76 - "ProvisionUserRequestHandler"
Cohesion: 0.18
Nodes (13): Adding a module or a feature, How modules fit together, IAM, Modules, ProvisionUserFailureReason, EmailAlreadyRegistered, IdentityProviderUnavailable, None (+5 more)

### Community 77 - "InterModuleRequestHandler"
Cohesion: 0.23
Nodes (6): IInterModuleRequestHandler, InterModuleRequestHandler, DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, GetDeviceSessionsRequestHandler

### Community 78 - "KeycloakOptions"
Cohesion: 0.05
Nodes (35): KeycloakOptions, AttemptTimeoutSeconds, Authority, BaseUrl, DecisionCacheMaxDurationSeconds, Realm, RequireHttpsMetadata, ResourceClientId (+27 more)

### Community 79 - "Response"
Cohesion: 0.14
Nodes (12): IAM.Endpoints.Users.VersionNeutral.Search, Endpoint, Response, BirthDate, CreatedOn, Email, Enabled, FirstName (+4 more)

### Community 80 - "Request"
Cohesion: 0.17
Nodes (12): Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FirstName, LastName (+4 more)

### Community 81 - "Request"
Cohesion: 0.18
Nodes (11): Request, Email, EmailVerificationToken, FirstName, LastName, Password, PhoneNumber, StoreAddress (+3 more)

### Community 82 - "CachingOptions"
Cohesion: 0.05
Nodes (44): Notifications, Transactional outbox and inbox, AlertRecipientsOptions, EmailHtmlBodyTemplate, EmailSubjectTemplate, EmailTextBodyTemplate, ReconcileCron, RoleName (+36 more)

### Community 83 - "common_application_localization_resources"
Cohesion: 0.04
Nodes (57): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Products.v1.ListImports, Inventory.Endpoints.StockReservations.v1.My.Search, Products.Endpoints.Stores.v1.My.AuditLog, Inventory.Endpoints.WarehouseConnections.v1.Search, Inventory.Endpoints.StockLevels.v1.ListBulkAdjustments, Inventory.Endpoints.WarehouseConnections.v1.My.Search, Products.Endpoints.Products.v1.AuditLog (+49 more)

### Community 84 - "EntityTypeBuilder"
Cohesion: 0.09
Nodes (20): AuditableEntityConfiguration, JobRowConfiguration, DomainEventConverter, IntegrationEventConverter, StronglyTypedIdValueConverter, StoreAccess, IsActive, OwnerId (+12 more)

### Community 86 - "WarehouseSecretReprotectSweepService"
Cohesion: 0.27
Nodes (3): Setup, WarehouseSecretReprotectSweepJobRegistrar, WarehouseSecretReprotectSweepService

### Community 87 - "RequestLoggingOptions"
Cohesion: 0.13
Nodes (15): RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes, SensitiveQueryParamPaths (+7 more)

### Community 89 - "Response"
Cohesion: 0.22
Nodes (8): Endpoint, Response, Address, Description, Name, OwnerId, ProductCount, Status

### Community 90 - "StoreAccessReconciliationJob"
Cohesion: 0.38
Nodes (7): GetStoresPageRequest, GetStoresPageResponse, StorePageItem, Setup, StoreAccessReconciliationCursor, StoreAccessReconciliationJob, GetStoresPageRequestHandler

### Community 91 - "InventoryDbContext"
Cohesion: 0.04
Nodes (41): IDatabaseSeeder, Priority, EventDispatcher, IntegrationEventOutbox, HasPending, Setup, BaseDbContext, AuditLog (+33 more)

### Community 94 - "SecretProtector"
Cohesion: 0.17
Nodes (4): Secret protection, SecretGenerator, SecretProtector, Setup

### Community 95 - "DefaultResponsesOperationFilter"
Cohesion: 0.29
Nodes (3): DefaultResponsesOperationFilter, RemoveDefaultResponseSchemaFilter, SwaggerDefaultValues

### Community 96 - "ILogger"
Cohesion: 0.05
Nodes (12): ProjectionReconciliationJob, ActorNames, LoginCompletion, RegistrationCompletion, ProductSummariesReconciliationJobRegistrar, StoreAccessReconciliationJobRegistrar, NotificationsHub, OutboxModule (+4 more)

### Community 97 - "Commit/Request.cs"
Cohesion: 0.28
Nodes (8): Inventory.Endpoints.StockReservations.v1.Commit, Request, Body, Id, RequestBody, ProviderReference, RequestBodyValidator, RequestValidator

### Community 98 - "Response"
Cohesion: 0.08
Nodes (21): BulkImportOptions, MaxFileBytes, MaxRows, SaveChunkSize, BulkImportOptionsValidator, Endpoint, Response, FailureKey (+13 more)

### Community 99 - "ReplaceCredentials/Request.cs"
Cohesion: 0.28
Nodes (8): Inventory.Endpoints.WarehouseConnections.v1.ReplaceCredentials, Request, Body, Id, RequestBody, ApiCredential, RequestBodyValidator, RequestValidator

### Community 100 - "Task"
Cohesion: 0.04
Nodes (17): OtpCacheEntry, IVariantFeatureManagerExtensions, IInboxStore, IPublicObjectStore, ObjectListItem, ObjectListPage, ObjectMetadata, UploadObjectRequest (+9 more)

### Community 101 - "ResiliencyOptions"
Cohesion: 0.06
Nodes (25): KeyedResilienceProfile, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, RateLimitFailOpen, RateLimitPermits (+17 more)

### Community 102 - "NotificationsTelemetry"
Cohesion: 0.13
Nodes (6): RequireFeatureFilter, RouteHandlerBuilderExtensions, IamTelemetry, InventoryTelemetry, NotificationsTelemetry, ProductsTelemetry

### Community 103 - "StockBulkAdjustJob"
Cohesion: 0.08
Nodes (21): StockBulkAdjustRequestedIntegrationEvent, StockBulkAdjustJob, ClientRequestId, FileName, LastProcessedRow, ObjectKey, Reason, RequeueCount (+13 more)

### Community 104 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.03
Nodes (29): Inventory.Domain.WebhookDeliveries, Inventory.Domain.StockLevels.Errors, Products.Endpoints.Stores.v1.GetApplication, Inventory.Infrastructure.InterModuleRequestHandlers, Common.Domain.StronglyTypedIds, Products.Endpoints.Stores.v1.Search, Products.Endpoints.Stores.v1.ListAll, Products.Endpoints.Stores.v1.Apply (+21 more)

### Community 105 - "Response"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegisterByEmail, Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 106 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.03
Nodes (47): Notifications.Application.Otp, Common.Endpoints, Notifications.Application.Sms, IAM.Infrastructure.Keycloak.Representations, Common.Application.Caching, Notifications.Infrastructure.Sms, Common.Application.FeatureManagement, IAM.Endpoints.Otp (+39 more)

### Community 107 - "WarehouseConnectionResponse"
Cohesion: 0.13
Nodes (11): WarehouseConnectionStatus, Active, Deactivated, Endpoint, Endpoint, Endpoint, WarehouseConnectionResponse, BaseUrl (+3 more)

### Community 108 - "VerifyPhoneOtpResponse"
Cohesion: 0.24
Nodes (8): OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, VerifyPhoneOtpResponseExtensions, VerifyPhoneOtpRequestHandler

### Community 109 - "Response"
Cohesion: 0.33
Nodes (4): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, Endpoint, Response, IsRegistered

### Community 110 - "V1ProductAddedToStoreDomainEvent"
Cohesion: 0.25
Nodes (6): ProductSnapshot, V1ProductAddedToStoreDomainEvent, V1ProductAddedToStoreDomainEventExtensions, ProductSnapshot, V1ProductRemovedFromStoreDomainEvent, V1ProductRemovedFromStoreDomainEventExtensions

### Community 112 - "Captcha/VersionNeutral/Setup.cs"
Cohesion: 0.22
Nodes (6): IAM.Endpoints.Captcha.VersionNeutral, IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, Endpoint, Response, ClientKey, Setup

### Community 113 - "CustomValidator"
Cohesion: 0.04
Nodes (69): Inventory.Endpoints.StockReservations.v1.ResolveReadOnly, Products.Endpoints.Stores.v1.Update, Inventory.Endpoints.StockLevels.v1.Adjust, Products.Endpoints.Products.v1.My.Update, Products.Endpoints.Products.v1.My.SetPackageDimensions, Products.Endpoints.Products.v1.Update, CustomValidator, Request (+61 more)

### Community 114 - "Response"
Cohesion: 0.14
Nodes (12): IAM.Endpoints.Users.VersionNeutral.Get, Endpoint, Response, BirthDate, CreatedOn, Email, Enabled, FirstName (+4 more)

### Community 115 - "InterModuleRequestOptions"
Cohesion: 0.25
Nodes (7): InterModuleRequestOptions, DependencyUnavailableRetryAfterSeconds, HandlerConcurrentMessageLimit, HandlerPrefetchCount, Timeouts, TimeoutSeconds, InterModuleRequestOptionsValidator

### Community 116 - "Receive/Request.cs"
Cohesion: 0.24
Nodes (9): Inventory.Endpoints.StockLevels.v1.Receive, Request, Body, Id, RequestBody, Quantity, Reason, RequestBodyValidator (+1 more)

### Community 118 - "NotificationPayload"
Cohesion: 0.31
Nodes (4): INotificationDispatcher, INotificationsClient, NotificationPayload, SignalRNotificationDispatcher

### Community 119 - "depends_on"
Cohesion: 0.22
Nodes (9): mm.aspire-dashboard, mm.keycloak, mm.postgres, mm.rabbitmq, depends_on, condition, condition, condition (+1 more)

### Community 120 - "healthcheck"
Cohesion: 0.56
Nodes (9): interval, retries, start_period, test, timeout, healthcheck, healthcheck, healthcheck (+1 more)

### Community 121 - "mm.keycloak"
Cohesion: 0.22
Nodes (9): command, container_name, environment, image, networks, ports, restart, volumes (+1 more)

### Community 122 - "mm.redis"
Cohesion: 0.22
Nodes (9): command, container_name, environment, image, networks, ports, restart, volumes (+1 more)

### Community 123 - "Response"
Cohesion: 0.12
Nodes (14): IAM.Endpoints.Users.VersionNeutral.SelfRegister, IdentityScheme, Email, PhoneNumber, IdentitySchemeOptions, Scheme, IdentitySchemeOptionsValidator, Setup (+6 more)

### Community 124 - "Response"
Cohesion: 0.22
Nodes (7): Products.Endpoints.Products.v1.Search, Endpoint, Response, Description, Name, Price, Quantity

### Community 125 - "ProductsOptions"
Cohesion: 0.10
Nodes (19): Products.Endpoints.Products.v1.AddImage, ProductsOptions, ExportJobConcurrentMessageLimit, ExportMaxRows, ExportPageSize, ImageMaxFileBytes, ImportBatchesPerMessage, ImportJobConcurrentMessageLimit (+11 more)

### Community 126 - "ChangeLowStockThreshold/Request.cs"
Cohesion: 0.28
Nodes (8): Inventory.Endpoints.StockLevels.v1.ChangeLowStockThreshold, Request, Body, Id, RequestBody, Threshold, RequestBodyValidator, RequestValidator

### Community 127 - "StockReservationId"
Cohesion: 0.13
Nodes (11): StockReservationId, WarehouseConnectionId, WebhookDelivery, ConnectionId, DeliveryId, ReceivedOn, ReservationId, WebhookDeliveryId (+3 more)

### Community 128 - ".DownloadExportAsync"
Cohesion: 0.17
Nodes (9): JobStatus, Failed, Queued, Running, Succeeded, Endpoint, FileDownload, FileReference (+1 more)

### Community 129 - "ProductImportJobRunner"
Cohesion: 0.16
Nodes (9): ProductImportRequestedIntegrationEvent, ProductImportJobId, ProductImportJobRunner, RowOutcome, Created, Rejected, Unchanged, Updated (+1 more)

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.25
Nodes (10): Observability (OpenTelemetry), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), external, name, networks, local_shared_network, Postgres Service (mm.postgres) (+2 more)

### Community 131 - "IServiceCollection"
Cohesion: 0.08
Nodes (6): Setup, Setup, ModuleRegistry, Setup, Setup, Setup

### Community 132 - "ObjectStorageOptions"
Cohesion: 0.08
Nodes (26): ObjectStorageOptions, AccessKey, AdditionalTransientStatusCodes, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds (+18 more)

### Community 134 - "AuditLogRetentionService"
Cohesion: 0.27
Nodes (3): AuditLogRetentionJobRegistrar, AuditLogRetentionService, Setup

### Community 135 - "PushOptions"
Cohesion: 0.12
Nodes (20): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri, Type (+12 more)

### Community 136 - "IAuditableEntity"
Cohesion: 0.25
Nodes (5): IAuditableEntity, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn

### Community 137 - "HttpContext"
Cohesion: 0.07
Nodes (18): Rate limiting and resilience, CustomRateLimitingOptionsValidator, FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit, FixedWindowValidator (+10 more)

### Community 138 - "services"
Cohesion: 0.25
Nodes (8): container_name, environment, image, networks, ports, restart, services, mm.aspire-dashboard

### Community 139 - "mm.postgres"
Cohesion: 0.25
Nodes (8): container_name, environment, image, networks, ports, restart, volumes, mm.postgres

### Community 140 - "AuditableEntityResponse"
Cohesion: 0.07
Nodes (24): AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, Endpoint, Response (+16 more)

### Community 141 - "IStronglyTypedId"
Cohesion: 0.12
Nodes (8): StrictDateTimeOffsetJsonConverter, StronglyTypedIdListReadOnlyJsonConverter, StronglyTypedIdReadOnlyJsonConverter, StronglyTypedIdWriteOnlyJsonConverter, IStronglyTypedId, Value, PolymorphicEventConverter, Endpoint

### Community 142 - "mm.rabbitmq"
Cohesion: 0.25
Nodes (8): container_name, environment, image, networks, ports, restart, volumes, mm.rabbitmq

### Community 143 - "AssignStoreOwnerRoleOnStoreApprovedHandler"
Cohesion: 0.33
Nodes (4): StatelessInboxStore, Instance, StoreApprovedIntegrationEvent, AssignStoreOwnerRoleOnStoreApprovedHandler

### Community 144 - "OutboxOptions.cs"
Cohesion: 0.32
Nodes (7): OutboxCleanupSettings, BatchSize, CronSchedule, Enabled, RetentionDays, OutboxCleanupSettingsValidator, OutboxOptionsValidator

### Community 145 - "Response"
Cohesion: 0.20
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.Create, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt, Setup

### Community 146 - "Response"
Cohesion: 0.17
Nodes (11): Products.Endpoints.Products.v1.My.Get, Endpoint, PackageDimensionsResponse, Response, Attributes, Description, Name, PackageDimensions (+3 more)

### Community 147 - "StockPushDirtyItem"
Cohesion: 0.22
Nodes (9): StockPushDirtyItem, LastErrorKey, MarkedOn, MarkVersion, ProductId, PushAttempts, StockLevelId, StoreId (+1 more)

### Community 148 - "Response"
Cohesion: 0.12
Nodes (13): Inventory.Endpoints.StockReservations.v1.Get, Response, HasPendingReleaseAttempt, Id, IsReadOnly, ProductId, ProviderReference, ProviderStatus (+5 more)

### Community 149 - "Request"
Cohesion: 0.25
Nodes (8): Request, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name, SearchTerm, RequestValidator

### Community 150 - "OpenApiOptions"
Cohesion: 0.22
Nodes (9): OpenApiOptions, ContactEmail, ContactName, Description, EnableSwagger, LicenseName, LicenseUrl, Title (+1 more)

### Community 151 - "Response"
Cohesion: 0.22
Nodes (8): Endpoint, Response, Address, CreatedOn, Description, Id, Name, OwnerId

### Community 153 - "NetGsmSmsGateway"
Cohesion: 0.14
Nodes (8): NetGsmSmsGateway, SendMessageBody, Msg, No, SendResponseBody, Code, Description, JobId

### Community 154 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.15
Nodes (5): KeycloakPermission, RouteHandlerBuilderExtensions, KeycloakPermissionAuthorizationHandler, KeycloakPermissionRequirement, Permission

### Community 155 - "DateOnly"
Cohesion: 0.22
Nodes (7): DayBoundsUtc, TimeProviderExtensions, Request, From, StoreId, To, RequestValidator

### Community 156 - "CachingOptions.cs"
Cohesion: 0.32
Nodes (7): CachingOptionsValidator, Redis, AppName, Host, Password, Port, RedisValidator

### Community 158 - "Stores/v1/My/Update/Request.cs"
Cohesion: 0.33
Nodes (6): Products.Endpoints.Stores.v1.My.Update, Request, Address, Description, Name, RequestValidator

### Community 159 - "StatusSnapshot"
Cohesion: 0.18
Nodes (9): StatusSnapshot, Active, Committed, Expired, Released, RequiresReconciliation, Unknown, V1StockReservationReadOnlyResolvedDomainEvent (+1 more)

### Community 160 - "BackgroundJobsOptions"
Cohesion: 0.29
Nodes (7): BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount, BackgroundJobsOptionsValidator

### Community 162 - "SendRequestBody"
Cohesion: 0.29
Nodes (6): SendRequestBody, AppName, Encoding, IysFilter, Messages, MsgHeader

### Community 163 - "IOptions"
Cohesion: 0.08
Nodes (25): SearchLanguageResolver, UniversalConfig, Setup, Setup, OtpDispatchErrors, SendPhoneOtpRequest, SendPhoneOtpResponse, SmsOtpDispatchOutcome (+17 more)

### Community 164 - "ProductSummariesReconciliationJob"
Cohesion: 0.35
Nodes (7): GetProductsPageRequest, GetProductsPageResponse, ProductPageItem, ProductSummariesReconciliationCursor, ProductSummariesReconciliationJob, Setup, GetProductsPageRequestHandler

### Community 165 - "fluentvalidation"
Cohesion: 0.05
Nodes (42): Common.Application.Validation, DatabaseOptions, ConnectionString, DatabaseOptionsValidator, HealthCheckOptions, EnableHealthChecks, ReadinessTimeoutInSeconds, StartupTimeoutInSeconds (+34 more)

### Community 166 - ".SaveBatchAsync"
Cohesion: 0.12
Nodes (13): StockBulkAdjustRowError, BatchSummary, RowResult, Adjusted, SheetRow, ProductImportRowError, BatchSummary, RowResult (+5 more)

### Community 167 - "IProductsDbContext"
Cohesion: 0.03
Nodes (27): AuditLogDto, ICurrentUser, Id, IdAsString, Roles, SessionId, PaginationResponse, HasNext (+19 more)

### Community 168 - "ApplyAuditingInterceptor"
Cohesion: 0.25
Nodes (7): Add a new language/culture, Add search to a new entity _(Build checklist)_, Change the vector (weights, fields, or config), Extending and maintaining, ApplyAuditingInterceptor, ApplySearchLanguageInterceptor, Setup

### Community 169 - "Response"
Cohesion: 0.13
Nodes (16): Products.Endpoints.Products.v1.Get, GetStockLevelRequest, GetStockLevelResponse, GetStockLevelRequestHandler, Setup, Endpoint, PackageDimensionsResponse, Response (+8 more)

### Community 171 - "WarehouseConnections/v1/Create/Request.cs"
Cohesion: 0.33
Nodes (6): Request, ApiCredential, BaseUrl, Provider, StoreId, RequestValidator

### Community 172 - "Product"
Cohesion: 0.07
Nodes (31): File map _(Build)_, Products, ISearchLocalized, Language, V1ProductDescriptionUpdatedDomainEvent, Product, Attributes, Description (+23 more)

### Community 173 - "Response"
Cohesion: 0.40
Nodes (3): Inventory.Endpoints.StockReservations.v1.ReserveSeries, Response, Ids

### Community 174 - "ProductImportJob"
Cohesion: 0.10
Nodes (15): ProductImportJob, ClientRequestId, FileName, LastProcessedRow, ObjectKey, ProductsCreated, ProductsUpdated, ProductTemplateId (+7 more)

### Community 175 - "Response"
Cohesion: 0.29
Nodes (6): Endpoint, Response, Description, Name, Price, Quantity

### Community 176 - "IInventoryDbContext"
Cohesion: 0.07
Nodes (18): IWarehouseGateway, IWarehouseGatewayFactory, WarehouseStockQuantity, StockLevelId, Endpoint, Response, CommittedReservations, CommittedUnits (+10 more)

### Community 177 - "CorsOptions"
Cohesion: 0.29
Nodes (7): CorsOptions, AllowCredentials, AllowedHeaders, AllowedMethods, AllowedOrigins, MaxAgeInSeconds, CorsOptionsValidator

### Community 178 - "SendForEmail/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForEmail, Request, CaptchaToken, Email, RequestValidator

### Community 179 - "V1ProductAttributesReplacedDomainEvent"
Cohesion: 0.47
Nodes (3): AttributeSnapshot, V1ProductAttributesReplacedDomainEvent, V1ProductAttributesReplacedDomainEventExtensions

### Community 180 - "SmsOptions"
Cohesion: 0.11
Nodes (20): SmsOptions, AppName, AttemptTimeoutSeconds, BaseUrl, MaxPerDay, MaxPerPhoneNumberPerDay, MaxRetryAttempts, MsgHeader (+12 more)

### Community 181 - "RabbitMqOptions"
Cohesion: 0.15
Nodes (13): RabbitMqOptions, DefaultConcurrentMessageLimit, DefaultPrefetchCount, Host, Password, Port, RetryIntervalDeltaMs, RetryLimit (+5 more)

### Community 182 - "IReceiveEndpointConfigurator"
Cohesion: 0.18
Nodes (5): ConcurrencyConfiguratorExtensions, RunStockBulkAdjustJobHandlerDefinition, PushStockToWarehouseHandlerDefinition, RunProductExportJobHandlerDefinition, RunProductImportJobHandlerDefinition

### Community 183 - "JobRow"
Cohesion: 0.17
Nodes (8): JobRow, FailureKey, FinishedOn, Progress, QueuedOn, RequestedBy, StartedOn, Status

### Community 185 - "SecretProtectionOptions"
Cohesion: 0.28
Nodes (5): Troubleshooting, SecretProtectionOptions, EncryptionKey, PreviousEncryptionKeys, SecretProtectionOptionsValidator

### Community 186 - "Uri"
Cohesion: 0.17
Nodes (6): ReCaptchaResponse, ChallengeTs, ErrorCodes, Hostname, Score, Success

### Community 188 - "RemoveImage/Request.cs"
Cohesion: 0.40
Nodes (5): Products.Endpoints.Products.v1.RemoveImage, Request, Id, ImageId, RequestValidator

### Community 189 - "AuditableEntity"
Cohesion: 0.08
Nodes (21): AuditableEntity, AuditableEntity, Id, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, Version (+13 more)

### Community 190 - "AI-assisted development workflow"
Cohesion: 0.25
Nodes (8): Adapting it, AI-assisted development workflow, `CLAUDE.md`, Guards, The commands, The four layers, The issue-to-pull-request automation, The knowledge graph

### Community 191 - "Endpoint.AddProductAsync"
Cohesion: 0.12
Nodes (14): Products.Endpoints.Stores.v1.AddProduct, Endpoint, Request, Body, Id, RequestBody, Description, Name (+6 more)

### Community 192 - "v1/RemoveProduct/Request.cs"
Cohesion: 0.40
Nodes (5): Products.Endpoints.Stores.v1.RemoveProduct, Request, Id, ProductId, RequestValidator

### Community 193 - "Response"
Cohesion: 0.11
Nodes (16): Response, FailureKey, FileName, FinishedOn, JobId, ProcessedRows, QueuedOn, Reason (+8 more)

### Community 194 - "Keycloak realm as code"
Cohesion: 0.22
Nodes (9): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Realms that already exist, Secrets, Seed users (dev / test only) (+1 more)

### Community 195 - "Response"
Cohesion: 0.33
Nodes (4): Inventory.Endpoints.StockReservations.v1.Reserve, Endpoint, Response, Id

### Community 196 - "Response"
Cohesion: 0.29
Nodes (5): Inventory.Endpoints.WarehouseConnections.v1.RotateWebhookSecret, Endpoint, Response, WebhookPath, WebhookSecret

### Community 197 - "SetAttributes/Request.cs"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Products.v1.My.SetAttributes, AttributeItem, Name, Value, Request, Body, Id, RequestBody (+3 more)

### Community 199 - "Common.InterModuleRequests.Products"
Cohesion: 0.16
Nodes (4): Products.Infrastructure.InterModuleRequestHandlers, Inventory.Domain.WarehouseConnections.Errors, Products.Infrastructure.Caching, Common.InterModuleRequests.Products

### Community 200 - "EmailOtpDispatchOutcome"
Cohesion: 0.31
Nodes (7): EmailOtpDispatchErrors, EmailOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, SendEmailOtpRequest, SendEmailOtpResponse

### Community 201 - "KeycloakScopes"
Cohesion: 0.17
Nodes (11): Devices, Hangfire, KeycloakScopes, Products, ProductTemplates, Sessions, StockLevels, StockReservations (+3 more)

### Community 203 - "Response"
Cohesion: 0.50
Nodes (3): Products.Endpoints.ProductTemplates.v1.Create, Response, Id

### Community 205 - "Your first requests"
Cohesion: 0.33
Nodes (6): Connect a store to a warehouse system, Errors, Queued work: accept, queue, poll, Sign in, Watch two modules cooperate, Your first requests

### Community 210 - "Response"
Cohesion: 0.22
Nodes (8): Endpoint, Response, Address, Description, Name, OwnerId, ProductCount, Status

### Community 212 - "ProviderStatusSnapshot"
Cohesion: 0.18
Nodes (9): ProviderStatusSnapshot, Active, Committed, Expired, Released, RequiresReconciliation, Unknown, V1StockReservationProviderStatusMirroredDomainEvent (+1 more)

### Community 213 - "ProductTemplates/v1/Search/Request.cs"
Cohesion: 0.25
Nodes (7): Constants, Request, Brand, Color, Model, SearchTerm, RequestValidator

### Community 215 - "Response"
Cohesion: 0.17
Nodes (10): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, Endpoint, Response, ClientId, DeviceName, Id, IpAddress, IsCurrent (+2 more)

### Community 216 - ".SearchAsync"
Cohesion: 0.26
Nodes (7): Inter-module requests, SearchProductsByTermRequest, SearchProductsByTermResponse, Endpoint, PageResponse, ProductsCacheKeys, SearchProductsByTermRequestHandler

### Community 217 - "Platform building blocks"
Cohesion: 0.11
Nodes (16): Authorization, Caching, Dirty set and lease, Feature flags, Observability, Options and configuration, Pagination and search, Platform building blocks (+8 more)

### Community 218 - "fluentvalidation"
Cohesion: 0.05
Nodes (47): IAM.Domain.Users, IAM.Endpoints.Otp.VersionNeutral.SendForLogin, Common.Domain.Extensions, Notifications.Infrastructure.Devices.UpdateCurrentPushToken, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, Common.Domain.Devices, IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, IAM.Endpoints.Common.Validations (+39 more)

### Community 219 - "BulkAdjust/Request.cs"
Cohesion: 0.40
Nodes (5): Request, ClientRequestId, File, Reason, RequestValidator

### Community 220 - "AuditLogOptions"
Cohesion: 0.33
Nodes (6): AuditLogOptions, PerSchemaRetentionDays, PurgeBatchSize, RetentionCron, RetentionDays, AuditLogOptionsValidator

### Community 221 - "ReleaseReconcileOutcome"
Cohesion: 0.20
Nodes (8): Inventory.Endpoints.StockReservations.v1.ReconcileRelease, Endpoint, Response, Outcome, ReleaseReconcileOutcome, AlreadyReleased, NotReleased, Released

### Community 222 - "SmsCategory"
Cohesion: 0.33
Nodes (4): SmsCategory, CommercialIndividual, CommercialMerchant, Transactional

### Community 223 - "Response"
Cohesion: 0.29
Nodes (4): Products.Endpoints.Stores.v1.Create, Setup, Response, Id

### Community 224 - "IServiceProvider"
Cohesion: 0.08
Nodes (14): IRecurringBackgroundJobs, ResultToAcceptedResponseTransformer, ResultToCreatedResponseTransformer, ResultToResponseTransformer, ResultToResponseTransformer, RouteHandlerBuilderExtensions, StockJobRequeueJobRegistrar, StockJobsHousekeepingRegistrar (+6 more)

### Community 225 - "Response"
Cohesion: 0.29
Nodes (7): Response, Id, LowStockThreshold, ProductId, ProductName, QuantityOnHand, StoreId

### Community 226 - "Split-Deployment PoC"
Cohesion: 0.33
Nodes (6): Concurrent safety, Files added by this PoC, How to run, Split-Deployment PoC, What this proves, What to check

### Community 227 - "Import/Request.cs"
Cohesion: 0.40
Nodes (5): Request, ClientRequestId, File, ProductTemplateId, RequestValidator

### Community 228 - "Key decisions"
Cohesion: 0.29
Nodes (7): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Key decisions

### Community 229 - "ProductTemplates/v1/Create/Request.cs"
Cohesion: 0.40
Nodes (5): Request, Brand, Color, Model, RequestValidator

### Community 230 - "ProductTemplates/v1/Deactivate/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.ProductTemplates.v1.Deactivate, Request, Id, RequestValidator

### Community 231 - "Response"
Cohesion: 0.33
Nodes (4): Products.Endpoints.Stores.v1.My.AddProduct, Endpoint, Response, Id

### Community 233 - "Approve/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Stores.v1.Approve, Request, Id, RequestValidator

### Community 234 - "FirebasePushGateway"
Cohesion: 0.14
Nodes (9): SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, IPushGateway, SendSecurityAlertRequestHandler, DummyPushGateway, FirebasePushGateway (+1 more)

### Community 235 - "Activate/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.ProductTemplates.v1.Activate, Request, Id, RequestValidator

### Community 237 - "Common.InterModuleRequests/Setup.cs"
Cohesion: 0.33
Nodes (3): Common.InterModuleRequests, IAssemblyReference, Setup

### Community 239 - "CachingEntryDefaults"
Cohesion: 0.33
Nodes (6): CachingEntryDefaults, Duration, FactoryHardTimeout, FactorySoftTimeout, FailSafeMaxDuration, FailSafeThrottleDuration

### Community 241 - "ResxLocalizationOptions"
Cohesion: 0.40
Nodes (5): Localization, ResxLocalizationOptions, DefaultCulture, SupportedCultures, ResxLocalizationOptionsValidator

### Community 242 - "Response"
Cohesion: 0.25
Nodes (6): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 243 - "ProjectionReconciliationOptions"
Cohesion: 0.50
Nodes (4): ProjectionReconciliationOptions, MaxPages, PageSize, ProjectionReconciliationOptionsValidator

### Community 244 - "RequestBody"
Cohesion: 0.50
Nodes (4): RequestBody, ProductId, Quantity, ReservationDeadline

### Community 245 - "DevicesOptions"
Cohesion: 0.40
Nodes (5): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator

### Community 246 - "system_globalization"
Cohesion: 0.06
Nodes (32): Common.Application.JsonConverters, Common.Infrastructure.Caching, Common.Infrastructure.Auth, Constants, RouteParameters, ConnectionId, RouteParametersValidator, Constants (+24 more)

### Community 247 - "How it works"
Cohesion: 0.33
Nodes (6): Cross-module term search, How it works, One-time setup _(Build, in a migration)_, Query path, Ranking, Write path

### Community 250 - "SignalROptions"
Cohesion: 0.40
Nodes (5): SignalROptions, RedisConnectionString, RequireRedisBackplaneInProduction, UseRedisBackplane, SignalROptionsValidator

### Community 251 - "Response"
Cohesion: 0.33
Nodes (4): Products.Endpoints.Stores.v1.My.Create, Endpoint, Response, Id

### Community 252 - "RequestBody"
Cohesion: 0.67
Nodes (3): RequestBody, ProviderReference, TargetStatus

### Community 269 - "RouteGroupBuilder"
Cohesion: 0.03
Nodes (38): Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Setup, Endpoint (+30 more)

### Community 272 - "v1/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Probe.v1, Request, Count, RequestValidator

### Community 274 - "ReverseProxyOptions"
Cohesion: 0.33
Nodes (5): ReverseProxyOptions, ForwardLimit, IsEnabled, TrustedNetworks, ReverseProxyOptionsValidator

### Community 284 - ".ReadRows"
Cohesion: 0.14
Nodes (4): StringExtensions, CsvWriter, WorkbookReader, FormulaGuard

### Community 285 - "Common.Domain.Events"
Cohesion: 0.07
Nodes (14): Common.Domain.Events, Products.Domain.Stores.DomainEvents.v2, Inventory.Domain.WarehouseConnections.DomainEvents.v1, Products.Domain.Stores.DomainEvents.v1, Inventory.Domain.StockLevels.DomainEvents.v1, IEventHandler, IEventHandlerWrapper, V1StockLevelCreatedDomainEvent (+6 more)

### Community 323 - "microsoft_aspnetcore_mvc"
Cohesion: 0.03
Nodes (62): Products.Endpoints.Products.v1.GetImport, Products.Endpoints.Stores.v1.Suspend, Inventory.Endpoints.StockLevels.v1.GetBulkAdjustment, Products.Endpoints.Stores.v1.Deactivate, Products.Endpoints.Products.v1.DownloadExport, Products.Endpoints.Stores.v1.My.RemoveProduct, Inventory.Endpoints.WarehouseConnections.v1.Get, Products.Endpoints.Stores.v1.Reinstate (+54 more)

## Knowledge Gaps
- **1299 isolated node(s):** `UserId`, `Id`, `IdAsString`, `Roles`, `SessionId` (+1294 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1718 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **113 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `microsoft_extensions_logging` to `IServiceCollection`, `ObjectStorageOptions`, `PushOptions`, `ProductImportJobRunner.cs`, `HttpContext`, `Common.Domain.ResultMonad`, `EmailOptions`, `OtpOptions`, `OutboxOptions.cs`, `ReverseProxyOptions`, `FullTextSearchOptions`, `microsoft_extensions_dependencyinjection`, `OpenApiOptions`, `InventoryOptions`, `CachingOptions.cs`, `BackgroundJobsOptions`, `fluentvalidation`, `microsoft_entityframeworkcore`, `ObservabilityOptions`, `WarehouseConnections/v1/Create/Request.cs`, `system_diagnostics`, `CorsOptions`, `CaptchaOptions`, `RabbitMqOptions`, `SmsOptions`, `Setup.Logger.cs`, `Host.Swagger`, `microsoft_aspnetcore_mvc`, `Common.InterModuleRequests.Products`, `KeycloakOptions`, `CachingOptions`, `RequestLoggingOptions`, `fluentvalidation`, `AuditLogOptions`, `Response`, `ResiliencyOptions`, `Common.Domain.StronglyTypedIds`, `Common.InterModuleRequests.Contracts`, `ResxLocalizationOptions`, `InterModuleRequestOptions`, `ProjectionReconciliationOptions`, `DevicesOptions`, `system_globalization`, `SignalROptions`, `Response`, `ProductsOptions`?**
  _High betweenness centrality (0.057) - this node is a cross-community bridge._
- **What connects `UserId`, `Id`, `IdAsString` to the rest of the system?**
  _1299 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `StockMovement` be split into smaller, more focused modules?**
  _Cohesion score 0.12105263157894737 - nodes in this community are weakly interconnected._
- **Why does `Common.Domain.ResultMonad` connect `Common.Domain.ResultMonad` to `microsoft_extensions_logging`, `IOptions`, `microsoft_entityframeworkcore`, `Common.InterModuleRequests.Products`, `EmailOtpDispatchOutcome`, `Common.Domain.StronglyTypedIds`, `Common.InterModuleRequests.Contracts`, `Result`, `system_diagnostics`, `FirebasePushGateway`, `ProductImportJobRunner.cs`, `microsoft_extensions_dependencyinjection`, `system_globalization`, `.ReadRows`, `SmsCategory`, `Error`?**
  _High betweenness centrality (0.045) - this node is a cross-community bridge._
- **Should `DateTimeOffset` be split into smaller, more focused modules?**
  _Cohesion score 0.07407407407407407 - nodes in this community are weakly interconnected._
- **Why does `Result` connect `Result` to `.DownloadExportAsync`, `DateTimeOffset`, `List`, `DomainEvent`, `IInterModuleRequestClient`, `NetGsmSmsGateway`, `StockLevel`, `ResultTelemetryExtensions`, `Error`, `IOptions`, `.SaveBatchAsync`, `IProductsDbContext`, `IInventoryDbContext`, `Response`, `Product`, `IInventoryDbContext`, `ReservationStatus`, `RowParser.Parse`, `BrevoEmailGateway`, `AuditableEntity`, `Endpoint.AddProductAsync`, `.CreateAsync`, `PackageDimensions`, `EmailOtpDispatchOutcome`, `ReCaptchaService`, `.SearchAsync`, `Task`, `StockBulkAdjustJob`, `VerifyPhoneOtpResponse`, `WarehouseHttpGateway`?**
  _High betweenness centrality (0.040) - this node is a cross-community bridge._
- **Should `microsoft_extensions_logging` be split into smaller, more focused modules?**
  _Cohesion score 0.051874366767983786 - nodes in this community are weakly interconnected._