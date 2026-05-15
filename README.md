# Modular Monolith, DDD, Vertical Slice Architecture WebAPI Boilerplate

This repository provides a .NET 10 WebAPI boilerplate implementing a Modular Monolith approach, Domain-Driven Design (
DDD), and Vertical Slices architecture, along with Clean Architecture principles per feature. It is designed to
facilitate the development of scalable and maintainable applications.

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Requirements](#requirements)
- [Getting Started](#getting-started)
  - [Split Deployment](#split-deployment-microservice-mode)
- [API Documentation](#api-documentation)
- [Contributing](#contributing)
- [License](#license)

## Introduction

This boilerplate serves as a starting point for building applications with a focus on modularity, scalability, and
maintainability. It leverages modern architectural patterns and practices to ensure a robust and flexible codebase.

## Features

This repository includes the following features:

- **Event-Driven Architecture**: Supports event-driven communication between components.
- **Modular Monolithic Architecture**: Organizes code into modules for better separation of concerns.
- **Dynamic Module Registration**: Configuration-driven module loading via `ModulesOptions`, enabling explicit startup priority ordering and true split-deployment isolation — run any subset of modules per process without recompilation (see [Split Deployment](#split-deployment-microservice-mode)).
- **Vertical Slices, REPR, & Minimal APIs**: Implements vertical slice architecture for feature-based organization.
- **Domain-Driven Design & Clean Architecture**: Adheres to DDD principles and clean architecture for maintainable code.
- **Identity and Access Management (IAM)**: Provides built-in IAM capabilities.
- **Audit Log & Retention**: Provides a transactional audit log (via domain events) with configurable automatic retention policies to manage database growth.
- **OpenTelemetry Support**: Integrates with Otel-Collector, Prometheus, Jaeger, and Aspire Dashboard for observability.
- **Result Monad for Error Management**: Utilizes result monads for error handling and flow control.
- **Unit of Work**: Ensures atomic operations across multiple repositories.
- **Hangfire**: Supports background job processing.
- **Transactional Outbox Pattern**: Ensures reliable message delivery via a custom outbox processor with lag tracking, cleanup, and distributed trace propagation.
- **RabbitMQ & MassTransit**: Integration events delivered over RabbitMQ via MassTransit; no application-side producers — publish via outbox only.
- **Redis or In-Memory Caching**: Provides caching mechanisms for performance optimization.
- **JWT Access-Token Revocation**: Redis-backed blacklist invalidates tokens on logout/revoke with a 15-minute ceiling enforced on expiry; Jti validated on every authenticated request.
- **Consumer Idempotency**: `EventHandlerBase` checks a `processed_msg:{messageId}` key in Redis before invoking the handler and writes it with a 24h TTL, ensuring at-least-once delivery without duplicate side effects.
- **Security Headers Middleware**: Configurable `SecurityHeadersMiddleware` injects `X-Content-Type-Options`, `X-Frame-Options`, `Content-Security-Policy`, and related headers on every response.
- **Reverse Proxy / Forwarded Headers Support**: Configurable `ForwardedHeaders` middleware trusts known proxy networks and correctly propagates client IP and scheme behind load balancers.
- **Feature Management**: `Microsoft.FeatureManagement` with targeting context support and automated endpoint filtering — feature flags configurable per-environment via `appsettings.json`.
- **Rate Limiting**: Modular per-policy rate limiting (registration, OTP/SMS, token, store creation) with override support via env vars for load testing.
- **k6 Load Testing**: Multi-scenario k6 suite (`docker compose -f docker-compose.yml -f docker-compose.perf.yml up k6`) with Aspire Dashboard OTel traces live during runs.
- **Readiness Health Checks**: `/health/ready` probe conditionally registers Redis and Kafka checks so orchestrators only route traffic once all backing services are reachable.
- **Architecture Boundary Tests**: NetArchTest rules assert no cross-module Domain references and that all `IntegrationEvent` types are declared exclusively in `Common.IntegrationEvents`.
- **Strongly Typed IDs**: Prevents primitive obsession by using strongly typed identifiers.
- **Strongly-Typed Localization & Multi-Language Support**: Leverages `Aigamo.ResXGenerator` to automatically generate strongly-typed `IResxLocalizer` properties for error-free resource resolution, ensuring compile-time safety and eliminating missing key errors at runtime.
- **Pagination and Flexible Search**: Implements pagination and flexible search capabilities.
- **MassTransit's Request Client**: Synchronous inter-module requests via `IInterModuleRequestClient<TRequest, TResponse>` — transparent whether the handler is in-process or running in a separate instance on another host. No HTTP, no shared memory; pure RabbitMQ request/response.
- **Option Pattern**: Utilizes the option pattern for configuration management.
- **Fluent Validation**: Provides fluent validation for input data.
- **Functional Programming & Railway-Oriented Syntax**: Encourages functional programming practices and method chaining.
- **Docker-Compose Support**: Simplifies development setup with Docker-Compose.
- **Grafana Templates for Monitoring**: Includes Grafana templates for monitoring and observability.
  and more.

## Requirements

To use or contribute to this project, you will need:

- .NET 10 SDK
- Docker
- GNU Make (`make`)

## Getting Started

- Clone the repository: `git@github.com:baranacikgoz/modular-monolith-ddd-vsa-webapi.git`

### Running services

- Start required infrastructure:
    ```bash
    docker compose -f "docker-compose.yml" up -d --build mm.postgres mm.redis mm.aspire-dashboard mm.rabbitmq
    ```

### Split Deployment (Microservice Mode)

Each module can run as its own process. The same image is used for all instances — only the `ModulesOptions__EnabledModules` environment variable differs. Inter-module communication still works transparently via MassTransit over RabbitMQ; no HTTP, no shared in-process memory.

```bash
# Start two instances: IAM on :5001, Products on :5002
docker compose -f docker-compose.split.yml up --build

# Prove the cross-process round-trip:
# Products instance has zero IAM code loaded — the request travels via RabbitMQ
curl "http://localhost:5002/v1/probe/cross-module?count=3"
```

Open `http://localhost:18888` (Aspire Dashboard) to inspect the distributed trace spanning both processes under a single `TraceId`.

See [`docs/split-deployment-poc.md`](docs/split-deployment-poc.md) for the full call-path walkthrough and concurrent-safety analysis.

### VSCode

#### One-time setup

- Use appropriate ``tasks.json`` depending on your OS
    - Copy ``tasks.windows.json`` or ``tasks.unix.json``, into a new file with the name ``tasks.json``.

##### Unix only

- Make VS Code helper scripts executable:
    ```bash
    chmod +x .vscode/exclude_docker_compose_dcproj.sh
    chmod +x .vscode/dotnet_build_and_cleanup.sh
    chmod +x .vscode/revert_exclude.sh
    ```

#### Development

- Press ``F5`` to start the application in debug mode

### Visual Studio

- Open the solution file ``ModularMonolith.sln`` in Visual Studio
- Set ``Docker Compose`` as the startup project
- Start application

---

## Developer Commands

All developer commands are centralized in the **Makefile**. Run `make <target>` from the repository root.

### Build & Test

| Command | Description |
|---|---|
| `make build` | Build all projects (auto-excludes `docker-compose.dcproj`) |
| `make test` | Run all integration & unit tests |
| `make sonar` | Run SonarQube analysis (requires `.env` with SonarQube vars) |

### Database Migrations

> **⚠️ We do NOT auto-migrate at startup.** All schema changes go through DBA review.

#### Add a new migration

| Command | Description |
|---|---|
| `make ef-add-IAM name=<Name>` | Add migration to the IAM module |
| `make ef-add-Products name=<Name>` | Add migration to the Products module |
| `make ef-add-Outbox name=<Name>` | Add migration to the Outbox module |

#### Generate idempotent SQL scripts (for DBA review)

| Command | Description |
|---|---|
| `make ef-script-IAM` | Generate SQL script for IAM → `migrations/IAM/` |
| `make ef-script-Products` | Generate SQL script for Products → `migrations/Products/` |
| `make ef-script-Outbox` | Generate SQL script for Outbox → `migrations/Outbox/` |
| `make ef-script-all` | Generate scripts for all modules |

### Migration Workflow Summary

```
1. make ef-add-Products name=AddPriceColumn   # Create the migration
2. make ef-script-Products                      # Generate idempotent SQL
3. git add . && git commit                      # Commit both .cs + .sql artifacts
4. DBA reviews & executes the .sql script       # Applied to target DB
5. Deploy                                        # App verifies — fails fast if pending
```

---

## API Documentation

Links to API documentation (if available), or instructions on how to generate it.

## Contributing

Guidelines for contributing to the project, including how to report bugs, suggest features, or submit pull requests.

## License

Information about the project's license.
