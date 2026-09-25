# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-09-25)

## Corpus Check
- 575 files · ~90,583 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 4942 nodes · 9762 edges · 389 communities (288 shown, 101 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 260 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `535ebb65`
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
- Common.Application.Options
- KeycloakPermissionAuthorizationHandler
- EmailOptions
- Product
- StockReservation
- Common.Domain.Events
- LogProductCatalogChangeHandler
- BoundedRequestCaptureStream
- DeviceRegistration
- IEmailGateway
- ICurrentUser
- Common.Infrastructure.Extensions
- EventDispatcher
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
- AdminRepresentations.cs
- RequestBody
- ObservabilityOptions
- Host.Swagger
- Request
- .SaveWithOutboxAsync
- CreateStoreRateLimitingPolicy
- KeycloakOptions
- .AddKeycloakInfrastructure
- PaginationQueryableExtensions
- Outbox Misuse Check
- Inventory.Domain.StockReservations
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
- .SendAsync
- BackgroundJobsService
- OutboxModule
- .AddNotificationsSignalR
- ValueObject
- GetSeedUserIdsRequest
- IntegrationEventHandlerBase
- Full-Text Search
- Response
- .WriteAsync
- AsNoTracking Coverage Check
- Setup
- Response
- AuditLogRetentionService
- CheckRegistrationRateLimitingPolicy
- DomainEvent
- IAuditableEntity
- .SearchStoresAsync
- Split-Deployment PoC
- Common.Application.EventBus
- .Configure
- HttpWarehouseGateway
- Store
- PushOptions
- Configuration-Driven Module Registration
- Setup
- .GetProductAsync
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
- DeviceRegistryReconcileJobRegistrar
- GlobalExceptionHandlingMiddleware
- Response
- EmailRateLimitingPolicy
- Seeder
- docker-compose.yml (Base Stack)
- .AddInfrastructure
- StoreId
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
- IRecurringBackgroundJobs
- IInterModuleRequestClient
- Response
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
- V1StoreCreatedDomainEvent
- ThrottledEmailGateway
- For
- NotificationsDbContext
- CurrentUser
- IamModule
- PaginationRequest
- Response
- SendSecurityAlertRequestHandler
- Notifications.Application/IAssemblyReference.cs
- IntegrationEventOutbox
- .MapEndpoint
- ProductTemplateId
- system_diagnostics
- PaginationResponse
- .FixedWindow
- CorsOptions
- .AddPushServices
- CachingOptions
- SmsOptions
- IAM.Application.Captcha.Services
- IAM.Domain
- SendRequestBody
- OtpService
- IdentityScheme
- Policies
- ReverseProxyOptions
- BoundedCaptureStream
- JobClaimExtensions
- ServiceAccountTokenCache
- ProjectionUpsertOutcome
- .AddServices
- .GetMeAsync
- .SeedStoresAsync
- StockReservationId
- TokenCreateRateLimitingPolicy
- StringExtensions
- InventoryModule
- FullTextSearchOptions
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
- RedisOtpService
- Stores/v1/My/Update/Request.cs
- BackgroundJobsTelemetry
- fluentvalidation
- .TryDeserialize
- .UpsertIfNewerAsync
- FeatureFlags
- .SendAsync
- IBackgroundJobs
- IamTelemetry
- Keycloak realm as code
- .ListSessions
- KeycloakPermission
- .MapEndpoint
- IInboxStore
- NetGsmSmsGateway
- RegisterRateLimitingPolicy
- .AddCommonCaching
- .AddCommonOptions
- InterModuleRequestHandler
- .UseModule
- DomainEventConverter
- .HasPatternIndex
- KeycloakPermissionAuthorizationHandler.cs
- BaseDbContext
- .HandleAsync
- ProblemDetailsExtensions.cs
- .UseInfrastructure
- system_net
- RabbitMqOptions
- AuditableEntityResponse
- DevicesOptions
- CustomValidator
- KeyedResilienceProfile
- SendForEmail/Request.cs
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
- SendPhoneOtpRequestHandler
- .GetAuditLogAsync
- .SendWithPipelineAsync
- Response
- ResxLocalizationOptions
- BackgroundJobsModule
- SwaggerDefaultValues
- AuditLogOptions
- .CommitStockReservationAsync
- IProductsDbContext
- .SendAsync
- .UpdateCurrentPushToken
- .AddResilientHttpClient
- .AddNetGsm
- Infrastructure/StringExtensions.cs
- .HandleAsync
- EnrichLogsWithUserInfoMiddleware
- SendResponseBody
- .TapWhenFeatureEnabledAsync
- Response
- .Apply
- .TryReadFromJsonAsync
- .ActivateProductTemplateAsync
- AuditableEntity
- RequestBody
- RequestBodyLimitFilter.cs
- .DeactivateProductTemplateAsync
- IOtpService
- DummySmsGateway
- .SetRetryAfterHeader
- IssueVerificationTokenRequestHandler
- .RemoveMyProductAsync
- .AddServices
- .MapEndpoints
- .UpdateStoreAsync
- v1/Request.cs
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
- InventoryTelemetry
- microsoft_aspnetcore_mvc
- ProductsTelemetry
- Setup
- ModulesOptions.cs
- Setup
- Setup
- .UseGlobalExceptionHandlingMiddleware
- SecurityHeadersOptions.cs
- RequestBody
- Infrastructure/Setup.cs
- system_runtime_compilerservices
- .AddGlobalExceptionHandlingMiddleware
- RequestBody
- ProjectionReconciliationOptions.cs
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
- `Gotchas` --references--> `FullTextSearchOptions`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Application/Options/FullTextSearchOptions.cs
- `Concurrent safety` --references--> `OutboxProcessor`  [INFERRED]
  docs/split-deployment-poc.md → src/Modules/Outbox/Outbox/OutboxProcessor.cs
- `Add search to a new entity _(Build checklist)_` --references--> `FullTextSearchOptions`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Application/Options/FullTextSearchOptions.cs
- `Configuration` --references--> `FullTextSearchOptions`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Application/Options/FullTextSearchOptions.cs
- `Full-Text Search` --references--> `FullTextSearchOptions`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Application/Options/FullTextSearchOptions.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Local Infrastructure Stack** — docker_compose_postgres, docker_compose_rabbitmq, docker_compose_redis, docker_compose_aspire_dashboard [EXTRACTED 1.00]

## Communities (389 total, 101 thin omitted)

### Community 0 - ".ReserveSeriesAsync"
Cohesion: 0.11
Nodes (18): Inventory.Endpoints.StockReservations.v1.ReserveSeries, GetProductRequest, GetProductResponse, DefaultIdType, CancellationToken, IOptions, List, RequestBody (+10 more)

### Community 1 - "NotificationPayload"
Cohesion: 0.23
Nodes (12): IHubContext, CancellationToken, IReadOnlyList, Task, INotificationDispatcher, Task, INotificationsClient, NotificationPayload (+4 more)

### Community 2 - "OutboxProcessor"
Cohesion: 0.21
Nodes (12): IPublishEndpoint, PublishOutcome, CancellationToken, Exception, ILogger, IOptions, IServiceScopeFactory, LoggerMessage (+4 more)

### Community 5 - "KeycloakAdminClient"
Cohesion: 0.09
Nodes (37): DateOnly, DateTimeOffset, IReadOnlyList, CreateKeycloakUser, GrantedPermission, KeycloakUser, KeycloakUserPage, KeycloakUserSession (+29 more)

### Community 6 - "RedisFixedWindowRateLimiter"
Cohesion: 0.13
Nodes (16): RateLimiter, RateLimiterStatistics, FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration, CancellationToken (+8 more)

### Community 7 - "ApplySearchLanguageInterceptor"
Cohesion: 0.10
Nodes (18): Add a new language/culture, Add search to a new entity _(Build checklist)_, Change the vector (weights, fields, or config), Extending and maintaining, File map _(Build)_, microsoft_entityframeworkcore_diagnostics, SaveChangesInterceptor, ISearchLocalized (+10 more)

### Community 8 - "FirebasePushGateway"
Cohesion: 0.17
Nodes (12): FirebaseApp, FirebaseMessaging, IDisposable, CancellationToken, Exception, IEnumerable, ILogger, IReadOnlyList (+4 more)

### Community 9 - ".RequireOtpTemplateForDefaultCulture"
Cohesion: 0.40
Nodes (4): Func, IConfiguration, IEnumerable, OtpTemplateConfiguration

### Community 10 - "Error"
Cohesion: 0.07
Nodes (22): Inventory.Domain.StockReservations.Errors, StringLocalizerExtensions, IStringLocalizer, Error, Key, ParameterName, StatusCode, SubErrors (+14 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.10
Nodes (26): IAM.Endpoints.Captcha.VersionNeutral, Products.Infrastructure.InterModuleRequestHandlers, Common.Application.Search, Products.Endpoints.Stores, Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Products.Endpoints.ProductTemplates (+18 more)

### Community 12 - "ApplicationUserId"
Cohesion: 0.15
Nodes (14): ApplicationUserId, IsEmpty, Value, DefaultIdType, CancellationToken, IReadOnlyList, Task, IKeycloakAdminClient (+6 more)

### Community 13 - "ISearchLanguageResolver"
Cohesion: 0.21
Nodes (8): Configuration, ISearchLanguageResolver, UniversalConfig, SearchLanguageResolver, UniversalConfig, IOptions, Setup, IServiceCollection

### Community 14 - "Common.Application.Options"
Cohesion: 0.05
Nodes (47): asp_versioning_apiexplorer, Common.Infrastructure.Modules, Notifications.Application.Otp, Notifications.Application.Sms, Notifications.Infrastructure.Devices, Notifications.Infrastructure.Email, Outbox, Common.Application.Caching (+39 more)

### Community 15 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.13
Nodes (17): AuthorizationHandler, AuthorizationHandlerContext, IAuthorizationRequirement, CancellationToken, ClaimsPrincipal, HttpContext, IFusionCache, IHttpContextAccessor (+9 more)

### Community 16 - "EmailOptions"
Cohesion: 0.09
Nodes (24): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+16 more)

### Community 17 - "Product"
Cohesion: 0.06
Nodes (30): ProductId, V1ProductDescriptionUpdatedDomainEvent, ProductId, V1ProductNameUpdatedDomainEvent, ProductTemplate, ProductTemplateId, Store, StoreId (+22 more)

### Community 18 - "StockReservation"
Cohesion: 0.06
Nodes (31): Inventory.Domain.StockReservations.DomainEvents.v1, DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationCommitConflictDetectedDomainEvent, DateTimeOffset, DefaultIdType, StockReservationId (+23 more)

### Community 19 - "Common.Domain.Events"
Cohesion: 0.06
Nodes (23): Products.Domain.Products.DomainEvents.v1, Products.Endpoints.Stores.v1.Search, Common.Domain.Events, Products.Endpoints.Products.v1.Search, Products.Endpoints.Stores.v1.My.Create, Products.Endpoints.ProductTemplates.v1.Create, Products.Domain.Products, Common.Application.DTOs (+15 more)

### Community 20 - "LogProductCatalogChangeHandler"
Cohesion: 0.19
Nodes (11): ProductCreatedIntegrationEvent, StoreCreatedIntegrationEvent, DefaultIdType, CancellationToken, DefaultIdType, IFusionCache, ILogger, IOptions (+3 more)

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.18
Nodes (7): BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position, Stream

### Community 22 - "DeviceRegistration"
Cohesion: 0.11
Nodes (16): DateTimeOffset, DefaultIdType, Guid, DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive (+8 more)

### Community 23 - "IEmailGateway"
Cohesion: 0.18
Nodes (11): IEmailGateway, CancellationToken, ILogger, LoggerMessage, Task, DummyEmailGateway, IConfiguration, IFusionCache (+3 more)

### Community 24 - "ICurrentUser"
Cohesion: 0.07
Nodes (26): ICurrentUser, Id, IdAsString, Roles, SessionId, ICollection, HttpContextExtensions, HttpContext (+18 more)

### Community 25 - "Common.Infrastructure.Extensions"
Cohesion: 0.10
Nodes (21): Products.Endpoints.Probe, Common.Infrastructure.RateLimiting, Products.Infrastructure.RateLimiting, Products.Endpoints.Products, Common.Infrastructure.Extensions, IAM.Endpoints.Otp.VersionNeutral, IAM.Infrastructure.RateLimiting, IAM.Endpoints.Tokens.VersionNeutral (+13 more)

### Community 26 - "EventDispatcher"
Cohesion: 0.09
Nodes (19): CancellationToken, Task, IEventHandler, CancellationToken, Task, CancellationToken, Task, IEvent (+11 more)

### Community 27 - "RequestResponseBodyLoggingMiddleware"
Cohesion: 0.22
Nodes (7): IDiagnosticContext, HttpContext, IList, IOptions, PathString, RequestDelegate, RequestResponseBodyLoggingMiddleware

### Community 28 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.08
Nodes (24): Common.Domain.StronglyTypedIds, Inventory.Infrastructure.Persistence.EntityConfigurations, Common.Application.Jobs, Common.Application.JsonConverters, Common.Domain.Entities, Common.Application.Pagination, Common.Infrastructure.Persistence.EntityConfigurations, Common.Domain.Aggregates (+16 more)

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
Cohesion: 0.10
Nodes (20): IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, DateTimeOffset, OutboxMessage (+12 more)

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
Cohesion: 0.07
Nodes (25): IHostBuilder, LoggerConfiguration, LoggerMinimumLevelConfiguration, ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics (+17 more)

### Community 40 - "Host.Swagger"
Cohesion: 0.27
Nodes (7): Host.Swagger, ISchemaFilter, microsoft_aspnetcore_mvc_apiexplorer, microsoft_openapi, StronglyTypedIdSchemaFilter, swashbuckle_aspnetcore_swaggergen, system_text_json_nodes

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

### Community 48 - "Inventory.Domain.StockReservations"
Cohesion: 0.07
Nodes (23): asp_versioning, asp_versioning_builder, asp_versioning_conventions, Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Products.Infrastructure.Persistence.Seeding, Inventory.Infrastructure.InterModuleRequestHandlers, Inventory.Endpoints (+15 more)

### Community 50 - "Response"
Cohesion: 0.12
Nodes (16): CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate (+8 more)

### Community 51 - "ProductsDbContext"
Cohesion: 0.13
Nodes (14): DbContextOptions, DbSet, ILogger, ProductTemplate, Store, TimeProvider, ProductsDbContext, Products (+6 more)

### Community 52 - "ReCaptchaService"
Cohesion: 0.06
Nodes (34): DateTime, FormUrlEncodedContent, ReCaptchaResponse, CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey (+26 more)

### Community 53 - "Request"
Cohesion: 0.15
Nodes (13): Guid, Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FirstName (+5 more)

### Community 54 - "SendEmailOtpRequestHandler"
Cohesion: 0.21
Nodes (11): EmailOtpDispatchErrors, EmailOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, SendEmailOtpRequest, SendEmailOtpResponse, IFusionCache (+3 more)

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
Cohesion: 0.15
Nodes (11): LikePattern, CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response (+3 more)

### Community 61 - "KeycloakTokenClient"
Cohesion: 0.14
Nodes (19): JsonWebTokenHandler, CancellationToken, Dictionary, Error, Exception, HttpClient, ILogger, IOptions (+11 more)

### Community 62 - "IDbContext"
Cohesion: 0.09
Nodes (21): DatabaseFacade, Deleted, EntityEntry, JobHousekeepingJob, CancellationToken, ILogger, IOptions, LoggerMessage (+13 more)

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
Cohesion: 0.11
Nodes (13): Notifications.Infrastructure.Hubs, Notifications.Application.Hubs, HubConnectionContext, IUserIdProvider, microsoft_aspnetcore_signalr, RedisOptions, IConfiguration, IConfigureOptions (+5 more)

### Community 68 - "ValueObject"
Cohesion: 0.25
Nodes (4): Common.Domain, IComparable, ValueObject, IEnumerable

### Community 69 - "GetSeedUserIdsRequest"
Cohesion: 0.39
Nodes (6): GetSeedUserIdsRequest, GetSeedUserIdsResponse, ICollection, CancellationToken, Task, GetSeedUserIdsRequestHandler

### Community 70 - "IntegrationEventHandlerBase"
Cohesion: 0.24
Nodes (11): IntegrationEventHandlerBase, MaxEventAge, CancellationToken, ConsumeContext, DefaultIdType, IFusionCache, ILogger, IOptions (+3 more)

### Community 71 - "Full-Text Search"
Cohesion: 0.11
Nodes (19): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Full-Text Search, Gotchas (+11 more)

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

### Community 79 - "DomainEvent"
Cohesion: 0.07
Nodes (29): AggregateRoot, Events, Id, Version, IReadOnlyCollection, List, IAggregateRoot, Events (+21 more)

### Community 80 - "IAuditableEntity"
Cohesion: 0.18
Nodes (10): IAuditableEntity, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, DateTimeOffset, CancellationToken, DbContextEventData (+2 more)

### Community 81 - ".SearchStoresAsync"
Cohesion: 0.15
Nodes (12): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Address (+4 more)

### Community 82 - "Split-Deployment PoC"
Cohesion: 0.18
Nodes (9): Concurrent safety, Cross-process call path, Files added by this PoC, How it works, How to run, Split-Deployment PoC, What this proves, README (Boilerplate Overview) (+1 more)

### Community 83 - "Common.Application.EventBus"
Cohesion: 0.05
Nodes (24): Common.Infrastructure.Persistence.Inbox, Common.Application.Persistence.Inbox, Host, Common.IntegrationEvents, Host.Middlewares, Common.Application.Persistence.Outbox, Common.Infrastructure.EventBus, Common.Application.EventBus (+16 more)

### Community 84 - ".Configure"
Cohesion: 0.13
Nodes (16): AuditableEntityConfiguration, EntityTypeBuilder, JobRowConfiguration, EntityTypeBuilder, StronglyTypedIdValueConverter, DefaultIdType, EntityTypeBuilder, StockReservationConfiguration (+8 more)

### Community 85 - "HttpWarehouseGateway"
Cohesion: 0.31
Nodes (8): ReleaseResponseBody, CancellationToken, HttpClient, HttpResponseMessage, Task, HttpWarehouseGateway, ReleaseRequestBody, ReleaseResponseBody

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

### Community 91 - ".SeedProductAsync"
Cohesion: 0.22
Nodes (7): CancellationToken, Task, CancellationToken, List, ProductTemplateId, StoreId, Task

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
Cohesion: 0.11
Nodes (18): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Request, MaxPrice (+10 more)

### Community 96 - "DatabaseSeederOrchestrator"
Cohesion: 0.07
Nodes (26): BackgroundService, IDatabaseSeeder, Priority, CancellationToken, Task, DatabaseSeederOrchestrator, CancellationToken, Exception (+18 more)

### Community 97 - "StockReservationExpirySweepService"
Cohesion: 0.18
Nodes (11): IServiceCollection, Setup, CancellationToken, Exception, Guid, ILogger, IOptions, LoggerMessage (+3 more)

### Community 98 - ".SearchStoreProductsAsync"
Cohesion: 0.09
Nodes (21): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, StoreId, Request (+13 more)

### Community 99 - "AuditLogRetentionJobRegistrar"
Cohesion: 0.22
Nodes (9): AuditLogRetentionJobRegistrar, CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task, Setup (+1 more)

### Community 100 - "OutboxDbContext"
Cohesion: 0.11
Nodes (16): DbContext, IEntityTypeConfiguration, IOutboxDbContext, OutboxMessages, CancellationToken, DbSet, Task, IConfiguration (+8 more)

### Community 101 - "ResiliencyOptions"
Cohesion: 0.15
Nodes (13): ResiliencyOptions, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, Keyed, MaxRetryAttempts (+5 more)

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
Cohesion: 0.18
Nodes (9): IAM.Endpoints.Users.VersionNeutral.SelfRegisterByEmail, RouteGroupBuilder, Endpoint, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken (+1 more)

### Community 106 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.08
Nodes (22): Notifications.Application.Push, IAM.Endpoints.Users, Common.Application.FeatureManagement, IAM.Endpoints.Otp, Notifications.Application.Persistence, Notifications.Domain.Devices, Common.InterModuleRequests.IAM, Common.InterModuleRequests.Contracts (+14 more)

### Community 107 - "InterModuleRequestOptions"
Cohesion: 0.12
Nodes (16): ConsumerDefinition, IConsumerConfigurator, IRegistrationContext, InterModuleRequestOptions, DependencyUnavailableRetryAfterSeconds, HandlerConcurrentMessageLimit, HandlerPrefetchCount, Timeouts (+8 more)

### Community 108 - "StockLevel"
Cohesion: 0.15
Nodes (11): Inventory.Domain.StockLevels.DomainEvents.v1, DefaultIdType, StockLevelId, V1StockLevelCreatedDomainEvent, DefaultIdType, StockLevel, ProductId, QuantityOnHand (+3 more)

### Community 109 - ".IsRegisteredAsync"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, IsRegistered

### Community 110 - "KeycloakPermissionPolicyProvider"
Cohesion: 0.24
Nodes (8): AuthorizationOptions, AuthorizationPolicy, DefaultAuthorizationPolicyProvider, IAuthorizationPolicyProvider, ConcurrentDictionary, IOptions, Task, KeycloakPermissionPolicyProvider

### Community 111 - ".ReleaseStockReservationAsync"
Cohesion: 0.12
Nodes (14): CancellationToken, Task, IWarehouseGateway, CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint (+6 more)

### Community 112 - "OutboxCleanupJob"
Cohesion: 0.27
Nodes (8): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxCleanupJob

### Community 113 - ".AddProductAsync"
Cohesion: 0.22
Nodes (8): CancellationToken, ProductTemplate, RouteGroupBuilder, Store, Task, Endpoint, Response, Id

### Community 114 - "Response"
Cohesion: 0.11
Nodes (17): IAM.Endpoints.Users.VersionNeutral.Me.Get, RouteGroupBuilder, Endpoint, DateOnly, DateTimeOffset, IReadOnlyCollection, Response, BirthDate (+9 more)

### Community 115 - "SkipOverlappingRecurringJobFilter"
Cohesion: 0.32
Nodes (5): SkipOverlappingRecurringJobFilter, ILogger, LoggerMessage, PerformedContext, PerformingContext

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
Nodes (16): IHeaderDictionary, IValidator, HmacSignatureVerifier, ReadOnlySpan, CancellationToken, HttpContext, IOptions, JsonSerializerOptions (+8 more)

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

### Community 125 - "DeviceRegistryReconcileJobRegistrar"
Cohesion: 0.31
Nodes (7): CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task, DeviceRegistryReconcileJobRegistrar

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
Cohesion: 0.29
Nodes (7): ILogger, LoggerMessage, ProductsDbContext, CancellationToken, List, Task, Seeder

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.48
Nodes (7): Observability (OpenTelemetry), docker-compose.yml (Base Stack), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), Postgres Service (mm.postgres), RabbitMQ Service (mm.rabbitmq), Redis Service (mm.redis)

### Community 131 - ".AddInfrastructure"
Cohesion: 0.29
Nodes (7): HostOptions, JsonOptions, Assembly, IConfiguration, IResxLocalizer, IServiceCollection, IWebHostEnvironment

### Community 132 - "StoreId"
Cohesion: 0.08
Nodes (24): Products.Endpoints.Stores.v1.Update, Products.Endpoints.Stores.v1.Create, Products.Endpoints.Stores.v1.Deactivate, DefaultIdType, StoreId, RequestBody, Request, Body (+16 more)

### Community 134 - "SendRequestBody"
Cohesion: 0.25
Nodes (8): SendContact, IReadOnlyList, SendRequestBody, HtmlContent, Sender, Subject, TextContent, To

### Community 135 - "Response"
Cohesion: 0.09
Nodes (18): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, RouteGroupBuilder, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken (+10 more)

### Community 136 - "ProductTemplates/v1/Search/Request.cs"
Cohesion: 0.20
Nodes (8): Products.Endpoints.ProductTemplates.v1.Search, Constants, Request, Brand, Color, Model, SearchTerm, RequestValidator

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
Nodes (39): BindDeviceSessionRequest, BindDeviceSessionResponse, Guid, OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts, VerifyPhoneOtpRequest (+31 more)

### Community 147 - "IRecurringBackgroundJobs"
Cohesion: 0.14
Nodes (13): RecurringJobOptions, IRecurringBackgroundJobs, Action, Expression, Func, Task, RecurringBackgroundJobsService, Action (+5 more)

### Community 148 - "IInterModuleRequestClient"
Cohesion: 0.09
Nodes (21): IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, IInterModuleRequestClient, VerifyEmailOtpRequest, VerifyEmailOtpResponse, EmailNormalization, CancellationToken, RouteGroupBuilder, Task (+13 more)

### Community 149 - "Response"
Cohesion: 0.18
Nodes (9): RouteGroupBuilder, Setup, RouteGroupBuilder, Response, AvailableQuantity, Description, Name, Price (+1 more)

### Community 150 - "StockReservationExpirySweepJobRegistrar"
Cohesion: 0.27
Nodes (8): IHostedService, CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task, StockReservationExpirySweepJobRegistrar

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

### Community 161 - "V1StoreCreatedDomainEvent"
Cohesion: 0.09
Nodes (20): Products.Application.Products.DomainEventHandlers.v1, Products.Application.Stores.DomainEventHandlers.v1, DomainEventHandlerBase, IEventHandlerWrapper, IIntegrationEventOutbox, Setup, IServiceCollection, CancellationToken (+12 more)

### Community 162 - "ThrottledEmailGateway"
Cohesion: 0.20
Nodes (8): CancellationToken, Task, EmailMessage, CancellationToken, IFusionCache, IOptions, Task, ThrottledEmailGateway

### Community 164 - "NotificationsDbContext"
Cohesion: 0.15
Nodes (13): DbSet, INotificationsDbContext, DeviceRegistrations, DbContextOptions, DbSet, ILogger, TimeProvider, NotificationsDbContext (+5 more)

### Community 165 - "CurrentUser"
Cohesion: 0.15
Nodes (12): CurrentUser, Id, IdAsString, IsAuthenticated, Principal, Roles, SessionId, ClaimsPrincipal (+4 more)

### Community 166 - "IamModule"
Cohesion: 0.18
Nodes (10): Action, IApplicationBuilder, IEnumerable, RateLimiterOptions, IamModule, ActivitySourceNames, MeterNames, Name (+2 more)

### Community 167 - "PaginationRequest"
Cohesion: 0.06
Nodes (32): Products.Endpoints.Stores.v1.AuditLog, IAM.Endpoints.Users.VersionNeutral.Search, Products.Endpoints.Stores.v1.My.AuditLog, Products.Endpoints.Products.v1.AuditLog, PaginationRequest, After, IncludeTotal, PageNumber (+24 more)

### Community 168 - "Response"
Cohesion: 0.29
Nodes (5): RouteGroupBuilder, Setup, RouteGroupBuilder, Response, Id

### Community 169 - "SendSecurityAlertRequestHandler"
Cohesion: 0.16
Nodes (15): IReadOnlyDictionary, SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, CancellationToken, IReadOnlyList, Task (+7 more)

### Community 171 - "IntegrationEventOutbox"
Cohesion: 0.18
Nodes (10): IntegrationEventOutbox, HasPending, IReadOnlyList, List, Lock, IntegrationEvent, CreatedOn, Id (+2 more)

### Community 172 - ".MapEndpoint"
Cohesion: 0.40
Nodes (3): RouteGroupBuilder, Setup, RouteGroupBuilder

### Community 173 - "ProductTemplateId"
Cohesion: 0.40
Nodes (3): ProductTemplateId, DefaultIdType, ProductTemplateId

### Community 174 - "system_diagnostics"
Cohesion: 0.16
Nodes (8): BackgroundJobs.Telemetry, hangfire_server, hangfire_storage, LoginMethods, SessionRevokedReasons, system_collections_concurrent, system_diagnostics, system_diagnostics_metrics

### Community 175 - "PaginationResponse"
Cohesion: 0.09
Nodes (23): JsonElement, AuditLogDto, DateTimeOffset, PaginationResponse, HasNext, HasPrevious, HasTotal, NextPageNumber (+15 more)

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
Nodes (21): CachingEntryDefaults, Duration, FactoryHardTimeout, FactorySoftTimeout, FailSafeMaxDuration, FailSafeThrottleDuration, CachingOptions, AllowInMemoryOnlyInProduction (+13 more)

### Community 180 - "SmsOptions"
Cohesion: 0.10
Nodes (21): SmsOptions, AppName, AttemptTimeoutSeconds, BaseUrl, MaxPerDay, MaxPerPhoneNumberPerDay, MaxRetryAttempts, MsgHeader (+13 more)

### Community 181 - "IAM.Application.Captcha.Services"
Cohesion: 0.29
Nodes (4): IAM.Domain.Captcha, IAM.Infrastructure.Captcha.Services, IAM.Application.Captcha.Services, IAM.Infrastructure.Captcha

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

### Community 191 - "ProjectionUpsertOutcome"
Cohesion: 0.20
Nodes (7): Common.Application.Persistence.Projections, ProjectionEntity, SourceVersion, ProjectionUpsertOutcome, Inserted, Stale, Updated

### Community 192 - ".AddServices"
Cohesion: 0.25
Nodes (6): IServerFilter, IConfiguration, ILogger, IServiceCollection, JobMetricsFilter, PerformingContext

### Community 193 - ".GetMeAsync"
Cohesion: 0.24
Nodes (7): CancellationToken, IReadOnlyList, Task, IKeycloakPermissionClient, CancellationToken, HttpContext, Task

### Community 194 - ".SeedStoresAsync"
Cohesion: 0.60
Nodes (3): CancellationToken, List, Task

### Community 195 - "StockReservationId"
Cohesion: 0.11
Nodes (13): Inventory.Endpoints.StockReservations.v1.Reserve, DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationReservedDomainEvent, DefaultIdType, StockReservationId, CancellationToken (+5 more)

### Community 196 - "TokenCreateRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenCreateRateLimitingPolicy (+1 more)

### Community 198 - "InventoryModule"
Cohesion: 0.15
Nodes (10): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, InventoryModule, ActivitySourceNames, MeterNames (+2 more)

### Community 199 - "FullTextSearchOptions"
Cohesion: 0.25
Nodes (8): FullTextSearchOptions, CultureToConfig, DefaultConfig, RankWeights, UseUnaccent, FullTextSearchOptionsValidator, Dictionary, IReadOnlyList

### Community 200 - "NotificationsTelemetry"
Cohesion: 0.22
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

### Community 211 - "RedisOtpService"
Cohesion: 0.32
Nodes (6): CancellationToken, IConnectionMultiplexer, IOptions, Task, TimeSpan, RedisOtpService

### Community 212 - "Stores/v1/My/Update/Request.cs"
Cohesion: 0.33
Nodes (6): Products.Endpoints.Stores.v1.My.Update, Request, Address, Description, Name, RequestValidator

### Community 213 - "BackgroundJobsTelemetry"
Cohesion: 0.20
Nodes (8): PerformedContext, BackgroundJobsTelemetry, ActivitySource, ConcurrentDictionary, Counter, Histogram, Meter, ObservableGauge

### Community 214 - "fluentvalidation"
Cohesion: 0.07
Nodes (38): common_application_localization_resources, IAM.Domain.Users, IAM.Endpoints.Otp.VersionNeutral.SendForLogin, Inventory.Endpoints.StockReservations.v1.WebhookCallback, Common.Domain.Extensions, Notifications.Infrastructure.Devices.UpdateCurrentPushToken, Common.Application.Validation, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke (+30 more)

### Community 216 - ".UpsertIfNewerAsync"
Cohesion: 0.20
Nodes (9): ICurrentDbContext, ProjectionUpsertExtensions, Action, CancellationToken, DbContext, DbSet, Func, Task (+1 more)

### Community 217 - "FeatureFlags"
Cohesion: 0.33
Nodes (5): Checkout, FeatureFlags, IAM, Notifications, Products

### Community 218 - ".SendAsync"
Cohesion: 0.05
Nodes (30): IClientFactory, CancellationToken, Task, MassTransitInterModuleRequestClient, CancellationToken, IOptions, Task, CancellationToken (+22 more)

### Community 219 - "IBackgroundJobs"
Cohesion: 0.26
Nodes (7): IBackgroundJobs, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 220 - "IamTelemetry"
Cohesion: 0.25
Nodes (4): ActivitySource, Counter, Meter, IamTelemetry

### Community 221 - "Keycloak realm as code"
Cohesion: 0.25
Nodes (7): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Secrets, Seed users (dev / test only)

### Community 222 - ".ListSessions"
Cohesion: 0.19
Nodes (12): DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, IReadOnlyList, CancellationToken, IReadOnlyCollection, RouteGroupBuilder, Task (+4 more)

### Community 223 - "KeycloakPermission"
Cohesion: 0.29
Nodes (3): KeycloakPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder

### Community 224 - ".MapEndpoint"
Cohesion: 0.40
Nodes (3): RouteGroupBuilder, RouteGroupBuilder, Setup

### Community 225 - "IInboxStore"
Cohesion: 0.16
Nodes (11): IInboxCleanupTarget, ModuleName, IInboxStore, CancellationToken, DateTimeOffset, DefaultIdType, Task, Setup (+3 more)

### Community 226 - "NetGsmSmsGateway"
Cohesion: 0.32
Nodes (7): Exception, HttpClient, ILogger, IOptions, JsonSerializerOptions, LoggerMessage, NetGsmSmsGateway

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
Nodes (23): IConsumer, IInterModuleRequest, IInterModuleRequestHandler, CancellationToken, Task, InterModuleRequestHandler, CancellationToken, ConsumeContext (+15 more)

### Community 231 - ".UseModule"
Cohesion: 0.22
Nodes (7): DashboardContext, IAuthorizationService, IDashboardAsyncAuthorizationFilter, IApplicationBuilder, IOptions, HangfireCustomAuthorizationFilter, Task

### Community 232 - "DomainEventConverter"
Cohesion: 0.13
Nodes (12): DateTimeOffset, ModelConfigurationBuilder, DomainEventConverter, JsonSerializerOptions, IntegrationEventConverter, JsonSerializerOptions, UtcDateTimeOffsetConverter, DateTimeOffset (+4 more)

### Community 233 - ".HasPatternIndex"
Cohesion: 0.36
Nodes (6): IndexBuilder, TrigramIndexExtensions, EntityTypeBuilder, Expression, Func, ModelBuilder

### Community 234 - "KeycloakPermissionAuthorizationHandler.cs"
Cohesion: 0.16
Nodes (9): Common.Infrastructure.Auth.Services, IAM.Infrastructure.Auth, Common.Infrastructure.Auth, hangfire_annotations, hangfire_dashboard, microsoft_aspnetcore_authentication_jwtbearer, microsoft_aspnetcore_authorization, microsoft_identitymodel_tokens (+1 more)

### Community 235 - "BaseDbContext"
Cohesion: 0.08
Nodes (21): AuditLogEntry, AggregateId, AggregateType, Event, EventType, Version, DefaultIdType, BaseDbContext (+13 more)

### Community 236 - ".HandleAsync"
Cohesion: 0.29
Nodes (4): CancellationToken, Task, CancellationToken, Task

### Community 237 - "ProblemDetailsExtensions.cs"
Cohesion: 0.40
Nodes (3): ProblemDetails, ProblemDetailsExtensions, ICollection

### Community 238 - ".UseInfrastructure"
Cohesion: 0.18
Nodes (9): IAuthenticationSchemeProvider, RequestBodyLimitMiddleware, RequestDelegate, IApplicationBuilder, HttpContext, IOptions, RequestDelegate, Task (+1 more)

### Community 239 - "system_net"
Cohesion: 0.12
Nodes (10): IAM.Infrastructure.Keycloak.Representations, Inventory.Infrastructure.Gateway, Inventory.Application.Gateway, IAM.Domain.Errors, IAM.Infrastructure.Keycloak, OAuthErrors, UserAttributes, system_net (+2 more)

### Community 240 - "RabbitMqOptions"
Cohesion: 0.11
Nodes (17): RabbitMqOptions, DefaultConcurrentMessageLimit, DefaultPrefetchCount, Host, Password, Port, RetryIntervalDeltaMs, RetryLimit (+9 more)

### Community 241 - "AuditableEntityResponse"
Cohesion: 0.29
Nodes (7): AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, DateTimeOffset

### Community 242 - "DevicesOptions"
Cohesion: 0.33
Nodes (6): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator, IReadOnlyCollection

### Community 243 - "CustomValidator"
Cohesion: 0.06
Nodes (39): Inventory.Endpoints.StockReservations.v1.Commit, CustomRateLimitingOptionsValidator, FixedWindowValidator, DatabaseOptions, ConnectionString, DatabaseOptionsValidator, ReverseProxyOptionsValidator, CustomValidator (+31 more)

### Community 244 - "KeyedResilienceProfile"
Cohesion: 0.15
Nodes (13): AbstractValidator, KeyedResilienceProfile, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, RateLimitFailOpen (+5 more)

### Community 245 - "SendForEmail/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForEmail, Request, CaptchaToken, Email, RequestValidator

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
Cohesion: 0.15
Nodes (10): CancellationToken, ILogger, IOptions, LoggerMessage, Task, TimeProvider, DeviceRegistryReconciliationService, IServiceCollection (+2 more)

### Community 250 - "Response"
Cohesion: 0.20
Nodes (9): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Description, Name, Price (+1 more)

### Community 251 - "ProductId"
Cohesion: 0.10
Nodes (18): Products.Endpoints.Products.v1.My.Update, Products.Endpoints.Products.v1.My.Get, Products.Endpoints.Products.v1.Update, DefaultIdType, ProductId, Request, Id, RequestValidator (+10 more)

### Community 252 - ".TryWriteAsync"
Cohesion: 0.40
Nodes (4): ProblemDetailsContext, ProblemDetailsServiceExtensions, IProblemDetailsService, Task

### Community 260 - "SendPhoneOtpRequestHandler"
Cohesion: 0.21
Nodes (11): OtpDispatchErrors, SendPhoneOtpRequest, SendPhoneOtpResponse, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, IFusionCache (+3 more)

### Community 261 - ".GetAuditLogAsync"
Cohesion: 0.38
Nodes (5): DbContextExtensions, CancellationToken, DbSet, JsonSerializerOptions, Task

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
Cohesion: 0.33
Nodes (5): IOperationFilter, JsonValue, OpenApiOperation, OperationFilterContext, SwaggerDefaultValues

### Community 267 - "AuditLogOptions"
Cohesion: 0.29
Nodes (7): AuditLogOptions, PerSchemaRetentionDays, PurgeBatchSize, RetentionCron, RetentionDays, AuditLogOptionsValidator, Dictionary

### Community 268 - ".CommitStockReservationAsync"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 269 - "IProductsDbContext"
Cohesion: 0.04
Nodes (39): CancellationToken, Task, DbSet, ProductTemplate, Store, IProductsDbContext, Products, ProductTemplates (+31 more)

### Community 270 - ".SendAsync"
Cohesion: 0.20
Nodes (6): CancellationToken, SendRequestBody, Task, SendMessageBody, Msg, No

### Community 271 - ".UpdateCurrentPushToken"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 272 - ".AddResilientHttpClient"
Cohesion: 0.22
Nodes (8): HttpStandardResilienceOptions, IHttpClientBuilder, Setup, Action, HttpClient, IOptions, IServiceCollection, IServiceProvider

### Community 273 - ".AddNetGsm"
Cohesion: 0.33
Nodes (5): IConfiguration, IFusionCache, IOptions, IServiceCollection, Setup

### Community 274 - "Infrastructure/StringExtensions.cs"
Cohesion: 0.40
Nodes (3): opentelemetry_exporter, OtlpExportProtocol, StringExtensions

### Community 275 - ".HandleAsync"
Cohesion: 0.29
Nodes (5): CancellationToken, IResult, RouteGroupBuilder, Task, Endpoint

### Community 276 - "EnrichLogsWithUserInfoMiddleware"
Cohesion: 0.33
Nodes (5): IMiddleware, HttpContext, RequestDelegate, Task, EnrichLogsWithUserInfoMiddleware

### Community 277 - "SendResponseBody"
Cohesion: 0.50
Nodes (4): SendResponseBody, Code, Description, JobId

### Community 278 - ".TapWhenFeatureEnabledAsync"
Cohesion: 0.33
Nodes (5): FeatureFlagResultExtensions, Action, Func, IFeatureManager, Task

### Community 279 - "Response"
Cohesion: 0.25
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.Create, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 281 - ".TryReadFromJsonAsync"
Cohesion: 0.40
Nodes (4): HttpContent, CancellationToken, Task, HttpContentJsonExtensions

### Community 282 - ".ActivateProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 283 - "AuditableEntity"
Cohesion: 0.25
Nodes (8): AuditableEntity, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, Version, DateTimeOffset

### Community 284 - "RequestBody"
Cohesion: 0.33
Nodes (6): DateTimeOffset, DefaultIdType, RequestBody, ProductId, Quantity, ReservationDeadline

### Community 285 - "RequestBodyLimitFilter.cs"
Cohesion: 0.18
Nodes (10): Common.Endpoints.Webhooks, microsoft_aspnetcore_http_features, RequestBodyLimitEndpointExtensions, RequestBodyLimitFilter, EndpointFilterDelegate, EndpointFilterInvocationContext, Func, HttpContext (+2 more)

### Community 286 - ".DeactivateProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 287 - "IOtpService"
Cohesion: 0.24
Nodes (8): CancellationToken, Task, TimeSpan, IOtpService, OtpVerificationOutcome, InvalidOtp, Success, TooManyAttempts

### Community 288 - "DummySmsGateway"
Cohesion: 0.38
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, DummySmsGateway

### Community 289 - ".SetRetryAfterHeader"
Cohesion: 0.40
Nodes (4): RetryAfterHeaderExtensions, HttpResponse, RateLimitLease, TimeSpan

### Community 290 - "IssueVerificationTokenRequestHandler"
Cohesion: 0.39
Nodes (6): IssueVerificationTokenRequest, IssueVerificationTokenResponse, CancellationToken, IOptions, Task, IssueVerificationTokenRequestHandler

### Community 291 - ".RemoveMyProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 294 - ".UpdateStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 295 - "v1/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Probe.v1, Request, Count, RequestValidator

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
Cohesion: 0.25
Nodes (6): OpenApiOperation, OperationFilterContext, RemoveDefaultResponseSchemaFilter, IConfigureOptions, IServiceCollection, SwaggerGenOptions

### Community 306 - ".PhoneNumberValidation"
Cohesion: 0.50
Nodes (3): IResxLocalizer, IRuleBuilder, IRuleBuilderOptions

### Community 307 - "SignalROptions"
Cohesion: 0.40
Nodes (5): SignalROptions, RedisConnectionString, RequireRedisBackplaneInProduction, UseRedisBackplane, SignalROptionsValidator

### Community 308 - "PublishOutcome"
Cohesion: 0.50
Nodes (4): PublishOutcome, Failed, Published, Skipped

### Community 310 - "InventoryTelemetry"
Cohesion: 0.50
Nodes (4): ActivitySource, Counter, Meter, InventoryTelemetry

### Community 311 - "microsoft_aspnetcore_mvc"
Cohesion: 0.06
Nodes (36): Products.Endpoints.ProductTemplates.v1.Deactivate, Products.Endpoints.Stores.v1.My.RemoveProduct, IAM.Endpoints.Users.VersionNeutral.Get, Inventory.Endpoints.StockReservations.v1.Release, Common.Application.ModelBinders, Products.Endpoints.ProductTemplates.v1.Activate, Products.Endpoints.Stores.v1.RemoveProduct, microsoft_aspnetcore_mvc (+28 more)

### Community 312 - "ProductsTelemetry"
Cohesion: 0.50
Nodes (4): ActivitySource, Counter, Meter, ProductsTelemetry

### Community 314 - "ModulesOptions.cs"
Cohesion: 0.50
Nodes (4): ModulesOptions, EnabledModules, ModulesOptionsValidator, IReadOnlyList

### Community 318 - "SecurityHeadersOptions.cs"
Cohesion: 0.50
Nodes (4): SecurityHeadersOptions, Headers, SecurityHeadersOptionsValidator, Dictionary

### Community 319 - "RequestBody"
Cohesion: 0.40
Nodes (5): RequestBody, Description, Name, Price, Quantity

### Community 320 - "Infrastructure/Setup.cs"
Cohesion: 0.25
Nodes (6): Common.InterModuleRequests, Common.Infrastructure.Localization, Common.Infrastructure.Caching, microsoft_aspnetcore_authentication, microsoft_aspnetcore_http_json, IAssemblyReference

### Community 324 - "RequestBody"
Cohesion: 0.40
Nodes (5): RequestBody, Description, Name, Price, Quantity

### Community 326 - "ProjectionReconciliationOptions.cs"
Cohesion: 0.50
Nodes (4): ProjectionReconciliationOptions, MaxPages, PageSize, ProjectionReconciliationOptionsValidator

## Knowledge Gaps
- **952 isolated node(s):** `Id`, `IdAsString`, `Roles`, `SessionId`, `JwtClaimNames` (+947 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 2229 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **101 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `ResxLocalizationOptions`, `AuditLogOptions`, `Common.Domain.ResultMonad`, `Setup`, `EmailOptions`, `OutboxOptions`, `Common.Infrastructure.Extensions`, `Setup.Logger.cs`, `Common.Domain.StronglyTypedIds`, `OtpOptions`, `ObservabilityOptions`, `KeycloakOptions`, `Inventory.Domain.StockReservations`, `CorsOptions`, `CachingOptions`, `ReCaptchaService`, `SignalROptions`, `SmsOptions`, `IAM.Application.Captcha.Services`, `IdentityScheme`, `ModulesOptions.cs`, `IDbContext`, `RequestLoggingOptions`, `SecurityHeadersOptions.cs`, `Infrastructure/Setup.cs`, `ProjectionReconciliationOptions.cs`, `FullTextSearchOptions`, `BackgroundJobsOptions`, `Common.Application.EventBus`, `fluentvalidation`, `PushOptions`, `ResiliencyOptions`, `InventoryOptions`, `KeycloakPermissionAuthorizationHandler.cs`, `InterModuleRequestOptions`, `Common.InterModuleRequests.Contracts`, `system_net`, `RabbitMqOptions`, `DevicesOptions`, `CustomValidator`, `OpenApiOptions`, `.AddCustomHealthChecks`?**
  _High betweenness centrality (0.160) - this node is a cross-community bridge._
- **Why does `Result` connect `Result` to `.ReserveSeriesAsync`, `SendPhoneOtpRequestHandler`, `.GetAuditLogAsync`, `FirebasePushGateway`, `Error`, `ProductTemplate`, `ApplicationUserId`, `.CommitStockReservationAsync`, `.SendAsync`, `.UpdateCurrentPushToken`, `Response`, `IProductsDbContext`, `.RegisterAsync`, `StockReservation`, `IInterModuleRequestClient`, `.TapWhenFeatureEnabledAsync`, `IEmailGateway`, `ICurrentUser`, `.ActivateProductTemplateAsync`, `ISmsGateway`, `.DeactivateProductTemplateAsync`, `.SingleAsResult`, `.RefreshToken`, `ThrottledEmailGateway`, `DummySmsGateway`, `.RemoveMyProductAsync`, `.UpdateStoreAsync`, `SendSecurityAlertRequestHandler`, `ResultTelemetryExtensions`, `PaginationQueryableExtensions`, `PaginationResponse`, `Response`, `.AddPushServices`, `ReCaptchaService`, `SendEmailOtpRequestHandler`, `BrevoEmailGateway`, `.SearchProductTemplatesAsync`, `.SendAsync`, `.GetMeAsync`, `StockReservationId`, `Response`, `Response`, `DomainEvent`, `.SearchStoresAsync`, `HttpWarehouseGateway`, `.SendAsync`, `.GetProductAsync`, `.AddProductToMyStoreAsync`, `.ListSessions`, `.SearchMyProductsAsync`, `NetGsmSmsGateway`, `.SearchStoreProductsAsync`, `.IsRegisteredAsync`, `.ReleaseStockReservationAsync`, `.AddProductAsync`, `Response`, `.HandleWarehouseWebhookAsync`, `Response`, `Response`?**
  _High betweenness centrality (0.158) - this node is a cross-community bridge._
- **Why does `ApplicationUserId` connect `ApplicationUserId` to `NotificationPayload`, `KeycloakAdminClient`, `IStronglyTypedId`, `IProductsDbContext`, `KeycloakPermissionAuthorizationHandler`, `Response`, `.RegisterAsync`, `LogProductCatalogChangeHandler`, `DeviceRegistration`, `ICurrentUser`, `AuditableEntity`, `Common.Domain.StronglyTypedIds`, `JobRow`, `V1StoreCreatedDomainEvent`, `For`, `CurrentUser`, `SendSecurityAlertRequestHandler`, `PaginationResponse`, `Response`, `microsoft_aspnetcore_mvc`, `KeycloakTokenClient`, `.SeedStoresAsync`, `GetSeedUserIdsRequest`, `Response`, `IAuditableEntity`, `.SearchStoresAsync`, `.Configure`, `Store`, `.TryDeserialize`, `.ListSessions`, `.SearchStoreProductsAsync`, `InterModuleRequestHandler`, `AuditableEntityResponse`, `Response`, `NotificationsHub`, `Response`?**
  _High betweenness centrality (0.068) - this node is a cross-community bridge._
- **What connects `Id`, `IdAsString`, `Roles` to the rest of the system?**
  _952 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `.ReserveSeriesAsync` be split into smaller, more focused modules?**
  _Cohesion score 0.10869565217391304 - nodes in this community are weakly interconnected._
- **Should `KeycloakAdminClient` be split into smaller, more focused modules?**
  _Cohesion score 0.09013914095583787 - nodes in this community are weakly interconnected._
- **Should `RedisFixedWindowRateLimiter` be split into smaller, more focused modules?**
  _Cohesion score 0.13333333333333333 - nodes in this community are weakly interconnected._