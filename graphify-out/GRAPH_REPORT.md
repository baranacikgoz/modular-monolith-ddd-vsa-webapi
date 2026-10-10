# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-10-07)

## Corpus Check
- 586 files · ~94,892 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 5079 nodes · 10103 edges · 397 communities (187 shown, 210 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 302 edges (avg confidence: 0.86)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `1685e65c`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- IProductsDbContext
- NotificationPayload
- OutboxProcessor
- Common.Application.DTOs
- KeycloakAdminClient
- RedisFixedWindowRateLimiter
- ApplySearchLanguageInterceptor
- FirebasePushGateway
- .RequireOtpTemplateForDefaultCulture
- Error
- Common.Domain.ResultMonad
- KeycloakUser
- ISearchLanguageResolver
- Common.Application.Options
- KeycloakPermissionAuthorizationHandler
- EmailOptions
- Product
- microsoft_extensions_options
- V1ProductAddedToStoreDomainEvent.cs
- IntegrationEvent
- BoundedRequestCaptureStream
- DeviceRegistration
- IEmailGateway
- IKeycloakAdminClient
- Response
- IEvent
- RequestResponseBodyLoggingMiddleware
- common_application_localization_resources
- Result
- .UpsertIfNewerAsync
- NotificationsModule
- .NotFound
- Response
- KeycloakPermissionClient
- OutboxMessage
- CustomRateLimitingOptions
- UserRepresentation
- RequestBody
- ObservabilityOptions
- Host.Swagger
- Request
- .SaveWithOutboxAsync
- CreateStoreRateLimitingPolicy
- KeycloakOptions
- AuditLogRetentionJobRegistrar
- PaginationQueryableExtensions
- Outbox Misuse Check
- microsoft_extensions_dependencyinjection
- Add Integration Event Command
- Response
- ChunkEncodingFreeS3Client
- ReCaptchaService
- VerifyPhoneOtpResponse
- IntegrationEventOutbox
- BrevoEmailGateway
- ProjectionReconciliationJob
- Cross-Module Reference Violation
- OutboxMetricsJob
- .SearchProductTemplatesAsync
- Bogus Test Data
- KeycloakTokenClient
- IDbContext
- RequestLoggingOptions
- .SendAsync
- BackgroundJobsService
- OutboxModule
- .AddNotificationsSignalR
- ValueObject
- PaginationCursor
- IntegrationEventHandlerBase
- FullTextSearchOptions
- Response
- AsNoTracking Coverage Check
- Setup
- Response
- TokenResponseRepresentation
- CheckRegistrationRateLimitingPolicy
- InterModuleRequestHandler
- S3ObjectStoreCore.cs
- .SearchStoresAsync
- Split-Deployment PoC
- Seeder
- AuditableEntityConfiguration
- StockReservation
- ProductsDbContext
- PushOptions
- Configuration-Driven Module Registration
- Program.cs
- .GetProductAsync
- Response
- AuditableEntity
- .MapEndpoint
- KeyedResiliencePipelines
- .SearchMyProductsAsync
- DatabaseSeederOrchestrator
- StockReservationExpirySweepService
- .SearchStoreProductsAsync
- InterModuleRequestOptions
- OutboxDbContext
- ResiliencyOptions
- HttpContextTargetingContextAccessor
- InboxCleanupJob
- .From
- .RegisterAsync
- Common.InterModuleRequests.Contracts
- InventoryOptions
- StockLevel
- .IsRegisteredAsync
- KeycloakPermissionPolicyProvider
- .ReleaseStockReservationAsync
- OutboxCleanupJob
- IAuditableEntity
- Response
- SkipOverlappingRecurringJobFilter
- .AddAuthInfrastructure
- Response
- IModule
- ProductsModule
- .HandleWarehouseWebhookAsync
- .GetVariantAsync
- OtpVerifyRateLimitingPolicy
- NotificationsHub
- Setup.HealthChecks.cs
- InterModuleRequestHandlerDefinition
- GlobalExceptionHandlingMiddleware
- Response
- EmailRateLimitingPolicy
- ProductTemplate
- docker-compose.yml (Base Stack)
- .AddInfrastructure
- ObjectStorageOptions
- SendRequestBody
- .HandleAsync
- IInterModuleRequest
- .WriteTooManyRequestsToResponse
- v1/AddProduct/Request.cs
- StrictDateTimeOffsetJsonConverter
- ProcessedMessage
- IStronglyTypedId
- TokenRefreshRateLimitingPolicy
- Setup
- Response
- JobRow
- .RegisterAsync
- .AddWarehouseGatewayInfrastructure
- Stores/v1/Update/Request.cs
- JobHousekeepingJob
- OutboxOptions
- .EnsureNoMigrationsPending
- ConfigureSwaggerOptions
- Setup.Logger.cs
- ISmsGateway
- Request
- CreateStockLevelOnProductCreatedHandler
- OtpOptions
- IInventoryDbContext
- SeedingCompletionTracker
- BaseDbContext
- ThrottledEmailGateway
- For
- NotificationsDbContext
- ApplicationUserId
- IamModule
- PaginationRequest
- Response
- SendSecurityAlertRequestHandler
- Notifications.Application/IAssemblyReference.cs
- .GetAuditLogAsync
- Common.Domain.Events
- S3ObjectStoreCore
- system_diagnostics
- PaginationResponse
- .FixedWindow
- CorsOptions
- PushMessage
- CachingOptions
- SmsOptions
- RabbitMqOptions
- IAM.Domain
- SendRequestBody
- .HandleAsync
- IdentityScheme
- Policies
- ReverseProxyOptions
- BoundedCaptureStream
- JobClaimExtensions
- ServiceAccountTokenCache
- PolymorphicEventConverter
- GetDeviceSessionsRequest
- .GetMeAsync
- JobStatus
- .ReserveStockAsync
- TokenCreateRateLimitingPolicy
- StockReservationExpirySweepJobRegistrar
- InventoryModule
- .AssignBasicRoleOrRollbackAsync
- NotificationsTelemetry
- KeycloakScopes
- DevicesOptions
- SmsRateLimitingPolicy
- StatelessInboxStore
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- BackgroundJobsOptions
- Key decisions
- BackgroundJobsTelemetry
- IDatabaseSeeder
- Response
- .AddServices
- FeatureFlags
- ICaptchaService
- IBackgroundJobs
- HttpWarehouseGateway
- Keycloak realm as code
- AuditableEntityResponse
- .SingleAsResult
- .AddCommonObjectStorage
- IInboxStore
- NetGsmSmsGateway
- RegisterRateLimitingPolicy
- AuditLogRetentionService
- .AddCommonOptions
- .SendOtp
- .UseModule
- S3PublicObjectStore
- .HasPatternIndex
- JwtBearerConfigureOptions.cs
- AuditLogEntry
- .SendAsync
- ProblemDetailsExtensions
- .UseInfrastructure
- Request
- WebhookCallback/Request.cs
- ResxLocalizationOptions
- Response
- CustomValidator
- KeyedResilienceProfile
- V1ProductCreatedDomainEvent
- Request
- OpenApiOptions
- SendErrorBody
- DeviceRegistryReconciliationService
- Common.Domain.StronglyTypedIds
- StoreId
- .TryWriteAsync
- Products.Domain/IAssemblyReference.cs
- IAM.Application/IAssemblyReference.cs
- Products.Infrastructure/IAssemblyReference.cs
- Products.Application/IAssemblyReference.cs
- Common.Infrastructure/IAssemblyReference.cs
- Notifications.Domain/IAssemblyReference.cs
- IAM.Infrastructure/IAssemblyReference.cs
- SendForRegistration/Request.cs
- Response
- .SendWithPipelineAsync
- Response
- IPrivateObjectStore
- BackgroundJobsModule
- SwaggerDefaultValues
- EmailOtpDispatchOutcome
- SmsOtpDispatchOutcome
- .SaveChangesAsync
- ObjectListPage
- InventoryTelemetry
- .AddResilientHttpClient
- ProductTemplates/v1/Create/Request.cs
- CurrentUser.cs
- EventDispatcher
- .AddProductToMyStoreAsync
- SendResponseBody
- .TapWhenFeatureEnabledAsync
- Response
- SendForEmail/Request.cs
- .TryReadFromJsonAsync
- fluentvalidation
- Request
- Reserve/Request.cs
- RequestBodyLimitFilter.cs
- RemoveDefaultResponseSchemaFilter
- LogProductCatalogChangeHandler
- DummySmsGateway
- ProductTemplateId
- ProductId
- .TryDeserialize
- Revoke/Request.cs
- StringExtensions
- Response
- AuditLogOptions
- .SetConcurrency
- Setup
- ProjectionReconciliationOptions.cs
- ResultTelemetryExtensions
- DeviceRegistrationId
- DefaultResponsesOperationFilter
- .SetRetryAfterHeader
- .AddCustomSwagger
- SignalROptions
- PublishOutcome
- KeyedRow
- Common.Application.ModelBinders
- IResult
- IAutoMigrateMarker.cs
- JobHousekeepingOptions
- Setup
- Setup
- RequestBody
- Products.Endpoints
- Products/v1/Update/Request.cs
- Notifications.Infrastructure
- .VerifySha256
- v1/Request.cs
- Products/v1/My/Update/Request.cs
- .AddNetGsm
- ProductsTelemetry
- Setup
- Common.InterModuleRequests.IAM
- .MapEndpoint
- .MapEndpoint
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
1. `Result` - 144 edges
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
- `Configuration` --references--> `FullTextSearchOptions`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Application/Options/FullTextSearchOptions.cs
- `Gotchas` --references--> `FullTextSearchOptions`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Application/Options/FullTextSearchOptions.cs
- `File map _(Build)_` --references--> `ISearchLanguageResolver`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Application/Search/ISearchLanguageResolver.cs
- `Add search to a new entity _(Build checklist)_` --references--> `ISearchLocalized`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Domain/Entities/ISearchLocalized.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Local Infrastructure Stack** — docker_compose_postgres, docker_compose_rabbitmq, docker_compose_redis, docker_compose_aspire_dashboard [EXTRACTED 1.00]

## Communities (397 total, 210 thin omitted)

### Community 0 - "IProductsDbContext"
Cohesion: 0.05
Nodes (13): ICurrentUser, Id, IdAsString, Roles, SessionId, HttpContextExtensions, IProductsDbContext, Products (+5 more)

### Community 1 - "NotificationPayload"
Cohesion: 0.17
Nodes (5): Notifications.Application.Hubs, INotificationDispatcher, INotificationsClient, NotificationPayload, SignalRNotificationDispatcher

### Community 3 - "Common.Application.DTOs"
Cohesion: 0.25
Nodes (4): Common.Application.DTOs, Products.Endpoints.Stores.v1.Get, Products.Endpoints.ProductTemplates.v1.Get, Products.Endpoints.Products.v1.Get

### Community 6 - "RedisFixedWindowRateLimiter"
Cohesion: 0.13
Nodes (5): FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration

### Community 7 - "ApplySearchLanguageInterceptor"
Cohesion: 0.17
Nodes (3): ApplyAuditingInterceptor, ApplySearchLanguageInterceptor, Setup

### Community 10 - "Error"
Cohesion: 0.08
Nodes (17): Inventory.Domain.StockReservations.Errors, StringLocalizerExtensions, Error, Key, ParameterName, StatusCode, SubErrors, Value (+9 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.09
Nodes (26): IAM.Endpoints.Captcha.VersionNeutral, Products.Infrastructure.InterModuleRequestHandlers, Common.Application.Search, Products.Endpoints.Stores, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Products.Endpoints.ProductTemplates, Products.Domain.Products (+18 more)

### Community 12 - "KeycloakUser"
Cohesion: 0.16
Nodes (4): CreateKeycloakUser, KeycloakUser, KeycloakUserPage, KeycloakUserSession

### Community 13 - "ISearchLanguageResolver"
Cohesion: 0.16
Nodes (6): Configuration, ISearchLanguageResolver, UniversalConfig, SearchLanguageResolver, UniversalConfig, Setup

### Community 14 - "Common.Application.Options"
Cohesion: 0.04
Nodes (31): Common.Infrastructure.Modules, Products.Infrastructure.Persistence, Notifications.Application.Sms, Notifications.Application.Push, IAM.Infrastructure.Keycloak.Representations, Notifications.Infrastructure.Email, Products.Endpoints.Probe, Common.Application.Caching (+23 more)

### Community 16 - "EmailOptions"
Cohesion: 0.09
Nodes (23): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+15 more)

### Community 17 - "Product"
Cohesion: 0.06
Nodes (26): File map _(Build)_, ISearchLocalized, Language, Product, Description, Language, Name, Price (+18 more)

### Community 18 - "microsoft_extensions_options"
Cohesion: 0.13
Nodes (8): Common.Infrastructure.RateLimiting, Products.Infrastructure.RateLimiting, Common.Infrastructure.Extensions, Host.Middlewares, IAM.Infrastructure.RateLimiting, Notifications.Infrastructure.Sms.NetGsm, Policies, RateLimitingConstants

### Community 19 - "V1ProductAddedToStoreDomainEvent.cs"
Cohesion: 0.18
Nodes (6): ProductSnapshot, V1ProductAddedToStoreDomainEvent, V1ProductAddedToStoreDomainEventExtensions, ProductSnapshot, V1ProductRemovedFromStoreDomainEvent, V1ProductRemovedFromStoreDomainEventExtensions

### Community 20 - "IntegrationEvent"
Cohesion: 0.21
Nodes (6): Common.IntegrationEvents, IntegrationEvent, CreatedOn, Id, ProductCreatedIntegrationEvent, StoreCreatedIntegrationEvent

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.18
Nodes (6): BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 22 - "DeviceRegistration"
Cohesion: 0.16
Nodes (10): DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive, LastReconciledOn, PushToken, PushTokenUpdatedOn (+2 more)

### Community 23 - "IEmailGateway"
Cohesion: 0.18
Nodes (3): IEmailGateway, DummyEmailGateway, Setup

### Community 24 - "IKeycloakAdminClient"
Cohesion: 0.06
Nodes (11): IInterModuleRequestClient, MassTransitInterModuleRequestClient, DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse, IKeycloakAdminClient, Endpoint, Endpoint, Endpoint (+3 more)

### Community 25 - "Response"
Cohesion: 0.18
Nodes (7): Setup, Response, AvailableQuantity, Description, Name, Price, Quantity

### Community 26 - "IEvent"
Cohesion: 0.11
Nodes (6): DomainEventHandlerBase, IEventHandler, IEventHandlerWrapper, IEvent, CreatedOn, Id

### Community 28 - "common_application_localization_resources"
Cohesion: 0.06
Nodes (20): Products.Endpoints.Stores.v1.My.Update, IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, IAM.Endpoints.Users.VersionNeutral.Search, IAM.Domain.Users, IAM.Endpoints.Tokens.VersionNeutral.Create, Products.Endpoints.Stores.v1.Search, IAM.Endpoints.Users.VersionNeutral.SelfRegister, Common.Application.FeatureManagement (+12 more)

### Community 29 - "Result"
Cohesion: 0.14
Nodes (7): Result, Error, IsFailure, Success, Value, AsyncExtensions, SyncExtensions

### Community 30 - ".UpsertIfNewerAsync"
Cohesion: 0.13
Nodes (5): ProjectionUpsertExtensions, ProjectionUpsertOutcome, Inserted, Stale, Updated

### Community 31 - "NotificationsModule"
Cohesion: 0.15
Nodes (5): NotificationsModule, ActivitySourceNames, MeterNames, Name, StartupPriority

### Community 33 - "Response"
Cohesion: 0.18
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.Refresh, Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 34 - "KeycloakPermissionClient"
Cohesion: 0.19
Nodes (4): KeycloakPermissionClient, TokenErrorRepresentation, Error, ErrorDescription

### Community 35 - "OutboxMessage"
Cohesion: 0.10
Nodes (17): IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, OutboxMessage, CreatedOn (+9 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.08
Nodes (11): CustomRateLimitingOptions, CheckRegistration, CreateStore, Email, ExemptPathPrefixes, Global, OtpVerify, Register (+3 more)

### Community 37 - "UserRepresentation"
Cohesion: 0.07
Nodes (27): CredentialRepresentation, Temporary, Type, Value, ErrorRepresentation, Error, ErrorMessage, Field (+19 more)

### Community 38 - "RequestBody"
Cohesion: 0.15
Nodes (10): RequestBody, FirstDeadline, IntervalDays, OccurrenceCount, ProductId, QuantityPerOccurrence, Warehouses, WarehouseAllocation (+2 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.07
Nodes (17): ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics, EnableTracing, LogSink, MinimumLevel (+9 more)

### Community 41 - "Request"
Cohesion: 0.14
Nodes (13): Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken (+5 more)

### Community 44 - "KeycloakOptions"
Cohesion: 0.14
Nodes (14): KeycloakOptions, AttemptTimeoutSeconds, Authority, BaseUrl, DecisionCacheMaxDurationSeconds, Realm, RequireHttpsMetadata, ResourceClientId (+6 more)

### Community 46 - "PaginationQueryableExtensions"
Cohesion: 0.18
Nodes (4): CursorBound, Value, PaginationQueryableExtensions, ParameterReplacer

### Community 48 - "microsoft_extensions_dependencyinjection"
Cohesion: 0.04
Nodes (31): Common.Infrastructure.Persistence.Inbox, Common.Infrastructure.Persistence, Common.InterModuleRequests, Notifications.Infrastructure.Devices, Common.Application.Persistence.Inbox, Inventory.Infrastructure.InterModuleRequestHandlers, Inventory.Endpoints, Common.Endpoints.Versioning (+23 more)

### Community 50 - "Response"
Cohesion: 0.11
Nodes (11): Endpoint, Response, BirthDate, CreatedOn, Email, Enabled, FirstName, Id (+3 more)

### Community 52 - "ReCaptchaService"
Cohesion: 0.06
Nodes (21): CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey, Provider, ScoreThreshold, SecretKey (+13 more)

### Community 53 - "VerifyPhoneOtpResponse"
Cohesion: 0.20
Nodes (8): OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, VerifyPhoneOtpResponseExtensions, VerifyPhoneOtpRequestHandler

### Community 54 - "IntegrationEventOutbox"
Cohesion: 0.13
Nodes (8): IIntegrationEventOutbox, IntegrationEventOutbox, HasPending, Setup, SimulateSomeBusinessHandler, StoreCreatedIntegrationEventPublishingHandler, V1StoreCreatedDomainEventHandlers, V1StoreCreatedDomainEvent

### Community 55 - "BrevoEmailGateway"
Cohesion: 0.19
Nodes (3): BrevoEmailGateway, SendResponseBody, MessageId

### Community 59 - ".SearchProductTemplatesAsync"
Cohesion: 0.12
Nodes (11): Endpoint, Request, Brand, Color, Model, SearchTerm, RequestValidator, Response (+3 more)

### Community 61 - "KeycloakTokenClient"
Cohesion: 0.11
Nodes (4): IKeycloakTokenClient, KeycloakTokens, KeycloakTokenClient, Setup

### Community 62 - "IDbContext"
Cohesion: 0.22
Nodes (4): IDbContext, AuditLog, ChangeTracker, Database

### Community 63 - "RequestLoggingOptions"
Cohesion: 0.11
Nodes (15): RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes, SensitiveQueryParamPaths (+7 more)

### Community 64 - ".SendAsync"
Cohesion: 0.22
Nodes (3): SendContact, Email, Name

### Community 66 - "OutboxModule"
Cohesion: 0.14
Nodes (5): OutboxModule, ActivitySourceNames, MeterNames, Name, StartupPriority

### Community 69 - "PaginationCursor"
Cohesion: 0.22
Nodes (3): PaginationCursor, Payload, TaggedValue

### Community 71 - "FullTextSearchOptions"
Cohesion: 0.09
Nodes (22): Add a new language/culture, Add search to a new entity _(Build checklist)_, Change the vector (weights, fields, or config), Extending and maintaining, Full-Text Search, Gotchas, How it works, Non-goals (+14 more)

### Community 72 - "Response"
Cohesion: 0.22
Nodes (5): Endpoint, Response, Brand, Color, Model

### Community 76 - "Response"
Cohesion: 0.18
Nodes (8): Products.Endpoints.Stores.v1.My.Get, Endpoint, Response, Address, Description, Name, OwnerId, ProductCount

### Community 77 - "TokenResponseRepresentation"
Cohesion: 0.15
Nodes (11): DecisionRepresentation, Result, PermissionRepresentation, ResourceName, Scopes, TokenResponseRepresentation, AccessToken, ExpiresIn (+3 more)

### Community 79 - "InterModuleRequestHandler"
Cohesion: 0.11
Nodes (5): IInterModuleRequestHandler, InterModuleRequestHandler, GetProductRequest, GetProductResponse, GetProductRequestHandler

### Community 81 - ".SearchStoresAsync"
Cohesion: 0.14
Nodes (7): Endpoint, Response, Address, Description, Name, OwnerId, ProductCount

### Community 82 - "Split-Deployment PoC"
Cohesion: 0.18
Nodes (9): Concurrent safety, Cross-process call path, Files added by this PoC, How it works, How to run, Split-Deployment PoC, What this proves, README (Boilerplate Overview) (+1 more)

### Community 84 - "AuditableEntityConfiguration"
Cohesion: 0.09
Nodes (8): AuditableEntityConfiguration, JobRowConfiguration, DomainEventConverter, IntegrationEventConverter, StronglyTypedIdValueConverter, OutboxMessageConfig, ProductConfiguration, StoreConfiguration

### Community 85 - "StockReservation"
Cohesion: 0.05
Nodes (17): Inventory.Domain.StockReservations.DomainEvents.v1, V1StockReservationCommitConflictDetectedDomainEvent, V1StockReservationCommittedDomainEvent, V1StockReservationExpiredDomainEvent, V1StockReservationReleaseAttemptAbandonedDomainEvent, V1StockReservationReleaseAttemptStartedDomainEvent, V1StockReservationReleasedDomainEvent, V1StockReservationReservedDomainEvent (+9 more)

### Community 86 - "ProductsDbContext"
Cohesion: 0.15
Nodes (5): ProductsDbContext, Products, ProductTemplates, Stores, Setup

### Community 87 - "PushOptions"
Cohesion: 0.11
Nodes (20): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri, Type (+12 more)

### Community 89 - "Program.cs"
Cohesion: 0.22
Nodes (4): Host, Host.Configurations, Setup, Program

### Community 90 - ".GetProductAsync"
Cohesion: 0.25
Nodes (4): GetStockLevelRequest, GetStockLevelResponse, GetStockLevelRequestHandler, Endpoint

### Community 91 - "Response"
Cohesion: 0.20
Nodes (7): Products.Endpoints.Products.v1.My.Get, Endpoint, Response, Description, Name, Price, Quantity

### Community 92 - "AuditableEntity"
Cohesion: 0.17
Nodes (9): ProjectionEntity, SourceVersion, AuditableEntity, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn (+1 more)

### Community 93 - ".MapEndpoint"
Cohesion: 0.22
Nodes (4): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, Response, ClientKey, Setup

### Community 95 - ".SearchMyProductsAsync"
Cohesion: 0.14
Nodes (7): LikePattern, Endpoint, Response, Description, Name, Price, Quantity

### Community 98 - ".SearchStoreProductsAsync"
Cohesion: 0.10
Nodes (15): Endpoint, Request, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name, OwnerId (+7 more)

### Community 99 - "InterModuleRequestOptions"
Cohesion: 0.20
Nodes (7): InterModuleRequestOptions, DependencyUnavailableRetryAfterSeconds, HandlerConcurrentMessageLimit, HandlerPrefetchCount, Timeouts, TimeoutSeconds, InterModuleRequestOptionsValidator

### Community 100 - "OutboxDbContext"
Cohesion: 0.14
Nodes (4): IOutboxDbContext, OutboxMessages, OutboxDbContext, OutboxMessages

### Community 101 - "ResiliencyOptions"
Cohesion: 0.15
Nodes (12): ResiliencyOptions, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, Keyed, MaxRetryAttempts (+4 more)

### Community 104 - ".From"
Cohesion: 0.05
Nodes (11): ProblemResponse, ResultToAcceptedResponseTransformer, ResultToCreatedResponseTransformer, ResultToResponseTransformer, RouteHandlerBuilderExtensions, RequireFeatureFilter, RouteHandlerBuilderExtensions, RequestBodyLimitEndpointExtensions (+3 more)

### Community 105 - ".RegisterAsync"
Cohesion: 0.12
Nodes (7): EmailNormalization, Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 106 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.10
Nodes (19): Notifications.Application.Otp, IAM.Endpoints.Users, IAM.Endpoints.Otp, Notifications.Application.Persistence, Notifications.Infrastructure.Telemetry, Notifications.Domain.Devices, Common.InterModuleRequests.Contracts, Notifications.Infrastructure.InterModuleRequestHandlers (+11 more)

### Community 107 - "InventoryOptions"
Cohesion: 0.16
Nodes (13): InventoryOptions, MaxOccurrencesPerSeries, SafetyBufferQuantity, StockReservationExpirySweepBatchSize, StockReservationExpirySweepCron, WarehouseGatewayBaseUrl, WarehouseGatewayProvider, WebhookMaxBodyBytes (+5 more)

### Community 108 - "StockLevel"
Cohesion: 0.17
Nodes (7): Inventory.Domain.StockLevels.DomainEvents.v1, V1StockLevelCreatedDomainEvent, StockLevel, ProductId, QuantityOnHand, StockLevelId, StockLevelConfiguration

### Community 109 - ".IsRegisteredAsync"
Cohesion: 0.18
Nodes (7): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, Endpoint, Request, PhoneNumber, RequestValidator, Response, IsRegistered

### Community 110 - "KeycloakPermissionPolicyProvider"
Cohesion: 0.12
Nodes (5): KeycloakPermission, RouteHandlerBuilderExtensions, KeycloakPermissionPolicyProvider, KeycloakPermissionRequirement, Permission

### Community 113 - "IAuditableEntity"
Cohesion: 0.18
Nodes (5): IAuditableEntity, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn

### Community 114 - "Response"
Cohesion: 0.11
Nodes (13): IAM.Endpoints.Users.VersionNeutral.Me.Get, Endpoint, Response, BirthDate, CreatedOn, Email, FirstName, Id (+5 more)

### Community 117 - "Response"
Cohesion: 0.09
Nodes (15): Inventory.Endpoints.StockReservations.v1.Get, ReservationStatus, Active, Committed, Expired, Released, RequiresReconciliation, Endpoint (+7 more)

### Community 118 - "IModule"
Cohesion: 0.09
Nodes (6): IModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority

### Community 119 - "ProductsModule"
Cohesion: 0.12
Nodes (6): ProductsModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority

### Community 124 - "Setup.HealthChecks.cs"
Cohesion: 0.10
Nodes (5): HealthCheckOptions, EnableHealthChecks, ReadinessTimeoutInSeconds, StartupTimeoutInSeconds, HealthCheckOptionsValidator

### Community 127 - "Response"
Cohesion: 0.18
Nodes (7): Endpoint, Response, Address, Description, Name, OwnerId, ProductCount

### Community 129 - "ProductTemplate"
Cohesion: 0.15
Nodes (7): ProductTemplate, Brand, Color, IsActive, Model, Products, ProductTemplateConfiguration

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.48
Nodes (7): Observability (OpenTelemetry), docker-compose.yml (Base Stack), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), Postgres Service (mm.postgres), RabbitMQ Service (mm.rabbitmq), Redis Service (mm.redis)

### Community 132 - "ObjectStorageOptions"
Cohesion: 0.08
Nodes (26): ObjectStorageOptions, AccessKey, AdditionalTransientStatusCodes, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds (+18 more)

### Community 134 - "SendRequestBody"
Cohesion: 0.25
Nodes (6): SendRequestBody, HtmlContent, Sender, Subject, TextContent, To

### Community 135 - ".HandleAsync"
Cohesion: 0.21
Nodes (4): GetSeedUserIdsRequest, GetSeedUserIdsResponse, GetSeedUserIdsRequestHandler, Endpoint

### Community 136 - "IInterModuleRequest"
Cohesion: 0.09
Nodes (13): IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, IInterModuleRequest, IssueVerificationTokenRequest, IssueVerificationTokenResponse, VerifyEmailOtpRequest, VerifyEmailOtpResponse, VerifyEmailOtpResponseExtensions, Endpoint (+5 more)

### Community 138 - "v1/AddProduct/Request.cs"
Cohesion: 0.13
Nodes (8): Products.Endpoints.Stores.v1.AddProduct, Endpoint, Request, Body, Id, RequestValidator, Response, Id

### Community 140 - "ProcessedMessage"
Cohesion: 0.19
Nodes (5): ProcessedMessage, ConsumerName, MessageId, ProcessedOn, ProcessedMessageConfiguration

### Community 141 - "IStronglyTypedId"
Cohesion: 0.08
Nodes (12): StronglyTypedIdReadOnlyJsonConverter, StronglyTypedIdWriteOnlyJsonConverter, AggregateRoot, Events, Id, Version, IAggregateRoot, Events (+4 more)

### Community 144 - "Response"
Cohesion: 0.11
Nodes (12): IAM.Endpoints.Users.VersionNeutral.Get, Endpoint, Response, BirthDate, CreatedOn, Email, Enabled, FirstName (+4 more)

### Community 145 - "JobRow"
Cohesion: 0.19
Nodes (8): JobRow, FailureKey, FinishedOn, Progress, QueuedOn, RequestedBy, StartedOn, Status

### Community 146 - ".RegisterAsync"
Cohesion: 0.08
Nodes (5): BindDeviceSessionRequest, BindDeviceSessionResponse, LoginCompletion, IamTelemetry, BindDeviceSessionRequestHandler

### Community 149 - "Stores/v1/Update/Request.cs"
Cohesion: 0.20
Nodes (9): Products.Endpoints.Stores.v1.Update, Request, Body, Id, RequestBody, Address, Description, Name (+1 more)

### Community 151 - "OutboxOptions"
Cohesion: 0.10
Nodes (21): OutboxCleanupSettings, BatchSize, CronSchedule, Enabled, RetentionDays, OutboxCleanupSettingsValidator, OutboxOptions, BaseBackoffSeconds (+13 more)

### Community 155 - "ISmsGateway"
Cohesion: 0.13
Nodes (7): ISmsGateway, SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, SmsMessage, ThrottledSmsGateway

### Community 156 - "Request"
Cohesion: 0.15
Nodes (12): Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FirstName, LastName (+4 more)

### Community 158 - "OtpOptions"
Cohesion: 0.05
Nodes (21): OtpCacheEntry, OtpOptions, DummyCode, EmailQuotaWindowMinutes, ExpirationInMinutes, Length, MaxSendsPerEmailPerWindow, MaxSendsPerPhonePerWindow (+13 more)

### Community 159 - "IInventoryDbContext"
Cohesion: 0.13
Nodes (7): IInventoryDbContext, StockLevels, StockReservations, InventoryDbContext, StockLevels, StockReservations, Setup

### Community 161 - "BaseDbContext"
Cohesion: 0.12
Nodes (3): BaseDbContext, AuditLog, UtcDateTimeOffsetConverter

### Community 164 - "NotificationsDbContext"
Cohesion: 0.15
Nodes (5): INotificationsDbContext, DeviceRegistrations, NotificationsDbContext, DeviceRegistrations, Setup

### Community 165 - "ApplicationUserId"
Cohesion: 0.10
Nodes (16): IBackgroundUserContext, UserId, ApplicationUserId, IsEmpty, Value, BackgroundUserContext, UserId, CurrentUser (+8 more)

### Community 166 - "IamModule"
Cohesion: 0.18
Nodes (6): IamModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority

### Community 167 - "PaginationRequest"
Cohesion: 0.08
Nodes (27): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Products.v1.AuditLog, PaginationRequest, After, IncludeTotal, PageNumber, PageSize, ShouldIncludeTotal (+19 more)

### Community 168 - "Response"
Cohesion: 0.22
Nodes (4): Products.Endpoints.ProductTemplates.v1.Create, Setup, Response, Id

### Community 169 - "SendSecurityAlertRequestHandler"
Cohesion: 0.27
Nodes (5): SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, SendSecurityAlertRequestHandler

### Community 172 - "Common.Domain.Events"
Cohesion: 0.06
Nodes (20): Products.Domain.Products.DomainEvents.v1, Common.Domain.Events, Common.Application.Persistence.Outbox, Products.Application.Stores.DomainEventHandlers.v1, Products.Domain.Stores.DomainEvents.v1, DomainEvent, CreatedOn, Id (+12 more)

### Community 173 - "S3ObjectStoreCore"
Cohesion: 0.11
Nodes (3): ObjectMetadata, S3ObjectStoreCore, S3PrivateObjectStore

### Community 174 - "system_diagnostics"
Cohesion: 0.10
Nodes (6): Common.Application.BackgroundJobs, BackgroundJobs.Telemetry, BackgroundJobs, Notifications.Infrastructure.Push.Firebase, LoginMethods, SessionRevokedReasons

### Community 175 - "PaginationResponse"
Cohesion: 0.07
Nodes (14): Products.Endpoints.Stores.v1.My.AuditLog, AuditLogDto, PaginationResponse, HasNext, HasPrevious, HasTotal, NextPageNumber, PreviousPageNumber (+6 more)

### Community 176 - ".FixedWindow"
Cohesion: 0.23
Nodes (6): FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit, RateLimitPartitions

### Community 177 - "CorsOptions"
Cohesion: 0.25
Nodes (7): CorsOptions, AllowCredentials, AllowedHeaders, AllowedMethods, AllowedOrigins, MaxAgeInSeconds, CorsOptionsValidator

### Community 178 - "PushMessage"
Cohesion: 0.14
Nodes (4): IPushGateway, PushMessage, DummyPushGateway, Setup

### Community 179 - "CachingOptions"
Cohesion: 0.08
Nodes (22): CachingEntryDefaults, Duration, FactoryHardTimeout, FactorySoftTimeout, FailSafeMaxDuration, FailSafeThrottleDuration, CachingOptions, AllowInMemoryOnlyInProduction (+14 more)

### Community 180 - "SmsOptions"
Cohesion: 0.10
Nodes (20): SmsOptions, AppName, AttemptTimeoutSeconds, BaseUrl, MaxPerDay, MaxPerPhoneNumberPerDay, MaxRetryAttempts, MsgHeader (+12 more)

### Community 181 - "RabbitMqOptions"
Cohesion: 0.10
Nodes (13): RabbitMqOptions, DefaultConcurrentMessageLimit, DefaultPrefetchCount, Host, Password, Port, RetryIntervalDeltaMs, RetryLimit (+5 more)

### Community 183 - "SendRequestBody"
Cohesion: 0.25
Nodes (6): SendRequestBody, AppName, Encoding, IysFilter, Messages, MsgHeader

### Community 184 - ".HandleAsync"
Cohesion: 0.39
Nodes (4): GetUsersInRolePageRequest, GetUsersInRolePageResponse, RoleUserSummary, GetUsersInRolePageRequestHandler

### Community 185 - "IdentityScheme"
Cohesion: 0.24
Nodes (7): IdentityScheme, Email, PhoneNumber, IdentitySchemeOptions, Scheme, IdentitySchemeOptionsValidator, Setup

### Community 187 - "ReverseProxyOptions"
Cohesion: 0.29
Nodes (5): ReverseProxyOptions, ForwardLimit, IsEnabled, TrustedNetworks, ReverseProxyOptionsValidator

### Community 188 - "BoundedCaptureStream"
Cohesion: 0.14
Nodes (6): BoundedCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 189 - "JobClaimExtensions"
Cohesion: 0.28
Nodes (3): IdBound, Value, JobClaimExtensions

### Community 190 - "ServiceAccountTokenCache"
Cohesion: 0.08
Nodes (6): IServiceAccountTokenProvider, KeycloakPaths, ServiceAccountTokenCache, AccessToken, ExpiresAt, ServiceAccountTokenProvider

### Community 192 - "GetDeviceSessionsRequest"
Cohesion: 0.39
Nodes (4): DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, GetDeviceSessionsRequestHandler

### Community 194 - "JobStatus"
Cohesion: 0.33
Nodes (5): JobStatus, Failed, Queued, Running, Succeeded

### Community 195 - ".ReserveStockAsync"
Cohesion: 0.22
Nodes (4): Inventory.Endpoints.StockReservations.v1.Reserve, Endpoint, Response, Id

### Community 198 - "InventoryModule"
Cohesion: 0.15
Nodes (5): InventoryModule, ActivitySourceNames, MeterNames, Name, StartupPriority

### Community 201 - "KeycloakScopes"
Cohesion: 0.20
Nodes (9): Devices, Hangfire, KeycloakScopes, Products, ProductTemplates, Sessions, StockReservations, Stores (+1 more)

### Community 202 - "DevicesOptions"
Cohesion: 0.33
Nodes (5): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator

### Community 210 - "BackgroundJobsOptions"
Cohesion: 0.29
Nodes (7): BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount, BackgroundJobsOptionsValidator

### Community 211 - "Key decisions"
Cohesion: 0.29
Nodes (7): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Key decisions

### Community 214 - "IDatabaseSeeder"
Cohesion: 0.18
Nodes (4): IDatabaseSeeder, Priority, ProductsDatabaseSeeder, Priority

### Community 215 - "Response"
Cohesion: 0.18
Nodes (9): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, Response, ClientId, DeviceName, Id, IpAddress, IsCurrent, LastAccessAt (+1 more)

### Community 217 - "FeatureFlags"
Cohesion: 0.33
Nodes (5): Checkout, FeatureFlags, IAM, Notifications, Products

### Community 218 - "ICaptchaService"
Cohesion: 0.07
Nodes (8): SendPhoneOtpRequest, SendPhoneOtpResponse, ICaptchaService, Endpoint, Endpoint, CachedCaptchaService, DummyCaptchaService, SendPhoneOtpRequestHandler

### Community 220 - "HttpWarehouseGateway"
Cohesion: 0.31
Nodes (3): HttpWarehouseGateway, ReleaseRequestBody, ReleaseResponseBody

### Community 221 - "Keycloak realm as code"
Cohesion: 0.25
Nodes (7): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Secrets, Seed users (dev / test only)

### Community 222 - "AuditableEntityResponse"
Cohesion: 0.29
Nodes (6): AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn

### Community 225 - "IInboxStore"
Cohesion: 0.12
Nodes (6): IInboxCleanupTarget, ModuleName, IInboxStore, Setup, InboxStore, ModuleName

### Community 230 - ".SendOtp"
Cohesion: 0.10
Nodes (5): SendEmailOtpRequest, SendEmailOtpResponse, Endpoint, IOtpService, SendEmailOtpRequestHandler

### Community 232 - "S3PublicObjectStore"
Cohesion: 0.13
Nodes (3): IPublicObjectStore, UploadObjectRequest, S3PublicObjectStore

### Community 235 - "AuditLogEntry"
Cohesion: 0.13
Nodes (7): AuditLogEntry, AggregateId, AggregateType, Event, EventType, Version, AuditLogEntryConfiguration

### Community 236 - ".SendAsync"
Cohesion: 0.20
Nodes (3): SendMessageBody, Msg, No

### Community 239 - "Request"
Cohesion: 0.29
Nodes (7): Request, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name, SearchTerm

### Community 240 - "WebhookCallback/Request.cs"
Cohesion: 0.40
Nodes (5): Inventory.Endpoints.StockReservations.v1.WebhookCallback, Request, ProviderReference, ReservationId, RequestValidator

### Community 241 - "ResxLocalizationOptions"
Cohesion: 0.40
Nodes (4): ResxLocalizationOptions, DefaultCulture, SupportedCultures, ResxLocalizationOptionsValidator

### Community 242 - "Response"
Cohesion: 0.22
Nodes (5): Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 243 - "CustomValidator"
Cohesion: 0.11
Nodes (23): Inventory.Endpoints.StockReservations.v1.Commit, CustomValidator, Request, Body, Id, RequestBody, ProviderReference, RequestBodyValidator (+15 more)

### Community 244 - "KeyedResilienceProfile"
Cohesion: 0.15
Nodes (11): KeyedResilienceProfile, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, RateLimitFailOpen, RateLimitPermits (+3 more)

### Community 245 - "V1ProductCreatedDomainEvent"
Cohesion: 0.22
Nodes (4): Products.Application.Products.DomainEventHandlers.v1, ProductCreatedIntegrationEventPublishingHandler, V1ProductCreatedDomainEventHandlers, V1ProductCreatedDomainEvent

### Community 246 - "Request"
Cohesion: 0.22
Nodes (8): Request, ClientId, DeviceId, DeviceName, Otp, PhoneNumber, PushToken, RequestValidator

### Community 247 - "OpenApiOptions"
Cohesion: 0.22
Nodes (9): OpenApiOptions, ContactEmail, ContactName, Description, EnableSwagger, LicenseName, LicenseUrl, Title (+1 more)

### Community 248 - "SendErrorBody"
Cohesion: 0.67
Nodes (3): SendErrorBody, Code, Message

### Community 249 - "DeviceRegistryReconciliationService"
Cohesion: 0.09
Nodes (7): GetActiveSessionIdsRequest, GetActiveSessionIdsResponse, UserSessionIds, GetActiveSessionIdsRequestHandler, DeviceRegistryReconcileJobRegistrar, DeviceRegistryReconciliationService, Setup

### Community 250 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.07
Nodes (17): Common.Domain.StronglyTypedIds, Common.Application.AuditLog, Inventory.Infrastructure.Persistence.EntityConfigurations, Common.Application.Jobs, Common.Application.JsonConverters, Common.Domain.Entities, Common.Infrastructure.Persistence.Auditing, Common.Infrastructure.Persistence.EntityConfigurations (+9 more)

### Community 251 - "StoreId"
Cohesion: 0.08
Nodes (19): Products.Endpoints.Stores.v1.Deactivate, Products.Endpoints.Stores.v1.My.Create, Products.Endpoints.Stores.v1.RemoveProduct, StoreId, Request, Id, RequestValidator, Request (+11 more)

### Community 260 - "SendForRegistration/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, Request, CaptchaToken, PhoneNumber, RequestValidator

### Community 261 - "Response"
Cohesion: 0.22
Nodes (4): Products.Endpoints.Stores.v1.Create, Setup, Response, Id

### Community 263 - "Response"
Cohesion: 0.29
Nodes (5): Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 265 - "BackgroundJobsModule"
Cohesion: 0.25
Nodes (5): BackgroundJobsModule, ActivitySourceNames, MeterNames, Name, StartupPriority

### Community 267 - "EmailOtpDispatchOutcome"
Cohesion: 0.33
Nodes (5): EmailOtpDispatchErrors, EmailOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled

### Community 268 - "SmsOtpDispatchOutcome"
Cohesion: 0.33
Nodes (5): OtpDispatchErrors, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled

### Community 269 - ".SaveChangesAsync"
Cohesion: 0.04
Nodes (8): Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint

### Community 273 - "ProductTemplates/v1/Create/Request.cs"
Cohesion: 0.40
Nodes (5): Request, Brand, Color, Model, RequestValidator

### Community 276 - ".AddProductToMyStoreAsync"
Cohesion: 0.12
Nodes (12): Products.Endpoints.Stores.v1.My.AddProduct, Constants, Endpoint, Request, Description, Name, Price, ProductTemplateId (+4 more)

### Community 277 - "SendResponseBody"
Cohesion: 0.50
Nodes (4): SendResponseBody, Code, Description, JobId

### Community 279 - "Response"
Cohesion: 0.29
Nodes (5): Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 280 - "SendForEmail/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForEmail, Request, CaptchaToken, Email, RequestValidator

### Community 282 - "fluentvalidation"
Cohesion: 0.06
Nodes (29): IAM.Endpoints.Otp.VersionNeutral.SendForLogin, Notifications.Infrastructure.Devices.UpdateCurrentPushToken, Common.Application.Validation, CustomRateLimitingOptionsValidator, FixedWindowValidator, DatabaseOptions, ConnectionString, DatabaseOptionsValidator (+21 more)

### Community 283 - "Request"
Cohesion: 0.20
Nodes (9): Request, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken, Password, PushToken (+1 more)

### Community 284 - "Reserve/Request.cs"
Cohesion: 0.20
Nodes (7): Request, Body, RequestBody, ProductId, Quantity, ReservationDeadline, RequestValidator

### Community 290 - "ProductId"
Cohesion: 0.15
Nodes (11): Products.Endpoints.Stores.v1.My.RemoveProduct, ProductId, Request, Id, RequestValidator, Request, Id, RequestValidator (+3 more)

### Community 293 - "Revoke/Request.cs"
Cohesion: 0.50
Nodes (4): IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, Request, Id, RequestValidator

### Community 295 - "Response"
Cohesion: 0.25
Nodes (4): Inventory.Endpoints.StockReservations.v1.ReserveSeries, Endpoint, Response, Ids

### Community 296 - "AuditLogOptions"
Cohesion: 0.29
Nodes (6): AuditLogOptions, PerSchemaRetentionDays, PurgeBatchSize, RetentionCron, RetentionDays, AuditLogOptionsValidator

### Community 299 - "ProjectionReconciliationOptions.cs"
Cohesion: 0.50
Nodes (4): ProjectionReconciliationOptions, MaxPages, PageSize, ProjectionReconciliationOptionsValidator

### Community 307 - "SignalROptions"
Cohesion: 0.40
Nodes (5): SignalROptions, RedisConnectionString, RequireRedisBackplaneInProduction, UseRedisBackplane, SignalROptionsValidator

### Community 308 - "PublishOutcome"
Cohesion: 0.50
Nodes (4): PublishOutcome, Failed, Published, Skipped

### Community 310 - "KeyedRow"
Cohesion: 0.50
Nodes (4): KeyedRow, Item, Sort, Tie

### Community 311 - "Common.Application.ModelBinders"
Cohesion: 0.07
Nodes (23): Products.Endpoints.ProductTemplates.v1.Deactivate, Inventory.Endpoints.StockReservations.v1.Release, Common.Application.ModelBinders, Products.Endpoints.ProductTemplates.v1.Activate, StronglyTypedIdBinder, Request, Id, RequestValidator (+15 more)

### Community 314 - "JobHousekeepingOptions"
Cohesion: 0.40
Nodes (5): JobHousekeepingOptions, PageSize, RetentionHours, StaleAfterMinutes, JobHousekeepingOptionsValidator

### Community 317 - "RequestBody"
Cohesion: 0.33
Nodes (6): RequestBody, Description, Name, Price, ProductTemplateId, Quantity

### Community 319 - "Products/v1/Update/Request.cs"
Cohesion: 0.18
Nodes (10): Products.Endpoints.Products.v1.Update, Request, Body, Id, RequestBody, Description, Name, Price (+2 more)

### Community 323 - "v1/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Probe.v1, Request, Count, RequestValidator

### Community 324 - "Products/v1/My/Update/Request.cs"
Cohesion: 0.18
Nodes (10): Products.Endpoints.Products.v1.My.Update, Request, Body, Id, RequestBody, Description, Name, Price (+2 more)

## Knowledge Gaps
- **981 isolated node(s):** `UserId`, `Id`, `IdAsString`, `Roles`, `SessionId` (+976 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 2277 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **210 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `ObjectStorageOptions`, `Common.Domain.ResultMonad`, `EmailOptions`, `microsoft_extensions_options`, `OutboxOptions`, `fluentvalidation`, `Setup.Logger.cs`, `common_application_localization_resources`, `OtpOptions`, `ObservabilityOptions`, `AuditLogOptions`, `ProjectionReconciliationOptions.cs`, `KeycloakOptions`, `system_diagnostics`, `microsoft_extensions_dependencyinjection`, `CorsOptions`, `CachingOptions`, `ReCaptchaService`, `RabbitMqOptions`, `SignalROptions`, `SmsOptions`, `IdentityScheme`, `JobHousekeepingOptions`, `ReverseProxyOptions`, `RequestLoggingOptions`, `FullTextSearchOptions`, `DevicesOptions`, `S3ObjectStoreCore.cs`, `BackgroundJobsOptions`, `PushOptions`, `Program.cs`, `InterModuleRequestOptions`, `ResiliencyOptions`, `JwtBearerConfigureOptions.cs`, `InventoryOptions`, `Common.InterModuleRequests.Contracts`, `ResxLocalizationOptions`, `OpenApiOptions`, `Common.Domain.StronglyTypedIds`, `Setup.HealthChecks.cs`?**
  _High betweenness centrality (0.205) - this node is a cross-community bridge._
- **What connects `UserId`, `Id`, `IdAsString` to the rest of the system?**
  _981 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `IProductsDbContext` be split into smaller, more focused modules?**
  _Cohesion score 0.047619047619047616 - nodes in this community are weakly interconnected._
- **Why does `Result` connect `Result` to `IProductsDbContext`, `ProductTemplate`, `KeycloakAdminClient`, `IInterModuleRequest`, `FirebasePushGateway`, `Error`, `EmailOtpDispatchOutcome`, `SmsOtpDispatchOutcome`, `KeycloakUser`, `.SaveChangesAsync`, `v1/AddProduct/Request.cs`, `Response`, `.RegisterAsync`, `.AddWarehouseGatewayInfrastructure`, `.AddProductToMyStoreAsync`, `.TapWhenFeatureEnabledAsync`, `IEmailGateway`, `IKeycloakAdminClient`, `ISmsGateway`, `.NotFound`, `DummySmsGateway`, `ThrottledEmailGateway`, `StringExtensions`, `.GetAuditLogAsync`, `ResultTelemetryExtensions`, `Common.Domain.Events`, `PaginationResponse`, `Response`, `PushMessage`, `ReCaptchaService`, `VerifyPhoneOtpResponse`, `BrevoEmailGateway`, `IResult`, `.SearchProductTemplatesAsync`, `KeycloakTokenClient`, `.SendAsync`, `.GetMeAsync`, `.ReserveStockAsync`, `PaginationCursor`, `.AssignBasicRoleOrRollbackAsync`, `Response`, `InterModuleRequestHandler`, `.SearchStoresAsync`, `.PaginateAsync`, `StockReservation`, `ICaptchaService`, `.GetProductAsync`, `HttpWarehouseGateway`, `.SingleAsResult`, `.SearchMyProductsAsync`, `NetGsmSmsGateway`, `.SearchStoreProductsAsync`, `.SendOtp`, `.RegisterAsync`, `.SendAsync`, `.IsRegisteredAsync`, `.ReleaseStockReservationAsync`, `Response`, `.HandleWarehouseWebhookAsync`, `Response`?**
  _High betweenness centrality (0.100) - this node is a cross-community bridge._
- **Should `RedisFixedWindowRateLimiter` be split into smaller, more focused modules?**
  _Cohesion score 0.13333333333333333 - nodes in this community are weakly interconnected._
- **Why does `ApplicationUserId` connect `ApplicationUserId` to `IProductsDbContext`, `NotificationPayload`, `KeycloakAdminClient`, `.HandleAsync`, `KeycloakUser`, `IStronglyTypedId`, `KeycloakPermissionAuthorizationHandler`, `Response`, `JobRow`, `.RegisterAsync`, `Product`, `IntegrationEvent`, `DeviceRegistration`, `IKeycloakAdminClient`, `For`, `.TryDeserialize`, `SendSecurityAlertRequestHandler`, `DeviceRegistrationId`, `PaginationResponse`, `Response`, `IntegrationEventOutbox`, `Common.Application.ModelBinders`, `.HandleAsync`, `KeycloakTokenClient`, `GetDeviceSessionsRequest`, `.AssignBasicRoleOrRollbackAsync`, `.LogRoleAssignmentFailed`, `Response`, `.SearchStoresAsync`, `Seeder`, `AuditableEntityConfiguration`, `AuditableEntity`, `AuditableEntityResponse`, `.SearchStoreProductsAsync`, `.RegisterAsync`, `IAuditableEntity`, `Response`, `DeviceRegistryReconciliationService`, `Common.Domain.StronglyTypedIds`, `NotificationsHub`, `Response`?**
  _High betweenness centrality (0.083) - this node is a cross-community bridge._
- **Should `Error` be split into smaller, more focused modules?**
  _Cohesion score 0.08307692307692308 - nodes in this community are weakly interconnected._