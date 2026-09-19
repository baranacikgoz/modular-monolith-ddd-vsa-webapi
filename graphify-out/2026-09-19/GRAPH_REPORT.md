# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-09-19)

## Corpus Check
- 544 files · ~80,048 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 4461 nodes · 8039 edges · 372 communities (278 shown, 89 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 274 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `371098d2`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- StockReservation
- NotificationPayload
- OutboxProcessor
- DeviceRegistryReconcileJobRegistrar
- Hybrid DDD (Writes) / VSA (Reads)
- KeycloakAdminClient
- RedisFixedWindowRateLimiter
- ApplyAuditingInterceptor
- FirebasePushGateway
- Inventory.Domain.StockReservations.DomainEvents.v1
- Error
- Common.Domain.ResultMonad
- IKeycloakAdminClient
- .SendAsync
- AdminRepresentations.cs
- KeycloakPermissionAuthorizationHandler
- EmailOptions
- HangfireCustomAuthorizationFilter
- AggregateRoot
- IntegrationEventHandlerBase
- IntegrationEvent
- BoundedRequestCaptureStream
- DeviceRegistration
- .AddBrevo
- DeactivateDeviceSessionsRequest
- NetGsmSmsGateway
- IEvent
- RequestResponseBodyLoggingMiddleware
- KeycloakTokenClient
- Result
- Product
- .VerifyOtp
- .SingleAsResult
- Products.Domain.Products.DomainEvents.v1
- KeycloakPermissionClient
- OutboxMessage
- CustomRateLimitingOptions
- DomainEvent
- .RefreshToken
- ObservabilityOptions
- IamTelemetry
- Request
- .SaveWithOutboxAsync
- CreateStoreRateLimitingPolicy
- KeycloakOptions
- .RegisterAsync
- Common.Application.DTOs
- Outbox Misuse Check
- .AddPersistence
- Add Integration Event Command
- Response
- .AddKeycloakInfrastructure
- ReCaptchaService
- Inventory.Domain.StockReservations
- .SendOtp
- BrevoEmailGateway
- CaptchaOptions
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
- Response
- ValueObject
- .HandleAsync
- PolymorphicEventConverter
- Full-Text Search
- AuditableEntityResponse
- .WriteAsync
- AsNoTracking Coverage Check
- IStronglyTypedId
- PaginationRequestValidator
- .SaveChangesAsync
- CheckRegistrationRateLimitingPolicy
- SeedingCompletionTracker
- Request
- .SearchStoresAsync
- UserRepresentation
- NotificationsDbContext
- .Configure
- ResultTelemetryExtensions
- Store
- PushOptions
- Configuration-Driven Module Registration
- Program.cs
- IInterModuleRequestClient
- Request
- Common.Domain.StronglyTypedIds
- .GetClientKey
- OtpOptions
- .SearchMyProductsAsync
- DatabaseSeederOrchestrator
- StockReservationExpirySweepService
- .SearchStoreProductsAsync
- .AddNetGsm
- OutboxDbContext
- ResiliencyOptions
- HttpContextTargetingContextAccessor
- BackgroundJobsTelemetry
- ProductId
- .RegisterAsync
- OutboxModule.cs
- .ReleaseStockReservationAsync
- StockLevel
- .IsRegisteredAsync
- KeycloakPermissionPolicyProvider
- BackgroundJobsService
- OutboxCleanupJob
- ProductsDbContext
- Response
- InterModuleRequestHandler
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
- V1StoreCreatedDomainEvent
- docker-compose.yml (Base Stack)
- .AddInfrastructure
- OtpVerifyRateLimitingPolicy
- Consumer Idempotency (IntegrationEventHandlerBase)
- SendRequestBody
- .ListSessions
- .AddProductAsync
- .WriteTooManyRequestsToResponse
- .From
- ProductTemplate
- Endpoint
- .GetMeAsync
- StoreId
- .AddModules
- Response
- EnrichLogsWithUserInfoMiddleware
- .CreateTokensByEmail
- IRecurringBackgroundJobs
- AuditLogRetentionService
- OtpService
- StockReservationExpirySweepJobRegistrar
- OutboxOptions
- .EnsureNoMigrationsPending
- SwaggerDefaultValues
- .SendCoreAsync
- EmailMessage
- Request
- StrictDateTimeOffsetJsonConverter
- EventDispatcher
- IInventoryDbContext
- OtpServiceBase
- .ReserveSeriesAsync
- StronglyTypedIdWriteOnlyJsonConverter
- ApplicationUserId
- Request
- CurrentUser
- IamModule
- PaginationRequest
- Request
- IInterModuleRequest
- Notifications.Application/IAssemblyReference.cs
- IOtpService
- ResxLocalizationOptions
- v1/AddProduct/Request.cs
- AuditLogRetentionJobRegistrar
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
- ServiceAccountTokenCache
- GetActiveSessionIdsRequest
- CachedCaptchaService
- HttpWarehouseGateway
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
- .AssignBasicRoleOrRollbackAsync
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- ConfigureSwaggerOptions
- Common.Application.BackgroundJobs
- Setup
- ProductsTelemetry
- Common.Application.FeatureManagement
- .CreateMyStoreAsync
- Common.Application.ModelBinders
- FeatureFlags
- Response
- SendForLogin/Request.cs
- DeviceRegistryReconciliationService
- Keycloak realm as code
- .CreateTokens
- Seeder
- Endpoint
- BackgroundJobsOptions
- .MapCode
- BackgroundJobsModule
- Request
- .AddCommonOptions
- .ReserveStockAsync
- Request
- Common.Application.Options
- Common.Application.Validation
- Setup
- Request
- StronglyTypedIdListReadOnlyJsonConverter
- ICaptchaService
- Common.Infrastructure.Persistence
- .TryReadFromJsonAsync
- v1/RemoveProduct/Request.cs
- RegisterRateLimitingPolicy
- .AddCustomSwagger
- CustomValidator
- ReCaptchaResponse
- Host.Swagger
- .AddAuthInfrastructure
- Common.InterModuleRequests
- SendErrorBody
- IntegrationEventOutbox
- KeycloakPermission
- .HandleAsync
- RedisOtpService
- Products.Domain/IAssemblyReference.cs
- IAM.Application/IAssemblyReference.cs
- Products.Infrastructure/IAssemblyReference.cs
- Products.Application/IAssemblyReference.cs
- Common.Infrastructure/IAssemblyReference.cs
- Notifications.Domain/IAssemblyReference.cs
- IAM.Infrastructure/IAssemblyReference.cs
- Response
- DevicesOptions
- .AddCommonCaching
- Response
- Common.Infrastructure.Persistence.AuditLog
- FirebaseServiceAccountOptions
- AuditLogOptions
- .SendOtp
- Request
- DefaultResponsesOperationFilter
- Common.Application.JsonConverters
- Common.InterModuleRequests.Contracts
- SendForRegistration/Request.cs
- GetProductRequest
- Request
- SendPhoneOtpRequestHandler
- SendResponseBody
- ReservationStatus
- Response
- InventoryTelemetry
- TokenResponseRepresentation
- Setup
- .RequireOtpTemplateForDefaultCulture
- Request
- Products.Endpoints.Probe
- .UpdateCurrentPushToken
- Inventory.Endpoints.StockReservations
- DummySmsGateway
- .AddDeviceRegistryReconciliation
- IProductsDbContext
- .ActivateProductTemplateAsync
- .UpdateStoreAsync
- .SeedProductAsync
- Infrastructure/StringExtensions.cs
- Request
- .SendAsync
- ModulesOptions
- Response
- SecurityHeadersOptions
- .LogRoleAssignmentFailed
- .MapOtpEndpoints
- V1StockReservationCommittedDomainEvent
- V1StockReservationReleaseAttemptStartedDomainEvent
- V1StockReservationReservedDomainEvent
- HttpContextExtensions
- .AddStockReservationExpirySweep
- Endpoint
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

## Communities (372 total, 89 thin omitted)

### Community 0 - "StockReservation"
Cohesion: 0.12
Nodes (15): DateTimeOffset, StockReservationId, V1StockReservationReleaseAttemptAbandonedDomainEvent, DateTimeOffset, DefaultIdType, StockReservation, LastReleaseAttemptReference, ProductId (+7 more)

### Community 1 - "NotificationPayload"
Cohesion: 0.05
Nodes (40): Notifications.Infrastructure.Hubs, Notifications.Application.Hubs, Hub, HubConnectionContext, IHubContext, IUserIdProvider, RedisOptions, CancellationToken (+32 more)

### Community 2 - "OutboxProcessor"
Cohesion: 0.22
Nodes (11): IPublishEndpoint, CancellationToken, Exception, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task (+3 more)

### Community 3 - "DeviceRegistryReconcileJobRegistrar"
Cohesion: 0.31
Nodes (7): CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task, DeviceRegistryReconcileJobRegistrar

### Community 5 - "KeycloakAdminClient"
Cohesion: 0.25
Nodes (13): HttpRequestMessage, CancellationToken, Error, Func, HttpClient, HttpResponseMessage, IFusionCache, IOptions (+5 more)

### Community 6 - "RedisFixedWindowRateLimiter"
Cohesion: 0.13
Nodes (16): RateLimiter, RateLimiterStatistics, RateLimitLease, FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration (+8 more)

### Community 7 - "ApplyAuditingInterceptor"
Cohesion: 0.09
Nodes (19): SaveChangesInterceptor, ISearchLocalized, Language, ApplyAuditingInterceptor, CancellationToken, DbContextEventData, InterceptionResult, TimeProvider (+11 more)

### Community 8 - "FirebasePushGateway"
Cohesion: 0.24
Nodes (8): FirebaseApp, FirebaseMessaging, IDisposable, Exception, ILogger, LoggerMessage, TimeSpan, FirebasePushGateway

### Community 9 - "Inventory.Domain.StockReservations.DomainEvents.v1"
Cohesion: 0.11
Nodes (13): Inventory.Domain.StockReservations.DomainEvents.v1, DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationCommitConflictDetectedDomainEvent, DateTimeOffset, DefaultIdType, StockReservationId (+5 more)

### Community 10 - "Error"
Cohesion: 0.07
Nodes (21): StringLocalizerExtensions, IStringLocalizer, Error, Key, ParameterName, StatusCode, SubErrors, Value (+13 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.11
Nodes (17): Common.Application.Search, Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions, Products.Endpoints.Products.v1.Search, Common.Application.Extensions, Products.Domain.Products, Products.Infrastructure.Telemetry, IAM.Infrastructure.Auth (+9 more)

### Community 12 - "IKeycloakAdminClient"
Cohesion: 0.18
Nodes (12): CancellationToken, IReadOnlyList, Task, IKeycloakAdminClient, DateOnly, DateTimeOffset, CreateKeycloakUser, KeycloakUser (+4 more)

### Community 13 - ".SendAsync"
Cohesion: 0.11
Nodes (14): IClientFactory, CancellationToken, Task, MassTransitInterModuleRequestClient, CancellationToken, IOptions, Task, CancellationToken (+6 more)

### Community 14 - "AdminRepresentations.cs"
Cohesion: 0.12
Nodes (16): CredentialRepresentation, Temporary, Type, Value, ErrorRepresentation, Error, ErrorMessage, Field (+8 more)

### Community 15 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.12
Nodes (17): AuthorizationHandler, AuthorizationHandlerContext, IAuthorizationRequirement, CancellationToken, ClaimsPrincipal, HttpContext, IFusionCache, IHttpContextAccessor (+9 more)

### Community 16 - "EmailOptions"
Cohesion: 0.09
Nodes (24): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+16 more)

### Community 17 - "HangfireCustomAuthorizationFilter"
Cohesion: 0.29
Nodes (5): DashboardContext, IAuthorizationService, IDashboardAsyncAuthorizationFilter, HangfireCustomAuthorizationFilter, Task

### Community 18 - "AggregateRoot"
Cohesion: 0.08
Nodes (25): AggregateRoot, Events, Id, Version, IReadOnlyCollection, List, IAggregateRoot, Events (+17 more)

### Community 19 - "IntegrationEventHandlerBase"
Cohesion: 0.22
Nodes (12): IConsumer, IntegrationEventHandlerBase, MaxEventAge, CancellationToken, ConsumeContext, DefaultIdType, IFusionCache, ILogger (+4 more)

### Community 20 - "IntegrationEvent"
Cohesion: 0.11
Nodes (17): IReadOnlyList, IntegrationEvent, CreatedOn, Id, DateTimeOffset, DefaultIdType, ProductCreatedIntegrationEvent, StoreCreatedIntegrationEvent (+9 more)

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.14
Nodes (8): ReadOnlySpan, BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position, Stream

### Community 22 - "DeviceRegistration"
Cohesion: 0.14
Nodes (13): DateTimeOffset, DefaultIdType, Guid, DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive (+5 more)

### Community 23 - ".AddBrevo"
Cohesion: 0.13
Nodes (13): CancellationToken, Task, IEmailGateway, IConfiguration, IFusionCache, IOptions, IServiceCollection, Setup (+5 more)

### Community 24 - "DeactivateDeviceSessionsRequest"
Cohesion: 0.13
Nodes (14): DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse, IReadOnlyList, CancellationToken, RouteGroupBuilder, Task, Endpoint, CancellationToken (+6 more)

### Community 25 - "NetGsmSmsGateway"
Cohesion: 0.18
Nodes (11): SendResponseBody, CancellationToken, HttpClient, IOptions, JsonSerializerOptions, SendRequestBody, Task, NetGsmSmsGateway (+3 more)

### Community 26 - "IEvent"
Cohesion: 0.11
Nodes (13): CancellationToken, Task, IEventHandler, CancellationToken, Task, IEventHandlerWrapper, CancellationToken, Task (+5 more)

### Community 27 - "RequestResponseBodyLoggingMiddleware"
Cohesion: 0.19
Nodes (7): IDiagnosticContext, HttpContext, IList, IOptions, PathString, RequestDelegate, RequestResponseBodyLoggingMiddleware

### Community 28 - "KeycloakTokenClient"
Cohesion: 0.15
Nodes (18): JsonWebTokenHandler, CancellationToken, Task, IKeycloakTokenClient, DateTimeOffset, KeycloakTokens, CancellationToken, Dictionary (+10 more)

### Community 29 - "Result"
Cohesion: 0.12
Nodes (15): Result, Error, IsFailure, Success, Value, Func, Task, AsyncExtensions (+7 more)

### Community 30 - "Product"
Cohesion: 0.09
Nodes (24): ProductTemplate, ProductTemplateId, Store, StoreId, Product, Description, Language, Name (+16 more)

### Community 31 - ".VerifyOtp"
Cohesion: 0.12
Nodes (15): IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, IssueVerificationTokenRequest, IssueVerificationTokenResponse, EmailNormalization, CancellationToken, RouteGroupBuilder, Task, Endpoint (+7 more)

### Community 32 - ".SingleAsResult"
Cohesion: 0.18
Nodes (10): CollectionExtensions, Func, ICollection, IEnumerable, PersistenceQueryableExtensions, CancellationToken, Expression, Func (+2 more)

### Community 33 - "Products.Domain.Products.DomainEvents.v1"
Cohesion: 0.10
Nodes (13): Products.Domain.Products.DomainEvents.v1, ProductId, V1ProductDescriptionUpdatedDomainEvent, ProductId, V1ProductNameUpdatedDomainEvent, ProductId, V1ProductPriceDecreasedDomainEvent, ProductId (+5 more)

### Community 34 - "KeycloakPermissionClient"
Cohesion: 0.12
Nodes (20): CancellationToken, Dictionary, HttpClient, HttpResponseMessage, ILogger, IOptions, IReadOnlyList, List (+12 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.10
Nodes (20): IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, DateTimeOffset, OutboxMessage (+12 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.08
Nodes (22): CheckRegistrationRateLimitingPolicy, EmailRateLimitingPolicy, OtpVerifyRateLimitingPolicy, RegisterRateLimitingPolicy, SmsRateLimitingPolicy, CustomRateLimitingOptions, CheckRegistration, CreateStore (+14 more)

### Community 37 - "DomainEvent"
Cohesion: 0.08
Nodes (24): AuditLogEntry, AggregateId, AggregateType, Event, EventType, Version, DefaultIdType, DomainEvent (+16 more)

### Community 38 - ".RefreshToken"
Cohesion: 0.15
Nodes (11): IAM.Endpoints.Tokens.VersionNeutral.Refresh, CancellationToken, RouteGroupBuilder, Task, Endpoint, DateTimeOffset, Response, AccessToken (+3 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.08
Nodes (24): IHostBuilder, LoggerConfiguration, LoggerMinimumLevelConfiguration, ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics (+16 more)

### Community 40 - "IamTelemetry"
Cohesion: 0.22
Nodes (6): ActivitySource, Counter, Meter, IamTelemetry, LoginMethods, SessionRevokedReasons

### Community 41 - "Request"
Cohesion: 0.10
Nodes (18): Common.Domain.Extensions, IAM.Endpoints.Users.VersionNeutral.SelfRegisterByEmail, SearchValues, StringExtensions, Guid, Request, BirthDate, CaptchaToken (+10 more)

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
Cohesion: 0.13
Nodes (18): BindDeviceSessionRequest, BindDeviceSessionResponse, Guid, CancellationToken, Exception, Guid, ILogger, LoggerMessage (+10 more)

### Community 46 - "Common.Application.DTOs"
Cohesion: 0.12
Nodes (8): Products.Endpoints.Stores.v1.Search, Products.Endpoints.Products.v1.My.Get, Products.Endpoints.ProductTemplates.v1.Search, Common.Application.DTOs, Products.Endpoints.Stores.v1.Get, Products.Endpoints.ProductTemplates.v1.Get, Products.Endpoints.Products.v1.Get, Products.Endpoints.Stores.v1.My.Get

### Community 48 - ".AddPersistence"
Cohesion: 0.15
Nodes (11): IDatabaseSeeder, Priority, CancellationToken, Task, CancellationToken, IServiceScopeFactory, Task, ProductsDatabaseSeeder (+3 more)

### Community 50 - "Response"
Cohesion: 0.09
Nodes (21): IAM.Endpoints.Users.VersionNeutral.Search, Constants, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, SearchTerm (+13 more)

### Community 51 - ".AddKeycloakInfrastructure"
Cohesion: 0.16
Nodes (10): CancellationToken, Task, IServiceAccountTokenProvider, HttpClient, IOptions, TimeProvider, ServiceAccountTokenProvider, IOptions (+2 more)

### Community 52 - "ReCaptchaService"
Cohesion: 0.21
Nodes (10): FormUrlEncodedContent, ReCaptchaResponse, CancellationToken, Exception, HttpClient, ILogger, IOptions, LoggerMessage (+2 more)

### Community 53 - "Inventory.Domain.StockReservations"
Cohesion: 0.08
Nodes (17): Products.Infrastructure.InterModuleRequestHandlers, Inventory.Infrastructure.InterModuleRequestHandlers, Inventory.Endpoints, Inventory.Application.Persistence, Inventory.Infrastructure.Persistence.EntityConfigurations, Inventory.Infrastructure.Gateway, Inventory.Domain.StockReservations, Inventory.Infrastructure.Persistence (+9 more)

### Community 54 - ".SendOtp"
Cohesion: 0.13
Nodes (15): EmailOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, SendEmailOtpRequest, SendEmailOtpResponse, CancellationToken, IFeatureManager (+7 more)

### Community 55 - "BrevoEmailGateway"
Cohesion: 0.23
Nodes (10): Exception, HttpClient, HttpStatusCode, ILogger, IOptions, JsonSerializerOptions, LoggerMessage, BrevoEmailGateway (+2 more)

### Community 56 - "CaptchaOptions"
Cohesion: 0.16
Nodes (13): CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey, Provider, ScoreThreshold, SecretKey (+5 more)

### Community 58 - "OutboxMetricsJob"
Cohesion: 0.13
Nodes (14): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxMetricsJob (+6 more)

### Community 59 - ".SearchProductTemplatesAsync"
Cohesion: 0.15
Nodes (11): LikePattern, CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response (+3 more)

### Community 61 - "Request"
Cohesion: 0.20
Nodes (9): Constants, ProductTemplateId, Request, Description, Name, Price, ProductTemplateId, Quantity (+1 more)

### Community 62 - "IDbContext"
Cohesion: 0.14
Nodes (12): DatabaseFacade, EntityEntry, IDbContext, AuditLog, ChangeTracker, Database, DbSet, DbContextExtensions (+4 more)

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

### Community 67 - "Response"
Cohesion: 0.22
Nodes (8): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Brand, Color, Model

### Community 68 - "ValueObject"
Cohesion: 0.25
Nodes (4): Common.Domain, IComparable, ValueObject, IEnumerable

### Community 69 - ".HandleAsync"
Cohesion: 0.18
Nodes (11): GetSeedUserIdsRequest, GetSeedUserIdsResponse, ICollection, CancellationToken, Task, GetSeedUserIdsRequestHandler, CancellationToken, IResult (+3 more)

### Community 70 - "PolymorphicEventConverter"
Cohesion: 0.21
Nodes (7): JsonConverter, UnknownDomainEvent, PolymorphicEventConverter, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 71 - "Full-Text Search"
Cohesion: 0.06
Nodes (33): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Add a new language/culture, Add search to a new entity _(Build checklist)_ (+25 more)

### Community 72 - "AuditableEntityResponse"
Cohesion: 0.13
Nodes (14): AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, DateTimeOffset, RouteGroupBuilder (+6 more)

### Community 73 - ".WriteAsync"
Cohesion: 0.33
Nodes (5): Memory, ReadOnlyMemory, CancellationToken, Task, ValueTask

### Community 75 - "IStronglyTypedId"
Cohesion: 0.21
Nodes (8): StronglyTypedIdReadOnlyJsonConverter, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, IStronglyTypedId, Value, DefaultIdType

### Community 76 - "PaginationRequestValidator"
Cohesion: 0.29
Nodes (6): Products.Endpoints.Products.v1.AuditLog, PaginationRequestValidator, Request, Id, RequestValidator, RequestValidator

### Community 77 - ".SaveChangesAsync"
Cohesion: 0.06
Nodes (23): CancellationToken, Task, CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint, CancellationToken (+15 more)

### Community 78 - "CheckRegistrationRateLimitingPolicy"
Cohesion: 0.25
Nodes (8): IRateLimiterPolicy, CancellationToken, Func, IOptions, OnRejectedContext, ValueTask, CheckRegistrationRateLimitingPolicy, OnRejected

### Community 79 - "SeedingCompletionTracker"
Cohesion: 0.15
Nodes (8): SeedingCompletionTracker, CancellationToken, Exception, Task, Setup, IOptions, IServiceCollection, TaskCompletionSource

### Community 80 - "Request"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Stores.v1.Update, RequestBody, Request, Body, Id, RequestBody, Address, Description (+3 more)

### Community 81 - ".SearchStoresAsync"
Cohesion: 0.10
Nodes (18): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Request, Address (+10 more)

### Community 82 - "UserRepresentation"
Cohesion: 0.15
Nodes (13): Dictionary, List, UserRepresentation, Attributes, CreatedTimestamp, Credentials, Email, EmailVerified (+5 more)

### Community 83 - "NotificationsDbContext"
Cohesion: 0.18
Nodes (10): DbContextOptions, DbSet, ILogger, TimeProvider, NotificationsDbContext, DeviceRegistrations, IApplicationBuilder, ILoggerFactory (+2 more)

### Community 84 - ".Configure"
Cohesion: 0.14
Nodes (14): AuditableEntityConfiguration, EntityTypeBuilder, StronglyTypedIdValueConverter, DefaultIdType, EntityTypeBuilder, DeviceRegistrationConfiguration, EntityTypeBuilder, NpgsqlTsVector (+6 more)

### Community 85 - "ResultTelemetryExtensions"
Cohesion: 0.38
Nodes (4): Activity, ResultTelemetryExtensions, ActivitySource, Task

### Community 86 - "Store"
Cohesion: 0.09
Nodes (19): StoreId, V1StoreAddressUpdatedDomainEvent, StoreId, V1StoreDescriptionUpdatedDomainEvent, StoreId, V1StoreEmptiedDomainEvent, IReadOnlyCollection, List (+11 more)

### Community 87 - "PushOptions"
Cohesion: 0.19
Nodes (13): PushOptions, Provider, SendTimeoutSeconds, ServiceAccount, Templates, PushOptionsValidator, PushProvider, Dummy (+5 more)

### Community 89 - "Program.cs"
Cohesion: 0.22
Nodes (6): ConfigurationManager, Host, Host.Configurations, Setup, Program, WebApplicationBuilder

### Community 90 - "IInterModuleRequestClient"
Cohesion: 0.10
Nodes (21): Products.Endpoints.Products, IInterModuleRequestClient, GetStockLevelRequest, GetStockLevelResponse, DefaultIdType, CancellationToken, Task, GetStockLevelRequestHandler (+13 more)

### Community 91 - "Request"
Cohesion: 0.17
Nodes (12): Products.Endpoints.Products.v1.My.Update, RequestBody, Request, Body, Id, RequestBody, Description, Name (+4 more)

### Community 92 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.08
Nodes (14): Common.Domain.StronglyTypedIds, Common.Domain.Events, Notifications.Domain.Devices, IAM.Endpoints.Users.VersionNeutral.Get, IAM.Endpoints.Users.VersionNeutral.Me.Get, Common.Infrastructure.EventBus, Common.Domain.Entities, Common.Infrastructure.Persistence.EntityConfigurations (+6 more)

### Community 93 - ".GetClientKey"
Cohesion: 0.18
Nodes (8): IAM.Endpoints.Captcha.VersionNeutral, IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, RouteGroupBuilder, Endpoint, Response, ClientKey, RouteGroupBuilder, Setup

### Community 94 - "OtpOptions"
Cohesion: 0.18
Nodes (11): OtpOptions, DummyCode, EmailQuotaWindowMinutes, ExpirationInMinutes, Length, MaxSendsPerEmailPerWindow, MaxSendsPerPhonePerWindow, PhoneQuotaWindowMinutes (+3 more)

### Community 95 - ".SearchMyProductsAsync"
Cohesion: 0.09
Nodes (21): Products.Endpoints.Products.v1.My.Search, CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Request (+13 more)

### Community 96 - "DatabaseSeederOrchestrator"
Cohesion: 0.27
Nodes (9): BackgroundService, DatabaseSeederOrchestrator, CancellationToken, Exception, IHostApplicationLifetime, ILogger, IServiceScopeFactory, LoggerMessage (+1 more)

### Community 97 - "StockReservationExpirySweepService"
Cohesion: 0.24
Nodes (9): CancellationToken, Exception, Guid, ILogger, IOptions, LoggerMessage, Task, TimeProvider (+1 more)

### Community 98 - ".SearchStoreProductsAsync"
Cohesion: 0.10
Nodes (18): ISearchLanguageResolver, UniversalConfig, SearchLanguageResolver, UniversalConfig, IOptions, Setup, IServiceCollection, CancellationToken (+10 more)

### Community 99 - ".AddNetGsm"
Cohesion: 0.21
Nodes (9): ISmsGateway, IConfiguration, IFusionCache, IOptions, IServiceCollection, Setup, IFusionCache, IOptions (+1 more)

### Community 100 - "OutboxDbContext"
Cohesion: 0.14
Nodes (13): DbContext, IOutboxDbContext, OutboxMessages, CancellationToken, DbSet, Task, IConfiguration, IServiceCollection (+5 more)

### Community 101 - "ResiliencyOptions"
Cohesion: 0.10
Nodes (19): HttpStandardResilienceOptions, IHttpClientBuilder, ResiliencyOptions, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds (+11 more)

### Community 102 - "HttpContextTargetingContextAccessor"
Cohesion: 0.17
Nodes (9): Common.Infrastructure.FeatureManagement, ITargetingContextAccessor, HttpContextTargetingContextAccessor, IHttpContextAccessor, ValueTask, Setup, IConfiguration, IServiceCollection (+1 more)

### Community 103 - "BackgroundJobsTelemetry"
Cohesion: 0.14
Nodes (11): IServerFilter, PerformedContext, PerformingContext, JobMetricsFilter, BackgroundJobsTelemetry, ActivitySource, ConcurrentDictionary, Counter (+3 more)

### Community 104 - "ProductId"
Cohesion: 0.08
Nodes (24): Products.Endpoints.Stores.v1.My.RemoveProduct, Products.Endpoints.Products.v1.Update, DefaultIdType, ProductId, Request, Id, RequestValidator, Request (+16 more)

### Community 105 - ".RegisterAsync"
Cohesion: 0.14
Nodes (12): CancellationToken, IFeatureManager, ILogger, RouteGroupBuilder, Task, Endpoint, DateTimeOffset, Response (+4 more)

### Community 106 - "OutboxModule.cs"
Cohesion: 0.13
Nodes (9): Outbox, Common.Infrastructure.Persistence.Outbox, Outbox.Persistence, Common.Application.Persistence.Outbox, Outbox.Telemetry, IEntityTypeConfiguration, ModelBuilder, EntityTypeBuilder (+1 more)

### Community 107 - ".ReleaseStockReservationAsync"
Cohesion: 0.13
Nodes (14): CancellationToken, Task, IWarehouseGateway, CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint (+6 more)

### Community 108 - "StockLevel"
Cohesion: 0.17
Nodes (11): Inventory.Domain.StockLevels.DomainEvents.v1, DefaultIdType, StockLevelId, V1StockLevelCreatedDomainEvent, DefaultIdType, StockLevel, ProductId, QuantityOnHand (+3 more)

### Community 109 - ".IsRegisteredAsync"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, IsRegistered

### Community 110 - "KeycloakPermissionPolicyProvider"
Cohesion: 0.27
Nodes (8): AuthorizationOptions, AuthorizationPolicy, DefaultAuthorizationPolicyProvider, IAuthorizationPolicyProvider, ConcurrentDictionary, IOptions, Task, KeycloakPermissionPolicyProvider

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
Cohesion: 0.12
Nodes (16): RouteGroupBuilder, Endpoint, DateOnly, DateTimeOffset, IReadOnlyCollection, Response, BirthDate, CreatedOn (+8 more)

### Community 115 - "InterModuleRequestHandler"
Cohesion: 0.21
Nodes (7): IInterModuleRequestHandler, CancellationToken, Task, InterModuleRequestHandler, CancellationToken, ConsumeContext, Task

### Community 116 - ".WriteProblemAsync"
Cohesion: 0.12
Nodes (14): IAllowAnonymous, IConfigureNamedOptions, ProblemDetailsContext, ProblemDetailsServiceExtensions, IProblemDetailsService, Task, HttpContext, HttpStatusCode (+6 more)

### Community 117 - "Response"
Cohesion: 0.14
Nodes (13): CancellationToken, RouteGroupBuilder, Task, Endpoint, DateTimeOffset, DefaultIdType, Response, Id (+5 more)

### Community 118 - "IModule"
Cohesion: 0.09
Nodes (20): OpenTelemetryBuilder, ResourceBuilder, IModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority (+12 more)

### Community 119 - "ProductsModule"
Cohesion: 0.12
Nodes (13): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, ProductsModule (+5 more)

### Community 120 - ".HandleWarehouseWebhookAsync"
Cohesion: 0.19
Nodes (10): IHeaderDictionary, IValidator, CancellationToken, HttpContext, IOptions, JsonSerializerOptions, RouteGroupBuilder, Task (+2 more)

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
Cohesion: 0.20
Nodes (11): IApplicationBuilder, IServiceCollection, Exception, HttpContext, ILogger, IProblemDetailsService, IResxLocalizer, LoggerMessage (+3 more)

### Community 127 - "BaseDbContext"
Cohesion: 0.09
Nodes (17): BaseDbContext, AuditLog, CancellationToken, DateTimeOffset, DbContextOptions, DbSet, ILogger, ModelConfigurationBuilder (+9 more)

### Community 128 - "EmailRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, EmailRateLimitingPolicy (+1 more)

### Community 129 - "V1StoreCreatedDomainEvent"
Cohesion: 0.29
Nodes (8): DomainEventHandlerBase, CancellationToken, Task, SimulateSomeBusinessHandler, StoreCreatedIntegrationEventPublishingHandler, V1StoreCreatedDomainEventHandlers, StoreId, V1StoreCreatedDomainEvent

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

### Community 135 - ".ListSessions"
Cohesion: 0.10
Nodes (22): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, IReadOnlyList, CancellationToken, IReadOnlyCollection, RouteGroupBuilder (+14 more)

### Community 136 - ".AddProductAsync"
Cohesion: 0.18
Nodes (9): Products.Endpoints.Stores.v1.AddProduct, CancellationToken, ProductTemplate, RouteGroupBuilder, Store, Task, Endpoint, Response (+1 more)

### Community 137 - ".WriteTooManyRequestsToResponse"
Cohesion: 0.13
Nodes (14): PartitionedRateLimiter, CancellationToken, Func, HttpContext, IConfiguration, IProblemDetailsService, IReadOnlyList, IResxLocalizer (+6 more)

### Community 138 - ".From"
Cohesion: 0.07
Nodes (30): AspNetResult, Common.Application.EndpointFilters, IEndpointFilter, IFeatureManagerSnapshot, ProblemDetails, ResxLocalizer, ProblemResponse, ResultToAcceptedResponseTransformer (+22 more)

### Community 139 - "ProductTemplate"
Cohesion: 0.09
Nodes (15): StronglyTypedIdHelper, ProductTemplateId, DefaultIdType, IReadOnlyList, List, ProductTemplate, Brand, Color (+7 more)

### Community 140 - "Endpoint"
Cohesion: 0.29
Nodes (5): IAM.Endpoints.Users.VersionNeutral, RouteGroupBuilder, Endpoint, RouteGroupBuilder, Setup

### Community 141 - ".GetMeAsync"
Cohesion: 0.19
Nodes (9): CancellationToken, IReadOnlyList, Task, IKeycloakPermissionClient, IReadOnlyList, GrantedPermission, CancellationToken, HttpContext (+1 more)

### Community 142 - "StoreId"
Cohesion: 0.11
Nodes (15): Products.Endpoints.Stores.v1.AuditLog, StoreId, DefaultIdType, StoreId, Request, Id, RequestValidator, RouteGroupBuilder (+7 more)

### Community 143 - ".AddModules"
Cohesion: 0.14
Nodes (12): LoadAll, ModuleRegistry, Names, Exception, IApplicationBuilder, IConfiguration, ILogger, IReadOnlyCollection (+4 more)

### Community 144 - "Response"
Cohesion: 0.12
Nodes (16): CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate (+8 more)

### Community 145 - "EnrichLogsWithUserInfoMiddleware"
Cohesion: 0.12
Nodes (12): IAuthenticationSchemeProvider, IMiddleware, IApplicationBuilder, HttpContext, RequestDelegate, Task, EnrichLogsWithUserInfoMiddleware, HttpContext (+4 more)

### Community 146 - ".CreateTokensByEmail"
Cohesion: 0.13
Nodes (18): VerifyEmailOtpRequest, VerifyEmailOtpResponse, CancellationToken, Exception, ILogger, LoggerMessage, RouteGroupBuilder, Task (+10 more)

### Community 147 - "IRecurringBackgroundJobs"
Cohesion: 0.13
Nodes (13): RecurringJobOptions, IRecurringBackgroundJobs, Action, Expression, Func, Task, RecurringBackgroundJobsService, Action (+5 more)

### Community 148 - "AuditLogRetentionService"
Cohesion: 0.33
Nodes (7): AuditLogRetentionService, CancellationToken, ILogger, IOptions, LoggerMessage, NpgsqlDataSource, Task

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
Cohesion: 0.30
Nodes (6): AutoMigrateMarker, IAutoMigrateMarker, MigrationGuard, ILogger, IServiceProvider, LoggerMessage

### Community 153 - "SwaggerDefaultValues"
Cohesion: 0.33
Nodes (4): JsonValue, OpenApiOperation, OperationFilterContext, SwaggerDefaultValues

### Community 154 - ".SendCoreAsync"
Cohesion: 0.27
Nodes (7): IReadOnlyDictionary, IReadOnlyList, PushMessage, CancellationToken, IEnumerable, IReadOnlyList, Task

### Community 155 - "EmailMessage"
Cohesion: 0.32
Nodes (6): EmailMessage, CancellationToken, ILogger, LoggerMessage, Task, DummyEmailGateway

### Community 156 - "Request"
Cohesion: 0.07
Nodes (28): IAM.Domain.Users, IAM.Endpoints.Common.Validations, IResxLocalizer, IRuleBuilder, IRuleBuilderOptions, CommonValidations, IResxLocalizer, IRuleBuilder (+20 more)

### Community 157 - "StrictDateTimeOffsetJsonConverter"
Cohesion: 0.33
Nodes (6): StrictDateTimeOffsetJsonConverter, DateTimeOffset, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 158 - "EventDispatcher"
Cohesion: 0.27
Nodes (7): EventDispatcher, ActivitySource, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task

### Community 159 - "IInventoryDbContext"
Cohesion: 0.13
Nodes (15): DbSet, IInventoryDbContext, StockLevels, StockReservations, DbContextOptions, DbSet, ILogger, TimeProvider (+7 more)

### Community 160 - "OtpServiceBase"
Cohesion: 0.21
Nodes (8): OtpCacheEntry, DateTimeOffset, CancellationToken, IFusionCache, SemaphoreSlim, Task, TimeSpan, OtpServiceBase

### Community 161 - ".ReserveSeriesAsync"
Cohesion: 0.11
Nodes (16): Inventory.Endpoints.StockReservations.v1.ReserveSeries, CancellationToken, IOptions, List, RequestBody, RouteGroupBuilder, Task, TimeProvider (+8 more)

### Community 162 - "StronglyTypedIdWriteOnlyJsonConverter"
Cohesion: 0.28
Nodes (5): StronglyTypedIdWriteOnlyJsonConverter, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 163 - "ApplicationUserId"
Cohesion: 0.12
Nodes (6): For, ApplicationUserId, IsEmpty, Value, DefaultIdType, NotificationGroupName

### Community 164 - "Request"
Cohesion: 0.18
Nodes (11): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, Guid, Request, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken (+3 more)

### Community 165 - "CurrentUser"
Cohesion: 0.12
Nodes (14): Common.Infrastructure.Auth.Services, Common.Infrastructure.Auth, CurrentUser, Id, IdAsString, IsAuthenticated, Principal, Roles (+6 more)

### Community 166 - "IamModule"
Cohesion: 0.14
Nodes (12): Action, IApplicationBuilder, IConfiguration, IEnumerable, IServiceCollection, RateLimiterOptions, IamModule, ActivitySourceNames (+4 more)

### Community 167 - "PaginationRequest"
Cohesion: 0.12
Nodes (14): Products.Endpoints.Stores.v1.My.AuditLog, PaginationRequest, PageNumber, PageSize, Skip, Take, PaginationQueryableExtensions, CancellationToken (+6 more)

### Community 168 - "Request"
Cohesion: 0.12
Nodes (13): Products.Endpoints.ProductTemplates, Products.Endpoints.ProductTemplates.v1.Create, RouteGroupBuilder, Setup, RouteGroupBuilder, Endpoint, Request, Brand (+5 more)

### Community 169 - "IInterModuleRequest"
Cohesion: 0.17
Nodes (13): IInterModuleRequest, SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, DbSet, INotificationsDbContext, DeviceRegistrations (+5 more)

### Community 171 - "IOtpService"
Cohesion: 0.24
Nodes (8): CancellationToken, Task, TimeSpan, IOtpService, OtpVerificationOutcome, InvalidOtp, Success, TooManyAttempts

### Community 172 - "ResxLocalizationOptions"
Cohesion: 0.25
Nodes (7): ResxLocalizationOptions, DefaultCulture, SupportedCultures, ResxLocalizationOptionsValidator, ICollection, IApplicationBuilder, IOptions

### Community 173 - "v1/AddProduct/Request.cs"
Cohesion: 0.15
Nodes (13): ProductTemplateId, RequestBody, Request, Body, Id, RequestBody, Description, Name (+5 more)

### Community 174 - "AuditLogRetentionJobRegistrar"
Cohesion: 0.31
Nodes (7): IHostedService, AuditLogRetentionJobRegistrar, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task

### Community 175 - "PaginationResponse"
Cohesion: 0.09
Nodes (22): JsonElement, AuditLogDto, DateTimeOffset, PaginationResponse, HasNext, HasPrevious, NextPageNumber, PreviousPageNumber (+14 more)

### Community 176 - ".FixedWindow"
Cohesion: 0.22
Nodes (7): RateLimitPartitions, HttpContext, IConnectionMultiplexer, ILoggerFactory, RateLimitPartition, HttpContext, RateLimitPartition

### Community 177 - "Request"
Cohesion: 0.18
Nodes (11): StoreId, Request, Description, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name (+3 more)

### Community 178 - ".AddPushServices"
Cohesion: 0.16
Nodes (11): CancellationToken, Task, IPushGateway, CancellationToken, ILogger, LoggerMessage, Task, DummyPushGateway (+3 more)

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

### Community 185 - "IdentityScheme"
Cohesion: 0.24
Nodes (8): IdentityScheme, Email, PhoneNumber, IdentitySchemeOptions, Scheme, IdentitySchemeOptionsValidator, IEndpointRouteBuilder, IOptions

### Community 186 - ".Get"
Cohesion: 0.24
Nodes (5): CreateStoreRateLimitingPolicy, RateLimiterOptions, Action, IEnumerable, RateLimiterOptions

### Community 187 - "ReverseProxyOptions"
Cohesion: 0.18
Nodes (9): ForwardedHeadersOptions, ReverseProxyOptions, ForwardLimit, IsEnabled, TrustedNetworks, ReverseProxyOptionsValidator, IReadOnlyList, IConfiguration (+1 more)

### Community 188 - "BoundedCaptureStream"
Cohesion: 0.14
Nodes (8): HttpResponse, SeekOrigin, BoundedCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 190 - "ServiceAccountTokenCache"
Cohesion: 0.11
Nodes (11): KeycloakPaths, CancellationToken, DateTimeOffset, SemaphoreSlim, Task, TimeSpan, ServiceAccountTokenCache, AccessToken (+3 more)

### Community 191 - "GetActiveSessionIdsRequest"
Cohesion: 0.44
Nodes (7): GetActiveSessionIdsRequest, GetActiveSessionIdsResponse, UserSessionIds, IReadOnlyList, CancellationToken, Task, GetActiveSessionIdsRequestHandler

### Community 192 - "CachedCaptchaService"
Cohesion: 0.25
Nodes (5): CancellationToken, IFusionCache, IOptions, Task, CachedCaptchaService

### Community 193 - "HttpWarehouseGateway"
Cohesion: 0.31
Nodes (8): ReleaseResponseBody, CancellationToken, HttpClient, HttpResponseMessage, Task, HttpWarehouseGateway, ReleaseRequestBody, ReleaseResponseBody

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
Cohesion: 0.22
Nodes (6): KeyValuePair, ActivitySource, Counter, Meter, NotificationsTelemetry, UpDownCounter

### Community 201 - "KeycloakScopes"
Cohesion: 0.20
Nodes (9): Devices, Hangfire, KeycloakScopes, Products, ProductTemplates, Sessions, StockReservations, Stores (+1 more)

### Community 202 - "Request"
Cohesion: 0.10
Nodes (19): Notifications.Infrastructure.Devices.UpdateCurrentPushToken, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, Common.Domain.Devices, DeviceSessionConstants, Guid, Request, ClientId, DeviceId (+11 more)

### Community 203 - "SmsRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, SmsRateLimitingPolicy (+1 more)

### Community 205 - ".AssignBasicRoleOrRollbackAsync"
Cohesion: 0.28
Nodes (6): CancellationToken, Exception, ILogger, LoggerMessage, Task, RegistrationCompletion

### Community 210 - "ConfigureSwaggerOptions"
Cohesion: 0.28
Nodes (7): ApiVersionDescription, IApiVersionDescriptionProvider, IConfigureOptions, OpenApiInfo, IOptions, SwaggerGenOptions, ConfigureSwaggerOptions

### Community 211 - "Common.Application.BackgroundJobs"
Cohesion: 0.31
Nodes (3): Common.Application.BackgroundJobs, BackgroundJobs.Telemetry, BackgroundJobs

### Community 212 - "Setup"
Cohesion: 0.29
Nodes (4): ApiVersionSet, Setup, IEndpointRouteBuilder, IServiceCollection

### Community 213 - "ProductsTelemetry"
Cohesion: 0.40
Nodes (4): ActivitySource, Counter, Meter, ProductsTelemetry

### Community 214 - "Common.Application.FeatureManagement"
Cohesion: 0.17
Nodes (5): Common.Application.FeatureManagement, IAM.Domain.Captcha, IAM.Infrastructure.Captcha.Services, IAM.Application.Captcha.Services, IAM.Infrastructure.Captcha

### Community 215 - ".CreateMyStoreAsync"
Cohesion: 0.14
Nodes (10): Products.Endpoints.Stores.v1.My.Create, Store, CancellationToken, Task, CancellationToken, RouteGroupBuilder, Task, Endpoint (+2 more)

### Community 216 - "Common.Application.ModelBinders"
Cohesion: 0.07
Nodes (27): Products.Endpoints.Stores.v1.Deactivate, Products.Endpoints.ProductTemplates.v1.Deactivate, Inventory.Endpoints.StockReservations.v1.Release, Common.Application.ModelBinders, Products.Endpoints.ProductTemplates.v1.Activate, IModelBinder, ModelBindingContext, StronglyTypedIdBinder (+19 more)

### Community 217 - "FeatureFlags"
Cohesion: 0.33
Nodes (5): Checkout, FeatureFlags, IAM, Notifications, Products

### Community 218 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 219 - "SendForLogin/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForLogin, Request, CaptchaToken, PhoneNumber, RequestValidator

### Community 220 - "DeviceRegistryReconciliationService"
Cohesion: 0.28
Nodes (6): CancellationToken, ILogger, IOptions, LoggerMessage, Task, DeviceRegistryReconciliationService

### Community 221 - "Keycloak realm as code"
Cohesion: 0.25
Nodes (7): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Secrets, Seed users (dev / test only)

### Community 222 - ".CreateTokens"
Cohesion: 0.15
Nodes (13): OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, VerifyPhoneOtpResponseExtensions, CancellationToken (+5 more)

### Community 223 - "Seeder"
Cohesion: 0.18
Nodes (12): CancellationToken, ILogger, LoggerMessage, ProductsDbContext, Task, CancellationToken, List, Task (+4 more)

### Community 224 - "Endpoint"
Cohesion: 0.29
Nodes (5): IAM.Endpoints.Tokens.VersionNeutral, RouteGroupBuilder, Endpoint, RouteGroupBuilder, Setup

### Community 225 - "BackgroundJobsOptions"
Cohesion: 0.16
Nodes (11): BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount, BackgroundJobsOptionsValidator, IApplicationBuilder (+3 more)

### Community 226 - ".MapCode"
Cohesion: 0.40
Nodes (3): Exception, ILogger, LoggerMessage

### Community 227 - "BackgroundJobsModule"
Cohesion: 0.22
Nodes (8): ICoreModule, BackgroundJobsModule, ActivitySourceNames, MeterNames, Name, StartupPriority, IEndpointRouteBuilder, IEnumerable

### Community 228 - "Request"
Cohesion: 0.25
Nodes (7): Constants, Request, Brand, Color, Model, SearchTerm, RequestValidator

### Community 229 - ".AddCommonOptions"
Cohesion: 0.20
Nodes (6): Setup, IConfiguration, IHostEnvironment, IServiceCollection, ValidationContextExtensions, ValidationContext

### Community 230 - ".ReserveStockAsync"
Cohesion: 0.22
Nodes (7): Inventory.Endpoints.StockReservations.v1.Reserve, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Id

### Community 231 - "Request"
Cohesion: 0.33
Nodes (6): Products.Endpoints.Stores.v1.My.Update, Request, Address, Description, Name, RequestValidator

### Community 232 - "Common.Application.Options"
Cohesion: 0.05
Nodes (31): Common.Infrastructure.Modules, Products.Infrastructure.Persistence, Notifications.Application.Sms, Notifications.Infrastructure.Devices, Notifications.Application.Push, Common.Endpoints.Versioning, Notifications.Infrastructure.Email, Common.Infrastructure.RateLimiting (+23 more)

### Community 233 - "Common.Application.Validation"
Cohesion: 0.11
Nodes (16): Products.Endpoints.Probe.v1, Common.Application.Validation, DatabaseOptions, ConnectionString, DatabaseOptionsValidator, Request, Id, RequestValidator (+8 more)

### Community 234 - "Setup"
Cohesion: 0.29
Nodes (4): IApplicationBuilder, IWebHostEnvironment, Type, Setup

### Community 235 - "Request"
Cohesion: 0.29
Nodes (7): Products.Endpoints.Stores.v1.Create, Request, Address, Description, Name, OwnerId, RequestValidator

### Community 236 - "StronglyTypedIdListReadOnlyJsonConverter"
Cohesion: 0.36
Nodes (6): StronglyTypedIdListReadOnlyJsonConverter, IReadOnlyList, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 237 - "ICaptchaService"
Cohesion: 0.36
Nodes (5): ICaptchaService, DummyCaptchaService, IConfiguration, IServiceCollection, Setup

### Community 238 - "Common.Infrastructure.Persistence"
Cohesion: 0.17
Nodes (5): Common.Infrastructure.Persistence, Products.Infrastructure.Persistence.Seeding, Common.Application.Persistence, Common.Infrastructure.Persistence.Auditing, Common.Infrastructure.Persistence.DbContext

### Community 239 - ".TryReadFromJsonAsync"
Cohesion: 0.33
Nodes (4): HttpContent, CancellationToken, Task, HttpContentJsonExtensions

### Community 240 - "v1/RemoveProduct/Request.cs"
Cohesion: 0.29
Nodes (7): Products.Endpoints.Stores.v1.RemoveProduct, ProductId, StoreId, Request, Id, ProductId, RequestValidator

### Community 241 - "RegisterRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, RegisterRateLimitingPolicy (+1 more)

### Community 242 - ".AddCustomSwagger"
Cohesion: 0.20
Nodes (7): IOpenApiSchema, ISchemaFilter, SchemaFilterContext, IConfigureOptions, IServiceCollection, SwaggerGenOptions, StronglyTypedIdSchemaFilter

### Community 243 - "CustomValidator"
Cohesion: 0.08
Nodes (27): AbstractValidator, Inventory.Endpoints.StockReservations.v1.Commit, CustomRateLimitingOptionsValidator, FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit (+19 more)

### Community 244 - "ReCaptchaResponse"
Cohesion: 0.25
Nodes (7): DateTime, ReCaptchaResponse, ChallengeTs, ErrorCodes, Hostname, Score, Success

### Community 245 - "Host.Swagger"
Cohesion: 0.22
Nodes (5): Host.Swagger, IOperationFilter, OpenApiOperation, OperationFilterContext, RemoveDefaultResponseSchemaFilter

### Community 246 - ".AddAuthInfrastructure"
Cohesion: 0.25
Nodes (6): IAuthorizationHandler, IAuthorizationPolicyProvider, IConfigureOptions, IServiceCollection, JwtBearerOptions, Setup

### Community 247 - "Common.InterModuleRequests"
Cohesion: 0.29
Nodes (4): Common.InterModuleRequests, IAssemblyReference, Setup, IServiceCollection

### Community 248 - "SendErrorBody"
Cohesion: 0.67
Nodes (3): SendErrorBody, Code, Message

### Community 249 - "IntegrationEventOutbox"
Cohesion: 0.10
Nodes (18): Inventory.Application.IntegrationEventHandlers, Common.IntegrationEvents, Products.Application.Products.DomainEventHandlers.v1, Products.Application.Stores.DomainEventHandlers.v1, Common.Application.EventBus, Lock, IIntegrationEventOutbox, IntegrationEventOutbox (+10 more)

### Community 250 - "KeycloakPermission"
Cohesion: 0.22
Nodes (3): KeycloakPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder

### Community 251 - ".HandleAsync"
Cohesion: 0.29
Nodes (4): CancellationToken, Task, CancellationToken, Task

### Community 252 - "RedisOtpService"
Cohesion: 0.32
Nodes (6): CancellationToken, IConnectionMultiplexer, IOptions, Task, TimeSpan, RedisOtpService

### Community 260 - "Response"
Cohesion: 0.29
Nodes (5): Products.Endpoints.Stores.v1.My.AddProduct, RouteGroupBuilder, Endpoint, Response, Id

### Community 261 - "DevicesOptions"
Cohesion: 0.33
Nodes (6): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator, IReadOnlyCollection

### Community 262 - ".AddCommonCaching"
Cohesion: 0.29
Nodes (5): Common.Infrastructure.Caching, Setup, IConfiguration, IConnectionMultiplexer, IServiceCollection

### Community 263 - "Response"
Cohesion: 0.25
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegister, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 264 - "Common.Infrastructure.Persistence.AuditLog"
Cohesion: 0.29
Nodes (3): Common.Infrastructure.Persistence.AuditLog, Setup, IServiceCollection

### Community 265 - "FirebaseServiceAccountOptions"
Cohesion: 0.29
Nodes (7): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri

### Community 267 - "AuditLogOptions"
Cohesion: 0.33
Nodes (6): AuditLogOptions, PerSchemaRetentionDays, PurgeBatchSize, RetentionDays, AuditLogOptionsValidator, Dictionary

### Community 268 - ".SendOtp"
Cohesion: 0.29
Nodes (5): CancellationToken, IFeatureManager, RouteGroupBuilder, Task, Endpoint

### Community 269 - "Request"
Cohesion: 0.40
Nodes (5): Inventory.Endpoints.StockReservations.v1.WebhookCallback, Request, ProviderReference, ReservationId, RequestValidator

### Community 270 - "DefaultResponsesOperationFilter"
Cohesion: 0.70
Nodes (3): OpenApiOperation, OperationFilterContext, DefaultResponsesOperationFilter

### Community 271 - "Common.Application.JsonConverters"
Cohesion: 0.33
Nodes (3): Common.Application.JsonConverters, DomainEventConverter, JsonSerializerOptions

### Community 272 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.07
Nodes (25): Notifications.Application.Otp, IAM.Endpoints.Users, IAM.Infrastructure.Keycloak.Representations, Common.Application.Caching, IAM.Endpoints.Otp, Notifications.Application.Persistence, Notifications.Infrastructure.Telemetry, Common.InterModuleRequests.IAM (+17 more)

### Community 273 - "SendForRegistration/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, Request, CaptchaToken, PhoneNumber, RequestValidator

### Community 274 - "GetProductRequest"
Cohesion: 0.39
Nodes (6): GetProductRequest, GetProductResponse, DefaultIdType, CancellationToken, Task, GetProductRequestHandler

### Community 275 - "Request"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForEmail, Request, CaptchaToken, Email, RequestValidator

### Community 276 - "SendPhoneOtpRequestHandler"
Cohesion: 0.21
Nodes (11): OtpDispatchErrors, SendPhoneOtpRequest, SendPhoneOtpResponse, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, IFusionCache (+3 more)

### Community 277 - "SendResponseBody"
Cohesion: 0.50
Nodes (4): SendResponseBody, Code, Description, JobId

### Community 278 - "ReservationStatus"
Cohesion: 0.29
Nodes (6): ReservationStatus, Active, Committed, Expired, Released, RequiresReconciliation

### Community 279 - "Response"
Cohesion: 0.25
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.Create, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 280 - "InventoryTelemetry"
Cohesion: 0.40
Nodes (4): ActivitySource, Counter, Meter, InventoryTelemetry

### Community 281 - "TokenResponseRepresentation"
Cohesion: 0.33
Nodes (6): TokenResponseRepresentation, AccessToken, ExpiresIn, RefreshExpiresIn, RefreshToken, SessionState

### Community 282 - "Setup"
Cohesion: 0.13
Nodes (8): Common.Infrastructure.Localization, Host.Middlewares, Host.Infrastructure, Type, Setup, Assembly, IEnumerable, IServiceCollection

### Community 283 - ".RequireOtpTemplateForDefaultCulture"
Cohesion: 0.40
Nodes (4): Func, IConfiguration, IEnumerable, OtpTemplateConfiguration

### Community 284 - "Request"
Cohesion: 0.20
Nodes (10): DateTimeOffset, DefaultIdType, RequestBody, Request, Body, RequestBody, ProductId, Quantity (+2 more)

### Community 285 - "Products.Endpoints.Probe"
Cohesion: 0.40
Nodes (3): Products.Endpoints.Probe, RouteGroupBuilder, Setup

### Community 286 - ".UpdateCurrentPushToken"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 287 - "Inventory.Endpoints.StockReservations"
Cohesion: 0.40
Nodes (3): Inventory.Endpoints.StockReservations, RouteGroupBuilder, Setup

### Community 288 - "DummySmsGateway"
Cohesion: 0.38
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, DummySmsGateway

### Community 289 - ".AddDeviceRegistryReconciliation"
Cohesion: 0.33
Nodes (3): IServiceCollection, RouteGroupBuilder, Setup

### Community 290 - "IProductsDbContext"
Cohesion: 0.05
Nodes (34): ICurrentUser, Id, IdAsString, Roles, SessionId, ICollection, DbSet, ProductTemplate (+26 more)

### Community 291 - ".ActivateProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 293 - ".UpdateStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 294 - ".SeedProductAsync"
Cohesion: 0.33
Nodes (5): CancellationToken, List, ProductTemplateId, StoreId, Task

### Community 296 - "Request"
Cohesion: 0.50
Nodes (4): Request, Email, Otp, RequestValidator

### Community 297 - ".SendAsync"
Cohesion: 0.18
Nodes (9): CancellationToken, Task, SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, SmsMessage, CancellationToken (+1 more)

### Community 298 - "ModulesOptions"
Cohesion: 0.50
Nodes (4): ModulesOptions, EnabledModules, ModulesOptionsValidator, IReadOnlyList

### Community 299 - "Response"
Cohesion: 0.20
Nodes (7): Products.Endpoints.Stores, RouteGroupBuilder, Setup, RouteGroupBuilder, Endpoint, Response, Id

### Community 300 - "SecurityHeadersOptions"
Cohesion: 0.50
Nodes (4): SecurityHeadersOptions, Headers, SecurityHeadersOptionsValidator, Dictionary

### Community 303 - "V1StockReservationCommittedDomainEvent"
Cohesion: 0.40
Nodes (4): DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationCommittedDomainEvent

### Community 304 - "V1StockReservationReleaseAttemptStartedDomainEvent"
Cohesion: 0.40
Nodes (3): DateTimeOffset, StockReservationId, V1StockReservationReleaseAttemptStartedDomainEvent

### Community 305 - "V1StockReservationReservedDomainEvent"
Cohesion: 0.40
Nodes (4): DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationReservedDomainEvent

## Knowledge Gaps
- **898 isolated node(s):** `Id`, `IdAsString`, `Roles`, `SessionId`, `JwtClaimNames` (+893 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 2032 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **89 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `NotificationPayload`, `DevicesOptions`, `.AddCommonCaching`, `Common.Infrastructure.Persistence.AuditLog`, `AuditLogOptions`, `Endpoint`, `Common.Domain.ResultMonad`, `EmailOptions`, `EnrichLogsWithUserInfoMiddleware`, `Common.InterModuleRequests.Contracts`, `OutboxOptions`, `Setup`, `RequestResponseBodyLoggingMiddleware`, `Request`, `Request`, `ObservabilityOptions`, `Request`, `ModulesOptions`, `KeycloakOptions`, `ResxLocalizationOptions`, `SecurityHeadersOptions`, `CachingOptions`, `SmsOptions`, `RabbitMqOptions`, `Inventory.Domain.StockReservations`, `CaptchaOptions`, `IdentityScheme`, `ReverseProxyOptions`, `RequestLoggingOptions`, `CorsOptions`, `InventoryOptions`, `OpenApiOptions`, `Request`, `Common.Application.BackgroundJobs`, `Common.Application.FeatureManagement`, `PushOptions`, `Program.cs`, `Common.Domain.StronglyTypedIds`, `OtpOptions`, `Endpoint`, `BackgroundJobsOptions`, `ResiliencyOptions`, `.AddCommonOptions`, `Common.Application.Validation`, `OutboxModule.cs`, `Common.Infrastructure.Persistence`, `CustomValidator`, `Host.Swagger`, `IntegrationEventOutbox`, `FullTextSearchOptions`, `.AddCustomHealthChecks`?**
  _High betweenness centrality (0.268) - this node is a cross-community bridge._
- **Why does `Result` connect `Result` to `StockReservation`, `.ListSessions`, `.AddProductAsync`, `Inventory.Domain.StockReservations.DomainEvents.v1`, `Error`, `ProductTemplate`, `IKeycloakAdminClient`, `.SendAsync`, `.SendOtp`, `.GetMeAsync`, `Response`, `.CreateTokensByEmail`, `SendPhoneOtpRequestHandler`, `.AddBrevo`, `DeactivateDeviceSessionsRequest`, `NetGsmSmsGateway`, `.SendCoreAsync`, `EmailMessage`, `KeycloakTokenClient`, `.UpdateCurrentPushToken`, `.VerifyOtp`, `.SingleAsResult`, `.ReserveSeriesAsync`, `DummySmsGateway`, `IProductsDbContext`, `.ActivateProductTemplateAsync`, `DomainEvent`, `.RefreshToken`, `.UpdateStoreAsync`, `.SendAsync`, `.RegisterAsync`, `PaginationResponse`, `V1StockReservationReleaseAttemptStartedDomainEvent`, `Response`, `.AddPushServices`, `ReCaptchaService`, `.SendOtp`, `BrevoEmailGateway`, `.SearchProductTemplatesAsync`, `IDbContext`, `CachedCaptchaService`, `HttpWarehouseGateway`, `.SendCoreAsync`, `Response`, `.AssignBasicRoleOrRollbackAsync`, `.SaveChangesAsync`, `.SearchStoresAsync`, `ResultTelemetryExtensions`, `.CreateMyStoreAsync`, `IInterModuleRequestClient`, `Response`, `.GetClientKey`, `.CreateTokens`, `.SearchMyProductsAsync`, `.MapCode`, `.SearchStoreProductsAsync`, `.ReserveStockAsync`, `.RegisterAsync`, `.ReleaseStockReservationAsync`, `.IsRegisteredAsync`, `Response`, `.HandleWarehouseWebhookAsync`, `.TapWhenFeatureEnabledAsync`?**
  _High betweenness centrality (0.163) - this node is a cross-community bridge._
- **Why does `Common.Domain.ResultMonad` connect `Common.Domain.ResultMonad` to `Common.Application.Options`, `Request`, `.From`, `Error`, `Common.InterModuleRequests.Contracts`, `SendPhoneOtpRequestHandler`, `Inventory.Domain.StockReservations`, `Common.Application.FeatureManagement`, `Common.Domain.StronglyTypedIds`, `Result`?**
  _High betweenness centrality (0.081) - this node is a cross-community bridge._
- **What connects `Id`, `IdAsString`, `Roles` to the rest of the system?**
  _898 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `StockReservation` be split into smaller, more focused modules?**
  _Cohesion score 0.11857707509881422 - nodes in this community are weakly interconnected._
- **Should `NotificationPayload` be split into smaller, more focused modules?**
  _Cohesion score 0.052464947987336044 - nodes in this community are weakly interconnected._
- **Should `RedisFixedWindowRateLimiter` be split into smaller, more focused modules?**
  _Cohesion score 0.12554112554112554 - nodes in this community are weakly interconnected._