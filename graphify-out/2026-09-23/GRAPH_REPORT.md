# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-09-23)

## Corpus Check
- 545 files · ~80,293 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 4571 nodes · 9031 edges · 371 communities (269 shown, 102 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 244 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `ca798d40`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- Inventory.Domain.StockReservations.DomainEvents.v1
- NotificationPayload
- OutboxProcessor
- Notifications.Infrastructure
- Hybrid DDD (Writes) / VSA (Reads)
- KeycloakAdminClient
- RedisFixedWindowRateLimiter
- ApplySearchLanguageInterceptor
- FirebasePushGateway
- .RequireOtpTemplateForDefaultCulture
- Error
- Common.Domain.ResultMonad
- ApplicationUserId
- ISearchLanguageResolver
- UserRepresentation
- KeycloakPermissionAuthorizationHandler
- EmailOptions
- .UseModule
- AuditableEntity
- IntegrationEventHandlerBase
- LogProductCatalogChangeHandler
- BoundedRequestCaptureStream
- DeviceRegistration
- IEmailGateway
- IInterModuleRequestClient
- StockReservation
- IEvent
- RequestResponseBodyLoggingMiddleware
- KeycloakTokenClient
- Result
- Product
- NotificationsModule
- .SingleAsResult
- DomainEvent
- KeycloakPermissionClient
- OutboxMessage
- CustomRateLimitingOptions
- AuditLogEntryConfiguration
- .RefreshToken
- ObservabilityOptions
- ConfigureSwaggerOptions.cs
- Request
- .SaveWithOutboxAsync
- CreateStoreRateLimitingPolicy
- KeycloakOptions
- .RegisterAsync
- .PaginateAsync
- Outbox Misuse Check
- .AddPersistence
- Add Integration Event Command
- Response
- Common.Application.DTOs
- ReCaptchaService
- AuditableEntityConfiguration
- .SendOtp
- BrevoEmailGateway
- AuditLogRetentionJobRegistrar
- Cross-Module Reference Violation
- OutboxMetricsJob
- .SearchProductTemplatesAsync
- Bogus Test Data
- CaptchaOptions
- AuditLogEntry
- RequestLoggingOptions
- .SendAsync
- BackgroundJobsService
- OutboxModule
- .AddNotificationsSignalR
- ValueObject
- .HandleAsync
- IStronglyTypedId
- FullTextSearchOptions
- Response
- .WriteAsync
- AsNoTracking Coverage Check
- IntegrationEventOutbox
- Response
- .SaveChangesAsync
- CheckRegistrationRateLimitingPolicy
- SeedingCompletionTracker
- StoreId
- .SearchStoresAsync
- Split-Deployment PoC
- NotificationsDbContext
- .Configure
- ResultTelemetryExtensions.cs
- Store
- PushOptions
- Configuration-Driven Module Registration
- Setup
- .GetProductAsync
- RequestBody
- Common.Domain.StronglyTypedIds
- .MapEndpoint
- OtpOptions
- .SearchMyProductsAsync
- DatabaseSeederOrchestrator
- StockReservationExpirySweepService
- .SearchStoreProductsAsync
- AuditLogRetentionService
- OutboxDbContext
- ResiliencyOptions
- HttpContextTargetingContextAccessor
- BackgroundJobsTelemetry
- RequestBody
- Response
- microsoft_extensions_dependencyinjection
- .ReleaseStockReservationAsync
- StockLevel
- .IsRegisteredAsync
- KeycloakPermissionPolicyProvider
- AggregateRoot
- OutboxCleanupJob
- ProductsDbContext
- Response
- InterModuleRequestHandler
- .AddAuthInfrastructure
- Response
- IModule
- ProductsModule
- .HandleWarehouseWebhookAsync
- .GetVariantAsync
- TokenRefreshRateLimitingPolicy
- NotificationsHub
- .AddCustomHealthChecks
- .TapWhenFeatureEnabledAsync
- GlobalExceptionHandlingMiddleware
- BaseDbContext
- EmailRateLimitingPolicy
- RedisOtpService
- docker-compose.yml (Base Stack)
- .AddInfrastructure
- OtpVerifyRateLimitingPolicy
- Consumer Idempotency (IntegrationEventHandlerBase)
- SendRequestBody
- .ListSessions
- .AddKeycloakInfrastructure
- .WriteTooManyRequestsToResponse
- ResultToResponseTransformer.cs
- ProductTemplate
- .MapEndpoint
- .GetMeAsync
- IBackgroundJobs
- Setup
- Response
- SecurityHeadersMiddleware
- .RegisterAsync
- .AddOrUpdate
- DeviceRegistryReconciliationService
- ProductTemplateId
- StockReservationExpirySweepJobRegistrar
- OutboxOptions
- .EnsureNoMigrationsPending
- SwaggerDefaultValues
- Response
- SendPhoneOtpRequestHandler
- Request
- CreateStockLevelOnProductCreatedHandler
- OtpService
- IInventoryDbContext
- OtpServiceBase
- .ReserveSeriesAsync
- ThrottledEmailGateway
- For
- IAutoMigrateMarker.cs
- CurrentUser.cs
- IamModule
- Common.Application.Pagination
- Response
- SendSecurityAlertRequestHandler
- Notifications.Application/IAssemblyReference.cs
- IOtpService
- ResxLocalizationOptions
- RequestBody
- FirebasePushGateway.cs
- PaginationResponse
- .FixedWindow
- Request
- .AddPushServices
- CachingOptions
- SmsOptions
- RabbitMqOptions
- IAM.Domain
- SendRequestBody
- RequestBody
- IdentityScheme
- Policies
- ReverseProxyOptions
- BoundedCaptureStream
- V1StoreCreatedDomainEvent
- ServiceAccountTokenCache
- IInterModuleRequest
- CachedCaptchaService
- Response
- CorsOptions
- InventoryOptions
- TokenCreateRateLimitingPolicy
- .ReserveStockAsync
- InventoryModule
- OpenApiOptions
- NotificationsTelemetry
- KeycloakScopes
- fluentvalidation
- SmsRateLimitingPolicy
- Configuration-Driven Module Loading
- Request
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- ConfigureSwaggerOptions
- EventDispatcher
- Setup
- .SendOtp
- Request
- IProductsDbContext
- microsoft_aspnetcore_mvc
- FeatureFlags
- Response
- Stores/v1/Create/Request.cs
- FixedWindow
- Keycloak realm as code
- Key decisions
- Products.Domain.Stores
- .MapEndpoint
- BackgroundJobsOptions
- NetGsmSmsGateway
- UtcDateTimeOffsetConverter
- StronglyTypedIdBinder
- .AddCommonOptions
- Inventory.Domain.StockReservations
- ProductsModule.cs
- Common.Application.Options
- ReCaptchaResponse
- .AddCustomSwagger
- .AddServices
- IssueVerificationTokenRequestHandler
- ProblemDetailsExtensions.cs
- Common.Infrastructure.Persistence.ValueConverters
- KeycloakTokenClient.cs
- BackgroundJobsModule
- RegisterRateLimitingPolicy
- StronglyTypedIdSchemaFilter.cs
- CustomValidator
- ICaptchaService
- .Apply
- system_diagnostics
- .Capture
- SendErrorBody
- Infrastructure/Setup.cs
- Response
- .DeactivateStoreAsync
- .TryWriteAsync
- Products.Domain/IAssemblyReference.cs
- IAM.Application/IAssemblyReference.cs
- Products.Infrastructure/IAssemblyReference.cs
- Products.Application/IAssemblyReference.cs
- Common.Infrastructure/IAssemblyReference.cs
- Notifications.Domain/IAssemblyReference.cs
- IAM.Infrastructure/IAssemblyReference.cs
- .AddProductToMyStoreAsync
- Request
- AuditableEntityResponse
- Response
- .GetAuditLogAsync
- VersionNeutral/Get/Request.cs
- .TryDeserialize
- AuditLogOptions
- .UpdateMyProductAsync
- .RemoveMyProductAsync
- DefaultResponsesOperationFilter
- Request
- Common.InterModuleRequests.Contracts
- .RemoveProductAsync
- GetProductRequest
- Request
- .SendOtp
- SendResponseBody
- .UpdateStoreAsync
- Response
- V1StockLevelCreatedDomainEvent
- .TryReadFromJsonAsync
- Setup.Logger.cs
- ModulesOptions.cs
- RequestBody
- Setup
- .UpdateCurrentPushToken
- Setup
- .AddNetGsm
- .SetRetryAfterHeader
- ICurrentUser
- V1StockReservationReservedDomainEvent
- .AddServices
- .MapEndpoints
- .SeedProductAsync
- StringExtensions
- StringExtensions
- ThrottledSmsGateway
- ValidationContextExtensions
- Response
- .EmailVerificationTokenValidation
- .PhoneNumberValidation
- KeycloakRoles.cs
- InventoryTelemetry
- ProductsTelemetry
- IAM.Endpoints
- Products.Endpoints
- Users/VersionNeutral/Setup.cs
- Setup
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
- Functional Result Pipeline (Railway-Oriented)
- InterModuleRequests (Sync Cross-Module)
- Modular Monolith Architecture
- BackgroundJobs Module
- Compiler-Enforced Module Boundaries
- Outbox Module
- Project Instructions (CLAUDE.md)
- REPR Pattern (Minimal API Endpoints)
- Two-Toolchain Sync Contract
- Transactional Outbox Pattern
- Develop as Monolith, Deploy as Microservices
- ICoreModule vs IModule Tiers
- MassTransitInterModuleRequestClient
- Each Module Owns Its Own DbContext
- GlobalUsings.cs
- Deploy-Time Materialized Config

## God Nodes (most connected - your core abstractions)
1. `Result` - 133 edges
2. `Common.Application.Options` - 125 edges
3. `Common.Domain.ResultMonad` - 103 edges
4. `CustomValidator` - 80 edges
5. `Common.Application.Validation` - 71 edges
6. `ApplicationUserId` - 68 edges
7. `Common.Application.Auth` - 66 edges
8. `Common.Application.Extensions` - 62 edges
9. `Common.Domain.StronglyTypedIds` - 61 edges
10. `Common.InterModuleRequests.Contracts` - 51 edges

## Surprising Connections (you probably didn't know these)
- `Concurrent safety` --references--> `OutboxProcessor`  [INFERRED]
  docs/split-deployment-poc.md → src/Modules/Outbox/Outbox/OutboxProcessor.cs
- `Configuration` --references--> `FullTextSearchOptions`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Application/Options/FullTextSearchOptions.cs
- `Gotchas` --references--> `FullTextSearchOptions`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Application/Options/FullTextSearchOptions.cs
- `Add search to a new entity _(Build checklist)_` --references--> `ISearchLocalized`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Domain/Entities/ISearchLocalized.cs
- `Add search to a new entity _(Build checklist)_` --references--> `ApplySearchLanguageInterceptor`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Infrastructure/Persistence/Auditing/ApplySearchLanguageInterceptor.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Local Infrastructure Stack** — docker_compose_postgres, docker_compose_rabbitmq, docker_compose_redis, docker_compose_aspire_dashboard [EXTRACTED 1.00]

## Communities (371 total, 102 thin omitted)

### Community 0 - "Inventory.Domain.StockReservations.DomainEvents.v1"
Cohesion: 0.07
Nodes (23): Inventory.Domain.StockReservations.DomainEvents.v1, DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationCommitConflictDetectedDomainEvent, DateTimeOffset, DefaultIdType, StockReservationId (+15 more)

### Community 1 - "NotificationPayload"
Cohesion: 0.18
Nodes (13): Notifications.Application.Hubs, IHubContext, CancellationToken, IReadOnlyList, Task, INotificationDispatcher, Task, INotificationsClient (+5 more)

### Community 2 - "OutboxProcessor"
Cohesion: 0.22
Nodes (11): IPublishEndpoint, CancellationToken, Exception, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task (+3 more)

### Community 5 - "KeycloakAdminClient"
Cohesion: 0.14
Nodes (22): HttpRequestMessage, DateOnly, DateTimeOffset, CreateKeycloakUser, KeycloakUser, KeycloakUserPage, KeycloakUserSession, CancellationToken (+14 more)

### Community 6 - "RedisFixedWindowRateLimiter"
Cohesion: 0.13
Nodes (16): RateLimiter, RateLimiterStatistics, FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration, CancellationToken (+8 more)

### Community 7 - "ApplySearchLanguageInterceptor"
Cohesion: 0.17
Nodes (11): Common.Infrastructure.Persistence.Auditing, microsoft_entityframeworkcore_diagnostics, SaveChangesInterceptor, ApplyAuditingInterceptor, TimeProvider, ApplySearchLanguageInterceptor, Setup, IServiceCollection (+3 more)

### Community 8 - "FirebasePushGateway"
Cohesion: 0.14
Nodes (15): FirebaseApp, FirebaseMessaging, IDisposable, IReadOnlyDictionary, IReadOnlyList, PushMessage, CancellationToken, Exception (+7 more)

### Community 9 - ".RequireOtpTemplateForDefaultCulture"
Cohesion: 0.40
Nodes (4): Func, IConfiguration, IEnumerable, OtpTemplateConfiguration

### Community 10 - "Error"
Cohesion: 0.07
Nodes (22): Inventory.Domain.StockReservations.Errors, StringLocalizerExtensions, IStringLocalizer, Error, Key, ParameterName, StatusCode, SubErrors (+14 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.11
Nodes (23): Common.Application.Search, Products.Endpoints.Stores, Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Products.Endpoints.ProductTemplates, Products.Infrastructure.Telemetry, IAM.Endpoints.Tokens.VersionNeutral.Revoke (+15 more)

### Community 12 - "ApplicationUserId"
Cohesion: 0.12
Nodes (15): ApplicationUserId, IsEmpty, Value, DefaultIdType, CancellationToken, IReadOnlyList, Task, IKeycloakAdminClient (+7 more)

### Community 13 - "ISearchLanguageResolver"
Cohesion: 0.11
Nodes (15): Configuration, File map _(Build)_, ISearchLanguageResolver, UniversalConfig, SearchLanguageResolver, UniversalConfig, IOptions, ISearchLocalized (+7 more)

### Community 14 - "UserRepresentation"
Cohesion: 0.07
Nodes (29): Dictionary, List, CredentialRepresentation, Temporary, Type, Value, ErrorRepresentation, Error (+21 more)

### Community 15 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.14
Nodes (14): AuthorizationHandler, AuthorizationHandlerContext, CancellationToken, ClaimsPrincipal, HttpContext, IFusionCache, IHttpContextAccessor, IOptions (+6 more)

### Community 16 - "EmailOptions"
Cohesion: 0.09
Nodes (24): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+16 more)

### Community 17 - ".UseModule"
Cohesion: 0.22
Nodes (7): DashboardContext, IAuthorizationService, IDashboardAsyncAuthorizationFilter, IApplicationBuilder, IOptions, HangfireCustomAuthorizationFilter, Task

### Community 18 - "AuditableEntity"
Cohesion: 0.11
Nodes (18): AuditableEntity, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, Version, DateTimeOffset (+10 more)

### Community 19 - "IntegrationEventHandlerBase"
Cohesion: 0.22
Nodes (12): IConsumer, IntegrationEventHandlerBase, MaxEventAge, CancellationToken, ConsumeContext, DefaultIdType, IFusionCache, ILogger (+4 more)

### Community 20 - "LogProductCatalogChangeHandler"
Cohesion: 0.24
Nodes (8): CancellationToken, DefaultIdType, IFusionCache, ILogger, IOptions, LoggerMessage, Task, LogProductCatalogChangeHandler

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.18
Nodes (7): BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position, Stream

### Community 22 - "DeviceRegistration"
Cohesion: 0.12
Nodes (15): DateTimeOffset, DefaultIdType, Guid, DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive (+7 more)

### Community 23 - "IEmailGateway"
Cohesion: 0.18
Nodes (11): IEmailGateway, CancellationToken, ILogger, LoggerMessage, Task, DummyEmailGateway, IConfiguration, IFusionCache (+3 more)

### Community 24 - "IInterModuleRequestClient"
Cohesion: 0.06
Nodes (33): IClientFactory, IInterModuleRequestClient, CancellationToken, Task, MassTransitInterModuleRequestClient, CancellationToken, IOptions, Task (+25 more)

### Community 25 - "StockReservation"
Cohesion: 0.16
Nodes (12): DateTimeOffset, DefaultIdType, StockReservation, LastReleaseAttemptReference, ProductId, ProviderReference, Quantity, ReservationDeadline (+4 more)

### Community 26 - "IEvent"
Cohesion: 0.12
Nodes (14): DomainEventHandlerBase, CancellationToken, Task, IEventHandler, CancellationToken, Task, IEventHandlerWrapper, CancellationToken (+6 more)

### Community 27 - "RequestResponseBodyLoggingMiddleware"
Cohesion: 0.22
Nodes (7): IDiagnosticContext, HttpContext, IList, IOptions, PathString, RequestDelegate, RequestResponseBodyLoggingMiddleware

### Community 28 - "KeycloakTokenClient"
Cohesion: 0.11
Nodes (24): JsonWebTokenHandler, CancellationToken, Task, IKeycloakTokenClient, DateTimeOffset, KeycloakTokens, CancellationToken, Dictionary (+16 more)

### Community 29 - "Result"
Cohesion: 0.10
Nodes (17): Result, Error, IsFailure, Success, Value, Func, Task, AsyncExtensions (+9 more)

### Community 30 - "Product"
Cohesion: 0.08
Nodes (26): ProductTemplate, ProductTemplateId, Store, StoreId, Product, Description, Language, Name (+18 more)

### Community 31 - "NotificationsModule"
Cohesion: 0.15
Nodes (10): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, NotificationsModule, ActivitySourceNames, MeterNames (+2 more)

### Community 32 - ".SingleAsResult"
Cohesion: 0.18
Nodes (10): CollectionExtensions, Func, ICollection, IEnumerable, PersistenceQueryableExtensions, CancellationToken, Expression, Func (+2 more)

### Community 33 - "DomainEvent"
Cohesion: 0.08
Nodes (22): DomainEvent, CreatedOn, Id, Version, DateTimeOffset, DefaultIdType, ProductId, V1ProductDescriptionUpdatedDomainEvent (+14 more)

### Community 34 - "KeycloakPermissionClient"
Cohesion: 0.12
Nodes (20): CancellationToken, Dictionary, HttpClient, HttpResponseMessage, ILogger, IOptions, IReadOnlyList, List (+12 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.09
Nodes (21): Common.Application.Persistence.Outbox, IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, DateTimeOffset (+13 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.08
Nodes (22): CheckRegistrationRateLimitingPolicy, EmailRateLimitingPolicy, OtpVerifyRateLimitingPolicy, RegisterRateLimitingPolicy, SmsRateLimitingPolicy, CustomRateLimitingOptions, CheckRegistration, CreateStore (+14 more)

### Community 37 - "AuditLogEntryConfiguration"
Cohesion: 0.29
Nodes (4): AuditLogEntryConfiguration, ModelBuilder, ModelBuilder, ModelBuilder

### Community 38 - ".RefreshToken"
Cohesion: 0.15
Nodes (11): IAM.Endpoints.Tokens.VersionNeutral.Refresh, CancellationToken, RouteGroupBuilder, Task, Endpoint, DateTimeOffset, Response, AccessToken (+3 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.08
Nodes (24): IHostBuilder, KeyValuePair, LoggerConfiguration, LoggerMinimumLevelConfiguration, ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl (+16 more)

### Community 40 - "ConfigureSwaggerOptions.cs"
Cohesion: 0.27
Nodes (7): asp_versioning_apiexplorer, Host.Swagger, microsoft_aspnetcore_mvc_apiexplorer, microsoft_openapi, RemoveDefaultResponseSchemaFilter, swashbuckle_aspnetcore_swaggergen, system_text_json_nodes

### Community 41 - "Request"
Cohesion: 0.14
Nodes (14): Guid, Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, Email (+6 more)

### Community 42 - ".SaveWithOutboxAsync"
Cohesion: 0.27
Nodes (9): OutboxSaveHelper, CancellationToken, DbContext, Exception, Func, ILogger, LoggerMessage, Task (+1 more)

### Community 43 - "CreateStoreRateLimitingPolicy"
Cohesion: 0.14
Nodes (12): CancellationToken, Func, HttpContext, IOptions, IProblemDetailsService, IResxLocalizer, OnRejectedContext, RateLimitPartition (+4 more)

### Community 44 - "KeycloakOptions"
Cohesion: 0.14
Nodes (14): KeycloakOptions, AttemptTimeoutSeconds, Authority, BaseUrl, DecisionCacheMaxDurationSeconds, Realm, RequireHttpsMetadata, ResourceClientId (+6 more)

### Community 45 - ".RegisterAsync"
Cohesion: 0.10
Nodes (23): BindDeviceSessionRequest, BindDeviceSessionResponse, Guid, OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts, VerifyPhoneOtpRequest (+15 more)

### Community 46 - ".PaginateAsync"
Cohesion: 0.17
Nodes (11): How it works, One-time setup _(Build, in a migration)_, Query path, Ranking, Write path, PaginationQueryableExtensions, CancellationToken, Expression (+3 more)

### Community 48 - ".AddPersistence"
Cohesion: 0.15
Nodes (11): IDatabaseSeeder, Priority, CancellationToken, Task, CancellationToken, IServiceScopeFactory, Task, ProductsDatabaseSeeder (+3 more)

### Community 50 - "Response"
Cohesion: 0.17
Nodes (12): DateOnly, DateTimeOffset, Response, BirthDate, CreatedOn, Email, Enabled, FirstName (+4 more)

### Community 51 - "Common.Application.DTOs"
Cohesion: 0.12
Nodes (10): Products.Endpoints.Stores.v1.Search, Products.Endpoints.Products.v1.My.Get, Products.Endpoints.Products.v1.Search, Common.Application.DTOs, Products.Endpoints.ProductTemplates.v1.Get, Products.Endpoints.Products.v1.Get, Products.Endpoints.Stores.v1.My.Get, Request (+2 more)

### Community 52 - "ReCaptchaService"
Cohesion: 0.21
Nodes (10): FormUrlEncodedContent, ReCaptchaResponse, CancellationToken, Exception, HttpClient, ILogger, IOptions, LoggerMessage (+2 more)

### Community 53 - "AuditableEntityConfiguration"
Cohesion: 0.14
Nodes (11): IEntityTypeConfiguration, AuditableEntityConfiguration, IntegrationEventConverter, JsonSerializerOptions, ModelBuilder, EntityTypeBuilder, OutboxMessageConfig, ProductConfiguration (+3 more)

### Community 54 - ".SendOtp"
Cohesion: 0.11
Nodes (18): EmailOtpDispatchErrors, EmailOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, SendEmailOtpRequest, SendEmailOtpResponse, CancellationToken (+10 more)

### Community 55 - "BrevoEmailGateway"
Cohesion: 0.19
Nodes (12): SendErrorBody, Exception, HttpClient, HttpStatusCode, ILogger, IOptions, JsonSerializerOptions, LoggerMessage (+4 more)

### Community 56 - "AuditLogRetentionJobRegistrar"
Cohesion: 0.22
Nodes (9): IHostedService, AuditLogRetentionJobRegistrar, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task, Setup (+1 more)

### Community 58 - "OutboxMetricsJob"
Cohesion: 0.13
Nodes (14): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxMetricsJob (+6 more)

### Community 59 - ".SearchProductTemplatesAsync"
Cohesion: 0.15
Nodes (11): Products.Endpoints.ProductTemplates.v1.Search, CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response (+3 more)

### Community 61 - "CaptchaOptions"
Cohesion: 0.16
Nodes (13): CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey, Provider, ScoreThreshold, SecretKey (+5 more)

### Community 62 - "AuditLogEntry"
Cohesion: 0.13
Nodes (14): DatabaseFacade, EntityEntry, IDbContext, AuditLog, ChangeTracker, Database, DbSet, AuditLogEntry (+6 more)

### Community 63 - "RequestLoggingOptions"
Cohesion: 0.11
Nodes (20): IPostConfigureOptions, RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes (+12 more)

### Community 64 - ".SendAsync"
Cohesion: 0.22
Nodes (7): SendResponseBody, CancellationToken, HttpResponseMessage, Task, SendContact, Email, Name

### Community 65 - "BackgroundJobsService"
Cohesion: 0.23
Nodes (8): IBackgroundJobClientV2, BackgroundJobsService, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 66 - "OutboxModule"
Cohesion: 0.13
Nodes (16): Action, Exception, IApplicationBuilder, IEndpointRouteBuilder, IEnumerable, IHostApplicationLifetime, ILogger, ILoggerFactory (+8 more)

### Community 67 - ".AddNotificationsSignalR"
Cohesion: 0.17
Nodes (10): HubConnectionContext, IUserIdProvider, RedisOptions, IConfiguration, IConfigureOptions, IConnectionMultiplexer, IServiceCollection, IUserIdProvider (+2 more)

### Community 68 - "ValueObject"
Cohesion: 0.25
Nodes (4): Common.Domain, IComparable, ValueObject, IEnumerable

### Community 69 - ".HandleAsync"
Cohesion: 0.14
Nodes (14): Cross-process call path, How it works, ICoreModule, GetSeedUserIdsRequest, GetSeedUserIdsResponse, ICollection, CancellationToken, Task (+6 more)

### Community 70 - "IStronglyTypedId"
Cohesion: 0.06
Nodes (32): JsonConverter, StrictDateTimeOffsetJsonConverter, DateTimeOffset, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, StronglyTypedIdListReadOnlyJsonConverter (+24 more)

### Community 71 - "FullTextSearchOptions"
Cohesion: 0.12
Nodes (19): Add a new language/culture, Add search to a new entity _(Build checklist)_, Change the vector (weights, fields, or config), Extending and maintaining, Full-Text Search, Gotchas, Non-goals, Per-entity strategy (+11 more)

### Community 72 - "Response"
Cohesion: 0.22
Nodes (8): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Brand, Color, Model

### Community 73 - ".WriteAsync"
Cohesion: 0.33
Nodes (5): Memory, ReadOnlyMemory, CancellationToken, Task, ValueTask

### Community 75 - "IntegrationEventOutbox"
Cohesion: 0.08
Nodes (24): Common.IntegrationEvents, Products.Application.Products.DomainEventHandlers.v1, Lock, IIntegrationEventOutbox, IntegrationEventOutbox, HasPending, IReadOnlyList, List (+16 more)

### Community 76 - "Response"
Cohesion: 0.22
Nodes (8): RouteGroupBuilder, Endpoint, Response, Address, Description, Name, OwnerId, ProductCount

### Community 77 - ".SaveChangesAsync"
Cohesion: 0.06
Nodes (23): CancellationToken, Task, CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint, CancellationToken (+15 more)

### Community 78 - "CheckRegistrationRateLimitingPolicy"
Cohesion: 0.25
Nodes (8): IRateLimiterPolicy, CancellationToken, Func, IOptions, OnRejectedContext, ValueTask, CheckRegistrationRateLimitingPolicy, OnRejected

### Community 79 - "SeedingCompletionTracker"
Cohesion: 0.15
Nodes (8): SeedingCompletionTracker, CancellationToken, Exception, Task, Setup, IOptions, IServiceCollection, TaskCompletionSource

### Community 80 - "StoreId"
Cohesion: 0.08
Nodes (22): Products.Endpoints.Stores.v1.Update, Products.Endpoints.Stores.v1.Deactivate, StoreId, DefaultIdType, StoreId, RequestBody, Request, Body (+14 more)

### Community 81 - ".SearchStoresAsync"
Cohesion: 0.15
Nodes (12): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Address (+4 more)

### Community 82 - "Split-Deployment PoC"
Cohesion: 0.25
Nodes (6): Concurrent safety, Files added by this PoC, How to run, Split-Deployment PoC, What this proves, README (Boilerplate Overview)

### Community 83 - "NotificationsDbContext"
Cohesion: 0.15
Nodes (13): DbSet, INotificationsDbContext, DeviceRegistrations, DbContextOptions, DbSet, ILogger, TimeProvider, NotificationsDbContext (+5 more)

### Community 84 - ".Configure"
Cohesion: 0.13
Nodes (14): EntityTypeBuilder, EntityTypeBuilder, DomainEventConverter, JsonSerializerOptions, StronglyTypedIdValueConverter, DefaultIdType, EntityTypeBuilder, NpgsqlTsVector (+6 more)

### Community 85 - "ResultTelemetryExtensions.cs"
Cohesion: 0.18
Nodes (5): Activity, ResultTelemetryExtensions, ActivitySource, Task, system_runtime_compilerservices

### Community 86 - "Store"
Cohesion: 0.09
Nodes (19): StoreId, V1StoreAddressUpdatedDomainEvent, StoreId, V1StoreDescriptionUpdatedDomainEvent, StoreId, V1StoreNameUpdatedDomainEvent, IReadOnlyCollection, List (+11 more)

### Community 87 - "PushOptions"
Cohesion: 0.12
Nodes (20): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri, PushOptions (+12 more)

### Community 89 - "Setup"
Cohesion: 0.33
Nodes (4): ConfigurationManager, Host.Configurations, Setup, WebApplicationBuilder

### Community 90 - ".GetProductAsync"
Cohesion: 0.25
Nodes (10): GetStockLevelRequest, GetStockLevelResponse, DefaultIdType, CancellationToken, Task, GetStockLevelRequestHandler, CancellationToken, DefaultIdType (+2 more)

### Community 91 - "RequestBody"
Cohesion: 0.40
Nodes (5): RequestBody, Description, Name, Price, Quantity

### Community 92 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.08
Nodes (15): Products.Domain.Products.DomainEvents.v1, Common.Domain.StronglyTypedIds, Common.Domain.Events, Products.Endpoints.ProductTemplates.v1.Create, Products.Domain.Products, Common.Application.JsonConverters, Common.Domain.Entities, Common.Domain.Aggregates (+7 more)

### Community 93 - ".MapEndpoint"
Cohesion: 0.22
Nodes (6): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, RouteGroupBuilder, Response, ClientKey, RouteGroupBuilder, Setup

### Community 94 - "OtpOptions"
Cohesion: 0.18
Nodes (11): OtpOptions, DummyCode, EmailQuotaWindowMinutes, ExpirationInMinutes, Length, MaxSendsPerEmailPerWindow, MaxSendsPerPhonePerWindow, PhoneQuotaWindowMinutes (+3 more)

### Community 95 - ".SearchMyProductsAsync"
Cohesion: 0.14
Nodes (12): Products.Endpoints.Products.v1.My.Search, CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response (+4 more)

### Community 96 - "DatabaseSeederOrchestrator"
Cohesion: 0.27
Nodes (9): BackgroundService, DatabaseSeederOrchestrator, CancellationToken, Exception, IHostApplicationLifetime, ILogger, IServiceScopeFactory, LoggerMessage (+1 more)

### Community 97 - "StockReservationExpirySweepService"
Cohesion: 0.18
Nodes (11): IServiceCollection, Setup, CancellationToken, Exception, Guid, ILogger, IOptions, LoggerMessage (+3 more)

### Community 98 - ".SearchStoreProductsAsync"
Cohesion: 0.14
Nodes (12): LikePattern, CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response (+4 more)

### Community 99 - "AuditLogRetentionService"
Cohesion: 0.33
Nodes (7): AuditLogRetentionService, CancellationToken, ILogger, IOptions, LoggerMessage, NpgsqlDataSource, Task

### Community 100 - "OutboxDbContext"
Cohesion: 0.14
Nodes (13): DbContext, IOutboxDbContext, OutboxMessages, CancellationToken, DbSet, Task, IConfiguration, IServiceCollection (+5 more)

### Community 101 - "ResiliencyOptions"
Cohesion: 0.11
Nodes (18): HttpStandardResilienceOptions, IHttpClientBuilder, ResiliencyOptions, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds (+10 more)

### Community 102 - "HttpContextTargetingContextAccessor"
Cohesion: 0.20
Nodes (8): ITargetingContextAccessor, HttpContextTargetingContextAccessor, IHttpContextAccessor, ValueTask, Setup, IConfiguration, IServiceCollection, TargetingContext

### Community 103 - "BackgroundJobsTelemetry"
Cohesion: 0.14
Nodes (11): IServerFilter, PerformedContext, PerformingContext, JobMetricsFilter, BackgroundJobsTelemetry, ActivitySource, ConcurrentDictionary, Counter (+3 more)

### Community 104 - "RequestBody"
Cohesion: 0.40
Nodes (5): RequestBody, Description, Name, Price, Quantity

### Community 105 - "Response"
Cohesion: 0.18
Nodes (9): IAM.Endpoints.Users.VersionNeutral.SelfRegisterByEmail, RouteGroupBuilder, Endpoint, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken (+1 more)

### Community 106 - "microsoft_extensions_dependencyinjection"
Cohesion: 0.06
Nodes (30): Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Inventory.Endpoints, Common.Application.Persistence, Outbox, Common.Application.BackgroundJobs, Common.Infrastructure.Persistence.Outbox, Notifications.Domain.Devices (+22 more)

### Community 107 - ".ReleaseStockReservationAsync"
Cohesion: 0.10
Nodes (19): ReleaseResponseBody, CancellationToken, Task, IWarehouseGateway, CancellationToken, RouteGroupBuilder, Task, TimeProvider (+11 more)

### Community 108 - "StockLevel"
Cohesion: 0.24
Nodes (7): DefaultIdType, StockLevel, ProductId, QuantityOnHand, StockLevelId, EntityTypeBuilder, StockLevelConfiguration

### Community 109 - ".IsRegisteredAsync"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, IsRegistered

### Community 110 - "KeycloakPermissionPolicyProvider"
Cohesion: 0.12
Nodes (14): AuthorizationOptions, AuthorizationPolicy, DefaultAuthorizationPolicyProvider, IAuthorizationPolicyProvider, IAuthorizationRequirement, KeycloakPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder (+6 more)

### Community 111 - "AggregateRoot"
Cohesion: 0.15
Nodes (11): AggregateRoot, Events, Id, Version, IReadOnlyCollection, List, IAggregateRoot, Events (+3 more)

### Community 112 - "OutboxCleanupJob"
Cohesion: 0.27
Nodes (8): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxCleanupJob

### Community 113 - "ProductsDbContext"
Cohesion: 0.15
Nodes (12): DbContextOptions, DbSet, ILogger, ProductTemplate, Store, TimeProvider, ProductsDbContext, Products (+4 more)

### Community 114 - "Response"
Cohesion: 0.11
Nodes (17): IAM.Endpoints.Users.VersionNeutral.Me.Get, RouteGroupBuilder, Endpoint, DateOnly, DateTimeOffset, IReadOnlyCollection, Response, BirthDate (+9 more)

### Community 115 - "InterModuleRequestHandler"
Cohesion: 0.24
Nodes (7): IInterModuleRequestHandler, CancellationToken, Task, InterModuleRequestHandler, CancellationToken, ConsumeContext, Task

### Community 116 - ".AddAuthInfrastructure"
Cohesion: 0.12
Nodes (16): IAllowAnonymous, IAuthorizationHandler, IConfigureNamedOptions, HttpContext, HttpStatusCode, IOptions, IProblemDetailsService, IResxLocalizer (+8 more)

### Community 117 - "Response"
Cohesion: 0.10
Nodes (19): ReservationStatus, Active, Committed, Expired, Released, RequiresReconciliation, CancellationToken, RouteGroupBuilder (+11 more)

### Community 118 - "IModule"
Cohesion: 0.09
Nodes (20): OpenTelemetryBuilder, ResourceBuilder, IModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority (+12 more)

### Community 119 - "ProductsModule"
Cohesion: 0.12
Nodes (13): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, ProductsModule (+5 more)

### Community 120 - ".HandleWarehouseWebhookAsync"
Cohesion: 0.15
Nodes (14): IHeaderDictionary, IValidator, CancellationToken, HttpContext, IOptions, JsonSerializerOptions, RouteGroupBuilder, Task (+6 more)

### Community 121 - ".GetVariantAsync"
Cohesion: 0.40
Nodes (4): IVariantFeatureManager, IVariantFeatureManagerExtensions, CancellationToken, Task

### Community 122 - "TokenRefreshRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenRefreshRateLimitingPolicy (+1 more)

### Community 123 - "NotificationsHub"
Cohesion: 0.31
Nodes (6): Hub, Exception, ILogger, LoggerMessage, Task, NotificationsHub

### Community 124 - ".AddCustomHealthChecks"
Cohesion: 0.12
Nodes (14): HealthCheckOptions, EnableHealthChecks, ReadinessTimeoutInSeconds, StartupTimeoutInSeconds, HealthCheckOptionsValidator, IApplicationBuilder, IConfiguration, IConnectionMultiplexer (+6 more)

### Community 125 - ".TapWhenFeatureEnabledAsync"
Cohesion: 0.33
Nodes (5): FeatureFlagResultExtensions, Action, Func, IFeatureManager, Task

### Community 126 - "GlobalExceptionHandlingMiddleware"
Cohesion: 0.20
Nodes (11): IApplicationBuilder, IServiceCollection, Exception, HttpContext, ILogger, IProblemDetailsService, IResxLocalizer, LoggerMessage (+3 more)

### Community 127 - "BaseDbContext"
Cohesion: 0.22
Nodes (8): BaseDbContext, AuditLog, CancellationToken, DbContextOptions, DbSet, ILogger, Task, TimeProvider

### Community 128 - "EmailRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, EmailRateLimitingPolicy (+1 more)

### Community 129 - "RedisOtpService"
Cohesion: 0.32
Nodes (6): CancellationToken, IConnectionMultiplexer, IOptions, Task, TimeSpan, RedisOtpService

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.48
Nodes (7): Observability (OpenTelemetry), docker-compose.yml (Base Stack), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), Postgres Service (mm.postgres), RabbitMQ Service (mm.rabbitmq), Redis Service (mm.redis)

### Community 131 - ".AddInfrastructure"
Cohesion: 0.16
Nodes (12): HostOptions, IMiddleware, JsonOptions, Assembly, IConfiguration, IResxLocalizer, IServiceCollection, IWebHostEnvironment (+4 more)

### Community 132 - "OtpVerifyRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, OtpVerifyRateLimitingPolicy (+1 more)

### Community 134 - "SendRequestBody"
Cohesion: 0.25
Nodes (8): SendContact, IReadOnlyList, SendRequestBody, HtmlContent, Sender, Subject, TextContent, To

### Community 135 - ".ListSessions"
Cohesion: 0.10
Nodes (22): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, IReadOnlyList, CancellationToken, IReadOnlyCollection, RouteGroupBuilder (+14 more)

### Community 136 - ".AddKeycloakInfrastructure"
Cohesion: 0.16
Nodes (10): CancellationToken, Task, IServiceAccountTokenProvider, HttpClient, IOptions, TimeProvider, ServiceAccountTokenProvider, IOptions (+2 more)

### Community 137 - ".WriteTooManyRequestsToResponse"
Cohesion: 0.13
Nodes (14): PartitionedRateLimiter, CancellationToken, Func, HttpContext, IConfiguration, IProblemDetailsService, IReadOnlyList, IResxLocalizer (+6 more)

### Community 138 - "ResultToResponseTransformer.cs"
Cohesion: 0.08
Nodes (30): AspNetResult, Common.Application.EndpointFilters, IEndpointFilter, IFeatureManagerSnapshot, microsoft_aspnetcore_hosting, microsoft_aspnetcore_http_iresult, microsoft_extensions_localization, ResxLocalizer (+22 more)

### Community 139 - "ProductTemplate"
Cohesion: 0.13
Nodes (11): ProductTemplate, IReadOnlyList, List, ProductTemplate, Brand, Color, IsActive, Model (+3 more)

### Community 140 - ".MapEndpoint"
Cohesion: 0.40
Nodes (3): RouteGroupBuilder, RouteGroupBuilder, Setup

### Community 141 - ".GetMeAsync"
Cohesion: 0.19
Nodes (9): CancellationToken, IReadOnlyList, Task, IKeycloakPermissionClient, IReadOnlyList, GrantedPermission, CancellationToken, HttpContext (+1 more)

### Community 142 - "IBackgroundJobs"
Cohesion: 0.26
Nodes (7): IBackgroundJobs, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 143 - "Setup"
Cohesion: 0.10
Nodes (19): LoadAll, microsoft_aspnetcore_httpoverrides, ModuleRegistry, Names, Type, Setup, Assembly, Exception (+11 more)

### Community 144 - "Response"
Cohesion: 0.12
Nodes (16): CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate (+8 more)

### Community 145 - "SecurityHeadersMiddleware"
Cohesion: 0.22
Nodes (7): IAuthenticationSchemeProvider, IApplicationBuilder, HttpContext, IOptions, RequestDelegate, Task, SecurityHeadersMiddleware

### Community 146 - ".RegisterAsync"
Cohesion: 0.07
Nodes (28): IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, VerifyEmailOtpRequest, VerifyEmailOtpResponse, EmailNormalization, CancellationToken, RouteGroupBuilder, Task, Endpoint (+20 more)

### Community 147 - ".AddOrUpdate"
Cohesion: 0.20
Nodes (8): Action, Expression, Func, Task, Action, Expression, Func, Task

### Community 148 - "DeviceRegistryReconciliationService"
Cohesion: 0.09
Nodes (22): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator, IReadOnlyCollection, CancellationToken, ILogger (+14 more)

### Community 149 - "ProductTemplateId"
Cohesion: 0.25
Nodes (6): ProductTemplateId, DefaultIdType, ProductTemplateId, CancellationToken, List, Task

### Community 150 - "StockReservationExpirySweepJobRegistrar"
Cohesion: 0.31
Nodes (7): CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task, StockReservationExpirySweepJobRegistrar

### Community 151 - "OutboxOptions"
Cohesion: 0.11
Nodes (20): OutboxCleanupSettings, BatchSize, CronSchedule, Enabled, RetentionDays, OutboxCleanupSettingsValidator, OutboxOptions, BaseBackoffSeconds (+12 more)

### Community 152 - ".EnsureNoMigrationsPending"
Cohesion: 0.54
Nodes (4): MigrationGuard, ILogger, IServiceProvider, LoggerMessage

### Community 153 - "SwaggerDefaultValues"
Cohesion: 0.33
Nodes (5): IOperationFilter, JsonValue, OpenApiOperation, OperationFilterContext, SwaggerDefaultValues

### Community 154 - "Response"
Cohesion: 0.20
Nodes (8): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, RouteGroupBuilder, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 155 - "SendPhoneOtpRequestHandler"
Cohesion: 0.13
Nodes (14): CancellationToken, Task, ISmsGateway, SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, SmsMessage (+6 more)

### Community 156 - "Request"
Cohesion: 0.15
Nodes (13): Guid, Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FirstName (+5 more)

### Community 157 - "CreateStockLevelOnProductCreatedHandler"
Cohesion: 0.24
Nodes (8): CancellationToken, DefaultIdType, IFusionCache, ILogger, IOptions, LoggerMessage, Task, CreateStockLevelOnProductCreatedHandler

### Community 158 - "OtpService"
Cohesion: 0.18
Nodes (7): OtpCodeGenerator, IFusionCache, IOptions, OtpService, IConfiguration, IServiceCollection, Setup

### Community 159 - "IInventoryDbContext"
Cohesion: 0.13
Nodes (15): DbSet, IInventoryDbContext, StockLevels, StockReservations, DbContextOptions, DbSet, ILogger, TimeProvider (+7 more)

### Community 160 - "OtpServiceBase"
Cohesion: 0.21
Nodes (8): OtpCacheEntry, DateTimeOffset, CancellationToken, IFusionCache, SemaphoreSlim, Task, TimeSpan, OtpServiceBase

### Community 161 - ".ReserveSeriesAsync"
Cohesion: 0.14
Nodes (12): Inventory.Endpoints.StockReservations.v1.ReserveSeries, CancellationToken, IOptions, List, RequestBody, RouteGroupBuilder, Task, TimeProvider (+4 more)

### Community 162 - "ThrottledEmailGateway"
Cohesion: 0.20
Nodes (8): CancellationToken, Task, EmailMessage, CancellationToken, IFusionCache, IOptions, Task, ThrottledEmailGateway

### Community 165 - "CurrentUser.cs"
Cohesion: 0.40
Nodes (3): Common.Infrastructure.Auth.Services, Common.Infrastructure.Auth, system_security_claims

### Community 166 - "IamModule"
Cohesion: 0.18
Nodes (10): Action, IApplicationBuilder, IEnumerable, RateLimiterOptions, IamModule, ActivitySourceNames, MeterNames, Name (+2 more)

### Community 167 - "Common.Application.Pagination"
Cohesion: 0.06
Nodes (38): Products.Endpoints.Stores.v1.AuditLog, IAM.Endpoints.Users.VersionNeutral.Search, Products.Endpoints.Stores.v1.My.AuditLog, Common.Application.Pagination, Products.Endpoints.Products.v1.AuditLog, PaginationRequest, PageNumber, PageSize (+30 more)

### Community 168 - "Response"
Cohesion: 0.29
Nodes (5): RouteGroupBuilder, Setup, RouteGroupBuilder, Response, Id

### Community 169 - "SendSecurityAlertRequestHandler"
Cohesion: 0.27
Nodes (9): SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, CancellationToken, IOptions, RequestLocalizationOptions, Task (+1 more)

### Community 171 - "IOtpService"
Cohesion: 0.24
Nodes (8): CancellationToken, Task, TimeSpan, IOtpService, OtpVerificationOutcome, InvalidOtp, Success, TooManyAttempts

### Community 172 - "ResxLocalizationOptions"
Cohesion: 0.25
Nodes (7): ResxLocalizationOptions, DefaultCulture, SupportedCultures, ResxLocalizationOptionsValidator, ICollection, IApplicationBuilder, IOptions

### Community 173 - "RequestBody"
Cohesion: 0.29
Nodes (7): ProductTemplateId, RequestBody, Description, Name, Price, ProductTemplateId, Quantity

### Community 174 - "FirebasePushGateway.cs"
Cohesion: 0.22
Nodes (6): Notifications.Application.Push, Notifications.Infrastructure.Push, Notifications.Infrastructure.Push.Firebase, firebaseadmin, firebaseadmin_messaging, google_apis_auth_oauth2

### Community 175 - "PaginationResponse"
Cohesion: 0.07
Nodes (26): JsonElement, AuditLogDto, DateTimeOffset, PaginationResponse, HasNext, HasPrevious, NextPageNumber, PreviousPageNumber (+18 more)

### Community 176 - ".FixedWindow"
Cohesion: 0.22
Nodes (7): RateLimitPartitions, HttpContext, IConnectionMultiplexer, ILoggerFactory, RateLimitPartition, HttpContext, RateLimitPartition

### Community 177 - "Request"
Cohesion: 0.18
Nodes (11): StoreId, Request, Description, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name (+3 more)

### Community 178 - ".AddPushServices"
Cohesion: 0.16
Nodes (11): CancellationToken, Task, IPushGateway, CancellationToken, ILogger, LoggerMessage, Task, DummyPushGateway (+3 more)

### Community 179 - "CachingOptions"
Cohesion: 0.09
Nodes (25): CachingEntryDefaults, Duration, FactoryHardTimeout, FactorySoftTimeout, FailSafeMaxDuration, FailSafeThrottleDuration, CachingOptions, AllowInMemoryOnlyInProduction (+17 more)

### Community 180 - "SmsOptions"
Cohesion: 0.10
Nodes (21): SmsOptions, AppName, AttemptTimeoutSeconds, BaseUrl, MaxPerDay, MaxPerPhoneNumberPerDay, MaxRetryAttempts, MsgHeader (+13 more)

### Community 181 - "RabbitMqOptions"
Cohesion: 0.12
Nodes (15): RabbitMqOptions, Host, Password, Port, RetryIntervalDeltaMs, RetryLimit, RetryMaxIntervalMs, RetryMinIntervalMs (+7 more)

### Community 183 - "SendRequestBody"
Cohesion: 0.25
Nodes (8): SendMessageBody, IReadOnlyList, SendRequestBody, AppName, Encoding, IysFilter, Messages, MsgHeader

### Community 184 - "RequestBody"
Cohesion: 0.15
Nodes (14): DateTimeOffset, DefaultIdType, ICollection, RequestBody, FirstDeadline, IntervalDays, OccurrenceCount, ProductId (+6 more)

### Community 185 - "IdentityScheme"
Cohesion: 0.24
Nodes (8): IdentityScheme, Email, PhoneNumber, IdentitySchemeOptions, Scheme, IdentitySchemeOptionsValidator, RouteGroupBuilder, Setup

### Community 186 - "Policies"
Cohesion: 0.25
Nodes (6): CreateStoreRateLimitingPolicy, RateLimiterOptions, Policies, Action, IEnumerable, RateLimiterOptions

### Community 187 - "ReverseProxyOptions"
Cohesion: 0.22
Nodes (8): ForwardedHeadersOptions, ReverseProxyOptions, ForwardLimit, IsEnabled, TrustedNetworks, IReadOnlyList, IConfiguration, IServiceCollection

### Community 188 - "BoundedCaptureStream"
Cohesion: 0.14
Nodes (8): SeekOrigin, HttpResponse, BoundedCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 189 - "V1StoreCreatedDomainEvent"
Cohesion: 0.27
Nodes (8): Products.Application.Stores.DomainEventHandlers.v1, CancellationToken, Task, SimulateSomeBusinessHandler, StoreCreatedIntegrationEventPublishingHandler, V1StoreCreatedDomainEventHandlers, StoreId, V1StoreCreatedDomainEvent

### Community 190 - "ServiceAccountTokenCache"
Cohesion: 0.11
Nodes (11): KeycloakPaths, CancellationToken, DateTimeOffset, SemaphoreSlim, Task, TimeSpan, ServiceAccountTokenCache, AccessToken (+3 more)

### Community 191 - "IInterModuleRequest"
Cohesion: 0.33
Nodes (8): IInterModuleRequest, GetActiveSessionIdsRequest, GetActiveSessionIdsResponse, UserSessionIds, IReadOnlyList, CancellationToken, Task, GetActiveSessionIdsRequestHandler

### Community 192 - "CachedCaptchaService"
Cohesion: 0.29
Nodes (5): CancellationToken, IFusionCache, IOptions, Task, CachedCaptchaService

### Community 193 - "Response"
Cohesion: 0.18
Nodes (9): RouteGroupBuilder, Setup, RouteGroupBuilder, Response, AvailableQuantity, Description, Name, Price (+1 more)

### Community 194 - "CorsOptions"
Cohesion: 0.25
Nodes (8): CorsOptions, AllowCredentials, AllowedHeaders, AllowedMethods, AllowedOrigins, MaxAgeInSeconds, CorsOptionsValidator, IReadOnlyList

### Community 195 - "InventoryOptions"
Cohesion: 0.13
Nodes (15): InventoryOptions, MaxOccurrencesPerSeries, SafetyBufferQuantity, StockReservationExpirySweepBatchSize, StockReservationExpirySweepCron, WarehouseGatewayBaseUrl, WarehouseGatewayProvider, WebhookSharedSecret (+7 more)

### Community 196 - "TokenCreateRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenCreateRateLimitingPolicy (+1 more)

### Community 197 - ".ReserveStockAsync"
Cohesion: 0.22
Nodes (7): Inventory.Endpoints.StockReservations.v1.Reserve, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Id

### Community 198 - "InventoryModule"
Cohesion: 0.15
Nodes (10): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, InventoryModule, ActivitySourceNames, MeterNames (+2 more)

### Community 199 - "OpenApiOptions"
Cohesion: 0.22
Nodes (9): OpenApiOptions, ContactEmail, ContactName, Description, EnableSwagger, LicenseName, LicenseUrl, Title (+1 more)

### Community 200 - "NotificationsTelemetry"
Cohesion: 0.22
Nodes (5): ActivitySource, Counter, Meter, NotificationsTelemetry, UpDownCounter

### Community 201 - "KeycloakScopes"
Cohesion: 0.20
Nodes (9): Devices, Hangfire, KeycloakScopes, Products, ProductTemplates, Sessions, StockReservations, Stores (+1 more)

### Community 202 - "fluentvalidation"
Cohesion: 0.07
Nodes (38): common_application_localization_resources, IAM.Domain.Users, IAM.Endpoints.Otp.VersionNeutral.SendForLogin, Inventory.Endpoints.StockReservations.v1.WebhookCallback, Common.Domain.Extensions, Notifications.Infrastructure.Devices.UpdateCurrentPushToken, Common.Application.Validation, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke (+30 more)

### Community 203 - "SmsRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, SmsRateLimitingPolicy (+1 more)

### Community 205 - "Request"
Cohesion: 0.22
Nodes (9): Guid, Request, ClientId, DeviceId, DeviceName, Otp, PhoneNumber, PushToken (+1 more)

### Community 210 - "ConfigureSwaggerOptions"
Cohesion: 0.28
Nodes (7): ApiVersionDescription, IApiVersionDescriptionProvider, IConfigureOptions, OpenApiInfo, IOptions, SwaggerGenOptions, ConfigureSwaggerOptions

### Community 211 - "EventDispatcher"
Cohesion: 0.31
Nodes (7): EventDispatcher, ActivitySource, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task

### Community 212 - "Setup"
Cohesion: 0.33
Nodes (4): ApiVersionSet, Setup, IEndpointRouteBuilder, IServiceCollection

### Community 213 - ".SendOtp"
Cohesion: 0.20
Nodes (7): CancellationToken, Task, CancellationToken, IFeatureManager, RouteGroupBuilder, Task, Endpoint

### Community 214 - "Request"
Cohesion: 0.20
Nodes (10): Guid, Request, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken, Password (+2 more)

### Community 215 - "IProductsDbContext"
Cohesion: 0.08
Nodes (20): DbSet, Store, IProductsDbContext, Products, ProductTemplates, Stores, CancellationToken, Task (+12 more)

### Community 216 - "microsoft_aspnetcore_mvc"
Cohesion: 0.05
Nodes (47): Products.Endpoints.Products.v1.My.Update, Products.Endpoints.ProductTemplates.v1.Deactivate, Products.Endpoints.Probe.v1, Products.Endpoints.Stores.v1.My.RemoveProduct, Inventory.Endpoints.StockReservations.v1.Release, Products.Endpoints.Products.v1.Update, Inventory.Endpoints.StockReservations.v1.Get, Common.Application.ModelBinders (+39 more)

### Community 217 - "FeatureFlags"
Cohesion: 0.33
Nodes (5): Checkout, FeatureFlags, IAM, Notifications, Products

### Community 218 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 219 - "Stores/v1/Create/Request.cs"
Cohesion: 0.25
Nodes (7): Products.Endpoints.Stores.v1.Create, Request, Address, Description, Name, OwnerId, RequestValidator

### Community 220 - "FixedWindow"
Cohesion: 0.29
Nodes (7): CustomRateLimitingOptionsValidator, FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit, FixedWindowValidator

### Community 221 - "Keycloak realm as code"
Cohesion: 0.25
Nodes (7): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Secrets, Seed users (dev / test only)

### Community 222 - "Key decisions"
Cohesion: 0.29
Nodes (7): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Key decisions

### Community 223 - "Products.Domain.Stores"
Cohesion: 0.07
Nodes (28): Products.Endpoints.Stores.v1.My.Update, Products.Infrastructure.Persistence.Seeding, Products.Endpoints.Stores.v1.My.Create, Products.Endpoints.Stores.v1.Get, Products.Domain.Stores, Products.Endpoints.Stores.v1.RemoveProduct, Constants, Request (+20 more)

### Community 224 - ".MapEndpoint"
Cohesion: 0.40
Nodes (3): RouteGroupBuilder, RouteGroupBuilder, Setup

### Community 225 - "BackgroundJobsOptions"
Cohesion: 0.29
Nodes (7): BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount, BackgroundJobsOptionsValidator

### Community 226 - "NetGsmSmsGateway"
Cohesion: 0.16
Nodes (13): CancellationToken, Exception, HttpClient, ILogger, IOptions, JsonSerializerOptions, LoggerMessage, SendRequestBody (+5 more)

### Community 227 - "UtcDateTimeOffsetConverter"
Cohesion: 0.22
Nodes (6): DateTimeOffset, ModelConfigurationBuilder, UtcDateTimeOffsetConverter, DateTimeOffset, DateTimeOffset, ModelConfigurationBuilder

### Community 228 - "StronglyTypedIdBinder"
Cohesion: 0.40
Nodes (4): IModelBinder, ModelBindingContext, StronglyTypedIdBinder, Task

### Community 229 - ".AddCommonOptions"
Cohesion: 0.40
Nodes (4): Setup, IConfiguration, IHostEnvironment, IServiceCollection

### Community 230 - "Inventory.Domain.StockReservations"
Cohesion: 0.18
Nodes (9): Products.Infrastructure.InterModuleRequestHandlers, Inventory.Infrastructure.InterModuleRequestHandlers, Inventory.Application.Persistence, Inventory.Application.IntegrationEventHandlers, Inventory.Domain.StockReservations, Inventory.Infrastructure.Telemetry, Common.InterModuleRequests.Inventory, Common.InterModuleRequests.Products (+1 more)

### Community 231 - "ProductsModule.cs"
Cohesion: 0.25
Nodes (6): asp_versioning, asp_versioning_builder, asp_versioning_conventions, Common.Endpoints.Versioning, Products.Endpoints.Probe, Products.Endpoints.Products

### Community 232 - "Common.Application.Options"
Cohesion: 0.04
Nodes (53): Common.Infrastructure.Modules, IAM.Endpoints.Captcha.VersionNeutral, Notifications.Application.Sms, Notifications.Infrastructure.Devices, Notifications.Infrastructure.Email, Host, Common.Application.Caching, Common.Infrastructure.RateLimiting (+45 more)

### Community 233 - "ReCaptchaResponse"
Cohesion: 0.25
Nodes (7): DateTime, ReCaptchaResponse, ChallengeTs, ErrorCodes, Hostname, Score, Success

### Community 234 - ".AddCustomSwagger"
Cohesion: 0.20
Nodes (7): IApplicationBuilder, IConfigureOptions, IServiceCollection, IWebHostEnvironment, SwaggerGenOptions, Type, Setup

### Community 235 - ".AddServices"
Cohesion: 0.29
Nodes (7): RecurringJobOptions, IRecurringBackgroundJobs, IConfiguration, IServiceCollection, RecurringBackgroundJobsService, IRecurringJobManagerV2, TimeProvider

### Community 236 - "IssueVerificationTokenRequestHandler"
Cohesion: 0.39
Nodes (6): IssueVerificationTokenRequest, IssueVerificationTokenResponse, CancellationToken, IOptions, Task, IssueVerificationTokenRequestHandler

### Community 237 - "ProblemDetailsExtensions.cs"
Cohesion: 0.40
Nodes (3): ProblemDetails, ProblemDetailsExtensions, ICollection

### Community 238 - "Common.Infrastructure.Persistence.ValueConverters"
Cohesion: 0.27
Nodes (7): Inventory.Infrastructure.Persistence.EntityConfigurations, Common.Infrastructure.Persistence.EntityConfigurations, Common.Infrastructure.Persistence.ValueConverters, Notifications.Infrastructure.Persistence.EntityConfigurations, Products.Infrastructure.Persistence.EntityConfigurations, microsoft_entityframeworkcore_metadata_builders, npgsqltypes

### Community 239 - "KeycloakTokenClient.cs"
Cohesion: 0.09
Nodes (14): IAM.Infrastructure.Keycloak.Representations, Notifications.Infrastructure.Email.Brevo, Inventory.Infrastructure.Gateway, Notifications.Application.Email, Inventory.Application.Gateway, IAM.Domain.Errors, IAM.Infrastructure.Keycloak, entityframework_exceptions_common (+6 more)

### Community 240 - "BackgroundJobsModule"
Cohesion: 0.25
Nodes (7): BackgroundJobsModule, ActivitySourceNames, MeterNames, Name, StartupPriority, IEndpointRouteBuilder, IEnumerable

### Community 241 - "RegisterRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, RegisterRateLimitingPolicy (+1 more)

### Community 242 - "StronglyTypedIdSchemaFilter.cs"
Cohesion: 0.33
Nodes (4): IOpenApiSchema, ISchemaFilter, SchemaFilterContext, StronglyTypedIdSchemaFilter

### Community 243 - "CustomValidator"
Cohesion: 0.05
Nodes (49): AbstractValidator, Inventory.Endpoints.StockReservations.v1.Commit, DatabaseOptions, ConnectionString, DatabaseOptionsValidator, InterModuleRequestOptions, TimeoutSeconds, InterModuleRequestOptionsValidator (+41 more)

### Community 244 - "ICaptchaService"
Cohesion: 0.36
Nodes (4): ICaptchaService, IConfiguration, IServiceCollection, Setup

### Community 246 - "system_diagnostics"
Cohesion: 0.08
Nodes (18): BackgroundJobs.Telemetry, BackgroundJobs, IAM.Infrastructure.Auth, Notifications.Infrastructure.Hubs, hangfire, hangfire_annotations, hangfire_dashboard, hangfire_postgresql (+10 more)

### Community 248 - "SendErrorBody"
Cohesion: 0.67
Nodes (3): SendErrorBody, Code, Message

### Community 249 - "Infrastructure/Setup.cs"
Cohesion: 0.25
Nodes (6): Common.InterModuleRequests, Common.Infrastructure.Localization, Common.Infrastructure.Caching, microsoft_aspnetcore_authentication, microsoft_aspnetcore_http_json, IAssemblyReference

### Community 250 - "Response"
Cohesion: 0.25
Nodes (7): RouteGroupBuilder, Endpoint, Response, Description, Name, Price, Quantity

### Community 251 - ".DeactivateStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 252 - ".TryWriteAsync"
Cohesion: 0.40
Nodes (4): ProblemDetailsContext, ProblemDetailsServiceExtensions, IProblemDetailsService, Task

### Community 260 - ".AddProductToMyStoreAsync"
Cohesion: 0.18
Nodes (9): Products.Endpoints.Stores.v1.My.AddProduct, CancellationToken, ProductTemplate, RouteGroupBuilder, Store, Task, Endpoint, Response (+1 more)

### Community 261 - "Request"
Cohesion: 0.25
Nodes (8): Request, Description, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name, SearchTerm

### Community 262 - "AuditableEntityResponse"
Cohesion: 0.29
Nodes (7): AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, DateTimeOffset

### Community 263 - "Response"
Cohesion: 0.25
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegister, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 264 - ".GetAuditLogAsync"
Cohesion: 0.38
Nodes (5): DbContextExtensions, CancellationToken, DbSet, JsonSerializerOptions, Task

### Community 265 - "VersionNeutral/Get/Request.cs"
Cohesion: 0.40
Nodes (4): IAM.Endpoints.Users.VersionNeutral.Get, Request, Id, RequestValidator

### Community 267 - "AuditLogOptions"
Cohesion: 0.33
Nodes (6): AuditLogOptions, PerSchemaRetentionDays, PurgeBatchSize, RetentionDays, AuditLogOptionsValidator, Dictionary

### Community 268 - ".UpdateMyProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 269 - ".RemoveMyProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 270 - "DefaultResponsesOperationFilter"
Cohesion: 0.70
Nodes (3): OpenApiOperation, OperationFilterContext, DefaultResponsesOperationFilter

### Community 271 - "Request"
Cohesion: 0.25
Nodes (8): ProductTemplateId, Request, Description, Name, Price, ProductTemplateId, Quantity, RequestValidator

### Community 272 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.09
Nodes (22): Notifications.Application.Otp, IAM.Endpoints.Users, Common.Application.FeatureManagement, IAM.Endpoints.Otp, Notifications.Application.Persistence, Notifications.Infrastructure.Telemetry, Common.InterModuleRequests.IAM, Common.InterModuleRequests.Contracts (+14 more)

### Community 273 - ".RemoveProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 274 - "GetProductRequest"
Cohesion: 0.39
Nodes (6): GetProductRequest, GetProductResponse, DefaultIdType, CancellationToken, Task, GetProductRequestHandler

### Community 275 - "Request"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForEmail, Request, CaptchaToken, Email, RequestValidator

### Community 276 - ".SendOtp"
Cohesion: 0.16
Nodes (12): OtpDispatchErrors, SendPhoneOtpRequest, SendPhoneOtpResponse, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, CancellationToken (+4 more)

### Community 277 - "SendResponseBody"
Cohesion: 0.50
Nodes (4): SendResponseBody, Code, Description, JobId

### Community 278 - ".UpdateStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 279 - "Response"
Cohesion: 0.25
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.Create, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 280 - "V1StockLevelCreatedDomainEvent"
Cohesion: 0.40
Nodes (4): Inventory.Domain.StockLevels.DomainEvents.v1, DefaultIdType, StockLevelId, V1StockLevelCreatedDomainEvent

### Community 281 - ".TryReadFromJsonAsync"
Cohesion: 0.40
Nodes (4): HttpContent, CancellationToken, Task, HttpContentJsonExtensions

### Community 282 - "Setup.Logger.cs"
Cohesion: 0.22
Nodes (8): elastic_serilog_sinks, serilog_configuration, serilog_enrichers_span, serilog_events, serilog_exceptions, serilog_formatting_compact, serilog_sinks_opentelemetry, serilog_sinks_systemconsole_themes

### Community 283 - "ModulesOptions.cs"
Cohesion: 0.50
Nodes (4): ModulesOptions, EnabledModules, ModulesOptionsValidator, IReadOnlyList

### Community 284 - "RequestBody"
Cohesion: 0.33
Nodes (6): DateTimeOffset, DefaultIdType, RequestBody, ProductId, Quantity, ReservationDeadline

### Community 286 - ".UpdateCurrentPushToken"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 288 - ".AddNetGsm"
Cohesion: 0.18
Nodes (10): CancellationToken, ILogger, LoggerMessage, Task, DummySmsGateway, IConfiguration, IFusionCache, IOptions (+2 more)

### Community 289 - ".SetRetryAfterHeader"
Cohesion: 0.40
Nodes (4): RetryAfterHeaderExtensions, HttpResponse, RateLimitLease, TimeSpan

### Community 290 - "ICurrentUser"
Cohesion: 0.06
Nodes (26): ICurrentUser, Id, IdAsString, Roles, SessionId, ICollection, CurrentUser, Id (+18 more)

### Community 291 - "V1StockReservationReservedDomainEvent"
Cohesion: 0.40
Nodes (4): DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationReservedDomainEvent

### Community 294 - ".SeedProductAsync"
Cohesion: 0.22
Nodes (7): CancellationToken, Task, CancellationToken, List, ProductTemplateId, StoreId, Task

### Community 297 - "ThrottledSmsGateway"
Cohesion: 0.29
Nodes (5): CancellationToken, IFusionCache, IOptions, Task, ThrottledSmsGateway

### Community 299 - "Response"
Cohesion: 0.29
Nodes (5): RouteGroupBuilder, Setup, RouteGroupBuilder, Response, Id

### Community 300 - ".EmailVerificationTokenValidation"
Cohesion: 0.50
Nodes (3): IResxLocalizer, IRuleBuilder, IRuleBuilderOptions

### Community 301 - ".PhoneNumberValidation"
Cohesion: 0.50
Nodes (3): IResxLocalizer, IRuleBuilder, IRuleBuilderOptions

### Community 303 - "InventoryTelemetry"
Cohesion: 0.50
Nodes (4): ActivitySource, Counter, Meter, InventoryTelemetry

### Community 304 - "ProductsTelemetry"
Cohesion: 0.50
Nodes (4): ActivitySource, Counter, Meter, ProductsTelemetry

## Knowledge Gaps
- **892 isolated node(s):** `Id`, `IdAsString`, `Roles`, `SessionId`, `JwtClaimNames` (+887 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 2082 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **102 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Result` connect `Result` to `.AddProductToMyStoreAsync`, `.ListSessions`, `.GetAuditLogAsync`, `FirebasePushGateway`, `Error`, `ProductTemplate`, `ApplicationUserId`, `.GetMeAsync`, `.UpdateMyProductAsync`, `.RemoveMyProductAsync`, `Response`, `.RemoveProductAsync`, `.RegisterAsync`, `.SendOtp`, `.UpdateStoreAsync`, `IEmailGateway`, `IInterModuleRequestClient`, `StockReservation`, `SendPhoneOtpRequestHandler`, `KeycloakTokenClient`, `.UpdateCurrentPushToken`, `.SingleAsResult`, `.ReserveSeriesAsync`, `ThrottledEmailGateway`, `.AddNetGsm`, `DomainEvent`, `ICurrentUser`, `.RefreshToken`, `ThrottledSmsGateway`, `.RegisterAsync`, `PaginationResponse`, `.AddPushServices`, `ReCaptchaService`, `.SendOtp`, `BrevoEmailGateway`, `.SearchProductTemplatesAsync`, `CachedCaptchaService`, `.SendAsync`, `.ReserveStockAsync`, `Response`, `.SaveChangesAsync`, `.SearchStoresAsync`, `ResultTelemetryExtensions.cs`, `.SendOtp`, `IProductsDbContext`, `.GetProductAsync`, `Response`, `.SearchMyProductsAsync`, `NetGsmSmsGateway`, `.SearchStoreProductsAsync`, `.ReleaseStockReservationAsync`, `.IsRegisteredAsync`, `ICaptchaService`, `Response`, `.HandleWarehouseWebhookAsync`, `.DeactivateStoreAsync`, `.TapWhenFeatureEnabledAsync`?**
  _High betweenness centrality (0.157) - this node is a cross-community bridge._
- **Why does `Common.Application.Options` connect `Common.Application.Options` to `ApplySearchLanguageInterceptor`, `AuditLogOptions`, `Common.Domain.ResultMonad`, `Setup`, `EmailOptions`, `Common.InterModuleRequests.Contracts`, `DeviceRegistryReconciliationService`, `OutboxOptions`, `Setup.Logger.cs`, `ModulesOptions.cs`, `ObservabilityOptions`, `ConfigureSwaggerOptions.cs`, `KeycloakOptions`, `ResxLocalizationOptions`, `FirebasePushGateway.cs`, `CachingOptions`, `SmsOptions`, `RabbitMqOptions`, `Users/VersionNeutral/Setup.cs`, `IdentityScheme`, `CaptchaOptions`, `RequestLoggingOptions`, `CorsOptions`, `InventoryOptions`, `FullTextSearchOptions`, `OpenApiOptions`, `fluentvalidation`, `PushOptions`, `FixedWindow`, `OtpOptions`, `BackgroundJobsOptions`, `ResiliencyOptions`, `Inventory.Domain.StockReservations`, `ProductsModule.cs`, `microsoft_extensions_dependencyinjection`, `Common.Infrastructure.Persistence.ValueConverters`, `KeycloakTokenClient.cs`, `CustomValidator`, `system_diagnostics`, `Infrastructure/Setup.cs`, `.AddCustomHealthChecks`?**
  _High betweenness centrality (0.140) - this node is a cross-community bridge._
- **Why does `Common.Domain.ResultMonad` connect `Common.Domain.ResultMonad` to `Inventory.Domain.StockReservations`, `Common.Application.Options`, `ResultToResponseTransformer.cs`, `Error`, `microsoft_extensions_dependencyinjection`, `FirebasePushGateway.cs`, `KeycloakTokenClient.cs`, `Common.InterModuleRequests.Contracts`, `.SendOtp`, `ResultTelemetryExtensions.cs`, `.SendOtp`, `SendPhoneOtpRequestHandler`, `Common.Domain.StronglyTypedIds`, `Result`?**
  _High betweenness centrality (0.068) - this node is a cross-community bridge._
- **What connects `Id`, `IdAsString`, `Roles` to the rest of the system?**
  _892 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Inventory.Domain.StockReservations.DomainEvents.v1` be split into smaller, more focused modules?**
  _Cohesion score 0.06896551724137931 - nodes in this community are weakly interconnected._
- **Should `KeycloakAdminClient` be split into smaller, more focused modules?**
  _Cohesion score 0.13821138211382114 - nodes in this community are weakly interconnected._
- **Should `RedisFixedWindowRateLimiter` be split into smaller, more focused modules?**
  _Cohesion score 0.13333333333333333 - nodes in this community are weakly interconnected._