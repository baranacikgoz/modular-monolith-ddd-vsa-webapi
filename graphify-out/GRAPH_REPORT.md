# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-09-25)

## Corpus Check
- 574 files · ~90,261 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 4928 nodes · 9737 edges · 394 communities (294 shown, 100 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 260 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `a0bda6f4`
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
- Common.Domain.StronglyTypedIds
- LogProductCatalogChangeHandler
- BoundedRequestCaptureStream
- DeviceRegistration
- EmailMessage
- ICurrentUser
- Common.Application.Options
- IEvent
- RequestResponseBodyLoggingMiddleware
- .AssignBasicRoleOrRollbackAsync
- Result
- JobRow
- NotificationsModule
- .SingleAsResult
- .RefreshToken
- KeycloakPermissionClient
- OutboxMessage
- CustomRateLimitingOptions
- AggregateRoot
- RequestBody
- ObservabilityOptions
- ConfigureSwaggerOptions.cs
- Request
- .SaveWithOutboxAsync
- CreateStoreRateLimitingPolicy
- KeycloakOptions
- .CreateTokens
- PaginationQueryableExtensions
- Outbox Misuse Check
- .SetConcurrency
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
- .RequestTokensAsync
- IDbContext
- RequestLoggingOptions
- .SendCoreAsync
- BackgroundJobsService
- OutboxModule
- .AddNotificationsSignalR
- ValueObject
- .HandleAsync
- IntegrationEventHandlerBase
- FullTextSearchOptions
- Response
- .WriteAsync
- AsNoTracking Coverage Check
- Inventory.Domain.StockReservations
- Response
- .RemoveMyProductAsync
- CheckRegistrationRateLimitingPolicy
- DomainEvent
- IAuditableEntity
- .SearchStoresAsync
- Split-Deployment PoC
- microsoft_entityframeworkcore
- .Configure
- CaptchaOptions
- Store
- PushOptions
- Configuration-Driven Module Registration
- Program.cs
- .GetProductAsync
- .SeedProductAsync
- Infrastructure/Setup.cs
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
- AuditableEntityResponse
- Response
- Common.InterModuleRequests.Contracts
- HttpWarehouseGateway
- StockLevel
- .IsRegisteredAsync
- KeycloakPermissionPolicyProvider
- .ReleaseStockReservationAsync
- OutboxCleanupJob
- .AddPersistence
- Response
- AuditLogRetentionService
- .AddAuthInfrastructure
- Response
- IModule
- ProductsModule
- InventoryOptions
- .GetVariantAsync
- OtpVerifyRateLimitingPolicy
- NotificationsHub
- .AddCustomHealthChecks
- .TapWhenFeatureEnabledAsync
- GlobalExceptionHandlingMiddleware
- Response
- EmailRateLimitingPolicy
- KeycloakTokenClient
- docker-compose.yml (Base Stack)
- .AddInfrastructure
- StoreId
- Consumer Idempotency (IntegrationEventHandlerBase)
- SendRequestBody
- .ListSessions
- .UseModules
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
- IRecurringBackgroundJobs
- .RegisterAsync
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
- IEmailGateway
- For
- NotificationsDbContext
- CurrentUser
- IamModule
- PaginationRequest
- .CreateProductTemplateAsync
- SendSecurityAlertRequestHandler
- Notifications.Application/IAssemblyReference.cs
- IntegrationEvent
- Response
- ProductTemplateId
- system_diagnostics
- PaginationResponse
- .FixedWindow
- V1StoreCreatedDomainEvent
- PushMessage
- CachingOptions
- SmsOptions
- Response
- IAM.Domain
- SendRequestBody
- RedisOtpService
- IdentityScheme
- Policies
- ReverseProxyOptions
- BoundedCaptureStream
- JobClaimExtensions
- ServiceAccountTokenCache
- ProjectionUpsertOutcome
- ReCaptchaResponse
- .UpdateMyStoreAsync
- Seeder
- .ReserveStockAsync
- TokenCreateRateLimitingPolicy
- StringExtensions
- InventoryModule
- .AddKeycloakInfrastructure
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
- ICaptchaService
- SeedingCompletionTracker
- Setup
- BackgroundJobsTelemetry
- fluentvalidation
- IProductsDbContext
- .UpsertIfNewerAsync
- FeatureFlags
- .SendOtp
- IBackgroundJobs
- StrictDateTimeOffsetJsonConverter
- Keycloak realm as code
- .UpdateCurrentPushToken
- FirebasePushGateway.cs
- .MapEndpoint
- IInboxStore
- NetGsmSmsGateway
- RegisterRateLimitingPolicy
- Request
- .AddCommonOptions
- InterModuleRequestHandler
- HangfireCustomAuthorizationFilter
- BaseDbContext
- .HasPatternIndex
- JwtBearerConfigureOptions.cs
- AuditLogEntry
- IOtpService
- microsoft_aspnetcore_mvc
- .UseInfrastructure
- microsoft_extensions_configuration
- RabbitMqOptions
- Request
- V1StockReservationCommitConflictDetectedDomainEvent
- CustomValidator
- KeyedResilienceProfile
- SendForEmail/Request.cs
- UtcDateTimeOffsetConverter
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
- RequestBodyLimitMetadata
- CorsOptions
- .SendWithPipelineAsync
- Response
- ResxLocalizationOptions
- .AddServices
- .AddCustomSwagger
- AuditLogOptions
- .DeactivateProductTemplateAsync
- .SaveChangesAsync
- .SendAsync
- ProductTemplates/v1/Create/Request.cs
- .AddResilientHttpClient
- IntegrationEventConverter
- DeviceRegistryReconcileJobRegistrar
- StronglyTypedIdListReadOnlyJsonConverter
- StronglyTypedIdWriteOnlyJsonConverter
- SendResponseBody
- BackgroundJobsOptions
- Response
- .Apply
- .TryReadFromJsonAsync
- Key decisions
- AuditableEntity
- RequestBody
- RequestBodyLimitFilter.cs
- DevicesOptions
- .CommitStockReservationAsync
- DummySmsGateway
- .SetRetryAfterHeader
- IInterModuleRequest
- .AddNetGsm
- .AddServices
- .MapEndpoints
- KeycloakPermissionRequirement
- EnrichLogsWithUserInfoMiddleware
- .HandleRequirementAsync
- VersionNeutral/Get/Request.cs
- Setup
- .MapEndpoint
- ResultTelemetryExtensions
- JobHousekeepingOptions
- ProblemDetailsExtensions.cs
- DefaultResponsesOperationFilter
- JobStatus
- .EmailVerificationTokenValidation
- Products/v1/Get/Request.cs
- SignalROptions
- PublishOutcome
- .Capture
- .WriteProblemAsync
- StronglyTypedIdBinder
- .DeactivateStoreAsync
- .RemoveProductAsync
- ModulesOptions.cs
- Setup
- Setup
- IAutoMigrateMarker.cs
- SecurityHeadersOptions.cs
- Products/v1/Update/Request.cs
- Setup
- system_runtime_compilerservices
- VerifyEmail/Request.cs
- .PhoneNumberValidation
- RequestBody
- InventoryTelemetry
- ValidationContextExtensions
- ProductsTelemetry
- IAM.Endpoints
- Notifications.Infrastructure
- .UseGlobalExceptionHandlingMiddleware
- .AddGlobalExceptionHandlingMiddleware
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
- `Add search to a new entity _(Build checklist)_` --references--> `ISearchLocalized`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Domain/Entities/ISearchLocalized.cs
- `Add search to a new entity _(Build checklist)_` --references--> `ApplySearchLanguageInterceptor`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Infrastructure/Persistence/Auditing/ApplySearchLanguageInterceptor.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Local Infrastructure Stack** — docker_compose_postgres, docker_compose_rabbitmq, docker_compose_redis, docker_compose_aspire_dashboard [EXTRACTED 1.00]

## Communities (394 total, 100 thin omitted)

### Community 0 - ".ReserveSeriesAsync"
Cohesion: 0.11
Nodes (18): Inventory.Endpoints.StockReservations.v1.ReserveSeries, GetProductRequest, GetProductResponse, DefaultIdType, CancellationToken, IOptions, List, RequestBody (+10 more)

### Community 1 - "NotificationPayload"
Cohesion: 0.19
Nodes (12): IHubContext, CancellationToken, IReadOnlyList, Task, INotificationDispatcher, Task, INotificationsClient, NotificationPayload (+4 more)

### Community 2 - "OutboxProcessor"
Cohesion: 0.21
Nodes (12): IPublishEndpoint, PublishOutcome, CancellationToken, Exception, ILogger, IOptions, IServiceScopeFactory, LoggerMessage (+4 more)

### Community 5 - "KeycloakAdminClient"
Cohesion: 0.14
Nodes (22): DateOnly, DateTimeOffset, CreateKeycloakUser, KeycloakUser, KeycloakUserPage, KeycloakUserSession, CancellationToken, Error (+14 more)

### Community 6 - "RedisFixedWindowRateLimiter"
Cohesion: 0.13
Nodes (16): RateLimiter, RateLimiterStatistics, FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration, CancellationToken (+8 more)

### Community 7 - "ApplySearchLanguageInterceptor"
Cohesion: 0.23
Nodes (8): Common.Infrastructure.Persistence.Auditing, microsoft_entityframeworkcore_diagnostics, SaveChangesInterceptor, ApplyAuditingInterceptor, TimeProvider, ApplySearchLanguageInterceptor, Setup, IServiceCollection

### Community 8 - "FirebasePushGateway"
Cohesion: 0.17
Nodes (12): FirebaseApp, FirebaseMessaging, IDisposable, CancellationToken, Exception, IEnumerable, ILogger, IReadOnlyList (+4 more)

### Community 9 - ".RequireOtpTemplateForDefaultCulture"
Cohesion: 0.40
Nodes (4): Func, IConfiguration, IEnumerable, OtpTemplateConfiguration

### Community 10 - "Error"
Cohesion: 0.06
Nodes (27): Inventory.Domain.StockReservations.Errors, StringLocalizerExtensions, IStringLocalizer, Error, Key, ParameterName, StatusCode, SubErrors (+19 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.09
Nodes (31): IAM.Endpoints.Captcha.VersionNeutral, Common.Application.Search, Products.Endpoints.Stores, Common.Application.AuditLog, Common.Application.FeatureManagement, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Products.Endpoints.ProductTemplates (+23 more)

### Community 12 - "ApplicationUserId"
Cohesion: 0.14
Nodes (12): ApplicationUserId, IsEmpty, Value, DefaultIdType, CancellationToken, IReadOnlyList, Task, IKeycloakAdminClient (+4 more)

### Community 13 - "ISearchLanguageResolver"
Cohesion: 0.10
Nodes (17): Configuration, File map _(Build)_, ISearchLanguageResolver, UniversalConfig, SearchLanguageResolver, UniversalConfig, IOptions, ISearchLocalized (+9 more)

### Community 14 - "UserRepresentation"
Cohesion: 0.07
Nodes (29): Dictionary, List, CredentialRepresentation, Temporary, Type, Value, ErrorRepresentation, Error (+21 more)

### Community 15 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.21
Nodes (10): AuthorizationHandler, CancellationToken, ClaimsPrincipal, IFusionCache, IHttpContextAccessor, IOptions, List, TimeProvider (+2 more)

### Community 16 - "EmailOptions"
Cohesion: 0.09
Nodes (24): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+16 more)

### Community 17 - "Product"
Cohesion: 0.06
Nodes (30): Products.Endpoints.Stores.v1.My.AddProduct, ProductTemplate, ProductTemplateId, Store, StoreId, Product, Description, Language (+22 more)

### Community 18 - "StockReservation"
Cohesion: 0.10
Nodes (19): DateTimeOffset, StockReservationId, V1StockReservationReleaseAttemptAbandonedDomainEvent, DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationReleasedDomainEvent, DateTimeOffset (+11 more)

### Community 19 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.06
Nodes (26): Inventory.Domain.StockReservations.DomainEvents.v1, Common.Domain.StronglyTypedIds, Products.Endpoints.Stores.v1.Search, Common.Domain.Events, Products.Endpoints.Products.v1.My.Get, Products.Domain.Products, Common.Application.DTOs, Common.Domain.Entities (+18 more)

### Community 20 - "LogProductCatalogChangeHandler"
Cohesion: 0.24
Nodes (8): CancellationToken, DefaultIdType, IFusionCache, ILogger, IOptions, LoggerMessage, Task, LogProductCatalogChangeHandler

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.18
Nodes (7): BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position, Stream

### Community 22 - "DeviceRegistration"
Cohesion: 0.11
Nodes (16): DateTimeOffset, DefaultIdType, Guid, DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive (+8 more)

### Community 23 - "EmailMessage"
Cohesion: 0.32
Nodes (6): EmailMessage, CancellationToken, ILogger, LoggerMessage, Task, DummyEmailGateway

### Community 24 - "ICurrentUser"
Cohesion: 0.07
Nodes (26): ICurrentUser, Id, IdAsString, Roles, SessionId, ICollection, HttpContextExtensions, HttpContext (+18 more)

### Community 25 - "Common.Application.Options"
Cohesion: 0.07
Nodes (33): Common.Infrastructure.Modules, Products.Endpoints.Probe, Common.Infrastructure.RateLimiting, Products.Infrastructure.RateLimiting, Products.Endpoints.Products, Common.Infrastructure.Extensions, Common.Application.Options, IAM.Endpoints.Otp.VersionNeutral (+25 more)

### Community 26 - "IEvent"
Cohesion: 0.11
Nodes (14): DomainEventHandlerBase, CancellationToken, Task, IEventHandler, CancellationToken, Task, IEventHandlerWrapper, CancellationToken (+6 more)

### Community 27 - "RequestResponseBodyLoggingMiddleware"
Cohesion: 0.22
Nodes (7): IDiagnosticContext, HttpContext, IList, IOptions, PathString, RequestDelegate, RequestResponseBodyLoggingMiddleware

### Community 28 - ".AssignBasicRoleOrRollbackAsync"
Cohesion: 0.14
Nodes (10): CancellationToken, Exception, ILogger, LoggerMessage, Task, RegistrationCompletion, ActivitySource, Counter (+2 more)

### Community 29 - "Result"
Cohesion: 0.13
Nodes (14): Result, Error, IsFailure, Success, Value, Func, Task, AsyncExtensions (+6 more)

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
Cohesion: 0.12
Nodes (20): CancellationToken, IReadOnlyList, Task, IKeycloakPermissionClient, IReadOnlyList, GrantedPermission, CancellationToken, Dictionary (+12 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.10
Nodes (20): IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, DateTimeOffset, OutboxMessage (+12 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.08
Nodes (22): CheckRegistrationRateLimitingPolicy, EmailRateLimitingPolicy, OtpVerifyRateLimitingPolicy, RegisterRateLimitingPolicy, SmsRateLimitingPolicy, CustomRateLimitingOptions, CheckRegistration, CreateStore (+14 more)

### Community 37 - "AggregateRoot"
Cohesion: 0.17
Nodes (11): AggregateRoot, Events, Id, Version, IReadOnlyCollection, List, IAggregateRoot, Events (+3 more)

### Community 38 - "RequestBody"
Cohesion: 0.15
Nodes (14): DateTimeOffset, DefaultIdType, ICollection, RequestBody, FirstDeadline, IntervalDays, OccurrenceCount, ProductId (+6 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.10
Nodes (20): IHostBuilder, ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics, EnableTracing, LogSink (+12 more)

### Community 40 - "ConfigureSwaggerOptions.cs"
Cohesion: 0.24
Nodes (8): asp_versioning_apiexplorer, Host.Swagger, ISchemaFilter, microsoft_aspnetcore_mvc_apiexplorer, microsoft_openapi, StronglyTypedIdSchemaFilter, swashbuckle_aspnetcore_swaggergen, system_text_json_nodes

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

### Community 45 - ".CreateTokens"
Cohesion: 0.09
Nodes (22): OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, VerifyPhoneOtpResponseExtensions, CancellationToken (+14 more)

### Community 46 - "PaginationQueryableExtensions"
Cohesion: 0.08
Nodes (27): BinaryExpression, ExpressionVisitor, KeyedRow, MemberExpression, MethodInfo, ParameterExpression, Payload, PaginationCursor (+19 more)

### Community 48 - ".SetConcurrency"
Cohesion: 0.50
Nodes (3): IBusFactoryConfigurator, ConcurrencyConfiguratorExtensions, IReceiveEndpointConfigurator

### Community 50 - "Response"
Cohesion: 0.17
Nodes (12): DateOnly, DateTimeOffset, Response, BirthDate, CreatedOn, Email, Enabled, FirstName (+4 more)

### Community 51 - "ProductsDbContext"
Cohesion: 0.15
Nodes (12): DbContextOptions, DbSet, ILogger, ProductTemplate, Store, TimeProvider, ProductsDbContext, Products (+4 more)

### Community 52 - "ReCaptchaService"
Cohesion: 0.21
Nodes (10): FormUrlEncodedContent, ReCaptchaResponse, CancellationToken, Exception, HttpClient, ILogger, IOptions, LoggerMessage (+2 more)

### Community 53 - "Request"
Cohesion: 0.15
Nodes (13): Guid, Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FirstName (+5 more)

### Community 54 - ".SendOtp"
Cohesion: 0.10
Nodes (18): EmailOtpDispatchErrors, EmailOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, SendEmailOtpRequest, SendEmailOtpResponse, CancellationToken (+10 more)

### Community 55 - "BrevoEmailGateway"
Cohesion: 0.23
Nodes (10): Exception, HttpClient, HttpStatusCode, ILogger, IOptions, JsonSerializerOptions, LoggerMessage, BrevoEmailGateway (+2 more)

### Community 56 - "ProjectionReconciliationJob"
Cohesion: 0.24
Nodes (10): Items, Next, ProjectionReconciliationJob, CancellationToken, IDbContext, ILogger, IOptions, IReadOnlyList (+2 more)

### Community 58 - "OutboxMetricsJob"
Cohesion: 0.13
Nodes (14): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxMetricsJob (+6 more)

### Community 59 - ".SearchProductTemplatesAsync"
Cohesion: 0.13
Nodes (12): Products.Endpoints.ProductTemplates.v1.Search, LikePattern, CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint (+4 more)

### Community 61 - ".RequestTokensAsync"
Cohesion: 0.21
Nodes (10): CancellationToken, Task, IKeycloakTokenClient, DateTimeOffset, KeycloakTokens, CancellationToken, Dictionary, Error (+2 more)

### Community 62 - "IDbContext"
Cohesion: 0.12
Nodes (16): DatabaseFacade, Deleted, EntityEntry, JobHousekeepingJob, CancellationToken, ILogger, IOptions, LoggerMessage (+8 more)

### Community 63 - "RequestLoggingOptions"
Cohesion: 0.11
Nodes (20): IPostConfigureOptions, RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes (+12 more)

### Community 64 - ".SendCoreAsync"
Cohesion: 0.21
Nodes (8): SendErrorBody, CancellationToken, HttpResponseMessage, SendRequestBody, Task, SendContact, Email, Name

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
Cohesion: 0.09
Nodes (24): Add a new language/culture, Add search to a new entity _(Build checklist)_, Change the vector (weights, fields, or config), Extending and maintaining, Full-Text Search, Gotchas, How it works, Non-goals (+16 more)

### Community 72 - "Response"
Cohesion: 0.18
Nodes (9): Products.Endpoints.ProductTemplates.v1.Get, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Brand, Color (+1 more)

### Community 73 - ".WriteAsync"
Cohesion: 0.33
Nodes (5): Memory, ReadOnlyMemory, CancellationToken, Task, ValueTask

### Community 75 - "Inventory.Domain.StockReservations"
Cohesion: 0.11
Nodes (15): asp_versioning, asp_versioning_builder, asp_versioning_conventions, Products.Infrastructure.InterModuleRequestHandlers, Inventory.Infrastructure.InterModuleRequestHandlers, Inventory.Endpoints, Common.Endpoints.Versioning, Inventory.Application.Persistence (+7 more)

### Community 76 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 77 - ".RemoveMyProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 78 - "CheckRegistrationRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, CheckRegistrationRateLimitingPolicy (+1 more)

### Community 79 - "DomainEvent"
Cohesion: 0.06
Nodes (31): Products.Domain.Products.DomainEvents.v1, Products.Application.Products.DomainEventHandlers.v1, DomainEvent, CreatedOn, Id, Version, DateTimeOffset, DefaultIdType (+23 more)

### Community 80 - "IAuditableEntity"
Cohesion: 0.18
Nodes (10): IAuditableEntity, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, DateTimeOffset, CancellationToken, DbContextEventData (+2 more)

### Community 81 - ".SearchStoresAsync"
Cohesion: 0.15
Nodes (12): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Address (+4 more)

### Community 82 - "Split-Deployment PoC"
Cohesion: 0.18
Nodes (9): Concurrent safety, Cross-process call path, Files added by this PoC, How it works, How to run, Split-Deployment PoC, What this proves, README (Boilerplate Overview) (+1 more)

### Community 83 - "microsoft_entityframeworkcore"
Cohesion: 0.06
Nodes (39): Common.Infrastructure.Persistence.Inbox, Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Products.Infrastructure.Persistence.Seeding, Common.Application.Persistence.Inbox, Inventory.Application.IntegrationEventHandlers, Common.IntegrationEvents, Common.Application.Persistence (+31 more)

### Community 84 - ".Configure"
Cohesion: 0.15
Nodes (14): AuditableEntityConfiguration, EntityTypeBuilder, JobRowConfiguration, EntityTypeBuilder, StronglyTypedIdValueConverter, DefaultIdType, EntityTypeBuilder, NpgsqlTsVector (+6 more)

### Community 85 - "CaptchaOptions"
Cohesion: 0.16
Nodes (13): CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey, Provider, ScoreThreshold, SecretKey (+5 more)

### Community 86 - "Store"
Cohesion: 0.09
Nodes (17): StoreId, V1StoreDescriptionUpdatedDomainEvent, StoreId, V1StoreNameUpdatedDomainEvent, IReadOnlyCollection, List, Store, Address (+9 more)

### Community 87 - "PushOptions"
Cohesion: 0.12
Nodes (20): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri, PushOptions (+12 more)

### Community 89 - "Program.cs"
Cohesion: 0.22
Nodes (6): ConfigurationManager, Host, Host.Configurations, Setup, Program, WebApplicationBuilder

### Community 90 - ".GetProductAsync"
Cohesion: 0.25
Nodes (10): GetStockLevelRequest, GetStockLevelResponse, DefaultIdType, CancellationToken, Task, GetStockLevelRequestHandler, CancellationToken, DefaultIdType (+2 more)

### Community 91 - ".SeedProductAsync"
Cohesion: 0.33
Nodes (5): CancellationToken, List, ProductTemplateId, StoreId, Task

### Community 92 - "Infrastructure/Setup.cs"
Cohesion: 0.06
Nodes (30): Common.InterModuleRequests, Common.Infrastructure.Localization, Common.Application.JsonConverters, Products.Endpoints.Stores.v1.AddProduct, Common.Infrastructure.Caching, microsoft_aspnetcore_authentication, microsoft_aspnetcore_http_json, microsoft_entityframeworkcore_storage_valueconversion (+22 more)

### Community 93 - ".MapEndpoint"
Cohesion: 0.22
Nodes (6): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, RouteGroupBuilder, Response, ClientKey, RouteGroupBuilder, Setup

### Community 94 - "KeyedResiliencePipelines"
Cohesion: 0.14
Nodes (13): HttpRequestException, ResiliencePipelineBuilder, ResiliencePipelineRegistry, KeyedResiliencePipelines, HttpResponseMessage, IConnectionMultiplexer, ILoggerFactory, IOptions (+5 more)

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
Nodes (12): Products.Endpoints.Products.v1.Search, CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response (+4 more)

### Community 99 - "AuditLogRetentionJobRegistrar"
Cohesion: 0.24
Nodes (8): AuditLogRetentionJobRegistrar, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task, Setup, IServiceCollection

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

### Community 104 - "AuditableEntityResponse"
Cohesion: 0.11
Nodes (16): AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, DateTimeOffset, RouteGroupBuilder (+8 more)

### Community 105 - "Response"
Cohesion: 0.18
Nodes (9): IAM.Endpoints.Users.VersionNeutral.SelfRegisterByEmail, RouteGroupBuilder, Endpoint, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken (+1 more)

### Community 106 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.06
Nodes (28): Notifications.Application.Otp, IAM.Endpoints.Users, IAM.Infrastructure.Keycloak.Representations, Common.Application.Caching, IAM.Endpoints.Otp, Notifications.Application.Persistence, Notifications.Infrastructure.Telemetry, Common.InterModuleRequests.IAM (+20 more)

### Community 107 - "HttpWarehouseGateway"
Cohesion: 0.31
Nodes (8): ReleaseResponseBody, CancellationToken, HttpClient, HttpResponseMessage, Task, HttpWarehouseGateway, ReleaseRequestBody, ReleaseResponseBody

### Community 108 - "StockLevel"
Cohesion: 0.12
Nodes (11): StronglyTypedIdHelper, DefaultIdType, StockLevelId, V1StockLevelCreatedDomainEvent, DefaultIdType, StockLevel, ProductId, QuantityOnHand (+3 more)

### Community 109 - ".IsRegisteredAsync"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, IsRegistered

### Community 110 - "KeycloakPermissionPolicyProvider"
Cohesion: 0.24
Nodes (8): AuthorizationOptions, AuthorizationPolicy, DefaultAuthorizationPolicyProvider, IAuthorizationPolicyProvider, ConcurrentDictionary, IOptions, Task, KeycloakPermissionPolicyProvider

### Community 111 - ".ReleaseStockReservationAsync"
Cohesion: 0.13
Nodes (14): CancellationToken, Task, IWarehouseGateway, CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint (+6 more)

### Community 112 - "OutboxCleanupJob"
Cohesion: 0.27
Nodes (8): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxCleanupJob

### Community 113 - ".AddPersistence"
Cohesion: 0.15
Nodes (11): IDatabaseSeeder, Priority, CancellationToken, Task, CancellationToken, IServiceScopeFactory, Task, ProductsDatabaseSeeder (+3 more)

### Community 114 - "Response"
Cohesion: 0.11
Nodes (17): IAM.Endpoints.Users.VersionNeutral.Me.Get, RouteGroupBuilder, Endpoint, DateOnly, DateTimeOffset, IReadOnlyCollection, Response, BirthDate (+9 more)

### Community 115 - "AuditLogRetentionService"
Cohesion: 0.33
Nodes (7): AuditLogRetentionService, CancellationToken, ILogger, IOptions, LoggerMessage, NpgsqlDataSource, Task

### Community 116 - ".AddAuthInfrastructure"
Cohesion: 0.17
Nodes (11): IAllowAnonymous, IAuthorizationHandler, IConfigureNamedOptions, IOptions, JwtBearerOptions, JwtBearerConfigureOptions, IAuthorizationPolicyProvider, IConfigureOptions (+3 more)

### Community 117 - "Response"
Cohesion: 0.09
Nodes (20): Inventory.Endpoints.StockReservations.v1.Get, ReservationStatus, Active, Committed, Expired, Released, RequiresReconciliation, CancellationToken (+12 more)

### Community 118 - "IModule"
Cohesion: 0.09
Nodes (20): OpenTelemetryBuilder, ResourceBuilder, IModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority (+12 more)

### Community 119 - "ProductsModule"
Cohesion: 0.12
Nodes (13): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, ProductsModule (+5 more)

### Community 120 - "InventoryOptions"
Cohesion: 0.08
Nodes (27): IHeaderDictionary, IValidator, InventoryOptions, MaxOccurrencesPerSeries, SafetyBufferQuantity, StockReservationExpirySweepBatchSize, StockReservationExpirySweepCron, WarehouseGatewayBaseUrl (+19 more)

### Community 121 - ".GetVariantAsync"
Cohesion: 0.40
Nodes (4): IVariantFeatureManager, IVariantFeatureManagerExtensions, CancellationToken, Task

### Community 122 - "OtpVerifyRateLimitingPolicy"
Cohesion: 0.18
Nodes (10): IRateLimiterPolicy, CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask (+2 more)

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
Cohesion: 0.27
Nodes (10): Exception, HttpContext, ILogger, IOptions, IProblemDetailsService, IResxLocalizer, LoggerMessage, RequestDelegate (+2 more)

### Community 127 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 128 - "EmailRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, EmailRateLimitingPolicy (+1 more)

### Community 129 - "KeycloakTokenClient"
Cohesion: 0.29
Nodes (8): JsonWebTokenHandler, Exception, HttpClient, ILogger, IOptions, LoggerMessage, TimeProvider, KeycloakTokenClient

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.48
Nodes (7): Observability (OpenTelemetry), docker-compose.yml (Base Stack), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), Postgres Service (mm.postgres), RabbitMQ Service (mm.rabbitmq), Redis Service (mm.redis)

### Community 131 - ".AddInfrastructure"
Cohesion: 0.29
Nodes (7): HostOptions, JsonOptions, Assembly, IConfiguration, IResxLocalizer, IServiceCollection, IWebHostEnvironment

### Community 132 - "StoreId"
Cohesion: 0.08
Nodes (21): Products.Endpoints.Stores.v1.Update, Products.Endpoints.Stores.v1.Deactivate, Products.Endpoints.Stores.v1.Get, StoreId, DefaultIdType, StoreId, Request, Id (+13 more)

### Community 134 - "SendRequestBody"
Cohesion: 0.25
Nodes (8): SendContact, IReadOnlyList, SendRequestBody, HtmlContent, Sender, Subject, TextContent, To

### Community 135 - ".ListSessions"
Cohesion: 0.10
Nodes (22): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, IReadOnlyList, CancellationToken, IReadOnlyCollection, RouteGroupBuilder (+14 more)

### Community 136 - ".UseModules"
Cohesion: 0.26
Nodes (6): ModuleRegistry, Exception, IApplicationBuilder, ILogger, LoggerMessage, WebApplication

### Community 137 - ".WriteTooManyRequestsToResponse"
Cohesion: 0.13
Nodes (14): PartitionedRateLimiter, CancellationToken, Func, HttpContext, IConfiguration, IProblemDetailsService, IReadOnlyList, IResxLocalizer (+6 more)

### Community 138 - "ResultToResponseTransformer.cs"
Cohesion: 0.08
Nodes (30): AspNetResult, Common.Application.EndpointFilters, IEndpointFilter, IFeatureManagerSnapshot, microsoft_aspnetcore_hosting, microsoft_aspnetcore_http_iresult, microsoft_extensions_localization, ResxLocalizer (+22 more)

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
Cohesion: 0.11
Nodes (15): LoadAll, LoggerConfiguration, LoggerMinimumLevelConfiguration, Names, Setup, IEnumerable, IHostEnvironment, KeyValuePair (+7 more)

### Community 144 - "Response"
Cohesion: 0.12
Nodes (16): CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate (+8 more)

### Community 145 - "RequestBody"
Cohesion: 0.29
Nodes (7): ProductTemplateId, RequestBody, Description, Name, Price, ProductTemplateId, Quantity

### Community 146 - ".RegisterAsync"
Cohesion: 0.13
Nodes (18): BindDeviceSessionRequest, BindDeviceSessionResponse, Guid, CancellationToken, Exception, Guid, ILogger, LoggerMessage (+10 more)

### Community 147 - "IRecurringBackgroundJobs"
Cohesion: 0.14
Nodes (13): RecurringJobOptions, IRecurringBackgroundJobs, Action, Expression, Func, Task, RecurringBackgroundJobsService, Action (+5 more)

### Community 148 - ".RegisterAsync"
Cohesion: 0.08
Nodes (28): IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, IInterModuleRequestClient, CancellationToken, Task, VerifyEmailOtpRequest, VerifyEmailOtpResponse, EmailNormalization, VerifyEmailOtpResponseExtensions (+20 more)

### Community 149 - "TokenResponseRepresentation"
Cohesion: 0.15
Nodes (12): List, DecisionRepresentation, Result, PermissionRepresentation, ResourceName, Scopes, TokenResponseRepresentation, AccessToken (+4 more)

### Community 150 - "StockReservationExpirySweepJobRegistrar"
Cohesion: 0.27
Nodes (8): IHostedService, CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task, StockReservationExpirySweepJobRegistrar

### Community 151 - "OutboxOptions"
Cohesion: 0.10
Nodes (21): OutboxCleanupSettings, BatchSize, CronSchedule, Enabled, RetentionDays, OutboxCleanupSettingsValidator, OutboxOptions, BaseBackoffSeconds (+13 more)

### Community 152 - ".EnsureNoMigrationsPending"
Cohesion: 0.54
Nodes (4): MigrationGuard, ILogger, IServiceProvider, LoggerMessage

### Community 153 - "ConfigureSwaggerOptions"
Cohesion: 0.28
Nodes (7): ApiVersionDescription, IApiVersionDescriptionProvider, IConfigureOptions, OpenApiInfo, IOptions, SwaggerGenOptions, ConfigureSwaggerOptions

### Community 154 - "Setup.Logger.cs"
Cohesion: 0.08
Nodes (18): Host.Infrastructure, elastic_serilog_sinks, microsoft_aspnetcore_httpoverrides, opentelemetry_exporter, opentelemetry_metrics, opentelemetry_opentelemetrybuilder, opentelemetry_resources, opentelemetry_trace (+10 more)

### Community 155 - "ISmsGateway"
Cohesion: 0.13
Nodes (13): CancellationToken, Task, ISmsGateway, SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, SmsMessage (+5 more)

### Community 156 - "PolymorphicEventConverter"
Cohesion: 0.21
Nodes (7): JsonConverter, UnknownDomainEvent, PolymorphicEventConverter, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 157 - "CreateStockLevelOnProductCreatedHandler"
Cohesion: 0.24
Nodes (8): CancellationToken, DefaultIdType, IFusionCache, ILogger, IOptions, LoggerMessage, Task, CreateStockLevelOnProductCreatedHandler

### Community 158 - "OtpOptions"
Cohesion: 0.18
Nodes (11): OtpOptions, DummyCode, EmailQuotaWindowMinutes, ExpirationInMinutes, Length, MaxSendsPerEmailPerWindow, MaxSendsPerPhonePerWindow, PhoneQuotaWindowMinutes (+3 more)

### Community 159 - "IInventoryDbContext"
Cohesion: 0.13
Nodes (15): DbSet, IInventoryDbContext, StockLevels, StockReservations, DbContextOptions, DbSet, ILogger, TimeProvider (+7 more)

### Community 160 - "OtpServiceBase"
Cohesion: 0.21
Nodes (8): OtpCacheEntry, DateTimeOffset, CancellationToken, IFusionCache, SemaphoreSlim, Task, TimeSpan, OtpServiceBase

### Community 161 - "IntegrationEventOutbox"
Cohesion: 0.14
Nodes (13): IIntegrationEventOutbox, IntegrationEventOutbox, HasPending, List, Lock, Setup, IServiceCollection, CancellationToken (+5 more)

### Community 162 - "IEmailGateway"
Cohesion: 0.13
Nodes (13): CancellationToken, Task, IEmailGateway, IConfiguration, IFusionCache, IOptions, IServiceCollection, Setup (+5 more)

### Community 164 - "NotificationsDbContext"
Cohesion: 0.15
Nodes (13): DbSet, INotificationsDbContext, DeviceRegistrations, DbContextOptions, DbSet, ILogger, TimeProvider, NotificationsDbContext (+5 more)

### Community 165 - "CurrentUser"
Cohesion: 0.12
Nodes (14): Common.Infrastructure.Auth.Services, Common.Infrastructure.Auth, CurrentUser, Id, IdAsString, IsAuthenticated, Principal, Roles (+6 more)

### Community 166 - "IamModule"
Cohesion: 0.18
Nodes (10): Action, IApplicationBuilder, IEnumerable, RateLimiterOptions, IamModule, ActivitySourceNames, MeterNames, Name (+2 more)

### Community 167 - "PaginationRequest"
Cohesion: 0.05
Nodes (45): Products.Endpoints.Stores.v1.AuditLog, IAM.Endpoints.Users.VersionNeutral.Search, Products.Endpoints.Stores.v1.My.AuditLog, Products.Endpoints.Products.v1.AuditLog, PaginationRequest, After, IncludeTotal, PageNumber (+37 more)

### Community 168 - ".CreateProductTemplateAsync"
Cohesion: 0.17
Nodes (8): ProductTemplate, RouteGroupBuilder, Setup, CancellationToken, RouteGroupBuilder, Task, Response, Id

### Community 169 - "SendSecurityAlertRequestHandler"
Cohesion: 0.27
Nodes (9): SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, CancellationToken, IOptions, RequestLocalizationOptions, Task (+1 more)

### Community 171 - "IntegrationEvent"
Cohesion: 0.20
Nodes (9): IReadOnlyList, IntegrationEvent, CreatedOn, Id, DateTimeOffset, DefaultIdType, ProductCreatedIntegrationEvent, StoreCreatedIntegrationEvent (+1 more)

### Community 172 - "Response"
Cohesion: 0.22
Nodes (6): Products.Endpoints.Stores.v1.Create, RouteGroupBuilder, Setup, RouteGroupBuilder, Response, Id

### Community 173 - "ProductTemplateId"
Cohesion: 0.25
Nodes (6): ProductTemplateId, DefaultIdType, ProductTemplateId, CancellationToken, List, Task

### Community 174 - "system_diagnostics"
Cohesion: 0.12
Nodes (11): Common.Application.BackgroundJobs, BackgroundJobs.Telemetry, BackgroundJobs, hangfire, hangfire_postgresql, hangfire_server, LoginMethods, SessionRevokedReasons (+3 more)

### Community 175 - "PaginationResponse"
Cohesion: 0.07
Nodes (27): JsonElement, AuditLogDto, DateTimeOffset, PaginationResponse, HasNext, HasPrevious, HasTotal, NextPageNumber (+19 more)

### Community 176 - ".FixedWindow"
Cohesion: 0.23
Nodes (10): FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit, RateLimitPartitions, HttpContext, IConnectionMultiplexer (+2 more)

### Community 177 - "V1StoreCreatedDomainEvent"
Cohesion: 0.27
Nodes (8): Products.Application.Stores.DomainEventHandlers.v1, CancellationToken, Task, SimulateSomeBusinessHandler, StoreCreatedIntegrationEventPublishingHandler, V1StoreCreatedDomainEventHandlers, StoreId, V1StoreCreatedDomainEvent

### Community 178 - "PushMessage"
Cohesion: 0.14
Nodes (14): IReadOnlyDictionary, CancellationToken, IReadOnlyList, Task, IPushGateway, PushMessage, CancellationToken, ILogger (+6 more)

### Community 179 - "CachingOptions"
Cohesion: 0.08
Nodes (26): CachingEntryDefaults, Duration, FactoryHardTimeout, FactorySoftTimeout, FailSafeMaxDuration, FailSafeThrottleDuration, CachingOptions, AllowInMemoryOnlyInProduction (+18 more)

### Community 180 - "SmsOptions"
Cohesion: 0.10
Nodes (21): SmsOptions, AppName, AttemptTimeoutSeconds, BaseUrl, MaxPerDay, MaxPerPhoneNumberPerDay, MaxRetryAttempts, MsgHeader (+13 more)

### Community 181 - "Response"
Cohesion: 0.20
Nodes (8): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, RouteGroupBuilder, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 183 - "SendRequestBody"
Cohesion: 0.25
Nodes (8): SendMessageBody, IReadOnlyList, SendRequestBody, AppName, Encoding, IysFilter, Messages, MsgHeader

### Community 184 - "RedisOtpService"
Cohesion: 0.12
Nodes (13): OtpCodeGenerator, IFusionCache, IOptions, OtpService, CancellationToken, IConnectionMultiplexer, IOptions, Task (+5 more)

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

### Community 191 - "ProjectionUpsertOutcome"
Cohesion: 0.20
Nodes (7): Common.Application.Persistence.Projections, ProjectionEntity, SourceVersion, ProjectionUpsertOutcome, Inserted, Stale, Updated

### Community 192 - "ReCaptchaResponse"
Cohesion: 0.25
Nodes (7): DateTime, ReCaptchaResponse, ChallengeTs, ErrorCodes, Hostname, Score, Success

### Community 193 - ".UpdateMyStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 194 - "Seeder"
Cohesion: 0.23
Nodes (9): CancellationToken, ILogger, LoggerMessage, ProductsDbContext, Task, Seeder, CancellationToken, List (+1 more)

### Community 195 - ".ReserveStockAsync"
Cohesion: 0.22
Nodes (7): Inventory.Endpoints.StockReservations.v1.Reserve, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Id

### Community 196 - "TokenCreateRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenCreateRateLimitingPolicy (+1 more)

### Community 197 - "StringExtensions"
Cohesion: 0.20
Nodes (5): SearchValues, StringExtensions, HmacSignatureVerifier, ReadOnlySpan, system_buffers

### Community 198 - "InventoryModule"
Cohesion: 0.15
Nodes (10): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, InventoryModule, ActivitySourceNames, MeterNames (+2 more)

### Community 199 - ".AddKeycloakInfrastructure"
Cohesion: 0.16
Nodes (10): CancellationToken, Task, IServiceAccountTokenProvider, HttpClient, IOptions, TimeProvider, ServiceAccountTokenProvider, IOptions (+2 more)

### Community 200 - "NotificationsTelemetry"
Cohesion: 0.22
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

### Community 210 - "ICaptchaService"
Cohesion: 0.15
Nodes (10): ICaptchaService, CancellationToken, IFusionCache, IOptions, Task, CachedCaptchaService, DummyCaptchaService, IConfiguration (+2 more)

### Community 211 - "SeedingCompletionTracker"
Cohesion: 0.15
Nodes (8): SeedingCompletionTracker, CancellationToken, Exception, Task, Setup, IOptions, IServiceCollection, TaskCompletionSource

### Community 212 - "Setup"
Cohesion: 0.33
Nodes (4): ApiVersionSet, Setup, IEndpointRouteBuilder, IServiceCollection

### Community 213 - "BackgroundJobsTelemetry"
Cohesion: 0.14
Nodes (11): IServerFilter, PerformedContext, PerformingContext, JobMetricsFilter, BackgroundJobsTelemetry, ActivitySource, ConcurrentDictionary, Counter (+3 more)

### Community 214 - "fluentvalidation"
Cohesion: 0.07
Nodes (36): common_application_localization_resources, Products.Endpoints.Stores.v1.My.Update, IAM.Domain.Users, IAM.Endpoints.Otp.VersionNeutral.SendForLogin, Inventory.Endpoints.StockReservations.v1.WebhookCallback, Common.Domain.Extensions, Notifications.Infrastructure.Devices.UpdateCurrentPushToken, Common.Application.Validation (+28 more)

### Community 215 - "IProductsDbContext"
Cohesion: 0.09
Nodes (19): Products.Endpoints.Stores.v1.My.Create, DbSet, Store, IProductsDbContext, Products, ProductTemplates, Stores, CancellationToken (+11 more)

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
Cohesion: 0.26
Nodes (7): IBackgroundJobs, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 220 - "StrictDateTimeOffsetJsonConverter"
Cohesion: 0.33
Nodes (6): StrictDateTimeOffsetJsonConverter, DateTimeOffset, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 221 - "Keycloak realm as code"
Cohesion: 0.25
Nodes (7): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Secrets, Seed users (dev / test only)

### Community 222 - ".UpdateCurrentPushToken"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 223 - "FirebasePushGateway.cs"
Cohesion: 0.22
Nodes (6): Notifications.Application.Push, Notifications.Infrastructure.Push, Notifications.Infrastructure.Push.Firebase, firebaseadmin, firebaseadmin_messaging, google_apis_auth_oauth2

### Community 224 - ".MapEndpoint"
Cohesion: 0.40
Nodes (3): RouteGroupBuilder, RouteGroupBuilder, Setup

### Community 225 - "IInboxStore"
Cohesion: 0.16
Nodes (11): IInboxCleanupTarget, ModuleName, IInboxStore, CancellationToken, DateTimeOffset, DefaultIdType, Task, Setup (+3 more)

### Community 226 - "NetGsmSmsGateway"
Cohesion: 0.24
Nodes (9): SendResponseBody, Exception, HttpClient, ILogger, IOptions, JsonSerializerOptions, LoggerMessage, SendRequestBody (+1 more)

### Community 227 - "RegisterRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, RegisterRateLimitingPolicy (+1 more)

### Community 228 - "Request"
Cohesion: 0.20
Nodes (10): StoreId, Request, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name, OwnerId (+2 more)

### Community 229 - ".AddCommonOptions"
Cohesion: 0.40
Nodes (4): Setup, IConfiguration, IHostEnvironment, IServiceCollection

### Community 230 - "InterModuleRequestHandler"
Cohesion: 0.16
Nodes (14): IInterModuleRequestHandler, CancellationToken, Task, InterModuleRequestHandler, CancellationToken, ConsumeContext, Task, GetActiveSessionIdsRequest (+6 more)

### Community 231 - "HangfireCustomAuthorizationFilter"
Cohesion: 0.33
Nodes (5): DashboardContext, IAuthorizationService, IDashboardAsyncAuthorizationFilter, HangfireCustomAuthorizationFilter, Task

### Community 232 - "BaseDbContext"
Cohesion: 0.22
Nodes (8): BaseDbContext, AuditLog, CancellationToken, DbContextOptions, DbSet, ILogger, Task, TimeProvider

### Community 233 - ".HasPatternIndex"
Cohesion: 0.36
Nodes (6): IndexBuilder, TrigramIndexExtensions, EntityTypeBuilder, Expression, Func, ModelBuilder

### Community 234 - "JwtBearerConfigureOptions.cs"
Cohesion: 0.16
Nodes (10): IAM.Infrastructure.Auth, Notifications.Infrastructure.Hubs, Notifications.Application.Hubs, hangfire_annotations, hangfire_dashboard, microsoft_aspnetcore_authentication_jwtbearer, microsoft_aspnetcore_authorization, microsoft_aspnetcore_signalr (+2 more)

### Community 235 - "AuditLogEntry"
Cohesion: 0.10
Nodes (16): AuditLogEntry, AggregateId, AggregateType, Event, EventType, Version, DefaultIdType, ModelBuilder (+8 more)

### Community 236 - "IOtpService"
Cohesion: 0.24
Nodes (8): CancellationToken, Task, TimeSpan, IOtpService, OtpVerificationOutcome, InvalidOtp, Success, TooManyAttempts

### Community 237 - "microsoft_aspnetcore_mvc"
Cohesion: 0.06
Nodes (35): Products.Endpoints.ProductTemplates.v1.Deactivate, Products.Endpoints.Probe.v1, Inventory.Endpoints.StockReservations.v1.Release, Common.Application.ModelBinders, Products.Endpoints.ProductTemplates.v1.Activate, Products.Endpoints.Stores.v1.RemoveProduct, microsoft_aspnetcore_mvc, microsoft_aspnetcore_mvc_modelbinding (+27 more)

### Community 238 - ".UseInfrastructure"
Cohesion: 0.18
Nodes (9): IAuthenticationSchemeProvider, RequestBodyLimitMiddleware, RequestDelegate, IApplicationBuilder, HttpContext, IOptions, RequestDelegate, Task (+1 more)

### Community 239 - "microsoft_extensions_configuration"
Cohesion: 0.06
Nodes (24): Notifications.Application.Sms, Notifications.Infrastructure.Devices, Notifications.Infrastructure.Email, Notifications.Infrastructure.Sms, Notifications.Infrastructure.Email.Brevo, Inventory.Infrastructure.Gateway, IAM.Domain.Captcha, IAM.Infrastructure.Captcha.Services (+16 more)

### Community 240 - "RabbitMqOptions"
Cohesion: 0.05
Nodes (38): ConsumerDefinition, IClientFactory, IConsumerConfigurator, IRegistrationContext, InterModuleRequestOptions, DependencyUnavailableRetryAfterSeconds, HandlerConcurrentMessageLimit, HandlerPrefetchCount (+30 more)

### Community 241 - "Request"
Cohesion: 0.20
Nodes (10): Guid, Request, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken, Password (+2 more)

### Community 242 - "V1StockReservationCommitConflictDetectedDomainEvent"
Cohesion: 0.22
Nodes (8): DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationCommitConflictDetectedDomainEvent, DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationCommittedDomainEvent

### Community 243 - "CustomValidator"
Cohesion: 0.06
Nodes (38): Inventory.Endpoints.StockReservations.v1.Commit, CustomRateLimitingOptionsValidator, FixedWindowValidator, DatabaseOptions, ConnectionString, DatabaseOptionsValidator, ProjectionReconciliationOptions, MaxPages (+30 more)

### Community 244 - "KeyedResilienceProfile"
Cohesion: 0.15
Nodes (13): AbstractValidator, KeyedResilienceProfile, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, RateLimitFailOpen (+5 more)

### Community 245 - "SendForEmail/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForEmail, Request, CaptchaToken, Email, RequestValidator

### Community 246 - "UtcDateTimeOffsetConverter"
Cohesion: 0.22
Nodes (6): DateTimeOffset, ModelConfigurationBuilder, UtcDateTimeOffsetConverter, DateTimeOffset, DateTimeOffset, ModelConfigurationBuilder

### Community 247 - "OpenApiOptions"
Cohesion: 0.22
Nodes (9): OpenApiOptions, ContactEmail, ContactName, Description, EnableSwagger, LicenseName, LicenseUrl, Title (+1 more)

### Community 248 - "SendErrorBody"
Cohesion: 0.67
Nodes (3): SendErrorBody, Code, Message

### Community 249 - "DeviceRegistryReconciliationService"
Cohesion: 0.15
Nodes (10): CancellationToken, ILogger, IOptions, LoggerMessage, Task, TimeProvider, DeviceRegistryReconciliationService, IServiceCollection (+2 more)

### Community 250 - "Response"
Cohesion: 0.20
Nodes (9): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Description, Name, Price (+1 more)

### Community 251 - "ProductId"
Cohesion: 0.08
Nodes (23): Products.Endpoints.Products.v1.My.Update, Products.Endpoints.Stores.v1.My.RemoveProduct, DefaultIdType, StoreId, ProductId, ProductSnapshot, ProductTemplateId, ProductSnapshot (+15 more)

### Community 252 - ".TryWriteAsync"
Cohesion: 0.40
Nodes (4): ProblemDetailsContext, ProblemDetailsServiceExtensions, IProblemDetailsService, Task

### Community 260 - "RequestBodyLimitMetadata"
Cohesion: 0.33
Nodes (6): RequestBodyLimitMetadata, MaxBodyBytes, Func, HttpContext, IHttpMaxRequestBodySizeFeature, Task

### Community 261 - "CorsOptions"
Cohesion: 0.25
Nodes (8): CorsOptions, AllowCredentials, AllowedHeaders, AllowedMethods, AllowedOrigins, MaxAgeInSeconds, CorsOptionsValidator, IReadOnlyList

### Community 262 - ".SendWithPipelineAsync"
Cohesion: 0.25
Nodes (7): HttpClientKeyedExtensions, CancellationToken, HttpClient, HttpRequestMessage, HttpResponseMessage, ResiliencePipeline, Task

### Community 263 - "Response"
Cohesion: 0.25
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegister, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 264 - "ResxLocalizationOptions"
Cohesion: 0.40
Nodes (5): ResxLocalizationOptions, DefaultCulture, SupportedCultures, ResxLocalizationOptionsValidator, ICollection

### Community 265 - ".AddServices"
Cohesion: 0.15
Nodes (11): BackgroundJobsModule, ActivitySourceNames, MeterNames, Name, StartupPriority, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder (+3 more)

### Community 266 - ".AddCustomSwagger"
Cohesion: 0.15
Nodes (11): IOperationFilter, JsonValue, OpenApiOperation, OperationFilterContext, RemoveDefaultResponseSchemaFilter, IConfigureOptions, IServiceCollection, SwaggerGenOptions (+3 more)

### Community 267 - "AuditLogOptions"
Cohesion: 0.33
Nodes (6): AuditLogOptions, PerSchemaRetentionDays, PurgeBatchSize, RetentionDays, AuditLogOptionsValidator, Dictionary

### Community 268 - ".DeactivateProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 269 - ".SaveChangesAsync"
Cohesion: 0.08
Nodes (19): CancellationToken, Task, DbContextExtensions, CancellationToken, DbSet, JsonSerializerOptions, Task, CancellationToken (+11 more)

### Community 270 - ".SendAsync"
Cohesion: 0.22
Nodes (5): CancellationToken, Task, SendMessageBody, Msg, No

### Community 271 - "ProductTemplates/v1/Create/Request.cs"
Cohesion: 0.29
Nodes (6): Products.Endpoints.ProductTemplates.v1.Create, Request, Brand, Color, Model, RequestValidator

### Community 272 - ".AddResilientHttpClient"
Cohesion: 0.22
Nodes (8): HttpStandardResilienceOptions, IHttpClientBuilder, Setup, Action, HttpClient, IOptions, IServiceCollection, IServiceProvider

### Community 273 - "IntegrationEventConverter"
Cohesion: 0.25
Nodes (6): IEntityTypeConfiguration, IntegrationEventConverter, JsonSerializerOptions, ModelBuilder, EntityTypeBuilder, OutboxMessageConfig

### Community 274 - "DeviceRegistryReconcileJobRegistrar"
Cohesion: 0.31
Nodes (7): CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task, DeviceRegistryReconcileJobRegistrar

### Community 275 - "StronglyTypedIdListReadOnlyJsonConverter"
Cohesion: 0.36
Nodes (6): StronglyTypedIdListReadOnlyJsonConverter, IReadOnlyList, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 276 - "StronglyTypedIdWriteOnlyJsonConverter"
Cohesion: 0.32
Nodes (5): StronglyTypedIdWriteOnlyJsonConverter, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 277 - "SendResponseBody"
Cohesion: 0.50
Nodes (4): SendResponseBody, Code, Description, JobId

### Community 278 - "BackgroundJobsOptions"
Cohesion: 0.29
Nodes (7): BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount, BackgroundJobsOptionsValidator

### Community 279 - "Response"
Cohesion: 0.25
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.Create, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 281 - ".TryReadFromJsonAsync"
Cohesion: 0.40
Nodes (4): HttpContent, CancellationToken, Task, HttpContentJsonExtensions

### Community 282 - "Key decisions"
Cohesion: 0.29
Nodes (7): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Key decisions

### Community 283 - "AuditableEntity"
Cohesion: 0.25
Nodes (8): AuditableEntity, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, Version, DateTimeOffset

### Community 284 - "RequestBody"
Cohesion: 0.33
Nodes (6): DateTimeOffset, DefaultIdType, RequestBody, ProductId, Quantity, ReservationDeadline

### Community 285 - "RequestBodyLimitFilter.cs"
Cohesion: 0.18
Nodes (10): Common.Endpoints.Webhooks, microsoft_aspnetcore_http_features, RequestBodyLimitEndpointExtensions, RequestBodyLimitFilter, EndpointFilterDelegate, EndpointFilterInvocationContext, Func, HttpContext (+2 more)

### Community 286 - "DevicesOptions"
Cohesion: 0.33
Nodes (6): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator, IReadOnlyCollection

### Community 287 - ".CommitStockReservationAsync"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 288 - "DummySmsGateway"
Cohesion: 0.38
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, DummySmsGateway

### Community 289 - ".SetRetryAfterHeader"
Cohesion: 0.40
Nodes (4): RetryAfterHeaderExtensions, HttpResponse, RateLimitLease, TimeSpan

### Community 290 - "IInterModuleRequest"
Cohesion: 0.16
Nodes (14): IInterModuleRequest, GetUsersInRolePageRequest, GetUsersInRolePageResponse, RoleUserSummary, ICollection, IssueVerificationTokenRequest, IssueVerificationTokenResponse, CancellationToken (+6 more)

### Community 291 - ".AddNetGsm"
Cohesion: 0.33
Nodes (5): IConfiguration, IFusionCache, IOptions, IServiceCollection, Setup

### Community 294 - "KeycloakPermissionRequirement"
Cohesion: 0.20
Nodes (6): IAuthorizationRequirement, KeycloakPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder, KeycloakPermissionRequirement, Permission

### Community 295 - "EnrichLogsWithUserInfoMiddleware"
Cohesion: 0.33
Nodes (5): IMiddleware, HttpContext, RequestDelegate, Task, EnrichLogsWithUserInfoMiddleware

### Community 296 - ".HandleRequirementAsync"
Cohesion: 0.40
Nodes (4): AuthorizationHandlerContext, HttpContext, Task, AccessTokenReader

### Community 297 - "VersionNeutral/Get/Request.cs"
Cohesion: 0.40
Nodes (4): IAM.Endpoints.Users.VersionNeutral.Get, Request, Id, RequestValidator

### Community 298 - "Setup"
Cohesion: 0.33
Nodes (4): IApplicationBuilder, IWebHostEnvironment, Type, Setup

### Community 299 - ".MapEndpoint"
Cohesion: 0.40
Nodes (3): RouteGroupBuilder, RouteGroupBuilder, Setup

### Community 300 - "ResultTelemetryExtensions"
Cohesion: 0.38
Nodes (4): Activity, ResultTelemetryExtensions, ActivitySource, Task

### Community 301 - "JobHousekeepingOptions"
Cohesion: 0.40
Nodes (5): JobHousekeepingOptions, PageSize, RetentionHours, StaleAfterMinutes, JobHousekeepingOptionsValidator

### Community 302 - "ProblemDetailsExtensions.cs"
Cohesion: 0.40
Nodes (3): ProblemDetails, ProblemDetailsExtensions, ICollection

### Community 303 - "DefaultResponsesOperationFilter"
Cohesion: 0.70
Nodes (3): OpenApiOperation, OperationFilterContext, DefaultResponsesOperationFilter

### Community 304 - "JobStatus"
Cohesion: 0.33
Nodes (5): JobStatus, Failed, Queued, Running, Succeeded

### Community 305 - ".EmailVerificationTokenValidation"
Cohesion: 0.50
Nodes (3): IResxLocalizer, IRuleBuilder, IRuleBuilderOptions

### Community 306 - "Products/v1/Get/Request.cs"
Cohesion: 0.40
Nodes (4): Products.Endpoints.Products.v1.Get, Request, Id, RequestValidator

### Community 307 - "SignalROptions"
Cohesion: 0.40
Nodes (5): SignalROptions, RedisConnectionString, RequireRedisBackplaneInProduction, UseRedisBackplane, SignalROptionsValidator

### Community 308 - "PublishOutcome"
Cohesion: 0.50
Nodes (4): PublishOutcome, Failed, Published, Skipped

### Community 310 - ".WriteProblemAsync"
Cohesion: 0.33
Nodes (5): HttpContext, HttpStatusCode, IProblemDetailsService, IResxLocalizer, Task

### Community 311 - "StronglyTypedIdBinder"
Cohesion: 0.40
Nodes (4): IModelBinder, ModelBindingContext, StronglyTypedIdBinder, Task

### Community 312 - ".DeactivateStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 313 - ".RemoveProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 314 - "ModulesOptions.cs"
Cohesion: 0.50
Nodes (4): ModulesOptions, EnabledModules, ModulesOptionsValidator, IReadOnlyList

### Community 318 - "SecurityHeadersOptions.cs"
Cohesion: 0.50
Nodes (4): SecurityHeadersOptions, Headers, SecurityHeadersOptionsValidator, Dictionary

### Community 319 - "Products/v1/Update/Request.cs"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Products.v1.Update, RequestBody, Request, Body, Id, RequestBody, Description, Name (+3 more)

### Community 322 - "VerifyEmail/Request.cs"
Cohesion: 0.50
Nodes (4): Request, Email, Otp, RequestValidator

### Community 323 - ".PhoneNumberValidation"
Cohesion: 0.50
Nodes (3): IResxLocalizer, IRuleBuilder, IRuleBuilderOptions

### Community 324 - "RequestBody"
Cohesion: 0.40
Nodes (5): RequestBody, Description, Name, Price, Quantity

### Community 325 - "InventoryTelemetry"
Cohesion: 0.50
Nodes (4): ActivitySource, Counter, Meter, InventoryTelemetry

### Community 327 - "ProductsTelemetry"
Cohesion: 0.50
Nodes (4): ActivitySource, Counter, Meter, ProductsTelemetry

## Knowledge Gaps
- **951 isolated node(s):** `Id`, `IdAsString`, `Roles`, `SessionId`, `JwtClaimNames` (+946 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 2224 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **100 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `CorsOptions`, `ApplySearchLanguageInterceptor`, `ResxLocalizationOptions`, `AuditLogOptions`, `Common.Domain.ResultMonad`, `EmailOptions`, `BackgroundJobsOptions`, `OutboxOptions`, `Setup.Logger.cs`, `DevicesOptions`, `OtpOptions`, `ObservabilityOptions`, `ConfigureSwaggerOptions.cs`, `KeycloakOptions`, `JobHousekeepingOptions`, `system_diagnostics`, `CachingOptions`, `SignalROptions`, `SmsOptions`, `IdentityScheme`, `ModulesOptions.cs`, `SecurityHeadersOptions.cs`, `RequestLoggingOptions`, `FullTextSearchOptions`, `Inventory.Domain.StockReservations`, `microsoft_entityframeworkcore`, `CaptchaOptions`, `fluentvalidation`, `PushOptions`, `Program.cs`, `Infrastructure/Setup.cs`, `FirebasePushGateway.cs`, `ResiliencyOptions`, `JwtBearerConfigureOptions.cs`, `Common.InterModuleRequests.Contracts`, `microsoft_extensions_configuration`, `RabbitMqOptions`, `CustomValidator`, `OpenApiOptions`, `InventoryOptions`, `.AddCustomHealthChecks`?**
  _High betweenness centrality (0.164) - this node is a cross-community bridge._
- **Why does `Result` connect `Result` to `.ReserveSeriesAsync`, `.ListSessions`, `FirebasePushGateway`, `Error`, `ProductTemplate`, `ApplicationUserId`, `.SaveChangesAsync`, `.SendAsync`, `.DeactivateProductTemplateAsync`, `Response`, `Product`, `.RegisterAsync`, `StockReservation`, `.RegisterAsync`, `Common.Domain.StronglyTypedIds`, `EmailMessage`, `ICurrentUser`, `ISmsGateway`, `.AssignBasicRoleOrRollbackAsync`, `.CommitStockReservationAsync`, `.SingleAsResult`, `.RefreshToken`, `IEmailGateway`, `DummySmsGateway`, `.CreateProductTemplateAsync`, `ResultTelemetryExtensions`, `.CreateTokens`, `PaginationQueryableExtensions`, `PaginationResponse`, `PushMessage`, `ReCaptchaService`, `.SendOtp`, `BrevoEmailGateway`, `.DeactivateStoreAsync`, `.RemoveProductAsync`, `.SearchProductTemplatesAsync`, `.RequestTokensAsync`, `.SendCoreAsync`, `.UpdateMyStoreAsync`, `.ReserveStockAsync`, `Response`, `Response`, `.RemoveMyProductAsync`, `DomainEvent`, `.SearchStoresAsync`, `ICaptchaService`, `IProductsDbContext`, `.SendOtp`, `.GetProductAsync`, `.UpdateCurrentPushToken`, `.SearchMyProductsAsync`, `NetGsmSmsGateway`, `.SearchStoreProductsAsync`, `HttpWarehouseGateway`, `.IsRegisteredAsync`, `.ReleaseStockReservationAsync`, `V1StockReservationCommitConflictDetectedDomainEvent`, `Response`, `InventoryOptions`, `Response`, `.TapWhenFeatureEnabledAsync`, `Response`?**
  _High betweenness centrality (0.147) - this node is a cross-community bridge._
- **Why does `ApplicationUserId` connect `ApplicationUserId` to `KeycloakTokenClient`, `NotificationPayload`, `KeycloakAdminClient`, `.ListSessions`, `IStronglyTypedId`, `KeycloakPermissionAuthorizationHandler`, `Response`, `.RegisterAsync`, `.RegisterAsync`, `DeviceRegistration`, `ICurrentUser`, `AuditableEntity`, `.AssignBasicRoleOrRollbackAsync`, `JobRow`, `IInterModuleRequest`, `For`, `CurrentUser`, `SendSecurityAlertRequestHandler`, `VersionNeutral/Get/Request.cs`, `IntegrationEvent`, `PaginationResponse`, `V1StoreCreatedDomainEvent`, `Response`, `.RequestTokensAsync`, `Seeder`, `.HandleAsync`, `Response`, `IAuditableEntity`, `.SearchStoresAsync`, `.Configure`, `Store`, `IProductsDbContext`, `Infrastructure/Setup.cs`, `Request`, `InterModuleRequestHandler`, `AuditableEntityResponse`, `StockLevel`, `Response`, `Response`?**
  _High betweenness centrality (0.084) - this node is a cross-community bridge._
- **What connects `Id`, `IdAsString`, `Roles` to the rest of the system?**
  _951 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `.ReserveSeriesAsync` be split into smaller, more focused modules?**
  _Cohesion score 0.10869565217391304 - nodes in this community are weakly interconnected._
- **Should `KeycloakAdminClient` be split into smaller, more focused modules?**
  _Cohesion score 0.1406423034330011 - nodes in this community are weakly interconnected._
- **Should `RedisFixedWindowRateLimiter` be split into smaller, more focused modules?**
  _Cohesion score 0.13333333333333333 - nodes in this community are weakly interconnected._