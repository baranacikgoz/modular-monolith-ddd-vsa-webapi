---
description: Update NuGet packages via Central Package Management, then build and run the full suite.
argument-hint: ""
allowed-tools: Read, Edit, Write, Bash, Glob, Grep
---

Update NuGet dependencies.

1. Read `Directory.Packages.props`.
2. Outdated list per project (solution-wide fails on `docker-compose.dcproj` with `NU1105`): `dotnet list <path>.csproj package --outdated` for each `.csproj`.
3. Plan: package, current, target. No major bumps without explicit approval.
4. Edit only the `<PackageVersion>` entries in `Directory.Packages.props`.
5. `make build`, fix breaking API changes.
6. `make test`.
7. Report each package (old to new), build result, test result.
