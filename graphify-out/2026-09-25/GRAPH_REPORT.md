# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-09-25)

## Corpus Check
- 575 files · ~90,792 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 4945 nodes · 9765 edges · 368 communities (272 shown, 96 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 260 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `60b75cba`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- .ReserveSeriesAsync
- NotificationPayload
- OutboxProcessor
- Common.Application.DTOs
- Hybrid DDD (Writes) / VSA (Reads)
- KeycloakAdminClient
- RedisFixedWindowRateLimiter
- .AddModuleDbContext
- FirebasePushGateway
- .RequireOtpTemplateForDefaultCulture
- Error
- Common.Domain.ResultMonad
- ApplicationUserId
- ISearchLanguageResolver
- Common.Application.Options
- KeycloakPermissionAuthorizationHandler
- EmailOptions
- Product
- DomainEvent
- Common.Domain.Events
- LogProductCatalogChangeHandler
- BoundedRequestCaptureStream
- DeviceRegistration
- DummyEmailGateway
- .RevokeToken
- microsoft_extensions_options
- IEvent
- RequestResponseBodyLoggingMiddleware
- Common.Domain.StronglyTypedIds
- Result
- JobRow
- NotificationsModule
- .SingleAsResult
- .RefreshToken
- KeycloakPermissionClient
- OutboxMessage
- CustomRateLimitingOptions
- UserRepresentation
- RequestBody
- ObservabilityOptions
- ThrottledEmailGateway.cs
- Request
- .SaveWithOutboxAsync
- CreateStoreRateLimitingPolicy
- KeycloakOptions
- .AddKeycloakInfrastructure
- PaginationQueryableExtensions
- Outbox Misuse Check
- microsoft_entityframeworkcore
- Add Integration Event Command
- Response
- ProductsDbContext
- ReCaptchaService
- Request
- .SendOtp
- BrevoEmailGateway
- ProjectionReconciliationJob
- Cross-Module Reference Violation
- OutboxMetricsJob
- .SearchProductTemplatesAsync
- Bogus Test Data
- KeycloakTokenClient
- JobHousekeepingJob
- RequestLoggingOptions
- .SendAsync
- BackgroundJobsService
- OutboxModule
- .AddNotificationsSignalR
- ValueObject
- .HandleAsync
- IntegrationEventHandlerBase
- FullTextSearchOptions
- AuditableEntityResponse
- .WriteAsync
- AsNoTracking Coverage Check
- Setup
- Response
- AuditLogRetentionService
- CheckRegistrationRateLimitingPolicy
- IAggregateRoot
- IAuditableEntity
- .SearchStoresAsync
- Split-Deployment PoC
- ICaptchaService
- .Configure
- .CreateMyStoreAsync
- Store
- PushOptions
- Configuration-Driven Module Registration
- Setup
- IInterModuleRequestClient
- .SeedProductAsync
- .AddProductToMyStoreAsync
- .MapEndpoint
- KeyedResiliencePipelines
- .SearchMyProductsAsync
- DatabaseSeederOrchestrator
- StockReservationExpirySweepService
- .SearchStoreProductsAsync
- AuditLogRetentionJobRegistrar
- OutboxDbContext
- ResiliencyOptions
- HttpContextTargetingContextAccessor
- InboxCleanupJob
- CaptchaOptions
- Response
- Common.InterModuleRequests.Contracts
- RabbitMqOptions
- StockLevel
- .IsRegisteredAsync
- KeycloakPermissionPolicyProvider
- .ReleaseStockReservationAsync
- OutboxCleanupJob
- SeedingCompletionTracker
- Response
- .AddServices
- .AddAuthInfrastructure
- IInventoryDbContext
- IModule
- ProductsModule
- InventoryOptions
- .GetVariantAsync
- OtpVerifyRateLimitingPolicy
- NotificationsHub
- .AddCustomHealthChecks
- .AddObservability
- GlobalExceptionHandlingMiddleware
- Response
- EmailRateLimitingPolicy
- Seeder
- docker-compose.yml (Base Stack)
- .AddInfrastructure
- StoreId
- Consumer Idempotency (IntegrationEventHandlerBase)
- SendRequestBody
- IDatabaseSeeder
- ProductTemplates/v1/Search/Request.cs
- .WriteTooManyRequestsToResponse
- .From
- ProductTemplate
- ProcessedMessage
- IStronglyTypedId
- TokenRefreshRateLimitingPolicy
- Setup
- Response
- RequestBody
- .RegisterAsync
- IRecurringBackgroundJobs
- .CreateTokensByEmail
- Stores/v1/Update/Request.cs
- StockReservationExpirySweepJobRegistrar
- OutboxOptions
- .EnsureNoMigrationsPending
- ConfigureSwaggerOptions
- Setup.Logger.cs
- ISmsGateway
- Request
- IntegrationEvent
- OtpOptions
- InventoryDbContext
- OtpServiceBase
- IntegrationEventOutbox
- IEmailGateway
- For
- NotificationsDbContext
- ICurrentUser
- IamModule
- PaginationRequest
- .MapEndpoint
- SendSecurityAlertRequestHandler
- Notifications.Application/IAssemblyReference.cs
- VerifyPhoneOtpResponse
- .MapEndpoint
- ProductTemplateId
- system_diagnostics
- PaginationResponse
- .FixedWindow
- CorsOptions
- PushMessage
- CachingOptions
- SmsOptions
- ProductTemplates/v1/Create/Request.cs
- IAM.Domain
- SendRequestBody
- RedisOtpService
- IdentityScheme
- .Get
- ReverseProxyOptions
- BoundedCaptureStream
- JobClaimExtensions
- ServiceAccountTokenCache
- ProjectionUpsertOutcome
- .ApplyConfigurations
- .GetMeAsync
- EventDispatcher
- .ReserveStockAsync
- TokenCreateRateLimitingPolicy
- Request
- InventoryModule
- ReCaptchaResponse
- NotificationsTelemetry
- KeycloakScopes
- Request
- SmsRateLimitingPolicy
- Configuration-Driven Module Loading
- StatelessInboxStore
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- BackgroundJobsOptions
- Key decisions
- Stores/v1/My/Update/Request.cs
- BackgroundJobsTelemetry
- fluentvalidation
- DeviceRegistrationId
- .UpsertIfNewerAsync
- FeatureFlags
- .SendOtp
- IBackgroundJobs
- .AddBrevo
- Keycloak realm as code
- .ListSessions
- KeycloakPermission
- .MapEndpoint
- IInboxStore
- .MapCode
- RegisterRateLimitingPolicy
- .AddCommonCaching
- .AddCommonOptions
- InterModuleRequestHandler
- .UseModule
- DomainEventConverter
- .HasPatternIndex
- KeycloakPermissionAuthorizationHandler.cs
- BaseDbContext
- V1StockLevelCreatedDomainEvent
- ProblemDetailsExtensions.cs
- .UseInfrastructure
- HttpWarehouseGateway.cs
- RemoveDefaultResponseSchemaFilter
- .CreateStorePolicy
- CustomValidator
- KeyedResilienceProfile
- RequestBodyLimitMetadata
- OpenApiOptions
- SendErrorBody
- DeviceRegistryReconciliationService
- Response
- ProductId
- .TryWriteAsync
- Products.Domain/IAssemblyReference.cs
- IAM.Application/IAssemblyReference.cs
- Products.Infrastructure/IAssemblyReference.cs
- Products.Application/IAssemblyReference.cs
- Common.Infrastructure/IAssemblyReference.cs
- Notifications.Domain/IAssemblyReference.cs
- IAM.Infrastructure/IAssemblyReference.cs
- IDbContext
- .SendWithPipelineAsync
- Response
- ResxLocalizationOptions
- BackgroundJobsModule
- SwaggerDefaultValues
- AuditLogOptions
- IProductsDbContext
- NetGsmSmsGateway
- .AddResilientHttpClient
- .AddNetGsm
- Infrastructure/StringExtensions.cs
- EnrichLogsWithUserInfoMiddleware
- SendResponseBody
- .TapWhenFeatureEnabledAsync
- Response
- .SetRetryAfterHeader
- AuditableEntity
- RequestBody
- RequestBodyLimitFilter.cs
- IOtpService
- DummySmsGateway
- .AddServices
- .MapEndpoints
- StronglyTypedIdBinder
- .SetConcurrency
- Setup
- .MapEndpoint
- ResultTelemetryExtensions
- ValidationContextExtensions
- .EmailVerificationTokenValidation
- DefaultResponsesOperationFilter
- JobStatus
- .AddCustomSwagger
- .PhoneNumberValidation
- SignalROptions
- PublishOutcome
- .Capture
- microsoft_aspnetcore_mvc
- ProductsTelemetry
- Common.InterModuleRequests/Setup.cs
- Setup
- Setup
- .UseGlobalExceptionHandlingMiddleware
- RequestBody
- system_runtime_compilerservices
- .AddGlobalExceptionHandlingMiddleware
- RequestBody
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
2. `Common.Application.Options` - 134 edges
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
- `Add search to a new entity _(Build checklist)_` --references--> `ApplySearchLanguageInterceptor`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Infrastructure/Persistence/Auditing/ApplySearchLanguageInterceptor.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Local Infrastructure Stack** — docker_compose_postgres, docker_compose_rabbitmq, docker_compose_redis, docker_compose_aspire_dashboard [EXTRACTED 1.00]

## Communities (368 total, 96 thin omitted)

### Community 0 - ".ReserveSeriesAsync"
Cohesion: 0.10
Nodes (20): Products.Infrastructure.InterModuleRequestHandlers, Inventory.Endpoints.StockReservations.v1.ReserveSeries, Common.InterModuleRequests.Products, GetProductRequest, GetProductResponse, DefaultIdType, CancellationToken, IOptions (+12 more)

### Community 1 - "NotificationPayload"
Cohesion: 0.18
Nodes (13): Notifications.Application.Hubs, IHubContext, CancellationToken, IReadOnlyList, Task, INotificationDispatcher, Task, INotificationsClient (+5 more)

### Community 2 - "OutboxProcessor"
Cohesion: 0.21
Nodes (12): IPublishEndpoint, PublishOutcome, CancellationToken, Exception, ILogger, IOptions, IServiceScopeFactory, LoggerMessage (+4 more)

### Community 3 - "Common.Application.DTOs"
Cohesion: 0.09
Nodes (16): Products.Endpoints.Products.v1.My.Get, Common.Application.DTOs, Products.Endpoints.Stores.v1.Get, Products.Endpoints.Products.v1.My.Search, Products.Endpoints.Stores.v1.My.Get, Request, Id, RequestValidator (+8 more)

### Community 5 - "KeycloakAdminClient"
Cohesion: 0.13
Nodes (24): DateOnly, DateTimeOffset, IReadOnlyList, CreateKeycloakUser, GrantedPermission, KeycloakUser, KeycloakUserPage, KeycloakUserSession (+16 more)

### Community 6 - "RedisFixedWindowRateLimiter"
Cohesion: 0.13
Nodes (16): RateLimiter, RateLimiterStatistics, FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration, CancellationToken (+8 more)

### Community 7 - ".AddModuleDbContext"
Cohesion: 0.18
Nodes (10): SaveChangesInterceptor, ApplyAuditingInterceptor, TimeProvider, ApplySearchLanguageInterceptor, Setup, IServiceCollection, Setup, IServiceCollection (+2 more)

### Community 8 - "FirebasePushGateway"
Cohesion: 0.16
Nodes (12): FirebaseApp, FirebaseMessaging, IDisposable, CancellationToken, Exception, IEnumerable, ILogger, IReadOnlyList (+4 more)

### Community 9 - ".RequireOtpTemplateForDefaultCulture"
Cohesion: 0.40
Nodes (4): Func, IConfiguration, IEnumerable, OtpTemplateConfiguration

### Community 10 - "Error"
Cohesion: 0.05
Nodes (32): Inventory.Domain.StockReservations.Errors, SearchValues, StringLocalizerExtensions, IStringLocalizer, StringExtensions, Error, Key, ParameterName (+24 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.09
Nodes (30): IAM.Endpoints.Captcha.VersionNeutral, Common.Application.Search, Products.Endpoints.Stores, Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Products.Endpoints.ProductTemplates, Common.Application.EndpointFilters (+22 more)

### Community 12 - "ApplicationUserId"
Cohesion: 0.10
Nodes (19): ApplicationUserId, IsEmpty, Value, DefaultIdType, CancellationToken, IReadOnlyList, Task, IKeycloakAdminClient (+11 more)

### Community 13 - "ISearchLanguageResolver"
Cohesion: 0.14
Nodes (12): Configuration, ISearchLanguageResolver, UniversalConfig, SearchLanguageResolver, UniversalConfig, IOptions, Setup, IServiceCollection (+4 more)

### Community 14 - "Common.Application.Options"
Cohesion: 0.05
Nodes (48): asp_versioning, asp_versioning_builder, asp_versioning_conventions, Common.Infrastructure.Modules, Notifications.Application.Sms, Notifications.Infrastructure.Devices, Notifications.Application.Push, Inventory.Endpoints (+40 more)

### Community 15 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.13
Nodes (17): AuthorizationHandler, AuthorizationHandlerContext, IAuthorizationRequirement, CancellationToken, ClaimsPrincipal, HttpContext, IFusionCache, IHttpContextAccessor (+9 more)

### Community 16 - "EmailOptions"
Cohesion: 0.09
Nodes (24): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+16 more)

### Community 17 - "Product"
Cohesion: 0.06
Nodes (32): ProductId, V1ProductDescriptionUpdatedDomainEvent, ProductId, V1ProductNameUpdatedDomainEvent, ProductId, V1ProductQuantityDecreasedDomainEvent, ProductTemplate, ProductTemplateId (+24 more)

### Community 18 - "DomainEvent"
Cohesion: 0.04
Nodes (49): Inventory.Domain.StockReservations.DomainEvents.v1, DomainEvent, CreatedOn, Id, Version, DateTimeOffset, DefaultIdType, DateTimeOffset (+41 more)

### Community 19 - "Common.Domain.Events"
Cohesion: 0.12
Nodes (8): Products.Domain.Products.DomainEvents.v1, Products.Infrastructure.Persistence.Seeding, Common.Domain.Events, Products.Domain.Products, Products.Domain.Stores, Products.Domain.ProductTemplates, Products.Domain.Stores.DomainEvents.v1, Constants

### Community 20 - "LogProductCatalogChangeHandler"
Cohesion: 0.24
Nodes (8): CancellationToken, DefaultIdType, IFusionCache, ILogger, IOptions, LoggerMessage, Task, LogProductCatalogChangeHandler

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.18
Nodes (7): BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position, Stream

### Community 22 - "DeviceRegistration"
Cohesion: 0.16
Nodes (12): DateTimeOffset, Guid, DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive, LastReconciledOn (+4 more)

### Community 23 - "DummyEmailGateway"
Cohesion: 0.38
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, DummyEmailGateway

### Community 24 - ".RevokeToken"
Cohesion: 0.13
Nodes (14): DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse, IReadOnlyList, CancellationToken, RouteGroupBuilder, Task, Endpoint, CancellationToken (+6 more)

### Community 25 - "microsoft_extensions_options"
Cohesion: 0.07
Nodes (29): IAM.Infrastructure.Keycloak.Representations, Common.Infrastructure.RateLimiting, Notifications.Infrastructure.Email.Brevo, Products.Infrastructure.RateLimiting, Common.Infrastructure.Extensions, IAM.Endpoints.Otp.VersionNeutral, IAM.Infrastructure.RateLimiting, Notifications.Infrastructure.Sms.NetGsm (+21 more)

### Community 26 - "IEvent"
Cohesion: 0.11
Nodes (14): DomainEventHandlerBase, CancellationToken, Task, IEventHandler, CancellationToken, Task, IEventHandlerWrapper, CancellationToken (+6 more)

### Community 27 - "RequestResponseBodyLoggingMiddleware"
Cohesion: 0.22
Nodes (7): IDiagnosticContext, HttpContext, IList, IOptions, PathString, RequestDelegate, RequestResponseBodyLoggingMiddleware

### Community 28 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.09
Nodes (23): Common.Domain.StronglyTypedIds, Common.Infrastructure.Localization, Inventory.Endpoints.StockReservations.v1.WebhookCallback, Common.Application.JsonConverters, Common.Infrastructure.FeatureManagement, Common.Application.Pagination, Common.Domain.Aggregates, Common.Infrastructure.Caching (+15 more)

### Community 29 - "Result"
Cohesion: 0.11
Nodes (16): Result, Error, IsFailure, Success, Value, Func, Task, AsyncExtensions (+8 more)

### Community 30 - "JobRow"
Cohesion: 0.19
Nodes (9): JobRow, FailureKey, FinishedOn, Progress, QueuedOn, RequestedBy, StartedOn, Status (+1 more)

### Community 31 - "NotificationsModule"
Cohesion: 0.15
Nodes (10): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, NotificationsModule, ActivitySourceNames, MeterNames (+2 more)

### Community 32 - ".SingleAsResult"
Cohesion: 0.18
Nodes (10): CollectionExtensions, Func, ICollection, IEnumerable, PersistenceQueryableExtensions, CancellationToken, Expression, Func (+2 more)

### Community 33 - ".RefreshToken"
Cohesion: 0.15
Nodes (11): IAM.Endpoints.Tokens.VersionNeutral.Refresh, CancellationToken, RouteGroupBuilder, Task, Endpoint, DateTimeOffset, Response, AccessToken (+3 more)

### Community 34 - "KeycloakPermissionClient"
Cohesion: 0.13
Nodes (20): CancellationToken, Dictionary, HttpClient, HttpResponseMessage, ILogger, IOptions, IReadOnlyList, List (+12 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.08
Nodes (21): Common.Application.Persistence.Outbox, IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, DateTimeOffset (+13 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.08
Nodes (22): CheckRegistrationRateLimitingPolicy, EmailRateLimitingPolicy, OtpVerifyRateLimitingPolicy, RegisterRateLimitingPolicy, SmsRateLimitingPolicy, CustomRateLimitingOptions, CheckRegistration, CreateStore (+14 more)

### Community 37 - "UserRepresentation"
Cohesion: 0.07
Nodes (29): Dictionary, List, CredentialRepresentation, Temporary, Type, Value, ErrorRepresentation, Error (+21 more)

### Community 38 - "RequestBody"
Cohesion: 0.15
Nodes (14): DateTimeOffset, DefaultIdType, ICollection, RequestBody, FirstDeadline, IntervalDays, OccurrenceCount, ProductId (+6 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.10
Nodes (20): IHostBuilder, ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics, EnableTracing, LogSink (+12 more)

### Community 40 - "ThrottledEmailGateway.cs"
Cohesion: 0.11
Nodes (14): asp_versioning_apiexplorer, Notifications.Infrastructure.Email, Host, Notifications.Application.Email, Host.Swagger, microsoft_aspnetcore_mvc_apiexplorer, microsoft_openapi, serilog (+6 more)

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

### Community 45 - ".AddKeycloakInfrastructure"
Cohesion: 0.16
Nodes (10): CancellationToken, Task, IServiceAccountTokenProvider, HttpClient, IOptions, TimeProvider, ServiceAccountTokenProvider, IOptions (+2 more)

### Community 46 - "PaginationQueryableExtensions"
Cohesion: 0.08
Nodes (27): BinaryExpression, ExpressionVisitor, KeyedRow, MemberExpression, MethodInfo, ParameterExpression, Payload, PaginationCursor (+19 more)

### Community 48 - "microsoft_entityframeworkcore"
Cohesion: 0.05
Nodes (41): Common.Infrastructure.Persistence.Inbox, Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Common.Application.Persistence.Inbox, Inventory.Infrastructure.InterModuleRequestHandlers, Inventory.Application.Persistence, Inventory.Application.IntegrationEventHandlers, Common.IntegrationEvents (+33 more)

### Community 50 - "Response"
Cohesion: 0.12
Nodes (16): CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate (+8 more)

### Community 51 - "ProductsDbContext"
Cohesion: 0.13
Nodes (14): DbContextOptions, DbSet, ILogger, ProductTemplate, Store, TimeProvider, ProductsDbContext, Products (+6 more)

### Community 52 - "ReCaptchaService"
Cohesion: 0.21
Nodes (10): FormUrlEncodedContent, ReCaptchaResponse, CancellationToken, Exception, HttpClient, ILogger, IOptions, LoggerMessage (+2 more)

### Community 53 - "Request"
Cohesion: 0.15
Nodes (13): Guid, Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FirstName (+5 more)

### Community 54 - ".SendOtp"
Cohesion: 0.14
Nodes (13): SendEmailOtpRequest, SendEmailOtpResponse, CancellationToken, IFeatureManager, RouteGroupBuilder, Task, Endpoint, CancellationToken (+5 more)

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
Cohesion: 0.18
Nodes (10): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Brand (+2 more)

### Community 61 - "KeycloakTokenClient"
Cohesion: 0.11
Nodes (24): JsonWebTokenHandler, CancellationToken, Task, IKeycloakTokenClient, DateTimeOffset, KeycloakTokens, CancellationToken, Dictionary (+16 more)

### Community 62 - "JobHousekeepingJob"
Cohesion: 0.14
Nodes (14): Deleted, JobHousekeepingJob, CancellationToken, ILogger, IOptions, LoggerMessage, Task, TimeProvider (+6 more)

### Community 63 - "RequestLoggingOptions"
Cohesion: 0.11
Nodes (20): IPostConfigureOptions, RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes (+12 more)

### Community 64 - ".SendAsync"
Cohesion: 0.25
Nodes (6): CancellationToken, HttpResponseMessage, Task, SendContact, Email, Name

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

### Community 70 - "IntegrationEventHandlerBase"
Cohesion: 0.22
Nodes (12): IConsumer, IntegrationEventHandlerBase, MaxEventAge, CancellationToken, ConsumeContext, DefaultIdType, IFusionCache, ILogger (+4 more)

### Community 71 - "FullTextSearchOptions"
Cohesion: 0.08
Nodes (27): Add a new language/culture, Add search to a new entity _(Build checklist)_, Change the vector (weights, fields, or config), Extending and maintaining, File map _(Build)_, Full-Text Search, Gotchas, How it works (+19 more)

### Community 72 - "AuditableEntityResponse"
Cohesion: 0.12
Nodes (15): AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, DateTimeOffset, CancellationToken (+7 more)

### Community 73 - ".WriteAsync"
Cohesion: 0.33
Nodes (5): Memory, ReadOnlyMemory, CancellationToken, Task, ValueTask

### Community 75 - "Setup"
Cohesion: 0.33
Nodes (4): ApiVersionSet, Setup, IEndpointRouteBuilder, IServiceCollection

### Community 76 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 77 - "AuditLogRetentionService"
Cohesion: 0.33
Nodes (7): AuditLogRetentionService, CancellationToken, ILogger, IOptions, LoggerMessage, NpgsqlDataSource, Task

### Community 78 - "CheckRegistrationRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, CheckRegistrationRateLimitingPolicy (+1 more)

### Community 79 - "IAggregateRoot"
Cohesion: 0.29
Nodes (5): IAggregateRoot, Events, Id, Version, IReadOnlyCollection

### Community 80 - "IAuditableEntity"
Cohesion: 0.17
Nodes (10): IAuditableEntity, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, DateTimeOffset, CancellationToken, DbContextEventData (+2 more)

### Community 81 - ".SearchStoresAsync"
Cohesion: 0.20
Nodes (7): LikePattern, CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint

### Community 82 - "Split-Deployment PoC"
Cohesion: 0.18
Nodes (9): Concurrent safety, Cross-process call path, Files added by this PoC, How it works, How to run, Split-Deployment PoC, What this proves, README (Boilerplate Overview) (+1 more)

### Community 83 - "ICaptchaService"
Cohesion: 0.15
Nodes (10): ICaptchaService, CancellationToken, IFusionCache, IOptions, Task, CachedCaptchaService, DummyCaptchaService, IConfiguration (+2 more)

### Community 84 - ".Configure"
Cohesion: 0.15
Nodes (14): AuditableEntityConfiguration, EntityTypeBuilder, JobRowConfiguration, EntityTypeBuilder, StronglyTypedIdValueConverter, DefaultIdType, EntityTypeBuilder, NpgsqlTsVector (+6 more)

### Community 85 - ".CreateMyStoreAsync"
Cohesion: 0.14
Nodes (10): Products.Endpoints.Stores.v1.My.Create, Store, CancellationToken, Task, CancellationToken, RouteGroupBuilder, Task, Endpoint (+2 more)

### Community 86 - "Store"
Cohesion: 0.06
Nodes (31): AggregateRoot, Events, Id, Version, IReadOnlyCollection, List, DateTimeOffset, DefaultIdType (+23 more)

### Community 87 - "PushOptions"
Cohesion: 0.12
Nodes (20): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri, PushOptions (+12 more)

### Community 89 - "Setup"
Cohesion: 0.33
Nodes (4): ConfigurationManager, Host.Configurations, Setup, WebApplicationBuilder

### Community 90 - "IInterModuleRequestClient"
Cohesion: 0.15
Nodes (14): IInterModuleRequestClient, GetStockLevelRequest, GetStockLevelResponse, DefaultIdType, CancellationToken, Task, GetStockLevelRequestHandler, RouteGroupBuilder (+6 more)

### Community 91 - ".SeedProductAsync"
Cohesion: 0.33
Nodes (5): CancellationToken, List, ProductTemplateId, StoreId, Task

### Community 92 - ".AddProductToMyStoreAsync"
Cohesion: 0.11
Nodes (17): Products.Endpoints.Stores.v1.My.AddProduct, CancellationToken, ProductTemplate, RouteGroupBuilder, Store, Task, Endpoint, ProductTemplateId (+9 more)

### Community 93 - ".MapEndpoint"
Cohesion: 0.22
Nodes (6): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, RouteGroupBuilder, Response, ClientKey, RouteGroupBuilder, Setup

### Community 94 - "KeyedResiliencePipelines"
Cohesion: 0.14
Nodes (13): HttpRequestException, ResiliencePipelineBuilder, ResiliencePipelineRegistry, KeyedResiliencePipelines, HttpResponseMessage, IConnectionMultiplexer, ILoggerFactory, IOptions (+5 more)

### Community 95 - ".SearchMyProductsAsync"
Cohesion: 0.17
Nodes (11): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Description (+3 more)

### Community 96 - "DatabaseSeederOrchestrator"
Cohesion: 0.27
Nodes (9): BackgroundService, DatabaseSeederOrchestrator, CancellationToken, Exception, IHostApplicationLifetime, ILogger, IServiceScopeFactory, LoggerMessage (+1 more)

### Community 97 - "StockReservationExpirySweepService"
Cohesion: 0.20
Nodes (10): EntityEntry, CancellationToken, Exception, Guid, ILogger, IOptions, LoggerMessage, Task (+2 more)

### Community 98 - ".SearchStoreProductsAsync"
Cohesion: 0.14
Nodes (12): Products.Endpoints.Products.v1.Search, CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response (+4 more)

### Community 99 - "AuditLogRetentionJobRegistrar"
Cohesion: 0.20
Nodes (10): IHostedService, AuditLogRetentionJobRegistrar, CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task (+2 more)

### Community 100 - "OutboxDbContext"
Cohesion: 0.14
Nodes (13): DbContext, IOutboxDbContext, OutboxMessages, CancellationToken, DbSet, Task, IConfiguration, IServiceCollection (+5 more)

### Community 101 - "ResiliencyOptions"
Cohesion: 0.15
Nodes (13): ResiliencyOptions, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, Keyed, MaxRetryAttempts (+5 more)

### Community 102 - "HttpContextTargetingContextAccessor"
Cohesion: 0.20
Nodes (8): ITargetingContextAccessor, HttpContextTargetingContextAccessor, IHttpContextAccessor, ValueTask, Setup, IConfiguration, IServiceCollection, TargetingContext

### Community 103 - "InboxCleanupJob"
Cohesion: 0.22
Nodes (10): CancellationToken, IEnumerable, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider (+2 more)

### Community 104 - "CaptchaOptions"
Cohesion: 0.16
Nodes (13): CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey, Provider, ScoreThreshold, SecretKey (+5 more)

### Community 105 - "Response"
Cohesion: 0.18
Nodes (9): IAM.Endpoints.Users.VersionNeutral.SelfRegisterByEmail, RouteGroupBuilder, Endpoint, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken (+1 more)

### Community 106 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.07
Nodes (28): Notifications.Application.Otp, IAM.Endpoints.Users, Common.Application.Caching, Common.Application.FeatureManagement, IAM.Endpoints.Otp, Notifications.Application.Persistence, Notifications.Infrastructure.Telemetry, IAM.Domain.Captcha (+20 more)

### Community 107 - "RabbitMqOptions"
Cohesion: 0.05
Nodes (38): ConsumerDefinition, IClientFactory, IConsumerConfigurator, IRegistrationContext, InterModuleRequestOptions, DependencyUnavailableRetryAfterSeconds, HandlerConcurrentMessageLimit, HandlerPrefetchCount (+30 more)

### Community 108 - "StockLevel"
Cohesion: 0.24
Nodes (7): DefaultIdType, StockLevel, ProductId, QuantityOnHand, StockLevelId, EntityTypeBuilder, StockLevelConfiguration

### Community 109 - ".IsRegisteredAsync"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, IsRegistered

### Community 110 - "KeycloakPermissionPolicyProvider"
Cohesion: 0.24
Nodes (8): AuthorizationOptions, AuthorizationPolicy, DefaultAuthorizationPolicyProvider, IAuthorizationPolicyProvider, ConcurrentDictionary, IOptions, Task, KeycloakPermissionPolicyProvider

### Community 111 - ".ReleaseStockReservationAsync"
Cohesion: 0.09
Nodes (22): ReleaseResponseBody, CancellationToken, Task, IWarehouseGateway, CancellationToken, RouteGroupBuilder, Task, TimeProvider (+14 more)

### Community 112 - "OutboxCleanupJob"
Cohesion: 0.27
Nodes (8): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxCleanupJob

### Community 113 - "SeedingCompletionTracker"
Cohesion: 0.15
Nodes (8): SeedingCompletionTracker, CancellationToken, Exception, Task, Setup, IOptions, IServiceCollection, TaskCompletionSource

### Community 114 - "Response"
Cohesion: 0.11
Nodes (17): IAM.Endpoints.Users.VersionNeutral.Me.Get, RouteGroupBuilder, Endpoint, DateOnly, DateTimeOffset, IReadOnlyCollection, Response, BirthDate (+9 more)

### Community 115 - ".AddServices"
Cohesion: 0.15
Nodes (11): IServerFilter, IConfiguration, ILogger, IServiceCollection, JobMetricsFilter, PerformingContext, SkipOverlappingRecurringJobFilter, ILogger (+3 more)

### Community 116 - ".AddAuthInfrastructure"
Cohesion: 0.12
Nodes (16): IAllowAnonymous, IAuthorizationHandler, IConfigureNamedOptions, HttpContext, HttpStatusCode, IOptions, IProblemDetailsService, IResxLocalizer (+8 more)

### Community 117 - "IInventoryDbContext"
Cohesion: 0.06
Nodes (29): Inventory.Endpoints.StockReservations.v1.Get, DbSet, IInventoryDbContext, StockLevels, StockReservations, ReservationStatus, Active, Committed (+21 more)

### Community 118 - "IModule"
Cohesion: 0.12
Nodes (13): IModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority, Action, IApplicationBuilder (+5 more)

### Community 119 - "ProductsModule"
Cohesion: 0.12
Nodes (13): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, ProductsModule (+5 more)

### Community 120 - "InventoryOptions"
Cohesion: 0.07
Nodes (29): IHeaderDictionary, IValidator, InventoryOptions, MaxOccurrencesPerSeries, SafetyBufferQuantity, StockReservationExpirySweepBatchSize, StockReservationExpirySweepCron, WarehouseGatewayBaseUrl (+21 more)

### Community 121 - ".GetVariantAsync"
Cohesion: 0.40
Nodes (4): IVariantFeatureManager, IVariantFeatureManagerExtensions, CancellationToken, Task

### Community 122 - "OtpVerifyRateLimitingPolicy"
Cohesion: 0.18
Nodes (10): IRateLimiterPolicy, CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask (+2 more)

### Community 123 - "NotificationsHub"
Cohesion: 0.36
Nodes (6): Hub, Exception, ILogger, LoggerMessage, Task, NotificationsHub

### Community 124 - ".AddCustomHealthChecks"
Cohesion: 0.12
Nodes (14): HealthCheckOptions, EnableHealthChecks, ReadinessTimeoutInSeconds, StartupTimeoutInSeconds, HealthCheckOptionsValidator, IApplicationBuilder, IConfiguration, IConnectionMultiplexer (+6 more)

### Community 125 - ".AddObservability"
Cohesion: 0.24
Nodes (7): OpenTelemetryBuilder, ResourceBuilder, Action, IConfiguration, IHostEnvironment, IReadOnlyList, IServiceCollection

### Community 126 - "GlobalExceptionHandlingMiddleware"
Cohesion: 0.27
Nodes (10): Exception, HttpContext, ILogger, IOptions, IProblemDetailsService, IResxLocalizer, LoggerMessage, RequestDelegate (+2 more)

### Community 127 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 128 - "EmailRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, EmailRateLimitingPolicy (+1 more)

### Community 129 - "Seeder"
Cohesion: 0.18
Nodes (12): CancellationToken, ILogger, LoggerMessage, ProductsDbContext, Task, CancellationToken, List, Task (+4 more)

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.48
Nodes (7): Observability (OpenTelemetry), docker-compose.yml (Base Stack), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), Postgres Service (mm.postgres), RabbitMQ Service (mm.rabbitmq), Redis Service (mm.redis)

### Community 131 - ".AddInfrastructure"
Cohesion: 0.29
Nodes (7): HostOptions, JsonOptions, Assembly, IConfiguration, IResxLocalizer, IServiceCollection, IWebHostEnvironment

### Community 132 - "StoreId"
Cohesion: 0.06
Nodes (22): Products.Endpoints.Stores.v1.Create, Products.Endpoints.Stores.v1.Search, StronglyTypedIdHelper, StoreId, DefaultIdType, StoreId, RequestBody, Request (+14 more)

### Community 134 - "SendRequestBody"
Cohesion: 0.25
Nodes (8): SendContact, IReadOnlyList, SendRequestBody, HtmlContent, Sender, Subject, TextContent, To

### Community 135 - "IDatabaseSeeder"
Cohesion: 0.18
Nodes (9): IDatabaseSeeder, Priority, CancellationToken, Task, CancellationToken, IServiceScopeFactory, Task, ProductsDatabaseSeeder (+1 more)

### Community 136 - "ProductTemplates/v1/Search/Request.cs"
Cohesion: 0.20
Nodes (8): Products.Endpoints.ProductTemplates.v1.Search, Constants, Request, Brand, Color, Model, SearchTerm, RequestValidator

### Community 137 - ".WriteTooManyRequestsToResponse"
Cohesion: 0.13
Nodes (14): PartitionedRateLimiter, CancellationToken, Func, HttpContext, IConfiguration, IProblemDetailsService, IReadOnlyList, IResxLocalizer (+6 more)

### Community 138 - ".From"
Cohesion: 0.09
Nodes (26): AspNetResult, IEndpointFilter, IFeatureManagerSnapshot, ResxLocalizer, ProblemResponse, ResultToAcceptedResponseTransformer, ResultToCreatedResponseTransformer, ResultToResponseTransformer (+18 more)

### Community 139 - "ProductTemplate"
Cohesion: 0.15
Nodes (11): IReadOnlyList, List, ProductTemplate, Brand, Color, IsActive, Model, Products (+3 more)

### Community 140 - "ProcessedMessage"
Cohesion: 0.13
Nodes (16): ProcessedMessage, ConsumerName, MessageId, ProcessedOn, DateTimeOffset, DefaultIdType, UniqueConstraintException, InboxStore (+8 more)

### Community 141 - "IStronglyTypedId"
Cohesion: 0.06
Nodes (32): JsonConverter, StrictDateTimeOffsetJsonConverter, DateTimeOffset, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, StronglyTypedIdListReadOnlyJsonConverter (+24 more)

### Community 142 - "TokenRefreshRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenRefreshRateLimitingPolicy (+1 more)

### Community 143 - "Setup"
Cohesion: 0.10
Nodes (18): LoadAll, microsoft_aspnetcore_httpoverrides, ModuleRegistry, Names, Setup, Assembly, Exception, IApplicationBuilder (+10 more)

### Community 144 - "Response"
Cohesion: 0.12
Nodes (16): CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate (+8 more)

### Community 145 - "RequestBody"
Cohesion: 0.29
Nodes (7): ProductTemplateId, RequestBody, Description, Name, Price, ProductTemplateId, Quantity

### Community 146 - ".RegisterAsync"
Cohesion: 0.07
Nodes (31): CancellationToken, Task, BindDeviceSessionRequest, BindDeviceSessionResponse, Guid, CancellationToken, Exception, Guid (+23 more)

### Community 147 - "IRecurringBackgroundJobs"
Cohesion: 0.13
Nodes (13): RecurringJobOptions, IRecurringBackgroundJobs, Action, Expression, Func, Task, RecurringBackgroundJobsService, Action (+5 more)

### Community 148 - ".CreateTokensByEmail"
Cohesion: 0.07
Nodes (28): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, VerifyEmailOtpRequest, VerifyEmailOtpResponse, EmailNormalization, CancellationToken, RouteGroupBuilder, Task (+20 more)

### Community 149 - "Stores/v1/Update/Request.cs"
Cohesion: 0.20
Nodes (10): Products.Endpoints.Stores.v1.Update, RequestBody, Request, Body, Id, RequestBody, Address, Description (+2 more)

### Community 150 - "StockReservationExpirySweepJobRegistrar"
Cohesion: 0.22
Nodes (9): IServiceCollection, Setup, CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task (+1 more)

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
Cohesion: 0.22
Nodes (8): elastic_serilog_sinks, serilog_configuration, serilog_enrichers_span, serilog_events, serilog_exceptions, serilog_formatting_compact, serilog_sinks_opentelemetry, serilog_sinks_systemconsole_themes

### Community 155 - "ISmsGateway"
Cohesion: 0.14
Nodes (13): CancellationToken, Task, ISmsGateway, SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, SmsMessage (+5 more)

### Community 156 - "Request"
Cohesion: 0.20
Nodes (10): Guid, Request, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken, Password (+2 more)

### Community 157 - "IntegrationEvent"
Cohesion: 0.13
Nodes (16): IntegrationEvent, CreatedOn, Id, DateTimeOffset, DefaultIdType, ProductCreatedIntegrationEvent, StoreCreatedIntegrationEvent, DefaultIdType (+8 more)

### Community 158 - "OtpOptions"
Cohesion: 0.18
Nodes (11): OtpOptions, DummyCode, EmailQuotaWindowMinutes, ExpirationInMinutes, Length, MaxSendsPerEmailPerWindow, MaxSendsPerPhonePerWindow, PhoneQuotaWindowMinutes (+3 more)

### Community 159 - "InventoryDbContext"
Cohesion: 0.17
Nodes (11): DbContextOptions, DbSet, ILogger, TimeProvider, InventoryDbContext, StockLevels, StockReservations, IApplicationBuilder (+3 more)

### Community 160 - "OtpServiceBase"
Cohesion: 0.21
Nodes (8): OtpCacheEntry, DateTimeOffset, CancellationToken, IFusionCache, SemaphoreSlim, Task, TimeSpan, OtpServiceBase

### Community 161 - "IntegrationEventOutbox"
Cohesion: 0.10
Nodes (21): IIntegrationEventOutbox, IntegrationEventOutbox, HasPending, IReadOnlyList, List, Lock, Setup, IServiceCollection (+13 more)

### Community 162 - "IEmailGateway"
Cohesion: 0.19
Nodes (9): CancellationToken, Task, EmailMessage, IEmailGateway, CancellationToken, IFusionCache, IOptions, Task (+1 more)

### Community 164 - "NotificationsDbContext"
Cohesion: 0.15
Nodes (13): DbSet, INotificationsDbContext, DeviceRegistrations, DbContextOptions, DbSet, ILogger, TimeProvider, NotificationsDbContext (+5 more)

### Community 165 - "ICurrentUser"
Cohesion: 0.09
Nodes (20): ICurrentUser, Id, IdAsString, Roles, SessionId, ICollection, CurrentUser, Id (+12 more)

### Community 166 - "IamModule"
Cohesion: 0.18
Nodes (10): Action, IApplicationBuilder, IEnumerable, RateLimiterOptions, IamModule, ActivitySourceNames, MeterNames, Name (+2 more)

### Community 167 - "PaginationRequest"
Cohesion: 0.06
Nodes (31): Products.Endpoints.Stores.v1.AuditLog, IAM.Endpoints.Users.VersionNeutral.Search, Products.Endpoints.Stores.v1.My.AuditLog, Products.Endpoints.Products.v1.AuditLog, PaginationRequest, After, IncludeTotal, PageNumber (+23 more)

### Community 168 - ".MapEndpoint"
Cohesion: 0.40
Nodes (3): RouteGroupBuilder, Setup, RouteGroupBuilder

### Community 169 - "SendSecurityAlertRequestHandler"
Cohesion: 0.27
Nodes (9): SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, CancellationToken, IOptions, RequestLocalizationOptions, Task (+1 more)

### Community 171 - "VerifyPhoneOtpResponse"
Cohesion: 0.27
Nodes (9): OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, CancellationToken, Task (+1 more)

### Community 172 - ".MapEndpoint"
Cohesion: 0.40
Nodes (3): RouteGroupBuilder, Setup, RouteGroupBuilder

### Community 173 - "ProductTemplateId"
Cohesion: 0.10
Nodes (15): Products.Endpoints.ProductTemplates.v1.Deactivate, Products.Endpoints.ProductTemplates.v1.Get, Products.Endpoints.ProductTemplates.v1.Activate, ProductTemplateId, DefaultIdType, ProductTemplateId, Request, Id (+7 more)

### Community 174 - "system_diagnostics"
Cohesion: 0.11
Nodes (12): BackgroundJobs.Telemetry, Notifications.Infrastructure.Push.Firebase, firebaseadmin, firebaseadmin_messaging, google_apis_auth_oauth2, hangfire_server, hangfire_storage, LoginMethods (+4 more)

### Community 175 - "PaginationResponse"
Cohesion: 0.09
Nodes (23): JsonElement, AuditLogDto, DateTimeOffset, PaginationResponse, HasNext, HasPrevious, HasTotal, NextPageNumber (+15 more)

### Community 176 - ".FixedWindow"
Cohesion: 0.23
Nodes (10): FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit, RateLimitPartitions, HttpContext, IConnectionMultiplexer (+2 more)

### Community 177 - "CorsOptions"
Cohesion: 0.25
Nodes (8): CorsOptions, AllowCredentials, AllowedHeaders, AllowedMethods, AllowedOrigins, MaxAgeInSeconds, CorsOptionsValidator, IReadOnlyList

### Community 178 - "PushMessage"
Cohesion: 0.13
Nodes (14): IReadOnlyDictionary, CancellationToken, IReadOnlyList, Task, IPushGateway, PushMessage, CancellationToken, ILogger (+6 more)

### Community 179 - "CachingOptions"
Cohesion: 0.11
Nodes (22): CachingEntryDefaults, Duration, FactoryHardTimeout, FactorySoftTimeout, FailSafeMaxDuration, FailSafeThrottleDuration, CachingOptions, AllowInMemoryOnlyInProduction (+14 more)

### Community 180 - "SmsOptions"
Cohesion: 0.10
Nodes (21): SmsOptions, AppName, AttemptTimeoutSeconds, BaseUrl, MaxPerDay, MaxPerPhoneNumberPerDay, MaxRetryAttempts, MsgHeader (+13 more)

### Community 181 - "ProductTemplates/v1/Create/Request.cs"
Cohesion: 0.22
Nodes (8): Products.Endpoints.ProductTemplates.v1.Create, Request, Brand, Color, Model, RequestValidator, Response, Id

### Community 183 - "SendRequestBody"
Cohesion: 0.25
Nodes (8): SendMessageBody, IReadOnlyList, SendRequestBody, AppName, Encoding, IysFilter, Messages, MsgHeader

### Community 184 - "RedisOtpService"
Cohesion: 0.12
Nodes (13): OtpCodeGenerator, IFusionCache, IOptions, OtpService, CancellationToken, IConnectionMultiplexer, IOptions, Task (+5 more)

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

### Community 189 - "JobClaimExtensions"
Cohesion: 0.28
Nodes (9): IdBound, Value, JobClaimExtensions, CancellationToken, DateTimeOffset, DbSet, Expression, Func (+1 more)

### Community 190 - "ServiceAccountTokenCache"
Cohesion: 0.11
Nodes (11): KeycloakPaths, CancellationToken, DateTimeOffset, SemaphoreSlim, Task, TimeSpan, ServiceAccountTokenCache, AccessToken (+3 more)

### Community 191 - "ProjectionUpsertOutcome"
Cohesion: 0.20
Nodes (7): Common.Application.Persistence.Projections, ProjectionEntity, SourceVersion, ProjectionUpsertOutcome, Inserted, Stale, Updated

### Community 192 - ".ApplyConfigurations"
Cohesion: 0.24
Nodes (5): LoggerConfiguration, LoggerMinimumLevelConfiguration, IEnumerable, IHostEnvironment, KeyValuePair

### Community 193 - ".GetMeAsync"
Cohesion: 0.24
Nodes (7): CancellationToken, IReadOnlyList, Task, IKeycloakPermissionClient, CancellationToken, HttpContext, Task

### Community 194 - "EventDispatcher"
Cohesion: 0.31
Nodes (7): EventDispatcher, ActivitySource, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task

### Community 195 - ".ReserveStockAsync"
Cohesion: 0.12
Nodes (12): Inventory.Endpoints.StockReservations.v1.Reserve, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Id, CancellationToken (+4 more)

### Community 196 - "TokenCreateRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenCreateRateLimitingPolicy (+1 more)

### Community 197 - "Request"
Cohesion: 0.20
Nodes (10): StoreId, Request, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name, OwnerId (+2 more)

### Community 198 - "InventoryModule"
Cohesion: 0.15
Nodes (10): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, InventoryModule, ActivitySourceNames, MeterNames (+2 more)

### Community 199 - "ReCaptchaResponse"
Cohesion: 0.25
Nodes (7): DateTime, ReCaptchaResponse, ChallengeTs, ErrorCodes, Hostname, Score, Success

### Community 200 - "NotificationsTelemetry"
Cohesion: 0.20
Nodes (5): ActivitySource, Counter, Meter, NotificationsTelemetry, UpDownCounter

### Community 201 - "KeycloakScopes"
Cohesion: 0.20
Nodes (9): Devices, Hangfire, KeycloakScopes, Products, ProductTemplates, Sessions, StockReservations, Stores (+1 more)

### Community 202 - "Request"
Cohesion: 0.22
Nodes (9): Guid, Request, ClientId, DeviceId, DeviceName, Otp, PhoneNumber, PushToken (+1 more)

### Community 203 - "SmsRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, SmsRateLimitingPolicy (+1 more)

### Community 205 - "StatelessInboxStore"
Cohesion: 0.24
Nodes (6): StatelessInboxStore, Instance, CancellationToken, DateTimeOffset, DefaultIdType, Task

### Community 210 - "BackgroundJobsOptions"
Cohesion: 0.29
Nodes (7): BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount, BackgroundJobsOptionsValidator

### Community 211 - "Key decisions"
Cohesion: 0.29
Nodes (7): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Key decisions

### Community 212 - "Stores/v1/My/Update/Request.cs"
Cohesion: 0.33
Nodes (6): Products.Endpoints.Stores.v1.My.Update, Request, Address, Description, Name, RequestValidator

### Community 213 - "BackgroundJobsTelemetry"
Cohesion: 0.20
Nodes (8): PerformedContext, BackgroundJobsTelemetry, ActivitySource, ConcurrentDictionary, Counter, Histogram, Meter, ObservableGauge

### Community 214 - "fluentvalidation"
Cohesion: 0.06
Nodes (44): common_application_localization_resources, IAM.Domain.Users, IAM.Endpoints.Otp.VersionNeutral.SendForLogin, Products.Endpoints.Probe.v1, Common.Domain.Extensions, Notifications.Infrastructure.Devices.UpdateCurrentPushToken, Common.Application.Validation, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke (+36 more)

### Community 215 - "DeviceRegistrationId"
Cohesion: 0.29
Nodes (4): DefaultIdType, DeviceRegistrationId, EntityTypeBuilder, DeviceRegistrationConfiguration

### Community 216 - ".UpsertIfNewerAsync"
Cohesion: 0.20
Nodes (9): ICurrentDbContext, ProjectionUpsertExtensions, Action, CancellationToken, DbContext, DbSet, Func, Task (+1 more)

### Community 217 - "FeatureFlags"
Cohesion: 0.33
Nodes (5): Checkout, FeatureFlags, IAM, Notifications, Products

### Community 218 - ".SendOtp"
Cohesion: 0.10
Nodes (20): SendPhoneOtpRequest, SendPhoneOtpResponse, CancellationToken, Task, CancellationToken, IFeatureManager, RouteGroupBuilder, Task (+12 more)

### Community 219 - "IBackgroundJobs"
Cohesion: 0.23
Nodes (7): IBackgroundJobs, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 220 - ".AddBrevo"
Cohesion: 0.33
Nodes (5): IConfiguration, IFusionCache, IOptions, IServiceCollection, Setup

### Community 221 - "Keycloak realm as code"
Cohesion: 0.25
Nodes (7): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Secrets, Seed users (dev / test only)

### Community 222 - ".ListSessions"
Cohesion: 0.10
Nodes (22): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, IReadOnlyList, CancellationToken, IReadOnlyCollection, RouteGroupBuilder (+14 more)

### Community 223 - "KeycloakPermission"
Cohesion: 0.29
Nodes (3): KeycloakPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder

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
Cohesion: 0.29
Nodes (6): IMemoryCache, Setup, IConfiguration, IConnectionMultiplexer, IServiceCollection, JsonSerializerOptions

### Community 229 - ".AddCommonOptions"
Cohesion: 0.40
Nodes (4): Setup, IConfiguration, IHostEnvironment, IServiceCollection

### Community 230 - "InterModuleRequestHandler"
Cohesion: 0.10
Nodes (22): IInterModuleRequest, IInterModuleRequestHandler, CancellationToken, Task, InterModuleRequestHandler, CancellationToken, ConsumeContext, Task (+14 more)

### Community 231 - ".UseModule"
Cohesion: 0.22
Nodes (7): DashboardContext, IAuthorizationService, IDashboardAsyncAuthorizationFilter, IApplicationBuilder, IOptions, HangfireCustomAuthorizationFilter, Task

### Community 232 - "DomainEventConverter"
Cohesion: 0.10
Nodes (15): IEntityTypeConfiguration, DateTimeOffset, ModelConfigurationBuilder, DomainEventConverter, JsonSerializerOptions, IntegrationEventConverter, JsonSerializerOptions, UtcDateTimeOffsetConverter (+7 more)

### Community 233 - ".HasPatternIndex"
Cohesion: 0.36
Nodes (6): IndexBuilder, TrigramIndexExtensions, EntityTypeBuilder, Expression, Func, ModelBuilder

### Community 234 - "KeycloakPermissionAuthorizationHandler.cs"
Cohesion: 0.13
Nodes (11): Common.Infrastructure.Auth.Services, IAM.Infrastructure.Auth, Notifications.Infrastructure.Hubs, Common.Infrastructure.Auth, hangfire_annotations, hangfire_dashboard, microsoft_aspnetcore_authentication_jwtbearer, microsoft_aspnetcore_authorization (+3 more)

### Community 235 - "BaseDbContext"
Cohesion: 0.08
Nodes (21): AuditLogEntry, AggregateId, AggregateType, Event, EventType, Version, DefaultIdType, BaseDbContext (+13 more)

### Community 236 - "V1StockLevelCreatedDomainEvent"
Cohesion: 0.40
Nodes (4): Inventory.Domain.StockLevels.DomainEvents.v1, DefaultIdType, StockLevelId, V1StockLevelCreatedDomainEvent

### Community 237 - "ProblemDetailsExtensions.cs"
Cohesion: 0.40
Nodes (3): ProblemDetails, ProblemDetailsExtensions, ICollection

### Community 238 - ".UseInfrastructure"
Cohesion: 0.18
Nodes (9): IAuthenticationSchemeProvider, RequestBodyLimitMiddleware, RequestDelegate, IApplicationBuilder, HttpContext, IOptions, RequestDelegate, Task (+1 more)

### Community 240 - "RemoveDefaultResponseSchemaFilter"
Cohesion: 0.40
Nodes (4): IOperationFilter, OpenApiOperation, OperationFilterContext, RemoveDefaultResponseSchemaFilter

### Community 243 - "CustomValidator"
Cohesion: 0.05
Nodes (46): Inventory.Endpoints.StockReservations.v1.Commit, CustomRateLimitingOptionsValidator, FixedWindowValidator, DatabaseOptions, ConnectionString, DatabaseOptionsValidator, ModulesOptions, EnabledModules (+38 more)

### Community 244 - "KeyedResilienceProfile"
Cohesion: 0.15
Nodes (13): AbstractValidator, KeyedResilienceProfile, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, RateLimitFailOpen (+5 more)

### Community 246 - "RequestBodyLimitMetadata"
Cohesion: 0.33
Nodes (6): RequestBodyLimitMetadata, MaxBodyBytes, Func, HttpContext, IHttpMaxRequestBodySizeFeature, Task

### Community 247 - "OpenApiOptions"
Cohesion: 0.22
Nodes (9): OpenApiOptions, ContactEmail, ContactName, Description, EnableSwagger, LicenseName, LicenseUrl, Title (+1 more)

### Community 248 - "SendErrorBody"
Cohesion: 0.67
Nodes (3): SendErrorBody, Code, Message

### Community 249 - "DeviceRegistryReconciliationService"
Cohesion: 0.08
Nodes (23): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator, IReadOnlyCollection, CancellationToken, ILogger (+15 more)

### Community 250 - "Response"
Cohesion: 0.20
Nodes (9): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Description, Name, Price (+1 more)

### Community 251 - "ProductId"
Cohesion: 0.07
Nodes (27): Products.Endpoints.Products.v1.My.Update, Products.Endpoints.Products.v1.Update, Products.Endpoints.Products.v1.Get, Products.Endpoints.Stores.v1.AddProduct, DefaultIdType, ProductId, Request, Id (+19 more)

### Community 252 - ".TryWriteAsync"
Cohesion: 0.40
Nodes (4): ProblemDetailsContext, ProblemDetailsServiceExtensions, IProblemDetailsService, Task

### Community 261 - "IDbContext"
Cohesion: 0.18
Nodes (11): DatabaseFacade, IDbContext, AuditLog, ChangeTracker, Database, DbSet, DbContextExtensions, CancellationToken (+3 more)

### Community 262 - ".SendWithPipelineAsync"
Cohesion: 0.25
Nodes (7): HttpClientKeyedExtensions, CancellationToken, HttpClient, HttpRequestMessage, HttpResponseMessage, ResiliencePipeline, Task

### Community 263 - "Response"
Cohesion: 0.25
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegister, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 264 - "ResxLocalizationOptions"
Cohesion: 0.25
Nodes (7): ResxLocalizationOptions, DefaultCulture, SupportedCultures, ResxLocalizationOptionsValidator, ICollection, IApplicationBuilder, IOptions

### Community 265 - "BackgroundJobsModule"
Cohesion: 0.25
Nodes (7): BackgroundJobsModule, ActivitySourceNames, MeterNames, Name, StartupPriority, IEndpointRouteBuilder, IEnumerable

### Community 266 - "SwaggerDefaultValues"
Cohesion: 0.40
Nodes (4): JsonValue, OpenApiOperation, OperationFilterContext, SwaggerDefaultValues

### Community 267 - "AuditLogOptions"
Cohesion: 0.29
Nodes (7): AuditLogOptions, PerSchemaRetentionDays, PurgeBatchSize, RetentionCron, RetentionDays, AuditLogOptionsValidator, Dictionary

### Community 269 - "IProductsDbContext"
Cohesion: 0.03
Nodes (52): CancellationToken, Task, DbSet, ProductTemplate, IProductsDbContext, Products, ProductTemplates, Stores (+44 more)

### Community 270 - "NetGsmSmsGateway"
Cohesion: 0.18
Nodes (11): SendResponseBody, CancellationToken, HttpClient, IOptions, JsonSerializerOptions, SendRequestBody, Task, NetGsmSmsGateway (+3 more)

### Community 272 - ".AddResilientHttpClient"
Cohesion: 0.22
Nodes (8): HttpStandardResilienceOptions, IHttpClientBuilder, Setup, Action, HttpClient, IOptions, IServiceCollection, IServiceProvider

### Community 273 - ".AddNetGsm"
Cohesion: 0.33
Nodes (5): IConfiguration, IFusionCache, IOptions, IServiceCollection, Setup

### Community 274 - "Infrastructure/StringExtensions.cs"
Cohesion: 0.40
Nodes (3): opentelemetry_exporter, OtlpExportProtocol, StringExtensions

### Community 276 - "EnrichLogsWithUserInfoMiddleware"
Cohesion: 0.25
Nodes (6): IMiddleware, serilog_context, HttpContext, RequestDelegate, Task, EnrichLogsWithUserInfoMiddleware

### Community 277 - "SendResponseBody"
Cohesion: 0.50
Nodes (4): SendResponseBody, Code, Description, JobId

### Community 278 - ".TapWhenFeatureEnabledAsync"
Cohesion: 0.33
Nodes (5): FeatureFlagResultExtensions, Action, Func, IFeatureManager, Task

### Community 279 - "Response"
Cohesion: 0.25
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.Create, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 281 - ".SetRetryAfterHeader"
Cohesion: 0.07
Nodes (18): IAM.Endpoints, Products.Endpoints, Notifications.Infrastructure, HttpContent, RetryAfterHeaderExtensions, HttpResponse, RateLimitLease, TimeSpan (+10 more)

### Community 283 - "AuditableEntity"
Cohesion: 0.25
Nodes (8): AuditableEntity, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, Version, DateTimeOffset

### Community 284 - "RequestBody"
Cohesion: 0.33
Nodes (6): DateTimeOffset, DefaultIdType, RequestBody, ProductId, Quantity, ReservationDeadline

### Community 285 - "RequestBodyLimitFilter.cs"
Cohesion: 0.18
Nodes (10): Common.Endpoints.Webhooks, microsoft_aspnetcore_http_features, RequestBodyLimitEndpointExtensions, RequestBodyLimitFilter, EndpointFilterDelegate, EndpointFilterInvocationContext, Func, HttpContext (+2 more)

### Community 287 - "IOtpService"
Cohesion: 0.15
Nodes (14): IssueVerificationTokenRequest, IssueVerificationTokenResponse, CancellationToken, Task, TimeSpan, IOtpService, OtpVerificationOutcome, InvalidOtp (+6 more)

### Community 288 - "DummySmsGateway"
Cohesion: 0.38
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, DummySmsGateway

### Community 296 - "StronglyTypedIdBinder"
Cohesion: 0.40
Nodes (4): IModelBinder, ModelBindingContext, StronglyTypedIdBinder, Task

### Community 297 - ".SetConcurrency"
Cohesion: 0.50
Nodes (3): IBusFactoryConfigurator, ConcurrencyConfiguratorExtensions, IReceiveEndpointConfigurator

### Community 298 - "Setup"
Cohesion: 0.33
Nodes (4): IApplicationBuilder, IWebHostEnvironment, Type, Setup

### Community 299 - ".MapEndpoint"
Cohesion: 0.40
Nodes (3): RouteGroupBuilder, RouteGroupBuilder, Setup

### Community 300 - "ResultTelemetryExtensions"
Cohesion: 0.38
Nodes (4): Activity, ResultTelemetryExtensions, ActivitySource, Task

### Community 302 - ".EmailVerificationTokenValidation"
Cohesion: 0.50
Nodes (3): IResxLocalizer, IRuleBuilder, IRuleBuilderOptions

### Community 303 - "DefaultResponsesOperationFilter"
Cohesion: 0.70
Nodes (3): OpenApiOperation, OperationFilterContext, DefaultResponsesOperationFilter

### Community 304 - "JobStatus"
Cohesion: 0.33
Nodes (5): JobStatus, Failed, Queued, Running, Succeeded

### Community 305 - ".AddCustomSwagger"
Cohesion: 0.22
Nodes (7): IOpenApiSchema, ISchemaFilter, SchemaFilterContext, IConfigureOptions, IServiceCollection, SwaggerGenOptions, StronglyTypedIdSchemaFilter

### Community 306 - ".PhoneNumberValidation"
Cohesion: 0.50
Nodes (3): IResxLocalizer, IRuleBuilder, IRuleBuilderOptions

### Community 307 - "SignalROptions"
Cohesion: 0.40
Nodes (5): SignalROptions, RedisConnectionString, RequireRedisBackplaneInProduction, UseRedisBackplane, SignalROptionsValidator

### Community 308 - "PublishOutcome"
Cohesion: 0.50
Nodes (4): PublishOutcome, Failed, Published, Skipped

### Community 311 - "microsoft_aspnetcore_mvc"
Cohesion: 0.07
Nodes (29): Products.Endpoints.Stores.v1.Deactivate, Products.Endpoints.Stores.v1.My.RemoveProduct, IAM.Endpoints.Users.VersionNeutral.Get, Inventory.Endpoints.StockReservations.v1.Release, Common.Application.ModelBinders, Products.Endpoints.Stores.v1.RemoveProduct, microsoft_aspnetcore_mvc, microsoft_aspnetcore_mvc_modelbinding (+21 more)

### Community 312 - "ProductsTelemetry"
Cohesion: 0.50
Nodes (4): ActivitySource, Counter, Meter, ProductsTelemetry

### Community 313 - "Common.InterModuleRequests/Setup.cs"
Cohesion: 0.29
Nodes (4): Common.InterModuleRequests, IAssemblyReference, Setup, IServiceCollection

### Community 319 - "RequestBody"
Cohesion: 0.40
Nodes (5): RequestBody, Description, Name, Price, Quantity

### Community 324 - "RequestBody"
Cohesion: 0.40
Nodes (5): RequestBody, Description, Name, Price, Quantity

## Knowledge Gaps
- **953 isolated node(s):** `Id`, `IdAsString`, `Roles`, `SessionId`, `JwtClaimNames` (+948 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 2232 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **96 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `ResxLocalizationOptions`, `AuditLogOptions`, `Common.Domain.ResultMonad`, `Setup`, `EmailOptions`, `OutboxOptions`, `microsoft_extensions_options`, `Setup.Logger.cs`, `Common.Domain.StronglyTypedIds`, `OtpOptions`, `ObservabilityOptions`, `ThrottledEmailGateway.cs`, `KeycloakOptions`, `system_diagnostics`, `microsoft_entityframeworkcore`, `CorsOptions`, `CachingOptions`, `SignalROptions`, `SmsOptions`, `IdentityScheme`, `JobHousekeepingJob`, `RequestLoggingOptions`, `FullTextSearchOptions`, `BackgroundJobsOptions`, `fluentvalidation`, `PushOptions`, `ResiliencyOptions`, `CaptchaOptions`, `Common.InterModuleRequests.Contracts`, `RabbitMqOptions`, `KeycloakPermissionAuthorizationHandler.cs`, `CustomValidator`, `OpenApiOptions`, `InventoryOptions`, `DeviceRegistryReconciliationService`, `.AddCustomHealthChecks`?**
  _High betweenness centrality (0.177) - this node is a cross-community bridge._
- **Why does `Result` connect `Result` to `.ReserveSeriesAsync`, `IDbContext`, `FirebasePushGateway`, `Error`, `ProductTemplate`, `ApplicationUserId`, `IProductsDbContext`, `NetGsmSmsGateway`, `Response`, `.RegisterAsync`, `.CreateTokensByEmail`, `.TapWhenFeatureEnabledAsync`, `DummyEmailGateway`, `.RevokeToken`, `ISmsGateway`, `.SingleAsResult`, `.RefreshToken`, `IEmailGateway`, `DummySmsGateway`, `ResultTelemetryExtensions`, `PaginationQueryableExtensions`, `PaginationResponse`, `Response`, `PushMessage`, `ReCaptchaService`, `.SendOtp`, `BrevoEmailGateway`, `.SearchProductTemplatesAsync`, `KeycloakTokenClient`, `.SendAsync`, `.GetMeAsync`, `.ReserveStockAsync`, `AuditableEntityResponse`, `Response`, `.SearchStoresAsync`, `ICaptchaService`, `.CreateMyStoreAsync`, `Store`, `.SendOtp`, `IInterModuleRequestClient`, `.AddProductToMyStoreAsync`, `.ListSessions`, `.SearchMyProductsAsync`, `.MapCode`, `.SearchStoreProductsAsync`, `.IsRegisteredAsync`, `.ReleaseStockReservationAsync`, `IInventoryDbContext`, `InventoryOptions`, `Response`, `Response`?**
  _High betweenness centrality (0.137) - this node is a cross-community bridge._
- **Why does `ApplicationUserId` connect `ApplicationUserId` to `NotificationPayload`, `Seeder`, `StoreId`, `KeycloakAdminClient`, `IStronglyTypedId`, `KeycloakPermissionAuthorizationHandler`, `Response`, `.RegisterAsync`, `DeviceRegistration`, `.RevokeToken`, `AuditableEntity`, `Common.Domain.StronglyTypedIds`, `IntegrationEvent`, `JobRow`, `IntegrationEventOutbox`, `For`, `ICurrentUser`, `SendSecurityAlertRequestHandler`, `PaginationResponse`, `Response`, `microsoft_aspnetcore_mvc`, `KeycloakTokenClient`, `.HandleAsync`, `Request`, `AuditableEntityResponse`, `Response`, `IAuditableEntity`, `.Configure`, `.CreateMyStoreAsync`, `Store`, `DeviceRegistrationId`, `.ListSessions`, `InterModuleRequestHandler`, `Response`, `Response`?**
  _High betweenness centrality (0.062) - this node is a cross-community bridge._
- **What connects `Id`, `IdAsString`, `Roles` to the rest of the system?**
  _953 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `.ReserveSeriesAsync` be split into smaller, more focused modules?**
  _Cohesion score 0.09686609686609686 - nodes in this community are weakly interconnected._
- **Should `Common.Application.DTOs` be split into smaller, more focused modules?**
  _Cohesion score 0.09486166007905138 - nodes in this community are weakly interconnected._
- **Should `KeycloakAdminClient` be split into smaller, more focused modules?**
  _Cohesion score 0.13131313131313133 - nodes in this community are weakly interconnected._