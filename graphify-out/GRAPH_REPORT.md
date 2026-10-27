# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-10-27)

## Corpus Check
- 746 files · ~133,898 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 5006 nodes · 14072 edges · 335 communities (207 shown, 128 thin omitted)
- Extraction: 96% EXTRACTED · 4% INFERRED · 0% AMBIGUOUS · INFERRED: 534 edges (avg confidence: 0.86)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `b53374f3`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- PaginationResponse
- ProductsModule
- HttpContext
- AuditableEntityResponse
- ApplicationUserId
- WarehouseConnection
- .AddModuleDbContext
- IOptions
- Host.Swagger
- Result
- Common.Domain.ResultMonad
- ProductImage
- ResxLocalizationOptions
- Response
- KeycloakPermissionAuthorizationHandler
- EmailOptions
- Common.Domain.StronglyTypedIds
- Common.Application.Options
- microsoft_extensions_options
- FullTextSearchOptions
- BoundedRequestCaptureStream
- DeviceRegistration
- DeviceRegistryReconciliationService
- .RegisterAsync
- InventoryOptions
- Response
- RequestResponseBodyLoggingMiddleware
- fluentvalidation
- ISearchLanguageResolver
- Error
- OutboxProcessor
- WebhookDelivery
- Response
- DatabaseSeederOrchestrator
- DateTimeOffset
- CustomRateLimitingOptions
- AdminRepresentations.cs
- microsoft_entityframeworkcore
- ObservabilityOptions
- NotifyRoleOnCriticalAlertRaisedHandler
- ProductSummariesReconciliationJob
- S3ObjectStoreCore.cs
- SmsOtpDispatchOutcome
- .SaveBatchAsync
- OutboxModule
- KeyedRow
- Outbox Misuse Check
- StockReservationExpirySweepJobRegistrar
- Add Integration Event Command
- host
- BackgroundJobsOptions
- CaptchaOptions
- KeyedResiliencePipelines
- RedisFixedWindowRateLimiter
- BrevoEmailGateway
- FirebasePushGateway
- Cross-Module Reference Violation
- OutboxOptions.cs
- Request
- Bogus Test Data
- PaginationQueryableExtensions
- Seeder
- RequestLoggingOptions
- S3ObjectStoreCore
- Task
- .EnsureNoMigrationsPending
- .AddNotificationsSignalR
- ValueObject
- Request
- IntegrationEventHandlerBase
- Getting started
- Inventory.Application.Gateway
- FormApiWarehouseGateway
- AsNoTracking Coverage Check
- ProductTemplateId
- Response
- ProvisionUserRequestHandler
- KeycloakOptions
- Response
- EmailOtpDispatchOutcome
- Response
- .AddDeviceRegistryReconciliation
- OtpOptions
- StoreId
- StockReservationId
- InventoryDbContext
- PushOptions
- Configuration-Driven Module Registration
- ServiceAccountTokenCache
- Setup
- ProductExportJobRunner
- JobStatus
- .MapEndpoint
- NotificationsHub
- IModule
- ILogger
- ProductImportJobRunner
- Response
- ServiceAccountTokenCache
- Task
- ResiliencyOptions
- IFusionCache
- Stores/v1/Update/Request.cs
- ResultToResponseTransformer.cs
- Response
- Common.InterModuleRequests.Contracts
- WarehouseConnectionResponse
- Products/v1/My/Update/Request.cs
- Response
- KeycloakPermissionPolicyProvider
- .UpsertIfNewerAsync
- Response
- Response
- DefaultIdType
- DefaultResponsesOperationFilter
- Response
- SkipOverlappingRecurringJobFilter
- depends_on
- healthcheck
- mm.keycloak
- mm.redis
- Response
- system_diagnostics
- ProductsOptions
- Pagination and search
- Response
- common_application_localization_resources
- Result
- docker-compose.yml (Base Stack)
- IServiceCollection
- ObjectStorageOptions
- ProductImportJob
- ProjectionReconciliationJob
- AddImage/Request.cs
- Policies
- services
- mm.postgres
- StockLevel
- JsonSerializerOptions
- mm.rabbitmq
- DateOnly
- Adjust/Request.cs
- OpenApiOptions
- Response
- ProductsDbContext
- .ResolveGatewayAsync
- ReCaptchaService
- ConcurrencyConfiguratorExtensions
- ConfigureSwaggerOptions
- ReplaceCredentials/Request.cs
- WebhookCallback/Request.cs
- IdentityScheme
- JwtBearerConfigureOptions
- InterModuleRequestOptions
- AlertRecipientsOptions
- BackgroundJobsModule
- Stores/v1/Create/Request.cs
- .ToFields
- KeycloakPermission
- Response
- Request
- SearchProductsByTermRequestHandler
- ApplicationUserId
- NotificationPayload
- WarehouseHttpGateway
- ProductTemplates/v1/Search/Request.cs
- .GetProductAsync
- Notifications.Application/IAssemblyReference.cs
- StringExtensions
- DomainEvent
- Response
- CachingOptions.cs
- IInterModuleRequest
- PaginationCursor
- CorsOptions
- Response
- CachingEntryDefaults
- SmsOptions
- RabbitMqOptions
- IAM.Domain
- SendRequestBody
- SendRequestBody
- Response
- ReCaptchaResponse
- CustomValidator
- BoundedCaptureStream
- V1StoreCreatedDomainEvent
- IOtpService
- Setup.Logger.cs
- ResultTelemetryExtensions
- BulkImportOptions
- Response
- .HandleWarehouseWebhookAsync
- Response
- StronglyTypedIdBinder.cs
- Request
- ActivitySource
- KeycloakScopes
- DevicesOptions
- Response
- Key decisions
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- Type
- GetExport/Request.cs
- Response
- Response
- RowOutcome
- Response
- RemoveImage/Request.cs
- Platform building blocks
- Response
- Response
- .Capture
- HttpClient
- StronglyTypedIdSchemaFilter.cs
- .GlobalRateLimiter
- RouteHandlerBuilder
- HealthCheckOptions
- ReverseProxyOptions
- SignalROptions
- PolymorphicEventConverter
- IAM.Endpoints
- Notifications.Infrastructure
- SmsCategory
- RequestBody
- WarehouseConnections/v1/Get/Request.cs
- WarehouseConnections/v1/Deactivate/Request.cs
- Activate/Request.cs
- NetGsmSmsGateway
- RowResult
- RequestBodyLimitMetadata
- ProjectionReconciliationOptions
- SecretProtectionOptions.cs
- DeprovisionUserRequest
- Response
- IssueVerificationTokenRequestHandler
- ValidationContextExtensions
- My/Get/Request.cs
- StockLevelErrors.cs
- Inventory.Domain.WarehouseConnections.Errors
- Inventory.Domain.StockReservations.Errors
- Tokens/VersionNeutral/Setup.cs
- Users/VersionNeutral/Setup.cs
- Response
- IResult
- Products.Domain/IAssemblyReference.cs
- IAM.Application/IAssemblyReference.cs
- Products.Infrastructure/IAssemblyReference.cs
- Products.Application/IAssemblyReference.cs
- Common.Infrastructure/IAssemblyReference.cs
- Notifications.Domain/IAssemblyReference.cs
- IAM.Infrastructure/IAssemblyReference.cs
- .ReadRows
- Setup
- Endpoint
- ReCaptchaResponse
- RouteGroupBuilder
- InterModuleRequestHandler
- SendForEmail/Request.cs
- AuditLogRetentionService
- Products.Endpoints
- Products/v1/Update/Request.cs
- WebhookCallback/Endpoint.cs
- Common.Application.Validation
- RequestBody
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
1. `Result` - 229 edges
2. `Common.Application.Options` - 172 edges
3. `Common.Domain.ResultMonad` - 145 edges
4. `CustomValidator` - 107 edges
5. `ApplicationUserId` - 96 edges
6. `Common.Application.Validation` - 93 edges
7. `Common.Application.Auth` - 91 edges
8. `Common.Domain.StronglyTypedIds` - 86 edges
9. `Common.Application.Extensions` - 84 edges
10. `Common.Infrastructure.Persistence.Extensions` - 70 edges

## Surprising Connections (you probably didn't know these)
- `Authorization` --references--> `ICurrentUser`  [INFERRED]
  docs/platform.md → src/Common/Common.Application/Auth/ICurrentUser.cs
- `The commands` --references--> `IModule`  [INFERRED]
  docs/ai-workflow.md → src/Common/Common.Infrastructure/Modules/IModule.cs
- `Adding a module or a feature` --references--> `IModule`  [INFERRED]
  docs/modules.md → src/Common/Common.Infrastructure/Modules/IModule.cs
- `Watch two modules cooperate` --references--> `ProductCreatedIntegrationEvent`  [INFERRED]
  docs/getting-started.md → src/Common/Common.IntegrationEvents/Products.cs
- `Results and errors` --references--> `GlobalExceptionHandlingMiddleware`  [INFERRED]
  docs/platform.md → src/Host/Host/Middlewares/GlobalExceptionHandlingMiddleware.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Local Infrastructure Stack** — docker_compose_postgres, docker_compose_rabbitmq, docker_compose_redis, docker_compose_aspire_dashboard [EXTRACTED 1.00]

## Communities (335 total, 128 thin omitted)

### Community 0 - "PaginationResponse"
Cohesion: 0.09
Nodes (19): Products.Endpoints.ProductTemplates.v1.Search, PaginationResponse, HasNext, HasPrevious, HasTotal, NextPageNumber, PreviousPageNumber, TotalPages (+11 more)

### Community 1 - "ProductsModule"
Cohesion: 0.15
Nodes (7): Setup, ProductsModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority

### Community 2 - "HttpContext"
Cohesion: 0.14
Nodes (11): Rate limiting and resilience, FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit, RequestBodyLimitEndpointExtensions, RequestBodyLimitMetadata (+3 more)

### Community 3 - "AuditableEntityResponse"
Cohesion: 0.08
Nodes (21): Common.Application.DTOs, Products.Endpoints.Products.v1.My.Search, Products.Endpoints.ProductTemplates.v1.Get, Products.Endpoints.Products.v1.Get, AuditableEntityResponse, CreatedBy, CreatedOn, Id (+13 more)

### Community 5 - "ApplicationUserId"
Cohesion: 0.13
Nodes (16): GrantedPermission, KeycloakUser, KeycloakUserPage, KeycloakUserSession, KeycloakAdminClient, UserRepresentation, Attributes, CreatedTimestamp (+8 more)

### Community 6 - "WarehouseConnection"
Cohesion: 0.06
Nodes (26): Inventory.Domain.WarehouseConnections.DomainEvents.v1, ProviderSnapshot, FormApi, JsonApi, V1WarehouseConnectionCreatedDomainEvent, V1WarehouseConnectionCreatedDomainEventExtensions, V1WarehouseConnectionCredentialsReplacedDomainEvent, V1WarehouseConnectionDeactivatedDomainEvent (+18 more)

### Community 7 - ".AddModuleDbContext"
Cohesion: 0.18
Nodes (5): Common.Infrastructure.Persistence.Auditing, ApplyAuditingInterceptor, ApplySearchLanguageInterceptor, Setup, Setup

### Community 8 - "IOptions"
Cohesion: 0.35
Nodes (9): JobHousekeepingJob, JobHousekeepingOptions, PageSize, RetentionHours, StaleAfterMinutes, ProductExportJobHousekeeping, ProductImportJobHousekeeping, ProductJobsHousekeeping (+1 more)

### Community 9 - "Host.Swagger"
Cohesion: 0.18
Nodes (4): Host, Host.Swagger, Program, Setup

### Community 10 - "Result"
Cohesion: 0.06
Nodes (10): CollectionExtensions, FeatureFlagResultExtensions, Result, Error, IsFailure, Success, AsyncExtensions, SyncExtensions (+2 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.08
Nodes (31): IAM.Endpoints.Captcha.VersionNeutral, Products.Infrastructure.InterModuleRequestHandlers, Common.Application.Search, Products.Application.Exports, Products.Endpoints.Stores, Products.Infrastructure.Imports, Products.Application.Imports, Common.Application.AuditLog (+23 more)

### Community 12 - "ProductImage"
Cohesion: 0.21
Nodes (7): ProductImage, ContentType, ObjectKey, ProductId, SizeBytes, ProductImageId, ProductImageConfiguration

### Community 13 - "ResxLocalizationOptions"
Cohesion: 0.40
Nodes (5): Localization, ResxLocalizationOptions, DefaultCulture, SupportedCultures, ResxLocalizationOptionsValidator

### Community 14 - "Response"
Cohesion: 0.13
Nodes (13): IAM.Endpoints.Users.VersionNeutral.Me.Get, Endpoint, Response, BirthDate, CreatedOn, Email, FirstName, Id (+5 more)

### Community 15 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.21
Nodes (4): HttpContextTargetingContextAccessor, KeycloakPermissionAuthorizationHandler, KeycloakPermissionRequirement, Permission

### Community 16 - "EmailOptions"
Cohesion: 0.10
Nodes (23): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+15 more)

### Community 17 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.06
Nodes (24): Common.Domain.StronglyTypedIds, Common.Application.JsonConverters, Common.Domain.Entities, Common.Domain.Aggregates, IAggregateRoot, Events, Id, Version (+16 more)

### Community 18 - "Common.Application.Options"
Cohesion: 0.03
Nodes (39): Common.Infrastructure.Modules, Notifications.Application.Sms, Notifications.Infrastructure.Devices, Inventory.Endpoints, Common.Endpoints.Versioning, Notifications.Infrastructure.Email, Products.Endpoints.Probe, Notifications.Infrastructure.Alerts (+31 more)

### Community 19 - "microsoft_extensions_options"
Cohesion: 0.08
Nodes (18): Inventory.Application.WarehouseConnections, Inventory.Infrastructure.IntegrationEventHandlers, Common.Application.Persistence.Inbox, Inventory.Application.Persistence, Inventory.Application.ProductSummaries, Inventory.Application.IntegrationEventHandlers, Common.IntegrationEvents, Common.Application.Persistence.Projections (+10 more)

### Community 20 - "FullTextSearchOptions"
Cohesion: 0.08
Nodes (26): Add a new language/culture, Add search to a new entity _(Build checklist)_, Change the vector (weights, fields, or config), Extending and maintaining, File map _(Build)_, Full-Text Search, Gotchas, How it works (+18 more)

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.18
Nodes (6): BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 22 - "DeviceRegistration"
Cohesion: 0.12
Nodes (12): DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive, LastReconciledOn, PushToken, PushTokenUpdatedOn (+4 more)

### Community 23 - "DeviceRegistryReconciliationService"
Cohesion: 0.38
Nodes (5): GetActiveSessionIdsRequest, GetActiveSessionIdsResponse, UserSessionIds, GetActiveSessionIdsRequestHandler, DeviceRegistryReconciliationService

### Community 24 - ".RegisterAsync"
Cohesion: 0.06
Nodes (29): IInterModuleRequest, IInterModuleRequestClient, BindDeviceSessionRequest, BindDeviceSessionResponse, DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse, VerifyEmailOtpRequest, VerifyEmailOtpResponse (+21 more)

### Community 25 - "InventoryOptions"
Cohesion: 0.12
Nodes (14): InventoryOptions, BusinessTimeZoneId, MaxOccurrencesPerSeries, ProductSummaryReconciliationCron, SafetyBufferQuantity, StockReservationExpirySweepBatchSize, StockReservationExpirySweepCron, WebhookDeliveryRetentionCron (+6 more)

### Community 26 - "Response"
Cohesion: 0.29
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, Endpoint, Response, EmailVerificationToken, IsRegistered

### Community 28 - "fluentvalidation"
Cohesion: 0.08
Nodes (31): Products.Endpoints.Stores.v1.My.Update, IAM.Domain.Users, IAM.Endpoints.Otp.VersionNeutral.SendForLogin, Common.Domain.Extensions, Notifications.Infrastructure.Devices.UpdateCurrentPushToken, Common.Application.Validation, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, Common.Domain.Devices (+23 more)

### Community 29 - "ISearchLanguageResolver"
Cohesion: 0.24
Nodes (6): Configuration, ISearchLanguageResolver, UniversalConfig, SearchLanguageResolver, UniversalConfig, Setup

### Community 30 - "Error"
Cohesion: 0.10
Nodes (18): Error, Key, ParameterName, StatusCode, SubErrors, Value, CaptchaErrors, IdentityErrors (+10 more)

### Community 31 - "OutboxProcessor"
Cohesion: 0.18
Nodes (5): OutboxProcessor, PublishOutcome, Failed, Published, Skipped

### Community 32 - "WebhookDelivery"
Cohesion: 0.10
Nodes (8): StronglyTypedIdHelper, WebhookDelivery, ConnectionId, DeliveryId, ReceivedOn, ReservationId, WebhookDeliveryId, WebhookDeliveryConfiguration

### Community 33 - "Response"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.Refresh, Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 34 - "DatabaseSeederOrchestrator"
Cohesion: 0.15
Nodes (6): Persistence conventions, IDatabaseSeeder, Priority, DatabaseSeederOrchestrator, SeedingCompletionTracker, Setup

### Community 35 - "DateTimeOffset"
Cohesion: 0.05
Nodes (35): JobRow, FailureKey, FinishedOn, Progress, QueuedOn, RequestedBy, StartedOn, Status (+27 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.05
Nodes (27): CustomRateLimitingOptions, CheckRegistration, CreateStore, Email, ExemptPathPrefixes, Global, OtpVerify, Register (+19 more)

### Community 37 - "AdminRepresentations.cs"
Cohesion: 0.11
Nodes (17): CredentialRepresentation, Temporary, Type, Value, ErrorRepresentation, Error, ErrorDescription, ErrorMessage (+9 more)

### Community 38 - "microsoft_entityframeworkcore"
Cohesion: 0.05
Nodes (32): Common.Infrastructure.Persistence.Inbox, Inventory.Domain.WebhookDeliveries, Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Inventory.Infrastructure.InterModuleRequestHandlers, Inventory.Domain.StockCounters, Common.Application.Persistence, Outbox (+24 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.08
Nodes (18): ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics, EnableTracing, LogSink, MinimumLevel (+10 more)

### Community 40 - "NotifyRoleOnCriticalAlertRaisedHandler"
Cohesion: 0.24
Nodes (3): CriticalAlertRaisedIntegrationEvent, EmailMessage, NotifyRoleOnCriticalAlertRaisedHandler

### Community 41 - "ProductSummariesReconciliationJob"
Cohesion: 0.47
Nodes (6): GetProductsPageRequest, GetProductsPageResponse, ProductPageItem, ProductSummariesReconciliationCursor, ProductSummariesReconciliationJob, GetProductsPageRequestHandler

### Community 43 - "SmsOtpDispatchOutcome"
Cohesion: 0.29
Nodes (8): OtpDispatchErrors, SendPhoneOtpRequest, SendPhoneOtpResponse, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, SendPhoneOtpRequestHandler

### Community 44 - ".SaveBatchAsync"
Cohesion: 0.28
Nodes (5): ProductImportRowError, BatchSummary, ProductRow, RowParser, SheetRow

### Community 45 - "OutboxModule"
Cohesion: 0.05
Nodes (32): Adding a module or a feature, How modules fit together, IAM, Modules, Notifications, Outbox, OutboxOptions, BaseBackoffSeconds (+24 more)

### Community 46 - "KeyedRow"
Cohesion: 0.50
Nodes (4): KeyedRow, Item, Sort, Tie

### Community 48 - "StockReservationExpirySweepJobRegistrar"
Cohesion: 0.15
Nodes (5): Setup, StockReservationExpirySweepJobRegistrar, AlertRecipientsReconcileJobRegistrar, DeviceRegistryReconcileJobRegistrar, ProductJobsHousekeepingRegistrar

### Community 50 - "host"
Cohesion: 0.18
Nodes (11): context, dockerfile, build, container_name, environment, image, networks, ports (+3 more)

### Community 51 - "BackgroundJobsOptions"
Cohesion: 0.15
Nodes (8): BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount, BackgroundJobsOptionsValidator, HangfireCustomAuthorizationFilter

### Community 52 - "CaptchaOptions"
Cohesion: 0.14
Nodes (14): CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey, Provider, ScoreThreshold, SecretKey (+6 more)

### Community 54 - "RedisFixedWindowRateLimiter"
Cohesion: 0.19
Nodes (6): FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration, RetryAfterHeaderExtensions

### Community 55 - "BrevoEmailGateway"
Cohesion: 0.11
Nodes (13): IEmailGateway, BrevoEmailGateway, SendContact, Email, Name, SendErrorBody, Code, Message (+5 more)

### Community 56 - "FirebasePushGateway"
Cohesion: 0.20
Nodes (3): IPushGateway, DummyPushGateway, FirebasePushGateway

### Community 58 - "OutboxOptions.cs"
Cohesion: 0.32
Nodes (7): OutboxCleanupSettings, BatchSize, CronSchedule, Enabled, RetentionDays, OutboxCleanupSettingsValidator, OutboxOptionsValidator

### Community 59 - "Request"
Cohesion: 0.15
Nodes (13): Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken (+5 more)

### Community 61 - "PaginationQueryableExtensions"
Cohesion: 0.17
Nodes (4): CursorBound, Value, PaginationQueryableExtensions, ParameterReplacer

### Community 62 - "Seeder"
Cohesion: 0.18
Nodes (6): Products.Infrastructure.Persistence.Seeding, GetSeedUserIdsRequest, GetSeedUserIdsResponse, GetSeedUserIdsRequestHandler, Endpoint, Seeder

### Community 63 - "RequestLoggingOptions"
Cohesion: 0.13
Nodes (15): RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes, SensitiveQueryParamPaths (+7 more)

### Community 64 - "S3ObjectStoreCore"
Cohesion: 0.05
Nodes (16): Object storage, IPrivateObjectStore, IPublicObjectStore, ObjectListItem, ObjectListPage, ObjectMetadata, UploadObjectRequest, S3ObjectStoreCore (+8 more)

### Community 65 - "Task"
Cohesion: 0.09
Nodes (4): IBackgroundJobs, IRecurringBackgroundJobs, BackgroundJobsService, RecurringBackgroundJobsService

### Community 66 - ".EnsureNoMigrationsPending"
Cohesion: 0.39
Nodes (3): AutoMigrateMarker, IAutoMigrateMarker, MigrationGuard

### Community 69 - "Request"
Cohesion: 0.17
Nodes (12): Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FirstName, LastName (+4 more)

### Community 70 - "IntegrationEventHandlerBase"
Cohesion: 0.08
Nodes (20): IntegrationEventHandlerBase, MaxEventAge, CachingOptions, AllowInMemoryOnlyInProduction, EntryDefaults, IdempotencyKeyDuration, IdempotencyL1Duration, MemoryCacheSizeLimit (+12 more)

### Community 71 - "Getting started"
Cohesion: 0.05
Nodes (40): Project Instructions (CLAUDE.md), Adapting it, AI-assisted development workflow, `CLAUDE.md`, Guards, The commands, The four layers, The issue-to-pull-request automation (+32 more)

### Community 73 - "FormApiWarehouseGateway"
Cohesion: 0.21
Nodes (6): FormApiWarehouseGateway, HoldEnvelope, HoldResult, JsonApiWarehouseGateway, ReleaseRequestBody, ReleaseResponseBody

### Community 75 - "ProductTemplateId"
Cohesion: 0.08
Nodes (19): DomainEventHandlerBase, IEventHandler, IEventHandlerWrapper, IIntegrationEventOutbox, StockLowThresholdCrossedIntegrationEventPublishingHandler, StockLowThresholdRecoveredIntegrationEventPublishingHandler, StockReservationCommittedIntegrationEventPublishingHandler, ProductChangedIntegrationEventPublishingHandler (+11 more)

### Community 76 - "Response"
Cohesion: 0.20
Nodes (8): Products.Endpoints.Stores.v1.My.Get, Endpoint, Response, Address, Description, Name, OwnerId, ProductCount

### Community 77 - "ProvisionUserRequestHandler"
Cohesion: 0.27
Nodes (9): ProvisionUserFailureReason, EmailAlreadyRegistered, IdentityProviderUnavailable, None, PhoneNumberAlreadyRegistered, Rejected, ProvisionUserRequest, ProvisionUserResponse (+1 more)

### Community 78 - "KeycloakOptions"
Cohesion: 0.10
Nodes (19): KeycloakOptions, AttemptTimeoutSeconds, Authority, BaseUrl, DecisionCacheMaxDurationSeconds, Realm, RequireHttpsMetadata, ResourceClientId (+11 more)

### Community 79 - "Response"
Cohesion: 0.14
Nodes (12): IAM.Endpoints.Users.VersionNeutral.Search, Endpoint, Response, BirthDate, CreatedOn, Email, Enabled, FirstName (+4 more)

### Community 80 - "EmailOtpDispatchOutcome"
Cohesion: 0.33
Nodes (5): EmailOtpDispatchErrors, EmailOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled

### Community 81 - "Response"
Cohesion: 0.20
Nodes (8): Products.Endpoints.Stores.v1.Search, Endpoint, Response, Address, Description, Name, OwnerId, ProductCount

### Community 83 - "OtpOptions"
Cohesion: 0.14
Nodes (13): OtpOptions, DummyCode, EmailQuotaWindowMinutes, ExpirationInMinutes, Length, MaxSendsPerEmailPerWindow, MaxSendsPerPhonePerWindow, PhoneQuotaWindowMinutes (+5 more)

### Community 84 - "StoreId"
Cohesion: 0.10
Nodes (18): AuditableEntityConfiguration, JobRowConfiguration, ProcessedMessageConfiguration, StronglyTypedIdValueConverter, StoreDailyStockTotal, CommittedReservations, CommittedUnits, Day (+10 more)

### Community 85 - "StockReservationId"
Cohesion: 0.09
Nodes (19): Inventory.Domain.StockReservations.DomainEvents.v1, Inventory, V1StockLevelCreatedDomainEvent, V1StockReservationCommitConflictDetectedDomainEvent, V1StockReservationCommittedDomainEvent, V1StockReservationExpiredDomainEvent, V1StockReservationReleaseAttemptAbandonedDomainEvent, V1StockReservationReleaseAttemptStartedDomainEvent (+11 more)

### Community 86 - "InventoryDbContext"
Cohesion: 0.04
Nodes (40): Transactional outbox and inbox, IdBound, Value, JobClaimExtensions, IDbContext, AuditLog, ChangeTracker, Database (+32 more)

### Community 87 - "PushOptions"
Cohesion: 0.12
Nodes (20): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri, Type (+12 more)

### Community 89 - "ServiceAccountTokenCache"
Cohesion: 0.17
Nodes (5): IServiceAccountTokenProvider, ServiceAccountTokenCache, AccessToken, ExpiresAt, ServiceAccountTokenProvider

### Community 91 - "ProductExportJobRunner"
Cohesion: 0.31
Nodes (4): ProductExportJobId, ExportPageRequest, ExportRow, ProductExportJobRunner

### Community 92 - "JobStatus"
Cohesion: 0.29
Nodes (6): JobStatus, Failed, Queued, Running, Succeeded, FileRow

### Community 93 - ".MapEndpoint"
Cohesion: 0.29
Nodes (4): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, Response, ClientKey, Setup

### Community 95 - "IModule"
Cohesion: 0.14
Nodes (12): IModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority, ModuleRegistry, NotificationsModule (+4 more)

### Community 96 - "ILogger"
Cohesion: 0.10
Nodes (7): EventDispatcher, OutboxSaveHelper, GlobalExceptionHandlingMiddleware, LoginCompletion, RegistrationCompletion, ProductSummariesReconciliationJobRegistrar, StockReservationExpirySweepService

### Community 97 - "ProductImportJobRunner"
Cohesion: 0.20
Nodes (5): ProductImportRequestedIntegrationEvent, ProductImportJobId, ProductImportJobRunner, Slice, RunProductImportJobHandler

### Community 98 - "Response"
Cohesion: 0.11
Nodes (16): Endpoint, Response, FailureKey, FileName, FinishedOn, JobId, ProcessedRows, ProductsCreated (+8 more)

### Community 99 - "ServiceAccountTokenCache"
Cohesion: 0.07
Nodes (22): SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, KeycloakPaths, KeycloakPermissionClient, KeycloakTokenClient, DecisionRepresentation (+14 more)

### Community 100 - "Task"
Cohesion: 0.03
Nodes (33): AuditLogDto, ICurrentUser, Id, IdAsString, Roles, SessionId, OtpCacheEntry, IVariantFeatureManagerExtensions (+25 more)

### Community 101 - "ResiliencyOptions"
Cohesion: 0.09
Nodes (23): KeyedResilienceProfile, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, RateLimitFailOpen, RateLimitPermits (+15 more)

### Community 102 - "IFusionCache"
Cohesion: 0.39
Nodes (4): ISmsGateway, DummySmsGateway, Setup, ThrottledSmsGateway

### Community 103 - "Stores/v1/Update/Request.cs"
Cohesion: 0.22
Nodes (10): Products.Endpoints.Stores.v1.Update, Request, Body, Id, RequestBody, Address, Description, Name (+2 more)

### Community 104 - "ResultToResponseTransformer.cs"
Cohesion: 0.17
Nodes (7): ProblemResponse, ResultToAcceptedResponseTransformer, ResultToCreatedResponseTransformer, ResultToResponseTransformer, ResultToResponseTransformer, StringLocalizerExtensions, RequestBodyLimitFilter

### Community 105 - "Response"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegisterByEmail, Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 106 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.05
Nodes (27): Common.Endpoints, Common.InterModuleRequests, IAM.Infrastructure.Keycloak.Representations, Products.Domain.Products.Errors, Common.Application.FeatureManagement, IAM.Endpoints.Otp, IAM.Domain.Captcha, Common.InterModuleRequests.IAM (+19 more)

### Community 107 - "WarehouseConnectionResponse"
Cohesion: 0.15
Nodes (10): WarehouseConnectionStatus, Active, Deactivated, Endpoint, Endpoint, WarehouseConnectionResponse, BaseUrl, Provider (+2 more)

### Community 108 - "Products/v1/My/Update/Request.cs"
Cohesion: 0.20
Nodes (11): Products.Endpoints.Products.v1.My.Update, Request, Body, Id, RequestBody, Description, Name, Price (+3 more)

### Community 109 - "Response"
Cohesion: 0.33
Nodes (4): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, Endpoint, Response, IsRegistered

### Community 111 - ".UpsertIfNewerAsync"
Cohesion: 0.18
Nodes (5): ProjectionUpsertExtensions, ProjectionUpsertOutcome, Inserted, Stale, Updated

### Community 113 - "Response"
Cohesion: 0.10
Nodes (18): Inventory.Endpoints.StockLevels.v1.Movements, ProblemDetailsExtensions, GetUsersRequest, GetUsersResponse, UserSummary, GetUsersRequestHandler, UserDisplayCacheEntry, Endpoint (+10 more)

### Community 114 - "Response"
Cohesion: 0.14
Nodes (12): IAM.Endpoints.Users.VersionNeutral.Get, Endpoint, Response, BirthDate, CreatedOn, Email, Enabled, FirstName (+4 more)

### Community 115 - "DefaultIdType"
Cohesion: 0.04
Nodes (46): Products, Projections and reconciliation, ProcessedMessage, ConsumerName, MessageId, ProcessedOn, ProjectionEntity, SourceVersion (+38 more)

### Community 116 - "DefaultResponsesOperationFilter"
Cohesion: 0.29
Nodes (3): DefaultResponsesOperationFilter, RemoveDefaultResponseSchemaFilter, SwaggerDefaultValues

### Community 117 - "Response"
Cohesion: 0.14
Nodes (13): ReservationStatus, Active, Committed, Expired, Released, RequiresReconciliation, Response, Id (+5 more)

### Community 118 - "SkipOverlappingRecurringJobFilter"
Cohesion: 0.27
Nodes (3): BackgroundJobs, JobMetricsFilter, SkipOverlappingRecurringJobFilter

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
Cohesion: 0.20
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.Create, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt, Setup

### Community 124 - "system_diagnostics"
Cohesion: 0.06
Nodes (8): BackgroundJobs.Telemetry, Products.Endpoints.Products.v1.Import, BackgroundJobs, Common.Infrastructure.Auth.Services, IAM.Infrastructure.Auth, Common.Infrastructure.Auth, LoginMethods, SessionRevokedReasons

### Community 125 - "ProductsOptions"
Cohesion: 0.15
Nodes (13): ProductsOptions, ExportJobConcurrentMessageLimit, ExportMaxRows, ExportPageSize, ImageMaxFileBytes, ImportBatchesPerMessage, ImportJobConcurrentMessageLimit, ImportMaxStoredRowErrors (+5 more)

### Community 126 - "Pagination and search"
Cohesion: 0.40
Nodes (3): Pagination and search, LikePattern, TrigramIndexExtensions

### Community 127 - "Response"
Cohesion: 0.20
Nodes (8): Products.Endpoints.Stores.v1.Get, Endpoint, Response, Address, Description, Name, OwnerId, ProductCount

### Community 128 - "common_application_localization_resources"
Cohesion: 0.04
Nodes (63): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Products.v1.ListImports, Inventory.Endpoints.StockLevels.v1.Search, Products.Endpoints.Stores.v1.My.AuditLog, Inventory.Endpoints.WarehouseConnections.v1.Search, Common.Application.Pagination, Products.Endpoints.Products.v1.AuditLog, PaginationRequest (+55 more)

### Community 129 - "Result"
Cohesion: 0.30
Nodes (4): Result, Error, IsFailure, Value

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.25
Nodes (10): Observability (OpenTelemetry), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), external, name, networks, local_shared_network, Postgres Service (mm.postgres) (+2 more)

### Community 131 - "IServiceCollection"
Cohesion: 0.07
Nodes (11): Setup, Setup, Setup, Setup, Setup, Setup, Setup, Setup (+3 more)

### Community 132 - "ObjectStorageOptions"
Cohesion: 0.05
Nodes (29): ObjectStorageOptions, AccessKey, AdditionalTransientStatusCodes, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds (+21 more)

### Community 134 - "ProductImportJob"
Cohesion: 0.17
Nodes (12): ProductImportJob, ClientRequestId, FileName, LastProcessedRow, ObjectKey, ProductsCreated, ProductsUpdated, ProductTemplateId (+4 more)

### Community 136 - "AddImage/Request.cs"
Cohesion: 0.20
Nodes (9): Request, File, Id, RequestValidator, Request, ClientRequestId, File, ProductTemplateId (+1 more)

### Community 137 - "Policies"
Cohesion: 0.10
Nodes (20): ProblemDetailsServiceExtensions, CheckRegistrationRateLimitingPolicy, OnRejected, Policies, EmailRateLimitingPolicy, OnRejected, OtpVerifyRateLimitingPolicy, OnRejected (+12 more)

### Community 138 - "services"
Cohesion: 0.25
Nodes (8): container_name, environment, image, networks, ports, restart, services, mm.aspire-dashboard

### Community 139 - "mm.postgres"
Cohesion: 0.25
Nodes (8): container_name, environment, image, networks, ports, restart, volumes, mm.postgres

### Community 140 - "StockLevel"
Cohesion: 0.05
Nodes (36): Inventory.Domain.StockLevels.DomainEvents.v1, AggregateRoot, Events, Id, Version, IStronglyTypedId, Value, V1StockLowThresholdChangedDomainEvent (+28 more)

### Community 141 - "JsonSerializerOptions"
Cohesion: 0.15
Nodes (5): StrictDateTimeOffsetJsonConverter, StronglyTypedIdListReadOnlyJsonConverter, StronglyTypedIdReadOnlyJsonConverter, StronglyTypedIdReadOnlyJsonConverter, StronglyTypedIdWriteOnlyJsonConverter

### Community 142 - "mm.rabbitmq"
Cohesion: 0.25
Nodes (8): container_name, environment, image, networks, ports, restart, volumes, mm.rabbitmq

### Community 144 - "Adjust/Request.cs"
Cohesion: 0.24
Nodes (9): Inventory.Endpoints.StockLevels.v1.Adjust, Request, Body, Id, RequestBody, Quantity, Reason, RequestBodyValidator (+1 more)

### Community 145 - "OpenApiOptions"
Cohesion: 0.22
Nodes (9): OpenApiOptions, ContactEmail, ContactName, Description, EnableSwagger, LicenseName, LicenseUrl, Title (+1 more)

### Community 146 - "Response"
Cohesion: 0.22
Nodes (7): Products.Endpoints.Products.v1.My.Get, Endpoint, Response, Description, Name, Price, Quantity

### Community 147 - "ProductsDbContext"
Cohesion: 0.11
Nodes (14): ProductTemplate, Brand, Color, IsActive, Model, Products, ProductTemplateConfiguration, ProductsDbContext (+6 more)

### Community 148 - ".ResolveGatewayAsync"
Cohesion: 0.25
Nodes (6): IWarehouseGateway, IWarehouseGatewayFactory, Endpoint, Setup, WarehouseGatewayFactory, WarehouseResiliencePipelines

### Community 150 - "ConcurrencyConfiguratorExtensions"
Cohesion: 0.19
Nodes (4): ConcurrencyConfiguratorExtensions, RunProductExportJobHandler, RunProductExportJobHandlerDefinition, RunProductImportJobHandlerDefinition

### Community 152 - "ReplaceCredentials/Request.cs"
Cohesion: 0.28
Nodes (8): Inventory.Endpoints.WarehouseConnections.v1.ReplaceCredentials, Request, Body, Id, RequestBody, ApiCredential, RequestBodyValidator, RequestValidator

### Community 153 - "WebhookCallback/Request.cs"
Cohesion: 0.22
Nodes (9): Constants, Request, DeliveryId, ProviderReference, ReservationId, RequestValidator, RouteParameters, ConnectionId (+1 more)

### Community 154 - "IdentityScheme"
Cohesion: 0.28
Nodes (7): IdentityScheme, Email, PhoneNumber, IdentitySchemeOptions, Scheme, IdentitySchemeOptionsValidator, Setup

### Community 156 - "InterModuleRequestOptions"
Cohesion: 0.25
Nodes (7): InterModuleRequestOptions, DependencyUnavailableRetryAfterSeconds, HandlerConcurrentMessageLimit, HandlerPrefetchCount, Timeouts, TimeoutSeconds, InterModuleRequestOptionsValidator

### Community 157 - "AlertRecipientsOptions"
Cohesion: 0.29
Nodes (7): AlertRecipientsOptions, EmailHtmlBodyTemplate, EmailSubjectTemplate, EmailTextBodyTemplate, ReconcileCron, RoleName, AlertRecipientsOptionsValidator

### Community 158 - "BackgroundJobsModule"
Cohesion: 0.22
Nodes (8): Cross-process call path, How it works, ICoreModule, BackgroundJobsModule, ActivitySourceNames, MeterNames, Name, StartupPriority

### Community 159 - "Stores/v1/Create/Request.cs"
Cohesion: 0.33
Nodes (6): Request, Address, Description, Name, OwnerId, RequestValidator

### Community 162 - "Response"
Cohesion: 0.22
Nodes (7): Products.Endpoints.Products.v1.Search, Endpoint, Response, Description, Name, Price, Quantity

### Community 163 - "Request"
Cohesion: 0.22
Nodes (9): Request, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken, Password, PushToken (+1 more)

### Community 164 - "SearchProductsByTermRequestHandler"
Cohesion: 0.27
Nodes (6): SearchProductsByTermRequest, SearchProductsByTermResponse, Endpoint, PageResponse, ProductsCacheKeys, SearchProductsByTermRequestHandler

### Community 165 - "ApplicationUserId"
Cohesion: 0.08
Nodes (18): IBackgroundUserContext, UserId, CacheKeys, For, ApplicationUserId, IsEmpty, Value, BackgroundUserContext (+10 more)

### Community 166 - "NotificationPayload"
Cohesion: 0.17
Nodes (5): INotificationDispatcher, INotificationsClient, NotificationPayload, PushMessage, SignalRNotificationDispatcher

### Community 168 - "ProductTemplates/v1/Search/Request.cs"
Cohesion: 0.25
Nodes (7): Constants, Request, Brand, Color, Model, SearchTerm, RequestValidator

### Community 169 - ".GetProductAsync"
Cohesion: 0.27
Nodes (7): GetStockLevelRequest, GetStockLevelResponse, GetProductRequest, GetProductResponse, GetStockLevelRequestHandler, Endpoint, GetProductRequestHandler

### Community 172 - "DomainEvent"
Cohesion: 0.04
Nodes (51): Products.Domain.Products.DomainEvents.v1, Common.Domain.Events, Products.Domain.Stores.DomainEvents.v1, DomainEvent, CreatedOn, Id, Version, V1ProductDescriptionUpdatedDomainEvent (+43 more)

### Community 173 - "Response"
Cohesion: 0.14
Nodes (12): Endpoint, Response, FailureKey, FileName, FinishedOn, JobId, ProductsWritten, QueuedOn (+4 more)

### Community 174 - "CachingOptions.cs"
Cohesion: 0.32
Nodes (7): CachingOptionsValidator, Redis, AppName, Host, Password, Port, RedisValidator

### Community 175 - "IInterModuleRequest"
Cohesion: 0.47
Nodes (6): GetUsersInRolePageRequest, GetUsersInRolePageResponse, RoleUserSummary, GetUsersInRolePageRequestHandler, AlertRecipientsCursor, AlertRecipientsReconciliationJob

### Community 176 - "PaginationCursor"
Cohesion: 0.27
Nodes (3): PaginationCursor, Payload, TaggedValue

### Community 177 - "CorsOptions"
Cohesion: 0.29
Nodes (7): CorsOptions, AllowCredentials, AllowedHeaders, AllowedMethods, AllowedOrigins, MaxAgeInSeconds, CorsOptionsValidator

### Community 178 - "Response"
Cohesion: 0.25
Nodes (6): Inventory.Endpoints.WarehouseConnections.v1.Create, Endpoint, Response, Id, WebhookPath, WebhookSecret

### Community 179 - "CachingEntryDefaults"
Cohesion: 0.33
Nodes (6): CachingEntryDefaults, Duration, FactoryHardTimeout, FactorySoftTimeout, FailSafeMaxDuration, FailSafeThrottleDuration

### Community 180 - "SmsOptions"
Cohesion: 0.11
Nodes (20): SmsOptions, AppName, AttemptTimeoutSeconds, BaseUrl, MaxPerDay, MaxPerPhoneNumberPerDay, MaxRetryAttempts, MsgHeader (+12 more)

### Community 181 - "RabbitMqOptions"
Cohesion: 0.15
Nodes (13): RabbitMqOptions, DefaultConcurrentMessageLimit, DefaultPrefetchCount, Host, Password, Port, RetryIntervalDeltaMs, RetryLimit (+5 more)

### Community 183 - "SendRequestBody"
Cohesion: 0.29
Nodes (6): SendRequestBody, HtmlContent, Sender, Subject, TextContent, To

### Community 184 - "SendRequestBody"
Cohesion: 0.29
Nodes (6): SendRequestBody, AppName, Encoding, IysFilter, Messages, MsgHeader

### Community 185 - "Response"
Cohesion: 0.20
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegister, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt, Setup

### Community 186 - "ReCaptchaResponse"
Cohesion: 0.29
Nodes (6): ReCaptchaResponse, ChallengeTs, ErrorCodes, Hostname, Score, Success

### Community 187 - "CustomValidator"
Cohesion: 0.05
Nodes (42): Products.Endpoints.Probe.v1, CustomRateLimitingOptionsValidator, FixedWindowValidator, DatabaseOptions, ConnectionString, DatabaseOptionsValidator, JobHousekeepingOptionsValidator, ModulesOptions (+34 more)

### Community 188 - "BoundedCaptureStream"
Cohesion: 0.15
Nodes (6): BoundedCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 189 - "V1StoreCreatedDomainEvent"
Cohesion: 0.29
Nodes (4): Products.Application.Stores.DomainEventHandlers.v1, SimulateSomeBusinessHandler, V1StoreCreatedDomainEventHandlers, V1StoreCreatedDomainEvent

### Community 190 - "IOtpService"
Cohesion: 0.18
Nodes (9): SendEmailOtpRequest, SendEmailOtpResponse, IOtpService, OtpVerificationOutcome, InvalidOtp, Success, TooManyAttempts, SendEmailOtpRequestHandler (+1 more)

### Community 193 - "BulkImportOptions"
Cohesion: 0.33
Nodes (5): BulkImportOptions, MaxFileBytes, MaxRows, SaveChunkSize, BulkImportOptionsValidator

### Community 195 - "Response"
Cohesion: 0.33
Nodes (4): Inventory.Endpoints.StockReservations.v1.Reserve, Endpoint, Response, Id

### Community 196 - ".HandleWarehouseWebhookAsync"
Cohesion: 0.21
Nodes (3): SecretProtector, Endpoint, WebhookRoute

### Community 197 - "Response"
Cohesion: 0.29
Nodes (4): Products.Endpoints.Stores.v1.Create, Setup, Response, Id

### Community 199 - "Request"
Cohesion: 0.25
Nodes (8): Request, ClientId, DeviceId, DeviceName, Otp, PhoneNumber, PushToken, RequestValidator

### Community 200 - "ActivitySource"
Cohesion: 0.09
Nodes (8): RequireFeatureFilter, RouteHandlerBuilderExtensions, BackgroundJobsTelemetry, IamTelemetry, InventoryTelemetry, NotificationsTelemetry, OutboxTelemetry, ProductsTelemetry

### Community 201 - "KeycloakScopes"
Cohesion: 0.17
Nodes (11): Devices, Hangfire, KeycloakScopes, Products, ProductTemplates, Sessions, StockLevels, StockReservations (+3 more)

### Community 202 - "DevicesOptions"
Cohesion: 0.40
Nodes (5): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator

### Community 203 - "Response"
Cohesion: 0.29
Nodes (5): Inventory.Endpoints.WarehouseConnections.v1.RotateWebhookSecret, Endpoint, Response, WebhookPath, WebhookSecret

### Community 205 - "Key decisions"
Cohesion: 0.29
Nodes (7): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Key decisions

### Community 211 - "GetExport/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Products.v1.GetExport, Request, Id, RequestValidator

### Community 212 - "Response"
Cohesion: 0.33
Nodes (4): Inventory.Endpoints.StockReservations.v1.ReserveSeries, Endpoint, Response, Ids

### Community 213 - "Response"
Cohesion: 0.33
Nodes (4): Products.Endpoints.Stores.v1.My.AddProduct, Endpoint, Response, Id

### Community 214 - "RowOutcome"
Cohesion: 0.40
Nodes (5): RowOutcome, Created, Rejected, Unchanged, Updated

### Community 215 - "Response"
Cohesion: 0.17
Nodes (10): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, Endpoint, Response, ClientId, DeviceName, Id, IpAddress, IsCurrent (+2 more)

### Community 216 - "RemoveImage/Request.cs"
Cohesion: 0.40
Nodes (5): Products.Endpoints.Products.v1.RemoveImage, Request, Id, ImageId, RequestValidator

### Community 217 - "Platform building blocks"
Cohesion: 0.12
Nodes (14): Authorization, Caching, Feature flags, Inter-module requests, Job rows, Options and configuration, Platform building blocks, Results and errors (+6 more)

### Community 218 - "Response"
Cohesion: 0.33
Nodes (4): Products.Endpoints.Products.v1.AddImage, Response, Id, Url

### Community 219 - "Response"
Cohesion: 0.33
Nodes (4): Products.Endpoints.Stores.v1.AddProduct, Endpoint, Response, Id

### Community 225 - "HealthCheckOptions"
Cohesion: 0.40
Nodes (5): HealthCheckOptions, EnableHealthChecks, ReadinessTimeoutInSeconds, StartupTimeoutInSeconds, HealthCheckOptionsValidator

### Community 226 - "ReverseProxyOptions"
Cohesion: 0.33
Nodes (5): ReverseProxyOptions, ForwardLimit, IsEnabled, TrustedNetworks, ReverseProxyOptionsValidator

### Community 227 - "SignalROptions"
Cohesion: 0.40
Nodes (5): SignalROptions, RedisConnectionString, RequireRedisBackplaneInProduction, UseRedisBackplane, SignalROptionsValidator

### Community 231 - "SmsCategory"
Cohesion: 0.33
Nodes (4): SmsCategory, CommercialIndividual, CommercialMerchant, Transactional

### Community 232 - "RequestBody"
Cohesion: 0.33
Nodes (6): RequestBody, Description, Name, Price, ProductTemplateId, Quantity

### Community 233 - "WarehouseConnections/v1/Get/Request.cs"
Cohesion: 0.50
Nodes (4): Inventory.Endpoints.WarehouseConnections.v1.Get, Request, Id, RequestValidator

### Community 234 - "WarehouseConnections/v1/Deactivate/Request.cs"
Cohesion: 0.50
Nodes (4): Inventory.Endpoints.WarehouseConnections.v1.Deactivate, Request, Id, RequestValidator

### Community 235 - "Activate/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.ProductTemplates.v1.Activate, Request, Id, RequestValidator

### Community 236 - "NetGsmSmsGateway"
Cohesion: 0.16
Nodes (8): NetGsmSmsGateway, SendMessageBody, Msg, No, SendResponseBody, Code, Description, JobId

### Community 237 - "RowResult"
Cohesion: 0.40
Nodes (4): RowResult, Created, Unchanged, Updated

### Community 238 - "RequestBodyLimitMetadata"
Cohesion: 0.22
Nodes (4): Webhooks, RequestBodyLimitMiddleware, EnrichLogsWithUserInfoMiddleware, SecurityHeadersMiddleware

### Community 239 - "ProjectionReconciliationOptions"
Cohesion: 0.50
Nodes (4): ProjectionReconciliationOptions, MaxPages, PageSize, ProjectionReconciliationOptionsValidator

### Community 240 - "SecretProtectionOptions.cs"
Cohesion: 0.50
Nodes (3): SecretProtectionOptions, EncryptionKey, SecretProtectionOptionsValidator

### Community 241 - "DeprovisionUserRequest"
Cohesion: 0.80
Nodes (3): DeprovisionUserRequest, DeprovisionUserResponse, DeprovisionUserRequestHandler

### Community 242 - "Response"
Cohesion: 0.25
Nodes (6): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 243 - "IssueVerificationTokenRequestHandler"
Cohesion: 0.80
Nodes (3): IssueVerificationTokenRequest, IssueVerificationTokenResponse, IssueVerificationTokenRequestHandler

### Community 245 - "My/Get/Request.cs"
Cohesion: 0.67
Nodes (3): Request, Id, RequestValidator

### Community 251 - "Response"
Cohesion: 0.33
Nodes (4): Products.Endpoints.Stores.v1.My.Create, Endpoint, Response, Id

### Community 264 - "ReCaptchaResponse"
Cohesion: 0.16
Nodes (6): Request, ApiCredential, BaseUrl, Provider, StoreId, RequestValidator

### Community 269 - "RouteGroupBuilder"
Cohesion: 0.03
Nodes (30): Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Setup, Endpoint (+22 more)

### Community 276 - "InterModuleRequestHandler"
Cohesion: 0.26
Nodes (6): IInterModuleRequestHandler, InterModuleRequestHandler, DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, GetDeviceSessionsRequestHandler

### Community 280 - "SendForEmail/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForEmail, Request, CaptchaToken, Email, RequestValidator

### Community 296 - "AuditLogRetentionService"
Cohesion: 0.16
Nodes (9): AuditLogOptions, PerSchemaRetentionDays, PurgeBatchSize, RetentionCron, RetentionDays, AuditLogOptionsValidator, AuditLogRetentionJobRegistrar, AuditLogRetentionService (+1 more)

### Community 319 - "Products/v1/Update/Request.cs"
Cohesion: 0.20
Nodes (11): Products.Endpoints.Products.v1.Update, Request, Body, Id, RequestBody, Description, Name, Price (+3 more)

### Community 320 - "WebhookCallback/Endpoint.cs"
Cohesion: 0.05
Nodes (14): Notifications.Application.Otp, Common.Endpoints.Webhooks, Notifications.Application.Push, Common.Application.Caching, Inventory.Endpoints.StockReservations.v1.WebhookCallback, Notifications.Infrastructure.Telemetry, Products.Infrastructure.Caching, Notifications.Infrastructure.InterModuleRequestHandlers (+6 more)

### Community 323 - "Common.Application.Validation"
Cohesion: 0.04
Nodes (52): Products.Endpoints.Products.v1.GetImport, Products.Endpoints.Stores.v1.Deactivate, Products.Endpoints.ProductTemplates.v1.Deactivate, Products.Endpoints.Products.v1.DownloadExport, Products.Endpoints.Stores.v1.My.RemoveProduct, Common.Application.EndpointFilters, Inventory.Endpoints.StockReservations.v1.Release, Common.Application.ModelBinders (+44 more)

### Community 324 - "RequestBody"
Cohesion: 0.06
Nodes (38): Inventory.Endpoints.StockReservations.v1.Commit, Inventory.Endpoints.StockLevels.v1.Receive, Inventory.Endpoints.StockLevels.v1.ChangeLowStockThreshold, Request, Body, Id, RequestBody, Threshold (+30 more)

## Knowledge Gaps
- **1105 isolated node(s):** `UserId`, `Id`, `IdAsString`, `Roles`, `SessionId` (+1100 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1495 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **128 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `ObjectStorageOptions`, `.AddModuleDbContext`, `Host.Swagger`, `Common.Domain.ResultMonad`, `ResxLocalizationOptions`, `EmailOptions`, `OpenApiOptions`, `microsoft_extensions_options`, `FullTextSearchOptions`, `InventoryOptions`, `IdentityScheme`, `fluentvalidation`, `InterModuleRequestOptions`, `AlertRecipientsOptions`, `microsoft_entityframeworkcore`, `ObservabilityOptions`, `AuditLogRetentionService`, `S3ObjectStoreCore.cs`, `CachingOptions.cs`, `CorsOptions`, `BackgroundJobsOptions`, `CaptchaOptions`, `RabbitMqOptions`, `SmsOptions`, `OutboxOptions.cs`, `CustomValidator`, `RequestLoggingOptions`, `WebhookCallback/Endpoint.cs`, `BulkImportOptions`, `Setup.Logger.cs`, `DevicesOptions`, `KeycloakOptions`, `OtpOptions`, `PushOptions`, `HealthCheckOptions`, `SignalROptions`, `ResiliencyOptions`, `Common.InterModuleRequests.Contracts`, `ProjectionReconciliationOptions`, `SecretProtectionOptions.cs`, `Tokens/VersionNeutral/Setup.cs`, `Users/VersionNeutral/Setup.cs`, `system_diagnostics`, `ProductsOptions`?**
  _High betweenness centrality (0.079) - this node is a cross-community bridge._
- **What connects `UserId`, `Id`, `IdAsString` to the rest of the system?**
  _1105 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `PaginationResponse` be split into smaller, more focused modules?**
  _Cohesion score 0.09230769230769231 - nodes in this community are weakly interconnected._
- **Why does `Result` connect `Result` to `PaginationResponse`, `Result`, `ApplicationUserId`, `WarehouseConnection`, `StockLevel`, `ProductImage`, `ProductsDbContext`, `.ResolveGatewayAsync`, `ReCaptchaService`, `.RegisterAsync`, `Error`, `Response`, `SearchProductsByTermRequestHandler`, `NotificationPayload`, `WarehouseHttpGateway`, `.GetProductAsync`, `SmsOtpDispatchOutcome`, `DomainEvent`, `.SaveBatchAsync`, `BrevoEmailGateway`, `ResultTelemetryExtensions`, `S3ObjectStoreCore`, `.HandleWarehouseWebhookAsync`, `ProductTemplateId`, `EmailOtpDispatchOutcome`, `Response`, `StockReservationId`, `ProductImportJobRunner`, `ServiceAccountTokenCache`, `Task`, `NetGsmSmsGateway`, `Response`, `IResult`?**
  _High betweenness centrality (0.046) - this node is a cross-community bridge._
- **Should `ProductsModule` be split into smaller, more focused modules?**
  _Cohesion score 0.14705882352941177 - nodes in this community are weakly interconnected._
- **Why does `ApplicationUserId` connect `ApplicationUserId` to `common_application_localization_resources`, `AuditableEntityResponse`, `ApplicationUserId`, `StockLevel`, `Response`, `KeycloakPermissionAuthorizationHandler`, `Common.Domain.StronglyTypedIds`, `InterModuleRequestHandler`, `DeviceRegistration`, `DeviceRegistryReconciliationService`, `.RegisterAsync`, `Stores/v1/Create/Request.cs`, `WebhookDelivery`, `DateTimeOffset`, `NotificationPayload`, `DomainEvent`, `IInterModuleRequest`, `V1StoreCreatedDomainEvent`, `Seeder`, `Common.Application.Validation`, `IntegrationEventHandlerBase`, `ProductTemplateId`, `Response`, `ProvisionUserRequestHandler`, `Response`, `Response`, `StoreId`, `InventoryDbContext`, `NotificationsHub`, `ILogger`, `ProductImportJobRunner`, `ServiceAccountTokenCache`, `Task`, `Response`, `DeprovisionUserRequest`, `DefaultIdType`, `Response`, `Response`?**
  _High betweenness centrality (0.042) - this node is a cross-community bridge._
- **Should `HttpContext` be split into smaller, more focused modules?**
  _Cohesion score 0.13756613756613756 - nodes in this community are weakly interconnected._