# Configurations — deploy-time materialized config

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

## How config is loaded

`Setup.cs` → `AddConfigurations()`:

```
configuration.AddJsonFile($"{filePath}.json", optional: false, reloadOnChange: true);
// per-environment overrides are intentionally disabled — see Setup.cs
configuration.AddEnvironmentVariables();   // env vars win over JSON
```

Precedence (low → high): JSON files → environment variables. So any value can be overridden
at runtime with `Section__Key=...` env vars (double underscore = nested key).

## Local development (docker-compose)

Local dev never raw-runs `dotnet`; it uses the IDE's docker-compose run. The image has no
config files, so `docker-compose.yml` mounts these committed placeholders read-only:

```yaml
volumes:
  - ./src/Host/Host/Configurations:/app/Configurations:ro
```

Secret-bearing values (DB connection string, RabbitMQ, caching/Redis) are overridden by the
`environment:` block in `docker-compose.yml`, which wins over the mounted JSON. The remaining
placeholder values (JWT dev secret, dummy captcha keys, OTP templates, rate limits, CORS, …)
are dev-safe as committed.

## Deploying to a real environment (devops responsibility)

Mount a materialized copy of every `*.json` over `/app/Configurations` via a sidecar /
init-container / secret volume. Checklist:

1. Provide **every** file name present in this directory (missing file → startup crash).
2. Replace all placeholder secrets with real values:
   - `jwt.json` → `JwtOptions.Secret`
   - `captcha.json` → `CaptchaOptions.ClientKey`, `CaptchaOptions.SecretKey`
   - `database.json` → `DatabaseOptions.ConnectionString`
   - `eventBus.json` → `RabbitMqOptions.*`
   - `caching.json` → `CachingOptions.Redis.*`
3. Alternatively, override individual secrets via `Section__Key` environment variables
   (they win over the mounted JSON) and mount only the non-secret structural files.
