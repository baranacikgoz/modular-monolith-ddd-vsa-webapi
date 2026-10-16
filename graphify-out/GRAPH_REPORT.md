# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-10-10)

## Corpus Check
- 586 files · ~94,963 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 3842 nodes · 10163 edges · 279 communities (167 shown, 112 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 301 edges (avg confidence: 0.86)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `f46e9f41`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- Result
- NotificationPayload
- OutboxOptions
- AuditableEntityResponse
- ApplicationUserId
- ReCaptchaService
- ApplySearchLanguageInterceptor
- IDbContext
- Response
- Error
- Common.Domain.ResultMonad
- ISearchLocalized
- ISearchLanguageResolver
- KeycloakTokenClient.cs
- KeycloakPermissionAuthorizationHandler
- EmailOptions
- StoreId
- Common.Application.Options
- Common.Domain.StronglyTypedIds
- FullTextSearchOptions
- BoundedRequestCaptureStream
- DeviceRegistration
- IOptions
- .RegisterAsync
- Stores/v1/My/Update/Request.cs
- Response
- RequestResponseBodyLoggingMiddleware
- fluentvalidation
- Func
- ProjectionUpsertOutcome
- IApplicationBuilder
- .NotFound
- Response
- Response
- OutboxMessage
- CustomRateLimitingOptions
- AdminRepresentations.cs
- ICollection
- ObservabilityOptions
- ConfigureSwaggerOptions.cs
- Request
- SendForLogin/Request.cs
- HttpContext
- KeycloakOptions
- LoggerMessage
- PaginationQueryableExtensions
- Outbox Misuse Check
- microsoft_extensions_dependencyinjection
- Add Integration Event Command
- Response
- v1/RemoveProduct/Request.cs
- CaptchaOptions
- Request
- V1StoreCreatedDomainEvent
- BrevoEmailGateway
- Infrastructure/StringExtensions.cs
- Cross-Module Reference Violation
- InboxCleanupJob
- ProductTemplates/v1/Search/Request.cs
- Bogus Test Data
- KeycloakTokenClient
- StronglyTypedIdBinder
- RequestLoggingOptions
- DatabaseOptions.cs
- Action
- OutboxModule
- .AddNotificationsSignalR
- ValueObject
- .PaginateAsync
- ModulesOptions.cs
- Full-Text Search
- ValidationContextExtensions
- .AddDeviceRegistryReconciliation
- AsNoTracking Coverage Check
- Response
- TokenResponseRepresentation
- .ReserveSeriesAsync
- Response
- Split-Deployment PoC
- EntityTypeBuilder
- DomainEvent
- ProductsDbContext
- PushOptions
- Configuration-Driven Module Registration
- Setup
- .GetProductAsync
- Response
- AuditableEntity
- .MapEndpoint
- Response
- ILogger
- Response
- InterModuleRequestOptions
- ResiliencyOptions
- ResultToResponseTransformer.cs
- Response
- Common.InterModuleRequests.Contracts
- InventoryOptions
- StockLevel
- Response
- KeycloakPermissionPolicyProvider
- Response
- SkipOverlappingRecurringJobFilter
- IResxLocalizer
- Response
- IModule
- ProductsModule
- HealthCheckOptions
- InterModuleRequestHandlerDefinition
- Exception
- Response
- ProductTemplateId
- docker-compose.yml (Base Stack)
- IServiceCollection
- ObjectStorageOptions
- SendRequestBody
- Seeder
- InterModuleRequestHandler
- .WriteTooManyRequestsToResponse
- Response
- ProcessedMessage
- Type
- Setup
- Response
- ActivitySource
- OutboxOptions.cs
- .EnsureNoMigrationsPending
- ConfigureSwaggerOptions
- Request
- DefaultIdType
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
- .GetMeAsync
- DateTimeOffset
- Response
- NotificationsTelemetry
- KeycloakScopes
- DevicesOptions
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- BackgroundJobsOptions
- Key decisions
- Response
- FeatureFlags
- Keycloak realm as code
- KeycloakPermissionAuthorizationHandler.cs
- .SaveWithOutboxAsync
- NetGsmSmsGateway
- .UseInfrastructure
- WebhookCallback/Request.cs
- ResxLocalizationOptions
- Response
- RequestBody
- OpenApiOptions
- DeviceRegistryReconciliationService
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
- DefaultResponsesOperationFilter
- EmailOtpDispatchOutcome
- SmsOtpDispatchOutcome
- RouteGroupBuilder
- Request
- Response
- SendForEmail/Request.cs
- CustomValidator
- Request
- Common.Application.EventBus
- AuditLogOptions
- ProjectionReconciliationOptions.cs
- ResultTelemetryExtensions
- StronglyTypedIdSchemaFilter.cs
- SignalROptions
- microsoft_aspnetcore_mvc
- JobHousekeepingOptions
- Products.Endpoints
- Products/v1/Update/Request.cs
- Notifications.Infrastructure
- .Capture
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

## Communities (279 total, 112 thin omitted)

### Community 0 - "Result"
Cohesion: 0.05
Nodes (21): ICurrentUser, Id, IdAsString, Roles, SessionId, FeatureFlagResultExtensions, Result, Error (+13 more)

### Community 1 - "NotificationPayload"
Cohesion: 0.20
Nodes (9): SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, INotificationDispatcher, INotificationsClient, NotificationPayload, SignalRNotificationDispatcher (+1 more)

### Community 2 - "OutboxOptions"
Cohesion: 0.09
Nodes (19): OutboxOptions, BaseBackoffSeconds, BatchSize, ClaimLeaseSeconds, Cleanup, IsProcessor, LagThresholdMinutes, MaxBackoffSeconds (+11 more)

### Community 3 - "AuditableEntityResponse"
Cohesion: 0.08
Nodes (21): Products.Endpoints.Stores.v1.Search, Common.Application.DTOs, Products.Endpoints.ProductTemplates.v1.Get, Products.Endpoints.Products.v1.Get, AuditableEntityResponse, CreatedBy, CreatedOn, Id (+13 more)

### Community 5 - "ApplicationUserId"
Cohesion: 0.08
Nodes (21): ApplicationUserId, IsEmpty, Value, IKeycloakAdminClient, GrantedPermission, KeycloakUser, KeycloakUserPage, KeycloakUserSession (+13 more)

### Community 7 - "ApplySearchLanguageInterceptor"
Cohesion: 0.31
Nodes (3): ApplyAuditingInterceptor, ApplySearchLanguageInterceptor, Setup

### Community 8 - "IDbContext"
Cohesion: 0.10
Nodes (9): JobHousekeepingJob, IDbContext, AuditLog, ChangeTracker, Database, IPushGateway, DummyPushGateway, FirebasePushGateway (+1 more)

### Community 9 - "Response"
Cohesion: 0.25
Nodes (6): Products.Endpoints.ProductTemplates.v1.Search, Endpoint, Response, Brand, Color, Model

### Community 10 - "Error"
Cohesion: 0.06
Nodes (24): Inventory.Domain.StockReservations.Errors, StringLocalizerExtensions, StringExtensions, Error, Key, ParameterName, StatusCode, SubErrors (+16 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.10
Nodes (25): IAM.Endpoints.Captcha.VersionNeutral, Products.Infrastructure.InterModuleRequestHandlers, Common.Application.Search, Products.Endpoints.Stores, Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Products.Domain.Products (+17 more)

### Community 12 - "ISearchLocalized"
Cohesion: 0.25
Nodes (7): Add a new language/culture, Add search to a new entity _(Build checklist)_, Change the vector (weights, fields, or config), Extending and maintaining, File map _(Build)_, ISearchLocalized, Language

### Community 13 - "ISearchLanguageResolver"
Cohesion: 0.24
Nodes (6): Configuration, ISearchLanguageResolver, UniversalConfig, SearchLanguageResolver, UniversalConfig, Setup

### Community 14 - "KeycloakTokenClient.cs"
Cohesion: 0.09
Nodes (10): IAM.Infrastructure.Keycloak.Representations, Notifications.Infrastructure.Email.Brevo, Inventory.Infrastructure.Gateway, IAM.Domain.Captcha, IAM.Infrastructure.Captcha.Services, Inventory.Application.Gateway, IAM.Domain.Errors, IAM.Infrastructure.Keycloak (+2 more)

### Community 15 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.20
Nodes (4): KeycloakPermission, KeycloakPermissionAuthorizationHandler, KeycloakPermissionRequirement, Permission

### Community 16 - "EmailOptions"
Cohesion: 0.10
Nodes (23): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+15 more)

### Community 17 - "StoreId"
Cohesion: 0.08
Nodes (22): V1ProductAddedToStoreDomainEvent, ProductSnapshot, V1ProductRemovedFromStoreDomainEvent, V1ProductRemovedFromStoreDomainEventExtensions, V1StoreAddressUpdatedDomainEvent, V1StoreDeactivatedDomainEvent, V1StoreDeactivatedWithStockOnHandDomainEvent, V1StoreDescriptionUpdatedDomainEvent (+14 more)

### Community 18 - "Common.Application.Options"
Cohesion: 0.06
Nodes (17): Outbox, Common.Application.BackgroundJobs, Common.Infrastructure.Persistence.Outbox, Common.Infrastructure.RateLimiting, Products.Infrastructure.RateLimiting, Common.Infrastructure.Storage, Common.Infrastructure.Extensions, BackgroundJobs (+9 more)

### Community 19 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.11
Nodes (7): Common.Domain.StronglyTypedIds, Common.Domain.Events, Common.Application.JsonConverters, Common.Domain.Aggregates, Common.Infrastructure.Persistence.ValueConverters, Products.Domain.Stores.DomainEvents.v1, NotificationGroupName

### Community 20 - "FullTextSearchOptions"
Cohesion: 0.29
Nodes (7): Gotchas, FullTextSearchOptions, CultureToConfig, DefaultConfig, RankWeights, UseUnaccent, FullTextSearchOptionsValidator

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.14
Nodes (6): BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 22 - "DeviceRegistration"
Cohesion: 0.11
Nodes (13): Notifications.Infrastructure.Persistence.EntityConfigurations, DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive, LastReconciledOn, PushToken (+5 more)

### Community 23 - "IOptions"
Cohesion: 0.15
Nodes (10): IEmailGateway, ISmsGateway, DummyEmailGateway, Setup, ThrottledEmailGateway, OtpService, OtpServiceBase, DummySmsGateway (+2 more)

### Community 24 - ".RegisterAsync"
Cohesion: 0.09
Nodes (18): IInterModuleRequestClient, MassTransitInterModuleRequestClient, BindDeviceSessionRequest, BindDeviceSessionResponse, DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse, SendPhoneOtpRequest, SendPhoneOtpResponse (+10 more)

### Community 25 - "Stores/v1/My/Update/Request.cs"
Cohesion: 0.33
Nodes (6): Products.Endpoints.Stores.v1.My.Update, Request, Address, Description, Name, RequestValidator

### Community 26 - "Response"
Cohesion: 0.29
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, Endpoint, Response, EmailVerificationToken, IsRegistered

### Community 28 - "fluentvalidation"
Cohesion: 0.13
Nodes (14): IAM.Domain.Users, Common.Domain.Extensions, Notifications.Infrastructure.Devices.UpdateCurrentPushToken, Common.Application.Validation, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, Common.Domain.Devices, IAM.Endpoints.Common.Validations, DeviceSessionConstants (+6 more)

### Community 29 - "Func"
Cohesion: 0.11
Nodes (17): SyncExtensions, CheckRegistrationRateLimitingPolicy, OnRejected, EmailRateLimitingPolicy, OnRejected, OtpVerifyRateLimitingPolicy, OnRejected, RegisterRateLimitingPolicy (+9 more)

### Community 30 - "ProjectionUpsertOutcome"
Cohesion: 0.20
Nodes (7): Common.Application.Persistence.Projections, ProjectionEntity, SourceVersion, ProjectionUpsertOutcome, Inserted, Stale, Updated

### Community 31 - "IApplicationBuilder"
Cohesion: 0.07
Nodes (17): IamModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority, InventoryModule, ActivitySourceNames (+9 more)

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
Cohesion: 0.09
Nodes (18): Inventory.Endpoints.StockReservations.v1.ReserveSeries, ProblemDetailsExtensions, GetUsersInRolePageRequest, GetUsersInRolePageResponse, RoleUserSummary, GetUsersInRolePageRequestHandler, RequestBody, FirstDeadline (+10 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.08
Nodes (17): ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics, EnableTracing, LogSink, MinimumLevel (+9 more)

### Community 40 - "ConfigureSwaggerOptions.cs"
Cohesion: 0.17
Nodes (4): Host, Host.Swagger, Program, Setup

### Community 41 - "Request"
Cohesion: 0.15
Nodes (13): Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken (+5 more)

### Community 42 - "SendForLogin/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForLogin, Request, CaptchaToken, PhoneNumber, RequestValidator

### Community 43 - "HttpContext"
Cohesion: 0.18
Nodes (5): RequestBodyLimitEndpointExtensions, RequestBodyLimitMetadata, MaxBodyBytes, HttpContextExtensions, RateLimitPartitions

### Community 44 - "KeycloakOptions"
Cohesion: 0.12
Nodes (15): KeycloakOptions, AttemptTimeoutSeconds, Authority, BaseUrl, DecisionCacheMaxDurationSeconds, Realm, RequireHttpsMetadata, ResourceClientId (+7 more)

### Community 45 - "LoggerMessage"
Cohesion: 0.12
Nodes (7): AuditLogRetentionJobRegistrar, AuditLogRetentionService, Setup, Setup, StockReservationExpirySweepJobRegistrar, StockReservationExpirySweepService, DeviceRegistryReconcileJobRegistrar

### Community 46 - "PaginationQueryableExtensions"
Cohesion: 0.12
Nodes (8): CursorBound, Value, KeyedRow, Item, Sort, Tie, PaginationQueryableExtensions, ParameterReplacer

### Community 48 - "microsoft_extensions_dependencyinjection"
Cohesion: 0.05
Nodes (27): Common.Infrastructure.Modules, Common.InterModuleRequests, Notifications.Infrastructure.Devices, Inventory.Infrastructure.InterModuleRequestHandlers, Inventory.Endpoints, Common.Endpoints.Versioning, Notifications.Infrastructure.Email, Inventory.Application.Persistence (+19 more)

### Community 50 - "Response"
Cohesion: 0.14
Nodes (12): IAM.Endpoints.Users.VersionNeutral.Search, Endpoint, Response, BirthDate, CreatedOn, Email, Enabled, FirstName (+4 more)

### Community 51 - "v1/RemoveProduct/Request.cs"
Cohesion: 0.40
Nodes (5): Products.Endpoints.Stores.v1.RemoveProduct, Request, Id, ProductId, RequestValidator

### Community 52 - "CaptchaOptions"
Cohesion: 0.16
Nodes (13): CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey, Provider, ScoreThreshold, SecretKey (+5 more)

### Community 53 - "Request"
Cohesion: 0.33
Nodes (6): Request, Address, Description, Name, OwnerId, RequestValidator

### Community 54 - "V1StoreCreatedDomainEvent"
Cohesion: 0.10
Nodes (14): Products.Application.Products.DomainEventHandlers.v1, Products.Application.Stores.DomainEventHandlers.v1, DomainEventHandlerBase, IEventHandler, IEventHandlerWrapper, IIntegrationEventOutbox, Setup, ProductCreatedIntegrationEventPublishingHandler (+6 more)

### Community 55 - "BrevoEmailGateway"
Cohesion: 0.16
Nodes (9): BrevoEmailGateway, SendContact, Email, Name, SendErrorBody, Code, Message, SendResponseBody (+1 more)

### Community 58 - "InboxCleanupJob"
Cohesion: 0.12
Nodes (9): IDatabaseSeeder, Priority, IOutboxDbContext, OutboxMessages, InboxCleanupJob, OutboxCleanupJob, OutboxMetricsJob, ProductsDatabaseSeeder (+1 more)

### Community 59 - "ProductTemplates/v1/Search/Request.cs"
Cohesion: 0.25
Nodes (7): Constants, Request, Brand, Color, Model, SearchTerm, RequestValidator

### Community 63 - "RequestLoggingOptions"
Cohesion: 0.13
Nodes (15): RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes, SensitiveQueryParamPaths (+7 more)

### Community 64 - "DatabaseOptions.cs"
Cohesion: 0.67
Nodes (3): DatabaseOptions, ConnectionString, DatabaseOptionsValidator

### Community 65 - "Action"
Cohesion: 0.17
Nodes (4): IBackgroundJobs, IRecurringBackgroundJobs, BackgroundJobsService, RecurringBackgroundJobsService

### Community 66 - "OutboxModule"
Cohesion: 0.22
Nodes (5): OutboxModule, ActivitySourceNames, MeterNames, Name, StartupPriority

### Community 69 - ".PaginateAsync"
Cohesion: 0.26
Nodes (3): PaginationCursor, Payload, TaggedValue

### Community 70 - "ModulesOptions.cs"
Cohesion: 0.67
Nodes (3): ModulesOptions, EnabledModules, ModulesOptionsValidator

### Community 71 - "Full-Text Search"
Cohesion: 0.18
Nodes (11): Full-Text Search, How it works, Non-goals, One-time setup _(Build, in a migration)_, Per-entity strategy, Query path, Ranking, Testing (+3 more)

### Community 76 - "Response"
Cohesion: 0.20
Nodes (8): Products.Endpoints.Stores.v1.My.Get, Endpoint, Response, Address, Description, Name, OwnerId, ProductCount

### Community 77 - "TokenResponseRepresentation"
Cohesion: 0.13
Nodes (14): DecisionRepresentation, Result, PermissionRepresentation, ResourceName, Scopes, TokenErrorRepresentation, Error, ErrorDescription (+6 more)

### Community 79 - ".ReserveSeriesAsync"
Cohesion: 0.46
Nodes (4): GetProductRequest, GetProductResponse, Endpoint, GetProductRequestHandler

### Community 81 - "Response"
Cohesion: 0.25
Nodes (7): Endpoint, Response, Address, Description, Name, OwnerId, ProductCount

### Community 82 - "Split-Deployment PoC"
Cohesion: 0.25
Nodes (6): Concurrent safety, Files added by this PoC, How to run, Split-Deployment PoC, What this proves, README (Boilerplate Overview)

### Community 84 - "EntityTypeBuilder"
Cohesion: 0.14
Nodes (8): AuditableEntityConfiguration, AuditLogEntryConfiguration, JobRowConfiguration, TrigramIndexExtensions, ProcessedMessageConfiguration, DomainEventConverter, StronglyTypedIdValueConverter, StoreConfiguration

### Community 85 - "DomainEvent"
Cohesion: 0.08
Nodes (25): Inventory.Domain.StockReservations.DomainEvents.v1, AggregateRoot, Events, Id, Version, DomainEvent, CreatedOn, Id (+17 more)

### Community 86 - "ProductsDbContext"
Cohesion: 0.07
Nodes (25): IdBound, Value, JobClaimExtensions, ProjectionUpsertExtensions, EventDispatcher, IntegrationEventOutbox, HasPending, BaseDbContext (+17 more)

### Community 87 - "PushOptions"
Cohesion: 0.12
Nodes (20): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri, Type (+12 more)

### Community 90 - ".GetProductAsync"
Cohesion: 0.54
Nodes (4): GetStockLevelRequest, GetStockLevelResponse, GetStockLevelRequestHandler, Endpoint

### Community 91 - "Response"
Cohesion: 0.22
Nodes (7): Products.Endpoints.Products.v1.My.Get, Endpoint, Response, Description, Name, Price, Quantity

### Community 92 - "AuditableEntity"
Cohesion: 0.10
Nodes (17): IAggregateRoot, Events, Id, Version, AuditableEntity, AuditableEntity, Id, CreatedBy (+9 more)

### Community 93 - ".MapEndpoint"
Cohesion: 0.29
Nodes (4): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, Response, ClientKey, Setup

### Community 95 - "Response"
Cohesion: 0.22
Nodes (7): Products.Endpoints.Products.v1.My.Search, Endpoint, Response, Description, Name, Price, Quantity

### Community 96 - "ILogger"
Cohesion: 0.18
Nodes (4): ProjectionReconciliationJob, DatabaseSeederOrchestrator, SeedingCompletionTracker, Setup

### Community 98 - "Response"
Cohesion: 0.22
Nodes (7): Products.Endpoints.Products.v1.Search, Endpoint, Response, Description, Name, Price, Quantity

### Community 99 - "InterModuleRequestOptions"
Cohesion: 0.25
Nodes (7): InterModuleRequestOptions, DependencyUnavailableRetryAfterSeconds, HandlerConcurrentMessageLimit, HandlerPrefetchCount, Timeouts, TimeoutSeconds, InterModuleRequestOptionsValidator

### Community 101 - "ResiliencyOptions"
Cohesion: 0.05
Nodes (30): KeyedResilienceProfile, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, RateLimitFailOpen, RateLimitPermits (+22 more)

### Community 104 - "ResultToResponseTransformer.cs"
Cohesion: 0.13
Nodes (10): Common.Application.EndpointFilters, RouteHandlerBuilderExtensions, ProblemResponse, ResultToAcceptedResponseTransformer, ResultToCreatedResponseTransformer, ResultToResponseTransformer, ResultToResponseTransformer, RouteHandlerBuilderExtensions (+2 more)

### Community 105 - "Response"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegisterByEmail, Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 106 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.08
Nodes (22): Notifications.Application.Otp, IAM.Endpoints.Users, Common.Application.Caching, Common.Application.FeatureManagement, IAM.Endpoints.Otp, Notifications.Application.Persistence, Notifications.Infrastructure.Telemetry, Common.InterModuleRequests.IAM (+14 more)

### Community 107 - "InventoryOptions"
Cohesion: 0.13
Nodes (16): InventoryOptions, MaxOccurrencesPerSeries, SafetyBufferQuantity, StockReservationExpirySweepBatchSize, StockReservationExpirySweepCron, WarehouseGatewayBaseUrl, WarehouseGatewayProvider, WebhookMaxBodyBytes (+8 more)

### Community 108 - "StockLevel"
Cohesion: 0.11
Nodes (8): Inventory.Domain.StockLevels.DomainEvents.v1, StronglyTypedIdHelper, V1StockLevelCreatedDomainEvent, StockLevel, ProductId, QuantityOnHand, StockLevelId, StockLevelConfiguration

### Community 109 - "Response"
Cohesion: 0.33
Nodes (4): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, Endpoint, Response, IsRegistered

### Community 114 - "Response"
Cohesion: 0.13
Nodes (13): IAM.Endpoints.Users.VersionNeutral.Me.Get, Endpoint, Response, BirthDate, CreatedOn, Email, FirstName, Id (+5 more)

### Community 117 - "Response"
Cohesion: 0.11
Nodes (15): Inventory.Endpoints.StockReservations.v1.Get, ReservationStatus, Active, Committed, Expired, Released, RequiresReconciliation, Endpoint (+7 more)

### Community 118 - "IModule"
Cohesion: 0.21
Nodes (6): IModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority

### Community 119 - "ProductsModule"
Cohesion: 0.12
Nodes (7): Setup, ProductsModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority

### Community 124 - "HealthCheckOptions"
Cohesion: 0.40
Nodes (5): HealthCheckOptions, EnableHealthChecks, ReadinessTimeoutInSeconds, StartupTimeoutInSeconds, HealthCheckOptionsValidator

### Community 125 - "InterModuleRequestHandlerDefinition"
Cohesion: 0.18
Nodes (3): ConcurrencyConfiguratorExtensions, InterModuleRequestHandlerDefinition, RequestType

### Community 126 - "Exception"
Cohesion: 0.18
Nodes (3): GlobalExceptionHandlingMiddleware, Endpoint, NotificationsHub

### Community 127 - "Response"
Cohesion: 0.20
Nodes (8): Products.Endpoints.Stores.v1.Get, Endpoint, Response, Address, Description, Name, OwnerId, ProductCount

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

### Community 134 - "SendRequestBody"
Cohesion: 0.29
Nodes (6): SendRequestBody, HtmlContent, Sender, Subject, TextContent, To

### Community 135 - "Seeder"
Cohesion: 0.26
Nodes (4): GetSeedUserIdsRequest, GetSeedUserIdsResponse, GetSeedUserIdsRequestHandler, Seeder

### Community 136 - "InterModuleRequestHandler"
Cohesion: 0.06
Nodes (31): IInterModuleRequest, IInterModuleRequestHandler, InterModuleRequestHandler, DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, IssueVerificationTokenRequest, IssueVerificationTokenResponse (+23 more)

### Community 138 - "Response"
Cohesion: 0.33
Nodes (4): Products.Endpoints.Stores.v1.AddProduct, Endpoint, Response, Id

### Community 140 - "ProcessedMessage"
Cohesion: 0.14
Nodes (12): IInboxCleanupTarget, ModuleName, IInboxStore, ProcessedMessage, ConsumerName, MessageId, ProcessedOn, StatelessInboxStore (+4 more)

### Community 141 - "Type"
Cohesion: 0.12
Nodes (9): StrictDateTimeOffsetJsonConverter, StronglyTypedIdListReadOnlyJsonConverter, StronglyTypedIdReadOnlyJsonConverter, StronglyTypedIdReadOnlyJsonConverter, StronglyTypedIdWriteOnlyJsonConverter, UnknownDomainEvent, IStronglyTypedId, Value (+1 more)

### Community 144 - "Response"
Cohesion: 0.14
Nodes (12): IAM.Endpoints.Users.VersionNeutral.Get, Endpoint, Response, BirthDate, CreatedOn, Email, Enabled, FirstName (+4 more)

### Community 146 - "ActivitySource"
Cohesion: 0.17
Nodes (6): RequireFeatureFilter, BackgroundJobsTelemetry, IamTelemetry, InventoryTelemetry, OutboxTelemetry, ProductsTelemetry

### Community 151 - "OutboxOptions.cs"
Cohesion: 0.32
Nodes (7): OutboxCleanupSettings, BatchSize, CronSchedule, Enabled, RetentionDays, OutboxCleanupSettingsValidator, OutboxOptionsValidator

### Community 152 - ".EnsureNoMigrationsPending"
Cohesion: 0.39
Nodes (3): AutoMigrateMarker, IAutoMigrateMarker, MigrationGuard

### Community 156 - "Request"
Cohesion: 0.17
Nodes (12): Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FirstName, LastName (+4 more)

### Community 157 - "DefaultIdType"
Cohesion: 0.18
Nodes (5): IntegrationEventHandlerBase, MaxEventAge, ProductCreatedIntegrationEvent, CreateStockLevelOnProductCreatedHandler, LogProductCatalogChangeHandler

### Community 158 - "OtpOptions"
Cohesion: 0.13
Nodes (12): OtpOptions, DummyCode, EmailQuotaWindowMinutes, ExpirationInMinutes, Length, MaxSendsPerEmailPerWindow, MaxSendsPerPhonePerWindow, PhoneQuotaWindowMinutes (+4 more)

### Community 165 - "CurrentUser"
Cohesion: 0.12
Nodes (14): IBackgroundUserContext, UserId, BackgroundUserContext, UserId, CurrentUser, BackgroundUserIdAsString, Id, IdAsString (+6 more)

### Community 167 - "PaginationRequest"
Cohesion: 0.04
Nodes (47): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Stores.v1.My.AuditLog, Products.Endpoints.Products.v1.AuditLog, PaginationRequest, After, IncludeTotal, PageNumber, PageSize (+39 more)

### Community 168 - "ProductTemplates/Setup.cs"
Cohesion: 0.40
Nodes (3): Products.Endpoints.ProductTemplates, Setup, Endpoint

### Community 172 - "Product"
Cohesion: 0.09
Nodes (21): Products.Domain.Products.DomainEvents.v1, V1ProductDescriptionUpdatedDomainEvent, V1ProductNameUpdatedDomainEvent, V1ProductPriceDecreasedDomainEvent, V1ProductPriceIncreasedDomainEvent, V1ProductQuantityDecreasedDomainEvent, V1ProductQuantityIncreasedDomainEvent, Product (+13 more)

### Community 173 - "Task"
Cohesion: 0.05
Nodes (16): OtpCacheEntry, IVariantFeatureManagerExtensions, IInboxStore, IPrivateObjectStore, IPublicObjectStore, ObjectListItem, ObjectListPage, ObjectMetadata (+8 more)

### Community 174 - "system_diagnostics"
Cohesion: 0.07
Nodes (6): Notifications.Application.Push, Notifications.Infrastructure.Push, BackgroundJobs.Telemetry, Notifications.Infrastructure.Push.Firebase, LoginMethods, SessionRevokedReasons

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
Cohesion: 0.11
Nodes (14): IAM.Endpoints.Users.VersionNeutral.SelfRegister, IdentityScheme, Email, PhoneNumber, IdentitySchemeOptions, Scheme, IdentitySchemeOptionsValidator, Setup (+6 more)

### Community 187 - "ReverseProxyOptions"
Cohesion: 0.33
Nodes (5): ReverseProxyOptions, ForwardLimit, IsEnabled, TrustedNetworks, ReverseProxyOptionsValidator

### Community 188 - "BoundedCaptureStream"
Cohesion: 0.15
Nodes (7): RetryAfterHeaderExtensions, BoundedCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 190 - ".GetMeAsync"
Cohesion: 0.08
Nodes (10): HttpClientKeyedExtensions, IKeycloakPermissionClient, IServiceAccountTokenProvider, AccessTokenReader, KeycloakPaths, KeycloakPermissionClient, ServiceAccountTokenProvider, HttpWarehouseGateway (+2 more)

### Community 194 - "DateTimeOffset"
Cohesion: 0.07
Nodes (21): JobRow, FailureKey, FinishedOn, Progress, QueuedOn, RequestedBy, StartedOn, Status (+13 more)

### Community 195 - "Response"
Cohesion: 0.33
Nodes (4): Inventory.Endpoints.StockReservations.v1.Reserve, Endpoint, Response, Id

### Community 201 - "KeycloakScopes"
Cohesion: 0.20
Nodes (9): Devices, Hangfire, KeycloakScopes, Products, ProductTemplates, Sessions, StockReservations, Stores (+1 more)

### Community 202 - "DevicesOptions"
Cohesion: 0.40
Nodes (5): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator

### Community 210 - "BackgroundJobsOptions"
Cohesion: 0.15
Nodes (8): BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount, BackgroundJobsOptionsValidator, HangfireCustomAuthorizationFilter

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
Cohesion: 0.11
Nodes (4): Common.Infrastructure.Auth.Services, IAM.Infrastructure.Auth, Notifications.Infrastructure.Hubs, Notifications.Application.Hubs

### Community 235 - ".SaveWithOutboxAsync"
Cohesion: 0.20
Nodes (7): AuditLogEntry, AggregateId, AggregateType, Event, EventType, Version, OutboxSaveHelper

### Community 236 - "NetGsmSmsGateway"
Cohesion: 0.08
Nodes (18): SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, NetGsmSmsGateway, SendMessageBody, Msg, No (+10 more)

### Community 238 - ".UseInfrastructure"
Cohesion: 0.28
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
Nodes (29): Inventory.Endpoints.StockReservations.v1.Commit, Products.Endpoints.Stores.v1.Update, Request, Body, Id, RequestBody, ProviderReference, RequestBodyValidator (+21 more)

### Community 247 - "OpenApiOptions"
Cohesion: 0.22
Nodes (9): OpenApiOptions, ContactEmail, ContactName, Description, EnableSwagger, LicenseName, LicenseUrl, Title (+1 more)

### Community 249 - "DeviceRegistryReconciliationService"
Cohesion: 0.47
Nodes (5): GetActiveSessionIdsRequest, GetActiveSessionIdsResponse, UserSessionIds, GetActiveSessionIdsRequestHandler, DeviceRegistryReconciliationService

### Community 250 - "microsoft_entityframeworkcore"
Cohesion: 0.06
Nodes (19): Common.Infrastructure.Persistence.Inbox, Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Products.Infrastructure.Persistence.Seeding, Common.Application.Persistence, Inventory.Infrastructure.Persistence.EntityConfigurations, Common.Application.Jobs, Notifications.Domain.Devices (+11 more)

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

### Community 266 - "DefaultResponsesOperationFilter"
Cohesion: 0.29
Nodes (3): DefaultResponsesOperationFilter, RemoveDefaultResponseSchemaFilter, SwaggerDefaultValues

### Community 267 - "EmailOtpDispatchOutcome"
Cohesion: 0.33
Nodes (5): EmailOtpDispatchErrors, EmailOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled

### Community 268 - "SmsOtpDispatchOutcome"
Cohesion: 0.33
Nodes (5): OtpDispatchErrors, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled

### Community 269 - "RouteGroupBuilder"
Cohesion: 0.04
Nodes (22): Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Setup, Endpoint (+14 more)

### Community 276 - "Request"
Cohesion: 0.29
Nodes (7): Request, Description, Name, Price, ProductTemplateId, Quantity, RequestValidator

### Community 279 - "Response"
Cohesion: 0.20
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.Create, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt, Setup

### Community 280 - "SendForEmail/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForEmail, Request, CaptchaToken, Email, RequestValidator

### Community 282 - "CustomValidator"
Cohesion: 0.06
Nodes (32): SecurityHeadersOptions, Headers, SecurityHeadersOptionsValidator, CustomValidator, Request, Email, Otp, RequestValidator (+24 more)

### Community 283 - "Request"
Cohesion: 0.22
Nodes (9): Request, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken, Password, PushToken (+1 more)

### Community 285 - "Common.Application.EventBus"
Cohesion: 0.09
Nodes (7): Common.Endpoints.Webhooks, Notifications.Application.Sms, Common.Application.Persistence.Inbox, Inventory.Application.IntegrationEventHandlers, Common.IntegrationEvents, Notifications.Application.Email, Common.Application.EventBus

### Community 296 - "AuditLogOptions"
Cohesion: 0.33
Nodes (6): AuditLogOptions, PerSchemaRetentionDays, PurgeBatchSize, RetentionCron, RetentionDays, AuditLogOptionsValidator

### Community 299 - "ProjectionReconciliationOptions.cs"
Cohesion: 0.50
Nodes (4): ProjectionReconciliationOptions, MaxPages, PageSize, ProjectionReconciliationOptionsValidator

### Community 307 - "SignalROptions"
Cohesion: 0.40
Nodes (5): SignalROptions, RedisConnectionString, RequireRedisBackplaneInProduction, UseRedisBackplane, SignalROptionsValidator

### Community 311 - "microsoft_aspnetcore_mvc"
Cohesion: 0.05
Nodes (39): Products.Endpoints.Stores.v1.Deactivate, Products.Endpoints.ProductTemplates.v1.Deactivate, Products.Endpoints.Stores.v1.My.RemoveProduct, Inventory.Endpoints.StockReservations.v1.Release, Common.Application.ModelBinders, Products.Endpoints.ProductTemplates.v1.Activate, Request, Id (+31 more)

### Community 314 - "JobHousekeepingOptions"
Cohesion: 0.40
Nodes (5): JobHousekeepingOptions, PageSize, RetentionHours, StaleAfterMinutes, JobHousekeepingOptionsValidator

### Community 319 - "Products/v1/Update/Request.cs"
Cohesion: 0.20
Nodes (11): Products.Endpoints.Products.v1.Update, Request, Body, Id, RequestBody, Description, Name, Price (+3 more)

### Community 323 - "v1/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Probe.v1, Request, Count, RequestValidator

### Community 324 - "Products/v1/My/Update/Request.cs"
Cohesion: 0.20
Nodes (11): Products.Endpoints.Products.v1.My.Update, Request, Body, Id, RequestBody, Description, Name, Price (+3 more)

## Knowledge Gaps
- **901 isolated node(s):** `UserId`, `Id`, `IdAsString`, `Roles`, `SessionId` (+896 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1206 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **112 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `ObjectStorageOptions`, `Common.Domain.ResultMonad`, `ISearchLanguageResolver`, `KeycloakTokenClient.cs`, `Setup`, `EmailOptions`, `FullTextSearchOptions`, `OutboxOptions.cs`, `CustomValidator`, `Setup.Logger.cs`, `fluentvalidation`, `Common.Application.EventBus`, `OtpOptions`, `ObservabilityOptions`, `AuditLogOptions`, `ConfigureSwaggerOptions.cs`, `ProjectionReconciliationOptions.cs`, `KeycloakOptions`, `system_diagnostics`, `FixedWindow`, `CorsOptions`, `microsoft_extensions_dependencyinjection`, `CachingOptions`, `CaptchaOptions`, `RabbitMqOptions`, `SignalROptions`, `SmsOptions`, `Response`, `JobHousekeepingOptions`, `RequestLoggingOptions`, `DatabaseOptions.cs`, `ModulesOptions.cs`, `DevicesOptions`, `BackgroundJobsOptions`, `PushOptions`, `InterModuleRequestOptions`, `ResiliencyOptions`, `KeycloakPermissionAuthorizationHandler.cs`, `InventoryOptions`, `Common.InterModuleRequests.Contracts`, `ResxLocalizationOptions`, `OpenApiOptions`, `microsoft_entityframeworkcore`, `HealthCheckOptions`?**
  _High betweenness centrality (0.082) - this node is a cross-community bridge._
- **What connects `UserId`, `Id`, `IdAsString` to the rest of the system?**
  _901 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Result` be split into smaller, more focused modules?**
  _Cohesion score 0.04955570745044429 - nodes in this community are weakly interconnected._
- **Why does `Result` connect `Result` to `ProductTemplateId`, `ApplicationUserId`, `ReCaptchaService`, `InterModuleRequestHandler`, `Error`, `EmailOtpDispatchOutcome`, `SmsOtpDispatchOutcome`, `StoreId`, `.RegisterAsync`, `Func`, `.NotFound`, `ResultTelemetryExtensions`, `Task`, `PaginationResponse`, `BrevoEmailGateway`, `KeycloakTokenClient`, `.GetMeAsync`, `.PaginateAsync`, `.ReserveSeriesAsync`, `DomainEvent`, `.GetProductAsync`, `NetGsmSmsGateway`?**
  _High betweenness centrality (0.059) - this node is a cross-community bridge._
- **Should `OutboxOptions` be split into smaller, more focused modules?**
  _Cohesion score 0.08522727272727272 - nodes in this community are weakly interconnected._
- **Why does `ApplicationUserId` connect `ApplicationUserId` to `Result`, `NotificationPayload`, `AuditableEntityResponse`, `Seeder`, `InterModuleRequestHandler`, `Type`, `KeycloakPermissionAuthorizationHandler`, `Response`, `StoreId`, `Common.Domain.StronglyTypedIds`, `DeviceRegistration`, `.RegisterAsync`, `DefaultIdType`, `For`, `CurrentUser`, `ICollection`, `PaginationRequest`, `Task`, `PaginationResponse`, `Response`, `Request`, `V1StoreCreatedDomainEvent`, `microsoft_aspnetcore_mvc`, `KeycloakTokenClient`, `DateTimeOffset`, `Response`, `Response`, `EntityTypeBuilder`, `AuditableEntity`, `StockLevel`, `Response`, `DeviceRegistryReconciliationService`, `Response`?**
  _High betweenness centrality (0.058) - this node is a cross-community bridge._
- **Should `AuditableEntityResponse` be split into smaller, more focused modules?**
  _Cohesion score 0.08307692307692308 - nodes in this community are weakly interconnected._