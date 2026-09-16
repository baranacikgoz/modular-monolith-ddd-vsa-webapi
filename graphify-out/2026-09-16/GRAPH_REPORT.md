# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-09-16)

## Corpus Check
- 544 files · ~79,551 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 4449 nodes · 8005 edges · 363 communities (271 shown, 87 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 275 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `b13c45d0`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- IAM.Application.Keycloak
- NotificationPayload
- OutboxProcessor
- DeviceRegistryReconcileJobRegistrar
- Hybrid DDD (Writes) / VSA (Reads)
- ApplicationUserId
- RedisFixedWindowRateLimiter
- ApplyAuditingInterceptor
- FirebasePushGateway
- NotificationsModule.cs
- Error
- Common.Domain.ResultMonad
- V1ProductAddedToStoreDomainEvent
- .SendOtp
- UserRepresentation
- KeycloakPermissionAuthorizationHandler
- EmailOptions
- .UseModule
- DomainEvent
- IntegrationEventHandlerBase
- IntegrationEventOutbox
- BoundedRequestCaptureStream
- DeviceRegistration
- .AddBrevo
- ICurrentUser
- NetGsmSmsGateway
- IEvent
- RequestResponseBodyLoggingMiddleware
- KeycloakTokenClient
- Result
- Product
- Request
- Endpoint
- Common.Infrastructure.Persistence
- KeycloakPermissionClient
- OutboxMessage
- CustomRateLimitingOptions
- StockReservation
- .RefreshToken
- ObservabilityOptions
- .AssignBasicRoleOrRollbackAsync
- Request
- .SaveWithOutboxAsync
- CreateStoreRateLimitingPolicy
- KeycloakOptions
- .RegisterAsync
- Products.Domain.Stores
- Outbox Misuse Check
- AuditableEntity
- Add Integration Event Command
- Response
- .AddKeycloakInfrastructure
- ReCaptchaService
- Inventory.Domain.StockReservations
- SendEmailOtpRequestHandler
- BrevoEmailGateway
- .AddPersistence
- Cross-Module Reference Violation
- OutboxMetricsJob
- .SearchProductTemplatesAsync
- Bogus Test Data
- Request
- IDbContext
- RequestLoggingOptions
- .SendCoreAsync
- IBackgroundJobs
- OutboxModule
- AuditableEntityResponse
- ValueObject
- NotificationsModule
- IStronglyTypedId
- Full-Text Search
- Response
- .WriteAsync
- AsNoTracking Coverage Check
- CaptchaOptions
- PaginationRequest
- IProductsDbContext
- CheckRegistrationRateLimitingPolicy
- CreateStockLevelOnProductCreatedHandler
- Request
- .SearchStoresAsync
- Inventory.Domain.StockReservations.DomainEvents.v1
- NotificationsDbContext
- .Configure
- ResultTelemetryExtensions
- Store
- PushOptions
- Configuration-Driven Module Registration
- Program.cs
- .GetProductAsync
- KeycloakPermissionPolicyProvider
- Common.Domain.StronglyTypedIds
- .GetClientKey
- OtpOptions
- .SearchMyProductsAsync
- DatabaseSeederOrchestrator
- StockReservationExpirySweepService
- Request
- .AddNetGsm
- OutboxDbContext
- ResiliencyOptions
- HttpContextTargetingContextAccessor
- BackgroundJobsTelemetry
- Request
- Response
- OutboxModule.cs
- .ReleaseStockReservationAsync
- StockLevel
- .IsRegisteredAsync
- V1StoreCreatedDomainEvent
- BackgroundJobsService
- OutboxCleanupJob
- ProductsDbContext
- Response
- .RegisterAsync
- .WriteProblemAsync
- Response
- IModule
- ProductsModule
- .HandleWarehouseWebhookAsync
- .GetVariantAsync
- TokenRefreshRateLimitingPolicy
- FullTextSearchOptions
- .AddCustomHealthChecks
- .TapWhenFeatureEnabledAsync
- GlobalExceptionHandlingMiddleware
- BaseDbContext
- EmailRateLimitingPolicy
- MassTransitInterModuleRequestClient
- docker-compose.yml (Base Stack)
- .AddInfrastructure
- OtpVerifyRateLimitingPolicy
- Consumer Idempotency (IntegrationEventHandlerBase)
- SendRequestBody
- Response
- .SingleAsResult
- .WriteTooManyRequestsToResponse
- .InvokeAsync
- ProductTemplate
- Response
- .GetMeAsync
- Response
- SeedingCompletionTracker
- Response
- EnrichLogsWithUserInfoMiddleware
- Response
- IRecurringBackgroundJobs
- AuditLogRetentionJobRegistrar
- OtpService
- StockReservationExpirySweepJobRegistrar
- OutboxOptions
- .EnsureNoMigrationsPending
- SwaggerDefaultValues
- CustomValidator
- EmailMessage
- Request
- Request
- EventDispatcher
- IInventoryDbContext
- OtpServiceBase
- .ReserveSeriesAsync
- HttpWarehouseGateway
- For
- Request
- CurrentUser
- IamModule
- PaginationResponse
- Endpoint
- CachedCaptchaService
- Notifications.Application/IAssemblyReference.cs
- IOtpService
- ResxLocalizationOptions
- RequestBody
- .AddPushServices
- AuditLogDto
- .FixedWindow
- .AddProductToMyStoreAsync
- DummyPushGateway
- CachingOptions
- SmsOptions
- RabbitMqOptions
- IAM.Domain
- SendRequestBody
- RequestBody
- Common.Domain.Devices
- .Get
- ReverseProxyOptions
- BoundedCaptureStream
- ServiceAccountTokenCache
- v1/RemoveProduct/Request.cs
- IssueVerificationTokenRequestHandler
- .SendCoreAsync
- CorsOptions
- InventoryOptions
- TokenCreateRateLimitingPolicy
- LogProductCatalogChangeHandler
- InventoryModule
- OpenApiOptions
- KeyValuePair
- KeycloakScopes
- Request
- SmsRateLimitingPolicy
- Configuration-Driven Module Loading
- UtcDateTimeOffsetConverter
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- ConfigureSwaggerOptions
- Common.Application.BackgroundJobs
- Setup
- ProductsTelemetry
- IAMModule.cs
- Request
- Common.Application.Validation
- FeatureFlags
- Response
- IAM.Endpoints.Common.Validations
- Seeder
- Keycloak realm as code
- .RemoveProductAsync
- GetActiveSessionIdsRequest
- AuditLogRetentionService
- BackgroundJobsOptions
- .HandleAsync
- BackgroundJobsModule
- .ListSessions
- .AddCommonOptions
- .ReserveStockAsync
- Request
- Common.Application.Options
- ModulesOptions
- Setup
- AuditLogEntry
- .UpdateCurrentPushToken
- ReCaptchaResponse
- InterModuleRequestHandler
- .TryReadFromJsonAsync
- RedisOtpService
- RegisterRateLimitingPolicy
- .AddCustomSwagger
- FixedWindow
- .AddAuthInfrastructure
- Host.Swagger
- KeycloakPermission
- DeviceRegistryReconciliationService
- SendErrorBody
- Common.Application.EventBus
- FirebaseServiceAccountOptions
- StoreId
- .GetAuditLogAsync
- Products.Domain/IAssemblyReference.cs
- IAM.Application/IAssemblyReference.cs
- Products.Infrastructure/IAssemblyReference.cs
- Products.Application/IAssemblyReference.cs
- Common.Infrastructure/IAssemblyReference.cs
- Notifications.Domain/IAssemblyReference.cs
- IAM.Infrastructure/IAssemblyReference.cs
- .MapEndpoint
- Common.Infrastructure.Persistence.AuditLog
- .AddCommonCaching
- .AddServices
- DevicesOptions
- .UpdateMyStoreAsync
- VersionNeutral/Get/Request.cs
- StringExtensions
- SendForRegistration/Request.cs
- DefaultResponsesOperationFilter
- .AddDeviceRegistryReconciliation
- Common.InterModuleRequests.Contracts
- Response
- GetProductRequest
- Request
- Inventory.Endpoints.StockReservations
- SendResponseBody
- ProductId
- .SaveChangesAsync
- InventoryTelemetry
- .CommitStockReservationAsync
- Infrastructure/Setup.cs
- .RequireOtpTemplateForDefaultCulture
- RequestBody
- ProductsModule.cs
- .ActivateProductTemplateAsync
- .AddStockReservationExpirySweep
- ICaptchaService
- Setup
- .RemoveMyProductAsync
- My/Create/Request.cs
- IAutoMigrateMarker.cs
- .AddProductAsync
- .Chunk
- SmsMessage
- AuditLogOptions
- Request
- .UpdateStoreAsync
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
- Deploy-Time Materialized Config

## God Nodes (most connected - your core abstractions)
1. `Result` - 133 edges
2. `Common.Application.Options` - 125 edges
3. `Common.Domain.ResultMonad` - 103 edges
4. `CustomValidator` - 80 edges
5. `Common.Application.Validation` - 71 edges
6. `ApplicationUserId` - 70 edges
7. `Common.Application.Auth` - 66 edges
8. `Common.Application.Extensions` - 62 edges
9. `Common.Domain.StronglyTypedIds` - 61 edges
10. `Common.InterModuleRequests.Contracts` - 51 edges

## Surprising Connections (you probably didn't know these)
- `Aspire Dashboard Service (mm.aspire-dashboard)` --conceptually_related_to--> `Observability (OpenTelemetry)`  [INFERRED]
  docker-compose.yml → CLAUDE.md
- `StockReservationErrors` --references--> `Error`  [EXTRACTED]
  src/Modules/Inventory/Inventory.Domain/StockReservations/Errors/StockReservationErrors.cs → src/Common/Common.Domain/ResultMonad/Error.cs
- `AuditLogDto` --references--> `ApplicationUserId`  [EXTRACTED]
  src/Common/Common.Application/AuditLog/AuditLogDto.cs → src/Common/Common.Domain/StronglyTypedIds/ApplicationUserId.cs
- `ICurrentUser` --references--> `ApplicationUserId`  [EXTRACTED]
  src/Common/Common.Application/Auth/ICurrentUser.cs → src/Common/Common.Domain/StronglyTypedIds/ApplicationUserId.cs
- `CurrentUser` --implements--> `ICurrentUser`  [EXTRACTED]
  src/Common/Common.Infrastructure/Auth/Services/CurrentUser.cs → src/Common/Common.Application/Auth/ICurrentUser.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Local Infrastructure Stack** — docker_compose_postgres, docker_compose_rabbitmq, docker_compose_redis, docker_compose_aspire_dashboard [EXTRACTED 1.00]

## Communities (363 total, 87 thin omitted)

### Community 0 - "IAM.Application.Keycloak"
Cohesion: 0.11
Nodes (9): IAM.Endpoints.Users, IAM.Infrastructure.Keycloak.Representations, Common.InterModuleRequests.IAM, IAM.Infrastructure.InterModuleRequestHandlers, IAM.Application.Keycloak, IAM.Domain.Errors, IAM.Infrastructure.Keycloak, OAuthErrors (+1 more)

### Community 1 - "NotificationPayload"
Cohesion: 0.06
Nodes (35): Notifications.Infrastructure.Hubs, Notifications.Application.Hubs, Hub, HubConnectionContext, IHubContext, IUserIdProvider, RedisOptions, SignalROptions (+27 more)

### Community 2 - "OutboxProcessor"
Cohesion: 0.22
Nodes (11): IPublishEndpoint, CancellationToken, Exception, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task (+3 more)

### Community 3 - "DeviceRegistryReconcileJobRegistrar"
Cohesion: 0.31
Nodes (7): CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task, DeviceRegistryReconcileJobRegistrar

### Community 5 - "ApplicationUserId"
Cohesion: 0.11
Nodes (29): HttpRequestMessage, ApplicationUserId, IsEmpty, Value, DefaultIdType, CancellationToken, IReadOnlyList, Task (+21 more)

### Community 6 - "RedisFixedWindowRateLimiter"
Cohesion: 0.13
Nodes (16): RateLimiter, RateLimiterStatistics, RateLimitLease, FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration (+8 more)

### Community 7 - "ApplyAuditingInterceptor"
Cohesion: 0.09
Nodes (19): SaveChangesInterceptor, ISearchLocalized, Language, ApplyAuditingInterceptor, CancellationToken, DbContextEventData, InterceptionResult, TimeProvider (+11 more)

### Community 8 - "FirebasePushGateway"
Cohesion: 0.24
Nodes (8): FirebaseApp, FirebaseMessaging, IDisposable, Exception, ILogger, LoggerMessage, TimeSpan, FirebasePushGateway

### Community 9 - "NotificationsModule.cs"
Cohesion: 0.11
Nodes (9): Notifications.Application.Sms, Notifications.Infrastructure.Devices, Notifications.Application.Push, Notifications.Infrastructure.Push, Notifications.Infrastructure.Sms, Notifications.Infrastructure.Sms.NetGsm, Notifications.Infrastructure.Push.Firebase, Notifications.Infrastructure (+1 more)

### Community 10 - "Error"
Cohesion: 0.07
Nodes (21): StringLocalizerExtensions, IStringLocalizer, Error, Key, ParameterName, StatusCode, SubErrors, Value (+13 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.14
Nodes (11): Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Products.Infrastructure.Telemetry, IAM.Infrastructure.Auth, Products.Application.Persistence, Common.Domain.ResultMonad, Common.Application.Pagination (+3 more)

### Community 12 - "V1ProductAddedToStoreDomainEvent"
Cohesion: 0.13
Nodes (13): StoreId, ProductSnapshot, ProductTemplateId, ProductSnapshot, V1ProductAddedToStoreDomainEvent, V1ProductAddedToStoreDomainEventExtensions, ProductSnapshot, ProductTemplateId (+5 more)

### Community 13 - ".SendOtp"
Cohesion: 0.08
Nodes (23): OtpDispatchErrors, SendPhoneOtpRequest, SendPhoneOtpResponse, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, CancellationToken (+15 more)

### Community 14 - "UserRepresentation"
Cohesion: 0.07
Nodes (29): Dictionary, List, CredentialRepresentation, Temporary, Type, Value, ErrorRepresentation, Error (+21 more)

### Community 15 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.12
Nodes (17): AuthorizationHandler, AuthorizationHandlerContext, IAuthorizationRequirement, CancellationToken, ClaimsPrincipal, HttpContext, IFusionCache, IHttpContextAccessor (+9 more)

### Community 16 - "EmailOptions"
Cohesion: 0.09
Nodes (24): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+16 more)

### Community 17 - ".UseModule"
Cohesion: 0.20
Nodes (7): DashboardContext, IAuthorizationService, IDashboardAsyncAuthorizationFilter, IApplicationBuilder, IOptions, HangfireCustomAuthorizationFilter, Task

### Community 18 - "DomainEvent"
Cohesion: 0.08
Nodes (25): AggregateRoot, Events, Id, Version, IReadOnlyCollection, List, IAggregateRoot, Events (+17 more)

### Community 19 - "IntegrationEventHandlerBase"
Cohesion: 0.22
Nodes (12): IConsumer, IntegrationEventHandlerBase, MaxEventAge, CancellationToken, ConsumeContext, DefaultIdType, IFusionCache, ILogger (+4 more)

### Community 20 - "IntegrationEventOutbox"
Cohesion: 0.13
Nodes (11): Lock, IntegrationEventOutbox, IReadOnlyList, List, IntegrationEvent, CreatedOn, Id, DateTimeOffset (+3 more)

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.14
Nodes (8): ReadOnlySpan, BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position, Stream

### Community 22 - "DeviceRegistration"
Cohesion: 0.12
Nodes (15): DateTimeOffset, DefaultIdType, Guid, DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive (+7 more)

### Community 23 - ".AddBrevo"
Cohesion: 0.13
Nodes (13): CancellationToken, Task, IEmailGateway, IConfiguration, IFusionCache, IOptions, IServiceCollection, Setup (+5 more)

### Community 24 - "ICurrentUser"
Cohesion: 0.07
Nodes (26): ICurrentUser, Id, IdAsString, Roles, SessionId, ICollection, HttpContextExtensions, HttpContext (+18 more)

### Community 25 - "NetGsmSmsGateway"
Cohesion: 0.16
Nodes (14): SendResponseBody, CancellationToken, Exception, HttpClient, ILogger, IOptions, JsonSerializerOptions, LoggerMessage (+6 more)

### Community 26 - "IEvent"
Cohesion: 0.11
Nodes (13): CancellationToken, Task, IEventHandler, CancellationToken, Task, IEventHandlerWrapper, CancellationToken, Task (+5 more)

### Community 27 - "RequestResponseBodyLoggingMiddleware"
Cohesion: 0.19
Nodes (7): IDiagnosticContext, HttpContext, IList, IOptions, PathString, RequestDelegate, RequestResponseBodyLoggingMiddleware

### Community 28 - "KeycloakTokenClient"
Cohesion: 0.11
Nodes (24): JsonWebTokenHandler, CancellationToken, Task, IKeycloakTokenClient, DateTimeOffset, KeycloakTokens, CancellationToken, Dictionary (+16 more)

### Community 29 - "Result"
Cohesion: 0.11
Nodes (16): Result, Error, IsFailure, Success, Value, Func, Task, AsyncExtensions (+8 more)

### Community 30 - "Product"
Cohesion: 0.07
Nodes (26): ProductId, V1ProductDescriptionUpdatedDomainEvent, ProductId, V1ProductNameUpdatedDomainEvent, ProductId, V1ProductQuantityDecreasedDomainEvent, ProductId, V1ProductQuantityIncreasedDomainEvent (+18 more)

### Community 31 - "Request"
Cohesion: 0.17
Nodes (10): IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, RouteGroupBuilder, Endpoint, Request, Email, Otp, RequestValidator, Response (+2 more)

### Community 32 - "Endpoint"
Cohesion: 0.29
Nodes (5): Products.Endpoints.Stores, RouteGroupBuilder, Setup, RouteGroupBuilder, Endpoint

### Community 33 - "Common.Infrastructure.Persistence"
Cohesion: 0.11
Nodes (11): Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Common.Application.Persistence, Inventory.Infrastructure.Persistence.EntityConfigurations, Notifications.Infrastructure.Persistence, Inventory.Infrastructure.Persistence, Common.Infrastructure.EventBus, Common.Infrastructure.Persistence.Auditing (+3 more)

### Community 34 - "KeycloakPermissionClient"
Cohesion: 0.12
Nodes (20): CancellationToken, Dictionary, HttpClient, HttpResponseMessage, ILogger, IOptions, IReadOnlyList, List (+12 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.10
Nodes (20): IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, DateTimeOffset, OutboxMessage (+12 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.07
Nodes (24): CheckRegistrationRateLimitingPolicy, CreateStoreRateLimitingPolicy, EmailRateLimitingPolicy, OtpVerifyRateLimitingPolicy, RegisterRateLimitingPolicy, SmsRateLimitingPolicy, CustomRateLimitingOptions, CheckRegistration (+16 more)

### Community 37 - "StockReservation"
Cohesion: 0.09
Nodes (22): DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationExpiredDomainEvent, DateTimeOffset, StockReservationId, V1StockReservationReleaseAttemptAbandonedDomainEvent, DateTimeOffset (+14 more)

### Community 38 - ".RefreshToken"
Cohesion: 0.11
Nodes (16): IAM.Domain.Users, IAM.Endpoints.Tokens.VersionNeutral.Refresh, Constants, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request (+8 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.08
Nodes (22): IHostBuilder, LoggerConfiguration, LoggerMinimumLevelConfiguration, ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics (+14 more)

### Community 40 - ".AssignBasicRoleOrRollbackAsync"
Cohesion: 0.12
Nodes (12): CancellationToken, Exception, ILogger, LoggerMessage, Task, RegistrationCompletion, ActivitySource, Counter (+4 more)

### Community 41 - "Request"
Cohesion: 0.12
Nodes (15): IAM.Endpoints.Users.VersionNeutral.SelfRegisterByEmail, Guid, Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName (+7 more)

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
Cohesion: 0.08
Nodes (31): IInterModuleRequest, BindDeviceSessionRequest, BindDeviceSessionResponse, Guid, OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts (+23 more)

### Community 46 - "Products.Domain.Stores"
Cohesion: 0.08
Nodes (20): Products.Endpoints.Products.v1.My.Get, Products.Endpoints.Products.v1.Search, Products.Domain.Products, Common.Application.DTOs, Products.Endpoints.Stores.v1.Get, Products.Endpoints.ProductTemplates.v1.Get, Products.Domain.Stores, Products.Endpoints.Products.v1.Get (+12 more)

### Community 48 - "AuditableEntity"
Cohesion: 0.14
Nodes (14): AuditableEntity, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, Version, DateTimeOffset (+6 more)

### Community 50 - "Response"
Cohesion: 0.14
Nodes (13): IAM.Endpoints.Users.VersionNeutral.Search, DateOnly, DateTimeOffset, Response, BirthDate, CreatedOn, Email, Enabled (+5 more)

### Community 51 - ".AddKeycloakInfrastructure"
Cohesion: 0.16
Nodes (10): CancellationToken, Task, IServiceAccountTokenProvider, HttpClient, IOptions, TimeProvider, ServiceAccountTokenProvider, IOptions (+2 more)

### Community 52 - "ReCaptchaService"
Cohesion: 0.21
Nodes (10): FormUrlEncodedContent, ReCaptchaResponse, CancellationToken, Exception, HttpClient, ILogger, IOptions, LoggerMessage (+2 more)

### Community 53 - "Inventory.Domain.StockReservations"
Cohesion: 0.10
Nodes (14): Inventory.Endpoints.StockReservations.v1.ReserveSeries, Inventory.Infrastructure.InterModuleRequestHandlers, Inventory.Endpoints, Inventory.Application.Persistence, Inventory.Infrastructure.Gateway, Inventory.Domain.StockReservations, Inventory.Domain.StockReservations.Errors, Inventory.Endpoints.StockReservations.v1.Get (+6 more)

### Community 54 - "SendEmailOtpRequestHandler"
Cohesion: 0.18
Nodes (12): EmailOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, SendEmailOtpRequest, SendEmailOtpResponse, CancellationToken, IFusionCache (+4 more)

### Community 55 - "BrevoEmailGateway"
Cohesion: 0.23
Nodes (10): Exception, HttpClient, HttpStatusCode, ILogger, IOptions, JsonSerializerOptions, LoggerMessage, BrevoEmailGateway (+2 more)

### Community 56 - ".AddPersistence"
Cohesion: 0.14
Nodes (11): IDatabaseSeeder, Priority, CancellationToken, Task, CancellationToken, IServiceScopeFactory, Task, ProductsDatabaseSeeder (+3 more)

### Community 58 - "OutboxMetricsJob"
Cohesion: 0.13
Nodes (14): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxMetricsJob (+6 more)

### Community 59 - ".SearchProductTemplatesAsync"
Cohesion: 0.15
Nodes (11): LikePattern, CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response (+3 more)

### Community 61 - "Request"
Cohesion: 0.20
Nodes (9): Products.Endpoints.Stores.v1.My.AddProduct, ProductTemplateId, Request, Description, Name, Price, ProductTemplateId, Quantity (+1 more)

### Community 62 - "IDbContext"
Cohesion: 0.25
Nodes (7): DatabaseFacade, EntityEntry, IDbContext, AuditLog, ChangeTracker, Database, DbSet

### Community 63 - "RequestLoggingOptions"
Cohesion: 0.11
Nodes (20): IPostConfigureOptions, RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes (+12 more)

### Community 64 - ".SendCoreAsync"
Cohesion: 0.23
Nodes (8): SendErrorBody, CancellationToken, HttpResponseMessage, SendRequestBody, Task, SendContact, Email, Name

### Community 65 - "IBackgroundJobs"
Cohesion: 0.26
Nodes (7): IBackgroundJobs, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 66 - "OutboxModule"
Cohesion: 0.13
Nodes (16): Action, Exception, IApplicationBuilder, IEndpointRouteBuilder, IEnumerable, IHostApplicationLifetime, ILogger, ILoggerFactory (+8 more)

### Community 67 - "AuditableEntityResponse"
Cohesion: 0.12
Nodes (15): AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, DateTimeOffset, CancellationToken (+7 more)

### Community 68 - "ValueObject"
Cohesion: 0.25
Nodes (4): Common.Domain, IComparable, ValueObject, IEnumerable

### Community 69 - "NotificationsModule"
Cohesion: 0.15
Nodes (10): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, NotificationsModule, ActivitySourceNames, MeterNames (+2 more)

### Community 70 - "IStronglyTypedId"
Cohesion: 0.06
Nodes (32): JsonConverter, StrictDateTimeOffsetJsonConverter, DateTimeOffset, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, StronglyTypedIdListReadOnlyJsonConverter (+24 more)

### Community 71 - "Full-Text Search"
Cohesion: 0.06
Nodes (33): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Add a new language/culture, Add search to a new entity _(Build checklist)_ (+25 more)

### Community 72 - "Response"
Cohesion: 0.20
Nodes (9): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Description, Name, Price (+1 more)

### Community 73 - ".WriteAsync"
Cohesion: 0.33
Nodes (5): Memory, ReadOnlyMemory, CancellationToken, Task, ValueTask

### Community 75 - "CaptchaOptions"
Cohesion: 0.16
Nodes (13): CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey, Provider, ScoreThreshold, SecretKey (+5 more)

### Community 76 - "PaginationRequest"
Cohesion: 0.08
Nodes (24): Products.Endpoints.Stores.v1.My.AuditLog, Products.Endpoints.Stores.v1.Search, Products.Endpoints.Products.v1.AuditLog, PaginationRequest, PageNumber, PageSize, Skip, Take (+16 more)

### Community 77 - "IProductsDbContext"
Cohesion: 0.09
Nodes (18): DbSet, Store, IProductsDbContext, Products, ProductTemplates, Stores, CancellationToken, RouteGroupBuilder (+10 more)

### Community 78 - "CheckRegistrationRateLimitingPolicy"
Cohesion: 0.25
Nodes (8): IRateLimiterPolicy, CancellationToken, Func, IOptions, OnRejectedContext, ValueTask, CheckRegistrationRateLimitingPolicy, OnRejected

### Community 79 - "CreateStockLevelOnProductCreatedHandler"
Cohesion: 0.19
Nodes (11): ProductCreatedIntegrationEvent, StoreCreatedIntegrationEvent, DefaultIdType, CancellationToken, DefaultIdType, IFusionCache, ILogger, IOptions (+3 more)

### Community 80 - "Request"
Cohesion: 0.20
Nodes (10): Products.Endpoints.Stores.v1.Update, RequestBody, Request, Body, Id, RequestBody, Address, Description (+2 more)

### Community 81 - ".SearchStoresAsync"
Cohesion: 0.15
Nodes (12): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Address (+4 more)

### Community 82 - "Inventory.Domain.StockReservations.DomainEvents.v1"
Cohesion: 0.09
Nodes (17): Inventory.Domain.StockReservations.DomainEvents.v1, DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationCommitConflictDetectedDomainEvent, DateTimeOffset, DefaultIdType, StockReservationId (+9 more)

### Community 83 - "NotificationsDbContext"
Cohesion: 0.14
Nodes (13): DbSet, INotificationsDbContext, DeviceRegistrations, DbContextOptions, DbSet, ILogger, TimeProvider, NotificationsDbContext (+5 more)

### Community 84 - ".Configure"
Cohesion: 0.08
Nodes (23): AuditableEntityConfiguration, EntityTypeBuilder, AuditLogEntryConfiguration, EntityTypeBuilder, DomainEventConverter, JsonSerializerOptions, IntegrationEventConverter, JsonSerializerOptions (+15 more)

### Community 85 - "ResultTelemetryExtensions"
Cohesion: 0.38
Nodes (4): Activity, ResultTelemetryExtensions, ActivitySource, Task

### Community 86 - "Store"
Cohesion: 0.09
Nodes (19): StoreId, V1StoreAddressUpdatedDomainEvent, StoreId, V1StoreDescriptionUpdatedDomainEvent, StoreId, V1StoreNameUpdatedDomainEvent, IReadOnlyCollection, List (+11 more)

### Community 87 - "PushOptions"
Cohesion: 0.19
Nodes (13): PushOptions, Provider, SendTimeoutSeconds, ServiceAccount, Templates, PushOptionsValidator, PushProvider, Dummy (+5 more)

### Community 89 - "Program.cs"
Cohesion: 0.22
Nodes (6): ConfigurationManager, Host, Host.Configurations, Setup, Program, WebApplicationBuilder

### Community 90 - ".GetProductAsync"
Cohesion: 0.25
Nodes (10): GetStockLevelRequest, GetStockLevelResponse, DefaultIdType, CancellationToken, Task, GetStockLevelRequestHandler, CancellationToken, DefaultIdType (+2 more)

### Community 91 - "KeycloakPermissionPolicyProvider"
Cohesion: 0.24
Nodes (8): AuthorizationOptions, AuthorizationPolicy, DefaultAuthorizationPolicyProvider, IAuthorizationPolicyProvider, ConcurrentDictionary, IOptions, Task, KeycloakPermissionPolicyProvider

### Community 92 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.08
Nodes (11): Products.Domain.Products.DomainEvents.v1, Common.Domain.StronglyTypedIds, Common.Domain.Events, Notifications.Domain.Devices, Common.Application.JsonConverters, Common.Domain.Entities, Common.Domain.Aggregates, Common.Infrastructure.Persistence.ValueConverters (+3 more)

### Community 93 - ".GetClientKey"
Cohesion: 0.18
Nodes (8): IAM.Endpoints.Captcha.VersionNeutral, IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, RouteGroupBuilder, Endpoint, Response, ClientKey, RouteGroupBuilder, Setup

### Community 94 - "OtpOptions"
Cohesion: 0.18
Nodes (11): OtpOptions, DummyCode, EmailQuotaWindowMinutes, ExpirationInMinutes, Length, MaxSendsPerEmailPerWindow, MaxSendsPerPhonePerWindow, PhoneQuotaWindowMinutes (+3 more)

### Community 95 - ".SearchMyProductsAsync"
Cohesion: 0.10
Nodes (18): ISearchLanguageResolver, UniversalConfig, SearchLanguageResolver, UniversalConfig, IOptions, Setup, IServiceCollection, CancellationToken (+10 more)

### Community 96 - "DatabaseSeederOrchestrator"
Cohesion: 0.27
Nodes (9): BackgroundService, DatabaseSeederOrchestrator, CancellationToken, Exception, IHostApplicationLifetime, ILogger, IServiceScopeFactory, LoggerMessage (+1 more)

### Community 97 - "StockReservationExpirySweepService"
Cohesion: 0.24
Nodes (9): CancellationToken, Exception, Guid, ILogger, IOptions, LoggerMessage, Task, TimeProvider (+1 more)

### Community 98 - "Request"
Cohesion: 0.09
Nodes (22): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, StoreId, Request (+14 more)

### Community 99 - ".AddNetGsm"
Cohesion: 0.13
Nodes (13): CancellationToken, Task, ISmsGateway, IConfiguration, IFusionCache, IOptions, IServiceCollection, Setup (+5 more)

### Community 100 - "OutboxDbContext"
Cohesion: 0.12
Nodes (14): DbContext, IOutboxDbContext, OutboxMessages, CancellationToken, DbSet, Task, IConfiguration, IServiceCollection (+6 more)

### Community 101 - "ResiliencyOptions"
Cohesion: 0.10
Nodes (19): HttpStandardResilienceOptions, IHttpClientBuilder, ResiliencyOptions, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds (+11 more)

### Community 102 - "HttpContextTargetingContextAccessor"
Cohesion: 0.17
Nodes (9): Common.Infrastructure.FeatureManagement, ITargetingContextAccessor, HttpContextTargetingContextAccessor, IHttpContextAccessor, ValueTask, Setup, IConfiguration, IServiceCollection (+1 more)

### Community 103 - "BackgroundJobsTelemetry"
Cohesion: 0.13
Nodes (11): IServerFilter, PerformedContext, PerformingContext, JobMetricsFilter, BackgroundJobsTelemetry, ActivitySource, ConcurrentDictionary, Counter (+3 more)

### Community 104 - "Request"
Cohesion: 0.20
Nodes (10): Products.Endpoints.Products.v1.My.Search, Request, Description, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name (+2 more)

### Community 105 - "Response"
Cohesion: 0.22
Nodes (8): RouteGroupBuilder, Endpoint, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 106 - "OutboxModule.cs"
Cohesion: 0.18
Nodes (7): Outbox, Common.Infrastructure.Persistence.Outbox, Outbox.Persistence, Common.Application.Persistence.Outbox, Outbox.Telemetry, IEntityTypeConfiguration, OutboxMessageConfig

### Community 107 - ".ReleaseStockReservationAsync"
Cohesion: 0.13
Nodes (14): CancellationToken, Task, IWarehouseGateway, CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint (+6 more)

### Community 108 - "StockLevel"
Cohesion: 0.18
Nodes (10): DefaultIdType, StockLevelId, V1StockLevelCreatedDomainEvent, DefaultIdType, StockLevel, ProductId, QuantityOnHand, StockLevelId (+2 more)

### Community 109 - ".IsRegisteredAsync"
Cohesion: 0.18
Nodes (10): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, PhoneNumber, RequestValidator (+2 more)

### Community 110 - "V1StoreCreatedDomainEvent"
Cohesion: 0.29
Nodes (8): DomainEventHandlerBase, CancellationToken, Task, SimulateSomeBusinessHandler, StoreCreatedIntegrationEventPublishingHandler, V1StoreCreatedDomainEventHandlers, StoreId, V1StoreCreatedDomainEvent

### Community 111 - "BackgroundJobsService"
Cohesion: 0.23
Nodes (8): IBackgroundJobClientV2, BackgroundJobsService, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 112 - "OutboxCleanupJob"
Cohesion: 0.27
Nodes (8): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxCleanupJob

### Community 113 - "ProductsDbContext"
Cohesion: 0.15
Nodes (12): DbContextOptions, DbSet, ILogger, ProductTemplate, Store, TimeProvider, ProductsDbContext, Products (+4 more)

### Community 114 - "Response"
Cohesion: 0.11
Nodes (17): IAM.Endpoints.Users.VersionNeutral.Me.Get, RouteGroupBuilder, Endpoint, DateOnly, DateTimeOffset, IReadOnlyCollection, Response, BirthDate (+9 more)

### Community 115 - ".RegisterAsync"
Cohesion: 0.08
Nodes (28): IInterModuleRequestClient, CancellationToken, Task, VerifyEmailOtpRequest, VerifyEmailOtpResponse, CancellationToken, Task, EmailNormalization (+20 more)

### Community 116 - ".WriteProblemAsync"
Cohesion: 0.12
Nodes (14): IAllowAnonymous, IConfigureNamedOptions, ProblemDetailsContext, ProblemDetailsServiceExtensions, IProblemDetailsService, Task, HttpContext, HttpStatusCode (+6 more)

### Community 117 - "Response"
Cohesion: 0.10
Nodes (19): ReservationStatus, Active, Committed, Expired, Released, RequiresReconciliation, CancellationToken, RouteGroupBuilder (+11 more)

### Community 118 - "IModule"
Cohesion: 0.08
Nodes (22): OpenTelemetryBuilder, ResourceBuilder, ICoreModule, IModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies (+14 more)

### Community 119 - "ProductsModule"
Cohesion: 0.12
Nodes (13): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, ProductsModule (+5 more)

### Community 120 - ".HandleWarehouseWebhookAsync"
Cohesion: 0.13
Nodes (15): Inventory.Endpoints.StockReservations.v1.WebhookCallback, IHeaderDictionary, IValidator, CancellationToken, HttpContext, IOptions, JsonSerializerOptions, RouteGroupBuilder (+7 more)

### Community 121 - ".GetVariantAsync"
Cohesion: 0.33
Nodes (4): IVariantFeatureManager, IVariantFeatureManagerExtensions, CancellationToken, Task

### Community 122 - "TokenRefreshRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenRefreshRateLimitingPolicy (+1 more)

### Community 123 - "FullTextSearchOptions"
Cohesion: 0.25
Nodes (8): FullTextSearchOptions, CultureToConfig, DefaultConfig, RankWeights, UseUnaccent, FullTextSearchOptionsValidator, Dictionary, IReadOnlyList

### Community 124 - ".AddCustomHealthChecks"
Cohesion: 0.12
Nodes (14): HealthCheckOptions, EnableHealthChecks, ReadinessTimeoutInSeconds, StartupTimeoutInSeconds, HealthCheckOptionsValidator, IApplicationBuilder, IConfiguration, IConnectionMultiplexer (+6 more)

### Community 125 - ".TapWhenFeatureEnabledAsync"
Cohesion: 0.33
Nodes (5): FeatureFlagResultExtensions, Action, Func, IFeatureManager, Task

### Community 126 - "GlobalExceptionHandlingMiddleware"
Cohesion: 0.19
Nodes (11): IApplicationBuilder, IServiceCollection, Exception, HttpContext, ILogger, IProblemDetailsService, IResxLocalizer, LoggerMessage (+3 more)

### Community 127 - "BaseDbContext"
Cohesion: 0.22
Nodes (8): BaseDbContext, AuditLog, CancellationToken, DbContextOptions, DbSet, ILogger, Task, TimeProvider

### Community 128 - "EmailRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, EmailRateLimitingPolicy (+1 more)

### Community 129 - "MassTransitInterModuleRequestClient"
Cohesion: 0.22
Nodes (8): IClientFactory, InterModuleRequestOptions, TimeoutSeconds, InterModuleRequestOptionsValidator, MassTransitInterModuleRequestClient, CancellationToken, IOptions, Task

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.48
Nodes (7): Observability (OpenTelemetry), docker-compose.yml (Base Stack), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), Postgres Service (mm.postgres), RabbitMQ Service (mm.rabbitmq), Redis Service (mm.redis)

### Community 131 - ".AddInfrastructure"
Cohesion: 0.29
Nodes (7): HostOptions, JsonOptions, Assembly, IConfiguration, IResxLocalizer, IServiceCollection, IWebHostEnvironment

### Community 132 - "OtpVerifyRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, OtpVerifyRateLimitingPolicy (+1 more)

### Community 134 - "SendRequestBody"
Cohesion: 0.25
Nodes (8): SendContact, IReadOnlyList, SendRequestBody, HtmlContent, Sender, Subject, TextContent, To

### Community 135 - "Response"
Cohesion: 0.18
Nodes (10): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, DateTimeOffset, Response, ClientId, DeviceName, Id, IpAddress, IsCurrent (+2 more)

### Community 136 - ".SingleAsResult"
Cohesion: 0.18
Nodes (10): CollectionExtensions, Func, ICollection, IEnumerable, PersistenceQueryableExtensions, CancellationToken, Expression, Func (+2 more)

### Community 137 - ".WriteTooManyRequestsToResponse"
Cohesion: 0.13
Nodes (14): PartitionedRateLimiter, CancellationToken, Func, HttpContext, IConfiguration, IProblemDetailsService, IReadOnlyList, IResxLocalizer (+6 more)

### Community 138 - ".InvokeAsync"
Cohesion: 0.07
Nodes (27): Common.Application.EndpointFilters, IEndpointFilter, IFeatureManagerSnapshot, ProblemDetails, ResxLocalizer, ResultToCreatedResponseTransformer, ResultToResponseTransformer, EndpointFilterDelegate (+19 more)

### Community 139 - "ProductTemplate"
Cohesion: 0.15
Nodes (11): IReadOnlyList, List, ProductTemplate, Brand, Color, IsActive, Model, Products (+3 more)

### Community 140 - "Response"
Cohesion: 0.13
Nodes (12): IAM.Endpoints.Users.VersionNeutral.SelfRegister, IAM.Endpoints.Users.VersionNeutral, RouteGroupBuilder, Endpoint, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt (+4 more)

### Community 141 - ".GetMeAsync"
Cohesion: 0.19
Nodes (9): CancellationToken, IReadOnlyList, Task, IKeycloakPermissionClient, IReadOnlyList, GrantedPermission, CancellationToken, HttpContext (+1 more)

### Community 142 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 143 - "SeedingCompletionTracker"
Cohesion: 0.17
Nodes (7): SeedingCompletionTracker, CancellationToken, Exception, Task, IOptions, IServiceCollection, TaskCompletionSource

### Community 144 - "Response"
Cohesion: 0.12
Nodes (16): CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate (+8 more)

### Community 145 - "EnrichLogsWithUserInfoMiddleware"
Cohesion: 0.10
Nodes (16): IAuthenticationSchemeProvider, IMiddleware, SecurityHeadersOptions, Headers, SecurityHeadersOptionsValidator, Dictionary, IApplicationBuilder, HttpContext (+8 more)

### Community 146 - "Response"
Cohesion: 0.20
Nodes (8): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, RouteGroupBuilder, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 147 - "IRecurringBackgroundJobs"
Cohesion: 0.13
Nodes (13): RecurringJobOptions, IRecurringBackgroundJobs, Action, Expression, Func, Task, RecurringBackgroundJobsService, Action (+5 more)

### Community 148 - "AuditLogRetentionJobRegistrar"
Cohesion: 0.31
Nodes (7): IHostedService, AuditLogRetentionJobRegistrar, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task

### Community 149 - "OtpService"
Cohesion: 0.18
Nodes (7): OtpCodeGenerator, IFusionCache, IOptions, OtpService, IConfiguration, IServiceCollection, Setup

### Community 150 - "StockReservationExpirySweepJobRegistrar"
Cohesion: 0.31
Nodes (7): CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task, StockReservationExpirySweepJobRegistrar

### Community 151 - "OutboxOptions"
Cohesion: 0.11
Nodes (20): OutboxCleanupSettings, BatchSize, CronSchedule, Enabled, RetentionDays, OutboxCleanupSettingsValidator, OutboxOptions, BaseBackoffSeconds (+12 more)

### Community 152 - ".EnsureNoMigrationsPending"
Cohesion: 0.44
Nodes (4): MigrationGuard, ILogger, IServiceProvider, LoggerMessage

### Community 153 - "SwaggerDefaultValues"
Cohesion: 0.33
Nodes (4): JsonValue, OpenApiOperation, OperationFilterContext, SwaggerDefaultValues

### Community 154 - "CustomValidator"
Cohesion: 0.05
Nodes (48): AbstractValidator, Inventory.Endpoints.StockReservations.v1.Commit, Products.Endpoints.Products.v1.My.Update, Products.Endpoints.Products.v1.Update, Inventory.Endpoints.StockReservations.v1.Reserve, CustomValidator, RequestBody, Request (+40 more)

### Community 155 - "EmailMessage"
Cohesion: 0.32
Nodes (6): EmailMessage, CancellationToken, ILogger, LoggerMessage, Task, DummyEmailGateway

### Community 156 - "Request"
Cohesion: 0.15
Nodes (13): Guid, Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FirstName (+5 more)

### Community 157 - "Request"
Cohesion: 0.22
Nodes (8): Products.Endpoints.ProductTemplates.v1.Search, Constants, Request, Brand, Color, Model, SearchTerm, RequestValidator

### Community 158 - "EventDispatcher"
Cohesion: 0.31
Nodes (7): EventDispatcher, ActivitySource, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task

### Community 159 - "IInventoryDbContext"
Cohesion: 0.13
Nodes (15): DbSet, IInventoryDbContext, StockLevels, StockReservations, DbContextOptions, DbSet, ILogger, TimeProvider (+7 more)

### Community 160 - "OtpServiceBase"
Cohesion: 0.21
Nodes (8): OtpCacheEntry, DateTimeOffset, CancellationToken, IFusionCache, SemaphoreSlim, Task, TimeSpan, OtpServiceBase

### Community 161 - ".ReserveSeriesAsync"
Cohesion: 0.16
Nodes (11): CancellationToken, IOptions, List, RequestBody, RouteGroupBuilder, Task, TimeProvider, Endpoint (+3 more)

### Community 162 - "HttpWarehouseGateway"
Cohesion: 0.31
Nodes (8): ReleaseResponseBody, CancellationToken, HttpClient, HttpResponseMessage, Task, HttpWarehouseGateway, ReleaseRequestBody, ReleaseResponseBody

### Community 164 - "Request"
Cohesion: 0.20
Nodes (10): Guid, Request, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken, Password (+2 more)

### Community 165 - "CurrentUser"
Cohesion: 0.12
Nodes (14): Common.Infrastructure.Auth.Services, Common.Infrastructure.Auth, CurrentUser, Id, IdAsString, IsAuthenticated, Principal, Roles (+6 more)

### Community 166 - "IamModule"
Cohesion: 0.05
Nodes (33): IAM.Endpoints.Tokens.VersionNeutral, IdentityScheme, Email, PhoneNumber, IdentitySchemeOptions, Scheme, IdentitySchemeOptionsValidator, Action (+25 more)

### Community 167 - "PaginationResponse"
Cohesion: 0.10
Nodes (17): PaginationResponse, HasNext, HasPrevious, NextPageNumber, PreviousPageNumber, TotalPages, ICollection, PaginationQueryableExtensions (+9 more)

### Community 168 - "Endpoint"
Cohesion: 0.29
Nodes (5): Products.Endpoints.ProductTemplates, RouteGroupBuilder, Setup, RouteGroupBuilder, Endpoint

### Community 169 - "CachedCaptchaService"
Cohesion: 0.25
Nodes (5): CancellationToken, IFusionCache, IOptions, Task, CachedCaptchaService

### Community 171 - "IOtpService"
Cohesion: 0.24
Nodes (8): CancellationToken, Task, TimeSpan, IOtpService, OtpVerificationOutcome, InvalidOtp, Success, TooManyAttempts

### Community 172 - "ResxLocalizationOptions"
Cohesion: 0.25
Nodes (7): ResxLocalizationOptions, DefaultCulture, SupportedCultures, ResxLocalizationOptionsValidator, ICollection, IApplicationBuilder, IOptions

### Community 173 - "RequestBody"
Cohesion: 0.29
Nodes (7): ProductTemplateId, RequestBody, Description, Name, Price, ProductTemplateId, Quantity

### Community 174 - ".AddPushServices"
Cohesion: 0.25
Nodes (6): CancellationToken, Task, IPushGateway, IConfiguration, IServiceCollection, Setup

### Community 175 - "AuditLogDto"
Cohesion: 0.09
Nodes (19): Products.Endpoints.Stores.v1.AuditLog, JsonElement, AuditLogDto, DateTimeOffset, CancellationToken, RouteGroupBuilder, Task, Endpoint (+11 more)

### Community 176 - ".FixedWindow"
Cohesion: 0.20
Nodes (7): RateLimitPartitions, HttpContext, IConnectionMultiplexer, ILoggerFactory, RateLimitPartition, HttpContext, RateLimitPartition

### Community 177 - ".AddProductToMyStoreAsync"
Cohesion: 0.22
Nodes (8): CancellationToken, ProductTemplate, RouteGroupBuilder, Store, Task, Endpoint, Response, Id

### Community 178 - "DummyPushGateway"
Cohesion: 0.38
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, DummyPushGateway

### Community 179 - "CachingOptions"
Cohesion: 0.11
Nodes (21): CachingEntryDefaults, Duration, FactoryHardTimeout, FactorySoftTimeout, FailSafeMaxDuration, FailSafeThrottleDuration, CachingOptions, AllowInMemoryOnlyInProduction (+13 more)

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

### Community 185 - "Common.Domain.Devices"
Cohesion: 0.29
Nodes (6): IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, Common.Domain.Devices, DeviceSessionConstants, Request, Id, RequestValidator

### Community 186 - ".Get"
Cohesion: 0.50
Nodes (3): Action, IEnumerable, RateLimiterOptions

### Community 187 - "ReverseProxyOptions"
Cohesion: 0.18
Nodes (9): ForwardedHeadersOptions, ReverseProxyOptions, ForwardLimit, IsEnabled, TrustedNetworks, ReverseProxyOptionsValidator, IReadOnlyList, IConfiguration (+1 more)

### Community 188 - "BoundedCaptureStream"
Cohesion: 0.14
Nodes (8): HttpResponse, SeekOrigin, BoundedCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 190 - "ServiceAccountTokenCache"
Cohesion: 0.11
Nodes (11): KeycloakPaths, CancellationToken, DateTimeOffset, SemaphoreSlim, Task, TimeSpan, ServiceAccountTokenCache, AccessToken (+3 more)

### Community 191 - "v1/RemoveProduct/Request.cs"
Cohesion: 0.29
Nodes (7): Products.Endpoints.Stores.v1.RemoveProduct, ProductId, StoreId, Request, Id, ProductId, RequestValidator

### Community 192 - "IssueVerificationTokenRequestHandler"
Cohesion: 0.39
Nodes (6): IssueVerificationTokenRequest, IssueVerificationTokenResponse, CancellationToken, IOptions, Task, IssueVerificationTokenRequestHandler

### Community 193 - ".SendCoreAsync"
Cohesion: 0.36
Nodes (5): IReadOnlyDictionary, IReadOnlyList, PushMessage, CancellationToken, Task

### Community 194 - "CorsOptions"
Cohesion: 0.25
Nodes (8): CorsOptions, AllowCredentials, AllowedHeaders, AllowedMethods, AllowedOrigins, MaxAgeInSeconds, CorsOptionsValidator, IReadOnlyList

### Community 195 - "InventoryOptions"
Cohesion: 0.17
Nodes (12): InventoryOptions, MaxOccurrencesPerSeries, SafetyBufferQuantity, StockReservationExpirySweepBatchSize, StockReservationExpirySweepCron, WarehouseGatewayBaseUrl, WarehouseGatewayProvider, WebhookSharedSecret (+4 more)

### Community 196 - "TokenCreateRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenCreateRateLimitingPolicy (+1 more)

### Community 197 - "LogProductCatalogChangeHandler"
Cohesion: 0.24
Nodes (8): CancellationToken, DefaultIdType, IFusionCache, ILogger, IOptions, LoggerMessage, Task, LogProductCatalogChangeHandler

### Community 198 - "InventoryModule"
Cohesion: 0.15
Nodes (10): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, InventoryModule, ActivitySourceNames, MeterNames (+2 more)

### Community 199 - "OpenApiOptions"
Cohesion: 0.22
Nodes (9): OpenApiOptions, ContactEmail, ContactName, Description, EnableSwagger, LicenseName, LicenseUrl, Title (+1 more)

### Community 200 - "KeyValuePair"
Cohesion: 0.17
Nodes (7): KeyValuePair, IEnumerable, ActivitySource, Counter, Meter, NotificationsTelemetry, UpDownCounter

### Community 201 - "KeycloakScopes"
Cohesion: 0.20
Nodes (9): Devices, Hangfire, KeycloakScopes, Products, ProductTemplates, Sessions, StockReservations, Stores (+1 more)

### Community 202 - "Request"
Cohesion: 0.18
Nodes (10): IAM.Endpoints.Tokens.VersionNeutral.Create, Guid, Request, ClientId, DeviceId, DeviceName, Otp, PhoneNumber (+2 more)

### Community 203 - "SmsRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, SmsRateLimitingPolicy (+1 more)

### Community 205 - "UtcDateTimeOffsetConverter"
Cohesion: 0.22
Nodes (6): DateTimeOffset, ModelConfigurationBuilder, UtcDateTimeOffsetConverter, DateTimeOffset, DateTimeOffset, ModelConfigurationBuilder

### Community 210 - "ConfigureSwaggerOptions"
Cohesion: 0.28
Nodes (7): ApiVersionDescription, IApiVersionDescriptionProvider, IConfigureOptions, OpenApiInfo, IOptions, SwaggerGenOptions, ConfigureSwaggerOptions

### Community 211 - "Common.Application.BackgroundJobs"
Cohesion: 0.36
Nodes (3): Common.Application.BackgroundJobs, BackgroundJobs.Telemetry, BackgroundJobs

### Community 212 - "Setup"
Cohesion: 0.29
Nodes (4): ApiVersionSet, Setup, IEndpointRouteBuilder, IServiceCollection

### Community 213 - "ProductsTelemetry"
Cohesion: 0.40
Nodes (4): ActivitySource, Counter, Meter, ProductsTelemetry

### Community 214 - "IAMModule.cs"
Cohesion: 0.16
Nodes (7): IAM.Domain.Captcha, IAM.Endpoints, IAM.Endpoints.Otp.VersionNeutral, IAM.Infrastructure.Captcha.Services, IAM.Application.Captcha.Services, IAM.Infrastructure.Captcha, IAssemblyReference

### Community 215 - "Request"
Cohesion: 0.22
Nodes (8): Products.Endpoints.ProductTemplates.v1.Create, Request, Brand, Color, Model, RequestValidator, Response, Id

### Community 216 - "Common.Application.Validation"
Cohesion: 0.06
Nodes (31): Products.Endpoints.ProductTemplates.v1.Deactivate, Products.Endpoints.Probe.v1, Inventory.Endpoints.StockReservations.v1.Release, Common.Application.ModelBinders, Common.Application.Validation, Products.Endpoints.ProductTemplates.v1.Activate, IModelBinder, ModelBindingContext (+23 more)

### Community 217 - "FeatureFlags"
Cohesion: 0.33
Nodes (5): Checkout, FeatureFlags, IAM, Notifications, Products

### Community 218 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 219 - "IAM.Endpoints.Common.Validations"
Cohesion: 0.12
Nodes (13): IAM.Endpoints.Otp.VersionNeutral.SendForLogin, IAM.Endpoints.Common.Validations, IResxLocalizer, IRuleBuilder, IRuleBuilderOptions, CommonValidations, IResxLocalizer, IRuleBuilder (+5 more)

### Community 220 - "Seeder"
Cohesion: 0.14
Nodes (13): Products.Infrastructure.Persistence.Seeding, CancellationToken, ILogger, LoggerMessage, ProductsDbContext, Task, CancellationToken, List (+5 more)

### Community 221 - "Keycloak realm as code"
Cohesion: 0.25
Nodes (7): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Secrets, Seed users (dev / test only)

### Community 222 - ".RemoveProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 223 - "GetActiveSessionIdsRequest"
Cohesion: 0.44
Nodes (7): GetActiveSessionIdsRequest, GetActiveSessionIdsResponse, UserSessionIds, IReadOnlyList, CancellationToken, Task, GetActiveSessionIdsRequestHandler

### Community 224 - "AuditLogRetentionService"
Cohesion: 0.29
Nodes (8): AuditLogRetentionService, CancellationToken, DateTimeOffset, ILogger, IOptions, LoggerMessage, NpgsqlDataSource, Task

### Community 225 - "BackgroundJobsOptions"
Cohesion: 0.29
Nodes (7): BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount, BackgroundJobsOptionsValidator

### Community 226 - ".HandleAsync"
Cohesion: 0.18
Nodes (11): GetSeedUserIdsRequest, GetSeedUserIdsResponse, ICollection, CancellationToken, Task, GetSeedUserIdsRequestHandler, CancellationToken, IResult (+3 more)

### Community 227 - "BackgroundJobsModule"
Cohesion: 0.25
Nodes (7): BackgroundJobsModule, ActivitySourceNames, MeterNames, Name, StartupPriority, IEndpointRouteBuilder, IEnumerable

### Community 228 - ".ListSessions"
Cohesion: 0.19
Nodes (12): DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, IReadOnlyList, CancellationToken, IReadOnlyCollection, RouteGroupBuilder, Task (+4 more)

### Community 229 - ".AddCommonOptions"
Cohesion: 0.20
Nodes (6): Setup, IConfiguration, IHostEnvironment, IServiceCollection, ValidationContextExtensions, ValidationContext

### Community 230 - ".ReserveStockAsync"
Cohesion: 0.25
Nodes (6): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Id

### Community 231 - "Request"
Cohesion: 0.33
Nodes (6): Products.Endpoints.Stores.v1.My.Update, Request, Address, Description, Name, RequestValidator

### Community 232 - "Common.Application.Options"
Cohesion: 0.09
Nodes (14): Common.Application.Search, Notifications.Infrastructure.Email, Common.Application.Caching, Common.Infrastructure.RateLimiting, Notifications.Infrastructure.Email.Brevo, Products.Infrastructure.RateLimiting, Common.Infrastructure.Extensions, Common.Application.Options (+6 more)

### Community 233 - "ModulesOptions"
Cohesion: 0.50
Nodes (4): ModulesOptions, EnabledModules, ModulesOptionsValidator, IReadOnlyList

### Community 234 - "Setup"
Cohesion: 0.29
Nodes (4): IApplicationBuilder, IWebHostEnvironment, Type, Setup

### Community 235 - "AuditLogEntry"
Cohesion: 0.29
Nodes (7): AuditLogEntry, AggregateId, AggregateType, Event, EventType, Version, DefaultIdType

### Community 236 - ".UpdateCurrentPushToken"
Cohesion: 0.18
Nodes (9): Notifications.Infrastructure.Devices.UpdateCurrentPushToken, CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint, Request, PushToken (+1 more)

### Community 237 - "ReCaptchaResponse"
Cohesion: 0.25
Nodes (7): DateTime, ReCaptchaResponse, ChallengeTs, ErrorCodes, Hostname, Score, Success

### Community 238 - "InterModuleRequestHandler"
Cohesion: 0.12
Nodes (16): IInterModuleRequestHandler, CancellationToken, Task, InterModuleRequestHandler, CancellationToken, ConsumeContext, Task, SecurityAlertType (+8 more)

### Community 239 - ".TryReadFromJsonAsync"
Cohesion: 0.33
Nodes (4): HttpContent, CancellationToken, Task, HttpContentJsonExtensions

### Community 240 - "RedisOtpService"
Cohesion: 0.32
Nodes (6): CancellationToken, IConnectionMultiplexer, IOptions, Task, TimeSpan, RedisOtpService

### Community 241 - "RegisterRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, RegisterRateLimitingPolicy (+1 more)

### Community 242 - ".AddCustomSwagger"
Cohesion: 0.20
Nodes (7): IOpenApiSchema, ISchemaFilter, SchemaFilterContext, IConfigureOptions, IServiceCollection, SwaggerGenOptions, StronglyTypedIdSchemaFilter

### Community 243 - "FixedWindow"
Cohesion: 0.29
Nodes (7): CustomRateLimitingOptionsValidator, FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit, FixedWindowValidator

### Community 244 - ".AddAuthInfrastructure"
Cohesion: 0.25
Nodes (6): IAuthorizationHandler, IAuthorizationPolicyProvider, IConfigureOptions, IServiceCollection, JwtBearerOptions, Setup

### Community 245 - "Host.Swagger"
Cohesion: 0.22
Nodes (5): Host.Swagger, IOperationFilter, OpenApiOperation, OperationFilterContext, RemoveDefaultResponseSchemaFilter

### Community 246 - "KeycloakPermission"
Cohesion: 0.25
Nodes (3): KeycloakPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder

### Community 247 - "DeviceRegistryReconciliationService"
Cohesion: 0.32
Nodes (6): CancellationToken, ILogger, IOptions, LoggerMessage, Task, DeviceRegistryReconciliationService

### Community 248 - "SendErrorBody"
Cohesion: 0.67
Nodes (3): SendErrorBody, Code, Message

### Community 249 - "Common.Application.EventBus"
Cohesion: 0.15
Nodes (10): Inventory.Application.IntegrationEventHandlers, Common.IntegrationEvents, Products.Application.Products.DomainEventHandlers.v1, Products.Application.Stores.DomainEventHandlers.v1, Common.Application.EventBus, IIntegrationEventOutbox, Setup, IServiceCollection (+2 more)

### Community 250 - "FirebaseServiceAccountOptions"
Cohesion: 0.29
Nodes (7): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri

### Community 251 - "StoreId"
Cohesion: 0.14
Nodes (12): Products.Endpoints.Stores.v1.Deactivate, ProductId, StoreId, V1ProductCreatedDomainEvent, DefaultIdType, StoreId, Request, Id (+4 more)

### Community 252 - ".GetAuditLogAsync"
Cohesion: 0.38
Nodes (5): DbContextExtensions, CancellationToken, DbSet, JsonSerializerOptions, Task

### Community 260 - ".MapEndpoint"
Cohesion: 0.29
Nodes (4): Products.Endpoints.Products, RouteGroupBuilder, Setup, RouteGroupBuilder

### Community 261 - "Common.Infrastructure.Persistence.AuditLog"
Cohesion: 0.29
Nodes (3): Common.Infrastructure.Persistence.AuditLog, Setup, IServiceCollection

### Community 262 - ".AddCommonCaching"
Cohesion: 0.29
Nodes (5): Common.Infrastructure.Caching, Setup, IConfiguration, IConnectionMultiplexer, IServiceCollection

### Community 263 - ".AddServices"
Cohesion: 0.33
Nodes (5): DatabaseOptions, ConnectionString, DatabaseOptionsValidator, IConfiguration, IServiceCollection

### Community 264 - "DevicesOptions"
Cohesion: 0.33
Nodes (6): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator, IReadOnlyCollection

### Community 265 - ".UpdateMyStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 267 - "VersionNeutral/Get/Request.cs"
Cohesion: 0.40
Nodes (4): IAM.Endpoints.Users.VersionNeutral.Get, Request, Id, RequestValidator

### Community 268 - "StringExtensions"
Cohesion: 0.33
Nodes (3): Common.Domain.Extensions, SearchValues, StringExtensions

### Community 269 - "SendForRegistration/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, Request, CaptchaToken, PhoneNumber, RequestValidator

### Community 270 - "DefaultResponsesOperationFilter"
Cohesion: 0.70
Nodes (3): OpenApiOperation, OperationFilterContext, DefaultResponsesOperationFilter

### Community 271 - ".AddDeviceRegistryReconciliation"
Cohesion: 0.33
Nodes (3): IServiceCollection, RouteGroupBuilder, Setup

### Community 272 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.10
Nodes (16): Notifications.Application.Otp, Common.Application.FeatureManagement, IAM.Endpoints.Otp, Notifications.Application.Persistence, Notifications.Infrastructure.Telemetry, Common.InterModuleRequests.Contracts, Notifications.Infrastructure.InterModuleRequestHandlers, IAM.Endpoints.Tokens.VersionNeutral.Revoke (+8 more)

### Community 273 - "Response"
Cohesion: 0.33
Nodes (6): Response, AvailableQuantity, Description, Name, Price, Quantity

### Community 274 - "GetProductRequest"
Cohesion: 0.27
Nodes (8): Products.Infrastructure.InterModuleRequestHandlers, Common.InterModuleRequests.Products, GetProductRequest, GetProductResponse, DefaultIdType, CancellationToken, Task, GetProductRequestHandler

### Community 275 - "Request"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForEmail, Request, CaptchaToken, Email, RequestValidator

### Community 276 - "Inventory.Endpoints.StockReservations"
Cohesion: 0.40
Nodes (3): Inventory.Endpoints.StockReservations, RouteGroupBuilder, Setup

### Community 277 - "SendResponseBody"
Cohesion: 0.50
Nodes (4): SendResponseBody, Code, Description, JobId

### Community 278 - "ProductId"
Cohesion: 0.10
Nodes (13): Products.Endpoints.Stores.v1.My.RemoveProduct, StronglyTypedIdHelper, DefaultIdType, ProductId, Request, Id, RequestValidator, Request (+5 more)

### Community 279 - ".SaveChangesAsync"
Cohesion: 0.08
Nodes (17): CancellationToken, Task, ProductTemplate, CancellationToken, RouteGroupBuilder, Task, Endpoint, CancellationToken (+9 more)

### Community 280 - "InventoryTelemetry"
Cohesion: 0.40
Nodes (4): ActivitySource, Counter, Meter, InventoryTelemetry

### Community 281 - ".CommitStockReservationAsync"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 282 - "Infrastructure/Setup.cs"
Cohesion: 0.10
Nodes (10): Common.Infrastructure.Modules, Common.InterModuleRequests, Common.Infrastructure.Localization, Host.Middlewares, Host.Infrastructure, OtlpExportProtocol, IAssemblyReference, Setup (+2 more)

### Community 283 - ".RequireOtpTemplateForDefaultCulture"
Cohesion: 0.40
Nodes (4): Func, IConfiguration, IEnumerable, OtpTemplateConfiguration

### Community 284 - "RequestBody"
Cohesion: 0.33
Nodes (6): DateTimeOffset, DefaultIdType, RequestBody, ProductId, Quantity, ReservationDeadline

### Community 285 - "ProductsModule.cs"
Cohesion: 0.20
Nodes (6): Common.Endpoints.Versioning, Products.Endpoints.Probe, Products.Endpoints, IAssemblyReference, RouteGroupBuilder, Setup

### Community 286 - ".ActivateProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 288 - "ICaptchaService"
Cohesion: 0.36
Nodes (5): ICaptchaService, DummyCaptchaService, IConfiguration, IServiceCollection, Setup

### Community 289 - "Setup"
Cohesion: 0.11
Nodes (16): LoadAll, ModuleRegistry, Names, Type, Setup, Assembly, Exception, IApplicationBuilder (+8 more)

### Community 290 - ".RemoveMyProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 291 - "My/Create/Request.cs"
Cohesion: 0.50
Nodes (3): Products.Endpoints.Stores.v1.My.Create, Request, RequestValidator

### Community 294 - ".AddProductAsync"
Cohesion: 0.22
Nodes (8): CancellationToken, ProductTemplate, RouteGroupBuilder, Store, Task, Endpoint, Response, Id

### Community 297 - "SmsMessage"
Cohesion: 0.18
Nodes (10): SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, SmsMessage, CancellationToken, ILogger, LoggerMessage (+2 more)

### Community 298 - "AuditLogOptions"
Cohesion: 0.50
Nodes (4): AuditLogOptions, PurgeBatchSize, RetentionDays, AuditLogOptionsValidator

### Community 299 - "Request"
Cohesion: 0.20
Nodes (9): Products.Endpoints.Stores.v1.Create, Request, Address, Description, Name, OwnerId, RequestValidator, Response (+1 more)

### Community 303 - ".UpdateStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

## Knowledge Gaps
- **896 isolated node(s):** `Id`, `IdAsString`, `Roles`, `SessionId`, `JwtClaimNames` (+891 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 2026 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **87 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `IAM.Application.Keycloak`, `MassTransitInterModuleRequestClient`, `NotificationPayload`, `Common.Infrastructure.Persistence.AuditLog`, `.AddCommonCaching`, `.AddServices`, `DevicesOptions`, `NotificationsModule.cs`, `Common.Domain.ResultMonad`, `Response`, `EmailOptions`, `EnrichLogsWithUserInfoMiddleware`, `Common.InterModuleRequests.Contracts`, `OutboxOptions`, `Infrastructure/Setup.cs`, `RequestResponseBodyLoggingMiddleware`, `Request`, `ProductsModule.cs`, `Common.Infrastructure.Persistence`, `Request`, `IamModule`, `ObservabilityOptions`, `Request`, `AuditLogOptions`, `KeycloakOptions`, `ResxLocalizationOptions`, `Products.Domain.Stores`, `.FixedWindow`, `CachingOptions`, `SmsOptions`, `RabbitMqOptions`, `Inventory.Domain.StockReservations`, `ReverseProxyOptions`, `RequestLoggingOptions`, `CorsOptions`, `InventoryOptions`, `OpenApiOptions`, `Request`, `CaptchaOptions`, `Common.Application.BackgroundJobs`, `IAMModule.cs`, `PushOptions`, `Program.cs`, `OtpOptions`, `BackgroundJobsOptions`, `ResiliencyOptions`, `.AddCommonOptions`, `ModulesOptions`, `OutboxModule.cs`, `FixedWindow`, `Host.Swagger`, `IModule`, `Common.Application.EventBus`, `FullTextSearchOptions`, `.AddCustomHealthChecks`?**
  _High betweenness centrality (0.276) - this node is a cross-community bridge._
- **Why does `Result` connect `Result` to `ApplicationUserId`, `.SingleAsResult`, `.UpdateMyStoreAsync`, `Error`, `ProductTemplate`, `.SendOtp`, `.GetMeAsync`, `Response`, `Response`, `DomainEvent`, `.AddBrevo`, `ICurrentUser`, `.CommitStockReservationAsync`, `NetGsmSmsGateway`, `EmailMessage`, `KeycloakTokenClient`, `.SaveChangesAsync`, `.ActivateProductTemplateAsync`, `.ReserveSeriesAsync`, `HttpWarehouseGateway`, `.RemoveMyProductAsync`, `StockReservation`, `.RefreshToken`, `PaginationResponse`, `.AssignBasicRoleOrRollbackAsync`, `CachedCaptchaService`, `SmsMessage`, `.AddProductAsync`, `.RegisterAsync`, `.AddPushServices`, `AuditLogDto`, `.UpdateStoreAsync`, `.AddProductToMyStoreAsync`, `DummyPushGateway`, `ReCaptchaService`, `BrevoEmailGateway`, `.SearchProductTemplatesAsync`, `.SendCoreAsync`, `.SendCoreAsync`, `AuditableEntityResponse`, `Response`, `IProductsDbContext`, `.SearchStoresAsync`, `Inventory.Domain.StockReservations.DomainEvents.v1`, `ResultTelemetryExtensions`, `.GetProductAsync`, `Response`, `.GetClientKey`, `.RemoveProductAsync`, `.SearchMyProductsAsync`, `Request`, `.AddNetGsm`, `.ListSessions`, `.ReserveStockAsync`, `.ReleaseStockReservationAsync`, `.UpdateCurrentPushToken`, `.IsRegisteredAsync`, `.RegisterAsync`, `Response`, `.HandleWarehouseWebhookAsync`, `.GetAuditLogAsync`, `.TapWhenFeatureEnabledAsync`?**
  _High betweenness centrality (0.172) - this node is a cross-community bridge._
- **Why does `ApplicationUserId` connect `ApplicationUserId` to `NotificationPayload`, `VersionNeutral/Get/Request.cs`, `Response`, `KeycloakPermissionAuthorizationHandler`, `Response`, `ProductId`, `DeviceRegistration`, `ICurrentUser`, `KeycloakTokenClient`, `For`, `CurrentUser`, `.AssignBasicRoleOrRollbackAsync`, `Request`, `.RegisterAsync`, `AuditLogDto`, `AuditableEntity`, `Response`, `AuditableEntityResponse`, `IStronglyTypedId`, `IProductsDbContext`, `CreateStockLevelOnProductCreatedHandler`, `.SearchStoresAsync`, `.Configure`, `Store`, `Response`, `Common.Domain.StronglyTypedIds`, `Seeder`, `GetActiveSessionIdsRequest`, `.HandleAsync`, `Request`, `.ListSessions`, `InterModuleRequestHandler`, `V1StoreCreatedDomainEvent`, `Response`, `.RegisterAsync`?**
  _High betweenness centrality (0.069) - this node is a cross-community bridge._
- **What connects `Id`, `IdAsString`, `Roles` to the rest of the system?**
  _896 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `IAM.Application.Keycloak` be split into smaller, more focused modules?**
  _Cohesion score 0.10541310541310542 - nodes in this community are weakly interconnected._
- **Should `NotificationPayload` be split into smaller, more focused modules?**
  _Cohesion score 0.05888376856118792 - nodes in this community are weakly interconnected._
- **Should `ApplicationUserId` be split into smaller, more focused modules?**
  _Cohesion score 0.11092896174863388 - nodes in this community are weakly interconnected._