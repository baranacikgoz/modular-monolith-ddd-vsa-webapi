# Getting started

This guide takes you from a fresh clone to a running system, a first authenticated request, a passing test run, a split deployment and a load test. The [README](../README.md) has the short version.

## Prerequisites

| Tool | Why |
| :-- | :-- |
| .NET 10 SDK | Builds the solution. `global.json` pins the SDK and uses the Microsoft.Testing.Platform test runner. |
| Docker | Runs PostgreSQL, RabbitMQ, Redis, Keycloak and the Aspire Dashboard. The tests also start containers. |
| GNU Make | Every developer command is a Makefile target. |
| EF Core CLI | `dotnet tool install --global dotnet-ef`. Used by the migration targets. |

## Run the whole system

```bash
# 1. One shared Docker network, created once
docker network create local_shared_network

# 2. Backing services
docker compose up -d mm.postgres mm.rabbitmq mm.redis mm.keycloak mm.aspire-dashboard

# 3. Create the database and apply every module's migrations
make ef-update-all

# 4. Build and start the API
docker compose up --build host
```

What each step does:

1. The compose file joins every container to an external network so other compose files (the split deployment, the load test) can reach the same infrastructure by host name.
2. PostgreSQL, RabbitMQ and Redis start with development credentials. Keycloak starts in `start-dev` mode and imports `keycloak/realm-modular-monolith.json` the first time. Keycloak keeps its state under `.containers/mm.keycloak`. After editing the realm file, delete that folder to import again.
3. The application never migrates the database itself (see [Database changes](../README.md#database-changes)). `make ef-update-all` is the local-development shortcut: it creates `modular-monolith-db` if missing and applies the Outbox, Notifications, Products and Inventory migrations. IAM has no database. The Hangfire storage schema is created by Hangfire when the API starts.
4. The `host` service builds `src/Host/Host/Dockerfile` and listens on `http://localhost:5001`. If you skip step 3 the container exits at startup with a message naming the module and the pending migrations.

### Run the API from the command line instead

With the infrastructure from step 2 running and the migrations applied, you can run the API without Docker:

```bash
dotnet run --project src/Host/Host --launch-profile http
```

It listens on `http://localhost:6000` and opens Swagger. The committed configuration points at `localhost` and the ports Docker Compose publishes, so no further setup is needed.

### Visual Studio and Rider

Open `ModularMonolith.sln`. In Visual Studio set the `docker-compose` project as the startup project. Rider picks up the committed run configuration in `.run/`. Both start the same `host` service as step 4, using the infrastructure containers.

## Where things are

| What | Where |
| :-- | :-- |
| Swagger UI (Development only) | `http://localhost:5001/swagger` |
| Health probes | `/health/live`, `/health/ready`, `/health/startup`, `/health/dependencies` |
| Hangfire dashboard | `/hangfire` (requires the `hangfire:manage` scope, so sign in as an administrator) |
| Aspire Dashboard | `http://localhost:18888` |
| Keycloak admin console | `http://localhost:8080`, user `admin`, password `admin` |
| RabbitMQ management | `http://localhost:15672`, user `guest`, password `guest` |
| Redis Stack browser | `http://localhost:8001` |

## Your first requests

### Sign in

The seeded system administrator has the phone number `901111111111`. In development the SMS provider is `Dummy` and `OtpOptions.DummyCode` is `123456`, so no message is sent and every code is `123456`.

```bash
curl -s -X POST http://localhost:5001/otp/login \
  -H 'Content-Type: application/json' \
  -d '{"phoneNumber":"901111111111"}'
# 204 No Content

curl -s -X POST http://localhost:5001/tokens \
  -H 'Content-Type: application/json' \
  -d '{"phoneNumber":"901111111111","otp":"123456","deviceId":"00000000-0000-4000-8000-000000000001","clientId":"mobile-app-1"}'
```

The response holds `accessToken`, `accessTokenExpiresAt`, `refreshToken` and `refreshTokenExpiresAt`. Send the access token as `Authorization: Bearer <token>`, or paste it into Swagger's **Authorize** button.

Three things to know when you try this more than once:

- A one-time code is single use. Signing in again needs a fresh `POST /otp/login`.
- `POST /otp/login` is limited to one request per 15 seconds (the `Sms` rate limit) and a resend interval of 60 seconds applies to the same number. A `429` response means wait and retry.
- `clientId` must be one of `DevicesOptions.AllowedClientIds` (`mobile-app-1`, `mobile-app-2`, `web-app-1`). Every login binds a session to a `deviceId` and `clientId`.

The default response language is Turkish. Send `Accept-Language: en` to get English messages.

Staff can sign in with an email and password instead. That flow, the other seeded users and the Keycloak model are described in [`keycloak/README.md`](../keycloak/README.md).

### Watch two modules cooperate

Create a store, add a product to it, then read the product back. Use the access token from above and `Accept-Language: en` if you prefer English.

```bash
TOKEN=<your access token>
AUTH="Authorization: Bearer $TOKEN"
JSON='Content-Type: application/json'

# 1. Create your store (one store per owner, a second attempt returns 409)
curl -s -X POST http://localhost:5001/v1/stores/my -H "$AUTH" -H "$JSON" \
  -d '{"name":"My Store","description":"A store for trying the API.","address":"Istanbul Kadikoy"}'

# 2. Pick an active product template. The seeder creates some, but a template must be active to be used.
curl -s "http://localhost:5001/v1/product-templates/search?PageNumber=1&PageSize=5" -H "$AUTH"

# 3. Add a product to your store
curl -s -X POST http://localhost:5001/v1/stores/my/products -H "$AUTH" -H "$JSON" \
  -d '{"productTemplateId":"<template id>","name":"First product","description":"My first product.","quantity":7,"price":10.5}'

# 4. Read it back
curl -s http://localhost:5001/v1/products/<product id> -H "$AUTH"
```

Step 3 commits the product, an outbox row and an audit row in one transaction. The outbox processor publishes `ProductCreatedIntegrationEvent` over RabbitMQ and the Inventory module creates a stock level from it. Step 4 returns `availableQuantity: 7`, filled in by a synchronous inter-module request from Products to Inventory. If the product is read before Inventory has consumed the event, or Inventory is not running, the field is `0` and the read still succeeds.

Open the Aspire Dashboard at `http://localhost:18888` and look at the traces for the two calls to see the whole path, including the hop through RabbitMQ.

### Queued work: accept, queue, poll

Some calls do their work after they answer. A product export is the shortest to try with the administrator token (it holds `products:export`):

```bash
# Queue the export. The answer is 202 with the job, not the file.
curl -s -X POST http://localhost:5001/v1/products/exports -H "$AUTH"

# Poll until status is Succeeded (Queued, Running, Succeeded, Failed)
curl -s http://localhost:5001/v1/products/exports/<job id> -H "$AUTH"

# Download the CSV
curl -s http://localhost:5001/v1/products/exports/<job id>/download -H "$AUTH" -o products.csv
```

The call writes a job row and a queue message in one transaction, a consumer builds the file in the background, and the download answers 409 `NotReady` until the job has succeeded. The product import (`POST /v1/products/imports`, a spreadsheet upload) and the Inventory bulk adjustment (`POST /v1/stock-levels/bulk-adjustments`) follow the same shape. The outbox processor and RabbitMQ must be running for the job to leave `Queued`, which they are in the compose setup above.

Imports, exports and product images keep their files in an S3-compatible object store. `docker-compose.yml` starts none, and `objectStorage.json` holds `CHANGE_ME` placeholders, so point `ObjectStorageOptions` at a service you run before you try them (any S3-compatible service works, with a public and a private bucket).

### Connect a store to a warehouse system

Releasing a stock reservation, pushing stock levels and the warehouse webhook go through a per-store warehouse connection. The repository has no built-in stand-in warehouse, so create a connection that points at an HTTPS server answering the provider's wire shape. The steps and the wire shapes are in [`modules.md`](modules.md#inventory).

### Errors

Failures return `ProblemDetails` with an extra `errorKey`, a `traceId` and the `environment`:

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.10",
  "title": "Store ViolatesUniqueConstraint",
  "status": 409,
  "instance": "POST /v1/stores/my",
  "errorKey": "ViolatesUniqueConstraint",
  "errors": { "Store": [] },
  "traceId": "00-...",
  "environment": "Development"
}
```

Use `traceId` to find the trace in the Aspire Dashboard.

## Seeded data

On startup, ordered seeders run after the host has fully started. Products creates sample stores, product templates and products, using the seed users that IAM returns over an inter-module request. The Keycloak realm brings the seed users: a system administrator, a staff user and five basic users (see [`keycloak/README.md`](../keycloak/README.md)).

## Run the tests

```bash
make test                # everything, one project after another
make test-products       # one project
```

The targets are `test-common`, `test-host`, `test-iam`, `test-products`, `test-inventory`, `test-outbox`, `test-notifications` and `test-backgroundjobs`. Integration tests start real PostgreSQL and Keycloak containers with Testcontainers and reset data with Respawn, so Docker must be running. Tests that touch Keycloak or the whole host are slower than the module tests.

## Split the deployment

Run two processes from the same image, each with a different module set, and watch a request cross the boundary through RabbitMQ.

```bash
# Infrastructure and migrations as above, and stop the single-process `host` service first (the ports overlap)
docker compose -f docker-compose.split.yml up --build

# The Products instance (port 5002) asks the IAM instance (port 5001) for user ids
curl "http://localhost:5002/v1/probe/cross-module?count=3"
```

The full call path and the concurrency analysis are in [`split-deployment-poc.md`](split-deployment-poc.md).

## Run the load test

```bash
make perf-smoke    # 1 virtual user for 30 seconds
make perf          # 50 virtual users at peak, about 7 minutes
make perf-down     # tear down
```

`make perf` starts the stack with `docker-compose.perf.yml`, which lifts the rate limits for the run and starts k6. The scenarios in `k6/scenarios` are buyer (50 percent), seller (30), new seller (15) and admin (5). Keep the Aspire Dashboard open while it runs to watch traces and metrics live. If an IDE already runs your infrastructure, use the `perf-rider*` targets, which start only the API and k6.

## Configuration

Every option class has a JSON file in `src/Host/Host/Configurations`, loaded at startup and validated. Any value can be overridden with an environment variable using a double underscore for nesting, for example `RabbitMqOptions__Host=localhost` or `OutboxOptions__BatchSize=200`. The deployment model and the list of files that hold secrets are in [`Configurations/README.md`](../src/Host/Host/Configurations/README.md). The Keycloak realm in `keycloak/realm-modular-monolith.json` also defines the `store-owner` role and the scopes of stock levels, warehouse connections, store approval and product import and export; like any realm change it only reaches a Keycloak that imports the file for the first time.

## Troubleshooting

| Symptom | Cause and fix |
| :-- | :-- |
| `network local_shared_network declared as external, but could not be found` | Run `docker network create local_shared_network` once. |
| The API container exits with `has N pending migration(s)` | Run `make ef-update-all`. |
| Login returns `429` | The OTP rate limit is one request per 15 seconds. Wait and retry. |
| Login returns `InvalidOtp` | The code is single use. Request a new one with `POST /otp/login`. |
| Keycloak realm changes do not show up | Keycloak skips the import when the realm exists. Delete `.containers/mm.keycloak` and start it again. |
| Port 5001 is already in use | Stop the other process, or use the `dotnet run` profile on port 6000. |
| An import, an export or an image upload cannot reach its files | These features need an S3-compatible object store. The compose file starts none and `objectStorage.json` holds `CHANGE_ME` placeholders. |
| A job stays `Queued` | Its queue message is published by the outbox processor over RabbitMQ and run by a consumer in a process that loads the module. Check `OutboxOptions.IsProcessor` and that RabbitMQ is up. |
| Startup fails naming `SecretProtectionOptions` and the development key | The checked-in `EncryptionKey` is accepted only when `ASPNETCORE_ENVIRONMENT` is `Development`. Set `SecretProtectionOptions__EncryptionKey` to base64 of 32 random bytes. |
