# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-08-10)

## Corpus Check
- 466 files · ~67,989 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2965 nodes · 5258 edges · 302 communities (199 shown, 103 thin omitted)
- Extraction: 99% EXTRACTED · 1% INFERRED · 0% AMBIGUOUS · INFERRED: 62 edges (avg confidence: 0.8)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `3be7702b`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- SelfRegister/Endpoint.cs
- ApplicationUserId
- OutboxProcessor
- IAMDbContext
- Hybrid DDD (Writes) / VSA (Reads)
- Session
- RedisFixedWindowRateLimiter
- Common.Infrastructure.Persistence.Auditing
- FirebasePushGateway
- CustomValidator
- Error
- Common.Domain.ResultMonad
- .RegisterAndLoginAsync
- SendPhoneOtpRequest
- LocalizedIdentityErrorDescriber
- Products.Domain.ProductTemplates
- Notifications.Application.Sms
- .AddOrUpdate
- DomainEvent
- AggregateRoot
- IAM.Endpoints.Common.Validations
- BoundedCaptureStream
- IDatabaseSeeder
- Notifications.Infrastructure.Telemetry
- .RevokeToken
- NetGsmSmsGateway
- IEvent
- RequestResponseBodyLoggingMiddleware
- ProblemDetailsExtensions
- Result
- IAM.Infrastructure/Auth/Setup.cs
- .AddModules
- Endpoint
- v1/AddProduct/Request.cs
- IAM.Domain.Identity
- OutboxMessage
- CustomRateLimitingOptions
- Seeder
- .RefreshToken
- ObservabilityOptions
- SmsRateLimitingPolicy
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
- ICaptchaService
- ReCaptchaService
- StoreId
- .AddResilientHttpClient
- CheckRegistrationRateLimitingPolicy
- .SendAsync
- Cross-Module Reference Violation
- StronglyTypedIdWriteOnlyJsonConverter
- .LogAssemblyLoadFailed
- Bogus Test Data
- .AddProductToMyStoreAsync
- RequireFeatureFilter
- RequestLoggingOptions
- IStronglyTypedId
- .SavingChangesAsync
- OutboxModule
- .GetProductTemplateAsync
- ValueObject
- DomainEventHandlerBase
- PolymorphicEventConverter
- Full-Text Search
- IProductsDbContext
- .WriteAsync
- AsNoTracking Coverage Check
- ProductId
- Common.Application.Validation
- .GetStoreAsync
- TokenCreateRateLimitingPolicy
- Common.Application.EventBus
- .DispatchAsync
- StrictDateTimeOffsetJsonConverter
- StringExtensions
- .EnsureNoMigrationsPending
- AuditLogEntry
- ResultTelemetryExtensions
- AuditLogRetentionJobRegistrar
- .LogDispatchingNotification
- Configuration-Driven Module Registration
- Setup
- .CreateTokens
- CaptchaOptions.cs
- Common.Domain.StronglyTypedIds
- Endpoint
- Common.Application.Options
- .AddInfrastructure
- .AddPushServices
- ConfigureSwaggerOptions
- .SearchMyProductsAsync
- .AuthorizeAsync
- Common.Application.BackgroundJobs
- ApplicationUser
- Common.Infrastructure.FeatureManagement
- JobMetricsFilter
- .SendAsync
- StronglyTypedIdListReadOnlyJsonConverter
- ProductTemplate
- SeedingCompletionTracker
- .TryDeserialize
- .IsRegisteredAsync
- Stores/v1/Create/Request.cs
- IBackgroundJobs
- UserRegisteredSignalRHandler
- Endpoint
- Refresh/Request.cs
- BackgroundJobsService
- BackgroundJobsModule
- ApiKeyAuthenticationHandler
- IModule
- ProductsModule
- IAM.Infrastructure.Auth.ApiKey
- IntegrationEvent
- TokenRefreshRateLimitingPolicy
- NotificationsModule
- .LogHealthChecksRegistered
- .TapWhenFeatureEnabledAsync
- GlobalExceptionHandlingMiddleware
- BaseDbContext
- Common.Application.JsonConverters
- .GetProductAsync
- docker-compose.yml (Base Stack)
- .InvokeAsync
- SwaggerDefaultValues
- Consumer Idempotency (IntegrationEventHandlerBase)
- .RemoveProductAsync
- .ListSessions
- .SingleAsResult
- .GlobalRateLimiter
- Common.Application.FeatureManagement
- My/Create/Request.cs
- .MapEndpoint
- Infrastructure/Setup.cs
- .ActivateProductTemplateAsync
- IDbContext
- IIAMDbContext
- .FixedWindow
- .RevokeSession
- Stores/v1/Update/Request.cs
- Setup
- .GetRoleIdByName
- FeatureFlags
- StronglyTypedIdSchemaFilter.cs
- .SendOtp
- Setup
- IOperationFilter
- VerifyPhoneOtpRequest
- Common.Application.ModelBinders
- .SendOtp
- RegisterRateLimitingPolicy
- .Capture
- ValidationContextExtensions
- Stores/v1/My/Update/Request.cs
- PermissionAuthorizationHandler
- For
- Host.Middlewares
- .GetMeAsync
- Endpoint
- Common.Application.Pagination
- CachedCaptchaService
- ProductsModule.cs
- Notifications.Application/IAssemblyReference.cs
- .UpdateStoreAsync
- BackgroundJobsTelemetry
- Host.Swagger
- IAM.Application.Tokens.DTOs
- .GetAuditLogAsync
- ApiKeysOptions.cs
- PermissionPolicyProvider
- EventBus/Setup.cs
- CachingOptions.cs
- SmsOptions.cs
- Setup.SignalR.cs
- Constants
- SendRequestBody
- Setup
- IAMModule.cs
- IEventHandler.cs
- ReverseProxyOptions.cs
- IAM.Infrastructure.Auth
- DefaultResponsesOperationFilter
- Split-Deployment PoC
- V1UserRegisteredDomainEvent
- V1SessionRevokedDomainEvent
- CorsOptions.cs
- Setup
- JwtOptions
- Common.InterModuleRequests.Contracts
- .AddCustomMassTransit
- ReCaptchaResponse
- NotificationsTelemetry
- SignalROptions.cs
- HttpContextExtensions.cs
- .UpdateProductAsync
- Configuration-Driven Module Loading
- Infrastructure/StringExtensions.cs
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- ProductsTelemetry
- .DeactivateProductTemplateAsync
- Auditing/Setup.cs
- .UpdateCurrentSessionPushToken
- JwtClaimNames.cs
- ModulesOptions.cs
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
8. `ApplicationUserId` - 50 edges
9. `Setup` - 50 edges
10. `Common.Application.Extensions` - 49 edges

## Surprising Connections (you probably didn't know these)
- `Aspire Dashboard Service (mm.aspire-dashboard)` --conceptually_related_to--> `Observability (OpenTelemetry)`  [INFERRED]
  docker-compose.yml → CLAUDE.md
- `CaptchaErrors` --references--> `Error`  [EXTRACTED]
  src/Modules/IAM/IAM.Domain/Captcha/CaptchaErrors.cs → src/Common/Common.Domain/ResultMonad/Error.cs
- `SmsErrors` --references--> `Error`  [EXTRACTED]
  src/Modules/Notifications/Notifications.Application/Sms/SmsErrors.cs → src/Common/Common.Domain/ResultMonad/Error.cs
- `RequestBody` --references--> `ProductTemplateId`  [EXTRACTED]
  src/Modules/Products/Products.Endpoints/Stores/v1/AddProduct/Request.cs → src/Modules/Products/Products.Domain/ProductTemplates/ProductTemplate.cs
- `ICurrentUser` --references--> `ApplicationUserId`  [EXTRACTED]
  src/Common/Common.Application/Auth/ICurrentUser.cs → src/Common/Common.Domain/StronglyTypedIds/ApplicationUserId.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Local Infrastructure Stack** — docker_compose_postgres, docker_compose_rabbitmq, docker_compose_redis, docker_compose_aspire_dashboard [EXTRACTED 1.00]

## Communities (302 total, 103 thin omitted)

### Community 0 - "SelfRegister/Endpoint.cs"
Cohesion: 0.12
Nodes (8): IAM.Application.Extensions, IAM.Endpoints.Otp, IAM.Infrastructure.Telemetry, IAM.Domain.Errors, string, OtpPurposes, string, Constants

### Community 1 - "ApplicationUserId"
Cohesion: 0.05
Nodes (41): Notifications.Application.Hubs, Hub, IEntityTypeConfiguration, ApplicationUserId, DefaultIdType, EntityTypeBuilder, IdentityRole, IdentityRoleClaim (+33 more)

### Community 2 - "OutboxProcessor"
Cohesion: 0.16
Nodes (13): BackgroundService, DatabaseSeederOrchestrator, CancellationToken, ILogger, LoggerMessage, Task, CancellationToken, Exception (+5 more)

### Community 3 - "IAMDbContext"
Cohesion: 0.12
Nodes (13): IdentityDbContext, CancellationToken, DbSet, IdentityRole, IdentityRoleClaim, IdentityUserClaim, IdentityUserLogin, IdentityUserRole (+5 more)

### Community 5 - "Session"
Cohesion: 0.14
Nodes (12): DateTimeOffset, Guid, DateTimeOffset, RefreshToken, RefreshTokenId, DateTimeOffset, Guid, IReadOnlyCollection (+4 more)

### Community 6 - "RedisFixedWindowRateLimiter"
Cohesion: 0.11
Nodes (17): IConnectionMultiplexer, RateLimiter, RateLimiterStatistics, RateLimitLease, FixedWindowLease, RedisFixedWindowRateLimiter, bool, CancellationToken (+9 more)

### Community 7 - "Common.Infrastructure.Persistence.Auditing"
Cohesion: 0.18
Nodes (5): Common.Infrastructure.Persistence.Auditing, Setup, IServiceCollection, Setup, IServiceCollection

### Community 8 - "FirebasePushGateway"
Cohesion: 0.16
Nodes (13): FirebaseApp, FirebaseMessaging, PushMessage, CancellationToken, Exception, IEnumerable, ILogger, int (+5 more)

### Community 9 - "CustomValidator"
Cohesion: 0.13
Nodes (19): Products.Endpoints.Products.v1.My.Update, OtpOptions, OtpOptionsValidator, OutboxCleanupSettings, OutboxCleanupSettingsValidator, OutboxOptions, OutboxOptionsValidator, ResxLocalizationOptions (+11 more)

### Community 10 - "Error"
Cohesion: 0.10
Nodes (13): HttpStatusCode, IdentityResult, IStringLocalizer, StringLocalizerExtensions, Error, ICollection, IResult, IdentityResultExtensions (+5 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.15
Nodes (12): Common.Application.Search, Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Products.Infrastructure.Telemetry, IAM.Endpoints.Tokens.VersionNeutral.Revoke, Products.Application.Persistence, Products.Domain.Stores (+4 more)

### Community 12 - ".RegisterAndLoginAsync"
Cohesion: 0.18
Nodes (14): IAM.Endpoints.Users.VersionNeutral.SelfRegister, CancellationToken, HttpContext, IFeatureManager, IOptions, Task, TimeProvider, Endpoint (+6 more)

### Community 13 - "SendPhoneOtpRequest"
Cohesion: 0.39
Nodes (5): SendPhoneOtpRequest, SendPhoneOtpResponse, CancellationToken, Task, SendPhoneOtpRequestHandler

### Community 14 - "LocalizedIdentityErrorDescriber"
Cohesion: 0.13
Nodes (4): IAM.Infrastructure.Identity, IdentityError, IdentityErrorDescriber, LocalizedIdentityErrorDescriber

### Community 15 - "Products.Domain.ProductTemplates"
Cohesion: 0.10
Nodes (14): Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Common.Application.Persistence, Common.Infrastructure.EventBus, IAM.Infrastructure.Persistence.EntityConfigurations, Common.Infrastructure.Persistence.EntityConfigurations, Products.Domain.ProductTemplates, Common.Infrastructure.Persistence.ValueConverters (+6 more)

### Community 16 - "Notifications.Application.Sms"
Cohesion: 0.16
Nodes (8): Notifications.Application.Sms, Notifications.Infrastructure.Sms, Notifications.Infrastructure.Sms.NetGsm, SmsErrors, CancellationToken, Task, TimeSpan, ThrottledSmsGateway

### Community 17 - ".AddOrUpdate"
Cohesion: 0.13
Nodes (11): RecurringJobOptions, IRecurringBackgroundJobs, Action, Expression, Func, Task, RecurringBackgroundJobsService, Action (+3 more)

### Community 18 - "DomainEvent"
Cohesion: 0.13
Nodes (19): DomainEvent, DateTimeOffset, DefaultIdType, V1RefreshTokenRevokedDomainEvent, V1RefreshTokenUpdatedDomainEvent, V1SessionCreatedDomainEvent, V1UserImageUrlUpdatedDomainEvent, V1ProductCreatedDomainEvent (+11 more)

### Community 19 - "AggregateRoot"
Cohesion: 0.14
Nodes (11): AggregateRoot, IEnumerable, IReadOnlyCollection, List, IAggregateRoot, IEnumerable, IReadOnlyCollection, AuditableEntity (+3 more)

### Community 20 - "IAM.Endpoints.Common.Validations"
Cohesion: 0.09
Nodes (19): IAM.Endpoints.Otp.VersionNeutral.SendForLogin, IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, IAM.Endpoints.Common.Validations, IResxLocalizer, IRuleBuilder, IRuleBuilderOptions, CommonValidations, IResxLocalizer (+11 more)

### Community 21 - "BoundedCaptureStream"
Cohesion: 0.14
Nodes (7): byte, SeekOrigin, bool, int, BoundedCaptureStream, BoundedRequestCaptureStream, Stream

### Community 22 - "IDatabaseSeeder"
Cohesion: 0.14
Nodes (9): IDatabaseSeeder, CancellationToken, Task, CancellationToken, Task, IamDatabaseSeeder, CancellationToken, Task (+1 more)

### Community 23 - "Notifications.Infrastructure.Telemetry"
Cohesion: 0.10
Nodes (13): Notifications.Application.Otp, Notifications.Infrastructure.Push, Notifications.Infrastructure.Telemetry, Notifications.Infrastructure.InterModuleRequestHandlers, Notifications.Infrastructure.Hubs, Notifications.Infrastructure.Otp, Notifications.Infrastructure, NotificationGroupName (+5 more)

### Community 24 - ".RevokeToken"
Cohesion: 0.10
Nodes (15): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint, CancellationToken, RouteGroupBuilder, Task (+7 more)

### Community 25 - "NetGsmSmsGateway"
Cohesion: 0.18
Nodes (11): SendRequestBody, CancellationToken, Exception, ILogger, JsonSerializerOptions, LoggerMessage, string, Task (+3 more)

### Community 26 - "IEvent"
Cohesion: 0.18
Nodes (7): CancellationToken, Task, IOutboxMessage, DateTimeOffset, IEvent, DateTimeOffset, DefaultIdType

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

### Community 31 - ".AddModules"
Cohesion: 0.20
Nodes (8): LoadAll, Names, Assembly, IConfiguration, IEnumerable, IReadOnlyList, IServiceCollection, Type

### Community 32 - "Endpoint"
Cohesion: 0.29
Nodes (5): Products.Endpoints.Stores, RouteGroupBuilder, Setup, RouteGroupBuilder, Endpoint

### Community 33 - "v1/AddProduct/Request.cs"
Cohesion: 0.16
Nodes (10): Products.Endpoints.Stores.v1.AddProduct, CancellationToken, RouteGroupBuilder, Task, Endpoint, RequestBody, Request, RequestBody (+2 more)

### Community 34 - "IAM.Domain.Identity"
Cohesion: 0.13
Nodes (7): IAM.Infrastructure.Persistence, IAM.Infrastructure.Persistence.Seeding, IAM.Domain.Identity, int, Constants, IServiceCollection, Setup

### Community 35 - "OutboxMessage"
Cohesion: 0.10
Nodes (14): Common.Application.Persistence.Outbox, OutboxMessage, DateTimeOffset, TimeSpan, IOutboxDbContext, CancellationToken, DbSet, Task (+6 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.23
Nodes (9): CustomRateLimitingOptions, CustomRateLimitingOptionsValidator, FixedWindow, FixedWindowValidator, IReadOnlyList, Policies, Action, IEnumerable (+1 more)

### Community 37 - "Seeder"
Cohesion: 0.16
Nodes (11): Task, IdentityRole, ILogger, LoggerMessage, Task, Seeder, Action, DateOnly (+3 more)

### Community 38 - ".RefreshToken"
Cohesion: 0.12
Nodes (15): accessToken, DateTimeOffset, expiresAt, ICollection, refreshTokenBytes, ITokenService, CancellationToken, HttpContext (+7 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.20
Nodes (8): KeyValuePair, LoggerConfiguration, LoggerMinimumLevelConfiguration, ObservabilityOptions, ObservabilityOptionsValidator, Dictionary, IEnumerable, IHostEnvironment

### Community 40 - "SmsRateLimitingPolicy"
Cohesion: 0.33
Nodes (6): IRateLimiterPolicy, CancellationToken, Func, OnRejectedContext, ValueTask, SmsRateLimitingPolicy

### Community 41 - "AuditableEntityResponse"
Cohesion: 0.06
Nodes (30): IAM.Endpoints.Users.VersionNeutral.Search, Products.Endpoints.Stores.v1.Search, Products.Endpoints.Products.v1.My.Get, Products.Endpoints.ProductTemplates.v1.Search, Products.Endpoints.Products.v1.Search, Common.Application.DTOs, IAM.Endpoints.Users.VersionNeutral.Get, Products.Endpoints.Stores.v1.Get (+22 more)

### Community 42 - ".SaveWithOutboxAsync"
Cohesion: 0.27
Nodes (9): OutboxSaveHelper, CancellationToken, DbContext, Exception, Func, ILogger, LoggerMessage, Task (+1 more)

### Community 43 - "CreateStoreRateLimitingPolicy"
Cohesion: 0.20
Nodes (8): CancellationToken, Func, HttpContext, OnRejectedContext, RateLimitPartition, TimeSpan, ValueTask, CreateStoreRateLimitingPolicy

### Community 44 - "Product"
Cohesion: 0.17
Nodes (8): ISearchLocalized, Product, IReadOnlyCollection, List, Store, DbSet, ModelBuilder, ProductsDbContext

### Community 45 - "ProductTemplateId"
Cohesion: 0.10
Nodes (16): Products.Endpoints.ProductTemplates.v1.Deactivate, Products.Endpoints.ProductTemplates.v1.Create, Products.Endpoints.ProductTemplates.v1.Activate, ProductTemplateId, Request, RequestValidator, CancellationToken, Task (+8 more)

### Community 46 - "Setup"
Cohesion: 0.29
Nodes (4): ApiVersionSet, Setup, IEndpointRouteBuilder, IServiceCollection

### Community 48 - "OtpServiceBase"
Cohesion: 0.08
Nodes (19): SemaphoreSlim, CancellationToken, Task, TimeSpan, IOtpService, OtpVerificationOutcome, string, DummyOtpService (+11 more)

### Community 50 - "IntegrationEventHandlerBase"
Cohesion: 0.32
Nodes (8): IntegrationEventHandlerBase, CancellationToken, ConsumeContext, DefaultIdType, ILogger, LoggerMessage, Task, TimeSpan

### Community 51 - "ICaptchaService"
Cohesion: 0.25
Nodes (4): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, ICaptchaService, Response, DummyCaptchaService

### Community 52 - "ReCaptchaService"
Cohesion: 0.22
Nodes (9): double, FormUrlEncodedContent, ReCaptchaResponse, CancellationToken, Exception, ILogger, LoggerMessage, Task (+1 more)

### Community 53 - "StoreId"
Cohesion: 0.11
Nodes (14): Products.Infrastructure.Persistence.Seeding, StoreId, CancellationToken, ILogger, int, LoggerMessage, Task, CancellationToken (+6 more)

### Community 54 - ".AddResilientHttpClient"
Cohesion: 0.22
Nodes (7): Common.Infrastructure.Resiliency, HttpClient, HttpStandardResilienceOptions, IHttpClientBuilder, Setup, Action, IServiceCollection

### Community 55 - "CheckRegistrationRateLimitingPolicy"
Cohesion: 0.25
Nodes (7): CancellationToken, Func, HttpContext, OnRejectedContext, RateLimitPartition, ValueTask, CheckRegistrationRateLimitingPolicy

### Community 56 - ".SendAsync"
Cohesion: 0.16
Nodes (10): CancellationToken, Task, ISmsGateway, SmsCategory, SmsMessage, CancellationToken, ILogger, LoggerMessage (+2 more)

### Community 58 - "StronglyTypedIdWriteOnlyJsonConverter"
Cohesion: 0.24
Nodes (6): JsonConverter, StronglyTypedIdWriteOnlyJsonConverter, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 59 - ".LogAssemblyLoadFailed"
Cohesion: 0.27
Nodes (5): Exception, IApplicationBuilder, ILogger, LoggerMessage, WebApplication

### Community 61 - ".AddProductToMyStoreAsync"
Cohesion: 0.14
Nodes (11): Products.Endpoints.Stores.v1.My.AddProduct, decimal, int, Constants, CancellationToken, RouteGroupBuilder, Task, Endpoint (+3 more)

### Community 62 - "RequireFeatureFilter"
Cohesion: 0.10
Nodes (16): Common.Application.EndpointFilters, IEndpointFilter, ResultToCreatedResponseTransformer, ResultToResponseTransformer, EndpointFilterDelegate, EndpointFilterInvocationContext, ValueTask, RouteHandlerBuilderExtensions (+8 more)

### Community 63 - "RequestLoggingOptions"
Cohesion: 0.31
Nodes (7): IPostConfigureOptions, RequestLoggingOptions, RequestLoggingOptionsValidator, SensitivePathRule, IList, int, RequestLoggingPathPostConfigure

### Community 64 - "IStronglyTypedId"
Cohesion: 0.25
Nodes (7): StronglyTypedIdReadOnlyJsonConverter, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, IStronglyTypedId, DefaultIdType

### Community 65 - ".SavingChangesAsync"
Cohesion: 0.15
Nodes (11): SaveChangesInterceptor, ApplyAuditingInterceptor, CancellationToken, DbContextEventData, InterceptionResult, ValueTask, ApplySearchLanguageInterceptor, CancellationToken (+3 more)

### Community 66 - "OutboxModule"
Cohesion: 0.06
Nodes (32): Outbox, Common.Infrastructure.Persistence.Outbox, Outbox.Persistence, Outbox.Telemetry, CancellationToken, ILogger, LoggerMessage, Task (+24 more)

### Community 67 - ".GetProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 68 - "ValueObject"
Cohesion: 0.25
Nodes (4): Common.Domain, IComparable, ValueObject, IEnumerable

### Community 69 - "DomainEventHandlerBase"
Cohesion: 0.14
Nodes (15): DomainEventHandlerBase, CancellationToken, Task, V1AllSessionsRevokedCacheInvalidationHandler, CancellationToken, Task, V1SessionRefreshedCacheInvalidationHandler, V1AllSessionsRevokedDomainEvent (+7 more)

### Community 70 - "PolymorphicEventConverter"
Cohesion: 0.27
Nodes (6): PolymorphicEventConverter, JsonSerializerOptions, string, Type, Utf8JsonReader, Utf8JsonWriter

### Community 71 - "Full-Text Search"
Cohesion: 0.08
Nodes (25): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Add a new language/culture, Add search to a new entity _(Build checklist)_ (+17 more)

### Community 72 - "IProductsDbContext"
Cohesion: 0.05
Nodes (32): ICurrentUser, Guid, ICollection, DbSet, IProductsDbContext, CancellationToken, RouteGroupBuilder, Task (+24 more)

### Community 73 - ".WriteAsync"
Cohesion: 0.33
Nodes (5): Memory, ReadOnlyMemory, CancellationToken, Task, ValueTask

### Community 75 - "ProductId"
Cohesion: 0.11
Nodes (16): Products.Endpoints.Stores.v1.My.RemoveProduct, Products.Endpoints.Products.v1.Update, Products.Endpoints.Stores.v1.RemoveProduct, ProductId, Request, RequestValidator, Request, RequestValidator (+8 more)

### Community 76 - "Common.Application.Validation"
Cohesion: 0.10
Nodes (18): Common.Application.Validation, AuditLogOptions, AuditLogOptionsValidator, BackgroundJobsOptions, BackgroundJobsOptionsValidator, InterModuleRequestOptions, InterModuleRequestOptionsValidator, OpenApiOptions (+10 more)

### Community 77 - ".GetStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 78 - "TokenCreateRateLimitingPolicy"
Cohesion: 0.25
Nodes (7): CancellationToken, Func, HttpContext, OnRejectedContext, RateLimitPartition, ValueTask, TokenCreateRateLimitingPolicy

### Community 79 - "Common.Application.EventBus"
Cohesion: 0.20
Nodes (7): Common.IntegrationEvents, Common.Application.Caching, Notifications.Application.IntegrationEventHandlers, IAM.Application.Users.DomainEventHandlers.v1, Products.Application.Stores.DomainEventHandlers.v1, Common.Application.EventBus, OtpCacheEntry

### Community 80 - ".DispatchAsync"
Cohesion: 0.19
Nodes (9): IEventHandlerWrapper, CancellationToken, Task, EventDispatcher, ActivitySource, CancellationToken, ILogger, LoggerMessage (+1 more)

### Community 81 - "StrictDateTimeOffsetJsonConverter"
Cohesion: 0.29
Nodes (6): StrictDateTimeOffsetJsonConverter, DateTimeOffset, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 82 - "StringExtensions"
Cohesion: 0.33
Nodes (3): Common.Domain.Extensions, SearchValues, StringExtensions

### Community 83 - ".EnsureNoMigrationsPending"
Cohesion: 0.44
Nodes (4): IServiceProvider, MigrationGuard, ILogger, LoggerMessage

### Community 84 - "AuditLogEntry"
Cohesion: 0.13
Nodes (14): AuditLogEntry, DefaultIdType, AuditableEntityConfiguration, EntityTypeBuilder, AuditLogEntryConfiguration, EntityTypeBuilder, EntityTypeBuilder, RefreshTokenConfig (+6 more)

### Community 85 - "ResultTelemetryExtensions"
Cohesion: 0.32
Nodes (4): Activity, ResultTelemetryExtensions, ActivitySource, Task

### Community 86 - "AuditLogRetentionJobRegistrar"
Cohesion: 0.11
Nodes (16): Common.Infrastructure.Persistence.AuditLog, IHostedService, AuditLogRetentionJobRegistrar, CancellationToken, ILogger, LoggerMessage, string, Task (+8 more)

### Community 87 - ".LogDispatchingNotification"
Cohesion: 0.28
Nodes (7): SessionTokenReuseDetectedIntegrationEvent, CancellationToken, Guid, ILogger, LoggerMessage, Task, SessionTokenReuseDetectedSignalRHandler

### Community 89 - "Setup"
Cohesion: 0.33
Nodes (4): ConfigurationManager, Host.Configurations, Setup, WebApplicationBuilder

### Community 90 - ".CreateTokens"
Cohesion: 0.15
Nodes (11): IAM.Endpoints.Tokens.VersionNeutral.Create, IInterModuleRequestClient, CancellationToken, Task, CancellationToken, HttpContext, IOptions, Task (+3 more)

### Community 91 - "CaptchaOptions.cs"
Cohesion: 0.31
Nodes (6): CaptchaOptions, CaptchaOptionsValidator, CaptchaProvider, IConfiguration, IServiceCollection, Setup

### Community 92 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.10
Nodes (10): Products.Domain.Products.DomainEvents.v1, Common.Domain.StronglyTypedIds, IAM.Application.Tokens.Services, IAM.Domain.Identity.DomainEvents.v1, Common.Domain.Events, Products.Domain.Products, IAM.Domain.Identity.Sessions, Common.Domain.Entities (+2 more)

### Community 93 - "Endpoint"
Cohesion: 0.29
Nodes (5): IAM.Endpoints.Captcha.VersionNeutral, RouteGroupBuilder, Endpoint, RouteGroupBuilder, Setup

### Community 94 - "Common.Application.Options"
Cohesion: 0.23
Nodes (6): Notifications.Application.Push, Common.Infrastructure.RateLimiting, Common.Infrastructure.Extensions, Common.Application.Options, IAM.Infrastructure.RateLimiting, Notifications.Infrastructure.Push.Firebase

### Community 95 - ".AddInfrastructure"
Cohesion: 0.44
Nodes (4): Assembly, IConfiguration, IServiceCollection, IWebHostEnvironment

### Community 96 - ".AddPushServices"
Cohesion: 0.50
Nodes (3): IConfiguration, IServiceCollection, Setup

### Community 97 - "ConfigureSwaggerOptions"
Cohesion: 0.32
Nodes (5): ApiVersionDescription, IConfigureOptions, OpenApiInfo, ConfigureSwaggerOptions, SwaggerGenOptions

### Community 98 - ".SearchMyProductsAsync"
Cohesion: 0.05
Nodes (34): FullTextSearchOptions, FullTextSearchOptionsValidator, Dictionary, IReadOnlyList, string, PaginationResponse, ISearchLanguageResolver, SearchLanguageResolver (+26 more)

### Community 99 - ".AuthorizeAsync"
Cohesion: 0.15
Nodes (7): DashboardContext, IDashboardAsyncAuthorizationFilter, CustomPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder, HangfireCustomAuthorizationFilter, Task

### Community 100 - "Common.Application.BackgroundJobs"
Cohesion: 0.32
Nodes (3): Common.Application.BackgroundJobs, BackgroundJobs.Telemetry, BackgroundJobs

### Community 101 - "ApplicationUser"
Cohesion: 0.14
Nodes (10): IdentityUser, IEnumerable, IReadOnlyCollection, List, ApplicationUser, DateTimeOffset, DateOnly, IReadOnlyCollection (+2 more)

### Community 102 - "Common.Infrastructure.FeatureManagement"
Cohesion: 0.17
Nodes (8): Common.Infrastructure.FeatureManagement, ITargetingContextAccessor, HttpContextTargetingContextAccessor, ValueTask, Setup, IConfiguration, IServiceCollection, TargetingContext

### Community 103 - "JobMetricsFilter"
Cohesion: 0.25
Nodes (5): IServerFilter, PerformedContext, PerformingContext, JobMetricsFilter, string

### Community 104 - ".SendAsync"
Cohesion: 0.18
Nodes (8): CancellationToken, Task, IPushGateway, CancellationToken, ILogger, LoggerMessage, Task, DummyPushGateway

### Community 105 - "StronglyTypedIdListReadOnlyJsonConverter"
Cohesion: 0.36
Nodes (6): StronglyTypedIdListReadOnlyJsonConverter, IReadOnlyList, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 106 - "ProductTemplate"
Cohesion: 0.29
Nodes (5): IReadOnlyList, List, ProductTemplate, EntityTypeBuilder, ProductTemplateConfiguration

### Community 107 - "SeedingCompletionTracker"
Cohesion: 0.22
Nodes (5): SeedingCompletionTracker, CancellationToken, Exception, Task, TaskCompletionSource

### Community 108 - ".TryDeserialize"
Cohesion: 0.17
Nodes (4): StronglyTypedIdHelper, AuthenticationBuilder, IConfiguration, Setup

### Community 109 - ".IsRegisteredAsync"
Cohesion: 0.22
Nodes (6): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response

### Community 110 - "Stores/v1/Create/Request.cs"
Cohesion: 0.40
Nodes (4): Products.Endpoints.Stores.v1.Create, Request, RequestValidator, Response

### Community 111 - "IBackgroundJobs"
Cohesion: 0.26
Nodes (7): IBackgroundJobs, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 112 - "UserRegisteredSignalRHandler"
Cohesion: 0.28
Nodes (6): UserRegisteredIntegrationEvent, CancellationToken, ILogger, LoggerMessage, Task, UserRegisteredSignalRHandler

### Community 113 - "Endpoint"
Cohesion: 0.29
Nodes (5): Products.Endpoints.ProductTemplates, RouteGroupBuilder, Setup, RouteGroupBuilder, Endpoint

### Community 114 - "Refresh/Request.cs"
Cohesion: 0.33
Nodes (5): IAM.Endpoints.Tokens.VersionNeutral.Refresh, Request, RequestValidator, DateTimeOffset, Response

### Community 115 - "BackgroundJobsService"
Cohesion: 0.26
Nodes (7): BackgroundJobsService, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 116 - "BackgroundJobsModule"
Cohesion: 0.22
Nodes (6): BackgroundJobsModule, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection

### Community 117 - "ApiKeyAuthenticationHandler"
Cohesion: 0.23
Nodes (8): AuthenticateResult, AuthenticationHandler, AuthenticationProperties, AuthenticationSchemeOptions, ILogger, LoggerMessage, Task, ApiKeyAuthenticationHandler

### Community 118 - "IModule"
Cohesion: 0.11
Nodes (16): OpenTelemetryBuilder, ResourceBuilder, ICoreModule, IModule, Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder (+8 more)

### Community 119 - "ProductsModule"
Cohesion: 0.18
Nodes (8): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, ProductsModule

### Community 120 - "IAM.Infrastructure.Auth.ApiKey"
Cohesion: 0.17
Nodes (6): IAM.Infrastructure.Auth.ApiKey, string, ApiKeyDefaults, ApiKeyHasher, AuthenticationBuilder, Setup

### Community 121 - "IntegrationEvent"
Cohesion: 0.14
Nodes (10): Lock, IIntegrationEventOutbox, IntegrationEventOutbox, IReadOnlyList, List, IntegrationEvent, DateTimeOffset, DefaultIdType (+2 more)

### Community 122 - "TokenRefreshRateLimitingPolicy"
Cohesion: 0.25
Nodes (7): CancellationToken, Func, HttpContext, OnRejectedContext, RateLimitPartition, ValueTask, TokenRefreshRateLimitingPolicy

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
Cohesion: 0.33
Nodes (7): Exception, HttpContext, ILogger, LoggerMessage, RequestDelegate, Task, GlobalExceptionHandlingMiddleware

### Community 127 - "BaseDbContext"
Cohesion: 0.25
Nodes (6): DbContext, BaseDbContext, CancellationToken, DbSet, ModelConfigurationBuilder, Task

### Community 128 - "Common.Application.JsonConverters"
Cohesion: 0.12
Nodes (12): Common.Application.JsonConverters, DomainEventConverter, JsonSerializerOptions, EventConverter, JsonSerializerOptions, IntegrationEventConverter, JsonSerializerOptions, StronglyTypedIdValueConverter (+4 more)

### Community 129 - ".GetProductAsync"
Cohesion: 0.20
Nodes (7): Products.Endpoints.Products, RouteGroupBuilder, Setup, CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.48
Nodes (7): Observability (OpenTelemetry), docker-compose.yml (Base Stack), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), Postgres Service (mm.postgres), RabbitMQ Service (mm.rabbitmq), Redis Service (mm.redis)

### Community 131 - ".InvokeAsync"
Cohesion: 0.29
Nodes (5): IMiddleware, HttpContext, RequestDelegate, Task, EnrichLogsWithUserInfoMiddleware

### Community 132 - "SwaggerDefaultValues"
Cohesion: 0.33
Nodes (4): JsonValue, OpenApiOperation, OperationFilterContext, SwaggerDefaultValues

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

### Community 138 - "Common.Application.FeatureManagement"
Cohesion: 0.11
Nodes (12): Common.Application.FeatureManagement, IAM.Domain.Captcha, IAM.Infrastructure.Captcha.Services, IAM.Application.Captcha.Services, Common.InterModuleRequests.Notifications, IVariantFeatureManager, IVariantFeatureManagerExtensions, CancellationToken (+4 more)

### Community 139 - "My/Create/Request.cs"
Cohesion: 0.40
Nodes (4): Products.Endpoints.Stores.v1.My.Create, Request, RequestValidator, Response

### Community 140 - ".MapEndpoint"
Cohesion: 0.29
Nodes (4): IAM.Endpoints.Users.VersionNeutral, RouteGroupBuilder, RouteGroupBuilder, Setup

### Community 141 - "Infrastructure/Setup.cs"
Cohesion: 0.08
Nodes (15): Common.InterModuleRequests, Common.Endpoints.Versioning, Common.Infrastructure.Localization, Common.Infrastructure.Auth.Services, Common.Infrastructure.Caching, Common.Infrastructure.Auth, Setup, IServiceCollection (+7 more)

### Community 142 - ".ActivateProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 143 - "IDbContext"
Cohesion: 0.20
Nodes (8): ChangeTracker, DatabaseFacade, EntityEntry, IDisposable, IDbContext, CancellationToken, DbSet, Task

### Community 144 - "IIAMDbContext"
Cohesion: 0.14
Nodes (12): DbSet, IdentityRole, IdentityRoleClaim, IdentityUserClaim, IdentityUserLogin, IdentityUserRole, IdentityUserToken, IIAMDbContext (+4 more)

### Community 145 - ".FixedWindow"
Cohesion: 0.29
Nodes (5): RateLimitPartitions, HttpContext, RateLimitPartition, HttpContext, RateLimitPartition

### Community 146 - ".RevokeSession"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 147 - "Stores/v1/Update/Request.cs"
Cohesion: 0.40
Nodes (5): Products.Endpoints.Stores.v1.Update, RequestBody, Request, RequestBody, RequestValidator

### Community 148 - "Setup"
Cohesion: 0.07
Nodes (16): Common.Infrastructure.Modules, Host.Infrastructure, IHostBuilder, HealthCheckOptions, HealthCheckOptionsValidator, IApplicationBuilder, IConfiguration, IServiceCollection (+8 more)

### Community 149 - ".GetRoleIdByName"
Cohesion: 0.20
Nodes (8): CancellationToken, DefaultIdType, Task, IRoleService, CancellationToken, DefaultIdType, Task, RoleService

### Community 150 - "FeatureFlags"
Cohesion: 0.43
Nodes (6): Checkout, FeatureFlags, IAM, Notifications, Products, string

### Community 151 - "StronglyTypedIdSchemaFilter.cs"
Cohesion: 0.33
Nodes (4): IOpenApiSchema, ISchemaFilter, SchemaFilterContext, StronglyTypedIdSchemaFilter

### Community 152 - ".SendOtp"
Cohesion: 0.20
Nodes (7): CancellationToken, Task, CancellationToken, IFeatureManager, RouteGroupBuilder, Task, Endpoint

### Community 153 - "Setup"
Cohesion: 0.22
Nodes (5): IApplicationBuilder, IServiceCollection, IWebHostEnvironment, Type, Setup

### Community 154 - "IOperationFilter"
Cohesion: 0.33
Nodes (4): IOperationFilter, OpenApiOperation, OperationFilterContext, RemoveDefaultResponseSchemaFilter

### Community 155 - "VerifyPhoneOtpRequest"
Cohesion: 0.25
Nodes (7): OtpVerificationFailureReason, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, VerifyPhoneOtpResponseExtensions, CancellationToken, Task, VerifyPhoneOtpRequestHandler

### Community 156 - "Common.Application.ModelBinders"
Cohesion: 0.12
Nodes (14): Common.Application.ModelBinders, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, IModelBinder, ModelBindingContext, StronglyTypedIdBinder, Task, Request, RequestValidator (+6 more)

### Community 157 - ".SendOtp"
Cohesion: 0.29
Nodes (5): CancellationToken, IFeatureManager, RouteGroupBuilder, Task, Endpoint

### Community 158 - "RegisterRateLimitingPolicy"
Cohesion: 0.25
Nodes (7): CancellationToken, Func, HttpContext, OnRejectedContext, RateLimitPartition, ValueTask, RegisterRateLimitingPolicy

### Community 160 - "ValidationContextExtensions"
Cohesion: 0.40
Nodes (3): ValidationContextExtensions, string, ValidationContext

### Community 161 - "Stores/v1/My/Update/Request.cs"
Cohesion: 0.67
Nodes (3): Products.Endpoints.Stores.v1.My.Update, Request, RequestValidator

### Community 162 - "PermissionAuthorizationHandler"
Cohesion: 0.29
Nodes (6): AuthorizationHandler, AuthorizationHandlerContext, IAuthorizationRequirement, Task, PermissionAuthorizationHandler, PermissionRequirement

### Community 163 - "For"
Cohesion: 0.33
Nodes (3): CacheKeys, For, Guid

### Community 164 - "Host.Middlewares"
Cohesion: 0.22
Nodes (4): Host.Middlewares, HttpContext, Task, SecurityHeadersMiddleware

### Community 165 - ".GetMeAsync"
Cohesion: 0.12
Nodes (13): FrozenDictionary, IReadOnlySet, CustomPermissions, HashSet, IEnumerable, CurrentUser, ClaimsPrincipal, Guid (+5 more)

### Community 166 - "Endpoint"
Cohesion: 0.29
Nodes (5): IAM.Endpoints.Tokens.VersionNeutral, RouteGroupBuilder, Endpoint, RouteGroupBuilder, Setup

### Community 167 - "Common.Application.Pagination"
Cohesion: 0.07
Nodes (31): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Stores.v1.My.AuditLog, Common.Application.Pagination, Products.Endpoints.Products.v1.AuditLog, PaginationRequest, PaginationRequestValidator, int, PaginationQueryableExtensions (+23 more)

### Community 168 - "CachedCaptchaService"
Cohesion: 0.40
Nodes (3): CancellationToken, Task, CachedCaptchaService

### Community 169 - "ProductsModule.cs"
Cohesion: 0.10
Nodes (13): Products.Endpoints.Probe, Products.Infrastructure.RateLimiting, Products.Endpoints, IAssemblyReference, RouteGroupBuilder, Setup, RateLimiterOptions, Policies (+5 more)

### Community 171 - ".UpdateStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 172 - "BackgroundJobsTelemetry"
Cohesion: 0.25
Nodes (8): ConcurrentDictionary, BackgroundJobsTelemetry, ActivitySource, Counter, Histogram, Meter, ObservableGauge, string

### Community 173 - "Host.Swagger"
Cohesion: 0.40
Nodes (3): Host, Host.Swagger, Program

### Community 174 - "IAM.Application.Tokens.DTOs"
Cohesion: 0.29
Nodes (5): IAM.Application.Tokens.DTOs, DateTimeOffset, AccessTokenDto, DateTimeOffset, TokensDto

### Community 175 - ".GetAuditLogAsync"
Cohesion: 0.08
Nodes (18): AuditLogDto, DbContextExtensions, CancellationToken, DbSet, JsonSerializerOptions, Task, CancellationToken, RouteGroupBuilder (+10 more)

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

### Community 181 - "Setup.SignalR.cs"
Cohesion: 0.40
Nodes (3): IConfiguration, IServiceCollection, Setup

### Community 182 - "Constants"
Cohesion: 0.33
Nodes (4): IAM.Domain, string, Constants, IAssemblyReference

### Community 183 - "SendRequestBody"
Cohesion: 0.67
Nodes (3): SendMessageBody, IReadOnlyList, SendRequestBody

### Community 184 - "Setup"
Cohesion: 0.40
Nodes (3): IApplicationBuilder, IServiceCollection, Setup

### Community 185 - "IAMModule.cs"
Cohesion: 0.07
Nodes (18): IAM.Endpoints, IAM.Endpoints.Otp.VersionNeutral, IAM.Infrastructure.Tokens, IAM.Infrastructure.Tokens.Services, IAM.Infrastructure.Captcha, Action, IApplicationBuilder, IConfiguration (+10 more)

### Community 186 - "IEventHandler.cs"
Cohesion: 0.40
Nodes (3): IEventHandler, CancellationToken, Task

### Community 187 - "ReverseProxyOptions.cs"
Cohesion: 0.50
Nodes (3): ReverseProxyOptions, ReverseProxyOptionsValidator, IReadOnlyList

### Community 188 - "IAM.Infrastructure.Auth"
Cohesion: 0.24
Nodes (5): IAM.Infrastructure.Auth, ClaimsPrincipal, ClaimsPrincipalExtensions, string, MultiAuthDefaults

### Community 190 - "DefaultResponsesOperationFilter"
Cohesion: 0.70
Nodes (3): OpenApiOperation, OperationFilterContext, DefaultResponsesOperationFilter

### Community 191 - "Split-Deployment PoC"
Cohesion: 0.25
Nodes (7): Concurrent safety, Cross-process call path, Files added by this PoC, How it works, How to run, Split-Deployment PoC, What this proves

### Community 192 - "V1UserRegisteredDomainEvent"
Cohesion: 0.50
Nodes (4): CancellationToken, Task, V1UserRegisteredDomainEventHandler, V1UserRegisteredDomainEvent

### Community 193 - "V1SessionRevokedDomainEvent"
Cohesion: 0.50
Nodes (4): CancellationToken, Task, V1SessionRevokedDomainEventHandler, V1SessionRevokedDomainEvent

### Community 194 - "CorsOptions.cs"
Cohesion: 0.67
Nodes (3): CorsOptions, CorsOptionsValidator, IReadOnlyList

### Community 195 - "Setup"
Cohesion: 0.40
Nodes (3): IApplicationBuilder, IServiceCollection, Setup

### Community 196 - "JwtOptions"
Cohesion: 0.67
Nodes (3): JwtOptions, JwtOptionsValidator, IReadOnlyCollection

### Community 197 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.05
Nodes (36): Products.Endpoints.Probe.v1, Common.InterModuleRequests.IAM, Common.InterModuleRequests.Contracts, IAM.Infrastructure.InterModuleRequestHandlers, IConsumer, IInterModuleRequest, IInterModuleRequestHandler, CancellationToken (+28 more)

### Community 198 - ".AddCustomMassTransit"
Cohesion: 0.50
Nodes (3): Assembly, IConfiguration, IServiceCollection

### Community 200 - "NotificationsTelemetry"
Cohesion: 0.18
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

### Community 226 - "ModulesOptions.cs"
Cohesion: 0.67
Nodes (3): ModulesOptions, ModulesOptionsValidator, IReadOnlyList

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
- **103 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `SelfRegister/Endpoint.cs`, `Common.Infrastructure.Persistence.Auditing`, `CustomValidator`, `Common.Application.FeatureManagement`, `Common.Domain.ResultMonad`, `.RegisterAndLoginAsync`, `Infrastructure/Setup.cs`, `Products.Domain.ProductTemplates`, `Notifications.Application.Sms`, `Setup`, `IAM.Endpoints.Common.Validations`, `Notifications.Infrastructure.Telemetry`, `CustomRateLimitingOptions`, `Host.Middlewares`, `ObservabilityOptions`, `ProductsModule.cs`, `Host.Swagger`, `ApiKeysOptions.cs`, `CachingOptions.cs`, `SmsOptions.cs`, `Setup.SignalR.cs`, `IAMModule.cs`, `ReverseProxyOptions.cs`, `RequestLoggingOptions`, `CorsOptions.cs`, `OutboxModule`, `JwtOptions`, `Common.InterModuleRequests.Contracts`, `SignalROptions.cs`, `Common.Application.Validation`, `Common.Application.EventBus`, `AuditLogRetentionJobRegistrar`, `CaptchaOptions.cs`, `Common.Domain.StronglyTypedIds`, `ConfigureSwaggerOptions`, `.SearchMyProductsAsync`, `ModulesOptions.cs`, `DatabaseOptions.cs`, `.AddCommonOptions`, `Common.Application.BackgroundJobs`, `IModule`?**
  _High betweenness centrality (0.373) - this node is a cross-community bridge._
- **Why does `Result` connect `Result` to `.GetProductAsync`, `.RemoveProductAsync`, `.ListSessions`, `.SingleAsResult`, `FirebasePushGateway`, `Error`, `.RegisterAndLoginAsync`, `.ActivateProductTemplateAsync`, `IIAMDbContext`, `Notifications.Application.Sms`, `.RevokeSession`, `.SendOtp`, `.RevokeToken`, `NetGsmSmsGateway`, `VerifyPhoneOtpRequest`, `.SendOtp`, `v1/AddProduct/Request.cs`, `.GetMeAsync`, `.RefreshToken`, `CachedCaptchaService`, `.UpdateStoreAsync`, `ProductTemplateId`, `.GetAuditLogAsync`, `ICaptchaService`, `ReCaptchaService`, `.SendAsync`, `.AddProductToMyStoreAsync`, `.GetProductTemplateAsync`, `IProductsDbContext`, `.UpdateProductAsync`, `.GetStoreAsync`, `ResultTelemetryExtensions`, `.DeactivateProductTemplateAsync`, `.CreateTokens`, `.UpdateCurrentSessionPushToken`, `.SearchMyProductsAsync`, `.SendAsync`, `ProductTemplate`, `.IsRegisteredAsync`, `.TapWhenFeatureEnabledAsync`?**
  _High betweenness centrality (0.095) - this node is a cross-community bridge._
- **Why does `Setup` connect `Setup` to `Host.Middlewares`, `.AddCustomMassTransit`, `ObservabilityOptions`, `RequestResponseBodyLoggingMiddleware`, `Infrastructure/Setup.cs`, `.AddModules`, `IModule`, `.LogAssemblyLoadFailed`, `.LogHealthChecksRegistered`, `RequestLoggingOptions`, `.AddInfrastructure`?**
  _High betweenness centrality (0.082) - this node is a cross-community bridge._
- **What connects `OtpCacheEntry`, `Common.Domain`, `Common.Infrastructure` to the rest of the system?**
  _135 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `SelfRegister/Endpoint.cs` be split into smaller, more focused modules?**
  _Cohesion score 0.12418300653594772 - nodes in this community are weakly interconnected._
- **Should `ApplicationUserId` be split into smaller, more focused modules?**
  _Cohesion score 0.05271629778672032 - nodes in this community are weakly interconnected._
- **Should `IAMDbContext` be split into smaller, more focused modules?**
  _Cohesion score 0.125 - nodes in this community are weakly interconnected._