#!/bin/bash

# Save the current directory
currentDir=$(pwd)

# Change to the project's root directory (assumed to be the parent of the .vscode directory)
cd "$(dirname "$0")/.."
echo "Building in $(pwd)"

# Build the solution and capture the exit code
dotnet build "$1" /property:GenerateFullPaths=true /consoleloggerparameters:NoSummary;ForceNoAlign
buildExitCode=$?

# Return to the original directory
cd "$currentDir"

# Perform the cleanup task or revert actions regardless of the build success
# Pass the path to the revert_exclude.sh script as the second argument
bash "$2"

# Exit with the original build exit code to indicate failure if build failed
exit $buildExitCode
