#!/bin/bash

# Execute dotnet sln command to remove projects with .dcproj extension
dotnet sln "ModularMonolith.sln" remove ./docker-compose.dcproj
