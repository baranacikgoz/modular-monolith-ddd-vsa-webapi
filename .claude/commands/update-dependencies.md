Safely update NuGet packages using Central Package Management (CPM).

1. **Analyze current dependencies**: read `Directory.Packages.props` to understand the package ecosystem.

2. **Find outdated packages**:
   ```bash
   dotnet list package --outdated
   ```

3. **Draft update plan**: list each package with its current version → target version. Do not upgrade major versions without explicit user approval unless requested.

4. **Apply updates**: modify `<PackageVersion Include="..." Version="..." />` entries in `Directory.Packages.props`. Make minimal, targeted edits — do not change packages not in the update plan.

5. **Restore and compile**:
   ```bash
   make build
   ```
   Fix any compilation errors or breaking API changes before proceeding.

6. **Full quality gate**:
   ```bash
   make test
   ```
   Confirm upgraded packages do not break integration tests, Kafka consumers, or database interactions.

7. **Report**: list each package updated (old → new), compilation result, and test result.
