# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-09-23)

## Corpus Check
- 544 files · ~80,152 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 4565 nodes · 9021 edges · 343 communities (246 shown, 97 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 244 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `42a1c9dc`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- StockReservation
- NotificationPayload
- OutboxProcessor
- system_net
- Hybrid DDD (Writes) / VSA (Reads)
- KeycloakAdminClient
- RedisFixedWindowRateLimiter
- ApplySearchLanguageInterceptor
- FirebasePushGateway
- IAMModule.cs
- Error
- Common.Domain.ResultMonad
- ApplicationUserId
- ISearchLanguageResolver
- UserRepresentation
- KeycloakPermissionAuthorizationHandler
- EmailOptions
- .AddServices
- AuditableEntity
- IntegrationEventHandlerBase
- IntegrationEvent
- BoundedRequestCaptureStream
- DeviceRegistration
- IEmailGateway
- .RevokeToken
- IStronglyTypedId
- IEvent
- RequestResponseBodyLoggingMiddleware
- KeycloakTokenClient
- Result
- Product
- NotificationsModule
- .SingleAsResult
- Products.Domain.Products.DomainEvents.v1
- KeycloakPermissionClient
- OutboxMessage
- CustomRateLimitingOptions
- DomainEvent
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
- .AssignBasicRoleOrRollbackAsync
- ReCaptchaService
- Resiliency/Setup.cs
- SendEmailOtpRequestHandler
- BrevoEmailGateway
- AuditLogRetentionJobRegistrar
- Cross-Module Reference Violation
- OutboxMetricsJob
- .SearchProductTemplatesAsync
- Bogus Test Data
- Setup.HealthChecks.cs
- IDbContext
- RequestLoggingOptions
- .SendCoreAsync
- BackgroundJobsService
- OutboxModule
- .AddNotificationsSignalR
- ValueObject
- .HandleAsync
- StrictDateTimeOffsetJsonConverter
- FullTextSearchOptions
- AuditableEntityResponse
- .WriteAsync
- AsNoTracking Coverage Check
- IntegrationEventOutbox
- Response
- .SaveChangesAsync
- CheckRegistrationRateLimitingPolicy
- SeedingCompletionTracker
- Stores/v1/Update/Request.cs
- .SearchStoresAsync
- Split-Deployment PoC
- NotificationsDbContext
- .Configure
- ResultTelemetryExtensions
- Store
- PushOptions
- Configuration-Driven Module Registration
- Setup
- .GetProductAsync
- Products/v1/My/Update/Request.cs
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
- Products/v1/Update/Request.cs
- Response
- microsoft_extensions_dependencyinjection
- .ReleaseStockReservationAsync
- StockLevel
- .IsRegisteredAsync
- KeycloakPermissionPolicyProvider
- Response
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
- .AddProductAsync
- .WriteTooManyRequestsToResponse
- ResultToResponseTransformer.cs
- ProductTemplate
- .MapEndpoint
- .GetMeAsync
- StoreId
- Setup
- Response
- EnrichLogsWithUserInfoMiddleware
- .RegisterAsync
- IRecurringBackgroundJobs
- DeviceRegistryReconcileJobRegistrar
- ProductTemplateId
- StockReservationExpirySweepJobRegistrar
- OutboxOptions
- .EnsureNoMigrationsPending
- SwaggerDefaultValues
- Response
- SmsMessage
- Request
- CreateStockLevelOnProductCreatedHandler
- OtpService
- IInventoryDbContext
- OtpServiceBase
- .ReserveSeriesAsync
- .Generate
- For
- IAutoMigrateMarker.cs
- CurrentUser
- IamModule
- system_globalization
- Response
- SendSecurityAlertRequestHandler
- Notifications.Application/IAssemblyReference.cs
- IOtpService
- fluentvalidation
- RequestBody
- V1ProductCreatedDomainEvent
- PaginationResponse
- .FixedWindow
- Request
- PushMessage
- CachingOptions
- SmsOptions
- RabbitMqOptions
- IAM.Domain
- SendRequestBody
- RequestBody
- IdentityScheme
- .Get
- ReverseProxyOptions
- BoundedCaptureStream
- ISearchLocalized
- ServiceAccountTokenCache
- DeviceRegistryReconciliationService
- CachedCaptchaService
- CorsOptions
- InventoryOptions
- TokenCreateRateLimitingPolicy
- InventoryModule
- OpenApiOptions
- NotificationsTelemetry
- KeycloakScopes
- CreateByEmail/Request.cs
- SmsRateLimitingPolicy
- Configuration-Driven Module Loading
- VersionNeutral/Create/Request.cs
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- ConfigureSwaggerOptions
- Versioning/Setup.cs
- ResultTelemetryExtensions.cs
- DummyCaptchaService.cs
- IProductsDbContext
- common_application_localization_resources
- FeatureFlags
- Response
- FixedWindow
- Keycloak realm as code
- Key decisions
- Seeder
- .MapEndpoint
- BackgroundJobsOptions
- NetGsmSmsGateway
- StronglyTypedIdBinder.cs
- .AddCommonOptions
- Inventory.Domain.StockReservations
- Request
- Common.Application.Options
- Setup
- ProblemDetailsExtensions.cs
- microsoft_entityframeworkcore
- microsoft_extensions_logging
- RegisterRateLimitingPolicy
- .AddCustomSwagger
- CustomValidator
- RemoveDefaultResponseSchemaFilter
- KeycloakPermissionAuthorizationHandler.cs
- .Capture
- SendErrorBody
- Infrastructure/Setup.cs
- KeycloakPermission
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
- Response
- ProductTemplates/v1/Create/Request.cs
- AuditLogOptions
- DefaultResponsesOperationFilter
- system_text_json_serialization
- Common.InterModuleRequests.Contracts
- GetProductRequest
- Request
- SendPhoneOtpRequestHandler
- SendResponseBody
- Response
- TokenEndpointRepresentations.cs
- Setup.Logger.cs
- Reserve/Request.cs
- Setup
- .UpdateCurrentPushToken
- Setup
- DummySmsGateway
- ICurrentUser
- .ActivateProductTemplateAsync
- .AddServices
- .MapEndpoints
- .SeedProductAsync
- Infrastructure/StringExtensions.cs
- Request
- ISmsGateway
- Stores/Setup.cs
- JwtClaimNames.cs
- KeycloakRoles.cs
- .AddStockReservationExpirySweep
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
- `Gotchas` --references--> `FullTextSearchOptions`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Application/Options/FullTextSearchOptions.cs
- `Configuration` --references--> `ISearchLanguageResolver`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Application/Search/ISearchLanguageResolver.cs
- `File map _(Build)_` --references--> `ISearchLanguageResolver`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Application/Search/ISearchLanguageResolver.cs
- `Add search to a new entity _(Build checklist)_` --references--> `ISearchLocalized`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Domain/Entities/ISearchLocalized.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Local Infrastructure Stack** — docker_compose_postgres, docker_compose_rabbitmq, docker_compose_redis, docker_compose_aspire_dashboard [EXTRACTED 1.00]

## Communities (343 total, 97 thin omitted)

### Community 0 - "StockReservation"
Cohesion: 0.05
Nodes (39): Inventory.Domain.StockReservations.DomainEvents.v1, DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationCommitConflictDetectedDomainEvent, DateTimeOffset, DefaultIdType, StockReservationId (+31 more)

### Community 1 - "NotificationPayload"
Cohesion: 0.18
Nodes (13): Notifications.Application.Hubs, IHubContext, CancellationToken, IReadOnlyList, Task, INotificationDispatcher, Task, INotificationsClient (+5 more)

### Community 2 - "OutboxProcessor"
Cohesion: 0.22
Nodes (11): IPublishEndpoint, CancellationToken, Exception, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task (+3 more)

### Community 3 - "system_net"
Cohesion: 0.10
Nodes (18): Notifications.Application.Otp, Notifications.Application.Sms, Notifications.Infrastructure.Email, Common.Application.Caching, Notifications.Infrastructure.Sms, Notifications.Infrastructure.Email.Brevo, Notifications.Infrastructure.Telemetry, Notifications.Infrastructure.Otp (+10 more)

### Community 5 - "KeycloakAdminClient"
Cohesion: 0.13
Nodes (24): HttpRequestMessage, DateOnly, DateTimeOffset, IReadOnlyList, CreateKeycloakUser, GrantedPermission, KeycloakUser, KeycloakUserPage (+16 more)

### Community 6 - "RedisFixedWindowRateLimiter"
Cohesion: 0.13
Nodes (16): RateLimiter, RateLimiterStatistics, RateLimitLease, FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration (+8 more)

### Community 7 - "ApplySearchLanguageInterceptor"
Cohesion: 0.14
Nodes (13): SaveChangesInterceptor, ApplyAuditingInterceptor, CancellationToken, DbContextEventData, InterceptionResult, TimeProvider, ValueTask, ApplySearchLanguageInterceptor (+5 more)

### Community 8 - "FirebasePushGateway"
Cohesion: 0.17
Nodes (12): FirebaseApp, FirebaseMessaging, IDisposable, CancellationToken, Exception, IEnumerable, ILogger, IReadOnlyList (+4 more)

### Community 9 - "IAMModule.cs"
Cohesion: 0.07
Nodes (26): Common.Infrastructure.Modules, IAM.Endpoints.Captcha.VersionNeutral, Inventory.Endpoints, Products.Endpoints.Stores, Common.Endpoints.Versioning, Products.Endpoints.Probe, Products.Endpoints.Products, Products.Endpoints.ProductTemplates (+18 more)

### Community 10 - "Error"
Cohesion: 0.06
Nodes (23): SearchValues, StringLocalizerExtensions, IStringLocalizer, StringExtensions, Error, Key, ParameterName, StatusCode (+15 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.14
Nodes (17): Common.Application.Search, Inventory.Application.Persistence, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Products.Infrastructure.Telemetry, IAM.Endpoints.Tokens.VersionNeutral.Revoke, Products.Application.Persistence, Inventory.Infrastructure.Telemetry (+9 more)

### Community 12 - "ApplicationUserId"
Cohesion: 0.13
Nodes (13): ApplicationUserId, IsEmpty, Value, DefaultIdType, CancellationToken, IReadOnlyList, Task, IKeycloakAdminClient (+5 more)

### Community 13 - "ISearchLanguageResolver"
Cohesion: 0.18
Nodes (9): ISearchLanguageResolver, UniversalConfig, SearchLanguageResolver, UniversalConfig, IOptions, Setup, IApplicationBuilder, IOptions (+1 more)

### Community 14 - "UserRepresentation"
Cohesion: 0.07
Nodes (29): Dictionary, List, CredentialRepresentation, Temporary, Type, Value, ErrorRepresentation, Error (+21 more)

### Community 15 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.13
Nodes (17): AuthorizationHandler, AuthorizationHandlerContext, IAuthorizationRequirement, CancellationToken, ClaimsPrincipal, HttpContext, IFusionCache, IHttpContextAccessor (+9 more)

### Community 16 - "EmailOptions"
Cohesion: 0.09
Nodes (24): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+16 more)

### Community 17 - ".AddServices"
Cohesion: 0.09
Nodes (19): DashboardContext, IAuthorizationService, IDashboardAsyncAuthorizationFilter, IServerFilter, PerformingContext, BackgroundJobsModule, ActivitySourceNames, MeterNames (+11 more)

### Community 18 - "AuditableEntity"
Cohesion: 0.10
Nodes (19): IAggregateRoot, Events, Id, Version, IReadOnlyCollection, AuditableEntity, CreatedBy, CreatedOn (+11 more)

### Community 19 - "IntegrationEventHandlerBase"
Cohesion: 0.22
Nodes (12): IConsumer, IntegrationEventHandlerBase, MaxEventAge, CancellationToken, ConsumeContext, DefaultIdType, IFusionCache, ILogger (+4 more)

### Community 20 - "IntegrationEvent"
Cohesion: 0.10
Nodes (19): IntegrationEventConverter, JsonSerializerOptions, IntegrationEvent, CreatedOn, Id, DateTimeOffset, DefaultIdType, ProductCreatedIntegrationEvent (+11 more)

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.14
Nodes (8): SeekOrigin, BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position, Stream

### Community 22 - "DeviceRegistration"
Cohesion: 0.15
Nodes (13): DateTimeOffset, DefaultIdType, Guid, DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive (+5 more)

### Community 23 - "IEmailGateway"
Cohesion: 0.10
Nodes (19): CancellationToken, Task, EmailMessage, IEmailGateway, CancellationToken, ILogger, LoggerMessage, Task (+11 more)

### Community 24 - ".RevokeToken"
Cohesion: 0.13
Nodes (14): DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse, IReadOnlyList, CancellationToken, RouteGroupBuilder, Task, Endpoint, CancellationToken (+6 more)

### Community 25 - "IStronglyTypedId"
Cohesion: 0.14
Nodes (13): StronglyTypedIdReadOnlyJsonConverter, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, StronglyTypedIdWriteOnlyJsonConverter, JsonSerializerOptions, Type (+5 more)

### Community 26 - "IEvent"
Cohesion: 0.11
Nodes (14): DomainEventHandlerBase, CancellationToken, Task, IEventHandler, CancellationToken, Task, IEventHandlerWrapper, CancellationToken (+6 more)

### Community 27 - "RequestResponseBodyLoggingMiddleware"
Cohesion: 0.22
Nodes (7): IDiagnosticContext, HttpContext, IList, IOptions, PathString, RequestDelegate, RequestResponseBodyLoggingMiddleware

### Community 28 - "KeycloakTokenClient"
Cohesion: 0.10
Nodes (27): JsonWebTokenHandler, CancellationToken, Task, IKeycloakTokenClient, DateTimeOffset, KeycloakTokens, CancellationToken, Dictionary (+19 more)

### Community 29 - "Result"
Cohesion: 0.11
Nodes (16): Result, Error, IsFailure, Success, Value, Func, Task, AsyncExtensions (+8 more)

### Community 30 - "Product"
Cohesion: 0.08
Nodes (26): DefaultIdType, ProductTemplate, ProductTemplateId, Store, StoreId, Product, Description, Language (+18 more)

### Community 31 - "NotificationsModule"
Cohesion: 0.15
Nodes (10): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, NotificationsModule, ActivitySourceNames, MeterNames (+2 more)

### Community 32 - ".SingleAsResult"
Cohesion: 0.18
Nodes (10): CollectionExtensions, Func, ICollection, IEnumerable, PersistenceQueryableExtensions, CancellationToken, Expression, Func (+2 more)

### Community 33 - "Products.Domain.Products.DomainEvents.v1"
Cohesion: 0.09
Nodes (14): Products.Domain.Products.DomainEvents.v1, Products.Application.Products.DomainEventHandlers.v1, ProductId, V1ProductDescriptionUpdatedDomainEvent, ProductId, V1ProductNameUpdatedDomainEvent, ProductId, V1ProductPriceDecreasedDomainEvent (+6 more)

### Community 34 - "KeycloakPermissionClient"
Cohesion: 0.22
Nodes (11): CancellationToken, Dictionary, HttpClient, HttpResponseMessage, ILogger, IOptions, IReadOnlyList, List (+3 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.10
Nodes (20): IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, DateTimeOffset, OutboxMessage (+12 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.08
Nodes (22): CheckRegistrationRateLimitingPolicy, EmailRateLimitingPolicy, OtpVerifyRateLimitingPolicy, RegisterRateLimitingPolicy, SmsRateLimitingPolicy, CustomRateLimitingOptions, CheckRegistration, CreateStore (+14 more)

### Community 37 - "DomainEvent"
Cohesion: 0.07
Nodes (27): AuditLogEntry, AggregateId, AggregateType, Event, EventType, Version, DefaultIdType, DomainEvent (+19 more)

### Community 38 - ".RefreshToken"
Cohesion: 0.15
Nodes (11): IAM.Endpoints.Tokens.VersionNeutral.Refresh, CancellationToken, RouteGroupBuilder, Task, Endpoint, DateTimeOffset, Response, AccessToken (+3 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.08
Nodes (24): IHostBuilder, KeyValuePair, LoggerConfiguration, LoggerMinimumLevelConfiguration, ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl (+16 more)

### Community 40 - "ConfigureSwaggerOptions.cs"
Cohesion: 0.30
Nodes (6): asp_versioning_apiexplorer, Host.Swagger, microsoft_aspnetcore_mvc_apiexplorer, microsoft_openapi, swashbuckle_aspnetcore_swaggergen, system_text_json_nodes

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
Cohesion: 0.07
Nodes (34): IInterModuleRequest, BindDeviceSessionRequest, BindDeviceSessionResponse, Guid, OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts (+26 more)

### Community 46 - ".PaginateAsync"
Cohesion: 0.17
Nodes (11): How it works, One-time setup _(Build, in a migration)_, Query path, Ranking, Write path, PaginationQueryableExtensions, CancellationToken, Expression (+3 more)

### Community 48 - ".AddPersistence"
Cohesion: 0.15
Nodes (11): IDatabaseSeeder, Priority, CancellationToken, Task, CancellationToken, IServiceScopeFactory, Task, ProductsDatabaseSeeder (+3 more)

### Community 50 - "Response"
Cohesion: 0.12
Nodes (16): CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate (+8 more)

### Community 51 - ".AssignBasicRoleOrRollbackAsync"
Cohesion: 0.14
Nodes (10): CancellationToken, Exception, ILogger, LoggerMessage, Task, RegistrationCompletion, ActivitySource, Counter (+2 more)

### Community 52 - "ReCaptchaService"
Cohesion: 0.06
Nodes (34): DateTime, FormUrlEncodedContent, ReCaptchaResponse, CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey (+26 more)

### Community 53 - "Resiliency/Setup.cs"
Cohesion: 0.24
Nodes (5): Inventory.Infrastructure.Gateway, Common.Infrastructure.Resiliency, Inventory.Application.Gateway, microsoft_extensions_http_resilience, polly

### Community 54 - "SendEmailOtpRequestHandler"
Cohesion: 0.16
Nodes (13): EmailOtpDispatchErrors, EmailOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, SendEmailOtpRequest, SendEmailOtpResponse, CancellationToken (+5 more)

### Community 55 - "BrevoEmailGateway"
Cohesion: 0.23
Nodes (10): Exception, HttpClient, HttpStatusCode, ILogger, IOptions, JsonSerializerOptions, LoggerMessage, BrevoEmailGateway (+2 more)

### Community 56 - "AuditLogRetentionJobRegistrar"
Cohesion: 0.22
Nodes (9): IHostedService, AuditLogRetentionJobRegistrar, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task, Setup (+1 more)

### Community 58 - "OutboxMetricsJob"
Cohesion: 0.13
Nodes (14): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxMetricsJob (+6 more)

### Community 59 - ".SearchProductTemplatesAsync"
Cohesion: 0.15
Nodes (11): LikePattern, CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response (+3 more)

### Community 61 - "Setup.HealthChecks.cs"
Cohesion: 0.19
Nodes (7): Notifications.Infrastructure.Hubs, Common.Infrastructure.Caching, healthchecks_ui_client, microsoft_aspnetcore_diagnostics_healthchecks_healthcheckoptions, microsoft_aspnetcore_signalr, microsoft_extensions_diagnostics_healthchecks, stackexchange_redis

### Community 62 - "IDbContext"
Cohesion: 0.15
Nodes (12): DatabaseFacade, EntityEntry, IDbContext, AuditLog, ChangeTracker, Database, DbSet, DbContextExtensions (+4 more)

### Community 63 - "RequestLoggingOptions"
Cohesion: 0.11
Nodes (20): IPostConfigureOptions, RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes (+12 more)

### Community 64 - ".SendCoreAsync"
Cohesion: 0.19
Nodes (9): SendErrorBody, SendResponseBody, CancellationToken, HttpResponseMessage, SendRequestBody, Task, SendContact, Email (+1 more)

### Community 65 - "BackgroundJobsService"
Cohesion: 0.13
Nodes (15): IBackgroundJobClientV2, IBackgroundJobs, Action, DateTimeOffset, Expression, Func, Task, TimeSpan (+7 more)

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
Cohesion: 0.19
Nodes (11): GetSeedUserIdsRequest, GetSeedUserIdsResponse, ICollection, CancellationToken, Task, GetSeedUserIdsRequestHandler, CancellationToken, IResult (+3 more)

### Community 70 - "StrictDateTimeOffsetJsonConverter"
Cohesion: 0.09
Nodes (19): JsonConverter, StrictDateTimeOffsetJsonConverter, DateTimeOffset, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, StronglyTypedIdListReadOnlyJsonConverter (+11 more)

### Community 71 - "FullTextSearchOptions"
Cohesion: 0.11
Nodes (20): Add a new language/culture, Add search to a new entity _(Build checklist)_, Change the vector (weights, fields, or config), Configuration, Extending and maintaining, Full-Text Search, Gotchas, Non-goals (+12 more)

### Community 72 - "AuditableEntityResponse"
Cohesion: 0.08
Nodes (24): AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, DateTimeOffset, CancellationToken (+16 more)

### Community 73 - ".WriteAsync"
Cohesion: 0.33
Nodes (5): Memory, ReadOnlyMemory, CancellationToken, Task, ValueTask

### Community 75 - "IntegrationEventOutbox"
Cohesion: 0.13
Nodes (15): Lock, IIntegrationEventOutbox, IntegrationEventOutbox, HasPending, IReadOnlyList, List, Setup, IServiceCollection (+7 more)

### Community 76 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 77 - ".SaveChangesAsync"
Cohesion: 0.06
Nodes (22): CancellationToken, Task, CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint, ProductTemplate (+14 more)

### Community 78 - "CheckRegistrationRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, CheckRegistrationRateLimitingPolicy (+1 more)

### Community 79 - "SeedingCompletionTracker"
Cohesion: 0.15
Nodes (8): SeedingCompletionTracker, CancellationToken, Exception, Task, Setup, IOptions, IServiceCollection, TaskCompletionSource

### Community 80 - "Stores/v1/Update/Request.cs"
Cohesion: 0.20
Nodes (10): Products.Endpoints.Stores.v1.Update, RequestBody, Request, Body, Id, RequestBody, Address, Description (+2 more)

### Community 81 - ".SearchStoresAsync"
Cohesion: 0.15
Nodes (12): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Address (+4 more)

### Community 82 - "Split-Deployment PoC"
Cohesion: 0.18
Nodes (9): Concurrent safety, Cross-process call path, Files added by this PoC, How it works, How to run, Split-Deployment PoC, What this proves, README (Boilerplate Overview) (+1 more)

### Community 83 - "NotificationsDbContext"
Cohesion: 0.12
Nodes (17): EventDispatcher, ActivitySource, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task, DbContextOptions (+9 more)

### Community 84 - ".Configure"
Cohesion: 0.15
Nodes (14): AuditableEntityConfiguration, EntityTypeBuilder, StronglyTypedIdValueConverter, DefaultIdType, EntityTypeBuilder, DeviceRegistrationConfiguration, EntityTypeBuilder, NpgsqlTsVector (+6 more)

### Community 85 - "ResultTelemetryExtensions"
Cohesion: 0.38
Nodes (4): Activity, ResultTelemetryExtensions, ActivitySource, Task

### Community 86 - "Store"
Cohesion: 0.08
Nodes (25): AggregateRoot, Events, Id, Version, IReadOnlyCollection, List, StoreId, V1StoreAddressUpdatedDomainEvent (+17 more)

### Community 87 - "PushOptions"
Cohesion: 0.12
Nodes (20): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri, PushOptions (+12 more)

### Community 89 - "Setup"
Cohesion: 0.33
Nodes (4): ConfigurationManager, Host.Configurations, Setup, WebApplicationBuilder

### Community 90 - ".GetProductAsync"
Cohesion: 0.12
Nodes (19): GetStockLevelRequest, GetStockLevelResponse, DefaultIdType, CancellationToken, Task, GetStockLevelRequestHandler, RouteGroupBuilder, Setup (+11 more)

### Community 91 - "Products/v1/My/Update/Request.cs"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Products.v1.My.Update, RequestBody, Request, Body, Id, RequestBody, Description, Name (+3 more)

### Community 92 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.05
Nodes (29): IAM.Endpoints.Users.VersionNeutral.Search, Products.Infrastructure.InterModuleRequestHandlers, Common.Domain.StronglyTypedIds, Products.Endpoints.Stores.v1.Search, Common.Domain.Events, Products.Endpoints.Products.v1.My.Get, Common.Application.AuditLog, Products.Endpoints.ProductTemplates.v1.Search (+21 more)

### Community 93 - ".MapEndpoint"
Cohesion: 0.22
Nodes (6): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, RouteGroupBuilder, Response, ClientKey, RouteGroupBuilder, Setup

### Community 94 - "OtpOptions"
Cohesion: 0.18
Nodes (11): OtpOptions, DummyCode, EmailQuotaWindowMinutes, ExpirationInMinutes, Length, MaxSendsPerEmailPerWindow, MaxSendsPerPhonePerWindow, PhoneQuotaWindowMinutes (+3 more)

### Community 95 - ".SearchMyProductsAsync"
Cohesion: 0.17
Nodes (11): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Description (+3 more)

### Community 96 - "DatabaseSeederOrchestrator"
Cohesion: 0.27
Nodes (9): BackgroundService, DatabaseSeederOrchestrator, CancellationToken, Exception, IHostApplicationLifetime, ILogger, IServiceScopeFactory, LoggerMessage (+1 more)

### Community 97 - "StockReservationExpirySweepService"
Cohesion: 0.24
Nodes (9): CancellationToken, Exception, Guid, ILogger, IOptions, LoggerMessage, Task, TimeProvider (+1 more)

### Community 98 - ".SearchStoreProductsAsync"
Cohesion: 0.17
Nodes (11): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Description (+3 more)

### Community 99 - "AuditLogRetentionService"
Cohesion: 0.33
Nodes (7): AuditLogRetentionService, CancellationToken, ILogger, IOptions, LoggerMessage, NpgsqlDataSource, Task

### Community 100 - "OutboxDbContext"
Cohesion: 0.12
Nodes (14): DbContext, IOutboxDbContext, OutboxMessages, CancellationToken, DbSet, Task, IConfiguration, IServiceCollection (+6 more)

### Community 101 - "ResiliencyOptions"
Cohesion: 0.10
Nodes (19): HttpStandardResilienceOptions, IHttpClientBuilder, ResiliencyOptions, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds (+11 more)

### Community 102 - "HttpContextTargetingContextAccessor"
Cohesion: 0.20
Nodes (8): ITargetingContextAccessor, HttpContextTargetingContextAccessor, IHttpContextAccessor, ValueTask, Setup, IConfiguration, IServiceCollection, TargetingContext

### Community 103 - "BackgroundJobsTelemetry"
Cohesion: 0.20
Nodes (8): PerformedContext, BackgroundJobsTelemetry, ActivitySource, ConcurrentDictionary, Counter, Histogram, Meter, ObservableGauge

### Community 104 - "Products/v1/Update/Request.cs"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Products.v1.Update, RequestBody, Request, Body, Id, RequestBody, Description, Name (+3 more)

### Community 105 - "Response"
Cohesion: 0.18
Nodes (9): IAM.Endpoints.Users.VersionNeutral.SelfRegisterByEmail, RouteGroupBuilder, Endpoint, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken (+1 more)

### Community 106 - "microsoft_extensions_dependencyinjection"
Cohesion: 0.10
Nodes (19): Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Outbox, Common.Infrastructure.Persistence.Outbox, Notifications.Infrastructure.Persistence, Inventory.Infrastructure.Persistence, Outbox.Persistence, Outbox.Telemetry (+11 more)

### Community 107 - ".ReleaseStockReservationAsync"
Cohesion: 0.09
Nodes (22): ReleaseResponseBody, CancellationToken, Task, IWarehouseGateway, CancellationToken, RouteGroupBuilder, Task, TimeProvider (+14 more)

### Community 108 - "StockLevel"
Cohesion: 0.11
Nodes (12): Inventory.Domain.StockLevels.DomainEvents.v1, StronglyTypedIdHelper, DefaultIdType, StockLevelId, V1StockLevelCreatedDomainEvent, DefaultIdType, StockLevel, ProductId (+4 more)

### Community 109 - ".IsRegisteredAsync"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, IsRegistered

### Community 110 - "KeycloakPermissionPolicyProvider"
Cohesion: 0.24
Nodes (8): AuthorizationOptions, AuthorizationPolicy, DefaultAuthorizationPolicyProvider, IAuthorizationPolicyProvider, ConcurrentDictionary, IOptions, Task, KeycloakPermissionPolicyProvider

### Community 111 - "Response"
Cohesion: 0.25
Nodes (6): IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, RouteGroupBuilder, Endpoint, Response, EmailVerificationToken, IsRegistered

### Community 112 - "OutboxCleanupJob"
Cohesion: 0.27
Nodes (8): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxCleanupJob

### Community 113 - "ProductsDbContext"
Cohesion: 0.15
Nodes (12): DbContextOptions, DbSet, ILogger, ProductTemplate, Store, TimeProvider, ProductsDbContext, Products (+4 more)

### Community 114 - "Response"
Cohesion: 0.12
Nodes (16): RouteGroupBuilder, Endpoint, DateOnly, DateTimeOffset, IReadOnlyCollection, Response, BirthDate, CreatedOn (+8 more)

### Community 115 - "InterModuleRequestHandler"
Cohesion: 0.21
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
Cohesion: 0.12
Nodes (14): BaseDbContext, AuditLog, CancellationToken, DateTimeOffset, DbContextOptions, DbSet, ILogger, ModelConfigurationBuilder (+6 more)

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
Cohesion: 0.29
Nodes (7): HostOptions, JsonOptions, Assembly, IConfiguration, IResxLocalizer, IServiceCollection, IWebHostEnvironment

### Community 132 - "OtpVerifyRateLimitingPolicy"
Cohesion: 0.25
Nodes (8): IRateLimiterPolicy, CancellationToken, Func, IOptions, OnRejectedContext, ValueTask, OtpVerifyRateLimitingPolicy, OnRejected

### Community 134 - "SendRequestBody"
Cohesion: 0.25
Nodes (8): SendContact, IReadOnlyList, SendRequestBody, HtmlContent, Sender, Subject, TextContent, To

### Community 135 - ".ListSessions"
Cohesion: 0.10
Nodes (22): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, IReadOnlyList, CancellationToken, IReadOnlyCollection, RouteGroupBuilder (+14 more)

### Community 136 - ".AddProductAsync"
Cohesion: 0.18
Nodes (9): Products.Endpoints.Stores.v1.AddProduct, CancellationToken, ProductTemplate, RouteGroupBuilder, Store, Task, Endpoint, Response (+1 more)

### Community 137 - ".WriteTooManyRequestsToResponse"
Cohesion: 0.13
Nodes (14): PartitionedRateLimiter, CancellationToken, Func, HttpContext, IConfiguration, IProblemDetailsService, IReadOnlyList, IResxLocalizer (+6 more)

### Community 138 - "ResultToResponseTransformer.cs"
Cohesion: 0.08
Nodes (30): AspNetResult, Common.Application.EndpointFilters, IEndpointFilter, IFeatureManagerSnapshot, microsoft_aspnetcore_hosting, microsoft_aspnetcore_http_iresult, microsoft_extensions_localization, ResxLocalizer (+22 more)

### Community 139 - "ProductTemplate"
Cohesion: 0.15
Nodes (11): IReadOnlyList, List, ProductTemplate, Brand, Color, IsActive, Model, Products (+3 more)

### Community 140 - ".MapEndpoint"
Cohesion: 0.40
Nodes (3): RouteGroupBuilder, RouteGroupBuilder, Setup

### Community 141 - ".GetMeAsync"
Cohesion: 0.24
Nodes (7): CancellationToken, IReadOnlyList, Task, IKeycloakPermissionClient, CancellationToken, HttpContext, Task

### Community 142 - "StoreId"
Cohesion: 0.12
Nodes (14): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Stores.v1.Deactivate, StoreId, DefaultIdType, StoreId, Request, Id, RequestValidator (+6 more)

### Community 143 - "Setup"
Cohesion: 0.11
Nodes (17): LoadAll, ModuleRegistry, Names, Type, Setup, Assembly, Exception, IApplicationBuilder (+9 more)

### Community 144 - "Response"
Cohesion: 0.12
Nodes (16): CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate (+8 more)

### Community 145 - "EnrichLogsWithUserInfoMiddleware"
Cohesion: 0.12
Nodes (13): IAuthenticationSchemeProvider, IMiddleware, serilog_context, IApplicationBuilder, HttpContext, RequestDelegate, Task, EnrichLogsWithUserInfoMiddleware (+5 more)

### Community 146 - ".RegisterAsync"
Cohesion: 0.05
Nodes (44): IClientFactory, IInterModuleRequestClient, CancellationToken, Task, MassTransitInterModuleRequestClient, CancellationToken, IOptions, Task (+36 more)

### Community 147 - "IRecurringBackgroundJobs"
Cohesion: 0.13
Nodes (13): RecurringJobOptions, IRecurringBackgroundJobs, Action, Expression, Func, Task, RecurringBackgroundJobsService, Action (+5 more)

### Community 148 - "DeviceRegistryReconcileJobRegistrar"
Cohesion: 0.18
Nodes (10): CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task, DeviceRegistryReconcileJobRegistrar, IServiceCollection (+2 more)

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
Cohesion: 0.40
Nodes (4): JsonValue, OpenApiOperation, OperationFilterContext, SwaggerDefaultValues

### Community 154 - "Response"
Cohesion: 0.20
Nodes (8): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, RouteGroupBuilder, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 155 - "SmsMessage"
Cohesion: 0.33
Nodes (5): SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, SmsMessage

### Community 156 - "Request"
Cohesion: 0.14
Nodes (14): Common.Domain.Extensions, Guid, Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName (+6 more)

### Community 157 - "CreateStockLevelOnProductCreatedHandler"
Cohesion: 0.24
Nodes (8): CancellationToken, DefaultIdType, IFusionCache, ILogger, IOptions, LoggerMessage, Task, CreateStockLevelOnProductCreatedHandler

### Community 158 - "OtpService"
Cohesion: 0.29
Nodes (6): IFusionCache, IOptions, OtpService, IConfiguration, IServiceCollection, Setup

### Community 159 - "IInventoryDbContext"
Cohesion: 0.09
Nodes (21): DbSet, IInventoryDbContext, StockLevels, StockReservations, CancellationToken, RouteGroupBuilder, Task, Endpoint (+13 more)

### Community 160 - "OtpServiceBase"
Cohesion: 0.21
Nodes (8): OtpCacheEntry, DateTimeOffset, CancellationToken, IFusionCache, SemaphoreSlim, Task, TimeSpan, OtpServiceBase

### Community 161 - ".ReserveSeriesAsync"
Cohesion: 0.16
Nodes (11): CancellationToken, IOptions, List, RequestBody, RouteGroupBuilder, Task, TimeProvider, Endpoint (+3 more)

### Community 165 - "CurrentUser"
Cohesion: 0.12
Nodes (14): Common.Infrastructure.Auth.Services, Common.Infrastructure.Auth, CurrentUser, Id, IdAsString, IsAuthenticated, Principal, Roles (+6 more)

### Community 166 - "IamModule"
Cohesion: 0.18
Nodes (10): Action, IApplicationBuilder, IEnumerable, RateLimiterOptions, IamModule, ActivitySourceNames, MeterNames, Name (+2 more)

### Community 167 - "system_globalization"
Cohesion: 0.05
Nodes (39): Products.Endpoints.Stores.v1.My.AuditLog, Products.Endpoints.Products.v1.AuditLog, PaginationRequest, PageNumber, PageSize, Skip, Take, PaginationRequestValidator (+31 more)

### Community 168 - "Response"
Cohesion: 0.22
Nodes (6): Products.Endpoints.ProductTemplates.v1.Create, RouteGroupBuilder, Setup, RouteGroupBuilder, Response, Id

### Community 169 - "SendSecurityAlertRequestHandler"
Cohesion: 0.27
Nodes (9): SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, CancellationToken, IOptions, RequestLocalizationOptions, Task (+1 more)

### Community 171 - "IOtpService"
Cohesion: 0.14
Nodes (14): IssueVerificationTokenRequest, IssueVerificationTokenResponse, CancellationToken, Task, TimeSpan, IOtpService, OtpVerificationOutcome, InvalidOtp (+6 more)

### Community 172 - "fluentvalidation"
Cohesion: 0.06
Nodes (34): Inventory.Endpoints.StockReservations.v1.WebhookCallback, Products.Endpoints.Probe.v1, Common.Application.Validation, fluentvalidation, DatabaseOptions, ConnectionString, DatabaseOptionsValidator, InterModuleRequestOptions (+26 more)

### Community 173 - "RequestBody"
Cohesion: 0.29
Nodes (7): ProductTemplateId, RequestBody, Description, Name, Price, ProductTemplateId, Quantity

### Community 174 - "V1ProductCreatedDomainEvent"
Cohesion: 0.33
Nodes (6): CancellationToken, Task, ProductCreatedIntegrationEventPublishingHandler, V1ProductCreatedDomainEventHandlers, ProductId, V1ProductCreatedDomainEvent

### Community 175 - "PaginationResponse"
Cohesion: 0.09
Nodes (22): JsonElement, AuditLogDto, DateTimeOffset, PaginationResponse, HasNext, HasPrevious, NextPageNumber, PreviousPageNumber (+14 more)

### Community 176 - ".FixedWindow"
Cohesion: 0.22
Nodes (7): RateLimitPartitions, HttpContext, IConnectionMultiplexer, ILoggerFactory, RateLimitPartition, HttpContext, RateLimitPartition

### Community 177 - "Request"
Cohesion: 0.18
Nodes (11): StoreId, Request, Description, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name (+3 more)

### Community 178 - "PushMessage"
Cohesion: 0.13
Nodes (14): IReadOnlyDictionary, CancellationToken, IReadOnlyList, Task, IPushGateway, PushMessage, CancellationToken, ILogger (+6 more)

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

### Community 186 - ".Get"
Cohesion: 0.24
Nodes (5): CreateStoreRateLimitingPolicy, RateLimiterOptions, Action, IEnumerable, RateLimiterOptions

### Community 187 - "ReverseProxyOptions"
Cohesion: 0.18
Nodes (9): ForwardedHeadersOptions, ReverseProxyOptions, ForwardLimit, IsEnabled, TrustedNetworks, ReverseProxyOptionsValidator, IReadOnlyList, IConfiguration (+1 more)

### Community 188 - "BoundedCaptureStream"
Cohesion: 0.18
Nodes (7): HttpResponse, BoundedCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 189 - "ISearchLocalized"
Cohesion: 0.22
Nodes (7): File map _(Build)_, ISearchLocalized, Language, CancellationToken, DbContextEventData, InterceptionResult, ValueTask

### Community 190 - "ServiceAccountTokenCache"
Cohesion: 0.08
Nodes (18): CancellationToken, Task, IServiceAccountTokenProvider, KeycloakPaths, CancellationToken, DateTimeOffset, SemaphoreSlim, Task (+10 more)

### Community 191 - "DeviceRegistryReconciliationService"
Cohesion: 0.18
Nodes (13): GetActiveSessionIdsRequest, GetActiveSessionIdsResponse, UserSessionIds, IReadOnlyList, CancellationToken, Task, GetActiveSessionIdsRequestHandler, CancellationToken (+5 more)

### Community 192 - "CachedCaptchaService"
Cohesion: 0.29
Nodes (5): CancellationToken, IFusionCache, IOptions, Task, CachedCaptchaService

### Community 194 - "CorsOptions"
Cohesion: 0.25
Nodes (8): CorsOptions, AllowCredentials, AllowedHeaders, AllowedMethods, AllowedOrigins, MaxAgeInSeconds, CorsOptionsValidator, IReadOnlyList

### Community 195 - "InventoryOptions"
Cohesion: 0.17
Nodes (12): InventoryOptions, MaxOccurrencesPerSeries, SafetyBufferQuantity, StockReservationExpirySweepBatchSize, StockReservationExpirySweepCron, WarehouseGatewayBaseUrl, WarehouseGatewayProvider, WebhookSharedSecret (+4 more)

### Community 196 - "TokenCreateRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenCreateRateLimitingPolicy (+1 more)

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

### Community 202 - "CreateByEmail/Request.cs"
Cohesion: 0.05
Nodes (33): IAM.Domain.Users, IAM.Endpoints.Otp.VersionNeutral.SendForLogin, IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, IAM.Endpoints.Common.Validations, Constants, IResxLocalizer, IRuleBuilder, IRuleBuilderOptions (+25 more)

### Community 203 - "SmsRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, SmsRateLimitingPolicy (+1 more)

### Community 205 - "VersionNeutral/Create/Request.cs"
Cohesion: 0.10
Nodes (19): Notifications.Infrastructure.Devices.UpdateCurrentPushToken, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, Common.Domain.Devices, DeviceSessionConstants, Guid, Request, ClientId, DeviceId (+11 more)

### Community 210 - "ConfigureSwaggerOptions"
Cohesion: 0.28
Nodes (7): ApiVersionDescription, IApiVersionDescriptionProvider, IConfigureOptions, OpenApiInfo, IOptions, SwaggerGenOptions, ConfigureSwaggerOptions

### Community 212 - "Versioning/Setup.cs"
Cohesion: 0.20
Nodes (7): ApiVersionSet, asp_versioning, asp_versioning_builder, asp_versioning_conventions, Setup, IEndpointRouteBuilder, IServiceCollection

### Community 215 - "IProductsDbContext"
Cohesion: 0.09
Nodes (19): Products.Endpoints.Stores.v1.My.Create, DbSet, Store, IProductsDbContext, Products, ProductTemplates, Stores, CancellationToken (+11 more)

### Community 216 - "common_application_localization_resources"
Cohesion: 0.05
Nodes (45): common_application_localization_resources, Products.Endpoints.ProductTemplates.v1.Deactivate, Products.Endpoints.Stores.v1.My.RemoveProduct, Inventory.Endpoints.StockReservations.v1.Release, Common.Application.ModelBinders, Products.Endpoints.ProductTemplates.v1.Activate, Products.Endpoints.Stores.v1.RemoveProduct, entityframework_exceptions_common (+37 more)

### Community 217 - "FeatureFlags"
Cohesion: 0.33
Nodes (5): Checkout, FeatureFlags, IAM, Notifications, Products

### Community 218 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 220 - "FixedWindow"
Cohesion: 0.29
Nodes (7): CustomRateLimitingOptionsValidator, FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit, FixedWindowValidator

### Community 221 - "Keycloak realm as code"
Cohesion: 0.25
Nodes (7): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Secrets, Seed users (dev / test only)

### Community 222 - "Key decisions"
Cohesion: 0.29
Nodes (7): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Key decisions

### Community 223 - "Seeder"
Cohesion: 0.31
Nodes (7): ILogger, LoggerMessage, ProductsDbContext, Seeder, CancellationToken, List, Task

### Community 224 - ".MapEndpoint"
Cohesion: 0.40
Nodes (3): RouteGroupBuilder, RouteGroupBuilder, Setup

### Community 225 - "BackgroundJobsOptions"
Cohesion: 0.29
Nodes (7): BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount, BackgroundJobsOptionsValidator

### Community 226 - "NetGsmSmsGateway"
Cohesion: 0.16
Nodes (13): CancellationToken, Exception, HttpClient, ILogger, IOptions, JsonSerializerOptions, LoggerMessage, SendRequestBody (+5 more)

### Community 228 - "StronglyTypedIdBinder.cs"
Cohesion: 0.29
Nodes (5): IModelBinder, microsoft_aspnetcore_mvc_modelbinding, ModelBindingContext, StronglyTypedIdBinder, Task

### Community 229 - ".AddCommonOptions"
Cohesion: 0.40
Nodes (4): Setup, IConfiguration, IHostEnvironment, IServiceCollection

### Community 230 - "Inventory.Domain.StockReservations"
Cohesion: 0.20
Nodes (6): Inventory.Endpoints.StockReservations.v1.ReserveSeries, Inventory.Infrastructure.InterModuleRequestHandlers, Inventory.Domain.StockReservations, Inventory.Endpoints.StockReservations.v1.Get, Inventory.Endpoints.StockReservations.v1.Reserve, Common.InterModuleRequests.Inventory

### Community 231 - "Request"
Cohesion: 0.33
Nodes (6): Products.Endpoints.Stores.v1.My.Update, Request, Address, Description, Name, RequestValidator

### Community 232 - "Common.Application.Options"
Cohesion: 0.19
Nodes (13): Notifications.Infrastructure.Devices, Common.Infrastructure.RateLimiting, Products.Infrastructure.RateLimiting, Common.Infrastructure.Extensions, Common.Application.Options, IAM.Infrastructure.RateLimiting, masstransit, microsoft_aspnetcore_ratelimiting (+5 more)

### Community 234 - "Setup"
Cohesion: 0.33
Nodes (4): IApplicationBuilder, IWebHostEnvironment, Type, Setup

### Community 237 - "ProblemDetailsExtensions.cs"
Cohesion: 0.40
Nodes (3): ProblemDetails, ProblemDetailsExtensions, ICollection

### Community 238 - "microsoft_entityframeworkcore"
Cohesion: 0.11
Nodes (15): Products.Infrastructure.Persistence.Seeding, Common.Application.Persistence, Inventory.Infrastructure.Persistence.EntityConfigurations, Notifications.Domain.Devices, Common.Infrastructure.Persistence.EntityConfigurations, Common.Infrastructure.Persistence.ValueConverters, Notifications.Infrastructure.Persistence.EntityConfigurations, Products.Infrastructure.Persistence.EntityConfigurations (+7 more)

### Community 239 - "microsoft_extensions_logging"
Cohesion: 0.06
Nodes (29): Notifications.Application.Push, IAM.Infrastructure.Keycloak.Representations, Notifications.Infrastructure.Push, Notifications.Infrastructure.Push.Firebase, IAM.Infrastructure.Keycloak, firebaseadmin, firebaseadmin_messaging, google_apis_auth_oauth2 (+21 more)

### Community 241 - "RegisterRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, RegisterRateLimitingPolicy (+1 more)

### Community 242 - ".AddCustomSwagger"
Cohesion: 0.22
Nodes (7): IOpenApiSchema, ISchemaFilter, SchemaFilterContext, IConfigureOptions, IServiceCollection, SwaggerGenOptions, StronglyTypedIdSchemaFilter

### Community 243 - "CustomValidator"
Cohesion: 0.09
Nodes (27): AbstractValidator, Inventory.Endpoints.StockReservations.v1.Commit, DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator, IReadOnlyCollection (+19 more)

### Community 245 - "RemoveDefaultResponseSchemaFilter"
Cohesion: 0.40
Nodes (4): IOperationFilter, OpenApiOperation, OperationFilterContext, RemoveDefaultResponseSchemaFilter

### Community 246 - "KeycloakPermissionAuthorizationHandler.cs"
Cohesion: 0.10
Nodes (15): Common.Application.BackgroundJobs, BackgroundJobs.Telemetry, BackgroundJobs, IAM.Infrastructure.Auth, Inventory.Infrastructure.StockReservations, hangfire, hangfire_annotations, hangfire_dashboard (+7 more)

### Community 248 - "SendErrorBody"
Cohesion: 0.67
Nodes (3): SendErrorBody, Code, Message

### Community 249 - "Infrastructure/Setup.cs"
Cohesion: 0.09
Nodes (15): Common.InterModuleRequests, Inventory.Application.IntegrationEventHandlers, Common.IntegrationEvents, Common.Infrastructure.Localization, Common.Infrastructure.EventBus, Common.Infrastructure.FeatureManagement, Products.Application.Stores.DomainEventHandlers.v1, Common.Application.EventBus (+7 more)

### Community 250 - "KeycloakPermission"
Cohesion: 0.29
Nodes (3): KeycloakPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder

### Community 251 - ".DeactivateStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 252 - ".TryWriteAsync"
Cohesion: 0.40
Nodes (4): ProblemDetailsContext, ProblemDetailsServiceExtensions, IProblemDetailsService, Task

### Community 260 - ".AddProductToMyStoreAsync"
Cohesion: 0.18
Nodes (9): Products.Endpoints.Stores.v1.My.AddProduct, CancellationToken, ProductTemplate, RouteGroupBuilder, Store, Task, Endpoint, Response (+1 more)

### Community 263 - "Response"
Cohesion: 0.25
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegister, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 264 - "ProductTemplates/v1/Create/Request.cs"
Cohesion: 0.40
Nodes (5): Request, Brand, Color, Model, RequestValidator

### Community 267 - "AuditLogOptions"
Cohesion: 0.33
Nodes (6): AuditLogOptions, PerSchemaRetentionDays, PurgeBatchSize, RetentionDays, AuditLogOptionsValidator, Dictionary

### Community 270 - "DefaultResponsesOperationFilter"
Cohesion: 0.70
Nodes (3): OpenApiOperation, OperationFilterContext, DefaultResponsesOperationFilter

### Community 271 - "system_text_json_serialization"
Cohesion: 0.08
Nodes (23): Common.Application.JsonConverters, Constants, RequestValidator, RequestBody, Request, Body, Id, RequestValidator (+15 more)

### Community 272 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.07
Nodes (26): IAM.Endpoints.Users, Common.Application.FeatureManagement, IAM.Endpoints.Otp, Notifications.Application.Persistence, Common.InterModuleRequests.IAM, Common.InterModuleRequests.Contracts, IAM.Application.Captcha.Services, Notifications.Infrastructure.InterModuleRequestHandlers (+18 more)

### Community 274 - "GetProductRequest"
Cohesion: 0.39
Nodes (6): GetProductRequest, GetProductResponse, DefaultIdType, CancellationToken, Task, GetProductRequestHandler

### Community 275 - "Request"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForEmail, Request, CaptchaToken, Email, RequestValidator

### Community 276 - "SendPhoneOtpRequestHandler"
Cohesion: 0.17
Nodes (13): OtpDispatchErrors, SendPhoneOtpRequest, SendPhoneOtpResponse, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, CancellationToken (+5 more)

### Community 277 - "SendResponseBody"
Cohesion: 0.50
Nodes (4): SendResponseBody, Code, Description, JobId

### Community 279 - "Response"
Cohesion: 0.25
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.Create, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 281 - "TokenEndpointRepresentations.cs"
Cohesion: 0.20
Nodes (9): List, DecisionRepresentation, Result, PermissionRepresentation, ResourceName, Scopes, TokenErrorRepresentation, Error (+1 more)

### Community 282 - "Setup.Logger.cs"
Cohesion: 0.09
Nodes (19): Host, Host.Middlewares, Host.Infrastructure, elastic_serilog_sinks, microsoft_aspnetcore_httpoverrides, opentelemetry_metrics, opentelemetry_opentelemetrybuilder, opentelemetry_resources (+11 more)

### Community 284 - "Reserve/Request.cs"
Cohesion: 0.20
Nodes (10): DateTimeOffset, DefaultIdType, RequestBody, Request, Body, RequestBody, ProductId, Quantity (+2 more)

### Community 286 - ".UpdateCurrentPushToken"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 288 - "DummySmsGateway"
Cohesion: 0.38
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, DummySmsGateway

### Community 290 - "ICurrentUser"
Cohesion: 0.07
Nodes (20): ICurrentUser, Id, IdAsString, Roles, SessionId, ICollection, HttpContextExtensions, HttpContext (+12 more)

### Community 291 - ".ActivateProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 294 - ".SeedProductAsync"
Cohesion: 0.22
Nodes (7): CancellationToken, Task, CancellationToken, List, ProductTemplateId, StoreId, Task

### Community 295 - "Infrastructure/StringExtensions.cs"
Cohesion: 0.40
Nodes (3): opentelemetry_exporter, OtlpExportProtocol, StringExtensions

### Community 296 - "Request"
Cohesion: 0.50
Nodes (4): Request, Email, Otp, RequestValidator

### Community 297 - "ISmsGateway"
Cohesion: 0.13
Nodes (13): CancellationToken, Task, ISmsGateway, IConfiguration, IFusionCache, IOptions, IServiceCollection, Setup (+5 more)

### Community 299 - "Stores/Setup.cs"
Cohesion: 0.20
Nodes (7): Products.Endpoints.Stores.v1.Create, RouteGroupBuilder, Setup, RouteGroupBuilder, Endpoint, Response, Id

## Knowledge Gaps
- **892 isolated node(s):** `Id`, `IdAsString`, `Roles`, `SessionId`, `JwtClaimNames` (+887 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 2079 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **97 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `system_net`, `IAMModule.cs`, `AuditLogOptions`, `Common.Domain.ResultMonad`, `EmailOptions`, `Common.InterModuleRequests.Contracts`, `OutboxOptions`, `Setup.Logger.cs`, `Request`, `ObservabilityOptions`, `ConfigureSwaggerOptions.cs`, `Request`, `fluentvalidation`, `KeycloakOptions`, `CachingOptions`, `ReCaptchaService`, `RabbitMqOptions`, `SmsOptions`, `Resiliency/Setup.cs`, `IdentityScheme`, `ReverseProxyOptions`, `Setup.HealthChecks.cs`, `RequestLoggingOptions`, `CorsOptions`, `InventoryOptions`, `FullTextSearchOptions`, `OpenApiOptions`, `CreateByEmail/Request.cs`, `VersionNeutral/Create/Request.cs`, `PushOptions`, `FixedWindow`, `Common.Domain.StronglyTypedIds`, `OtpOptions`, `BackgroundJobsOptions`, `ResiliencyOptions`, `microsoft_extensions_dependencyinjection`, `microsoft_entityframeworkcore`, `microsoft_extensions_logging`, `CustomValidator`, `KeycloakPermissionAuthorizationHandler.cs`, `Infrastructure/Setup.cs`, `.AddCustomHealthChecks`?**
  _High betweenness centrality (0.173) - this node is a cross-community bridge._
- **Why does `Result` connect `Result` to `StockReservation`, `.AddProductToMyStoreAsync`, `.ListSessions`, `FirebasePushGateway`, `.AddProductAsync`, `Error`, `ProductTemplate`, `ApplicationUserId`, `.GetMeAsync`, `Response`, `.RegisterAsync`, `SendPhoneOtpRequestHandler`, `IEmailGateway`, `.RevokeToken`, `KeycloakTokenClient`, `.UpdateCurrentPushToken`, `IInventoryDbContext`, `.SingleAsResult`, `.ReserveSeriesAsync`, `DummySmsGateway`, `ICurrentUser`, `.ActivateProductTemplateAsync`, `DomainEvent`, `.RefreshToken`, `ISmsGateway`, `.RegisterAsync`, `PaginationResponse`, `Response`, `.AssignBasicRoleOrRollbackAsync`, `ReCaptchaService`, `PushMessage`, `SendEmailOtpRequestHandler`, `BrevoEmailGateway`, `.SearchProductTemplatesAsync`, `IDbContext`, `CachedCaptchaService`, `.SendCoreAsync`, `AuditableEntityResponse`, `Response`, `.SaveChangesAsync`, `.SearchStoresAsync`, `ResultTelemetryExtensions`, `IProductsDbContext`, `.GetProductAsync`, `Response`, `.SearchMyProductsAsync`, `NetGsmSmsGateway`, `.SearchStoreProductsAsync`, `.ReleaseStockReservationAsync`, `.IsRegisteredAsync`, `Response`, `.HandleWarehouseWebhookAsync`, `.DeactivateStoreAsync`, `.TapWhenFeatureEnabledAsync`?**
  _High betweenness centrality (0.138) - this node is a cross-community bridge._
- **Why does `Common.Domain.ResultMonad` connect `Common.Domain.ResultMonad` to `system_net`, `IAMModule.cs`, `ResultToResponseTransformer.cs`, `Error`, `microsoft_entityframeworkcore`, `microsoft_extensions_logging`, `Common.InterModuleRequests.Contracts`, `PushMessage`, `SendPhoneOtpRequestHandler`, `ResultTelemetryExtensions.cs`, `SendEmailOtpRequestHandler`, `DummyCaptchaService.cs`, `Resiliency/Setup.cs`, `IEmailGateway`, `SmsMessage`, `Common.Domain.StronglyTypedIds`, `Result`?**
  _High betweenness centrality (0.066) - this node is a cross-community bridge._
- **What connects `Id`, `IdAsString`, `Roles` to the rest of the system?**
  _892 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `StockReservation` be split into smaller, more focused modules?**
  _Cohesion score 0.046753246753246755 - nodes in this community are weakly interconnected._
- **Should `system_net` be split into smaller, more focused modules?**
  _Cohesion score 0.09986504723346828 - nodes in this community are weakly interconnected._
- **Should `KeycloakAdminClient` be split into smaller, more focused modules?**
  _Cohesion score 0.12896405919661733 - nodes in this community are weakly interconnected._