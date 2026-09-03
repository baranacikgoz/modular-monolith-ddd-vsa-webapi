# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-08-16)

## Corpus Check
- 467 files · ~68,201 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2971 nodes · 5265 edges · 297 communities (188 shown, 109 thin omitted)
- Extraction: 99% EXTRACTED · 1% INFERRED · 0% AMBIGUOUS · INFERRED: 62 edges (avg confidence: 0.8)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `d89a952d`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- Common.Domain.ResultMonad
- NotificationPayload
- OutboxProcessor
- IIAMDbContext
- Hybrid DDD (Writes) / VSA (Reads)
- Session
- RedisFixedWindowRateLimiter
- Common.Infrastructure.Persistence.Auditing
- FirebasePushGateway
- CustomValidator
- Error
- Common.Application.Auth
- .RegisterAndLoginAsync
- .SendOtp
- LocalizedIdentityErrorDescriber
- Common.Infrastructure.Persistence
- .SendAsync
- Common.Application.BackgroundJobs
- DomainEvent
- IStronglyTypedId
- IAM.Endpoints.Common.Validations
- BoundedCaptureStream
- IAM.Infrastructure.Persistence.Seeding
- NotificationsModule.cs
- .RevokeSession
- NetGsmSmsGateway
- IEvent
- RequestResponseBodyLoggingMiddleware
- ProblemDetailsExtensions
- Result
- IAM.Infrastructure/Auth/Setup.cs
- ApplicationUserId
- Endpoint
- v1/AddProduct/Request.cs
- Identity/Setup.cs
- OutboxMessage
- Common.Infrastructure.Extensions
- Seeder
- .RefreshToken
- ObservabilityOptions
- SmsRateLimitingPolicy
- AuditableEntityResponse
- .SaveWithOutboxAsync
- CreateStoreRateLimitingPolicy
- ApplicationUser
- Activate/Request.cs
- Setup
- Outbox Misuse Check
- OtpServiceBase
- Add Integration Event Command
- .SearchUsersAsync
- .SendOtp
- ReCaptchaService
- ProductTemplateId
- Notifications.Infrastructure.Telemetry
- CheckRegistrationRateLimitingPolicy
- .SendAsync
- Cross-Module Reference Violation
- OutboxTelemetry
- .SearchProductTemplatesAsync
- Bogus Test Data
- .AddProductToMyStoreAsync
- RequireFeatureFilter
- RequestLoggingOptions
- Tokens/Setup.cs
- .SavingChangesAsync
- OutboxModule
- .GetProductTemplateAsync
- ValueObject
- DomainEventHandlerBase
- StrictDateTimeOffsetJsonConverter
- Full-Text Search
- ICurrentUser
- .WriteAsync
- AsNoTracking Coverage Check
- ProductId
- Common.Application.Validation
- IProductsDbContext
- TokenCreateRateLimitingPolicy
- Common.Application.EventBus
- .DispatchAsync
- .SearchStoresAsync
- SelfRegister/Request.cs
- .EnsureNoMigrationsPending
- AuditLogEntry
- ResultTelemetryExtensions
- AuditLogRetentionJobRegistrar
- IntegrationEvent
- Configuration-Driven Module Registration
- Setup
- .CreateTokens
- CaptchaOptions.cs
- Common.Domain.StronglyTypedIds
- Endpoint
- FirebasePushGateway.cs
- .SearchMyProductsAsync
- DatabaseSeederOrchestrator
- .PurgeExpiredEntriesAsync
- .SearchStoreProductsAsync
- .AuthorizeAsync
- OutboxDbContext
- .GetMyStoreAuditLogAsync
- Common.Infrastructure.FeatureManagement
- BackgroundJobsTelemetry
- .SendAsync
- Common.Infrastructure/Persistence/Setup.cs
- Common.Infrastructure.Persistence.Outbox
- SeedingCompletionTracker
- StoreId
- .IsRegisteredAsync
- Products.Endpoints.Stores.v1.Create
- IBackgroundJobs
- .ExecuteAsync
- ApplySearchLanguageInterceptor.cs
- OutboxCleanupJob.cs
- BackgroundJobsService
- .TryWriteAsync
- ApiKeyAuthenticationHandler
- IModule
- ProductsModule
- IAM.Infrastructure.Auth.ApiKey
- .GetVariantAsync
- TokenRefreshRateLimitingPolicy
- FullTextSearchOptions
- .LogHealthChecksRegistered
- .TapWhenFeatureEnabledAsync
- GlobalExceptionHandlingMiddleware
- BaseDbContext
- Common.Infrastructure.Persistence.ValueConverters
- .GetProductAsync
- docker-compose.yml (Base Stack)
- .InvokeAsync
- .ActivateProductTemplateAsync
- Consumer Idempotency (IntegrationEventHandlerBase)
- .RemoveProductAsync
- .ListSessions
- .SingleAsResult
- .FixedWindow
- Common.Application.FeatureManagement
- Products.Endpoints.Stores.v1.My.Create
- .MapEndpoint
- Common.Application.Options
- .GetMyStoreAsync
- IDbContext
- .GetAsync
- Products.Endpoints.Probe
- IAM.Endpoints.Otp.VersionNeutral
- .AddOrUpdate
- Setup
- .GetRoleIdByName
- FeatureFlags
- OutboxOptions.cs
- Setup
- Host.Swagger
- .AddOrUpdate
- VerifyPhoneOtpRequest
- Common.Application.ModelBinders
- Stores/v1/AuditLog/Request.cs
- RegisterRateLimitingPolicy
- .Capture
- ValidationContextExtensions
- .UpdateMyStoreAsync
- PermissionAuthorizationHandler
- For
- Revoke/Request.cs
- .GetMeAsync
- Endpoint
- PaginationRequest
- CachedCaptchaService
- Policies.CreateStore.cs
- Notifications.Application/IAssemblyReference.cs
- .UpdateStoreAsync
- ResxLocalizationOptions.cs
- PushOptions.cs
- IAM.Application.Tokens.DTOs
- .GetProductAuditLogAsync
- ApiKeysOptions.cs
- PermissionPolicyProvider
- OtpOptions.cs
- CachingOptions.cs
- SmsOptions.cs
- RabbitMqOptions.cs
- Constants
- SendRequestBody
- SignalROptions.cs
- IamModule
- IEventHandler.cs
- ReverseProxyOptions.cs
- IAM.Infrastructure.Auth
- OtpPurposes.cs
- Split-Deployment PoC
- VersionNeutral/Get/Request.cs
- .SaveChangesAsync
- CorsOptions.cs
- My/Create/Request.cs
- JwtOptions
- IntegrationEventHandlerBase
- Stores/v1/My/Get/Response.cs
- NotificationsTelemetry
- .UpdateProductAsync
- Configuration-Driven Module Loading
- Infrastructure/StringExtensions.cs
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- ProductsTelemetry
- .DeactivateProductTemplateAsync
- .UpdateCurrentSessionPushToken
- JwtClaimNames.cs
- DatabaseOptions.cs
- .AddCommonOptions
- ActionsAndResources.cs
- CustomRoles
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
1. `Common.Application.Options` - 98 edges
2. `Result` - 88 edges
3. `Common.Domain.ResultMonad` - 72 edges
4. `Common.Domain.StronglyTypedIds` - 68 edges
5. `CustomValidator` - 62 edges
6. `Common.Application.Auth` - 60 edges
7. `Common.Application.Validation` - 56 edges
8. `Common.Application.Extensions` - 51 edges
9. `ApplicationUserId` - 50 edges
10. `Setup` - 50 edges

## Surprising Connections (you probably didn't know these)
- `Aspire Dashboard Service (mm.aspire-dashboard)` --conceptually_related_to--> `Observability (OpenTelemetry)`  [INFERRED]
  docker-compose.yml → CLAUDE.md
- `CaptchaErrors` --references--> `Error`  [EXTRACTED]
  src/Modules/IAM/IAM.Domain/Captcha/CaptchaErrors.cs → src/Common/Common.Domain/ResultMonad/Error.cs
- `PushErrors` --references--> `Error`  [EXTRACTED]
  src/Modules/Notifications/Notifications.Application/Push/PushErrors.cs → src/Common/Common.Domain/ResultMonad/Error.cs
- `SmsErrors` --references--> `Error`  [EXTRACTED]
  src/Modules/Notifications/Notifications.Application/Sms/SmsErrors.cs → src/Common/Common.Domain/ResultMonad/Error.cs
- `RequestBody` --references--> `ProductTemplateId`  [EXTRACTED]
  src/Modules/Products/Products.Endpoints/Stores/v1/AddProduct/Request.cs → src/Modules/Products/Products.Domain/ProductTemplates/ProductTemplate.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Local Infrastructure Stack** — docker_compose_postgres, docker_compose_rabbitmq, docker_compose_redis, docker_compose_aspire_dashboard [EXTRACTED 1.00]

## Communities (297 total, 109 thin omitted)

### Community 0 - "Common.Domain.ResultMonad"
Cohesion: 0.11
Nodes (15): IAM.Application.Tokens.Services, IAM.Application.Extensions, IAM.Endpoints.Otp, Common.InterModuleRequests.Contracts, IAM.Domain.Identity, IAM.Endpoints.Tokens.VersionNeutral.Revoke, IAM.Infrastructure.Telemetry, Common.Domain.ResultMonad (+7 more)

### Community 1 - "NotificationPayload"
Cohesion: 0.06
Nodes (29): Notifications.Infrastructure.Hubs, Notifications.Application.Hubs, Hub, accessToken, DateTimeOffset, expiresAt, ICollection, refreshTokenBytes (+21 more)

### Community 2 - "OutboxProcessor"
Cohesion: 0.30
Nodes (7): CancellationToken, Exception, ILogger, LoggerMessage, Task, TimeSpan, OutboxProcessor

### Community 3 - "IIAMDbContext"
Cohesion: 0.10
Nodes (19): IdentityDbContext, DbSet, IdentityRole, IdentityRoleClaim, IdentityUserClaim, IdentityUserLogin, IdentityUserRole, IdentityUserToken (+11 more)

### Community 5 - "Session"
Cohesion: 0.15
Nodes (13): DateTimeOffset, Guid, DateTimeOffset, RefreshToken, RefreshTokenId, DateTimeOffset, Guid, IReadOnlyCollection (+5 more)

### Community 6 - "RedisFixedWindowRateLimiter"
Cohesion: 0.11
Nodes (17): IConnectionMultiplexer, RateLimiter, RateLimiterStatistics, RateLimitLease, FixedWindowLease, RedisFixedWindowRateLimiter, bool, CancellationToken (+9 more)

### Community 7 - "Common.Infrastructure.Persistence.Auditing"
Cohesion: 0.22
Nodes (5): Common.Infrastructure.Persistence.Auditing, Setup, IServiceCollection, Setup, IServiceCollection

### Community 8 - "FirebasePushGateway"
Cohesion: 0.13
Nodes (16): FirebaseApp, FirebaseMessaging, CancellationToken, Task, IPushGateway, PushMessage, CancellationToken, Exception (+8 more)

### Community 9 - "CustomValidator"
Cohesion: 0.13
Nodes (20): Products.Endpoints.Stores.v1.Update, Products.Endpoints.Products.v1.My.Update, Products.Endpoints.Products.v1.Update, CustomValidator, RequestBody, Request, RequestBody, RequestBodyValidator (+12 more)

### Community 10 - "Error"
Cohesion: 0.11
Nodes (12): HttpStatusCode, IdentityResult, IStringLocalizer, StringLocalizerExtensions, Error, ICollection, IResult, IdentityResultExtensions (+4 more)

### Community 11 - "Common.Application.Auth"
Cohesion: 0.19
Nodes (9): Common.Application.Search, Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Products.Infrastructure.Telemetry, Products.Application.Persistence, Products.Domain.Stores, Common.Application.Pagination (+1 more)

### Community 12 - ".RegisterAndLoginAsync"
Cohesion: 0.17
Nodes (16): accessToken, DateTimeOffset, expiresAt, ICollection, refreshTokenBytes, ITokenService, CancellationToken, HttpContext (+8 more)

### Community 13 - ".SendOtp"
Cohesion: 0.18
Nodes (10): SendPhoneOtpRequest, SendPhoneOtpResponse, CancellationToken, IFeatureManager, RouteGroupBuilder, Task, Endpoint, CancellationToken (+2 more)

### Community 14 - "LocalizedIdentityErrorDescriber"
Cohesion: 0.13
Nodes (4): IAM.Infrastructure.Identity, IdentityError, IdentityErrorDescriber, LocalizedIdentityErrorDescriber

### Community 15 - "Common.Infrastructure.Persistence"
Cohesion: 0.08
Nodes (15): Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Common.Application.Persistence, Common.Infrastructure.EventBus, Common.Infrastructure.Persistence.DbContext, Setup, IServiceCollection, AutoMigrateMarker (+7 more)

### Community 16 - ".SendAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, Task, TimeSpan, ThrottledSmsGateway

### Community 17 - "Common.Application.BackgroundJobs"
Cohesion: 0.24
Nodes (5): Common.Application.BackgroundJobs, BackgroundJobs, RecurringJobOptions, IRecurringBackgroundJobs, RecurringBackgroundJobsService

### Community 18 - "DomainEvent"
Cohesion: 0.12
Nodes (19): AggregateRoot, IEnumerable, IReadOnlyCollection, List, DomainEvent, DateTimeOffset, DefaultIdType, V1ProductCreatedDomainEvent (+11 more)

### Community 19 - "IStronglyTypedId"
Cohesion: 0.17
Nodes (9): IAggregateRoot, IEnumerable, IReadOnlyCollection, AuditableEntity, DateTimeOffset, IAuditableEntity, DateTimeOffset, IStronglyTypedId (+1 more)

### Community 20 - "IAM.Endpoints.Common.Validations"
Cohesion: 0.10
Nodes (17): IAM.Endpoints.Otp.VersionNeutral.SendForLogin, IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, IAM.Endpoints.Common.Validations, IResxLocalizer, IRuleBuilder, IRuleBuilderOptions, CommonValidations, IResxLocalizer (+9 more)

### Community 21 - "BoundedCaptureStream"
Cohesion: 0.14
Nodes (7): byte, SeekOrigin, bool, int, BoundedCaptureStream, BoundedRequestCaptureStream, Stream

### Community 22 - "IAM.Infrastructure.Persistence.Seeding"
Cohesion: 0.11
Nodes (10): IAM.Infrastructure.Persistence.Seeding, IDatabaseSeeder, CancellationToken, Task, CancellationToken, Task, IamDatabaseSeeder, CancellationToken (+2 more)

### Community 23 - "NotificationsModule.cs"
Cohesion: 0.09
Nodes (14): Notifications.Application.Otp, Notifications.Infrastructure.Otp, Notifications.Infrastructure, IAssemblyReference, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable (+6 more)

### Community 24 - ".RevokeSession"
Cohesion: 0.07
Nodes (20): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint, CancellationToken, RouteGroupBuilder, Task (+12 more)

### Community 25 - "NetGsmSmsGateway"
Cohesion: 0.16
Nodes (11): SendRequestBody, CancellationToken, Exception, ILogger, JsonSerializerOptions, LoggerMessage, string, Task (+3 more)

### Community 26 - "IEvent"
Cohesion: 0.17
Nodes (9): CancellationToken, Task, CancellationToken, Task, IOutboxMessage, DateTimeOffset, IEvent, DateTimeOffset (+1 more)

### Community 27 - "RequestResponseBodyLoggingMiddleware"
Cohesion: 0.20
Nodes (7): IApplicationBuilder, HttpContext, IList, PathString, RequestDelegate, string, RequestResponseBodyLoggingMiddleware

### Community 28 - "ProblemDetailsExtensions"
Cohesion: 0.40
Nodes (3): ProblemDetails, ProblemDetailsExtensions, ICollection

### Community 29 - "Result"
Cohesion: 0.16
Nodes (10): Result, Func, Task, AsyncExtensions, SyncExtensions, Action, Func, Task (+2 more)

### Community 30 - "IAM.Infrastructure/Auth/Setup.cs"
Cohesion: 0.22
Nodes (6): IAM.Infrastructure.Auth.Jwt, IAM.Infrastructure.Auth.Services, IAM.Application.Auth.Services, IConfiguration, IServiceCollection, Setup

### Community 31 - "ApplicationUserId"
Cohesion: 0.16
Nodes (17): IEntityTypeConfiguration, ApplicationUserId, DefaultIdType, EntityTypeBuilder, IdentityRole, IdentityRoleClaim, IdentityUserClaim, IdentityUserLogin (+9 more)

### Community 32 - "Endpoint"
Cohesion: 0.29
Nodes (5): Products.Endpoints.Stores, RouteGroupBuilder, Setup, RouteGroupBuilder, Endpoint

### Community 33 - "v1/AddProduct/Request.cs"
Cohesion: 0.11
Nodes (13): Products.Endpoints.Stores.v1.AddProduct, decimal, int, Constants, CancellationToken, RouteGroupBuilder, Task, Endpoint (+5 more)

### Community 34 - "Identity/Setup.cs"
Cohesion: 0.40
Nodes (3): IAM.Infrastructure.Persistence, IServiceCollection, Setup

### Community 35 - "OutboxMessage"
Cohesion: 0.27
Nodes (5): OutboxMessage, DateTimeOffset, TimeSpan, EntityTypeBuilder, OutboxMessageConfig

### Community 36 - "Common.Infrastructure.Extensions"
Cohesion: 0.12
Nodes (14): Common.Infrastructure.RateLimiting, Common.Infrastructure.Extensions, IAM.Infrastructure.RateLimiting, CustomRateLimitingOptions, CustomRateLimitingOptionsValidator, FixedWindow, FixedWindowValidator, IReadOnlyList (+6 more)

### Community 37 - "Seeder"
Cohesion: 0.15
Nodes (11): Task, IdentityRole, ILogger, LoggerMessage, Task, Seeder, Action, DateOnly (+3 more)

### Community 38 - ".RefreshToken"
Cohesion: 0.12
Nodes (14): IAM.Endpoints.Tokens.VersionNeutral.Refresh, CancellationToken, HttpContext, ILogger, IOptions, LoggerMessage, RouteGroupBuilder, Task (+6 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.11
Nodes (15): KeyValuePair, LoggerConfiguration, LoggerMinimumLevelConfiguration, OpenTelemetryBuilder, ResourceBuilder, ObservabilityOptions, ObservabilityOptionsValidator, Dictionary (+7 more)

### Community 40 - "SmsRateLimitingPolicy"
Cohesion: 0.25
Nodes (7): CancellationToken, Func, HttpContext, OnRejectedContext, RateLimitPartition, ValueTask, SmsRateLimitingPolicy

### Community 41 - "AuditableEntityResponse"
Cohesion: 0.11
Nodes (17): Products.Endpoints.Products.v1.My.Get, Products.Endpoints.Products.v1.Search, Common.Application.DTOs, Products.Endpoints.Stores.v1.Get, IAM.Endpoints.Users.VersionNeutral.Me.Get, Products.Endpoints.ProductTemplates.v1.Get, Products.Endpoints.Products.v1.Get, AuditableEntityResponse (+9 more)

### Community 42 - ".SaveWithOutboxAsync"
Cohesion: 0.27
Nodes (9): OutboxSaveHelper, CancellationToken, DbContext, Exception, Func, ILogger, LoggerMessage, Task (+1 more)

### Community 43 - "CreateStoreRateLimitingPolicy"
Cohesion: 0.20
Nodes (8): CancellationToken, Func, HttpContext, OnRejectedContext, RateLimitPartition, TimeSpan, ValueTask, CreateStoreRateLimitingPolicy

### Community 44 - "ApplicationUser"
Cohesion: 0.09
Nodes (16): IdentityUser, ISearchLocalized, IEnumerable, IReadOnlyCollection, List, ApplicationUser, DateTimeOffset, DateOnly (+8 more)

### Community 45 - "Activate/Request.cs"
Cohesion: 0.67
Nodes (3): Products.Endpoints.ProductTemplates.v1.Activate, Request, RequestValidator

### Community 46 - "Setup"
Cohesion: 0.29
Nodes (4): ApiVersionSet, Setup, IEndpointRouteBuilder, IServiceCollection

### Community 48 - "OtpServiceBase"
Cohesion: 0.08
Nodes (19): SemaphoreSlim, CancellationToken, Task, TimeSpan, IOtpService, OtpVerificationOutcome, string, DummyOtpService (+11 more)

### Community 50 - ".SearchUsersAsync"
Cohesion: 0.13
Nodes (12): IAM.Endpoints.Users.VersionNeutral.Search, int, Constants, CancellationToken, IOptions, RouteGroupBuilder, Task, Endpoint (+4 more)

### Community 51 - ".SendOtp"
Cohesion: 0.12
Nodes (11): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, CancellationToken, Task, ICaptchaService, Response, CancellationToken, IFeatureManager, RouteGroupBuilder (+3 more)

### Community 52 - "ReCaptchaService"
Cohesion: 0.17
Nodes (11): DateTime, double, FormUrlEncodedContent, ReCaptchaResponse, CancellationToken, Exception, ILogger, LoggerMessage (+3 more)

### Community 53 - "ProductTemplateId"
Cohesion: 0.05
Nodes (34): Products.Infrastructure.Persistence.Seeding, Common.InterModuleRequests.IAM, Products.Endpoints.ProductTemplates, Products.Endpoints.ProductTemplates.v1.Create, IReadOnlyList, List, ProductTemplate, ProductTemplateId (+26 more)

### Community 54 - "Notifications.Infrastructure.Telemetry"
Cohesion: 0.12
Nodes (13): Notifications.Application.Sms, Notifications.Infrastructure.Sms, Notifications.Infrastructure.Telemetry, Notifications.Infrastructure.InterModuleRequestHandlers, Common.Infrastructure.Resiliency, Notifications.Infrastructure.Sms.NetGsm, HttpClient, HttpStandardResilienceOptions (+5 more)

### Community 55 - "CheckRegistrationRateLimitingPolicy"
Cohesion: 0.25
Nodes (7): CancellationToken, Func, HttpContext, OnRejectedContext, RateLimitPartition, ValueTask, CheckRegistrationRateLimitingPolicy

### Community 56 - ".SendAsync"
Cohesion: 0.16
Nodes (10): CancellationToken, Task, ISmsGateway, SmsCategory, SmsMessage, CancellationToken, ILogger, LoggerMessage (+2 more)

### Community 58 - "OutboxTelemetry"
Cohesion: 0.13
Nodes (13): CancellationToken, ILogger, LoggerMessage, Task, OutboxMetricsJob, ActivitySource, Counter, Histogram (+5 more)

### Community 59 - ".SearchProductTemplatesAsync"
Cohesion: 0.14
Nodes (11): Products.Endpoints.ProductTemplates.v1.Search, int, Constants, CancellationToken, IOptions, RouteGroupBuilder, Task, Endpoint (+3 more)

### Community 61 - ".AddProductToMyStoreAsync"
Cohesion: 0.20
Nodes (8): Products.Endpoints.Stores.v1.My.AddProduct, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator, Response

### Community 62 - "RequireFeatureFilter"
Cohesion: 0.11
Nodes (16): Common.Application.EndpointFilters, IEndpointFilter, ResultToCreatedResponseTransformer, ResultToResponseTransformer, EndpointFilterDelegate, EndpointFilterInvocationContext, ValueTask, RouteHandlerBuilderExtensions (+8 more)

### Community 63 - "RequestLoggingOptions"
Cohesion: 0.33
Nodes (7): IPostConfigureOptions, RequestLoggingOptions, RequestLoggingOptionsValidator, SensitivePathRule, IList, int, RequestLoggingPathPostConfigure

### Community 64 - "Tokens/Setup.cs"
Cohesion: 0.14
Nodes (8): Common.InterModuleRequests, IAM.Infrastructure.Tokens, IAM.Infrastructure.Tokens.Services, IAssemblyReference, Setup, IServiceCollection, IServiceCollection, Setup

### Community 65 - ".SavingChangesAsync"
Cohesion: 0.29
Nodes (6): SaveChangesInterceptor, ApplyAuditingInterceptor, CancellationToken, DbContextEventData, InterceptionResult, ValueTask

### Community 66 - "OutboxModule"
Cohesion: 0.19
Nodes (10): Action, Exception, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, ILogger, IServiceCollection (+2 more)

### Community 67 - ".GetProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 68 - "ValueObject"
Cohesion: 0.25
Nodes (4): Common.Domain, IComparable, ValueObject, IEnumerable

### Community 69 - "DomainEventHandlerBase"
Cohesion: 0.08
Nodes (27): DomainEventHandlerBase, CancellationToken, Task, V1AllSessionsRevokedCacheInvalidationHandler, CancellationToken, Task, V1SessionRefreshedCacheInvalidationHandler, CancellationToken (+19 more)

### Community 70 - "StrictDateTimeOffsetJsonConverter"
Cohesion: 0.06
Nodes (29): JsonConverter, StrictDateTimeOffsetJsonConverter, DateTimeOffset, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, StronglyTypedIdListReadOnlyJsonConverter (+21 more)

### Community 71 - "Full-Text Search"
Cohesion: 0.08
Nodes (25): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Add a new language/culture, Add search to a new entity _(Build checklist)_ (+17 more)

### Community 72 - "ICurrentUser"
Cohesion: 0.09
Nodes (15): ICurrentUser, Guid, ICollection, CancellationToken, RouteGroupBuilder, Task, Endpoint, CancellationToken (+7 more)

### Community 73 - ".WriteAsync"
Cohesion: 0.33
Nodes (5): Memory, ReadOnlyMemory, CancellationToken, Task, ValueTask

### Community 75 - "ProductId"
Cohesion: 0.16
Nodes (11): Products.Endpoints.Stores.v1.My.RemoveProduct, Products.Endpoints.Stores.v1.RemoveProduct, ProductId, Request, RequestValidator, Request, RequestValidator, Request (+3 more)

### Community 76 - "Common.Application.Validation"
Cohesion: 0.10
Nodes (17): Common.Application.Validation, AuditLogOptions, AuditLogOptionsValidator, InterModuleRequestOptions, InterModuleRequestOptionsValidator, ModulesOptions, ModulesOptionsValidator, IReadOnlyList (+9 more)

### Community 77 - "IProductsDbContext"
Cohesion: 0.10
Nodes (15): DbSet, IProductsDbContext, CancellationToken, Task, CancellationToken, RouteGroupBuilder, Task, Endpoint (+7 more)

### Community 78 - "TokenCreateRateLimitingPolicy"
Cohesion: 0.22
Nodes (8): IRateLimiterPolicy, CancellationToken, Func, HttpContext, OnRejectedContext, RateLimitPartition, ValueTask, TokenCreateRateLimitingPolicy

### Community 79 - "Common.Application.EventBus"
Cohesion: 0.11
Nodes (13): Common.IntegrationEvents, Common.Application.Caching, Notifications.Application.IntegrationEventHandlers, IAM.Application.Users.DomainEventHandlers.v1, Products.Application.Stores.DomainEventHandlers.v1, Common.Application.EventBus, Lock, OtpCacheEntry (+5 more)

### Community 80 - ".DispatchAsync"
Cohesion: 0.33
Nodes (6): EventDispatcher, ActivitySource, CancellationToken, ILogger, LoggerMessage, Task

### Community 81 - ".SearchStoresAsync"
Cohesion: 0.18
Nodes (9): Products.Endpoints.Stores.v1.Search, CancellationToken, IOptions, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator (+1 more)

### Community 82 - "SelfRegister/Request.cs"
Cohesion: 0.18
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegister, Common.Domain.Extensions, SearchValues, StringExtensions, Guid, Request, RequestValidator

### Community 83 - ".EnsureNoMigrationsPending"
Cohesion: 0.44
Nodes (4): IServiceProvider, MigrationGuard, ILogger, LoggerMessage

### Community 84 - "AuditLogEntry"
Cohesion: 0.15
Nodes (12): AuditLogEntry, DefaultIdType, AuditableEntityConfiguration, EntityTypeBuilder, AuditLogEntryConfiguration, EntityTypeBuilder, EntityTypeBuilder, SessionConfig (+4 more)

### Community 85 - "ResultTelemetryExtensions"
Cohesion: 0.32
Nodes (4): Activity, ResultTelemetryExtensions, ActivitySource, Task

### Community 86 - "AuditLogRetentionJobRegistrar"
Cohesion: 0.29
Nodes (7): IHostedService, AuditLogRetentionJobRegistrar, CancellationToken, ILogger, LoggerMessage, string, Task

### Community 87 - "IntegrationEvent"
Cohesion: 0.10
Nodes (18): SessionTokenReuseDetectedIntegrationEvent, UserRegisteredIntegrationEvent, IntegrationEvent, DateTimeOffset, DefaultIdType, ProductCreatedIntegrationEvent, StoreCreatedIntegrationEvent, CancellationToken (+10 more)

### Community 89 - "Setup"
Cohesion: 0.33
Nodes (4): ConfigurationManager, Host.Configurations, Setup, WebApplicationBuilder

### Community 90 - ".CreateTokens"
Cohesion: 0.11
Nodes (14): IAM.Endpoints.Tokens.VersionNeutral.Create, IInterModuleRequestClient, CancellationToken, Task, MassTransitInterModuleRequestClient, CancellationToken, Task, CancellationToken (+6 more)

### Community 91 - "CaptchaOptions.cs"
Cohesion: 0.31
Nodes (6): CaptchaOptions, CaptchaOptionsValidator, CaptchaProvider, IConfiguration, IServiceCollection, Setup

### Community 92 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.10
Nodes (10): Products.Domain.Products.DomainEvents.v1, Common.Domain.StronglyTypedIds, IAM.Domain.Identity.DomainEvents.v1, Common.Domain.Events, Products.Domain.Products, IAM.Domain.Identity.Sessions, Common.Domain.Entities, Common.Domain.Aggregates (+2 more)

### Community 93 - "Endpoint"
Cohesion: 0.29
Nodes (5): IAM.Endpoints.Captcha.VersionNeutral, RouteGroupBuilder, Endpoint, RouteGroupBuilder, Setup

### Community 94 - "FirebasePushGateway.cs"
Cohesion: 0.18
Nodes (8): Notifications.Application.Push, Notifications.Infrastructure.Push, Notifications.Infrastructure.Push.Firebase, PushErrors, DummyPushGateway, IConfiguration, IServiceCollection, Setup

### Community 95 - ".SearchMyProductsAsync"
Cohesion: 0.18
Nodes (9): Products.Endpoints.Products.v1.My.Search, CancellationToken, IOptions, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator (+1 more)

### Community 96 - "DatabaseSeederOrchestrator"
Cohesion: 0.36
Nodes (6): BackgroundService, DatabaseSeederOrchestrator, CancellationToken, ILogger, LoggerMessage, Task

### Community 97 - ".PurgeExpiredEntriesAsync"
Cohesion: 0.31
Nodes (6): AuditLogRetentionService, CancellationToken, DateTimeOffset, ILogger, LoggerMessage, Task

### Community 98 - ".SearchStoreProductsAsync"
Cohesion: 0.18
Nodes (8): ISearchLanguageResolver, SearchLanguageResolver, string, CancellationToken, IOptions, RouteGroupBuilder, Task, Endpoint

### Community 99 - ".AuthorizeAsync"
Cohesion: 0.17
Nodes (7): DashboardContext, IDashboardAsyncAuthorizationFilter, CustomPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder, HangfireCustomAuthorizationFilter, Task

### Community 100 - "OutboxDbContext"
Cohesion: 0.18
Nodes (8): IOutboxDbContext, CancellationToken, DbSet, Task, DbSet, ModelBuilder, ModelConfigurationBuilder, OutboxDbContext

### Community 101 - ".GetMyStoreAuditLogAsync"
Cohesion: 0.22
Nodes (7): Products.Endpoints.Stores.v1.My.AuditLog, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator

### Community 102 - "Common.Infrastructure.FeatureManagement"
Cohesion: 0.17
Nodes (8): Common.Infrastructure.FeatureManagement, ITargetingContextAccessor, HttpContextTargetingContextAccessor, ValueTask, Setup, IConfiguration, IServiceCollection, TargetingContext

### Community 103 - "BackgroundJobsTelemetry"
Cohesion: 0.11
Nodes (14): ConcurrentDictionary, BackgroundJobs.Telemetry, IServerFilter, PerformedContext, PerformingContext, JobMetricsFilter, string, BackgroundJobsTelemetry (+6 more)

### Community 104 - ".SendAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, ILogger, LoggerMessage, Task

### Community 105 - "Common.Infrastructure/Persistence/Setup.cs"
Cohesion: 0.20
Nodes (5): Common.Infrastructure.Persistence.AuditLog, Setup, IServiceCollection, Setup, IServiceCollection

### Community 106 - "Common.Infrastructure.Persistence.Outbox"
Cohesion: 0.32
Nodes (3): Common.Infrastructure.Persistence.Outbox, Outbox.Persistence, Common.Application.Persistence.Outbox

### Community 107 - "SeedingCompletionTracker"
Cohesion: 0.22
Nodes (5): SeedingCompletionTracker, CancellationToken, Exception, Task, TaskCompletionSource

### Community 108 - "StoreId"
Cohesion: 0.12
Nodes (5): StronglyTypedIdHelper, AuthenticationBuilder, IConfiguration, Setup, StoreId

### Community 109 - ".IsRegisteredAsync"
Cohesion: 0.20
Nodes (8): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator, Response

### Community 111 - "IBackgroundJobs"
Cohesion: 0.26
Nodes (7): IBackgroundJobs, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 112 - ".ExecuteAsync"
Cohesion: 0.39
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, OutboxCleanupJob

### Community 113 - "ApplySearchLanguageInterceptor.cs"
Cohesion: 0.29
Nodes (5): ApplySearchLanguageInterceptor, CancellationToken, DbContextEventData, InterceptionResult, ValueTask

### Community 115 - "BackgroundJobsService"
Cohesion: 0.26
Nodes (7): BackgroundJobsService, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 116 - ".TryWriteAsync"
Cohesion: 0.33
Nodes (4): IProblemDetailsService, ProblemDetailsContext, ProblemDetailsServiceExtensions, Task

### Community 117 - "ApiKeyAuthenticationHandler"
Cohesion: 0.23
Nodes (8): AuthenticateResult, AuthenticationHandler, AuthenticationProperties, AuthenticationSchemeOptions, ILogger, LoggerMessage, Task, ApiKeyAuthenticationHandler

### Community 118 - "IModule"
Cohesion: 0.10
Nodes (15): ICoreModule, IModule, Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection (+7 more)

### Community 119 - "ProductsModule"
Cohesion: 0.18
Nodes (8): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, ProductsModule

### Community 120 - "IAM.Infrastructure.Auth.ApiKey"
Cohesion: 0.17
Nodes (6): IAM.Infrastructure.Auth.ApiKey, string, ApiKeyDefaults, ApiKeyHasher, AuthenticationBuilder, Setup

### Community 121 - ".GetVariantAsync"
Cohesion: 0.33
Nodes (4): IVariantFeatureManager, IVariantFeatureManagerExtensions, CancellationToken, Task

### Community 122 - "TokenRefreshRateLimitingPolicy"
Cohesion: 0.25
Nodes (7): CancellationToken, Func, HttpContext, OnRejectedContext, RateLimitPartition, ValueTask, TokenRefreshRateLimitingPolicy

### Community 123 - "FullTextSearchOptions"
Cohesion: 0.40
Nodes (5): FullTextSearchOptions, FullTextSearchOptionsValidator, Dictionary, IReadOnlyList, string

### Community 124 - ".LogHealthChecksRegistered"
Cohesion: 0.33
Nodes (4): IApplicationBuilder, ILogger, LoggerMessage, WebApplication

### Community 125 - ".TapWhenFeatureEnabledAsync"
Cohesion: 0.33
Nodes (5): FeatureFlagResultExtensions, Action, Func, IFeatureManager, Task

### Community 126 - "GlobalExceptionHandlingMiddleware"
Cohesion: 0.29
Nodes (7): Exception, HttpContext, ILogger, LoggerMessage, RequestDelegate, Task, GlobalExceptionHandlingMiddleware

### Community 127 - "BaseDbContext"
Cohesion: 0.25
Nodes (6): DbContext, BaseDbContext, CancellationToken, DbSet, ModelConfigurationBuilder, Task

### Community 128 - "Common.Infrastructure.Persistence.ValueConverters"
Cohesion: 0.08
Nodes (18): Common.Application.JsonConverters, IAM.Infrastructure.Persistence.EntityConfigurations, Common.Infrastructure.Persistence.EntityConfigurations, Common.Infrastructure.Persistence.ValueConverters, Products.Infrastructure.Persistence.EntityConfigurations, DomainEventConverter, JsonSerializerOptions, EventConverter (+10 more)

### Community 129 - ".GetProductAsync"
Cohesion: 0.20
Nodes (7): Products.Endpoints.Products, RouteGroupBuilder, Setup, CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.48
Nodes (7): Observability (OpenTelemetry), docker-compose.yml (Base Stack), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), Postgres Service (mm.postgres), RabbitMQ Service (mm.rabbitmq), Redis Service (mm.redis)

### Community 131 - ".InvokeAsync"
Cohesion: 0.29
Nodes (5): IMiddleware, HttpContext, RequestDelegate, Task, EnrichLogsWithUserInfoMiddleware

### Community 132 - ".ActivateProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 134 - ".RemoveProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 135 - ".ListSessions"
Cohesion: 0.17
Nodes (9): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, CancellationToken, IReadOnlyCollection, RouteGroupBuilder, Task, Endpoint, DateTimeOffset, Guid (+1 more)

### Community 136 - ".SingleAsResult"
Cohesion: 0.18
Nodes (10): CollectionExtensions, Func, ICollection, IEnumerable, PersistenceQueryableExtensions, CancellationToken, Expression, Func (+2 more)

### Community 137 - ".FixedWindow"
Cohesion: 0.12
Nodes (14): PartitionedRateLimiter, RateLimitPartitions, HttpContext, RateLimitPartition, CancellationToken, Func, HttpContext, IConfiguration (+6 more)

### Community 138 - "Common.Application.FeatureManagement"
Cohesion: 0.14
Nodes (8): Common.Application.FeatureManagement, IAM.Domain.Captcha, IAM.Infrastructure.Captcha.Services, IAM.Application.Captcha.Services, IAM.Infrastructure.Captcha, RouteHandlerBuilderExtensions, RouteHandlerBuilder, CaptchaErrors

### Community 140 - ".MapEndpoint"
Cohesion: 0.29
Nodes (4): IAM.Endpoints.Users.VersionNeutral, RouteGroupBuilder, RouteGroupBuilder, Setup

### Community 141 - "Common.Application.Options"
Cohesion: 0.06
Nodes (23): Common.Infrastructure.Modules, Common.Endpoints.Versioning, Host, Common.Infrastructure.Localization, IAM.Endpoints, Common.Application.Options, Host.Middlewares, Products.Endpoints (+15 more)

### Community 142 - ".GetMyStoreAsync"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response

### Community 143 - "IDbContext"
Cohesion: 0.20
Nodes (8): ChangeTracker, DatabaseFacade, EntityEntry, IDisposable, IDbContext, CancellationToken, DbSet, Task

### Community 144 - ".GetAsync"
Cohesion: 0.20
Nodes (7): IAM.Endpoints.Users.VersionNeutral.Get, CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, Response

### Community 145 - "Products.Endpoints.Probe"
Cohesion: 0.40
Nodes (3): Products.Endpoints.Probe, RouteGroupBuilder, Setup

### Community 146 - "IAM.Endpoints.Otp.VersionNeutral"
Cohesion: 0.40
Nodes (3): IAM.Endpoints.Otp.VersionNeutral, RouteGroupBuilder, Setup

### Community 147 - ".AddOrUpdate"
Cohesion: 0.40
Nodes (4): Action, Expression, Func, Task

### Community 148 - "Setup"
Cohesion: 0.05
Nodes (32): IHostBuilder, LoadAll, Names, Assembly, IApplicationBuilder, IConfiguration, IServiceCollection, IWebHostEnvironment (+24 more)

### Community 149 - ".GetRoleIdByName"
Cohesion: 0.20
Nodes (8): CancellationToken, DefaultIdType, Task, IRoleService, CancellationToken, DefaultIdType, Task, RoleService

### Community 150 - "FeatureFlags"
Cohesion: 0.43
Nodes (6): Checkout, FeatureFlags, IAM, Notifications, Products, string

### Community 151 - "OutboxOptions.cs"
Cohesion: 0.70
Nodes (4): OutboxCleanupSettings, OutboxCleanupSettingsValidator, OutboxOptions, OutboxOptionsValidator

### Community 152 - "Setup"
Cohesion: 0.40
Nodes (3): Setup, IApplicationBuilder, IServiceCollection

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
Cohesion: 0.09
Nodes (19): Products.Endpoints.ProductTemplates.v1.Deactivate, Common.Application.ModelBinders, Products.Endpoints.Products.v1.AuditLog, IModelBinder, ModelBindingContext, StronglyTypedIdBinder, Task, PaginationRequestValidator (+11 more)

### Community 157 - "Stores/v1/AuditLog/Request.cs"
Cohesion: 0.67
Nodes (3): Products.Endpoints.Stores.v1.AuditLog, Request, RequestValidator

### Community 158 - "RegisterRateLimitingPolicy"
Cohesion: 0.25
Nodes (7): CancellationToken, Func, HttpContext, OnRejectedContext, RateLimitPartition, ValueTask, RegisterRateLimitingPolicy

### Community 160 - "ValidationContextExtensions"
Cohesion: 0.40
Nodes (3): ValidationContextExtensions, string, ValidationContext

### Community 161 - ".UpdateMyStoreAsync"
Cohesion: 0.22
Nodes (7): Products.Endpoints.Stores.v1.My.Update, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator

### Community 162 - "PermissionAuthorizationHandler"
Cohesion: 0.29
Nodes (6): AuthorizationHandler, AuthorizationHandlerContext, IAuthorizationRequirement, Task, PermissionAuthorizationHandler, PermissionRequirement

### Community 163 - "For"
Cohesion: 0.33
Nodes (3): CacheKeys, For, Guid

### Community 164 - "Revoke/Request.cs"
Cohesion: 0.67
Nodes (3): IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, Request, RequestValidator

### Community 165 - ".GetMeAsync"
Cohesion: 0.08
Nodes (17): Common.Infrastructure.Auth.Services, Common.Infrastructure.Auth, FrozenDictionary, IReadOnlySet, CustomPermissions, HashSet, IEnumerable, CurrentUser (+9 more)

### Community 166 - "Endpoint"
Cohesion: 0.29
Nodes (5): IAM.Endpoints.Tokens.VersionNeutral, RouteGroupBuilder, Endpoint, RouteGroupBuilder, Setup

### Community 167 - "PaginationRequest"
Cohesion: 0.12
Nodes (13): PaginationRequest, PaginationResponse, DbContextExtensions, CancellationToken, DbSet, JsonSerializerOptions, Task, PaginationQueryableExtensions (+5 more)

### Community 168 - "CachedCaptchaService"
Cohesion: 0.40
Nodes (3): CancellationToken, Task, CachedCaptchaService

### Community 169 - "Policies.CreateStore.cs"
Cohesion: 0.17
Nodes (8): Products.Infrastructure.RateLimiting, RateLimiterOptions, Policies, Action, IEnumerable, RateLimiterOptions, string, RateLimitingConstants

### Community 171 - ".UpdateStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 172 - "ResxLocalizationOptions.cs"
Cohesion: 0.67
Nodes (3): ResxLocalizationOptions, ResxLocalizationOptionsValidator, ICollection

### Community 173 - "PushOptions.cs"
Cohesion: 0.70
Nodes (4): FirebaseServiceAccountOptions, PushOptions, PushOptionsValidator, PushProvider

### Community 174 - "IAM.Application.Tokens.DTOs"
Cohesion: 0.29
Nodes (5): IAM.Application.Tokens.DTOs, DateTimeOffset, AccessTokenDto, DateTimeOffset, TokensDto

### Community 175 - ".GetProductAuditLogAsync"
Cohesion: 0.14
Nodes (9): AuditLogDto, CancellationToken, RouteGroupBuilder, Task, Endpoint, CancellationToken, RouteGroupBuilder, Task (+1 more)

### Community 176 - "ApiKeysOptions.cs"
Cohesion: 0.48
Nodes (6): AbstractValidator, ApiKeyEntry, ApiKeyEntryValidator, ApiKeysOptions, ApiKeysOptionsValidator, IReadOnlyList

### Community 177 - "PermissionPolicyProvider"
Cohesion: 0.23
Nodes (7): AuthorizationPolicy, IAM.Application.Auth, IAuthorizationPolicyProvider, string, CustomClaims, Task, PermissionPolicyProvider

### Community 179 - "CachingOptions.cs"
Cohesion: 0.52
Nodes (6): CachingEntryDefaults, CachingOptions, CachingOptionsValidator, Redis, RedisValidator, TimeSpan

### Community 180 - "SmsOptions.cs"
Cohesion: 0.23
Nodes (9): SmsOptions, SmsOptionsValidator, SmsProvider, SmsTemplatesOptions, Dictionary, IConfiguration, IServiceCollection, long (+1 more)

### Community 182 - "Constants"
Cohesion: 0.33
Nodes (4): IAM.Domain, string, Constants, IAssemblyReference

### Community 183 - "SendRequestBody"
Cohesion: 0.67
Nodes (3): SendMessageBody, IReadOnlyList, SendRequestBody

### Community 185 - "IamModule"
Cohesion: 0.18
Nodes (8): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, IamModule

### Community 186 - "IEventHandler.cs"
Cohesion: 0.40
Nodes (3): IEventHandler, CancellationToken, Task

### Community 187 - "ReverseProxyOptions.cs"
Cohesion: 0.50
Nodes (3): ReverseProxyOptions, ReverseProxyOptionsValidator, IReadOnlyList

### Community 188 - "IAM.Infrastructure.Auth"
Cohesion: 0.24
Nodes (5): IAM.Infrastructure.Auth, ClaimsPrincipal, ClaimsPrincipalExtensions, string, MultiAuthDefaults

### Community 191 - "Split-Deployment PoC"
Cohesion: 0.25
Nodes (7): Concurrent safety, Cross-process call path, Files added by this PoC, How it works, How to run, Split-Deployment PoC, What this proves

### Community 194 - "CorsOptions.cs"
Cohesion: 0.67
Nodes (3): CorsOptions, CorsOptionsValidator, IReadOnlyList

### Community 196 - "JwtOptions"
Cohesion: 0.67
Nodes (3): JwtOptions, JwtOptionsValidator, IReadOnlyCollection

### Community 197 - "IntegrationEventHandlerBase"
Cohesion: 0.06
Nodes (34): Products.Endpoints.Probe.v1, IConsumer, IntegrationEventHandlerBase, CancellationToken, ConsumeContext, DefaultIdType, ILogger, LoggerMessage (+26 more)

### Community 200 - "NotificationsTelemetry"
Cohesion: 0.22
Nodes (6): ActivitySource, Counter, Meter, string, NotificationsTelemetry, UpDownCounter

### Community 203 - ".UpdateProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 213 - "ProductsTelemetry"
Cohesion: 0.33
Nodes (5): ActivitySource, Counter, Meter, string, ProductsTelemetry

### Community 215 - ".DeactivateProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 219 - ".UpdateCurrentSessionPushToken"
Cohesion: 0.20
Nodes (8): IAM.Endpoints.Tokens.VersionNeutral.Sessions.UpdatePushToken, CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint, Request, RequestValidator

### Community 229 - ".AddCommonOptions"
Cohesion: 0.33
Nodes (4): Setup, IConfiguration, IHostEnvironment, IServiceCollection

### Community 243 - "ActionsAndResources.cs"
Cohesion: 0.67
Nodes (3): CustomActions, CustomResources, string

### Community 244 - "CustomRoles"
Cohesion: 0.50
Nodes (3): CustomRoles, HashSet, string

## Knowledge Gaps
- **135 isolated node(s):** `OtpCacheEntry`, `Common.Domain`, `Common.Infrastructure`, `IAssemblyReference`, `PushTarget` (+130 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **109 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `Common.Domain.ResultMonad`, `Common.Infrastructure.Persistence.ValueConverters`, `NotificationPayload`, `.FixedWindow`, `Common.Application.FeatureManagement`, `Common.Application.Auth`, `IAM.Endpoints.Common.Validations`, `OutboxOptions.cs`, `NotificationsModule.cs`, `Host.Swagger`, `Common.Infrastructure.Extensions`, `ObservabilityOptions`, `Policies.CreateStore.cs`, `ResxLocalizationOptions.cs`, `PushOptions.cs`, `ApiKeysOptions.cs`, `OtpOptions.cs`, `CachingOptions.cs`, `SmsOptions.cs`, `RabbitMqOptions.cs`, `Notifications.Infrastructure.Telemetry`, `SignalROptions.cs`, `ReverseProxyOptions.cs`, `RequestLoggingOptions`, `CorsOptions.cs`, `JwtOptions`, `IntegrationEventHandlerBase`, `Common.Application.Validation`, `Common.Application.EventBus`, `SelfRegister/Request.cs`, `.CreateTokens`, `CaptchaOptions.cs`, `Common.Domain.StronglyTypedIds`, `FirebasePushGateway.cs`, `.PurgeExpiredEntriesAsync`, `.SearchStoreProductsAsync`, `DatabaseOptions.cs`, `.AddCommonOptions`, `Common.Infrastructure/Persistence/Setup.cs`, `ApplySearchLanguageInterceptor.cs`, `OutboxCleanupJob.cs`, `FullTextSearchOptions`?**
  _High betweenness centrality (0.372) - this node is a cross-community bridge._
- **Why does `Common.Application.Auth` connect `Common.Application.Auth` to `Common.Domain.ResultMonad`, `PermissionAuthorizationHandler`, `.AuthorizeAsync`, `Common.Infrastructure.Extensions`, `.GetMeAsync`, `Common.Infrastructure.FeatureManagement`, `.InvokeAsync`, `ICurrentUser`, `Common.Application.FeatureManagement`, `Common.Domain.StronglyTypedIds`, `Common.Infrastructure.Persistence`, `Common.Application.BackgroundJobs`, `ActionsAndResources.cs`, `CustomRoles`, `IAM.Infrastructure.Persistence.Seeding`, `JwtClaimNames.cs`?**
  _High betweenness centrality (0.088) - this node is a cross-community bridge._
- **Why does `Result` connect `Result` to `.GetProductAsync`, `.ActivateProductTemplateAsync`, `.RemoveProductAsync`, `.ListSessions`, `.SingleAsResult`, `FirebasePushGateway`, `Error`, `.RegisterAndLoginAsync`, `.SendOtp`, `.GetMyStoreAsync`, `.GetAsync`, `.SendAsync`, `.RevokeSession`, `NetGsmSmsGateway`, `VerifyPhoneOtpRequest`, `v1/AddProduct/Request.cs`, `.UpdateMyStoreAsync`, `.GetMeAsync`, `.RefreshToken`, `PaginationRequest`, `CachedCaptchaService`, `.UpdateStoreAsync`, `.GetProductAuditLogAsync`, `.SearchUsersAsync`, `.SendOtp`, `ReCaptchaService`, `ProductTemplateId`, `.SendAsync`, `.SearchProductTemplatesAsync`, `.AddProductToMyStoreAsync`, `.GetProductTemplateAsync`, `ICurrentUser`, `.UpdateProductAsync`, `IProductsDbContext`, `.SearchStoresAsync`, `ResultTelemetryExtensions`, `.DeactivateProductTemplateAsync`, `.CreateTokens`, `.UpdateCurrentSessionPushToken`, `.SearchMyProductsAsync`, `.SearchStoreProductsAsync`, `.GetMyStoreAuditLogAsync`, `.SendAsync`, `.IsRegisteredAsync`, `.TapWhenFeatureEnabledAsync`?**
  _High betweenness centrality (0.082) - this node is a cross-community bridge._
- **What connects `OtpCacheEntry`, `Common.Domain`, `Common.Infrastructure` to the rest of the system?**
  _135 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Common.Domain.ResultMonad` be split into smaller, more focused modules?**
  _Cohesion score 0.11336032388663968 - nodes in this community are weakly interconnected._
- **Should `NotificationPayload` be split into smaller, more focused modules?**
  _Cohesion score 0.061952861952861954 - nodes in this community are weakly interconnected._
- **Should `IIAMDbContext` be split into smaller, more focused modules?**
  _Cohesion score 0.09523809523809523 - nodes in this community are weakly interconnected._