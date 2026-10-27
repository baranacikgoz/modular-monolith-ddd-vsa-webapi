# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-10-27)

## Corpus Check
- 882 files · ~183,517 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 5951 nodes · 17839 edges · 345 communities (219 shown, 126 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 815 edges (avg confidence: 0.87)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `273127e0`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- StockMovement
- StockReservation
- RequestBodyLimitMetadata
- Products.Endpoints
- List
- DateTimeOffset
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
- PushOptions
- BackgroundJobsModule
- Error
- InventoryDbContext
- IDbContext
- Response
- Response
- OutboxMessage
- IModule
- AdminRepresentations.cs
- Common.Domain.StronglyTypedIds
- ObservabilityOptions
- .WriteTooManyRequestsToResponse
- ProductSummariesReconciliationJob
- BoundedCaptureStream
- AuditableEntity
- KeycloakPermissionAuthorizationHandler.cs
- OutboxOptions
- StoreAccessReconciliationJob
- Outbox Misuse Check
- Request
- Add Integration Event Command
- host
- Response
- CaptchaOptions
- KeyedResiliencePipelines
- RedisFixedWindowRateLimiter
- .ImportBatchAsync
- IEnumerable
- Cross-Module Reference Violation
- .PageMovementsAsync
- Request
- Bogus Test Data
- BrevoEmailGateway
- ProductImportJobRunner
- S3ObjectStoreCore
- Host.Swagger
- RequestBody
- SearchLanguageResolver
- PackageDimensions
- .AddWarehouseGatewayInfrastructure
- DomainEvent
- Getting started
- AggregateRoot
- FormApiWarehouseGateway
- AsNoTracking Coverage Check
- IIntegrationEventOutbox
- InventoryDbContext
- .GetUserSessionsAsync
- KeycloakOptions
- Response
- NotifyRoleOnCriticalAlertRaisedHandler
- system_globalization
- DefaultIdType
- common_application_localization_resources
- EntityTypeBuilder
- RequestResponseBodyLoggingMiddleware
- ISmsGateway
- RequestLoggingOptions
- Configuration-Driven Module Registration
- Response
- .AddNotificationsSignalR
- BaseDbContext
- AlertRecipientsOptions
- DatabaseSeederOrchestrator
- SecretProtector
- DefaultResponsesOperationFilter
- ILogger
- StockBulkAdjustRowError
- Response
- StockLevel
- Task
- ResiliencyOptions
- BackgroundJobsTelemetry
- Common.Application.Storage
- ProductAttribute
- Response
- Common.InterModuleRequests.Contracts
- WarehouseConnectionResponse
- Products/v1/My/Update/Request.cs
- Response
- SetAttributes/Request.cs
- WarehouseHttpGateway
- OtpOptions
- CustomValidator
- Response
- InterModuleRequestOptions
- .TryDeserialize
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
- microsoft_extensions_configuration
- docker-compose.yml (Base Stack)
- IServiceCollection
- ObjectStorageOptions
- IServiceProvider
- NotificationPayload
- .GetClientKey
- ValueTask
- services
- mm.postgres
- AuditableEntityResponse
- IStronglyTypedId
- mm.rabbitmq
- Result
- Persistence conventions
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
- ProcessedMessage
- Adjust/Request.cs
- StatusSnapshot
- BackgroundJobsOptions
- ConfigureSwaggerOptions
- v1/AddProduct/Request.cs
- ActivitySource
- Request
- fluentvalidation
- ResultTelemetryExtensions
- IProductsDbContext
- .HandleAsync
- Response
- Notifications.Application/IAssemblyReference.cs
- ProductTemplates/Constants.cs
- Product
- Response
- CachingOptions.cs
- StockPushDirtyItem
- IInventoryDbContext
- CorsOptions
- WebhookDelivery
- HttpClient
- SmsOptions
- RabbitMqOptions
- ProductTemplate
- JobRow
- ChunkEncodingFreeS3Client
- SecretProtectionOptions
- Uri
- V1ProductRemovedFromStoreDomainEvent.cs
- Request
- StoreId
- AI-assisted development workflow
- Response
- KeycloakPermission
- Response
- Keycloak realm as code
- Response
- Response
- .AdjustBatchAsync
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
- V1StockQuantityChangedDomainEvent
- SmsOtpDispatchOutcome
- Response
- IQueryable
- Platform building blocks
- IResxLocalizer
- Response
- AuditLogOptions
- ReleaseReconcileOutcome
- StockBulkAdjustJob
- Response
- ResultToResponseTransformer.cs
- Response
- Split-Deployment PoC
- AuditLogEntry
- GetBulkAdjustment/Request.cs
- RequestBody
- Approve/Request.cs
- Response
- Setup
- DownloadExport/Request.cs
- AddImage/Request.cs
- Activate/Request.cs
- Response
- Your first requests
- StronglyTypedIdSchemaFilter.cs
- CachingEntryDefaults
- ConstraintViolationExtensions
- ResxLocalizationOptions
- Response
- GetDeviceSessionsRequest
- WebhookCallback/Request.cs
- DevicesOptions
- Common.Application.DTOs
- ProblemDetailsExtensions.cs
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
- ProviderSnapshot
- StockLevelErrors.cs
- Inventory.Endpoints
- Infrastructure/StringExtensions.cs
- IAM.Endpoints
- Inventory.Application/IAssemblyReference.cs
- SendResponseBody
- Inventory.Infrastructure/IAssemblyReference.cs
- Inventory.Domain/IAssemblyReference.cs
- RouteGroupBuilder
- Notifications.Infrastructure
- .LocalizeFromError
- IResult
- HealthCheckOptions
- ReverseProxyOptions
- IAutoMigrateMarker.cs
- JwtClaimNames.cs
- KeycloakRoles.cs
- Stores/Constants.cs
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
- `Watch two modules cooperate` --references--> `ProductCreatedIntegrationEvent`  [INFERRED]
  docs/getting-started.md → src/Common/Common.IntegrationEvents/Products.cs
- `Observability` --references--> `RequestResponseBodyLoggingMiddleware`  [INFERRED]
  docs/platform.md → src/Host/Host/Middlewares/RequestResponseBodyLoggingMiddleware.cs

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Local Infrastructure Stack** — docker_compose_postgres, docker_compose_rabbitmq, docker_compose_redis, docker_compose_aspire_dashboard [EXTRACTED 1.00]

## Communities (345 total, 126 thin omitted)

### Community 0 - "StockMovement"
Cohesion: 0.12
Nodes (14): StockMovement, Quantity, QuantityOnHandAfter, Reason, ReservationId, Sequence, StockLevelId, Type (+6 more)

### Community 1 - "StockReservation"
Cohesion: 0.09
Nodes (24): V1StockReservationReadOnlyResolvedDomainEvent, V1StockReservationReadOnlyResolvedDomainEventExtensions, V1StockReservationReleasedDomainEvent, ReservationStatus, Active, Committed, Expired, Released (+16 more)

### Community 2 - "RequestBodyLimitMetadata"
Cohesion: 0.15
Nodes (6): RequestBodyLimitEndpointExtensions, RequestBodyLimitMetadata, MaxBodyBytes, RequestBodyLimitMiddleware, EnrichLogsWithUserInfoMiddleware, SecurityHeadersMiddleware

### Community 5 - "List"
Cohesion: 0.13
Nodes (14): KeycloakUser, KeycloakUserPage, KeycloakAdminClient, UserRepresentation, Attributes, CreatedTimestamp, Credentials, Email (+6 more)

### Community 6 - "DateTimeOffset"
Cohesion: 0.07
Nodes (17): ServiceAccountTokenCache, AccessToken, ExpiresAt, V1StockReservationProviderEventTimeRecordedDomainEvent, V1StockReservationProviderStatusUnrecognizedDomainEvent, V1StockReservationReleaseAttemptAbandonedDomainEvent, V1StockReservationReleaseAttemptConfirmedDomainEvent, V1StockReservationReleaseAttemptStartedDomainEvent (+9 more)

### Community 7 - "IReadOnlyList"
Cohesion: 0.16
Nodes (10): GetUsersInRolePageRequest, GetUsersInRolePageResponse, RoleUserSummary, GetUsersInRolePageRequestHandler, RoleMember, RoleMembersSnapshot, AlertRecipientsCursor, AlertRecipientsReconciliationJob (+2 more)

### Community 8 - "IOptions"
Cohesion: 0.11
Nodes (29): BackgroundJobs, Products, Job rows, JobHousekeepingJob, JobHousekeepingOptions, PageSize, RetentionHours, StaleAfterMinutes (+21 more)

### Community 9 - "Common.Infrastructure.Extensions"
Cohesion: 0.05
Nodes (21): Common.Infrastructure.Modules, Common.Endpoints.Versioning, Products.Endpoints.Probe, Common.Infrastructure.RateLimiting, Inventory.Endpoints.StockReservations.v1.WebhookCallback, Products.Infrastructure.RateLimiting, Products.Endpoints.Products, Common.Infrastructure.Extensions (+13 more)

### Community 10 - "Result"
Cohesion: 0.04
Nodes (12): IBackgroundJobs, FeatureFlagResultExtensions, Result, Error, IsFailure, Success, AsyncExtensions, SyncExtensions (+4 more)

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.07
Nodes (34): Inventory.Application.StockLevels, IAM.Endpoints.Captcha.VersionNeutral, Products.Infrastructure.Persistence, Common.Endpoints.Webhooks, Inventory.Application.StockReservations, Products.Infrastructure.InterModuleRequestHandlers, Common.Application.Search, Products.Application.Exports (+26 more)

### Community 12 - "ProductExportJobRunner"
Cohesion: 0.26
Nodes (4): IBackgroundUserContext, UserId, ProductExportJobId, ProductExportJobRunner

### Community 13 - "ProductId"
Cohesion: 0.10
Nodes (11): V1ProductImageAddedDomainEvent, V1ProductImageRemovedDomainEvent, ProductId, ProductImage, ContentType, ObjectKey, ProductId, SizeBytes (+3 more)

### Community 14 - "Response"
Cohesion: 0.13
Nodes (13): IAM.Endpoints.Users.VersionNeutral.Me.Get, Endpoint, Response, BirthDate, CreatedOn, Email, FirstName, Id (+5 more)

### Community 15 - "CurrentUser"
Cohesion: 0.13
Nodes (10): CurrentUser, BackgroundUserIdAsString, Id, IdAsString, IsAuthenticated, Principal, Roles, SessionId (+2 more)

### Community 16 - "EmailOptions"
Cohesion: 0.10
Nodes (23): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+15 more)

### Community 17 - "IOtpService"
Cohesion: 0.08
Nodes (19): OtpCacheEntry, IssueVerificationTokenRequest, IssueVerificationTokenResponse, SendEmailOtpRequest, SendEmailOtpResponse, SendPhoneOtpRequest, SendPhoneOtpResponse, EmailMessage (+11 more)

### Community 18 - "Response"
Cohesion: 0.14
Nodes (12): Response, ActingUserId, ActingUserName, CreatedOn, Id, Quantity, QuantityOnHandAfter, Reason (+4 more)

### Community 19 - "Common.Application.Options"
Cohesion: 0.04
Nodes (40): Inventory.Application.WarehouseConnections, Inventory.Infrastructure.IntegrationEventHandlers, Common.Application.Persistence.Inbox, Inventory.Application.Persistence, Inventory.Application.ProductSummaries, Inventory.Application.IntegrationEventHandlers, Notifications.Infrastructure.Alerts, Common.IntegrationEvents (+32 more)

### Community 20 - "FullTextSearchOptions"
Cohesion: 0.05
Nodes (40): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Add a new language/culture, Add search to a new entity _(Build checklist)_ (+32 more)

### Community 21 - "BoundedRequestCaptureStream"
Cohesion: 0.13
Nodes (6): BoundedRequestCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 22 - "DeviceRegistration"
Cohesion: 0.12
Nodes (12): DeviceRegistration, ClientId, DeviceId, DeviceName, IsActive, LastReconciledOn, PushToken, PushTokenUpdatedOn (+4 more)

### Community 23 - "InterModuleRequestHandler"
Cohesion: 0.17
Nodes (9): IInterModuleRequestHandler, InterModuleRequestHandler, GetStockLevelRequest, GetStockLevelResponse, GetProductRequest, GetProductResponse, GetStockLevelRequestHandler, Endpoint (+1 more)

### Community 24 - "IInterModuleRequestClient"
Cohesion: 0.06
Nodes (32): IInterModuleRequest, IInterModuleRequestClient, DeprovisionUserRequest, DeprovisionUserResponse, BindDeviceSessionRequest, BindDeviceSessionResponse, DeactivateDeviceSessionsRequest, DeactivateDeviceSessionsResponse (+24 more)

### Community 25 - "InventoryOptions"
Cohesion: 0.05
Nodes (36): InventoryOptions, AllowPrivateWarehouseAddresses, BulkAdjustBatchesPerMessage, BulkAdjustJobConcurrentMessageLimit, BulkAdjustMaxStoredRowErrors, BusinessTimeZoneId, DailyTotalsMaxRangeDays, MaxOccurrencesPerSeries (+28 more)

### Community 26 - "Response"
Cohesion: 0.29
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, Endpoint, Response, EmailVerificationToken, IsRegistered

### Community 27 - "CustomRateLimitingOptions"
Cohesion: 0.06
Nodes (22): CustomRateLimitingOptions, CheckRegistration, CreateStore, Email, ExemptPathPrefixes, Global, OtpVerify, Register (+14 more)

### Community 28 - "PushOptions"
Cohesion: 0.12
Nodes (20): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri, Type (+12 more)

### Community 29 - "BackgroundJobsModule"
Cohesion: 0.13
Nodes (9): Cross-process call path, How it works, ICoreModule, BackgroundJobsModule, ActivitySourceNames, MeterNames, Name, StartupPriority (+1 more)

### Community 30 - "Error"
Cohesion: 0.07
Nodes (24): Error, Key, ParameterName, StatusCode, SubErrors, Value, CaptchaErrors, IdentityErrors (+16 more)

### Community 31 - "InventoryDbContext"
Cohesion: 0.13
Nodes (12): ProjectionEntity, SourceVersion, ProductSummary, Name, ProductId, StoreId, StoreAccess, IsActive (+4 more)

### Community 32 - "IDbContext"
Cohesion: 0.12
Nodes (8): IdBound, Value, JobClaimExtensions, IDbContext, AuditLog, ChangeTracker, Database, ProjectionUpsertExtensions

### Community 33 - "Response"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.Refresh, Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 34 - "Response"
Cohesion: 0.06
Nodes (28): StoreStatus, Active, Inactive, PendingApproval, Suspended, Endpoint, Response, Address (+20 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.05
Nodes (27): IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, OutboxMessage, CreatedOn (+19 more)

### Community 36 - "IModule"
Cohesion: 0.05
Nodes (31): Adding a module or a feature, Setup, IModule, ActivitySourceNames, MeterNames, Name, RateLimitingPolicies, StartupPriority (+23 more)

### Community 37 - "AdminRepresentations.cs"
Cohesion: 0.11
Nodes (18): CredentialRepresentation, Temporary, Type, Value, ErrorRepresentation, Error, ErrorDescription, ErrorMessage (+10 more)

### Community 38 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.03
Nodes (38): Common.Infrastructure.Persistence.Inbox, Inventory.Domain.WebhookDeliveries, Inventory.Endpoints.StockLevels.v1.Search, Common.Infrastructure.Persistence, Products.Infrastructure.Persistence.Seeding, Inventory.Infrastructure.InterModuleRequestHandlers, Common.Domain.StronglyTypedIds, Inventory.Domain.StockCounters (+30 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.09
Nodes (17): ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics, EnableTracing, LogSink, MinimumLevel (+9 more)

### Community 41 - "ProductSummariesReconciliationJob"
Cohesion: 0.22
Nodes (8): GetProductsPageRequest, GetProductsPageResponse, ProductPageItem, ProductSummariesReconciliationCursor, ProductSummariesReconciliationJob, ProductSummariesReconciliationJobRegistrar, Setup, GetProductsPageRequestHandler

### Community 42 - "BoundedCaptureStream"
Cohesion: 0.13
Nodes (7): HmacSignatureVerifier, BoundedCaptureStream, CanRead, CanSeek, CanWrite, Length, Position

### Community 43 - "AuditableEntity"
Cohesion: 0.12
Nodes (13): AuditableEntity, AuditableEntity, Id, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, Version (+5 more)

### Community 44 - "KeycloakPermissionAuthorizationHandler.cs"
Cohesion: 0.17
Nodes (3): Common.Infrastructure.Auth.Services, IAM.Infrastructure.Auth, Common.Infrastructure.Auth

### Community 45 - "OutboxOptions"
Cohesion: 0.07
Nodes (24): How modules fit together, Modules, Outbox, OutboxOptions, BaseBackoffSeconds, BatchSize, ClaimLeaseSeconds, Cleanup (+16 more)

### Community 46 - "StoreAccessReconciliationJob"
Cohesion: 0.14
Nodes (9): ProjectionReconciliationJob, GetStoresPageRequest, GetStoresPageResponse, StorePageItem, Setup, StoreAccessReconciliationCursor, StoreAccessReconciliationJob, StoreAccessReconciliationJobRegistrar (+1 more)

### Community 48 - "Request"
Cohesion: 0.10
Nodes (14): Webhooks, ProviderStatusMap, ProviderStatusResolution, Mapped, Unrecognized, ReservationStatusTransitions, Request, DeliveryId (+6 more)

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
Cohesion: 0.17
Nodes (6): FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration, RetryAfterHeaderExtensions

### Community 55 - ".ImportBatchAsync"
Cohesion: 0.19
Nodes (8): ProductImportRowError, BatchSummary, RowResult, Created, Unchanged, Updated, RowParser, SheetRow

### Community 56 - "IEnumerable"
Cohesion: 0.12
Nodes (5): CollectionExtensions, IPushGateway, OtpTemplateConfiguration, FirebasePushGateway, Setup

### Community 58 - ".PageMovementsAsync"
Cohesion: 0.28
Nodes (7): ActorNames, GetUsersRequest, GetUsersResponse, UserSummary, GetUsersRequestHandler, UserDisplayCacheEntry, Endpoint

### Community 59 - "Request"
Cohesion: 0.05
Nodes (36): Common.Domain.Extensions, Notifications.Infrastructure.Devices.UpdateCurrentPushToken, IAM.Endpoints.Tokens.VersionNeutral.Sessions.Revoke, Common.Domain.Devices, DeviceSessionConstants, Request, Id, RequestValidator (+28 more)

### Community 61 - "BrevoEmailGateway"
Cohesion: 0.11
Nodes (13): IEmailGateway, BrevoEmailGateway, SendContact, Email, Name, SendErrorBody, Code, Message (+5 more)

### Community 63 - "ProductImportJobRunner"
Cohesion: 0.08
Nodes (17): ProductImportRequestedIntegrationEvent, StockBulkAdjustJobId, StockBulkAdjustJobEnqueuer, RowOutcome, Adjusted, Rejected, Slice, StockBulkAdjustJobRunner (+9 more)

### Community 64 - "S3ObjectStoreCore"
Cohesion: 0.10
Nodes (8): IPublicObjectStore, ObjectListItem, ObjectListPage, UploadObjectRequest, S3ObjectStoreCore, S3PublicObjectStore, Endpoint, ImageUpload

### Community 65 - "Host.Swagger"
Cohesion: 0.18
Nodes (4): Host, Host.Swagger, Program, Setup

### Community 66 - "RequestBody"
Cohesion: 0.18
Nodes (10): RequestBody, FirstDeadline, IntervalDays, OccurrenceCount, ProductId, QuantityPerOccurrence, Warehouses, WarehouseAllocation (+2 more)

### Community 67 - "SearchLanguageResolver"
Cohesion: 0.33
Nodes (3): SearchLanguageResolver, UniversalConfig, Setup

### Community 68 - "PackageDimensions"
Cohesion: 0.10
Nodes (11): Common.Domain, ValueObject, DimensionsSnapshot, V1ProductPackageDimensionsChangedDomainEvent, V1ProductPackageDimensionsChangedDomainEventExtensions, PackageDimensions, HeightCm, LengthCm (+3 more)

### Community 69 - ".AddWarehouseGatewayInfrastructure"
Cohesion: 0.27
Nodes (4): Setup, IWarehouseHostResolver, SystemWarehouseHostResolver, WarehouseConnectGuard

### Community 70 - "DomainEvent"
Cohesion: 0.09
Nodes (15): Products.Domain.Products.DomainEvents.v1, Products.Domain.Stores.DomainEvents.v2, Products.Application.Products.DomainEventHandlers.v1, DomainEvent, CreatedOn, Id, Version, UnknownDomainEvent (+7 more)

### Community 71 - "Getting started"
Cohesion: 0.18
Nodes (11): Configuration, Getting started, Prerequisites, Run the API from the command line instead, Run the load test, Run the tests, Run the whole system, Seeded data (+3 more)

### Community 72 - "AggregateRoot"
Cohesion: 0.13
Nodes (13): AggregateRoot, Events, Id, Version, IAggregateRoot, Events, Id, Version (+5 more)

### Community 73 - "FormApiWarehouseGateway"
Cohesion: 0.18
Nodes (9): FormApiWarehouseGateway, HoldEnvelope, HoldResult, ErrorResponseBody, JsonApiWarehouseGateway, PushStockItem, PushStockRequestBody, ReleaseRequestBody (+1 more)

### Community 75 - "IIntegrationEventOutbox"
Cohesion: 0.07
Nodes (36): DomainEventHandlerBase, IEventHandler, IEventHandlerWrapper, IIntegrationEventOutbox, StoreAccessChangedIntegrationEvent, StockLowThresholdCrossedIntegrationEventPublishingHandler, StockLowThresholdRecoveredIntegrationEventPublishingHandler, StockReservationCommittedIntegrationEventPublishingHandler (+28 more)

### Community 76 - "InventoryDbContext"
Cohesion: 0.10
Nodes (18): StoreDailyStockTotal, CommittedReservations, CommittedUnits, Day, StoreId, StoreDailyStockTotalConfiguration, InventoryDbContext, ProductSummaries (+10 more)

### Community 77 - ".GetUserSessionsAsync"
Cohesion: 0.22
Nodes (10): GetActiveSessionIdsRequest, GetActiveSessionIdsResponse, UserSessionIds, KeycloakUserSession, GetActiveSessionIdsRequestHandler, UserSessionRepresentation, Id, IpAddress (+2 more)

### Community 78 - "KeycloakOptions"
Cohesion: 0.11
Nodes (19): KeycloakOptions, AttemptTimeoutSeconds, Authority, BaseUrl, DecisionCacheMaxDurationSeconds, Realm, RequireHttpsMetadata, ResourceClientId (+11 more)

### Community 79 - "Response"
Cohesion: 0.14
Nodes (12): IAM.Endpoints.Users.VersionNeutral.Search, Endpoint, Response, BirthDate, CreatedOn, Email, Enabled, FirstName (+4 more)

### Community 81 - "system_globalization"
Cohesion: 0.05
Nodes (38): Products.Endpoints.Stores.v1.My.Update, Request, ClientRequestId, File, Reason, RequestValidator, Request, ClientRequestId (+30 more)

### Community 82 - "DefaultIdType"
Cohesion: 0.05
Nodes (57): IAM, Inventory, Notifications, Projections and reconciliation, Transactional outbox and inbox, IntegrationEventHandlerBase, MaxEventAge, CachingOptions (+49 more)

### Community 83 - "common_application_localization_resources"
Cohesion: 0.03
Nodes (70): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Products.v1.ListImports, Inventory.Endpoints.StockReservations.v1.My.Search, Products.Endpoints.Stores.v1.My.AuditLog, Inventory.Endpoints.WarehouseConnections.v1.Search, Inventory.Endpoints.StockLevels.v1.ListBulkAdjustments, Inventory.Endpoints.WarehouseConnections.v1.My.Search, Products.Endpoints.Products.v1.AuditLog (+62 more)

### Community 84 - "EntityTypeBuilder"
Cohesion: 0.15
Nodes (9): AuditableEntityConfiguration, JobRowConfiguration, StronglyTypedIdValueConverter, StockBulkAdjustJobConfiguration, StockReservationConfiguration, WarehouseConnectionConfiguration, ProductConfiguration, ProductImportJobConfiguration (+1 more)

### Community 86 - "ISmsGateway"
Cohesion: 0.19
Nodes (8): ISmsGateway, SmsCategory, CommercialIndividual, CommercialMerchant, Transactional, DummySmsGateway, Setup, ThrottledSmsGateway

### Community 87 - "RequestLoggingOptions"
Cohesion: 0.13
Nodes (15): RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes, SensitiveQueryParamPaths (+7 more)

### Community 89 - "Response"
Cohesion: 0.22
Nodes (8): Endpoint, Response, Address, Description, Name, OwnerId, ProductCount, Status

### Community 90 - ".AddNotificationsSignalR"
Cohesion: 0.20
Nodes (3): Setup, Setup, SubjectUserIdProvider

### Community 91 - "BaseDbContext"
Cohesion: 0.07
Nodes (19): IntegrationEventOutbox, HasPending, Setup, BaseDbContext, AuditLog, MigrationGuard, INotificationsDbContext, AlertRecipients (+11 more)

### Community 92 - "AlertRecipientsOptions"
Cohesion: 0.29
Nodes (7): AlertRecipientsOptions, EmailHtmlBodyTemplate, EmailSubjectTemplate, EmailTextBodyTemplate, ReconcileCron, RoleName, AlertRecipientsOptionsValidator

### Community 93 - "DatabaseSeederOrchestrator"
Cohesion: 0.18
Nodes (3): DatabaseSeederOrchestrator, SeedingCompletionTracker, Setup

### Community 94 - "SecretProtector"
Cohesion: 0.08
Nodes (20): Secret protection, SecretGenerator, SecretProtector, Setup, ProvisionUserFailureReason, EmailAlreadyRegistered, IdentityProviderUnavailable, None (+12 more)

### Community 95 - "DefaultResponsesOperationFilter"
Cohesion: 0.29
Nodes (3): DefaultResponsesOperationFilter, RemoveDefaultResponseSchemaFilter, SwaggerDefaultValues

### Community 96 - "ILogger"
Cohesion: 0.06
Nodes (8): AuditLogRetentionService, OutboxSaveHelper, GlobalExceptionHandlingMiddleware, LoginCompletion, ReCaptchaService, RegistrationCompletion, NotificationsHub, DummyPushGateway

### Community 97 - "StockBulkAdjustRowError"
Cohesion: 0.36
Nodes (4): StockBulkAdjustRowError, BatchSummary, RowResult, Adjusted

### Community 98 - "Response"
Cohesion: 0.11
Nodes (16): Endpoint, Response, FailureKey, FileName, FinishedOn, JobId, ProcessedRows, ProductsCreated (+8 more)

### Community 99 - "StockLevel"
Cohesion: 0.23
Nodes (7): StockLevel, LastMovementSequence, LowStockThreshold, Movements, ProductId, QuantityOnHand, StockLevelConfiguration

### Community 100 - "Task"
Cohesion: 0.03
Nodes (11): IVariantFeatureManagerExtensions, IInboxStore, ObjectMetadata, HttpClientKeyedExtensions, IKeycloakPermissionClient, AccessTokenReader, HttpContentJsonExtensions, StockLevelRowLock (+3 more)

### Community 101 - "ResiliencyOptions"
Cohesion: 0.09
Nodes (23): KeyedResilienceProfile, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, RateLimitFailOpen, RateLimitPermits (+15 more)

### Community 103 - "Common.Application.Storage"
Cohesion: 0.18
Nodes (3): Common.Infrastructure.Storage, Common.Infrastructure.Resiliency, Common.Application.Storage

### Community 104 - "ProductAttribute"
Cohesion: 0.29
Nodes (4): AttributeSnapshot, V1ProductAttributesReplacedDomainEvent, V1ProductAttributesReplacedDomainEventExtensions, ProductAttribute

### Community 105 - "Response"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegisterByEmail, Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 106 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.04
Nodes (29): Common.Endpoints, Common.InterModuleRequests, Inventory.Domain.WarehouseConnections.Errors, IAM.Infrastructure.Keycloak.Representations, Products.Endpoints.Stores.v1.Apply, Products.Domain.Stores.Errors, Common.Application.FeatureManagement, IAM.Endpoints.Otp (+21 more)

### Community 107 - "WarehouseConnectionResponse"
Cohesion: 0.13
Nodes (11): WarehouseConnectionStatus, Active, Deactivated, Endpoint, Endpoint, Endpoint, WarehouseConnectionResponse, BaseUrl (+3 more)

### Community 108 - "Products/v1/My/Update/Request.cs"
Cohesion: 0.20
Nodes (11): Products.Endpoints.Products.v1.My.Update, Request, Body, Id, RequestBody, Description, Name, Price (+3 more)

### Community 109 - "Response"
Cohesion: 0.33
Nodes (4): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, Endpoint, Response, IsRegistered

### Community 110 - "SetAttributes/Request.cs"
Cohesion: 0.24
Nodes (9): Products.Endpoints.Products.v1.My.SetAttributes, AttributeItem, Name, Value, Request, Body, Id, RequestBodyValidator (+1 more)

### Community 112 - "OtpOptions"
Cohesion: 0.14
Nodes (12): OtpOptions, DummyCode, EmailQuotaWindowMinutes, ExpirationInMinutes, Length, MaxSendsPerEmailPerWindow, MaxSendsPerPhonePerWindow, PhoneQuotaWindowMinutes (+4 more)

### Community 113 - "CustomValidator"
Cohesion: 0.04
Nodes (71): Inventory.Endpoints.StockReservations.v1.Commit, Inventory.Endpoints.StockReservations.v1.ResolveReadOnly, Products.Endpoints.Stores.v1.Update, Products.Endpoints.Products.v1.My.SetPackageDimensions, Inventory.Endpoints.WarehouseConnections.v1.ReplaceCredentials, Inventory.Endpoints.StockLevels.v1.ChangeLowStockThreshold, OutboxCleanupSettings, BatchSize (+63 more)

### Community 114 - "Response"
Cohesion: 0.14
Nodes (12): IAM.Endpoints.Users.VersionNeutral.Get, Endpoint, Response, BirthDate, CreatedOn, Email, Enabled, FirstName (+4 more)

### Community 115 - "InterModuleRequestOptions"
Cohesion: 0.15
Nodes (10): Inter-module requests, InterModuleRequestOptions, DependencyUnavailableRetryAfterSeconds, HandlerConcurrentMessageLimit, HandlerPrefetchCount, Timeouts, TimeoutSeconds, InterModuleRequestOptionsValidator (+2 more)

### Community 117 - "ApplicationUserId"
Cohesion: 0.08
Nodes (8): CacheKeys, For, ApplicationUserId, IsEmpty, Value, BackgroundUserContext, UserId, NotificationGroupName

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
Cohesion: 0.12
Nodes (14): IAM.Endpoints.Users.VersionNeutral.SelfRegister, IdentityScheme, Email, PhoneNumber, IdentitySchemeOptions, Scheme, IdentitySchemeOptionsValidator, Setup (+6 more)

### Community 124 - "system_diagnostics"
Cohesion: 0.07
Nodes (7): BackgroundJobs.Telemetry, Products.Endpoints.Products.v1.Import, BackgroundJobs, Inventory.Endpoints.StockLevels.v1.BulkAdjust, Products.Endpoints.Products.v1.Export, LoginMethods, SessionRevokedReasons

### Community 125 - "ProductsOptions"
Cohesion: 0.08
Nodes (23): ConcurrencyConfiguratorExtensions, ProductsOptions, ExportJobConcurrentMessageLimit, ExportMaxRows, ExportPageSize, ImageMaxFileBytes, ImportBatchesPerMessage, ImportJobConcurrentMessageLimit (+15 more)

### Community 126 - "Pagination and search"
Cohesion: 0.36
Nodes (3): Pagination and search, LikePattern, TrigramIndexExtensions

### Community 127 - "WarehouseConnectionId"
Cohesion: 0.08
Nodes (17): Inventory.Domain.WarehouseConnections.DomainEvents.v1, V1WarehouseConnectionCreatedDomainEventExtensions, V1WarehouseConnectionCredentialsReplacedDomainEvent, V1WarehouseConnectionDeactivatedDomainEvent, V1WarehouseConnectionSecretsReprotectedDomainEvent, V1WarehouseConnectionWebhookSecretRotatedDomainEvent, WarehouseConnection, BaseUrl (+9 more)

### Community 128 - ".DownloadExportAsync"
Cohesion: 0.17
Nodes (9): JobStatus, Failed, Queued, Running, Succeeded, Endpoint, FileDownload, FileReference (+1 more)

### Community 129 - "microsoft_extensions_configuration"
Cohesion: 0.04
Nodes (22): Notifications.Application.Otp, Notifications.Application.Sms, Products.Infrastructure.Spreadsheets, Notifications.Infrastructure.Devices, Notifications.Application.Push, Notifications.Infrastructure.Email, Common.Application.Caching, Notifications.Infrastructure.Push (+14 more)

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.25
Nodes (10): Observability (OpenTelemetry), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), external, name, networks, local_shared_network, Postgres Service (mm.postgres) (+2 more)

### Community 131 - "IServiceCollection"
Cohesion: 0.06
Nodes (10): Setup, Setup, Setup, Setup, ModuleRegistry, IServiceAccountTokenProvider, Setup, Setup (+2 more)

### Community 132 - "ObjectStorageOptions"
Cohesion: 0.08
Nodes (26): ObjectStorageOptions, AccessKey, AdditionalTransientStatusCodes, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds (+18 more)

### Community 134 - "IServiceProvider"
Cohesion: 0.06
Nodes (16): IRecurringBackgroundJobs, EventDispatcher, AuditLogRetentionJobRegistrar, Setup, StockJobRequeueJobRegistrar, StockJobsHousekeepingRegistrar, Setup, StockPushSweepJobRegistrar (+8 more)

### Community 135 - "NotificationPayload"
Cohesion: 0.18
Nodes (9): SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, INotificationDispatcher, INotificationsClient, NotificationPayload, SignalRNotificationDispatcher (+1 more)

### Community 136 - ".GetClientKey"
Cohesion: 0.20
Nodes (4): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, Response, ClientKey, DummyCaptchaService

### Community 137 - "ValueTask"
Cohesion: 0.10
Nodes (23): Rate limiting and resilience, HttpContextExtensions, RateLimitPartitions, CheckRegistrationRateLimitingPolicy, OnRejected, EmailRateLimitingPolicy, OnRejected, OtpVerifyRateLimitingPolicy (+15 more)

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
Cohesion: 0.05
Nodes (19): StrictDateTimeOffsetJsonConverter, StronglyTypedIdListReadOnlyJsonConverter, StronglyTypedIdReadOnlyJsonConverter, StronglyTypedIdReadOnlyJsonConverter, StronglyTypedIdWriteOnlyJsonConverter, PaginationCursor, Payload, TaggedValue (+11 more)

### Community 142 - "mm.rabbitmq"
Cohesion: 0.25
Nodes (8): container_name, environment, image, networks, ports, restart, volumes, mm.rabbitmq

### Community 143 - "Result"
Cohesion: 0.30
Nodes (4): Result, Error, IsFailure, Value

### Community 145 - "Response"
Cohesion: 0.20
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.Create, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt, Setup

### Community 146 - "Response"
Cohesion: 0.17
Nodes (11): Products.Endpoints.Products.v1.My.Get, Endpoint, PackageDimensionsResponse, Response, Attributes, Description, Name, PackageDimensions (+3 more)

### Community 147 - "KeycloakPermissionClient"
Cohesion: 0.10
Nodes (17): GrantedPermission, KeycloakPaths, KeycloakPermissionClient, DecisionRepresentation, Result, PermissionRepresentation, ResourceName, Scopes (+9 more)

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
Cohesion: 0.21
Nodes (4): NetGsmSmsGateway, SendMessageBody, Msg, No

### Community 154 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.27
Nodes (3): KeycloakPermissionAuthorizationHandler, KeycloakPermissionRequirement, Permission

### Community 156 - "SendRequestBody"
Cohesion: 0.29
Nodes (6): SendRequestBody, HtmlContent, Sender, Subject, TextContent, To

### Community 157 - "ProcessedMessage"
Cohesion: 0.24
Nodes (5): ProcessedMessage, ConsumerName, MessageId, ProcessedOn, ProcessedMessageConfiguration

### Community 158 - "Adjust/Request.cs"
Cohesion: 0.24
Nodes (9): Inventory.Endpoints.StockLevels.v1.Adjust, Request, Body, Id, RequestBody, Quantity, Reason, RequestBodyValidator (+1 more)

### Community 159 - "StatusSnapshot"
Cohesion: 0.29
Nodes (7): StatusSnapshot, Active, Committed, Expired, Released, RequiresReconciliation, Unknown

### Community 160 - "BackgroundJobsOptions"
Cohesion: 0.29
Nodes (7): BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount, BackgroundJobsOptionsValidator

### Community 162 - "v1/AddProduct/Request.cs"
Cohesion: 0.08
Nodes (24): Constants, Request, MaxPrice, MaxQuantity, MinPrice, MinQuantity, Name, OwnerId (+16 more)

### Community 163 - "ActivitySource"
Cohesion: 0.13
Nodes (5): RequireFeatureFilter, IamTelemetry, InventoryTelemetry, NotificationsTelemetry, ProductsTelemetry

### Community 164 - "Request"
Cohesion: 0.20
Nodes (11): Products.Endpoints.Products.v1.Update, Request, Body, Id, RequestBody, Description, Name, Price (+3 more)

### Community 165 - "fluentvalidation"
Cohesion: 0.04
Nodes (48): Products.Endpoints.Probe.v1, Inventory.Endpoints.WarehouseConnections.v1.Get, Products.Endpoints.Stores.v1.Reinstate, Products.Endpoints.Products.v1.GetExport, Common.Application.Validation, IAM.Endpoints.Otp.VersionNeutral.SendForEmail, DatabaseOptions, ConnectionString (+40 more)

### Community 167 - "IProductsDbContext"
Cohesion: 0.05
Nodes (27): AuditLogDto, ICurrentUser, Id, IdAsString, Roles, SessionId, PaginationResponse, HasNext (+19 more)

### Community 168 - ".HandleAsync"
Cohesion: 0.36
Nodes (4): GetSeedUserIdsRequest, GetSeedUserIdsResponse, GetSeedUserIdsRequestHandler, Endpoint

### Community 169 - "Response"
Cohesion: 0.18
Nodes (11): Products.Endpoints.Products.v1.Get, PackageDimensionsResponse, Response, Attributes, AvailableQuantity, Description, Name, PackageDimensions (+3 more)

### Community 172 - "Product"
Cohesion: 0.05
Nodes (33): File map _(Build)_, ISearchLocalized, Language, ProductChangedIntegrationEventPublishingHandler, V1ProductNameUpdatedDomainEventHandlers, V1ProductDescriptionUpdatedDomainEvent, V1ProductNameUpdatedDomainEvent, Product (+25 more)

### Community 173 - "Response"
Cohesion: 0.29
Nodes (4): Inventory.Endpoints.StockReservations.v1.ReserveSeries, Endpoint, Response, Ids

### Community 174 - "CachingOptions.cs"
Cohesion: 0.32
Nodes (7): CachingOptionsValidator, Redis, AppName, Host, Password, Port, RedisValidator

### Community 175 - "StockPushDirtyItem"
Cohesion: 0.22
Nodes (9): StockPushDirtyItem, LastErrorKey, MarkedOn, MarkVersion, ProductId, PushAttempts, StockLevelId, StoreId (+1 more)

### Community 176 - "IInventoryDbContext"
Cohesion: 0.04
Nodes (35): Dirty set and lease, IWarehouseGateway, IWarehouseGatewayFactory, WarehouseStockQuantity, IInventoryDbContext, ProductSummaries, StockBulkAdjustJobs, StockLevels (+27 more)

### Community 177 - "CorsOptions"
Cohesion: 0.29
Nodes (7): CorsOptions, AllowCredentials, AllowedHeaders, AllowedMethods, AllowedOrigins, MaxAgeInSeconds, CorsOptionsValidator

### Community 178 - "WebhookDelivery"
Cohesion: 0.22
Nodes (7): WebhookDelivery, ConnectionId, DeliveryId, ReceivedOn, ReservationId, WebhookDeliveryId, WebhookDeliveryConfiguration

### Community 180 - "SmsOptions"
Cohesion: 0.11
Nodes (20): SmsOptions, AppName, AttemptTimeoutSeconds, BaseUrl, MaxPerDay, MaxPerPhoneNumberPerDay, MaxRetryAttempts, MsgHeader (+12 more)

### Community 181 - "RabbitMqOptions"
Cohesion: 0.15
Nodes (13): RabbitMqOptions, DefaultConcurrentMessageLimit, DefaultPrefetchCount, Host, Password, Port, RetryIntervalDeltaMs, RetryLimit (+5 more)

### Community 182 - "ProductTemplate"
Cohesion: 0.22
Nodes (7): ProductTemplate, Brand, Color, IsActive, Model, Products, ProductTemplateConfiguration

### Community 183 - "JobRow"
Cohesion: 0.12
Nodes (13): JobRow, FailureKey, FinishedOn, Progress, QueuedOn, RequestedBy, StartedOn, Status (+5 more)

### Community 184 - "ChunkEncodingFreeS3Client"
Cohesion: 0.17
Nodes (3): ChunkEncodingFreeS3Client, DisableChunkEncodingHandler, Setup

### Community 185 - "SecretProtectionOptions"
Cohesion: 0.28
Nodes (5): Troubleshooting, SecretProtectionOptions, EncryptionKey, PreviousEncryptionKeys, SecretProtectionOptionsValidator

### Community 186 - "Uri"
Cohesion: 0.17
Nodes (6): ReCaptchaResponse, ChallengeTs, ErrorCodes, Hostname, Score, Success

### Community 187 - "V1ProductRemovedFromStoreDomainEvent.cs"
Cohesion: 0.32
Nodes (4): ProductSnapshot, ProductSnapshot, V1ProductRemovedFromStoreDomainEvent, V1ProductRemovedFromStoreDomainEventExtensions

### Community 188 - "Request"
Cohesion: 0.33
Nodes (6): Request, ApiCredential, BaseUrl, Provider, StoreId, RequestValidator

### Community 189 - "StoreId"
Cohesion: 0.07
Nodes (26): IDatabaseSeeder, Priority, ProductImportJob, ClientRequestId, FileName, LastProcessedRow, ObjectKey, ProductsCreated (+18 more)

### Community 190 - "AI-assisted development workflow"
Cohesion: 0.25
Nodes (8): Adapting it, AI-assisted development workflow, `CLAUDE.md`, Guards, The commands, The four layers, The issue-to-pull-request automation, The knowledge graph

### Community 191 - "Response"
Cohesion: 0.33
Nodes (4): Products.Endpoints.Stores.v1.AddProduct, Endpoint, Response, Id

### Community 193 - "Response"
Cohesion: 0.08
Nodes (21): BulkImportOptions, MaxFileBytes, MaxRows, SaveChunkSize, BulkImportOptionsValidator, Response, FailureKey, FileName (+13 more)

### Community 194 - "Keycloak realm as code"
Cohesion: 0.22
Nodes (9): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Realms that already exist, Secrets, Seed users (dev / test only) (+1 more)

### Community 195 - "Response"
Cohesion: 0.33
Nodes (4): Inventory.Endpoints.StockReservations.v1.Reserve, Endpoint, Response, Id

### Community 196 - "Response"
Cohesion: 0.29
Nodes (5): Inventory.Endpoints.WarehouseConnections.v1.RotateWebhookSecret, Endpoint, Response, WebhookPath, WebhookSecret

### Community 197 - ".AdjustBatchAsync"
Cohesion: 0.39
Nodes (3): RowParser, SheetRow, WorkbookReader

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
Cohesion: 0.18
Nodes (9): ProviderStatusSnapshot, Active, Committed, Expired, Released, RequiresReconciliation, Unknown, V1StockReservationProviderStatusMirroredDomainEvent (+1 more)

### Community 213 - "V1StockQuantityChangedDomainEvent"
Cohesion: 0.11
Nodes (11): Inventory.Application.StockLevels.DomainEventHandlers.v1, Inventory.Domain.StockLevels.DomainEvents.v1, V1StockLowThresholdCrossedDomainEventHandlers, V1StockLowThresholdRecoveredDomainEventHandlers, V1StockLowThresholdChangedDomainEvent, MovementTypeSnapshot, Adjusted, Received (+3 more)

### Community 214 - "SmsOtpDispatchOutcome"
Cohesion: 0.33
Nodes (5): OtpDispatchErrors, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled

### Community 215 - "Response"
Cohesion: 0.17
Nodes (10): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, Endpoint, Response, ClientId, DeviceName, Id, IpAddress, IsCurrent (+2 more)

### Community 216 - "IQueryable"
Cohesion: 0.14
Nodes (9): PersistenceQueryableExtensions, SearchProductsByTermRequest, SearchProductsByTermResponse, StockLevelQueries, StockReservationQueries, StoreAccessQueries, Endpoint, PageResponse (+1 more)

### Community 217 - "Platform building blocks"
Cohesion: 0.12
Nodes (14): Authorization, Caching, Feature flags, Object storage, Observability, Options and configuration, Platform building blocks, Results and errors (+6 more)

### Community 218 - "IResxLocalizer"
Cohesion: 0.05
Nodes (37): IAM.Domain.Users, IAM.Endpoints.Otp.VersionNeutral.SendForLogin, IAM.Endpoints.Otp.VersionNeutral.SendForRegistration, IAM.Endpoints.Common.Validations, Constants, CommonValidations, Request, CaptchaToken (+29 more)

### Community 219 - "Response"
Cohesion: 0.25
Nodes (6): Inventory.Endpoints.StockLevels.v1.DailyTotals, Response, CommittedReservations, CommittedUnits, Day, Endpoint

### Community 220 - "AuditLogOptions"
Cohesion: 0.33
Nodes (6): AuditLogOptions, PerSchemaRetentionDays, PurgeBatchSize, RetentionCron, RetentionDays, AuditLogOptionsValidator

### Community 221 - "ReleaseReconcileOutcome"
Cohesion: 0.20
Nodes (8): Inventory.Endpoints.StockReservations.v1.ReconcileRelease, Endpoint, Response, Outcome, ReleaseReconcileOutcome, AlreadyReleased, NotReleased, Released

### Community 222 - "StockBulkAdjustJob"
Cohesion: 0.18
Nodes (11): StockBulkAdjustJob, ClientRequestId, FileName, LastProcessedRow, ObjectKey, Reason, RequeueCount, RowErrors (+3 more)

### Community 223 - "Response"
Cohesion: 0.29
Nodes (4): Products.Endpoints.Stores.v1.Create, Setup, Response, Id

### Community 224 - "ResultToResponseTransformer.cs"
Cohesion: 0.11
Nodes (9): Common.Application.EndpointFilters, ProblemResponse, ResultToAcceptedResponseTransformer, ResultToCreatedResponseTransformer, ResultToResponseTransformer, ResultToResponseTransformer, RouteHandlerBuilderExtensions, RouteHandlerBuilderExtensions (+1 more)

### Community 225 - "Response"
Cohesion: 0.29
Nodes (7): Response, Id, LowStockThreshold, ProductId, ProductName, QuantityOnHand, StoreId

### Community 226 - "Split-Deployment PoC"
Cohesion: 0.33
Nodes (6): Concurrent safety, Files added by this PoC, How to run, Split-Deployment PoC, What this proves, What to check

### Community 227 - "AuditLogEntry"
Cohesion: 0.14
Nodes (9): AuditLogEntry, AggregateId, AggregateType, Event, EventType, Version, AuditableEntityConfiguration, AuditLogEntryConfiguration (+1 more)

### Community 228 - "GetBulkAdjustment/Request.cs"
Cohesion: 0.50
Nodes (4): Inventory.Endpoints.StockLevels.v1.GetBulkAdjustment, Request, Id, RequestValidator

### Community 229 - "RequestBody"
Cohesion: 0.29
Nodes (7): RequestBody, Description, Name, Price, ProductTemplateId, Quantity, Sku

### Community 230 - "Approve/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Stores.v1.Approve, Request, Id, RequestValidator

### Community 231 - "Response"
Cohesion: 0.33
Nodes (4): Products.Endpoints.Stores.v1.My.AddProduct, Endpoint, Response, Id

### Community 233 - "DownloadExport/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.Products.v1.DownloadExport, Request, Id, RequestValidator

### Community 234 - "AddImage/Request.cs"
Cohesion: 0.14
Nodes (12): Products.Endpoints.Stores.v1.Suspend, Inventory.Endpoints.WarehouseConnections.v1.Deactivate, Request, Id, RequestValidator, Request, File, Id (+4 more)

### Community 235 - "Activate/Request.cs"
Cohesion: 0.50
Nodes (4): Products.Endpoints.ProductTemplates.v1.Activate, Request, Id, RequestValidator

### Community 236 - "Response"
Cohesion: 0.33
Nodes (4): Products.Endpoints.Products.v1.AddImage, Response, Id, Url

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
Cohesion: 0.40
Nodes (4): Constants, RouteParameters, ConnectionId, RouteParametersValidator

### Community 245 - "DevicesOptions"
Cohesion: 0.40
Nodes (5): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator

### Community 246 - "Common.Application.DTOs"
Cohesion: 0.09
Nodes (11): Products.Endpoints.Stores.v1.GetApplication, Products.Endpoints.Stores.v1.Search, Products.Endpoints.Stores.v1.ListAll, Products.Endpoints.ProductTemplates.v1.Search, Products.Endpoints.Products.v1.Search, Common.Application.DTOs, Products.Endpoints.Stores.v1.Get, Products.Endpoints.Products.v1.My.Search (+3 more)

### Community 250 - "Request"
Cohesion: 0.40
Nodes (5): Request, From, StoreId, To, RequestValidator

### Community 251 - "Response"
Cohesion: 0.33
Nodes (4): Products.Endpoints.Stores.v1.My.Create, Endpoint, Response, Id

### Community 260 - "ProviderSnapshot"
Cohesion: 0.50
Nodes (4): ProviderSnapshot, Dummy, FormApi, JsonApi

### Community 266 - "SendResponseBody"
Cohesion: 0.50
Nodes (4): SendResponseBody, Code, Description, JobId

### Community 269 - "RouteGroupBuilder"
Cohesion: 0.03
Nodes (36): Setup, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Setup (+28 more)

### Community 273 - "HealthCheckOptions"
Cohesion: 0.40
Nodes (5): HealthCheckOptions, EnableHealthChecks, ReadinessTimeoutInSeconds, StartupTimeoutInSeconds, HealthCheckOptionsValidator

### Community 274 - "ReverseProxyOptions"
Cohesion: 0.33
Nodes (5): ReverseProxyOptions, ForwardLimit, IsEnabled, TrustedNetworks, ReverseProxyOptionsValidator

### Community 284 - ".ReadRows"
Cohesion: 0.15
Nodes (4): StringExtensions, CsvWriter, WorkbookReader, FormulaGuard

### Community 285 - "Common.Domain.Events"
Cohesion: 0.07
Nodes (12): Inventory.Domain.StockReservations.DomainEvents.v1, Common.Domain.Events, Inventory.Application.StockReservations.DomainEventHandlers.v1, Products.Domain.Stores.DomainEvents.v1, V1StockReservationCommittedDomainEventHandlers, V1ProductAddedToStoreDomainEventExtensions, V1ProductImagesRemovedWithProductDomainEvent, V1StoreAddressUpdatedDomainEvent (+4 more)

### Community 323 - "microsoft_aspnetcore_mvc"
Cohesion: 0.03
Nodes (62): Products.Endpoints.Products.v1.GetImport, Products.Endpoints.Stores.v1.Deactivate, Products.Endpoints.ProductTemplates.v1.Deactivate, Products.Endpoints.Products.v1.RemoveImage, Products.Endpoints.Stores.v1.My.RemoveProduct, Inventory.Endpoints.StockReservations.v1.Release, Common.Application.ModelBinders, Products.Endpoints.Stores.v1.RemoveProduct (+54 more)

## Knowledge Gaps
- **1299 isolated node(s):** `UserId`, `Id`, `IdAsString`, `Roles`, `SessionId` (+1294 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1718 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **126 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `Common.Application.Options` to `microsoft_extensions_configuration`, `IServiceCollection`, `ObjectStorageOptions`, `Common.Infrastructure.Extensions`, `Common.Domain.ResultMonad`, `EmailOptions`, `HealthCheckOptions`, `ReverseProxyOptions`, `FullTextSearchOptions`, `OpenApiOptions`, `InventoryOptions`, `CustomRateLimitingOptions`, `PushOptions`, `BackgroundJobsOptions`, `fluentvalidation`, `Common.Domain.StronglyTypedIds`, `ObservabilityOptions`, `KeycloakPermissionAuthorizationHandler.cs`, `CachingOptions.cs`, `CorsOptions`, `CaptchaOptions`, `RabbitMqOptions`, `SmsOptions`, `Request`, `Request`, `Setup.Logger.cs`, `Response`, `Host.Swagger`, `KeycloakOptions`, `system_globalization`, `RequestLoggingOptions`, `IResxLocalizer`, `AlertRecipientsOptions`, `AuditLogOptions`, `ResiliencyOptions`, `Common.Application.Storage`, `Common.InterModuleRequests.Contracts`, `OtpOptions`, `CustomValidator`, `ResxLocalizationOptions`, `InterModuleRequestOptions`, `DevicesOptions`, `Response`, `ProductsOptions`?**
  _High betweenness centrality (0.072) - this node is a cross-community bridge._
- **What connects `UserId`, `Id`, `IdAsString` to the rest of the system?**
  _1299 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `StockMovement` be split into smaller, more focused modules?**
  _Cohesion score 0.12280701754385964 - nodes in this community are weakly interconnected._
- **Why does `Result` connect `Result` to `.DownloadExportAsync`, `StockReservation`, `List`, `DateTimeOffset`, `.GetClientKey`, `ProductId`, `Result`, `IResult`, `IOtpService`, `InterModuleRequestHandler`, `IInterModuleRequestClient`, `NetGsmSmsGateway`, `Common.Domain.Events`, `Error`, `ResultTelemetryExtensions`, `IProductsDbContext`, `Product`, `IInventoryDbContext`, `ProductTemplate`, `.ImportBatchAsync`, `IEnumerable`, `.PageMovementsAsync`, `BrevoEmailGateway`, `ProductImportJobRunner`, `S3ObjectStoreCore`, `PackageDimensions`, `.AdjustBatchAsync`, `EmailOtpDispatchOutcome`, `IIntegrationEventOutbox`, `SmsOtpDispatchOutcome`, `IQueryable`, `SecretProtector`, `ILogger`, `StockLevel`, `Task`, `ProductAttribute`, `WarehouseHttpGateway`, `ApplicationUserId`, `WarehouseConnectionId`?**
  _High betweenness centrality (0.049) - this node is a cross-community bridge._
- **Should `StockReservation` be split into smaller, more focused modules?**
  _Cohesion score 0.0858974358974359 - nodes in this community are weakly interconnected._
- **Why does `Common.Domain.ResultMonad` connect `Common.Domain.ResultMonad` to `ResultToResponseTransformer.cs`, `microsoft_extensions_configuration`, `StockLevelErrors.cs`, `Common.Domain.StronglyTypedIds`, `Common.InterModuleRequests.Contracts`, `Result`, `Result`, `IResult`, `Common.Application.Options`, `ISmsGateway`, `system_diagnostics`, `Error`?**
  _High betweenness centrality (0.037) - this node is a cross-community bridge._
- **Should `List` be split into smaller, more focused modules?**
  _Cohesion score 0.13360323886639677 - nodes in this community are weakly interconnected._