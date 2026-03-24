#!/usr/bin/env bash

# Exit immediately if any command returns a non-zero status
set -e

echo "=========================================================="
echo "🚀 Running Unified Build for all projects"
echo "=========================================================="

# Dynamically find all C# projects and format them into a JSON array string
PROJECTS=$(find src -name "*.csproj" | awk '{print "\""$0"\""}' | paste -sd "," -)

# Generate a temporary MSBuild Solution Filter file (.slnf)
# This excludes `docker-compose.dcproj` which often causes build issues in some environments
cat <<EOF > ModularMonolith.Build.slnf
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
echo "▶️ Building Solution Filter: ModularMonolith.Build.slnf"

# Execute build across all defined projects
dotnet build ModularMonolith.Build.slnf

# Clean up the generated filter file
rm ModularMonolith.Build.slnf

echo "=========================================================="
echo "✅ Build completed successfully!"
