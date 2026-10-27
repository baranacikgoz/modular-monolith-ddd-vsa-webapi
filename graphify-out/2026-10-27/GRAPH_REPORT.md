# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-10-27)

## Corpus Check
- 702 files · ~128,189 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 4769 nodes · 13298 edges · 314 communities (191 shown, 123 thin omitted)
- Extraction: 96% EXTRACTED · 4% INFERRED · 0% AMBIGUOUS · INFERRED: 510 edges (avg confidence: 0.86)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `d3fdff97`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- PaginationResponse
- IModule
- HttpContext
- AuditableEntityResponse
- ApplicationUserId
- AuditableEntity
- .AddAuditingInterceptors
- TimeProvider
- OpenApiOptions
- Task
- Common.Domain.ResultMonad
- ProductId
- HealthCheckOptions
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
- Stores/v1/My/Update/Request.cs
- Response
- RequestResponseBodyLoggingMiddleware
- CreateByEmail/Request.cs
- IOptions
- Error
- OutboxProcessor
- SendSecurityAlertRequestHandler
- Response
- IAM.Application.Captcha.Services
- OutboxMessage
- CustomRateLimitingOptions
- AdminRepresentations.cs
- Common.Infrastructure.Persistence.EntityConfigurations
- ObservabilityOptions
- NotifyRoleOnCriticalAlertRaisedHandler
- ProductSummariesReconciliationJob
- S3ObjectStoreCore.cs
- SmsOtpDispatchOutcome
- ProductImportJob
- OutboxModule
- KeyedRow
- Outbox Misuse Check
- ProductsDbContext
- Add Integration Event Command
- host
- BackgroundJobsOptions
- CaptchaOptions
- KeyedResiliencePipelines
- .SetRetryAfterHeader
- BrevoEmailGateway
- FirebasePushGateway
- Cross-Module Reference Violation
- OutboxOptions
- Request
- Bogus Test Data
- DateTimeOffset
- v1/RemoveProduct/Request.cs
- RequestLoggingOptions
- S3ObjectStoreCore
- Task
- IAutoMigrateMarker.cs
- .AddNotificationsSignalR
- ValueObject
- Request
- DefaultIdType
- Getting started
- system_net
- VerifyPhoneOtpResponse
- AsNoTracking Coverage Check
- IntegrationEventOutbox
- Response
- ProvisionUserRequestHandler
- KeycloakOptions
- Response
- SendEmailOtpRequestHandler
- Response
- RouteGroupBuilder
- OtpOptions
- EntityTypeBuilder
- DomainEvent
- InventoryDbContext
- PushOptions
- Configuration-Driven Module Registration
- KeycloakPermissionAuthorizationHandler.cs
- Setup
- ProductExportJobRunner
- .DownloadExportAsync
- .MapEndpoint
- NotificationsHub
- Response
- ILogger
- ProductImportJobRunner
- Response
- ServiceAccountTokenCache
- IInboxStore
- ResiliencyOptions
- .SendAsync
- Stores/v1/Update/Request.cs
- ResultToResponseTransformer.cs
- Response
- Common.InterModuleRequests.Contracts
- .AddCommonObjectStorage
- INotificationsDbContext
- Response
- KeycloakPermissionPolicyProvider
- ProjectionUpsertOutcome
- IReadOnlyCollection
- Response
- Response
- RequestBody
- ProcessedMessage
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
- .SendAsync
- docker-compose.yml (Base Stack)
- IServiceCollection
- ObjectStorageOptions
- Response
- ProjectionReconciliationJob
- AddImage/Request.cs
- .WriteTooManyRequestsToResponse
- services
- mm.postgres
- StockLevel
- JsonSerializerOptions
- mm.rabbitmq
- DateOnly
- AI-assisted development workflow
- FirebasePushGateway.cs
- Response
- StoreId
- .ReleaseStockReservationAsync
- ReCaptchaService
- ConcurrencyConfiguratorExtensions
- ChangeLowStockThreshold/Request.cs
- system_globalization
- WebhookCallback/Request.cs
- IdentityScheme
- JwtBearerConfigureOptions
- InterModuleRequestOptions
- AlertRecipientsOptions
- .SearchProductTemplatesAsync
- Stores/v1/Create/Request.cs
- Response
- KeycloakPermission
- Response
- For
- SearchProductsByTermRequestHandler
- ApplicationUserId
- NotificationPayload
- FirebaseServiceAccountOptions
- ObjectListPage
- .GetProductAsync
- Notifications.Application/IAssemblyReference.cs
- StringExtensions
- DomainEvent
- IProductsDbContext
- CachingOptions.cs
- IInterModuleRequest
- .HandleWarehouseWebhookAsync
- CorsOptions
- FeatureFlags
- CachingEntryDefaults
- SmsOptions
- RabbitMqOptions
- IAM.Domain
- SendRequestBody
- SendRequestBody
- Response
- ReCaptchaResponse
- fluentvalidation
- BoundedCaptureStream
- .HandleAsync
- IOtpService
- Infrastructure/StringExtensions.cs
- Result
- .EnqueueAsync
- Modules
- Response
- SecretProtector
- Response
- StronglyTypedIdBinder.cs
- UploadObjectRequest
- ActivitySource
- KeycloakScopes
- DevicesOptions
- v1/Request.cs
- My/RemoveProduct/Request.cs
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- ResultToResponseTransformer.cs
- GetExport/Request.cs
- Split-Deployment PoC
- ProblemDetailsExtensions.cs
- RowOutcome
- Response
- SendResponseBody
- Platform building blocks
- .TryReadFromJsonAsync
- .GetVariantAsync
- .TryDetect
- Keycloak realm as code
- .VerifySha256
- JwtClaimNames.cs
- KeycloakRoles.cs
- StockReservations/Constants.cs
- IAM.Endpoints
- Notifications.Infrastructure
- SecretGenerator
- NetGsmSmsGateway
- RequestBodyLimitMetadata
- Response
- Commit/Request.cs
- .CreateMyStoreAsync
- Products.Domain/IAssemblyReference.cs
- IAM.Application/IAssemblyReference.cs
- Products.Infrastructure/IAssemblyReference.cs
- Products.Application/IAssemblyReference.cs
- Common.Infrastructure/IAssemblyReference.cs
- Notifications.Domain/IAssemblyReference.cs
- IAM.Infrastructure/IAssemblyReference.cs
- ReCaptchaResponse
- RouteGroupBuilder
- InterModuleRequestHandler
- SendForEmail/Request.cs
- AuditLogOptions
- Products.Endpoints
- Products/v1/Update/Request.cs
- Common.Application.Options
- Common.Application.Validation
- CustomValidator
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
- README (Boilerplate Overview)
- REPR Pattern (Minimal API Endpoints)

## God Nodes (most connected - your core abstractions)
1. `Result` - 217 edges
2. `Common.Application.Options` - 166 edges
3. `Common.Domain.ResultMonad` - 134 edges
4. `CustomValidator` - 100 edges
5. `ApplicationUserId` - 96 edges
6. `Common.Application.Validation` - 88 edges
7. `Common.Application.Auth` - 85 edges
8. `Common.Domain.StronglyTypedIds` - 84 edges
9. `Common.Application.Extensions` - 78 edges
10. `Common.InterModuleRequests.Contracts` - 69 edges

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

## Communities (314 total, 123 thin omitted)

### Community 0 - "PaginationResponse"
Cohesion: 0.14
Nodes (11): AuditLogDto, PaginationResponse, HasNext, HasPrevious, HasTotal, NextPageNumber, PreviousPageNumber, TotalPages (+3 more)

### Community 1 - "IModule"
Cohesion: 0.05
Nodes (31): Cross-process call path, How it works, Setup, ICoreModule, IModule, ActivitySourceNames, MeterNames, Name (+23 more)

### Community 2 - "HttpContext"
Cohesion: 0.09
Nodes (25): Rate limiting and resilience, FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit, RequestBodyLimitEndpointExtensions, RequestBodyLimitMetadata (+17 more)

### Community 3 - "AuditableEntityResponse"
Cohesion: 0.09
Nodes (16): Products.Endpoints.Stores.v1.Search, Products.Endpoints.ProductTemplates.v1.Search, Common.Application.DTOs, Products.Endpoints.Products.v1.My.Search, Products.Endpoints.ProductTemplates.v1.Get, AuditableEntityResponse, CreatedBy, CreatedOn (+8 more)

### Community 5 - "ApplicationUserId"
Cohesion: 0.10
Nodes (21): ApplicationUserId, IsEmpty, Value, IKeycloakAdminClient, CreateKeycloakUser, GrantedPermission, KeycloakUser, KeycloakUserPage (+13 more)

### Community 6 - "AuditableEntity"
Cohesion: 0.10
Nodes (19): AuditableEntity, AuditableEntity, Id, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, Version (+11 more)

### Community 8 - "TimeProvider"
Cohesion: 0.12
Nodes (14): JobHousekeepingJob, JobHousekeepingOptions, PageSize, RetentionHours, StaleAfterMinutes, JobHousekeepingOptionsValidator, IDbContext, AuditLog (+6 more)

### Community 9 - "OpenApiOptions"
Cohesion: 0.06
Nodes (16): Host.Swagger, OpenApiOptions, ContactEmail, ContactName, Description, EnableSwagger, LicenseName, LicenseUrl (+8 more)

### Community 10 - "Task"
Cohesion: 0.10
Nodes (3): FeatureFlagResultExtensions, AsyncExtensions, ProductsSearchCacheExtensions

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.09
Nodes (26): IAM.Endpoints.Captcha.VersionNeutral, Common.Endpoints.Webhooks, Products.Infrastructure.InterModuleRequestHandlers, Common.Application.Search, Products.Application.Exports, Products.Endpoints.Stores.v1.My.AddProduct, Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions (+18 more)

### Community 12 - "ProductId"
Cohesion: 0.06
Nodes (19): Products.Endpoints.Products.v1.AddImage, IStronglyTypedId, Value, StronglyTypedIdHelper, DeviceRegistrationId, V1ProductImageAddedDomainEvent, V1ProductImageRemovedDomainEvent, ProductId (+11 more)

### Community 13 - "HealthCheckOptions"
Cohesion: 0.18
Nodes (10): Localization, HealthCheckOptions, EnableHealthChecks, ReadinessTimeoutInSeconds, StartupTimeoutInSeconds, HealthCheckOptionsValidator, ResxLocalizationOptions, DefaultCulture (+2 more)

### Community 14 - "Response"
Cohesion: 0.13
Nodes (13): IAM.Endpoints.Users.VersionNeutral.Me.Get, Endpoint, Response, BirthDate, CreatedOn, Email, FirstName, Id (+5 more)

### Community 15 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.22
Nodes (4): AccessTokenReader, KeycloakPermissionAuthorizationHandler, KeycloakPermissionRequirement, Permission

### Community 16 - "EmailOptions"
Cohesion: 0.10
Nodes (23): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+15 more)

### Community 17 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.07
Nodes (15): Products.Domain.Products.DomainEvents.v1, Inventory.Domain.StockLevels.Errors, Common.Endpoints, Common.Domain.StronglyTypedIds, Common.Domain.Events, IAM.Endpoints.Users.VersionNeutral.Get, Inventory.Domain.StockReservations.Errors, Common.Application.JsonConverters (+7 more)

### Community 18 - "Common.Application.Options"
Cohesion: 0.04
Nodes (31): Common.Infrastructure.Modules, Products.Infrastructure.Persistence, Common.InterModuleRequests, Notifications.Infrastructure.Devices, Inventory.Endpoints, Products.Endpoints.Stores, Common.Endpoints.Versioning, Notifications.Infrastructure.Email (+23 more)

### Community 19 - "microsoft_extensions_options"
Cohesion: 0.06
Nodes (26): Common.Infrastructure.Persistence.Inbox, Inventory.Endpoints.StockLevels.v1.Search, Common.Infrastructure.Persistence, Inventory.Infrastructure.IntegrationEventHandlers, Common.Application.Persistence.Inbox, Products.Infrastructure.Imports, Inventory.Application.Persistence, Inventory.Domain.StockCounters (+18 more)

### Community 20 - "FullTextSearchOptions"
Cohesion: 0.07
Nodes (30): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Add a new language/culture, Add search to a new entity _(Build checklist)_ (+22 more)

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.12
Nodes (6): BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 22 - "DeviceRegistration"
Cohesion: 0.15
Nodes (10): DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive, LastReconciledOn, PushToken, PushTokenUpdatedOn (+2 more)

### Community 23 - "DeviceRegistryReconciliationService"
Cohesion: 0.40
Nodes (5): GetActiveSessionIdsRequest, GetActiveSessionIdsResponse, UserSessionIds, GetActiveSessionIdsRequestHandler, DeviceRegistryReconciliationService

### Community 24 - ".RegisterAsync"
Cohesion: 0.09
Nodes (16): IInterModuleRequestClient, BindDeviceSessionRequest, BindDeviceSessionResponse, DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse, VerifyEmailOtpRequest, VerifyEmailOtpResponse, ICaptchaService (+8 more)

### Community 25 - "Stores/v1/My/Update/Request.cs"
Cohesion: 0.33
Nodes (6): Products.Endpoints.Stores.v1.My.Update, Request, Address, Description, Name, RequestValidator

### Community 26 - "Response"
Cohesion: 0.29
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, Endpoint, Response, EmailVerificationToken, IsRegistered

### Community 27 - "RequestResponseBodyLoggingMiddleware"
Cohesion: 0.21
Nodes (4): SensitivePathRule, Methods, Path, RequestResponseBodyLoggingMiddleware

### Community 28 - "CreateByEmail/Request.cs"
Cohesion: 0.07
Nodes (25): IAM.Domain.Users, IAM.Endpoints.Otp.VersionNeutral.SendForLogin, IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, IAM.Endpoints.Common.Validations, Constants, Request, CaptchaToken, PhoneNumber (+17 more)

### Community 29 - "IOptions"
Cohesion: 0.12
Nodes (10): Configuration, File map _(Build)_, ISearchLanguageResolver, UniversalConfig, SearchLanguageResolver, UniversalConfig, ISearchLocalized, Language (+2 more)

### Community 30 - "Error"
Cohesion: 0.08
Nodes (24): Error, Key, ParameterName, StatusCode, SubErrors, Value, IResult, Error (+16 more)

### Community 31 - "OutboxProcessor"
Cohesion: 0.18
Nodes (5): OutboxProcessor, PublishOutcome, Failed, Published, Skipped

### Community 32 - "SendSecurityAlertRequestHandler"
Cohesion: 0.52
Nodes (5): SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, SendSecurityAlertRequestHandler

### Community 33 - "Response"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.Refresh, Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 34 - "IAM.Application.Captcha.Services"
Cohesion: 0.29
Nodes (4): IAM.Domain.Captcha, IAM.Infrastructure.Captcha.Services, IAM.Application.Captcha.Services, IAM.Infrastructure.Captcha

### Community 35 - "OutboxMessage"
Cohesion: 0.08
Nodes (21): IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, OutboxMessage, CreatedOn (+13 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.07
Nodes (19): CustomRateLimitingOptions, CheckRegistration, CreateStore, Email, ExemptPathPrefixes, Global, OtpVerify, Register (+11 more)

### Community 37 - "AdminRepresentations.cs"
Cohesion: 0.11
Nodes (17): CredentialRepresentation, Temporary, Type, Value, ErrorRepresentation, Error, ErrorDescription, ErrorMessage (+9 more)

### Community 38 - "Common.Infrastructure.Persistence.EntityConfigurations"
Cohesion: 0.12
Nodes (9): Inventory.Infrastructure.Persistence.EntityConfigurations, Notifications.Domain.Devices, Outbox.Persistence, Common.Application.Persistence.Outbox, Notifications.Domain.Alerts, Common.Infrastructure.Persistence.EntityConfigurations, Common.Infrastructure.Persistence.ValueConverters, Notifications.Infrastructure.Persistence.EntityConfigurations (+1 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.08
Nodes (18): ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics, EnableTracing, LogSink, MinimumLevel (+10 more)

### Community 41 - "ProductSummariesReconciliationJob"
Cohesion: 0.20
Nodes (8): GetProductsPageRequest, GetProductsPageResponse, ProductPageItem, ProductSummariesReconciliationCursor, ProductSummariesReconciliationJob, ProductSummariesReconciliationJobRegistrar, Setup, GetProductsPageRequestHandler

### Community 43 - "SmsOtpDispatchOutcome"
Cohesion: 0.33
Nodes (5): OtpDispatchErrors, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled

### Community 44 - "ProductImportJob"
Cohesion: 0.10
Nodes (21): ProductImportJob, ClientRequestId, FileName, LastProcessedRow, ObjectKey, ProductsCreated, ProductsUpdated, ProductTemplateId (+13 more)

### Community 45 - "OutboxModule"
Cohesion: 0.06
Nodes (16): Persistence conventions, IDatabaseSeeder, Priority, ApplyAuditingInterceptor, DatabaseSeederOrchestrator, SeedingCompletionTracker, InboxCleanupJob, OutboxCleanupJob (+8 more)

### Community 46 - "KeyedRow"
Cohesion: 0.50
Nodes (4): KeyedRow, Item, Sort, Tie

### Community 48 - "ProductsDbContext"
Cohesion: 0.11
Nodes (14): AuditLogEntry, AggregateId, AggregateType, Event, EventType, Version, AuditLogEntryConfiguration, ProductsDbContext (+6 more)

### Community 50 - "host"
Cohesion: 0.18
Nodes (11): context, dockerfile, build, container_name, environment, image, networks, ports (+3 more)

### Community 51 - "BackgroundJobsOptions"
Cohesion: 0.15
Nodes (8): BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount, BackgroundJobsOptionsValidator, HangfireCustomAuthorizationFilter

### Community 52 - "CaptchaOptions"
Cohesion: 0.16
Nodes (13): CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey, Provider, ScoreThreshold, SecretKey (+5 more)

### Community 53 - "KeyedResiliencePipelines"
Cohesion: 0.09
Nodes (8): FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration, HttpClientKeyedExtensions, KeyedResiliencePipelines, Setup

### Community 55 - "BrevoEmailGateway"
Cohesion: 0.14
Nodes (9): BrevoEmailGateway, SendContact, Email, Name, SendErrorBody, Code, Message, SendResponseBody (+1 more)

### Community 56 - "FirebasePushGateway"
Cohesion: 0.16
Nodes (4): IPushGateway, PushMessage, DummyPushGateway, FirebasePushGateway

### Community 58 - "OutboxOptions"
Cohesion: 0.11
Nodes (21): OutboxCleanupSettings, BatchSize, CronSchedule, Enabled, RetentionDays, OutboxCleanupSettingsValidator, OutboxOptions, BaseBackoffSeconds (+13 more)

### Community 59 - "Request"
Cohesion: 0.06
Nodes (31): Notifications.Infrastructure.Devices.UpdateCurrentPushToken, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, Common.Domain.Devices, DeviceSessionConstants, Request, ClientId, DeviceId, DeviceName (+23 more)

### Community 61 - "DateTimeOffset"
Cohesion: 0.10
Nodes (13): JobRow, FailureKey, FinishedOn, Progress, QueuedOn, RequestedBy, StartedOn, Status (+5 more)

### Community 62 - "v1/RemoveProduct/Request.cs"
Cohesion: 0.40
Nodes (5): Products.Endpoints.Stores.v1.RemoveProduct, Request, Id, ProductId, RequestValidator

### Community 63 - "RequestLoggingOptions"
Cohesion: 0.16
Nodes (12): RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes, SensitiveQueryParamPaths (+4 more)

### Community 65 - "Task"
Cohesion: 0.07
Nodes (8): IBackgroundJobs, IRecurringBackgroundJobs, CursorBound, Value, PaginationQueryableExtensions, ParameterReplacer, BackgroundJobsService, RecurringBackgroundJobsService

### Community 69 - "Request"
Cohesion: 0.15
Nodes (13): Common.Domain.Extensions, Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FirstName (+5 more)

### Community 70 - "DefaultIdType"
Cohesion: 0.06
Nodes (35): Inventory.Infrastructure.StockCounters, IntegrationEventHandlerBase, MaxEventAge, CachingOptions, AllowInMemoryOnlyInProduction, EntryDefaults, IdempotencyKeyDuration, IdempotencyL1Duration (+27 more)

### Community 71 - "Getting started"
Cohesion: 0.12
Nodes (16): Configuration, Errors, Getting started, Prerequisites, Run the API from the command line instead, Run the load test, Run the tests, Run the whole system (+8 more)

### Community 72 - "system_net"
Cohesion: 0.10
Nodes (9): IAM.Infrastructure.Keycloak.Representations, Products.Domain.Products.Errors, Inventory.Infrastructure.Gateway, Inventory.Application.Gateway, IAM.Domain.Errors, IAM.Infrastructure.Keycloak, OAuthErrors, UserAttributes (+1 more)

### Community 73 - "VerifyPhoneOtpResponse"
Cohesion: 0.27
Nodes (8): OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, VerifyPhoneOtpResponseExtensions, VerifyPhoneOtpRequestHandler

### Community 75 - "IntegrationEventOutbox"
Cohesion: 0.08
Nodes (20): Products.Application.Stores.DomainEventHandlers.v1, DomainEventHandlerBase, IEventHandler, IEventHandlerWrapper, IIntegrationEventOutbox, IntegrationEventOutbox, HasPending, Setup (+12 more)

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

### Community 80 - "SendEmailOtpRequestHandler"
Cohesion: 0.31
Nodes (7): EmailOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, SendEmailOtpRequest, SendEmailOtpResponse, SendEmailOtpRequestHandler

### Community 81 - "Response"
Cohesion: 0.25
Nodes (7): Endpoint, Response, Address, Description, Name, OwnerId, ProductCount

### Community 82 - "RouteGroupBuilder"
Cohesion: 0.07
Nodes (13): Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Setup (+5 more)

### Community 83 - "OtpOptions"
Cohesion: 0.12
Nodes (15): OtpOptions, DummyCode, EmailQuotaWindowMinutes, ExpirationInMinutes, Length, MaxSendsPerEmailPerWindow, MaxSendsPerPhonePerWindow, PhoneQuotaWindowMinutes (+7 more)

### Community 84 - "EntityTypeBuilder"
Cohesion: 0.08
Nodes (23): Projections and reconciliation, ProjectionEntity, SourceVersion, AuditableEntityConfiguration, AuditableEntityConfiguration, JobRowConfiguration, DomainEventConverter, IntegrationEventConverter (+15 more)

### Community 85 - "DomainEvent"
Cohesion: 0.05
Nodes (42): Inventory.Endpoints.StockReservations.v1.ReserveSeries, Inventory.Domain.StockReservations.DomainEvents.v1, Inventory.Application.StockReservations.DomainEventHandlers.v1, Inventory, InventoryOptions, BusinessTimeZoneId, MaxOccurrencesPerSeries, ProductSummaryReconciliationCron (+34 more)

### Community 86 - "InventoryDbContext"
Cohesion: 0.09
Nodes (17): IdBound, Value, JobClaimExtensions, ProjectionUpsertExtensions, BaseDbContext, AuditLog, IOutboxDbContext, OutboxMessages (+9 more)

### Community 87 - "PushOptions"
Cohesion: 0.21
Nodes (12): PushOptions, Provider, SendTimeoutSeconds, ServiceAccount, Templates, PushOptionsValidator, PushProvider, Dummy (+4 more)

### Community 89 - "KeycloakPermissionAuthorizationHandler.cs"
Cohesion: 0.13
Nodes (4): Common.Infrastructure.Auth.Services, IAM.Infrastructure.Auth, Notifications.Infrastructure.Hubs, Notifications.Application.Hubs

### Community 91 - "ProductExportJobRunner"
Cohesion: 0.15
Nodes (7): ProductExportRequestedIntegrationEvent, ProductExportJobId, CsvWriter, ExportPageRequest, ExportRow, ProductExportJobRunner, RunProductExportJobHandler

### Community 92 - ".DownloadExportAsync"
Cohesion: 0.14
Nodes (9): JobStatus, Failed, Queued, Running, Succeeded, Endpoint, FileDownload, FileReference (+1 more)

### Community 93 - ".MapEndpoint"
Cohesion: 0.25
Nodes (4): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, Response, ClientKey, Setup

### Community 95 - "Response"
Cohesion: 0.29
Nodes (6): Endpoint, Response, Description, Name, Price, Quantity

### Community 96 - "ILogger"
Cohesion: 0.05
Nodes (15): EventDispatcher, AuditLogRetentionJobRegistrar, AuditLogRetentionService, Setup, MigrationGuard, OutboxSaveHelper, GlobalExceptionHandlingMiddleware, LoginCompletion (+7 more)

### Community 97 - "ProductImportJobRunner"
Cohesion: 0.21
Nodes (6): ProductImportRequestedIntegrationEvent, ProductImportJobId, ProductImportJobRunner, Slice, RunProductImportJobHandler, WorkbookReader

### Community 98 - "Response"
Cohesion: 0.11
Nodes (16): Endpoint, Response, FailureKey, FileName, FinishedOn, JobId, ProcessedRows, ProductsCreated (+8 more)

### Community 99 - "ServiceAccountTokenCache"
Cohesion: 0.07
Nodes (22): IKeycloakPermissionClient, IServiceAccountTokenProvider, KeycloakPaths, KeycloakPermissionClient, DecisionRepresentation, Result, PermissionRepresentation, ResourceName (+14 more)

### Community 100 - "IInboxStore"
Cohesion: 0.11
Nodes (9): IInboxCleanupTarget, ModuleName, IInboxStore, IInboxStore, StatelessInboxStore, Instance, Setup, InboxStore (+1 more)

### Community 101 - "ResiliencyOptions"
Cohesion: 0.09
Nodes (23): KeyedResilienceProfile, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, RateLimitFailOpen, RateLimitPermits (+15 more)

### Community 102 - ".SendAsync"
Cohesion: 0.17
Nodes (9): ISmsGateway, SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, SmsMessage, DummySmsGateway, Setup (+1 more)

### Community 103 - "Stores/v1/Update/Request.cs"
Cohesion: 0.22
Nodes (10): Products.Endpoints.Stores.v1.Update, Request, Body, Id, RequestBody, Address, Description, Name (+2 more)

### Community 104 - "ResultToResponseTransformer.cs"
Cohesion: 0.11
Nodes (10): Webhooks, ProblemResponse, ResultToAcceptedResponseTransformer, ResultToCreatedResponseTransformer, ResultToResponseTransformer, ResultToResponseTransformer, RouteHandlerBuilderExtensions, StringLocalizerExtensions (+2 more)

### Community 105 - "Response"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegisterByEmail, Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 106 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.07
Nodes (22): Notifications.Application.Otp, Inventory.Infrastructure.InterModuleRequestHandlers, Common.Application.Caching, Common.Application.FeatureManagement, IAM.Endpoints.Otp, Notifications.Application.Persistence, Notifications.Infrastructure.Telemetry, Common.InterModuleRequests.IAM (+14 more)

### Community 107 - ".AddCommonObjectStorage"
Cohesion: 0.15
Nodes (3): ChunkEncodingFreeS3Client, DisableChunkEncodingHandler, Setup

### Community 108 - "INotificationsDbContext"
Cohesion: 0.14
Nodes (12): INotificationsDbContext, AlertRecipients, DeviceRegistrations, AlertRecipient, Email, LastSeenOn, UserId, AlertRecipientConfiguration (+4 more)

### Community 109 - "Response"
Cohesion: 0.33
Nodes (4): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, Endpoint, Response, IsRegistered

### Community 111 - "ProjectionUpsertOutcome"
Cohesion: 0.40
Nodes (4): ProjectionUpsertOutcome, Inserted, Stale, Updated

### Community 112 - "IReadOnlyCollection"
Cohesion: 0.20
Nodes (3): IPublicObjectStore, S3PublicObjectStore, Endpoint

### Community 113 - "Response"
Cohesion: 0.15
Nodes (11): Inventory.Endpoints.StockLevels.v1.Movements, Endpoint, Response, ActingUserId, CreatedOn, Id, Quantity, QuantityOnHandAfter (+3 more)

### Community 114 - "Response"
Cohesion: 0.20
Nodes (10): Response, BirthDate, CreatedOn, Email, Enabled, FirstName, Id, LastName (+2 more)

### Community 115 - "RequestBody"
Cohesion: 0.18
Nodes (10): RequestBody, FirstDeadline, IntervalDays, OccurrenceCount, ProductId, QuantityPerOccurrence, Warehouses, WarehouseAllocation (+2 more)

### Community 116 - "ProcessedMessage"
Cohesion: 0.16
Nodes (8): Transactional outbox and inbox, ProcessedMessage, ConsumerName, MessageId, ProcessedOn, ProcessedMessageConfiguration, ICoalescableIntegrationEvent, CoalesceKey

### Community 117 - "Response"
Cohesion: 0.11
Nodes (15): Inventory.Endpoints.StockReservations.v1.Get, ReservationStatus, Active, Committed, Expired, Released, RequiresReconciliation, Endpoint (+7 more)

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
Cohesion: 0.08
Nodes (6): Common.Application.BackgroundJobs, BackgroundJobs.Telemetry, Products.Endpoints.Products.v1.Import, BackgroundJobs, LoginMethods, SessionRevokedReasons

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
Cohesion: 0.05
Nodes (51): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Products.v1.ListImports, Products.Endpoints.Stores.v1.My.AuditLog, Products.Endpoints.Products.v1.AuditLog, PaginationRequest, After, IncludeTotal, PageNumber (+43 more)

### Community 129 - ".SendAsync"
Cohesion: 0.26
Nodes (5): EmailMessage, IEmailGateway, DummyEmailGateway, Setup, ThrottledEmailGateway

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.25
Nodes (10): Observability (OpenTelemetry), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), external, name, networks, local_shared_network, Postgres Service (mm.postgres) (+2 more)

### Community 131 - "IServiceCollection"
Cohesion: 0.05
Nodes (9): Setup, Setup, Setup, Setup, Setup, ModuleRegistry, Setup, Setup (+1 more)

### Community 132 - "ObjectStorageOptions"
Cohesion: 0.08
Nodes (26): ObjectStorageOptions, AccessKey, AdditionalTransientStatusCodes, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds (+18 more)

### Community 134 - "Response"
Cohesion: 0.25
Nodes (7): Products.Endpoints.Products.v1.Get, Response, AvailableQuantity, Description, Name, Price, Quantity

### Community 136 - "AddImage/Request.cs"
Cohesion: 0.20
Nodes (9): Request, File, Id, RequestValidator, Request, ClientRequestId, File, ProductTemplateId (+1 more)

### Community 137 - ".WriteTooManyRequestsToResponse"
Cohesion: 0.13
Nodes (5): ProblemDetailsServiceExtensions, RateLimitingMiddleware, CommonValidations, CreateStoreRateLimitingPolicy, OnRejected

### Community 138 - "services"
Cohesion: 0.25
Nodes (8): container_name, environment, image, networks, ports, restart, services, mm.aspire-dashboard

### Community 139 - "mm.postgres"
Cohesion: 0.25
Nodes (8): container_name, environment, image, networks, ports, restart, volumes, mm.postgres

### Community 140 - "StockLevel"
Cohesion: 0.07
Nodes (25): V1StockLevelCreatedDomainEvent, MovementTypeSnapshot, Adjusted, Received, ReservationCommitted, V1StockQuantityChangedDomainEvent, V1StockQuantityChangedDomainEventExtensions, StockLevel (+17 more)

### Community 141 - "JsonSerializerOptions"
Cohesion: 0.12
Nodes (7): StrictDateTimeOffsetJsonConverter, StronglyTypedIdListReadOnlyJsonConverter, StronglyTypedIdReadOnlyJsonConverter, StronglyTypedIdReadOnlyJsonConverter, StronglyTypedIdWriteOnlyJsonConverter, UnknownDomainEvent, PolymorphicEventConverter

### Community 142 - "mm.rabbitmq"
Cohesion: 0.25
Nodes (8): container_name, environment, image, networks, ports, restart, volumes, mm.rabbitmq

### Community 144 - "AI-assisted development workflow"
Cohesion: 0.25
Nodes (8): Adapting it, AI-assisted development workflow, `CLAUDE.md`, Guards, The commands, The four layers, The issue-to-pull-request automation, The knowledge graph

### Community 145 - "FirebasePushGateway.cs"
Cohesion: 0.22
Nodes (3): Notifications.Application.Push, Notifications.Infrastructure.Push, Notifications.Infrastructure.Push.Firebase

### Community 146 - "Response"
Cohesion: 0.22
Nodes (7): Products.Endpoints.Products.v1.My.Get, Endpoint, Response, Description, Name, Price, Quantity

### Community 147 - "StoreId"
Cohesion: 0.04
Nodes (36): Products.Infrastructure.Persistence.Seeding, Products.Endpoints.ProductTemplates.v1.Create, Products.Application.Products.DomainEventHandlers.v1, Products.Domain.ProductTemplates, ProductChangedIntegrationEventPublishingHandler, ProductCreatedIntegrationEventPublishingHandler, V1ProductCreatedDomainEventHandlers, V1ProductCreatedDomainEvent (+28 more)

### Community 148 - ".ReleaseStockReservationAsync"
Cohesion: 0.17
Nodes (7): IWarehouseGateway, Endpoint, DummyWarehouseGateway, HttpWarehouseGateway, ReleaseRequestBody, ReleaseResponseBody, Setup

### Community 150 - "ConcurrencyConfiguratorExtensions"
Cohesion: 0.16
Nodes (5): ConcurrencyConfiguratorExtensions, InterModuleRequestHandlerDefinition, RequestType, RunProductExportJobHandlerDefinition, RunProductImportJobHandlerDefinition

### Community 151 - "ChangeLowStockThreshold/Request.cs"
Cohesion: 0.28
Nodes (8): Inventory.Endpoints.StockLevels.v1.ChangeLowStockThreshold, Request, Body, Id, RequestBody, Threshold, RequestBodyValidator, RequestValidator

### Community 152 - "system_globalization"
Cohesion: 0.07
Nodes (26): Products.Endpoints.Stores.v1.My.Create, Constants, Request, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name (+18 more)

### Community 153 - "WebhookCallback/Request.cs"
Cohesion: 0.40
Nodes (5): Inventory.Endpoints.StockReservations.v1.WebhookCallback, Request, ProviderReference, ReservationId, RequestValidator

### Community 154 - "IdentityScheme"
Cohesion: 0.28
Nodes (7): IdentityScheme, Email, PhoneNumber, IdentitySchemeOptions, Scheme, IdentitySchemeOptionsValidator, Setup

### Community 156 - "InterModuleRequestOptions"
Cohesion: 0.25
Nodes (7): InterModuleRequestOptions, DependencyUnavailableRetryAfterSeconds, HandlerConcurrentMessageLimit, HandlerPrefetchCount, Timeouts, TimeoutSeconds, InterModuleRequestOptionsValidator

### Community 157 - "AlertRecipientsOptions"
Cohesion: 0.29
Nodes (7): AlertRecipientsOptions, EmailHtmlBodyTemplate, EmailSubjectTemplate, EmailTextBodyTemplate, ReconcileCron, RoleName, AlertRecipientsOptionsValidator

### Community 158 - ".SearchProductTemplatesAsync"
Cohesion: 0.29
Nodes (5): Endpoint, Response, Brand, Color, Model

### Community 159 - "Stores/v1/Create/Request.cs"
Cohesion: 0.33
Nodes (6): Request, Address, Description, Name, OwnerId, RequestValidator

### Community 160 - "Response"
Cohesion: 0.29
Nodes (7): Response, Id, LowStockThreshold, ProductId, ProductName, QuantityOnHand, StoreId

### Community 162 - "Response"
Cohesion: 0.22
Nodes (7): Products.Endpoints.Products.v1.Search, Endpoint, Response, Description, Name, Price, Quantity

### Community 164 - "SearchProductsByTermRequestHandler"
Cohesion: 0.27
Nodes (6): SearchProductsByTermRequest, SearchProductsByTermResponse, Endpoint, PageResponse, ProductsCacheKeys, SearchProductsByTermRequestHandler

### Community 165 - "ApplicationUserId"
Cohesion: 0.08
Nodes (19): IBackgroundUserContext, UserId, ICurrentUser, Id, IdAsString, Roles, SessionId, BackgroundUserContext (+11 more)

### Community 166 - "NotificationPayload"
Cohesion: 0.24
Nodes (4): INotificationDispatcher, INotificationsClient, NotificationPayload, SignalRNotificationDispatcher

### Community 167 - "FirebaseServiceAccountOptions"
Cohesion: 0.25
Nodes (8): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri, Type

### Community 169 - ".GetProductAsync"
Cohesion: 0.54
Nodes (4): GetStockLevelRequest, GetStockLevelResponse, GetStockLevelRequestHandler, Endpoint

### Community 172 - "DomainEvent"
Cohesion: 0.04
Nodes (54): Products.Domain.Stores.DomainEvents.v1, Products, AggregateRoot, Events, Id, Version, IAggregateRoot, Events (+46 more)

### Community 173 - "IProductsDbContext"
Cohesion: 0.09
Nodes (23): ProductExportJob, FileName, ObjectKey, StoreId, IProductsDbContext, ExportJobs, ImportJobs, Products (+15 more)

### Community 174 - "CachingOptions.cs"
Cohesion: 0.32
Nodes (7): CachingOptionsValidator, Redis, AppName, Host, Password, Port, RedisValidator

### Community 175 - "IInterModuleRequest"
Cohesion: 0.20
Nodes (8): GetUsersInRolePageRequest, GetUsersInRolePageResponse, RoleUserSummary, GetUsersInRolePageRequestHandler, AlertRecipientsReconcileJobRegistrar, AlertRecipientsCursor, AlertRecipientsReconciliationJob, Setup

### Community 176 - ".HandleWarehouseWebhookAsync"
Cohesion: 0.12
Nodes (5): PaginationCursor, Payload, TaggedValue, DbContextExtensions, Endpoint

### Community 177 - "CorsOptions"
Cohesion: 0.29
Nodes (7): CorsOptions, AllowCredentials, AllowedHeaders, AllowedMethods, AllowedOrigins, MaxAgeInSeconds, CorsOptionsValidator

### Community 178 - "FeatureFlags"
Cohesion: 0.29
Nodes (6): Feature flags, Checkout, FeatureFlags, IAM, Notifications, Products

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

### Community 187 - "fluentvalidation"
Cohesion: 0.05
Nodes (31): DatabaseOptions, ConnectionString, DatabaseOptionsValidator, ModulesOptions, EnabledModules, ModulesOptionsValidator, ProjectionReconciliationOptions, MaxPages (+23 more)

### Community 188 - "BoundedCaptureStream"
Cohesion: 0.14
Nodes (6): BoundedCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 190 - "IOtpService"
Cohesion: 0.14
Nodes (10): OtpCacheEntry, IssueVerificationTokenRequest, IssueVerificationTokenResponse, IOtpService, OtpVerificationOutcome, InvalidOtp, Success, TooManyAttempts (+2 more)

### Community 192 - "Result"
Cohesion: 0.11
Nodes (6): Result, Error, IsFailure, Success, SyncExtensions, ResultTelemetryExtensions

### Community 193 - ".EnqueueAsync"
Cohesion: 0.19
Nodes (7): BulkImportOptions, MaxFileBytes, MaxRows, SaveChunkSize, BulkImportOptionsValidator, Endpoint, ProductImportJobEnqueuer

### Community 194 - "Modules"
Cohesion: 0.33
Nodes (6): Adding a module or a feature, How modules fit together, IAM, Modules, Notifications, Outbox

### Community 195 - "Response"
Cohesion: 0.33
Nodes (4): Inventory.Endpoints.StockReservations.v1.Reserve, Endpoint, Response, Id

### Community 197 - "Response"
Cohesion: 0.25
Nodes (5): Products.Endpoints.Stores.v1.Create, Setup, Endpoint, Response, Id

### Community 200 - "ActivitySource"
Cohesion: 0.11
Nodes (8): RequireFeatureFilter, RouteHandlerBuilderExtensions, BackgroundJobsTelemetry, IamTelemetry, InventoryTelemetry, NotificationsTelemetry, OutboxTelemetry, ProductsTelemetry

### Community 201 - "KeycloakScopes"
Cohesion: 0.18
Nodes (10): Devices, Hangfire, KeycloakScopes, Products, ProductTemplates, Sessions, StockLevels, StockReservations (+2 more)

### Community 202 - "DevicesOptions"
Cohesion: 0.40
Nodes (5): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator

### Community 203 - "v1/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Probe.v1, Request, Count, RequestValidator

### Community 205 - "My/RemoveProduct/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Stores.v1.My.RemoveProduct, Request, Id, RequestValidator

### Community 211 - "GetExport/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Products.v1.GetExport, Request, Id, RequestValidator

### Community 212 - "Split-Deployment PoC"
Cohesion: 0.33
Nodes (6): Concurrent safety, Files added by this PoC, How to run, Split-Deployment PoC, What this proves, What to check

### Community 214 - "RowOutcome"
Cohesion: 0.40
Nodes (5): RowOutcome, Created, Rejected, Unchanged, Updated

### Community 215 - "Response"
Cohesion: 0.17
Nodes (10): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, Endpoint, Response, ClientId, DeviceName, Id, IpAddress, IsCurrent (+2 more)

### Community 216 - "SendResponseBody"
Cohesion: 0.50
Nodes (4): SendResponseBody, Code, Description, JobId

### Community 217 - "Platform building blocks"
Cohesion: 0.18
Nodes (9): Authorization, Caching, Inter-module requests, Job rows, Observability, Options and configuration, Platform building blocks, Results and errors (+1 more)

### Community 221 - "Keycloak realm as code"
Cohesion: 0.25
Nodes (8): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Secrets, Seed users (dev / test only), Signing in through the API

### Community 236 - "NetGsmSmsGateway"
Cohesion: 0.23
Nodes (4): NetGsmSmsGateway, SendMessageBody, Msg, No

### Community 242 - "Response"
Cohesion: 0.25
Nodes (6): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 243 - "Commit/Request.cs"
Cohesion: 0.28
Nodes (8): Inventory.Endpoints.StockReservations.v1.Commit, Request, Body, Id, RequestBody, ProviderReference, RequestBodyValidator, RequestValidator

### Community 251 - ".CreateMyStoreAsync"
Cohesion: 0.16
Nodes (5): CollectionExtensions, PersistenceQueryableExtensions, Endpoint, Response, Id

### Community 264 - "ReCaptchaResponse"
Cohesion: 0.18
Nodes (3): Object storage, IPrivateObjectStore, S3PrivateObjectStore

### Community 269 - "RouteGroupBuilder"
Cohesion: 0.05
Nodes (14): Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint (+6 more)

### Community 276 - "InterModuleRequestHandler"
Cohesion: 0.11
Nodes (20): IInterModuleRequest, InterModuleRequestHandler, DeprovisionUserRequest, DeprovisionUserResponse, GetSeedUserIdsRequest, GetSeedUserIdsResponse, GetUsersRequest, GetUsersResponse (+12 more)

### Community 280 - "SendForEmail/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForEmail, Request, CaptchaToken, Email, RequestValidator

### Community 296 - "AuditLogOptions"
Cohesion: 0.33
Nodes (6): AuditLogOptions, PerSchemaRetentionDays, PurgeBatchSize, RetentionCron, RetentionDays, AuditLogOptionsValidator

### Community 319 - "Products/v1/Update/Request.cs"
Cohesion: 0.20
Nodes (11): Products.Endpoints.Products.v1.Update, Request, Body, Id, RequestBody, Description, Name, Price (+3 more)

### Community 320 - "Common.Application.Options"
Cohesion: 0.05
Nodes (20): Notifications.Application.Sms, Host, Outbox, Common.Infrastructure.Persistence.Outbox, Common.Infrastructure.RateLimiting, Notifications.Infrastructure.Sms, Products.Infrastructure.RateLimiting, Common.Infrastructure.Extensions (+12 more)

### Community 323 - "Common.Application.Validation"
Cohesion: 0.04
Nodes (55): Products.Endpoints.Products.v1.GetImport, Products.Endpoints.Stores.v1.Deactivate, Products.Endpoints.ProductTemplates.v1.Deactivate, Products.Endpoints.Products.v1.DownloadExport, Products.Endpoints.Products.v1.RemoveImage, Inventory.Endpoints.StockReservations.v1.Release, Common.Application.ModelBinders, Common.Application.Validation (+47 more)

### Community 324 - "CustomValidator"
Cohesion: 0.06
Nodes (42): Inventory.Endpoints.StockLevels.v1.Receive, Inventory.Endpoints.StockLevels.v1.Adjust, Products.Endpoints.Products.v1.My.Update, CustomRateLimitingOptionsValidator, FixedWindowValidator, CustomValidator, Request, Body (+34 more)

## Knowledge Gaps
- **1058 isolated node(s):** `UserId`, `Id`, `IdAsString`, `Roles`, `SessionId` (+1053 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1447 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **123 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `IServiceCollection`, `ObjectStorageOptions`, `TimeProvider`, `OpenApiOptions`, `Common.Domain.ResultMonad`, `HealthCheckOptions`, `EmailOptions`, `FirebasePushGateway.cs`, `Common.Application.Options`, `microsoft_extensions_options`, `FullTextSearchOptions`, `IdentityScheme`, `InterModuleRequestOptions`, `AlertRecipientsOptions`, `IOptions`, `CreateByEmail/Request.cs`, `IAM.Application.Captcha.Services`, `Common.Infrastructure.Persistence.EntityConfigurations`, `ObservabilityOptions`, `AuditLogOptions`, `S3ObjectStoreCore.cs`, `CachingOptions.cs`, `CorsOptions`, `BackgroundJobsOptions`, `CaptchaOptions`, `RabbitMqOptions`, `SmsOptions`, `OutboxOptions`, `fluentvalidation`, `Request`, `RequestLoggingOptions`, `.EnqueueAsync`, `CustomValidator`, `Request`, `system_net`, `DevicesOptions`, `KeycloakOptions`, `OtpOptions`, `DomainEvent`, `PushOptions`, `KeycloakPermissionAuthorizationHandler.cs`, `ResiliencyOptions`, `Common.InterModuleRequests.Contracts`, `system_diagnostics`, `ProductsOptions`?**
  _High betweenness centrality (0.084) - this node is a cross-community bridge._
- **What connects `UserId`, `Id`, `IdAsString` to the rest of the system?**
  _1058 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `PaginationResponse` be split into smaller, more focused modules?**
  _Cohesion score 0.14210526315789473 - nodes in this community are weakly interconnected._
- **Why does `Result` connect `Result` to `PaginationResponse`, `.SendAsync`, `AuditableEntityResponse`, `ApplicationUserId`, `Task`, `StockLevel`, `RouteGroupBuilder`, `ProductId`, `Response`, `StoreId`, `InterModuleRequestHandler`, `ReCaptchaService`, `.ReleaseStockReservationAsync`, `.RegisterAsync`, `Error`, `.SearchProductTemplatesAsync`, `Response`, `SearchProductsByTermRequestHandler`, `.GetProductAsync`, `SmsOtpDispatchOutcome`, `DomainEvent`, `IProductsDbContext`, `ProductImportJob`, `.HandleWarehouseWebhookAsync`, `BrevoEmailGateway`, `FirebasePushGateway`, `Task`, `.EnqueueAsync`, `Response`, `Response`, `DefaultIdType`, `VerifyPhoneOtpResponse`, `Response`, `Response`, `OtpOptions`, `DomainEvent`, `.DownloadExportAsync`, `.MapEndpoint`, `Response`, `ProductImportJobRunner`, `Response`, `.SendAsync`, `Common.InterModuleRequests.Contracts`, `NetGsmSmsGateway`, `IReadOnlyCollection`, `Response`, `Response`, `.CreateMyStoreAsync`, `Response`?**
  _High betweenness centrality (0.045) - this node is a cross-community bridge._
- **Should `IModule` be split into smaller, more focused modules?**
  _Cohesion score 0.05357142857142857 - nodes in this community are weakly interconnected._
- **Why does `ApplicationUserId` connect `ApplicationUserId` to `PaginationResponse`, `AuditableEntityResponse`, `AuditableEntity`, `ProductId`, `Response`, `KeycloakPermissionAuthorizationHandler`, `Common.Domain.StronglyTypedIds`, `StoreId`, `InterModuleRequestHandler`, `DeviceRegistration`, `DeviceRegistryReconciliationService`, `.RegisterAsync`, `system_globalization`, `Stores/v1/Create/Request.cs`, `SendSecurityAlertRequestHandler`, `For`, `ApplicationUserId`, `NotificationPayload`, `DomainEvent`, `IProductsDbContext`, `IInterModuleRequest`, `DateTimeOffset`, `.EnqueueAsync`, `Common.Application.Validation`, `DefaultIdType`, `IntegrationEventOutbox`, `Response`, `ProvisionUserRequestHandler`, `Response`, `Response`, `EntityTypeBuilder`, `NotificationsHub`, `ILogger`, `INotificationsDbContext`, `Response`, `Response`, `Response`?**
  _High betweenness centrality (0.040) - this node is a cross-community bridge._
- **Should `HttpContext` be split into smaller, more focused modules?**
  _Cohesion score 0.08710801393728224 - nodes in this community are weakly interconnected._