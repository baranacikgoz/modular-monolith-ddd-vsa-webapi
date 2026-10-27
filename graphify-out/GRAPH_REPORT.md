# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-10-27)

## Corpus Check
- 882 files · ~183,447 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 5951 nodes · 17837 edges · 359 communities (235 shown, 124 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 815 edges (avg confidence: 0.87)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `6a9a78a8`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- StockLevelId
- StockReservation
- RequestBodyLimitMetadata
- Products.Endpoints
- List
- DateTimeOffset
- ApplyAuditingInterceptor
- TimeProvider
- Common.Infrastructure.Extensions
- Result
- Common.Domain.ResultMonad
- KeycloakPermissionAuthorizationHandler.cs
- Result
- Response
- CurrentUser
- EmailOptions
- IAutoMigrateMarker.cs
- Response
- Common.Application.Options
- FullTextSearchOptions
- BoundedRequestCaptureStream
- DeviceRegistration
- .GetProductAsync
- IInterModuleRequestClient
- InventoryOptions
- Response
- CustomRateLimitingOptions
- Stores/v1/My/Update/Request.cs
- BackgroundJobsModule
- Error
- InventoryDbContext
- IReadOnlyCollection
- Response
- Response
- OutboxMessage
- CustomRateLimitingOptions
- AdminRepresentations.cs
- microsoft_entityframeworkcore
- ObservabilityOptions
- .WriteTooManyRequestsToResponse
- OpenApiOptions
- BoundedCaptureStream
- V1StockQuantityChangedDomainEvent
- ProductImportJobRunner
- OutboxProcessor
- StoreAccessReconciliationJob
- Outbox Misuse Check
- Request
- Add Integration Event Command
- host
- Response
- CaptchaOptions
- KeyedResiliencePipelines
- RedisFixedWindowRateLimiter
- .ImportBatchAsync
- FirebasePushGateway
- Cross-Module Reference Violation
- OutboxOptions
- Request
- Bogus Test Data
- BrevoEmailGateway
- ProductSummariesReconciliationJob
- S3ObjectStoreCore
- Host.Swagger
- RequestBody
- SearchLanguageResolver
- PackageDimensions
- .AddWarehouseGatewayInfrastructure
- DomainEvent
- Getting started
- AuditableEntity
- FormApiWarehouseGateway
- AsNoTracking Coverage Check
- IIntegrationEventOutbox
- InventoryDbContext
- ReplaceCredentials/Request.cs
- KeycloakOptions
- Response
- IReadOnlyList
- Request
- IntegrationEventHandlerBase
- common_application_localization_resources
- EntityTypeBuilder
- RequestResponseBodyLoggingMiddleware
- NotificationsTelemetry
- RequestLoggingOptions
- Configuration-Driven Module Registration
- Response
- UserRepresentation
- ProductsDbContext
- AlertRecipientsOptions
- DatabaseSeederOrchestrator
- SecretProtector
- NotifyRoleOnCriticalAlertRaisedHandler
- ILogger
- StockBulkAdjustRowError
- Response
- AggregateRoot
- Task
- ResiliencyOptions
- ISmsGateway
- Common.Application.Storage
- DefaultIdType
- Response
- Common.InterModuleRequests.Contracts
- WarehouseConnectionResponse
- Products/v1/My/Update/Request.cs
- Response
- Response
- WarehouseHttpGateway
- OtpOptions
- CustomValidator
- Response
- InterModuleRequestOptions
- WebhookDelivery
- ApplicationUserId
- DefaultResponsesOperationFilter
- depends_on
- healthcheck
- mm.keycloak
- mm.redis
- Response
- system_diagnostics
- ProductsOptions
- Pagination and search
- WarehouseConnectionId
- JobStatus
- system_text
- docker-compose.yml (Base Stack)
- IServiceCollection
- ObjectStorageOptions
- IServiceProvider
- NotificationPayload
- .GetClientKey
- ValueTask
- services
- mm.postgres
- AuditableEntityResponse
- JsonSerializerOptions
- mm.rabbitmq
- Result
- Persistence conventions
- AuditableEntityConfiguration
- Response
- KeycloakPermissionClient
- Response
- SkipOverlappingRecurringJobFilter
- V1ProductAddedToStoreDomainEvent
- Response
- README (Boilerplate Overview)
- NetGsmSmsGateway
- KeycloakPermissionAuthorizationHandler
- DateOnly
- SendRequestBody
- Request
- Adjust/Request.cs
- StatusSnapshot
- BackgroundJobsOptions
- Response
- Request
- ActivitySource
- Request
- fluentvalidation
- ResultTelemetryExtensions
- .PageMovementsAsync
- .ToFields
- Response
- Notifications.Application/IAssemblyReference.cs
- AuditLogEntryConfiguration
- Product
- Response
- CachingOptions.cs
- AlertRecipientsReconciliationJob
- TimeProvider
- CorsOptions
- WebhookDelivery
- HttpClient
- SmsOptions
- RabbitMqOptions
- ConfigureSwaggerOptions
- DomainEventConverter
- ChunkEncodingFreeS3Client
- SecretProtectionOptions
- Uri
- SignalROptions
- Request
- StoreId
- AI-assisted development workflow
- Response
- GetDeviceSessionsRequest
- Response
- Keycloak realm as code
- Response
- Response
- StockPushDirtyItem
- StronglyTypedIdBinder.cs
- ProductTemplate
- EmailOtpDispatchOutcome
- KeycloakScopes
- ProjectionUpsertOutcome
- Response
- Response
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- Response
- JwtBearerConfigureOptions
- ProviderStatusSnapshot
- Common.Domain.ResultMonad
- SmsOtpDispatchOutcome
- Response
- IInventoryDbContext
- FeatureFlags
- Request
- Response
- SendForEmail/Request.cs
- ReleaseReconcileOutcome
- StockBulkAdjustJobRunner
- Response
- ResultToResponseTransformer.cs
- ProcessedMessage
- Split-Deployment PoC
- AuditLogEntry
- GetImport/Request.cs
- Suspend/Request.cs
- .AdjustBatchAsync
- Key decisions
- Setup
- WarehouseConnections/v1/Deactivate/Request.cs
- AuditLogOptions
- .GetMeAsync
- How it works
- Response
- Response
- RequestBody
- ConstraintViolationExtensions
- ResxLocalizationOptions
- Response
- ConcurrencyConfiguratorExtensions
- StronglyTypedIdSchemaFilter.cs
- DevicesOptions
- Common.Domain.StronglyTypedIds
- Setup
- IAM.Domain
- Request
- Request
- Response
- VersionNeutral/Get/Request.cs
- Products.Domain/IAssemblyReference.cs
- IAM.Application/IAssemblyReference.cs
- Products.Infrastructure/IAssemblyReference.cs
- Products.Application/IAssemblyReference.cs
- Common.Infrastructure/IAssemblyReference.cs
- Notifications.Domain/IAssemblyReference.cs
- IAM.Infrastructure/IAssemblyReference.cs
- CachingEntryDefaults
- StockLevelErrors.cs
- Inventory.Endpoints
- Infrastructure/StringExtensions.cs
- IAM.Endpoints
- Inventory.Application/IAssemblyReference.cs
- Object storage
- Inventory.Infrastructure/IAssemblyReference.cs
- Inventory.Domain/IAssemblyReference.cs
- RouteGroupBuilder
- Notifications.Infrastructure
- .LocalizeFromError
- IResult
- HealthCheckOptions
- ReverseProxyOptions
- VersionNeutral/Search/Request.cs
- Request
- Release/Request.cs
- GetExport/Request.cs
- WarehouseAddressRefusedException
- ProblemDetailsExtensions.cs
- ProjectionReconciliationOptions
- VerifyEmail/Request.cs
- Request
- .ReadRows
- Common.Domain.Events
- Request
- AddImage/Request.cs
- DatabaseOptions.cs
- SecurityHeadersOptions.cs
- ValidationContextExtensions
- ProviderSnapshot
- SendResponseBody
- Products/v1/Get/Request.cs
- Stores/v1/Get/Request.cs
- microsoft_aspnetcore_mvc
- Request
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
1. `Result` - 310 edges
2. `Common.Application.Options` - 205 edges
3. `Common.Domain.ResultMonad` - 181 edges
4. `ApplicationUserId` - 123 edges
5. `CustomValidator` - 122 edges
6. `Common.Application.Auth` - 113 edges
7. `IInventoryDbContext` - 108 edges
8. `Common.Application.Extensions` - 107 edges
9. `Common.Application.Validation` - 105 edges
10. `Common.Domain.StronglyTypedIds` - 105 edges

## Surprising Connections (you probably didn't know these)
- `Authorization` --references--> `ICurrentUser`  [INFERRED]
  docs/platform.md → src/Common/Common.Application/Auth/ICurrentUser.cs
- `Queued work: accept, queue, poll` --references--> `ObjectStorageOptions`  [INFERRED]
  docs/getting-started.md → src/Common/Common.Application/Options/ObjectStorageOptions.cs
- `The commands` --references--> `IModule`  [INFERRED]
  docs/ai-workflow.md → src/Common/Common.Infrastructure/Modules/IModule.cs
- `Watch two modules cooperate` --references--> `ProductCreatedIntegrationEvent`  [INFERRED]
  docs/getting-started.md → src/Common/Common.IntegrationEvents/Products.cs
- `Observability` --references--> `RequestResponseBodyLoggingMiddleware`  [INFERRED]
  docs/platform.md → src/Host/Host/Middlewares/RequestResponseBodyLoggingMiddleware.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Local Infrastructure Stack** — docker_compose_postgres, docker_compose_rabbitmq, docker_compose_redis, docker_compose_aspire_dashboard [EXTRACTED 1.00]

## Communities (359 total, 124 thin omitted)

### Community 0 - "StockLevelId"
Cohesion: 0.08
Nodes (21): StockLevel, LastMovementSequence, LowStockThreshold, Movements, ProductId, QuantityOnHand, StockMovement, Quantity (+13 more)

### Community 1 - "StockReservation"
Cohesion: 0.09
Nodes (24): V1StockReservationReadOnlyResolvedDomainEvent, V1StockReservationReadOnlyResolvedDomainEventExtensions, V1StockReservationReleasedDomainEvent, ReservationStatus, Active, Committed, Expired, Released (+16 more)

### Community 2 - "RequestBodyLimitMetadata"
Cohesion: 0.14
Nodes (6): RequestBodyLimitEndpointExtensions, RequestBodyLimitMetadata, MaxBodyBytes, RequestBodyLimitMiddleware, EnrichLogsWithUserInfoMiddleware, SecurityHeadersMiddleware

### Community 6 - "DateTimeOffset"
Cohesion: 0.05
Nodes (19): IdBound, Value, JobClaimExtensions, DbContextExtensions, ServiceAccountTokenCache, AccessToken, ExpiresAt, V1StockReservationProviderEventTimeRecordedDomainEvent (+11 more)

### Community 7 - "ApplyAuditingInterceptor"
Cohesion: 0.31
Nodes (3): ApplyAuditingInterceptor, ApplySearchLanguageInterceptor, Setup

### Community 8 - "TimeProvider"
Cohesion: 0.07
Nodes (35): BackgroundJobs, How modules fit together, Modules, Products, Job rows, IBackgroundUserContext, UserId, JobHousekeepingJob (+27 more)

### Community 9 - "Common.Infrastructure.Extensions"
Cohesion: 0.05
Nodes (19): Common.Infrastructure.Modules, Common.Endpoints.Versioning, Products.Endpoints.Probe, Common.Infrastructure.RateLimiting, Products.Infrastructure.RateLimiting, Products.Endpoints.Products, Common.Infrastructure.Extensions, IAM.Endpoints.Otp.VersionNeutral (+11 more)

### Community 10 - "Result"
Cohesion: 0.05
Nodes (8): IBackgroundJobs, FeatureFlagResultExtensions, ProjectionUpsertExtensions, AsyncExtensions, BackgroundJobsService, RecurringBackgroundJobsService, HttpContentJsonExtensions, ProductsSearchCacheExtensions

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.06
Nodes (40): Inventory.Application.StockLevels, IAM.Endpoints.Captcha.VersionNeutral, Products.Infrastructure.Persistence, Inventory.Application.StockReservations, Products.Infrastructure.InterModuleRequestHandlers, Common.Application.Search, Products.Application.Exports, Products.Endpoints.Stores (+32 more)

### Community 12 - "KeycloakPermissionAuthorizationHandler.cs"
Cohesion: 0.14
Nodes (3): IAM.Infrastructure.Auth, Notifications.Infrastructure.Hubs, Notifications.Application.Hubs

### Community 13 - "Result"
Cohesion: 0.06
Nodes (13): Result, Error, IsFailure, Success, SyncExtensions, VerifyEmailOtpResponseExtensions, VerifyPhoneOtpResponseExtensions, Endpoint (+5 more)

### Community 14 - "Response"
Cohesion: 0.13
Nodes (13): IAM.Endpoints.Users.VersionNeutral.Me.Get, Endpoint, Response, BirthDate, CreatedOn, Email, FirstName, Id (+5 more)

### Community 15 - "CurrentUser"
Cohesion: 0.13
Nodes (10): CurrentUser, BackgroundUserIdAsString, Id, IdAsString, IsAuthenticated, Principal, Roles, SessionId (+2 more)

### Community 16 - "EmailOptions"
Cohesion: 0.10
Nodes (23): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+15 more)

### Community 18 - "Response"
Cohesion: 0.14
Nodes (12): Response, ActingUserId, ActingUserName, CreatedOn, Id, Quantity, QuantityOnHandAfter, Reason (+4 more)

### Community 19 - "Common.Application.Options"
Cohesion: 0.04
Nodes (42): Common.Infrastructure.Persistence, Notifications.Application.Sms, Notifications.Infrastructure.Devices, Inventory.Infrastructure.IntegrationEventHandlers, Common.Application.Persistence.Inbox, Notifications.Infrastructure.Email, Inventory.Application.Persistence, Inventory.Application.IntegrationEventHandlers (+34 more)

### Community 20 - "FullTextSearchOptions"
Cohesion: 0.12
Nodes (19): Add a new language/culture, Add search to a new entity _(Build checklist)_, Change the vector (weights, fields, or config), Configuration, Extending and maintaining, Full-Text Search, Gotchas, Non-goals (+11 more)

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.13
Nodes (6): BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 22 - "DeviceRegistration"
Cohesion: 0.12
Nodes (12): DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive, LastReconciledOn, PushToken, PushTokenUpdatedOn (+4 more)

### Community 23 - ".GetProductAsync"
Cohesion: 0.54
Nodes (4): GetStockLevelRequest, GetStockLevelResponse, GetStockLevelRequestHandler, Endpoint

### Community 24 - "IInterModuleRequestClient"
Cohesion: 0.04
Nodes (51): OtpCacheEntry, IInterModuleRequest, IInterModuleRequestClient, IInterModuleRequestHandler, InterModuleRequestHandler, DeprovisionUserRequest, DeprovisionUserResponse, BindDeviceSessionRequest (+43 more)

### Community 25 - "InventoryOptions"
Cohesion: 0.05
Nodes (36): InventoryOptions, AllowPrivateWarehouseAddresses, BulkAdjustBatchesPerMessage, BulkAdjustJobConcurrentMessageLimit, BulkAdjustMaxStoredRowErrors, BusinessTimeZoneId, DailyTotalsMaxRangeDays, MaxOccurrencesPerSeries (+28 more)

### Community 26 - "Response"
Cohesion: 0.29
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, Endpoint, Response, EmailVerificationToken, IsRegistered

### Community 27 - "CustomRateLimitingOptions"
Cohesion: 0.07
Nodes (22): CustomRateLimitingOptions, CheckRegistration, CreateStore, Email, ExemptPathPrefixes, Global, OtpVerify, Register (+14 more)

### Community 28 - "Stores/v1/My/Update/Request.cs"
Cohesion: 0.40
Nodes (5): Request, Address, Description, Name, RequestValidator

### Community 29 - "BackgroundJobsModule"
Cohesion: 0.13
Nodes (9): Cross-process call path, How it works, ICoreModule, BackgroundJobsModule, ActivitySourceNames, MeterNames, Name, StartupPriority (+1 more)

### Community 30 - "Error"
Cohesion: 0.07
Nodes (24): Error, Key, ParameterName, StatusCode, SubErrors, Value, CaptchaErrors, IdentityErrors (+16 more)

### Community 31 - "InventoryDbContext"
Cohesion: 0.13
Nodes (12): ProjectionEntity, SourceVersion, ProductSummary, Name, ProductId, StoreId, StoreAccess, IsActive (+4 more)

### Community 33 - "Response"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.Refresh, Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 34 - "Response"
Cohesion: 0.09
Nodes (20): StoreStatus, Active, Inactive, PendingApproval, Suspended, Endpoint, Response, Address (+12 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.07
Nodes (24): IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, OutboxMessage, CreatedOn (+16 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.05
Nodes (32): Adding a module or a feature, CollectionExtensions, IModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority (+24 more)

### Community 37 - "AdminRepresentations.cs"
Cohesion: 0.10
Nodes (19): ErrorRepresentation, Error, ErrorDescription, ErrorMessage, Field, GroupRepresentation, Id, RoleRepresentation (+11 more)

### Community 38 - "microsoft_entityframeworkcore"
Cohesion: 0.07
Nodes (15): Common.Infrastructure.Persistence.Inbox, Products.Infrastructure.Persistence.Seeding, Common.Application.Persistence, Products.Endpoints.Products.v1.Import, Inventory.Infrastructure.Persistence.EntityConfigurations, Notifications.Domain.Devices, Outbox.Persistence, Common.Application.Persistence.Outbox (+7 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.08
Nodes (17): ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics, EnableTracing, LogSink, MinimumLevel (+9 more)

### Community 41 - "OpenApiOptions"
Cohesion: 0.22
Nodes (9): OpenApiOptions, ContactEmail, ContactName, Description, EnableSwagger, LicenseName, LicenseUrl, Title (+1 more)

### Community 42 - "BoundedCaptureStream"
Cohesion: 0.15
Nodes (6): BoundedCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 43 - "V1StockQuantityChangedDomainEvent"
Cohesion: 0.11
Nodes (11): Inventory.Application.StockLevels.DomainEventHandlers.v1, Inventory.Domain.StockLevels.DomainEvents.v1, V1StockLowThresholdCrossedDomainEventHandlers, V1StockLowThresholdRecoveredDomainEventHandlers, V1StockLowThresholdChangedDomainEvent, MovementTypeSnapshot, Adjusted, Received (+3 more)

### Community 44 - "ProductImportJobRunner"
Cohesion: 0.12
Nodes (14): Authorization, Caching, Observability, Options and configuration, Platform building blocks, Results and errors, ProductImportRequestedIntegrationEvent, ProductImportJobId (+6 more)

### Community 45 - "OutboxProcessor"
Cohesion: 0.11
Nodes (11): Outbox, InboxCleanupJob, OutboxCleanupJob, OutboxMetricsJob, OutboxProcessor, PublishOutcome, Failed, Published (+3 more)

### Community 46 - "StoreAccessReconciliationJob"
Cohesion: 0.14
Nodes (9): ProjectionReconciliationJob, GetStoresPageRequest, GetStoresPageResponse, StorePageItem, Setup, StoreAccessReconciliationCursor, StoreAccessReconciliationJob, StoreAccessReconciliationJobRegistrar (+1 more)

### Community 48 - "Request"
Cohesion: 0.10
Nodes (14): Webhooks, ProviderStatusMap, ProviderStatusResolution, Mapped, Unrecognized, ReservationStatusTransitions, Request, DeliveryId (+6 more)

### Community 50 - "host"
Cohesion: 0.18
Nodes (11): context, dockerfile, build, container_name, environment, image, networks, ports (+3 more)

### Community 51 - "Response"
Cohesion: 0.14
Nodes (12): Endpoint, Response, FailureKey, FileName, FinishedOn, JobId, ProductsWritten, QueuedOn (+4 more)

### Community 52 - "CaptchaOptions"
Cohesion: 0.16
Nodes (13): CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey, Provider, ScoreThreshold, SecretKey (+5 more)

### Community 53 - "KeyedResiliencePipelines"
Cohesion: 0.17
Nodes (3): KeyedResiliencePipelines, WarehouseGatewayFactory, WarehouseResiliencePipelines

### Community 54 - "RedisFixedWindowRateLimiter"
Cohesion: 0.17
Nodes (6): FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration, RetryAfterHeaderExtensions

### Community 55 - ".ImportBatchAsync"
Cohesion: 0.19
Nodes (8): ProductImportRowError, BatchSummary, RowResult, Created, Unchanged, Updated, RowParser, SheetRow

### Community 56 - "FirebasePushGateway"
Cohesion: 0.25
Nodes (3): IPushGateway, FirebasePushGateway, Setup

### Community 58 - "OutboxOptions"
Cohesion: 0.11
Nodes (21): OutboxCleanupSettings, BatchSize, CronSchedule, Enabled, RetentionDays, OutboxCleanupSettingsValidator, OutboxOptions, BaseBackoffSeconds (+13 more)

### Community 59 - "Request"
Cohesion: 0.15
Nodes (13): Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken (+5 more)

### Community 61 - "BrevoEmailGateway"
Cohesion: 0.12
Nodes (13): IEmailGateway, BrevoEmailGateway, SendContact, Email, Name, SendErrorBody, Code, Message (+5 more)

### Community 63 - "ProductSummariesReconciliationJob"
Cohesion: 0.20
Nodes (8): GetProductsPageRequest, GetProductsPageResponse, ProductPageItem, ProductSummariesReconciliationCursor, ProductSummariesReconciliationJob, ProductSummariesReconciliationJobRegistrar, Setup, GetProductsPageRequestHandler

### Community 64 - "S3ObjectStoreCore"
Cohesion: 0.07
Nodes (9): IPublicObjectStore, ObjectListItem, ObjectListPage, ObjectMetadata, UploadObjectRequest, S3ObjectStoreCore, S3PublicObjectStore, Endpoint (+1 more)

### Community 65 - "Host.Swagger"
Cohesion: 0.18
Nodes (4): Host, Host.Swagger, Program, Setup

### Community 66 - "RequestBody"
Cohesion: 0.18
Nodes (10): RequestBody, FirstDeadline, IntervalDays, OccurrenceCount, ProductId, QuantityPerOccurrence, Warehouses, WarehouseAllocation (+2 more)

### Community 67 - "SearchLanguageResolver"
Cohesion: 0.33
Nodes (3): SearchLanguageResolver, UniversalConfig, Setup

### Community 68 - "PackageDimensions"
Cohesion: 0.10
Nodes (11): Common.Domain, ValueObject, DimensionsSnapshot, V1ProductPackageDimensionsChangedDomainEvent, V1ProductPackageDimensionsChangedDomainEventExtensions, PackageDimensions, HeightCm, LengthCm (+3 more)

### Community 69 - ".AddWarehouseGatewayInfrastructure"
Cohesion: 0.20
Nodes (5): WarehouseAddressPolicy, Setup, IWarehouseHostResolver, SystemWarehouseHostResolver, WarehouseConnectGuard

### Community 70 - "DomainEvent"
Cohesion: 0.06
Nodes (24): Products.Domain.Products.DomainEvents.v1, Products.Application.Products.DomainEventHandlers.v1, DomainEvent, CreatedOn, Id, Version, UnknownDomainEvent, V1ProductImageRemovedDomainEventHandlers (+16 more)

### Community 71 - "Getting started"
Cohesion: 0.12
Nodes (17): Configuration, Connect a store to a warehouse system, Errors, Getting started, Prerequisites, Queued work: accept, queue, poll, Run the API from the command line instead, Run the load test (+9 more)

### Community 72 - "AuditableEntity"
Cohesion: 0.13
Nodes (14): AuditableEntity, AuditableEntity, Id, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, Version (+6 more)

### Community 73 - "FormApiWarehouseGateway"
Cohesion: 0.19
Nodes (9): FormApiWarehouseGateway, HoldEnvelope, HoldResult, ErrorResponseBody, JsonApiWarehouseGateway, PushStockItem, PushStockRequestBody, ReleaseRequestBody (+1 more)

### Community 75 - "IIntegrationEventOutbox"
Cohesion: 0.06
Nodes (37): DomainEventHandlerBase, IEventHandler, IEventHandlerWrapper, IIntegrationEventOutbox, StoreAccessChangedIntegrationEvent, StockLowThresholdCrossedIntegrationEventPublishingHandler, StockLowThresholdRecoveredIntegrationEventPublishingHandler, StockReservationCommittedIntegrationEventPublishingHandler (+29 more)

### Community 76 - "InventoryDbContext"
Cohesion: 0.15
Nodes (12): InventoryDbContext, ProductSummaries, StockBulkAdjustJobs, StockLevels, StockMovements, StockPushDirtyItems, StockReservations, StoreAccess (+4 more)

### Community 77 - "ReplaceCredentials/Request.cs"
Cohesion: 0.28
Nodes (8): Inventory.Endpoints.WarehouseConnections.v1.ReplaceCredentials, Request, Body, Id, RequestBody, ApiCredential, RequestBodyValidator, RequestValidator

### Community 78 - "KeycloakOptions"
Cohesion: 0.11
Nodes (19): KeycloakOptions, AttemptTimeoutSeconds, Authority, BaseUrl, DecisionCacheMaxDurationSeconds, Realm, RequireHttpsMetadata, ResourceClientId (+11 more)

### Community 79 - "Response"
Cohesion: 0.14
Nodes (12): IAM.Endpoints.Users.VersionNeutral.Search, Endpoint, Response, BirthDate, CreatedOn, Email, Enabled, FirstName (+4 more)

### Community 80 - "IReadOnlyList"
Cohesion: 0.14
Nodes (10): GetActiveSessionIdsRequest, GetActiveSessionIdsResponse, UserSessionIds, GrantedPermission, KeycloakUserPage, KeycloakUserSession, GetActiveSessionIdsRequestHandler, RoleMembersSnapshot (+2 more)

### Community 81 - "Request"
Cohesion: 0.18
Nodes (11): Request, Email, EmailVerificationToken, FirstName, LastName, Password, PhoneNumber, StoreAddress (+3 more)

### Community 82 - "IntegrationEventHandlerBase"
Cohesion: 0.07
Nodes (41): Projections and reconciliation, IntegrationEventHandlerBase, MaxEventAge, CachingOptions, AllowInMemoryOnlyInProduction, EntryDefaults, IdempotencyKeyDuration, IdempotencyL1Duration (+33 more)

### Community 83 - "common_application_localization_resources"
Cohesion: 0.03
Nodes (78): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Products.v1.ListImports, Inventory.Endpoints.StockReservations.v1.My.Search, Products.Endpoints.Stores.v1.My.AuditLog, Inventory.Endpoints.WarehouseConnections.v1.Search, Inventory.Endpoints.StockLevels.v1.ListBulkAdjustments, Inventory.Endpoints.WarehouseConnections.v1.My.Search, Products.Endpoints.Products.v1.AuditLog (+70 more)

### Community 84 - "EntityTypeBuilder"
Cohesion: 0.18
Nodes (5): JobRowConfiguration, StronglyTypedIdValueConverter, StockBulkAdjustJobConfiguration, ProductExportJobConfiguration, ProductImportJobConfiguration

### Community 85 - "RequestResponseBodyLoggingMiddleware"
Cohesion: 0.22
Nodes (4): SensitivePathRule, Methods, Path, RequestResponseBodyLoggingMiddleware

### Community 87 - "RequestLoggingOptions"
Cohesion: 0.16
Nodes (12): RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes, SensitiveQueryParamPaths (+4 more)

### Community 89 - "Response"
Cohesion: 0.22
Nodes (8): Endpoint, Response, Address, Description, Name, OwnerId, ProductCount, Status

### Community 90 - "UserRepresentation"
Cohesion: 0.13
Nodes (15): CredentialRepresentation, Temporary, Type, Value, UserRepresentation, Attributes, CreatedTimestamp, Credentials (+7 more)

### Community 91 - "ProductsDbContext"
Cohesion: 0.10
Nodes (18): IDatabaseSeeder, Priority, BaseDbContext, AuditLog, INotificationsDbContext, AlertRecipients, DeviceRegistrations, NotificationsDbContext (+10 more)

### Community 92 - "AlertRecipientsOptions"
Cohesion: 0.29
Nodes (7): AlertRecipientsOptions, EmailHtmlBodyTemplate, EmailSubjectTemplate, EmailTextBodyTemplate, ReconcileCron, RoleName, AlertRecipientsOptionsValidator

### Community 93 - "DatabaseSeederOrchestrator"
Cohesion: 0.20
Nodes (3): DatabaseSeederOrchestrator, SeedingCompletionTracker, Setup

### Community 94 - "SecretProtector"
Cohesion: 0.08
Nodes (20): Secret protection, SecretGenerator, SecretProtector, Setup, ProvisionUserFailureReason, EmailAlreadyRegistered, IdentityProviderUnavailable, None (+12 more)

### Community 96 - "ILogger"
Cohesion: 0.04
Nodes (14): AuditLogRetentionService, MigrationGuard, OutboxSaveHelper, GlobalExceptionHandlingMiddleware, LoginCompletion, ReCaptchaService, RegistrationCompletion, NotificationsHub (+6 more)

### Community 97 - "StockBulkAdjustRowError"
Cohesion: 0.36
Nodes (4): StockBulkAdjustRowError, BatchSummary, RowResult, Adjusted

### Community 98 - "Response"
Cohesion: 0.11
Nodes (16): Endpoint, Response, FailureKey, FileName, FinishedOn, JobId, ProcessedRows, ProductsCreated (+8 more)

### Community 99 - "AggregateRoot"
Cohesion: 0.13
Nodes (13): AggregateRoot, Events, Id, Version, IAggregateRoot, Events, Id, Version (+5 more)

### Community 100 - "Task"
Cohesion: 0.03
Nodes (32): AuditLogDto, ICurrentUser, Id, IdAsString, Roles, SessionId, IVariantFeatureManagerExtensions, PaginationResponse (+24 more)

### Community 101 - "ResiliencyOptions"
Cohesion: 0.09
Nodes (23): KeyedResilienceProfile, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, RateLimitFailOpen, RateLimitPermits (+15 more)

### Community 102 - "ISmsGateway"
Cohesion: 0.19
Nodes (8): ISmsGateway, SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, DummySmsGateway, Setup, ThrottledSmsGateway

### Community 104 - "DefaultIdType"
Cohesion: 0.06
Nodes (32): IAM, Inventory, Notifications, Transactional outbox and inbox, StatelessInboxStore, Instance, IntegrationEventOutbox, HasPending (+24 more)

### Community 105 - "Response"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegisterByEmail, Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 106 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.05
Nodes (29): Notifications.Application.Otp, Common.Endpoints, IAM.Infrastructure.Keycloak.Representations, Common.Application.Caching, Common.Application.FeatureManagement, IAM.Endpoints.Otp, Notifications.Application.Persistence, Notifications.Infrastructure.Telemetry (+21 more)

### Community 107 - "WarehouseConnectionResponse"
Cohesion: 0.13
Nodes (11): WarehouseConnectionStatus, Active, Deactivated, Endpoint, Endpoint, Endpoint, WarehouseConnectionResponse, BaseUrl (+3 more)

### Community 108 - "Products/v1/My/Update/Request.cs"
Cohesion: 0.20
Nodes (11): Products.Endpoints.Products.v1.My.Update, Request, Body, Id, RequestBody, Description, Name, Price (+3 more)

### Community 109 - "Response"
Cohesion: 0.33
Nodes (4): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, Endpoint, Response, IsRegistered

### Community 110 - "Response"
Cohesion: 0.29
Nodes (6): Endpoint, Response, Description, Name, Price, Quantity

### Community 112 - "OtpOptions"
Cohesion: 0.14
Nodes (12): OtpOptions, DummyCode, EmailQuotaWindowMinutes, ExpirationInMinutes, Length, MaxSendsPerEmailPerWindow, MaxSendsPerPhonePerWindow, PhoneQuotaWindowMinutes (+4 more)

### Community 113 - "CustomValidator"
Cohesion: 0.04
Nodes (69): Products.Endpoints.Products.v1.My.SetAttributes, Inventory.Endpoints.StockReservations.v1.Commit, Inventory.Endpoints.StockReservations.v1.ResolveReadOnly, Inventory.Endpoints.StockLevels.v1.Receive, Products.Endpoints.Stores.v1.Update, Inventory.Endpoints.StockLevels.v1.ChangeLowStockThreshold, JobHousekeepingOptionsValidator, CustomValidator (+61 more)

### Community 114 - "Response"
Cohesion: 0.14
Nodes (12): IAM.Endpoints.Users.VersionNeutral.Get, Endpoint, Response, BirthDate, CreatedOn, Email, Enabled, FirstName (+4 more)

### Community 115 - "InterModuleRequestOptions"
Cohesion: 0.15
Nodes (10): Inter-module requests, InterModuleRequestOptions, DependencyUnavailableRetryAfterSeconds, HandlerConcurrentMessageLimit, HandlerPrefetchCount, Timeouts, TimeoutSeconds, InterModuleRequestOptionsValidator (+2 more)

### Community 117 - "ApplicationUserId"
Cohesion: 0.11
Nodes (8): CacheKeys, For, ApplicationUserId, IsEmpty, Value, BackgroundUserContext, UserId, KeycloakUser

### Community 118 - "DefaultResponsesOperationFilter"
Cohesion: 0.29
Nodes (3): DefaultResponsesOperationFilter, RemoveDefaultResponseSchemaFilter, SwaggerDefaultValues

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
Nodes (14): IAM.Endpoints.Tokens.VersionNeutral.Create, IdentityScheme, Email, PhoneNumber, IdentitySchemeOptions, Scheme, IdentitySchemeOptionsValidator, Setup (+6 more)

### Community 124 - "system_diagnostics"
Cohesion: 0.09
Nodes (6): Notifications.Application.Push, Notifications.Infrastructure.Push, BackgroundJobs.Telemetry, Notifications.Infrastructure.Push.Firebase, LoginMethods, SessionRevokedReasons

### Community 125 - "ProductsOptions"
Cohesion: 0.13
Nodes (15): ProductsOptions, ExportJobConcurrentMessageLimit, ExportMaxRows, ExportPageSize, ImageMaxFileBytes, ImportBatchesPerMessage, ImportJobConcurrentMessageLimit, ImportMaxStoredRowErrors (+7 more)

### Community 126 - "Pagination and search"
Cohesion: 0.36
Nodes (3): Pagination and search, LikePattern, TrigramIndexExtensions

### Community 127 - "WarehouseConnectionId"
Cohesion: 0.08
Nodes (17): Inventory.Domain.WarehouseConnections.DomainEvents.v1, V1WarehouseConnectionCreatedDomainEventExtensions, V1WarehouseConnectionCredentialsReplacedDomainEvent, V1WarehouseConnectionDeactivatedDomainEvent, V1WarehouseConnectionSecretsReprotectedDomainEvent, V1WarehouseConnectionWebhookSecretRotatedDomainEvent, WarehouseConnection, BaseUrl (+9 more)

### Community 128 - "JobStatus"
Cohesion: 0.08
Nodes (17): JobRow, FailureKey, FinishedOn, Progress, QueuedOn, RequestedBy, StartedOn, Status (+9 more)

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.25
Nodes (10): Observability (OpenTelemetry), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), external, name, networks, local_shared_network, Postgres Service (mm.postgres) (+2 more)

### Community 131 - "IServiceCollection"
Cohesion: 0.06
Nodes (11): Setup, Setup, Setup, Setup, Setup, ModuleRegistry, IServiceAccountTokenProvider, Setup (+3 more)

### Community 132 - "ObjectStorageOptions"
Cohesion: 0.08
Nodes (26): ObjectStorageOptions, AccessKey, AdditionalTransientStatusCodes, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds (+18 more)

### Community 134 - "IServiceProvider"
Cohesion: 0.06
Nodes (17): IRecurringBackgroundJobs, EventDispatcher, Setup, AuditLogRetentionJobRegistrar, Setup, StockJobRequeueJobRegistrar, StockJobsHousekeepingRegistrar, StockPushSweepJobRegistrar (+9 more)

### Community 135 - "NotificationPayload"
Cohesion: 0.05
Nodes (32): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri, Type (+24 more)

### Community 136 - ".GetClientKey"
Cohesion: 0.20
Nodes (4): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, Response, ClientKey, DummyCaptchaService

### Community 137 - "ValueTask"
Cohesion: 0.10
Nodes (23): Rate limiting and resilience, HttpContextExtensions, RateLimitPartitions, CheckRegistrationRateLimitingPolicy, OnRejected, EmailRateLimitingPolicy, OnRejected, OtpVerifyRateLimitingPolicy (+15 more)

### Community 138 - "services"
Cohesion: 0.25
Nodes (8): container_name, environment, image, networks, ports, restart, services, mm.aspire-dashboard

### Community 139 - "mm.postgres"
Cohesion: 0.25
Nodes (8): container_name, environment, image, networks, ports, restart, volumes, mm.postgres

### Community 140 - "AuditableEntityResponse"
Cohesion: 0.07
Nodes (24): AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, Endpoint, Response (+16 more)

### Community 141 - "JsonSerializerOptions"
Cohesion: 0.05
Nodes (19): StrictDateTimeOffsetJsonConverter, StronglyTypedIdListReadOnlyJsonConverter, StronglyTypedIdReadOnlyJsonConverter, StronglyTypedIdReadOnlyJsonConverter, StronglyTypedIdWriteOnlyJsonConverter, PaginationCursor, Payload, TaggedValue (+11 more)

### Community 142 - "mm.rabbitmq"
Cohesion: 0.25
Nodes (8): container_name, environment, image, networks, ports, restart, volumes, mm.rabbitmq

### Community 143 - "Result"
Cohesion: 0.30
Nodes (4): Result, Error, IsFailure, Value

### Community 145 - "AuditableEntityConfiguration"
Cohesion: 0.17
Nodes (10): AuditableEntityConfiguration, StockReservationConfiguration, WarehouseConnectionConfiguration, AlertRecipient, Email, LastSeenOn, UserId, AlertRecipientConfiguration (+2 more)

### Community 146 - "Response"
Cohesion: 0.17
Nodes (11): Products.Endpoints.Products.v1.My.Get, Endpoint, PackageDimensionsResponse, Response, Attributes, Description, Name, PackageDimensions (+3 more)

### Community 147 - "KeycloakPermissionClient"
Cohesion: 0.11
Nodes (16): KeycloakPaths, KeycloakPermissionClient, DecisionRepresentation, Result, PermissionRepresentation, ResourceName, Scopes, TokenErrorRepresentation (+8 more)

### Community 148 - "Response"
Cohesion: 0.12
Nodes (13): Inventory.Endpoints.StockReservations.v1.Get, Response, HasPendingReleaseAttempt, Id, IsReadOnly, ProductId, ProviderReference, ProviderStatus (+5 more)

### Community 150 - "V1ProductAddedToStoreDomainEvent"
Cohesion: 0.25
Nodes (6): ProductSnapshot, V1ProductAddedToStoreDomainEvent, V1ProductAddedToStoreDomainEventExtensions, ProductSnapshot, V1ProductRemovedFromStoreDomainEvent, V1ProductRemovedFromStoreDomainEventExtensions

### Community 151 - "Response"
Cohesion: 0.22
Nodes (8): Endpoint, Response, Address, CreatedOn, Description, Id, Name, OwnerId

### Community 153 - "NetGsmSmsGateway"
Cohesion: 0.12
Nodes (10): NetGsmSmsGateway, SendMessageBody, Msg, No, SendRequestBody, AppName, Encoding, IysFilter (+2 more)

### Community 154 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.15
Nodes (5): KeycloakPermission, RouteHandlerBuilderExtensions, KeycloakPermissionAuthorizationHandler, KeycloakPermissionRequirement, Permission

### Community 156 - "SendRequestBody"
Cohesion: 0.29
Nodes (6): SendRequestBody, HtmlContent, Sender, Subject, TextContent, To

### Community 157 - "Request"
Cohesion: 0.17
Nodes (12): Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FirstName, LastName (+4 more)

### Community 158 - "Adjust/Request.cs"
Cohesion: 0.24
Nodes (9): Inventory.Endpoints.StockLevels.v1.Adjust, Request, Body, Id, RequestBody, Quantity, Reason, RequestBodyValidator (+1 more)

### Community 159 - "StatusSnapshot"
Cohesion: 0.29
Nodes (7): StatusSnapshot, Active, Committed, Expired, Released, RequiresReconciliation, Unknown

### Community 160 - "BackgroundJobsOptions"
Cohesion: 0.29
Nodes (7): BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount, BackgroundJobsOptionsValidator

### Community 161 - "Response"
Cohesion: 0.20
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegister, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt, Setup

### Community 162 - "Request"
Cohesion: 0.25
Nodes (8): Request, Description, Name, Price, ProductTemplateId, Quantity, Sku, RequestValidator

### Community 163 - "ActivitySource"
Cohesion: 0.16
Nodes (6): RequireFeatureFilter, BackgroundJobsTelemetry, IamTelemetry, InventoryTelemetry, OutboxTelemetry, ProductsTelemetry

### Community 164 - "Request"
Cohesion: 0.20
Nodes (11): Products.Endpoints.Products.v1.Update, Request, Body, Id, RequestBody, Description, Name, Price (+3 more)

### Community 165 - "fluentvalidation"
Cohesion: 0.06
Nodes (38): Products.Endpoints.Stores.v1.My.Update, IAM.Domain.Users, IAM.Endpoints.Otp.VersionNeutral.SendForLogin, Common.Domain.Extensions, Notifications.Infrastructure.Devices.UpdateCurrentPushToken, Common.Application.Validation, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, Common.Domain.Devices (+30 more)

### Community 167 - ".PageMovementsAsync"
Cohesion: 0.28
Nodes (7): ActorNames, GetUsersRequest, GetUsersResponse, UserSummary, GetUsersRequestHandler, UserDisplayCacheEntry, Endpoint

### Community 169 - "Response"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Products.v1.Get, PackageDimensionsResponse, Response, Attributes, AvailableQuantity, Description, Name, PackageDimensions (+3 more)

### Community 171 - "AuditLogEntryConfiguration"
Cohesion: 0.31
Nodes (3): AuditableEntityConfiguration, AuditLogEntryConfiguration, OutboxMessageConfig

### Community 172 - "Product"
Cohesion: 0.05
Nodes (37): Products.Domain.Stores.DomainEvents.v2, File map _(Build)_, ISearchLocalized, Language, Product, Attributes, Description, Images (+29 more)

### Community 173 - "Response"
Cohesion: 0.29
Nodes (4): Inventory.Endpoints.StockReservations.v1.ReserveSeries, Endpoint, Response, Ids

### Community 174 - "CachingOptions.cs"
Cohesion: 0.32
Nodes (7): CachingOptionsValidator, Redis, AppName, Host, Password, Port, RedisValidator

### Community 175 - "AlertRecipientsReconciliationJob"
Cohesion: 0.30
Nodes (7): GetUsersInRolePageRequest, GetUsersInRolePageResponse, RoleUserSummary, GetUsersInRolePageRequestHandler, RoleMember, AlertRecipientsCursor, AlertRecipientsReconciliationJob

### Community 176 - "TimeProvider"
Cohesion: 0.07
Nodes (18): Dirty set and lease, IWarehouseGateway, IWarehouseGatewayFactory, StockLevelId, V1StockReservationReleaseAttemptConfirmedDomainEvent, V1StockReservationReleaseAttemptStartedDomainEvent, StockReservationId, WarehouseConnectionId (+10 more)

### Community 177 - "CorsOptions"
Cohesion: 0.29
Nodes (7): CorsOptions, AllowCredentials, AllowedHeaders, AllowedMethods, AllowedOrigins, MaxAgeInSeconds, CorsOptionsValidator

### Community 178 - "WebhookDelivery"
Cohesion: 0.22
Nodes (7): WebhookDelivery, ConnectionId, DeliveryId, ReceivedOn, ReservationId, WebhookDeliveryId, WebhookDeliveryConfiguration

### Community 180 - "SmsOptions"
Cohesion: 0.11
Nodes (20): SmsOptions, AppName, AttemptTimeoutSeconds, BaseUrl, MaxPerDay, MaxPerPhoneNumberPerDay, MaxRetryAttempts, MsgHeader (+12 more)

### Community 181 - "RabbitMqOptions"
Cohesion: 0.15
Nodes (13): RabbitMqOptions, DefaultConcurrentMessageLimit, DefaultPrefetchCount, Host, Password, Port, RetryIntervalDeltaMs, RetryLimit (+5 more)

### Community 183 - "DomainEventConverter"
Cohesion: 0.25
Nodes (3): DomainEventConverter, IntegrationEventConverter, UtcDateTimeOffsetConverter

### Community 184 - "ChunkEncodingFreeS3Client"
Cohesion: 0.17
Nodes (3): ChunkEncodingFreeS3Client, DisableChunkEncodingHandler, Setup

### Community 185 - "SecretProtectionOptions"
Cohesion: 0.28
Nodes (5): Troubleshooting, SecretProtectionOptions, EncryptionKey, PreviousEncryptionKeys, SecretProtectionOptionsValidator

### Community 186 - "Uri"
Cohesion: 0.11
Nodes (10): Products.Endpoints.Products.v1.AddImage, ReCaptchaResponse, ChallengeTs, ErrorCodes, Hostname, Score, Success, Response (+2 more)

### Community 187 - "SignalROptions"
Cohesion: 0.40
Nodes (5): SignalROptions, RedisConnectionString, RequireRedisBackplaneInProduction, UseRedisBackplane, SignalROptionsValidator

### Community 188 - "Request"
Cohesion: 0.33
Nodes (6): Request, ApiCredential, BaseUrl, Provider, StoreId, RequestValidator

### Community 189 - "StoreId"
Cohesion: 0.08
Nodes (22): GetSeedUserIdsRequest, GetSeedUserIdsResponse, GetSeedUserIdsRequestHandler, ProductImportJob, ClientRequestId, FileName, LastProcessedRow, ObjectKey (+14 more)

### Community 190 - "AI-assisted development workflow"
Cohesion: 0.25
Nodes (8): Adapting it, AI-assisted development workflow, `CLAUDE.md`, Guards, The commands, The four layers, The issue-to-pull-request automation, The knowledge graph

### Community 191 - "Response"
Cohesion: 0.33
Nodes (4): Products.Endpoints.Stores.v1.AddProduct, Endpoint, Response, Id

### Community 192 - "GetDeviceSessionsRequest"
Cohesion: 0.73
Nodes (4): DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, GetDeviceSessionsRequestHandler

### Community 193 - "Response"
Cohesion: 0.08
Nodes (21): BulkImportOptions, MaxFileBytes, MaxRows, SaveChunkSize, BulkImportOptionsValidator, Response, FailureKey, FileName (+13 more)

### Community 194 - "Keycloak realm as code"
Cohesion: 0.22
Nodes (9): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Realms that already exist, Secrets, Seed users (dev / test only) (+1 more)

### Community 195 - "Response"
Cohesion: 0.33
Nodes (4): Inventory.Endpoints.StockReservations.v1.Reserve, Endpoint, Response, Id

### Community 196 - "Response"
Cohesion: 0.29
Nodes (5): Inventory.Endpoints.WarehouseConnections.v1.RotateWebhookSecret, Endpoint, Response, WebhookPath, WebhookSecret

### Community 197 - "StockPushDirtyItem"
Cohesion: 0.22
Nodes (9): StockPushDirtyItem, LastErrorKey, MarkedOn, MarkVersion, ProductId, PushAttempts, StockLevelId, StoreId (+1 more)

### Community 199 - "ProductTemplate"
Cohesion: 0.22
Nodes (7): ProductTemplate, Brand, Color, IsActive, Model, Products, ProductTemplateConfiguration

### Community 200 - "EmailOtpDispatchOutcome"
Cohesion: 0.33
Nodes (5): EmailOtpDispatchErrors, EmailOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled

### Community 201 - "KeycloakScopes"
Cohesion: 0.17
Nodes (11): Devices, Hangfire, KeycloakScopes, Products, ProductTemplates, Sessions, StockLevels, StockReservations (+3 more)

### Community 202 - "ProjectionUpsertOutcome"
Cohesion: 0.40
Nodes (4): ProjectionUpsertOutcome, Inserted, Stale, Updated

### Community 203 - "Response"
Cohesion: 0.29
Nodes (4): Products.Endpoints.ProductTemplates.v1.Create, Setup, Response, Id

### Community 205 - "Response"
Cohesion: 0.33
Nodes (4): Products.Endpoints.Stores.v1.My.AddProduct, Endpoint, Response, Id

### Community 210 - "Response"
Cohesion: 0.22
Nodes (8): Endpoint, Response, Address, Description, Name, OwnerId, ProductCount, Status

### Community 211 - "JwtBearerConfigureOptions"
Cohesion: 0.17
Nodes (3): JwtBearerConfigureOptions, KeycloakPermissionPolicyProvider, Setup

### Community 212 - "ProviderStatusSnapshot"
Cohesion: 0.18
Nodes (9): ProviderStatusSnapshot, Active, Committed, Expired, Released, RequiresReconciliation, Unknown, V1StockReservationProviderStatusMirroredDomainEvent (+1 more)

### Community 213 - "Common.Domain.ResultMonad"
Cohesion: 0.06
Nodes (23): Inventory.Domain.WebhookDeliveries, Inventory.Endpoints.StockLevels.v1.Search, Inventory.Application.WarehouseConnections, Inventory.Infrastructure.InterModuleRequestHandlers, Inventory.Domain.StockCounters, Inventory.Application.ProductSummaries, Inventory.Endpoints.StockReservations.v1.WebhookCallback, Inventory.Infrastructure.Gateway (+15 more)

### Community 214 - "SmsOtpDispatchOutcome"
Cohesion: 0.33
Nodes (5): OtpDispatchErrors, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled

### Community 215 - "Response"
Cohesion: 0.17
Nodes (10): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, Endpoint, Response, ClientId, DeviceName, Id, IpAddress, IsCurrent (+2 more)

### Community 216 - "IInventoryDbContext"
Cohesion: 0.05
Nodes (27): PersistenceQueryableExtensions, SearchProductsByTermRequest, SearchProductsByTermResponse, IInventoryDbContext, ProductSummaries, StockBulkAdjustJobs, StockLevels, StockMovements (+19 more)

### Community 217 - "FeatureFlags"
Cohesion: 0.29
Nodes (6): Feature flags, Checkout, FeatureFlags, IAM, Notifications, Products

### Community 218 - "Request"
Cohesion: 0.11
Nodes (16): ModulesOptions, EnabledModules, ModulesOptionsValidator, Request, ClientId, DeviceId, DeviceName, Otp (+8 more)

### Community 219 - "Response"
Cohesion: 0.25
Nodes (6): Inventory.Endpoints.StockLevels.v1.DailyTotals, Response, CommittedReservations, CommittedUnits, Day, Endpoint

### Community 220 - "SendForEmail/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForEmail, Request, CaptchaToken, Email, RequestValidator

### Community 221 - "ReleaseReconcileOutcome"
Cohesion: 0.20
Nodes (8): Inventory.Endpoints.StockReservations.v1.ReconcileRelease, Endpoint, Response, Outcome, ReleaseReconcileOutcome, AlreadyReleased, NotReleased, Released

### Community 222 - "StockBulkAdjustJobRunner"
Cohesion: 0.08
Nodes (21): StockBulkAdjustRequestedIntegrationEvent, StockBulkAdjustJob, ClientRequestId, FileName, LastProcessedRow, ObjectKey, Reason, RequeueCount (+13 more)

### Community 223 - "Response"
Cohesion: 0.29
Nodes (4): Products.Endpoints.Stores.v1.Create, Setup, Response, Id

### Community 224 - "ResultToResponseTransformer.cs"
Cohesion: 0.11
Nodes (9): Common.Application.EndpointFilters, ProblemResponse, ResultToAcceptedResponseTransformer, ResultToCreatedResponseTransformer, ResultToResponseTransformer, ResultToResponseTransformer, RouteHandlerBuilderExtensions, RouteHandlerBuilderExtensions (+1 more)

### Community 225 - "ProcessedMessage"
Cohesion: 0.29
Nodes (5): ProcessedMessage, ConsumerName, MessageId, ProcessedOn, ProcessedMessageConfiguration

### Community 226 - "Split-Deployment PoC"
Cohesion: 0.33
Nodes (6): Concurrent safety, Files added by this PoC, How to run, Split-Deployment PoC, What this proves, What to check

### Community 227 - "AuditLogEntry"
Cohesion: 0.25
Nodes (6): AuditLogEntry, AggregateId, AggregateType, Event, EventType, Version

### Community 228 - "GetImport/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Products.v1.GetImport, Request, Id, RequestValidator

### Community 229 - "Suspend/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Stores.v1.Suspend, Request, Id, RequestValidator

### Community 230 - ".AdjustBatchAsync"
Cohesion: 0.39
Nodes (3): RowParser, SheetRow, WorkbookReader

### Community 231 - "Key decisions"
Cohesion: 0.29
Nodes (7): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Key decisions

### Community 233 - "WarehouseConnections/v1/Deactivate/Request.cs"
Cohesion: 0.14
Nodes (12): Products.Endpoints.Products.v1.DownloadExport, Products.Endpoints.Probe.v1, Inventory.Endpoints.WarehouseConnections.v1.Deactivate, Request, Id, RequestValidator, Request, Count (+4 more)

### Community 234 - "AuditLogOptions"
Cohesion: 0.33
Nodes (6): AuditLogOptions, PerSchemaRetentionDays, PurgeBatchSize, RetentionCron, RetentionDays, AuditLogOptionsValidator

### Community 236 - "How it works"
Cohesion: 0.33
Nodes (6): Cross-module term search, How it works, One-time setup _(Build, in a migration)_, Query path, Ranking, Write path

### Community 237 - "Response"
Cohesion: 0.29
Nodes (7): Response, Id, LowStockThreshold, ProductId, ProductName, QuantityOnHand, StoreId

### Community 238 - "Response"
Cohesion: 0.29
Nodes (6): Endpoint, Response, Description, Name, Price, Quantity

### Community 239 - "RequestBody"
Cohesion: 0.29
Nodes (7): RequestBody, Description, Name, Price, ProductTemplateId, Quantity, Sku

### Community 241 - "ResxLocalizationOptions"
Cohesion: 0.40
Nodes (5): Localization, ResxLocalizationOptions, DefaultCulture, SupportedCultures, ResxLocalizationOptionsValidator

### Community 242 - "Response"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 245 - "DevicesOptions"
Cohesion: 0.40
Nodes (5): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator

### Community 246 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.03
Nodes (31): Products.Endpoints.Stores.v1.GetApplication, Common.InterModuleRequests, Inventory.Domain.WarehouseConnections.Errors, Common.Domain.StronglyTypedIds, Products.Endpoints.Stores.v1.Search, Products.Endpoints.Stores.v1.ListAll, Products.Endpoints.Stores.v1.Apply, Products.Domain.Stores.Errors (+23 more)

### Community 249 - "Request"
Cohesion: 0.40
Nodes (5): Request, Brand, Color, Model, RequestValidator

### Community 250 - "Request"
Cohesion: 0.22
Nodes (9): Request, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken, Password, PushToken (+1 more)

### Community 251 - "Response"
Cohesion: 0.33
Nodes (4): Products.Endpoints.Stores.v1.My.Create, Endpoint, Response, Id

### Community 252 - "VersionNeutral/Get/Request.cs"
Cohesion: 0.67
Nodes (3): Request, Id, RequestValidator

### Community 260 - "CachingEntryDefaults"
Cohesion: 0.33
Nodes (6): CachingEntryDefaults, Duration, FactoryHardTimeout, FactorySoftTimeout, FailSafeMaxDuration, FailSafeThrottleDuration

### Community 266 - "Object storage"
Cohesion: 0.29
Nodes (3): Object storage, ImageSignature, HmacSignatureVerifier

### Community 269 - "RouteGroupBuilder"
Cohesion: 0.03
Nodes (36): Setup, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Setup (+28 more)

### Community 273 - "HealthCheckOptions"
Cohesion: 0.40
Nodes (5): HealthCheckOptions, EnableHealthChecks, ReadinessTimeoutInSeconds, StartupTimeoutInSeconds, HealthCheckOptionsValidator

### Community 274 - "ReverseProxyOptions"
Cohesion: 0.33
Nodes (5): ReverseProxyOptions, ForwardLimit, IsEnabled, TrustedNetworks, ReverseProxyOptionsValidator

### Community 275 - "VersionNeutral/Search/Request.cs"
Cohesion: 0.40
Nodes (4): Constants, Request, SearchTerm, RequestValidator

### Community 276 - "Request"
Cohesion: 0.33
Nodes (6): Request, Address, Description, Name, OwnerId, RequestValidator

### Community 277 - "Release/Request.cs"
Cohesion: 0.50
Nodes (4): Inventory.Endpoints.StockReservations.v1.Release, Request, Id, RequestValidator

### Community 278 - "GetExport/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Products.v1.GetExport, Request, Id, RequestValidator

### Community 281 - "ProjectionReconciliationOptions"
Cohesion: 0.50
Nodes (4): ProjectionReconciliationOptions, MaxPages, PageSize, ProjectionReconciliationOptionsValidator

### Community 282 - "VerifyEmail/Request.cs"
Cohesion: 0.50
Nodes (4): Request, Email, Otp, RequestValidator

### Community 283 - "Request"
Cohesion: 0.40
Nodes (5): Request, ClientRequestId, File, Reason, RequestValidator

### Community 284 - ".ReadRows"
Cohesion: 0.24
Nodes (3): StringExtensions, WorkbookReader, FormulaGuard

### Community 285 - "Common.Domain.Events"
Cohesion: 0.07
Nodes (12): Inventory.Domain.StockReservations.DomainEvents.v1, Common.Domain.Events, Inventory.Application.StockReservations.DomainEventHandlers.v1, Products.Domain.Stores.DomainEvents.v1, V1StockReservationCommittedDomainEventHandlers, V1ProductImagesRemovedWithProductDomainEvent, V1StoreAddressUpdatedDomainEvent, V1StoreApplicationSubmittedDomainEvent (+4 more)

### Community 286 - "Request"
Cohesion: 0.40
Nodes (5): Request, From, StoreId, To, RequestValidator

### Community 287 - "AddImage/Request.cs"
Cohesion: 0.50
Nodes (4): Request, File, Id, RequestValidator

### Community 288 - "DatabaseOptions.cs"
Cohesion: 0.67
Nodes (3): DatabaseOptions, ConnectionString, DatabaseOptionsValidator

### Community 289 - "SecurityHeadersOptions.cs"
Cohesion: 0.67
Nodes (3): SecurityHeadersOptions, Headers, SecurityHeadersOptionsValidator

### Community 291 - "ProviderSnapshot"
Cohesion: 0.50
Nodes (4): ProviderSnapshot, Dummy, FormApi, JsonApi

### Community 292 - "SendResponseBody"
Cohesion: 0.50
Nodes (4): SendResponseBody, Code, Description, JobId

### Community 293 - "Products/v1/Get/Request.cs"
Cohesion: 0.67
Nodes (3): Request, Id, RequestValidator

### Community 294 - "Stores/v1/Get/Request.cs"
Cohesion: 0.67
Nodes (3): Request, Id, RequestValidator

### Community 323 - "microsoft_aspnetcore_mvc"
Cohesion: 0.03
Nodes (65): Inventory.Endpoints.StockLevels.v1.GetBulkAdjustment, Products.Endpoints.Stores.v1.Deactivate, Products.Endpoints.ProductTemplates.v1.Deactivate, Products.Endpoints.Products.v1.RemoveImage, Products.Endpoints.Stores.v1.Approve, Products.Endpoints.Stores.v1.My.RemoveProduct, Inventory.Endpoints.WarehouseConnections.v1.Get, Products.Endpoints.Stores.v1.Reinstate (+57 more)

### Community 324 - "Request"
Cohesion: 0.20
Nodes (11): Products.Endpoints.Products.v1.My.SetPackageDimensions, Request, Body, Id, RequestBody, HeightCm, LengthCm, WeightKg (+3 more)

## Knowledge Gaps
- **1299 isolated node(s):** `UserId`, `Id`, `IdAsString`, `Roles`, `SessionId` (+1294 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1718 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **124 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `system_text`, `IServiceCollection`, `ObjectStorageOptions`, `NotificationPayload`, `Common.Infrastructure.Extensions`, `Common.Domain.ResultMonad`, `KeycloakPermissionAuthorizationHandler.cs`, `EmailOptions`, `HealthCheckOptions`, `FullTextSearchOptions`, `InventoryOptions`, `ProjectionReconciliationOptions`, `CustomRateLimitingOptions`, `BackgroundJobsOptions`, `DatabaseOptions.cs`, `SecurityHeadersOptions.cs`, `fluentvalidation`, `microsoft_entityframeworkcore`, `ObservabilityOptions`, `OpenApiOptions`, `CachingOptions.cs`, `CorsOptions`, `CaptchaOptions`, `RabbitMqOptions`, `SmsOptions`, `OutboxOptions`, `SignalROptions`, `Setup.Logger.cs`, `Response`, `Host.Swagger`, `KeycloakOptions`, `Common.Domain.ResultMonad`, `RequestLoggingOptions`, `Request`, `AlertRecipientsOptions`, `ResiliencyOptions`, `Common.Application.Storage`, `AuditLogOptions`, `Common.InterModuleRequests.Contracts`, `OtpOptions`, `ResxLocalizationOptions`, `InterModuleRequestOptions`, `DevicesOptions`, `Common.Domain.StronglyTypedIds`, `Response`, `system_diagnostics`, `ProductsOptions`?**
  _High betweenness centrality (0.073) - this node is a cross-community bridge._
- **What connects `UserId`, `Id`, `IdAsString` to the rest of the system?**
  _1299 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `StockLevelId` be split into smaller, more focused modules?**
  _Cohesion score 0.08387096774193549 - nodes in this community are weakly interconnected._
- **Why does `Result` connect `Result` to `StockLevelId`, `StockReservation`, `JobStatus`, `List`, `DateTimeOffset`, `.GetClientKey`, `Result`, `Result`, `IResult`, `.GetProductAsync`, `IInterModuleRequestClient`, `NetGsmSmsGateway`, `Common.Domain.Events`, `Error`, `IReadOnlyCollection`, `CustomRateLimitingOptions`, `ResultTelemetryExtensions`, `.PageMovementsAsync`, `Product`, `TimeProvider`, `.ImportBatchAsync`, `BrevoEmailGateway`, `S3ObjectStoreCore`, `PackageDimensions`, `ProductTemplate`, `EmailOtpDispatchOutcome`, `IIntegrationEventOutbox`, `SmsOtpDispatchOutcome`, `IInventoryDbContext`, `SecretProtector`, `StockBulkAdjustJobRunner`, `ILogger`, `Task`, `.AdjustBatchAsync`, `.GetMeAsync`, `WarehouseHttpGateway`, `ApplicationUserId`, `WarehouseConnectionId`?**
  _High betweenness centrality (0.049) - this node is a cross-community bridge._
- **Should `StockReservation` be split into smaller, more focused modules?**
  _Cohesion score 0.0858974358974359 - nodes in this community are weakly interconnected._
- **Why does `Common.Domain.ResultMonad` connect `Common.Domain.ResultMonad` to `ResultToResponseTransformer.cs`, `system_text`, `StockLevelErrors.cs`, `microsoft_entityframeworkcore`, `ISmsGateway`, `Common.InterModuleRequests.Contracts`, `Result`, `Result`, `IResult`, `Common.Application.Options`, `Common.Domain.ResultMonad`, `Common.Domain.StronglyTypedIds`, `system_diagnostics`, `Error`?**
  _High betweenness centrality (0.037) - this node is a cross-community bridge._
- **Should `RequestBodyLimitMetadata` be split into smaller, more focused modules?**
  _Cohesion score 0.14166666666666666 - nodes in this community are weakly interconnected._