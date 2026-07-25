# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-07-25)

## Corpus Check
- 443 files · ~63,427 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2822 nodes · 4977 edges · 279 communities (179 shown, 100 thin omitted)
- Extraction: 98% EXTRACTED · 2% INFERRED · 0% AMBIGUOUS · INFERRED: 76 edges (avg confidence: 0.8)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `5b906e9b`
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
- RequestBody
- RequestBody
- Request.cs
- Setup
- Setup
- Setup
- Endpoint
- Setup
- Endpoint
- Setup
- Setup
- Setup
- Setup
- HostEnvironmentExtensions.cs
- BackgroundJobsOptions
- CaptchaOptions
- DatabaseOptions
- ModulesOptions
- OtpPurposes.cs
- OtpOptions
- SecurityHeadersOptions
- SignalROptions
- Setup
- AutoMigrateMarker
- Setup
- .GetMeAsync
- ProductTemplates/v1/Search/Request.cs
- Endpoint
- VerifyPhoneOtpRequest
- CurrentUser
- Setup
- Endpoint
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
- DatabaseOptions.cs
- Setup
- NameFor
- IAutoMigrateMarker.cs
- IAssemblyReference
- IAssemblyReference
- Request.cs
- IAssemblyReference
- .LogDispatchingNotification
- IAssemblyReference
- IAssemblyReference
- .DeactivateProductTemplateAsync
- .RevokeAllSessions
- Host.Swagger
- NotificationsTelemetry
- Products.Endpoints.Probe
- IAM
- IntegrationEvent
- AccessTokenDto
- IAM.Endpoints.Otp.VersionNeutral
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
1. `Common.Application.Options` - 90 edges
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

## Communities (279 total, 100 thin omitted)

### Community 0 - "Host Logging & Serilog Setup"
Cohesion: 0.10
Nodes (15): IAM.Application.Tokens.Services, IAM.Application.Extensions, IAM.Endpoints.Otp, IAM.Domain.Identity, Common.Infrastructure.Extensions, IAM.Endpoints.Tokens.VersionNeutral.Revoke, IAM.Infrastructure.Telemetry, Common.Domain.ResultMonad (+7 more)

### Community 1 - "IAM User Identity & Auditing"
Cohesion: 0.10
Nodes (24): IEntityTypeConfiguration, ApplicationUserId, DefaultIdType, DateTimeOffset, ApplicationUser, DateOnly, IReadOnlyCollection, Response (+16 more)

### Community 2 - "Products Store & Audit Services"
Cohesion: 0.11
Nodes (18): BackgroundService, DatabaseSeederOrchestrator, CancellationToken, ILogger, LoggerMessage, Task, SeedingCompletionTracker, CancellationToken (+10 more)

### Community 3 - "Notifications Dispatch & SignalR Client"
Cohesion: 0.67
Nodes (3): Products.Endpoints.Stores.v1.AuditLog, Request, RequestValidator

### Community 5 - "Cross-Module Comm & Arch Audit Skills"
Cohesion: 0.05
Nodes (36): IdentityDbContext, IdentityUser, AuditableEntity, DateTimeOffset, StronglyTypedIdHelper, DateOnly, DateTimeOffset, Guid (+28 more)

### Community 6 - "Domain Event Handling & Outbox Collect"
Cohesion: 0.40
Nodes (4): int, Constants, Request, RequestValidator

### Community 7 - "Host NuGet Deps (OTel/Health)"
Cohesion: 0.09
Nodes (15): Common.Application.FeatureManagement, Notifications.Infrastructure.Telemetry, Common.InterModuleRequests.Contracts, Notifications.Infrastructure.InterModuleRequestHandlers, IAM.Infrastructure.RateLimiting, Common.InterModuleRequests.Notifications, RouteHandlerBuilderExtensions, RouteHandlerBuilder (+7 more)

### Community 8 - "k6 Load Test Scripts"
Cohesion: 0.31
Nodes (4): AggregateRoot, IEnumerable, IReadOnlyCollection, List

### Community 9 - "REPR Request Validators"
Cohesion: 0.21
Nodes (12): AbstractValidator, Products.Endpoints.Products.v1.My.Update, JwtOptionsValidator, CustomValidator, RequestBody, Request, RequestBody, RequestBodyValidator (+4 more)

### Community 10 - "IAM Error Catalogs"
Cohesion: 0.09
Nodes (14): HttpStatusCode, IdentityResult, IStringLocalizer, StringLocalizerExtensions, Error, ICollection, IResult, IdentityResultExtensions (+6 more)

### Community 11 - "BackgroundJobs Service (Hangfire)"
Cohesion: 0.15
Nodes (12): Common.Application.Search, Products.Endpoints.Stores.v1.Search, Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Products.Domain.Products, Products.Infrastructure.Telemetry, Products.Application.Persistence (+4 more)

### Community 12 - "IAM OTP Verify & Token Endpoint"
Cohesion: 0.10
Nodes (27): JwtOptions, IReadOnlyCollection, IInterModuleRequestClient, CancellationToken, Task, accessToken, DateTimeOffset, expiresAt (+19 more)

### Community 13 - "Project Files & Solution"
Cohesion: 0.09
Nodes (19): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, SendPhoneOtpRequest, SendPhoneOtpResponse, CancellationToken, Task, ICaptchaService, Response, CancellationToken (+11 more)

### Community 14 - "Localized Identity Errors"
Cohesion: 0.13
Nodes (4): IAM.Infrastructure.Identity, IdentityError, IdentityErrorDescriber, LocalizedIdentityErrorDescriber

### Community 15 - "Functional Result Extensions"
Cohesion: 0.09
Nodes (12): Common.Domain.StronglyTypedIds, IAM.Domain.Identity.DomainEvents.v1, Common.Domain.Events, IAM.Domain.Identity.Sessions, Common.Infrastructure.EventBus, Common.Domain.Entities, IAM.Infrastructure.Persistence.EntityConfigurations, Common.Infrastructure.Persistence.EntityConfigurations (+4 more)

### Community 16 - "Launch Settings"
Cohesion: 0.12
Nodes (10): Notifications.Application.Otp, Common.Application.Caching, Notifications.Infrastructure.Otp, CacheKeys, For, OtpCacheEntry, OtpService, IConfiguration (+2 more)

### Community 17 - "Module Installers (IModule)"
Cohesion: 0.14
Nodes (11): RecurringJobOptions, IRecurringBackgroundJobs, Action, Expression, Func, Task, RecurringBackgroundJobsService, Action (+3 more)

### Community 18 - "Host Infrastructure Setup"
Cohesion: 0.16
Nodes (16): DomainEvent, DateTimeOffset, DefaultIdType, V1AllSessionsRevokedDomainEvent, V1RefreshTokenRevokedDomainEvent, V1RefreshTokenUpdatedDomainEvent, V1SessionCreatedDomainEvent, V1SessionRefreshedDomainEvent (+8 more)

### Community 19 - "IAM OTP Send & Captcha"
Cohesion: 0.32
Nodes (6): UserRegisteredIntegrationEvent, CancellationToken, ILogger, LoggerMessage, Task, UserRegisteredSignalRHandler

### Community 20 - "Authz Constants & Feature Flags"
Cohesion: 0.10
Nodes (15): IAM.Endpoints.Otp.VersionNeutral.SendForLogin, IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, IAM.Endpoints.Common.Validations, IResxLocalizer, IRuleBuilder, IRuleBuilderOptions, CommonValidations, IResxLocalizer (+7 more)

### Community 21 - "Bounded Capture Streams"
Cohesion: 0.05
Nodes (29): bool, byte, IPostConfigureOptions, Memory, PathString, ReadOnlyMemory, ReadOnlySpan, SeekOrigin (+21 more)

### Community 22 - "SignalR Hub & Exception Middleware"
Cohesion: 0.07
Nodes (16): Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Products.Infrastructure.Persistence.Seeding, Common.Application.Persistence, IAM.Infrastructure.Persistence, IAM.Infrastructure.Persistence.Seeding, Common.InterModuleRequests.IAM, Common.Infrastructure.Persistence.Auditing (+8 more)

### Community 23 - "Telemetry (ActivitySource/Meter)"
Cohesion: 0.06
Nodes (34): SendMessageBody, SendRequestBody, CancellationToken, Task, ISmsGateway, SmsCategory, SmsMessage, CancellationToken (+26 more)

### Community 24 - "Outbox Processor & Seeder"
Cohesion: 0.19
Nodes (9): CancellationToken, HttpContext, ILogger, IOptions, LoggerMessage, RouteGroupBuilder, Task, TimeProvider (+1 more)

### Community 25 - "EF Core DbContexts"
Cohesion: 0.18
Nodes (7): Common.Application.BackgroundJobs, BackgroundJobs.Telemetry, BackgroundJobs, IServerFilter, PerformingContext, JobMetricsFilter, string

### Community 26 - "Integration Event Handler Base"
Cohesion: 0.29
Nodes (5): Outbox, Common.Infrastructure.Persistence.Outbox, Outbox.Persistence, Outbox.Telemetry, OutboxMetricsJob

### Community 27 - "Product Template Aggregate"
Cohesion: 0.06
Nodes (24): ICurrentUser, Guid, ICollection, CancellationToken, RouteGroupBuilder, Task, Endpoint, CancellationToken (+16 more)

### Community 28 - "Outbox Message & Tokens"
Cohesion: 0.40
Nodes (3): ProblemDetails, ProblemDetailsExtensions, ICollection

### Community 29 - "MassTransit & DI Setup"
Cohesion: 0.14
Nodes (11): Result, Func, Task, AsyncExtensions, SyncExtensions, Action, Func, Task (+3 more)

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
Cohesion: 0.29
Nodes (6): Products.Endpoints.Stores.v1.AddProduct, RequestBody, Request, RequestBody, RequestValidator, Response

### Community 34 - "Microsoft.EntityFrameworkCore.Abstractions"
Cohesion: 0.07
Nodes (27): Products.Endpoints.Probe.v1, IConsumer, IntegrationEventHandlerBase, CancellationToken, ConsumeContext, DefaultIdType, ILogger, LoggerMessage (+19 more)

### Community 35 - "ISearchLocalized"
Cohesion: 0.21
Nodes (10): DomainEventHandlerBase, IEventHandler, CancellationToken, Task, CancellationToken, Task, SimulateSomeBusinessHandler, StoreCreatedIntegrationEventPublishingHandler (+2 more)

### Community 36 - "StoreConfiguration"
Cohesion: 0.23
Nodes (8): CustomRateLimitingOptions, CustomRateLimitingOptionsValidator, FixedWindow, FixedWindowValidator, Action, IEnumerable, RateLimiterOptions, Policies

### Community 37 - "Setup"
Cohesion: 0.10
Nodes (17): IEnumerable, IReadOnlyCollection, List, ApplicationUser, Task, Seeder, IdentityRole, ILogger (+9 more)

### Community 38 - "Hangfire.PostgreSql"
Cohesion: 0.24
Nodes (6): Products.Endpoints.Stores.v1.Create, CancellationToken, Task, Request, RequestValidator, Response

### Community 39 - "EventDispatcher"
Cohesion: 0.16
Nodes (9): ISearchLanguageResolver, SearchLanguageResolver, string, CancellationToken, IOptions, RouteGroupBuilder, Task, Endpoint (+1 more)

### Community 40 - "NetArchTest.Rules"
Cohesion: 0.27
Nodes (8): OpenTelemetryBuilder, ResourceBuilder, Action, IConfiguration, IHostEnvironment, IReadOnlyList, IServiceCollection, Setup

### Community 41 - "Aigamo.ResXGenerator"
Cohesion: 0.07
Nodes (21): IAM.Endpoints.Users.VersionNeutral.Search, Products.Endpoints.Products.v1.My.Get, Products.Endpoints.ProductTemplates.v1.Search, Products.Endpoints.Products.v1.Search, Common.Application.DTOs, IAM.Endpoints.Users.VersionNeutral.Get, Products.Endpoints.Stores.v1.Get, Products.Endpoints.Products.v1.My.Search (+13 more)

### Community 42 - "IOperationFilter"
Cohesion: 0.05
Nodes (38): ChangeTracker, DatabaseFacade, EntityEntry, IDisposable, Lock, IDbContext, CancellationToken, DbSet (+30 more)

### Community 43 - "IRateLimiterPolicy"
Cohesion: 0.05
Nodes (32): IRateLimiterPolicy, CancellationToken, Func, HttpContext, OnRejectedContext, RateLimitPartition, ValueTask, RegisterRateLimitingPolicy (+24 more)

### Community 44 - "Seeder"
Cohesion: 0.19
Nodes (7): ISearchLocalized, Product, IReadOnlyCollection, List, Store, EntityTypeBuilder, ProductConfiguration

### Community 45 - "AggregateRoot"
Cohesion: 0.15
Nodes (11): Products.Endpoints.ProductTemplates.v1.Create, ProductTemplateId, CancellationToken, Task, Request, RequestValidator, Response, CancellationToken (+3 more)

### Community 46 - "ApiVersionSet"
Cohesion: 0.29
Nodes (4): ApiVersionSet, Setup, IEndpointRouteBuilder, IServiceCollection

### Community 48 - "IntegrationTestFactory"
Cohesion: 0.15
Nodes (9): IDatabaseSeeder, CancellationToken, Task, CancellationToken, Task, IamDatabaseSeeder, CancellationToken, Task (+1 more)

### Community 50 - "Asp.Versioning.Http"
Cohesion: 0.25
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 51 - "IInterModuleRequestHandler"
Cohesion: 0.33
Nodes (5): IAM.Endpoints.Tokens.VersionNeutral.Refresh, Request, RequestValidator, DateTimeOffset, Response

### Community 52 - "double"
Cohesion: 0.17
Nodes (11): DateTime, double, FormUrlEncodedContent, ReCaptchaResponse, CancellationToken, Exception, ILogger, LoggerMessage (+3 more)

### Community 53 - "RouteHandlerBuilderExtensions"
Cohesion: 0.11
Nodes (15): StoreId, CancellationToken, ILogger, int, LoggerMessage, Task, CancellationToken, List (+7 more)

### Community 54 - "IList"
Cohesion: 0.15
Nodes (10): IAM.Domain.Captcha, IAM.Infrastructure.Captcha.Services, IAM.Application.Captcha.Services, Common.Infrastructure.Resiliency, HttpClient, HttpStandardResilienceOptions, IHttpClientBuilder, Setup (+2 more)

### Community 55 - "Microsoft.AspNetCore.SignalR.StackExchangeRedis"
Cohesion: 0.29
Nodes (5): DbContext, DbSet, ModelBuilder, ModelConfigurationBuilder, OutboxDbContext

### Community 56 - "DummyOtpService"
Cohesion: 0.29
Nodes (5): CancellationToken, IOptions, RouteGroupBuilder, Task, Endpoint

### Community 58 - "CollectionExtensions"
Cohesion: 0.23
Nodes (7): StronglyTypedIdWriteOnlyJsonConverter, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, IStronglyTypedId, DefaultIdType

### Community 59 - "coverlet.collector"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

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
Cohesion: 0.17
Nodes (9): ICoreModule, IModule, Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection (+1 more)

### Community 65 - "ApplyAuditingInterceptor"
Cohesion: 0.15
Nodes (11): SaveChangesInterceptor, ApplyAuditingInterceptor, CancellationToken, DbContextEventData, InterceptionResult, ValueTask, ApplySearchLanguageInterceptor, CancellationToken (+3 more)

### Community 66 - "CustomRoles"
Cohesion: 0.20
Nodes (8): FrozenDictionary, IReadOnlySet, CustomPermissions, HashSet, IEnumerable, CurrentUser, Guid, ICollection

### Community 67 - "AuthenticateResult"
Cohesion: 0.11
Nodes (13): DbSet, IProductsDbContext, CancellationToken, RouteGroupBuilder, Task, Endpoint, CancellationToken, RouteGroupBuilder (+5 more)

### Community 68 - "ValueObject"
Cohesion: 0.27
Nodes (4): Common.Domain, IComparable, ValueObject, IEnumerable

### Community 69 - "IResxLocalizer"
Cohesion: 0.24
Nodes (6): JsonConverter, StronglyTypedIdReadOnlyJsonConverter, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 70 - "OutboxModule"
Cohesion: 0.40
Nodes (5): FullTextSearchOptions, FullTextSearchOptionsValidator, Dictionary, IReadOnlyList, string

### Community 71 - "V1ProductCreatedDomainEvent"
Cohesion: 0.08
Nodes (25): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Add a new language/culture, Add search to a new entity _(Build checklist)_ (+17 more)

### Community 72 - "IAggregateRoot"
Cohesion: 0.20
Nodes (8): Products.Endpoints.Stores.v1.My.Create, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator, Response

### Community 73 - "ApiVersionDescription"
Cohesion: 0.40
Nodes (4): int, Constants, Request, RequestValidator

### Community 75 - "GetSeedUserIdsRequest"
Cohesion: 0.16
Nodes (11): Products.Endpoints.Stores.v1.My.RemoveProduct, Products.Endpoints.Stores.v1.RemoveProduct, ProductId, Request, RequestValidator, Request, RequestValidator, Request (+3 more)

### Community 76 - "HostCollection"
Cohesion: 0.67
Nodes (3): CorsOptions, CorsOptionsValidator, IReadOnlyList

### Community 79 - "Endpoint"
Cohesion: 0.12
Nodes (12): Common.IntegrationEvents, Notifications.Application.IntegrationEventHandlers, IAM.Application.Users.DomainEventHandlers.v1, Products.Application.Stores.DomainEventHandlers.v1, Common.Application.EventBus, IIntegrationEventOutbox, CancellationToken, Task (+4 more)

### Community 81 - "HttpContextExtensions"
Cohesion: 0.13
Nodes (12): ObservableGauge, CancellationToken, ILogger, LoggerMessage, Task, ActivitySource, Counter, Histogram (+4 more)

### Community 82 - "net10.0"
Cohesion: 0.13
Nodes (9): IAM.Endpoints.Users.VersionNeutral.SelfRegister, Common.Domain.Extensions, SearchValues, StringExtensions, int, Constants, Guid, Request (+1 more)

### Community 83 - "IServiceProvider"
Cohesion: 0.44
Nodes (4): IServiceProvider, MigrationGuard, ILogger, LoggerMessage

### Community 84 - "enabledManagers"
Cohesion: 0.16
Nodes (9): AuditableEntityConfiguration, EntityTypeBuilder, IReadOnlyList, List, ProductTemplate, EntityTypeBuilder, ProductTemplateConfiguration, EntityTypeBuilder (+1 more)

### Community 85 - "Activity"
Cohesion: 0.38
Nodes (4): Activity, ResultTelemetryExtensions, ActivitySource, Task

### Community 86 - "AuditLogRetentionJobRegistrar"
Cohesion: 0.11
Nodes (16): Common.Infrastructure.Persistence.AuditLog, IHostedService, AuditLogRetentionJobRegistrar, CancellationToken, ILogger, LoggerMessage, string, Task (+8 more)

### Community 87 - "AuditLogRetentionService"
Cohesion: 0.24
Nodes (8): Action, Exception, IApplicationBuilder, IEndpointRouteBuilder, IEnumerable, ILogger, LoggerMessage, OutboxModule

### Community 89 - "ConfigurationManager"
Cohesion: 0.33
Nodes (4): ConfigurationManager, Host.Configurations, Setup, WebApplicationBuilder

### Community 90 - "HostTestFactory"
Cohesion: 0.29
Nodes (6): IAM.Endpoints.Tokens.VersionNeutral.Create, Guid, Request, RequestValidator, DateTimeOffset, Response

### Community 91 - "HttpClient"
Cohesion: 0.31
Nodes (6): CaptchaOptions, CaptchaOptionsValidator, CaptchaProvider, IConfiguration, IServiceCollection, Setup

### Community 96 - "OutboxCleanupSettings"
Cohesion: 0.19
Nodes (8): OutboxMessage, DateTimeOffset, TimeSpan, IntegrationEvent, DateTimeOffset, DefaultIdType, ProductCreatedIntegrationEvent, StoreCreatedIntegrationEvent

### Community 97 - "OutboxTestWebAppFactory"
Cohesion: 0.10
Nodes (17): DbSet, IdentityRole, IdentityRoleClaim, IdentityUserClaim, IdentityUserLogin, IdentityUserRole, IdentityUserToken, IIAMDbContext (+9 more)

### Community 98 - "IDatabaseSeeder"
Cohesion: 0.67
Nodes (3): Products.Endpoints.Stores.v1.My.AuditLog, Request, RequestValidator

### Community 99 - "HangfireCustomAuthorizationFilter"
Cohesion: 0.15
Nodes (7): DashboardContext, IDashboardAsyncAuthorizationFilter, CustomPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder, HangfireCustomAuthorizationFilter, Task

### Community 100 - "IdentityResultExtensions"
Cohesion: 0.17
Nodes (8): Products.Domain.Products.DomainEvents.v1, V1ProductCreatedDomainEvent, V1ProductDescriptionUpdatedDomainEvent, V1ProductNameUpdatedDomainEvent, V1ProductPriceDecreasedDomainEvent, V1ProductPriceIncreasedDomainEvent, V1ProductQuantityDecreasedDomainEvent, V1ProductQuantityIncreasedDomainEvent

### Community 101 - "Setup.GlobalExceptionHandlingMiddleware.cs"
Cohesion: 0.27
Nodes (6): PolymorphicEventConverter, JsonSerializerOptions, string, Type, Utf8JsonReader, Utf8JsonWriter

### Community 102 - "HttpContextTargetingContextAccessor"
Cohesion: 0.17
Nodes (8): Common.Infrastructure.FeatureManagement, ITargetingContextAccessor, HttpContextTargetingContextAccessor, ValueTask, Setup, IConfiguration, IServiceCollection, TargetingContext

### Community 103 - "IVariantFeatureManagerExtensions"
Cohesion: 0.29
Nodes (5): FeatureFlagResultExtensions, Action, Func, IFeatureManager, Task

### Community 104 - "IMiddleware"
Cohesion: 0.17
Nodes (12): Products.Endpoints.Products.v1.AuditLog, PaginationRequest, PaginationRequestValidator, int, Request, RequestValidator, Request, RequestValidator (+4 more)

### Community 105 - "IModelBinder"
Cohesion: 0.20
Nodes (8): IFusionCache, CancellationToken, HttpContext, IOptions, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 107 - "ProductsModule"
Cohesion: 0.29
Nodes (5): IAggregateRoot, IEnumerable, IReadOnlyCollection, IAuditableEntity, DateTimeOffset

### Community 108 - "ReverseProxyOptions.cs"
Cohesion: 0.39
Nodes (4): CancellationToken, IReadOnlyList, Task, SignalRNotificationDispatcher

### Community 109 - "RequestBody"
Cohesion: 0.20
Nodes (8): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator, Response

### Community 110 - "JobTargets"
Cohesion: 0.15
Nodes (9): Notifications.Application.Sms, Notifications.Infrastructure.Sms, Notifications.Infrastructure.Sms.NetGsm, Notifications.Infrastructure, IAssemblyReference, IConfiguration, IServiceCollection, long (+1 more)

### Community 111 - "CacheKeys"
Cohesion: 0.26
Nodes (7): IBackgroundJobs, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 112 - "StringExtensions"
Cohesion: 0.10
Nodes (14): Common.Infrastructure.Modules, Common.Endpoints.Versioning, Host, Common.Infrastructure.Localization, Common.Application.Options, Host.Middlewares, Products.Endpoints, Common.Infrastructure.Caching (+6 more)

### Community 113 - "ReverseProxyOptions"
Cohesion: 0.25
Nodes (5): Products.Endpoints.ProductTemplates, RouteGroupBuilder, Setup, RouteGroupBuilder, Endpoint

### Community 114 - "OutboxMetricsJob"
Cohesion: 0.33
Nodes (6): StrictDateTimeOffsetJsonConverter, DateTimeOffset, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 115 - "SearchLanguageResolver"
Cohesion: 0.26
Nodes (7): BackgroundJobsService, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 116 - "IRoleService"
Cohesion: 0.40
Nodes (5): Products.Endpoints.Products.v1.Update, RequestBody, Request, RequestBody, RequestValidator

### Community 118 - "RequestBody"
Cohesion: 0.14
Nodes (10): IHostBuilder, KeyValuePair, LoggerConfiguration, LoggerMinimumLevelConfiguration, ObservabilityOptions, ObservabilityOptionsValidator, Dictionary, IEnumerable (+2 more)

### Community 119 - "RequestBody"
Cohesion: 0.18
Nodes (8): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, ProductsModule

### Community 120 - "Request.cs"
Cohesion: 0.67
Nodes (3): ModulesOptions, ModulesOptionsValidator, IReadOnlyList

### Community 122 - "Setup"
Cohesion: 0.47
Nodes (5): CancellationToken, IReadOnlyList, Task, INotificationDispatcher, NotificationPayload

### Community 123 - "Setup"
Cohesion: 0.22
Nodes (6): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, NotificationsModule

### Community 124 - "Setup"
Cohesion: 0.20
Nodes (8): IApplicationBuilder, IConfiguration, ILogger, IServiceCollection, LoggerMessage, string, WebApplication, Setup

### Community 125 - "Endpoint"
Cohesion: 0.36
Nodes (6): StronglyTypedIdListReadOnlyJsonConverter, IReadOnlyList, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 126 - "Setup"
Cohesion: 0.17
Nodes (12): IMiddleware, HttpContext, RequestDelegate, Task, EnrichLogsWithUserInfoMiddleware, Exception, HttpContext, ILogger (+4 more)

### Community 128 - "Endpoint"
Cohesion: 0.12
Nodes (12): Common.Application.JsonConverters, DomainEventConverter, JsonSerializerOptions, EventConverter, JsonSerializerOptions, IntegrationEventConverter, JsonSerializerOptions, StronglyTypedIdValueConverter (+4 more)

### Community 129 - "Setup"
Cohesion: 0.18
Nodes (7): Products.Endpoints.Products, RouteGroupBuilder, Setup, CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 130 - "Setup"
Cohesion: 0.16
Nodes (17): Configuration-Driven Module Loading, IntegrationEvents (Async Cross-Module), IAM Module, Notifications Module, Products Module, Observability (OpenTelemetry), docker-compose.yml (Base Stack), docker-compose.app.yml (App-Only) (+9 more)

### Community 131 - "Setup"
Cohesion: 0.70
Nodes (4): OutboxCleanupSettings, OutboxCleanupSettingsValidator, OutboxOptions, OutboxOptionsValidator

### Community 135 - "HostEnvironmentExtensions.cs"
Cohesion: 0.17
Nodes (9): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, CancellationToken, IReadOnlyCollection, RouteGroupBuilder, Task, Endpoint, DateTimeOffset, Guid (+1 more)

### Community 136 - "BackgroundJobsOptions"
Cohesion: 0.18
Nodes (10): CollectionExtensions, Func, ICollection, IEnumerable, PersistenceQueryableExtensions, CancellationToken, Expression, Func (+2 more)

### Community 137 - "CaptchaOptions"
Cohesion: 0.21
Nodes (9): PartitionedRateLimiter, CancellationToken, Func, HttpContext, IConfiguration, IServiceCollection, OnRejectedContext, ValueTask (+1 more)

### Community 139 - "DatabaseOptions"
Cohesion: 0.22
Nodes (6): BackgroundJobsModule, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection

### Community 142 - "ModulesOptions"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 143 - "OtpPurposes.cs"
Cohesion: 0.40
Nodes (5): Products.Endpoints.Stores.v1.Update, RequestBody, Request, RequestBody, RequestValidator

### Community 144 - "OtpOptions"
Cohesion: 0.25
Nodes (6): CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, Response

### Community 146 - "SecurityHeadersOptions"
Cohesion: 0.12
Nodes (8): Notifications.Infrastructure.Hubs, Notifications.Application.Hubs, Task, INotificationsClient, NotificationGroupName, IConfiguration, IServiceCollection, Setup

### Community 147 - "SignalROptions"
Cohesion: 0.40
Nodes (3): MassTransitInterModuleRequestClient, CancellationToken, Task

### Community 148 - "Setup"
Cohesion: 0.35
Nodes (6): Assembly, IApplicationBuilder, IConfiguration, IServiceCollection, IWebHostEnvironment, Setup

### Community 149 - "AutoMigrateMarker"
Cohesion: 0.13
Nodes (10): CancellationToken, Task, IEventHandlerWrapper, CancellationToken, Task, IOutboxMessage, DateTimeOffset, IEvent (+2 more)

### Community 150 - "Setup"
Cohesion: 0.32
Nodes (6): accessToken, DateTimeOffset, expiresAt, ICollection, refreshTokenBytes, TokenService

### Community 151 - ".GetMeAsync"
Cohesion: 0.50
Nodes (3): ReverseProxyOptions, ReverseProxyOptionsValidator, IReadOnlyList

### Community 152 - "ProductTemplates/v1/Search/Request.cs"
Cohesion: 0.29
Nodes (6): PaginationQueryableExtensions, CancellationToken, Expression, Func, IQueryable, Task

### Community 153 - "Endpoint"
Cohesion: 0.06
Nodes (26): ApiVersionDescription, Host.Swagger, IConfigureOptions, IOpenApiSchema, IOperationFilter, ISchemaFilter, JsonValue, OpenApiInfo (+18 more)

### Community 155 - "VerifyPhoneOtpRequest"
Cohesion: 0.21
Nodes (8): IInterModuleRequest, OtpVerificationFailureReason, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, VerifyPhoneOtpResponseExtensions, CancellationToken, Task, VerifyPhoneOtpRequestHandler

### Community 156 - "CurrentUser"
Cohesion: 0.10
Nodes (17): Products.Endpoints.ProductTemplates.v1.Deactivate, Common.Application.ModelBinders, Products.Endpoints.ProductTemplates.v1.Activate, IModelBinder, ModelBindingContext, StronglyTypedIdBinder, Task, Request (+9 more)

### Community 157 - "Setup"
Cohesion: 0.33
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, OutboxCleanupJob

### Community 158 - "Endpoint"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response

### Community 160 - "ValidationContextExtensions"
Cohesion: 0.40
Nodes (3): ValidationContextExtensions, string, ValidationContext

### Community 161 - "Endpoint"
Cohesion: 0.08
Nodes (19): SemaphoreSlim, CancellationToken, Task, TimeSpan, IOtpService, OtpVerificationOutcome, string, DummyOtpService (+11 more)

### Community 162 - ".ForUser"
Cohesion: 0.67
Nodes (3): IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, Request, RequestValidator

### Community 163 - "Endpoint"
Cohesion: 0.67
Nodes (3): Products.Endpoints.Stores.v1.My.Update, Request, RequestValidator

### Community 164 - "Endpoint"
Cohesion: 0.33
Nodes (3): IApplicationBuilder, IServiceCollection, Setup

### Community 165 - "Endpoint"
Cohesion: 0.33
Nodes (4): Common.Infrastructure.Auth.Services, Common.Infrastructure.Auth, Setup, IServiceCollection

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
Nodes (19): AuditLogDto, PaginationResponse, DbContextExtensions, CancellationToken, DbSet, JsonSerializerOptions, Task, CancellationToken (+11 more)

### Community 176 - "FeatureManagement/RouteHandlerBuilderExtensions.cs"
Cohesion: 0.67
Nodes (3): OtpOptions, OtpOptionsValidator, Dictionary

### Community 178 - "Endpoint"
Cohesion: 0.67
Nodes (3): ResxLocalizationOptions, ResxLocalizationOptionsValidator, ICollection

### Community 179 - "Endpoint"
Cohesion: 0.52
Nodes (6): CachingEntryDefaults, CachingOptions, CachingOptionsValidator, Redis, RedisValidator, TimeSpan

### Community 180 - "SmsOptions.cs"
Cohesion: 0.83
Nodes (3): SmsOptions, SmsOptionsValidator, SmsProvider

### Community 182 - "Endpoint"
Cohesion: 0.33
Nodes (4): IAM.Domain, string, Constants, IAssemblyReference

### Community 184 - "ReCaptchaResponse"
Cohesion: 0.67
Nodes (3): SecurityHeadersOptions, SecurityHeadersOptionsValidator, Dictionary

### Community 185 - "Endpoint"
Cohesion: 0.18
Nodes (8): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, IamModule

### Community 186 - "Endpoint"
Cohesion: 0.50
Nodes (3): IConfiguration, IServiceCollection, Setup

### Community 187 - "Endpoint"
Cohesion: 0.50
Nodes (3): Setup, IConfiguration, IServiceCollection

### Community 191 - "Setup"
Cohesion: 0.25
Nodes (7): Concurrent safety, Cross-process call path, Files added by this PoC, How it works, How to run, Split-Deployment PoC, What this proves

### Community 192 - "NameFor"
Cohesion: 0.43
Nodes (6): Checkout, FeatureFlags, IAM, Notifications, Products, string

### Community 202 - "IAssemblyReference"
Cohesion: 0.25
Nodes (6): CancellationToken, IOptions, RouteGroupBuilder, Task, Endpoint, Response

### Community 203 - "IAssemblyReference"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 205 - "Request.cs"
Cohesion: 0.40
Nodes (4): Assembly, IConfiguration, IServiceCollection, Setup

### Community 206 - "IAssemblyReference"
Cohesion: 0.20
Nodes (5): Common.Application.Persistence.Outbox, IOutboxDbContext, CancellationToken, DbSet, Task

### Community 208 - ".LogDispatchingNotification"
Cohesion: 0.24
Nodes (7): SessionTokenReuseDetectedIntegrationEvent, CancellationToken, Guid, ILogger, LoggerMessage, Task, SessionTokenReuseDetectedSignalRHandler

### Community 211 - "IAssemblyReference"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response

### Community 213 - "IAssemblyReference"
Cohesion: 0.33
Nodes (5): ActivitySource, Counter, Meter, string, ProductsTelemetry

### Community 215 - ".DeactivateProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 219 - ".RevokeAllSessions"
Cohesion: 0.10
Nodes (15): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint, CancellationToken, RouteGroupBuilder, Task (+7 more)

### Community 221 - "NotificationsTelemetry"
Cohesion: 0.23
Nodes (7): Hub, Exception, ILogger, LoggerMessage, string, Task, NotificationsHub

### Community 225 - "Products.Endpoints.Probe"
Cohesion: 0.40
Nodes (3): Products.Endpoints.Probe, RouteGroupBuilder, Setup

### Community 228 - "IAM"
Cohesion: 0.15
Nodes (11): Common.Application.Validation, HealthCheckOptions, HealthCheckOptionsValidator, InterModuleRequestOptions, InterModuleRequestOptionsValidator, OpenApiOptions, OpenApiOptionsValidator, SignalROptions (+3 more)

### Community 229 - "IntegrationEvent"
Cohesion: 0.33
Nodes (4): Setup, IConfiguration, IHostEnvironment, IServiceCollection

### Community 231 - "AccessTokenDto"
Cohesion: 0.40
Nodes (3): Setup, IApplicationBuilder, IServiceCollection

### Community 233 - "IAM.Endpoints.Otp.VersionNeutral"
Cohesion: 0.10
Nodes (13): IAM.Endpoints, IAM.Endpoints.Otp.VersionNeutral, IAM.Infrastructure.Tokens, IAM.Infrastructure.Tokens.Services, IAM.Infrastructure.Captcha, IAM.Endpoints.Users.VersionNeutral, IAssemblyReference, RouteGroupBuilder (+5 more)

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
Cohesion: 0.50
Nodes (3): IConfiguration, IServiceCollection, Setup

## Knowledge Gaps
- **135 isolated node(s):** `OtpCacheEntry`, `Common.Domain`, `Common.Infrastructure`, `IAssemblyReference`, `IAssemblyReference` (+130 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **100 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `StringExtensions` to `Host Logging & Serilog Setup`, `Setup`, `Host NuGet Deps (OTel/Health)`, `REPR Request Validators`, `BackgroundJobs Service (Hangfire)`, `Functional Result Extensions`, `Launch Settings`, `SecurityHeadersOptions`, `SignalROptions`, `Bounded Capture Streams`, `SignalR Hub & Exception Middleware`, `.GetMeAsync`, `Endpoint`, `EF Core DbContexts`, `Integration Event Handler Base`, `Setup`, `StoreConfiguration`, `EventDispatcher`, `.Get`, `FeatureManagement/RouteHandlerBuilderExtensions.cs`, `Endpoint`, `Endpoint`, `SmsOptions.cs`, `IList`, `ReCaptchaResponse`, `DatabaseOptions.cs`, `Seeder`, `OutboxModule`, `HostCollection`, `RequestLoggingPathPostConfigure`, `Endpoint`, `BaseIntegrationTest`, `net10.0`, `AuditLogRetentionJobRegistrar`, `HostTestFactory`, `HttpClient`, `IntegrationTestWebAppFactory`, `IAM`, `IntegrationEvent`, `IAM.Endpoints.Otp.VersionNeutral`, `JobTargets`, `RequestBody`, `Request.cs`?**
  _High betweenness centrality (0.349) - this node is a cross-community bridge._
- **Why does `Common.Domain.StronglyTypedIds` connect `Functional Result Extensions` to `Endpoint`, `IAM User Identity & Auditing`, `Host Logging & Serilog Setup`, `Cross-Module Comm & Arch Audit Skills`, `BackgroundJobs Service (Hangfire)`, `SecurityHeadersOptions`, `SignalR Hub & Exception Middleware`, `Endpoint`, `CurrentUser`, `PermissionAuthorizationHandler`, `Microsoft.EntityFrameworkCore.Abstractions`, `Hangfire.PostgreSql`, `Aigamo.ResXGenerator`, `CollectionExtensions`, `IResxLocalizer`, `Endpoint`, `.LogDispatchingNotification`, `OutboxCleanupSettings`, `ReverseProxyOptions.cs`?**
  _High betweenness centrality (0.095) - this node is a cross-community bridge._
- **Why does `Result` connect `MassTransit & DI Setup` to `Setup`, `HostEnvironmentExtensions.cs`, `BackgroundJobsOptions`, `IAM Error Catalogs`, `IAM OTP Verify & Token Endpoint`, `Project Files & Solution`, `ModulesOptions`, `OtpOptions`, `Telemetry (ActivitySource/Meter)`, `Outbox Processor & Seeder`, `VerifyPhoneOtpRequest`, `Product Template Aggregate`, `Endpoint`, `Hangfire.PostgreSql`, `EventDispatcher`, `AggregateRoot`, `Endpoint`, `Asp.Versioning.Http`, `double`, `DummyOtpService`, `coverlet.collector`, `decimal`, `AuthenticateResult`, `IAggregateRoot`, `IAssemblyReference`, `IAssemblyReference`, `net10.0`, `IAssemblyReference`, `enabledManagers`, `Activity`, `.DeactivateProductTemplateAsync`, `.RevokeAllSessions`, `OutboxTestWebAppFactory`, `IVariantFeatureManagerExtensions`, `IModelBinder`, `RequestBody`?**
  _High betweenness centrality (0.085) - this node is a cross-community bridge._
- **What connects `OtpCacheEntry`, `Common.Domain`, `Common.Infrastructure` to the rest of the system?**
  _135 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Host Logging & Serilog Setup` be split into smaller, more focused modules?**
  _Cohesion score 0.0951219512195122 - nodes in this community are weakly interconnected._
- **Should `IAM User Identity & Auditing` be split into smaller, more focused modules?**
  _Cohesion score 0.09523809523809523 - nodes in this community are weakly interconnected._
- **Should `Products Store & Audit Services` be split into smaller, more focused modules?**
  _Cohesion score 0.10810810810810811 - nodes in this community are weakly interconnected._