# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-07-25)

## Corpus Check
- 442 files · ~63,319 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2813 nodes · 4964 edges · 305 communities (202 shown, 103 thin omitted)
- Extraction: 98% EXTRACTED · 2% INFERRED · 0% AMBIGUOUS · INFERRED: 76 edges (avg confidence: 0.8)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `dcbfb129`
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
- Common.InterModuleRequests.Contracts
- HostEnvironmentExtensions.cs
- BackgroundJobsOptions
- CaptchaOptions
- CorsOptions
- DatabaseOptions
- HealthCheckOptions
- SecurityHeadersOptions.cs
- ModulesOptions
- OtpPurposes.cs
- OtpOptions
- ICurrentUser
- SecurityHeadersOptions
- SignalROptions
- Setup
- AutoMigrateMarker
- Setup
- .GetMeAsync
- ProductTemplates/v1/Search/Request.cs
- Endpoint
- .UpdateMyProductAsync
- VerifyPhoneOtpRequest
- CurrentUser
- Setup
- Endpoint
- Response
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
- CustomRateLimitingOptions.cs
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
- IAutoMigrateMarker.cs
- Endpoint
- DatabaseOptions.cs
- Setup
- NameFor
- HealthCheckOptions.cs
- Sync AI Settings Command
- IAutoMigrateMarker.cs
- Setup
- Stores/v1/Get/Request.cs
- IAssemblyReference
- IAssemblyReference
- .GetProductTemplateAsync
- Request.cs
- IAssemblyReference
- .LogDispatchingNotification
- IAssemblyReference
- IAssemblyReference
- .DeactivateProductTemplateAsync
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
- SendPhoneOtp
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

## Communities (305 total, 103 thin omitted)

### Community 0 - "Host Logging & Serilog Setup"
Cohesion: 0.10
Nodes (14): IAM.Application.Tokens.Services, IAM.Application.Extensions, IAM.Endpoints.Otp, Common.Infrastructure.Extensions, IAM.Endpoints.Tokens.VersionNeutral.Revoke, IAM.Infrastructure.Telemetry, Common.Domain.ResultMonad, IAM.Application.Persistence (+6 more)

### Community 1 - "IAM User Identity & Auditing"
Cohesion: 0.12
Nodes (20): IEntityTypeConfiguration, ApplicationUserId, DefaultIdType, DateTimeOffset, ApplicationUser, EntityTypeBuilder, IdentityRole, IdentityRoleClaim (+12 more)

### Community 2 - "Products Store & Audit Services"
Cohesion: 0.16
Nodes (11): BackgroundService, DatabaseSeederOrchestrator, CancellationToken, ILogger, LoggerMessage, Task, SeedingCompletionTracker, CancellationToken (+3 more)

### Community 3 - "Notifications Dispatch & SignalR Client"
Cohesion: 0.22
Nodes (7): Products.Endpoints.Stores.v1.AuditLog, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator

### Community 5 - "Cross-Module Comm & Arch Audit Skills"
Cohesion: 0.05
Nodes (35): IdentityDbContext, IdentityUser, StronglyTypedIdHelper, DateOnly, DateTimeOffset, Guid, IReadOnlyCollection, List (+27 more)

### Community 6 - "Domain Event Handling & Outbox Collect"
Cohesion: 0.13
Nodes (12): IAM.Endpoints.Users.VersionNeutral.Search, int, Constants, CancellationToken, IOptions, RouteGroupBuilder, Task, Endpoint (+4 more)

### Community 7 - "Host NuGet Deps (OTel/Health)"
Cohesion: 0.19
Nodes (8): Notifications.Application.Otp, Common.InterModuleRequests.Contracts, Notifications.Infrastructure.InterModuleRequestHandlers, IAM.Infrastructure.RateLimiting, IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, Common.InterModuleRequests.Notifications, Setup, IServiceCollection

### Community 8 - "k6 Load Test Scripts"
Cohesion: 0.31
Nodes (4): AggregateRoot, IEnumerable, IReadOnlyCollection, List

### Community 9 - "REPR Request Validators"
Cohesion: 0.18
Nodes (15): AbstractValidator, Products.Endpoints.Products.v1.My.Update, CustomValidator, RequestBody, Request, RequestBody, RequestBodyValidator, RequestValidator (+7 more)

### Community 10 - "IAM Error Catalogs"
Cohesion: 0.12
Nodes (11): HttpStatusCode, IStringLocalizer, StringLocalizerExtensions, Error, ICollection, IResult, ICollection, IdentityErrors (+3 more)

### Community 11 - "BackgroundJobs Service (Hangfire)"
Cohesion: 0.17
Nodes (11): Common.Application.Search, Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Products.Domain.Products, Products.Infrastructure.Telemetry, Products.Application.Persistence, Products.Domain.Stores (+3 more)

### Community 12 - "IAM OTP Verify & Token Endpoint"
Cohesion: 0.17
Nodes (15): IInterModuleRequestClient, CancellationToken, Task, ITokenService, CancellationToken, HttpContext, IFeatureManager, IOptions (+7 more)

### Community 13 - "Project Files & Solution"
Cohesion: 0.14
Nodes (9): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, CancellationToken, Task, ICaptchaService, Response, CancellationToken, Task, CachedCaptchaService (+1 more)

### Community 14 - "Localized Identity Errors"
Cohesion: 0.15
Nodes (3): IdentityError, IdentityErrorDescriber, LocalizedIdentityErrorDescriber

### Community 15 - "Functional Result Extensions"
Cohesion: 0.10
Nodes (9): Common.Infrastructure.Persistence, Common.Domain.StronglyTypedIds, IAM.Domain.Identity.DomainEvents.v1, Common.Domain.Events, IAM.Domain.Identity.Sessions, Common.Infrastructure.EventBus, Common.Domain.Entities, Common.Domain.Aggregates (+1 more)

### Community 16 - "Launch Settings"
Cohesion: 0.83
Nodes (3): CaptchaOptions, CaptchaOptionsValidator, CaptchaProvider

### Community 17 - "Module Installers (IModule)"
Cohesion: 0.16
Nodes (9): Common.Application.BackgroundJobs, BackgroundJobs, RecurringJobOptions, IRecurringBackgroundJobs, Action, Expression, Func, Task (+1 more)

### Community 18 - "Host Infrastructure Setup"
Cohesion: 0.18
Nodes (14): DomainEvent, DateTimeOffset, DefaultIdType, V1AllSessionsRevokedDomainEvent, V1RefreshTokenRevokedDomainEvent, V1RefreshTokenUpdatedDomainEvent, V1SessionCreatedDomainEvent, V1SessionRefreshedDomainEvent (+6 more)

### Community 19 - "IAM OTP Send & Captcha"
Cohesion: 0.28
Nodes (6): UserRegisteredIntegrationEvent, CancellationToken, ILogger, LoggerMessage, Task, UserRegisteredSignalRHandler

### Community 20 - "Authz Constants & Feature Flags"
Cohesion: 0.13
Nodes (11): IAM.Endpoints.Common.Validations, IResxLocalizer, IRuleBuilder, IRuleBuilderOptions, CommonValidations, IResxLocalizer, IRuleBuilder, IRuleBuilderOptions (+3 more)

### Community 21 - "Bounded Capture Streams"
Cohesion: 0.07
Nodes (20): bool, byte, Memory, PathString, ReadOnlyMemory, ReadOnlySpan, SeekOrigin, IApplicationBuilder (+12 more)

### Community 22 - "SignalR Hub & Exception Middleware"
Cohesion: 0.09
Nodes (11): Products.Infrastructure.Persistence, Products.Infrastructure.Persistence.Seeding, Common.Application.Persistence, IAM.Infrastructure.Persistence, IAM.Infrastructure.Persistence.Seeding, Common.InterModuleRequests.IAM, IAM.Domain.Identity, IAM.Infrastructure.InterModuleRequestHandlers (+3 more)

### Community 23 - "Telemetry (ActivitySource/Meter)"
Cohesion: 0.06
Nodes (34): SendMessageBody, SendRequestBody, CancellationToken, Task, ISmsGateway, SmsCategory, SmsMessage, CancellationToken (+26 more)

### Community 24 - "Outbox Processor & Seeder"
Cohesion: 0.12
Nodes (14): IAM.Endpoints.Tokens.VersionNeutral.Refresh, CancellationToken, HttpContext, ILogger, IOptions, LoggerMessage, RouteGroupBuilder, Task (+6 more)

### Community 25 - "EF Core DbContexts"
Cohesion: 0.20
Nodes (5): Common.Infrastructure.Persistence.Auditing, Setup, IServiceCollection, Setup, IServiceCollection

### Community 26 - "Integration Event Handler Base"
Cohesion: 0.30
Nodes (7): CancellationToken, Exception, ILogger, LoggerMessage, Task, TimeSpan, OutboxProcessor

### Community 27 - "Product Template Aggregate"
Cohesion: 0.18
Nodes (9): Products.Endpoints.Products.v1.My.Search, CancellationToken, IOptions, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator (+1 more)

### Community 28 - "Outbox Message & Tokens"
Cohesion: 0.40
Nodes (3): ProblemDetails, ProblemDetailsExtensions, ICollection

### Community 29 - "MassTransit & DI Setup"
Cohesion: 0.22
Nodes (7): Result, AsyncExtensions, SyncExtensions, Action, Func, Task, VerifyPhoneOtpResponseExtensions

### Community 30 - "PermissionAuthorizationHandler"
Cohesion: 0.05
Nodes (30): AuthorizationHandler, AuthorizationHandlerContext, AuthorizationPolicy, ClaimsPrincipal, IAM.Infrastructure.Auth.Jwt, IAM.Infrastructure.Auth.Services, IAM.Infrastructure.Auth, IAM.Application.Auth.Services (+22 more)

### Community 31 - "DbSet"
Cohesion: 0.14
Nodes (15): LoadAll, Names, Assembly, Exception, IApplicationBuilder, IConfiguration, IEnumerable, ILogger (+7 more)

### Community 32 - "CustomRateLimitingOptions"
Cohesion: 0.40
Nodes (4): IAM.Endpoints.Users.VersionNeutral.Me.Get, DateOnly, IReadOnlyCollection, Response

### Community 33 - "PaginationRequestValidator"
Cohesion: 0.20
Nodes (10): SendPhoneOtpRequest, SendPhoneOtpResponse, CancellationToken, IFeatureManager, RouteGroupBuilder, Task, Endpoint, CancellationToken (+2 more)

### Community 34 - "Microsoft.EntityFrameworkCore.Abstractions"
Cohesion: 0.14
Nodes (12): Products.Endpoints.Probe.v1, IInterModuleRequest, GetSeedUserIdsRequest, GetSeedUserIdsResponse, CancellationToken, Task, GetSeedUserIdsRequestHandler, CancellationToken (+4 more)

### Community 35 - "ISearchLocalized"
Cohesion: 0.33
Nodes (7): DomainEventHandlerBase, CancellationToken, Task, SimulateSomeBusinessHandler, StoreCreatedIntegrationEventPublishingHandler, V1StoreCreatedDomainEventHandlers, V1StoreCreatedDomainEvent

### Community 36 - "StoreConfiguration"
Cohesion: 0.23
Nodes (8): CustomRateLimitingOptions, CustomRateLimitingOptionsValidator, FixedWindow, FixedWindowValidator, Action, IEnumerable, RateLimiterOptions, Policies

### Community 37 - "Setup"
Cohesion: 0.16
Nodes (11): IEnumerable, IReadOnlyCollection, List, ApplicationUser, ApplicationUserConfig, Action, DateOnly, ILogger (+3 more)

### Community 38 - "Hangfire.PostgreSql"
Cohesion: 0.13
Nodes (11): Products.Endpoints.Stores.v1.Create, Products.Endpoints.Stores, RouteGroupBuilder, Setup, CancellationToken, RouteGroupBuilder, Task, Endpoint (+3 more)

### Community 39 - "EventDispatcher"
Cohesion: 0.13
Nodes (12): Products.Endpoints.Products.v1.Search, decimal, int, Constants, CancellationToken, IOptions, RouteGroupBuilder, Task (+4 more)

### Community 40 - "NetArchTest.Rules"
Cohesion: 0.27
Nodes (8): OpenTelemetryBuilder, ResourceBuilder, Action, IConfiguration, IHostEnvironment, IReadOnlyList, IServiceCollection, Setup

### Community 41 - "Aigamo.ResXGenerator"
Cohesion: 0.24
Nodes (7): Products.Endpoints.ProductTemplates.v1.Search, Common.Application.DTOs, Products.Endpoints.ProductTemplates.v1.Get, AuditableEntityResponse, DateTimeOffset, Response, Response

### Community 42 - "IOperationFilter"
Cohesion: 0.12
Nodes (19): EventDispatcher, ActivitySource, CancellationToken, ILogger, LoggerMessage, Task, BaseDbContext, CancellationToken (+11 more)

### Community 43 - "IRateLimiterPolicy"
Cohesion: 0.05
Nodes (35): IRateLimiterPolicy, CancellationToken, Func, HttpContext, OnRejectedContext, RateLimitPartition, ValueTask, Policies (+27 more)

### Community 44 - "Seeder"
Cohesion: 0.23
Nodes (5): ISearchLocalized, Product, IReadOnlyCollection, List, Store

### Community 45 - "AggregateRoot"
Cohesion: 0.16
Nodes (10): IReadOnlyList, List, ProductTemplate, ProductTemplateId, EntityTypeBuilder, ProductTemplateConfiguration, CancellationToken, List (+2 more)

### Community 46 - "ApiVersionSet"
Cohesion: 0.29
Nodes (4): ApiVersionSet, Setup, IEndpointRouteBuilder, IServiceCollection

### Community 48 - "IntegrationTestFactory"
Cohesion: 0.15
Nodes (9): IDatabaseSeeder, CancellationToken, Task, CancellationToken, Task, IamDatabaseSeeder, CancellationToken, Task (+1 more)

### Community 50 - "Asp.Versioning.Http"
Cohesion: 0.36
Nodes (6): AuditLogRetentionService, CancellationToken, DateTimeOffset, ILogger, LoggerMessage, Task

### Community 51 - "IInterModuleRequestHandler"
Cohesion: 0.18
Nodes (8): IConsumer, IInterModuleRequestHandler, CancellationToken, Task, InterModuleRequestHandler, CancellationToken, ConsumeContext, Task

### Community 52 - "double"
Cohesion: 0.17
Nodes (11): DateTime, double, FormUrlEncodedContent, ReCaptchaResponse, CancellationToken, Exception, ILogger, LoggerMessage (+3 more)

### Community 53 - "RouteHandlerBuilderExtensions"
Cohesion: 0.11
Nodes (15): StoreId, CancellationToken, ILogger, int, LoggerMessage, Task, CancellationToken, List (+7 more)

### Community 54 - "IList"
Cohesion: 0.22
Nodes (7): Common.Infrastructure.Resiliency, HttpClient, HttpStandardResilienceOptions, IHttpClientBuilder, Setup, Action, IServiceCollection

### Community 55 - "Microsoft.AspNetCore.SignalR.StackExchangeRedis"
Cohesion: 0.28
Nodes (6): Products.Endpoints.ProductTemplates.v1.Create, CancellationToken, Task, Request, RequestValidator, Response

### Community 56 - "DummyOtpService"
Cohesion: 0.13
Nodes (13): FullTextSearchOptions, FullTextSearchOptionsValidator, Dictionary, IReadOnlyList, string, ISearchLanguageResolver, SearchLanguageResolver, string (+5 more)

### Community 58 - "CollectionExtensions"
Cohesion: 0.32
Nodes (5): StronglyTypedIdWriteOnlyJsonConverter, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 59 - "coverlet.collector"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 61 - "decimal"
Cohesion: 0.20
Nodes (8): Products.Endpoints.Stores.v1.My.AddProduct, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator, Response

### Community 62 - "EndpointFilterDelegate"
Cohesion: 0.10
Nodes (16): Common.Application.EndpointFilters, IEndpointFilter, ResultToCreatedResponseTransformer, ResultToResponseTransformer, EndpointFilterDelegate, EndpointFilterInvocationContext, ValueTask, RouteHandlerBuilderExtensions (+8 more)

### Community 63 - "Hangfire"
Cohesion: 0.33
Nodes (4): IAM.Infrastructure.Tokens, IAM.Infrastructure.Tokens.Services, IServiceCollection, Setup

### Community 64 - "Seeder"
Cohesion: 0.17
Nodes (9): ICoreModule, IModule, Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection (+1 more)

### Community 65 - "ApplyAuditingInterceptor"
Cohesion: 0.14
Nodes (11): SaveChangesInterceptor, ApplyAuditingInterceptor, CancellationToken, DbContextEventData, InterceptionResult, ValueTask, ApplySearchLanguageInterceptor, CancellationToken (+3 more)

### Community 66 - "CustomRoles"
Cohesion: 0.20
Nodes (8): FrozenDictionary, IReadOnlySet, CustomPermissions, HashSet, IEnumerable, CurrentUser, Guid, ICollection

### Community 67 - "AuthenticateResult"
Cohesion: 0.13
Nodes (11): Products.Endpoints.Stores.v1.AddProduct, DbSet, IProductsDbContext, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response (+3 more)

### Community 68 - "ValueObject"
Cohesion: 0.27
Nodes (4): Common.Domain, IComparable, ValueObject, IEnumerable

### Community 69 - "IResxLocalizer"
Cohesion: 0.25
Nodes (7): StronglyTypedIdReadOnlyJsonConverter, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, IStronglyTypedId, DefaultIdType

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
Cohesion: 0.31
Nodes (4): CollectionExtensions, Func, ICollection, IEnumerable

### Community 75 - "GetSeedUserIdsRequest"
Cohesion: 0.16
Nodes (11): Products.Endpoints.Stores.v1.My.RemoveProduct, Products.Endpoints.Stores.v1.RemoveProduct, ProductId, Request, RequestValidator, Request, RequestValidator, Request (+3 more)

### Community 76 - "HostCollection"
Cohesion: 0.20
Nodes (8): IAM.Endpoints.Otp.VersionNeutral.SendForLogin, CancellationToken, IFeatureManager, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator

### Community 77 - "Microsoft.AspNetCore.Identity.EntityFrameworkCore"
Cohesion: 0.29
Nodes (3): Common.Infrastructure.Persistence.AuditLog, Setup, IServiceCollection

### Community 78 - "RequestLoggingPathPostConfigure"
Cohesion: 0.33
Nodes (7): IPostConfigureOptions, RequestLoggingOptions, RequestLoggingOptionsValidator, SensitivePathRule, IList, int, RequestLoggingPathPostConfigure

### Community 79 - "Endpoint"
Cohesion: 0.18
Nodes (7): Common.IntegrationEvents, Notifications.Application.IntegrationEventHandlers, IAM.Application.Users.DomainEventHandlers.v1, Products.Application.Stores.DomainEventHandlers.v1, Common.Application.EventBus, Setup, IServiceCollection

### Community 81 - "HttpContextExtensions"
Cohesion: 0.11
Nodes (15): Outbox, Outbox.Telemetry, ObservableGauge, CancellationToken, ILogger, LoggerMessage, Task, OutboxMetricsJob (+7 more)

### Community 82 - "net10.0"
Cohesion: 0.25
Nodes (6): IAM.Endpoints.Users.VersionNeutral.SelfRegister, int, Constants, Guid, Request, RequestValidator

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
Cohesion: 0.29
Nodes (7): IHostedService, AuditLogRetentionJobRegistrar, CancellationToken, ILogger, LoggerMessage, string, Task

### Community 87 - "AuditLogRetentionService"
Cohesion: 0.19
Nodes (10): Action, Exception, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, ILogger, IServiceCollection (+2 more)

### Community 89 - "ConfigurationManager"
Cohesion: 0.22
Nodes (6): ConfigurationManager, Host, Host.Configurations, Setup, Program, WebApplicationBuilder

### Community 90 - "HostTestFactory"
Cohesion: 0.13
Nodes (13): IAM.Endpoints.Tokens.VersionNeutral.Create, accessToken, DateTimeOffset, expiresAt, ICollection, refreshTokenBytes, CancellationToken, HttpContext (+5 more)

### Community 91 - "HttpClient"
Cohesion: 0.20
Nodes (7): IAM.Domain.Captcha, IAM.Infrastructure.Captcha.Services, IAM.Application.Captcha.Services, CaptchaErrors, IConfiguration, IServiceCollection, Setup

### Community 92 - "IntegrationTestWebAppFactory"
Cohesion: 0.21
Nodes (7): Task, Seeder, IdentityRole, ILogger, LoggerMessage, Task, Seeder

### Community 93 - "IOpenApiSchema"
Cohesion: 0.17
Nodes (9): Lock, IIntegrationEventOutbox, IntegrationEventOutbox, IReadOnlyList, List, CancellationToken, Task, V1SessionRevokedDomainEventHandler (+1 more)

### Community 94 - "ISmsService"
Cohesion: 0.33
Nodes (4): IAM.Infrastructure.Persistence.EntityConfigurations, Common.Infrastructure.Persistence.EntityConfigurations, Common.Infrastructure.Persistence.ValueConverters, Products.Infrastructure.Persistence.EntityConfigurations

### Community 95 - "Request.cs"
Cohesion: 0.20
Nodes (8): ChangeTracker, DatabaseFacade, EntityEntry, IDisposable, IDbContext, CancellationToken, DbSet, Task

### Community 96 - "OutboxCleanupSettings"
Cohesion: 0.27
Nodes (5): IOutboxMessage, DateTimeOffset, OutboxMessage, DateTimeOffset, TimeSpan

### Community 97 - "OutboxTestWebAppFactory"
Cohesion: 0.14
Nodes (12): DbSet, IdentityRole, IdentityRoleClaim, IdentityUserClaim, IdentityUserLogin, IdentityUserRole, IdentityUserToken, IIAMDbContext (+4 more)

### Community 98 - "IDatabaseSeeder"
Cohesion: 0.22
Nodes (7): Products.Endpoints.Stores.v1.My.AuditLog, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator

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
Cohesion: 0.33
Nodes (4): IVariantFeatureManager, IVariantFeatureManagerExtensions, CancellationToken, Task

### Community 104 - "IMiddleware"
Cohesion: 0.33
Nodes (5): Products.Endpoints.Products.v1.AuditLog, PaginationRequestValidator, int, Request, RequestValidator

### Community 105 - "IModelBinder"
Cohesion: 0.15
Nodes (11): IFusionCache, JwtOptions, JwtOptionsValidator, IReadOnlyCollection, CancellationToken, HttpContext, IOptions, RouteGroupBuilder (+3 more)

### Community 106 - ".RemoveMyProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 107 - "ProductsModule"
Cohesion: 0.25
Nodes (7): IAggregateRoot, IEnumerable, IReadOnlyCollection, AuditableEntity, DateTimeOffset, IAuditableEntity, DateTimeOffset

### Community 108 - "ReverseProxyOptions.cs"
Cohesion: 0.39
Nodes (4): CancellationToken, IReadOnlyList, Task, SignalRNotificationDispatcher

### Community 109 - "RequestBody"
Cohesion: 0.20
Nodes (8): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator, Response

### Community 110 - "JobTargets"
Cohesion: 0.15
Nodes (10): Notifications.Application.Sms, Notifications.Infrastructure.Sms, Notifications.Infrastructure.Telemetry, Notifications.Infrastructure.Sms.NetGsm, Notifications.Infrastructure, IAssemblyReference, IConfiguration, IServiceCollection (+2 more)

### Community 111 - "CacheKeys"
Cohesion: 0.26
Nodes (7): IBackgroundJobs, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 112 - "StringExtensions"
Cohesion: 0.20
Nodes (7): Common.Infrastructure.Modules, Common.Endpoints.Versioning, Common.Application.Options, Host.Middlewares, Host.Infrastructure, BackgroundJobsOptions, BackgroundJobsOptionsValidator

### Community 113 - "ReverseProxyOptions"
Cohesion: 0.25
Nodes (5): Products.Endpoints.ProductTemplates, RouteGroupBuilder, Setup, RouteGroupBuilder, Endpoint

### Community 114 - "OutboxMetricsJob"
Cohesion: 0.25
Nodes (7): JsonConverter, StrictDateTimeOffsetJsonConverter, DateTimeOffset, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 115 - "SearchLanguageResolver"
Cohesion: 0.26
Nodes (7): BackgroundJobsService, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 116 - "IRoleService"
Cohesion: 0.40
Nodes (5): Products.Endpoints.Products.v1.Update, RequestBody, Request, RequestBody, RequestValidator

### Community 117 - "DummySmsGateway"
Cohesion: 0.22
Nodes (6): Products.Endpoints.Products.v1.My.Get, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response

### Community 118 - "RequestBody"
Cohesion: 0.14
Nodes (10): IHostBuilder, KeyValuePair, LoggerConfiguration, LoggerMinimumLevelConfiguration, ObservabilityOptions, ObservabilityOptionsValidator, Dictionary, IEnumerable (+2 more)

### Community 119 - "RequestBody"
Cohesion: 0.18
Nodes (8): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, ProductsModule

### Community 120 - "Request.cs"
Cohesion: 0.67
Nodes (3): ModulesOptions, ModulesOptionsValidator, IReadOnlyList

### Community 121 - "Setup"
Cohesion: 0.33
Nodes (4): Products.Endpoints.Products.v1.Get, CancellationToken, Task, Response

### Community 122 - "Setup"
Cohesion: 0.35
Nodes (6): CancellationToken, IReadOnlyList, Task, INotificationDispatcher, Task, NotificationPayload

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

### Community 127 - "Setup"
Cohesion: 0.33
Nodes (5): IntegrationEvent, DateTimeOffset, DefaultIdType, ProductCreatedIntegrationEvent, StoreCreatedIntegrationEvent

### Community 128 - "Endpoint"
Cohesion: 0.13
Nodes (10): Common.Application.JsonConverters, DomainEventConverter, JsonSerializerOptions, EventConverter, JsonSerializerOptions, IntegrationEventConverter, JsonSerializerOptions, StronglyTypedIdValueConverter (+2 more)

### Community 129 - "Setup"
Cohesion: 0.25
Nodes (5): Products.Endpoints.Products, RouteGroupBuilder, Setup, RouteGroupBuilder, Endpoint

### Community 130 - "Setup"
Cohesion: 0.16
Nodes (17): Configuration-Driven Module Loading, IntegrationEvents (Async Cross-Module), IAM Module, Notifications Module, Products Module, Observability (OpenTelemetry), docker-compose.yml (Base Stack), docker-compose.app.yml (App-Only) (+9 more)

### Community 131 - "Setup"
Cohesion: 0.70
Nodes (4): OutboxCleanupSettings, OutboxCleanupSettingsValidator, OutboxOptions, OutboxOptionsValidator

### Community 132 - ".ToResult"
Cohesion: 0.19
Nodes (6): IdentityResult, Func, Task, IdentityResultExtensions, CancellationToken, Task

### Community 134 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.40
Nodes (3): HttpContext, Task, SecurityHeadersMiddleware

### Community 135 - "HostEnvironmentExtensions.cs"
Cohesion: 0.13
Nodes (12): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, ICurrentUser, Guid, ICollection, CancellationToken, IReadOnlyCollection, RouteGroupBuilder, Task (+4 more)

### Community 136 - "BackgroundJobsOptions"
Cohesion: 0.33
Nodes (6): PersistenceQueryableExtensions, CancellationToken, Expression, Func, IQueryable, Task

### Community 137 - "CaptchaOptions"
Cohesion: 0.21
Nodes (9): PartitionedRateLimiter, CancellationToken, Func, HttpContext, IConfiguration, IServiceCollection, OnRejectedContext, ValueTask (+1 more)

### Community 138 - "CorsOptions"
Cohesion: 0.40
Nodes (4): Action, Expression, Func, Task

### Community 139 - "DatabaseOptions"
Cohesion: 0.20
Nodes (6): BackgroundJobsModule, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection

### Community 140 - "HealthCheckOptions"
Cohesion: 0.47
Nodes (4): AuditLogEntry, DefaultIdType, AuditLogEntryConfiguration, EntityTypeBuilder

### Community 141 - "SecurityHeadersOptions.cs"
Cohesion: 0.40
Nodes (3): IEventHandler, CancellationToken, Task

### Community 142 - "ModulesOptions"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 143 - "OtpPurposes.cs"
Cohesion: 0.40
Nodes (5): Products.Endpoints.Stores.v1.Update, RequestBody, Request, RequestBody, RequestValidator

### Community 144 - "OtpOptions"
Cohesion: 0.20
Nodes (7): IAM.Endpoints.Users.VersionNeutral.Get, CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, Response

### Community 145 - "ICurrentUser"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 146 - "SecurityHeadersOptions"
Cohesion: 0.17
Nodes (6): Notifications.Infrastructure.Hubs, Notifications.Application.Hubs, INotificationsClient, IConfiguration, IServiceCollection, Setup

### Community 147 - "SignalROptions"
Cohesion: 0.40
Nodes (3): MassTransitInterModuleRequestClient, CancellationToken, Task

### Community 148 - "Setup"
Cohesion: 0.35
Nodes (6): Assembly, IApplicationBuilder, IConfiguration, IServiceCollection, IWebHostEnvironment, Setup

### Community 149 - "AutoMigrateMarker"
Cohesion: 0.15
Nodes (8): CancellationToken, Task, IEventHandlerWrapper, CancellationToken, Task, IEvent, DateTimeOffset, DefaultIdType

### Community 150 - "Setup"
Cohesion: 0.32
Nodes (6): accessToken, DateTimeOffset, expiresAt, ICollection, refreshTokenBytes, TokenService

### Community 151 - ".GetMeAsync"
Cohesion: 0.50
Nodes (3): ReverseProxyOptions, ReverseProxyOptionsValidator, IReadOnlyList

### Community 152 - "ProductTemplates/v1/Search/Request.cs"
Cohesion: 0.14
Nodes (11): PaginationRequest, PaginationQueryableExtensions, CancellationToken, Expression, Func, IQueryable, Task, int (+3 more)

### Community 153 - "Endpoint"
Cohesion: 0.06
Nodes (26): ApiVersionDescription, Host.Swagger, IConfigureOptions, IOpenApiSchema, IOperationFilter, ISchemaFilter, JsonValue, OpenApiInfo (+18 more)

### Community 154 - ".UpdateMyProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 155 - "VerifyPhoneOtpRequest"
Cohesion: 0.33
Nodes (6): OtpVerificationFailureReason, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, CancellationToken, Task, VerifyPhoneOtpRequestHandler

### Community 156 - "CurrentUser"
Cohesion: 0.11
Nodes (15): Products.Endpoints.ProductTemplates.v1.Deactivate, Common.Application.ModelBinders, Products.Endpoints.ProductTemplates.v1.Activate, IModelBinder, ModelBindingContext, StronglyTypedIdBinder, Task, Request (+7 more)

### Community 157 - "Setup"
Cohesion: 0.39
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, OutboxCleanupJob

### Community 158 - "Endpoint"
Cohesion: 0.22
Nodes (6): Products.Endpoints.Stores.v1.My.Get, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response

### Community 159 - "Response"
Cohesion: 0.50
Nodes (4): CancellationToken, Task, V1UserRegisteredDomainEventHandler, V1UserRegisteredDomainEvent

### Community 160 - "ValidationContextExtensions"
Cohesion: 0.40
Nodes (3): ValidationContextExtensions, string, ValidationContext

### Community 161 - "Endpoint"
Cohesion: 0.05
Nodes (28): Common.Application.Caching, Notifications.Infrastructure.Otp, SemaphoreSlim, CacheKeys, For, OtpCacheEntry, CancellationToken, Task (+20 more)

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

### Community 172 - "Endpoint"
Cohesion: 0.12
Nodes (12): BackgroundJobs.Telemetry, IServerFilter, PerformedContext, PerformingContext, JobMetricsFilter, string, BackgroundJobsTelemetry, ActivitySource (+4 more)

### Community 174 - "Endpoint"
Cohesion: 0.29
Nodes (5): IAM.Application.Tokens.DTOs, DateTimeOffset, AccessTokenDto, DateTimeOffset, TokensDto

### Community 175 - "Endpoint"
Cohesion: 0.14
Nodes (11): AuditLogDto, PaginationResponse, DbContextExtensions, CancellationToken, DbSet, JsonSerializerOptions, Task, CancellationToken (+3 more)

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
Cohesion: 0.33
Nodes (4): Common.Infrastructure.Caching, Setup, IConfiguration, IServiceCollection

### Community 188 - "IAutoMigrateMarker.cs"
Cohesion: 0.67
Nodes (3): Guid, Request, RequestValidator

### Community 191 - "Setup"
Cohesion: 0.25
Nodes (7): Concurrent safety, Cross-process call path, Files added by this PoC, How it works, How to run, Split-Deployment PoC, What this proves

### Community 192 - "NameFor"
Cohesion: 0.12
Nodes (14): Common.Application.FeatureManagement, FeatureFlagResultExtensions, Action, Func, IFeatureManager, Task, Checkout, FeatureFlags (+6 more)

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
Cohesion: 0.40
Nodes (4): Assembly, IConfiguration, IServiceCollection, Setup

### Community 206 - "IAssemblyReference"
Cohesion: 0.13
Nodes (11): Common.Infrastructure.Persistence.Outbox, Outbox.Persistence, Common.Application.Persistence.Outbox, DbContext, IOutboxDbContext, CancellationToken, DbSet, Task (+3 more)

### Community 208 - ".LogDispatchingNotification"
Cohesion: 0.28
Nodes (7): SessionTokenReuseDetectedIntegrationEvent, CancellationToken, Guid, ILogger, LoggerMessage, Task, SessionTokenReuseDetectedSignalRHandler

### Community 211 - "IAssemblyReference"
Cohesion: 0.22
Nodes (6): Products.Endpoints.Stores.v1.Get, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response

### Community 213 - "IAssemblyReference"
Cohesion: 0.33
Nodes (5): ActivitySource, Counter, Meter, string, ProductsTelemetry

### Community 215 - ".DeactivateProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 218 - ".RemoveProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 219 - ".RevokeAllSessions"
Cohesion: 0.15
Nodes (10): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint, ActivitySource, Counter, Meter (+2 more)

### Community 221 - "NotificationsTelemetry"
Cohesion: 0.19
Nodes (8): Hub, NotificationGroupName, Exception, ILogger, LoggerMessage, string, Task, NotificationsHub

### Community 223 - "Setup.cs"
Cohesion: 0.40
Nodes (3): IAM.Endpoints.Users.VersionNeutral, RouteGroupBuilder, Setup

### Community 224 - "RequestValidator"
Cohesion: 0.29
Nodes (3): Common.Domain.Extensions, SearchValues, StringExtensions

### Community 225 - "Products.Endpoints.Probe"
Cohesion: 0.40
Nodes (3): Products.Endpoints.Probe, RouteGroupBuilder, Setup

### Community 228 - "IAM"
Cohesion: 0.17
Nodes (10): Common.Application.Validation, AuditLogOptions, AuditLogOptionsValidator, CorsOptions, CorsOptionsValidator, IReadOnlyList, RabbitMqOptions, RabbitMqOptionsValidator (+2 more)

### Community 229 - "IntegrationEvent"
Cohesion: 0.33
Nodes (4): Setup, IConfiguration, IHostEnvironment, IServiceCollection

### Community 231 - "AccessTokenDto"
Cohesion: 0.29
Nodes (4): Common.Infrastructure.Localization, Setup, IApplicationBuilder, IServiceCollection

### Community 233 - "IAM.Endpoints.Otp.VersionNeutral"
Cohesion: 0.17
Nodes (7): IAM.Endpoints, IAM.Endpoints.Otp.VersionNeutral, IAM.Infrastructure.Identity, IAM.Infrastructure.Captcha, IAssemblyReference, RouteGroupBuilder, Setup

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

### Community 250 - "SecurityHeadersOptions.cs"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

## Knowledge Gaps
- **135 isolated node(s):** `OtpCacheEntry`, `Common.Domain`, `Common.Infrastructure`, `IAssemblyReference`, `IAssemblyReference` (+130 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **103 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `StringExtensions` to `Host Logging & Serilog Setup`, `Setup`, `Common.InterModuleRequests.Contracts`, `Host NuGet Deps (OTel/Health)`, `DatabaseOptions`, `BackgroundJobs Service (Hangfire)`, `Functional Result Extensions`, `Launch Settings`, `SecurityHeadersOptions`, `SignalROptions`, `.GetMeAsync`, `EF Core DbContexts`, `Endpoint`, `Endpoint`, `StoreConfiguration`, `.Get`, `IRateLimiterPolicy`, `FeatureManagement/RouteHandlerBuilderExtensions.cs`, `CustomRateLimitingOptions.cs`, `Endpoint`, `Endpoint`, `SmsOptions.cs`, `ReCaptchaResponse`, `DummyOtpService`, `Endpoint`, `IAutoMigrateMarker.cs`, `DatabaseOptions.cs`, `Seeder`, `HealthCheckOptions.cs`, `Sync AI Settings Command`, `Microsoft.AspNetCore.Identity.EntityFrameworkCore`, `RequestLoggingPathPostConfigure`, `Endpoint`, `IAssemblyReference`, `HttpContextExtensions`, `net10.0`, `ConfigurationManager`, `HttpClient`, `ISmsService`, `IAM`, `IntegrationEvent`, `AccessTokenDto`, `IModelBinder`, `IAM.Endpoints.Otp.VersionNeutral`, `JobTargets`, `RequestBody`, `Request.cs`?**
  _High betweenness centrality (0.351) - this node is a cross-community bridge._
- **Why does `Common.Application.Auth` connect `BackgroundJobs Service (Hangfire)` to `Host Logging & Serilog Setup`, `CustomRoles`, `HangfireCustomAuthorizationFilter`, `Endpoint`, `HttpContextTargetingContextAccessor`, `Functional Result Extensions`, `Response`, `Response`, `SignalR Hub & Exception Middleware`, `PermissionAuthorizationHandler`, `Setup`, `Host.Swagger`, `EndpointFilterDelegate`?**
  _High betweenness centrality (0.103) - this node is a cross-community bridge._
- **Why does `Common.Domain.StronglyTypedIds` connect `Functional Result Extensions` to `Endpoint`, `IAM User Identity & Auditing`, `Host Logging & Serilog Setup`, `Cross-Module Comm & Arch Audit Skills`, `Domain Event Handling & Outbox Collect`, `OtpOptions`, `SecurityHeadersOptions`, `IAM OTP Send & Captcha`, `SignalR Hub & Exception Middleware`, `Endpoint`, `CurrentUser`, `PermissionAuthorizationHandler`, `Endpoint`, `CustomRateLimitingOptions`, `Microsoft.EntityFrameworkCore.Abstractions`, `Hangfire.PostgreSql`, `EventDispatcher`, `Aigamo.ResXGenerator`, `IResxLocalizer`, `IAssemblyReference`, `Endpoint`, `IAssemblyReference`, `ISmsService`, `ReverseProxyOptions.cs`, `Setup`?**
  _High betweenness centrality (0.100) - this node is a cross-community bridge._
- **What connects `OtpCacheEntry`, `Common.Domain`, `Common.Infrastructure` to the rest of the system?**
  _135 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Host Logging & Serilog Setup` be split into smaller, more focused modules?**
  _Cohesion score 0.09759759759759759 - nodes in this community are weakly interconnected._
- **Should `IAM User Identity & Auditing` be split into smaller, more focused modules?**
  _Cohesion score 0.11612903225806452 - nodes in this community are weakly interconnected._
- **Should `Cross-Module Comm & Arch Audit Skills` be split into smaller, more focused modules?**
  _Cohesion score 0.052884615384615384 - nodes in this community are weakly interconnected._