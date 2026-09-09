# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-09-09)

## Corpus Check
- 515 files · ~72,973 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 4163 nodes · 7399 edges · 363 communities (271 shown, 87 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 250 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `01c0c8d5`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- Common.Domain.ResultMonad
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
- Common.Application.Auth
- IInterModuleRequest
- ICaptchaService
- AdminRepresentations.cs
- KeycloakPermissionAuthorizationHandler
- ISmsGateway
- .UseModule
- Common.Domain.Events
- AuditableEntity
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
- DomainEvent
- .CreateTokens
- ObservabilityOptions
- .RegisterAsync
- PaginationRequest
- .SaveWithOutboxAsync
- CreateStoreRateLimitingPolicy
- KeycloakOptions
- .BindDeviceAsync
- Products.Domain.Stores.DomainEvents.v1
- Outbox Misuse Check
- AggregateRoot
- Add Integration Event Command
- Response
- Uri
- ReCaptchaService
- Inventory.Domain.StockReservations
- Notifications.Infrastructure.Telemetry
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
- Response
- ValueObject
- V1StoreCreatedDomainEvent
- IStronglyTypedId
- Full-Text Search
- AuditableEntityResponse
- .WriteAsync
- AsNoTracking Coverage Check
- ProductId
- Common.Application.ModelBinders
- .CreateMyStoreAsync
- CheckRegistrationRateLimitingPolicy
- IntegrationEventOutbox
- EventDispatcher
- .SearchStoresAsync
- SelfRegister/Request.cs
- .EnsureNoMigrationsPending
- .Configure
- ResultTelemetryExtensions
- Store
- PushOptions
- Configuration-Driven Module Registration
- Program.cs
- IInterModuleRequestClient
- Infrastructure/Setup.cs
- Common.Domain.StronglyTypedIds
- Endpoint
- SeedingCompletionTracker
- .SearchMyProductsAsync
- DatabaseSeederOrchestrator
- AuditLogRetentionJobRegistrar
- .SearchStoreProductsAsync
- UserRepresentation
- OutboxDbContext
- ResiliencyOptions
- HttpContextTargetingContextAccessor
- Common.Application.BackgroundJobs
- IntegrationEvent
- .AddKeycloakInfrastructure
- OutboxModule.cs
- .ReleaseStockReservationAsync
- StockLevel
- .IsRegisteredAsync
- ProductsDbContext
- BackgroundJobsService
- OutboxCleanupJob
- Request
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
- .UseModules
- docker-compose.yml (Base Stack)
- .AddInfrastructure
- .UpdateProductAsync
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
- IAM.Endpoints.Otp.VersionNeutral
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
- IOtpService
- .BuildReservations
- IAuditableEntity
- For
- Request
- CurrentUser
- Response
- .PaginateAsync
- IProductsDbContext
- StronglyTypedIdWriteOnlyJsonConverter
- Notifications.Application/IAssemblyReference.cs
- .UpdateStoreAsync
- ResxLocalizationOptions
- RequestBody
- SendSecurityAlertRequestHandler
- PaginationResponse
- PolymorphicEventConverter
- .AddProductToMyStoreAsync
- StrictDateTimeOffsetJsonConverter
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
- TokenResponseRepresentation
- DummyPushGateway
- InventoryModule
- OpenApiOptions
- KeyValuePair
- KeycloakScopes
- Request
- .ActivateProductTemplateAsync
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
- GetDeviceSessionsRequest
- Request
- ProductTemplateId
- Request
- Response
- .AddPushServices
- Seeder
- Keycloak realm as code
- Endpoint
- StronglyTypedIdListReadOnlyJsonConverter
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
- StoreId
- .UpdateCurrentPushToken
- .SeedAsync
- NotificationsHub
- .TryReadFromJsonAsync
- KeycloakUser
- SendForLogin/Request.cs
- .AddCustomSwagger
- .RemoveProductAsync
- Common.InterModuleRequests.IAM
- Host.Swagger
- .MapEndpoint
- StockReservationExpirySweepJobRegistrar
- .SaveChangesAsync
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
- Refresh/Request.cs
- EnrichLogsWithUserInfoMiddleware
- .AddNetGsm
- AuditLogOptions
- SecurityHeadersOptions
- LogProductCatalogChangeHandler
- Request
- DefaultResponsesOperationFilter
- Notifications.Application.Hubs
- .InvokeAsync
- FixedWindow
- .ReserveSeriesAsync
- .ReserveStockAsync
- INotificationDispatcher
- SendResponseBody
- .CreateProductTemplateAsync
- .AddCommonCaching
- .GetAuditLogAsync
- .CommitStockReservationAsync
- Setup
- Request
- ProblemDetailsContext
- Products.Endpoints.Probe
- SendForRegistration/Request.cs
- .DeactivateProductTemplateAsync
- Inventory.Endpoints.StockReservations
- ModulesOptions
- .AddStockReservationExpirySweep
- .DeactivateStoreAsync
- .RemoveMyProductAsync
- .SeedProductAsync
- v1/Request.cs
- InventoryTelemetry
- .PhoneNumberValidation
- VersionNeutral/Get/Request.cs
- VersionNeutral/Search/Request.cs
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

### Community 0 - "Common.Domain.ResultMonad"
Cohesion: 0.07
Nodes (23): IAM.Infrastructure.Keycloak.Representations, Common.Application.FeatureManagement, IAM.Endpoints.Otp, IAM.Domain.Captcha, Common.InterModuleRequests.Contracts, IAM.Infrastructure.Captcha.Services, IAM.Application.Captcha.Services, IAM.Endpoints.Tokens.VersionNeutral.Revoke (+15 more)

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
Cohesion: 0.24
Nodes (13): HttpRequestMessage, CancellationToken, Func, HttpClient, HttpResponseMessage, IFusionCache, ILogger, IOptions (+5 more)

### Community 6 - "RedisFixedWindowRateLimiter"
Cohesion: 0.13
Nodes (16): RateLimiter, RateLimiterStatistics, RateLimitLease, FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration (+8 more)

### Community 7 - "ApplyAuditingInterceptor"
Cohesion: 0.20
Nodes (9): SaveChangesInterceptor, ApplyAuditingInterceptor, TimeProvider, ApplySearchLanguageInterceptor, Setup, IServiceCollection, Setup, IServiceCollection (+1 more)

### Community 8 - "FirebasePushGateway"
Cohesion: 0.24
Nodes (8): FirebaseApp, FirebaseMessaging, IDisposable, Exception, ILogger, LoggerMessage, TimeSpan, FirebasePushGateway

### Community 9 - "Request"
Cohesion: 0.17
Nodes (12): Products.Endpoints.Products.v1.My.Update, RequestBody, Request, Body, Id, RequestBody, Description, Name (+4 more)

### Community 10 - "Error"
Cohesion: 0.08
Nodes (19): StringLocalizerExtensions, IStringLocalizer, Error, Key, ParameterName, StatusCode, SubErrors, Value (+11 more)

### Community 11 - "Common.Application.Auth"
Cohesion: 0.08
Nodes (25): Products.Infrastructure.InterModuleRequestHandlers, Common.Application.Search, Products.Endpoints.Stores.v1.Search, Products.Endpoints.Products.v1.My.Get, Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions, Products.Endpoints.ProductTemplates.v1.Search, Products.Endpoints.Products.v1.Search (+17 more)

### Community 12 - "IInterModuleRequest"
Cohesion: 0.17
Nodes (11): IInterModuleRequest, OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, VerifyPhoneOtpResponseExtensions (+3 more)

### Community 13 - "ICaptchaService"
Cohesion: 0.07
Nodes (26): SendPhoneOtpRequest, SendPhoneOtpResponse, CancellationToken, Task, ICaptchaService, CancellationToken, IFeatureManager, RouteGroupBuilder (+18 more)

### Community 14 - "AdminRepresentations.cs"
Cohesion: 0.12
Nodes (16): CredentialRepresentation, Temporary, Type, Value, ErrorRepresentation, Error, ErrorMessage, Field (+8 more)

### Community 15 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.05
Nodes (34): AuthorizationHandler, AuthorizationHandlerContext, AuthorizationOptions, AuthorizationPolicy, DefaultAuthorizationPolicyProvider, IAuthorizationHandler, IAuthorizationPolicyProvider, IAuthorizationRequirement (+26 more)

### Community 16 - "ISmsGateway"
Cohesion: 0.13
Nodes (13): CancellationToken, Task, ISmsGateway, SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, SmsMessage (+5 more)

### Community 17 - ".UseModule"
Cohesion: 0.20
Nodes (7): DashboardContext, IAuthorizationService, IDashboardAsyncAuthorizationFilter, IApplicationBuilder, IOptions, HangfireCustomAuthorizationFilter, Task

### Community 18 - "Common.Domain.Events"
Cohesion: 0.08
Nodes (16): Products.Domain.Products.DomainEvents.v1, Inventory.Domain.StockReservations.DomainEvents.v1, Common.Domain.Events, UnknownDomainEvent, ProductId, V1ProductDescriptionUpdatedDomainEvent, ProductId, V1ProductNameUpdatedDomainEvent (+8 more)

### Community 19 - "AuditableEntity"
Cohesion: 0.25
Nodes (8): AuditableEntity, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, Version, DateTimeOffset

### Community 20 - "ApplicationUserId"
Cohesion: 0.27
Nodes (7): ApplicationUserId, IsEmpty, Value, DefaultIdType, CancellationToken, Task, IKeycloakAdminClient

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.14
Nodes (8): ReadOnlySpan, BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position, Stream

### Community 22 - "DeviceRegistration"
Cohesion: 0.14
Nodes (13): DateTimeOffset, DefaultIdType, Guid, DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive (+5 more)

### Community 23 - "StockReservation"
Cohesion: 0.16
Nodes (12): DateTimeOffset, DefaultIdType, StockReservation, LastReleaseAttemptReference, ProductId, ProviderReference, Quantity, ReservationDeadline (+4 more)

### Community 24 - ".SendAsync"
Cohesion: 0.09
Nodes (21): IClientFactory, CancellationToken, Task, MassTransitInterModuleRequestClient, CancellationToken, IOptions, Task, DeactivateDeviceSessionsRequest (+13 more)

### Community 25 - "NetGsmSmsGateway"
Cohesion: 0.16
Nodes (14): SendRequestBody, SendResponseBody, CancellationToken, Exception, HttpClient, ILogger, IOptions, JsonSerializerOptions (+6 more)

### Community 26 - "IEvent"
Cohesion: 0.12
Nodes (12): CancellationToken, Task, IEventHandler, CancellationToken, Task, CancellationToken, Task, IEvent (+4 more)

### Community 27 - "RequestResponseBodyLoggingMiddleware"
Cohesion: 0.21
Nodes (7): IDiagnosticContext, HttpContext, IList, IOptions, PathString, RequestDelegate, RequestResponseBodyLoggingMiddleware

### Community 28 - "KeycloakTokenClient"
Cohesion: 0.17
Nodes (15): JsonWebTokenHandler, DateTimeOffset, KeycloakTokens, CancellationToken, Dictionary, Error, Exception, HttpClient (+7 more)

### Community 29 - "Result"
Cohesion: 0.11
Nodes (16): SearchValues, StringExtensions, Result, Error, IsFailure, Success, Value, Func (+8 more)

### Community 30 - "Product"
Cohesion: 0.09
Nodes (24): ProductTemplate, ProductTemplateId, Store, StoreId, Product, Description, Language, Name (+16 more)

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
Cohesion: 0.08
Nodes (23): CheckRegistrationRateLimitingPolicy, CreateStoreRateLimitingPolicy, RegisterRateLimitingPolicy, SmsRateLimitingPolicy, CustomRateLimitingOptions, CheckRegistration, CreateStore, ExemptPathPrefixes (+15 more)

### Community 37 - "DomainEvent"
Cohesion: 0.05
Nodes (39): DomainEvent, CreatedOn, Id, Version, DateTimeOffset, DefaultIdType, AuditLogEntryConfiguration, EntityTypeBuilder (+31 more)

### Community 38 - ".CreateTokens"
Cohesion: 0.11
Nodes (17): IAM.Endpoints.Tokens.VersionNeutral.Refresh, CancellationToken, Task, IKeycloakTokenClient, CancellationToken, ILogger, Task, CancellationToken (+9 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.08
Nodes (22): IHostBuilder, LoggerConfiguration, LoggerMinimumLevelConfiguration, ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics (+14 more)

### Community 40 - ".RegisterAsync"
Cohesion: 0.14
Nodes (13): CancellationToken, Exception, IFeatureManager, ILogger, LoggerMessage, Task, Endpoint, ActivitySource (+5 more)

### Community 41 - "PaginationRequest"
Cohesion: 0.09
Nodes (23): Products.Endpoints.Stores.v1.My.AuditLog, Products.Endpoints.Products.v1.AuditLog, PaginationRequest, PageNumber, PageSize, Skip, Take, PaginationRequestValidator (+15 more)

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
Nodes (26): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, BindDeviceSessionRequest, BindDeviceSessionResponse, Guid, CancellationToken, Exception, Guid, ILogger (+18 more)

### Community 46 - "Products.Domain.Stores.DomainEvents.v1"
Cohesion: 0.13
Nodes (10): Products.Application.Stores.DomainEventHandlers.v1, Products.Domain.Stores.DomainEvents.v1, StoreId, V1StoreAddressUpdatedDomainEvent, StoreId, V1StoreDeactivatedDomainEvent, StoreId, V1StoreDeactivatedWithStockOnHandDomainEvent (+2 more)

### Community 48 - "AggregateRoot"
Cohesion: 0.15
Nodes (11): AggregateRoot, Events, Id, Version, IReadOnlyCollection, List, IAggregateRoot, Events (+3 more)

### Community 50 - "Response"
Cohesion: 0.12
Nodes (16): CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate (+8 more)

### Community 51 - "Uri"
Cohesion: 0.23
Nodes (8): OpenTelemetryBuilder, ResourceBuilder, Action, IConfiguration, IHostEnvironment, IReadOnlyList, IServiceCollection, Uri

### Community 52 - "ReCaptchaService"
Cohesion: 0.06
Nodes (33): DateTime, FormUrlEncodedContent, ReCaptchaResponse, CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey (+25 more)

### Community 53 - "Inventory.Domain.StockReservations"
Cohesion: 0.08
Nodes (18): Inventory.Endpoints.StockReservations.v1.ReserveSeries, Inventory.Infrastructure.InterModuleRequestHandlers, Inventory.Endpoints, Common.Endpoints.Versioning, Inventory.Application.Persistence, Inventory.Infrastructure.Persistence.EntityConfigurations, Inventory.Infrastructure.Gateway, Inventory.Domain.StockReservations (+10 more)

### Community 54 - "Notifications.Infrastructure.Telemetry"
Cohesion: 0.10
Nodes (13): Notifications.Application.Otp, Notifications.Application.Sms, Notifications.Infrastructure.Devices, Common.Application.Caching, Notifications.Infrastructure.Sms, Notifications.Application.Persistence, Notifications.Infrastructure.Telemetry, Notifications.Infrastructure.InterModuleRequestHandlers (+5 more)

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
Cohesion: 0.18
Nodes (10): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Brand (+2 more)

### Community 61 - "Request"
Cohesion: 0.20
Nodes (9): Products.Endpoints.Stores.v1.My.AddProduct, ProductTemplateId, Request, Description, Name, Price, ProductTemplateId, Quantity (+1 more)

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
Cohesion: 0.06
Nodes (19): Common.Infrastructure.Modules, Products.Infrastructure.Persistence, Notifications.Application.Push, Common.Infrastructure.RateLimiting, Notifications.Infrastructure.Push, Products.Infrastructure.RateLimiting, IAM.Endpoints, Common.Infrastructure.Extensions (+11 more)

### Community 66 - "OutboxModule"
Cohesion: 0.13
Nodes (16): Action, Exception, IApplicationBuilder, IEndpointRouteBuilder, IEnumerable, IHostApplicationLifetime, ILogger, ILoggerFactory (+8 more)

### Community 67 - "Response"
Cohesion: 0.15
Nodes (12): Products.Endpoints.ProductTemplates.v1.Get, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, Id, RequestValidator (+4 more)

### Community 68 - "ValueObject"
Cohesion: 0.25
Nodes (4): Common.Domain, IComparable, ValueObject, IEnumerable

### Community 69 - "V1StoreCreatedDomainEvent"
Cohesion: 0.26
Nodes (8): DomainEventHandlerBase, CancellationToken, Task, SimulateSomeBusinessHandler, StoreCreatedIntegrationEventPublishingHandler, V1StoreCreatedDomainEventHandlers, StoreId, V1StoreCreatedDomainEvent

### Community 70 - "IStronglyTypedId"
Cohesion: 0.23
Nodes (8): StronglyTypedIdReadOnlyJsonConverter, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, IStronglyTypedId, Value, DefaultIdType

### Community 71 - "Full-Text Search"
Cohesion: 0.08
Nodes (25): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Add a new language/culture, Add search to a new entity _(Build checklist)_ (+17 more)

### Community 72 - "AuditableEntityResponse"
Cohesion: 0.12
Nodes (16): AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, DateTimeOffset, CancellationToken (+8 more)

### Community 73 - ".WriteAsync"
Cohesion: 0.33
Nodes (5): Memory, ReadOnlyMemory, CancellationToken, Task, ValueTask

### Community 75 - "ProductId"
Cohesion: 0.14
Nodes (13): Products.Endpoints.Stores.v1.My.RemoveProduct, Products.Endpoints.Products.v1.Get, DefaultIdType, ProductId, Request, Id, RequestValidator, Request (+5 more)

### Community 76 - "Common.Application.ModelBinders"
Cohesion: 0.09
Nodes (19): Products.Endpoints.Stores.v1.Deactivate, Inventory.Endpoints.StockReservations.v1.Release, Common.Application.ModelBinders, IModelBinder, ModelBindingContext, StronglyTypedIdBinder, Task, Request (+11 more)

### Community 77 - ".CreateMyStoreAsync"
Cohesion: 0.19
Nodes (9): Products.Endpoints.Stores.v1.My.Create, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator, Response (+1 more)

### Community 78 - "CheckRegistrationRateLimitingPolicy"
Cohesion: 0.13
Nodes (15): IRateLimiterPolicy, CancellationToken, Func, IOptions, OnRejectedContext, ValueTask, CheckRegistrationRateLimitingPolicy, OnRejected (+7 more)

### Community 79 - "IntegrationEventOutbox"
Cohesion: 0.14
Nodes (12): Lock, IIntegrationEventOutbox, IntegrationEventOutbox, IReadOnlyList, List, Setup, IServiceCollection, CancellationToken (+4 more)

### Community 80 - "EventDispatcher"
Cohesion: 0.27
Nodes (7): EventDispatcher, ActivitySource, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task

### Community 81 - ".SearchStoresAsync"
Cohesion: 0.15
Nodes (12): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Address (+4 more)

### Community 82 - "SelfRegister/Request.cs"
Cohesion: 0.22
Nodes (6): IAM.Domain.Users, Common.Domain.Extensions, IAM.Endpoints.Common.Validations, Constants, CommonValidations, RequestValidator

### Community 83 - ".EnsureNoMigrationsPending"
Cohesion: 0.30
Nodes (6): AutoMigrateMarker, IAutoMigrateMarker, MigrationGuard, ILogger, IServiceProvider, LoggerMessage

### Community 84 - ".Configure"
Cohesion: 0.14
Nodes (14): AuditableEntityConfiguration, EntityTypeBuilder, StronglyTypedIdValueConverter, DefaultIdType, EntityTypeBuilder, DeviceRegistrationConfiguration, EntityTypeBuilder, NpgsqlTsVector (+6 more)

### Community 85 - "ResultTelemetryExtensions"
Cohesion: 0.38
Nodes (4): Activity, ResultTelemetryExtensions, ActivitySource, Task

### Community 86 - "Store"
Cohesion: 0.08
Nodes (19): ISearchLocalized, Language, StoreId, V1StoreDescriptionUpdatedDomainEvent, StoreId, V1StoreNameUpdatedDomainEvent, IReadOnlyCollection, List (+11 more)

### Community 87 - "PushOptions"
Cohesion: 0.19
Nodes (13): PushOptions, Provider, SendTimeoutSeconds, ServiceAccount, Templates, PushOptionsValidator, PushProvider, Dummy (+5 more)

### Community 89 - "Program.cs"
Cohesion: 0.22
Nodes (6): ConfigurationManager, Host, Host.Configurations, Setup, Program, WebApplicationBuilder

### Community 90 - "IInterModuleRequestClient"
Cohesion: 0.10
Nodes (21): Products.Endpoints.Products, IInterModuleRequestClient, GetStockLevelRequest, GetStockLevelResponse, DefaultIdType, CancellationToken, Task, GetStockLevelRequestHandler (+13 more)

### Community 91 - "Infrastructure/Setup.cs"
Cohesion: 0.22
Nodes (4): Common.Infrastructure.Localization, Common.Infrastructure.Auth.Services, Common.Infrastructure.FeatureManagement, Common.Infrastructure.Auth

### Community 92 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.07
Nodes (17): Common.Infrastructure.Persistence, IAM.Endpoints.Users.VersionNeutral.Search, Common.Domain.StronglyTypedIds, Common.Application.Persistence, Notifications.Domain.Devices, Notifications.Infrastructure.Persistence, IAM.Endpoints.Users.VersionNeutral.Get, IAM.Endpoints.Users.VersionNeutral.Me.Get (+9 more)

### Community 93 - "Endpoint"
Cohesion: 0.18
Nodes (8): IAM.Endpoints.Captcha.VersionNeutral, IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, RouteGroupBuilder, Endpoint, Response, ClientKey, RouteGroupBuilder, Setup

### Community 94 - "SeedingCompletionTracker"
Cohesion: 0.15
Nodes (8): SeedingCompletionTracker, CancellationToken, Exception, Task, Setup, IOptions, IServiceCollection, TaskCompletionSource

### Community 95 - ".SearchMyProductsAsync"
Cohesion: 0.17
Nodes (11): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Description (+3 more)

### Community 96 - "DatabaseSeederOrchestrator"
Cohesion: 0.27
Nodes (9): BackgroundService, DatabaseSeederOrchestrator, CancellationToken, Exception, IHostApplicationLifetime, ILogger, IServiceScopeFactory, LoggerMessage (+1 more)

### Community 97 - "AuditLogRetentionJobRegistrar"
Cohesion: 0.18
Nodes (9): Common.Infrastructure.Persistence.AuditLog, AuditLogRetentionJobRegistrar, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task, Setup (+1 more)

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
Cohesion: 0.10
Nodes (19): HttpStandardResilienceOptions, IHttpClientBuilder, ResiliencyOptions, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds (+11 more)

### Community 102 - "HttpContextTargetingContextAccessor"
Cohesion: 0.18
Nodes (8): ITargetingContextAccessor, HttpContextTargetingContextAccessor, IHttpContextAccessor, ValueTask, Setup, IConfiguration, IServiceCollection, TargetingContext

### Community 103 - "Common.Application.BackgroundJobs"
Cohesion: 0.10
Nodes (14): Common.Application.BackgroundJobs, BackgroundJobs.Telemetry, BackgroundJobs, IServerFilter, PerformedContext, PerformingContext, JobMetricsFilter, BackgroundJobsTelemetry (+6 more)

### Community 104 - "IntegrationEvent"
Cohesion: 0.13
Nodes (16): IntegrationEvent, CreatedOn, Id, DateTimeOffset, DefaultIdType, ProductCreatedIntegrationEvent, StoreCreatedIntegrationEvent, DefaultIdType (+8 more)

### Community 105 - ".AddKeycloakInfrastructure"
Cohesion: 0.16
Nodes (10): CancellationToken, Task, IServiceAccountTokenProvider, HttpClient, IOptions, TimeProvider, ServiceAccountTokenProvider, IOptions (+2 more)

### Community 106 - "OutboxModule.cs"
Cohesion: 0.18
Nodes (7): Outbox, Common.Infrastructure.Persistence.Outbox, Outbox.Persistence, Common.Application.Persistence.Outbox, Outbox.Telemetry, IEntityTypeConfiguration, OutboxMessageConfig

### Community 107 - ".ReleaseStockReservationAsync"
Cohesion: 0.09
Nodes (22): ReleaseResponseBody, CancellationToken, Task, IWarehouseGateway, CancellationToken, RouteGroupBuilder, Task, TimeProvider (+14 more)

### Community 108 - "StockLevel"
Cohesion: 0.17
Nodes (11): Inventory.Domain.StockLevels.DomainEvents.v1, DefaultIdType, StockLevelId, V1StockLevelCreatedDomainEvent, DefaultIdType, StockLevel, ProductId, QuantityOnHand (+3 more)

### Community 109 - ".IsRegisteredAsync"
Cohesion: 0.18
Nodes (10): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, PhoneNumber, RequestValidator (+2 more)

### Community 110 - "ProductsDbContext"
Cohesion: 0.10
Nodes (19): IDatabaseSeeder, Priority, DbContextOptions, DbSet, ILogger, ProductTemplate, Store, TimeProvider (+11 more)

### Community 111 - "BackgroundJobsService"
Cohesion: 0.23
Nodes (8): IBackgroundJobClientV2, BackgroundJobsService, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 112 - "OutboxCleanupJob"
Cohesion: 0.27
Nodes (8): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxCleanupJob

### Community 113 - "Request"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Products.v1.Update, RequestBody, Request, Body, Id, RequestBody, Description, Name (+3 more)

### Community 114 - "Response"
Cohesion: 0.12
Nodes (16): RouteGroupBuilder, Endpoint, DateOnly, DateTimeOffset, IReadOnlyCollection, Response, BirthDate, CreatedOn (+8 more)

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
Cohesion: 0.19
Nodes (11): IApplicationBuilder, IServiceCollection, Exception, HttpContext, ILogger, IProblemDetailsService, IResxLocalizer, LoggerMessage (+3 more)

### Community 127 - "BaseDbContext"
Cohesion: 0.11
Nodes (15): BaseDbContext, AuditLog, CancellationToken, DateTimeOffset, DbContextOptions, DbSet, ILogger, ModelConfigurationBuilder (+7 more)

### Community 128 - "AuditLogEntry"
Cohesion: 0.25
Nodes (7): AuditLogEntry, AggregateId, AggregateType, Event, EventType, Version, DefaultIdType

### Community 129 - ".UseModules"
Cohesion: 0.26
Nodes (6): ModuleRegistry, Exception, IApplicationBuilder, ILogger, LoggerMessage, WebApplication

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.48
Nodes (7): Observability (OpenTelemetry), docker-compose.yml (Base Stack), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), Postgres Service (mm.postgres), RabbitMQ Service (mm.rabbitmq), Redis Service (mm.redis)

### Community 131 - ".AddInfrastructure"
Cohesion: 0.29
Nodes (7): HostOptions, JsonOptions, Assembly, IConfiguration, IResxLocalizer, IServiceCollection, IWebHostEnvironment

### Community 132 - ".UpdateProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

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
Cohesion: 0.25
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegister, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 141 - ".GetMeAsync"
Cohesion: 0.24
Nodes (7): CancellationToken, IReadOnlyList, Task, IKeycloakPermissionClient, CancellationToken, HttpContext, Task

### Community 142 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 143 - "IDbContext"
Cohesion: 0.29
Nodes (6): DatabaseFacade, IDbContext, AuditLog, ChangeTracker, Database, DbSet

### Community 144 - "Response"
Cohesion: 0.12
Nodes (16): CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate (+8 more)

### Community 145 - "SecurityHeadersMiddleware"
Cohesion: 0.20
Nodes (7): IAuthenticationSchemeProvider, IApplicationBuilder, HttpContext, IOptions, RequestDelegate, Task, SecurityHeadersMiddleware

### Community 146 - "IAM.Endpoints.Otp.VersionNeutral"
Cohesion: 0.40
Nodes (3): IAM.Endpoints.Otp.VersionNeutral, RouteGroupBuilder, Setup

### Community 147 - ".AddServices"
Cohesion: 0.12
Nodes (15): RecurringJobOptions, IRecurringBackgroundJobs, Action, Expression, Func, Task, IConfiguration, IServiceCollection (+7 more)

### Community 148 - "Setup"
Cohesion: 0.13
Nodes (11): Host.Infrastructure, LoadAll, Names, Type, Setup, Assembly, IConfiguration, IEnumerable (+3 more)

### Community 149 - ".RevokeSession"
Cohesion: 0.18
Nodes (8): IReadOnlyList, DateTimeOffset, KeycloakUserSession, CancellationToken, RouteGroupBuilder, Task, Endpoint, IReadOnlyList

### Community 150 - "FeatureFlags"
Cohesion: 0.33
Nodes (5): Checkout, FeatureFlags, IAM, Notifications, Products

### Community 151 - "OutboxOptions"
Cohesion: 0.11
Nodes (20): OutboxCleanupSettings, BatchSize, CronSchedule, Enabled, RetentionDays, OutboxCleanupSettingsValidator, OutboxOptions, BaseBackoffSeconds (+12 more)

### Community 152 - "ISearchLanguageResolver"
Cohesion: 0.15
Nodes (11): ISearchLanguageResolver, UniversalConfig, SearchLanguageResolver, UniversalConfig, IOptions, Setup, IServiceCollection, CancellationToken (+3 more)

### Community 153 - "SwaggerDefaultValues"
Cohesion: 0.33
Nodes (4): JsonValue, OpenApiOperation, OperationFilterContext, SwaggerDefaultValues

### Community 154 - "CustomValidator"
Cohesion: 0.11
Nodes (24): AbstractValidator, Inventory.Endpoints.StockReservations.v1.Commit, CustomValidator, RequestBody, Request, Body, Id, RequestBody (+16 more)

### Community 155 - "InterModuleRequestHandler"
Cohesion: 0.19
Nodes (8): IConsumer, IInterModuleRequestHandler, CancellationToken, Task, InterModuleRequestHandler, CancellationToken, ConsumeContext, Task

### Community 156 - "Request"
Cohesion: 0.17
Nodes (12): Guid, Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FirstName (+4 more)

### Community 157 - "IntegrationEventHandlerBase"
Cohesion: 0.24
Nodes (11): IntegrationEventHandlerBase, MaxEventAge, CancellationToken, ConsumeContext, DefaultIdType, IFusionCache, ILogger, IOptions (+3 more)

### Community 158 - ".FixedWindow"
Cohesion: 0.17
Nodes (9): RateLimitPartitions, HttpContext, IConnectionMultiplexer, ILoggerFactory, RateLimitPartition, HttpContext, RateLimitPartition, HttpContext (+1 more)

### Community 159 - "IInventoryDbContext"
Cohesion: 0.13
Nodes (15): DbSet, IInventoryDbContext, StockLevels, StockReservations, DbContextOptions, DbSet, ILogger, TimeProvider (+7 more)

### Community 160 - "IOtpService"
Cohesion: 0.06
Nodes (30): OtpCacheEntry, DateTimeOffset, CancellationToken, Task, TimeSpan, IOtpService, OtpVerificationOutcome, InvalidOtp (+22 more)

### Community 161 - ".BuildReservations"
Cohesion: 0.22
Nodes (7): List, RequestBody, RouteGroupBuilder, Endpoint, ICollection, Response, Ids

### Community 162 - "IAuditableEntity"
Cohesion: 0.18
Nodes (10): IAuditableEntity, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, DateTimeOffset, CancellationToken, DbContextEventData (+2 more)

### Community 164 - "Request"
Cohesion: 0.10
Nodes (19): Notifications.Infrastructure.Devices.UpdateCurrentPushToken, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, Common.Domain.Devices, DeviceSessionConstants, Guid, Request, ClientId, DeviceId (+11 more)

### Community 165 - "CurrentUser"
Cohesion: 0.15
Nodes (12): CurrentUser, Id, IdAsString, IsAuthenticated, Principal, Roles, SessionId, ClaimsPrincipal (+4 more)

### Community 166 - "Response"
Cohesion: 0.14
Nodes (11): IAM.Endpoints.Tokens.VersionNeutral, RouteGroupBuilder, Endpoint, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken (+3 more)

### Community 167 - ".PaginateAsync"
Cohesion: 0.29
Nodes (6): PaginationQueryableExtensions, CancellationToken, Expression, Func, IQueryable, Task

### Community 168 - "IProductsDbContext"
Cohesion: 0.20
Nodes (8): DbSet, Store, IProductsDbContext, Products, ProductTemplates, Stores, CancellationToken, Task

### Community 169 - "StronglyTypedIdWriteOnlyJsonConverter"
Cohesion: 0.24
Nodes (6): JsonConverter, StronglyTypedIdWriteOnlyJsonConverter, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 171 - ".UpdateStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

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
Cohesion: 0.10
Nodes (18): JsonElement, AuditLogDto, DateTimeOffset, PaginationResponse, HasNext, HasPrevious, NextPageNumber, PreviousPageNumber (+10 more)

### Community 176 - "PolymorphicEventConverter"
Cohesion: 0.29
Nodes (5): PolymorphicEventConverter, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 177 - ".AddProductToMyStoreAsync"
Cohesion: 0.22
Nodes (8): CancellationToken, ProductTemplate, RouteGroupBuilder, Store, Task, Endpoint, Response, Id

### Community 178 - "StrictDateTimeOffsetJsonConverter"
Cohesion: 0.33
Nodes (6): StrictDateTimeOffsetJsonConverter, DateTimeOffset, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

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

### Community 196 - "TokenResponseRepresentation"
Cohesion: 0.12
Nodes (15): List, DecisionRepresentation, Result, PermissionRepresentation, ResourceName, Scopes, TokenErrorRepresentation, Error (+7 more)

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
Cohesion: 0.19
Nodes (7): KeyValuePair, IEnumerable, ActivitySource, Counter, Meter, NotificationsTelemetry, UpDownCounter

### Community 201 - "KeycloakScopes"
Cohesion: 0.20
Nodes (9): Devices, Hangfire, KeycloakScopes, Products, ProductTemplates, Sessions, StockReservations, Stores (+1 more)

### Community 202 - "Request"
Cohesion: 0.20
Nodes (10): IAM.Endpoints.Tokens.VersionNeutral.Create, Guid, Request, ClientId, DeviceId, DeviceName, Otp, PhoneNumber (+2 more)

### Community 203 - ".ActivateProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

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

### Community 214 - "GetDeviceSessionsRequest"
Cohesion: 0.39
Nodes (7): DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, IReadOnlyList, CancellationToken, Task, GetDeviceSessionsRequestHandler

### Community 215 - "Request"
Cohesion: 0.29
Nodes (6): Products.Endpoints.ProductTemplates.v1.Create, Request, Brand, Color, Model, RequestValidator

### Community 216 - "ProductTemplateId"
Cohesion: 0.10
Nodes (12): Products.Endpoints.ProductTemplates.v1.Deactivate, Products.Endpoints.ProductTemplates.v1.Activate, StronglyTypedIdHelper, ProductTemplateId, DefaultIdType, ProductTemplateId, Request, Id (+4 more)

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
Cohesion: 0.18
Nodes (12): CancellationToken, ILogger, LoggerMessage, ProductsDbContext, Task, CancellationToken, List, Task (+4 more)

### Community 221 - "Keycloak realm as code"
Cohesion: 0.25
Nodes (7): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Secrets, Seed users (dev / test only)

### Community 222 - "Endpoint"
Cohesion: 0.29
Nodes (5): Products.Endpoints.ProductTemplates, RouteGroupBuilder, Setup, RouteGroupBuilder, Endpoint

### Community 223 - "StronglyTypedIdListReadOnlyJsonConverter"
Cohesion: 0.36
Nodes (6): StronglyTypedIdListReadOnlyJsonConverter, IReadOnlyList, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

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
Cohesion: 0.12
Nodes (15): Common.Application.Validation, DatabaseOptions, ConnectionString, DatabaseOptionsValidator, InterModuleRequestOptions, TimeoutSeconds, InterModuleRequestOptionsValidator, OtpOptions (+7 more)

### Community 229 - ".AddCommonOptions"
Cohesion: 0.20
Nodes (6): Setup, IConfiguration, IHostEnvironment, IServiceCollection, ValidationContextExtensions, ValidationContext

### Community 230 - "Request"
Cohesion: 0.20
Nodes (10): DateTimeOffset, DefaultIdType, RequestBody, Request, Body, RequestBody, ProductId, Quantity (+2 more)

### Community 231 - ".UpdateMyStoreAsync"
Cohesion: 0.17
Nodes (10): Products.Endpoints.Stores.v1.My.Update, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, Address, Description (+2 more)

### Community 232 - "FirebaseServiceAccountOptions"
Cohesion: 0.29
Nodes (7): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri

### Community 233 - "StockReservationExpirySweepService"
Cohesion: 0.20
Nodes (10): EntityEntry, CancellationToken, Exception, Guid, ILogger, IOptions, LoggerMessage, Task (+2 more)

### Community 234 - "Setup"
Cohesion: 0.29
Nodes (4): IApplicationBuilder, IWebHostEnvironment, Type, Setup

### Community 235 - "StoreId"
Cohesion: 0.14
Nodes (11): Products.Endpoints.Stores.v1.AuditLog, StoreId, DefaultIdType, StoreId, CancellationToken, RouteGroupBuilder, Task, Endpoint (+3 more)

### Community 236 - ".UpdateCurrentPushToken"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 237 - ".SeedAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, Task, CancellationToken, Task

### Community 238 - "NotificationsHub"
Cohesion: 0.31
Nodes (6): Hub, Exception, ILogger, LoggerMessage, Task, NotificationsHub

### Community 239 - ".TryReadFromJsonAsync"
Cohesion: 0.33
Nodes (4): HttpContent, CancellationToken, Task, HttpContentJsonExtensions

### Community 240 - "KeycloakUser"
Cohesion: 0.36
Nodes (6): DateOnly, IReadOnlyList, CreateKeycloakUser, GrantedPermission, KeycloakUser, KeycloakUserPage

### Community 241 - "SendForLogin/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForLogin, Request, CaptchaToken, PhoneNumber, RequestValidator

### Community 242 - ".AddCustomSwagger"
Cohesion: 0.20
Nodes (7): IOpenApiSchema, ISchemaFilter, SchemaFilterContext, IConfigureOptions, IServiceCollection, SwaggerGenOptions, StronglyTypedIdSchemaFilter

### Community 243 - ".RemoveProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 245 - "Host.Swagger"
Cohesion: 0.22
Nodes (5): Host.Swagger, IOperationFilter, OpenApiOperation, OperationFilterContext, RemoveDefaultResponseSchemaFilter

### Community 246 - ".MapEndpoint"
Cohesion: 0.29
Nodes (4): IAM.Endpoints.Users.VersionNeutral, RouteGroupBuilder, RouteGroupBuilder, Setup

### Community 247 - "StockReservationExpirySweepJobRegistrar"
Cohesion: 0.27
Nodes (8): IHostedService, CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task, StockReservationExpirySweepJobRegistrar

### Community 248 - ".SaveChangesAsync"
Cohesion: 0.22
Nodes (6): CancellationToken, Task, CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 249 - "Common.Application.EventBus"
Cohesion: 0.21
Nodes (6): Inventory.Application.IntegrationEventHandlers, Common.IntegrationEvents, Products.Application.Products.DomainEventHandlers.v1, Common.Application.EventBus, IEventHandlerWrapper, V1ProductCreatedDomainEventHandlers

### Community 250 - "Request"
Cohesion: 0.20
Nodes (9): Products.Endpoints.Stores.v1.Create, Request, Address, Description, Name, OwnerId, RequestValidator, Response (+1 more)

### Community 251 - "Common.Application.JsonConverters"
Cohesion: 0.22
Nodes (4): Common.Application.JsonConverters, IntegrationEventConverter, JsonSerializerOptions, EntityTypeBuilder

### Community 252 - "ICurrentUser"
Cohesion: 0.20
Nodes (8): ICurrentUser, Id, IdAsString, Roles, SessionId, ICollection, HttpContextExtensions, HttpContext

### Community 260 - ".SendCoreAsync"
Cohesion: 0.27
Nodes (7): IReadOnlyDictionary, IReadOnlyList, PushMessage, CancellationToken, IEnumerable, IReadOnlyList, Task

### Community 261 - "TokenCreateRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenCreateRateLimitingPolicy (+1 more)

### Community 262 - "Refresh/Request.cs"
Cohesion: 0.67
Nodes (3): Request, RefreshToken, RequestValidator

### Community 263 - "EnrichLogsWithUserInfoMiddleware"
Cohesion: 0.29
Nodes (5): IMiddleware, HttpContext, RequestDelegate, Task, EnrichLogsWithUserInfoMiddleware

### Community 264 - ".AddNetGsm"
Cohesion: 0.33
Nodes (5): IConfiguration, IFusionCache, IOptions, IServiceCollection, Setup

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
Cohesion: 0.33
Nodes (6): Request, Address, Description, Name, SearchTerm, RequestValidator

### Community 270 - "DefaultResponsesOperationFilter"
Cohesion: 0.70
Nodes (3): OpenApiOperation, OperationFilterContext, DefaultResponsesOperationFilter

### Community 271 - "Notifications.Application.Hubs"
Cohesion: 0.28
Nodes (3): Notifications.Infrastructure.Hubs, Notifications.Application.Hubs, Setup

### Community 272 - ".InvokeAsync"
Cohesion: 0.14
Nodes (11): IFeatureManagerSnapshot, RequireFeatureFilter, ActivitySource, Counter, EndpointFilterDelegate, EndpointFilterInvocationContext, IResxLocalizer, Meter (+3 more)

### Community 273 - "FixedWindow"
Cohesion: 0.29
Nodes (7): CustomRateLimitingOptionsValidator, FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit, FixedWindowValidator

### Community 274 - ".ReserveSeriesAsync"
Cohesion: 0.22
Nodes (10): GetProductRequest, GetProductResponse, DefaultIdType, CancellationToken, IOptions, Task, TimeProvider, CancellationToken (+2 more)

### Community 275 - ".ReserveStockAsync"
Cohesion: 0.29
Nodes (6): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Id

### Community 276 - "INotificationDispatcher"
Cohesion: 0.46
Nodes (4): CancellationToken, IReadOnlyList, Task, INotificationDispatcher

### Community 277 - "SendResponseBody"
Cohesion: 0.50
Nodes (4): SendResponseBody, Code, Description, JobId

### Community 278 - ".CreateProductTemplateAsync"
Cohesion: 0.29
Nodes (5): ProductTemplate, CancellationToken, Task, Response, Id

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
Cohesion: 0.29
Nodes (4): ApiVersionSet, Setup, IEndpointRouteBuilder, IServiceCollection

### Community 283 - "Request"
Cohesion: 0.40
Nodes (5): Inventory.Endpoints.StockReservations.v1.WebhookCallback, Request, ProviderReference, ReservationId, RequestValidator

### Community 284 - "ProblemDetailsContext"
Cohesion: 0.33
Nodes (4): ProblemDetailsContext, ProblemDetailsServiceExtensions, IProblemDetailsService, Task

### Community 285 - "Products.Endpoints.Probe"
Cohesion: 0.40
Nodes (3): Products.Endpoints.Probe, RouteGroupBuilder, Setup

### Community 286 - "SendForRegistration/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, Request, CaptchaToken, PhoneNumber, RequestValidator

### Community 287 - ".DeactivateProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 288 - "Inventory.Endpoints.StockReservations"
Cohesion: 0.40
Nodes (3): Inventory.Endpoints.StockReservations, RouteGroupBuilder, Setup

### Community 289 - "ModulesOptions"
Cohesion: 0.50
Nodes (4): ModulesOptions, EnabledModules, ModulesOptionsValidator, IReadOnlyList

### Community 291 - ".DeactivateStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 293 - ".RemoveMyProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 294 - ".SeedProductAsync"
Cohesion: 0.33
Nodes (5): CancellationToken, List, ProductTemplateId, StoreId, Task

### Community 295 - "v1/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Probe.v1, Request, Count, RequestValidator

### Community 296 - "InventoryTelemetry"
Cohesion: 0.40
Nodes (4): ActivitySource, Counter, Meter, InventoryTelemetry

### Community 297 - ".PhoneNumberValidation"
Cohesion: 0.50
Nodes (3): IRuleBuilder, IRuleBuilderOptions, IResxLocalizer

### Community 298 - "VersionNeutral/Get/Request.cs"
Cohesion: 0.67
Nodes (3): Request, Id, RequestValidator

### Community 299 - "VersionNeutral/Search/Request.cs"
Cohesion: 0.67
Nodes (3): Request, SearchTerm, RequestValidator

## Knowledge Gaps
- **826 isolated node(s):** `Id`, `IdAsString`, `Roles`, `SessionId`, `JwtClaimNames` (+821 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1884 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **87 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `Common.Domain.ResultMonad`, `DeviceRegistryReconciliationService`, `AuditLogOptions`, `SecurityHeadersOptions`, `Common.Application.Auth`, `Notifications.Application.Hubs`, `FixedWindow`, `SecurityHeadersMiddleware`, `Setup`, `OutboxOptions`, `.AddCommonCaching`, `ModulesOptions`, `Request`, `ObservabilityOptions`, `KeycloakOptions`, `ResxLocalizationOptions`, `CachingOptions`, `ReCaptchaService`, `RabbitMqOptions`, `SmsOptions`, `Inventory.Domain.StockReservations`, `Notifications.Infrastructure.Telemetry`, `ReverseProxyOptions`, `RequestLoggingOptions`, `CorsOptions`, `InventoryOptions`, `OpenApiOptions`, `Request`, `SelfRegister/Request.cs`, `PushOptions`, `Program.cs`, `Infrastructure/Setup.cs`, `Common.Domain.StronglyTypedIds`, `BackgroundJobsOptions`, `AuditLogRetentionJobRegistrar`, `Common.Application.Validation`, `ResiliencyOptions`, `.AddCommonOptions`, `Common.Application.BackgroundJobs`, `OutboxModule.cs`, `Host.Swagger`, `Common.Application.EventBus`, `FullTextSearchOptions`, `.AddCustomHealthChecks`?**
  _High betweenness centrality (0.226) - this node is a cross-community bridge._
- **Why does `Result` connect `Result` to `.SendCoreAsync`, `.UpdateProductAsync`, `.ListSessions`, `.SingleAsResult`, `Error`, `ProductTemplate`, `IInterModuleRequest`, `ICaptchaService`, `.GetMeAsync`, `Response`, `Response`, `ISmsGateway`, `.ReserveSeriesAsync`, `.ReserveStockAsync`, `ApplicationUserId`, `.RevokeSession`, `.CreateProductTemplateAsync`, `StockReservation`, `.GetAuditLogAsync`, `.SendAsync`, `.CommitStockReservationAsync`, `NetGsmSmsGateway`, `.DeactivateProductTemplateAsync`, `.AddProductAsync`, `.DeactivateStoreAsync`, `.RemoveMyProductAsync`, `.CreateTokens`, `.RegisterAsync`, `IProductsDbContext`, `.UpdateStoreAsync`, `.BindDeviceAsync`, `Products.Domain.Stores.DomainEvents.v1`, `PaginationResponse`, `.AddProductToMyStoreAsync`, `Response`, `ReCaptchaService`, `DummySmsGateway`, `.SearchProductTemplatesAsync`, `Response`, `DummyPushGateway`, `AuditableEntityResponse`, `.ActivateProductTemplateAsync`, `.CreateMyStoreAsync`, `.SearchStoresAsync`, `ResultTelemetryExtensions`, `IInterModuleRequestClient`, `.AddPushServices`, `Response`, `.SearchMyProductsAsync`, `.SaveChangesAsync`, `.SearchStoreProductsAsync`, `.UpdateMyStoreAsync`, `.ReleaseStockReservationAsync`, `.UpdateCurrentPushToken`, `.IsRegisteredAsync`, `StoreId`, `.RemoveProductAsync`, `Response`, `.HandleWarehouseWebhookAsync`, `.TapWhenFeatureEnabledAsync`?**
  _High betweenness centrality (0.165) - this node is a cross-community bridge._
- **Why does `ApplicationUserId` connect `ApplicationUserId` to `NotificationPayload`, `DeviceRegistryReconciliationService`, `KeycloakAdminClient`, `Response`, `KeycloakPermissionAuthorizationHandler`, `Response`, `AuditableEntity`, `INotificationDispatcher`, `.RevokeSession`, `DeviceRegistration`, `.SendAsync`, `KeycloakTokenClient`, `.ForUser`, `IAuditableEntity`, `For`, `CurrentUser`, `.RegisterAsync`, `IProductsDbContext`, `VersionNeutral/Get/Request.cs`, `.BindDeviceAsync`, `SendSecurityAlertRequestHandler`, `PaginationResponse`, `Response`, `Request`, `V1StoreCreatedDomainEvent`, `IStronglyTypedId`, `AuditableEntityResponse`, `.SearchStoresAsync`, `.Configure`, `GetDeviceSessionsRequest`, `Store`, `ProductTemplateId`, `Response`, `Common.Domain.StronglyTypedIds`, `Seeder`, `.HandleAsync`, `IntegrationEvent`, `KeycloakUser`, `Response`, `Request`, `ICurrentUser`?**
  _High betweenness centrality (0.094) - this node is a cross-community bridge._
- **What connects `Id`, `IdAsString`, `Roles` to the rest of the system?**
  _826 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Common.Domain.ResultMonad` be split into smaller, more focused modules?**
  _Cohesion score 0.06853146853146853 - nodes in this community are weakly interconnected._
- **Should `DeviceRegistryReconciliationService` be split into smaller, more focused modules?**
  _Cohesion score 0.07317073170731707 - nodes in this community are weakly interconnected._
- **Should `RedisFixedWindowRateLimiter` be split into smaller, more focused modules?**
  _Cohesion score 0.12554112554112554 - nodes in this community are weakly interconnected._