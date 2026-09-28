# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-09-25)

## Corpus Check
- 577 files · ~91,296 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 4956 nodes · 9795 edges · 387 communities (289 shown, 98 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 261 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `4c0f2323`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- .ReserveSeriesAsync
- NotificationPayload
- OutboxProcessor
- Common.Domain.StronglyTypedIds
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
- DomainEvent
- IInterModuleRequest
- Product
- IntegrationEvent
- BoundedRequestCaptureStream
- DeviceRegistration
- IEmailGateway
- ICurrentUser
- Common.Infrastructure.Extensions
- IEvent
- RequestResponseBodyLoggingMiddleware
- fluentvalidation
- Result
- AuditableEntity
- NotificationsModule
- .SingleAsResult
- IKeycloakTokenClient
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
- microsoft_entityframeworkcore
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
- JobHousekeepingJob
- RequestLoggingOptions
- .SendAsync
- .Schedule
- OutboxModule
- .AddNotificationsSignalR
- ValueObject
- StockReservation
- IntegrationEventHandlerBase
- FullTextSearchOptions
- Response
- .WriteAsync
- AsNoTracking Coverage Check
- Setup
- Response
- AuditLogRetentionService
- CheckRegistrationRateLimitingPolicy
- IAggregateRoot
- .UpdateCurrentPushToken
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
- CaptchaOptions
- .RegisterAsync
- Common.InterModuleRequests.Contracts
- InterModuleRequestOptions
- StockLevel
- .IsRegisteredAsync
- KeycloakPermissionPolicyProvider
- .ReleaseStockReservationAsync
- OutboxCleanupJob
- .AddPersistence
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
- Seeder
- docker-compose.yml (Base Stack)
- .AddInfrastructure
- StoreId
- Consumer Idempotency (IntegrationEventHandlerBase)
- SendRequestBody
- .HandleAsync
- InventoryOptions
- .WriteTooManyRequestsToResponse
- RequireFeatureFilter
- ProductTemplate
- ProcessedMessage
- IStronglyTypedId
- TokenRefreshRateLimitingPolicy
- Setup
- Response
- RequestBody
- .BindDeviceAsync
- .AddOrUpdate
- .CreateTokensByEmail
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
- ThrottledEmailGateway
- For
- NotificationsDbContext
- ApplicationUserId
- IamModule
- PaginationRequest
- Response
- SendSecurityAlertRequestHandler
- Notifications.Application/IAssemblyReference.cs
- .RegisterAsync
- Request
- ProductTemplateId
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
- Notifications.Application.Hubs
- .GetMeAsync
- EventDispatcher
- .ReserveStockAsync
- TokenCreateRateLimitingPolicy
- SeedingCompletionTracker
- InventoryModule
- ReCaptchaResponse
- NotificationsTelemetry
- KeycloakScopes
- BaseDbContext
- SmsRateLimitingPolicy
- Configuration-Driven Module Loading
- StatelessInboxStore
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- BackgroundJobsOptions
- Key decisions
- ProductTemplates/v1/Create/Request.cs
- BackgroundJobsTelemetry
- Common.Application.Validation
- .ListSessions
- .From
- FeatureFlags
- .SendOtp
- BackgroundJobsService
- HttpWarehouseGateway
- Keycloak realm as code
- GetDeviceSessionsRequest
- Request
- .MapEndpoint
- IInboxStore
- NetGsmSmsGateway
- RegisterRateLimitingPolicy
- .AddCommonCaching
- .AddCommonOptions
- InterModuleRequestHandler
- .UseModule
- IDbContext
- .HasPatternIndex
- IAMModule.cs
- AuditLogEntry
- V1StoreCreatedDomainEvent
- ProblemDetailsExtensions.cs
- .UseInfrastructure
- KeycloakTokenClient.cs
- .Apply
- .AddServices
- GetActiveSessionIdsRequest
- CustomValidator
- KeyedResilienceProfile
- .HandleAsync
- Request
- OpenApiOptions
- SendErrorBody
- DeviceRegistryReconciliationService
- Response
- AuditableEntityResponse
- .TryWriteAsync
- Products.Domain/IAssemblyReference.cs
- IAM.Application/IAssemblyReference.cs
- Products.Infrastructure/IAssemblyReference.cs
- Products.Application/IAssemblyReference.cs
- Common.Infrastructure/IAssemblyReference.cs
- Notifications.Domain/IAssemblyReference.cs
- IAM.Infrastructure/IAssemblyReference.cs
- Infrastructure/Setup.cs
- .GetAuditLogAsync
- .SendWithPipelineAsync
- Response
- InboxStore
- .AddProductToMyStoreAsync
- SwaggerDefaultValues
- AuditLogOptions
- DevicesOptions
- IProductsDbContext
- BackgroundJobsModule
- InventoryTelemetry
- .AddResilientHttpClient
- KeycloakPermission
- .CommitStockReservationAsync
- VersionNeutral/Get/Request.cs
- ResxLocalizationOptions
- SendResponseBody
- .TapWhenFeatureEnabledAsync
- Response
- SendForEmail/Request.cs
- .TryReadFromJsonAsync
- JobHousekeepingOptions
- TokenResponseRepresentation
- RequestBody
- RequestBodyLimitFilter.cs
- v1/Request.cs
- IOtpService
- DummySmsGateway
- My/Create/Request.cs
- HttpContextTargetingContextAccessor.cs
- StronglyTypedIdBinder
- .AddServices
- .MapEndpoints
- UpdateCurrentPushToken/Request.cs
- Infrastructure/StringExtensions.cs
- SecurityHeadersOptions.cs
- Common.Application.EventBus
- .AddCustomSwagger
- .MapEndpoint
- ResultTelemetryExtensions
- ModulesOptions.cs
- .EmailVerificationTokenValidation
- DefaultResponsesOperationFilter
- .SetRetryAfterHeader
- StronglyTypedIdSchemaFilter.cs
- .PhoneNumberValidation
- SignalROptions
- PublishOutcome
- .Capture
- .InvokeAsync
- microsoft_aspnetcore_mvc
- HttpContextExtensions
- .MapEndpoint
- Versioning/Setup.cs
- Setup
- Setup
- StringExtensions
- Products.Endpoints
- Request
- Notifications.Infrastructure
- system_runtime_compilerservices
- .CreateStorePolicy
- IAutoMigrateMarker.cs
- .UpdateMyProductAsync
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
1. `Result` - 136 edges
2. `Common.Application.Options` - 134 edges
3. `Common.Domain.ResultMonad` - 105 edges
4. `CustomValidator` - 82 edges
5. `ApplicationUserId` - 75 edges
6. `Common.Application.Validation` - 73 edges
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
- `Add search to a new entity _(Build checklist)_` --references--> `ApplySearchLanguageInterceptor`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Infrastructure/Persistence/Auditing/ApplySearchLanguageInterceptor.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Local Infrastructure Stack** — docker_compose_postgres, docker_compose_rabbitmq, docker_compose_redis, docker_compose_aspire_dashboard [EXTRACTED 1.00]

## Communities (387 total, 98 thin omitted)

### Community 0 - ".ReserveSeriesAsync"
Cohesion: 0.11
Nodes (18): Inventory.Endpoints.StockReservations.v1.ReserveSeries, GetProductRequest, GetProductResponse, DefaultIdType, CancellationToken, IOptions, List, RequestBody (+10 more)

### Community 1 - "NotificationPayload"
Cohesion: 0.23
Nodes (12): IHubContext, CancellationToken, IReadOnlyList, Task, INotificationDispatcher, Task, INotificationsClient, NotificationPayload (+4 more)

### Community 2 - "OutboxProcessor"
Cohesion: 0.21
Nodes (12): IPublishEndpoint, PublishOutcome, CancellationToken, Exception, ILogger, IOptions, IServiceScopeFactory, LoggerMessage (+4 more)

### Community 3 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.06
Nodes (21): Products.Domain.Products.DomainEvents.v1, IAM.Endpoints.Users.VersionNeutral.Search, Common.Domain.StronglyTypedIds, Products.Endpoints.Stores.v1.Search, Common.Domain.Events, Products.Endpoints.Products.v1.My.Get, Products.Domain.Products, Common.Application.DTOs (+13 more)

### Community 5 - "KeycloakAdminClient"
Cohesion: 0.17
Nodes (16): CancellationToken, Error, Func, HttpClient, HttpRequestMessage, HttpResponseMessage, IFusionCache, ILogger (+8 more)

### Community 6 - "RedisFixedWindowRateLimiter"
Cohesion: 0.13
Nodes (16): RateLimiter, RateLimiterStatistics, FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration, CancellationToken (+8 more)

### Community 7 - "ApplySearchLanguageInterceptor"
Cohesion: 0.17
Nodes (10): SaveChangesInterceptor, ApplyAuditingInterceptor, CancellationToken, DbContextEventData, InterceptionResult, TimeProvider, ValueTask, ApplySearchLanguageInterceptor (+2 more)

### Community 8 - "FirebasePushGateway"
Cohesion: 0.14
Nodes (15): FirebaseApp, FirebaseMessaging, IDisposable, IReadOnlyDictionary, IReadOnlyList, PushMessage, CancellationToken, Exception (+7 more)

### Community 9 - ".RequireOtpTemplateForDefaultCulture"
Cohesion: 0.40
Nodes (4): Func, IConfiguration, IEnumerable, OtpTemplateConfiguration

### Community 10 - "Error"
Cohesion: 0.07
Nodes (23): Inventory.Domain.StockReservations.Errors, StringLocalizerExtensions, IStringLocalizer, Error, Key, ParameterName, StatusCode, SubErrors (+15 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.10
Nodes (25): IAM.Endpoints.Captcha.VersionNeutral, Common.Application.Search, Products.Endpoints.Stores, Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Products.Endpoints.ProductTemplates, Inventory.Domain.StockReservations (+17 more)

### Community 12 - "IKeycloakAdminClient"
Cohesion: 0.19
Nodes (12): CancellationToken, IReadOnlyList, Task, IKeycloakAdminClient, DateOnly, DateTimeOffset, IReadOnlyList, CreateKeycloakUser (+4 more)

### Community 13 - "ISearchLanguageResolver"
Cohesion: 0.12
Nodes (14): Configuration, ISearchLanguageResolver, UniversalConfig, SearchLanguageResolver, UniversalConfig, IOptions, Setup, IApplicationBuilder (+6 more)

### Community 14 - "Common.Application.Options"
Cohesion: 0.04
Nodes (54): Common.Infrastructure.Modules, Notifications.Application.Sms, Notifications.Infrastructure.Devices, Inventory.Endpoints, Common.Endpoints.Versioning, Notifications.Infrastructure.Email, Products.Endpoints.Probe, Host (+46 more)

### Community 15 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.13
Nodes (17): AuthorizationHandler, AuthorizationHandlerContext, IAuthorizationRequirement, CancellationToken, ClaimsPrincipal, HttpContext, IFusionCache, IHttpContextAccessor (+9 more)

### Community 16 - "EmailOptions"
Cohesion: 0.09
Nodes (24): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+16 more)

### Community 17 - "DomainEvent"
Cohesion: 0.08
Nodes (28): AggregateRoot, Events, Id, Version, IReadOnlyCollection, List, DomainEvent, CreatedOn (+20 more)

### Community 18 - "IInterModuleRequest"
Cohesion: 0.11
Nodes (16): IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, IInterModuleRequest, IssueVerificationTokenRequest, IssueVerificationTokenResponse, EmailNormalization, CancellationToken, RouteGroupBuilder, Task (+8 more)

### Community 19 - "Product"
Cohesion: 0.06
Nodes (36): DefaultIdType, ProductTemplate, ProductTemplateId, Store, StoreId, Product, Description, Language (+28 more)

### Community 20 - "IntegrationEvent"
Cohesion: 0.13
Nodes (16): IntegrationEvent, CreatedOn, Id, DateTimeOffset, DefaultIdType, ProductCreatedIntegrationEvent, StoreCreatedIntegrationEvent, DefaultIdType (+8 more)

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
Nodes (26): ICurrentUser, Id, IdAsString, Roles, SessionId, ICollection, CancellationToken, Task (+18 more)

### Community 25 - "Common.Infrastructure.Extensions"
Cohesion: 0.18
Nodes (13): Common.Infrastructure.RateLimiting, Products.Infrastructure.RateLimiting, Common.Infrastructure.Extensions, IAM.Infrastructure.RateLimiting, microsoft_aspnetcore_ratelimiting, microsoft_extensions_logging_abstractions, polly_ratelimiting, polly_registry (+5 more)

### Community 26 - "IEvent"
Cohesion: 0.11
Nodes (13): CancellationToken, Task, IEventHandler, CancellationToken, Task, IEventHandlerWrapper, CancellationToken, Task (+5 more)

### Community 27 - "RequestResponseBodyLoggingMiddleware"
Cohesion: 0.22
Nodes (7): IDiagnosticContext, HttpContext, IList, IOptions, PathString, RequestDelegate, RequestResponseBodyLoggingMiddleware

### Community 28 - "fluentvalidation"
Cohesion: 0.10
Nodes (23): common_application_localization_resources, IAM.Domain.Users, Inventory.Endpoints.StockReservations.v1.WebhookCallback, Products.Endpoints.Products.v1.Update, Common.Application.JsonConverters, Common.Domain.Extensions, Common.Application.Pagination, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke (+15 more)

### Community 29 - "Result"
Cohesion: 0.13
Nodes (14): Result, Error, IsFailure, Success, Value, Func, Task, AsyncExtensions (+6 more)

### Community 30 - "AuditableEntity"
Cohesion: 0.05
Nodes (37): Common.Application.Persistence.Projections, ICurrentDbContext, JobRow, FailureKey, FinishedOn, Progress, QueuedOn, RequestedBy (+29 more)

### Community 31 - "NotificationsModule"
Cohesion: 0.15
Nodes (10): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, NotificationsModule, ActivitySourceNames, MeterNames (+2 more)

### Community 32 - ".SingleAsResult"
Cohesion: 0.18
Nodes (10): CollectionExtensions, Func, ICollection, IEnumerable, PersistenceQueryableExtensions, CancellationToken, Expression, Func (+2 more)

### Community 33 - "IKeycloakTokenClient"
Cohesion: 0.13
Nodes (14): IAM.Endpoints.Tokens.VersionNeutral.Refresh, CancellationToken, Task, IKeycloakTokenClient, CancellationToken, RouteGroupBuilder, Task, Endpoint (+6 more)

### Community 34 - "KeycloakPermissionClient"
Cohesion: 0.13
Nodes (20): CancellationToken, Dictionary, HttpClient, HttpResponseMessage, ILogger, IOptions, IReadOnlyList, List (+12 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.11
Nodes (16): IEntityTypeConfiguration, OutboxMessage, CreatedOn, Event, FailedOn, Id, IsProcessed, NextRetryAt (+8 more)

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
Cohesion: 0.07
Nodes (25): IHostBuilder, LoggerConfiguration, LoggerMinimumLevelConfiguration, ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics (+17 more)

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
Cohesion: 0.16
Nodes (10): CancellationToken, Task, IServiceAccountTokenProvider, HttpClient, IOptions, TimeProvider, ServiceAccountTokenProvider, IOptions (+2 more)

### Community 46 - "PaginationQueryableExtensions"
Cohesion: 0.08
Nodes (27): BinaryExpression, ExpressionVisitor, KeyedRow, MemberExpression, MethodInfo, ParameterExpression, Payload, PaginationCursor (+19 more)

### Community 48 - "microsoft_entityframeworkcore"
Cohesion: 0.05
Nodes (35): Common.Infrastructure.Persistence.Inbox, Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Products.Infrastructure.Persistence.Seeding, Common.Application.Persistence.Inbox, Inventory.Infrastructure.InterModuleRequestHandlers, Inventory.Application.Persistence, Inventory.Application.IntegrationEventHandlers (+27 more)

### Community 50 - "Response"
Cohesion: 0.12
Nodes (16): CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate (+8 more)

### Community 51 - "ProductsDbContext"
Cohesion: 0.15
Nodes (12): DbContextOptions, DbSet, ILogger, ProductTemplate, Store, TimeProvider, ProductsDbContext, Products (+4 more)

### Community 52 - "ReCaptchaService"
Cohesion: 0.21
Nodes (10): FormUrlEncodedContent, ReCaptchaResponse, CancellationToken, Exception, HttpClient, ILogger, IOptions, LoggerMessage (+2 more)

### Community 53 - "Request"
Cohesion: 0.15
Nodes (13): Guid, Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FirstName (+5 more)

### Community 54 - "SendEmailOtpRequestHandler"
Cohesion: 0.15
Nodes (13): EmailOtpDispatchErrors, EmailOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, SendEmailOtpRequest, SendEmailOtpResponse, CancellationToken (+5 more)

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
Cohesion: 0.13
Nodes (12): Products.Endpoints.ProductTemplates.v1.Search, LikePattern, CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint (+4 more)

### Community 61 - "KeycloakTokenClient"
Cohesion: 0.17
Nodes (15): JsonWebTokenHandler, DateTimeOffset, KeycloakTokens, CancellationToken, Dictionary, Error, Exception, HttpClient (+7 more)

### Community 62 - "JobHousekeepingJob"
Cohesion: 0.22
Nodes (9): Deleted, JobHousekeepingJob, CancellationToken, ILogger, IOptions, LoggerMessage, Task, TimeProvider (+1 more)

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
Cohesion: 0.17
Nodes (10): HubConnectionContext, IUserIdProvider, RedisOptions, IConfiguration, IConfigureOptions, IConnectionMultiplexer, IServiceCollection, IUserIdProvider (+2 more)

### Community 68 - "ValueObject"
Cohesion: 0.25
Nodes (4): Common.Domain, IComparable, ValueObject, IEnumerable

### Community 69 - "StockReservation"
Cohesion: 0.05
Nodes (39): Inventory.Domain.StockReservations.DomainEvents.v1, DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationCommitConflictDetectedDomainEvent, DateTimeOffset, DefaultIdType, StockReservationId (+31 more)

### Community 70 - "IntegrationEventHandlerBase"
Cohesion: 0.24
Nodes (11): IntegrationEventHandlerBase, MaxEventAge, CancellationToken, ConsumeContext, DefaultIdType, IFusionCache, ILogger, IOptions (+3 more)

### Community 71 - "FullTextSearchOptions"
Cohesion: 0.08
Nodes (27): Add a new language/culture, Add search to a new entity _(Build checklist)_, Change the vector (weights, fields, or config), Extending and maintaining, File map _(Build)_, Full-Text Search, Gotchas, How it works (+19 more)

### Community 72 - "Response"
Cohesion: 0.22
Nodes (8): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Brand, Color, Model

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
Cohesion: 0.15
Nodes (11): IAggregateRoot, Events, Id, Version, IReadOnlyCollection, IAuditableEntity, CreatedBy, CreatedOn (+3 more)

### Community 80 - ".UpdateCurrentPushToken"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 81 - ".SearchStoresAsync"
Cohesion: 0.15
Nodes (12): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Address (+4 more)

### Community 82 - "Split-Deployment PoC"
Cohesion: 0.18
Nodes (9): Concurrent safety, Cross-process call path, Files added by this PoC, How it works, How to run, Split-Deployment PoC, What this proves, README (Boilerplate Overview) (+1 more)

### Community 83 - "ICaptchaService"
Cohesion: 0.09
Nodes (17): CancellationToken, Task, ICaptchaService, CancellationToken, IFeatureManager, RouteGroupBuilder, Task, Endpoint (+9 more)

### Community 84 - ".Configure"
Cohesion: 0.15
Nodes (14): AuditableEntityConfiguration, EntityTypeBuilder, JobRowConfiguration, EntityTypeBuilder, StronglyTypedIdValueConverter, DefaultIdType, EntityTypeBuilder, NpgsqlTsVector (+6 more)

### Community 85 - ".CreateMyStoreAsync"
Cohesion: 0.16
Nodes (9): Store, CancellationToken, Task, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response (+1 more)

### Community 86 - "Store"
Cohesion: 0.09
Nodes (19): StoreId, V1StoreAddressUpdatedDomainEvent, StoreId, V1StoreDescriptionUpdatedDomainEvent, StoreId, V1StoreNameUpdatedDomainEvent, IReadOnlyCollection, List (+11 more)

### Community 87 - "PushOptions"
Cohesion: 0.12
Nodes (20): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri, PushOptions (+12 more)

### Community 89 - "Setup"
Cohesion: 0.33
Nodes (4): ConfigurationManager, Host.Configurations, Setup, WebApplicationBuilder

### Community 90 - "IInterModuleRequestClient"
Cohesion: 0.14
Nodes (16): IClientFactory, IInterModuleRequestClient, MassTransitInterModuleRequestClient, CancellationToken, IOptions, Task, GetStockLevelRequest, GetStockLevelResponse (+8 more)

### Community 91 - ".SeedProductAsync"
Cohesion: 0.33
Nodes (5): CancellationToken, List, ProductTemplateId, StoreId, Task

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
Cohesion: 0.22
Nodes (9): AuditLogRetentionJobRegistrar, CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task, Setup (+1 more)

### Community 100 - "OutboxDbContext"
Cohesion: 0.12
Nodes (14): DbContext, IOutboxDbContext, OutboxMessages, CancellationToken, DbSet, Task, IConfiguration, IServiceCollection (+6 more)

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

### Community 105 - ".RegisterAsync"
Cohesion: 0.09
Nodes (17): IAM.Endpoints.Users.VersionNeutral.SelfRegisterByEmail, CancellationToken, IFeatureManager, ILogger, RouteGroupBuilder, Task, Endpoint, DateTimeOffset (+9 more)

### Community 106 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.07
Nodes (27): Notifications.Application.Otp, Products.Infrastructure.InterModuleRequestHandlers, IAM.Endpoints.Users, Common.Application.Caching, Common.Application.FeatureManagement, IAM.Endpoints.Otp, Notifications.Application.Persistence, Notifications.Infrastructure.Telemetry (+19 more)

### Community 107 - "InterModuleRequestOptions"
Cohesion: 0.09
Nodes (20): ConsumerDefinition, IConsumerConfigurator, IRegistrationContext, InterModuleRequestOptions, DependencyUnavailableRetryAfterSeconds, HandlerConcurrentMessageLimit, HandlerPrefetchCount, Timeouts (+12 more)

### Community 108 - "StockLevel"
Cohesion: 0.11
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

### Community 112 - "OutboxCleanupJob"
Cohesion: 0.27
Nodes (8): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxCleanupJob

### Community 113 - ".AddPersistence"
Cohesion: 0.15
Nodes (11): IDatabaseSeeder, Priority, CancellationToken, Task, CancellationToken, IServiceScopeFactory, Task, ProductsDatabaseSeeder (+3 more)

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
Cohesion: 0.13
Nodes (16): IHeaderDictionary, IValidator, HmacSignatureVerifier, ReadOnlySpan, CancellationToken, HttpContext, IOptions, JsonSerializerOptions (+8 more)

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
Cohesion: 0.29
Nodes (7): IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, DateTimeOffset

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
Cohesion: 0.23
Nodes (9): CancellationToken, ILogger, LoggerMessage, ProductsDbContext, Task, Seeder, CancellationToken, List (+1 more)

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.48
Nodes (7): Observability (OpenTelemetry), docker-compose.yml (Base Stack), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), Postgres Service (mm.postgres), RabbitMQ Service (mm.rabbitmq), Redis Service (mm.redis)

### Community 131 - ".AddInfrastructure"
Cohesion: 0.29
Nodes (7): HostOptions, JsonOptions, Assembly, IConfiguration, IResxLocalizer, IServiceCollection, IWebHostEnvironment

### Community 132 - "StoreId"
Cohesion: 0.09
Nodes (18): Products.Endpoints.Stores.v1.Create, Products.Endpoints.Stores.v1.Deactivate, StoreId, DefaultIdType, StoreId, RequestBody, Request, Body (+10 more)

### Community 134 - "SendRequestBody"
Cohesion: 0.25
Nodes (8): SendContact, IReadOnlyList, SendRequestBody, HtmlContent, Sender, Subject, TextContent, To

### Community 135 - ".HandleAsync"
Cohesion: 0.18
Nodes (11): GetSeedUserIdsRequest, GetSeedUserIdsResponse, ICollection, CancellationToken, Task, GetSeedUserIdsRequestHandler, CancellationToken, IResult (+3 more)

### Community 136 - "InventoryOptions"
Cohesion: 0.15
Nodes (13): InventoryOptions, MaxOccurrencesPerSeries, SafetyBufferQuantity, StockReservationExpirySweepBatchSize, StockReservationExpirySweepCron, WarehouseGatewayBaseUrl, WarehouseGatewayProvider, WebhookMaxBodyBytes (+5 more)

### Community 137 - ".WriteTooManyRequestsToResponse"
Cohesion: 0.13
Nodes (14): PartitionedRateLimiter, CancellationToken, Func, HttpContext, IConfiguration, IProblemDetailsService, IReadOnlyList, IResxLocalizer (+6 more)

### Community 138 - "RequireFeatureFilter"
Cohesion: 0.14
Nodes (12): IEndpointFilter, IFeatureManagerSnapshot, RequireFeatureFilter, ActivitySource, Counter, EndpointFilterDelegate, EndpointFilterInvocationContext, IResxLocalizer (+4 more)

### Community 139 - "ProductTemplate"
Cohesion: 0.15
Nodes (11): IReadOnlyList, List, ProductTemplate, Brand, Color, IsActive, Model, Products (+3 more)

### Community 140 - "ProcessedMessage"
Cohesion: 0.19
Nodes (10): ProcessedMessage, ConsumerName, MessageId, ProcessedOn, DateTimeOffset, DefaultIdType, UniqueConstraintException, DefaultIdType (+2 more)

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

### Community 146 - ".BindDeviceAsync"
Cohesion: 0.14
Nodes (17): BindDeviceSessionRequest, BindDeviceSessionResponse, Guid, CancellationToken, Exception, Guid, ILogger, LoggerMessage (+9 more)

### Community 147 - ".AddOrUpdate"
Cohesion: 0.20
Nodes (8): Action, Expression, Func, Task, Action, Expression, Func, Task

### Community 148 - ".CreateTokensByEmail"
Cohesion: 0.10
Nodes (20): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, VerifyEmailOtpRequest, VerifyEmailOtpResponse, VerifyEmailOtpResponseExtensions, CancellationToken, Exception, ILogger, LoggerMessage (+12 more)

### Community 149 - "Stores/v1/Update/Request.cs"
Cohesion: 0.20
Nodes (10): Products.Endpoints.Stores.v1.Update, RequestBody, Request, Body, Id, RequestBody, Address, Description (+2 more)

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
Cohesion: 0.22
Nodes (8): elastic_serilog_sinks, serilog_configuration, serilog_enrichers_span, serilog_events, serilog_exceptions, serilog_formatting_compact, serilog_sinks_opentelemetry, serilog_sinks_systemconsole_themes

### Community 155 - "ISmsGateway"
Cohesion: 0.11
Nodes (18): CancellationToken, Task, ISmsGateway, SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, SmsMessage (+10 more)

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

### Community 161 - "IntegrationEventOutbox"
Cohesion: 0.22
Nodes (7): IntegrationEventOutbox, HasPending, IReadOnlyList, List, Lock, Setup, IServiceCollection

### Community 162 - "ThrottledEmailGateway"
Cohesion: 0.20
Nodes (8): CancellationToken, Task, EmailMessage, CancellationToken, IFusionCache, IOptions, Task, ThrottledEmailGateway

### Community 164 - "NotificationsDbContext"
Cohesion: 0.15
Nodes (13): DbSet, INotificationsDbContext, DeviceRegistrations, DbContextOptions, DbSet, ILogger, TimeProvider, NotificationsDbContext (+5 more)

### Community 165 - "ApplicationUserId"
Cohesion: 0.07
Nodes (29): Common.Infrastructure.Auth.Services, Common.Infrastructure.Auth, IBackgroundUserContext, UserId, ApplicationUserId, IsEmpty, Value, DefaultIdType (+21 more)

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

### Community 171 - ".RegisterAsync"
Cohesion: 0.15
Nodes (14): OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, VerifyPhoneOtpResponseExtensions, CancellationToken (+6 more)

### Community 172 - "Request"
Cohesion: 0.33
Nodes (6): Request, Address, Description, Name, OwnerId, RequestValidator

### Community 173 - "ProductTemplateId"
Cohesion: 0.25
Nodes (6): ProductTemplateId, DefaultIdType, ProductTemplateId, CancellationToken, List, Task

### Community 174 - "system_diagnostics"
Cohesion: 0.08
Nodes (17): Notifications.Application.Push, BackgroundJobs.Telemetry, Notifications.Infrastructure.Push.Firebase, firebaseadmin, firebaseadmin_messaging, google_apis_auth_oauth2, hangfire_server, hangfire_storage (+9 more)

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
Cohesion: 0.16
Nodes (11): CancellationToken, Task, IPushGateway, CancellationToken, ILogger, LoggerMessage, Task, DummyPushGateway (+3 more)

### Community 179 - "CachingOptions"
Cohesion: 0.11
Nodes (22): CachingEntryDefaults, Duration, FactoryHardTimeout, FactorySoftTimeout, FailSafeMaxDuration, FailSafeThrottleDuration, CachingOptions, AllowInMemoryOnlyInProduction (+14 more)

### Community 180 - "SmsOptions"
Cohesion: 0.10
Nodes (21): SmsOptions, AppName, AttemptTimeoutSeconds, BaseUrl, MaxPerDay, MaxPerPhoneNumberPerDay, MaxRetryAttempts, MsgHeader (+13 more)

### Community 181 - "RabbitMqOptions"
Cohesion: 0.15
Nodes (13): RabbitMqOptions, DefaultConcurrentMessageLimit, DefaultPrefetchCount, Host, Password, Port, RetryIntervalDeltaMs, RetryLimit (+5 more)

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

### Community 191 - "DeviceRegistryReconcileJobRegistrar"
Cohesion: 0.31
Nodes (7): CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task, DeviceRegistryReconcileJobRegistrar

### Community 192 - "Notifications.Application.Hubs"
Cohesion: 0.19
Nodes (4): Notifications.Infrastructure.Hubs, Notifications.Application.Hubs, microsoft_aspnetcore_signalr, NotificationGroupName

### Community 193 - ".GetMeAsync"
Cohesion: 0.24
Nodes (7): CancellationToken, IReadOnlyList, Task, IKeycloakPermissionClient, CancellationToken, HttpContext, Task

### Community 194 - "EventDispatcher"
Cohesion: 0.31
Nodes (7): EventDispatcher, ActivitySource, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task

### Community 195 - ".ReserveStockAsync"
Cohesion: 0.22
Nodes (7): Inventory.Endpoints.StockReservations.v1.Reserve, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Id

### Community 196 - "TokenCreateRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenCreateRateLimitingPolicy (+1 more)

### Community 197 - "SeedingCompletionTracker"
Cohesion: 0.15
Nodes (8): SeedingCompletionTracker, CancellationToken, Exception, Task, Setup, IOptions, IServiceCollection, TaskCompletionSource

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

### Community 202 - "BaseDbContext"
Cohesion: 0.11
Nodes (14): BaseDbContext, AuditLog, CancellationToken, DateTimeOffset, DbContextOptions, DbSet, ILogger, ModelConfigurationBuilder (+6 more)

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

### Community 212 - "ProductTemplates/v1/Create/Request.cs"
Cohesion: 0.17
Nodes (11): Products.Endpoints.Stores.v1.My.Update, Request, Brand, Color, Model, RequestValidator, Request, Address (+3 more)

### Community 213 - "BackgroundJobsTelemetry"
Cohesion: 0.20
Nodes (8): PerformedContext, BackgroundJobsTelemetry, ActivitySource, ConcurrentDictionary, Counter, Histogram, Meter, ObservableGauge

### Community 214 - "Common.Application.Validation"
Cohesion: 0.06
Nodes (30): IAM.Endpoints.Otp.VersionNeutral.SendForLogin, Common.Application.Validation, IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, DatabaseOptions, ConnectionString, DatabaseOptionsValidator, ProjectionReconciliationOptions, MaxPages (+22 more)

### Community 215 - ".ListSessions"
Cohesion: 0.12
Nodes (15): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, CancellationToken, IReadOnlyCollection, RouteGroupBuilder, Task, Endpoint, DateTimeOffset, Response (+7 more)

### Community 216 - ".From"
Cohesion: 0.36
Nodes (6): AspNetResult, ResxLocalizer, EndpointFilterDelegate, EndpointFilterInvocationContext, IStringLocalizer, ValueTask

### Community 217 - "FeatureFlags"
Cohesion: 0.33
Nodes (5): Checkout, FeatureFlags, IAM, Notifications, Products

### Community 218 - ".SendOtp"
Cohesion: 0.09
Nodes (22): SendPhoneOtpRequest, SendPhoneOtpResponse, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, CancellationToken, IFeatureManager (+14 more)

### Community 219 - "BackgroundJobsService"
Cohesion: 0.19
Nodes (9): IBackgroundJobClientV2, IBackgroundJobs, Action, DateTimeOffset, Expression, Func, Task, TimeSpan (+1 more)

### Community 220 - "HttpWarehouseGateway"
Cohesion: 0.31
Nodes (8): ReleaseResponseBody, CancellationToken, HttpClient, HttpResponseMessage, Task, HttpWarehouseGateway, ReleaseRequestBody, ReleaseResponseBody

### Community 221 - "Keycloak realm as code"
Cohesion: 0.25
Nodes (7): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Secrets, Seed users (dev / test only)

### Community 222 - "GetDeviceSessionsRequest"
Cohesion: 0.39
Nodes (7): DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, IReadOnlyList, CancellationToken, Task, GetDeviceSessionsRequestHandler

### Community 223 - "Request"
Cohesion: 0.20
Nodes (10): StoreId, Request, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name, OwnerId (+2 more)

### Community 224 - ".MapEndpoint"
Cohesion: 0.40
Nodes (3): RouteGroupBuilder, RouteGroupBuilder, Setup

### Community 225 - "IInboxStore"
Cohesion: 0.16
Nodes (11): IInboxCleanupTarget, ModuleName, IInboxStore, CancellationToken, DateTimeOffset, DefaultIdType, Task, Setup (+3 more)

### Community 226 - "NetGsmSmsGateway"
Cohesion: 0.16
Nodes (13): CancellationToken, Exception, HttpClient, ILogger, IOptions, JsonSerializerOptions, LoggerMessage, SendRequestBody (+5 more)

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
Cohesion: 0.21
Nodes (8): IConsumer, IInterModuleRequestHandler, CancellationToken, Task, InterModuleRequestHandler, CancellationToken, ConsumeContext, Task

### Community 231 - ".UseModule"
Cohesion: 0.22
Nodes (7): DashboardContext, IAuthorizationService, IDashboardAsyncAuthorizationFilter, IApplicationBuilder, IOptions, HangfireCustomAuthorizationFilter, Task

### Community 232 - "IDbContext"
Cohesion: 0.25
Nodes (7): DatabaseFacade, EntityEntry, IDbContext, AuditLog, ChangeTracker, Database, DbSet

### Community 233 - ".HasPatternIndex"
Cohesion: 0.36
Nodes (6): IndexBuilder, TrigramIndexExtensions, EntityTypeBuilder, Expression, Func, ModelBuilder

### Community 234 - "IAMModule.cs"
Cohesion: 0.10
Nodes (15): IAM.Endpoints, IAM.Endpoints.Otp.VersionNeutral, IAM.Infrastructure.Auth, IAM.Infrastructure.Captcha, IAM.Endpoints.Tokens.VersionNeutral, IAM.Endpoints.Users.VersionNeutral, hangfire_annotations, hangfire_dashboard (+7 more)

### Community 235 - "AuditLogEntry"
Cohesion: 0.10
Nodes (17): AuditLogEntry, AggregateId, AggregateType, Event, EventType, DefaultIdType, ModelBuilder, AuditLogEntryConfiguration (+9 more)

### Community 236 - "V1StoreCreatedDomainEvent"
Cohesion: 0.15
Nodes (15): DomainEventHandlerBase, IIntegrationEventOutbox, CancellationToken, Task, ProductCreatedIntegrationEventPublishingHandler, V1ProductCreatedDomainEventHandlers, CancellationToken, Task (+7 more)

### Community 237 - "ProblemDetailsExtensions.cs"
Cohesion: 0.40
Nodes (3): ProblemDetails, ProblemDetailsExtensions, ICollection

### Community 238 - ".UseInfrastructure"
Cohesion: 0.13
Nodes (12): IAuthenticationSchemeProvider, IMiddleware, IApplicationBuilder, HttpContext, RequestDelegate, Task, EnrichLogsWithUserInfoMiddleware, HttpContext (+4 more)

### Community 239 - "KeycloakTokenClient.cs"
Cohesion: 0.11
Nodes (11): IAM.Infrastructure.Keycloak.Representations, Notifications.Infrastructure.Email.Brevo, Inventory.Application.Gateway, IAM.Domain.Errors, IAM.Infrastructure.Keycloak, microsoft_identitymodel_jsonwebtokens, OAuthErrors, UserAttributes (+3 more)

### Community 241 - ".AddServices"
Cohesion: 0.25
Nodes (8): RecurringJobOptions, IRecurringBackgroundJobs, IConfiguration, ILogger, IServiceCollection, RecurringBackgroundJobsService, IRecurringJobManagerV2, TimeProvider

### Community 242 - "GetActiveSessionIdsRequest"
Cohesion: 0.44
Nodes (7): GetActiveSessionIdsRequest, GetActiveSessionIdsResponse, UserSessionIds, IReadOnlyList, CancellationToken, Task, GetActiveSessionIdsRequestHandler

### Community 243 - "CustomValidator"
Cohesion: 0.07
Nodes (35): Inventory.Endpoints.StockReservations.v1.Commit, CustomRateLimitingOptionsValidator, FixedWindowValidator, ReverseProxyOptionsValidator, CustomValidator, RequestBody, Request, Body (+27 more)

### Community 244 - "KeyedResilienceProfile"
Cohesion: 0.15
Nodes (13): AbstractValidator, KeyedResilienceProfile, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, RateLimitFailOpen (+5 more)

### Community 245 - ".HandleAsync"
Cohesion: 0.39
Nodes (7): GetUsersInRolePageRequest, GetUsersInRolePageResponse, RoleUserSummary, ICollection, CancellationToken, Task, GetUsersInRolePageRequestHandler

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

### Community 251 - "AuditableEntityResponse"
Cohesion: 0.11
Nodes (16): AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, DateTimeOffset, RouteGroupBuilder (+8 more)

### Community 252 - ".TryWriteAsync"
Cohesion: 0.40
Nodes (4): ProblemDetailsContext, ProblemDetailsServiceExtensions, IProblemDetailsService, Task

### Community 260 - "Infrastructure/Setup.cs"
Cohesion: 0.25
Nodes (6): Common.InterModuleRequests, Common.Infrastructure.Localization, Common.Infrastructure.Caching, microsoft_aspnetcore_authentication, microsoft_aspnetcore_http_json, IAssemblyReference

### Community 261 - ".GetAuditLogAsync"
Cohesion: 0.24
Nodes (8): CreatedOn, Version, DbContextExtensions, CancellationToken, DateTimeOffset, DbSet, JsonSerializerOptions, Task

### Community 262 - ".SendWithPipelineAsync"
Cohesion: 0.25
Nodes (7): HttpClientKeyedExtensions, CancellationToken, HttpClient, HttpRequestMessage, HttpResponseMessage, ResiliencePipeline, Task

### Community 263 - "Response"
Cohesion: 0.25
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegister, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 264 - "InboxStore"
Cohesion: 0.32
Nodes (6): InboxStore, ModuleName, CancellationToken, DateTimeOffset, Task, TimeProvider

### Community 265 - ".AddProductToMyStoreAsync"
Cohesion: 0.18
Nodes (9): Products.Endpoints.Stores.v1.My.AddProduct, CancellationToken, ProductTemplate, RouteGroupBuilder, Store, Task, Endpoint, Response (+1 more)

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
Nodes (38): CancellationToken, Task, DbSet, ProductTemplate, IProductsDbContext, Products, ProductTemplates, Stores (+30 more)

### Community 270 - "BackgroundJobsModule"
Cohesion: 0.25
Nodes (7): BackgroundJobsModule, ActivitySourceNames, MeterNames, Name, StartupPriority, IEndpointRouteBuilder, IEnumerable

### Community 271 - "InventoryTelemetry"
Cohesion: 0.21
Nodes (8): ActivitySource, Counter, Meter, InventoryTelemetry, ActivitySource, Counter, Meter, ProductsTelemetry

### Community 272 - ".AddResilientHttpClient"
Cohesion: 0.22
Nodes (8): HttpStandardResilienceOptions, IHttpClientBuilder, Setup, Action, HttpClient, IOptions, IServiceCollection, IServiceProvider

### Community 273 - "KeycloakPermission"
Cohesion: 0.29
Nodes (3): KeycloakPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder

### Community 274 - ".CommitStockReservationAsync"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 275 - "VersionNeutral/Get/Request.cs"
Cohesion: 0.40
Nodes (4): IAM.Endpoints.Users.VersionNeutral.Get, Request, Id, RequestValidator

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

### Community 283 - "TokenResponseRepresentation"
Cohesion: 0.33
Nodes (6): TokenResponseRepresentation, AccessToken, ExpiresIn, RefreshExpiresIn, RefreshToken, SessionState

### Community 284 - "RequestBody"
Cohesion: 0.33
Nodes (6): DateTimeOffset, DefaultIdType, RequestBody, ProductId, Quantity, ReservationDeadline

### Community 285 - "RequestBodyLimitFilter.cs"
Cohesion: 0.15
Nodes (14): Common.Endpoints.Webhooks, microsoft_aspnetcore_http_features, RequestBodyLimitEndpointExtensions, RequestBodyLimitFilter, Func, HttpContext, RequestBodyLimitMetadata, MaxBodyBytes (+6 more)

### Community 286 - "v1/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Probe.v1, Request, Count, RequestValidator

### Community 287 - "IOtpService"
Cohesion: 0.24
Nodes (8): CancellationToken, Task, TimeSpan, IOtpService, OtpVerificationOutcome, InvalidOtp, Success, TooManyAttempts

### Community 288 - "DummySmsGateway"
Cohesion: 0.38
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, DummySmsGateway

### Community 289 - "My/Create/Request.cs"
Cohesion: 0.50
Nodes (3): Products.Endpoints.Stores.v1.My.Create, Request, RequestValidator

### Community 290 - "HttpContextTargetingContextAccessor.cs"
Cohesion: 0.30
Nodes (3): Common.Infrastructure.FeatureManagement, microsoft_featuremanagement_featurefilters, serilog_context

### Community 291 - "StronglyTypedIdBinder"
Cohesion: 0.40
Nodes (4): IModelBinder, ModelBindingContext, StronglyTypedIdBinder, Task

### Community 294 - "UpdateCurrentPushToken/Request.cs"
Cohesion: 0.50
Nodes (4): Notifications.Infrastructure.Devices.UpdateCurrentPushToken, Request, PushToken, RequestValidator

### Community 295 - "Infrastructure/StringExtensions.cs"
Cohesion: 0.40
Nodes (3): opentelemetry_exporter, OtlpExportProtocol, StringExtensions

### Community 296 - "SecurityHeadersOptions.cs"
Cohesion: 0.50
Nodes (4): SecurityHeadersOptions, Headers, SecurityHeadersOptionsValidator, Dictionary

### Community 297 - "Common.Application.EventBus"
Cohesion: 0.14
Nodes (8): Common.IntegrationEvents, Common.Infrastructure.EventBus, Products.Application.Products.DomainEventHandlers.v1, Products.Application.Stores.DomainEventHandlers.v1, Common.Application.EventBus, IBusFactoryConfigurator, ConcurrencyConfiguratorExtensions, IReceiveEndpointConfigurator

### Community 298 - ".AddCustomSwagger"
Cohesion: 0.20
Nodes (7): IApplicationBuilder, IConfigureOptions, IServiceCollection, IWebHostEnvironment, SwaggerGenOptions, Type, Setup

### Community 299 - ".MapEndpoint"
Cohesion: 0.40
Nodes (3): RouteGroupBuilder, RouteGroupBuilder, Setup

### Community 300 - "ResultTelemetryExtensions"
Cohesion: 0.38
Nodes (4): Activity, ResultTelemetryExtensions, ActivitySource, Task

### Community 301 - "ModulesOptions.cs"
Cohesion: 0.50
Nodes (4): ModulesOptions, EnabledModules, ModulesOptionsValidator, IReadOnlyList

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

### Community 310 - ".InvokeAsync"
Cohesion: 0.40
Nodes (4): EndpointFilterDelegate, EndpointFilterInvocationContext, IHttpMaxRequestBodySizeFeature, ValueTask

### Community 311 - "microsoft_aspnetcore_mvc"
Cohesion: 0.05
Nodes (39): Products.Endpoints.Products.v1.My.Update, Products.Endpoints.ProductTemplates.v1.Deactivate, Products.Endpoints.Stores.v1.My.RemoveProduct, Inventory.Endpoints.StockReservations.v1.Release, Common.Application.ModelBinders, Products.Endpoints.ProductTemplates.v1.Activate, Products.Endpoints.Stores.v1.RemoveProduct, microsoft_aspnetcore_mvc (+31 more)

### Community 313 - ".MapEndpoint"
Cohesion: 0.40
Nodes (3): RouteGroupBuilder, Setup, RouteGroupBuilder

### Community 314 - "Versioning/Setup.cs"
Cohesion: 0.50
Nodes (3): asp_versioning, asp_versioning_builder, asp_versioning_conventions

### Community 319 - "Request"
Cohesion: 0.12
Nodes (14): CancellationToken, RouteGroupBuilder, Task, Endpoint, RequestBody, Request, Body, Id (+6 more)

### Community 324 - ".UpdateMyProductAsync"
Cohesion: 0.12
Nodes (14): CancellationToken, RouteGroupBuilder, Task, Endpoint, RequestBody, Request, Body, Id (+6 more)

## Knowledge Gaps
- **954 isolated node(s):** `UserId`, `Id`, `IdAsString`, `Roles`, `SessionId` (+949 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 2234 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **98 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `Infrastructure/Setup.cs`, `InventoryOptions`, `AuditLogOptions`, `DevicesOptions`, `Common.Domain.ResultMonad`, `Setup`, `EmailOptions`, `ResxLocalizationOptions`, `OutboxOptions`, `Common.Infrastructure.Extensions`, `JobHousekeepingOptions`, `Setup.Logger.cs`, `fluentvalidation`, `OtpOptions`, `ObservabilityOptions`, `SecurityHeadersOptions.cs`, `ConfigureSwaggerOptions.cs`, `KeycloakOptions`, `ModulesOptions.cs`, `system_diagnostics`, `microsoft_entityframeworkcore`, `CorsOptions`, `CachingOptions`, `SignalROptions`, `RabbitMqOptions`, `SmsOptions`, `IdentityScheme`, `RequestLoggingOptions`, `FullTextSearchOptions`, `BackgroundJobsOptions`, `Common.Application.Validation`, `PushOptions`, `ResiliencyOptions`, `CaptchaOptions`, `IAMModule.cs`, `InterModuleRequestOptions`, `Common.InterModuleRequests.Contracts`, `KeycloakTokenClient.cs`, `CustomValidator`, `OpenApiOptions`, `.AddCustomHealthChecks`?**
  _High betweenness centrality (0.162) - this node is a cross-community bridge._
- **Why does `Result` connect `Result` to `.ReserveSeriesAsync`, `.GetAuditLogAsync`, `FirebasePushGateway`, `.AddProductToMyStoreAsync`, `Error`, `ProductTemplate`, `IKeycloakAdminClient`, `IProductsDbContext`, `Response`, `DomainEvent`, `IInterModuleRequest`, `.BindDeviceAsync`, `.CreateTokensByEmail`, `.CommitStockReservationAsync`, `.TapWhenFeatureEnabledAsync`, `IEmailGateway`, `ICurrentUser`, `Product`, `ISmsGateway`, `.SingleAsResult`, `IKeycloakTokenClient`, `ThrottledEmailGateway`, `DummySmsGateway`, `ApplicationUserId`, `.RegisterAsync`, `ResultTelemetryExtensions`, `PaginationQueryableExtensions`, `PaginationResponse`, `Response`, `.AddPushServices`, `ReCaptchaService`, `SendEmailOtpRequestHandler`, `BrevoEmailGateway`, `.SearchProductTemplatesAsync`, `Request`, `.SendAsync`, `.GetMeAsync`, `.ReserveStockAsync`, `.UpdateMyProductAsync`, `StockReservation`, `Response`, `Response`, `.UpdateCurrentPushToken`, `.SearchStoresAsync`, `ICaptchaService`, `.CreateMyStoreAsync`, `.ListSessions`, `.SendOtp`, `IInterModuleRequestClient`, `HttpWarehouseGateway`, `.SearchMyProductsAsync`, `NetGsmSmsGateway`, `.SearchStoreProductsAsync`, `.RegisterAsync`, `.IsRegisteredAsync`, `.ReleaseStockReservationAsync`, `Response`, `.HandleWarehouseWebhookAsync`, `Response`, `Response`?**
  _High betweenness centrality (0.132) - this node is a cross-community bridge._
- **Why does `ApplicationUserId` connect `ApplicationUserId` to `NotificationPayload`, `Seeder`, `KeycloakAdminClient`, `.HandleAsync`, `IKeycloakAdminClient`, `IStronglyTypedId`, `KeycloakPermissionAuthorizationHandler`, `Response`, `.BindDeviceAsync`, `VersionNeutral/Get/Request.cs`, `IntegrationEvent`, `DeviceRegistration`, `ICurrentUser`, `AuditableEntity`, `For`, `SendSecurityAlertRequestHandler`, `.RegisterAsync`, `Request`, `PaginationResponse`, `Response`, `KeycloakTokenClient`, `Notifications.Application.Hubs`, `Response`, `IAggregateRoot`, `.SearchStoresAsync`, `.Configure`, `.CreateMyStoreAsync`, `Store`, `GetDeviceSessionsRequest`, `Request`, `.RegisterAsync`, `StockLevel`, `V1StoreCreatedDomainEvent`, `GetActiveSessionIdsRequest`, `Response`, `.HandleAsync`, `AuditableEntityResponse`, `Response`?**
  _High betweenness centrality (0.065) - this node is a cross-community bridge._
- **What connects `UserId`, `Id`, `IdAsString` to the rest of the system?**
  _954 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `.ReserveSeriesAsync` be split into smaller, more focused modules?**
  _Cohesion score 0.10869565217391304 - nodes in this community are weakly interconnected._
- **Should `Common.Domain.StronglyTypedIds` be split into smaller, more focused modules?**
  _Cohesion score 0.05961538461538462 - nodes in this community are weakly interconnected._
- **Should `RedisFixedWindowRateLimiter` be split into smaller, more focused modules?**
  _Cohesion score 0.13333333333333333 - nodes in this community are weakly interconnected._