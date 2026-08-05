# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-08-05)

## Corpus Check
- 463 files · ~67,060 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2959 nodes · 5239 edges · 300 communities (201 shown, 99 thin omitted)
- Extraction: 99% EXTRACTED · 1% INFERRED · 0% AMBIGUOUS · INFERRED: 63 edges (avg confidence: 0.8)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `0834c67a`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- IAM.Domain.Identity.Sessions
- ApplicationUserId
- OutboxProcessor
- IAMDbContext
- Hybrid DDD (Writes) / VSA (Reads)
- Session
- RedisFixedWindowRateLimiter
- Common.Infrastructure.Persistence
- FirebasePushGateway
- Products/v1/My/Update/Request.cs
- Error
- Common.Domain.ResultMonad
- .RegisterAndLoginAsync
- .SendOtp
- LocalizedIdentityErrorDescriber
- Common.Infrastructure.Persistence.ValueConverters
- .SendAsync
- Common.Application.BackgroundJobs
- DomainEvent
- OutboxModule
- SelfRegister/Request.cs
- BoundedCaptureStream
- IDatabaseSeeder
- AggregateRoot
- .RefreshToken
- NetGsmSmsGateway
- IEvent
- .GetMyProductAsync
- ProblemDetailsExtensions
- Result
- IAM.Infrastructure/Auth/Setup.cs
- Setup
- Endpoint
- v1/AddProduct/Request.cs
- .HandleAsync
- OutboxMessage
- CustomRateLimitingOptions
- Seeder
- RequestResponseBodyLoggingMiddleware
- .SearchUsersAsync
- CheckRegistrationRateLimitingPolicy
- AuditableEntityResponse
- .SaveWithOutboxAsync
- CreateStoreRateLimitingPolicy
- Product
- ProductTemplateId
- Setup
- Outbox Misuse Check
- OtpServiceBase
- Add Integration Event Command
- IntegrationEventHandlerBase
- .SendOtp
- ReCaptchaService
- StoreId
- .AddResilientHttpClient
- .FixedWindow
- Notifications.Application.Sms
- Cross-Module Reference Violation
- IStronglyTypedId
- NotificationPayload
- Bogus Test Data
- .AddProductToMyStoreAsync
- RequireFeatureFilter
- NotificationsHub
- InterModuleRequestHandler
- .SavingChangesAsync
- OutboxTelemetry
- .GetProductTemplateAsync
- ValueObject
- DomainEventHandlerBase
- .SearchProductTemplatesAsync
- Full-Text Search
- .CreateMyStoreAsync
- V1SessionRevokedDomainEvent
- AsNoTracking Coverage Check
- ProductId
- CorsOptions.cs
- ICurrentUser
- TokenCreateRateLimitingPolicy
- Common.IntegrationEvents
- RequestLoggingOptions
- .GenerateAccessToken
- StringExtensions
- .EnsureNoMigrationsPending
- AuditableEntityConfiguration
- ResultTelemetryExtensions
- AuditLogRetentionJobRegistrar
- TokenRefreshRateLimitingPolicy
- Configuration-Driven Module Registration
- Setup
- VersionNeutral/Create/Request.cs
- CaptchaOptions.cs
- Common.Domain.StronglyTypedIds
- .SearchStoresAsync
- Common.Infrastructure.Extensions
- RefreshToken
- FirebasePushGateway.cs
- .MapCode
- .SearchStoreProductsAsync
- .AuthorizeAsync
- .SearchMyProductsAsync
- ApplicationUser
- Common.Infrastructure.FeatureManagement
- Common.Application.FeatureManagement
- .GetStoreAuditLogAsync
- OutboxMetricsJob.cs
- .InvokeAsync
- SeedingCompletionTracker
- .TryDeserialize
- .IsRegisteredAsync
- Common.Application.JsonConverters
- IBackgroundJobs
- UserRegisteredSignalRHandler
- Endpoint
- OutboxCleanupJob.cs
- BackgroundJobsService
- IModule
- ApiKeyAuthenticationHandler
- ObservabilityOptions
- ProductsModule
- IAM.Infrastructure.Auth.ApiKey
- IntegrationEventOutbox
- SmsRateLimitingPolicy
- NotificationsModule
- .LogHealthChecksRegistered
- .TapWhenFeatureEnabledAsync
- GlobalExceptionHandlingMiddleware
- AuditLogEntry
- ValueConverter
- .GetProductAsync
- Host Service (mm.host)
- Common.Infrastructure.Persistence.Outbox
- Stores/v1/Search/Request.cs
- Consumer Idempotency (IntegrationEventHandlerBase)
- IProductsDbContext
- .ListSessions
- .SingleAsResult
- .GlobalRateLimiter
- IAM.Endpoints.Otp.VersionNeutral
- My/Create/Request.cs
- .MapEndpoint
- CurrentUser.cs
- .ActivateProductTemplateAsync
- IIAMDbContext
- .GetAsync
- PermissionPolicyProvider.cs
- .PaginateAsync
- MassTransitInterModuleRequestClient.cs
- .AddInfrastructure
- .GetRoleIdByName
- .WriteAsync
- FullTextSearchOptions
- .UpdateStoreAsync
- Host.Swagger
- .AddOrUpdate
- VerifyPhoneOtpRequest
- Common.Application.ModelBinders
- SecurityHeadersMiddleware.cs
- .Capture
- .AddOrUpdate
- ValidationContextExtensions
- .GetMyStoreAuditLogAsync
- PermissionAuthorizationHandler
- V1UserRegisteredDomainEvent
- Common.Application.Options
- CurrentUser
- Endpoint
- PaginationRequest
- Response
- RateLimitingConstants.cs
- Notifications.Application/IAssemblyReference.cs
- Common.InterModuleRequests
- BackgroundJobsTelemetry
- IOutboxMessage
- IAM.Application.Tokens.DTOs
- PaginationResponse
- ApiKeysOptions.cs
- PermissionPolicyProvider
- Products.Endpoints
- CachingOptions.cs
- SmsOptions.cs
- Notifications.Infrastructure.Telemetry
- Constants
- SendRequestBody
- Setup
- IamModule
- .RevokeSession
- Caching/Setup.cs
- IAM.Infrastructure.Auth
- .AddCustomMassTransit
- Split-Deployment PoC
- .UpdateMyProductAsync
- .UpdateMyStoreAsync
- Setup
- Common.InterModuleRequests.Contracts
- IntegrationEvent
- NotificationsTelemetry
- HttpContextExtensions.cs
- .UpdateProductAsync
- Infrastructure/StringExtensions.cs
- OutboxDbContext
- .GetStoreAsync
- ProductsTelemetry
- .DeactivateProductTemplateAsync
- Products.Endpoints.Probe
- PushOptions.cs
- Auditing/Setup.cs
- .UpdateCurrentSessionPushToken
- JwtClaimNames.cs
- IAutoMigrateMarker.cs
- ModulesOptions.cs
- ResxLocalizationOptions.cs
- CustomValidator
- .AddCommonOptions
- Setup
- ActionsAndResources.cs
- CustomRoles
- .AddJwtBearerScheme
- Products.Domain/IAssemblyReference.cs
- IAM.Application/IAssemblyReference.cs
- Products.Infrastructure/IAssemblyReference.cs
- Products.Application/IAssemblyReference.cs
- Common.Infrastructure/IAssemblyReference.cs
- Notifications.Domain/IAssemblyReference.cs
- IAM.Infrastructure/IAssemblyReference.cs
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
- README (Boilerplate Overview)

## God Nodes (most connected - your core abstractions)
1. `Common.Application.Options` - 99 edges
2. `Result` - 88 edges
3. `Common.Domain.ResultMonad` - 72 edges
4. `Common.Domain.StronglyTypedIds` - 68 edges
5. `CustomValidator` - 62 edges
6. `Common.Application.Auth` - 60 edges
7. `Common.Application.Validation` - 56 edges
8. `ApplicationUserId` - 50 edges
9. `Setup` - 50 edges
10. `Common.Application.Extensions` - 49 edges

## Surprising Connections (you probably didn't know these)
- `docker-compose.split.yml (Split Deployment)` --conceptually_related_to--> `Configuration-Driven Module Loading`  [EXTRACTED]
  docker-compose.split.yml → CLAUDE.md
- `SignalR PoC — Notifications Hub Client` --conceptually_related_to--> `IntegrationEvents (Async Cross-Module)`  [INFERRED]
  signalr-poc/signalr-poc.html → CLAUDE.md
- `Aspire Dashboard Service (mm.aspire-dashboard)` --conceptually_related_to--> `Observability (OpenTelemetry)`  [INFERRED]
  docker-compose.yml → CLAUDE.md
- `docker-compose.split.yml (Split Deployment)` --references--> `IAM Module`  [EXTRACTED]
  docker-compose.split.yml → CLAUDE.md
- `SignalR PoC — Notifications Hub Client` --references--> `IAM Module`  [INFERRED]
  signalr-poc/signalr-poc.html → CLAUDE.md

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Local Infrastructure Stack** — docker_compose_postgres, docker_compose_rabbitmq, docker_compose_redis, docker_compose_aspire_dashboard [EXTRACTED 1.00]

## Communities (300 total, 99 thin omitted)

### Community 0 - "IAM.Domain.Identity.Sessions"
Cohesion: 0.08
Nodes (20): IAM.Application.Tokens.Services, IAM.Application.Extensions, IAM.Endpoints.Otp, IAM.Domain.Identity, IAM.Domain.Identity.Sessions, IAM.Infrastructure.Tokens, IAM.Endpoints.Tokens.VersionNeutral.Revoke, IAM.Infrastructure.Tokens.Services (+12 more)

### Community 1 - "ApplicationUserId"
Cohesion: 0.16
Nodes (17): IEntityTypeConfiguration, ApplicationUserId, DefaultIdType, EntityTypeBuilder, IdentityRole, IdentityRoleClaim, IdentityUserClaim, IdentityUserLogin (+9 more)

### Community 2 - "OutboxProcessor"
Cohesion: 0.16
Nodes (13): BackgroundService, DatabaseSeederOrchestrator, CancellationToken, ILogger, LoggerMessage, Task, CancellationToken, Exception (+5 more)

### Community 3 - "IAMDbContext"
Cohesion: 0.15
Nodes (11): IdentityDbContext, DbSet, IdentityRole, IdentityRoleClaim, IdentityUserClaim, IdentityUserLogin, IdentityUserRole, IdentityUserToken (+3 more)

### Community 5 - "Session"
Cohesion: 0.19
Nodes (8): DateTimeOffset, Guid, DateTimeOffset, Guid, IReadOnlyCollection, List, Session, SessionRevokedReason

### Community 6 - "RedisFixedWindowRateLimiter"
Cohesion: 0.11
Nodes (17): IConnectionMultiplexer, RateLimiter, RateLimiterStatistics, RateLimitLease, FixedWindowLease, RedisFixedWindowRateLimiter, bool, CancellationToken (+9 more)

### Community 7 - "Common.Infrastructure.Persistence"
Cohesion: 0.07
Nodes (14): Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Common.Application.Persistence, IAM.Infrastructure.Persistence, IAM.Infrastructure.Persistence.Seeding, Common.Infrastructure.EventBus, Common.Infrastructure.Persistence.Auditing, Common.Infrastructure.Persistence.DbContext (+6 more)

### Community 8 - "FirebasePushGateway"
Cohesion: 0.16
Nodes (13): FirebaseApp, FirebaseMessaging, PushMessage, CancellationToken, Exception, IEnumerable, ILogger, int (+5 more)

### Community 9 - "Products/v1/My/Update/Request.cs"
Cohesion: 0.11
Nodes (18): Products.Endpoints.Stores.v1.Update, Products.Endpoints.Products.v1.My.Update, Products.Endpoints.Products.v1.Update, RequestBody, Request, RequestBody, RequestBodyValidator, RequestValidator (+10 more)

### Community 10 - "Error"
Cohesion: 0.11
Nodes (12): HttpStatusCode, IdentityResult, IStringLocalizer, StringLocalizerExtensions, Error, ICollection, IResult, IdentityResultExtensions (+4 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.16
Nodes (12): Common.Application.Search, Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Products.Domain.Products, Products.Infrastructure.Telemetry, Products.Application.Persistence, Products.Domain.Stores (+4 more)

### Community 12 - ".RegisterAndLoginAsync"
Cohesion: 0.10
Nodes (27): JwtOptions, JwtOptionsValidator, IReadOnlyCollection, IInterModuleRequestClient, CancellationToken, Task, accessToken, DateTimeOffset (+19 more)

### Community 13 - ".SendOtp"
Cohesion: 0.12
Nodes (16): Notifications.Application.Otp, IAM.Endpoints.Otp.VersionNeutral.SendForLogin, Notifications.Infrastructure.InterModuleRequestHandlers, Common.InterModuleRequests.Notifications, SendPhoneOtpRequest, SendPhoneOtpResponse, CancellationToken, IFeatureManager (+8 more)

### Community 14 - "LocalizedIdentityErrorDescriber"
Cohesion: 0.13
Nodes (4): IAM.Infrastructure.Identity, IdentityError, IdentityErrorDescriber, LocalizedIdentityErrorDescriber

### Community 15 - "Common.Infrastructure.Persistence.ValueConverters"
Cohesion: 0.23
Nodes (4): IAM.Infrastructure.Persistence.EntityConfigurations, Common.Infrastructure.Persistence.EntityConfigurations, Common.Infrastructure.Persistence.ValueConverters, Products.Infrastructure.Persistence.EntityConfigurations

### Community 16 - ".SendAsync"
Cohesion: 0.16
Nodes (9): CancellationToken, Task, ISmsGateway, SmsCategory, SmsMessage, CancellationToken, Task, TimeSpan (+1 more)

### Community 17 - "Common.Application.BackgroundJobs"
Cohesion: 0.24
Nodes (5): Common.Application.BackgroundJobs, BackgroundJobs, RecurringJobOptions, IRecurringBackgroundJobs, RecurringBackgroundJobsService

### Community 18 - "DomainEvent"
Cohesion: 0.10
Nodes (22): Products.Domain.Products.DomainEvents.v1, DomainEvent, DateTimeOffset, DefaultIdType, V1AllSessionsRevokedDomainEvent, V1RefreshTokenRevokedDomainEvent, V1RefreshTokenUpdatedDomainEvent, V1SessionCreatedDomainEvent (+14 more)

### Community 19 - "OutboxModule"
Cohesion: 0.19
Nodes (10): Action, Exception, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, ILogger, IServiceCollection (+2 more)

### Community 20 - "SelfRegister/Request.cs"
Cohesion: 0.12
Nodes (12): IAM.Endpoints.Users.VersionNeutral.SelfRegister, IAM.Endpoints.Common.Validations, IResxLocalizer, IRuleBuilder, IRuleBuilderOptions, CommonValidations, IResxLocalizer, IRuleBuilder (+4 more)

### Community 21 - "BoundedCaptureStream"
Cohesion: 0.14
Nodes (7): byte, SeekOrigin, bool, int, BoundedCaptureStream, BoundedRequestCaptureStream, Stream

### Community 22 - "IDatabaseSeeder"
Cohesion: 0.14
Nodes (9): IDatabaseSeeder, CancellationToken, Task, CancellationToken, Task, IamDatabaseSeeder, CancellationToken, Task (+1 more)

### Community 23 - "AggregateRoot"
Cohesion: 0.15
Nodes (9): AggregateRoot, IEnumerable, IReadOnlyCollection, List, IAggregateRoot, IEnumerable, IReadOnlyCollection, IAuditableEntity (+1 more)

### Community 24 - ".RefreshToken"
Cohesion: 0.05
Nodes (32): IAM.Endpoints.Tokens.VersionNeutral.Refresh, IFusionCache, CancellationToken, HttpContext, ILogger, IOptions, LoggerMessage, RouteGroupBuilder (+24 more)

### Community 25 - "NetGsmSmsGateway"
Cohesion: 0.20
Nodes (8): SendRequestBody, CancellationToken, JsonSerializerOptions, string, Task, NetGsmSmsGateway, SendMessageBody, SendResponseBody

### Community 26 - "IEvent"
Cohesion: 0.13
Nodes (10): CancellationToken, Task, IEventHandlerWrapper, CancellationToken, Task, IEvent, DateTimeOffset, DefaultIdType (+2 more)

### Community 27 - ".GetMyProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 28 - "ProblemDetailsExtensions"
Cohesion: 0.40
Nodes (3): ProblemDetails, ProblemDetailsExtensions, ICollection

### Community 29 - "Result"
Cohesion: 0.14
Nodes (11): Result, Func, Task, AsyncExtensions, SyncExtensions, Action, Func, Task (+3 more)

### Community 30 - "IAM.Infrastructure/Auth/Setup.cs"
Cohesion: 0.22
Nodes (6): IAM.Infrastructure.Auth.Jwt, IAM.Infrastructure.Auth.Services, IAM.Application.Auth.Services, IConfiguration, IServiceCollection, Setup

### Community 31 - "Setup"
Cohesion: 0.06
Nodes (25): IHostBuilder, LoadAll, Names, IApplicationBuilder, IConfiguration, IServiceCollection, Setup, IApplicationBuilder (+17 more)

### Community 32 - "Endpoint"
Cohesion: 0.29
Nodes (5): Products.Endpoints.Stores, RouteGroupBuilder, Setup, RouteGroupBuilder, Endpoint

### Community 33 - "v1/AddProduct/Request.cs"
Cohesion: 0.15
Nodes (11): Products.Endpoints.Stores.v1.AddProduct, CancellationToken, RouteGroupBuilder, Task, Endpoint, RequestBody, Request, RequestBody (+3 more)

### Community 34 - ".HandleAsync"
Cohesion: 0.20
Nodes (10): GetSeedUserIdsRequest, GetSeedUserIdsResponse, CancellationToken, Task, GetSeedUserIdsRequestHandler, CancellationToken, IResult, RouteGroupBuilder (+2 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.27
Nodes (5): OutboxMessage, DateTimeOffset, TimeSpan, EntityTypeBuilder, OutboxMessageConfig

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.23
Nodes (9): CustomRateLimitingOptions, CustomRateLimitingOptionsValidator, FixedWindow, FixedWindowValidator, IReadOnlyList, Policies, Action, IEnumerable (+1 more)

### Community 37 - "Seeder"
Cohesion: 0.16
Nodes (11): Task, IdentityRole, ILogger, LoggerMessage, Task, Seeder, Action, DateOnly (+3 more)

### Community 38 - "RequestResponseBodyLoggingMiddleware"
Cohesion: 0.20
Nodes (7): IApplicationBuilder, HttpContext, IList, PathString, RequestDelegate, string, RequestResponseBodyLoggingMiddleware

### Community 39 - ".SearchUsersAsync"
Cohesion: 0.29
Nodes (5): CancellationToken, IOptions, RouteGroupBuilder, Task, Endpoint

### Community 40 - "CheckRegistrationRateLimitingPolicy"
Cohesion: 0.18
Nodes (11): IRateLimiterPolicy, CancellationToken, Func, OnRejectedContext, ValueTask, CheckRegistrationRateLimitingPolicy, CancellationToken, Func (+3 more)

### Community 41 - "AuditableEntityResponse"
Cohesion: 0.12
Nodes (15): Products.Endpoints.Products.v1.My.Get, Products.Endpoints.Products.v1.Search, Common.Application.DTOs, Products.Endpoints.Products.v1.My.Search, IAM.Endpoints.Users.VersionNeutral.Me.Get, Products.Endpoints.Stores.v1.My.Get, AuditableEntityResponse, DateTimeOffset (+7 more)

### Community 42 - ".SaveWithOutboxAsync"
Cohesion: 0.13
Nodes (17): EventDispatcher, ActivitySource, CancellationToken, ILogger, LoggerMessage, Task, OutboxSaveHelper, CancellationToken (+9 more)

### Community 43 - "CreateStoreRateLimitingPolicy"
Cohesion: 0.12
Nodes (13): CancellationToken, Func, HttpContext, OnRejectedContext, RateLimiterOptions, RateLimitPartition, TimeSpan, ValueTask (+5 more)

### Community 44 - "Product"
Cohesion: 0.23
Nodes (5): ISearchLocalized, Product, IReadOnlyCollection, List, Store

### Community 45 - "ProductTemplateId"
Cohesion: 0.11
Nodes (15): Products.Endpoints.ProductTemplates.v1.Create, Products.Endpoints.ProductTemplates.v1.Activate, ProductTemplateId, Request, RequestValidator, CancellationToken, Task, Request (+7 more)

### Community 46 - "Setup"
Cohesion: 0.29
Nodes (4): ApiVersionSet, Setup, IEndpointRouteBuilder, IServiceCollection

### Community 48 - "OtpServiceBase"
Cohesion: 0.05
Nodes (28): Common.Application.Caching, Notifications.Infrastructure.Otp, SemaphoreSlim, CacheKeys, For, OtpCacheEntry, CancellationToken, Task (+20 more)

### Community 50 - "IntegrationEventHandlerBase"
Cohesion: 0.32
Nodes (8): IntegrationEventHandlerBase, CancellationToken, ConsumeContext, DefaultIdType, ILogger, LoggerMessage, Task, TimeSpan

### Community 51 - ".SendOtp"
Cohesion: 0.08
Nodes (19): IAM.Endpoints.Captcha.VersionNeutral, IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, IAM.Application.Captcha.Services, IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, CancellationToken, Task, ICaptchaService, RouteGroupBuilder (+11 more)

### Community 52 - "ReCaptchaService"
Cohesion: 0.08
Nodes (18): IAM.Domain.Captcha, IAM.Infrastructure.Captcha.Services, DateTime, double, FormUrlEncodedContent, ReCaptchaResponse, CaptchaErrors, CancellationToken (+10 more)

### Community 53 - "StoreId"
Cohesion: 0.11
Nodes (14): Products.Infrastructure.Persistence.Seeding, StoreId, CancellationToken, ILogger, int, LoggerMessage, Task, CancellationToken (+6 more)

### Community 54 - ".AddResilientHttpClient"
Cohesion: 0.22
Nodes (7): Common.Infrastructure.Resiliency, HttpClient, HttpStandardResilienceOptions, IHttpClientBuilder, Setup, Action, IServiceCollection

### Community 55 - ".FixedWindow"
Cohesion: 0.18
Nodes (7): RateLimitPartitions, HttpContext, RateLimitPartition, HttpContext, RateLimitPartition, HttpContext, RateLimitPartition

### Community 56 - "Notifications.Application.Sms"
Cohesion: 0.16
Nodes (9): Notifications.Application.Sms, Notifications.Infrastructure.Sms, Notifications.Infrastructure.Sms.NetGsm, SmsErrors, CancellationToken, ILogger, LoggerMessage, Task (+1 more)

### Community 58 - "IStronglyTypedId"
Cohesion: 0.06
Nodes (31): JsonConverter, StrictDateTimeOffsetJsonConverter, DateTimeOffset, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, StronglyTypedIdListReadOnlyJsonConverter (+23 more)

### Community 59 - "NotificationPayload"
Cohesion: 0.25
Nodes (9): CancellationToken, IReadOnlyList, Task, INotificationDispatcher, NotificationPayload, CancellationToken, IReadOnlyList, Task (+1 more)

### Community 61 - ".AddProductToMyStoreAsync"
Cohesion: 0.14
Nodes (11): Products.Endpoints.Stores.v1.My.AddProduct, decimal, int, Constants, CancellationToken, RouteGroupBuilder, Task, Endpoint (+3 more)

### Community 62 - "RequireFeatureFilter"
Cohesion: 0.10
Nodes (16): Common.Application.EndpointFilters, IEndpointFilter, ResultToCreatedResponseTransformer, ResultToResponseTransformer, EndpointFilterDelegate, EndpointFilterInvocationContext, ValueTask, RouteHandlerBuilderExtensions (+8 more)

### Community 63 - "NotificationsHub"
Cohesion: 0.21
Nodes (8): Hub, Task, INotificationsClient, Exception, ILogger, LoggerMessage, Task, NotificationsHub

### Community 64 - "InterModuleRequestHandler"
Cohesion: 0.18
Nodes (8): IConsumer, IInterModuleRequestHandler, CancellationToken, Task, InterModuleRequestHandler, CancellationToken, ConsumeContext, Task

### Community 65 - ".SavingChangesAsync"
Cohesion: 0.15
Nodes (11): SaveChangesInterceptor, ApplyAuditingInterceptor, CancellationToken, DbContextEventData, InterceptionResult, ValueTask, ApplySearchLanguageInterceptor, CancellationToken (+3 more)

### Community 66 - "OutboxTelemetry"
Cohesion: 0.17
Nodes (10): CancellationToken, Task, ActivitySource, Counter, Histogram, long, Meter, ObservableGauge (+2 more)

### Community 67 - ".GetProductTemplateAsync"
Cohesion: 0.22
Nodes (6): Products.Endpoints.ProductTemplates.v1.Get, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response

### Community 68 - "ValueObject"
Cohesion: 0.25
Nodes (4): Common.Domain, IComparable, ValueObject, IEnumerable

### Community 69 - "DomainEventHandlerBase"
Cohesion: 0.19
Nodes (10): DomainEventHandlerBase, IEventHandler, CancellationToken, Task, CancellationToken, Task, SimulateSomeBusinessHandler, StoreCreatedIntegrationEventPublishingHandler (+2 more)

### Community 70 - ".SearchProductTemplatesAsync"
Cohesion: 0.14
Nodes (11): Products.Endpoints.ProductTemplates.v1.Search, int, Constants, CancellationToken, IOptions, RouteGroupBuilder, Task, Endpoint (+3 more)

### Community 71 - "Full-Text Search"
Cohesion: 0.08
Nodes (25): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Add a new language/culture, Add search to a new entity _(Build checklist)_ (+17 more)

### Community 72 - ".CreateMyStoreAsync"
Cohesion: 0.20
Nodes (6): CancellationToken, Task, CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 73 - "V1SessionRevokedDomainEvent"
Cohesion: 0.50
Nodes (4): CancellationToken, Task, V1SessionRevokedDomainEventHandler, V1SessionRevokedDomainEvent

### Community 75 - "ProductId"
Cohesion: 0.16
Nodes (11): Products.Endpoints.Stores.v1.My.RemoveProduct, Products.Endpoints.Stores.v1.RemoveProduct, ProductId, Request, RequestValidator, Request, RequestValidator, Request (+3 more)

### Community 76 - "CorsOptions.cs"
Cohesion: 0.67
Nodes (3): CorsOptions, CorsOptionsValidator, IReadOnlyList

### Community 77 - "ICurrentUser"
Cohesion: 0.09
Nodes (15): ICurrentUser, Guid, ICollection, CancellationToken, RouteGroupBuilder, Task, Endpoint, CancellationToken (+7 more)

### Community 78 - "TokenCreateRateLimitingPolicy"
Cohesion: 0.25
Nodes (7): CancellationToken, Func, HttpContext, OnRejectedContext, RateLimitPartition, ValueTask, TokenCreateRateLimitingPolicy

### Community 79 - "Common.IntegrationEvents"
Cohesion: 0.18
Nodes (7): Common.IntegrationEvents, Notifications.Application.IntegrationEventHandlers, IAM.Application.Users.DomainEventHandlers.v1, Products.Application.Stores.DomainEventHandlers.v1, Common.Application.EventBus, Setup, IServiceCollection

### Community 80 - "RequestLoggingOptions"
Cohesion: 0.23
Nodes (9): IPostConfigureOptions, BackgroundJobsOptions, BackgroundJobsOptionsValidator, RequestLoggingOptions, RequestLoggingOptionsValidator, SensitivePathRule, IList, int (+1 more)

### Community 81 - ".GenerateAccessToken"
Cohesion: 0.20
Nodes (7): accessToken, DateTimeOffset, expiresAt, ICollection, refreshTokenBytes, TokenService, NotificationGroupName

### Community 82 - "StringExtensions"
Cohesion: 0.33
Nodes (3): Common.Domain.Extensions, SearchValues, StringExtensions

### Community 83 - ".EnsureNoMigrationsPending"
Cohesion: 0.44
Nodes (4): IServiceProvider, MigrationGuard, ILogger, LoggerMessage

### Community 84 - "AuditableEntityConfiguration"
Cohesion: 0.21
Nodes (8): AuditableEntityConfiguration, EntityTypeBuilder, EntityTypeBuilder, SessionConfig, EntityTypeBuilder, ProductConfiguration, EntityTypeBuilder, StoreConfiguration

### Community 85 - "ResultTelemetryExtensions"
Cohesion: 0.32
Nodes (4): Activity, ResultTelemetryExtensions, ActivitySource, Task

### Community 86 - "AuditLogRetentionJobRegistrar"
Cohesion: 0.11
Nodes (16): Common.Infrastructure.Persistence.AuditLog, IHostedService, AuditLogRetentionJobRegistrar, CancellationToken, ILogger, LoggerMessage, string, Task (+8 more)

### Community 87 - "TokenRefreshRateLimitingPolicy"
Cohesion: 0.25
Nodes (7): CancellationToken, Func, HttpContext, OnRejectedContext, RateLimitPartition, ValueTask, TokenRefreshRateLimitingPolicy

### Community 89 - "Setup"
Cohesion: 0.33
Nodes (4): ConfigurationManager, Host.Configurations, Setup, WebApplicationBuilder

### Community 90 - "VersionNeutral/Create/Request.cs"
Cohesion: 0.29
Nodes (6): IAM.Endpoints.Tokens.VersionNeutral.Create, Guid, Request, RequestValidator, DateTimeOffset, Response

### Community 91 - "CaptchaOptions.cs"
Cohesion: 0.31
Nodes (6): CaptchaOptions, CaptchaOptionsValidator, CaptchaProvider, IConfiguration, IServiceCollection, Setup

### Community 92 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.12
Nodes (6): Common.Domain.StronglyTypedIds, IAM.Domain.Identity.DomainEvents.v1, Common.Domain.Events, Common.Domain.Entities, Common.Domain.Aggregates, Products.Domain.Stores.DomainEvents.v1

### Community 93 - ".SearchStoresAsync"
Cohesion: 0.29
Nodes (5): CancellationToken, IOptions, RouteGroupBuilder, Task, Endpoint

### Community 94 - "Common.Infrastructure.Extensions"
Cohesion: 0.40
Nodes (3): Common.Infrastructure.RateLimiting, Common.Infrastructure.Extensions, IAM.Infrastructure.RateLimiting

### Community 95 - "RefreshToken"
Cohesion: 0.25
Nodes (6): DateTimeOffset, RefreshToken, RefreshTokenId, SessionId, EntityTypeBuilder, RefreshTokenConfig

### Community 96 - "FirebasePushGateway.cs"
Cohesion: 0.10
Nodes (15): Notifications.Application.Push, Notifications.Infrastructure.Push, Notifications.Infrastructure.Push.Firebase, CancellationToken, Task, IPushGateway, PushErrors, CancellationToken (+7 more)

### Community 97 - ".MapCode"
Cohesion: 0.40
Nodes (3): Exception, ILogger, LoggerMessage

### Community 98 - ".SearchStoreProductsAsync"
Cohesion: 0.18
Nodes (8): ISearchLanguageResolver, SearchLanguageResolver, string, CancellationToken, IOptions, RouteGroupBuilder, Task, Endpoint

### Community 99 - ".AuthorizeAsync"
Cohesion: 0.17
Nodes (7): DashboardContext, IDashboardAsyncAuthorizationFilter, CustomPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder, HangfireCustomAuthorizationFilter, Task

### Community 100 - ".SearchMyProductsAsync"
Cohesion: 0.29
Nodes (5): CancellationToken, IOptions, RouteGroupBuilder, Task, Endpoint

### Community 101 - "ApplicationUser"
Cohesion: 0.15
Nodes (10): IdentityUser, IEnumerable, IReadOnlyCollection, List, ApplicationUser, DateTimeOffset, DateOnly, IReadOnlyCollection (+2 more)

### Community 102 - "Common.Infrastructure.FeatureManagement"
Cohesion: 0.17
Nodes (8): Common.Infrastructure.FeatureManagement, ITargetingContextAccessor, HttpContextTargetingContextAccessor, ValueTask, Setup, IConfiguration, IServiceCollection, TargetingContext

### Community 103 - "Common.Application.FeatureManagement"
Cohesion: 0.13
Nodes (13): Common.Application.FeatureManagement, IVariantFeatureManager, Checkout, FeatureFlags, IAM, Notifications, Products, string (+5 more)

### Community 104 - ".GetStoreAuditLogAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 105 - "OutboxMetricsJob.cs"
Cohesion: 0.25
Nodes (5): Outbox, Outbox.Telemetry, ILogger, LoggerMessage, OutboxMetricsJob

### Community 106 - ".InvokeAsync"
Cohesion: 0.29
Nodes (5): IMiddleware, HttpContext, RequestDelegate, Task, EnrichLogsWithUserInfoMiddleware

### Community 107 - "SeedingCompletionTracker"
Cohesion: 0.22
Nodes (5): SeedingCompletionTracker, CancellationToken, Exception, Task, TaskCompletionSource

### Community 109 - ".IsRegisteredAsync"
Cohesion: 0.20
Nodes (8): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator, Response

### Community 110 - "Common.Application.JsonConverters"
Cohesion: 0.29
Nodes (5): Products.Endpoints.Stores.v1.Create, Common.Application.JsonConverters, Request, RequestValidator, Response

### Community 111 - "IBackgroundJobs"
Cohesion: 0.26
Nodes (7): IBackgroundJobs, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 112 - "UserRegisteredSignalRHandler"
Cohesion: 0.28
Nodes (6): UserRegisteredIntegrationEvent, CancellationToken, ILogger, LoggerMessage, Task, UserRegisteredSignalRHandler

### Community 113 - "Endpoint"
Cohesion: 0.29
Nodes (5): Products.Endpoints.ProductTemplates, RouteGroupBuilder, Setup, RouteGroupBuilder, Endpoint

### Community 114 - "OutboxCleanupJob.cs"
Cohesion: 0.33
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, OutboxCleanupJob

### Community 115 - "BackgroundJobsService"
Cohesion: 0.26
Nodes (7): BackgroundJobsService, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 116 - "IModule"
Cohesion: 0.10
Nodes (15): ICoreModule, IModule, Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection (+7 more)

### Community 117 - "ApiKeyAuthenticationHandler"
Cohesion: 0.16
Nodes (9): AuthenticateResult, AuthenticationHandler, AuthenticationProperties, AuthenticationSchemeOptions, ILogger, LoggerMessage, Task, ApiKeyAuthenticationHandler (+1 more)

### Community 118 - "ObservabilityOptions"
Cohesion: 0.11
Nodes (15): KeyValuePair, LoggerConfiguration, LoggerMinimumLevelConfiguration, OpenTelemetryBuilder, ResourceBuilder, ObservabilityOptions, ObservabilityOptionsValidator, Dictionary (+7 more)

### Community 119 - "ProductsModule"
Cohesion: 0.18
Nodes (8): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, ProductsModule

### Community 120 - "IAM.Infrastructure.Auth.ApiKey"
Cohesion: 0.25
Nodes (5): IAM.Infrastructure.Auth.ApiKey, string, ApiKeyDefaults, AuthenticationBuilder, Setup

### Community 121 - "IntegrationEventOutbox"
Cohesion: 0.25
Nodes (5): Lock, IIntegrationEventOutbox, IntegrationEventOutbox, IReadOnlyList, List

### Community 122 - "SmsRateLimitingPolicy"
Cohesion: 0.25
Nodes (7): CancellationToken, Func, HttpContext, OnRejectedContext, RateLimitPartition, ValueTask, SmsRateLimitingPolicy

### Community 123 - "NotificationsModule"
Cohesion: 0.22
Nodes (6): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, NotificationsModule

### Community 124 - ".LogHealthChecksRegistered"
Cohesion: 0.33
Nodes (4): IApplicationBuilder, ILogger, LoggerMessage, WebApplication

### Community 125 - ".TapWhenFeatureEnabledAsync"
Cohesion: 0.29
Nodes (5): FeatureFlagResultExtensions, Action, Func, IFeatureManager, Task

### Community 126 - "GlobalExceptionHandlingMiddleware"
Cohesion: 0.29
Nodes (7): Exception, HttpContext, ILogger, LoggerMessage, RequestDelegate, Task, GlobalExceptionHandlingMiddleware

### Community 127 - "AuditLogEntry"
Cohesion: 0.15
Nodes (11): AuditableEntity, DateTimeOffset, AuditLogEntry, DefaultIdType, BaseDbContext, CancellationToken, DbSet, ModelConfigurationBuilder (+3 more)

### Community 128 - "ValueConverter"
Cohesion: 0.18
Nodes (9): DomainEventConverter, JsonSerializerOptions, IntegrationEventConverter, JsonSerializerOptions, StronglyTypedIdValueConverter, DefaultIdType, UtcDateTimeOffsetConverter, DateTimeOffset (+1 more)

### Community 129 - ".GetProductAsync"
Cohesion: 0.15
Nodes (9): Products.Endpoints.Products, Products.Endpoints.Products.v1.Get, RouteGroupBuilder, Setup, CancellationToken, RouteGroupBuilder, Task, Endpoint (+1 more)

### Community 130 - "Host Service (mm.host)"
Cohesion: 0.16
Nodes (17): Configuration-Driven Module Loading, IntegrationEvents (Async Cross-Module), IAM Module, Notifications Module, Products Module, Observability (OpenTelemetry), docker-compose.yml (Base Stack), docker-compose.app.yml (App-Only) (+9 more)

### Community 131 - "Common.Infrastructure.Persistence.Outbox"
Cohesion: 0.38
Nodes (3): Common.Infrastructure.Persistence.Outbox, Outbox.Persistence, Common.Application.Persistence.Outbox

### Community 132 - "Stores/v1/Search/Request.cs"
Cohesion: 0.40
Nodes (4): Products.Endpoints.Stores.v1.Search, Request, RequestValidator, Response

### Community 134 - "IProductsDbContext"
Cohesion: 0.11
Nodes (14): DbSet, IProductsDbContext, IReadOnlyList, List, ProductTemplate, CancellationToken, RouteGroupBuilder, Task (+6 more)

### Community 135 - ".ListSessions"
Cohesion: 0.17
Nodes (9): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, CancellationToken, IReadOnlyCollection, RouteGroupBuilder, Task, Endpoint, DateTimeOffset, Guid (+1 more)

### Community 136 - ".SingleAsResult"
Cohesion: 0.18
Nodes (10): CollectionExtensions, Func, ICollection, IEnumerable, PersistenceQueryableExtensions, CancellationToken, Expression, Func (+2 more)

### Community 137 - ".GlobalRateLimiter"
Cohesion: 0.17
Nodes (11): PartitionedRateLimiter, CancellationToken, Func, HttpContext, IConfiguration, IReadOnlyList, IServiceCollection, OnRejectedContext (+3 more)

### Community 138 - "IAM.Endpoints.Otp.VersionNeutral"
Cohesion: 0.40
Nodes (3): IAM.Endpoints.Otp.VersionNeutral, RouteGroupBuilder, Setup

### Community 139 - "My/Create/Request.cs"
Cohesion: 0.40
Nodes (4): Products.Endpoints.Stores.v1.My.Create, Request, RequestValidator, Response

### Community 140 - ".MapEndpoint"
Cohesion: 0.29
Nodes (4): IAM.Endpoints.Users.VersionNeutral, RouteGroupBuilder, RouteGroupBuilder, Setup

### Community 141 - "CurrentUser.cs"
Cohesion: 0.29
Nodes (4): Common.Infrastructure.Auth.Services, Common.Infrastructure.Auth, Setup, IServiceCollection

### Community 142 - ".ActivateProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 143 - "IIAMDbContext"
Cohesion: 0.11
Nodes (16): ChangeTracker, DatabaseFacade, EntityEntry, IDisposable, IDbContext, CancellationToken, DbSet, Task (+8 more)

### Community 144 - ".GetAsync"
Cohesion: 0.18
Nodes (9): IAM.Endpoints.Users.VersionNeutral.Get, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator, DateOnly (+1 more)

### Community 145 - "PermissionPolicyProvider.cs"
Cohesion: 0.40
Nodes (3): IAM.Application.Auth, string, CustomClaims

### Community 146 - ".PaginateAsync"
Cohesion: 0.29
Nodes (6): PaginationQueryableExtensions, CancellationToken, Expression, Func, IQueryable, Task

### Community 147 - "MassTransitInterModuleRequestClient.cs"
Cohesion: 0.40
Nodes (3): MassTransitInterModuleRequestClient, CancellationToken, Task

### Community 148 - ".AddInfrastructure"
Cohesion: 0.44
Nodes (4): Assembly, IConfiguration, IServiceCollection, IWebHostEnvironment

### Community 149 - ".GetRoleIdByName"
Cohesion: 0.20
Nodes (8): CancellationToken, DefaultIdType, Task, IRoleService, CancellationToken, DefaultIdType, Task, RoleService

### Community 150 - ".WriteAsync"
Cohesion: 0.33
Nodes (5): Memory, ReadOnlyMemory, CancellationToken, Task, ValueTask

### Community 151 - "FullTextSearchOptions"
Cohesion: 0.40
Nodes (5): FullTextSearchOptions, FullTextSearchOptionsValidator, Dictionary, IReadOnlyList, string

### Community 152 - ".UpdateStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 153 - "Host.Swagger"
Cohesion: 0.06
Nodes (26): ApiVersionDescription, Host.Swagger, IConfigureOptions, IOpenApiSchema, IOperationFilter, ISchemaFilter, JsonValue, OpenApiInfo (+18 more)

### Community 154 - ".AddOrUpdate"
Cohesion: 0.40
Nodes (4): Action, Expression, Func, Task

### Community 155 - "VerifyPhoneOtpRequest"
Cohesion: 0.33
Nodes (6): OtpVerificationFailureReason, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, CancellationToken, Task, VerifyPhoneOtpRequestHandler

### Community 156 - "Common.Application.ModelBinders"
Cohesion: 0.13
Nodes (13): Products.Endpoints.ProductTemplates.v1.Deactivate, Common.Application.ModelBinders, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, IModelBinder, ModelBindingContext, StronglyTypedIdBinder, Task, Request (+5 more)

### Community 157 - "SecurityHeadersMiddleware.cs"
Cohesion: 0.40
Nodes (3): HttpContext, Task, SecurityHeadersMiddleware

### Community 159 - ".AddOrUpdate"
Cohesion: 0.40
Nodes (4): Action, Expression, Func, Task

### Community 160 - "ValidationContextExtensions"
Cohesion: 0.40
Nodes (3): ValidationContextExtensions, string, ValidationContext

### Community 161 - ".GetMyStoreAuditLogAsync"
Cohesion: 0.22
Nodes (7): Products.Endpoints.Stores.v1.My.AuditLog, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator

### Community 162 - "PermissionAuthorizationHandler"
Cohesion: 0.29
Nodes (6): AuthorizationHandler, AuthorizationHandlerContext, IAuthorizationRequirement, Task, PermissionAuthorizationHandler, PermissionRequirement

### Community 163 - "V1UserRegisteredDomainEvent"
Cohesion: 0.50
Nodes (4): CancellationToken, Task, V1UserRegisteredDomainEventHandler, V1UserRegisteredDomainEvent

### Community 164 - "Common.Application.Options"
Cohesion: 0.10
Nodes (14): Common.Infrastructure.Modules, Common.Endpoints.Versioning, Host, Common.Infrastructure.Localization, Products.Infrastructure.RateLimiting, IAM.Endpoints, Common.Application.Options, Host.Middlewares (+6 more)

### Community 165 - "CurrentUser"
Cohesion: 0.18
Nodes (9): FrozenDictionary, IReadOnlySet, CustomPermissions, HashSet, IEnumerable, CurrentUser, Guid, HashSet (+1 more)

### Community 166 - "Endpoint"
Cohesion: 0.29
Nodes (5): IAM.Endpoints.Tokens.VersionNeutral, RouteGroupBuilder, Endpoint, RouteGroupBuilder, Setup

### Community 167 - "PaginationRequest"
Cohesion: 0.11
Nodes (17): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Products.v1.AuditLog, PaginationRequest, PaginationRequestValidator, int, int, Constants, Request (+9 more)

### Community 168 - "Response"
Cohesion: 0.50
Nodes (3): IAM.Endpoints.Users.VersionNeutral.Search, DateOnly, Response

### Community 171 - "Common.InterModuleRequests"
Cohesion: 0.29
Nodes (4): Common.InterModuleRequests, IAssemblyReference, Setup, IServiceCollection

### Community 172 - "BackgroundJobsTelemetry"
Cohesion: 0.11
Nodes (14): ConcurrentDictionary, BackgroundJobs.Telemetry, IServerFilter, PerformedContext, PerformingContext, JobMetricsFilter, string, BackgroundJobsTelemetry (+6 more)

### Community 174 - "IAM.Application.Tokens.DTOs"
Cohesion: 0.29
Nodes (5): IAM.Application.Tokens.DTOs, DateTimeOffset, AccessTokenDto, DateTimeOffset, TokensDto

### Community 175 - "PaginationResponse"
Cohesion: 0.14
Nodes (11): AuditLogDto, PaginationResponse, DbContextExtensions, CancellationToken, DbSet, JsonSerializerOptions, Task, CancellationToken (+3 more)

### Community 176 - "ApiKeysOptions.cs"
Cohesion: 0.48
Nodes (6): AbstractValidator, ApiKeyEntry, ApiKeyEntryValidator, ApiKeysOptions, ApiKeysOptionsValidator, IReadOnlyList

### Community 177 - "PermissionPolicyProvider"
Cohesion: 0.48
Nodes (4): AuthorizationPolicy, IAuthorizationPolicyProvider, Task, PermissionPolicyProvider

### Community 179 - "CachingOptions.cs"
Cohesion: 0.52
Nodes (6): CachingEntryDefaults, CachingOptions, CachingOptionsValidator, Redis, RedisValidator, TimeSpan

### Community 180 - "SmsOptions.cs"
Cohesion: 0.23
Nodes (9): SmsOptions, SmsOptionsValidator, SmsProvider, SmsTemplatesOptions, Dictionary, IConfiguration, IServiceCollection, long (+1 more)

### Community 181 - "Notifications.Infrastructure.Telemetry"
Cohesion: 0.15
Nodes (8): Notifications.Infrastructure.Telemetry, Notifications.Infrastructure.Hubs, Notifications.Application.Hubs, Notifications.Infrastructure, IConfiguration, IServiceCollection, Setup, IAssemblyReference

### Community 182 - "Constants"
Cohesion: 0.33
Nodes (4): IAM.Domain, string, Constants, IAssemblyReference

### Community 183 - "SendRequestBody"
Cohesion: 0.67
Nodes (3): SendMessageBody, IReadOnlyList, SendRequestBody

### Community 184 - "Setup"
Cohesion: 0.40
Nodes (3): IApplicationBuilder, IServiceCollection, Setup

### Community 185 - "IamModule"
Cohesion: 0.18
Nodes (8): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, IamModule

### Community 186 - ".RevokeSession"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 187 - "Caching/Setup.cs"
Cohesion: 0.33
Nodes (4): Common.Infrastructure.Caching, Setup, IConfiguration, IServiceCollection

### Community 188 - "IAM.Infrastructure.Auth"
Cohesion: 0.24
Nodes (5): ClaimsPrincipal, IAM.Infrastructure.Auth, ClaimsPrincipalExtensions, string, MultiAuthDefaults

### Community 190 - ".AddCustomMassTransit"
Cohesion: 0.50
Nodes (3): Assembly, IConfiguration, IServiceCollection

### Community 191 - "Split-Deployment PoC"
Cohesion: 0.25
Nodes (7): Concurrent safety, Cross-process call path, Files added by this PoC, How it works, How to run, Split-Deployment PoC, What this proves

### Community 192 - ".UpdateMyProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 194 - ".UpdateMyStoreAsync"
Cohesion: 0.22
Nodes (7): Products.Endpoints.Stores.v1.My.Update, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator

### Community 195 - "Setup"
Cohesion: 0.40
Nodes (3): IApplicationBuilder, IServiceCollection, Setup

### Community 197 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Probe.v1, Common.InterModuleRequests.IAM, Common.InterModuleRequests.Contracts, IAM.Infrastructure.InterModuleRequestHandlers, IInterModuleRequest, GetPushTokensRequest, GetPushTokensResponse, PushTarget (+3 more)

### Community 198 - "IntegrationEvent"
Cohesion: 0.15
Nodes (12): SessionTokenReuseDetectedIntegrationEvent, IntegrationEvent, DateTimeOffset, DefaultIdType, ProductCreatedIntegrationEvent, StoreCreatedIntegrationEvent, CancellationToken, Guid (+4 more)

### Community 200 - "NotificationsTelemetry"
Cohesion: 0.20
Nodes (6): ActivitySource, Counter, Meter, string, NotificationsTelemetry, UpDownCounter

### Community 203 - ".UpdateProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 206 - "OutboxDbContext"
Cohesion: 0.17
Nodes (9): DbContext, IOutboxDbContext, CancellationToken, DbSet, Task, DbSet, ModelBuilder, ModelConfigurationBuilder (+1 more)

### Community 211 - ".GetStoreAsync"
Cohesion: 0.22
Nodes (6): Products.Endpoints.Stores.v1.Get, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response

### Community 213 - "ProductsTelemetry"
Cohesion: 0.33
Nodes (5): ActivitySource, Counter, Meter, string, ProductsTelemetry

### Community 215 - ".DeactivateProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 216 - "Products.Endpoints.Probe"
Cohesion: 0.40
Nodes (3): Products.Endpoints.Probe, RouteGroupBuilder, Setup

### Community 217 - "PushOptions.cs"
Cohesion: 0.70
Nodes (4): FirebaseServiceAccountOptions, PushOptions, PushOptionsValidator, PushProvider

### Community 219 - ".UpdateCurrentSessionPushToken"
Cohesion: 0.20
Nodes (8): IAM.Endpoints.Tokens.VersionNeutral.Sessions.UpdatePushToken, CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint, Request, RequestValidator

### Community 226 - "ModulesOptions.cs"
Cohesion: 0.67
Nodes (3): ModulesOptions, ModulesOptionsValidator, IReadOnlyList

### Community 227 - "ResxLocalizationOptions.cs"
Cohesion: 0.67
Nodes (3): ResxLocalizationOptions, ResxLocalizationOptionsValidator, ICollection

### Community 228 - "CustomValidator"
Cohesion: 0.08
Nodes (28): Common.Application.Validation, AuditLogOptions, AuditLogOptionsValidator, DatabaseOptions, DatabaseOptionsValidator, InterModuleRequestOptions, InterModuleRequestOptionsValidator, OpenApiOptions (+20 more)

### Community 229 - ".AddCommonOptions"
Cohesion: 0.33
Nodes (4): Setup, IConfiguration, IHostEnvironment, IServiceCollection

### Community 231 - "Setup"
Cohesion: 0.40
Nodes (3): Setup, IApplicationBuilder, IServiceCollection

### Community 243 - "ActionsAndResources.cs"
Cohesion: 0.67
Nodes (3): CustomActions, CustomResources, string

### Community 244 - "CustomRoles"
Cohesion: 0.50
Nodes (3): CustomRoles, HashSet, string

### Community 249 - ".AddJwtBearerScheme"
Cohesion: 0.50
Nodes (3): AuthenticationBuilder, IConfiguration, Setup

## Knowledge Gaps
- **135 isolated node(s):** `OtpCacheEntry`, `Common.Domain`, `Common.Infrastructure`, `IAssemblyReference`, `PushTarget` (+130 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **99 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `IAM.Domain.Identity.Sessions`, `Common.Infrastructure.Persistence`, `Common.Domain.ResultMonad`, `.RegisterAndLoginAsync`, `.SendOtp`, `Common.Infrastructure.Persistence.ValueConverters`, `.SendAsync`, `MassTransitInterModuleRequestClient.cs`, `SelfRegister/Request.cs`, `FullTextSearchOptions`, `Host.Swagger`, `SecurityHeadersMiddleware.cs`, `CustomRateLimitingOptions`, `ApiKeysOptions.cs`, `OtpServiceBase`, `CachingOptions.cs`, `SmsOptions.cs`, `ReCaptchaService`, `Notifications.Infrastructure.Telemetry`, `.FixedWindow`, `Notifications.Application.Sms`, `Caching/Setup.cs`, `CorsOptions.cs`, `Common.IntegrationEvents`, `RequestLoggingOptions`, `AuditLogRetentionJobRegistrar`, `PushOptions.cs`, `VersionNeutral/Create/Request.cs`, `CaptchaOptions.cs`, `Common.Infrastructure.Extensions`, `FirebasePushGateway.cs`, `ModulesOptions.cs`, `ResxLocalizationOptions.cs`, `CustomValidator`, `.AddCommonOptions`, `.SearchStoreProductsAsync`, `OutboxMetricsJob.cs`, `OutboxCleanupJob.cs`, `ObservabilityOptions`?**
  _High betweenness centrality (0.383) - this node is a cross-community bridge._
- **Why does `Setup` connect `Setup` to `Common.Application.Options`, `RequestResponseBodyLoggingMiddleware`, `RequestLoggingOptions`, `.AddInfrastructure`, `ObservabilityOptions`, `.LogHealthChecksRegistered`, `.AddCustomMassTransit`?**
  _High betweenness centrality (0.089) - this node is a cross-community bridge._
- **Why does `Common.Domain.StronglyTypedIds` connect `Common.Domain.StronglyTypedIds` to `IAM.Domain.Identity.Sessions`, `ApplicationUserId`, `Stores/v1/Search/Request.cs`, `Common.Infrastructure.Persistence`, `Common.Domain.ResultMonad`, `CurrentUser.cs`, `Common.Infrastructure.Persistence.ValueConverters`, `.GetAsync`, `Host.Swagger`, `Common.Application.ModelBinders`, `.HandleAsync`, `PaginationRequest`, `Response`, `AuditableEntityResponse`, `Notifications.Infrastructure.Telemetry`, `StoreId`, `IStronglyTypedId`, `IAM.Infrastructure.Auth`, `Common.InterModuleRequests.Contracts`, `IntegrationEvent`, `Common.IntegrationEvents`, `.GenerateAccessToken`, `.GetStoreAsync`, `.TryDeserialize`, `Common.Application.JsonConverters`, `UserRegisteredSignalRHandler`?**
  _High betweenness centrality (0.082) - this node is a cross-community bridge._
- **What connects `OtpCacheEntry`, `Common.Domain`, `Common.Infrastructure` to the rest of the system?**
  _135 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `IAM.Domain.Identity.Sessions` be split into smaller, more focused modules?**
  _Cohesion score 0.08416389811738649 - nodes in this community are weakly interconnected._
- **Should `RedisFixedWindowRateLimiter` be split into smaller, more focused modules?**
  _Cohesion score 0.1067193675889328 - nodes in this community are weakly interconnected._
- **Should `Common.Infrastructure.Persistence` be split into smaller, more focused modules?**
  _Cohesion score 0.07386363636363637 - nodes in this community are weakly interconnected._