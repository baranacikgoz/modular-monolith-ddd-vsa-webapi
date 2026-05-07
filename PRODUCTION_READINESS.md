# Production Readiness Checklist

Four hardening items remain before this template is ready for real traffic.
Each item below is scoped to this repository only and is written as a complete
brief for an AI agent — no prior context needed.

Items already confirmed complete: Swagger env-gating, outbound HTTP resilience
(Polly 5-layer pipeline in `Common.Infrastructure.Resiliency.Setup`), CAPTCHA
resilient client, `ClockSkew = TimeSpan.Zero`.

---

## 1. JWT Access-Token Revocation via Redis Blacklist

**Why it matters.** The Revoke endpoint (`POST /tokens/revoke`) clears the
refresh token on the `ApplicationUser` entity but does nothing to the already-
issued access token. That token stays valid until its natural expiry. If an
account is compromised or a user logs out, the access token remains a live
credential for the full expiry window.

**What already exists.**
- Every access token carries a `Jti` claim (a `Guid`, added in
  `TokenService.cs:23`).
- `ICacheService` / `CacheService` (HybridCache → Redis L2) is DI-registered
  and available everywhere.
- `JwtOptions.AccessTokenExpirationInMinutes` is configurable but has no
  upper-bound guard.
- The JWT pipeline lives in
  `src/Modules/IAM/IAM.Infrastructure/Auth/Jwt/Setup.cs`. The
  `JwtBearerEvents` block already handles `OnChallenge`, `OnAuthenticationFailed`,
  and `OnForbidden` — add `OnTokenValidated` here.

**Step-by-step implementation.**

1. **Enforce a short expiry ceiling.**
   In `src/Common/Common.Application/Options/JwtOptions.cs`, add to
   `JwtOptionsValidator`:
   ```csharp
   RuleFor(o => o.AccessTokenExpirationInMinutes)
       .LessThanOrEqualTo(15)
       .WithMessage("AccessTokenExpirationInMinutes must not exceed 15 minutes.");
   ```

2. **Blacklist the Jti on revoke.**
   In `src/Modules/IAM/IAM.Endpoints/Tokens/VersionNeutral/Revoke/Endpoint.cs`,
   inject `ICacheService` and after `user.RevokeRefreshToken()` succeeds, store
   the Jti in the cache with a TTL equal to the remaining access-token lifetime:
   ```csharp
   // Extract Jti from the incoming bearer token via ICurrentUser or IHttpContextAccessor
   // Key pattern: "blacklisted_jti:{jti}"
   // TTL: jwtOptions.AccessTokenExpirationInMinutes (remaining seconds is ideal, use full
   //      duration as a safe upper bound)
   await cacheService.SetAsync(
       $"blacklisted_jti:{jti}",
       true,
       absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(jwtOptions.AccessTokenExpirationInMinutes),
       cancellationToken: cancellationToken);
   ```
   To get the Jti of the *current request's* access token, read it from
   `HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Jti)` — inject
   `IHttpContextAccessor` or extend `ICurrentUser`.

3. **Validate Jti on every authenticated request.**
   In `src/Modules/IAM/IAM.Infrastructure/Auth/Jwt/Setup.cs`, add to the
   `JwtBearerEvents` block:
   ```csharp
   OnTokenValidated = async context =>
   {
       var jti = context.Principal?.FindFirstValue(JwtRegisteredClaimNames.Jti);
       if (jti is null)
       {
           context.Fail("Missing jti claim.");
           return;
       }
       var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
       var isBlacklisted = await cacheService.GetAsync<bool?>($"blacklisted_jti:{jti}",
           context.HttpContext.RequestAborted);
       if (isBlacklisted == true)
       {
           context.Fail("Token has been revoked.");
       }
   }
   ```

4. **Tests.**
   - Add a test in `IAM.Tests/Endpoints/Tokens/RevokeTests.cs`:
     `RevokeToken_ThenUseAccessToken_ReturnsUnauthorized` — call revoke, then
     call any authenticated endpoint with the old access token, assert 401.

**Files touched.**
- `src/Common/Common.Application/Options/JwtOptions.cs`
- `src/Modules/IAM/IAM.Infrastructure/Auth/Jwt/Setup.cs`
- `src/Modules/IAM/IAM.Endpoints/Tokens/VersionNeutral/Revoke/Endpoint.cs`
- `src/Modules/IAM/IAM.Tests/Endpoints/Tokens/RevokeTests.cs`

---

## 2. Consumer Idempotency (Deduplication)

**Why it matters.** Kafka delivers messages *at-least-once*. A broker retry or
rebalance will re-deliver a message. `EventHandlerBase<TEvent>` passes straight
through with no guard — double-processing will happen in production.

**What already exists.**
- MassTransit's `ConsumeContext<TEvent>` exposes `context.MessageId` — a stable
  `Guid` assigned by the publisher.
- `ICacheService` (Redis-backed) is available everywhere.
- All consumer base logic is in
  `src/Common/Common.Application/EventBus/EventHandler.cs`.

**Step-by-step implementation.**

1. **Add deduplication to `EventHandlerBase`.**
   Inject `ICacheService` into the base class constructor. In `Consume()`, before
   calling `HandleAsync`, check Redis:
   ```csharp
   public abstract class EventHandlerBase<TEvent> : IEventHandler<TEvent>
       where TEvent : class, IEvent
   {
       private readonly ICacheService _cache;

       protected EventHandlerBase(ICacheService cache)
       {
           _cache = cache;
       }

       public async Task Consume(ConsumeContext<TEvent> context)
       {
           var messageId = context.MessageId?.ToString();
           if (messageId is not null)
           {
               var key = $"processed_msg:{messageId}";
               var alreadyProcessed = await _cache.GetAsync<bool?>(key, context.CancellationToken);
               if (alreadyProcessed == true) return;

               await HandleAsync(context, context.Message, context.CancellationToken);

               await _cache.SetAsync(key, true,
                   absoluteExpirationRelativeToNow: TimeSpan.FromDays(1),
                   cancellationToken: context.CancellationToken);
           }
           else
           {
               await HandleAsync(context, context.Message, context.CancellationToken);
           }
       }

       protected abstract Task HandleAsync(ConsumeContext<TEvent> context, TEvent @event,
           CancellationToken cancellationToken);
   }
   ```
   The 24h TTL is a safe window: Kafka's consumer group offsets won't re-deliver
   beyond that under normal operation.

2. **Update all concrete handlers.**
   Every class that inherits `EventHandlerBase<T>` must now pass `ICacheService`
   to the base constructor via primary constructors. Run a project-wide search for
   `: EventHandlerBase<` and update each. Files include (non-exhaustive):
   - `src/Modules/IAM/IAM.Application/Users/DomainEventHandlers/v1/V1UserRegisteredDomainEventHandler.cs`
   - `src/Modules/Products/Products.Application/Stores/DomainEventHandlers/v1/V1StoreCreatedDomainEventHandlers.cs`
   - `src/Modules/Notifications/Notifications.Application/IntegrationEventHandlers/UserRegisteredIntegrationEventHandler.cs`

3. **Tests.**
   Add a test for any one consumer handler:
   `Handle_SameMessageIdTwice_ProcessesOnlyOnce` — publish the same
   `ConsumeContext` mock with the same `MessageId` twice, assert the domain
   side-effect occurs exactly once.

**Files touched.**
- `src/Common/Common.Application/EventBus/EventHandler.cs`
- Every file matching `*DomainEventHandler*.cs` and `*IntegrationEventHandler*.cs`

---

## 3. Health Checks: Add Redis and Kafka to Readiness Probe

**Why it matters.** The readiness probe at `/health/ready` currently only checks
Postgres. If Redis (caching, session) or Kafka (event bus) is unavailable, the
pod will continue receiving traffic while core functionality is broken. Kubernetes
would keep routing to it because the probe reports healthy.

**What already exists.**
- Health check infrastructure is in
  `src/Host/Host/Infrastructure/Setup.HealthChecks.cs`.
- Three probe endpoints exist: `/health/live`, `/health/ready`, `/health/startup`.
- `HealthCheckOptions` (with `EnableHealthChecks`, timeout config) is already
  wired.
- NuGet packages to add (to `Directory.Packages.props` and the Host `.csproj`):
  - `AspNetCore.HealthChecks.Redis`
  - `AspNetCore.HealthChecks.Kafka`

**Step-by-step implementation.**

1. **Add NuGet packages.**
   In `Directory.Packages.props`:
   ```xml
   <PackageVersion Include="AspNetCore.HealthChecks.Redis" Version="9.*" />
   <PackageVersion Include="AspNetCore.HealthChecks.Kafka" Version="9.*" />
   ```
   In `src/Host/Host/Host.csproj`:
   ```xml
   <PackageReference Include="AspNetCore.HealthChecks.Redis" />
   <PackageReference Include="AspNetCore.HealthChecks.Kafka" />
   ```

2. **Register the Redis check** in `AddCustomHealthChecks()` after the Postgres
   check. Pull the Redis connection string from `CachingOptions`:
   ```csharp
   builder.AddRedis(
       sp => sp.GetRequiredService<IOptions<CachingOptions>>().Value.Redis.ConnectionString,
       name: "redis",
       tags: [ReadyTag],
       timeout: TimeSpan.FromSeconds(options.ReadinessTimeoutInSeconds));
   ```

3. **Register the Kafka check** after Redis. Pull the bootstrap servers from
   `EventBusOptions` (or `KafkaConsumer` options):
   ```csharp
   builder.AddKafka(
       kafkaConfig =>
       {
           kafkaConfig.BootstrapServers = sp
               .GetRequiredService<IOptions<EventBusOptions>>()
               .Value.MessageBroker.BootstrapServers;
       },
       name: "kafka",
       tags: [ReadyTag],
       timeout: TimeSpan.FromSeconds(options.ReadinessTimeoutInSeconds));
   ```

4. **Verify option types.** Before writing, read `CachingOptions.cs` and
   `EventBusOptions.cs` to confirm exact property names for the connection
   strings, then adjust the lambdas above accordingly.

5. **Tests.**
   Existing integration tests already spin up Postgres, Redis, and Kafka via
   Testcontainers. Add a test that hits `/health/ready` and asserts `200 OK` with
   all three checks passing.

**Files touched.**
- `Directory.Packages.props`
- `src/Host/Host/Host.csproj`
- `src/Host/Host/Infrastructure/Setup.HealthChecks.cs`

---

## 4. Architecture Boundary Enforcement via NetArchTest

**Why it matters.** The module-boundary rules in `CLAUDE.md` are enforced today
only by convention and code review. A single accidental `.csproj` reference
between modules would silently pass CI. NetArchTest turns these rules into
failing tests.

**What already exists.**
- Test infrastructure is in `src/Common/Common.Tests` (xUnit, NSubstitute,
  Bogus, Testcontainers).
- Each module has an `IAssemblyReference` marker interface used for scanning.
- Rules to enforce:
  - No module assembly may reference another module assembly.
  - All integration events must reside in `Common.IntegrationEvents`.
  - All inter-module requests must reside in `Common.InterModuleRequests`.
  - Domain layer must not reference Infrastructure layer within the same module.

**Step-by-step implementation.**

1. **Add NuGet package.**
   In `Directory.Packages.props`:
   ```xml
   <PackageVersion Include="NetArchTest.Rules" Version="1.*" />
   ```
   Add to `src/Common/Common.Tests/Common.Tests.csproj` (or create a new
   `src/Host/ArchitectureTests/ArchitectureTests.csproj` if you want it isolated):
   ```xml
   <PackageReference Include="NetArchTest.Rules" />
   ```

2. **Write the boundary tests.** Create
   `src/Common/Common.Tests/Architecture/ModuleBoundaryTests.cs`:
   ```csharp
   public class ModuleBoundaryTests
   {
       private static readonly string[] ModuleNamespaces =
       [
           "IAM", "Products", "Notifications", "BackgroundJobs", "Outbox"
       ];

       [Fact]
       public void ModuleDomain_MustNotDependOn_OtherModules()
       {
           foreach (var module in ModuleNamespaces)
           {
               var forbidden = ModuleNamespaces.Except([module]).ToArray();
               var result = Types
                   .InAssembly(Assembly.Load($"{module}.Domain"))
                   .ShouldNot()
                   .HaveDependencyOnAny(forbidden.Select(m => $"{m}.").ToArray())
                   .GetResult();

               Assert.True(result.IsSuccessful,
                   $"{module}.Domain depends on: {string.Join(", ", result.FailingTypes?.Select(t => t.FullName) ?? [])}");
           }
       }

       [Fact]
       public void IntegrationEvents_MustLiveIn_CommonIntegrationEvents()
       {
           var result = Types
               .InAssemblies(ModuleNamespaces.Select(m => Assembly.Load($"{m}.Application")))
               .That().ImplementInterface(typeof(IIntegrationEvent))
               .ShouldNot().ResideInNamespaceMatching(".*\\.Application\\..*")
               .GetResult();

           Assert.True(result.IsSuccessful,
               $"Integration events found outside Common.IntegrationEvents: " +
               $"{string.Join(", ", result.FailingTypes?.Select(t => t.FullName) ?? [])}");
       }
   }
   ```
   Adjust assembly names and interface types to match the actual names in this
   repo (check `IAssemblyReference` marker files and `IIntegrationEvent` in
   `Common.Domain`).

3. **Wire into CI.**
   The `make test-common` target already runs `Common.Tests`. Architecture tests
   live there, so no Makefile change is needed. Confirm with `make test-common`
   locally before committing.

**Files touched.**
- `Directory.Packages.props`
- `src/Common/Common.Tests/Common.Tests.csproj`
- `src/Common/Common.Tests/Architecture/ModuleBoundaryTests.cs` (new file)

---

## Completion Order (recommended)

| # | Item | Effort | Risk if skipped |
|---|------|--------|-----------------|
| 1 | JWT access token revocation | Medium | Security — live credentials after logout |
| 2 | Consumer idempotency | Medium | Data correctness — double-processing on retry |
| 3 | Health checks: Redis + Kafka | Small | Ops — unhealthy pod keeps receiving traffic |
| 4 | Architecture boundary tests | Small | Maintainability — boundary violations pass CI |
