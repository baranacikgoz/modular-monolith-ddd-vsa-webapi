# Modular Monolith, DDD, Vertical Slice Architecture WebAPI Boilerplate

A .NET 9 Webapi boilerplate with Modular Monolith approach, Domain-Driven Design and Vertical Slices architecture along with Clean Architecture principles per feature.

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Requirements](#requirements)
- [Getting Started](#getting-started)
<!-- - [API Documentation](#api-documentation)
- [Contributing](#contributing)
- [License](#license) -->

## Introduction

This project is a .NET 9 WebAPI boilerplate designed with a Modular Monolith approach, incorporating Domain-Driven Design (DDD) and Vertical Slices architecture. It adheres to Clean Architecture principles, ensuring a scalable and maintainable codebase.

## Features

This boilerplate includes a comprehensive set of features:

- **Event-Driven Architecture**: Facilitates asynchronous communication between components.
- **Modular Monolithic Architecture**: Organizes code into modules for better separation of concerns.
- **Vertical Slices, REPR, & Minimal APIs**: Implements vertical slice architecture for feature-based organization.
- **Domain-Driven Design & Clean Architecture**: Ensures a clear separation between domain logic and application logic.
- **Identity and Access Management (IAM)**: Manages user identities and access control.
- **Event-Sourcing Aggregates**: Currently used for audit/event logging with future plans for full event sourcing.
- **OpenTelemetry Support**: Integrates with Otel-Collector, Prometheus, Jaeger, and Seq for observability.
- **Result Monad for Error Management**: Provides flow control without heavy exceptions.
- **Repository Pattern with Ardalis Specifications**: Adheres to the Open-Closed Principle.
- **Unit of Work**: Manages transactions across multiple repositories.
- **Hangfire**: Handles background job processing.
- **MassTransit & RabbitMQ**: Facilitates message-based communication.
- **Transactional Outbox Pattern**: Ensures reliable message delivery.
- **Redis or In-Memory Caching**: Provides caching mechanisms to improve performance.
- **Strongly Typed IDs**: Prevents primitive obsession by using specific types for identifiers.
- **Localization & Multi-Language Support**: Supports multiple languages and regional settings.
- **Pagination and Flexible Search**: Implements efficient data retrieval techniques.
- **MassTransit's Request Client**: Enables inter-module communication and data transfer.
- **Option Pattern**: Provides a flexible configuration mechanism.
- **Fluent Validation**: Ensures input validation with a fluent API.
- **Functional Programming & Railway-Oriented Syntax**: Encourages functional programming practices.
- **Docker-Compose Support**: Simplifies development setup with containerization.
- **Grafana Templates for Monitoring**: Provides templates for monitoring application metrics.

## Requirements

To use or contribute to this project, you will need:

- .NET 9 SDK
- Docker
- Access to a SQL Server instance (if applicable)
- Other dependencies as specified in the project documentation.

## Getting Started

- Clone the repository: `git@github.com:baranacikgoz/modular-monolith-ddd-vsa-webapi.git`

### VSCode

#### One-time setup
- Use appropriate ``tasks.json`` depending on your OS
    - Copy ``tasks.windows.json`` or ``tasks.unix.json``, into a new file with the name ``tasks.json``.

##### Unix only
- Make before and after restore & build scripts executable
    - ``chmod +x .vscode/exclude_docker_compose_dcproj.sh``
    - ``chmod +x .vscode/dotnet_build_and_cleanup.sh``
    - ``chmod +x .vscode/revert_exclude.sh``

#### Development
- Run required services
    - If you have VSCode Docker extension installed, right click on ``docker-compose.yml`` and select ``Compose Up - Select Services``. Select the following services:
        - ``mm.database``
        - ``mm.rabbitmq``
        - ``mm.seq`` - Optional but recommended
    - Or run the following command in terminal:
        ```bash
        docker compose -f "docker-compose.yml" up -d --build mm.database mm.rabbitmq mm.seq
        ```
- Run the application
    - Press ``F5`` to start the application in debug mode

### Visual Studio
- Open the solution file ``ModularMonolith.sln`` in Visual Studio

- Set ``Docker Compose`` as the startup project
    - Right click on the ``Docker Compose`` project and select ``Set as Startup Project`` if it's not already set

- Start application

<!-- ## API Documentation

Links to API documentation (if available), or instructions on how to generate it.

## Contributing

Guidelines for contributing to the project, including how to report bugs, suggest features, or submit pull requests.

## License

Information about the project's license. -->
