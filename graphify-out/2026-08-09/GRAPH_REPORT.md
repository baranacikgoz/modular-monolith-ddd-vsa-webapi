# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-08-06)

## Corpus Check
- 464 files · ~67,565 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2961 nodes · 5242 edges · 297 communities (199 shown, 98 thin omitted)
- Extraction: 99% EXTRACTED · 1% INFERRED · 0% AMBIGUOUS · INFERRED: 63 edges (avg confidence: 0.8)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `2be86b6b`
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
- IAM.Endpoints.Common.Validations
- BoundedCaptureStream
- IDatabaseSeeder
- Notifications.Application.Otp
- IIAMDbContext
- NetGsmSmsGateway
- IEvent
- .GetMyProductAsync
- ProblemDetailsExtensions
- Result
- IAM.Infrastructure/Auth/Setup.cs
- .AddModules
- Endpoint
- v1/AddProduct/Request.cs
- .HandleAsync
- OutboxMessage
- CustomRateLimitingOptions
- Seeder
- .RefreshToken
- .SendToUserAsync
- .FixedWindow
- AuditableEntityResponse
- .SaveWithOutboxAsync
- CreateStoreRateLimitingPolicy
- Product
- ProductTemplateId
- ProductsModule.cs
- Outbox Misuse Check
- RedisOtpService
- Add Integration Event Command
- IntegrationEventHandlerBase
- ICaptchaService
- ReCaptchaService
- Seeder
- .AddResilientHttpClient
- CheckRegistrationRateLimitingPolicy
- Notifications.Application.Sms
- Cross-Module Reference Violation
- IStronglyTypedId
- NotificationPayload
- Bogus Test Data
- .AddProductToMyStoreAsync
- ResultToResponseTransformer.cs
- NotificationsHub
- InterModuleRequestHandler
- .SavingChangesAsync
- OutboxTelemetry
- .GetProductTemplateAsync
- ValueObject
- DomainEventHandlerBase
- .SearchProductTemplatesAsync
- Full-Text Search
- IProductsDbContext
- OtpServiceBase
- AsNoTracking Coverage Check
- ProductId
- Common.Application.Validation
- .GetMyStoreAsync
- TokenCreateRateLimitingPolicy
- Common.IntegrationEvents
- .DispatchAsync
- .GenerateAccessToken
- SelfRegister/Request.cs
- .EnsureNoMigrationsPending
- ProductTemplate
- ResultTelemetryExtensions
- AuditLogRetentionJobRegistrar
- .CreateProductTemplateAsync
- Configuration-Driven Module Registration
- Setup
- .CreateTokens
- CaptchaOptions.cs
- Common.Domain.StronglyTypedIds
- Endpoint
- Common.Infrastructure.Extensions
- RequireFeatureFilter
- FirebasePushGateway.cs
- .MapCode
- .SearchMyProductsAsync
- .AuthorizeAsync
- .SendCoreAsync
- ApplicationUser
- Common.Infrastructure.FeatureManagement
- Common.Application.FeatureManagement
- .SendAsync
- RouteHandlerBuilderExtensions
- Products/v1/Update/Request.cs
- SeedingCompletionTracker
- .TryDeserialize
- .IsRegisteredAsync
- StoreId
- IBackgroundJobs
- UserRegisteredSignalRHandler
- Endpoint
- OutboxCleanupJob.cs
- BackgroundJobsService
- BackgroundJobsModule.cs
- ApiKeyAuthenticationHandler
- IModule
- ProductsModule
- IAM.Infrastructure.Auth.ApiKey
- IntegrationEventOutbox
- TokenRefreshRateLimitingPolicy
- NotificationsModule.cs
- .LogHealthChecksRegistered
- .TapWhenFeatureEnabledAsync
- GlobalExceptionHandlingMiddleware
- BaseDbContext
- ValueConverter
- .GetProductAsync
- Host Service (mm.host)
- PaginationRequestValidator
- Tokens/Setup.cs
- Consumer Idempotency (IntegrationEventHandlerBase)
- .RemoveProductAsync
- .ListSessions
- .SingleAsResult
- .GlobalRateLimiter
- IAMModule.cs
- My/Create/Request.cs
- .MapEndpoint
- Common.Infrastructure/Auth/Setup.cs
- .ActivateProductTemplateAsync
- IDbContext
- .GetAsync
- PermissionPolicyProvider.cs
- .GetVariantAsync
- MassTransitInterModuleRequestClient.cs
- Setup
- .GetRoleIdByName
- FeatureFlags
- .RemoveMyProductAsync
- SendForLogin/Endpoint.cs
- Host.Swagger
- .AddOrUpdate
- VerifyPhoneOtpRequest
- Common.Application.ModelBinders
- SendForRegistration/Endpoint.cs
- .SendAsync
- .AddOrUpdate
- ValidationContextExtensions
- Stores/v1/My/Update/Request.cs
- PermissionAuthorizationHandler
- V1UserRegisteredDomainEvent
- Common.Application.Options
- .GetMeAsync
- Endpoint
- PaginationRequest
- v1/Request.cs
- Policies.CreateStore.cs
- Notifications.Application/IAssemblyReference.cs
- Common.InterModuleRequests
- BackgroundJobsTelemetry
- SecurityHeadersOptions.cs
- IAM.Application.Tokens.DTOs
- .GetMyStoreAuditLogAsync
- ApiKeysOptions.cs
- PermissionPolicyProvider
- EventBus/Setup.cs
- CachingOptions.cs
- SmsOptions.cs
- Notifications.Infrastructure.Hubs
- Constants
- SendRequestBody
- Setup
- IamModule
- InterModuleRequestOptions.cs
- Caching/Setup.cs
- IAM.Infrastructure.Auth
- Notifications.Infrastructure.Telemetry
- Split-Deployment PoC
- .UpdateMyProductAsync
- .UpdateMyStoreAsync
- Setup
- Common.InterModuleRequests.Contracts
- NotificationsTelemetry
- HttpContextExtensions.cs
- .UpdateProductAsync
- Infrastructure/StringExtensions.cs
- .GetStoreAsync
- ProductsTelemetry
- .DeactivateProductTemplateAsync
- PushOptions.cs
- Auditing/Setup.cs
- UpdatePushToken/Request.cs
- JwtClaimNames.cs
- IAutoMigrateMarker.cs
- ModulesOptions.cs
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

## Communities (297 total, 98 thin omitted)

### Community 0 - "IAM.Domain.Identity.Sessions"
Cohesion: 0.14
Nodes (12): IAM.Application.Tokens.Services, IAM.Application.Extensions, IAM.Endpoints.Otp, IAM.Domain.Identity, IAM.Domain.Identity.Sessions, IAM.Endpoints.Tokens.VersionNeutral.Revoke, IAM.Infrastructure.Telemetry, IAM.Application.Persistence (+4 more)

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
Cohesion: 0.13
Nodes (12): DateTimeOffset, Guid, DateTimeOffset, RefreshToken, RefreshTokenId, DateTimeOffset, Guid, IReadOnlyCollection (+4 more)

### Community 6 - "RedisFixedWindowRateLimiter"
Cohesion: 0.11
Nodes (17): IConnectionMultiplexer, RateLimiter, RateLimiterStatistics, RateLimitLease, FixedWindowLease, RedisFixedWindowRateLimiter, bool, CancellationToken (+9 more)

### Community 7 - "Common.Infrastructure.Persistence"
Cohesion: 0.08
Nodes (13): Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Common.Application.Persistence, IAM.Infrastructure.Persistence, IAM.Infrastructure.Persistence.Seeding, Common.Infrastructure.Persistence.Auditing, Common.Infrastructure.Persistence.DbContext, Setup (+5 more)

### Community 8 - "FirebasePushGateway"
Cohesion: 0.24
Nodes (8): FirebaseApp, FirebaseMessaging, Exception, ILogger, int, LoggerMessage, TimeSpan, FirebasePushGateway

### Community 9 - "Products/v1/My/Update/Request.cs"
Cohesion: 0.16
Nodes (13): Products.Endpoints.Stores.v1.Update, Products.Endpoints.Products.v1.My.Update, RequestBody, Request, RequestBody, RequestBodyValidator, RequestValidator, RequestBodyValidator (+5 more)

### Community 10 - "Error"
Cohesion: 0.11
Nodes (12): HttpStatusCode, IdentityResult, IStringLocalizer, StringLocalizerExtensions, Error, ICollection, IResult, IdentityResultExtensions (+4 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.16
Nodes (12): Common.Application.Search, Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Products.Domain.Products, Products.Infrastructure.Telemetry, Products.Application.Persistence, Products.Domain.Stores (+4 more)

### Community 12 - ".RegisterAndLoginAsync"
Cohesion: 0.17
Nodes (16): accessToken, DateTimeOffset, expiresAt, ICollection, refreshTokenBytes, ITokenService, CancellationToken, HttpContext (+8 more)

### Community 13 - ".SendOtp"
Cohesion: 0.15
Nodes (14): IInterModuleRequestClient, CancellationToken, Task, SendPhoneOtpRequest, SendPhoneOtpResponse, CancellationToken, IFeatureManager, Task (+6 more)

### Community 14 - "LocalizedIdentityErrorDescriber"
Cohesion: 0.13
Nodes (4): IAM.Infrastructure.Identity, IdentityError, IdentityErrorDescriber, LocalizedIdentityErrorDescriber

### Community 15 - "Common.Infrastructure.Persistence.ValueConverters"
Cohesion: 0.18
Nodes (5): Common.Application.JsonConverters, IAM.Infrastructure.Persistence.EntityConfigurations, Common.Infrastructure.Persistence.EntityConfigurations, Common.Infrastructure.Persistence.ValueConverters, Products.Infrastructure.Persistence.EntityConfigurations

### Community 16 - ".SendAsync"
Cohesion: 0.16
Nodes (9): CancellationToken, Task, ISmsGateway, SmsCategory, SmsMessage, CancellationToken, Task, TimeSpan (+1 more)

### Community 17 - "Common.Application.BackgroundJobs"
Cohesion: 0.24
Nodes (5): Common.Application.BackgroundJobs, BackgroundJobs, RecurringJobOptions, IRecurringBackgroundJobs, RecurringBackgroundJobsService

### Community 18 - "DomainEvent"
Cohesion: 0.08
Nodes (28): AggregateRoot, IEnumerable, IReadOnlyCollection, List, IAggregateRoot, IEnumerable, IReadOnlyCollection, DomainEvent (+20 more)

### Community 19 - "OutboxModule"
Cohesion: 0.19
Nodes (10): Action, Exception, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, ILogger, IServiceCollection (+2 more)

### Community 20 - "IAM.Endpoints.Common.Validations"
Cohesion: 0.11
Nodes (15): IAM.Endpoints.Common.Validations, IResxLocalizer, IRuleBuilder, IRuleBuilderOptions, CommonValidations, IResxLocalizer, IRuleBuilder, IRuleBuilderOptions (+7 more)

### Community 21 - "BoundedCaptureStream"
Cohesion: 0.06
Nodes (27): byte, IPostConfigureOptions, Memory, ReadOnlyMemory, ReadOnlySpan, SeekOrigin, RequestLoggingOptions, RequestLoggingOptionsValidator (+19 more)

### Community 22 - "IDatabaseSeeder"
Cohesion: 0.14
Nodes (9): IDatabaseSeeder, CancellationToken, Task, CancellationToken, Task, IamDatabaseSeeder, CancellationToken, Task (+1 more)

### Community 23 - "Notifications.Application.Otp"
Cohesion: 0.14
Nodes (9): Notifications.Application.Otp, Common.Application.Caching, Notifications.Infrastructure.Otp, CacheKeys, For, OtpCacheEntry, IConfiguration, IServiceCollection (+1 more)

### Community 24 - "IIAMDbContext"
Cohesion: 0.05
Nodes (39): IFusionCache, ICurrentUser, Guid, ICollection, DbSet, IdentityRole, IdentityRoleClaim, IdentityUserClaim (+31 more)

### Community 25 - "NetGsmSmsGateway"
Cohesion: 0.20
Nodes (8): SendRequestBody, CancellationToken, JsonSerializerOptions, string, Task, NetGsmSmsGateway, SendMessageBody, SendResponseBody

### Community 26 - "IEvent"
Cohesion: 0.20
Nodes (7): CancellationToken, Task, IOutboxMessage, DateTimeOffset, IEvent, DateTimeOffset, DefaultIdType

### Community 27 - ".GetMyProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 28 - "ProblemDetailsExtensions"
Cohesion: 0.40
Nodes (3): ProblemDetails, ProblemDetailsExtensions, ICollection

### Community 29 - "Result"
Cohesion: 0.16
Nodes (10): Result, Func, Task, AsyncExtensions, SyncExtensions, Action, Func, Task (+2 more)

### Community 30 - "IAM.Infrastructure/Auth/Setup.cs"
Cohesion: 0.22
Nodes (6): IAM.Infrastructure.Auth.Jwt, IAM.Infrastructure.Auth.Services, IAM.Application.Auth.Services, IConfiguration, IServiceCollection, Setup

### Community 31 - ".AddModules"
Cohesion: 0.12
Nodes (13): LoadAll, Names, Assembly, Exception, IApplicationBuilder, IConfiguration, IEnumerable, ILogger (+5 more)

### Community 32 - "Endpoint"
Cohesion: 0.29
Nodes (5): Products.Endpoints.Stores, RouteGroupBuilder, Setup, RouteGroupBuilder, Endpoint

### Community 33 - "v1/AddProduct/Request.cs"
Cohesion: 0.12
Nodes (13): Products.Endpoints.Stores.v1.AddProduct, decimal, int, Constants, CancellationToken, RouteGroupBuilder, Task, Endpoint (+5 more)

### Community 34 - ".HandleAsync"
Cohesion: 0.20
Nodes (10): GetSeedUserIdsRequest, GetSeedUserIdsResponse, CancellationToken, Task, GetSeedUserIdsRequestHandler, CancellationToken, IResult, RouteGroupBuilder (+2 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.16
Nodes (10): OutboxMessage, DateTimeOffset, TimeSpan, IntegrationEvent, DateTimeOffset, DefaultIdType, ProductCreatedIntegrationEvent, StoreCreatedIntegrationEvent (+2 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.23
Nodes (9): CustomRateLimitingOptions, CustomRateLimitingOptionsValidator, FixedWindow, FixedWindowValidator, IReadOnlyList, Policies, Action, IEnumerable (+1 more)

### Community 37 - "Seeder"
Cohesion: 0.15
Nodes (11): Task, IdentityRole, ILogger, LoggerMessage, Task, Seeder, Action, DateOnly (+3 more)

### Community 38 - ".RefreshToken"
Cohesion: 0.13
Nodes (14): IAM.Endpoints.Tokens.VersionNeutral.Refresh, CancellationToken, HttpContext, ILogger, IOptions, LoggerMessage, RouteGroupBuilder, Task (+6 more)

### Community 39 - ".SendToUserAsync"
Cohesion: 0.40
Nodes (4): CancellationToken, IReadOnlyList, Task, SignalRNotificationDispatcher

### Community 40 - ".FixedWindow"
Cohesion: 0.10
Nodes (18): IRateLimiterPolicy, RateLimitPartitions, HttpContext, RateLimitPartition, CancellationToken, Func, HttpContext, OnRejectedContext (+10 more)

### Community 41 - "AuditableEntityResponse"
Cohesion: 0.05
Nodes (33): IAM.Endpoints.Users.VersionNeutral.Search, Products.Endpoints.Stores.v1.Search, Products.Endpoints.Products.v1.My.Get, Products.Endpoints.ProductTemplates.v1.Search, Products.Endpoints.Products.v1.Search, Common.Application.DTOs, IAM.Endpoints.Users.VersionNeutral.Get, Products.Endpoints.Stores.v1.Get (+25 more)

### Community 42 - ".SaveWithOutboxAsync"
Cohesion: 0.21
Nodes (11): OutboxSaveHelper, CancellationToken, DbContext, Exception, Func, ILogger, LoggerMessage, Task (+3 more)

### Community 43 - "CreateStoreRateLimitingPolicy"
Cohesion: 0.20
Nodes (8): CancellationToken, Func, HttpContext, OnRejectedContext, RateLimitPartition, TimeSpan, ValueTask, CreateStoreRateLimitingPolicy

### Community 44 - "Product"
Cohesion: 0.17
Nodes (8): ISearchLocalized, Product, IReadOnlyCollection, List, Store, DbSet, ModelBuilder, ProductsDbContext

### Community 45 - "ProductTemplateId"
Cohesion: 0.14
Nodes (12): Products.Endpoints.ProductTemplates.v1.Deactivate, Products.Endpoints.ProductTemplates.v1.Activate, ProductTemplateId, Request, RequestValidator, Request, RequestValidator, Request (+4 more)

### Community 46 - "ProductsModule.cs"
Cohesion: 0.12
Nodes (10): ApiVersionSet, Common.Endpoints.Versioning, Products.Endpoints.Probe, Products.Endpoints, Setup, IEndpointRouteBuilder, IServiceCollection, IAssemblyReference (+2 more)

### Community 48 - "RedisOtpService"
Cohesion: 0.15
Nodes (11): CancellationToken, Task, TimeSpan, IOtpService, OtpVerificationOutcome, CancellationToken, int, string (+3 more)

### Community 50 - "IntegrationEventHandlerBase"
Cohesion: 0.32
Nodes (8): IntegrationEventHandlerBase, CancellationToken, ConsumeContext, DefaultIdType, ILogger, LoggerMessage, Task, TimeSpan

### Community 51 - "ICaptchaService"
Cohesion: 0.15
Nodes (8): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, CancellationToken, Task, ICaptchaService, Response, CancellationToken, Task, CachedCaptchaService

### Community 52 - "ReCaptchaService"
Cohesion: 0.17
Nodes (11): DateTime, double, FormUrlEncodedContent, ReCaptchaResponse, CancellationToken, Exception, ILogger, LoggerMessage (+3 more)

### Community 53 - "Seeder"
Cohesion: 0.15
Nodes (10): Products.Infrastructure.Persistence.Seeding, CancellationToken, ILogger, int, LoggerMessage, Task, CancellationToken, List (+2 more)

### Community 54 - ".AddResilientHttpClient"
Cohesion: 0.22
Nodes (7): Common.Infrastructure.Resiliency, HttpClient, HttpStandardResilienceOptions, IHttpClientBuilder, Setup, Action, IServiceCollection

### Community 55 - "CheckRegistrationRateLimitingPolicy"
Cohesion: 0.25
Nodes (7): CancellationToken, Func, HttpContext, OnRejectedContext, RateLimitPartition, ValueTask, CheckRegistrationRateLimitingPolicy

### Community 56 - "Notifications.Application.Sms"
Cohesion: 0.16
Nodes (9): Notifications.Application.Sms, Notifications.Infrastructure.Sms, Notifications.Infrastructure.Sms.NetGsm, SmsErrors, CancellationToken, ILogger, LoggerMessage, Task (+1 more)

### Community 58 - "IStronglyTypedId"
Cohesion: 0.06
Nodes (31): JsonConverter, StrictDateTimeOffsetJsonConverter, DateTimeOffset, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, StronglyTypedIdListReadOnlyJsonConverter (+23 more)

### Community 59 - "NotificationPayload"
Cohesion: 0.22
Nodes (8): Notifications.Application.Hubs, CancellationToken, IReadOnlyList, Task, INotificationDispatcher, Task, INotificationsClient, NotificationPayload

### Community 61 - ".AddProductToMyStoreAsync"
Cohesion: 0.16
Nodes (8): Products.Endpoints.Stores.v1.My.AddProduct, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator, Response

### Community 62 - "ResultToResponseTransformer.cs"
Cohesion: 0.33
Nodes (6): IEndpointFilter, ResultToCreatedResponseTransformer, ResultToResponseTransformer, EndpointFilterDelegate, EndpointFilterInvocationContext, ValueTask

### Community 63 - "NotificationsHub"
Cohesion: 0.33
Nodes (6): Hub, Exception, ILogger, LoggerMessage, Task, NotificationsHub

### Community 64 - "InterModuleRequestHandler"
Cohesion: 0.18
Nodes (8): IConsumer, IInterModuleRequestHandler, CancellationToken, Task, InterModuleRequestHandler, CancellationToken, ConsumeContext, Task

### Community 65 - ".SavingChangesAsync"
Cohesion: 0.15
Nodes (11): SaveChangesInterceptor, ApplyAuditingInterceptor, CancellationToken, DbContextEventData, InterceptionResult, ValueTask, ApplySearchLanguageInterceptor, CancellationToken (+3 more)

### Community 66 - "OutboxTelemetry"
Cohesion: 0.07
Nodes (22): Outbox, Common.Infrastructure.Persistence.Outbox, Outbox.Persistence, Common.Application.Persistence.Outbox, Outbox.Telemetry, IOutboxDbContext, CancellationToken, DbSet (+14 more)

### Community 67 - ".GetProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 68 - "ValueObject"
Cohesion: 0.25
Nodes (4): Common.Domain, IComparable, ValueObject, IEnumerable

### Community 69 - "DomainEventHandlerBase"
Cohesion: 0.14
Nodes (14): DomainEventHandlerBase, IEventHandler, CancellationToken, Task, CancellationToken, Task, V1SessionRevokedDomainEventHandler, V1SessionRevokedDomainEvent (+6 more)

### Community 70 - ".SearchProductTemplatesAsync"
Cohesion: 0.29
Nodes (5): CancellationToken, IOptions, RouteGroupBuilder, Task, Endpoint

### Community 71 - "Full-Text Search"
Cohesion: 0.08
Nodes (25): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Add a new language/culture, Add search to a new entity _(Build checklist)_ (+17 more)

### Community 72 - "IProductsDbContext"
Cohesion: 0.12
Nodes (12): DbSet, IProductsDbContext, CancellationToken, Task, CancellationToken, RouteGroupBuilder, Task, Endpoint (+4 more)

### Community 73 - "OtpServiceBase"
Cohesion: 0.15
Nodes (9): SemaphoreSlim, string, DummyOtpService, OtpService, CancellationToken, int, Task, TimeSpan (+1 more)

### Community 75 - "ProductId"
Cohesion: 0.16
Nodes (11): Products.Endpoints.Stores.v1.My.RemoveProduct, Products.Endpoints.Stores.v1.RemoveProduct, ProductId, Request, RequestValidator, Request, RequestValidator, Request (+3 more)

### Community 76 - "Common.Application.Validation"
Cohesion: 0.11
Nodes (16): Common.Application.Validation, AuditLogOptions, AuditLogOptionsValidator, CorsOptions, CorsOptionsValidator, IReadOnlyList, RabbitMqOptions, RabbitMqOptionsValidator (+8 more)

### Community 77 - ".GetMyStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 78 - "TokenCreateRateLimitingPolicy"
Cohesion: 0.25
Nodes (7): CancellationToken, Func, HttpContext, OnRejectedContext, RateLimitPartition, ValueTask, TokenCreateRateLimitingPolicy

### Community 79 - "Common.IntegrationEvents"
Cohesion: 0.13
Nodes (12): Common.IntegrationEvents, Notifications.Application.IntegrationEventHandlers, IAM.Application.Users.DomainEventHandlers.v1, Products.Application.Stores.DomainEventHandlers.v1, Common.Application.EventBus, SessionTokenReuseDetectedIntegrationEvent, CancellationToken, Guid (+4 more)

### Community 80 - ".DispatchAsync"
Cohesion: 0.19
Nodes (9): IEventHandlerWrapper, CancellationToken, Task, EventDispatcher, ActivitySource, CancellationToken, ILogger, LoggerMessage (+1 more)

### Community 81 - ".GenerateAccessToken"
Cohesion: 0.32
Nodes (6): accessToken, DateTimeOffset, expiresAt, ICollection, refreshTokenBytes, TokenService

### Community 82 - "SelfRegister/Request.cs"
Cohesion: 0.14
Nodes (9): IAM.Endpoints.Users.VersionNeutral.SelfRegister, Common.Domain.Extensions, SearchValues, StringExtensions, int, Constants, Guid, Request (+1 more)

### Community 83 - ".EnsureNoMigrationsPending"
Cohesion: 0.44
Nodes (4): IServiceProvider, MigrationGuard, ILogger, LoggerMessage

### Community 84 - "ProductTemplate"
Cohesion: 0.08
Nodes (23): AuditableEntity, DateTimeOffset, AuditLogEntry, DefaultIdType, IAuditableEntity, DateTimeOffset, AuditableEntityConfiguration, EntityTypeBuilder (+15 more)

### Community 85 - "ResultTelemetryExtensions"
Cohesion: 0.32
Nodes (4): Activity, ResultTelemetryExtensions, ActivitySource, Task

### Community 86 - "AuditLogRetentionJobRegistrar"
Cohesion: 0.11
Nodes (16): Common.Infrastructure.Persistence.AuditLog, IHostedService, AuditLogRetentionJobRegistrar, CancellationToken, ILogger, LoggerMessage, string, Task (+8 more)

### Community 87 - ".CreateProductTemplateAsync"
Cohesion: 0.28
Nodes (6): Products.Endpoints.ProductTemplates.v1.Create, CancellationToken, Task, Request, RequestValidator, Response

### Community 89 - "Setup"
Cohesion: 0.33
Nodes (4): ConfigurationManager, Host.Configurations, Setup, WebApplicationBuilder

### Community 90 - ".CreateTokens"
Cohesion: 0.15
Nodes (11): IAM.Endpoints.Tokens.VersionNeutral.Create, JwtOptions, JwtOptionsValidator, IReadOnlyCollection, CancellationToken, HttpContext, IOptions, Task (+3 more)

### Community 91 - "CaptchaOptions.cs"
Cohesion: 0.31
Nodes (6): CaptchaOptions, CaptchaOptionsValidator, CaptchaProvider, IConfiguration, IServiceCollection, Setup

### Community 92 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.09
Nodes (8): Products.Domain.Products.DomainEvents.v1, Common.Domain.StronglyTypedIds, IAM.Domain.Identity.DomainEvents.v1, Common.Domain.Events, Common.Infrastructure.EventBus, Common.Domain.Entities, Common.Domain.Aggregates, Products.Domain.Stores.DomainEvents.v1

### Community 93 - "Endpoint"
Cohesion: 0.29
Nodes (5): IAM.Endpoints.Captcha.VersionNeutral, RouteGroupBuilder, Endpoint, RouteGroupBuilder, Setup

### Community 94 - "Common.Infrastructure.Extensions"
Cohesion: 0.27
Nodes (5): Common.Infrastructure.RateLimiting, Common.Infrastructure.Extensions, IAM.Infrastructure.RateLimiting, string, Constants

### Community 95 - "RequireFeatureFilter"
Cohesion: 0.25
Nodes (7): RequireFeatureFilter, ActivitySource, Counter, EndpointFilterDelegate, EndpointFilterInvocationContext, Meter, ValueTask

### Community 96 - "FirebasePushGateway.cs"
Cohesion: 0.20
Nodes (7): Notifications.Application.Push, Notifications.Infrastructure.Push, Notifications.Infrastructure.Push.Firebase, PushErrors, IConfiguration, IServiceCollection, Setup

### Community 97 - ".MapCode"
Cohesion: 0.40
Nodes (3): Exception, ILogger, LoggerMessage

### Community 98 - ".SearchMyProductsAsync"
Cohesion: 0.06
Nodes (29): FullTextSearchOptions, FullTextSearchOptionsValidator, Dictionary, IReadOnlyList, string, PaginationResponse, ISearchLanguageResolver, SearchLanguageResolver (+21 more)

### Community 99 - ".AuthorizeAsync"
Cohesion: 0.17
Nodes (7): DashboardContext, IDashboardAsyncAuthorizationFilter, CustomPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder, HangfireCustomAuthorizationFilter, Task

### Community 100 - ".SendCoreAsync"
Cohesion: 0.36
Nodes (5): PushMessage, CancellationToken, IEnumerable, IReadOnlyList, Task

### Community 101 - "ApplicationUser"
Cohesion: 0.16
Nodes (10): IdentityUser, IEnumerable, IReadOnlyCollection, List, ApplicationUser, DateTimeOffset, DateOnly, IReadOnlyCollection (+2 more)

### Community 102 - "Common.Infrastructure.FeatureManagement"
Cohesion: 0.17
Nodes (8): Common.Infrastructure.FeatureManagement, ITargetingContextAccessor, HttpContextTargetingContextAccessor, ValueTask, Setup, IConfiguration, IServiceCollection, TargetingContext

### Community 103 - "Common.Application.FeatureManagement"
Cohesion: 0.29
Nodes (3): Common.Application.FeatureManagement, RouteHandlerBuilderExtensions, RouteHandlerBuilder

### Community 104 - ".SendAsync"
Cohesion: 0.29
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, DummyPushGateway

### Community 105 - "RouteHandlerBuilderExtensions"
Cohesion: 0.38
Nodes (3): Common.Application.EndpointFilters, RouteHandlerBuilderExtensions, RouteHandlerBuilder

### Community 106 - "Products/v1/Update/Request.cs"
Cohesion: 0.33
Nodes (6): Products.Endpoints.Products.v1.Update, RequestBody, Request, RequestBody, RequestBodyValidator, RequestValidator

### Community 107 - "SeedingCompletionTracker"
Cohesion: 0.22
Nodes (5): SeedingCompletionTracker, CancellationToken, Exception, Task, TaskCompletionSource

### Community 109 - ".IsRegisteredAsync"
Cohesion: 0.20
Nodes (8): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator, Response

### Community 110 - "StoreId"
Cohesion: 0.17
Nodes (8): Products.Endpoints.Stores.v1.Create, Products.Endpoints.Stores.v1.My.Create, StoreId, Response, Response, CancellationToken, List, Task

### Community 111 - "IBackgroundJobs"
Cohesion: 0.26
Nodes (7): IBackgroundJobs, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 112 - "UserRegisteredSignalRHandler"
Cohesion: 0.32
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

### Community 116 - "BackgroundJobsModule.cs"
Cohesion: 0.20
Nodes (6): BackgroundJobsModule, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection

### Community 117 - "ApiKeyAuthenticationHandler"
Cohesion: 0.16
Nodes (9): AuthenticateResult, AuthenticationHandler, AuthenticationProperties, AuthenticationSchemeOptions, ILogger, LoggerMessage, Task, ApiKeyAuthenticationHandler (+1 more)

### Community 118 - "IModule"
Cohesion: 0.10
Nodes (19): OpenTelemetryBuilder, ResourceBuilder, ObservabilityOptions, ObservabilityOptionsValidator, Dictionary, ICoreModule, IModule, Action (+11 more)

### Community 119 - "ProductsModule"
Cohesion: 0.18
Nodes (8): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, ProductsModule

### Community 120 - "IAM.Infrastructure.Auth.ApiKey"
Cohesion: 0.25
Nodes (5): IAM.Infrastructure.Auth.ApiKey, string, ApiKeyDefaults, AuthenticationBuilder, Setup

### Community 121 - "IntegrationEventOutbox"
Cohesion: 0.25
Nodes (5): Lock, IIntegrationEventOutbox, IntegrationEventOutbox, IReadOnlyList, List

### Community 122 - "TokenRefreshRateLimitingPolicy"
Cohesion: 0.25
Nodes (7): CancellationToken, Func, HttpContext, OnRejectedContext, RateLimitPartition, ValueTask, TokenRefreshRateLimitingPolicy

### Community 123 - "NotificationsModule.cs"
Cohesion: 0.15
Nodes (8): Notifications.Infrastructure, IAssemblyReference, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, NotificationsModule

### Community 124 - ".LogHealthChecksRegistered"
Cohesion: 0.33
Nodes (4): IApplicationBuilder, ILogger, LoggerMessage, WebApplication

### Community 125 - ".TapWhenFeatureEnabledAsync"
Cohesion: 0.29
Nodes (5): FeatureFlagResultExtensions, Action, Func, IFeatureManager, Task

### Community 126 - "GlobalExceptionHandlingMiddleware"
Cohesion: 0.16
Nodes (12): IMiddleware, HttpContext, RequestDelegate, Task, EnrichLogsWithUserInfoMiddleware, Exception, HttpContext, ILogger (+4 more)

### Community 127 - "BaseDbContext"
Cohesion: 0.14
Nodes (10): DbContext, BaseDbContext, CancellationToken, DbSet, ModelConfigurationBuilder, Task, DbSet, ModelBuilder (+2 more)

### Community 128 - "ValueConverter"
Cohesion: 0.15
Nodes (11): DomainEventConverter, JsonSerializerOptions, EventConverter, JsonSerializerOptions, IntegrationEventConverter, JsonSerializerOptions, StronglyTypedIdValueConverter, DefaultIdType (+3 more)

### Community 129 - ".GetProductAsync"
Cohesion: 0.20
Nodes (7): Products.Endpoints.Products, RouteGroupBuilder, Setup, CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 130 - "Host Service (mm.host)"
Cohesion: 0.16
Nodes (17): Configuration-Driven Module Loading, IntegrationEvents (Async Cross-Module), IAM Module, Notifications Module, Products Module, Observability (OpenTelemetry), docker-compose.yml (Base Stack), docker-compose.app.yml (App-Only) (+9 more)

### Community 131 - "PaginationRequestValidator"
Cohesion: 0.33
Nodes (5): Products.Endpoints.Products.v1.AuditLog, PaginationRequestValidator, int, Request, RequestValidator

### Community 132 - "Tokens/Setup.cs"
Cohesion: 0.33
Nodes (4): IAM.Infrastructure.Tokens, IAM.Infrastructure.Tokens.Services, IServiceCollection, Setup

### Community 134 - ".RemoveProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 135 - ".ListSessions"
Cohesion: 0.17
Nodes (9): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, CancellationToken, IReadOnlyCollection, RouteGroupBuilder, Task, Endpoint, DateTimeOffset, Guid (+1 more)

### Community 136 - ".SingleAsResult"
Cohesion: 0.18
Nodes (10): CollectionExtensions, Func, ICollection, IEnumerable, PersistenceQueryableExtensions, CancellationToken, Expression, Func (+2 more)

### Community 137 - ".GlobalRateLimiter"
Cohesion: 0.17
Nodes (11): PartitionedRateLimiter, CancellationToken, Func, HttpContext, IConfiguration, IReadOnlyList, IServiceCollection, OnRejectedContext (+3 more)

### Community 138 - "IAMModule.cs"
Cohesion: 0.11
Nodes (11): IAM.Domain.Captcha, IAM.Endpoints, IAM.Endpoints.Otp.VersionNeutral, IAM.Infrastructure.Captcha.Services, IAM.Application.Captcha.Services, IAM.Infrastructure.Captcha, CaptchaErrors, IAssemblyReference (+3 more)

### Community 140 - ".MapEndpoint"
Cohesion: 0.29
Nodes (4): IAM.Endpoints.Users.VersionNeutral, RouteGroupBuilder, RouteGroupBuilder, Setup

### Community 141 - "Common.Infrastructure/Auth/Setup.cs"
Cohesion: 0.33
Nodes (4): Common.Infrastructure.Auth.Services, Common.Infrastructure.Auth, Setup, IServiceCollection

### Community 142 - ".ActivateProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 143 - "IDbContext"
Cohesion: 0.20
Nodes (8): ChangeTracker, DatabaseFacade, EntityEntry, IDisposable, IDbContext, CancellationToken, DbSet, Task

### Community 144 - ".GetAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 145 - "PermissionPolicyProvider.cs"
Cohesion: 0.40
Nodes (3): IAM.Application.Auth, string, CustomClaims

### Community 146 - ".GetVariantAsync"
Cohesion: 0.33
Nodes (4): IVariantFeatureManager, IVariantFeatureManagerExtensions, CancellationToken, Task

### Community 147 - "MassTransitInterModuleRequestClient.cs"
Cohesion: 0.40
Nodes (3): MassTransitInterModuleRequestClient, CancellationToken, Task

### Community 148 - "Setup"
Cohesion: 0.06
Nodes (24): IHostBuilder, KeyValuePair, LoggerConfiguration, LoggerMinimumLevelConfiguration, Assembly, IApplicationBuilder, IConfiguration, IServiceCollection (+16 more)

### Community 149 - ".GetRoleIdByName"
Cohesion: 0.20
Nodes (8): CancellationToken, DefaultIdType, Task, IRoleService, CancellationToken, DefaultIdType, Task, RoleService

### Community 150 - "FeatureFlags"
Cohesion: 0.53
Nodes (6): Checkout, FeatureFlags, IAM, Notifications, Products, string

### Community 151 - ".RemoveMyProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 152 - "SendForLogin/Endpoint.cs"
Cohesion: 0.40
Nodes (3): IAM.Endpoints.Otp.VersionNeutral.SendForLogin, RouteGroupBuilder, Endpoint

### Community 153 - "Host.Swagger"
Cohesion: 0.06
Nodes (26): ApiVersionDescription, Host.Swagger, IConfigureOptions, IOpenApiSchema, IOperationFilter, ISchemaFilter, JsonValue, OpenApiInfo (+18 more)

### Community 154 - ".AddOrUpdate"
Cohesion: 0.40
Nodes (4): Action, Expression, Func, Task

### Community 155 - "VerifyPhoneOtpRequest"
Cohesion: 0.25
Nodes (7): OtpVerificationFailureReason, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, VerifyPhoneOtpResponseExtensions, CancellationToken, Task, VerifyPhoneOtpRequestHandler

### Community 156 - "Common.Application.ModelBinders"
Cohesion: 0.14
Nodes (12): Common.Application.ModelBinders, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, IModelBinder, ModelBindingContext, StronglyTypedIdBinder, Task, Request, RequestValidator (+4 more)

### Community 157 - "SendForRegistration/Endpoint.cs"
Cohesion: 0.40
Nodes (3): IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, RouteGroupBuilder, Endpoint

### Community 158 - ".SendAsync"
Cohesion: 0.40
Nodes (3): CancellationToken, Task, IPushGateway

### Community 159 - ".AddOrUpdate"
Cohesion: 0.40
Nodes (4): Action, Expression, Func, Task

### Community 160 - "ValidationContextExtensions"
Cohesion: 0.40
Nodes (3): ValidationContextExtensions, string, ValidationContext

### Community 161 - "Stores/v1/My/Update/Request.cs"
Cohesion: 0.67
Nodes (3): Products.Endpoints.Stores.v1.My.Update, Request, RequestValidator

### Community 162 - "PermissionAuthorizationHandler"
Cohesion: 0.29
Nodes (6): AuthorizationHandler, AuthorizationHandlerContext, IAuthorizationRequirement, Task, PermissionAuthorizationHandler, PermissionRequirement

### Community 163 - "V1UserRegisteredDomainEvent"
Cohesion: 0.50
Nodes (4): CancellationToken, Task, V1UserRegisteredDomainEventHandler, V1UserRegisteredDomainEvent

### Community 164 - "Common.Application.Options"
Cohesion: 0.11
Nodes (14): Common.Infrastructure.Modules, Host, Common.Infrastructure.Localization, Common.Application.Options, Host.Middlewares, Host.Infrastructure, BackgroundJobsOptions, BackgroundJobsOptionsValidator (+6 more)

### Community 165 - ".GetMeAsync"
Cohesion: 0.10
Nodes (16): FrozenDictionary, IReadOnlySet, CustomPermissions, HashSet, IEnumerable, CurrentUser, Guid, HashSet (+8 more)

### Community 166 - "Endpoint"
Cohesion: 0.29
Nodes (5): IAM.Endpoints.Tokens.VersionNeutral, RouteGroupBuilder, Endpoint, RouteGroupBuilder, Setup

### Community 167 - "PaginationRequest"
Cohesion: 0.06
Nodes (26): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Stores.v1.My.AuditLog, PaginationRequest, DbContextExtensions, CancellationToken, DbSet, JsonSerializerOptions, Task (+18 more)

### Community 168 - "v1/Request.cs"
Cohesion: 0.67
Nodes (3): Products.Endpoints.Probe.v1, Request, RequestValidator

### Community 169 - "Policies.CreateStore.cs"
Cohesion: 0.17
Nodes (8): Products.Infrastructure.RateLimiting, RateLimiterOptions, Policies, Action, IEnumerable, RateLimiterOptions, string, RateLimitingConstants

### Community 171 - "Common.InterModuleRequests"
Cohesion: 0.29
Nodes (4): Common.InterModuleRequests, IAssemblyReference, Setup, IServiceCollection

### Community 172 - "BackgroundJobsTelemetry"
Cohesion: 0.11
Nodes (14): ConcurrentDictionary, BackgroundJobs.Telemetry, IServerFilter, PerformedContext, PerformingContext, JobMetricsFilter, string, BackgroundJobsTelemetry (+6 more)

### Community 173 - "SecurityHeadersOptions.cs"
Cohesion: 0.67
Nodes (3): SecurityHeadersOptions, SecurityHeadersOptionsValidator, Dictionary

### Community 174 - "IAM.Application.Tokens.DTOs"
Cohesion: 0.29
Nodes (5): IAM.Application.Tokens.DTOs, DateTimeOffset, AccessTokenDto, DateTimeOffset, TokensDto

### Community 175 - ".GetMyStoreAuditLogAsync"
Cohesion: 0.11
Nodes (13): AuditLogDto, CancellationToken, RouteGroupBuilder, Task, Endpoint, CancellationToken, RouteGroupBuilder, Task (+5 more)

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

### Community 181 - "Notifications.Infrastructure.Hubs"
Cohesion: 0.20
Nodes (5): Notifications.Infrastructure.Hubs, NotificationGroupName, IConfiguration, IServiceCollection, Setup

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

### Community 187 - "Caching/Setup.cs"
Cohesion: 0.33
Nodes (4): Common.Infrastructure.Caching, Setup, IConfiguration, IServiceCollection

### Community 188 - "IAM.Infrastructure.Auth"
Cohesion: 0.24
Nodes (5): ClaimsPrincipal, IAM.Infrastructure.Auth, ClaimsPrincipalExtensions, string, MultiAuthDefaults

### Community 190 - "Notifications.Infrastructure.Telemetry"
Cohesion: 0.47
Nodes (3): Notifications.Infrastructure.Telemetry, Notifications.Infrastructure.InterModuleRequestHandlers, Common.InterModuleRequests.Notifications

### Community 191 - "Split-Deployment PoC"
Cohesion: 0.25
Nodes (7): Concurrent safety, Cross-process call path, Files added by this PoC, How it works, How to run, Split-Deployment PoC, What this proves

### Community 192 - ".UpdateMyProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 194 - ".UpdateMyStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 195 - "Setup"
Cohesion: 0.40
Nodes (3): IApplicationBuilder, IServiceCollection, Setup

### Community 197 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.19
Nodes (10): Common.InterModuleRequests.IAM, Common.InterModuleRequests.Contracts, IAM.Infrastructure.InterModuleRequestHandlers, IInterModuleRequest, GetPushTokensRequest, GetPushTokensResponse, PushTarget, CancellationToken (+2 more)

### Community 200 - "NotificationsTelemetry"
Cohesion: 0.20
Nodes (6): ActivitySource, Counter, Meter, string, NotificationsTelemetry, UpDownCounter

### Community 203 - ".UpdateProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 211 - ".GetStoreAsync"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response

### Community 213 - "ProductsTelemetry"
Cohesion: 0.33
Nodes (5): ActivitySource, Counter, Meter, string, ProductsTelemetry

### Community 215 - ".DeactivateProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 217 - "PushOptions.cs"
Cohesion: 0.70
Nodes (4): FirebaseServiceAccountOptions, PushOptions, PushOptionsValidator, PushProvider

### Community 219 - "UpdatePushToken/Request.cs"
Cohesion: 0.67
Nodes (3): IAM.Endpoints.Tokens.VersionNeutral.Sessions.UpdatePushToken, Request, RequestValidator

### Community 226 - "ModulesOptions.cs"
Cohesion: 0.67
Nodes (3): ModulesOptions, ModulesOptionsValidator, IReadOnlyList

### Community 228 - "CustomValidator"
Cohesion: 0.16
Nodes (13): DatabaseOptions, DatabaseOptionsValidator, OpenApiOptions, OpenApiOptionsValidator, OtpOptions, OtpOptionsValidator, OutboxCleanupSettings, OutboxCleanupSettingsValidator (+5 more)

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
- **98 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `IAM.Domain.Identity.Sessions`, `Common.Infrastructure.Persistence`, `IAMModule.cs`, `Common.Domain.ResultMonad`, `Common.Infrastructure.Persistence.ValueConverters`, `.SendAsync`, `MassTransitInterModuleRequestClient.cs`, `IAM.Endpoints.Common.Validations`, `BoundedCaptureStream`, `Notifications.Application.Otp`, `Host.Swagger`, `CustomRateLimitingOptions`, `.FixedWindow`, `Policies.CreateStore.cs`, `SecurityHeadersOptions.cs`, `ProductsModule.cs`, `ApiKeysOptions.cs`, `CachingOptions.cs`, `SmsOptions.cs`, `Notifications.Infrastructure.Hubs`, `Notifications.Application.Sms`, `InterModuleRequestOptions.cs`, `Caching/Setup.cs`, `Notifications.Infrastructure.Telemetry`, `OutboxTelemetry`, `OtpServiceBase`, `Common.Application.Validation`, `Common.IntegrationEvents`, `SelfRegister/Request.cs`, `AuditLogRetentionJobRegistrar`, `PushOptions.cs`, `.CreateTokens`, `CaptchaOptions.cs`, `Common.Infrastructure.Extensions`, `FirebasePushGateway.cs`, `.SearchMyProductsAsync`, `ModulesOptions.cs`, `CustomValidator`, `.AddCommonOptions`, `.SendAsync`, `OutboxCleanupJob.cs`, `BackgroundJobsModule.cs`, `IModule`?**
  _High betweenness centrality (0.403) - this node is a cross-community bridge._
- **Why does `Setup` connect `Setup` to `Common.Application.Options`, `BoundedCaptureStream`, `IModule`, `.LogHealthChecksRegistered`, `.AddModules`?**
  _High betweenness centrality (0.088) - this node is a cross-community bridge._
- **Why does `Common.Domain.StronglyTypedIds` connect `Common.Domain.StronglyTypedIds` to `IAM.Domain.Identity.Sessions`, `ApplicationUserId`, `Common.Infrastructure.Persistence`, `Common.Domain.ResultMonad`, `Common.Infrastructure.Persistence.ValueConverters`, `Host.Swagger`, `Common.Application.ModelBinders`, `.HandleAsync`, `OutboxMessage`, `.SendToUserAsync`, `AuditableEntityResponse`, `Notifications.Infrastructure.Hubs`, `Seeder`, `IStronglyTypedId`, `NotificationPayload`, `IAM.Infrastructure.Auth`, `Common.InterModuleRequests.Contracts`, `Common.Application.Validation`, `Common.IntegrationEvents`, `.TryDeserialize`?**
  _High betweenness centrality (0.081) - this node is a cross-community bridge._
- **What connects `OtpCacheEntry`, `Common.Domain`, `Common.Infrastructure` to the rest of the system?**
  _135 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `IAM.Domain.Identity.Sessions` be split into smaller, more focused modules?**
  _Cohesion score 0.135632183908046 - nodes in this community are weakly interconnected._
- **Should `Session` be split into smaller, more focused modules?**
  _Cohesion score 0.1268939393939394 - nodes in this community are weakly interconnected._
- **Should `RedisFixedWindowRateLimiter` be split into smaller, more focused modules?**
  _Cohesion score 0.1067193675889328 - nodes in this community are weakly interconnected._