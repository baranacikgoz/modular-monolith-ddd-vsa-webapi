# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-09-24)

## Corpus Check
- 572 files · ~89,385 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 4907 nodes · 9672 edges · 389 communities (296 shown, 93 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 256 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `046d09c3`
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
- .AddModuleDbContext
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
- Inventory.Domain.StockReservations.DomainEvents.v1
- Common.Domain.StronglyTypedIds
- LogProductCatalogChangeHandler
- BoundedRequestCaptureStream
- DeviceRegistration
- DummyEmailGateway
- .RevokeSession
- Common.Infrastructure.Extensions
- IEvent
- RequestResponseBodyLoggingMiddleware
- AuditLogEntry
- Result
- JobRow
- NotificationsModule
- .SingleAsResult
- Inventory.Domain.StockReservations
- KeycloakPermissionClient
- OutboxMessage
- CustomRateLimitingOptions
- .SendOtp
- .RefreshToken
- ObservabilityOptions
- ConfigureSwaggerOptions.cs
- Request
- .SaveWithOutboxAsync
- CreateStoreRateLimitingPolicy
- KeycloakOptions
- VerifyPhoneOtpResponse
- PaginationQueryableExtensions
- Outbox Misuse Check
- Common.InterModuleRequests.Contracts
- Add Integration Event Command
- Response
- Common.Domain.Events
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
- IDbContext
- RequestLoggingOptions
- .SendCoreAsync
- BackgroundJobsService
- OutboxModule
- .AddNotificationsSignalR
- ValueObject
- .HandleAsync
- StockReservation
- FullTextSearchOptions
- Response
- .WriteAsync
- AsNoTracking Coverage Check
- IntegrationEventOutbox
- AuditableEntityResponse
- .DeactivateProductTemplateAsync
- CheckRegistrationRateLimitingPolicy
- DomainEvent
- IAuditableEntity
- .SearchStoresAsync
- Split-Deployment PoC
- BaseDbContext
- .Configure
- CaptchaOptions
- Store
- PushOptions
- Configuration-Driven Module Registration
- Setup
- .GetProductAsync
- Seeder
- Infrastructure/Setup.cs
- .MapEndpoint
- .Configure
- .SearchMyProductsAsync
- DatabaseSeederOrchestrator
- StockReservationExpirySweepService
- .SearchStoreProductsAsync
- AuditLogRetentionJobRegistrar
- OutboxDbContext
- ResiliencyOptions
- HttpContextTargetingContextAccessor
- InboxCleanupJob
- Common.Application.Pagination
- Response
- KeycloakTokenClient.cs
- HttpWarehouseGateway
- StockLevel
- .IsRegisteredAsync
- KeycloakPermissionPolicyProvider
- InventoryOptions
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
- ProductTemplates/v1/Search/Request.cs
- EmailRateLimitingPolicy
- .SendAsync
- docker-compose.yml (Base Stack)
- .AddInfrastructure
- Request
- Consumer Idempotency (IntegrationEventHandlerBase)
- SendRequestBody
- Response
- .GetMeAsync
- .WriteTooManyRequestsToResponse
- ResultToResponseTransformer.cs
- ProductTemplate
- Notifications.Application.Hubs
- Response
- TokenRefreshRateLimitingPolicy
- Setup
- Response
- Request
- .RegisterAsync
- IRecurringBackgroundJobs
- .RegisterAsync
- .AddPersistence
- StockReservationExpirySweepJobRegistrar
- OutboxOptions
- .EnsureNoMigrationsPending
- .AddCustomSwagger
- Response
- ISmsGateway
- Products.Domain.Products.DomainEvents.v1
- CreateStockLevelOnProductCreatedHandler
- OtpOptions
- IInventoryDbContext
- OtpServiceBase
- .ReserveSeriesAsync
- IEmailGateway
- For
- NotificationsDbContext
- ICurrentUser
- IamModule
- PaginationRequest
- ProductsModule.cs
- SendSecurityAlertRequestHandler
- Notifications.Application/IAssemblyReference.cs
- ICaptchaService
- ResxLocalizationOptions
- IAM.Application.Captcha.Services
- system_diagnostics
- PaginationResponse
- .FixedWindow
- RequireFeatureFilter
- PushMessage
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
- JobClaimExtensions
- ServiceAccountTokenCache
- .AddProductToMyStoreAsync
- ReCaptchaResponse
- Request
- CorsOptions
- WarehouseGatewayProvider
- TokenCreateRateLimitingPolicy
- .ReserveStockAsync
- InventoryModule
- OpenApiOptions
- NotificationsTelemetry
- KeycloakScopes
- Request
- SmsRateLimitingPolicy
- Configuration-Driven Module Loading
- CachedCaptchaService
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- ConfigureSwaggerOptions
- Request
- Versioning/Setup.cs
- BackgroundJobsTelemetry
- Common.Application.Options
- IProductsDbContext
- ReservationStatus
- FeatureFlags
- StoreId
- IBackgroundJobs
- .SaveChangesAsync
- Keycloak realm as code
- Key decisions
- Request
- .MapEndpoint
- IntegrationEventHandlerBase
- NetGsmSmsGateway
- RegisterRateLimitingPolicy
- ProjectionUpsertOutcome
- .AddCommonOptions
- InterModuleRequestHandler
- .UseModule
- DeviceRegistryReconciliationService
- .HasPatternIndex
- Setup
- V1ProductAddedToStoreDomainEvent
- IOtpService
- ProblemDetailsExtensions.cs
- SecurityHeadersMiddleware
- IAMModule.cs
- InterModuleRequestOptions
- Request
- StronglyTypedIdSchemaFilter.cs
- CustomValidator
- KeyedResilienceProfile
- SendForEmail/Request.cs
- SeedingCompletionTracker
- KeycloakPermissionAuthorizationHandler.cs
- SendErrorBody
- .AddProductAsync
- ProductId
- Request
- .TryWriteAsync
- Products.Domain/IAssemblyReference.cs
- IAM.Application/IAssemblyReference.cs
- Products.Infrastructure/IAssemblyReference.cs
- Products.Application/IAssemblyReference.cs
- Common.Infrastructure/IAssemblyReference.cs
- Notifications.Domain/IAssemblyReference.cs
- IAM.Infrastructure/IAssemblyReference.cs
- RequestBodyLimitMetadata
- FirebasePushGateway.cs
- .SendWithPipelineAsync
- Response
- V1StoreCreatedDomainEvent
- BackgroundJobsModule
- KeycloakPermissionRequirement
- AuditLogOptions
- .ActivateProductTemplateAsync
- .DeactivateStoreAsync
- DefaultResponsesOperationFilter
- .UpsertIfNewerAsync
- .AddResilientHttpClient
- .AddNetGsm
- DeviceRegistryReconcileJobRegistrar
- .GetAuditLogAsync
- .From
- SendResponseBody
- EventDispatcher
- Response
- .UpdateMyStoreAsync
- .TryReadFromJsonAsync
- Infrastructure/StringExtensions.cs
- .UpdateStoreAsync
- Request
- RequestBodyLimitFilter.cs
- microsoft_extensions_options
- Setup
- DummySmsGateway
- .SetRetryAfterHeader
- IInterModuleRequest
- GetActiveSessionIdsRequest
- .AddServices
- IdentitySchemeOptions
- .SeedProductAsync
- EnrichLogsWithUserInfoMiddleware
- StringExtensions
- IOutboxMessage
- Request
- AuditableEntity
- ResultTelemetryExtensions
- SwaggerDefaultValues
- KeyedResiliencePipelines
- BackgroundJobsOptions
- Request
- .EmailVerificationTokenValidation
- IAutoMigrateMarker.cs
- JwtClaimNames.cs
- .AddBrevo
- KeycloakRoles.cs
- .RemoveProductAsync
- StronglyTypedIdBinder.cs
- JobStatus
- .AddServices
- Request
- .UpdateProductAsync
- Request
- Request
- CurrentUser.cs
- Request
- .InvokeAsync
- system_runtime_compilerservices
- V1StockReservationCommitConflictDetectedDomainEvent
- .PhoneNumberValidation
- V1StockReservationExpiredDomainEvent
- .AddDeviceRegistryReconciliation
- ValidationContextExtensions
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
- `Add search to a new entity _(Build checklist)_` --references--> `ISearchLocalized`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Domain/Entities/ISearchLocalized.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Local Infrastructure Stack** — docker_compose_postgres, docker_compose_rabbitmq, docker_compose_redis, docker_compose_aspire_dashboard [EXTRACTED 1.00]

## Communities (389 total, 93 thin omitted)

### Community 0 - "IStronglyTypedId"
Cohesion: 0.06
Nodes (32): JsonConverter, StrictDateTimeOffsetJsonConverter, DateTimeOffset, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, StronglyTypedIdListReadOnlyJsonConverter (+24 more)

### Community 1 - "NotificationPayload"
Cohesion: 0.23
Nodes (12): IHubContext, CancellationToken, IReadOnlyList, Task, INotificationDispatcher, Task, INotificationsClient, NotificationPayload (+4 more)

### Community 2 - "OutboxProcessor"
Cohesion: 0.16
Nodes (16): IPublishEndpoint, PublishOutcome, CancellationToken, Exception, ILogger, IOptions, IServiceScopeFactory, LoggerMessage (+8 more)

### Community 3 - "microsoft_entityframeworkcore"
Cohesion: 0.08
Nodes (26): Common.Infrastructure.Persistence.Inbox, Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Common.Application.Persistence, Inventory.Infrastructure.Persistence.EntityConfigurations, Common.Application.Jobs, Notifications.Domain.Devices, Notifications.Infrastructure.Persistence (+18 more)

### Community 5 - "KeycloakAdminClient"
Cohesion: 0.16
Nodes (17): KeycloakUser, CancellationToken, Error, Func, HttpClient, HttpRequestMessage, HttpResponseMessage, IFusionCache (+9 more)

### Community 6 - "RedisFixedWindowRateLimiter"
Cohesion: 0.13
Nodes (16): RateLimiter, RateLimiterStatistics, FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration, CancellationToken (+8 more)

### Community 7 - ".AddModuleDbContext"
Cohesion: 0.12
Nodes (14): SaveChangesInterceptor, ApplyAuditingInterceptor, TimeProvider, ApplySearchLanguageInterceptor, CancellationToken, DbContextEventData, InterceptionResult, ValueTask (+6 more)

### Community 8 - "FirebasePushGateway"
Cohesion: 0.16
Nodes (12): FirebaseApp, FirebaseMessaging, IDisposable, CancellationToken, Exception, IEnumerable, ILogger, IReadOnlyList (+4 more)

### Community 9 - ".RequireOtpTemplateForDefaultCulture"
Cohesion: 0.40
Nodes (4): Func, IConfiguration, IEnumerable, OtpTemplateConfiguration

### Community 10 - "Error"
Cohesion: 0.08
Nodes (17): StringLocalizerExtensions, IStringLocalizer, Error, Key, ParameterName, StatusCode, SubErrors, Value (+9 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.09
Nodes (34): Products.Endpoints.Stores.v1.My.Update, IAM.Endpoints.Captcha.VersionNeutral, Products.Endpoints.Stores.v1.Update, Products.Endpoints.Stores.v1.Deactivate, Products.Endpoints.Products.v1.My.Update, Products.Endpoints.ProductTemplates.v1.Deactivate, Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions (+26 more)

### Community 12 - "ApplicationUserId"
Cohesion: 0.09
Nodes (26): ApplicationUserId, IsEmpty, Value, DefaultIdType, CancellationToken, IReadOnlyList, Task, IKeycloakAdminClient (+18 more)

### Community 13 - "ISearchLanguageResolver"
Cohesion: 0.21
Nodes (8): Configuration, ISearchLanguageResolver, UniversalConfig, SearchLanguageResolver, UniversalConfig, IOptions, Setup, IServiceCollection

### Community 14 - "UserRepresentation"
Cohesion: 0.07
Nodes (29): Dictionary, List, CredentialRepresentation, Temporary, Type, Value, ErrorRepresentation, Error (+21 more)

### Community 15 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.15
Nodes (14): AuthorizationHandler, AuthorizationHandlerContext, CancellationToken, ClaimsPrincipal, HttpContext, IFusionCache, IHttpContextAccessor, IOptions (+6 more)

### Community 16 - "EmailOptions"
Cohesion: 0.09
Nodes (24): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+16 more)

### Community 17 - "Product"
Cohesion: 0.12
Nodes (15): ProductId, V1ProductNameUpdatedDomainEvent, ProductTemplate, ProductTemplateId, Store, StoreId, Product, Description (+7 more)

### Community 18 - "Inventory.Domain.StockReservations.DomainEvents.v1"
Cohesion: 0.08
Nodes (19): Inventory.Domain.StockReservations.DomainEvents.v1, DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationCommittedDomainEvent, DateTimeOffset, StockReservationId, V1StockReservationReleaseAttemptAbandonedDomainEvent (+11 more)

### Community 19 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.11
Nodes (12): IAM.Endpoints.Users.VersionNeutral.Search, Common.Domain.StronglyTypedIds, IAM.Endpoints.Users.VersionNeutral.Get, Common.Application.JsonConverters, IAM.Endpoints.Users.VersionNeutral.Me.Get, Common.Domain.Aggregates, Inventory.Domain.StockLevels.DomainEvents.v1, microsoft_entityframeworkcore_storage_valueconversion (+4 more)

### Community 20 - "LogProductCatalogChangeHandler"
Cohesion: 0.24
Nodes (8): CancellationToken, DefaultIdType, IFusionCache, ILogger, IOptions, LoggerMessage, Task, LogProductCatalogChangeHandler

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.13
Nodes (8): ReadOnlySpan, BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position, Stream

### Community 22 - "DeviceRegistration"
Cohesion: 0.13
Nodes (14): DateTimeOffset, DefaultIdType, Guid, DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive (+6 more)

### Community 23 - "DummyEmailGateway"
Cohesion: 0.38
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, DummyEmailGateway

### Community 24 - ".RevokeSession"
Cohesion: 0.09
Nodes (18): DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse, IReadOnlyList, CancellationToken, RouteGroupBuilder, Task, Endpoint, CancellationToken (+10 more)

### Community 25 - "Common.Infrastructure.Extensions"
Cohesion: 0.10
Nodes (24): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, IAM.Endpoints.Users, IAM.Endpoints.Users.VersionNeutral.SelfRegister, Common.Infrastructure.RateLimiting, Products.Infrastructure.RateLimiting, IAM.Endpoints.Otp, Common.Infrastructure.Extensions, IAM.Endpoints.Users.VersionNeutral.SelfRegisterByEmail (+16 more)

### Community 26 - "IEvent"
Cohesion: 0.11
Nodes (14): DomainEventHandlerBase, CancellationToken, Task, IEventHandler, CancellationToken, Task, IEventHandlerWrapper, CancellationToken (+6 more)

### Community 27 - "RequestResponseBodyLoggingMiddleware"
Cohesion: 0.16
Nodes (11): IDiagnosticContext, SensitivePathRule, Methods, Path, IList, HttpContext, IList, IOptions (+3 more)

### Community 28 - "AuditLogEntry"
Cohesion: 0.11
Nodes (16): AuditLogEntry, AggregateId, AggregateType, Event, EventType, Version, DefaultIdType, ModelBuilder (+8 more)

### Community 29 - "Result"
Cohesion: 0.13
Nodes (14): Result, Error, IsFailure, Success, Value, Func, Task, AsyncExtensions (+6 more)

### Community 30 - "JobRow"
Cohesion: 0.19
Nodes (9): JobRow, FailureKey, FinishedOn, Progress, QueuedOn, RequestedBy, StartedOn, Status (+1 more)

### Community 31 - "NotificationsModule"
Cohesion: 0.15
Nodes (10): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, NotificationsModule, ActivitySourceNames, MeterNames (+2 more)

### Community 32 - ".SingleAsResult"
Cohesion: 0.18
Nodes (10): CollectionExtensions, Func, ICollection, IEnumerable, PersistenceQueryableExtensions, CancellationToken, Expression, Func (+2 more)

### Community 33 - "Inventory.Domain.StockReservations"
Cohesion: 0.13
Nodes (12): Inventory.Endpoints.StockReservations.v1.Commit, Inventory.Endpoints.StockReservations.v1.ReserveSeries, Inventory.Infrastructure.InterModuleRequestHandlers, Inventory.Application.Persistence, Inventory.Domain.StockReservations, Inventory.Endpoints.StockReservations.v1.Release, Inventory.Domain.StockReservations.Errors, Inventory.Endpoints.StockReservations.v1.Get (+4 more)

### Community 34 - "KeycloakPermissionClient"
Cohesion: 0.13
Nodes (20): CancellationToken, Dictionary, HttpClient, HttpResponseMessage, ILogger, IOptions, IReadOnlyList, List (+12 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.10
Nodes (18): IEntityTypeConfiguration, OutboxMessage, CreatedOn, Event, FailedOn, Id, IsProcessed, NextRetryAt (+10 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.08
Nodes (23): CheckRegistrationRateLimitingPolicy, EmailRateLimitingPolicy, OtpVerifyRateLimitingPolicy, RegisterRateLimitingPolicy, SmsRateLimitingPolicy, CustomRateLimitingOptions, CheckRegistration, CreateStore (+15 more)

### Community 37 - ".SendOtp"
Cohesion: 0.06
Nodes (31): OtpDispatchErrors, SendPhoneOtpRequest, SendPhoneOtpResponse, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, CancellationToken (+23 more)

### Community 38 - ".RefreshToken"
Cohesion: 0.12
Nodes (14): IAM.Endpoints.Tokens.VersionNeutral.Refresh, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RefreshToken, RequestValidator (+6 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.07
Nodes (25): IHostBuilder, LoggerConfiguration, LoggerMinimumLevelConfiguration, ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics (+17 more)

### Community 40 - "ConfigureSwaggerOptions.cs"
Cohesion: 0.31
Nodes (6): asp_versioning_apiexplorer, Host.Swagger, microsoft_aspnetcore_mvc_apiexplorer, microsoft_openapi, swashbuckle_aspnetcore_swaggergen, system_text_json_nodes

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

### Community 45 - "VerifyPhoneOtpResponse"
Cohesion: 0.24
Nodes (9): OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, CancellationToken, Task (+1 more)

### Community 46 - "PaginationQueryableExtensions"
Cohesion: 0.08
Nodes (27): BinaryExpression, ExpressionVisitor, KeyedRow, MemberExpression, MethodInfo, ParameterExpression, Payload, PaginationCursor (+19 more)

### Community 48 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.17
Nodes (11): Notifications.Application.Otp, IAM.Endpoints.Otp.VersionNeutral.SendForLogin, Common.Application.Caching, Common.Application.FeatureManagement, Notifications.Application.Persistence, Notifications.Infrastructure.Telemetry, Common.InterModuleRequests.Contracts, Notifications.Infrastructure.InterModuleRequestHandlers (+3 more)

### Community 50 - "Response"
Cohesion: 0.12
Nodes (16): CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate (+8 more)

### Community 51 - "Common.Domain.Events"
Cohesion: 0.10
Nodes (11): Products.Infrastructure.InterModuleRequestHandlers, Products.Endpoints.Stores.v1.My.AddProduct, Common.Domain.Events, Products.Endpoints.Stores.v1.My.Create, Products.Domain.Products, Common.Application.Persistence.Outbox, Products.Domain.Stores, Products.Domain.ProductTemplates (+3 more)

### Community 52 - "ReCaptchaService"
Cohesion: 0.21
Nodes (10): FormUrlEncodedContent, ReCaptchaResponse, CancellationToken, Exception, HttpClient, ILogger, IOptions, LoggerMessage (+2 more)

### Community 53 - "Request"
Cohesion: 0.15
Nodes (13): Guid, Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FirstName (+5 more)

### Community 54 - "SendEmailOtpRequestHandler"
Cohesion: 0.16
Nodes (13): EmailOtpDispatchErrors, EmailOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, SendEmailOtpRequest, SendEmailOtpResponse, CancellationToken (+5 more)

### Community 55 - "BrevoEmailGateway"
Cohesion: 0.23
Nodes (10): Exception, HttpClient, HttpStatusCode, ILogger, IOptions, JsonSerializerOptions, LoggerMessage, BrevoEmailGateway (+2 more)

### Community 56 - "ProjectionReconciliationJob"
Cohesion: 0.18
Nodes (14): Items, Next, ProjectionReconciliationOptions, MaxPages, PageSize, ProjectionReconciliationOptionsValidator, ProjectionReconciliationJob, CancellationToken (+6 more)

### Community 58 - "OutboxMetricsJob"
Cohesion: 0.13
Nodes (14): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxMetricsJob (+6 more)

### Community 59 - ".SearchProductTemplatesAsync"
Cohesion: 0.15
Nodes (11): LikePattern, CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response (+3 more)

### Community 61 - "KeycloakTokenClient"
Cohesion: 0.11
Nodes (24): JsonWebTokenHandler, CancellationToken, Task, IKeycloakTokenClient, DateTimeOffset, KeycloakTokens, CancellationToken, Dictionary (+16 more)

### Community 62 - "IDbContext"
Cohesion: 0.09
Nodes (21): DatabaseFacade, Deleted, EntityEntry, JobHousekeepingJob, CancellationToken, ILogger, IOptions, LoggerMessage (+13 more)

### Community 63 - "RequestLoggingOptions"
Cohesion: 0.13
Nodes (16): IPostConfigureOptions, RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes (+8 more)

### Community 64 - ".SendCoreAsync"
Cohesion: 0.21
Nodes (8): SendErrorBody, CancellationToken, HttpResponseMessage, SendRequestBody, Task, SendContact, Email, Name

### Community 65 - "BackgroundJobsService"
Cohesion: 0.23
Nodes (8): IBackgroundJobClientV2, BackgroundJobsService, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 66 - "OutboxModule"
Cohesion: 0.14
Nodes (16): Action, Exception, IApplicationBuilder, IEndpointRouteBuilder, IEnumerable, IHostApplicationLifetime, ILogger, ILoggerFactory (+8 more)

### Community 67 - ".AddNotificationsSignalR"
Cohesion: 0.15
Nodes (12): RedisOptions, SignalROptions, RedisConnectionString, RequireRedisBackplaneInProduction, UseRedisBackplane, SignalROptionsValidator, IConfiguration, IConfigureOptions (+4 more)

### Community 68 - "ValueObject"
Cohesion: 0.25
Nodes (4): Common.Domain, IComparable, ValueObject, IEnumerable

### Community 69 - ".HandleAsync"
Cohesion: 0.29
Nodes (5): CancellationToken, IResult, RouteGroupBuilder, Task, Endpoint

### Community 70 - "StockReservation"
Cohesion: 0.15
Nodes (12): DateTimeOffset, DefaultIdType, StockReservation, LastReleaseAttemptReference, ProductId, ProviderReference, Quantity, ReservationDeadline (+4 more)

### Community 71 - "FullTextSearchOptions"
Cohesion: 0.09
Nodes (24): Add a new language/culture, Add search to a new entity _(Build checklist)_, Change the vector (weights, fields, or config), Extending and maintaining, Full-Text Search, Gotchas, How it works, Non-goals (+16 more)

### Community 72 - "Response"
Cohesion: 0.14
Nodes (12): Products.Endpoints.ProductTemplates.v1.Get, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, Id, RequestValidator (+4 more)

### Community 73 - ".WriteAsync"
Cohesion: 0.33
Nodes (5): Memory, ReadOnlyMemory, CancellationToken, Task, ValueTask

### Community 75 - "IntegrationEventOutbox"
Cohesion: 0.08
Nodes (23): Common.IntegrationEvents, IIntegrationEventOutbox, IntegrationEventOutbox, HasPending, IReadOnlyList, List, Lock, Setup (+15 more)

### Community 76 - "AuditableEntityResponse"
Cohesion: 0.11
Nodes (17): AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, DateTimeOffset, CancellationToken (+9 more)

### Community 77 - ".DeactivateProductTemplateAsync"
Cohesion: 0.22
Nodes (7): CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, Id, RequestValidator

### Community 78 - "CheckRegistrationRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, CheckRegistrationRateLimitingPolicy (+1 more)

### Community 79 - "DomainEvent"
Cohesion: 0.07
Nodes (27): AggregateRoot, Events, Id, Version, IReadOnlyCollection, List, IAggregateRoot, Events (+19 more)

### Community 80 - "IAuditableEntity"
Cohesion: 0.18
Nodes (10): IAuditableEntity, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, DateTimeOffset, CancellationToken, DbContextEventData (+2 more)

### Community 81 - ".SearchStoresAsync"
Cohesion: 0.15
Nodes (12): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Address (+4 more)

### Community 82 - "Split-Deployment PoC"
Cohesion: 0.18
Nodes (9): Concurrent safety, Cross-process call path, Files added by this PoC, How it works, How to run, Split-Deployment PoC, What this proves, README (Boilerplate Overview) (+1 more)

### Community 83 - "BaseDbContext"
Cohesion: 0.11
Nodes (14): BaseDbContext, AuditLog, CancellationToken, DateTimeOffset, DbContextOptions, DbSet, ILogger, ModelConfigurationBuilder (+6 more)

### Community 84 - ".Configure"
Cohesion: 0.13
Nodes (16): AuditableEntityConfiguration, EntityTypeBuilder, JobRowConfiguration, EntityTypeBuilder, StronglyTypedIdValueConverter, DefaultIdType, EntityTypeBuilder, DeviceRegistrationConfiguration (+8 more)

### Community 85 - "CaptchaOptions"
Cohesion: 0.15
Nodes (13): CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey, Provider, ScoreThreshold, SecretKey (+5 more)

### Community 86 - "Store"
Cohesion: 0.08
Nodes (22): File map _(Build)_, ISearchLocalized, Language, StoreId, V1StoreAddressUpdatedDomainEvent, StoreId, V1StoreDescriptionUpdatedDomainEvent, StoreId (+14 more)

### Community 87 - "PushOptions"
Cohesion: 0.12
Nodes (20): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri, PushOptions (+12 more)

### Community 89 - "Setup"
Cohesion: 0.33
Nodes (4): ConfigurationManager, Host.Configurations, Setup, WebApplicationBuilder

### Community 90 - ".GetProductAsync"
Cohesion: 0.10
Nodes (22): GetStockLevelRequest, GetStockLevelResponse, DefaultIdType, CancellationToken, Task, GetStockLevelRequestHandler, RouteGroupBuilder, Setup (+14 more)

### Community 91 - "Seeder"
Cohesion: 0.11
Nodes (18): Products.Infrastructure.Persistence.Seeding, Common.InterModuleRequests.IAM, IAM.Infrastructure.InterModuleRequestHandlers, GetSeedUserIdsRequest, GetSeedUserIdsResponse, ICollection, CancellationToken, Task (+10 more)

### Community 92 - "Infrastructure/Setup.cs"
Cohesion: 0.06
Nodes (26): Common.InterModuleRequests, Host, Common.Infrastructure.Localization, Host.Middlewares, Common.Infrastructure.FeatureManagement, Common.Infrastructure.Caching, Host.Infrastructure, elastic_serilog_sinks (+18 more)

### Community 93 - ".MapEndpoint"
Cohesion: 0.22
Nodes (6): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, RouteGroupBuilder, Response, ClientKey, RouteGroupBuilder, Setup

### Community 94 - ".Configure"
Cohesion: 0.29
Nodes (5): HttpRequestException, ResiliencePipelineBuilder, HttpResponseMessage, ResiliencePipeline, TimeoutRejectedException

### Community 95 - ".SearchMyProductsAsync"
Cohesion: 0.10
Nodes (19): Products.Endpoints.Products.v1.My.Search, CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Request (+11 more)

### Community 96 - "DatabaseSeederOrchestrator"
Cohesion: 0.27
Nodes (9): BackgroundService, DatabaseSeederOrchestrator, CancellationToken, Exception, IHostApplicationLifetime, ILogger, IServiceScopeFactory, LoggerMessage (+1 more)

### Community 97 - "StockReservationExpirySweepService"
Cohesion: 0.18
Nodes (11): IServiceCollection, Setup, CancellationToken, Exception, Guid, ILogger, IOptions, LoggerMessage (+3 more)

### Community 98 - ".SearchStoreProductsAsync"
Cohesion: 0.09
Nodes (21): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, StoreId, Request (+13 more)

### Community 99 - "AuditLogRetentionJobRegistrar"
Cohesion: 0.22
Nodes (9): IHostedService, AuditLogRetentionJobRegistrar, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task, Setup (+1 more)

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

### Community 104 - "Common.Application.Pagination"
Cohesion: 0.11
Nodes (12): Common.Application.Search, Products.Endpoints.Stores.v1.Search, Products.Endpoints.Products.v1.My.Get, Products.Endpoints.ProductTemplates.v1.Search, Products.Endpoints.Products.v1.Search, Common.Application.DTOs, Products.Endpoints.Stores.v1.Get, Products.Endpoints.Products.v1.Get (+4 more)

### Community 105 - "Response"
Cohesion: 0.20
Nodes (8): RouteGroupBuilder, Endpoint, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 106 - "KeycloakTokenClient.cs"
Cohesion: 0.13
Nodes (10): IAM.Infrastructure.Keycloak.Representations, IAM.Domain.Errors, IAM.Infrastructure.Keycloak, microsoft_identitymodel_jsonwebtokens, VerifyEmailOtpResponseExtensions, VerifyPhoneOtpResponseExtensions, OAuthErrors, UserAttributes (+2 more)

### Community 107 - "HttpWarehouseGateway"
Cohesion: 0.31
Nodes (8): ReleaseResponseBody, CancellationToken, HttpClient, HttpResponseMessage, Task, HttpWarehouseGateway, ReleaseRequestBody, ReleaseResponseBody

### Community 108 - "StockLevel"
Cohesion: 0.12
Nodes (11): StronglyTypedIdHelper, DefaultIdType, StockLevelId, V1StockLevelCreatedDomainEvent, DefaultIdType, StockLevel, ProductId, QuantityOnHand (+3 more)

### Community 109 - ".IsRegisteredAsync"
Cohesion: 0.17
Nodes (10): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, PhoneNumber, RequestValidator (+2 more)

### Community 110 - "KeycloakPermissionPolicyProvider"
Cohesion: 0.27
Nodes (8): AuthorizationOptions, AuthorizationPolicy, DefaultAuthorizationPolicyProvider, IAuthorizationPolicyProvider, ConcurrentDictionary, IOptions, Task, KeycloakPermissionPolicyProvider

### Community 111 - "InventoryOptions"
Cohesion: 0.07
Nodes (25): Inventory.Application.Gateway, InventoryOptions, MaxOccurrencesPerSeries, SafetyBufferQuantity, StockReservationExpirySweepBatchSize, StockReservationExpirySweepCron, WarehouseGatewayBaseUrl, WarehouseGatewayProvider (+17 more)

### Community 112 - "OutboxCleanupJob"
Cohesion: 0.27
Nodes (8): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxCleanupJob

### Community 113 - "ProductsDbContext"
Cohesion: 0.15
Nodes (12): DbContextOptions, DbSet, ILogger, ProductTemplate, Store, TimeProvider, ProductsDbContext, Products (+4 more)

### Community 114 - "Response"
Cohesion: 0.12
Nodes (16): RouteGroupBuilder, Endpoint, DateOnly, DateTimeOffset, IReadOnlyCollection, Response, BirthDate, CreatedOn (+8 more)

### Community 115 - "AuditLogRetentionService"
Cohesion: 0.33
Nodes (7): AuditLogRetentionService, CancellationToken, ILogger, IOptions, LoggerMessage, NpgsqlDataSource, Task

### Community 116 - ".AddAuthInfrastructure"
Cohesion: 0.12
Nodes (16): IAllowAnonymous, IAuthorizationHandler, IConfigureNamedOptions, HttpContext, HttpStatusCode, IOptions, IProblemDetailsService, IResxLocalizer (+8 more)

### Community 117 - "Response"
Cohesion: 0.12
Nodes (16): CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, Id, RequestValidator, DateTimeOffset (+8 more)

### Community 118 - "IModule"
Cohesion: 0.09
Nodes (20): OpenTelemetryBuilder, ResourceBuilder, IModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority (+12 more)

### Community 119 - "ProductsModule"
Cohesion: 0.12
Nodes (13): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, ProductsModule (+5 more)

### Community 120 - ".HandleWarehouseWebhookAsync"
Cohesion: 0.12
Nodes (17): Inventory.Endpoints.StockReservations.v1.WebhookCallback, IHeaderDictionary, IValidator, HmacSignatureVerifier, ReadOnlySpan, CancellationToken, HttpContext, IOptions (+9 more)

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

### Community 125 - ".TapWhenFeatureEnabledAsync"
Cohesion: 0.33
Nodes (5): FeatureFlagResultExtensions, Action, Func, IFeatureManager, Task

### Community 126 - "GlobalExceptionHandlingMiddleware"
Cohesion: 0.20
Nodes (12): IApplicationBuilder, IServiceCollection, Exception, HttpContext, ILogger, IOptions, IProblemDetailsService, IResxLocalizer (+4 more)

### Community 127 - "ProductTemplates/v1/Search/Request.cs"
Cohesion: 0.25
Nodes (7): Constants, Request, Brand, Color, Model, SearchTerm, RequestValidator

### Community 128 - "EmailRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, EmailRateLimitingPolicy (+1 more)

### Community 129 - ".SendAsync"
Cohesion: 0.18
Nodes (7): SendResponseBody, CancellationToken, SendRequestBody, Task, SendMessageBody, Msg, No

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.48
Nodes (7): Observability (OpenTelemetry), docker-compose.yml (Base Stack), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), Postgres Service (mm.postgres), RabbitMQ Service (mm.rabbitmq), Redis Service (mm.redis)

### Community 131 - ".AddInfrastructure"
Cohesion: 0.29
Nodes (7): HostOptions, JsonOptions, Assembly, IConfiguration, IResxLocalizer, IServiceCollection, IWebHostEnvironment

### Community 132 - "Request"
Cohesion: 0.22
Nodes (9): RequestBody, Request, Body, Id, RequestBody, Address, Description, Name (+1 more)

### Community 134 - "SendRequestBody"
Cohesion: 0.25
Nodes (8): SendContact, IReadOnlyList, SendRequestBody, HtmlContent, Sender, Subject, TextContent, To

### Community 135 - "Response"
Cohesion: 0.18
Nodes (10): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, DateTimeOffset, Response, ClientId, DeviceName, Id, IpAddress, IsCurrent (+2 more)

### Community 136 - ".GetMeAsync"
Cohesion: 0.24
Nodes (7): CancellationToken, IReadOnlyList, Task, IKeycloakPermissionClient, CancellationToken, HttpContext, Task

### Community 137 - ".WriteTooManyRequestsToResponse"
Cohesion: 0.13
Nodes (14): PartitionedRateLimiter, CancellationToken, Func, HttpContext, IConfiguration, IProblemDetailsService, IReadOnlyList, IResxLocalizer (+6 more)

### Community 138 - "ResultToResponseTransformer.cs"
Cohesion: 0.19
Nodes (12): Common.Application.EndpointFilters, microsoft_aspnetcore_hosting, microsoft_aspnetcore_http_iresult, microsoft_extensions_localization, ProblemResponse, ResultToAcceptedResponseTransformer, ResultToCreatedResponseTransformer, ResultToResponseTransformer (+4 more)

### Community 139 - "ProductTemplate"
Cohesion: 0.10
Nodes (17): ProductTemplateId, DefaultIdType, IReadOnlyList, List, ProductTemplate, Brand, Color, IsActive (+9 more)

### Community 140 - "Notifications.Application.Hubs"
Cohesion: 0.14
Nodes (7): Notifications.Infrastructure.Hubs, Notifications.Application.Hubs, HubConnectionContext, IUserIdProvider, microsoft_aspnetcore_signalr, NotificationGroupName, SubjectUserIdProvider

### Community 141 - "Response"
Cohesion: 0.22
Nodes (7): RouteGroupBuilder, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 142 - "TokenRefreshRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenRefreshRateLimitingPolicy (+1 more)

### Community 143 - "Setup"
Cohesion: 0.09
Nodes (22): LoadAll, microsoft_aspnetcore_httpoverrides, ModuleRegistry, Names, ModulesOptions, EnabledModules, ModulesOptionsValidator, IReadOnlyList (+14 more)

### Community 144 - "Response"
Cohesion: 0.13
Nodes (14): RouteGroupBuilder, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate, CreatedOn, Email (+6 more)

### Community 145 - "Request"
Cohesion: 0.17
Nodes (12): ProductTemplateId, RequestBody, Request, Body, Id, RequestBody, Description, Name (+4 more)

### Community 146 - ".RegisterAsync"
Cohesion: 0.08
Nodes (25): BindDeviceSessionRequest, BindDeviceSessionResponse, Guid, CancellationToken, Exception, Guid, ILogger, LoggerMessage (+17 more)

### Community 147 - "IRecurringBackgroundJobs"
Cohesion: 0.13
Nodes (13): RecurringJobOptions, IRecurringBackgroundJobs, Action, Expression, Func, Task, RecurringBackgroundJobsService, Action (+5 more)

### Community 148 - ".RegisterAsync"
Cohesion: 0.07
Nodes (33): IClientFactory, IInterModuleRequestClient, CancellationToken, Task, MassTransitInterModuleRequestClient, CancellationToken, IOptions, Task (+25 more)

### Community 149 - ".AddPersistence"
Cohesion: 0.14
Nodes (11): IDatabaseSeeder, Priority, CancellationToken, Task, CancellationToken, IServiceScopeFactory, Task, ProductsDatabaseSeeder (+3 more)

### Community 150 - "StockReservationExpirySweepJobRegistrar"
Cohesion: 0.31
Nodes (7): CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task, StockReservationExpirySweepJobRegistrar

### Community 151 - "OutboxOptions"
Cohesion: 0.10
Nodes (21): OutboxCleanupSettings, BatchSize, CronSchedule, Enabled, RetentionDays, OutboxCleanupSettingsValidator, OutboxOptions, BaseBackoffSeconds (+13 more)

### Community 152 - ".EnsureNoMigrationsPending"
Cohesion: 0.54
Nodes (4): MigrationGuard, ILogger, IServiceProvider, LoggerMessage

### Community 153 - ".AddCustomSwagger"
Cohesion: 0.25
Nodes (6): OpenApiOperation, OperationFilterContext, RemoveDefaultResponseSchemaFilter, IConfigureOptions, IServiceCollection, SwaggerGenOptions

### Community 154 - "Response"
Cohesion: 0.25
Nodes (6): IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, RouteGroupBuilder, Endpoint, Response, EmailVerificationToken, IsRegistered

### Community 155 - "ISmsGateway"
Cohesion: 0.13
Nodes (13): CancellationToken, Task, ISmsGateway, SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, SmsMessage (+5 more)

### Community 156 - "Products.Domain.Products.DomainEvents.v1"
Cohesion: 0.14
Nodes (8): Products.Domain.Products.DomainEvents.v1, Products.Application.Products.DomainEventHandlers.v1, ProductId, V1ProductDescriptionUpdatedDomainEvent, ProductId, V1ProductQuantityDecreasedDomainEvent, ProductId, V1ProductQuantityIncreasedDomainEvent

### Community 157 - "CreateStockLevelOnProductCreatedHandler"
Cohesion: 0.24
Nodes (8): CancellationToken, DefaultIdType, IFusionCache, ILogger, IOptions, LoggerMessage, Task, CreateStockLevelOnProductCreatedHandler

### Community 158 - "OtpOptions"
Cohesion: 0.08
Nodes (24): OtpOptions, DummyCode, EmailQuotaWindowMinutes, ExpirationInMinutes, Length, MaxSendsPerEmailPerWindow, MaxSendsPerPhonePerWindow, PhoneQuotaWindowMinutes (+16 more)

### Community 159 - "IInventoryDbContext"
Cohesion: 0.13
Nodes (15): DbSet, IInventoryDbContext, StockLevels, StockReservations, DbContextOptions, DbSet, ILogger, TimeProvider (+7 more)

### Community 160 - "OtpServiceBase"
Cohesion: 0.21
Nodes (8): OtpCacheEntry, DateTimeOffset, CancellationToken, IFusionCache, SemaphoreSlim, Task, TimeSpan, OtpServiceBase

### Community 161 - ".ReserveSeriesAsync"
Cohesion: 0.12
Nodes (17): GetProductRequest, GetProductResponse, DefaultIdType, CancellationToken, IOptions, List, RequestBody, RouteGroupBuilder (+9 more)

### Community 162 - "IEmailGateway"
Cohesion: 0.21
Nodes (9): CancellationToken, Task, EmailMessage, IEmailGateway, CancellationToken, IFusionCache, IOptions, Task (+1 more)

### Community 164 - "NotificationsDbContext"
Cohesion: 0.15
Nodes (13): DbSet, INotificationsDbContext, DeviceRegistrations, DbContextOptions, DbSet, ILogger, TimeProvider, NotificationsDbContext (+5 more)

### Community 165 - "ICurrentUser"
Cohesion: 0.07
Nodes (25): ICurrentUser, Id, IdAsString, Roles, SessionId, ICollection, CurrentUser, Id (+17 more)

### Community 166 - "IamModule"
Cohesion: 0.18
Nodes (10): Action, IApplicationBuilder, IEnumerable, RateLimiterOptions, IamModule, ActivitySourceNames, MeterNames, Name (+2 more)

### Community 167 - "PaginationRequest"
Cohesion: 0.07
Nodes (30): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Stores.v1.My.AuditLog, Products.Endpoints.Products.v1.AuditLog, PaginationRequest, After, IncludeTotal, PageNumber, PageSize (+22 more)

### Community 168 - "ProductsModule.cs"
Cohesion: 0.06
Nodes (22): Products.Endpoints.Stores.v1.Create, Products.Endpoints.Stores, Products.Endpoints.Probe, Products.Endpoints.Products, Products.Endpoints.ProductTemplates, Products.Endpoints.ProductTemplates.v1.Create, Products.Endpoints, IAssemblyReference (+14 more)

### Community 169 - "SendSecurityAlertRequestHandler"
Cohesion: 0.27
Nodes (9): SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, CancellationToken, IOptions, RequestLocalizationOptions, Task (+1 more)

### Community 171 - "ICaptchaService"
Cohesion: 0.25
Nodes (5): ICaptchaService, DummyCaptchaService, IConfiguration, IServiceCollection, Setup

### Community 172 - "ResxLocalizationOptions"
Cohesion: 0.25
Nodes (7): ResxLocalizationOptions, DefaultCulture, SupportedCultures, ResxLocalizationOptionsValidator, ICollection, IApplicationBuilder, IOptions

### Community 173 - "IAM.Application.Captcha.Services"
Cohesion: 0.48
Nodes (3): IAM.Domain.Captcha, IAM.Infrastructure.Captcha.Services, IAM.Application.Captcha.Services

### Community 174 - "system_diagnostics"
Cohesion: 0.09
Nodes (18): BackgroundJobs.Telemetry, hangfire_server, opentelemetry_metrics, opentelemetry_opentelemetrybuilder, opentelemetry_resources, opentelemetry_trace, LoginMethods, SessionRevokedReasons (+10 more)

### Community 175 - "PaginationResponse"
Cohesion: 0.08
Nodes (23): JsonElement, AuditLogDto, DateTimeOffset, PaginationResponse, HasNext, HasPrevious, HasTotal, NextPageNumber (+15 more)

### Community 176 - ".FixedWindow"
Cohesion: 0.21
Nodes (11): FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit, FixedWindowValidator, RateLimitPartitions, HttpContext (+3 more)

### Community 177 - "RequireFeatureFilter"
Cohesion: 0.14
Nodes (12): IEndpointFilter, IFeatureManagerSnapshot, RequireFeatureFilter, ActivitySource, Counter, EndpointFilterDelegate, EndpointFilterInvocationContext, IResxLocalizer (+4 more)

### Community 178 - "PushMessage"
Cohesion: 0.13
Nodes (14): IReadOnlyDictionary, CancellationToken, IReadOnlyList, Task, IPushGateway, PushMessage, CancellationToken, ILogger (+6 more)

### Community 179 - "CachingOptions"
Cohesion: 0.09
Nodes (25): CachingEntryDefaults, Duration, FactoryHardTimeout, FactorySoftTimeout, FailSafeMaxDuration, FailSafeThrottleDuration, CachingOptions, AllowInMemoryOnlyInProduction (+17 more)

### Community 180 - "SmsOptions"
Cohesion: 0.10
Nodes (21): SmsOptions, AppName, AttemptTimeoutSeconds, BaseUrl, MaxPerDay, MaxPerPhoneNumberPerDay, MaxRetryAttempts, MsgHeader (+13 more)

### Community 181 - "RabbitMqOptions"
Cohesion: 0.11
Nodes (17): RabbitMqOptions, DefaultConcurrentMessageLimit, DefaultPrefetchCount, Host, Password, Port, RetryIntervalDeltaMs, RetryLimit (+9 more)

### Community 183 - "SendRequestBody"
Cohesion: 0.25
Nodes (8): SendMessageBody, IReadOnlyList, SendRequestBody, AppName, Encoding, IysFilter, Messages, MsgHeader

### Community 184 - "RequestBody"
Cohesion: 0.12
Nodes (18): DateTimeOffset, DefaultIdType, ICollection, RequestBody, Request, Body, RequestBody, FirstDeadline (+10 more)

### Community 185 - "IdentityScheme"
Cohesion: 0.22
Nodes (7): IdentityScheme, Email, PhoneNumber, RouteGroupBuilder, Setup, RouteGroupBuilder, Setup

### Community 186 - ".Get"
Cohesion: 0.24
Nodes (5): CreateStoreRateLimitingPolicy, RateLimiterOptions, Action, IEnumerable, RateLimiterOptions

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
Cohesion: 0.07
Nodes (21): CancellationToken, Task, IServiceAccountTokenProvider, KeycloakPaths, CancellationToken, DateTimeOffset, SemaphoreSlim, Task (+13 more)

### Community 191 - ".AddProductToMyStoreAsync"
Cohesion: 0.22
Nodes (8): CancellationToken, ProductTemplate, RouteGroupBuilder, Store, Task, Endpoint, Response, Id

### Community 192 - "ReCaptchaResponse"
Cohesion: 0.25
Nodes (7): DateTime, ReCaptchaResponse, ChallengeTs, ErrorCodes, Hostname, Score, Success

### Community 193 - "Request"
Cohesion: 0.40
Nodes (5): Request, Address, Description, Name, RequestValidator

### Community 194 - "CorsOptions"
Cohesion: 0.25
Nodes (8): CorsOptions, AllowCredentials, AllowedHeaders, AllowedMethods, AllowedOrigins, MaxAgeInSeconds, CorsOptionsValidator, IReadOnlyList

### Community 195 - "WarehouseGatewayProvider"
Cohesion: 0.67
Nodes (3): WarehouseGatewayProvider, Dummy, Http

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

### Community 205 - "CachedCaptchaService"
Cohesion: 0.29
Nodes (5): CancellationToken, IFusionCache, IOptions, Task, CachedCaptchaService

### Community 210 - "ConfigureSwaggerOptions"
Cohesion: 0.28
Nodes (7): ApiVersionDescription, IApiVersionDescriptionProvider, IConfigureOptions, OpenApiInfo, IOptions, SwaggerGenOptions, ConfigureSwaggerOptions

### Community 211 - "Request"
Cohesion: 0.25
Nodes (8): ProductTemplateId, Request, Description, Name, Price, ProductTemplateId, Quantity, RequestValidator

### Community 212 - "Versioning/Setup.cs"
Cohesion: 0.20
Nodes (7): ApiVersionSet, asp_versioning, asp_versioning_builder, asp_versioning_conventions, Setup, IEndpointRouteBuilder, IServiceCollection

### Community 213 - "BackgroundJobsTelemetry"
Cohesion: 0.14
Nodes (11): IServerFilter, PerformedContext, PerformingContext, JobMetricsFilter, BackgroundJobsTelemetry, ActivitySource, ConcurrentDictionary, Counter (+3 more)

### Community 214 - "Common.Application.Options"
Cohesion: 0.07
Nodes (18): common_application_localization_resources, IAM.Domain.Users, Common.Application.Options, Common.Domain.Extensions, Common.Application.ModelBinders, Common.Application.Validation, Common.Domain.Devices, IAM.Endpoints.Common.Validations (+10 more)

### Community 215 - "IProductsDbContext"
Cohesion: 0.10
Nodes (17): DbSet, ProductTemplate, Store, IProductsDbContext, Products, ProductTemplates, Stores, CancellationToken (+9 more)

### Community 216 - "ReservationStatus"
Cohesion: 0.29
Nodes (6): ReservationStatus, Active, Committed, Expired, Released, RequiresReconciliation

### Community 217 - "FeatureFlags"
Cohesion: 0.33
Nodes (5): Checkout, FeatureFlags, IAM, Notifications, Products

### Community 218 - "StoreId"
Cohesion: 0.09
Nodes (19): StoreId, DefaultIdType, StoreId, Request, Id, RequestValidator, CancellationToken, RouteGroupBuilder (+11 more)

### Community 219 - "IBackgroundJobs"
Cohesion: 0.23
Nodes (7): IBackgroundJobs, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 220 - ".SaveChangesAsync"
Cohesion: 0.06
Nodes (23): CancellationToken, Task, CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint, CancellationToken (+15 more)

### Community 221 - "Keycloak realm as code"
Cohesion: 0.25
Nodes (7): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Secrets, Seed users (dev / test only)

### Community 222 - "Key decisions"
Cohesion: 0.29
Nodes (7): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Key decisions

### Community 223 - "Request"
Cohesion: 0.67
Nodes (3): Request, Id, RequestValidator

### Community 224 - ".MapEndpoint"
Cohesion: 0.40
Nodes (3): RouteGroupBuilder, RouteGroupBuilder, Setup

### Community 225 - "IntegrationEventHandlerBase"
Cohesion: 0.05
Nodes (40): IntegrationEventHandlerBase, MaxEventAge, CancellationToken, ConsumeContext, DefaultIdType, IFusionCache, ILogger, IOptions (+32 more)

### Community 226 - "NetGsmSmsGateway"
Cohesion: 0.32
Nodes (7): Exception, HttpClient, ILogger, IOptions, JsonSerializerOptions, LoggerMessage, NetGsmSmsGateway

### Community 227 - "RegisterRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, RegisterRateLimitingPolicy (+1 more)

### Community 228 - "ProjectionUpsertOutcome"
Cohesion: 0.20
Nodes (7): Common.Application.Persistence.Projections, ProjectionEntity, SourceVersion, ProjectionUpsertOutcome, Inserted, Stale, Updated

### Community 229 - ".AddCommonOptions"
Cohesion: 0.40
Nodes (4): Setup, IConfiguration, IHostEnvironment, IServiceCollection

### Community 230 - "InterModuleRequestHandler"
Cohesion: 0.19
Nodes (8): IConsumer, IInterModuleRequestHandler, CancellationToken, Task, InterModuleRequestHandler, CancellationToken, ConsumeContext, Task

### Community 231 - ".UseModule"
Cohesion: 0.22
Nodes (7): DashboardContext, IAuthorizationService, IDashboardAsyncAuthorizationFilter, IApplicationBuilder, IOptions, HangfireCustomAuthorizationFilter, Task

### Community 232 - "DeviceRegistryReconciliationService"
Cohesion: 0.14
Nodes (13): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator, IReadOnlyCollection, CancellationToken, ILogger (+5 more)

### Community 233 - ".HasPatternIndex"
Cohesion: 0.36
Nodes (6): IndexBuilder, TrigramIndexExtensions, EntityTypeBuilder, Expression, Func, ModelBuilder

### Community 234 - "Setup"
Cohesion: 0.33
Nodes (4): IApplicationBuilder, IWebHostEnvironment, Type, Setup

### Community 235 - "V1ProductAddedToStoreDomainEvent"
Cohesion: 0.16
Nodes (11): StoreId, ProductSnapshot, ProductTemplateId, ProductSnapshot, V1ProductAddedToStoreDomainEvent, V1ProductAddedToStoreDomainEventExtensions, ProductSnapshot, ProductTemplateId (+3 more)

### Community 236 - "IOtpService"
Cohesion: 0.15
Nodes (14): IssueVerificationTokenRequest, IssueVerificationTokenResponse, CancellationToken, Task, TimeSpan, IOtpService, OtpVerificationOutcome, InvalidOtp (+6 more)

### Community 237 - "ProblemDetailsExtensions.cs"
Cohesion: 0.40
Nodes (3): ProblemDetails, ProblemDetailsExtensions, ICollection

### Community 238 - "SecurityHeadersMiddleware"
Cohesion: 0.20
Nodes (9): SecurityHeadersOptions, Headers, SecurityHeadersOptionsValidator, Dictionary, HttpContext, IOptions, RequestDelegate, Task (+1 more)

### Community 239 - "IAMModule.cs"
Cohesion: 0.06
Nodes (29): Common.Infrastructure.Modules, Notifications.Application.Sms, Notifications.Infrastructure.Devices, Inventory.Endpoints, Common.Endpoints.Versioning, Notifications.Infrastructure.Email, Notifications.Infrastructure.Sms, Notifications.Infrastructure.Email.Brevo (+21 more)

### Community 240 - "InterModuleRequestOptions"
Cohesion: 0.11
Nodes (16): ConsumerDefinition, IConsumerConfigurator, IRegistrationContext, InterModuleRequestOptions, DependencyUnavailableRetryAfterSeconds, HandlerConcurrentMessageLimit, HandlerPrefetchCount, Timeouts (+8 more)

### Community 241 - "Request"
Cohesion: 0.20
Nodes (10): Guid, Request, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken, Password (+2 more)

### Community 242 - "StronglyTypedIdSchemaFilter.cs"
Cohesion: 0.33
Nodes (4): IOpenApiSchema, ISchemaFilter, SchemaFilterContext, StronglyTypedIdSchemaFilter

### Community 243 - "CustomValidator"
Cohesion: 0.15
Nodes (18): CustomValidator, Request, Id, RequestValidator, RequestBody, RequestBodyValidator, RequestBodyValidator, RequestBodyValidator (+10 more)

### Community 244 - "KeyedResilienceProfile"
Cohesion: 0.17
Nodes (12): AbstractValidator, KeyedResilienceProfile, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, RateLimitPermits (+4 more)

### Community 245 - "SendForEmail/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForEmail, Request, CaptchaToken, Email, RequestValidator

### Community 246 - "SeedingCompletionTracker"
Cohesion: 0.15
Nodes (8): SeedingCompletionTracker, CancellationToken, Exception, Task, Setup, IOptions, IServiceCollection, TaskCompletionSource

### Community 247 - "KeycloakPermissionAuthorizationHandler.cs"
Cohesion: 0.23
Nodes (7): IAM.Infrastructure.Auth, hangfire_annotations, hangfire_dashboard, microsoft_aspnetcore_authentication, microsoft_aspnetcore_authentication_jwtbearer, microsoft_aspnetcore_authorization, system_collections_concurrent

### Community 248 - "SendErrorBody"
Cohesion: 0.67
Nodes (3): SendErrorBody, Code, Message

### Community 249 - ".AddProductAsync"
Cohesion: 0.18
Nodes (9): Products.Endpoints.Stores.v1.AddProduct, CancellationToken, ProductTemplate, RouteGroupBuilder, Store, Task, Endpoint, Response (+1 more)

### Community 250 - "ProductId"
Cohesion: 0.13
Nodes (14): DefaultIdType, ProductId, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, Id (+6 more)

### Community 251 - "Request"
Cohesion: 0.20
Nodes (10): RequestBody, Request, Body, Id, RequestBody, Description, Name, Price (+2 more)

### Community 252 - ".TryWriteAsync"
Cohesion: 0.40
Nodes (4): ProblemDetailsContext, ProblemDetailsServiceExtensions, IProblemDetailsService, Task

### Community 260 - "RequestBodyLimitMetadata"
Cohesion: 0.18
Nodes (10): IAuthenticationSchemeProvider, RequestBodyLimitMetadata, MaxBodyBytes, RequestBodyLimitMiddleware, Func, HttpContext, IHttpMaxRequestBodySizeFeature, RequestDelegate (+2 more)

### Community 261 - "FirebasePushGateway.cs"
Cohesion: 0.22
Nodes (7): Notifications.Application.Push, Notifications.Infrastructure.Push, Notifications.Infrastructure.Push.Firebase, firebaseadmin, firebaseadmin_messaging, google_apis_auth_oauth2, PushErrors

### Community 262 - ".SendWithPipelineAsync"
Cohesion: 0.25
Nodes (7): HttpClientKeyedExtensions, CancellationToken, HttpClient, HttpRequestMessage, HttpResponseMessage, ResiliencePipeline, Task

### Community 263 - "Response"
Cohesion: 0.20
Nodes (8): RouteGroupBuilder, Endpoint, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 264 - "V1StoreCreatedDomainEvent"
Cohesion: 0.27
Nodes (8): Products.Application.Stores.DomainEventHandlers.v1, CancellationToken, Task, SimulateSomeBusinessHandler, StoreCreatedIntegrationEventPublishingHandler, V1StoreCreatedDomainEventHandlers, StoreId, V1StoreCreatedDomainEvent

### Community 265 - "BackgroundJobsModule"
Cohesion: 0.25
Nodes (7): BackgroundJobsModule, ActivitySourceNames, MeterNames, Name, StartupPriority, IEndpointRouteBuilder, IEnumerable

### Community 266 - "KeycloakPermissionRequirement"
Cohesion: 0.18
Nodes (6): IAuthorizationRequirement, KeycloakPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder, KeycloakPermissionRequirement, Permission

### Community 267 - "AuditLogOptions"
Cohesion: 0.33
Nodes (6): AuditLogOptions, PerSchemaRetentionDays, PurgeBatchSize, RetentionDays, AuditLogOptionsValidator, Dictionary

### Community 268 - ".ActivateProductTemplateAsync"
Cohesion: 0.22
Nodes (7): CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, Id, RequestValidator

### Community 269 - ".DeactivateStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 270 - "DefaultResponsesOperationFilter"
Cohesion: 0.70
Nodes (3): OpenApiOperation, OperationFilterContext, DefaultResponsesOperationFilter

### Community 271 - ".UpsertIfNewerAsync"
Cohesion: 0.20
Nodes (9): ICurrentDbContext, ProjectionUpsertExtensions, Action, CancellationToken, DbContext, DbSet, Func, Task (+1 more)

### Community 272 - ".AddResilientHttpClient"
Cohesion: 0.22
Nodes (8): HttpStandardResilienceOptions, IHttpClientBuilder, Setup, Action, HttpClient, IOptions, IServiceCollection, IServiceProvider

### Community 273 - ".AddNetGsm"
Cohesion: 0.33
Nodes (5): IConfiguration, IFusionCache, IOptions, IServiceCollection, Setup

### Community 274 - "DeviceRegistryReconcileJobRegistrar"
Cohesion: 0.31
Nodes (7): CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task, DeviceRegistryReconcileJobRegistrar

### Community 275 - ".GetAuditLogAsync"
Cohesion: 0.38
Nodes (5): DbContextExtensions, CancellationToken, DbSet, JsonSerializerOptions, Task

### Community 276 - ".From"
Cohesion: 0.36
Nodes (6): AspNetResult, ResxLocalizer, EndpointFilterDelegate, EndpointFilterInvocationContext, IStringLocalizer, ValueTask

### Community 277 - "SendResponseBody"
Cohesion: 0.50
Nodes (4): SendResponseBody, Code, Description, JobId

### Community 278 - "EventDispatcher"
Cohesion: 0.31
Nodes (7): EventDispatcher, ActivitySource, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task

### Community 279 - "Response"
Cohesion: 0.25
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.Create, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 280 - ".UpdateMyStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 281 - ".TryReadFromJsonAsync"
Cohesion: 0.40
Nodes (4): HttpContent, CancellationToken, Task, HttpContentJsonExtensions

### Community 282 - "Infrastructure/StringExtensions.cs"
Cohesion: 0.40
Nodes (3): opentelemetry_exporter, OtlpExportProtocol, StringExtensions

### Community 283 - ".UpdateStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 284 - "Request"
Cohesion: 0.20
Nodes (10): DateTimeOffset, DefaultIdType, RequestBody, Request, Body, RequestBody, ProductId, Quantity (+2 more)

### Community 285 - "RequestBodyLimitFilter.cs"
Cohesion: 0.31
Nodes (6): Common.Endpoints.Webhooks, microsoft_aspnetcore_http_features, RequestBodyLimitEndpointExtensions, RequestBodyLimitFilter, Func, HttpContext

### Community 286 - "microsoft_extensions_options"
Cohesion: 0.08
Nodes (25): Common.Application.Persistence.Inbox, Inventory.Application.IntegrationEventHandlers, Outbox, Common.Application.BackgroundJobs, Common.Infrastructure.Persistence.Outbox, BackgroundJobs, Outbox.Telemetry, Common.Application.EventBus (+17 more)

### Community 288 - "DummySmsGateway"
Cohesion: 0.38
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, DummySmsGateway

### Community 289 - ".SetRetryAfterHeader"
Cohesion: 0.40
Nodes (4): RetryAfterHeaderExtensions, HttpResponse, RateLimitLease, TimeSpan

### Community 290 - "IInterModuleRequest"
Cohesion: 0.29
Nodes (8): IInterModuleRequest, DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, IReadOnlyList, CancellationToken, Task, GetDeviceSessionsRequestHandler

### Community 291 - "GetActiveSessionIdsRequest"
Cohesion: 0.44
Nodes (7): GetActiveSessionIdsRequest, GetActiveSessionIdsResponse, UserSessionIds, IReadOnlyList, CancellationToken, Task, GetActiveSessionIdsRequestHandler

### Community 293 - "IdentitySchemeOptions"
Cohesion: 0.33
Nodes (5): IdentitySchemeOptions, Scheme, IdentitySchemeOptionsValidator, IEndpointRouteBuilder, IOptions

### Community 294 - ".SeedProductAsync"
Cohesion: 0.33
Nodes (5): CancellationToken, List, ProductTemplateId, StoreId, Task

### Community 295 - "EnrichLogsWithUserInfoMiddleware"
Cohesion: 0.25
Nodes (6): IMiddleware, serilog_context, HttpContext, RequestDelegate, Task, EnrichLogsWithUserInfoMiddleware

### Community 297 - "IOutboxMessage"
Cohesion: 0.29
Nodes (7): IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, DateTimeOffset

### Community 298 - "Request"
Cohesion: 0.40
Nodes (5): Request, Brand, Color, Model, RequestValidator

### Community 299 - "AuditableEntity"
Cohesion: 0.25
Nodes (8): AuditableEntity, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, Version, DateTimeOffset

### Community 300 - "ResultTelemetryExtensions"
Cohesion: 0.38
Nodes (4): Activity, ResultTelemetryExtensions, ActivitySource, Task

### Community 301 - "SwaggerDefaultValues"
Cohesion: 0.33
Nodes (5): IOperationFilter, JsonValue, OpenApiOperation, OperationFilterContext, SwaggerDefaultValues

### Community 302 - "KeyedResiliencePipelines"
Cohesion: 0.29
Nodes (6): ResiliencePipelineRegistry, KeyedResiliencePipelines, IOptions, List, Lock, RateLimiter

### Community 303 - "BackgroundJobsOptions"
Cohesion: 0.29
Nodes (7): BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount, BackgroundJobsOptionsValidator

### Community 304 - "Request"
Cohesion: 0.50
Nodes (4): Request, Email, Otp, RequestValidator

### Community 305 - ".EmailVerificationTokenValidation"
Cohesion: 0.50
Nodes (3): IResxLocalizer, IRuleBuilder, IRuleBuilderOptions

### Community 308 - ".AddBrevo"
Cohesion: 0.33
Nodes (5): IConfiguration, IFusionCache, IOptions, IServiceCollection, Setup

### Community 310 - ".RemoveProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 311 - "StronglyTypedIdBinder.cs"
Cohesion: 0.29
Nodes (5): IModelBinder, microsoft_aspnetcore_mvc_modelbinding, ModelBindingContext, StronglyTypedIdBinder, Task

### Community 312 - "JobStatus"
Cohesion: 0.33
Nodes (5): JobStatus, Failed, Queued, Running, Succeeded

### Community 313 - ".AddServices"
Cohesion: 0.33
Nodes (5): DatabaseOptions, ConnectionString, DatabaseOptionsValidator, IConfiguration, IServiceCollection

### Community 314 - "Request"
Cohesion: 0.33
Nodes (6): Request, Body, Id, RequestBody, ProviderReference, RequestValidator

### Community 315 - ".UpdateProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 316 - "Request"
Cohesion: 0.33
Nodes (6): Request, Address, Description, Name, OwnerId, RequestValidator

### Community 317 - "Request"
Cohesion: 0.33
Nodes (6): ProductId, StoreId, Request, Id, ProductId, RequestValidator

### Community 318 - "CurrentUser.cs"
Cohesion: 0.40
Nodes (3): Common.Infrastructure.Auth.Services, Common.Infrastructure.Auth, system_security_claims

### Community 319 - "Request"
Cohesion: 0.20
Nodes (10): RequestBody, Request, Body, Id, RequestBody, Description, Name, Price (+2 more)

### Community 320 - ".InvokeAsync"
Cohesion: 0.40
Nodes (4): EndpointFilterDelegate, EndpointFilterInvocationContext, IHttpMaxRequestBodySizeFeature, ValueTask

### Community 322 - "V1StockReservationCommitConflictDetectedDomainEvent"
Cohesion: 0.40
Nodes (4): DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationCommitConflictDetectedDomainEvent

### Community 323 - ".PhoneNumberValidation"
Cohesion: 0.50
Nodes (3): IResxLocalizer, IRuleBuilder, IRuleBuilderOptions

### Community 324 - "V1StockReservationExpiredDomainEvent"
Cohesion: 0.40
Nodes (4): DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationExpiredDomainEvent

### Community 325 - ".AddDeviceRegistryReconciliation"
Cohesion: 0.40
Nodes (3): IServiceCollection, RouteGroupBuilder, Setup

## Knowledge Gaps
- **950 isolated node(s):** `Id`, `IdAsString`, `Roles`, `SessionId`, `JwtClaimNames` (+945 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 2217 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **93 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `microsoft_entityframeworkcore`, `FirebasePushGateway.cs`, `Common.Domain.ResultMonad`, `Setup`, `EmailOptions`, `Common.Infrastructure.Extensions`, `microsoft_extensions_options`, `Inventory.Domain.StockReservations`, `ConfigureSwaggerOptions.cs`, `ProductsModule.cs`, `IAM.Application.Captcha.Services`, `system_diagnostics`, `Common.InterModuleRequests.Contracts`, `CachingOptions`, `PushOptions`, `Infrastructure/Setup.cs`, `Common.Application.Pagination`, `KeycloakTokenClient.cs`, `IAMModule.cs`, `KeycloakPermissionAuthorizationHandler.cs`?**
  _High betweenness centrality (0.172) - this node is a cross-community bridge._
- **Why does `Result` connect `Result` to `.SendAsync`, `.GetMeAsync`, `FirebasePushGateway`, `Error`, `ProductTemplate`, `ApplicationUserId`, `.ActivateProductTemplateAsync`, `.DeactivateStoreAsync`, `.RegisterAsync`, `.GetAuditLogAsync`, `.RegisterAsync`, `DummyEmailGateway`, `.RevokeSession`, `.UpdateMyStoreAsync`, `ISmsGateway`, `.UpdateStoreAsync`, `.SingleAsResult`, `.ReserveSeriesAsync`, `IEmailGateway`, `DummySmsGateway`, `.SendOtp`, `.RefreshToken`, `ICurrentUser`, `ICaptchaService`, `ResultTelemetryExtensions`, `PaginationQueryableExtensions`, `PaginationResponse`, `Response`, `PushMessage`, `ReCaptchaService`, `SendEmailOtpRequestHandler`, `BrevoEmailGateway`, `.RemoveProductAsync`, `.UpdateProductAsync`, `.SearchProductTemplatesAsync`, `KeycloakTokenClient`, `.AddProductToMyStoreAsync`, `.SendCoreAsync`, `.ReserveStockAsync`, `StockReservation`, `Response`, `AuditableEntityResponse`, `CachedCaptchaService`, `.DeactivateProductTemplateAsync`, `DomainEvent`, `.SearchStoresAsync`, `IProductsDbContext`, `.GetProductAsync`, `StoreId`, `.SaveChangesAsync`, `.SearchMyProductsAsync`, `NetGsmSmsGateway`, `.SearchStoreProductsAsync`, `KeycloakTokenClient.cs`, `HttpWarehouseGateway`, `.IsRegisteredAsync`, `InventoryOptions`, `Response`, `.HandleWarehouseWebhookAsync`, `.AddProductAsync`, `ProductId`, `.TapWhenFeatureEnabledAsync`?**
  _High betweenness centrality (0.150) - this node is a cross-community bridge._
- **Why does `ApplicationUserId` connect `ApplicationUserId` to `IStronglyTypedId`, `NotificationPayload`, `KeycloakAdminClient`, `V1StoreCreatedDomainEvent`, `Notifications.Application.Hubs`, `KeycloakPermissionAuthorizationHandler`, `Response`, `.RegisterAsync`, `Common.Domain.StronglyTypedIds`, `.RegisterAsync`, `DeviceRegistration`, `.RevokeSession`, `JobRow`, `IInterModuleRequest`, `For`, `GetActiveSessionIdsRequest`, `ICurrentUser`, `SendSecurityAlertRequestHandler`, `AuditableEntity`, `PaginationResponse`, `Response`, `Request`, `KeycloakTokenClient`, `IntegrationEventOutbox`, `AuditableEntityResponse`, `IAuditableEntity`, `.SearchStoresAsync`, `.Configure`, `Store`, `IProductsDbContext`, `StoreId`, `Seeder`, `.SearchStoreProductsAsync`, `StockLevel`, `Response`?**
  _High betweenness centrality (0.073) - this node is a cross-community bridge._
- **What connects `Id`, `IdAsString`, `Roles` to the rest of the system?**
  _950 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `IStronglyTypedId` be split into smaller, more focused modules?**
  _Cohesion score 0.05782312925170068 - nodes in this community are weakly interconnected._
- **Should `microsoft_entityframeworkcore` be split into smaller, more focused modules?**
  _Cohesion score 0.07864488808227466 - nodes in this community are weakly interconnected._
- **Should `RedisFixedWindowRateLimiter` be split into smaller, more focused modules?**
  _Cohesion score 0.13333333333333333 - nodes in this community are weakly interconnected._