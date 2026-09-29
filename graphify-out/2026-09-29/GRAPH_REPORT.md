# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-09-29)

## Corpus Check
- 585 files · ~94,496 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 5055 nodes · 10053 edges · 394 communities (294 shown, 100 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 294 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `95445ea6`
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
- microsoft_aspnetcore_http
- IKeycloakAdminClient
- ISearchLanguageResolver
- Common.Application.Options
- KeycloakPermissionAuthorizationHandler
- EmailOptions
- Product
- IssueVerificationTokenRequestHandler
- Products.Domain.Stores.DomainEvents.v1
- LogProductCatalogChangeHandler
- BoundedRequestCaptureStream
- DeviceRegistration
- EmailMessage
- .RevokeToken
- microsoft_extensions_configuration
- IEvent
- RequestResponseBodyLoggingMiddleware
- fluentvalidation
- Func
- .UpsertIfNewerAsync
- NotificationsModule
- PersistenceQueryableExtensions
- Response
- KeycloakPermissionClient
- OutboxMessage
- CustomRateLimitingOptions
- AdminRepresentations.cs
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
- microsoft_entityframeworkcore
- Add Integration Event Command
- Response
- ProductsDbContext
- ReCaptchaService
- Request
- EmailOtpDispatchOutcome
- BrevoEmailGateway
- ProjectionReconciliationJob
- Cross-Module Reference Violation
- OutboxMetricsJob
- .SearchProductTemplatesAsync
- Bogus Test Data
- Result
- IDbContext
- RequestLoggingOptions
- .SendCoreAsync
- .Schedule
- OutboxModule
- .AddNotificationsSignalR
- ValueObject
- DomainEvent
- IntegrationEventHandlerBase
- FullTextSearchOptions
- Response
- .WriteAsync
- AsNoTracking Coverage Check
- Setup
- AuditableEntityResponse
- AuditLogRetentionService
- CheckRegistrationRateLimitingPolicy
- .GetAuditLogAsync
- Common.Domain.Events
- .SearchStoresAsync
- Split-Deployment PoC
- .CreateMyStoreAsync
- .Configure
- StockReservation
- Store
- PushOptions
- Configuration-Driven Module Registration
- Program.cs
- .GetProductAsync
- ProductId
- ResultToResponseTransformer.cs
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
- RequireFeatureFilter
- Response
- Common.Domain.ResultMonad
- InventoryOptions
- StockLevel
- .IsRegisteredAsync
- KeycloakPermissionPolicyProvider
- .ReleaseStockReservationAsync
- OutboxCleanupJob
- IPrivateObjectStore
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
- IOutboxMessage
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
- VerifyPhoneOtpResponse
- .WriteTooManyRequestsToResponse
- ObjectMetadata
- ProductTemplate
- ProcessedMessage
- IStronglyTypedId
- TokenRefreshRateLimitingPolicy
- Setup
- Response
- JobRow
- .RegisterAsync
- .AddOrUpdate
- Response
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
- IInventoryDbContext
- OtpServiceBase
- IntegrationEventOutbox
- IEmailGateway
- For
- NotificationsDbContext
- ApplicationUserId
- IamModule
- PaginationRequest
- Response
- SendSecurityAlertRequestHandler
- Notifications.Application/IAssemblyReference.cs
- TokenResponseRepresentation
- Common.Domain.StronglyTypedIds
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
- OtpService
- IdentityScheme
- Policies
- ReverseProxyOptions
- BoundedCaptureStream
- JobClaimExtensions
- ServiceAccountTokenCache
- S3PrivateObjectStore
- .From
- .ListPermissionsAsync
- EventDispatcher
- .ReserveStockAsync
- TokenCreateRateLimitingPolicy
- .UseModules
- InventoryModule
- ProjectionUpsertOutcome
- NotificationsTelemetry
- KeycloakScopes
- ProductTemplateId
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
- .AddServices
- FeatureFlags
- ICaptchaService
- BackgroundJobsService
- HttpWarehouseGateway
- Keycloak realm as code
- .ListSessions
- Request
- .MapEndpoint
- IInboxStore
- NetGsmSmsGateway
- RegisterRateLimitingPolicy
- .AddCommonCaching
- .AddCommonOptions
- InterModuleRequestHandler
- .UseModule
- S3PublicObjectStore
- .HasPatternIndex
- Notifications.Application.Hubs
- AuditLogEntry
- .SendAsync
- ProblemDetailsExtensions.cs
- .UseInfrastructure
- KeycloakAdminClient.cs
- V1StockReservationCommitConflictDetectedDomainEvent
- .NotFound
- Response
- CustomValidator
- KeyedResilienceProfile
- .AddProductAsync
- Request
- OpenApiOptions
- SendErrorBody
- DeviceRegistryReconciliationService
- .TryDeserialize
- GetProductRequest
- .TryWriteAsync
- Products.Domain/IAssemblyReference.cs
- IAM.Application/IAssemblyReference.cs
- Products.Infrastructure/IAssemblyReference.cs
- Products.Application/IAssemblyReference.cs
- Common.Infrastructure/IAssemblyReference.cs
- Notifications.Domain/IAssemblyReference.cs
- IAM.Infrastructure/IAssemblyReference.cs
- Infrastructure/Setup.cs
- AuditableEntity
- .SendWithPipelineAsync
- Response
- .CreatePresignedUrl
- BackgroundJobsModule
- .AddCustomSwagger
- AuditLogOptions
- RedisOtpService
- IProductsDbContext
- ObjectListPage
- InventoryTelemetry
- .AddResilientHttpClient
- KeycloakPermission
- CurrentUser.cs
- BaseDbContext
- Request
- SendResponseBody
- .TapWhenFeatureEnabledAsync
- Response
- SendForEmail/Request.cs
- .TryReadFromJsonAsync
- JobHousekeepingOptions
- FirebasePushGateway.cs
- RequestBody
- RequestBodyLimitFilter.cs
- IOtpService
- OtpVerificationOutcome
- DummySmsGateway
- SendForLogin/Request.cs
- UploadObjectRequest
- StronglyTypedIdBinder
- IAuditableEntity
- .MapEndpoints
- SendForRegistration/Request.cs
- SmsOtpDispatchOutcome
- SecurityHeadersOptions.cs
- .SetConcurrency
- Setup
- .MapEndpoint
- ResultTelemetryExtensions
- RequestBody
- CommonValidations
- DefaultResponsesOperationFilter
- .SetRetryAfterHeader
- .Apply
- ModulesOptions.cs
- SignalROptions
- PublishOutcome
- .Capture
- .InvokeAsync
- Common.Application.ModelBinders
- Request
- Response
- Inventory.Domain.StockReservations
- Setup
- Setup
- .CreateAsync
- Products.Endpoints
- Products/v1/Update/Request.cs
- Notifications.Infrastructure
- system_runtime_compilerservices
- .VerifySha256
- KeyedRow
- Products/v1/My/Update/Request.cs
- .AddNetGsm
- ProductsTelemetry
- Setup
- .AddServices
- JobStatus
- ValidationContextExtensions
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

## Communities (394 total, 100 thin omitted)

### Community 0 - ".ReserveSeriesAsync"
Cohesion: 0.14
Nodes (12): Inventory.Endpoints.StockReservations.v1.ReserveSeries, CancellationToken, IOptions, List, RequestBody, RouteGroupBuilder, Task, TimeProvider (+4 more)

### Community 1 - "NotificationPayload"
Cohesion: 0.23
Nodes (12): IHubContext, CancellationToken, IReadOnlyList, Task, INotificationDispatcher, Task, INotificationsClient, NotificationPayload (+4 more)

### Community 2 - "OutboxProcessor"
Cohesion: 0.21
Nodes (12): IPublishEndpoint, PublishOutcome, CancellationToken, Exception, ILogger, IOptions, IServiceScopeFactory, LoggerMessage (+4 more)

### Community 3 - "Common.Application.DTOs"
Cohesion: 0.11
Nodes (11): Products.Endpoints.ProductTemplates.v1.Search, Products.Endpoints.Products.v1.Search, Common.Application.DTOs, Products.Endpoints.Stores.v1.Get, Products.Endpoints.Products.v1.My.Search, Products.Endpoints.ProductTemplates.v1.Get, Products.Endpoints.Products.v1.Get, Products.Endpoints.Stores.v1.My.Get (+3 more)

### Community 5 - "KeycloakAdminClient"
Cohesion: 0.10
Nodes (33): DateOnly, DateTimeOffset, CreateKeycloakUser, KeycloakUser, KeycloakUserPage, KeycloakUserSession, CancellationToken, Func (+25 more)

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
Cohesion: 0.07
Nodes (22): Inventory.Domain.StockReservations.Errors, StringLocalizerExtensions, IStringLocalizer, Error, Key, ParameterName, StatusCode, SubErrors (+14 more)

### Community 11 - "microsoft_aspnetcore_http"
Cohesion: 0.11
Nodes (22): IAM.Endpoints.Captcha.VersionNeutral, Common.Application.Search, Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Products.Endpoints.ProductTemplates, Products.Infrastructure.Telemetry, Products.Application.Persistence (+14 more)

### Community 12 - "IKeycloakAdminClient"
Cohesion: 0.09
Nodes (21): CancellationToken, IReadOnlyList, Task, IKeycloakAdminClient, CancellationToken, RouteGroupBuilder, Task, Endpoint (+13 more)

### Community 13 - "ISearchLanguageResolver"
Cohesion: 0.12
Nodes (14): Configuration, ISearchLanguageResolver, UniversalConfig, SearchLanguageResolver, UniversalConfig, IOptions, Setup, IApplicationBuilder (+6 more)

### Community 14 - "Common.Application.Options"
Cohesion: 0.06
Nodes (43): amazon_runtime, amazon_s3, amazon_s3_model, amazon_s3_transfer, Common.Infrastructure.Modules, Products.Endpoints.Stores, Products.Endpoints.Probe, Common.Infrastructure.RateLimiting (+35 more)

### Community 15 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.13
Nodes (17): AuthorizationHandler, AuthorizationHandlerContext, IAuthorizationRequirement, CancellationToken, ClaimsPrincipal, HttpContext, IFusionCache, IHttpContextAccessor (+9 more)

### Community 16 - "EmailOptions"
Cohesion: 0.09
Nodes (24): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+16 more)

### Community 17 - "Product"
Cohesion: 0.07
Nodes (23): Products.Domain.Products.DomainEvents.v1, ProductId, V1ProductDescriptionUpdatedDomainEvent, ProductId, V1ProductNameUpdatedDomainEvent, ProductId, V1ProductPriceDecreasedDomainEvent, ProductId (+15 more)

### Community 18 - "IssueVerificationTokenRequestHandler"
Cohesion: 0.39
Nodes (6): IssueVerificationTokenRequest, IssueVerificationTokenResponse, CancellationToken, IOptions, Task, IssueVerificationTokenRequestHandler

### Community 19 - "Products.Domain.Stores.DomainEvents.v1"
Cohesion: 0.09
Nodes (17): Products.Application.Stores.DomainEventHandlers.v1, Products.Domain.Stores.DomainEvents.v1, V1StoreCreatedDomainEventHandlers, ProductSnapshot, StoreId, ProductSnapshot, V1ProductAddedToStoreDomainEvent, V1ProductAddedToStoreDomainEventExtensions (+9 more)

### Community 20 - "LogProductCatalogChangeHandler"
Cohesion: 0.24
Nodes (8): CancellationToken, DefaultIdType, IFusionCache, ILogger, IOptions, LoggerMessage, Task, LogProductCatalogChangeHandler

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.18
Nodes (7): BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position, Stream

### Community 22 - "DeviceRegistration"
Cohesion: 0.13
Nodes (14): DateTimeOffset, DefaultIdType, Guid, DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive (+6 more)

### Community 23 - "EmailMessage"
Cohesion: 0.32
Nodes (6): EmailMessage, CancellationToken, ILogger, LoggerMessage, Task, DummyEmailGateway

### Community 24 - ".RevokeToken"
Cohesion: 0.09
Nodes (18): DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse, IReadOnlyList, CancellationToken, RouteGroupBuilder, Task, Endpoint, CancellationToken (+10 more)

### Community 25 - "microsoft_extensions_configuration"
Cohesion: 0.09
Nodes (17): Notifications.Application.Otp, Notifications.Application.Sms, Notifications.Infrastructure.Devices, Notifications.Infrastructure.Email, Common.Application.Caching, Notifications.Infrastructure.Sms, Notifications.Infrastructure.Email.Brevo, Notifications.Infrastructure.Telemetry (+9 more)

### Community 26 - "IEvent"
Cohesion: 0.11
Nodes (14): DomainEventHandlerBase, CancellationToken, Task, IEventHandler, CancellationToken, Task, IEventHandlerWrapper, CancellationToken (+6 more)

### Community 27 - "RequestResponseBodyLoggingMiddleware"
Cohesion: 0.22
Nodes (7): IDiagnosticContext, HttpContext, IList, IOptions, PathString, RequestDelegate, RequestResponseBodyLoggingMiddleware

### Community 28 - "fluentvalidation"
Cohesion: 0.09
Nodes (26): common_application_localization_resources, IAM.Domain.Users, Inventory.Endpoints.StockReservations.v1.WebhookCallback, Products.Endpoints.Probe.v1, Common.Domain.Extensions, Common.Application.Pagination, Notifications.Infrastructure.Devices.UpdateCurrentPushToken, Common.Application.Validation (+18 more)

### Community 29 - "Func"
Cohesion: 0.22
Nodes (5): AsyncExtensions, SyncExtensions, Action, Func, Task

### Community 30 - ".UpsertIfNewerAsync"
Cohesion: 0.18
Nodes (9): ICurrentDbContext, ProjectionUpsertExtensions, Action, CancellationToken, DbContext, DbSet, Func, Task (+1 more)

### Community 31 - "NotificationsModule"
Cohesion: 0.15
Nodes (10): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, NotificationsModule, ActivitySourceNames, MeterNames (+2 more)

### Community 32 - "PersistenceQueryableExtensions"
Cohesion: 0.33
Nodes (6): PersistenceQueryableExtensions, CancellationToken, Expression, Func, IQueryable, Task

### Community 33 - "Response"
Cohesion: 0.18
Nodes (9): IAM.Endpoints.Tokens.VersionNeutral.Refresh, RouteGroupBuilder, Endpoint, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken (+1 more)

### Community 34 - "KeycloakPermissionClient"
Cohesion: 0.19
Nodes (14): CancellationToken, Dictionary, HttpClient, HttpResponseMessage, ILogger, IOptions, IReadOnlyList, List (+6 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.10
Nodes (17): IEntityTypeConfiguration, OutboxMessage, CreatedOn, Event, FailedOn, Id, IsProcessed, NextRetryAt (+9 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.08
Nodes (22): CheckRegistrationRateLimitingPolicy, EmailRateLimitingPolicy, OtpVerifyRateLimitingPolicy, RegisterRateLimitingPolicy, SmsRateLimitingPolicy, CustomRateLimitingOptions, CheckRegistration, CreateStore (+14 more)

### Community 37 - "AdminRepresentations.cs"
Cohesion: 0.12
Nodes (16): CredentialRepresentation, Temporary, Type, Value, ErrorRepresentation, Error, ErrorMessage, Field (+8 more)

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

### Community 45 - ".AddKeycloakInfrastructure"
Cohesion: 0.16
Nodes (10): CancellationToken, Task, IServiceAccountTokenProvider, HttpClient, IOptions, TimeProvider, ServiceAccountTokenProvider, IOptions (+2 more)

### Community 46 - "PaginationQueryableExtensions"
Cohesion: 0.14
Nodes (16): BinaryExpression, ExpressionVisitor, KeyedRow, MemberExpression, MethodInfo, ParameterExpression, CursorBound, Value (+8 more)

### Community 48 - "microsoft_entityframeworkcore"
Cohesion: 0.06
Nodes (33): Common.Infrastructure.Persistence.Inbox, Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Products.Infrastructure.Persistence.Seeding, Common.Application.Persistence, Outbox, Common.Infrastructure.Persistence.Outbox, Inventory.Infrastructure.Persistence.EntityConfigurations (+25 more)

### Community 50 - "Response"
Cohesion: 0.13
Nodes (14): RouteGroupBuilder, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate, CreatedOn, Email (+6 more)

### Community 51 - "ProductsDbContext"
Cohesion: 0.09
Nodes (21): IDatabaseSeeder, Priority, CancellationToken, Task, DbContextOptions, DbSet, ILogger, TimeProvider (+13 more)

### Community 52 - "ReCaptchaService"
Cohesion: 0.06
Nodes (36): FormUrlEncodedContent, ReCaptchaResponse, CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey, Provider (+28 more)

### Community 53 - "Request"
Cohesion: 0.15
Nodes (13): Guid, Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FirstName (+5 more)

### Community 54 - "EmailOtpDispatchOutcome"
Cohesion: 0.33
Nodes (5): EmailOtpDispatchErrors, EmailOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled

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

### Community 61 - "Result"
Cohesion: 0.14
Nodes (16): JsonWebTokenHandler, Result, Error, IsFailure, Value, VerifyEmailOtpResponseExtensions, CancellationToken, Dictionary (+8 more)

### Community 62 - "IDbContext"
Cohesion: 0.15
Nodes (14): ChangeTracker, DatabaseFacade, JobHousekeepingJob, CancellationToken, ILogger, IOptions, LoggerMessage, Task (+6 more)

### Community 63 - "RequestLoggingOptions"
Cohesion: 0.11
Nodes (20): IPostConfigureOptions, RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes (+12 more)

### Community 64 - ".SendCoreAsync"
Cohesion: 0.21
Nodes (8): SendErrorBody, CancellationToken, HttpResponseMessage, SendRequestBody, Task, SendContact, Email, Name

### Community 65 - ".Schedule"
Cohesion: 0.31
Nodes (6): Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 66 - "OutboxModule"
Cohesion: 0.14
Nodes (16): Action, Exception, IApplicationBuilder, IEndpointRouteBuilder, IEnumerable, IHostApplicationLifetime, ILogger, ILoggerFactory (+8 more)

### Community 67 - ".AddNotificationsSignalR"
Cohesion: 0.17
Nodes (10): HubConnectionContext, IUserIdProvider, RedisOptions, IConfiguration, IConfigureOptions, IConnectionMultiplexer, IServiceCollection, IUserIdProvider (+2 more)

### Community 68 - "ValueObject"
Cohesion: 0.25
Nodes (4): Common.Domain, IComparable, ValueObject, IEnumerable

### Community 69 - "DomainEvent"
Cohesion: 0.08
Nodes (24): AggregateRoot, Events, Id, Version, IReadOnlyCollection, List, IAggregateRoot, Events (+16 more)

### Community 70 - "IntegrationEventHandlerBase"
Cohesion: 0.22
Nodes (12): IConsumer, IntegrationEventHandlerBase, MaxEventAge, CancellationToken, ConsumeContext, DefaultIdType, IFusionCache, ILogger (+4 more)

### Community 71 - "FullTextSearchOptions"
Cohesion: 0.09
Nodes (24): Add a new language/culture, Add search to a new entity _(Build checklist)_, Change the vector (weights, fields, or config), Extending and maintaining, Full-Text Search, Gotchas, How it works, Non-goals (+16 more)

### Community 72 - "Response"
Cohesion: 0.22
Nodes (8): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Brand, Color, Model

### Community 73 - ".WriteAsync"
Cohesion: 0.33
Nodes (5): Memory, ReadOnlyMemory, CancellationToken, Task, ValueTask

### Community 75 - "Setup"
Cohesion: 0.33
Nodes (4): ApiVersionSet, Setup, IEndpointRouteBuilder, IServiceCollection

### Community 76 - "AuditableEntityResponse"
Cohesion: 0.12
Nodes (15): AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, DateTimeOffset, RouteGroupBuilder (+7 more)

### Community 77 - "AuditLogRetentionService"
Cohesion: 0.33
Nodes (7): AuditLogRetentionService, CancellationToken, ILogger, IOptions, LoggerMessage, NpgsqlDataSource, Task

### Community 78 - "CheckRegistrationRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, CheckRegistrationRateLimitingPolicy (+1 more)

### Community 79 - ".GetAuditLogAsync"
Cohesion: 0.13
Nodes (13): Payload, PaginationCursor, Payload, TaggedValue, JsonSerializerOptions, Type, DbContextExtensions, CancellationToken (+5 more)

### Community 80 - "Common.Domain.Events"
Cohesion: 0.13
Nodes (11): Inventory.Domain.StockReservations.DomainEvents.v1, Common.Domain.Events, Inventory.Domain.StockLevels.DomainEvents.v1, UnknownDomainEvent, DefaultIdType, StockLevelId, V1StockLevelCreatedDomainEvent, DateTimeOffset (+3 more)

### Community 81 - ".SearchStoresAsync"
Cohesion: 0.13
Nodes (13): Products.Endpoints.Stores.v1.Search, CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response (+5 more)

### Community 82 - "Split-Deployment PoC"
Cohesion: 0.18
Nodes (9): Concurrent safety, Cross-process call path, Files added by this PoC, How it works, How to run, Split-Deployment PoC, What this proves, README (Boilerplate Overview) (+1 more)

### Community 83 - ".CreateMyStoreAsync"
Cohesion: 0.14
Nodes (9): Products.Endpoints.Stores.v1.My.Create, CancellationToken, Task, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response (+1 more)

### Community 84 - ".Configure"
Cohesion: 0.15
Nodes (14): AuditableEntityConfiguration, EntityTypeBuilder, JobRowConfiguration, EntityTypeBuilder, StronglyTypedIdValueConverter, DefaultIdType, EntityTypeBuilder, DeviceRegistrationConfiguration (+6 more)

### Community 85 - "StockReservation"
Cohesion: 0.09
Nodes (23): DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationExpiredDomainEvent, DateTimeOffset, StockReservationId, V1StockReservationReleaseAttemptStartedDomainEvent, DateTimeOffset (+15 more)

### Community 86 - "Store"
Cohesion: 0.08
Nodes (20): File map _(Build)_, ISearchLocalized, Language, StoreId, V1StoreAddressUpdatedDomainEvent, StoreId, V1StoreDescriptionUpdatedDomainEvent, IReadOnlyCollection (+12 more)

### Community 87 - "PushOptions"
Cohesion: 0.11
Nodes (21): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri, Type (+13 more)

### Community 89 - "Program.cs"
Cohesion: 0.22
Nodes (6): ConfigurationManager, Host, Host.Configurations, Setup, Program, WebApplicationBuilder

### Community 90 - ".GetProductAsync"
Cohesion: 0.12
Nodes (19): GetStockLevelRequest, GetStockLevelResponse, DefaultIdType, CancellationToken, Task, GetStockLevelRequestHandler, RouteGroupBuilder, Setup (+11 more)

### Community 91 - "ProductId"
Cohesion: 0.07
Nodes (27): Products.Endpoints.Stores.v1.My.AddProduct, Products.Endpoints.Products.v1.My.Get, Products.Endpoints.Stores.v1.My.RemoveProduct, Products.Endpoints.Stores.v1.RemoveProduct, DefaultIdType, ProductId, CancellationToken, RouteGroupBuilder (+19 more)

### Community 92 - "ResultToResponseTransformer.cs"
Cohesion: 0.19
Nodes (12): Common.Application.EndpointFilters, microsoft_aspnetcore_hosting, microsoft_aspnetcore_http_iresult, microsoft_extensions_localization, ProblemResponse, ResultToAcceptedResponseTransformer, ResultToCreatedResponseTransformer, ResultToResponseTransformer (+4 more)

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
Cohesion: 0.12
Nodes (17): BackgroundService, DatabaseSeederOrchestrator, CancellationToken, Exception, IHostApplicationLifetime, ILogger, IServiceScopeFactory, LoggerMessage (+9 more)

### Community 97 - "StockReservationExpirySweepService"
Cohesion: 0.20
Nodes (10): EntityEntry, CancellationToken, Exception, Guid, ILogger, IOptions, LoggerMessage, Task (+2 more)

### Community 98 - ".SearchStoreProductsAsync"
Cohesion: 0.17
Nodes (11): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Description (+3 more)

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

### Community 104 - "RequireFeatureFilter"
Cohesion: 0.14
Nodes (12): IEndpointFilter, IFeatureManagerSnapshot, RequireFeatureFilter, ActivitySource, Counter, EndpointFilterDelegate, EndpointFilterInvocationContext, IResxLocalizer (+4 more)

### Community 105 - "Response"
Cohesion: 0.18
Nodes (9): IAM.Endpoints.Users.VersionNeutral.SelfRegisterByEmail, RouteGroupBuilder, Endpoint, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken (+1 more)

### Community 106 - "Common.Domain.ResultMonad"
Cohesion: 0.07
Nodes (28): IAM.Endpoints.Users, Common.Application.FeatureManagement, IAM.Endpoints.Otp, Notifications.Application.Persistence, IAM.Domain.Captcha, Common.InterModuleRequests.IAM, Common.InterModuleRequests.Contracts, IAM.Infrastructure.Captcha.Services (+20 more)

### Community 107 - "InventoryOptions"
Cohesion: 0.16
Nodes (13): InventoryOptions, MaxOccurrencesPerSeries, SafetyBufferQuantity, StockReservationExpirySweepBatchSize, StockReservationExpirySweepCron, WarehouseGatewayBaseUrl, WarehouseGatewayProvider, WebhookMaxBodyBytes (+5 more)

### Community 108 - "StockLevel"
Cohesion: 0.24
Nodes (7): DefaultIdType, StockLevel, ProductId, QuantityOnHand, StockLevelId, EntityTypeBuilder, StockLevelConfiguration

### Community 109 - ".IsRegisteredAsync"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, IsRegistered

### Community 110 - "KeycloakPermissionPolicyProvider"
Cohesion: 0.27
Nodes (8): AuthorizationOptions, AuthorizationPolicy, DefaultAuthorizationPolicyProvider, IAuthorizationPolicyProvider, ConcurrentDictionary, IOptions, Task, KeycloakPermissionPolicyProvider

### Community 111 - ".ReleaseStockReservationAsync"
Cohesion: 0.13
Nodes (14): CancellationToken, Task, IWarehouseGateway, CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint (+6 more)

### Community 112 - "OutboxCleanupJob"
Cohesion: 0.27
Nodes (8): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxCleanupJob

### Community 113 - "IPrivateObjectStore"
Cohesion: 0.29
Nodes (5): IPrivateObjectStore, CancellationToken, IReadOnlyCollection, Stream, Task

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
Cohesion: 0.16
Nodes (14): IHeaderDictionary, IValidator, CancellationToken, HttpContext, IOptions, JsonSerializerOptions, RouteGroupBuilder, Task (+6 more)

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

### Community 125 - "IOutboxMessage"
Cohesion: 0.22
Nodes (7): IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, DateTimeOffset

### Community 126 - "GlobalExceptionHandlingMiddleware"
Cohesion: 0.06
Nodes (34): ConsumerDefinition, IClientFactory, IConsumerConfigurator, IRegistrationContext, InterModuleRequestOptions, DependencyUnavailableRetryAfterSeconds, HandlerConcurrentMessageLimit, HandlerPrefetchCount (+26 more)

### Community 127 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 128 - "EmailRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, EmailRateLimitingPolicy (+1 more)

### Community 129 - "StoreId"
Cohesion: 0.15
Nodes (14): DefaultIdType, StoreId, CancellationToken, ILogger, LoggerMessage, ProductsDbContext, Task, CancellationToken (+6 more)

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.48
Nodes (7): Observability (OpenTelemetry), docker-compose.yml (Base Stack), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), Postgres Service (mm.postgres), RabbitMQ Service (mm.rabbitmq), Redis Service (mm.redis)

### Community 131 - ".AddInfrastructure"
Cohesion: 0.29
Nodes (7): HostOptions, JsonOptions, Assembly, IConfiguration, IResxLocalizer, IServiceCollection, IWebHostEnvironment

### Community 132 - "ObjectStorageOptions"
Cohesion: 0.09
Nodes (23): ObjectStorageOptions, AccessKey, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, ForcePathStyle (+15 more)

### Community 134 - "SendRequestBody"
Cohesion: 0.25
Nodes (8): SendContact, IReadOnlyList, SendRequestBody, HtmlContent, Sender, Subject, TextContent, To

### Community 135 - ".HandleAsync"
Cohesion: 0.18
Nodes (11): GetSeedUserIdsRequest, GetSeedUserIdsResponse, ICollection, CancellationToken, Task, GetSeedUserIdsRequestHandler, CancellationToken, IResult (+3 more)

### Community 136 - "VerifyPhoneOtpResponse"
Cohesion: 0.20
Nodes (10): OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, VerifyPhoneOtpResponseExtensions, CancellationToken (+2 more)

### Community 137 - ".WriteTooManyRequestsToResponse"
Cohesion: 0.13
Nodes (14): PartitionedRateLimiter, CancellationToken, Func, HttpContext, IConfiguration, IProblemDetailsService, IReadOnlyList, IResxLocalizer (+6 more)

### Community 139 - "ProductTemplate"
Cohesion: 0.15
Nodes (11): IReadOnlyList, List, ProductTemplate, Brand, Color, IsActive, Model, Products (+3 more)

### Community 140 - "ProcessedMessage"
Cohesion: 0.13
Nodes (16): ProcessedMessage, ConsumerName, MessageId, ProcessedOn, DateTimeOffset, DefaultIdType, UniqueConstraintException, InboxStore (+8 more)

### Community 141 - "IStronglyTypedId"
Cohesion: 0.06
Nodes (31): JsonConverter, StrictDateTimeOffsetJsonConverter, DateTimeOffset, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, StronglyTypedIdListReadOnlyJsonConverter (+23 more)

### Community 142 - "TokenRefreshRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenRefreshRateLimitingPolicy (+1 more)

### Community 143 - "Setup"
Cohesion: 0.11
Nodes (14): LoggerConfiguration, LoggerMinimumLevelConfiguration, Setup, IEnumerable, IHostEnvironment, KeyValuePair, Assembly, IConfiguration (+6 more)

### Community 144 - "Response"
Cohesion: 0.13
Nodes (14): RouteGroupBuilder, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate, CreatedOn, Email (+6 more)

### Community 145 - "JobRow"
Cohesion: 0.19
Nodes (9): JobRow, FailureKey, FinishedOn, Progress, QueuedOn, RequestedBy, StartedOn, Status (+1 more)

### Community 146 - ".RegisterAsync"
Cohesion: 0.05
Nodes (49): IInterModuleRequestClient, CancellationToken, Task, BindDeviceSessionRequest, BindDeviceSessionResponse, Guid, VerifyEmailOtpRequest, VerifyEmailOtpResponse (+41 more)

### Community 147 - ".AddOrUpdate"
Cohesion: 0.20
Nodes (8): Action, Expression, Func, Task, Action, Expression, Func, Task

### Community 148 - "Response"
Cohesion: 0.25
Nodes (6): IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, RouteGroupBuilder, Endpoint, Response, EmailVerificationToken, IsRegistered

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
Cohesion: 0.08
Nodes (18): Host.Infrastructure, elastic_serilog_sinks, microsoft_aspnetcore_httpoverrides, opentelemetry_exporter, opentelemetry_metrics, opentelemetry_opentelemetrybuilder, opentelemetry_resources, opentelemetry_trace (+10 more)

### Community 155 - "ISmsGateway"
Cohesion: 0.13
Nodes (13): CancellationToken, Task, ISmsGateway, SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, SmsMessage (+5 more)

### Community 156 - "Request"
Cohesion: 0.20
Nodes (10): Guid, Request, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken, Password (+2 more)

### Community 157 - "CreateStockLevelOnProductCreatedHandler"
Cohesion: 0.24
Nodes (8): CancellationToken, DefaultIdType, IFusionCache, ILogger, IOptions, LoggerMessage, Task, CreateStockLevelOnProductCreatedHandler

### Community 158 - "OtpOptions"
Cohesion: 0.18
Nodes (11): OtpOptions, DummyCode, EmailQuotaWindowMinutes, ExpirationInMinutes, Length, MaxSendsPerEmailPerWindow, MaxSendsPerPhonePerWindow, PhoneQuotaWindowMinutes (+3 more)

### Community 159 - "IInventoryDbContext"
Cohesion: 0.09
Nodes (20): DbSet, IInventoryDbContext, StockLevels, StockReservations, CancellationToken, RouteGroupBuilder, Task, TimeProvider (+12 more)

### Community 160 - "OtpServiceBase"
Cohesion: 0.21
Nodes (8): OtpCacheEntry, DateTimeOffset, CancellationToken, IFusionCache, SemaphoreSlim, Task, TimeSpan, OtpServiceBase

### Community 161 - "IntegrationEventOutbox"
Cohesion: 0.09
Nodes (22): IIntegrationEventOutbox, IntegrationEventOutbox, HasPending, IReadOnlyList, List, Lock, Setup, IServiceCollection (+14 more)

### Community 162 - "IEmailGateway"
Cohesion: 0.13
Nodes (13): CancellationToken, Task, IEmailGateway, IConfiguration, IFusionCache, IOptions, IServiceCollection, Setup (+5 more)

### Community 164 - "NotificationsDbContext"
Cohesion: 0.18
Nodes (10): DbContextOptions, DbSet, ILogger, TimeProvider, NotificationsDbContext, DeviceRegistrations, IApplicationBuilder, ILoggerFactory (+2 more)

### Community 165 - "ApplicationUserId"
Cohesion: 0.07
Nodes (28): IBackgroundUserContext, UserId, ICurrentUser, Id, IdAsString, Roles, SessionId, ICollection (+20 more)

### Community 166 - "IamModule"
Cohesion: 0.18
Nodes (10): Action, IApplicationBuilder, IEnumerable, RateLimiterOptions, IamModule, ActivitySourceNames, MeterNames, Name (+2 more)

### Community 167 - "PaginationRequest"
Cohesion: 0.05
Nodes (43): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Stores.v1.My.AuditLog, Products.Endpoints.Products.v1.AuditLog, PaginationRequest, After, IncludeTotal, PageNumber, PageSize (+35 more)

### Community 168 - "Response"
Cohesion: 0.22
Nodes (6): Products.Endpoints.ProductTemplates.v1.Create, RouteGroupBuilder, Setup, RouteGroupBuilder, Response, Id

### Community 169 - "SendSecurityAlertRequestHandler"
Cohesion: 0.27
Nodes (9): SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, CancellationToken, IOptions, RequestLocalizationOptions, Task (+1 more)

### Community 171 - "TokenResponseRepresentation"
Cohesion: 0.15
Nodes (12): List, DecisionRepresentation, Result, PermissionRepresentation, ResourceName, Scopes, TokenResponseRepresentation, AccessToken (+4 more)

### Community 172 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.09
Nodes (19): IAM.Endpoints.Users.VersionNeutral.Search, Common.Domain.StronglyTypedIds, Products.Domain.Products, Common.Application.JsonConverters, Common.Domain.Entities, Common.Domain.Aggregates, Products.Endpoints.Stores.v1.AddProduct, Common.Infrastructure.Persistence.ValueConverters (+11 more)

### Community 173 - "S3ObjectStoreCore"
Cohesion: 0.21
Nodes (9): AmazonServiceException, S3ObjectStoreCore, CancellationToken, IAmazonS3, IReadOnlyCollection, ResiliencePipeline, Stream, Task (+1 more)

### Community 174 - "system_diagnostics"
Cohesion: 0.08
Nodes (17): Common.Application.BackgroundJobs, BackgroundJobs.Telemetry, BackgroundJobs, IAM.Infrastructure.Auth, hangfire, hangfire_annotations, hangfire_dashboard, hangfire_server (+9 more)

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
Cohesion: 0.14
Nodes (14): CancellationToken, IReadOnlyDictionary, IReadOnlyList, Task, IPushGateway, PushMessage, CancellationToken, ILogger (+6 more)

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

### Community 184 - "OtpService"
Cohesion: 0.18
Nodes (7): OtpCodeGenerator, IFusionCache, IOptions, OtpService, IConfiguration, IServiceCollection, Setup

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

### Community 191 - "S3PrivateObjectStore"
Cohesion: 0.25
Nodes (7): S3PrivateObjectStore, CancellationToken, IReadOnlyCollection, Stream, Task, TimeProvider, TimeSpan

### Community 192 - ".From"
Cohesion: 0.36
Nodes (6): AspNetResult, ResxLocalizer, EndpointFilterDelegate, EndpointFilterInvocationContext, IStringLocalizer, ValueTask

### Community 193 - ".ListPermissionsAsync"
Cohesion: 0.28
Nodes (6): CancellationToken, IReadOnlyList, Task, IKeycloakPermissionClient, IReadOnlyList, GrantedPermission

### Community 194 - "EventDispatcher"
Cohesion: 0.31
Nodes (7): EventDispatcher, ActivitySource, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task

### Community 195 - ".ReserveStockAsync"
Cohesion: 0.22
Nodes (7): Inventory.Endpoints.StockReservations.v1.Reserve, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Id

### Community 196 - "TokenCreateRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenCreateRateLimitingPolicy (+1 more)

### Community 197 - ".UseModules"
Cohesion: 0.26
Nodes (6): ModuleRegistry, Exception, IApplicationBuilder, ILogger, LoggerMessage, WebApplication

### Community 198 - "InventoryModule"
Cohesion: 0.15
Nodes (10): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, InventoryModule, ActivitySourceNames, MeterNames (+2 more)

### Community 199 - "ProjectionUpsertOutcome"
Cohesion: 0.20
Nodes (7): Common.Application.Persistence.Projections, ProjectionEntity, SourceVersion, ProjectionUpsertOutcome, Inserted, Stale, Updated

### Community 200 - "NotificationsTelemetry"
Cohesion: 0.22
Nodes (5): ActivitySource, Counter, Meter, NotificationsTelemetry, UpDownCounter

### Community 201 - "KeycloakScopes"
Cohesion: 0.20
Nodes (9): Devices, Hangfire, KeycloakScopes, Products, ProductTemplates, Sessions, StockReservations, Stores (+1 more)

### Community 202 - "ProductTemplateId"
Cohesion: 0.29
Nodes (5): DefaultIdType, ProductTemplateId, CancellationToken, List, Task

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

### Community 214 - "ProjectionReconciliationOptions.cs"
Cohesion: 0.50
Nodes (4): ProjectionReconciliationOptions, MaxPages, PageSize, ProjectionReconciliationOptionsValidator

### Community 215 - "Response"
Cohesion: 0.13
Nodes (13): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, IReadOnlyCollection, RouteGroupBuilder, Endpoint, DateTimeOffset, Response, ClientId, DeviceName (+5 more)

### Community 216 - ".AddServices"
Cohesion: 0.25
Nodes (8): RecurringJobOptions, IRecurringBackgroundJobs, IConfiguration, ILogger, IServiceCollection, RecurringBackgroundJobsService, IRecurringJobManagerV2, TimeProvider

### Community 217 - "FeatureFlags"
Cohesion: 0.33
Nodes (5): Checkout, FeatureFlags, IAM, Notifications, Products

### Community 218 - "ICaptchaService"
Cohesion: 0.08
Nodes (24): SendPhoneOtpRequest, SendPhoneOtpResponse, CancellationToken, Task, ICaptchaService, CancellationToken, IFeatureManager, RouteGroupBuilder (+16 more)

### Community 219 - "BackgroundJobsService"
Cohesion: 0.19
Nodes (9): IBackgroundJobClientV2, IBackgroundJobs, Action, DateTimeOffset, Expression, Func, Task, TimeSpan (+1 more)

### Community 220 - "HttpWarehouseGateway"
Cohesion: 0.31
Nodes (8): ReleaseResponseBody, CancellationToken, HttpClient, HttpResponseMessage, Task, HttpWarehouseGateway, ReleaseRequestBody, ReleaseResponseBody

### Community 221 - "Keycloak realm as code"
Cohesion: 0.25
Nodes (7): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Secrets, Seed users (dev / test only)

### Community 222 - ".ListSessions"
Cohesion: 0.12
Nodes (17): DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, IReadOnlyList, CancellationToken, Task, DbSet, INotificationsDbContext (+9 more)

### Community 223 - "Request"
Cohesion: 0.22
Nodes (9): Request, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name, OwnerId, SearchTerm (+1 more)

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

### Community 228 - ".AddCommonCaching"
Cohesion: 0.29
Nodes (6): IMemoryCache, Setup, IConfiguration, IConnectionMultiplexer, IServiceCollection, JsonSerializerOptions

### Community 229 - ".AddCommonOptions"
Cohesion: 0.40
Nodes (4): Setup, IConfiguration, IHostEnvironment, IServiceCollection

### Community 230 - "InterModuleRequestHandler"
Cohesion: 0.08
Nodes (26): IInterModuleRequest, IInterModuleRequestHandler, CancellationToken, Task, InterModuleRequestHandler, CancellationToken, ConsumeContext, Task (+18 more)

### Community 231 - ".UseModule"
Cohesion: 0.22
Nodes (7): DashboardContext, IAuthorizationService, IDashboardAsyncAuthorizationFilter, IApplicationBuilder, IOptions, HangfireCustomAuthorizationFilter, Task

### Community 232 - "S3PublicObjectStore"
Cohesion: 0.13
Nodes (14): IPublicObjectStore, CancellationToken, IReadOnlyCollection, Task, Uri, S3PublicObjectStore, CancellationToken, IReadOnlyCollection (+6 more)

### Community 233 - ".HasPatternIndex"
Cohesion: 0.36
Nodes (6): IndexBuilder, TrigramIndexExtensions, EntityTypeBuilder, Expression, Func, ModelBuilder

### Community 234 - "Notifications.Application.Hubs"
Cohesion: 0.19
Nodes (4): Notifications.Infrastructure.Hubs, Notifications.Application.Hubs, microsoft_aspnetcore_signalr, NotificationGroupName

### Community 235 - "AuditLogEntry"
Cohesion: 0.09
Nodes (18): AuditLogEntry, AggregateId, AggregateType, Event, EventType, Version, DefaultIdType, ModelBuilder (+10 more)

### Community 236 - ".SendAsync"
Cohesion: 0.22
Nodes (5): CancellationToken, Task, SendMessageBody, Msg, No

### Community 237 - "ProblemDetailsExtensions.cs"
Cohesion: 0.40
Nodes (3): ProblemDetails, ProblemDetailsExtensions, ICollection

### Community 238 - ".UseInfrastructure"
Cohesion: 0.13
Nodes (12): IAuthenticationSchemeProvider, IMiddleware, IApplicationBuilder, HttpContext, RequestDelegate, Task, EnrichLogsWithUserInfoMiddleware, HttpContext (+4 more)

### Community 239 - "KeycloakAdminClient.cs"
Cohesion: 0.16
Nodes (8): IAM.Infrastructure.Keycloak.Representations, Inventory.Infrastructure.Gateway, Inventory.Application.Gateway, IAM.Infrastructure.Keycloak, OAuthErrors, UserAttributes, system_net_http_headers, system_net_http_json

### Community 240 - "V1StockReservationCommitConflictDetectedDomainEvent"
Cohesion: 0.22
Nodes (8): DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationCommitConflictDetectedDomainEvent, DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationCommittedDomainEvent

### Community 241 - ".NotFound"
Cohesion: 0.36
Nodes (4): CollectionExtensions, Func, ICollection, IEnumerable

### Community 242 - "Response"
Cohesion: 0.20
Nodes (8): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, RouteGroupBuilder, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 243 - "CustomValidator"
Cohesion: 0.04
Nodes (62): Inventory.Endpoints.StockReservations.v1.Commit, CustomRateLimitingOptionsValidator, FixedWindowValidator, DatabaseOptions, ConnectionString, DatabaseOptionsValidator, ResxLocalizationOptions, DefaultCulture (+54 more)

### Community 244 - "KeyedResilienceProfile"
Cohesion: 0.15
Nodes (13): AbstractValidator, KeyedResilienceProfile, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, RateLimitFailOpen (+5 more)

### Community 245 - ".AddProductAsync"
Cohesion: 0.11
Nodes (13): Products.Application.Products.DomainEventHandlers.v1, CancellationToken, Task, ProductCreatedIntegrationEventPublishingHandler, V1ProductCreatedDomainEventHandlers, ProductId, V1ProductCreatedDomainEvent, CancellationToken (+5 more)

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
Cohesion: 0.07
Nodes (30): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator, IReadOnlyCollection, GetActiveSessionIdsRequest, GetActiveSessionIdsResponse (+22 more)

### Community 251 - "GetProductRequest"
Cohesion: 0.39
Nodes (6): GetProductRequest, GetProductResponse, DefaultIdType, CancellationToken, Task, GetProductRequestHandler

### Community 252 - ".TryWriteAsync"
Cohesion: 0.40
Nodes (4): ProblemDetailsContext, ProblemDetailsServiceExtensions, IProblemDetailsService, Task

### Community 260 - "Infrastructure/Setup.cs"
Cohesion: 0.25
Nodes (6): Common.InterModuleRequests, Common.Infrastructure.Localization, Common.Infrastructure.Caching, microsoft_aspnetcore_authentication, microsoft_aspnetcore_http_json, IAssemblyReference

### Community 261 - "AuditableEntity"
Cohesion: 0.25
Nodes (8): AuditableEntity, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, Version, DateTimeOffset

### Community 262 - ".SendWithPipelineAsync"
Cohesion: 0.25
Nodes (7): HttpClientKeyedExtensions, CancellationToken, HttpClient, HttpRequestMessage, HttpResponseMessage, ResiliencePipeline, Task

### Community 263 - "Response"
Cohesion: 0.25
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegister, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 264 - ".CreatePresignedUrl"
Cohesion: 0.24
Nodes (5): HttpVerb, Uri, DateTime, Uri, Uri

### Community 265 - "BackgroundJobsModule"
Cohesion: 0.25
Nodes (7): BackgroundJobsModule, ActivitySourceNames, MeterNames, Name, StartupPriority, IEndpointRouteBuilder, IEnumerable

### Community 266 - ".AddCustomSwagger"
Cohesion: 0.15
Nodes (11): IOperationFilter, JsonValue, OpenApiOperation, OperationFilterContext, RemoveDefaultResponseSchemaFilter, IConfigureOptions, IServiceCollection, SwaggerGenOptions (+3 more)

### Community 267 - "AuditLogOptions"
Cohesion: 0.29
Nodes (7): AuditLogOptions, PerSchemaRetentionDays, PurgeBatchSize, RetentionCron, RetentionDays, AuditLogOptionsValidator, Dictionary

### Community 268 - "RedisOtpService"
Cohesion: 0.32
Nodes (6): CancellationToken, IConnectionMultiplexer, IOptions, Task, TimeSpan, RedisOtpService

### Community 269 - "IProductsDbContext"
Cohesion: 0.03
Nodes (51): CancellationToken, Task, DbSet, IProductsDbContext, Products, ProductTemplates, Stores, CancellationToken (+43 more)

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
Cohesion: 0.25
Nodes (3): KeycloakPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder

### Community 274 - "CurrentUser.cs"
Cohesion: 0.29
Nodes (4): Common.Infrastructure.Auth.Services, Common.Infrastructure.Auth, Setup, system_security_claims

### Community 275 - "BaseDbContext"
Cohesion: 0.12
Nodes (14): BaseDbContext, AuditLog, CancellationToken, DateTimeOffset, DbContextOptions, DbSet, ILogger, ModelConfigurationBuilder (+6 more)

### Community 276 - "Request"
Cohesion: 0.29
Nodes (7): Request, Description, Name, Price, ProductTemplateId, Quantity, RequestValidator

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
Cohesion: 0.22
Nodes (6): Notifications.Application.Push, Notifications.Infrastructure.Push, Notifications.Infrastructure.Push.Firebase, firebaseadmin, firebaseadmin_messaging, google_apis_auth_oauth2

### Community 284 - "RequestBody"
Cohesion: 0.33
Nodes (6): DateTimeOffset, DefaultIdType, RequestBody, ProductId, Quantity, ReservationDeadline

### Community 285 - "RequestBodyLimitFilter.cs"
Cohesion: 0.15
Nodes (14): Common.Endpoints.Webhooks, microsoft_aspnetcore_http_features, RequestBodyLimitEndpointExtensions, RequestBodyLimitFilter, Func, HttpContext, RequestBodyLimitMetadata, MaxBodyBytes (+6 more)

### Community 286 - "IOtpService"
Cohesion: 0.20
Nodes (8): CancellationToken, Task, TimeSpan, IOtpService, CancellationToken, Task, CancellationToken, Task

### Community 287 - "OtpVerificationOutcome"
Cohesion: 0.40
Nodes (4): OtpVerificationOutcome, InvalidOtp, Success, TooManyAttempts

### Community 288 - "DummySmsGateway"
Cohesion: 0.38
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, DummySmsGateway

### Community 289 - "SendForLogin/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForLogin, Request, CaptchaToken, PhoneNumber, RequestValidator

### Community 290 - "UploadObjectRequest"
Cohesion: 0.39
Nodes (3): UploadObjectRequest, IReadOnlyDictionary, Stream

### Community 291 - "StronglyTypedIdBinder"
Cohesion: 0.40
Nodes (4): IModelBinder, ModelBindingContext, StronglyTypedIdBinder, Task

### Community 292 - "IAuditableEntity"
Cohesion: 0.18
Nodes (10): IAuditableEntity, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, DateTimeOffset, CancellationToken, DbContextEventData (+2 more)

### Community 294 - "SendForRegistration/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, Request, CaptchaToken, PhoneNumber, RequestValidator

### Community 295 - "SmsOtpDispatchOutcome"
Cohesion: 0.33
Nodes (5): OtpDispatchErrors, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled

### Community 296 - "SecurityHeadersOptions.cs"
Cohesion: 0.50
Nodes (4): SecurityHeadersOptions, Headers, SecurityHeadersOptionsValidator, Dictionary

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

### Community 301 - "RequestBody"
Cohesion: 0.33
Nodes (6): RequestBody, Description, Name, Price, ProductTemplateId, Quantity

### Community 302 - "CommonValidations"
Cohesion: 0.22
Nodes (7): IResxLocalizer, IRuleBuilder, IRuleBuilderOptions, CommonValidations, IResxLocalizer, IRuleBuilder, IRuleBuilderOptions

### Community 303 - "DefaultResponsesOperationFilter"
Cohesion: 0.70
Nodes (3): OpenApiOperation, OperationFilterContext, DefaultResponsesOperationFilter

### Community 304 - ".SetRetryAfterHeader"
Cohesion: 0.40
Nodes (4): RetryAfterHeaderExtensions, HttpResponse, RateLimitLease, TimeSpan

### Community 306 - "ModulesOptions.cs"
Cohesion: 0.50
Nodes (4): ModulesOptions, EnabledModules, ModulesOptionsValidator, IReadOnlyList

### Community 307 - "SignalROptions"
Cohesion: 0.40
Nodes (5): SignalROptions, RedisConnectionString, RequireRedisBackplaneInProduction, UseRedisBackplane, SignalROptionsValidator

### Community 308 - "PublishOutcome"
Cohesion: 0.50
Nodes (4): PublishOutcome, Failed, Published, Skipped

### Community 310 - ".InvokeAsync"
Cohesion: 0.40
Nodes (4): EndpointFilterDelegate, EndpointFilterInvocationContext, IHttpMaxRequestBodySizeFeature, ValueTask

### Community 311 - "Common.Application.ModelBinders"
Cohesion: 0.08
Nodes (22): Products.Endpoints.Stores.v1.Deactivate, Products.Endpoints.ProductTemplates.v1.Deactivate, IAM.Endpoints.Users.VersionNeutral.Get, Inventory.Endpoints.StockReservations.v1.Release, Common.Application.ModelBinders, Products.Endpoints.ProductTemplates.v1.Activate, microsoft_aspnetcore_mvc_modelbinding, Request (+14 more)

### Community 312 - "Request"
Cohesion: 0.40
Nodes (5): RequestBody, Request, Body, Id, RequestValidator

### Community 313 - "Response"
Cohesion: 0.22
Nodes (6): Products.Endpoints.Stores.v1.Create, RouteGroupBuilder, Setup, RouteGroupBuilder, Response, Id

### Community 314 - "Inventory.Domain.StockReservations"
Cohesion: 0.06
Nodes (24): asp_versioning, asp_versioning_builder, asp_versioning_conventions, Products.Infrastructure.InterModuleRequestHandlers, Common.Application.Persistence.Inbox, Inventory.Infrastructure.InterModuleRequestHandlers, Inventory.Endpoints, Common.Endpoints.Versioning (+16 more)

### Community 317 - ".CreateAsync"
Cohesion: 0.20
Nodes (5): SearchValues, StringExtensions, Success, Func, Task

### Community 319 - "Products/v1/Update/Request.cs"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Products.v1.Update, RequestBody, Request, Body, Id, RequestBody, Description, Name (+3 more)

### Community 323 - "KeyedRow"
Cohesion: 0.50
Nodes (4): KeyedRow, Item, Sort, Tie

### Community 324 - "Products/v1/My/Update/Request.cs"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Products.v1.My.Update, RequestBody, Request, Body, Id, RequestBody, Description, Name (+3 more)

### Community 325 - ".AddNetGsm"
Cohesion: 0.33
Nodes (5): IConfiguration, IFusionCache, IOptions, IServiceCollection, Setup

### Community 326 - "ProductsTelemetry"
Cohesion: 0.50
Nodes (4): ActivitySource, Counter, Meter, ProductsTelemetry

### Community 334 - "JobStatus"
Cohesion: 0.25
Nodes (6): Common.Application.Jobs, JobStatus, Failed, Queued, Running, Succeeded

## Knowledge Gaps
- **978 isolated node(s):** `UserId`, `Id`, `IdAsString`, `Roles`, `SessionId` (+973 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 2263 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **100 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `ObjectStorageOptions`, `Infrastructure/Setup.cs`, `ApplySearchLanguageInterceptor`, `AuditLogOptions`, `microsoft_aspnetcore_http`, `EmailOptions`, `OutboxOptions`, `microsoft_extensions_configuration`, `JobHousekeepingOptions`, `Setup.Logger.cs`, `fluentvalidation`, `FirebasePushGateway.cs`, `OtpOptions`, `ObservabilityOptions`, `SecurityHeadersOptions.cs`, `ConfigureSwaggerOptions.cs`, `KeycloakOptions`, `microsoft_entityframeworkcore`, `CorsOptions`, `ModulesOptions.cs`, `CachingOptions`, `ReCaptchaService`, `RabbitMqOptions`, `SignalROptions`, `SmsOptions`, `IdentityScheme`, `Inventory.Domain.StockReservations`, `RequestLoggingOptions`, `FullTextSearchOptions`, `BackgroundJobsOptions`, `ProjectionReconciliationOptions.cs`, `PushOptions`, `Program.cs`, `ResiliencyOptions`, `Common.Domain.ResultMonad`, `InventoryOptions`, `KeycloakAdminClient.cs`, `CustomValidator`, `OpenApiOptions`, `DeviceRegistryReconciliationService`, `.AddCustomHealthChecks`, `GlobalExceptionHandlingMiddleware`?**
  _High betweenness centrality (0.203) - this node is a cross-community bridge._
- **Why does `Result` connect `Result` to `.ReserveSeriesAsync`, `KeycloakAdminClient`, `VerifyPhoneOtpResponse`, `FirebasePushGateway`, `Error`, `ProductTemplate`, `IKeycloakAdminClient`, `IProductsDbContext`, `.RegisterAsync`, `.TapWhenFeatureEnabledAsync`, `EmailMessage`, `.RevokeToken`, `ISmsGateway`, `Func`, `IInventoryDbContext`, `PersistenceQueryableExtensions`, `DummySmsGateway`, `IEmailGateway`, `SmsOtpDispatchOutcome`, `ResultTelemetryExtensions`, `PaginationQueryableExtensions`, `PaginationResponse`, `PushMessage`, `ReCaptchaService`, `EmailOtpDispatchOutcome`, `BrevoEmailGateway`, `.SearchProductTemplatesAsync`, `.CreateAsync`, `.SendCoreAsync`, `.ReserveStockAsync`, `DomainEvent`, `Response`, `.GetAuditLogAsync`, `.SearchStoresAsync`, `.CreateMyStoreAsync`, `StockReservation`, `ICaptchaService`, `.GetProductAsync`, `HttpWarehouseGateway`, `ProductId`, `.ListSessions`, `.SearchMyProductsAsync`, `NetGsmSmsGateway`, `.SearchStoreProductsAsync`, `InterModuleRequestHandler`, `.SendAsync`, `.IsRegisteredAsync`, `.ReleaseStockReservationAsync`, `V1StockReservationCommitConflictDetectedDomainEvent`, `.NotFound`, `Response`, `.AddProductAsync`, `.HandleWarehouseWebhookAsync`, `Response`?**
  _High betweenness centrality (0.111) - this node is a cross-community bridge._
- **Why does `ApplicationUserId` connect `ApplicationUserId` to `NotificationPayload`, `StoreId`, `AuditableEntity`, `KeycloakAdminClient`, `.HandleAsync`, `IKeycloakAdminClient`, `IStronglyTypedId`, `KeycloakPermissionAuthorizationHandler`, `Response`, `JobRow`, `.RegisterAsync`, `DeviceRegistration`, `.RevokeToken`, `IntegrationEventOutbox`, `For`, `IAuditableEntity`, `SendSecurityAlertRequestHandler`, `Common.Domain.StronglyTypedIds`, `PaginationResponse`, `Response`, `Common.Application.ModelBinders`, `Result`, `AuditableEntityResponse`, `.SearchStoresAsync`, `.CreateMyStoreAsync`, `.Configure`, `Store`, `.ListSessions`, `Request`, `InterModuleRequestHandler`, `Notifications.Application.Hubs`, `Response`, `DeviceRegistryReconciliationService`, `.TryDeserialize`, `Response`?**
  _High betweenness centrality (0.073) - this node is a cross-community bridge._
- **What connects `UserId`, `Id`, `IdAsString` to the rest of the system?**
  _978 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `.ReserveSeriesAsync` be split into smaller, more focused modules?**
  _Cohesion score 0.14166666666666666 - nodes in this community are weakly interconnected._
- **Should `Common.Application.DTOs` be split into smaller, more focused modules?**
  _Cohesion score 0.10526315789473684 - nodes in this community are weakly interconnected._
- **Should `KeycloakAdminClient` be split into smaller, more focused modules?**
  _Cohesion score 0.0992313067784766 - nodes in this community are weakly interconnected._