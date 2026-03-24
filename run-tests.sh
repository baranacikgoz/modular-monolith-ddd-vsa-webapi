#!/usr/bin/env bash

# Exit immediately if any command returns a non-zero status
set -e

echo "=========================================================="
echo "🚀 Running Unified Integration & Unit Test Suite"
echo "=========================================================="

# Dynamically find all test projects and format them into a JSON array string
PROJECTS=$(find src -name "*.Tests.csproj" | awk '{print "\""$0"\""}' | paste -sd "," -)

# Generate a temporary MSBuild Solution Filter file (.slnf)
# This explicitly tells `dotnet test` to ONLY load and build test projects from the `.sln`,
# which beautifully bypasses the NU1105 error from `docker-compose.dcproj` and 
# aggregates all test results into ONE unified TRX file and ONE coverage report.
cat <<EOF > ModularMonolith.Tests.slnf
{
  "solution": {
    "path": "ModularMonolith.sln",
    "projects": [
      $PROJECTS
    ]
  }
}
EOF

echo ""
echo "▶️ Testing Solution Filter: ModularMonolith.Tests.slnf"

# Execute tests across all defined projects through the single Solution Filter
# Using quiet verbosity to hide background service shutdown logs (e.g. Postgres terminating early)
dotnet test ModularMonolith.Tests.slnf --verbosity quiet

# Clean up the generated filter file
rm ModularMonolith.Tests.slnf

echo "=========================================================="
echo "✅ All tests completed successfully!"
