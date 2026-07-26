# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-07-26)

## Corpus Check
- 447 files · ~64,502 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2872 nodes · 5052 edges · 289 communities (186 shown, 103 thin omitted)
- Extraction: 98% EXTRACTED · 2% INFERRED · 0% AMBIGUOUS · INFERRED: 76 edges (avg confidence: 0.8)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `30d01dd9`
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
- Setup
- IAssemblyReference
- Request.cs
- IAssemblyReference
- .LogDispatchingNotification
- IAssemblyReference
- IAssemblyReference
- .DeactivateProductTemplateAsync
- .RevokeAllSessions
- Host.Swagger
- Products.Endpoints.Probe
- IAM
- IntegrationEvent
- AccessTokenDto
- SendPhoneOtp
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
1. `Common.Application.Options` - 93 edges
2. `Result` - 83 edges
3. `Common.Domain.ResultMonad` - 67 edges
4. `Common.Domain.StronglyTypedIds` - 67 edges
5. `CustomValidator` - 59 edges
6. `Common.Application.Auth` - 58 edges
7. `Common.Application.Validation` - 53 edges
8. `ApplicationUserId` - 51 edges
9. `Common.Application.Extensions` - 47 edges
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

## Communities (289 total, 103 thin omitted)

### Community 0 - "Host Logging & Serilog Setup"
Cohesion: 0.13
Nodes (13): IAM.Application.Tokens.Services, IAM.Domain.Identity, IAM.Domain.Identity.Sessions, IAM.Endpoints.Tokens.VersionNeutral.Revoke, IAM.Infrastructure.Tokens.Services, IAM.Infrastructure.Telemetry, IAM.Application.Persistence, IAM.Domain.Errors (+5 more)

### Community 1 - "IAM User Identity & Auditing"
Cohesion: 0.14
Nodes (19): IEntityTypeConfiguration, ApplicationUserId, DefaultIdType, DateTimeOffset, ApplicationUser, EntityTypeBuilder, IdentityRole, IdentityRoleClaim (+11 more)

### Community 2 - "Products Store & Audit Services"
Cohesion: 0.16
Nodes (11): BackgroundService, DatabaseSeederOrchestrator, CancellationToken, ILogger, LoggerMessage, Task, SeedingCompletionTracker, CancellationToken (+3 more)

### Community 3 - "Notifications Dispatch & SignalR Client"
Cohesion: 0.12
Nodes (13): IdentityDbContext, CancellationToken, DbSet, IdentityRole, IdentityRoleClaim, IdentityUserClaim, IdentityUserLogin, IdentityUserRole (+5 more)

### Community 5 - "Cross-Module Comm & Arch Audit Skills"
Cohesion: 0.21
Nodes (9): Guid, DateTimeOffset, Guid, IReadOnlyCollection, List, Session, SessionId, EntityTypeBuilder (+1 more)

### Community 6 - "Domain Event Handling & Outbox Collect"
Cohesion: 0.11
Nodes (17): IConnectionMultiplexer, RateLimiter, RateLimiterStatistics, RateLimitLease, FixedWindowLease, RedisFixedWindowRateLimiter, bool, CancellationToken (+9 more)

### Community 7 - "Host NuGet Deps (OTel/Health)"
Cohesion: 0.07
Nodes (20): Notifications.Application.Otp, Notifications.Application.Sms, IAM.Application.Extensions, Notifications.Infrastructure.Sms, Common.Application.FeatureManagement, IAM.Endpoints.Otp, Notifications.Infrastructure.Telemetry, IAM.Domain.Captcha (+12 more)

### Community 8 - "k6 Load Test Scripts"
Cohesion: 0.30
Nodes (7): CancellationToken, Exception, ILogger, LoggerMessage, Task, TimeSpan, OutboxProcessor

### Community 9 - "REPR Request Validators"
Cohesion: 0.16
Nodes (16): AbstractValidator, Products.Endpoints.Stores.v1.Update, Products.Endpoints.Products.v1.My.Update, CustomValidator, RequestBody, Request, RequestBody, RequestBodyValidator (+8 more)

### Community 10 - "IAM Error Catalogs"
Cohesion: 0.08
Nodes (14): HttpStatusCode, IdentityResult, IStringLocalizer, StringLocalizerExtensions, Error, ICollection, IResult, IdentityResultExtensions (+6 more)

### Community 11 - "BackgroundJobs Service (Hangfire)"
Cohesion: 0.19
Nodes (8): Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Products.Domain.Products, Products.Infrastructure.Telemetry, Products.Application.Persistence, Products.Domain.Stores, Products.Domain.ProductTemplates, Common.Application.Auth

### Community 12 - "IAM OTP Verify & Token Endpoint"
Cohesion: 0.17
Nodes (15): IInterModuleRequestClient, CancellationToken, Task, ITokenService, CancellationToken, HttpContext, IFeatureManager, IOptions (+7 more)

### Community 13 - "Project Files & Solution"
Cohesion: 0.39
Nodes (5): SendPhoneOtpRequest, SendPhoneOtpResponse, CancellationToken, Task, SendPhoneOtpRequestHandler

### Community 14 - "Localized Identity Errors"
Cohesion: 0.15
Nodes (3): IdentityError, IdentityErrorDescriber, LocalizedIdentityErrorDescriber

### Community 15 - "Functional Result Extensions"
Cohesion: 0.26
Nodes (5): Common.Domain.StronglyTypedIds, Common.Domain.Entities, Common.Domain.Aggregates, IAuditableEntity, DateTimeOffset

### Community 16 - "Launch Settings"
Cohesion: 0.25
Nodes (4): Common.Application.Caching, Notifications.Infrastructure.Otp, OtpCacheEntry, OtpService

### Community 17 - "Module Installers (IModule)"
Cohesion: 0.14
Nodes (11): RecurringJobOptions, IRecurringBackgroundJobs, Action, Expression, Func, Task, RecurringBackgroundJobsService, Action (+3 more)

### Community 18 - "Host Infrastructure Setup"
Cohesion: 0.07
Nodes (25): Products.Domain.Products.DomainEvents.v1, Common.Domain.Events, Products.Domain.Stores.DomainEvents.v1, AggregateRoot, IEnumerable, IReadOnlyCollection, List, IAggregateRoot (+17 more)

### Community 19 - "IAM OTP Send & Captcha"
Cohesion: 0.13
Nodes (12): ObservableGauge, CancellationToken, ILogger, LoggerMessage, Task, ActivitySource, Counter, Histogram (+4 more)

### Community 20 - "Authz Constants & Feature Flags"
Cohesion: 0.09
Nodes (18): IAM.Endpoints.Otp.VersionNeutral.SendForLogin, IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, IAM.Endpoints.Common.Validations, IResxLocalizer, IRuleBuilder, IRuleBuilderOptions, CommonValidations, IResxLocalizer (+10 more)

### Community 21 - "Bounded Capture Streams"
Cohesion: 0.10
Nodes (13): byte, Memory, ReadOnlyMemory, ReadOnlySpan, SeekOrigin, bool, CancellationToken, int (+5 more)

### Community 22 - "SignalR Hub & Exception Middleware"
Cohesion: 0.07
Nodes (18): Common.Infrastructure.Persistence, Common.Application.Persistence, IAM.Infrastructure.Persistence, IAM.Infrastructure.Persistence.Seeding, Common.Infrastructure.Persistence.DbContext, IDatabaseSeeder, CancellationToken, Task (+10 more)

### Community 23 - "Telemetry (ActivitySource/Meter)"
Cohesion: 0.18
Nodes (7): Common.Application.BackgroundJobs, BackgroundJobs.Telemetry, BackgroundJobs, IServerFilter, PerformingContext, JobMetricsFilter, string

### Community 24 - "Outbox Processor & Seeder"
Cohesion: 0.13
Nodes (14): IAM.Endpoints.Tokens.VersionNeutral.Refresh, CancellationToken, HttpContext, ILogger, IOptions, LoggerMessage, RouteGroupBuilder, Task (+6 more)

### Community 25 - "EF Core DbContexts"
Cohesion: 0.06
Nodes (34): SendMessageBody, SendRequestBody, CancellationToken, Task, ISmsGateway, SmsCategory, SmsMessage, CancellationToken (+26 more)

### Community 26 - "Integration Event Handler Base"
Cohesion: 0.17
Nodes (8): Hub, NotificationGroupName, Exception, ILogger, LoggerMessage, string, Task, NotificationsHub

### Community 27 - "Product Template Aggregate"
Cohesion: 0.07
Nodes (19): ICurrentUser, Guid, ICollection, CancellationToken, RouteGroupBuilder, Task, Endpoint, CancellationToken (+11 more)

### Community 28 - "Outbox Message & Tokens"
Cohesion: 0.40
Nodes (3): ProblemDetails, ProblemDetailsExtensions, ICollection

### Community 29 - "MassTransit & DI Setup"
Cohesion: 0.16
Nodes (10): Result, Func, Task, AsyncExtensions, SyncExtensions, Action, Func, Task (+2 more)

### Community 30 - "PermissionAuthorizationHandler"
Cohesion: 0.05
Nodes (30): AuthorizationHandler, AuthorizationHandlerContext, AuthorizationPolicy, ClaimsPrincipal, IAM.Infrastructure.Auth.Jwt, IAM.Infrastructure.Auth.Services, IAM.Infrastructure.Auth, IAM.Application.Auth.Services (+22 more)

### Community 31 - "DbSet"
Cohesion: 0.14
Nodes (15): LoadAll, Names, Assembly, Exception, IApplicationBuilder, IConfiguration, IEnumerable, ILogger (+7 more)

### Community 32 - "CustomRateLimitingOptions"
Cohesion: 0.25
Nodes (5): Products.Endpoints.Stores, RouteGroupBuilder, Setup, RouteGroupBuilder, Endpoint

### Community 33 - "PaginationRequestValidator"
Cohesion: 0.16
Nodes (10): Products.Endpoints.Stores.v1.AddProduct, CancellationToken, RouteGroupBuilder, Task, Endpoint, RequestBody, Request, RequestBody (+2 more)

### Community 34 - "Microsoft.EntityFrameworkCore.Abstractions"
Cohesion: 0.22
Nodes (9): IInterModuleRequest, GetSeedUserIdsRequest, GetSeedUserIdsResponse, CancellationToken, Task, GetSeedUserIdsRequestHandler, CancellationToken, IResult (+1 more)

### Community 35 - "ISearchLocalized"
Cohesion: 0.09
Nodes (18): DomainEventHandlerBase, CancellationToken, Task, IEventHandler, CancellationToken, Task, IEventHandlerWrapper, CancellationToken (+10 more)

### Community 36 - "StoreConfiguration"
Cohesion: 0.26
Nodes (8): CustomRateLimitingOptions, CustomRateLimitingOptionsValidator, FixedWindow, FixedWindowValidator, Action, IEnumerable, RateLimiterOptions, Policies

### Community 37 - "Setup"
Cohesion: 0.10
Nodes (17): IEnumerable, IReadOnlyCollection, List, ApplicationUser, Task, Seeder, IdentityRole, ILogger (+9 more)

### Community 38 - "Hangfire.PostgreSql"
Cohesion: 0.19
Nodes (7): PathString, IApplicationBuilder, HttpContext, IList, RequestDelegate, string, RequestResponseBodyLoggingMiddleware

### Community 39 - "EventDispatcher"
Cohesion: 0.05
Nodes (40): FullTextSearchOptions, FullTextSearchOptionsValidator, Dictionary, IReadOnlyList, string, PaginationResponse, ISearchLanguageResolver, SearchLanguageResolver (+32 more)

### Community 40 - "NetArchTest.Rules"
Cohesion: 0.22
Nodes (8): CancellationToken, Func, HttpContext, OnRejectedContext, RateLimitPartition, ValueTask, CheckRegistrationRateLimitingPolicy, Policies

### Community 41 - "Aigamo.ResXGenerator"
Cohesion: 0.06
Nodes (28): IAM.Endpoints.Users.VersionNeutral.Search, Products.Endpoints.Stores.v1.Search, Products.Endpoints.Products.v1.My.Get, Products.Endpoints.ProductTemplates.v1.Search, Products.Endpoints.Products.v1.Search, Common.Application.DTOs, Products.Endpoints.Stores.v1.Get, Products.Endpoints.Products.v1.My.Search (+20 more)

### Community 42 - "IOperationFilter"
Cohesion: 0.11
Nodes (19): Lock, EventDispatcher, ActivitySource, CancellationToken, ILogger, LoggerMessage, Task, IntegrationEventOutbox (+11 more)

### Community 43 - "IRateLimiterPolicy"
Cohesion: 0.15
Nodes (10): CancellationToken, Func, HttpContext, OnRejectedContext, RateLimiterOptions, RateLimitPartition, TimeSpan, ValueTask (+2 more)

### Community 44 - "Seeder"
Cohesion: 0.23
Nodes (5): ISearchLocalized, Product, IReadOnlyCollection, List, Store

### Community 45 - "AggregateRoot"
Cohesion: 0.16
Nodes (10): IReadOnlyList, List, ProductTemplate, ProductTemplateId, EntityTypeBuilder, ProductTemplateConfiguration, CancellationToken, List (+2 more)

### Community 46 - "ApiVersionSet"
Cohesion: 0.25
Nodes (5): ApiVersionSet, Common.Endpoints.Versioning, Setup, IEndpointRouteBuilder, IServiceCollection

### Community 48 - "IntegrationTestFactory"
Cohesion: 0.22
Nodes (5): Products.Infrastructure.Persistence.Seeding, Products.Endpoints.Probe.v1, Common.InterModuleRequests.IAM, RouteGroupBuilder, Endpoint

### Community 50 - "Asp.Versioning.Http"
Cohesion: 0.07
Nodes (29): IConsumer, IntegrationEventHandlerBase, CancellationToken, ConsumeContext, DefaultIdType, ILogger, LoggerMessage, Task (+21 more)

### Community 51 - "IInterModuleRequestHandler"
Cohesion: 0.16
Nodes (7): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, ICaptchaService, Response, CancellationToken, Task, CachedCaptchaService, DummyCaptchaService

### Community 52 - "double"
Cohesion: 0.22
Nodes (9): double, FormUrlEncodedContent, ReCaptchaResponse, CancellationToken, Exception, ILogger, LoggerMessage, Task (+1 more)

### Community 53 - "RouteHandlerBuilderExtensions"
Cohesion: 0.31
Nodes (5): StoreId, CancellationToken, List, Task, Seeder

### Community 54 - "IList"
Cohesion: 0.33
Nodes (5): HttpClient, HttpStandardResilienceOptions, IHttpClientBuilder, Action, IServiceCollection

### Community 55 - "Microsoft.AspNetCore.SignalR.StackExchangeRedis"
Cohesion: 0.29
Nodes (7): IRateLimiterPolicy, CancellationToken, Func, OnRejectedContext, ValueTask, Policies, RegisterRateLimitingPolicy

### Community 56 - "DummyOtpService"
Cohesion: 0.20
Nodes (7): CancellationToken, Task, CancellationToken, IFeatureManager, RouteGroupBuilder, Task, Endpoint

### Community 58 - "CollectionExtensions"
Cohesion: 0.06
Nodes (31): JsonConverter, StrictDateTimeOffsetJsonConverter, DateTimeOffset, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, StronglyTypedIdListReadOnlyJsonConverter (+23 more)

### Community 59 - "coverlet.collector"
Cohesion: 0.29
Nodes (6): PaginationQueryableExtensions, CancellationToken, Expression, Func, IQueryable, Task

### Community 61 - "decimal"
Cohesion: 0.14
Nodes (11): Products.Endpoints.Stores.v1.My.AddProduct, decimal, int, Constants, CancellationToken, RouteGroupBuilder, Task, Endpoint (+3 more)

### Community 62 - "EndpointFilterDelegate"
Cohesion: 0.11
Nodes (16): Common.Application.EndpointFilters, IEndpointFilter, ResultToCreatedResponseTransformer, ResultToResponseTransformer, EndpointFilterDelegate, EndpointFilterInvocationContext, ValueTask, RouteHandlerBuilderExtensions (+8 more)

### Community 63 - "Hangfire"
Cohesion: 0.33
Nodes (4): IVariantFeatureManager, IVariantFeatureManagerExtensions, CancellationToken, Task

### Community 64 - "Seeder"
Cohesion: 0.22
Nodes (6): BackgroundJobsModule, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection

### Community 65 - "ApplyAuditingInterceptor"
Cohesion: 0.25
Nodes (7): SaveChangesInterceptor, ApplyAuditingInterceptor, CancellationToken, DbContextEventData, InterceptionResult, ValueTask, ApplySearchLanguageInterceptor

### Community 66 - "CustomRoles"
Cohesion: 0.12
Nodes (12): FrozenDictionary, IReadOnlySet, CustomPermissions, HashSet, IEnumerable, CurrentUser, Guid, ICollection (+4 more)

### Community 67 - "AuthenticateResult"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 68 - "ValueObject"
Cohesion: 0.27
Nodes (4): Common.Domain, IComparable, ValueObject, IEnumerable

### Community 69 - "IResxLocalizer"
Cohesion: 0.13
Nodes (12): IAM.Domain.Identity.DomainEvents.v1, CancellationToken, Task, V1SessionRevokedDomainEventHandler, V1AllSessionsRevokedDomainEvent, V1RefreshTokenRevokedDomainEvent, V1RefreshTokenUpdatedDomainEvent, V1SessionCreatedDomainEvent (+4 more)

### Community 70 - "OutboxModule"
Cohesion: 0.27
Nodes (5): DateTimeOffset, RefreshToken, RefreshTokenId, EntityTypeBuilder, RefreshTokenConfig

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
Cohesion: 0.11
Nodes (16): Products.Endpoints.Stores.v1.My.RemoveProduct, Products.Endpoints.Products.v1.Update, Products.Endpoints.Stores.v1.RemoveProduct, ProductId, Request, RequestValidator, Request, RequestValidator (+8 more)

### Community 76 - "HostCollection"
Cohesion: 0.67
Nodes (3): CorsOptions, CorsOptionsValidator, IReadOnlyList

### Community 77 - "Microsoft.AspNetCore.Identity.EntityFrameworkCore"
Cohesion: 0.20
Nodes (8): ChangeTracker, DatabaseFacade, EntityEntry, IDisposable, IDbContext, CancellationToken, DbSet, Task

### Community 78 - "RequestLoggingPathPostConfigure"
Cohesion: 0.22
Nodes (8): CancellationToken, Func, HttpContext, OnRejectedContext, RateLimitPartition, ValueTask, Policies, TokenCreateRateLimitingPolicy

### Community 79 - "Endpoint"
Cohesion: 0.11
Nodes (12): Common.IntegrationEvents, Notifications.Application.IntegrationEventHandlers, IAM.Application.Users.DomainEventHandlers.v1, Common.Infrastructure.EventBus, Products.Application.Stores.DomainEventHandlers.v1, Common.Application.EventBus, IIntegrationEventOutbox, Setup (+4 more)

### Community 80 - "BaseIntegrationTest"
Cohesion: 0.22
Nodes (9): IPostConfigureOptions, RequestLoggingOptions, RequestLoggingOptionsValidator, SensitivePathRule, IList, int, IServiceCollection, RequestLoggingPathPostConfigure (+1 more)

### Community 81 - "HttpContextExtensions"
Cohesion: 0.19
Nodes (10): Action, Exception, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, ILogger, IServiceCollection (+2 more)

### Community 82 - "net10.0"
Cohesion: 0.18
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegister, Common.Domain.Extensions, SearchValues, StringExtensions, Guid, Request, RequestValidator

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
Cohesion: 0.06
Nodes (23): Common.Infrastructure.Persistence.Auditing, Common.Infrastructure.Persistence.AuditLog, IHostedService, Setup, IServiceCollection, AuditLogRetentionJobRegistrar, CancellationToken, ILogger (+15 more)

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
Cohesion: 0.47
Nodes (5): CancellationToken, IReadOnlyList, Task, INotificationDispatcher, NotificationPayload

### Community 93 - "IOpenApiSchema"
Cohesion: 0.47
Nodes (4): CancellationToken, IReadOnlyList, Task, SignalRNotificationDispatcher

### Community 94 - "OtpServiceBase"
Cohesion: 0.28
Nodes (6): SemaphoreSlim, CancellationToken, int, Task, TimeSpan, OtpServiceBase

### Community 95 - ".SendAsync"
Cohesion: 0.28
Nodes (6): CancellationToken, int, string, Task, TimeSpan, RedisOtpService

### Community 96 - "OutboxCleanupSettings"
Cohesion: 0.24
Nodes (5): OutboxMessage, DateTimeOffset, TimeSpan, EntityTypeBuilder, OutboxMessageConfig

### Community 97 - "OutboxTestWebAppFactory"
Cohesion: 0.32
Nodes (5): ApiVersionDescription, IConfigureOptions, OpenApiInfo, ConfigureSwaggerOptions, SwaggerGenOptions

### Community 99 - "HangfireCustomAuthorizationFilter"
Cohesion: 0.15
Nodes (7): DashboardContext, IDashboardAsyncAuthorizationFilter, CustomPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder, HangfireCustomAuthorizationFilter, Task

### Community 100 - "IdentityResultExtensions"
Cohesion: 0.32
Nodes (6): accessToken, DateTimeOffset, expiresAt, ICollection, refreshTokenBytes, TokenService

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
Cohesion: 0.08
Nodes (26): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Stores.v1.My.AuditLog, Common.Application.AuditLog, Common.Application.Pagination, Products.Endpoints.Products.v1.AuditLog, PaginationRequest, PaginationRequestValidator, int (+18 more)

### Community 105 - "IModelBinder"
Cohesion: 0.15
Nodes (11): IFusionCache, JwtOptions, JwtOptionsValidator, IReadOnlyCollection, CancellationToken, HttpContext, IOptions, RouteGroupBuilder (+3 more)

### Community 106 - ".InvokeAsync"
Cohesion: 0.29
Nodes (5): IMiddleware, HttpContext, RequestDelegate, Task, EnrichLogsWithUserInfoMiddleware

### Community 107 - "ProductsModule"
Cohesion: 0.25
Nodes (6): CancellationToken, ILogger, int, LoggerMessage, Task, Seeder

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
Cohesion: 0.22
Nodes (6): Outbox, Common.Infrastructure.Persistence.Outbox, Outbox.Persistence, Common.Application.Persistence.Outbox, Outbox.Telemetry, OutboxMetricsJob

### Community 113 - "ReverseProxyOptions"
Cohesion: 0.13
Nodes (11): Products.Endpoints.ProductTemplates, Products.Endpoints.ProductTemplates.v1.Create, RouteGroupBuilder, Setup, CancellationToken, RouteGroupBuilder, Task, Endpoint (+3 more)

### Community 114 - "OutboxMetricsJob"
Cohesion: 0.25
Nodes (6): DbContext, BaseDbContext, CancellationToken, DbSet, ModelConfigurationBuilder, Task

### Community 115 - "SearchLanguageResolver"
Cohesion: 0.26
Nodes (7): BackgroundJobsService, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 116 - "IRoleService"
Cohesion: 0.29
Nodes (5): CancellationToken, IFeatureManager, RouteGroupBuilder, Task, Endpoint

### Community 117 - ".SendAsync"
Cohesion: 0.29
Nodes (4): CancellationToken, List, Task, Seeder

### Community 118 - "RequestBody"
Cohesion: 0.06
Nodes (27): IHostBuilder, KeyValuePair, LoggerConfiguration, LoggerMinimumLevelConfiguration, OpenTelemetryBuilder, ResourceBuilder, ObservabilityOptions, ObservabilityOptionsValidator (+19 more)

### Community 119 - "RequestBody"
Cohesion: 0.18
Nodes (8): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, ProductsModule

### Community 120 - "Request.cs"
Cohesion: 0.67
Nodes (3): ModulesOptions, ModulesOptionsValidator, IReadOnlyList

### Community 121 - "My/Search/Request.cs"
Cohesion: 0.33
Nodes (4): IOpenApiSchema, ISchemaFilter, SchemaFilterContext, StronglyTypedIdSchemaFilter

### Community 122 - "Setup"
Cohesion: 0.33
Nodes (4): IOperationFilter, OpenApiOperation, OperationFilterContext, RemoveDefaultResponseSchemaFilter

### Community 123 - "Setup"
Cohesion: 0.15
Nodes (8): Notifications.Infrastructure, IAssemblyReference, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, NotificationsModule

### Community 124 - "Setup"
Cohesion: 0.18
Nodes (8): IApplicationBuilder, IConfiguration, ILogger, IServiceCollection, LoggerMessage, string, WebApplication, Setup

### Community 125 - "Endpoint"
Cohesion: 0.33
Nodes (5): FeatureFlagResultExtensions, Action, Func, IFeatureManager, Task

### Community 126 - "Setup"
Cohesion: 0.33
Nodes (7): Exception, HttpContext, ILogger, LoggerMessage, RequestDelegate, Task, GlobalExceptionHandlingMiddleware

### Community 127 - "AuditLogEntry"
Cohesion: 0.36
Nodes (6): AuditableEntity, DateTimeOffset, AuditLogEntry, DefaultIdType, AuditLogEntryConfiguration, EntityTypeBuilder

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
Cohesion: 0.10
Nodes (17): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, DbSet, IdentityRole, IdentityRoleClaim, IdentityUserClaim, IdentityUserLogin, IdentityUserRole, IdentityUserToken (+9 more)

### Community 136 - "BackgroundJobsOptions"
Cohesion: 0.13
Nodes (12): CollectionExtensions, Func, ICollection, IEnumerable, PersistenceQueryableExtensions, CancellationToken, Expression, Func (+4 more)

### Community 137 - "CaptchaOptions"
Cohesion: 0.21
Nodes (9): PartitionedRateLimiter, CancellationToken, Func, HttpContext, IConfiguration, IServiceCollection, OnRejectedContext, ValueTask (+1 more)

### Community 138 - "IAM.Endpoints.Otp.VersionNeutral"
Cohesion: 0.15
Nodes (8): IAM.Endpoints, IAM.Endpoints.Otp.VersionNeutral, IAM.Infrastructure.Tokens, IAM.Infrastructure.Identity, IAM.Infrastructure.Captcha, IAssemblyReference, RouteGroupBuilder, Setup

### Community 139 - ".GetMyStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

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
Cohesion: 0.40
Nodes (3): IConfiguration, IServiceCollection, Setup

### Community 144 - "OtpOptions"
Cohesion: 0.20
Nodes (7): IAM.Endpoints.Users.VersionNeutral.Get, CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, Response

### Community 145 - ".AddOrUpdate"
Cohesion: 0.67
Nodes (3): Products.Endpoints.ProductTemplates.v1.Activate, Request, RequestValidator

### Community 146 - "SecurityHeadersOptions"
Cohesion: 0.14
Nodes (7): Notifications.Infrastructure.Hubs, Notifications.Application.Hubs, Task, INotificationsClient, IConfiguration, IServiceCollection, Setup

### Community 147 - "SignalROptions"
Cohesion: 0.40
Nodes (3): MassTransitInterModuleRequestClient, CancellationToken, Task

### Community 148 - "Setup"
Cohesion: 0.35
Nodes (6): Assembly, IApplicationBuilder, IConfiguration, IServiceCollection, IWebHostEnvironment, Setup

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
Cohesion: 0.29
Nodes (7): OtpVerificationFailureReason, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, VerifyPhoneOtpResponseExtensions, CancellationToken, Task, VerifyPhoneOtpRequestHandler

### Community 156 - "CurrentUser"
Cohesion: 0.10
Nodes (17): Products.Endpoints.ProductTemplates.v1.Deactivate, Common.Application.ModelBinders, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, IModelBinder, ModelBindingContext, StronglyTypedIdBinder, Task, Request (+9 more)

### Community 157 - "Setup"
Cohesion: 0.39
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, OutboxCleanupJob

### Community 158 - "Endpoint"
Cohesion: 0.50
Nodes (3): DbSet, ModelBuilder, ProductsDbContext

### Community 160 - "ValidationContextExtensions"
Cohesion: 0.40
Nodes (3): ValidationContextExtensions, string, ValidationContext

### Community 161 - "Endpoint"
Cohesion: 0.31
Nodes (5): CancellationToken, Task, TimeSpan, IOtpService, OtpVerificationOutcome

### Community 163 - "Endpoint"
Cohesion: 0.67
Nodes (3): Products.Endpoints.Stores.v1.My.Update, Request, RequestValidator

### Community 164 - "Endpoint"
Cohesion: 0.14
Nodes (7): Host.Middlewares, IApplicationBuilder, IServiceCollection, Setup, HttpContext, Task, SecurityHeadersMiddleware

### Community 165 - "Endpoint"
Cohesion: 0.22
Nodes (5): Common.Infrastructure.Localization, Common.Infrastructure.Auth.Services, Common.Infrastructure.Auth, Setup, IServiceCollection

### Community 166 - "Setup"
Cohesion: 0.25
Nodes (5): IAM.Endpoints.Tokens.VersionNeutral, RouteGroupBuilder, Endpoint, RouteGroupBuilder, Setup

### Community 167 - "Setup"
Cohesion: 0.40
Nodes (3): IApplicationBuilder, IServiceCollection, Setup

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
Cohesion: 0.22
Nodes (7): PerformedContext, BackgroundJobsTelemetry, ActivitySource, Counter, Histogram, Meter, string

### Community 174 - "Endpoint"
Cohesion: 0.29
Nodes (5): IAM.Application.Tokens.DTOs, DateTimeOffset, AccessTokenDto, DateTimeOffset, TokensDto

### Community 175 - "Endpoint"
Cohesion: 0.08
Nodes (18): AuditLogDto, DbContextExtensions, CancellationToken, DbSet, JsonSerializerOptions, Task, CancellationToken, RouteGroupBuilder (+10 more)

### Community 178 - "Endpoint"
Cohesion: 0.67
Nodes (3): ResxLocalizationOptions, ResxLocalizationOptionsValidator, ICollection

### Community 179 - "Endpoint"
Cohesion: 0.52
Nodes (6): CachingEntryDefaults, CachingOptions, CachingOptionsValidator, Redis, RedisValidator, TimeSpan

### Community 180 - "SmsOptions.cs"
Cohesion: 0.23
Nodes (9): SmsOptions, SmsOptionsValidator, SmsProvider, SmsTemplatesOptions, Dictionary, IConfiguration, IServiceCollection, long (+1 more)

### Community 181 - "HttpContextExtensions.cs"
Cohesion: 0.14
Nodes (10): Common.Application.Search, Common.Infrastructure.RateLimiting, Common.Infrastructure.Extensions, Common.Application.Options, IAM.Infrastructure.RateLimiting, Products.Infrastructure.Persistence.EntityConfigurations, HttpContextExtensions, HttpContext (+2 more)

### Community 182 - "Endpoint"
Cohesion: 0.33
Nodes (4): IAM.Domain, string, Constants, IAssemblyReference

### Community 185 - "Endpoint"
Cohesion: 0.18
Nodes (8): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, IamModule

### Community 187 - "Endpoint"
Cohesion: 0.33
Nodes (4): Common.Infrastructure.Caching, Setup, IConfiguration, IServiceCollection

### Community 191 - "Setup"
Cohesion: 0.25
Nodes (7): Concurrent safety, Cross-process call path, Files added by this PoC, How it works, How to run, Split-Deployment PoC, What this proves

### Community 203 - "IAssemblyReference"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 205 - "Request.cs"
Cohesion: 0.11
Nodes (11): Common.Infrastructure.Modules, Host.Infrastructure, OtlpExportProtocol, IConfiguration, IServiceCollection, Setup, Assembly, IConfiguration (+3 more)

### Community 206 - "IAssemblyReference"
Cohesion: 0.18
Nodes (8): IOutboxDbContext, CancellationToken, DbSet, Task, DbSet, ModelBuilder, ModelConfigurationBuilder, OutboxDbContext

### Community 208 - ".LogDispatchingNotification"
Cohesion: 0.33
Nodes (5): IntegrationEvent, DateTimeOffset, DefaultIdType, ProductCreatedIntegrationEvent, StoreCreatedIntegrationEvent

### Community 211 - "IAssemblyReference"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 213 - "IAssemblyReference"
Cohesion: 0.33
Nodes (5): ActivitySource, Counter, Meter, string, ProductsTelemetry

### Community 215 - ".DeactivateProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 219 - ".RevokeAllSessions"
Cohesion: 0.10
Nodes (15): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint, CancellationToken, RouteGroupBuilder, Task (+7 more)

### Community 225 - "Products.Endpoints.Probe"
Cohesion: 0.20
Nodes (6): Products.Infrastructure.Persistence, Products.Endpoints.Probe, Products.Endpoints, IAssemblyReference, RouteGroupBuilder, Setup

### Community 228 - "IAM"
Cohesion: 0.12
Nodes (14): Common.Application.Validation, DatabaseOptions, DatabaseOptionsValidator, HealthCheckOptions, HealthCheckOptionsValidator, InterModuleRequestOptions, InterModuleRequestOptionsValidator, OpenApiOptions (+6 more)

### Community 229 - "IntegrationEvent"
Cohesion: 0.33
Nodes (4): Setup, IConfiguration, IHostEnvironment, IServiceCollection

### Community 231 - "AccessTokenDto"
Cohesion: 0.40
Nodes (3): Setup, IApplicationBuilder, IServiceCollection

### Community 238 - "SendPhoneOtp"
Cohesion: 0.40
Nodes (3): IApplicationBuilder, IServiceCollection, Setup

### Community 243 - "Response"
Cohesion: 0.67
Nodes (3): CustomActions, CustomResources, string

### Community 244 - "Response"
Cohesion: 0.50
Nodes (3): CustomRoles, HashSet, string

### Community 249 - "Response"
Cohesion: 0.40
Nodes (3): IConfiguration, IServiceCollection, Setup

## Knowledge Gaps
- **135 isolated node(s):** `OtpCacheEntry`, `Common.Domain`, `Common.Infrastructure`, `IAssemblyReference`, `IAssemblyReference` (+130 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **103 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `HttpContextExtensions.cs` to `Host Logging & Serilog Setup`, `Endpoint`, `Setup`, `Host NuGet Deps (OTel/Health)`, `IAM.Endpoints.Otp.VersionNeutral`, `BackgroundJobs Service (Hangfire)`, `OtpPurposes.cs`, `Launch Settings`, `SecurityHeadersOptions`, `SignalROptions`, `Authz Constants & Feature Flags`, `.GetMeAsync`, `Telemetry (ActivitySource/Meter)`, `Host.Swagger`, `StoreConfiguration`, `Endpoint`, `Endpoint`, `EventDispatcher`, `Aigamo.ResXGenerator`, `.Get`, `Endpoint`, `FeatureManagement/RouteHandlerBuilderExtensions.cs`, `SendRequestBody`, `Endpoint`, `Endpoint`, `SmsOptions.cs`, `Auditing/Setup.cs`, `ReCaptchaResponse`, `Endpoint`, `HostCollection`, `Request.cs`, `Endpoint`, `BaseIntegrationTest`, `net10.0`, `AuditLogRetentionJobRegistrar`, `HttpClient`, `OutboxTestWebAppFactory`, `Products.Endpoints.Probe`, `IAM`, `IntegrationEvent`, `IModelBinder`, `StringExtensions`, `RequestBody`, `Request.cs`, `Response`, `Setup`?**
  _High betweenness centrality (0.347) - this node is a cross-community bridge._
- **Why does `Common.Domain.StronglyTypedIds` connect `Functional Result Extensions` to `Endpoint`, `IAM User Identity & Auditing`, `Host Logging & Serilog Setup`, `BackgroundJobs Service (Hangfire)`, `OtpOptions`, `SecurityHeadersOptions`, `Host Infrastructure Setup`, `SignalR Hub & Exception Middleware`, `Product Template Aggregate`, `CurrentUser`, `PermissionAuthorizationHandler`, `Microsoft.EntityFrameworkCore.Abstractions`, `Endpoint`, `Aigamo.ResXGenerator`, `Endpoint`, `IntegrationTestFactory`, `Asp.Versioning.Http`, `CollectionExtensions`, `Endpoint`, `IResxLocalizer`, `Endpoint`, `.LogDispatchingNotification`, `IMiddleware`, `ReverseProxyOptions.cs`, `My/Search/Request.cs`?**
  _High betweenness centrality (0.106) - this node is a cross-community bridge._
- **Why does `Result` connect `MassTransit & DI Setup` to `Setup`, `.RemoveProductAsync`, `HostEnvironmentExtensions.cs`, `BackgroundJobsOptions`, `IAM Error Catalogs`, `.GetMyStoreAsync`, `IAM OTP Verify & Token Endpoint`, `ModulesOptions`, `OtpOptions`, `Outbox Processor & Seeder`, `EF Core DbContexts`, `.UpdateStoreAsync`, `VerifyPhoneOtpRequest`, `Product Template Aggregate`, `PaginationRequestValidator`, `EventDispatcher`, `AggregateRoot`, `Endpoint`, `IInterModuleRequestHandler`, `double`, `DummyOtpService`, `decimal`, `CustomRoles`, `AuthenticateResult`, `IAggregateRoot`, `IAssemblyReference`, `IAssemblyReference`, `Activity`, `.DeactivateProductTemplateAsync`, `HostTestFactory`, `.RevokeAllSessions`, `IModelBinder`, `RequestBody`, `ReverseProxyOptions`, `IRoleService`, `Endpoint`?**
  _High betweenness centrality (0.092) - this node is a cross-community bridge._
- **What connects `OtpCacheEntry`, `Common.Domain`, `Common.Infrastructure` to the rest of the system?**
  _135 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Host Logging & Serilog Setup` be split into smaller, more focused modules?**
  _Cohesion score 0.13105413105413105 - nodes in this community are weakly interconnected._
- **Should `IAM User Identity & Auditing` be split into smaller, more focused modules?**
  _Cohesion score 0.1396011396011396 - nodes in this community are weakly interconnected._
- **Should `Notifications Dispatch & SignalR Client` be split into smaller, more focused modules?**
  _Cohesion score 0.125 - nodes in this community are weakly interconnected._