# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-10-27)

## Corpus Check
- 882 files · ~192,206 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 5951 nodes · 17892 edges · 342 communities (211 shown, 131 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 868 edges (avg confidence: 0.87)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `3b24c986`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- DomainEvent
- DateTimeOffset
- Inventory.Domain.WarehouseConnections
- Products.Endpoints
- List
- ServiceAccountTokenCache
- IReadOnlyList
- IOptions
- Common.Infrastructure.Extensions
- Result
- Common.Domain.ResultMonad
- ProductExportJobRunner
- ProductId
- Response
- CurrentUser
- EmailOptions
- IOtpService
- Response
- Common.Application.Options
- FullTextSearchOptions
- BoundedRequestCaptureStream
- DeviceRegistration
- InterModuleRequestHandler
- IInterModuleRequestClient
- InventoryOptions
- Response
- CustomRateLimitingOptions
- OutboxModule
- BackgroundJobsModule
- Error
- InventoryDbContext
- IDbContext
- Response
- Response
- OutboxMessage
- IModule
- AdminRepresentations.cs
- microsoft_entityframeworkcore
- ObservabilityOptions
- .WriteTooManyRequestsToResponse
- IQueryable
- BoundedCaptureStream
- PaginationQueryableExtensions
- KeycloakPermissionAuthorizationHandler.cs
- OutboxOptions
- ProjectionReconciliationJob
- Outbox Misuse Check
- ReservationStatus
- Add Integration Event Command
- host
- Response
- CaptchaOptions
- KeyedResiliencePipelines
- RedisFixedWindowRateLimiter
- .ImportBatchAsync
- IEnumerable
- Cross-Module Reference Violation
- PaginationCursor
- Request
- Bogus Test Data
- BrevoEmailGateway
- RowOutcome
- StockBulkAdjustJobId
- Host.Swagger
- RequestBody
- .CreateAsync
- PackageDimensions
- .AddWarehouseGatewayInfrastructure
- PolymorphicEventConverter
- Getting started
- SetPackageDimensions/Request.cs
- FormApiWarehouseGateway
- AsNoTracking Coverage Check
- IIntegrationEventOutbox
- ProvisionUserRequestHandler
- IReadOnlyList
- KeycloakOptions
- Response
- Request
- Request
- DefaultIdType
- common_application_localization_resources
- EntityTypeBuilder
- RequestResponseBodyLoggingMiddleware
- WarehouseSecretReprotectSweepService
- RequestLoggingOptions
- Configuration-Driven Module Registration
- Response
- StoreAccessReconciliationJob
- .SaveWithOutboxAsync
- AlertRecipientsOptions
- DatabaseSeederOrchestrator
- SecretProtector
- DefaultResponsesOperationFilter
- ILogger
- Commit/Request.cs
- Response
- ReplaceCredentials/Request.cs
- Task
- ResiliencyOptions
- ActivitySource
- Common.Application.Storage
- ChangeLowStockThreshold/Request.cs
- Response
- Common.InterModuleRequests.Contracts
- WarehouseConnectionResponse
- Products/v1/My/Update/Request.cs
- Response
- Request
- WarehouseHttpGateway
- OtpOptions
- RequestBody
- Response
- InterModuleRequestOptions
- Response
- ApplicationUserId
- Receive/Request.cs
- depends_on
- healthcheck
- mm.keycloak
- mm.redis
- Response
- system_diagnostics
- ProductsOptions
- Pagination and search
- WarehouseConnectionId
- .DownloadExportAsync
- system_security_cryptography
- docker-compose.yml (Base Stack)
- IServiceCollection
- ObjectStorageOptions
- IServiceProvider
- NotificationPayload
- .GetClientKey
- HttpContext
- services
- mm.postgres
- AuditableEntityResponse
- IStronglyTypedId
- mm.rabbitmq
- Result
- OutboxOptions.cs
- Response
- Response
- KeycloakPermissionClient
- Response
- SkipOverlappingRecurringJobFilter
- OpenApiOptions
- Response
- README (Boilerplate Overview)
- NetGsmSmsGateway
- KeycloakPermissionAuthorizationHandler
- DateOnly
- SendRequestBody
- Request
- Adjust/Request.cs
- StatusSnapshot
- BackgroundJobsOptions
- ConfigureSwaggerOptions
- Request
- ActivitySource
- WebhookDeliveryOutcome
- CustomValidator
- ResultTelemetryExtensions
- PaginationResponse
- DisableChunkEncodingHandler
- Response
- Notifications.Application/IAssemblyReference.cs
- .KeyedRowSelector
- Product
- ICollection
- CachingOptions.cs
- v1/RemoveProduct/Request.cs
- IInventoryDbContext
- CorsOptions
- SendForEmail/Request.cs
- HttpClient
- SmsOptions
- RabbitMqOptions
- ConcurrencyConfiguratorExtensions
- StockBulkAdjustJob
- ChunkEncodingFreeS3Client
- SecretProtectionOptions
- ReCaptchaResponse
- VersionNeutral/Search/Request.cs
- GetImport/Request.cs
- StoreId
- AI-assisted development workflow
- Response
- KeycloakPermission
- Response
- Keycloak realm as code
- Response
- Response
- WarehouseConnections/v1/Get/Request.cs
- StronglyTypedIdBinder.cs
- KeycloakPermissionPolicyProvider
- EmailOtpDispatchOutcome
- KeycloakScopes
- ProjectionUpsertOutcome
- Response
- SendRequestBody
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- Response
- JwtBearerConfigureOptions
- ProviderStatusSnapshot
- SmsOtpDispatchOutcome
- Response
- .SearchAsync
- Platform building blocks
- fluentvalidation
- Response
- AuditLogOptions
- ReleaseReconcileOutcome
- ObjectStorageOptions.cs
- Response
- ResultToResponseTransformer.cs
- Response
- Split-Deployment PoC
- Request
- Request
- Modules
- Approve/Request.cs
- Response
- Setup
- AddImage/Request.cs
- WarehouseConnections/v1/Deactivate/Request.cs
- Activate/Request.cs
- ValidationContextExtensions
- Your first requests
- StronglyTypedIdSchemaFilter.cs
- CachingEntryDefaults
- ConstraintViolationExtensions
- ResxLocalizationOptions
- Response
- GetDeviceSessionsRequest
- WebhookCallback/Request.cs
- DevicesOptions
- Common.Domain.StronglyTypedIds
- .ToResult
- IAM.Domain
- .IsPublicAddress
- Request
- Response
- Response
- Products.Domain/IAssemblyReference.cs
- IAM.Application/IAssemblyReference.cs
- Products.Infrastructure/IAssemblyReference.cs
- Products.Application/IAssemblyReference.cs
- Common.Infrastructure/IAssemblyReference.cs
- Notifications.Domain/IAssemblyReference.cs
- IAM.Infrastructure/IAssemblyReference.cs
- .VerifySha256
- StockLevelErrors.cs
- Inventory.Endpoints
- Infrastructure/StringExtensions.cs
- IAM.Endpoints
- Inventory.Application/IAssemblyReference.cs
- StockBulkAdjustErrors.cs
- Inventory.Infrastructure/IAssemblyReference.cs
- Inventory.Domain/IAssemblyReference.cs
- RouteGroupBuilder
- Notifications.Infrastructure
- .LocalizeFromError
- IResult
- HealthCheckOptions
- ReverseProxyOptions
- INotificationsDbContext
- WarehouseAddressRefusedException
- .ReadRows
- Common.Domain.Events
- microsoft_aspnetcore_mvc
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
- REPR Pattern (Minimal API Endpoints)

## God Nodes (most connected - your core abstractions)
1. `Result` - 311 edges
2. `Common.Application.Options` - 205 edges
3. `Common.Domain.ResultMonad` - 181 edges
4. `ApplicationUserId` - 123 edges
5. `CustomValidator` - 122 edges
6. `Common.Application.Auth` - 113 edges
7. `IInventoryDbContext` - 108 edges
8. `Common.Application.Extensions` - 107 edges
9. `Common.Application.Validation` - 105 edges
10. `Common.Domain.StronglyTypedIds` - 105 edges

## Surprising Connections (you probably didn't know these)
- `Authorization` --references--> `ICurrentUser`  [INFERRED]
  docs/platform.md → src/Common/Common.Application/Auth/ICurrentUser.cs
- `Queued work: accept, queue, poll` --references--> `ObjectStorageOptions`  [INFERRED]
  docs/getting-started.md → src/Common/Common.Application/Options/ObjectStorageOptions.cs
- `The commands` --references--> `IModule`  [INFERRED]
  docs/ai-workflow.md → src/Common/Common.Infrastructure/Modules/IModule.cs
- `Adding a module or a feature` --references--> `IModule`  [INFERRED]
  docs/modules.md → src/Common/Common.Infrastructure/Modules/IModule.cs
- `Watch two modules cooperate` --references--> `ProductCreatedIntegrationEvent`  [INFERRED]
  docs/getting-started.md → src/Common/Common.IntegrationEvents/Products.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Local Infrastructure Stack** — docker_compose_postgres, docker_compose_rabbitmq, docker_compose_redis, docker_compose_aspire_dashboard [EXTRACTED 1.00]

## Communities (342 total, 131 thin omitted)

### Community 0 - "DomainEvent"
Cohesion: 0.04
Nodes (51): AggregateRoot, Events, Id, Version, IAggregateRoot, Events, Id, Version (+43 more)

### Community 1 - "DateTimeOffset"
Cohesion: 0.05
Nodes (55): Inventory, Notifications, IEvent, CreatedOn, Id, UtcDateTimeOffsetConverter, IntegrationEvent, CreatedOn (+47 more)

### Community 2 - "Inventory.Domain.WarehouseConnections"
Cohesion: 0.06
Nodes (23): Inventory.Domain.WebhookDeliveries, Inventory.Endpoints.StockLevels.v1.Search, Inventory.Application.WarehouseConnections, Inventory.Infrastructure.InterModuleRequestHandlers, Inventory.Domain.StockCounters, Inventory.Application.ProductSummaries, Inventory.Endpoints.StockReservations.v1.WebhookCallback, Inventory.Infrastructure.Gateway (+15 more)

### Community 5 - "List"
Cohesion: 0.13
Nodes (14): KeycloakUser, KeycloakUserPage, KeycloakAdminClient, UserRepresentation, Attributes, CreatedTimestamp, Credentials, Email (+6 more)

### Community 6 - "ServiceAccountTokenCache"
Cohesion: 0.13
Nodes (9): TokenResponseRepresentation, AccessToken, ExpiresIn, RefreshExpiresIn, RefreshToken, SessionState, ServiceAccountTokenCache, AccessToken (+1 more)

### Community 7 - "IReadOnlyList"
Cohesion: 0.18
Nodes (8): GetUsersInRolePageRequest, GetUsersInRolePageResponse, RoleUserSummary, GetUsersInRolePageRequestHandler, RoleMember, RoleMembersSnapshot, AlertRecipientsCursor, AlertRecipientsReconciliationJob

### Community 8 - "IOptions"
Cohesion: 0.08
Nodes (27): BackgroundJobs, Job rows, IBackgroundUserContext, UserId, JobHousekeepingJob, JobHousekeepingOptions, PageSize, RetentionHours (+19 more)

### Community 9 - "Common.Infrastructure.Extensions"
Cohesion: 0.05
Nodes (19): Common.Infrastructure.Modules, Common.Endpoints.Versioning, Products.Endpoints.Probe, Common.Infrastructure.RateLimiting, Products.Infrastructure.RateLimiting, Products.Endpoints.Products, Common.Infrastructure.Extensions, IAM.Endpoints.Otp.VersionNeutral (+11 more)

### Community 10 - "Result"
Cohesion: 0.05
Nodes (11): IBackgroundJobs, FeatureFlagResultExtensions, Result, Error, IsFailure, Success, AsyncExtensions, SyncExtensions (+3 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.06
Nodes (40): Inventory.Application.StockLevels, IAM.Endpoints.Captcha.VersionNeutral, Products.Infrastructure.Persistence, Inventory.Application.StockReservations, Products.Infrastructure.InterModuleRequestHandlers, Common.Application.Search, Products.Application.Exports, Products.Endpoints.Stores (+32 more)

### Community 13 - "ProductId"
Cohesion: 0.04
Nodes (27): Products.Domain.Products.DomainEvents.v1, Products.Application.Products.DomainEventHandlers.v1, StronglyTypedIdHelper, V1ProductImageRemovedDomainEventHandlers, ProductChangedIntegrationEventPublishingHandler, V1ProductNameUpdatedDomainEventHandlers, AttributeSnapshot, V1ProductAttributesReplacedDomainEvent (+19 more)

### Community 14 - "Response"
Cohesion: 0.13
Nodes (13): IAM.Endpoints.Users.VersionNeutral.Me.Get, Endpoint, Response, BirthDate, CreatedOn, Email, FirstName, Id (+5 more)

### Community 15 - "CurrentUser"
Cohesion: 0.20
Nodes (9): CurrentUser, BackgroundUserIdAsString, Id, IdAsString, IsAuthenticated, Principal, Roles, SessionId (+1 more)

### Community 16 - "EmailOptions"
Cohesion: 0.10
Nodes (23): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+15 more)

### Community 17 - "IOtpService"
Cohesion: 0.07
Nodes (20): OtpCacheEntry, IssueVerificationTokenRequest, IssueVerificationTokenResponse, SendEmailOtpRequest, SendEmailOtpResponse, SendPhoneOtpRequest, SendPhoneOtpResponse, ICaptchaService (+12 more)

### Community 18 - "Response"
Cohesion: 0.14
Nodes (12): Response, ActingUserId, ActingUserName, CreatedOn, Id, Quantity, QuantityOnHandAfter, Reason (+4 more)

### Community 19 - "Common.Application.Options"
Cohesion: 0.04
Nodes (42): Common.Infrastructure.Persistence, Notifications.Application.Sms, Notifications.Infrastructure.Devices, Inventory.Infrastructure.IntegrationEventHandlers, Common.Application.Persistence.Inbox, Notifications.Infrastructure.Email, Inventory.Application.Persistence, Inventory.Application.IntegrationEventHandlers (+34 more)

### Community 20 - "FullTextSearchOptions"
Cohesion: 0.05
Nodes (38): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Add a new language/culture, Add search to a new entity _(Build checklist)_ (+30 more)

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.13
Nodes (6): BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 22 - "DeviceRegistration"
Cohesion: 0.07
Nodes (25): AuditableEntity, AuditableEntity, Id, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, Version (+17 more)

### Community 23 - "InterModuleRequestHandler"
Cohesion: 0.17
Nodes (9): IInterModuleRequestHandler, InterModuleRequestHandler, GetStockLevelRequest, GetStockLevelResponse, GetProductRequest, GetProductResponse, GetStockLevelRequestHandler, Endpoint (+1 more)

### Community 24 - "IInterModuleRequestClient"
Cohesion: 0.06
Nodes (31): IInterModuleRequest, IInterModuleRequestClient, DeprovisionUserRequest, DeprovisionUserResponse, BindDeviceSessionRequest, BindDeviceSessionResponse, DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse (+23 more)

### Community 25 - "InventoryOptions"
Cohesion: 0.05
Nodes (37): InventoryOptions, AllowPrivateWarehouseAddresses, BulkAdjustBatchesPerMessage, BulkAdjustJobConcurrentMessageLimit, BulkAdjustMaxStoredRowErrors, BusinessTimeZoneId, DailyTotalsMaxRangeDays, MaxOccurrencesPerSeries (+29 more)

### Community 26 - "Response"
Cohesion: 0.29
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, Endpoint, Response, EmailVerificationToken, IsRegistered

### Community 27 - "CustomRateLimitingOptions"
Cohesion: 0.05
Nodes (36): CustomRateLimitingOptions, CheckRegistration, CreateStore, Email, ExemptPathPrefixes, Global, OtpVerify, Register (+28 more)

### Community 28 - "OutboxModule"
Cohesion: 0.11
Nodes (11): IInboxCleanupTarget, ModuleName, Setup, InboxStore, ModuleName, InboxCleanupJob, OutboxModule, ActivitySourceNames (+3 more)

### Community 29 - "BackgroundJobsModule"
Cohesion: 0.13
Nodes (9): Cross-process call path, How it works, ICoreModule, BackgroundJobsModule, ActivitySourceNames, MeterNames, Name, StartupPriority (+1 more)

### Community 30 - "Error"
Cohesion: 0.09
Nodes (21): Error, Key, ParameterName, StatusCode, SubErrors, Value, CaptchaErrors, IdentityErrors (+13 more)

### Community 31 - "InventoryDbContext"
Cohesion: 0.05
Nodes (33): Projections and reconciliation, ProjectionEntity, SourceVersion, ProductSummary, Name, ProductId, StoreId, StockReservationQueries (+25 more)

### Community 32 - "IDbContext"
Cohesion: 0.08
Nodes (10): IdBound, Value, JobClaimExtensions, IDbContext, AuditLog, ChangeTracker, Database, ProjectionUpsertExtensions (+2 more)

### Community 33 - "Response"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.Refresh, Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 34 - "Response"
Cohesion: 0.09
Nodes (20): StoreStatus, Active, Inactive, PendingApproval, Suspended, Endpoint, Response, Address (+12 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.08
Nodes (19): IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, OutboxMessage, CreatedOn (+11 more)

### Community 36 - "IModule"
Cohesion: 0.05
Nodes (30): Setup, IModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority, IamModule (+22 more)

### Community 37 - "AdminRepresentations.cs"
Cohesion: 0.08
Nodes (23): CredentialRepresentation, Temporary, Type, Value, ErrorRepresentation, Error, ErrorDescription, ErrorMessage (+15 more)

### Community 38 - "microsoft_entityframeworkcore"
Cohesion: 0.07
Nodes (15): Common.Infrastructure.Persistence.Inbox, Products.Infrastructure.Persistence.Seeding, Common.Application.Persistence, Products.Endpoints.Products.v1.Import, Inventory.Infrastructure.Persistence.EntityConfigurations, Notifications.Domain.Devices, Outbox.Persistence, Common.Application.Persistence.Outbox (+7 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.08
Nodes (17): ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics, EnableTracing, LogSink, MinimumLevel (+9 more)

### Community 41 - "IQueryable"
Cohesion: 0.15
Nodes (6): PersistenceQueryableExtensions, StockLevelQueries, StoreAccessQueries, Endpoint, Request, Id

### Community 42 - "BoundedCaptureStream"
Cohesion: 0.16
Nodes (6): BoundedCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 43 - "PaginationQueryableExtensions"
Cohesion: 0.17
Nodes (7): CursorBound, Value, KeyedRow, Item, Sort, Tie, PaginationQueryableExtensions

### Community 44 - "KeycloakPermissionAuthorizationHandler.cs"
Cohesion: 0.14
Nodes (3): IAM.Infrastructure.Auth, Notifications.Infrastructure.Hubs, Notifications.Application.Hubs

### Community 45 - "OutboxOptions"
Cohesion: 0.06
Nodes (24): Outbox, OutboxOptions, BaseBackoffSeconds, BatchSize, ClaimLeaseSeconds, Cleanup, IsProcessor, LagThresholdMinutes (+16 more)

### Community 48 - "ReservationStatus"
Cohesion: 0.08
Nodes (18): Webhooks, V1StockReservationProviderStatusMirroredDomainEventExtensions, V1StockReservationReadOnlyResolvedDomainEventExtensions, ProviderStatusMap, ProviderStatusResolution, Mapped, Unrecognized, ReservationStatus (+10 more)

### Community 50 - "host"
Cohesion: 0.18
Nodes (11): context, dockerfile, build, container_name, environment, image, networks, ports (+3 more)

### Community 51 - "Response"
Cohesion: 0.14
Nodes (12): Endpoint, Response, FailureKey, FileName, FinishedOn, JobId, ProductsWritten, QueuedOn (+4 more)

### Community 52 - "CaptchaOptions"
Cohesion: 0.16
Nodes (13): CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey, Provider, ScoreThreshold, SecretKey (+5 more)

### Community 54 - "RedisFixedWindowRateLimiter"
Cohesion: 0.20
Nodes (6): FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration, RetryAfterHeaderExtensions

### Community 55 - ".ImportBatchAsync"
Cohesion: 0.16
Nodes (10): StockBulkAdjustRowError, BatchSummary, RowResult, Adjusted, ProductImportRowError, BatchSummary, RowResult, Created (+2 more)

### Community 56 - "IEnumerable"
Cohesion: 0.11
Nodes (6): CollectionExtensions, WorkbookReader, IPushGateway, OtpTemplateConfiguration, FirebasePushGateway, Setup

### Community 58 - "PaginationCursor"
Cohesion: 0.19
Nodes (3): PaginationCursor, Payload, TaggedValue

### Community 59 - "Request"
Cohesion: 0.15
Nodes (13): Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken (+5 more)

### Community 61 - "BrevoEmailGateway"
Cohesion: 0.11
Nodes (12): IEmailGateway, BrevoEmailGateway, SendContact, Email, Name, SendErrorBody, Code, Message (+4 more)

### Community 63 - "RowOutcome"
Cohesion: 0.40
Nodes (5): RowOutcome, Created, Rejected, Unchanged, Updated

### Community 64 - "StockBulkAdjustJobId"
Cohesion: 0.05
Nodes (13): IPublicObjectStore, ObjectListItem, ObjectListPage, ObjectMetadata, UploadObjectRequest, S3ObjectStoreCore, S3PublicObjectStore, StockBulkAdjustJobId (+5 more)

### Community 65 - "Host.Swagger"
Cohesion: 0.18
Nodes (4): Host, Host.Swagger, Program, Setup

### Community 66 - "RequestBody"
Cohesion: 0.18
Nodes (10): RequestBody, FirstDeadline, IntervalDays, OccurrenceCount, ProductId, QuantityPerOccurrence, Warehouses, WarehouseAllocation (+2 more)

### Community 67 - ".CreateAsync"
Cohesion: 0.26
Nodes (6): Endpoint, Response, Id, WebhookPath, WebhookSecret, Endpoint

### Community 68 - "PackageDimensions"
Cohesion: 0.09
Nodes (13): Common.Domain, Persistence conventions, ValueObject, DimensionsSnapshot, V1ProductPackageDimensionsChangedDomainEvent, V1ProductPackageDimensionsChangedDomainEventExtensions, PackageDimensions, HeightCm (+5 more)

### Community 69 - ".AddWarehouseGatewayInfrastructure"
Cohesion: 0.27
Nodes (4): Setup, IWarehouseHostResolver, SystemWarehouseHostResolver, WarehouseConnectGuard

### Community 71 - "Getting started"
Cohesion: 0.18
Nodes (11): Configuration, Getting started, Prerequisites, Run the API from the command line instead, Run the load test, Run the tests, Run the whole system, Seeded data (+3 more)

### Community 72 - "SetPackageDimensions/Request.cs"
Cohesion: 0.20
Nodes (11): Products.Endpoints.Products.v1.My.SetPackageDimensions, Request, Body, Id, RequestBody, HeightCm, LengthCm, WeightKg (+3 more)

### Community 73 - "FormApiWarehouseGateway"
Cohesion: 0.19
Nodes (9): FormApiWarehouseGateway, HoldEnvelope, HoldResult, ErrorResponseBody, JsonApiWarehouseGateway, PushStockItem, PushStockRequestBody, ReleaseRequestBody (+1 more)

### Community 75 - "IIntegrationEventOutbox"
Cohesion: 0.06
Nodes (35): DomainEventHandlerBase, IEventHandler, IEventHandlerWrapper, IIntegrationEventOutbox, StockPushDueIntegrationEvent, CoalesceKey, StoreAccessChangedIntegrationEvent, StockLowThresholdCrossedIntegrationEventPublishingHandler (+27 more)

### Community 76 - "ProvisionUserRequestHandler"
Cohesion: 0.27
Nodes (9): ProvisionUserFailureReason, EmailAlreadyRegistered, IdentityProviderUnavailable, None, PhoneNumberAlreadyRegistered, Rejected, ProvisionUserRequest, ProvisionUserResponse (+1 more)

### Community 77 - "IReadOnlyList"
Cohesion: 0.14
Nodes (14): GetActiveSessionIdsRequest, GetActiveSessionIdsResponse, UserSessionIds, GrantedPermission, KeycloakUserSession, GetActiveSessionIdsRequestHandler, RowParser, Slice (+6 more)

### Community 78 - "KeycloakOptions"
Cohesion: 0.11
Nodes (19): KeycloakOptions, AttemptTimeoutSeconds, Authority, BaseUrl, DecisionCacheMaxDurationSeconds, Realm, RequireHttpsMetadata, ResourceClientId (+11 more)

### Community 79 - "Response"
Cohesion: 0.14
Nodes (12): IAM.Endpoints.Users.VersionNeutral.Search, Endpoint, Response, BirthDate, CreatedOn, Email, Enabled, FirstName (+4 more)

### Community 80 - "Request"
Cohesion: 0.17
Nodes (12): Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FirstName, LastName (+4 more)

### Community 81 - "Request"
Cohesion: 0.18
Nodes (11): Request, Email, EmailVerificationToken, FirstName, LastName, Password, PhoneNumber, StoreAddress (+3 more)

### Community 82 - "DefaultIdType"
Cohesion: 0.07
Nodes (41): Transactional outbox and inbox, IntegrationEventHandlerBase, MaxEventAge, CachingOptions, AllowInMemoryOnlyInProduction, EntryDefaults, IdempotencyKeyDuration, IdempotencyL1Duration (+33 more)

### Community 83 - "common_application_localization_resources"
Cohesion: 0.03
Nodes (71): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Products.v1.ListImports, Inventory.Endpoints.StockReservations.v1.My.Search, Products.Endpoints.Stores.v1.My.AuditLog, Inventory.Endpoints.WarehouseConnections.v1.Search, Inventory.Endpoints.StockLevels.v1.ListBulkAdjustments, Inventory.Endpoints.WarehouseConnections.v1.My.Search, Products.Endpoints.Products.v1.AuditLog (+63 more)

### Community 84 - "EntityTypeBuilder"
Cohesion: 0.06
Nodes (34): ProcessedMessage, ConsumerName, MessageId, ProcessedOn, AuditableEntityConfiguration, JobRowConfiguration, ProcessedMessageConfiguration, DomainEventConverter (+26 more)

### Community 86 - "WarehouseSecretReprotectSweepService"
Cohesion: 0.27
Nodes (3): Setup, WarehouseSecretReprotectSweepJobRegistrar, WarehouseSecretReprotectSweepService

### Community 87 - "RequestLoggingOptions"
Cohesion: 0.13
Nodes (15): RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes, SensitiveQueryParamPaths (+7 more)

### Community 89 - "Response"
Cohesion: 0.22
Nodes (8): Endpoint, Response, Address, Description, Name, OwnerId, ProductCount, Status

### Community 90 - "StoreAccessReconciliationJob"
Cohesion: 0.47
Nodes (6): GetStoresPageRequest, GetStoresPageResponse, StorePageItem, StoreAccessReconciliationCursor, StoreAccessReconciliationJob, GetStoresPageRequestHandler

### Community 91 - ".SaveWithOutboxAsync"
Cohesion: 0.12
Nodes (12): EventDispatcher, IntegrationEventOutbox, HasPending, Setup, BaseDbContext, AuditLog, OutboxSaveHelper, NotificationsDbContext (+4 more)

### Community 92 - "AlertRecipientsOptions"
Cohesion: 0.29
Nodes (7): AlertRecipientsOptions, EmailHtmlBodyTemplate, EmailSubjectTemplate, EmailTextBodyTemplate, ReconcileCron, RoleName, AlertRecipientsOptionsValidator

### Community 93 - "DatabaseSeederOrchestrator"
Cohesion: 0.13
Nodes (8): IDatabaseSeeder, Priority, DatabaseSeederOrchestrator, SeedingCompletionTracker, Setup, ProductsDatabaseSeeder, Priority, Setup

### Community 94 - "SecretProtector"
Cohesion: 0.21
Nodes (4): Secret protection, SecretGenerator, SecretProtector, Setup

### Community 95 - "DefaultResponsesOperationFilter"
Cohesion: 0.29
Nodes (3): DefaultResponsesOperationFilter, RemoveDefaultResponseSchemaFilter, SwaggerDefaultValues

### Community 96 - "ILogger"
Cohesion: 0.06
Nodes (13): AuditLogRetentionService, GlobalExceptionHandlingMiddleware, LoginCompletion, ReCaptchaService, RegistrationCompletion, RowOutcome, Adjusted, Rejected (+5 more)

### Community 97 - "Commit/Request.cs"
Cohesion: 0.28
Nodes (8): Inventory.Endpoints.StockReservations.v1.Commit, Request, Body, Id, RequestBody, ProviderReference, RequestBodyValidator, RequestValidator

### Community 98 - "Response"
Cohesion: 0.08
Nodes (21): BulkImportOptions, MaxFileBytes, MaxRows, SaveChunkSize, BulkImportOptionsValidator, Endpoint, Response, FailureKey (+13 more)

### Community 99 - "ReplaceCredentials/Request.cs"
Cohesion: 0.28
Nodes (8): Inventory.Endpoints.WarehouseConnections.v1.ReplaceCredentials, Request, Body, Id, RequestBody, ApiCredential, RequestBodyValidator, RequestValidator

### Community 100 - "Task"
Cohesion: 0.03
Nodes (22): ICurrentUser, Id, IdAsString, Roles, SessionId, IVariantFeatureManagerExtensions, IInboxStore, HttpClientKeyedExtensions (+14 more)

### Community 101 - "ResiliencyOptions"
Cohesion: 0.09
Nodes (23): KeyedResilienceProfile, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, RateLimitFailOpen, RateLimitPermits (+15 more)

### Community 102 - "ActivitySource"
Cohesion: 0.21
Nodes (6): RequireFeatureFilter, BackgroundJobsTelemetry, IamTelemetry, InventoryTelemetry, OutboxTelemetry, ProductsTelemetry

### Community 104 - "ChangeLowStockThreshold/Request.cs"
Cohesion: 0.28
Nodes (8): Inventory.Endpoints.StockLevels.v1.ChangeLowStockThreshold, Request, Body, Id, RequestBody, Threshold, RequestBodyValidator, RequestValidator

### Community 105 - "Response"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegisterByEmail, Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 106 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.05
Nodes (29): Notifications.Application.Otp, Common.Endpoints, IAM.Infrastructure.Keycloak.Representations, Common.Application.Caching, Common.Application.FeatureManagement, IAM.Endpoints.Otp, Notifications.Application.Persistence, Notifications.Infrastructure.Telemetry (+21 more)

### Community 107 - "WarehouseConnectionResponse"
Cohesion: 0.13
Nodes (11): WarehouseConnectionStatus, Active, Deactivated, Endpoint, Endpoint, Endpoint, WarehouseConnectionResponse, BaseUrl (+3 more)

### Community 108 - "Products/v1/My/Update/Request.cs"
Cohesion: 0.20
Nodes (11): Products.Endpoints.Products.v1.My.Update, Request, Body, Id, RequestBody, Description, Name, Price (+3 more)

### Community 109 - "Response"
Cohesion: 0.33
Nodes (4): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, Endpoint, Response, IsRegistered

### Community 110 - "Request"
Cohesion: 0.22
Nodes (9): Request, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken, Password, PushToken (+1 more)

### Community 112 - "OtpOptions"
Cohesion: 0.14
Nodes (12): OtpOptions, DummyCode, EmailQuotaWindowMinutes, ExpirationInMinutes, Length, MaxSendsPerEmailPerWindow, MaxSendsPerPhonePerWindow, PhoneQuotaWindowMinutes (+4 more)

### Community 113 - "RequestBody"
Cohesion: 0.05
Nodes (49): Products.Endpoints.Products.v1.My.SetAttributes, Inventory.Endpoints.StockReservations.v1.ResolveReadOnly, Products.Endpoints.Stores.v1.Update, Products.Endpoints.Products.v1.Update, Request, Body, RequestBodyValidator, RequestValidator (+41 more)

### Community 114 - "Response"
Cohesion: 0.14
Nodes (12): IAM.Endpoints.Users.VersionNeutral.Get, Endpoint, Response, BirthDate, CreatedOn, Email, Enabled, FirstName (+4 more)

### Community 115 - "InterModuleRequestOptions"
Cohesion: 0.15
Nodes (10): Inter-module requests, InterModuleRequestOptions, DependencyUnavailableRetryAfterSeconds, HandlerConcurrentMessageLimit, HandlerPrefetchCount, Timeouts, TimeoutSeconds, InterModuleRequestOptionsValidator (+2 more)

### Community 116 - "Response"
Cohesion: 0.22
Nodes (8): Endpoint, Response, Address, Description, Name, OwnerId, ProductCount, Status

### Community 117 - "ApplicationUserId"
Cohesion: 0.11
Nodes (7): CacheKeys, For, ApplicationUserId, IsEmpty, Value, BackgroundUserContext, UserId

### Community 118 - "Receive/Request.cs"
Cohesion: 0.24
Nodes (9): Inventory.Endpoints.StockLevels.v1.Receive, Request, Body, Id, RequestBody, Quantity, Reason, RequestBodyValidator (+1 more)

### Community 119 - "depends_on"
Cohesion: 0.22
Nodes (9): mm.aspire-dashboard, mm.keycloak, mm.postgres, mm.rabbitmq, depends_on, condition, condition, condition (+1 more)

### Community 120 - "healthcheck"
Cohesion: 0.56
Nodes (9): interval, retries, start_period, test, timeout, healthcheck, healthcheck, healthcheck (+1 more)

### Community 121 - "mm.keycloak"
Cohesion: 0.22
Nodes (9): command, container_name, environment, image, networks, ports, restart, volumes (+1 more)

### Community 122 - "mm.redis"
Cohesion: 0.22
Nodes (9): command, container_name, environment, image, networks, ports, restart, volumes (+1 more)

### Community 123 - "Response"
Cohesion: 0.20
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegister, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt, Setup

### Community 124 - "system_diagnostics"
Cohesion: 0.09
Nodes (6): Notifications.Application.Push, Notifications.Infrastructure.Push, BackgroundJobs.Telemetry, Notifications.Infrastructure.Push.Firebase, LoginMethods, SessionRevokedReasons

### Community 125 - "ProductsOptions"
Cohesion: 0.08
Nodes (26): Products.Endpoints.Products.v1.AddImage, ProductsOptions, ExportJobConcurrentMessageLimit, ExportMaxRows, ExportPageSize, ImageMaxFileBytes, ImportBatchesPerMessage, ImportJobConcurrentMessageLimit (+18 more)

### Community 126 - "Pagination and search"
Cohesion: 0.42
Nodes (3): Pagination and search, LikePattern, TrigramIndexExtensions

### Community 127 - "WarehouseConnectionId"
Cohesion: 0.05
Nodes (29): Inventory.Domain.WarehouseConnections.DomainEvents.v1, WarehouseConnectionQueries, ProviderSnapshot, Dummy, FormApi, JsonApi, V1WarehouseConnectionCreatedDomainEvent, V1WarehouseConnectionCreatedDomainEventExtensions (+21 more)

### Community 128 - ".DownloadExportAsync"
Cohesion: 0.17
Nodes (9): JobStatus, Failed, Queued, Running, Succeeded, Endpoint, FileDownload, FileReference (+1 more)

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.25
Nodes (10): Observability (OpenTelemetry), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), external, name, networks, local_shared_network, Postgres Service (mm.postgres) (+2 more)

### Community 131 - "IServiceCollection"
Cohesion: 0.06
Nodes (12): Setup, Setup, Setup, Setup, Setup, ModuleRegistry, IServiceAccountTokenProvider, Setup (+4 more)

### Community 132 - "ObjectStorageOptions"
Cohesion: 0.09
Nodes (22): ObjectStorageOptions, AccessKey, AdditionalTransientStatusCodes, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds (+14 more)

### Community 134 - "IServiceProvider"
Cohesion: 0.08
Nodes (11): IRecurringBackgroundJobs, AuditLogRetentionJobRegistrar, Setup, StockJobRequeueJobRegistrar, StockJobsHousekeepingRegistrar, StockPushSweepJobRegistrar, StockReleaseReconcileSweepJobRegistrar, StockReservationExpirySweepJobRegistrar (+3 more)

### Community 135 - "NotificationPayload"
Cohesion: 0.05
Nodes (32): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri, Type (+24 more)

### Community 136 - ".GetClientKey"
Cohesion: 0.20
Nodes (4): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, Response, ClientKey, DummyCaptchaService

### Community 137 - "HttpContext"
Cohesion: 0.09
Nodes (14): Rate limiting and resilience, FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit, RequestBodyLimitEndpointExtensions, RequestBodyLimitMetadata (+6 more)

### Community 138 - "services"
Cohesion: 0.25
Nodes (8): container_name, environment, image, networks, ports, restart, services, mm.aspire-dashboard

### Community 139 - "mm.postgres"
Cohesion: 0.25
Nodes (8): container_name, environment, image, networks, ports, restart, volumes, mm.postgres

### Community 140 - "AuditableEntityResponse"
Cohesion: 0.06
Nodes (28): AuditableEntityResponse, CreatedBy, CreatedOn, Id, LastModifiedBy, LastModifiedOn, Endpoint, Response (+20 more)

### Community 141 - "IStronglyTypedId"
Cohesion: 0.14
Nodes (7): StrictDateTimeOffsetJsonConverter, StronglyTypedIdListReadOnlyJsonConverter, StronglyTypedIdReadOnlyJsonConverter, StronglyTypedIdReadOnlyJsonConverter, StronglyTypedIdWriteOnlyJsonConverter, IStronglyTypedId, Value

### Community 142 - "mm.rabbitmq"
Cohesion: 0.25
Nodes (8): container_name, environment, image, networks, ports, restart, volumes, mm.rabbitmq

### Community 143 - "Result"
Cohesion: 0.30
Nodes (4): Result, Error, IsFailure, Value

### Community 144 - "OutboxOptions.cs"
Cohesion: 0.32
Nodes (7): OutboxCleanupSettings, BatchSize, CronSchedule, Enabled, RetentionDays, OutboxCleanupSettingsValidator, OutboxOptionsValidator

### Community 145 - "Response"
Cohesion: 0.12
Nodes (14): IAM.Endpoints.Tokens.VersionNeutral.Create, IdentityScheme, Email, PhoneNumber, IdentitySchemeOptions, Scheme, IdentitySchemeOptionsValidator, Setup (+6 more)

### Community 146 - "Response"
Cohesion: 0.17
Nodes (11): Products.Endpoints.Products.v1.My.Get, Endpoint, PackageDimensionsResponse, Response, Attributes, Description, Name, PackageDimensions (+3 more)

### Community 147 - "KeycloakPermissionClient"
Cohesion: 0.16
Nodes (10): KeycloakPaths, KeycloakPermissionClient, DecisionRepresentation, Result, PermissionRepresentation, ResourceName, Scopes, TokenErrorRepresentation (+2 more)

### Community 148 - "Response"
Cohesion: 0.12
Nodes (13): Inventory.Endpoints.StockReservations.v1.Get, Response, HasPendingReleaseAttempt, Id, IsReadOnly, ProductId, ProviderReference, ProviderStatus (+5 more)

### Community 150 - "OpenApiOptions"
Cohesion: 0.22
Nodes (9): OpenApiOptions, ContactEmail, ContactName, Description, EnableSwagger, LicenseName, LicenseUrl, Title (+1 more)

### Community 151 - "Response"
Cohesion: 0.22
Nodes (8): Endpoint, Response, Address, CreatedOn, Description, Id, Name, OwnerId

### Community 153 - "NetGsmSmsGateway"
Cohesion: 0.15
Nodes (8): NetGsmSmsGateway, SendMessageBody, Msg, No, SendResponseBody, Code, Description, JobId

### Community 154 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.29
Nodes (3): KeycloakPermissionAuthorizationHandler, KeycloakPermissionRequirement, Permission

### Community 156 - "SendRequestBody"
Cohesion: 0.29
Nodes (6): SendRequestBody, HtmlContent, Sender, Subject, TextContent, To

### Community 157 - "Request"
Cohesion: 0.25
Nodes (8): Request, ClientId, DeviceId, DeviceName, Otp, PhoneNumber, PushToken, RequestValidator

### Community 158 - "Adjust/Request.cs"
Cohesion: 0.24
Nodes (9): Inventory.Endpoints.StockLevels.v1.Adjust, Request, Body, Id, RequestBody, Quantity, Reason, RequestBodyValidator (+1 more)

### Community 159 - "StatusSnapshot"
Cohesion: 0.29
Nodes (7): StatusSnapshot, Active, Committed, Expired, Released, RequiresReconciliation, Unknown

### Community 160 - "BackgroundJobsOptions"
Cohesion: 0.29
Nodes (7): BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount, BackgroundJobsOptionsValidator

### Community 162 - "Request"
Cohesion: 0.20
Nodes (10): Request, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name, OwnerId, SearchTerm (+2 more)

### Community 164 - "WebhookDeliveryOutcome"
Cohesion: 0.25
Nodes (7): WebhookDeliveryOutcome, Applied, Duplicate, Ignored, Mirrored, Stale, Unrecognized

### Community 165 - "CustomValidator"
Cohesion: 0.04
Nodes (50): CustomRateLimitingOptionsValidator, FixedWindowValidator, DatabaseOptions, ConnectionString, DatabaseOptionsValidator, JobHousekeepingOptionsValidator, ModulesOptions, EnabledModules (+42 more)

### Community 167 - "PaginationResponse"
Cohesion: 0.09
Nodes (20): AuditLogDto, PaginationResponse, HasNext, HasPrevious, HasTotal, NextPageNumber, PreviousPageNumber, TotalPages (+12 more)

### Community 169 - "Response"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Products.v1.Get, PackageDimensionsResponse, Response, Attributes, AvailableQuantity, Description, Name, PackageDimensions (+3 more)

### Community 172 - "Product"
Cohesion: 0.05
Nodes (46): File map _(Build)_, Products, ISearchLocalized, Language, ProductExportJob, FileName, ObjectKey, StoreId (+38 more)

### Community 173 - "ICollection"
Cohesion: 0.17
Nodes (5): Inventory.Endpoints.StockReservations.v1.ReserveSeries, ProblemDetailsExtensions, Endpoint, Response, Ids

### Community 174 - "CachingOptions.cs"
Cohesion: 0.32
Nodes (7): CachingOptionsValidator, Redis, AppName, Host, Password, Port, RedisValidator

### Community 175 - "v1/RemoveProduct/Request.cs"
Cohesion: 0.40
Nodes (5): Products.Endpoints.Stores.v1.RemoveProduct, Request, Id, ProductId, RequestValidator

### Community 176 - "IInventoryDbContext"
Cohesion: 0.04
Nodes (29): Dirty set and lease, IWarehouseGateway, IWarehouseGatewayFactory, WarehouseStockQuantity, IInventoryDbContext, ProductSummaries, StockBulkAdjustJobs, StockLevels (+21 more)

### Community 177 - "CorsOptions"
Cohesion: 0.29
Nodes (7): CorsOptions, AllowCredentials, AllowedHeaders, AllowedMethods, AllowedOrigins, MaxAgeInSeconds, CorsOptionsValidator

### Community 178 - "SendForEmail/Request.cs"
Cohesion: 0.40
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.SendForEmail, Request, CaptchaToken, Email, RequestValidator

### Community 180 - "SmsOptions"
Cohesion: 0.08
Nodes (28): SmsOptions, AppName, AttemptTimeoutSeconds, BaseUrl, MaxPerDay, MaxPerPhoneNumberPerDay, MaxRetryAttempts, MsgHeader (+20 more)

### Community 181 - "RabbitMqOptions"
Cohesion: 0.15
Nodes (13): RabbitMqOptions, DefaultConcurrentMessageLimit, DefaultPrefetchCount, Host, Password, Port, RetryIntervalDeltaMs, RetryLimit (+5 more)

### Community 183 - "StockBulkAdjustJob"
Cohesion: 0.08
Nodes (20): JobRow, FailureKey, FinishedOn, Progress, QueuedOn, RequestedBy, StartedOn, Status (+12 more)

### Community 185 - "SecretProtectionOptions"
Cohesion: 0.28
Nodes (5): Troubleshooting, SecretProtectionOptions, EncryptionKey, PreviousEncryptionKeys, SecretProtectionOptionsValidator

### Community 186 - "ReCaptchaResponse"
Cohesion: 0.29
Nodes (6): ReCaptchaResponse, ChallengeTs, ErrorCodes, Hostname, Score, Success

### Community 187 - "VersionNeutral/Search/Request.cs"
Cohesion: 0.40
Nodes (4): Constants, Request, SearchTerm, RequestValidator

### Community 188 - "GetImport/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Products.v1.GetImport, Request, Id, RequestValidator

### Community 189 - "StoreId"
Cohesion: 0.05
Nodes (38): GetSeedUserIdsRequest, GetSeedUserIdsResponse, GetSeedUserIdsRequestHandler, ProductImportJob, ClientRequestId, FileName, LastProcessedRow, ObjectKey (+30 more)

### Community 190 - "AI-assisted development workflow"
Cohesion: 0.25
Nodes (8): Adapting it, AI-assisted development workflow, `CLAUDE.md`, Guards, The commands, The four layers, The issue-to-pull-request automation, The knowledge graph

### Community 191 - "Response"
Cohesion: 0.33
Nodes (4): Products.Endpoints.Stores.v1.AddProduct, Endpoint, Response, Id

### Community 193 - "Response"
Cohesion: 0.11
Nodes (16): Response, FailureKey, FileName, FinishedOn, JobId, ProcessedRows, QueuedOn, Reason (+8 more)

### Community 194 - "Keycloak realm as code"
Cohesion: 0.22
Nodes (9): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Realms that already exist, Secrets, Seed users (dev / test only) (+1 more)

### Community 195 - "Response"
Cohesion: 0.33
Nodes (4): Inventory.Endpoints.StockReservations.v1.Reserve, Endpoint, Response, Id

### Community 196 - "Response"
Cohesion: 0.29
Nodes (5): Inventory.Endpoints.WarehouseConnections.v1.RotateWebhookSecret, Endpoint, Response, WebhookPath, WebhookSecret

### Community 197 - "WarehouseConnections/v1/Get/Request.cs"
Cohesion: 0.50
Nodes (4): Inventory.Endpoints.WarehouseConnections.v1.Get, Request, Id, RequestValidator

### Community 200 - "EmailOtpDispatchOutcome"
Cohesion: 0.33
Nodes (5): EmailOtpDispatchErrors, EmailOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled

### Community 201 - "KeycloakScopes"
Cohesion: 0.17
Nodes (11): Devices, Hangfire, KeycloakScopes, Products, ProductTemplates, Sessions, StockLevels, StockReservations (+3 more)

### Community 202 - "ProjectionUpsertOutcome"
Cohesion: 0.40
Nodes (4): ProjectionUpsertOutcome, Inserted, Stale, Updated

### Community 203 - "Response"
Cohesion: 0.29
Nodes (4): Products.Endpoints.ProductTemplates.v1.Create, Setup, Response, Id

### Community 205 - "SendRequestBody"
Cohesion: 0.29
Nodes (6): SendRequestBody, AppName, Encoding, IysFilter, Messages, MsgHeader

### Community 210 - "Response"
Cohesion: 0.22
Nodes (8): Endpoint, Response, Address, Description, Name, OwnerId, ProductCount, Status

### Community 212 - "ProviderStatusSnapshot"
Cohesion: 0.29
Nodes (7): ProviderStatusSnapshot, Active, Committed, Expired, Released, RequiresReconciliation, Unknown

### Community 214 - "SmsOtpDispatchOutcome"
Cohesion: 0.33
Nodes (5): OtpDispatchErrors, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled

### Community 215 - "Response"
Cohesion: 0.17
Nodes (10): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, Endpoint, Response, ClientId, DeviceName, Id, IpAddress, IsCurrent (+2 more)

### Community 216 - ".SearchAsync"
Cohesion: 0.37
Nodes (5): SearchProductsByTermRequest, SearchProductsByTermResponse, Endpoint, PageResponse, SearchProductsByTermRequestHandler

### Community 217 - "Platform building blocks"
Cohesion: 0.12
Nodes (14): Authorization, Caching, Feature flags, Object storage, Observability, Options and configuration, Platform building blocks, Results and errors (+6 more)

### Community 218 - "fluentvalidation"
Cohesion: 0.07
Nodes (34): Products.Endpoints.Stores.v1.My.Update, IAM.Domain.Users, IAM.Endpoints.Otp.VersionNeutral.SendForLogin, Common.Domain.Extensions, Notifications.Infrastructure.Devices.UpdateCurrentPushToken, Common.Application.Validation, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, Common.Domain.Devices (+26 more)

### Community 219 - "Response"
Cohesion: 0.25
Nodes (6): Inventory.Endpoints.StockLevels.v1.DailyTotals, Response, CommittedReservations, CommittedUnits, Day, Endpoint

### Community 220 - "AuditLogOptions"
Cohesion: 0.33
Nodes (6): AuditLogOptions, PerSchemaRetentionDays, PurgeBatchSize, RetentionCron, RetentionDays, AuditLogOptionsValidator

### Community 221 - "ReleaseReconcileOutcome"
Cohesion: 0.20
Nodes (8): Inventory.Endpoints.StockReservations.v1.ReconcileRelease, Endpoint, Response, Outcome, ReleaseReconcileOutcome, AlreadyReleased, NotReleased, Released

### Community 222 - "ObjectStorageOptions.cs"
Cohesion: 0.40
Nodes (4): ObjectStorageOptionsValidator, ObjectStorageRetryMode, Adaptive, Standard

### Community 223 - "Response"
Cohesion: 0.29
Nodes (4): Products.Endpoints.Stores.v1.Create, Setup, Response, Id

### Community 224 - "ResultToResponseTransformer.cs"
Cohesion: 0.10
Nodes (9): Common.Application.EndpointFilters, ProblemResponse, ResultToAcceptedResponseTransformer, ResultToCreatedResponseTransformer, ResultToResponseTransformer, ResultToResponseTransformer, RouteHandlerBuilderExtensions, RouteHandlerBuilderExtensions (+1 more)

### Community 225 - "Response"
Cohesion: 0.29
Nodes (7): Response, Id, LowStockThreshold, ProductId, ProductName, QuantityOnHand, StoreId

### Community 226 - "Split-Deployment PoC"
Cohesion: 0.33
Nodes (6): Concurrent safety, Files added by this PoC, How to run, Split-Deployment PoC, What this proves, What to check

### Community 227 - "Request"
Cohesion: 0.40
Nodes (5): Request, ClientRequestId, File, Reason, RequestValidator

### Community 228 - "Request"
Cohesion: 0.40
Nodes (5): Request, ClientRequestId, File, ProductTemplateId, RequestValidator

### Community 229 - "Modules"
Cohesion: 0.50
Nodes (4): Adding a module or a feature, How modules fit together, IAM, Modules

### Community 230 - "Approve/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Stores.v1.Approve, Request, Id, RequestValidator

### Community 231 - "Response"
Cohesion: 0.33
Nodes (4): Products.Endpoints.Stores.v1.My.AddProduct, Endpoint, Response, Id

### Community 233 - "AddImage/Request.cs"
Cohesion: 0.14
Nodes (12): Products.Endpoints.Products.v1.DownloadExport, Products.Endpoints.Probe.v1, Request, Count, RequestValidator, Request, File, Id (+4 more)

### Community 234 - "WarehouseConnections/v1/Deactivate/Request.cs"
Cohesion: 0.50
Nodes (4): Inventory.Endpoints.WarehouseConnections.v1.Deactivate, Request, Id, RequestValidator

### Community 235 - "Activate/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.ProductTemplates.v1.Activate, Request, Id, RequestValidator

### Community 237 - "Your first requests"
Cohesion: 0.33
Nodes (6): Connect a store to a warehouse system, Errors, Queued work: accept, queue, poll, Sign in, Watch two modules cooperate, Your first requests

### Community 239 - "CachingEntryDefaults"
Cohesion: 0.33
Nodes (6): CachingEntryDefaults, Duration, FactoryHardTimeout, FactorySoftTimeout, FailSafeMaxDuration, FailSafeThrottleDuration

### Community 241 - "ResxLocalizationOptions"
Cohesion: 0.40
Nodes (5): Localization, ResxLocalizationOptions, DefaultCulture, SupportedCultures, ResxLocalizationOptionsValidator

### Community 242 - "Response"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 243 - "GetDeviceSessionsRequest"
Cohesion: 0.73
Nodes (4): DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, GetDeviceSessionsRequestHandler

### Community 244 - "WebhookCallback/Request.cs"
Cohesion: 0.14
Nodes (12): Constants, Request, DeliveryId, OccurredOn, ProviderReference, ReservationId, Status, StatusWord (+4 more)

### Community 245 - "DevicesOptions"
Cohesion: 0.40
Nodes (5): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator

### Community 246 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.03
Nodes (31): Products.Endpoints.Stores.v1.GetApplication, Common.InterModuleRequests, Inventory.Domain.WarehouseConnections.Errors, Common.Domain.StronglyTypedIds, Products.Endpoints.Stores.v1.Search, Products.Endpoints.Stores.v1.ListAll, Products.Endpoints.Stores.v1.Apply, Products.Domain.Stores.Errors (+23 more)

### Community 250 - "Request"
Cohesion: 0.40
Nodes (5): Request, From, StoreId, To, RequestValidator

### Community 251 - "Response"
Cohesion: 0.33
Nodes (4): Products.Endpoints.Stores.v1.My.Create, Endpoint, Response, Id

### Community 269 - "RouteGroupBuilder"
Cohesion: 0.03
Nodes (37): Setup, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Setup (+29 more)

### Community 273 - "HealthCheckOptions"
Cohesion: 0.40
Nodes (5): HealthCheckOptions, EnableHealthChecks, ReadinessTimeoutInSeconds, StartupTimeoutInSeconds, HealthCheckOptionsValidator

### Community 274 - "ReverseProxyOptions"
Cohesion: 0.33
Nodes (5): ReverseProxyOptions, ForwardLimit, IsEnabled, TrustedNetworks, ReverseProxyOptionsValidator

### Community 275 - "INotificationsDbContext"
Cohesion: 0.12
Nodes (9): AutoMigrateMarker, IAutoMigrateMarker, MigrationGuard, INotificationsDbContext, AlertRecipients, DeviceRegistrations, DeviceRegistryReconcileJobRegistrar, DeviceRegistryReconciliationService (+1 more)

### Community 284 - ".ReadRows"
Cohesion: 0.15
Nodes (4): StringExtensions, CsvWriter, WorkbookReader, FormulaGuard

### Community 285 - "Common.Domain.Events"
Cohesion: 0.06
Nodes (13): Inventory.Domain.StockReservations.DomainEvents.v1, Common.Domain.Events, Products.Domain.Stores.DomainEvents.v2, Inventory.Application.StockReservations.DomainEventHandlers.v1, Inventory.Application.StockLevels.DomainEventHandlers.v1, Products.Domain.Stores.DomainEvents.v1, Inventory.Domain.StockLevels.DomainEvents.v1, V1StockLowThresholdCrossedDomainEventHandlers (+5 more)

### Community 323 - "microsoft_aspnetcore_mvc"
Cohesion: 0.03
Nodes (66): Products.Endpoints.Stores.v1.Suspend, Inventory.Endpoints.StockLevels.v1.GetBulkAdjustment, Products.Endpoints.Stores.v1.Deactivate, Products.Endpoints.ProductTemplates.v1.Deactivate, Products.Endpoints.Products.v1.RemoveImage, Products.Endpoints.Stores.v1.My.RemoveProduct, Products.Endpoints.Stores.v1.Reinstate, Inventory.Endpoints.StockReservations.v1.Release (+58 more)

## Knowledge Gaps
- **1299 isolated node(s):** `UserId`, `Id`, `IdAsString`, `Roles`, `SessionId` (+1294 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1718 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **131 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `system_security_cryptography`, `Inventory.Domain.WarehouseConnections`, `IServiceCollection`, `NotificationPayload`, `Common.Infrastructure.Extensions`, `Common.Domain.ResultMonad`, `EmailOptions`, `HealthCheckOptions`, `Response`, `OutboxOptions.cs`, `FullTextSearchOptions`, `OpenApiOptions`, `InventoryOptions`, `BackgroundJobsOptions`, `CustomValidator`, `microsoft_entityframeworkcore`, `ObservabilityOptions`, `KeycloakPermissionAuthorizationHandler.cs`, `CachingOptions.cs`, `CorsOptions`, `CaptchaOptions`, `RabbitMqOptions`, `SmsOptions`, `Setup.Logger.cs`, `Host.Swagger`, `KeycloakOptions`, `RequestLoggingOptions`, `fluentvalidation`, `AlertRecipientsOptions`, `AuditLogOptions`, `ObjectStorageOptions.cs`, `Response`, `ResiliencyOptions`, `Common.Application.Storage`, `Common.InterModuleRequests.Contracts`, `OtpOptions`, `ResxLocalizationOptions`, `InterModuleRequestOptions`, `DevicesOptions`, `Common.Domain.StronglyTypedIds`, `system_diagnostics`, `ProductsOptions`?**
  _High betweenness centrality (0.072) - this node is a cross-community bridge._
- **What connects `UserId`, `Id`, `IdAsString` to the rest of the system?**
  _1299 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `DomainEvent` be split into smaller, more focused modules?**
  _Cohesion score 0.0379746835443038 - nodes in this community are weakly interconnected._
- **Why does `Result` connect `Result` to `DomainEvent`, `DateTimeOffset`, `.DownloadExportAsync`, `List`, `.GetClientKey`, `ProductId`, `Result`, `IResult`, `IOtpService`, `InterModuleRequestHandler`, `IInterModuleRequestClient`, `NetGsmSmsGateway`, `Error`, `IDbContext`, `ResultTelemetryExtensions`, `PaginationResponse`, `IQueryable`, `Product`, `IInventoryDbContext`, `IEnumerable`, `BrevoEmailGateway`, `StoreId`, `StockBulkAdjustJobId`, `.CreateAsync`, `PackageDimensions`, `EmailOtpDispatchOutcome`, `IIntegrationEventOutbox`, `IReadOnlyList`, `SmsOtpDispatchOutcome`, `.SearchAsync`, `ILogger`, `Task`, `WarehouseHttpGateway`, `.ToResult`, `WarehouseConnectionId`?**
  _High betweenness centrality (0.049) - this node is a cross-community bridge._
- **Should `DateTimeOffset` be split into smaller, more focused modules?**
  _Cohesion score 0.05194805194805195 - nodes in this community are weakly interconnected._
- **Why does `Common.Domain.ResultMonad` connect `Common.Domain.ResultMonad` to `ResultToResponseTransformer.cs`, `system_security_cryptography`, `Inventory.Domain.WarehouseConnections`, `StockLevelErrors.cs`, `microsoft_entityframeworkcore`, `Common.InterModuleRequests.Contracts`, `Result`, `StockBulkAdjustErrors.cs`, `Result`, `IResult`, `Common.Application.Options`, `SmsOptions`, `Common.Domain.StronglyTypedIds`, `system_diagnostics`, `Error`?**
  _High betweenness centrality (0.040) - this node is a cross-community bridge._
- **Should `Inventory.Domain.WarehouseConnections` be split into smaller, more focused modules?**
  _Cohesion score 0.05817028027498678 - nodes in this community are weakly interconnected._