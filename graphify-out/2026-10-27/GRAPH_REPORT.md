# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-10-27)

## Corpus Check
- 878 files · ~182,162 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 5914 nodes · 17708 edges · 339 communities (216 shown, 123 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 814 edges (avg confidence: 0.87)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `46bde49b`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- StockLevelId
- DateTimeOffset
- HttpContext
- Products.Endpoints
- ApplicationUserId
- DbSet
- ApplyAuditingInterceptor
- StockJobRequeueService
- microsoft_extensions_dependencyinjection
- Result
- Common.Domain.ResultMonad
- KeycloakPermissionAuthorizationHandler.cs
- ServiceAccountTokenCache
- Response
- CurrentUser
- EmailOptions
- OutboxModule
- Response
- Common.Application.Options
- FullTextSearchOptions
- BoundedRequestCaptureStream
- DeviceRegistration
- InterModuleRequestHandler
- IInterModuleRequestClient
- InventoryOptions
- Response
- WarehouseSecretReprotectSweepService
- Stores/v1/My/Update/Request.cs
- BackgroundJobsModule
- Error
- InventoryDbContext
- Response
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
- Common.Domain.Events
- ProductImportJobRunner
- OutboxModule
- StoreAccessReconciliationJob
- Outbox Misuse Check
- StockReservationExpirySweepService
- Add Integration Event Command
- host
- Response
- CaptchaOptions
- KeyedResiliencePipelines
- RedisFixedWindowRateLimiter
- Dictionary
- FirebasePushGateway
- Cross-Module Reference Violation
- CustomValidator
- Request
- Bogus Test Data
- BrevoEmailGateway
- AuditLogRetentionService
- S3ObjectStoreCore
- OpenApiOptions
- RequestBody
- IApplicationBuilder
- PackageDimensions
- .AddWarehouseGatewayInfrastructure
- ProductExportJobRunner
- Getting started
- PackageDimensions
- FormApiWarehouseGateway
- AsNoTracking Coverage Check
- IIntegrationEventOutbox
- InventoryDbContext
- ReplaceCredentials/Request.cs
- KeycloakOptions
- Response
- DeviceRegistryReconciliationService
- Request
- IntegrationEventHandlerBase
- common_application_localization_resources
- ProductTemplateId
- .AddNotificationsSignalR
- NotificationsTelemetry
- PushOptions
- Configuration-Driven Module Registration
- Response
- KeycloakPermissionPolicyProvider
- INotificationsDbContext
- AlertRecipientsOptions
- DatabaseSeederOrchestrator
- SecretProtector
- FirebaseServiceAccountOptions
- ILogger
- StockBulkAdjustJob
- Response
- AuditableEntity
- Task
- ResiliencyOptions
- ProvisionUserFailureReason
- SendSecurityAlertRequestHandler
- DefaultIdType
- Response
- Common.InterModuleRequests.Contracts
- WarehouseConnectionResponse
- Products/v1/My/Update/Request.cs
- Response
- Response
- WarehouseHttpGateway
- OtpOptions
- RequestBody
- Response
- SearchProductsByTermRequestHandler
- WebhookDelivery
- For
- IDbContext
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
- system_globalization
- docker-compose.yml (Base Stack)
- IServiceCollection
- ObjectStorageOptions
- IServiceProvider
- NotificationPayload
- .MapEndpoint
- ValueTask
- services
- mm.postgres
- AuditableEntityResponse
- JsonSerializerOptions
- mm.rabbitmq
- ChangeLowStockThreshold/Request.cs
- Persistence conventions
- ReCaptchaService
- Response
- KeycloakPermissionClient
- Response
- NotificationsHub
- Response
- README (Boilerplate Overview)
- NetGsmSmsGateway
- KeycloakPermissionAuthorizationHandler
- DateOnly
- SendRequestBody
- Request
- Request
- StatusSnapshot
- BackgroundJobsOptions
- StoreAccessReconciliationJobRegistrar
- v1/AddProduct/Request.cs
- ActivitySource
- Request
- fluentvalidation
- ResultTelemetryExtensions
- PaginationResponse
- .ToFields
- Response
- Notifications.Application/IAssemblyReference.cs
- Commit/Request.cs
- Product
- Response
- CachingOptions.cs
- AlertRecipientsReconciliationJob
- TimeProvider
- CorsOptions
- RemoveImage/Request.cs
- HttpClient
- SmsOptions
- RabbitMqOptions
- SendForLogin/Request.cs
- Refresh/Request.cs
- ChunkEncodingFreeS3Client
- SecretProtectionOptions
- Uri
- SignalROptions
- Request
- ProductImportJob
- AI-assisted development workflow
- Response
- GetDeviceSessionsRequest
- Response
- Keycloak realm as code
- Response
- Response
- Your first requests
- StronglyTypedIdBinder.cs
- Response
- EmailOtpDispatchOutcome
- KeycloakScopes
- ProjectionUpsertOutcome
- Response
- Response
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- .AddCommonObjectStorage
- JwtBearerConfigureOptions
- ProviderStatusSnapshot
- Common.Domain.ResultMonad
- SmsOtpDispatchOutcome
- Response
- IInventoryDbContext
- FeatureFlags
- Request
- TokenResponseRepresentation
- SendForEmail/Request.cs
- ReleaseReconcileOutcome
- StockBulkAdjustJobRunner
- Response
- RouteHandlerBuilder
- Request
- Split-Deployment PoC
- Request
- GetImport/Request.cs
- Suspend/Request.cs
- Stores/v1/Deactivate/Request.cs
- Key decisions
- Approve/Request.cs
- v1/Request.cs
- My/RemoveProduct/Request.cs
- WarehouseConnections/v1/Deactivate/Request.cs
- How it works
- UpdateCurrentPushToken/Request.cs
- Request
- Revoke/Request.cs
- ConstraintViolationExtensions
- HttpContextTargetingContextAccessor
- Response
- ObjectStorageOptions.cs
- RowOutcome
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
- .Token
- StockLevelErrors.cs
- Inventory.Endpoints
- Infrastructure/StringExtensions.cs
- IAM.Endpoints
- Inventory.Application/IAssemblyReference.cs
- Platform building blocks
- Inventory.Infrastructure/IAssemblyReference.cs
- Inventory.Domain/IAssemblyReference.cs
- RouteGroupBuilder
- Notifications.Infrastructure
- .LocalizeFromError
- IResult
- StringExtensions
- StoreId
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
1. `Result` - 308 edges
2. `Common.Application.Options` - 203 edges
3. `Common.Domain.ResultMonad` - 180 edges
4. `CustomValidator` - 122 edges
5. `ApplicationUserId` - 121 edges
6. `Common.Application.Auth` - 112 edges
7. `IInventoryDbContext` - 107 edges
8. `Common.Application.Extensions` - 106 edges
9. `Common.Application.Validation` - 105 edges
10. `Common.Domain.StronglyTypedIds` - 104 edges

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

## Communities (339 total, 123 thin omitted)

### Community 0 - "StockLevelId"
Cohesion: 0.05
Nodes (31): StockLevel, LastMovementSequence, LowStockThreshold, Movements, ProductId, QuantityOnHand, StockLevelId, StockMovement (+23 more)

### Community 1 - "DateTimeOffset"
Cohesion: 0.04
Nodes (58): Inventory, Webhooks, DomainEvent, CreatedOn, Id, Version, RequestBodyLimitFilter, V1StockLevelCreatedDomainEvent (+50 more)

### Community 2 - "HttpContext"
Cohesion: 0.09
Nodes (13): FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit, RequestBodyLimitEndpointExtensions, RequestBodyLimitMetadata, MaxBodyBytes (+5 more)

### Community 5 - "ApplicationUserId"
Cohesion: 0.07
Nodes (23): ApplicationUserId, IsEmpty, Value, GrantedPermission, KeycloakUser, KeycloakUserPage, KeycloakUserSession, KeycloakAdminClient (+15 more)

### Community 6 - "DbSet"
Cohesion: 0.36
Nodes (3): IdBound, Value, JobClaimExtensions

### Community 7 - "ApplyAuditingInterceptor"
Cohesion: 0.19
Nodes (7): Add a new language/culture, Add search to a new entity _(Build checklist)_, Change the vector (weights, fields, or config), Extending and maintaining, ApplyAuditingInterceptor, ApplySearchLanguageInterceptor, Setup

### Community 8 - "StockJobRequeueService"
Cohesion: 0.08
Nodes (19): BackgroundJobs, JobHousekeepingJob, JobHousekeepingOptions, PageSize, RetentionHours, StaleAfterMinutes, JobHousekeepingOptionsValidator, JobMetricsFilter (+11 more)

### Community 9 - "microsoft_extensions_dependencyinjection"
Cohesion: 0.04
Nodes (27): Common.Infrastructure.Modules, Notifications.Application.Push, Common.Endpoints.Versioning, Notifications.Infrastructure.Email, Products.Endpoints.Probe, Common.Infrastructure.RateLimiting, Notifications.Infrastructure.Push, Notifications.Infrastructure.Sms (+19 more)

### Community 10 - "Result"
Cohesion: 0.04
Nodes (16): IBackgroundJobs, IRecurringBackgroundJobs, CollectionExtensions, FeatureFlagResultExtensions, Result, Result, Error, IsFailure (+8 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.05
Nodes (49): Inventory.Application.StockLevels, IAM.Endpoints.Captcha.VersionNeutral, Common.Endpoints.Webhooks, Inventory.Application.StockReservations, Products.Infrastructure.InterModuleRequestHandlers, Common.Application.Search, Products.Endpoints.Stores, Products.Domain.Products.Errors (+41 more)

### Community 12 - "KeycloakPermissionAuthorizationHandler.cs"
Cohesion: 0.20
Nodes (3): IAM.Infrastructure.Auth, Notifications.Infrastructure.Hubs, Notifications.Application.Hubs

### Community 13 - "ServiceAccountTokenCache"
Cohesion: 0.18
Nodes (6): IServiceAccountTokenProvider, ServiceAccountTokenCache, AccessToken, ExpiresAt, ServiceAccountTokenProvider, Setup

### Community 14 - "Response"
Cohesion: 0.13
Nodes (13): IAM.Endpoints.Users.VersionNeutral.Me.Get, Endpoint, Response, BirthDate, CreatedOn, Email, FirstName, Id (+5 more)

### Community 15 - "CurrentUser"
Cohesion: 0.13
Nodes (14): Job rows, IBackgroundUserContext, UserId, BackgroundUserContext, UserId, CurrentUser, BackgroundUserIdAsString, Id (+6 more)

### Community 16 - "EmailOptions"
Cohesion: 0.10
Nodes (23): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+15 more)

### Community 17 - "OutboxModule"
Cohesion: 0.13
Nodes (8): AutoMigrateMarker, IAutoMigrateMarker, MigrationGuard, OutboxModule, ActivitySourceNames, MeterNames, Name, StartupPriority

### Community 18 - "Response"
Cohesion: 0.15
Nodes (12): Response, ActingUserId, ActingUserName, CreatedOn, Id, Quantity, QuantityOnHandAfter, Reason (+4 more)

### Community 19 - "Common.Application.Options"
Cohesion: 0.06
Nodes (30): Products.Domain.Products.DomainEvents.v1, Common.Application.Persistence.Inbox, Products.Infrastructure.Imports, Inventory.Application.Persistence, Inventory.Application.IntegrationEventHandlers, Notifications.Infrastructure.Alerts, Common.IntegrationEvents, Outbox (+22 more)

### Community 20 - "FullTextSearchOptions"
Cohesion: 0.15
Nodes (15): Configuration, Full-Text Search, Gotchas, Non-goals, Per-entity strategy, Testing, The problem, TL;DR: what the feature gives you (+7 more)

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.13
Nodes (6): BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 22 - "DeviceRegistration"
Cohesion: 0.12
Nodes (12): DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive, LastReconciledOn, PushToken, PushTokenUpdatedOn (+4 more)

### Community 23 - "InterModuleRequestHandler"
Cohesion: 0.12
Nodes (12): IInterModuleRequestHandler, InterModuleRequestHandler, GetSeedUserIdsRequest, GetSeedUserIdsResponse, GetStockLevelRequest, GetStockLevelResponse, GetProductRequest, GetProductResponse (+4 more)

### Community 24 - "IInterModuleRequestClient"
Cohesion: 0.05
Nodes (38): IInterModuleRequest, IInterModuleRequestClient, MassTransitInterModuleRequestClient, DeprovisionUserRequest, DeprovisionUserResponse, ProvisionUserRequest, ProvisionUserResponse, BindDeviceSessionRequest (+30 more)

### Community 25 - "InventoryOptions"
Cohesion: 0.05
Nodes (39): InventoryOptions, AllowPrivateWarehouseAddresses, BulkAdjustBatchesPerMessage, BulkAdjustJobConcurrentMessageLimit, BulkAdjustMaxStoredRowErrors, BusinessTimeZoneId, DailyTotalsMaxRangeDays, MaxOccurrencesPerSeries (+31 more)

### Community 26 - "Response"
Cohesion: 0.29
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, Endpoint, Response, EmailVerificationToken, IsRegistered

### Community 27 - "WarehouseSecretReprotectSweepService"
Cohesion: 0.27
Nodes (3): Setup, WarehouseSecretReprotectSweepJobRegistrar, WarehouseSecretReprotectSweepService

### Community 28 - "Stores/v1/My/Update/Request.cs"
Cohesion: 0.33
Nodes (6): Products.Endpoints.Stores.v1.My.Update, Request, Address, Description, Name, RequestValidator

### Community 29 - "BackgroundJobsModule"
Cohesion: 0.13
Nodes (9): Cross-process call path, How it works, ICoreModule, BackgroundJobsModule, ActivitySourceNames, MeterNames, Name, StartupPriority (+1 more)

### Community 30 - "Error"
Cohesion: 0.07
Nodes (24): Error, Key, ParameterName, StatusCode, SubErrors, Value, CaptchaErrors, IdentityErrors (+16 more)

### Community 31 - "InventoryDbContext"
Cohesion: 0.09
Nodes (19): ProjectionEntity, SourceVersion, GetProductsPageRequest, GetProductsPageResponse, ProductPageItem, ProductSummary, Name, ProductId (+11 more)

### Community 33 - "Response"
Cohesion: 0.29
Nodes (6): Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 34 - "Response"
Cohesion: 0.09
Nodes (20): StoreStatus, Active, Inactive, PendingApproval, Suspended, Endpoint, Response, Address (+12 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.07
Nodes (22): Common.Application.Persistence.Outbox, IEventHandlerWrapper, IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn (+14 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.04
Nodes (44): CustomRateLimitingOptions, CheckRegistration, CreateStore, Email, ExemptPathPrefixes, Global, OtpVerify, Register (+36 more)

### Community 37 - "AdminRepresentations.cs"
Cohesion: 0.08
Nodes (23): CredentialRepresentation, Temporary, Type, Value, ErrorRepresentation, Error, ErrorDescription, ErrorMessage (+15 more)

### Community 38 - "microsoft_entityframeworkcore"
Cohesion: 0.07
Nodes (20): Common.Infrastructure.Persistence.Inbox, Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Products.Infrastructure.Persistence.Seeding, Products.Application.Exports, Common.Application.Persistence, Common.Infrastructure.Persistence.Outbox, Inventory.Infrastructure.Persistence.EntityConfigurations (+12 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.09
Nodes (17): ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics, EnableTracing, LogSink, MinimumLevel (+9 more)

### Community 41 - "OpenApiOptions"
Cohesion: 0.22
Nodes (9): OpenApiOptions, ContactEmail, ContactName, Description, EnableSwagger, LicenseName, LicenseUrl, Title (+1 more)

### Community 42 - "BoundedCaptureStream"
Cohesion: 0.06
Nodes (22): RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes, SensitiveQueryParamPaths (+14 more)

### Community 43 - "Common.Domain.Events"
Cohesion: 0.07
Nodes (16): Inventory.Domain.StockReservations.DomainEvents.v1, Common.Domain.Events, Inventory.Application.StockReservations.DomainEventHandlers.v1, Inventory.Application.StockLevels.DomainEventHandlers.v1, Inventory.Domain.StockLevels.DomainEvents.v1, IEventHandler, UnknownDomainEvent, StockLowThresholdCrossedIntegrationEventPublishingHandler (+8 more)

### Community 44 - "ProductImportJobRunner"
Cohesion: 0.08
Nodes (9): WorkbookReader, ProductImportJobId, Endpoint, ProductImportJobEnqueuer, ProductImportJobRunner, Slice, RowParser, SheetRow (+1 more)

### Community 45 - "OutboxModule"
Cohesion: 0.05
Nodes (33): Adding a module or a feature, How modules fit together, Modules, Outbox, OutboxOptions, BaseBackoffSeconds, BatchSize, ClaimLeaseSeconds (+25 more)

### Community 46 - "StoreAccessReconciliationJob"
Cohesion: 0.35
Nodes (7): GetStoresPageRequest, GetStoresPageResponse, StorePageItem, Setup, StoreAccessReconciliationCursor, StoreAccessReconciliationJob, GetStoresPageRequestHandler

### Community 48 - "StockReservationExpirySweepService"
Cohesion: 0.31
Nodes (3): Setup, StockReservationExpirySweepJobRegistrar, StockReservationExpirySweepService

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
Cohesion: 0.14
Nodes (8): Rate limiting and resilience, FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration, RetryAfterHeaderExtensions, WarehouseOutcomeUnknownException

### Community 55 - "Dictionary"
Cohesion: 0.18
Nodes (7): ProductImportRowError, BatchSummary, RowResult, Created, Unchanged, Updated, ProductRow

### Community 56 - "FirebasePushGateway"
Cohesion: 0.19
Nodes (5): IPushGateway, PushMessage, DummyPushGateway, FirebasePushGateway, Setup

### Community 58 - "CustomValidator"
Cohesion: 0.06
Nodes (38): CustomRateLimitingOptionsValidator, FixedWindowValidator, HealthCheckOptions, EnableHealthChecks, ReadinessTimeoutInSeconds, StartupTimeoutInSeconds, HealthCheckOptionsValidator, OutboxCleanupSettings (+30 more)

### Community 59 - "Request"
Cohesion: 0.15
Nodes (13): Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken (+5 more)

### Community 61 - "BrevoEmailGateway"
Cohesion: 0.14
Nodes (9): BrevoEmailGateway, SendContact, Email, Name, SendErrorBody, Code, Message, SendResponseBody (+1 more)

### Community 63 - "AuditLogRetentionService"
Cohesion: 0.27
Nodes (3): AuditLogRetentionJobRegistrar, AuditLogRetentionService, Setup

### Community 64 - "S3ObjectStoreCore"
Cohesion: 0.09
Nodes (9): IPrivateObjectStore, IPublicObjectStore, ObjectListItem, ObjectListPage, ObjectMetadata, UploadObjectRequest, S3ObjectStoreCore, S3PrivateObjectStore (+1 more)

### Community 65 - "OpenApiOptions"
Cohesion: 0.06
Nodes (11): Host, Host.Configurations, Host.Swagger, Setup, Program, ConfigureSwaggerOptions, DefaultResponsesOperationFilter, RemoveDefaultResponseSchemaFilter (+3 more)

### Community 66 - "RequestBody"
Cohesion: 0.18
Nodes (10): RequestBody, FirstDeadline, IntervalDays, OccurrenceCount, ProductId, QuantityPerOccurrence, Warehouses, WarehouseAllocation (+2 more)

### Community 67 - "IApplicationBuilder"
Cohesion: 0.12
Nodes (4): SearchLanguageResolver, UniversalConfig, Setup, Setup

### Community 69 - ".AddWarehouseGatewayInfrastructure"
Cohesion: 0.17
Nodes (5): Setup, WarehouseAddressRefusedException, IWarehouseHostResolver, SystemWarehouseHostResolver, WarehouseConnectGuard

### Community 70 - "ProductExportJobRunner"
Cohesion: 0.33
Nodes (4): ProductExportJobId, ExportPageRequest, ExportRow, ProductExportJobRunner

### Community 71 - "Getting started"
Cohesion: 0.18
Nodes (11): Configuration, Getting started, Prerequisites, Run the API from the command line instead, Run the load test, Run the tests, Run the whole system, Seeded data (+3 more)

### Community 72 - "PackageDimensions"
Cohesion: 0.22
Nodes (6): PackageDimensions, HeightCm, LengthCm, VolumeCm3, WeightKg, WidthCm

### Community 73 - "FormApiWarehouseGateway"
Cohesion: 0.19
Nodes (9): FormApiWarehouseGateway, HoldEnvelope, HoldResult, ErrorResponseBody, JsonApiWarehouseGateway, PushStockItem, PushStockRequestBody, ReleaseRequestBody (+1 more)

### Community 75 - "IIntegrationEventOutbox"
Cohesion: 0.09
Nodes (29): DomainEventHandlerBase, IIntegrationEventOutbox, StockLowThresholdRecoveredIntegrationEventPublishingHandler, StockReservationCommittedIntegrationEventPublishingHandler, MovementTypeSnapshot, Adjusted, Received, ReservationCommitted (+21 more)

### Community 76 - "InventoryDbContext"
Cohesion: 0.04
Nodes (36): ProjectionUpsertExtensions, AuditLogEntry, AggregateId, AggregateType, Event, EventType, Version, IntegrationEventOutbox (+28 more)

### Community 77 - "ReplaceCredentials/Request.cs"
Cohesion: 0.28
Nodes (8): Inventory.Endpoints.WarehouseConnections.v1.ReplaceCredentials, Request, Body, Id, RequestBody, ApiCredential, RequestBodyValidator, RequestValidator

### Community 78 - "KeycloakOptions"
Cohesion: 0.11
Nodes (19): KeycloakOptions, AttemptTimeoutSeconds, Authority, BaseUrl, DecisionCacheMaxDurationSeconds, Realm, RequireHttpsMetadata, ResourceClientId (+11 more)

### Community 79 - "Response"
Cohesion: 0.14
Nodes (12): IAM.Endpoints.Users.VersionNeutral.Search, Endpoint, Response, BirthDate, CreatedOn, Email, Enabled, FirstName (+4 more)

### Community 80 - "DeviceRegistryReconciliationService"
Cohesion: 0.47
Nodes (5): GetActiveSessionIdsRequest, GetActiveSessionIdsResponse, UserSessionIds, GetActiveSessionIdsRequestHandler, DeviceRegistryReconciliationService

### Community 81 - "Request"
Cohesion: 0.18
Nodes (11): Request, Email, EmailVerificationToken, FirstName, LastName, Password, PhoneNumber, StoreAddress (+3 more)

### Community 82 - "IntegrationEventHandlerBase"
Cohesion: 0.04
Nodes (44): Projections and reconciliation, ConcurrencyConfiguratorExtensions, IntegrationEventHandlerBase, MaxEventAge, CachingOptions, AllowInMemoryOnlyInProduction, EntryDefaults, IdempotencyKeyDuration (+36 more)

### Community 83 - "common_application_localization_resources"
Cohesion: 0.04
Nodes (57): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Products.v1.ListImports, Inventory.Endpoints.StockReservations.v1.My.Search, Products.Endpoints.Stores.v1.My.AuditLog, Inventory.Endpoints.WarehouseConnections.v1.Search, Inventory.Endpoints.StockLevels.v1.ListBulkAdjustments, Inventory.Endpoints.WarehouseConnections.v1.My.Search, Products.Endpoints.Products.v1.AuditLog (+49 more)

### Community 84 - "ProductTemplateId"
Cohesion: 0.05
Nodes (37): AuditableEntityConfiguration, JobRowConfiguration, DomainEventConverter, IntegrationEventConverter, StronglyTypedIdValueConverter, StoreDailyStockTotal, CommittedReservations, CommittedUnits (+29 more)

### Community 87 - "PushOptions"
Cohesion: 0.21
Nodes (12): PushOptions, Provider, SendTimeoutSeconds, ServiceAccount, Templates, PushOptionsValidator, PushProvider, Dummy (+4 more)

### Community 89 - "Response"
Cohesion: 0.22
Nodes (8): Endpoint, Response, Address, Description, Name, OwnerId, ProductCount, Status

### Community 91 - "INotificationsDbContext"
Cohesion: 0.24
Nodes (7): INotificationsDbContext, AlertRecipients, DeviceRegistrations, NotificationsDbContext, AlertRecipients, DeviceRegistrations, Setup

### Community 92 - "AlertRecipientsOptions"
Cohesion: 0.29
Nodes (7): AlertRecipientsOptions, EmailHtmlBodyTemplate, EmailSubjectTemplate, EmailTextBodyTemplate, ReconcileCron, RoleName, AlertRecipientsOptionsValidator

### Community 94 - "SecretProtector"
Cohesion: 0.05
Nodes (24): Secret protection, HmacSignatureVerifier, SecretGenerator, SecretProtector, Setup, Endpoint, Request, DeliveryId (+16 more)

### Community 95 - "FirebaseServiceAccountOptions"
Cohesion: 0.25
Nodes (8): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri, Type

### Community 96 - "ILogger"
Cohesion: 0.06
Nodes (11): ProjectionReconciliationJob, ActorNames, EventDispatcher, OutboxSaveHelper, CriticalAlertRaisedIntegrationEvent, GlobalExceptionHandlingMiddleware, LoginCompletion, KeycloakTokenClient (+3 more)

### Community 97 - "StockBulkAdjustJob"
Cohesion: 0.12
Nodes (15): StockBulkAdjustJob, ClientRequestId, FileName, LastProcessedRow, ObjectKey, Reason, RequeueCount, RowErrors (+7 more)

### Community 98 - "Response"
Cohesion: 0.08
Nodes (21): BulkImportOptions, MaxFileBytes, MaxRows, SaveChunkSize, BulkImportOptionsValidator, Endpoint, Response, FailureKey (+13 more)

### Community 99 - "AuditableEntity"
Cohesion: 0.06
Nodes (29): JobRow, FailureKey, FinishedOn, Progress, QueuedOn, RequestedBy, StartedOn, Status (+21 more)

### Community 100 - "Task"
Cohesion: 0.03
Nodes (22): ICurrentUser, Id, IdAsString, Roles, SessionId, OtpCacheEntry, IVariantFeatureManagerExtensions, IInboxStore (+14 more)

### Community 101 - "ResiliencyOptions"
Cohesion: 0.09
Nodes (23): KeyedResilienceProfile, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, RateLimitFailOpen, RateLimitPermits (+15 more)

### Community 102 - "ProvisionUserFailureReason"
Cohesion: 0.29
Nodes (6): ProvisionUserFailureReason, EmailAlreadyRegistered, IdentityProviderUnavailable, None, PhoneNumberAlreadyRegistered, Rejected

### Community 103 - "SendSecurityAlertRequestHandler"
Cohesion: 0.52
Nodes (5): SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, SendSecurityAlertRequestHandler

### Community 104 - "DefaultIdType"
Cohesion: 0.07
Nodes (36): IAM, Notifications, Products, Transactional outbox and inbox, ProcessedMessage, ConsumerName, MessageId, ProcessedOn (+28 more)

### Community 105 - "Response"
Cohesion: 0.25
Nodes (6): Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 106 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.05
Nodes (22): Notifications.Application.Otp, Common.Endpoints, Notifications.Application.Sms, Notifications.Infrastructure.Devices, IAM.Infrastructure.Keycloak.Representations, Products.Endpoints.Stores.v1.Apply, Products.Domain.Stores.Errors, Common.Application.Caching (+14 more)

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
Cohesion: 0.25
Nodes (6): Endpoint, Response, Description, Name, Price, Quantity

### Community 112 - "OtpOptions"
Cohesion: 0.07
Nodes (29): OtpOptions, DummyCode, EmailQuotaWindowMinutes, ExpirationInMinutes, Length, MaxSendsPerEmailPerWindow, MaxSendsPerPhonePerWindow, PhoneQuotaWindowMinutes (+21 more)

### Community 113 - "RequestBody"
Cohesion: 0.07
Nodes (36): Inventory.Endpoints.StockLevels.v1.Receive, Products.Endpoints.Stores.v1.Update, Inventory.Endpoints.StockLevels.v1.Adjust, Request, Body, Id, RequestBody, Quantity (+28 more)

### Community 114 - "Response"
Cohesion: 0.14
Nodes (12): IAM.Endpoints.Users.VersionNeutral.Get, Endpoint, Response, BirthDate, CreatedOn, Email, Enabled, FirstName (+4 more)

### Community 115 - "SearchProductsByTermRequestHandler"
Cohesion: 0.26
Nodes (7): Inter-module requests, SearchProductsByTermRequest, SearchProductsByTermResponse, Endpoint, PageResponse, ProductsCacheKeys, SearchProductsByTermRequestHandler

### Community 116 - "WebhookDelivery"
Cohesion: 0.10
Nodes (8): StronglyTypedIdHelper, WebhookDelivery, ConnectionId, DeliveryId, ReceivedOn, ReservationId, WebhookDeliveryId, WebhookDeliveryConfiguration

### Community 118 - "IDbContext"
Cohesion: 0.29
Nodes (4): IDbContext, AuditLog, ChangeTracker, Database

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
Cohesion: 0.09
Nodes (16): IdentityScheme, Email, PhoneNumber, Setup, Response, AccessToken, AccessTokenExpiresAt, RefreshToken (+8 more)

### Community 124 - "system_diagnostics"
Cohesion: 0.05
Nodes (7): BackgroundJobs.Telemetry, BackgroundJobs, Inventory.Endpoints.StockLevels.v1.BulkAdjust, Products.Endpoints.Products.v1.Export, Host.Infrastructure, LoginMethods, SessionRevokedReasons

### Community 125 - "ProductsOptions"
Cohesion: 0.13
Nodes (15): ProductsOptions, ExportJobConcurrentMessageLimit, ExportMaxRows, ExportPageSize, ImageMaxFileBytes, ImportBatchesPerMessage, ImportJobConcurrentMessageLimit, ImportMaxStoredRowErrors (+7 more)

### Community 126 - "Pagination and search"
Cohesion: 0.42
Nodes (3): Pagination and search, LikePattern, TrigramIndexExtensions

### Community 127 - "WarehouseConnectionId"
Cohesion: 0.06
Nodes (29): Inventory.Domain.WarehouseConnections.DomainEvents.v1, IStronglyTypedId, Value, ProviderSnapshot, Dummy, FormApi, JsonApi, V1WarehouseConnectionCreatedDomainEvent (+21 more)

### Community 128 - "JobStatus"
Cohesion: 0.17
Nodes (9): JobStatus, Failed, Queued, Running, Succeeded, Endpoint, FileDownload, FileReference (+1 more)

### Community 129 - "system_globalization"
Cohesion: 0.06
Nodes (15): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, IAM.Domain.Users, IAM.Endpoints.Tokens.VersionNeutral.Create, IAM.Endpoints.Users.VersionNeutral.SelfRegister, Common.Infrastructure.Localization, Products.Endpoints.Products.v1.Import, Products.Application.Imports, Products.Infrastructure.Caching (+7 more)

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.25
Nodes (10): Observability (OpenTelemetry), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), external, name, networks, local_shared_network, Postgres Service (mm.postgres) (+2 more)

### Community 131 - "IServiceCollection"
Cohesion: 0.06
Nodes (9): Setup, Setup, Setup, Setup, Setup, ModuleRegistry, Setup, Setup (+1 more)

### Community 132 - "ObjectStorageOptions"
Cohesion: 0.09
Nodes (22): ObjectStorageOptions, AccessKey, AdditionalTransientStatusCodes, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds (+14 more)

### Community 134 - "IServiceProvider"
Cohesion: 0.11
Nodes (9): ResultToAcceptedResponseTransformer, ResultToCreatedResponseTransformer, ResultToResponseTransformer, ResultToResponseTransformer, StockJobRequeueJobRegistrar, ProductSummariesReconciliationJobRegistrar, StockReleaseReconcileSweepJobRegistrar, AlertRecipientsReconcileJobRegistrar (+1 more)

### Community 135 - "NotificationPayload"
Cohesion: 0.20
Nodes (4): INotificationDispatcher, INotificationsClient, NotificationPayload, SignalRNotificationDispatcher

### Community 136 - ".MapEndpoint"
Cohesion: 0.29
Nodes (4): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, Response, ClientKey, Setup

### Community 137 - "ValueTask"
Cohesion: 0.10
Nodes (22): ProblemResponse, Setup, CheckRegistrationRateLimitingPolicy, OnRejected, EmailRateLimitingPolicy, OnRejected, OtpVerifyRateLimitingPolicy, OnRejected (+14 more)

### Community 138 - "services"
Cohesion: 0.25
Nodes (8): container_name, environment, image, networks, ports, restart, services, mm.aspire-dashboard

### Community 139 - "mm.postgres"
Cohesion: 0.25
Nodes (8): container_name, environment, image, networks, ports, restart, volumes, mm.postgres

### Community 140 - "AuditableEntityResponse"
Cohesion: 0.07
Nodes (23): Products.Endpoints.ProductTemplates.v1.Search, AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, Endpoint (+15 more)

### Community 141 - "JsonSerializerOptions"
Cohesion: 0.06
Nodes (17): StrictDateTimeOffsetJsonConverter, StronglyTypedIdListReadOnlyJsonConverter, StronglyTypedIdReadOnlyJsonConverter, StronglyTypedIdReadOnlyJsonConverter, StronglyTypedIdWriteOnlyJsonConverter, PaginationCursor, Payload, TaggedValue (+9 more)

### Community 142 - "mm.rabbitmq"
Cohesion: 0.25
Nodes (8): container_name, environment, image, networks, ports, restart, volumes, mm.rabbitmq

### Community 143 - "ChangeLowStockThreshold/Request.cs"
Cohesion: 0.28
Nodes (8): Inventory.Endpoints.StockLevels.v1.ChangeLowStockThreshold, Request, Body, Id, RequestBody, Threshold, RequestBodyValidator, RequestValidator

### Community 144 - "Persistence conventions"
Cohesion: 0.43
Nodes (3): Persistence conventions, StockCounterWriter, JsonbListExtensions

### Community 146 - "Response"
Cohesion: 0.17
Nodes (11): Products.Endpoints.Products.v1.My.Get, Endpoint, PackageDimensionsResponse, Response, Attributes, Description, Name, PackageDimensions (+3 more)

### Community 147 - "KeycloakPermissionClient"
Cohesion: 0.22
Nodes (9): KeycloakPermissionClient, DecisionRepresentation, Result, PermissionRepresentation, ResourceName, Scopes, TokenErrorRepresentation, Error (+1 more)

### Community 148 - "Response"
Cohesion: 0.12
Nodes (13): Inventory.Endpoints.StockReservations.v1.Get, Response, HasPendingReleaseAttempt, Id, IsReadOnly, ProductId, ProviderReference, ProviderStatus (+5 more)

### Community 151 - "Response"
Cohesion: 0.22
Nodes (8): Endpoint, Response, Address, CreatedOn, Description, Id, Name, OwnerId

### Community 153 - "NetGsmSmsGateway"
Cohesion: 0.10
Nodes (14): NetGsmSmsGateway, SendMessageBody, Msg, No, SendRequestBody, AppName, Encoding, IysFilter (+6 more)

### Community 154 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.19
Nodes (4): KeycloakPermission, KeycloakPermissionAuthorizationHandler, KeycloakPermissionRequirement, Permission

### Community 155 - "DateOnly"
Cohesion: 0.22
Nodes (7): DayBoundsUtc, TimeProviderExtensions, Request, From, StoreId, To, RequestValidator

### Community 156 - "SendRequestBody"
Cohesion: 0.29
Nodes (6): SendRequestBody, HtmlContent, Sender, Subject, TextContent, To

### Community 157 - "Request"
Cohesion: 0.17
Nodes (12): Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FirstName, LastName (+4 more)

### Community 158 - "Request"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Products.v1.My.SetAttributes, AttributeItem, Name, Value, Request, Body, Id, RequestBody (+3 more)

### Community 159 - "StatusSnapshot"
Cohesion: 0.29
Nodes (7): StatusSnapshot, Active, Committed, Expired, Released, RequiresReconciliation, Unknown

### Community 160 - "BackgroundJobsOptions"
Cohesion: 0.29
Nodes (7): BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount, BackgroundJobsOptionsValidator

### Community 162 - "v1/AddProduct/Request.cs"
Cohesion: 0.06
Nodes (32): Products.Endpoints.Products.v1.Search, Constants, Request, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name (+24 more)

### Community 163 - "ActivitySource"
Cohesion: 0.16
Nodes (7): RequireFeatureFilter, RouteHandlerBuilderExtensions, BackgroundJobsTelemetry, IamTelemetry, InventoryTelemetry, OutboxTelemetry, ProductsTelemetry

### Community 164 - "Request"
Cohesion: 0.20
Nodes (11): Products.Endpoints.Products.v1.Update, Request, Body, Id, RequestBody, Description, Name, Price (+3 more)

### Community 165 - "fluentvalidation"
Cohesion: 0.03
Nodes (60): Products.Endpoints.Stores.v1.My.Create, Products.Endpoints.Stores.v1.Reinstate, Common.Application.Validation, IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, Localization, AuditLogOptions, PerSchemaRetentionDays, PurgeBatchSize (+52 more)

### Community 167 - "PaginationResponse"
Cohesion: 0.08
Nodes (20): AuditLogDto, PaginationResponse, HasNext, HasPrevious, HasTotal, NextPageNumber, PreviousPageNumber, TotalPages (+12 more)

### Community 169 - "Response"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Products.v1.Get, PackageDimensionsResponse, Response, Attributes, AvailableQuantity, Description, Name, PackageDimensions (+3 more)

### Community 171 - "Commit/Request.cs"
Cohesion: 0.28
Nodes (8): Inventory.Endpoints.StockReservations.v1.Commit, Request, Body, Id, RequestBody, ProviderReference, RequestBodyValidator, RequestValidator

### Community 172 - "Product"
Cohesion: 0.04
Nodes (41): ProductChangedIntegrationEventPublishingHandler, V1ProductNameUpdatedDomainEventHandlers, AttributeSnapshot, V1ProductAttributesReplacedDomainEvent, V1ProductAttributesReplacedDomainEventExtensions, V1ProductDescriptionUpdatedDomainEvent, V1ProductImageAddedDomainEvent, V1ProductImageRemovedDomainEvent (+33 more)

### Community 173 - "Response"
Cohesion: 0.33
Nodes (4): Inventory.Endpoints.StockReservations.v1.ReserveSeries, Endpoint, Response, Ids

### Community 174 - "CachingOptions.cs"
Cohesion: 0.16
Nodes (13): CachingEntryDefaults, Duration, FactoryHardTimeout, FactorySoftTimeout, FailSafeMaxDuration, FailSafeThrottleDuration, CachingOptionsValidator, Redis (+5 more)

### Community 175 - "AlertRecipientsReconciliationJob"
Cohesion: 0.36
Nodes (6): GetUsersInRolePageRequest, GetUsersInRolePageResponse, RoleUserSummary, GetUsersInRolePageRequestHandler, AlertRecipientsCursor, AlertRecipientsReconciliationJob

### Community 176 - "TimeProvider"
Cohesion: 0.07
Nodes (15): IWarehouseGateway, IWarehouseGatewayFactory, WarehouseStockQuantity, Endpoint, Response, CommittedReservations, CommittedUnits, Day (+7 more)

### Community 177 - "CorsOptions"
Cohesion: 0.29
Nodes (7): CorsOptions, AllowCredentials, AllowedHeaders, AllowedMethods, AllowedOrigins, MaxAgeInSeconds, CorsOptionsValidator

### Community 178 - "RemoveImage/Request.cs"
Cohesion: 0.40
Nodes (5): Products.Endpoints.Products.v1.RemoveImage, Request, Id, ImageId, RequestValidator

### Community 180 - "SmsOptions"
Cohesion: 0.07
Nodes (29): SmsOptions, AppName, AttemptTimeoutSeconds, BaseUrl, MaxPerDay, MaxPerPhoneNumberPerDay, MaxRetryAttempts, MsgHeader (+21 more)

### Community 181 - "RabbitMqOptions"
Cohesion: 0.15
Nodes (13): RabbitMqOptions, DefaultConcurrentMessageLimit, DefaultPrefetchCount, Host, Password, Port, RetryIntervalDeltaMs, RetryLimit (+5 more)

### Community 182 - "SendForLogin/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForLogin, Request, CaptchaToken, PhoneNumber, RequestValidator

### Community 183 - "Refresh/Request.cs"
Cohesion: 0.40
Nodes (4): IAM.Endpoints.Tokens.VersionNeutral.Refresh, Request, RefreshToken, RequestValidator

### Community 185 - "SecretProtectionOptions"
Cohesion: 0.27
Nodes (5): Troubleshooting, SecretProtectionOptions, EncryptionKey, PreviousEncryptionKeys, SecretProtectionOptionsValidator

### Community 186 - "Uri"
Cohesion: 0.11
Nodes (10): Products.Endpoints.Products.v1.AddImage, ReCaptchaResponse, ChallengeTs, ErrorCodes, Hostname, Score, Success, Response (+2 more)

### Community 187 - "SignalROptions"
Cohesion: 0.40
Nodes (5): SignalROptions, RedisConnectionString, RequireRedisBackplaneInProduction, UseRedisBackplane, SignalROptionsValidator

### Community 188 - "Request"
Cohesion: 0.24
Nodes (7): WarehouseAddressPolicy, Request, ApiCredential, BaseUrl, Provider, StoreId, RequestValidator

### Community 189 - "ProductImportJob"
Cohesion: 0.15
Nodes (13): ProductImportJob, ClientRequestId, FileName, LastProcessedRow, ObjectKey, ProductsCreated, ProductsUpdated, ProductTemplateId (+5 more)

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

### Community 197 - "Your first requests"
Cohesion: 0.33
Nodes (6): Connect a store to a warehouse system, Errors, Queued work: accept, queue, poll, Sign in, Watch two modules cooperate, Your first requests

### Community 199 - "Response"
Cohesion: 0.22
Nodes (8): Endpoint, Response, Address, Description, Name, OwnerId, ProductCount, Status

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

### Community 212 - "ProviderStatusSnapshot"
Cohesion: 0.29
Nodes (7): ProviderStatusSnapshot, Active, Committed, Expired, Released, RequiresReconciliation, Unknown

### Community 213 - "Common.Domain.ResultMonad"
Cohesion: 0.05
Nodes (22): Inventory.Domain.WebhookDeliveries, Inventory.Endpoints.StockLevels.v1.Search, Inventory.Application.WarehouseConnections, Inventory.Infrastructure.IntegrationEventHandlers, Inventory.Domain.WarehouseConnections.Errors, Inventory.Infrastructure.InterModuleRequestHandlers, Inventory.Domain.StockCounters, Inventory.Application.ProductSummaries (+14 more)

### Community 214 - "SmsOtpDispatchOutcome"
Cohesion: 0.33
Nodes (5): OtpDispatchErrors, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled

### Community 215 - "Response"
Cohesion: 0.17
Nodes (10): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, Endpoint, Response, ClientId, DeviceName, Id, IpAddress, IsCurrent (+2 more)

### Community 216 - "IInventoryDbContext"
Cohesion: 0.06
Nodes (20): PersistenceQueryableExtensions, IInventoryDbContext, ProductSummaries, StockBulkAdjustJobs, StockLevels, StockMovements, StockPushDirtyItems, StockReservations (+12 more)

### Community 217 - "FeatureFlags"
Cohesion: 0.29
Nodes (6): Feature flags, Checkout, FeatureFlags, IAM, Notifications, Products

### Community 218 - "Request"
Cohesion: 0.25
Nodes (8): Request, ClientId, DeviceId, DeviceName, Otp, PhoneNumber, PushToken, RequestValidator

### Community 219 - "TokenResponseRepresentation"
Cohesion: 0.33
Nodes (6): TokenResponseRepresentation, AccessToken, ExpiresIn, RefreshExpiresIn, RefreshToken, SessionState

### Community 220 - "SendForEmail/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForEmail, Request, CaptchaToken, Email, RequestValidator

### Community 221 - "ReleaseReconcileOutcome"
Cohesion: 0.20
Nodes (8): Inventory.Endpoints.StockReservations.v1.ReconcileRelease, Endpoint, Response, Outcome, ReleaseReconcileOutcome, AlreadyReleased, NotReleased, Released

### Community 222 - "StockBulkAdjustJobRunner"
Cohesion: 0.23
Nodes (5): StockBulkAdjustJobId, RowOutcome, Adjusted, Rejected, StockBulkAdjustJobRunner

### Community 223 - "Response"
Cohesion: 0.29
Nodes (4): Products.Endpoints.Stores.v1.Create, Setup, Response, Id

### Community 225 - "Request"
Cohesion: 0.24
Nodes (9): Inventory.Endpoints.StockReservations.v1.ResolveReadOnly, Request, Body, Id, RequestBody, ProviderReference, TargetStatus, RequestBodyValidator (+1 more)

### Community 226 - "Split-Deployment PoC"
Cohesion: 0.33
Nodes (6): Concurrent safety, Files added by this PoC, How to run, Split-Deployment PoC, What this proves, What to check

### Community 227 - "Request"
Cohesion: 0.25
Nodes (8): Request, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name, SearchTerm, RequestValidator

### Community 228 - "GetImport/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Products.v1.GetImport, Request, Id, RequestValidator

### Community 229 - "Suspend/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Stores.v1.Suspend, Request, Id, RequestValidator

### Community 230 - "Stores/v1/Deactivate/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Stores.v1.Deactivate, Request, Id, RequestValidator

### Community 231 - "Key decisions"
Cohesion: 0.29
Nodes (7): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Key decisions

### Community 232 - "Approve/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Stores.v1.Approve, Request, Id, RequestValidator

### Community 233 - "v1/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Probe.v1, Request, Count, RequestValidator

### Community 234 - "My/RemoveProduct/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Stores.v1.My.RemoveProduct, Request, Id, RequestValidator

### Community 235 - "WarehouseConnections/v1/Deactivate/Request.cs"
Cohesion: 0.50
Nodes (4): Inventory.Endpoints.WarehouseConnections.v1.Deactivate, Request, Id, RequestValidator

### Community 236 - "How it works"
Cohesion: 0.33
Nodes (6): Cross-module term search, How it works, One-time setup _(Build, in a migration)_, Query path, Ranking, Write path

### Community 237 - "UpdateCurrentPushToken/Request.cs"
Cohesion: 0.50
Nodes (4): Notifications.Infrastructure.Devices.UpdateCurrentPushToken, Request, PushToken, RequestValidator

### Community 238 - "Request"
Cohesion: 0.25
Nodes (7): Constants, Request, Brand, Color, Model, SearchTerm, RequestValidator

### Community 239 - "Revoke/Request.cs"
Cohesion: 0.50
Nodes (4): IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, Request, Id, RequestValidator

### Community 242 - "Response"
Cohesion: 0.29
Nodes (5): Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 243 - "ObjectStorageOptions.cs"
Cohesion: 0.40
Nodes (4): ObjectStorageOptionsValidator, ObjectStorageRetryMode, Adaptive, Standard

### Community 244 - "RowOutcome"
Cohesion: 0.40
Nodes (5): RowOutcome, Created, Rejected, Unchanged, Updated

### Community 245 - "DevicesOptions"
Cohesion: 0.40
Nodes (5): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator

### Community 246 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.04
Nodes (24): Products.Endpoints.Stores.v1.GetApplication, Common.InterModuleRequests, Common.Domain.StronglyTypedIds, Products.Endpoints.Stores.v1.Search, Notifications.Infrastructure.Email.Brevo, Common.Application.AuditLog, Products.Endpoints.Stores.v1.ListApplications, IAM.Domain.Captcha (+16 more)

### Community 249 - "Request"
Cohesion: 0.40
Nodes (5): Request, Brand, Color, Model, RequestValidator

### Community 250 - "Request"
Cohesion: 0.22
Nodes (9): Request, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken, Password, PushToken (+1 more)

### Community 251 - "Response"
Cohesion: 0.40
Nodes (3): Endpoint, Response, Id

### Community 252 - "VersionNeutral/Get/Request.cs"
Cohesion: 0.67
Nodes (3): Request, Id, RequestValidator

### Community 266 - "Platform building blocks"
Cohesion: 0.18
Nodes (9): Authorization, Caching, Dirty set and lease, Object storage, Observability, Options and configuration, Platform building blocks, Results and errors (+1 more)

### Community 269 - "RouteGroupBuilder"
Cohesion: 0.03
Nodes (36): Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Setup, Endpoint (+28 more)

### Community 285 - "StoreId"
Cohesion: 0.04
Nodes (39): Products.Domain.Stores.DomainEvents.v2, Products.Application.Stores.DomainEventHandlers.v1, Products.Domain.Stores.DomainEvents.v1, File map _(Build)_, ISearchLocalized, Language, ProductExportJob, FileName (+31 more)

### Community 323 - "microsoft_aspnetcore_mvc"
Cohesion: 0.03
Nodes (59): Inventory.Endpoints.StockLevels.v1.GetBulkAdjustment, Products.Endpoints.ProductTemplates.v1.Deactivate, Products.Endpoints.Products.v1.DownloadExport, Inventory.Endpoints.WarehouseConnections.v1.Get, Inventory.Endpoints.StockReservations.v1.Release, Products.Endpoints.Products.v1.GetExport, Common.Application.ModelBinders, Products.Endpoints.ProductTemplates.v1.Activate (+51 more)

### Community 324 - "Request"
Cohesion: 0.20
Nodes (11): Products.Endpoints.Products.v1.My.SetPackageDimensions, Request, Body, Id, RequestBody, HeightCm, LengthCm, WeightKg (+3 more)

## Knowledge Gaps
- **1292 isolated node(s):** `UserId`, `Id`, `IdAsString`, `Roles`, `SessionId` (+1287 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1712 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **123 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Result` connect `Result` to `StockLevelId`, `DateTimeOffset`, `JobStatus`, `ApplicationUserId`, `IResult`, `ReCaptchaService`, `InterModuleRequestHandler`, `IInterModuleRequestClient`, `NetGsmSmsGateway`, `StoreId`, `Error`, `ResultTelemetryExtensions`, `PaginationResponse`, `ProductImportJobRunner`, `Product`, `TimeProvider`, `SmsOptions`, `FirebasePushGateway`, `BrevoEmailGateway`, `EmailOtpDispatchOutcome`, `PackageDimensions`, `IIntegrationEventOutbox`, `ProductTemplateId`, `SmsOtpDispatchOutcome`, `IInventoryDbContext`, `SecretProtector`, `Task`, `WarehouseHttpGateway`, `SearchProductsByTermRequestHandler`, `WarehouseConnectionId`?**
  _High betweenness centrality (0.062) - this node is a cross-community bridge._
- **What connects `UserId`, `Id`, `IdAsString` to the rest of the system?**
  _1292 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `StockLevelId` be split into smaller, more focused modules?**
  _Cohesion score 0.05365402405180388 - nodes in this community are weakly interconnected._
- **Why does `Common.Application.Options` connect `Common.Application.Options` to `system_globalization`, `IServiceCollection`, `StockJobRequeueService`, `microsoft_extensions_dependencyinjection`, `Common.Domain.ResultMonad`, `KeycloakPermissionAuthorizationHandler.cs`, `EmailOptions`, `FullTextSearchOptions`, `InventoryOptions`, `BackgroundJobsOptions`, `fluentvalidation`, `microsoft_entityframeworkcore`, `ObservabilityOptions`, `OpenApiOptions`, `BoundedCaptureStream`, `CachingOptions.cs`, `CorsOptions`, `CaptchaOptions`, `RabbitMqOptions`, `SmsOptions`, `SecretProtectionOptions`, `CustomValidator`, `SignalROptions`, `Setup.Logger.cs`, `OpenApiOptions`, `KeycloakOptions`, `Common.Domain.ResultMonad`, `PushOptions`, `AlertRecipientsOptions`, `Response`, `ResiliencyOptions`, `Common.InterModuleRequests.Contracts`, `OtpOptions`, `ObjectStorageOptions.cs`, `DevicesOptions`, `Common.Domain.StronglyTypedIds`, `system_diagnostics`, `ProductsOptions`?**
  _High betweenness centrality (0.056) - this node is a cross-community bridge._
- **Should `DateTimeOffset` be split into smaller, more focused modules?**
  _Cohesion score 0.043440636898580826 - nodes in this community are weakly interconnected._
- **Why does `Common.Domain.ResultMonad` connect `Common.Domain.ResultMonad` to `system_globalization`, `StockLevelErrors.cs`, `microsoft_entityframeworkcore`, `microsoft_extensions_dependencyinjection`, `Common.InterModuleRequests.Contracts`, `Result`, `IResult`, `Common.Application.Options`, `SmsOptions`, `Common.Domain.ResultMonad`, `Common.Domain.StronglyTypedIds`, `FirebasePushGateway`, `system_diagnostics`, `Error`?**
  _High betweenness centrality (0.039) - this node is a cross-community bridge._
- **Should `HttpContext` be split into smaller, more focused modules?**
  _Cohesion score 0.08658536585365853 - nodes in this community are weakly interconnected._