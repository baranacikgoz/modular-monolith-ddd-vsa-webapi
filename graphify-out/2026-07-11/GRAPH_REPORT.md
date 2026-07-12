# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-07-10)

## Corpus Check
- 515 files · ~73,538 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 3161 nodes · 5342 edges · 344 communities (243 shown, 101 thin omitted)
- Extraction: 99% EXTRACTED · 1% INFERRED · 0% AMBIGUOUS · INFERRED: 72 edges (avg confidence: 0.8)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `af228d23`
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
- CachingEntryDefaults
- OutboxCleanupSettings
- OutboxTestWebAppFactory
- IDatabaseSeeder
- HangfireCustomAuthorizationFilter
- IdentityResultExtensions
- ProblemDetailsExtensions
- HttpContextTargetingContextAccessor
- IVariantFeatureManagerExtensions
- IMiddleware
- IModelBinder
- OutboxCleanupJob
- ProductsModule
- Setup
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
- StronglyTypedIdHelper
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
- Setup
- Setup
- Setup
- AuditLogOptions
- BackgroundJobsOptions
- CaptchaOptions
- CorsOptions
- DatabaseOptions
- HealthCheckOptions
- JwtOptions
- ModulesOptions
- OpenApiOptions
- OtpOptions
- ResxLocalizationOptions
- SecurityHeadersOptions
- SignalROptions
- Setup
- AutoMigrateMarker
- Setup
- Setup
- Setup
- Endpoint
- Endpoint
- Endpoint
- CurrentUser
- Setup
- Endpoint
- Setup
- Setup
- Endpoint
- Setup
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
- Setup
- NameFor
- RequestValidator
- Sync AI Settings Command
- IAssemblyReference
- IAssemblyReference
- IInterModuleRequest
- check-csproj-refs.sh
- pre-commit-guard.sh
- sync-reminder.sh
- IAssemblyReference
- IAssemblyReference
- IAssemblyReference
- IAssemblyReference
- Request.cs
- IAssemblyReference
- IAssemblyReference
- IAssemblyReference
- RabbitMqOptions
- IAssemblyReference
- IAssemblyReference
- IAssemblyReference
- IAssemblyReference
- Request.cs
- .DeactivateProductTemplateAsync
- RequestValidator
- .UpdateMyStoreAsync
- .RemoveProductAsync
- Setup.cs
- Host.Swagger
- IAM.Endpoints.Otp.VersionNeutral
- RequestValidator
- Setup.cs
- RequestValidator
- OtpCacheEntry
- IAM
- IntegrationEvent
- Request.cs
- AccessTokenDto
- Request.cs
- GetSeedUserIds
- SendPhoneOtp
- VerifyPhoneOtp
- Setup
- Response
- Response
- Response
- .CreateStorePolicy
- IAutoMigrateMarker.cs
- Response
- Response
- Response
- Response
- Response
- Response
- Response
- Response
- V1ProductQuantityDecreasedDomainEvent
- V1ProductRemovedFromStoreDomainEvent
- V1RefreshTokenRevokedDomainEvent
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
1. `Common.Application.Options` - 82 edges
2. `Result` - 77 edges
3. `Common.Domain.StronglyTypedIds` - 67 edges
4. `Common.Domain.ResultMonad` - 63 edges
5. `Common.Application.Auth` - 59 edges
6. `CustomValidator` - 55 edges
7. `ApplicationUserId` - 51 edges
8. `Common.Application.Validation` - 48 edges
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

## Communities (344 total, 101 thin omitted)

### Community 0 - "Host Logging & Serilog Setup"
Cohesion: 0.14
Nodes (13): JsonConverter, StronglyTypedIdReadOnlyJsonConverter, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, StronglyTypedIdWriteOnlyJsonConverter, JsonSerializerOptions (+5 more)

### Community 1 - "IAM User Identity & Auditing"
Cohesion: 0.12
Nodes (20): IEntityTypeConfiguration, ApplicationUserId, DefaultIdType, DateTimeOffset, ApplicationUser, EntityTypeBuilder, IdentityRole, IdentityRoleClaim (+12 more)

### Community 2 - "Products Store & Audit Services"
Cohesion: 0.11
Nodes (14): IAM.Application.Tokens.Services, IAM.Application.Extensions, IAM.Endpoints.Otp, IAM.Domain.Identity, Common.Infrastructure.Extensions, IAM.Endpoints.Tokens.VersionNeutral.Revoke, IAM.Infrastructure.Telemetry, IAM.Application.Persistence (+6 more)

### Community 3 - "Notifications Dispatch & SignalR Client"
Cohesion: 0.33
Nodes (5): Memory, ReadOnlyMemory, CancellationToken, Task, ValueTask

### Community 5 - "Cross-Module Comm & Arch Audit Skills"
Cohesion: 0.18
Nodes (10): DateTimeOffset, Guid, DateTimeOffset, Guid, IReadOnlyCollection, List, Session, SessionRevokedReason (+2 more)

### Community 6 - "Domain Event Handling & Outbox Collect"
Cohesion: 0.31
Nodes (6): IdentityUser, DateOnly, IReadOnlyCollection, List, ApplicationUser, Uri

### Community 7 - "Host NuGet Deps (OTel/Health)"
Cohesion: 0.05
Nodes (36): AspNetCore.HealthChecks.NpgSql, AspNetCore.HealthChecks.RabbitMQ, AspNetCore.HealthChecks.Redis, AspNetCore.HealthChecks.UI.Client, Elastic.Serilog.Sinks, FluentValidation.DependencyInjectionExtensions, MassTransit.RabbitMQ, Microsoft.VisualStudio.Azure.Containers.Tools.Targets (+28 more)

### Community 8 - "k6 Load Test Scripts"
Cohesion: 0.16
Nodes (26): login(), register(), revoke(), sendOtpForLogin(), sendOtpForRegistration(), TURKISH_NAMES, turkishName(), bearerHeaders() (+18 more)

### Community 9 - "REPR Request Validators"
Cohesion: 0.19
Nodes (15): AbstractValidator, Products.Endpoints.Products.v1.My.Update, OutboxCleanupSettings, OutboxCleanupSettingsValidator, OutboxOptions, OutboxOptionsValidator, CustomValidator, RequestBody (+7 more)

### Community 10 - "IAM Error Catalogs"
Cohesion: 0.09
Nodes (15): HttpStatusCode, IStringLocalizer, StringLocalizerExtensions, Error, ICollection, IResult, Func, Task (+7 more)

### Community 11 - "BackgroundJobs Service (Hangfire)"
Cohesion: 0.13
Nodes (15): Common.Application.Search, Common.Application.AuditLog, Common.Infrastructure.Persistence.Extensions, Products.Endpoints.Products.v1.Search, Common.Application.Extensions, Products.Domain.Products, Products.Infrastructure.Telemetry, Products.Application.Persistence (+7 more)

### Community 12 - "IAM OTP Verify & Token Endpoint"
Cohesion: 0.13
Nodes (20): OtpVerificationFailureReason, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, VerifyPhoneOtpResponseExtensions, CancellationToken, HttpContext, IFeatureManager, IOptions (+12 more)

### Community 13 - "Project Files & Solution"
Cohesion: 0.09
Nodes (5): Microsoft.NET.Sdk, Microsoft.NET.Sdk, Microsoft.NET.Sdk, Microsoft.NET.Sdk, docker-compose

### Community 14 - "Localized Identity Errors"
Cohesion: 0.13
Nodes (4): IAM.Infrastructure.Identity, IdentityError, IdentityErrorDescriber, LocalizedIdentityErrorDescriber

### Community 15 - "Functional Result Extensions"
Cohesion: 0.09
Nodes (9): Products.Domain.Products.DomainEvents.v1, Common.Domain.StronglyTypedIds, IAM.Domain.Identity.DomainEvents.v1, Common.Domain.Events, IAM.Domain.Identity.Sessions, Common.Infrastructure.EventBus, Common.Domain.Entities, Common.Domain.Aggregates (+1 more)

### Community 16 - "Launch Settings"
Cohesion: 0.10
Nodes (10): Common.Application.FeatureManagement, IAM.Domain.Captcha, IAM.Infrastructure.Captcha.Services, IAM.Application.Captcha.Services, IAM.Infrastructure.RateLimiting, DateTime, ReCaptchaResponse, Policies (+2 more)

### Community 17 - "Module Installers (IModule)"
Cohesion: 0.08
Nodes (13): Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Common.Application.Persistence, IAM.Infrastructure.Persistence, IAM.Infrastructure.Persistence.Seeding, Common.Infrastructure.Persistence.Auditing, Common.Infrastructure.Persistence.DbContext, Setup (+5 more)

### Community 18 - "Host Infrastructure Setup"
Cohesion: 0.15
Nodes (9): IDatabaseSeeder, CancellationToken, Task, CancellationToken, Task, IamDatabaseSeeder, CancellationToken, Task (+1 more)

### Community 19 - "IAM OTP Send & Captcha"
Cohesion: 0.08
Nodes (24): commandName, environmentVariables, launchBrowser, launchUrl, publishAllPorts, ASPNETCORE_ENVIRONMENT, ASPNETCORE_HTTP_PORTS, applicationUrl (+16 more)

### Community 20 - "Authz Constants & Feature Flags"
Cohesion: 0.09
Nodes (18): IAM.Endpoints.Otp.VersionNeutral.SendForLogin, IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, IAM.Endpoints.Common.Validations, IResxLocalizer, IRuleBuilder, IRuleBuilderOptions, CommonValidations, IResxLocalizer (+10 more)

### Community 21 - "Bounded Capture Streams"
Cohesion: 0.15
Nodes (7): bool, byte, SeekOrigin, int, BoundedCaptureStream, BoundedRequestCaptureStream, Stream

### Community 22 - "SignalR Hub & Exception Middleware"
Cohesion: 0.08
Nodes (15): IAM.Endpoints.Captcha.VersionNeutral, IAM.Endpoints, IAM.Endpoints.Otp.VersionNeutral, IAM.Infrastructure.Tokens, IAM.Infrastructure.Tokens.Services, IAM.Infrastructure.Captcha, RouteGroupBuilder, Endpoint (+7 more)

### Community 23 - "Telemetry (ActivitySource/Meter)"
Cohesion: 0.14
Nodes (9): Notifications.Infrastructure.Sms, Notifications.Application, IAssemblyReference, Task, ISmsService, Task, DummySmsService, IServiceCollection (+1 more)

### Community 24 - "Outbox Processor & Seeder"
Cohesion: 0.09
Nodes (20): IAM.Endpoints.Tokens.VersionNeutral.Refresh, DbSet, IdentityRole, IdentityRoleClaim, IdentityUserClaim, IdentityUserLogin, IdentityUserRole, IdentityUserToken (+12 more)

### Community 25 - "EF Core DbContexts"
Cohesion: 0.25
Nodes (6): CancellationToken, IOptions, RouteGroupBuilder, Task, Endpoint, Response

### Community 26 - "Integration Event Handler Base"
Cohesion: 0.13
Nodes (8): Notifications.Infrastructure.Telemetry, Notifications.Infrastructure.Hubs, Notifications.Infrastructure, NotificationGroupName, IConfiguration, IServiceCollection, Setup, IAssemblyReference

### Community 27 - "Product Template Aggregate"
Cohesion: 0.08
Nodes (19): AuditLogDto, PaginationResponse, DbContextExtensions, CancellationToken, DbSet, JsonSerializerOptions, Task, CancellationToken (+11 more)

### Community 28 - "Outbox Message & Tokens"
Cohesion: 0.40
Nodes (3): ProblemDetails, ProblemDetailsExtensions, ICollection

### Community 29 - "MassTransit & DI Setup"
Cohesion: 0.26
Nodes (6): Result, AsyncExtensions, SyncExtensions, Action, Func, Task

### Community 30 - "PermissionAuthorizationHandler"
Cohesion: 0.05
Nodes (30): AuthorizationHandler, AuthorizationPolicy, ClaimsPrincipal, IAM.Infrastructure.Auth.Jwt, IAM.Infrastructure.Auth.Services, IAM.Infrastructure.Auth, IAM.Application.Auth.Services, IAM.Application.Auth (+22 more)

### Community 31 - "DbSet"
Cohesion: 0.14
Nodes (14): LoadAll, Names, Assembly, Exception, IApplicationBuilder, IConfiguration, IEnumerable, ILogger (+6 more)

### Community 32 - "CustomRateLimitingOptions"
Cohesion: 0.11
Nodes (17): Products.Endpoints.Products.v1.My.Get, Products.Endpoints.ProductTemplates.v1.Search, Common.Application.DTOs, Products.Endpoints.Stores.v1.Get, IAM.Endpoints.Users.VersionNeutral.Me.Get, Products.Endpoints.ProductTemplates.v1.Get, Products.Endpoints.Products.v1.Get, AuditableEntityResponse (+9 more)

### Community 33 - "PaginationRequestValidator"
Cohesion: 0.15
Nodes (14): SendPhoneOtpRequest, SendPhoneOtpResponse, CancellationToken, IFeatureManager, RouteGroupBuilder, Task, Endpoint, CancellationToken (+6 more)

### Community 34 - "Microsoft.EntityFrameworkCore.Abstractions"
Cohesion: 0.13
Nodes (13): Microsoft.EntityFrameworkCore.Abstractions, Microsoft.Extensions.Identity.Core, Microsoft.NET.Sdk, Microsoft.AspNetCore.Authentication.JwtBearer, Microsoft.NET.Sdk, Microsoft.NET.Sdk, Asp.Versioning.Http, FluentValidation (+5 more)

### Community 35 - "ISearchLocalized"
Cohesion: 0.13
Nodes (9): Products.Endpoints.Stores.v1.Create, Products.Endpoints.Stores, RouteGroupBuilder, Setup, CancellationToken, RouteGroupBuilder, Task, Endpoint (+1 more)

### Community 36 - "StoreConfiguration"
Cohesion: 0.18
Nodes (10): CustomRateLimitingOptions, CustomRateLimitingOptionsValidator, FixedWindow, FixedWindowValidator, Action, IEnumerable, RateLimiterOptions, Policies (+2 more)

### Community 37 - "Setup"
Cohesion: 0.22
Nodes (8): Notifications.Application.Hubs, CancellationToken, IReadOnlyList, Task, INotificationDispatcher, Task, INotificationsClient, NotificationPayload

### Community 38 - "Hangfire.PostgreSql"
Cohesion: 0.18
Nodes (10): Microsoft.Extensions.Caching.StackExchangeRedis, Microsoft.Extensions.Http.Resilience, Microsoft.FeatureManagement.AspNetCore, Npgsql, Scrutor, ZiggyCreatures.FusionCache.Backplane.StackExchangeRedis, ZiggyCreatures.FusionCache.Serialization.SystemTextJson, MassTransit (+2 more)

### Community 39 - "EventDispatcher"
Cohesion: 0.14
Nodes (11): AggregateRoot, IEnumerable, IReadOnlyCollection, List, IAggregateRoot, IEnumerable, IReadOnlyCollection, AuditableEntity (+3 more)

### Community 40 - "NetArchTest.Rules"
Cohesion: 0.12
Nodes (15): net10.0, NetArchTest.Rules, Respawn, Microsoft.AspNetCore.Mvc.Testing, Microsoft.NET.Test.Sdk, NSubstitute, Testcontainers.PostgreSql, xunit.runner.visualstudio (+7 more)

### Community 41 - "Aigamo.ResXGenerator"
Cohesion: 0.11
Nodes (17): Aigamo.ResXGenerator, EntityFrameworkCore.Exceptions.PostgreSQL, Microsoft.Extensions.DependencyInjection.Abstractions, Microsoft.Extensions.Localization, Microsoft.FeatureManagement, Asp.Versioning.Http, FluentValidation, MassTransit (+9 more)

### Community 42 - "IOperationFilter"
Cohesion: 0.17
Nodes (13): Lock, IntegrationEventOutbox, IReadOnlyList, List, OutboxSaveHelper, CancellationToken, DbContext, Exception (+5 more)

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
Cohesion: 0.12
Nodes (10): ApiVersionSet, Common.Endpoints.Versioning, Products.Endpoints.Probe, Products.Endpoints, Setup, IEndpointRouteBuilder, IServiceCollection, IAssemblyReference (+2 more)

### Community 48 - "IntegrationTestFactory"
Cohesion: 0.24
Nodes (6): PostgreSqlContainer, Program, IntegrationTestFactory, IWebHostBuilder, ValueTask, WebApplicationFactory

### Community 50 - "Asp.Versioning.Http"
Cohesion: 0.15
Nodes (10): Microsoft.NET.Sdk, Microsoft.NET.Sdk, Microsoft.NET.Sdk, Microsoft.EntityFrameworkCore.Design, Npgsql.EntityFrameworkCore.PostgreSQL, Microsoft.NET.Sdk, Bogus, Microsoft.NET.Test.Sdk (+2 more)

### Community 51 - "IInterModuleRequestHandler"
Cohesion: 0.18
Nodes (8): IConsumer, IInterModuleRequestHandler, CancellationToken, Task, InterModuleRequestHandler, CancellationToken, ConsumeContext, Task

### Community 52 - "double"
Cohesion: 0.22
Nodes (9): double, FormUrlEncodedContent, ReCaptchaResponse, CancellationToken, Exception, ILogger, LoggerMessage, Task (+1 more)

### Community 53 - "RouteHandlerBuilderExtensions"
Cohesion: 0.15
Nodes (11): StoreId, CancellationToken, ILogger, int, LoggerMessage, Task, Seeder, CancellationToken (+3 more)

### Community 54 - "IList"
Cohesion: 0.24
Nodes (6): PathString, HttpContext, IList, RequestDelegate, string, RequestResponseBodyLoggingMiddleware

### Community 55 - "Microsoft.AspNetCore.SignalR.StackExchangeRedis"
Cohesion: 0.20
Nodes (8): Microsoft.AspNetCore.SignalR.StackExchangeRedis, Microsoft.NET.Sdk, MassTransit, Microsoft.NET.Test.Sdk, NSubstitute, xunit.runner.visualstudio, ZiggyCreatures.FusionCache, Microsoft.NET.Sdk

### Community 56 - "DummyOtpService"
Cohesion: 0.13
Nodes (12): IAM.Endpoints.Users.VersionNeutral.Search, int, Constants, CancellationToken, IOptions, RouteGroupBuilder, Task, Endpoint (+4 more)

### Community 58 - "CollectionExtensions"
Cohesion: 0.28
Nodes (5): Common.Application.BackgroundJobs, BackgroundJobs, RecurringJobOptions, IRecurringBackgroundJobs, RecurringBackgroundJobsService

### Community 59 - "coverlet.collector"
Cohesion: 0.12
Nodes (14): coverlet.collector, Testcontainers.RabbitMq, Hangfire, MassTransit, Microsoft.EntityFrameworkCore.Design, Microsoft.NET.Sdk, Bogus, Microsoft.AspNetCore.Mvc.Testing (+6 more)

### Community 61 - "decimal"
Cohesion: 0.20
Nodes (8): Products.Endpoints.Stores.v1.My.AddProduct, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator, Response

### Community 62 - "EndpointFilterDelegate"
Cohesion: 0.11
Nodes (16): Common.Application.EndpointFilters, IEndpointFilter, ResultToCreatedResponseTransformer, ResultToResponseTransformer, EndpointFilterDelegate, EndpointFilterInvocationContext, ValueTask, RouteHandlerBuilderExtensions (+8 more)

### Community 63 - "Hangfire"
Cohesion: 0.20
Nodes (8): ChangeTracker, DatabaseFacade, EntityEntry, IDisposable, IDbContext, CancellationToken, DbSet, Task

### Community 64 - "Seeder"
Cohesion: 0.15
Nodes (9): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, CancellationToken, Task, ICaptchaService, Response, CancellationToken, Task, CachedCaptchaService (+1 more)

### Community 65 - "ApplyAuditingInterceptor"
Cohesion: 0.14
Nodes (11): SaveChangesInterceptor, ApplyAuditingInterceptor, CancellationToken, DbContextEventData, InterceptionResult, ValueTask, ApplySearchLanguageInterceptor, CancellationToken (+3 more)

### Community 66 - "CustomRoles"
Cohesion: 0.32
Nodes (5): FrozenDictionary, IReadOnlySet, CustomPermissions, HashSet, IEnumerable

### Community 67 - "AuthenticateResult"
Cohesion: 0.22
Nodes (8): AuthenticateResult, AuthenticationHandler, AuthenticationProperties, AuthenticationSchemeOptions, TestAuthHandler, Guid, string, Task

### Community 68 - "ValueObject"
Cohesion: 0.27
Nodes (4): Common.Domain, IComparable, ValueObject, IEnumerable

### Community 69 - "IResxLocalizer"
Cohesion: 0.28
Nodes (6): Products.Endpoints.ProductTemplates.v1.Create, CancellationToken, Task, Request, RequestValidator, Response

### Community 70 - "OutboxModule"
Cohesion: 0.08
Nodes (26): IntegrationEventHandlerBase, CancellationToken, ConsumeContext, DefaultIdType, ILogger, LoggerMessage, Task, TimeSpan (+18 more)

### Community 71 - "V1ProductCreatedDomainEvent"
Cohesion: 0.15
Nodes (13): Add a new language/culture, Add search to a new entity _(Build checklist)_, Change the vector (weights, fields, or config), Configuration, Extending and maintaining, File map _(Build)_, Full-Text Search, Gotchas (+5 more)

### Community 72 - "IAggregateRoot"
Cohesion: 0.23
Nodes (7): Hub, Exception, ILogger, LoggerMessage, string, Task, NotificationsHub

### Community 73 - "ApiVersionDescription"
Cohesion: 0.17
Nodes (9): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, CancellationToken, IReadOnlyCollection, RouteGroupBuilder, Task, Endpoint, DateTimeOffset, Guid (+1 more)

### Community 75 - "GetSeedUserIdsRequest"
Cohesion: 0.16
Nodes (11): Products.Endpoints.Stores.v1.My.RemoveProduct, Products.Endpoints.Stores.v1.RemoveProduct, ProductId, Request, RequestValidator, Request, RequestValidator, Request (+3 more)

### Community 76 - "HostCollection"
Cohesion: 0.29
Nodes (4): Host.Tests, ICollectionFixture, IntegrationTestCollection, HostCollection

### Community 77 - "Microsoft.AspNetCore.Identity.EntityFrameworkCore"
Cohesion: 0.25
Nodes (6): Microsoft.AspNetCore.Identity.EntityFrameworkCore, Microsoft.NET.Sdk, Bogus, Microsoft.NET.Test.Sdk, xunit.runner.visualstudio, Microsoft.NET.Sdk

### Community 78 - "RequestLoggingPathPostConfigure"
Cohesion: 0.33
Nodes (7): IPostConfigureOptions, RequestLoggingOptions, RequestLoggingOptionsValidator, SensitivePathRule, IList, int, RequestLoggingPathPostConfigure

### Community 79 - "Endpoint"
Cohesion: 0.21
Nodes (6): Common.IntegrationEvents, Notifications.Application.IntegrationEventHandlers, IAM.Application.Users.DomainEventHandlers.v1, Common.Application.EventBus, Setup, IServiceCollection

### Community 80 - "BaseIntegrationTest"
Cohesion: 0.25
Nodes (5): IAsyncLifetime, RabbitMqContainer, IWebHostBuilder, ValueTask, OutboxTestWebAppFactory

### Community 81 - "HttpContextExtensions"
Cohesion: 0.12
Nodes (15): Common.Application.Validation, AuditLogOptions, AuditLogOptionsValidator, DatabaseOptions, DatabaseOptionsValidator, ModulesOptions, ModulesOptionsValidator, IReadOnlyList (+7 more)

### Community 82 - "net10.0"
Cohesion: 0.10
Nodes (18): IEnumerable, IReadOnlyCollection, List, ApplicationUser, ApplicationUserConfig, Task, Seeder, IdentityRole (+10 more)

### Community 83 - "IServiceProvider"
Cohesion: 0.44
Nodes (4): IServiceProvider, MigrationGuard, ILogger, LoggerMessage

### Community 84 - "enabledManagers"
Cohesion: 0.29
Nodes (6): enabledManagers, extends, packageRules, schedule, $schema, timezone

### Community 85 - "Activity"
Cohesion: 0.38
Nodes (4): Activity, ResultTelemetryExtensions, ActivitySource, Task

### Community 86 - "AuditLogRetentionJobRegistrar"
Cohesion: 0.11
Nodes (16): Common.Infrastructure.Persistence.AuditLog, IHostedService, AuditLogRetentionJobRegistrar, CancellationToken, ILogger, LoggerMessage, string, Task (+8 more)

### Community 87 - "AuditLogRetentionService"
Cohesion: 0.08
Nodes (23): Histogram, long, ObservableGauge, CancellationToken, ILogger, LoggerMessage, Task, OutboxMetricsJob (+15 more)

### Community 89 - "ConfigurationManager"
Cohesion: 0.22
Nodes (6): ConfigurationManager, Host, Host.Configurations, Setup, Program, WebApplicationBuilder

### Community 90 - "HostTestFactory"
Cohesion: 0.14
Nodes (14): IAM.Endpoints.Tokens.VersionNeutral.Create, accessToken, DateTimeOffset, expiresAt, ICollection, refreshTokenBytes, ITokenService, CancellationToken (+6 more)

### Community 91 - "HttpClient"
Cohesion: 0.22
Nodes (7): Common.Infrastructure.Resiliency, HttpClient, HttpStandardResilienceOptions, IHttpClientBuilder, Setup, Action, IServiceCollection

### Community 92 - "IntegrationTestWebAppFactory"
Cohesion: 0.17
Nodes (11): IInterModuleRequest, GetSeedUserIdsRequest, GetSeedUserIdsResponse, CancellationToken, Task, GetSeedUserIdsRequestHandler, CancellationToken, IResult (+3 more)

### Community 93 - "IOpenApiSchema"
Cohesion: 0.16
Nodes (11): Products.Application.Stores.DomainEventHandlers.v1, DomainEventHandlerBase, IEventHandler, CancellationToken, Task, CancellationToken, Task, SimulateSomeBusinessHandler (+3 more)

### Community 94 - "ISmsService"
Cohesion: 0.21
Nodes (9): PartitionedRateLimiter, CancellationToken, Func, HttpContext, IConfiguration, IServiceCollection, OnRejectedContext, ValueTask (+1 more)

### Community 95 - "CachingEntryDefaults"
Cohesion: 0.27
Nodes (5): DateTimeOffset, RefreshToken, RefreshTokenId, EntityTypeBuilder, RefreshTokenConfig

### Community 96 - "OutboxCleanupSettings"
Cohesion: 0.08
Nodes (19): Outbox.Persistence, Common.Application.Persistence.Outbox, IOutboxMessage, DateTimeOffset, OutboxMessage, DateTimeOffset, TimeSpan, IOutboxDbContext (+11 more)

### Community 97 - "OutboxTestWebAppFactory"
Cohesion: 0.14
Nodes (12): IdentityDbContext, CancellationToken, DbSet, IdentityRole, IdentityRoleClaim, IdentityUserClaim, IdentityUserLogin, IdentityUserRole (+4 more)

### Community 98 - "IDatabaseSeeder"
Cohesion: 0.29
Nodes (6): PaginationQueryableExtensions, CancellationToken, Expression, Func, IQueryable, Task

### Community 99 - "HangfireCustomAuthorizationFilter"
Cohesion: 0.29
Nodes (3): CustomPermission, RouteHandlerBuilderExtensions, RouteHandlerBuilder

### Community 100 - "IdentityResultExtensions"
Cohesion: 0.12
Nodes (21): DomainEvent, DateTimeOffset, DefaultIdType, V1AllSessionsRevokedDomainEvent, V1RefreshTokenRevokedDomainEvent, V1RefreshTokenUpdatedDomainEvent, V1SessionCreatedDomainEvent, V1SessionRefreshedDomainEvent (+13 more)

### Community 101 - "ProblemDetailsExtensions"
Cohesion: 0.08
Nodes (23): 1. Functional Pipeline — the Golden Path, 2. Persistence Rules, 3. REPR Pattern (Endpoints), 4. C# 14 Standards, 5. Cross-Module Communication, 6. Observability (OpenTelemetry), 7. Zero Trust / Defensive Implementation, 8. Testing Standards (+15 more)

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
Cohesion: 0.36
Nodes (4): CollectionExtensions, Func, ICollection, IEnumerable

### Community 106 - "OutboxCleanupJob"
Cohesion: 0.18
Nodes (9): Products.Endpoints.Stores.v1.Search, CancellationToken, IOptions, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator (+1 more)

### Community 107 - "ProductsModule"
Cohesion: 0.15
Nodes (8): CancellationToken, RouteGroupBuilder, Task, Endpoint, CancellationToken, List, Task, Seeder

### Community 108 - "Setup"
Cohesion: 0.25
Nodes (8): API Documentation, Contributing, Features, Introduction, License, Modular Monolith, DDD, Vertical Slice Architecture WebAPI Boilerplate, Requirements, Table of Contents

### Community 109 - "RequestBody"
Cohesion: 0.20
Nodes (8): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator, Response

### Community 110 - "JobTargets"
Cohesion: 0.18
Nodes (9): Products.Endpoints.Products.v1.My.Search, CancellationToken, IOptions, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator (+1 more)

### Community 111 - "CacheKeys"
Cohesion: 0.26
Nodes (7): IBackgroundJobs, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 112 - "StringExtensions"
Cohesion: 0.14
Nodes (10): Common.Infrastructure.Modules, Common.Application.Options, Host.Middlewares, Host.Infrastructure, BackgroundJobsOptions, BackgroundJobsOptionsValidator, HealthCheckOptions, HealthCheckOptionsValidator (+2 more)

### Community 115 - "SearchLanguageResolver"
Cohesion: 0.26
Nodes (7): BackgroundJobsService, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 116 - "IRoleService"
Cohesion: 0.15
Nodes (10): IHostBuilder, KeyValuePair, LoggerConfiguration, LoggerMinimumLevelConfiguration, ObservabilityOptions, ObservabilityOptionsValidator, Dictionary, IEnumerable (+2 more)

### Community 117 - "DummySmsGateway"
Cohesion: 0.33
Nodes (6): Architecture Decision Record, Design Goals, Honest Opinion, Related documents, The Core Problem, What "split deployment" actually means

### Community 118 - "RequestBody"
Cohesion: 0.15
Nodes (14): OpenTelemetryBuilder, ResourceBuilder, IModule, Action, IApplicationBuilder, IEndpointRouteBuilder, IEnumerable, RateLimiterOptions (+6 more)

### Community 119 - "RequestBody"
Cohesion: 0.18
Nodes (8): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, ProductsModule

### Community 120 - "StronglyTypedIdHelper"
Cohesion: 0.20
Nodes (8): Products.Endpoints.Stores.v1.My.Create, CancellationToken, RouteGroupBuilder, Task, Endpoint, Request, RequestValidator, Response

### Community 121 - "Setup"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 122 - "Setup"
Cohesion: 0.25
Nodes (4): Common.Application.Caching, CacheKeys, For, OtpCacheEntry

### Community 123 - "Setup"
Cohesion: 0.33
Nodes (4): Common.Infrastructure.Auth.Services, Common.Infrastructure.Auth, Setup, IServiceCollection

### Community 124 - "Setup"
Cohesion: 0.20
Nodes (8): IApplicationBuilder, IConfiguration, ILogger, IServiceCollection, LoggerMessage, string, WebApplication, Setup

### Community 125 - "Endpoint"
Cohesion: 0.20
Nodes (7): Products.Infrastructure.RateLimiting, Action, IEnumerable, RateLimiterOptions, Policies, string, RateLimitingConstants

### Community 126 - "Setup"
Cohesion: 0.29
Nodes (7): Exception, HttpContext, ILogger, LoggerMessage, RequestDelegate, Task, GlobalExceptionHandlingMiddleware

### Community 127 - "Setup"
Cohesion: 0.20
Nodes (7): Notifications.Application.Sms, CancellationToken, Task, ISmsGateway, CancellationToken, Task, DummySmsGateway

### Community 128 - "Endpoint"
Cohesion: 0.18
Nodes (6): IAM.Infrastructure.Persistence.EntityConfigurations, Common.Infrastructure.Persistence.EntityConfigurations, Common.Infrastructure.Persistence.ValueConverters, Products.Infrastructure.Persistence.EntityConfigurations, int, Constants

### Community 129 - "Setup"
Cohesion: 0.18
Nodes (7): Products.Endpoints.Products, RouteGroupBuilder, Setup, CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 130 - "Setup"
Cohesion: 0.16
Nodes (17): Configuration-Driven Module Loading, IntegrationEvents (Async Cross-Module), IAM Module, Notifications Module, Products Module, Observability (OpenTelemetry), docker-compose.yml (Base Stack), docker-compose.app.yml (App-Only) (+9 more)

### Community 131 - "Setup"
Cohesion: 0.20
Nodes (10): 1. Configuration-driven module loading, 2. Two module tiers: `IModule` and `ICoreModule`, 3. `IInterModuleRequestClient<TRequest, TResponse>` as the only synchronous cross-module communication path, 4. IntegrationEvents, 5. Transactional Outbox, 6. Each module owns its own `DbContext`, 7. DDD on writes, VSA on reads, 8. Compiler-enforced module boundaries (+2 more)

### Community 132 - "Setup"
Cohesion: 0.11
Nodes (18): BackgroundService, DatabaseSeederOrchestrator, CancellationToken, ILogger, LoggerMessage, Task, SeedingCompletionTracker, CancellationToken (+10 more)

### Community 134 - "Setup"
Cohesion: 0.20
Nodes (9): DbContext, AuditLogEntry, DefaultIdType, BaseDbContext, CancellationToken, DbSet, Task, AuditLogEntryConfiguration (+1 more)

### Community 135 - "AuditLogOptions"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 136 - "BackgroundJobsOptions"
Cohesion: 0.33
Nodes (6): PersistenceQueryableExtensions, CancellationToken, Expression, Func, IQueryable, Task

### Community 137 - "CaptchaOptions"
Cohesion: 0.27
Nodes (6): PolymorphicEventConverter, JsonSerializerOptions, string, Type, Utf8JsonReader, Utf8JsonWriter

### Community 138 - "CorsOptions"
Cohesion: 0.18
Nodes (8): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, IamModule

### Community 139 - "DatabaseOptions"
Cohesion: 0.12
Nodes (12): BackgroundJobs.Telemetry, ICoreModule, BackgroundJobsModule, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection (+4 more)

### Community 140 - "HealthCheckOptions"
Cohesion: 0.31
Nodes (6): StronglyTypedIdListReadOnlyJsonConverter, IReadOnlyList, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter

### Community 141 - "JwtOptions"
Cohesion: 0.29
Nodes (4): Notifications.Application.Otp, Notifications.Infrastructure.InterModuleRequestHandlers, IServiceCollection, Setup

### Community 142 - "ModulesOptions"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 143 - "OpenApiOptions"
Cohesion: 0.40
Nodes (4): Action, Expression, Func, Task

### Community 144 - "OtpOptions"
Cohesion: 0.20
Nodes (7): IAM.Endpoints.Users.VersionNeutral.Get, CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, Response

### Community 145 - "ResxLocalizationOptions"
Cohesion: 0.20
Nodes (8): Hangfire.PostgreSql, Newtonsoft.Json, Hangfire, Microsoft.NET.Sdk, Microsoft.NET.Test.Sdk, NSubstitute, xunit.runner.visualstudio, Microsoft.NET.Sdk

### Community 146 - "SecurityHeadersOptions"
Cohesion: 0.23
Nodes (5): Products.Infrastructure.Persistence.Seeding, Products.Endpoints.Probe.v1, Common.InterModuleRequests.IAM, Common.InterModuleRequests.Contracts, IAM.Infrastructure.InterModuleRequestHandlers

### Community 147 - "SignalROptions"
Cohesion: 0.12
Nodes (11): IInterModuleRequestClient, CancellationToken, Task, MassTransitInterModuleRequestClient, CancellationToken, Task, CancellationToken, IFeatureManager (+3 more)

### Community 148 - "Setup"
Cohesion: 0.33
Nodes (6): Assembly, IApplicationBuilder, IConfiguration, IServiceCollection, IWebHostEnvironment, Setup

### Community 149 - "AutoMigrateMarker"
Cohesion: 0.15
Nodes (11): IFusionCache, JwtOptions, JwtOptionsValidator, IReadOnlyCollection, CancellationToken, HttpContext, IOptions, RouteGroupBuilder (+3 more)

### Community 150 - "Setup"
Cohesion: 0.32
Nodes (6): accessToken, DateTimeOffset, expiresAt, ICollection, refreshTokenBytes, TokenService

### Community 151 - "Setup"
Cohesion: 0.25
Nodes (4): Notifications.Infrastructure.Otp, string, DummyOtpService, OtpService

### Community 152 - "Setup"
Cohesion: 0.25
Nodes (6): AuditableEntityConfiguration, EntityTypeBuilder, EntityTypeBuilder, ProductConfiguration, EntityTypeBuilder, StoreConfiguration

### Community 153 - "Endpoint"
Cohesion: 0.06
Nodes (26): ApiVersionDescription, Host.Swagger, IConfigureOptions, IOpenApiSchema, IOperationFilter, ISchemaFilter, JsonValue, OpenApiInfo (+18 more)

### Community 154 - "Endpoint"
Cohesion: 0.25
Nodes (5): string, Task, TimeSpan, ValueTask, HostTestFactory

### Community 155 - "Endpoint"
Cohesion: 0.39
Nodes (4): CancellationToken, IReadOnlyList, Task, SignalRNotificationDispatcher

### Community 156 - "CurrentUser"
Cohesion: 0.10
Nodes (17): Common.Application.ModelBinders, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, Products.Endpoints.ProductTemplates.v1.Activate, IModelBinder, ModelBindingContext, StronglyTypedIdBinder, Task, Request (+9 more)

### Community 157 - "Setup"
Cohesion: 0.33
Nodes (6): EventDispatcher, ActivitySource, CancellationToken, ILogger, LoggerMessage, Task

### Community 158 - "Endpoint"
Cohesion: 0.31
Nodes (5): CancellationToken, Task, TimeSpan, IOtpService, OtpVerificationOutcome

### Community 159 - "Setup"
Cohesion: 0.22
Nodes (6): IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, NotificationsModule

### Community 160 - "Setup"
Cohesion: 0.39
Nodes (3): Outbox, Common.Infrastructure.Persistence.Outbox, Outbox.Telemetry

### Community 161 - "Endpoint"
Cohesion: 0.32
Nodes (5): CancellationToken, int, Task, TimeSpan, OtpServiceBase

### Community 162 - "Setup"
Cohesion: 0.25
Nodes (3): Outbox.Tests, Common.Tests, ErrorExtensions

### Community 163 - "Endpoint"
Cohesion: 0.39
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, OutboxCleanupJob

### Community 164 - "Endpoint"
Cohesion: 0.50
Nodes (3): HttpContext, Task, SecurityHeadersMiddleware

### Community 165 - "Endpoint"
Cohesion: 0.13
Nodes (12): Common.Application.JsonConverters, Products.Endpoints.Stores.v1.AddProduct, decimal, int, Constants, RequestBody, Request, RequestBody (+4 more)

### Community 166 - "Setup"
Cohesion: 0.25
Nodes (5): IAM.Endpoints.Tokens.VersionNeutral, RouteGroupBuilder, Endpoint, RouteGroupBuilder, Setup

### Community 167 - "Endpoint"
Cohesion: 0.25
Nodes (8): Development, Getting Started, One-time setup, Running services, Split Deployment (Microservice Mode), Unix only, Visual Studio, VSCode

### Community 168 - "Endpoint"
Cohesion: 0.25
Nodes (5): Products.Endpoints.ProductTemplates, RouteGroupBuilder, Setup, RouteGroupBuilder, Endpoint

### Community 169 - "Endpoint"
Cohesion: 0.29
Nodes (6): ICurrentUser, Guid, ICollection, CurrentUser, Guid, ICollection

### Community 170 - "Endpoint"
Cohesion: 0.29
Nodes (4): IAM.Tests, IntegrationTestCollection, IWebHostBuilder, IntegrationTestWebAppFactory

### Community 171 - "Endpoint"
Cohesion: 0.29
Nodes (4): Common.InterModuleRequests, IAssemblyReference, Setup, IServiceCollection

### Community 172 - "Endpoint"
Cohesion: 0.33
Nodes (4): DashboardContext, IDashboardAsyncAuthorizationFilter, HangfireCustomAuthorizationFilter, Task

### Community 173 - "Endpoint"
Cohesion: 0.40
Nodes (4): IAM.Endpoints.Users.VersionNeutral.SelfRegister, Guid, Request, RequestValidator

### Community 174 - "Endpoint"
Cohesion: 0.29
Nodes (5): IAM.Application.Tokens.DTOs, DateTimeOffset, AccessTokenDto, DateTimeOffset, TokensDto

### Community 175 - "Endpoint"
Cohesion: 0.11
Nodes (18): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Stores.v1.My.AuditLog, Products.Endpoints.Products.v1.AuditLog, PaginationRequest, PaginationRequestValidator, int, int, Constants (+10 more)

### Community 176 - "Endpoint"
Cohesion: 0.29
Nodes (7): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Key decisions

### Community 177 - "Endpoint"
Cohesion: 0.33
Nodes (5): IServiceScope, Respawner, BaseIntegrationTest, JsonSerializerOptions, ValueTask

### Community 178 - "Endpoint"
Cohesion: 0.43
Nodes (6): Checkout, FeatureFlags, IAM, Notifications, Products, string

### Community 179 - "Endpoint"
Cohesion: 0.52
Nodes (6): CachingEntryDefaults, CachingOptions, CachingOptionsValidator, Redis, RedisValidator, TimeSpan

### Community 181 - "Endpoint"
Cohesion: 0.50
Nodes (3): IConfiguration, IServiceCollection, Setup

### Community 182 - "Endpoint"
Cohesion: 0.33
Nodes (4): IAM.Domain, string, Constants, IAssemblyReference

### Community 183 - "Endpoint"
Cohesion: 0.40
Nodes (3): Products.Tests, IntegrationTestCollection, IntegrationTestWebAppFactory

### Community 184 - "Endpoint"
Cohesion: 0.33
Nodes (3): BackgroundJobs.Tests, JobTargets, Task

### Community 185 - "Endpoint"
Cohesion: 0.40
Nodes (5): Products.Endpoints.Stores.v1.Update, RequestBody, Request, RequestBody, RequestValidator

### Community 186 - "Endpoint"
Cohesion: 0.67
Nodes (3): ResxLocalizationOptions, ResxLocalizationOptionsValidator, ICollection

### Community 187 - "Endpoint"
Cohesion: 0.33
Nodes (4): Common.Infrastructure.Caching, Setup, IConfiguration, IServiceCollection

### Community 188 - "Endpoint"
Cohesion: 0.33
Nodes (4): IAuthorizationHandler, AllowAllAuthorizationHandler, AuthorizationHandlerContext, Task

### Community 190 - "Setup"
Cohesion: 0.33
Nodes (6): Add a new migration, Build & Test, Database Migrations, Developer Commands, Generate idempotent SQL scripts (for DBA review), Migration Workflow Summary

### Community 191 - "Setup"
Cohesion: 0.29
Nodes (7): Concurrent safety, Cross-process call path, Files added by this PoC, How it works, How to run, Split-Deployment PoC, What this proves

### Community 192 - "NameFor"
Cohesion: 0.33
Nodes (5): FeatureFlagResultExtensions, Action, Func, IFeatureManager, Task

### Community 193 - "RequestValidator"
Cohesion: 0.33
Nodes (6): ActivitySource, Counter, Meter, string, NotificationsTelemetry, UpDownCounter

### Community 194 - "Sync AI Settings Command"
Cohesion: 0.40
Nodes (3): IApplicationBuilder, IServiceCollection, Setup

### Community 195 - "IAssemblyReference"
Cohesion: 0.67
Nodes (3): Products.Endpoints.ProductTemplates.v1.Deactivate, Request, RequestValidator

### Community 196 - "IAssemblyReference"
Cohesion: 0.33
Nodes (5): Configurations — deploy-time materialized config, Deploying to a real environment (devops responsibility), How config is loaded, Local development (docker-compose), Why this pattern

### Community 197 - "IInterModuleRequest"
Cohesion: 0.40
Nodes (3): IApplicationBuilder, IServiceCollection, Setup

### Community 199 - "pre-commit-guard.sh"
Cohesion: 0.33
Nodes (5): ActivitySource, Counter, Meter, string, IamTelemetry

### Community 200 - "sync-reminder.sh"
Cohesion: 0.40
Nodes (5): Products.Endpoints.Products.v1.Update, RequestBody, Request, RequestBody, RequestValidator

### Community 201 - "IAssemblyReference"
Cohesion: 0.67
Nodes (3): OtpOptions, OtpOptionsValidator, Dictionary

### Community 202 - "IAssemblyReference"
Cohesion: 0.13
Nodes (13): FullTextSearchOptions, FullTextSearchOptionsValidator, Dictionary, IReadOnlyList, string, ISearchLanguageResolver, SearchLanguageResolver, string (+5 more)

### Community 203 - "IAssemblyReference"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 204 - "IAssemblyReference"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 205 - "Request.cs"
Cohesion: 0.40
Nodes (4): Assembly, IConfiguration, IServiceCollection, Setup

### Community 206 - "IAssemblyReference"
Cohesion: 0.16
Nodes (10): DbSet, IProductsDbContext, IReadOnlyList, List, ProductTemplate, EntityTypeBuilder, ProductTemplateConfiguration, DbSet (+2 more)

### Community 207 - "IAssemblyReference"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 208 - "IAssemblyReference"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response

### Community 209 - "RabbitMqOptions"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 210 - "IAssemblyReference"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 211 - "IAssemblyReference"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 212 - "IAssemblyReference"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 213 - "IAssemblyReference"
Cohesion: 0.33
Nodes (5): ActivitySource, Counter, Meter, string, ProductsTelemetry

### Community 214 - "Request.cs"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 215 - ".DeactivateProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 217 - ".UpdateMyStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 218 - ".RemoveProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 219 - "Setup.cs"
Cohesion: 0.20
Nodes (9): IIntegrationEventOutbox, CancellationToken, Task, V1SessionRevokedDomainEventHandler, CancellationToken, Task, V1UserRegisteredDomainEventHandler, V1SessionRevokedDomainEvent (+1 more)

### Community 222 - "RequestValidator"
Cohesion: 0.40
Nodes (5): How it works, One-time setup _(Build — in a migration)_, Query path, Ranking, Write path

### Community 223 - "Setup.cs"
Cohesion: 0.40
Nodes (3): IAM.Endpoints.Users.VersionNeutral, RouteGroupBuilder, Setup

### Community 224 - "RequestValidator"
Cohesion: 0.29
Nodes (3): Common.Domain.Extensions, SearchValues, StringExtensions

### Community 226 - "OtpCacheEntry"
Cohesion: 0.09
Nodes (17): CancellationToken, Task, IEventHandlerWrapper, CancellationToken, Task, IEvent, DateTimeOffset, DefaultIdType (+9 more)

### Community 228 - "IAM"
Cohesion: 0.50
Nodes (3): ReverseProxyOptions, ReverseProxyOptionsValidator, IReadOnlyList

### Community 229 - "IntegrationEvent"
Cohesion: 0.40
Nodes (3): Setup, IConfiguration, IServiceCollection

### Community 230 - "Request.cs"
Cohesion: 0.67
Nodes (3): Products.Endpoints.Stores.v1.My.Update, Request, RequestValidator

### Community 231 - "AccessTokenDto"
Cohesion: 0.29
Nodes (4): Common.Infrastructure.Localization, Setup, IApplicationBuilder, IServiceCollection

### Community 232 - "Request.cs"
Cohesion: 0.40
Nodes (3): IApplicationBuilder, IServiceCollection, Setup

### Community 236 - "GetSeedUserIds"
Cohesion: 0.40
Nodes (4): Action, Expression, Func, Task

### Community 238 - "SendPhoneOtp"
Cohesion: 0.40
Nodes (3): IApplicationBuilder, IServiceCollection, Setup

### Community 239 - "VerifyPhoneOtp"
Cohesion: 0.67
Nodes (3): CorsOptions, CorsOptionsValidator, IReadOnlyList

### Community 243 - "Response"
Cohesion: 0.67
Nodes (3): CustomActions, CustomResources, string

### Community 244 - "Response"
Cohesion: 0.50
Nodes (3): CustomRoles, HashSet, string

### Community 246 - ".CreateStorePolicy"
Cohesion: 0.50
Nodes (3): IConfiguration, IServiceCollection, Setup

### Community 249 - "Response"
Cohesion: 0.50
Nodes (3): IConfiguration, IServiceCollection, Setup

## Knowledge Gaps
- **366 isolated node(s):** `check-csproj-refs.sh script`, `pre-commit-guard.sh script`, `docker-compose`, `TURKISH_NAMES`, `JSON_HEADERS` (+361 more)
  These have ≤1 connection - possible missing edges or undocumented components.
- **101 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `StringExtensions` to `Endpoint`, `Products Store & Audit Services`, `REPR Request Validators`, `DatabaseOptions`, `BackgroundJobs Service (Hangfire)`, `JwtOptions`, `Functional Result Extensions`, `Launch Settings`, `Module Installers (IModule)`, `Authz Constants & Feature Flags`, `AutoMigrateMarker`, `SignalR Hub & Exception Middleware`, `Setup`, `Endpoint`, `Integration Event Handler Base`, `Setup`, `StoreConfiguration`, `Endpoint`, `ApiVersionSet`, `Endpoint`, `Endpoint`, `Endpoint`, `IAssemblyReference`, `IAssemblyReference`, `RequestLoggingPathPostConfigure`, `Endpoint`, `HttpContextExtensions`, `AuditLogRetentionJobRegistrar`, `ConfigurationManager`, `IAM.Endpoints.Otp.VersionNeutral`, `IAM`, `IntegrationEvent`, `AccessTokenDto`, `VerifyPhoneOtp`, `IRoleService`, `Endpoint`?**
  _High betweenness centrality (0.288) - this node is a cross-community bridge._
- **Why does `Common.Domain.StronglyTypedIds` connect `Functional Result Extensions` to `Host Logging & Serilog Setup`, `IAM User Identity & Auditing`, `Endpoint`, `Products Store & Audit Services`, `BackgroundJobs Service (Hangfire)`, `HealthCheckOptions`, `OtpOptions`, `Module Installers (IModule)`, `SecurityHeadersOptions`, `Endpoint`, `Integration Event Handler Base`, `Endpoint`, `CurrentUser`, `PermissionAuthorizationHandler`, `CustomRateLimitingOptions`, `Endpoint`, `Setup`, `Endpoint`, `DummyOtpService`, `OutboxModule`, `Endpoint`, `OutboxCleanupSettings`, `OutboxCleanupJob`?**
  _High betweenness centrality (0.069) - this node is a cross-community bridge._
- **Why does `Common.Application.Auth` connect `BackgroundJobs Service (Hangfire)` to `CustomRoles`, `HangfireCustomAuthorizationFilter`, `Setup`, `Products Store & Audit Services`, `HttpContextTargetingContextAccessor`, `IMiddleware`, `Endpoint`, `Functional Result Extensions`, `Launch Settings`, `Module Installers (IModule)`, `SecurityHeadersOptions`, `Response`, `Response`, `Setup`, `Host.Swagger`, `PermissionAuthorizationHandler`?**
  _High betweenness centrality (0.064) - this node is a cross-community bridge._
- **What connects `check-csproj-refs.sh script`, `pre-commit-guard.sh script`, `docker-compose` to the rest of the system?**
  _379 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Host Logging & Serilog Setup` be split into smaller, more focused modules?**
  _Cohesion score 0.1380952380952381 - nodes in this community are weakly interconnected._
- **Should `IAM User Identity & Auditing` be split into smaller, more focused modules?**
  _Cohesion score 0.11612903225806452 - nodes in this community are weakly interconnected._
- **Should `Products Store & Audit Services` be split into smaller, more focused modules?**
  _Cohesion score 0.11264367816091954 - nodes in this community are weakly interconnected._