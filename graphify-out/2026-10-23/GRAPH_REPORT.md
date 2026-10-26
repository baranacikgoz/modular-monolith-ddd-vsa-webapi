# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-10-23)

## Corpus Check
- 586 files · ~95,090 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 3842 nodes · 10186 edges · 277 communities (165 shown, 112 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 332 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `63ef7a67`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- Result
- NotificationPayload
- OutboxOptions
- AuditableEntityResponse
- ApplicationUserId
- DomainEvent
- ApplySearchLanguageInterceptor
- IDbContext
- Common.Infrastructure.Extensions
- Error
- Common.Domain.ResultMonad
- ISearchLocalized
- ISearchLanguageResolver
- SelfRegisterByEmail/Endpoint.cs
- KeycloakPermissionAuthorizationHandler
- EmailOptions
- StoreId
- Common.Application.Options
- system_text_json_serialization
- FullTextSearchOptions
- BoundedRequestCaptureStream
- DeviceRegistration
- ISmsGateway
- .RegisterAsync
- Stores/v1/My/Update/Request.cs
- Response
- RequestResponseBodyLoggingMiddleware
- fluentvalidation
- Func
- ProjectionUpsertOutcome
- IModule
- ILogger
- Response
- Response
- OutboxMessage
- CustomRateLimitingOptions
- AdminRepresentations.cs
- ICollection
- ObservabilityOptions
- ConfigureSwaggerOptions
- IFusionCache
- S3ObjectStoreCore.cs
- HttpContext
- KeycloakOptions
- LoggerMessage
- PaginationQueryableExtensions
- Outbox Misuse Check
- Add Integration Event Command
- Response
- Type
- CaptchaOptions
- Request
- V1ProductCreatedDomainEvent
- BrevoEmailGateway
- Infrastructure/StringExtensions.cs
- Cross-Module Reference Violation
- InboxCleanupJob
- ProductTemplates/v1/Search/Request.cs
- Bogus Test Data
- KeycloakTokenClient
- StronglyTypedIdBinder
- RequestLoggingOptions
- Action
- OutboxModule
- .AddNotificationsSignalR
- ValueObject
- .PaginateAsync
- RedisFixedWindowRateLimiter
- Full-Text Search
- ValidationContextExtensions
- VerifyPhoneOtpResponse
- AsNoTracking Coverage Check
- Stores/v1/Update/Request.cs
- Response
- TokenResponseRepresentation
- KeyedResiliencePipelines
- ResiliencyOptions
- Response
- Response
- Split-Deployment PoC
- IEmailGateway
- EntityTypeBuilder
- DomainEvent
- ProductsDbContext
- PushOptions
- Configuration-Driven Module Registration
- KeycloakPermission
- .GetProductAsync
- ProductTemplates/v1/Deactivate/Request.cs
- AuditableEntity
- .MapEndpoint
- Release/Request.cs
- Response
- ILogger
- How it works
- Response
- InterModuleRequestOptions
- .AddResilientHttpClient
- KeyedResilienceProfile
- HttpContextTargetingContextAccessor
- IssueVerificationTokenRequestHandler
- ResultToResponseTransformer.cs
- Response
- Common.InterModuleRequests.Contracts
- InventoryOptions
- StockLevel
- Response
- KeycloakPermissionPolicyProvider
- VerifyEmail/Request.cs
- KeyedRow
- Response
- SkipOverlappingRecurringJobFilter
- IResxLocalizer
- Response
- HealthCheckOptions
- InterModuleRequestHandlerDefinition
- Exception
- Response
- ProductTemplateId
- docker-compose.yml (Base Stack)
- IServiceCollection
- ObjectStorageOptions
- Seeder
- InterModuleRequestHandler
- IWebHostEnvironment
- Response
- DefaultIdType
- Type
- Setup
- Response
- ActivitySource
- OutboxOptions.cs
- .EnsureNoMigrationsPending
- OtpOptions
- For
- CurrentUser
- PaginationRequest
- ProductTemplates/Setup.cs
- Notifications.Application/IAssemblyReference.cs
- Product
- Task
- system_diagnostics
- PaginationResponse
- FixedWindow
- CorsOptions
- CachingOptions
- SmsOptions
- RabbitMqOptions
- IAM.Domain
- Response
- ReverseProxyOptions
- BoundedCaptureStream
- IReadOnlyList
- JobStatus
- Response
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
- .UseInfrastructure
- WebhookCallback/Request.cs
- ResxLocalizationOptions
- Response
- RequestBody
- OpenApiOptions
- microsoft_entityframeworkcore
- Response
- Products.Domain/IAssemblyReference.cs
- IAM.Application/IAssemblyReference.cs
- Products.Infrastructure/IAssemblyReference.cs
- Products.Application/IAssemblyReference.cs
- Common.Infrastructure/IAssemblyReference.cs
- Notifications.Domain/IAssemblyReference.cs
- IAM.Infrastructure/IAssemblyReference.cs
- SendForRegistration/Request.cs
- Response
- ReCaptchaResponse
- BackgroundJobsModule
- EmailOtpDispatchOutcome
- RouteGroupBuilder
- Request
- Response
- SendForEmail/Request.cs
- Request
- Request
- RequestBodyLimitFilter.cs
- AuditLogOptions
- ProjectionReconciliationOptions.cs
- ResultTelemetryExtensions
- SignalROptions
- microsoft_aspnetcore_mvc
- JobHousekeepingOptions
- Products.Endpoints
- Products/v1/Update/Request.cs
- Notifications.Infrastructure
- .VerifySha256
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
1. `Result` - 147 edges
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
- `Configuration` --references--> `FullTextSearchOptions`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Application/Options/FullTextSearchOptions.cs
- `Full-Text Search` --references--> `FullTextSearchOptions`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Application/Options/FullTextSearchOptions.cs
- `Gotchas` --references--> `FullTextSearchOptions`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Application/Options/FullTextSearchOptions.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Local Infrastructure Stack** — docker_compose_postgres, docker_compose_rabbitmq, docker_compose_redis, docker_compose_aspire_dashboard [EXTRACTED 1.00]

## Communities (277 total, 112 thin omitted)

### Community 0 - "Result"
Cohesion: 0.04
Nodes (25): ICurrentUser, Id, IdAsString, Roles, SessionId, CollectionExtensions, FeatureFlagResultExtensions, Result (+17 more)

### Community 1 - "NotificationPayload"
Cohesion: 0.35
Nodes (4): INotificationDispatcher, INotificationsClient, NotificationPayload, SignalRNotificationDispatcher

### Community 2 - "OutboxOptions"
Cohesion: 0.09
Nodes (19): OutboxOptions, BaseBackoffSeconds, BatchSize, ClaimLeaseSeconds, Cleanup, IsProcessor, LagThresholdMinutes, MaxBackoffSeconds (+11 more)

### Community 3 - "AuditableEntityResponse"
Cohesion: 0.06
Nodes (28): Products.Endpoints.Stores.v1.Search, Products.Endpoints.Products.v1.My.Get, Products.Endpoints.ProductTemplates.v1.Search, Common.Application.DTOs, Products.Endpoints.Products.v1.Get, AuditableEntityResponse, CreatedBy, CreatedOn (+20 more)

### Community 5 - "ApplicationUserId"
Cohesion: 0.08
Nodes (19): ApplicationUserId, IsEmpty, Value, HttpClientKeyedExtensions, KeycloakUser, KeycloakUserPage, KeycloakUserSession, KeycloakAdminClient (+11 more)

### Community 6 - "DomainEvent"
Cohesion: 0.08
Nodes (23): Products.Application.Stores.DomainEventHandlers.v1, Products.Domain.Stores.DomainEvents.v1, DomainEvent, CreatedOn, Id, Version, SimulateSomeBusinessHandler, StoreCreatedIntegrationEventPublishingHandler (+15 more)

### Community 7 - "ApplySearchLanguageInterceptor"
Cohesion: 0.31
Nodes (3): ApplyAuditingInterceptor, ApplySearchLanguageInterceptor, Setup

### Community 8 - "IDbContext"
Cohesion: 0.22
Nodes (5): JobHousekeepingJob, IDbContext, AuditLog, ChangeTracker, Database

### Community 9 - "Common.Infrastructure.Extensions"
Cohesion: 0.14
Nodes (10): Common.Infrastructure.RateLimiting, Products.Infrastructure.RateLimiting, Common.Infrastructure.Extensions, IAM.Endpoints.Otp.VersionNeutral, IAM.Infrastructure.RateLimiting, IAM.Infrastructure.Captcha, IAM.Endpoints.Tokens.VersionNeutral, Endpoint (+2 more)

### Community 10 - "Error"
Cohesion: 0.05
Nodes (28): StringLocalizerExtensions, StringExtensions, Error, Key, ParameterName, StatusCode, SubErrors, Value (+20 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.07
Nodes (32): IAM.Endpoints.Captcha.VersionNeutral, Products.Infrastructure.InterModuleRequestHandlers, Common.Application.Search, Inventory.Infrastructure.InterModuleRequestHandlers, Inventory.Application.Persistence, Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions (+24 more)

### Community 12 - "ISearchLocalized"
Cohesion: 0.25
Nodes (7): Add a new language/culture, Add search to a new entity _(Build checklist)_, Change the vector (weights, fields, or config), Extending and maintaining, File map _(Build)_, ISearchLocalized, Language

### Community 13 - "ISearchLanguageResolver"
Cohesion: 0.24
Nodes (6): Configuration, ISearchLanguageResolver, UniversalConfig, SearchLanguageResolver, UniversalConfig, Setup

### Community 14 - "SelfRegisterByEmail/Endpoint.cs"
Cohesion: 0.17
Nodes (8): Common.Application.FeatureManagement, IAM.Domain.Captcha, IAM.Application.Captcha.Services, IAM.Endpoints.Tokens, IAM.Endpoints.Users.VersionNeutral, IAM.Endpoints.Common, Constants, Endpoint

### Community 15 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.29
Nodes (3): KeycloakPermissionAuthorizationHandler, KeycloakPermissionRequirement, Permission

### Community 16 - "EmailOptions"
Cohesion: 0.10
Nodes (23): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+15 more)

### Community 17 - "StoreId"
Cohesion: 0.11
Nodes (15): AggregateRoot, Events, Id, Version, Store, Address, Description, Language (+7 more)

### Community 18 - "Common.Application.Options"
Cohesion: 0.04
Nodes (34): Common.Infrastructure.Modules, Common.InterModuleRequests, Notifications.Infrastructure.Devices, Inventory.Endpoints, Products.Endpoints.Stores, Common.Endpoints.Versioning, Notifications.Infrastructure.Email, Products.Endpoints.Probe (+26 more)

### Community 19 - "system_text_json_serialization"
Cohesion: 0.19
Nodes (4): Outbox.Persistence, Common.Application.Persistence.Outbox, Common.Application.JsonConverters, Common.Infrastructure.Persistence.ValueConverters

### Community 20 - "FullTextSearchOptions"
Cohesion: 0.29
Nodes (7): Gotchas, FullTextSearchOptions, CultureToConfig, DefaultConfig, RankWeights, UseUnaccent, FullTextSearchOptionsValidator

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.14
Nodes (6): BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 22 - "DeviceRegistration"
Cohesion: 0.05
Nodes (38): Notifications.Infrastructure.Persistence.EntityConfigurations, Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FirstName (+30 more)

### Community 23 - "ISmsGateway"
Cohesion: 0.19
Nodes (8): ISmsGateway, SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, DummySmsGateway, Setup, ThrottledSmsGateway

### Community 24 - ".RegisterAsync"
Cohesion: 0.07
Nodes (25): IInterModuleRequest, IInterModuleRequestClient, MassTransitInterModuleRequestClient, BindDeviceSessionRequest, BindDeviceSessionResponse, DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse, SendPhoneOtpRequest (+17 more)

### Community 25 - "Stores/v1/My/Update/Request.cs"
Cohesion: 0.33
Nodes (6): Products.Endpoints.Stores.v1.My.Update, Request, Address, Description, Name, RequestValidator

### Community 26 - "Response"
Cohesion: 0.29
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, Endpoint, Response, EmailVerificationToken, IsRegistered

### Community 28 - "fluentvalidation"
Cohesion: 0.10
Nodes (23): IAM.Domain.Users, IAM.Endpoints.Otp.VersionNeutral.SendForLogin, Common.Domain.Extensions, Notifications.Infrastructure.Devices.UpdateCurrentPushToken, Common.Application.Validation, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, Common.Domain.Devices, IAM.Endpoints.Common.Validations (+15 more)

### Community 29 - "Func"
Cohesion: 0.11
Nodes (17): SyncExtensions, CheckRegistrationRateLimitingPolicy, OnRejected, EmailRateLimitingPolicy, OnRejected, OtpVerifyRateLimitingPolicy, OnRejected, RegisterRateLimitingPolicy (+9 more)

### Community 30 - "ProjectionUpsertOutcome"
Cohesion: 0.20
Nodes (7): Common.Application.Persistence.Projections, ProjectionEntity, SourceVersion, ProjectionUpsertOutcome, Inserted, Stale, Updated

### Community 31 - "IModule"
Cohesion: 0.06
Nodes (30): Setup, IModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority, IamModule (+22 more)

### Community 32 - "ILogger"
Cohesion: 0.11
Nodes (8): ProjectionReconciliationJob, AuditLogRetentionJobRegistrar, AuditLogRetentionService, Setup, IPushGateway, DummyPushGateway, FirebasePushGateway, InboxCleanupJob

### Community 33 - "Response"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.Refresh, Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 34 - "Response"
Cohesion: 0.33
Nodes (4): Products.Endpoints.Stores.v1.My.AddProduct, Endpoint, Response, Id

### Community 35 - "OutboxMessage"
Cohesion: 0.06
Nodes (26): IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, OutboxMessage, CreatedOn (+18 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.09
Nodes (12): CustomRateLimitingOptions, CheckRegistration, CreateStore, Email, ExemptPathPrefixes, Global, OtpVerify, Register (+4 more)

### Community 37 - "AdminRepresentations.cs"
Cohesion: 0.12
Nodes (16): CredentialRepresentation, Temporary, Type, Value, ErrorRepresentation, Error, ErrorMessage, Field (+8 more)

### Community 38 - "ICollection"
Cohesion: 0.16
Nodes (8): Inventory.Endpoints.StockReservations.v1.ReserveSeries, ProblemDetailsExtensions, GetUsersInRolePageRequest, GetUsersInRolePageResponse, RoleUserSummary, GetUsersInRolePageRequestHandler, Response, Ids

### Community 39 - "ObservabilityOptions"
Cohesion: 0.09
Nodes (17): ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics, EnableTracing, LogSink, MinimumLevel (+9 more)

### Community 40 - "ConfigureSwaggerOptions"
Cohesion: 0.06
Nodes (11): Host, Host.Configurations, Host.Swagger, Setup, Program, ConfigureSwaggerOptions, DefaultResponsesOperationFilter, RemoveDefaultResponseSchemaFilter (+3 more)

### Community 41 - "IFusionCache"
Cohesion: 0.15
Nodes (10): IOtpService, OtpVerificationOutcome, InvalidOtp, Success, TooManyAttempts, OtpCodeGenerator, OtpService, OtpServiceBase (+2 more)

### Community 43 - "HttpContext"
Cohesion: 0.20
Nodes (5): RequestBodyLimitEndpointExtensions, RequestBodyLimitMetadata, MaxBodyBytes, HttpContextExtensions, RateLimitPartitions

### Community 44 - "KeycloakOptions"
Cohesion: 0.12
Nodes (15): KeycloakOptions, AttemptTimeoutSeconds, Authority, BaseUrl, DecisionCacheMaxDurationSeconds, Realm, RequireHttpsMetadata, ResourceClientId (+7 more)

### Community 45 - "LoggerMessage"
Cohesion: 0.11
Nodes (8): Setup, ProductCreatedIntegrationEvent, CreateStockLevelOnProductCreatedHandler, LogProductCatalogChangeHandler, Setup, StockReservationExpirySweepJobRegistrar, StockReservationExpirySweepService, DeviceRegistryReconcileJobRegistrar

### Community 46 - "PaginationQueryableExtensions"
Cohesion: 0.17
Nodes (4): CursorBound, Value, PaginationQueryableExtensions, ParameterReplacer

### Community 50 - "Response"
Cohesion: 0.14
Nodes (12): IAM.Endpoints.Users.VersionNeutral.Search, Endpoint, Response, BirthDate, CreatedOn, Email, Enabled, FirstName (+4 more)

### Community 51 - "Type"
Cohesion: 0.22
Nodes (4): UnknownDomainEvent, PolymorphicEventConverter, InterModuleRequestHandlerDefinition, RequestType

### Community 52 - "CaptchaOptions"
Cohesion: 0.16
Nodes (13): CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey, Provider, ScoreThreshold, SecretKey (+5 more)

### Community 53 - "Request"
Cohesion: 0.33
Nodes (6): Request, Address, Description, Name, OwnerId, RequestValidator

### Community 54 - "V1ProductCreatedDomainEvent"
Cohesion: 0.22
Nodes (6): Products.Application.Products.DomainEventHandlers.v1, IIntegrationEventOutbox, Setup, ProductCreatedIntegrationEventPublishingHandler, V1ProductCreatedDomainEventHandlers, V1ProductCreatedDomainEvent

### Community 55 - "BrevoEmailGateway"
Cohesion: 0.10
Nodes (15): BrevoEmailGateway, SendContact, Email, Name, SendErrorBody, Code, Message, SendRequestBody (+7 more)

### Community 58 - "InboxCleanupJob"
Cohesion: 0.14
Nodes (7): IDatabaseSeeder, Priority, OutboxCleanupJob, OutboxMetricsJob, ProductsDatabaseSeeder, Priority, Setup

### Community 59 - "ProductTemplates/v1/Search/Request.cs"
Cohesion: 0.25
Nodes (7): Constants, Request, Brand, Color, Model, SearchTerm, RequestValidator

### Community 61 - "KeycloakTokenClient"
Cohesion: 0.18
Nodes (6): SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, KeycloakTokenClient, SendSecurityAlertRequestHandler

### Community 63 - "RequestLoggingOptions"
Cohesion: 0.07
Nodes (23): BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount, BackgroundJobsOptionsValidator, RequestLoggingOptions (+15 more)

### Community 65 - "Action"
Cohesion: 0.16
Nodes (4): IBackgroundJobs, IRecurringBackgroundJobs, BackgroundJobsService, RecurringBackgroundJobsService

### Community 66 - "OutboxModule"
Cohesion: 0.22
Nodes (5): OutboxModule, ActivitySourceNames, MeterNames, Name, StartupPriority

### Community 69 - ".PaginateAsync"
Cohesion: 0.25
Nodes (3): PaginationCursor, Payload, TaggedValue

### Community 70 - "RedisFixedWindowRateLimiter"
Cohesion: 0.26
Nodes (5): FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration

### Community 71 - "Full-Text Search"
Cohesion: 0.33
Nodes (6): Full-Text Search, Non-goals, Per-entity strategy, Testing, The problem, TL;DR: what the feature gives you

### Community 73 - "VerifyPhoneOtpResponse"
Cohesion: 0.24
Nodes (8): OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, VerifyPhoneOtpResponseExtensions, VerifyPhoneOtpRequestHandler

### Community 75 - "Stores/v1/Update/Request.cs"
Cohesion: 0.22
Nodes (10): Products.Endpoints.Stores.v1.Update, Request, Body, Id, RequestBody, Address, Description, Name (+2 more)

### Community 76 - "Response"
Cohesion: 0.25
Nodes (7): Endpoint, Response, Address, Description, Name, OwnerId, ProductCount

### Community 77 - "TokenResponseRepresentation"
Cohesion: 0.13
Nodes (14): DecisionRepresentation, Result, PermissionRepresentation, ResourceName, Scopes, TokenErrorRepresentation, Error, ErrorDescription (+6 more)

### Community 79 - "ResiliencyOptions"
Cohesion: 0.18
Nodes (11): ResiliencyOptions, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, Keyed, MaxRetryAttempts (+3 more)

### Community 80 - "Response"
Cohesion: 0.25
Nodes (6): Products.Endpoints.ProductTemplates.v1.Get, Endpoint, Response, Brand, Color, Model

### Community 81 - "Response"
Cohesion: 0.25
Nodes (7): Endpoint, Response, Address, Description, Name, OwnerId, ProductCount

### Community 82 - "Split-Deployment PoC"
Cohesion: 0.25
Nodes (6): Concurrent safety, Files added by this PoC, How to run, Split-Deployment PoC, What this proves, README (Boilerplate Overview)

### Community 83 - "IEmailGateway"
Cohesion: 0.36
Nodes (4): IEmailGateway, DummyEmailGateway, Setup, ThrottledEmailGateway

### Community 84 - "EntityTypeBuilder"
Cohesion: 0.11
Nodes (13): ProcessedMessage, ConsumerName, MessageId, ProcessedOn, AuditableEntityConfiguration, AuditLogEntryConfiguration, JobRowConfiguration, TrigramIndexExtensions (+5 more)

### Community 85 - "DomainEvent"
Cohesion: 0.05
Nodes (29): Inventory.Domain.StockReservations.DomainEvents.v1, JobRow, FailureKey, FinishedOn, Progress, QueuedOn, RequestedBy, StartedOn (+21 more)

### Community 86 - "ProductsDbContext"
Cohesion: 0.06
Nodes (27): IdBound, Value, JobClaimExtensions, ProjectionUpsertExtensions, EventDispatcher, IntegrationEventOutbox, HasPending, BaseDbContext (+19 more)

### Community 87 - "PushOptions"
Cohesion: 0.12
Nodes (20): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri, Type (+12 more)

### Community 90 - ".GetProductAsync"
Cohesion: 0.54
Nodes (4): GetStockLevelRequest, GetStockLevelResponse, GetStockLevelRequestHandler, Endpoint

### Community 91 - "ProductTemplates/v1/Deactivate/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.ProductTemplates.v1.Deactivate, Request, Id, RequestValidator

### Community 92 - "AuditableEntity"
Cohesion: 0.10
Nodes (17): IAggregateRoot, Events, Id, Version, AuditableEntity, AuditableEntity, Id, CreatedBy (+9 more)

### Community 93 - ".MapEndpoint"
Cohesion: 0.29
Nodes (4): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, Response, ClientKey, Setup

### Community 94 - "Release/Request.cs"
Cohesion: 0.50
Nodes (4): Inventory.Endpoints.StockReservations.v1.Release, Request, Id, RequestValidator

### Community 95 - "Response"
Cohesion: 0.22
Nodes (7): Products.Endpoints.Products.v1.My.Search, Endpoint, Response, Description, Name, Price, Quantity

### Community 96 - "ILogger"
Cohesion: 0.17
Nodes (3): DatabaseSeederOrchestrator, SeedingCompletionTracker, ReCaptchaService

### Community 97 - "How it works"
Cohesion: 0.40
Nodes (5): How it works, One-time setup _(Build, in a migration)_, Query path, Ranking, Write path

### Community 98 - "Response"
Cohesion: 0.22
Nodes (7): Products.Endpoints.Products.v1.Search, Endpoint, Response, Description, Name, Price, Quantity

### Community 99 - "InterModuleRequestOptions"
Cohesion: 0.25
Nodes (7): InterModuleRequestOptions, DependencyUnavailableRetryAfterSeconds, HandlerConcurrentMessageLimit, HandlerPrefetchCount, Timeouts, TimeoutSeconds, InterModuleRequestOptionsValidator

### Community 101 - "KeyedResilienceProfile"
Cohesion: 0.15
Nodes (12): KeyedResilienceProfile, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, RateLimitFailOpen, RateLimitPermits (+4 more)

### Community 103 - "IssueVerificationTokenRequestHandler"
Cohesion: 0.80
Nodes (3): IssueVerificationTokenRequest, IssueVerificationTokenResponse, IssueVerificationTokenRequestHandler

### Community 104 - "ResultToResponseTransformer.cs"
Cohesion: 0.11
Nodes (9): Common.Application.EndpointFilters, ProblemResponse, ResultToAcceptedResponseTransformer, ResultToCreatedResponseTransformer, ResultToResponseTransformer, ResultToResponseTransformer, RouteHandlerBuilderExtensions, RouteHandlerBuilderExtensions (+1 more)

### Community 105 - "Response"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegisterByEmail, Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 106 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.06
Nodes (23): Notifications.Application.Otp, Notifications.Application.Sms, IAM.Endpoints.Users, IAM.Infrastructure.Keycloak.Representations, Common.Application.Caching, Notifications.Infrastructure.Email.Brevo, IAM.Endpoints.Otp, Notifications.Application.Persistence (+15 more)

### Community 107 - "InventoryOptions"
Cohesion: 0.13
Nodes (16): InventoryOptions, MaxOccurrencesPerSeries, SafetyBufferQuantity, StockReservationExpirySweepBatchSize, StockReservationExpirySweepCron, WarehouseGatewayBaseUrl, WarehouseGatewayProvider, WebhookMaxBodyBytes (+8 more)

### Community 108 - "StockLevel"
Cohesion: 0.12
Nodes (8): Inventory.Domain.StockLevels.DomainEvents.v1, StronglyTypedIdHelper, V1StockLevelCreatedDomainEvent, StockLevel, ProductId, QuantityOnHand, StockLevelId, StockLevelConfiguration

### Community 109 - "Response"
Cohesion: 0.33
Nodes (4): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, Endpoint, Response, IsRegistered

### Community 111 - "VerifyEmail/Request.cs"
Cohesion: 0.50
Nodes (4): Request, Email, Otp, RequestValidator

### Community 112 - "KeyedRow"
Cohesion: 0.50
Nodes (4): KeyedRow, Item, Sort, Tie

### Community 114 - "Response"
Cohesion: 0.13
Nodes (13): IAM.Endpoints.Users.VersionNeutral.Me.Get, Endpoint, Response, BirthDate, CreatedOn, Email, FirstName, Id (+5 more)

### Community 117 - "Response"
Cohesion: 0.11
Nodes (15): Inventory.Endpoints.StockReservations.v1.Get, ReservationStatus, Active, Committed, Expired, Released, RequiresReconciliation, Endpoint (+7 more)

### Community 124 - "HealthCheckOptions"
Cohesion: 0.40
Nodes (5): HealthCheckOptions, EnableHealthChecks, ReadinessTimeoutInSeconds, StartupTimeoutInSeconds, HealthCheckOptionsValidator

### Community 126 - "Exception"
Cohesion: 0.18
Nodes (3): GlobalExceptionHandlingMiddleware, Endpoint, NotificationsHub

### Community 127 - "Response"
Cohesion: 0.25
Nodes (7): Endpoint, Response, Address, Description, Name, OwnerId, ProductCount

### Community 129 - "ProductTemplateId"
Cohesion: 0.10
Nodes (17): Products.Endpoints.ProductTemplates.v1.Create, ProductTemplate, Brand, Color, IsActive, Model, Products, ProductTemplateId (+9 more)

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.48
Nodes (7): Observability (OpenTelemetry), docker-compose.yml (Base Stack), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), Postgres Service (mm.postgres), RabbitMQ Service (mm.rabbitmq), Redis Service (mm.redis)

### Community 131 - "IServiceCollection"
Cohesion: 0.10
Nodes (6): Setup, Setup, Setup, Setup, Setup, Setup

### Community 132 - "ObjectStorageOptions"
Cohesion: 0.05
Nodes (29): ObjectStorageOptions, AccessKey, AdditionalTransientStatusCodes, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds (+21 more)

### Community 135 - "Seeder"
Cohesion: 0.23
Nodes (4): GetSeedUserIdsRequest, GetSeedUserIdsResponse, GetSeedUserIdsRequestHandler, Seeder

### Community 136 - "InterModuleRequestHandler"
Cohesion: 0.15
Nodes (10): IInterModuleRequestHandler, InterModuleRequestHandler, DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, GetProductRequest, GetProductResponse, Endpoint (+2 more)

### Community 138 - "Response"
Cohesion: 0.33
Nodes (4): Products.Endpoints.Stores.v1.AddProduct, Endpoint, Response, Id

### Community 140 - "DefaultIdType"
Cohesion: 0.06
Nodes (30): IntegrationEventHandlerBase, MaxEventAge, IInboxCleanupTarget, ModuleName, IInboxStore, StatelessInboxStore, Instance, AuditLogEntry (+22 more)

### Community 141 - "Type"
Cohesion: 0.14
Nodes (7): StrictDateTimeOffsetJsonConverter, StronglyTypedIdListReadOnlyJsonConverter, StronglyTypedIdReadOnlyJsonConverter, StronglyTypedIdReadOnlyJsonConverter, StronglyTypedIdWriteOnlyJsonConverter, IStronglyTypedId, Value

### Community 143 - "Setup"
Cohesion: 0.11
Nodes (5): ModulesOptions, EnabledModules, ModulesOptionsValidator, Setup, ModuleRegistry

### Community 144 - "Response"
Cohesion: 0.14
Nodes (12): IAM.Endpoints.Users.VersionNeutral.Get, Endpoint, Response, BirthDate, CreatedOn, Email, Enabled, FirstName (+4 more)

### Community 146 - "ActivitySource"
Cohesion: 0.27
Nodes (6): RequireFeatureFilter, BackgroundJobsTelemetry, IamTelemetry, InventoryTelemetry, OutboxTelemetry, ProductsTelemetry

### Community 151 - "OutboxOptions.cs"
Cohesion: 0.32
Nodes (7): OutboxCleanupSettings, BatchSize, CronSchedule, Enabled, RetentionDays, OutboxCleanupSettingsValidator, OutboxOptionsValidator

### Community 152 - ".EnsureNoMigrationsPending"
Cohesion: 0.39
Nodes (3): AutoMigrateMarker, IAutoMigrateMarker, MigrationGuard

### Community 158 - "OtpOptions"
Cohesion: 0.18
Nodes (11): OtpOptions, DummyCode, EmailQuotaWindowMinutes, ExpirationInMinutes, Length, MaxSendsPerEmailPerWindow, MaxSendsPerPhonePerWindow, PhoneQuotaWindowMinutes (+3 more)

### Community 165 - "CurrentUser"
Cohesion: 0.15
Nodes (13): IBackgroundUserContext, UserId, BackgroundUserContext, UserId, CurrentUser, BackgroundUserIdAsString, Id, IdAsString (+5 more)

### Community 167 - "PaginationRequest"
Cohesion: 0.04
Nodes (47): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Stores.v1.My.AuditLog, Products.Endpoints.Products.v1.AuditLog, PaginationRequest, After, IncludeTotal, PageNumber, PageSize (+39 more)

### Community 168 - "ProductTemplates/Setup.cs"
Cohesion: 0.40
Nodes (3): Products.Endpoints.ProductTemplates, Setup, Endpoint

### Community 172 - "Product"
Cohesion: 0.08
Nodes (23): Products.Domain.Products.DomainEvents.v1, Common.Domain.Events, Common.Domain.Aggregates, DomainEventHandlerBase, IEventHandler, IEventHandlerWrapper, V1ProductDescriptionUpdatedDomainEvent, V1ProductNameUpdatedDomainEvent (+15 more)

### Community 173 - "Task"
Cohesion: 0.04
Nodes (17): OtpCacheEntry, IVariantFeatureManagerExtensions, IInboxStore, IPrivateObjectStore, IPublicObjectStore, ObjectListItem, ObjectListPage, ObjectMetadata (+9 more)

### Community 174 - "system_diagnostics"
Cohesion: 0.08
Nodes (5): Notifications.Application.Push, BackgroundJobs.Telemetry, Notifications.Infrastructure.Push.Firebase, LoginMethods, SessionRevokedReasons

### Community 175 - "PaginationResponse"
Cohesion: 0.10
Nodes (13): AuditLogDto, PaginationResponse, HasNext, HasPrevious, HasTotal, NextPageNumber, PreviousPageNumber, TotalPages (+5 more)

### Community 176 - "FixedWindow"
Cohesion: 0.32
Nodes (7): CustomRateLimitingOptionsValidator, FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit, FixedWindowValidator

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

### Community 185 - "Response"
Cohesion: 0.20
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegister, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt, Setup

### Community 187 - "ReverseProxyOptions"
Cohesion: 0.33
Nodes (5): ReverseProxyOptions, ForwardLimit, IsEnabled, TrustedNetworks, ReverseProxyOptionsValidator

### Community 188 - "BoundedCaptureStream"
Cohesion: 0.14
Nodes (7): RetryAfterHeaderExtensions, BoundedCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 190 - "IReadOnlyList"
Cohesion: 0.08
Nodes (12): GetActiveSessionIdsRequest, GetActiveSessionIdsResponse, UserSessionIds, IKeycloakPermissionClient, IServiceAccountTokenProvider, GrantedPermission, GetActiveSessionIdsRequestHandler, KeycloakPaths (+4 more)

### Community 194 - "JobStatus"
Cohesion: 0.33
Nodes (5): JobStatus, Failed, Queued, Running, Succeeded

### Community 195 - "Response"
Cohesion: 0.33
Nodes (4): Inventory.Endpoints.StockReservations.v1.Reserve, Endpoint, Response, Id

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
Cohesion: 0.09
Nodes (6): Common.Infrastructure.Auth.Services, IAM.Infrastructure.Auth, Notifications.Infrastructure.Hubs, Notifications.Application.Hubs, Common.Infrastructure.Auth, NotificationGroupName

### Community 236 - "NetGsmSmsGateway"
Cohesion: 0.10
Nodes (14): NetGsmSmsGateway, SendMessageBody, Msg, No, SendRequestBody, AppName, Encoding, IysFilter (+6 more)

### Community 238 - ".UseInfrastructure"
Cohesion: 0.24
Nodes (3): RequestBodyLimitMiddleware, EnrichLogsWithUserInfoMiddleware, SecurityHeadersMiddleware

### Community 240 - "WebhookCallback/Request.cs"
Cohesion: 0.40
Nodes (5): Inventory.Endpoints.StockReservations.v1.WebhookCallback, Request, ProviderReference, ReservationId, RequestValidator

### Community 241 - "ResxLocalizationOptions"
Cohesion: 0.50
Nodes (4): ResxLocalizationOptions, DefaultCulture, SupportedCultures, ResxLocalizationOptionsValidator

### Community 242 - "Response"
Cohesion: 0.29
Nodes (6): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 243 - "RequestBody"
Cohesion: 0.08
Nodes (33): Inventory.Endpoints.StockReservations.v1.Commit, CustomValidator, Request, RefreshToken, RequestValidator, Request, Id, RequestValidator (+25 more)

### Community 247 - "OpenApiOptions"
Cohesion: 0.22
Nodes (9): OpenApiOptions, ContactEmail, ContactName, Description, EnableSwagger, LicenseName, LicenseUrl, Title (+1 more)

### Community 250 - "microsoft_entityframeworkcore"
Cohesion: 0.06
Nodes (19): Common.Infrastructure.Persistence.Inbox, Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Products.Infrastructure.Persistence.Seeding, Common.Application.Persistence.Inbox, Common.Domain.StronglyTypedIds, Common.Application.Persistence, Inventory.Infrastructure.Persistence.EntityConfigurations (+11 more)

### Community 251 - "Response"
Cohesion: 0.33
Nodes (4): Products.Endpoints.Stores.v1.My.Create, Endpoint, Response, Id

### Community 260 - "SendForRegistration/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, Request, CaptchaToken, PhoneNumber, RequestValidator

### Community 261 - "Response"
Cohesion: 0.29
Nodes (4): Products.Endpoints.Stores.v1.Create, Setup, Response, Id

### Community 264 - "ReCaptchaResponse"
Cohesion: 0.15
Nodes (6): ReCaptchaResponse, ChallengeTs, ErrorCodes, Hostname, Score, Success

### Community 265 - "BackgroundJobsModule"
Cohesion: 0.22
Nodes (8): Cross-process call path, How it works, ICoreModule, BackgroundJobsModule, ActivitySourceNames, MeterNames, Name, StartupPriority

### Community 267 - "EmailOtpDispatchOutcome"
Cohesion: 0.22
Nodes (8): EmailOtpDispatchErrors, EmailOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, SendEmailOtpRequest, SendEmailOtpResponse, SendEmailOtpRequestHandler

### Community 269 - "RouteGroupBuilder"
Cohesion: 0.04
Nodes (23): Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Setup, Endpoint (+15 more)

### Community 276 - "Request"
Cohesion: 0.29
Nodes (7): Request, Description, Name, Price, ProductTemplateId, Quantity, RequestValidator

### Community 279 - "Response"
Cohesion: 0.12
Nodes (14): IAM.Endpoints.Tokens.VersionNeutral.Create, IdentityScheme, Email, PhoneNumber, IdentitySchemeOptions, Scheme, IdentitySchemeOptionsValidator, Setup (+6 more)

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
Cohesion: 0.29
Nodes (6): AuditLogOptions, PerSchemaRetentionDays, PurgeBatchSize, RetentionCron, RetentionDays, AuditLogOptionsValidator

### Community 299 - "ProjectionReconciliationOptions.cs"
Cohesion: 0.50
Nodes (4): ProjectionReconciliationOptions, MaxPages, PageSize, ProjectionReconciliationOptionsValidator

### Community 307 - "SignalROptions"
Cohesion: 0.40
Nodes (5): SignalROptions, RedisConnectionString, RequireRedisBackplaneInProduction, UseRedisBackplane, SignalROptionsValidator

### Community 311 - "microsoft_aspnetcore_mvc"
Cohesion: 0.06
Nodes (36): Products.Endpoints.Stores.v1.Deactivate, Products.Endpoints.Stores.v1.My.RemoveProduct, Common.Application.ModelBinders, Products.Endpoints.ProductTemplates.v1.Activate, Products.Endpoints.Stores.v1.RemoveProduct, Request, Id, RequestValidator (+28 more)

### Community 314 - "JobHousekeepingOptions"
Cohesion: 0.40
Nodes (5): JobHousekeepingOptions, PageSize, RetentionHours, StaleAfterMinutes, JobHousekeepingOptionsValidator

### Community 319 - "Products/v1/Update/Request.cs"
Cohesion: 0.20
Nodes (11): Products.Endpoints.Products.v1.Update, Request, Body, Id, RequestBody, Description, Name, Price (+3 more)

### Community 323 - "v1/Request.cs"
Cohesion: 0.17
Nodes (10): Products.Endpoints.Probe.v1, DatabaseOptions, ConnectionString, DatabaseOptionsValidator, SecurityHeadersOptions, Headers, SecurityHeadersOptionsValidator, Request (+2 more)

### Community 324 - "Products/v1/My/Update/Request.cs"
Cohesion: 0.20
Nodes (11): Products.Endpoints.Products.v1.My.Update, Request, Body, Id, RequestBody, Description, Name, Price (+3 more)

## Knowledge Gaps
- **901 isolated node(s):** `UserId`, `Id`, `IdAsString`, `Roles`, `SessionId` (+896 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1205 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **112 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `ObjectStorageOptions`, `Common.Infrastructure.Extensions`, `Common.Domain.ResultMonad`, `ISearchLanguageResolver`, `SelfRegisterByEmail/Endpoint.cs`, `Setup`, `EmailOptions`, `FullTextSearchOptions`, `Response`, `OutboxOptions.cs`, `Setup.Logger.cs`, `fluentvalidation`, `OtpOptions`, `ObservabilityOptions`, `AuditLogOptions`, `ConfigureSwaggerOptions`, `S3ObjectStoreCore.cs`, `ProjectionReconciliationOptions.cs`, `KeycloakOptions`, `system_diagnostics`, `FixedWindow`, `CorsOptions`, `CachingOptions`, `CaptchaOptions`, `RabbitMqOptions`, `SignalROptions`, `SmsOptions`, `JobHousekeepingOptions`, `RequestLoggingOptions`, `v1/Request.cs`, `DevicesOptions`, `PushOptions`, `InterModuleRequestOptions`, `KeyedResilienceProfile`, `KeycloakPermissionAuthorizationHandler.cs`, `InventoryOptions`, `Common.InterModuleRequests.Contracts`, `ResxLocalizationOptions`, `OpenApiOptions`, `microsoft_entityframeworkcore`, `HealthCheckOptions`?**
  _High betweenness centrality (0.084) - this node is a cross-community bridge._
- **What connects `UserId`, `Id`, `IdAsString` to the rest of the system?**
  _901 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Result` be split into smaller, more focused modules?**
  _Cohesion score 0.04468845760980592 - nodes in this community are weakly interconnected._
- **Why does `ApplicationUserId` connect `ApplicationUserId` to `Result`, `AuditableEntityResponse`, `DomainEvent`, `Seeder`, `InterModuleRequestHandler`, `DefaultIdType`, `Type`, `KeycloakPermissionAuthorizationHandler`, `Response`, `StoreId`, `DeviceRegistration`, `.RegisterAsync`, `For`, `CurrentUser`, `ICollection`, `PaginationRequest`, `Task`, `PaginationResponse`, `Response`, `Request`, `microsoft_aspnetcore_mvc`, `KeycloakTokenClient`, `IReadOnlyList`, `Response`, `Response`, `EntityTypeBuilder`, `DomainEvent`, `AuditableEntity`, `StockLevel`, `Response`, `Response`?**
  _High betweenness centrality (0.058) - this node is a cross-community bridge._
- **Should `OutboxOptions` be split into smaller, more focused modules?**
  _Cohesion score 0.08522727272727272 - nodes in this community are weakly interconnected._
- **Why does `Result` connect `Result` to `ProductTemplateId`, `ApplicationUserId`, `InterModuleRequestHandler`, `Error`, `EmailOtpDispatchOutcome`, `StoreId`, `.RegisterAsync`, `Func`, `ResultTelemetryExtensions`, `Task`, `PaginationQueryableExtensions`, `PaginationResponse`, `BrevoEmailGateway`, `KeycloakTokenClient`, `IReadOnlyList`, `.PaginateAsync`, `VerifyPhoneOtpResponse`, `DomainEvent`, `.GetProductAsync`, `ILogger`, `NetGsmSmsGateway`?**
  _High betweenness centrality (0.058) - this node is a cross-community bridge._
- **Should `AuditableEntityResponse` be split into smaller, more focused modules?**
  _Cohesion score 0.06218487394957983 - nodes in this community are weakly interconnected._