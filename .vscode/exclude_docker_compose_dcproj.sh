#!/bin/bash

# Change to the directory where the script is located
cd "$(dirname "$0")"

# Execute dotnet sln command to remove projects with .dcproj extension
dotnet sln ../ModularMonolith.sln remove ../docker-compose.dcproj
