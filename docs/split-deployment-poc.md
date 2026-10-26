# Split-Deployment PoC

## What this proves

The module system supports running each module as its own process (i.e., like microservices), with **zero HTTP between them**. Inter-module communication still works transparently via MassTransit over RabbitMQ: the caller does not know or care whether the handler is in-process or remote.

Same image. Same codebase. Different `EnabledModules` config per instance.

## How it works

`docker-compose.split.yml` runs two instances of the Host:

| Instance | Port | Modules loaded | Role in PoC |
|---|---|---|---|
| `mm.iam-instance` | 5001 | IAM + Outbox* + BackgroundJobs* | Owns `GetSeedUserIdsRequestHandler`, listens on RabbitMQ |
| `mm.products-instance` | 5002 | Products + Outbox* + BackgroundJobs* | Does not load the IAM module; calls IAM cross-process via `IInterModuleRequestClient` |

*Outbox and BackgroundJobs are `ICoreModule`: they auto-load on every instance regardless of `EnabledModules` config.

Both instances run the same image, so the IAM assemblies are present in the Products container. What differs is which modules the host activates. A module that is not activated registers no services, no routes and no message consumers.

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
`local_shared_network`, so infra must be running first from the base compose file. Keycloak is part of that infra: the IAM instance brokers every identity operation through it.

```bash
# Step 1: start infra and create the database (skip what is already done)
docker network create local_shared_network
docker compose up -d mm.postgres mm.rabbitmq mm.keycloak mm.aspire-dashboard
make ef-update-all

# Step 2: stop the single-process API if it is running (both use port 5001)
docker compose stop host

# Step 3: build and start both module instances
docker compose -f docker-compose.split.yml up --build

# Step 4: trigger the cross-process round-trip
curl "http://localhost:5002/v1/probe/cross-module?count=3"

# Expected response (IAM user IDs returned by the IAM instance to the Products instance):
# { "userIds": ["...", "...", "..."] }
```

Open `http://localhost:18888` (Aspire Dashboard) to see the distributed trace spanning both processes under a single `TraceId`.

### What to check

- **Routes follow modules.** The IAM instance answers `/otp/login` but returns `404` for `/v1/stores/search`. The Products instance does the opposite, and only it serves `/v1/probe/cross-module`.
- **Start order matters for seeding.** On startup the Products seeder asks IAM for the seed user ids to create sample stores. If the IAM instance has not registered its request consumer yet, the request times out, the seeder logs a warning and skips store seeding, and the instance still starts. The compose file makes the Products instance wait for the IAM instance's liveness check before it starts.

## Concurrent safety

Two instances running the same core infrastructure is safe:

- **Outbox**: `OutboxProcessor` uses `SELECT ... FOR UPDATE SKIP LOCKED`: instances claim disjoint row batches, no duplicate publishing.
- **Hangfire**: Postgres-backed distributed queue: only one server executes each job. Two Hangfire servers = active/active failover, not duplicate execution. Set `BackgroundJobsOptions__IsServer=false` on any instance you want to exclude from job execution.
- **Consumers**: events can be delivered more than once and out of order. Every consumer deduplicates with the transactional inbox, so running the same consumer in several processes is safe.
- **Outbox publishing**: set `OutboxOptions__IsProcessor=false` on an instance that should write outbox rows but leave publishing to others.

## Files added by this PoC

| File | Purpose |
|---|---|
| `docker-compose.split.yml` | Two-instance compose configuration |
| `src/Modules/Products/Products.Endpoints/Probe/v1/Endpoint.cs` | `GET /v1/probe/cross-module` trigger endpoint |
| `src/Modules/Products/Products.Endpoints/Probe/Setup.cs` | Route group registration for probe |