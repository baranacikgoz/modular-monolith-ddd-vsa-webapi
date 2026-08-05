# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-07-29)

## Corpus Check
- 463 files · ~67,082 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2982 nodes · 5248 edges · 321 communities (219 shown, 102 thin omitted)
- Extraction: 99% EXTRACTED · 1% INFERRED · 0% AMBIGUOUS · INFERRED: 77 edges (avg confidence: 0.8)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `56a0ee7c`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- Host Logging & Serilog Setup
- IAM User Identity & Auditing
- Products Store & Audit Services
- Notifications Dispatch & SignalR Client
- Modular Monolith Architecture Concepts
- Cross-Module Comm & Arch Audit Skills
- Domain Event Handling & Outbox Collect
- Host NuGet Deps (OTel/Health)
- k6 Load Test Scripts
- REPR Request Validators
- IAM Error Catalogs
- BackgroundJobs Service (Hangfire)
- IAM OTP Verify & Token Endpoint
- Project Files & Solution
- Localized Identity Errors
- Functional Result Extensions
- Launch Settings
- Module Installers (IModule)
- Host Infrastructure Setup
- IAM OTP Send & Captcha
- Authz Constants & Feature Flags
- Bounded Capture Streams
- SignalR Hub & Exception Middleware
- Telemetry (ActivitySource/Meter)
- Outbox Processor & Seeder
- EF Core DbContexts
- Integration Event Handler Base
- Product Template Aggregate
- Outbox Message & Tokens
- MassTransit & DI Setup
- PermissionAuthorizationHandler
- DbSet
- CustomRateLimitingOptions
- PaginationRequestValidator
- Microsoft.EntityFrameworkCore.Abstractions
- ISearchLocalized
- StoreConfiguration
- Setup
- Hangfire.PostgreSql
- EventDispatcher
- NetArchTest.Rules
- Aigamo.ResXGenerator
- IOperationFilter
- IRateLimiterPolicy
- Seeder
- AggregateRoot
- ApiVersionSet
- Outbox Misuse Check
- IntegrationTestFactory
- Add Integration Event Command
- Asp.Versioning.Http
- IInterModuleRequestHandler
- double
- RouteHandlerBuilderExtensions
- IList
- Microsoft.AspNetCore.SignalR.StackExchangeRedis
- DummyOtpService
- Cross-Module Reference Violation
- CollectionExtensions
- coverlet.collector
- Bogus Test Data
- decimal
- EndpointFilterDelegate
- Hangfire
- Seeder
- ApplyAuditingInterceptor
- CustomRoles
- AuthenticateResult
- ValueObject
- IResxLocalizer
- OutboxModule
- V1ProductCreatedDomainEvent
- IAggregateRoot
- ApiVersionDescription
- AsNoTracking Coverage Check
- GetSeedUserIdsRequest
- HostCollection
- Microsoft.AspNetCore.Identity.EntityFrameworkCore
- RequestLoggingPathPostConfigure
- Endpoint
- BaseIntegrationTest
- HttpContextExtensions
- net10.0
- IServiceProvider
- enabledManagers
- Activity
- AuditLogRetentionJobRegistrar
- AuditLogRetentionService
- Configuration-Driven Module Registration
- ConfigurationManager
- HostTestFactory
- HttpClient
- IntegrationTestWebAppFactory
- IOpenApiSchema
- OtpServiceBase
- .SendAsync
- OutboxCleanupSettings
- OutboxTestWebAppFactory
- IDatabaseSeeder
- HangfireCustomAuthorizationFilter
- IdentityResultExtensions
- Setup.GlobalExceptionHandlingMiddleware.cs
- HttpContextTargetingContextAccessor
- IVariantFeatureManagerExtensions
- IMiddleware
- IModelBinder
- .InvokeAsync
- ProductsModule
- ReverseProxyOptions.cs
- RequestBody
- JobTargets
- CacheKeys
- StringExtensions
- ReverseProxyOptions
- OutboxMetricsJob
- SearchLanguageResolver
- IRoleService
- .SendAsync
- RequestBody
- RequestBody
- Request.cs
- My/Search/Request.cs
- Setup
- Setup
- Setup
- Endpoint
- Setup
- AuditLogEntry
- Endpoint
- Setup
- Setup
- Setup
- .RemoveMyProductAsync
- Setup
- .RemoveProductAsync
- HostEnvironmentExtensions.cs
- BackgroundJobsOptions
- CaptchaOptions
- IAM.Endpoints.Otp.VersionNeutral
- .GetMyStoreAsync
- Users/VersionNeutral/Setup.cs
- Setup
- ModulesOptions
- OtpPurposes.cs
- OtpOptions
- .AddOrUpdate
- SecurityHeadersOptions
- SignalROptions
- Setup
- AutoMigrateMarker
- Setup
- .GetMeAsync
- .UpdateStoreAsync
- Endpoint
- Host.Swagger
- VerifyPhoneOtpRequest
- CurrentUser
- Setup
- Endpoint
- IAM.Endpoints
- ValidationContextExtensions
- Endpoint
- .ForUser
- Endpoint
- Endpoint
- Endpoint
- Setup
- Setup
- Endpoint
- .Get
- v1/Request.cs
- Endpoint
- Endpoint
- Endpoint
- Endpoint
- Endpoint
- FeatureManagement/RouteHandlerBuilderExtensions.cs
- SendRequestBody
- Endpoint
- Endpoint
- SmsOptions.cs
- HttpContextExtensions.cs
- Endpoint
- Auditing/Setup.cs
- ReCaptchaResponse
- Endpoint
- Endpoint
- Endpoint
- DummyOtpService
- ISearchLanguageResolver
- .AddCustomMassTransit
- Setup
- .UpdateMyProductAsync
- .RemoveMyProductAsync
- .UpdateMyStoreAsync
- ProductTemplates/Setup.cs
- Response
- IInterModuleRequestHandler
- Revoke/Request.cs
- .ToResult
- JwtOptions
- SecurityHeadersOptions.cs
- HttpContextExtensions.cs
- IAssemblyReference
- HealthCheckOptions.cs
- Request.cs
- IAssemblyReference
- Setup
- .SendOtp
- Stores/v1/Update/Request.cs
- TokenService.cs
- IAssemblyReference
- PaginationRequestValidator
- IAssemblyReference
- AuditLogEntry
- .DeactivateProductTemplateAsync
- Products.Endpoints.Probe
- PushOptions.cs
- Stores/v1/My/Update/Request.cs
- .RevokeAllSessions
- Host.Swagger
- Response
- Deactivate/Request.cs
- My/RemoveProduct/Request.cs
- UpdatePushToken/Request.cs
- Products.Endpoints.Probe
- ModulesOptions.cs
- ResxLocalizationOptions.cs
- IAM
- IntegrationEvent
- ReCaptchaResponse
- AccessTokenDto
- OtpOptions.cs
- OtpPurposes.cs
- .AddServices
- Stores/v1/Create/Request.cs
- Response
- Response
- Response
- Response
- Response
- Response
- Response
- Response
- Response
- Response
- V1ProductQuantityDecreasedDomainEvent
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
8. `ApplicationUserId` - 51 edges
9. `Common.Application.Extensions` - 49 edges
10. `Common.Domain.Events` - 42 edges

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

## Communities (321 total, 102 thin omitted)

### Community 0 - "Host Logging & Serilog Setup"
Cohesion: 0.13
Nodes (12): IAM.Application.Tokens.Services, IAM.Application.Extensions, IAM.Endpoints.Otp, IAM.Domain.Identity, IAM.Endpoints.Tokens.VersionNeutral.Revoke, IAM.Infrastructure.Telemetry, Common.Domain.ResultMonad, IAM.Application.Persistence (+4 more)

### Community 1 - "IAM User Identity & Auditing"
Cohesion: 0.11
Nodes (21): IEntityTypeConfiguration, ApplicationUserId, DefaultIdType, DateTimeOffset, ApplicationUser, EntityTypeBuilder, IdentityRole, IdentityRoleClaim (+13 more)

### Community 2 - "Products Store & Audit Services"
Cohesion: 0.11
Nodes (18): BackgroundService, DatabaseSeederOrchestrator, CancellationToken, ILogger, LoggerMessage, Task, SeedingCompletionTracker, CancellationToken (+10 more)

### Community 3 - "Notifications Dispatch & SignalR Client"
Cohesion: 0.12
Nodes (13): IdentityDbContext, CancellationToken, DbSet, IdentityRole, IdentityRoleClaim, IdentityUserClaim, IdentityUserLogin, IdentityUserRole (+5 more)

### Community 5 - "Cross-Module Comm & Arch Audit Skills"
Cohesion: 0.26
Nodes (6): Guid, DateTimeOffset, Guid, IReadOnlyCollection, List, Session

### Community 6 - "Domain Event Handling & Outbox Collect"
Cohesion: 0.11
Nodes (17): IConnectionMultiplexer, RateLimiter, RateLimiterStatistics, RateLimitLease, FixedWindowLease, RedisFixedWindowRateLimiter, bool, CancellationToken (+9 more)

### Community 7 - "Host NuGet Deps (OTel/Health)"
Cohesion: 0.12
Nodes (10): Common.Application.FeatureManagement, IAM.Domain.Captcha, IAM.Infrastructure.Captcha.Services, IAM.Application.Captcha.Services, Common.Infrastructure.Resiliency, FeatureFlagResultExtensions, RouteHandlerBuilderExtensions, RouteHandlerBuilder (+2 more)

### Community 8 - "k6 Load Test Scripts"
Cohesion: 0.16
Nodes (13): FirebaseApp, FirebaseMessaging, PushMessage, CancellationToken, Exception, IEnumerable, ILogger, int (+5 more)

### Community 9 - "REPR Request Validators"
Cohesion: 0.22
Nodes (12): Products.Endpoints.Products.v1.My.Update, OpenApiOptions, OpenApiOptionsValidator, CustomValidator, RequestBody, Request, RequestBody, RequestBodyValidator (+4 more)

### Community 10 - "IAM Error Catalogs"
Cohesion: 0.12
Nodes (11): HttpStatusCode, IStringLocalizer, StringLocalizerExtensions, Error, ICollection, IResult, ICollection, IdentityErrors (+3 more)

### Community 11 - "BackgroundJobs Service (Hangfire)"
Cohesion: 0.17
Nodes (10): Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Products.Domain.Products, Products.Infrastructure.Telemetry, Products.Application.Persistence, Products.Domain.Stores, Common.Application.Pagination (+2 more)

### Community 12 - "IAM OTP Verify & Token Endpoint"
Cohesion: 0.13
Nodes (19): IAM.Endpoints.Users.VersionNeutral.SelfRegister, IInterModuleRequestClient, CancellationToken, Task, ITokenService, CancellationToken, HttpContext, IFeatureManager (+11 more)

### Community 13 - "Project Files & Solution"
Cohesion: 0.39
Nodes (5): SendPhoneOtpRequest, SendPhoneOtpResponse, CancellationToken, Task, SendPhoneOtpRequestHandler

### Community 14 - "Localized Identity Errors"
Cohesion: 0.11
Nodes (6): IAM.Infrastructure.Identity, IdentityError, IdentityErrorDescriber, LocalizedIdentityErrorDescriber, IServiceCollection, Setup

### Community 15 - "Functional Result Extensions"
Cohesion: 0.18
Nodes (6): Common.Domain.StronglyTypedIds, IAM.Domain.Identity.DomainEvents.v1, Common.Domain.Events, IAM.Domain.Identity.Sessions, Common.Domain.Entities, Common.Domain.Aggregates

### Community 16 - "Launch Settings"
Cohesion: 0.13
Nodes (13): CancellationToken, Task, ISmsGateway, SmsMessage, CancellationToken, ILogger, LoggerMessage, Task (+5 more)

### Community 17 - "Module Installers (IModule)"
Cohesion: 0.13
Nodes (11): RecurringJobOptions, IRecurringBackgroundJobs, Action, Expression, Func, Task, RecurringBackgroundJobsService, Action (+3 more)

### Community 18 - "Host Infrastructure Setup"
Cohesion: 0.08
Nodes (26): Products.Domain.Products.DomainEvents.v1, AggregateRoot, IEnumerable, IReadOnlyCollection, List, IAggregateRoot, IEnumerable, IReadOnlyCollection (+18 more)

### Community 19 - "IAM OTP Send & Captcha"
Cohesion: 0.13
Nodes (12): CancellationToken, ILogger, LoggerMessage, Task, ActivitySource, Counter, Histogram, long (+4 more)

### Community 20 - "Authz Constants & Feature Flags"
Cohesion: 0.09
Nodes (18): IAM.Endpoints.Otp.VersionNeutral.SendForLogin, IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, IAM.Endpoints.Common.Validations, IResxLocalizer, IRuleBuilder, IRuleBuilderOptions, CommonValidations, IResxLocalizer (+10 more)

### Community 21 - "Bounded Capture Streams"
Cohesion: 0.12
Nodes (8): byte, ReadOnlySpan, SeekOrigin, bool, int, BoundedCaptureStream, BoundedRequestCaptureStream, Stream

### Community 22 - "SignalR Hub & Exception Middleware"
Cohesion: 0.08
Nodes (16): Common.Application.Persistence, IAM.Infrastructure.Persistence, IAM.Infrastructure.Persistence.Seeding, IDatabaseSeeder, CancellationToken, Task, CancellationToken, Task (+8 more)

### Community 23 - "Telemetry (ActivitySource/Meter)"
Cohesion: 0.32
Nodes (3): Common.Application.BackgroundJobs, BackgroundJobs.Telemetry, BackgroundJobs

### Community 24 - "Outbox Processor & Seeder"
Cohesion: 0.12
Nodes (14): IAM.Endpoints.Tokens.VersionNeutral.Refresh, CancellationToken, HttpContext, ILogger, IOptions, LoggerMessage, RouteGroupBuilder, Task (+6 more)

### Community 25 - "EF Core DbContexts"
Cohesion: 0.15
Nodes (12): SendMessageBody, SendRequestBody, SmsCategory, CancellationToken, IReadOnlyList, JsonSerializerOptions, string, Task (+4 more)

### Community 26 - "Integration Event Handler Base"
Cohesion: 0.18
Nodes (8): Hub, NotificationGroupName, Exception, ILogger, LoggerMessage, string, Task, NotificationsHub

### Community 27 - "Product Template Aggregate"
Cohesion: 0.22
Nodes (6): Products.Endpoints.Products.v1.My.Get, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response

### Community 28 - "Outbox Message & Tokens"
Cohesion: 0.40
Nodes (3): ProblemDetails, ProblemDetailsExtensions, ICollection

### Community 29 - "MassTransit & DI Setup"
Cohesion: 0.14
Nodes (11): Result, Func, Task, AsyncExtensions, SyncExtensions, Action, Func, Task (+3 more)

### Community 30 - "PermissionAuthorizationHandler"
Cohesion: 0.22
Nodes (6): IAM.Infrastructure.Auth.Jwt, IAM.Infrastructure.Auth.Services, IAM.Application.Auth.Services, IConfiguration, IServiceCollection, Setup

### Community 31 - "DbSet"
Cohesion: 0.14
Nodes (15): LoadAll, Names, Assembly, Exception, IApplicationBuilder, IConfiguration, IEnumerable, ILogger (+7 more)

### Community 32 - "CustomRateLimitingOptions"
Cohesion: 0.25
Nodes (5): Products.Endpoints.Stores, RouteGroupBuilder, Setup, RouteGroupBuilder, Endpoint

### Community 33 - "PaginationRequestValidator"
Cohesion: 0.29
Nodes (6): Products.Endpoints.Stores.v1.AddProduct, RequestBody, Request, RequestBody, RequestValidator, Response

### Community 34 - "Microsoft.EntityFrameworkCore.Abstractions"
Cohesion: 0.06
Nodes (28): Products.Endpoints.Probe.v1, IConsumer, IInterModuleRequest, IInterModuleRequestHandler, CancellationToken, Task, InterModuleRequestHandler, CancellationToken (+20 more)

### Community 35 - "ISearchLocalized"
Cohesion: 0.09
Nodes (16): CancellationToken, Task, IOutboxMessage, DateTimeOffset, OutboxMessage, DateTimeOffset, TimeSpan, IEvent (+8 more)

### Community 36 - "StoreConfiguration"
Cohesion: 0.26
Nodes (8): CustomRateLimitingOptions, CustomRateLimitingOptionsValidator, FixedWindow, FixedWindowValidator, Action, IEnumerable, RateLimiterOptions, Policies

### Community 37 - "Setup"
Cohesion: 0.10
Nodes (16): IEnumerable, IReadOnlyCollection, List, ApplicationUser, Task, IdentityRole, ILogger, LoggerMessage (+8 more)

### Community 38 - "Hangfire.PostgreSql"
Cohesion: 0.20
Nodes (7): PathString, IApplicationBuilder, HttpContext, IList, RequestDelegate, string, RequestResponseBodyLoggingMiddleware

### Community 39 - "EventDispatcher"
Cohesion: 0.17
Nodes (9): int, Constants, CancellationToken, IOptions, RouteGroupBuilder, Task, Endpoint, Request (+1 more)

### Community 40 - "NetArchTest.Rules"
Cohesion: 0.29
Nodes (7): IRateLimiterPolicy, CancellationToken, Func, OnRejectedContext, ValueTask, CheckRegistrationRateLimitingPolicy, Policies

### Community 41 - "Aigamo.ResXGenerator"
Cohesion: 0.16
Nodes (11): Products.Endpoints.Stores.v1.Search, Products.Endpoints.Products.v1.Search, Common.Application.DTOs, Products.Endpoints.Products.v1.My.Search, Products.Endpoints.Products.v1.Get, AuditableEntityResponse, DateTimeOffset, Response (+3 more)

### Community 42 - "IOperationFilter"
Cohesion: 0.11
Nodes (20): EventDispatcher, ActivitySource, CancellationToken, ILogger, LoggerMessage, Task, BaseDbContext, CancellationToken (+12 more)

### Community 43 - "IRateLimiterPolicy"
Cohesion: 0.15
Nodes (10): CancellationToken, Func, HttpContext, OnRejectedContext, RateLimiterOptions, RateLimitPartition, TimeSpan, ValueTask (+2 more)

### Community 44 - "Seeder"
Cohesion: 0.23
Nodes (5): ISearchLocalized, Product, IReadOnlyCollection, List, Store

### Community 45 - "AggregateRoot"
Cohesion: 0.15
Nodes (11): ProductTemplateId, CancellationToken, Task, CancellationToken, List, Task, Seeder, CancellationToken (+3 more)

### Community 46 - "ApiVersionSet"
Cohesion: 0.29
Nodes (4): ApiVersionSet, Setup, IEndpointRouteBuilder, IServiceCollection

### Community 48 - "IntegrationTestFactory"
Cohesion: 0.12
Nodes (12): Notifications.Application.Otp, Notifications.Application.Sms, Notifications.Infrastructure.Sms, Notifications.Infrastructure.Telemetry, Notifications.Infrastructure.InterModuleRequestHandlers, Notifications.Infrastructure.Sms.NetGsm, Notifications.Infrastructure, Common.InterModuleRequests.Notifications (+4 more)

### Community 50 - "Asp.Versioning.Http"
Cohesion: 0.29
Nodes (8): IntegrationEventHandlerBase, CancellationToken, ConsumeContext, DefaultIdType, ILogger, LoggerMessage, Task, TimeSpan

### Community 51 - "IInterModuleRequestHandler"
Cohesion: 0.25
Nodes (5): CancellationToken, Task, CancellationToken, Task, CachedCaptchaService

### Community 52 - "double"
Cohesion: 0.22
Nodes (9): double, FormUrlEncodedContent, ReCaptchaResponse, CancellationToken, Exception, ILogger, LoggerMessage, Task (+1 more)

### Community 53 - "RouteHandlerBuilderExtensions"
Cohesion: 0.22
Nodes (7): StoreId, ILogger, LoggerMessage, CancellationToken, List, Task, Seeder

### Community 54 - "IList"
Cohesion: 0.33
Nodes (5): HttpClient, HttpStandardResilienceOptions, IHttpClientBuilder, Action, IServiceCollection

### Community 55 - "Microsoft.AspNetCore.SignalR.StackExchangeRedis"
Cohesion: 0.22
Nodes (8): CancellationToken, Func, HttpContext, OnRejectedContext, RateLimitPartition, ValueTask, Policies, RegisterRateLimitingPolicy

### Community 56 - "DummyOtpService"
Cohesion: 0.29
Nodes (5): CancellationToken, IFeatureManager, RouteGroupBuilder, Task, Endpoint

### Community 58 - "CollectionExtensions"
Cohesion: 0.06
Nodes (31): JsonConverter, StrictDateTimeOffsetJsonConverter, DateTimeOffset, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, StronglyTypedIdListReadOnlyJsonConverter (+23 more)

### Community 59 - "coverlet.collector"
Cohesion: 0.14
Nodes (7): Notifications.Infrastructure.Hubs, Notifications.Application.Hubs, Task, INotificationsClient, IConfiguration, IServiceCollection, Setup

### Community 61 - "decimal"
Cohesion: 0.14
Nodes (11): Products.Endpoints.Stores.v1.My.AddProduct, decimal, int, Constants, CancellationToken, RouteGroupBuilder, Task, Endpoint (+3 more)

### Community 62 - "EndpointFilterDelegate"
Cohesion: 0.10
Nodes (16): Common.Application.EndpointFilters, IEndpointFilter, ResultToCreatedResponseTransformer, ResultToResponseTransformer, EndpointFilterDelegate, EndpointFilterInvocationContext, ValueTask, RouteHandlerBuilderExtensions (+8 more)

### Community 63 - "Hangfire"
Cohesion: 0.33
Nodes (4): IVariantFeatureManager, IVariantFeatureManagerExtensions, CancellationToken, Task

### Community 64 - "Seeder"
Cohesion: 0.22
Nodes (6): BackgroundJobsModule, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection

### Community 65 - "ApplyAuditingInterceptor"
Cohesion: 0.14
Nodes (11): SaveChangesInterceptor, ApplyAuditingInterceptor, CancellationToken, DbContextEventData, InterceptionResult, ValueTask, ApplySearchLanguageInterceptor, CancellationToken (+3 more)

### Community 66 - "CustomRoles"
Cohesion: 0.20
Nodes (7): Common.Infrastructure.Persistence, Common.Infrastructure.Localization, Common.Infrastructure.EventBus, Setup, IServiceCollection, AutoMigrateMarker, IAutoMigrateMarker

### Community 67 - "AuthenticateResult"
Cohesion: 0.22
Nodes (6): Products.Endpoints.ProductTemplates.v1.Get, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response

### Community 68 - "ValueObject"
Cohesion: 0.27
Nodes (4): Common.Domain, IComparable, ValueObject, IEnumerable

### Community 69 - "IResxLocalizer"
Cohesion: 0.11
Nodes (17): DomainEventHandlerBase, IEventHandler, CancellationToken, Task, IEventHandlerWrapper, CancellationToken, Task, CancellationToken (+9 more)

### Community 70 - "OutboxModule"
Cohesion: 0.14
Nodes (11): Products.Endpoints.ProductTemplates.v1.Search, int, Constants, CancellationToken, IOptions, RouteGroupBuilder, Task, Endpoint (+3 more)

### Community 71 - "V1ProductCreatedDomainEvent"
Cohesion: 0.08
Nodes (25): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Add a new language/culture, Add search to a new entity _(Build checklist)_ (+17 more)

### Community 72 - "IAggregateRoot"
Cohesion: 0.20
Nodes (8): Products.Endpoints.Stores.v1.My.Create, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator, Response

### Community 73 - "ApiVersionDescription"
Cohesion: 0.22
Nodes (8): CancellationToken, Func, HttpContext, OnRejectedContext, RateLimitPartition, ValueTask, Policies, SmsRateLimitingPolicy

### Community 75 - "GetSeedUserIdsRequest"
Cohesion: 0.14
Nodes (13): Products.Endpoints.Products.v1.Update, Products.Endpoints.Stores.v1.RemoveProduct, ProductId, Request, RequestValidator, Request, RequestValidator, RequestBody (+5 more)

### Community 76 - "HostCollection"
Cohesion: 0.67
Nodes (3): CorsOptions, CorsOptionsValidator, IReadOnlyList

### Community 77 - "Microsoft.AspNetCore.Identity.EntityFrameworkCore"
Cohesion: 0.13
Nodes (13): DbSet, IdentityRole, IdentityRoleClaim, IdentityUserClaim, IdentityUserLogin, IdentityUserRole, IdentityUserToken, IIAMDbContext (+5 more)

### Community 78 - "RequestLoggingPathPostConfigure"
Cohesion: 0.22
Nodes (8): CancellationToken, Func, HttpContext, OnRejectedContext, RateLimitPartition, ValueTask, Policies, TokenCreateRateLimitingPolicy

### Community 79 - "Endpoint"
Cohesion: 0.09
Nodes (18): Common.IntegrationEvents, Notifications.Application.IntegrationEventHandlers, IAM.Application.Users.DomainEventHandlers.v1, Common.Application.EventBus, Lock, IIntegrationEventOutbox, IntegrationEventOutbox, List (+10 more)

### Community 80 - "BaseIntegrationTest"
Cohesion: 0.22
Nodes (9): IPostConfigureOptions, RequestLoggingOptions, RequestLoggingOptionsValidator, SensitivePathRule, IList, int, IServiceCollection, RequestLoggingPathPostConfigure (+1 more)

### Community 81 - "HttpContextExtensions"
Cohesion: 0.24
Nodes (8): Action, Exception, IApplicationBuilder, IEndpointRouteBuilder, IEnumerable, ILogger, LoggerMessage, OutboxModule

### Community 82 - "net10.0"
Cohesion: 0.29
Nodes (3): Common.Domain.Extensions, SearchValues, StringExtensions

### Community 83 - "IServiceProvider"
Cohesion: 0.44
Nodes (4): IServiceProvider, MigrationGuard, ILogger, LoggerMessage

### Community 84 - "enabledManagers"
Cohesion: 0.25
Nodes (6): AuditableEntityConfiguration, EntityTypeBuilder, EntityTypeBuilder, ProductConfiguration, EntityTypeBuilder, StoreConfiguration

### Community 85 - "Activity"
Cohesion: 0.38
Nodes (4): Activity, ResultTelemetryExtensions, ActivitySource, Task

### Community 86 - "AuditLogRetentionJobRegistrar"
Cohesion: 0.05
Nodes (28): Products.Infrastructure.Persistence, Common.Infrastructure.Persistence.Auditing, Common.Infrastructure.Persistence.AuditLog, Common.Infrastructure.Persistence.DbContext, IHostedService, Setup, IServiceCollection, AuditLogRetentionJobRegistrar (+20 more)

### Community 87 - "AuditLogRetentionService"
Cohesion: 0.22
Nodes (8): CancellationToken, Func, HttpContext, OnRejectedContext, RateLimitPartition, ValueTask, Policies, TokenRefreshRateLimitingPolicy

### Community 89 - "ConfigurationManager"
Cohesion: 0.33
Nodes (4): ConfigurationManager, Host.Configurations, Setup, WebApplicationBuilder

### Community 90 - "HostTestFactory"
Cohesion: 0.13
Nodes (13): IAM.Endpoints.Tokens.VersionNeutral.Create, accessToken, DateTimeOffset, expiresAt, ICollection, refreshTokenBytes, CancellationToken, HttpContext (+5 more)

### Community 91 - "HttpClient"
Cohesion: 0.31
Nodes (6): CaptchaOptions, CaptchaOptionsValidator, CaptchaProvider, IConfiguration, IServiceCollection, Setup

### Community 92 - "IntegrationTestWebAppFactory"
Cohesion: 0.17
Nodes (7): Products.Application.Stores.DomainEventHandlers.v1, Products.Domain.Stores.DomainEvents.v1, V1ProductAddedToStoreDomainEvent, V1ProductRemovedFromStoreDomainEvent, V1StoreAddressUpdatedDomainEvent, V1StoreDescriptionUpdatedDomainEvent, V1StoreNameUpdatedDomainEvent

### Community 93 - "IOpenApiSchema"
Cohesion: 0.17
Nodes (10): FullTextSearchOptions, FullTextSearchOptionsValidator, Dictionary, IReadOnlyList, string, CancellationToken, IOptions, RouteGroupBuilder (+2 more)

### Community 94 - "OtpServiceBase"
Cohesion: 0.06
Nodes (25): Common.Application.Caching, Notifications.Infrastructure.Otp, SemaphoreSlim, CacheKeys, For, OtpCacheEntry, CancellationToken, Task (+17 more)

### Community 95 - ".SendAsync"
Cohesion: 0.30
Nodes (5): DateTimeOffset, RefreshToken, RefreshTokenId, EntityTypeBuilder, RefreshTokenConfig

### Community 96 - "OutboxCleanupSettings"
Cohesion: 0.18
Nodes (8): CancellationToken, Task, IPushGateway, CancellationToken, ILogger, LoggerMessage, Task, DummyPushGateway

### Community 97 - "OutboxTestWebAppFactory"
Cohesion: 0.32
Nodes (5): ApiVersionDescription, IConfigureOptions, OpenApiInfo, ConfigureSwaggerOptions, SwaggerGenOptions

### Community 98 - "IDatabaseSeeder"
Cohesion: 0.22
Nodes (7): CancellationToken, IOptions, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator

### Community 99 - "HangfireCustomAuthorizationFilter"
Cohesion: 0.15
Nodes (7): DashboardContext, IDashboardAsyncAuthorizationFilter, CustomPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder, HangfireCustomAuthorizationFilter, Task

### Community 100 - "IdentityResultExtensions"
Cohesion: 0.22
Nodes (7): CancellationToken, IOptions, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator

### Community 101 - "Setup.GlobalExceptionHandlingMiddleware.cs"
Cohesion: 0.20
Nodes (8): IdentityUser, DateOnly, DateTimeOffset, IReadOnlyCollection, List, ApplicationUser, SessionRevokedReason, Uri

### Community 102 - "HttpContextTargetingContextAccessor"
Cohesion: 0.17
Nodes (8): Common.Infrastructure.FeatureManagement, ITargetingContextAccessor, HttpContextTargetingContextAccessor, ValueTask, Setup, IConfiguration, IServiceCollection, TargetingContext

### Community 103 - "IVariantFeatureManagerExtensions"
Cohesion: 0.43
Nodes (6): Checkout, FeatureFlags, IAM, Notifications, Products, string

### Community 104 - "IMiddleware"
Cohesion: 0.18
Nodes (8): Products.Endpoints.Stores.v1.AuditLog, AuditLogDto, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator

### Community 105 - "IModelBinder"
Cohesion: 0.15
Nodes (11): IFusionCache, JwtOptions, JwtOptionsValidator, IReadOnlyCollection, CancellationToken, HttpContext, IOptions, RouteGroupBuilder (+3 more)

### Community 106 - ".InvokeAsync"
Cohesion: 0.29
Nodes (5): IMiddleware, HttpContext, RequestDelegate, Task, EnrichLogsWithUserInfoMiddleware

### Community 107 - "ProductsModule"
Cohesion: 0.23
Nodes (6): Products.Infrastructure.Persistence.Seeding, Common.InterModuleRequests.IAM, Common.InterModuleRequests.Contracts, IAM.Infrastructure.InterModuleRequestHandlers, int, Seeder

### Community 108 - "ReverseProxyOptions.cs"
Cohesion: 0.15
Nodes (4): StronglyTypedIdHelper, SessionId, EntityTypeBuilder, SessionConfig

### Community 109 - "RequestBody"
Cohesion: 0.20
Nodes (8): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator, Response

### Community 110 - "JobTargets"
Cohesion: 0.33
Nodes (4): JsonValue, OpenApiOperation, OperationFilterContext, SwaggerDefaultValues

### Community 111 - "CacheKeys"
Cohesion: 0.26
Nodes (7): IBackgroundJobs, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 112 - "StringExtensions"
Cohesion: 0.23
Nodes (6): Outbox, Common.Infrastructure.Persistence.Outbox, Outbox.Persistence, Common.Application.Persistence.Outbox, Outbox.Telemetry, OutboxMetricsJob

### Community 113 - "ReverseProxyOptions"
Cohesion: 0.28
Nodes (6): Products.Endpoints.ProductTemplates.v1.Create, CancellationToken, Task, Request, RequestValidator, Response

### Community 114 - "OutboxMetricsJob"
Cohesion: 0.27
Nodes (8): OpenTelemetryBuilder, ResourceBuilder, Action, IConfiguration, IHostEnvironment, IReadOnlyList, IServiceCollection, Setup

### Community 115 - "SearchLanguageResolver"
Cohesion: 0.26
Nodes (7): BackgroundJobsService, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 116 - "IRoleService"
Cohesion: 0.17
Nodes (9): ICoreModule, IModule, Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection (+1 more)

### Community 117 - ".SendAsync"
Cohesion: 0.23
Nodes (8): AuthenticateResult, AuthenticationHandler, AuthenticationProperties, AuthenticationSchemeOptions, ILogger, LoggerMessage, Task, ApiKeyAuthenticationHandler

### Community 118 - "RequestBody"
Cohesion: 0.15
Nodes (10): IHostBuilder, KeyValuePair, LoggerConfiguration, LoggerMinimumLevelConfiguration, ObservabilityOptions, ObservabilityOptionsValidator, Dictionary, IEnumerable (+2 more)

### Community 119 - "RequestBody"
Cohesion: 0.18
Nodes (8): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, ProductsModule

### Community 120 - "Request.cs"
Cohesion: 0.17
Nodes (6): IAM.Infrastructure.Auth.ApiKey, string, ApiKeyDefaults, ApiKeyHasher, AuthenticationBuilder, Setup

### Community 121 - "My/Search/Request.cs"
Cohesion: 0.33
Nodes (4): IOpenApiSchema, ISchemaFilter, SchemaFilterContext, StronglyTypedIdSchemaFilter

### Community 122 - "Setup"
Cohesion: 0.33
Nodes (4): IOperationFilter, OpenApiOperation, OperationFilterContext, RemoveDefaultResponseSchemaFilter

### Community 123 - "Setup"
Cohesion: 0.22
Nodes (6): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, NotificationsModule

### Community 124 - "Setup"
Cohesion: 0.18
Nodes (8): IApplicationBuilder, IConfiguration, ILogger, IServiceCollection, LoggerMessage, string, WebApplication, Setup

### Community 125 - "Endpoint"
Cohesion: 0.40
Nodes (4): Action, Func, IFeatureManager, Task

### Community 126 - "Setup"
Cohesion: 0.33
Nodes (7): Exception, HttpContext, ILogger, LoggerMessage, RequestDelegate, Task, GlobalExceptionHandlingMiddleware

### Community 128 - "Endpoint"
Cohesion: 0.09
Nodes (17): Common.Application.JsonConverters, IAM.Infrastructure.Persistence.EntityConfigurations, Common.Infrastructure.Persistence.EntityConfigurations, Common.Infrastructure.Persistence.ValueConverters, DomainEventConverter, JsonSerializerOptions, EventConverter, JsonSerializerOptions (+9 more)

### Community 129 - "Setup"
Cohesion: 0.18
Nodes (7): Products.Endpoints.Products, RouteGroupBuilder, Setup, CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 130 - "Setup"
Cohesion: 0.16
Nodes (17): Configuration-Driven Module Loading, IntegrationEvents (Async Cross-Module), IAM Module, Notifications Module, Products Module, Observability (OpenTelemetry), docker-compose.yml (Base Stack), docker-compose.app.yml (App-Only) (+9 more)

### Community 131 - "Setup"
Cohesion: 0.70
Nodes (4): OutboxCleanupSettings, OutboxCleanupSettingsValidator, OutboxOptions, OutboxOptionsValidator

### Community 132 - ".RemoveMyProductAsync"
Cohesion: 0.29
Nodes (5): RateLimitPartitions, HttpContext, RateLimitPartition, HttpContext, RateLimitPartition

### Community 134 - ".RemoveProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 135 - "HostEnvironmentExtensions.cs"
Cohesion: 0.17
Nodes (9): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, CancellationToken, IReadOnlyCollection, RouteGroupBuilder, Task, Endpoint, DateTimeOffset, Guid (+1 more)

### Community 136 - "BackgroundJobsOptions"
Cohesion: 0.33
Nodes (6): PersistenceQueryableExtensions, CancellationToken, Expression, Func, IQueryable, Task

### Community 137 - "CaptchaOptions"
Cohesion: 0.21
Nodes (9): PartitionedRateLimiter, CancellationToken, Func, HttpContext, IConfiguration, IServiceCollection, OnRejectedContext, ValueTask (+1 more)

### Community 138 - "IAM.Endpoints.Otp.VersionNeutral"
Cohesion: 0.40
Nodes (3): IAM.Endpoints.Otp.VersionNeutral, RouteGroupBuilder, Setup

### Community 139 - ".GetMyStoreAsync"
Cohesion: 0.22
Nodes (6): Products.Endpoints.Stores.v1.My.Get, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response

### Community 140 - "Users/VersionNeutral/Setup.cs"
Cohesion: 0.40
Nodes (3): IAM.Endpoints.Users.VersionNeutral, RouteGroupBuilder, Setup

### Community 141 - "Setup"
Cohesion: 0.70
Nodes (3): OpenApiOperation, OperationFilterContext, DefaultResponsesOperationFilter

### Community 142 - "ModulesOptions"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 143 - "OtpPurposes.cs"
Cohesion: 0.20
Nodes (8): ChangeTracker, DatabaseFacade, EntityEntry, IDisposable, IDbContext, CancellationToken, DbSet, Task

### Community 144 - "OtpOptions"
Cohesion: 0.20
Nodes (7): IAM.Endpoints.Users.VersionNeutral.Get, CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, Response

### Community 145 - ".AddOrUpdate"
Cohesion: 0.20
Nodes (6): IAM.Infrastructure.Auth, IAM.Application.Auth, string, CustomClaims, string, MultiAuthDefaults

### Community 146 - "SecurityHeadersOptions"
Cohesion: 0.35
Nodes (3): Exception, ILogger, LoggerMessage

### Community 147 - "SignalROptions"
Cohesion: 0.40
Nodes (3): MassTransitInterModuleRequestClient, CancellationToken, Task

### Community 148 - "Setup"
Cohesion: 0.35
Nodes (6): Assembly, IApplicationBuilder, IConfiguration, IServiceCollection, IWebHostEnvironment, Setup

### Community 149 - "AutoMigrateMarker"
Cohesion: 0.20
Nodes (8): CancellationToken, DefaultIdType, Task, IRoleService, CancellationToken, DefaultIdType, Task, RoleService

### Community 150 - "Setup"
Cohesion: 0.33
Nodes (5): Memory, ReadOnlyMemory, CancellationToken, Task, ValueTask

### Community 151 - ".GetMeAsync"
Cohesion: 0.50
Nodes (3): ReverseProxyOptions, ReverseProxyOptionsValidator, IReadOnlyList

### Community 152 - ".UpdateStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 153 - "Endpoint"
Cohesion: 0.22
Nodes (5): IApplicationBuilder, IServiceCollection, IWebHostEnvironment, Type, Setup

### Community 154 - "Host.Swagger"
Cohesion: 0.40
Nodes (3): Host, Host.Swagger, Program

### Community 155 - "VerifyPhoneOtpRequest"
Cohesion: 0.33
Nodes (6): OtpVerificationFailureReason, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, CancellationToken, Task, VerifyPhoneOtpRequestHandler

### Community 156 - "CurrentUser"
Cohesion: 0.10
Nodes (17): Common.Application.ModelBinders, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, Products.Endpoints.ProductTemplates.v1.Activate, IModelBinder, ModelBindingContext, StronglyTypedIdBinder, Task, Request (+9 more)

### Community 157 - "Setup"
Cohesion: 0.33
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, OutboxCleanupJob

### Community 158 - "Endpoint"
Cohesion: 0.16
Nodes (10): DbSet, IProductsDbContext, IReadOnlyList, List, ProductTemplate, EntityTypeBuilder, ProductTemplateConfiguration, DbSet (+2 more)

### Community 160 - "ValidationContextExtensions"
Cohesion: 0.40
Nodes (3): ValidationContextExtensions, string, ValidationContext

### Community 161 - "Endpoint"
Cohesion: 0.22
Nodes (7): Products.Endpoints.Stores.v1.My.AuditLog, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator

### Community 162 - ".ForUser"
Cohesion: 0.29
Nodes (6): AuthorizationHandler, AuthorizationHandlerContext, IAuthorizationRequirement, Task, PermissionAuthorizationHandler, PermissionRequirement

### Community 163 - "Endpoint"
Cohesion: 0.21
Nodes (6): CollectionExtensions, Func, ICollection, IEnumerable, CancellationToken, Task

### Community 164 - "Endpoint"
Cohesion: 0.14
Nodes (7): Host.Middlewares, IApplicationBuilder, IServiceCollection, Setup, HttpContext, Task, SecurityHeadersMiddleware

### Community 165 - "Endpoint"
Cohesion: 0.11
Nodes (13): Common.Infrastructure.Auth.Services, Common.Infrastructure.Auth, FrozenDictionary, IReadOnlySet, CustomPermissions, HashSet, IEnumerable, CurrentUser (+5 more)

### Community 166 - "Setup"
Cohesion: 0.25
Nodes (5): IAM.Endpoints.Tokens.VersionNeutral, RouteGroupBuilder, Endpoint, RouteGroupBuilder, Setup

### Community 167 - "Setup"
Cohesion: 0.22
Nodes (7): Products.Endpoints.Products.v1.AuditLog, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator

### Community 168 - "Endpoint"
Cohesion: 0.25
Nodes (5): IAM.Endpoints.Captcha.VersionNeutral, RouteGroupBuilder, Endpoint, RouteGroupBuilder, Setup

### Community 169 - ".Get"
Cohesion: 0.20
Nodes (7): Products.Infrastructure.RateLimiting, Action, IEnumerable, RateLimiterOptions, Policies, string, RateLimitingConstants

### Community 171 - "Endpoint"
Cohesion: 0.29
Nodes (4): Common.InterModuleRequests, IAssemblyReference, Setup, IServiceCollection

### Community 172 - "Endpoint"
Cohesion: 0.25
Nodes (8): ConcurrentDictionary, BackgroundJobsTelemetry, ActivitySource, Counter, Histogram, Meter, ObservableGauge, string

### Community 174 - "Endpoint"
Cohesion: 0.29
Nodes (5): IAM.Application.Tokens.DTOs, DateTimeOffset, AccessTokenDto, DateTimeOffset, TokensDto

### Community 175 - "Endpoint"
Cohesion: 0.12
Nodes (13): PaginationRequest, PaginationResponse, DbContextExtensions, CancellationToken, DbSet, JsonSerializerOptions, Task, PaginationQueryableExtensions (+5 more)

### Community 176 - "FeatureManagement/RouteHandlerBuilderExtensions.cs"
Cohesion: 0.48
Nodes (6): AbstractValidator, ApiKeyEntry, ApiKeyEntryValidator, ApiKeysOptions, ApiKeysOptionsValidator, IReadOnlyList

### Community 177 - "SendRequestBody"
Cohesion: 0.48
Nodes (4): AuthorizationPolicy, IAuthorizationPolicyProvider, Task, PermissionPolicyProvider

### Community 178 - "Endpoint"
Cohesion: 0.47
Nodes (5): NotificationPayload, CancellationToken, IReadOnlyList, Task, SignalRNotificationDispatcher

### Community 179 - "Endpoint"
Cohesion: 0.52
Nodes (6): CachingEntryDefaults, CachingOptions, CachingOptionsValidator, Redis, RedisValidator, TimeSpan

### Community 180 - "SmsOptions.cs"
Cohesion: 0.23
Nodes (9): SmsOptions, SmsOptionsValidator, SmsProvider, SmsTemplatesOptions, Dictionary, IConfiguration, IServiceCollection, long (+1 more)

### Community 181 - "HttpContextExtensions.cs"
Cohesion: 0.09
Nodes (17): Common.Application.Search, Notifications.Application.Push, Common.Infrastructure.RateLimiting, Notifications.Infrastructure.Push, IAM.Endpoints, Common.Infrastructure.Extensions, Common.Application.Options, IAM.Infrastructure.Tokens (+9 more)

### Community 182 - "Endpoint"
Cohesion: 0.33
Nodes (4): IAM.Domain, string, Constants, IAssemblyReference

### Community 184 - "ReCaptchaResponse"
Cohesion: 0.25
Nodes (4): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, ICaptchaService, Response, DummyCaptchaService

### Community 185 - "Endpoint"
Cohesion: 0.18
Nodes (8): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, IamModule

### Community 186 - "Endpoint"
Cohesion: 0.14
Nodes (10): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint, ActivitySource, Counter, Meter (+2 more)

### Community 187 - "Endpoint"
Cohesion: 0.33
Nodes (4): Common.Infrastructure.Caching, Setup, IConfiguration, IServiceCollection

### Community 189 - "ISearchLanguageResolver"
Cohesion: 0.47
Nodes (3): ISearchLanguageResolver, SearchLanguageResolver, string

### Community 190 - ".AddCustomMassTransit"
Cohesion: 0.33
Nodes (4): Assembly, IConfiguration, IServiceCollection, Setup

### Community 191 - "Setup"
Cohesion: 0.25
Nodes (7): Concurrent safety, Cross-process call path, Files added by this PoC, How it works, How to run, Split-Deployment PoC, What this proves

### Community 192 - ".UpdateMyProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 193 - ".RemoveMyProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 194 - ".UpdateMyStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 195 - "ProductTemplates/Setup.cs"
Cohesion: 0.25
Nodes (5): Products.Endpoints.ProductTemplates, RouteGroupBuilder, Setup, RouteGroupBuilder, Endpoint

### Community 196 - "Response"
Cohesion: 0.40
Nodes (4): IAM.Endpoints.Users.VersionNeutral.Me.Get, DateOnly, IReadOnlyCollection, Response

### Community 197 - "IInterModuleRequestHandler"
Cohesion: 0.28
Nodes (6): accessToken, DateTimeOffset, expiresAt, ICollection, refreshTokenBytes, TokenService

### Community 198 - "Revoke/Request.cs"
Cohesion: 0.28
Nodes (7): SessionTokenReuseDetectedIntegrationEvent, CancellationToken, Guid, ILogger, LoggerMessage, Task, SessionTokenReuseDetectedSignalRHandler

### Community 200 - "JwtOptions"
Cohesion: 0.22
Nodes (6): ActivitySource, Counter, Meter, string, NotificationsTelemetry, UpDownCounter

### Community 201 - "SecurityHeadersOptions.cs"
Cohesion: 0.25
Nodes (5): IServerFilter, PerformedContext, PerformingContext, JobMetricsFilter, string

### Community 203 - "IAssemblyReference"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 204 - "HealthCheckOptions.cs"
Cohesion: 0.46
Nodes (4): CancellationToken, IReadOnlyList, Task, INotificationDispatcher

### Community 205 - "Request.cs"
Cohesion: 0.15
Nodes (7): Common.Infrastructure.Modules, Host.Infrastructure, OtlpExportProtocol, IConfiguration, IServiceCollection, Setup, StringExtensions

### Community 206 - "IAssemblyReference"
Cohesion: 0.17
Nodes (9): DbContext, IOutboxDbContext, CancellationToken, DbSet, Task, DbSet, ModelBuilder, ModelConfigurationBuilder (+1 more)

### Community 207 - "Setup"
Cohesion: 0.25
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 208 - ".SendOtp"
Cohesion: 0.29
Nodes (5): CancellationToken, IFeatureManager, RouteGroupBuilder, Task, Endpoint

### Community 209 - "Stores/v1/Update/Request.cs"
Cohesion: 0.40
Nodes (5): Products.Endpoints.Stores.v1.Update, RequestBody, Request, RequestBody, RequestValidator

### Community 210 - "TokenService.cs"
Cohesion: 0.33
Nodes (3): IAM.Infrastructure.Tokens.Services, IServiceCollection, Setup

### Community 211 - "IAssemblyReference"
Cohesion: 0.22
Nodes (6): Products.Endpoints.Stores.v1.Get, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response

### Community 212 - "PaginationRequestValidator"
Cohesion: 0.40
Nodes (4): PaginationRequestValidator, int, Request, RequestValidator

### Community 213 - "IAssemblyReference"
Cohesion: 0.33
Nodes (5): ActivitySource, Counter, Meter, string, ProductsTelemetry

### Community 214 - "AuditLogEntry"
Cohesion: 0.47
Nodes (4): AuditLogEntry, DefaultIdType, AuditLogEntryConfiguration, EntityTypeBuilder

### Community 215 - ".DeactivateProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 216 - "Products.Endpoints.Probe"
Cohesion: 0.40
Nodes (3): Products.Endpoints.Probe, RouteGroupBuilder, Setup

### Community 217 - "PushOptions.cs"
Cohesion: 0.70
Nodes (4): FirebaseServiceAccountOptions, PushOptions, PushOptionsValidator, PushProvider

### Community 218 - "Stores/v1/My/Update/Request.cs"
Cohesion: 0.67
Nodes (3): Products.Endpoints.Stores.v1.My.Update, Request, RequestValidator

### Community 219 - ".RevokeAllSessions"
Cohesion: 0.12
Nodes (12): ICurrentUser, Guid, ICollection, CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint (+4 more)

### Community 221 - "Response"
Cohesion: 0.50
Nodes (3): IAM.Endpoints.Users.VersionNeutral.Search, DateOnly, Response

### Community 222 - "Deactivate/Request.cs"
Cohesion: 0.67
Nodes (3): Products.Endpoints.ProductTemplates.v1.Deactivate, Request, RequestValidator

### Community 223 - "My/RemoveProduct/Request.cs"
Cohesion: 0.67
Nodes (3): Products.Endpoints.Stores.v1.My.RemoveProduct, Request, RequestValidator

### Community 224 - "UpdatePushToken/Request.cs"
Cohesion: 0.67
Nodes (3): IAM.Endpoints.Tokens.VersionNeutral.Sessions.UpdatePushToken, Request, RequestValidator

### Community 225 - "Products.Endpoints.Probe"
Cohesion: 0.40
Nodes (3): Common.Endpoints.Versioning, Products.Endpoints, IAssemblyReference

### Community 226 - "ModulesOptions.cs"
Cohesion: 0.67
Nodes (3): ModulesOptions, ModulesOptionsValidator, IReadOnlyList

### Community 227 - "ResxLocalizationOptions.cs"
Cohesion: 0.67
Nodes (3): ResxLocalizationOptions, ResxLocalizationOptionsValidator, ICollection

### Community 228 - "IAM"
Cohesion: 0.12
Nodes (14): Common.Application.Validation, BackgroundJobsOptions, BackgroundJobsOptionsValidator, DatabaseOptions, DatabaseOptionsValidator, HealthCheckOptions, HealthCheckOptionsValidator, InterModuleRequestOptions (+6 more)

### Community 229 - "IntegrationEvent"
Cohesion: 0.33
Nodes (4): Setup, IConfiguration, IHostEnvironment, IServiceCollection

### Community 231 - "AccessTokenDto"
Cohesion: 0.40
Nodes (3): Setup, IApplicationBuilder, IServiceCollection

### Community 243 - "Response"
Cohesion: 0.67
Nodes (3): CustomActions, CustomResources, string

### Community 244 - "Response"
Cohesion: 0.50
Nodes (3): CustomRoles, HashSet, string

### Community 249 - "Response"
Cohesion: 0.40
Nodes (3): AuthenticationBuilder, IConfiguration, Setup

## Knowledge Gaps
- **135 isolated node(s):** `OtpCacheEntry`, `Common.Domain`, `Common.Infrastructure`, `IAssemblyReference`, `PushTarget` (+130 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **102 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `HttpContextExtensions.cs` to `Host Logging & Serilog Setup`, `Endpoint`, `Setup`, `Host NuGet Deps (OTel/Health)`, `REPR Request Validators`, `BackgroundJobs Service (Hangfire)`, `IAM OTP Verify & Token Endpoint`, `SignalROptions`, `Authz Constants & Feature Flags`, `.GetMeAsync`, `Telemetry (ActivitySource/Meter)`, `Host.Swagger`, `Setup`, `StoreConfiguration`, `Endpoint`, `.Get`, `Endpoint`, `FeatureManagement/RouteHandlerBuilderExtensions.cs`, `IntegrationTestFactory`, `Asp.Versioning.Http`, `Endpoint`, `SmsOptions.cs`, `Auditing/Setup.cs`, `Endpoint`, `coverlet.collector`, `ISearchLanguageResolver`, `.AddCustomMassTransit`, `CustomRoles`, `HostCollection`, `Request.cs`, `Endpoint`, `BaseIntegrationTest`, `TokenService.cs`, `AuditLogRetentionJobRegistrar`, `PushOptions.cs`, `HttpClient`, `IOpenApiSchema`, `OtpServiceBase`, `OutboxTestWebAppFactory`, `ModulesOptions.cs`, `ResxLocalizationOptions.cs`, `IAM`, `IntegrationEvent`, `Products.Endpoints.Probe`, `OtpOptions.cs`, `IModelBinder`, `StringExtensions`, `IRoleService`, `RequestBody`, `Response`, `Setup`?**
  _High betweenness centrality (0.396) - this node is a cross-community bridge._
- **Why does `Result` connect `MassTransit & DI Setup` to `Setup`, `.RemoveProductAsync`, `HostEnvironmentExtensions.cs`, `BackgroundJobsOptions`, `k6 Load Test Scripts`, `IAM Error Catalogs`, `.GetMyStoreAsync`, `IAM OTP Verify & Token Endpoint`, `ModulesOptions`, `OtpOptions`, `Launch Settings`, `SecurityHeadersOptions`, `Outbox Processor & Seeder`, `EF Core DbContexts`, `.UpdateStoreAsync`, `Product Template Aggregate`, `Endpoint`, `Endpoint`, `Endpoint`, `EventDispatcher`, `Setup`, `Endpoint`, `IInterModuleRequestHandler`, `double`, `ReCaptchaResponse`, `DummyOtpService`, `Endpoint`, `decimal`, `.UpdateMyProductAsync`, `.RemoveMyProductAsync`, `.UpdateMyStoreAsync`, `AuthenticateResult`, `OutboxModule`, `.ToResult`, `IAggregateRoot`, `IAssemblyReference`, `Microsoft.AspNetCore.Identity.EntityFrameworkCore`, `Setup`, `.SendOtp`, `net10.0`, `IAssemblyReference`, `Activity`, `.DeactivateProductTemplateAsync`, `HostTestFactory`, `.RevokeAllSessions`, `IOpenApiSchema`, `OutboxCleanupSettings`, `IDatabaseSeeder`, `IdentityResultExtensions`, `IMiddleware`, `IModelBinder`, `RequestBody`, `ReverseProxyOptions`, `Endpoint`?**
  _High betweenness centrality (0.092) - this node is a cross-community bridge._
- **Why does `Common.Domain.StronglyTypedIds` connect `Functional Result Extensions` to `Endpoint`, `IAM User Identity & Auditing`, `Host Logging & Serilog Setup`, `BackgroundJobs Service (Hangfire)`, `.GetMyStoreAsync`, `Localized Identity Errors`, `OtpOptions`, `SignalR Hub & Exception Middleware`, `CurrentUser`, `Microsoft.EntityFrameworkCore.Abstractions`, `ISearchLocalized`, `Endpoint`, `Aigamo.ResXGenerator`, `CollectionExtensions`, `coverlet.collector`, `DummyOtpService`, `Response`, `Endpoint`, `TokenService.cs`, `IAssemblyReference`, `.RevokeAllSessions`, `Response`, `IDatabaseSeeder`, `IMiddleware`, `Stores/v1/Create/Request.cs`, `ReverseProxyOptions.cs`, `ProductsModule`, `My/Search/Request.cs`?**
  _High betweenness centrality (0.083) - this node is a cross-community bridge._
- **What connects `OtpCacheEntry`, `Common.Domain`, `Common.Infrastructure` to the rest of the system?**
  _135 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Host Logging & Serilog Setup` be split into smaller, more focused modules?**
  _Cohesion score 0.13257575757575757 - nodes in this community are weakly interconnected._
- **Should `IAM User Identity & Auditing` be split into smaller, more focused modules?**
  _Cohesion score 0.11397849462365592 - nodes in this community are weakly interconnected._
- **Should `Products Store & Audit Services` be split into smaller, more focused modules?**
  _Cohesion score 0.10810810810810811 - nodes in this community are weakly interconnected._