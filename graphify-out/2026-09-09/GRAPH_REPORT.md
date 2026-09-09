# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-09-09)

## Corpus Check
- 515 files · ~72,858 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 4157 nodes · 7390 edges · 356 communities (262 shown, 89 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 249 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `98090d67`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- Common.InterModuleRequests.Contracts
- NotificationPayload
- OutboxProcessor
- DeviceRegistryReconciliationService
- Hybrid DDD (Writes) / VSA (Reads)
- KeycloakAdminClient
- RedisFixedWindowRateLimiter
- ApplyAuditingInterceptor
- FirebasePushGateway
- Request
- Error
- Common.Domain.ResultMonad
- VerifyPhoneOtpResponse
- .SendOtp
- UserRepresentation
- KeycloakPermissionAuthorizationHandler
- .SendAsync
- .UseModule
- DomainEvent
- AuditableEntity
- ApplicationUserId
- BoundedRequestCaptureStream
- DeviceRegistration
- KeycloakPermissionPolicyProvider
- DeactivateDeviceSessionsRequest
- NetGsmSmsGateway
- IEvent
- RequestResponseBodyLoggingMiddleware
- KeycloakTokenClient
- Result
- Product
- .ForUser
- .CreateStoreAsync
- .AddProductAsync
- KeycloakPermissionClient
- OutboxMessage
- CustomRateLimitingOptions
- StockReservation
- .RefreshToken
- ObservabilityOptions
- SmsRateLimitingPolicy
- Request
- .SaveWithOutboxAsync
- CreateStoreRateLimitingPolicy
- KeycloakOptions
- .BindDeviceAsync
- ProductsModule.cs
- Outbox Misuse Check
- IOtpService
- Add Integration Event Command
- Response
- Common.Domain.Events
- ReCaptchaService
- Inventory.Domain.StockReservations
- Notifications.Application.Sms
- RegisterRateLimitingPolicy
- .AddNetGsm
- Cross-Module Reference Violation
- OutboxMetricsJob
- .SearchProductTemplatesAsync
- Bogus Test Data
- .AddProductToMyStoreAsync
- NotificationsDbContext
- RequestLoggingOptions
- Common.InterModuleRequests
- Common.Application.Options
- OutboxModule
- Response
- ValueObject
- V1StoreCreatedDomainEvent
- IStronglyTypedId
- Full-Text Search
- Response
- .WriteAsync
- AsNoTracking Coverage Check
- ProductId
- Common.Application.ModelBinders
- .CreateMyStoreAsync
- CheckRegistrationRateLimitingPolicy
- IntegrationEventOutbox
- EventDispatcher
- AuditableEntityResponse
- .Failure
- .EnsureNoMigrationsPending
- .Configure
- ResultTelemetryExtensions
- Store
- PushOptions
- Configuration-Driven Module Registration
- Program.cs
- IInterModuleRequestClient
- Setup
- Common.Domain.StronglyTypedIds
- Endpoint
- NotificationsModule.cs
- .SearchMyProductsAsync
- DatabaseSeederOrchestrator
- AuditLogRetentionJobRegistrar
- .SearchStoreProductsAsync
- KeycloakPermission
- OutboxDbContext
- ResiliencyOptions
- HttpContextTargetingContextAccessor
- Common.Application.BackgroundJobs
- CreateStockLevelOnProductCreatedHandler
- Uri
- OutboxModule.cs
- .ReleaseStockReservationAsync
- StockLevel
- .IsRegisteredAsync
- ProductsDbContext
- BackgroundJobsService
- OutboxCleanupJob
- CaptchaOptions
- Response
- IBackgroundJobs
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
- AuditLogEntry
- Response
- docker-compose.yml (Base Stack)
- .AddInfrastructure
- .SaveChangesAsync
- Consumer Idempotency (IntegrationEventHandlerBase)
- v1/RemoveProduct/Request.cs
- .ListSessions
- .SingleAsResult
- .WriteTooManyRequestsToResponse
- ResultToResponseTransformer
- ProductTemplate
- Response
- .GetMeAsync
- Response
- IDbContext
- Response
- EnrichLogsWithUserInfoMiddleware
- IAM.Endpoints.Otp.VersionNeutral
- .AddServices
- .AddModules
- Response
- Common.Application.FeatureManagement
- OutboxOptions
- .SearchStoresAsync
- SwaggerDefaultValues
- CustomValidator
- InterModuleRequestHandler
- PaginationRequestValidator
- IntegrationEventHandlerBase
- .FixedWindow
- IInventoryDbContext
- OtpServiceBase
- .ReserveSeriesAsync
- UtcDateTimeOffsetConverter
- For
- Request
- CurrentUser
- Response
- PaginationRequest
- ICaptchaService
- .Get
- Notifications.Application/IAssemblyReference.cs
- .UpdateStoreAsync
- ResxLocalizationOptions
- RequestBody
- INotificationsDbContext
- AuditLogDto
- PaginationResponse
- .AddAuthInfrastructure
- OtpOptions
- CachingOptions
- SmsOptions
- RabbitMqOptions
- IAM.Domain
- SendRequestBody
- RequestBody
- IamModule
- Request
- ReverseProxyOptions
- BoundedCaptureStream
- ServiceAccountTokenCache
- Split-Deployment PoC
- ProblemDetails
- Request
- CorsOptions
- InventoryOptions
- TokenEndpointRepresentations.cs
- DummyPushGateway
- InventoryModule
- OpenApiOptions
- KeyValuePair
- KeycloakScopes
- Request
- IProductsDbContext
- Configuration-Driven Module Loading
- Infrastructure/StringExtensions.cs
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- ConfigureSwaggerOptions
- NotificationsModule
- .AddNotificationsSignalR
- ProductsTelemetry
- .SendAsync
- RedisOtpService
- ProductTemplateId
- Request
- Response
- .AddPushServices
- Seeder
- Keycloak realm as code
- .CreateProductTemplateAsync
- ReCaptchaResponse
- AuditLogRetentionService
- BackgroundJobsOptions
- .HandleAsync
- BackgroundJobsModule
- Common.Application.Validation
- .AddCommonOptions
- Request
- .UpdateMyStoreAsync
- FirebaseServiceAccountOptions
- StockReservationExpirySweepService
- Setup
- .GetStoreAuditLogAsync
- .UpdateCurrentPushToken
- IDatabaseSeeder
- NotificationsHub
- .TryReadFromJsonAsync
- TokenResponseRepresentation
- IAM.Endpoints.Common.Validations
- .AddCustomSwagger
- .RemoveProductAsync
- HttpWarehouseGateway
- Host.Swagger
- StoreId
- StockReservationExpirySweepJobRegistrar
- .UpdateMyProductAsync
- Common.Application.EventBus
- Request
- Common.Application.JsonConverters
- ICurrentUser
- Products.Domain/IAssemblyReference.cs
- IAM.Application/IAssemblyReference.cs
- Products.Infrastructure/IAssemblyReference.cs
- Products.Application/IAssemblyReference.cs
- Common.Infrastructure/IAssemblyReference.cs
- Notifications.Domain/IAssemblyReference.cs
- IAM.Infrastructure/IAssemblyReference.cs
- .SendCoreAsync
- TokenCreateRateLimitingPolicy
- Request
- JwtClaimNames.cs
- KeycloakRoles.cs
- AuditLogOptions
- SecurityHeadersOptions
- LogProductCatalogChangeHandler
- Request
- DefaultResponsesOperationFilter
- Notifications.Application.Hubs
- RequireFeatureFilter
- FixedWindow
- GetProductRequest
- .ReserveStockAsync
- INotificationDispatcher
- SendResponseBody
- ThrottledSmsGateway
- .AddCommonCaching
- .GetAuditLogAsync
- .CommitStockReservationAsync
- Setup
- .InvokeAsync
- ProblemDetailsContext
- Products.Endpoints.Probe
- Stores/v1/Deactivate/Request.cs
- ProductTemplates/v1/Deactivate/Request.cs
- Inventory.Endpoints.StockReservations
- ModulesOptions
- .AddStockReservationExpirySweep
- Stores/Constants.cs
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
1. `Result` - 120 edges
2. `Common.Application.Options` - 109 edges
3. `Common.Domain.ResultMonad` - 91 edges
4. `CustomValidator` - 75 edges
5. `ApplicationUserId` - 69 edges
6. `Common.Application.Auth` - 66 edges
7. `Common.Application.Validation` - 66 edges
8. `Common.Application.Extensions` - 59 edges
9. `Common.Domain.StronglyTypedIds` - 59 edges
10. `Setup` - 50 edges

## Surprising Connections (you probably didn't know these)
- `Aspire Dashboard Service (mm.aspire-dashboard)` --conceptually_related_to--> `Observability (OpenTelemetry)`  [INFERRED]
  docker-compose.yml → CLAUDE.md
- `OtpErrors` --references--> `Error`  [EXTRACTED]
  src/Modules/IAM/IAM.Domain/Errors/OtpErrors.cs → src/Common/Common.Domain/ResultMonad/Error.cs
- `TokenErrors` --references--> `Error`  [EXTRACTED]
  src/Modules/IAM/IAM.Domain/Errors/TokenErrors.cs → src/Common/Common.Domain/ResultMonad/Error.cs
- `StockReservationErrors` --references--> `Error`  [EXTRACTED]
  src/Modules/Inventory/Inventory.Domain/StockReservations/Errors/StockReservationErrors.cs → src/Common/Common.Domain/ResultMonad/Error.cs
- `SmsErrors` --references--> `Error`  [EXTRACTED]
  src/Modules/Notifications/Notifications.Application/Sms/SmsErrors.cs → src/Common/Common.Domain/ResultMonad/Error.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Local Infrastructure Stack** — docker_compose_postgres, docker_compose_rabbitmq, docker_compose_redis, docker_compose_aspire_dashboard [EXTRACTED 1.00]

## Communities (356 total, 89 thin omitted)

### Community 0 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.09
Nodes (21): Notifications.Application.Otp, IAM.Endpoints.Otp, Notifications.Application.Persistence, Notifications.Infrastructure.Telemetry, Products.Endpoints.Probe.v1, Common.InterModuleRequests.IAM, Common.InterModuleRequests.Contracts, Common.Infrastructure.Extensions (+13 more)

### Community 1 - "NotificationPayload"
Cohesion: 0.34
Nodes (8): IHubContext, Task, INotificationsClient, NotificationPayload, CancellationToken, IReadOnlyList, Task, SignalRNotificationDispatcher

### Community 2 - "OutboxProcessor"
Cohesion: 0.22
Nodes (11): IPublishEndpoint, CancellationToken, Exception, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task (+3 more)

### Community 3 - "DeviceRegistryReconciliationService"
Cohesion: 0.07
Nodes (29): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator, IReadOnlyCollection, GetActiveSessionIdsRequest, GetActiveSessionIdsResponse (+21 more)

### Community 5 - "KeycloakAdminClient"
Cohesion: 0.15
Nodes (22): HttpRequestMessage, DateOnly, DateTimeOffset, IReadOnlyList, CreateKeycloakUser, GrantedPermission, KeycloakUser, KeycloakUserPage (+14 more)

### Community 6 - "RedisFixedWindowRateLimiter"
Cohesion: 0.13
Nodes (16): RateLimiter, RateLimiterStatistics, RateLimitLease, FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration (+8 more)

### Community 7 - "ApplyAuditingInterceptor"
Cohesion: 0.08
Nodes (20): Common.Infrastructure.Persistence.Auditing, SaveChangesInterceptor, ISearchLocalized, Language, ApplyAuditingInterceptor, CancellationToken, DbContextEventData, InterceptionResult (+12 more)

### Community 8 - "FirebasePushGateway"
Cohesion: 0.24
Nodes (8): FirebaseApp, FirebaseMessaging, IDisposable, Exception, ILogger, LoggerMessage, TimeSpan, FirebasePushGateway

### Community 9 - "Request"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Products.v1.My.Update, RequestBody, Request, Body, Id, RequestBody, Description, Name (+3 more)

### Community 10 - "Error"
Cohesion: 0.09
Nodes (16): StringLocalizerExtensions, IStringLocalizer, Error, Key, ParameterName, StatusCode, SubErrors, Value (+8 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.17
Nodes (10): Products.Endpoints.Stores.v1.Create, Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Products.Infrastructure.Telemetry, Products.Application.Persistence, Products.Domain.Stores, Common.Domain.ResultMonad (+2 more)

### Community 12 - "VerifyPhoneOtpResponse"
Cohesion: 0.20
Nodes (10): OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, VerifyPhoneOtpResponseExtensions, CancellationToken (+2 more)

### Community 13 - ".SendOtp"
Cohesion: 0.09
Nodes (19): SendPhoneOtpRequest, SendPhoneOtpResponse, CancellationToken, Task, CancellationToken, IFeatureManager, RouteGroupBuilder, Task (+11 more)

### Community 14 - "UserRepresentation"
Cohesion: 0.08
Nodes (24): Dictionary, List, ErrorRepresentation, Error, ErrorMessage, Field, RoleRepresentation, Id (+16 more)

### Community 15 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.12
Nodes (17): AuthorizationHandler, AuthorizationHandlerContext, IAuthorizationRequirement, CancellationToken, ClaimsPrincipal, HttpContext, IFusionCache, IHttpContextAccessor (+9 more)

### Community 16 - ".SendAsync"
Cohesion: 0.22
Nodes (7): CancellationToken, Task, SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, SmsMessage

### Community 17 - ".UseModule"
Cohesion: 0.20
Nodes (7): DashboardContext, IAuthorizationService, IDashboardAsyncAuthorizationFilter, IApplicationBuilder, IOptions, HangfireCustomAuthorizationFilter, Task

### Community 18 - "DomainEvent"
Cohesion: 0.07
Nodes (25): Products.Domain.Products.DomainEvents.v1, AggregateRoot, Events, Id, Version, IReadOnlyCollection, List, DomainEvent (+17 more)

### Community 19 - "AuditableEntity"
Cohesion: 0.10
Nodes (19): IAggregateRoot, Events, Id, Version, IReadOnlyCollection, AuditableEntity, CreatedBy, CreatedOn (+11 more)

### Community 20 - "ApplicationUserId"
Cohesion: 0.12
Nodes (19): ApplicationUserId, IsEmpty, Value, DefaultIdType, CancellationToken, IReadOnlyList, Task, IKeycloakAdminClient (+11 more)

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.14
Nodes (8): ReadOnlySpan, BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position, Stream

### Community 22 - "DeviceRegistration"
Cohesion: 0.12
Nodes (15): DateTimeOffset, DefaultIdType, Guid, DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive (+7 more)

### Community 23 - "KeycloakPermissionPolicyProvider"
Cohesion: 0.27
Nodes (8): AuthorizationOptions, AuthorizationPolicy, DefaultAuthorizationPolicyProvider, IAuthorizationPolicyProvider, ConcurrentDictionary, IOptions, Task, KeycloakPermissionPolicyProvider

### Community 24 - "DeactivateDeviceSessionsRequest"
Cohesion: 0.13
Nodes (14): DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse, IReadOnlyList, CancellationToken, RouteGroupBuilder, Task, Endpoint, CancellationToken (+6 more)

### Community 25 - "NetGsmSmsGateway"
Cohesion: 0.32
Nodes (7): Exception, HttpClient, ILogger, IOptions, JsonSerializerOptions, LoggerMessage, NetGsmSmsGateway

### Community 26 - "IEvent"
Cohesion: 0.11
Nodes (14): DomainEventHandlerBase, CancellationToken, Task, IEventHandler, CancellationToken, Task, IEventHandlerWrapper, CancellationToken (+6 more)

### Community 27 - "RequestResponseBodyLoggingMiddleware"
Cohesion: 0.21
Nodes (7): IDiagnosticContext, HttpContext, IList, IOptions, PathString, RequestDelegate, RequestResponseBodyLoggingMiddleware

### Community 28 - "KeycloakTokenClient"
Cohesion: 0.15
Nodes (18): JsonWebTokenHandler, CancellationToken, Task, IKeycloakTokenClient, DateTimeOffset, KeycloakTokens, CancellationToken, Dictionary (+10 more)

### Community 29 - "Result"
Cohesion: 0.20
Nodes (9): Result, Error, IsFailure, Value, AsyncExtensions, SyncExtensions, Action, Func (+1 more)

### Community 30 - "Product"
Cohesion: 0.07
Nodes (26): ProductTemplate, ProductTemplateId, Store, StoreId, Product, Description, Language, Name (+18 more)

### Community 32 - ".CreateStoreAsync"
Cohesion: 0.14
Nodes (10): Products.Endpoints.Stores, Store, RouteGroupBuilder, Setup, CancellationToken, RouteGroupBuilder, Task, Endpoint (+2 more)

### Community 33 - ".AddProductAsync"
Cohesion: 0.18
Nodes (9): Products.Endpoints.Stores.v1.AddProduct, CancellationToken, ProductTemplate, RouteGroupBuilder, Store, Task, Endpoint, Response (+1 more)

### Community 34 - "KeycloakPermissionClient"
Cohesion: 0.23
Nodes (11): CancellationToken, Dictionary, HttpClient, HttpResponseMessage, ILogger, IOptions, IReadOnlyList, List (+3 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.10
Nodes (20): IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, DateTimeOffset, OutboxMessage (+12 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.09
Nodes (20): CheckRegistrationRateLimitingPolicy, CreateStoreRateLimitingPolicy, RegisterRateLimitingPolicy, SmsRateLimitingPolicy, CustomRateLimitingOptions, CheckRegistration, CreateStore, ExemptPathPrefixes (+12 more)

### Community 37 - "StockReservation"
Cohesion: 0.05
Nodes (37): Inventory.Domain.StockReservations.DomainEvents.v1, DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationCommitConflictDetectedDomainEvent, DateTimeOffset, DefaultIdType, StockReservationId (+29 more)

### Community 38 - ".RefreshToken"
Cohesion: 0.15
Nodes (11): IAM.Endpoints.Tokens.VersionNeutral.Refresh, CancellationToken, RouteGroupBuilder, Task, Endpoint, DateTimeOffset, Response, AccessToken (+3 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.08
Nodes (26): IHostBuilder, OpenTelemetryBuilder, ResourceBuilder, ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics (+18 more)

### Community 40 - "SmsRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, SmsRateLimitingPolicy (+1 more)

### Community 41 - "Request"
Cohesion: 0.18
Nodes (10): Products.Endpoints.Products.v1.My.Search, Request, Description, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name (+2 more)

### Community 42 - ".SaveWithOutboxAsync"
Cohesion: 0.27
Nodes (9): OutboxSaveHelper, CancellationToken, DbContext, Exception, Func, ILogger, LoggerMessage, Task (+1 more)

### Community 43 - "CreateStoreRateLimitingPolicy"
Cohesion: 0.14
Nodes (12): CancellationToken, Func, HttpContext, IOptions, IProblemDetailsService, IResxLocalizer, OnRejectedContext, RateLimitPartition (+4 more)

### Community 44 - "KeycloakOptions"
Cohesion: 0.14
Nodes (14): KeycloakOptions, AttemptTimeoutSeconds, Authority, BaseUrl, DecisionCacheMaxDurationSeconds, Realm, RequireHttpsMetadata, ResourceClientId (+6 more)

### Community 45 - ".BindDeviceAsync"
Cohesion: 0.08
Nodes (26): BindDeviceSessionRequest, BindDeviceSessionResponse, Guid, CancellationToken, Exception, Guid, ILogger, LoggerMessage (+18 more)

### Community 46 - "ProductsModule.cs"
Cohesion: 0.18
Nodes (6): Common.Endpoints.Versioning, Products.Infrastructure.RateLimiting, Products.Endpoints, IAssemblyReference, Policies, RateLimitingConstants

### Community 48 - "IOtpService"
Cohesion: 0.21
Nodes (8): CancellationToken, Task, TimeSpan, IOtpService, OtpVerificationOutcome, InvalidOtp, Success, TooManyAttempts

### Community 50 - "Response"
Cohesion: 0.12
Nodes (16): IAM.Endpoints.Users.VersionNeutral.Search, Request, SearchTerm, RequestValidator, DateOnly, DateTimeOffset, Response, BirthDate (+8 more)

### Community 51 - "Common.Domain.Events"
Cohesion: 0.16
Nodes (10): Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Common.Domain.Events, Inventory.Infrastructure.Persistence.EntityConfigurations, Notifications.Infrastructure.Persistence, Inventory.Infrastructure.Persistence, Common.Infrastructure.EventBus, Common.Infrastructure.Persistence.EntityConfigurations (+2 more)

### Community 52 - "ReCaptchaService"
Cohesion: 0.21
Nodes (10): FormUrlEncodedContent, ReCaptchaResponse, CancellationToken, Exception, HttpClient, ILogger, IOptions, LoggerMessage (+2 more)

### Community 53 - "Inventory.Domain.StockReservations"
Cohesion: 0.09
Nodes (17): Products.Infrastructure.InterModuleRequestHandlers, Inventory.Infrastructure.InterModuleRequestHandlers, Inventory.Endpoints, Inventory.Application.Persistence, Inventory.Domain.StockReservations, Inventory.Domain.StockReservations.Errors, Inventory.Endpoints.StockReservations.v1.Get, Inventory.Endpoints.StockReservations.v1.Reserve (+9 more)

### Community 54 - "Notifications.Application.Sms"
Cohesion: 0.14
Nodes (8): Notifications.Application.Sms, Notifications.Infrastructure.Sms, Inventory.Infrastructure.Gateway, Common.Infrastructure.Resiliency, Notifications.Infrastructure.Sms.NetGsm, Inventory.Application.Gateway, Setup, SmsErrors

### Community 55 - "RegisterRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, RegisterRateLimitingPolicy (+1 more)

### Community 56 - ".AddNetGsm"
Cohesion: 0.18
Nodes (11): ISmsGateway, CancellationToken, ILogger, LoggerMessage, Task, DummySmsGateway, IConfiguration, IFusionCache (+3 more)

### Community 58 - "OutboxMetricsJob"
Cohesion: 0.13
Nodes (14): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxMetricsJob (+6 more)

### Community 59 - ".SearchProductTemplatesAsync"
Cohesion: 0.18
Nodes (10): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Brand (+2 more)

### Community 61 - ".AddProductToMyStoreAsync"
Cohesion: 0.11
Nodes (17): Products.Endpoints.Stores.v1.My.AddProduct, CancellationToken, ProductTemplate, RouteGroupBuilder, Store, Task, Endpoint, ProductTemplateId (+9 more)

### Community 62 - "NotificationsDbContext"
Cohesion: 0.18
Nodes (10): DbContextOptions, DbSet, ILogger, TimeProvider, NotificationsDbContext, DeviceRegistrations, IApplicationBuilder, ILoggerFactory (+2 more)

### Community 63 - "RequestLoggingOptions"
Cohesion: 0.11
Nodes (20): IPostConfigureOptions, RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes (+12 more)

### Community 64 - "Common.InterModuleRequests"
Cohesion: 0.29
Nodes (4): Common.InterModuleRequests, IAssemblyReference, Setup, IServiceCollection

### Community 65 - "Common.Application.Options"
Cohesion: 0.09
Nodes (13): Common.Application.Search, IAM.Infrastructure.Keycloak.Representations, Common.Infrastructure.RateLimiting, IAM.Endpoints, Common.Application.Options, IAM.Infrastructure.Auth, IAM.Infrastructure.RateLimiting, IAM.Infrastructure.Keycloak (+5 more)

### Community 66 - "OutboxModule"
Cohesion: 0.13
Nodes (16): Action, Exception, IApplicationBuilder, IEndpointRouteBuilder, IEnumerable, IHostApplicationLifetime, ILogger, ILoggerFactory (+8 more)

### Community 67 - "Response"
Cohesion: 0.22
Nodes (8): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Brand, Color, Model

### Community 68 - "ValueObject"
Cohesion: 0.25
Nodes (4): Common.Domain, IComparable, ValueObject, IEnumerable

### Community 69 - "V1StoreCreatedDomainEvent"
Cohesion: 0.27
Nodes (8): Products.Application.Stores.DomainEventHandlers.v1, CancellationToken, Task, SimulateSomeBusinessHandler, StoreCreatedIntegrationEventPublishingHandler, V1StoreCreatedDomainEventHandlers, StoreId, V1StoreCreatedDomainEvent

### Community 70 - "IStronglyTypedId"
Cohesion: 0.05
Nodes (32): JsonConverter, StrictDateTimeOffsetJsonConverter, DateTimeOffset, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, StronglyTypedIdListReadOnlyJsonConverter (+24 more)

### Community 71 - "Full-Text Search"
Cohesion: 0.08
Nodes (25): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Add a new language/culture, Add search to a new entity _(Build checklist)_ (+17 more)

### Community 72 - "Response"
Cohesion: 0.20
Nodes (9): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Description, Name, Price (+1 more)

### Community 73 - ".WriteAsync"
Cohesion: 0.33
Nodes (5): Memory, ReadOnlyMemory, CancellationToken, Task, ValueTask

### Community 75 - "ProductId"
Cohesion: 0.09
Nodes (21): Products.Endpoints.Stores.v1.My.RemoveProduct, DefaultIdType, ProductId, Request, Id, RequestValidator, Request, Id (+13 more)

### Community 76 - "Common.Application.ModelBinders"
Cohesion: 0.08
Nodes (22): Inventory.Endpoints.StockReservations.v1.Release, Common.Application.ModelBinders, Products.Endpoints.ProductTemplates.v1.Activate, IModelBinder, ModelBindingContext, StronglyTypedIdBinder, Task, Request (+14 more)

### Community 77 - ".CreateMyStoreAsync"
Cohesion: 0.19
Nodes (9): Products.Endpoints.Stores.v1.My.Create, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator, Response (+1 more)

### Community 78 - "CheckRegistrationRateLimitingPolicy"
Cohesion: 0.25
Nodes (8): IRateLimiterPolicy, CancellationToken, Func, IOptions, OnRejectedContext, ValueTask, CheckRegistrationRateLimitingPolicy, OnRejected

### Community 79 - "IntegrationEventOutbox"
Cohesion: 0.10
Nodes (17): Lock, IIntegrationEventOutbox, IntegrationEventOutbox, IReadOnlyList, List, Setup, IServiceCollection, IntegrationEvent (+9 more)

### Community 80 - "EventDispatcher"
Cohesion: 0.31
Nodes (7): EventDispatcher, ActivitySource, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task

### Community 81 - "AuditableEntityResponse"
Cohesion: 0.12
Nodes (15): AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, DateTimeOffset, RouteGroupBuilder (+7 more)

### Community 82 - ".Failure"
Cohesion: 0.14
Nodes (8): Common.Domain.Extensions, SearchValues, StringExtensions, Success, Func, Task, CancellationToken, Task

### Community 83 - ".EnsureNoMigrationsPending"
Cohesion: 0.30
Nodes (6): AutoMigrateMarker, IAutoMigrateMarker, MigrationGuard, ILogger, IServiceProvider, LoggerMessage

### Community 84 - ".Configure"
Cohesion: 0.13
Nodes (13): AuditableEntityConfiguration, EntityTypeBuilder, StronglyTypedIdValueConverter, DefaultIdType, EntityTypeBuilder, StockReservationConfiguration, EntityTypeBuilder, NpgsqlTsVector (+5 more)

### Community 85 - "ResultTelemetryExtensions"
Cohesion: 0.38
Nodes (4): Activity, ResultTelemetryExtensions, ActivitySource, Task

### Community 86 - "Store"
Cohesion: 0.07
Nodes (24): Products.Domain.Stores.DomainEvents.v1, StoreId, V1StoreAddressUpdatedDomainEvent, StoreId, V1StoreDeactivatedDomainEvent, StoreId, V1StoreDeactivatedWithStockOnHandDomainEvent, StoreId (+16 more)

### Community 87 - "PushOptions"
Cohesion: 0.19
Nodes (13): PushOptions, Provider, SendTimeoutSeconds, ServiceAccount, Templates, PushOptionsValidator, PushProvider, Dummy (+5 more)

### Community 89 - "Program.cs"
Cohesion: 0.22
Nodes (6): ConfigurationManager, Host, Host.Configurations, Setup, Program, WebApplicationBuilder

### Community 90 - "IInterModuleRequestClient"
Cohesion: 0.12
Nodes (18): IClientFactory, IInterModuleRequestClient, CancellationToken, Task, MassTransitInterModuleRequestClient, CancellationToken, IOptions, Task (+10 more)

### Community 91 - "Setup"
Cohesion: 0.13
Nodes (9): Common.Infrastructure.Modules, Common.Infrastructure.Localization, Host.Middlewares, Host.Infrastructure, LoggerConfiguration, LoggerMinimumLevelConfiguration, Setup, IEnumerable (+1 more)

### Community 92 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.06
Nodes (21): Products.Infrastructure.Persistence.Seeding, Common.Domain.StronglyTypedIds, Common.Application.Persistence, Products.Endpoints.Products.v1.My.Get, Products.Endpoints.ProductTemplates.v1.Search, Products.Endpoints.Products.v1.Search, Notifications.Domain.Devices, Products.Domain.Products (+13 more)

### Community 93 - "Endpoint"
Cohesion: 0.18
Nodes (8): IAM.Endpoints.Captcha.VersionNeutral, IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, RouteGroupBuilder, Endpoint, Response, ClientKey, RouteGroupBuilder, Setup

### Community 94 - "NotificationsModule.cs"
Cohesion: 0.16
Nodes (6): Notifications.Infrastructure.Devices, Notifications.Application.Push, Notifications.Infrastructure.Push, Notifications.Infrastructure.Push.Firebase, Notifications.Infrastructure, IAssemblyReference

### Community 95 - ".SearchMyProductsAsync"
Cohesion: 0.17
Nodes (11): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Description (+3 more)

### Community 96 - "DatabaseSeederOrchestrator"
Cohesion: 0.12
Nodes (17): BackgroundService, DatabaseSeederOrchestrator, CancellationToken, Exception, IHostApplicationLifetime, ILogger, IServiceScopeFactory, LoggerMessage (+9 more)

### Community 97 - "AuditLogRetentionJobRegistrar"
Cohesion: 0.16
Nodes (10): Common.Infrastructure.Persistence.AuditLog, IHostedService, AuditLogRetentionJobRegistrar, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task (+2 more)

### Community 98 - ".SearchStoreProductsAsync"
Cohesion: 0.17
Nodes (11): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Description (+3 more)

### Community 99 - "KeycloakPermission"
Cohesion: 0.22
Nodes (3): KeycloakPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder

### Community 100 - "OutboxDbContext"
Cohesion: 0.14
Nodes (13): DbContext, IOutboxDbContext, OutboxMessages, CancellationToken, DbSet, Task, IConfiguration, IServiceCollection (+5 more)

### Community 101 - "ResiliencyOptions"
Cohesion: 0.11
Nodes (18): HttpStandardResilienceOptions, IHttpClientBuilder, ResiliencyOptions, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds (+10 more)

### Community 102 - "HttpContextTargetingContextAccessor"
Cohesion: 0.17
Nodes (9): Common.Infrastructure.FeatureManagement, ITargetingContextAccessor, HttpContextTargetingContextAccessor, IHttpContextAccessor, ValueTask, Setup, IConfiguration, IServiceCollection (+1 more)

### Community 103 - "Common.Application.BackgroundJobs"
Cohesion: 0.10
Nodes (14): Common.Application.BackgroundJobs, BackgroundJobs.Telemetry, BackgroundJobs, IServerFilter, PerformedContext, PerformingContext, JobMetricsFilter, BackgroundJobsTelemetry (+6 more)

### Community 104 - "CreateStockLevelOnProductCreatedHandler"
Cohesion: 0.19
Nodes (11): ProductCreatedIntegrationEvent, StoreCreatedIntegrationEvent, DefaultIdType, CancellationToken, DefaultIdType, IFusionCache, ILogger, IOptions (+3 more)

### Community 105 - "Uri"
Cohesion: 0.15
Nodes (11): CancellationToken, Task, IServiceAccountTokenProvider, HttpClient, IOptions, TimeProvider, ServiceAccountTokenProvider, IOptions (+3 more)

### Community 106 - "OutboxModule.cs"
Cohesion: 0.13
Nodes (9): Outbox, Common.Infrastructure.Persistence.Outbox, Outbox.Persistence, Common.Application.Persistence.Outbox, Outbox.Telemetry, IEntityTypeConfiguration, ModelBuilder, EntityTypeBuilder (+1 more)

### Community 107 - ".ReleaseStockReservationAsync"
Cohesion: 0.12
Nodes (14): CancellationToken, Task, IWarehouseGateway, CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint (+6 more)

### Community 108 - "StockLevel"
Cohesion: 0.11
Nodes (12): Inventory.Domain.StockLevels.DomainEvents.v1, StronglyTypedIdHelper, DefaultIdType, StockLevelId, V1StockLevelCreatedDomainEvent, DefaultIdType, StockLevel, ProductId (+4 more)

### Community 109 - ".IsRegisteredAsync"
Cohesion: 0.18
Nodes (10): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, PhoneNumber, RequestValidator (+2 more)

### Community 110 - "ProductsDbContext"
Cohesion: 0.13
Nodes (14): DbContextOptions, DbSet, ILogger, ProductTemplate, Store, TimeProvider, ProductsDbContext, Products (+6 more)

### Community 111 - "BackgroundJobsService"
Cohesion: 0.23
Nodes (8): IBackgroundJobClientV2, BackgroundJobsService, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 112 - "OutboxCleanupJob"
Cohesion: 0.27
Nodes (8): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxCleanupJob

### Community 113 - "CaptchaOptions"
Cohesion: 0.16
Nodes (13): CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey, Provider, ScoreThreshold, SecretKey (+5 more)

### Community 114 - "Response"
Cohesion: 0.11
Nodes (17): IAM.Endpoints.Users.VersionNeutral.Me.Get, RouteGroupBuilder, Endpoint, DateOnly, DateTimeOffset, IReadOnlyCollection, Response, BirthDate (+9 more)

### Community 115 - "IBackgroundJobs"
Cohesion: 0.26
Nodes (7): IBackgroundJobs, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 116 - ".WriteProblemAsync"
Cohesion: 0.20
Nodes (10): IAllowAnonymous, IConfigureNamedOptions, HttpContext, HttpStatusCode, IOptions, IProblemDetailsService, IResxLocalizer, JwtBearerOptions (+2 more)

### Community 117 - "Response"
Cohesion: 0.10
Nodes (19): ReservationStatus, Active, Committed, Expired, Released, RequiresReconciliation, CancellationToken, RouteGroupBuilder (+11 more)

### Community 118 - "IModule"
Cohesion: 0.12
Nodes (13): IModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority, Action, IApplicationBuilder (+5 more)

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
Cohesion: 0.40
Nodes (4): Action, Func, IFeatureManager, Task

### Community 126 - "GlobalExceptionHandlingMiddleware"
Cohesion: 0.18
Nodes (11): IApplicationBuilder, IServiceCollection, Exception, HttpContext, ILogger, IProblemDetailsService, IResxLocalizer, LoggerMessage (+3 more)

### Community 127 - "BaseDbContext"
Cohesion: 0.22
Nodes (8): BaseDbContext, AuditLog, CancellationToken, DbContextOptions, DbSet, ILogger, Task, TimeProvider

### Community 128 - "AuditLogEntry"
Cohesion: 0.13
Nodes (12): AuditLogEntry, AggregateId, AggregateType, Event, EventType, Version, DefaultIdType, AuditLogEntryConfiguration (+4 more)

### Community 129 - "Response"
Cohesion: 0.15
Nodes (10): Products.Endpoints.Products, RouteGroupBuilder, Setup, RouteGroupBuilder, Response, AvailableQuantity, Description, Name (+2 more)

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.48
Nodes (7): Observability (OpenTelemetry), docker-compose.yml (Base Stack), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), Postgres Service (mm.postgres), RabbitMQ Service (mm.rabbitmq), Redis Service (mm.redis)

### Community 131 - ".AddInfrastructure"
Cohesion: 0.29
Nodes (7): HostOptions, JsonOptions, Assembly, IConfiguration, IResxLocalizer, IServiceCollection, IWebHostEnvironment

### Community 132 - ".SaveChangesAsync"
Cohesion: 0.13
Nodes (10): CancellationToken, Task, CancellationToken, RouteGroupBuilder, Task, Endpoint, CancellationToken, RouteGroupBuilder (+2 more)

### Community 134 - "v1/RemoveProduct/Request.cs"
Cohesion: 0.29
Nodes (7): Products.Endpoints.Stores.v1.RemoveProduct, ProductId, StoreId, Request, Id, ProductId, RequestValidator

### Community 135 - ".ListSessions"
Cohesion: 0.12
Nodes (15): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, CancellationToken, IReadOnlyCollection, RouteGroupBuilder, Task, Endpoint, DateTimeOffset, Response (+7 more)

### Community 136 - ".SingleAsResult"
Cohesion: 0.18
Nodes (10): CollectionExtensions, Func, ICollection, IEnumerable, PersistenceQueryableExtensions, CancellationToken, Expression, Func (+2 more)

### Community 137 - ".WriteTooManyRequestsToResponse"
Cohesion: 0.13
Nodes (14): PartitionedRateLimiter, CancellationToken, Func, HttpContext, IConfiguration, IProblemDetailsService, IReadOnlyList, IResxLocalizer (+6 more)

### Community 138 - "ResultToResponseTransformer"
Cohesion: 0.26
Nodes (8): Common.Application.EndpointFilters, IEndpointFilter, ResultToCreatedResponseTransformer, ResultToResponseTransformer, IServiceProvider, IWebHostEnvironment, RouteHandlerBuilderExtensions, RouteHandlerBuilder

### Community 139 - "ProductTemplate"
Cohesion: 0.15
Nodes (11): IReadOnlyList, List, ProductTemplate, Brand, Color, IsActive, Model, Products (+3 more)

### Community 140 - "Response"
Cohesion: 0.13
Nodes (11): IAM.Endpoints.Users.VersionNeutral.SelfRegister, IAM.Endpoints.Users.VersionNeutral, RouteGroupBuilder, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken (+3 more)

### Community 141 - ".GetMeAsync"
Cohesion: 0.24
Nodes (7): CancellationToken, IReadOnlyList, Task, IKeycloakPermissionClient, CancellationToken, HttpContext, Task

### Community 142 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 143 - "IDbContext"
Cohesion: 0.25
Nodes (7): DatabaseFacade, EntityEntry, IDbContext, AuditLog, ChangeTracker, Database, DbSet

### Community 144 - "Response"
Cohesion: 0.11
Nodes (17): IAM.Endpoints.Users.VersionNeutral.Get, CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, DateTimeOffset, Response (+9 more)

### Community 145 - "EnrichLogsWithUserInfoMiddleware"
Cohesion: 0.12
Nodes (12): IAuthenticationSchemeProvider, IMiddleware, IApplicationBuilder, HttpContext, RequestDelegate, Task, EnrichLogsWithUserInfoMiddleware, HttpContext (+4 more)

### Community 146 - "IAM.Endpoints.Otp.VersionNeutral"
Cohesion: 0.40
Nodes (3): IAM.Endpoints.Otp.VersionNeutral, RouteGroupBuilder, Setup

### Community 147 - ".AddServices"
Cohesion: 0.12
Nodes (15): RecurringJobOptions, IRecurringBackgroundJobs, Action, Expression, Func, Task, IConfiguration, IServiceCollection (+7 more)

### Community 148 - ".AddModules"
Cohesion: 0.11
Nodes (15): LoadAll, ModuleRegistry, Names, Type, Assembly, Exception, IApplicationBuilder, IConfiguration (+7 more)

### Community 149 - "Response"
Cohesion: 0.18
Nodes (9): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, RouteGroupBuilder, Endpoint, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken (+1 more)

### Community 150 - "Common.Application.FeatureManagement"
Cohesion: 0.12
Nodes (11): Common.Application.FeatureManagement, IAM.Domain.Captcha, IAM.Infrastructure.Captcha.Services, IAM.Application.Captcha.Services, IAM.Infrastructure.Captcha, FeatureFlagResultExtensions, Checkout, FeatureFlags (+3 more)

### Community 151 - "OutboxOptions"
Cohesion: 0.11
Nodes (20): OutboxCleanupSettings, BatchSize, CronSchedule, Enabled, RetentionDays, OutboxCleanupSettingsValidator, OutboxOptions, BaseBackoffSeconds (+12 more)

### Community 152 - ".SearchStoresAsync"
Cohesion: 0.16
Nodes (11): ISearchLanguageResolver, UniversalConfig, SearchLanguageResolver, UniversalConfig, IOptions, Setup, IServiceCollection, CancellationToken (+3 more)

### Community 153 - "SwaggerDefaultValues"
Cohesion: 0.33
Nodes (4): JsonValue, OpenApiOperation, OperationFilterContext, SwaggerDefaultValues

### Community 154 - "CustomValidator"
Cohesion: 0.09
Nodes (26): AbstractValidator, Inventory.Endpoints.StockReservations.v1.Commit, Products.Endpoints.Products.v1.Update, CustomValidator, RequestBody, Request, Body, Id (+18 more)

### Community 155 - "InterModuleRequestHandler"
Cohesion: 0.19
Nodes (8): IConsumer, IInterModuleRequestHandler, CancellationToken, Task, InterModuleRequestHandler, CancellationToken, ConsumeContext, Task

### Community 156 - "PaginationRequestValidator"
Cohesion: 0.29
Nodes (6): Products.Endpoints.Products.v1.AuditLog, PaginationRequestValidator, Request, Id, RequestValidator, RequestValidator

### Community 157 - "IntegrationEventHandlerBase"
Cohesion: 0.24
Nodes (11): IntegrationEventHandlerBase, MaxEventAge, CancellationToken, ConsumeContext, DefaultIdType, IFusionCache, ILogger, IOptions (+3 more)

### Community 158 - ".FixedWindow"
Cohesion: 0.22
Nodes (7): RateLimitPartitions, HttpContext, IConnectionMultiplexer, ILoggerFactory, RateLimitPartition, HttpContext, RateLimitPartition

### Community 159 - "IInventoryDbContext"
Cohesion: 0.13
Nodes (15): DbSet, IInventoryDbContext, StockLevels, StockReservations, DbContextOptions, DbSet, ILogger, TimeProvider (+7 more)

### Community 160 - "OtpServiceBase"
Cohesion: 0.11
Nodes (14): Common.Application.Caching, Notifications.Infrastructure.Otp, OtpCacheEntry, DateTimeOffset, IFusionCache, DummyOtpService, CancellationToken, IFusionCache (+6 more)

### Community 161 - ".ReserveSeriesAsync"
Cohesion: 0.14
Nodes (12): Inventory.Endpoints.StockReservations.v1.ReserveSeries, CancellationToken, IOptions, List, RequestBody, RouteGroupBuilder, Task, TimeProvider (+4 more)

### Community 162 - "UtcDateTimeOffsetConverter"
Cohesion: 0.22
Nodes (6): DateTimeOffset, ModelConfigurationBuilder, UtcDateTimeOffsetConverter, DateTimeOffset, DateTimeOffset, ModelConfigurationBuilder

### Community 164 - "Request"
Cohesion: 0.08
Nodes (23): Notifications.Infrastructure.Devices.UpdateCurrentPushToken, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, Common.Domain.Devices, DeviceSessionConstants, Request, Id, RequestValidator, Guid (+15 more)

### Community 165 - "CurrentUser"
Cohesion: 0.12
Nodes (14): Common.Infrastructure.Auth.Services, Common.Infrastructure.Auth, CurrentUser, Id, IdAsString, IsAuthenticated, Principal, Roles (+6 more)

### Community 166 - "Response"
Cohesion: 0.14
Nodes (11): IAM.Endpoints.Tokens.VersionNeutral, RouteGroupBuilder, Endpoint, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken (+3 more)

### Community 167 - "PaginationRequest"
Cohesion: 0.15
Nodes (11): PaginationRequest, PageNumber, PageSize, Skip, Take, PaginationQueryableExtensions, CancellationToken, Expression (+3 more)

### Community 168 - "ICaptchaService"
Cohesion: 0.14
Nodes (10): ICaptchaService, CancellationToken, IFusionCache, IOptions, Task, CachedCaptchaService, DummyCaptchaService, IConfiguration (+2 more)

### Community 169 - ".Get"
Cohesion: 0.50
Nodes (3): Action, IEnumerable, RateLimiterOptions

### Community 171 - ".UpdateStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 172 - "ResxLocalizationOptions"
Cohesion: 0.25
Nodes (7): ResxLocalizationOptions, DefaultCulture, SupportedCultures, ResxLocalizationOptionsValidator, ICollection, IApplicationBuilder, IOptions

### Community 173 - "RequestBody"
Cohesion: 0.29
Nodes (7): ProductTemplateId, RequestBody, Description, Name, Price, ProductTemplateId, Quantity

### Community 174 - "INotificationsDbContext"
Cohesion: 0.12
Nodes (20): IInterModuleRequest, DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, IReadOnlyList, SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest (+12 more)

### Community 175 - "AuditLogDto"
Cohesion: 0.12
Nodes (14): Products.Endpoints.Stores.v1.My.AuditLog, JsonElement, AuditLogDto, DateTimeOffset, CancellationToken, RouteGroupBuilder, Task, Endpoint (+6 more)

### Community 176 - "PaginationResponse"
Cohesion: 0.15
Nodes (11): PaginationResponse, HasNext, HasPrevious, NextPageNumber, PreviousPageNumber, TotalPages, ICollection, CancellationToken (+3 more)

### Community 177 - ".AddAuthInfrastructure"
Cohesion: 0.25
Nodes (6): IAuthorizationHandler, IAuthorizationPolicyProvider, IConfigureOptions, IServiceCollection, JwtBearerOptions, Setup

### Community 178 - "OtpOptions"
Cohesion: 0.25
Nodes (7): OtpOptions, ExpirationInMinutes, Length, OtpOptionsValidator, IFusionCache, IOptions, OtpService

### Community 179 - "CachingOptions"
Cohesion: 0.11
Nodes (21): CachingEntryDefaults, Duration, FactoryHardTimeout, FactorySoftTimeout, FailSafeMaxDuration, FailSafeThrottleDuration, CachingOptions, AllowInMemoryOnlyInProduction (+13 more)

### Community 180 - "SmsOptions"
Cohesion: 0.10
Nodes (21): SmsOptions, AppName, AttemptTimeoutSeconds, BaseUrl, MaxPerDay, MaxPerPhoneNumberPerDay, MaxRetryAttempts, MsgHeader (+13 more)

### Community 181 - "RabbitMqOptions"
Cohesion: 0.12
Nodes (15): RabbitMqOptions, Host, Password, Port, RetryIntervalDeltaMs, RetryLimit, RetryMaxIntervalMs, RetryMinIntervalMs (+7 more)

### Community 182 - "IAM.Domain"
Cohesion: 0.40
Nodes (3): IAM.Domain, Constants, IAssemblyReference

### Community 183 - "SendRequestBody"
Cohesion: 0.25
Nodes (8): SendMessageBody, IReadOnlyList, SendRequestBody, AppName, Encoding, IysFilter, Messages, MsgHeader

### Community 184 - "RequestBody"
Cohesion: 0.15
Nodes (14): DateTimeOffset, DefaultIdType, ICollection, RequestBody, FirstDeadline, IntervalDays, OccurrenceCount, ProductId (+6 more)

### Community 185 - "IamModule"
Cohesion: 0.12
Nodes (13): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, IamModule (+5 more)

### Community 186 - "Request"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Stores.v1.Update, RequestBody, Request, Body, Id, RequestBody, Address, Description (+3 more)

### Community 187 - "ReverseProxyOptions"
Cohesion: 0.18
Nodes (9): ForwardedHeadersOptions, ReverseProxyOptions, ForwardLimit, IsEnabled, TrustedNetworks, ReverseProxyOptionsValidator, IReadOnlyList, IConfiguration (+1 more)

### Community 188 - "BoundedCaptureStream"
Cohesion: 0.14
Nodes (8): HttpResponse, SeekOrigin, BoundedCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 190 - "ServiceAccountTokenCache"
Cohesion: 0.11
Nodes (11): KeycloakPaths, CancellationToken, DateTimeOffset, SemaphoreSlim, Task, TimeSpan, ServiceAccountTokenCache, AccessToken (+3 more)

### Community 191 - "Split-Deployment PoC"
Cohesion: 0.22
Nodes (8): Concurrent safety, Cross-process call path, Files added by this PoC, How it works, How to run, Split-Deployment PoC, What this proves, README (Boilerplate Overview)

### Community 192 - "ProblemDetails"
Cohesion: 0.23
Nodes (8): ProblemDetails, ResxLocalizer, EndpointFilterDelegate, EndpointFilterInvocationContext, IStringLocalizer, ValueTask, ProblemDetailsExtensions, ICollection

### Community 193 - "Request"
Cohesion: 0.18
Nodes (11): StoreId, Request, Description, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name (+3 more)

### Community 194 - "CorsOptions"
Cohesion: 0.25
Nodes (8): CorsOptions, AllowCredentials, AllowedHeaders, AllowedMethods, AllowedOrigins, MaxAgeInSeconds, CorsOptionsValidator, IReadOnlyList

### Community 195 - "InventoryOptions"
Cohesion: 0.17
Nodes (12): InventoryOptions, MaxOccurrencesPerSeries, SafetyBufferQuantity, StockReservationExpirySweepBatchSize, StockReservationExpirySweepCron, WarehouseGatewayBaseUrl, WarehouseGatewayProvider, WebhookSharedSecret (+4 more)

### Community 196 - "TokenEndpointRepresentations.cs"
Cohesion: 0.20
Nodes (9): List, DecisionRepresentation, Result, PermissionRepresentation, ResourceName, Scopes, TokenErrorRepresentation, Error (+1 more)

### Community 197 - "DummyPushGateway"
Cohesion: 0.38
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, DummyPushGateway

### Community 198 - "InventoryModule"
Cohesion: 0.15
Nodes (10): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, InventoryModule, ActivitySourceNames, MeterNames (+2 more)

### Community 199 - "OpenApiOptions"
Cohesion: 0.22
Nodes (9): OpenApiOptions, ContactEmail, ContactName, Description, EnableSwagger, LicenseName, LicenseUrl, Title (+1 more)

### Community 200 - "KeyValuePair"
Cohesion: 0.23
Nodes (6): KeyValuePair, ActivitySource, Counter, Meter, NotificationsTelemetry, UpDownCounter

### Community 201 - "KeycloakScopes"
Cohesion: 0.20
Nodes (9): Devices, Hangfire, KeycloakScopes, Products, ProductTemplates, Sessions, StockReservations, Stores (+1 more)

### Community 202 - "Request"
Cohesion: 0.20
Nodes (10): IAM.Endpoints.Tokens.VersionNeutral.Create, Guid, Request, ClientId, DeviceId, DeviceName, Otp, PhoneNumber (+2 more)

### Community 203 - "IProductsDbContext"
Cohesion: 0.12
Nodes (13): DbSet, IProductsDbContext, Products, ProductTemplates, Stores, CancellationToken, RouteGroupBuilder, Task (+5 more)

### Community 210 - "ConfigureSwaggerOptions"
Cohesion: 0.28
Nodes (7): ApiVersionDescription, IApiVersionDescriptionProvider, IConfigureOptions, OpenApiInfo, IOptions, SwaggerGenOptions, ConfigureSwaggerOptions

### Community 211 - "NotificationsModule"
Cohesion: 0.15
Nodes (10): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, NotificationsModule, ActivitySourceNames, MeterNames (+2 more)

### Community 212 - ".AddNotificationsSignalR"
Cohesion: 0.17
Nodes (9): HubConnectionContext, IUserIdProvider, RedisOptions, IConfiguration, IConfigureOptions, IConnectionMultiplexer, IServiceCollection, IUserIdProvider (+1 more)

### Community 213 - "ProductsTelemetry"
Cohesion: 0.40
Nodes (4): ActivitySource, Counter, Meter, ProductsTelemetry

### Community 214 - ".SendAsync"
Cohesion: 0.20
Nodes (7): SendRequestBody, SendResponseBody, CancellationToken, Task, SendMessageBody, Msg, No

### Community 215 - "RedisOtpService"
Cohesion: 0.28
Nodes (6): CancellationToken, IConnectionMultiplexer, IOptions, Task, TimeSpan, RedisOtpService

### Community 216 - "ProductTemplateId"
Cohesion: 0.25
Nodes (6): ProductTemplateId, DefaultIdType, ProductTemplateId, CancellationToken, List, Task

### Community 217 - "Request"
Cohesion: 0.25
Nodes (7): Constants, Request, Brand, Color, Model, SearchTerm, RequestValidator

### Community 218 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 219 - ".AddPushServices"
Cohesion: 0.25
Nodes (6): CancellationToken, Task, IPushGateway, IConfiguration, IServiceCollection, Setup

### Community 220 - "Seeder"
Cohesion: 0.15
Nodes (14): CancellationToken, ILogger, LoggerMessage, ProductsDbContext, Task, CancellationToken, List, ProductTemplateId (+6 more)

### Community 221 - "Keycloak realm as code"
Cohesion: 0.25
Nodes (7): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Secrets, Seed users (dev / test only)

### Community 222 - ".CreateProductTemplateAsync"
Cohesion: 0.10
Nodes (16): Products.Endpoints.ProductTemplates, Products.Endpoints.ProductTemplates.v1.Create, ProductTemplate, RouteGroupBuilder, Setup, CancellationToken, RouteGroupBuilder, Task (+8 more)

### Community 223 - "ReCaptchaResponse"
Cohesion: 0.25
Nodes (7): DateTime, ReCaptchaResponse, ChallengeTs, ErrorCodes, Hostname, Score, Success

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
Cohesion: 0.22
Nodes (8): ICoreModule, BackgroundJobsModule, ActivitySourceNames, MeterNames, Name, StartupPriority, IEndpointRouteBuilder, IEnumerable

### Community 228 - "Common.Application.Validation"
Cohesion: 0.11
Nodes (17): Common.Application.Validation, DatabaseOptions, ConnectionString, DatabaseOptionsValidator, InterModuleRequestOptions, TimeoutSeconds, InterModuleRequestOptionsValidator, SignalROptions (+9 more)

### Community 229 - ".AddCommonOptions"
Cohesion: 0.20
Nodes (6): Setup, IConfiguration, IHostEnvironment, IServiceCollection, ValidationContextExtensions, ValidationContext

### Community 230 - "Request"
Cohesion: 0.18
Nodes (11): DateTimeOffset, DefaultIdType, RequestBody, Request, Body, RequestBody, ProductId, Quantity (+3 more)

### Community 231 - ".UpdateMyStoreAsync"
Cohesion: 0.17
Nodes (10): Products.Endpoints.Stores.v1.My.Update, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, Address, Description (+2 more)

### Community 232 - "FirebaseServiceAccountOptions"
Cohesion: 0.29
Nodes (7): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri

### Community 233 - "StockReservationExpirySweepService"
Cohesion: 0.24
Nodes (9): CancellationToken, Exception, Guid, ILogger, IOptions, LoggerMessage, Task, TimeProvider (+1 more)

### Community 234 - "Setup"
Cohesion: 0.29
Nodes (4): IApplicationBuilder, IWebHostEnvironment, Type, Setup

### Community 235 - ".GetStoreAuditLogAsync"
Cohesion: 0.20
Nodes (8): Products.Endpoints.Stores.v1.AuditLog, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, Id, RequestValidator

### Community 236 - ".UpdateCurrentPushToken"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 237 - "IDatabaseSeeder"
Cohesion: 0.17
Nodes (9): IDatabaseSeeder, Priority, CancellationToken, Task, CancellationToken, IServiceScopeFactory, Task, ProductsDatabaseSeeder (+1 more)

### Community 238 - "NotificationsHub"
Cohesion: 0.31
Nodes (6): Hub, Exception, ILogger, LoggerMessage, Task, NotificationsHub

### Community 239 - ".TryReadFromJsonAsync"
Cohesion: 0.33
Nodes (4): HttpContent, CancellationToken, Task, HttpContentJsonExtensions

### Community 240 - "TokenResponseRepresentation"
Cohesion: 0.33
Nodes (6): TokenResponseRepresentation, AccessToken, ExpiresIn, RefreshExpiresIn, RefreshToken, SessionState

### Community 241 - "IAM.Endpoints.Common.Validations"
Cohesion: 0.12
Nodes (15): IAM.Endpoints.Otp.VersionNeutral.SendForLogin, IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, IAM.Endpoints.Common.Validations, IRuleBuilder, IRuleBuilderOptions, IResxLocalizer, CommonValidations, Request (+7 more)

### Community 242 - ".AddCustomSwagger"
Cohesion: 0.20
Nodes (7): IOpenApiSchema, ISchemaFilter, SchemaFilterContext, IConfigureOptions, IServiceCollection, SwaggerGenOptions, StronglyTypedIdSchemaFilter

### Community 243 - ".RemoveProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 244 - "HttpWarehouseGateway"
Cohesion: 0.31
Nodes (8): ReleaseResponseBody, CancellationToken, HttpClient, HttpResponseMessage, Task, HttpWarehouseGateway, ReleaseRequestBody, ReleaseResponseBody

### Community 245 - "Host.Swagger"
Cohesion: 0.22
Nodes (5): Host.Swagger, IOperationFilter, OpenApiOperation, OperationFilterContext, RemoveDefaultResponseSchemaFilter

### Community 246 - "StoreId"
Cohesion: 0.40
Nodes (3): StoreId, DefaultIdType, StoreId

### Community 247 - "StockReservationExpirySweepJobRegistrar"
Cohesion: 0.31
Nodes (7): CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task, StockReservationExpirySweepJobRegistrar

### Community 248 - ".UpdateMyProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 249 - "Common.Application.EventBus"
Cohesion: 0.27
Nodes (5): Inventory.Application.IntegrationEventHandlers, Common.IntegrationEvents, Products.Application.Products.DomainEventHandlers.v1, Common.Application.EventBus, V1ProductCreatedDomainEventHandlers

### Community 250 - "Request"
Cohesion: 0.33
Nodes (6): Request, Address, Description, Name, OwnerId, RequestValidator

### Community 251 - "Common.Application.JsonConverters"
Cohesion: 0.22
Nodes (6): Common.Application.JsonConverters, DomainEventConverter, JsonSerializerOptions, IntegrationEventConverter, JsonSerializerOptions, ValueConverter

### Community 252 - "ICurrentUser"
Cohesion: 0.12
Nodes (12): ICurrentUser, Id, IdAsString, Roles, SessionId, ICollection, HttpContextExtensions, HttpContext (+4 more)

### Community 260 - ".SendCoreAsync"
Cohesion: 0.27
Nodes (7): IReadOnlyDictionary, IReadOnlyList, PushMessage, CancellationToken, IEnumerable, IReadOnlyList, Task

### Community 261 - "TokenCreateRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenCreateRateLimitingPolicy (+1 more)

### Community 262 - "Request"
Cohesion: 0.13
Nodes (14): IAM.Domain.Users, Constants, Guid, Request, ClientId, DeviceId, DeviceName, Email (+6 more)

### Community 265 - "AuditLogOptions"
Cohesion: 0.50
Nodes (4): AuditLogOptions, PurgeBatchSize, RetentionDays, AuditLogOptionsValidator

### Community 267 - "SecurityHeadersOptions"
Cohesion: 0.50
Nodes (4): SecurityHeadersOptions, Headers, SecurityHeadersOptionsValidator, Dictionary

### Community 268 - "LogProductCatalogChangeHandler"
Cohesion: 0.24
Nodes (8): CancellationToken, DefaultIdType, IFusionCache, ILogger, IOptions, LoggerMessage, Task, LogProductCatalogChangeHandler

### Community 269 - "Request"
Cohesion: 0.25
Nodes (7): Products.Endpoints.Stores.v1.Search, Request, Address, Description, Name, SearchTerm, RequestValidator

### Community 270 - "DefaultResponsesOperationFilter"
Cohesion: 0.70
Nodes (3): OpenApiOperation, OperationFilterContext, DefaultResponsesOperationFilter

### Community 271 - "Notifications.Application.Hubs"
Cohesion: 0.28
Nodes (3): Notifications.Infrastructure.Hubs, Notifications.Application.Hubs, Setup

### Community 272 - "RequireFeatureFilter"
Cohesion: 0.22
Nodes (6): RequireFeatureFilter, ActivitySource, Counter, Meter, RouteHandlerBuilderExtensions, RouteHandlerBuilder

### Community 273 - "FixedWindow"
Cohesion: 0.29
Nodes (7): CustomRateLimitingOptionsValidator, FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit, FixedWindowValidator

### Community 274 - "GetProductRequest"
Cohesion: 0.39
Nodes (6): GetProductRequest, GetProductResponse, DefaultIdType, CancellationToken, Task, GetProductRequestHandler

### Community 275 - ".ReserveStockAsync"
Cohesion: 0.29
Nodes (6): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Id

### Community 276 - "INotificationDispatcher"
Cohesion: 0.46
Nodes (4): CancellationToken, IReadOnlyList, Task, INotificationDispatcher

### Community 277 - "SendResponseBody"
Cohesion: 0.50
Nodes (4): SendResponseBody, Code, Description, JobId

### Community 278 - "ThrottledSmsGateway"
Cohesion: 0.29
Nodes (5): CancellationToken, IFusionCache, IOptions, Task, ThrottledSmsGateway

### Community 279 - ".AddCommonCaching"
Cohesion: 0.29
Nodes (5): Common.Infrastructure.Caching, Setup, IConfiguration, IConnectionMultiplexer, IServiceCollection

### Community 280 - ".GetAuditLogAsync"
Cohesion: 0.38
Nodes (5): DbContextExtensions, CancellationToken, DbSet, JsonSerializerOptions, Task

### Community 281 - ".CommitStockReservationAsync"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 282 - "Setup"
Cohesion: 0.33
Nodes (4): ApiVersionSet, Setup, IEndpointRouteBuilder, IServiceCollection

### Community 283 - ".InvokeAsync"
Cohesion: 0.33
Nodes (5): IFeatureManagerSnapshot, EndpointFilterDelegate, EndpointFilterInvocationContext, IResxLocalizer, ValueTask

### Community 284 - "ProblemDetailsContext"
Cohesion: 0.33
Nodes (4): ProblemDetailsContext, ProblemDetailsServiceExtensions, IProblemDetailsService, Task

### Community 285 - "Products.Endpoints.Probe"
Cohesion: 0.40
Nodes (3): Products.Endpoints.Probe, RouteGroupBuilder, Setup

### Community 286 - "Stores/v1/Deactivate/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Stores.v1.Deactivate, Request, Id, RequestValidator

### Community 287 - "ProductTemplates/v1/Deactivate/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.ProductTemplates.v1.Deactivate, Request, Id, RequestValidator

### Community 288 - "Inventory.Endpoints.StockReservations"
Cohesion: 0.40
Nodes (3): Inventory.Endpoints.StockReservations, RouteGroupBuilder, Setup

### Community 289 - "ModulesOptions"
Cohesion: 0.50
Nodes (4): ModulesOptions, EnabledModules, ModulesOptionsValidator, IReadOnlyList

## Knowledge Gaps
- **822 isolated node(s):** `Id`, `IdAsString`, `Roles`, `SessionId`, `JwtClaimNames` (+817 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1880 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **89 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `Common.InterModuleRequests.Contracts`, `DeviceRegistryReconciliationService`, `Request`, `ApplyAuditingInterceptor`, `AuditLogOptions`, `SecurityHeadersOptions`, `Common.Domain.ResultMonad`, `Notifications.Application.Hubs`, `FixedWindow`, `EnrichLogsWithUserInfoMiddleware`, `Common.Application.FeatureManagement`, `OutboxOptions`, `.AddCommonCaching`, `OtpServiceBase`, `ModulesOptions`, `Request`, `ObservabilityOptions`, `KeycloakOptions`, `ResxLocalizationOptions`, `ProductsModule.cs`, `OtpOptions`, `CachingOptions`, `SmsOptions`, `RabbitMqOptions`, `Notifications.Application.Sms`, `Inventory.Domain.StockReservations`, `ReverseProxyOptions`, `RequestLoggingOptions`, `CorsOptions`, `InventoryOptions`, `OpenApiOptions`, `Request`, `.Configure`, `PushOptions`, `Program.cs`, `IInterModuleRequestClient`, `Setup`, `Common.Domain.StronglyTypedIds`, `NotificationsModule.cs`, `BackgroundJobsOptions`, `AuditLogRetentionJobRegistrar`, `Common.Application.Validation`, `ResiliencyOptions`, `.AddCommonOptions`, `Common.Application.BackgroundJobs`, `OutboxModule.cs`, `CaptchaOptions`, `Host.Swagger`, `IModule`, `Common.Application.EventBus`, `FullTextSearchOptions`, `.AddCustomHealthChecks`?**
  _High betweenness centrality (0.221) - this node is a cross-community bridge._
- **Why does `Result` connect `Result` to `.SendCoreAsync`, `.SaveChangesAsync`, `.ListSessions`, `.SingleAsResult`, `Error`, `ProductTemplate`, `VerifyPhoneOtpResponse`, `.SendOtp`, `.GetMeAsync`, `Response`, `Response`, `.SendAsync`, `.ReserveStockAsync`, `ApplicationUserId`, `ThrottledSmsGateway`, `.GetAuditLogAsync`, `DeactivateDeviceSessionsRequest`, `.CommitStockReservationAsync`, `NetGsmSmsGateway`, `KeycloakTokenClient`, `.SearchStoresAsync`, `.CreateStoreAsync`, `.ReserveSeriesAsync`, `.AddProductAsync`, `StockReservation`, `.RefreshToken`, `ICaptchaService`, `.UpdateStoreAsync`, `.BindDeviceAsync`, `AuditLogDto`, `PaginationResponse`, `ReCaptchaService`, `.AddNetGsm`, `.SearchProductTemplatesAsync`, `.AddProductToMyStoreAsync`, `Response`, `DummyPushGateway`, `Response`, `IProductsDbContext`, `.CreateMyStoreAsync`, `.Failure`, `ResultTelemetryExtensions`, `.SendAsync`, `Store`, `IInterModuleRequestClient`, `.AddPushServices`, `Response`, `.CreateProductTemplateAsync`, `.SearchMyProductsAsync`, `.UpdateMyProductAsync`, `.SearchStoreProductsAsync`, `.UpdateMyStoreAsync`, `.ReleaseStockReservationAsync`, `.UpdateCurrentPushToken`, `.IsRegisteredAsync`, `.GetStoreAuditLogAsync`, `.RemoveProductAsync`, `HttpWarehouseGateway`, `Response`, `.HandleWarehouseWebhookAsync`, `ICurrentUser`, `.TapWhenFeatureEnabledAsync`?**
  _High betweenness centrality (0.160) - this node is a cross-community bridge._
- **Why does `ApplicationUserId` connect `ApplicationUserId` to `NotificationPayload`, `DeviceRegistryReconciliationService`, `KeycloakAdminClient`, `Response`, `KeycloakPermissionAuthorizationHandler`, `Response`, `AuditableEntity`, `INotificationDispatcher`, `DeviceRegistration`, `DeactivateDeviceSessionsRequest`, `KeycloakTokenClient`, `.ForUser`, `.CreateStoreAsync`, `For`, `CurrentUser`, `.BindDeviceAsync`, `INotificationsDbContext`, `AuditLogDto`, `Response`, `Request`, `V1StoreCreatedDomainEvent`, `IStronglyTypedId`, `AuditableEntityResponse`, `.Configure`, `Store`, `Response`, `Seeder`, `.HandleAsync`, `Common.Application.Validation`, `CreateStockLevelOnProductCreatedHandler`, `StockLevel`, `Response`, `Request`, `ICurrentUser`?**
  _High betweenness centrality (0.095) - this node is a cross-community bridge._
- **What connects `Id`, `IdAsString`, `Roles` to the rest of the system?**
  _822 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Common.InterModuleRequests.Contracts` be split into smaller, more focused modules?**
  _Cohesion score 0.09061224489795919 - nodes in this community are weakly interconnected._
- **Should `DeviceRegistryReconciliationService` be split into smaller, more focused modules?**
  _Cohesion score 0.07317073170731707 - nodes in this community are weakly interconnected._
- **Should `RedisFixedWindowRateLimiter` be split into smaller, more focused modules?**
  _Cohesion score 0.12554112554112554 - nodes in this community are weakly interconnected._