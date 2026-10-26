# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-10-26)

## Corpus Check
- 588 files · ~95,443 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 4011 nodes · 10608 edges · 309 communities (186 shown, 123 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 350 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `47c0990d`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- IProductsDbContext
- NotificationPayload
- HttpContext
- AuditableEntityResponse
- ApplicationUserId
- StoreId
- ApplySearchLanguageInterceptor
- IDbContext
- Result
- Common.Domain.ResultMonad
- IStronglyTypedId
- ISearchLanguageResolver
- Response
- KeycloakPermissionAuthorizationHandler
- EmailOptions
- Product
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
- IModule
- ILogger
- Response
- fluentvalidation
- OutboxMessage
- CustomRateLimitingOptions
- AdminRepresentations.cs
- ProblemDetailsExtensions.cs
- ObservabilityOptions
- DefaultResponsesOperationFilter
- IOtpService
- S3ObjectStoreCore.cs
- ICaptchaService
- KeycloakOptions
- ConfigureSwaggerOptions.cs
- PaginationQueryableExtensions
- Outbox Misuse Check
- OtpOptions
- Add Integration Event Command
- host
- BackgroundJobsOptions
- CaptchaOptions
- HttpResponseMessage
- RedisFixedWindowRateLimiter
- BrevoEmailGateway
- Infrastructure/StringExtensions.cs
- Cross-Module Reference Violation
- TimeProvider
- Request
- Bogus Test Data
- KeycloakTokenClient
- StronglyTypedIdBinder
- RequestLoggingOptions
- Request
- Task
- OutboxModule
- .AddNotificationsSignalR
- ValueObject
- Request
- RequestBody
- Full-Text Search
- ValidationContextExtensions
- VerifyPhoneOtpResponse
- AsNoTracking Coverage Check
- Request
- Response
- OpenApiOptions
- KeyedResilienceProfile
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
- .TryDeserialize
- .GetClientKey
- SendRequestBody
- Response
- LoggerMessage
- How it works
- Response
- InterModuleRequestOptions
- ReCaptchaService
- ResiliencyOptions
- .GetMeAsync
- Response
- ResultToResponseTransformer.cs
- Response
- Common.InterModuleRequests.Contracts
- InventoryOptions
- ChunkEncodingFreeS3Client
- Response
- KeycloakPermissionPolicyProvider
- VerifyEmail/Request.cs
- InventoryModule
- ConfigureSwaggerOptions
- Response
- SkipOverlappingRecurringJobFilter
- IResxLocalizer
- Response
- DatabaseOptions.cs
- depends_on
- healthcheck
- mm.keycloak
- mm.redis
- IdentityScheme
- IApplicationBuilder
- InterModuleRequestHandlerDefinition
- NotificationsHub
- Response
- PaginationRequestValidator
- Response
- docker-compose.yml (Base Stack)
- IServiceCollection
- ObjectStorageOptions
- Response
- Request
- InterModuleRequestHandler
- .WriteTooManyRequestsToResponse
- services
- mm.postgres
- DefaultIdType
- Type
- mm.rabbitmq
- Type
- KeyedResiliencePipelines
- FixedWindow
- BackgroundJobsTelemetry
- PaginationRequest
- HttpWarehouseGateway
- DisableChunkEncodingHandler
- IamTelemetry
- OutboxOptions
- Request
- SendForRegistration/Request.cs
- IResxLocalizer
- GetDeviceSessionsRequest
- .Generate
- Request
- Request
- HttpContextTargetingContextAccessor
- ObjectStorageOptions.cs
- PolymorphicEventConverter
- For
- IssueVerificationTokenRequestHandler
- ApplicationUserId
- Request
- Request
- Request
- My/AuditLog/Request.cs
- Notifications.Application/IAssemblyReference.cs
- StringExtensions
- Common.Domain.Events
- Task
- KeycloakTokenClient.cs
- IInterModuleRequest
- KeyedRow
- CorsOptions
- Swagger/Setup.cs
- CachingOptions
- SmsOptions
- RabbitMqOptions
- IAM.Domain
- Response
- Response
- Response
- .SetRetryAfterHeader
- ReverseProxyOptions
- BoundedCaptureStream
- KeycloakPermissionClient
- Request
- Request
- Request
- Request
- Response
- Request
- Request
- Request
- Request
- NotificationsTelemetry
- KeycloakScopes
- DevicesOptions
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- Key decisions
- Response
- FeatureFlags
- Keycloak realm as code
- KeycloakPermissionAuthorizationHandler.cs
- NetGsmSmsGateway
- RequestBodyLimitFilter.cs
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
- ReCaptchaResponse
- BackgroundJobsModule
- RouteGroupBuilder
- CustomValidator
- Response
- SendForEmail/Request.cs
- Request
- Request
- AuditLogOptions
- ProjectionReconciliationOptions.cs
- ResultTelemetryExtensions
- Request
- JobHousekeepingOptions
- Products.Endpoints
- Products/v1/Update/Request.cs
- Notifications.Infrastructure
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
- Project Instructions (CLAUDE.md)
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
- `Concurrent safety` --references--> `OutboxProcessor`  [INFERRED]
  docs/split-deployment-poc.md → src/Modules/Outbox/Outbox/OutboxProcessor.cs
- `Add search to a new entity _(Build checklist)_` --references--> `FullTextSearchOptions`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Application/Options/FullTextSearchOptions.cs
- `Full-Text Search` --references--> `FullTextSearchOptions`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Application/Options/FullTextSearchOptions.cs
- `Gotchas` --references--> `FullTextSearchOptions`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Application/Options/FullTextSearchOptions.cs
- `Configuration` --references--> `ISearchLanguageResolver`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Application/Search/ISearchLanguageResolver.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Local Infrastructure Stack** — docker_compose_postgres, docker_compose_rabbitmq, docker_compose_redis, docker_compose_aspire_dashboard [EXTRACTED 1.00]

## Communities (309 total, 123 thin omitted)

### Community 0 - "IProductsDbContext"
Cohesion: 0.06
Nodes (23): AuditLogDto, ICurrentUser, Id, IdAsString, Roles, SessionId, PaginationResponse, HasNext (+15 more)

### Community 1 - "NotificationPayload"
Cohesion: 0.19
Nodes (5): INotificationDispatcher, INotificationsClient, NotificationPayload, PushMessage, SignalRNotificationDispatcher

### Community 3 - "AuditableEntityResponse"
Cohesion: 0.11
Nodes (16): AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, Endpoint, Response (+8 more)

### Community 5 - "ApplicationUserId"
Cohesion: 0.15
Nodes (13): KeycloakUser, KeycloakAdminClient, UserRepresentation, Attributes, CreatedTimestamp, Credentials, Email, EmailVerified (+5 more)

### Community 6 - "StoreId"
Cohesion: 0.13
Nodes (11): GetSeedUserIdsRequest, GetSeedUserIdsResponse, GetSeedUserIdsRequestHandler, V1ProductCreatedDomainEvent, ProductTemplateId, StoreId, Request, Id (+3 more)

### Community 7 - "ApplySearchLanguageInterceptor"
Cohesion: 0.19
Nodes (7): Add a new language/culture, Add search to a new entity _(Build checklist)_, Change the vector (weights, fields, or config), Extending and maintaining, ApplyAuditingInterceptor, ApplySearchLanguageInterceptor, Setup

### Community 8 - "IDbContext"
Cohesion: 0.11
Nodes (8): JobHousekeepingJob, IDbContext, AuditLog, ChangeTracker, Database, Setup, StockReservationExpirySweepJobRegistrar, StockReservationExpirySweepService

### Community 10 - "Result"
Cohesion: 0.04
Nodes (31): CollectionExtensions, Error, Key, ParameterName, StatusCode, SubErrors, Value, IResult (+23 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.10
Nodes (18): IAM.Endpoints.Captcha.VersionNeutral, Common.Application.Search, Inventory.Application.Persistence, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Common.Application.EndpointFilters, Inventory.Domain.StockReservations.Errors, Products.Infrastructure.Telemetry (+10 more)

### Community 12 - "IStronglyTypedId"
Cohesion: 0.12
Nodes (15): AggregateRoot, Events, Id, Version, IAggregateRoot, Events, Id, Version (+7 more)

### Community 13 - "ISearchLanguageResolver"
Cohesion: 0.33
Nodes (3): SearchLanguageResolver, UniversalConfig, Setup

### Community 14 - "Response"
Cohesion: 0.13
Nodes (13): IAM.Endpoints.Users.VersionNeutral.Me.Get, Endpoint, Response, BirthDate, CreatedOn, Email, FirstName, Id (+5 more)

### Community 15 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.29
Nodes (3): KeycloakPermissionAuthorizationHandler, KeycloakPermissionRequirement, Permission

### Community 16 - "EmailOptions"
Cohesion: 0.10
Nodes (23): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+15 more)

### Community 17 - "Product"
Cohesion: 0.07
Nodes (27): File map _(Build)_, ISearchLocalized, Language, Product, Description, Language, Name, Price (+19 more)

### Community 18 - "Common.Application.Options"
Cohesion: 0.03
Nodes (41): Common.Infrastructure.Modules, Notifications.Infrastructure.Devices, Common.Application.Persistence.Inbox, Notifications.Application.Push, Inventory.Endpoints, Products.Endpoints.Stores, Common.Endpoints.Versioning, Products.Endpoints.Probe (+33 more)

### Community 19 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.04
Nodes (29): Common.Infrastructure.Persistence.Inbox, Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, IAM.Endpoints.Users.VersionNeutral.Search, Products.Infrastructure.Persistence.Seeding, Inventory.Infrastructure.InterModuleRequestHandlers, Common.Domain.StronglyTypedIds, Common.Application.Persistence (+21 more)

### Community 20 - "FullTextSearchOptions"
Cohesion: 0.25
Nodes (8): Configuration, Gotchas, FullTextSearchOptions, CultureToConfig, DefaultConfig, RankWeights, UseUnaccent, FullTextSearchOptionsValidator

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.13
Nodes (6): BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 22 - "DeviceRegistration"
Cohesion: 0.15
Nodes (10): DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive, LastReconciledOn, PushToken, PushTokenUpdatedOn (+2 more)

### Community 23 - "IReadOnlyList"
Cohesion: 0.29
Nodes (7): GetActiveSessionIdsRequest, GetActiveSessionIdsResponse, UserSessionIds, GrantedPermission, KeycloakUserPage, KeycloakUserSession, GetActiveSessionIdsRequestHandler

### Community 24 - ".RegisterAsync"
Cohesion: 0.11
Nodes (16): IInterModuleRequestClient, BindDeviceSessionRequest, BindDeviceSessionResponse, DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse, VerifyEmailOtpRequest, VerifyEmailOtpResponse, IKeycloakAdminClient (+8 more)

### Community 25 - "Stores/v1/My/Update/Request.cs"
Cohesion: 0.40
Nodes (5): Request, Address, Description, Name, RequestValidator

### Community 26 - "Response"
Cohesion: 0.33
Nodes (4): Endpoint, Response, EmailVerificationToken, IsRegistered

### Community 28 - "SendForLogin/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForLogin, Request, CaptchaToken, PhoneNumber, RequestValidator

### Community 29 - "Func"
Cohesion: 0.15
Nodes (16): CheckRegistrationRateLimitingPolicy, OnRejected, EmailRateLimitingPolicy, OnRejected, OtpVerifyRateLimitingPolicy, OnRejected, RegisterRateLimitingPolicy, OnRejected (+8 more)

### Community 30 - "ProjectionUpsertOutcome"
Cohesion: 0.18
Nodes (7): Common.Application.Persistence.Projections, ProjectionEntity, SourceVersion, ProjectionUpsertOutcome, Inserted, Stale, Updated

### Community 31 - "IModule"
Cohesion: 0.07
Nodes (24): IModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority, IamModule, ActivitySourceNames (+16 more)

### Community 32 - "ILogger"
Cohesion: 0.12
Nodes (7): ProjectionReconciliationJob, AuditLogRetentionJobRegistrar, AuditLogRetentionService, Setup, IPushGateway, DummyPushGateway, FirebasePushGateway

### Community 33 - "Response"
Cohesion: 0.25
Nodes (6): Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 34 - "fluentvalidation"
Cohesion: 0.04
Nodes (52): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Stores.v1.My.Update, Inventory.Endpoints.StockReservations.v1.Commit, Products.Infrastructure.InterModuleRequestHandlers, Products.Endpoints.Stores.v1.Update, IAM.Domain.Users, Products.Endpoints.Stores.v1.Create, IAM.Endpoints.Otp.VersionNeutral.VerifyEmail (+44 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.04
Nodes (34): Common.IntegrationEvents, Outbox.Persistence, Products.Application.Products.DomainEventHandlers.v1, IOutboxMessage, CreatedOn, Event, Id, IsProcessed (+26 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.09
Nodes (12): CustomRateLimitingOptions, CheckRegistration, CreateStore, Email, ExemptPathPrefixes, Global, OtpVerify, Register (+4 more)

### Community 37 - "AdminRepresentations.cs"
Cohesion: 0.12
Nodes (16): CredentialRepresentation, Temporary, Type, Value, ErrorRepresentation, Error, ErrorMessage, Field (+8 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.09
Nodes (17): ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics, EnableTracing, LogSink, MinimumLevel (+9 more)

### Community 40 - "DefaultResponsesOperationFilter"
Cohesion: 0.26
Nodes (3): DefaultResponsesOperationFilter, RemoveDefaultResponseSchemaFilter, SwaggerDefaultValues

### Community 41 - "IOtpService"
Cohesion: 0.20
Nodes (8): OtpCacheEntry, IOtpService, OtpVerificationOutcome, InvalidOtp, Success, TooManyAttempts, RedisOtpService, Setup

### Community 43 - "ICaptchaService"
Cohesion: 0.17
Nodes (10): FeatureFlagResultExtensions, OtpDispatchErrors, SendPhoneOtpRequest, SendPhoneOtpResponse, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled (+2 more)

### Community 44 - "KeycloakOptions"
Cohesion: 0.06
Nodes (21): KeycloakOptions, AttemptTimeoutSeconds, Authority, BaseUrl, DecisionCacheMaxDurationSeconds, Realm, RequireHttpsMetadata, ResourceClientId (+13 more)

### Community 46 - "PaginationQueryableExtensions"
Cohesion: 0.17
Nodes (4): CursorBound, Value, PaginationQueryableExtensions, ParameterReplacer

### Community 48 - "OtpOptions"
Cohesion: 0.18
Nodes (11): OtpOptions, DummyCode, EmailQuotaWindowMinutes, ExpirationInMinutes, Length, MaxSendsPerEmailPerWindow, MaxSendsPerPhonePerWindow, PhoneQuotaWindowMinutes (+3 more)

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
Cohesion: 0.27
Nodes (5): FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration

### Community 55 - "BrevoEmailGateway"
Cohesion: 0.14
Nodes (9): BrevoEmailGateway, SendContact, Email, Name, SendErrorBody, Code, Message, SendResponseBody (+1 more)

### Community 58 - "TimeProvider"
Cohesion: 0.10
Nodes (15): IDatabaseSeeder, Priority, IInboxCleanupTarget, ModuleName, IInboxStore, Setup, InboxStore, ModuleName (+7 more)

### Community 59 - "Request"
Cohesion: 0.15
Nodes (13): Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken (+5 more)

### Community 61 - "KeycloakTokenClient"
Cohesion: 0.12
Nodes (12): SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, KeycloakTokenClient, TokenResponseRepresentation, AccessToken, ExpiresIn (+4 more)

### Community 63 - "RequestLoggingOptions"
Cohesion: 0.13
Nodes (15): RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes, SensitiveQueryParamPaths (+7 more)

### Community 64 - "Request"
Cohesion: 0.18
Nodes (11): Request, Body, Id, RequestBody, Description, Name, Price, ProductTemplateId (+3 more)

### Community 65 - "Task"
Cohesion: 0.06
Nodes (5): IBackgroundJobs, IRecurringBackgroundJobs, AsyncExtensions, BackgroundJobsService, RecurringBackgroundJobsService

### Community 66 - "OutboxModule"
Cohesion: 0.10
Nodes (10): AutoMigrateMarker, IAutoMigrateMarker, MigrationGuard, Setup, OutboxModule, ActivitySourceNames, MeterNames, Name (+2 more)

### Community 67 - ".AddNotificationsSignalR"
Cohesion: 0.13
Nodes (8): SignalROptions, RedisConnectionString, RequireRedisBackplaneInProduction, UseRedisBackplane, SignalROptionsValidator, Setup, Setup, SubjectUserIdProvider

### Community 69 - "Request"
Cohesion: 0.17
Nodes (12): Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FirstName, LastName (+4 more)

### Community 70 - "RequestBody"
Cohesion: 0.18
Nodes (10): RequestBody, FirstDeadline, IntervalDays, OccurrenceCount, ProductId, QuantityPerOccurrence, Warehouses, WarehouseAllocation (+2 more)

### Community 71 - "Full-Text Search"
Cohesion: 0.33
Nodes (6): Full-Text Search, Non-goals, Per-entity strategy, Testing, The problem, TL;DR: what the feature gives you

### Community 73 - "VerifyPhoneOtpResponse"
Cohesion: 0.24
Nodes (8): OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, VerifyPhoneOtpResponseExtensions, VerifyPhoneOtpRequestHandler

### Community 75 - "Request"
Cohesion: 0.29
Nodes (7): Request, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name, SearchTerm

### Community 76 - "Response"
Cohesion: 0.25
Nodes (7): Endpoint, Response, Address, Description, Name, OwnerId, ProductCount

### Community 77 - "OpenApiOptions"
Cohesion: 0.22
Nodes (9): OpenApiOptions, ContactEmail, ContactName, Description, EnableSwagger, LicenseName, LicenseUrl, Title (+1 more)

### Community 78 - "KeyedResilienceProfile"
Cohesion: 0.20
Nodes (10): KeyedResilienceProfile, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, RateLimitFailOpen, RateLimitPermits (+2 more)

### Community 79 - "DeviceRegistryReconciliationService"
Cohesion: 0.25
Nodes (3): DeviceRegistryReconcileJobRegistrar, DeviceRegistryReconciliationService, Setup

### Community 80 - "SendEmailOtpRequestHandler"
Cohesion: 0.29
Nodes (8): EmailOtpDispatchErrors, EmailOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, SendEmailOtpRequest, SendEmailOtpResponse, SendEmailOtpRequestHandler

### Community 81 - "Response"
Cohesion: 0.25
Nodes (7): Endpoint, Response, Address, Description, Name, OwnerId, ProductCount

### Community 82 - "Split-Deployment PoC"
Cohesion: 0.25
Nodes (6): Concurrent safety, Files added by this PoC, How to run, Split-Deployment PoC, What this proves, README (Boilerplate Overview)

### Community 83 - "IOptions"
Cohesion: 0.10
Nodes (17): ProductCreatedIntegrationEvent, CachedCaptchaService, CreateStockLevelOnProductCreatedHandler, LogProductCatalogChangeHandler, IInventoryDbContext, StockLevels, StockReservations, IEmailGateway (+9 more)

### Community 84 - "ProductTemplate"
Cohesion: 0.07
Nodes (20): ProcessedMessage, ConsumerName, MessageId, ProcessedOn, AuditableEntityConfiguration, AuditLogEntryConfiguration, JobRowConfiguration, TrigramIndexExtensions (+12 more)

### Community 85 - "DateTimeOffset"
Cohesion: 0.05
Nodes (34): Inventory.Domain.StockReservations.DomainEvents.v1, JobRow, FailureKey, FinishedOn, Progress, QueuedOn, RequestedBy, StartedOn (+26 more)

### Community 86 - ".SaveWithOutboxAsync"
Cohesion: 0.07
Nodes (26): IIntegrationEventOutbox, IdBound, Value, JobClaimExtensions, ProjectionUpsertExtensions, EventDispatcher, IntegrationEventOutbox, HasPending (+18 more)

### Community 87 - "PushOptions"
Cohesion: 0.12
Nodes (20): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri, Type (+12 more)

### Community 90 - "Setup"
Cohesion: 0.20
Nodes (4): Host, Host.Configurations, Setup, Program

### Community 91 - "SendRequestBody"
Cohesion: 0.29
Nodes (6): SendRequestBody, HtmlContent, Sender, Subject, TextContent, To

### Community 93 - ".GetClientKey"
Cohesion: 0.18
Nodes (5): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, Endpoint, Response, ClientKey, DummyCaptchaService

### Community 94 - "SendRequestBody"
Cohesion: 0.29
Nodes (6): SendRequestBody, AppName, Encoding, IysFilter, Messages, MsgHeader

### Community 95 - "Response"
Cohesion: 0.29
Nodes (6): Endpoint, Response, Description, Name, Price, Quantity

### Community 96 - "LoggerMessage"
Cohesion: 0.07
Nodes (11): DatabaseSeederOrchestrator, SeedingCompletionTracker, Setup, GlobalExceptionHandlingMiddleware, LoginCompletion, RegistrationCompletion, OutboxProcessor, PublishOutcome (+3 more)

### Community 97 - "How it works"
Cohesion: 0.40
Nodes (5): How it works, One-time setup _(Build, in a migration)_, Query path, Ranking, Write path

### Community 98 - "Response"
Cohesion: 0.29
Nodes (6): Endpoint, Response, Description, Name, Price, Quantity

### Community 99 - "InterModuleRequestOptions"
Cohesion: 0.20
Nodes (8): InterModuleRequestOptions, DependencyUnavailableRetryAfterSeconds, HandlerConcurrentMessageLimit, HandlerPrefetchCount, Timeouts, TimeoutSeconds, InterModuleRequestOptionsValidator, MassTransitInterModuleRequestClient

### Community 101 - "ResiliencyOptions"
Cohesion: 0.15
Nodes (13): KeyedResilienceProfileValidator, ResiliencyOptions, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, Keyed (+5 more)

### Community 103 - "Response"
Cohesion: 0.29
Nodes (6): Endpoint, Response, Description, Name, Price, Quantity

### Community 104 - "ResultToResponseTransformer.cs"
Cohesion: 0.13
Nodes (8): ProblemResponse, ResultToAcceptedResponseTransformer, ResultToCreatedResponseTransformer, ResultToResponseTransformer, ResultToResponseTransformer, RouteHandlerBuilderExtensions, StringLocalizerExtensions, RequestBodyLimitFilter

### Community 105 - "Response"
Cohesion: 0.25
Nodes (6): Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 106 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.06
Nodes (27): Notifications.Application.Otp, IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, Common.InterModuleRequests, IAM.Endpoints.Tokens.VersionNeutral.Create, IAM.Endpoints.Users, IAM.Endpoints.Users.VersionNeutral.SelfRegister, Common.Application.FeatureManagement, IAM.Endpoints.Otp (+19 more)

### Community 107 - "InventoryOptions"
Cohesion: 0.07
Nodes (26): Inventory.Endpoints.StockReservations.v1.ReserveSeries, InventoryOptions, MaxOccurrencesPerSeries, SafetyBufferQuantity, StockReservationExpirySweepBatchSize, StockReservationExpirySweepCron, WarehouseGatewayBaseUrl, WarehouseGatewayProvider (+18 more)

### Community 109 - "Response"
Cohesion: 0.40
Nodes (3): Endpoint, Response, IsRegistered

### Community 111 - "VerifyEmail/Request.cs"
Cohesion: 0.50
Nodes (4): Request, Email, Otp, RequestValidator

### Community 112 - "InventoryModule"
Cohesion: 0.16
Nodes (6): Setup, InventoryModule, ActivitySourceNames, MeterNames, Name, StartupPriority

### Community 114 - "Response"
Cohesion: 0.10
Nodes (20): Response, BirthDate, CreatedOn, Email, Enabled, FirstName, Id, LastName (+12 more)

### Community 115 - "SkipOverlappingRecurringJobFilter"
Cohesion: 0.15
Nodes (3): BackgroundJobs.Telemetry, JobMetricsFilter, SkipOverlappingRecurringJobFilter

### Community 117 - "Response"
Cohesion: 0.12
Nodes (14): ReservationStatus, Active, Committed, Expired, Released, RequiresReconciliation, Endpoint, Response (+6 more)

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

### Community 123 - "IdentityScheme"
Cohesion: 0.28
Nodes (7): IdentityScheme, Email, PhoneNumber, IdentitySchemeOptions, Scheme, IdentitySchemeOptionsValidator, Setup

### Community 124 - "IApplicationBuilder"
Cohesion: 0.16
Nodes (5): HealthCheckOptions, EnableHealthChecks, ReadinessTimeoutInSeconds, StartupTimeoutInSeconds, HealthCheckOptionsValidator

### Community 125 - "InterModuleRequestHandlerDefinition"
Cohesion: 0.16
Nodes (3): ConcurrencyConfiguratorExtensions, InterModuleRequestHandlerDefinition, RequestType

### Community 127 - "Response"
Cohesion: 0.25
Nodes (7): Endpoint, Response, Address, Description, Name, OwnerId, ProductCount

### Community 128 - "PaginationRequestValidator"
Cohesion: 0.22
Nodes (9): PaginationRequestValidator, Request, Id, RequestValidator, RequestValidator, RequestValidator, Request, Id (+1 more)

### Community 129 - "Response"
Cohesion: 0.33
Nodes (4): Setup, Endpoint, Response, Id

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.25
Nodes (10): Observability (OpenTelemetry), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), external, name, networks, local_shared_network, Postgres Service (mm.postgres) (+2 more)

### Community 131 - "IServiceCollection"
Cohesion: 0.09
Nodes (6): Setup, Setup, Setup, Setup, ModuleRegistry, Setup

### Community 132 - "ObjectStorageOptions"
Cohesion: 0.09
Nodes (22): ObjectStorageOptions, AccessKey, AdditionalTransientStatusCodes, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds (+14 more)

### Community 134 - "Response"
Cohesion: 0.22
Nodes (7): Setup, Response, AvailableQuantity, Description, Name, Price, Quantity

### Community 135 - "Request"
Cohesion: 0.22
Nodes (9): Request, Body, Id, RequestBody, Address, Description, Name, RequestBodyValidator (+1 more)

### Community 136 - "InterModuleRequestHandler"
Cohesion: 0.23
Nodes (6): IInterModuleRequestHandler, InterModuleRequestHandler, GetStockLevelRequest, GetStockLevelResponse, GetStockLevelRequestHandler, Endpoint

### Community 138 - "services"
Cohesion: 0.25
Nodes (8): container_name, environment, image, networks, ports, restart, services, mm.aspire-dashboard

### Community 139 - "mm.postgres"
Cohesion: 0.25
Nodes (8): container_name, environment, image, networks, ports, restart, volumes, mm.postgres

### Community 140 - "DefaultIdType"
Cohesion: 0.07
Nodes (21): Inventory.Domain.StockLevels.DomainEvents.v1, IntegrationEventHandlerBase, MaxEventAge, StatelessInboxStore, Instance, AuditLogEntry, AggregateId, AggregateType (+13 more)

### Community 141 - "Type"
Cohesion: 0.15
Nodes (5): StrictDateTimeOffsetJsonConverter, StronglyTypedIdListReadOnlyJsonConverter, StronglyTypedIdReadOnlyJsonConverter, StronglyTypedIdReadOnlyJsonConverter, StronglyTypedIdWriteOnlyJsonConverter

### Community 142 - "mm.rabbitmq"
Cohesion: 0.25
Nodes (8): container_name, environment, image, networks, ports, restart, volumes, mm.rabbitmq

### Community 143 - "Type"
Cohesion: 0.21
Nodes (3): PaginationCursor, Payload, TaggedValue

### Community 145 - "FixedWindow"
Cohesion: 0.32
Nodes (7): CustomRateLimitingOptionsValidator, FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit, FixedWindowValidator

### Community 147 - "PaginationRequest"
Cohesion: 0.25
Nodes (8): PaginationRequest, After, IncludeTotal, PageNumber, PageSize, ShouldIncludeTotal, Skip, Take

### Community 148 - "HttpWarehouseGateway"
Cohesion: 0.36
Nodes (6): IWarehouseGateway, DummyWarehouseGateway, HttpWarehouseGateway, ReleaseRequestBody, ReleaseResponseBody, Setup

### Community 150 - "IamTelemetry"
Cohesion: 0.29
Nodes (3): IamTelemetry, LoginMethods, SessionRevokedReasons

### Community 151 - "OutboxOptions"
Cohesion: 0.11
Nodes (21): OutboxCleanupSettings, BatchSize, CronSchedule, Enabled, RetentionDays, OutboxCleanupSettingsValidator, OutboxOptions, BaseBackoffSeconds (+13 more)

### Community 152 - "Request"
Cohesion: 0.29
Nodes (7): Request, Description, Name, Price, ProductTemplateId, Quantity, RequestValidator

### Community 153 - "SendForRegistration/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, Request, CaptchaToken, PhoneNumber, RequestValidator

### Community 156 - "GetDeviceSessionsRequest"
Cohesion: 0.73
Nodes (4): DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, GetDeviceSessionsRequestHandler

### Community 158 - "Request"
Cohesion: 0.33
Nodes (6): Request, Brand, Color, Model, SearchTerm, RequestValidator

### Community 159 - "Request"
Cohesion: 0.33
Nodes (6): Request, Address, Description, Name, OwnerId, RequestValidator

### Community 161 - "ObjectStorageOptions.cs"
Cohesion: 0.40
Nodes (4): ObjectStorageOptionsValidator, ObjectStorageRetryMode, Adaptive, Standard

### Community 164 - "IssueVerificationTokenRequestHandler"
Cohesion: 0.80
Nodes (3): IssueVerificationTokenRequest, IssueVerificationTokenResponse, IssueVerificationTokenRequestHandler

### Community 165 - "ApplicationUserId"
Cohesion: 0.11
Nodes (16): IBackgroundUserContext, UserId, ApplicationUserId, IsEmpty, Value, BackgroundUserContext, UserId, CurrentUser (+8 more)

### Community 166 - "Request"
Cohesion: 0.40
Nodes (5): Request, Brand, Color, Model, RequestValidator

### Community 167 - "Request"
Cohesion: 0.22
Nodes (9): Request, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name, OwnerId, SearchTerm (+1 more)

### Community 168 - "Request"
Cohesion: 0.40
Nodes (5): Request, Address, Name, SearchTerm, RequestValidator

### Community 169 - "My/AuditLog/Request.cs"
Cohesion: 0.67
Nodes (3): Products.Endpoints.Stores.v1.My.AuditLog, Request, RequestValidator

### Community 172 - "Common.Domain.Events"
Cohesion: 0.05
Nodes (32): Products.Domain.Products.DomainEvents.v1, Common.Domain.Events, Products.Application.Stores.DomainEventHandlers.v1, Products.Domain.Stores.DomainEvents.v1, DomainEventHandlerBase, IEventHandler, IEventHandlerWrapper, DomainEvent (+24 more)

### Community 173 - "Task"
Cohesion: 0.04
Nodes (13): IVariantFeatureManagerExtensions, IInboxStore, IPrivateObjectStore, IPublicObjectStore, ObjectListItem, ObjectListPage, ObjectMetadata, UploadObjectRequest (+5 more)

### Community 174 - "KeycloakTokenClient.cs"
Cohesion: 0.06
Nodes (15): Notifications.Application.Sms, IAM.Infrastructure.Keycloak.Representations, Notifications.Infrastructure.Email, Common.Application.Caching, Notifications.Infrastructure.Sms, Notifications.Infrastructure.Email.Brevo, Inventory.Infrastructure.Gateway, Notifications.Infrastructure.Telemetry (+7 more)

### Community 175 - "IInterModuleRequest"
Cohesion: 0.46
Nodes (5): IInterModuleRequest, GetUsersInRolePageRequest, GetUsersInRolePageResponse, RoleUserSummary, GetUsersInRolePageRequestHandler

### Community 176 - "KeyedRow"
Cohesion: 0.50
Nodes (4): KeyedRow, Item, Sort, Tie

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
Cohesion: 0.50
Nodes (3): Endpoint, Response, Id

### Community 184 - "Response"
Cohesion: 0.50
Nodes (3): Endpoint, Response, Id

### Community 185 - "Response"
Cohesion: 0.20
Nodes (7): Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt, Setup

### Community 187 - "ReverseProxyOptions"
Cohesion: 0.33
Nodes (5): ReverseProxyOptions, ForwardLimit, IsEnabled, TrustedNetworks, ReverseProxyOptionsValidator

### Community 188 - "BoundedCaptureStream"
Cohesion: 0.16
Nodes (6): BoundedCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 190 - "KeycloakPermissionClient"
Cohesion: 0.22
Nodes (9): KeycloakPermissionClient, DecisionRepresentation, Result, PermissionRepresentation, ResourceName, Scopes, TokenErrorRepresentation, Error (+1 more)

### Community 191 - "Request"
Cohesion: 0.67
Nodes (3): Request, PhoneNumber, RequestValidator

### Community 192 - "Request"
Cohesion: 0.67
Nodes (3): Request, SearchTerm, RequestValidator

### Community 193 - "Request"
Cohesion: 0.67
Nodes (3): Request, Id, RequestValidator

### Community 194 - "Request"
Cohesion: 0.67
Nodes (3): Request, Id, RequestValidator

### Community 195 - "Response"
Cohesion: 0.33
Nodes (4): Inventory.Endpoints.StockReservations.v1.Reserve, Endpoint, Response, Id

### Community 196 - "Request"
Cohesion: 0.67
Nodes (3): Request, Id, RequestValidator

### Community 197 - "Request"
Cohesion: 0.67
Nodes (3): Request, Id, RequestValidator

### Community 198 - "Request"
Cohesion: 0.67
Nodes (3): Request, Id, RequestValidator

### Community 199 - "Request"
Cohesion: 0.67
Nodes (3): Request, Id, RequestValidator

### Community 200 - "NotificationsTelemetry"
Cohesion: 0.14
Nodes (5): RequireFeatureFilter, RouteHandlerBuilderExtensions, InventoryTelemetry, NotificationsTelemetry, ProductsTelemetry

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

### Community 217 - "FeatureFlags"
Cohesion: 0.33
Nodes (5): Checkout, FeatureFlags, IAM, Notifications, Products

### Community 221 - "Keycloak realm as code"
Cohesion: 0.25
Nodes (7): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Secrets, Seed users (dev / test only)

### Community 234 - "KeycloakPermissionAuthorizationHandler.cs"
Cohesion: 0.10
Nodes (6): Common.Infrastructure.Auth.Services, IAM.Infrastructure.Auth, Notifications.Infrastructure.Hubs, Notifications.Application.Hubs, Common.Infrastructure.Auth, Setup

### Community 236 - "NetGsmSmsGateway"
Cohesion: 0.10
Nodes (12): SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, NetGsmSmsGateway, SendMessageBody, Msg, No (+4 more)

### Community 238 - "RequestBodyLimitFilter.cs"
Cohesion: 0.11
Nodes (10): Common.Endpoints.Webhooks, SecurityHeadersOptions, Headers, SecurityHeadersOptionsValidator, RequestBodyLimitEndpointExtensions, RequestBodyLimitMetadata, MaxBodyBytes, RequestBodyLimitMiddleware (+2 more)

### Community 241 - "ResxLocalizationOptions"
Cohesion: 0.50
Nodes (4): ResxLocalizationOptions, DefaultCulture, SupportedCultures, ResxLocalizationOptionsValidator

### Community 242 - "Response"
Cohesion: 0.15
Nodes (10): JobStatus, Failed, Queued, Running, Succeeded, Response, AccessToken, AccessTokenExpiresAt (+2 more)

### Community 243 - "RequestBody"
Cohesion: 0.16
Nodes (15): Request, Body, Id, RequestBody, ProviderReference, RequestBodyValidator, RequestValidator, Request (+7 more)

### Community 251 - "Response"
Cohesion: 0.50
Nodes (3): Endpoint, Response, Id

### Community 261 - "Response"
Cohesion: 0.33
Nodes (4): Setup, Endpoint, Response, Id

### Community 264 - "ReCaptchaResponse"
Cohesion: 0.15
Nodes (6): ReCaptchaResponse, ChallengeTs, ErrorCodes, Hostname, Score, Success

### Community 265 - "BackgroundJobsModule"
Cohesion: 0.22
Nodes (8): Cross-process call path, How it works, ICoreModule, BackgroundJobsModule, ActivitySourceNames, MeterNames, Name, StartupPriority

### Community 269 - "RouteGroupBuilder"
Cohesion: 0.04
Nodes (23): Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint (+15 more)

### Community 276 - "CustomValidator"
Cohesion: 0.07
Nodes (29): ModulesOptions, EnabledModules, ModulesOptionsValidator, CustomValidator, Request, RefreshToken, RequestValidator, Request (+21 more)

### Community 279 - "Response"
Cohesion: 0.20
Nodes (7): Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt, Setup

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

### Community 311 - "Request"
Cohesion: 0.67
Nodes (3): Request, Id, RequestValidator

### Community 314 - "JobHousekeepingOptions"
Cohesion: 0.40
Nodes (5): JobHousekeepingOptions, PageSize, RetentionHours, StaleAfterMinutes, JobHousekeepingOptionsValidator

### Community 319 - "Products/v1/Update/Request.cs"
Cohesion: 0.20
Nodes (10): Request, Body, Id, RequestBody, Description, Name, Price, Quantity (+2 more)

### Community 323 - "v1/Request.cs"
Cohesion: 0.67
Nodes (3): Request, Count, RequestValidator

### Community 324 - "Products/v1/My/Update/Request.cs"
Cohesion: 0.25
Nodes (8): Request, Body, Id, RequestBody, Description, Name, Price, Quantity

## Knowledge Gaps
- **902 isolated node(s):** `UserId`, `Id`, `IdAsString`, `Roles`, `SessionId` (+897 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1254 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **123 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `Setup.Observability.cs`, `Common.Domain.ResultMonad`, `EmailOptions`, `FixedWindow`, `Common.Domain.StronglyTypedIds`, `FullTextSearchOptions`, `CustomValidator`, `OutboxOptions`, `Setup.Logger.cs`, `ObjectStorageOptions.cs`, `fluentvalidation`, `ObservabilityOptions`, `AuditLogOptions`, `S3ObjectStoreCore.cs`, `ProjectionReconciliationOptions.cs`, `KeycloakOptions`, `ConfigureSwaggerOptions.cs`, `KeycloakTokenClient.cs`, `OtpOptions`, `CorsOptions`, `BackgroundJobsOptions`, `CaptchaOptions`, `CachingOptions`, `RabbitMqOptions`, `SmsOptions`, `JobHousekeepingOptions`, `ReverseProxyOptions`, `RequestLoggingOptions`, `.AddNotificationsSignalR`, `DevicesOptions`, `OpenApiOptions`, `PushOptions`, `Setup`, `InterModuleRequestOptions`, `ResiliencyOptions`, `KeycloakPermissionAuthorizationHandler.cs`, `InventoryOptions`, `Common.InterModuleRequests.Contracts`, `RequestBodyLimitFilter.cs`, `ResxLocalizationOptions`, `DatabaseOptions.cs`, `IdentityScheme`, `IApplicationBuilder`?**
  _High betweenness centrality (0.099) - this node is a cross-community bridge._
- **What connects `UserId`, `Id`, `IdAsString` to the rest of the system?**
  _902 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `IProductsDbContext` be split into smaller, more focused modules?**
  _Cohesion score 0.055299539170506916 - nodes in this community are weakly interconnected._
- **Why does `Result` connect `Result` to `IProductsDbContext`, `NotificationPayload`, `ApplicationUserId`, `InterModuleRequestHandler`, `.RegisterAsync`, `ICaptchaService`, `ResultTelemetryExtensions`, `Task`, `Common.Domain.Events`, `BrevoEmailGateway`, `KeycloakTokenClient`, `Task`, `VerifyPhoneOtpResponse`, `SendEmailOtpRequestHandler`, `ProductTemplate`, `DateTimeOffset`, `.GetClientKey`, `ReCaptchaService`, `.GetMeAsync`, `InventoryOptions`, `NetGsmSmsGateway`?**
  _High betweenness centrality (0.070) - this node is a cross-community bridge._
- **Should `AuditableEntityResponse` be split into smaller, more focused modules?**
  _Cohesion score 0.1111111111111111 - nodes in this community are weakly interconnected._
- **Why does `ApplicationUserId` connect `ApplicationUserId` to `IProductsDbContext`, `NotificationPayload`, `AuditableEntityResponse`, `ApplicationUserId`, `StoreId`, `IStronglyTypedId`, `DefaultIdType`, `Response`, `KeycloakPermissionAuthorizationHandler`, `Product`, `DeviceRegistration`, `IReadOnlyList`, `.RegisterAsync`, `GetDeviceSessionsRequest`, `Request`, `For`, `OutboxMessage`, `Request`, `Common.Domain.Events`, `IInterModuleRequest`, `Request`, `KeycloakTokenClient`, `Response`, `Response`, `ProductTemplate`, `DateTimeOffset`, `.TryDeserialize`, `LoggerMessage`, `.GetMeAsync`, `Response`, `Response`?**
  _High betweenness centrality (0.043) - this node is a cross-community bridge._
- **Should `ApplicationUserId` be split into smaller, more focused modules?**
  _Cohesion score 0.14772727272727273 - nodes in this community are weakly interconnected._