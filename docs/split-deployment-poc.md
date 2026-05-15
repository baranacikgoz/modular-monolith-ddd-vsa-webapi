# Split-Deployment PoC

## What this proves

The module system supports running each module as its own process (i.e., like microservices), with **zero HTTP between them**. Inter-module communication still works transparently via MassTransit over RabbitMQ — the caller does not know or care whether the handler is in-process or remote.

Same image. Same codebase. Different `EnabledModules` config per instance.

## How it works

`docker-compose.split.yml` runs two instances of the Host:

| Instance | Port | Modules loaded | Role in PoC |
|---|---|---|---|
| `mm.iam-instance` | 5001 | IAM + Outbox* + BackgroundJobs* | Owns `GetSeedUserIdsRequestHandler`, listens on RabbitMQ |
| `mm.products-instance` | 5002 | Products + Outbox* + BackgroundJobs* | Has no IAM code; calls IAM cross-process via `IInterModuleRequestClient` |

*Outbox and BackgroundJobs are `ICoreModule` — they auto-load on every instance regardless of `EnabledModules` config.

### Cross-process call path

```
curl :5002/v1/probe/cross-module?count=3
  → Products.Endpoints.Probe.v1.Endpoint.HandleAsync
  → IInterModuleRequestClient<GetSeedUserIdsRequest, GetSeedUserIdsResponse>.SendAsync
  → MassTransitInterModuleRequestClient → RabbitMQ exchange (GetSeedUserIdsRequest)
  → IAM instance picks up from queue → GetSeedUserIdsRequestHandler.HandleAsync
  → RabbitMQ reply queue → Products instance receives response
  → Results.Ok(response)
```

No HTTP call. No shared in-process memory. Pure message-passing.

## How to run

`docker-compose.split.yml` defines only the two app instances. It joins the existing
`local_shared_network`, so infra must be running first from the base compose file.

```bash
# Step 1 — start infra (skip if already running)
docker network create local_shared_network
docker compose up -d mm.postgres mm.rabbitmq mm.aspire-dashboard

# Step 2 — build and start both module instances
docker compose -f docker-compose.split.yml up --build

# Step 3 — trigger the cross-process round-trip
curl "http://localhost:5002/v1/probe/cross-module?count=3"

# Expected response (IAM user IDs returned by the IAM instance to the Products instance):
# { "userIds": ["...", "...", "..."] }
```

Open `http://localhost:18888` (Aspire Dashboard) to see the distributed trace spanning both processes under a single `TraceId`.

## Concurrent safety

Two instances running the same core infrastructure is safe:

- **Outbox**: `OutboxProcessor` uses `SELECT ... FOR UPDATE SKIP LOCKED` — instances claim disjoint row batches, no duplicate publishing.
- **Hangfire**: Postgres-backed distributed queue — only one server executes each job. Two Hangfire servers = active/active failover, not duplicate execution. Set `BackgroundJobsOptions__IsServer=false` on any instance you want to exclude from job execution.

## Files added by this PoC

| File | Purpose |
|---|---|
| `docker-compose.split.yml` | Two-instance compose configuration |
| `src/Modules/Products/Products.Endpoints/Probe/v1/Endpoint.cs` | `GET /v1/probe/cross-module` trigger endpoint |
| `src/Modules/Products/Products.Endpoints/Probe/Setup.cs` | Route group registration for probe |