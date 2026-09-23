# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-09-23)

## Corpus Check
- 546 files · ~80,554 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 4578 nodes · 9040 edges · 374 communities (269 shown, 105 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 244 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `80a429cf`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- StockReservation
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
- HangfireCustomAuthorizationFilter
- IAuditableEntity
- IntegrationEventHandlerBase
- LogProductCatalogChangeHandler
- BoundedRequestCaptureStream
- DeviceRegistration
- IEmailGateway
- .RevokeSession
- Common.Infrastructure.Extensions
- IEvent
- RequestResponseBodyLoggingMiddleware
- .RequestTokensAsync
- Func
- Product
- NotificationsModule
- PersistenceQueryableExtensions
- DomainEvent
- KeycloakPermissionClient
- OutboxMessage
- CustomRateLimitingOptions
- TokenResponseRepresentation
- .RefreshToken
- ObservabilityOptions
- ConfigureSwaggerOptions.cs
- Request
- .SaveWithOutboxAsync
- CreateStoreRateLimitingPolicy
- KeycloakOptions
- .CreateTokens
- .PaginateAsync
- Outbox Misuse Check
- .AddPersistence
- Add Integration Event Command
- Response
- Products.Domain.Products
- ReCaptchaService
- IntegrationEventConverter
- SendEmailOtpRequestHandler
- BrevoEmailGateway
- AuditLogRetentionJobRegistrar
- Cross-Module Reference Violation
- OutboxMetricsJob
- .SearchProductTemplatesAsync
- Bogus Test Data
- KeycloakTokenClient
- AuditLogEntry
- RequestLoggingOptions
- .SendAsync
- BackgroundJobsService
- OutboxModule
- .AddNotificationsSignalR
- ValueObject
- IInterModuleRequest
- IStronglyTypedId
- FullTextSearchOptions
- Response
- .WriteAsync
- AsNoTracking Coverage Check
- IntegrationEventOutbox
- Response
- .CommitStockReservationAsync
- CheckRegistrationRateLimitingPolicy
- SeedingCompletionTracker
- StoreId
- .SearchStoresAsync
- Split-Deployment PoC
- NotificationsDbContext
- .Configure
- ResultTelemetryExtensions
- Store
- PushOptions
- Configuration-Driven Module Registration
- Program.cs
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
- microsoft_entityframeworkcore
- HttpWarehouseGateway
- StockLevel
- .IsRegisteredAsync
- KeycloakPermissionPolicyProvider
- IWarehouseGateway
- OutboxCleanupJob
- ProductsDbContext
- Response
- InterModuleRequestHandler
- JwtBearerConfigureOptions
- Response
- IModule
- ProductsModule
- .HandleWarehouseWebhookAsync
- .GetVariantAsync
- OtpVerifyRateLimitingPolicy
- NotificationsHub
- .AddCustomHealthChecks
- .TapWhenFeatureEnabledAsync
- GlobalExceptionHandlingMiddleware
- BaseDbContext
- EmailRateLimitingPolicy
- .SendAsync
- docker-compose.yml (Base Stack)
- .AddInfrastructure
- Stores/v1/Update/Request.cs
- Consumer Idempotency (IntegrationEventHandlerBase)
- SendRequestBody
- Response
- .AddKeycloakInfrastructure
- .WriteTooManyRequestsToResponse
- ResultToResponseTransformer.cs
- ProductTemplate
- .MapEndpoint
- IntegrationEvent
- .Schedule
- Setup
- Response
- SecurityHeadersMiddleware
- IInterModuleRequestClient
- IRecurringBackgroundJobs
- DeviceRegistryReconciliationService
- ProductTemplateId
- StockReservationExpirySweepJobRegistrar
- OutboxOptions
- .EnsureNoMigrationsPending
- .AddCustomSwagger
- Response
- ISmsGateway
- DeviceRegistryReconcileJobRegistrar
- CreateStockLevelOnProductCreatedHandler
- RedisOtpService
- IInventoryDbContext
- OtpServiceBase
- .ReserveSeriesAsync
- ThrottledEmailGateway
- For
- IAutoMigrateMarker.cs
- CurrentUser.cs
- IamModule
- PaginationRequest
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
- .Get
- ReverseProxyOptions
- BoundedCaptureStream
- V1StoreCreatedDomainEvent
- ServiceAccountTokenCache
- GetActiveSessionIdsRequest
- KeycloakPermissionRequirement
- Response
- CorsOptions
- InventoryOptions
- TokenCreateRateLimitingPolicy
- StockReservationId
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
- .RegisterAsync
- Request
- Result
- Common.Application.ModelBinders
- FeatureFlags
- Response
- Stores/v1/Create/Request.cs
- SendPhoneOtpRequestHandler
- Keycloak realm as code
- Key decisions
- Products.Domain.ProductTemplates
- .MapEndpoint
- BackgroundJobsOptions
- NetGsmSmsGateway
- UtcDateTimeOffsetConverter
- StronglyTypedIdBinder
- .AddCommonOptions
- Inventory.Domain.StockReservations
- .BindDeviceAsync
- Common.Application.Options
- .SingleAsResult
- Setup
- .Failure
- IssueVerificationTokenRequestHandler
- ProblemDetailsExtensions.cs
- Common.Domain.Events
- microsoft_extensions_logging
- .AddServices
- RegisterRateLimitingPolicy
- .Apply
- CustomValidator
- GetDeviceSessionsRequest
- AuditableEntity
- system_diagnostics
- .Capture
- SendErrorBody
- DeviceRegistrationId
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
- .AddProductToMyStoreAsync
- Request
- AuditableEntityResponse
- Response
- JobMetricsFilter.cs
- .AddAuthInfrastructure
- CachingEntryDefaults
- AuditLogOptions
- DevicesOptions
- .ListSessions
- DefaultResponsesOperationFilter
- .ReleaseStockReservationAsync
- SendForEmail/Endpoint.cs
- .AddNetGsm
- GetProductRequestHandler.cs
- .HandleRequirementAsync
- SmsOtpDispatchOutcome
- SendResponseBody
- SendForLogin/Request.cs
- Response
- SendForRegistration/Request.cs
- .TryReadFromJsonAsync
- Setup.Logger.cs
- ModulesOptions.cs
- RequestBody
- Setup
- NotificationsHub.cs
- Setup
- DummySmsGateway
- .SetRetryAfterHeader
- CurrentUser
- ConcurrencyConfiguratorExtensions.cs
- .AddServices
- .MapEndpoints
- .SeedProductAsync
- SecurityHeadersOptions.cs
- StringExtensions
- SignalROptions.cs
- ValidationContextExtensions
- .AddCommonCaching
- .EmailVerificationTokenValidation
- .PhoneNumberValidation
- system_runtime_compilerservices
- VerifyEmail/Request.cs
- Inventory.Domain.StockReservations.Errors
- IAM.Endpoints
- Products.Endpoints
- Users/VersionNeutral/Setup.cs
- Setup
- Tokens/VersionNeutral/Setup.cs
- IResult
- EnrichLogsWithUserInfoMiddleware.cs
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
- `File map _(Build)_` --references--> `ISearchLanguageResolver`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Application/Search/ISearchLanguageResolver.cs
- `Add search to a new entity _(Build checklist)_` --references--> `ISearchLocalized`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Domain/Entities/ISearchLocalized.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Local Infrastructure Stack** — docker_compose_postgres, docker_compose_rabbitmq, docker_compose_redis, docker_compose_aspire_dashboard [EXTRACTED 1.00]

## Communities (374 total, 105 thin omitted)

### Community 0 - "StockReservation"
Cohesion: 0.06
Nodes (31): Inventory.Domain.StockReservations.DomainEvents.v1, DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationCommitConflictDetectedDomainEvent, DateTimeOffset, DefaultIdType, StockReservationId (+23 more)

### Community 1 - "NotificationPayload"
Cohesion: 0.18
Nodes (13): Notifications.Application.Hubs, IHubContext, CancellationToken, IReadOnlyList, Task, INotificationDispatcher, Task, INotificationsClient (+5 more)

### Community 2 - "OutboxProcessor"
Cohesion: 0.22
Nodes (11): IPublishEndpoint, CancellationToken, Exception, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task (+3 more)

### Community 5 - "KeycloakAdminClient"
Cohesion: 0.17
Nodes (16): HttpRequestMessage, CancellationToken, Error, Func, HttpClient, HttpResponseMessage, IFusionCache, ILogger (+8 more)

### Community 6 - "RedisFixedWindowRateLimiter"
Cohesion: 0.13
Nodes (16): RateLimiter, RateLimiterStatistics, FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration, CancellationToken (+8 more)

### Community 7 - "ApplySearchLanguageInterceptor"
Cohesion: 0.17
Nodes (11): Common.Infrastructure.Persistence.Auditing, microsoft_entityframeworkcore_diagnostics, SaveChangesInterceptor, ApplyAuditingInterceptor, TimeProvider, ApplySearchLanguageInterceptor, Setup, IServiceCollection (+3 more)

### Community 8 - "FirebasePushGateway"
Cohesion: 0.17
Nodes (12): FirebaseApp, FirebaseMessaging, IDisposable, CancellationToken, Exception, IEnumerable, ILogger, IReadOnlyList (+4 more)

### Community 9 - ".RequireOtpTemplateForDefaultCulture"
Cohesion: 0.40
Nodes (4): Func, IConfiguration, IEnumerable, OtpTemplateConfiguration

### Community 10 - "Error"
Cohesion: 0.11
Nodes (16): Error, Key, ParameterName, StatusCode, SubErrors, Value, HttpStatusCode, ICollection (+8 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.10
Nodes (22): Common.Application.Search, Products.Endpoints.Stores, Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Products.Endpoints.ProductTemplates, Products.Infrastructure.Telemetry, IAM.Endpoints.Tokens.VersionNeutral.Revoke (+14 more)

### Community 12 - "ApplicationUserId"
Cohesion: 0.10
Nodes (25): ApplicationUserId, IsEmpty, Value, DefaultIdType, CancellationToken, IReadOnlyList, Task, IKeycloakAdminClient (+17 more)

### Community 13 - "ISearchLanguageResolver"
Cohesion: 0.14
Nodes (12): Configuration, ISearchLanguageResolver, UniversalConfig, SearchLanguageResolver, UniversalConfig, IOptions, Setup, IServiceCollection (+4 more)

### Community 14 - "UserRepresentation"
Cohesion: 0.07
Nodes (29): Dictionary, List, CredentialRepresentation, Temporary, Type, Value, ErrorRepresentation, Error (+21 more)

### Community 15 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.21
Nodes (10): AuthorizationHandler, CancellationToken, ClaimsPrincipal, IFusionCache, IHttpContextAccessor, IOptions, List, TimeProvider (+2 more)

### Community 16 - "EmailOptions"
Cohesion: 0.09
Nodes (24): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+16 more)

### Community 17 - "HangfireCustomAuthorizationFilter"
Cohesion: 0.33
Nodes (5): DashboardContext, IAuthorizationService, IDashboardAsyncAuthorizationFilter, HangfireCustomAuthorizationFilter, Task

### Community 18 - "IAuditableEntity"
Cohesion: 0.18
Nodes (10): IAuditableEntity, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, DateTimeOffset, CancellationToken, DbContextEventData (+2 more)

### Community 19 - "IntegrationEventHandlerBase"
Cohesion: 0.24
Nodes (11): IntegrationEventHandlerBase, MaxEventAge, CancellationToken, ConsumeContext, DefaultIdType, IFusionCache, ILogger, IOptions (+3 more)

### Community 20 - "LogProductCatalogChangeHandler"
Cohesion: 0.24
Nodes (8): CancellationToken, DefaultIdType, IFusionCache, ILogger, IOptions, LoggerMessage, Task, LogProductCatalogChangeHandler

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.18
Nodes (7): BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position, Stream

### Community 22 - "DeviceRegistration"
Cohesion: 0.19
Nodes (11): DateTimeOffset, Guid, DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive, PushToken (+3 more)

### Community 23 - "IEmailGateway"
Cohesion: 0.18
Nodes (11): IEmailGateway, CancellationToken, ILogger, LoggerMessage, Task, DummyEmailGateway, IConfiguration, IFusionCache (+3 more)

### Community 24 - ".RevokeSession"
Cohesion: 0.10
Nodes (18): DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse, IReadOnlyList, CancellationToken, RouteGroupBuilder, Task, Endpoint, CancellationToken (+10 more)

### Community 25 - "Common.Infrastructure.Extensions"
Cohesion: 0.20
Nodes (10): Common.Infrastructure.RateLimiting, Products.Infrastructure.RateLimiting, Common.Infrastructure.Extensions, IAM.Infrastructure.RateLimiting, microsoft_aspnetcore_ratelimiting, Policies, Policies, RateLimitingConstants (+2 more)

### Community 26 - "IEvent"
Cohesion: 0.12
Nodes (12): CancellationToken, Task, CancellationToken, Task, IEventHandlerWrapper, CancellationToken, Task, IEvent (+4 more)

### Community 27 - "RequestResponseBodyLoggingMiddleware"
Cohesion: 0.22
Nodes (7): IDiagnosticContext, HttpContext, IList, IOptions, PathString, RequestDelegate, RequestResponseBodyLoggingMiddleware

### Community 28 - ".RequestTokensAsync"
Cohesion: 0.21
Nodes (10): CancellationToken, Task, IKeycloakTokenClient, DateTimeOffset, KeycloakTokens, CancellationToken, Dictionary, Error (+2 more)

### Community 29 - "Func"
Cohesion: 0.27
Nodes (4): AsyncExtensions, SyncExtensions, Func, Task

### Community 30 - "Product"
Cohesion: 0.06
Nodes (37): Products.Endpoints.Stores.v1.AddProduct, DefaultIdType, ProductTemplate, ProductTemplateId, Store, StoreId, Product, Description (+29 more)

### Community 31 - "NotificationsModule"
Cohesion: 0.15
Nodes (10): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, NotificationsModule, ActivitySourceNames, MeterNames (+2 more)

### Community 32 - "PersistenceQueryableExtensions"
Cohesion: 0.33
Nodes (6): PersistenceQueryableExtensions, CancellationToken, Expression, Func, IQueryable, Task

### Community 33 - "DomainEvent"
Cohesion: 0.05
Nodes (37): AggregateRoot, Events, Id, Version, IReadOnlyCollection, List, IAggregateRoot, Events (+29 more)

### Community 34 - "KeycloakPermissionClient"
Cohesion: 0.13
Nodes (17): CancellationToken, IReadOnlyList, Task, IKeycloakPermissionClient, IReadOnlyList, GrantedPermission, CancellationToken, Dictionary (+9 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.10
Nodes (20): IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, DateTimeOffset, OutboxMessage (+12 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.07
Nodes (24): CheckRegistrationRateLimitingPolicy, CreateStoreRateLimitingPolicy, EmailRateLimitingPolicy, OtpVerifyRateLimitingPolicy, RegisterRateLimitingPolicy, SmsRateLimitingPolicy, CustomRateLimitingOptions, CheckRegistration (+16 more)

### Community 37 - "TokenResponseRepresentation"
Cohesion: 0.12
Nodes (15): List, DecisionRepresentation, Result, PermissionRepresentation, ResourceName, Scopes, TokenErrorRepresentation, Error (+7 more)

### Community 38 - ".RefreshToken"
Cohesion: 0.15
Nodes (11): IAM.Endpoints.Tokens.VersionNeutral.Refresh, CancellationToken, RouteGroupBuilder, Task, Endpoint, DateTimeOffset, Response, AccessToken (+3 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.08
Nodes (24): IHostBuilder, KeyValuePair, LoggerConfiguration, LoggerMinimumLevelConfiguration, ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl (+16 more)

### Community 40 - "ConfigureSwaggerOptions.cs"
Cohesion: 0.24
Nodes (8): asp_versioning_apiexplorer, Host.Swagger, ISchemaFilter, microsoft_aspnetcore_mvc_apiexplorer, microsoft_openapi, StronglyTypedIdSchemaFilter, swashbuckle_aspnetcore_swaggergen, system_text_json_nodes

### Community 41 - "Request"
Cohesion: 0.07
Nodes (27): Guid, Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FirstName (+19 more)

### Community 42 - ".SaveWithOutboxAsync"
Cohesion: 0.27
Nodes (9): OutboxSaveHelper, CancellationToken, DbContext, Exception, Func, ILogger, LoggerMessage, Task (+1 more)

### Community 43 - "CreateStoreRateLimitingPolicy"
Cohesion: 0.14
Nodes (12): CancellationToken, Func, HttpContext, IOptions, IProblemDetailsService, IResxLocalizer, OnRejectedContext, RateLimitPartition (+4 more)

### Community 44 - "KeycloakOptions"
Cohesion: 0.14
Nodes (14): KeycloakOptions, AttemptTimeoutSeconds, Authority, BaseUrl, DecisionCacheMaxDurationSeconds, Realm, RequireHttpsMetadata, ResourceClientId (+6 more)

### Community 45 - ".CreateTokens"
Cohesion: 0.16
Nodes (13): OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, VerifyPhoneOtpResponseExtensions, CancellationToken (+5 more)

### Community 46 - ".PaginateAsync"
Cohesion: 0.17
Nodes (11): How it works, One-time setup _(Build, in a migration)_, Query path, Ranking, Write path, PaginationQueryableExtensions, CancellationToken, Expression (+3 more)

### Community 48 - ".AddPersistence"
Cohesion: 0.15
Nodes (11): IDatabaseSeeder, Priority, CancellationToken, Task, CancellationToken, IServiceScopeFactory, Task, ProductsDatabaseSeeder (+3 more)

### Community 50 - "Response"
Cohesion: 0.13
Nodes (14): RouteGroupBuilder, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate, CreatedOn, Email (+6 more)

### Community 51 - "Products.Domain.Products"
Cohesion: 0.08
Nodes (18): Products.Endpoints.Stores.v1.Search, Products.Endpoints.Products.v1.My.Get, Products.Endpoints.Products.v1.Search, Products.Domain.Products, Common.Application.DTOs, Products.Endpoints.Stores.v1.Get, Products.Endpoints.Products.v1.Update, Products.Endpoints.Products.v1.My.Search (+10 more)

### Community 52 - "ReCaptchaService"
Cohesion: 0.06
Nodes (36): DateTime, FormUrlEncodedContent, ReCaptchaResponse, CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey (+28 more)

### Community 53 - "IntegrationEventConverter"
Cohesion: 0.25
Nodes (6): IEntityTypeConfiguration, IntegrationEventConverter, JsonSerializerOptions, ModelBuilder, EntityTypeBuilder, OutboxMessageConfig

### Community 54 - "SendEmailOtpRequestHandler"
Cohesion: 0.16
Nodes (13): EmailOtpDispatchErrors, EmailOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, SendEmailOtpRequest, SendEmailOtpResponse, CancellationToken (+5 more)

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
Nodes (11): LikePattern, CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response (+3 more)

### Community 61 - "KeycloakTokenClient"
Cohesion: 0.29
Nodes (8): JsonWebTokenHandler, Exception, HttpClient, ILogger, IOptions, LoggerMessage, TimeProvider, KeycloakTokenClient

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
Cohesion: 0.19
Nodes (9): IBackgroundJobClientV2, IBackgroundJobs, BackgroundJobsService, Action, DateTimeOffset, Expression, Func, Task (+1 more)

### Community 66 - "OutboxModule"
Cohesion: 0.13
Nodes (16): Action, Exception, IApplicationBuilder, IEndpointRouteBuilder, IEnumerable, IHostApplicationLifetime, ILogger, ILoggerFactory (+8 more)

### Community 67 - ".AddNotificationsSignalR"
Cohesion: 0.17
Nodes (10): HubConnectionContext, IUserIdProvider, RedisOptions, IConfiguration, IConfigureOptions, IConnectionMultiplexer, IServiceCollection, IUserIdProvider (+2 more)

### Community 68 - "ValueObject"
Cohesion: 0.25
Nodes (4): Common.Domain, IComparable, ValueObject, IEnumerable

### Community 69 - "IInterModuleRequest"
Cohesion: 0.15
Nodes (12): IInterModuleRequest, GetSeedUserIdsRequest, GetSeedUserIdsResponse, ICollection, CancellationToken, Task, GetSeedUserIdsRequestHandler, CancellationToken (+4 more)

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
Cohesion: 0.14
Nodes (13): Lock, IIntegrationEventOutbox, IntegrationEventOutbox, HasPending, List, Setup, IServiceCollection, CancellationToken (+5 more)

### Community 76 - "Response"
Cohesion: 0.22
Nodes (8): RouteGroupBuilder, Endpoint, Response, Address, Description, Name, OwnerId, ProductCount

### Community 77 - ".CommitStockReservationAsync"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 78 - "CheckRegistrationRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, CheckRegistrationRateLimitingPolicy (+1 more)

### Community 79 - "SeedingCompletionTracker"
Cohesion: 0.15
Nodes (8): SeedingCompletionTracker, CancellationToken, Exception, Task, Setup, IOptions, IServiceCollection, TaskCompletionSource

### Community 80 - "StoreId"
Cohesion: 0.08
Nodes (20): Products.Endpoints.Stores.v1.Deactivate, Products.Endpoints.Stores.v1.My.Create, StoreId, DefaultIdType, StoreId, RequestBody, Request, Body (+12 more)

### Community 81 - ".SearchStoresAsync"
Cohesion: 0.15
Nodes (12): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Address (+4 more)

### Community 82 - "Split-Deployment PoC"
Cohesion: 0.18
Nodes (9): Concurrent safety, Cross-process call path, Files added by this PoC, How it works, How to run, Split-Deployment PoC, What this proves, README (Boilerplate Overview) (+1 more)

### Community 83 - "NotificationsDbContext"
Cohesion: 0.15
Nodes (13): DbSet, INotificationsDbContext, DeviceRegistrations, DbContextOptions, DbSet, ILogger, TimeProvider, NotificationsDbContext (+5 more)

### Community 84 - ".Configure"
Cohesion: 0.11
Nodes (18): AuditableEntityConfiguration, EntityTypeBuilder, EntityTypeBuilder, DomainEventConverter, JsonSerializerOptions, StronglyTypedIdValueConverter, DefaultIdType, EntityTypeBuilder (+10 more)

### Community 85 - "ResultTelemetryExtensions"
Cohesion: 0.38
Nodes (4): Activity, ResultTelemetryExtensions, ActivitySource, Task

### Community 86 - "Store"
Cohesion: 0.08
Nodes (22): File map _(Build)_, ISearchLocalized, Language, StoreId, V1StoreAddressUpdatedDomainEvent, StoreId, V1StoreDescriptionUpdatedDomainEvent, StoreId (+14 more)

### Community 87 - "PushOptions"
Cohesion: 0.12
Nodes (20): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri, PushOptions (+12 more)

### Community 89 - "Program.cs"
Cohesion: 0.22
Nodes (6): ConfigurationManager, Host, Host.Configurations, Setup, Program, WebApplicationBuilder

### Community 90 - ".GetProductAsync"
Cohesion: 0.25
Nodes (10): GetStockLevelRequest, GetStockLevelResponse, DefaultIdType, CancellationToken, Task, GetStockLevelRequestHandler, CancellationToken, DefaultIdType (+2 more)

### Community 91 - "RequestBody"
Cohesion: 0.40
Nodes (5): RequestBody, Description, Name, Price, Quantity

### Community 92 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.08
Nodes (25): Products.Endpoints.Stores.v1.My.Update, IAM.Endpoints.Users.VersionNeutral.Search, Common.Domain.StronglyTypedIds, Inventory.Endpoints.StockReservations.v1.WebhookCallback, Inventory.Infrastructure.Persistence.EntityConfigurations, Common.Application.JsonConverters, Common.Domain.Entities, Products.Domain.Stores (+17 more)

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
Cohesion: 0.18
Nodes (11): IServiceCollection, Setup, CancellationToken, Exception, Guid, ILogger, IOptions, LoggerMessage (+3 more)

### Community 98 - ".SearchStoreProductsAsync"
Cohesion: 0.17
Nodes (11): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Description (+3 more)

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
Cohesion: 0.20
Nodes (8): PerformedContext, BackgroundJobsTelemetry, ActivitySource, ConcurrentDictionary, Counter, Histogram, Meter, ObservableGauge

### Community 104 - "RequestBody"
Cohesion: 0.40
Nodes (5): RequestBody, Description, Name, Price, Quantity

### Community 105 - "Response"
Cohesion: 0.18
Nodes (9): IAM.Endpoints.Users.VersionNeutral.SelfRegisterByEmail, RouteGroupBuilder, Endpoint, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken (+1 more)

### Community 106 - "microsoft_entityframeworkcore"
Cohesion: 0.08
Nodes (21): Notifications.Application.Otp, Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Common.InterModuleRequests, Common.Application.Persistence, Common.Application.Caching, Notifications.Application.Persistence, Notifications.Infrastructure.Telemetry (+13 more)

### Community 107 - "HttpWarehouseGateway"
Cohesion: 0.31
Nodes (8): ReleaseResponseBody, CancellationToken, HttpClient, HttpResponseMessage, Task, HttpWarehouseGateway, ReleaseRequestBody, ReleaseResponseBody

### Community 108 - "StockLevel"
Cohesion: 0.11
Nodes (12): Inventory.Domain.StockLevels.DomainEvents.v1, StronglyTypedIdHelper, DefaultIdType, StockLevelId, V1StockLevelCreatedDomainEvent, DefaultIdType, StockLevel, ProductId (+4 more)

### Community 109 - ".IsRegisteredAsync"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, IsRegistered

### Community 110 - "KeycloakPermissionPolicyProvider"
Cohesion: 0.24
Nodes (8): AuthorizationOptions, AuthorizationPolicy, DefaultAuthorizationPolicyProvider, IAuthorizationPolicyProvider, ConcurrentDictionary, IOptions, Task, KeycloakPermissionPolicyProvider

### Community 111 - "IWarehouseGateway"
Cohesion: 0.20
Nodes (9): CancellationToken, Task, IWarehouseGateway, CancellationToken, Task, DummyWarehouseGateway, IConfiguration, IServiceCollection (+1 more)

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
Cohesion: 0.21
Nodes (8): IConsumer, IInterModuleRequestHandler, CancellationToken, Task, InterModuleRequestHandler, CancellationToken, ConsumeContext, Task

### Community 116 - "JwtBearerConfigureOptions"
Cohesion: 0.20
Nodes (10): IAllowAnonymous, IConfigureNamedOptions, HttpContext, HttpStatusCode, IOptions, IProblemDetailsService, IResxLocalizer, JwtBearerOptions (+2 more)

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

### Community 122 - "OtpVerifyRateLimitingPolicy"
Cohesion: 0.13
Nodes (15): IRateLimiterPolicy, CancellationToken, Func, IOptions, OnRejectedContext, ValueTask, OtpVerifyRateLimitingPolicy, OnRejected (+7 more)

### Community 123 - "NotificationsHub"
Cohesion: 0.20
Nodes (7): Hub, NotificationGroupName, Exception, ILogger, LoggerMessage, Task, NotificationsHub

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

### Community 129 - ".SendAsync"
Cohesion: 0.20
Nodes (6): CancellationToken, SendRequestBody, Task, SendMessageBody, Msg, No

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.48
Nodes (7): Observability (OpenTelemetry), docker-compose.yml (Base Stack), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), Postgres Service (mm.postgres), RabbitMQ Service (mm.rabbitmq), Redis Service (mm.redis)

### Community 131 - ".AddInfrastructure"
Cohesion: 0.16
Nodes (12): HostOptions, IMiddleware, JsonOptions, Assembly, IConfiguration, IResxLocalizer, IServiceCollection, IWebHostEnvironment (+4 more)

### Community 132 - "Stores/v1/Update/Request.cs"
Cohesion: 0.20
Nodes (10): Products.Endpoints.Stores.v1.Update, RequestBody, Request, Body, Id, RequestBody, Address, Description (+2 more)

### Community 134 - "SendRequestBody"
Cohesion: 0.25
Nodes (8): SendContact, IReadOnlyList, SendRequestBody, HtmlContent, Sender, Subject, TextContent, To

### Community 135 - "Response"
Cohesion: 0.18
Nodes (10): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, DateTimeOffset, Response, ClientId, DeviceName, Id, IpAddress, IsCurrent (+2 more)

### Community 136 - ".AddKeycloakInfrastructure"
Cohesion: 0.16
Nodes (10): CancellationToken, Task, IServiceAccountTokenProvider, HttpClient, IOptions, TimeProvider, ServiceAccountTokenProvider, IOptions (+2 more)

### Community 137 - ".WriteTooManyRequestsToResponse"
Cohesion: 0.13
Nodes (14): PartitionedRateLimiter, CancellationToken, Func, HttpContext, IConfiguration, IProblemDetailsService, IReadOnlyList, IResxLocalizer (+6 more)

### Community 138 - "ResultToResponseTransformer.cs"
Cohesion: 0.07
Nodes (32): AspNetResult, Common.Application.EndpointFilters, IEndpointFilter, IFeatureManagerSnapshot, microsoft_aspnetcore_hosting, microsoft_aspnetcore_http_iresult, microsoft_extensions_localization, ResxLocalizer (+24 more)

### Community 139 - "ProductTemplate"
Cohesion: 0.15
Nodes (11): IReadOnlyList, List, ProductTemplate, Brand, Color, IsActive, Model, Products (+3 more)

### Community 140 - ".MapEndpoint"
Cohesion: 0.40
Nodes (3): RouteGroupBuilder, RouteGroupBuilder, Setup

### Community 141 - "IntegrationEvent"
Cohesion: 0.22
Nodes (9): IReadOnlyList, IntegrationEvent, CreatedOn, Id, DateTimeOffset, DefaultIdType, ProductCreatedIntegrationEvent, StoreCreatedIntegrationEvent (+1 more)

### Community 142 - ".Schedule"
Cohesion: 0.31
Nodes (6): Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 143 - "Setup"
Cohesion: 0.11
Nodes (17): LoadAll, ModuleRegistry, Names, Type, Setup, Assembly, Exception, IApplicationBuilder (+9 more)

### Community 144 - "Response"
Cohesion: 0.13
Nodes (14): RouteGroupBuilder, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate, CreatedOn, Email (+6 more)

### Community 145 - "SecurityHeadersMiddleware"
Cohesion: 0.22
Nodes (7): IAuthenticationSchemeProvider, IApplicationBuilder, HttpContext, IOptions, RequestDelegate, Task, SecurityHeadersMiddleware

### Community 146 - "IInterModuleRequestClient"
Cohesion: 0.07
Nodes (28): IClientFactory, IInterModuleRequestClient, CancellationToken, Task, MassTransitInterModuleRequestClient, CancellationToken, IOptions, Task (+20 more)

### Community 147 - "IRecurringBackgroundJobs"
Cohesion: 0.14
Nodes (13): RecurringJobOptions, IRecurringBackgroundJobs, Action, Expression, Func, Task, RecurringBackgroundJobsService, Action (+5 more)

### Community 148 - "DeviceRegistryReconciliationService"
Cohesion: 0.16
Nodes (9): CancellationToken, ILogger, IOptions, LoggerMessage, Task, DeviceRegistryReconciliationService, IServiceCollection, RouteGroupBuilder (+1 more)

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

### Community 153 - ".AddCustomSwagger"
Cohesion: 0.15
Nodes (11): IOperationFilter, JsonValue, OpenApiOperation, OperationFilterContext, RemoveDefaultResponseSchemaFilter, IConfigureOptions, IServiceCollection, SwaggerGenOptions (+3 more)

### Community 154 - "Response"
Cohesion: 0.20
Nodes (8): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, RouteGroupBuilder, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 155 - "ISmsGateway"
Cohesion: 0.13
Nodes (13): CancellationToken, Task, ISmsGateway, SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, SmsMessage (+5 more)

### Community 156 - "DeviceRegistryReconcileJobRegistrar"
Cohesion: 0.31
Nodes (7): CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task, DeviceRegistryReconcileJobRegistrar

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

### Community 165 - "CurrentUser.cs"
Cohesion: 0.40
Nodes (3): Common.Infrastructure.Auth.Services, Common.Infrastructure.Auth, system_security_claims

### Community 166 - "IamModule"
Cohesion: 0.18
Nodes (10): Action, IApplicationBuilder, IEnumerable, RateLimiterOptions, IamModule, ActivitySourceNames, MeterNames, Name (+2 more)

### Community 167 - "PaginationRequest"
Cohesion: 0.07
Nodes (34): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Stores.v1.My.AuditLog, Products.Endpoints.Products.v1.AuditLog, PaginationRequest, PageNumber, PageSize, Skip, Take (+26 more)

### Community 168 - "Response"
Cohesion: 0.29
Nodes (5): RouteGroupBuilder, Setup, RouteGroupBuilder, Response, Id

### Community 169 - "SendSecurityAlertRequestHandler"
Cohesion: 0.16
Nodes (15): IReadOnlyDictionary, SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, CancellationToken, IReadOnlyList, Task (+7 more)

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
Nodes (5): Notifications.Application.Push, Notifications.Infrastructure.Push.Firebase, firebaseadmin, firebaseadmin_messaging, google_apis_auth_oauth2

### Community 175 - "PaginationResponse"
Cohesion: 0.07
Nodes (27): JsonElement, AuditLogDto, DateTimeOffset, PaginationResponse, HasNext, HasPrevious, NextPageNumber, PreviousPageNumber (+19 more)

### Community 176 - ".FixedWindow"
Cohesion: 0.12
Nodes (14): FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit, RateLimitPartitions, HttpContext, IConnectionMultiplexer (+6 more)

### Community 177 - "Request"
Cohesion: 0.18
Nodes (11): StoreId, Request, Description, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name (+3 more)

### Community 178 - ".AddPushServices"
Cohesion: 0.22
Nodes (8): CancellationToken, ILogger, LoggerMessage, Task, DummyPushGateway, IConfiguration, IServiceCollection, Setup

### Community 179 - "CachingOptions"
Cohesion: 0.16
Nodes (14): CachingOptions, AllowInMemoryOnlyInProduction, EntryDefaults, IdempotencyKeyDuration, IdempotencyL1Duration, Redis, UseRedis, CachingOptionsValidator (+6 more)

### Community 180 - "SmsOptions"
Cohesion: 0.10
Nodes (21): SmsOptions, AppName, AttemptTimeoutSeconds, BaseUrl, MaxPerDay, MaxPerPhoneNumberPerDay, MaxRetryAttempts, MsgHeader (+13 more)

### Community 181 - "RabbitMqOptions"
Cohesion: 0.11
Nodes (17): RabbitMqOptions, DefaultConcurrentMessageLimit, DefaultPrefetchCount, Host, Password, Port, RetryIntervalDeltaMs, RetryLimit (+9 more)

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
Cohesion: 0.50
Nodes (3): Action, IEnumerable, RateLimiterOptions

### Community 187 - "ReverseProxyOptions"
Cohesion: 0.22
Nodes (8): ForwardedHeadersOptions, ReverseProxyOptions, ForwardLimit, IsEnabled, TrustedNetworks, IReadOnlyList, IConfiguration, IServiceCollection

### Community 188 - "BoundedCaptureStream"
Cohesion: 0.14
Nodes (8): SeekOrigin, HttpResponse, BoundedCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 189 - "V1StoreCreatedDomainEvent"
Cohesion: 0.26
Nodes (9): DomainEventHandlerBase, IEventHandler, CancellationToken, Task, SimulateSomeBusinessHandler, StoreCreatedIntegrationEventPublishingHandler, V1StoreCreatedDomainEventHandlers, StoreId (+1 more)

### Community 190 - "ServiceAccountTokenCache"
Cohesion: 0.11
Nodes (11): KeycloakPaths, CancellationToken, DateTimeOffset, SemaphoreSlim, Task, TimeSpan, ServiceAccountTokenCache, AccessToken (+3 more)

### Community 191 - "GetActiveSessionIdsRequest"
Cohesion: 0.44
Nodes (7): GetActiveSessionIdsRequest, GetActiveSessionIdsResponse, UserSessionIds, IReadOnlyList, CancellationToken, Task, GetActiveSessionIdsRequestHandler

### Community 192 - "KeycloakPermissionRequirement"
Cohesion: 0.20
Nodes (6): IAuthorizationRequirement, KeycloakPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder, KeycloakPermissionRequirement, Permission

### Community 193 - "Response"
Cohesion: 0.18
Nodes (9): RouteGroupBuilder, Setup, RouteGroupBuilder, Response, AvailableQuantity, Description, Name, Price (+1 more)

### Community 194 - "CorsOptions"
Cohesion: 0.25
Nodes (8): CorsOptions, AllowCredentials, AllowedHeaders, AllowedMethods, AllowedOrigins, MaxAgeInSeconds, CorsOptionsValidator, IReadOnlyList

### Community 195 - "InventoryOptions"
Cohesion: 0.17
Nodes (12): InventoryOptions, MaxOccurrencesPerSeries, SafetyBufferQuantity, StockReservationExpirySweepBatchSize, StockReservationExpirySweepCron, WarehouseGatewayBaseUrl, WarehouseGatewayProvider, WebhookSharedSecret (+4 more)

### Community 196 - "TokenCreateRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenCreateRateLimitingPolicy (+1 more)

### Community 197 - "StockReservationId"
Cohesion: 0.12
Nodes (13): Inventory.Endpoints.StockReservations.v1.Reserve, DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationReservedDomainEvent, DefaultIdType, StockReservationId, CancellationToken (+5 more)

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
Cohesion: 0.09
Nodes (26): common_application_localization_resources, IAM.Domain.Users, Products.Endpoints.Probe.v1, Inventory.Endpoints.StockReservations.v1.Get, Common.Domain.Extensions, Common.Application.Pagination, Notifications.Infrastructure.Devices.UpdateCurrentPushToken, Common.Application.Validation (+18 more)

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

### Community 213 - ".RegisterAsync"
Cohesion: 0.05
Nodes (37): BindDeviceSessionRequest, BindDeviceSessionResponse, Guid, CancellationToken, Task, ICaptchaService, CancellationToken, IFeatureManager (+29 more)

### Community 214 - "Request"
Cohesion: 0.20
Nodes (10): Guid, Request, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken, Password (+2 more)

### Community 215 - "Result"
Cohesion: 0.03
Nodes (72): ICurrentUser, Id, IdAsString, Roles, SessionId, ICollection, CancellationToken, Task (+64 more)

### Community 216 - "Common.Application.ModelBinders"
Cohesion: 0.09
Nodes (21): Products.Endpoints.Stores.v1.My.RemoveProduct, IAM.Endpoints.Users.VersionNeutral.Get, Inventory.Endpoints.StockReservations.v1.Release, Common.Application.ModelBinders, Products.Endpoints.Stores.v1.RemoveProduct, microsoft_aspnetcore_mvc_modelbinding, Request, Id (+13 more)

### Community 217 - "FeatureFlags"
Cohesion: 0.33
Nodes (5): Checkout, FeatureFlags, IAM, Notifications, Products

### Community 218 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 219 - "Stores/v1/Create/Request.cs"
Cohesion: 0.13
Nodes (12): Products.Endpoints.Stores.v1.Create, RouteGroupBuilder, Setup, RouteGroupBuilder, Request, Address, Description, Name (+4 more)

### Community 220 - "SendPhoneOtpRequestHandler"
Cohesion: 0.27
Nodes (8): SendPhoneOtpRequest, SendPhoneOtpResponse, CancellationToken, IFusionCache, IOptions, RequestLocalizationOptions, Task, SendPhoneOtpRequestHandler

### Community 221 - "Keycloak realm as code"
Cohesion: 0.25
Nodes (7): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Secrets, Seed users (dev / test only)

### Community 222 - "Key decisions"
Cohesion: 0.29
Nodes (7): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Key decisions

### Community 223 - "Products.Domain.ProductTemplates"
Cohesion: 0.07
Nodes (24): Products.Infrastructure.Persistence.Seeding, Products.Endpoints.ProductTemplates.v1.Deactivate, Products.Endpoints.ProductTemplates.v1.Search, Products.Endpoints.ProductTemplates.v1.Create, Products.Endpoints.ProductTemplates.v1.Get, Products.Domain.ProductTemplates, Products.Endpoints.ProductTemplates.v1.Activate, Constants (+16 more)

### Community 224 - ".MapEndpoint"
Cohesion: 0.40
Nodes (3): RouteGroupBuilder, RouteGroupBuilder, Setup

### Community 225 - "BackgroundJobsOptions"
Cohesion: 0.29
Nodes (7): BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount, BackgroundJobsOptionsValidator

### Community 226 - "NetGsmSmsGateway"
Cohesion: 0.32
Nodes (7): Exception, HttpClient, ILogger, IOptions, JsonSerializerOptions, LoggerMessage, NetGsmSmsGateway

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
Cohesion: 0.09
Nodes (17): asp_versioning, asp_versioning_builder, asp_versioning_conventions, Inventory.Infrastructure.InterModuleRequestHandlers, Inventory.Endpoints, Common.Endpoints.Versioning, Inventory.Application.Persistence, Inventory.Application.IntegrationEventHandlers (+9 more)

### Community 231 - ".BindDeviceAsync"
Cohesion: 0.33
Nodes (7): CancellationToken, Exception, Guid, ILogger, LoggerMessage, Task, LoginCompletion

### Community 232 - "Common.Application.Options"
Cohesion: 0.04
Nodes (59): Common.Infrastructure.Modules, IAM.Endpoints.Captcha.VersionNeutral, Notifications.Infrastructure.Devices, Notifications.Infrastructure.Email, Products.Endpoints.Probe, Outbox, Common.Application.BackgroundJobs, Common.Infrastructure.Persistence.Outbox (+51 more)

### Community 233 - ".SingleAsResult"
Cohesion: 0.31
Nodes (4): CollectionExtensions, Func, ICollection, IEnumerable

### Community 234 - "Setup"
Cohesion: 0.33
Nodes (4): IApplicationBuilder, IWebHostEnvironment, Type, Setup

### Community 235 - ".Failure"
Cohesion: 0.31
Nodes (4): Success, Func, Task, Action

### Community 236 - "IssueVerificationTokenRequestHandler"
Cohesion: 0.39
Nodes (6): IssueVerificationTokenRequest, IssueVerificationTokenResponse, CancellationToken, IOptions, Task, IssueVerificationTokenRequestHandler

### Community 237 - "ProblemDetailsExtensions.cs"
Cohesion: 0.40
Nodes (3): ProblemDetails, ProblemDetailsExtensions, ICollection

### Community 238 - "Common.Domain.Events"
Cohesion: 0.09
Nodes (8): Products.Domain.Products.DomainEvents.v1, Common.IntegrationEvents, Common.Domain.Events, Common.Application.Persistence.Outbox, Common.Infrastructure.EventBus, Products.Application.Products.DomainEventHandlers.v1, Products.Application.Stores.DomainEventHandlers.v1, Products.Domain.Stores.DomainEvents.v1

### Community 239 - "microsoft_extensions_logging"
Cohesion: 0.07
Nodes (27): Notifications.Application.Sms, IAM.Endpoints.Users, IAM.Infrastructure.Keycloak.Representations, Notifications.Infrastructure.Email.Brevo, IAM.Endpoints.Otp, Common.InterModuleRequests.IAM, Notifications.Application.Email, IAM.Infrastructure.Telemetry (+19 more)

### Community 240 - ".AddServices"
Cohesion: 0.15
Nodes (11): BackgroundJobsModule, ActivitySourceNames, MeterNames, Name, StartupPriority, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder (+3 more)

### Community 241 - "RegisterRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, RegisterRateLimitingPolicy (+1 more)

### Community 243 - "CustomValidator"
Cohesion: 0.04
Nodes (59): AbstractValidator, Inventory.Endpoints.StockReservations.v1.Commit, IAM.Endpoints.Otp.VersionNeutral.SendForEmail, CustomRateLimitingOptionsValidator, FixedWindowValidator, DatabaseOptions, ConnectionString, DatabaseOptionsValidator (+51 more)

### Community 244 - "GetDeviceSessionsRequest"
Cohesion: 0.39
Nodes (7): DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, IReadOnlyList, CancellationToken, Task, GetDeviceSessionsRequestHandler

### Community 245 - "AuditableEntity"
Cohesion: 0.25
Nodes (8): AuditableEntity, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, Version, DateTimeOffset

### Community 246 - "system_diagnostics"
Cohesion: 0.13
Nodes (13): LoginMethods, SessionRevokedReasons, ActivitySource, Counter, Meter, InventoryTelemetry, ActivitySource, Counter (+5 more)

### Community 248 - "SendErrorBody"
Cohesion: 0.67
Nodes (3): SendErrorBody, Code, Message

### Community 249 - "DeviceRegistrationId"
Cohesion: 0.25
Nodes (4): DefaultIdType, DeviceRegistrationId, EntityTypeBuilder, DeviceRegistrationConfiguration

### Community 250 - "Response"
Cohesion: 0.25
Nodes (7): RouteGroupBuilder, Endpoint, Response, Description, Name, Price, Quantity

### Community 251 - "Products/v1/My/Update/Request.cs"
Cohesion: 0.33
Nodes (6): Products.Endpoints.Products.v1.My.Update, RequestBody, Request, Body, Id, RequestValidator

### Community 252 - ".TryWriteAsync"
Cohesion: 0.40
Nodes (4): ProblemDetailsContext, ProblemDetailsServiceExtensions, IProblemDetailsService, Task

### Community 260 - ".AddProductToMyStoreAsync"
Cohesion: 0.11
Nodes (17): Products.Endpoints.Stores.v1.My.AddProduct, CancellationToken, ProductTemplate, RouteGroupBuilder, Store, Task, Endpoint, ProductTemplateId (+9 more)

### Community 261 - "Request"
Cohesion: 0.25
Nodes (8): Request, Description, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name, SearchTerm

### Community 262 - "AuditableEntityResponse"
Cohesion: 0.29
Nodes (7): AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, DateTimeOffset

### Community 263 - "Response"
Cohesion: 0.25
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegister, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 264 - "JobMetricsFilter.cs"
Cohesion: 0.29
Nodes (5): BackgroundJobs.Telemetry, hangfire_server, IServerFilter, PerformingContext, JobMetricsFilter

### Community 265 - ".AddAuthInfrastructure"
Cohesion: 0.29
Nodes (6): IAuthorizationHandler, IAuthorizationPolicyProvider, IConfigureOptions, IServiceCollection, JwtBearerOptions, Setup

### Community 266 - "CachingEntryDefaults"
Cohesion: 0.29
Nodes (7): CachingEntryDefaults, Duration, FactoryHardTimeout, FactorySoftTimeout, FailSafeMaxDuration, FailSafeThrottleDuration, TimeSpan

### Community 267 - "AuditLogOptions"
Cohesion: 0.33
Nodes (6): AuditLogOptions, PerSchemaRetentionDays, PurgeBatchSize, RetentionDays, AuditLogOptionsValidator, Dictionary

### Community 268 - "DevicesOptions"
Cohesion: 0.33
Nodes (6): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator, IReadOnlyCollection

### Community 269 - ".ListSessions"
Cohesion: 0.33
Nodes (5): CancellationToken, IReadOnlyCollection, RouteGroupBuilder, Task, Endpoint

### Community 270 - "DefaultResponsesOperationFilter"
Cohesion: 0.70
Nodes (3): OpenApiOperation, OperationFilterContext, DefaultResponsesOperationFilter

### Community 271 - ".ReleaseStockReservationAsync"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 272 - "SendForEmail/Endpoint.cs"
Cohesion: 0.21
Nodes (5): Common.Application.FeatureManagement, IAM.Domain.Captcha, IAM.Infrastructure.Captcha.Services, IAM.Application.Captcha.Services, microsoft_featuremanagement

### Community 273 - ".AddNetGsm"
Cohesion: 0.33
Nodes (5): IConfiguration, IFusionCache, IOptions, IServiceCollection, Setup

### Community 274 - "GetProductRequestHandler.cs"
Cohesion: 0.27
Nodes (8): Products.Infrastructure.InterModuleRequestHandlers, Common.InterModuleRequests.Products, GetProductRequest, GetProductResponse, DefaultIdType, CancellationToken, Task, GetProductRequestHandler

### Community 275 - ".HandleRequirementAsync"
Cohesion: 0.40
Nodes (4): AuthorizationHandlerContext, HttpContext, Task, AccessTokenReader

### Community 276 - "SmsOtpDispatchOutcome"
Cohesion: 0.33
Nodes (5): OtpDispatchErrors, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled

### Community 277 - "SendResponseBody"
Cohesion: 0.50
Nodes (4): SendResponseBody, Code, Description, JobId

### Community 278 - "SendForLogin/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForLogin, Request, CaptchaToken, PhoneNumber, RequestValidator

### Community 279 - "Response"
Cohesion: 0.25
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.Create, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 280 - "SendForRegistration/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, Request, CaptchaToken, PhoneNumber, RequestValidator

### Community 281 - ".TryReadFromJsonAsync"
Cohesion: 0.40
Nodes (4): HttpContent, CancellationToken, Task, HttpContentJsonExtensions

### Community 282 - "Setup.Logger.cs"
Cohesion: 0.08
Nodes (18): Host.Infrastructure, elastic_serilog_sinks, microsoft_aspnetcore_httpoverrides, opentelemetry_exporter, opentelemetry_metrics, opentelemetry_opentelemetrybuilder, opentelemetry_resources, opentelemetry_trace (+10 more)

### Community 283 - "ModulesOptions.cs"
Cohesion: 0.50
Nodes (4): ModulesOptions, EnabledModules, ModulesOptionsValidator, IReadOnlyList

### Community 284 - "RequestBody"
Cohesion: 0.33
Nodes (6): DateTimeOffset, DefaultIdType, RequestBody, ProductId, Quantity, ReservationDeadline

### Community 288 - "DummySmsGateway"
Cohesion: 0.38
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, DummySmsGateway

### Community 289 - ".SetRetryAfterHeader"
Cohesion: 0.40
Nodes (4): RetryAfterHeaderExtensions, HttpResponse, RateLimitLease, TimeSpan

### Community 290 - "CurrentUser"
Cohesion: 0.08
Nodes (20): IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, CurrentUser, Id, IdAsString, IsAuthenticated, Principal, Roles, SessionId (+12 more)

### Community 291 - "ConcurrencyConfiguratorExtensions.cs"
Cohesion: 0.40
Nodes (3): IBusFactoryConfigurator, IReceiveEndpointConfigurator, ConcurrencyConfiguratorExtensions

### Community 294 - ".SeedProductAsync"
Cohesion: 0.22
Nodes (7): CancellationToken, Task, CancellationToken, List, ProductTemplateId, StoreId, Task

### Community 295 - "SecurityHeadersOptions.cs"
Cohesion: 0.50
Nodes (4): SecurityHeadersOptions, Headers, SecurityHeadersOptionsValidator, Dictionary

### Community 297 - "SignalROptions.cs"
Cohesion: 0.50
Nodes (4): SignalROptions, RedisConnectionString, UseRedisBackplane, SignalROptionsValidator

### Community 299 - ".AddCommonCaching"
Cohesion: 0.40
Nodes (4): Setup, IConfiguration, IConnectionMultiplexer, IServiceCollection

### Community 300 - ".EmailVerificationTokenValidation"
Cohesion: 0.50
Nodes (3): IResxLocalizer, IRuleBuilder, IRuleBuilderOptions

### Community 301 - ".PhoneNumberValidation"
Cohesion: 0.50
Nodes (3): IResxLocalizer, IRuleBuilder, IRuleBuilderOptions

### Community 303 - "VerifyEmail/Request.cs"
Cohesion: 0.50
Nodes (4): Request, Email, Otp, RequestValidator

## Knowledge Gaps
- **894 isolated node(s):** `Id`, `IdAsString`, `Roles`, `SessionId`, `JwtClaimNames` (+889 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 2086 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **105 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `ApplySearchLanguageInterceptor`, `AuditLogOptions`, `DevicesOptions`, `Common.Domain.ResultMonad`, `EmailOptions`, `SendForEmail/Endpoint.cs`, `OutboxOptions`, `Common.Infrastructure.Extensions`, `Setup.Logger.cs`, `ModulesOptions.cs`, `ObservabilityOptions`, `SecurityHeadersOptions.cs`, `SignalROptions.cs`, `ConfigureSwaggerOptions.cs`, `KeycloakOptions`, `ResxLocalizationOptions`, `FirebasePushGateway.cs`, `CachingOptions`, `ReCaptchaService`, `RabbitMqOptions`, `SmsOptions`, `Tokens/VersionNeutral/Setup.cs`, `Users/VersionNeutral/Setup.cs`, `IdentityScheme`, `RequestLoggingOptions`, `CorsOptions`, `InventoryOptions`, `FullTextSearchOptions`, `OpenApiOptions`, `fluentvalidation`, `PushOptions`, `Program.cs`, `Common.Domain.StronglyTypedIds`, `OtpOptions`, `BackgroundJobsOptions`, `ResiliencyOptions`, `Inventory.Domain.StockReservations`, `microsoft_entityframeworkcore`, `microsoft_extensions_logging`, `CustomValidator`, `.AddCustomHealthChecks`?**
  _High betweenness centrality (0.169) - this node is a cross-community bridge._
- **Why does `Result` connect `Result` to `StockReservation`, `.SendAsync`, `.AddProductToMyStoreAsync`, `FirebasePushGateway`, `Error`, `ProductTemplate`, `ApplicationUserId`, `.ListSessions`, `.ReleaseStockReservationAsync`, `IInterModuleRequestClient`, `SmsOtpDispatchOutcome`, `IEmailGateway`, `.RevokeSession`, `ISmsGateway`, `.RequestTokensAsync`, `Func`, `Product`, `PersistenceQueryableExtensions`, `.ReserveSeriesAsync`, `ThrottledEmailGateway`, `DummySmsGateway`, `DomainEvent`, `.RefreshToken`, `StringExtensions`, `SendSecurityAlertRequestHandler`, `.CreateTokens`, `PaginationResponse`, `.AddPushServices`, `ReCaptchaService`, `IResult`, `SendEmailOtpRequestHandler`, `BrevoEmailGateway`, `.SearchProductTemplatesAsync`, `.SendAsync`, `StockReservationId`, `Response`, `.CommitStockReservationAsync`, `.SearchStoresAsync`, `ResultTelemetryExtensions`, `.RegisterAsync`, `.GetProductAsync`, `Response`, `.SearchMyProductsAsync`, `NetGsmSmsGateway`, `.SearchStoreProductsAsync`, `.BindDeviceAsync`, `.SingleAsResult`, `.Failure`, `HttpWarehouseGateway`, `.IsRegisteredAsync`, `IWarehouseGateway`, `Response`, `.HandleWarehouseWebhookAsync`, `.TapWhenFeatureEnabledAsync`?**
  _High betweenness centrality (0.149) - this node is a cross-community bridge._
- **Why does `ApplicationUserId` connect `ApplicationUserId` to `NotificationPayload`, `KeycloakAdminClient`, `AuditableEntityResponse`, `IntegrationEvent`, `KeycloakPermissionAuthorizationHandler`, `Response`, `IAuditableEntity`, `DeviceRegistration`, `.RevokeSession`, `.RequestTokensAsync`, `CurrentUser`, `For`, `SendSecurityAlertRequestHandler`, `PaginationResponse`, `Request`, `Response`, `KeycloakTokenClient`, `V1StoreCreatedDomainEvent`, `GetActiveSessionIdsRequest`, `IInterModuleRequest`, `IStronglyTypedId`, `Response`, `.SearchStoresAsync`, `.Configure`, `.RegisterAsync`, `Store`, `Result`, `Common.Application.ModelBinders`, `Response`, `Stores/v1/Create/Request.cs`, `Products.Domain.ProductTemplates`, `.BindDeviceAsync`, `StockLevel`, `Response`, `GetDeviceSessionsRequest`, `AuditableEntity`, `DeviceRegistrationId`, `NotificationsHub`?**
  _High betweenness centrality (0.075) - this node is a cross-community bridge._
- **What connects `Id`, `IdAsString`, `Roles` to the rest of the system?**
  _894 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `StockReservation` be split into smaller, more focused modules?**
  _Cohesion score 0.059233449477351915 - nodes in this community are weakly interconnected._
- **Should `RedisFixedWindowRateLimiter` be split into smaller, more focused modules?**
  _Cohesion score 0.13333333333333333 - nodes in this community are weakly interconnected._
- **Should `Error` be split into smaller, more focused modules?**
  _Cohesion score 0.11052631578947368 - nodes in this community are weakly interconnected._