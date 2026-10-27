# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-10-27)

## Corpus Check
- 838 files · ~167,432 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 5636 nodes · 16534 edges · 355 communities (231 shown, 124 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 761 edges (avg confidence: 0.87)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `3b87e5d5`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- .GetMovementsAsync
- DefaultIdType
- HttpContext
- Common.Application.DTOs
- ApplicationUserId
- .HandleWarehouseWebhookAsync
- ApplyAuditingInterceptor
- ProductExportJobRunner
- IAM.Endpoints
- Result
- Common.Domain.ResultMonad
- Common.Infrastructure.Extensions
- WarehouseConnectionId
- Response
- CurrentUser
- EmailOptions
- Infrastructure/Setup.cs
- KeycloakPermissionAuthorizationHandler.cs
- Common.Application.Options
- FullTextSearchOptions
- BoundedRequestCaptureStream
- DeviceRegistration
- INotificationsDbContext
- IKeycloakAdminClient
- TimeProvider
- Response
- RequestLoggingOptions
- Stores/v1/My/Update/Request.cs
- IModule
- Error
- StoreAccess
- DateTimeOffset
- Response
- Response
- OutboxMessage
- CustomRateLimitingOptions
- InterModuleRequestHandlerDefinition
- microsoft_entityframeworkcore
- ObservabilityOptions
- ReservationStatus
- ProductSummariesReconciliationJob
- RequestResponseBodyLoggingMiddleware
- SmsOtpDispatchOutcome
- ProductImportJobRunner
- OutboxModule
- StoreAccessReconciliationJob
- Outbox Misuse Check
- .UseInfrastructure
- Add Integration Event Command
- host
- Response
- CaptchaOptions
- KeyedResiliencePipelines
- RedisFixedWindowRateLimiter
- .SaveBatchAsync
- FirebasePushGateway
- Cross-Module Reference Violation
- CustomValidator
- Request
- Bogus Test Data
- .ReceiveAsync
- S3ObjectStoreCore
- OpenApiOptions
- RequestBody
- .AddNotificationsSignalR
- PackageDimensions
- IResxLocalizer
- ISearchLanguageResolver
- Getting started
- ServiceAccountTokenCache
- FormApiWarehouseGateway
- AsNoTracking Coverage Check
- IIntegrationEventOutbox
- NotificationPayload
- .HandleAsync
- KeycloakOptions
- Response
- EmailOtpDispatchOutcome
- Request
- IOptions
- PaginationRequestValidator
- EntityTypeBuilder
- SetAttributes/Request.cs
- DateTimeOffset
- PushOptions
- Configuration-Driven Module Registration
- NotifyRoleOnCriticalAlertRaisedHandler
- Setup
- InventoryDbContext
- IStronglyTypedId
- Persistence conventions
- SecretProtector
- IamModule
- ILogger
- StockBulkAdjustJobRunner
- Response
- KeycloakTokenClient
- Task
- ResiliencyOptions
- Products/v1/Update/Request.cs
- Request
- Response
- Response
- Common.InterModuleRequests.Contracts
- WarehouseConnectionResponse
- Products/v1/My/Update/Request.cs
- Response
- KeycloakPermissionPolicyProvider
- Dictionary
- OtpOptions
- RequestBody
- Response
- SearchProductsByTermRequestHandler
- Request
- For
- Response
- depends_on
- healthcheck
- mm.keycloak
- mm.redis
- Response
- system_diagnostics
- ProductsOptions
- Pagination and search
- ResiliencyOptions
- .DownloadExportAsync
- Common.InterModuleRequests.Products
- docker-compose.yml (Base Stack)
- IServiceCollection
- ObjectStorageOptions
- InventoryModule
- Request
- Response
- ValueTask
- services
- mm.postgres
- Response
- JsonSerializerOptions
- mm.rabbitmq
- .ToFields
- PaginationRequest
- Request
- Response
- KeycloakPermissionClient
- Response
- RowResult
- InventoryOptions
- Response
- README (Boilerplate Overview)
- NetGsmSmsGateway
- KeycloakPermissionAuthorizationHandler
- Response
- InterModuleRequestOptions
- AlertRecipientsOptions
- BackgroundJobsOptions
- Response
- Response
- Request
- ProductTemplateId
- NotificationsTelemetry
- SendRequestBody
- AuditLogOptions
- JobStatus
- PaginationResponse
- ProductTemplates/v1/Search/Request.cs
- Response
- Notifications.Application/IAssemblyReference.cs
- Commit/Request.cs
- Common.Domain.Events
- MassTransitInterModuleRequestClient
- CachingOptions.cs
- AlertRecipientsReconciliationJob
- IInventoryDbContext
- CorsOptions
- AuditableEntityResponse
- ReCaptchaResponse
- SmsOptions
- RabbitMqOptions
- Request
- SendRequestBody
- ChunkEncodingFreeS3Client
- SecretProtectionOptions
- Uri
- Request
- BoundedCaptureStream
- ProductImportJob
- AI-assisted development workflow
- Response
- IAM.Domain
- Response
- Keycloak realm as code
- Response
- Response
- Your first requests
- StronglyTypedIdBinder.cs
- Request
- NotificationsTelemetry
- KeycloakScopes
- IamModule
- Response
- Response
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- Key decisions
- JwtBearerConfigureOptions
- ProviderStatusSnapshot
- SmsCategory
- Response
- Response
- Response
- Platform building blocks
- Stores/v1/Create/Request.cs
- StoreId
- SendForEmail/Request.cs
- HttpClient
- How it works
- Response
- IServiceProvider
- Stores/v1/AuditLog/Request.cs
- Split-Deployment PoC
- .WriteTooManyRequestsToResponse
- ReverseProxyOptions
- Products/v1/AuditLog/Request.cs
- DevicesOptions
- HealthCheckOptions
- SignalROptions
- .AddCommonPersistence
- GetImport/Request.cs
- OutboxCleanupSettings
- ProvisionUserResponseExtensions.cs
- InboxStore
- Request
- ConstraintViolationExtensions
- Request
- Response
- GetExport/Request.cs
- Request
- Request
- StockLevelErrors.cs
- ListImports/Request.cs
- MovementTypeSnapshot
- Products.Endpoints
- Request
- Response
- IResult
- Products.Domain/IAssemblyReference.cs
- IAM.Application/IAssemblyReference.cs
- Products.Infrastructure/IAssemblyReference.cs
- Products.Application/IAssemblyReference.cs
- Common.Infrastructure/IAssemblyReference.cs
- Notifications.Domain/IAssemblyReference.cs
- IAM.Infrastructure/IAssemblyReference.cs
- Notifications.Infrastructure
- My/AuditLog/Request.cs
- ListBulkAdjustments/Request.cs
- Infrastructure/StringExtensions.cs
- WarehouseConnections/v1/My/Search/Request.cs
- ValidationContextExtensions
- ProblemDetailsExtensions.cs
- Request
- Request
- RouteGroupBuilder
- Response
- RowOutcome
- .SetRetryAfterHeader
- .LocalizeFromError
- ModulesOptions
- ObjectStorageRetryMode
- SensitivePathRule
- .VerifySha256
- Security/Setup.cs
- Request
- IamTelemetry.cs
- StockBulkAdjustErrors.cs
- Request
- .ReadRows
- StringExtensions
- Common.Domain.Events
- ProductImportErrors.cs
- Request
- .ReadRows
- CaptchaErrors.cs
- OtpErrors.cs
- fluentvalidation
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
1. `Result` - 270 edges
2. `Common.Application.Options` - 193 edges
3. `Common.Domain.ResultMonad` - 169 edges
4. `CustomValidator` - 119 edges
5. `ApplicationUserId` - 117 edges
6. `Common.Application.Auth` - 104 edges
7. `Common.Application.Validation` - 103 edges
8. `Common.Domain.StronglyTypedIds` - 101 edges
9. `Common.Application.Extensions` - 98 edges
10. `Common.Infrastructure.Persistence.Extensions` - 90 edges

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

## Communities (355 total, 124 thin omitted)

### Community 0 - ".GetMovementsAsync"
Cohesion: 0.40
Nodes (5): GetUsersRequest, GetUsersResponse, UserSummary, GetUsersRequestHandler, UserDisplayCacheEntry

### Community 1 - "DefaultIdType"
Cohesion: 0.05
Nodes (55): IAM, Inventory, Notifications, Products, Transactional outbox and inbox, ProcessedMessage, ConsumerName, MessageId (+47 more)

### Community 2 - "HttpContext"
Cohesion: 0.13
Nodes (12): Rate limiting and resilience, FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit, RequestBodyLimitEndpointExtensions, RequestBodyLimitMetadata (+4 more)

### Community 3 - "Common.Application.DTOs"
Cohesion: 0.11
Nodes (9): Products.Endpoints.Stores.v1.GetApplication, Products.Endpoints.Stores.v1.Search, Products.Endpoints.ProductTemplates.v1.Search, Products.Endpoints.Products.v1.Search, Common.Application.DTOs, Products.Endpoints.Stores.v1.Get, Products.Endpoints.ProductTemplates.v1.Get, Inventory.Endpoints.WarehouseConnections (+1 more)

### Community 5 - "ApplicationUserId"
Cohesion: 0.05
Nodes (43): ApplicationUserId, IsEmpty, Value, BackgroundUserContext, UserId, GrantedPermission, KeycloakUser, KeycloakUserPage (+35 more)

### Community 6 - ".HandleWarehouseWebhookAsync"
Cohesion: 0.13
Nodes (4): DbContextExtensions, PersistenceQueryableExtensions, WarehouseConnectionQueries, Endpoint

### Community 7 - "ApplyAuditingInterceptor"
Cohesion: 0.22
Nodes (7): Add a new language/culture, Add search to a new entity _(Build checklist)_, Change the vector (weights, fields, or config), Extending and maintaining, ApplyAuditingInterceptor, ApplySearchLanguageInterceptor, Setup

### Community 8 - "ProductExportJobRunner"
Cohesion: 0.33
Nodes (4): ProductExportJobId, ExportPageRequest, ExportRow, ProductExportJobRunner

### Community 10 - "Result"
Cohesion: 0.05
Nodes (15): CollectionExtensions, FeatureFlagResultExtensions, Result, Result, Error, IsFailure, Value, Error (+7 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.06
Nodes (36): Inventory.Endpoints.StockLevels.v1.Search, Products.Infrastructure.Persistence, Common.Endpoints.Webhooks, Products.Infrastructure.InterModuleRequestHandlers, Common.Application.Search, Products.Application.Exports, Products.Endpoints.Stores.v1.Create, Products.Domain.Products.Errors (+28 more)

### Community 12 - "Common.Infrastructure.Extensions"
Cohesion: 0.06
Nodes (25): Common.Infrastructure.Modules, IAM.Endpoints.Captcha.VersionNeutral, Notifications.Application.Push, Common.Endpoints.Versioning, Products.Endpoints.Probe, Common.Infrastructure.RateLimiting, Notifications.Infrastructure.Push, Products.Infrastructure.RateLimiting (+17 more)

### Community 13 - "WarehouseConnectionId"
Cohesion: 0.08
Nodes (21): Inventory.Domain.WarehouseConnections.DomainEvents.v1, ProviderSnapshot, FormApi, JsonApi, V1WarehouseConnectionCreatedDomainEvent, V1WarehouseConnectionCreatedDomainEventExtensions, V1WarehouseConnectionCredentialsReplacedDomainEvent, V1WarehouseConnectionDeactivatedDomainEvent (+13 more)

### Community 14 - "Response"
Cohesion: 0.10
Nodes (19): IAM.Endpoints.Users.VersionNeutral.Me.Get, DayBoundsUtc, TimeProviderExtensions, Response, BirthDate, CreatedOn, Email, FirstName (+11 more)

### Community 15 - "CurrentUser"
Cohesion: 0.15
Nodes (10): CurrentUser, BackgroundUserIdAsString, Id, IdAsString, IsAuthenticated, Principal, Roles, SessionId (+2 more)

### Community 16 - "EmailOptions"
Cohesion: 0.08
Nodes (27): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+19 more)

### Community 17 - "Infrastructure/Setup.cs"
Cohesion: 0.08
Nodes (13): Common.InterModuleRequests, Common.Infrastructure.Localization, Notifications.Infrastructure.Email.Brevo, Common.Application.AuditLog, IAM.Domain.Captcha, IAM.Infrastructure.Captcha.Services, IAM.Application.Captcha.Services, Common.Application.JsonConverters (+5 more)

### Community 18 - "KeycloakPermissionAuthorizationHandler.cs"
Cohesion: 0.07
Nodes (8): Host, Common.Infrastructure.Auth.Services, IAM.Infrastructure.Auth, Host.Swagger, Common.Infrastructure.Auth, Host.Infrastructure, Program, AccessTokenReader

### Community 19 - "Common.Application.Options"
Cohesion: 0.04
Nodes (49): Common.Infrastructure.Persistence, Notifications.Application.Sms, Notifications.Infrastructure.Devices, Common.Application.Persistence.Inbox, Inventory.Endpoints, Notifications.Infrastructure.Email, Products.Infrastructure.Imports, Inventory.Application.Persistence (+41 more)

### Community 20 - "FullTextSearchOptions"
Cohesion: 0.15
Nodes (15): Configuration, Full-Text Search, Gotchas, Non-goals, Per-entity strategy, Testing, The problem, TL;DR: what the feature gives you (+7 more)

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.14
Nodes (6): BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 22 - "DeviceRegistration"
Cohesion: 0.07
Nodes (12): StronglyTypedIdHelper, DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive, LastReconciledOn, PushToken (+4 more)

### Community 23 - "INotificationsDbContext"
Cohesion: 0.21
Nodes (12): GetActiveSessionIdsRequest, GetActiveSessionIdsResponse, UserSessionIds, DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, GetActiveSessionIdsRequestHandler, INotificationsDbContext (+4 more)

### Community 24 - "IKeycloakAdminClient"
Cohesion: 0.06
Nodes (40): IInterModuleRequest, IInterModuleRequestClient, IInterModuleRequestHandler, InterModuleRequestHandler, DeprovisionUserRequest, DeprovisionUserResponse, ProvisionUserFailureReason, EmailAlreadyRegistered (+32 more)

### Community 25 - "TimeProvider"
Cohesion: 0.11
Nodes (16): JobHousekeepingJob, JobHousekeepingOptions, PageSize, RetentionHours, StaleAfterMinutes, JobHousekeepingOptionsValidator, IDbContext, AuditLog (+8 more)

### Community 26 - "Response"
Cohesion: 0.29
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, Endpoint, Response, EmailVerificationToken, IsRegistered

### Community 27 - "RequestLoggingOptions"
Cohesion: 0.16
Nodes (12): RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes, SensitiveQueryParamPaths (+4 more)

### Community 28 - "Stores/v1/My/Update/Request.cs"
Cohesion: 0.40
Nodes (5): Request, Address, Description, Name, RequestValidator

### Community 29 - "IModule"
Cohesion: 0.11
Nodes (17): IModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority, NotificationsModule, ActivitySourceNames (+9 more)

### Community 30 - "Error"
Cohesion: 0.10
Nodes (17): Error, Key, ParameterName, StatusCode, SubErrors, Value, IdentityErrors, TokenErrors (+9 more)

### Community 31 - "StoreAccess"
Cohesion: 0.13
Nodes (11): ProjectionEntity, SourceVersion, ProductSummary, Name, ProductId, StoreId, StoreAccess, IsActive (+3 more)

### Community 32 - "DateTimeOffset"
Cohesion: 0.06
Nodes (40): JobRow, FailureKey, FinishedOn, Progress, QueuedOn, RequestedBy, StartedOn, Status (+32 more)

### Community 33 - "Response"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.Refresh, Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 34 - "Response"
Cohesion: 0.09
Nodes (20): StoreStatus, Active, Inactive, PendingApproval, Suspended, Endpoint, Response, Address (+12 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.06
Nodes (25): Common.Application.Persistence.Outbox, IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, OutboxMessage (+17 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.07
Nodes (14): CustomRateLimitingOptions, CheckRegistration, CreateStore, Email, ExemptPathPrefixes, Global, OtpVerify, Register (+6 more)

### Community 37 - "InterModuleRequestHandlerDefinition"
Cohesion: 0.19
Nodes (3): ConcurrencyConfiguratorExtensions, InterModuleRequestHandlerDefinition, RequestType

### Community 38 - "microsoft_entityframeworkcore"
Cohesion: 0.04
Nodes (37): Common.Infrastructure.Persistence.Inbox, Inventory.Domain.WebhookDeliveries, Inventory.Application.WarehouseConnections, IAM.Endpoints.Users.VersionNeutral.Search, Products.Infrastructure.Persistence.Seeding, Inventory.Infrastructure.IntegrationEventHandlers, Inventory.Domain.WarehouseConnections.Errors, Inventory.Infrastructure.InterModuleRequestHandlers (+29 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.08
Nodes (17): ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics, EnableTracing, LogSink, MinimumLevel (+9 more)

### Community 40 - "ReservationStatus"
Cohesion: 0.14
Nodes (11): Webhooks, RequestBodyLimitFilter, ProviderStatusMap, ReservationStatus, Active, Committed, Expired, Released (+3 more)

### Community 41 - "ProductSummariesReconciliationJob"
Cohesion: 0.11
Nodes (13): ProjectionReconciliationOptions, MaxPages, PageSize, ProjectionReconciliationOptionsValidator, ProjectionReconciliationJob, GetProductsPageRequest, GetProductsPageResponse, ProductPageItem (+5 more)

### Community 43 - "SmsOtpDispatchOutcome"
Cohesion: 0.33
Nodes (5): OtpDispatchErrors, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled

### Community 44 - "ProductImportJobRunner"
Cohesion: 0.24
Nodes (3): ProductImportJobId, ProductImportJobRunner, Slice

### Community 45 - "OutboxModule"
Cohesion: 0.05
Nodes (33): Adding a module or a feature, How modules fit together, Modules, Outbox, OutboxOptions, BaseBackoffSeconds, BatchSize, ClaimLeaseSeconds (+25 more)

### Community 46 - "StoreAccessReconciliationJob"
Cohesion: 0.22
Nodes (8): GetStoresPageRequest, GetStoresPageResponse, StorePageItem, Setup, StoreAccessReconciliationCursor, StoreAccessReconciliationJob, StoreAccessReconciliationJobRegistrar, GetStoresPageRequestHandler

### Community 48 - ".UseInfrastructure"
Cohesion: 0.16
Nodes (3): RequestBodyLimitMiddleware, EnrichLogsWithUserInfoMiddleware, SecurityHeadersMiddleware

### Community 50 - "host"
Cohesion: 0.18
Nodes (11): context, dockerfile, build, container_name, environment, image, networks, ports (+3 more)

### Community 51 - "Response"
Cohesion: 0.14
Nodes (12): Endpoint, Response, FailureKey, FileName, FinishedOn, JobId, ProductsWritten, QueuedOn (+4 more)

### Community 52 - "CaptchaOptions"
Cohesion: 0.08
Nodes (20): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey, Provider, ScoreThreshold (+12 more)

### Community 54 - "RedisFixedWindowRateLimiter"
Cohesion: 0.27
Nodes (5): FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration

### Community 55 - ".SaveBatchAsync"
Cohesion: 0.29
Nodes (4): ProductImportRowError, BatchSummary, RowParser, SheetRow

### Community 58 - "CustomValidator"
Cohesion: 0.05
Nodes (43): CustomRateLimitingOptionsValidator, FixedWindowValidator, OutboxCleanupSettingsValidator, OutboxOptionsValidator, CustomValidator, Request, Email, Otp (+35 more)

### Community 59 - "Request"
Cohesion: 0.15
Nodes (13): Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken (+5 more)

### Community 61 - ".ReceiveAsync"
Cohesion: 0.26
Nodes (7): WarehouseWebhookIntake, WebhookDeliveryOutcome, Applied, Duplicate, Ignored, Mirrored, Unrecognized

### Community 64 - "S3ObjectStoreCore"
Cohesion: 0.06
Nodes (16): Object storage, IPrivateObjectStore, IPublicObjectStore, ObjectListItem, ObjectListPage, ObjectMetadata, UploadObjectRequest, S3ObjectStoreCore (+8 more)

### Community 65 - "OpenApiOptions"
Cohesion: 0.07
Nodes (15): OpenApiOptions, ContactEmail, ContactName, Description, EnableSwagger, LicenseName, LicenseUrl, Title (+7 more)

### Community 66 - "RequestBody"
Cohesion: 0.18
Nodes (10): RequestBody, FirstDeadline, IntervalDays, OccurrenceCount, ProductId, QuantityPerOccurrence, Warehouses, WarehouseAllocation (+2 more)

### Community 68 - "PackageDimensions"
Cohesion: 0.10
Nodes (11): Common.Domain, ValueObject, DimensionsSnapshot, V1ProductPackageDimensionsChangedDomainEvent, V1ProductPackageDimensionsChangedDomainEventExtensions, PackageDimensions, HeightCm, LengthCm (+3 more)

### Community 69 - "IResxLocalizer"
Cohesion: 0.29
Nodes (3): CommonValidations, Request, RequestValidator

### Community 70 - "ISearchLanguageResolver"
Cohesion: 0.18
Nodes (8): Localization, ResxLocalizationOptions, DefaultCulture, SupportedCultures, ResxLocalizationOptionsValidator, SearchLanguageResolver, UniversalConfig, Setup

### Community 71 - "Getting started"
Cohesion: 0.18
Nodes (11): Configuration, Getting started, Prerequisites, Run the API from the command line instead, Run the load test, Run the tests, Run the whole system, Seeded data (+3 more)

### Community 72 - "ServiceAccountTokenCache"
Cohesion: 0.11
Nodes (12): IServiceAccountTokenProvider, TokenResponseRepresentation, AccessToken, ExpiresIn, RefreshExpiresIn, RefreshToken, SessionState, ServiceAccountTokenCache (+4 more)

### Community 73 - "FormApiWarehouseGateway"
Cohesion: 0.18
Nodes (8): FormApiWarehouseGateway, HoldEnvelope, HoldResult, JsonApiWarehouseGateway, PushStockItem, PushStockRequestBody, ReleaseRequestBody, ReleaseResponseBody

### Community 75 - "IIntegrationEventOutbox"
Cohesion: 0.07
Nodes (31): DomainEventHandlerBase, IEventHandler, IEventHandlerWrapper, IIntegrationEventOutbox, StoreAccessChangedIntegrationEvent, StockLowThresholdCrossedIntegrationEventPublishingHandler, StockLowThresholdRecoveredIntegrationEventPublishingHandler, StockReservationCommittedIntegrationEventPublishingHandler (+23 more)

### Community 76 - "NotificationPayload"
Cohesion: 0.19
Nodes (5): INotificationDispatcher, INotificationsClient, NotificationPayload, PushMessage, SignalRNotificationDispatcher

### Community 77 - ".HandleAsync"
Cohesion: 0.24
Nodes (7): GetSeedUserIdsRequest, GetSeedUserIdsResponse, GetSeedUserIdsRequestHandler, Endpoint, Request, Count, RequestValidator

### Community 78 - "KeycloakOptions"
Cohesion: 0.11
Nodes (18): KeycloakOptions, AttemptTimeoutSeconds, Authority, BaseUrl, DecisionCacheMaxDurationSeconds, Realm, RequireHttpsMetadata, ResourceClientId (+10 more)

### Community 79 - "Response"
Cohesion: 0.17
Nodes (11): Endpoint, Response, BirthDate, CreatedOn, Email, Enabled, FirstName, Id (+3 more)

### Community 80 - "EmailOtpDispatchOutcome"
Cohesion: 0.29
Nodes (8): EmailOtpDispatchErrors, EmailOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, SendEmailOtpRequest, SendEmailOtpResponse, SendEmailOtpRequestHandler

### Community 81 - "Request"
Cohesion: 0.18
Nodes (11): Request, Email, EmailVerificationToken, FirstName, LastName, Password, PhoneNumber, StoreAddress (+3 more)

### Community 82 - "IOptions"
Cohesion: 0.15
Nodes (30): Projections and reconciliation, IntegrationEventHandlerBase, MaxEventAge, CachingOptions, AllowInMemoryOnlyInProduction, EntryDefaults, IdempotencyKeyDuration, IdempotencyL1Duration (+22 more)

### Community 83 - "PaginationRequestValidator"
Cohesion: 0.17
Nodes (10): PaginationRequestValidator, Request, SearchTerm, RequestValidator, Request, Id, RequestValidator, Request (+2 more)

### Community 84 - "EntityTypeBuilder"
Cohesion: 0.08
Nodes (27): AuditableEntityConfiguration, JobRowConfiguration, ProcessedMessageConfiguration, DomainEventConverter, IntegrationEventConverter, StronglyTypedIdValueConverter, StoreDailyStockTotal, CommittedReservations (+19 more)

### Community 85 - "SetAttributes/Request.cs"
Cohesion: 0.18
Nodes (10): AttributeItem, Name, Value, Request, Body, Id, RequestBody, Attributes (+2 more)

### Community 86 - "DateTimeOffset"
Cohesion: 0.09
Nodes (4): IBackgroundJobs, IRecurringBackgroundJobs, BackgroundJobsService, RecurringBackgroundJobsService

### Community 87 - "PushOptions"
Cohesion: 0.12
Nodes (20): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri, Type (+12 more)

### Community 89 - "NotifyRoleOnCriticalAlertRaisedHandler"
Cohesion: 0.11
Nodes (4): CriticalAlertRaisedIntegrationEvent, NotifyRoleOnCriticalAlertRaisedHandler, NotificationGroupName, NotificationsHub

### Community 91 - "InventoryDbContext"
Cohesion: 0.04
Nodes (39): IdBound, Value, JobClaimExtensions, ProjectionUpsertExtensions, ProjectionUpsertOutcome, Inserted, Stale, Updated (+31 more)

### Community 92 - "IStronglyTypedId"
Cohesion: 0.04
Nodes (41): AggregateRoot, Events, Id, Version, IAggregateRoot, Events, Id, Version (+33 more)

### Community 93 - "Persistence conventions"
Cohesion: 0.10
Nodes (9): Persistence conventions, IDatabaseSeeder, Priority, DatabaseSeederOrchestrator, AutoMigrateMarker, IAutoMigrateMarker, MigrationGuard, SeedingCompletionTracker (+1 more)

### Community 94 - "SecretProtector"
Cohesion: 0.20
Nodes (4): Secret protection, SecretGenerator, SecretProtector, WebhookRoute

### Community 96 - "ILogger"
Cohesion: 0.04
Nodes (23): ActorNames, EventDispatcher, AuditLogRetentionJobRegistrar, AuditLogRetentionService, Setup, OutboxSaveHelper, GlobalExceptionHandlingMiddleware, LoginCompletion (+15 more)

### Community 97 - "StockBulkAdjustJobRunner"
Cohesion: 0.09
Nodes (21): StockBulkAdjustJob, ClientRequestId, FileName, LastProcessedRow, ObjectKey, Reason, RequeueCount, RowErrors (+13 more)

### Community 98 - "Response"
Cohesion: 0.11
Nodes (16): Endpoint, Response, FailureKey, FileName, FinishedOn, JobId, ProcessedRows, ProductsCreated (+8 more)

### Community 99 - "KeycloakTokenClient"
Cohesion: 0.16
Nodes (7): SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, KeycloakTokens, KeycloakTokenClient, SendSecurityAlertRequestHandler

### Community 100 - "Task"
Cohesion: 0.04
Nodes (13): OtpCacheEntry, IVariantFeatureManagerExtensions, IInboxStore, IKeycloakPermissionClient, StockLevelRowLock, StockCounterWriter, EmailMessage, OtpVerificationOutcome (+5 more)

### Community 101 - "ResiliencyOptions"
Cohesion: 0.13
Nodes (12): KeyedResilienceProfile, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, RateLimitFailOpen, RateLimitPermits (+4 more)

### Community 102 - "Products/v1/Update/Request.cs"
Cohesion: 0.20
Nodes (10): Request, Body, Id, RequestBody, Description, Name, Price, Quantity (+2 more)

### Community 103 - "Request"
Cohesion: 0.17
Nodes (12): Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FirstName, LastName (+4 more)

### Community 104 - "Response"
Cohesion: 0.29
Nodes (7): Response, Id, LowStockThreshold, ProductId, ProductName, QuantityOnHand, StoreId

### Community 105 - "Response"
Cohesion: 0.25
Nodes (6): Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 106 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.04
Nodes (30): Notifications.Application.Otp, Common.Endpoints, IAM.Infrastructure.Keycloak.Representations, IAM.Endpoints.Users.VersionNeutral.SelfRegister, Common.Application.Caching, IAM.Endpoints.Otp, Notifications.Application.Persistence, Notifications.Infrastructure.Telemetry (+22 more)

### Community 107 - "WarehouseConnectionResponse"
Cohesion: 0.18
Nodes (8): Endpoint, Endpoint, Endpoint, WarehouseConnectionResponse, BaseUrl, Provider, Status, StoreId

### Community 108 - "Products/v1/My/Update/Request.cs"
Cohesion: 0.20
Nodes (10): Request, Body, Id, RequestBody, Description, Name, Price, Quantity (+2 more)

### Community 109 - "Response"
Cohesion: 0.33
Nodes (4): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, Endpoint, Response, IsRegistered

### Community 111 - "Dictionary"
Cohesion: 0.27
Nodes (6): SecurityHeadersOptions, Headers, SecurityHeadersOptionsValidator, SmsTemplatesOptions, Otp, ProductRow

### Community 112 - "OtpOptions"
Cohesion: 0.09
Nodes (20): OtpOptions, DummyCode, EmailQuotaWindowMinutes, ExpirationInMinutes, Length, MaxSendsPerEmailPerWindow, MaxSendsPerPhonePerWindow, PhoneQuotaWindowMinutes (+12 more)

### Community 113 - "RequestBody"
Cohesion: 0.08
Nodes (27): Request, Body, Id, RequestBody, Quantity, Reason, RequestBodyValidator, RequestValidator (+19 more)

### Community 114 - "Response"
Cohesion: 0.17
Nodes (11): Endpoint, Response, BirthDate, CreatedOn, Email, Enabled, FirstName, Id (+3 more)

### Community 115 - "SearchProductsByTermRequestHandler"
Cohesion: 0.38
Nodes (5): SearchProductsByTermRequest, SearchProductsByTermResponse, Endpoint, PageResponse, SearchProductsByTermRequestHandler

### Community 116 - "Request"
Cohesion: 0.20
Nodes (9): ProviderStatusResolution, Mapped, Unrecognized, Request, DeliveryId, ProviderReference, ReservationId, Status (+1 more)

### Community 118 - "Response"
Cohesion: 0.20
Nodes (10): Response, ActingUserId, ActingUserName, CreatedOn, Id, Quantity, QuantityOnHandAfter, Reason (+2 more)

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
Cohesion: 0.11
Nodes (14): IdentityScheme, Email, PhoneNumber, IdentitySchemeOptions, Scheme, IdentitySchemeOptionsValidator, Setup, Endpoint (+6 more)

### Community 124 - "system_diagnostics"
Cohesion: 0.10
Nodes (4): BackgroundJobs.Telemetry, Products.Endpoints.Products.v1.Import, BackgroundJobs, Inventory.Endpoints.StockLevels.v1.BulkAdjust

### Community 125 - "ProductsOptions"
Cohesion: 0.13
Nodes (15): ProductsOptions, ExportJobConcurrentMessageLimit, ExportMaxRows, ExportPageSize, ImageMaxFileBytes, ImportBatchesPerMessage, ImportJobConcurrentMessageLimit, ImportMaxStoredRowErrors (+7 more)

### Community 126 - "Pagination and search"
Cohesion: 0.36
Nodes (3): Pagination and search, LikePattern, TrigramIndexExtensions

### Community 127 - "ResiliencyOptions"
Cohesion: 0.14
Nodes (14): ResiliencyOptions, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, Keyed, MaxRetryAttempts (+6 more)

### Community 128 - ".DownloadExportAsync"
Cohesion: 0.33
Nodes (3): Endpoint, FileDownload, FileReference

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.25
Nodes (10): Observability (OpenTelemetry), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), external, name, networks, local_shared_network, Postgres Service (mm.postgres) (+2 more)

### Community 131 - "IServiceCollection"
Cohesion: 0.06
Nodes (8): Setup, Setup, Setup, Setup, Setup, ModuleRegistry, Setup, Setup

### Community 132 - "ObjectStorageOptions"
Cohesion: 0.09
Nodes (23): ObjectStorageOptions, AccessKey, AdditionalTransientStatusCodes, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds (+15 more)

### Community 134 - "InventoryModule"
Cohesion: 0.33
Nodes (6): InventoryModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority

### Community 135 - "Request"
Cohesion: 0.20
Nodes (10): Request, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name, OwnerId, SearchTerm (+2 more)

### Community 136 - "Response"
Cohesion: 0.20
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.Create, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt, Setup

### Community 137 - "ValueTask"
Cohesion: 0.08
Nodes (21): ProblemDetailsServiceExtensions, CheckRegistrationRateLimitingPolicy, OnRejected, EmailRateLimitingPolicy, OnRejected, OtpVerifyRateLimitingPolicy, OnRejected, RegisterRateLimitingPolicy (+13 more)

### Community 138 - "services"
Cohesion: 0.25
Nodes (8): container_name, environment, image, networks, ports, restart, services, mm.aspire-dashboard

### Community 139 - "mm.postgres"
Cohesion: 0.25
Nodes (8): container_name, environment, image, networks, ports, restart, volumes, mm.postgres

### Community 140 - "Response"
Cohesion: 0.22
Nodes (7): Products.Endpoints.Products.v1.My.Search, Endpoint, Response, Description, Name, Price, Quantity

### Community 141 - "JsonSerializerOptions"
Cohesion: 0.06
Nodes (18): StrictDateTimeOffsetJsonConverter, StronglyTypedIdListReadOnlyJsonConverter, StronglyTypedIdReadOnlyJsonConverter, StronglyTypedIdReadOnlyJsonConverter, StronglyTypedIdWriteOnlyJsonConverter, PaginationCursor, Payload, TaggedValue (+10 more)

### Community 142 - "mm.rabbitmq"
Cohesion: 0.25
Nodes (8): container_name, environment, image, networks, ports, restart, volumes, mm.rabbitmq

### Community 144 - "PaginationRequest"
Cohesion: 0.22
Nodes (8): PaginationRequest, After, IncludeTotal, PageNumber, PageSize, ShouldIncludeTotal, Skip, Take

### Community 145 - "Request"
Cohesion: 0.25
Nodes (8): Request, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name, SearchTerm, RequestValidator

### Community 146 - "Response"
Cohesion: 0.17
Nodes (11): Products.Endpoints.Products.v1.My.Get, Endpoint, PackageDimensionsResponse, Response, Attributes, Description, Name, PackageDimensions (+3 more)

### Community 147 - "KeycloakPermissionClient"
Cohesion: 0.16
Nodes (10): KeycloakPaths, KeycloakPermissionClient, DecisionRepresentation, Result, PermissionRepresentation, ResourceName, Scopes, TokenErrorRepresentation (+2 more)

### Community 148 - "Response"
Cohesion: 0.22
Nodes (9): Response, Id, IsReadOnly, ProductId, ProviderReference, ProviderStatus, Quantity, ReservationDeadline (+1 more)

### Community 149 - "RowResult"
Cohesion: 0.22
Nodes (6): RowResult, Adjusted, RowResult, Created, Unchanged, Updated

### Community 150 - "InventoryOptions"
Cohesion: 0.07
Nodes (26): InventoryOptions, BulkAdjustBatchesPerMessage, BulkAdjustJobConcurrentMessageLimit, BulkAdjustMaxStoredRowErrors, BusinessTimeZoneId, DailyTotalsMaxRangeDays, MaxOccurrencesPerSeries, ProductSummaryReconciliationCron (+18 more)

### Community 151 - "Response"
Cohesion: 0.22
Nodes (8): Endpoint, Response, Address, CreatedOn, Description, Id, Name, OwnerId

### Community 153 - "NetGsmSmsGateway"
Cohesion: 0.14
Nodes (8): NetGsmSmsGateway, SendMessageBody, Msg, No, SendResponseBody, Code, Description, JobId

### Community 154 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.15
Nodes (5): KeycloakPermission, RouteHandlerBuilderExtensions, KeycloakPermissionAuthorizationHandler, KeycloakPermissionRequirement, Permission

### Community 155 - "Response"
Cohesion: 0.25
Nodes (6): Inventory.Endpoints.StockLevels.v1.DailyTotals, Endpoint, Response, CommittedReservations, CommittedUnits, Day

### Community 156 - "InterModuleRequestOptions"
Cohesion: 0.25
Nodes (7): InterModuleRequestOptions, DependencyUnavailableRetryAfterSeconds, HandlerConcurrentMessageLimit, HandlerPrefetchCount, Timeouts, TimeoutSeconds, InterModuleRequestOptionsValidator

### Community 157 - "AlertRecipientsOptions"
Cohesion: 0.29
Nodes (7): AlertRecipientsOptions, EmailHtmlBodyTemplate, EmailSubjectTemplate, EmailTextBodyTemplate, ReconcileCron, RoleName, AlertRecipientsOptionsValidator

### Community 158 - "BackgroundJobsOptions"
Cohesion: 0.09
Nodes (16): Cross-process call path, How it works, BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount (+8 more)

### Community 159 - "Response"
Cohesion: 0.22
Nodes (8): Endpoint, Response, Address, Description, Name, OwnerId, ProductCount, Status

### Community 160 - "Response"
Cohesion: 0.22
Nodes (8): Endpoint, Response, Address, Description, Name, OwnerId, ProductCount, Status

### Community 161 - "Request"
Cohesion: 0.22
Nodes (9): Request, Body, Id, RequestBody, Address, Description, Name, RequestBodyValidator (+1 more)

### Community 162 - "ProductTemplateId"
Cohesion: 0.05
Nodes (34): ProductTemplate, Brand, Color, IsActive, Model, Products, ProductTemplateId, Request (+26 more)

### Community 163 - "NotificationsTelemetry"
Cohesion: 0.13
Nodes (6): RequireFeatureFilter, RouteHandlerBuilderExtensions, IamTelemetry, InventoryTelemetry, NotificationsTelemetry, ProductsTelemetry

### Community 164 - "SendRequestBody"
Cohesion: 0.29
Nodes (6): SendRequestBody, AppName, Encoding, IysFilter, Messages, MsgHeader

### Community 165 - "AuditLogOptions"
Cohesion: 0.33
Nodes (6): AuditLogOptions, PerSchemaRetentionDays, PurgeBatchSize, RetentionCron, RetentionDays, AuditLogOptionsValidator

### Community 166 - "JobStatus"
Cohesion: 0.29
Nodes (6): JobStatus, Failed, Queued, Running, Succeeded, FileRow

### Community 167 - "PaginationResponse"
Cohesion: 0.05
Nodes (26): AuditLogDto, ICurrentUser, Id, IdAsString, Roles, SessionId, PaginationResponse, HasNext (+18 more)

### Community 168 - "ProductTemplates/v1/Search/Request.cs"
Cohesion: 0.33
Nodes (6): Request, Brand, Color, Model, SearchTerm, RequestValidator

### Community 169 - "Response"
Cohesion: 0.11
Nodes (19): Products.Endpoints.Products.v1.Get, GetStockLevelRequest, GetStockLevelResponse, GetStockLevelRequestHandler, Setup, Endpoint, Request, Id (+11 more)

### Community 171 - "Commit/Request.cs"
Cohesion: 0.29
Nodes (7): Request, Body, Id, RequestBody, ProviderReference, RequestBodyValidator, RequestValidator

### Community 172 - "Common.Domain.Events"
Cohesion: 0.04
Nodes (52): Products.Domain.Products.DomainEvents.v1, GetProductRequest, GetProductResponse, AttributeSnapshot, V1ProductAttributesReplacedDomainEvent, V1ProductAttributesReplacedDomainEventExtensions, V1ProductDescriptionUpdatedDomainEvent, V1ProductImageAddedDomainEvent (+44 more)

### Community 173 - "MassTransitInterModuleRequestClient"
Cohesion: 0.40
Nodes (3): Inter-module requests, MassTransitInterModuleRequestClient, ProductsCacheKeys

### Community 174 - "CachingOptions.cs"
Cohesion: 0.16
Nodes (13): CachingEntryDefaults, Duration, FactoryHardTimeout, FactorySoftTimeout, FailSafeMaxDuration, FailSafeThrottleDuration, CachingOptionsValidator, Redis (+5 more)

### Community 175 - "AlertRecipientsReconciliationJob"
Cohesion: 0.14
Nodes (11): BackgroundJobs, GetUsersInRolePageRequest, GetUsersInRolePageResponse, RoleUserSummary, JobMetricsFilter, SkipOverlappingRecurringJobFilter, GetUsersInRolePageRequestHandler, AlertRecipientsReconcileJobRegistrar (+3 more)

### Community 176 - "IInventoryDbContext"
Cohesion: 0.06
Nodes (20): IWarehouseGateway, IWarehouseGatewayFactory, WarehouseStockQuantity, IInventoryDbContext, ProductSummaries, StockBulkAdjustJobs, StockLevels, StockMovements (+12 more)

### Community 177 - "CorsOptions"
Cohesion: 0.29
Nodes (7): CorsOptions, AllowCredentials, AllowedHeaders, AllowedMethods, AllowedOrigins, MaxAgeInSeconds, CorsOptionsValidator

### Community 178 - "AuditableEntityResponse"
Cohesion: 0.08
Nodes (22): AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, Endpoint, Response (+14 more)

### Community 179 - "ReCaptchaResponse"
Cohesion: 0.29
Nodes (6): ReCaptchaResponse, ChallengeTs, ErrorCodes, Hostname, Score, Success

### Community 180 - "SmsOptions"
Cohesion: 0.09
Nodes (23): SmsOptions, AppName, AttemptTimeoutSeconds, BaseUrl, MaxPerDay, MaxPerPhoneNumberPerDay, MaxRetryAttempts, MsgHeader (+15 more)

### Community 181 - "RabbitMqOptions"
Cohesion: 0.15
Nodes (13): RabbitMqOptions, DefaultConcurrentMessageLimit, DefaultPrefetchCount, Host, Password, Port, RetryIntervalDeltaMs, RetryLimit (+5 more)

### Community 182 - "Request"
Cohesion: 0.29
Nodes (6): WarehouseConnectionStatus, Active, Deactivated, Request, Status, StoreId

### Community 183 - "SendRequestBody"
Cohesion: 0.13
Nodes (12): SendContact, Email, Name, SendErrorBody, Code, Message, SendRequestBody, HtmlContent (+4 more)

### Community 184 - "ChunkEncodingFreeS3Client"
Cohesion: 0.17
Nodes (3): ChunkEncodingFreeS3Client, DisableChunkEncodingHandler, Setup

### Community 185 - "SecretProtectionOptions"
Cohesion: 0.28
Nodes (5): Troubleshooting, SecretProtectionOptions, EncryptionKey, PreviousEncryptionKeys, SecretProtectionOptionsValidator

### Community 186 - "Uri"
Cohesion: 0.16
Nodes (6): Request, ApiCredential, BaseUrl, Provider, StoreId, RequestValidator

### Community 187 - "Request"
Cohesion: 0.29
Nodes (7): Request, Body, Id, RequestBody, ApiCredential, RequestBodyValidator, RequestValidator

### Community 188 - "BoundedCaptureStream"
Cohesion: 0.14
Nodes (7): ImageSignature, BoundedCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 189 - "ProductImportJob"
Cohesion: 0.15
Nodes (13): ProductImportJob, ClientRequestId, FileName, LastProcessedRow, ObjectKey, ProductsCreated, ProductsUpdated, ProductTemplateId (+5 more)

### Community 190 - "AI-assisted development workflow"
Cohesion: 0.25
Nodes (8): Adapting it, AI-assisted development workflow, `CLAUDE.md`, Guards, The commands, The four layers, The issue-to-pull-request automation, The knowledge graph

### Community 191 - "Response"
Cohesion: 0.33
Nodes (4): Products.Endpoints.Stores.v1.AddProduct, Endpoint, Response, Id

### Community 193 - "Response"
Cohesion: 0.08
Nodes (21): BulkImportOptions, MaxFileBytes, MaxRows, SaveChunkSize, BulkImportOptionsValidator, Response, FailureKey, FileName (+13 more)

### Community 194 - "Keycloak realm as code"
Cohesion: 0.22
Nodes (9): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Realms that already exist, Secrets, Seed users (dev / test only) (+1 more)

### Community 195 - "Response"
Cohesion: 0.22
Nodes (8): Inventory.Endpoints.StockReservations.v1.Reserve, Endpoint, Request, Body, RequestBodyValidator, RequestValidator, Response, Id

### Community 196 - "Response"
Cohesion: 0.29
Nodes (5): Inventory.Endpoints.WarehouseConnections.v1.RotateWebhookSecret, Endpoint, Response, WebhookPath, WebhookSecret

### Community 197 - "Your first requests"
Cohesion: 0.33
Nodes (6): Connect a store to a warehouse system, Errors, Queued work: accept, queue, poll, Sign in, Watch two modules cooperate, Your first requests

### Community 199 - "Request"
Cohesion: 0.25
Nodes (8): Request, ClientId, DeviceId, DeviceName, Otp, PhoneNumber, PushToken, RequestValidator

### Community 201 - "KeycloakScopes"
Cohesion: 0.17
Nodes (11): Devices, Hangfire, KeycloakScopes, Products, ProductTemplates, Sessions, StockLevels, StockReservations (+3 more)

### Community 202 - "IamModule"
Cohesion: 0.33
Nodes (6): IamModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority

### Community 203 - "Response"
Cohesion: 0.29
Nodes (4): Products.Endpoints.ProductTemplates.v1.Create, Setup, Response, Id

### Community 205 - "Response"
Cohesion: 0.33
Nodes (4): Products.Endpoints.Stores.v1.My.AddProduct, Endpoint, Response, Id

### Community 210 - "Key decisions"
Cohesion: 0.29
Nodes (7): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Key decisions

### Community 212 - "ProviderStatusSnapshot"
Cohesion: 0.29
Nodes (7): ProviderStatusSnapshot, Active, Committed, Expired, Released, RequiresReconciliation, Unknown

### Community 213 - "SmsCategory"
Cohesion: 0.33
Nodes (4): SmsCategory, CommercialIndividual, CommercialMerchant, Transactional

### Community 214 - "Response"
Cohesion: 0.25
Nodes (6): Inventory.Endpoints.WarehouseConnections.v1.Create, Endpoint, Response, Id, WebhookPath, WebhookSecret

### Community 215 - "Response"
Cohesion: 0.17
Nodes (10): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, Endpoint, Response, ClientId, DeviceName, Id, IpAddress, IsCurrent (+2 more)

### Community 216 - "Response"
Cohesion: 0.33
Nodes (4): Inventory.Endpoints.StockReservations.v1.ReserveSeries, Endpoint, Response, Ids

### Community 217 - "Platform building blocks"
Cohesion: 0.12
Nodes (16): Authorization, Caching, Dirty set and lease, Feature flags, Job rows, Observability, Options and configuration, Platform building blocks (+8 more)

### Community 218 - "Stores/v1/Create/Request.cs"
Cohesion: 0.33
Nodes (6): Request, Address, Description, Name, OwnerId, RequestValidator

### Community 219 - "StoreId"
Cohesion: 0.06
Nodes (31): ProductExportJob, FileName, ObjectKey, StoreId, StoreId, Request, StoreId, RequestValidator (+23 more)

### Community 220 - "SendForEmail/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForEmail, Request, CaptchaToken, Email, RequestValidator

### Community 222 - "How it works"
Cohesion: 0.33
Nodes (6): Cross-module term search, How it works, One-time setup _(Build, in a migration)_, Query path, Ranking, Write path

### Community 223 - "Response"
Cohesion: 0.29
Nodes (5): Products.Endpoints.Stores, Setup, Endpoint, Response, Id

### Community 224 - "IServiceProvider"
Cohesion: 0.19
Nodes (7): Common.Application.EndpointFilters, ProblemResponse, ResultToAcceptedResponseTransformer, ResultToCreatedResponseTransformer, ResultToResponseTransformer, ResultToResponseTransformer, RouteHandlerBuilderExtensions

### Community 225 - "Stores/v1/AuditLog/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Stores.v1.AuditLog, Request, Id, RequestValidator

### Community 226 - "Split-Deployment PoC"
Cohesion: 0.33
Nodes (6): Concurrent safety, Files added by this PoC, How to run, Split-Deployment PoC, What this proves, What to check

### Community 228 - "ReverseProxyOptions"
Cohesion: 0.33
Nodes (5): ReverseProxyOptions, ForwardLimit, IsEnabled, TrustedNetworks, ReverseProxyOptionsValidator

### Community 229 - "Products/v1/AuditLog/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Products.v1.AuditLog, Request, Id, RequestValidator

### Community 230 - "DevicesOptions"
Cohesion: 0.40
Nodes (5): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator

### Community 231 - "HealthCheckOptions"
Cohesion: 0.40
Nodes (5): HealthCheckOptions, EnableHealthChecks, ReadinessTimeoutInSeconds, StartupTimeoutInSeconds, HealthCheckOptionsValidator

### Community 232 - "SignalROptions"
Cohesion: 0.40
Nodes (5): SignalROptions, RedisConnectionString, RequireRedisBackplaneInProduction, UseRedisBackplane, SignalROptionsValidator

### Community 234 - ".AddCommonPersistence"
Cohesion: 0.40
Nodes (4): DatabaseOptions, ConnectionString, DatabaseOptionsValidator, Setup

### Community 235 - "GetImport/Request.cs"
Cohesion: 0.67
Nodes (3): Request, Id, RequestValidator

### Community 236 - "OutboxCleanupSettings"
Cohesion: 0.40
Nodes (5): OutboxCleanupSettings, BatchSize, CronSchedule, Enabled, RetentionDays

### Community 237 - "ProvisionUserResponseExtensions.cs"
Cohesion: 0.15
Nodes (7): Products.Endpoints.Stores.v1.Apply, Products.Domain.Stores.Errors, StoreErrors, ProvisionUserResponseExtensions, Response, Id, VerifyEmailOtpResponseExtensions

### Community 238 - "InboxStore"
Cohesion: 0.40
Nodes (4): IInboxCleanupTarget, ModuleName, InboxStore, ModuleName

### Community 239 - "Request"
Cohesion: 0.40
Nodes (5): Request, ClientRequestId, File, Reason, RequestValidator

### Community 241 - "Request"
Cohesion: 0.40
Nodes (5): Request, ClientRequestId, File, ProductTemplateId, RequestValidator

### Community 242 - "Response"
Cohesion: 0.25
Nodes (6): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 243 - "GetExport/Request.cs"
Cohesion: 0.67
Nodes (3): Request, Id, RequestValidator

### Community 244 - "Request"
Cohesion: 0.40
Nodes (5): Request, Brand, Color, Model, RequestValidator

### Community 245 - "Request"
Cohesion: 0.40
Nodes (5): Request, Address, Name, SearchTerm, RequestValidator

### Community 247 - "ListImports/Request.cs"
Cohesion: 0.67
Nodes (3): Products.Endpoints.Products.v1.ListImports, Request, RequestValidator

### Community 248 - "MovementTypeSnapshot"
Cohesion: 0.50
Nodes (4): MovementTypeSnapshot, Adjusted, Received, ReservationCommitted

### Community 250 - "Request"
Cohesion: 0.22
Nodes (9): Request, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken, Password, PushToken (+1 more)

### Community 251 - "Response"
Cohesion: 0.33
Nodes (4): Products.Endpoints.Stores.v1.My.Create, Endpoint, Response, Id

### Community 261 - "My/AuditLog/Request.cs"
Cohesion: 0.67
Nodes (3): Products.Endpoints.Stores.v1.My.AuditLog, Request, RequestValidator

### Community 262 - "ListBulkAdjustments/Request.cs"
Cohesion: 0.67
Nodes (3): Inventory.Endpoints.StockLevels.v1.ListBulkAdjustments, Request, RequestValidator

### Community 264 - "WarehouseConnections/v1/My/Search/Request.cs"
Cohesion: 0.67
Nodes (3): Inventory.Endpoints.WarehouseConnections.v1.My.Search, Request, RequestValidator

### Community 267 - "Request"
Cohesion: 0.50
Nodes (4): Request, CaptchaToken, PhoneNumber, RequestValidator

### Community 268 - "Request"
Cohesion: 0.50
Nodes (4): Request, CaptchaToken, PhoneNumber, RequestValidator

### Community 269 - "RouteGroupBuilder"
Cohesion: 0.03
Nodes (35): Setup, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint (+27 more)

### Community 270 - "Response"
Cohesion: 0.50
Nodes (3): Response, Id, Url

### Community 271 - "RowOutcome"
Cohesion: 0.40
Nodes (5): RowOutcome, Created, Rejected, Unchanged, Updated

### Community 274 - "ModulesOptions"
Cohesion: 0.67
Nodes (3): ModulesOptions, EnabledModules, ModulesOptionsValidator

### Community 275 - "ObjectStorageRetryMode"
Cohesion: 0.67
Nodes (3): ObjectStorageRetryMode, Adaptive, Standard

### Community 276 - "SensitivePathRule"
Cohesion: 0.67
Nodes (3): SensitivePathRule, Methods, Path

### Community 279 - "Request"
Cohesion: 0.67
Nodes (3): Request, RefreshToken, RequestValidator

### Community 282 - "Request"
Cohesion: 0.67
Nodes (3): Request, Id, RequestValidator

### Community 285 - "Common.Domain.Events"
Cohesion: 0.03
Nodes (39): Inventory.Domain.StockReservations.DomainEvents.v1, Common.Domain.Events, Products.Domain.Stores.DomainEvents.v2, Inventory.Application.StockReservations.DomainEventHandlers.v1, Products.Application.Stores.DomainEventHandlers.v1, Inventory.Application.StockLevels.DomainEventHandlers.v1, Products.Domain.Stores.DomainEvents.v1, Inventory.Domain.StockLevels.DomainEvents.v1 (+31 more)

### Community 287 - "Request"
Cohesion: 0.67
Nodes (3): Request, Id, RequestValidator

### Community 323 - "fluentvalidation"
Cohesion: 0.04
Nodes (43): Products.Endpoints.Stores.v1.My.Update, Products.Endpoints.Products.v1.GetImport, Products.Endpoints.Products.v1.My.SetAttributes, Inventory.Endpoints.StockReservations.v1.Commit, Products.Endpoints.Stores.v1.Suspend, Inventory.Endpoints.StockLevels.v1.GetBulkAdjustment, Inventory.Endpoints.StockLevels.v1.Receive, Products.Endpoints.Stores.v1.Update (+35 more)

### Community 324 - "Request"
Cohesion: 0.20
Nodes (10): Request, Body, Id, RequestBody, HeightCm, LengthCm, WeightKg, WidthCm (+2 more)

## Knowledge Gaps
- **1253 isolated node(s):** `UserId`, `Id`, `IdAsString`, `Roles`, `SessionId` (+1248 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1651 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **124 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `Common.InterModuleRequests.Products`, `fluentvalidation`, `IServiceCollection`, `microsoft_entityframeworkcore`, `Setup.Observability.cs`, `Common.InterModuleRequests.Contracts`, `Common.Domain.ResultMonad`, `Common.Infrastructure.Extensions`, `CachingOptions.cs`, `EmailOptions`, `Infrastructure/Setup.cs`, `KeycloakPermissionAuthorizationHandler.cs`, `PushOptions`, `Setup.ForwardedHeaders.cs`?**
  _High betweenness centrality (0.077) - this node is a cross-community bridge._
- **What connects `UserId`, `Id`, `IdAsString` to the rest of the system?**
  _1253 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `DefaultIdType` be split into smaller, more focused modules?**
  _Cohesion score 0.047058823529411764 - nodes in this community are weakly interconnected._
- **Why does `Result` connect `Result` to `.DownloadExportAsync`, `DefaultIdType`, `ApplicationUserId`, `.HandleWarehouseWebhookAsync`, `WarehouseConnectionId`, `IKeycloakAdminClient`, `NetGsmSmsGateway`, `TimeProvider`, `Common.Domain.Events`, `Error`, `DateTimeOffset`, `ProductTemplateId`, `PaginationResponse`, `Response`, `SmsOtpDispatchOutcome`, `Common.Domain.Events`, `ProductImportJobRunner`, `IInventoryDbContext`, `CaptchaOptions`, `.SaveBatchAsync`, `.ReceiveAsync`, `S3ObjectStoreCore`, `PackageDimensions`, `IIntegrationEventOutbox`, `NotificationPayload`, `EmailOtpDispatchOutcome`, `SecretProtector`, `ILogger`, `StockBulkAdjustJobRunner`, `KeycloakTokenClient`, `Task`, `ProvisionUserResponseExtensions.cs`, `SearchProductsByTermRequestHandler`, `IResult`?**
  _High betweenness centrality (0.051) - this node is a cross-community bridge._
- **Should `HttpContext` be split into smaller, more focused modules?**
  _Cohesion score 0.12643678160919541 - nodes in this community are weakly interconnected._
- **Why does `Common.Domain.ResultMonad` connect `Common.Domain.ResultMonad` to `Common.InterModuleRequests.Products`, `Result`, `Common.Infrastructure.Extensions`, `EmailOptions`, `Infrastructure/Setup.cs`, `Common.Application.Options`, `StockBulkAdjustErrors.cs`, `Error`, `ProductImportErrors.cs`, `CaptchaErrors.cs`, `OtpErrors.cs`, `microsoft_entityframeworkcore`, `CaptchaOptions`, `FirebasePushGateway`, `EmailOtpDispatchOutcome`, `SmsCategory`, `IServiceProvider`, `Common.InterModuleRequests.Contracts`, `ProvisionUserResponseExtensions.cs`, `IResult`, `StockLevelErrors.cs`, `system_diagnostics`?**
  _High betweenness centrality (0.045) - this node is a cross-community bridge._
- **Should `Common.Application.DTOs` be split into smaller, more focused modules?**
  _Cohesion score 0.1111111111111111 - nodes in this community are weakly interconnected._