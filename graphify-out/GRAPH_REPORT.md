# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-10-27)

## Corpus Check
- 592 files · ~106,897 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 4067 nodes · 10747 edges · 296 communities (176 shown, 120 thin omitted)
- Extraction: 96% EXTRACTED · 4% INFERRED · 0% AMBIGUOUS · INFERRED: 420 edges (avg confidence: 0.86)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `f4140b60`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- PaginationResponse
- NotificationPayload
- HttpContext
- Response
- ApplicationUserId
- ProductTemplateId
- ApplySearchLanguageInterceptor
- IDbContext
- Setup.Observability.cs
- Result
- Common.Domain.ResultMonad
- IStronglyTypedId
- IApplicationBuilder
- Response
- KeycloakPermissionAuthorizationHandler
- EmailOptions
- DomainEvent
- Common.Application.Options
- Common.Domain.StronglyTypedIds
- FullTextSearchOptions
- BoundedRequestCaptureStream
- DeviceRegistration
- IReadOnlyList
- .RegisterAsync
- Stores/v1/My/Update/Request.cs
- Response
- RequestResponseBodyLoggingMiddleware
- SendForLogin/Request.cs
- Func
- ProjectionUpsertOutcome
- FirebasePushGateway
- AuditLogRetentionService
- Response
- fluentvalidation
- OutboxMessage
- CustomRateLimitingOptions
- AdminRepresentations.cs
- Common.Domain.StronglyTypedIds
- ObservabilityOptions
- DefaultResponsesOperationFilter
- DefaultIdType
- S3ObjectStoreCore.cs
- SmsOtpDispatchOutcome
- ServiceAccountTokenCache
- ConfigureSwaggerOptions.cs
- PaginationQueryableExtensions
- Outbox Misuse Check
- OutboxProcessor
- Add Integration Event Command
- host
- BackgroundJobsOptions
- CaptchaOptions
- KeyedResiliencePipelines
- RedisFixedWindowRateLimiter
- SendContact
- DatabaseSeederOrchestrator
- Cross-Module Reference Violation
- OutboxOptions
- Request
- Bogus Test Data
- SendSecurityAlertRequestHandler
- StronglyTypedIdBinder
- RequestLoggingOptions
- ObjectListPage
- Task
- .EnsureNoMigrationsPending
- .AddNotificationsSignalR
- ValueObject
- Request
- ValueTask
- Getting started
- ValidationContextExtensions
- VerifyPhoneOtpResponse
- AsNoTracking Coverage Check
- V1StoreCreatedDomainEvent
- Response
- OpenApiOptions
- KeycloakOptions
- DeviceRegistryReconciliationService
- SendEmailOtpRequestHandler
- Response
- Split-Deployment PoC
- IOptions
- ProductTemplate
- DateTimeOffset
- .SaveWithOutboxAsync
- PushOptions
- Configuration-Driven Module Registration
- KeycloakPermission
- Setup
- SendRequestBody
- .SaveWithOutboxAsync
- .GetClientKey
- .SendAsync
- Response
- ILogger
- How it works
- AuditableEntityResponse
- InterModuleRequestOptions
- InboxStore
- ResiliencyOptions
- .GetMeAsync
- FirebasePushGateway.cs
- ResultToResponseTransformer.cs
- Response
- Common.InterModuleRequests.Contracts
- .PaginateAsync
- ChunkEncodingFreeS3Client
- Response
- KeycloakPermissionPolicyProvider
- VerifyEmail/Request.cs
- IModule
- ConfigureSwaggerOptions
- Response
- Type
- IResxLocalizer
- Response
- DatabaseOptions.cs
- depends_on
- healthcheck
- mm.keycloak
- mm.redis
- Response
- Setup.HealthChecks.cs
- InterModuleRequestHandlerDefinition
- Pagination and search
- Response
- PaginationRequest
- Response
- docker-compose.yml (Base Stack)
- IServiceCollection
- ObjectStorageOptions
- .GetProductAsync
- ProjectionReconciliationJob
- Response
- .WriteTooManyRequestsToResponse
- services
- mm.postgres
- StockLevel
- Type
- mm.rabbitmq
- Type
- AI-assisted development workflow
- FixedWindow
- RequestBodyLimitMetadata
- .HandleAsync
- .ReleaseStockReservationAsync
- ReCaptchaResponse
- Common.InterModuleRequests/Setup.cs
- OutboxOptions.cs
- Request
- WebhookCallback/Request.cs
- v1/RemoveProduct/Request.cs
- InterModuleRequestHandler
- Activate/Request.cs
- Request
- HttpContextTargetingContextAccessor
- RequestBodyLimitFilter.cs
- PolymorphicEventConverter
- For
- .AddCommonCaching
- ApplicationUserId
- StoreStatus
- .VerifySha256
- JwtClaimNames.cs
- KeycloakRoles.cs
- Notifications.Application/IAssemblyReference.cs
- StringExtensions
- Product
- Task
- KeycloakTokenClient.cs
- IInterModuleRequest
- StockReservations/Constants.cs
- CorsOptions
- Stores/Constants.cs
- CachingOptions
- SmsOptions
- RabbitMqOptions
- IAM.Domain
- Response
- Response
- Response
- ReverseProxyOptions
- BoundedCaptureStream
- .Apply
- KeycloakPermissionClient
- Response
- system_diagnostics
- KeycloakScopes
- DevicesOptions
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- Key decisions
- Response
- Platform building blocks
- Keycloak realm as code
- BackgroundJobsModule.cs
- NetGsmSmsGateway
- .UseInfrastructure
- ResxLocalizationOptions
- Response
- RequestBody
- Response
- Products.Domain/IAssemblyReference.cs
- IAM.Application/IAssemblyReference.cs
- Products.Infrastructure/IAssemblyReference.cs
- Products.Application/IAssemblyReference.cs
- Common.Infrastructure/IAssemblyReference.cs
- Notifications.Domain/IAssemblyReference.cs
- IAM.Infrastructure/IAssemblyReference.cs
- Response
- S3ObjectStoreCore
- RouteGroupBuilder
- microsoft_aspnetcore_mvc
- SendForEmail/Request.cs
- Request
- Request
- AuditLogOptions
- ProjectionReconciliationOptions.cs
- CustomValidator
- Products.Endpoints
- Products/v1/Update/Request.cs
- IAMModule.cs
- v1/Request.cs
- Products/v1/My/Update/Request.cs
- IAM.Endpoints
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
1. `Result` - 189 edges
2. `Common.Application.Options` - 139 edges
3. `Common.Domain.ResultMonad` - 105 edges
4. `CustomValidator` - 83 edges
5. `ApplicationUserId` - 77 edges
6. `Common.Application.Validation` - 74 edges
7. `Common.Domain.StronglyTypedIds` - 69 edges
8. `Common.Application.Auth` - 68 edges
9. `Common.Application.Extensions` - 62 edges
10. `Common.InterModuleRequests.Contracts` - 55 edges

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

## Communities (296 total, 120 thin omitted)

### Community 0 - "PaginationResponse"
Cohesion: 0.11
Nodes (13): AuditLogDto, PaginationResponse, HasNext, HasPrevious, HasTotal, NextPageNumber, PreviousPageNumber, TotalPages (+5 more)

### Community 1 - "NotificationPayload"
Cohesion: 0.23
Nodes (5): Notifications.Application.Hubs, INotificationDispatcher, INotificationsClient, NotificationPayload, SignalRNotificationDispatcher

### Community 2 - "HttpContext"
Cohesion: 0.26
Nodes (3): Rate limiting and resilience, HttpContextExtensions, RateLimitPartitions

### Community 3 - "Response"
Cohesion: 0.25
Nodes (6): Products.Endpoints.ProductTemplates.v1.Search, Endpoint, Response, Brand, Color, Model

### Community 5 - "ApplicationUserId"
Cohesion: 0.15
Nodes (13): KeycloakUser, KeycloakAdminClient, UserRepresentation, Attributes, CreatedTimestamp, Credentials, Email, EmailVerified (+5 more)

### Community 6 - "ProductTemplateId"
Cohesion: 0.10
Nodes (15): ProductTemplate, Brand, Color, IsActive, Model, Products, ProductTemplateId, RequestBody (+7 more)

### Community 7 - "ApplySearchLanguageInterceptor"
Cohesion: 0.31
Nodes (3): ApplyAuditingInterceptor, ApplySearchLanguageInterceptor, Setup

### Community 8 - "IDbContext"
Cohesion: 0.11
Nodes (8): JobHousekeepingJob, IDbContext, AuditLog, ChangeTracker, Database, Setup, StockReservationExpirySweepJobRegistrar, StockReservationExpirySweepService

### Community 10 - "Result"
Cohesion: 0.04
Nodes (29): Error, Key, ParameterName, StatusCode, SubErrors, Value, IResult, Error (+21 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.09
Nodes (30): IAM.Endpoints.Captcha.VersionNeutral, Products.Infrastructure.InterModuleRequestHandlers, Common.Application.Search, Common.Application.AuditLog, Common.Application.FeatureManagement, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Products.Endpoints.ProductTemplates (+22 more)

### Community 12 - "IStronglyTypedId"
Cohesion: 0.11
Nodes (17): IAggregateRoot, Events, Id, Version, AuditableEntity, AuditableEntity, Id, CreatedBy (+9 more)

### Community 13 - "IApplicationBuilder"
Cohesion: 0.13
Nodes (4): SearchLanguageResolver, UniversalConfig, Setup, HangfireCustomAuthorizationFilter

### Community 14 - "Response"
Cohesion: 0.13
Nodes (13): IAM.Endpoints.Users.VersionNeutral.Me.Get, Endpoint, Response, BirthDate, CreatedOn, Email, FirstName, Id (+5 more)

### Community 15 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.29
Nodes (3): KeycloakPermissionAuthorizationHandler, KeycloakPermissionRequirement, Permission

### Community 16 - "EmailOptions"
Cohesion: 0.10
Nodes (23): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+15 more)

### Community 17 - "DomainEvent"
Cohesion: 0.09
Nodes (26): Products, AggregateRoot, Events, Id, Version, DomainEvent, CreatedOn, Id (+18 more)

### Community 18 - "Common.Application.Options"
Cohesion: 0.06
Nodes (18): Notifications.Application.Sms, Notifications.Infrastructure.Email, Common.Infrastructure.RateLimiting, Common.Infrastructure.Localization, Notifications.Infrastructure.Sms, Notifications.Infrastructure.Email.Brevo, Products.Infrastructure.RateLimiting, Common.Infrastructure.Extensions (+10 more)

### Community 19 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.05
Nodes (29): Common.Infrastructure.Persistence.Inbox, Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Products.Infrastructure.Persistence.Seeding, Common.Application.Persistence.Inbox, Inventory.Application.Persistence, Inventory.Application.IntegrationEventHandlers, Common.IntegrationEvents (+21 more)

### Community 20 - "FullTextSearchOptions"
Cohesion: 0.13
Nodes (18): Add a new language/culture, Add search to a new entity _(Build checklist)_, Change the vector (weights, fields, or config), Configuration, Extending and maintaining, Full-Text Search, Gotchas, Non-goals (+10 more)

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.13
Nodes (6): BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 22 - "DeviceRegistration"
Cohesion: 0.12
Nodes (12): DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive, LastReconciledOn, PushToken, PushTokenUpdatedOn (+4 more)

### Community 23 - "IReadOnlyList"
Cohesion: 0.29
Nodes (7): GetActiveSessionIdsRequest, GetActiveSessionIdsResponse, UserSessionIds, GrantedPermission, KeycloakUserPage, KeycloakUserSession, GetActiveSessionIdsRequestHandler

### Community 24 - ".RegisterAsync"
Cohesion: 0.08
Nodes (18): IInterModuleRequestClient, BindDeviceSessionRequest, BindDeviceSessionResponse, DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse, VerifyEmailOtpRequest, VerifyEmailOtpResponse, ICaptchaService (+10 more)

### Community 25 - "Stores/v1/My/Update/Request.cs"
Cohesion: 0.33
Nodes (6): Products.Endpoints.Stores.v1.My.Update, Request, Address, Description, Name, RequestValidator

### Community 26 - "Response"
Cohesion: 0.29
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, Endpoint, Response, EmailVerificationToken, IsRegistered

### Community 28 - "SendForLogin/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForLogin, Request, CaptchaToken, PhoneNumber, RequestValidator

### Community 29 - "Func"
Cohesion: 0.15
Nodes (16): CheckRegistrationRateLimitingPolicy, OnRejected, EmailRateLimitingPolicy, OnRejected, OtpVerifyRateLimitingPolicy, OnRejected, RegisterRateLimitingPolicy, OnRejected (+8 more)

### Community 30 - "ProjectionUpsertOutcome"
Cohesion: 0.18
Nodes (8): Common.Application.Persistence.Projections, Projections and reconciliation, ProjectionEntity, SourceVersion, ProjectionUpsertOutcome, Inserted, Stale, Updated

### Community 31 - "FirebasePushGateway"
Cohesion: 0.20
Nodes (4): IPushGateway, DummyPushGateway, FirebasePushGateway, Setup

### Community 32 - "AuditLogRetentionService"
Cohesion: 0.24
Nodes (3): AuditLogRetentionJobRegistrar, AuditLogRetentionService, Setup

### Community 33 - "Response"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.Refresh, Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 34 - "fluentvalidation"
Cohesion: 0.11
Nodes (19): IAM.Domain.Users, Common.Domain.Extensions, Notifications.Infrastructure.Devices.UpdateCurrentPushToken, Common.Application.Validation, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, Common.Domain.Devices, IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, IAM.Endpoints.Common.Validations (+11 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.04
Nodes (42): JobRow, FailureKey, FinishedOn, Progress, QueuedOn, RequestedBy, StartedOn, Status (+34 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.06
Nodes (24): CustomRateLimitingOptions, CheckRegistration, CreateStore, Email, ExemptPathPrefixes, Global, OtpVerify, Register (+16 more)

### Community 37 - "AdminRepresentations.cs"
Cohesion: 0.12
Nodes (16): CredentialRepresentation, Temporary, Type, Value, ErrorRepresentation, Error, ErrorMessage, Field (+8 more)

### Community 38 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.05
Nodes (15): Common.Domain.StronglyTypedIds, Common.Domain.Events, Common.Application.Jobs, Notifications.Domain.Devices, Common.Infrastructure.Auth.Services, Inventory.Domain.StockReservations.Errors, Common.Application.JsonConverters, Common.Domain.Entities (+7 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.08
Nodes (17): ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics, EnableTracing, LogSink, MinimumLevel (+9 more)

### Community 41 - "DefaultIdType"
Cohesion: 0.16
Nodes (6): IntegrationEventHandlerBase, MaxEventAge, ProductCreatedIntegrationEvent, StoreCreatedIntegrationEvent, CreateStockLevelOnProductCreatedHandler, LogProductCatalogChangeHandler

### Community 42 - "S3ObjectStoreCore.cs"
Cohesion: 0.23
Nodes (3): Common.Infrastructure.Storage, Common.Application.Storage, Setup

### Community 43 - "SmsOtpDispatchOutcome"
Cohesion: 0.33
Nodes (5): OtpDispatchErrors, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled

### Community 44 - "ServiceAccountTokenCache"
Cohesion: 0.05
Nodes (28): IServiceAccountTokenProvider, KeycloakPaths, DecisionRepresentation, Result, PermissionRepresentation, ResourceName, Scopes, TokenResponseRepresentation (+20 more)

### Community 46 - "PaginationQueryableExtensions"
Cohesion: 0.12
Nodes (8): CursorBound, Value, KeyedRow, Item, Sort, Tie, PaginationQueryableExtensions, ParameterReplacer

### Community 48 - "OutboxProcessor"
Cohesion: 0.14
Nodes (8): Transactional outbox and inbox, ICoalescableIntegrationEvent, CoalesceKey, OutboxProcessor, PublishOutcome, Failed, Published, Skipped

### Community 50 - "host"
Cohesion: 0.18
Nodes (11): context, dockerfile, build, container_name, environment, image, networks, ports (+3 more)

### Community 51 - "BackgroundJobsOptions"
Cohesion: 0.29
Nodes (7): BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount, BackgroundJobsOptionsValidator

### Community 52 - "CaptchaOptions"
Cohesion: 0.16
Nodes (13): CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey, Provider, ScoreThreshold, SecretKey (+5 more)

### Community 53 - "KeyedResiliencePipelines"
Cohesion: 0.14
Nodes (3): HttpClientKeyedExtensions, KeyedResiliencePipelines, Setup

### Community 54 - "RedisFixedWindowRateLimiter"
Cohesion: 0.17
Nodes (6): FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration, RetryAfterHeaderExtensions

### Community 55 - "SendContact"
Cohesion: 0.27
Nodes (6): SendContact, Email, Name, SendErrorBody, Code, Message

### Community 56 - "DatabaseSeederOrchestrator"
Cohesion: 0.16
Nodes (5): Persistence conventions, IDatabaseSeeder, Priority, DatabaseSeederOrchestrator, SeedingCompletionTracker

### Community 58 - "OutboxOptions"
Cohesion: 0.06
Nodes (32): Adding a module or a feature, How modules fit together, IAM, Modules, Notifications, Outbox, OutboxOptions, BaseBackoffSeconds (+24 more)

### Community 59 - "Request"
Cohesion: 0.15
Nodes (13): Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken (+5 more)

### Community 61 - "SendSecurityAlertRequestHandler"
Cohesion: 0.52
Nodes (5): SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, SendSecurityAlertRequestHandler

### Community 63 - "RequestLoggingOptions"
Cohesion: 0.13
Nodes (15): RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes, SensitiveQueryParamPaths (+7 more)

### Community 64 - "ObjectListPage"
Cohesion: 0.19
Nodes (3): ObjectListItem, ObjectListPage, UploadObjectRequest

### Community 65 - "Task"
Cohesion: 0.06
Nodes (6): IBackgroundJobs, IRecurringBackgroundJobs, FeatureFlagResultExtensions, AsyncExtensions, BackgroundJobsService, RecurringBackgroundJobsService

### Community 66 - ".EnsureNoMigrationsPending"
Cohesion: 0.39
Nodes (3): AutoMigrateMarker, IAutoMigrateMarker, MigrationGuard

### Community 69 - "Request"
Cohesion: 0.17
Nodes (12): Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FirstName, LastName (+4 more)

### Community 71 - "Getting started"
Cohesion: 0.12
Nodes (16): Configuration, Errors, Getting started, Prerequisites, Run the API from the command line instead, Run the load test, Run the tests, Run the whole system (+8 more)

### Community 73 - "VerifyPhoneOtpResponse"
Cohesion: 0.11
Nodes (13): OtpCacheEntry, OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, VerifyPhoneOtpResponseExtensions (+5 more)

### Community 75 - "V1StoreCreatedDomainEvent"
Cohesion: 0.16
Nodes (10): Products.Application.Products.DomainEventHandlers.v1, DomainEventHandlerBase, IEventHandler, IEventHandlerWrapper, ProductCreatedIntegrationEventPublishingHandler, V1ProductCreatedDomainEventHandlers, SimulateSomeBusinessHandler, StoreCreatedIntegrationEventPublishingHandler (+2 more)

### Community 76 - "Response"
Cohesion: 0.25
Nodes (7): Endpoint, Response, Address, Description, Name, OwnerId, ProductCount

### Community 77 - "OpenApiOptions"
Cohesion: 0.22
Nodes (9): OpenApiOptions, ContactEmail, ContactName, Description, EnableSwagger, LicenseName, LicenseUrl, Title (+1 more)

### Community 78 - "KeycloakOptions"
Cohesion: 0.14
Nodes (14): KeycloakOptions, AttemptTimeoutSeconds, Authority, BaseUrl, DecisionCacheMaxDurationSeconds, Realm, RequireHttpsMetadata, ResourceClientId (+6 more)

### Community 79 - "DeviceRegistryReconciliationService"
Cohesion: 0.25
Nodes (3): DeviceRegistryReconcileJobRegistrar, DeviceRegistryReconciliationService, Setup

### Community 80 - "SendEmailOtpRequestHandler"
Cohesion: 0.33
Nodes (5): EmailOtpDispatchErrors, EmailOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled

### Community 81 - "Response"
Cohesion: 0.20
Nodes (8): Products.Endpoints.Stores.v1.Search, Endpoint, Response, Address, Description, Name, OwnerId, ProductCount

### Community 82 - "Split-Deployment PoC"
Cohesion: 0.33
Nodes (6): Concurrent safety, Files added by this PoC, How to run, Split-Deployment PoC, What this proves, What to check

### Community 83 - "IOptions"
Cohesion: 0.07
Nodes (31): OtpOptions, DummyCode, EmailQuotaWindowMinutes, ExpirationInMinutes, Length, MaxSendsPerEmailPerWindow, MaxSendsPerPhonePerWindow, PhoneQuotaWindowMinutes (+23 more)

### Community 84 - "ProductTemplate"
Cohesion: 0.11
Nodes (13): ProcessedMessage, ConsumerName, MessageId, ProcessedOn, AuditableEntityConfiguration, AuditableEntityConfiguration, AuditLogEntryConfiguration, JobRowConfiguration (+5 more)

### Community 85 - "DateTimeOffset"
Cohesion: 0.05
Nodes (35): Inventory.Endpoints.StockReservations.v1.ReserveSeries, Inventory.Domain.StockReservations.DomainEvents.v1, Inventory, InventoryOptions, MaxOccurrencesPerSeries, SafetyBufferQuantity, StockReservationExpirySweepBatchSize, StockReservationExpirySweepCron (+27 more)

### Community 86 - ".SaveWithOutboxAsync"
Cohesion: 0.06
Nodes (27): IIntegrationEventOutbox, IdBound, Value, JobClaimExtensions, ProjectionUpsertExtensions, EventDispatcher, IntegrationEventOutbox, HasPending (+19 more)

### Community 87 - "PushOptions"
Cohesion: 0.12
Nodes (20): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri, Type (+12 more)

### Community 90 - "Setup"
Cohesion: 0.22
Nodes (4): Host, Host.Configurations, Setup, Program

### Community 91 - "SendRequestBody"
Cohesion: 0.29
Nodes (6): SendRequestBody, HtmlContent, Sender, Subject, TextContent, To

### Community 92 - ".SaveWithOutboxAsync"
Cohesion: 0.22
Nodes (7): AuditLogEntry, AggregateId, AggregateType, Event, EventType, Version, OutboxSaveHelper

### Community 93 - ".GetClientKey"
Cohesion: 0.20
Nodes (4): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, Response, ClientKey, DummyCaptchaService

### Community 94 - ".SendAsync"
Cohesion: 0.09
Nodes (18): ISmsGateway, SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, SmsMessage, DummySmsGateway, SendMessageBody (+10 more)

### Community 95 - "Response"
Cohesion: 0.22
Nodes (7): Products.Endpoints.Products.v1.My.Search, Endpoint, Response, Description, Name, Price, Quantity

### Community 96 - "ILogger"
Cohesion: 0.10
Nodes (8): GlobalExceptionHandlingMiddleware, LoginCompletion, RegistrationCompletion, ReCaptchaService, BrevoEmailGateway, SendResponseBody, MessageId, NotificationsHub

### Community 97 - "How it works"
Cohesion: 0.40
Nodes (5): How it works, One-time setup _(Build, in a migration)_, Query path, Ranking, Write path

### Community 98 - "AuditableEntityResponse"
Cohesion: 0.08
Nodes (22): Products.Endpoints.Products.v1.My.Get, Products.Endpoints.Products.v1.Search, Common.Application.DTOs, Products.Endpoints.Products.v1.Get, AuditableEntityResponse, CreatedBy, CreatedOn, Id (+14 more)

### Community 99 - "InterModuleRequestOptions"
Cohesion: 0.25
Nodes (7): InterModuleRequestOptions, DependencyUnavailableRetryAfterSeconds, HandlerConcurrentMessageLimit, HandlerPrefetchCount, Timeouts, TimeoutSeconds, InterModuleRequestOptionsValidator

### Community 100 - "InboxStore"
Cohesion: 0.21
Nodes (8): IInboxCleanupTarget, ModuleName, IInboxStore, StatelessInboxStore, Instance, Setup, InboxStore, ModuleName

### Community 101 - "ResiliencyOptions"
Cohesion: 0.09
Nodes (23): KeyedResilienceProfile, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, RateLimitFailOpen, RateLimitPermits (+15 more)

### Community 103 - "FirebasePushGateway.cs"
Cohesion: 0.22
Nodes (3): Notifications.Application.Push, Notifications.Infrastructure.Push, Notifications.Infrastructure.Push.Firebase

### Community 104 - "ResultToResponseTransformer.cs"
Cohesion: 0.18
Nodes (8): Common.Application.EndpointFilters, ProblemResponse, ResultToAcceptedResponseTransformer, ResultToCreatedResponseTransformer, ResultToResponseTransformer, ResultToResponseTransformer, RouteHandlerBuilderExtensions, RequestBodyLimitFilter

### Community 105 - "Response"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegisterByEmail, Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 106 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.10
Nodes (14): Notifications.Application.Otp, Common.Application.Caching, IAM.Endpoints.Otp, Notifications.Application.Persistence, Notifications.Infrastructure.Telemetry, Common.InterModuleRequests.IAM, Common.InterModuleRequests.Contracts, Notifications.Infrastructure.InterModuleRequestHandlers (+6 more)

### Community 107 - ".PaginateAsync"
Cohesion: 0.11
Nodes (4): CollectionExtensions, DbContextExtensions, PersistenceQueryableExtensions, Endpoint

### Community 109 - "Response"
Cohesion: 0.33
Nodes (4): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, Endpoint, Response, IsRegistered

### Community 111 - "VerifyEmail/Request.cs"
Cohesion: 0.50
Nodes (4): Request, Email, Otp, RequestValidator

### Community 112 - "IModule"
Cohesion: 0.06
Nodes (25): Cross-process call path, How it works, Setup, ICoreModule, IModule, ActivitySourceNames, MeterNames, Name (+17 more)

### Community 114 - "Response"
Cohesion: 0.08
Nodes (22): IAM.Endpoints.Users.VersionNeutral.Search, IAM.Endpoints.Users.VersionNeutral.Get, Response, BirthDate, CreatedOn, Email, Enabled, FirstName (+14 more)

### Community 115 - "Type"
Cohesion: 0.27
Nodes (3): StronglyTypedIdReadOnlyJsonConverter, InterModuleRequestHandlerDefinition, RequestType

### Community 117 - "Response"
Cohesion: 0.11
Nodes (15): Inventory.Endpoints.StockReservations.v1.Get, ReservationStatus, Active, Committed, Expired, Released, RequiresReconciliation, Endpoint (+7 more)

### Community 118 - "DatabaseOptions.cs"
Cohesion: 0.67
Nodes (3): DatabaseOptions, ConnectionString, DatabaseOptionsValidator

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

### Community 124 - "Setup.HealthChecks.cs"
Cohesion: 0.22
Nodes (5): HealthCheckOptions, EnableHealthChecks, ReadinessTimeoutInSeconds, StartupTimeoutInSeconds, HealthCheckOptionsValidator

### Community 126 - "Pagination and search"
Cohesion: 0.36
Nodes (3): Pagination and search, LikePattern, TrigramIndexExtensions

### Community 127 - "Response"
Cohesion: 0.20
Nodes (8): Products.Endpoints.Stores.v1.Get, Endpoint, Response, Address, Description, Name, OwnerId, ProductCount

### Community 128 - "PaginationRequest"
Cohesion: 0.04
Nodes (54): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Stores.v1.My.AuditLog, Products.Endpoints.Products.v1.AuditLog, PaginationRequest, After, IncludeTotal, PageNumber, PageSize (+46 more)

### Community 129 - "Response"
Cohesion: 0.29
Nodes (4): Products.Endpoints.ProductTemplates.v1.Create, Setup, Response, Id

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.25
Nodes (10): Observability (OpenTelemetry), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), external, name, networks, local_shared_network, Postgres Service (mm.postgres) (+2 more)

### Community 131 - "IServiceCollection"
Cohesion: 0.07
Nodes (9): ModulesOptions, EnabledModules, ModulesOptionsValidator, Setup, Setup, Setup, ModuleRegistry, Setup (+1 more)

### Community 132 - "ObjectStorageOptions"
Cohesion: 0.08
Nodes (26): ObjectStorageOptions, AccessKey, AdditionalTransientStatusCodes, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds (+18 more)

### Community 134 - ".GetProductAsync"
Cohesion: 0.15
Nodes (13): Inventory.Infrastructure.InterModuleRequestHandlers, Common.InterModuleRequests.Inventory, GetStockLevelRequest, GetStockLevelResponse, GetStockLevelRequestHandler, Setup, Endpoint, Response (+5 more)

### Community 136 - "Response"
Cohesion: 0.25
Nodes (6): Products.Endpoints.ProductTemplates.v1.Get, Endpoint, Response, Brand, Color, Model

### Community 138 - "services"
Cohesion: 0.25
Nodes (8): container_name, environment, image, networks, ports, restart, services, mm.aspire-dashboard

### Community 139 - "mm.postgres"
Cohesion: 0.25
Nodes (8): container_name, environment, image, networks, ports, restart, volumes, mm.postgres

### Community 140 - "StockLevel"
Cohesion: 0.11
Nodes (8): Inventory.Domain.StockLevels.DomainEvents.v1, StronglyTypedIdHelper, V1StockLevelCreatedDomainEvent, StockLevel, ProductId, QuantityOnHand, StockLevelId, StockLevelConfiguration

### Community 141 - "Type"
Cohesion: 0.16
Nodes (6): StrictDateTimeOffsetJsonConverter, StronglyTypedIdListReadOnlyJsonConverter, StronglyTypedIdReadOnlyJsonConverter, StronglyTypedIdWriteOnlyJsonConverter, IStronglyTypedId, Value

### Community 142 - "mm.rabbitmq"
Cohesion: 0.25
Nodes (8): container_name, environment, image, networks, ports, restart, volumes, mm.rabbitmq

### Community 143 - "Type"
Cohesion: 0.31
Nodes (3): PaginationCursor, Payload, TaggedValue

### Community 144 - "AI-assisted development workflow"
Cohesion: 0.25
Nodes (8): Adapting it, AI-assisted development workflow, `CLAUDE.md`, Guards, The commands, The four layers, The issue-to-pull-request automation, The knowledge graph

### Community 145 - "FixedWindow"
Cohesion: 0.40
Nodes (5): FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit

### Community 146 - "RequestBodyLimitMetadata"
Cohesion: 0.25
Nodes (5): Webhooks, RequestBodyLimitEndpointExtensions, RequestBodyLimitMetadata, MaxBodyBytes, RequestBodyLimitMiddleware

### Community 147 - ".HandleAsync"
Cohesion: 0.52
Nodes (3): GetSeedUserIdsRequest, GetSeedUserIdsResponse, GetSeedUserIdsRequestHandler

### Community 148 - ".ReleaseStockReservationAsync"
Cohesion: 0.22
Nodes (5): IWarehouseGateway, DummyWarehouseGateway, HttpWarehouseGateway, ReleaseRequestBody, ReleaseResponseBody

### Community 149 - "ReCaptchaResponse"
Cohesion: 0.29
Nodes (6): ReCaptchaResponse, ChallengeTs, ErrorCodes, Hostname, Score, Success

### Community 150 - "Common.InterModuleRequests/Setup.cs"
Cohesion: 0.33
Nodes (3): Common.InterModuleRequests, IAssemblyReference, Setup

### Community 151 - "OutboxOptions.cs"
Cohesion: 0.32
Nodes (7): OutboxCleanupSettings, BatchSize, CronSchedule, Enabled, RetentionDays, OutboxCleanupSettingsValidator, OutboxOptionsValidator

### Community 152 - "Request"
Cohesion: 0.29
Nodes (7): Request, Description, Name, Price, ProductTemplateId, Quantity, RequestValidator

### Community 153 - "WebhookCallback/Request.cs"
Cohesion: 0.40
Nodes (5): Inventory.Endpoints.StockReservations.v1.WebhookCallback, Request, ProviderReference, ReservationId, RequestValidator

### Community 155 - "v1/RemoveProduct/Request.cs"
Cohesion: 0.40
Nodes (5): Products.Endpoints.Stores.v1.RemoveProduct, Request, Id, ProductId, RequestValidator

### Community 156 - "InterModuleRequestHandler"
Cohesion: 0.20
Nodes (10): IInterModuleRequest, IInterModuleRequestHandler, InterModuleRequestHandler, DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, GetProductRequest, GetProductResponse (+2 more)

### Community 158 - "Activate/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.ProductTemplates.v1.Activate, Request, Id, RequestValidator

### Community 159 - "Request"
Cohesion: 0.33
Nodes (6): Request, Address, Description, Name, OwnerId, RequestValidator

### Community 165 - "ApplicationUserId"
Cohesion: 0.10
Nodes (16): IBackgroundUserContext, UserId, ApplicationUserId, IsEmpty, Value, BackgroundUserContext, UserId, CurrentUser (+8 more)

### Community 166 - "StoreStatus"
Cohesion: 0.50
Nodes (3): StoreStatus, Active, Inactive

### Community 172 - "Product"
Cohesion: 0.07
Nodes (27): Products.Domain.Products.DomainEvents.v1, File map _(Build)_, ISearchLocalized, Language, V1ProductDescriptionUpdatedDomainEvent, V1ProductNameUpdatedDomainEvent, V1ProductPriceDecreasedDomainEvent, V1ProductPriceIncreasedDomainEvent (+19 more)

### Community 173 - "Task"
Cohesion: 0.04
Nodes (16): ICurrentUser, Id, IdAsString, Roles, SessionId, IVariantFeatureManagerExtensions, IInboxStore, ObjectMetadata (+8 more)

### Community 174 - "KeycloakTokenClient.cs"
Cohesion: 0.09
Nodes (10): IAM.Endpoints.Users, IAM.Infrastructure.Keycloak.Representations, Inventory.Infrastructure.Gateway, Inventory.Application.Gateway, IAM.Domain.Errors, IAM.Endpoints.Tokens, IAM.Infrastructure.Keycloak, OAuthErrors (+2 more)

### Community 175 - "IInterModuleRequest"
Cohesion: 0.57
Nodes (4): GetUsersInRolePageRequest, GetUsersInRolePageResponse, RoleUserSummary, GetUsersInRolePageRequestHandler

### Community 177 - "CorsOptions"
Cohesion: 0.29
Nodes (7): CorsOptions, AllowCredentials, AllowedHeaders, AllowedMethods, AllowedOrigins, MaxAgeInSeconds, CorsOptionsValidator

### Community 179 - "CachingOptions"
Cohesion: 0.11
Nodes (21): CachingEntryDefaults, Duration, FactoryHardTimeout, FactorySoftTimeout, FailSafeMaxDuration, FailSafeThrottleDuration, CachingOptions, AllowInMemoryOnlyInProduction (+13 more)

### Community 180 - "SmsOptions"
Cohesion: 0.11
Nodes (20): SmsOptions, AppName, AttemptTimeoutSeconds, BaseUrl, MaxPerDay, MaxPerPhoneNumberPerDay, MaxRetryAttempts, MsgHeader (+12 more)

### Community 181 - "RabbitMqOptions"
Cohesion: 0.15
Nodes (13): RabbitMqOptions, DefaultConcurrentMessageLimit, DefaultPrefetchCount, Host, Password, Port, RetryIntervalDeltaMs, RetryLimit (+5 more)

### Community 183 - "Response"
Cohesion: 0.33
Nodes (4): Products.Endpoints.Stores.v1.AddProduct, Endpoint, Response, Id

### Community 184 - "Response"
Cohesion: 0.33
Nodes (4): Products.Endpoints.Stores.v1.My.AddProduct, Endpoint, Response, Id

### Community 185 - "Response"
Cohesion: 0.20
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegister, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt, Setup

### Community 187 - "ReverseProxyOptions"
Cohesion: 0.33
Nodes (5): ReverseProxyOptions, ForwardLimit, IsEnabled, TrustedNetworks, ReverseProxyOptionsValidator

### Community 188 - "BoundedCaptureStream"
Cohesion: 0.16
Nodes (6): BoundedCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 190 - "KeycloakPermissionClient"
Cohesion: 0.42
Nodes (4): KeycloakPermissionClient, TokenErrorRepresentation, Error, ErrorDescription

### Community 195 - "Response"
Cohesion: 0.33
Nodes (4): Inventory.Endpoints.StockReservations.v1.Reserve, Endpoint, Response, Id

### Community 200 - "system_diagnostics"
Cohesion: 0.05
Nodes (14): BackgroundJobs.Telemetry, BackgroundJobs, RequireFeatureFilter, RouteHandlerBuilderExtensions, JobMetricsFilter, SkipOverlappingRecurringJobFilter, BackgroundJobsTelemetry, IamTelemetry (+6 more)

### Community 201 - "KeycloakScopes"
Cohesion: 0.20
Nodes (9): Devices, Hangfire, KeycloakScopes, Products, ProductTemplates, Sessions, StockReservations, Stores (+1 more)

### Community 202 - "DevicesOptions"
Cohesion: 0.40
Nodes (5): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator

### Community 211 - "Key decisions"
Cohesion: 0.29
Nodes (7): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Key decisions

### Community 215 - "Response"
Cohesion: 0.17
Nodes (10): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, Endpoint, Response, ClientId, DeviceName, Id, IpAddress, IsCurrent (+2 more)

### Community 217 - "Platform building blocks"
Cohesion: 0.11
Nodes (16): Authorization, Caching, Feature flags, Inter-module requests, Job rows, Object storage, Observability, Options and configuration (+8 more)

### Community 221 - "Keycloak realm as code"
Cohesion: 0.25
Nodes (8): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Secrets, Seed users (dev / test only), Signing in through the API

### Community 234 - "BackgroundJobsModule.cs"
Cohesion: 0.13
Nodes (3): Common.Application.BackgroundJobs, BackgroundJobs, IAM.Infrastructure.Auth

### Community 236 - "NetGsmSmsGateway"
Cohesion: 0.21
Nodes (5): NetGsmSmsGateway, SendResponseBody, Code, Description, JobId

### Community 241 - "ResxLocalizationOptions"
Cohesion: 0.40
Nodes (5): Localization, ResxLocalizationOptions, DefaultCulture, SupportedCultures, ResxLocalizationOptionsValidator

### Community 242 - "Response"
Cohesion: 0.25
Nodes (6): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 243 - "RequestBody"
Cohesion: 0.08
Nodes (31): Inventory.Endpoints.StockReservations.v1.Commit, Products.Endpoints.Stores.v1.Update, Request, Body, Id, RequestBody, ProviderReference, RequestBodyValidator (+23 more)

### Community 251 - "Response"
Cohesion: 0.33
Nodes (4): Products.Endpoints.Stores.v1.My.Create, Endpoint, Response, Id

### Community 261 - "Response"
Cohesion: 0.29
Nodes (4): Products.Endpoints.Stores.v1.Create, Setup, Response, Id

### Community 264 - "S3ObjectStoreCore"
Cohesion: 0.16
Nodes (5): IPrivateObjectStore, IPublicObjectStore, S3ObjectStoreCore, S3PrivateObjectStore, S3PublicObjectStore

### Community 269 - "RouteGroupBuilder"
Cohesion: 0.04
Nodes (24): Setup, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint (+16 more)

### Community 276 - "microsoft_aspnetcore_mvc"
Cohesion: 0.05
Nodes (37): Products.Endpoints.Stores.v1.Deactivate, Products.Endpoints.ProductTemplates.v1.Deactivate, Products.Endpoints.Stores.v1.My.RemoveProduct, Inventory.Endpoints.StockReservations.v1.Release, Common.Application.ModelBinders, ProblemDetailsExtensions, Request, Id (+29 more)

### Community 280 - "SendForEmail/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForEmail, Request, CaptchaToken, Email, RequestValidator

### Community 282 - "Request"
Cohesion: 0.25
Nodes (8): Request, ClientId, DeviceId, DeviceName, Otp, PhoneNumber, PushToken, RequestValidator

### Community 283 - "Request"
Cohesion: 0.22
Nodes (9): Request, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken, Password, PushToken (+1 more)

### Community 296 - "AuditLogOptions"
Cohesion: 0.33
Nodes (6): AuditLogOptions, PerSchemaRetentionDays, PurgeBatchSize, RetentionCron, RetentionDays, AuditLogOptionsValidator

### Community 299 - "ProjectionReconciliationOptions.cs"
Cohesion: 0.50
Nodes (4): ProjectionReconciliationOptions, MaxPages, PageSize, ProjectionReconciliationOptionsValidator

### Community 314 - "CustomValidator"
Cohesion: 0.07
Nodes (30): CustomRateLimitingOptionsValidator, FixedWindowValidator, JobHousekeepingOptions, PageSize, RetentionHours, StaleAfterMinutes, JobHousekeepingOptionsValidator, SecurityHeadersOptions (+22 more)

### Community 319 - "Products/v1/Update/Request.cs"
Cohesion: 0.20
Nodes (11): Products.Endpoints.Products.v1.Update, Request, Body, Id, RequestBody, Description, Name, Price (+3 more)

### Community 320 - "IAMModule.cs"
Cohesion: 0.07
Nodes (17): Common.Infrastructure.Modules, Notifications.Infrastructure.Devices, Inventory.Endpoints, Products.Endpoints.Stores, Common.Endpoints.Versioning, Products.Endpoints.Probe, Products.Endpoints.Products, IAM.Domain.Captcha (+9 more)

### Community 323 - "v1/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Probe.v1, Request, Count, RequestValidator

### Community 324 - "Products/v1/My/Update/Request.cs"
Cohesion: 0.20
Nodes (11): Products.Endpoints.Products.v1.My.Update, Request, Body, Id, RequestBody, Description, Name, Price (+3 more)

## Knowledge Gaps
- **901 isolated node(s):** `UserId`, `Id`, `IdAsString`, `Roles`, `SessionId` (+896 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1276 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **120 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `IServiceCollection`, `ObjectStorageOptions`, `Setup.Observability.cs`, `Common.Domain.ResultMonad`, `EmailOptions`, `Common.Domain.StronglyTypedIds`, `FullTextSearchOptions`, `OutboxOptions.cs`, `Setup.Logger.cs`, `fluentvalidation`, `Common.Domain.StronglyTypedIds`, `ObservabilityOptions`, `AuditLogOptions`, `S3ObjectStoreCore.cs`, `ProjectionReconciliationOptions.cs`, `KeycloakTokenClient.cs`, `CorsOptions`, `BackgroundJobsOptions`, `CaptchaOptions`, `CachingOptions`, `RabbitMqOptions`, `SmsOptions`, `CustomValidator`, `RequestLoggingOptions`, `IAMModule.cs`, `DevicesOptions`, `OpenApiOptions`, `KeycloakOptions`, `IOptions`, `DateTimeOffset`, `PushOptions`, `Setup`, `InterModuleRequestOptions`, `ResiliencyOptions`, `FirebasePushGateway.cs`, `BackgroundJobsModule.cs`, `Common.InterModuleRequests.Contracts`, `ResxLocalizationOptions`, `DatabaseOptions.cs`, `Response`, `Setup.HealthChecks.cs`?**
  _High betweenness centrality (0.095) - this node is a cross-community bridge._
- **What connects `UserId`, `Id`, `IdAsString` to the rest of the system?**
  _901 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `PaginationResponse` be split into smaller, more focused modules?**
  _Cohesion score 0.11264367816091954 - nodes in this community are weakly interconnected._
- **Why does `Result` connect `Result` to `PaginationResponse`, `ApplicationUserId`, `ProductTemplateId`, `.GetProductAsync`, `DomainEvent`, `.ReleaseStockReservationAsync`, `.RegisterAsync`, `SmsOtpDispatchOutcome`, `Task`, `Task`, `VerifyPhoneOtpResponse`, `SendEmailOtpRequestHandler`, `DateTimeOffset`, `.GetClientKey`, `.SendAsync`, `ILogger`, `.GetMeAsync`, `.PaginateAsync`, `NetGsmSmsGateway`?**
  _High betweenness centrality (0.069) - this node is a cross-community bridge._
- **Should `ApplicationUserId` be split into smaller, more focused modules?**
  _Cohesion score 0.14616755793226383 - nodes in this community are weakly interconnected._
- **Why does `ApplicationUserId` connect `ApplicationUserId` to `PaginationResponse`, `NotificationPayload`, `PaginationRequest`, `ApplicationUserId`, `ProductTemplateId`, `IStronglyTypedId`, `StockLevel`, `Type`, `Response`, `KeycloakPermissionAuthorizationHandler`, `DomainEvent`, `.HandleAsync`, `microsoft_aspnetcore_mvc`, `DeviceRegistration`, `IReadOnlyList`, `.RegisterAsync`, `InterModuleRequestHandler`, `Request`, `OutboxMessage`, `For`, `Common.Domain.StronglyTypedIds`, `DefaultIdType`, `Task`, `IInterModuleRequest`, `SendSecurityAlertRequestHandler`, `V1StoreCreatedDomainEvent`, `Response`, `Response`, `ProductTemplate`, `ILogger`, `AuditableEntityResponse`, `.GetMeAsync`, `Response`, `Response`?**
  _High betweenness centrality (0.044) - this node is a cross-community bridge._
- **Should `ProductTemplateId` be split into smaller, more focused modules?**
  _Cohesion score 0.10317460317460317 - nodes in this community are weakly interconnected._