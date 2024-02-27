# Project Name

A .NET 8 Webapi boilerplate with Modular Monolith approach, Domain-Driven Design and Vertical Slices architecture along with Clean Architecture principles per feature.

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Requirements](#requirements)
- [Getting Started](#getting-started)
- [API Documentation](#api-documentation)
- [Contributing](#contributing)
- [License](#license)

## Introduction

Brief introduction to the project, its purpose, and goals.

## Features

List of key features provided by the boilerplate:

- Feature 1
- Feature 2
- ...

## Requirements

List of prerequisites to use or contribute to the project:

- .NET Core SDK version X.X.X
- SQL Server (if applicable)
- ...

## Getting Started

- Clone the repository: `git@github.com:baranacikgoz/modular-monolith-ddd-vsa-webapi.git`

### VSCode

#### One-time setup
- Use appropriate ``tasks.json`` depending on your OS
    - Duplicate ``tasks.windows.json`` or ``tasks.unix.json``, then rename as ``tasks.json``.

##### Unix only
- Make before and after restore & build scripts executable
    - ``chmod +x exclude_docker_compose_dcproj.sh``
    - ``chmod +x revert_exclude.sh``

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
