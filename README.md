# Modular Monolith, DDD, Vertical Slice Architecture WebAPI Boilerplate

This repository provides a .NET 9 WebAPI boilerplate implementing a Modular Monolith approach, Domain-Driven Design (DDD), and Vertical Slices architecture, along with Clean Architecture principles per feature. It is designed to facilitate the development of scalable and maintainable applications.

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Requirements](#requirements)
- [Getting Started](#getting-started)
- [API Documentation](#api-documentation)
- [Contributing](#contributing)
- [License](#license)

## Introduction

This boilerplate serves as a starting point for building applications with a focus on modularity, scalability, and maintainability. It leverages modern architectural patterns and practices to ensure a robust and flexible codebase.

## Features

This repository includes the following features:

- **Event-Driven Architecture**: Supports event-driven communication between components.
- **Modular Monolithic Architecture**: Organizes code into modules for better separation of concerns.
- **Vertical Slices, REPR, & Minimal APIs**: Implements vertical slice architecture for feature-based organization.
- **Domain-Driven Design & Clean Architecture**: Adheres to DDD principles and clean architecture for maintainable code.
- **Identity and Access Management (IAM)**: Provides built-in IAM capabilities.
- **Event-Sourcing Aggregates**: Supports event sourcing infrastructure for audit/event logging currently, with future plans for full event sourcing.
- **OpenTelemetry Support**: Integrates with Otel-Collector, Prometheus, Jaeger, and Seq for observability.
- **Result Monad for Error Management**: Utilizes result monads for error handling and flow control.
- **Unit of Work**: Ensures atomic operations across multiple repositories.
- **Hangfire**: Supports background job processing.
- **MassTransit & RabbitMQ**: Facilitates message-based communication.
- **Transactional Outbox Pattern**: Ensures reliable message delivery.
- **CDC - Kafka & Debezium**: Processes outbox messages with Kafka & Debezium.
- **Redis or In-Memory Caching**: Provides caching mechanisms for performance optimization.
- **Strongly Typed IDs**: Prevents primitive obsession by using strongly typed identifiers.
- **Localization & Multi-Language Support**: Supports multiple languages and localization.
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

- .NET 9 SDK
- Docker

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

## API Documentation

Links to API documentation (if available), or instructions on how to generate it.

## Contributing

Guidelines for contributing to the project, including how to report bugs, suggest features, or submit pull requests.

## License

Information about the project's license.

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
