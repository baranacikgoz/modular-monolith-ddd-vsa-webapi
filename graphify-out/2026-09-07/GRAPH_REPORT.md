# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-09-04)

## Corpus Check
- 466 files · ~68,524 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 3741 nodes · 6451 edges · 354 communities (249 shown, 102 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 186 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `389abbf1`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- Common.Domain.ResultMonad
- NotificationPayload
- OutboxProcessor
- IAMDbContext
- Hybrid DDD (Writes) / VSA (Reads)
- Session
- RedisFixedWindowRateLimiter
- ApplyAuditingInterceptor
- FirebasePushGateway
- Request
- Error
- Common.Application.Auth
- .RegisterAndLoginAsync
- .SendOtp
- LocalizedIdentityErrorDescriber
- Common.Infrastructure.Persistence
- ISmsGateway
- .UseModule
- DomainEvent
- AuditableEntity
- IAM.Endpoints.Common.Validations
- BoundedRequestCaptureStream
- IDatabaseSeeder
- NotificationsModule
- .RevokeSession
- NetGsmSmsGateway
- IEvent
- RequestResponseBodyLoggingMiddleware
- ProblemDetails
- Result
- Product
- ApplicationUserId
- .CreateStoreAsync
- .AddProductAsync
- .AddIdentityInfrastructure
- OutboxMessage
- CustomRateLimitingOptions
- Seeder
- .RefreshToken
- ObservabilityOptions
- SmsRateLimitingPolicy
- Common.Application.DTOs
- .SaveWithOutboxAsync
- CreateStoreRateLimitingPolicy
- ApplicationUser
- .CreateTokens
- ProductsModule.cs
- Outbox Misuse Check
- IOtpService
- Add Integration Event Command
- .SearchUsersAsync
- Common.Domain.Events
- ReCaptchaService
- Seeder
- Notifications.Infrastructure.Telemetry
- .FixedWindow
- DummySmsGateway
- Cross-Module Reference Violation
- OutboxMetricsJob
- .SearchProductTemplatesAsync
- Bogus Test Data
- .AddProductToMyStoreAsync
- ResultToResponseTransformer
- RequestLoggingOptions
- Common.InterModuleRequests
- Common.Application.Options
- OutboxModule
- Response
- ValueObject
- V1StoreCreatedDomainEvent
- StrictDateTimeOffsetJsonConverter
- Full-Text Search
- Response
- .WriteAsync
- AsNoTracking Coverage Check
- My/RemoveProduct/Request.cs
- Common.Application.Validation
- .CreateMyStoreAsync
- TokenCreateRateLimitingPolicy
- Common.Application.EventBus
- EventDispatcher
- .SearchStoresAsync
- StringExtensions
- .EnsureNoMigrationsPending
- .Configure
- ResultTelemetryExtensions
- Store
- SessionTokenReuseDetectedSignalRHandler
- Configuration-Driven Module Registration
- Setup
- .SendAsync
- ICaptchaService
- Common.Domain.StronglyTypedIds
- .GetClientKey
- Notifications.Application.Push
- .SearchMyProductsAsync
- DatabaseSeederOrchestrator
- AuditLogRetentionService
- .SearchStoreProductsAsync
- CustomPermissions
- OutboxDbContext
- ResiliencyOptions
- HttpContextTargetingContextAccessor
- BackgroundJobsTelemetry
- IntegrationEvent
- IIAMDbContext
- OutboxModule.cs
- V1AllSessionsRevokedDomainEvent
- RefreshToken
- .IsRegisteredAsync
- ProductsDbContext
- .Schedule
- OutboxCleanupJob
- CaptchaOptions
- Response
- .AddServices
- .TryWriteAsync
- ApiKeyAuthenticationHandler
- IModule
- ProductsModule
- IAM.Infrastructure.Auth.ApiKey
- .GetVariantAsync
- TokenRefreshRateLimitingPolicy
- FullTextSearchOptions
- HealthCheckOptions
- .TapWhenFeatureEnabledAsync
- GlobalExceptionHandlingMiddleware
- BaseDbContext
- AuditLogEntry
- Endpoint
- docker-compose.yml (Base Stack)
- .AddInfrastructure
- .ActivateProductTemplateAsync
- Consumer Idempotency (IntegrationEventHandlerBase)
- .RemoveProductAsync
- Response
- PersistenceQueryableExtensions
- .WriteTooManyRequestsToResponse
- .InvokeAsync
- ProductTemplate
- Response
- Setup
- Response
- IDbContext
- AuditableEntityResponse
- Products.Endpoints.Probe
- IAM.Endpoints.Otp.VersionNeutral
- RecurringBackgroundJobsService
- .AddModules
- .HandleAsync
- FeatureFlags
- OutboxOptions
- ISearchLanguageResolver
- SwaggerDefaultValues
- Request
- InterModuleRequestHandler
- Products.Domain.Products
- .UseModules
- .AddObservability
- .Capture
- OtpServiceBase
- .UpdateMyStoreAsync
- UtcDateTimeOffsetConverter
- For
- Request
- ICurrentUser
- Endpoint
- PaginationRequest
- CachedCaptchaService
- Policies
- Notifications.Application/IAssemblyReference.cs
- .UpdateStoreAsync
- ResxLocalizationOptions
- Request
- TokensDto
- PaginationResponse
- ApiKeyEntry
- .AddAuthInfrastructure
- OtpOptions
- CachingOptions
- SmsOptions
- RabbitMqOptions
- IAM.Domain
- SendRequestBody
- SignalROptions
- IamModule
- Request
- ReverseProxyOptions
- BoundedCaptureStream
- V1UserRegisteredDomainEvent
- Split-Deployment PoC
- UserRegisteredSignalRHandler
- Request
- CorsOptions
- NotificationsHub
- JwtOptions
- IntegrationEventHandlerBase
- SecurityHeadersMiddleware
- OpenApiOptions
- KeyValuePair
- GetPushTokensRequest
- Request
- IProductsDbContext
- Configuration-Driven Module Loading
- Infrastructure/StringExtensions.cs
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- ConfigureSwaggerOptions
- .CreateProductTemplateAsync
- .SeedUser
- ProductsTelemetry
- .AddOtpServices
- RedisOtpService
- ProductTemplateId
- Request
- .GetStoreAsync
- .SaveChangesAsync
- .SeedProductAsync
- Response
- Endpoint
- ReCaptchaResponse
- .SingleAsResult
- BackgroundJobsOptions
- GetSeedUserIdsRequest
- BackgroundJobsModule
- CustomValidator
- .AddCommonOptions
- .HandleChallengeAsync
- Request
- .AddCommonCaching
- .AddNotificationsSignalR
- Setup
- V1SessionRevokedDomainEvent
- SmsMessage
- ProductsDatabaseSeeder
- SendForLogin/Request.cs
- Host.Swagger
- CheckRegistration/Request.cs
- SendForRegistration/Request.cs
- StronglyTypedIdSchemaFilter
- ActionsAndResources.cs
- CustomRoles.cs
- RemoveDefaultResponseSchemaFilter
- StoreId
- .GetProductAsync
- .UpdateMyProductAsync
- Request
- Request
- Response
- .RemoveMyProductAsync
- Products.Domain/IAssemblyReference.cs
- IAM.Application/IAssemblyReference.cs
- Products.Infrastructure/IAssemblyReference.cs
- Products.Application/IAssemblyReference.cs
- Common.Infrastructure/IAssemblyReference.cs
- Notifications.Domain/IAssemblyReference.cs
- IAM.Infrastructure/IAssemblyReference.cs
- Common.Application.Caching
- Deactivate/Request.cs
- Refresh/Request.cs
- UpdatePushToken/Request.cs
- StronglyTypedIdBinder
- AuditLogOptions
- ModulesOptions
- SecurityHeadersOptions
- .CreateAsync
- HttpContextExtensions.cs
- DefaultResponsesOperationFilter
- Response
- .SeedProductTemplatesAsync
- .SeedStoresAsync
- .ToResult
- .AddCustomSwagger
- .PhoneNumberValidation
- SendResponseBody
- IAM.Endpoints
- .HandleAsync
- .AddServices
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
- Implement Endpoint Command
- CustomValidator (FluentValidation)
- REPR Pattern
- Setup.cs Endpoint Registration
- SingleAsResultAsync
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
- Run Quality Gate Command
- NetArchTest Architecture Tests
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
- Scaffold Tests (Red-Phase) Command
- Red Baseline (TDD)
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
- Makefile Test/Build Targets
- Develop as Monolith, Deploy as Microservices
- ICoreModule vs IModule Tiers
- MassTransitInterModuleRequestClient
- Each Module Owns Its Own DbContext
- Deploy-Time Materialized Config

## God Nodes (most connected - your core abstractions)
1. `Common.Application.Options` - 100 edges
2. `Result` - 91 edges
3. `Common.Domain.ResultMonad` - 72 edges
4. `ApplicationUserId` - 72 edges
5. `Common.Domain.StronglyTypedIds` - 66 edges
6. `CustomValidator` - 63 edges
7. `Common.Application.Auth` - 60 edges
8. `Common.Application.Validation` - 57 edges
9. `Common.Application.Extensions` - 51 edges
10. `ApplicationUser` - 50 edges

## Surprising Connections (you probably didn't know these)
- `Aspire Dashboard Service (mm.aspire-dashboard)` --conceptually_related_to--> `Observability (OpenTelemetry)`  [INFERRED]
  docker-compose.yml → CLAUDE.md
- `CaptchaErrors` --references--> `Error`  [EXTRACTED]
  src/Modules/IAM/IAM.Domain/Captcha/CaptchaErrors.cs → src/Common/Common.Domain/ResultMonad/Error.cs
- `PushErrors` --references--> `Error`  [EXTRACTED]
  src/Modules/Notifications/Notifications.Application/Push/PushErrors.cs → src/Common/Common.Domain/ResultMonad/Error.cs
- `AuditLogDto` --references--> `ApplicationUserId`  [EXTRACTED]
  src/Common/Common.Application/AuditLog/AuditLogDto.cs → src/Common/Common.Domain/StronglyTypedIds/ApplicationUserId.cs
- `ICurrentUser` --references--> `ApplicationUserId`  [EXTRACTED]
  src/Common/Common.Application/Auth/ICurrentUser.cs → src/Common/Common.Domain/StronglyTypedIds/ApplicationUserId.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Local Infrastructure Stack** — docker_compose_postgres, docker_compose_rabbitmq, docker_compose_redis, docker_compose_aspire_dashboard [EXTRACTED 1.00]

## Communities (354 total, 102 thin omitted)

### Community 0 - "Common.Domain.ResultMonad"
Cohesion: 0.08
Nodes (22): Notifications.Application.Otp, IAM.Application.Tokens.Services, IAM.Application.Extensions, Common.Application.FeatureManagement, IAM.Endpoints.Otp, IAM.Domain.Captcha, Common.InterModuleRequests.Contracts, IAM.Domain.Identity (+14 more)

### Community 1 - "NotificationPayload"
Cohesion: 0.21
Nodes (12): IHubContext, CancellationToken, IReadOnlyList, Task, INotificationDispatcher, Task, INotificationsClient, NotificationPayload (+4 more)

### Community 2 - "OutboxProcessor"
Cohesion: 0.22
Nodes (11): IPublishEndpoint, CancellationToken, Exception, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task (+3 more)

### Community 3 - "IAMDbContext"
Cohesion: 0.11
Nodes (17): IdentityDbContext, CancellationToken, DbContextOptions, DbSet, IdentityRole, IdentityRoleClaim, IdentityUserClaim, IdentityUserLogin (+9 more)

### Community 5 - "Session"
Cohesion: 0.08
Nodes (27): DateTimeOffset, Guid, Guid, V1SessionCreatedDomainEvent, DateTimeOffset, Guid, IReadOnlyCollection, List (+19 more)

### Community 6 - "RedisFixedWindowRateLimiter"
Cohesion: 0.13
Nodes (16): RateLimiter, RateLimiterStatistics, RateLimitLease, FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration (+8 more)

### Community 7 - "ApplyAuditingInterceptor"
Cohesion: 0.10
Nodes (17): SaveChangesInterceptor, ApplyAuditingInterceptor, CancellationToken, DbContextEventData, InterceptionResult, TimeProvider, ValueTask, ApplySearchLanguageInterceptor (+9 more)

### Community 8 - "FirebasePushGateway"
Cohesion: 0.05
Nodes (39): FirebaseApp, FirebaseMessaging, IReadOnlyDictionary, FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId (+31 more)

### Community 9 - "Request"
Cohesion: 0.15
Nodes (14): Products.Endpoints.Products.v1.My.Update, RequestBody, Request, Body, Id, RequestBody, Description, Name (+6 more)

### Community 10 - "Error"
Cohesion: 0.09
Nodes (17): HttpStatusCode, StringLocalizerExtensions, IStringLocalizer, Error, Key, ParameterName, StatusCode, SubErrors (+9 more)

### Community 11 - "Common.Application.Auth"
Cohesion: 0.16
Nodes (11): Common.Application.Search, Products.Endpoints.Stores.v1.Search, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Products.Infrastructure.Telemetry, Products.Application.Persistence, Products.Domain.Stores, Common.Application.Pagination (+3 more)

### Community 12 - ".RegisterAndLoginAsync"
Cohesion: 0.14
Nodes (18): IInterModuleRequestClient, OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, CancellationToken (+10 more)

### Community 13 - ".SendOtp"
Cohesion: 0.09
Nodes (20): RequestLocalizationOptions, IInterModuleRequest, SendPhoneOtpRequest, SendPhoneOtpResponse, CancellationToken, Task, CancellationToken, IFeatureManager (+12 more)

### Community 14 - "LocalizedIdentityErrorDescriber"
Cohesion: 0.13
Nodes (5): IAM.Infrastructure.Identity, IdentityError, IdentityErrorDescriber, IResxLocalizer, LocalizedIdentityErrorDescriber

### Community 15 - "Common.Infrastructure.Persistence"
Cohesion: 0.08
Nodes (11): Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Common.Application.Persistence, Common.Infrastructure.Localization, IAM.Infrastructure.Persistence, IAM.Infrastructure.Persistence.Seeding, Common.Infrastructure.Auth.Services, Common.Infrastructure.EventBus (+3 more)

### Community 16 - "ISmsGateway"
Cohesion: 0.14
Nodes (13): CancellationToken, Task, ISmsGateway, IConfiguration, IFusionCache, IOptions, IServiceCollection, Setup (+5 more)

### Community 17 - ".UseModule"
Cohesion: 0.22
Nodes (6): DashboardContext, IDashboardAsyncAuthorizationFilter, IApplicationBuilder, IOptions, HangfireCustomAuthorizationFilter, Task

### Community 18 - "DomainEvent"
Cohesion: 0.05
Nodes (34): AggregateRoot, Events, Id, Version, IReadOnlyCollection, List, IAggregateRoot, Events (+26 more)

### Community 19 - "AuditableEntity"
Cohesion: 0.14
Nodes (14): AuditableEntity, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, Version, DateTimeOffset (+6 more)

### Community 20 - "IAM.Endpoints.Common.Validations"
Cohesion: 0.29
Nodes (5): IAM.Endpoints.Common.Validations, CommonValidations, IResxLocalizer, IRuleBuilder, IRuleBuilderOptions

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.14
Nodes (8): SeekOrigin, BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position, Stream

### Community 22 - "IDatabaseSeeder"
Cohesion: 0.12
Nodes (13): IDatabaseSeeder, Priority, CancellationToken, Task, CancellationToken, IServiceScopeFactory, Task, IamDatabaseSeeder (+5 more)

### Community 23 - "NotificationsModule"
Cohesion: 0.15
Nodes (10): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, NotificationsModule, ActivitySourceNames, MeterNames (+2 more)

### Community 24 - ".RevokeSession"
Cohesion: 0.11
Nodes (14): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint, CancellationToken, RouteGroupBuilder, Task (+6 more)

### Community 25 - "NetGsmSmsGateway"
Cohesion: 0.16
Nodes (14): SendRequestBody, SendResponseBody, CancellationToken, Exception, HttpClient, ILogger, IOptions, JsonSerializerOptions (+6 more)

### Community 26 - "IEvent"
Cohesion: 0.14
Nodes (11): CancellationToken, Task, CancellationToken, Task, CancellationToken, Task, IEvent, CreatedOn (+3 more)

### Community 27 - "RequestResponseBodyLoggingMiddleware"
Cohesion: 0.22
Nodes (7): IDiagnosticContext, HttpContext, IList, IOptions, PathString, RequestDelegate, RequestResponseBodyLoggingMiddleware

### Community 28 - "ProblemDetails"
Cohesion: 0.23
Nodes (8): ProblemDetails, ResxLocalizer, EndpointFilterDelegate, EndpointFilterInvocationContext, IStringLocalizer, ValueTask, ProblemDetailsExtensions, ICollection

### Community 29 - "Result"
Cohesion: 0.15
Nodes (12): Result, Error, IsFailure, Value, AsyncExtensions, SyncExtensions, Action, Func (+4 more)

### Community 30 - "Product"
Cohesion: 0.08
Nodes (26): DefaultIdType, ProductTemplate, ProductTemplateId, Store, StoreId, Product, Description, Language (+18 more)

### Community 31 - "ApplicationUserId"
Cohesion: 0.10
Nodes (25): IEntityTypeConfiguration, ApplicationUserId, IsEmpty, Value, DefaultIdType, StronglyTypedIdValueConverter, DefaultIdType, EntityTypeBuilder (+17 more)

### Community 32 - ".CreateStoreAsync"
Cohesion: 0.14
Nodes (10): Products.Endpoints.Stores, Store, RouteGroupBuilder, Setup, CancellationToken, RouteGroupBuilder, Task, Endpoint (+2 more)

### Community 33 - ".AddProductAsync"
Cohesion: 0.22
Nodes (8): CancellationToken, ProductTemplate, RouteGroupBuilder, Store, Task, Endpoint, Response, Id

### Community 34 - ".AddIdentityInfrastructure"
Cohesion: 0.50
Nodes (3): IdentityRole, IServiceCollection, Setup

### Community 35 - "OutboxMessage"
Cohesion: 0.09
Nodes (20): IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, DateTimeOffset, OutboxMessage (+12 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.10
Nodes (18): CheckRegistrationRateLimitingPolicy, RegisterRateLimitingPolicy, SmsRateLimitingPolicy, CustomRateLimitingOptions, CheckRegistration, CreateStore, ExemptPathPrefixes, Global (+10 more)

### Community 37 - "Seeder"
Cohesion: 0.15
Nodes (12): RoleManager, IdentityRole, ILogger, Task, UserManager, IdentityRole, ILogger, LoggerMessage (+4 more)

### Community 38 - ".RefreshToken"
Cohesion: 0.12
Nodes (15): CancellationToken, HttpContext, ILogger, IOptions, LoggerMessage, RouteGroupBuilder, Task, TimeProvider (+7 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.08
Nodes (23): IHostBuilder, LoggerConfiguration, LoggerMinimumLevelConfiguration, ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics (+15 more)

### Community 40 - "SmsRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, SmsRateLimitingPolicy (+1 more)

### Community 41 - "Common.Application.DTOs"
Cohesion: 0.14
Nodes (6): IAM.Endpoints.Users.VersionNeutral.Search, Products.Endpoints.ProductTemplates.v1.Search, Common.Application.DTOs, Products.Endpoints.Stores.v1.Get, IAM.Endpoints.Users.VersionNeutral.Me.Get, Products.Endpoints.Stores.v1.My.Get

### Community 42 - ".SaveWithOutboxAsync"
Cohesion: 0.27
Nodes (9): OutboxSaveHelper, CancellationToken, DbContext, Exception, Func, ILogger, LoggerMessage, Task (+1 more)

### Community 43 - "CreateStoreRateLimitingPolicy"
Cohesion: 0.14
Nodes (12): CancellationToken, Func, HttpContext, IOptions, IProblemDetailsService, IResxLocalizer, OnRejectedContext, RateLimitPartition (+4 more)

### Community 44 - "ApplicationUser"
Cohesion: 0.09
Nodes (22): IdentityUser, IReadOnlyCollection, List, ApplicationUser, Events, DateTimeOffset, CreatedBy, CreatedOn (+14 more)

### Community 45 - ".CreateTokens"
Cohesion: 0.09
Nodes (23): IAM.Infrastructure.Tokens, IAM.Infrastructure.Tokens.Services, accessToken, DateTimeOffset, expiresAt, ICollection, refreshTokenBytes, ITokenService (+15 more)

### Community 46 - "ProductsModule.cs"
Cohesion: 0.17
Nodes (7): ApiVersionSet, Common.Endpoints.Versioning, Products.Endpoints, Setup, IEndpointRouteBuilder, IServiceCollection, IAssemblyReference

### Community 48 - "IOtpService"
Cohesion: 0.21
Nodes (8): CancellationToken, Task, TimeSpan, IOtpService, OtpVerificationOutcome, InvalidOtp, Success, TooManyAttempts

### Community 50 - ".SearchUsersAsync"
Cohesion: 0.11
Nodes (16): Constants, CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Request (+8 more)

### Community 51 - "Common.Domain.Events"
Cohesion: 0.15
Nodes (5): Products.Domain.Products.DomainEvents.v1, Common.Domain.Events, Products.Endpoints.ProductTemplates.v1.Get, Products.Domain.ProductTemplates, Products.Domain.Stores.DomainEvents.v1

### Community 52 - "ReCaptchaService"
Cohesion: 0.21
Nodes (10): FormUrlEncodedContent, ReCaptchaResponse, CancellationToken, Exception, HttpClient, ILogger, IOptions, LoggerMessage (+2 more)

### Community 53 - "Seeder"
Cohesion: 0.29
Nodes (6): Products.Infrastructure.Persistence.Seeding, Common.InterModuleRequests.IAM, ILogger, LoggerMessage, ProductsDbContext, Seeder

### Community 54 - "Notifications.Infrastructure.Telemetry"
Cohesion: 0.16
Nodes (8): Notifications.Application.Sms, Notifications.Infrastructure.Sms, Notifications.Infrastructure.Telemetry, Notifications.Infrastructure.Hubs, Notifications.Application.Hubs, Notifications.Infrastructure.Sms.NetGsm, Notifications.Infrastructure, IAssemblyReference

### Community 55 - ".FixedWindow"
Cohesion: 0.08
Nodes (24): IRateLimiterPolicy, RateLimitPartitions, HttpContext, IConnectionMultiplexer, ILoggerFactory, RateLimitPartition, CancellationToken, Func (+16 more)

### Community 56 - "DummySmsGateway"
Cohesion: 0.38
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, DummySmsGateway

### Community 58 - "OutboxMetricsJob"
Cohesion: 0.12
Nodes (14): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxMetricsJob (+6 more)

### Community 59 - ".SearchProductTemplatesAsync"
Cohesion: 0.18
Nodes (10): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Brand (+2 more)

### Community 61 - ".AddProductToMyStoreAsync"
Cohesion: 0.12
Nodes (16): CancellationToken, ProductTemplate, RouteGroupBuilder, Store, Task, Endpoint, ProductTemplateId, Request (+8 more)

### Community 62 - "ResultToResponseTransformer"
Cohesion: 0.26
Nodes (8): Common.Application.EndpointFilters, IEndpointFilter, ResultToCreatedResponseTransformer, ResultToResponseTransformer, IServiceProvider, IWebHostEnvironment, RouteHandlerBuilderExtensions, RouteHandlerBuilder

### Community 63 - "RequestLoggingOptions"
Cohesion: 0.11
Nodes (20): IPostConfigureOptions, RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes (+12 more)

### Community 64 - "Common.InterModuleRequests"
Cohesion: 0.29
Nodes (4): Common.InterModuleRequests, IAssemblyReference, Setup, IServiceCollection

### Community 65 - "Common.Application.Options"
Cohesion: 0.20
Nodes (8): Common.Infrastructure.RateLimiting, Common.Infrastructure.Extensions, Common.Application.Options, Common.Infrastructure.Resiliency, IAM.Infrastructure.RateLimiting, IAM.Infrastructure.Captcha, Setup, Policies

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
Cohesion: 0.36
Nodes (7): CancellationToken, Task, SimulateSomeBusinessHandler, StoreCreatedIntegrationEventPublishingHandler, V1StoreCreatedDomainEventHandlers, StoreId, V1StoreCreatedDomainEvent

### Community 70 - "StrictDateTimeOffsetJsonConverter"
Cohesion: 0.06
Nodes (29): JsonConverter, StrictDateTimeOffsetJsonConverter, DateTimeOffset, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, StronglyTypedIdListReadOnlyJsonConverter (+21 more)

### Community 71 - "Full-Text Search"
Cohesion: 0.08
Nodes (25): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Add a new language/culture, Add search to a new entity _(Build checklist)_ (+17 more)

### Community 72 - "Response"
Cohesion: 0.15
Nodes (12): CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, Id, RequestValidator, Response (+4 more)

### Community 73 - ".WriteAsync"
Cohesion: 0.33
Nodes (5): Memory, ReadOnlyMemory, CancellationToken, Task, ValueTask

### Community 75 - "My/RemoveProduct/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Stores.v1.My.RemoveProduct, Request, Id, RequestValidator

### Community 76 - "Common.Application.Validation"
Cohesion: 0.15
Nodes (12): Common.Application.Validation, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, Products.Endpoints.ProductTemplates.v1.Activate, InterModuleRequestOptions, TimeoutSeconds, InterModuleRequestOptionsValidator, Request, Id (+4 more)

### Community 77 - ".CreateMyStoreAsync"
Cohesion: 0.19
Nodes (9): Products.Endpoints.Stores.v1.My.Create, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator, Response (+1 more)

### Community 78 - "TokenCreateRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenCreateRateLimitingPolicy (+1 more)

### Community 79 - "Common.Application.EventBus"
Cohesion: 0.13
Nodes (14): Common.IntegrationEvents, Notifications.Application.IntegrationEventHandlers, IAM.Application.Users.DomainEventHandlers.v1, Products.Application.Stores.DomainEventHandlers.v1, Common.Application.EventBus, DomainEventHandlerBase, IEventHandler, IEventHandlerWrapper (+6 more)

### Community 80 - "EventDispatcher"
Cohesion: 0.20
Nodes (9): EventDispatcher, ActivitySource, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task, Setup (+1 more)

### Community 81 - ".SearchStoresAsync"
Cohesion: 0.11
Nodes (18): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Request, Address (+10 more)

### Community 82 - "StringExtensions"
Cohesion: 0.29
Nodes (3): Common.Domain.Extensions, SearchValues, StringExtensions

### Community 83 - ".EnsureNoMigrationsPending"
Cohesion: 0.30
Nodes (6): AutoMigrateMarker, IAutoMigrateMarker, MigrationGuard, ILogger, IServiceProvider, LoggerMessage

### Community 84 - ".Configure"
Cohesion: 0.15
Nodes (12): AuditableEntityConfiguration, EntityTypeBuilder, EntityTypeBuilder, SessionConfig, EntityTypeBuilder, NpgsqlTsVector, ProductTemplateId, StoreId (+4 more)

### Community 85 - "ResultTelemetryExtensions"
Cohesion: 0.32
Nodes (4): Activity, ResultTelemetryExtensions, ActivitySource, Task

### Community 86 - "Store"
Cohesion: 0.10
Nodes (17): ISearchLocalized, Language, StoreId, V1StoreAddressUpdatedDomainEvent, StoreId, V1StoreDescriptionUpdatedDomainEvent, StoreId, V1StoreNameUpdatedDomainEvent (+9 more)

### Community 87 - "SessionTokenReuseDetectedSignalRHandler"
Cohesion: 0.21
Nodes (10): SessionTokenReuseDetectedIntegrationEvent, Guid, CancellationToken, Guid, IFusionCache, ILogger, IOptions, LoggerMessage (+2 more)

### Community 89 - "Setup"
Cohesion: 0.33
Nodes (4): ConfigurationManager, Host.Configurations, Setup, WebApplicationBuilder

### Community 90 - ".SendAsync"
Cohesion: 0.20
Nodes (7): IClientFactory, CancellationToken, Task, MassTransitInterModuleRequestClient, CancellationToken, IOptions, Task

### Community 91 - "ICaptchaService"
Cohesion: 0.36
Nodes (5): ICaptchaService, DummyCaptchaService, IConfiguration, IServiceCollection, Setup

### Community 92 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.06
Nodes (15): Common.Domain.StronglyTypedIds, Products.Endpoints.Stores.v1.Create, IAM.Domain.Identity.DomainEvents.v1, Common.Application.AuditLog, IAM.Endpoints.Users.VersionNeutral.Get, IAM.Domain.Identity.Sessions, Common.Application.JsonConverters, Common.Domain.Entities (+7 more)

### Community 93 - ".GetClientKey"
Cohesion: 0.18
Nodes (8): IAM.Endpoints.Captcha.VersionNeutral, IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, RouteGroupBuilder, Endpoint, Response, ClientKey, RouteGroupBuilder, Setup

### Community 94 - "Notifications.Application.Push"
Cohesion: 0.29
Nodes (5): Notifications.Application.Push, Notifications.Infrastructure.Push, Notifications.Infrastructure.Push.Firebase, PushErrors, Setup

### Community 95 - ".SearchMyProductsAsync"
Cohesion: 0.10
Nodes (19): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Request, Description (+11 more)

### Community 96 - "DatabaseSeederOrchestrator"
Cohesion: 0.12
Nodes (17): BackgroundService, DatabaseSeederOrchestrator, CancellationToken, Exception, IHostApplicationLifetime, ILogger, IServiceScopeFactory, LoggerMessage (+9 more)

### Community 97 - "AuditLogRetentionService"
Cohesion: 0.11
Nodes (18): Common.Infrastructure.Persistence.AuditLog, IHostedService, AuditLogRetentionJobRegistrar, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task (+10 more)

### Community 98 - ".SearchStoreProductsAsync"
Cohesion: 0.17
Nodes (11): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Description (+3 more)

### Community 99 - "CustomPermissions"
Cohesion: 0.14
Nodes (9): FrozenDictionary, IReadOnlySet, CustomPermission, Name, CustomPermissions, HashSet, IEnumerable, RouteHandlerBuilderExtensions (+1 more)

### Community 100 - "OutboxDbContext"
Cohesion: 0.14
Nodes (13): DbContext, IOutboxDbContext, OutboxMessages, CancellationToken, DbSet, Task, IConfiguration, IServiceCollection (+5 more)

### Community 101 - "ResiliencyOptions"
Cohesion: 0.11
Nodes (17): HttpStandardResilienceOptions, IHttpClientBuilder, ResiliencyOptions, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds (+9 more)

### Community 102 - "HttpContextTargetingContextAccessor"
Cohesion: 0.18
Nodes (8): ITargetingContextAccessor, HttpContextTargetingContextAccessor, IHttpContextAccessor, ValueTask, Setup, IConfiguration, IServiceCollection, TargetingContext

### Community 103 - "BackgroundJobsTelemetry"
Cohesion: 0.10
Nodes (14): ConcurrentDictionary, Common.Application.BackgroundJobs, BackgroundJobs.Telemetry, BackgroundJobs, IServerFilter, PerformedContext, PerformingContext, JobMetricsFilter (+6 more)

### Community 104 - "IntegrationEvent"
Cohesion: 0.12
Nodes (15): Lock, IntegrationEventOutbox, IReadOnlyList, List, IntegrationEventConverter, JsonSerializerOptions, IntegrationEvent, CreatedOn (+7 more)

### Community 105 - "IIAMDbContext"
Cohesion: 0.12
Nodes (17): DbSet, IdentityRole, IdentityRoleClaim, IdentityUserClaim, IdentityUserLogin, IdentityUserRole, IdentityUserToken, IIAMDbContext (+9 more)

### Community 106 - "OutboxModule.cs"
Cohesion: 0.26
Nodes (5): Outbox, Common.Infrastructure.Persistence.Outbox, Outbox.Persistence, Common.Application.Persistence.Outbox, Outbox.Telemetry

### Community 107 - "V1AllSessionsRevokedDomainEvent"
Cohesion: 0.13
Nodes (13): CancellationToken, IFusionCache, Task, V1AllSessionsRevokedCacheInvalidationHandler, ReasonSnapshot, ReasonSnapshot, Expired, SignedOutEverywhere (+5 more)

### Community 108 - "RefreshToken"
Cohesion: 0.10
Nodes (15): StronglyTypedIdHelper, V1SessionRefreshedDomainEvent, DateTimeOffset, DefaultIdType, RefreshToken, ConsumedAt, ExpiresAt, ReplacedByTokenId (+7 more)

### Community 109 - ".IsRegisteredAsync"
Cohesion: 0.29
Nodes (6): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, IsRegistered

### Community 110 - "ProductsDbContext"
Cohesion: 0.13
Nodes (14): DbContextOptions, DbSet, ILogger, ProductTemplate, Store, TimeProvider, ProductsDbContext, Products (+6 more)

### Community 111 - ".Schedule"
Cohesion: 0.17
Nodes (12): Action, DateTimeOffset, Expression, Func, Task, TimeSpan, Action, DateTimeOffset (+4 more)

### Community 112 - "OutboxCleanupJob"
Cohesion: 0.27
Nodes (8): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxCleanupJob

### Community 113 - "CaptchaOptions"
Cohesion: 0.16
Nodes (13): CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey, Provider, ScoreThreshold, SecretKey (+5 more)

### Community 114 - "Response"
Cohesion: 0.15
Nodes (12): CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, IReadOnlyCollection, Response, BirthDate (+4 more)

### Community 115 - ".AddServices"
Cohesion: 0.27
Nodes (5): IBackgroundJobClientV2, IBackgroundJobs, IConfiguration, IServiceCollection, BackgroundJobsService

### Community 116 - ".TryWriteAsync"
Cohesion: 0.40
Nodes (3): ProblemDetailsServiceExtensions, IProblemDetailsService, Task

### Community 117 - "ApiKeyAuthenticationHandler"
Cohesion: 0.20
Nodes (9): AuthenticateResult, AuthenticationHandler, IOptionsMonitor, AuthenticationSchemeOptions, ILogger, ILoggerFactory, LoggerMessage, ApiKeyAuthenticationHandler (+1 more)

### Community 118 - "IModule"
Cohesion: 0.12
Nodes (14): ICoreModule, IModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority, Action (+6 more)

### Community 119 - "ProductsModule"
Cohesion: 0.15
Nodes (11): Action, IApplicationBuilder, IEndpointRouteBuilder, IEnumerable, RateLimiterOptions, ProductsModule, ActivitySourceNames, MeterNames (+3 more)

### Community 120 - "IAM.Infrastructure.Auth.ApiKey"
Cohesion: 0.17
Nodes (6): IAM.Infrastructure.Auth.ApiKey, ApiKeyDefaults, ApiKeyHasher, AuthenticationBuilder, AuthenticationSchemeOptions, Setup

### Community 121 - ".GetVariantAsync"
Cohesion: 0.33
Nodes (4): IVariantFeatureManager, IVariantFeatureManagerExtensions, CancellationToken, Task

### Community 122 - "TokenRefreshRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenRefreshRateLimitingPolicy (+1 more)

### Community 123 - "FullTextSearchOptions"
Cohesion: 0.25
Nodes (8): FullTextSearchOptions, CultureToConfig, DefaultConfig, RankWeights, UseUnaccent, FullTextSearchOptionsValidator, Dictionary, IReadOnlyList

### Community 124 - "HealthCheckOptions"
Cohesion: 0.11
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
Cohesion: 0.12
Nodes (14): AuditLogEntry, AggregateId, AggregateType, Event, EventType, Version, DefaultIdType, AuditLogEntryConfiguration (+6 more)

### Community 129 - "Endpoint"
Cohesion: 0.29
Nodes (5): Products.Endpoints.Products, RouteGroupBuilder, Setup, RouteGroupBuilder, Endpoint

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.48
Nodes (7): Observability (OpenTelemetry), docker-compose.yml (Base Stack), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), Postgres Service (mm.postgres), RabbitMQ Service (mm.rabbitmq), Redis Service (mm.redis)

### Community 131 - ".AddInfrastructure"
Cohesion: 0.15
Nodes (12): HostOptions, IMiddleware, JsonOptions, Assembly, IConfiguration, IResxLocalizer, IServiceCollection, IWebHostEnvironment (+4 more)

### Community 132 - ".ActivateProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 134 - ".RemoveProductAsync"
Cohesion: 0.17
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, ProductId, StoreId, Request, Id (+2 more)

### Community 135 - "Response"
Cohesion: 0.12
Nodes (15): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, CancellationToken, IReadOnlyCollection, RouteGroupBuilder, Task, Endpoint, DateTimeOffset, Guid (+7 more)

### Community 136 - "PersistenceQueryableExtensions"
Cohesion: 0.33
Nodes (6): PersistenceQueryableExtensions, CancellationToken, Expression, Func, IQueryable, Task

### Community 137 - ".WriteTooManyRequestsToResponse"
Cohesion: 0.13
Nodes (14): PartitionedRateLimiter, CancellationToken, Func, HttpContext, IConfiguration, IProblemDetailsService, IReadOnlyList, IResxLocalizer (+6 more)

### Community 138 - ".InvokeAsync"
Cohesion: 0.14
Nodes (11): IFeatureManagerSnapshot, RequireFeatureFilter, ActivitySource, Counter, EndpointFilterDelegate, EndpointFilterInvocationContext, IResxLocalizer, Meter (+3 more)

### Community 139 - "ProductTemplate"
Cohesion: 0.15
Nodes (11): IReadOnlyList, List, ProductTemplate, Brand, Color, IsActive, Model, Products (+3 more)

### Community 140 - "Response"
Cohesion: 0.13
Nodes (11): IAM.Endpoints.Users.VersionNeutral.SelfRegister, IAM.Endpoints.Users.VersionNeutral, RouteGroupBuilder, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken (+3 more)

### Community 141 - "Setup"
Cohesion: 0.20
Nodes (4): Common.Infrastructure.Modules, Host.Middlewares, Host.Infrastructure, Setup

### Community 142 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 143 - "IDbContext"
Cohesion: 0.22
Nodes (8): DatabaseFacade, EntityEntry, IDisposable, IDbContext, AuditLog, ChangeTracker, Database, DbSet

### Community 144 - "AuditableEntityResponse"
Cohesion: 0.10
Nodes (19): AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, DateTimeOffset, CancellationToken (+11 more)

### Community 145 - "Products.Endpoints.Probe"
Cohesion: 0.40
Nodes (3): Products.Endpoints.Probe, RouteGroupBuilder, Setup

### Community 146 - "IAM.Endpoints.Otp.VersionNeutral"
Cohesion: 0.40
Nodes (3): IAM.Endpoints.Otp.VersionNeutral, RouteGroupBuilder, Setup

### Community 147 - "RecurringBackgroundJobsService"
Cohesion: 0.13
Nodes (13): RecurringJobOptions, IRecurringBackgroundJobs, Action, Expression, Func, Task, RecurringBackgroundJobsService, Action (+5 more)

### Community 148 - ".AddModules"
Cohesion: 0.19
Nodes (9): LoadAll, Names, Type, Assembly, IConfiguration, IEnumerable, IReadOnlyList, IServiceCollection (+1 more)

### Community 149 - ".HandleAsync"
Cohesion: 0.18
Nodes (9): Products.Endpoints.Probe.v1, CancellationToken, IResult, RouteGroupBuilder, Task, Endpoint, Request, Count (+1 more)

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

### Community 154 - "Request"
Cohesion: 0.17
Nodes (12): Products.Endpoints.Products.v1.Update, RequestBody, Request, Body, Id, RequestBody, Description, Name (+4 more)

### Community 155 - "InterModuleRequestHandler"
Cohesion: 0.19
Nodes (8): IConsumer, IInterModuleRequestHandler, CancellationToken, Task, InterModuleRequestHandler, CancellationToken, ConsumeContext, Task

### Community 156 - "Products.Domain.Products"
Cohesion: 0.07
Nodes (21): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Stores.v1.My.AddProduct, Products.Endpoints.Products.v1.My.Get, Products.Endpoints.Products.v1.Search, Products.Domain.Products, Products.Endpoints.Products.v1.My.Search, Common.Application.ModelBinders, Products.Endpoints.Products.v1.Get (+13 more)

### Community 157 - ".UseModules"
Cohesion: 0.26
Nodes (6): ModuleRegistry, Exception, IApplicationBuilder, ILogger, LoggerMessage, WebApplication

### Community 158 - ".AddObservability"
Cohesion: 0.24
Nodes (7): OpenTelemetryBuilder, ResourceBuilder, Action, IConfiguration, IHostEnvironment, IReadOnlyList, IServiceCollection

### Community 160 - "OtpServiceBase"
Cohesion: 0.21
Nodes (8): SemaphoreSlim, OtpCacheEntry, DateTimeOffset, CancellationToken, IFusionCache, Task, TimeSpan, OtpServiceBase

### Community 161 - ".UpdateMyStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 162 - "UtcDateTimeOffsetConverter"
Cohesion: 0.17
Nodes (8): DateTimeOffset, ModelConfigurationBuilder, UtcDateTimeOffsetConverter, DateTimeOffset, DateTimeOffset, ModelConfigurationBuilder, DateTimeOffset, ModelConfigurationBuilder

### Community 163 - "For"
Cohesion: 0.33
Nodes (3): CacheKeys, For, Guid

### Community 164 - "Request"
Cohesion: 0.18
Nodes (11): Guid, Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FullName (+3 more)

### Community 165 - "ICurrentUser"
Cohesion: 0.09
Nodes (20): ICurrentUser, Id, IdAsString, Roles, SessionId, Guid, ICollection, CurrentUser (+12 more)

### Community 166 - "Endpoint"
Cohesion: 0.29
Nodes (5): IAM.Endpoints.Tokens.VersionNeutral, RouteGroupBuilder, Endpoint, RouteGroupBuilder, Setup

### Community 167 - "PaginationRequest"
Cohesion: 0.11
Nodes (16): PaginationRequest, PageNumber, PageSize, Skip, Take, DbContextExtensions, CancellationToken, DbSet (+8 more)

### Community 168 - "CachedCaptchaService"
Cohesion: 0.25
Nodes (5): CancellationToken, IFusionCache, IOptions, Task, CachedCaptchaService

### Community 169 - "Policies"
Cohesion: 0.17
Nodes (8): CreateStoreRateLimitingPolicy, Products.Infrastructure.RateLimiting, RateLimiterOptions, Policies, Action, IEnumerable, RateLimiterOptions, RateLimitingConstants

### Community 171 - ".UpdateStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 172 - "ResxLocalizationOptions"
Cohesion: 0.25
Nodes (7): ResxLocalizationOptions, DefaultCulture, SupportedCultures, ResxLocalizationOptionsValidator, ICollection, IApplicationBuilder, IOptions

### Community 173 - "Request"
Cohesion: 0.17
Nodes (12): ProductTemplateId, RequestBody, Request, Body, Id, RequestBody, Description, Name (+4 more)

### Community 174 - "TokensDto"
Cohesion: 0.15
Nodes (11): IAM.Application.Tokens.DTOs, DateTimeOffset, AccessTokenDto, AccessToken, AccessTokenExpiresAt, DateTimeOffset, TokensDto, AccessToken (+3 more)

### Community 175 - "PaginationResponse"
Cohesion: 0.08
Nodes (25): Products.Endpoints.Stores.v1.My.AuditLog, JsonElement, AuditLogDto, DateTimeOffset, PaginationResponse, HasNext, HasPrevious, NextPageNumber (+17 more)

### Community 176 - "ApiKeyEntry"
Cohesion: 0.25
Nodes (10): AbstractValidator, ApiKeyEntry, KeyHash, Name, Permissions, ApiKeyEntryValidator, ApiKeysOptions, Keys (+2 more)

### Community 177 - ".AddAuthInfrastructure"
Cohesion: 0.05
Nodes (33): AuthorizationHandler, AuthorizationHandlerContext, AuthorizationPolicy, IAM.Infrastructure.Auth.Jwt, IAM.Infrastructure.Auth.Services, IAM.Infrastructure.Auth, IAM.Application.Auth.Services, IAM.Application.Auth (+25 more)

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

### Community 184 - "SignalROptions"
Cohesion: 0.50
Nodes (4): SignalROptions, RedisConnectionString, UseRedisBackplane, SignalROptionsValidator

### Community 185 - "IamModule"
Cohesion: 0.12
Nodes (13): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, IamModule (+5 more)

### Community 186 - "Request"
Cohesion: 0.20
Nodes (10): Products.Endpoints.Stores.v1.Update, RequestBody, Request, Body, Id, RequestBody, Address, Description (+2 more)

### Community 187 - "ReverseProxyOptions"
Cohesion: 0.18
Nodes (9): ForwardedHeadersOptions, ReverseProxyOptions, ForwardLimit, IsEnabled, TrustedNetworks, ReverseProxyOptionsValidator, IReadOnlyList, IConfiguration (+1 more)

### Community 188 - "BoundedCaptureStream"
Cohesion: 0.18
Nodes (7): HttpResponse, BoundedCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 190 - "V1UserRegisteredDomainEvent"
Cohesion: 0.18
Nodes (7): CancellationToken, Task, CancellationToken, Task, DateOnly, Uri, V1UserRegisteredDomainEvent

### Community 191 - "Split-Deployment PoC"
Cohesion: 0.22
Nodes (8): Concurrent safety, Cross-process call path, Files added by this PoC, How it works, How to run, Split-Deployment PoC, What this proves, README (Boilerplate Overview)

### Community 192 - "UserRegisteredSignalRHandler"
Cohesion: 0.24
Nodes (8): UserRegisteredIntegrationEvent, CancellationToken, IFusionCache, ILogger, IOptions, LoggerMessage, Task, UserRegisteredSignalRHandler

### Community 193 - "Request"
Cohesion: 0.18
Nodes (11): StoreId, Request, Description, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name (+3 more)

### Community 194 - "CorsOptions"
Cohesion: 0.25
Nodes (8): CorsOptions, AllowCredentials, AllowedHeaders, AllowedMethods, AllowedOrigins, MaxAgeInSeconds, CorsOptionsValidator, IReadOnlyList

### Community 195 - "NotificationsHub"
Cohesion: 0.36
Nodes (6): Hub, Exception, ILogger, LoggerMessage, Task, NotificationsHub

### Community 196 - "JwtOptions"
Cohesion: 0.11
Nodes (17): JwtOptions, AccessTokenExpirationInMinutes, AllowedClientIds, Audience, Issuer, RefreshTokenExpirationInDays, RefreshTokenReuseGraceWindowInSeconds, Secret (+9 more)

### Community 197 - "IntegrationEventHandlerBase"
Cohesion: 0.24
Nodes (11): IntegrationEventHandlerBase, MaxEventAge, CancellationToken, ConsumeContext, DefaultIdType, IFusionCache, ILogger, IOptions (+3 more)

### Community 198 - "SecurityHeadersMiddleware"
Cohesion: 0.20
Nodes (7): IAuthenticationSchemeProvider, IApplicationBuilder, HttpContext, IOptions, RequestDelegate, Task, SecurityHeadersMiddleware

### Community 199 - "OpenApiOptions"
Cohesion: 0.22
Nodes (9): OpenApiOptions, ContactEmail, ContactName, Description, EnableSwagger, LicenseName, LicenseUrl, Title (+1 more)

### Community 200 - "KeyValuePair"
Cohesion: 0.21
Nodes (6): KeyValuePair, ActivitySource, Counter, Meter, NotificationsTelemetry, UpDownCounter

### Community 201 - "GetPushTokensRequest"
Cohesion: 0.36
Nodes (8): GetPushTokensRequest, GetPushTokensResponse, PushTarget, IReadOnlyList, CancellationToken, Task, TimeProvider, GetPushTokensRequestHandler

### Community 202 - "Request"
Cohesion: 0.22
Nodes (9): Guid, Request, ClientId, DeviceId, DeviceName, Otp, PhoneNumber, PushToken (+1 more)

### Community 203 - "IProductsDbContext"
Cohesion: 0.12
Nodes (13): DbSet, IProductsDbContext, Products, ProductTemplates, Stores, CancellationToken, RouteGroupBuilder, Task (+5 more)

### Community 210 - "ConfigureSwaggerOptions"
Cohesion: 0.28
Nodes (7): ApiVersionDescription, IApiVersionDescriptionProvider, IConfigureOptions, OpenApiInfo, IOptions, SwaggerGenOptions, ConfigureSwaggerOptions

### Community 211 - ".CreateProductTemplateAsync"
Cohesion: 0.22
Nodes (6): Products.Endpoints.ProductTemplates.v1.Create, ProductTemplate, CancellationToken, Task, Response, Id

### Community 212 - ".SeedUser"
Cohesion: 0.44
Nodes (3): Action, DateOnly, Task

### Community 213 - "ProductsTelemetry"
Cohesion: 0.40
Nodes (4): ActivitySource, Counter, Meter, ProductsTelemetry

### Community 214 - ".AddOtpServices"
Cohesion: 0.22
Nodes (5): IFusionCache, DummyOtpService, IConfiguration, IServiceCollection, Setup

### Community 215 - "RedisOtpService"
Cohesion: 0.28
Nodes (6): CancellationToken, IConnectionMultiplexer, IOptions, Task, TimeSpan, RedisOtpService

### Community 216 - "ProductTemplateId"
Cohesion: 0.25
Nodes (6): ProductTemplateId, DefaultIdType, ProductTemplateId, Request, Id, RequestValidator

### Community 217 - "Request"
Cohesion: 0.25
Nodes (7): Constants, Request, Brand, Color, Model, SearchTerm, RequestValidator

### Community 218 - ".GetStoreAsync"
Cohesion: 0.22
Nodes (7): CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, Id, RequestValidator

### Community 219 - ".SaveChangesAsync"
Cohesion: 0.12
Nodes (12): CancellationToken, Task, CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint, CancellationToken (+4 more)

### Community 220 - ".SeedProductAsync"
Cohesion: 0.22
Nodes (7): CancellationToken, Task, CancellationToken, List, ProductTemplateId, StoreId, Task

### Community 221 - "Response"
Cohesion: 0.25
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.Create, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 222 - "Endpoint"
Cohesion: 0.29
Nodes (5): Products.Endpoints.ProductTemplates, RouteGroupBuilder, Setup, RouteGroupBuilder, Endpoint

### Community 223 - "ReCaptchaResponse"
Cohesion: 0.25
Nodes (7): DateTime, ReCaptchaResponse, ChallengeTs, ErrorCodes, Hostname, Score, Success

### Community 224 - ".SingleAsResult"
Cohesion: 0.36
Nodes (4): CollectionExtensions, Func, ICollection, IEnumerable

### Community 225 - "BackgroundJobsOptions"
Cohesion: 0.29
Nodes (7): BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount, BackgroundJobsOptionsValidator

### Community 226 - "GetSeedUserIdsRequest"
Cohesion: 0.39
Nodes (6): GetSeedUserIdsRequest, GetSeedUserIdsResponse, ICollection, CancellationToken, Task, GetSeedUserIdsRequestHandler

### Community 227 - "BackgroundJobsModule"
Cohesion: 0.25
Nodes (7): BackgroundJobsModule, ActivitySourceNames, MeterNames, Name, StartupPriority, IEndpointRouteBuilder, IEnumerable

### Community 228 - "CustomValidator"
Cohesion: 0.16
Nodes (12): CustomRateLimitingOptionsValidator, FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit, FixedWindowValidator, DatabaseOptions (+4 more)

### Community 229 - ".AddCommonOptions"
Cohesion: 0.20
Nodes (6): Setup, IConfiguration, IHostEnvironment, IServiceCollection, ValidationContextExtensions, ValidationContext

### Community 230 - ".HandleChallengeAsync"
Cohesion: 0.48
Nodes (5): AuthenticationProperties, ProblemDetailsContext, IProblemDetailsService, IResxLocalizer, Task

### Community 231 - "Request"
Cohesion: 0.33
Nodes (6): Products.Endpoints.Stores.v1.My.Update, Request, Address, Description, Name, RequestValidator

### Community 232 - ".AddCommonCaching"
Cohesion: 0.29
Nodes (5): Common.Infrastructure.Caching, Setup, IConfiguration, IConnectionMultiplexer, IServiceCollection

### Community 233 - ".AddNotificationsSignalR"
Cohesion: 0.29
Nodes (6): RedisOptions, IConfiguration, IConfigureOptions, IConnectionMultiplexer, IServiceCollection, Setup

### Community 234 - "Setup"
Cohesion: 0.29
Nodes (4): IApplicationBuilder, IWebHostEnvironment, Type, Setup

### Community 235 - "V1SessionRevokedDomainEvent"
Cohesion: 0.29
Nodes (7): ReasonSnapshot, ReasonSnapshot, Expired, SignedOutEverywhere, TokenReuseDetected, UserSignedOut, V1SessionRevokedDomainEvent

### Community 236 - "SmsMessage"
Cohesion: 0.33
Nodes (5): SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, SmsMessage

### Community 237 - "ProductsDatabaseSeeder"
Cohesion: 0.29
Nodes (5): CancellationToken, IServiceScopeFactory, Task, ProductsDatabaseSeeder, Priority

### Community 238 - "SendForLogin/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForLogin, Request, CaptchaToken, PhoneNumber, RequestValidator

### Community 239 - "Host.Swagger"
Cohesion: 0.33
Nodes (3): Host, Host.Swagger, Program

### Community 240 - "CheckRegistration/Request.cs"
Cohesion: 0.40
Nodes (4): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, Request, PhoneNumber, RequestValidator

### Community 241 - "SendForRegistration/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, Request, CaptchaToken, PhoneNumber, RequestValidator

### Community 242 - "StronglyTypedIdSchemaFilter"
Cohesion: 0.33
Nodes (4): IOpenApiSchema, ISchemaFilter, SchemaFilterContext, StronglyTypedIdSchemaFilter

### Community 245 - "RemoveDefaultResponseSchemaFilter"
Cohesion: 0.33
Nodes (4): IOperationFilter, OpenApiOperation, OperationFilterContext, RemoveDefaultResponseSchemaFilter

### Community 246 - "StoreId"
Cohesion: 0.40
Nodes (3): StoreId, DefaultIdType, StoreId

### Community 247 - ".GetProductAsync"
Cohesion: 0.33
Nodes (5): CancellationToken, Task, Request, Id, RequestValidator

### Community 248 - ".UpdateMyProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 249 - "Request"
Cohesion: 0.40
Nodes (5): Request, Brand, Color, Model, RequestValidator

### Community 250 - "Request"
Cohesion: 0.33
Nodes (6): Request, Address, Description, Name, OwnerId, RequestValidator

### Community 251 - "Response"
Cohesion: 0.33
Nodes (6): Response, Address, Description, Name, OwnerId, ProductCount

### Community 252 - ".RemoveMyProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 261 - "Deactivate/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.ProductTemplates.v1.Deactivate, Request, Id, RequestValidator

### Community 262 - "Refresh/Request.cs"
Cohesion: 0.50
Nodes (4): IAM.Endpoints.Tokens.VersionNeutral.Refresh, Request, RefreshToken, RequestValidator

### Community 263 - "UpdatePushToken/Request.cs"
Cohesion: 0.50
Nodes (4): IAM.Endpoints.Tokens.VersionNeutral.Sessions.UpdatePushToken, Request, PushToken, RequestValidator

### Community 264 - "StronglyTypedIdBinder"
Cohesion: 0.40
Nodes (4): IModelBinder, ModelBindingContext, StronglyTypedIdBinder, Task

### Community 265 - "AuditLogOptions"
Cohesion: 0.50
Nodes (4): AuditLogOptions, PurgeBatchSize, RetentionDays, AuditLogOptionsValidator

### Community 266 - "ModulesOptions"
Cohesion: 0.50
Nodes (4): ModulesOptions, EnabledModules, ModulesOptionsValidator, IReadOnlyList

### Community 267 - "SecurityHeadersOptions"
Cohesion: 0.50
Nodes (4): SecurityHeadersOptions, Headers, SecurityHeadersOptionsValidator, Dictionary

### Community 268 - ".CreateAsync"
Cohesion: 0.50
Nodes (3): Success, Func, Task

### Community 270 - "DefaultResponsesOperationFilter"
Cohesion: 0.70
Nodes (3): OpenApiOperation, OperationFilterContext, DefaultResponsesOperationFilter

### Community 271 - "Response"
Cohesion: 0.40
Nodes (5): Response, Description, Name, Price, Quantity

### Community 272 - ".SeedProductTemplatesAsync"
Cohesion: 0.60
Nodes (3): CancellationToken, List, Task

### Community 273 - ".SeedStoresAsync"
Cohesion: 0.60
Nodes (3): CancellationToken, List, Task

### Community 275 - ".AddCustomSwagger"
Cohesion: 0.50
Nodes (3): IConfigureOptions, IServiceCollection, SwaggerGenOptions

### Community 276 - ".PhoneNumberValidation"
Cohesion: 0.50
Nodes (3): IResxLocalizer, IRuleBuilder, IRuleBuilderOptions

### Community 277 - "SendResponseBody"
Cohesion: 0.50
Nodes (4): SendResponseBody, Code, Description, JobId

## Knowledge Gaps
- **734 isolated node(s):** `CustomActions`, `CustomResources`, `Name`, `Id`, `IdAsString` (+729 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1705 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **102 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `Common.Domain.ResultMonad`, `Common.Application.Caching`, `FirebasePushGateway`, `AuditLogOptions`, `ModulesOptions`, `SecurityHeadersOptions`, `Common.Application.Auth`, `Setup`, `Common.Infrastructure.Persistence`, `OutboxOptions`, `ISearchLanguageResolver`, `Request`, `ObservabilityOptions`, `Policies`, `ResxLocalizationOptions`, `ProductsModule.cs`, `ApiKeyEntry`, `OtpOptions`, `CachingOptions`, `SmsOptions`, `RabbitMqOptions`, `Notifications.Infrastructure.Telemetry`, `SignalROptions`, `ReverseProxyOptions`, `RequestLoggingOptions`, `CorsOptions`, `SecurityHeadersMiddleware`, `OpenApiOptions`, `Request`, `Common.Application.Validation`, `Common.Application.EventBus`, `.AddOtpServices`, `.SendAsync`, `Common.Domain.StronglyTypedIds`, `Notifications.Application.Push`, `BackgroundJobsOptions`, `AuditLogRetentionService`, `CustomValidator`, `ResiliencyOptions`, `.AddCommonOptions`, `BackgroundJobsTelemetry`, `.AddCommonCaching`, `OutboxModule.cs`, `Host.Swagger`, `CaptchaOptions`, `IModule`, `FullTextSearchOptions`, `HealthCheckOptions`?**
  _High betweenness centrality (0.293) - this node is a cross-community bridge._
- **Why does `ApplicationUserId` connect `ApplicationUserId` to `NotificationPayload`, `IAMDbContext`, `Session`, `Response`, `AuditableEntityResponse`, `.SeedStoresAsync`, `DomainEvent`, `AuditableEntity`, `.CreateStoreAsync`, `.AddIdentityInfrastructure`, `ICurrentUser`, `.RefreshToken`, `Seeder`, `ApplicationUser`, `.CreateTokens`, `PaginationResponse`, `.AddAuthInfrastructure`, `.SearchUsersAsync`, `V1UserRegisteredDomainEvent`, `UserRegisteredSignalRHandler`, `Request`, `V1StoreCreatedDomainEvent`, `GetPushTokensRequest`, `.SearchStoresAsync`, `.Configure`, `Store`, `SessionTokenReuseDetectedSignalRHandler`, `GetSeedUserIdsRequest`, `IntegrationEvent`, `IIAMDbContext`, `V1AllSessionsRevokedDomainEvent`, `RefreshToken`, `V1SessionRevokedDomainEvent`, `Response`, `Request`, `Response`?**
  _High betweenness centrality (0.117) - this node is a cross-community bridge._
- **Why does `Result` connect `Result` to `.ActivateProductTemplateAsync`, `.RemoveProductAsync`, `Response`, `PersistenceQueryableExtensions`, `FirebasePushGateway`, `Error`, `ProductTemplate`, `.CreateAsync`, `.SendOtp`, `.RegisterAndLoginAsync`, `Response`, `AuditableEntityResponse`, `ISmsGateway`, `.ToResult`, `.RevokeSession`, `NetGsmSmsGateway`, `.CreateStoreAsync`, `.AddProductAsync`, `.UpdateMyStoreAsync`, `.RefreshToken`, `PaginationRequest`, `CachedCaptchaService`, `.UpdateStoreAsync`, `.CreateTokens`, `PaginationResponse`, `.SearchUsersAsync`, `ReCaptchaService`, `DummySmsGateway`, `.SearchProductTemplatesAsync`, `.AddProductToMyStoreAsync`, `Response`, `Response`, `IProductsDbContext`, `.CreateMyStoreAsync`, `.SearchStoresAsync`, `StringExtensions`, `.CreateProductTemplateAsync`, `ResultTelemetryExtensions`, `.GetStoreAsync`, `.SaveChangesAsync`, `.GetClientKey`, `.SearchMyProductsAsync`, `.SingleAsResult`, `.SearchStoreProductsAsync`, `.IsRegisteredAsync`, `Response`, `.GetProductAsync`, `.UpdateMyProductAsync`, `.RemoveMyProductAsync`, `.TapWhenFeatureEnabledAsync`?**
  _High betweenness centrality (0.097) - this node is a cross-community bridge._
- **What connects `CustomActions`, `CustomResources`, `Name` to the rest of the system?**
  _734 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Common.Domain.ResultMonad` be split into smaller, more focused modules?**
  _Cohesion score 0.08013468013468013 - nodes in this community are weakly interconnected._
- **Should `IAMDbContext` be split into smaller, more focused modules?**
  _Cohesion score 0.1111111111111111 - nodes in this community are weakly interconnected._
- **Should `Session` be split into smaller, more focused modules?**
  _Cohesion score 0.07823613086770982 - nodes in this community are weakly interconnected._