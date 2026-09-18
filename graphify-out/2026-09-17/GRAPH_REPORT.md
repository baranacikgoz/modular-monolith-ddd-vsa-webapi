# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-09-17)

## Corpus Check
- 544 files · ~79,842 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 4453 nodes · 8015 edges · 366 communities (268 shown, 93 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 275 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `f218c5eb`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- .HandleAsync
- NotificationPayload
- OutboxProcessor
- DeviceRegistryReconciliationService
- Hybrid DDD (Writes) / VSA (Reads)
- ApplicationUserId
- RedisFixedWindowRateLimiter
- Common.Infrastructure.Persistence
- FirebasePushGateway
- Notifications.Application.Push
- Error
- Common.Domain.ResultMonad
- Result
- ICaptchaService
- UserRepresentation
- KeycloakPermissionAuthorizationHandler
- EmailOptions
- HangfireCustomAuthorizationFilter
- DomainEvent
- IntegrationEventHandlerBase
- IntegrationEvent
- BoundedRequestCaptureStream
- DeviceRegistration
- .AddBrevo
- ICurrentUser
- NetGsmSmsGateway
- IEvent
- RequestResponseBodyLoggingMiddleware
- KeycloakTokenClient
- Func
- Product
- Request
- Endpoint
- Products.Domain.Products.DomainEvents.v1
- KeycloakPermissionClient
- OutboxMessage
- CustomRateLimitingOptions
- StockReservation
- Response
- ObservabilityOptions
- .AssignBasicRoleOrRollbackAsync
- Request
- .SaveWithOutboxAsync
- CreateStoreRateLimitingPolicy
- KeycloakOptions
- .RegisterAsync
- Common.Application.DTOs
- Outbox Misuse Check
- AuditableEntity
- Add Integration Event Command
- Response
- .AddKeycloakInfrastructure
- ReCaptchaService
- Inventory.Domain.StockReservations
- SendEmailOtpRequestHandler
- BrevoEmailGateway
- IDatabaseSeeder
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
- NotificationsModule
- IStronglyTypedId
- Full-Text Search
- Response
- .WriteAsync
- AsNoTracking Coverage Check
- TokenResponseRepresentation
- PaginationRequestValidator
- IProductsDbContext
- CheckRegistrationRateLimitingPolicy
- CreateStockLevelOnProductCreatedHandler
- Request
- .SearchStoresAsync
- Notifications.Application.Hubs
- NotificationsDbContext
- .Configure
- ResultTelemetryExtensions
- Store
- PushOptions
- Configuration-Driven Module Registration
- Program.cs
- .GetProductAsync
- Request
- Common.Domain.StronglyTypedIds
- Endpoint
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
- Request
- Response
- OutboxModule.cs
- .ReleaseStockReservationAsync
- StockLevel
- Response
- V1StoreCreatedDomainEvent
- BackgroundJobsService
- OutboxCleanupJob
- ProductsDbContext
- Response
- .VerifyOtp
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
- .AddNotificationsSignalR
- .WriteTooManyRequestsToResponse
- ResultToResponseTransformer
- ProductTemplate
- Endpoint
- IKeycloakPermissionClient
- Response
- .UseModules
- Response
- EnrichLogsWithUserInfoMiddleware
- Response
- IRecurringBackgroundJobs
- AuditLogRetentionService
- OtpService
- StockReservationExpirySweepJobRegistrar
- OutboxOptions
- .EnsureNoMigrationsPending
- SwaggerDefaultValues
- CustomValidator
- DummyEmailGateway
- Request
- ISearchLanguageResolver
- EventDispatcher
- IInventoryDbContext
- OtpServiceBase
- .BuildReservations
- NotificationsHub
- For
- Request
- CurrentUser
- IamModule
- PaginationRequest
- Response
- SendSecurityAlertRequestHandler
- Notifications.Application/IAssemblyReference.cs
- IOtpService
- ResxLocalizationOptions
- v1/AddProduct/Request.cs
- .AddPushServices
- PaginationResponse
- .FixedWindow
- Request
- DummyPushGateway
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
- ServiceAccountTokenCache
- Common.Application.ModelBinders
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
- Common.Application.FeatureManagement
- .CreateProductTemplateAsync
- ProductTemplateId
- FeatureFlags
- Response
- IAM.Endpoints.Common.Validations
- Seeder
- Keycloak realm as code
- .Failure
- .SeedProductAsync
- Endpoint
- BackgroundJobsOptions
- GetSeedUserIdsRequest
- .AddServices
- .ListSessions
- .AddCommonOptions
- StockReservationId
- Request
- Common.Application.Options
- Common.Application.Validation
- Setup
- AuditLogEntry
- ProblemDetails
- RequireFeatureFilter
- InterModuleRequestHandler
- .TryReadFromJsonAsync
- RedisOtpService
- RegisterRateLimitingPolicy
- .AddCustomSwagger
- FixedWindow
- .SavingChangesAsync
- Host.Swagger
- INotificationDispatcher
- Common.InterModuleRequests
- SendErrorBody
- IntegrationEventOutbox
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
- Response
- AuditableEntityResponse
- .AddCommonCaching
- Response
- V1ProductCreatedDomainEvent
- Request
- Request
- StringExtensions
- .InvokeAsync
- DefaultResponsesOperationFilter
- ProblemDetailsContext
- Common.InterModuleRequests.Contracts
- EmailOtpDispatchOutcome
- .ReserveSeriesAsync
- Request
- SmsOtpDispatchOutcome
- SendResponseBody
- ProductId
- Response
- InventoryTelemetry
- .CommitStockReservationAsync
- Setup
- .RequireOtpTemplateForDefaultCulture
- Request
- ProductsModule.cs
- ProblemDetailsExtensions
- Inventory.Infrastructure.StockReservations
- SignalROptions
- .AddModules
- HttpContextExtensions.cs
- .SeedProductTemplatesAsync
- .SeedStoresAsync
- CurrentUser.cs
- Infrastructure/StringExtensions.cs
- .Token
- ISmsGateway
- SendPhoneOtpRequestHandler
- Request
- .LocalizeFromError
- IResult
- .MapOtpEndpoints
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
- `EmailErrors` --references--> `Error`  [EXTRACTED]
  src/Modules/Notifications/Notifications.Application/Email/EmailErrors.cs → src/Common/Common.Domain/ResultMonad/Error.cs
- `PushErrors` --references--> `Error`  [EXTRACTED]
  src/Modules/Notifications/Notifications.Application/Push/PushErrors.cs → src/Common/Common.Domain/ResultMonad/Error.cs
- `SmsErrors` --references--> `Error`  [EXTRACTED]
  src/Modules/Notifications/Notifications.Application/Sms/SmsErrors.cs → src/Common/Common.Domain/ResultMonad/Error.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Local Infrastructure Stack** — docker_compose_postgres, docker_compose_rabbitmq, docker_compose_redis, docker_compose_aspire_dashboard [EXTRACTED 1.00]

## Communities (366 total, 93 thin omitted)

### Community 0 - ".HandleAsync"
Cohesion: 0.18
Nodes (9): Products.Endpoints.Probe.v1, CancellationToken, IResult, RouteGroupBuilder, Task, Endpoint, Request, Count (+1 more)

### Community 1 - "NotificationPayload"
Cohesion: 0.34
Nodes (8): IHubContext, Task, INotificationsClient, NotificationPayload, CancellationToken, IReadOnlyList, Task, SignalRNotificationDispatcher

### Community 2 - "OutboxProcessor"
Cohesion: 0.22
Nodes (11): IPublishEndpoint, CancellationToken, Exception, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task (+3 more)

### Community 3 - "DeviceRegistryReconciliationService"
Cohesion: 0.07
Nodes (29): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator, IReadOnlyCollection, GetActiveSessionIdsRequest, GetActiveSessionIdsResponse (+21 more)

### Community 5 - "ApplicationUserId"
Cohesion: 0.15
Nodes (21): HttpRequestMessage, ApplicationUserId, IsEmpty, Value, DefaultIdType, KeycloakUser, KeycloakUserPage, CancellationToken (+13 more)

### Community 6 - "RedisFixedWindowRateLimiter"
Cohesion: 0.13
Nodes (16): RateLimiter, RateLimiterStatistics, RateLimitLease, FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration (+8 more)

### Community 7 - "Common.Infrastructure.Persistence"
Cohesion: 0.10
Nodes (16): Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Common.Application.Persistence, Notifications.Domain.Devices, Notifications.Infrastructure.Persistence, Common.Infrastructure.Persistence.Auditing, Common.Infrastructure.Persistence.DbContext, SaveChangesInterceptor (+8 more)

### Community 8 - "FirebasePushGateway"
Cohesion: 0.24
Nodes (8): FirebaseApp, FirebaseMessaging, IDisposable, Exception, ILogger, LoggerMessage, TimeSpan, FirebasePushGateway

### Community 9 - "Notifications.Application.Push"
Cohesion: 0.25
Nodes (5): Notifications.Application.Push, Notifications.Infrastructure.Push, Notifications.Infrastructure.Push.Firebase, PushErrors, Setup

### Community 10 - "Error"
Cohesion: 0.12
Nodes (13): Error, Key, ParameterName, StatusCode, SubErrors, Value, HttpStatusCode, ICollection (+5 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.12
Nodes (17): Common.Application.Search, Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions, Products.Endpoints.Stores.v1.My.Create, Common.Application.Extensions, Products.Endpoints.ProductTemplates.v1.Create, Products.Domain.Products, Products.Infrastructure.Telemetry (+9 more)

### Community 12 - "Result"
Cohesion: 0.08
Nodes (25): Result, Error, IsFailure, Value, CancellationToken, IReadOnlyList, Task, IKeycloakAdminClient (+17 more)

### Community 13 - "ICaptchaService"
Cohesion: 0.06
Nodes (26): SendPhoneOtpRequest, SendPhoneOtpResponse, CancellationToken, Task, ICaptchaService, EmailNormalization, CancellationToken, IFeatureManager (+18 more)

### Community 14 - "UserRepresentation"
Cohesion: 0.07
Nodes (29): Dictionary, List, CredentialRepresentation, Temporary, Type, Value, ErrorRepresentation, Error (+21 more)

### Community 15 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.05
Nodes (34): AuthorizationHandler, AuthorizationHandlerContext, AuthorizationOptions, AuthorizationPolicy, DefaultAuthorizationPolicyProvider, IAuthorizationHandler, IAuthorizationPolicyProvider, IAuthorizationRequirement (+26 more)

### Community 16 - "EmailOptions"
Cohesion: 0.09
Nodes (24): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+16 more)

### Community 17 - "HangfireCustomAuthorizationFilter"
Cohesion: 0.29
Nodes (5): DashboardContext, IAuthorizationService, IDashboardAsyncAuthorizationFilter, HangfireCustomAuthorizationFilter, Task

### Community 18 - "DomainEvent"
Cohesion: 0.08
Nodes (27): AggregateRoot, Events, Id, Version, IReadOnlyCollection, List, IAggregateRoot, Events (+19 more)

### Community 19 - "IntegrationEventHandlerBase"
Cohesion: 0.22
Nodes (12): IConsumer, IntegrationEventHandlerBase, MaxEventAge, CancellationToken, ConsumeContext, DefaultIdType, IFusionCache, ILogger (+4 more)

### Community 20 - "IntegrationEvent"
Cohesion: 0.22
Nodes (9): IReadOnlyList, IntegrationEvent, CreatedOn, Id, DateTimeOffset, DefaultIdType, ProductCreatedIntegrationEvent, StoreCreatedIntegrationEvent (+1 more)

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.14
Nodes (8): ReadOnlySpan, BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position, Stream

### Community 22 - "DeviceRegistration"
Cohesion: 0.12
Nodes (15): DateTimeOffset, DefaultIdType, Guid, DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive (+7 more)

### Community 23 - ".AddBrevo"
Cohesion: 0.13
Nodes (13): CancellationToken, Task, EmailMessage, IEmailGateway, IConfiguration, IFusionCache, IOptions, IServiceCollection (+5 more)

### Community 24 - "ICurrentUser"
Cohesion: 0.09
Nodes (20): ICurrentUser, Id, IdAsString, Roles, SessionId, ICollection, DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse (+12 more)

### Community 25 - "NetGsmSmsGateway"
Cohesion: 0.16
Nodes (14): SendResponseBody, CancellationToken, Exception, HttpClient, ILogger, IOptions, JsonSerializerOptions, LoggerMessage (+6 more)

### Community 26 - "IEvent"
Cohesion: 0.11
Nodes (14): DomainEventHandlerBase, CancellationToken, Task, IEventHandler, CancellationToken, Task, IEventHandlerWrapper, CancellationToken (+6 more)

### Community 27 - "RequestResponseBodyLoggingMiddleware"
Cohesion: 0.19
Nodes (7): IDiagnosticContext, HttpContext, IList, IOptions, PathString, RequestDelegate, RequestResponseBodyLoggingMiddleware

### Community 28 - "KeycloakTokenClient"
Cohesion: 0.20
Nodes (13): JsonWebTokenHandler, CancellationToken, Dictionary, Error, Exception, HttpClient, ILogger, IOptions (+5 more)

### Community 29 - "Func"
Cohesion: 0.27
Nodes (4): AsyncExtensions, SyncExtensions, Func, Task

### Community 30 - "Product"
Cohesion: 0.07
Nodes (29): Products.Endpoints.Stores.v1.AddProduct, ProductTemplate, ProductTemplateId, Store, StoreId, Product, Description, Language (+21 more)

### Community 31 - "Request"
Cohesion: 0.17
Nodes (10): IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, RouteGroupBuilder, Endpoint, Request, Email, Otp, RequestValidator, Response (+2 more)

### Community 32 - "Endpoint"
Cohesion: 0.33
Nodes (4): RouteGroupBuilder, Setup, RouteGroupBuilder, Endpoint

### Community 33 - "Products.Domain.Products.DomainEvents.v1"
Cohesion: 0.09
Nodes (14): Products.Domain.Products.DomainEvents.v1, Products.Application.Products.DomainEventHandlers.v1, ProductId, V1ProductDescriptionUpdatedDomainEvent, ProductId, V1ProductNameUpdatedDomainEvent, ProductId, V1ProductPriceDecreasedDomainEvent (+6 more)

### Community 34 - "KeycloakPermissionClient"
Cohesion: 0.23
Nodes (11): CancellationToken, Dictionary, HttpClient, HttpResponseMessage, ILogger, IOptions, IReadOnlyList, List (+3 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.10
Nodes (20): IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, DateTimeOffset, OutboxMessage (+12 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.08
Nodes (22): CheckRegistrationRateLimitingPolicy, EmailRateLimitingPolicy, OtpVerifyRateLimitingPolicy, RegisterRateLimitingPolicy, SmsRateLimitingPolicy, CustomRateLimitingOptions, CheckRegistration, CreateStore (+14 more)

### Community 37 - "StockReservation"
Cohesion: 0.06
Nodes (31): Inventory.Domain.StockReservations.DomainEvents.v1, DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationCommitConflictDetectedDomainEvent, DateTimeOffset, DefaultIdType, StockReservationId (+23 more)

### Community 38 - "Response"
Cohesion: 0.18
Nodes (9): IAM.Endpoints.Tokens.VersionNeutral.Refresh, RouteGroupBuilder, Endpoint, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken (+1 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.07
Nodes (30): IHostBuilder, LoggerConfiguration, LoggerMinimumLevelConfiguration, OpenTelemetryBuilder, ResourceBuilder, ObservabilityOptions, AppName, AppVersion (+22 more)

### Community 40 - ".AssignBasicRoleOrRollbackAsync"
Cohesion: 0.12
Nodes (12): CancellationToken, Exception, ILogger, LoggerMessage, Task, RegistrationCompletion, ActivitySource, Counter (+4 more)

### Community 41 - "Request"
Cohesion: 0.13
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
Cohesion: 0.07
Nodes (41): IInterModuleRequestClient, CancellationToken, Task, BindDeviceSessionRequest, BindDeviceSessionResponse, Guid, CancellationToken, Task (+33 more)

### Community 46 - "Common.Application.DTOs"
Cohesion: 0.11
Nodes (9): Products.Endpoints.Stores.v1.Search, Products.Endpoints.Products.v1.My.Get, Products.Endpoints.Products.v1.Search, Common.Application.DTOs, Products.Endpoints.Stores.v1.Get, Products.Endpoints.Products.v1.Get, Products.Endpoints.Stores.v1.My.Get, Constants (+1 more)

### Community 48 - "AuditableEntity"
Cohesion: 0.11
Nodes (18): AuditableEntity, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, Version, DateTimeOffset (+10 more)

### Community 50 - "Response"
Cohesion: 0.13
Nodes (14): RouteGroupBuilder, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate, CreatedOn, Email (+6 more)

### Community 51 - ".AddKeycloakInfrastructure"
Cohesion: 0.16
Nodes (10): CancellationToken, Task, IServiceAccountTokenProvider, HttpClient, IOptions, TimeProvider, ServiceAccountTokenProvider, IOptions (+2 more)

### Community 52 - "ReCaptchaService"
Cohesion: 0.06
Nodes (36): DateTime, FormUrlEncodedContent, ReCaptchaResponse, CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey (+28 more)

### Community 53 - "Inventory.Domain.StockReservations"
Cohesion: 0.12
Nodes (12): Products.Infrastructure.InterModuleRequestHandlers, Inventory.Infrastructure.InterModuleRequestHandlers, Inventory.Application.Persistence, Inventory.Infrastructure.Persistence.EntityConfigurations, Inventory.Domain.StockReservations, Inventory.Domain.StockReservations.Errors, Inventory.Endpoints.StockReservations.v1.Get, Inventory.Infrastructure.Telemetry (+4 more)

### Community 54 - "SendEmailOtpRequestHandler"
Cohesion: 0.27
Nodes (8): SendEmailOtpRequest, SendEmailOtpResponse, CancellationToken, IFusionCache, IOptions, RequestLocalizationOptions, Task, SendEmailOtpRequestHandler

### Community 55 - "BrevoEmailGateway"
Cohesion: 0.23
Nodes (10): Exception, HttpClient, HttpStatusCode, ILogger, IOptions, JsonSerializerOptions, LoggerMessage, BrevoEmailGateway (+2 more)

### Community 56 - "IDatabaseSeeder"
Cohesion: 0.17
Nodes (9): IDatabaseSeeder, Priority, CancellationToken, Task, CancellationToken, IServiceScopeFactory, Task, ProductsDatabaseSeeder (+1 more)

### Community 58 - "OutboxMetricsJob"
Cohesion: 0.13
Nodes (14): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxMetricsJob (+6 more)

### Community 59 - ".SearchProductTemplatesAsync"
Cohesion: 0.09
Nodes (19): Products.Endpoints.ProductTemplates.v1.Search, LikePattern, Constants, CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task (+11 more)

### Community 61 - "Request"
Cohesion: 0.13
Nodes (13): Products.Endpoints.Stores.v1.My.AddProduct, RouteGroupBuilder, Endpoint, ProductTemplateId, Request, Description, Name, Price (+5 more)

### Community 62 - "IDbContext"
Cohesion: 0.22
Nodes (7): DatabaseFacade, EntityEntry, IDbContext, AuditLog, ChangeTracker, Database, DbSet

### Community 63 - "RequestLoggingOptions"
Cohesion: 0.11
Nodes (20): IPostConfigureOptions, RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes (+12 more)

### Community 64 - ".SendCoreAsync"
Cohesion: 0.21
Nodes (8): SendErrorBody, CancellationToken, HttpResponseMessage, SendRequestBody, Task, SendContact, Email, Name

### Community 65 - "IBackgroundJobs"
Cohesion: 0.26
Nodes (7): IBackgroundJobs, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 66 - "OutboxModule"
Cohesion: 0.13
Nodes (16): Action, Exception, IApplicationBuilder, IEndpointRouteBuilder, IEnumerable, IHostApplicationLifetime, ILogger, ILoggerFactory (+8 more)

### Community 67 - "Response"
Cohesion: 0.20
Nodes (8): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Brand, Color, Model

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

### Community 75 - "TokenResponseRepresentation"
Cohesion: 0.12
Nodes (15): List, DecisionRepresentation, Result, PermissionRepresentation, ResourceName, Scopes, TokenErrorRepresentation, Error (+7 more)

### Community 76 - "PaginationRequestValidator"
Cohesion: 0.20
Nodes (9): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Products.v1.AuditLog, PaginationRequestValidator, Request, Id, RequestValidator, Request, Id (+1 more)

### Community 77 - "IProductsDbContext"
Cohesion: 0.03
Nodes (61): CancellationToken, Task, CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint, DbSet (+53 more)

### Community 78 - "CheckRegistrationRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, CheckRegistrationRateLimitingPolicy (+1 more)

### Community 79 - "CreateStockLevelOnProductCreatedHandler"
Cohesion: 0.24
Nodes (8): CancellationToken, DefaultIdType, IFusionCache, ILogger, IOptions, LoggerMessage, Task, CreateStockLevelOnProductCreatedHandler

### Community 80 - "Request"
Cohesion: 0.20
Nodes (10): Products.Endpoints.Stores.v1.Update, RequestBody, Request, Body, Id, RequestBody, Address, Description (+2 more)

### Community 81 - ".SearchStoresAsync"
Cohesion: 0.15
Nodes (12): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Address (+4 more)

### Community 82 - "Notifications.Application.Hubs"
Cohesion: 0.18
Nodes (4): Notifications.Infrastructure.Hubs, Notifications.Application.Hubs, NotificationGroupName, Setup

### Community 83 - "NotificationsDbContext"
Cohesion: 0.15
Nodes (13): DbSet, INotificationsDbContext, DeviceRegistrations, DbContextOptions, DbSet, ILogger, TimeProvider, NotificationsDbContext (+5 more)

### Community 84 - ".Configure"
Cohesion: 0.14
Nodes (14): AuditableEntityConfiguration, EntityTypeBuilder, StronglyTypedIdValueConverter, DefaultIdType, EntityTypeBuilder, StockReservationConfiguration, EntityTypeBuilder, NpgsqlTsVector (+6 more)

### Community 85 - "ResultTelemetryExtensions"
Cohesion: 0.32
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

### Community 91 - "Request"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Products.v1.My.Update, RequestBody, Request, Body, Id, RequestBody, Description, Name (+3 more)

### Community 92 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.06
Nodes (15): IAM.Endpoints.Users.VersionNeutral.Search, Common.Domain.StronglyTypedIds, Common.Domain.Events, IAM.Endpoints.Users.VersionNeutral.Get, Common.Application.JsonConverters, IAM.Endpoints.Users.VersionNeutral.Me.Get, Common.Infrastructure.EventBus, Common.Domain.Entities (+7 more)

### Community 93 - "Endpoint"
Cohesion: 0.18
Nodes (8): IAM.Endpoints.Captcha.VersionNeutral, IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, RouteGroupBuilder, Endpoint, Response, ClientKey, RouteGroupBuilder, Setup

### Community 94 - "OtpOptions"
Cohesion: 0.18
Nodes (11): OtpOptions, DummyCode, EmailQuotaWindowMinutes, ExpirationInMinutes, Length, MaxSendsPerEmailPerWindow, MaxSendsPerPhonePerWindow, PhoneQuotaWindowMinutes (+3 more)

### Community 95 - ".SearchMyProductsAsync"
Cohesion: 0.09
Nodes (21): Products.Endpoints.Products.v1.My.Search, CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Request (+13 more)

### Community 96 - "DatabaseSeederOrchestrator"
Cohesion: 0.11
Nodes (17): BackgroundService, DatabaseSeederOrchestrator, CancellationToken, Exception, IHostApplicationLifetime, ILogger, IServiceScopeFactory, LoggerMessage (+9 more)

### Community 97 - "StockReservationExpirySweepService"
Cohesion: 0.24
Nodes (9): CancellationToken, Exception, Guid, ILogger, IOptions, LoggerMessage, Task, TimeProvider (+1 more)

### Community 98 - ".SearchStoreProductsAsync"
Cohesion: 0.17
Nodes (11): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Description (+3 more)

### Community 99 - ".AddNetGsm"
Cohesion: 0.20
Nodes (7): IFusionCache, IOptions, CancellationToken, IFusionCache, IOptions, Task, ThrottledSmsGateway

### Community 100 - "OutboxDbContext"
Cohesion: 0.14
Nodes (13): DbContext, IOutboxDbContext, OutboxMessages, CancellationToken, DbSet, Task, IConfiguration, IServiceCollection (+5 more)

### Community 101 - "ResiliencyOptions"
Cohesion: 0.11
Nodes (18): HttpStandardResilienceOptions, IHttpClientBuilder, ResiliencyOptions, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds (+10 more)

### Community 102 - "HttpContextTargetingContextAccessor"
Cohesion: 0.17
Nodes (9): Common.Infrastructure.FeatureManagement, ITargetingContextAccessor, HttpContextTargetingContextAccessor, IHttpContextAccessor, ValueTask, Setup, IConfiguration, IServiceCollection (+1 more)

### Community 103 - "BackgroundJobsTelemetry"
Cohesion: 0.13
Nodes (11): IServerFilter, PerformedContext, PerformingContext, JobMetricsFilter, BackgroundJobsTelemetry, ActivitySource, ConcurrentDictionary, Counter (+3 more)

### Community 104 - "Request"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Products.v1.Update, RequestBody, Request, Body, Id, RequestBody, Description, Name (+3 more)

### Community 105 - "Response"
Cohesion: 0.20
Nodes (8): RouteGroupBuilder, Endpoint, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 106 - "OutboxModule.cs"
Cohesion: 0.13
Nodes (9): Outbox, Common.Infrastructure.Persistence.Outbox, Outbox.Persistence, Common.Application.Persistence.Outbox, Outbox.Telemetry, IEntityTypeConfiguration, ModelBuilder, EntityTypeBuilder (+1 more)

### Community 107 - ".ReleaseStockReservationAsync"
Cohesion: 0.05
Nodes (34): Inventory.Infrastructure.Gateway, Inventory.Application.Gateway, ReleaseResponseBody, CollectionExtensions, Func, ICollection, IEnumerable, PersistenceQueryableExtensions (+26 more)

### Community 108 - "StockLevel"
Cohesion: 0.15
Nodes (11): Inventory.Domain.StockLevels.DomainEvents.v1, DefaultIdType, StockLevelId, V1StockLevelCreatedDomainEvent, DefaultIdType, StockLevel, ProductId, QuantityOnHand (+3 more)

### Community 109 - "Response"
Cohesion: 0.29
Nodes (5): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, RouteGroupBuilder, Endpoint, Response, IsRegistered

### Community 110 - "V1StoreCreatedDomainEvent"
Cohesion: 0.36
Nodes (7): CancellationToken, Task, SimulateSomeBusinessHandler, StoreCreatedIntegrationEventPublishingHandler, V1StoreCreatedDomainEventHandlers, StoreId, V1StoreCreatedDomainEvent

### Community 111 - "BackgroundJobsService"
Cohesion: 0.23
Nodes (8): IBackgroundJobClientV2, BackgroundJobsService, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 112 - "OutboxCleanupJob"
Cohesion: 0.27
Nodes (8): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxCleanupJob

### Community 113 - "ProductsDbContext"
Cohesion: 0.13
Nodes (14): DbContextOptions, DbSet, ILogger, ProductTemplate, Store, TimeProvider, ProductsDbContext, Products (+6 more)

### Community 114 - "Response"
Cohesion: 0.12
Nodes (16): RouteGroupBuilder, Endpoint, DateOnly, DateTimeOffset, IReadOnlyCollection, Response, BirthDate, CreatedOn (+8 more)

### Community 115 - ".VerifyOtp"
Cohesion: 0.10
Nodes (19): IInterModuleRequest, VerifyEmailOtpRequest, VerifyEmailOtpResponse, OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts, VerifyPhoneOtpRequest (+11 more)

### Community 116 - ".WriteProblemAsync"
Cohesion: 0.20
Nodes (10): IAllowAnonymous, IConfigureNamedOptions, HttpContext, HttpStatusCode, IOptions, IProblemDetailsService, IResxLocalizer, JwtBearerOptions (+2 more)

### Community 117 - "Response"
Cohesion: 0.10
Nodes (19): ReservationStatus, Active, Committed, Expired, Released, RequiresReconciliation, CancellationToken, RouteGroupBuilder (+11 more)

### Community 118 - "IModule"
Cohesion: 0.12
Nodes (14): ICoreModule, IModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority, Action (+6 more)

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
Cohesion: 0.22
Nodes (8): BaseDbContext, AuditLog, CancellationToken, DbContextOptions, DbSet, ILogger, Task, TimeProvider

### Community 128 - "EmailRateLimitingPolicy"
Cohesion: 0.25
Nodes (8): IRateLimiterPolicy, CancellationToken, Func, IOptions, OnRejectedContext, ValueTask, EmailRateLimitingPolicy, OnRejected

### Community 129 - "MassTransitInterModuleRequestClient"
Cohesion: 0.29
Nodes (5): IClientFactory, MassTransitInterModuleRequestClient, CancellationToken, IOptions, Task

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
Cohesion: 0.13
Nodes (13): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, IReadOnlyCollection, RouteGroupBuilder, Endpoint, DateTimeOffset, Response, ClientId, DeviceName (+5 more)

### Community 136 - ".AddNotificationsSignalR"
Cohesion: 0.17
Nodes (9): HubConnectionContext, IUserIdProvider, RedisOptions, IConfiguration, IConfigureOptions, IConnectionMultiplexer, IServiceCollection, IUserIdProvider (+1 more)

### Community 137 - ".WriteTooManyRequestsToResponse"
Cohesion: 0.13
Nodes (14): PartitionedRateLimiter, CancellationToken, Func, HttpContext, IConfiguration, IProblemDetailsService, IReadOnlyList, IResxLocalizer (+6 more)

### Community 138 - "ResultToResponseTransformer"
Cohesion: 0.26
Nodes (8): Common.Application.EndpointFilters, IEndpointFilter, ResultToCreatedResponseTransformer, ResultToResponseTransformer, IServiceProvider, IWebHostEnvironment, RouteHandlerBuilderExtensions, RouteHandlerBuilder

### Community 139 - "ProductTemplate"
Cohesion: 0.15
Nodes (11): IReadOnlyList, List, ProductTemplate, Brand, Color, IsActive, Model, Products (+3 more)

### Community 140 - "Endpoint"
Cohesion: 0.29
Nodes (5): IAM.Endpoints.Users.VersionNeutral, RouteGroupBuilder, Endpoint, RouteGroupBuilder, Setup

### Community 141 - "IKeycloakPermissionClient"
Cohesion: 0.28
Nodes (6): CancellationToken, IReadOnlyList, Task, IKeycloakPermissionClient, IReadOnlyList, GrantedPermission

### Community 142 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 143 - ".UseModules"
Cohesion: 0.26
Nodes (6): ModuleRegistry, Exception, IApplicationBuilder, ILogger, LoggerMessage, WebApplication

### Community 144 - "Response"
Cohesion: 0.13
Nodes (14): RouteGroupBuilder, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate, CreatedOn, Email (+6 more)

### Community 145 - "EnrichLogsWithUserInfoMiddleware"
Cohesion: 0.12
Nodes (12): IAuthenticationSchemeProvider, IMiddleware, IApplicationBuilder, HttpContext, RequestDelegate, Task, EnrichLogsWithUserInfoMiddleware, HttpContext (+4 more)

### Community 146 - "Response"
Cohesion: 0.22
Nodes (7): RouteGroupBuilder, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 147 - "IRecurringBackgroundJobs"
Cohesion: 0.13
Nodes (13): RecurringJobOptions, IRecurringBackgroundJobs, Action, Expression, Func, Task, RecurringBackgroundJobsService, Action (+5 more)

### Community 148 - "AuditLogRetentionService"
Cohesion: 0.09
Nodes (23): Common.Infrastructure.Persistence.AuditLog, IHostedService, AuditLogOptions, PerSchemaRetentionDays, PurgeBatchSize, RetentionDays, AuditLogOptionsValidator, Dictionary (+15 more)

### Community 149 - "OtpService"
Cohesion: 0.18
Nodes (6): IFusionCache, IOptions, OtpService, IConfiguration, IServiceCollection, Setup

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

### Community 154 - "CustomValidator"
Cohesion: 0.13
Nodes (21): AbstractValidator, Inventory.Endpoints.StockReservations.v1.Commit, CustomValidator, RequestBody, Request, Body, Id, RequestBody (+13 more)

### Community 155 - "DummyEmailGateway"
Cohesion: 0.38
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, DummyEmailGateway

### Community 156 - "Request"
Cohesion: 0.12
Nodes (15): IAM.Endpoints.Users.VersionNeutral.SelfRegister, Common.Domain.Extensions, Guid, Request, BirthDate, CaptchaToken, ClientId, DeviceId (+7 more)

### Community 157 - "ISearchLanguageResolver"
Cohesion: 0.23
Nodes (7): ISearchLanguageResolver, UniversalConfig, SearchLanguageResolver, UniversalConfig, IOptions, Setup, IServiceCollection

### Community 158 - "EventDispatcher"
Cohesion: 0.31
Nodes (7): EventDispatcher, ActivitySource, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task

### Community 159 - "IInventoryDbContext"
Cohesion: 0.13
Nodes (15): DbSet, IInventoryDbContext, StockLevels, StockReservations, DbContextOptions, DbSet, ILogger, TimeProvider (+7 more)

### Community 160 - "OtpServiceBase"
Cohesion: 0.21
Nodes (8): OtpCacheEntry, DateTimeOffset, CancellationToken, IFusionCache, SemaphoreSlim, Task, TimeSpan, OtpServiceBase

### Community 161 - ".BuildReservations"
Cohesion: 0.18
Nodes (8): Inventory.Endpoints.StockReservations.v1.ReserveSeries, List, RequestBody, RouteGroupBuilder, Endpoint, ICollection, Response, Ids

### Community 162 - "NotificationsHub"
Cohesion: 0.31
Nodes (6): Hub, Exception, ILogger, LoggerMessage, Task, NotificationsHub

### Community 164 - "Request"
Cohesion: 0.10
Nodes (19): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, IAM.Domain.Users, Constants, Guid, Request, ClientId, DeviceId, DeviceName (+11 more)

### Community 165 - "CurrentUser"
Cohesion: 0.15
Nodes (12): CurrentUser, Id, IdAsString, IsAuthenticated, Principal, Roles, SessionId, ClaimsPrincipal (+4 more)

### Community 166 - "IamModule"
Cohesion: 0.14
Nodes (12): Action, IApplicationBuilder, IConfiguration, IEnumerable, IServiceCollection, RateLimiterOptions, IamModule, ActivitySourceNames (+4 more)

### Community 167 - "PaginationRequest"
Cohesion: 0.15
Nodes (11): PaginationRequest, PageNumber, PageSize, Skip, Take, PaginationQueryableExtensions, CancellationToken, Expression (+3 more)

### Community 168 - "Response"
Cohesion: 0.22
Nodes (7): Products.Endpoints.ProductTemplates, RouteGroupBuilder, Setup, RouteGroupBuilder, Endpoint, Response, Id

### Community 169 - "SendSecurityAlertRequestHandler"
Cohesion: 0.27
Nodes (9): SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, CancellationToken, IOptions, RequestLocalizationOptions, Task (+1 more)

### Community 171 - "IOtpService"
Cohesion: 0.24
Nodes (8): CancellationToken, Task, TimeSpan, IOtpService, OtpVerificationOutcome, InvalidOtp, Success, TooManyAttempts

### Community 172 - "ResxLocalizationOptions"
Cohesion: 0.25
Nodes (7): ResxLocalizationOptions, DefaultCulture, SupportedCultures, ResxLocalizationOptionsValidator, ICollection, IApplicationBuilder, IOptions

### Community 173 - "v1/AddProduct/Request.cs"
Cohesion: 0.17
Nodes (12): ProductTemplateId, RequestBody, Request, Body, Id, RequestBody, Description, Name (+4 more)

### Community 174 - ".AddPushServices"
Cohesion: 0.29
Nodes (5): CancellationToken, Task, IPushGateway, IConfiguration, IServiceCollection

### Community 175 - "PaginationResponse"
Cohesion: 0.07
Nodes (25): Products.Endpoints.Stores.v1.My.AuditLog, JsonElement, AuditLogDto, DateTimeOffset, PaginationResponse, HasNext, HasPrevious, NextPageNumber (+17 more)

### Community 176 - ".FixedWindow"
Cohesion: 0.22
Nodes (7): RateLimitPartitions, HttpContext, IConnectionMultiplexer, ILoggerFactory, RateLimitPartition, HttpContext, RateLimitPartition

### Community 177 - "Request"
Cohesion: 0.18
Nodes (11): StoreId, Request, Description, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name (+3 more)

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

### Community 185 - "IdentityScheme"
Cohesion: 0.24
Nodes (8): IdentityScheme, Email, PhoneNumber, IdentitySchemeOptions, Scheme, IdentitySchemeOptionsValidator, IEndpointRouteBuilder, IOptions

### Community 186 - "Policies"
Cohesion: 0.22
Nodes (6): CreateStoreRateLimitingPolicy, RateLimiterOptions, Policies, Action, IEnumerable, RateLimiterOptions

### Community 187 - "ReverseProxyOptions"
Cohesion: 0.18
Nodes (9): ForwardedHeadersOptions, ReverseProxyOptions, ForwardLimit, IsEnabled, TrustedNetworks, ReverseProxyOptionsValidator, IReadOnlyList, IConfiguration (+1 more)

### Community 188 - "BoundedCaptureStream"
Cohesion: 0.14
Nodes (8): HttpResponse, SeekOrigin, BoundedCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 190 - "ServiceAccountTokenCache"
Cohesion: 0.14
Nodes (10): CancellationToken, DateTimeOffset, SemaphoreSlim, Task, TimeSpan, ServiceAccountTokenCache, AccessToken, ExpiresAt (+2 more)

### Community 191 - "Common.Application.ModelBinders"
Cohesion: 0.08
Nodes (22): Inventory.Endpoints.StockReservations.v1.Release, Common.Application.ModelBinders, Products.Endpoints.Stores.v1.RemoveProduct, IModelBinder, ModelBindingContext, StronglyTypedIdBinder, Task, Request (+14 more)

### Community 192 - "IssueVerificationTokenRequestHandler"
Cohesion: 0.39
Nodes (6): IssueVerificationTokenRequest, IssueVerificationTokenResponse, CancellationToken, IOptions, Task, IssueVerificationTokenRequestHandler

### Community 193 - ".SendCoreAsync"
Cohesion: 0.27
Nodes (7): IReadOnlyDictionary, IReadOnlyList, PushMessage, CancellationToken, IEnumerable, IReadOnlyList, Task

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
Cohesion: 0.10
Nodes (15): Inventory.Endpoints, Inventory.Infrastructure.Persistence, Inventory.Endpoints.StockReservations, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection (+7 more)

### Community 199 - "OpenApiOptions"
Cohesion: 0.22
Nodes (9): OpenApiOptions, ContactEmail, ContactName, Description, EnableSwagger, LicenseName, LicenseUrl, Title (+1 more)

### Community 200 - "KeyValuePair"
Cohesion: 0.14
Nodes (9): KeyValuePair, IEnumerable, CancellationToken, Task, ActivitySource, Counter, Meter, NotificationsTelemetry (+1 more)

### Community 201 - "KeycloakScopes"
Cohesion: 0.20
Nodes (9): Devices, Hangfire, KeycloakScopes, Products, ProductTemplates, Sessions, StockReservations, Stores (+1 more)

### Community 202 - "Request"
Cohesion: 0.09
Nodes (20): IAM.Endpoints.Tokens.VersionNeutral.Create, Notifications.Infrastructure.Devices.UpdateCurrentPushToken, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, Common.Domain.Devices, DeviceSessionConstants, Guid, Request, ClientId (+12 more)

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
Cohesion: 0.28
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

### Community 215 - ".CreateProductTemplateAsync"
Cohesion: 0.20
Nodes (8): ProductTemplate, CancellationToken, Task, Request, Brand, Color, Model, RequestValidator

### Community 216 - "ProductTemplateId"
Cohesion: 0.11
Nodes (15): Products.Endpoints.ProductTemplates.v1.Deactivate, Products.Endpoints.ProductTemplates.v1.Get, Products.Endpoints.ProductTemplates.v1.Activate, ProductTemplateId, DefaultIdType, ProductTemplateId, Request, Id (+7 more)

### Community 217 - "FeatureFlags"
Cohesion: 0.33
Nodes (5): Checkout, FeatureFlags, IAM, Notifications, Products

### Community 218 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 219 - "IAM.Endpoints.Common.Validations"
Cohesion: 0.08
Nodes (21): IAM.Endpoints.Otp.VersionNeutral.SendForLogin, IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, IAM.Endpoints.Common.Validations, IResxLocalizer, IRuleBuilder, IRuleBuilderOptions, CommonValidations, IResxLocalizer (+13 more)

### Community 220 - "Seeder"
Cohesion: 0.31
Nodes (5): Products.Infrastructure.Persistence.Seeding, ILogger, LoggerMessage, ProductsDbContext, Seeder

### Community 221 - "Keycloak realm as code"
Cohesion: 0.25
Nodes (7): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Secrets, Seed users (dev / test only)

### Community 222 - ".Failure"
Cohesion: 0.31
Nodes (4): Success, Func, Task, Action

### Community 223 - ".SeedProductAsync"
Cohesion: 0.22
Nodes (7): CancellationToken, Task, CancellationToken, List, ProductTemplateId, StoreId, Task

### Community 224 - "Endpoint"
Cohesion: 0.29
Nodes (5): IAM.Endpoints.Tokens.VersionNeutral, RouteGroupBuilder, Endpoint, RouteGroupBuilder, Setup

### Community 225 - "BackgroundJobsOptions"
Cohesion: 0.29
Nodes (7): BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount, BackgroundJobsOptionsValidator

### Community 226 - "GetSeedUserIdsRequest"
Cohesion: 0.39
Nodes (6): GetSeedUserIdsRequest, GetSeedUserIdsResponse, ICollection, CancellationToken, Task, GetSeedUserIdsRequestHandler

### Community 227 - ".AddServices"
Cohesion: 0.15
Nodes (11): BackgroundJobsModule, ActivitySourceNames, MeterNames, Name, StartupPriority, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder (+3 more)

### Community 228 - ".ListSessions"
Cohesion: 0.27
Nodes (9): DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, IReadOnlyList, CancellationToken, Task, CancellationToken, Task (+1 more)

### Community 229 - ".AddCommonOptions"
Cohesion: 0.20
Nodes (6): Setup, IConfiguration, IHostEnvironment, IServiceCollection, ValidationContextExtensions, ValidationContext

### Community 230 - "StockReservationId"
Cohesion: 0.12
Nodes (12): DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationReservedDomainEvent, DefaultIdType, StockReservationId, CancellationToken, RouteGroupBuilder (+4 more)

### Community 231 - "Request"
Cohesion: 0.33
Nodes (6): Products.Endpoints.Stores.v1.My.Update, Request, Address, Description, Name, RequestValidator

### Community 232 - "Common.Application.Options"
Cohesion: 0.06
Nodes (31): Notifications.Application.Otp, Notifications.Application.Sms, Notifications.Infrastructure.Devices, Notifications.Infrastructure.Email, Common.Application.Caching, Common.Infrastructure.RateLimiting, Common.Infrastructure.Localization, Notifications.Infrastructure.Sms (+23 more)

### Community 233 - "Common.Application.Validation"
Cohesion: 0.11
Nodes (17): Common.Application.Validation, DatabaseOptions, ConnectionString, DatabaseOptionsValidator, InterModuleRequestOptions, TimeoutSeconds, InterModuleRequestOptionsValidator, ModulesOptions (+9 more)

### Community 234 - "Setup"
Cohesion: 0.29
Nodes (4): IApplicationBuilder, IWebHostEnvironment, Type, Setup

### Community 235 - "AuditLogEntry"
Cohesion: 0.10
Nodes (17): AuditLogEntry, AggregateId, AggregateType, Event, EventType, Version, DefaultIdType, AuditLogEntryConfiguration (+9 more)

### Community 236 - "ProblemDetails"
Cohesion: 0.43
Nodes (6): ProblemDetails, ResxLocalizer, EndpointFilterDelegate, EndpointFilterInvocationContext, IStringLocalizer, ValueTask

### Community 237 - "RequireFeatureFilter"
Cohesion: 0.25
Nodes (6): RequireFeatureFilter, ActivitySource, Counter, Meter, RouteHandlerBuilderExtensions, RouteHandlerBuilder

### Community 238 - "InterModuleRequestHandler"
Cohesion: 0.21
Nodes (7): IInterModuleRequestHandler, CancellationToken, Task, InterModuleRequestHandler, CancellationToken, ConsumeContext, Task

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

### Community 244 - ".SavingChangesAsync"
Cohesion: 0.25
Nodes (6): ISearchLocalized, Language, CancellationToken, DbContextEventData, InterceptionResult, ValueTask

### Community 245 - "Host.Swagger"
Cohesion: 0.22
Nodes (5): Host.Swagger, IOperationFilter, OpenApiOperation, OperationFilterContext, RemoveDefaultResponseSchemaFilter

### Community 246 - "INotificationDispatcher"
Cohesion: 0.46
Nodes (4): CancellationToken, IReadOnlyList, Task, INotificationDispatcher

### Community 247 - "Common.InterModuleRequests"
Cohesion: 0.29
Nodes (4): Common.InterModuleRequests, IAssemblyReference, Setup, IServiceCollection

### Community 248 - "SendErrorBody"
Cohesion: 0.67
Nodes (3): SendErrorBody, Code, Message

### Community 249 - "IntegrationEventOutbox"
Cohesion: 0.14
Nodes (10): Inventory.Application.IntegrationEventHandlers, Common.IntegrationEvents, Products.Application.Stores.DomainEventHandlers.v1, Common.Application.EventBus, Lock, IIntegrationEventOutbox, IntegrationEventOutbox, List (+2 more)

### Community 250 - "FirebaseServiceAccountOptions"
Cohesion: 0.29
Nodes (7): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri

### Community 251 - "StoreId"
Cohesion: 0.16
Nodes (10): Products.Endpoints.Stores.v1.Deactivate, StoreId, DefaultIdType, StoreId, Request, Id, RequestValidator, Request (+2 more)

### Community 252 - ".GetAuditLogAsync"
Cohesion: 0.38
Nodes (5): DbContextExtensions, CancellationToken, DbSet, JsonSerializerOptions, Task

### Community 260 - "Response"
Cohesion: 0.15
Nodes (10): Products.Endpoints.Products, RouteGroupBuilder, Setup, RouteGroupBuilder, Response, AvailableQuantity, Description, Name (+2 more)

### Community 261 - "AuditableEntityResponse"
Cohesion: 0.29
Nodes (7): AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, DateTimeOffset

### Community 262 - ".AddCommonCaching"
Cohesion: 0.29
Nodes (5): Common.Infrastructure.Caching, Setup, IConfiguration, IConnectionMultiplexer, IServiceCollection

### Community 263 - "Response"
Cohesion: 0.29
Nodes (6): DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 264 - "V1ProductCreatedDomainEvent"
Cohesion: 0.33
Nodes (6): CancellationToken, Task, ProductCreatedIntegrationEventPublishingHandler, V1ProductCreatedDomainEventHandlers, ProductId, V1ProductCreatedDomainEvent

### Community 265 - "Request"
Cohesion: 0.33
Nodes (6): Request, Address, Description, Name, SearchTerm, RequestValidator

### Community 267 - "Request"
Cohesion: 0.40
Nodes (5): Inventory.Endpoints.StockReservations.v1.WebhookCallback, Request, ProviderReference, ReservationId, RequestValidator

### Community 269 - ".InvokeAsync"
Cohesion: 0.33
Nodes (5): IFeatureManagerSnapshot, EndpointFilterDelegate, EndpointFilterInvocationContext, IResxLocalizer, ValueTask

### Community 270 - "DefaultResponsesOperationFilter"
Cohesion: 0.70
Nodes (3): OpenApiOperation, OperationFilterContext, DefaultResponsesOperationFilter

### Community 271 - "ProblemDetailsContext"
Cohesion: 0.33
Nodes (4): ProblemDetailsContext, ProblemDetailsServiceExtensions, IProblemDetailsService, Task

### Community 272 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.08
Nodes (22): IAM.Endpoints.Users, IAM.Infrastructure.Keycloak.Representations, IAM.Endpoints.Otp, Notifications.Application.Persistence, Common.InterModuleRequests.IAM, Common.InterModuleRequests.Contracts, Notifications.Infrastructure.InterModuleRequestHandlers, IAM.Endpoints.Tokens.VersionNeutral.Revoke (+14 more)

### Community 273 - "EmailOtpDispatchOutcome"
Cohesion: 0.33
Nodes (5): EmailOtpDispatchErrors, EmailOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled

### Community 274 - ".ReserveSeriesAsync"
Cohesion: 0.22
Nodes (10): GetProductRequest, GetProductResponse, DefaultIdType, CancellationToken, IOptions, Task, TimeProvider, CancellationToken (+2 more)

### Community 275 - "Request"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForEmail, Request, CaptchaToken, Email, RequestValidator

### Community 276 - "SmsOtpDispatchOutcome"
Cohesion: 0.33
Nodes (5): OtpDispatchErrors, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled

### Community 277 - "SendResponseBody"
Cohesion: 0.50
Nodes (4): SendResponseBody, Code, Description, JobId

### Community 278 - "ProductId"
Cohesion: 0.10
Nodes (13): Products.Endpoints.Stores.v1.My.RemoveProduct, StronglyTypedIdHelper, DefaultIdType, ProductId, Request, Id, RequestValidator, Request (+5 more)

### Community 279 - "Response"
Cohesion: 0.33
Nodes (6): DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 280 - "InventoryTelemetry"
Cohesion: 0.40
Nodes (4): ActivitySource, Counter, Meter, InventoryTelemetry

### Community 281 - ".CommitStockReservationAsync"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 282 - "Setup"
Cohesion: 0.20
Nodes (5): Common.Infrastructure.Modules, Common.Endpoints.Versioning, Host.Middlewares, Host.Infrastructure, Setup

### Community 283 - ".RequireOtpTemplateForDefaultCulture"
Cohesion: 0.50
Nodes (3): Func, IConfiguration, IEnumerable

### Community 284 - "Request"
Cohesion: 0.18
Nodes (11): Inventory.Endpoints.StockReservations.v1.Reserve, DateTimeOffset, DefaultIdType, RequestBody, Request, Body, RequestBody, ProductId (+3 more)

### Community 285 - "ProductsModule.cs"
Cohesion: 0.15
Nodes (8): Products.Endpoints.Stores, Products.Endpoints.Probe, Products.Infrastructure.RateLimiting, Products.Endpoints, IAssemblyReference, RouteGroupBuilder, Setup, RateLimitingConstants

### Community 287 - "Inventory.Infrastructure.StockReservations"
Cohesion: 0.33
Nodes (3): Inventory.Infrastructure.StockReservations, IServiceCollection, Setup

### Community 288 - "SignalROptions"
Cohesion: 0.50
Nodes (4): SignalROptions, RedisConnectionString, UseRedisBackplane, SignalROptionsValidator

### Community 289 - ".AddModules"
Cohesion: 0.16
Nodes (10): LoadAll, Names, Type, Assembly, IConfiguration, IEnumerable, IReadOnlyCollection, IReadOnlyList (+2 more)

### Community 291 - ".SeedProductTemplatesAsync"
Cohesion: 0.60
Nodes (3): CancellationToken, List, Task

### Community 293 - ".SeedStoresAsync"
Cohesion: 0.60
Nodes (3): CancellationToken, List, Task

### Community 297 - "ISmsGateway"
Cohesion: 0.12
Nodes (15): CancellationToken, Task, ISmsGateway, SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, SmsMessage (+7 more)

### Community 298 - "SendPhoneOtpRequestHandler"
Cohesion: 0.50
Nodes (4): IFusionCache, IOptions, RequestLocalizationOptions, SendPhoneOtpRequestHandler

### Community 299 - "Request"
Cohesion: 0.20
Nodes (9): Products.Endpoints.Stores.v1.Create, Request, Address, Description, Name, OwnerId, RequestValidator, Response (+1 more)

## Knowledge Gaps
- **897 isolated node(s):** `Id`, `IdAsString`, `Roles`, `SessionId`, `JwtClaimNames` (+892 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 2028 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **93 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `MassTransitInterModuleRequestClient`, `DeviceRegistryReconciliationService`, `.AddCommonCaching`, `Common.Infrastructure.Persistence`, `Notifications.Application.Push`, `Common.Domain.ResultMonad`, `Endpoint`, `EmailOptions`, `EnrichLogsWithUserInfoMiddleware`, `Common.InterModuleRequests.Contracts`, `AuditLogRetentionService`, `OtpService`, `OutboxOptions`, `Setup`, `RequestResponseBodyLoggingMiddleware`, `Request`, `ISearchLanguageResolver`, `ProductsModule.cs`, `Inventory.Infrastructure.StockReservations`, `SignalROptions`, `Request`, `ObservabilityOptions`, `Request`, `KeycloakOptions`, `ResxLocalizationOptions`, `CachingOptions`, `ReCaptchaService`, `RabbitMqOptions`, `SmsOptions`, `Inventory.Domain.StockReservations`, `IdentityScheme`, `Policies`, `ReverseProxyOptions`, `RequestLoggingOptions`, `CorsOptions`, `InventoryOptions`, `OpenApiOptions`, `Request`, `Notifications.Application.Hubs`, `Common.Application.BackgroundJobs`, `Common.Application.FeatureManagement`, `PushOptions`, `Program.cs`, `Common.Domain.StronglyTypedIds`, `OtpOptions`, `Endpoint`, `BackgroundJobsOptions`, `ResiliencyOptions`, `.AddCommonOptions`, `Common.Application.Validation`, `OutboxModule.cs`, `.ReleaseStockReservationAsync`, `FixedWindow`, `Host.Swagger`, `IModule`, `IntegrationEventOutbox`, `FullTextSearchOptions`, `.AddCustomHealthChecks`?**
  _High betweenness centrality (0.276) - this node is a cross-community bridge._
- **Why does `Result` connect `Result` to `Error`, `ProductTemplate`, `StringExtensions`, `ICaptchaService`, `Response`, `EmailOtpDispatchOutcome`, `.ReserveSeriesAsync`, `DomainEvent`, `SmsOtpDispatchOutcome`, `.AddBrevo`, `ICurrentUser`, `.CommitStockReservationAsync`, `NetGsmSmsGateway`, `DummyEmailGateway`, `Func`, `Product`, `StockReservation`, `.AssignBasicRoleOrRollbackAsync`, `ISmsGateway`, `IResult`, `.RegisterAsync`, `.AddPushServices`, `PaginationResponse`, `DummyPushGateway`, `ReCaptchaService`, `BrevoEmailGateway`, `.SearchProductTemplatesAsync`, `.SendCoreAsync`, `.SendCoreAsync`, `Response`, `Response`, `IProductsDbContext`, `.SearchStoresAsync`, `ResultTelemetryExtensions`, `.CreateProductTemplateAsync`, `.GetProductAsync`, `Response`, `.Failure`, `.SearchMyProductsAsync`, `.SearchStoreProductsAsync`, `.AddNetGsm`, `.ListSessions`, `StockReservationId`, `.ReleaseStockReservationAsync`, `.VerifyOtp`, `Response`, `.HandleWarehouseWebhookAsync`, `.GetAuditLogAsync`, `.TapWhenFeatureEnabledAsync`?**
  _High betweenness centrality (0.169) - this node is a cross-community bridge._
- **Why does `ApplicationUserId` connect `ApplicationUserId` to `NotificationPayload`, `DeviceRegistryReconciliationService`, `AuditableEntityResponse`, `Result`, `Response`, `KeycloakPermissionAuthorizationHandler`, `Response`, `IntegrationEvent`, `ProductId`, `DeviceRegistration`, `ICurrentUser`, `KeycloakTokenClient`, `For`, `CurrentUser`, `.SeedStoresAsync`, `.AssignBasicRoleOrRollbackAsync`, `SendSecurityAlertRequestHandler`, `Request`, `.RegisterAsync`, `PaginationResponse`, `AuditableEntity`, `Request`, `Response`, `Common.Application.ModelBinders`, `IStronglyTypedId`, `IProductsDbContext`, `.SearchStoresAsync`, `Notifications.Application.Hubs`, `.Configure`, `Store`, `Response`, `Common.Domain.StronglyTypedIds`, `GetSeedUserIdsRequest`, `.ListSessions`, `V1StoreCreatedDomainEvent`, `Response`, `INotificationDispatcher`?**
  _High betweenness centrality (0.070) - this node is a cross-community bridge._
- **What connects `Id`, `IdAsString`, `Roles` to the rest of the system?**
  _897 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `DeviceRegistryReconciliationService` be split into smaller, more focused modules?**
  _Cohesion score 0.07317073170731707 - nodes in this community are weakly interconnected._
- **Should `RedisFixedWindowRateLimiter` be split into smaller, more focused modules?**
  _Cohesion score 0.12554112554112554 - nodes in this community are weakly interconnected._
- **Should `Common.Infrastructure.Persistence` be split into smaller, more focused modules?**
  _Cohesion score 0.09523809523809523 - nodes in this community are weakly interconnected._