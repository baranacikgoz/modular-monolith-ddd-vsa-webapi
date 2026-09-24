# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-09-24)

## Corpus Check
- 570 files · ~88,647 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 4890 nodes · 9636 edges · 386 communities (287 shown, 99 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 255 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `dea0f9a9`
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
- .RevokeToken
- Common.Infrastructure.Extensions
- IEvent
- RequestResponseBodyLoggingMiddleware
- AuditLogEntryConfiguration
- Result
- JobRow
- NotificationsModule
- .SingleAsResult
- .RaiseEvent
- KeycloakPermissionClient
- OutboxMessage
- CustomRateLimitingOptions
- SendPhoneOtpRequestHandler
- .RefreshToken
- ObservabilityOptions
- Host.Swagger
- Request
- .SaveWithOutboxAsync
- CreateStoreRateLimitingPolicy
- KeycloakOptions
- IInterModuleRequest
- PaginationQueryableExtensions
- Outbox Misuse Check
- V1StoreCreatedDomainEvent
- Add Integration Event Command
- Response
- Common.Domain.StronglyTypedIds
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
- PolymorphicEventConverter
- FullTextSearchOptions
- Response
- .WriteAsync
- AsNoTracking Coverage Check
- IntegrationEventOutbox
- Response
- IProductsDbContext
- CheckRegistrationRateLimitingPolicy
- Common.Domain.Events
- IAuditableEntity
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
- Seeder
- system_globalization
- .MapEndpoint
- KeyedResiliencePipelines
- .SearchMyProductsAsync
- DatabaseSeederOrchestrator
- StockReservationExpirySweepService
- AuditableEntityResponse
- AuditLogRetentionJobRegistrar
- OutboxDbContext
- ResiliencyOptions
- HttpContextTargetingContextAccessor
- InboxCleanupJob
- ProductId
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
- AuditLogRetentionService
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
- .GetMeAsync
- .WriteTooManyRequestsToResponse
- ResultToResponseTransformer.cs
- ProductTemplate
- .MapEndpoint
- Host.Infrastructure
- TokenRefreshRateLimitingPolicy
- Setup
- Response
- StoreId
- .RegisterAsync
- .AddOrUpdate
- GetActiveSessionIdsRequest
- ProductTemplateId
- StockReservationExpirySweepJobRegistrar
- OutboxOptions
- .EnsureNoMigrationsPending
- SwaggerDefaultValues
- .CreateTokensByEmail
- ISmsGateway
- IInboxStore
- IntegrationEvent
- RedisOtpService
- IInventoryDbContext
- OtpServiceBase
- .ReserveSeriesAsync
- ThrottledEmailGateway
- For
- .AddKeycloakInfrastructure
- CurrentUser
- IamModule
- Common.Application.Pagination
- Response
- SendSecurityAlertRequestHandler
- Notifications.Application/IAssemblyReference.cs
- IBackgroundJobs
- ResxLocalizationOptions
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
- .AddProductToMyStoreAsync
- KeycloakPermission
- Stores/v1/My/Update/Request.cs
- CorsOptions
- InventoryOptions
- TokenCreateRateLimitingPolicy
- .ReserveStockAsync
- InventoryModule
- OpenApiOptions
- NotificationsTelemetry
- KeycloakScopes
- Request
- SmsRateLimitingPolicy
- Configuration-Driven Module Loading
- AuditableEntity
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- ConfigureSwaggerOptions
- EventDispatcher
- Setup
- ICaptchaService
- fluentvalidation
- .CreateMyStoreAsync
- OtpOptions
- FeatureFlags
- Response
- .MapEndpoint
- InboxStore
- Keycloak realm as code
- Key decisions
- microsoft_aspnetcore_mvc
- .MapEndpoint
- ProcessedMessage
- NetGsmSmsGateway
- RegisterRateLimitingPolicy
- .UpsertIfNewerAsync
- .AddCommonOptions
- InterModuleRequestHandler
- .UseModule
- DeviceRegistryReconcileJobRegistrar
- .HasPatternIndex
- Setup
- StatelessInboxStore
- IOtpService
- ProblemDetailsExtensions.cs
- EnrichLogsWithUserInfoMiddleware
- InventoryModule.cs
- InterModuleRequestOptions
- Request
- .AddCustomSwagger
- CustomValidator
- KeyedResilienceProfile
- DeviceRegistryReconciliationService
- BackgroundJobsTelemetry
- .Capture
- SendErrorBody
- .AddProductAsync
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
- JobStatus
- .SendWithPipelineAsync
- Response
- IDbContext
- BackgroundJobsModule
- StrictDateTimeOffsetJsonConverter
- AuditLogOptions
- DevicesOptions
- IOutboxMessage
- DefaultResponsesOperationFilter
- PublishOutcome
- .AddResilientHttpClient
- .AddNetGsm
- GetProductRequest
- .GetAuditLogAsync
- .CommitStockReservationAsync
- SendResponseBody
- AuditLogEntry
- Response
- V1StockReservationCommitConflictDetectedDomainEvent
- .TryReadFromJsonAsync
- Setup.Logger.cs
- ResiliencyOptions.cs
- RequestBody
- Setup
- Common.Application.Options
- Setup
- DummySmsGateway
- .SetRetryAfterHeader
- ICurrentUser
- .SetConcurrency
- .AddServices
- .MapEndpoints
- .SeedProductAsync
- RemoveDefaultResponseSchemaFilter
- StringExtensions
- SignalROptions
- ProductTemplates/v1/Create/Request.cs
- InventoryTelemetry
- ProductsTelemetry
- IAM.Endpoints
- Products.Endpoints
- Notifications.Infrastructure
- .AddServices
- StronglyTypedIdListReadOnlyJsonConverter
- StronglyTypedIdReadOnlyJsonConverter
- IssueVerificationTokenRequestHandler
- Setup
- .UpdateProductAsync
- .RemoveProductAsync
- StronglyTypedIdBinder
- Infrastructure/StringExtensions.cs
- ProjectionReconciliationOptions.cs
- SecurityHeadersOptions.cs
- .AddModuleDbContext
- system_runtime_compilerservices
- DeviceRegistrationId
- .AddDeviceRegistryReconciliation
- RequestBody
- HttpContextTargetingContextAccessor.cs
- ValidationContextExtensions
- HmacSignatureVerifier
- .PhoneNumberValidation
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

## Communities (386 total, 99 thin omitted)

### Community 0 - "IStronglyTypedId"
Cohesion: 0.23
Nodes (8): StronglyTypedIdWriteOnlyJsonConverter, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, IStronglyTypedId, Value, DefaultIdType

### Community 1 - "NotificationPayload"
Cohesion: 0.23
Nodes (12): IHubContext, CancellationToken, IReadOnlyList, Task, INotificationDispatcher, Task, INotificationsClient, NotificationPayload (+4 more)

### Community 2 - "OutboxProcessor"
Cohesion: 0.23
Nodes (10): IPublishEndpoint, CancellationToken, Exception, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task (+2 more)

### Community 3 - "microsoft_entityframeworkcore"
Cohesion: 0.05
Nodes (44): Common.Infrastructure.Persistence.Inbox, Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Common.Application.Persistence.Inbox, Inventory.Application.IntegrationEventHandlers, Common.IntegrationEvents, Common.Application.Persistence, Outbox (+36 more)

### Community 5 - "KeycloakAdminClient"
Cohesion: 0.17
Nodes (16): CancellationToken, Error, Func, HttpClient, HttpRequestMessage, HttpResponseMessage, IFusionCache, ILogger (+8 more)

### Community 6 - "RedisFixedWindowRateLimiter"
Cohesion: 0.13
Nodes (16): RateLimiter, RateLimiterStatistics, FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration, CancellationToken (+8 more)

### Community 7 - "ApplySearchLanguageInterceptor"
Cohesion: 0.18
Nodes (10): SaveChangesInterceptor, ApplyAuditingInterceptor, TimeProvider, ApplySearchLanguageInterceptor, CancellationToken, DbContextEventData, InterceptionResult, ValueTask (+2 more)

### Community 8 - "FirebasePushGateway"
Cohesion: 0.17
Nodes (12): FirebaseApp, FirebaseMessaging, IDisposable, CancellationToken, Exception, IEnumerable, ILogger, IReadOnlyList (+4 more)

### Community 9 - ".RequireOtpTemplateForDefaultCulture"
Cohesion: 0.40
Nodes (4): Func, IConfiguration, IEnumerable, OtpTemplateConfiguration

### Community 10 - "Error"
Cohesion: 0.07
Nodes (23): Inventory.Domain.StockReservations.Errors, StringLocalizerExtensions, IStringLocalizer, Error, Key, ParameterName, StatusCode, SubErrors (+15 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.10
Nodes (26): IAM.Endpoints.Captcha.VersionNeutral, Common.Application.Search, Inventory.Infrastructure.InterModuleRequestHandlers, Products.Endpoints.Stores, Inventory.Application.Persistence, Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions (+18 more)

### Community 12 - "ApplicationUserId"
Cohesion: 0.10
Nodes (24): ApplicationUserId, IsEmpty, Value, DefaultIdType, CancellationToken, IReadOnlyList, Task, IKeycloakAdminClient (+16 more)

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
Cohesion: 0.06
Nodes (30): ProductId, V1ProductNameUpdatedDomainEvent, ProductId, V1ProductPriceDecreasedDomainEvent, ProductTemplate, ProductTemplateId, Store, StoreId (+22 more)

### Community 18 - "StockReservation"
Cohesion: 0.09
Nodes (22): DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationExpiredDomainEvent, DateTimeOffset, StockReservationId, V1StockReservationReleaseAttemptAbandonedDomainEvent, DateTimeOffset (+14 more)

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
Cohesion: 0.16
Nodes (12): DateTimeOffset, Guid, DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive, LastReconciledOn (+4 more)

### Community 23 - "IEmailGateway"
Cohesion: 0.18
Nodes (11): IEmailGateway, CancellationToken, ILogger, LoggerMessage, Task, DummyEmailGateway, IConfiguration, IFusionCache (+3 more)

### Community 24 - ".RevokeToken"
Cohesion: 0.09
Nodes (18): DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse, IReadOnlyList, CancellationToken, RouteGroupBuilder, Task, Endpoint, CancellationToken (+10 more)

### Community 25 - "Common.Infrastructure.Extensions"
Cohesion: 0.13
Nodes (17): Common.Infrastructure.Modules, Products.Endpoints.Probe, Common.Infrastructure.RateLimiting, Products.Infrastructure.RateLimiting, Products.Endpoints.Products, Common.Infrastructure.Extensions, IAM.Endpoints.Otp.VersionNeutral, IAM.Infrastructure.RateLimiting (+9 more)

### Community 26 - "IEvent"
Cohesion: 0.11
Nodes (13): CancellationToken, Task, IEventHandler, CancellationToken, Task, IEventHandlerWrapper, CancellationToken, Task (+5 more)

### Community 27 - "RequestResponseBodyLoggingMiddleware"
Cohesion: 0.22
Nodes (7): IDiagnosticContext, HttpContext, IList, IOptions, PathString, RequestDelegate, RequestResponseBodyLoggingMiddleware

### Community 28 - "AuditLogEntryConfiguration"
Cohesion: 0.10
Nodes (15): IEntityTypeConfiguration, ModelBuilder, AuditLogEntryConfiguration, EntityTypeBuilder, DomainEventConverter, JsonSerializerOptions, IntegrationEventConverter, JsonSerializerOptions (+7 more)

### Community 29 - "Result"
Cohesion: 0.12
Nodes (15): Result, Error, IsFailure, Success, Value, Func, Task, AsyncExtensions (+7 more)

### Community 30 - "JobRow"
Cohesion: 0.19
Nodes (9): JobRow, FailureKey, FinishedOn, Progress, QueuedOn, RequestedBy, StartedOn, Status (+1 more)

### Community 31 - "NotificationsModule"
Cohesion: 0.15
Nodes (10): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, NotificationsModule, ActivitySourceNames, MeterNames (+2 more)

### Community 32 - ".SingleAsResult"
Cohesion: 0.18
Nodes (10): CollectionExtensions, Func, ICollection, IEnumerable, PersistenceQueryableExtensions, CancellationToken, Expression, Func (+2 more)

### Community 33 - ".RaiseEvent"
Cohesion: 0.15
Nodes (11): AggregateRoot, Events, Id, Version, IReadOnlyCollection, List, IAggregateRoot, Events (+3 more)

### Community 34 - "KeycloakPermissionClient"
Cohesion: 0.13
Nodes (20): CancellationToken, Dictionary, HttpClient, HttpResponseMessage, ILogger, IOptions, IReadOnlyList, List (+12 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.14
Nodes (14): PublishOutcome, OutboxMessage, CreatedOn, Event, FailedOn, Id, IsProcessed, NextRetryAt (+6 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.08
Nodes (22): CheckRegistrationRateLimitingPolicy, EmailRateLimitingPolicy, OtpVerifyRateLimitingPolicy, RegisterRateLimitingPolicy, SmsRateLimitingPolicy, CustomRateLimitingOptions, CheckRegistration, CreateStore (+14 more)

### Community 37 - "SendPhoneOtpRequestHandler"
Cohesion: 0.16
Nodes (13): OtpDispatchErrors, SendPhoneOtpRequest, SendPhoneOtpResponse, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, CancellationToken (+5 more)

### Community 38 - ".RefreshToken"
Cohesion: 0.15
Nodes (11): IAM.Endpoints.Tokens.VersionNeutral.Refresh, CancellationToken, RouteGroupBuilder, Task, Endpoint, DateTimeOffset, Response, AccessToken (+3 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.07
Nodes (25): IHostBuilder, LoggerConfiguration, LoggerMinimumLevelConfiguration, ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics (+17 more)

### Community 40 - "Host.Swagger"
Cohesion: 0.36
Nodes (5): Host.Swagger, microsoft_aspnetcore_mvc_apiexplorer, microsoft_openapi, swashbuckle_aspnetcore_swaggergen, system_text_json_nodes

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

### Community 45 - "IInterModuleRequest"
Cohesion: 0.18
Nodes (11): IInterModuleRequest, OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, VerifyPhoneOtpResponseExtensions (+3 more)

### Community 46 - "PaginationQueryableExtensions"
Cohesion: 0.07
Nodes (32): BinaryExpression, How it works, One-time setup _(Build, in a migration)_, Query path, Ranking, Write path, ExpressionVisitor, KeyedRow (+24 more)

### Community 48 - "V1StoreCreatedDomainEvent"
Cohesion: 0.23
Nodes (9): Products.Application.Stores.DomainEventHandlers.v1, DomainEventHandlerBase, CancellationToken, Task, SimulateSomeBusinessHandler, StoreCreatedIntegrationEventPublishingHandler, V1StoreCreatedDomainEventHandlers, StoreId (+1 more)

### Community 50 - "Response"
Cohesion: 0.12
Nodes (16): CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate (+8 more)

### Community 51 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.08
Nodes (18): Products.Domain.Products.DomainEvents.v1, Common.Domain.StronglyTypedIds, Products.Endpoints.Products.v1.My.Get, Products.Endpoints.Products.v1.Search, Products.Endpoints.Stores.v1.My.Create, Products.Domain.Products, Common.Application.DTOs, Products.Endpoints.Products.v1.My.Search (+10 more)

### Community 52 - "ReCaptchaService"
Cohesion: 0.06
Nodes (34): DateTime, FormUrlEncodedContent, ReCaptchaResponse, CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey (+26 more)

### Community 53 - "Request"
Cohesion: 0.15
Nodes (13): Guid, Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FirstName (+5 more)

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
Nodes (11): Products.Endpoints.ProductTemplates.v1.Search, CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response (+3 more)

### Community 61 - "KeycloakTokenClient"
Cohesion: 0.14
Nodes (19): JsonWebTokenHandler, CancellationToken, Dictionary, Error, Exception, HttpClient, ILogger, IOptions (+11 more)

### Community 62 - "JobHousekeepingJob"
Cohesion: 0.22
Nodes (9): Deleted, JobHousekeepingJob, CancellationToken, ILogger, IOptions, LoggerMessage, Task, TimeProvider (+1 more)

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

### Community 69 - ".HandleAsync"
Cohesion: 0.18
Nodes (11): GetSeedUserIdsRequest, GetSeedUserIdsResponse, ICollection, CancellationToken, Task, GetSeedUserIdsRequestHandler, CancellationToken, IResult (+3 more)

### Community 70 - "PolymorphicEventConverter"
Cohesion: 0.25
Nodes (6): JsonConverter, PolymorphicEventConverter, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

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
Cohesion: 0.11
Nodes (15): Products.Application.Products.DomainEventHandlers.v1, IIntegrationEventOutbox, IntegrationEventOutbox, HasPending, IReadOnlyList, List, Lock, Setup (+7 more)

### Community 76 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 77 - "IProductsDbContext"
Cohesion: 0.04
Nodes (38): CancellationToken, Task, DbSet, ProductTemplate, IProductsDbContext, Products, ProductTemplates, Stores (+30 more)

### Community 78 - "CheckRegistrationRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, CheckRegistrationRateLimitingPolicy (+1 more)

### Community 79 - "Common.Domain.Events"
Cohesion: 0.06
Nodes (29): Inventory.Domain.StockReservations.DomainEvents.v1, Common.Domain.Events, DomainEvent, CreatedOn, Id, Version, DateTimeOffset, DefaultIdType (+21 more)

### Community 80 - "IAuditableEntity"
Cohesion: 0.17
Nodes (10): IAuditableEntity, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, DateTimeOffset, CancellationToken, DbContextEventData (+2 more)

### Community 81 - ".SearchStoresAsync"
Cohesion: 0.15
Nodes (12): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Address (+4 more)

### Community 82 - "Split-Deployment PoC"
Cohesion: 0.18
Nodes (9): Concurrent safety, Cross-process call path, Files added by this PoC, How it works, How to run, Split-Deployment PoC, What this proves, README (Boilerplate Overview) (+1 more)

### Community 83 - "NotificationsDbContext"
Cohesion: 0.18
Nodes (10): DbContextOptions, DbSet, ILogger, TimeProvider, NotificationsDbContext, DeviceRegistrations, IApplicationBuilder, ILoggerFactory (+2 more)

### Community 84 - ".Configure"
Cohesion: 0.13
Nodes (16): AuditableEntityConfiguration, EntityTypeBuilder, JobRowConfiguration, EntityTypeBuilder, StronglyTypedIdValueConverter, DefaultIdType, EntityTypeBuilder, DeviceRegistrationConfiguration (+8 more)

### Community 85 - "ResultTelemetryExtensions"
Cohesion: 0.38
Nodes (4): Activity, ResultTelemetryExtensions, ActivitySource, Task

### Community 86 - "Store"
Cohesion: 0.08
Nodes (19): StoreId, V1StoreAddressUpdatedDomainEvent, StoreId, V1StoreDescriptionUpdatedDomainEvent, StoreId, V1StoreNameUpdatedDomainEvent, IReadOnlyCollection, List (+11 more)

### Community 87 - "PushOptions"
Cohesion: 0.12
Nodes (20): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri, PushOptions (+12 more)

### Community 89 - "Program.cs"
Cohesion: 0.22
Nodes (6): ConfigurationManager, Host, Host.Configurations, Setup, Program, WebApplicationBuilder

### Community 90 - ".GetProductAsync"
Cohesion: 0.16
Nodes (13): GetStockLevelRequest, GetStockLevelResponse, DefaultIdType, CancellationToken, Task, GetStockLevelRequestHandler, RouteGroupBuilder, Setup (+5 more)

### Community 91 - "Seeder"
Cohesion: 0.21
Nodes (9): Products.Infrastructure.Persistence.Seeding, Common.InterModuleRequests.IAM, ILogger, LoggerMessage, ProductsDbContext, Seeder, CancellationToken, List (+1 more)

### Community 92 - "system_globalization"
Cohesion: 0.07
Nodes (26): Common.InterModuleRequests, Products.Endpoints.Stores.v1.My.AddProduct, Common.Infrastructure.Localization, Inventory.Endpoints.StockReservations.v1.WebhookCallback, Common.Application.JsonConverters, Products.Endpoints.Stores.v1.AddProduct, Common.Infrastructure.Caching, microsoft_aspnetcore_authentication (+18 more)

### Community 93 - ".MapEndpoint"
Cohesion: 0.22
Nodes (6): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, RouteGroupBuilder, Response, ClientKey, RouteGroupBuilder, Setup

### Community 94 - "KeyedResiliencePipelines"
Cohesion: 0.16
Nodes (11): HttpRequestException, ResiliencePipelineBuilder, ResiliencePipelineRegistry, KeyedResiliencePipelines, HttpResponseMessage, IOptions, List, Lock (+3 more)

### Community 95 - ".SearchMyProductsAsync"
Cohesion: 0.14
Nodes (12): LikePattern, CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response (+4 more)

### Community 96 - "DatabaseSeederOrchestrator"
Cohesion: 0.12
Nodes (17): BackgroundService, DatabaseSeederOrchestrator, CancellationToken, Exception, IHostApplicationLifetime, ILogger, IServiceScopeFactory, LoggerMessage (+9 more)

### Community 97 - "StockReservationExpirySweepService"
Cohesion: 0.18
Nodes (11): IServiceCollection, Setup, CancellationToken, Exception, Guid, ILogger, IOptions, LoggerMessage (+3 more)

### Community 98 - "AuditableEntityResponse"
Cohesion: 0.11
Nodes (18): AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, DateTimeOffset, CancellationToken (+10 more)

### Community 99 - "AuditLogRetentionJobRegistrar"
Cohesion: 0.22
Nodes (9): IHostedService, AuditLogRetentionJobRegistrar, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task, Setup (+1 more)

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

### Community 104 - "ProductId"
Cohesion: 0.08
Nodes (25): Products.Endpoints.Stores.v1.My.RemoveProduct, Products.Endpoints.Products.v1.Update, Products.Endpoints.Products.v1.Get, DefaultIdType, ProductId, Request, Id, RequestValidator (+17 more)

### Community 105 - "Response"
Cohesion: 0.18
Nodes (9): IAM.Endpoints.Users.VersionNeutral.SelfRegisterByEmail, RouteGroupBuilder, Endpoint, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken (+1 more)

### Community 106 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.06
Nodes (27): Products.Infrastructure.InterModuleRequestHandlers, IAM.Endpoints.Users, Common.Application.FeatureManagement, IAM.Endpoints.Otp, Notifications.Application.Persistence, IAM.Domain.Captcha, Common.InterModuleRequests.Contracts, IAM.Infrastructure.Captcha.Services (+19 more)

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

### Community 111 - ".ReleaseStockReservationAsync"
Cohesion: 0.13
Nodes (14): CancellationToken, Task, IWarehouseGateway, CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint (+6 more)

### Community 112 - "OutboxCleanupJob"
Cohesion: 0.27
Nodes (8): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxCleanupJob

### Community 113 - "ProductsDbContext"
Cohesion: 0.08
Nodes (23): IDatabaseSeeder, Priority, CancellationToken, Task, DbContextOptions, DbSet, ILogger, ProductTemplate (+15 more)

### Community 114 - "Response"
Cohesion: 0.11
Nodes (17): IAM.Endpoints.Users.VersionNeutral.Me.Get, RouteGroupBuilder, Endpoint, DateOnly, DateTimeOffset, IReadOnlyCollection, Response, BirthDate (+9 more)

### Community 115 - "AuditLogRetentionService"
Cohesion: 0.33
Nodes (7): AuditLogRetentionService, CancellationToken, ILogger, IOptions, LoggerMessage, NpgsqlDataSource, Task

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
Cohesion: 0.15
Nodes (14): IHeaderDictionary, IValidator, CancellationToken, HttpContext, IOptions, JsonSerializerOptions, RouteGroupBuilder, Task (+6 more)

### Community 121 - ".GetVariantAsync"
Cohesion: 0.40
Nodes (4): IVariantFeatureManager, IVariantFeatureManagerExtensions, CancellationToken, Task

### Community 122 - "OtpVerifyRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, OtpVerifyRateLimitingPolicy (+1 more)

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
Nodes (12): IApplicationBuilder, IServiceCollection, Exception, HttpContext, ILogger, IOptions, IProblemDetailsService, IResxLocalizer (+4 more)

### Community 127 - "BaseDbContext"
Cohesion: 0.11
Nodes (14): BaseDbContext, AuditLog, CancellationToken, DateTimeOffset, DbContextOptions, DbSet, ILogger, ModelConfigurationBuilder (+6 more)

### Community 128 - "EmailRateLimitingPolicy"
Cohesion: 0.18
Nodes (10): IRateLimiterPolicy, CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask (+2 more)

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

### Community 135 - "Response"
Cohesion: 0.18
Nodes (10): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, DateTimeOffset, Response, ClientId, DeviceName, Id, IpAddress, IsCurrent (+2 more)

### Community 136 - ".GetMeAsync"
Cohesion: 0.19
Nodes (9): CancellationToken, IReadOnlyList, Task, IKeycloakPermissionClient, IReadOnlyList, GrantedPermission, CancellationToken, HttpContext (+1 more)

### Community 137 - ".WriteTooManyRequestsToResponse"
Cohesion: 0.13
Nodes (14): PartitionedRateLimiter, CancellationToken, Func, HttpContext, IConfiguration, IProblemDetailsService, IReadOnlyList, IResxLocalizer (+6 more)

### Community 138 - "ResultToResponseTransformer.cs"
Cohesion: 0.06
Nodes (40): AspNetResult, Common.Endpoints.Webhooks, Common.Application.EndpointFilters, IEndpointFilter, IFeatureManagerSnapshot, IHttpMaxRequestBodySizeFeature, microsoft_aspnetcore_hosting, microsoft_aspnetcore_http_features (+32 more)

### Community 139 - "ProductTemplate"
Cohesion: 0.15
Nodes (11): IReadOnlyList, List, ProductTemplate, Brand, Color, IsActive, Model, Products (+3 more)

### Community 140 - ".MapEndpoint"
Cohesion: 0.40
Nodes (3): RouteGroupBuilder, RouteGroupBuilder, Setup

### Community 141 - "Host.Infrastructure"
Cohesion: 0.18
Nodes (8): Host.Infrastructure, healthchecks_ui_client, microsoft_aspnetcore_diagnostics_healthchecks_healthcheckoptions, microsoft_extensions_diagnostics_healthchecks, opentelemetry_metrics, opentelemetry_opentelemetrybuilder, opentelemetry_resources, opentelemetry_trace

### Community 142 - "TokenRefreshRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenRefreshRateLimitingPolicy (+1 more)

### Community 143 - "Setup"
Cohesion: 0.10
Nodes (18): LoadAll, microsoft_aspnetcore_httpoverrides, ModuleRegistry, Names, Setup, Assembly, Exception, IApplicationBuilder (+10 more)

### Community 144 - "Response"
Cohesion: 0.12
Nodes (16): CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate (+8 more)

### Community 145 - "StoreId"
Cohesion: 0.12
Nodes (14): Products.Endpoints.Stores.v1.Create, Products.Endpoints.Stores.v1.Deactivate, Products.Endpoints.Stores.v1.Get, StoreId, DefaultIdType, StoreId, Response, Id (+6 more)

### Community 146 - ".RegisterAsync"
Cohesion: 0.08
Nodes (33): IInterModuleRequestClient, CancellationToken, Task, BindDeviceSessionRequest, BindDeviceSessionResponse, Guid, CancellationToken, Task (+25 more)

### Community 147 - ".AddOrUpdate"
Cohesion: 0.20
Nodes (8): Action, Expression, Func, Task, Action, Expression, Func, Task

### Community 148 - "GetActiveSessionIdsRequest"
Cohesion: 0.44
Nodes (7): GetActiveSessionIdsRequest, GetActiveSessionIdsResponse, UserSessionIds, IReadOnlyList, CancellationToken, Task, GetActiveSessionIdsRequestHandler

### Community 149 - "ProductTemplateId"
Cohesion: 0.25
Nodes (6): ProductTemplateId, DefaultIdType, ProductTemplateId, CancellationToken, List, Task

### Community 150 - "StockReservationExpirySweepJobRegistrar"
Cohesion: 0.31
Nodes (7): CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task, StockReservationExpirySweepJobRegistrar

### Community 151 - "OutboxOptions"
Cohesion: 0.09
Nodes (22): OutboxCleanupSettings, BatchSize, CronSchedule, Enabled, RetentionDays, OutboxCleanupSettingsValidator, OutboxOptions, BaseBackoffSeconds (+14 more)

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

### Community 156 - "IInboxStore"
Cohesion: 0.24
Nodes (7): IInboxCleanupTarget, ModuleName, IInboxStore, CancellationToken, DateTimeOffset, DefaultIdType, Task

### Community 157 - "IntegrationEvent"
Cohesion: 0.13
Nodes (16): IntegrationEvent, CreatedOn, Id, DateTimeOffset, DefaultIdType, ProductCreatedIntegrationEvent, StoreCreatedIntegrationEvent, DefaultIdType (+8 more)

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
Cohesion: 0.22
Nodes (6): CancellationToken, Task, IServiceAccountTokenProvider, IOptions, IServiceCollection, Setup

### Community 165 - "CurrentUser"
Cohesion: 0.12
Nodes (15): Common.Infrastructure.Auth.Services, Common.Infrastructure.Auth, CurrentUser, Id, IdAsString, IsAuthenticated, Principal, Roles (+7 more)

### Community 166 - "IamModule"
Cohesion: 0.18
Nodes (10): Action, IApplicationBuilder, IEnumerable, RateLimiterOptions, IamModule, ActivitySourceNames, MeterNames, Name (+2 more)

### Community 167 - "Common.Application.Pagination"
Cohesion: 0.04
Nodes (50): Products.Endpoints.Stores.v1.AuditLog, IAM.Endpoints.Users.VersionNeutral.Search, Products.Endpoints.Stores.v1.My.AuditLog, Products.Endpoints.Stores.v1.Search, Common.Application.Pagination, Products.Endpoints.Products.v1.AuditLog, PaginationRequest, After (+42 more)

### Community 168 - "Response"
Cohesion: 0.29
Nodes (5): RouteGroupBuilder, Setup, RouteGroupBuilder, Response, Id

### Community 169 - "SendSecurityAlertRequestHandler"
Cohesion: 0.16
Nodes (15): IReadOnlyDictionary, SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, CancellationToken, IReadOnlyList, Task (+7 more)

### Community 171 - "IBackgroundJobs"
Cohesion: 0.26
Nodes (7): IBackgroundJobs, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 172 - "ResxLocalizationOptions"
Cohesion: 0.40
Nodes (5): ResxLocalizationOptions, DefaultCulture, SupportedCultures, ResxLocalizationOptionsValidator, ICollection

### Community 173 - "RequestBody"
Cohesion: 0.29
Nodes (7): ProductTemplateId, RequestBody, Description, Name, Price, ProductTemplateId, Quantity

### Community 174 - "system_diagnostics"
Cohesion: 0.10
Nodes (14): Common.Application.BackgroundJobs, BackgroundJobs.Telemetry, BackgroundJobs, hangfire, hangfire_annotations, hangfire_dashboard, hangfire_postgresql, hangfire_server (+6 more)

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
Cohesion: 0.09
Nodes (25): CachingEntryDefaults, Duration, FactoryHardTimeout, FactorySoftTimeout, FailSafeMaxDuration, FailSafeThrottleDuration, CachingOptions, AllowInMemoryOnlyInProduction (+17 more)

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
Cohesion: 0.18
Nodes (9): ForwardedHeadersOptions, ReverseProxyOptions, ForwardLimit, IsEnabled, TrustedNetworks, ReverseProxyOptionsValidator, IReadOnlyList, IConfiguration (+1 more)

### Community 188 - "BoundedCaptureStream"
Cohesion: 0.14
Nodes (8): SeekOrigin, HttpResponse, BoundedCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 189 - "JobClaimExtensions"
Cohesion: 0.28
Nodes (9): IdBound, Value, JobClaimExtensions, CancellationToken, DateTimeOffset, DbSet, Expression, Func (+1 more)

### Community 190 - "ServiceAccountTokenCache"
Cohesion: 0.10
Nodes (15): KeycloakPaths, CancellationToken, DateTimeOffset, SemaphoreSlim, Task, TimeSpan, ServiceAccountTokenCache, AccessToken (+7 more)

### Community 191 - ".AddProductToMyStoreAsync"
Cohesion: 0.12
Nodes (16): CancellationToken, ProductTemplate, RouteGroupBuilder, Store, Task, Endpoint, ProductTemplateId, Request (+8 more)

### Community 192 - "KeycloakPermission"
Cohesion: 0.29
Nodes (3): KeycloakPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder

### Community 193 - "Stores/v1/My/Update/Request.cs"
Cohesion: 0.12
Nodes (15): Products.Endpoints.Stores.v1.My.Update, IAM.Endpoints.Otp.VersionNeutral.SendForEmail, ModulesOptions, EnabledModules, ModulesOptionsValidator, IReadOnlyList, Request, CaptchaToken (+7 more)

### Community 194 - "CorsOptions"
Cohesion: 0.25
Nodes (8): CorsOptions, AllowCredentials, AllowedHeaders, AllowedMethods, AllowedOrigins, MaxAgeInSeconds, CorsOptionsValidator, IReadOnlyList

### Community 195 - "InventoryOptions"
Cohesion: 0.17
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

### Community 202 - "Request"
Cohesion: 0.22
Nodes (9): Guid, Request, ClientId, DeviceId, DeviceName, Otp, PhoneNumber, PushToken (+1 more)

### Community 203 - "SmsRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, SmsRateLimitingPolicy (+1 more)

### Community 205 - "AuditableEntity"
Cohesion: 0.18
Nodes (10): ProjectionEntity, SourceVersion, AuditableEntity, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn (+2 more)

### Community 210 - "ConfigureSwaggerOptions"
Cohesion: 0.28
Nodes (7): ApiVersionDescription, IApiVersionDescriptionProvider, IConfigureOptions, OpenApiInfo, IOptions, SwaggerGenOptions, ConfigureSwaggerOptions

### Community 211 - "EventDispatcher"
Cohesion: 0.31
Nodes (7): EventDispatcher, ActivitySource, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task

### Community 212 - "Setup"
Cohesion: 0.33
Nodes (4): ApiVersionSet, Setup, IEndpointRouteBuilder, IServiceCollection

### Community 213 - "ICaptchaService"
Cohesion: 0.09
Nodes (18): CancellationToken, Task, ICaptchaService, CancellationToken, IFeatureManager, RouteGroupBuilder, Task, Endpoint (+10 more)

### Community 214 - "fluentvalidation"
Cohesion: 0.06
Nodes (44): common_application_localization_resources, IAM.Domain.Users, IAM.Endpoints.Otp.VersionNeutral.SendForLogin, Products.Endpoints.Probe.v1, Common.Domain.Extensions, Notifications.Infrastructure.Devices.UpdateCurrentPushToken, Common.Application.Validation, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke (+36 more)

### Community 215 - ".CreateMyStoreAsync"
Cohesion: 0.16
Nodes (9): Store, CancellationToken, Task, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response (+1 more)

### Community 216 - "OtpOptions"
Cohesion: 0.18
Nodes (11): OtpOptions, DummyCode, EmailQuotaWindowMinutes, ExpirationInMinutes, Length, MaxSendsPerEmailPerWindow, MaxSendsPerPhonePerWindow, PhoneQuotaWindowMinutes (+3 more)

### Community 217 - "FeatureFlags"
Cohesion: 0.33
Nodes (5): Checkout, FeatureFlags, IAM, Notifications, Products

### Community 218 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 219 - ".MapEndpoint"
Cohesion: 0.40
Nodes (3): RouteGroupBuilder, Setup, RouteGroupBuilder

### Community 220 - "InboxStore"
Cohesion: 0.32
Nodes (6): InboxStore, ModuleName, CancellationToken, DateTimeOffset, Task, TimeProvider

### Community 221 - "Keycloak realm as code"
Cohesion: 0.25
Nodes (7): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Secrets, Seed users (dev / test only)

### Community 222 - "Key decisions"
Cohesion: 0.29
Nodes (7): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Key decisions

### Community 223 - "microsoft_aspnetcore_mvc"
Cohesion: 0.06
Nodes (32): Products.Endpoints.ProductTemplates.v1.Deactivate, IAM.Endpoints.Users.VersionNeutral.Get, Inventory.Endpoints.StockReservations.v1.Release, Common.Application.ModelBinders, Products.Endpoints.ProductTemplates.v1.Activate, Products.Endpoints.Stores.v1.RemoveProduct, microsoft_aspnetcore_mvc, microsoft_aspnetcore_mvc_modelbinding (+24 more)

### Community 224 - ".MapEndpoint"
Cohesion: 0.40
Nodes (3): RouteGroupBuilder, RouteGroupBuilder, Setup

### Community 225 - "ProcessedMessage"
Cohesion: 0.19
Nodes (10): ProcessedMessage, ConsumerName, MessageId, ProcessedOn, DateTimeOffset, DefaultIdType, DefaultIdType, ProcessedMessageConfiguration (+2 more)

### Community 226 - "NetGsmSmsGateway"
Cohesion: 0.32
Nodes (7): Exception, HttpClient, ILogger, IOptions, JsonSerializerOptions, LoggerMessage, NetGsmSmsGateway

### Community 227 - "RegisterRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, RegisterRateLimitingPolicy (+1 more)

### Community 228 - ".UpsertIfNewerAsync"
Cohesion: 0.15
Nodes (11): ICurrentDbContext, ProjectionUpsertExtensions, Action, CancellationToken, DbSet, Func, Task, ProjectionUpsertOutcome (+3 more)

### Community 229 - ".AddCommonOptions"
Cohesion: 0.40
Nodes (4): Setup, IConfiguration, IHostEnvironment, IServiceCollection

### Community 230 - "InterModuleRequestHandler"
Cohesion: 0.24
Nodes (7): IInterModuleRequestHandler, CancellationToken, Task, InterModuleRequestHandler, CancellationToken, ConsumeContext, Task

### Community 231 - ".UseModule"
Cohesion: 0.22
Nodes (7): DashboardContext, IAuthorizationService, IDashboardAsyncAuthorizationFilter, IApplicationBuilder, IOptions, HangfireCustomAuthorizationFilter, Task

### Community 232 - "DeviceRegistryReconcileJobRegistrar"
Cohesion: 0.31
Nodes (7): CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task, DeviceRegistryReconcileJobRegistrar

### Community 233 - ".HasPatternIndex"
Cohesion: 0.36
Nodes (6): IndexBuilder, TrigramIndexExtensions, EntityTypeBuilder, Expression, Func, ModelBuilder

### Community 234 - "Setup"
Cohesion: 0.33
Nodes (4): IApplicationBuilder, IWebHostEnvironment, Type, Setup

### Community 235 - "StatelessInboxStore"
Cohesion: 0.24
Nodes (6): StatelessInboxStore, Instance, CancellationToken, DateTimeOffset, DefaultIdType, Task

### Community 236 - "IOtpService"
Cohesion: 0.24
Nodes (8): CancellationToken, Task, TimeSpan, IOtpService, OtpVerificationOutcome, InvalidOtp, Success, TooManyAttempts

### Community 237 - "ProblemDetailsExtensions.cs"
Cohesion: 0.40
Nodes (3): ProblemDetails, ProblemDetailsExtensions, ICollection

### Community 238 - "EnrichLogsWithUserInfoMiddleware"
Cohesion: 0.13
Nodes (12): IAuthenticationSchemeProvider, IMiddleware, IApplicationBuilder, HttpContext, RequestDelegate, Task, EnrichLogsWithUserInfoMiddleware, HttpContext (+4 more)

### Community 239 - "InventoryModule.cs"
Cohesion: 0.17
Nodes (8): asp_versioning, asp_versioning_builder, asp_versioning_conventions, Inventory.Endpoints, Common.Endpoints.Versioning, Inventory.Infrastructure.Gateway, Inventory.Endpoints.StockReservations, Inventory.Application.Gateway

### Community 240 - "InterModuleRequestOptions"
Cohesion: 0.10
Nodes (19): ConsumerDefinition, IClientFactory, IConsumerConfigurator, IRegistrationContext, InterModuleRequestOptions, DependencyUnavailableRetryAfterSeconds, HandlerConcurrentMessageLimit, HandlerPrefetchCount (+11 more)

### Community 241 - "Request"
Cohesion: 0.20
Nodes (10): Guid, Request, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken, Password (+2 more)

### Community 242 - ".AddCustomSwagger"
Cohesion: 0.22
Nodes (7): IOpenApiSchema, ISchemaFilter, SchemaFilterContext, IConfigureOptions, IServiceCollection, SwaggerGenOptions, StronglyTypedIdSchemaFilter

### Community 243 - "CustomValidator"
Cohesion: 0.07
Nodes (34): Inventory.Endpoints.StockReservations.v1.Commit, CustomRateLimitingOptionsValidator, FixedWindowValidator, DatabaseOptions, ConnectionString, DatabaseOptionsValidator, JobHousekeepingOptions, PageSize (+26 more)

### Community 244 - "KeyedResilienceProfile"
Cohesion: 0.22
Nodes (9): KeyedResilienceProfile, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, RateLimitPermits, RateLimitQueueLimit (+1 more)

### Community 245 - "DeviceRegistryReconciliationService"
Cohesion: 0.24
Nodes (7): CancellationToken, ILogger, IOptions, LoggerMessage, Task, TimeProvider, DeviceRegistryReconciliationService

### Community 246 - "BackgroundJobsTelemetry"
Cohesion: 0.14
Nodes (11): IServerFilter, PerformedContext, PerformingContext, JobMetricsFilter, BackgroundJobsTelemetry, ActivitySource, ConcurrentDictionary, Counter (+3 more)

### Community 248 - "SendErrorBody"
Cohesion: 0.67
Nodes (3): SendErrorBody, Code, Message

### Community 249 - ".AddProductAsync"
Cohesion: 0.22
Nodes (8): CancellationToken, ProductTemplate, RouteGroupBuilder, Store, Task, Endpoint, Response, Id

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

### Community 261 - "JobStatus"
Cohesion: 0.22
Nodes (6): Common.Application.Jobs, JobStatus, Failed, Queued, Running, Succeeded

### Community 262 - ".SendWithPipelineAsync"
Cohesion: 0.25
Nodes (7): HttpClientKeyedExtensions, CancellationToken, HttpClient, HttpRequestMessage, HttpResponseMessage, ResiliencePipeline, Task

### Community 263 - "Response"
Cohesion: 0.25
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegister, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 264 - "IDbContext"
Cohesion: 0.25
Nodes (7): DatabaseFacade, EntityEntry, IDbContext, AuditLog, ChangeTracker, Database, DbSet

### Community 265 - "BackgroundJobsModule"
Cohesion: 0.25
Nodes (7): BackgroundJobsModule, ActivitySourceNames, MeterNames, Name, StartupPriority, IEndpointRouteBuilder, IEnumerable

### Community 266 - "StrictDateTimeOffsetJsonConverter"
Cohesion: 0.33
Nodes (6): StrictDateTimeOffsetJsonConverter, DateTimeOffset, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 267 - "AuditLogOptions"
Cohesion: 0.33
Nodes (6): AuditLogOptions, PerSchemaRetentionDays, PurgeBatchSize, RetentionDays, AuditLogOptionsValidator, Dictionary

### Community 268 - "DevicesOptions"
Cohesion: 0.33
Nodes (6): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator, IReadOnlyCollection

### Community 269 - "IOutboxMessage"
Cohesion: 0.25
Nodes (7): IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, DateTimeOffset

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

### Community 278 - "AuditLogEntry"
Cohesion: 0.25
Nodes (7): AuditLogEntry, AggregateId, AggregateType, Event, EventType, Version, DefaultIdType

### Community 279 - "Response"
Cohesion: 0.25
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.Create, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 280 - "V1StockReservationCommitConflictDetectedDomainEvent"
Cohesion: 0.22
Nodes (8): DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationCommitConflictDetectedDomainEvent, DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationCommittedDomainEvent

### Community 281 - ".TryReadFromJsonAsync"
Cohesion: 0.40
Nodes (4): HttpContent, CancellationToken, Task, HttpContentJsonExtensions

### Community 282 - "Setup.Logger.cs"
Cohesion: 0.22
Nodes (8): elastic_serilog_sinks, serilog_configuration, serilog_enrichers_span, serilog_events, serilog_exceptions, serilog_formatting_compact, serilog_sinks_opentelemetry, serilog_sinks_systemconsole_themes

### Community 283 - "ResiliencyOptions.cs"
Cohesion: 0.40
Nodes (4): AbstractValidator, KeyedResilienceProfileValidator, ResiliencyOptionsValidator, KeyValuePair

### Community 284 - "RequestBody"
Cohesion: 0.33
Nodes (6): DateTimeOffset, DefaultIdType, RequestBody, ProductId, Quantity, ReservationDeadline

### Community 286 - "Common.Application.Options"
Cohesion: 0.05
Nodes (48): asp_versioning_apiexplorer, Notifications.Application.Otp, Notifications.Application.Sms, Notifications.Infrastructure.Devices, Notifications.Application.Push, IAM.Infrastructure.Keycloak.Representations, Notifications.Infrastructure.Email, Common.Application.Caching (+40 more)

### Community 288 - "DummySmsGateway"
Cohesion: 0.38
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, DummySmsGateway

### Community 289 - ".SetRetryAfterHeader"
Cohesion: 0.40
Nodes (4): RetryAfterHeaderExtensions, HttpResponse, RateLimitLease, TimeSpan

### Community 290 - "ICurrentUser"
Cohesion: 0.07
Nodes (28): ICurrentUser, Id, IdAsString, Roles, SessionId, ICollection, HttpContextExtensions, HttpContext (+20 more)

### Community 291 - ".SetConcurrency"
Cohesion: 0.50
Nodes (3): IBusFactoryConfigurator, ConcurrencyConfiguratorExtensions, IReceiveEndpointConfigurator

### Community 294 - ".SeedProductAsync"
Cohesion: 0.22
Nodes (7): CancellationToken, Task, CancellationToken, List, ProductTemplateId, StoreId, Task

### Community 295 - "RemoveDefaultResponseSchemaFilter"
Cohesion: 0.40
Nodes (4): IOperationFilter, OpenApiOperation, OperationFilterContext, RemoveDefaultResponseSchemaFilter

### Community 296 - "StringExtensions"
Cohesion: 0.29
Nodes (3): SearchValues, StringExtensions, system_buffers

### Community 297 - "SignalROptions"
Cohesion: 0.40
Nodes (5): SignalROptions, RedisConnectionString, RequireRedisBackplaneInProduction, UseRedisBackplane, SignalROptionsValidator

### Community 298 - "ProductTemplates/v1/Create/Request.cs"
Cohesion: 0.29
Nodes (6): Products.Endpoints.ProductTemplates.v1.Create, Request, Brand, Color, Model, RequestValidator

### Community 299 - "InventoryTelemetry"
Cohesion: 0.50
Nodes (4): ActivitySource, Counter, Meter, InventoryTelemetry

### Community 300 - "ProductsTelemetry"
Cohesion: 0.50
Nodes (4): ActivitySource, Counter, Meter, ProductsTelemetry

### Community 304 - ".AddServices"
Cohesion: 0.29
Nodes (7): RecurringJobOptions, IRecurringBackgroundJobs, IConfiguration, IServiceCollection, RecurringBackgroundJobsService, IRecurringJobManagerV2, TimeProvider

### Community 305 - "StronglyTypedIdListReadOnlyJsonConverter"
Cohesion: 0.36
Nodes (6): StronglyTypedIdListReadOnlyJsonConverter, IReadOnlyList, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 306 - "StronglyTypedIdReadOnlyJsonConverter"
Cohesion: 0.32
Nodes (5): StronglyTypedIdReadOnlyJsonConverter, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 307 - "IssueVerificationTokenRequestHandler"
Cohesion: 0.39
Nodes (6): IssueVerificationTokenRequest, IssueVerificationTokenResponse, CancellationToken, IOptions, Task, IssueVerificationTokenRequestHandler

### Community 309 - ".UpdateProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 310 - ".RemoveProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 311 - "StronglyTypedIdBinder"
Cohesion: 0.40
Nodes (4): IModelBinder, ModelBindingContext, StronglyTypedIdBinder, Task

### Community 312 - "Infrastructure/StringExtensions.cs"
Cohesion: 0.40
Nodes (3): opentelemetry_exporter, OtlpExportProtocol, StringExtensions

### Community 313 - "ProjectionReconciliationOptions.cs"
Cohesion: 0.50
Nodes (4): ProjectionReconciliationOptions, MaxPages, PageSize, ProjectionReconciliationOptionsValidator

### Community 314 - "SecurityHeadersOptions.cs"
Cohesion: 0.50
Nodes (4): SecurityHeadersOptions, Headers, SecurityHeadersOptionsValidator, Dictionary

### Community 315 - ".AddModuleDbContext"
Cohesion: 0.40
Nodes (4): Setup, IServiceCollection, NpgsqlDataSource, TimeProvider

### Community 318 - ".AddDeviceRegistryReconciliation"
Cohesion: 0.40
Nodes (3): IServiceCollection, RouteGroupBuilder, Setup

### Community 319 - "RequestBody"
Cohesion: 0.40
Nodes (5): RequestBody, Description, Name, Price, Quantity

### Community 323 - ".PhoneNumberValidation"
Cohesion: 0.50
Nodes (3): IResxLocalizer, IRuleBuilder, IRuleBuilderOptions

## Knowledge Gaps
- **949 isolated node(s):** `Id`, `IdAsString`, `Roles`, `SessionId`, `JwtClaimNames` (+944 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 2213 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **99 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `microsoft_entityframeworkcore`, `BackgroundJobsOptions`, `AuditLogOptions`, `DevicesOptions`, `Host.Infrastructure`, `Common.Domain.ResultMonad`, `Setup`, `EmailOptions`, `OutboxOptions`, `Common.Infrastructure.Extensions`, `Setup.Logger.cs`, `ResiliencyOptions.cs`, `ObservabilityOptions`, `SignalROptions`, `KeycloakOptions`, `ResxLocalizationOptions`, `system_diagnostics`, `CachingOptions`, `ReCaptchaService`, `RabbitMqOptions`, `SmsOptions`, `IdentityScheme`, `ProjectionReconciliationOptions.cs`, `ReverseProxyOptions`, `SecurityHeadersOptions.cs`, `RequestLoggingOptions`, `Stores/v1/My/Update/Request.cs`, `CorsOptions`, `InventoryOptions`, `FullTextSearchOptions`, `OpenApiOptions`, `fluentvalidation`, `PushOptions`, `OtpOptions`, `Program.cs`, `system_globalization`, `Common.InterModuleRequests.Contracts`, `InterModuleRequestOptions`, `CustomValidator`, `.AddCustomHealthChecks`?**
  _High betweenness centrality (0.165) - this node is a cross-community bridge._
- **Why does `Result` connect `Result` to `.SendAsync`, `.GetMeAsync`, `FirebasePushGateway`, `Error`, `ProductTemplate`, `ApplicationUserId`, `Response`, `.RegisterAsync`, `.GetAuditLogAsync`, `StockReservation`, `.CommitStockReservationAsync`, `IEmailGateway`, `.RevokeToken`, `V1StockReservationCommitConflictDetectedDomainEvent`, `.CreateTokensByEmail`, `ISmsGateway`, `.SingleAsResult`, `.ReserveSeriesAsync`, `ICurrentUser`, `ThrottledEmailGateway`, `DummySmsGateway`, `SendPhoneOtpRequestHandler`, `.RefreshToken`, `StringExtensions`, `SendSecurityAlertRequestHandler`, `IInterModuleRequest`, `PaginationQueryableExtensions`, `PaginationResponse`, `Response`, `.AddPushServices`, `ReCaptchaService`, `.UpdateProductAsync`, `.SendOtp`, `BrevoEmailGateway`, `.RemoveProductAsync`, `.SearchProductTemplatesAsync`, `.AddProductToMyStoreAsync`, `.SendAsync`, `.ReserveStockAsync`, `Response`, `Response`, `IProductsDbContext`, `Common.Domain.Events`, `.SearchStoresAsync`, `ResultTelemetryExtensions`, `ICaptchaService`, `.CreateMyStoreAsync`, `.GetProductAsync`, `Response`, `.SearchMyProductsAsync`, `NetGsmSmsGateway`, `AuditableEntityResponse`, `HttpWarehouseGateway`, `.IsRegisteredAsync`, `.ReleaseStockReservationAsync`, `Response`, `.HandleWarehouseWebhookAsync`, `.AddProductAsync`, `Response`, `.TapWhenFeatureEnabledAsync`?**
  _High betweenness centrality (0.141) - this node is a cross-community bridge._
- **Why does `ApplicationUserId` connect `ApplicationUserId` to `IStronglyTypedId`, `NotificationPayload`, `KeycloakAdminClient`, `KeycloakPermissionAuthorizationHandler`, `Response`, `.RegisterAsync`, `GetActiveSessionIdsRequest`, `DeviceRegistration`, `.RevokeToken`, `IntegrationEvent`, `JobRow`, `ICurrentUser`, `For`, `CurrentUser`, `SendSecurityAlertRequestHandler`, `PaginationResponse`, `V1StoreCreatedDomainEvent`, `Request`, `Response`, `KeycloakTokenClient`, `.HandleAsync`, `Response`, `AuditableEntity`, `IAuditableEntity`, `.SearchStoresAsync`, `.Configure`, `Store`, `.CreateMyStoreAsync`, `Response`, `Seeder`, `system_globalization`, `microsoft_aspnetcore_mvc`, `AuditableEntityResponse`, `StockLevel`, `Response`, `NotificationsHub`?**
  _High betweenness centrality (0.061) - this node is a cross-community bridge._
- **What connects `Id`, `IdAsString`, `Roles` to the rest of the system?**
  _949 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `microsoft_entityframeworkcore` be split into smaller, more focused modules?**
  _Cohesion score 0.050853548966756514 - nodes in this community are weakly interconnected._
- **Should `RedisFixedWindowRateLimiter` be split into smaller, more focused modules?**
  _Cohesion score 0.13333333333333333 - nodes in this community are weakly interconnected._
- **Should `Error` be split into smaller, more focused modules?**
  _Cohesion score 0.06881720430107527 - nodes in this community are weakly interconnected._