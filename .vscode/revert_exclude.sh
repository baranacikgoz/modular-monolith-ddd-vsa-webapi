#!/bin/bash

# Change to the directory where the script is located
cd "$(dirname "$0")"

# Revert changes to the solution file
git checkout -- "ModularMonolith.sln"
