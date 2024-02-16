#!/bin/bash

# Copy ModularMonolith.sln file to temp file
cp "ModularMonolith.sln" "ModularMonolith_temp.sln"

# Execute dotnet sln command to remove projects with .dcproj extension
dotnet sln "ModularMonolith.sln" remove ./docker-compose.dcproj
