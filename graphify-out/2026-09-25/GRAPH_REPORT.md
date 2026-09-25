# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-09-24)

## Corpus Check
- 574 files · ~89,811 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 4921 nodes · 9721 edges · 385 communities (294 shown, 91 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 259 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `28533ea9`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- IStronglyTypedId
- NotificationPayload
- OutboxProcessor
- Common.Application.Options
- Hybrid DDD (Writes) / VSA (Reads)
- KeycloakAdminClient
- RedisFixedWindowRateLimiter
- .AddModuleDbContext
- FirebasePushGateway
- .RequireOtpTemplateForDefaultCulture
- Error
- microsoft_aspnetcore_http
- IKeycloakAdminClient
- ISearchLanguageResolver
- UserRepresentation
- KeycloakPermissionAuthorizationHandler
- EmailOptions
- Product
- StockReservation
- Common.Domain.StronglyTypedIds
- LogProductCatalogChangeHandler
- BoundedRequestCaptureStream
- DeviceRegistration
- DummyEmailGateway
- ICurrentUser
- microsoft_extensions_options
- IEvent
- RequestResponseBodyLoggingMiddleware
- IDbContext
- Result
- JobRow
- NotificationsModule
- .SingleAsResult
- Inventory.Domain.StockReservations.DomainEvents.v1
- KeycloakPermissionClient
- OutboxMessage
- CustomRateLimitingOptions
- .SendOtp
- RequestBody
- ObservabilityOptions
- KeycloakPermissionAuthorizationHandler.cs
- Request
- .SaveWithOutboxAsync
- CreateStoreRateLimitingPolicy
- KeycloakOptions
- VerifyPhoneOtpResponse
- PaginationQueryableExtensions
- Outbox Misuse Check
- Common.Application.EventBus
- Add Integration Event Command
- Response
- .VerifyOtp
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
- .SendCoreAsync
- BackgroundJobsService
- OutboxModule
- .AddNotificationsSignalR
- ValueObject
- .HandleAsync
- IntegrationEventHandlerBase
- FullTextSearchOptions
- Response
- .WriteAsync
- AsNoTracking Coverage Check
- IntegrationEvent
- Response
- .RemoveMyProductAsync
- CheckRegistrationRateLimitingPolicy
- DomainEvent
- IAuditableEntity
- .SearchStoresAsync
- Split-Deployment PoC
- microsoft_entityframeworkcore
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
- KeyedResiliencePipelines
- .SearchMyProductsAsync
- DatabaseSeederOrchestrator
- StockReservationExpirySweepService
- .SearchStoreProductsAsync
- AuditLogRetentionJobRegistrar
- OutboxDbContext
- ResiliencyOptions
- HttpContextTargetingContextAccessor
- InboxCleanupJob
- AuditableEntityResponse
- .RegisterAsync
- Common.Domain.ResultMonad
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
- StoreId
- EmailRateLimitingPolicy
- .SendAsync
- docker-compose.yml (Base Stack)
- .AddInfrastructure
- Request
- Consumer Idempotency (IntegrationEventHandlerBase)
- SendRequestBody
- IInterModuleRequest
- .GetMeAsync
- .WriteTooManyRequestsToResponse
- ResultToResponseTransformer.cs
- ProductTemplate
- ProcessedMessage
- system_net
- TokenRefreshRateLimitingPolicy
- Setup
- Response
- Request
- .RegisterAsync
- .AddOrUpdate
- IInterModuleRequestClient
- TokenResponseRepresentation
- StockReservationExpirySweepJobRegistrar
- OutboxOptions
- .EnsureNoMigrationsPending
- OpenApiOptions
- Setup.Logger.cs
- ISmsGateway
- StrictDateTimeOffsetJsonConverter
- CreateStockLevelOnProductCreatedHandler
- OtpOptions
- IInventoryDbContext
- OtpServiceBase
- IntegrationEventOutbox
- IEmailGateway
- For
- NotificationsDbContext
- CurrentUser
- IamModule
- PaginationRequest
- Response
- SendSecurityAlertRequestHandler
- Notifications.Application/IAssemblyReference.cs
- ICaptchaService
- Stores/v1/Create/Endpoint.cs
- ProductTemplateId
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
- .AssignBasicRoleOrRollbackAsync
- IdentityScheme
- .Get
- ReverseProxyOptions
- BoundedCaptureStream
- JobClaimExtensions
- ServiceAccountTokenCache
- .AddProductToMyStoreAsync
- ReCaptchaResponse
- .UpdateMyStoreAsync
- .RaiseEvent
- InventoryOptions.cs
- TokenCreateRateLimitingPolicy
- .Failure
- InventoryModule
- .AddKeycloakInfrastructure
- NotificationsTelemetry
- KeycloakScopes
- Request
- SmsRateLimitingPolicy
- Configuration-Driven Module Loading
- StatelessInboxStore
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- IDatabaseSeeder
- SeedingCompletionTracker
- Versioning/Setup.cs
- BackgroundJobsTelemetry
- fluentvalidation
- IProductsDbContext
- .UpsertIfNewerAsync
- FeatureFlags
- .SendOtp
- IBackgroundJobs
- .ActivateProductTemplateAsync
- Keycloak realm as code
- Key decisions
- FirebasePushGateway.cs
- .MapEndpoint
- IInboxStore
- NetGsmSmsGateway
- RegisterRateLimitingPolicy
- ProjectionUpsertOutcome
- .AddCommonOptions
- InterModuleRequestHandler
- .UseModule
- ApplicationUserId
- .HasPatternIndex
- .ReleaseStockReservationAsync
- BaseDbContext
- IOtpService
- .RemoveProductAsync
- SecurityHeadersMiddleware
- microsoft_extensions_configuration
- InterModuleRequestHandlerDefinition
- Request
- .ReserveSeriesAsync
- CustomValidator
- KeyedResilienceProfile
- SendForEmail/Request.cs
- UtcDateTimeOffsetConverter
- AuditLogEntryConfiguration
- SendErrorBody
- GetActiveSessionIdsRequest
- Response
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
- .CommitStockReservationAsync
- .SendWithPipelineAsync
- Response
- V1StoreCreatedDomainEvent
- BackgroundJobsModule
- KeycloakUser
- AuditLogOptions
- .DeactivateProductTemplateAsync
- .DeactivateStoreAsync
- IntegrationEventConverter
- ResiliencyOptions.cs
- .AddResilientHttpClient
- .AddNetGsm
- DeviceRegistryReconciliationService
- .GetAuditLogAsync
- .From
- SendResponseBody
- EventDispatcher
- Response
- InterModuleRequestOptions
- .TryReadFromJsonAsync
- Infrastructure/StringExtensions.cs
- AuditableEntity
- Request
- RequestBodyLimitFilter.cs
- .SetConcurrency
- AuditLogEntry
- DummySmsGateway
- .SetRetryAfterHeader
- .HandleAsync
- DeviceRegistrationId
- .AddServices
- IdentitySchemeOptions
- KeycloakPermission
- EnrichLogsWithUserInfoMiddleware
- FirebaseServiceAccountOptions
- .AddBrevo
- .CreateProductTemplateAsync
- .MapEndpoint
- ResultTelemetryExtensions
- MassTransitInterModuleRequestClient
- ProblemDetailsExtensions.cs
- BackgroundJobsOptions
- JobStatus
- .EmailVerificationTokenValidation
- InventoryTelemetry
- Common.Application.Auth
- PublishOutcome
- .TryDeserialize
- .InvokeAsync
- StronglyTypedIdBinder.cs
- ProductsTelemetry
- .AddServices
- .AddCustomMassTransit
- .ConfigureConsumer
- HttpContextExtensions
- IAutoMigrateMarker.cs
- Request
- Common.InterModuleRequests/Setup.cs
- system_runtime_compilerservices
- .PhoneNumberValidation
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
6. `ApplicationUserId` - 71 edges
7. `Common.Domain.StronglyTypedIds` - 67 edges
8. `Common.Application.Auth` - 66 edges
9. `Common.Application.Extensions` - 62 edges
10. `Common.InterModuleRequests.Contracts` - 55 edges

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

## Communities (385 total, 91 thin omitted)

### Community 0 - "IStronglyTypedId"
Cohesion: 0.14
Nodes (13): StronglyTypedIdReadOnlyJsonConverter, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, StronglyTypedIdWriteOnlyJsonConverter, JsonSerializerOptions, Type (+5 more)

### Community 1 - "NotificationPayload"
Cohesion: 0.18
Nodes (13): Notifications.Application.Hubs, IHubContext, CancellationToken, IReadOnlyList, Task, INotificationDispatcher, Task, INotificationsClient (+5 more)

### Community 2 - "OutboxProcessor"
Cohesion: 0.19
Nodes (13): BackgroundService, IPublishEndpoint, PublishOutcome, CancellationToken, Exception, ILogger, IOptions, IServiceScopeFactory (+5 more)

### Community 3 - "Common.Application.Options"
Cohesion: 0.07
Nodes (32): Common.Infrastructure.Modules, Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Inventory.Endpoints, Common.Endpoints.Versioning, Outbox, Common.Application.BackgroundJobs, Common.Infrastructure.Persistence.Outbox (+24 more)

### Community 5 - "KeycloakAdminClient"
Cohesion: 0.17
Nodes (16): CancellationToken, Error, Func, HttpClient, HttpRequestMessage, HttpResponseMessage, IFusionCache, ILogger (+8 more)

### Community 6 - "RedisFixedWindowRateLimiter"
Cohesion: 0.13
Nodes (16): RateLimiter, RateLimiterStatistics, FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration, CancellationToken (+8 more)

### Community 7 - ".AddModuleDbContext"
Cohesion: 0.12
Nodes (14): SaveChangesInterceptor, ApplyAuditingInterceptor, TimeProvider, ApplySearchLanguageInterceptor, CancellationToken, DbContextEventData, InterceptionResult, ValueTask (+6 more)

### Community 8 - "FirebasePushGateway"
Cohesion: 0.17
Nodes (12): FirebaseApp, FirebaseMessaging, IDisposable, CancellationToken, Exception, IEnumerable, ILogger, IReadOnlyList (+4 more)

### Community 9 - ".RequireOtpTemplateForDefaultCulture"
Cohesion: 0.40
Nodes (4): Func, IConfiguration, IEnumerable, OtpTemplateConfiguration

### Community 10 - "Error"
Cohesion: 0.07
Nodes (22): IAM.Domain.Captcha, StringLocalizerExtensions, IStringLocalizer, Error, Key, ParameterName, StatusCode, SubErrors (+14 more)

### Community 11 - "microsoft_aspnetcore_http"
Cohesion: 0.05
Nodes (33): IAM.Endpoints.Captcha.VersionNeutral, Inventory.Endpoints.StockReservations.v1.Commit, IAM.Endpoints.Users.VersionNeutral.Search, Inventory.Endpoints.StockReservations.v1.ReserveSeries, Inventory.Infrastructure.InterModuleRequestHandlers, Inventory.Application.Persistence, Products.Endpoints.Probe, Products.Endpoints.Products (+25 more)

### Community 12 - "IKeycloakAdminClient"
Cohesion: 0.20
Nodes (8): CancellationToken, IReadOnlyList, Task, IKeycloakAdminClient, CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 13 - "ISearchLanguageResolver"
Cohesion: 0.12
Nodes (15): Configuration, ResxLocalizationOptions, DefaultCulture, SupportedCultures, ResxLocalizationOptionsValidator, ICollection, ISearchLanguageResolver, UniversalConfig (+7 more)

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
Cohesion: 0.05
Nodes (38): ProductId, V1ProductNameUpdatedDomainEvent, DefaultIdType, ProductTemplate, ProductTemplateId, Store, StoreId, Product (+30 more)

### Community 18 - "StockReservation"
Cohesion: 0.06
Nodes (29): DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationExpiredDomainEvent, DateTimeOffset, StockReservationId, V1StockReservationReleaseAttemptAbandonedDomainEvent, DateTimeOffset (+21 more)

### Community 19 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.05
Nodes (24): Products.Infrastructure.InterModuleRequestHandlers, Common.Domain.StronglyTypedIds, Products.Endpoints.Stores.v1.Search, Common.Domain.Events, Common.Application.Jobs, Products.Endpoints.ProductTemplates.v1.Search, Products.Endpoints.Products.v1.Search, Products.Domain.Products (+16 more)

### Community 20 - "LogProductCatalogChangeHandler"
Cohesion: 0.24
Nodes (8): CancellationToken, DefaultIdType, IFusionCache, ILogger, IOptions, LoggerMessage, Task, LogProductCatalogChangeHandler

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.13
Nodes (8): ReadOnlySpan, BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position, Stream

### Community 22 - "DeviceRegistration"
Cohesion: 0.17
Nodes (12): DateTimeOffset, Guid, DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive, LastReconciledOn (+4 more)

### Community 23 - "DummyEmailGateway"
Cohesion: 0.38
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, DummyEmailGateway

### Community 24 - "ICurrentUser"
Cohesion: 0.05
Nodes (34): ICurrentUser, Id, IdAsString, Roles, SessionId, ICollection, CancellationToken, Task (+26 more)

### Community 25 - "microsoft_extensions_options"
Cohesion: 0.17
Nodes (13): Common.Infrastructure.RateLimiting, Products.Infrastructure.RateLimiting, IAM.Endpoints, IAM.Infrastructure.Captcha.Services, IAM.Infrastructure.RateLimiting, IAM.Infrastructure.Captcha, microsoft_aspnetcore_ratelimiting, microsoft_extensions_options (+5 more)

### Community 26 - "IEvent"
Cohesion: 0.11
Nodes (14): DomainEventHandlerBase, CancellationToken, Task, IEventHandler, CancellationToken, Task, IEventHandlerWrapper, CancellationToken (+6 more)

### Community 27 - "RequestResponseBodyLoggingMiddleware"
Cohesion: 0.16
Nodes (11): IDiagnosticContext, SensitivePathRule, Methods, Path, IList, HttpContext, IList, IOptions (+3 more)

### Community 28 - "IDbContext"
Cohesion: 0.25
Nodes (7): DatabaseFacade, EntityEntry, IDbContext, AuditLog, ChangeTracker, Database, DbSet

### Community 29 - "Result"
Cohesion: 0.18
Nodes (10): Result, Error, IsFailure, Value, AsyncExtensions, SyncExtensions, Action, Func (+2 more)

### Community 30 - "JobRow"
Cohesion: 0.19
Nodes (9): JobRow, FailureKey, FinishedOn, Progress, QueuedOn, RequestedBy, StartedOn, Status (+1 more)

### Community 31 - "NotificationsModule"
Cohesion: 0.15
Nodes (10): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, NotificationsModule, ActivitySourceNames, MeterNames (+2 more)

### Community 32 - ".SingleAsResult"
Cohesion: 0.18
Nodes (10): CollectionExtensions, Func, ICollection, IEnumerable, PersistenceQueryableExtensions, CancellationToken, Expression, Func (+2 more)

### Community 33 - "Inventory.Domain.StockReservations.DomainEvents.v1"
Cohesion: 0.09
Nodes (17): Inventory.Domain.StockReservations.DomainEvents.v1, DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationCommitConflictDetectedDomainEvent, DateTimeOffset, DefaultIdType, StockReservationId (+9 more)

### Community 34 - "KeycloakPermissionClient"
Cohesion: 0.13
Nodes (20): CancellationToken, Dictionary, HttpClient, HttpResponseMessage, ILogger, IOptions, IReadOnlyList, List (+12 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.09
Nodes (20): IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, DateTimeOffset, OutboxMessage (+12 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.07
Nodes (24): CheckRegistrationRateLimitingPolicy, CreateStoreRateLimitingPolicy, EmailRateLimitingPolicy, OtpVerifyRateLimitingPolicy, RegisterRateLimitingPolicy, SmsRateLimitingPolicy, CustomRateLimitingOptions, CheckRegistration (+16 more)

### Community 37 - ".SendOtp"
Cohesion: 0.12
Nodes (16): OtpDispatchErrors, SendPhoneOtpRequest, SendPhoneOtpResponse, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, CancellationToken (+8 more)

### Community 38 - "RequestBody"
Cohesion: 0.06
Nodes (32): IAM.Endpoints.Tokens.VersionNeutral.Refresh, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RefreshToken, RequestValidator (+24 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.07
Nodes (25): IHostBuilder, LoggerConfiguration, LoggerMinimumLevelConfiguration, ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics (+17 more)

### Community 40 - "KeycloakPermissionAuthorizationHandler.cs"
Cohesion: 0.15
Nodes (12): asp_versioning_apiexplorer, IAM.Infrastructure.Auth, Host.Swagger, microsoft_aspnetcore_authentication, microsoft_aspnetcore_authentication_jwtbearer, microsoft_aspnetcore_authorization, microsoft_aspnetcore_mvc_apiexplorer, microsoft_identitymodel_tokens (+4 more)

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
Cohesion: 0.20
Nodes (10): OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, VerifyPhoneOtpResponseExtensions, CancellationToken (+2 more)

### Community 46 - "PaginationQueryableExtensions"
Cohesion: 0.08
Nodes (27): BinaryExpression, ExpressionVisitor, KeyedRow, MemberExpression, MethodInfo, ParameterExpression, Payload, PaginationCursor (+19 more)

### Community 48 - "Common.Application.EventBus"
Cohesion: 0.17
Nodes (8): Common.Application.Persistence.Inbox, Inventory.Application.IntegrationEventHandlers, Common.IntegrationEvents, Products.Application.Stores.DomainEventHandlers.v1, Common.Application.EventBus, entityframework_exceptions_common, masstransit, ziggycreatures_caching_fusion

### Community 50 - "Response"
Cohesion: 0.12
Nodes (16): CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate (+8 more)

### Community 51 - ".VerifyOtp"
Cohesion: 0.14
Nodes (14): IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, IssueVerificationTokenRequest, IssueVerificationTokenResponse, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response (+6 more)

### Community 52 - "ReCaptchaService"
Cohesion: 0.21
Nodes (10): FormUrlEncodedContent, ReCaptchaResponse, CancellationToken, Exception, HttpClient, ILogger, IOptions, LoggerMessage (+2 more)

### Community 53 - "Request"
Cohesion: 0.15
Nodes (13): Guid, Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FirstName (+5 more)

### Community 54 - ".SendOtp"
Cohesion: 0.11
Nodes (18): EmailOtpDispatchErrors, EmailOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, SendEmailOtpRequest, SendEmailOtpResponse, CancellationToken (+10 more)

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
Cohesion: 0.12
Nodes (16): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Request, Brand (+8 more)

### Community 61 - "KeycloakTokenClient"
Cohesion: 0.15
Nodes (18): JsonWebTokenHandler, CancellationToken, Task, IKeycloakTokenClient, DateTimeOffset, KeycloakTokens, CancellationToken, Dictionary (+10 more)

### Community 62 - "JobHousekeepingJob"
Cohesion: 0.14
Nodes (14): Deleted, JobHousekeepingJob, CancellationToken, ILogger, IOptions, LoggerMessage, Task, TimeProvider (+6 more)

### Community 63 - "RequestLoggingOptions"
Cohesion: 0.13
Nodes (16): IPostConfigureOptions, RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes (+8 more)

### Community 64 - ".SendCoreAsync"
Cohesion: 0.19
Nodes (9): SendErrorBody, SendResponseBody, CancellationToken, HttpResponseMessage, SendRequestBody, Task, SendContact, Email (+1 more)

### Community 65 - "BackgroundJobsService"
Cohesion: 0.23
Nodes (8): IBackgroundJobClientV2, BackgroundJobsService, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 66 - "OutboxModule"
Cohesion: 0.14
Nodes (16): Action, Exception, IApplicationBuilder, IEndpointRouteBuilder, IEnumerable, IHostApplicationLifetime, ILogger, ILoggerFactory (+8 more)

### Community 67 - ".AddNotificationsSignalR"
Cohesion: 0.12
Nodes (15): HubConnectionContext, IUserIdProvider, RedisOptions, SignalROptions, RedisConnectionString, RequireRedisBackplaneInProduction, UseRedisBackplane, SignalROptionsValidator (+7 more)

### Community 68 - "ValueObject"
Cohesion: 0.25
Nodes (4): Common.Domain, IComparable, ValueObject, IEnumerable

### Community 69 - ".HandleAsync"
Cohesion: 0.14
Nodes (14): GetSeedUserIdsRequest, GetSeedUserIdsResponse, ICollection, CancellationToken, Task, GetSeedUserIdsRequestHandler, CancellationToken, IResult (+6 more)

### Community 70 - "IntegrationEventHandlerBase"
Cohesion: 0.24
Nodes (11): IntegrationEventHandlerBase, MaxEventAge, CancellationToken, ConsumeContext, DefaultIdType, IFusionCache, ILogger, IOptions (+3 more)

### Community 71 - "FullTextSearchOptions"
Cohesion: 0.08
Nodes (27): Add a new language/culture, Add search to a new entity _(Build checklist)_, Change the vector (weights, fields, or config), Extending and maintaining, File map _(Build)_, Full-Text Search, Gotchas, How it works (+19 more)

### Community 72 - "Response"
Cohesion: 0.17
Nodes (11): CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, Id, RequestValidator, Response (+3 more)

### Community 73 - ".WriteAsync"
Cohesion: 0.33
Nodes (5): Memory, ReadOnlyMemory, CancellationToken, Task, ValueTask

### Community 75 - "IntegrationEvent"
Cohesion: 0.20
Nodes (9): IReadOnlyList, IntegrationEvent, CreatedOn, Id, DateTimeOffset, DefaultIdType, ProductCreatedIntegrationEvent, StoreCreatedIntegrationEvent (+1 more)

### Community 76 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 77 - ".RemoveMyProductAsync"
Cohesion: 0.22
Nodes (7): CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, Id, RequestValidator

### Community 78 - "CheckRegistrationRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, CheckRegistrationRateLimitingPolicy (+1 more)

### Community 79 - "DomainEvent"
Cohesion: 0.08
Nodes (22): Products.Domain.Products.DomainEvents.v1, Products.Application.Products.DomainEventHandlers.v1, DomainEvent, CreatedOn, Id, Version, DateTimeOffset, DefaultIdType (+14 more)

### Community 80 - "IAuditableEntity"
Cohesion: 0.18
Nodes (10): IAuditableEntity, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, DateTimeOffset, CancellationToken, DbContextEventData (+2 more)

### Community 81 - ".SearchStoresAsync"
Cohesion: 0.11
Nodes (17): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Request, Address (+9 more)

### Community 82 - "Split-Deployment PoC"
Cohesion: 0.18
Nodes (9): Concurrent safety, Cross-process call path, Files added by this PoC, How it works, How to run, Split-Deployment PoC, What this proves, README (Boilerplate Overview) (+1 more)

### Community 83 - "microsoft_entityframeworkcore"
Cohesion: 0.09
Nodes (26): Common.Infrastructure.Persistence.Inbox, Common.Application.Search, Common.Application.Persistence, Inventory.Infrastructure.Persistence.EntityConfigurations, Common.Infrastructure.Persistence.Extensions, Notifications.Application.Persistence, Notifications.Domain.Devices, Notifications.Infrastructure.InterModuleRequestHandlers (+18 more)

### Community 84 - ".Configure"
Cohesion: 0.11
Nodes (18): AuditableEntityConfiguration, EntityTypeBuilder, EntityTypeBuilder, JobRowConfiguration, EntityTypeBuilder, DomainEventConverter, JsonSerializerOptions, StronglyTypedIdValueConverter (+10 more)

### Community 85 - "CaptchaOptions"
Cohesion: 0.11
Nodes (17): CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey, Provider, ScoreThreshold, SecretKey (+9 more)

### Community 86 - "Store"
Cohesion: 0.09
Nodes (19): StoreId, V1StoreAddressUpdatedDomainEvent, StoreId, V1StoreDescriptionUpdatedDomainEvent, StoreId, V1StoreNameUpdatedDomainEvent, IReadOnlyCollection, List (+11 more)

### Community 87 - "PushOptions"
Cohesion: 0.19
Nodes (13): PushOptions, Provider, SendTimeoutSeconds, ServiceAccount, Templates, PushOptionsValidator, PushProvider, Dummy (+5 more)

### Community 89 - "Setup"
Cohesion: 0.33
Nodes (4): ConfigurationManager, Host.Configurations, Setup, WebApplicationBuilder

### Community 90 - ".GetProductAsync"
Cohesion: 0.25
Nodes (10): GetStockLevelRequest, GetStockLevelResponse, DefaultIdType, CancellationToken, Task, GetStockLevelRequestHandler, CancellationToken, DefaultIdType (+2 more)

### Community 91 - "Seeder"
Cohesion: 0.13
Nodes (15): Products.Infrastructure.Persistence.Seeding, CancellationToken, ILogger, LoggerMessage, ProductsDbContext, Task, CancellationToken, List (+7 more)

### Community 92 - "Infrastructure/Setup.cs"
Cohesion: 0.12
Nodes (14): Common.Infrastructure.Localization, Inventory.Endpoints.StockReservations.v1.WebhookCallback, Common.Application.JsonConverters, Common.Infrastructure.FeatureManagement, microsoft_aspnetcore_http_json, microsoft_aspnetcore_localization, microsoft_entityframeworkcore_storage_valueconversion, resxgenerator_registration (+6 more)

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
Cohesion: 0.30
Nodes (8): DatabaseSeederOrchestrator, CancellationToken, Exception, IHostApplicationLifetime, ILogger, IServiceScopeFactory, LoggerMessage, Task

### Community 97 - "StockReservationExpirySweepService"
Cohesion: 0.18
Nodes (11): IServiceCollection, Setup, CancellationToken, Exception, Guid, ILogger, IOptions, LoggerMessage (+3 more)

### Community 98 - ".SearchStoreProductsAsync"
Cohesion: 0.17
Nodes (11): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Description (+3 more)

### Community 99 - "AuditLogRetentionJobRegistrar"
Cohesion: 0.24
Nodes (8): AuditLogRetentionJobRegistrar, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task, Setup, IServiceCollection

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

### Community 104 - "AuditableEntityResponse"
Cohesion: 0.11
Nodes (16): AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, DateTimeOffset, RouteGroupBuilder (+8 more)

### Community 105 - ".RegisterAsync"
Cohesion: 0.11
Nodes (14): IAM.Endpoints.Users.VersionNeutral.SelfRegisterByEmail, EmailNormalization, CancellationToken, IFeatureManager, ILogger, RouteGroupBuilder, Task, Endpoint (+6 more)

### Community 106 - "Common.Domain.ResultMonad"
Cohesion: 0.08
Nodes (29): IAM.Endpoints.Users, IAM.Endpoints.Otp.VersionNeutral.SendForLogin, Common.Application.FeatureManagement, IAM.Endpoints.Otp, Products.Endpoints.Probe.v1, Common.InterModuleRequests.IAM, Common.InterModuleRequests.Contracts, Common.Infrastructure.Extensions (+21 more)

### Community 107 - "HttpWarehouseGateway"
Cohesion: 0.31
Nodes (8): ReleaseResponseBody, CancellationToken, HttpClient, HttpResponseMessage, Task, HttpWarehouseGateway, ReleaseRequestBody, ReleaseResponseBody

### Community 108 - "StockLevel"
Cohesion: 0.18
Nodes (10): DefaultIdType, StockLevelId, V1StockLevelCreatedDomainEvent, DefaultIdType, StockLevel, ProductId, QuantityOnHand, StockLevelId (+2 more)

### Community 109 - ".IsRegisteredAsync"
Cohesion: 0.17
Nodes (10): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, PhoneNumber, RequestValidator (+2 more)

### Community 110 - "KeycloakPermissionPolicyProvider"
Cohesion: 0.24
Nodes (8): AuthorizationOptions, AuthorizationPolicy, DefaultAuthorizationPolicyProvider, IAuthorizationPolicyProvider, ConcurrentDictionary, IOptions, Task, KeycloakPermissionPolicyProvider

### Community 111 - "InventoryOptions"
Cohesion: 0.10
Nodes (19): InventoryOptions, MaxOccurrencesPerSeries, SafetyBufferQuantity, StockReservationExpirySweepBatchSize, StockReservationExpirySweepCron, WarehouseGatewayBaseUrl, WarehouseGatewayProvider, WebhookMaxBodyBytes (+11 more)

### Community 112 - "OutboxCleanupJob"
Cohesion: 0.27
Nodes (8): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxCleanupJob

### Community 113 - "ProductsDbContext"
Cohesion: 0.13
Nodes (14): DbContextOptions, DbSet, ILogger, ProductTemplate, Store, TimeProvider, ProductsDbContext, Products (+6 more)

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
Cohesion: 0.08
Nodes (22): ReservationStatus, Active, Committed, Expired, Released, RequiresReconciliation, CancellationToken, RouteGroupBuilder (+14 more)

### Community 118 - "IModule"
Cohesion: 0.09
Nodes (20): OpenTelemetryBuilder, ResourceBuilder, IModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority (+12 more)

### Community 119 - "ProductsModule"
Cohesion: 0.12
Nodes (13): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, ProductsModule (+5 more)

### Community 120 - ".HandleWarehouseWebhookAsync"
Cohesion: 0.16
Nodes (12): IHeaderDictionary, IValidator, HmacSignatureVerifier, ReadOnlySpan, CancellationToken, HttpContext, IOptions, JsonSerializerOptions (+4 more)

### Community 121 - ".GetVariantAsync"
Cohesion: 0.40
Nodes (4): IVariantFeatureManager, IVariantFeatureManagerExtensions, CancellationToken, Task

### Community 122 - "OtpVerifyRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, OtpVerifyRateLimitingPolicy (+1 more)

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

### Community 127 - "StoreId"
Cohesion: 0.11
Nodes (16): StoreId, DefaultIdType, StoreId, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request (+8 more)

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
Cohesion: 0.14
Nodes (15): HostOptions, JsonOptions, CorsOptions, AllowCredentials, AllowedHeaders, AllowedMethods, AllowedOrigins, MaxAgeInSeconds (+7 more)

### Community 132 - "Request"
Cohesion: 0.13
Nodes (13): CancellationToken, RouteGroupBuilder, Task, Endpoint, RequestBody, Request, Body, Id (+5 more)

### Community 134 - "SendRequestBody"
Cohesion: 0.25
Nodes (8): SendContact, IReadOnlyList, SendRequestBody, HtmlContent, Sender, Subject, TextContent, To

### Community 135 - "IInterModuleRequest"
Cohesion: 0.09
Nodes (23): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, IInterModuleRequest, DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, IReadOnlyList, CancellationToken, IReadOnlyCollection (+15 more)

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
Cohesion: 0.15
Nodes (11): IReadOnlyList, List, ProductTemplate, Brand, Color, IsActive, Model, Products (+3 more)

### Community 140 - "ProcessedMessage"
Cohesion: 0.13
Nodes (16): ProcessedMessage, ConsumerName, MessageId, ProcessedOn, DateTimeOffset, DefaultIdType, UniqueConstraintException, InboxStore (+8 more)

### Community 141 - "system_net"
Cohesion: 0.11
Nodes (14): IAM.Infrastructure.Keycloak.Representations, Inventory.Infrastructure.Gateway, Common.Infrastructure.Resiliency, Inventory.Application.Gateway, IAM.Infrastructure.Keycloak, microsoft_extensions_http_resilience, polly, polly_ratelimiting (+6 more)

### Community 142 - "TokenRefreshRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenRefreshRateLimitingPolicy (+1 more)

### Community 143 - "Setup"
Cohesion: 0.09
Nodes (22): LoadAll, microsoft_aspnetcore_httpoverrides, ModuleRegistry, Names, ModulesOptions, EnabledModules, ModulesOptionsValidator, IReadOnlyList (+14 more)

### Community 144 - "Response"
Cohesion: 0.10
Nodes (19): CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, Id, RequestValidator, DateOnly (+11 more)

### Community 145 - "Request"
Cohesion: 0.17
Nodes (12): ProductTemplateId, RequestBody, Request, Body, Id, RequestBody, Description, Name (+4 more)

### Community 146 - ".RegisterAsync"
Cohesion: 0.10
Nodes (23): CancellationToken, Task, BindDeviceSessionRequest, BindDeviceSessionResponse, Guid, CancellationToken, Exception, Guid (+15 more)

### Community 147 - ".AddOrUpdate"
Cohesion: 0.20
Nodes (8): Action, Expression, Func, Task, Action, Expression, Func, Task

### Community 148 - "IInterModuleRequestClient"
Cohesion: 0.11
Nodes (20): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, IInterModuleRequestClient, VerifyEmailOtpRequest, VerifyEmailOtpResponse, CancellationToken, Exception, ILogger, LoggerMessage (+12 more)

### Community 149 - "TokenResponseRepresentation"
Cohesion: 0.33
Nodes (6): TokenResponseRepresentation, AccessToken, ExpiresIn, RefreshExpiresIn, RefreshToken, SessionState

### Community 150 - "StockReservationExpirySweepJobRegistrar"
Cohesion: 0.27
Nodes (8): IHostedService, CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task, StockReservationExpirySweepJobRegistrar

### Community 151 - "OutboxOptions"
Cohesion: 0.10
Nodes (21): OutboxCleanupSettings, BatchSize, CronSchedule, Enabled, RetentionDays, OutboxCleanupSettingsValidator, OutboxOptions, BaseBackoffSeconds (+13 more)

### Community 152 - ".EnsureNoMigrationsPending"
Cohesion: 0.54
Nodes (4): MigrationGuard, ILogger, IServiceProvider, LoggerMessage

### Community 153 - "OpenApiOptions"
Cohesion: 0.05
Nodes (38): ApiVersionDescription, IApiVersionDescriptionProvider, IConfigureOptions, IOpenApiSchema, IOperationFilter, ISchemaFilter, JsonValue, OpenApiInfo (+30 more)

### Community 154 - "Setup.Logger.cs"
Cohesion: 0.13
Nodes (13): Host, Host.Middlewares, Host.Infrastructure, elastic_serilog_sinks, serilog, serilog_configuration, serilog_enrichers_span, serilog_events (+5 more)

### Community 155 - "ISmsGateway"
Cohesion: 0.13
Nodes (13): CancellationToken, Task, ISmsGateway, SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, SmsMessage (+5 more)

### Community 156 - "StrictDateTimeOffsetJsonConverter"
Cohesion: 0.10
Nodes (19): JsonConverter, StrictDateTimeOffsetJsonConverter, DateTimeOffset, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, StronglyTypedIdListReadOnlyJsonConverter (+11 more)

### Community 157 - "CreateStockLevelOnProductCreatedHandler"
Cohesion: 0.24
Nodes (8): CancellationToken, DefaultIdType, IFusionCache, ILogger, IOptions, LoggerMessage, Task, CreateStockLevelOnProductCreatedHandler

### Community 158 - "OtpOptions"
Cohesion: 0.08
Nodes (24): OtpOptions, DummyCode, EmailQuotaWindowMinutes, ExpirationInMinutes, Length, MaxSendsPerEmailPerWindow, MaxSendsPerPhonePerWindow, PhoneQuotaWindowMinutes (+16 more)

### Community 159 - "IInventoryDbContext"
Cohesion: 0.09
Nodes (21): DbSet, IInventoryDbContext, StockLevels, StockReservations, CancellationToken, RouteGroupBuilder, Task, Endpoint (+13 more)

### Community 160 - "OtpServiceBase"
Cohesion: 0.21
Nodes (8): OtpCacheEntry, DateTimeOffset, CancellationToken, IFusionCache, SemaphoreSlim, Task, TimeSpan, OtpServiceBase

### Community 161 - "IntegrationEventOutbox"
Cohesion: 0.14
Nodes (13): IIntegrationEventOutbox, IntegrationEventOutbox, HasPending, List, Lock, Setup, IServiceCollection, CancellationToken (+5 more)

### Community 162 - "IEmailGateway"
Cohesion: 0.19
Nodes (9): CancellationToken, Task, EmailMessage, IEmailGateway, CancellationToken, IFusionCache, IOptions, Task (+1 more)

### Community 164 - "NotificationsDbContext"
Cohesion: 0.18
Nodes (10): DbContextOptions, DbSet, ILogger, TimeProvider, NotificationsDbContext, DeviceRegistrations, IApplicationBuilder, ILoggerFactory (+2 more)

### Community 165 - "CurrentUser"
Cohesion: 0.12
Nodes (14): Common.Infrastructure.Auth.Services, Common.Infrastructure.Auth, CurrentUser, Id, IdAsString, IsAuthenticated, Principal, Roles (+6 more)

### Community 166 - "IamModule"
Cohesion: 0.18
Nodes (10): Action, IApplicationBuilder, IEnumerable, RateLimiterOptions, IamModule, ActivitySourceNames, MeterNames, Name (+2 more)

### Community 167 - "PaginationRequest"
Cohesion: 0.06
Nodes (34): Products.Endpoints.Stores.v1.My.AuditLog, PaginationRequest, After, IncludeTotal, PageNumber, PageSize, ShouldIncludeTotal, Skip (+26 more)

### Community 168 - "Response"
Cohesion: 0.22
Nodes (6): Products.Endpoints.ProductTemplates.v1.Create, RouteGroupBuilder, Setup, RouteGroupBuilder, Response, Id

### Community 169 - "SendSecurityAlertRequestHandler"
Cohesion: 0.27
Nodes (9): SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, CancellationToken, IOptions, RequestLocalizationOptions, Task (+1 more)

### Community 171 - "ICaptchaService"
Cohesion: 0.19
Nodes (8): CancellationToken, Task, ICaptchaService, CancellationToken, IFusionCache, IOptions, Task, CachedCaptchaService

### Community 172 - "Stores/v1/Create/Endpoint.cs"
Cohesion: 0.14
Nodes (10): Products.Endpoints.Stores.v1.Create, Products.Endpoints.Stores, Products.Infrastructure.Telemetry, Products.Endpoints.Stores.v1.RemoveProduct, RouteGroupBuilder, Setup, RouteGroupBuilder, Endpoint (+2 more)

### Community 173 - "ProductTemplateId"
Cohesion: 0.25
Nodes (6): ProductTemplateId, DefaultIdType, ProductTemplateId, CancellationToken, List, Task

### Community 174 - "system_diagnostics"
Cohesion: 0.10
Nodes (15): BackgroundJobs.Telemetry, BackgroundJobs, hangfire, hangfire_annotations, hangfire_dashboard, hangfire_postgresql, hangfire_server, opentelemetry_metrics (+7 more)

### Community 175 - "PaginationResponse"
Cohesion: 0.06
Nodes (31): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Products.v1.AuditLog, JsonElement, AuditLogDto, DateTimeOffset, PaginationResponse, HasNext, HasPrevious (+23 more)

### Community 176 - ".FixedWindow"
Cohesion: 0.18
Nodes (12): CustomRateLimitingOptionsValidator, FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit, FixedWindowValidator, RateLimitPartitions (+4 more)

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
Cohesion: 0.15
Nodes (13): RabbitMqOptions, DefaultConcurrentMessageLimit, DefaultPrefetchCount, Host, Password, Port, RetryIntervalDeltaMs, RetryLimit (+5 more)

### Community 183 - "SendRequestBody"
Cohesion: 0.25
Nodes (8): SendMessageBody, IReadOnlyList, SendRequestBody, AppName, Encoding, IysFilter, Messages, MsgHeader

### Community 184 - ".AssignBasicRoleOrRollbackAsync"
Cohesion: 0.14
Nodes (10): CancellationToken, Exception, ILogger, LoggerMessage, Task, RegistrationCompletion, ActivitySource, Counter (+2 more)

### Community 185 - "IdentityScheme"
Cohesion: 0.33
Nodes (5): IdentityScheme, Email, PhoneNumber, RouteGroupBuilder, Setup

### Community 186 - ".Get"
Cohesion: 0.50
Nodes (3): Action, IEnumerable, RateLimiterOptions

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
Cohesion: 0.11
Nodes (11): KeycloakPaths, CancellationToken, DateTimeOffset, SemaphoreSlim, Task, TimeSpan, ServiceAccountTokenCache, AccessToken (+3 more)

### Community 191 - ".AddProductToMyStoreAsync"
Cohesion: 0.11
Nodes (17): Products.Endpoints.Stores.v1.My.AddProduct, CancellationToken, ProductTemplate, RouteGroupBuilder, Store, Task, Endpoint, ProductTemplateId (+9 more)

### Community 192 - "ReCaptchaResponse"
Cohesion: 0.25
Nodes (7): DateTime, ReCaptchaResponse, ChallengeTs, ErrorCodes, Hostname, Score, Success

### Community 193 - ".UpdateMyStoreAsync"
Cohesion: 0.17
Nodes (10): Products.Endpoints.Stores.v1.My.Update, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, Address, Description (+2 more)

### Community 194 - ".RaiseEvent"
Cohesion: 0.15
Nodes (11): AggregateRoot, Events, Id, Version, IReadOnlyCollection, List, IAggregateRoot, Events (+3 more)

### Community 195 - "InventoryOptions.cs"
Cohesion: 0.50
Nodes (3): WarehouseGatewayProvider, Dummy, Http

### Community 196 - "TokenCreateRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenCreateRateLimitingPolicy (+1 more)

### Community 197 - ".Failure"
Cohesion: 0.16
Nodes (7): SearchValues, StringExtensions, Success, Func, Task, CancellationToken, Task

### Community 198 - "InventoryModule"
Cohesion: 0.15
Nodes (10): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, InventoryModule, ActivitySourceNames, MeterNames (+2 more)

### Community 199 - ".AddKeycloakInfrastructure"
Cohesion: 0.16
Nodes (10): CancellationToken, Task, IServiceAccountTokenProvider, HttpClient, IOptions, TimeProvider, ServiceAccountTokenProvider, IOptions (+2 more)

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

### Community 205 - "StatelessInboxStore"
Cohesion: 0.24
Nodes (6): StatelessInboxStore, Instance, CancellationToken, DateTimeOffset, DefaultIdType, Task

### Community 210 - "IDatabaseSeeder"
Cohesion: 0.17
Nodes (9): IDatabaseSeeder, Priority, CancellationToken, Task, CancellationToken, IServiceScopeFactory, Task, ProductsDatabaseSeeder (+1 more)

### Community 211 - "SeedingCompletionTracker"
Cohesion: 0.15
Nodes (8): SeedingCompletionTracker, CancellationToken, Exception, Task, Setup, IOptions, IServiceCollection, TaskCompletionSource

### Community 212 - "Versioning/Setup.cs"
Cohesion: 0.20
Nodes (7): ApiVersionSet, asp_versioning, asp_versioning_builder, asp_versioning_conventions, Setup, IEndpointRouteBuilder, IServiceCollection

### Community 213 - "BackgroundJobsTelemetry"
Cohesion: 0.14
Nodes (11): IServerFilter, PerformedContext, PerformingContext, JobMetricsFilter, BackgroundJobsTelemetry, ActivitySource, ConcurrentDictionary, Counter (+3 more)

### Community 214 - "fluentvalidation"
Cohesion: 0.07
Nodes (16): common_application_localization_resources, IAM.Domain.Users, Common.Domain.Extensions, Common.Application.ModelBinders, Common.Application.Validation, Common.Domain.Devices, IAM.Endpoints.Common.Validations, fluentvalidation (+8 more)

### Community 215 - "IProductsDbContext"
Cohesion: 0.09
Nodes (21): Products.Endpoints.Stores.v1.My.Create, DbSet, Store, IProductsDbContext, Products, ProductTemplates, Stores, CancellationToken (+13 more)

### Community 216 - ".UpsertIfNewerAsync"
Cohesion: 0.20
Nodes (9): ICurrentDbContext, ProjectionUpsertExtensions, Action, CancellationToken, DbContext, DbSet, Func, Task (+1 more)

### Community 217 - "FeatureFlags"
Cohesion: 0.33
Nodes (5): Checkout, FeatureFlags, IAM, Notifications, Products

### Community 218 - ".SendOtp"
Cohesion: 0.18
Nodes (9): CancellationToken, IFeatureManager, RouteGroupBuilder, Task, Endpoint, Request, CaptchaToken, PhoneNumber (+1 more)

### Community 219 - "IBackgroundJobs"
Cohesion: 0.23
Nodes (7): IBackgroundJobs, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 220 - ".ActivateProductTemplateAsync"
Cohesion: 0.22
Nodes (7): CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, Id, RequestValidator

### Community 221 - "Keycloak realm as code"
Cohesion: 0.25
Nodes (7): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Secrets, Seed users (dev / test only)

### Community 222 - "Key decisions"
Cohesion: 0.29
Nodes (7): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Key decisions

### Community 223 - "FirebasePushGateway.cs"
Cohesion: 0.24
Nodes (6): Notifications.Application.Push, Notifications.Infrastructure.Push, Notifications.Infrastructure.Push.Firebase, firebaseadmin, firebaseadmin_messaging, google_apis_auth_oauth2

### Community 224 - ".MapEndpoint"
Cohesion: 0.40
Nodes (3): RouteGroupBuilder, RouteGroupBuilder, Setup

### Community 225 - "IInboxStore"
Cohesion: 0.24
Nodes (7): IInboxCleanupTarget, ModuleName, IInboxStore, CancellationToken, DateTimeOffset, DefaultIdType, Task

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

### Community 232 - "ApplicationUserId"
Cohesion: 0.22
Nodes (5): ApplicationUserId, IsEmpty, Value, DefaultIdType, NotificationGroupName

### Community 233 - ".HasPatternIndex"
Cohesion: 0.36
Nodes (6): IndexBuilder, TrigramIndexExtensions, EntityTypeBuilder, Expression, Func, ModelBuilder

### Community 234 - ".ReleaseStockReservationAsync"
Cohesion: 0.20
Nodes (8): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint, Request, Id, RequestValidator

### Community 235 - "BaseDbContext"
Cohesion: 0.22
Nodes (8): BaseDbContext, AuditLog, CancellationToken, DbContextOptions, DbSet, ILogger, Task, TimeProvider

### Community 236 - "IOtpService"
Cohesion: 0.13
Nodes (14): CancellationToken, Task, TimeSpan, IOtpService, OtpVerificationOutcome, InvalidOtp, Success, TooManyAttempts (+6 more)

### Community 237 - ".RemoveProductAsync"
Cohesion: 0.17
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, ProductId, StoreId, Request, Id (+2 more)

### Community 238 - "SecurityHeadersMiddleware"
Cohesion: 0.20
Nodes (9): SecurityHeadersOptions, Headers, SecurityHeadersOptionsValidator, Dictionary, HttpContext, IOptions, RequestDelegate, Task (+1 more)

### Community 239 - "microsoft_extensions_configuration"
Cohesion: 0.07
Nodes (20): Notifications.Application.Otp, Notifications.Application.Sms, Notifications.Infrastructure.Devices, Notifications.Infrastructure.Email, Common.Application.Caching, Notifications.Infrastructure.Sms, Notifications.Infrastructure.Email.Brevo, Notifications.Infrastructure.Telemetry (+12 more)

### Community 240 - "InterModuleRequestHandlerDefinition"
Cohesion: 0.29
Nodes (5): ConsumerDefinition, Type, InterModuleRequestHandlerDefinition, RequestType, IOptions

### Community 241 - "Request"
Cohesion: 0.20
Nodes (10): Guid, Request, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken, Password (+2 more)

### Community 242 - ".ReserveSeriesAsync"
Cohesion: 0.22
Nodes (10): GetProductRequest, GetProductResponse, DefaultIdType, CancellationToken, IOptions, Task, TimeProvider, CancellationToken (+2 more)

### Community 243 - "CustomValidator"
Cohesion: 0.12
Nodes (22): CustomValidator, Request, Email, Otp, RequestValidator, Request, Id, RequestValidator (+14 more)

### Community 244 - "KeyedResilienceProfile"
Cohesion: 0.22
Nodes (9): KeyedResilienceProfile, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, RateLimitPermits, RateLimitQueueLimit (+1 more)

### Community 245 - "SendForEmail/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForEmail, Request, CaptchaToken, Email, RequestValidator

### Community 246 - "UtcDateTimeOffsetConverter"
Cohesion: 0.22
Nodes (6): DateTimeOffset, ModelConfigurationBuilder, UtcDateTimeOffsetConverter, DateTimeOffset, DateTimeOffset, ModelConfigurationBuilder

### Community 247 - "AuditLogEntryConfiguration"
Cohesion: 0.28
Nodes (5): ModelBuilder, AuditLogEntryConfiguration, ModelBuilder, ModelBuilder, ModelBuilder

### Community 248 - "SendErrorBody"
Cohesion: 0.67
Nodes (3): SendErrorBody, Code, Message

### Community 249 - "GetActiveSessionIdsRequest"
Cohesion: 0.44
Nodes (7): GetActiveSessionIdsRequest, GetActiveSessionIdsResponse, UserSessionIds, IReadOnlyList, CancellationToken, Task, GetActiveSessionIdsRequestHandler

### Community 250 - "Response"
Cohesion: 0.15
Nodes (12): CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, Id, RequestValidator, Response (+4 more)

### Community 251 - "Request"
Cohesion: 0.20
Nodes (10): RequestBody, Request, Body, Id, RequestBody, Description, Name, Price (+2 more)

### Community 252 - ".TryWriteAsync"
Cohesion: 0.40
Nodes (4): ProblemDetailsContext, ProblemDetailsServiceExtensions, IProblemDetailsService, Task

### Community 260 - "RequestBodyLimitMetadata"
Cohesion: 0.18
Nodes (10): IAuthenticationSchemeProvider, RequestBodyLimitMetadata, MaxBodyBytes, RequestBodyLimitMiddleware, Func, HttpContext, IHttpMaxRequestBodySizeFeature, RequestDelegate (+2 more)

### Community 261 - ".CommitStockReservationAsync"
Cohesion: 0.15
Nodes (11): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint, Request, Body, Id (+3 more)

### Community 262 - ".SendWithPipelineAsync"
Cohesion: 0.25
Nodes (7): HttpClientKeyedExtensions, CancellationToken, HttpClient, HttpRequestMessage, HttpResponseMessage, ResiliencePipeline, Task

### Community 263 - "Response"
Cohesion: 0.25
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegister, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 264 - "V1StoreCreatedDomainEvent"
Cohesion: 0.36
Nodes (7): CancellationToken, Task, SimulateSomeBusinessHandler, StoreCreatedIntegrationEventPublishingHandler, V1StoreCreatedDomainEventHandlers, StoreId, V1StoreCreatedDomainEvent

### Community 265 - "BackgroundJobsModule"
Cohesion: 0.25
Nodes (7): BackgroundJobsModule, ActivitySourceNames, MeterNames, Name, StartupPriority, IEndpointRouteBuilder, IEnumerable

### Community 266 - "KeycloakUser"
Cohesion: 0.33
Nodes (8): DateOnly, DateTimeOffset, IReadOnlyList, CreateKeycloakUser, GrantedPermission, KeycloakUser, KeycloakUserPage, KeycloakUserSession

### Community 267 - "AuditLogOptions"
Cohesion: 0.33
Nodes (6): AuditLogOptions, PerSchemaRetentionDays, PurgeBatchSize, RetentionDays, AuditLogOptionsValidator, Dictionary

### Community 268 - ".DeactivateProductTemplateAsync"
Cohesion: 0.22
Nodes (7): CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, Id, RequestValidator

### Community 269 - ".DeactivateStoreAsync"
Cohesion: 0.22
Nodes (7): CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, Id, RequestValidator

### Community 270 - "IntegrationEventConverter"
Cohesion: 0.25
Nodes (6): IEntityTypeConfiguration, IntegrationEventConverter, JsonSerializerOptions, ModelBuilder, EntityTypeBuilder, OutboxMessageConfig

### Community 271 - "ResiliencyOptions.cs"
Cohesion: 0.40
Nodes (4): AbstractValidator, KeyedResilienceProfileValidator, ResiliencyOptionsValidator, KeyValuePair

### Community 272 - ".AddResilientHttpClient"
Cohesion: 0.22
Nodes (8): HttpStandardResilienceOptions, IHttpClientBuilder, Setup, Action, HttpClient, IOptions, IServiceCollection, IServiceProvider

### Community 273 - ".AddNetGsm"
Cohesion: 0.33
Nodes (5): IConfiguration, IFusionCache, IOptions, IServiceCollection, Setup

### Community 274 - "DeviceRegistryReconciliationService"
Cohesion: 0.08
Nodes (23): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator, IReadOnlyCollection, CancellationToken, ILogger (+15 more)

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

### Community 280 - "InterModuleRequestOptions"
Cohesion: 0.25
Nodes (8): InterModuleRequestOptions, DependencyUnavailableRetryAfterSeconds, HandlerConcurrentMessageLimit, HandlerPrefetchCount, Timeouts, TimeoutSeconds, InterModuleRequestOptionsValidator, Dictionary

### Community 281 - ".TryReadFromJsonAsync"
Cohesion: 0.40
Nodes (4): HttpContent, CancellationToken, Task, HttpContentJsonExtensions

### Community 282 - "Infrastructure/StringExtensions.cs"
Cohesion: 0.40
Nodes (3): opentelemetry_exporter, OtlpExportProtocol, StringExtensions

### Community 283 - "AuditableEntity"
Cohesion: 0.25
Nodes (8): AuditableEntity, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, Version, DateTimeOffset

### Community 284 - "Request"
Cohesion: 0.20
Nodes (10): DateTimeOffset, DefaultIdType, RequestBody, Request, Body, RequestBody, ProductId, Quantity (+2 more)

### Community 285 - "RequestBodyLimitFilter.cs"
Cohesion: 0.31
Nodes (6): Common.Endpoints.Webhooks, microsoft_aspnetcore_http_features, RequestBodyLimitEndpointExtensions, RequestBodyLimitFilter, Func, HttpContext

### Community 286 - ".SetConcurrency"
Cohesion: 0.50
Nodes (3): IBusFactoryConfigurator, ConcurrencyConfiguratorExtensions, IReceiveEndpointConfigurator

### Community 287 - "AuditLogEntry"
Cohesion: 0.29
Nodes (7): AuditLogEntry, AggregateId, AggregateType, Event, EventType, Version, DefaultIdType

### Community 288 - "DummySmsGateway"
Cohesion: 0.38
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, DummySmsGateway

### Community 289 - ".SetRetryAfterHeader"
Cohesion: 0.40
Nodes (4): RetryAfterHeaderExtensions, HttpResponse, RateLimitLease, TimeSpan

### Community 290 - ".HandleAsync"
Cohesion: 0.39
Nodes (7): GetUsersInRolePageRequest, GetUsersInRolePageResponse, RoleUserSummary, ICollection, CancellationToken, Task, GetUsersInRolePageRequestHandler

### Community 291 - "DeviceRegistrationId"
Cohesion: 0.25
Nodes (4): DefaultIdType, DeviceRegistrationId, EntityTypeBuilder, DeviceRegistrationConfiguration

### Community 293 - "IdentitySchemeOptions"
Cohesion: 0.33
Nodes (5): IdentitySchemeOptions, Scheme, IdentitySchemeOptionsValidator, IEndpointRouteBuilder, IOptions

### Community 294 - "KeycloakPermission"
Cohesion: 0.29
Nodes (3): KeycloakPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder

### Community 295 - "EnrichLogsWithUserInfoMiddleware"
Cohesion: 0.25
Nodes (6): IMiddleware, serilog_context, HttpContext, RequestDelegate, Task, EnrichLogsWithUserInfoMiddleware

### Community 296 - "FirebaseServiceAccountOptions"
Cohesion: 0.29
Nodes (7): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri

### Community 297 - ".AddBrevo"
Cohesion: 0.33
Nodes (5): IConfiguration, IFusionCache, IOptions, IServiceCollection, Setup

### Community 298 - ".CreateProductTemplateAsync"
Cohesion: 0.20
Nodes (8): ProductTemplate, CancellationToken, Task, Request, Brand, Color, Model, RequestValidator

### Community 299 - ".MapEndpoint"
Cohesion: 0.40
Nodes (3): RouteGroupBuilder, RouteGroupBuilder, Setup

### Community 300 - "ResultTelemetryExtensions"
Cohesion: 0.38
Nodes (4): Activity, ResultTelemetryExtensions, ActivitySource, Task

### Community 301 - "MassTransitInterModuleRequestClient"
Cohesion: 0.33
Nodes (5): IClientFactory, MassTransitInterModuleRequestClient, CancellationToken, IOptions, Task

### Community 302 - "ProblemDetailsExtensions.cs"
Cohesion: 0.40
Nodes (3): ProblemDetails, ProblemDetailsExtensions, ICollection

### Community 303 - "BackgroundJobsOptions"
Cohesion: 0.29
Nodes (7): BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount, BackgroundJobsOptionsValidator

### Community 304 - "JobStatus"
Cohesion: 0.33
Nodes (5): JobStatus, Failed, Queued, Running, Succeeded

### Community 305 - ".EmailVerificationTokenValidation"
Cohesion: 0.50
Nodes (3): IResxLocalizer, IRuleBuilder, IRuleBuilderOptions

### Community 306 - "InventoryTelemetry"
Cohesion: 0.50
Nodes (4): ActivitySource, Counter, Meter, InventoryTelemetry

### Community 307 - "Common.Application.Auth"
Cohesion: 0.07
Nodes (17): Products.Endpoints.Stores.v1.Update, Products.Endpoints.Stores.v1.Deactivate, Products.Endpoints.Products.v1.My.Update, Products.Endpoints.ProductTemplates.v1.Deactivate, Products.Endpoints.Products.v1.My.Get, Common.Application.AuditLog, Common.Application.Extensions, Products.Endpoints.Stores.v1.My.RemoveProduct (+9 more)

### Community 308 - "PublishOutcome"
Cohesion: 0.50
Nodes (4): PublishOutcome, Failed, Published, Skipped

### Community 310 - ".InvokeAsync"
Cohesion: 0.40
Nodes (4): EndpointFilterDelegate, EndpointFilterInvocationContext, IHttpMaxRequestBodySizeFeature, ValueTask

### Community 311 - "StronglyTypedIdBinder.cs"
Cohesion: 0.29
Nodes (5): IModelBinder, microsoft_aspnetcore_mvc_modelbinding, ModelBindingContext, StronglyTypedIdBinder, Task

### Community 312 - "ProductsTelemetry"
Cohesion: 0.50
Nodes (4): ActivitySource, Counter, Meter, ProductsTelemetry

### Community 313 - ".AddServices"
Cohesion: 0.18
Nodes (10): RecurringJobOptions, IRecurringBackgroundJobs, DatabaseOptions, ConnectionString, DatabaseOptionsValidator, IConfiguration, IServiceCollection, RecurringBackgroundJobsService (+2 more)

### Community 314 - ".AddCustomMassTransit"
Cohesion: 0.40
Nodes (4): Assembly, IConfiguration, IOptions, IServiceCollection

### Community 315 - ".ConfigureConsumer"
Cohesion: 0.50
Nodes (3): IConsumerConfigurator, IRegistrationContext, IReceiveEndpointConfigurator

### Community 319 - "Request"
Cohesion: 0.12
Nodes (14): CancellationToken, RouteGroupBuilder, Task, Endpoint, RequestBody, Request, Body, Id (+6 more)

### Community 320 - "Common.InterModuleRequests/Setup.cs"
Cohesion: 0.29
Nodes (4): Common.InterModuleRequests, IAssemblyReference, Setup, IServiceCollection

### Community 323 - ".PhoneNumberValidation"
Cohesion: 0.50
Nodes (3): IResxLocalizer, IRuleBuilder, IRuleBuilderOptions

## Knowledge Gaps
- **950 isolated node(s):** `Id`, `IdAsString`, `Roles`, `SessionId`, `JwtClaimNames` (+945 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 2220 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **91 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `microsoft_aspnetcore_http`, `system_net`, `ResiliencyOptions.cs`, `EmailOptions`, `Setup`, `Common.Domain.StronglyTypedIds`, `OutboxOptions`, `microsoft_extensions_options`, `Setup.Logger.cs`, `KeycloakPermissionAuthorizationHandler.cs`, `system_diagnostics`, `Common.Application.EventBus`, `.FixedWindow`, `CachingOptions`, `InventoryOptions.cs`, `microsoft_entityframeworkcore`, `fluentvalidation`, `PushOptions`, `Infrastructure/Setup.cs`, `FirebasePushGateway.cs`, `Common.Domain.ResultMonad`, `microsoft_extensions_configuration`?**
  _High betweenness centrality (0.171) - this node is a cross-community bridge._
- **Why does `Result` connect `Result` to `.SendAsync`, `Request`, `.CommitStockReservationAsync`, `IInterModuleRequest`, `.GetMeAsync`, `FirebasePushGateway`, `Error`, `ProductTemplate`, `IKeycloakAdminClient`, `.DeactivateProductTemplateAsync`, `.DeactivateStoreAsync`, `Response`, `Product`, `.RegisterAsync`, `.GetAuditLogAsync`, `IInterModuleRequestClient`, `StockReservation`, `DummyEmailGateway`, `ICurrentUser`, `ISmsGateway`, `IInventoryDbContext`, `.SingleAsResult`, `Inventory.Domain.StockReservations.DomainEvents.v1`, `IEmailGateway`, `DummySmsGateway`, `.SendOtp`, `RequestBody`, `.CreateProductTemplateAsync`, `ICaptchaService`, `ResultTelemetryExtensions`, `VerifyPhoneOtpResponse`, `PaginationQueryableExtensions`, `PaginationResponse`, `Response`, `.VerifyOtp`, `ReCaptchaService`, `PushMessage`, `.SendOtp`, `BrevoEmailGateway`, `.AssignBasicRoleOrRollbackAsync`, `.SearchProductTemplatesAsync`, `KeycloakTokenClient`, `Request`, `.SendCoreAsync`, `.AddProductToMyStoreAsync`, `.UpdateMyStoreAsync`, `.Failure`, `Response`, `Response`, `.RemoveMyProductAsync`, `DomainEvent`, `.SearchStoresAsync`, `IProductsDbContext`, `.SendOtp`, `.GetProductAsync`, `.ActivateProductTemplateAsync`, `.SearchMyProductsAsync`, `NetGsmSmsGateway`, `.SearchStoreProductsAsync`, `.RegisterAsync`, `.ReleaseStockReservationAsync`, `HttpWarehouseGateway`, `.IsRegisteredAsync`, `.RemoveProductAsync`, `InventoryOptions`, `.ReserveSeriesAsync`, `Response`, `.HandleWarehouseWebhookAsync`, `Response`, `.TapWhenFeatureEnabledAsync`, `StoreId`?**
  _High betweenness centrality (0.136) - this node is a cross-community bridge._
- **Why does `ApplicationUserId` connect `ApplicationUserId` to `IStronglyTypedId`, `NotificationPayload`, `KeycloakAdminClient`, `IInterModuleRequest`, `V1StoreCreatedDomainEvent`, `KeycloakUser`, `IKeycloakAdminClient`, `KeycloakPermissionAuthorizationHandler`, `Response`, `.RegisterAsync`, `DeviceRegistration`, `ICurrentUser`, `AuditableEntity`, `JobRow`, `.HandleAsync`, `For`, `DeviceRegistrationId`, `CurrentUser`, `PaginationRequest`, `SendSecurityAlertRequestHandler`, `PaginationResponse`, `Response`, `.TryDeserialize`, `.AssignBasicRoleOrRollbackAsync`, `KeycloakTokenClient`, `.HandleAsync`, `IntegrationEvent`, `Response`, `IAuditableEntity`, `.SearchStoresAsync`, `.Configure`, `Store`, `IProductsDbContext`, `Seeder`, `AuditableEntityResponse`, `.RegisterAsync`, `Response`, `GetActiveSessionIdsRequest`, `StoreId`?**
  _High betweenness centrality (0.069) - this node is a cross-community bridge._
- **What connects `Id`, `IdAsString`, `Roles` to the rest of the system?**
  _950 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `IStronglyTypedId` be split into smaller, more focused modules?**
  _Cohesion score 0.14210526315789473 - nodes in this community are weakly interconnected._
- **Should `Common.Application.Options` be split into smaller, more focused modules?**
  _Cohesion score 0.06994047619047619 - nodes in this community are weakly interconnected._
- **Should `RedisFixedWindowRateLimiter` be split into smaller, more focused modules?**
  _Cohesion score 0.13333333333333333 - nodes in this community are weakly interconnected._