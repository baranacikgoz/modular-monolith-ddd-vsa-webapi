# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-10-23)

## Corpus Check
- 587 files · ~95,426 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 3846 nodes · 10193 edges · 273 communities (157 shown, 116 thin omitted)
- Extraction: 97% EXTRACTED · 3% INFERRED · 0% AMBIGUOUS · INFERRED: 332 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `39b8557a`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- Result
- NotificationPayload
- OutboxOptions
- AuditableEntityResponse
- ApplicationUserId
- StoreId
- ApplySearchLanguageInterceptor
- IDbContext
- Common.Infrastructure.Extensions
- Error
- Common.Domain.ResultMonad
- DomainEvent
- ISearchLanguageResolver
- DummyCaptchaService.cs
- KeycloakPermissionAuthorizationHandler
- EmailOptions
- Product
- Common.Application.Options
- Common.Domain.StronglyTypedIds
- FullTextSearchOptions
- BoundedRequestCaptureStream
- DeviceRegistration
- DateTimeOffset
- .RegisterAsync
- Stores/v1/My/Update/Request.cs
- Response
- RequestResponseBodyLoggingMiddleware
- fluentvalidation
- Func
- ProjectionUpsertOutcome
- IModule
- ILogger
- Response
- Products.Domain.Stores
- OutboxMessage
- CustomRateLimitingOptions
- AdminRepresentations.cs
- ProblemDetailsExtensions.cs
- ObservabilityOptions
- ConfigureSwaggerOptions
- OtpOptions
- S3ObjectStoreCore.cs
- ICaptchaService
- KeycloakOptions
- StockReservationExpirySweepJobRegistrar
- PaginationQueryableExtensions
- Outbox Misuse Check
- InventoryModule.cs
- Add Integration Event Command
- system_text_json
- BackgroundJobsOptions
- CaptchaOptions
- AuditLogRetentionService
- V1StoreCreatedDomainEvent
- BrevoEmailGateway
- Infrastructure/StringExtensions.cs
- Cross-Module Reference Violation
- ProductsDbContext
- Request
- Bogus Test Data
- SendSecurityAlertRequestHandler
- StronglyTypedIdBinder
- RequestLoggingOptions
- Func
- OutboxOptions
- .AddNotificationsSignalR
- ValueObject
- Request
- RequestBody
- Full-Text Search
- ValidationContextExtensions
- VerifyPhoneOtpResponse
- AsNoTracking Coverage Check
- Request
- Response
- IBackgroundUserContext
- IntegrationEventOutbox
- DeviceRegistryReconciliationService
- FirebaseServiceAccountOptions
- Response
- Split-Deployment PoC
- IOptions
- EntityTypeBuilder
- StockReservation
- BaseDbContext
- PushOptions
- Configuration-Driven Module Registration
- KeycloakPermission
- Setup
- SendRequestBody
- Common.Domain.Entities
- .MapEndpoint
- SendRequestBody
- Response
- LoggerMessage
- How it works
- Response
- InterModuleRequestOptions
- ReCaptchaService
- ResiliencyOptions
- ReCaptchaResponse
- Response
- ResultToResponseTransformer.cs
- Response
- Common.InterModuleRequests.Contracts
- InventoryOptions
- StockLevel
- Response
- KeycloakPermissionPolicyProvider
- VerifyEmail/Request.cs
- Setup
- Localization/Setup.cs
- Response
- SkipOverlappingRecurringJobFilter
- IResxLocalizer
- Response
- DatabaseOptions.cs
- .ForUser
- ICoalescableIntegrationEvent.cs
- HealthCheckOptions
- InterModuleRequestHandlerDefinition
- NotificationsHub
- Response
- Response
- docker-compose.yml (Base Stack)
- IServiceCollection
- ObjectStorageOptions
- Seeder
- InterModuleRequestHandler
- IWebHostEnvironment
- DefaultIdType
- Type
- Setup
- ActivitySource
- OutboxOptions.cs
- .EnsureNoMigrationsPending
- For
- CurrentUser
- PaginationRequest
- Notifications.Application/IAssemblyReference.cs
- Common.Domain.Events
- Task
- system_diagnostics
- PaginationResponse
- CorsOptions
- CachingOptions
- SmsOptions
- RabbitMqOptions
- IAM.Domain
- Response
- ReverseProxyOptions
- BoundedCaptureStream
- .GetMeAsync
- JobRow
- Response
- NotificationsTelemetry
- KeycloakScopes
- DevicesOptions
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- Key decisions
- Response
- FeatureFlags
- Keycloak realm as code
- KeycloakPermissionAuthorizationHandler.cs
- NetGsmSmsGateway
- RequestBodyLimitFilter.cs
- WebhookCallback/Request.cs
- ResxLocalizationOptions
- Response
- RequestBody
- microsoft_entityframeworkcore
- Response
- Products.Domain/IAssemblyReference.cs
- IAM.Application/IAssemblyReference.cs
- Products.Infrastructure/IAssemblyReference.cs
- Products.Application/IAssemblyReference.cs
- Common.Infrastructure/IAssemblyReference.cs
- Notifications.Domain/IAssemblyReference.cs
- IAM.Infrastructure/IAssemblyReference.cs
- Response
- S3ObjectStoreCore
- IEnumerable
- RouteGroupBuilder
- CustomValidator
- Response
- SendForEmail/Request.cs
- Request
- Request
- AuditLogOptions
- ProjectionReconciliationOptions.cs
- ResultTelemetryExtensions
- SignalROptions
- microsoft_aspnetcore_mvc
- JobHousekeepingOptions
- Products.Endpoints
- Products/v1/Update/Request.cs
- Notifications.Infrastructure
- .Capture
- v1/Request.cs
- Products/v1/My/Update/Request.cs
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
- Update Dependencies Command
- Central Package Management
- Verify Feature Command
- InterModuleRequests (Sync Cross-Module)
- BackgroundJobs Module
- Outbox Module
- Project Instructions (CLAUDE.md)
- REPR Pattern (Minimal API Endpoints)

## God Nodes (most connected - your core abstractions)
1. `Result` - 147 edges
2. `Common.Application.Options` - 139 edges
3. `Common.Domain.ResultMonad` - 105 edges
4. `CustomValidator` - 83 edges
5. `ApplicationUserId` - 77 edges
6. `Common.Application.Validation` - 74 edges
7. `Common.Domain.StronglyTypedIds` - 69 edges
8. `Common.Application.Auth` - 68 edges
9. `Common.Application.Extensions` - 62 edges
10. `Common.InterModuleRequests.Contracts` - 55 edges

## Surprising Connections (you probably didn't know these)
- `Concurrent safety` --references--> `OutboxProcessor`  [INFERRED]
  docs/split-deployment-poc.md → src/Modules/Outbox/Outbox/OutboxProcessor.cs
- `Add search to a new entity _(Build checklist)_` --references--> `FullTextSearchOptions`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Application/Options/FullTextSearchOptions.cs
- `Configuration` --references--> `FullTextSearchOptions`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Application/Options/FullTextSearchOptions.cs
- `Full-Text Search` --references--> `FullTextSearchOptions`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Application/Options/FullTextSearchOptions.cs
- `Gotchas` --references--> `FullTextSearchOptions`  [INFERRED]
  docs/full-text-search.md → src/Common/Common.Application/Options/FullTextSearchOptions.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Local Infrastructure Stack** — docker_compose_postgres, docker_compose_rabbitmq, docker_compose_redis, docker_compose_aspire_dashboard [EXTRACTED 1.00]

## Communities (273 total, 116 thin omitted)

### Community 0 - "Result"
Cohesion: 0.06
Nodes (20): ICurrentUser, Id, IdAsString, Roles, SessionId, Result, Error, IsFailure (+12 more)

### Community 1 - "NotificationPayload"
Cohesion: 0.20
Nodes (5): Notifications.Application.Hubs, INotificationDispatcher, INotificationsClient, NotificationPayload, SignalRNotificationDispatcher

### Community 2 - "OutboxOptions"
Cohesion: 0.17
Nodes (5): OutboxProcessor, PublishOutcome, Failed, Published, Skipped

### Community 3 - "AuditableEntityResponse"
Cohesion: 0.08
Nodes (22): AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, Response, AvailableQuantity (+14 more)

### Community 5 - "ApplicationUserId"
Cohesion: 0.08
Nodes (24): ApplicationUserId, IsEmpty, Value, HttpClientKeyedExtensions, GetActiveSessionIdsRequest, GetActiveSessionIdsResponse, UserSessionIds, GrantedPermission (+16 more)

### Community 6 - "StoreId"
Cohesion: 0.07
Nodes (18): StronglyTypedIdHelper, ProductCreatedIntegrationEventPublishingHandler, V1ProductCreatedDomainEventHandlers, V1ProductCreatedDomainEvent, ProductTemplateId, ProductSnapshot, V1ProductAddedToStoreDomainEvent, V1ProductAddedToStoreDomainEventExtensions (+10 more)

### Community 7 - "ApplySearchLanguageInterceptor"
Cohesion: 0.25
Nodes (6): Add a new language/culture, Add search to a new entity _(Build checklist)_, Change the vector (weights, fields, or config), Extending and maintaining, ApplySearchLanguageInterceptor, Setup

### Community 8 - "IDbContext"
Cohesion: 0.20
Nodes (5): JobHousekeepingJob, IDbContext, AuditLog, ChangeTracker, Database

### Community 9 - "Common.Infrastructure.Extensions"
Cohesion: 0.07
Nodes (17): Common.Infrastructure.Modules, Products.Endpoints.Probe, Host, Common.Infrastructure.RateLimiting, Products.Infrastructure.RateLimiting, Products.Endpoints.Products, Common.Infrastructure.Extensions, IAM.Endpoints.Otp.VersionNeutral (+9 more)

### Community 10 - "Error"
Cohesion: 0.07
Nodes (22): StringLocalizerExtensions, StringExtensions, Error, Key, ParameterName, StatusCode, SubErrors, Value (+14 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.10
Nodes (22): IAM.Endpoints.Captcha.VersionNeutral, Common.Application.Search, Inventory.Infrastructure.InterModuleRequestHandlers, Inventory.Application.Persistence, Common.Infrastructure.Persistence.Extensions, Common.Application.Extensions, Products.Endpoints.ProductTemplates, Common.Application.EndpointFilters (+14 more)

### Community 12 - "DomainEvent"
Cohesion: 0.10
Nodes (19): AggregateRoot, Events, Id, Version, IAggregateRoot, Events, Id, Version (+11 more)

### Community 13 - "ISearchLanguageResolver"
Cohesion: 0.24
Nodes (6): Configuration, ISearchLanguageResolver, UniversalConfig, SearchLanguageResolver, UniversalConfig, Setup

### Community 16 - "EmailOptions"
Cohesion: 0.10
Nodes (23): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+15 more)

### Community 17 - "Product"
Cohesion: 0.06
Nodes (31): Products.Domain.Stores.DomainEvents.v1, File map _(Build)_, ISearchLocalized, Language, Product, Description, Language, Name (+23 more)

### Community 18 - "Common.Application.Options"
Cohesion: 0.05
Nodes (31): Notifications.Application.Otp, Notifications.Application.Sms, Notifications.Infrastructure.Devices, Common.Application.Persistence.Inbox, IAM.Infrastructure.Keycloak.Representations, Notifications.Infrastructure.Email, Inventory.Application.IntegrationEventHandlers, Common.IntegrationEvents (+23 more)

### Community 19 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.08
Nodes (18): Common.Domain.StronglyTypedIds, Inventory.Infrastructure.Persistence.EntityConfigurations, Common.Application.Jobs, Common.Application.JsonConverters, Common.Infrastructure.Persistence.EntityConfigurations, Common.Infrastructure.Persistence.ValueConverters, Notifications.Infrastructure.Persistence.EntityConfigurations, Products.Infrastructure.Persistence.EntityConfigurations (+10 more)

### Community 20 - "FullTextSearchOptions"
Cohesion: 0.29
Nodes (7): Gotchas, FullTextSearchOptions, CultureToConfig, DefaultConfig, RankWeights, UseUnaccent, FullTextSearchOptionsValidator

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.14
Nodes (6): BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 22 - "DeviceRegistration"
Cohesion: 0.15
Nodes (10): DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive, LastReconciledOn, PushToken, PushTokenUpdatedOn (+2 more)

### Community 23 - "DateTimeOffset"
Cohesion: 0.12
Nodes (8): UtcDateTimeOffsetConverter, ServiceAccountTokenCache, AccessToken, ExpiresAt, RequestBody, ProductId, Quantity, ReservationDeadline

### Community 24 - ".RegisterAsync"
Cohesion: 0.10
Nodes (18): IInterModuleRequestClient, MassTransitInterModuleRequestClient, BindDeviceSessionRequest, BindDeviceSessionResponse, DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse, VerifyEmailOtpRequest, VerifyEmailOtpResponse (+10 more)

### Community 25 - "Stores/v1/My/Update/Request.cs"
Cohesion: 0.33
Nodes (6): Products.Endpoints.Stores.v1.My.Update, Request, Address, Description, Name, RequestValidator

### Community 26 - "Response"
Cohesion: 0.29
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, Endpoint, Response, EmailVerificationToken, IsRegistered

### Community 28 - "fluentvalidation"
Cohesion: 0.10
Nodes (24): IAM.Domain.Users, IAM.Endpoints.Otp.VersionNeutral.SendForLogin, Common.Domain.Extensions, Notifications.Infrastructure.Devices.UpdateCurrentPushToken, Common.Application.Validation, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, Common.Domain.Devices, IAM.Endpoints.Otp.VersionNeutral.SendForRegistration (+16 more)

### Community 29 - "Func"
Cohesion: 0.11
Nodes (18): HttpContextExtensions, RateLimitPartitions, CheckRegistrationRateLimitingPolicy, OnRejected, EmailRateLimitingPolicy, OnRejected, OtpVerifyRateLimitingPolicy, OnRejected (+10 more)

### Community 30 - "ProjectionUpsertOutcome"
Cohesion: 0.20
Nodes (7): Common.Application.Persistence.Projections, ProjectionEntity, SourceVersion, ProjectionUpsertOutcome, Inserted, Stale, Updated

### Community 31 - "IModule"
Cohesion: 0.06
Nodes (29): IModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority, Setup, IamModule (+21 more)

### Community 32 - "ILogger"
Cohesion: 0.13
Nodes (6): ProjectionReconciliationJob, OutboxSaveHelper, IPushGateway, DummyPushGateway, FirebasePushGateway, DummySmsGateway

### Community 33 - "Response"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.Refresh, Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 34 - "Products.Domain.Stores"
Cohesion: 0.05
Nodes (34): Products.Endpoints.Stores.v1.Create, Products.Endpoints.Stores.v1.Search, Products.Endpoints.Stores.v1.My.AddProduct, Products.Endpoints.Products.v1.My.Get, Products.Endpoints.ProductTemplates.v1.Search, Products.Endpoints.Products.v1.Search, Products.Domain.Products, Common.Application.DTOs (+26 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.09
Nodes (20): IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, OutboxMessage, CreatedOn (+12 more)

### Community 36 - "CustomRateLimitingOptions"
Cohesion: 0.07
Nodes (19): CustomRateLimitingOptions, CheckRegistration, CreateStore, Email, ExemptPathPrefixes, Global, OtpVerify, Register (+11 more)

### Community 37 - "AdminRepresentations.cs"
Cohesion: 0.12
Nodes (16): CredentialRepresentation, Temporary, Type, Value, ErrorRepresentation, Error, ErrorMessage, Field (+8 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.08
Nodes (17): ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics, EnableTracing, LogSink, MinimumLevel (+9 more)

### Community 40 - "ConfigureSwaggerOptions"
Cohesion: 0.07
Nodes (15): Host.Swagger, OpenApiOptions, ContactEmail, ContactName, Description, EnableSwagger, LicenseName, LicenseUrl (+7 more)

### Community 41 - "OtpOptions"
Cohesion: 0.06
Nodes (31): OtpCacheEntry, OtpOptions, DummyCode, EmailQuotaWindowMinutes, ExpirationInMinutes, Length, MaxSendsPerEmailPerWindow, MaxSendsPerPhonePerWindow (+23 more)

### Community 43 - "ICaptchaService"
Cohesion: 0.20
Nodes (5): SendPhoneOtpRequest, SendPhoneOtpResponse, ICaptchaService, CachedCaptchaService, DummyCaptchaService

### Community 44 - "KeycloakOptions"
Cohesion: 0.12
Nodes (15): KeycloakOptions, AttemptTimeoutSeconds, Authority, BaseUrl, DecisionCacheMaxDurationSeconds, Realm, RequireHttpsMetadata, ResourceClientId (+7 more)

### Community 45 - "StockReservationExpirySweepJobRegistrar"
Cohesion: 0.31
Nodes (3): Setup, StockReservationExpirySweepJobRegistrar, StockReservationExpirySweepService

### Community 46 - "PaginationQueryableExtensions"
Cohesion: 0.08
Nodes (13): PaginationCursor, Payload, TaggedValue, CursorBound, Value, KeyedRow, Item, Sort (+5 more)

### Community 48 - "InventoryModule.cs"
Cohesion: 0.17
Nodes (5): Inventory.Endpoints, Common.Endpoints.Versioning, Inventory.Infrastructure.Gateway, Inventory.Endpoints.StockReservations, Inventory.Application.Gateway

### Community 50 - "system_text_json"
Cohesion: 0.15
Nodes (3): Notifications.Application.Push, Common.Application.AuditLog, Notifications.Infrastructure.Push.Firebase

### Community 51 - "BackgroundJobsOptions"
Cohesion: 0.15
Nodes (8): BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount, BackgroundJobsOptionsValidator, HangfireCustomAuthorizationFilter

### Community 52 - "CaptchaOptions"
Cohesion: 0.16
Nodes (13): CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey, Provider, ScoreThreshold, SecretKey (+5 more)

### Community 53 - "AuditLogRetentionService"
Cohesion: 0.24
Nodes (3): AuditLogRetentionJobRegistrar, AuditLogRetentionService, Setup

### Community 54 - "V1StoreCreatedDomainEvent"
Cohesion: 0.15
Nodes (9): Products.Application.Stores.DomainEventHandlers.v1, DomainEventHandlerBase, IEventHandler, IEventHandlerWrapper, IIntegrationEventOutbox, SimulateSomeBusinessHandler, StoreCreatedIntegrationEventPublishingHandler, V1StoreCreatedDomainEventHandlers (+1 more)

### Community 55 - "BrevoEmailGateway"
Cohesion: 0.15
Nodes (9): BrevoEmailGateway, SendContact, Email, Name, SendErrorBody, Code, Message, SendResponseBody (+1 more)

### Community 58 - "ProductsDbContext"
Cohesion: 0.10
Nodes (15): IDatabaseSeeder, Priority, EventDispatcher, Setup, InventoryDbContext, StockLevels, StockReservations, Setup (+7 more)

### Community 59 - "Request"
Cohesion: 0.15
Nodes (13): Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken (+5 more)

### Community 61 - "SendSecurityAlertRequestHandler"
Cohesion: 0.52
Nodes (5): SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, SendSecurityAlertRequestHandler

### Community 63 - "RequestLoggingOptions"
Cohesion: 0.13
Nodes (15): RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes, SensitiveQueryParamPaths (+7 more)

### Community 65 - "Func"
Cohesion: 0.10
Nodes (7): IBackgroundJobs, IRecurringBackgroundJobs, FeatureFlagResultExtensions, AsyncExtensions, SyncExtensions, BackgroundJobsService, RecurringBackgroundJobsService

### Community 66 - "OutboxOptions"
Cohesion: 0.07
Nodes (22): OutboxOptions, BaseBackoffSeconds, BatchSize, ClaimLeaseSeconds, Cleanup, IsProcessor, LagThresholdMinutes, MaxBackoffSeconds (+14 more)

### Community 69 - "Request"
Cohesion: 0.17
Nodes (12): Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FirstName, LastName (+4 more)

### Community 70 - "RequestBody"
Cohesion: 0.18
Nodes (10): RequestBody, FirstDeadline, IntervalDays, OccurrenceCount, ProductId, QuantityPerOccurrence, Warehouses, WarehouseAllocation (+2 more)

### Community 71 - "Full-Text Search"
Cohesion: 0.33
Nodes (6): Full-Text Search, Non-goals, Per-entity strategy, Testing, The problem, TL;DR: what the feature gives you

### Community 73 - "VerifyPhoneOtpResponse"
Cohesion: 0.24
Nodes (8): OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, VerifyPhoneOtpResponseExtensions, VerifyPhoneOtpRequestHandler

### Community 75 - "Request"
Cohesion: 0.22
Nodes (9): Products.Endpoints.Products.v1.My.Search, Request, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name, SearchTerm (+1 more)

### Community 76 - "Response"
Cohesion: 0.25
Nodes (7): Endpoint, Response, Address, Description, Name, OwnerId, ProductCount

### Community 77 - "IBackgroundUserContext"
Cohesion: 0.28
Nodes (5): IBackgroundUserContext, UserId, BackgroundUserContext, UserId, Setup

### Community 78 - "IntegrationEventOutbox"
Cohesion: 0.31
Nodes (5): IntegrationEventOutbox, HasPending, IntegrationEvent, CreatedOn, Id

### Community 79 - "DeviceRegistryReconciliationService"
Cohesion: 0.31
Nodes (3): DeviceRegistryReconcileJobRegistrar, DeviceRegistryReconciliationService, Setup

### Community 80 - "FirebaseServiceAccountOptions"
Cohesion: 0.25
Nodes (8): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri, Type

### Community 81 - "Response"
Cohesion: 0.25
Nodes (7): Endpoint, Response, Address, Description, Name, OwnerId, ProductCount

### Community 82 - "Split-Deployment PoC"
Cohesion: 0.25
Nodes (6): Concurrent safety, Files added by this PoC, How to run, Split-Deployment PoC, What this proves, README (Boilerplate Overview)

### Community 83 - "IOptions"
Cohesion: 0.20
Nodes (10): CreateStockLevelOnProductCreatedHandler, IEmailGateway, ISmsGateway, DummyEmailGateway, Setup, ThrottledEmailGateway, SendPhoneOtpRequestHandler, OtpService (+2 more)

### Community 84 - "EntityTypeBuilder"
Cohesion: 0.18
Nodes (4): TrigramIndexExtensions, DomainEventConverter, IntegrationEventConverter, StronglyTypedIdValueConverter

### Community 85 - "StockReservation"
Cohesion: 0.07
Nodes (22): Inventory.Endpoints.StockReservations.v1.ReserveSeries, Inventory.Domain.StockReservations.DomainEvents.v1, Inventory.Domain.StockReservations.Errors, V1StockReservationCommitConflictDetectedDomainEvent, V1StockReservationCommittedDomainEvent, V1StockReservationExpiredDomainEvent, V1StockReservationReleaseAttemptAbandonedDomainEvent, V1StockReservationReleaseAttemptStartedDomainEvent (+14 more)

### Community 86 - "BaseDbContext"
Cohesion: 0.12
Nodes (15): IdBound, Value, JobClaimExtensions, ProjectionUpsertExtensions, BaseDbContext, AuditLog, IOutboxDbContext, OutboxMessages (+7 more)

### Community 87 - "PushOptions"
Cohesion: 0.21
Nodes (12): PushOptions, Provider, SendTimeoutSeconds, ServiceAccount, Templates, PushOptionsValidator, PushProvider, Dummy (+4 more)

### Community 89 - "KeycloakPermission"
Cohesion: 0.22
Nodes (4): KeycloakPermission, RouteHandlerBuilderExtensions, KeycloakPermissionRequirement, Permission

### Community 91 - "SendRequestBody"
Cohesion: 0.29
Nodes (6): SendRequestBody, HtmlContent, Sender, Subject, TextContent, To

### Community 92 - "Common.Domain.Entities"
Cohesion: 0.07
Nodes (22): Common.Domain.Entities, Common.Domain.Aggregates, AuditableEntity, AuditableEntity, Id, CreatedBy, CreatedOn, LastModifiedBy (+14 more)

### Community 93 - ".MapEndpoint"
Cohesion: 0.29
Nodes (4): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, Response, ClientKey, Setup

### Community 94 - "SendRequestBody"
Cohesion: 0.29
Nodes (6): SendRequestBody, AppName, Encoding, IysFilter, Messages, MsgHeader

### Community 95 - "Response"
Cohesion: 0.29
Nodes (6): Endpoint, Response, Description, Name, Price, Quantity

### Community 96 - "LoggerMessage"
Cohesion: 0.11
Nodes (6): DatabaseSeederOrchestrator, SeedingCompletionTracker, Setup, GlobalExceptionHandlingMiddleware, LoginCompletion, RegistrationCompletion

### Community 97 - "How it works"
Cohesion: 0.40
Nodes (5): How it works, One-time setup _(Build, in a migration)_, Query path, Ranking, Write path

### Community 98 - "Response"
Cohesion: 0.29
Nodes (6): Endpoint, Response, Description, Name, Price, Quantity

### Community 99 - "InterModuleRequestOptions"
Cohesion: 0.25
Nodes (7): InterModuleRequestOptions, DependencyUnavailableRetryAfterSeconds, HandlerConcurrentMessageLimit, HandlerPrefetchCount, Timeouts, TimeoutSeconds, InterModuleRequestOptionsValidator

### Community 101 - "ResiliencyOptions"
Cohesion: 0.05
Nodes (30): KeyedResilienceProfile, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, RateLimitFailOpen, RateLimitPermits (+22 more)

### Community 102 - "ReCaptchaResponse"
Cohesion: 0.29
Nodes (6): ReCaptchaResponse, ChallengeTs, ErrorCodes, Hostname, Score, Success

### Community 103 - "Response"
Cohesion: 0.29
Nodes (6): Endpoint, Response, Description, Name, Price, Quantity

### Community 104 - "ResultToResponseTransformer.cs"
Cohesion: 0.14
Nodes (6): ProblemResponse, ResultToAcceptedResponseTransformer, ResultToCreatedResponseTransformer, ResultToResponseTransformer, ResultToResponseTransformer, RouteHandlerBuilderExtensions

### Community 105 - "Response"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegisterByEmail, Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 106 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.08
Nodes (17): IAM.Endpoints.Users, Common.Application.FeatureManagement, IAM.Endpoints.Otp, Common.InterModuleRequests.IAM, Common.InterModuleRequests.Contracts, IAM.Application.Captcha.Services, IAM.Infrastructure.Telemetry, IAM.Infrastructure.InterModuleRequestHandlers (+9 more)

### Community 107 - "InventoryOptions"
Cohesion: 0.12
Nodes (19): InventoryOptions, MaxOccurrencesPerSeries, SafetyBufferQuantity, StockReservationExpirySweepBatchSize, StockReservationExpirySweepCron, WarehouseGatewayBaseUrl, WarehouseGatewayProvider, WebhookMaxBodyBytes (+11 more)

### Community 108 - "StockLevel"
Cohesion: 0.21
Nodes (7): Inventory.Domain.StockLevels.DomainEvents.v1, V1StockLevelCreatedDomainEvent, StockLevel, ProductId, QuantityOnHand, StockLevelId, StockLevelConfiguration

### Community 109 - "Response"
Cohesion: 0.33
Nodes (4): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, Endpoint, Response, IsRegistered

### Community 111 - "VerifyEmail/Request.cs"
Cohesion: 0.50
Nodes (4): Request, Email, Otp, RequestValidator

### Community 114 - "Response"
Cohesion: 0.05
Nodes (37): IAM.Endpoints.Users.VersionNeutral.Search, IAM.Endpoints.Users.VersionNeutral.Get, IAM.Endpoints.Users.VersionNeutral.Me.Get, Endpoint, Response, BirthDate, CreatedOn, Email (+29 more)

### Community 116 - "IResxLocalizer"
Cohesion: 0.18
Nodes (3): ProblemDetailsServiceExtensions, JwtBearerConfigureOptions, Setup

### Community 117 - "Response"
Cohesion: 0.11
Nodes (15): Inventory.Endpoints.StockReservations.v1.Get, ReservationStatus, Active, Committed, Expired, Released, RequiresReconciliation, Endpoint (+7 more)

### Community 118 - "DatabaseOptions.cs"
Cohesion: 0.67
Nodes (3): DatabaseOptions, ConnectionString, DatabaseOptionsValidator

### Community 124 - "HealthCheckOptions"
Cohesion: 0.40
Nodes (5): HealthCheckOptions, EnableHealthChecks, ReadinessTimeoutInSeconds, StartupTimeoutInSeconds, HealthCheckOptionsValidator

### Community 125 - "InterModuleRequestHandlerDefinition"
Cohesion: 0.14
Nodes (6): ConcurrencyConfiguratorExtensions, OtpDispatchErrors, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled

### Community 127 - "Response"
Cohesion: 0.25
Nodes (7): Endpoint, Response, Address, Description, Name, OwnerId, ProductCount

### Community 129 - "Response"
Cohesion: 0.29
Nodes (4): Products.Endpoints.ProductTemplates.v1.Create, Setup, Response, Id

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.48
Nodes (7): Observability (OpenTelemetry), docker-compose.yml (Base Stack), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), Postgres Service (mm.postgres), RabbitMQ Service (mm.rabbitmq), Redis Service (mm.redis)

### Community 131 - "IServiceCollection"
Cohesion: 0.10
Nodes (7): Setup, Setup, Setup, Setup, Setup, Setup, Setup

### Community 132 - "ObjectStorageOptions"
Cohesion: 0.06
Nodes (28): ObjectStorageOptions, AccessKey, AdditionalTransientStatusCodes, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds (+20 more)

### Community 135 - "Seeder"
Cohesion: 0.32
Nodes (4): GetSeedUserIdsRequest, GetSeedUserIdsResponse, GetSeedUserIdsRequestHandler, Seeder

### Community 136 - "InterModuleRequestHandler"
Cohesion: 0.12
Nodes (14): IInterModuleRequest, IInterModuleRequestHandler, InterModuleRequestHandler, GetStockLevelRequest, GetStockLevelResponse, DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse (+6 more)

### Community 140 - "DefaultIdType"
Cohesion: 0.09
Nodes (17): IntegrationEventHandlerBase, MaxEventAge, IInboxCleanupTarget, ModuleName, IInboxStore, ProcessedMessage, ConsumerName, MessageId (+9 more)

### Community 141 - "Type"
Cohesion: 0.13
Nodes (8): StrictDateTimeOffsetJsonConverter, StronglyTypedIdListReadOnlyJsonConverter, StronglyTypedIdReadOnlyJsonConverter, StronglyTypedIdReadOnlyJsonConverter, StronglyTypedIdWriteOnlyJsonConverter, IStronglyTypedId, Value, PolymorphicEventConverter

### Community 146 - "ActivitySource"
Cohesion: 0.18
Nodes (7): RequireFeatureFilter, RouteHandlerBuilderExtensions, BackgroundJobsTelemetry, IamTelemetry, InventoryTelemetry, OutboxTelemetry, ProductsTelemetry

### Community 151 - "OutboxOptions.cs"
Cohesion: 0.32
Nodes (7): OutboxCleanupSettings, BatchSize, CronSchedule, Enabled, RetentionDays, OutboxCleanupSettingsValidator, OutboxOptionsValidator

### Community 152 - ".EnsureNoMigrationsPending"
Cohesion: 0.39
Nodes (3): AutoMigrateMarker, IAutoMigrateMarker, MigrationGuard

### Community 165 - "CurrentUser"
Cohesion: 0.15
Nodes (9): CurrentUser, BackgroundUserIdAsString, Id, IdAsString, IsAuthenticated, Principal, Roles, SessionId (+1 more)

### Community 167 - "PaginationRequest"
Cohesion: 0.05
Nodes (46): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Stores.v1.My.AuditLog, Common.Application.Pagination, Products.Endpoints.Products.v1.AuditLog, PaginationRequest, After, IncludeTotal, PageNumber (+38 more)

### Community 172 - "Common.Domain.Events"
Cohesion: 0.09
Nodes (18): Products.Domain.Products.DomainEvents.v1, Common.Domain.Events, Common.Application.Persistence.Outbox, Products.Application.Products.DomainEventHandlers.v1, UnknownDomainEvent, V1ProductDescriptionUpdatedDomainEvent, V1ProductNameUpdatedDomainEvent, V1ProductPriceDecreasedDomainEvent (+10 more)

### Community 173 - "Task"
Cohesion: 0.07
Nodes (5): IVariantFeatureManagerExtensions, IInboxStore, HttpContentJsonExtensions, EmailMessage, SmsMessage

### Community 174 - "system_diagnostics"
Cohesion: 0.11
Nodes (5): Common.Application.BackgroundJobs, BackgroundJobs.Telemetry, BackgroundJobs, LoginMethods, SessionRevokedReasons

### Community 175 - "PaginationResponse"
Cohesion: 0.08
Nodes (17): AuditLogDto, PaginationResponse, HasNext, HasPrevious, HasTotal, NextPageNumber, PreviousPageNumber, TotalPages (+9 more)

### Community 177 - "CorsOptions"
Cohesion: 0.29
Nodes (7): CorsOptions, AllowCredentials, AllowedHeaders, AllowedMethods, AllowedOrigins, MaxAgeInSeconds, CorsOptionsValidator

### Community 179 - "CachingOptions"
Cohesion: 0.11
Nodes (21): CachingEntryDefaults, Duration, FactoryHardTimeout, FactorySoftTimeout, FailSafeMaxDuration, FailSafeThrottleDuration, CachingOptions, AllowInMemoryOnlyInProduction (+13 more)

### Community 180 - "SmsOptions"
Cohesion: 0.12
Nodes (18): SmsOptions, AppName, AttemptTimeoutSeconds, BaseUrl, MaxPerDay, MaxPerPhoneNumberPerDay, MaxRetryAttempts, MsgHeader (+10 more)

### Community 181 - "RabbitMqOptions"
Cohesion: 0.15
Nodes (13): RabbitMqOptions, DefaultConcurrentMessageLimit, DefaultPrefetchCount, Host, Password, Port, RetryIntervalDeltaMs, RetryLimit (+5 more)

### Community 185 - "Response"
Cohesion: 0.20
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegister, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt, Setup

### Community 187 - "ReverseProxyOptions"
Cohesion: 0.33
Nodes (5): ReverseProxyOptions, ForwardLimit, IsEnabled, TrustedNetworks, ReverseProxyOptionsValidator

### Community 188 - "BoundedCaptureStream"
Cohesion: 0.15
Nodes (7): RetryAfterHeaderExtensions, BoundedCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 190 - ".GetMeAsync"
Cohesion: 0.07
Nodes (20): IKeycloakPermissionClient, IServiceAccountTokenProvider, AccessTokenReader, KeycloakPaths, KeycloakPermissionClient, DecisionRepresentation, Result, PermissionRepresentation (+12 more)

### Community 194 - "JobRow"
Cohesion: 0.11
Nodes (13): JobRow, FailureKey, FinishedOn, Progress, QueuedOn, RequestedBy, StartedOn, Status (+5 more)

### Community 195 - "Response"
Cohesion: 0.33
Nodes (4): Inventory.Endpoints.StockReservations.v1.Reserve, Endpoint, Response, Id

### Community 201 - "KeycloakScopes"
Cohesion: 0.20
Nodes (9): Devices, Hangfire, KeycloakScopes, Products, ProductTemplates, Sessions, StockReservations, Stores (+1 more)

### Community 202 - "DevicesOptions"
Cohesion: 0.40
Nodes (5): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator

### Community 211 - "Key decisions"
Cohesion: 0.29
Nodes (7): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Key decisions

### Community 215 - "Response"
Cohesion: 0.17
Nodes (10): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, Endpoint, Response, ClientId, DeviceName, Id, IpAddress, IsCurrent (+2 more)

### Community 217 - "FeatureFlags"
Cohesion: 0.33
Nodes (5): Checkout, FeatureFlags, IAM, Notifications, Products

### Community 221 - "Keycloak realm as code"
Cohesion: 0.25
Nodes (7): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Secrets, Seed users (dev / test only)

### Community 234 - "KeycloakPermissionAuthorizationHandler.cs"
Cohesion: 0.13
Nodes (3): Common.Infrastructure.Auth.Services, IAM.Infrastructure.Auth, Common.Infrastructure.Auth

### Community 236 - "NetGsmSmsGateway"
Cohesion: 0.10
Nodes (12): SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, NetGsmSmsGateway, SendMessageBody, Msg, No (+4 more)

### Community 238 - "RequestBodyLimitFilter.cs"
Cohesion: 0.13
Nodes (7): RequestBodyLimitEndpointExtensions, RequestBodyLimitFilter, RequestBodyLimitMetadata, MaxBodyBytes, RequestBodyLimitMiddleware, EnrichLogsWithUserInfoMiddleware, SecurityHeadersMiddleware

### Community 240 - "WebhookCallback/Request.cs"
Cohesion: 0.40
Nodes (5): Inventory.Endpoints.StockReservations.v1.WebhookCallback, Request, ProviderReference, ReservationId, RequestValidator

### Community 241 - "ResxLocalizationOptions"
Cohesion: 0.50
Nodes (4): ResxLocalizationOptions, DefaultCulture, SupportedCultures, ResxLocalizationOptionsValidator

### Community 242 - "Response"
Cohesion: 0.25
Nodes (6): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 243 - "RequestBody"
Cohesion: 0.08
Nodes (29): Inventory.Endpoints.StockReservations.v1.Commit, Products.Endpoints.Stores.v1.Update, Request, Body, Id, RequestBody, ProviderReference, RequestBodyValidator (+21 more)

### Community 250 - "microsoft_entityframeworkcore"
Cohesion: 0.05
Nodes (21): Common.Infrastructure.Persistence.Inbox, Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Common.Endpoints.Webhooks, Common.InterModuleRequests, Products.Infrastructure.InterModuleRequestHandlers, Products.Infrastructure.Persistence.Seeding, Common.Application.Persistence (+13 more)

### Community 251 - "Response"
Cohesion: 0.33
Nodes (4): Products.Endpoints.Stores.v1.My.Create, Endpoint, Response, Id

### Community 261 - "Response"
Cohesion: 0.29
Nodes (5): Products.Endpoints.Stores, Setup, Endpoint, Response, Id

### Community 264 - "S3ObjectStoreCore"
Cohesion: 0.08
Nodes (10): IPrivateObjectStore, IPublicObjectStore, ObjectListItem, ObjectListPage, ObjectMetadata, UploadObjectRequest, S3ObjectStoreCore, S3PrivateObjectStore (+2 more)

### Community 265 - "IEnumerable"
Cohesion: 0.12
Nodes (11): Cross-process call path, How it works, CollectionExtensions, ICoreModule, PersistenceQueryableExtensions, BackgroundJobsModule, ActivitySourceNames, MeterNames (+3 more)

### Community 269 - "RouteGroupBuilder"
Cohesion: 0.04
Nodes (22): Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Setup, Endpoint (+14 more)

### Community 276 - "CustomValidator"
Cohesion: 0.06
Nodes (36): ModulesOptions, EnabledModules, ModulesOptionsValidator, SecurityHeadersOptions, Headers, SecurityHeadersOptionsValidator, CustomValidator, Request (+28 more)

### Community 279 - "Response"
Cohesion: 0.12
Nodes (14): IAM.Endpoints.Tokens.VersionNeutral.Create, IdentityScheme, Email, PhoneNumber, IdentitySchemeOptions, Scheme, IdentitySchemeOptionsValidator, Setup (+6 more)

### Community 280 - "SendForEmail/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForEmail, Request, CaptchaToken, Email, RequestValidator

### Community 282 - "Request"
Cohesion: 0.25
Nodes (8): Request, ClientId, DeviceId, DeviceName, Otp, PhoneNumber, PushToken, RequestValidator

### Community 283 - "Request"
Cohesion: 0.22
Nodes (9): Request, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken, Password, PushToken (+1 more)

### Community 296 - "AuditLogOptions"
Cohesion: 0.22
Nodes (8): AuditLogOptions, PerSchemaRetentionDays, PurgeBatchSize, RetentionCron, RetentionDays, AuditLogOptionsValidator, SmsTemplatesOptions, Otp

### Community 299 - "ProjectionReconciliationOptions.cs"
Cohesion: 0.50
Nodes (4): ProjectionReconciliationOptions, MaxPages, PageSize, ProjectionReconciliationOptionsValidator

### Community 307 - "SignalROptions"
Cohesion: 0.40
Nodes (5): SignalROptions, RedisConnectionString, RequireRedisBackplaneInProduction, UseRedisBackplane, SignalROptionsValidator

### Community 311 - "microsoft_aspnetcore_mvc"
Cohesion: 0.08
Nodes (27): Products.Endpoints.Stores.v1.Deactivate, Products.Endpoints.ProductTemplates.v1.Deactivate, Products.Endpoints.Stores.v1.My.RemoveProduct, Inventory.Endpoints.StockReservations.v1.Release, Common.Application.ModelBinders, Products.Endpoints.ProductTemplates.v1.Activate, Request, Id (+19 more)

### Community 314 - "JobHousekeepingOptions"
Cohesion: 0.40
Nodes (5): JobHousekeepingOptions, PageSize, RetentionHours, StaleAfterMinutes, JobHousekeepingOptionsValidator

### Community 319 - "Products/v1/Update/Request.cs"
Cohesion: 0.20
Nodes (11): Products.Endpoints.Products.v1.Update, Request, Body, Id, RequestBody, Description, Name, Price (+3 more)

### Community 323 - "v1/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Probe.v1, Request, Count, RequestValidator

### Community 324 - "Products/v1/My/Update/Request.cs"
Cohesion: 0.20
Nodes (11): Products.Endpoints.Products.v1.My.Update, Request, Body, Id, RequestBody, Description, Name, Price (+3 more)

## Knowledge Gaps
- **902 isolated node(s):** `UserId`, `Id`, `IdAsString`, `Roles`, `SessionId` (+897 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1206 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **116 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `ObjectStorageOptions`, `Common.Infrastructure.Extensions`, `Common.Domain.ResultMonad`, `ISearchLanguageResolver`, `Setup`, `EmailOptions`, `Common.Domain.StronglyTypedIds`, `FullTextSearchOptions`, `CustomValidator`, `Response`, `OutboxOptions.cs`, `Setup.Logger.cs`, `fluentvalidation`, `CustomRateLimitingOptions`, `ObservabilityOptions`, `AuditLogOptions`, `ConfigureSwaggerOptions`, `OtpOptions`, `ProjectionReconciliationOptions.cs`, `KeycloakOptions`, `S3ObjectStoreCore.cs`, `system_diagnostics`, `CorsOptions`, `system_text_json`, `BackgroundJobsOptions`, `CaptchaOptions`, `CachingOptions`, `RabbitMqOptions`, `SignalROptions`, `SmsOptions`, `JobHousekeepingOptions`, `RequestLoggingOptions`, `DevicesOptions`, `PushOptions`, `InterModuleRequestOptions`, `ResiliencyOptions`, `KeycloakPermissionAuthorizationHandler.cs`, `InventoryOptions`, `Common.InterModuleRequests.Contracts`, `ResxLocalizationOptions`, `Localization/Setup.cs`, `DatabaseOptions.cs`, `microsoft_entityframeworkcore`, `HealthCheckOptions`?**
  _High betweenness centrality (0.091) - this node is a cross-community bridge._
- **What connects `UserId`, `Id`, `IdAsString` to the rest of the system?**
  _902 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Result` be split into smaller, more focused modules?**
  _Cohesion score 0.059673659673659674 - nodes in this community are weakly interconnected._
- **Why does `ApplicationUserId` connect `ApplicationUserId` to `Result`, `NotificationPayload`, `AuditableEntityResponse`, `StoreId`, `Seeder`, `InterModuleRequestHandler`, `DefaultIdType`, `Type`, `KeycloakPermissionAuthorizationHandler`, `Product`, `Common.Domain.StronglyTypedIds`, `CustomValidator`, `DeviceRegistration`, `.RegisterAsync`, `For`, `CurrentUser`, `PaginationRequest`, `PaginationResponse`, `V1StoreCreatedDomainEvent`, `microsoft_aspnetcore_mvc`, `SendSecurityAlertRequestHandler`, `JobRow`, `Response`, `IBackgroundUserContext`, `Response`, `EntityTypeBuilder`, `Common.Domain.Entities`, `LoggerMessage`, `Response`, `.ForUser`, `Response`?**
  _High betweenness centrality (0.067) - this node is a cross-community bridge._
- **Should `AuditableEntityResponse` be split into smaller, more focused modules?**
  _Cohesion score 0.08333333333333333 - nodes in this community are weakly interconnected._
- **Why does `Result` connect `Result` to `ApplicationUserId`, `InterModuleRequestHandler`, `IEnumerable`, `Error`, `Product`, `.RegisterAsync`, `OtpOptions`, `ICaptchaService`, `ResultTelemetryExtensions`, `Task`, `PaginationQueryableExtensions`, `PaginationResponse`, `BrevoEmailGateway`, `.GetMeAsync`, `Func`, `VerifyPhoneOtpResponse`, `StockReservation`, `Common.Domain.Entities`, `ReCaptchaService`, `NetGsmSmsGateway`, `InterModuleRequestHandlerDefinition`?**
  _High betweenness centrality (0.055) - this node is a cross-community bridge._
- **Should `ApplicationUserId` be split into smaller, more focused modules?**
  _Cohesion score 0.07738095238095238 - nodes in this community are weakly interconnected._