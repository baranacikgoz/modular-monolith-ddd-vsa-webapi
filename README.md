# Modular Monolith, DDD, Vertical Slice Architecture WebAPI Boilerplate

This repository provides a .NET 9 WebAPI boilerplate implementing a Modular Monolith approach, Domain-Driven Design (
DDD), and Vertical Slices architecture, along with Clean Architecture principles per feature. It is designed to
facilitate the development of scalable and maintainable applications.

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Requirements](#requirements)
- [Getting Started](#getting-started)
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
- **Dynamic Module Registration**: Configuration-driven module loading via `modules.json` and `ModulesOptions`, enabling explicit startup priority ordering and true microservice-like deployment isolation without recompilation.
- **Vertical Slices, REPR, & Minimal APIs**: Implements vertical slice architecture for feature-based organization.
- **Domain-Driven Design & Clean Architecture**: Adheres to DDD principles and clean architecture for maintainable code.
- **Identity and Access Management (IAM)**: Provides built-in IAM capabilities.
- **Audit Log & Retention**: Provides a transactional audit log (via domain events) with configurable automatic retention policies to manage database growth.
- **OpenTelemetry Support**: Integrates with Otel-Collector, Prometheus, Jaeger, and Seq for observability.
- **Result Monad for Error Management**: Utilizes result monads for error handling and flow control.
- **Unit of Work**: Ensures atomic operations across multiple repositories.
- **Hangfire**: Supports background job processing.
- **MassTransit & RabbitMQ**: Facilitates message-based communication.
- **Transactional Outbox Pattern**: Ensures reliable message delivery.
- **CDC - Kafka & Debezium**: Processes outbox messages with Kafka & Debezium.
- **Redis or In-Memory Caching**: Provides caching mechanisms for performance optimization.
- **Strongly Typed IDs**: Prevents primitive obsession by using strongly typed identifiers.
- **Strongly-Typed Localization & Multi-Language Support**: Leverages `Aigamo.ResXGenerator` to automatically generate strongly-typed `IResxLocalizer` properties for error-free resource resolution, ensuring compile-time safety and eliminating missing key errors at runtime.
- **Pagination and Flexible Search**: Implements pagination and flexible search capabilities.
- **MassTransit's Request Client**: Enables inter-module communication and data transfer.
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
    docker compose -f "docker-compose.yml" up -d --build mm.database mm.rabbitmq mm.seq
    ```

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
> See [`docs/migration-workflow.md`](docs/migration-workflow.md) for the full workflow.

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
