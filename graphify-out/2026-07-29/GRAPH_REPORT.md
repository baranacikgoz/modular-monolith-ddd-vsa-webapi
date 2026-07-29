# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-07-26)

## Corpus Check
- 453 files · ~65,304 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2907 nodes · 5102 edges · 303 communities (203 shown, 100 thin omitted)
- Extraction: 99% EXTRACTED · 1% INFERRED · 0% AMBIGUOUS · INFERRED: 76 edges (avg confidence: 0.8)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `5398e122`
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
1. `Common.Application.Options` - 95 edges
2. `Result` - 83 edges
3. `Common.Domain.ResultMonad` - 67 edges
4. `Common.Domain.StronglyTypedIds` - 67 edges
5. `CustomValidator` - 60 edges
6. `Common.Application.Auth` - 59 edges
7. `Common.Application.Validation` - 54 edges
8. `ApplicationUserId` - 51 edges
9. `Common.Application.Extensions` - 48 edges
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

## Communities (303 total, 100 thin omitted)

### Community 1 - "IAM User Identity & Auditing"
Cohesion: 0.14
Nodes (19): IEntityTypeConfiguration, ApplicationUserId, DefaultIdType, DateTimeOffset, ApplicationUser, EntityTypeBuilder, IdentityRole, IdentityRoleClaim (+11 more)

### Community 2 - "Products Store & Audit Services"
Cohesion: 0.16
Nodes (11): BackgroundService, DatabaseSeederOrchestrator, CancellationToken, ILogger, LoggerMessage, Task, SeedingCompletionTracker, CancellationToken (+3 more)

### Community 3 - "Notifications Dispatch & SignalR Client"
Cohesion: 0.15
Nodes (11): IdentityDbContext, DbSet, IdentityRole, IdentityRoleClaim, IdentityUserClaim, IdentityUserLogin, IdentityUserRole, IdentityUserToken (+3 more)

### Community 5 - "Cross-Module Comm & Arch Audit Skills"
Cohesion: 0.14
Nodes (14): Guid, DateTimeOffset, RefreshToken, RefreshTokenId, DateTimeOffset, Guid, IReadOnlyCollection, List (+6 more)

### Community 6 - "Domain Event Handling & Outbox Collect"
Cohesion: 0.11
Nodes (17): IConnectionMultiplexer, RateLimiter, RateLimiterStatistics, RateLimitLease, FixedWindowLease, RedisFixedWindowRateLimiter, bool, CancellationToken (+9 more)

### Community 7 - "Host NuGet Deps (OTel/Health)"
Cohesion: 0.08
Nodes (14): IAM.Application.Extensions, Common.Application.FeatureManagement, IAM.Endpoints.Otp, IAM.Domain.Captcha, Common.InterModuleRequests.Contracts, IAM.Infrastructure.Captcha.Services, IAM.Application.Captcha.Services, IAM.Infrastructure.Captcha (+6 more)

### Community 8 - "k6 Load Test Scripts"
Cohesion: 0.30
Nodes (7): CancellationToken, Exception, ILogger, LoggerMessage, Task, TimeSpan, OutboxProcessor

### Community 9 - "REPR Request Validators"
Cohesion: 0.16
Nodes (13): Products.Endpoints.Stores.v1.Update, Products.Endpoints.Products.v1.My.Update, RequestBody, Request, RequestBody, RequestBodyValidator, RequestValidator, RequestBodyValidator (+5 more)

### Community 10 - "IAM Error Catalogs"
Cohesion: 0.12
Nodes (11): HttpStatusCode, IStringLocalizer, StringLocalizerExtensions, Error, ICollection, IResult, CaptchaErrors, ICollection (+3 more)

### Community 11 - "BackgroundJobs Service (Hangfire)"
Cohesion: 0.16
Nodes (11): Common.Application.Search, Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Products.Infrastructure.Telemetry, Products.Application.Persistence, Products.Domain.Stores, Common.Domain.ResultMonad (+3 more)

### Community 12 - "IAM OTP Verify & Token Endpoint"
Cohesion: 0.22
Nodes (12): ITokenService, CancellationToken, HttpContext, IFeatureManager, IOptions, RouteGroupBuilder, Task, TimeProvider (+4 more)

### Community 13 - "Project Files & Solution"
Cohesion: 0.18
Nodes (10): SendPhoneOtpRequest, SendPhoneOtpResponse, CancellationToken, IFeatureManager, RouteGroupBuilder, Task, Endpoint, CancellationToken (+2 more)

### Community 14 - "Localized Identity Errors"
Cohesion: 0.13
Nodes (4): IAM.Infrastructure.Identity, IdentityError, IdentityErrorDescriber, LocalizedIdentityErrorDescriber

### Community 15 - "Functional Result Extensions"
Cohesion: 0.12
Nodes (9): Common.Domain.StronglyTypedIds, IAM.Domain.Identity.DomainEvents.v1, Common.Domain.Events, IAM.Infrastructure.Persistence, IAM.Infrastructure.Persistence.Seeding, IAM.Domain.Identity, IAM.Domain.Identity.Sessions, Common.Domain.Entities (+1 more)

### Community 16 - "Launch Settings"
Cohesion: 0.14
Nodes (9): Notifications.Application.Otp, Common.Application.Caching, Notifications.Infrastructure.Otp, CacheKeys, For, OtpCacheEntry, IConfiguration, IServiceCollection (+1 more)

### Community 17 - "Module Installers (IModule)"
Cohesion: 0.14
Nodes (11): RecurringJobOptions, IRecurringBackgroundJobs, Action, Expression, Func, Task, RecurringBackgroundJobsService, Action (+3 more)

### Community 18 - "Host Infrastructure Setup"
Cohesion: 0.08
Nodes (24): Products.Domain.Products.DomainEvents.v1, AggregateRoot, IEnumerable, IReadOnlyCollection, List, IAggregateRoot, IEnumerable, IReadOnlyCollection (+16 more)

### Community 19 - "IAM OTP Send & Captcha"
Cohesion: 0.13
Nodes (12): ObservableGauge, CancellationToken, ILogger, LoggerMessage, Task, ActivitySource, Counter, Histogram (+4 more)

### Community 20 - "Authz Constants & Feature Flags"
Cohesion: 0.09
Nodes (18): IAM.Endpoints.Otp.VersionNeutral.SendForLogin, IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, IAM.Endpoints.Common.Validations, IResxLocalizer, IRuleBuilder, IRuleBuilderOptions, CommonValidations, IResxLocalizer (+10 more)

### Community 21 - "Bounded Capture Streams"
Cohesion: 0.12
Nodes (8): byte, ReadOnlySpan, SeekOrigin, bool, int, BoundedCaptureStream, BoundedRequestCaptureStream, Stream

### Community 22 - "SignalR Hub & Exception Middleware"
Cohesion: 0.07
Nodes (20): Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Products.Infrastructure.Persistence.Seeding, Common.Application.Persistence, Common.Infrastructure.Localization, Common.InterModuleRequests.IAM, Common.Infrastructure.EventBus, Products.Domain.ProductTemplates (+12 more)

### Community 23 - "Telemetry (ActivitySource/Meter)"
Cohesion: 0.18
Nodes (7): Common.Application.BackgroundJobs, BackgroundJobs.Telemetry, BackgroundJobs, IServerFilter, PerformingContext, JobMetricsFilter, string

### Community 24 - "Outbox Processor & Seeder"
Cohesion: 0.12
Nodes (14): IAM.Endpoints.Tokens.VersionNeutral.Refresh, CancellationToken, HttpContext, ILogger, IOptions, LoggerMessage, RouteGroupBuilder, Task (+6 more)

### Community 25 - "EF Core DbContexts"
Cohesion: 0.06
Nodes (34): SendMessageBody, SendRequestBody, CancellationToken, Task, ISmsGateway, SmsCategory, SmsMessage, CancellationToken (+26 more)

### Community 26 - "Integration Event Handler Base"
Cohesion: 0.06
Nodes (30): Notifications.Infrastructure.Hubs, Notifications.Application.Hubs, Hub, accessToken, DateTimeOffset, expiresAt, ICollection, refreshTokenBytes (+22 more)

### Community 27 - "Product Template Aggregate"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 28 - "Outbox Message & Tokens"
Cohesion: 0.40
Nodes (3): ProblemDetails, ProblemDetailsExtensions, ICollection

### Community 29 - "MassTransit & DI Setup"
Cohesion: 0.26
Nodes (6): Result, AsyncExtensions, SyncExtensions, Action, Func, Task

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
Cohesion: 0.16
Nodes (10): Products.Endpoints.Stores.v1.AddProduct, CancellationToken, RouteGroupBuilder, Task, Endpoint, RequestBody, Request, RequestBody (+2 more)

### Community 34 - "Microsoft.EntityFrameworkCore.Abstractions"
Cohesion: 0.17
Nodes (11): Products.Endpoints.Probe.v1, GetSeedUserIdsRequest, GetSeedUserIdsResponse, CancellationToken, Task, GetSeedUserIdsRequestHandler, CancellationToken, IResult (+3 more)

### Community 35 - "ISearchLocalized"
Cohesion: 0.10
Nodes (13): Common.Application.Persistence.Outbox, CancellationToken, Task, IEventHandlerWrapper, CancellationToken, Task, IOutboxMessage, DateTimeOffset (+5 more)

### Community 36 - "StoreConfiguration"
Cohesion: 0.26
Nodes (8): CustomRateLimitingOptions, CustomRateLimitingOptionsValidator, FixedWindow, FixedWindowValidator, Action, IEnumerable, RateLimiterOptions, Policies

### Community 37 - "Setup"
Cohesion: 0.29
Nodes (6): Action, DateOnly, ILogger, LoggerMessage, Task, Seeder

### Community 38 - "Hangfire.PostgreSql"
Cohesion: 0.20
Nodes (7): PathString, IApplicationBuilder, HttpContext, IList, RequestDelegate, string, RequestResponseBodyLoggingMiddleware

### Community 39 - "EventDispatcher"
Cohesion: 0.17
Nodes (10): FullTextSearchOptions, FullTextSearchOptionsValidator, Dictionary, IReadOnlyList, string, CancellationToken, IOptions, RouteGroupBuilder (+2 more)

### Community 40 - "NetArchTest.Rules"
Cohesion: 0.29
Nodes (7): IRateLimiterPolicy, CancellationToken, Func, OnRejectedContext, ValueTask, CheckRegistrationRateLimitingPolicy, Policies

### Community 41 - "Aigamo.ResXGenerator"
Cohesion: 0.11
Nodes (16): IAM.Endpoints.Users.VersionNeutral.Search, Products.Endpoints.Products.v1.My.Get, Common.Application.DTOs, Products.Endpoints.Stores.v1.Get, Products.Endpoints.ProductTemplates.v1.Get, Products.Endpoints.Products.v1.Get, Products.Endpoints.Stores.v1.My.Get, AuditableEntityResponse (+8 more)

### Community 42 - "IOperationFilter"
Cohesion: 0.05
Nodes (38): ChangeTracker, DatabaseFacade, EntityEntry, IDisposable, Lock, IDbContext, CancellationToken, DbSet (+30 more)

### Community 43 - "IRateLimiterPolicy"
Cohesion: 0.15
Nodes (10): CancellationToken, Func, HttpContext, OnRejectedContext, RateLimiterOptions, RateLimitPartition, TimeSpan, ValueTask (+2 more)

### Community 44 - "Seeder"
Cohesion: 0.23
Nodes (5): ISearchLocalized, Product, IReadOnlyCollection, List, Store

### Community 45 - "AggregateRoot"
Cohesion: 0.16
Nodes (9): ProductTemplateId, CancellationToken, List, Task, Seeder, CancellationToken, List, Task (+1 more)

### Community 46 - "ApiVersionSet"
Cohesion: 0.29
Nodes (4): ApiVersionSet, Setup, IEndpointRouteBuilder, IServiceCollection

### Community 48 - "IntegrationTestFactory"
Cohesion: 0.14
Nodes (10): Notifications.Application.Sms, Notifications.Infrastructure.Sms, Notifications.Infrastructure.Telemetry, Notifications.Infrastructure.InterModuleRequestHandlers, Common.Infrastructure.Resiliency, Notifications.Infrastructure.Sms.NetGsm, Notifications.Infrastructure, Setup (+2 more)

### Community 50 - "Asp.Versioning.Http"
Cohesion: 0.08
Nodes (27): IConsumer, IntegrationEventHandlerBase, CancellationToken, ConsumeContext, DefaultIdType, ILogger, LoggerMessage, Task (+19 more)

### Community 51 - "IInterModuleRequestHandler"
Cohesion: 0.15
Nodes (9): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, CancellationToken, Task, ICaptchaService, Response, CancellationToken, Task, CachedCaptchaService (+1 more)

### Community 52 - "double"
Cohesion: 0.17
Nodes (11): DateTime, double, FormUrlEncodedContent, ReCaptchaResponse, CancellationToken, Exception, ILogger, LoggerMessage (+3 more)

### Community 53 - "RouteHandlerBuilderExtensions"
Cohesion: 0.31
Nodes (5): StoreId, CancellationToken, List, Task, Seeder

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
Cohesion: 0.09
Nodes (19): JsonConverter, StrictDateTimeOffsetJsonConverter, DateTimeOffset, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, StronglyTypedIdListReadOnlyJsonConverter (+11 more)

### Community 59 - "coverlet.collector"
Cohesion: 0.15
Nodes (12): StronglyTypedIdReadOnlyJsonConverter, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, StronglyTypedIdWriteOnlyJsonConverter, JsonSerializerOptions, Type (+4 more)

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
Cohesion: 0.14
Nodes (11): SaveChangesInterceptor, ApplyAuditingInterceptor, CancellationToken, DbContextEventData, InterceptionResult, ValueTask, ApplySearchLanguageInterceptor, CancellationToken (+3 more)

### Community 66 - "CustomRoles"
Cohesion: 0.16
Nodes (9): FrozenDictionary, IReadOnlySet, CustomPermissions, HashSet, IEnumerable, CancellationToken, RouteGroupBuilder, Task (+1 more)

### Community 67 - "AuthenticateResult"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 68 - "ValueObject"
Cohesion: 0.27
Nodes (4): Common.Domain, IComparable, ValueObject, IEnumerable

### Community 69 - "IResxLocalizer"
Cohesion: 0.13
Nodes (16): DomainEventHandlerBase, IIntegrationEventOutbox, CancellationToken, Task, V1SessionRevokedDomainEventHandler, CancellationToken, Task, V1UserRegisteredDomainEventHandler (+8 more)

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
Cohesion: 0.16
Nodes (11): Products.Endpoints.Stores.v1.My.RemoveProduct, Products.Endpoints.Stores.v1.RemoveProduct, ProductId, Request, RequestValidator, Request, RequestValidator, Request (+3 more)

### Community 76 - "HostCollection"
Cohesion: 0.67
Nodes (3): CorsOptions, CorsOptionsValidator, IReadOnlyList

### Community 77 - "Microsoft.AspNetCore.Identity.EntityFrameworkCore"
Cohesion: 0.13
Nodes (12): DbSet, IdentityRole, IdentityRoleClaim, IdentityUserClaim, IdentityUserLogin, IdentityUserRole, IdentityUserToken, IIAMDbContext (+4 more)

### Community 78 - "RequestLoggingPathPostConfigure"
Cohesion: 0.22
Nodes (8): CancellationToken, Func, HttpContext, OnRejectedContext, RateLimitPartition, ValueTask, Policies, TokenCreateRateLimitingPolicy

### Community 79 - "Endpoint"
Cohesion: 0.13
Nodes (10): Common.IntegrationEvents, Notifications.Application.IntegrationEventHandlers, IAM.Application.Users.DomainEventHandlers.v1, Products.Application.Stores.DomainEventHandlers.v1, Common.Application.EventBus, IEventHandler, CancellationToken, Task (+2 more)

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
Cohesion: 0.20
Nodes (7): Products.Domain.Products, Products.Domain.Stores.DomainEvents.v1, V1ProductAddedToStoreDomainEvent, V1ProductRemovedFromStoreDomainEvent, V1StoreAddressUpdatedDomainEvent, V1StoreDescriptionUpdatedDomainEvent, V1StoreNameUpdatedDomainEvent

### Community 93 - "IOpenApiSchema"
Cohesion: 0.18
Nodes (9): Products.Endpoints.Stores.v1.Search, CancellationToken, IOptions, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator (+1 more)

### Community 94 - "OtpServiceBase"
Cohesion: 0.15
Nodes (9): SemaphoreSlim, string, DummyOtpService, OtpService, CancellationToken, int, Task, TimeSpan (+1 more)

### Community 95 - ".SendAsync"
Cohesion: 0.28
Nodes (6): CancellationToken, int, string, Task, TimeSpan, RedisOtpService

### Community 96 - "OutboxCleanupSettings"
Cohesion: 0.24
Nodes (5): OutboxMessage, DateTimeOffset, TimeSpan, EntityTypeBuilder, OutboxMessageConfig

### Community 97 - "OutboxTestWebAppFactory"
Cohesion: 0.32
Nodes (5): ApiVersionDescription, IConfigureOptions, OpenApiInfo, ConfigureSwaggerOptions, SwaggerGenOptions

### Community 98 - "IDatabaseSeeder"
Cohesion: 0.18
Nodes (9): Products.Endpoints.Products.v1.Search, CancellationToken, IOptions, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator (+1 more)

### Community 99 - "HangfireCustomAuthorizationFilter"
Cohesion: 0.15
Nodes (7): DashboardContext, IDashboardAsyncAuthorizationFilter, CustomPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder, HangfireCustomAuthorizationFilter, Task

### Community 100 - "IdentityResultExtensions"
Cohesion: 0.18
Nodes (9): Products.Endpoints.Products.v1.My.Search, CancellationToken, IOptions, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator (+1 more)

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
Cohesion: 0.13
Nodes (13): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Products.v1.AuditLog, PaginationRequest, PaginationRequestValidator, int, int, Constants, Request (+5 more)

### Community 105 - "IModelBinder"
Cohesion: 0.20
Nodes (8): IFusionCache, CancellationToken, HttpContext, IOptions, RouteGroupBuilder, Task, TimeProvider, Endpoint

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
Cohesion: 0.29
Nodes (5): Outbox, Common.Infrastructure.Persistence.Outbox, Outbox.Persistence, Outbox.Telemetry, OutboxMetricsJob

### Community 113 - "ReverseProxyOptions"
Cohesion: 0.20
Nodes (8): Products.Endpoints.ProductTemplates.v1.Create, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator, Response

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
Cohesion: 0.33
Nodes (5): FeatureFlagResultExtensions, Action, Func, IFeatureManager, Task

### Community 126 - "Setup"
Cohesion: 0.33
Nodes (7): Exception, HttpContext, ILogger, LoggerMessage, RequestDelegate, Task, GlobalExceptionHandlingMiddleware

### Community 127 - "AuditLogEntry"
Cohesion: 0.67
Nodes (4): AuditableEntity, DateTimeOffset, IAuditableEntity, DateTimeOffset

### Community 128 - "Endpoint"
Cohesion: 0.09
Nodes (16): Common.Application.JsonConverters, IAM.Infrastructure.Persistence.EntityConfigurations, Common.Infrastructure.Persistence.EntityConfigurations, Common.Infrastructure.Persistence.ValueConverters, Products.Infrastructure.Persistence.EntityConfigurations, DomainEventConverter, JsonSerializerOptions, IntegrationEventConverter (+8 more)

### Community 129 - "Setup"
Cohesion: 0.18
Nodes (7): Products.Endpoints.Products, RouteGroupBuilder, Setup, CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 130 - "Setup"
Cohesion: 0.16
Nodes (17): Configuration-Driven Module Loading, IntegrationEvents (Async Cross-Module), IAM Module, Notifications Module, Products Module, Observability (OpenTelemetry), docker-compose.yml (Base Stack), docker-compose.app.yml (App-Only) (+9 more)

### Community 131 - "Setup"
Cohesion: 0.18
Nodes (12): ModulesOptions, ModulesOptionsValidator, IReadOnlyList, OpenApiOptions, OpenApiOptionsValidator, OtpOptions, OtpOptionsValidator, OutboxCleanupSettings (+4 more)

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
Cohesion: 0.20
Nodes (7): ICurrentUser, Guid, ICollection, CancellationToken, RouteGroupBuilder, Task, Endpoint

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
Cohesion: 0.24
Nodes (7): Task, Seeder, IdentityRole, ILogger, LoggerMessage, Task, Seeder

### Community 144 - "OtpOptions"
Cohesion: 0.20
Nodes (7): IAM.Endpoints.Users.VersionNeutral.Get, CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, Response

### Community 145 - ".AddOrUpdate"
Cohesion: 0.20
Nodes (6): IAM.Infrastructure.Auth, IAM.Application.Auth, string, CustomClaims, string, MultiAuthDefaults

### Community 146 - "SecurityHeadersOptions"
Cohesion: 0.24
Nodes (4): Func, Task, CancellationToken, Task

### Community 147 - "SignalROptions"
Cohesion: 0.22
Nodes (6): IInterModuleRequestClient, CancellationToken, Task, MassTransitInterModuleRequestClient, CancellationToken, Task

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
Cohesion: 0.13
Nodes (12): IInterModuleRequest, InterModuleRequestHandler, CancellationToken, ConsumeContext, Task, OtpVerificationFailureReason, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse (+4 more)

### Community 156 - "CurrentUser"
Cohesion: 0.10
Nodes (17): Products.Endpoints.ProductTemplates.v1.Deactivate, Common.Application.ModelBinders, Products.Endpoints.ProductTemplates.v1.Activate, IModelBinder, ModelBindingContext, StronglyTypedIdBinder, Task, Request (+9 more)

### Community 157 - "Setup"
Cohesion: 0.39
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, OutboxCleanupJob

### Community 158 - "Endpoint"
Cohesion: 0.16
Nodes (10): DbSet, IProductsDbContext, IReadOnlyList, List, ProductTemplate, EntityTypeBuilder, ProductTemplateConfiguration, DbSet (+2 more)

### Community 159 - "IAM.Endpoints"
Cohesion: 0.24
Nodes (6): Products.Endpoints.Stores.v1.Create, CancellationToken, Task, Request, RequestValidator, Response

### Community 160 - "ValidationContextExtensions"
Cohesion: 0.40
Nodes (3): ValidationContextExtensions, string, ValidationContext

### Community 161 - "Endpoint"
Cohesion: 0.31
Nodes (5): CancellationToken, Task, TimeSpan, IOtpService, OtpVerificationOutcome

### Community 162 - ".ForUser"
Cohesion: 0.29
Nodes (6): AuthorizationHandler, AuthorizationHandlerContext, IAuthorizationRequirement, Task, PermissionAuthorizationHandler, PermissionRequirement

### Community 163 - "Endpoint"
Cohesion: 0.36
Nodes (4): CollectionExtensions, Func, ICollection, IEnumerable

### Community 164 - "Endpoint"
Cohesion: 0.14
Nodes (7): Host.Middlewares, IApplicationBuilder, IServiceCollection, Setup, HttpContext, Task, SecurityHeadersMiddleware

### Community 165 - "Endpoint"
Cohesion: 0.17
Nodes (8): Common.Infrastructure.Auth.Services, Common.Infrastructure.Auth, CurrentUser, Guid, HashSet, ICollection, Setup, IServiceCollection

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
Cohesion: 0.06
Nodes (28): Products.Endpoints.Stores.v1.My.AuditLog, AuditLogDto, PaginationResponse, DbContextExtensions, CancellationToken, DbSet, JsonSerializerOptions, Task (+20 more)

### Community 176 - "FeatureManagement/RouteHandlerBuilderExtensions.cs"
Cohesion: 0.48
Nodes (6): AbstractValidator, ApiKeyEntry, ApiKeyEntryValidator, ApiKeysOptions, ApiKeysOptionsValidator, IReadOnlyList

### Community 177 - "SendRequestBody"
Cohesion: 0.48
Nodes (4): AuthorizationPolicy, IAuthorizationPolicyProvider, Task, PermissionPolicyProvider

### Community 178 - "Endpoint"
Cohesion: 0.33
Nodes (6): Products.Endpoints.Products.v1.Update, RequestBody, Request, RequestBody, RequestBodyValidator, RequestValidator

### Community 179 - "Endpoint"
Cohesion: 0.52
Nodes (6): CachingEntryDefaults, CachingOptions, CachingOptionsValidator, Redis, RedisValidator, TimeSpan

### Community 180 - "SmsOptions.cs"
Cohesion: 0.23
Nodes (9): SmsOptions, SmsOptionsValidator, SmsProvider, SmsTemplatesOptions, Dictionary, IConfiguration, IServiceCollection, long (+1 more)

### Community 181 - "HttpContextExtensions.cs"
Cohesion: 0.12
Nodes (15): IAM.Application.Tokens.Services, Common.Infrastructure.RateLimiting, IAM.Endpoints, Common.Infrastructure.Extensions, Common.Application.Options, IAM.Infrastructure.Tokens, IAM.Endpoints.Tokens.VersionNeutral.Revoke, IAM.Infrastructure.Tokens.Services (+7 more)

### Community 182 - "Endpoint"
Cohesion: 0.33
Nodes (4): IAM.Domain, string, Constants, IAssemblyReference

### Community 185 - "Endpoint"
Cohesion: 0.18
Nodes (8): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, IamModule

### Community 186 - "Endpoint"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

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
Cohesion: 0.40
Nodes (3): Products.Endpoints.ProductTemplates, RouteGroupBuilder, Setup

### Community 196 - "Response"
Cohesion: 0.40
Nodes (4): IAM.Endpoints.Users.VersionNeutral.Me.Get, DateOnly, IReadOnlyCollection, Response

### Community 197 - "IInterModuleRequestHandler"
Cohesion: 0.40
Nodes (3): IInterModuleRequestHandler, CancellationToken, Task

### Community 198 - "Revoke/Request.cs"
Cohesion: 0.67
Nodes (3): IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, Request, RequestValidator

### Community 200 - "JwtOptions"
Cohesion: 0.67
Nodes (3): JwtOptions, JwtOptionsValidator, IReadOnlyCollection

### Community 201 - "SecurityHeadersOptions.cs"
Cohesion: 0.67
Nodes (3): SecurityHeadersOptions, SecurityHeadersOptionsValidator, Dictionary

### Community 203 - "IAssemblyReference"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 205 - "Request.cs"
Cohesion: 0.15
Nodes (7): Common.Infrastructure.Modules, Host.Infrastructure, OtlpExportProtocol, IConfiguration, IServiceCollection, Setup, StringExtensions

### Community 206 - "IAssemblyReference"
Cohesion: 0.15
Nodes (9): DbContext, IOutboxDbContext, CancellationToken, DbSet, Task, DbSet, ModelBuilder, ModelConfigurationBuilder (+1 more)

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
Cohesion: 0.14
Nodes (10): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint, ActivitySource, Counter, Meter (+2 more)

### Community 225 - "Products.Endpoints.Probe"
Cohesion: 0.20
Nodes (6): Common.Endpoints.Versioning, Products.Endpoints.Probe, Products.Endpoints, IAssemblyReference, RouteGroupBuilder, Setup

### Community 228 - "IAM"
Cohesion: 0.12
Nodes (15): Products.Endpoints.Stores.v1.My.Update, Common.Application.Validation, BackgroundJobsOptions, BackgroundJobsOptionsValidator, DatabaseOptions, DatabaseOptionsValidator, InterModuleRequestOptions, InterModuleRequestOptionsValidator (+7 more)

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
Nodes (3): AuthenticationBuilder, IConfiguration, Setup

## Knowledge Gaps
- **135 isolated node(s):** `OtpCacheEntry`, `Common.Domain`, `Common.Infrastructure`, `IAssemblyReference`, `IAssemblyReference` (+130 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **100 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `HttpContextExtensions.cs` to `Endpoint`, `Setup`, `Host NuGet Deps (OTel/Health)`, `BackgroundJobs Service (Hangfire)`, `Launch Settings`, `Authz Constants & Feature Flags`, `SignalR Hub & Exception Middleware`, `.GetMeAsync`, `Telemetry (ActivitySource/Meter)`, `Host.Swagger`, `Integration Event Handler Base`, `StoreConfiguration`, `Endpoint`, `EventDispatcher`, `.Get`, `Endpoint`, `FeatureManagement/RouteHandlerBuilderExtensions.cs`, `IntegrationTestFactory`, `Endpoint`, `SmsOptions.cs`, `Auditing/Setup.cs`, `ReCaptchaResponse`, `Endpoint`, `ISearchLanguageResolver`, `.AddCustomMassTransit`, `JwtOptions`, `SecurityHeadersOptions.cs`, `HostCollection`, `HealthCheckOptions.cs`, `Request.cs`, `Endpoint`, `BaseIntegrationTest`, `net10.0`, `AuditLogRetentionJobRegistrar`, `HttpClient`, `OutboxTestWebAppFactory`, `Products.Endpoints.Probe`, `IAM`, `IntegrationEvent`, `StringExtensions`, `IRoleService`, `RequestBody`, `Response`, `Setup`?**
  _High betweenness centrality (0.382) - this node is a cross-community bridge._
- **Why does `Common.Domain.StronglyTypedIds` connect `Functional Result Extensions` to `Endpoint`, `IAM User Identity & Auditing`, `.GetMyStoreAsync`, `BackgroundJobs Service (Hangfire)`, `OtpOptions`, `SignalR Hub & Exception Middleware`, `Integration Event Handler Base`, `CurrentUser`, `IAM.Endpoints`, `Microsoft.EntityFrameworkCore.Abstractions`, `Endpoint`, `Aigamo.ResXGenerator`, `Endpoint`, `Asp.Versioning.Http`, `HttpContextExtensions.cs`, `coverlet.collector`, `DummyOtpService`, `Response`, `Endpoint`, `IOpenApiSchema`, `IDatabaseSeeder`, `ReverseProxyOptions.cs`, `My/Search/Request.cs`?**
  _High betweenness centrality (0.096) - this node is a cross-community bridge._
- **Why does `Result` connect `MassTransit & DI Setup` to `Setup`, `.RemoveProductAsync`, `HostEnvironmentExtensions.cs`, `BackgroundJobsOptions`, `IAM Error Catalogs`, `.GetMyStoreAsync`, `IAM OTP Verify & Token Endpoint`, `Project Files & Solution`, `ModulesOptions`, `OtpOptions`, `SecurityHeadersOptions`, `Outbox Processor & Seeder`, `EF Core DbContexts`, `.UpdateStoreAsync`, `VerifyPhoneOtpRequest`, `Product Template Aggregate`, `Endpoint`, `IAM.Endpoints`, `PaginationRequestValidator`, `Endpoint`, `EventDispatcher`, `Endpoint`, `IInterModuleRequestHandler`, `double`, `DummyOtpService`, `Endpoint`, `decimal`, `.UpdateMyProductAsync`, `.RemoveMyProductAsync`, `CustomRoles`, `AuthenticateResult`, `.UpdateMyStoreAsync`, `OutboxModule`, `.ToResult`, `IAggregateRoot`, `IAssemblyReference`, `IAssemblyReference`, `Activity`, `.DeactivateProductTemplateAsync`, `HostTestFactory`, `.RevokeAllSessions`, `IOpenApiSchema`, `IDatabaseSeeder`, `IdentityResultExtensions`, `IModelBinder`, `RequestBody`, `ReverseProxyOptions`, `Endpoint`?**
  _High betweenness centrality (0.094) - this node is a cross-community bridge._
- **What connects `OtpCacheEntry`, `Common.Domain`, `Common.Infrastructure` to the rest of the system?**
  _135 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `IAM User Identity & Auditing` be split into smaller, more focused modules?**
  _Cohesion score 0.1396011396011396 - nodes in this community are weakly interconnected._
- **Should `Cross-Module Comm & Arch Audit Skills` be split into smaller, more focused modules?**
  _Cohesion score 0.14039408866995073 - nodes in this community are weakly interconnected._
- **Should `Domain Event Handling & Outbox Collect` be split into smaller, more focused modules?**
  _Cohesion score 0.1067193675889328 - nodes in this community are weakly interconnected._