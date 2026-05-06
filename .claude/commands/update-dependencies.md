---
description: Safely update NuGet packages via Central Package Management — check outdated, plan, update, build, test.
argument-hint: ""
allowed-tools: Read, Edit, Write, Bash, Glob, Grep
---

Update NuGet dependencies.

1. **Analyze**: read `Directory.Packages.props` to understand the current ecosystem.

2. **Find outdated**:
   ```bash
   dotnet list package --outdated
   ```

3. **Draft plan**: list each package with current → target version. Do not upgrade major versions without explicit approval.

4. **Apply**: edit `<PackageVersion Include="..." Version="..." />` entries in `Directory.Packages.props`. Targeted edits only.

5. **Restore and compile**:
   ```bash
   make build
   ```
   Fix any compilation errors or breaking API changes before proceeding.

6. **Full quality gate**:
   ```bash
   make test
   ```
   Confirm no integration tests, Kafka consumers, or DB interactions broke.

7. **Report**: each package updated (old → new), build result, test result.
