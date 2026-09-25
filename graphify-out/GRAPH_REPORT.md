# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-09-25)

## Corpus Check
- 575 files · ~91,066 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 4947 nodes · 9774 edges · 386 communities (286 shown, 100 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 260 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `50cca624`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- .ReserveSeriesAsync
- NotificationPayload
- OutboxProcessor
- Common.Domain.StronglyTypedIds
- Hybrid DDD (Writes) / VSA (Reads)
- KeycloakAdminClient
- RedisFixedWindowRateLimiter
- ApplyAuditingInterceptor.cs
- FirebasePushGateway
- .RequireOtpTemplateForDefaultCulture
- Error
- Common.Domain.ResultMonad
- ApplicationUserId
- ISearchLanguageResolver
- Common.Application.Options
- KeycloakPermissionAuthorizationHandler
- EmailOptions
- Product
- Inventory.Domain.StockReservations.DomainEvents.v1
- V1ProductAddedToStoreDomainEvent.cs
- LogProductCatalogChangeHandler
- BoundedRequestCaptureStream
- DeviceRegistration
- EmailMessage
- .RevokeToken
- microsoft_extensions_options
- V1StoreCreatedDomainEvent
- RequestResponseBodyLoggingMiddleware
- system_text_json_serialization
- Result
- JobRow
- NotificationsModule
- .SingleAsResult
- .RefreshToken
- KeycloakPermissionClient
- OutboxMessage
- CustomRateLimitingOptions
- AdminRepresentations.cs
- RequestBody
- ObservabilityOptions
- Host.Swagger
- Request
- .SaveWithOutboxAsync
- CreateStoreRateLimitingPolicy
- KeycloakOptions
- .AddKeycloakInfrastructure
- PaginationQueryableExtensions
- Outbox Misuse Check
- microsoft_entityframeworkcore
- Add Integration Event Command
- Response
- ProductsDbContext
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
- IDbContext
- RequestLoggingOptions
- .SendCoreAsync
- BackgroundJobsService
- OutboxModule
- .AddNotificationsSignalR
- ValueObject
- StockReservation
- IntegrationEventHandlerBase
- FullTextSearchOptions
- Response
- .WriteAsync
- AsNoTracking Coverage Check
- Setup
- Response
- AuditLogRetentionService
- CheckRegistrationRateLimitingPolicy
- IAggregateRoot
- .SaveChangesAsync
- .SearchStoresAsync
- Split-Deployment PoC
- ICaptchaService
- .Configure
- My/Create/Request.cs
- DomainEvent
- PushOptions
- Configuration-Driven Module Registration
- Program.cs
- IInterModuleRequestClient
- .SeedProductAsync
- My/AddProduct/Request.cs
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
- CaptchaOptions
- .RegisterAsync
- Common.InterModuleRequests.Contracts
- RabbitMqOptions
- StockLevel
- .IsRegisteredAsync
- KeycloakPermissionPolicyProvider
- .ReleaseStockReservationAsync
- OutboxCleanupJob
- ProductId
- Response
- SkipOverlappingRecurringJobFilter
- .AddAuthInfrastructure
- Response
- IModule
- ProductsModule
- InventoryOptions
- .GetVariantAsync
- OtpVerifyRateLimitingPolicy
- NotificationsHub
- .AddCustomHealthChecks
- IEvent
- GlobalExceptionHandlingMiddleware
- Response
- EmailRateLimitingPolicy
- Seeder
- docker-compose.yml (Base Stack)
- .AddInfrastructure
- StoreId
- Consumer Idempotency (IntegrationEventHandlerBase)
- SendRequestBody
- AuditableEntityResponse
- ProductTemplates/v1/Search/Request.cs
- .WriteTooManyRequestsToResponse
- .From
- ProductTemplate
- ProcessedMessage
- IStronglyTypedId
- TokenRefreshRateLimitingPolicy
- .AddModules
- Response
- v1/AddProduct/Request.cs
- .RegisterAsync
- .AddServices
- .CreateTokensByEmail
- Stores/v1/Update/Request.cs
- StockReservationExpirySweepJobRegistrar
- OutboxOptions
- .EnsureNoMigrationsPending
- ConfigureSwaggerOptions
- Setup.Logger.cs
- ISmsGateway
- Request
- CreateStockLevelOnProductCreatedHandler
- OtpOptions
- InventoryDbContext
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
- IInterModuleRequest
- Stores/v1/Create/Request.cs
- ProductTemplateId
- system_diagnostics
- PaginationResponse
- .FixedWindow
- CorsOptions
- PushMessage
- CachingOptions
- SmsOptions
- ProductTemplates/v1/Create/Request.cs
- IAM.Domain
- SendRequestBody
- RedisOtpService
- IdentityScheme
- Policies
- ReverseProxyOptions
- BoundedCaptureStream
- JobClaimExtensions
- ServiceAccountTokenCache
- DeviceRegistryReconcileJobRegistrar
- Setup
- .GetMeAsync
- EventDispatcher
- IInventoryDbContext
- TokenCreateRateLimitingPolicy
- .UseModules
- InventoryModule
- ReCaptchaResponse
- NotificationsTelemetry
- KeycloakScopes
- BaseDbContext
- SmsRateLimitingPolicy
- Configuration-Driven Module Loading
- StatelessInboxStore
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- BackgroundJobsOptions
- Key decisions
- Stores/v1/My/Update/Request.cs
- BackgroundJobsTelemetry
- fluentvalidation
- Response
- .UpsertIfNewerAsync
- FeatureFlags
- .SendOtp
- IBackgroundJobs
- HttpWarehouseGateway
- Keycloak realm as code
- .ListSessions
- PolymorphicEventConverter
- .MapEndpoint
- IInboxStore
- NetGsmSmsGateway
- RegisterRateLimitingPolicy
- .AddCommonCaching
- .AddCommonOptions
- InterModuleRequestHandler
- BackgroundJobsModule
- IntegrationEventConverter
- .HasPatternIndex
- KeycloakPermissionAuthorizationHandler.cs
- AuditLogEntry
- V1ProductCreatedDomainEvent
- ProblemDetailsExtensions
- .UseInfrastructure
- KeycloakTokenClient.cs
- RemoveDefaultResponseSchemaFilter
- StronglyTypedIdReadOnlyJsonConverter
- StrictDateTimeOffsetJsonConverter
- CustomValidator
- KeyedResilienceProfile
- FirebasePushGateway.cs
- RequestBodyLimitMetadata
- OpenApiOptions
- SendErrorBody
- DeviceRegistryReconciliationService
- Response
- Response
- .TryWriteAsync
- Products.Domain/IAssemblyReference.cs
- IAM.Application/IAssemblyReference.cs
- Products.Infrastructure/IAssemblyReference.cs
- Products.Application/IAssemblyReference.cs
- Common.Infrastructure/IAssemblyReference.cs
- Notifications.Domain/IAssemblyReference.cs
- IAM.Infrastructure/IAssemblyReference.cs
- v1/RemoveProduct/Request.cs
- .GetAuditLogAsync
- .SendWithPipelineAsync
- Response
- StronglyTypedIdListReadOnlyJsonConverter
- Response
- SwaggerDefaultValues
- AuditLogOptions
- DevicesOptions
- IProductsDbContext
- .SendAsync
- SendForLogin/Request.cs
- .AddResilientHttpClient
- .AddNetGsm
- WebhookCallback/Request.cs
- SendForRegistration/Request.cs
- EnrichLogsWithUserInfoMiddleware
- SendResponseBody
- .TapWhenFeatureEnabledAsync
- Response
- SendForEmail/Request.cs
- .TryReadFromJsonAsync
- JobHousekeepingOptions
- AuditableEntity
- Reserve/Request.cs
- RequestBodyLimitFilter.cs
- .UpdateProductAsync
- IOtpService
- DummySmsGateway
- .DeactivateProductTemplateAsync
- .RemoveProductAsync
- .UpdateStoreAsync
- .AddServices
- .MapEndpoints
- UpdateCurrentPushToken/Request.cs
- ProjectionReconciliationOptions.cs
- SecurityHeadersOptions.cs
- .SetConcurrency
- Setup
- .MapEndpoint
- ResultTelemetryExtensions
- ValidationContextExtensions.cs
- .EmailVerificationTokenValidation
- DefaultResponsesOperationFilter
- .SetRetryAfterHeader
- .AddCustomSwagger
- .PhoneNumberValidation
- SignalROptions
- PublishOutcome
- .Capture
- VerifyEmail/Request.cs
- Common.Application.ModelBinders
- HttpContextExtensions
- Setup
- IAM.Endpoints
- Setup
- Setup
- .UseGlobalExceptionHandlingMiddleware
- Products.Endpoints
- Products/v1/Update/Request.cs
- Notifications.Infrastructure
- ResultTelemetryExtensions.cs
- .AddGlobalExceptionHandlingMiddleware
- Products/v1/My/Update/Request.cs
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
1. `Result` - 136 edges
2. `Common.Application.Options` - 134 edges
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

## Communities (386 total, 100 thin omitted)

### Community 0 - ".ReserveSeriesAsync"
Cohesion: 0.11
Nodes (18): Inventory.Endpoints.StockReservations.v1.ReserveSeries, GetProductRequest, GetProductResponse, DefaultIdType, CancellationToken, IOptions, List, RequestBody (+10 more)

### Community 1 - "NotificationPayload"
Cohesion: 0.18
Nodes (13): Notifications.Application.Hubs, IHubContext, CancellationToken, IReadOnlyList, Task, INotificationDispatcher, Task, INotificationsClient (+5 more)

### Community 2 - "OutboxProcessor"
Cohesion: 0.21
Nodes (12): IPublishEndpoint, PublishOutcome, CancellationToken, Exception, ILogger, IOptions, IServiceScopeFactory, LoggerMessage (+4 more)

### Community 3 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.16
Nodes (8): Common.Domain.StronglyTypedIds, Common.Application.DTOs, Products.Endpoints.Stores.v1.Get, Common.Domain.Entities, Common.Domain.Aggregates, Products.Endpoints.Stores.v1.My.Get, StronglyTypedIdHelper, system_componentmodel_dataannotations

### Community 5 - "KeycloakAdminClient"
Cohesion: 0.09
Nodes (37): DateOnly, DateTimeOffset, IReadOnlyList, CreateKeycloakUser, GrantedPermission, KeycloakUser, KeycloakUserPage, KeycloakUserSession (+29 more)

### Community 6 - "RedisFixedWindowRateLimiter"
Cohesion: 0.13
Nodes (16): RateLimiter, RateLimiterStatistics, FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration, CancellationToken (+8 more)

### Community 7 - "ApplyAuditingInterceptor.cs"
Cohesion: 0.15
Nodes (10): Common.Infrastructure.Persistence.Auditing, microsoft_entityframeworkcore_diagnostics, ApplyAuditingInterceptor, CancellationToken, DbContextEventData, InterceptionResult, TimeProvider, ValueTask (+2 more)

### Community 8 - "FirebasePushGateway"
Cohesion: 0.16
Nodes (12): FirebaseApp, FirebaseMessaging, IDisposable, CancellationToken, Exception, IEnumerable, ILogger, IReadOnlyList (+4 more)

### Community 9 - ".RequireOtpTemplateForDefaultCulture"
Cohesion: 0.40
Nodes (4): Func, IConfiguration, IEnumerable, OtpTemplateConfiguration

### Community 10 - "Error"
Cohesion: 0.07
Nodes (22): StringLocalizerExtensions, IStringLocalizer, Error, Key, ParameterName, StatusCode, SubErrors, Value (+14 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.08
Nodes (33): Products.Infrastructure.InterModuleRequestHandlers, Common.Application.Search, Inventory.Infrastructure.InterModuleRequestHandlers, Inventory.Application.Persistence, Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Products.Endpoints.ProductTemplates (+25 more)

### Community 12 - "ApplicationUserId"
Cohesion: 0.10
Nodes (19): ApplicationUserId, IsEmpty, Value, DefaultIdType, CancellationToken, IReadOnlyList, Task, IKeycloakAdminClient (+11 more)

### Community 13 - "ISearchLanguageResolver"
Cohesion: 0.11
Nodes (16): Configuration, SaveChangesInterceptor, ISearchLanguageResolver, UniversalConfig, SearchLanguageResolver, UniversalConfig, IOptions, Setup (+8 more)

### Community 14 - "Common.Application.Options"
Cohesion: 0.04
Nodes (55): asp_versioning, asp_versioning_builder, asp_versioning_conventions, Common.Infrastructure.Modules, IAM.Endpoints.Captcha.VersionNeutral, Notifications.Application.Sms, Common.InterModuleRequests, Notifications.Infrastructure.Devices (+47 more)

### Community 15 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.14
Nodes (14): AuthorizationHandler, AuthorizationHandlerContext, CancellationToken, ClaimsPrincipal, HttpContext, IFusionCache, IHttpContextAccessor, IOptions (+6 more)

### Community 16 - "EmailOptions"
Cohesion: 0.09
Nodes (24): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+16 more)

### Community 17 - "Product"
Cohesion: 0.07
Nodes (26): Products.Domain.Products.DomainEvents.v1, ProductId, V1ProductDescriptionUpdatedDomainEvent, ProductId, V1ProductNameUpdatedDomainEvent, ProductId, V1ProductPriceDecreasedDomainEvent, ProductId (+18 more)

### Community 18 - "Inventory.Domain.StockReservations.DomainEvents.v1"
Cohesion: 0.09
Nodes (17): Inventory.Domain.StockReservations.DomainEvents.v1, DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationCommitConflictDetectedDomainEvent, DateTimeOffset, DefaultIdType, StockReservationId (+9 more)

### Community 19 - "V1ProductAddedToStoreDomainEvent.cs"
Cohesion: 0.16
Nodes (11): StoreId, ProductSnapshot, ProductTemplateId, ProductSnapshot, V1ProductAddedToStoreDomainEvent, V1ProductAddedToStoreDomainEventExtensions, ProductSnapshot, ProductTemplateId (+3 more)

### Community 20 - "LogProductCatalogChangeHandler"
Cohesion: 0.24
Nodes (8): CancellationToken, DefaultIdType, IFusionCache, ILogger, IOptions, LoggerMessage, Task, LogProductCatalogChangeHandler

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.18
Nodes (7): BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position, Stream

### Community 22 - "DeviceRegistration"
Cohesion: 0.12
Nodes (14): DateTimeOffset, DefaultIdType, Guid, DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive (+6 more)

### Community 23 - "EmailMessage"
Cohesion: 0.32
Nodes (6): EmailMessage, CancellationToken, ILogger, LoggerMessage, Task, DummyEmailGateway

### Community 24 - ".RevokeToken"
Cohesion: 0.13
Nodes (14): DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse, IReadOnlyList, CancellationToken, RouteGroupBuilder, Task, Endpoint, CancellationToken (+6 more)

### Community 25 - "microsoft_extensions_options"
Cohesion: 0.06
Nodes (36): common_application_localization_resources, IAM.Endpoints.Users.VersionNeutral.Search, IAM.Domain.Users, Products.Endpoints.Stores.v1.Search, Common.Infrastructure.RateLimiting, Notifications.Infrastructure.Email.Brevo, Products.Infrastructure.RateLimiting, Products.Endpoints.Products.v1.Search (+28 more)

### Community 26 - "V1StoreCreatedDomainEvent"
Cohesion: 0.11
Nodes (16): DomainEventHandlerBase, CancellationToken, Task, IEventHandler, CancellationToken, Task, IEventHandlerWrapper, CancellationToken (+8 more)

### Community 27 - "RequestResponseBodyLoggingMiddleware"
Cohesion: 0.22
Nodes (7): IDiagnosticContext, HttpContext, IList, IOptions, PathString, RequestDelegate, RequestResponseBodyLoggingMiddleware

### Community 28 - "system_text_json_serialization"
Cohesion: 0.29
Nodes (6): Common.Application.JsonConverters, microsoft_entityframeworkcore_storage_valueconversion, DomainEventConverter, JsonSerializerOptions, system_text_json, system_text_json_serialization

### Community 29 - "Result"
Cohesion: 0.09
Nodes (18): SearchValues, StringExtensions, Result, Error, IsFailure, Success, Value, Func (+10 more)

### Community 30 - "JobRow"
Cohesion: 0.11
Nodes (15): Common.Application.Jobs, JobRow, FailureKey, FinishedOn, Progress, QueuedOn, RequestedBy, StartedOn (+7 more)

### Community 31 - "NotificationsModule"
Cohesion: 0.15
Nodes (10): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, NotificationsModule, ActivitySourceNames, MeterNames (+2 more)

### Community 32 - ".SingleAsResult"
Cohesion: 0.18
Nodes (10): CollectionExtensions, Func, ICollection, IEnumerable, PersistenceQueryableExtensions, CancellationToken, Expression, Func (+2 more)

### Community 33 - ".RefreshToken"
Cohesion: 0.15
Nodes (11): IAM.Endpoints.Tokens.VersionNeutral.Refresh, CancellationToken, RouteGroupBuilder, Task, Endpoint, DateTimeOffset, Response, AccessToken (+3 more)

### Community 34 - "KeycloakPermissionClient"
Cohesion: 0.13
Nodes (20): CancellationToken, Dictionary, HttpClient, HttpResponseMessage, ILogger, IOptions, IReadOnlyList, List (+12 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.14
Nodes (13): OutboxMessage, CreatedOn, Event, FailedOn, Id, IsProcessed, NextRetryAt, ParentSpanId (+5 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.08
Nodes (22): CheckRegistrationRateLimitingPolicy, EmailRateLimitingPolicy, OtpVerifyRateLimitingPolicy, RegisterRateLimitingPolicy, SmsRateLimitingPolicy, CustomRateLimitingOptions, CheckRegistration, CreateStore (+14 more)

### Community 37 - "AdminRepresentations.cs"
Cohesion: 0.12
Nodes (16): CredentialRepresentation, Temporary, Type, Value, ErrorRepresentation, Error, ErrorMessage, Field (+8 more)

### Community 38 - "RequestBody"
Cohesion: 0.15
Nodes (14): DateTimeOffset, DefaultIdType, ICollection, RequestBody, FirstDeadline, IntervalDays, OccurrenceCount, ProductId (+6 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.10
Nodes (20): IHostBuilder, ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics, EnableTracing, LogSink (+12 more)

### Community 40 - "Host.Swagger"
Cohesion: 0.36
Nodes (5): Host.Swagger, microsoft_aspnetcore_mvc_apiexplorer, microsoft_openapi, swashbuckle_aspnetcore_swaggergen, system_text_json_nodes

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

### Community 45 - ".AddKeycloakInfrastructure"
Cohesion: 0.16
Nodes (10): CancellationToken, Task, IServiceAccountTokenProvider, HttpClient, IOptions, TimeProvider, ServiceAccountTokenProvider, IOptions (+2 more)

### Community 46 - "PaginationQueryableExtensions"
Cohesion: 0.07
Nodes (32): BinaryExpression, How it works, One-time setup _(Build, in a migration)_, Query path, Ranking, Write path, ExpressionVisitor, KeyedRow (+24 more)

### Community 48 - "microsoft_entityframeworkcore"
Cohesion: 0.06
Nodes (36): Common.Infrastructure.Persistence.Inbox, Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Products.Infrastructure.Persistence.Seeding, Common.Application.Persistence.Inbox, Inventory.Application.IntegrationEventHandlers, Common.Application.Persistence, Outbox (+28 more)

### Community 50 - "Response"
Cohesion: 0.11
Nodes (16): CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate (+8 more)

### Community 51 - "ProductsDbContext"
Cohesion: 0.13
Nodes (14): DbContextOptions, DbSet, ILogger, ProductTemplate, Store, TimeProvider, ProductsDbContext, Products (+6 more)

### Community 52 - "ReCaptchaService"
Cohesion: 0.21
Nodes (10): FormUrlEncodedContent, ReCaptchaResponse, CancellationToken, Exception, HttpClient, ILogger, IOptions, LoggerMessage (+2 more)

### Community 53 - "Request"
Cohesion: 0.09
Nodes (22): Guid, Request, ClientId, DeviceId, DeviceName, Otp, PhoneNumber, PushToken (+14 more)

### Community 54 - ".SendOtp"
Cohesion: 0.11
Nodes (17): EmailOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, SendEmailOtpRequest, SendEmailOtpResponse, CancellationToken, IFeatureManager (+9 more)

### Community 55 - "BrevoEmailGateway"
Cohesion: 0.23
Nodes (10): Exception, HttpClient, HttpStatusCode, ILogger, IOptions, JsonSerializerOptions, LoggerMessage, BrevoEmailGateway (+2 more)

### Community 56 - "ProjectionReconciliationJob"
Cohesion: 0.24
Nodes (10): Items, Next, ProjectionReconciliationJob, CancellationToken, IDbContext, ILogger, IOptions, IReadOnlyList (+2 more)

### Community 58 - "OutboxMetricsJob"
Cohesion: 0.13
Nodes (14): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxMetricsJob (+6 more)

### Community 59 - ".SearchProductTemplatesAsync"
Cohesion: 0.12
Nodes (16): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Request, Brand (+8 more)

### Community 61 - "KeycloakTokenClient"
Cohesion: 0.10
Nodes (27): JsonWebTokenHandler, CancellationToken, Task, IKeycloakTokenClient, DateTimeOffset, KeycloakTokens, CancellationToken, ILogger (+19 more)

### Community 62 - "IDbContext"
Cohesion: 0.12
Nodes (16): DatabaseFacade, Deleted, EntityEntry, JobHousekeepingJob, CancellationToken, ILogger, IOptions, LoggerMessage (+8 more)

### Community 63 - "RequestLoggingOptions"
Cohesion: 0.11
Nodes (20): IPostConfigureOptions, RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes (+12 more)

### Community 64 - ".SendCoreAsync"
Cohesion: 0.23
Nodes (8): SendErrorBody, CancellationToken, HttpResponseMessage, SendRequestBody, Task, SendContact, Email, Name

### Community 65 - "BackgroundJobsService"
Cohesion: 0.23
Nodes (8): IBackgroundJobClientV2, BackgroundJobsService, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 66 - "OutboxModule"
Cohesion: 0.14
Nodes (16): Action, Exception, IApplicationBuilder, IEndpointRouteBuilder, IEnumerable, IHostApplicationLifetime, ILogger, ILoggerFactory (+8 more)

### Community 67 - ".AddNotificationsSignalR"
Cohesion: 0.17
Nodes (10): HubConnectionContext, IUserIdProvider, RedisOptions, IConfiguration, IConfigureOptions, IConnectionMultiplexer, IServiceCollection, IUserIdProvider (+2 more)

### Community 68 - "ValueObject"
Cohesion: 0.25
Nodes (4): Common.Domain, IComparable, ValueObject, IEnumerable

### Community 69 - "StockReservation"
Cohesion: 0.07
Nodes (24): Inventory.Domain.StockReservations.Errors, DateTimeOffset, DefaultIdType, StockReservationId, V1StockReservationExpiredDomainEvent, DateTimeOffset, StockReservationId, V1StockReservationReleaseAttemptAbandonedDomainEvent (+16 more)

### Community 70 - "IntegrationEventHandlerBase"
Cohesion: 0.22
Nodes (12): IConsumer, IntegrationEventHandlerBase, MaxEventAge, CancellationToken, ConsumeContext, DefaultIdType, IFusionCache, ILogger (+4 more)

### Community 71 - "FullTextSearchOptions"
Cohesion: 0.10
Nodes (22): Add a new language/culture, Add search to a new entity _(Build checklist)_, Change the vector (weights, fields, or config), Extending and maintaining, File map _(Build)_, Full-Text Search, Gotchas, Non-goals (+14 more)

### Community 72 - "Response"
Cohesion: 0.18
Nodes (9): Products.Endpoints.ProductTemplates.v1.Get, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Brand, Color (+1 more)

### Community 73 - ".WriteAsync"
Cohesion: 0.33
Nodes (5): Memory, ReadOnlyMemory, CancellationToken, Task, ValueTask

### Community 75 - "Setup"
Cohesion: 0.33
Nodes (4): ApiVersionSet, Setup, IEndpointRouteBuilder, IServiceCollection

### Community 76 - "Response"
Cohesion: 0.22
Nodes (8): RouteGroupBuilder, Endpoint, Response, Address, Description, Name, OwnerId, ProductCount

### Community 77 - "AuditLogRetentionService"
Cohesion: 0.33
Nodes (7): AuditLogRetentionService, CancellationToken, ILogger, IOptions, LoggerMessage, NpgsqlDataSource, Task

### Community 78 - "CheckRegistrationRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, CheckRegistrationRateLimitingPolicy (+1 more)

### Community 79 - "IAggregateRoot"
Cohesion: 0.14
Nodes (11): IAggregateRoot, Events, Id, Version, IReadOnlyCollection, IAuditableEntity, CreatedBy, CreatedOn (+3 more)

### Community 80 - ".SaveChangesAsync"
Cohesion: 0.06
Nodes (23): CancellationToken, Task, CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint, CancellationToken (+15 more)

### Community 81 - ".SearchStoresAsync"
Cohesion: 0.13
Nodes (12): LikePattern, CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Request (+4 more)

### Community 82 - "Split-Deployment PoC"
Cohesion: 0.25
Nodes (6): Concurrent safety, Files added by this PoC, How to run, Split-Deployment PoC, What this proves, README (Boilerplate Overview)

### Community 83 - "ICaptchaService"
Cohesion: 0.15
Nodes (10): ICaptchaService, CancellationToken, IFusionCache, IOptions, Task, CachedCaptchaService, DummyCaptchaService, IConfiguration (+2 more)

### Community 84 - ".Configure"
Cohesion: 0.13
Nodes (16): AuditableEntityConfiguration, EntityTypeBuilder, JobRowConfiguration, EntityTypeBuilder, StronglyTypedIdValueConverter, DefaultIdType, EntityTypeBuilder, DeviceRegistrationConfiguration (+8 more)

### Community 85 - "My/Create/Request.cs"
Cohesion: 0.22
Nodes (7): Products.Endpoints.Stores.v1.My.Create, RouteGroupBuilder, Endpoint, Request, RequestValidator, Response, Id

### Community 86 - "DomainEvent"
Cohesion: 0.05
Nodes (39): Products.Application.Stores.DomainEventHandlers.v1, Products.Domain.Stores.DomainEvents.v1, AggregateRoot, Events, Id, Version, IReadOnlyCollection, List (+31 more)

### Community 87 - "PushOptions"
Cohesion: 0.12
Nodes (20): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri, PushOptions (+12 more)

### Community 89 - "Program.cs"
Cohesion: 0.22
Nodes (6): ConfigurationManager, Host, Host.Configurations, Setup, Program, WebApplicationBuilder

### Community 90 - "IInterModuleRequestClient"
Cohesion: 0.08
Nodes (27): Cross-process call path, How it works, ICoreModule, IInterModuleRequestClient, CancellationToken, Task, GetSeedUserIdsRequest, GetSeedUserIdsResponse (+19 more)

### Community 91 - ".SeedProductAsync"
Cohesion: 0.22
Nodes (7): CancellationToken, Task, CancellationToken, List, ProductTemplateId, StoreId, Task

### Community 92 - "My/AddProduct/Request.cs"
Cohesion: 0.20
Nodes (9): Constants, ProductTemplateId, Request, Description, Name, Price, ProductTemplateId, Quantity (+1 more)

### Community 93 - ".MapEndpoint"
Cohesion: 0.22
Nodes (6): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, RouteGroupBuilder, Response, ClientKey, RouteGroupBuilder, Setup

### Community 94 - "KeyedResiliencePipelines"
Cohesion: 0.14
Nodes (13): HttpRequestException, ResiliencePipelineBuilder, ResiliencePipelineRegistry, KeyedResiliencePipelines, HttpResponseMessage, IConnectionMultiplexer, ILoggerFactory, IOptions (+5 more)

### Community 95 - ".SearchMyProductsAsync"
Cohesion: 0.11
Nodes (18): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Request, MaxPrice (+10 more)

### Community 96 - "DatabaseSeederOrchestrator"
Cohesion: 0.07
Nodes (26): BackgroundService, IDatabaseSeeder, Priority, CancellationToken, Task, DatabaseSeederOrchestrator, CancellationToken, Exception (+18 more)

### Community 97 - "StockReservationExpirySweepService"
Cohesion: 0.18
Nodes (11): IServiceCollection, Setup, CancellationToken, Exception, Guid, ILogger, IOptions, LoggerMessage (+3 more)

### Community 98 - ".SearchStoreProductsAsync"
Cohesion: 0.17
Nodes (11): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Description (+3 more)

### Community 99 - "AuditLogRetentionJobRegistrar"
Cohesion: 0.22
Nodes (9): AuditLogRetentionJobRegistrar, CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task, Setup (+1 more)

### Community 100 - "OutboxDbContext"
Cohesion: 0.11
Nodes (16): DbContext, IEntityTypeConfiguration, IOutboxDbContext, OutboxMessages, CancellationToken, DbSet, Task, IConfiguration (+8 more)

### Community 101 - "ResiliencyOptions"
Cohesion: 0.15
Nodes (13): ResiliencyOptions, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, Keyed, MaxRetryAttempts (+5 more)

### Community 102 - "HttpContextTargetingContextAccessor"
Cohesion: 0.20
Nodes (8): ITargetingContextAccessor, HttpContextTargetingContextAccessor, IHttpContextAccessor, ValueTask, Setup, IConfiguration, IServiceCollection, TargetingContext

### Community 103 - "InboxCleanupJob"
Cohesion: 0.22
Nodes (10): CancellationToken, IEnumerable, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider (+2 more)

### Community 104 - "CaptchaOptions"
Cohesion: 0.16
Nodes (13): CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey, Provider, ScoreThreshold, SecretKey (+5 more)

### Community 105 - ".RegisterAsync"
Cohesion: 0.10
Nodes (16): CancellationToken, IFeatureManager, ILogger, RouteGroupBuilder, Task, Endpoint, DateTimeOffset, Response (+8 more)

### Community 106 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.07
Nodes (29): Notifications.Application.Otp, IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, IAM.Endpoints.Tokens.VersionNeutral.Create, IAM.Endpoints.Users, IAM.Endpoints.Users.VersionNeutral.SelfRegister, Common.Application.FeatureManagement, IAM.Endpoints.Otp, Notifications.Application.Persistence (+21 more)

### Community 107 - "RabbitMqOptions"
Cohesion: 0.05
Nodes (38): ConsumerDefinition, IClientFactory, IConsumerConfigurator, IRegistrationContext, InterModuleRequestOptions, DependencyUnavailableRetryAfterSeconds, HandlerConcurrentMessageLimit, HandlerPrefetchCount (+30 more)

### Community 108 - "StockLevel"
Cohesion: 0.15
Nodes (12): Inventory.Domain.StockLevels, Inventory.Domain.StockLevels.DomainEvents.v1, DefaultIdType, StockLevelId, V1StockLevelCreatedDomainEvent, DefaultIdType, StockLevel, ProductId (+4 more)

### Community 109 - ".IsRegisteredAsync"
Cohesion: 0.18
Nodes (10): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, PhoneNumber, RequestValidator (+2 more)

### Community 110 - "KeycloakPermissionPolicyProvider"
Cohesion: 0.12
Nodes (14): AuthorizationOptions, AuthorizationPolicy, DefaultAuthorizationPolicyProvider, IAuthorizationPolicyProvider, IAuthorizationRequirement, KeycloakPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder (+6 more)

### Community 111 - ".ReleaseStockReservationAsync"
Cohesion: 0.13
Nodes (14): CancellationToken, Task, IWarehouseGateway, CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint (+6 more)

### Community 112 - "OutboxCleanupJob"
Cohesion: 0.27
Nodes (8): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxCleanupJob

### Community 113 - "ProductId"
Cohesion: 0.15
Nodes (12): Products.Endpoints.Stores.v1.My.RemoveProduct, DefaultIdType, ProductId, Request, Id, RequestValidator, Request, Id (+4 more)

### Community 114 - "Response"
Cohesion: 0.11
Nodes (17): IAM.Endpoints.Users.VersionNeutral.Me.Get, RouteGroupBuilder, Endpoint, DateOnly, DateTimeOffset, IReadOnlyCollection, Response, BirthDate (+9 more)

### Community 115 - "SkipOverlappingRecurringJobFilter"
Cohesion: 0.22
Nodes (7): hangfire_server, hangfire_storage, SkipOverlappingRecurringJobFilter, ILogger, LoggerMessage, PerformedContext, PerformingContext

### Community 116 - ".AddAuthInfrastructure"
Cohesion: 0.12
Nodes (16): IAllowAnonymous, IAuthorizationHandler, IConfigureNamedOptions, HttpContext, HttpStatusCode, IOptions, IProblemDetailsService, IResxLocalizer (+8 more)

### Community 117 - "Response"
Cohesion: 0.09
Nodes (20): Inventory.Endpoints.StockReservations.v1.Get, ReservationStatus, Active, Committed, Expired, Released, RequiresReconciliation, CancellationToken (+12 more)

### Community 118 - "IModule"
Cohesion: 0.09
Nodes (20): OpenTelemetryBuilder, ResourceBuilder, IModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority (+12 more)

### Community 119 - "ProductsModule"
Cohesion: 0.12
Nodes (13): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, ProductsModule (+5 more)

### Community 120 - "InventoryOptions"
Cohesion: 0.08
Nodes (25): IHeaderDictionary, IValidator, InventoryOptions, MaxOccurrencesPerSeries, SafetyBufferQuantity, StockReservationExpirySweepBatchSize, StockReservationExpirySweepCron, WarehouseGatewayBaseUrl (+17 more)

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

### Community 125 - "IEvent"
Cohesion: 0.14
Nodes (12): IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, DateTimeOffset, IEvent (+4 more)

### Community 126 - "GlobalExceptionHandlingMiddleware"
Cohesion: 0.27
Nodes (10): Exception, HttpContext, ILogger, IOptions, IProblemDetailsService, IResxLocalizer, LoggerMessage, RequestDelegate (+2 more)

### Community 127 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 128 - "EmailRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, EmailRateLimitingPolicy (+1 more)

### Community 129 - "Seeder"
Cohesion: 0.31
Nodes (7): ILogger, LoggerMessage, ProductsDbContext, Seeder, CancellationToken, List, Task

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.48
Nodes (7): Observability (OpenTelemetry), docker-compose.yml (Base Stack), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), Postgres Service (mm.postgres), RabbitMQ Service (mm.rabbitmq), Redis Service (mm.redis)

### Community 131 - ".AddInfrastructure"
Cohesion: 0.29
Nodes (7): HostOptions, JsonOptions, Assembly, IConfiguration, IResxLocalizer, IServiceCollection, IWebHostEnvironment

### Community 132 - "StoreId"
Cohesion: 0.12
Nodes (10): Products.Endpoints.Stores.v1.Deactivate, StoreId, DefaultIdType, StoreId, Request, Id, RequestValidator, Request (+2 more)

### Community 134 - "SendRequestBody"
Cohesion: 0.25
Nodes (8): SendContact, IReadOnlyList, SendRequestBody, HtmlContent, Sender, Subject, TextContent, To

### Community 135 - "AuditableEntityResponse"
Cohesion: 0.15
Nodes (13): AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, DateTimeOffset, Response (+5 more)

### Community 137 - ".WriteTooManyRequestsToResponse"
Cohesion: 0.13
Nodes (14): PartitionedRateLimiter, CancellationToken, Func, HttpContext, IConfiguration, IProblemDetailsService, IReadOnlyList, IResxLocalizer (+6 more)

### Community 138 - ".From"
Cohesion: 0.09
Nodes (26): AspNetResult, IEndpointFilter, IFeatureManagerSnapshot, ResxLocalizer, ProblemResponse, ResultToAcceptedResponseTransformer, ResultToCreatedResponseTransformer, ResultToResponseTransformer (+18 more)

### Community 139 - "ProductTemplate"
Cohesion: 0.15
Nodes (11): IReadOnlyList, List, ProductTemplate, Brand, Color, IsActive, Model, Products (+3 more)

### Community 140 - "ProcessedMessage"
Cohesion: 0.19
Nodes (10): ProcessedMessage, ConsumerName, MessageId, ProcessedOn, DateTimeOffset, DefaultIdType, UniqueConstraintException, DefaultIdType (+2 more)

### Community 141 - "IStronglyTypedId"
Cohesion: 0.21
Nodes (8): StronglyTypedIdWriteOnlyJsonConverter, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, IStronglyTypedId, Value, DefaultIdType

### Community 142 - "TokenRefreshRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenRefreshRateLimitingPolicy (+1 more)

### Community 143 - ".AddModules"
Cohesion: 0.17
Nodes (9): LoadAll, Names, Assembly, IConfiguration, IEnumerable, IReadOnlyCollection, IReadOnlyList, IServiceCollection (+1 more)

### Community 144 - "Response"
Cohesion: 0.11
Nodes (17): IAM.Endpoints.Users.VersionNeutral.Get, CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, DateTimeOffset, Response (+9 more)

### Community 145 - "v1/AddProduct/Request.cs"
Cohesion: 0.11
Nodes (17): Products.Endpoints.Stores.v1.AddProduct, RouteGroupBuilder, Endpoint, ProductTemplateId, RequestBody, Request, Body, Id (+9 more)

### Community 146 - ".RegisterAsync"
Cohesion: 0.13
Nodes (18): BindDeviceSessionRequest, BindDeviceSessionResponse, Guid, CancellationToken, Exception, Guid, ILogger, LoggerMessage (+10 more)

### Community 147 - ".AddServices"
Cohesion: 0.12
Nodes (16): RecurringJobOptions, IRecurringBackgroundJobs, Action, Expression, Func, Task, IConfiguration, ILogger (+8 more)

### Community 148 - ".CreateTokensByEmail"
Cohesion: 0.07
Nodes (27): IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, VerifyEmailOtpRequest, VerifyEmailOtpResponse, EmailNormalization, CancellationToken, RouteGroupBuilder, Task, Endpoint (+19 more)

### Community 149 - "Stores/v1/Update/Request.cs"
Cohesion: 0.20
Nodes (10): Products.Endpoints.Stores.v1.Update, RequestBody, Request, Body, Id, RequestBody, Address, Description (+2 more)

### Community 150 - "StockReservationExpirySweepJobRegistrar"
Cohesion: 0.31
Nodes (7): CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task, StockReservationExpirySweepJobRegistrar

### Community 151 - "OutboxOptions"
Cohesion: 0.10
Nodes (21): OutboxCleanupSettings, BatchSize, CronSchedule, Enabled, RetentionDays, OutboxCleanupSettingsValidator, OutboxOptions, BaseBackoffSeconds (+13 more)

### Community 152 - ".EnsureNoMigrationsPending"
Cohesion: 0.35
Nodes (6): AutoMigrateMarker, IAutoMigrateMarker, MigrationGuard, ILogger, IServiceProvider, LoggerMessage

### Community 153 - "ConfigureSwaggerOptions"
Cohesion: 0.28
Nodes (7): ApiVersionDescription, IApiVersionDescriptionProvider, IConfigureOptions, OpenApiInfo, IOptions, SwaggerGenOptions, ConfigureSwaggerOptions

### Community 154 - "Setup.Logger.cs"
Cohesion: 0.07
Nodes (21): Host.Infrastructure, elastic_serilog_sinks, healthchecks_ui_client, microsoft_aspnetcore_diagnostics_healthchecks_healthcheckoptions, microsoft_aspnetcore_httpoverrides, microsoft_extensions_diagnostics_healthchecks, opentelemetry_exporter, opentelemetry_metrics (+13 more)

### Community 155 - "ISmsGateway"
Cohesion: 0.13
Nodes (13): CancellationToken, Task, ISmsGateway, SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, SmsMessage (+5 more)

### Community 156 - "Request"
Cohesion: 0.20
Nodes (10): Guid, Request, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken, Password (+2 more)

### Community 157 - "CreateStockLevelOnProductCreatedHandler"
Cohesion: 0.19
Nodes (11): ProductCreatedIntegrationEvent, StoreCreatedIntegrationEvent, DefaultIdType, CancellationToken, DefaultIdType, IFusionCache, ILogger, IOptions (+3 more)

### Community 158 - "OtpOptions"
Cohesion: 0.18
Nodes (11): OtpOptions, DummyCode, EmailQuotaWindowMinutes, ExpirationInMinutes, Length, MaxSendsPerEmailPerWindow, MaxSendsPerPhonePerWindow, PhoneQuotaWindowMinutes (+3 more)

### Community 159 - "InventoryDbContext"
Cohesion: 0.17
Nodes (11): DbContextOptions, DbSet, ILogger, TimeProvider, InventoryDbContext, StockLevels, StockReservations, IApplicationBuilder (+3 more)

### Community 160 - "OtpServiceBase"
Cohesion: 0.21
Nodes (8): OtpCacheEntry, DateTimeOffset, CancellationToken, IFusionCache, SemaphoreSlim, Task, TimeSpan, OtpServiceBase

### Community 161 - "IntegrationEventOutbox"
Cohesion: 0.11
Nodes (14): Common.IntegrationEvents, IIntegrationEventOutbox, IntegrationEventOutbox, HasPending, IReadOnlyList, List, Lock, Setup (+6 more)

### Community 162 - "IEmailGateway"
Cohesion: 0.13
Nodes (13): CancellationToken, Task, IEmailGateway, IConfiguration, IFusionCache, IOptions, IServiceCollection, Setup (+5 more)

### Community 164 - "NotificationsDbContext"
Cohesion: 0.15
Nodes (13): DbSet, INotificationsDbContext, DeviceRegistrations, DbContextOptions, DbSet, ILogger, TimeProvider, NotificationsDbContext (+5 more)

### Community 165 - "CurrentUser"
Cohesion: 0.15
Nodes (12): CurrentUser, Id, IdAsString, IsAuthenticated, Principal, Roles, SessionId, ClaimsPrincipal (+4 more)

### Community 166 - "IamModule"
Cohesion: 0.18
Nodes (10): Action, IApplicationBuilder, IEnumerable, RateLimiterOptions, IamModule, ActivitySourceNames, MeterNames, Name (+2 more)

### Community 167 - "PaginationRequest"
Cohesion: 0.07
Nodes (31): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Stores.v1.My.AuditLog, PaginationRequest, After, IncludeTotal, PageNumber, PageSize, ShouldIncludeTotal (+23 more)

### Community 168 - "Response"
Cohesion: 0.22
Nodes (6): Products.Endpoints.ProductTemplates.v1.Create, RouteGroupBuilder, Setup, RouteGroupBuilder, Response, Id

### Community 169 - "SendSecurityAlertRequestHandler"
Cohesion: 0.27
Nodes (9): SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, CancellationToken, IOptions, RequestLocalizationOptions, Task (+1 more)

### Community 171 - "IInterModuleRequest"
Cohesion: 0.14
Nodes (17): IInterModuleRequest, GetActiveSessionIdsRequest, GetActiveSessionIdsResponse, UserSessionIds, IReadOnlyList, OtpVerificationFailureReason, InvalidOtp, None (+9 more)

### Community 172 - "Stores/v1/Create/Request.cs"
Cohesion: 0.13
Nodes (12): Products.Endpoints.Stores.v1.Create, RouteGroupBuilder, Setup, RouteGroupBuilder, Request, Address, Description, Name (+4 more)

### Community 173 - "ProductTemplateId"
Cohesion: 0.25
Nodes (6): ProductTemplateId, DefaultIdType, ProductTemplateId, CancellationToken, List, Task

### Community 174 - "system_diagnostics"
Cohesion: 0.12
Nodes (14): BackgroundJobs.Telemetry, LoginMethods, SessionRevokedReasons, ActivitySource, Counter, Meter, InventoryTelemetry, ActivitySource (+6 more)

### Community 175 - "PaginationResponse"
Cohesion: 0.07
Nodes (25): Products.Endpoints.Products.v1.AuditLog, JsonElement, AuditLogDto, DateTimeOffset, PaginationResponse, HasNext, HasPrevious, HasTotal (+17 more)

### Community 176 - ".FixedWindow"
Cohesion: 0.23
Nodes (10): FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit, RateLimitPartitions, HttpContext, IConnectionMultiplexer (+2 more)

### Community 177 - "CorsOptions"
Cohesion: 0.25
Nodes (8): CorsOptions, AllowCredentials, AllowedHeaders, AllowedMethods, AllowedOrigins, MaxAgeInSeconds, CorsOptionsValidator, IReadOnlyList

### Community 178 - "PushMessage"
Cohesion: 0.14
Nodes (14): IReadOnlyDictionary, CancellationToken, IReadOnlyList, Task, IPushGateway, PushMessage, CancellationToken, ILogger (+6 more)

### Community 179 - "CachingOptions"
Cohesion: 0.11
Nodes (22): CachingEntryDefaults, Duration, FactoryHardTimeout, FactorySoftTimeout, FailSafeMaxDuration, FailSafeThrottleDuration, CachingOptions, AllowInMemoryOnlyInProduction (+14 more)

### Community 180 - "SmsOptions"
Cohesion: 0.10
Nodes (21): SmsOptions, AppName, AttemptTimeoutSeconds, BaseUrl, MaxPerDay, MaxPerPhoneNumberPerDay, MaxRetryAttempts, MsgHeader (+13 more)

### Community 181 - "ProductTemplates/v1/Create/Request.cs"
Cohesion: 0.40
Nodes (5): Request, Brand, Color, Model, RequestValidator

### Community 183 - "SendRequestBody"
Cohesion: 0.25
Nodes (8): SendMessageBody, IReadOnlyList, SendRequestBody, AppName, Encoding, IysFilter, Messages, MsgHeader

### Community 184 - "RedisOtpService"
Cohesion: 0.12
Nodes (13): OtpCodeGenerator, IFusionCache, IOptions, OtpService, CancellationToken, IConnectionMultiplexer, IOptions, Task (+5 more)

### Community 185 - "IdentityScheme"
Cohesion: 0.24
Nodes (8): IdentityScheme, Email, PhoneNumber, IdentitySchemeOptions, Scheme, IdentitySchemeOptionsValidator, RouteGroupBuilder, Setup

### Community 186 - "Policies"
Cohesion: 0.25
Nodes (6): CreateStoreRateLimitingPolicy, RateLimiterOptions, Policies, Action, IEnumerable, RateLimiterOptions

### Community 187 - "ReverseProxyOptions"
Cohesion: 0.29
Nodes (6): ReverseProxyOptions, ForwardLimit, IsEnabled, TrustedNetworks, ReverseProxyOptionsValidator, IReadOnlyList

### Community 188 - "BoundedCaptureStream"
Cohesion: 0.14
Nodes (8): SeekOrigin, HttpResponse, BoundedCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 189 - "JobClaimExtensions"
Cohesion: 0.28
Nodes (9): IdBound, Value, JobClaimExtensions, CancellationToken, DateTimeOffset, DbSet, Expression, Func (+1 more)

### Community 190 - "ServiceAccountTokenCache"
Cohesion: 0.11
Nodes (11): KeycloakPaths, CancellationToken, DateTimeOffset, SemaphoreSlim, Task, TimeSpan, ServiceAccountTokenCache, AccessToken (+3 more)

### Community 191 - "DeviceRegistryReconcileJobRegistrar"
Cohesion: 0.27
Nodes (8): IHostedService, CancellationToken, ILogger, IOptions, IServiceProvider, LoggerMessage, Task, DeviceRegistryReconcileJobRegistrar

### Community 192 - "Setup"
Cohesion: 0.15
Nodes (9): ForwardedHeadersOptions, LoggerConfiguration, LoggerMinimumLevelConfiguration, IConfiguration, IServiceCollection, Setup, IEnumerable, IHostEnvironment (+1 more)

### Community 193 - ".GetMeAsync"
Cohesion: 0.24
Nodes (7): CancellationToken, IReadOnlyList, Task, IKeycloakPermissionClient, CancellationToken, HttpContext, Task

### Community 194 - "EventDispatcher"
Cohesion: 0.31
Nodes (7): EventDispatcher, ActivitySource, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task

### Community 195 - "IInventoryDbContext"
Cohesion: 0.10
Nodes (16): Inventory.Endpoints.StockReservations.v1.Reserve, DbSet, IInventoryDbContext, StockLevels, StockReservations, CancellationToken, RouteGroupBuilder, Task (+8 more)

### Community 196 - "TokenCreateRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenCreateRateLimitingPolicy (+1 more)

### Community 197 - ".UseModules"
Cohesion: 0.26
Nodes (6): ModuleRegistry, Exception, IApplicationBuilder, ILogger, LoggerMessage, WebApplication

### Community 198 - "InventoryModule"
Cohesion: 0.15
Nodes (10): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, InventoryModule, ActivitySourceNames, MeterNames (+2 more)

### Community 199 - "ReCaptchaResponse"
Cohesion: 0.25
Nodes (7): DateTime, ReCaptchaResponse, ChallengeTs, ErrorCodes, Hostname, Score, Success

### Community 200 - "NotificationsTelemetry"
Cohesion: 0.22
Nodes (5): ActivitySource, Counter, Meter, NotificationsTelemetry, UpDownCounter

### Community 201 - "KeycloakScopes"
Cohesion: 0.20
Nodes (9): Devices, Hangfire, KeycloakScopes, Products, ProductTemplates, Sessions, StockReservations, Stores (+1 more)

### Community 202 - "BaseDbContext"
Cohesion: 0.17
Nodes (10): BaseDbContext, AuditLog, CancellationToken, DateTimeOffset, DbContextOptions, DbSet, ILogger, ModelConfigurationBuilder (+2 more)

### Community 203 - "SmsRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, SmsRateLimitingPolicy (+1 more)

### Community 205 - "StatelessInboxStore"
Cohesion: 0.24
Nodes (6): StatelessInboxStore, Instance, CancellationToken, DateTimeOffset, DefaultIdType, Task

### Community 210 - "BackgroundJobsOptions"
Cohesion: 0.29
Nodes (7): BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount, BackgroundJobsOptionsValidator

### Community 211 - "Key decisions"
Cohesion: 0.29
Nodes (7): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Key decisions

### Community 212 - "Stores/v1/My/Update/Request.cs"
Cohesion: 0.33
Nodes (6): Products.Endpoints.Stores.v1.My.Update, Request, Address, Description, Name, RequestValidator

### Community 213 - "BackgroundJobsTelemetry"
Cohesion: 0.14
Nodes (11): IServerFilter, JobMetricsFilter, PerformedContext, PerformingContext, BackgroundJobsTelemetry, ActivitySource, ConcurrentDictionary, Counter (+3 more)

### Community 214 - "fluentvalidation"
Cohesion: 0.07
Nodes (30): Products.Endpoints.Probe.v1, Common.Application.Validation, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, fluentvalidation, CustomRateLimitingOptionsValidator, FixedWindowValidator, DatabaseOptions, ConnectionString (+22 more)

### Community 215 - "Response"
Cohesion: 0.18
Nodes (10): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, DateTimeOffset, Response, ClientId, DeviceName, Id, IpAddress, IsCurrent (+2 more)

### Community 216 - ".UpsertIfNewerAsync"
Cohesion: 0.12
Nodes (14): Common.Application.Persistence.Projections, ICurrentDbContext, ProjectionUpsertExtensions, Action, CancellationToken, DbContext, DbSet, Func (+6 more)

### Community 217 - "FeatureFlags"
Cohesion: 0.33
Nodes (5): Checkout, FeatureFlags, IAM, Notifications, Products

### Community 218 - ".SendOtp"
Cohesion: 0.08
Nodes (24): SendPhoneOtpRequest, SendPhoneOtpResponse, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled, CancellationToken, Task (+16 more)

### Community 219 - "IBackgroundJobs"
Cohesion: 0.26
Nodes (7): IBackgroundJobs, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 220 - "HttpWarehouseGateway"
Cohesion: 0.31
Nodes (8): ReleaseResponseBody, CancellationToken, HttpClient, HttpResponseMessage, Task, HttpWarehouseGateway, ReleaseRequestBody, ReleaseResponseBody

### Community 221 - "Keycloak realm as code"
Cohesion: 0.25
Nodes (7): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Secrets, Seed users (dev / test only)

### Community 222 - ".ListSessions"
Cohesion: 0.19
Nodes (12): DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, IReadOnlyList, CancellationToken, IReadOnlyCollection, RouteGroupBuilder, Task (+4 more)

### Community 223 - "PolymorphicEventConverter"
Cohesion: 0.25
Nodes (6): UnknownDomainEvent, PolymorphicEventConverter, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 224 - ".MapEndpoint"
Cohesion: 0.40
Nodes (3): RouteGroupBuilder, RouteGroupBuilder, Setup

### Community 225 - "IInboxStore"
Cohesion: 0.12
Nodes (17): IInboxCleanupTarget, ModuleName, IInboxStore, CancellationToken, DateTimeOffset, DefaultIdType, Task, Setup (+9 more)

### Community 226 - "NetGsmSmsGateway"
Cohesion: 0.24
Nodes (9): SendResponseBody, Exception, HttpClient, ILogger, IOptions, JsonSerializerOptions, LoggerMessage, SendRequestBody (+1 more)

### Community 227 - "RegisterRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, RegisterRateLimitingPolicy (+1 more)

### Community 228 - ".AddCommonCaching"
Cohesion: 0.29
Nodes (6): IMemoryCache, Setup, IConfiguration, IConnectionMultiplexer, IServiceCollection, JsonSerializerOptions

### Community 229 - ".AddCommonOptions"
Cohesion: 0.40
Nodes (4): Setup, IConfiguration, IHostEnvironment, IServiceCollection

### Community 230 - "InterModuleRequestHandler"
Cohesion: 0.15
Nodes (14): IInterModuleRequestHandler, CancellationToken, Task, InterModuleRequestHandler, CancellationToken, ConsumeContext, Task, GetUsersInRolePageRequest (+6 more)

### Community 231 - "BackgroundJobsModule"
Cohesion: 0.12
Nodes (14): DashboardContext, IAuthorizationService, IDashboardAsyncAuthorizationFilter, BackgroundJobsModule, ActivitySourceNames, MeterNames, Name, StartupPriority (+6 more)

### Community 232 - "IntegrationEventConverter"
Cohesion: 0.20
Nodes (8): IntegrationEventConverter, JsonSerializerOptions, UtcDateTimeOffsetConverter, DateTimeOffset, DateTimeOffset, ModelConfigurationBuilder, EntityTypeBuilder, ValueConverter

### Community 233 - ".HasPatternIndex"
Cohesion: 0.36
Nodes (6): IndexBuilder, TrigramIndexExtensions, EntityTypeBuilder, Expression, Func, ModelBuilder

### Community 234 - "KeycloakPermissionAuthorizationHandler.cs"
Cohesion: 0.11
Nodes (12): asp_versioning_apiexplorer, Common.Infrastructure.Auth.Services, IAM.Infrastructure.Auth, Notifications.Infrastructure.Hubs, Common.Infrastructure.Auth, hangfire_annotations, hangfire_dashboard, microsoft_aspnetcore_authentication_jwtbearer (+4 more)

### Community 235 - "AuditLogEntry"
Cohesion: 0.13
Nodes (12): AuditLogEntry, AggregateId, AggregateType, Event, EventType, DefaultIdType, ModelBuilder, AuditLogEntryConfiguration (+4 more)

### Community 236 - "V1ProductCreatedDomainEvent"
Cohesion: 0.22
Nodes (7): Products.Application.Products.DomainEventHandlers.v1, CancellationToken, Task, ProductCreatedIntegrationEventPublishingHandler, V1ProductCreatedDomainEventHandlers, ProductId, V1ProductCreatedDomainEvent

### Community 237 - "ProblemDetailsExtensions"
Cohesion: 0.50
Nodes (3): ProblemDetails, ProblemDetailsExtensions, ICollection

### Community 238 - ".UseInfrastructure"
Cohesion: 0.18
Nodes (9): IAuthenticationSchemeProvider, RequestBodyLimitMiddleware, RequestDelegate, IApplicationBuilder, HttpContext, IOptions, RequestDelegate, Task (+1 more)

### Community 239 - "KeycloakTokenClient.cs"
Cohesion: 0.10
Nodes (12): IAM.Infrastructure.Keycloak.Representations, Inventory.Infrastructure.Gateway, IAM.Domain.Captcha, IAM.Infrastructure.Captcha.Services, Inventory.Application.Gateway, IAM.Domain.Errors, IAM.Infrastructure.Keycloak, microsoft_identitymodel_jsonwebtokens (+4 more)

### Community 240 - "RemoveDefaultResponseSchemaFilter"
Cohesion: 0.40
Nodes (4): IOperationFilter, OpenApiOperation, OperationFilterContext, RemoveDefaultResponseSchemaFilter

### Community 241 - "StronglyTypedIdReadOnlyJsonConverter"
Cohesion: 0.24
Nodes (6): JsonConverter, StronglyTypedIdReadOnlyJsonConverter, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 242 - "StrictDateTimeOffsetJsonConverter"
Cohesion: 0.33
Nodes (6): StrictDateTimeOffsetJsonConverter, DateTimeOffset, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 243 - "CustomValidator"
Cohesion: 0.14
Nodes (20): Inventory.Endpoints.StockReservations.v1.Commit, CustomValidator, RequestBody, Request, Body, Id, RequestBody, ProviderReference (+12 more)

### Community 244 - "KeyedResilienceProfile"
Cohesion: 0.15
Nodes (13): AbstractValidator, KeyedResilienceProfile, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, RateLimitFailOpen (+5 more)

### Community 245 - "FirebasePushGateway.cs"
Cohesion: 0.25
Nodes (5): Notifications.Application.Push, Notifications.Infrastructure.Push.Firebase, firebaseadmin, firebaseadmin_messaging, google_apis_auth_oauth2

### Community 246 - "RequestBodyLimitMetadata"
Cohesion: 0.33
Nodes (6): RequestBodyLimitMetadata, MaxBodyBytes, Func, HttpContext, IHttpMaxRequestBodySizeFeature, Task

### Community 247 - "OpenApiOptions"
Cohesion: 0.22
Nodes (9): OpenApiOptions, ContactEmail, ContactName, Description, EnableSwagger, LicenseName, LicenseUrl, Title (+1 more)

### Community 248 - "SendErrorBody"
Cohesion: 0.67
Nodes (3): SendErrorBody, Code, Message

### Community 249 - "DeviceRegistryReconciliationService"
Cohesion: 0.15
Nodes (10): CancellationToken, ILogger, IOptions, LoggerMessage, Task, TimeProvider, DeviceRegistryReconciliationService, IServiceCollection (+2 more)

### Community 250 - "Response"
Cohesion: 0.20
Nodes (8): Products.Endpoints.Products.v1.My.Get, RouteGroupBuilder, Endpoint, Response, Description, Name, Price, Quantity

### Community 251 - "Response"
Cohesion: 0.15
Nodes (10): Products.Endpoints.Products.v1.Get, RouteGroupBuilder, Setup, RouteGroupBuilder, Response, AvailableQuantity, Description, Name (+2 more)

### Community 252 - ".TryWriteAsync"
Cohesion: 0.40
Nodes (4): ProblemDetailsContext, ProblemDetailsServiceExtensions, IProblemDetailsService, Task

### Community 260 - "v1/RemoveProduct/Request.cs"
Cohesion: 0.29
Nodes (7): Products.Endpoints.Stores.v1.RemoveProduct, ProductId, StoreId, Request, Id, ProductId, RequestValidator

### Community 261 - ".GetAuditLogAsync"
Cohesion: 0.24
Nodes (8): CreatedOn, Version, DbContextExtensions, CancellationToken, DateTimeOffset, DbSet, JsonSerializerOptions, Task

### Community 262 - ".SendWithPipelineAsync"
Cohesion: 0.25
Nodes (7): HttpClientKeyedExtensions, CancellationToken, HttpClient, HttpRequestMessage, HttpResponseMessage, ResiliencePipeline, Task

### Community 263 - "Response"
Cohesion: 0.29
Nodes (6): DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 264 - "StronglyTypedIdListReadOnlyJsonConverter"
Cohesion: 0.36
Nodes (6): StronglyTypedIdListReadOnlyJsonConverter, IReadOnlyList, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 265 - "Response"
Cohesion: 0.29
Nodes (5): Products.Endpoints.Stores.v1.My.AddProduct, RouteGroupBuilder, Endpoint, Response, Id

### Community 266 - "SwaggerDefaultValues"
Cohesion: 0.40
Nodes (4): JsonValue, OpenApiOperation, OperationFilterContext, SwaggerDefaultValues

### Community 267 - "AuditLogOptions"
Cohesion: 0.29
Nodes (7): AuditLogOptions, PerSchemaRetentionDays, PurgeBatchSize, RetentionCron, RetentionDays, AuditLogOptionsValidator, Dictionary

### Community 268 - "DevicesOptions"
Cohesion: 0.33
Nodes (6): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator, IReadOnlyCollection

### Community 269 - "IProductsDbContext"
Cohesion: 0.04
Nodes (37): ICurrentUser, Id, IdAsString, Roles, SessionId, ICollection, DbSet, ProductTemplate (+29 more)

### Community 270 - ".SendAsync"
Cohesion: 0.22
Nodes (5): CancellationToken, Task, SendMessageBody, Msg, No

### Community 271 - "SendForLogin/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForLogin, Request, CaptchaToken, PhoneNumber, RequestValidator

### Community 272 - ".AddResilientHttpClient"
Cohesion: 0.22
Nodes (8): HttpStandardResilienceOptions, IHttpClientBuilder, Setup, Action, HttpClient, IOptions, IServiceCollection, IServiceProvider

### Community 273 - ".AddNetGsm"
Cohesion: 0.33
Nodes (5): IConfiguration, IFusionCache, IOptions, IServiceCollection, Setup

### Community 274 - "WebhookCallback/Request.cs"
Cohesion: 0.40
Nodes (5): Inventory.Endpoints.StockReservations.v1.WebhookCallback, Request, ProviderReference, ReservationId, RequestValidator

### Community 275 - "SendForRegistration/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, Request, CaptchaToken, PhoneNumber, RequestValidator

### Community 276 - "EnrichLogsWithUserInfoMiddleware"
Cohesion: 0.33
Nodes (5): IMiddleware, HttpContext, RequestDelegate, Task, EnrichLogsWithUserInfoMiddleware

### Community 277 - "SendResponseBody"
Cohesion: 0.50
Nodes (4): SendResponseBody, Code, Description, JobId

### Community 278 - ".TapWhenFeatureEnabledAsync"
Cohesion: 0.33
Nodes (5): FeatureFlagResultExtensions, Action, Func, IFeatureManager, Task

### Community 279 - "Response"
Cohesion: 0.29
Nodes (6): DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 280 - "SendForEmail/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForEmail, Request, CaptchaToken, Email, RequestValidator

### Community 281 - ".TryReadFromJsonAsync"
Cohesion: 0.40
Nodes (4): HttpContent, CancellationToken, Task, HttpContentJsonExtensions

### Community 282 - "JobHousekeepingOptions"
Cohesion: 0.40
Nodes (5): JobHousekeepingOptions, PageSize, RetentionHours, StaleAfterMinutes, JobHousekeepingOptionsValidator

### Community 283 - "AuditableEntity"
Cohesion: 0.18
Nodes (9): ProjectionEntity, SourceVersion, AuditableEntity, CreatedBy, Id, LastModifiedBy, LastModifiedOn, Version (+1 more)

### Community 284 - "Reserve/Request.cs"
Cohesion: 0.20
Nodes (10): DateTimeOffset, DefaultIdType, RequestBody, Request, Body, RequestBody, ProductId, Quantity (+2 more)

### Community 285 - "RequestBodyLimitFilter.cs"
Cohesion: 0.18
Nodes (10): Common.Endpoints.Webhooks, microsoft_aspnetcore_http_features, RequestBodyLimitEndpointExtensions, RequestBodyLimitFilter, EndpointFilterDelegate, EndpointFilterInvocationContext, Func, HttpContext (+2 more)

### Community 286 - ".UpdateProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 287 - "IOtpService"
Cohesion: 0.15
Nodes (14): IssueVerificationTokenRequest, IssueVerificationTokenResponse, CancellationToken, Task, TimeSpan, IOtpService, OtpVerificationOutcome, InvalidOtp (+6 more)

### Community 288 - "DummySmsGateway"
Cohesion: 0.38
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, DummySmsGateway

### Community 289 - ".DeactivateProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 290 - ".RemoveProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 291 - ".UpdateStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 294 - "UpdateCurrentPushToken/Request.cs"
Cohesion: 0.50
Nodes (4): Notifications.Infrastructure.Devices.UpdateCurrentPushToken, Request, PushToken, RequestValidator

### Community 295 - "ProjectionReconciliationOptions.cs"
Cohesion: 0.50
Nodes (4): ProjectionReconciliationOptions, MaxPages, PageSize, ProjectionReconciliationOptionsValidator

### Community 296 - "SecurityHeadersOptions.cs"
Cohesion: 0.50
Nodes (4): SecurityHeadersOptions, Headers, SecurityHeadersOptionsValidator, Dictionary

### Community 297 - ".SetConcurrency"
Cohesion: 0.50
Nodes (3): IBusFactoryConfigurator, ConcurrencyConfiguratorExtensions, IReceiveEndpointConfigurator

### Community 298 - "Setup"
Cohesion: 0.33
Nodes (4): IApplicationBuilder, IWebHostEnvironment, Type, Setup

### Community 299 - ".MapEndpoint"
Cohesion: 0.40
Nodes (3): RouteGroupBuilder, RouteGroupBuilder, Setup

### Community 300 - "ResultTelemetryExtensions"
Cohesion: 0.38
Nodes (4): Activity, ResultTelemetryExtensions, ActivitySource, Task

### Community 302 - ".EmailVerificationTokenValidation"
Cohesion: 0.50
Nodes (3): IResxLocalizer, IRuleBuilder, IRuleBuilderOptions

### Community 303 - "DefaultResponsesOperationFilter"
Cohesion: 0.70
Nodes (3): OpenApiOperation, OperationFilterContext, DefaultResponsesOperationFilter

### Community 304 - ".SetRetryAfterHeader"
Cohesion: 0.40
Nodes (4): RetryAfterHeaderExtensions, HttpResponse, RateLimitLease, TimeSpan

### Community 305 - ".AddCustomSwagger"
Cohesion: 0.22
Nodes (7): IOpenApiSchema, ISchemaFilter, SchemaFilterContext, IConfigureOptions, IServiceCollection, SwaggerGenOptions, StronglyTypedIdSchemaFilter

### Community 306 - ".PhoneNumberValidation"
Cohesion: 0.50
Nodes (3): IResxLocalizer, IRuleBuilder, IRuleBuilderOptions

### Community 307 - "SignalROptions"
Cohesion: 0.40
Nodes (5): SignalROptions, RedisConnectionString, RequireRedisBackplaneInProduction, UseRedisBackplane, SignalROptionsValidator

### Community 308 - "PublishOutcome"
Cohesion: 0.50
Nodes (4): PublishOutcome, Failed, Published, Skipped

### Community 310 - "VerifyEmail/Request.cs"
Cohesion: 0.50
Nodes (4): Request, Email, Otp, RequestValidator

### Community 311 - "Common.Application.ModelBinders"
Cohesion: 0.08
Nodes (24): Products.Endpoints.ProductTemplates.v1.Deactivate, Inventory.Endpoints.StockReservations.v1.Release, Common.Application.ModelBinders, Products.Endpoints.ProductTemplates.v1.Activate, IModelBinder, microsoft_aspnetcore_mvc_modelbinding, ModelBindingContext, StronglyTypedIdBinder (+16 more)

### Community 319 - "Products/v1/Update/Request.cs"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Products.v1.Update, RequestBody, Request, Body, Id, RequestBody, Description, Name (+3 more)

### Community 324 - "Products/v1/My/Update/Request.cs"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Products.v1.My.Update, RequestBody, Request, Body, Id, RequestBody, Description, Name (+3 more)

## Knowledge Gaps
- **951 isolated node(s):** `Id`, `IdAsString`, `Roles`, `SessionId`, `JwtClaimNames` (+946 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 2231 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **100 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `ApplyAuditingInterceptor.cs`, `AuditLogOptions`, `DevicesOptions`, `Common.Domain.ResultMonad`, `EmailOptions`, `OutboxOptions`, `microsoft_extensions_options`, `JobHousekeepingOptions`, `Setup.Logger.cs`, `OtpOptions`, `ObservabilityOptions`, `ProjectionReconciliationOptions.cs`, `SecurityHeadersOptions.cs`, `KeycloakOptions`, `microsoft_entityframeworkcore`, `CorsOptions`, `CachingOptions`, `SignalROptions`, `SmsOptions`, `IdentityScheme`, `ReverseProxyOptions`, `RequestLoggingOptions`, `FullTextSearchOptions`, `BackgroundJobsOptions`, `fluentvalidation`, `PushOptions`, `Program.cs`, `ResiliencyOptions`, `CaptchaOptions`, `Common.InterModuleRequests.Contracts`, `RabbitMqOptions`, `KeycloakPermissionAuthorizationHandler.cs`, `KeycloakTokenClient.cs`, `FirebasePushGateway.cs`, `OpenApiOptions`, `InventoryOptions`, `.AddCustomHealthChecks`?**
  _High betweenness centrality (0.164) - this node is a cross-community bridge._
- **Why does `Result` connect `Result` to `.ReserveSeriesAsync`, `.GetAuditLogAsync`, `FirebasePushGateway`, `Error`, `ProductTemplate`, `ApplicationUserId`, `IProductsDbContext`, `.SendAsync`, `Response`, `.RegisterAsync`, `Inventory.Domain.StockReservations.DomainEvents.v1`, `.CreateTokensByEmail`, `.TapWhenFeatureEnabledAsync`, `EmailMessage`, `.RevokeToken`, `ISmsGateway`, `.UpdateProductAsync`, `.SingleAsResult`, `.RefreshToken`, `IEmailGateway`, `DummySmsGateway`, `.DeactivateProductTemplateAsync`, `.RemoveProductAsync`, `.UpdateStoreAsync`, `ResultTelemetryExtensions`, `PaginationQueryableExtensions`, `PaginationResponse`, `Response`, `PushMessage`, `ReCaptchaService`, `.SendOtp`, `BrevoEmailGateway`, `.SearchProductTemplatesAsync`, `KeycloakTokenClient`, `.SendCoreAsync`, `.GetMeAsync`, `IInventoryDbContext`, `StockReservation`, `Response`, `.SaveChangesAsync`, `.SearchStoresAsync`, `ICaptchaService`, `DomainEvent`, `.SendOtp`, `IInterModuleRequestClient`, `HttpWarehouseGateway`, `.ListSessions`, `.SearchMyProductsAsync`, `NetGsmSmsGateway`, `.SearchStoreProductsAsync`, `.RegisterAsync`, `.IsRegisteredAsync`, `.ReleaseStockReservationAsync`, `Response`, `InventoryOptions`, `Response`?**
  _High betweenness centrality (0.137) - this node is a cross-community bridge._
- **Why does `ApplicationUserId` connect `ApplicationUserId` to `NotificationPayload`, `Seeder`, `Common.Domain.StronglyTypedIds`, `StoreId`, `KeycloakAdminClient`, `AuditableEntityResponse`, `IProductsDbContext`, `IStronglyTypedId`, `KeycloakPermissionAuthorizationHandler`, `Response`, `.RegisterAsync`, `DeviceRegistration`, `.RevokeToken`, `V1StoreCreatedDomainEvent`, `AuditableEntity`, `CreateStockLevelOnProductCreatedHandler`, `JobRow`, `For`, `CurrentUser`, `PaginationRequest`, `SendSecurityAlertRequestHandler`, `IInterModuleRequest`, `Stores/v1/Create/Request.cs`, `PaginationResponse`, `Response`, `Common.Application.ModelBinders`, `KeycloakTokenClient`, `Response`, `IAggregateRoot`, `.Configure`, `DomainEvent`, `IInterModuleRequestClient`, `.ListSessions`, `InterModuleRequestHandler`, `.RegisterAsync`, `Response`, `Response`?**
  _High betweenness centrality (0.074) - this node is a cross-community bridge._
- **What connects `Id`, `IdAsString`, `Roles` to the rest of the system?**
  _951 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `.ReserveSeriesAsync` be split into smaller, more focused modules?**
  _Cohesion score 0.10869565217391304 - nodes in this community are weakly interconnected._
- **Should `KeycloakAdminClient` be split into smaller, more focused modules?**
  _Cohesion score 0.09013914095583787 - nodes in this community are weakly interconnected._
- **Should `RedisFixedWindowRateLimiter` be split into smaller, more focused modules?**
  _Cohesion score 0.13333333333333333 - nodes in this community are weakly interconnected._