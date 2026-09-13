# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-09-12)

## Corpus Check
- 515 files · ~73,691 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 4177 nodes · 7428 edges · 359 communities (264 shown, 90 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 251 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `459000fe`
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
- .SavingChangesAsync
- FirebasePushGateway
- Request
- Error
- Common.Domain.ResultMonad
- VerifyPhoneOtpResponse
- .SendOtp
- UserRepresentation
- KeycloakPermissionAuthorizationHandler
- ISmsGateway
- .UseModule
- DomainEvent
- Common.Infrastructure.Persistence
- ApplicationUserId
- BoundedRequestCaptureStream
- DeviceRegistration
- Product
- .SendAsync
- NetGsmSmsGateway
- IEvent
- RequestResponseBodyLoggingMiddleware
- KeycloakTokenClient
- Result
- V1ProductAddedToStoreDomainEvent
- .RaiseEvent
- .CreateStoreAsync
- .SaveChangesAsync
- KeycloakPermissionClient
- OutboxMessage
- CustomRateLimitingOptions
- StockReservation
- .RefreshToken
- ObservabilityOptions
- IamTelemetry
- Request
- .SaveWithOutboxAsync
- CreateStoreRateLimitingPolicy
- KeycloakOptions
- .BindDeviceAsync
- Common.Domain.Events
- Outbox Misuse Check
- AuditableEntity
- Add Integration Event Command
- Response
- ProductsDbContext
- ReCaptchaService
- Inventory.Domain.StockReservations
- Response
- RegisterRateLimitingPolicy
- .SendOtp
- Cross-Module Reference Violation
- OutboxMetricsJob
- .SearchProductTemplatesAsync
- Bogus Test Data
- Request
- NotificationsDbContext
- RequestLoggingOptions
- Common.InterModuleRequests
- IAMModule.cs
- OutboxModule
- Response
- ValueObject
- V1StoreCreatedDomainEvent
- IStronglyTypedId
- Full-Text Search
- Response
- .WriteAsync
- AsNoTracking Coverage Check
- CaptchaOptions
- Common.Application.ModelBinders
- .CreateMyStoreAsync
- CheckRegistrationRateLimitingPolicy
- IntegrationEvent
- EventDispatcher
- .SearchStoresAsync
- V1StockReservationCommitConflictDetectedDomainEvent
- .EnsureNoMigrationsPending
- .Configure
- ResultTelemetryExtensions
- Store
- PushOptions
- Configuration-Driven Module Registration
- Program.cs
- IInterModuleRequestClient
- KeycloakPermissionPolicyProvider
- Common.Domain.StronglyTypedIds
- .GetClientKey
- .AddCommonPersistence
- .SearchMyProductsAsync
- DatabaseSeederOrchestrator
- .SeedProductAsync
- .SearchStoreProductsAsync
- ThrottledSmsGateway
- OutboxDbContext
- ResiliencyOptions
- HttpContextTargetingContextAccessor
- Common.Application.BackgroundJobs
- CreateStockLevelOnProductCreatedHandler
- .AddKeycloakInfrastructure
- OutboxModule.cs
- .ReleaseStockReservationAsync
- StockLevel
- .IsRegisteredAsync
- .SeedAsync
- BackgroundJobsService
- OutboxCleanupJob
- Request
- Response
- Response
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
- MassTransitInterModuleRequestClient
- docker-compose.yml (Base Stack)
- .AddInfrastructure
- .UpdateProductAsync
- Consumer Idempotency (IntegrationEventHandlerBase)
- Request
- InterModuleRequestHandler
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
- Setup
- .AddCommonCaching
- FeatureFlags
- OutboxOptions
- ISearchLanguageResolver
- SwaggerDefaultValues
- CustomValidator
- AuditableEntityResponse
- Request
- IntegrationEventHandlerBase
- FirebaseServiceAccountOptions
- IInventoryDbContext
- OtpServiceBase
- .ReserveSeriesAsync
- HttpWarehouseGateway
- For
- Request
- CurrentUser
- Response
- PaginationRequest
- IProductsDbContext
- ICaptchaService
- Notifications.Application/IAssemblyReference.cs
- IOtpService
- ResxLocalizationOptions
- v1/AddProduct/Request.cs
- SendSecurityAlertRequestHandler
- PaginationResponse
- Common.Application.Options
- .AddProductToMyStoreAsync
- DummyPushGateway
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
- .AddPushServices
- InventoryModule
- OpenApiOptions
- KeyValuePair
- KeycloakScopes
- Request
- SmsRateLimitingPolicy
- Configuration-Driven Module Loading
- Infrastructure/StringExtensions.cs
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- ConfigureSwaggerOptions
- NotificationsModule
- Setup
- ProductsTelemetry
- Common.Application.FeatureManagement
- Request
- ProductTemplateId
- Request
- Response
- SendForLogin/Request.cs
- Seeder
- Keycloak realm as code
- IntegrationEventConverter
- RedisOtpService
- AuditLogRetentionService
- BackgroundJobsOptions
- .HandleAsync
- BackgroundJobsModule
- OtpOptions
- .AddCommonOptions
- .ReserveStockAsync
- Request
- Common.Domain.Devices
- StockReservationExpirySweepService
- Setup
- Stores/v1/Get/Request.cs
- .UpdateCurrentPushToken
- ReCaptchaResponse
- StockReservations/v1/Get/Request.cs
- .TryReadFromJsonAsync
- StringExtensions
- IAM.Endpoints.Common.Validations
- .AddCustomSwagger
- .RemoveProductAsync
- .AddAuthInfrastructure
- Host.Swagger
- KeycloakPermission
- StockReservationExpirySweepJobRegistrar
- TokenResponseRepresentation
- IntegrationEventOutbox
- V1ProductCreatedDomainEvent
- StoreId
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
- .ActivateProductTemplateAsync
- ReservationStatus
- .AddNetGsm
- .UpdateMyStoreAsync
- .InvokeAsync
- LogProductCatalogChangeHandler
- Request
- DefaultResponsesOperationFilter
- v1/Request.cs
- RequireFeatureFilter
- .FixedWindow
- GetProductRequest
- .Capture
- Inventory.Endpoints.StockReservations
- SendResponseBody
- ProductId
- V1StockLevelCreatedDomainEvent
- ProblemDetailsExtensions
- .CommitStockReservationAsync
- ProductsModule.cs
- InventoryTelemetry
- ProblemDetailsContext
- Products.Endpoints.Probe
- .SeedProductTemplatesAsync
- IAutoMigrateMarker.cs
- .AddModules
- .AddStockReservationExpirySweep
- .DeactivateStoreAsync
- .RemoveMyProductAsync
- VersionNeutral/Get/Request.cs
- JwtClaimNames.cs
- KeycloakRoles.cs
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
1. `Result` - 121 edges
2. `Common.Application.Options` - 109 edges
3. `Common.Domain.ResultMonad` - 92 edges
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
- `CaptchaErrors` --references--> `Error`  [EXTRACTED]
  src/Modules/IAM/IAM.Domain/Captcha/CaptchaErrors.cs → src/Common/Common.Domain/ResultMonad/Error.cs
- `StockReservationErrors` --references--> `Error`  [EXTRACTED]
  src/Modules/Inventory/Inventory.Domain/StockReservations/Errors/StockReservationErrors.cs → src/Common/Common.Domain/ResultMonad/Error.cs
- `AuditLogDto` --references--> `ApplicationUserId`  [EXTRACTED]
  src/Common/Common.Application/AuditLog/AuditLogDto.cs → src/Common/Common.Domain/StronglyTypedIds/ApplicationUserId.cs
- `ICurrentUser` --references--> `ApplicationUserId`  [EXTRACTED]
  src/Common/Common.Application/Auth/ICurrentUser.cs → src/Common/Common.Domain/StronglyTypedIds/ApplicationUserId.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Local Infrastructure Stack** — docker_compose_postgres, docker_compose_rabbitmq, docker_compose_redis, docker_compose_aspire_dashboard [EXTRACTED 1.00]

## Communities (359 total, 90 thin omitted)

### Community 0 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.08
Nodes (19): IAM.Infrastructure.Keycloak.Representations, IAM.Endpoints.Otp, Notifications.Application.Persistence, Common.InterModuleRequests.IAM, Common.InterModuleRequests.Contracts, Notifications.Infrastructure.InterModuleRequestHandlers, IAM.Endpoints.Tokens.VersionNeutral.Revoke, IAM.Infrastructure.Telemetry (+11 more)

### Community 1 - "NotificationPayload"
Cohesion: 0.06
Nodes (35): Notifications.Infrastructure.Hubs, Notifications.Application.Hubs, Hub, HubConnectionContext, IHubContext, IUserIdProvider, RedisOptions, SignalROptions (+27 more)

### Community 2 - "OutboxProcessor"
Cohesion: 0.22
Nodes (11): IPublishEndpoint, CancellationToken, Exception, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task (+3 more)

### Community 3 - "DeviceRegistryReconciliationService"
Cohesion: 0.07
Nodes (29): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator, IReadOnlyCollection, GetActiveSessionIdsRequest, GetActiveSessionIdsResponse (+21 more)

### Community 5 - "KeycloakAdminClient"
Cohesion: 0.22
Nodes (14): HttpRequestMessage, CancellationToken, Func, HttpClient, HttpResponseMessage, IFusionCache, ILogger, IOptions (+6 more)

### Community 6 - "RedisFixedWindowRateLimiter"
Cohesion: 0.13
Nodes (16): RateLimiter, RateLimiterStatistics, RateLimitLease, FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration (+8 more)

### Community 7 - ".SavingChangesAsync"
Cohesion: 0.25
Nodes (6): ISearchLocalized, Language, CancellationToken, DbContextEventData, InterceptionResult, ValueTask

### Community 8 - "FirebasePushGateway"
Cohesion: 0.24
Nodes (8): FirebaseApp, FirebaseMessaging, IDisposable, Exception, ILogger, LoggerMessage, TimeSpan, FirebasePushGateway

### Community 9 - "Request"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Products.v1.My.Update, RequestBody, Request, Body, Id, RequestBody, Description, Name (+3 more)

### Community 10 - "Error"
Cohesion: 0.08
Nodes (19): StringLocalizerExtensions, IStringLocalizer, Error, Key, ParameterName, StatusCode, SubErrors, Value (+11 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.16
Nodes (9): Common.Application.Search, Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Products.Infrastructure.Telemetry, Products.Application.Persistence, Common.Domain.ResultMonad, Common.Application.Pagination (+1 more)

### Community 12 - "VerifyPhoneOtpResponse"
Cohesion: 0.20
Nodes (10): OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, VerifyPhoneOtpResponseExtensions, CancellationToken (+2 more)

### Community 13 - ".SendOtp"
Cohesion: 0.11
Nodes (17): SendPhoneOtpRequest, SendPhoneOtpResponse, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, CancellationToken, IFeatureManager (+9 more)

### Community 14 - "UserRepresentation"
Cohesion: 0.07
Nodes (29): Dictionary, List, CredentialRepresentation, Temporary, Type, Value, ErrorRepresentation, Error (+21 more)

### Community 15 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.12
Nodes (17): AuthorizationHandler, AuthorizationHandlerContext, IAuthorizationRequirement, CancellationToken, ClaimsPrincipal, HttpContext, IFusionCache, IHttpContextAccessor (+9 more)

### Community 16 - "ISmsGateway"
Cohesion: 0.14
Nodes (13): CancellationToken, Task, ISmsGateway, SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, SmsMessage (+5 more)

### Community 17 - ".UseModule"
Cohesion: 0.20
Nodes (7): DashboardContext, IAuthorizationService, IDashboardAsyncAuthorizationFilter, IApplicationBuilder, IOptions, HangfireCustomAuthorizationFilter, Task

### Community 18 - "DomainEvent"
Cohesion: 0.08
Nodes (23): DomainEvent, CreatedOn, Id, Version, DateTimeOffset, DefaultIdType, AuditLogEntryConfiguration, EntityTypeBuilder (+15 more)

### Community 19 - "Common.Infrastructure.Persistence"
Cohesion: 0.10
Nodes (14): Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Common.Application.Persistence, Common.Infrastructure.Localization, Notifications.Domain.Devices, Notifications.Infrastructure.Persistence, Inventory.Infrastructure.Persistence, Common.Infrastructure.EventBus (+6 more)

### Community 20 - "ApplicationUserId"
Cohesion: 0.12
Nodes (23): ApplicationUserId, IsEmpty, Value, DefaultIdType, CancellationToken, IReadOnlyList, Task, IKeycloakAdminClient (+15 more)

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.14
Nodes (8): SeekOrigin, BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position, Stream

### Community 22 - "DeviceRegistration"
Cohesion: 0.12
Nodes (15): DateTimeOffset, DefaultIdType, Guid, DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive (+7 more)

### Community 23 - "Product"
Cohesion: 0.10
Nodes (19): ProductId, V1ProductDescriptionUpdatedDomainEvent, ProductId, V1ProductNameUpdatedDomainEvent, ProductId, V1ProductPriceIncreasedDomainEvent, ProductTemplate, ProductTemplateId (+11 more)

### Community 24 - ".SendAsync"
Cohesion: 0.09
Nodes (20): CancellationToken, Task, DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse, IReadOnlyList, CancellationToken, RouteGroupBuilder, Task (+12 more)

### Community 25 - "NetGsmSmsGateway"
Cohesion: 0.16
Nodes (14): SendRequestBody, SendResponseBody, CancellationToken, Exception, HttpClient, ILogger, IOptions, JsonSerializerOptions (+6 more)

### Community 26 - "IEvent"
Cohesion: 0.12
Nodes (12): CancellationToken, Task, CancellationToken, Task, IEventHandlerWrapper, CancellationToken, Task, IEvent (+4 more)

### Community 27 - "RequestResponseBodyLoggingMiddleware"
Cohesion: 0.20
Nodes (7): IDiagnosticContext, HttpContext, IList, IOptions, PathString, RequestDelegate, RequestResponseBodyLoggingMiddleware

### Community 28 - "KeycloakTokenClient"
Cohesion: 0.15
Nodes (18): JsonWebTokenHandler, CancellationToken, Task, IKeycloakTokenClient, DateTimeOffset, KeycloakTokens, CancellationToken, Dictionary (+10 more)

### Community 29 - "Result"
Cohesion: 0.13
Nodes (14): Result, Error, IsFailure, Success, Value, Func, Task, AsyncExtensions (+6 more)

### Community 30 - "V1ProductAddedToStoreDomainEvent"
Cohesion: 0.13
Nodes (13): StoreId, ProductSnapshot, ProductTemplateId, ProductSnapshot, V1ProductAddedToStoreDomainEvent, V1ProductAddedToStoreDomainEventExtensions, ProductSnapshot, ProductTemplateId (+5 more)

### Community 31 - ".RaiseEvent"
Cohesion: 0.15
Nodes (11): AggregateRoot, Events, Id, Version, IReadOnlyCollection, List, IAggregateRoot, Events (+3 more)

### Community 32 - ".CreateStoreAsync"
Cohesion: 0.14
Nodes (10): Products.Endpoints.Stores.v1.Create, Products.Endpoints.Stores, RouteGroupBuilder, Setup, CancellationToken, RouteGroupBuilder, Task, Endpoint (+2 more)

### Community 33 - ".SaveChangesAsync"
Cohesion: 0.08
Nodes (18): CancellationToken, Task, CancellationToken, RouteGroupBuilder, Task, Endpoint, CancellationToken, RouteGroupBuilder (+10 more)

### Community 34 - "KeycloakPermissionClient"
Cohesion: 0.23
Nodes (11): CancellationToken, Dictionary, HttpClient, HttpResponseMessage, ILogger, IOptions, IReadOnlyList, List (+3 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.10
Nodes (20): IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, DateTimeOffset, OutboxMessage (+12 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.08
Nodes (23): CheckRegistrationRateLimitingPolicy, CreateStoreRateLimitingPolicy, RegisterRateLimitingPolicy, SmsRateLimitingPolicy, CustomRateLimitingOptions, CheckRegistration, CreateStore, ExemptPathPrefixes (+15 more)

### Community 37 - "StockReservation"
Cohesion: 0.06
Nodes (31): Inventory.Domain.StockReservations.DomainEvents.v1, DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationExpiredDomainEvent, DateTimeOffset, StockReservationId, V1StockReservationReleaseAttemptAbandonedDomainEvent (+23 more)

### Community 38 - ".RefreshToken"
Cohesion: 0.11
Nodes (16): IAM.Domain.Users, IAM.Endpoints.Tokens.VersionNeutral.Refresh, Constants, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request (+8 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.07
Nodes (30): IHostBuilder, LoggerConfiguration, LoggerMinimumLevelConfiguration, OpenTelemetryBuilder, ResourceBuilder, ObservabilityOptions, AppName, AppVersion (+22 more)

### Community 40 - "IamTelemetry"
Cohesion: 0.22
Nodes (6): ActivitySource, Counter, Meter, IamTelemetry, LoginMethods, SessionRevokedReasons

### Community 41 - "Request"
Cohesion: 0.20
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
Cohesion: 0.11
Nodes (20): BindDeviceSessionRequest, BindDeviceSessionResponse, Guid, CancellationToken, Exception, Guid, ILogger, LoggerMessage (+12 more)

### Community 46 - "Common.Domain.Events"
Cohesion: 0.17
Nodes (3): Products.Domain.Products.DomainEvents.v1, Common.Domain.Events, Products.Domain.Stores.DomainEvents.v1

### Community 48 - "AuditableEntity"
Cohesion: 0.11
Nodes (18): AuditableEntity, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, Version, DateTimeOffset (+10 more)

### Community 50 - "Response"
Cohesion: 0.09
Nodes (20): IAM.Endpoints.Users.VersionNeutral.Search, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, SearchTerm, RequestValidator (+12 more)

### Community 51 - "ProductsDbContext"
Cohesion: 0.13
Nodes (14): DbContextOptions, DbSet, ILogger, ProductTemplate, Store, TimeProvider, ProductsDbContext, Products (+6 more)

### Community 52 - "ReCaptchaService"
Cohesion: 0.21
Nodes (10): FormUrlEncodedContent, ReCaptchaResponse, CancellationToken, Exception, HttpClient, ILogger, IOptions, LoggerMessage (+2 more)

### Community 53 - "Inventory.Domain.StockReservations"
Cohesion: 0.19
Nodes (8): Inventory.Infrastructure.InterModuleRequestHandlers, Inventory.Application.Persistence, Inventory.Domain.StockReservations, Inventory.Domain.StockReservations.Errors, Inventory.Infrastructure.Telemetry, Common.InterModuleRequests.Inventory, Inventory.Infrastructure.StockReservations, StockReservationErrors

### Community 54 - "Response"
Cohesion: 0.15
Nodes (10): Products.Endpoints.Products, RouteGroupBuilder, Setup, RouteGroupBuilder, Response, AvailableQuantity, Description, Name (+2 more)

### Community 55 - "RegisterRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, RegisterRateLimitingPolicy (+1 more)

### Community 56 - ".SendOtp"
Cohesion: 0.20
Nodes (7): CancellationToken, Task, CancellationToken, IFeatureManager, RouteGroupBuilder, Task, Endpoint

### Community 58 - "OutboxMetricsJob"
Cohesion: 0.13
Nodes (14): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxMetricsJob (+6 more)

### Community 59 - ".SearchProductTemplatesAsync"
Cohesion: 0.15
Nodes (11): LikePattern, CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response (+3 more)

### Community 61 - "Request"
Cohesion: 0.14
Nodes (11): Products.Endpoints.Products.v1.Search, Constants, RequestValidator, ProductTemplateId, Request, Description, Name, Price (+3 more)

### Community 62 - "NotificationsDbContext"
Cohesion: 0.15
Nodes (13): DbSet, INotificationsDbContext, DeviceRegistrations, DbContextOptions, DbSet, ILogger, TimeProvider, NotificationsDbContext (+5 more)

### Community 63 - "RequestLoggingOptions"
Cohesion: 0.11
Nodes (20): IPostConfigureOptions, RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes (+12 more)

### Community 64 - "Common.InterModuleRequests"
Cohesion: 0.29
Nodes (4): Common.InterModuleRequests, IAssemblyReference, Setup, IServiceCollection

### Community 65 - "IAMModule.cs"
Cohesion: 0.22
Nodes (4): IAM.Endpoints, IAM.Infrastructure.Auth, IAM.Infrastructure.Captcha, IAssemblyReference

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
Cohesion: 0.26
Nodes (9): DomainEventHandlerBase, IEventHandler, CancellationToken, Task, SimulateSomeBusinessHandler, StoreCreatedIntegrationEventPublishingHandler, V1StoreCreatedDomainEventHandlers, StoreId (+1 more)

### Community 70 - "IStronglyTypedId"
Cohesion: 0.06
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

### Community 75 - "CaptchaOptions"
Cohesion: 0.16
Nodes (13): CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey, Provider, ScoreThreshold, SecretKey (+5 more)

### Community 76 - "Common.Application.ModelBinders"
Cohesion: 0.08
Nodes (22): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Stores.v1.Deactivate, Inventory.Endpoints.StockReservations.v1.Release, Common.Application.ModelBinders, Products.Endpoints.Products.v1.AuditLog, IModelBinder, ModelBindingContext, StronglyTypedIdBinder (+14 more)

### Community 77 - ".CreateMyStoreAsync"
Cohesion: 0.16
Nodes (10): Products.Endpoints.Stores.v1.My.Create, Store, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator (+2 more)

### Community 78 - "CheckRegistrationRateLimitingPolicy"
Cohesion: 0.18
Nodes (10): IRateLimiterPolicy, CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask (+2 more)

### Community 79 - "IntegrationEvent"
Cohesion: 0.22
Nodes (9): IReadOnlyList, IntegrationEvent, CreatedOn, Id, DateTimeOffset, DefaultIdType, ProductCreatedIntegrationEvent, StoreCreatedIntegrationEvent (+1 more)

### Community 80 - "EventDispatcher"
Cohesion: 0.31
Nodes (7): EventDispatcher, ActivitySource, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task

### Community 81 - ".SearchStoresAsync"
Cohesion: 0.15
Nodes (12): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Address (+4 more)

### Community 82 - "V1StockReservationCommitConflictDetectedDomainEvent"
Cohesion: 0.20
Nodes (8): DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationCommitConflictDetectedDomainEvent, DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationCommittedDomainEvent

### Community 83 - ".EnsureNoMigrationsPending"
Cohesion: 0.44
Nodes (4): MigrationGuard, ILogger, IServiceProvider, LoggerMessage

### Community 84 - ".Configure"
Cohesion: 0.17
Nodes (12): AuditableEntityConfiguration, EntityTypeBuilder, StronglyTypedIdValueConverter, DefaultIdType, EntityTypeBuilder, NpgsqlTsVector, ProductTemplateId, StoreId (+4 more)

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

### Community 90 - "IInterModuleRequestClient"
Cohesion: 0.18
Nodes (12): IInterModuleRequest, IInterModuleRequestClient, GetStockLevelRequest, GetStockLevelResponse, DefaultIdType, CancellationToken, Task, GetStockLevelRequestHandler (+4 more)

### Community 91 - "KeycloakPermissionPolicyProvider"
Cohesion: 0.24
Nodes (8): AuthorizationOptions, AuthorizationPolicy, DefaultAuthorizationPolicyProvider, IAuthorizationPolicyProvider, ConcurrentDictionary, IOptions, Task, KeycloakPermissionPolicyProvider

### Community 92 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.09
Nodes (15): Products.Infrastructure.Persistence.Seeding, Common.Domain.StronglyTypedIds, Products.Domain.Products, Common.Application.DTOs, Common.Application.JsonConverters, Common.Domain.Entities, Products.Domain.Stores, Common.Infrastructure.Persistence.EntityConfigurations (+7 more)

### Community 93 - ".GetClientKey"
Cohesion: 0.18
Nodes (8): IAM.Endpoints.Captcha.VersionNeutral, IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, RouteGroupBuilder, Endpoint, Response, ClientKey, RouteGroupBuilder, Setup

### Community 94 - ".AddCommonPersistence"
Cohesion: 0.33
Nodes (5): DatabaseOptions, ConnectionString, DatabaseOptionsValidator, IOptions, IServiceCollection

### Community 95 - ".SearchMyProductsAsync"
Cohesion: 0.17
Nodes (11): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Description (+3 more)

### Community 96 - "DatabaseSeederOrchestrator"
Cohesion: 0.15
Nodes (14): BackgroundService, DatabaseSeederOrchestrator, CancellationToken, Exception, IHostApplicationLifetime, ILogger, IServiceScopeFactory, LoggerMessage (+6 more)

### Community 97 - ".SeedProductAsync"
Cohesion: 0.22
Nodes (7): CancellationToken, Task, CancellationToken, List, ProductTemplateId, StoreId, Task

### Community 98 - ".SearchStoreProductsAsync"
Cohesion: 0.17
Nodes (11): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Description (+3 more)

### Community 99 - "ThrottledSmsGateway"
Cohesion: 0.29
Nodes (5): CancellationToken, IFusionCache, IOptions, Task, ThrottledSmsGateway

### Community 100 - "OutboxDbContext"
Cohesion: 0.12
Nodes (14): DbContext, IOutboxDbContext, OutboxMessages, CancellationToken, DbSet, Task, IConfiguration, IServiceCollection (+6 more)

### Community 101 - "ResiliencyOptions"
Cohesion: 0.10
Nodes (19): HttpStandardResilienceOptions, IHttpClientBuilder, ResiliencyOptions, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds (+11 more)

### Community 102 - "HttpContextTargetingContextAccessor"
Cohesion: 0.17
Nodes (9): Common.Infrastructure.FeatureManagement, ITargetingContextAccessor, HttpContextTargetingContextAccessor, IHttpContextAccessor, ValueTask, Setup, IConfiguration, IServiceCollection (+1 more)

### Community 103 - "Common.Application.BackgroundJobs"
Cohesion: 0.10
Nodes (14): Common.Application.BackgroundJobs, BackgroundJobs.Telemetry, BackgroundJobs, IServerFilter, PerformedContext, PerformingContext, JobMetricsFilter, BackgroundJobsTelemetry (+6 more)

### Community 104 - "CreateStockLevelOnProductCreatedHandler"
Cohesion: 0.24
Nodes (8): CancellationToken, DefaultIdType, IFusionCache, ILogger, IOptions, LoggerMessage, Task, CreateStockLevelOnProductCreatedHandler

### Community 105 - ".AddKeycloakInfrastructure"
Cohesion: 0.16
Nodes (10): CancellationToken, Task, IServiceAccountTokenProvider, HttpClient, IOptions, TimeProvider, ServiceAccountTokenProvider, IOptions (+2 more)

### Community 106 - "OutboxModule.cs"
Cohesion: 0.18
Nodes (7): Outbox, Common.Infrastructure.Persistence.Outbox, Outbox.Persistence, Common.Application.Persistence.Outbox, Outbox.Telemetry, IEntityTypeConfiguration, OutboxMessageConfig

### Community 107 - ".ReleaseStockReservationAsync"
Cohesion: 0.13
Nodes (14): CancellationToken, Task, IWarehouseGateway, CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint (+6 more)

### Community 108 - "StockLevel"
Cohesion: 0.18
Nodes (9): Inventory.Infrastructure.Persistence.EntityConfigurations, Inventory.Domain.StockLevels, DefaultIdType, StockLevel, ProductId, QuantityOnHand, StockLevelId, EntityTypeBuilder (+1 more)

### Community 109 - ".IsRegisteredAsync"
Cohesion: 0.18
Nodes (10): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, PhoneNumber, RequestValidator (+2 more)

### Community 110 - ".SeedAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, Task, CancellationToken, Task

### Community 111 - "BackgroundJobsService"
Cohesion: 0.14
Nodes (15): IBackgroundJobClientV2, IBackgroundJobs, Action, DateTimeOffset, Expression, Func, Task, TimeSpan (+7 more)

### Community 112 - "OutboxCleanupJob"
Cohesion: 0.27
Nodes (8): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxCleanupJob

### Community 113 - "Request"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Products.v1.Update, RequestBody, Request, Body, Id, RequestBody, Description, Name (+3 more)

### Community 114 - "Response"
Cohesion: 0.11
Nodes (17): IAM.Endpoints.Users.VersionNeutral.Me.Get, RouteGroupBuilder, Endpoint, DateOnly, DateTimeOffset, IReadOnlyCollection, Response, BirthDate (+9 more)

### Community 115 - "Response"
Cohesion: 0.18
Nodes (9): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, RouteGroupBuilder, Endpoint, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken (+1 more)

### Community 116 - ".WriteProblemAsync"
Cohesion: 0.20
Nodes (10): IAllowAnonymous, IConfigureNamedOptions, HttpContext, HttpStatusCode, IOptions, IProblemDetailsService, IResxLocalizer, JwtBearerOptions (+2 more)

### Community 117 - "Response"
Cohesion: 0.14
Nodes (13): CancellationToken, RouteGroupBuilder, Task, Endpoint, DateTimeOffset, DefaultIdType, Response, Id (+5 more)

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
Cohesion: 0.33
Nodes (5): FeatureFlagResultExtensions, Action, Func, IFeatureManager, Task

### Community 126 - "GlobalExceptionHandlingMiddleware"
Cohesion: 0.19
Nodes (11): IApplicationBuilder, IServiceCollection, Exception, HttpContext, ILogger, IProblemDetailsService, IResxLocalizer, LoggerMessage (+3 more)

### Community 127 - "BaseDbContext"
Cohesion: 0.22
Nodes (8): BaseDbContext, AuditLog, CancellationToken, DbContextOptions, DbSet, ILogger, Task, TimeProvider

### Community 128 - "AuditLogEntry"
Cohesion: 0.29
Nodes (7): AuditLogEntry, AggregateId, AggregateType, Event, EventType, Version, DefaultIdType

### Community 129 - "MassTransitInterModuleRequestClient"
Cohesion: 0.22
Nodes (8): IClientFactory, InterModuleRequestOptions, TimeoutSeconds, InterModuleRequestOptionsValidator, MassTransitInterModuleRequestClient, CancellationToken, IOptions, Task

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.48
Nodes (7): Observability (OpenTelemetry), docker-compose.yml (Base Stack), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), Postgres Service (mm.postgres), RabbitMQ Service (mm.rabbitmq), Redis Service (mm.redis)

### Community 131 - ".AddInfrastructure"
Cohesion: 0.29
Nodes (7): HostOptions, JsonOptions, Assembly, IConfiguration, IResxLocalizer, IServiceCollection, IWebHostEnvironment

### Community 132 - ".UpdateProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 134 - "Request"
Cohesion: 0.33
Nodes (5): Inventory.Endpoints.StockReservations.v1.ReserveSeries, RequestBody, Request, Body, RequestValidator

### Community 135 - "InterModuleRequestHandler"
Cohesion: 0.07
Nodes (30): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, IConsumer, IInterModuleRequestHandler, CancellationToken, Task, InterModuleRequestHandler, CancellationToken, ConsumeContext (+22 more)

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
Cohesion: 0.15
Nodes (12): DatabaseFacade, EntityEntry, IDbContext, AuditLog, ChangeTracker, Database, DbSet, DbContextExtensions (+4 more)

### Community 144 - "Response"
Cohesion: 0.12
Nodes (16): CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate (+8 more)

### Community 145 - "EnrichLogsWithUserInfoMiddleware"
Cohesion: 0.12
Nodes (12): IAuthenticationSchemeProvider, IMiddleware, IApplicationBuilder, HttpContext, RequestDelegate, Task, EnrichLogsWithUserInfoMiddleware, HttpContext (+4 more)

### Community 146 - "IAM.Endpoints.Otp.VersionNeutral"
Cohesion: 0.40
Nodes (3): IAM.Endpoints.Otp.VersionNeutral, RouteGroupBuilder, Setup

### Community 147 - ".AddServices"
Cohesion: 0.12
Nodes (15): RecurringJobOptions, IRecurringBackgroundJobs, Action, Expression, Func, Task, IConfiguration, IServiceCollection (+7 more)

### Community 148 - "Setup"
Cohesion: 0.14
Nodes (9): Host.Middlewares, Host.Infrastructure, ModuleRegistry, Setup, Exception, IApplicationBuilder, ILogger, LoggerMessage (+1 more)

### Community 149 - ".AddCommonCaching"
Cohesion: 0.29
Nodes (5): Common.Infrastructure.Caching, Setup, IConfiguration, IConnectionMultiplexer, IServiceCollection

### Community 150 - "FeatureFlags"
Cohesion: 0.33
Nodes (5): Checkout, FeatureFlags, IAM, Notifications, Products

### Community 151 - "OutboxOptions"
Cohesion: 0.11
Nodes (20): OutboxCleanupSettings, BatchSize, CronSchedule, Enabled, RetentionDays, OutboxCleanupSettingsValidator, OutboxOptions, BaseBackoffSeconds (+12 more)

### Community 152 - "ISearchLanguageResolver"
Cohesion: 0.23
Nodes (7): ISearchLanguageResolver, UniversalConfig, SearchLanguageResolver, UniversalConfig, IOptions, Setup, IServiceCollection

### Community 153 - "SwaggerDefaultValues"
Cohesion: 0.33
Nodes (4): JsonValue, OpenApiOperation, OperationFilterContext, SwaggerDefaultValues

### Community 154 - "CustomValidator"
Cohesion: 0.08
Nodes (32): AbstractValidator, Inventory.Endpoints.StockReservations.v1.Commit, Products.Endpoints.Stores.v1.Update, Common.Application.Validation, Products.Endpoints.Stores.v1.RemoveProduct, SecurityHeadersOptions, Headers, SecurityHeadersOptionsValidator (+24 more)

### Community 155 - "AuditableEntityResponse"
Cohesion: 0.29
Nodes (7): AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, DateTimeOffset

### Community 156 - "Request"
Cohesion: 0.15
Nodes (13): Guid, Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FirstName (+5 more)

### Community 157 - "IntegrationEventHandlerBase"
Cohesion: 0.24
Nodes (11): IntegrationEventHandlerBase, MaxEventAge, CancellationToken, ConsumeContext, DefaultIdType, IFusionCache, ILogger, IOptions (+3 more)

### Community 158 - "FirebaseServiceAccountOptions"
Cohesion: 0.29
Nodes (7): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri

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
Cohesion: 0.22
Nodes (9): Guid, Request, ClientId, DeviceId, DeviceName, Email, Password, PushToken (+1 more)

### Community 165 - "CurrentUser"
Cohesion: 0.12
Nodes (14): Common.Infrastructure.Auth.Services, Common.Infrastructure.Auth, CurrentUser, Id, IdAsString, IsAuthenticated, Principal, Roles (+6 more)

### Community 166 - "Response"
Cohesion: 0.13
Nodes (12): IAM.Endpoints.Tokens.VersionNeutral.Create, IAM.Endpoints.Tokens.VersionNeutral, RouteGroupBuilder, Endpoint, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt (+4 more)

### Community 167 - "PaginationRequest"
Cohesion: 0.15
Nodes (11): PaginationRequest, PageNumber, PageSize, Skip, Take, PaginationQueryableExtensions, CancellationToken, Expression (+3 more)

### Community 168 - "IProductsDbContext"
Cohesion: 0.09
Nodes (19): Products.Endpoints.ProductTemplates, DbSet, ProductTemplate, IProductsDbContext, Products, ProductTemplates, Stores, RouteGroupBuilder (+11 more)

### Community 169 - "ICaptchaService"
Cohesion: 0.14
Nodes (10): ICaptchaService, CancellationToken, IFusionCache, IOptions, Task, CachedCaptchaService, DummyCaptchaService, IConfiguration (+2 more)

### Community 171 - "IOtpService"
Cohesion: 0.21
Nodes (8): CancellationToken, Task, TimeSpan, IOtpService, OtpVerificationOutcome, InvalidOtp, Success, TooManyAttempts

### Community 172 - "ResxLocalizationOptions"
Cohesion: 0.25
Nodes (7): ResxLocalizationOptions, DefaultCulture, SupportedCultures, ResxLocalizationOptionsValidator, ICollection, IApplicationBuilder, IOptions

### Community 173 - "v1/AddProduct/Request.cs"
Cohesion: 0.14
Nodes (13): Products.Endpoints.Stores.v1.AddProduct, ProductTemplateId, RequestBody, Request, Body, Id, RequestBody, Description (+5 more)

### Community 174 - "SendSecurityAlertRequestHandler"
Cohesion: 0.27
Nodes (9): SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, CancellationToken, IOptions, RequestLocalizationOptions, Task (+1 more)

### Community 175 - "PaginationResponse"
Cohesion: 0.07
Nodes (25): Products.Endpoints.Stores.v1.My.AuditLog, JsonElement, AuditLogDto, DateTimeOffset, PaginationResponse, HasNext, HasPrevious, NextPageNumber (+17 more)

### Community 176 - "Common.Application.Options"
Cohesion: 0.08
Nodes (19): Notifications.Application.Otp, Notifications.Application.Sms, Notifications.Infrastructure.Devices, Notifications.Application.Push, Common.Application.Caching, Common.Infrastructure.RateLimiting, Notifications.Infrastructure.Push, Notifications.Infrastructure.Sms (+11 more)

### Community 177 - ".AddProductToMyStoreAsync"
Cohesion: 0.18
Nodes (9): Products.Endpoints.Stores.v1.My.AddProduct, CancellationToken, ProductTemplate, RouteGroupBuilder, Store, Task, Endpoint, Response (+1 more)

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
Cohesion: 0.25
Nodes (8): RequestBody, Request, Body, Id, RequestBody, Address, Description, Name

### Community 187 - "ReverseProxyOptions"
Cohesion: 0.18
Nodes (9): ForwardedHeadersOptions, ReverseProxyOptions, ForwardLimit, IsEnabled, TrustedNetworks, ReverseProxyOptionsValidator, IReadOnlyList, IConfiguration (+1 more)

### Community 188 - "BoundedCaptureStream"
Cohesion: 0.18
Nodes (7): HttpResponse, BoundedCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 190 - "ServiceAccountTokenCache"
Cohesion: 0.11
Nodes (11): KeycloakPaths, CancellationToken, DateTimeOffset, SemaphoreSlim, Task, TimeSpan, ServiceAccountTokenCache, AccessToken (+3 more)

### Community 191 - "Split-Deployment PoC"
Cohesion: 0.22
Nodes (8): Concurrent safety, Cross-process call path, Files added by this PoC, How it works, How to run, Split-Deployment PoC, What this proves, README (Boilerplate Overview)

### Community 192 - "ProblemDetails"
Cohesion: 0.43
Nodes (6): ProblemDetails, ResxLocalizer, EndpointFilterDelegate, EndpointFilterInvocationContext, IStringLocalizer, ValueTask

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

### Community 197 - ".AddPushServices"
Cohesion: 0.25
Nodes (6): CancellationToken, Task, IPushGateway, IConfiguration, IServiceCollection, Setup

### Community 198 - "InventoryModule"
Cohesion: 0.15
Nodes (10): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, InventoryModule, ActivitySourceNames, MeterNames (+2 more)

### Community 199 - "OpenApiOptions"
Cohesion: 0.22
Nodes (9): OpenApiOptions, ContactEmail, ContactName, Description, EnableSwagger, LicenseName, LicenseUrl, Title (+1 more)

### Community 200 - "KeyValuePair"
Cohesion: 0.19
Nodes (7): KeyValuePair, IEnumerable, ActivitySource, Counter, Meter, NotificationsTelemetry, UpDownCounter

### Community 201 - "KeycloakScopes"
Cohesion: 0.20
Nodes (9): Devices, Hangfire, KeycloakScopes, Products, ProductTemplates, Sessions, StockReservations, Stores (+1 more)

### Community 202 - "Request"
Cohesion: 0.22
Nodes (9): Guid, Request, ClientId, DeviceId, DeviceName, Otp, PhoneNumber, PushToken (+1 more)

### Community 203 - "SmsRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, SmsRateLimitingPolicy (+1 more)

### Community 210 - "ConfigureSwaggerOptions"
Cohesion: 0.28
Nodes (7): ApiVersionDescription, IApiVersionDescriptionProvider, IConfigureOptions, OpenApiInfo, IOptions, SwaggerGenOptions, ConfigureSwaggerOptions

### Community 211 - "NotificationsModule"
Cohesion: 0.15
Nodes (10): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, NotificationsModule, ActivitySourceNames, MeterNames (+2 more)

### Community 212 - "Setup"
Cohesion: 0.33
Nodes (4): ApiVersionSet, Setup, IEndpointRouteBuilder, IServiceCollection

### Community 213 - "ProductsTelemetry"
Cohesion: 0.40
Nodes (4): ActivitySource, Counter, Meter, ProductsTelemetry

### Community 214 - "Common.Application.FeatureManagement"
Cohesion: 0.19
Nodes (5): Common.Application.FeatureManagement, IAM.Domain.Captcha, IAM.Infrastructure.Captcha.Services, IAM.Application.Captcha.Services, CaptchaErrors

### Community 215 - "Request"
Cohesion: 0.29
Nodes (6): Products.Endpoints.ProductTemplates.v1.Create, Request, Brand, Color, Model, RequestValidator

### Community 216 - "ProductTemplateId"
Cohesion: 0.11
Nodes (15): Products.Endpoints.ProductTemplates.v1.Deactivate, Products.Endpoints.ProductTemplates.v1.Get, Products.Endpoints.ProductTemplates.v1.Activate, ProductTemplateId, DefaultIdType, ProductTemplateId, Request, Id (+7 more)

### Community 217 - "Request"
Cohesion: 0.22
Nodes (8): Products.Endpoints.ProductTemplates.v1.Search, Constants, Request, Brand, Color, Model, SearchTerm, RequestValidator

### Community 218 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 219 - "SendForLogin/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForLogin, Request, CaptchaToken, PhoneNumber, RequestValidator

### Community 220 - "Seeder"
Cohesion: 0.31
Nodes (7): ILogger, LoggerMessage, ProductsDbContext, Seeder, CancellationToken, List, Task

### Community 221 - "Keycloak realm as code"
Cohesion: 0.25
Nodes (7): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Secrets, Seed users (dev / test only)

### Community 222 - "IntegrationEventConverter"
Cohesion: 0.14
Nodes (10): DateTimeOffset, ModelConfigurationBuilder, IntegrationEventConverter, JsonSerializerOptions, UtcDateTimeOffsetConverter, DateTimeOffset, DateTimeOffset, ModelConfigurationBuilder (+2 more)

### Community 223 - "RedisOtpService"
Cohesion: 0.13
Nodes (11): IFusionCache, DummyOtpService, CancellationToken, IConnectionMultiplexer, IOptions, Task, TimeSpan, RedisOtpService (+3 more)

### Community 224 - "AuditLogRetentionService"
Cohesion: 0.06
Nodes (32): Common.Infrastructure.Persistence.Auditing, Common.Infrastructure.Persistence.AuditLog, SaveChangesInterceptor, AuditLogOptions, PurgeBatchSize, RetentionDays, AuditLogOptionsValidator, ApplyAuditingInterceptor (+24 more)

### Community 225 - "BackgroundJobsOptions"
Cohesion: 0.29
Nodes (7): BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount, BackgroundJobsOptionsValidator

### Community 226 - ".HandleAsync"
Cohesion: 0.18
Nodes (11): GetSeedUserIdsRequest, GetSeedUserIdsResponse, ICollection, CancellationToken, Task, GetSeedUserIdsRequestHandler, CancellationToken, IResult (+3 more)

### Community 227 - "BackgroundJobsModule"
Cohesion: 0.22
Nodes (8): ICoreModule, BackgroundJobsModule, ActivitySourceNames, MeterNames, Name, StartupPriority, IEndpointRouteBuilder, IEnumerable

### Community 228 - "OtpOptions"
Cohesion: 0.17
Nodes (10): OtpOptions, ExpirationInMinutes, Length, MaxSendsPerPhonePerWindow, PhoneQuotaWindowMinutes, ResendIntervalSeconds, OtpOptionsValidator, IFusionCache (+2 more)

### Community 229 - ".AddCommonOptions"
Cohesion: 0.20
Nodes (6): Setup, IConfiguration, IHostEnvironment, IServiceCollection, ValidationContextExtensions, ValidationContext

### Community 230 - ".ReserveStockAsync"
Cohesion: 0.11
Nodes (17): Inventory.Endpoints.StockReservations.v1.Reserve, CancellationToken, RouteGroupBuilder, Task, Endpoint, DateTimeOffset, DefaultIdType, RequestBody (+9 more)

### Community 231 - "Request"
Cohesion: 0.33
Nodes (6): Products.Endpoints.Stores.v1.My.Update, Request, Address, Description, Name, RequestValidator

### Community 232 - "Common.Domain.Devices"
Cohesion: 0.29
Nodes (6): IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, Common.Domain.Devices, DeviceSessionConstants, Request, Id, RequestValidator

### Community 233 - "StockReservationExpirySweepService"
Cohesion: 0.24
Nodes (9): CancellationToken, Exception, Guid, ILogger, IOptions, LoggerMessage, Task, TimeProvider (+1 more)

### Community 234 - "Setup"
Cohesion: 0.29
Nodes (4): IApplicationBuilder, IWebHostEnvironment, Type, Setup

### Community 235 - "Stores/v1/Get/Request.cs"
Cohesion: 0.40
Nodes (4): Products.Endpoints.Stores.v1.Get, Request, Id, RequestValidator

### Community 236 - ".UpdateCurrentPushToken"
Cohesion: 0.18
Nodes (9): Notifications.Infrastructure.Devices.UpdateCurrentPushToken, CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint, Request, PushToken (+1 more)

### Community 237 - "ReCaptchaResponse"
Cohesion: 0.25
Nodes (7): DateTime, ReCaptchaResponse, ChallengeTs, ErrorCodes, Hostname, Score, Success

### Community 238 - "StockReservations/v1/Get/Request.cs"
Cohesion: 0.40
Nodes (4): Inventory.Endpoints.StockReservations.v1.Get, Request, Id, RequestValidator

### Community 239 - ".TryReadFromJsonAsync"
Cohesion: 0.33
Nodes (4): HttpContent, CancellationToken, Task, HttpContentJsonExtensions

### Community 240 - "StringExtensions"
Cohesion: 0.33
Nodes (3): Common.Domain.Extensions, SearchValues, StringExtensions

### Community 241 - "IAM.Endpoints.Common.Validations"
Cohesion: 0.17
Nodes (10): IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, IAM.Endpoints.Common.Validations, IRuleBuilder, IRuleBuilderOptions, IResxLocalizer, CommonValidations, Request, CaptchaToken (+2 more)

### Community 242 - ".AddCustomSwagger"
Cohesion: 0.20
Nodes (7): IOpenApiSchema, ISchemaFilter, SchemaFilterContext, IConfigureOptions, IServiceCollection, SwaggerGenOptions, StronglyTypedIdSchemaFilter

### Community 243 - ".RemoveProductAsync"
Cohesion: 0.18
Nodes (9): CancellationToken, RouteGroupBuilder, Task, Endpoint, ProductId, StoreId, Request, Id (+1 more)

### Community 244 - ".AddAuthInfrastructure"
Cohesion: 0.25
Nodes (6): IAuthorizationHandler, IAuthorizationPolicyProvider, IConfigureOptions, IServiceCollection, JwtBearerOptions, Setup

### Community 245 - "Host.Swagger"
Cohesion: 0.22
Nodes (5): Host.Swagger, IOperationFilter, OpenApiOperation, OperationFilterContext, RemoveDefaultResponseSchemaFilter

### Community 246 - "KeycloakPermission"
Cohesion: 0.25
Nodes (3): KeycloakPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder

### Community 247 - "StockReservationExpirySweepJobRegistrar"
Cohesion: 0.27
Nodes (8): IHostedService, CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task, StockReservationExpirySweepJobRegistrar

### Community 248 - "TokenResponseRepresentation"
Cohesion: 0.33
Nodes (6): TokenResponseRepresentation, AccessToken, ExpiresIn, RefreshExpiresIn, RefreshToken, SessionState

### Community 249 - "IntegrationEventOutbox"
Cohesion: 0.12
Nodes (12): Inventory.Application.IntegrationEventHandlers, Common.IntegrationEvents, Products.Application.Products.DomainEventHandlers.v1, Products.Application.Stores.DomainEventHandlers.v1, Common.Application.EventBus, Lock, IIntegrationEventOutbox, IntegrationEventOutbox (+4 more)

### Community 250 - "V1ProductCreatedDomainEvent"
Cohesion: 0.40
Nodes (5): CancellationToken, Task, ProductCreatedIntegrationEventPublishingHandler, ProductId, V1ProductCreatedDomainEvent

### Community 251 - "StoreId"
Cohesion: 0.40
Nodes (3): StoreId, DefaultIdType, StoreId

### Community 252 - "ICurrentUser"
Cohesion: 0.20
Nodes (8): ICurrentUser, Id, IdAsString, Roles, SessionId, ICollection, HttpContextExtensions, HttpContext

### Community 260 - ".SendCoreAsync"
Cohesion: 0.24
Nodes (7): IReadOnlyDictionary, IReadOnlyList, PushMessage, CancellationToken, IEnumerable, IReadOnlyList, Task

### Community 261 - "TokenCreateRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenCreateRateLimitingPolicy (+1 more)

### Community 262 - ".ActivateProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 263 - "ReservationStatus"
Cohesion: 0.29
Nodes (6): ReservationStatus, Active, Committed, Expired, Released, RequiresReconciliation

### Community 264 - ".AddNetGsm"
Cohesion: 0.33
Nodes (5): IConfiguration, IFusionCache, IOptions, IServiceCollection, Setup

### Community 265 - ".UpdateMyStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 267 - ".InvokeAsync"
Cohesion: 0.33
Nodes (5): IFeatureManagerSnapshot, EndpointFilterDelegate, EndpointFilterInvocationContext, IResxLocalizer, ValueTask

### Community 268 - "LogProductCatalogChangeHandler"
Cohesion: 0.24
Nodes (8): CancellationToken, DefaultIdType, IFusionCache, ILogger, IOptions, LoggerMessage, Task, LogProductCatalogChangeHandler

### Community 269 - "Request"
Cohesion: 0.29
Nodes (7): Products.Endpoints.Stores.v1.Search, Request, Address, Description, Name, SearchTerm, RequestValidator

### Community 270 - "DefaultResponsesOperationFilter"
Cohesion: 0.70
Nodes (3): OpenApiOperation, OperationFilterContext, DefaultResponsesOperationFilter

### Community 271 - "v1/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Probe.v1, Request, Count, RequestValidator

### Community 272 - "RequireFeatureFilter"
Cohesion: 0.25
Nodes (6): RequireFeatureFilter, ActivitySource, Counter, Meter, RouteHandlerBuilderExtensions, RouteHandlerBuilder

### Community 273 - ".FixedWindow"
Cohesion: 0.14
Nodes (12): CustomRateLimitingOptionsValidator, FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit, FixedWindowValidator, RateLimitPartitions (+4 more)

### Community 274 - "GetProductRequest"
Cohesion: 0.27
Nodes (8): Products.Infrastructure.InterModuleRequestHandlers, Common.InterModuleRequests.Products, GetProductRequest, GetProductResponse, DefaultIdType, CancellationToken, Task, GetProductRequestHandler

### Community 276 - "Inventory.Endpoints.StockReservations"
Cohesion: 0.40
Nodes (3): Inventory.Endpoints.StockReservations, RouteGroupBuilder, Setup

### Community 277 - "SendResponseBody"
Cohesion: 0.50
Nodes (4): SendResponseBody, Code, Description, JobId

### Community 278 - "ProductId"
Cohesion: 0.08
Nodes (15): Products.Endpoints.Products.v1.My.Get, Products.Endpoints.Stores.v1.My.RemoveProduct, Products.Endpoints.Products.v1.Get, StronglyTypedIdHelper, DefaultIdType, ProductId, Request, Id (+7 more)

### Community 279 - "V1StockLevelCreatedDomainEvent"
Cohesion: 0.40
Nodes (4): Inventory.Domain.StockLevels.DomainEvents.v1, DefaultIdType, StockLevelId, V1StockLevelCreatedDomainEvent

### Community 281 - ".CommitStockReservationAsync"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 282 - "ProductsModule.cs"
Cohesion: 0.10
Nodes (10): Common.Infrastructure.Modules, Inventory.Endpoints, Common.Endpoints.Versioning, Products.Infrastructure.RateLimiting, Inventory.Infrastructure.Gateway, Products.Endpoints, Common.Infrastructure.Resiliency, Inventory.Application.Gateway (+2 more)

### Community 283 - "InventoryTelemetry"
Cohesion: 0.40
Nodes (4): ActivitySource, Counter, Meter, InventoryTelemetry

### Community 284 - "ProblemDetailsContext"
Cohesion: 0.33
Nodes (4): ProblemDetailsContext, ProblemDetailsServiceExtensions, IProblemDetailsService, Task

### Community 285 - "Products.Endpoints.Probe"
Cohesion: 0.40
Nodes (3): Products.Endpoints.Probe, RouteGroupBuilder, Setup

### Community 286 - ".SeedProductTemplatesAsync"
Cohesion: 0.60
Nodes (3): CancellationToken, List, Task

### Community 289 - ".AddModules"
Cohesion: 0.13
Nodes (13): LoadAll, Names, ModulesOptions, EnabledModules, ModulesOptionsValidator, IReadOnlyList, Type, Assembly (+5 more)

### Community 291 - ".DeactivateStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 293 - ".RemoveMyProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 298 - "VersionNeutral/Get/Request.cs"
Cohesion: 0.40
Nodes (4): IAM.Endpoints.Users.VersionNeutral.Get, Request, Id, RequestValidator

## Knowledge Gaps
- **832 isolated node(s):** `Id`, `IdAsString`, `Roles`, `SessionId`, `JwtClaimNames` (+827 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1893 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **90 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `Common.InterModuleRequests.Contracts`, `MassTransitInterModuleRequestClient`, `NotificationPayload`, `DeviceRegistryReconciliationService`, `Common.Domain.ResultMonad`, `.FixedWindow`, `EnrichLogsWithUserInfoMiddleware`, `Common.Infrastructure.Persistence`, `Setup`, `.AddCommonCaching`, `OutboxOptions`, `ISearchLanguageResolver`, `CustomValidator`, `ProductsModule.cs`, `RequestResponseBodyLoggingMiddleware`, `Request`, `.AddModules`, `Request`, `ObservabilityOptions`, `KeycloakOptions`, `ResxLocalizationOptions`, `CachingOptions`, `SmsOptions`, `RabbitMqOptions`, `Inventory.Domain.StockReservations`, `ReverseProxyOptions`, `RequestLoggingOptions`, `IAMModule.cs`, `CorsOptions`, `InventoryOptions`, `OpenApiOptions`, `Request`, `CaptchaOptions`, `Common.Application.FeatureManagement`, `PushOptions`, `Program.cs`, `Common.Domain.StronglyTypedIds`, `.AddCommonPersistence`, `AuditLogRetentionService`, `BackgroundJobsOptions`, `OtpOptions`, `ResiliencyOptions`, `.AddCommonOptions`, `Common.Application.BackgroundJobs`, `OutboxModule.cs`, `Host.Swagger`, `IntegrationEventOutbox`, `FullTextSearchOptions`, `.AddCustomHealthChecks`?**
  _High betweenness centrality (0.240) - this node is a cross-community bridge._
- **Why does `Result` connect `Result` to `.SendCoreAsync`, `.UpdateProductAsync`, `.ActivateProductTemplateAsync`, `InterModuleRequestHandler`, `.SingleAsResult`, `.UpdateMyStoreAsync`, `Error`, `ProductTemplate`, `VerifyPhoneOtpResponse`, `.SendOtp`, `.GetMeAsync`, `IDbContext`, `Response`, `ISmsGateway`, `DomainEvent`, `Response`, `ApplicationUserId`, `.SendAsync`, `.CommitStockReservationAsync`, `NetGsmSmsGateway`, `KeycloakTokenClient`, `.CreateStoreAsync`, `.ReserveSeriesAsync`, `HttpWarehouseGateway`, `.SaveChangesAsync`, `.DeactivateStoreAsync`, `StockReservation`, `.RefreshToken`, `.RemoveMyProductAsync`, `IProductsDbContext`, `ICaptchaService`, `.BindDeviceAsync`, `PaginationResponse`, `.AddProductToMyStoreAsync`, `Response`, `DummyPushGateway`, `ReCaptchaService`, `.SendOtp`, `.SearchProductTemplatesAsync`, `Response`, `.AddPushServices`, `Response`, `.CreateMyStoreAsync`, `.SearchStoresAsync`, `V1StockReservationCommitConflictDetectedDomainEvent`, `ResultTelemetryExtensions`, `IInterModuleRequestClient`, `Response`, `.GetClientKey`, `.SearchMyProductsAsync`, `.SearchStoreProductsAsync`, `ThrottledSmsGateway`, `.ReserveStockAsync`, `.ReleaseStockReservationAsync`, `.UpdateCurrentPushToken`, `.IsRegisteredAsync`, `.RemoveProductAsync`, `Response`, `.HandleWarehouseWebhookAsync`, `.TapWhenFeatureEnabledAsync`?**
  _High betweenness centrality (0.145) - this node is a cross-community bridge._
- **Why does `ApplicationUserId` connect `ApplicationUserId` to `NotificationPayload`, `DeviceRegistryReconciliationService`, `KeycloakAdminClient`, `InterModuleRequestHandler`, `Response`, `KeycloakPermissionAuthorizationHandler`, `Response`, `ProductId`, `DeviceRegistration`, `.SendAsync`, `CustomValidator`, `AuditableEntityResponse`, `KeycloakTokenClient`, `For`, `CurrentUser`, `VersionNeutral/Get/Request.cs`, `.BindDeviceAsync`, `SendSecurityAlertRequestHandler`, `PaginationResponse`, `AuditableEntity`, `Response`, `Request`, `V1StoreCreatedDomainEvent`, `IStronglyTypedId`, `.CreateMyStoreAsync`, `IntegrationEvent`, `.SearchStoresAsync`, `.Configure`, `Store`, `Response`, `Seeder`, `.HandleAsync`, `Response`, `ICurrentUser`?**
  _High betweenness centrality (0.101) - this node is a cross-community bridge._
- **What connects `Id`, `IdAsString`, `Roles` to the rest of the system?**
  _832 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Common.InterModuleRequests.Contracts` be split into smaller, more focused modules?**
  _Cohesion score 0.07756813417190776 - nodes in this community are weakly interconnected._
- **Should `NotificationPayload` be split into smaller, more focused modules?**
  _Cohesion score 0.0597567424643046 - nodes in this community are weakly interconnected._
- **Should `DeviceRegistryReconciliationService` be split into smaller, more focused modules?**
  _Cohesion score 0.07317073170731707 - nodes in this community are weakly interconnected._