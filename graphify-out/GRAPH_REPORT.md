# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-09-28)

## Corpus Check
- 585 files · ~94,061 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 5051 nodes · 10048 edges · 404 communities (303 shown, 101 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 295 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `bafe0333`
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
- ApplySearchLanguageInterceptor
- FirebasePushGateway
- .RequireOtpTemplateForDefaultCulture
- Error
- Common.Domain.ResultMonad
- IKeycloakAdminClient
- ISearchLanguageResolver
- Common.Application.Options
- KeycloakPermissionAuthorizationHandler
- EmailOptions
- Products.Domain.Products.DomainEvents.v1
- IssueVerificationTokenRequestHandler
- Product
- LogProductCatalogChangeHandler
- BoundedRequestCaptureStream
- DeviceRegistration
- DummyEmailGateway
- .RevokeToken
- Common.Infrastructure.Extensions
- V1StoreCreatedDomainEvent
- RequestResponseBodyLoggingMiddleware
- fluentvalidation
- Result
- .UpsertIfNewerAsync
- NotificationsModule
- .NotFound
- .RefreshToken
- KeycloakPermissionClient
- OutboxMessage
- CustomRateLimitingOptions
- UserRepresentation
- RequestBody
- ObservabilityOptions
- ConfigureSwaggerOptions.cs
- Request
- .SaveWithOutboxAsync
- CreateStoreRateLimitingPolicy
- KeycloakOptions
- .AddKeycloakInfrastructure
- PaginationQueryableExtensions
- Outbox Misuse Check
- Common.Domain.StronglyTypedIds
- Add Integration Event Command
- Response
- ProductsDbContext
- ReCaptchaService
- Request
- SendEmailOtpRequestHandler
- BrevoEmailGateway
- ProjectionReconciliationJob
- Cross-Module Reference Violation
- OutboxMetricsJob
- .SearchProductTemplatesAsync
- Bogus Test Data
- KeycloakTokenClient
- IDbContext
- RequestLoggingOptions
- .SendCoreAsync
- BackgroundJobsService
- OutboxModule
- .AddNotificationsSignalR
- ValueObject
- Common.Domain.Events
- IntegrationEventHandlerBase
- FullTextSearchOptions
- Response
- .WriteAsync
- AsNoTracking Coverage Check
- Setup
- Response
- AuditLogRetentionService
- CheckRegistrationRateLimitingPolicy
- AggregateRoot
- .UpdateCurrentPushToken
- .SearchStoresAsync
- Split-Deployment PoC
- ICaptchaService
- .Configure
- StockReservation
- Store
- PushOptions
- Configuration-Driven Module Registration
- Setup
- .GetProductAsync
- ProductId
- ResultToResponseTransformer.cs
- .GetClientKey
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
- InterModuleRequestOptions
- StockLevel
- .IsRegisteredAsync
- KeycloakPermissionPolicyProvider
- .ReleaseStockReservationAsync
- IOutboxDbContext
- S3PrivateObjectStore
- Response
- SkipOverlappingRecurringJobFilter
- .AddAuthInfrastructure
- Response
- IModule
- ProductsModule
- InventoryOptions
- .GetVariantAsync
- OtpVerifyRateLimitingPolicy
- NotificationsHub
- .AddCustomHealthChecks
- IEvent
- GlobalExceptionHandlingMiddleware
- Response
- EmailRateLimitingPolicy
- StoreId
- docker-compose.yml (Base Stack)
- .AddInfrastructure
- ObjectStorageOptions
- Consumer Idempotency (IntegrationEventHandlerBase)
- SendRequestBody
- .HandleAsync
- .AssignBasicRoleOrRollbackAsync
- .WriteTooManyRequestsToResponse
- IPublicObjectStore
- ProductTemplateId
- ProcessedMessage
- IStronglyTypedId
- TokenRefreshRateLimitingPolicy
- Setup
- Response
- JobRow
- .RegisterAsync
- .AddServices
- IInterModuleRequestClient
- Stores/v1/Update/Request.cs
- StockReservationExpirySweepJobRegistrar
- OutboxOptions
- .EnsureNoMigrationsPending
- ConfigureSwaggerOptions
- Setup.Logger.cs
- ISmsGateway
- Request
- CreateStockLevelOnProductCreatedHandler
- OtpOptions
- InventoryDbContext
- OtpServiceBase
- IntegrationEventOutbox
- IEmailGateway
- For
- NotificationsDbContext
- ApplicationUserId
- IamModule
- Common.Application.Pagination
- Response
- SendSecurityAlertRequestHandler
- Notifications.Application/IAssemblyReference.cs
- S3ObjectStoreCore.cs
- system_globalization
- S3ObjectStoreCore
- system_diagnostics
- PaginationResponse
- .FixedWindow
- CorsOptions
- .AddPushServices
- CachingOptions
- SmsOptions
- RabbitMqOptions
- IAM.Domain
- SendRequestBody
- RedisOtpService
- IdentityScheme
- .Get
- ReverseProxyOptions
- BoundedCaptureStream
- JobClaimExtensions
- ServiceAccountTokenCache
- DeviceRegistryReconcileJobRegistrar
- system_linq_expressions
- .GetMeAsync
- EventDispatcher
- IInventoryDbContext
- TokenCreateRateLimitingPolicy
- .UseModules
- InventoryModule
- ReCaptchaResponse
- NotificationsTelemetry
- KeycloakScopes
- .Configure
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
- ProjectionReconciliationOptions.cs
- Response
- .AddObservability
- FeatureFlags
- .SendOtp
- IBackgroundJobs
- HttpWarehouseGateway
- Keycloak realm as code
- .ListSessions
- Request
- Endpoint
- IInboxStore
- NetGsmSmsGateway
- RegisterRateLimitingPolicy
- .AddCommonCaching
- .AddCommonOptions
- InterModuleRequestHandler
- BackgroundJobsModule
- S3PublicObjectStore
- .HasPatternIndex
- KeycloakPermissionAuthorizationHandler.cs
- AuditLogEntryConfiguration
- .SendAsync
- ProblemDetailsExtensions.cs
- .UseInfrastructure
- KeycloakTokenClient.cs
- .Apply
- InterModuleRequestHandlerDefinition
- Response
- CustomValidator
- KeyedResilienceProfile
- .AddProductAsync
- Request
- OpenApiOptions
- SendErrorBody
- DeviceRegistryReconciliationService
- Response
- Response
- .TryWriteAsync
- Products.Domain/IAssemblyReference.cs
- IAM.Application/IAssemblyReference.cs
- Products.Infrastructure/IAssemblyReference.cs
- Products.Application/IAssemblyReference.cs
- Common.Infrastructure/IAssemblyReference.cs
- Notifications.Domain/IAssemblyReference.cs
- IAM.Infrastructure/IAssemblyReference.cs
- Common.InterModuleRequests/Setup.cs
- AuditableEntity
- .SendWithPipelineAsync
- Response
- .CreatePresignedUrl
- .AddProductToMyStoreAsync
- SwaggerDefaultValues
- AuditLogOptions
- DevicesOptions
- IProductsDbContext
- ObjectListPage
- InventoryTelemetry
- .AddResilientHttpClient
- KeycloakPermission
- AuditLogEntry
- BaseDbContext
- ResxLocalizationOptions
- SendResponseBody
- .TapWhenFeatureEnabledAsync
- Response
- SendForEmail/Request.cs
- .TryReadFromJsonAsync
- JobHousekeepingOptions
- FirebasePushGateway.cs
- RequestBody
- RequestBodyLimitMetadata
- .HandleAsync
- IOtpService
- DummySmsGateway
- AuditableEntityResponse
- UploadObjectRequest
- StronglyTypedIdBinder.cs
- IAuditableEntity
- .MapEndpoints
- .SendOtp
- Infrastructure/StringExtensions.cs
- SecurityHeadersOptions.cs
- .SetConcurrency
- .AddCustomSwagger
- Endpoint
- ResultTelemetryExtensions
- CachedCaptchaService
- .EmailVerificationTokenValidation
- DefaultResponsesOperationFilter
- .SetRetryAfterHeader
- StronglyTypedIdSchemaFilter.cs
- .PhoneNumberValidation
- SignalROptions
- PublishOutcome
- .Capture
- RequestBodyLimitFilter
- microsoft_aspnetcore_mvc
- DeviceRegistrationId
- Response
- Versioning/Setup.cs
- Setup
- Setup
- StringExtensions
- Products.Endpoints
- Products/v1/Update/Request.cs
- Notifications.Infrastructure
- ResultTelemetryExtensions.cs
- .CreateStorePolicy
- .AddBrevo
- Products/v1/My/Update/Request.cs
- .AddNetGsm
- MassTransitInterModuleRequestClient
- EnrichLogsWithUserInfoMiddleware
- .ActivateProductTemplateAsync
- ProductTemplates/v1/Create/Request.cs
- .UpdateMyStoreAsync
- .RemoveProductAsync
- How it works
- Setup.Observability.cs
- JobStatus
- .AddCommonObjectStorage
- RequestBodyLimitFilter.cs
- DatabaseOptions.cs
- ValidationContextExtensions
- IAM.Endpoints
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
1. `Result` - 144 edges
2. `Common.Application.Options` - 139 edges
3. `Common.Domain.ResultMonad` - 105 edges
4. `CustomValidator` - 83 edges
5. `ApplicationUserId` - 75 edges
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

## Communities (404 total, 101 thin omitted)

### Community 0 - ".ReserveSeriesAsync"
Cohesion: 0.12
Nodes (16): Inventory.Endpoints.StockReservations.v1.ReserveSeries, GetProductRequest, GetProductResponse, DefaultIdType, CancellationToken, IOptions, RouteGroupBuilder, Task (+8 more)

### Community 1 - "NotificationPayload"
Cohesion: 0.18
Nodes (13): Notifications.Application.Hubs, IHubContext, CancellationToken, IReadOnlyList, Task, INotificationDispatcher, Task, INotificationsClient (+5 more)

### Community 2 - "OutboxProcessor"
Cohesion: 0.21
Nodes (12): IPublishEndpoint, PublishOutcome, CancellationToken, Exception, ILogger, IOptions, IServiceScopeFactory, LoggerMessage (+4 more)

### Community 3 - "Common.Application.DTOs"
Cohesion: 0.17
Nodes (6): Products.Endpoints.Products.v1.My.Get, Products.Endpoints.ProductTemplates.v1.Search, Products.Endpoints.Products.v1.Search, Common.Application.DTOs, Products.Endpoints.Products.v1.My.Search, Products.Endpoints.Products.v1.Get

### Community 5 - "KeycloakAdminClient"
Cohesion: 0.15
Nodes (20): DateOnly, DateTimeOffset, CreateKeycloakUser, KeycloakUser, KeycloakUserPage, KeycloakUserSession, CancellationToken, Func (+12 more)

### Community 6 - "RedisFixedWindowRateLimiter"
Cohesion: 0.13
Nodes (16): RateLimiter, RateLimiterStatistics, FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration, CancellationToken (+8 more)

### Community 7 - "ApplySearchLanguageInterceptor"
Cohesion: 0.18
Nodes (10): SaveChangesInterceptor, ApplyAuditingInterceptor, CancellationToken, DbContextEventData, InterceptionResult, TimeProvider, ValueTask, ApplySearchLanguageInterceptor (+2 more)

### Community 8 - "FirebasePushGateway"
Cohesion: 0.17
Nodes (12): FirebaseApp, FirebaseMessaging, IDisposable, CancellationToken, Exception, IEnumerable, ILogger, IReadOnlyList (+4 more)

### Community 9 - ".RequireOtpTemplateForDefaultCulture"
Cohesion: 0.40
Nodes (4): Func, IConfiguration, IEnumerable, OtpTemplateConfiguration

### Community 10 - "Error"
Cohesion: 0.07
Nodes (24): Error, Key, ParameterName, StatusCode, SubErrors, Value, HttpStatusCode, ICollection (+16 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.08
Nodes (36): Products.Infrastructure.InterModuleRequestHandlers, Common.Application.Search, Inventory.Infrastructure.InterModuleRequestHandlers, Products.Endpoints.Stores, Products.Endpoints.Stores.v1.Search, Inventory.Application.Persistence, Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions (+28 more)

### Community 12 - "IKeycloakAdminClient"
Cohesion: 0.20
Nodes (8): CancellationToken, IReadOnlyList, Task, IKeycloakAdminClient, CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 13 - "ISearchLanguageResolver"
Cohesion: 0.12
Nodes (14): Configuration, ISearchLanguageResolver, UniversalConfig, SearchLanguageResolver, UniversalConfig, IOptions, Setup, IApplicationBuilder (+6 more)

### Community 14 - "Common.Application.Options"
Cohesion: 0.04
Nodes (61): Common.Infrastructure.Modules, Notifications.Application.Sms, Notifications.Infrastructure.Devices, Common.Application.Persistence.Inbox, Inventory.Endpoints, Common.Endpoints.Versioning, Notifications.Infrastructure.Email, Products.Endpoints.Probe (+53 more)

### Community 15 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.13
Nodes (17): AuthorizationHandler, AuthorizationHandlerContext, IAuthorizationRequirement, CancellationToken, ClaimsPrincipal, HttpContext, IFusionCache, IHttpContextAccessor (+9 more)

### Community 16 - "EmailOptions"
Cohesion: 0.09
Nodes (24): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+16 more)

### Community 17 - "Products.Domain.Products.DomainEvents.v1"
Cohesion: 0.10
Nodes (13): Products.Domain.Products.DomainEvents.v1, ProductId, V1ProductDescriptionUpdatedDomainEvent, ProductId, V1ProductNameUpdatedDomainEvent, ProductId, V1ProductPriceDecreasedDomainEvent, ProductId (+5 more)

### Community 18 - "IssueVerificationTokenRequestHandler"
Cohesion: 0.39
Nodes (6): IssueVerificationTokenRequest, IssueVerificationTokenResponse, CancellationToken, IOptions, Task, IssueVerificationTokenRequestHandler

### Community 19 - "Product"
Cohesion: 0.09
Nodes (22): Product, Description, Language, Name, Price, ProductTemplate, ProductTemplateId, Quantity (+14 more)

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

### Community 25 - "Common.Infrastructure.Extensions"
Cohesion: 0.13
Nodes (18): IAM.Endpoints.Captcha.VersionNeutral, Common.Infrastructure.RateLimiting, Products.Infrastructure.RateLimiting, Common.Infrastructure.Extensions, IAM.Endpoints.Otp.VersionNeutral, IAM.Infrastructure.RateLimiting, IAM.Infrastructure.Captcha, IAM.Endpoints.Tokens.VersionNeutral (+10 more)

### Community 26 - "V1StoreCreatedDomainEvent"
Cohesion: 0.11
Nodes (16): DomainEventHandlerBase, CancellationToken, Task, IEventHandler, CancellationToken, Task, IEventHandlerWrapper, CancellationToken (+8 more)

### Community 27 - "RequestResponseBodyLoggingMiddleware"
Cohesion: 0.22
Nodes (7): IDiagnosticContext, HttpContext, IList, IOptions, PathString, RequestDelegate, RequestResponseBodyLoggingMiddleware

### Community 28 - "fluentvalidation"
Cohesion: 0.08
Nodes (32): common_application_localization_resources, IAM.Domain.Users, Products.Endpoints.Probe.v1, Common.Domain.Extensions, Notifications.Infrastructure.Devices.UpdateCurrentPushToken, Common.Application.Validation, Common.Domain.Devices, IAM.Endpoints.Common.Validations (+24 more)

### Community 29 - "Result"
Cohesion: 0.11
Nodes (16): Result, Error, IsFailure, Success, Value, Func, Task, AsyncExtensions (+8 more)

### Community 30 - ".UpsertIfNewerAsync"
Cohesion: 0.13
Nodes (13): ICurrentDbContext, ProjectionUpsertExtensions, Action, CancellationToken, DbContext, DbSet, Func, Task (+5 more)

### Community 31 - "NotificationsModule"
Cohesion: 0.15
Nodes (10): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, NotificationsModule, ActivitySourceNames, MeterNames (+2 more)

### Community 32 - ".NotFound"
Cohesion: 0.16
Nodes (10): CollectionExtensions, Func, ICollection, IEnumerable, PersistenceQueryableExtensions, CancellationToken, Expression, Func (+2 more)

### Community 33 - ".RefreshToken"
Cohesion: 0.15
Nodes (11): IAM.Endpoints.Tokens.VersionNeutral.Refresh, CancellationToken, RouteGroupBuilder, Task, Endpoint, DateTimeOffset, Response, AccessToken (+3 more)

### Community 34 - "KeycloakPermissionClient"
Cohesion: 0.13
Nodes (20): CancellationToken, Dictionary, HttpClient, HttpResponseMessage, ILogger, IOptions, IReadOnlyList, List (+12 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.10
Nodes (18): OutboxMessage, CreatedOn, Event, FailedOn, Id, IsProcessed, NextRetryAt, ParentSpanId (+10 more)

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

### Community 45 - ".AddKeycloakInfrastructure"
Cohesion: 0.22
Nodes (8): IServiceAccountTokenProvider, HttpClient, IOptions, TimeProvider, ServiceAccountTokenProvider, IOptions, IServiceCollection, Setup

### Community 46 - "PaginationQueryableExtensions"
Cohesion: 0.08
Nodes (27): BinaryExpression, ExpressionVisitor, KeyedRow, MemberExpression, MethodInfo, ParameterExpression, Payload, PaginationCursor (+19 more)

### Community 48 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.06
Nodes (33): Common.Infrastructure.Persistence.Inbox, Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Common.Domain.StronglyTypedIds, Common.Application.Persistence, Common.Application.Persistence.Projections, Inventory.Infrastructure.Persistence.EntityConfigurations, Common.Application.Jobs (+25 more)

### Community 50 - "Response"
Cohesion: 0.14
Nodes (13): IAM.Endpoints.Users.VersionNeutral.Search, DateOnly, DateTimeOffset, Response, BirthDate, CreatedOn, Email, Enabled (+5 more)

### Community 51 - "ProductsDbContext"
Cohesion: 0.15
Nodes (12): DbContextOptions, DbSet, ILogger, TimeProvider, ProductsDbContext, Products, ProductTemplates, Stores (+4 more)

### Community 52 - "ReCaptchaService"
Cohesion: 0.21
Nodes (10): FormUrlEncodedContent, ReCaptchaResponse, CancellationToken, Exception, HttpClient, ILogger, IOptions, LoggerMessage (+2 more)

### Community 53 - "Request"
Cohesion: 0.15
Nodes (13): Guid, Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FirstName (+5 more)

### Community 54 - "SendEmailOtpRequestHandler"
Cohesion: 0.21
Nodes (11): EmailOtpDispatchErrors, EmailOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, SendEmailOtpRequest, SendEmailOtpResponse, IFusionCache (+3 more)

### Community 55 - "BrevoEmailGateway"
Cohesion: 0.23
Nodes (10): Exception, HttpClient, HttpStatusCode, ILogger, IOptions, JsonSerializerOptions, LoggerMessage, BrevoEmailGateway (+2 more)

### Community 56 - "ProjectionReconciliationJob"
Cohesion: 0.29
Nodes (8): ProjectionReconciliationJob, CancellationToken, IDbContext, ILogger, IOptions, IReadOnlyList, LoggerMessage, Task

### Community 58 - "OutboxMetricsJob"
Cohesion: 0.13
Nodes (14): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxMetricsJob (+6 more)

### Community 59 - ".SearchProductTemplatesAsync"
Cohesion: 0.15
Nodes (11): LikePattern, CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response (+3 more)

### Community 61 - "KeycloakTokenClient"
Cohesion: 0.10
Nodes (26): JsonWebTokenHandler, CancellationToken, Task, IKeycloakTokenClient, DateTimeOffset, KeycloakTokens, CancellationToken, IFeatureManager (+18 more)

### Community 62 - "IDbContext"
Cohesion: 0.15
Nodes (14): ChangeTracker, DatabaseFacade, JobHousekeepingJob, CancellationToken, ILogger, IOptions, LoggerMessage, Task (+6 more)

### Community 63 - "RequestLoggingOptions"
Cohesion: 0.11
Nodes (20): IPostConfigureOptions, RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes (+12 more)

### Community 64 - ".SendCoreAsync"
Cohesion: 0.19
Nodes (9): SendErrorBody, SendResponseBody, CancellationToken, HttpResponseMessage, SendRequestBody, Task, SendContact, Email (+1 more)

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

### Community 69 - "Common.Domain.Events"
Cohesion: 0.05
Nodes (41): Inventory.Domain.StockReservations.DomainEvents.v1, Common.Domain.Events, DomainEvent, CreatedOn, Id, Version, DateTimeOffset, DefaultIdType (+33 more)

### Community 70 - "IntegrationEventHandlerBase"
Cohesion: 0.22
Nodes (12): IConsumer, IntegrationEventHandlerBase, MaxEventAge, CancellationToken, ConsumeContext, DefaultIdType, IFusionCache, ILogger (+4 more)

### Community 71 - "FullTextSearchOptions"
Cohesion: 0.12
Nodes (19): Add a new language/culture, Add search to a new entity _(Build checklist)_, Change the vector (weights, fields, or config), Extending and maintaining, Full-Text Search, Gotchas, Non-goals, Per-entity strategy (+11 more)

### Community 72 - "Response"
Cohesion: 0.18
Nodes (9): Products.Endpoints.ProductTemplates.v1.Get, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Brand, Color (+1 more)

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

### Community 79 - "AggregateRoot"
Cohesion: 0.15
Nodes (11): AggregateRoot, Events, Id, Version, IReadOnlyCollection, List, IAggregateRoot, Events (+3 more)

### Community 80 - ".UpdateCurrentPushToken"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 81 - ".SearchStoresAsync"
Cohesion: 0.15
Nodes (12): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Address (+4 more)

### Community 82 - "Split-Deployment PoC"
Cohesion: 0.25
Nodes (6): Concurrent safety, Files added by this PoC, How to run, Split-Deployment PoC, What this proves, README (Boilerplate Overview)

### Community 83 - "ICaptchaService"
Cohesion: 0.36
Nodes (5): ICaptchaService, DummyCaptchaService, IConfiguration, IServiceCollection, Setup

### Community 84 - ".Configure"
Cohesion: 0.17
Nodes (12): AuditableEntityConfiguration, EntityTypeBuilder, JobRowConfiguration, EntityTypeBuilder, StronglyTypedIdValueConverter, DefaultIdType, EntityTypeBuilder, NpgsqlTsVector (+4 more)

### Community 85 - "StockReservation"
Cohesion: 0.11
Nodes (16): Inventory.Domain.StockReservations.Errors, StockReservationErrors, DateTimeOffset, DefaultIdType, StockReservation, LastReleaseAttemptReference, ProductId, ProviderReference (+8 more)

### Community 86 - "Store"
Cohesion: 0.08
Nodes (20): File map _(Build)_, ISearchLocalized, Language, StoreId, V1StoreDescriptionUpdatedDomainEvent, StoreId, V1StoreNameUpdatedDomainEvent, IReadOnlyCollection (+12 more)

### Community 87 - "PushOptions"
Cohesion: 0.11
Nodes (21): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri, Type (+13 more)

### Community 89 - "Setup"
Cohesion: 0.33
Nodes (4): ConfigurationManager, Host.Configurations, Setup, WebApplicationBuilder

### Community 90 - ".GetProductAsync"
Cohesion: 0.25
Nodes (10): GetStockLevelRequest, GetStockLevelResponse, DefaultIdType, CancellationToken, Task, GetStockLevelRequestHandler, CancellationToken, DefaultIdType (+2 more)

### Community 91 - "ProductId"
Cohesion: 0.11
Nodes (17): Products.Endpoints.Stores.v1.My.RemoveProduct, Products.Endpoints.Stores.v1.RemoveProduct, DefaultIdType, ProductId, Request, Id, RequestValidator, Request (+9 more)

### Community 92 - "ResultToResponseTransformer.cs"
Cohesion: 0.07
Nodes (32): AspNetResult, Common.Application.EndpointFilters, IEndpointFilter, IFeatureManagerSnapshot, microsoft_aspnetcore_hosting, microsoft_aspnetcore_http_iresult, microsoft_extensions_localization, ResxLocalizer (+24 more)

### Community 93 - ".GetClientKey"
Cohesion: 0.17
Nodes (7): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, RouteGroupBuilder, Endpoint, Response, ClientKey, RouteGroupBuilder, Setup

### Community 94 - "KeyedResiliencePipelines"
Cohesion: 0.14
Nodes (13): HttpRequestException, ResiliencePipelineBuilder, ResiliencePipelineRegistry, KeyedResiliencePipelines, HttpResponseMessage, IConnectionMultiplexer, ILoggerFactory, IOptions (+5 more)

### Community 95 - ".SearchMyProductsAsync"
Cohesion: 0.17
Nodes (11): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Description (+3 more)

### Community 96 - "DatabaseSeederOrchestrator"
Cohesion: 0.07
Nodes (26): BackgroundService, IDatabaseSeeder, Priority, CancellationToken, Task, DatabaseSeederOrchestrator, CancellationToken, Exception (+18 more)

### Community 97 - "StockReservationExpirySweepService"
Cohesion: 0.20
Nodes (10): EntityEntry, CancellationToken, Exception, Guid, ILogger, IOptions, LoggerMessage, Task (+2 more)

### Community 98 - ".SearchStoreProductsAsync"
Cohesion: 0.17
Nodes (11): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Description (+3 more)

### Community 99 - "AuditLogRetentionJobRegistrar"
Cohesion: 0.22
Nodes (9): AuditLogRetentionJobRegistrar, CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task, Setup (+1 more)

### Community 100 - "OutboxDbContext"
Cohesion: 0.13
Nodes (12): DbContext, IEntityTypeConfiguration, IConfiguration, IServiceCollection, NpgsqlDataSource, DbContextOptions, DbSet, ModelBuilder (+4 more)

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
Nodes (27): Notifications.Application.Otp, IAM.Endpoints.Users, IAM.Endpoints.Otp.VersionNeutral.SendForLogin, Common.Application.Caching, Common.Application.FeatureManagement, IAM.Endpoints.Otp, Notifications.Application.Persistence, Notifications.Infrastructure.Telemetry (+19 more)

### Community 107 - "InterModuleRequestOptions"
Cohesion: 0.20
Nodes (9): InterModuleRequestOptions, DependencyUnavailableRetryAfterSeconds, HandlerConcurrentMessageLimit, HandlerPrefetchCount, Timeouts, TimeoutSeconds, InterModuleRequestOptionsValidator, Dictionary (+1 more)

### Community 108 - "StockLevel"
Cohesion: 0.10
Nodes (12): Inventory.Domain.StockLevels.DomainEvents.v1, StronglyTypedIdHelper, DefaultIdType, StockLevelId, V1StockLevelCreatedDomainEvent, DefaultIdType, StockLevel, ProductId (+4 more)

### Community 109 - ".IsRegisteredAsync"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, IsRegistered

### Community 110 - "KeycloakPermissionPolicyProvider"
Cohesion: 0.24
Nodes (8): AuthorizationOptions, AuthorizationPolicy, DefaultAuthorizationPolicyProvider, IAuthorizationPolicyProvider, ConcurrentDictionary, IOptions, Task, KeycloakPermissionPolicyProvider

### Community 111 - ".ReleaseStockReservationAsync"
Cohesion: 0.13
Nodes (14): CancellationToken, Task, IWarehouseGateway, CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint (+6 more)

### Community 112 - "IOutboxDbContext"
Cohesion: 0.15
Nodes (13): IOutboxDbContext, OutboxMessages, CancellationToken, DbSet, Task, CancellationToken, ILogger, IOptions (+5 more)

### Community 113 - "S3PrivateObjectStore"
Cohesion: 0.16
Nodes (12): IPrivateObjectStore, CancellationToken, IReadOnlyCollection, Stream, Task, S3PrivateObjectStore, CancellationToken, IReadOnlyCollection (+4 more)

### Community 114 - "Response"
Cohesion: 0.11
Nodes (17): IAM.Endpoints.Users.VersionNeutral.Me.Get, RouteGroupBuilder, Endpoint, DateOnly, DateTimeOffset, IReadOnlyCollection, Response, BirthDate (+9 more)

### Community 115 - "SkipOverlappingRecurringJobFilter"
Cohesion: 0.22
Nodes (7): hangfire_server, hangfire_storage, SkipOverlappingRecurringJobFilter, ILogger, LoggerMessage, PerformedContext, PerformingContext

### Community 116 - ".AddAuthInfrastructure"
Cohesion: 0.12
Nodes (16): IAllowAnonymous, IAuthorizationHandler, IConfigureNamedOptions, HttpContext, HttpStatusCode, IOptions, IProblemDetailsService, IResxLocalizer (+8 more)

### Community 117 - "Response"
Cohesion: 0.09
Nodes (20): Inventory.Endpoints.StockReservations.v1.Get, ReservationStatus, Active, Committed, Expired, Released, RequiresReconciliation, CancellationToken (+12 more)

### Community 118 - "IModule"
Cohesion: 0.12
Nodes (13): IModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority, Action, IApplicationBuilder (+5 more)

### Community 119 - "ProductsModule"
Cohesion: 0.12
Nodes (13): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, ProductsModule (+5 more)

### Community 120 - "InventoryOptions"
Cohesion: 0.08
Nodes (25): IHeaderDictionary, IValidator, InventoryOptions, MaxOccurrencesPerSeries, SafetyBufferQuantity, StockReservationExpirySweepBatchSize, StockReservationExpirySweepCron, WarehouseGatewayBaseUrl (+17 more)

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

### Community 125 - "IEvent"
Cohesion: 0.14
Nodes (12): IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, DateTimeOffset, IEvent (+4 more)

### Community 126 - "GlobalExceptionHandlingMiddleware"
Cohesion: 0.27
Nodes (10): Exception, HttpContext, ILogger, IOptions, IProblemDetailsService, IResxLocalizer, LoggerMessage, RequestDelegate (+2 more)

### Community 127 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 128 - "EmailRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, EmailRateLimitingPolicy (+1 more)

### Community 129 - "StoreId"
Cohesion: 0.08
Nodes (22): Products.Infrastructure.Persistence.Seeding, Products.Application.Products.DomainEventHandlers.v1, CancellationToken, Task, ProductCreatedIntegrationEventPublishingHandler, V1ProductCreatedDomainEventHandlers, ProductId, V1ProductCreatedDomainEvent (+14 more)

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.48
Nodes (7): Observability (OpenTelemetry), docker-compose.yml (Base Stack), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), Postgres Service (mm.postgres), RabbitMQ Service (mm.rabbitmq), Redis Service (mm.redis)

### Community 131 - ".AddInfrastructure"
Cohesion: 0.29
Nodes (7): HostOptions, JsonOptions, Assembly, IConfiguration, IResxLocalizer, IServiceCollection, IWebHostEnvironment

### Community 132 - "ObjectStorageOptions"
Cohesion: 0.11
Nodes (19): ObjectStorageOptions, AccessKey, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, ForcePathStyle (+11 more)

### Community 134 - "SendRequestBody"
Cohesion: 0.25
Nodes (8): SendContact, IReadOnlyList, SendRequestBody, HtmlContent, Sender, Subject, TextContent, To

### Community 135 - ".HandleAsync"
Cohesion: 0.14
Nodes (14): Cross-process call path, How it works, ICoreModule, GetSeedUserIdsRequest, GetSeedUserIdsResponse, ICollection, CancellationToken, Task (+6 more)

### Community 136 - ".AssignBasicRoleOrRollbackAsync"
Cohesion: 0.14
Nodes (10): CancellationToken, Exception, ILogger, LoggerMessage, Task, RegistrationCompletion, ActivitySource, Counter (+2 more)

### Community 137 - ".WriteTooManyRequestsToResponse"
Cohesion: 0.13
Nodes (14): PartitionedRateLimiter, CancellationToken, Func, HttpContext, IConfiguration, IProblemDetailsService, IReadOnlyList, IResxLocalizer (+6 more)

### Community 138 - "IPublicObjectStore"
Cohesion: 0.23
Nodes (6): IPublicObjectStore, CancellationToken, IReadOnlyCollection, Task, ObjectMetadata, IReadOnlyDictionary

### Community 139 - "ProductTemplateId"
Cohesion: 0.08
Nodes (22): DefaultIdType, IReadOnlyList, List, ProductTemplate, Brand, Color, IsActive, Model (+14 more)

### Community 140 - "ProcessedMessage"
Cohesion: 0.19
Nodes (10): ProcessedMessage, ConsumerName, MessageId, ProcessedOn, DateTimeOffset, DefaultIdType, UniqueConstraintException, DefaultIdType (+2 more)

### Community 141 - "IStronglyTypedId"
Cohesion: 0.06
Nodes (31): JsonConverter, StrictDateTimeOffsetJsonConverter, DateTimeOffset, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, StronglyTypedIdListReadOnlyJsonConverter (+23 more)

### Community 142 - "TokenRefreshRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenRefreshRateLimitingPolicy (+1 more)

### Community 143 - "Setup"
Cohesion: 0.10
Nodes (16): LoggerConfiguration, LoggerMinimumLevelConfiguration, microsoft_aspnetcore_httpoverrides, Setup, IEnumerable, IHostEnvironment, KeyValuePair, Assembly (+8 more)

### Community 144 - "Response"
Cohesion: 0.11
Nodes (17): IAM.Endpoints.Users.VersionNeutral.Get, CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, DateTimeOffset, Response (+9 more)

### Community 145 - "JobRow"
Cohesion: 0.19
Nodes (9): JobRow, FailureKey, FinishedOn, Progress, QueuedOn, RequestedBy, StartedOn, Status (+1 more)

### Community 146 - ".RegisterAsync"
Cohesion: 0.07
Nodes (32): BindDeviceSessionRequest, BindDeviceSessionResponse, Guid, OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts, VerifyPhoneOtpRequest (+24 more)

### Community 147 - ".AddServices"
Cohesion: 0.12
Nodes (16): RecurringJobOptions, IRecurringBackgroundJobs, Action, Expression, Func, Task, IConfiguration, ILogger (+8 more)

### Community 148 - "IInterModuleRequestClient"
Cohesion: 0.07
Nodes (28): IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, IInterModuleRequestClient, CancellationToken, Task, VerifyEmailOtpRequest, VerifyEmailOtpResponse, EmailNormalization, CancellationToken (+20 more)

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
Cohesion: 0.13
Nodes (13): CancellationToken, Task, ISmsGateway, SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, SmsMessage (+5 more)

### Community 156 - "Request"
Cohesion: 0.20
Nodes (10): Guid, Request, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken, Password (+2 more)

### Community 157 - "CreateStockLevelOnProductCreatedHandler"
Cohesion: 0.19
Nodes (11): ProductCreatedIntegrationEvent, StoreCreatedIntegrationEvent, DefaultIdType, CancellationToken, DefaultIdType, IFusionCache, ILogger, IOptions (+3 more)

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
Cohesion: 0.20
Nodes (8): IIntegrationEventOutbox, IntegrationEventOutbox, HasPending, IReadOnlyList, List, Lock, Setup, IServiceCollection

### Community 162 - "IEmailGateway"
Cohesion: 0.19
Nodes (9): CancellationToken, Task, EmailMessage, IEmailGateway, CancellationToken, IFusionCache, IOptions, Task (+1 more)

### Community 164 - "NotificationsDbContext"
Cohesion: 0.15
Nodes (13): DbSet, INotificationsDbContext, DeviceRegistrations, DbContextOptions, DbSet, ILogger, TimeProvider, NotificationsDbContext (+5 more)

### Community 165 - "ApplicationUserId"
Cohesion: 0.06
Nodes (35): IBackgroundUserContext, UserId, ICurrentUser, Id, IdAsString, Roles, SessionId, ICollection (+27 more)

### Community 166 - "IamModule"
Cohesion: 0.14
Nodes (12): Action, IApplicationBuilder, IConfiguration, IEnumerable, IServiceCollection, RateLimiterOptions, IamModule, ActivitySourceNames (+4 more)

### Community 167 - "Common.Application.Pagination"
Cohesion: 0.05
Nodes (46): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Stores.v1.My.AuditLog, Common.Application.Pagination, Products.Endpoints.Products.v1.AuditLog, PaginationRequest, After, IncludeTotal, PageNumber (+38 more)

### Community 168 - "Response"
Cohesion: 0.22
Nodes (6): Products.Endpoints.ProductTemplates.v1.Create, RouteGroupBuilder, Setup, RouteGroupBuilder, Response, Id

### Community 169 - "SendSecurityAlertRequestHandler"
Cohesion: 0.16
Nodes (15): SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, CancellationToken, IReadOnlyDictionary, IReadOnlyList, Task (+7 more)

### Community 171 - "S3ObjectStoreCore.cs"
Cohesion: 0.26
Nodes (7): amazon_runtime, amazon_s3, amazon_s3_model, amazon_s3_transfer, Common.Infrastructure.Storage, Common.Application.Storage, polly

### Community 172 - "system_globalization"
Cohesion: 0.08
Nodes (28): Inventory.Endpoints.StockReservations.v1.WebhookCallback, Common.Application.JsonConverters, microsoft_entityframeworkcore_storage_valueconversion, Request, ProviderReference, ReservationId, RequestValidator, RequestBody (+20 more)

### Community 173 - "S3ObjectStoreCore"
Cohesion: 0.20
Nodes (9): AmazonServiceException, S3ObjectStoreCore, CancellationToken, IAmazonS3, IReadOnlyCollection, ResiliencePipeline, Stream, Task (+1 more)

### Community 174 - "system_diagnostics"
Cohesion: 0.16
Nodes (10): BackgroundJobs.Telemetry, LoginMethods, SessionRevokedReasons, ActivitySource, Counter, Meter, ProductsTelemetry, system_collections_concurrent (+2 more)

### Community 175 - "PaginationResponse"
Cohesion: 0.07
Nodes (27): JsonElement, AuditLogDto, DateTimeOffset, PaginationResponse, HasNext, HasPrevious, HasTotal, NextPageNumber (+19 more)

### Community 176 - ".FixedWindow"
Cohesion: 0.23
Nodes (10): FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit, RateLimitPartitions, HttpContext, IConnectionMultiplexer (+2 more)

### Community 177 - "CorsOptions"
Cohesion: 0.25
Nodes (8): CorsOptions, AllowCredentials, AllowedHeaders, AllowedMethods, AllowedOrigins, MaxAgeInSeconds, CorsOptionsValidator, IReadOnlyList

### Community 178 - ".AddPushServices"
Cohesion: 0.22
Nodes (8): CancellationToken, ILogger, LoggerMessage, Task, DummyPushGateway, IConfiguration, IServiceCollection, Setup

### Community 179 - "CachingOptions"
Cohesion: 0.11
Nodes (22): CachingEntryDefaults, Duration, FactoryHardTimeout, FactorySoftTimeout, FailSafeMaxDuration, FailSafeThrottleDuration, CachingOptions, AllowInMemoryOnlyInProduction (+14 more)

### Community 180 - "SmsOptions"
Cohesion: 0.10
Nodes (21): SmsOptions, AppName, AttemptTimeoutSeconds, BaseUrl, MaxPerDay, MaxPerPhoneNumberPerDay, MaxRetryAttempts, MsgHeader (+13 more)

### Community 181 - "RabbitMqOptions"
Cohesion: 0.10
Nodes (18): RabbitMqOptions, DefaultConcurrentMessageLimit, DefaultPrefetchCount, Host, Password, Port, RetryIntervalDeltaMs, RetryLimit (+10 more)

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
Cohesion: 0.10
Nodes (13): CancellationToken, Task, KeycloakPaths, CancellationToken, DateTimeOffset, SemaphoreSlim, Task, TimeSpan (+5 more)

### Community 191 - "DeviceRegistryReconcileJobRegistrar"
Cohesion: 0.27
Nodes (8): IHostedService, CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task, DeviceRegistryReconcileJobRegistrar

### Community 192 - "system_linq_expressions"
Cohesion: 0.24
Nodes (6): Common.Application.BackgroundJobs, BackgroundJobs, hangfire, hangfire_annotations, hangfire_dashboard, system_linq_expressions

### Community 193 - ".GetMeAsync"
Cohesion: 0.19
Nodes (9): CancellationToken, IReadOnlyList, Task, IKeycloakPermissionClient, IReadOnlyList, GrantedPermission, CancellationToken, HttpContext (+1 more)

### Community 194 - "EventDispatcher"
Cohesion: 0.31
Nodes (7): EventDispatcher, ActivitySource, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task

### Community 195 - "IInventoryDbContext"
Cohesion: 0.10
Nodes (16): Inventory.Endpoints.StockReservations.v1.Reserve, DbSet, IInventoryDbContext, StockLevels, StockReservations, CancellationToken, RouteGroupBuilder, Task (+8 more)

### Community 196 - "TokenCreateRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenCreateRateLimitingPolicy (+1 more)

### Community 197 - ".UseModules"
Cohesion: 0.26
Nodes (6): ModuleRegistry, Exception, IApplicationBuilder, ILogger, LoggerMessage, WebApplication

### Community 198 - "InventoryModule"
Cohesion: 0.15
Nodes (10): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, InventoryModule, ActivitySourceNames, MeterNames (+2 more)

### Community 199 - "ReCaptchaResponse"
Cohesion: 0.25
Nodes (7): DateTime, ReCaptchaResponse, ChallengeTs, ErrorCodes, Hostname, Score, Success

### Community 200 - "NotificationsTelemetry"
Cohesion: 0.22
Nodes (5): ActivitySource, Counter, Meter, NotificationsTelemetry, UpDownCounter

### Community 201 - "KeycloakScopes"
Cohesion: 0.20
Nodes (9): Devices, Hangfire, KeycloakScopes, Products, ProductTemplates, Sessions, StockReservations, Stores (+1 more)

### Community 202 - ".Configure"
Cohesion: 0.12
Nodes (12): DateTimeOffset, ModelConfigurationBuilder, EntityTypeBuilder, DomainEventConverter, JsonSerializerOptions, IntegrationEventConverter, JsonSerializerOptions, UtcDateTimeOffsetConverter (+4 more)

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
Cohesion: 0.14
Nodes (11): IServerFilter, JobMetricsFilter, PerformedContext, PerformingContext, BackgroundJobsTelemetry, ActivitySource, ConcurrentDictionary, Counter (+3 more)

### Community 214 - "ProjectionReconciliationOptions.cs"
Cohesion: 0.50
Nodes (4): ProjectionReconciliationOptions, MaxPages, PageSize, ProjectionReconciliationOptionsValidator

### Community 215 - "Response"
Cohesion: 0.18
Nodes (10): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, DateTimeOffset, Response, ClientId, DeviceName, Id, IpAddress, IsCurrent (+2 more)

### Community 216 - ".AddObservability"
Cohesion: 0.24
Nodes (7): OpenTelemetryBuilder, ResourceBuilder, Action, IConfiguration, IHostEnvironment, IReadOnlyList, IServiceCollection

### Community 217 - "FeatureFlags"
Cohesion: 0.33
Nodes (5): Checkout, FeatureFlags, IAM, Notifications, Products

### Community 218 - ".SendOtp"
Cohesion: 0.13
Nodes (16): OtpDispatchErrors, SendPhoneOtpRequest, SendPhoneOtpResponse, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, CancellationToken (+8 more)

### Community 219 - "IBackgroundJobs"
Cohesion: 0.26
Nodes (7): IBackgroundJobs, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 220 - "HttpWarehouseGateway"
Cohesion: 0.31
Nodes (8): ReleaseResponseBody, CancellationToken, HttpClient, HttpResponseMessage, Task, HttpWarehouseGateway, ReleaseRequestBody, ReleaseResponseBody

### Community 221 - "Keycloak realm as code"
Cohesion: 0.25
Nodes (7): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Secrets, Seed users (dev / test only)

### Community 222 - ".ListSessions"
Cohesion: 0.19
Nodes (12): DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, IReadOnlyList, CancellationToken, IReadOnlyCollection, RouteGroupBuilder, Task (+4 more)

### Community 223 - "Request"
Cohesion: 0.22
Nodes (9): Request, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name, OwnerId, SearchTerm (+1 more)

### Community 224 - "Endpoint"
Cohesion: 0.33
Nodes (4): RouteGroupBuilder, Endpoint, RouteGroupBuilder, Setup

### Community 225 - "IInboxStore"
Cohesion: 0.12
Nodes (17): IInboxCleanupTarget, ModuleName, IInboxStore, CancellationToken, DateTimeOffset, DefaultIdType, Task, Setup (+9 more)

### Community 226 - "NetGsmSmsGateway"
Cohesion: 0.32
Nodes (7): Exception, HttpClient, ILogger, IOptions, JsonSerializerOptions, LoggerMessage, NetGsmSmsGateway

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
Cohesion: 0.12
Nodes (15): IInterModuleRequest, IInterModuleRequestHandler, CancellationToken, Task, InterModuleRequestHandler, CancellationToken, ConsumeContext, Task (+7 more)

### Community 231 - "BackgroundJobsModule"
Cohesion: 0.12
Nodes (14): DashboardContext, IAuthorizationService, IDashboardAsyncAuthorizationFilter, BackgroundJobsModule, ActivitySourceNames, MeterNames, Name, StartupPriority (+6 more)

### Community 232 - "S3PublicObjectStore"
Cohesion: 0.23
Nodes (6): Uri, S3PublicObjectStore, CancellationToken, IReadOnlyCollection, Task, Uri

### Community 233 - ".HasPatternIndex"
Cohesion: 0.36
Nodes (6): IndexBuilder, TrigramIndexExtensions, EntityTypeBuilder, Expression, Func, ModelBuilder

### Community 234 - "KeycloakPermissionAuthorizationHandler.cs"
Cohesion: 0.11
Nodes (12): Common.Infrastructure.Auth.Services, IAM.Infrastructure.Auth, Notifications.Infrastructure.Hubs, Common.Infrastructure.Auth, microsoft_aspnetcore_authentication, microsoft_aspnetcore_authentication_jwtbearer, microsoft_aspnetcore_authorization, microsoft_aspnetcore_signalr (+4 more)

### Community 235 - "AuditLogEntryConfiguration"
Cohesion: 0.28
Nodes (5): ModelBuilder, AuditLogEntryConfiguration, ModelBuilder, ModelBuilder, ModelBuilder

### Community 236 - ".SendAsync"
Cohesion: 0.20
Nodes (6): CancellationToken, SendRequestBody, Task, SendMessageBody, Msg, No

### Community 237 - "ProblemDetailsExtensions.cs"
Cohesion: 0.40
Nodes (3): ProblemDetails, ProblemDetailsExtensions, ICollection

### Community 238 - ".UseInfrastructure"
Cohesion: 0.22
Nodes (7): IAuthenticationSchemeProvider, IApplicationBuilder, HttpContext, IOptions, RequestDelegate, Task, SecurityHeadersMiddleware

### Community 239 - "KeycloakTokenClient.cs"
Cohesion: 0.11
Nodes (12): IAM.Infrastructure.Keycloak.Representations, Notifications.Infrastructure.Email.Brevo, Notifications.Application.Email, Inventory.Application.Gateway, IAM.Domain.Errors, IAM.Infrastructure.Keycloak, microsoft_identitymodel_jsonwebtokens, OAuthErrors (+4 more)

### Community 241 - "InterModuleRequestHandlerDefinition"
Cohesion: 0.22
Nodes (8): ConsumerDefinition, IConsumerConfigurator, IRegistrationContext, InterModuleRequestHandlerDefinition, RequestType, IOptions, IReceiveEndpointConfigurator, Type

### Community 242 - "Response"
Cohesion: 0.20
Nodes (8): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, RouteGroupBuilder, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 243 - "CustomValidator"
Cohesion: 0.06
Nodes (39): Inventory.Endpoints.StockReservations.v1.Commit, CustomRateLimitingOptionsValidator, FixedWindowValidator, ModulesOptions, EnabledModules, ModulesOptionsValidator, IReadOnlyList, ReverseProxyOptionsValidator (+31 more)

### Community 244 - "KeyedResilienceProfile"
Cohesion: 0.15
Nodes (13): AbstractValidator, KeyedResilienceProfile, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, RateLimitFailOpen (+5 more)

### Community 245 - ".AddProductAsync"
Cohesion: 0.22
Nodes (7): Products.Endpoints.Stores.v1.AddProduct, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Id

### Community 246 - "Request"
Cohesion: 0.22
Nodes (9): Guid, Request, ClientId, DeviceId, DeviceName, Otp, PhoneNumber, PushToken (+1 more)

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

### Community 251 - "Response"
Cohesion: 0.18
Nodes (9): RouteGroupBuilder, Setup, RouteGroupBuilder, Response, AvailableQuantity, Description, Name, Price (+1 more)

### Community 252 - ".TryWriteAsync"
Cohesion: 0.40
Nodes (4): ProblemDetailsContext, ProblemDetailsServiceExtensions, IProblemDetailsService, Task

### Community 260 - "Common.InterModuleRequests/Setup.cs"
Cohesion: 0.29
Nodes (4): Common.InterModuleRequests, IAssemblyReference, Setup, IServiceCollection

### Community 261 - "AuditableEntity"
Cohesion: 0.18
Nodes (10): ProjectionEntity, SourceVersion, AuditableEntity, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn (+2 more)

### Community 262 - ".SendWithPipelineAsync"
Cohesion: 0.25
Nodes (7): HttpClientKeyedExtensions, CancellationToken, HttpClient, HttpRequestMessage, HttpResponseMessage, ResiliencePipeline, Task

### Community 263 - "Response"
Cohesion: 0.25
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegister, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 264 - ".CreatePresignedUrl"
Cohesion: 0.24
Nodes (5): HttpVerb, Uri, DateTime, Uri, Uri

### Community 265 - ".AddProductToMyStoreAsync"
Cohesion: 0.22
Nodes (7): Products.Endpoints.Stores.v1.My.AddProduct, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Id

### Community 266 - "SwaggerDefaultValues"
Cohesion: 0.33
Nodes (5): IOperationFilter, JsonValue, OpenApiOperation, OperationFilterContext, SwaggerDefaultValues

### Community 267 - "AuditLogOptions"
Cohesion: 0.29
Nodes (7): AuditLogOptions, PerSchemaRetentionDays, PurgeBatchSize, RetentionCron, RetentionDays, AuditLogOptionsValidator, Dictionary

### Community 268 - "DevicesOptions"
Cohesion: 0.33
Nodes (6): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator, IReadOnlyCollection

### Community 269 - "IProductsDbContext"
Cohesion: 0.04
Nodes (42): Products.Endpoints.Stores.v1.My.Create, CancellationToken, Task, DbSet, IProductsDbContext, Products, ProductTemplates, Stores (+34 more)

### Community 270 - "ObjectListPage"
Cohesion: 0.33
Nodes (4): ObjectListItem, ObjectListPage, DateTimeOffset, IReadOnlyList

### Community 271 - "InventoryTelemetry"
Cohesion: 0.50
Nodes (4): ActivitySource, Counter, Meter, InventoryTelemetry

### Community 272 - ".AddResilientHttpClient"
Cohesion: 0.22
Nodes (8): HttpStandardResilienceOptions, IHttpClientBuilder, Setup, Action, HttpClient, IOptions, IServiceCollection, IServiceProvider

### Community 273 - "KeycloakPermission"
Cohesion: 0.29
Nodes (3): KeycloakPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder

### Community 274 - "AuditLogEntry"
Cohesion: 0.25
Nodes (7): AuditLogEntry, AggregateId, AggregateType, Event, EventType, Version, DefaultIdType

### Community 275 - "BaseDbContext"
Cohesion: 0.22
Nodes (8): BaseDbContext, AuditLog, CancellationToken, DbContextOptions, DbSet, ILogger, Task, TimeProvider

### Community 276 - "ResxLocalizationOptions"
Cohesion: 0.40
Nodes (5): ResxLocalizationOptions, DefaultCulture, SupportedCultures, ResxLocalizationOptionsValidator, ICollection

### Community 277 - "SendResponseBody"
Cohesion: 0.50
Nodes (4): SendResponseBody, Code, Description, JobId

### Community 278 - ".TapWhenFeatureEnabledAsync"
Cohesion: 0.33
Nodes (5): FeatureFlagResultExtensions, Action, Func, IFeatureManager, Task

### Community 279 - "Response"
Cohesion: 0.25
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.Create, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 280 - "SendForEmail/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForEmail, Request, CaptchaToken, Email, RequestValidator

### Community 281 - ".TryReadFromJsonAsync"
Cohesion: 0.40
Nodes (4): HttpContent, CancellationToken, Task, HttpContentJsonExtensions

### Community 282 - "JobHousekeepingOptions"
Cohesion: 0.40
Nodes (5): JobHousekeepingOptions, PageSize, RetentionHours, StaleAfterMinutes, JobHousekeepingOptionsValidator

### Community 283 - "FirebasePushGateway.cs"
Cohesion: 0.25
Nodes (5): Notifications.Application.Push, Notifications.Infrastructure.Push.Firebase, firebaseadmin, firebaseadmin_messaging, google_apis_auth_oauth2

### Community 284 - "RequestBody"
Cohesion: 0.33
Nodes (6): DateTimeOffset, DefaultIdType, RequestBody, ProductId, Quantity, ReservationDeadline

### Community 285 - "RequestBodyLimitMetadata"
Cohesion: 0.25
Nodes (8): RequestBodyLimitMetadata, MaxBodyBytes, RequestBodyLimitMiddleware, Func, HttpContext, IHttpMaxRequestBodySizeFeature, RequestDelegate, Task

### Community 286 - ".HandleAsync"
Cohesion: 0.29
Nodes (4): CancellationToken, Task, CancellationToken, Task

### Community 287 - "IOtpService"
Cohesion: 0.24
Nodes (8): CancellationToken, Task, TimeSpan, IOtpService, OtpVerificationOutcome, InvalidOtp, Success, TooManyAttempts

### Community 288 - "DummySmsGateway"
Cohesion: 0.38
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, DummySmsGateway

### Community 289 - "AuditableEntityResponse"
Cohesion: 0.29
Nodes (7): AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, DateTimeOffset

### Community 291 - "StronglyTypedIdBinder.cs"
Cohesion: 0.29
Nodes (5): IModelBinder, microsoft_aspnetcore_mvc_modelbinding, ModelBindingContext, StronglyTypedIdBinder, Task

### Community 292 - "IAuditableEntity"
Cohesion: 0.29
Nodes (6): IAuditableEntity, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, DateTimeOffset

### Community 294 - ".SendOtp"
Cohesion: 0.29
Nodes (5): CancellationToken, IFeatureManager, RouteGroupBuilder, Task, Endpoint

### Community 295 - "Infrastructure/StringExtensions.cs"
Cohesion: 0.40
Nodes (3): opentelemetry_exporter, OtlpExportProtocol, StringExtensions

### Community 296 - "SecurityHeadersOptions.cs"
Cohesion: 0.50
Nodes (4): SecurityHeadersOptions, Headers, SecurityHeadersOptionsValidator, Dictionary

### Community 297 - ".SetConcurrency"
Cohesion: 0.50
Nodes (3): IBusFactoryConfigurator, ConcurrencyConfiguratorExtensions, IReceiveEndpointConfigurator

### Community 298 - ".AddCustomSwagger"
Cohesion: 0.20
Nodes (7): IApplicationBuilder, IConfigureOptions, IServiceCollection, IWebHostEnvironment, SwaggerGenOptions, Type, Setup

### Community 299 - "Endpoint"
Cohesion: 0.33
Nodes (4): RouteGroupBuilder, Endpoint, RouteGroupBuilder, Setup

### Community 300 - "ResultTelemetryExtensions"
Cohesion: 0.38
Nodes (4): Activity, ResultTelemetryExtensions, ActivitySource, Task

### Community 301 - "CachedCaptchaService"
Cohesion: 0.33
Nodes (5): CancellationToken, IFusionCache, IOptions, Task, CachedCaptchaService

### Community 302 - ".EmailVerificationTokenValidation"
Cohesion: 0.50
Nodes (3): IResxLocalizer, IRuleBuilder, IRuleBuilderOptions

### Community 303 - "DefaultResponsesOperationFilter"
Cohesion: 0.70
Nodes (3): OpenApiOperation, OperationFilterContext, DefaultResponsesOperationFilter

### Community 304 - ".SetRetryAfterHeader"
Cohesion: 0.40
Nodes (4): RetryAfterHeaderExtensions, HttpResponse, RateLimitLease, TimeSpan

### Community 305 - "StronglyTypedIdSchemaFilter.cs"
Cohesion: 0.33
Nodes (4): IOpenApiSchema, ISchemaFilter, SchemaFilterContext, StronglyTypedIdSchemaFilter

### Community 306 - ".PhoneNumberValidation"
Cohesion: 0.50
Nodes (3): IResxLocalizer, IRuleBuilder, IRuleBuilderOptions

### Community 307 - "SignalROptions"
Cohesion: 0.40
Nodes (5): SignalROptions, RedisConnectionString, RequireRedisBackplaneInProduction, UseRedisBackplane, SignalROptionsValidator

### Community 308 - "PublishOutcome"
Cohesion: 0.50
Nodes (4): PublishOutcome, Failed, Published, Skipped

### Community 310 - "RequestBodyLimitFilter"
Cohesion: 0.22
Nodes (8): RequestBodyLimitEndpointExtensions, RequestBodyLimitFilter, EndpointFilterDelegate, EndpointFilterInvocationContext, Func, HttpContext, IHttpMaxRequestBodySizeFeature, ValueTask

### Community 311 - "microsoft_aspnetcore_mvc"
Cohesion: 0.07
Nodes (31): Products.Endpoints.Stores.v1.Deactivate, Products.Endpoints.ProductTemplates.v1.Deactivate, Inventory.Endpoints.StockReservations.v1.Release, Common.Application.ModelBinders, Products.Endpoints.ProductTemplates.v1.Activate, microsoft_aspnetcore_mvc, Request, Id (+23 more)

### Community 312 - "DeviceRegistrationId"
Cohesion: 0.29
Nodes (4): DefaultIdType, DeviceRegistrationId, EntityTypeBuilder, DeviceRegistrationConfiguration

### Community 313 - "Response"
Cohesion: 0.22
Nodes (6): Products.Endpoints.Stores.v1.Create, RouteGroupBuilder, Setup, RouteGroupBuilder, Response, Id

### Community 314 - "Versioning/Setup.cs"
Cohesion: 0.50
Nodes (3): asp_versioning, asp_versioning_builder, asp_versioning_conventions

### Community 319 - "Products/v1/Update/Request.cs"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Products.v1.Update, RequestBody, Request, Body, Id, RequestBody, Description, Name (+3 more)

### Community 323 - ".AddBrevo"
Cohesion: 0.33
Nodes (5): IConfiguration, IFusionCache, IOptions, IServiceCollection, Setup

### Community 324 - "Products/v1/My/Update/Request.cs"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Products.v1.My.Update, RequestBody, Request, Body, Id, RequestBody, Description, Name (+3 more)

### Community 325 - ".AddNetGsm"
Cohesion: 0.33
Nodes (5): IConfiguration, IFusionCache, IOptions, IServiceCollection, Setup

### Community 326 - "MassTransitInterModuleRequestClient"
Cohesion: 0.33
Nodes (5): IClientFactory, MassTransitInterModuleRequestClient, CancellationToken, IOptions, Task

### Community 327 - "EnrichLogsWithUserInfoMiddleware"
Cohesion: 0.33
Nodes (5): IMiddleware, HttpContext, RequestDelegate, Task, EnrichLogsWithUserInfoMiddleware

### Community 328 - ".ActivateProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 329 - "ProductTemplates/v1/Create/Request.cs"
Cohesion: 0.40
Nodes (5): Request, Brand, Color, Model, RequestValidator

### Community 330 - ".UpdateMyStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 331 - ".RemoveProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 332 - "How it works"
Cohesion: 0.40
Nodes (5): How it works, One-time setup _(Build, in a migration)_, Query path, Ranking, Write path

### Community 333 - "Setup.Observability.cs"
Cohesion: 0.40
Nodes (4): opentelemetry_metrics, opentelemetry_opentelemetrybuilder, opentelemetry_resources, opentelemetry_trace

### Community 334 - "JobStatus"
Cohesion: 0.40
Nodes (5): JobStatus, Failed, Queued, Running, Succeeded

### Community 335 - ".AddCommonObjectStorage"
Cohesion: 0.40
Nodes (4): Setup, IAmazonS3, IOptions, IServiceCollection

### Community 337 - "DatabaseOptions.cs"
Cohesion: 0.67
Nodes (3): DatabaseOptions, ConnectionString, DatabaseOptionsValidator

## Knowledge Gaps
- **975 isolated node(s):** `UserId`, `Id`, `IdAsString`, `Roles`, `SessionId` (+970 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 2260 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **101 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `ObjectStorageOptions`, `AuditLogOptions`, `DevicesOptions`, `Common.Domain.ResultMonad`, `Setup`, `EmailOptions`, `ResxLocalizationOptions`, `OutboxOptions`, `Common.Infrastructure.Extensions`, `JobHousekeepingOptions`, `Setup.Logger.cs`, `fluentvalidation`, `FirebasePushGateway.cs`, `OtpOptions`, `ObservabilityOptions`, `SecurityHeadersOptions.cs`, `ConfigureSwaggerOptions.cs`, `S3ObjectStoreCore.cs`, `KeycloakOptions`, `Common.Domain.StronglyTypedIds`, `CorsOptions`, `CachingOptions`, `SignalROptions`, `RabbitMqOptions`, `SmsOptions`, `IdentityScheme`, `RequestLoggingOptions`, `FullTextSearchOptions`, `Setup.Observability.cs`, `DatabaseOptions.cs`, `BackgroundJobsOptions`, `ProjectionReconciliationOptions.cs`, `PushOptions`, `ResiliencyOptions`, `CaptchaOptions`, `KeycloakPermissionAuthorizationHandler.cs`, `InterModuleRequestOptions`, `Common.InterModuleRequests.Contracts`, `KeycloakTokenClient.cs`, `CustomValidator`, `OpenApiOptions`, `InventoryOptions`, `.AddCustomHealthChecks`?**
  _High betweenness centrality (0.203) - this node is a cross-community bridge._
- **Why does `Result` connect `Result` to `.ReserveSeriesAsync`, `KeycloakAdminClient`, `.AssignBasicRoleOrRollbackAsync`, `FirebasePushGateway`, `Error`, `ProductTemplateId`, `IKeycloakAdminClient`, `IProductsDbContext`, `.AddProductToMyStoreAsync`, `Response`, `.RegisterAsync`, `IInterModuleRequestClient`, `.TapWhenFeatureEnabledAsync`, `DummyEmailGateway`, `.RevokeToken`, `ISmsGateway`, `.NotFound`, `.RefreshToken`, `IEmailGateway`, `DummySmsGateway`, `.SendOtp`, `SendSecurityAlertRequestHandler`, `ResultTelemetryExtensions`, `CachedCaptchaService`, `PaginationQueryableExtensions`, `PaginationResponse`, `.AddPushServices`, `ReCaptchaService`, `SendEmailOtpRequestHandler`, `BrevoEmailGateway`, `.SearchProductTemplatesAsync`, `KeycloakTokenClient`, `.SendCoreAsync`, `.GetMeAsync`, `IInventoryDbContext`, `Common.Domain.Events`, `.ActivateProductTemplateAsync`, `Response`, `.UpdateMyStoreAsync`, `.RemoveProductAsync`, `Response`, `.UpdateCurrentPushToken`, `.SearchStoresAsync`, `StockReservation`, `.SendOtp`, `.GetProductAsync`, `HttpWarehouseGateway`, `.GetClientKey`, `.ListSessions`, `.SearchMyProductsAsync`, `NetGsmSmsGateway`, `.SearchStoreProductsAsync`, `.SendAsync`, `.IsRegisteredAsync`, `.ReleaseStockReservationAsync`, `Response`, `.AddProductAsync`, `InventoryOptions`, `Response`, `Response`?**
  _High betweenness centrality (0.113) - this node is a cross-community bridge._
- **Why does `ApplicationUserId` connect `ApplicationUserId` to `NotificationPayload`, `StoreId`, `AuditableEntity`, `KeycloakAdminClient`, `.HandleAsync`, `.AssignBasicRoleOrRollbackAsync`, `IKeycloakAdminClient`, `IStronglyTypedId`, `KeycloakPermissionAuthorizationHandler`, `Response`, `JobRow`, `.RegisterAsync`, `DeviceRegistration`, `.RevokeToken`, `V1StoreCreatedDomainEvent`, `CreateStockLevelOnProductCreatedHandler`, `AuditableEntityResponse`, `For`, `IAuditableEntity`, `SendSecurityAlertRequestHandler`, `system_globalization`, `PaginationResponse`, `Common.Domain.StronglyTypedIds`, `Response`, `microsoft_aspnetcore_mvc`, `DeviceRegistrationId`, `KeycloakTokenClient`, `Response`, `.SearchStoresAsync`, `.Configure`, `Store`, `.ListSessions`, `Request`, `InterModuleRequestHandler`, `StockLevel`, `Response`, `Response`?**
  _High betweenness centrality (0.064) - this node is a cross-community bridge._
- **What connects `UserId`, `Id`, `IdAsString` to the rest of the system?**
  _975 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `.ReserveSeriesAsync` be split into smaller, more focused modules?**
  _Cohesion score 0.12380952380952381 - nodes in this community are weakly interconnected._
- **Should `RedisFixedWindowRateLimiter` be split into smaller, more focused modules?**
  _Cohesion score 0.13333333333333333 - nodes in this community are weakly interconnected._
- **Should `Error` be split into smaller, more focused modules?**
  _Cohesion score 0.07196969696969698 - nodes in this community are weakly interconnected._