# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-07-20)

## Corpus Check
- 441 files · ~61,220 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2772 nodes · 4871 edges · 286 communities (186 shown, 100 thin omitted)
- Extraction: 98% EXTRACTED · 2% INFERRED · 0% AMBIGUOUS · INFERRED: 76 edges (avg confidence: 0.8)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `24a4dd2c`
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
- ISmsService
- Request.cs
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
- .RemoveMyProductAsync
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
- DummySmsGateway
- RequestBody
- RequestBody
- Request.cs
- Setup
- Setup
- Setup
- Setup
- Endpoint
- Setup
- Setup
- Endpoint
- Setup
- Setup
- Setup
- .ToResult
- Setup
- HostEnvironmentExtensions.cs
- BackgroundJobsOptions
- CaptchaOptions
- CorsOptions
- DatabaseOptions
- HealthCheckOptions
- SecurityHeadersOptions.cs
- ModulesOptions
- OtpOptions
- SecurityHeadersOptions
- SignalROptions
- Setup
- AutoMigrateMarker
- Setup
- Endpoint
- VerifyPhoneOtpRequest
- CurrentUser
- Setup
- Endpoint
- Endpoint
- Endpoint
- Endpoint
- Endpoint
- Setup
- Endpoint
- Endpoint
- Endpoint
- Endpoint
- Endpoint
- Endpoint
- Endpoint
- Endpoint
- Endpoint
- Endpoint
- Endpoint
- Endpoint
- Endpoint
- Setup
- NameFor
- Sync AI Settings Command
- IAssemblyReference
- IAssemblyReference
- IAssemblyReference
- .GetProductTemplateAsync
- Request.cs
- IAssemblyReference
- .LogDispatchingNotification
- IAssemblyReference
- IAssemblyReference
- .DeactivateProductTemplateAsync
- Request.cs
- .RemoveProductAsync
- .RevokeAllSessions
- Host.Swagger
- NotificationsTelemetry
- Setup.cs
- RequestValidator
- Products.Endpoints.Probe
- IAM
- IntegrationEvent
- AccessTokenDto
- IAM.Endpoints.Otp.VersionNeutral
- DefaultResponsesOperationFilter
- SendPhoneOtp
- Setup
- Response
- Response
- Response
- SecurityHeadersOptions.cs
- Response
- Response
- Response
- Response
- Response
- Response
- Response
- .CreateStorePolicy
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
1. `Common.Application.Options` - 86 edges
2. `Result` - 77 edges
3. `Common.Domain.StronglyTypedIds` - 68 edges
4. `Common.Domain.ResultMonad` - 62 edges
5. `Common.Application.Auth` - 58 edges
6. `CustomValidator` - 58 edges
7. `Common.Application.Extensions` - 52 edges
8. `ApplicationUserId` - 52 edges
9. `Common.Application.Validation` - 51 edges
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

## Communities (286 total, 100 thin omitted)

### Community 0 - "Host Logging & Serilog Setup"
Cohesion: 0.12
Nodes (11): Common.Application.Search, IAM.Endpoints, Common.Infrastructure.Extensions, Common.Application.Options, IAM.Infrastructure.RateLimiting, Common.Application.Pagination, IAM.Infrastructure.Captcha, IAssemblyReference (+3 more)

### Community 1 - "IAM User Identity & Auditing"
Cohesion: 0.13
Nodes (19): IEntityTypeConfiguration, ApplicationUserId, DefaultIdType, DateTimeOffset, ApplicationUser, EntityTypeBuilder, IdentityRole, IdentityRoleClaim (+11 more)

### Community 2 - "Products Store & Audit Services"
Cohesion: 0.15
Nodes (11): BackgroundService, DatabaseSeederOrchestrator, CancellationToken, ILogger, LoggerMessage, Task, SeedingCompletionTracker, CancellationToken (+3 more)

### Community 3 - "Notifications Dispatch & SignalR Client"
Cohesion: 0.33
Nodes (5): Memory, ReadOnlyMemory, CancellationToken, Task, ValueTask

### Community 5 - "Cross-Module Comm & Arch Audit Skills"
Cohesion: 0.21
Nodes (8): DateTimeOffset, Guid, DateTimeOffset, Guid, IReadOnlyCollection, List, Session, SessionRevokedReason

### Community 6 - "Domain Event Handling & Outbox Collect"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 7 - "Host NuGet Deps (OTel/Health)"
Cohesion: 0.10
Nodes (17): IAM.Endpoints.Users.VersionNeutral.SelfRegister, IAM.Application.Tokens.Services, IAM.Application.Extensions, IAM.Endpoints.Otp, IAM.Domain.Identity, IAM.Endpoints.Tokens.VersionNeutral.Revoke, Common.Domain.Extensions, IAM.Infrastructure.Telemetry (+9 more)

### Community 8 - "k6 Load Test Scripts"
Cohesion: 0.30
Nodes (7): CancellationToken, Exception, ILogger, LoggerMessage, Task, TimeSpan, OutboxProcessor

### Community 9 - "REPR Request Validators"
Cohesion: 0.16
Nodes (16): AbstractValidator, Products.Endpoints.Stores.v1.Update, Products.Endpoints.Products.v1.My.Update, CustomValidator, RequestValidator, RequestBody, Request, RequestBody (+8 more)

### Community 10 - "IAM Error Catalogs"
Cohesion: 0.12
Nodes (11): HttpStatusCode, IStringLocalizer, StringLocalizerExtensions, Error, ICollection, IResult, CaptchaErrors, ICollection (+3 more)

### Community 11 - "BackgroundJobs Service (Hangfire)"
Cohesion: 0.20
Nodes (9): Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Products.Domain.Products, Products.Infrastructure.Telemetry, Products.Application.Persistence, Products.Domain.Stores, Products.Domain.ProductTemplates (+1 more)

### Community 12 - "IAM OTP Verify & Token Endpoint"
Cohesion: 0.13
Nodes (17): IInterModuleRequestClient, CancellationToken, Task, ITokenService, CancellationToken, HttpContext, IFeatureManager, IOptions (+9 more)

### Community 13 - "Project Files & Solution"
Cohesion: 0.15
Nodes (9): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, CancellationToken, Task, ICaptchaService, Response, CancellationToken, Task, CachedCaptchaService (+1 more)

### Community 14 - "Localized Identity Errors"
Cohesion: 0.15
Nodes (3): IdentityError, IdentityErrorDescriber, LocalizedIdentityErrorDescriber

### Community 15 - "Functional Result Extensions"
Cohesion: 0.16
Nodes (8): Common.Domain.StronglyTypedIds, IAM.Domain.Identity.DomainEvents.v1, Common.Domain.Events, IAM.Domain.Identity.Sessions, Common.Domain.Entities, Common.Domain.Aggregates, IAuditableEntity, DateTimeOffset

### Community 16 - "Launch Settings"
Cohesion: 0.08
Nodes (14): Common.Application.FeatureManagement, IAM.Domain.Captcha, Common.InterModuleRequests.Contracts, IAM.Infrastructure.Captcha.Services, IAM.Application.Captcha.Services, IAM.Infrastructure.InterModuleRequestHandlers, DateTime, RouteHandlerBuilderExtensions (+6 more)

### Community 17 - "Module Installers (IModule)"
Cohesion: 0.14
Nodes (11): RecurringJobOptions, IRecurringBackgroundJobs, Action, Expression, Func, Task, RecurringBackgroundJobsService, Action (+3 more)

### Community 18 - "Host Infrastructure Setup"
Cohesion: 0.21
Nodes (6): Products.Domain.Stores.DomainEvents.v1, V1ProductAddedToStoreDomainEvent, V1ProductRemovedFromStoreDomainEvent, V1StoreAddressUpdatedDomainEvent, V1StoreDescriptionUpdatedDomainEvent, V1StoreNameUpdatedDomainEvent

### Community 19 - "IAM OTP Send & Captcha"
Cohesion: 0.32
Nodes (6): UserRegisteredIntegrationEvent, CancellationToken, ILogger, LoggerMessage, Task, UserRegisteredSignalRHandler

### Community 20 - "Authz Constants & Feature Flags"
Cohesion: 0.09
Nodes (18): IAM.Endpoints.Otp.VersionNeutral.SendForLogin, IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, IAM.Endpoints.Common.Validations, IResxLocalizer, IRuleBuilder, IRuleBuilderOptions, CommonValidations, IResxLocalizer (+10 more)

### Community 21 - "Bounded Capture Streams"
Cohesion: 0.12
Nodes (8): bool, byte, ReadOnlySpan, SeekOrigin, int, BoundedCaptureStream, BoundedRequestCaptureStream, Stream

### Community 22 - "SignalR Hub & Exception Middleware"
Cohesion: 0.05
Nodes (26): Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Common.Application.Persistence, IAM.Infrastructure.Persistence, IAM.Infrastructure.Persistence.Seeding, IAM.Infrastructure.Identity, Common.Infrastructure.Persistence.DbContext, IDatabaseSeeder (+18 more)

### Community 23 - "Telemetry (ActivitySource/Meter)"
Cohesion: 0.14
Nodes (9): Notifications.Infrastructure.Sms, Notifications.Application, IAssemblyReference, Task, ISmsService, Task, DummySmsService, IServiceCollection (+1 more)

### Community 24 - "Outbox Processor & Seeder"
Cohesion: 0.12
Nodes (14): IAM.Endpoints.Tokens.VersionNeutral.Refresh, CancellationToken, HttpContext, ILogger, IOptions, LoggerMessage, RouteGroupBuilder, Task (+6 more)

### Community 25 - "EF Core DbContexts"
Cohesion: 0.10
Nodes (17): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Products.v1.AuditLog, PaginationRequest, PaginationRequestValidator, int, int, Constants, Request (+9 more)

### Community 26 - "Integration Event Handler Base"
Cohesion: 0.47
Nodes (3): Outbox, Common.Infrastructure.Persistence.Outbox, Outbox.Persistence

### Community 27 - "Product Template Aggregate"
Cohesion: 0.18
Nodes (9): Products.Endpoints.Products.v1.My.Search, CancellationToken, IOptions, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator (+1 more)

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
Cohesion: 0.20
Nodes (8): LoadAll, Names, Assembly, IConfiguration, IEnumerable, IReadOnlyList, IServiceCollection, Type

### Community 32 - "CustomRateLimitingOptions"
Cohesion: 0.09
Nodes (21): IAM.Endpoints.Users.VersionNeutral.Search, Products.Endpoints.Products.v1.My.Get, Products.Endpoints.ProductTemplates.v1.Search, Common.Application.DTOs, IAM.Endpoints.Users.VersionNeutral.Get, Products.Endpoints.Stores.v1.Get, Products.Endpoints.ProductTemplates.v1.Get, Products.Endpoints.Products.v1.Get (+13 more)

### Community 33 - "PaginationRequestValidator"
Cohesion: 0.20
Nodes (10): SendPhoneOtpRequest, SendPhoneOtpResponse, CancellationToken, IFeatureManager, RouteGroupBuilder, Task, Endpoint, CancellationToken (+2 more)

### Community 34 - "Microsoft.EntityFrameworkCore.Abstractions"
Cohesion: 0.22
Nodes (6): Products.Infrastructure.Persistence.Seeding, Common.InterModuleRequests.IAM, ILogger, int, LoggerMessage, Seeder

### Community 35 - "ISearchLocalized"
Cohesion: 0.29
Nodes (4): RouteGroupBuilder, Setup, RouteGroupBuilder, Endpoint

### Community 36 - "StoreConfiguration"
Cohesion: 0.26
Nodes (8): CustomRateLimitingOptions, CustomRateLimitingOptionsValidator, FixedWindow, FixedWindowValidator, Action, IEnumerable, RateLimiterOptions, Policies

### Community 37 - "Setup"
Cohesion: 0.10
Nodes (16): IEnumerable, IReadOnlyCollection, List, ApplicationUser, Task, IdentityRole, ILogger, LoggerMessage (+8 more)

### Community 38 - "Hangfire.PostgreSql"
Cohesion: 0.24
Nodes (6): Products.Endpoints.Stores.v1.Create, CancellationToken, Task, Request, RequestValidator, Response

### Community 39 - "EventDispatcher"
Cohesion: 0.32
Nodes (6): Exception, IApplicationBuilder, ILogger, LoggerMessage, WebApplication, Setup

### Community 40 - "NetArchTest.Rules"
Cohesion: 0.20
Nodes (8): ChangeTracker, DatabaseFacade, EntityEntry, IDisposable, IDbContext, CancellationToken, DbSet, Task

### Community 41 - "Aigamo.ResXGenerator"
Cohesion: 0.18
Nodes (10): IdentityDbContext, DbSet, IdentityRole, IdentityRoleClaim, IdentityUserClaim, IdentityUserLogin, IdentityUserRole, IdentityUserToken (+2 more)

### Community 42 - "IOperationFilter"
Cohesion: 0.10
Nodes (21): Lock, EventDispatcher, ActivitySource, CancellationToken, ILogger, LoggerMessage, Task, IntegrationEventOutbox (+13 more)

### Community 43 - "IRateLimiterPolicy"
Cohesion: 0.06
Nodes (30): IRateLimiterPolicy, CancellationToken, Func, HttpContext, OnRejectedContext, RateLimitPartition, ValueTask, RegisterRateLimitingPolicy (+22 more)

### Community 44 - "Seeder"
Cohesion: 0.23
Nodes (5): ISearchLocalized, Product, IReadOnlyCollection, List, Store

### Community 45 - "AggregateRoot"
Cohesion: 0.31
Nodes (5): ProductTemplateId, CancellationToken, List, Task, Seeder

### Community 46 - "ApiVersionSet"
Cohesion: 0.25
Nodes (5): ApiVersionSet, Common.Endpoints.Versioning, Setup, IEndpointRouteBuilder, IServiceCollection

### Community 48 - "IntegrationTestFactory"
Cohesion: 0.25
Nodes (5): Products.Endpoints.ProductTemplates, RouteGroupBuilder, Setup, RouteGroupBuilder, Endpoint

### Community 50 - "Asp.Versioning.Http"
Cohesion: 0.31
Nodes (6): DateTimeOffset, RefreshToken, RefreshTokenId, SessionId, EntityTypeBuilder, RefreshTokenConfig

### Community 51 - "IInterModuleRequestHandler"
Cohesion: 0.07
Nodes (22): Products.Endpoints.Probe.v1, IConsumer, IInterModuleRequest, IInterModuleRequestHandler, CancellationToken, Task, InterModuleRequestHandler, CancellationToken (+14 more)

### Community 52 - "double"
Cohesion: 0.22
Nodes (9): double, FormUrlEncodedContent, ReCaptchaResponse, CancellationToken, Exception, ILogger, LoggerMessage, Task (+1 more)

### Community 53 - "RouteHandlerBuilderExtensions"
Cohesion: 0.15
Nodes (11): StoreId, CancellationToken, Task, CancellationToken, List, Task, Seeder, CancellationToken (+3 more)

### Community 54 - "IList"
Cohesion: 0.20
Nodes (7): PathString, IApplicationBuilder, HttpContext, IList, RequestDelegate, string, RequestResponseBodyLoggingMiddleware

### Community 55 - "Microsoft.AspNetCore.SignalR.StackExchangeRedis"
Cohesion: 0.28
Nodes (6): Products.Endpoints.ProductTemplates.v1.Create, CancellationToken, Task, Request, RequestValidator, Response

### Community 56 - "DummyOtpService"
Cohesion: 0.09
Nodes (18): FullTextSearchOptions, FullTextSearchOptionsValidator, Dictionary, IReadOnlyList, string, ISearchLanguageResolver, SearchLanguageResolver, string (+10 more)

### Community 58 - "CollectionExtensions"
Cohesion: 0.18
Nodes (7): Common.Application.BackgroundJobs, BackgroundJobs.Telemetry, BackgroundJobs, IServerFilter, PerformingContext, JobMetricsFilter, string

### Community 59 - "coverlet.collector"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 61 - "decimal"
Cohesion: 0.14
Nodes (11): Products.Endpoints.Stores.v1.My.AddProduct, decimal, int, Constants, CancellationToken, RouteGroupBuilder, Task, Endpoint (+3 more)

### Community 62 - "EndpointFilterDelegate"
Cohesion: 0.11
Nodes (16): Common.Application.EndpointFilters, IEndpointFilter, ResultToCreatedResponseTransformer, ResultToResponseTransformer, EndpointFilterDelegate, EndpointFilterInvocationContext, ValueTask, RouteHandlerBuilderExtensions (+8 more)

### Community 63 - "Hangfire"
Cohesion: 0.29
Nodes (4): IAM.Infrastructure.Tokens, IAM.Infrastructure.Tokens.Services, IServiceCollection, Setup

### Community 64 - "Seeder"
Cohesion: 0.28
Nodes (6): CancellationToken, int, string, Task, TimeSpan, RedisOtpService

### Community 65 - "ApplyAuditingInterceptor"
Cohesion: 0.14
Nodes (11): SaveChangesInterceptor, ApplyAuditingInterceptor, CancellationToken, DbContextEventData, InterceptionResult, ValueTask, ApplySearchLanguageInterceptor, CancellationToken (+3 more)

### Community 66 - "CustomRoles"
Cohesion: 0.09
Nodes (16): Common.Infrastructure.Auth.Services, Common.Infrastructure.Auth, FrozenDictionary, IReadOnlySet, CustomPermissions, HashSet, IEnumerable, CurrentUser (+8 more)

### Community 67 - "AuthenticateResult"
Cohesion: 0.25
Nodes (7): Products.Endpoints.Stores.v1.AddProduct, RequestBody, Request, RequestBody, RequestBodyValidator, RequestValidator, Response

### Community 68 - "ValueObject"
Cohesion: 0.27
Nodes (4): Common.Domain, IComparable, ValueObject, IEnumerable

### Community 69 - "IResxLocalizer"
Cohesion: 0.06
Nodes (31): JsonConverter, StrictDateTimeOffsetJsonConverter, DateTimeOffset, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, StronglyTypedIdListReadOnlyJsonConverter (+23 more)

### Community 70 - "OutboxModule"
Cohesion: 0.32
Nodes (8): IntegrationEventHandlerBase, CancellationToken, ConsumeContext, DefaultIdType, ILogger, LoggerMessage, Task, TimeSpan

### Community 71 - "V1ProductCreatedDomainEvent"
Cohesion: 0.08
Nodes (25): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Add a new language/culture, Add search to a new entity _(Build checklist)_ (+17 more)

### Community 72 - "IAggregateRoot"
Cohesion: 0.20
Nodes (8): Products.Endpoints.Stores.v1.My.Create, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator, Response

### Community 73 - "ApiVersionDescription"
Cohesion: 0.36
Nodes (4): CollectionExtensions, Func, ICollection, IEnumerable

### Community 75 - "GetSeedUserIdsRequest"
Cohesion: 0.14
Nodes (13): Products.Endpoints.Products.v1.Update, Products.Endpoints.Stores.v1.RemoveProduct, ProductId, Request, RequestValidator, Request, RequestValidator, RequestBody (+5 more)

### Community 76 - "HostCollection"
Cohesion: 0.29
Nodes (5): CancellationToken, IFeatureManager, RouteGroupBuilder, Task, Endpoint

### Community 77 - "Microsoft.AspNetCore.Identity.EntityFrameworkCore"
Cohesion: 0.31
Nodes (6): IdentityUser, DateOnly, IReadOnlyCollection, List, ApplicationUser, Uri

### Community 78 - "RequestLoggingPathPostConfigure"
Cohesion: 0.22
Nodes (9): IPostConfigureOptions, RequestLoggingOptions, RequestLoggingOptionsValidator, SensitivePathRule, IList, int, IServiceCollection, RequestLoggingPathPostConfigure (+1 more)

### Community 79 - "Endpoint"
Cohesion: 0.13
Nodes (10): Common.IntegrationEvents, Notifications.Application.IntegrationEventHandlers, IAM.Application.Users.DomainEventHandlers.v1, Common.Infrastructure.EventBus, Common.Application.EventBus, IEventHandler, CancellationToken, Task (+2 more)

### Community 81 - "HttpContextExtensions"
Cohesion: 0.12
Nodes (14): Outbox.Telemetry, long, ObservableGauge, CancellationToken, ILogger, LoggerMessage, Task, OutboxMetricsJob (+6 more)

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
Cohesion: 0.07
Nodes (21): Common.Infrastructure.Persistence.Auditing, Common.Infrastructure.Persistence.AuditLog, IHostedService, Setup, IServiceCollection, AuditLogRetentionJobRegistrar, CancellationToken, ILogger (+13 more)

### Community 87 - "AuditLogRetentionService"
Cohesion: 0.24
Nodes (8): Action, Exception, IApplicationBuilder, IEndpointRouteBuilder, IEnumerable, ILogger, LoggerMessage, OutboxModule

### Community 89 - "ConfigurationManager"
Cohesion: 0.33
Nodes (4): ConfigurationManager, Host.Configurations, Setup, WebApplicationBuilder

### Community 90 - "HostTestFactory"
Cohesion: 0.13
Nodes (13): IAM.Endpoints.Tokens.VersionNeutral.Create, accessToken, DateTimeOffset, expiresAt, ICollection, refreshTokenBytes, CancellationToken, HttpContext (+5 more)

### Community 91 - "HttpClient"
Cohesion: 0.22
Nodes (7): Common.Infrastructure.Resiliency, HttpClient, HttpStandardResilienceOptions, IHttpClientBuilder, Setup, Action, IServiceCollection

### Community 92 - "IntegrationTestWebAppFactory"
Cohesion: 0.36
Nodes (6): AuditableEntity, DateTimeOffset, AuditLogEntry, DefaultIdType, AuditLogEntryConfiguration, EntityTypeBuilder

### Community 93 - "IOpenApiSchema"
Cohesion: 0.10
Nodes (19): Products.Application.Stores.DomainEventHandlers.v1, DomainEventHandlerBase, CancellationToken, Task, IIntegrationEventOutbox, CancellationToken, Task, V1SessionRevokedDomainEventHandler (+11 more)

### Community 95 - "Request.cs"
Cohesion: 0.33
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, UserRegisteredSmsHandler

### Community 96 - "OutboxCleanupSettings"
Cohesion: 0.09
Nodes (16): Common.Application.Persistence.Outbox, IOutboxMessage, DateTimeOffset, OutboxMessage, DateTimeOffset, TimeSpan, IEvent, DateTimeOffset (+8 more)

### Community 97 - "OutboxTestWebAppFactory"
Cohesion: 0.10
Nodes (17): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, DbSet, IdentityRole, IdentityRoleClaim, IdentityUserClaim, IdentityUserLogin, IdentityUserRole, IdentityUserToken (+9 more)

### Community 98 - "IDatabaseSeeder"
Cohesion: 0.40
Nodes (4): IAM.Endpoints.Users.VersionNeutral.Me.Get, DateOnly, IReadOnlyCollection, Response

### Community 99 - "HangfireCustomAuthorizationFilter"
Cohesion: 0.15
Nodes (7): DashboardContext, IDashboardAsyncAuthorizationFilter, CustomPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder, HangfireCustomAuthorizationFilter, Task

### Community 100 - "IdentityResultExtensions"
Cohesion: 0.08
Nodes (24): Products.Domain.Products.DomainEvents.v1, AggregateRoot, IEnumerable, IReadOnlyCollection, List, IAggregateRoot, IEnumerable, IReadOnlyCollection (+16 more)

### Community 101 - "Setup.GlobalExceptionHandlingMiddleware.cs"
Cohesion: 0.33
Nodes (3): IApplicationBuilder, IServiceCollection, Setup

### Community 102 - "HttpContextTargetingContextAccessor"
Cohesion: 0.17
Nodes (8): Common.Infrastructure.FeatureManagement, ITargetingContextAccessor, HttpContextTargetingContextAccessor, ValueTask, Setup, IConfiguration, IServiceCollection, TargetingContext

### Community 103 - "IVariantFeatureManagerExtensions"
Cohesion: 0.33
Nodes (4): IVariantFeatureManager, IVariantFeatureManagerExtensions, CancellationToken, Task

### Community 104 - "IMiddleware"
Cohesion: 0.29
Nodes (5): IMiddleware, HttpContext, RequestDelegate, Task, EnrichLogsWithUserInfoMiddleware

### Community 105 - "IModelBinder"
Cohesion: 0.15
Nodes (11): IFusionCache, JwtOptions, JwtOptionsValidator, IReadOnlyCollection, CancellationToken, HttpContext, IOptions, RouteGroupBuilder (+3 more)

### Community 106 - ".RemoveMyProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 107 - "ProductsModule"
Cohesion: 0.25
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 108 - "ReverseProxyOptions.cs"
Cohesion: 0.50
Nodes (3): ReverseProxyOptions, ReverseProxyOptionsValidator, IReadOnlyList

### Community 109 - "RequestBody"
Cohesion: 0.20
Nodes (8): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator, Response

### Community 110 - "JobTargets"
Cohesion: 0.40
Nodes (3): IConfiguration, IServiceCollection, Setup

### Community 111 - "CacheKeys"
Cohesion: 0.26
Nodes (7): IBackgroundJobs, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 112 - "StringExtensions"
Cohesion: 0.22
Nodes (5): Common.Infrastructure.Modules, Host.Infrastructure, OtlpExportProtocol, ModuleRegistry, StringExtensions

### Community 113 - "ReverseProxyOptions"
Cohesion: 0.67
Nodes (3): Products.Endpoints.ProductTemplates.v1.Deactivate, Request, RequestValidator

### Community 114 - "OutboxMetricsJob"
Cohesion: 0.67
Nodes (3): Products.Endpoints.Stores.v1.My.RemoveProduct, Request, RequestValidator

### Community 115 - "SearchLanguageResolver"
Cohesion: 0.26
Nodes (7): BackgroundJobsService, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 116 - "IRoleService"
Cohesion: 0.67
Nodes (3): CorsOptions, CorsOptionsValidator, IReadOnlyList

### Community 117 - "DummySmsGateway"
Cohesion: 0.09
Nodes (15): ICurrentUser, Guid, ICollection, CancellationToken, RouteGroupBuilder, Task, Endpoint, CancellationToken (+7 more)

### Community 118 - "RequestBody"
Cohesion: 0.05
Nodes (33): IHostBuilder, KeyValuePair, LoggerConfiguration, LoggerMinimumLevelConfiguration, OpenTelemetryBuilder, ResourceBuilder, ObservabilityOptions, ObservabilityOptionsValidator (+25 more)

### Community 119 - "RequestBody"
Cohesion: 0.18
Nodes (8): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, ProductsModule

### Community 120 - "Request.cs"
Cohesion: 0.67
Nodes (3): ModulesOptions, ModulesOptionsValidator, IReadOnlyList

### Community 122 - "Setup"
Cohesion: 0.12
Nodes (11): Notifications.Application.Otp, Notifications.Application.Sms, Common.Application.Caching, Notifications.Infrastructure.InterModuleRequestHandlers, Notifications.Infrastructure.Otp, CacheKeys, For, OtpCacheEntry (+3 more)

### Community 124 - "Setup"
Cohesion: 0.18
Nodes (8): IApplicationBuilder, IConfiguration, ILogger, IServiceCollection, LoggerMessage, string, WebApplication, Setup

### Community 126 - "Setup"
Cohesion: 0.33
Nodes (7): Exception, HttpContext, ILogger, LoggerMessage, RequestDelegate, Task, GlobalExceptionHandlingMiddleware

### Community 127 - "Setup"
Cohesion: 0.20
Nodes (6): CancellationToken, Task, ISmsGateway, CancellationToken, Task, DummySmsGateway

### Community 128 - "Endpoint"
Cohesion: 0.09
Nodes (16): Common.Application.JsonConverters, IAM.Infrastructure.Persistence.EntityConfigurations, Common.Infrastructure.Persistence.EntityConfigurations, Common.Infrastructure.Persistence.ValueConverters, Products.Infrastructure.Persistence.EntityConfigurations, DomainEventConverter, JsonSerializerOptions, EventConverter (+8 more)

### Community 129 - "Setup"
Cohesion: 0.18
Nodes (7): Products.Endpoints.Products, RouteGroupBuilder, Setup, CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 130 - "Setup"
Cohesion: 0.16
Nodes (17): Configuration-Driven Module Loading, IntegrationEvents (Async Cross-Module), IAM Module, Notifications Module, Products Module, Observability (OpenTelemetry), docker-compose.yml (Base Stack), docker-compose.app.yml (App-Only) (+9 more)

### Community 131 - "Setup"
Cohesion: 0.16
Nodes (10): DbSet, IProductsDbContext, IReadOnlyList, List, ProductTemplate, EntityTypeBuilder, ProductTemplateConfiguration, DbSet (+2 more)

### Community 136 - "BackgroundJobsOptions"
Cohesion: 0.33
Nodes (6): PersistenceQueryableExtensions, CancellationToken, Expression, Func, IQueryable, Task

### Community 137 - "CaptchaOptions"
Cohesion: 0.21
Nodes (9): PartitionedRateLimiter, CancellationToken, Func, HttpContext, IConfiguration, IServiceCollection, OnRejectedContext, ValueTask (+1 more)

### Community 138 - "CorsOptions"
Cohesion: 0.40
Nodes (3): Host, Host.Swagger, Program

### Community 139 - "DatabaseOptions"
Cohesion: 0.22
Nodes (6): BackgroundJobsModule, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection

### Community 140 - "HealthCheckOptions"
Cohesion: 0.70
Nodes (4): OutboxCleanupSettings, OutboxCleanupSettingsValidator, OutboxOptions, OutboxOptionsValidator

### Community 141 - "SecurityHeadersOptions.cs"
Cohesion: 0.67
Nodes (3): SecurityHeadersOptions, SecurityHeadersOptionsValidator, Dictionary

### Community 142 - "ModulesOptions"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 144 - "OtpOptions"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 146 - "SecurityHeadersOptions"
Cohesion: 0.40
Nodes (3): IConfiguration, IServiceCollection, Setup

### Community 147 - "SignalROptions"
Cohesion: 0.50
Nodes (3): MassTransitInterModuleRequestClient, CancellationToken, Task

### Community 148 - "Setup"
Cohesion: 0.33
Nodes (6): Assembly, IApplicationBuilder, IConfiguration, IServiceCollection, IWebHostEnvironment, Setup

### Community 149 - "AutoMigrateMarker"
Cohesion: 0.40
Nodes (3): IEventHandlerWrapper, CancellationToken, Task

### Community 150 - "Setup"
Cohesion: 0.28
Nodes (6): accessToken, DateTimeOffset, expiresAt, ICollection, refreshTokenBytes, TokenService

### Community 153 - "Endpoint"
Cohesion: 0.22
Nodes (5): IApplicationBuilder, IServiceCollection, IWebHostEnvironment, Type, Setup

### Community 155 - "VerifyPhoneOtpRequest"
Cohesion: 0.43
Nodes (5): VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, CancellationToken, Task, VerifyPhoneOtpRequestHandler

### Community 156 - "CurrentUser"
Cohesion: 0.10
Nodes (17): Common.Application.ModelBinders, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, Products.Endpoints.ProductTemplates.v1.Activate, IModelBinder, ModelBindingContext, StronglyTypedIdBinder, Task, Request (+9 more)

### Community 157 - "Setup"
Cohesion: 0.33
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, OutboxCleanupJob

### Community 158 - "Endpoint"
Cohesion: 0.31
Nodes (5): CancellationToken, Task, TimeSpan, IOtpService, OtpVerificationOutcome

### Community 161 - "Endpoint"
Cohesion: 0.15
Nodes (9): SemaphoreSlim, string, DummyOtpService, OtpService, CancellationToken, int, Task, TimeSpan (+1 more)

### Community 163 - "Endpoint"
Cohesion: 0.32
Nodes (5): ApiVersionDescription, IConfigureOptions, OpenApiInfo, ConfigureSwaggerOptions, SwaggerGenOptions

### Community 164 - "Endpoint"
Cohesion: 0.25
Nodes (4): Host.Middlewares, HttpContext, Task, SecurityHeadersMiddleware

### Community 165 - "Endpoint"
Cohesion: 0.20
Nodes (7): Products.Infrastructure.RateLimiting, Action, IEnumerable, RateLimiterOptions, Policies, string, RateLimitingConstants

### Community 166 - "Setup"
Cohesion: 0.25
Nodes (5): IAM.Endpoints.Tokens.VersionNeutral, RouteGroupBuilder, Endpoint, RouteGroupBuilder, Setup

### Community 168 - "Endpoint"
Cohesion: 0.25
Nodes (5): IAM.Endpoints.Captcha.VersionNeutral, RouteGroupBuilder, Endpoint, RouteGroupBuilder, Setup

### Community 171 - "Endpoint"
Cohesion: 0.20
Nodes (6): Common.InterModuleRequests, Common.Infrastructure.Localization, Common.Infrastructure.Caching, IAssemblyReference, Setup, IServiceCollection

### Community 172 - "Endpoint"
Cohesion: 0.22
Nodes (7): PerformedContext, BackgroundJobsTelemetry, ActivitySource, Counter, Histogram, Meter, string

### Community 173 - "Endpoint"
Cohesion: 0.33
Nodes (4): JsonValue, OpenApiOperation, OperationFilterContext, SwaggerDefaultValues

### Community 174 - "Endpoint"
Cohesion: 0.29
Nodes (5): IAM.Application.Tokens.DTOs, DateTimeOffset, AccessTokenDto, DateTimeOffset, TokensDto

### Community 175 - "Endpoint"
Cohesion: 0.06
Nodes (28): Products.Endpoints.Stores.v1.My.AuditLog, AuditLogDto, PaginationResponse, DbContextExtensions, CancellationToken, DbSet, JsonSerializerOptions, Task (+20 more)

### Community 178 - "Endpoint"
Cohesion: 0.43
Nodes (6): Checkout, FeatureFlags, IAM, Notifications, Products, string

### Community 179 - "Endpoint"
Cohesion: 0.52
Nodes (6): CachingEntryDefaults, CachingOptions, CachingOptionsValidator, Redis, RedisValidator, TimeSpan

### Community 182 - "Endpoint"
Cohesion: 0.33
Nodes (4): IAM.Domain, string, Constants, IAssemblyReference

### Community 185 - "Endpoint"
Cohesion: 0.18
Nodes (8): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, IamModule

### Community 186 - "Endpoint"
Cohesion: 0.67
Nodes (3): ResxLocalizationOptions, ResxLocalizationOptionsValidator, ICollection

### Community 187 - "Endpoint"
Cohesion: 0.40
Nodes (3): Setup, IConfiguration, IServiceCollection

### Community 189 - "Endpoint"
Cohesion: 0.33
Nodes (4): IOpenApiSchema, ISchemaFilter, SchemaFilterContext, StronglyTypedIdSchemaFilter

### Community 191 - "Setup"
Cohesion: 0.25
Nodes (7): Concurrent safety, Cross-process call path, Files added by this PoC, How it works, How to run, Split-Deployment PoC, What this proves

### Community 192 - "NameFor"
Cohesion: 0.33
Nodes (5): FeatureFlagResultExtensions, Action, Func, IFeatureManager, Task

### Community 194 - "Sync AI Settings Command"
Cohesion: 0.33
Nodes (4): IOperationFilter, OpenApiOperation, OperationFilterContext, RemoveDefaultResponseSchemaFilter

### Community 201 - "IAssemblyReference"
Cohesion: 0.67
Nodes (3): OtpOptions, OtpOptionsValidator, Dictionary

### Community 202 - "IAssemblyReference"
Cohesion: 0.18
Nodes (9): Products.Endpoints.Stores.v1.Search, CancellationToken, IOptions, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator (+1 more)

### Community 203 - "IAssemblyReference"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 204 - ".GetProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 205 - "Request.cs"
Cohesion: 0.33
Nodes (4): Assembly, IConfiguration, IServiceCollection, Setup

### Community 206 - "IAssemblyReference"
Cohesion: 0.12
Nodes (12): DbContext, BaseDbContext, CancellationToken, DbSet, Task, IOutboxDbContext, CancellationToken, DbSet (+4 more)

### Community 208 - ".LogDispatchingNotification"
Cohesion: 0.24
Nodes (7): SessionTokenReuseDetectedIntegrationEvent, CancellationToken, Guid, ILogger, LoggerMessage, Task, SessionTokenReuseDetectedSignalRHandler

### Community 211 - "IAssemblyReference"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 213 - "IAssemblyReference"
Cohesion: 0.33
Nodes (5): ActivitySource, Counter, Meter, string, ProductsTelemetry

### Community 215 - ".DeactivateProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 216 - "Request.cs"
Cohesion: 0.18
Nodes (9): Products.Endpoints.Products.v1.Search, CancellationToken, IOptions, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator (+1 more)

### Community 218 - ".RemoveProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 219 - ".RevokeAllSessions"
Cohesion: 0.14
Nodes (10): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint, ActivitySource, Counter, Meter (+2 more)

### Community 221 - "NotificationsTelemetry"
Cohesion: 0.06
Nodes (30): Notifications.Infrastructure.Telemetry, Notifications.Infrastructure.Hubs, Notifications.Application.Hubs, Notifications.Infrastructure, Hub, CancellationToken, IReadOnlyList, Task (+22 more)

### Community 223 - "Setup.cs"
Cohesion: 0.40
Nodes (3): IAM.Endpoints.Users.VersionNeutral, RouteGroupBuilder, Setup

### Community 225 - "Products.Endpoints.Probe"
Cohesion: 0.20
Nodes (6): Products.Endpoints.Stores, Products.Endpoints.Probe, Products.Endpoints, IAssemblyReference, RouteGroupBuilder, Setup

### Community 228 - "IAM"
Cohesion: 0.11
Nodes (15): Common.Application.Validation, AuditLogOptions, AuditLogOptionsValidator, CaptchaOptions, CaptchaOptionsValidator, DatabaseOptions, DatabaseOptionsValidator, HealthCheckOptions (+7 more)

### Community 229 - "IntegrationEvent"
Cohesion: 0.40
Nodes (3): Setup, IConfiguration, IServiceCollection

### Community 231 - "AccessTokenDto"
Cohesion: 0.40
Nodes (3): Setup, IApplicationBuilder, IServiceCollection

### Community 233 - "IAM.Endpoints.Otp.VersionNeutral"
Cohesion: 0.40
Nodes (3): IAM.Endpoints.Otp.VersionNeutral, RouteGroupBuilder, Setup

### Community 237 - "DefaultResponsesOperationFilter"
Cohesion: 0.70
Nodes (3): OpenApiOperation, OperationFilterContext, DefaultResponsesOperationFilter

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

### Community 250 - "SecurityHeadersOptions.cs"
Cohesion: 0.22
Nodes (7): Products.Endpoints.Stores.v1.My.Update, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator

## Knowledge Gaps
- **138 isolated node(s):** `CacheKeys`, `OtpCacheEntry`, `FeatureFlags`, `Common.Domain`, `Common.Infrastructure` (+133 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **100 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Host Logging & Serilog Setup` to `Endpoint`, `Host NuGet Deps (OTel/Health)`, `CorsOptions`, `BackgroundJobs Service (Hangfire)`, `HealthCheckOptions`, `SecurityHeadersOptions.cs`, `Launch Settings`, `SecurityHeadersOptions`, `Authz Constants & Feature Flags`, `Integration Event Handler Base`, `Setup`, `Endpoint`, `StoreConfiguration`, `Endpoint`, `Endpoint`, `Endpoint`, `Endpoint`, `DummyOtpService`, `Endpoint`, `Endpoint`, `CollectionExtensions`, `Hangfire`, `IAssemblyReference`, `Request.cs`, `RequestLoggingPathPostConfigure`, `Endpoint`, `HttpContextExtensions`, `net10.0`, `AuditLogRetentionJobRegistrar`, `Products.Endpoints.Probe`, `IAM`, `IntegrationEvent`, `Response`, `IModelBinder`, `ReverseProxyOptions.cs`, `JobTargets`, `StringExtensions`, `IRoleService`, `RequestBody`, `Request.cs`, `Setup`, `Setup`, `Setup`?**
  _High betweenness centrality (0.348) - this node is a cross-community bridge._
- **Why does `Common.Application.Auth` connect `BackgroundJobs Service (Hangfire)` to `Host Logging & Serilog Setup`, `CustomRoles`, `HangfireCustomAuthorizationFilter`, `HttpContextTargetingContextAccessor`, `Host NuGet Deps (OTel/Health)`, `IMiddleware`, `Functional Result Extensions`, `Launch Settings`, `Endpoint`, `Response`, `Response`, `DummySmsGateway`, `SignalR Hub & Exception Middleware`, `Response`, `Host.Swagger`, `PermissionAuthorizationHandler`, `Hangfire`?**
  _High betweenness centrality (0.091) - this node is a cross-community bridge._
- **Why does `Result` connect `MassTransit & DI Setup` to `Setup`, `Setup`, `.ToResult`, `Domain Event Handling & Outbox Collect`, `BackgroundJobsOptions`, `IAM Error Catalogs`, `IAM OTP Verify & Token Endpoint`, `Project Files & Solution`, `ModulesOptions`, `OtpOptions`, `Outbox Processor & Seeder`, `Product Template Aggregate`, `PaginationRequestValidator`, `Hangfire.PostgreSql`, `Endpoint`, `double`, `Microsoft.AspNetCore.SignalR.StackExchangeRedis`, `DummyOtpService`, `coverlet.collector`, `decimal`, `NameFor`, `CustomRoles`, `IAggregateRoot`, `ApiVersionDescription`, `IAssemblyReference`, `IAssemblyReference`, `HostCollection`, `.GetProductTemplateAsync`, `IAssemblyReference`, `Activity`, `.DeactivateProductTemplateAsync`, `Request.cs`, `HostTestFactory`, `.RevokeAllSessions`, `.RemoveProductAsync`, `OutboxTestWebAppFactory`, `IModelBinder`, `.RemoveMyProductAsync`, `ProductsModule`, `RequestBody`, `DummySmsGateway`, `SecurityHeadersOptions.cs`, `Endpoint`?**
  _High betweenness centrality (0.083) - this node is a cross-community bridge._
- **What connects `CacheKeys`, `OtpCacheEntry`, `FeatureFlags` to the rest of the system?**
  _138 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Host Logging & Serilog Setup` be split into smaller, more focused modules?**
  _Cohesion score 0.1164021164021164 - nodes in this community are weakly interconnected._
- **Should `IAM User Identity & Auditing` be split into smaller, more focused modules?**
  _Cohesion score 0.13227513227513227 - nodes in this community are weakly interconnected._
- **Should `Host NuGet Deps (OTel/Health)` be split into smaller, more focused modules?**
  _Cohesion score 0.09986504723346828 - nodes in this community are weakly interconnected._