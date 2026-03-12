#!/bin/bash

# Find and run all test projects in the src directory
for project in $(find src -name "*.Tests.csproj"); do
    echo -e "\n--- Running tests for $project ---"
    dotnet test "$project"
done
