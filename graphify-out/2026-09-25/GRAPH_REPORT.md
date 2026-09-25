# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-09-25)

## Corpus Check
- 575 files · ~90,556 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 4940 nodes · 9757 edges · 387 communities (284 shown, 103 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 260 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `a4e2ea8e`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- .ReserveSeriesAsync
- NotificationPayload
- OutboxProcessor
- Products.Endpoints
- Hybrid DDD (Writes) / VSA (Reads)
- KeycloakAdminClient
- RedisFixedWindowRateLimiter
- .AddModuleDbContext
- FirebasePushGateway
- .RequireOtpTemplateForDefaultCulture
- Error
- microsoft_aspnetcore_http
- ApplicationUserId
- SearchLanguageResolver
- microsoft_extensions_dependencyinjection
- KeycloakPermissionAuthorizationHandler
- EmailOptions
- Product
- StockReservation
- Common.Domain.Events
- LogProductCatalogChangeHandler
- BoundedRequestCaptureStream
- DeviceRegistration
- IEmailGateway
- .RevokeSession
- Common.Application.Options
- IEvent
- RequestResponseBodyLoggingMiddleware
- Common.Domain.StronglyTypedIds
- Result
- JobRow
- NotificationsModule
- .SearchMyProductsAsync
- Response
- KeycloakPermissionClient
- OutboxMessage
- CustomRateLimitingOptions
- IAggregateRoot
- RequestBody
- ObservabilityOptions
- ConfigureSwaggerOptions.cs
- Request
- .SaveWithOutboxAsync
- CreateStoreRateLimitingPolicy
- KeycloakOptions
- ziggycreatures_caching_fusion
- PaginationQueryableExtensions
- Outbox Misuse Check
- Inventory.Domain.StockReservations
- Add Integration Event Command
- Response
- ProductsDbContext
- ReCaptchaService
- system_linq_expressions
- EmailOtpDispatchOutcome
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
- .Schedule
- OutboxModule
- .AddNotificationsSignalR
- ValueObject
- .HandleAsync
- IntegrationEventHandlerBase
- FullTextSearchOptions
- Response
- .WriteAsync
- AsNoTracking Coverage Check
- InventoryModule.cs
- Response
- RequireFeatureFilter
- CheckRegistrationRateLimitingPolicy
- DomainEvent
- IAuditableEntity
- Response
- Split-Deployment PoC
- microsoft_entityframeworkcore
- .Configure
- PaginationCursor
- Store
- PushOptions
- Configuration-Driven Module Registration
- Setup
- .GetProductAsync
- .SeedProductAsync
- v1/AddProduct/Request.cs
- .MapEndpoint
- KeyedResiliencePipelines
- Response
- DatabaseSeederOrchestrator
- StockReservationExpirySweepService
- .SearchStoreProductsAsync
- AuditLogRetentionService
- OutboxDbContext
- ResiliencyOptions
- HttpContextTargetingContextAccessor
- InboxCleanupJob
- InventoryOptions
- Response
- Common.InterModuleRequests.Contracts
- InterModuleRequestOptions
- StockLevel
- .IsRegisteredAsync
- KeycloakPermissionPolicyProvider
- .ReleaseStockReservationAsync
- OutboxCleanupJob
- .AddProductAsync
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
- .AddCustomHealthChecks
- .From
- GlobalExceptionHandlingMiddleware
- Response
- EmailRateLimitingPolicy
- Seeder
- docker-compose.yml (Base Stack)
- .AddInfrastructure
- Stores/v1/Update/Request.cs
- Consumer Idempotency (IntegrationEventHandlerBase)
- SendRequestBody
- Response
- ProductTemplates/v1/Search/Request.cs
- .WriteTooManyRequestsToResponse
- ResultToResponseTransformer.cs
- ProductTemplate
- ProcessedMessage
- IStronglyTypedId
- TokenRefreshRateLimitingPolicy
- Setup
- Response
- RequestBody
- .RegisterAsync
- .AddOrUpdate
- .CreateTokensByEmail
- TokenResponseRepresentation
- StockReservationExpirySweepJobRegistrar
- OutboxOptions
- .EnsureNoMigrationsPending
- ConfigureSwaggerOptions
- Setup.Logger.cs
- ISmsGateway
- PolymorphicEventConverter
- CreateStockLevelOnProductCreatedHandler
- OtpOptions
- IInventoryDbContext
- OtpServiceBase
- IntegrationEventOutbox
- ThrottledEmailGateway
- For
- NotificationsDbContext
- CurrentUser
- IamModule
- PaginationRequest
- Response
- SendSecurityAlertRequestHandler
- Notifications.Application/IAssemblyReference.cs
- IntegrationEvent
- Response
- ProductTemplateId
- system_diagnostics
- PaginationResponse
- .FixedWindow
- V1StoreCreatedDomainEvent
- .AddPushServices
- CachingOptions
- SmsOptions
- system_globalization
- IAM.Domain
- SendRequestBody
- OtpService
- IdentityScheme
- .Get
- ReverseProxyOptions
- BoundedCaptureStream
- JobClaimExtensions
- ServiceAccountTokenCache
- ProjectionUpsertOutcome
- .AddServices
- .ListPermissionsAsync
- StoreId
- .ReserveStockAsync
- TokenCreateRateLimitingPolicy
- .Failure
- InventoryModule
- v1/RemoveProduct/Request.cs
- NotificationsTelemetry
- KeycloakScopes
- EventDispatcher
- SmsRateLimitingPolicy
- Configuration-Driven Module Loading
- StatelessInboxStore
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- GetProductRequest
- RedisOtpService
- Stores/v1/My/Update/Request.cs
- BackgroundJobsTelemetry
- common_application_localization_resources
- Response
- .UpsertIfNewerAsync
- FeatureFlags
- .RegisterAsync
- BackgroundJobsService
- StrictDateTimeOffsetJsonConverter
- Keycloak realm as code
- .ListSessions
- .PaginateAsync
- .MapEndpoint
- IInboxStore
- .MapCode
- RegisterRateLimitingPolicy
- .AddCommonCaching
- .AddCommonOptions
- InterModuleRequestHandler
- BackgroundJobsOptions
- BaseDbContext
- .HasPatternIndex
- KeycloakPermissionAuthorizationHandler.cs
- AuditLogEntry
- IOtpService
- microsoft_aspnetcore_mvc
- .UseInfrastructure
- Common.Domain.ResultMonad
- RabbitMqOptions
- DeviceRegistrationId
- V1StockReservationCommitConflictDetectedDomainEvent
- CustomValidator
- KeyedResilienceProfile
- SendForEmail/Request.cs
- Stores/v1/Create/Request.cs
- OpenApiOptions
- SendErrorBody
- DeviceRegistryReconciliationService
- AuditableEntityResponse
- ProductId
- .TryWriteAsync
- Products.Domain/IAssemblyReference.cs
- IAM.Application/IAssemblyReference.cs
- Products.Infrastructure/IAssemblyReference.cs
- Products.Application/IAssemblyReference.cs
- Common.Infrastructure/IAssemblyReference.cs
- Notifications.Domain/IAssemblyReference.cs
- IAM.Infrastructure/IAssemblyReference.cs
- SmsOtpDispatchOutcome
- ProductTemplates/v1/Deactivate/Request.cs
- .SendWithPipelineAsync
- Response
- ResxLocalizationOptions
- BackgroundJobsModule
- SwaggerDefaultValues
- AuditLogOptions
- Activate/Request.cs
- IProductsDbContext
- NetGsmSmsGateway
- ProductTemplates/v1/Create/Request.cs
- .AddResilientHttpClient
- IntegrationEventConverter
- Infrastructure/StringExtensions.cs
- JsonConverter
- StronglyTypedIdWriteOnlyJsonConverter
- SendResponseBody
- Setup.Observability.cs
- Response
- StronglyTypedIdSchemaFilter.cs
- .TryReadFromJsonAsync
- Key decisions
- AuditableEntity
- Reserve/Request.cs
- RequestBodyLimitFilter.cs
- .InvokeAsync
- OtpVerificationOutcome
- SmsMessage
- .SetRetryAfterHeader
- IssueVerificationTokenRequestHandler
- .VerifySha256
- .AddServices
- .MapEndpoints
- KeyedRow
- .CreateStorePolicy
- Tokens/VersionNeutral/Setup.cs
- .LocalizeFromError
- .AddCustomSwagger
- .MapEndpoint
- ResultTelemetryExtensions
- JobHousekeepingOptions
- IResult
- DefaultResponsesOperationFilter
- JobStatus
- .Apply
- JwtClaimNames.cs
- SignalROptions
- PublishOutcome
- .Capture
- KeycloakRoles.cs
- Common.Application.ModelBinders
- Stores/Constants.cs
- ModulesOptions.cs
- Setup
- Setup
- SecurityHeadersOptions.cs
- Products/v1/Update/Request.cs
- Infrastructure/Setup.cs
- system_runtime_compilerservices
- VerifyEmail/Request.cs
- Products/v1/My/Update/Request.cs
- fluentvalidation
- IAM.Endpoints
- Notifications.Infrastructure
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
6. `ApplicationUserId` - 71 edges
7. `Common.Domain.StronglyTypedIds` - 67 edges
8. `Common.Application.Auth` - 66 edges
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

## Communities (387 total, 103 thin omitted)

### Community 0 - ".ReserveSeriesAsync"
Cohesion: 0.14
Nodes (12): Inventory.Endpoints.StockReservations.v1.ReserveSeries, CancellationToken, IOptions, List, RequestBody, RouteGroupBuilder, Task, TimeProvider (+4 more)

### Community 1 - "NotificationPayload"
Cohesion: 0.23
Nodes (12): IHubContext, CancellationToken, IReadOnlyList, Task, INotificationDispatcher, Task, INotificationsClient, NotificationPayload (+4 more)

### Community 2 - "OutboxProcessor"
Cohesion: 0.21
Nodes (12): IPublishEndpoint, PublishOutcome, CancellationToken, Exception, ILogger, IOptions, IServiceScopeFactory, LoggerMessage (+4 more)

### Community 5 - "KeycloakAdminClient"
Cohesion: 0.07
Nodes (45): CancellationToken, Error, Func, HttpClient, HttpRequestMessage, HttpResponseMessage, IFusionCache, ILogger (+37 more)

### Community 6 - "RedisFixedWindowRateLimiter"
Cohesion: 0.13
Nodes (16): RateLimiter, RateLimiterStatistics, FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration, CancellationToken (+8 more)

### Community 7 - ".AddModuleDbContext"
Cohesion: 0.20
Nodes (10): SaveChangesInterceptor, ApplyAuditingInterceptor, TimeProvider, ApplySearchLanguageInterceptor, Setup, IServiceCollection, Setup, IServiceCollection (+2 more)

### Community 8 - "FirebasePushGateway"
Cohesion: 0.14
Nodes (15): FirebaseApp, FirebaseMessaging, IDisposable, IReadOnlyDictionary, IReadOnlyList, PushMessage, CancellationToken, Exception (+7 more)

### Community 9 - ".RequireOtpTemplateForDefaultCulture"
Cohesion: 0.40
Nodes (4): Func, IConfiguration, IEnumerable, OtpTemplateConfiguration

### Community 10 - "Error"
Cohesion: 0.09
Nodes (18): Inventory.Domain.StockReservations.Errors, Error, Key, ParameterName, StatusCode, SubErrors, Value, HttpStatusCode (+10 more)

### Community 11 - "microsoft_aspnetcore_http"
Cohesion: 0.11
Nodes (23): Products.Infrastructure.InterModuleRequestHandlers, Common.Application.Search, Products.Endpoints.Stores, Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Products.Endpoints.ProductTemplates, Products.Domain.Products (+15 more)

### Community 12 - "ApplicationUserId"
Cohesion: 0.10
Nodes (24): ApplicationUserId, IsEmpty, Value, DefaultIdType, CancellationToken, IReadOnlyList, Task, IKeycloakAdminClient (+16 more)

### Community 13 - "SearchLanguageResolver"
Cohesion: 0.20
Nodes (7): SearchLanguageResolver, UniversalConfig, IOptions, Setup, IApplicationBuilder, IOptions, IServiceCollection

### Community 14 - "microsoft_extensions_dependencyinjection"
Cohesion: 0.14
Nodes (14): Common.Infrastructure.Persistence, Outbox, Common.Infrastructure.Persistence.Outbox, Outbox.Telemetry, Common.Infrastructure.Persistence.AuditLog, Common.Infrastructure.Persistence.DbContext, Inventory.Infrastructure.StockReservations, entityframework_exceptions_postgresql (+6 more)

### Community 15 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.14
Nodes (14): AuthorizationHandler, AuthorizationHandlerContext, CancellationToken, ClaimsPrincipal, HttpContext, IFusionCache, IHttpContextAccessor, IOptions (+6 more)

### Community 16 - "EmailOptions"
Cohesion: 0.09
Nodes (24): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+16 more)

### Community 17 - "Product"
Cohesion: 0.07
Nodes (29): File map _(Build)_, ISearchLocalized, Language, ProductTemplate, ProductTemplateId, Store, StoreId, Product (+21 more)

### Community 18 - "StockReservation"
Cohesion: 0.07
Nodes (29): AggregateRoot, Events, Id, Version, IReadOnlyCollection, List, DateTimeOffset, DefaultIdType (+21 more)

### Community 19 - "Common.Domain.Events"
Cohesion: 0.08
Nodes (15): Inventory.Domain.StockReservations.DomainEvents.v1, Common.Domain.Events, Common.Application.Persistence.Outbox, Products.Domain.Stores.DomainEvents.v1, Inventory.Domain.StockLevels.DomainEvents.v1, DefaultIdType, StockLevelId, V1StockLevelCreatedDomainEvent (+7 more)

### Community 20 - "LogProductCatalogChangeHandler"
Cohesion: 0.24
Nodes (8): CancellationToken, DefaultIdType, IFusionCache, ILogger, IOptions, LoggerMessage, Task, LogProductCatalogChangeHandler

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.18
Nodes (7): BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position, Stream

### Community 22 - "DeviceRegistration"
Cohesion: 0.16
Nodes (12): DateTimeOffset, Guid, DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive, LastReconciledOn (+4 more)

### Community 23 - "IEmailGateway"
Cohesion: 0.18
Nodes (11): IEmailGateway, CancellationToken, ILogger, LoggerMessage, Task, DummyEmailGateway, IConfiguration, IFusionCache (+3 more)

### Community 24 - ".RevokeSession"
Cohesion: 0.10
Nodes (18): DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse, IReadOnlyList, CancellationToken, RouteGroupBuilder, Task, Endpoint, CancellationToken (+10 more)

### Community 25 - "Common.Application.Options"
Cohesion: 0.06
Nodes (38): Common.Infrastructure.Modules, Notifications.Infrastructure.Email, Products.Endpoints.Probe, Common.Infrastructure.RateLimiting, Notifications.Infrastructure.Push, Products.Infrastructure.RateLimiting, Products.Endpoints.Products, Common.Infrastructure.Extensions (+30 more)

### Community 26 - "IEvent"
Cohesion: 0.12
Nodes (13): CancellationToken, Task, IEventHandler, CancellationToken, Task, IEventHandlerWrapper, CancellationToken, Task (+5 more)

### Community 27 - "RequestResponseBodyLoggingMiddleware"
Cohesion: 0.22
Nodes (7): IDiagnosticContext, HttpContext, IList, IOptions, PathString, RequestDelegate, RequestResponseBodyLoggingMiddleware

### Community 28 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.13
Nodes (9): Common.Domain.StronglyTypedIds, Common.Application.JsonConverters, Common.Domain.Entities, Common.Infrastructure.Persistence.Auditing, Common.Domain.Aggregates, microsoft_entityframeworkcore_diagnostics, microsoft_entityframeworkcore_storage_valueconversion, system_componentmodel_dataannotations (+1 more)

### Community 29 - "Result"
Cohesion: 0.14
Nodes (14): FeatureFlagResultExtensions, Action, Func, IFeatureManager, Task, Result, Error, IsFailure (+6 more)

### Community 30 - "JobRow"
Cohesion: 0.19
Nodes (9): JobRow, FailureKey, FinishedOn, Progress, QueuedOn, RequestedBy, StartedOn, Status (+1 more)

### Community 31 - "NotificationsModule"
Cohesion: 0.15
Nodes (10): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, NotificationsModule, ActivitySourceNames, MeterNames (+2 more)

### Community 32 - ".SearchMyProductsAsync"
Cohesion: 0.10
Nodes (16): Configuration, ISearchLanguageResolver, UniversalConfig, CancellationToken, DbContextEventData, InterceptionResult, ValueTask, LikePattern (+8 more)

### Community 33 - "Response"
Cohesion: 0.20
Nodes (8): RouteGroupBuilder, Endpoint, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 34 - "KeycloakPermissionClient"
Cohesion: 0.19
Nodes (14): CancellationToken, Dictionary, HttpClient, HttpResponseMessage, ILogger, IOptions, IReadOnlyList, List (+6 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.10
Nodes (20): IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, DateTimeOffset, OutboxMessage (+12 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.08
Nodes (22): CheckRegistrationRateLimitingPolicy, EmailRateLimitingPolicy, OtpVerifyRateLimitingPolicy, RegisterRateLimitingPolicy, SmsRateLimitingPolicy, CustomRateLimitingOptions, CheckRegistration, CreateStore (+14 more)

### Community 37 - "IAggregateRoot"
Cohesion: 0.29
Nodes (5): IAggregateRoot, Events, Id, Version, IReadOnlyCollection

### Community 38 - "RequestBody"
Cohesion: 0.15
Nodes (14): DateTimeOffset, DefaultIdType, ICollection, RequestBody, FirstDeadline, IntervalDays, OccurrenceCount, ProductId (+6 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.08
Nodes (24): IHostBuilder, LoggerConfiguration, LoggerMinimumLevelConfiguration, ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics (+16 more)

### Community 40 - "ConfigureSwaggerOptions.cs"
Cohesion: 0.27
Nodes (7): asp_versioning_apiexplorer, Host.Swagger, microsoft_aspnetcore_mvc_apiexplorer, microsoft_openapi, RemoveDefaultResponseSchemaFilter, swashbuckle_aspnetcore_swaggergen, system_text_json_nodes

### Community 41 - "Request"
Cohesion: 0.04
Nodes (46): Guid, Request, ClientId, DeviceId, DeviceName, Otp, PhoneNumber, PushToken (+38 more)

### Community 42 - ".SaveWithOutboxAsync"
Cohesion: 0.27
Nodes (9): OutboxSaveHelper, CancellationToken, DbContext, Exception, Func, ILogger, LoggerMessage, Task (+1 more)

### Community 43 - "CreateStoreRateLimitingPolicy"
Cohesion: 0.14
Nodes (12): CancellationToken, Func, HttpContext, IOptions, IProblemDetailsService, IResxLocalizer, OnRejectedContext, RateLimitPartition (+4 more)

### Community 44 - "KeycloakOptions"
Cohesion: 0.09
Nodes (21): KeycloakOptions, AttemptTimeoutSeconds, Authority, BaseUrl, DecisionCacheMaxDurationSeconds, Realm, RequireHttpsMetadata, ResourceClientId (+13 more)

### Community 45 - "ziggycreatures_caching_fusion"
Cohesion: 0.22
Nodes (7): Notifications.Application.Sms, Notifications.Infrastructure.Sms, Notifications.Infrastructure.Sms.NetGsm, system_buffers, system_security_cryptography, system_text, ziggycreatures_caching_fusion

### Community 46 - "PaginationQueryableExtensions"
Cohesion: 0.18
Nodes (11): BinaryExpression, ExpressionVisitor, MemberExpression, MethodInfo, ParameterExpression, CursorBound, Value, PaginationQueryableExtensions (+3 more)

### Community 48 - "Inventory.Domain.StockReservations"
Cohesion: 0.09
Nodes (14): Common.Application.Persistence.Inbox, Inventory.Infrastructure.InterModuleRequestHandlers, Inventory.Application.Persistence, Inventory.Application.IntegrationEventHandlers, Common.IntegrationEvents, Inventory.Domain.StockReservations, Products.Application.Stores.DomainEventHandlers.v1, Common.Application.EventBus (+6 more)

### Community 50 - "Response"
Cohesion: 0.12
Nodes (15): IAM.Endpoints.Users.VersionNeutral.Search, RouteGroupBuilder, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate, CreatedOn (+7 more)

### Community 51 - "ProductsDbContext"
Cohesion: 0.13
Nodes (14): DbContextOptions, DbSet, ILogger, ProductTemplate, Store, TimeProvider, ProductsDbContext, Products (+6 more)

### Community 52 - "ReCaptchaService"
Cohesion: 0.06
Nodes (36): DateTime, FormUrlEncodedContent, ReCaptchaResponse, CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey (+28 more)

### Community 53 - "system_linq_expressions"
Cohesion: 0.19
Nodes (8): Common.Application.BackgroundJobs, BackgroundJobs, hangfire, hangfire_annotations, hangfire_dashboard, hangfire_server, hangfire_storage, system_linq_expressions

### Community 54 - "EmailOtpDispatchOutcome"
Cohesion: 0.33
Nodes (5): EmailOtpDispatchErrors, EmailOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled

### Community 55 - "BrevoEmailGateway"
Cohesion: 0.19
Nodes (12): SendErrorBody, Exception, HttpClient, HttpStatusCode, ILogger, IOptions, JsonSerializerOptions, LoggerMessage (+4 more)

### Community 56 - "ProjectionReconciliationJob"
Cohesion: 0.24
Nodes (10): Items, Next, ProjectionReconciliationJob, CancellationToken, IDbContext, ILogger, IOptions, IReadOnlyList (+2 more)

### Community 58 - "OutboxMetricsJob"
Cohesion: 0.13
Nodes (14): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxMetricsJob (+6 more)

### Community 59 - ".SearchProductTemplatesAsync"
Cohesion: 0.17
Nodes (10): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Brand (+2 more)

### Community 61 - "KeycloakTokenClient"
Cohesion: 0.13
Nodes (20): JsonWebTokenHandler, CancellationToken, Task, IKeycloakTokenClient, DateTimeOffset, KeycloakTokens, CancellationToken, Task (+12 more)

### Community 62 - "IDbContext"
Cohesion: 0.12
Nodes (16): DatabaseFacade, Deleted, EntityEntry, JobHousekeepingJob, CancellationToken, ILogger, IOptions, LoggerMessage (+8 more)

### Community 63 - "RequestLoggingOptions"
Cohesion: 0.11
Nodes (20): IPostConfigureOptions, RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes (+12 more)

### Community 64 - ".SendAsync"
Cohesion: 0.22
Nodes (7): SendResponseBody, CancellationToken, HttpResponseMessage, Task, SendContact, Email, Name

### Community 65 - ".Schedule"
Cohesion: 0.31
Nodes (6): Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 66 - "OutboxModule"
Cohesion: 0.14
Nodes (16): Action, Exception, IApplicationBuilder, IEndpointRouteBuilder, IEnumerable, IHostApplicationLifetime, ILogger, ILoggerFactory (+8 more)

### Community 67 - ".AddNotificationsSignalR"
Cohesion: 0.12
Nodes (13): Notifications.Infrastructure.Hubs, Notifications.Application.Hubs, HubConnectionContext, IUserIdProvider, microsoft_aspnetcore_signalr, RedisOptions, IConfiguration, IConfigureOptions (+5 more)

### Community 68 - "ValueObject"
Cohesion: 0.25
Nodes (4): Common.Domain, IComparable, ValueObject, IEnumerable

### Community 69 - ".HandleAsync"
Cohesion: 0.18
Nodes (11): GetSeedUserIdsRequest, GetSeedUserIdsResponse, ICollection, CancellationToken, Task, GetSeedUserIdsRequestHandler, CancellationToken, IResult (+3 more)

### Community 70 - "IntegrationEventHandlerBase"
Cohesion: 0.24
Nodes (11): IntegrationEventHandlerBase, MaxEventAge, CancellationToken, ConsumeContext, DefaultIdType, IFusionCache, ILogger, IOptions (+3 more)

### Community 71 - "FullTextSearchOptions"
Cohesion: 0.09
Nodes (23): Add a new language/culture, Add search to a new entity _(Build checklist)_, Change the vector (weights, fields, or config), Extending and maintaining, Full-Text Search, Gotchas, How it works, Non-goals (+15 more)

### Community 72 - "Response"
Cohesion: 0.18
Nodes (9): Products.Endpoints.ProductTemplates.v1.Get, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Brand, Color (+1 more)

### Community 73 - ".WriteAsync"
Cohesion: 0.33
Nodes (5): Memory, ReadOnlyMemory, CancellationToken, Task, ValueTask

### Community 75 - "InventoryModule.cs"
Cohesion: 0.13
Nodes (11): ApiVersionSet, asp_versioning, asp_versioning_builder, asp_versioning_conventions, Inventory.Endpoints, Common.Endpoints.Versioning, Inventory.Infrastructure.Persistence, Inventory.Endpoints.StockReservations (+3 more)

### Community 76 - "Response"
Cohesion: 0.18
Nodes (9): Products.Endpoints.Stores.v1.My.Get, RouteGroupBuilder, Endpoint, Response, Address, Description, Name, OwnerId (+1 more)

### Community 77 - "RequireFeatureFilter"
Cohesion: 0.14
Nodes (12): IEndpointFilter, IFeatureManagerSnapshot, RequireFeatureFilter, ActivitySource, Counter, EndpointFilterDelegate, EndpointFilterInvocationContext, IResxLocalizer (+4 more)

### Community 78 - "CheckRegistrationRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, CheckRegistrationRateLimitingPolicy (+1 more)

### Community 79 - "DomainEvent"
Cohesion: 0.07
Nodes (24): Products.Domain.Products.DomainEvents.v1, Products.Application.Products.DomainEventHandlers.v1, DomainEvent, CreatedOn, Id, Version, DateTimeOffset, DefaultIdType (+16 more)

### Community 80 - "IAuditableEntity"
Cohesion: 0.18
Nodes (10): IAuditableEntity, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, DateTimeOffset, CancellationToken, DbContextEventData (+2 more)

### Community 81 - "Response"
Cohesion: 0.18
Nodes (9): Products.Endpoints.Stores.v1.Search, RouteGroupBuilder, Endpoint, Response, Address, Description, Name, OwnerId (+1 more)

### Community 82 - "Split-Deployment PoC"
Cohesion: 0.18
Nodes (9): Concurrent safety, Cross-process call path, Files added by this PoC, How it works, How to run, Split-Deployment PoC, What this proves, README (Boilerplate Overview) (+1 more)

### Community 83 - "microsoft_entityframeworkcore"
Cohesion: 0.13
Nodes (14): Common.Infrastructure.Persistence.Inbox, Common.Application.Persistence, Inventory.Infrastructure.Persistence.EntityConfigurations, Common.Application.Jobs, Notifications.Domain.Devices, Notifications.Infrastructure.Persistence, Outbox.Persistence, Common.Infrastructure.Persistence.EntityConfigurations (+6 more)

### Community 84 - ".Configure"
Cohesion: 0.15
Nodes (14): AuditableEntityConfiguration, EntityTypeBuilder, JobRowConfiguration, EntityTypeBuilder, StronglyTypedIdValueConverter, DefaultIdType, EntityTypeBuilder, NpgsqlTsVector (+6 more)

### Community 85 - "PaginationCursor"
Cohesion: 0.22
Nodes (7): Payload, PaginationCursor, Payload, TaggedValue, JsonSerializerOptions, Type, TaggedValue

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
Cohesion: 0.11
Nodes (20): Common.InterModuleRequests.Inventory, GetStockLevelRequest, GetStockLevelResponse, DefaultIdType, CancellationToken, Task, GetStockLevelRequestHandler, RouteGroupBuilder (+12 more)

### Community 91 - ".SeedProductAsync"
Cohesion: 0.22
Nodes (7): CancellationToken, Task, CancellationToken, List, ProductTemplateId, StoreId, Task

### Community 92 - "v1/AddProduct/Request.cs"
Cohesion: 0.08
Nodes (25): Constants, StoreId, Request, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name (+17 more)

### Community 93 - ".MapEndpoint"
Cohesion: 0.22
Nodes (6): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, RouteGroupBuilder, Response, ClientKey, RouteGroupBuilder, Setup

### Community 94 - "KeyedResiliencePipelines"
Cohesion: 0.14
Nodes (13): HttpRequestException, ResiliencePipelineBuilder, ResiliencePipelineRegistry, KeyedResiliencePipelines, HttpResponseMessage, IConnectionMultiplexer, ILoggerFactory, IOptions (+5 more)

### Community 95 - "Response"
Cohesion: 0.20
Nodes (8): Products.Endpoints.Products.v1.My.Search, RouteGroupBuilder, Endpoint, Response, Description, Name, Price, Quantity

### Community 96 - "DatabaseSeederOrchestrator"
Cohesion: 0.07
Nodes (26): BackgroundService, IDatabaseSeeder, Priority, CancellationToken, Task, DatabaseSeederOrchestrator, CancellationToken, Exception (+18 more)

### Community 97 - "StockReservationExpirySweepService"
Cohesion: 0.18
Nodes (11): IServiceCollection, Setup, CancellationToken, Exception, Guid, ILogger, IOptions, LoggerMessage (+3 more)

### Community 98 - ".SearchStoreProductsAsync"
Cohesion: 0.17
Nodes (11): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Description (+3 more)

### Community 99 - "AuditLogRetentionService"
Cohesion: 0.07
Nodes (31): IHostedService, DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, IReadOnlyCollection, AuditLogRetentionJobRegistrar, CancellationToken (+23 more)

### Community 100 - "OutboxDbContext"
Cohesion: 0.14
Nodes (13): DbContext, IOutboxDbContext, OutboxMessages, CancellationToken, DbSet, Task, IConfiguration, IServiceCollection (+5 more)

### Community 101 - "ResiliencyOptions"
Cohesion: 0.17
Nodes (12): ResiliencyOptions, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, Keyed, MaxRetryAttempts (+4 more)

### Community 102 - "HttpContextTargetingContextAccessor"
Cohesion: 0.20
Nodes (8): ITargetingContextAccessor, HttpContextTargetingContextAccessor, IHttpContextAccessor, ValueTask, Setup, IConfiguration, IServiceCollection, TargetingContext

### Community 103 - "InboxCleanupJob"
Cohesion: 0.22
Nodes (10): CancellationToken, IEnumerable, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider (+2 more)

### Community 104 - "InventoryOptions"
Cohesion: 0.15
Nodes (13): InventoryOptions, MaxOccurrencesPerSeries, SafetyBufferQuantity, StockReservationExpirySweepBatchSize, StockReservationExpirySweepCron, WarehouseGatewayBaseUrl, WarehouseGatewayProvider, WebhookMaxBodyBytes (+5 more)

### Community 105 - "Response"
Cohesion: 0.20
Nodes (8): RouteGroupBuilder, Endpoint, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 106 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.12
Nodes (18): Notifications.Application.Otp, Notifications.Infrastructure.Devices, IAM.Endpoints.Users, Common.Application.Caching, Notifications.Application.Persistence, Notifications.Infrastructure.Telemetry, Common.InterModuleRequests.IAM, Common.InterModuleRequests.Contracts (+10 more)

### Community 107 - "InterModuleRequestOptions"
Cohesion: 0.15
Nodes (12): IClientFactory, InterModuleRequestOptions, DependencyUnavailableRetryAfterSeconds, HandlerConcurrentMessageLimit, HandlerPrefetchCount, Timeouts, TimeoutSeconds, Dictionary (+4 more)

### Community 108 - "StockLevel"
Cohesion: 0.14
Nodes (8): StronglyTypedIdHelper, DefaultIdType, StockLevel, ProductId, QuantityOnHand, StockLevelId, EntityTypeBuilder, StockLevelConfiguration

### Community 109 - ".IsRegisteredAsync"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, IsRegistered

### Community 110 - "KeycloakPermissionPolicyProvider"
Cohesion: 0.12
Nodes (14): AuthorizationOptions, AuthorizationPolicy, DefaultAuthorizationPolicyProvider, IAuthorizationPolicyProvider, IAuthorizationRequirement, KeycloakPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder (+6 more)

### Community 111 - ".ReleaseStockReservationAsync"
Cohesion: 0.05
Nodes (34): Inventory.Infrastructure.Gateway, Inventory.Application.Gateway, ReleaseResponseBody, CollectionExtensions, Func, ICollection, IEnumerable, PersistenceQueryableExtensions (+26 more)

### Community 112 - "OutboxCleanupJob"
Cohesion: 0.27
Nodes (8): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxCleanupJob

### Community 113 - ".AddProductAsync"
Cohesion: 0.18
Nodes (9): Products.Endpoints.Stores.v1.AddProduct, CancellationToken, ProductTemplate, RouteGroupBuilder, Store, Task, Endpoint, Response (+1 more)

### Community 114 - "Response"
Cohesion: 0.11
Nodes (17): IAM.Endpoints.Users.VersionNeutral.Me.Get, RouteGroupBuilder, Endpoint, DateOnly, DateTimeOffset, IReadOnlyCollection, Response, BirthDate (+9 more)

### Community 115 - "SkipOverlappingRecurringJobFilter"
Cohesion: 0.20
Nodes (8): IServerFilter, JobMetricsFilter, PerformingContext, SkipOverlappingRecurringJobFilter, ILogger, LoggerMessage, PerformedContext, PerformingContext

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
Cohesion: 0.14
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
Cohesion: 0.13
Nodes (13): HealthCheckOptions, EnableHealthChecks, ReadinessTimeoutInSeconds, StartupTimeoutInSeconds, IApplicationBuilder, IConfiguration, IConnectionMultiplexer, ILogger (+5 more)

### Community 125 - ".From"
Cohesion: 0.36
Nodes (6): AspNetResult, ResxLocalizer, EndpointFilterDelegate, EndpointFilterInvocationContext, IStringLocalizer, ValueTask

### Community 126 - "GlobalExceptionHandlingMiddleware"
Cohesion: 0.20
Nodes (12): IApplicationBuilder, IServiceCollection, Exception, HttpContext, ILogger, IOptions, IProblemDetailsService, IResxLocalizer (+4 more)

### Community 127 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 128 - "EmailRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, EmailRateLimitingPolicy (+1 more)

### Community 129 - "Seeder"
Cohesion: 0.29
Nodes (5): Products.Infrastructure.Persistence.Seeding, ILogger, LoggerMessage, ProductsDbContext, Seeder

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.48
Nodes (7): Observability (OpenTelemetry), docker-compose.yml (Base Stack), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), Postgres Service (mm.postgres), RabbitMQ Service (mm.rabbitmq), Redis Service (mm.redis)

### Community 131 - ".AddInfrastructure"
Cohesion: 0.15
Nodes (14): HostOptions, JsonOptions, CorsOptions, AllowCredentials, AllowedHeaders, AllowedMethods, AllowedOrigins, MaxAgeInSeconds (+6 more)

### Community 132 - "Stores/v1/Update/Request.cs"
Cohesion: 0.20
Nodes (10): Products.Endpoints.Stores.v1.Update, RequestBody, Request, Body, Id, RequestBody, Address, Description (+2 more)

### Community 134 - "SendRequestBody"
Cohesion: 0.25
Nodes (8): SendContact, IReadOnlyList, SendRequestBody, HtmlContent, Sender, Subject, TextContent, To

### Community 135 - "Response"
Cohesion: 0.13
Nodes (13): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, IReadOnlyCollection, RouteGroupBuilder, Endpoint, DateTimeOffset, Response, ClientId, DeviceName (+5 more)

### Community 136 - "ProductTemplates/v1/Search/Request.cs"
Cohesion: 0.22
Nodes (8): Products.Endpoints.ProductTemplates.v1.Search, Constants, Request, Brand, Color, Model, SearchTerm, RequestValidator

### Community 137 - ".WriteTooManyRequestsToResponse"
Cohesion: 0.13
Nodes (14): PartitionedRateLimiter, CancellationToken, Func, HttpContext, IConfiguration, IProblemDetailsService, IReadOnlyList, IResxLocalizer (+6 more)

### Community 138 - "ResultToResponseTransformer.cs"
Cohesion: 0.19
Nodes (12): Common.Application.EndpointFilters, microsoft_aspnetcore_hosting, microsoft_aspnetcore_http_iresult, microsoft_extensions_localization, ProblemResponse, ResultToAcceptedResponseTransformer, ResultToCreatedResponseTransformer, ResultToResponseTransformer (+4 more)

### Community 139 - "ProductTemplate"
Cohesion: 0.15
Nodes (11): IReadOnlyList, List, ProductTemplate, Brand, Color, IsActive, Model, Products (+3 more)

### Community 140 - "ProcessedMessage"
Cohesion: 0.13
Nodes (16): ProcessedMessage, ConsumerName, MessageId, ProcessedOn, DateTimeOffset, DefaultIdType, UniqueConstraintException, InboxStore (+8 more)

### Community 141 - "IStronglyTypedId"
Cohesion: 0.23
Nodes (8): StronglyTypedIdReadOnlyJsonConverter, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, IStronglyTypedId, Value, DefaultIdType

### Community 142 - "TokenRefreshRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenRefreshRateLimitingPolicy (+1 more)

### Community 143 - "Setup"
Cohesion: 0.09
Nodes (21): ForwardedHeadersOptions, LoadAll, microsoft_aspnetcore_httpoverrides, ModuleRegistry, Names, IConfiguration, IServiceCollection, Setup (+13 more)

### Community 144 - "Response"
Cohesion: 0.12
Nodes (15): IAM.Endpoints.Users.VersionNeutral.Get, RouteGroupBuilder, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate, CreatedOn (+7 more)

### Community 145 - "RequestBody"
Cohesion: 0.29
Nodes (7): ProductTemplateId, RequestBody, Description, Name, Price, ProductTemplateId, Quantity

### Community 146 - ".RegisterAsync"
Cohesion: 0.06
Nodes (35): BindDeviceSessionRequest, BindDeviceSessionResponse, Guid, OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts, VerifyPhoneOtpRequest (+27 more)

### Community 147 - ".AddOrUpdate"
Cohesion: 0.20
Nodes (8): Action, Expression, Func, Task, Action, Expression, Func, Task

### Community 148 - ".CreateTokensByEmail"
Cohesion: 0.07
Nodes (28): IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, VerifyEmailOtpRequest, VerifyEmailOtpResponse, EmailNormalization, VerifyEmailOtpResponseExtensions, CancellationToken, RouteGroupBuilder, Task (+20 more)

### Community 149 - "TokenResponseRepresentation"
Cohesion: 0.15
Nodes (12): List, DecisionRepresentation, Result, PermissionRepresentation, ResourceName, Scopes, TokenResponseRepresentation, AccessToken (+4 more)

### Community 150 - "StockReservationExpirySweepJobRegistrar"
Cohesion: 0.31
Nodes (7): CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task, StockReservationExpirySweepJobRegistrar

### Community 151 - "OutboxOptions"
Cohesion: 0.10
Nodes (21): OutboxCleanupSettings, BatchSize, CronSchedule, Enabled, RetentionDays, OutboxCleanupSettingsValidator, OutboxOptions, BaseBackoffSeconds (+13 more)

### Community 152 - ".EnsureNoMigrationsPending"
Cohesion: 0.35
Nodes (6): AutoMigrateMarker, IAutoMigrateMarker, MigrationGuard, ILogger, IServiceProvider, LoggerMessage

### Community 153 - "ConfigureSwaggerOptions"
Cohesion: 0.28
Nodes (7): ApiVersionDescription, IApiVersionDescriptionProvider, IConfigureOptions, OpenApiInfo, IOptions, SwaggerGenOptions, ConfigureSwaggerOptions

### Community 154 - "Setup.Logger.cs"
Cohesion: 0.15
Nodes (11): Host, elastic_serilog_sinks, serilog, serilog_configuration, serilog_enrichers_span, serilog_events, serilog_exceptions, serilog_formatting_compact (+3 more)

### Community 155 - "ISmsGateway"
Cohesion: 0.13
Nodes (13): CancellationToken, Task, ISmsGateway, IConfiguration, IFusionCache, IOptions, IServiceCollection, Setup (+5 more)

### Community 156 - "PolymorphicEventConverter"
Cohesion: 0.25
Nodes (6): UnknownDomainEvent, PolymorphicEventConverter, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 157 - "CreateStockLevelOnProductCreatedHandler"
Cohesion: 0.24
Nodes (8): CancellationToken, DefaultIdType, IFusionCache, ILogger, IOptions, LoggerMessage, Task, CreateStockLevelOnProductCreatedHandler

### Community 158 - "OtpOptions"
Cohesion: 0.20
Nodes (10): OtpOptions, DummyCode, EmailQuotaWindowMinutes, ExpirationInMinutes, Length, MaxSendsPerEmailPerWindow, MaxSendsPerPhonePerWindow, PhoneQuotaWindowMinutes (+2 more)

### Community 159 - "IInventoryDbContext"
Cohesion: 0.09
Nodes (20): DbSet, IInventoryDbContext, StockLevels, StockReservations, CancellationToken, RouteGroupBuilder, Task, TimeProvider (+12 more)

### Community 160 - "OtpServiceBase"
Cohesion: 0.21
Nodes (8): OtpCacheEntry, DateTimeOffset, CancellationToken, IFusionCache, SemaphoreSlim, Task, TimeSpan, OtpServiceBase

### Community 161 - "IntegrationEventOutbox"
Cohesion: 0.15
Nodes (13): IIntegrationEventOutbox, IntegrationEventOutbox, HasPending, List, Lock, Setup, IServiceCollection, CancellationToken (+5 more)

### Community 162 - "ThrottledEmailGateway"
Cohesion: 0.20
Nodes (8): CancellationToken, Task, EmailMessage, CancellationToken, IFusionCache, IOptions, Task, ThrottledEmailGateway

### Community 164 - "NotificationsDbContext"
Cohesion: 0.18
Nodes (10): DbContextOptions, DbSet, ILogger, TimeProvider, NotificationsDbContext, DeviceRegistrations, IApplicationBuilder, ILoggerFactory (+2 more)

### Community 165 - "CurrentUser"
Cohesion: 0.15
Nodes (12): CurrentUser, Id, IdAsString, IsAuthenticated, Principal, Roles, SessionId, ClaimsPrincipal (+4 more)

### Community 166 - "IamModule"
Cohesion: 0.18
Nodes (10): Action, IApplicationBuilder, IEnumerable, RateLimiterOptions, IamModule, ActivitySourceNames, MeterNames, Name (+2 more)

### Community 167 - "PaginationRequest"
Cohesion: 0.05
Nodes (37): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Stores.v1.My.AuditLog, Products.Endpoints.Products.v1.AuditLog, PaginationRequest, After, IncludeTotal, PageNumber, PageSize (+29 more)

### Community 168 - "Response"
Cohesion: 0.22
Nodes (6): Products.Endpoints.ProductTemplates.v1.Create, RouteGroupBuilder, Setup, RouteGroupBuilder, Response, Id

### Community 169 - "SendSecurityAlertRequestHandler"
Cohesion: 0.27
Nodes (9): SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, CancellationToken, IOptions, RequestLocalizationOptions, Task (+1 more)

### Community 171 - "IntegrationEvent"
Cohesion: 0.22
Nodes (9): IReadOnlyList, IntegrationEvent, CreatedOn, Id, DateTimeOffset, DefaultIdType, ProductCreatedIntegrationEvent, StoreCreatedIntegrationEvent (+1 more)

### Community 172 - "Response"
Cohesion: 0.22
Nodes (6): Products.Endpoints.Stores.v1.Create, RouteGroupBuilder, Setup, RouteGroupBuilder, Response, Id

### Community 173 - "ProductTemplateId"
Cohesion: 0.25
Nodes (6): ProductTemplateId, DefaultIdType, ProductTemplateId, CancellationToken, List, Task

### Community 174 - "system_diagnostics"
Cohesion: 0.13
Nodes (14): BackgroundJobs.Telemetry, LoginMethods, SessionRevokedReasons, ActivitySource, Counter, Meter, InventoryTelemetry, ActivitySource (+6 more)

### Community 175 - "PaginationResponse"
Cohesion: 0.07
Nodes (28): JsonElement, AuditLogDto, DateTimeOffset, PaginationResponse, HasNext, HasPrevious, HasTotal, NextPageNumber (+20 more)

### Community 176 - ".FixedWindow"
Cohesion: 0.23
Nodes (10): FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit, RateLimitPartitions, HttpContext, IConnectionMultiplexer (+2 more)

### Community 177 - "V1StoreCreatedDomainEvent"
Cohesion: 0.33
Nodes (8): DomainEventHandlerBase, CancellationToken, Task, SimulateSomeBusinessHandler, StoreCreatedIntegrationEventPublishingHandler, V1StoreCreatedDomainEventHandlers, StoreId, V1StoreCreatedDomainEvent

### Community 178 - ".AddPushServices"
Cohesion: 0.16
Nodes (11): CancellationToken, Task, IPushGateway, CancellationToken, ILogger, LoggerMessage, Task, DummyPushGateway (+3 more)

### Community 179 - "CachingOptions"
Cohesion: 0.11
Nodes (21): CachingEntryDefaults, Duration, FactoryHardTimeout, FactorySoftTimeout, FailSafeMaxDuration, FailSafeThrottleDuration, CachingOptions, AllowInMemoryOnlyInProduction (+13 more)

### Community 180 - "SmsOptions"
Cohesion: 0.10
Nodes (21): SmsOptions, AppName, AttemptTimeoutSeconds, BaseUrl, MaxPerDay, MaxPerPhoneNumberPerDay, MaxRetryAttempts, MsgHeader (+13 more)

### Community 181 - "system_globalization"
Cohesion: 0.08
Nodes (24): IAM.Endpoints.Captcha.VersionNeutral, IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, IAM.Domain.Users, IAM.Endpoints.Tokens.VersionNeutral.Create, IAM.Endpoints.Users.VersionNeutral.SelfRegister, Common.Application.FeatureManagement, IAM.Endpoints.Otp, IAM.Application.Captcha.Services (+16 more)

### Community 183 - "SendRequestBody"
Cohesion: 0.25
Nodes (8): SendMessageBody, IReadOnlyList, SendRequestBody, AppName, Encoding, IysFilter, Messages, MsgHeader

### Community 184 - "OtpService"
Cohesion: 0.18
Nodes (7): OtpCodeGenerator, IFusionCache, IOptions, OtpService, IConfiguration, IServiceCollection, Setup

### Community 185 - "IdentityScheme"
Cohesion: 0.24
Nodes (8): IdentityScheme, Email, PhoneNumber, IdentitySchemeOptions, Scheme, IdentitySchemeOptionsValidator, RouteGroupBuilder, Setup

### Community 186 - ".Get"
Cohesion: 0.50
Nodes (3): Action, IEnumerable, RateLimiterOptions

### Community 187 - "ReverseProxyOptions"
Cohesion: 0.29
Nodes (6): ReverseProxyOptions, ForwardLimit, IsEnabled, TrustedNetworks, ReverseProxyOptionsValidator, IReadOnlyList

### Community 188 - "BoundedCaptureStream"
Cohesion: 0.14
Nodes (8): SeekOrigin, HttpResponse, BoundedCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 189 - "JobClaimExtensions"
Cohesion: 0.28
Nodes (9): IdBound, Value, JobClaimExtensions, CancellationToken, DateTimeOffset, DbSet, Expression, Func (+1 more)

### Community 190 - "ServiceAccountTokenCache"
Cohesion: 0.10
Nodes (13): CancellationToken, Task, KeycloakPaths, CancellationToken, DateTimeOffset, SemaphoreSlim, Task, TimeSpan (+5 more)

### Community 191 - "ProjectionUpsertOutcome"
Cohesion: 0.20
Nodes (7): Common.Application.Persistence.Projections, ProjectionEntity, SourceVersion, ProjectionUpsertOutcome, Inserted, Stale, Updated

### Community 192 - ".AddServices"
Cohesion: 0.25
Nodes (8): RecurringJobOptions, IRecurringBackgroundJobs, IConfiguration, ILogger, IServiceCollection, RecurringBackgroundJobsService, IRecurringJobManagerV2, TimeProvider

### Community 193 - ".ListPermissionsAsync"
Cohesion: 0.28
Nodes (6): CancellationToken, IReadOnlyList, Task, IKeycloakPermissionClient, IReadOnlyList, GrantedPermission

### Community 194 - "StoreId"
Cohesion: 0.25
Nodes (6): StoreId, DefaultIdType, StoreId, CancellationToken, List, Task

### Community 195 - ".ReserveStockAsync"
Cohesion: 0.22
Nodes (7): Inventory.Endpoints.StockReservations.v1.Reserve, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Id

### Community 196 - "TokenCreateRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenCreateRateLimitingPolicy (+1 more)

### Community 197 - ".Failure"
Cohesion: 0.22
Nodes (5): SearchValues, StringExtensions, Success, Func, Task

### Community 198 - "InventoryModule"
Cohesion: 0.15
Nodes (10): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, InventoryModule, ActivitySourceNames, MeterNames (+2 more)

### Community 199 - "v1/RemoveProduct/Request.cs"
Cohesion: 0.29
Nodes (7): Products.Endpoints.Stores.v1.RemoveProduct, ProductId, StoreId, Request, Id, ProductId, RequestValidator

### Community 200 - "NotificationsTelemetry"
Cohesion: 0.20
Nodes (5): ActivitySource, Counter, Meter, NotificationsTelemetry, UpDownCounter

### Community 201 - "KeycloakScopes"
Cohesion: 0.20
Nodes (9): Devices, Hangfire, KeycloakScopes, Products, ProductTemplates, Sessions, StockReservations, Stores (+1 more)

### Community 202 - "EventDispatcher"
Cohesion: 0.31
Nodes (7): EventDispatcher, ActivitySource, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task

### Community 203 - "SmsRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, SmsRateLimitingPolicy (+1 more)

### Community 205 - "StatelessInboxStore"
Cohesion: 0.24
Nodes (6): StatelessInboxStore, Instance, CancellationToken, DateTimeOffset, DefaultIdType, Task

### Community 210 - "GetProductRequest"
Cohesion: 0.39
Nodes (6): GetProductRequest, GetProductResponse, DefaultIdType, CancellationToken, Task, GetProductRequestHandler

### Community 211 - "RedisOtpService"
Cohesion: 0.32
Nodes (6): CancellationToken, IConnectionMultiplexer, IOptions, Task, TimeSpan, RedisOtpService

### Community 212 - "Stores/v1/My/Update/Request.cs"
Cohesion: 0.33
Nodes (6): Products.Endpoints.Stores.v1.My.Update, Request, Address, Description, Name, RequestValidator

### Community 213 - "BackgroundJobsTelemetry"
Cohesion: 0.20
Nodes (8): PerformedContext, BackgroundJobsTelemetry, ActivitySource, ConcurrentDictionary, Counter, Histogram, Meter, ObservableGauge

### Community 214 - "common_application_localization_resources"
Cohesion: 0.07
Nodes (26): common_application_localization_resources, IAM.Endpoints.Otp.VersionNeutral.SendForLogin, IAM.Endpoints.Tokens.VersionNeutral.Refresh, Notifications.Infrastructure.Devices.UpdateCurrentPushToken, IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, IResxLocalizer, IRuleBuilder, IRuleBuilderOptions (+18 more)

### Community 215 - "Response"
Cohesion: 0.29
Nodes (5): Products.Endpoints.Stores.v1.My.Create, RouteGroupBuilder, Endpoint, Response, Id

### Community 216 - ".UpsertIfNewerAsync"
Cohesion: 0.20
Nodes (9): ICurrentDbContext, ProjectionUpsertExtensions, Action, CancellationToken, DbContext, DbSet, Func, Task (+1 more)

### Community 217 - "FeatureFlags"
Cohesion: 0.33
Nodes (5): Checkout, FeatureFlags, IAM, Notifications, Products

### Community 218 - ".RegisterAsync"
Cohesion: 0.05
Nodes (36): IInterModuleRequestClient, CancellationToken, Task, SendPhoneOtpRequest, SendPhoneOtpResponse, CancellationToken, Task, ICaptchaService (+28 more)

### Community 219 - "BackgroundJobsService"
Cohesion: 0.19
Nodes (9): IBackgroundJobClientV2, IBackgroundJobs, Action, DateTimeOffset, Expression, Func, Task, TimeSpan (+1 more)

### Community 220 - "StrictDateTimeOffsetJsonConverter"
Cohesion: 0.33
Nodes (6): StrictDateTimeOffsetJsonConverter, DateTimeOffset, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 221 - "Keycloak realm as code"
Cohesion: 0.25
Nodes (7): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Secrets, Seed users (dev / test only)

### Community 222 - ".ListSessions"
Cohesion: 0.12
Nodes (17): DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, IReadOnlyList, CancellationToken, Task, DbSet, INotificationsDbContext (+9 more)

### Community 223 - ".PaginateAsync"
Cohesion: 0.33
Nodes (5): KeyedRow, CancellationToken, Func, IQueryable, Task

### Community 224 - ".MapEndpoint"
Cohesion: 0.40
Nodes (3): RouteGroupBuilder, RouteGroupBuilder, Setup

### Community 225 - "IInboxStore"
Cohesion: 0.24
Nodes (7): IInboxCleanupTarget, ModuleName, IInboxStore, CancellationToken, DateTimeOffset, DefaultIdType, Task

### Community 226 - ".MapCode"
Cohesion: 0.40
Nodes (3): Exception, ILogger, LoggerMessage

### Community 227 - "RegisterRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, RegisterRateLimitingPolicy (+1 more)

### Community 228 - ".AddCommonCaching"
Cohesion: 0.33
Nodes (5): Setup, IConfiguration, IConnectionMultiplexer, IServiceCollection, JsonSerializerOptions

### Community 229 - ".AddCommonOptions"
Cohesion: 0.40
Nodes (4): Setup, IConfiguration, IHostEnvironment, IServiceCollection

### Community 230 - "InterModuleRequestHandler"
Cohesion: 0.10
Nodes (22): IConsumer, IInterModuleRequest, IInterModuleRequestHandler, CancellationToken, Task, InterModuleRequestHandler, CancellationToken, ConsumeContext (+14 more)

### Community 231 - "BackgroundJobsOptions"
Cohesion: 0.13
Nodes (13): DashboardContext, IAuthorizationService, IDashboardAsyncAuthorizationFilter, BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds (+5 more)

### Community 232 - "BaseDbContext"
Cohesion: 0.11
Nodes (14): BaseDbContext, AuditLog, CancellationToken, DateTimeOffset, DbContextOptions, DbSet, ILogger, ModelConfigurationBuilder (+6 more)

### Community 233 - ".HasPatternIndex"
Cohesion: 0.36
Nodes (6): IndexBuilder, TrigramIndexExtensions, EntityTypeBuilder, Expression, Func, ModelBuilder

### Community 234 - "KeycloakPermissionAuthorizationHandler.cs"
Cohesion: 0.33
Nodes (5): IAM.Infrastructure.Auth, microsoft_aspnetcore_authentication_jwtbearer, microsoft_aspnetcore_authorization, microsoft_identitymodel_tokens, system_security_claims

### Community 235 - "AuditLogEntry"
Cohesion: 0.11
Nodes (16): AuditLogEntry, AggregateId, AggregateType, Event, EventType, Version, DefaultIdType, ModelBuilder (+8 more)

### Community 236 - "IOtpService"
Cohesion: 0.20
Nodes (8): CancellationToken, Task, TimeSpan, IOtpService, CancellationToken, Task, CancellationToken, Task

### Community 237 - "microsoft_aspnetcore_mvc"
Cohesion: 0.09
Nodes (19): Products.Endpoints.Probe.v1, Inventory.Endpoints.StockReservations.v1.Release, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, microsoft_aspnetcore_mvc, ProblemDetails, ProblemDetailsExtensions, ICollection, Request (+11 more)

### Community 238 - ".UseInfrastructure"
Cohesion: 0.13
Nodes (12): IAuthenticationSchemeProvider, IMiddleware, IApplicationBuilder, HttpContext, RequestDelegate, Task, EnrichLogsWithUserInfoMiddleware, HttpContext (+4 more)

### Community 239 - "Common.Domain.ResultMonad"
Cohesion: 0.07
Nodes (20): Notifications.Application.Push, IAM.Infrastructure.Keycloak.Representations, Notifications.Infrastructure.Email.Brevo, IAM.Domain.Captcha, Notifications.Application.Email, Common.Domain.ResultMonad, IAM.Domain.Errors, Notifications.Infrastructure.Push.Firebase (+12 more)

### Community 240 - "RabbitMqOptions"
Cohesion: 0.07
Nodes (24): ConsumerDefinition, IConsumerConfigurator, IRegistrationContext, Type, RabbitMqOptions, DefaultConcurrentMessageLimit, DefaultPrefetchCount, Host (+16 more)

### Community 241 - "DeviceRegistrationId"
Cohesion: 0.29
Nodes (4): DefaultIdType, DeviceRegistrationId, EntityTypeBuilder, DeviceRegistrationConfiguration

### Community 242 - "V1StockReservationCommitConflictDetectedDomainEvent"
Cohesion: 0.20
Nodes (8): DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationCommitConflictDetectedDomainEvent, DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationCommittedDomainEvent

### Community 243 - "CustomValidator"
Cohesion: 0.10
Nodes (26): AbstractValidator, Inventory.Endpoints.StockReservations.v1.Commit, CustomRateLimitingOptionsValidator, FixedWindowValidator, KeyedResilienceProfileValidator, ResiliencyOptionsValidator, KeyValuePair, CustomValidator (+18 more)

### Community 244 - "KeyedResilienceProfile"
Cohesion: 0.20
Nodes (10): KeyedResilienceProfile, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, RateLimitFailOpen, RateLimitPermits (+2 more)

### Community 245 - "SendForEmail/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForEmail, Request, CaptchaToken, Email, RequestValidator

### Community 246 - "Stores/v1/Create/Request.cs"
Cohesion: 0.33
Nodes (6): Request, Address, Description, Name, OwnerId, RequestValidator

### Community 247 - "OpenApiOptions"
Cohesion: 0.22
Nodes (9): OpenApiOptions, ContactEmail, ContactName, Description, EnableSwagger, LicenseName, LicenseUrl, Title (+1 more)

### Community 248 - "SendErrorBody"
Cohesion: 0.67
Nodes (3): SendErrorBody, Code, Message

### Community 249 - "DeviceRegistryReconciliationService"
Cohesion: 0.17
Nodes (14): GetActiveSessionIdsRequest, GetActiveSessionIdsResponse, UserSessionIds, IReadOnlyList, CancellationToken, Task, GetActiveSessionIdsRequestHandler, CancellationToken (+6 more)

### Community 250 - "AuditableEntityResponse"
Cohesion: 0.08
Nodes (19): Products.Endpoints.Products.v1.My.Get, Products.Endpoints.Products.v1.Search, Common.Application.DTOs, Products.Endpoints.Stores.v1.Get, Products.Endpoints.Products.v1.Get, AuditableEntityResponse, CreatedBy, CreatedOn (+11 more)

### Community 251 - "ProductId"
Cohesion: 0.10
Nodes (17): Products.Endpoints.Stores.v1.My.AddProduct, Products.Endpoints.Stores.v1.My.RemoveProduct, DefaultIdType, ProductId, Request, Id, RequestValidator, Request (+9 more)

### Community 252 - ".TryWriteAsync"
Cohesion: 0.40
Nodes (4): ProblemDetailsContext, ProblemDetailsServiceExtensions, IProblemDetailsService, Task

### Community 260 - "SmsOtpDispatchOutcome"
Cohesion: 0.33
Nodes (5): OtpDispatchErrors, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled

### Community 261 - "ProductTemplates/v1/Deactivate/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.ProductTemplates.v1.Deactivate, Request, Id, RequestValidator

### Community 262 - ".SendWithPipelineAsync"
Cohesion: 0.25
Nodes (7): HttpClientKeyedExtensions, CancellationToken, HttpClient, HttpRequestMessage, HttpResponseMessage, ResiliencePipeline, Task

### Community 263 - "Response"
Cohesion: 0.29
Nodes (6): DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 264 - "ResxLocalizationOptions"
Cohesion: 0.40
Nodes (5): ResxLocalizationOptions, DefaultCulture, SupportedCultures, ResxLocalizationOptionsValidator, ICollection

### Community 265 - "BackgroundJobsModule"
Cohesion: 0.25
Nodes (7): BackgroundJobsModule, ActivitySourceNames, MeterNames, Name, StartupPriority, IEndpointRouteBuilder, IEnumerable

### Community 266 - "SwaggerDefaultValues"
Cohesion: 0.33
Nodes (5): IOperationFilter, JsonValue, OpenApiOperation, OperationFilterContext, SwaggerDefaultValues

### Community 267 - "AuditLogOptions"
Cohesion: 0.33
Nodes (6): AuditLogOptions, PerSchemaRetentionDays, PurgeBatchSize, RetentionDays, AuditLogOptionsValidator, Dictionary

### Community 268 - "Activate/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.ProductTemplates.v1.Activate, Request, Id, RequestValidator

### Community 269 - "IProductsDbContext"
Cohesion: 0.02
Nodes (70): ICurrentUser, Id, IdAsString, Roles, SessionId, ICollection, CancellationToken, Task (+62 more)

### Community 270 - "NetGsmSmsGateway"
Cohesion: 0.19
Nodes (10): CancellationToken, HttpClient, IOptions, JsonSerializerOptions, SendRequestBody, Task, NetGsmSmsGateway, SendMessageBody (+2 more)

### Community 271 - "ProductTemplates/v1/Create/Request.cs"
Cohesion: 0.40
Nodes (5): Request, Brand, Color, Model, RequestValidator

### Community 272 - ".AddResilientHttpClient"
Cohesion: 0.22
Nodes (8): HttpStandardResilienceOptions, IHttpClientBuilder, Setup, Action, HttpClient, IOptions, IServiceCollection, IServiceProvider

### Community 273 - "IntegrationEventConverter"
Cohesion: 0.25
Nodes (6): IEntityTypeConfiguration, IntegrationEventConverter, JsonSerializerOptions, ModelBuilder, EntityTypeBuilder, OutboxMessageConfig

### Community 274 - "Infrastructure/StringExtensions.cs"
Cohesion: 0.40
Nodes (3): opentelemetry_exporter, OtlpExportProtocol, StringExtensions

### Community 275 - "JsonConverter"
Cohesion: 0.31
Nodes (7): JsonConverter, StronglyTypedIdListReadOnlyJsonConverter, IReadOnlyList, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 276 - "StronglyTypedIdWriteOnlyJsonConverter"
Cohesion: 0.32
Nodes (5): StronglyTypedIdWriteOnlyJsonConverter, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 277 - "SendResponseBody"
Cohesion: 0.50
Nodes (4): SendResponseBody, Code, Description, JobId

### Community 278 - "Setup.Observability.cs"
Cohesion: 0.40
Nodes (4): opentelemetry_metrics, opentelemetry_opentelemetrybuilder, opentelemetry_resources, opentelemetry_trace

### Community 279 - "Response"
Cohesion: 0.29
Nodes (6): DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 280 - "StronglyTypedIdSchemaFilter.cs"
Cohesion: 0.33
Nodes (4): IOpenApiSchema, ISchemaFilter, SchemaFilterContext, StronglyTypedIdSchemaFilter

### Community 281 - ".TryReadFromJsonAsync"
Cohesion: 0.40
Nodes (4): HttpContent, CancellationToken, Task, HttpContentJsonExtensions

### Community 282 - "Key decisions"
Cohesion: 0.29
Nodes (7): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Key decisions

### Community 283 - "AuditableEntity"
Cohesion: 0.25
Nodes (8): AuditableEntity, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, Version, DateTimeOffset

### Community 284 - "Reserve/Request.cs"
Cohesion: 0.20
Nodes (10): DateTimeOffset, DefaultIdType, RequestBody, Request, Body, RequestBody, ProductId, Quantity (+2 more)

### Community 285 - "RequestBodyLimitFilter.cs"
Cohesion: 0.15
Nodes (14): Common.Endpoints.Webhooks, microsoft_aspnetcore_http_features, RequestBodyLimitEndpointExtensions, RequestBodyLimitFilter, Func, HttpContext, RequestBodyLimitMetadata, MaxBodyBytes (+6 more)

### Community 286 - ".InvokeAsync"
Cohesion: 0.40
Nodes (4): EndpointFilterDelegate, EndpointFilterInvocationContext, IHttpMaxRequestBodySizeFeature, ValueTask

### Community 287 - "OtpVerificationOutcome"
Cohesion: 0.40
Nodes (4): OtpVerificationOutcome, InvalidOtp, Success, TooManyAttempts

### Community 288 - "SmsMessage"
Cohesion: 0.20
Nodes (10): SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, SmsMessage, CancellationToken, ILogger, LoggerMessage (+2 more)

### Community 289 - ".SetRetryAfterHeader"
Cohesion: 0.40
Nodes (4): RetryAfterHeaderExtensions, HttpResponse, RateLimitLease, TimeSpan

### Community 290 - "IssueVerificationTokenRequestHandler"
Cohesion: 0.39
Nodes (6): IssueVerificationTokenRequest, IssueVerificationTokenResponse, CancellationToken, IOptions, Task, IssueVerificationTokenRequestHandler

### Community 294 - "KeyedRow"
Cohesion: 0.50
Nodes (4): KeyedRow, Item, Sort, Tie

### Community 298 - ".AddCustomSwagger"
Cohesion: 0.20
Nodes (7): IApplicationBuilder, IConfigureOptions, IServiceCollection, IWebHostEnvironment, SwaggerGenOptions, Type, Setup

### Community 299 - ".MapEndpoint"
Cohesion: 0.40
Nodes (3): RouteGroupBuilder, RouteGroupBuilder, Setup

### Community 300 - "ResultTelemetryExtensions"
Cohesion: 0.38
Nodes (4): Activity, ResultTelemetryExtensions, ActivitySource, Task

### Community 301 - "JobHousekeepingOptions"
Cohesion: 0.40
Nodes (5): JobHousekeepingOptions, PageSize, RetentionHours, StaleAfterMinutes, JobHousekeepingOptionsValidator

### Community 303 - "DefaultResponsesOperationFilter"
Cohesion: 0.70
Nodes (3): OpenApiOperation, OperationFilterContext, DefaultResponsesOperationFilter

### Community 304 - "JobStatus"
Cohesion: 0.33
Nodes (5): JobStatus, Failed, Queued, Running, Succeeded

### Community 307 - "SignalROptions"
Cohesion: 0.40
Nodes (5): SignalROptions, RedisConnectionString, RequireRedisBackplaneInProduction, UseRedisBackplane, SignalROptionsValidator

### Community 308 - "PublishOutcome"
Cohesion: 0.50
Nodes (4): PublishOutcome, Failed, Published, Skipped

### Community 311 - "Common.Application.ModelBinders"
Cohesion: 0.08
Nodes (22): Products.Endpoints.Stores.v1.Deactivate, Common.Application.ModelBinders, IModelBinder, microsoft_aspnetcore_mvc_modelbinding, ModelBindingContext, StronglyTypedIdBinder, Task, Request (+14 more)

### Community 314 - "ModulesOptions.cs"
Cohesion: 0.50
Nodes (4): ModulesOptions, EnabledModules, ModulesOptionsValidator, IReadOnlyList

### Community 318 - "SecurityHeadersOptions.cs"
Cohesion: 0.50
Nodes (4): SecurityHeadersOptions, Headers, SecurityHeadersOptionsValidator, Dictionary

### Community 319 - "Products/v1/Update/Request.cs"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Products.v1.Update, RequestBody, Request, Body, Id, RequestBody, Description, Name (+3 more)

### Community 320 - "Infrastructure/Setup.cs"
Cohesion: 0.09
Nodes (15): Products.Infrastructure.Persistence, Common.InterModuleRequests, Common.Infrastructure.Localization, Common.Infrastructure.Auth.Services, Common.Infrastructure.EventBus, Common.Infrastructure.FeatureManagement, Common.Infrastructure.Caching, Common.Infrastructure.Auth (+7 more)

### Community 322 - "VerifyEmail/Request.cs"
Cohesion: 0.50
Nodes (4): Request, Email, Otp, RequestValidator

### Community 324 - "Products/v1/My/Update/Request.cs"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Products.v1.My.Update, RequestBody, Request, Body, Id, RequestBody, Description, Name (+3 more)

### Community 326 - "fluentvalidation"
Cohesion: 0.07
Nodes (23): Common.Application.Validation, fluentvalidation, BackgroundJobsOptionsValidator, CorsOptionsValidator, DatabaseOptions, ConnectionString, DatabaseOptionsValidator, DevicesOptionsValidator (+15 more)

## Knowledge Gaps
- **951 isolated node(s):** `Id`, `IdAsString`, `Roles`, `SessionId`, `JwtClaimNames` (+946 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 2227 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **103 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Result` connect `Result` to `.ReserveSeriesAsync`, `SmsOtpDispatchOutcome`, `FirebasePushGateway`, `Error`, `ProductTemplate`, `ApplicationUserId`, `IProductsDbContext`, `NetGsmSmsGateway`, `.RegisterAsync`, `StockReservation`, `.CreateTokensByEmail`, `IEmailGateway`, `.RevokeSession`, `ISmsGateway`, `IInventoryDbContext`, `SmsMessage`, `.SearchMyProductsAsync`, `ThrottledEmailGateway`, `ResultTelemetryExtensions`, `IResult`, `PaginationResponse`, `.AddPushServices`, `ReCaptchaService`, `EmailOtpDispatchOutcome`, `BrevoEmailGateway`, `.SearchProductTemplatesAsync`, `KeycloakTokenClient`, `.SendAsync`, `.ReserveStockAsync`, `.Failure`, `Response`, `DomainEvent`, `PaginationCursor`, `.RegisterAsync`, `.GetProductAsync`, `.ListSessions`, `.PaginateAsync`, `.MapCode`, `.SearchStoreProductsAsync`, `.IsRegisteredAsync`, `.ReleaseStockReservationAsync`, `.AddProductAsync`, `V1StockReservationCommitConflictDetectedDomainEvent`, `Response`, `.HandleWarehouseWebhookAsync`, `Response`?**
  _High betweenness centrality (0.171) - this node is a cross-community bridge._
- **Why does `Common.Application.Options` connect `Common.Application.Options` to `ResxLocalizationOptions`, `AuditLogOptions`, `microsoft_aspnetcore_http`, `microsoft_extensions_dependencyinjection`, `Setup`, `EmailOptions`, `Setup.Observability.cs`, `OutboxOptions`, `Setup.Logger.cs`, `Common.Domain.StronglyTypedIds`, `ConfigureSwaggerOptions.cs`, `Tokens/VersionNeutral/Setup.cs`, `JobHousekeepingOptions`, `ziggycreatures_caching_fusion`, `Inventory.Domain.StockReservations`, `CachingOptions`, `ReCaptchaService`, `SignalROptions`, `SmsOptions`, `system_globalization`, `IdentityScheme`, `ModulesOptions.cs`, `ReverseProxyOptions`, `SecurityHeadersOptions.cs`, `RequestLoggingOptions`, `Infrastructure/Setup.cs`, `.AddNotificationsSignalR`, `fluentvalidation`, `microsoft_entityframeworkcore`, `PushOptions`, `InventoryOptions`, `KeycloakPermissionAuthorizationHandler.cs`, `Common.InterModuleRequests.Contracts`, `Common.Domain.ResultMonad`, `.ReleaseStockReservationAsync`, `CustomValidator`, `OpenApiOptions`?**
  _High betweenness centrality (0.162) - this node is a cross-community bridge._
- **Why does `ApplicationUserId` connect `ApplicationUserId` to `NotificationPayload`, `KeycloakAdminClient`, `IProductsDbContext`, `IStronglyTypedId`, `KeycloakPermissionAuthorizationHandler`, `Response`, `.RegisterAsync`, `DeviceRegistration`, `.RevokeSession`, `AuditableEntity`, `JobRow`, `For`, `CurrentUser`, `SendSecurityAlertRequestHandler`, `IntegrationEvent`, `PaginationResponse`, `V1StoreCreatedDomainEvent`, `Response`, `Common.Application.ModelBinders`, `KeycloakTokenClient`, `StoreId`, `.HandleAsync`, `Response`, `IAuditableEntity`, `Response`, `.Configure`, `Store`, `.RegisterAsync`, `v1/AddProduct/Request.cs`, `.ListSessions`, `InterModuleRequestHandler`, `StockLevel`, `DeviceRegistrationId`, `Response`, `Stores/v1/Create/Request.cs`, `DeviceRegistryReconciliationService`, `AuditableEntityResponse`, `NotificationsHub`, `Response`?**
  _High betweenness centrality (0.073) - this node is a cross-community bridge._
- **What connects `Id`, `IdAsString`, `Roles` to the rest of the system?**
  _951 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `.ReserveSeriesAsync` be split into smaller, more focused modules?**
  _Cohesion score 0.14166666666666666 - nodes in this community are weakly interconnected._
- **Should `KeycloakAdminClient` be split into smaller, more focused modules?**
  _Cohesion score 0.0703962703962704 - nodes in this community are weakly interconnected._
- **Should `RedisFixedWindowRateLimiter` be split into smaller, more focused modules?**
  _Cohesion score 0.13333333333333333 - nodes in this community are weakly interconnected._