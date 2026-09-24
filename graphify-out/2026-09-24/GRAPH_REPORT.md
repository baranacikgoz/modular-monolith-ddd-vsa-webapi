# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-09-24)

## Corpus Check
- 566 files · ~87,530 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 4847 nodes · 9562 edges · 369 communities (274 shown, 95 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 257 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `13ecb5a1`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- IStronglyTypedId
- NotificationPayload
- OutboxProcessor
- microsoft_entityframeworkcore
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
- Product
- StockReservation
- IntegrationEventHandlerBase
- LogProductCatalogChangeHandler
- BoundedRequestCaptureStream
- DeviceRegistration
- IEmailGateway
- IInterModuleRequestClient
- Common.Infrastructure.Extensions
- IEvent
- RequestResponseBodyLoggingMiddleware
- AuditLogEntry
- Result
- JobRow
- NotificationsModule
- PersistenceQueryableExtensions
- AggregateRoot
- KeycloakPermissionClient
- OutboxMessage
- CustomRateLimitingOptions
- ICaptchaService
- .RefreshToken
- ObservabilityOptions
- Host.Swagger
- Request
- .SaveWithOutboxAsync
- CreateStoreRateLimitingPolicy
- KeycloakOptions
- InterModuleRequestHandler
- PaginationQueryableExtensions
- Outbox Misuse Check
- IDatabaseSeeder
- Add Integration Event Command
- Response
- Products.Domain.Products
- ReCaptchaService
- UtcDateTimeOffsetConverter
- .SendOtp
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
- .HandleAsync
- StrictDateTimeOffsetJsonConverter
- FullTextSearchOptions
- Response
- .WriteAsync
- AsNoTracking Coverage Check
- IntegrationEventOutbox
- Response
- IProductsDbContext
- CheckRegistrationRateLimitingPolicy
- Common.Domain.Events
- Stores/v1/Get/Request.cs
- .SearchStoresAsync
- Split-Deployment PoC
- NotificationsDbContext
- Common.Infrastructure.Persistence.EntityConfigurations
- ResultTelemetryExtensions.cs
- Store
- PushOptions
- Configuration-Driven Module Registration
- Program.cs
- AuditableEntityResponse
- Seeder
- Common.Domain.StronglyTypedIds
- .MapEndpoint
- KeyedResiliencePipelines
- .SearchMyProductsAsync
- DatabaseSeederOrchestrator
- StockReservationExpirySweepService
- .SearchStoreProductsAsync
- AuditLogRetentionService
- OutboxDbContext
- ResiliencyOptions
- HttpContextTargetingContextAccessor
- InboxCleanupJob
- Products/v1/Update/Request.cs
- Response
- Common.InterModuleRequests.Contracts
- HttpWarehouseGateway
- StockLevel
- .IsRegisteredAsync
- KeycloakPermissionPolicyProvider
- .ReleaseStockReservationAsync
- OutboxCleanupJob
- ProductsDbContext
- Response
- CaptchaOptions
- .AddAuthInfrastructure
- Response
- IModule
- ProductsModule
- .HandleWarehouseWebhookAsync
- .GetVariantAsync
- OtpVerifyRateLimitingPolicy
- NotificationsHub
- .AddCustomHealthChecks
- .TapWhenFeatureEnabledAsync
- .HandleExceptionAsync
- BaseDbContext
- EmailRateLimitingPolicy
- .SendAsync
- docker-compose.yml (Base Stack)
- .AddInfrastructure
- Stores/v1/Update/Request.cs
- Consumer Idempotency (IntegrationEventHandlerBase)
- SendRequestBody
- .ListSessions
- .GetMeAsync
- .WriteTooManyRequestsToResponse
- .From
- ProductTemplate
- .MapEndpoint
- IntegrationEvent
- TokenRefreshRateLimitingPolicy
- Setup
- Response
- StoreId
- .RegisterAsync
- .AddServices
- IInterModuleRequest
- ProductTemplateId
- StockReservationExpirySweepJobRegistrar
- OutboxOptions
- .EnsureNoMigrationsPending
- SwaggerDefaultValues
- .CreateTokensByEmail
- ISmsGateway
- InboxStore
- CreateStockLevelOnProductCreatedHandler
- RedisOtpService
- IInventoryDbContext
- OtpServiceBase
- .ReserveSeriesAsync
- ThrottledEmailGateway
- For
- .AddKeycloakInfrastructure
- KeycloakPermissionAuthorizationHandler.cs
- IamModule
- common_application_localization_resources
- Response
- SendSecurityAlertRequestHandler
- Notifications.Application/IAssemblyReference.cs
- IBackgroundJobs
- fluentvalidation
- RequestBody
- system_diagnostics
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
- JobClaimExtensions
- ServiceAccountTokenCache
- My/AddProduct/Request.cs
- KeycloakPermission
- system_globalization
- CorsOptions
- InventoryOptions
- TokenCreateRateLimitingPolicy
- .ReserveStockAsync
- InventoryModule
- OpenApiOptions
- NotificationsTelemetry
- KeycloakScopes
- VersionNeutral/Create/Request.cs
- SmsRateLimitingPolicy
- Configuration-Driven Module Loading
- .UseModules
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- ConfigureSwaggerOptions
- EventDispatcher
- Setup
- .SendOtp
- CreateByEmail/Request.cs
- .CreateMyStoreAsync
- OtpOptions
- FeatureFlags
- Response
- Response
- .CleanupAsync
- Keycloak realm as code
- Key decisions
- microsoft_aspnetcore_mvc
- .MapEndpoint
- ProcessedMessage
- NetGsmSmsGateway
- RegisterRateLimitingPolicy
- .UpsertIfNewerAsync
- .AddCommonOptions
- Versioning/Setup.cs
- .UseModule
- system_linq_expressions
- SendForEmail/Request.cs
- Setup
- MassTransitInterModuleRequestClient
- IOtpService
- ProblemDetailsExtensions.cs
- SecurityHeadersMiddleware
- IAM.Application.Keycloak
- InterModuleRequestOptions
- .DeactivateStoreAsync
- .AddCustomSwagger
- CustomValidator
- KeyedResilienceProfile
- ReCaptchaResponse
- BackgroundJobsTelemetry
- .Capture
- SendErrorBody
- .UpdateStoreAsync
- Response
- Products/v1/My/Update/Request.cs
- .TryWriteAsync
- Products.Domain/IAssemblyReference.cs
- IAM.Application/IAssemblyReference.cs
- Products.Infrastructure/IAssemblyReference.cs
- Products.Application/IAssemblyReference.cs
- Common.Infrastructure/IAssemblyReference.cs
- Notifications.Domain/IAssemblyReference.cs
- IAM.Infrastructure/IAssemblyReference.cs
- BackgroundJobsOptions
- v1/Request.cs
- .SendWithPipelineAsync
- Response
- GlobalExceptionHandlingMiddleware
- BackgroundJobsModule
- Request
- AuditLogOptions
- DevicesOptions
- InterModuleRequestHandlerDefinition
- DefaultResponsesOperationFilter
- PublishOutcome
- .AddResilientHttpClient
- .AddNetGsm
- GetProductRequest
- .GetAuditLogAsync
- .CommitStockReservationAsync
- SendResponseBody
- ProductTemplates/v1/Get/Request.cs
- Response
- EnrichLogsWithUserInfoMiddleware
- .TryReadFromJsonAsync
- Setup.Logger.cs
- ResiliencyOptions.cs
- Reserve/Request.cs
- Setup
- Common.Application.Options
- Setup
- DummySmsGateway
- .SetRetryAfterHeader
- ICurrentUser
- ConcurrencyConfiguratorExtensions.cs
- .AddServices
- .MapEndpoints
- .SeedProductAsync
- RemoveDefaultResponseSchemaFilter
- StringExtensions
- SignalROptions
- .AddCommonCaching
- InventoryTelemetry
- ProductsTelemetry
- IAM.Endpoints
- Products.Endpoints
- Notifications.Infrastructure
- WarehouseGatewayProvider
- .AddStockReservationExpirySweep
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
1. `Result` - 135 edges
2. `Common.Application.Options` - 133 edges
3. `Common.Domain.ResultMonad` - 105 edges
4. `CustomValidator` - 82 edges
5. `Common.Application.Validation` - 73 edges
6. `ApplicationUserId` - 70 edges
7. `Common.Application.Auth` - 66 edges
8. `Common.Domain.StronglyTypedIds` - 66 edges
9. `Common.Application.Extensions` - 62 edges
10. `Common.InterModuleRequests.Contracts` - 53 edges

## Surprising Connections (you probably didn't know these)
- `Concurrent safety` --references--> `OutboxProcessor`  [INFERRED]
  docs/split-deployment-poc.md → src/Modules/Outbox/Outbox/OutboxProcessor.cs
- `Configuration` --references--> `FullTextSearchOptions`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Application/Options/FullTextSearchOptions.cs
- `Gotchas` --references--> `FullTextSearchOptions`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Application/Options/FullTextSearchOptions.cs
- `File map _(Build)_` --references--> `ISearchLanguageResolver`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Application/Search/ISearchLanguageResolver.cs
- `Add search to a new entity _(Build checklist)_` --references--> `ApplySearchLanguageInterceptor`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Infrastructure/Persistence/Auditing/ApplySearchLanguageInterceptor.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Local Infrastructure Stack** — docker_compose_postgres, docker_compose_rabbitmq, docker_compose_redis, docker_compose_aspire_dashboard [EXTRACTED 1.00]

## Communities (369 total, 95 thin omitted)

### Community 0 - "IStronglyTypedId"
Cohesion: 0.14
Nodes (13): StronglyTypedIdReadOnlyJsonConverter, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, StronglyTypedIdWriteOnlyJsonConverter, JsonSerializerOptions, Type (+5 more)

### Community 1 - "NotificationPayload"
Cohesion: 0.18
Nodes (13): Notifications.Application.Hubs, IHubContext, CancellationToken, IReadOnlyList, Task, INotificationDispatcher, Task, INotificationsClient (+5 more)

### Community 2 - "OutboxProcessor"
Cohesion: 0.21
Nodes (12): IPublishEndpoint, PublishOutcome, CancellationToken, Exception, ILogger, IOptions, IServiceScopeFactory, LoggerMessage (+4 more)

### Community 3 - "microsoft_entityframeworkcore"
Cohesion: 0.06
Nodes (30): Common.Infrastructure.Persistence.Inbox, Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Products.Infrastructure.InterModuleRequestHandlers, Products.Infrastructure.Persistence.Seeding, Common.Application.Persistence.Inbox, Inventory.Infrastructure.InterModuleRequestHandlers, Inventory.Application.Persistence (+22 more)

### Community 5 - "KeycloakAdminClient"
Cohesion: 0.14
Nodes (22): DateOnly, DateTimeOffset, CreateKeycloakUser, KeycloakUser, KeycloakUserPage, KeycloakUserSession, CancellationToken, Error (+14 more)

### Community 6 - "RedisFixedWindowRateLimiter"
Cohesion: 0.13
Nodes (16): RateLimiter, RateLimiterStatistics, FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration, CancellationToken (+8 more)

### Community 7 - "ApplySearchLanguageInterceptor"
Cohesion: 0.12
Nodes (14): SaveChangesInterceptor, ApplyAuditingInterceptor, CancellationToken, DbContextEventData, InterceptionResult, TimeProvider, ValueTask, ApplySearchLanguageInterceptor (+6 more)

### Community 8 - "FirebasePushGateway"
Cohesion: 0.17
Nodes (12): FirebaseApp, FirebaseMessaging, IDisposable, CancellationToken, Exception, IEnumerable, ILogger, IReadOnlyList (+4 more)

### Community 9 - ".RequireOtpTemplateForDefaultCulture"
Cohesion: 0.40
Nodes (4): Func, IConfiguration, IEnumerable, OtpTemplateConfiguration

### Community 10 - "Error"
Cohesion: 0.07
Nodes (22): StringLocalizerExtensions, IStringLocalizer, Error, Key, ParameterName, StatusCode, SubErrors, Value (+14 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.10
Nodes (28): Common.Application.Search, Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Products.Endpoints.ProductTemplates, Common.Application.EndpointFilters, Products.Infrastructure.Telemetry, IAM.Endpoints.Tokens.VersionNeutral.Revoke (+20 more)

### Community 12 - "ApplicationUserId"
Cohesion: 0.16
Nodes (14): ApplicationUserId, IsEmpty, Value, DefaultIdType, CancellationToken, IReadOnlyList, Task, IKeycloakAdminClient (+6 more)

### Community 13 - "ISearchLanguageResolver"
Cohesion: 0.16
Nodes (10): Configuration, ISearchLanguageResolver, UniversalConfig, SearchLanguageResolver, UniversalConfig, IOptions, Setup, IApplicationBuilder (+2 more)

### Community 14 - "UserRepresentation"
Cohesion: 0.07
Nodes (29): Dictionary, List, CredentialRepresentation, Temporary, Type, Value, ErrorRepresentation, Error (+21 more)

### Community 15 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.13
Nodes (17): AuthorizationHandler, AuthorizationHandlerContext, IAuthorizationRequirement, CancellationToken, ClaimsPrincipal, HttpContext, IFusionCache, IHttpContextAccessor (+9 more)

### Community 16 - "EmailOptions"
Cohesion: 0.09
Nodes (24): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+16 more)

### Community 17 - "Product"
Cohesion: 0.04
Nodes (46): ProductId, V1ProductNameUpdatedDomainEvent, DefaultIdType, ProductTemplate, ProductTemplateId, Store, StoreId, Product (+38 more)

### Community 18 - "StockReservation"
Cohesion: 0.14
Nodes (12): Inventory.Domain.StockReservations.Errors, StockReservationErrors, DateTimeOffset, DefaultIdType, StockReservation, LastReleaseAttemptReference, ProductId, ProviderReference (+4 more)

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
Cohesion: 0.13
Nodes (15): DateTimeOffset, DefaultIdType, Guid, DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive (+7 more)

### Community 23 - "IEmailGateway"
Cohesion: 0.18
Nodes (11): IEmailGateway, CancellationToken, ILogger, LoggerMessage, Task, DummyEmailGateway, IConfiguration, IFusionCache (+3 more)

### Community 24 - "IInterModuleRequestClient"
Cohesion: 0.07
Nodes (31): IInterModuleRequestClient, CancellationToken, Task, GetStockLevelRequest, GetStockLevelResponse, DefaultIdType, DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse (+23 more)

### Community 25 - "Common.Infrastructure.Extensions"
Cohesion: 0.13
Nodes (16): IAM.Endpoints.Captcha.VersionNeutral, Common.Infrastructure.RateLimiting, Products.Infrastructure.RateLimiting, Common.Infrastructure.Extensions, IAM.Endpoints.Otp.VersionNeutral, IAM.Infrastructure.RateLimiting, Notifications.Infrastructure.Sms.NetGsm, IAM.Infrastructure.Captcha (+8 more)

### Community 26 - "IEvent"
Cohesion: 0.08
Nodes (21): DomainEventHandlerBase, CancellationToken, Task, IEventHandler, CancellationToken, Task, IEventHandlerWrapper, CancellationToken (+13 more)

### Community 27 - "RequestResponseBodyLoggingMiddleware"
Cohesion: 0.22
Nodes (7): IDiagnosticContext, HttpContext, IList, IOptions, PathString, RequestDelegate, RequestResponseBodyLoggingMiddleware

### Community 28 - "AuditLogEntry"
Cohesion: 0.12
Nodes (13): AuditLogEntry, AggregateId, AggregateType, Event, EventType, Version, DefaultIdType, ModelBuilder (+5 more)

### Community 29 - "Result"
Cohesion: 0.11
Nodes (16): Result, Error, IsFailure, Success, Value, Func, Task, AsyncExtensions (+8 more)

### Community 30 - "JobRow"
Cohesion: 0.12
Nodes (14): JobRow, FailureKey, FinishedOn, Progress, QueuedOn, RequestedBy, StartedOn, Status (+6 more)

### Community 31 - "NotificationsModule"
Cohesion: 0.15
Nodes (10): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, NotificationsModule, ActivitySourceNames, MeterNames (+2 more)

### Community 32 - "PersistenceQueryableExtensions"
Cohesion: 0.33
Nodes (6): PersistenceQueryableExtensions, CancellationToken, Expression, Func, IQueryable, Task

### Community 33 - "AggregateRoot"
Cohesion: 0.06
Nodes (27): ProjectionEntity, SourceVersion, AggregateRoot, Events, Id, Version, IReadOnlyCollection, List (+19 more)

### Community 34 - "KeycloakPermissionClient"
Cohesion: 0.12
Nodes (20): CancellationToken, Dictionary, HttpClient, HttpResponseMessage, ILogger, IOptions, IReadOnlyList, List (+12 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.10
Nodes (20): IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, DateTimeOffset, OutboxMessage (+12 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.08
Nodes (22): CheckRegistrationRateLimitingPolicy, EmailRateLimitingPolicy, OtpVerifyRateLimitingPolicy, RegisterRateLimitingPolicy, SmsRateLimitingPolicy, CustomRateLimitingOptions, CheckRegistration, CreateStore (+14 more)

### Community 37 - "ICaptchaService"
Cohesion: 0.15
Nodes (10): ICaptchaService, CancellationToken, IFusionCache, IOptions, Task, CachedCaptchaService, DummyCaptchaService, IConfiguration (+2 more)

### Community 38 - ".RefreshToken"
Cohesion: 0.15
Nodes (11): IAM.Endpoints.Tokens.VersionNeutral.Refresh, CancellationToken, RouteGroupBuilder, Task, Endpoint, DateTimeOffset, Response, AccessToken (+3 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.10
Nodes (20): IHostBuilder, ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics, EnableTracing, LogSink (+12 more)

### Community 40 - "Host.Swagger"
Cohesion: 0.36
Nodes (5): Host.Swagger, microsoft_aspnetcore_mvc_apiexplorer, microsoft_openapi, swashbuckle_aspnetcore_swaggergen, system_text_json_nodes

### Community 41 - "Request"
Cohesion: 0.07
Nodes (28): Common.Domain.Extensions, Guid, Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName (+20 more)

### Community 42 - ".SaveWithOutboxAsync"
Cohesion: 0.27
Nodes (9): OutboxSaveHelper, CancellationToken, DbContext, Exception, Func, ILogger, LoggerMessage, Task (+1 more)

### Community 43 - "CreateStoreRateLimitingPolicy"
Cohesion: 0.14
Nodes (12): CancellationToken, Func, HttpContext, IOptions, IProblemDetailsService, IResxLocalizer, OnRejectedContext, RateLimitPartition (+4 more)

### Community 44 - "KeycloakOptions"
Cohesion: 0.14
Nodes (14): KeycloakOptions, AttemptTimeoutSeconds, Authority, BaseUrl, DecisionCacheMaxDurationSeconds, Realm, RequireHttpsMetadata, ResourceClientId (+6 more)

### Community 45 - "InterModuleRequestHandler"
Cohesion: 0.12
Nodes (16): IInterModuleRequestHandler, CancellationToken, Task, InterModuleRequestHandler, CancellationToken, ConsumeContext, Task, OtpVerificationFailureReason (+8 more)

### Community 46 - "PaginationQueryableExtensions"
Cohesion: 0.07
Nodes (32): BinaryExpression, How it works, One-time setup _(Build, in a migration)_, Query path, Ranking, Write path, ExpressionVisitor, KeyedRow (+24 more)

### Community 48 - "IDatabaseSeeder"
Cohesion: 0.18
Nodes (9): IDatabaseSeeder, Priority, CancellationToken, Task, CancellationToken, IServiceScopeFactory, Task, ProductsDatabaseSeeder (+1 more)

### Community 50 - "Response"
Cohesion: 0.11
Nodes (17): IAM.Endpoints.Users.VersionNeutral.Search, CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, DateTimeOffset, Response (+9 more)

### Community 51 - "Products.Domain.Products"
Cohesion: 0.08
Nodes (21): Products.Endpoints.Stores.v1.Search, Products.Endpoints.Products.v1.My.Get, Products.Endpoints.ProductTemplates.v1.Search, Products.Endpoints.Products.v1.Search, Products.Domain.Products, Common.Application.DTOs, Products.Endpoints.Products.v1.My.Search, Products.Endpoints.Products.v1.Get (+13 more)

### Community 52 - "ReCaptchaService"
Cohesion: 0.21
Nodes (10): FormUrlEncodedContent, ReCaptchaResponse, CancellationToken, Exception, HttpClient, ILogger, IOptions, LoggerMessage (+2 more)

### Community 53 - "UtcDateTimeOffsetConverter"
Cohesion: 0.25
Nodes (6): DateTimeOffset, ModelConfigurationBuilder, UtcDateTimeOffsetConverter, DateTimeOffset, DateTimeOffset, ModelConfigurationBuilder

### Community 54 - ".SendOtp"
Cohesion: 0.11
Nodes (17): EmailOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, SendEmailOtpRequest, SendEmailOtpResponse, CancellationToken, IFeatureManager (+9 more)

### Community 55 - "BrevoEmailGateway"
Cohesion: 0.19
Nodes (12): SendErrorBody, Exception, HttpClient, HttpStatusCode, ILogger, IOptions, JsonSerializerOptions, LoggerMessage (+4 more)

### Community 56 - "ProjectionReconciliationJob"
Cohesion: 0.25
Nodes (10): Items, Next, ProjectionReconciliationJob, CancellationToken, IDbContext, ILogger, IOptions, IReadOnlyList (+2 more)

### Community 58 - "OutboxMetricsJob"
Cohesion: 0.13
Nodes (14): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxMetricsJob (+6 more)

### Community 59 - ".SearchProductTemplatesAsync"
Cohesion: 0.15
Nodes (11): LikePattern, CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response (+3 more)

### Community 61 - "KeycloakTokenClient"
Cohesion: 0.11
Nodes (24): JsonWebTokenHandler, CancellationToken, Task, IKeycloakTokenClient, DateTimeOffset, KeycloakTokens, CancellationToken, Dictionary (+16 more)

### Community 62 - "IDbContext"
Cohesion: 0.12
Nodes (16): DatabaseFacade, Deleted, EntityEntry, JobHousekeepingJob, CancellationToken, ILogger, IOptions, LoggerMessage (+8 more)

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
Cohesion: 0.14
Nodes (16): Action, Exception, IApplicationBuilder, IEndpointRouteBuilder, IEnumerable, IHostApplicationLifetime, ILogger, ILoggerFactory (+8 more)

### Community 67 - ".AddNotificationsSignalR"
Cohesion: 0.17
Nodes (10): HubConnectionContext, IUserIdProvider, RedisOptions, IConfiguration, IConfigureOptions, IConnectionMultiplexer, IServiceCollection, IUserIdProvider (+2 more)

### Community 68 - "ValueObject"
Cohesion: 0.25
Nodes (4): Common.Domain, IComparable, ValueObject, IEnumerable

### Community 69 - ".HandleAsync"
Cohesion: 0.18
Nodes (11): GetSeedUserIdsRequest, GetSeedUserIdsResponse, ICollection, CancellationToken, Task, GetSeedUserIdsRequestHandler, CancellationToken, IResult (+3 more)

### Community 70 - "StrictDateTimeOffsetJsonConverter"
Cohesion: 0.11
Nodes (18): JsonConverter, StrictDateTimeOffsetJsonConverter, DateTimeOffset, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, StronglyTypedIdListReadOnlyJsonConverter (+10 more)

### Community 71 - "FullTextSearchOptions"
Cohesion: 0.10
Nodes (22): Add a new language/culture, Add search to a new entity _(Build checklist)_, Change the vector (weights, fields, or config), Extending and maintaining, File map _(Build)_, Full-Text Search, Gotchas, Non-goals (+14 more)

### Community 72 - "Response"
Cohesion: 0.22
Nodes (8): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Brand, Color, Model

### Community 73 - ".WriteAsync"
Cohesion: 0.33
Nodes (5): Memory, ReadOnlyMemory, CancellationToken, Task, ValueTask

### Community 75 - "IntegrationEventOutbox"
Cohesion: 0.22
Nodes (7): IIntegrationEventOutbox, IntegrationEventOutbox, HasPending, List, Lock, Setup, IServiceCollection

### Community 76 - "Response"
Cohesion: 0.15
Nodes (11): Products.Endpoints.Stores.v1.My.Get, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description (+3 more)

### Community 77 - "IProductsDbContext"
Cohesion: 0.04
Nodes (38): CancellationToken, Task, DbSet, ProductTemplate, IProductsDbContext, Products, ProductTemplates, Stores (+30 more)

### Community 78 - "CheckRegistrationRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, CheckRegistrationRateLimitingPolicy (+1 more)

### Community 79 - "Common.Domain.Events"
Cohesion: 0.04
Nodes (49): Products.Domain.Products.DomainEvents.v1, Inventory.Domain.StockReservations.DomainEvents.v1, Common.IntegrationEvents, Common.Domain.Events, Common.Application.Persistence.Outbox, Products.Application.Products.DomainEventHandlers.v1, DomainEvent, CreatedOn (+41 more)

### Community 80 - "Stores/v1/Get/Request.cs"
Cohesion: 0.40
Nodes (4): Products.Endpoints.Stores.v1.Get, Request, Id, RequestValidator

### Community 81 - ".SearchStoresAsync"
Cohesion: 0.15
Nodes (12): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Address (+4 more)

### Community 82 - "Split-Deployment PoC"
Cohesion: 0.18
Nodes (9): Concurrent safety, Cross-process call path, Files added by this PoC, How it works, How to run, Split-Deployment PoC, What this proves, README (Boilerplate Overview) (+1 more)

### Community 83 - "NotificationsDbContext"
Cohesion: 0.15
Nodes (13): DbSet, INotificationsDbContext, DeviceRegistrations, DbContextOptions, DbSet, ILogger, TimeProvider, NotificationsDbContext (+5 more)

### Community 84 - "Common.Infrastructure.Persistence.EntityConfigurations"
Cohesion: 0.06
Nodes (30): Inventory.Infrastructure.Persistence.EntityConfigurations, Common.Infrastructure.Persistence.EntityConfigurations, Notifications.Infrastructure.Persistence.EntityConfigurations, IEntityTypeConfiguration, microsoft_entityframeworkcore_metadata_builders, AuditableEntityConfiguration, EntityTypeBuilder, JobRowConfiguration (+22 more)

### Community 85 - "ResultTelemetryExtensions.cs"
Cohesion: 0.18
Nodes (5): Activity, ResultTelemetryExtensions, ActivitySource, Task, system_runtime_compilerservices

### Community 86 - "Store"
Cohesion: 0.06
Nodes (25): Products.Application.Stores.DomainEventHandlers.v1, Products.Domain.Stores.DomainEvents.v1, StoreId, V1StoreAddressUpdatedDomainEvent, StoreId, V1StoreDeactivatedDomainEvent, StoreId, V1StoreDeactivatedWithStockOnHandDomainEvent (+17 more)

### Community 87 - "PushOptions"
Cohesion: 0.12
Nodes (20): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri, PushOptions (+12 more)

### Community 89 - "Program.cs"
Cohesion: 0.22
Nodes (6): ConfigurationManager, Host, Host.Configurations, Setup, Program, WebApplicationBuilder

### Community 90 - "AuditableEntityResponse"
Cohesion: 0.11
Nodes (16): AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, DateTimeOffset, RouteGroupBuilder (+8 more)

### Community 91 - "Seeder"
Cohesion: 0.23
Nodes (9): CancellationToken, ILogger, LoggerMessage, ProductsDbContext, Task, Seeder, CancellationToken, List (+1 more)

### Community 92 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.12
Nodes (13): Common.Domain.StronglyTypedIds, Common.Application.Jobs, IAM.Endpoints.Users.VersionNeutral.Get, Common.Application.JsonConverters, Common.Domain.Entities, Common.Domain.Aggregates, Common.Infrastructure.Persistence.ValueConverters, microsoft_entityframeworkcore_diagnostics (+5 more)

### Community 93 - ".MapEndpoint"
Cohesion: 0.22
Nodes (6): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, RouteGroupBuilder, Response, ClientKey, RouteGroupBuilder, Setup

### Community 94 - "KeyedResiliencePipelines"
Cohesion: 0.16
Nodes (11): HttpRequestException, ResiliencePipelineBuilder, ResiliencePipelineRegistry, KeyedResiliencePipelines, HttpResponseMessage, IOptions, List, Lock (+3 more)

### Community 95 - ".SearchMyProductsAsync"
Cohesion: 0.17
Nodes (11): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Description (+3 more)

### Community 96 - "DatabaseSeederOrchestrator"
Cohesion: 0.12
Nodes (17): BackgroundService, DatabaseSeederOrchestrator, CancellationToken, Exception, IHostApplicationLifetime, ILogger, IServiceScopeFactory, LoggerMessage (+9 more)

### Community 97 - "StockReservationExpirySweepService"
Cohesion: 0.24
Nodes (9): CancellationToken, Exception, Guid, ILogger, IOptions, LoggerMessage, Task, TimeProvider (+1 more)

### Community 98 - ".SearchStoreProductsAsync"
Cohesion: 0.17
Nodes (11): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Description (+3 more)

### Community 99 - "AuditLogRetentionService"
Cohesion: 0.06
Nodes (32): IHostedService, AuditLogRetentionJobRegistrar, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task, AuditLogRetentionService (+24 more)

### Community 100 - "OutboxDbContext"
Cohesion: 0.12
Nodes (14): DbContext, IOutboxDbContext, OutboxMessages, CancellationToken, DbSet, Task, IConfiguration, IServiceCollection (+6 more)

### Community 101 - "ResiliencyOptions"
Cohesion: 0.17
Nodes (12): ResiliencyOptions, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, Keyed, MaxRetryAttempts (+4 more)

### Community 102 - "HttpContextTargetingContextAccessor"
Cohesion: 0.20
Nodes (8): ITargetingContextAccessor, HttpContextTargetingContextAccessor, IHttpContextAccessor, ValueTask, Setup, IConfiguration, IServiceCollection, TargetingContext

### Community 103 - "InboxCleanupJob"
Cohesion: 0.22
Nodes (10): CancellationToken, IEnumerable, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider (+2 more)

### Community 104 - "Products/v1/Update/Request.cs"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Products.v1.Update, RequestBody, Request, Body, Id, RequestBody, Description, Name (+3 more)

### Community 105 - "Response"
Cohesion: 0.18
Nodes (9): IAM.Endpoints.Users.VersionNeutral.SelfRegisterByEmail, RouteGroupBuilder, Endpoint, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken (+1 more)

### Community 106 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.10
Nodes (16): Notifications.Application.Otp, Common.InterModuleRequests, Common.Application.Caching, Common.Application.FeatureManagement, Notifications.Application.Persistence, Notifications.Infrastructure.Telemetry, IAM.Domain.Captcha, Common.InterModuleRequests.IAM (+8 more)

### Community 107 - "HttpWarehouseGateway"
Cohesion: 0.31
Nodes (8): ReleaseResponseBody, CancellationToken, HttpClient, HttpResponseMessage, Task, HttpWarehouseGateway, ReleaseRequestBody, ReleaseResponseBody

### Community 108 - "StockLevel"
Cohesion: 0.16
Nodes (11): Inventory.Domain.StockLevels.DomainEvents.v1, DefaultIdType, StockLevelId, V1StockLevelCreatedDomainEvent, DefaultIdType, StockLevel, ProductId, QuantityOnHand (+3 more)

### Community 109 - ".IsRegisteredAsync"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, IsRegistered

### Community 110 - "KeycloakPermissionPolicyProvider"
Cohesion: 0.24
Nodes (8): AuthorizationOptions, AuthorizationPolicy, DefaultAuthorizationPolicyProvider, IAuthorizationPolicyProvider, ConcurrentDictionary, IOptions, Task, KeycloakPermissionPolicyProvider

### Community 111 - ".ReleaseStockReservationAsync"
Cohesion: 0.15
Nodes (11): CancellationToken, Task, IWarehouseGateway, CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint (+3 more)

### Community 112 - "OutboxCleanupJob"
Cohesion: 0.27
Nodes (8): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxCleanupJob

### Community 113 - "ProductsDbContext"
Cohesion: 0.13
Nodes (14): DbContextOptions, DbSet, ILogger, ProductTemplate, Store, TimeProvider, ProductsDbContext, Products (+6 more)

### Community 114 - "Response"
Cohesion: 0.11
Nodes (17): IAM.Endpoints.Users.VersionNeutral.Me.Get, RouteGroupBuilder, Endpoint, DateOnly, DateTimeOffset, IReadOnlyCollection, Response, BirthDate (+9 more)

### Community 115 - "CaptchaOptions"
Cohesion: 0.16
Nodes (13): CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey, Provider, ScoreThreshold, SecretKey (+5 more)

### Community 116 - ".AddAuthInfrastructure"
Cohesion: 0.12
Nodes (16): IAllowAnonymous, IAuthorizationHandler, IConfigureNamedOptions, HttpContext, HttpStatusCode, IOptions, IProblemDetailsService, IResxLocalizer (+8 more)

### Community 117 - "Response"
Cohesion: 0.09
Nodes (20): Inventory.Endpoints.StockReservations.v1.Get, ReservationStatus, Active, Committed, Expired, Released, RequiresReconciliation, CancellationToken (+12 more)

### Community 118 - "IModule"
Cohesion: 0.09
Nodes (20): OpenTelemetryBuilder, ResourceBuilder, IModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority (+12 more)

### Community 119 - "ProductsModule"
Cohesion: 0.12
Nodes (13): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, ProductsModule (+5 more)

### Community 120 - ".HandleWarehouseWebhookAsync"
Cohesion: 0.13
Nodes (15): Inventory.Endpoints.StockReservations.v1.WebhookCallback, IHeaderDictionary, IValidator, CancellationToken, HttpContext, IOptions, JsonSerializerOptions, RouteGroupBuilder (+7 more)

### Community 121 - ".GetVariantAsync"
Cohesion: 0.40
Nodes (4): IVariantFeatureManager, IVariantFeatureManagerExtensions, CancellationToken, Task

### Community 122 - "OtpVerifyRateLimitingPolicy"
Cohesion: 0.18
Nodes (10): IRateLimiterPolicy, CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask (+2 more)

### Community 123 - "NotificationsHub"
Cohesion: 0.20
Nodes (7): Hub, NotificationGroupName, Exception, ILogger, LoggerMessage, Task, NotificationsHub

### Community 124 - ".AddCustomHealthChecks"
Cohesion: 0.12
Nodes (14): HealthCheckOptions, EnableHealthChecks, ReadinessTimeoutInSeconds, StartupTimeoutInSeconds, HealthCheckOptionsValidator, IApplicationBuilder, IConfiguration, IConnectionMultiplexer (+6 more)

### Community 125 - ".TapWhenFeatureEnabledAsync"
Cohesion: 0.33
Nodes (5): FeatureFlagResultExtensions, Action, Func, IFeatureManager, Task

### Community 126 - ".HandleExceptionAsync"
Cohesion: 0.33
Nodes (6): Exception, HttpContext, ILogger, LoggerMessage, RequestDelegate, Task

### Community 127 - "BaseDbContext"
Cohesion: 0.22
Nodes (8): BaseDbContext, AuditLog, CancellationToken, DbContextOptions, DbSet, ILogger, Task, TimeProvider

### Community 128 - "EmailRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, EmailRateLimitingPolicy (+1 more)

### Community 129 - ".SendAsync"
Cohesion: 0.20
Nodes (6): CancellationToken, SendRequestBody, Task, SendMessageBody, Msg, No

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.48
Nodes (7): Observability (OpenTelemetry), docker-compose.yml (Base Stack), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), Postgres Service (mm.postgres), RabbitMQ Service (mm.rabbitmq), Redis Service (mm.redis)

### Community 131 - ".AddInfrastructure"
Cohesion: 0.29
Nodes (7): HostOptions, JsonOptions, Assembly, IConfiguration, IResxLocalizer, IServiceCollection, IWebHostEnvironment

### Community 132 - "Stores/v1/Update/Request.cs"
Cohesion: 0.20
Nodes (10): Products.Endpoints.Stores.v1.Update, RequestBody, Request, Body, Id, RequestBody, Address, Description (+2 more)

### Community 134 - "SendRequestBody"
Cohesion: 0.25
Nodes (8): SendContact, IReadOnlyList, SendRequestBody, HtmlContent, Sender, Subject, TextContent, To

### Community 135 - ".ListSessions"
Cohesion: 0.10
Nodes (22): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, IReadOnlyList, CancellationToken, IReadOnlyCollection, RouteGroupBuilder (+14 more)

### Community 136 - ".GetMeAsync"
Cohesion: 0.19
Nodes (9): CancellationToken, IReadOnlyList, Task, IKeycloakPermissionClient, IReadOnlyList, GrantedPermission, CancellationToken, HttpContext (+1 more)

### Community 137 - ".WriteTooManyRequestsToResponse"
Cohesion: 0.13
Nodes (14): PartitionedRateLimiter, CancellationToken, Func, HttpContext, IConfiguration, IProblemDetailsService, IReadOnlyList, IResxLocalizer (+6 more)

### Community 138 - ".From"
Cohesion: 0.09
Nodes (26): AspNetResult, IEndpointFilter, IFeatureManagerSnapshot, ResxLocalizer, ProblemResponse, ResultToAcceptedResponseTransformer, ResultToCreatedResponseTransformer, ResultToResponseTransformer (+18 more)

### Community 139 - "ProductTemplate"
Cohesion: 0.15
Nodes (11): IReadOnlyList, List, ProductTemplate, Brand, Color, IsActive, Model, Products (+3 more)

### Community 140 - ".MapEndpoint"
Cohesion: 0.40
Nodes (3): RouteGroupBuilder, RouteGroupBuilder, Setup

### Community 141 - "IntegrationEvent"
Cohesion: 0.22
Nodes (9): IReadOnlyList, IntegrationEvent, CreatedOn, Id, DateTimeOffset, DefaultIdType, ProductCreatedIntegrationEvent, StoreCreatedIntegrationEvent (+1 more)

### Community 142 - "TokenRefreshRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenRefreshRateLimitingPolicy (+1 more)

### Community 143 - "Setup"
Cohesion: 0.11
Nodes (15): LoadAll, LoggerConfiguration, LoggerMinimumLevelConfiguration, Names, Setup, IEnumerable, IHostEnvironment, KeyValuePair (+7 more)

### Community 144 - "Response"
Cohesion: 0.12
Nodes (16): CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate (+8 more)

### Community 145 - "StoreId"
Cohesion: 0.10
Nodes (10): StronglyTypedIdHelper, CancellationToken, Task, ProductCreatedIntegrationEventPublishingHandler, V1ProductCreatedDomainEventHandlers, ProductId, StoreId, V1ProductCreatedDomainEvent (+2 more)

### Community 146 - ".RegisterAsync"
Cohesion: 0.07
Nodes (29): BindDeviceSessionRequest, BindDeviceSessionResponse, Guid, CancellationToken, Exception, Guid, ILogger, LoggerMessage (+21 more)

### Community 147 - ".AddServices"
Cohesion: 0.12
Nodes (15): RecurringJobOptions, IRecurringBackgroundJobs, Action, Expression, Func, Task, IConfiguration, IServiceCollection (+7 more)

### Community 148 - "IInterModuleRequest"
Cohesion: 0.33
Nodes (8): IInterModuleRequest, GetActiveSessionIdsRequest, GetActiveSessionIdsResponse, UserSessionIds, IReadOnlyList, CancellationToken, Task, GetActiveSessionIdsRequestHandler

### Community 149 - "ProductTemplateId"
Cohesion: 0.25
Nodes (6): ProductTemplateId, DefaultIdType, ProductTemplateId, CancellationToken, List, Task

### Community 150 - "StockReservationExpirySweepJobRegistrar"
Cohesion: 0.31
Nodes (7): CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task, StockReservationExpirySweepJobRegistrar

### Community 151 - "OutboxOptions"
Cohesion: 0.10
Nodes (21): OutboxCleanupSettings, BatchSize, CronSchedule, Enabled, RetentionDays, OutboxCleanupSettingsValidator, OutboxOptions, BaseBackoffSeconds (+13 more)

### Community 152 - ".EnsureNoMigrationsPending"
Cohesion: 0.35
Nodes (6): AutoMigrateMarker, IAutoMigrateMarker, MigrationGuard, ILogger, IServiceProvider, LoggerMessage

### Community 153 - "SwaggerDefaultValues"
Cohesion: 0.40
Nodes (4): JsonValue, OpenApiOperation, OperationFilterContext, SwaggerDefaultValues

### Community 154 - ".CreateTokensByEmail"
Cohesion: 0.07
Nodes (28): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, VerifyEmailOtpRequest, VerifyEmailOtpResponse, EmailNormalization, CancellationToken, RouteGroupBuilder, Task (+20 more)

### Community 155 - "ISmsGateway"
Cohesion: 0.13
Nodes (13): CancellationToken, Task, ISmsGateway, SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, SmsMessage (+5 more)

### Community 156 - "InboxStore"
Cohesion: 0.18
Nodes (11): IInboxCleanupTarget, ModuleName, IInboxStore, DefaultIdType, Setup, IServiceCollection, NpgsqlDataSource, TimeProvider (+3 more)

### Community 157 - "CreateStockLevelOnProductCreatedHandler"
Cohesion: 0.24
Nodes (8): CancellationToken, DefaultIdType, IFusionCache, ILogger, IOptions, LoggerMessage, Task, CreateStockLevelOnProductCreatedHandler

### Community 158 - "RedisOtpService"
Cohesion: 0.12
Nodes (13): OtpCodeGenerator, IFusionCache, IOptions, OtpService, CancellationToken, IConnectionMultiplexer, IOptions, Task (+5 more)

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

### Community 164 - ".AddKeycloakInfrastructure"
Cohesion: 0.16
Nodes (10): CancellationToken, Task, IServiceAccountTokenProvider, HttpClient, IOptions, TimeProvider, ServiceAccountTokenProvider, IOptions (+2 more)

### Community 165 - "KeycloakPermissionAuthorizationHandler.cs"
Cohesion: 0.13
Nodes (11): Common.Infrastructure.Auth.Services, IAM.Infrastructure.Auth, Notifications.Infrastructure.Hubs, Common.Infrastructure.Auth, microsoft_aspnetcore_authentication, microsoft_aspnetcore_authentication_jwtbearer, microsoft_aspnetcore_authorization, microsoft_aspnetcore_signalr (+3 more)

### Community 166 - "IamModule"
Cohesion: 0.18
Nodes (10): Action, IApplicationBuilder, IEnumerable, RateLimiterOptions, IamModule, ActivitySourceNames, MeterNames, Name (+2 more)

### Community 167 - "common_application_localization_resources"
Cohesion: 0.06
Nodes (40): common_application_localization_resources, Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Stores.v1.My.AuditLog, Common.Application.Pagination, Products.Endpoints.Products.v1.AuditLog, PaginationRequest, After, IncludeTotal (+32 more)

### Community 168 - "Response"
Cohesion: 0.22
Nodes (6): Products.Endpoints.ProductTemplates.v1.Create, RouteGroupBuilder, Setup, RouteGroupBuilder, Response, Id

### Community 169 - "SendSecurityAlertRequestHandler"
Cohesion: 0.16
Nodes (15): IReadOnlyDictionary, SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, CancellationToken, IReadOnlyList, Task (+7 more)

### Community 171 - "IBackgroundJobs"
Cohesion: 0.26
Nodes (7): IBackgroundJobs, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 172 - "fluentvalidation"
Cohesion: 0.06
Nodes (34): Common.Application.Validation, fluentvalidation, DatabaseOptions, ConnectionString, DatabaseOptionsValidator, JobHousekeepingOptions, PageSize, RetentionHours (+26 more)

### Community 173 - "RequestBody"
Cohesion: 0.29
Nodes (7): ProductTemplateId, RequestBody, Description, Name, Price, ProductTemplateId, Quantity

### Community 174 - "system_diagnostics"
Cohesion: 0.11
Nodes (11): Notifications.Application.Push, BackgroundJobs.Telemetry, Notifications.Infrastructure.Push.Firebase, firebaseadmin, firebaseadmin_messaging, google_apis_auth_oauth2, hangfire_server, LoginMethods (+3 more)

### Community 175 - "PaginationResponse"
Cohesion: 0.09
Nodes (23): JsonElement, AuditLogDto, DateTimeOffset, PaginationResponse, HasNext, HasPrevious, HasTotal, NextPageNumber (+15 more)

### Community 176 - ".FixedWindow"
Cohesion: 0.23
Nodes (10): FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit, RateLimitPartitions, HttpContext, IConnectionMultiplexer (+2 more)

### Community 177 - "Request"
Cohesion: 0.18
Nodes (11): StoreId, Request, Description, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name (+3 more)

### Community 178 - ".AddPushServices"
Cohesion: 0.22
Nodes (8): CancellationToken, ILogger, LoggerMessage, Task, DummyPushGateway, IConfiguration, IServiceCollection, Setup

### Community 179 - "CachingOptions"
Cohesion: 0.11
Nodes (21): CachingEntryDefaults, Duration, FactoryHardTimeout, FactorySoftTimeout, FailSafeMaxDuration, FailSafeThrottleDuration, CachingOptions, AllowInMemoryOnlyInProduction (+13 more)

### Community 180 - "SmsOptions"
Cohesion: 0.10
Nodes (21): SmsOptions, AppName, AttemptTimeoutSeconds, BaseUrl, MaxPerDay, MaxPerPhoneNumberPerDay, MaxRetryAttempts, MsgHeader (+13 more)

### Community 181 - "RabbitMqOptions"
Cohesion: 0.10
Nodes (18): Type, RabbitMqOptions, DefaultConcurrentMessageLimit, DefaultPrefetchCount, Host, Password, Port, RetryIntervalDeltaMs (+10 more)

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

### Community 189 - "JobClaimExtensions"
Cohesion: 0.28
Nodes (9): IdBound, Value, JobClaimExtensions, CancellationToken, DateTimeOffset, DbSet, Expression, Func (+1 more)

### Community 190 - "ServiceAccountTokenCache"
Cohesion: 0.11
Nodes (11): KeycloakPaths, CancellationToken, DateTimeOffset, SemaphoreSlim, Task, TimeSpan, ServiceAccountTokenCache, AccessToken (+3 more)

### Community 191 - "My/AddProduct/Request.cs"
Cohesion: 0.20
Nodes (9): Products.Endpoints.Stores.v1.My.AddProduct, ProductTemplateId, Request, Description, Name, Price, ProductTemplateId, Quantity (+1 more)

### Community 192 - "KeycloakPermission"
Cohesion: 0.29
Nodes (3): KeycloakPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder

### Community 193 - "system_globalization"
Cohesion: 0.10
Nodes (20): Products.Endpoints.Stores.v1.My.Update, Request, Brand, Color, Model, RequestValidator, Request, Address (+12 more)

### Community 194 - "CorsOptions"
Cohesion: 0.25
Nodes (8): CorsOptions, AllowCredentials, AllowedHeaders, AllowedMethods, AllowedOrigins, MaxAgeInSeconds, CorsOptionsValidator, IReadOnlyList

### Community 195 - "InventoryOptions"
Cohesion: 0.16
Nodes (12): InventoryOptions, MaxOccurrencesPerSeries, SafetyBufferQuantity, StockReservationExpirySweepBatchSize, StockReservationExpirySweepCron, WarehouseGatewayBaseUrl, WarehouseGatewayProvider, WebhookSharedSecret (+4 more)

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

### Community 202 - "VersionNeutral/Create/Request.cs"
Cohesion: 0.10
Nodes (19): Notifications.Infrastructure.Devices.UpdateCurrentPushToken, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, Common.Domain.Devices, DeviceSessionConstants, Guid, Request, ClientId, DeviceId (+11 more)

### Community 203 - "SmsRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, SmsRateLimitingPolicy (+1 more)

### Community 205 - ".UseModules"
Cohesion: 0.26
Nodes (6): ModuleRegistry, Exception, IApplicationBuilder, ILogger, LoggerMessage, WebApplication

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
Cohesion: 0.08
Nodes (24): SendPhoneOtpRequest, SendPhoneOtpResponse, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, CancellationToken, Task (+16 more)

### Community 214 - "CreateByEmail/Request.cs"
Cohesion: 0.05
Nodes (33): IAM.Domain.Users, IAM.Endpoints.Otp.VersionNeutral.SendForLogin, IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, IAM.Endpoints.Common.Validations, Constants, IResxLocalizer, IRuleBuilder, IRuleBuilderOptions (+25 more)

### Community 215 - ".CreateMyStoreAsync"
Cohesion: 0.10
Nodes (14): Products.Endpoints.Stores.v1.My.Create, CollectionExtensions, Func, ICollection, IEnumerable, Store, CancellationToken, Task (+6 more)

### Community 216 - "OtpOptions"
Cohesion: 0.18
Nodes (11): OtpOptions, DummyCode, EmailQuotaWindowMinutes, ExpirationInMinutes, Length, MaxSendsPerEmailPerWindow, MaxSendsPerPhonePerWindow, PhoneQuotaWindowMinutes (+3 more)

### Community 217 - "FeatureFlags"
Cohesion: 0.33
Nodes (5): Checkout, FeatureFlags, IAM, Notifications, Products

### Community 218 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 219 - "Response"
Cohesion: 0.22
Nodes (6): Products.Endpoints.Stores.v1.Create, RouteGroupBuilder, Setup, RouteGroupBuilder, Response, Id

### Community 220 - ".CleanupAsync"
Cohesion: 0.29
Nodes (6): CancellationToken, DateTimeOffset, Task, CancellationToken, DateTimeOffset, Task

### Community 221 - "Keycloak realm as code"
Cohesion: 0.25
Nodes (7): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Secrets, Seed users (dev / test only)

### Community 222 - "Key decisions"
Cohesion: 0.29
Nodes (7): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Key decisions

### Community 223 - "microsoft_aspnetcore_mvc"
Cohesion: 0.06
Nodes (38): Products.Endpoints.Stores.v1.Deactivate, Products.Endpoints.ProductTemplates.v1.Deactivate, Products.Endpoints.Stores.v1.My.RemoveProduct, Inventory.Endpoints.StockReservations.v1.Release, Common.Application.ModelBinders, Products.Endpoints.ProductTemplates.v1.Activate, Products.Endpoints.Stores.v1.RemoveProduct, microsoft_aspnetcore_mvc (+30 more)

### Community 224 - ".MapEndpoint"
Cohesion: 0.40
Nodes (3): RouteGroupBuilder, RouteGroupBuilder, Setup

### Community 225 - "ProcessedMessage"
Cohesion: 0.24
Nodes (8): ProcessedMessage, ConsumerName, MessageId, ProcessedOn, DateTimeOffset, DefaultIdType, DefaultIdType, UniqueConstraintException

### Community 226 - "NetGsmSmsGateway"
Cohesion: 0.32
Nodes (7): Exception, HttpClient, ILogger, IOptions, JsonSerializerOptions, LoggerMessage, NetGsmSmsGateway

### Community 227 - "RegisterRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, RegisterRateLimitingPolicy (+1 more)

### Community 228 - ".UpsertIfNewerAsync"
Cohesion: 0.09
Nodes (17): Common.Application.Persistence.Projections, ICurrentDbContext, IModelBinder, microsoft_aspnetcore_mvc_modelbinding, ModelBindingContext, StronglyTypedIdBinder, Task, ProjectionUpsertExtensions (+9 more)

### Community 229 - ".AddCommonOptions"
Cohesion: 0.40
Nodes (4): Setup, IConfiguration, IHostEnvironment, IServiceCollection

### Community 230 - "Versioning/Setup.cs"
Cohesion: 0.50
Nodes (3): asp_versioning, asp_versioning_builder, asp_versioning_conventions

### Community 231 - ".UseModule"
Cohesion: 0.22
Nodes (7): DashboardContext, IAuthorizationService, IDashboardAsyncAuthorizationFilter, IApplicationBuilder, IOptions, HangfireCustomAuthorizationFilter, Task

### Community 232 - "system_linq_expressions"
Cohesion: 0.24
Nodes (6): Common.Application.BackgroundJobs, BackgroundJobs, hangfire, hangfire_annotations, hangfire_dashboard, system_linq_expressions

### Community 233 - "SendForEmail/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForEmail, Request, CaptchaToken, Email, RequestValidator

### Community 234 - "Setup"
Cohesion: 0.33
Nodes (4): IApplicationBuilder, IWebHostEnvironment, Type, Setup

### Community 235 - "MassTransitInterModuleRequestClient"
Cohesion: 0.33
Nodes (5): IClientFactory, MassTransitInterModuleRequestClient, CancellationToken, IOptions, Task

### Community 236 - "IOtpService"
Cohesion: 0.15
Nodes (14): IssueVerificationTokenRequest, IssueVerificationTokenResponse, CancellationToken, Task, TimeSpan, IOtpService, OtpVerificationOutcome, InvalidOtp (+6 more)

### Community 237 - "ProblemDetailsExtensions.cs"
Cohesion: 0.40
Nodes (3): ProblemDetails, ProblemDetailsExtensions, ICollection

### Community 238 - "SecurityHeadersMiddleware"
Cohesion: 0.22
Nodes (7): IAuthenticationSchemeProvider, IApplicationBuilder, HttpContext, IOptions, RequestDelegate, Task, SecurityHeadersMiddleware

### Community 239 - "IAM.Application.Keycloak"
Cohesion: 0.07
Nodes (24): IAM.Endpoints.Users, IAM.Infrastructure.Keycloak.Representations, IAM.Endpoints.Otp, Inventory.Infrastructure.Gateway, IAM.Infrastructure.Telemetry, Inventory.Application.Gateway, IAM.Application.Keycloak, IAM.Domain.Errors (+16 more)

### Community 240 - "InterModuleRequestOptions"
Cohesion: 0.25
Nodes (8): InterModuleRequestOptions, DependencyUnavailableRetryAfterSeconds, HandlerConcurrentMessageLimit, HandlerPrefetchCount, Timeouts, TimeoutSeconds, InterModuleRequestOptionsValidator, Dictionary

### Community 241 - ".DeactivateStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 242 - ".AddCustomSwagger"
Cohesion: 0.22
Nodes (7): IOpenApiSchema, ISchemaFilter, SchemaFilterContext, IConfigureOptions, IServiceCollection, SwaggerGenOptions, StronglyTypedIdSchemaFilter

### Community 243 - "CustomValidator"
Cohesion: 0.12
Nodes (22): Inventory.Endpoints.StockReservations.v1.Commit, CustomRateLimitingOptionsValidator, FixedWindowValidator, CustomValidator, RequestBody, Request, Body, Id (+14 more)

### Community 244 - "KeyedResilienceProfile"
Cohesion: 0.22
Nodes (9): KeyedResilienceProfile, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, RateLimitPermits, RateLimitQueueLimit (+1 more)

### Community 245 - "ReCaptchaResponse"
Cohesion: 0.25
Nodes (7): DateTime, ReCaptchaResponse, ChallengeTs, ErrorCodes, Hostname, Score, Success

### Community 246 - "BackgroundJobsTelemetry"
Cohesion: 0.14
Nodes (11): IServerFilter, PerformedContext, PerformingContext, JobMetricsFilter, BackgroundJobsTelemetry, ActivitySource, ConcurrentDictionary, Counter (+3 more)

### Community 248 - "SendErrorBody"
Cohesion: 0.67
Nodes (3): SendErrorBody, Code, Message

### Community 249 - ".UpdateStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 250 - "Response"
Cohesion: 0.20
Nodes (9): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Description, Name, Price (+1 more)

### Community 251 - "Products/v1/My/Update/Request.cs"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Products.v1.My.Update, RequestBody, Request, Body, Id, RequestBody, Description, Name (+3 more)

### Community 252 - ".TryWriteAsync"
Cohesion: 0.40
Nodes (4): ProblemDetailsContext, ProblemDetailsServiceExtensions, IProblemDetailsService, Task

### Community 260 - "BackgroundJobsOptions"
Cohesion: 0.29
Nodes (7): BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount, BackgroundJobsOptionsValidator

### Community 261 - "v1/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Probe.v1, Request, Count, RequestValidator

### Community 262 - ".SendWithPipelineAsync"
Cohesion: 0.25
Nodes (7): HttpClientKeyedExtensions, CancellationToken, HttpClient, HttpRequestMessage, HttpResponseMessage, ResiliencePipeline, Task

### Community 263 - "Response"
Cohesion: 0.25
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegister, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 264 - "GlobalExceptionHandlingMiddleware"
Cohesion: 0.25
Nodes (6): IApplicationBuilder, IServiceCollection, IOptions, IProblemDetailsService, IResxLocalizer, GlobalExceptionHandlingMiddleware

### Community 265 - "BackgroundJobsModule"
Cohesion: 0.25
Nodes (7): BackgroundJobsModule, ActivitySourceNames, MeterNames, Name, StartupPriority, IEndpointRouteBuilder, IEnumerable

### Community 266 - "Request"
Cohesion: 0.25
Nodes (8): Request, Description, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name, SearchTerm

### Community 267 - "AuditLogOptions"
Cohesion: 0.33
Nodes (6): AuditLogOptions, PerSchemaRetentionDays, PurgeBatchSize, RetentionDays, AuditLogOptionsValidator, Dictionary

### Community 268 - "DevicesOptions"
Cohesion: 0.33
Nodes (6): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator, IReadOnlyCollection

### Community 269 - "InterModuleRequestHandlerDefinition"
Cohesion: 0.29
Nodes (6): ConsumerDefinition, IConsumerConfigurator, IRegistrationContext, InterModuleRequestHandlerDefinition, IOptions, IReceiveEndpointConfigurator

### Community 270 - "DefaultResponsesOperationFilter"
Cohesion: 0.70
Nodes (3): OpenApiOperation, OperationFilterContext, DefaultResponsesOperationFilter

### Community 271 - "PublishOutcome"
Cohesion: 0.50
Nodes (4): PublishOutcome, Failed, Published, Skipped

### Community 272 - ".AddResilientHttpClient"
Cohesion: 0.22
Nodes (8): HttpStandardResilienceOptions, IHttpClientBuilder, Setup, Action, HttpClient, IOptions, IServiceCollection, IServiceProvider

### Community 273 - ".AddNetGsm"
Cohesion: 0.33
Nodes (5): IConfiguration, IFusionCache, IOptions, IServiceCollection, Setup

### Community 274 - "GetProductRequest"
Cohesion: 0.39
Nodes (6): GetProductRequest, GetProductResponse, DefaultIdType, CancellationToken, Task, GetProductRequestHandler

### Community 275 - ".GetAuditLogAsync"
Cohesion: 0.38
Nodes (5): DbContextExtensions, CancellationToken, DbSet, JsonSerializerOptions, Task

### Community 276 - ".CommitStockReservationAsync"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 277 - "SendResponseBody"
Cohesion: 0.50
Nodes (4): SendResponseBody, Code, Description, JobId

### Community 278 - "ProductTemplates/v1/Get/Request.cs"
Cohesion: 0.40
Nodes (4): Products.Endpoints.ProductTemplates.v1.Get, Request, Id, RequestValidator

### Community 279 - "Response"
Cohesion: 0.25
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.Create, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 280 - "EnrichLogsWithUserInfoMiddleware"
Cohesion: 0.33
Nodes (5): IMiddleware, HttpContext, RequestDelegate, Task, EnrichLogsWithUserInfoMiddleware

### Community 281 - ".TryReadFromJsonAsync"
Cohesion: 0.40
Nodes (4): HttpContent, CancellationToken, Task, HttpContentJsonExtensions

### Community 282 - "Setup.Logger.cs"
Cohesion: 0.08
Nodes (18): Host.Infrastructure, elastic_serilog_sinks, microsoft_aspnetcore_httpoverrides, opentelemetry_exporter, opentelemetry_metrics, opentelemetry_opentelemetrybuilder, opentelemetry_resources, opentelemetry_trace (+10 more)

### Community 283 - "ResiliencyOptions.cs"
Cohesion: 0.40
Nodes (4): AbstractValidator, KeyedResilienceProfileValidator, ResiliencyOptionsValidator, KeyValuePair

### Community 284 - "Reserve/Request.cs"
Cohesion: 0.20
Nodes (10): DateTimeOffset, DefaultIdType, RequestBody, Request, Body, RequestBody, ProductId, Quantity (+2 more)

### Community 286 - "Common.Application.Options"
Cohesion: 0.04
Nodes (53): asp_versioning_apiexplorer, Common.Infrastructure.Modules, Notifications.Application.Sms, Notifications.Infrastructure.Devices, Inventory.Endpoints, Products.Endpoints.Stores, Common.Endpoints.Versioning, Notifications.Infrastructure.Email (+45 more)

### Community 288 - "DummySmsGateway"
Cohesion: 0.38
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, DummySmsGateway

### Community 289 - ".SetRetryAfterHeader"
Cohesion: 0.40
Nodes (4): RetryAfterHeaderExtensions, HttpResponse, RateLimitLease, TimeSpan

### Community 290 - "ICurrentUser"
Cohesion: 0.07
Nodes (25): ICurrentUser, Id, IdAsString, Roles, SessionId, ICollection, CurrentUser, Id (+17 more)

### Community 291 - "ConcurrencyConfiguratorExtensions.cs"
Cohesion: 0.40
Nodes (3): IBusFactoryConfigurator, ConcurrencyConfiguratorExtensions, IReceiveEndpointConfigurator

### Community 294 - ".SeedProductAsync"
Cohesion: 0.33
Nodes (5): CancellationToken, List, ProductTemplateId, StoreId, Task

### Community 295 - "RemoveDefaultResponseSchemaFilter"
Cohesion: 0.40
Nodes (4): IOperationFilter, OpenApiOperation, OperationFilterContext, RemoveDefaultResponseSchemaFilter

### Community 296 - "StringExtensions"
Cohesion: 0.29
Nodes (3): SearchValues, StringExtensions, system_buffers

### Community 297 - "SignalROptions"
Cohesion: 0.40
Nodes (5): SignalROptions, RedisConnectionString, RequireRedisBackplaneInProduction, UseRedisBackplane, SignalROptionsValidator

### Community 298 - ".AddCommonCaching"
Cohesion: 0.40
Nodes (4): Setup, IConfiguration, IConnectionMultiplexer, IServiceCollection

### Community 299 - "InventoryTelemetry"
Cohesion: 0.50
Nodes (4): ActivitySource, Counter, Meter, InventoryTelemetry

### Community 300 - "ProductsTelemetry"
Cohesion: 0.50
Nodes (4): ActivitySource, Counter, Meter, ProductsTelemetry

### Community 304 - "WarehouseGatewayProvider"
Cohesion: 0.67
Nodes (3): WarehouseGatewayProvider, Dummy, Http

## Knowledge Gaps
- **947 isolated node(s):** `Id`, `IdAsString`, `Roles`, `SessionId`, `JwtClaimNames` (+942 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 2203 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **95 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `microsoft_entityframeworkcore`, `BackgroundJobsOptions`, `AuditLogOptions`, `DevicesOptions`, `Common.Domain.ResultMonad`, `EmailOptions`, `OutboxOptions`, `Common.Infrastructure.Extensions`, `Setup.Logger.cs`, `ResiliencyOptions.cs`, `KeycloakPermissionAuthorizationHandler.cs`, `ObservabilityOptions`, `SignalROptions`, `Request`, `fluentvalidation`, `KeycloakOptions`, `system_diagnostics`, `CachingOptions`, `SmsOptions`, `RabbitMqOptions`, `IdentityScheme`, `RequestLoggingOptions`, `CorsOptions`, `InventoryOptions`, `FullTextSearchOptions`, `OpenApiOptions`, `VersionNeutral/Create/Request.cs`, `CreateByEmail/Request.cs`, `PushOptions`, `OtpOptions`, `Program.cs`, `Common.InterModuleRequests.Contracts`, `IAM.Application.Keycloak`, `InterModuleRequestOptions`, `CaptchaOptions`, `CustomValidator`, `.AddCustomHealthChecks`?**
  _High betweenness centrality (0.171) - this node is a cross-community bridge._
- **Why does `Result` connect `Result` to `.SendAsync`, `.ListSessions`, `.GetMeAsync`, `FirebasePushGateway`, `Error`, `ProductTemplate`, `ApplicationUserId`, `Response`, `Product`, `.RegisterAsync`, `.GetAuditLogAsync`, `StockReservation`, `.CommitStockReservationAsync`, `IEmailGateway`, `IInterModuleRequestClient`, `.CreateTokensByEmail`, `ISmsGateway`, `PersistenceQueryableExtensions`, `.ReserveSeriesAsync`, `ThrottledEmailGateway`, `ICurrentUser`, `DummySmsGateway`, `ICaptchaService`, `.RefreshToken`, `StringExtensions`, `SendSecurityAlertRequestHandler`, `PaginationQueryableExtensions`, `PaginationResponse`, `Response`, `.AddPushServices`, `ReCaptchaService`, `.SendOtp`, `BrevoEmailGateway`, `.SearchProductTemplatesAsync`, `KeycloakTokenClient`, `.SendAsync`, `.ReserveStockAsync`, `Response`, `Response`, `IProductsDbContext`, `.SearchStoresAsync`, `ResultTelemetryExtensions.cs`, `.SendOtp`, `.CreateMyStoreAsync`, `Store`, `Response`, `.SearchMyProductsAsync`, `NetGsmSmsGateway`, `.SearchStoreProductsAsync`, `HttpWarehouseGateway`, `.IsRegisteredAsync`, `.ReleaseStockReservationAsync`, `.DeactivateStoreAsync`, `Response`, `.HandleWarehouseWebhookAsync`, `.UpdateStoreAsync`, `Response`, `.TapWhenFeatureEnabledAsync`?**
  _High betweenness centrality (0.124) - this node is a cross-community bridge._
- **Why does `ApplicationUserId` connect `ApplicationUserId` to `IStronglyTypedId`, `NotificationPayload`, `KeycloakAdminClient`, `.ListSessions`, `IntegrationEvent`, `KeycloakPermissionAuthorizationHandler`, `Response`, `StoreId`, `.RegisterAsync`, `IInterModuleRequest`, `DeviceRegistration`, `IInterModuleRequestClient`, `IEvent`, `JobRow`, `AggregateRoot`, `ICurrentUser`, `For`, `SendSecurityAlertRequestHandler`, `PaginationResponse`, `Request`, `Response`, `KeycloakTokenClient`, `system_globalization`, `.HandleAsync`, `Response`, `.SearchStoresAsync`, `Common.Infrastructure.Persistence.EntityConfigurations`, `Store`, `.CreateMyStoreAsync`, `AuditableEntityResponse`, `Response`, `Common.Domain.StronglyTypedIds`, `Seeder`, `microsoft_aspnetcore_mvc`, `Response`, `NotificationsHub`?**
  _High betweenness centrality (0.070) - this node is a cross-community bridge._
- **What connects `Id`, `IdAsString`, `Roles` to the rest of the system?**
  _947 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `IStronglyTypedId` be split into smaller, more focused modules?**
  _Cohesion score 0.14210526315789473 - nodes in this community are weakly interconnected._
- **Should `microsoft_entityframeworkcore` be split into smaller, more focused modules?**
  _Cohesion score 0.06346153846153846 - nodes in this community are weakly interconnected._
- **Should `KeycloakAdminClient` be split into smaller, more focused modules?**
  _Cohesion score 0.13821138211382114 - nodes in this community are weakly interconnected._