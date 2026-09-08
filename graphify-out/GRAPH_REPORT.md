# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-09-08)

## Corpus Check
- 462 files · ~71,151 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 3739 nodes · 6563 edges · 347 communities (241 shown, 102 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 216 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `df0dea92`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- Common.InterModuleRequests.Contracts
- NotificationPayload
- OutboxProcessor
- DeviceRegistryReconciliationService
- Hybrid DDD (Writes) / VSA (Reads)
- KeycloakAdminClient
- RedisFixedWindowRateLimiter
- ApplyAuditingInterceptor
- FirebasePushGateway
- Request
- Error
- Common.Domain.ResultMonad
- VerifyPhoneOtpResponse
- .SendOtp
- UserRepresentation
- KeycloakPermissionAuthorizationHandler
- ISmsGateway
- HangfireCustomAuthorizationFilter
- DomainEvent
- AuditLogEntry
- IKeycloakAdminClient
- BoundedRequestCaptureStream
- DeviceRegistration
- KeycloakPermissionPolicyProvider
- DeactivateDeviceSessionsRequest
- NetGsmSmsGateway
- IEvent
- RequestResponseBodyLoggingMiddleware
- KeycloakTokenClient
- Result
- Product
- ApplicationUserId
- .CreateStoreAsync
- .AddProductAsync
- KeycloakPermissionClient
- OutboxMessage
- CustomRateLimitingOptions
- KeycloakTokenClient.cs
- .RefreshToken
- ObservabilityOptions
- SmsRateLimitingPolicy
- Products.Domain.Stores
- .SaveWithOutboxAsync
- CreateStoreRateLimitingPolicy
- KeycloakOptions
- .CreateTokens
- ProductsModule.cs
- Outbox Misuse Check
- IOtpService
- Add Integration Event Command
- Response
- Products.Domain.Products
- ReCaptchaService
- Products.Domain.ProductTemplates
- NotificationsModule.cs
- RegisterRateLimitingPolicy
- DummySmsGateway
- Cross-Module Reference Violation
- OutboxMetricsJob
- .SearchProductTemplatesAsync
- Bogus Test Data
- .AddProductToMyStoreAsync
- NotificationsDbContext
- RequestLoggingOptions
- Common.InterModuleRequests
- Common.Application.Options
- OutboxModule
- AuditableEntityResponse
- ValueObject
- V1StoreCreatedDomainEvent
- IStronglyTypedId
- Full-Text Search
- Response
- .WriteAsync
- AsNoTracking Coverage Check
- ProductId
- Common.Application.ModelBinders
- IProductsDbContext
- CheckRegistrationRateLimitingPolicy
- IntegrationEventOutbox
- EventDispatcher
- .SearchStoresAsync
- StringExtensions
- .EnsureNoMigrationsPending
- .Configure
- ResultTelemetryExtensions
- Store
- PushOptions
- Configuration-Driven Module Registration
- Setup
- IInterModuleRequestClient
- .AddCaptchaInfrastructure
- Common.Domain.StronglyTypedIds
- Endpoint
- FirebasePushGateway.cs
- .SearchMyProductsAsync
- DatabaseSeederOrchestrator
- AuditLogRetentionService
- .SearchStoreProductsAsync
- .RequireScope
- OutboxDbContext
- ResiliencyOptions
- HttpContextTargetingContextAccessor
- BackgroundJobsTelemetry
- IntegrationEvent
- .AddKeycloakInfrastructure
- OutboxModule.cs
- .RequestTokensAsync
- .TryDeserialize
- .IsRegisteredAsync
- ProductsDbContext
- BackgroundJobsService
- OutboxCleanupJob
- CaptchaOptions
- Response
- IBackgroundJobs
- .WriteProblemAsync
- Common.Domain.Devices
- IModule
- ProductsModule
- AggregateRoot
- .GetVariantAsync
- TokenRefreshRateLimitingPolicy
- FullTextSearchOptions
- HealthCheckOptions
- .TapWhenFeatureEnabledAsync
- GlobalExceptionHandlingMiddleware
- BaseDbContext
- AuditLogEntryConfiguration
- Response
- docker-compose.yml (Base Stack)
- .AddInfrastructure
- .SaveChangesAsync
- Consumer Idempotency (IntegrationEventHandlerBase)
- v1/RemoveProduct/Request.cs
- .ListSessions
- PersistenceQueryableExtensions
- .WriteTooManyRequestsToResponse
- .InvokeAsync
- ProductTemplate
- .RegisterAsync
- .GetMeAsync
- Response
- IDbContext
- Response
- .ExecuteAsync
- IAM.Endpoints.Otp.VersionNeutral
- RecurringBackgroundJobsService
- Setup
- Response
- FeatureFlags
- OutboxOptions
- ISearchLanguageResolver
- SwaggerDefaultValues
- Request
- InterModuleRequestHandler
- PaginationRequest
- IAuditableEntity
- .FixedWindow
- .Capture
- OtpServiceBase
- .UpdateMyStoreAsync
- UtcDateTimeOffsetConverter
- For
- Request
- ICurrentUser
- Response
- .PaginateAsync
- ICaptchaService
- Policies.CreateStore.cs
- Notifications.Application/IAssemblyReference.cs
- .UpdateStoreAsync
- ResxLocalizationOptions
- Request
- IInterModuleRequest
- PaginationResponse
- SendSecurityAlertRequestHandler
- .AddAuthInfrastructure
- OtpOptions
- IntegrationEventHandlerBase
- SmsOptions
- RabbitMqOptions
- IAM.Domain
- SendRequestBody
- SignalROptions
- IamModule
- Request
- ReverseProxyOptions
- BoundedCaptureStream
- .GetAccessTokenAsync
- Split-Deployment PoC
- ServiceAccountTokenCache
- Request
- CorsOptions
- .BindDeviceAsync
- TokenEndpointRepresentations.cs
- .AddPushServices
- Common.Application.BackgroundJobs
- OpenApiOptions
- KeyValuePair
- KeycloakScopes
- Request
- .DeactivateProductTemplateAsync
- Configuration-Driven Module Loading
- Infrastructure/StringExtensions.cs
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- ConfigureSwaggerOptions
- .CreateProductTemplateAsync
- IamTelemetry
- ProductsTelemetry
- Notifications.Infrastructure.Otp
- RedisOtpService
- ProductTemplateId
- Request
- Response
- PushMessage
- .SeedProductAsync
- Keycloak realm as code
- Endpoint
- ReCaptchaResponse
- .SingleAsResult
- BackgroundJobsOptions
- .HandleAsync
- BackgroundJobsModule
- CustomValidator
- .AddCommonOptions
- OutboxMessageConfig
- Request
- FirebaseServiceAccountOptions
- IKeycloakTokenClient
- Setup
- KeycloakUser
- .UpdateCurrentPushToken
- .AddPersistence
- .AddNetGsm
- .TryReadFromJsonAsync
- TokenResponseRepresentation
- IAM.Endpoints.Common.Validations
- Host.Swagger
- .RemoveProductAsync
- .LogRoleAssignmentFailed
- RemoveDefaultResponseSchemaFilter
- StoreId
- .AddCommonPersistence
- .UpdateMyProductAsync
- Request
- Request
- VersionNeutral/Get/Request.cs
- .RemoveMyProductAsync
- Products.Domain/IAssemblyReference.cs
- IAM.Application/IAssemblyReference.cs
- Products.Infrastructure/IAssemblyReference.cs
- Products.Application/IAssemblyReference.cs
- Common.Infrastructure/IAssemblyReference.cs
- Notifications.Domain/IAssemblyReference.cs
- IAM.Infrastructure/IAssemblyReference.cs
- OtpVerificationOutcome
- OtpService
- Request
- JwtClaimNames.cs
- KeycloakRoles.cs
- Common.Application.Validation
- SecurityHeadersOptions
- .CreateAsync
- HttpContextExtensions.cs
- DefaultResponsesOperationFilter
- .AddCustomSwagger
- SendResponseBody
- IAM.Endpoints
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

## God Nodes (most connected - your core abstractions)
1. `Common.Application.Options` - 101 edges
2. `Result` - 97 edges
3. `Common.Domain.ResultMonad` - 78 edges
4. `ApplicationUserId` - 69 edges
5. `CustomValidator` - 64 edges
6. `Common.Application.Auth` - 59 edges
7. `Common.Application.Validation` - 58 edges
8. `Common.Domain.StronglyTypedIds` - 57 edges
9. `Common.Application.Extensions` - 52 edges
10. `Setup` - 50 edges

## Surprising Connections (you probably didn't know these)
- `Aspire Dashboard Service (mm.aspire-dashboard)` --conceptually_related_to--> `Observability (OpenTelemetry)`  [INFERRED]
  docker-compose.yml → CLAUDE.md
- `OtpErrors` --references--> `Error`  [EXTRACTED]
  src/Modules/IAM/IAM.Domain/Errors/OtpErrors.cs → src/Common/Common.Domain/ResultMonad/Error.cs
- `TokenErrors` --references--> `Error`  [EXTRACTED]
  src/Modules/IAM/IAM.Domain/Errors/TokenErrors.cs → src/Common/Common.Domain/ResultMonad/Error.cs
- `PushErrors` --references--> `Error`  [EXTRACTED]
  src/Modules/Notifications/Notifications.Application/Push/PushErrors.cs → src/Common/Common.Domain/ResultMonad/Error.cs
- `AuditLogDto` --references--> `ApplicationUserId`  [EXTRACTED]
  src/Common/Common.Application/AuditLog/AuditLogDto.cs → src/Common/Common.Domain/StronglyTypedIds/ApplicationUserId.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Local Infrastructure Stack** — docker_compose_postgres, docker_compose_rabbitmq, docker_compose_redis, docker_compose_aspire_dashboard [EXTRACTED 1.00]

## Communities (347 total, 102 thin omitted)

### Community 0 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.10
Nodes (17): IAM.Endpoints.Otp.VersionNeutral.SendForLogin, Common.Application.FeatureManagement, IAM.Endpoints.Otp, Notifications.Application.Persistence, Notifications.Infrastructure.Telemetry, IAM.Domain.Captcha, Common.InterModuleRequests.Contracts, IAM.Infrastructure.Captcha.Services (+9 more)

### Community 1 - "NotificationPayload"
Cohesion: 0.05
Nodes (40): Notifications.Infrastructure.Hubs, Notifications.Application.Hubs, Hub, HubConnectionContext, IHubContext, IUserIdProvider, RedisOptions, CancellationToken (+32 more)

### Community 2 - "OutboxProcessor"
Cohesion: 0.22
Nodes (11): IPublishEndpoint, CancellationToken, Exception, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task (+3 more)

### Community 3 - "DeviceRegistryReconciliationService"
Cohesion: 0.08
Nodes (29): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator, IReadOnlyCollection, GetActiveSessionIdsRequest, GetActiveSessionIdsResponse (+21 more)

### Community 5 - "KeycloakAdminClient"
Cohesion: 0.27
Nodes (12): HttpRequestMessage, CancellationToken, Func, HttpClient, HttpResponseMessage, IFusionCache, IOptions, IReadOnlyList (+4 more)

### Community 6 - "RedisFixedWindowRateLimiter"
Cohesion: 0.13
Nodes (16): RateLimiter, RateLimiterStatistics, RateLimitLease, FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration (+8 more)

### Community 7 - "ApplyAuditingInterceptor"
Cohesion: 0.22
Nodes (8): SaveChangesInterceptor, ApplyAuditingInterceptor, TimeProvider, ApplySearchLanguageInterceptor, Setup, IServiceCollection, IServiceCollection, NpgsqlDataSource

### Community 8 - "FirebasePushGateway"
Cohesion: 0.17
Nodes (11): FirebaseApp, FirebaseMessaging, CancellationToken, Exception, IEnumerable, ILogger, IReadOnlyList, LoggerMessage (+3 more)

### Community 9 - "Request"
Cohesion: 0.15
Nodes (14): Products.Endpoints.Products.v1.My.Update, RequestBody, Request, Body, Id, RequestBody, Description, Name (+6 more)

### Community 10 - "Error"
Cohesion: 0.08
Nodes (16): StringLocalizerExtensions, IStringLocalizer, Error, Key, ParameterName, StatusCode, SubErrors, Value (+8 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.13
Nodes (15): Common.Application.Search, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Common.Infrastructure.Extensions, Products.Infrastructure.Telemetry, IAM.Infrastructure.Auth, IAM.Endpoints.Tokens.VersionNeutral.Revoke, Products.Application.Persistence (+7 more)

### Community 12 - "VerifyPhoneOtpResponse"
Cohesion: 0.20
Nodes (10): OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, VerifyPhoneOtpResponseExtensions, CancellationToken (+2 more)

### Community 13 - ".SendOtp"
Cohesion: 0.22
Nodes (10): SendPhoneOtpRequest, SendPhoneOtpResponse, CancellationToken, IFeatureManager, Task, CancellationToken, IOptions, RequestLocalizationOptions (+2 more)

### Community 14 - "UserRepresentation"
Cohesion: 0.08
Nodes (24): Dictionary, List, ErrorRepresentation, Error, ErrorMessage, Field, RoleRepresentation, Id (+16 more)

### Community 15 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.14
Nodes (14): AuthorizationHandler, AuthorizationHandlerContext, CancellationToken, ClaimsPrincipal, HttpContext, IFusionCache, IHttpContextAccessor, IOptions (+6 more)

### Community 16 - "ISmsGateway"
Cohesion: 0.13
Nodes (13): CancellationToken, Task, ISmsGateway, SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, SmsMessage (+5 more)

### Community 17 - "HangfireCustomAuthorizationFilter"
Cohesion: 0.29
Nodes (5): DashboardContext, IAuthorizationService, IDashboardAsyncAuthorizationFilter, HangfireCustomAuthorizationFilter, Task

### Community 18 - "DomainEvent"
Cohesion: 0.11
Nodes (18): DomainEvent, CreatedOn, Id, Version, DateTimeOffset, DefaultIdType, ProductId, V1ProductDescriptionUpdatedDomainEvent (+10 more)

### Community 19 - "AuditLogEntry"
Cohesion: 0.13
Nodes (15): AuditableEntity, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, Version, DateTimeOffset (+7 more)

### Community 20 - "IKeycloakAdminClient"
Cohesion: 0.21
Nodes (8): CancellationToken, IReadOnlyList, Task, IKeycloakAdminClient, CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.14
Nodes (8): SeekOrigin, BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position, Stream

### Community 22 - "DeviceRegistration"
Cohesion: 0.14
Nodes (13): DateTimeOffset, DefaultIdType, Guid, DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive (+5 more)

### Community 23 - "KeycloakPermissionPolicyProvider"
Cohesion: 0.15
Nodes (12): AuthorizationOptions, AuthorizationPolicy, DefaultAuthorizationPolicyProvider, IAuthorizationPolicyProvider, IAuthorizationRequirement, KeycloakPermission, ConcurrentDictionary, IOptions (+4 more)

### Community 24 - "DeactivateDeviceSessionsRequest"
Cohesion: 0.13
Nodes (14): DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse, IReadOnlyList, CancellationToken, RouteGroupBuilder, Task, Endpoint, CancellationToken (+6 more)

### Community 25 - "NetGsmSmsGateway"
Cohesion: 0.16
Nodes (14): SendRequestBody, SendResponseBody, CancellationToken, Exception, HttpClient, ILogger, IOptions, JsonSerializerOptions (+6 more)

### Community 26 - "IEvent"
Cohesion: 0.12
Nodes (14): DomainEventHandlerBase, CancellationToken, Task, IEventHandler, CancellationToken, Task, IEventHandlerWrapper, CancellationToken (+6 more)

### Community 27 - "RequestResponseBodyLoggingMiddleware"
Cohesion: 0.22
Nodes (7): IDiagnosticContext, HttpContext, IList, IOptions, PathString, RequestDelegate, RequestResponseBodyLoggingMiddleware

### Community 28 - "KeycloakTokenClient"
Cohesion: 0.26
Nodes (8): JsonWebTokenHandler, Exception, HttpClient, ILogger, IOptions, LoggerMessage, TimeProvider, KeycloakTokenClient

### Community 29 - "Result"
Cohesion: 0.17
Nodes (11): Result, Error, IsFailure, Value, AsyncExtensions, SyncExtensions, Action, Func (+3 more)

### Community 30 - "Product"
Cohesion: 0.08
Nodes (26): ISearchLocalized, Language, ProductTemplate, ProductTemplateId, Store, StoreId, Product, Description (+18 more)

### Community 31 - "ApplicationUserId"
Cohesion: 0.22
Nodes (5): ApplicationUserId, IsEmpty, Value, DefaultIdType, NotificationGroupName

### Community 32 - ".CreateStoreAsync"
Cohesion: 0.16
Nodes (9): Products.Endpoints.Stores, RouteGroupBuilder, Setup, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response (+1 more)

### Community 33 - ".AddProductAsync"
Cohesion: 0.22
Nodes (8): CancellationToken, ProductTemplate, RouteGroupBuilder, Store, Task, Endpoint, Response, Id

### Community 34 - "KeycloakPermissionClient"
Cohesion: 0.23
Nodes (11): CancellationToken, Dictionary, HttpClient, HttpResponseMessage, ILogger, IOptions, IReadOnlyList, List (+3 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.10
Nodes (20): IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, DateTimeOffset, OutboxMessage (+12 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.10
Nodes (18): CheckRegistrationRateLimitingPolicy, RegisterRateLimitingPolicy, SmsRateLimitingPolicy, CustomRateLimitingOptions, CheckRegistration, CreateStore, ExemptPathPrefixes, Global (+10 more)

### Community 37 - "KeycloakTokenClient.cs"
Cohesion: 0.17
Nodes (7): IAM.Infrastructure.Keycloak.Representations, IAM.Domain.Errors, IAM.Infrastructure.Keycloak, OtpErrors, TokenErrors, OAuthErrors, UserAttributes

### Community 38 - ".RefreshToken"
Cohesion: 0.15
Nodes (11): IAM.Endpoints.Tokens.VersionNeutral.Refresh, CancellationToken, RouteGroupBuilder, Task, Endpoint, DateTimeOffset, Response, AccessToken (+3 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.10
Nodes (19): IHostBuilder, ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics, EnableTracing, LogSink (+11 more)

### Community 40 - "SmsRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, SmsRateLimitingPolicy (+1 more)

### Community 41 - "Products.Domain.Stores"
Cohesion: 0.09
Nodes (12): Products.Endpoints.Stores.v1.Create, Products.Endpoints.Stores.v1.Search, Products.Endpoints.Products.v1.My.Get, Products.Endpoints.ProductTemplates.v1.Search, Common.Application.DTOs, Products.Endpoints.Stores.v1.Get, Products.Endpoints.Products.v1.My.Search, Products.Endpoints.ProductTemplates.v1.Get (+4 more)

### Community 42 - ".SaveWithOutboxAsync"
Cohesion: 0.27
Nodes (9): OutboxSaveHelper, CancellationToken, DbContext, Exception, Func, ILogger, LoggerMessage, Task (+1 more)

### Community 43 - "CreateStoreRateLimitingPolicy"
Cohesion: 0.14
Nodes (12): CancellationToken, Func, HttpContext, IOptions, IProblemDetailsService, IResxLocalizer, OnRejectedContext, RateLimitPartition (+4 more)

### Community 44 - "KeycloakOptions"
Cohesion: 0.14
Nodes (14): KeycloakOptions, AttemptTimeoutSeconds, Authority, BaseUrl, DecisionCacheMaxDurationSeconds, Realm, RequireHttpsMetadata, ResourceClientId (+6 more)

### Community 45 - ".CreateTokens"
Cohesion: 0.16
Nodes (13): BindDeviceSessionRequest, BindDeviceSessionResponse, Guid, CancellationToken, ILogger, Task, CancellationToken, ILogger (+5 more)

### Community 46 - "ProductsModule.cs"
Cohesion: 0.12
Nodes (10): ApiVersionSet, Common.Endpoints.Versioning, Products.Endpoints.Probe, Products.Endpoints, Setup, IEndpointRouteBuilder, IServiceCollection, IAssemblyReference (+2 more)

### Community 48 - "IOtpService"
Cohesion: 0.38
Nodes (4): CancellationToken, Task, TimeSpan, IOtpService

### Community 50 - "Response"
Cohesion: 0.12
Nodes (16): CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate (+8 more)

### Community 51 - "Products.Domain.Products"
Cohesion: 0.10
Nodes (9): Products.Domain.Products.DomainEvents.v1, Products.Endpoints.Stores.v1.My.AddProduct, Common.Domain.Events, Products.Endpoints.Products.v1.Search, Products.Domain.Products, Common.Domain.Aggregates, Products.Endpoints.Stores.v1.AddProduct, Products.Domain.Stores.DomainEvents.v1 (+1 more)

### Community 52 - "ReCaptchaService"
Cohesion: 0.21
Nodes (10): FormUrlEncodedContent, ReCaptchaResponse, CancellationToken, Exception, HttpClient, ILogger, IOptions, LoggerMessage (+2 more)

### Community 53 - "Products.Domain.ProductTemplates"
Cohesion: 0.17
Nodes (9): Products.Infrastructure.Persistence.Seeding, Products.Endpoints.Probe.v1, Common.InterModuleRequests.IAM, IAM.Infrastructure.InterModuleRequestHandlers, Products.Domain.ProductTemplates, ILogger, LoggerMessage, ProductsDbContext (+1 more)

### Community 54 - "NotificationsModule.cs"
Cohesion: 0.14
Nodes (8): Notifications.Application.Sms, Notifications.Infrastructure.Devices, Notifications.Infrastructure.Sms, Common.Infrastructure.Resiliency, Notifications.Infrastructure.Sms.NetGsm, Notifications.Infrastructure, Setup, IAssemblyReference

### Community 55 - "RegisterRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, RegisterRateLimitingPolicy (+1 more)

### Community 56 - "DummySmsGateway"
Cohesion: 0.38
Nodes (5): CancellationToken, ILogger, LoggerMessage, Task, DummySmsGateway

### Community 58 - "OutboxMetricsJob"
Cohesion: 0.13
Nodes (14): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxMetricsJob (+6 more)

### Community 59 - ".SearchProductTemplatesAsync"
Cohesion: 0.18
Nodes (10): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Brand (+2 more)

### Community 61 - ".AddProductToMyStoreAsync"
Cohesion: 0.12
Nodes (16): CancellationToken, ProductTemplate, RouteGroupBuilder, Store, Task, Endpoint, ProductTemplateId, Request (+8 more)

### Community 62 - "NotificationsDbContext"
Cohesion: 0.15
Nodes (13): DbSet, INotificationsDbContext, DeviceRegistrations, DbContextOptions, DbSet, ILogger, TimeProvider, NotificationsDbContext (+5 more)

### Community 63 - "RequestLoggingOptions"
Cohesion: 0.11
Nodes (20): IPostConfigureOptions, RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes (+12 more)

### Community 64 - "Common.InterModuleRequests"
Cohesion: 0.29
Nodes (4): Common.InterModuleRequests, IAssemblyReference, Setup, IServiceCollection

### Community 65 - "Common.Application.Options"
Cohesion: 0.11
Nodes (13): Common.Infrastructure.Modules, Host, Common.Infrastructure.RateLimiting, Common.Infrastructure.Localization, Common.Application.Options, Host.Middlewares, IAM.Infrastructure.RateLimiting, Common.Infrastructure.Caching (+5 more)

### Community 66 - "OutboxModule"
Cohesion: 0.13
Nodes (16): Action, Exception, IApplicationBuilder, IEndpointRouteBuilder, IEnumerable, IHostApplicationLifetime, ILogger, ILoggerFactory (+8 more)

### Community 67 - "AuditableEntityResponse"
Cohesion: 0.12
Nodes (15): AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, DateTimeOffset, CancellationToken (+7 more)

### Community 68 - "ValueObject"
Cohesion: 0.25
Nodes (4): Common.Domain, IComparable, ValueObject, IEnumerable

### Community 69 - "V1StoreCreatedDomainEvent"
Cohesion: 0.36
Nodes (7): CancellationToken, Task, SimulateSomeBusinessHandler, StoreCreatedIntegrationEventPublishingHandler, V1StoreCreatedDomainEventHandlers, StoreId, V1StoreCreatedDomainEvent

### Community 70 - "IStronglyTypedId"
Cohesion: 0.06
Nodes (32): JsonConverter, StrictDateTimeOffsetJsonConverter, DateTimeOffset, JsonSerializerOptions, Type, Utf8JsonReader, Utf8JsonWriter, StronglyTypedIdListReadOnlyJsonConverter (+24 more)

### Community 71 - "Full-Text Search"
Cohesion: 0.08
Nodes (25): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Add a new language/culture, Add search to a new entity _(Build checklist)_ (+17 more)

### Community 72 - "Response"
Cohesion: 0.20
Nodes (9): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Description, Name, Price (+1 more)

### Community 73 - ".WriteAsync"
Cohesion: 0.33
Nodes (5): Memory, ReadOnlyMemory, CancellationToken, Task, ValueTask

### Community 75 - "ProductId"
Cohesion: 0.15
Nodes (12): Products.Endpoints.Stores.v1.My.RemoveProduct, DefaultIdType, ProductId, Request, Id, RequestValidator, Request, Id (+4 more)

### Community 76 - "Common.Application.ModelBinders"
Cohesion: 0.09
Nodes (19): Products.Endpoints.ProductTemplates.v1.Deactivate, Common.Application.ModelBinders, Products.Endpoints.ProductTemplates.v1.Activate, IModelBinder, ModelBindingContext, StronglyTypedIdBinder, Task, Request (+11 more)

### Community 77 - "IProductsDbContext"
Cohesion: 0.12
Nodes (15): Products.Endpoints.Stores.v1.My.Create, DbSet, Store, IProductsDbContext, Products, ProductTemplates, Stores, CancellationToken (+7 more)

### Community 78 - "CheckRegistrationRateLimitingPolicy"
Cohesion: 0.13
Nodes (15): IRateLimiterPolicy, CancellationToken, Func, IOptions, OnRejectedContext, ValueTask, CheckRegistrationRateLimitingPolicy, OnRejected (+7 more)

### Community 79 - "IntegrationEventOutbox"
Cohesion: 0.10
Nodes (16): Common.IntegrationEvents, Products.Application.Products.DomainEventHandlers.v1, Products.Application.Stores.DomainEventHandlers.v1, Common.Application.EventBus, Lock, IIntegrationEventOutbox, IntegrationEventOutbox, List (+8 more)

### Community 80 - "EventDispatcher"
Cohesion: 0.31
Nodes (7): EventDispatcher, ActivitySource, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task

### Community 81 - ".SearchStoresAsync"
Cohesion: 0.15
Nodes (12): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Address (+4 more)

### Community 82 - "StringExtensions"
Cohesion: 0.29
Nodes (3): Common.Domain.Extensions, SearchValues, StringExtensions

### Community 83 - ".EnsureNoMigrationsPending"
Cohesion: 0.30
Nodes (6): AutoMigrateMarker, IAutoMigrateMarker, MigrationGuard, ILogger, IServiceProvider, LoggerMessage

### Community 84 - ".Configure"
Cohesion: 0.15
Nodes (14): AuditableEntityConfiguration, EntityTypeBuilder, StronglyTypedIdValueConverter, DefaultIdType, EntityTypeBuilder, DeviceRegistrationConfiguration, EntityTypeBuilder, NpgsqlTsVector (+6 more)

### Community 85 - "ResultTelemetryExtensions"
Cohesion: 0.32
Nodes (4): Activity, ResultTelemetryExtensions, ActivitySource, Task

### Community 86 - "Store"
Cohesion: 0.12
Nodes (15): StoreId, V1StoreAddressUpdatedDomainEvent, StoreId, V1StoreDescriptionUpdatedDomainEvent, StoreId, V1StoreNameUpdatedDomainEvent, IReadOnlyCollection, List (+7 more)

### Community 87 - "PushOptions"
Cohesion: 0.19
Nodes (13): PushOptions, Provider, SendTimeoutSeconds, ServiceAccount, Templates, PushOptionsValidator, PushProvider, Dummy (+5 more)

### Community 89 - "Setup"
Cohesion: 0.33
Nodes (4): ConfigurationManager, Host.Configurations, Setup, WebApplicationBuilder

### Community 90 - "IInterModuleRequestClient"
Cohesion: 0.12
Nodes (13): IClientFactory, IInterModuleRequestClient, CancellationToken, Task, MassTransitInterModuleRequestClient, CancellationToken, IOptions, Task (+5 more)

### Community 91 - ".AddCaptchaInfrastructure"
Cohesion: 0.33
Nodes (4): DummyCaptchaService, IConfiguration, IServiceCollection, Setup

### Community 92 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.05
Nodes (20): Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, IAM.Endpoints.Users.VersionNeutral.Search, Common.Domain.StronglyTypedIds, Common.Application.Persistence, Common.Application.AuditLog, Notifications.Domain.Devices, Notifications.Infrastructure.Persistence (+12 more)

### Community 93 - "Endpoint"
Cohesion: 0.18
Nodes (8): IAM.Endpoints.Captcha.VersionNeutral, IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, RouteGroupBuilder, Endpoint, Response, ClientKey, RouteGroupBuilder, Setup

### Community 94 - "FirebasePushGateway.cs"
Cohesion: 0.28
Nodes (5): Notifications.Application.Push, Notifications.Infrastructure.Push, Notifications.Infrastructure.Push.Firebase, PushErrors, Setup

### Community 95 - ".SearchMyProductsAsync"
Cohesion: 0.10
Nodes (19): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Request, Description (+11 more)

### Community 96 - "DatabaseSeederOrchestrator"
Cohesion: 0.29
Nodes (7): BackgroundService, DatabaseSeederOrchestrator, Exception, IHostApplicationLifetime, ILogger, IServiceScopeFactory, LoggerMessage

### Community 97 - "AuditLogRetentionService"
Cohesion: 0.11
Nodes (18): Common.Infrastructure.Persistence.AuditLog, IHostedService, AuditLogRetentionJobRegistrar, CancellationToken, ILogger, IServiceProvider, LoggerMessage, Task (+10 more)

### Community 98 - ".SearchStoreProductsAsync"
Cohesion: 0.17
Nodes (11): CancellationToken, IOptions, NpgsqlTsVector, RouteGroupBuilder, Task, Endpoint, Response, Description (+3 more)

### Community 100 - "OutboxDbContext"
Cohesion: 0.14
Nodes (13): DbContext, IOutboxDbContext, OutboxMessages, CancellationToken, DbSet, Task, IConfiguration, IServiceCollection (+5 more)

### Community 101 - "ResiliencyOptions"
Cohesion: 0.11
Nodes (18): HttpStandardResilienceOptions, IHttpClientBuilder, ResiliencyOptions, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds (+10 more)

### Community 102 - "HttpContextTargetingContextAccessor"
Cohesion: 0.17
Nodes (9): Common.Infrastructure.FeatureManagement, ITargetingContextAccessor, HttpContextTargetingContextAccessor, IHttpContextAccessor, ValueTask, Setup, IConfiguration, IServiceCollection (+1 more)

### Community 103 - "BackgroundJobsTelemetry"
Cohesion: 0.14
Nodes (11): IServerFilter, PerformedContext, PerformingContext, JobMetricsFilter, BackgroundJobsTelemetry, ActivitySource, ConcurrentDictionary, Counter (+3 more)

### Community 104 - "IntegrationEvent"
Cohesion: 0.22
Nodes (9): IReadOnlyList, IntegrationEvent, CreatedOn, Id, DateTimeOffset, DefaultIdType, ProductCreatedIntegrationEvent, StoreCreatedIntegrationEvent (+1 more)

### Community 105 - ".AddKeycloakInfrastructure"
Cohesion: 0.16
Nodes (10): CancellationToken, Task, IServiceAccountTokenProvider, HttpClient, IOptions, TimeProvider, ServiceAccountTokenProvider, IOptions (+2 more)

### Community 106 - "OutboxModule.cs"
Cohesion: 0.22
Nodes (5): Outbox, Common.Infrastructure.Persistence.Outbox, Outbox.Persistence, Common.Application.Persistence.Outbox, Outbox.Telemetry

### Community 107 - ".RequestTokensAsync"
Cohesion: 0.34
Nodes (7): DateTimeOffset, KeycloakTokens, CancellationToken, Dictionary, Error, Result, Task

### Community 109 - ".IsRegisteredAsync"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, IsRegistered

### Community 110 - "ProductsDbContext"
Cohesion: 0.15
Nodes (12): DbContextOptions, DbSet, ILogger, ProductTemplate, Store, TimeProvider, ProductsDbContext, Products (+4 more)

### Community 111 - "BackgroundJobsService"
Cohesion: 0.23
Nodes (8): IBackgroundJobClientV2, BackgroundJobsService, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 112 - "OutboxCleanupJob"
Cohesion: 0.27
Nodes (8): CancellationToken, ILogger, IOptions, IServiceScopeFactory, LoggerMessage, Task, TimeProvider, OutboxCleanupJob

### Community 113 - "CaptchaOptions"
Cohesion: 0.16
Nodes (13): CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey, Provider, ScoreThreshold, SecretKey (+5 more)

### Community 114 - "Response"
Cohesion: 0.12
Nodes (16): RouteGroupBuilder, Endpoint, DateOnly, DateTimeOffset, IReadOnlyCollection, Response, BirthDate, CreatedOn (+8 more)

### Community 115 - "IBackgroundJobs"
Cohesion: 0.26
Nodes (7): IBackgroundJobs, Action, DateTimeOffset, Expression, Func, Task, TimeSpan

### Community 116 - ".WriteProblemAsync"
Cohesion: 0.12
Nodes (14): IAllowAnonymous, IConfigureNamedOptions, ProblemDetailsContext, ProblemDetailsServiceExtensions, IProblemDetailsService, Task, HttpContext, HttpStatusCode (+6 more)

### Community 117 - "Common.Domain.Devices"
Cohesion: 0.18
Nodes (10): Notifications.Infrastructure.Devices.UpdateCurrentPushToken, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, Common.Domain.Devices, DeviceSessionConstants, Request, Id, RequestValidator, Request (+2 more)

### Community 118 - "IModule"
Cohesion: 0.09
Nodes (21): OpenTelemetryBuilder, ResourceBuilder, IModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority (+13 more)

### Community 119 - "ProductsModule"
Cohesion: 0.12
Nodes (13): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, ProductsModule (+5 more)

### Community 120 - "AggregateRoot"
Cohesion: 0.17
Nodes (11): AggregateRoot, Events, Id, Version, IReadOnlyCollection, List, IAggregateRoot, Events (+3 more)

### Community 121 - ".GetVariantAsync"
Cohesion: 0.33
Nodes (4): IVariantFeatureManager, IVariantFeatureManagerExtensions, CancellationToken, Task

### Community 122 - "TokenRefreshRateLimitingPolicy"
Cohesion: 0.20
Nodes (9): CancellationToken, Func, HttpContext, IOptions, OnRejectedContext, RateLimitPartition, ValueTask, TokenRefreshRateLimitingPolicy (+1 more)

### Community 123 - "FullTextSearchOptions"
Cohesion: 0.25
Nodes (8): FullTextSearchOptions, CultureToConfig, DefaultConfig, RankWeights, UseUnaccent, FullTextSearchOptionsValidator, Dictionary, IReadOnlyList

### Community 124 - "HealthCheckOptions"
Cohesion: 0.12
Nodes (14): HealthCheckOptions, EnableHealthChecks, ReadinessTimeoutInSeconds, StartupTimeoutInSeconds, HealthCheckOptionsValidator, IApplicationBuilder, IConfiguration, IConnectionMultiplexer (+6 more)

### Community 125 - ".TapWhenFeatureEnabledAsync"
Cohesion: 0.40
Nodes (4): Action, Func, IFeatureManager, Task

### Community 126 - "GlobalExceptionHandlingMiddleware"
Cohesion: 0.08
Nodes (23): IAuthenticationSchemeProvider, IMiddleware, IApplicationBuilder, IApplicationBuilder, IServiceCollection, HttpContext, RequestDelegate, Task (+15 more)

### Community 127 - "BaseDbContext"
Cohesion: 0.22
Nodes (8): BaseDbContext, AuditLog, CancellationToken, DbContextOptions, DbSet, ILogger, Task, TimeProvider

### Community 128 - "AuditLogEntryConfiguration"
Cohesion: 0.17
Nodes (9): AuditLogEntryConfiguration, EntityTypeBuilder, DomainEventConverter, JsonSerializerOptions, IntegrationEventConverter, JsonSerializerOptions, ModelBuilder, ModelBuilder (+1 more)

### Community 129 - "Response"
Cohesion: 0.14
Nodes (12): Products.Endpoints.Products, RouteGroupBuilder, Setup, CancellationToken, RouteGroupBuilder, Task, Endpoint, Response (+4 more)

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.48
Nodes (7): Observability (OpenTelemetry), docker-compose.yml (Base Stack), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), Postgres Service (mm.postgres), RabbitMQ Service (mm.rabbitmq), Redis Service (mm.redis)

### Community 131 - ".AddInfrastructure"
Cohesion: 0.29
Nodes (7): HostOptions, JsonOptions, Assembly, IConfiguration, IResxLocalizer, IServiceCollection, IWebHostEnvironment

### Community 132 - ".SaveChangesAsync"
Cohesion: 0.13
Nodes (10): CancellationToken, Task, CancellationToken, RouteGroupBuilder, Task, Endpoint, CancellationToken, RouteGroupBuilder (+2 more)

### Community 134 - "v1/RemoveProduct/Request.cs"
Cohesion: 0.29
Nodes (7): Products.Endpoints.Stores.v1.RemoveProduct, ProductId, StoreId, Request, Id, ProductId, RequestValidator

### Community 135 - ".ListSessions"
Cohesion: 0.12
Nodes (15): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, CancellationToken, IReadOnlyCollection, RouteGroupBuilder, Task, Endpoint, DateTimeOffset, Response (+7 more)

### Community 136 - "PersistenceQueryableExtensions"
Cohesion: 0.33
Nodes (6): PersistenceQueryableExtensions, CancellationToken, Expression, Func, IQueryable, Task

### Community 137 - ".WriteTooManyRequestsToResponse"
Cohesion: 0.13
Nodes (14): PartitionedRateLimiter, CancellationToken, Func, HttpContext, IConfiguration, IProblemDetailsService, IReadOnlyList, IResxLocalizer (+6 more)

### Community 138 - ".InvokeAsync"
Cohesion: 0.07
Nodes (27): Common.Application.EndpointFilters, IEndpointFilter, IFeatureManagerSnapshot, ProblemDetails, ResxLocalizer, ResultToCreatedResponseTransformer, ResultToResponseTransformer, EndpointFilterDelegate (+19 more)

### Community 139 - "ProductTemplate"
Cohesion: 0.15
Nodes (11): IReadOnlyList, List, ProductTemplate, Brand, Color, IsActive, Model, Products (+3 more)

### Community 140 - ".RegisterAsync"
Cohesion: 0.11
Nodes (18): IAM.Endpoints.Users.VersionNeutral.SelfRegister, IAM.Endpoints.Users.VersionNeutral, CancellationToken, Exception, IFeatureManager, ILogger, LoggerMessage, RouteGroupBuilder (+10 more)

### Community 141 - ".GetMeAsync"
Cohesion: 0.19
Nodes (9): CancellationToken, IReadOnlyList, Task, IKeycloakPermissionClient, IReadOnlyList, GrantedPermission, CancellationToken, HttpContext (+1 more)

### Community 142 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 143 - "IDbContext"
Cohesion: 0.22
Nodes (8): DatabaseFacade, EntityEntry, IDisposable, IDbContext, AuditLog, ChangeTracker, Database, DbSet

### Community 144 - "Response"
Cohesion: 0.12
Nodes (16): CancellationToken, RouteGroupBuilder, Task, Endpoint, DateOnly, DateTimeOffset, Response, BirthDate (+8 more)

### Community 145 - ".ExecuteAsync"
Cohesion: 0.20
Nodes (7): CancellationToken, Task, SeedingCompletionTracker, CancellationToken, Exception, Task, TaskCompletionSource

### Community 146 - "IAM.Endpoints.Otp.VersionNeutral"
Cohesion: 0.40
Nodes (3): IAM.Endpoints.Otp.VersionNeutral, RouteGroupBuilder, Setup

### Community 147 - "RecurringBackgroundJobsService"
Cohesion: 0.13
Nodes (13): RecurringJobOptions, IRecurringBackgroundJobs, Action, Expression, Func, Task, RecurringBackgroundJobsService, Action (+5 more)

### Community 148 - "Setup"
Cohesion: 0.09
Nodes (19): LoadAll, LoggerConfiguration, LoggerMinimumLevelConfiguration, ModuleRegistry, Names, Type, Setup, IHostEnvironment (+11 more)

### Community 149 - "Response"
Cohesion: 0.18
Nodes (9): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, RouteGroupBuilder, Endpoint, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken (+1 more)

### Community 150 - "FeatureFlags"
Cohesion: 0.33
Nodes (5): Checkout, FeatureFlags, IAM, Notifications, Products

### Community 151 - "OutboxOptions"
Cohesion: 0.11
Nodes (20): OutboxCleanupSettings, BatchSize, CronSchedule, Enabled, RetentionDays, OutboxCleanupSettingsValidator, OutboxOptions, BaseBackoffSeconds (+12 more)

### Community 152 - "ISearchLanguageResolver"
Cohesion: 0.15
Nodes (11): ISearchLanguageResolver, UniversalConfig, SearchLanguageResolver, UniversalConfig, IOptions, Setup, IServiceCollection, CancellationToken (+3 more)

### Community 153 - "SwaggerDefaultValues"
Cohesion: 0.33
Nodes (4): JsonValue, OpenApiOperation, OperationFilterContext, SwaggerDefaultValues

### Community 154 - "Request"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Products.v1.Update, RequestBody, Request, Body, Id, RequestBody, Description, Name (+3 more)

### Community 155 - "InterModuleRequestHandler"
Cohesion: 0.19
Nodes (8): IConsumer, IInterModuleRequestHandler, CancellationToken, Task, InterModuleRequestHandler, CancellationToken, ConsumeContext, Task

### Community 156 - "PaginationRequest"
Cohesion: 0.08
Nodes (24): Products.Endpoints.Stores.v1.My.AuditLog, Products.Endpoints.Products.v1.AuditLog, PaginationRequest, PageNumber, PageSize, Skip, Take, PaginationRequestValidator (+16 more)

### Community 157 - "IAuditableEntity"
Cohesion: 0.18
Nodes (10): IAuditableEntity, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, DateTimeOffset, CancellationToken, DbContextEventData (+2 more)

### Community 158 - ".FixedWindow"
Cohesion: 0.18
Nodes (8): HttpContext, IConnectionMultiplexer, ILoggerFactory, RateLimitPartition, HttpContext, RateLimitPartition, HttpContext, RateLimitPartition

### Community 160 - "OtpServiceBase"
Cohesion: 0.21
Nodes (8): OtpCacheEntry, DateTimeOffset, CancellationToken, IFusionCache, SemaphoreSlim, Task, TimeSpan, OtpServiceBase

### Community 161 - ".UpdateMyStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 162 - "UtcDateTimeOffsetConverter"
Cohesion: 0.22
Nodes (6): DateTimeOffset, ModelConfigurationBuilder, UtcDateTimeOffsetConverter, DateTimeOffset, DateTimeOffset, ModelConfigurationBuilder

### Community 164 - "Request"
Cohesion: 0.15
Nodes (13): Guid, Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FirstName (+5 more)

### Community 165 - "ICurrentUser"
Cohesion: 0.10
Nodes (20): Common.Infrastructure.Auth.Services, Common.Infrastructure.Auth, ICurrentUser, Id, IdAsString, Roles, SessionId, ICollection (+12 more)

### Community 166 - "Response"
Cohesion: 0.14
Nodes (11): IAM.Endpoints.Tokens.VersionNeutral, RouteGroupBuilder, Endpoint, DateTimeOffset, Response, AccessToken, AccessTokenExpiresAt, RefreshToken (+3 more)

### Community 167 - ".PaginateAsync"
Cohesion: 0.29
Nodes (6): PaginationQueryableExtensions, CancellationToken, Expression, Func, IQueryable, Task

### Community 168 - "ICaptchaService"
Cohesion: 0.19
Nodes (8): CancellationToken, Task, ICaptchaService, CancellationToken, IFusionCache, IOptions, Task, CachedCaptchaService

### Community 169 - "Policies.CreateStore.cs"
Cohesion: 0.17
Nodes (8): CreateStoreRateLimitingPolicy, Products.Infrastructure.RateLimiting, RateLimiterOptions, Policies, Action, IEnumerable, RateLimiterOptions, RateLimitingConstants

### Community 171 - ".UpdateStoreAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 172 - "ResxLocalizationOptions"
Cohesion: 0.25
Nodes (7): ResxLocalizationOptions, DefaultCulture, SupportedCultures, ResxLocalizationOptionsValidator, ICollection, IApplicationBuilder, IOptions

### Community 173 - "Request"
Cohesion: 0.17
Nodes (12): ProductTemplateId, RequestBody, Request, Body, Id, RequestBody, Description, Name (+4 more)

### Community 174 - "IInterModuleRequest"
Cohesion: 0.29
Nodes (8): IInterModuleRequest, DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, IReadOnlyList, CancellationToken, Task, GetDeviceSessionsRequestHandler

### Community 175 - "PaginationResponse"
Cohesion: 0.06
Nodes (31): Products.Endpoints.Stores.v1.AuditLog, JsonElement, AuditLogDto, DateTimeOffset, PaginationResponse, HasNext, HasPrevious, NextPageNumber (+23 more)

### Community 176 - "SendSecurityAlertRequestHandler"
Cohesion: 0.27
Nodes (9): SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, CancellationToken, IOptions, RequestLocalizationOptions, Task (+1 more)

### Community 177 - ".AddAuthInfrastructure"
Cohesion: 0.25
Nodes (6): IAuthorizationHandler, IAuthorizationPolicyProvider, IConfigureOptions, IServiceCollection, JwtBearerOptions, Setup

### Community 178 - "OtpOptions"
Cohesion: 0.50
Nodes (4): OtpOptions, ExpirationInMinutes, Length, OtpOptionsValidator

### Community 179 - "IntegrationEventHandlerBase"
Cohesion: 0.07
Nodes (35): IntegrationEventHandlerBase, MaxEventAge, CancellationToken, ConsumeContext, DefaultIdType, IFusionCache, ILogger, IOptions (+27 more)

### Community 180 - "SmsOptions"
Cohesion: 0.10
Nodes (21): SmsOptions, AppName, AttemptTimeoutSeconds, BaseUrl, MaxPerDay, MaxPerPhoneNumberPerDay, MaxRetryAttempts, MsgHeader (+13 more)

### Community 181 - "RabbitMqOptions"
Cohesion: 0.12
Nodes (15): RabbitMqOptions, Host, Password, Port, RetryIntervalDeltaMs, RetryLimit, RetryMaxIntervalMs, RetryMinIntervalMs (+7 more)

### Community 182 - "IAM.Domain"
Cohesion: 0.40
Nodes (3): IAM.Domain, Constants, IAssemblyReference

### Community 183 - "SendRequestBody"
Cohesion: 0.25
Nodes (8): SendMessageBody, IReadOnlyList, SendRequestBody, AppName, Encoding, IysFilter, Messages, MsgHeader

### Community 184 - "SignalROptions"
Cohesion: 0.50
Nodes (4): SignalROptions, RedisConnectionString, UseRedisBackplane, SignalROptionsValidator

### Community 185 - "IamModule"
Cohesion: 0.12
Nodes (13): Action, IApplicationBuilder, IConfiguration, IEndpointRouteBuilder, IEnumerable, IServiceCollection, RateLimiterOptions, IamModule (+5 more)

### Community 186 - "Request"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Stores.v1.Update, RequestBody, Request, Body, Id, RequestBody, Address, Description (+3 more)

### Community 187 - "ReverseProxyOptions"
Cohesion: 0.18
Nodes (9): ForwardedHeadersOptions, ReverseProxyOptions, ForwardLimit, IsEnabled, TrustedNetworks, ReverseProxyOptionsValidator, IReadOnlyList, IConfiguration (+1 more)

### Community 188 - "BoundedCaptureStream"
Cohesion: 0.18
Nodes (7): HttpResponse, BoundedCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 190 - ".GetAccessTokenAsync"
Cohesion: 0.18
Nodes (5): KeycloakPaths, CancellationToken, Task, CancellationToken, Task

### Community 191 - "Split-Deployment PoC"
Cohesion: 0.22
Nodes (8): Concurrent safety, Cross-process call path, Files added by this PoC, How it works, How to run, Split-Deployment PoC, What this proves, README (Boilerplate Overview)

### Community 192 - "ServiceAccountTokenCache"
Cohesion: 0.22
Nodes (6): DateTimeOffset, SemaphoreSlim, TimeSpan, ServiceAccountTokenCache, AccessToken, ExpiresAt

### Community 193 - "Request"
Cohesion: 0.18
Nodes (11): StoreId, Request, Description, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name (+3 more)

### Community 194 - "CorsOptions"
Cohesion: 0.25
Nodes (8): CorsOptions, AllowCredentials, AllowedHeaders, AllowedMethods, AllowedOrigins, MaxAgeInSeconds, CorsOptionsValidator, IReadOnlyList

### Community 195 - ".BindDeviceAsync"
Cohesion: 0.33
Nodes (7): CancellationToken, Exception, Guid, ILogger, LoggerMessage, Task, LoginCompletion

### Community 196 - "TokenEndpointRepresentations.cs"
Cohesion: 0.20
Nodes (9): List, DecisionRepresentation, Result, PermissionRepresentation, ResourceName, Scopes, TokenErrorRepresentation, Error (+1 more)

### Community 197 - ".AddPushServices"
Cohesion: 0.24
Nodes (7): CancellationToken, ILogger, LoggerMessage, Task, DummyPushGateway, IConfiguration, IServiceCollection

### Community 198 - "Common.Application.BackgroundJobs"
Cohesion: 0.31
Nodes (3): Common.Application.BackgroundJobs, BackgroundJobs.Telemetry, BackgroundJobs

### Community 199 - "OpenApiOptions"
Cohesion: 0.22
Nodes (9): OpenApiOptions, ContactEmail, ContactName, Description, EnableSwagger, LicenseName, LicenseUrl, Title (+1 more)

### Community 200 - "KeyValuePair"
Cohesion: 0.17
Nodes (7): KeyValuePair, IEnumerable, ActivitySource, Counter, Meter, NotificationsTelemetry, UpDownCounter

### Community 201 - "KeycloakScopes"
Cohesion: 0.22
Nodes (8): Devices, Hangfire, KeycloakScopes, Products, ProductTemplates, Sessions, Stores, Users

### Community 202 - "Request"
Cohesion: 0.20
Nodes (10): IAM.Endpoints.Tokens.VersionNeutral.Create, Guid, Request, ClientId, DeviceId, DeviceName, Otp, PhoneNumber (+2 more)

### Community 203 - ".DeactivateProductTemplateAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 210 - "ConfigureSwaggerOptions"
Cohesion: 0.28
Nodes (7): ApiVersionDescription, IApiVersionDescriptionProvider, IConfigureOptions, OpenApiInfo, IOptions, SwaggerGenOptions, ConfigureSwaggerOptions

### Community 211 - ".CreateProductTemplateAsync"
Cohesion: 0.29
Nodes (5): ProductTemplate, CancellationToken, Task, Response, Id

### Community 212 - "IamTelemetry"
Cohesion: 0.22
Nodes (6): ActivitySource, Counter, Meter, IamTelemetry, LoginMethods, SessionRevokedReasons

### Community 213 - "ProductsTelemetry"
Cohesion: 0.40
Nodes (4): ActivitySource, Counter, Meter, ProductsTelemetry

### Community 214 - "Notifications.Infrastructure.Otp"
Cohesion: 0.20
Nodes (6): Notifications.Infrastructure.Otp, IFusionCache, DummyOtpService, IConfiguration, IServiceCollection, Setup

### Community 215 - "RedisOtpService"
Cohesion: 0.28
Nodes (6): CancellationToken, IConnectionMultiplexer, IOptions, Task, TimeSpan, RedisOtpService

### Community 216 - "ProductTemplateId"
Cohesion: 0.25
Nodes (6): ProductTemplateId, DefaultIdType, ProductTemplateId, CancellationToken, List, Task

### Community 217 - "Request"
Cohesion: 0.25
Nodes (7): Constants, Request, Brand, Color, Model, SearchTerm, RequestValidator

### Community 218 - "Response"
Cohesion: 0.18
Nodes (10): CancellationToken, RouteGroupBuilder, Task, Endpoint, Response, Address, Description, Name (+2 more)

### Community 219 - "PushMessage"
Cohesion: 0.29
Nodes (6): IReadOnlyDictionary, CancellationToken, IReadOnlyList, Task, IPushGateway, PushMessage

### Community 220 - ".SeedProductAsync"
Cohesion: 0.22
Nodes (7): CancellationToken, Task, CancellationToken, List, ProductTemplateId, StoreId, Task

### Community 221 - "Keycloak realm as code"
Cohesion: 0.25
Nodes (7): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Secrets, Seed users (dev / test only)

### Community 222 - "Endpoint"
Cohesion: 0.29
Nodes (5): Products.Endpoints.ProductTemplates, RouteGroupBuilder, Setup, RouteGroupBuilder, Endpoint

### Community 223 - "ReCaptchaResponse"
Cohesion: 0.25
Nodes (7): DateTime, ReCaptchaResponse, ChallengeTs, ErrorCodes, Hostname, Score, Success

### Community 224 - ".SingleAsResult"
Cohesion: 0.36
Nodes (4): CollectionExtensions, Func, ICollection, IEnumerable

### Community 225 - "BackgroundJobsOptions"
Cohesion: 0.16
Nodes (11): BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount, BackgroundJobsOptionsValidator, IApplicationBuilder (+3 more)

### Community 226 - ".HandleAsync"
Cohesion: 0.18
Nodes (11): GetSeedUserIdsRequest, GetSeedUserIdsResponse, ICollection, CancellationToken, Task, GetSeedUserIdsRequestHandler, CancellationToken, IResult (+3 more)

### Community 227 - "BackgroundJobsModule"
Cohesion: 0.22
Nodes (8): ICoreModule, BackgroundJobsModule, ActivitySourceNames, MeterNames, Name, StartupPriority, IEndpointRouteBuilder, IEnumerable

### Community 228 - "CustomValidator"
Cohesion: 0.14
Nodes (15): AbstractValidator, CustomRateLimitingOptionsValidator, FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit, FixedWindowValidator (+7 more)

### Community 229 - ".AddCommonOptions"
Cohesion: 0.20
Nodes (6): Setup, IConfiguration, IHostEnvironment, IServiceCollection, ValidationContextExtensions, ValidationContext

### Community 230 - "OutboxMessageConfig"
Cohesion: 0.29
Nodes (4): IEntityTypeConfiguration, ModelBuilder, EntityTypeBuilder, OutboxMessageConfig

### Community 231 - "Request"
Cohesion: 0.33
Nodes (6): Products.Endpoints.Stores.v1.My.Update, Request, Address, Description, Name, RequestValidator

### Community 232 - "FirebaseServiceAccountOptions"
Cohesion: 0.29
Nodes (7): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri

### Community 233 - "IKeycloakTokenClient"
Cohesion: 0.48
Nodes (3): CancellationToken, Task, IKeycloakTokenClient

### Community 234 - "Setup"
Cohesion: 0.29
Nodes (4): IApplicationBuilder, IWebHostEnvironment, Type, Setup

### Community 235 - "KeycloakUser"
Cohesion: 0.43
Nodes (6): DateOnly, DateTimeOffset, CreateKeycloakUser, KeycloakUser, KeycloakUserPage, KeycloakUserSession

### Community 236 - ".UpdateCurrentPushToken"
Cohesion: 0.29
Nodes (5): CancellationToken, RouteGroupBuilder, Task, TimeProvider, Endpoint

### Community 237 - ".AddPersistence"
Cohesion: 0.14
Nodes (11): IDatabaseSeeder, Priority, CancellationToken, Task, CancellationToken, IServiceScopeFactory, Task, ProductsDatabaseSeeder (+3 more)

### Community 238 - ".AddNetGsm"
Cohesion: 0.33
Nodes (5): IConfiguration, IFusionCache, IOptions, IServiceCollection, Setup

### Community 239 - ".TryReadFromJsonAsync"
Cohesion: 0.33
Nodes (4): HttpContent, CancellationToken, Task, HttpContentJsonExtensions

### Community 240 - "TokenResponseRepresentation"
Cohesion: 0.33
Nodes (6): TokenResponseRepresentation, AccessToken, ExpiresIn, RefreshExpiresIn, RefreshToken, SessionState

### Community 241 - "IAM.Endpoints.Common.Validations"
Cohesion: 0.10
Nodes (17): IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, IAM.Endpoints.Common.Validations, IRuleBuilder, IRuleBuilderOptions, IResxLocalizer, CommonValidations, Request, CaptchaToken (+9 more)

### Community 242 - "Host.Swagger"
Cohesion: 0.22
Nodes (5): Host.Swagger, IOpenApiSchema, ISchemaFilter, SchemaFilterContext, StronglyTypedIdSchemaFilter

### Community 243 - ".RemoveProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 245 - "RemoveDefaultResponseSchemaFilter"
Cohesion: 0.33
Nodes (4): IOperationFilter, OpenApiOperation, OperationFilterContext, RemoveDefaultResponseSchemaFilter

### Community 246 - "StoreId"
Cohesion: 0.25
Nodes (6): StoreId, DefaultIdType, StoreId, CancellationToken, List, Task

### Community 247 - ".AddCommonPersistence"
Cohesion: 0.50
Nodes (3): Setup, IOptions, IServiceCollection

### Community 248 - ".UpdateMyProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 249 - "Request"
Cohesion: 0.29
Nodes (6): Products.Endpoints.ProductTemplates.v1.Create, Request, Brand, Color, Model, RequestValidator

### Community 250 - "Request"
Cohesion: 0.33
Nodes (6): Request, Address, Description, Name, OwnerId, RequestValidator

### Community 251 - "VersionNeutral/Get/Request.cs"
Cohesion: 0.67
Nodes (3): Request, Id, RequestValidator

### Community 252 - ".RemoveMyProductAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, RouteGroupBuilder, Task, Endpoint

### Community 260 - "OtpVerificationOutcome"
Cohesion: 0.22
Nodes (6): Notifications.Application.Otp, Common.Application.Caching, OtpVerificationOutcome, InvalidOtp, Success, TooManyAttempts

### Community 261 - "OtpService"
Cohesion: 0.50
Nodes (3): IFusionCache, IOptions, OtpService

### Community 262 - "Request"
Cohesion: 0.13
Nodes (14): IAM.Domain.Users, Constants, Guid, Request, ClientId, DeviceId, DeviceName, Email (+6 more)

### Community 265 - "Common.Application.Validation"
Cohesion: 0.15
Nodes (12): Common.Application.Validation, AuditLogOptions, PurgeBatchSize, RetentionDays, AuditLogOptionsValidator, ModulesOptions, EnabledModules, ModulesOptionsValidator (+4 more)

### Community 267 - "SecurityHeadersOptions"
Cohesion: 0.50
Nodes (4): SecurityHeadersOptions, Headers, SecurityHeadersOptionsValidator, Dictionary

### Community 268 - ".CreateAsync"
Cohesion: 0.50
Nodes (3): Success, Func, Task

### Community 270 - "DefaultResponsesOperationFilter"
Cohesion: 0.70
Nodes (3): OpenApiOperation, OperationFilterContext, DefaultResponsesOperationFilter

### Community 275 - ".AddCustomSwagger"
Cohesion: 0.50
Nodes (3): IConfigureOptions, IServiceCollection, SwaggerGenOptions

### Community 277 - "SendResponseBody"
Cohesion: 0.50
Nodes (4): SendResponseBody, Code, Description, JobId

## Knowledge Gaps
- **764 isolated node(s):** `Id`, `IdAsString`, `Roles`, `SessionId`, `JwtClaimNames` (+759 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1706 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **102 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `Common.InterModuleRequests.Contracts`, `NotificationPayload`, `DeviceRegistryReconciliationService`, `OtpVerificationOutcome`, `Request`, `Common.Application.Validation`, `SecurityHeadersOptions`, `Common.Domain.ResultMonad`, `OutboxOptions`, `ISearchLanguageResolver`, `Request`, `KeycloakTokenClient.cs`, `ObservabilityOptions`, `Policies.CreateStore.cs`, `KeycloakOptions`, `ResxLocalizationOptions`, `ProductsModule.cs`, `OtpOptions`, `IntegrationEventHandlerBase`, `SmsOptions`, `RabbitMqOptions`, `NotificationsModule.cs`, `SignalROptions`, `ReverseProxyOptions`, `RequestLoggingOptions`, `CorsOptions`, `Common.Application.BackgroundJobs`, `OpenApiOptions`, `Request`, `IntegrationEventOutbox`, `Notifications.Infrastructure.Otp`, `PushOptions`, `IInterModuleRequestClient`, `Common.Domain.StronglyTypedIds`, `FirebasePushGateway.cs`, `BackgroundJobsOptions`, `AuditLogRetentionService`, `CustomValidator`, `ResiliencyOptions`, `.AddCommonOptions`, `OutboxModule.cs`, `CaptchaOptions`, `Host.Swagger`, `FullTextSearchOptions`, `HealthCheckOptions`?**
  _High betweenness centrality (0.275) - this node is a cross-community bridge._
- **Why does `Result` connect `Result` to `Response`, `.SaveChangesAsync`, `.ListSessions`, `PersistenceQueryableExtensions`, `FirebasePushGateway`, `Error`, `ProductTemplate`, `.CreateAsync`, `VerifyPhoneOtpResponse`, `.SendOtp`, `.GetMeAsync`, `Response`, `.RegisterAsync`, `ISmsGateway`, `Response`, `IKeycloakAdminClient`, `DeactivateDeviceSessionsRequest`, `NetGsmSmsGateway`, `.CreateStoreAsync`, `.AddProductAsync`, `.UpdateMyStoreAsync`, `.RefreshToken`, `ICaptchaService`, `.UpdateStoreAsync`, `.CreateTokens`, `PaginationResponse`, `Response`, `ReCaptchaService`, `DummySmsGateway`, `.SearchProductTemplatesAsync`, `.AddProductToMyStoreAsync`, `.BindDeviceAsync`, `AuditableEntityResponse`, `.AddPushServices`, `Response`, `.DeactivateProductTemplateAsync`, `IProductsDbContext`, `.SearchStoresAsync`, `StringExtensions`, `.CreateProductTemplateAsync`, `ResultTelemetryExtensions`, `IInterModuleRequestClient`, `PushMessage`, `Response`, `.SearchMyProductsAsync`, `.SingleAsResult`, `.SearchStoreProductsAsync`, `IKeycloakTokenClient`, `.UpdateCurrentPushToken`, `.IsRegisteredAsync`, `.RemoveProductAsync`, `.UpdateMyProductAsync`, `.RemoveMyProductAsync`, `.TapWhenFeatureEnabledAsync`?**
  _High betweenness centrality (0.106) - this node is a cross-community bridge._
- **Why does `ApplicationUserId` connect `ApplicationUserId` to `NotificationPayload`, `DeviceRegistryReconciliationService`, `KeycloakAdminClient`, `.RegisterAsync`, `Response`, `KeycloakPermissionAuthorizationHandler`, `Response`, `AuditLogEntry`, `IKeycloakAdminClient`, `DeviceRegistration`, `DeactivateDeviceSessionsRequest`, `KeycloakTokenClient`, `IAuditableEntity`, `For`, `ICurrentUser`, `.CreateTokens`, `IInterModuleRequest`, `PaginationResponse`, `SendSecurityAlertRequestHandler`, `Response`, `Request`, `AuditableEntityResponse`, `.BindDeviceAsync`, `V1StoreCreatedDomainEvent`, `IStronglyTypedId`, `IProductsDbContext`, `.SearchStoresAsync`, `.Configure`, `Store`, `Response`, `.HandleAsync`, `IntegrationEvent`, `.RequestTokensAsync`, `.TryDeserialize`, `KeycloakUser`, `Response`, `.LogRoleAssignmentFailed`, `StoreId`, `Request`, `VersionNeutral/Get/Request.cs`?**
  _High betweenness centrality (0.101) - this node is a cross-community bridge._
- **What connects `Id`, `IdAsString`, `Roles` to the rest of the system?**
  _764 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Common.InterModuleRequests.Contracts` be split into smaller, more focused modules?**
  _Cohesion score 0.09672830725462304 - nodes in this community are weakly interconnected._
- **Should `NotificationPayload` be split into smaller, more focused modules?**
  _Cohesion score 0.052464947987336044 - nodes in this community are weakly interconnected._
- **Should `DeviceRegistryReconciliationService` be split into smaller, more focused modules?**
  _Cohesion score 0.07560975609756097 - nodes in this community are weakly interconnected._