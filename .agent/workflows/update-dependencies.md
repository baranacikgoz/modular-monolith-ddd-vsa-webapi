---
description: Update NuGet dependencies centrally and verify integrity
---
# Workflow: Update Dependencies

This workflow is used to safely update NuGet package versions across the entire monolithic codebase using Central Package Management (CPM), ensuring that no architectural regressions occur.

1. **Analyze Dependencies**: Read `Directory.Packages.props` to understand the current external library ecosystem.
2. **Check for Updates**: Run `dotnet list package --outdated` to identify which packages have newer stable versions available.
3. **Draft Plan**: Create or update the `implementation_plan.md` listing the packages to be updated, their current versions, and their target versions.
4. **Update CPM (Safe Execution)**: Modify the specific `<PackageVersion Include="..." Version="..." />` tags inside the `Directory.Packages.props` file to the new stable versions. (Do not arbitrarily update major versions without explicit User approval unless requested).
// turbo-all
5. **Restore & Compile**: Run `dotnet restore ModularMonolith.sln` and `dotnet build src/Host/Host.csproj --no-restore` to verify the new packages do not cause compilation errors or breaking API changes.
6. **Verify Quality Gate**: Run the unified test suite `make test` to ensure the upgraded packages do not break complex Integration Tests, Kafka consumers, or database architectures.
7. **Handoff**: Use `notify_user` to return execution control back to the Developer, providing a successful upgrade summary.
