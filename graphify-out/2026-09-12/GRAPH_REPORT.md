# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-09-10)

## Corpus Check
- 515 files · ~73,137 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 4165 nodes · 7405 edges · 369 communities (268 shown, 96 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 250 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `06819997`
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
- IInterModuleRequest
- .SendOtp
- AdminRepresentations.cs
- KeycloakPermissionAuthorizationHandler
- ISmsGateway
- .UseModule
- DomainEvent
- Common.Infrastructure.Persistence
- ApplicationUserId
- BoundedRequestCaptureStream
- DeviceRegistration
- StockReservation
- .SendAsync
- NetGsmSmsGateway
- IEvent
- RequestResponseBodyLoggingMiddleware
- KeycloakTokenClient
- Result
- Product
- .ForUser
- Endpoint
- .AddProductAsync
- KeycloakPermissionClient
- OutboxMessage
- CustomRateLimitingOptions
- Inventory.Domain.StockReservations.DomainEvents.v1
- .RefreshToken
- ObservabilityOptions
- IamTelemetry
- Request
- .SaveWithOutboxAsync
- CreateStoreRateLimitingPolicy
- KeycloakOptions
- .CreateTokens
- Common.Domain.Events
- Outbox Misuse Check
- AggregateRoot
- Add Integration Event Command
- Response
- ProductsDbContext
- ReCaptchaService
- Inventory.Domain.StockReservations
- Notifications.Application.Otp
- RegisterRateLimitingPolicy
- DummySmsGateway
- Cross-Module Reference Violation
- OutboxMetricsJob
- .SearchProductTemplatesAsync
- Bogus Test Data
- Request
- NotificationsDbContext
- RequestLoggingOptions
- Common.InterModuleRequests
- Common.Application.Options
- OutboxModule
- AuditableEntityResponse
- ValueObject
- V1StoreCreatedDomainEvent
- IStronglyTypedId
- Full-Text Search
- Response
- .WriteAsync
- AsNoTracking Coverage Check
- CaptchaOptions
- Common.Application.Validation
- My/Create/Request.cs
- CheckRegistrationRateLimitingPolicy
- IntegrationEventOutbox
- EventDispatcher
- .SearchStoresAsync
- Policies.CreateStore.cs
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
- Endpoint
- .AddCommonPersistence
- .SearchMyProductsAsync
- DatabaseSeederOrchestrator
- AuditLogRetentionJobRegistrar
- .SearchStoreProductsAsync
- UserRepresentation
- OutboxDbContext
- ResiliencyOptions
- HttpContextTargetingContextAccessor
- Common.Application.BackgroundJobs
- CreateStockLevelOnProductCreatedHandler
- .AddKeycloakInfrastructure
- Common.Infrastructure.Persistence.Outbox
- .ReleaseStockReservationAsync
- StockLevel
- .IsRegisteredAsync
- Common.Application.Persistence
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
- SecurityHeadersMiddleware
- Otp/VersionNeutral/Setup.cs
- .AddServices
- Setup
- .RevokeSession
- FeatureFlags
- OutboxOptions
- ISearchLanguageResolver
- SwaggerDefaultValues
- CustomValidator
- InterModuleRequestHandler
- Request
- IntegrationEventHandlerBase
- .FixedWindow
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
- RequestBody
- SendSecurityAlertRequestHandler
- PaginationResponse
- Policies
- .AddProductToMyStoreAsync
- .BindDeviceAsync
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
- .AddNotificationsSignalR
- ProductsTelemetry
- IAM.Application.Captcha.Services
- Request
- ProductTemplateId
- Request
- Response
- FirebasePushGateway.cs
- Products.Domain.ProductTemplates
- Keycloak realm as code
- UtcDateTimeOffsetConverter
- RedisOtpService
- AuditLogRetentionService
- BackgroundJobsOptions
- .HandleAsync
- BackgroundJobsModule
- OtpOptions
- .AddCommonOptions
- RequestBody
- .UpdateMyStoreAsync
- Common.Domain.Devices
- StockReservationExpirySweepService
- Setup
- PaginationRequestValidator
- .UpdateCurrentPushToken
- ReCaptchaResponse
- NotificationsHub
- IAM.Infrastructure.Keycloak
- KeycloakUser
- IAM.Endpoints.Common.Validations
- Host.Swagger
- .RemoveProductAsync
- .AddAuthInfrastructure
- RemoveDefaultResponseSchemaFilter
- KeycloakPermission
- StockReservationExpirySweepJobRegistrar
- CachedCaptchaService
- Common.Application.EventBus
- Request
- Notifications.Application.Sms
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
- Common.Infrastructure.Persistence.AuditLog
- ReservationStatus
- .AddNetGsm
- AuditLogOptions
- .InvokeAsync
- LogProductCatalogChangeHandler
- Request
- DefaultResponsesOperationFilter
- Notifications.Infrastructure.Telemetry
- RequireFeatureFilter
- FixedWindow
- GetProductRequest
- .Capture
- INotificationDispatcher
- SendResponseBody
- .TryDeserialize
- DummyWarehouseGateway
- .GetAuditLogAsync
- .CommitStockReservationAsync
- InventoryModule.cs
- .AddOtpServices
- ProblemDetailsContext
- Probe/Setup.cs
- SendForRegistration/Request.cs
- .DeactivateProductTemplateAsync
- IAM.Infrastructure.Auth
- ModulesOptions
- .AddStockReservationExpirySweep
- .DeactivateStoreAsync
- .RemoveMyProductAsync
- StronglyTypedIdBinder
- SignalROptions
- .LogRoleAssignmentFailed
- V1StockReservationReleaseAttemptAbandonedDomainEvent
- VersionNeutral/Get/Request.cs
- V1StockReservationReleaseAttemptStartedDomainEvent
- V1StockReservationReservedDomainEvent
- .AddCustomSwagger
- .AddServices
- .AddServices
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
- `PushErrors` --references--> `Error`  [EXTRACTED]
  src/Modules/Notifications/Notifications.Application/Push/PushErrors.cs → src/Common/Common.Domain/ResultMonad/Error.cs
- `AuditLogDto` --references--> `ApplicationUserId`  [EXTRACTED]
  src/Common/Common.Application/AuditLog/AuditLogDto.cs → src/Common/Common.Domain/StronglyTypedIds/ApplicationUserId.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Local Infrastructure Stack** — docker_compose_postgres, docker_compose_rabbitmq, docker_compose_redis, docker_compose_aspire_dashboard [EXTRACTED 1.00]

## Communities (369 total, 96 thin omitted)

### Community 0 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.12
Nodes (17): Common.Application.FeatureManagement, IAM.Endpoints.Otp, Notifications.Application.Persistence, Common.InterModuleRequests.IAM, Common.InterModuleRequests.Contracts, Common.Infrastructure.Extensions, Notifications.Infrastructure.InterModuleRequestHandlers, IAM.Endpoints.Tokens.VersionNeutral.Revoke (+9 more)

### Community 1 - "NotificationPayload"
Cohesion: 0.34
Nodes (8): IHubContext, Task, INotificationsClient, NotificationPayload, CancellationToken, IReadOnlyList, Task, SignalRNotificationDispatcher

### Community 2 - "OutboxProcessor"
Cohesion: 0.22
Nodes (11): IPublishEndpoint, CancellationToken, Exception, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task (+3 more)

### Community 3 - "DeviceRegistryReconciliationService"
Cohesion: 0.07
Nodes (30): Notifications.Infrastructure.Devices, DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator, IReadOnlyCollection, GetActiveSessionIdsRequest (+22 more)

### Community 5 - "KeycloakAdminClient"
Cohesion: 0.27
Nodes (12): HttpRequestMessage, CancellationToken, Func, HttpClient, HttpResponseMessage, IFusionCache, IOptions, IReadOnlyList (+4 more)

### Community 6 - "RedisFixedWindowRateLimiter"
Cohesion: 0.13
Nodes (16): RateLimiter, RateLimiterStatistics, RateLimitLease, FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration (+8 more)

### Community 7 - "ApplyAuditingInterceptor"
Cohesion: 0.09
Nodes (18): SaveChangesInterceptor, ISearchLocalized, Language, ApplyAuditingInterceptor, CancellationToken, DbContextEventData, InterceptionResult, TimeProvider (+10 more)

### Community 8 - "FirebasePushGateway"
Cohesion: 0.24
Nodes (8): FirebaseApp, FirebaseMessaging, IDisposable, Exception, ILogger, LoggerMessage, TimeSpan, FirebasePushGateway

### Community 9 - "Request"
Cohesion: 0.20
Nodes (10): RequestBody, Request, Body, Id, RequestBody, Description, Name, Price (+2 more)

### Community 10 - "Error"
Cohesion: 0.06
Nodes (19): SearchValues, StringLocalizerExtensions, IStringLocalizer, StringExtensions, Error, Key, ParameterName, StatusCode (+11 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.16
Nodes (13): Common.Application.Search, Products.Endpoints.Products.v1.My.Update, Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Products.Endpoints.Products.v1.Update, Products.Infrastructure.Telemetry, Products.Application.Persistence (+5 more)

### Community 12 - "IInterModuleRequest"
Cohesion: 0.17
Nodes (11): IInterModuleRequest, OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, VerifyPhoneOtpResponseExtensions (+3 more)

### Community 13 - ".SendOtp"
Cohesion: 0.09
Nodes (19): SendPhoneOtpRequest, SendPhoneOtpResponse, CancellationToken, Task, CancellationToken, IFeatureManager, RouteGroupBuilder, Task (+11 more)

### Community 14 - "AdminRepresentations.cs"
Cohesion: 0.12
Nodes (16): CredentialRepresentation, Temporary, Type, Value, ErrorRepresentation, Error, ErrorMessage, Field (+8 more)

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
Cohesion: 0.09
Nodes (22): DomainEvent, CreatedOn, Id, Version, DateTimeOffset, DefaultIdType, ProductId, V1ProductDescriptionUpdatedDomainEvent (+14 more)

### Community 19 - "Common.Infrastructure.Persistence"
Cohesion: 0.15
Nodes (9): Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Notifications.Infrastructure.Persistence, Inventory.Infrastructure.Persistence, Common.Infrastructure.EventBus, Common.Infrastructure.Persistence.Auditing, Common.Infrastructure.Persistence.DbContext, Setup (+1 more)

### Community 20 - "ApplicationUserId"
Cohesion: 0.14
Nodes (15): ApplicationUserId, IsEmpty, Value, DefaultIdType, CancellationToken, IReadOnlyList, Task, IKeycloakAdminClient (+7 more)

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.14
Nodes (8): SeekOrigin, BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position, Stream

### Community 22 - "DeviceRegistration"
Cohesion: 0.12
Nodes (15): DateTimeOffset, DefaultIdType, Guid, DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive (+7 more)

### Community 23 - "StockReservation"
Cohesion: 0.17
Nodes (11): DefaultIdType, StockReservation, LastReleaseAttemptReference, ProductId, ProviderReference, Quantity, ReservationDeadline, Status (+3 more)

### Community 24 - ".SendAsync"
Cohesion: 0.11
Nodes (16): CancellationToken, Task, DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse, IReadOnlyList, CancellationToken, RouteGroupBuilder, Task (+8 more)

### Community 25 - "NetGsmSmsGateway"
Cohesion: 0.16
Nodes (14): SendRequestBody, SendResponseBody, CancellationToken, Exception, HttpClient, ILogger, IOptions, JsonSerializerOptions (+6 more)

### Community 26 - "IEvent"
Cohesion: 0.14
Nodes (11): CancellationToken, Task, CancellationToken, Task, CancellationToken, Task, IEvent, CreatedOn (+3 more)

### Community 27 - "RequestResponseBodyLoggingMiddleware"
Cohesion: 0.22
Nodes (7): IDiagnosticContext, HttpContext, IList, IOptions, PathString, RequestDelegate, RequestResponseBodyLoggingMiddleware

### Community 28 - "KeycloakTokenClient"
Cohesion: 0.14
Nodes (19): JsonWebTokenHandler, CancellationToken, Dictionary, Error, Exception, HttpClient, ILogger, IOptions (+11 more)

### Community 29 - "Result"
Cohesion: 0.14
Nodes (14): Result, Error, IsFailure, Success, Value, Func, Task, AsyncExtensions (+6 more)

### Community 30 - "Product"
Cohesion: 0.06
Nodes (31): ProductTemplate, ProductTemplateId, Store, StoreId, Product, Description, Language, Name (+23 more)

### Community 32 - "Endpoint"
Cohesion: 0.29
Nodes (5): Products.Endpoints.Stores, RouteGroupBuilder, Setup, RouteGroupBuilder, Endpoint

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
Cohesion: 0.10
Nodes (18): CheckRegistrationRateLimitingPolicy, RegisterRateLimitingPolicy, SmsRateLimitingPolicy, CustomRateLimitingOptions, CheckRegistration, CreateStore, ExemptPathPrefixes, Global (+10 more)

### Community 37 - "Inventory.Domain.StockReservations.DomainEvents.v1"
Cohesion: 0.09
Nodes (18): Inventory.Domain.StockReservations.DomainEvents.v1, DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationCommitConflictDetectedDomainEvent, DateTimeOffset, DefaultIdType, StockReservationId (+10 more)

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

### Community 45 - ".CreateTokens"
Cohesion: 0.12
Nodes (18): BindDeviceSessionRequest, BindDeviceSessionResponse, Guid, CancellationToken, Task, IKeycloakTokenClient, DateTimeOffset, KeycloakTokens (+10 more)

### Community 46 - "Common.Domain.Events"
Cohesion: 0.13
Nodes (6): Products.Domain.Products.DomainEvents.v1, Common.Domain.Events, Products.Domain.Stores.DomainEvents.v1, Inventory.Domain.StockLevels.DomainEvents.v1, ProductId, V1ProductCreatedDomainEvent

### Community 48 - "AggregateRoot"
Cohesion: 0.08
Nodes (25): AggregateRoot, Events, Id, Version, IReadOnlyCollection, List, IAggregateRoot, Events (+17 more)

### Community 50 - "Response"
Cohesion: 0.09
Nodes (20): IAM.Endpoints.Users.VersionNeutral.Search, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, SearchTerm, RequestValidator (+12 more)

### Community 51 - "ProductsDbContext"
Cohesion: 0.12
Nodes (14): AuditLogEntryConfiguration, ModelBuilder, ModelBuilder, DbContextOptions, DbSet, ILogger, ModelBuilder, ProductTemplate (+6 more)

### Community 52 - "ReCaptchaService"
Cohesion: 0.21
Nodes (10): FormUrlEncodedContent, ReCaptchaResponse, CancellationToken, Exception, HttpClient, ILogger, IOptions, LoggerMessage (+2 more)

### Community 53 - "Inventory.Domain.StockReservations"
Cohesion: 0.07
Nodes (21): Products.Infrastructure.InterModuleRequestHandlers, Inventory.Infrastructure.InterModuleRequestHandlers, Inventory.Application.Persistence, Inventory.Domain.StockReservations, Inventory.Domain.StockReservations.Errors, Inventory.Endpoints.StockReservations.v1.Get, Inventory.Endpoints.StockReservations.v1.Reserve, Inventory.Infrastructure.Telemetry (+13 more)

### Community 54 - "Notifications.Application.Otp"
Cohesion: 0.24
Nodes (4): Notifications.Application.Otp, Common.Application.Caching, Notifications.Infrastructure.Otp, Setup

### Community 55 - "RegisterRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, RegisterRateLimitingPolicy (+1 more)

### Community 56 - "DummySmsGateway"
Cohesion: 0.38
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, DummySmsGateway

### Community 58 - "OutboxMetricsJob"
Cohesion: 0.13
Nodes (14): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxMetricsJob (+6 more)

### Community 59 - ".SearchProductTemplatesAsync"
Cohesion: 0.15
Nodes (11): LikePattern, CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response (+3 more)

### Community 61 - "Request"
Cohesion: 0.25
Nodes (8): ProductTemplateId, Request, Description, Name, Price, ProductTemplateId, Quantity, RequestValidator

### Community 62 - "NotificationsDbContext"
Cohesion: 0.15
Nodes (13): DbSet, INotificationsDbContext, DeviceRegistrations, DbContextOptions, DbSet, ILogger, TimeProvider, NotificationsDbContext (+5 more)

### Community 63 - "RequestLoggingOptions"
Cohesion: 0.11
Nodes (20): IPostConfigureOptions, RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes (+12 more)

### Community 64 - "Common.InterModuleRequests"
Cohesion: 0.29
Nodes (4): Common.InterModuleRequests, IAssemblyReference, Setup, IServiceCollection

### Community 65 - "Common.Application.Options"
Cohesion: 0.10
Nodes (13): Common.Infrastructure.Modules, Products.Endpoints.Probe, Common.Infrastructure.Localization, IAM.Endpoints, Common.Application.Options, IAM.Endpoints.Otp.VersionNeutral, Host.Middlewares, Products.Endpoints (+5 more)

### Community 66 - "OutboxModule"
Cohesion: 0.13
Nodes (16): Action, Exception, IApplicationBuilder, IEndpointRouteBuilder, IEnumerable, IHostApplicationLifetime, ILogger, ILoggerFactory (+8 more)

### Community 67 - "AuditableEntityResponse"
Cohesion: 0.12
Nodes (15): AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, DateTimeOffset, CancellationToken (+7 more)

### Community 68 - "ValueObject"
Cohesion: 0.25
Nodes (4): Common.Domain, IComparable, ValueObject, IEnumerable

### Community 69 - "V1StoreCreatedDomainEvent"
Cohesion: 0.20
Nodes (9): CancellationToken, Task, CancellationToken, Task, SimulateSomeBusinessHandler, StoreCreatedIntegrationEventPublishingHandler, V1StoreCreatedDomainEventHandlers, StoreId (+1 more)

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

### Community 76 - "Common.Application.Validation"
Cohesion: 0.06
Nodes (36): Products.Endpoints.Stores.v1.Deactivate, Products.Endpoints.Stores.v1.My.RemoveProduct, Inventory.Endpoints.StockReservations.v1.Release, Common.Application.ModelBinders, Common.Application.Validation, Products.Endpoints.ProductTemplates.v1.Activate, Products.Endpoints.Products.v1.AuditLog, Request (+28 more)

### Community 77 - "My/Create/Request.cs"
Cohesion: 0.50
Nodes (3): Products.Endpoints.Stores.v1.My.Create, Request, RequestValidator

### Community 78 - "CheckRegistrationRateLimitingPolicy"
Cohesion: 0.25
Nodes (8): IRateLimiterPolicy, CancellationToken, Func, IOptions, OnRejectedContext, ValueTask, CheckRegistrationRateLimitingPolicy, OnRejected

### Community 79 - "IntegrationEventOutbox"
Cohesion: 0.20
Nodes (9): Lock, IntegrationEventOutbox, IReadOnlyList, List, IntegrationEvent, CreatedOn, Id, DateTimeOffset (+1 more)

### Community 80 - "EventDispatcher"
Cohesion: 0.31
Nodes (7): EventDispatcher, ActivitySource, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task

### Community 81 - ".SearchStoresAsync"
Cohesion: 0.15
Nodes (12): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Address (+4 more)

### Community 82 - "Policies.CreateStore.cs"
Cohesion: 0.17
Nodes (8): CreateStoreRateLimitingPolicy, Products.Infrastructure.RateLimiting, RateLimiterOptions, Policies, Action, IEnumerable, RateLimiterOptions, RateLimitingConstants

### Community 83 - ".EnsureNoMigrationsPending"
Cohesion: 0.30
Nodes (6): AutoMigrateMarker, IAutoMigrateMarker, MigrationGuard, ILogger, IServiceProvider, LoggerMessage

### Community 84 - ".Configure"
Cohesion: 0.09
Nodes (21): AuditableEntityConfiguration, EntityTypeBuilder, EntityTypeBuilder, DomainEventConverter, JsonSerializerOptions, StronglyTypedIdValueConverter, DefaultIdType, EntityTypeBuilder (+13 more)

### Community 85 - "ResultTelemetryExtensions"
Cohesion: 0.32
Nodes (4): Activity, ResultTelemetryExtensions, ActivitySource, Task

### Community 86 - "Store"
Cohesion: 0.09
Nodes (19): StoreId, V1StoreAddressUpdatedDomainEvent, StoreId, V1StoreDescriptionUpdatedDomainEvent, StoreId, V1StoreNameUpdatedDomainEvent, IReadOnlyCollection, List (+11 more)

### Community 87 - "PushOptions"
Cohesion: 0.12
Nodes (20): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri, PushOptions (+12 more)

### Community 89 - "Program.cs"
Cohesion: 0.22
Nodes (6): ConfigurationManager, Host, Host.Configurations, Setup, Program, WebApplicationBuilder

### Community 90 - "IInterModuleRequestClient"
Cohesion: 0.10
Nodes (21): Products.Endpoints.Products, IInterModuleRequestClient, GetStockLevelRequest, GetStockLevelResponse, DefaultIdType, CancellationToken, Task, GetStockLevelRequestHandler (+13 more)

### Community 91 - "KeycloakPermissionPolicyProvider"
Cohesion: 0.24
Nodes (8): AuthorizationOptions, AuthorizationPolicy, DefaultAuthorizationPolicyProvider, IAuthorizationPolicyProvider, ConcurrentDictionary, IOptions, Task, KeycloakPermissionPolicyProvider

### Community 92 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.07
Nodes (18): Common.Domain.StronglyTypedIds, Products.Endpoints.Products.v1.My.Get, Inventory.Infrastructure.Persistence.EntityConfigurations, Products.Endpoints.Products.v1.Search, Products.Domain.Products, Common.Application.DTOs, Products.Endpoints.Stores.v1.Get, Common.Application.JsonConverters (+10 more)

### Community 93 - "Endpoint"
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

### Community 97 - "AuditLogRetentionJobRegistrar"
Cohesion: 0.36
Nodes (6): AuditLogRetentionJobRegistrar, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task

### Community 98 - ".SearchStoreProductsAsync"
Cohesion: 0.17
Nodes (11): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Description (+3 more)

### Community 99 - "UserRepresentation"
Cohesion: 0.15
Nodes (13): Dictionary, List, UserRepresentation, Attributes, CreatedTimestamp, Credentials, Email, EmailVerified (+5 more)

### Community 100 - "OutboxDbContext"
Cohesion: 0.12
Nodes (14): DbContext, IOutboxDbContext, OutboxMessages, CancellationToken, DbSet, Task, IConfiguration, IServiceCollection (+6 more)

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

### Community 105 - ".AddKeycloakInfrastructure"
Cohesion: 0.22
Nodes (8): IServiceAccountTokenProvider, HttpClient, IOptions, TimeProvider, ServiceAccountTokenProvider, IOptions, IServiceCollection, Setup

### Community 106 - "Common.Infrastructure.Persistence.Outbox"
Cohesion: 0.13
Nodes (10): Outbox, Common.Infrastructure.Persistence.Outbox, Outbox.Persistence, Common.Application.Persistence.Outbox, Outbox.Telemetry, IEntityTypeConfiguration, IntegrationEventConverter, JsonSerializerOptions (+2 more)

### Community 107 - ".ReleaseStockReservationAsync"
Cohesion: 0.21
Nodes (8): CancellationToken, Task, IWarehouseGateway, CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 108 - "StockLevel"
Cohesion: 0.18
Nodes (10): DefaultIdType, StockLevelId, V1StockLevelCreatedDomainEvent, DefaultIdType, StockLevel, ProductId, QuantityOnHand, StockLevelId (+2 more)

### Community 109 - ".IsRegisteredAsync"
Cohesion: 0.18
Nodes (10): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, PhoneNumber, RequestValidator (+2 more)

### Community 110 - "Common.Application.Persistence"
Cohesion: 0.09
Nodes (15): Common.Application.Persistence, Notifications.Domain.Devices, IDatabaseSeeder, Priority, CancellationToken, Task, CancellationToken, IServiceScopeFactory (+7 more)

### Community 111 - "BackgroundJobsService"
Cohesion: 0.14
Nodes (15): IBackgroundJobClientV2, IBackgroundJobs, Action, DateTimeOffset, Expression, Func, Task, TimeSpan (+7 more)

### Community 112 - "OutboxCleanupJob"
Cohesion: 0.27
Nodes (8): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxCleanupJob

### Community 113 - "Request"
Cohesion: 0.20
Nodes (10): RequestBody, Request, Body, Id, RequestBody, Description, Name, Price (+2 more)

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
Cohesion: 0.15
Nodes (11): IModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority, Action, IApplicationBuilder (+3 more)

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
Cohesion: 0.29
Nodes (7): AuditLogEntry, AggregateId, AggregateType, Event, EventType, Version, DefaultIdType

### Community 129 - "MassTransitInterModuleRequestClient"
Cohesion: 0.20
Nodes (8): IClientFactory, InterModuleRequestOptions, TimeoutSeconds, InterModuleRequestOptionsValidator, MassTransitInterModuleRequestClient, CancellationToken, IOptions, Task

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.48
Nodes (7): Observability (OpenTelemetry), docker-compose.yml (Base Stack), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), Postgres Service (mm.postgres), RabbitMQ Service (mm.rabbitmq), Redis Service (mm.redis)

### Community 131 - ".AddInfrastructure"
Cohesion: 0.15
Nodes (12): HostOptions, IMiddleware, JsonOptions, Assembly, IConfiguration, IResxLocalizer, IServiceCollection, IWebHostEnvironment (+4 more)

### Community 132 - ".SaveChangesAsync"
Cohesion: 0.10
Nodes (14): CancellationToken, Task, CancellationToken, RouteGroupBuilder, Task, Endpoint, CancellationToken, RouteGroupBuilder (+6 more)

### Community 134 - "v1/RemoveProduct/Request.cs"
Cohesion: 0.33
Nodes (6): ProductId, StoreId, Request, Id, ProductId, RequestValidator

### Community 135 - ".ListSessions"
Cohesion: 0.10
Nodes (22): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, IReadOnlyList, CancellationToken, IReadOnlyCollection, RouteGroupBuilder (+14 more)

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
Cohesion: 0.20
Nodes (8): IReadOnlyList, List, ProductTemplate, Brand, Color, IsActive, Model, Products

### Community 140 - "Response"
Cohesion: 0.13
Nodes (12): IAM.Endpoints.Users.VersionNeutral.SelfRegister, IAM.Endpoints.Users.VersionNeutral, RouteGroupBuilder, Endpoint, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt (+4 more)

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
Cohesion: 0.12
Nodes (16): CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate (+8 more)

### Community 145 - "SecurityHeadersMiddleware"
Cohesion: 0.15
Nodes (11): IAuthenticationSchemeProvider, SecurityHeadersOptions, Headers, SecurityHeadersOptionsValidator, Dictionary, IApplicationBuilder, HttpContext, IOptions (+3 more)

### Community 147 - ".AddServices"
Cohesion: 0.12
Nodes (15): RecurringJobOptions, IRecurringBackgroundJobs, Action, Expression, Func, Task, IConfiguration, IServiceCollection (+7 more)

### Community 148 - "Setup"
Cohesion: 0.11
Nodes (16): LoadAll, ModuleRegistry, Names, Type, Setup, Assembly, Exception, IApplicationBuilder (+8 more)

### Community 149 - ".RevokeSession"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

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
Cohesion: 0.13
Nodes (21): AbstractValidator, Inventory.Endpoints.StockReservations.v1.Commit, CustomValidator, RequestBody, Request, Body, Id, RequestBody (+13 more)

### Community 155 - "InterModuleRequestHandler"
Cohesion: 0.19
Nodes (8): IConsumer, IInterModuleRequestHandler, CancellationToken, Task, InterModuleRequestHandler, CancellationToken, ConsumeContext, Task

### Community 156 - "Request"
Cohesion: 0.13
Nodes (14): Common.Domain.Extensions, Guid, Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName (+6 more)

### Community 157 - "IntegrationEventHandlerBase"
Cohesion: 0.24
Nodes (11): IntegrationEventHandlerBase, MaxEventAge, CancellationToken, ConsumeContext, DefaultIdType, IFusionCache, ILogger, IOptions (+3 more)

### Community 158 - ".FixedWindow"
Cohesion: 0.20
Nodes (7): RateLimitPartitions, HttpContext, IConnectionMultiplexer, ILoggerFactory, RateLimitPartition, HttpContext, RateLimitPartition

### Community 159 - "IInventoryDbContext"
Cohesion: 0.13
Nodes (15): DbSet, IInventoryDbContext, StockLevels, StockReservations, DbContextOptions, DbSet, ILogger, TimeProvider (+7 more)

### Community 160 - "OtpServiceBase"
Cohesion: 0.21
Nodes (8): OtpCacheEntry, DateTimeOffset, CancellationToken, IFusionCache, SemaphoreSlim, Task, TimeSpan, OtpServiceBase

### Community 161 - ".ReserveSeriesAsync"
Cohesion: 0.13
Nodes (14): Inventory.Endpoints.StockReservations.v1.ReserveSeries, CancellationToken, IOptions, RouteGroupBuilder, Task, TimeProvider, Endpoint, RequestBody (+6 more)

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
Cohesion: 0.10
Nodes (17): DbSet, ProductTemplate, Store, IProductsDbContext, Products, ProductTemplates, Stores, CancellationToken (+9 more)

### Community 169 - "ICaptchaService"
Cohesion: 0.25
Nodes (5): ICaptchaService, DummyCaptchaService, IConfiguration, IServiceCollection, Setup

### Community 171 - "IOtpService"
Cohesion: 0.22
Nodes (8): CancellationToken, Task, TimeSpan, IOtpService, OtpVerificationOutcome, InvalidOtp, Success, TooManyAttempts

### Community 172 - "ResxLocalizationOptions"
Cohesion: 0.25
Nodes (7): ResxLocalizationOptions, DefaultCulture, SupportedCultures, ResxLocalizationOptionsValidator, ICollection, IApplicationBuilder, IOptions

### Community 173 - "RequestBody"
Cohesion: 0.29
Nodes (7): ProductTemplateId, RequestBody, Description, Name, Price, ProductTemplateId, Quantity

### Community 174 - "SendSecurityAlertRequestHandler"
Cohesion: 0.27
Nodes (9): SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, CancellationToken, IOptions, RequestLocalizationOptions, Task (+1 more)

### Community 175 - "PaginationResponse"
Cohesion: 0.07
Nodes (25): Products.Endpoints.Stores.v1.My.AuditLog, JsonElement, AuditLogDto, DateTimeOffset, PaginationResponse, HasNext, HasPrevious, NextPageNumber (+17 more)

### Community 176 - "Policies"
Cohesion: 0.40
Nodes (3): Common.Infrastructure.RateLimiting, IAM.Infrastructure.RateLimiting, Policies

### Community 177 - ".AddProductToMyStoreAsync"
Cohesion: 0.18
Nodes (9): Products.Endpoints.Stores.v1.My.AddProduct, CancellationToken, ProductTemplate, RouteGroupBuilder, Store, Task, Endpoint, Response (+1 more)

### Community 178 - ".BindDeviceAsync"
Cohesion: 0.33
Nodes (7): CancellationToken, Exception, Guid, ILogger, LoggerMessage, Task, LoginCompletion

### Community 179 - "CachingOptions"
Cohesion: 0.09
Nodes (24): CachingEntryDefaults, Duration, FactoryHardTimeout, FactorySoftTimeout, FailSafeMaxDuration, FailSafeThrottleDuration, CachingOptions, AllowInMemoryOnlyInProduction (+16 more)

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
Cohesion: 0.15
Nodes (11): Action, IApplicationBuilder, IEndpointRouteBuilder, IEnumerable, RateLimiterOptions, IamModule, ActivitySourceNames, MeterNames (+3 more)

### Community 186 - "Request"
Cohesion: 0.20
Nodes (10): Products.Endpoints.Stores.v1.Update, RequestBody, Request, Body, Id, RequestBody, Address, Description (+2 more)

### Community 187 - "ReverseProxyOptions"
Cohesion: 0.18
Nodes (9): ForwardedHeadersOptions, ReverseProxyOptions, ForwardLimit, IsEnabled, TrustedNetworks, ReverseProxyOptionsValidator, IReadOnlyList, IConfiguration (+1 more)

### Community 188 - "BoundedCaptureStream"
Cohesion: 0.18
Nodes (7): HttpResponse, BoundedCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 190 - "ServiceAccountTokenCache"
Cohesion: 0.09
Nodes (13): CancellationToken, Task, KeycloakPaths, CancellationToken, DateTimeOffset, SemaphoreSlim, Task, TimeSpan (+5 more)

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
Cohesion: 0.13
Nodes (15): InventoryOptions, MaxOccurrencesPerSeries, SafetyBufferQuantity, StockReservationExpirySweepBatchSize, StockReservationExpirySweepCron, WarehouseGatewayBaseUrl, WarehouseGatewayProvider, WebhookSharedSecret (+7 more)

### Community 196 - "TokenEndpointRepresentations.cs"
Cohesion: 0.20
Nodes (9): List, DecisionRepresentation, Result, PermissionRepresentation, ResourceName, Scopes, TokenErrorRepresentation, Error (+1 more)

### Community 197 - ".AddPushServices"
Cohesion: 0.18
Nodes (10): CancellationToken, Task, IPushGateway, CancellationToken, ILogger, LoggerMessage, Task, DummyPushGateway (+2 more)

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

### Community 212 - ".AddNotificationsSignalR"
Cohesion: 0.17
Nodes (9): HubConnectionContext, IUserIdProvider, RedisOptions, IConfiguration, IConfigureOptions, IConnectionMultiplexer, IServiceCollection, IUserIdProvider (+1 more)

### Community 213 - "ProductsTelemetry"
Cohesion: 0.40
Nodes (4): ActivitySource, Counter, Meter, ProductsTelemetry

### Community 214 - "IAM.Application.Captcha.Services"
Cohesion: 0.33
Nodes (4): IAM.Domain.Captcha, IAM.Infrastructure.Captcha.Services, IAM.Application.Captcha.Services, IAM.Infrastructure.Captcha

### Community 215 - "Request"
Cohesion: 0.12
Nodes (13): Products.Endpoints.ProductTemplates, Products.Endpoints.ProductTemplates.v1.Create, RouteGroupBuilder, Setup, RouteGroupBuilder, Endpoint, Request, Brand (+5 more)

### Community 216 - "ProductTemplateId"
Cohesion: 0.17
Nodes (10): Products.Endpoints.ProductTemplates.v1.Deactivate, ProductTemplateId, DefaultIdType, ProductTemplateId, Request, Id, RequestValidator, CancellationToken (+2 more)

### Community 217 - "Request"
Cohesion: 0.22
Nodes (8): Products.Endpoints.ProductTemplates.v1.Search, Constants, Request, Brand, Color, Model, SearchTerm, RequestValidator

### Community 218 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 219 - "FirebasePushGateway.cs"
Cohesion: 0.24
Nodes (5): Notifications.Application.Push, Notifications.Infrastructure.Push, Notifications.Infrastructure.Push.Firebase, PushErrors, Setup

### Community 220 - "Products.Domain.ProductTemplates"
Cohesion: 0.09
Nodes (18): Products.Infrastructure.Persistence.Seeding, Products.Endpoints.ProductTemplates.v1.Get, Products.Domain.ProductTemplates, StoreId, DefaultIdType, StoreId, Request, Id (+10 more)

### Community 221 - "Keycloak realm as code"
Cohesion: 0.25
Nodes (7): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Secrets, Seed users (dev / test only)

### Community 222 - "UtcDateTimeOffsetConverter"
Cohesion: 0.22
Nodes (6): DateTimeOffset, ModelConfigurationBuilder, UtcDateTimeOffsetConverter, DateTimeOffset, DateTimeOffset, ModelConfigurationBuilder

### Community 223 - "RedisOtpService"
Cohesion: 0.28
Nodes (6): CancellationToken, IConnectionMultiplexer, IOptions, Task, TimeSpan, RedisOtpService

### Community 224 - "AuditLogRetentionService"
Cohesion: 0.29
Nodes (8): AuditLogRetentionService, CancellationToken, DateTimeOffset, ILogger, IOptions, LoggerMessage, NpgsqlDataSource, Task

### Community 225 - "BackgroundJobsOptions"
Cohesion: 0.29
Nodes (7): BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount, BackgroundJobsOptionsValidator

### Community 226 - ".HandleAsync"
Cohesion: 0.13
Nodes (15): Products.Endpoints.Probe.v1, GetSeedUserIdsRequest, GetSeedUserIdsResponse, ICollection, CancellationToken, Task, GetSeedUserIdsRequestHandler, CancellationToken (+7 more)

### Community 227 - "BackgroundJobsModule"
Cohesion: 0.22
Nodes (8): ICoreModule, BackgroundJobsModule, ActivitySourceNames, MeterNames, Name, StartupPriority, IEndpointRouteBuilder, IEnumerable

### Community 228 - "OtpOptions"
Cohesion: 0.25
Nodes (7): OtpOptions, ExpirationInMinutes, Length, OtpOptionsValidator, IFusionCache, IOptions, OtpService

### Community 229 - ".AddCommonOptions"
Cohesion: 0.20
Nodes (6): Setup, IConfiguration, IHostEnvironment, IServiceCollection, ValidationContextExtensions, ValidationContext

### Community 230 - "RequestBody"
Cohesion: 0.33
Nodes (6): DateTimeOffset, DefaultIdType, RequestBody, ProductId, Quantity, ReservationDeadline

### Community 231 - ".UpdateMyStoreAsync"
Cohesion: 0.17
Nodes (10): Products.Endpoints.Stores.v1.My.Update, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, Address, Description (+2 more)

### Community 232 - "Common.Domain.Devices"
Cohesion: 0.29
Nodes (6): IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, Common.Domain.Devices, DeviceSessionConstants, Request, Id, RequestValidator

### Community 233 - "StockReservationExpirySweepService"
Cohesion: 0.24
Nodes (9): CancellationToken, Exception, Guid, ILogger, IOptions, LoggerMessage, Task, TimeProvider (+1 more)

### Community 234 - "Setup"
Cohesion: 0.29
Nodes (4): IApplicationBuilder, IWebHostEnvironment, Type, Setup

### Community 235 - "PaginationRequestValidator"
Cohesion: 0.13
Nodes (12): Products.Endpoints.Stores.v1.AuditLog, PaginationRequestValidator, Constants, RequestValidator, RequestBody, Request, Body, Id (+4 more)

### Community 236 - ".UpdateCurrentPushToken"
Cohesion: 0.18
Nodes (9): Notifications.Infrastructure.Devices.UpdateCurrentPushToken, CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint, Request, PushToken (+1 more)

### Community 237 - "ReCaptchaResponse"
Cohesion: 0.25
Nodes (7): DateTime, ReCaptchaResponse, ChallengeTs, ErrorCodes, Hostname, Score, Success

### Community 238 - "NotificationsHub"
Cohesion: 0.31
Nodes (6): Hub, Exception, ILogger, LoggerMessage, Task, NotificationsHub

### Community 239 - "IAM.Infrastructure.Keycloak"
Cohesion: 0.10
Nodes (11): IAM.Infrastructure.Keycloak.Representations, IAM.Domain.Errors, IAM.Infrastructure.Keycloak, HttpContent, OtpErrors, TokenErrors, CancellationToken, Task (+3 more)

### Community 240 - "KeycloakUser"
Cohesion: 0.36
Nodes (7): DateOnly, DateTimeOffset, IReadOnlyList, GrantedPermission, KeycloakUser, KeycloakUserPage, KeycloakUserSession

### Community 241 - "IAM.Endpoints.Common.Validations"
Cohesion: 0.17
Nodes (10): IAM.Endpoints.Otp.VersionNeutral.SendForLogin, IAM.Endpoints.Common.Validations, IRuleBuilder, IRuleBuilderOptions, IResxLocalizer, CommonValidations, Request, CaptchaToken (+2 more)

### Community 242 - "Host.Swagger"
Cohesion: 0.22
Nodes (5): Host.Swagger, IOpenApiSchema, ISchemaFilter, SchemaFilterContext, StronglyTypedIdSchemaFilter

### Community 243 - ".RemoveProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 244 - ".AddAuthInfrastructure"
Cohesion: 0.25
Nodes (6): IAuthorizationHandler, IAuthorizationPolicyProvider, IConfigureOptions, IServiceCollection, JwtBearerOptions, Setup

### Community 245 - "RemoveDefaultResponseSchemaFilter"
Cohesion: 0.33
Nodes (4): IOperationFilter, OpenApiOperation, OperationFilterContext, RemoveDefaultResponseSchemaFilter

### Community 246 - "KeycloakPermission"
Cohesion: 0.25
Nodes (3): KeycloakPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder

### Community 247 - "StockReservationExpirySweepJobRegistrar"
Cohesion: 0.27
Nodes (8): IHostedService, CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task, StockReservationExpirySweepJobRegistrar

### Community 248 - "CachedCaptchaService"
Cohesion: 0.29
Nodes (5): CancellationToken, IFusionCache, IOptions, Task, CachedCaptchaService

### Community 249 - "Common.Application.EventBus"
Cohesion: 0.12
Nodes (13): Inventory.Application.IntegrationEventHandlers, Common.IntegrationEvents, Products.Application.Products.DomainEventHandlers.v1, Products.Application.Stores.DomainEventHandlers.v1, Common.Application.EventBus, DomainEventHandlerBase, IEventHandler, IEventHandlerWrapper (+5 more)

### Community 250 - "Request"
Cohesion: 0.20
Nodes (9): Products.Endpoints.Stores.v1.Create, Request, Address, Description, Name, OwnerId, RequestValidator, Response (+1 more)

### Community 251 - "Notifications.Application.Sms"
Cohesion: 0.43
Nodes (3): Notifications.Application.Sms, Notifications.Infrastructure.Sms, Notifications.Infrastructure.Sms.NetGsm

### Community 252 - "ICurrentUser"
Cohesion: 0.12
Nodes (12): ICurrentUser, Id, IdAsString, Roles, SessionId, ICollection, HttpContextExtensions, HttpContext (+4 more)

### Community 260 - ".SendCoreAsync"
Cohesion: 0.24
Nodes (7): IReadOnlyDictionary, IReadOnlyList, PushMessage, CancellationToken, IEnumerable, IReadOnlyList, Task

### Community 261 - "TokenCreateRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenCreateRateLimitingPolicy (+1 more)

### Community 262 - "Common.Infrastructure.Persistence.AuditLog"
Cohesion: 0.29
Nodes (3): Common.Infrastructure.Persistence.AuditLog, Setup, IServiceCollection

### Community 263 - "ReservationStatus"
Cohesion: 0.29
Nodes (6): ReservationStatus, Active, Committed, Expired, Released, RequiresReconciliation

### Community 264 - ".AddNetGsm"
Cohesion: 0.33
Nodes (5): IConfiguration, IFusionCache, IOptions, IServiceCollection, Setup

### Community 265 - "AuditLogOptions"
Cohesion: 0.50
Nodes (4): AuditLogOptions, PurgeBatchSize, RetentionDays, AuditLogOptionsValidator

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

### Community 271 - "Notifications.Infrastructure.Telemetry"
Cohesion: 0.19
Nodes (6): Notifications.Infrastructure.Telemetry, Notifications.Infrastructure.Hubs, Notifications.Application.Hubs, Notifications.Infrastructure, Setup, IAssemblyReference

### Community 272 - "RequireFeatureFilter"
Cohesion: 0.22
Nodes (6): RequireFeatureFilter, ActivitySource, Counter, Meter, RouteHandlerBuilderExtensions, RouteHandlerBuilder

### Community 273 - "FixedWindow"
Cohesion: 0.29
Nodes (7): CustomRateLimitingOptionsValidator, FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit, FixedWindowValidator

### Community 274 - "GetProductRequest"
Cohesion: 0.39
Nodes (6): GetProductRequest, GetProductResponse, DefaultIdType, CancellationToken, Task, GetProductRequestHandler

### Community 276 - "INotificationDispatcher"
Cohesion: 0.46
Nodes (4): CancellationToken, IReadOnlyList, Task, INotificationDispatcher

### Community 277 - "SendResponseBody"
Cohesion: 0.50
Nodes (4): SendResponseBody, Code, Description, JobId

### Community 279 - "DummyWarehouseGateway"
Cohesion: 0.47
Nodes (3): CancellationToken, Task, DummyWarehouseGateway

### Community 280 - ".GetAuditLogAsync"
Cohesion: 0.38
Nodes (5): DbContextExtensions, CancellationToken, DbSet, JsonSerializerOptions, Task

### Community 281 - ".CommitStockReservationAsync"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 282 - "InventoryModule.cs"
Cohesion: 0.09
Nodes (13): ApiVersionSet, Inventory.Endpoints, Common.Endpoints.Versioning, Inventory.Infrastructure.Gateway, Inventory.Endpoints.StockReservations, Common.Infrastructure.Resiliency, Inventory.Infrastructure.StockReservations, Setup (+5 more)

### Community 283 - ".AddOtpServices"
Cohesion: 0.33
Nodes (4): IFusionCache, DummyOtpService, IConfiguration, IServiceCollection

### Community 284 - "ProblemDetailsContext"
Cohesion: 0.33
Nodes (4): ProblemDetailsContext, ProblemDetailsServiceExtensions, IProblemDetailsService, Task

### Community 286 - "SendForRegistration/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, Request, CaptchaToken, PhoneNumber, RequestValidator

### Community 287 - ".DeactivateProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 289 - "ModulesOptions"
Cohesion: 0.50
Nodes (4): ModulesOptions, EnabledModules, ModulesOptionsValidator, IReadOnlyList

### Community 291 - ".DeactivateStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 293 - ".RemoveMyProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 294 - "StronglyTypedIdBinder"
Cohesion: 0.40
Nodes (4): IModelBinder, ModelBindingContext, StronglyTypedIdBinder, Task

### Community 295 - "SignalROptions"
Cohesion: 0.50
Nodes (4): SignalROptions, RedisConnectionString, UseRedisBackplane, SignalROptionsValidator

### Community 297 - "V1StockReservationReleaseAttemptAbandonedDomainEvent"
Cohesion: 0.40
Nodes (3): DateTimeOffset, StockReservationId, V1StockReservationReleaseAttemptAbandonedDomainEvent

### Community 298 - "VersionNeutral/Get/Request.cs"
Cohesion: 0.40
Nodes (4): IAM.Endpoints.Users.VersionNeutral.Get, Request, Id, RequestValidator

### Community 299 - "V1StockReservationReleaseAttemptStartedDomainEvent"
Cohesion: 0.40
Nodes (3): DateTimeOffset, StockReservationId, V1StockReservationReleaseAttemptStartedDomainEvent

### Community 300 - "V1StockReservationReservedDomainEvent"
Cohesion: 0.40
Nodes (4): DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationReservedDomainEvent

### Community 301 - ".AddCustomSwagger"
Cohesion: 0.50
Nodes (3): IConfigureOptions, IServiceCollection, SwaggerGenOptions

## Knowledge Gaps
- **826 isolated node(s):** `Id`, `IdAsString`, `Roles`, `SessionId`, `JwtClaimNames` (+821 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1884 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **96 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `Common.InterModuleRequests.Contracts`, `MassTransitInterModuleRequestClient`, `DeviceRegistryReconciliationService`, `Common.Infrastructure.Persistence.AuditLog`, `AuditLogOptions`, `Common.Domain.ResultMonad`, `Notifications.Infrastructure.Telemetry`, `FixedWindow`, `SecurityHeadersMiddleware`, `Common.Infrastructure.Persistence`, `OutboxOptions`, `ISearchLanguageResolver`, `InventoryModule.cs`, `Request`, `.FixedWindow`, `IAM.Infrastructure.Auth`, `ModulesOptions`, `Request`, `ObservabilityOptions`, `SignalROptions`, `KeycloakOptions`, `ResxLocalizationOptions`, `Policies`, `CachingOptions`, `SmsOptions`, `RabbitMqOptions`, `Inventory.Domain.StockReservations`, `Notifications.Application.Otp`, `ReverseProxyOptions`, `RequestLoggingOptions`, `CorsOptions`, `InventoryOptions`, `OpenApiOptions`, `Request`, `CaptchaOptions`, `Policies.CreateStore.cs`, `IAM.Application.Captcha.Services`, `PushOptions`, `Program.cs`, `FirebasePushGateway.cs`, `Common.Domain.StronglyTypedIds`, `.AddCommonPersistence`, `BackgroundJobsOptions`, `OtpOptions`, `ResiliencyOptions`, `.AddCommonOptions`, `Common.Application.BackgroundJobs`, `Common.Infrastructure.Persistence.Outbox`, `IAM.Infrastructure.Keycloak`, `Notifications.Application.Sms`, `Host.Swagger`, `Common.Application.EventBus`, `FullTextSearchOptions`, `.AddCustomHealthChecks`?**
  _High betweenness centrality (0.238) - this node is a cross-community bridge._
- **Why does `Result` connect `Result` to `.SendCoreAsync`, `.SaveChangesAsync`, `.ListSessions`, `.SingleAsResult`, `Error`, `ProductTemplate`, `IInterModuleRequest`, `.SendOtp`, `.GetMeAsync`, `Response`, `Response`, `ISmsGateway`, `DomainEvent`, `ApplicationUserId`, `.RevokeSession`, `DummyWarehouseGateway`, `.GetAuditLogAsync`, `.SendAsync`, `.CommitStockReservationAsync`, `NetGsmSmsGateway`, `.DeactivateProductTemplateAsync`, `.ReserveSeriesAsync`, `HttpWarehouseGateway`, `.AddProductAsync`, `.DeactivateStoreAsync`, `Inventory.Domain.StockReservations.DomainEvents.v1`, `.RefreshToken`, `.RemoveMyProductAsync`, `IProductsDbContext`, `ICaptchaService`, `V1StockReservationReleaseAttemptAbandonedDomainEvent`, `V1StockReservationReleaseAttemptStartedDomainEvent`, `.CreateTokens`, `PaginationResponse`, `.AddProductToMyStoreAsync`, `.BindDeviceAsync`, `Response`, `ReCaptchaService`, `Inventory.Domain.StockReservations`, `DummySmsGateway`, `.SearchProductTemplatesAsync`, `AuditableEntityResponse`, `.AddPushServices`, `Response`, `.SearchStoresAsync`, `ResultTelemetryExtensions`, `IInterModuleRequestClient`, `Response`, `.HandleWarehouseWebhookAsync`, `.SearchMyProductsAsync`, `.SearchStoreProductsAsync`, `.UpdateMyStoreAsync`, `.ReleaseStockReservationAsync`, `.UpdateCurrentPushToken`, `.IsRegisteredAsync`, `.RemoveProductAsync`, `Response`, `CachedCaptchaService`, `ICurrentUser`, `.TapWhenFeatureEnabledAsync`?**
  _High betweenness centrality (0.156) - this node is a cross-community bridge._
- **Why does `ApplicationUserId` connect `ApplicationUserId` to `NotificationPayload`, `DeviceRegistryReconciliationService`, `KeycloakAdminClient`, `.ListSessions`, `Response`, `KeycloakPermissionAuthorizationHandler`, `Response`, `INotificationDispatcher`, `.TryDeserialize`, `DeviceRegistration`, `.SendAsync`, `KeycloakTokenClient`, `.ForUser`, `For`, `CurrentUser`, `.LogRoleAssignmentFailed`, `IProductsDbContext`, `VersionNeutral/Get/Request.cs`, `.CreateTokens`, `SendSecurityAlertRequestHandler`, `PaginationResponse`, `AggregateRoot`, `.BindDeviceAsync`, `Response`, `Request`, `AuditableEntityResponse`, `V1StoreCreatedDomainEvent`, `IStronglyTypedId`, `.SearchStoresAsync`, `.Configure`, `Store`, `Response`, `Products.Domain.ProductTemplates`, `.HandleAsync`, `CreateStockLevelOnProductCreatedHandler`, `KeycloakUser`, `Response`, `Request`, `ICurrentUser`?**
  _High betweenness centrality (0.090) - this node is a cross-community bridge._
- **What connects `Id`, `IdAsString`, `Roles` to the rest of the system?**
  _826 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Common.InterModuleRequests.Contracts` be split into smaller, more focused modules?**
  _Cohesion score 0.12073170731707317 - nodes in this community are weakly interconnected._
- **Should `DeviceRegistryReconciliationService` be split into smaller, more focused modules?**
  _Cohesion score 0.06976744186046512 - nodes in this community are weakly interconnected._
- **Should `RedisFixedWindowRateLimiter` be split into smaller, more focused modules?**
  _Cohesion score 0.12554112554112554 - nodes in this community are weakly interconnected._