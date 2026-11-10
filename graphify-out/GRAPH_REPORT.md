# Graph Report - modular-monolith-ddd-vsa-webapi  (2026-11-10)

## Corpus Check
- 882 files · ~193,200 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 6550 nodes · 19349 edges · 349 communities (226 shown, 123 thin omitted)
- Extraction: 92% EXTRACTED · 8% INFERRED · 0% AMBIGUOUS · INFERRED: 1560 edges (avg confidence: 0.83)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `34aae5ac`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- StockLevel
- DateTimeOffset
- microsoft_extensions_logging
- Products.Endpoints
- List
- ServiceAccountTokenCache
- Common.InterModuleRequests.Products
- TimeProvider
- Common.Domain.Events
- Result
- Common.Domain.ResultMonad
- ProductExportJobRunner
- ProductId
- Response
- ValueTask
- EmailOptions
- OtpOptions
- ResultToResponseTransformer.cs
- Common.Infrastructure.Extensions
- FullTextSearchOptions
- BoundedCaptureStream
- DeviceRegistration
- IReadOnlyList
- IInterModuleRequestClient
- InventoryOptions
- Response
- CustomRateLimitingOptions
- Endpoint.PageMovementsAsync
- KeycloakPermissionClient
- Error
- FormApiWarehouseGateway
- IDbContext
- Response
- Response
- OutboxMessage
- BackgroundJobsOptions
- AdminRepresentations.cs
- microsoft_entityframeworkcore
- ObservabilityOptions
- KeycloakPermissionPolicyProvider
- ProductSummariesReconciliationJob
- IPublicObjectStore
- PaginationQueryableExtensions
- system_diagnostics
- OutboxOptions
- ProcessedMessage
- Outbox Misuse Check
- Request
- Add Integration Event Command
- host
- Response
- CaptchaOptions
- BulkImportOptions
- KeyedResiliencePipelines
- AlertRecipientsReconciliationJob
- DeviceRegistryReconciliationService
- Cross-Module Reference Violation
- PaginationCursor
- Request
- Bogus Test Data
- SendRequestBody
- Setup.Logger.cs
- WarehouseProvider
- ProductImportRowError
- S3ObjectStoreCore
- RequestBody
- RouteGroupBuilder
- ValueObject
- .AddWarehouseGatewayInfrastructure
- Stores/v1/Update/Request.cs
- Getting started
- Seeder
- FormApiWarehouseGateway
- AsNoTracking Coverage Check
- IIntegrationEventOutbox
- ProvisionUserRequestHandler
- InterModuleRequestHandler
- KeycloakOptions
- Response
- Adjust/Request.cs
- Request
- IOptions
- PaginationRequest
- InventoryDbContext
- Stream
- AlertRecipientsOptions
- StockJobRequeueService
- Configuration-Driven Module Registration
- Request
- StoreAccessReconciliationJob
- IApplicationBuilder
- BackgroundJobsTelemetry
- DatabaseSeederOrchestrator
- StockReservationId
- OpenApiOptions
- ILogger
- Endpoint.GetProductAsync
- Response
- ReplaceCredentials/Request.cs
- NetGsmSmsGateway.SendAsync
- ResiliencyOptions
- NotificationsTelemetry
- StockBulkAdjustJob
- Common.Domain.StronglyTypedIds
- Response
- Common.InterModuleRequests.Contracts
- WarehouseConnectionResponse
- VerifyPhoneOtpResponse
- Response
- PackageDimensions
- WarehouseHttpGateway
- Response
- RequestBody
- Response
- Type
- system_linq_expressions
- ApplicationUserId
- NotificationPayload
- depends_on
- healthcheck
- mm.keycloak
- mm.redis
- Response
- ObjectListPage
- ProductsOptions
- ChangeLowStockThreshold/Request.cs
- WebhookDelivery
- JobStatus
- ProductImportJobRunner
- docker-compose.yml (Base Stack)
- IServiceCollection
- ObjectStorageOptions
- AuditLogRetentionService
- PushOptions
- Request
- HttpContext
- services
- mm.postgres
- AuditableEntityResponse
- IStronglyTypedId
- mm.rabbitmq
- AssignStoreOwnerRoleOnStoreApprovedHandler
- OutboxOptions.cs
- Response
- StockReservationExpirySweepService
- NotificationsTelemetry
- Response
- ResolveReadOnly/Request.cs
- Response
- Response
- README (Boilerplate Overview)
- NetGsmSmsGateway
- KeycloakPermissionAuthorizationHandler
- DateOnly
- CachingOptions.cs
- Result
- Stores/v1/My/Update/Request.cs
- StatusSnapshot
- V1ProductPackageDimensionsChangedDomainEvent
- UploadObjectRequest
- SendRequestBody
- SmsOtpDispatchOutcome
- Response
- CustomValidator
- StockBulkAdjustJobRunner.SaveBatchAsync
- IProductsDbContext
- MigrationGuard.EnsureNoMigrationsPending
- Response
- Notifications.Application/IAssemblyReference.cs
- WebhookDeliveryOutcome
- StoreId
- MassTransitInterModuleRequestClient
- ProductImportJob
- Setup.AddNotificationsSignalR
- IInventoryDbContext
- CorsOptions
- SendForEmail/Request.cs
- HealthCheckOptions
- SmsOptions
- RabbitMqOptions
- IReceiveEndpointConfigurator
- ProductsDbContext
- ChunkEncodingFreeS3Client
- ValidationContextExtensions.IsProduction
- Uri
- NotifyRoleOnCriticalAlertRaisedHandler
- JobHousekeepingOptions
- ProductTemplateId
- AI-assisted development workflow
- ObjectMetadata
- v1/RemoveProduct/Request.cs
- Response
- Keycloak realm as code
- PolymorphicEventConverter
- Response
- SetAttributes/Request.cs
- StronglyTypedIdBinder.cs
- Inventory.Domain.WarehouseConnections
- EmailOtpDispatchOutcome
- KeycloakScopes
- ReCaptchaService
- ReCaptchaResponse
- PaginationQueryableExtensions.ReplaceParameter
- IntegrationEvents (Async Cross-Module)
- IAM Module
- Notifications Module
- Products Module
- Response
- HttpClient
- ProviderStatusSnapshot
- ProductTemplates/Constants.cs
- Response
- Response
- .SearchAsync
- Platform building blocks
- IResxLocalizer
- Request
- AuditLogOptions
- ReleaseReconcileOutcome
- DatabaseOptions.cs
- ModulesOptions.cs
- IServiceProvider
- Response
- Split-Deployment PoC
- SecurityHeadersOptions.cs
- Key decisions
- ProductTemplates/v1/Create/Request.cs
- ProductTemplates/v1/Deactivate/Request.cs
- ProjectionUpsertOutcome
- SearchLanguageResolver
- Approve/Request.cs
- KeycloakTokenClient
- Activate/Request.cs
- WarehouseAddressRefusedException
- KeyedRow
- JsonbListExtensions
- RowOutcome
- ConstraintViolationExtensions
- ResxLocalizationOptions
- Response
- Request
- RequestBody
- DevicesOptions
- Request
- How it works
- IAM.Domain
- WarehouseAddressPolicy
- Request
- Response
- MovementTypeSnapshot
- Products.Domain/IAssemblyReference.cs
- IAM.Application/IAssemblyReference.cs
- Products.Infrastructure/IAssemblyReference.cs
- Products.Application/IAssemblyReference.cs
- Common.Infrastructure/IAssemblyReference.cs
- Notifications.Domain/IAssemblyReference.cs
- IAM.Infrastructure/IAssemblyReference.cs
- ProviderSnapshot
- Request
- Request
- Request
- Request
- Inventory.Application/IAssemblyReference.cs
- Request
- Inventory.Infrastructure/IAssemblyReference.cs
- Inventory.Domain/IAssemblyReference.cs
- RouteGroupBuilder
- Request
- Request
- v1/Request.cs
- Request
- ReverseProxyOptions
- Request
- Request
- Request
- Inventory.Endpoints
- IAM.Endpoints
- Notifications.Infrastructure
- IVariantFeatureManagerExtensions.GetVariantAsync
- DeviceSessionConstants.cs
- .ReadRows
- StockReservations/Constants.cs
- fluentvalidation
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
1. `Result` - 317 edges
2. `Common.Application.Options` - 205 edges
3. `Common.Domain.ResultMonad` - 181 edges
4. `ApplicationUserId` - 129 edges
5. `CustomValidator` - 122 edges
6. `Common.Application.Auth` - 113 edges
7. `IInventoryDbContext` - 108 edges
8. `Common.Application.Extensions` - 107 edges
9. `Common.Domain.StronglyTypedIds` - 106 edges
10. `Common.Application.Validation` - 105 edges

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

## Communities (349 total, 123 thin omitted)

### Community 0 - "StockLevel"
Cohesion: 0.04
Nodes (36): V1StockLevelCreatedDomainEvent, V1StockLowThresholdChangedDomainEvent, V1StockQuantityChangedDomainEventExtensions, StockLevel, LastMovementSequence, LowStockThreshold, Movements, ProductId (+28 more)

### Community 1 - "DateTimeOffset"
Cohesion: 0.04
Nodes (43): Inventory.Domain.StockReservations.DomainEvents.v1, Inventory.Application.StockReservations.DomainEventHandlers.v1, V1StockReservationCommittedDomainEventHandlers, V1StockReservationCommitConflictDetectedDomainEvent, V1StockReservationExpiredDomainEvent, V1StockReservationProviderEventTimeRecordedDomainEvent, V1StockReservationProviderStatusMirroredDomainEvent, V1StockReservationProviderStatusMirroredDomainEventExtensions (+35 more)

### Community 2 - "microsoft_extensions_logging"
Cohesion: 0.03
Nodes (41): Notifications.Application.Sms, Notifications.Infrastructure.Devices, Inventory.Infrastructure.IntegrationEventHandlers, Common.Application.Persistence.Inbox, Notifications.Infrastructure.Email, Products.Infrastructure.Imports, Inventory.Application.IntegrationEventHandlers, Notifications.Infrastructure.Alerts (+33 more)

### Community 5 - "List"
Cohesion: 0.13
Nodes (12): KeycloakAdminClient, UserRepresentation, Attributes, CreatedTimestamp, Credentials, Email, EmailVerified, Enabled (+4 more)

### Community 6 - "ServiceAccountTokenCache"
Cohesion: 0.09
Nodes (13): IServiceAccountTokenProvider, KeycloakPaths, TokenResponseRepresentation, AccessToken, ExpiresIn, RefreshExpiresIn, RefreshToken, SessionState (+5 more)

### Community 7 - "Common.InterModuleRequests.Products"
Cohesion: 0.11
Nodes (5): Products.Infrastructure.InterModuleRequestHandlers, Products.Infrastructure.Spreadsheets, Products.Infrastructure.Caching, Common.InterModuleRequests.Products, Products.Infrastructure.Exports

### Community 8 - "TimeProvider"
Cohesion: 0.11
Nodes (12): JobHousekeepingJob, Endpoint, Request, From, StoreId, To, StockBulkAdjustJobHousekeeping, StockJobsHousekeeping (+4 more)

### Community 9 - "Common.Domain.Events"
Cohesion: 0.04
Nodes (30): Products.Domain.Products.DomainEvents.v1, Common.Domain.Events, Products.Domain.Stores.DomainEvents.v2, Inventory.Domain.WarehouseConnections.DomainEvents.v1, AggregateRoot, Events, Id, Version (+22 more)

### Community 10 - "Result"
Cohesion: 0.05
Nodes (6): IBackgroundJobs, IRecurringBackgroundJobs, AsyncExtensions, BackgroundJobsService, RecurringBackgroundJobsService, ProductsSearchCacheExtensions

### Community 11 - "Common.Domain.ResultMonad"
Cohesion: 0.06
Nodes (39): Inventory.Application.StockLevels, IAM.Endpoints.Captcha.VersionNeutral, Common.Endpoints.Webhooks, Inventory.Application.StockReservations, Common.Application.Search, Products.Application.Exports, Products.Endpoints.Stores.v1.Create, Inventory.Application.Persistence (+31 more)

### Community 12 - "ProductExportJobRunner"
Cohesion: 0.25
Nodes (4): ProductExportJobId, ExportPageRequest, ExportRow, ProductExportJobRunner

### Community 13 - "ProductId"
Cohesion: 0.05
Nodes (24): Products.Endpoints.Stores.v1.My.AddProduct, Products.Endpoints.Stores.v1.AddProduct, GetProductRequest, GetProductResponse, V1ProductCreatedDomainEvent, V1ProductImageAddedDomainEvent, V1ProductImageRemovedDomainEvent, V1ProductSkuAssignedDomainEvent (+16 more)

### Community 14 - "Response"
Cohesion: 0.15
Nodes (12): IAM.Endpoints.Users.VersionNeutral.Me.Get, Response, BirthDate, CreatedOn, Email, FirstName, Id, LastName (+4 more)

### Community 15 - "ValueTask"
Cohesion: 0.06
Nodes (23): ProblemDetailsExtensions, RetryAfterHeaderExtensions, RateLimitingMiddleware, CheckRegistrationRateLimitingPolicy, OnRejected, EmailRateLimitingPolicy, OnRejected, OtpVerifyRateLimitingPolicy (+15 more)

### Community 16 - "EmailOptions"
Cohesion: 0.09
Nodes (23): EmailOptions, ApiKey, AttemptTimeoutSeconds, BaseUrl, MaxPerAddressPerDay, MaxPerDay, MaxRetryAttempts, Provider (+15 more)

### Community 17 - "OtpOptions"
Cohesion: 0.11
Nodes (14): OtpOptions, DummyCode, EmailQuotaWindowMinutes, ExpirationInMinutes, Length, MaxSendsPerEmailPerWindow, MaxSendsPerPhonePerWindow, PhoneQuotaWindowMinutes (+6 more)

### Community 18 - "ResultToResponseTransformer.cs"
Cohesion: 0.10
Nodes (6): Common.Application.EndpointFilters, ProblemResponse, ResultToAcceptedResponseTransformer, ResultToResponseTransformer, StringLocalizerExtensions, RequestBodyLimitFilter

### Community 19 - "Common.Infrastructure.Extensions"
Cohesion: 0.07
Nodes (22): Common.Infrastructure.Modules, Products.Endpoints.Stores, Common.Endpoints.Versioning, Products.Endpoints.Probe, Common.Infrastructure.RateLimiting, Products.Infrastructure.RateLimiting, Products.Endpoints.Products, Inventory.Endpoints.StockLevels (+14 more)

### Community 20 - "FullTextSearchOptions"
Cohesion: 0.11
Nodes (21): Add a new language/culture, Add search to a new entity _(Build checklist)_, Change the vector (weights, fields, or config), Configuration, Extending and maintaining, File map _(Build)_, Full-Text Search, Gotchas (+13 more)

### Community 21 - "BoundedCaptureStream"
Cohesion: 0.04
Nodes (27): RequestLoggingOptions, ExcludedPathPrefixes, LogQueryString, LogRequestBody, LogResponseBody, RequestBodyLogLimitBytes, ResponseBodyLogLimitBytes, SensitiveQueryParamPaths (+19 more)

### Community 22 - "DeviceRegistration"
Cohesion: 0.05
Nodes (36): AuditableEntity, AuditableEntity, Id, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, Version (+28 more)

### Community 23 - "IReadOnlyList"
Cohesion: 0.12
Nodes (9): GrantedPermission, KeycloakUser, KeycloakUserPage, KeycloakUserSession, GetUsersInRolePageRequestHandler, RoleMember, RoleMembersSnapshot, RequestBody (+1 more)

### Community 24 - "IInterModuleRequestClient"
Cohesion: 0.05
Nodes (28): ActorNames, IInterModuleRequest, IInterModuleRequestClient, DeprovisionUserRequest, DeprovisionUserResponse, BindDeviceSessionRequest, BindDeviceSessionResponse, DeactivateDeviceSessionsRequest (+20 more)

### Community 25 - "InventoryOptions"
Cohesion: 0.04
Nodes (41): BackgroundJobs, InventoryOptions, AllowPrivateWarehouseAddresses, BulkAdjustBatchesPerMessage, BulkAdjustJobConcurrentMessageLimit, BulkAdjustMaxStoredRowErrors, BusinessTimeZoneId, DailyTotalsMaxRangeDays (+33 more)

### Community 26 - "Response"
Cohesion: 0.29
Nodes (5): IAM.Endpoints.Otp.VersionNeutral.VerifyEmail, Endpoint, Response, EmailVerificationToken, IsRegistered

### Community 27 - "CustomRateLimitingOptions"
Cohesion: 0.04
Nodes (44): Adding a module or a feature, CustomRateLimitingOptions, CheckRegistration, CreateStore, Email, ExemptPathPrefixes, Global, OtpVerify (+36 more)

### Community 28 - "Endpoint.PageMovementsAsync"
Cohesion: 0.11
Nodes (10): CollectionExtensions, GetUsersRequest, GetUsersResponse, UserSummary, AccessTokenReader, GetUsersRequestHandler, UserDisplayCacheEntry, Endpoint (+2 more)

### Community 29 - "KeycloakPermissionClient"
Cohesion: 0.15
Nodes (11): IKeycloakPermissionClient, HttpContentJsonExtensions, KeycloakPermissionClient, DecisionRepresentation, Result, PermissionRepresentation, ResourceName, Scopes (+3 more)

### Community 30 - "Error"
Cohesion: 0.06
Nodes (28): Inventory.Domain.StockLevels.Errors, Error, Key, ParameterName, StatusCode, SubErrors, Value, IResult (+20 more)

### Community 31 - "FormApiWarehouseGateway"
Cohesion: 0.17
Nodes (3): FormApiWarehouseGateway, HoldEnvelope, HoldResult

### Community 32 - "IDbContext"
Cohesion: 0.13
Nodes (8): IdBound, Value, JobClaimExtensions, IDbContext, AuditLog, ChangeTracker, Database, DbContextExtensions

### Community 33 - "Response"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.Refresh, Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 34 - "Response"
Cohesion: 0.06
Nodes (30): StoreStatus, Active, Inactive, PendingApproval, Suspended, Endpoint, Response, Address (+22 more)

### Community 35 - "OutboxMessage"
Cohesion: 0.04
Nodes (37): IOutboxMessage, CreatedOn, Event, Id, IsProcessed, ProcessedOn, OutboxMessage, CreatedOn (+29 more)

### Community 36 - "BackgroundJobsOptions"
Cohesion: 0.10
Nodes (15): Cross-process call path, How it works, BackgroundJobsOptions, DashboardPath, IsServer, MaxPoolSize, PollingFrequencyInSeconds, WorkerCount (+7 more)

### Community 37 - "AdminRepresentations.cs"
Cohesion: 0.08
Nodes (23): CredentialRepresentation, Temporary, Type, Value, ErrorRepresentation, Error, ErrorDescription, ErrorMessage (+15 more)

### Community 38 - "microsoft_entityframeworkcore"
Cohesion: 0.05
Nodes (29): Common.Infrastructure.Persistence.Inbox, Inventory.Domain.WebhookDeliveries, Inventory.Endpoints.StockLevels.v1.Search, Common.Infrastructure.Persistence, Products.Infrastructure.Persistence, Products.Infrastructure.Persistence.Seeding, Inventory.Infrastructure.InterModuleRequestHandlers, Inventory.Domain.StockCounters (+21 more)

### Community 39 - "ObservabilityOptions"
Cohesion: 0.08
Nodes (16): ObservabilityOptions, AppName, AppVersion, ElasticsearchUrl, EnableMetrics, EnableTracing, LogSink, MinimumLevel (+8 more)

### Community 40 - "KeycloakPermissionPolicyProvider"
Cohesion: 0.11
Nodes (4): ProblemDetailsServiceExtensions, JwtBearerConfigureOptions, KeycloakPermissionPolicyProvider, Setup

### Community 41 - "ProductSummariesReconciliationJob"
Cohesion: 0.15
Nodes (3): ProjectionReconciliationJob, ProductSummariesReconciliationJob, ProductSummariesReconciliationJobRegistrar

### Community 42 - "IPublicObjectStore"
Cohesion: 0.22
Nodes (5): Object storage, ImageSignature, IPublicObjectStore, Endpoint, ImageUpload

### Community 43 - "PaginationQueryableExtensions"
Cohesion: 0.20
Nodes (3): CursorBound, Value, PaginationQueryableExtensions

### Community 44 - "system_diagnostics"
Cohesion: 0.11
Nodes (4): BackgroundJobs.Telemetry, Notifications.Infrastructure.Push.Firebase, LoginMethods, SessionRevokedReasons

### Community 45 - "OutboxOptions"
Cohesion: 0.06
Nodes (26): How modules fit together, IAM, Modules, Outbox, OutboxOptions, BaseBackoffSeconds, BatchSize, ClaimLeaseSeconds (+18 more)

### Community 46 - "ProcessedMessage"
Cohesion: 0.13
Nodes (9): IInboxCleanupTarget, ModuleName, ProcessedMessage, ConsumerName, MessageId, ProcessedOn, InboxStore, ModuleName (+1 more)

### Community 48 - "Request"
Cohesion: 0.25
Nodes (7): Request, DeliveryId, OccurredOn, ProviderReference, ReservationId, Status, StatusWord

### Community 50 - "host"
Cohesion: 0.18
Nodes (11): context, dockerfile, build, container_name, environment, image, networks, ports (+3 more)

### Community 51 - "Response"
Cohesion: 0.17
Nodes (11): Response, FailureKey, FileName, FinishedOn, JobId, ProductsWritten, QueuedOn, StartedOn (+3 more)

### Community 52 - "CaptchaOptions"
Cohesion: 0.15
Nodes (13): CaptchaOptions, AttemptTimeoutSeconds, BaseUrl, CaptchaEndpoint, ClientKey, Provider, ScoreThreshold, SecretKey (+5 more)

### Community 53 - "BulkImportOptions"
Cohesion: 0.16
Nodes (7): RouteHandlerBuilderExtensions, BulkImportOptions, MaxFileBytes, MaxRows, SaveChunkSize, RequestBodyLimitEndpointExtensions, Endpoint

### Community 54 - "KeyedResiliencePipelines"
Cohesion: 0.12
Nodes (6): FixedWindowLease, IsAcquired, MetadataNames, RedisFixedWindowRateLimiter, IdleDuration, KeyedResiliencePipelines

### Community 55 - "AlertRecipientsReconciliationJob"
Cohesion: 0.24
Nodes (5): GetUsersInRolePageRequest, GetUsersInRolePageResponse, RoleUserSummary, AlertRecipientsCursor, AlertRecipientsReconciliationJob

### Community 56 - "DeviceRegistryReconciliationService"
Cohesion: 0.24
Nodes (5): GetActiveSessionIdsRequest, GetActiveSessionIdsResponse, UserSessionIds, GetActiveSessionIdsRequestHandler, DeviceRegistryReconciliationService

### Community 58 - "PaginationCursor"
Cohesion: 0.31
Nodes (3): PaginationCursor, Payload, TaggedValue

### Community 59 - "Request"
Cohesion: 0.07
Nodes (25): Request, BirthDate, CaptchaToken, ClientId, DeviceId, DeviceName, FirstName, LastName (+17 more)

### Community 61 - "SendRequestBody"
Cohesion: 0.29
Nodes (6): SendRequestBody, HtmlContent, Sender, Subject, TextContent, To

### Community 62 - "Setup.Logger.cs"
Cohesion: 0.06
Nodes (6): Host, Host.Configurations, Host.Infrastructure, Setup, StringExtensions, Program

### Community 63 - "WarehouseProvider"
Cohesion: 0.15
Nodes (10): V1WarehouseConnectionCreatedDomainEventExtensions, WarehouseProvider, Dummy, FormApi, JsonApi, Request, ApiCredential, BaseUrl (+2 more)

### Community 64 - "ProductImportRowError"
Cohesion: 0.23
Nodes (6): ProductImportRowError, BatchSummary, RowResult, Created, Unchanged, Updated

### Community 66 - "RequestBody"
Cohesion: 0.18
Nodes (10): RequestBody, FirstDeadline, IntervalDays, OccurrenceCount, ProductId, QuantityPerOccurrence, Warehouses, WarehouseAllocation (+2 more)

### Community 67 - "RouteGroupBuilder"
Cohesion: 0.05
Nodes (24): AuditLogDto, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint (+16 more)

### Community 69 - ".AddWarehouseGatewayInfrastructure"
Cohesion: 0.15
Nodes (6): Rate limiting and resilience, WarehouseOutcomeUnknownException, Setup, IWarehouseHostResolver, SystemWarehouseHostResolver, WarehouseConnectGuard

### Community 70 - "Stores/v1/Update/Request.cs"
Cohesion: 0.19
Nodes (10): Products.Endpoints.Stores.v1.Update, Request, Body, Id, RequestBody, Address, Description, Name (+2 more)

### Community 71 - "Getting started"
Cohesion: 0.12
Nodes (17): Configuration, Connect a store to a warehouse system, Errors, Getting started, Prerequisites, Queued work: accept, queue, poll, Run the API from the command line instead, Run the load test (+9 more)

### Community 72 - "Seeder"
Cohesion: 0.15
Nodes (5): GetSeedUserIdsRequest, GetSeedUserIdsResponse, GetSeedUserIdsRequestHandler, Endpoint, Seeder

### Community 73 - "FormApiWarehouseGateway"
Cohesion: 0.14
Nodes (6): ErrorResponseBody, JsonApiWarehouseGateway, PushStockItem, PushStockRequestBody, ReleaseRequestBody, ReleaseResponseBody

### Community 75 - "IIntegrationEventOutbox"
Cohesion: 0.04
Nodes (39): Dirty set and lease, DomainEventHandlerBase, IEventHandler, IEventHandlerWrapper, IIntegrationEventOutbox, StockPushDueIntegrationEvent, CoalesceKey, StockPushInitialMarkRequestedIntegrationEvent (+31 more)

### Community 76 - "ProvisionUserRequestHandler"
Cohesion: 0.16
Nodes (10): ProvisionUserFailureReason, EmailAlreadyRegistered, IdentityProviderUnavailable, None, PhoneNumberAlreadyRegistered, Rejected, ProvisionUserRequest, ProvisionUserResponse (+2 more)

### Community 77 - "InterModuleRequestHandler"
Cohesion: 0.17
Nodes (6): IInterModuleRequestHandler, InterModuleRequestHandler, DeviceSession, GetDeviceSessionsRequest, GetDeviceSessionsResponse, GetDeviceSessionsRequestHandler

### Community 78 - "KeycloakOptions"
Cohesion: 0.11
Nodes (18): KeycloakOptions, AttemptTimeoutSeconds, Authority, BaseUrl, DecisionCacheMaxDurationSeconds, Realm, RequireHttpsMetadata, ResourceClientId (+10 more)

### Community 79 - "Response"
Cohesion: 0.17
Nodes (11): IAM.Endpoints.Users.VersionNeutral.Search, Response, BirthDate, CreatedOn, Email, Enabled, FirstName, Id (+3 more)

### Community 80 - "Adjust/Request.cs"
Cohesion: 0.21
Nodes (9): Inventory.Endpoints.StockLevels.v1.Adjust, Request, Body, Id, RequestBody, Quantity, Reason, RequestBodyValidator (+1 more)

### Community 81 - "Request"
Cohesion: 0.17
Nodes (11): Request, Email, EmailVerificationToken, FirstName, LastName, Password, PhoneNumber, StoreAddress (+3 more)

### Community 82 - "IOptions"
Cohesion: 0.04
Nodes (51): Inventory, Notifications, Projections and reconciliation, IntegrationEventHandlerBase, MaxEventAge, CachingOptions, AllowInMemoryOnlyInProduction, EntryDefaults (+43 more)

### Community 83 - "PaginationRequest"
Cohesion: 0.03
Nodes (64): Products.Endpoints.Stores.v1.My.AuditLog, PaginationRequest, After, IncludeTotal, PageNumber, PageSize, ShouldIncludeTotal, Skip (+56 more)

### Community 84 - "InventoryDbContext"
Cohesion: 0.04
Nodes (49): Pagination and search, ProjectionEntity, SourceVersion, AuditableEntityConfiguration, JobRowConfiguration, LikePattern, TrigramIndexExtensions, StronglyTypedIdValueConverter (+41 more)

### Community 85 - "Stream"
Cohesion: 0.23
Nodes (3): Endpoint, FileDownload, FileReference

### Community 86 - "AlertRecipientsOptions"
Cohesion: 0.20
Nodes (7): AlertRecipientsOptions, EmailHtmlBodyTemplate, EmailSubjectTemplate, EmailTextBodyTemplate, ReconcileCron, RoleName, AlertRecipientsReconcileJobRegistrar

### Community 89 - "Request"
Cohesion: 0.18
Nodes (10): Request, Body, Id, RequestBody, HeightCm, LengthCm, WeightKg, WidthCm (+2 more)

### Community 90 - "StoreAccessReconciliationJob"
Cohesion: 0.16
Nodes (10): ProjectionReconciliationOptions, MaxPages, PageSize, ProjectionReconciliationOptionsValidator, GetStoresPageRequest, GetStoresPageResponse, StorePageItem, StoreAccessReconciliationCursor (+2 more)

### Community 91 - "IApplicationBuilder"
Cohesion: 0.06
Nodes (12): IDatabaseSeeder, Priority, Setup, ApplyAuditingInterceptor, ApplySearchLanguageInterceptor, Setup, Setup, Setup (+4 more)

### Community 94 - "StockReservationId"
Cohesion: 0.04
Nodes (34): Secret protection, Webhooks, SecretGenerator, SecretProtector, StockReservationId, V1WarehouseConnectionCreatedDomainEvent, WarehouseConnection, BaseUrl (+26 more)

### Community 95 - "OpenApiOptions"
Cohesion: 0.07
Nodes (14): Host.Swagger, OpenApiOptions, ContactEmail, ContactName, Description, EnableSwagger, LicenseName, LicenseUrl (+6 more)

### Community 96 - "ILogger"
Cohesion: 0.04
Nodes (13): EventDispatcher, OutboxSaveHelper, GlobalExceptionHandlingMiddleware, LoginCompletion, RegistrationCompletion, DummyEmailGateway, NotificationsHub, DummySmsGateway (+5 more)

### Community 97 - "Endpoint.GetProductAsync"
Cohesion: 0.33
Nodes (4): GetStockLevelRequest, GetStockLevelResponse, GetStockLevelRequestHandler, Endpoint

### Community 98 - "Response"
Cohesion: 0.14
Nodes (14): Response, FailureKey, FileName, FinishedOn, JobId, ProcessedRows, ProductsCreated, ProductsUpdated (+6 more)

### Community 99 - "ReplaceCredentials/Request.cs"
Cohesion: 0.24
Nodes (8): Inventory.Endpoints.WarehouseConnections.v1.ReplaceCredentials, Request, Body, Id, RequestBody, ApiCredential, RequestBodyValidator, RequestValidator

### Community 100 - "NetGsmSmsGateway.SendAsync"
Cohesion: 0.06
Nodes (17): OtpCacheEntry, SendEmailOtpRequest, SendEmailOtpResponse, EmailMessage, IOtpService, OtpVerificationOutcome, InvalidOtp, Success (+9 more)

### Community 101 - "ResiliencyOptions"
Cohesion: 0.09
Nodes (23): KeyedResilienceProfile, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds, RateLimitFailOpen, RateLimitPermits (+15 more)

### Community 102 - "NotificationsTelemetry"
Cohesion: 0.24
Nodes (5): RequireFeatureFilter, RouteHandlerBuilderExtensions, IamTelemetry, InventoryTelemetry, ProductsTelemetry

### Community 103 - "StockBulkAdjustJob"
Cohesion: 0.10
Nodes (17): StockBulkAdjustJob, ClientRequestId, FileName, LastProcessedRow, ObjectKey, Reason, RequeueCount, RowErrors (+9 more)

### Community 104 - "Common.Domain.StronglyTypedIds"
Cohesion: 0.03
Nodes (31): Products.Endpoints.Stores.v1.GetApplication, Common.InterModuleRequests, Common.Domain.StronglyTypedIds, Products.Endpoints.Stores.v1.Search, Products.Endpoints.Stores.v1.ListAll, Products.Endpoints.Stores.v1.Apply, Products.Domain.Stores.Errors, Common.Infrastructure.Localization (+23 more)

### Community 105 - "Response"
Cohesion: 0.22
Nodes (7): IAM.Endpoints.Users.VersionNeutral.SelfRegisterByEmail, Endpoint, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 106 - "Common.InterModuleRequests.Contracts"
Cohesion: 0.05
Nodes (30): Notifications.Application.Otp, Common.Endpoints, Notifications.Application.Push, IAM.Infrastructure.Keycloak.Representations, Common.Application.Caching, Common.Application.FeatureManagement, IAM.Endpoints.Otp, Notifications.Application.Persistence (+22 more)

### Community 107 - "WarehouseConnectionResponse"
Cohesion: 0.18
Nodes (8): Endpoint, Endpoint, Endpoint, WarehouseConnectionResponse, BaseUrl, Provider, Status, StoreId

### Community 108 - "VerifyPhoneOtpResponse"
Cohesion: 0.20
Nodes (8): OtpVerificationFailureReason, InvalidOtp, None, TooManyAttempts, VerifyPhoneOtpRequest, VerifyPhoneOtpResponse, VerifyPhoneOtpResponseExtensions, VerifyPhoneOtpRequestHandler

### Community 109 - "Response"
Cohesion: 0.33
Nodes (4): IAM.Endpoints.Users.VersionNeutral.CheckRegistration, Endpoint, Response, IsRegistered

### Community 110 - "PackageDimensions"
Cohesion: 0.20
Nodes (6): PackageDimensions, HeightCm, LengthCm, VolumeCm3, WeightKg, WidthCm

### Community 111 - "WarehouseHttpGateway"
Cohesion: 0.18
Nodes (3): HttpClientKeyedExtensions, WarehouseHttpGateway, WarehouseResiliencePipelines

### Community 112 - "Response"
Cohesion: 0.50
Nodes (3): IAM.Endpoints.Captcha.VersionNeutral.ClientKey.Get, Response, ClientKey

### Community 113 - "RequestBody"
Cohesion: 0.04
Nodes (48): Products.Endpoints.Products.v1.My.Update, Products.Endpoints.Products.v1.Update, Request, Body, Id, RequestBody, Quantity, Reason (+40 more)

### Community 114 - "Response"
Cohesion: 0.17
Nodes (11): IAM.Endpoints.Users.VersionNeutral.Get, Response, BirthDate, CreatedOn, Email, Enabled, FirstName, Id (+3 more)

### Community 115 - "Type"
Cohesion: 0.18
Nodes (8): InterModuleRequestOptions, DependencyUnavailableRetryAfterSeconds, HandlerConcurrentMessageLimit, HandlerPrefetchCount, Timeouts, TimeoutSeconds, InterModuleRequestHandlerDefinition, RequestType

### Community 117 - "ApplicationUserId"
Cohesion: 0.06
Nodes (20): Job rows, IBackgroundUserContext, UserId, CacheKeys, For, ApplicationUserId, IsEmpty, Value (+12 more)

### Community 118 - "NotificationPayload"
Cohesion: 0.19
Nodes (4): INotificationDispatcher, INotificationsClient, NotificationPayload, SignalRNotificationDispatcher

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
Cohesion: 0.07
Nodes (18): IAM.Endpoints.Users.VersionNeutral.SelfRegister, IdentityScheme, Email, PhoneNumber, IdentitySchemeOptions, Scheme, IdentitySchemeOptionsValidator, Setup (+10 more)

### Community 125 - "ProductsOptions"
Cohesion: 0.10
Nodes (19): ProductsOptions, ExportJobConcurrentMessageLimit, ExportMaxRows, ExportPageSize, ImageMaxFileBytes, ImportBatchesPerMessage, ImportJobConcurrentMessageLimit, ImportMaxStoredRowErrors (+11 more)

### Community 126 - "ChangeLowStockThreshold/Request.cs"
Cohesion: 0.24
Nodes (8): Inventory.Endpoints.StockLevels.v1.ChangeLowStockThreshold, Request, Body, Id, RequestBody, Threshold, RequestBodyValidator, RequestValidator

### Community 127 - "WebhookDelivery"
Cohesion: 0.09
Nodes (9): StronglyTypedIdHelper, WebhookDelivery, ConnectionId, DeliveryId, ReceivedOn, ReservationId, WebhookDeliveryId, WebhookDeliveryConfiguration (+1 more)

### Community 128 - "JobStatus"
Cohesion: 0.25
Nodes (6): JobStatus, Failed, Queued, Running, Succeeded, FileRow

### Community 129 - "ProductImportJobRunner"
Cohesion: 0.12
Nodes (8): SmsTemplatesOptions, Otp, ProductImportJobRunner, Slice, ProductRow, RowParser, SheetRow, WorkbookReader

### Community 130 - "docker-compose.yml (Base Stack)"
Cohesion: 0.25
Nodes (10): Observability (OpenTelemetry), Aspire Dashboard Service (mm.aspire-dashboard), Host Service (mm.host), external, name, networks, local_shared_network, Postgres Service (mm.postgres) (+2 more)

### Community 131 - "IServiceCollection"
Cohesion: 0.05
Nodes (25): Setup, Setup, Setup, Setup, Setup, Setup, Setup, Setup (+17 more)

### Community 132 - "ObjectStorageOptions"
Cohesion: 0.08
Nodes (25): ObjectStorageOptions, AccessKey, AdditionalTransientStatusCodes, AttemptTimeoutSeconds, CircuitBreakerBreakDurationSeconds, CircuitBreakerFailureRatio, CircuitBreakerMinimumThroughput, CircuitBreakerSamplingDurationSeconds (+17 more)

### Community 135 - "PushOptions"
Cohesion: 0.11
Nodes (20): FirebaseServiceAccountOptions, ClientEmail, ClientId, PrivateKey, PrivateKeyId, ProjectId, TokenUri, Type (+12 more)

### Community 136 - "Request"
Cohesion: 0.20
Nodes (9): Request, ClientId, DeviceId, DeviceName, Email, EmailVerificationToken, Password, PushToken (+1 more)

### Community 137 - "HttpContext"
Cohesion: 0.11
Nodes (12): FixedWindow, FailOpen, Limit, PeriodInMs, QueueLimit, RequestBodyLimitMetadata, MaxBodyBytes, RequestBodyLimitMiddleware (+4 more)

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

### Community 143 - "AssignStoreOwnerRoleOnStoreApprovedHandler"
Cohesion: 0.15
Nodes (7): Transactional outbox and inbox, StatelessInboxStore, Instance, ICoalescableIntegrationEvent, CoalesceKey, StoreApprovedIntegrationEvent, AssignStoreOwnerRoleOnStoreApprovedHandler

### Community 144 - "OutboxOptions.cs"
Cohesion: 0.27
Nodes (7): OutboxCleanupSettings, BatchSize, CronSchedule, Enabled, RetentionDays, OutboxCleanupSettingsValidator, OutboxOptionsValidator

### Community 145 - "Response"
Cohesion: 0.20
Nodes (7): IAM.Endpoints.Tokens.VersionNeutral.Create, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt, Setup

### Community 148 - "Response"
Cohesion: 0.12
Nodes (13): Inventory.Endpoints.StockReservations.v1.Get, Response, HasPendingReleaseAttempt, Id, IsReadOnly, ProductId, ProviderReference, ProviderStatus (+5 more)

### Community 149 - "ResolveReadOnly/Request.cs"
Cohesion: 0.31
Nodes (6): Inventory.Endpoints.StockReservations.v1.ResolveReadOnly, Request, Body, Id, RequestBodyValidator, RequestValidator

### Community 150 - "Response"
Cohesion: 0.22
Nodes (6): Inventory.Endpoints.StockLevels.v1.DailyTotals, Response, CommittedReservations, CommittedUnits, Day, Endpoint

### Community 151 - "Response"
Cohesion: 0.29
Nodes (7): Response, Address, CreatedOn, Description, Id, Name, OwnerId

### Community 153 - "NetGsmSmsGateway"
Cohesion: 0.07
Nodes (17): BrevoEmailGateway, SendContact, Email, Name, SendErrorBody, Code, Message, SendResponseBody (+9 more)

### Community 154 - "KeycloakPermissionAuthorizationHandler"
Cohesion: 0.10
Nodes (5): KeycloakPermission, HttpContextTargetingContextAccessor, KeycloakPermissionAuthorizationHandler, KeycloakPermissionRequirement, Permission

### Community 156 - "CachingOptions.cs"
Cohesion: 0.15
Nodes (13): CachingEntryDefaults, Duration, FactoryHardTimeout, FactorySoftTimeout, FailSafeMaxDuration, FailSafeThrottleDuration, CachingOptionsValidator, Redis (+5 more)

### Community 157 - "Result"
Cohesion: 0.05
Nodes (15): FeatureFlagResultExtensions, Result, Result, Error, IsFailure, Value, Error, IsFailure (+7 more)

### Community 158 - "Stores/v1/My/Update/Request.cs"
Cohesion: 0.33
Nodes (5): Request, Address, Description, Name, RequestValidator

### Community 159 - "StatusSnapshot"
Cohesion: 0.29
Nodes (7): StatusSnapshot, Active, Committed, Expired, Released, RequiresReconciliation, Unknown

### Community 160 - "V1ProductPackageDimensionsChangedDomainEvent"
Cohesion: 0.33
Nodes (3): DimensionsSnapshot, V1ProductPackageDimensionsChangedDomainEvent, V1ProductPackageDimensionsChangedDomainEventExtensions

### Community 162 - "SendRequestBody"
Cohesion: 0.29
Nodes (6): SendRequestBody, AppName, Encoding, IysFilter, Messages, MsgHeader

### Community 163 - "SmsOtpDispatchOutcome"
Cohesion: 0.33
Nodes (5): OtpDispatchErrors, SmsOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled

### Community 164 - "Response"
Cohesion: 0.22
Nodes (8): Endpoint, Response, Address, Description, Name, OwnerId, ProductCount, Status

### Community 165 - "CustomValidator"
Cohesion: 0.02
Nodes (56): AlertRecipientsOptionsValidator, AuditLogOptionsValidator, BackgroundJobsOptionsValidator, BulkImportOptionsValidator, CustomRateLimitingOptionsValidator, FixedWindowValidator, FullTextSearchOptionsValidator, InterModuleRequestOptionsValidator (+48 more)

### Community 166 - "StockBulkAdjustJobRunner.SaveBatchAsync"
Cohesion: 0.15
Nodes (8): StockBulkAdjustRowError, AdjustmentRow, RowParser, BatchSummary, RowResult, Adjusted, SheetRow, WorkbookReader

### Community 167 - "IProductsDbContext"
Cohesion: 0.04
Nodes (44): ICurrentUser, Id, IdAsString, Roles, SessionId, PaginationResponse, HasNext, HasPrevious (+36 more)

### Community 168 - "MigrationGuard.EnsureNoMigrationsPending"
Cohesion: 0.39
Nodes (3): AutoMigrateMarker, IAutoMigrateMarker, MigrationGuard

### Community 169 - "Response"
Cohesion: 0.06
Nodes (25): Products.Endpoints.Products.v1.My.Get, Products.Endpoints.Products.v1.Get, AttributeSnapshot, V1ProductAttributesReplacedDomainEvent, V1ProductAttributesReplacedDomainEventExtensions, ProductAttribute, PackageDimensionsResponse, Response (+17 more)

### Community 171 - "WebhookDeliveryOutcome"
Cohesion: 0.25
Nodes (7): WebhookDeliveryOutcome, Applied, Duplicate, Ignored, Mirrored, Stale, Unrecognized

### Community 172 - "StoreId"
Cohesion: 0.03
Nodes (54): Products.Application.Stores.DomainEventHandlers.v1, Products.Domain.Stores.DomainEvents.v1, Products, StoreCreatedIntegrationEvent, ProductImageObjectsOrphanedIntegrationEventPublishingHandler, V1ProductImagesRemovedWithProductDomainEventHandlers, V1StoreApprovedDomainEventHandlers, SimulateSomeBusinessHandler (+46 more)

### Community 173 - "MassTransitInterModuleRequestClient"
Cohesion: 0.33
Nodes (3): Inter-module requests, MassTransitInterModuleRequestClient, ProductsCacheKeys

### Community 174 - "ProductImportJob"
Cohesion: 0.11
Nodes (14): ProductImportJob, ClientRequestId, FileName, LastProcessedRow, ObjectKey, ProductsCreated, ProductsUpdated, ProductTemplateId (+6 more)

### Community 176 - "IInventoryDbContext"
Cohesion: 0.07
Nodes (11): IInboxStore, IWarehouseGateway, IWarehouseGatewayFactory, WarehouseStockQuantity, StockLevelId, Endpoint, PushedMark, StockPushMarks (+3 more)

### Community 177 - "CorsOptions"
Cohesion: 0.25
Nodes (7): CorsOptions, AllowCredentials, AllowedHeaders, AllowedMethods, AllowedOrigins, MaxAgeInSeconds, CorsOptionsValidator

### Community 178 - "SendForEmail/Request.cs"
Cohesion: 0.40
Nodes (4): Request, CaptchaToken, Email, RequestValidator

### Community 179 - "HealthCheckOptions"
Cohesion: 0.33
Nodes (5): HealthCheckOptions, EnableHealthChecks, ReadinessTimeoutInSeconds, StartupTimeoutInSeconds, HealthCheckOptionsValidator

### Community 180 - "SmsOptions"
Cohesion: 0.10
Nodes (19): SmsOptions, AppName, AttemptTimeoutSeconds, BaseUrl, MaxPerDay, MaxPerPhoneNumberPerDay, MaxRetryAttempts, MsgHeader (+11 more)

### Community 181 - "RabbitMqOptions"
Cohesion: 0.17
Nodes (12): RabbitMqOptions, DefaultConcurrentMessageLimit, DefaultPrefetchCount, Host, Password, Port, RetryIntervalDeltaMs, RetryLimit (+4 more)

### Community 183 - "ProductsDbContext"
Cohesion: 0.07
Nodes (18): JobRow, FailureKey, FinishedOn, Progress, QueuedOn, RequestedBy, StartedOn, Status (+10 more)

### Community 184 - "ChunkEncodingFreeS3Client"
Cohesion: 0.15
Nodes (3): ChunkEncodingFreeS3Client, DisableChunkEncodingHandler, Setup

### Community 185 - "ValidationContextExtensions.IsProduction"
Cohesion: 0.06
Nodes (16): Troubleshooting, CaptchaOptionsValidator, InventoryOptionsValidator, KeycloakOptionsValidator, OtpOptionsValidator, SecretProtectionOptions, EncryptionKey, PreviousEncryptionKeys (+8 more)

### Community 187 - "NotifyRoleOnCriticalAlertRaisedHandler"
Cohesion: 0.17
Nodes (3): CriticalAlertRaisedIntegrationEvent, UserSignedInIntegrationEvent, NotifyRoleOnCriticalAlertRaisedHandler

### Community 188 - "JobHousekeepingOptions"
Cohesion: 0.33
Nodes (5): JobHousekeepingOptions, PageSize, RetentionHours, StaleAfterMinutes, JobHousekeepingOptionsValidator

### Community 189 - "ProductTemplateId"
Cohesion: 0.07
Nodes (25): Products.Endpoints.ProductTemplates.v1.Create, ProductTemplate, Brand, Color, IsActive, Model, Products, ProductTemplateId (+17 more)

### Community 190 - "AI-assisted development workflow"
Cohesion: 0.25
Nodes (8): Adapting it, AI-assisted development workflow, `CLAUDE.md`, Guards, The commands, The four layers, The issue-to-pull-request automation, The knowledge graph

### Community 192 - "v1/RemoveProduct/Request.cs"
Cohesion: 0.40
Nodes (4): Request, Id, ProductId, RequestValidator

### Community 193 - "Response"
Cohesion: 0.14
Nodes (14): Response, FailureKey, FileName, FinishedOn, JobId, ProcessedRows, QueuedOn, Reason (+6 more)

### Community 194 - "Keycloak realm as code"
Cohesion: 0.22
Nodes (9): Editing the realm, Identity audit trail, Keycloak realm as code, Model, Production, Realms that already exist, Secrets, Seed users (dev / test only) (+1 more)

### Community 196 - "Response"
Cohesion: 0.29
Nodes (5): Inventory.Endpoints.WarehouseConnections.v1.RotateWebhookSecret, Endpoint, Response, WebhookPath, WebhookSecret

### Community 197 - "SetAttributes/Request.cs"
Cohesion: 0.22
Nodes (8): AttributeItem, Name, Value, Request, Body, Id, RequestBodyValidator, RequestValidator

### Community 199 - "Inventory.Domain.WarehouseConnections"
Cohesion: 0.06
Nodes (15): Inventory.Application.WarehouseConnections, Inventory.Endpoints.StockReservations.v1.ReserveSeries, Inventory.Domain.WarehouseConnections.Errors, Inventory.Infrastructure.Gateway, Inventory.Domain.StockReservations, Inventory.Domain.StockReservations.Errors, Inventory.Domain.WarehouseConnections, Inventory.Endpoints.WarehouseConnections.v1.Create (+7 more)

### Community 200 - "EmailOtpDispatchOutcome"
Cohesion: 0.33
Nodes (5): EmailOtpDispatchErrors, EmailOtpDispatchOutcome, ProviderUnavailable, Sent, Throttled

### Community 201 - "KeycloakScopes"
Cohesion: 0.17
Nodes (11): Devices, Hangfire, KeycloakScopes, Products, ProductTemplates, Sessions, StockLevels, StockReservations (+3 more)

### Community 203 - "ReCaptchaResponse"
Cohesion: 0.29
Nodes (6): ReCaptchaResponse, ChallengeTs, ErrorCodes, Hostname, Score, Success

### Community 210 - "Response"
Cohesion: 0.22
Nodes (8): Endpoint, Response, Address, Description, Name, OwnerId, ProductCount, Status

### Community 212 - "ProviderStatusSnapshot"
Cohesion: 0.29
Nodes (7): ProviderStatusSnapshot, Active, Committed, Expired, Released, RequiresReconciliation, Unknown

### Community 214 - "Response"
Cohesion: 0.40
Nodes (4): Products.Endpoints.Products.v1.AddImage, Response, Id, Url

### Community 215 - "Response"
Cohesion: 0.17
Nodes (10): IAM.Endpoints.Tokens.VersionNeutral.Sessions.List, Endpoint, Response, ClientId, DeviceName, Id, IpAddress, IsCurrent (+2 more)

### Community 216 - ".SearchAsync"
Cohesion: 0.23
Nodes (7): SearchProductsByTermRequest, SearchProductsByTermResponse, Endpoint, Request, ProductName, PageResponse, SearchProductsByTermRequestHandler

### Community 217 - "Platform building blocks"
Cohesion: 0.15
Nodes (12): Authorization, Caching, Feature flags, Observability, Options and configuration, Platform building blocks, Results and errors, Checkout (+4 more)

### Community 218 - "IResxLocalizer"
Cohesion: 0.05
Nodes (31): Products.Endpoints.Products.v1.ListImports, Inventory.Endpoints.StockLevels.v1.ListBulkAdjustments, Inventory.Endpoints.WarehouseConnections.v1.My.Search, CommonValidations, Request, CaptchaToken, PhoneNumber, RequestValidator (+23 more)

### Community 219 - "Request"
Cohesion: 0.10
Nodes (15): Endpoint, Request, ClientRequestId, File, Reason, RequestValidator, Request, File (+7 more)

### Community 220 - "AuditLogOptions"
Cohesion: 0.40
Nodes (5): AuditLogOptions, PerSchemaRetentionDays, PurgeBatchSize, RetentionCron, RetentionDays

### Community 221 - "ReleaseReconcileOutcome"
Cohesion: 0.25
Nodes (7): Inventory.Endpoints.StockReservations.v1.ReconcileRelease, Response, Outcome, ReleaseReconcileOutcome, AlreadyReleased, NotReleased, Released

### Community 222 - "DatabaseOptions.cs"
Cohesion: 0.50
Nodes (3): DatabaseOptions, ConnectionString, DatabaseOptionsValidator

### Community 223 - "ModulesOptions.cs"
Cohesion: 0.50
Nodes (3): ModulesOptions, EnabledModules, ModulesOptionsValidator

### Community 224 - "IServiceProvider"
Cohesion: 0.07
Nodes (7): ResultToCreatedResponseTransformer, StockJobsHousekeepingRegistrar, StockReleaseReconcileSweepJobRegistrar, StoreAccessReconciliationJobRegistrar, WebhookDeliveryRetentionJobRegistrar, DeviceRegistryReconcileJobRegistrar, ProductJobsHousekeepingRegistrar

### Community 225 - "Response"
Cohesion: 0.29
Nodes (7): Response, Id, LowStockThreshold, ProductId, ProductName, QuantityOnHand, StoreId

### Community 226 - "Split-Deployment PoC"
Cohesion: 0.33
Nodes (6): Concurrent safety, Files added by this PoC, How to run, Split-Deployment PoC, What this proves, What to check

### Community 227 - "SecurityHeadersOptions.cs"
Cohesion: 0.50
Nodes (3): SecurityHeadersOptions, Headers, SecurityHeadersOptionsValidator

### Community 228 - "Key decisions"
Cohesion: 0.29
Nodes (7): 1. Per-row authored language, not a fixed column language, 2. Two-layer vector: a universal layer plus a per-language prose layer, 3. Generated column with an `IMMUTABLE` wrapper function (not a trigger), 4. Accent folding via custom `*_unaccent` configs, 5. Language resolved from request culture, never from a query parameter, 6. No language filter on read, Key decisions

### Community 229 - "ProductTemplates/v1/Create/Request.cs"
Cohesion: 0.33
Nodes (5): Request, Brand, Color, Model, RequestValidator

### Community 230 - "ProductTemplates/v1/Deactivate/Request.cs"
Cohesion: 0.50
Nodes (3): Request, Id, RequestValidator

### Community 231 - "ProjectionUpsertOutcome"
Cohesion: 0.40
Nodes (4): ProjectionUpsertOutcome, Inserted, Stale, Updated

### Community 233 - "Approve/Request.cs"
Cohesion: 0.50
Nodes (3): Request, Id, RequestValidator

### Community 234 - "KeycloakTokenClient"
Cohesion: 0.09
Nodes (9): SecurityAlertType, SessionRevokedTokenReuse, SendSecurityAlertRequest, SendSecurityAlertResponse, KeycloakTokenClient, IPushGateway, SendSecurityAlertRequestHandler, DummyPushGateway (+1 more)

### Community 235 - "Activate/Request.cs"
Cohesion: 0.50
Nodes (3): Request, Id, RequestValidator

### Community 237 - "KeyedRow"
Cohesion: 0.40
Nodes (4): KeyedRow, Item, Sort, Tie

### Community 239 - "RowOutcome"
Cohesion: 0.40
Nodes (5): RowOutcome, Created, Rejected, Unchanged, Updated

### Community 241 - "ResxLocalizationOptions"
Cohesion: 0.33
Nodes (5): Localization, ResxLocalizationOptions, DefaultCulture, SupportedCultures, ResxLocalizationOptionsValidator

### Community 242 - "Response"
Cohesion: 0.25
Nodes (6): IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail, Response, AccessToken, AccessTokenExpiresAt, RefreshToken, RefreshTokenExpiresAt

### Community 243 - "Request"
Cohesion: 0.50
Nodes (3): Request, RefreshToken, RequestValidator

### Community 244 - "RequestBody"
Cohesion: 0.50
Nodes (4): RequestBody, ProductId, Quantity, ReservationDeadline

### Community 245 - "DevicesOptions"
Cohesion: 0.33
Nodes (5): DevicesOptions, AllowedClientIds, ReconcileBatchSize, ReconcileCron, DevicesOptionsValidator

### Community 246 - "Request"
Cohesion: 0.29
Nodes (6): Request, Address, Description, Name, OwnerId, RequestValidator

### Community 247 - "How it works"
Cohesion: 0.33
Nodes (6): Cross-module term search, How it works, One-time setup _(Build, in a migration)_, Query path, Ranking, Write path

### Community 250 - "Request"
Cohesion: 0.50
Nodes (3): Request, Id, RequestValidator

### Community 251 - "Response"
Cohesion: 0.50
Nodes (3): Products.Endpoints.Stores.v1.My.Create, Response, Id

### Community 252 - "MovementTypeSnapshot"
Cohesion: 0.50
Nodes (4): MovementTypeSnapshot, Adjusted, Received, ReservationCommitted

### Community 260 - "ProviderSnapshot"
Cohesion: 0.50
Nodes (4): ProviderSnapshot, Dummy, FormApi, JsonApi

### Community 261 - "Request"
Cohesion: 0.50
Nodes (3): Request, Id, RequestValidator

### Community 262 - "Request"
Cohesion: 0.50
Nodes (3): Request, Id, RequestValidator

### Community 263 - "Request"
Cohesion: 0.50
Nodes (3): Request, PushToken, RequestValidator

### Community 264 - "Request"
Cohesion: 0.50
Nodes (3): Request, Id, RequestValidator

### Community 266 - "Request"
Cohesion: 0.50
Nodes (3): Request, Id, RequestValidator

### Community 269 - "RouteGroupBuilder"
Cohesion: 0.04
Nodes (31): RouteHandlerBuilderExtensions, ResultToResponseTransformer, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint, Endpoint (+23 more)

### Community 270 - "Request"
Cohesion: 0.50
Nodes (3): Request, Id, RequestValidator

### Community 271 - "Request"
Cohesion: 0.50
Nodes (3): Request, Id, RequestValidator

### Community 272 - "v1/Request.cs"
Cohesion: 0.50
Nodes (3): Request, Count, RequestValidator

### Community 273 - "Request"
Cohesion: 0.50
Nodes (3): Request, Id, RequestValidator

### Community 274 - "ReverseProxyOptions"
Cohesion: 0.29
Nodes (5): ReverseProxyOptions, ForwardLimit, IsEnabled, TrustedNetworks, ReverseProxyOptionsValidator

### Community 275 - "Request"
Cohesion: 0.50
Nodes (3): Request, Id, RequestValidator

### Community 276 - "Request"
Cohesion: 0.50
Nodes (3): Request, Id, RequestValidator

### Community 277 - "Request"
Cohesion: 0.50
Nodes (3): Request, Id, RequestValidator

### Community 284 - ".ReadRows"
Cohesion: 0.14
Nodes (3): StringExtensions, CsvWriter, FormulaGuard

### Community 323 - "fluentvalidation"
Cohesion: 0.06
Nodes (39): Products.Endpoints.Stores.v1.AuditLog, Products.Endpoints.Stores.v1.My.Update, Products.Endpoints.Products.v1.GetImport, Products.Endpoints.Products.v1.My.SetAttributes, Inventory.Endpoints.StockReservations.v1.Commit, Products.Endpoints.Stores.v1.Suspend, Inventory.Endpoints.StockLevels.v1.GetBulkAdjustment, Inventory.Endpoints.StockLevels.v1.Receive (+31 more)

## Knowledge Gaps
- **1274 isolated node(s):** `UserId`, `Id`, `IdAsString`, `Roles`, `SessionId` (+1269 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 2011 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **123 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Common.Application.Options` connect `microsoft_extensions_logging` to `PushOptions`, `Common.InterModuleRequests.Products`, `Common.Domain.ResultMonad`, `EmailOptions`, `OutboxOptions.cs`, `ReverseProxyOptions`, `Common.Infrastructure.Extensions`, `CachingOptions.cs`, `CustomValidator`, `microsoft_entityframeworkcore`, `system_diagnostics`, `CorsOptions`, `HealthCheckOptions`, `ValidationContextExtensions.IsProduction`, `JobHousekeepingOptions`, `Setup.Logger.cs`, `fluentvalidation`, `Inventory.Domain.WarehouseConnections`, `StoreAccessReconciliationJob`, `DatabaseOptions.cs`, `ModulesOptions.cs`, `SecurityHeadersOptions.cs`, `ResiliencyOptions`, `Common.Domain.StronglyTypedIds`, `Common.InterModuleRequests.Contracts`, `ResxLocalizationOptions`, `DevicesOptions`, `Response`?**
  _High betweenness centrality (0.061) - this node is a cross-community bridge._
- **What connects `UserId`, `Id`, `IdAsString` to the rest of the system?**
  _1274 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `StockLevel` be split into smaller, more focused modules?**
  _Cohesion score 0.041530054644808745 - nodes in this community are weakly interconnected._
- **Why does `Result` connect `Result` to `StockLevel`, `DateTimeOffset`, `ProductImportJobRunner`, `List`, `TimeProvider`, `Common.Domain.Events`, `Result`, `ProductId`, `IInterModuleRequestClient`, `NetGsmSmsGateway`, `Endpoint.PageMovementsAsync`, `Error`, `IDbContext`, `SmsOtpDispatchOutcome`, `StockBulkAdjustJobRunner.SaveBatchAsync`, `IProductsDbContext`, `Response`, `IPublicObjectStore`, `StoreId`, `ProductImportJob`, `IInventoryDbContext`, `ProductTemplateId`, `EmailOtpDispatchOutcome`, `ReCaptchaService`, `ProvisionUserRequestHandler`, `Stream`, `.SearchAsync`, `Request`, `StockReservationId`, `Endpoint.GetProductAsync`, `NetGsmSmsGateway.SendAsync`, `StockBulkAdjustJob`, `KeycloakTokenClient`, `VerifyPhoneOtpResponse`, `PackageDimensions`, `WarehouseHttpGateway`, `ConstraintViolationExtensions`?**
  _High betweenness centrality (0.056) - this node is a cross-community bridge._
- **Should `DateTimeOffset` be split into smaller, more focused modules?**
  _Cohesion score 0.03618756371049949 - nodes in this community are weakly interconnected._
- **Why does `CustomValidator` connect `CustomValidator` to `Request`, `Request`, `PushOptions`, `Request`, `Request`, `Request`, `Request`, `Request`, `Request`, `EmailOptions`, `OutboxOptions.cs`, `ReverseProxyOptions`, `v1/Request.cs`, `Request`, `ResolveReadOnly/Request.cs`, `Request`, `Request`, `Request`, `CachingOptions.cs`, `Stores/v1/My/Update/Request.cs`, `CorsOptions`, `SendForEmail/Request.cs`, `HealthCheckOptions`, `ValidationContextExtensions.IsProduction`, `Request`, `JobHousekeepingOptions`, `v1/RemoveProduct/Request.cs`, `fluentvalidation`, `SetAttributes/Request.cs`, `Stores/v1/Update/Request.cs`, `Adjust/Request.cs`, `Request`, `PaginationRequest`, `Request`, `StoreAccessReconciliationJob`, `IResxLocalizer`, `Request`, `DatabaseOptions.cs`, `ModulesOptions.cs`, `SecurityHeadersOptions.cs`, `ReplaceCredentials/Request.cs`, `ResiliencyOptions`, `ProductTemplates/v1/Create/Request.cs`, `ProductTemplates/v1/Deactivate/Request.cs`, `Approve/Request.cs`, `Activate/Request.cs`, `ResxLocalizationOptions`, `RequestBody`, `Request`, `DevicesOptions`, `Request`, `Request`, `Response`, `ChangeLowStockThreshold/Request.cs`?**
  _High betweenness centrality (0.031) - this node is a cross-community bridge._
- **Should `microsoft_extensions_logging` be split into smaller, more focused modules?**
  _Cohesion score 0.0344699973565953 - nodes in this community are weakly interconnected._