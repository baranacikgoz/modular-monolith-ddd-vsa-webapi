# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-09-15)

## Corpus Check
- 539 files · ~78,148 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 4400 nodes · 7899 edges · 351 communities (260 shown, 86 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 269 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `6d84518a`
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
- Common.Infrastructure.Persistence
- FirebasePushGateway
- Request
- Error
- Common.Application.Auth
- VerifyPhoneOtpResponse
- .SendOtp
- UserRepresentation
- KeycloakPermissionAuthorizationHandler
- EmailOptions
- HangfireCustomAuthorizationFilter
- DomainEvent
- Common.Domain.Events
- ApplicationUserId
- BoundedRequestCaptureStream
- DeviceRegistration
- .AddBrevo
- .RevokeSession
- NetGsmSmsGateway
- EventDispatcher
- RequestResponseBodyLoggingMiddleware
- KeycloakTokenClient
- Result
- Product
- .VerifyOtp
- ProductsModule.cs
- .UpdateMyProductAsync
- KeycloakPermissionClient
- OutboxMessage
- CustomRateLimitingOptions
- StockReservation
- .RefreshToken
- ObservabilityOptions
- .RegisterAsync
- StockReservationId
- .SaveWithOutboxAsync
- CreateStoreRateLimitingPolicy
- KeycloakOptions
- .BindDeviceAsync
- Common.Application.DTOs
- Outbox Misuse Check
- AuditableEntity
- Add Integration Event Command
- Response
- ProductsDbContext
- ReCaptchaService
- Inventory.Domain.StockReservations
- SendEmailOtpRequestHandler
- BrevoEmailGateway
- Uri
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
- Common.Application.Pagination
- IProductsDbContext
- .FixedWindow
- IntegrationEvent
- Request
- .SearchStoresAsync
- V1StockReservationCommitConflictDetectedDomainEvent
- .SendAsync
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
- .CreateStoreAsync
- .SearchMyProductsAsync
- DatabaseSeederOrchestrator
- .SeedProductAsync
- .SearchStoreProductsAsync
- ISmsGateway
- OutboxDbContext
- ResiliencyOptions
- HttpContextTargetingContextAccessor
- BackgroundJobsTelemetry
- EnrichLogsWithUserInfoMiddleware
- StronglyTypedIdReadOnlyJsonConverter
- OutboxModule.cs
- .ReleaseStockReservationAsync
- StockLevel
- .IsRegisteredAsync
- StrictDateTimeOffsetJsonConverter
- BackgroundJobsService
- OutboxCleanupJob
- Request
- Response
- .CreateTokensByEmail
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
- .SendAsync
- docker-compose.yml (Base Stack)
- .AddInfrastructure
- OtpVerifyRateLimitingPolicy
- Consumer Idempotency (IntegrationEventHandlerBase)
- SendRequestBody
- .ListSessions
- .NotFound
- .GlobalRateLimiter
- ResultToResponseTransformer
- ProductTemplate
- Response
- .GetMeAsync
- Response
- StronglyTypedIdListReadOnlyJsonConverter
- Response
- SecurityHeadersMiddleware
- UsernameSource
- IRecurringBackgroundJobs
- .SavingChangesAsync
- OtpOptions
- ReservationStatus
- OutboxOptions
- ISearchLanguageResolver
- SwaggerDefaultValues
- CustomValidator
- DummyEmailGateway
- Request
- .AddNetGsm
- FirebaseServiceAccountOptions
- IInventoryDbContext
- OtpServiceBase
- .ReserveSeriesAsync
- HttpWarehouseGateway
- For
- Request
- ICurrentUser
- Response
- .PaginateAsync
- Endpoint
- CachedCaptchaService
- Notifications.Application/IAssemblyReference.cs
- IOtpService
- ResxLocalizationOptions
- v1/AddProduct/Request.cs
- SendSecurityAlertRequestHandler
- PaginationResponse
- .Deactivate
- .SaveChangesAsync
- .AddPushServices
- IntegrationEventHandlerBase
- SmsOptions
- RabbitMqOptions
- IAM.Domain
- SendRequestBody
- RequestBody
- IamModule
- Policies.CreateStore.cs
- ReverseProxyOptions
- BoundedCaptureStream
- ServiceAccountTokenCache
- Split-Deployment PoC
- ProblemDetails
- Request
- CorsOptions
- InventoryOptions
- TokenEndpointRepresentations.cs
- PushMessage
- InventoryModule
- OpenApiOptions
- KeyValuePair
- KeycloakScopes
- Request
- SmsRateLimitingPolicy
- Configuration-Driven Module Loading
- Request
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- ConfigureSwaggerOptions
- Common.Application.BackgroundJobs
- Setup
- ProductsTelemetry
- IAM.Application.Captcha.Services
- .CreateProductTemplateAsync
- Common.Application.ModelBinders
- FeatureFlags
- Response
- IAM.Endpoints.Common.Validations
- Seeder
- Keycloak realm as code
- .RemoveProductAsync
- Stores/v1/Deactivate/Request.cs
- AuditLogRetentionService
- BackgroundJobsOptions
- IInterModuleRequest
- BackgroundJobsModule
- ProductTemplates/v1/Deactivate/Request.cs
- .AddCommonOptions
- Request
- Request
- Common.Application.Options
- ModulesOptions
- Setup
- SignalROptions
- .UpdateCurrentPushToken
- ReCaptchaResponse
- InterModuleRequestHandler
- .TryReadFromJsonAsync
- StringExtensions
- Request
- Host.Swagger
- v1/RemoveProduct/Request.cs
- .AddAuthInfrastructure
- RemoveDefaultResponseSchemaFilter
- KeycloakPermissionRequirement
- IAM.Endpoints
- SendErrorBody
- IntegrationEventOutbox
- StoreId
- HttpContextExtensions.cs
- Products.Domain/IAssemblyReference.cs
- IAM.Application/IAssemblyReference.cs
- Products.Infrastructure/IAssemblyReference.cs
- Products.Application/IAssemblyReference.cs
- Common.Infrastructure/IAssemblyReference.cs
- Notifications.Domain/IAssemblyReference.cs
- IAM.Infrastructure/IAssemblyReference.cs
- .TryDeserialize
- .UpdateMyStoreAsync
- .InvokeAsync
- DefaultResponsesOperationFilter
- RequireFeatureFilter
- GetProductRequest
- Inventory.Endpoints.StockReservations
- SendResponseBody
- ProductId
- .CommitStockReservationAsync
- Infrastructure/Setup.cs
- NotificationsModule.cs
- .WriteTooManyRequestsToResponse
- Products.Endpoints.Probe
- ProductTemplateId
- ICaptchaService
- Setup
- .DeactivateStoreAsync
- .RemoveMyProductAsync
- .AddProductAsync
- .GetAuditLogAsync
- DummySmsGateway
- Common.Application.Validation
- Common.Application.JsonConverters
- Inventory.Application.Gateway
- .UpdateStoreAsync
- .AddCustomSwagger
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
1. `Result` - 132 edges
2. `Common.Application.Options` - 123 edges
3. `Common.Domain.ResultMonad` - 101 edges
4. `CustomValidator` - 79 edges
5. `Common.Application.Validation` - 70 edges
6. `ApplicationUserId` - 69 edges
7. `Common.Application.Auth` - 66 edges
8. `Common.Application.Extensions` - 61 edges
9. `Common.Domain.StronglyTypedIds` - 59 edges
10. `Common.InterModuleRequests.Contracts` - 50 edges

## Surprising Connections (you probably didn't know these)
- `Aspire Dashboard Service (mm.aspire-dashboard)` --conceptually_related_to--> `Observability (OpenTelemetry)`  [INFERRED]
  docker-compose.yml → CLAUDE.md
- `CaptchaErrors` --references--> `Error`  [EXTRACTED]
  src/Modules/IAM/IAM.Domain/Captcha/CaptchaErrors.cs → src/Common/Common.Domain/ResultMonad/Error.cs
- `OtpErrors` --references--> `Error`  [EXTRACTED]
  src/Modules/IAM/IAM.Domain/Errors/OtpErrors.cs → src/Common/Common.Domain/ResultMonad/Error.cs
- `TokenErrors` --references--> `Error`  [EXTRACTED]
  src/Modules/IAM/IAM.Domain/Errors/TokenErrors.cs → src/Common/Common.Domain/ResultMonad/Error.cs
- `StockReservationErrors` --references--> `Error`  [EXTRACTED]
  src/Modules/Inventory/Inventory.Domain/StockReservations/Errors/StockReservationErrors.cs → src/Common/Common.Domain/ResultMonad/Error.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Local Infrastructure Stack** — docker_compose_postgres, docker_compose_rabbitmq, docker_compose_redis, docker_compose_aspire_dashboard [EXTRACTED 1.00]

## Communities (351 total, 86 thin omitted)

### Community 0 - "Common.Domain.ResultMonad"
Cohesion: 0.07
Nodes (27): Notifications.Application.Otp, IAM.Infrastructure.Keycloak.Representations, Common.Application.Caching, Common.Application.FeatureManagement, IAM.Endpoints.Otp, Notifications.Application.Persistence, Notifications.Infrastructure.Telemetry, Common.InterModuleRequests.Contracts (+19 more)

### Community 1 - "NotificationPayload"
Cohesion: 0.07
Nodes (31): Notifications.Infrastructure.Hubs, Notifications.Application.Hubs, Hub, HubConnectionContext, IHubContext, IUserIdProvider, RedisOptions, CancellationToken (+23 more)

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

### Community 7 - "Common.Infrastructure.Persistence"
Cohesion: 0.11
Nodes (14): Common.Infrastructure.Persistence, Notifications.Infrastructure.Persistence, Common.Infrastructure.Persistence.Auditing, Common.Infrastructure.Persistence.DbContext, SaveChangesInterceptor, ApplyAuditingInterceptor, TimeProvider, ApplySearchLanguageInterceptor (+6 more)

### Community 8 - "FirebasePushGateway"
Cohesion: 0.15
Nodes (12): FirebaseApp, FirebaseMessaging, IDisposable, CancellationToken, Exception, IEnumerable, ILogger, IReadOnlyList (+4 more)

### Community 9 - "Request"
Cohesion: 0.17
Nodes (12): Products.Endpoints.Products.v1.My.Update, RequestBody, Request, Body, Id, RequestBody, Description, Name (+4 more)

### Community 10 - "Error"
Cohesion: 0.09
Nodes (15): StringLocalizerExtensions, IStringLocalizer, Error, Key, ParameterName, StatusCode, SubErrors, Value (+7 more)

### Community 11 - "Common.Application.Auth"
Cohesion: 0.10
Nodes (17): Common.Application.Search, Products.Endpoints.Stores.v1.My.AddProduct, Common.Infrastructure.Persistence.Extensions, Products.Endpoints.Stores.v1.My.Create, Common.Application.Extensions, Products.Domain.Products, Products.Infrastructure.Telemetry, IAM.Infrastructure.Auth (+9 more)

### Community 12 - "VerifyPhoneOtpResponse"
Cohesion: 0.22
Nodes (10): OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, VerifyPhoneOtpResponseExtensions, CancellationToken (+2 more)

### Community 13 - ".SendOtp"
Cohesion: 0.10
Nodes (19): OtpDispatchErrors, SendPhoneOtpRequest, SendPhoneOtpResponse, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, CancellationToken (+11 more)

### Community 14 - "UserRepresentation"
Cohesion: 0.07
Nodes (29): Dictionary, List, CredentialRepresentation, Temporary, Type, Value, ErrorRepresentation, Error (+21 more)

### Community 15 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.14
Nodes (14): AuthorizationHandler, AuthorizationHandlerContext, CancellationToken, ClaimsPrincipal, HttpContext, IFusionCache, IHttpContextAccessor, IOptions (+6 more)

### Community 16 - "EmailOptions"
Cohesion: 0.09
Nodes (24): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+16 more)

### Community 17 - "HangfireCustomAuthorizationFilter"
Cohesion: 0.29
Nodes (5): DashboardContext, IAuthorizationService, IDashboardAsyncAuthorizationFilter, HangfireCustomAuthorizationFilter, Task

### Community 18 - "DomainEvent"
Cohesion: 0.06
Nodes (30): Products.Domain.Products.DomainEvents.v1, AggregateRoot, Events, Id, Version, IReadOnlyCollection, List, IAggregateRoot (+22 more)

### Community 19 - "Common.Domain.Events"
Cohesion: 0.13
Nodes (9): Inventory.Domain.StockReservations.DomainEvents.v1, Common.Domain.Events, Common.Application.Persistence.Outbox, UnknownDomainEvent, PolymorphicEventConverter, JsonSerializerOptions, Type, Utf8JsonReader (+1 more)

### Community 20 - "ApplicationUserId"
Cohesion: 0.12
Nodes (20): ApplicationUserId, IsEmpty, Value, DefaultIdType, CancellationToken, IReadOnlyList, Task, IKeycloakAdminClient (+12 more)

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.14
Nodes (8): ReadOnlySpan, BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position, Stream

### Community 22 - "DeviceRegistration"
Cohesion: 0.14
Nodes (13): DateTimeOffset, DefaultIdType, Guid, DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive (+5 more)

### Community 23 - ".AddBrevo"
Cohesion: 0.13
Nodes (13): CancellationToken, Task, EmailMessage, IEmailGateway, IConfiguration, IFusionCache, IOptions, IServiceCollection (+5 more)

### Community 24 - ".RevokeSession"
Cohesion: 0.13
Nodes (14): DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse, IReadOnlyList, CancellationToken, RouteGroupBuilder, Task, Endpoint, CancellationToken (+6 more)

### Community 25 - "NetGsmSmsGateway"
Cohesion: 0.32
Nodes (7): Exception, HttpClient, ILogger, IOptions, JsonSerializerOptions, LoggerMessage, NetGsmSmsGateway

### Community 26 - "EventDispatcher"
Cohesion: 0.11
Nodes (17): CancellationToken, Task, IEventHandlerWrapper, CancellationToken, Task, IEvent, CreatedOn, Id (+9 more)

### Community 27 - "RequestResponseBodyLoggingMiddleware"
Cohesion: 0.17
Nodes (9): IDiagnosticContext, IApplicationBuilder, IOptions, HttpContext, IList, IOptions, PathString, RequestDelegate (+1 more)

### Community 28 - "KeycloakTokenClient"
Cohesion: 0.15
Nodes (18): JsonWebTokenHandler, CancellationToken, Task, IKeycloakTokenClient, DateTimeOffset, KeycloakTokens, CancellationToken, Dictionary (+10 more)

### Community 29 - "Result"
Cohesion: 0.14
Nodes (14): Result, Error, IsFailure, Success, Value, Func, Task, AsyncExtensions (+6 more)

### Community 30 - "Product"
Cohesion: 0.08
Nodes (26): ProductTemplate, ProductTemplateId, Store, StoreId, Product, Description, Language, Name (+18 more)

### Community 31 - ".VerifyOtp"
Cohesion: 0.14
Nodes (14): IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, IssueVerificationTokenRequest, IssueVerificationTokenResponse, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response (+6 more)

### Community 32 - "ProductsModule.cs"
Cohesion: 0.10
Nodes (13): Products.Infrastructure.Persistence, Products.Endpoints.Stores.v1.Create, Products.Endpoints.Stores, Common.Endpoints.Versioning, Common.Application.Persistence, Products.Endpoints, IAssemblyReference, RouteGroupBuilder (+5 more)

### Community 33 - ".UpdateMyProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 34 - "KeycloakPermissionClient"
Cohesion: 0.23
Nodes (11): CancellationToken, Dictionary, HttpClient, HttpResponseMessage, ILogger, IOptions, IReadOnlyList, List (+3 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.08
Nodes (23): IEntityTypeConfiguration, IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, DateTimeOffset (+15 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.08
Nodes (22): CheckRegistrationRateLimitingPolicy, EmailRateLimitingPolicy, OtpVerifyRateLimitingPolicy, RegisterRateLimitingPolicy, SmsRateLimitingPolicy, CustomRateLimitingOptions, CheckRegistration, CreateStore (+14 more)

### Community 37 - "StockReservation"
Cohesion: 0.09
Nodes (22): DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationExpiredDomainEvent, DateTimeOffset, StockReservationId, V1StockReservationReleaseAttemptAbandonedDomainEvent, DateTimeOffset (+14 more)

### Community 38 - ".RefreshToken"
Cohesion: 0.15
Nodes (11): IAM.Endpoints.Tokens.VersionNeutral.Refresh, CancellationToken, RouteGroupBuilder, Task, Endpoint, DateTimeOffset, Response, AccessToken (+3 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.08
Nodes (22): IHostBuilder, LoggerConfiguration, LoggerMinimumLevelConfiguration, ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics (+14 more)

### Community 40 - ".RegisterAsync"
Cohesion: 0.14
Nodes (13): CancellationToken, Exception, IFeatureManager, ILogger, LoggerMessage, Task, Endpoint, ActivitySource (+5 more)

### Community 41 - "StockReservationId"
Cohesion: 0.13
Nodes (12): DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationReservedDomainEvent, DefaultIdType, StockReservationId, CancellationToken, RouteGroupBuilder (+4 more)

### Community 42 - ".SaveWithOutboxAsync"
Cohesion: 0.14
Nodes (16): AuditLogEntry, AggregateId, AggregateType, Event, EventType, Version, DefaultIdType, OutboxSaveHelper (+8 more)

### Community 43 - "CreateStoreRateLimitingPolicy"
Cohesion: 0.14
Nodes (12): CancellationToken, Func, HttpContext, IOptions, IProblemDetailsService, IResxLocalizer, OnRejectedContext, RateLimitPartition (+4 more)

### Community 44 - "KeycloakOptions"
Cohesion: 0.14
Nodes (14): KeycloakOptions, AttemptTimeoutSeconds, Authority, BaseUrl, DecisionCacheMaxDurationSeconds, Realm, RequireHttpsMetadata, ResourceClientId (+6 more)

### Community 45 - ".BindDeviceAsync"
Cohesion: 0.14
Nodes (17): BindDeviceSessionRequest, BindDeviceSessionResponse, Guid, CancellationToken, Exception, Guid, ILogger, LoggerMessage (+9 more)

### Community 46 - "Common.Application.DTOs"
Cohesion: 0.12
Nodes (8): Products.Endpoints.Products.v1.My.Get, Products.Endpoints.Products.v1.Search, Common.Application.DTOs, Products.Endpoints.Stores.v1.Get, Products.Endpoints.Products.v1.My.Search, Products.Endpoints.ProductTemplates.v1.Get, Products.Endpoints.Products.v1.Get, Products.Endpoints.Stores.v1.My.Get

### Community 48 - "AuditableEntity"
Cohesion: 0.11
Nodes (18): AuditableEntity, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, Version, DateTimeOffset (+10 more)

### Community 50 - "Response"
Cohesion: 0.17
Nodes (12): DateOnly, DateTimeOffset, Response, BirthDate, CreatedOn, Email, Enabled, FirstName (+4 more)

### Community 51 - "ProductsDbContext"
Cohesion: 0.17
Nodes (11): DbContextOptions, DbSet, ILogger, ModelBuilder, ProductTemplate, Store, TimeProvider, ProductsDbContext (+3 more)

### Community 52 - "ReCaptchaService"
Cohesion: 0.21
Nodes (10): FormUrlEncodedContent, ReCaptchaResponse, CancellationToken, Exception, HttpClient, ILogger, IOptions, LoggerMessage (+2 more)

### Community 53 - "Inventory.Domain.StockReservations"
Cohesion: 0.09
Nodes (18): Products.Infrastructure.InterModuleRequestHandlers, Inventory.Endpoints.StockReservations.v1.ReserveSeries, Inventory.Infrastructure.InterModuleRequestHandlers, Inventory.Endpoints, Inventory.Application.Persistence, Inventory.Domain.StockReservations, Inventory.Infrastructure.Persistence, Inventory.Domain.StockReservations.Errors (+10 more)

### Community 54 - "SendEmailOtpRequestHandler"
Cohesion: 0.17
Nodes (13): EmailOtpDispatchErrors, EmailOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, SendEmailOtpRequest, SendEmailOtpResponse, CancellationToken (+5 more)

### Community 55 - "BrevoEmailGateway"
Cohesion: 0.23
Nodes (10): Exception, HttpClient, HttpStatusCode, ILogger, IOptions, JsonSerializerOptions, LoggerMessage, BrevoEmailGateway (+2 more)

### Community 56 - "Uri"
Cohesion: 0.23
Nodes (8): OpenTelemetryBuilder, ResourceBuilder, Action, IConfiguration, IHostEnvironment, IReadOnlyList, IServiceCollection, Uri

### Community 58 - "OutboxMetricsJob"
Cohesion: 0.12
Nodes (14): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxMetricsJob (+6 more)

### Community 59 - ".SearchProductTemplatesAsync"
Cohesion: 0.09
Nodes (19): Products.Endpoints.ProductTemplates.v1.Search, LikePattern, Constants, CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task (+11 more)

### Community 61 - "Request"
Cohesion: 0.25
Nodes (8): ProductTemplateId, Request, Description, Name, Price, ProductTemplateId, Quantity, RequestValidator

### Community 62 - "IDbContext"
Cohesion: 0.09
Nodes (20): DatabaseFacade, EntityEntry, IDbContext, AuditLog, ChangeTracker, Database, DbSet, DbSet (+12 more)

### Community 63 - "RequestLoggingOptions"
Cohesion: 0.12
Nodes (18): IPostConfigureOptions, RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes (+10 more)

### Community 64 - ".SendCoreAsync"
Cohesion: 0.21
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
Cohesion: 0.21
Nodes (8): StronglyTypedIdWriteOnlyJsonConverter, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, IStronglyTypedId, Value, DefaultIdType

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

### Community 76 - "Common.Application.Pagination"
Cohesion: 0.06
Nodes (33): Products.Endpoints.Stores.v1.AuditLog, IAM.Endpoints.Users.VersionNeutral.Search, Products.Endpoints.Stores.v1.My.AuditLog, Products.Endpoints.Stores.v1.Search, Common.Application.AuditLog, Common.Application.Pagination, Products.Endpoints.Products.v1.AuditLog, PaginationRequest (+25 more)

### Community 77 - "IProductsDbContext"
Cohesion: 0.10
Nodes (16): DbSet, Store, IProductsDbContext, Products, ProductTemplates, Stores, CancellationToken, RouteGroupBuilder (+8 more)

### Community 78 - ".FixedWindow"
Cohesion: 0.06
Nodes (33): IRateLimiterPolicy, RateLimitPartitions, HttpContext, IConnectionMultiplexer, ILoggerFactory, RateLimitPartition, CancellationToken, Func (+25 more)

### Community 79 - "IntegrationEvent"
Cohesion: 0.12
Nodes (17): IReadOnlyList, IntegrationEvent, CreatedOn, Id, DateTimeOffset, DefaultIdType, ProductCreatedIntegrationEvent, StoreCreatedIntegrationEvent (+9 more)

### Community 80 - "Request"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Stores.v1.Update, RequestBody, Request, Body, Id, RequestBody, Address, Description (+3 more)

### Community 81 - ".SearchStoresAsync"
Cohesion: 0.15
Nodes (12): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Address (+4 more)

### Community 82 - "V1StockReservationCommitConflictDetectedDomainEvent"
Cohesion: 0.22
Nodes (8): DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationCommitConflictDetectedDomainEvent, DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationCommittedDomainEvent

### Community 83 - ".SendAsync"
Cohesion: 0.20
Nodes (7): SendResponseBody, CancellationToken, SendRequestBody, Task, SendMessageBody, Msg, No

### Community 84 - ".Configure"
Cohesion: 0.09
Nodes (20): AuditableEntityConfiguration, EntityTypeBuilder, AuditLogEntryConfiguration, EntityTypeBuilder, StronglyTypedIdValueConverter, DefaultIdType, EntityTypeBuilder, StockReservationConfiguration (+12 more)

### Community 85 - "ResultTelemetryExtensions"
Cohesion: 0.32
Nodes (4): Activity, ResultTelemetryExtensions, ActivitySource, Task

### Community 86 - "Store"
Cohesion: 0.08
Nodes (19): StoreId, V1StoreAddressUpdatedDomainEvent, StoreId, V1StoreDescriptionUpdatedDomainEvent, StoreId, V1StoreNameUpdatedDomainEvent, IReadOnlyCollection, List (+11 more)

### Community 87 - "PushOptions"
Cohesion: 0.19
Nodes (13): PushOptions, Provider, SendTimeoutSeconds, ServiceAccount, Templates, PushOptionsValidator, PushProvider, Dummy (+5 more)

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
Cohesion: 0.12
Nodes (10): Common.Domain.StronglyTypedIds, Inventory.Infrastructure.Persistence.EntityConfigurations, Notifications.Domain.Devices, Common.Domain.Entities, Common.Infrastructure.Persistence.EntityConfigurations, Common.Domain.Aggregates, Common.Infrastructure.Persistence.ValueConverters, Notifications.Infrastructure.Persistence.EntityConfigurations (+2 more)

### Community 93 - ".GetClientKey"
Cohesion: 0.18
Nodes (8): IAM.Endpoints.Captcha.VersionNeutral, IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, RouteGroupBuilder, Endpoint, Response, ClientKey, RouteGroupBuilder, Setup

### Community 94 - ".CreateStoreAsync"
Cohesion: 0.22
Nodes (6): CollectionExtensions, Func, ICollection, IEnumerable, CancellationToken, Task

### Community 95 - ".SearchMyProductsAsync"
Cohesion: 0.10
Nodes (19): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Request, Description (+11 more)

### Community 96 - "DatabaseSeederOrchestrator"
Cohesion: 0.05
Nodes (35): BackgroundService, IDatabaseSeeder, Priority, CancellationToken, Task, DatabaseSeederOrchestrator, CancellationToken, Exception (+27 more)

### Community 97 - ".SeedProductAsync"
Cohesion: 0.22
Nodes (7): CancellationToken, Task, CancellationToken, List, ProductTemplateId, StoreId, Task

### Community 98 - ".SearchStoreProductsAsync"
Cohesion: 0.17
Nodes (11): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Description (+3 more)

### Community 99 - "ISmsGateway"
Cohesion: 0.13
Nodes (13): CancellationToken, Task, ISmsGateway, SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, SmsMessage (+5 more)

### Community 100 - "OutboxDbContext"
Cohesion: 0.10
Nodes (16): DbContext, IOutboxDbContext, OutboxMessages, CancellationToken, DbSet, Task, IConfiguration, IServiceCollection (+8 more)

### Community 101 - "ResiliencyOptions"
Cohesion: 0.11
Nodes (18): HttpStandardResilienceOptions, IHttpClientBuilder, ResiliencyOptions, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds (+10 more)

### Community 102 - "HttpContextTargetingContextAccessor"
Cohesion: 0.17
Nodes (9): Common.Infrastructure.FeatureManagement, ITargetingContextAccessor, HttpContextTargetingContextAccessor, IHttpContextAccessor, ValueTask, Setup, IConfiguration, IServiceCollection (+1 more)

### Community 103 - "BackgroundJobsTelemetry"
Cohesion: 0.14
Nodes (11): IServerFilter, PerformedContext, PerformingContext, JobMetricsFilter, BackgroundJobsTelemetry, ActivitySource, ConcurrentDictionary, Counter (+3 more)

### Community 104 - "EnrichLogsWithUserInfoMiddleware"
Cohesion: 0.20
Nodes (7): IAuthenticationSchemeProvider, IMiddleware, IApplicationBuilder, HttpContext, RequestDelegate, Task, EnrichLogsWithUserInfoMiddleware

### Community 105 - "StronglyTypedIdReadOnlyJsonConverter"
Cohesion: 0.24
Nodes (6): JsonConverter, StronglyTypedIdReadOnlyJsonConverter, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 106 - "OutboxModule.cs"
Cohesion: 0.39
Nodes (4): Outbox, Common.Infrastructure.Persistence.Outbox, Outbox.Persistence, Outbox.Telemetry

### Community 107 - ".ReleaseStockReservationAsync"
Cohesion: 0.21
Nodes (8): CancellationToken, Task, IWarehouseGateway, CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 108 - "StockLevel"
Cohesion: 0.16
Nodes (11): Inventory.Domain.StockLevels.DomainEvents.v1, DefaultIdType, StockLevelId, V1StockLevelCreatedDomainEvent, DefaultIdType, StockLevel, ProductId, QuantityOnHand (+3 more)

### Community 109 - ".IsRegisteredAsync"
Cohesion: 0.18
Nodes (10): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, PhoneNumber, RequestValidator (+2 more)

### Community 110 - "StrictDateTimeOffsetJsonConverter"
Cohesion: 0.29
Nodes (6): StrictDateTimeOffsetJsonConverter, DateTimeOffset, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 111 - "BackgroundJobsService"
Cohesion: 0.23
Nodes (8): IBackgroundJobClientV2, BackgroundJobsService, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 112 - "OutboxCleanupJob"
Cohesion: 0.27
Nodes (8): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxCleanupJob

### Community 113 - "Request"
Cohesion: 0.17
Nodes (12): Products.Endpoints.Products.v1.Update, RequestBody, Request, Body, Id, RequestBody, Description, Name (+4 more)

### Community 114 - "Response"
Cohesion: 0.11
Nodes (17): IAM.Endpoints.Users.VersionNeutral.Me.Get, RouteGroupBuilder, Endpoint, DateOnly, DateTimeOffset, IReadOnlyCollection, Response, BirthDate (+9 more)

### Community 115 - ".CreateTokensByEmail"
Cohesion: 0.12
Nodes (17): VerifyEmailOtpRequest, VerifyEmailOtpResponse, VerifyEmailOtpResponseExtensions, CancellationToken, ILogger, RouteGroupBuilder, Task, Endpoint (+9 more)

### Community 116 - ".WriteProblemAsync"
Cohesion: 0.20
Nodes (10): IAllowAnonymous, IConfigureNamedOptions, HttpContext, HttpStatusCode, IOptions, IProblemDetailsService, IResxLocalizer, JwtBearerOptions (+2 more)

### Community 117 - "Response"
Cohesion: 0.11
Nodes (17): Inventory.Endpoints.StockReservations.v1.Get, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, Id, RequestValidator (+9 more)

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
Cohesion: 0.13
Nodes (12): BaseDbContext, AuditLog, CancellationToken, DateTimeOffset, DbContextOptions, DbSet, ILogger, ModelConfigurationBuilder (+4 more)

### Community 128 - "EmailRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, EmailRateLimitingPolicy (+1 more)

### Community 129 - ".SendAsync"
Cohesion: 0.10
Nodes (13): IClientFactory, CancellationToken, Task, MassTransitInterModuleRequestClient, CancellationToken, IOptions, Task, EmailNormalization (+5 more)

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

### Community 136 - ".NotFound"
Cohesion: 0.30
Nodes (6): PersistenceQueryableExtensions, CancellationToken, Expression, Func, IQueryable, Task

### Community 137 - ".GlobalRateLimiter"
Cohesion: 0.25
Nodes (7): PartitionedRateLimiter, HttpContext, IConfiguration, IReadOnlyList, IServiceCollection, PathString, RateLimitingMiddleware

### Community 138 - "ResultToResponseTransformer"
Cohesion: 0.26
Nodes (8): Common.Application.EndpointFilters, IEndpointFilter, ResultToCreatedResponseTransformer, ResultToResponseTransformer, IServiceProvider, IWebHostEnvironment, RouteHandlerBuilderExtensions, RouteHandlerBuilder

### Community 139 - "ProductTemplate"
Cohesion: 0.15
Nodes (11): IReadOnlyList, List, ProductTemplate, Brand, Color, IsActive, Model, Products (+3 more)

### Community 140 - "Response"
Cohesion: 0.15
Nodes (10): IAM.Endpoints.Users.VersionNeutral.SelfRegister, RouteGroupBuilder, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt (+2 more)

### Community 141 - ".GetMeAsync"
Cohesion: 0.19
Nodes (9): CancellationToken, IReadOnlyList, Task, IKeycloakPermissionClient, IReadOnlyList, GrantedPermission, CancellationToken, HttpContext (+1 more)

### Community 142 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 143 - "StronglyTypedIdListReadOnlyJsonConverter"
Cohesion: 0.36
Nodes (6): StronglyTypedIdListReadOnlyJsonConverter, IReadOnlyList, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 144 - "Response"
Cohesion: 0.12
Nodes (15): IAM.Endpoints.Users.VersionNeutral.Get, RouteGroupBuilder, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate, CreatedOn (+7 more)

### Community 145 - "SecurityHeadersMiddleware"
Cohesion: 0.18
Nodes (9): SecurityHeadersOptions, Headers, SecurityHeadersOptionsValidator, Dictionary, HttpContext, IOptions, RequestDelegate, Task (+1 more)

### Community 146 - "UsernameSource"
Cohesion: 0.18
Nodes (10): UserIdentityOptions, UsernameSource, UserIdentityOptionsValidator, UsernameSource, Email, PhoneNumber, IEndpointRouteBuilder, IOptions (+2 more)

### Community 147 - "IRecurringBackgroundJobs"
Cohesion: 0.13
Nodes (13): RecurringJobOptions, IRecurringBackgroundJobs, Action, Expression, Func, Task, RecurringBackgroundJobsService, Action (+5 more)

### Community 148 - ".SavingChangesAsync"
Cohesion: 0.25
Nodes (6): ISearchLocalized, Language, CancellationToken, DbContextEventData, InterceptionResult, ValueTask

### Community 149 - "OtpOptions"
Cohesion: 0.07
Nodes (24): OtpOptions, DummyCode, EmailQuotaWindowMinutes, ExpirationInMinutes, Length, MaxSendsPerEmailPerWindow, MaxSendsPerPhonePerWindow, PhoneQuotaWindowMinutes (+16 more)

### Community 150 - "ReservationStatus"
Cohesion: 0.29
Nodes (6): ReservationStatus, Active, Committed, Expired, Released, RequiresReconciliation

### Community 151 - "OutboxOptions"
Cohesion: 0.15
Nodes (13): OutboxOptions, BaseBackoffSeconds, BatchSize, ClaimLeaseSeconds, Cleanup, IsProcessor, LagThresholdMinutes, MaxBackoffSeconds (+5 more)

### Community 152 - "ISearchLanguageResolver"
Cohesion: 0.23
Nodes (7): ISearchLanguageResolver, UniversalConfig, SearchLanguageResolver, UniversalConfig, IOptions, Setup, IServiceCollection

### Community 153 - "SwaggerDefaultValues"
Cohesion: 0.33
Nodes (4): JsonValue, OpenApiOperation, OperationFilterContext, SwaggerDefaultValues

### Community 154 - "CustomValidator"
Cohesion: 0.08
Nodes (31): AbstractValidator, Inventory.Endpoints.StockReservations.v1.Commit, CustomRateLimitingOptionsValidator, FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit (+23 more)

### Community 155 - "DummyEmailGateway"
Cohesion: 0.38
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, DummyEmailGateway

### Community 156 - "Request"
Cohesion: 0.15
Nodes (13): Guid, Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FirstName (+5 more)

### Community 157 - ".AddNetGsm"
Cohesion: 0.33
Nodes (5): IConfiguration, IFusionCache, IOptions, IServiceCollection, Setup

### Community 158 - "FirebaseServiceAccountOptions"
Cohesion: 0.29
Nodes (7): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri

### Community 159 - "IInventoryDbContext"
Cohesion: 0.12
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
Cohesion: 0.12
Nodes (16): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, IAM.Domain.Users, Constants, Guid, Request, ClientId, DeviceId, DeviceName (+8 more)

### Community 165 - "ICurrentUser"
Cohesion: 0.09
Nodes (20): Common.Infrastructure.Auth.Services, Common.Infrastructure.Auth, ICurrentUser, Id, IdAsString, Roles, SessionId, ICollection (+12 more)

### Community 166 - "Response"
Cohesion: 0.14
Nodes (11): IAM.Endpoints.Tokens.VersionNeutral, RouteGroupBuilder, Endpoint, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken (+3 more)

### Community 167 - ".PaginateAsync"
Cohesion: 0.29
Nodes (6): PaginationQueryableExtensions, CancellationToken, Expression, Func, IQueryable, Task

### Community 168 - "Endpoint"
Cohesion: 0.29
Nodes (5): Products.Endpoints.ProductTemplates, RouteGroupBuilder, Setup, RouteGroupBuilder, Endpoint

### Community 169 - "CachedCaptchaService"
Cohesion: 0.25
Nodes (5): CancellationToken, IFusionCache, IOptions, Task, CachedCaptchaService

### Community 171 - "IOtpService"
Cohesion: 0.13
Nodes (14): CancellationToken, Task, TimeSpan, IOtpService, OtpVerificationOutcome, InvalidOtp, Success, TooManyAttempts (+6 more)

### Community 172 - "ResxLocalizationOptions"
Cohesion: 0.25
Nodes (7): ResxLocalizationOptions, DefaultCulture, SupportedCultures, ResxLocalizationOptionsValidator, ICollection, IApplicationBuilder, IOptions

### Community 173 - "v1/AddProduct/Request.cs"
Cohesion: 0.17
Nodes (12): ProductTemplateId, RequestBody, Request, Body, Id, RequestBody, Description, Name (+4 more)

### Community 174 - "SendSecurityAlertRequestHandler"
Cohesion: 0.27
Nodes (9): SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, CancellationToken, IOptions, RequestLocalizationOptions, Task (+1 more)

### Community 175 - "PaginationResponse"
Cohesion: 0.07
Nodes (26): JsonElement, AuditLogDto, DateTimeOffset, PaginationResponse, HasNext, HasPrevious, NextPageNumber, PreviousPageNumber (+18 more)

### Community 176 - ".Deactivate"
Cohesion: 0.29
Nodes (4): StoreId, V1StoreDeactivatedDomainEvent, StoreId, V1StoreDeactivatedWithStockOnHandDomainEvent

### Community 177 - ".SaveChangesAsync"
Cohesion: 0.08
Nodes (18): CancellationToken, Task, CancellationToken, RouteGroupBuilder, Task, Endpoint, CancellationToken, RouteGroupBuilder (+10 more)

### Community 178 - ".AddPushServices"
Cohesion: 0.24
Nodes (7): CancellationToken, ILogger, LoggerMessage, Task, DummyPushGateway, IConfiguration, IServiceCollection

### Community 179 - "IntegrationEventHandlerBase"
Cohesion: 0.06
Nodes (43): IntegrationEventHandlerBase, MaxEventAge, CancellationToken, ConsumeContext, DefaultIdType, IFusionCache, ILogger, IOptions (+35 more)

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
Cohesion: 0.14
Nodes (12): Action, IApplicationBuilder, IConfiguration, IEnumerable, IServiceCollection, RateLimiterOptions, IamModule, ActivitySourceNames (+4 more)

### Community 186 - "Policies.CreateStore.cs"
Cohesion: 0.17
Nodes (8): CreateStoreRateLimitingPolicy, Products.Infrastructure.RateLimiting, RateLimiterOptions, Policies, Action, IEnumerable, RateLimiterOptions, RateLimitingConstants

### Community 187 - "ReverseProxyOptions"
Cohesion: 0.18
Nodes (9): ForwardedHeadersOptions, ReverseProxyOptions, ForwardLimit, IsEnabled, TrustedNetworks, ReverseProxyOptionsValidator, IReadOnlyList, IConfiguration (+1 more)

### Community 188 - "BoundedCaptureStream"
Cohesion: 0.14
Nodes (8): HttpResponse, SeekOrigin, BoundedCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 190 - "ServiceAccountTokenCache"
Cohesion: 0.06
Nodes (27): CancellationToken, Task, IServiceAccountTokenProvider, KeycloakPaths, TokenResponseRepresentation, AccessToken, ExpiresIn, RefreshExpiresIn (+19 more)

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

### Community 197 - "PushMessage"
Cohesion: 0.29
Nodes (6): IReadOnlyDictionary, CancellationToken, IReadOnlyList, Task, IPushGateway, PushMessage

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
Cohesion: 0.09
Nodes (20): IAM.Endpoints.Tokens.VersionNeutral.Create, Notifications.Infrastructure.Devices.UpdateCurrentPushToken, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, Common.Domain.Devices, DeviceSessionConstants, Guid, Request, ClientId (+12 more)

### Community 203 - "SmsRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, SmsRateLimitingPolicy (+1 more)

### Community 205 - "Request"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForEmail, Request, CaptchaToken, Email, RequestValidator

### Community 210 - "ConfigureSwaggerOptions"
Cohesion: 0.28
Nodes (7): ApiVersionDescription, IApiVersionDescriptionProvider, IConfigureOptions, OpenApiInfo, IOptions, SwaggerGenOptions, ConfigureSwaggerOptions

### Community 211 - "Common.Application.BackgroundJobs"
Cohesion: 0.31
Nodes (3): Common.Application.BackgroundJobs, BackgroundJobs.Telemetry, BackgroundJobs

### Community 212 - "Setup"
Cohesion: 0.33
Nodes (4): ApiVersionSet, Setup, IEndpointRouteBuilder, IServiceCollection

### Community 213 - "ProductsTelemetry"
Cohesion: 0.40
Nodes (4): ActivitySource, Counter, Meter, ProductsTelemetry

### Community 214 - "IAM.Application.Captcha.Services"
Cohesion: 0.25
Nodes (5): IAM.Domain.Captcha, IAM.Infrastructure.Captcha.Services, IAM.Application.Captcha.Services, IAM.Infrastructure.Captcha, CaptchaErrors

### Community 215 - ".CreateProductTemplateAsync"
Cohesion: 0.15
Nodes (11): Products.Endpoints.ProductTemplates.v1.Create, ProductTemplate, CancellationToken, Task, Request, Brand, Color, Model (+3 more)

### Community 216 - "Common.Application.ModelBinders"
Cohesion: 0.08
Nodes (22): Inventory.Endpoints.StockReservations.v1.Release, Common.Application.ModelBinders, Products.Endpoints.ProductTemplates.v1.Activate, IModelBinder, ModelBindingContext, StronglyTypedIdBinder, Task, Request (+14 more)

### Community 217 - "FeatureFlags"
Cohesion: 0.33
Nodes (5): Checkout, FeatureFlags, IAM, Notifications, Products

### Community 218 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 219 - "IAM.Endpoints.Common.Validations"
Cohesion: 0.09
Nodes (18): IAM.Endpoints.Otp.VersionNeutral.SendForLogin, IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, IAM.Endpoints.Common.Validations, IResxLocalizer, IRuleBuilder, IRuleBuilderOptions, CommonValidations, IResxLocalizer (+10 more)

### Community 220 - "Seeder"
Cohesion: 0.17
Nodes (8): Products.Infrastructure.Persistence.Seeding, Products.Endpoints.Probe.v1, Common.InterModuleRequests.IAM, IAM.Infrastructure.InterModuleRequestHandlers, ILogger, LoggerMessage, ProductsDbContext, Seeder

### Community 221 - "Keycloak realm as code"
Cohesion: 0.25
Nodes (7): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Secrets, Seed users (dev / test only)

### Community 222 - ".RemoveProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 223 - "Stores/v1/Deactivate/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Stores.v1.Deactivate, Request, Id, RequestValidator

### Community 224 - "AuditLogRetentionService"
Cohesion: 0.05
Nodes (36): Common.Infrastructure.Persistence.AuditLog, IHostedService, AuditLogRetentionJobRegistrar, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task (+28 more)

### Community 225 - "BackgroundJobsOptions"
Cohesion: 0.16
Nodes (11): BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount, BackgroundJobsOptionsValidator, IApplicationBuilder (+3 more)

### Community 226 - "IInterModuleRequest"
Cohesion: 0.15
Nodes (12): IInterModuleRequest, GetSeedUserIdsRequest, GetSeedUserIdsResponse, ICollection, CancellationToken, Task, GetSeedUserIdsRequestHandler, CancellationToken (+4 more)

### Community 227 - "BackgroundJobsModule"
Cohesion: 0.22
Nodes (8): ICoreModule, BackgroundJobsModule, ActivitySourceNames, MeterNames, Name, StartupPriority, IEndpointRouteBuilder, IEnumerable

### Community 228 - "ProductTemplates/v1/Deactivate/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.ProductTemplates.v1.Deactivate, Request, Id, RequestValidator

### Community 229 - ".AddCommonOptions"
Cohesion: 0.20
Nodes (6): Setup, IConfiguration, IHostEnvironment, IServiceCollection, ValidationContextExtensions, ValidationContext

### Community 230 - "Request"
Cohesion: 0.17
Nodes (12): Inventory.Endpoints.StockReservations.v1.Reserve, DateTimeOffset, DefaultIdType, RequestBody, Request, Body, RequestBody, ProductId (+4 more)

### Community 231 - "Request"
Cohesion: 0.33
Nodes (6): Products.Endpoints.Stores.v1.My.Update, Request, Address, Description, Name, RequestValidator

### Community 232 - "Common.Application.Options"
Cohesion: 0.11
Nodes (16): Notifications.Application.Sms, Notifications.Infrastructure.Email, Common.Infrastructure.RateLimiting, Notifications.Infrastructure.Sms, Notifications.Infrastructure.Email.Brevo, Common.Infrastructure.Extensions, Common.Application.Options, IAM.Endpoints.Otp.VersionNeutral (+8 more)

### Community 233 - "ModulesOptions"
Cohesion: 0.50
Nodes (4): ModulesOptions, EnabledModules, ModulesOptionsValidator, IReadOnlyList

### Community 234 - "Setup"
Cohesion: 0.29
Nodes (4): IApplicationBuilder, IWebHostEnvironment, Type, Setup

### Community 235 - "SignalROptions"
Cohesion: 0.50
Nodes (4): SignalROptions, RedisConnectionString, UseRedisBackplane, SignalROptionsValidator

### Community 236 - ".UpdateCurrentPushToken"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 237 - "ReCaptchaResponse"
Cohesion: 0.25
Nodes (7): DateTime, ReCaptchaResponse, ChallengeTs, ErrorCodes, Hostname, Score, Success

### Community 238 - "InterModuleRequestHandler"
Cohesion: 0.19
Nodes (8): IConsumer, IInterModuleRequestHandler, CancellationToken, Task, InterModuleRequestHandler, CancellationToken, ConsumeContext, Task

### Community 239 - ".TryReadFromJsonAsync"
Cohesion: 0.33
Nodes (4): HttpContent, CancellationToken, Task, HttpContentJsonExtensions

### Community 240 - "StringExtensions"
Cohesion: 0.29
Nodes (3): Common.Domain.Extensions, SearchValues, StringExtensions

### Community 241 - "Request"
Cohesion: 0.50
Nodes (4): Request, Email, Otp, RequestValidator

### Community 242 - "Host.Swagger"
Cohesion: 0.22
Nodes (5): Host.Swagger, IOpenApiSchema, ISchemaFilter, SchemaFilterContext, StronglyTypedIdSchemaFilter

### Community 243 - "v1/RemoveProduct/Request.cs"
Cohesion: 0.29
Nodes (7): Products.Endpoints.Stores.v1.RemoveProduct, ProductId, StoreId, Request, Id, ProductId, RequestValidator

### Community 244 - ".AddAuthInfrastructure"
Cohesion: 0.25
Nodes (6): IAuthorizationHandler, IAuthorizationPolicyProvider, IConfigureOptions, IServiceCollection, JwtBearerOptions, Setup

### Community 245 - "RemoveDefaultResponseSchemaFilter"
Cohesion: 0.33
Nodes (4): IOperationFilter, OpenApiOperation, OperationFilterContext, RemoveDefaultResponseSchemaFilter

### Community 246 - "KeycloakPermissionRequirement"
Cohesion: 0.18
Nodes (6): IAuthorizationRequirement, KeycloakPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder, KeycloakPermissionRequirement, Permission

### Community 248 - "SendErrorBody"
Cohesion: 0.67
Nodes (3): SendErrorBody, Code, Message

### Community 249 - "IntegrationEventOutbox"
Cohesion: 0.06
Nodes (29): Inventory.Application.IntegrationEventHandlers, Common.IntegrationEvents, Common.Infrastructure.EventBus, Products.Application.Products.DomainEventHandlers.v1, Products.Application.Stores.DomainEventHandlers.v1, Common.Application.EventBus, Lock, DomainEventHandlerBase (+21 more)

### Community 251 - "StoreId"
Cohesion: 0.25
Nodes (6): StoreId, DefaultIdType, StoreId, CancellationToken, List, Task

### Community 265 - ".UpdateMyStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 267 - ".InvokeAsync"
Cohesion: 0.33
Nodes (5): IFeatureManagerSnapshot, EndpointFilterDelegate, EndpointFilterInvocationContext, IResxLocalizer, ValueTask

### Community 270 - "DefaultResponsesOperationFilter"
Cohesion: 0.70
Nodes (3): OpenApiOperation, OperationFilterContext, DefaultResponsesOperationFilter

### Community 272 - "RequireFeatureFilter"
Cohesion: 0.22
Nodes (6): RequireFeatureFilter, ActivitySource, Counter, Meter, RouteHandlerBuilderExtensions, RouteHandlerBuilder

### Community 274 - "GetProductRequest"
Cohesion: 0.39
Nodes (6): GetProductRequest, GetProductResponse, DefaultIdType, CancellationToken, Task, GetProductRequestHandler

### Community 276 - "Inventory.Endpoints.StockReservations"
Cohesion: 0.40
Nodes (3): Inventory.Endpoints.StockReservations, RouteGroupBuilder, Setup

### Community 277 - "SendResponseBody"
Cohesion: 0.50
Nodes (4): SendResponseBody, Code, Description, JobId

### Community 278 - "ProductId"
Cohesion: 0.15
Nodes (12): Products.Endpoints.Stores.v1.My.RemoveProduct, DefaultIdType, ProductId, Request, Id, RequestValidator, Request, Id (+4 more)

### Community 281 - ".CommitStockReservationAsync"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 282 - "Infrastructure/Setup.cs"
Cohesion: 0.08
Nodes (12): Common.Infrastructure.Modules, Common.InterModuleRequests, Common.Infrastructure.Localization, Host.Middlewares, Common.Infrastructure.Caching, Host.Infrastructure, OtlpExportProtocol, Setup (+4 more)

### Community 283 - "NotificationsModule.cs"
Cohesion: 0.15
Nodes (8): Notifications.Infrastructure.Devices, Notifications.Application.Push, Notifications.Infrastructure.Push, Notifications.Infrastructure.Push.Firebase, Notifications.Infrastructure, PushErrors, IAssemblyReference, Setup

### Community 284 - ".WriteTooManyRequestsToResponse"
Cohesion: 0.14
Nodes (11): ProblemDetailsContext, ProblemDetailsServiceExtensions, IProblemDetailsService, Task, CancellationToken, Func, IProblemDetailsService, IResxLocalizer (+3 more)

### Community 285 - "Products.Endpoints.Probe"
Cohesion: 0.40
Nodes (3): Products.Endpoints.Probe, RouteGroupBuilder, Setup

### Community 286 - "ProductTemplateId"
Cohesion: 0.25
Nodes (6): ProductTemplateId, DefaultIdType, ProductTemplateId, CancellationToken, List, Task

### Community 288 - "ICaptchaService"
Cohesion: 0.31
Nodes (5): ICaptchaService, DummyCaptchaService, IConfiguration, IServiceCollection, Setup

### Community 289 - "Setup"
Cohesion: 0.11
Nodes (16): LoadAll, ModuleRegistry, Names, Type, Setup, Assembly, Exception, IApplicationBuilder (+8 more)

### Community 291 - ".DeactivateStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 293 - ".RemoveMyProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 294 - ".AddProductAsync"
Cohesion: 0.22
Nodes (8): CancellationToken, ProductTemplate, RouteGroupBuilder, Store, Task, Endpoint, Response, Id

### Community 295 - ".GetAuditLogAsync"
Cohesion: 0.38
Nodes (5): DbContextExtensions, CancellationToken, DbSet, JsonSerializerOptions, Task

### Community 297 - "DummySmsGateway"
Cohesion: 0.38
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, DummySmsGateway

### Community 298 - "Common.Application.Validation"
Cohesion: 0.11
Nodes (16): Common.Application.Validation, AuditLogOptions, PurgeBatchSize, RetentionDays, AuditLogOptionsValidator, DatabaseOptions, ConnectionString, DatabaseOptionsValidator (+8 more)

### Community 299 - "Common.Application.JsonConverters"
Cohesion: 0.14
Nodes (12): Common.Application.JsonConverters, DomainEventConverter, JsonSerializerOptions, IntegrationEventConverter, JsonSerializerOptions, Request, Address, Description (+4 more)

### Community 300 - "Inventory.Application.Gateway"
Cohesion: 0.25
Nodes (5): Inventory.Infrastructure.Gateway, Inventory.Application.Gateway, CancellationToken, Task, DummyWarehouseGateway

### Community 303 - ".UpdateStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 307 - ".AddCustomSwagger"
Cohesion: 0.50
Nodes (3): IConfigureOptions, IServiceCollection, SwaggerGenOptions

## Knowledge Gaps
- **881 isolated node(s):** `Id`, `IdAsString`, `Roles`, `SessionId`, `JwtClaimNames` (+876 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1995 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **86 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `Common.Domain.ResultMonad`, `.SendAsync`, `NotificationPayload`, `DeviceRegistryReconciliationService`, `Common.Infrastructure.Persistence`, `Common.Application.Auth`, `EmailOptions`, `SecurityHeadersMiddleware`, `UsernameSource`, `OtpOptions`, `ISearchLanguageResolver`, `CustomValidator`, `Infrastructure/Setup.cs`, `Request`, `NotificationsModule.cs`, `ProductsModule.cs`, `Request`, `Response`, `ObservabilityOptions`, `Common.Application.Validation`, `KeycloakOptions`, `ResxLocalizationOptions`, `Inventory.Application.Gateway`, `IntegrationEventHandlerBase`, `SmsOptions`, `RabbitMqOptions`, `Inventory.Domain.StockReservations`, `Policies.CreateStore.cs`, `ReverseProxyOptions`, `RequestLoggingOptions`, `CorsOptions`, `InventoryOptions`, `OpenApiOptions`, `Request`, `CaptchaOptions`, `Common.Application.BackgroundJobs`, `IAM.Application.Captcha.Services`, `PushOptions`, `Program.cs`, `Common.Domain.StronglyTypedIds`, `AuditLogRetentionService`, `BackgroundJobsOptions`, `ResiliencyOptions`, `.AddCommonOptions`, `ModulesOptions`, `OutboxModule.cs`, `SignalROptions`, `Host.Swagger`, `IntegrationEventOutbox`, `FullTextSearchOptions`, `.AddCustomHealthChecks`?**
  _High betweenness centrality (0.266) - this node is a cross-community bridge._
- **Why does `Result` connect `Result` to `.SendAsync`, `.ListSessions`, `.NotFound`, `FirebasePushGateway`, `Error`, `ProductTemplate`, `VerifyPhoneOtpResponse`, `.SendOtp`, `.GetMeAsync`, `Response`, `.UpdateMyStoreAsync`, `ApplicationUserId`, `.AddBrevo`, `.RevokeSession`, `.CommitStockReservationAsync`, `NetGsmSmsGateway`, `DummyEmailGateway`, `KeycloakTokenClient`, `.VerifyOtp`, `.ReserveSeriesAsync`, `HttpWarehouseGateway`, `.UpdateMyProductAsync`, `.DeactivateStoreAsync`, `StockReservation`, `.RefreshToken`, `.GetAuditLogAsync`, `.RegisterAsync`, `CachedCaptchaService`, `StockReservationId`, `DummySmsGateway`, `Inventory.Application.Gateway`, `.BindDeviceAsync`, `.AddProductAsync`, `PaginationResponse`, `.Deactivate`, `.SaveChangesAsync`, `.AddPushServices`, `.UpdateStoreAsync`, `ReCaptchaService`, `SendEmailOtpRequestHandler`, `BrevoEmailGateway`, `.SearchProductTemplatesAsync`, `.SendCoreAsync`, `AuditableEntityResponse`, `.RemoveMyProductAsync`, `PushMessage`, `Response`, `IProductsDbContext`, `.SearchStoresAsync`, `V1StockReservationCommitConflictDetectedDomainEvent`, `.SendAsync`, `ResultTelemetryExtensions`, `.CreateProductTemplateAsync`, `IInterModuleRequestClient`, `Response`, `.GetClientKey`, `.CreateStoreAsync`, `.SearchMyProductsAsync`, `.RemoveProductAsync`, `.SearchStoreProductsAsync`, `ISmsGateway`, `.ReleaseStockReservationAsync`, `.UpdateCurrentPushToken`, `.IsRegisteredAsync`, `StringExtensions`, `.CreateTokensByEmail`, `Response`, `.HandleWarehouseWebhookAsync`, `.TapWhenFeatureEnabledAsync`?**
  _High betweenness centrality (0.168) - this node is a cross-community bridge._
- **Why does `ApplicationUserId` connect `ApplicationUserId` to `NotificationPayload`, `DeviceRegistryReconciliationService`, `KeycloakAdminClient`, `.TryDeserialize`, `.ListSessions`, `Response`, `KeycloakPermissionAuthorizationHandler`, `Response`, `DeviceRegistration`, `.RevokeSession`, `KeycloakTokenClient`, `For`, `ICurrentUser`, `.RegisterAsync`, `Common.Application.JsonConverters`, `.BindDeviceAsync`, `SendSecurityAlertRequestHandler`, `PaginationResponse`, `AuditableEntity`, `Response`, `Request`, `AuditableEntityResponse`, `IStronglyTypedId`, `IProductsDbContext`, `IntegrationEvent`, `.SearchStoresAsync`, `.Configure`, `Store`, `Common.Application.ModelBinders`, `Response`, `Common.Domain.StronglyTypedIds`, `IInterModuleRequest`, `Response`, `IntegrationEventOutbox`, `StoreId`?**
  _High betweenness centrality (0.081) - this node is a cross-community bridge._
- **What connects `Id`, `IdAsString`, `Roles` to the rest of the system?**
  _881 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Common.Domain.ResultMonad` be split into smaller, more focused modules?**
  _Cohesion score 0.07081377151799687 - nodes in this community are weakly interconnected._
- **Should `NotificationPayload` be split into smaller, more focused modules?**
  _Cohesion score 0.06594071385359952 - nodes in this community are weakly interconnected._
- **Should `DeviceRegistryReconciliationService` be split into smaller, more focused modules?**
  _Cohesion score 0.07317073170731707 - nodes in this community are weakly interconnected._