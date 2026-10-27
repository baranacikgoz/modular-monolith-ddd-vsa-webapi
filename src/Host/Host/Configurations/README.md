# Configurations: deploy-time materialized config

These `*.json` files are **placeholders / local-dev defaults**. They are intentionally
**excluded from the published Docker image** (`CopyToPublishDirectory=Never` in `Host.csproj`).
The real, environment-specific values are materialized at **deploy time** by devops, using the
**same file names** under `/app/Configurations` (the app's `ContentRoot`).

## Why this pattern

- This repo has **no HashiCorp Vault-like secret integration**. Rather than maintaining
  per-environment `*.{Environment}.json` overrides (complex and confusing for a deploy-time
  materialization flow), each environment injects **one** fully-materialized set of files.
- Secrets never live in the image. The same image artifact promotes dev → staging → prod
  unchanged; only the mounted config differs.
- **Fail-loud:** `Host/Configurations/Setup.cs` loads every file with `optional: false`.
  A deploy that forgets to mount the config volume **crashes at startup** instead of silently
  booting with placeholder values.
- **Validated:** every file binds to an `*Options` class with a validator, and `AddCommonOptions`
  validates all of them at startup. A bad or missing value stops the host with a message that
  names the option. In `Production` the validators also reject placeholder providers (see below).

## How config is loaded

`Setup.cs` → `AddConfigurations()`:

```
configuration.AddJsonFile($"{filePath}.json", optional: false, reloadOnChange: true);
// per-environment overrides are intentionally disabled: see Setup.cs
configuration.AddEnvironmentVariables();   // env vars win over JSON
```

Precedence (low → high): JSON files → environment variables. So any value can be overridden
at runtime with `Section__Key=...` env vars (double underscore = nested key), for example
`RabbitMqOptions__Host=mq.internal` or `ModulesOptions__EnabledModules__0=Products`.

## Local development

Local dev usually runs through docker-compose. The image has no config files, so
`docker-compose.yml` mounts these committed placeholders read-only:

```yaml
volumes:
  - ./src/Host/Host/Configurations:/app/Configurations:ro
```

Values that differ inside the compose network (database host, RabbitMQ host, Keycloak URL,
OTLP endpoint) are overridden by the `environment:` block in `docker-compose.yml`, which wins over
the mounted JSON. `dotnet run --project src/Host/Host` also works, because the files are copied to
the build output; the committed values point at `localhost` and the ports compose publishes.

## The files

Each file's top-level key is the options class it binds to (the class lives in
`src/Common/Common.Application/Options`). `featureFlags.json` binds to the
`FeatureManagement` section instead.

| File | Options class | Controls |
| :-- | :-- | :-- |
| `modules.json` | `ModulesOptions` | Which modules a process loads (`"*"` for all) |
| `database.json` | `DatabaseOptions` | PostgreSQL connection string |
| `eventBus.json` | `RabbitMqOptions` | Broker address, credentials, prefetch, bus-level retry |
| `outbox.json` | `OutboxOptions` | Processor on/off, batch size, polling, lease, backoff, publish concurrency, cleanup |
| `interModuleRequest.json` | `InterModuleRequestOptions` | Request timeouts (default and per request), handler concurrency |
| `backgroundJobs.json` | `BackgroundJobsOptions` | Hangfire server on/off, workers, polling, dashboard path |
| `jobHousekeeping.json` | `JobHousekeepingOptions` | Stale job and retention windows |
| `projectionReconciliation.json` | `ProjectionReconciliationOptions` | Reconciliation page size and page ceiling |
| `auditLog.json` | `AuditLogOptions` | Audit retention, purge batch and schedule, per-schema overrides |
| `caching.json` | `CachingOptions` | Memory bound, Redis on/off, entry defaults, idempotency key durations |
| `keycloak.json` | `KeycloakOptions` | Keycloak address, realm, client ids and secrets, decision cache, timeouts |
| `identityScheme.json` | `IdentitySchemeOptions` | `PhoneNumber` or `Email` self-service scheme |
| `devices.json` | `DevicesOptions` | Allowed client ids, device reconciliation schedule |
| `otp.json` | `OtpOptions` | One-time code length, expiry, resend interval, quotas, development `DummyCode` |
| `sms.json` | `SmsOptions` | SMS provider, credentials, timeouts, caps, templates |
| `email.json` | `EmailOptions` | Email provider, API key, sender, timeouts, caps, templates |
| `push.json` | `PushOptions` | Push provider, Firebase service account, templates |
| `signalR.json` | `SignalROptions` | Redis backplane |
| `captcha.json` | `CaptchaOptions` | Captcha provider, keys, score threshold |
| `inventory.json` | `InventoryOptions` | Warehouse gateway, webhook secret and body limit, sweep schedule, series limits |
| `objectStorage.json` | `ObjectStorageOptions` | S3-compatible endpoint, buckets, credentials, presign lifetimes, retries, circuit breaker |
| `rateLimiting.json` | `CustomRateLimitingOptions` | Named rate limit policies, global limit, exempt paths |
| `products.json` | `ProductsOptions` | Import and export job limits, housekeeping schedule, image limits, cross-module page cap and search cache |
| `resiliency.json` | `ResiliencyOptions` | Outbound HTTP timeouts, retries, circuit breaker, keyed profiles |
| `securityHeaders.json` | `SecurityHeadersOptions` | Response security headers |
| `reverseProxy.json` | `ReverseProxyOptions` | Forwarded headers and trusted proxy networks |
| `cors.json` | `CorsOptions` | Allowed origins, methods and headers |
| `healthCheck.json` | `HealthCheckOptions` | Health checks on/off and probe timeouts |
| `requestLogging.json` | `RequestLoggingOptions` | Request and response body logging, exclusions, redaction |
| `observability.json` | `ObservabilityOptions` | Log levels and sinks, tracing and metrics, OTLP endpoint |
| `openApi.json` | `OpenApiOptions` | Swagger switch and API metadata |
| `localization.json` | `ResxLocalizationOptions` | Default and supported cultures |
| `featureFlags.json` | `FeatureManagement` | Feature flags, targeting rules and variants |
| `fullTextSearch.json` | `FullTextSearchOptions` | Search language mapping and ranking weights |

A new options class gets its own file here and an entry in the `AddJsonFile(...)` list in
`Setup.cs`.

## Deploying to a real environment (devops responsibility)

Mount a materialized copy of every `*.json` over `/app/Configurations` via a sidecar /
init-container / secret volume. Checklist:

1. Provide **every** file name present in this directory (missing file → startup crash).
2. Replace all placeholder secrets with real values:
   - `database.json` → `DatabaseOptions.ConnectionString`
   - `eventBus.json` → `RabbitMqOptions.Username`, `RabbitMqOptions.Password` and the host
   - `caching.json` → `CachingOptions.Redis.*`
   - `signalR.json` → `SignalROptions.RedisConnectionString`
   - `keycloak.json` → `KeycloakOptions.BaseUrl`, `ResourceClientSecret`, `TrustedLoginClientSecret`,
     and `RequireHttpsMetadata: true`
   - `sms.json` → `SmsOptions.Provider` plus `UserCode`, `Password`, `MsgHeader`
   - `email.json` → `EmailOptions.Provider` plus `ApiKey`, `SenderEmail`
   - `push.json` → `PushOptions.Provider` plus the `ServiceAccount` block
   - `captcha.json` → `CaptchaOptions.Provider` plus `ClientKey`, `SecretKey`
   - `inventory.json` → `InventoryOptions.WarehouseGatewayProvider`, `WarehouseGatewayBaseUrl`,
     `WebhookSharedSecret`
   - `objectStorage.json` → endpoint, `AccessKey`, `SecretKey`, bucket names, public base URL
   - `otp.json` → remove `DummyCode`
3. Set the structural values for your network: `reverseProxy.json` `TrustedNetworks`,
   `cors.json` `AllowedOrigins`, `modules.json` `EnabledModules`.
4. Alternatively, override individual secrets via `Section__Key` environment variables
   (they win over the mounted JSON) and mount only the non-secret structural files.

In `Production` the host refuses to start with `Dummy` SMS, email, push, captcha or warehouse
providers, a fixed OTP `DummyCode`, an in-memory-only cache (unless `AllowInMemoryOnlyInProduction`
is set) or SignalR without a Redis backplane (unless `RequireRedisBackplaneInProduction` is off).

## Adding a field to an options class that is already deployed

Keep the field `required`. Then tell whoever owns each deployed environment which class got which
field and its value, because a configuration that predates the field fails validation at startup.
