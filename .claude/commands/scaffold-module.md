---
description: Scaffold a new top-level module — split projects, DbContext, ModuleInstaller, IModule, test factory.
argument-hint: "<ModuleName>"
allowed-tools: Read, Edit, Write, Bash, Glob, Grep
---

Scaffold module: $ARGUMENTS

1. **Create project directories**:
   - `src/Modules/{Module}/{Module}.Domain/`
   - `src/Modules/{Module}/{Module}.Application/`
   - `src/Modules/{Module}/{Module}.Endpoints/`
   - `src/Modules/{Module}/{Module}.Infrastructure/`

2. **Create class library projects** (one per layer, remove generated `Class1.cs` from each):
   ```bash
   dotnet new classlib -n {Module}.Domain -o src/Modules/{Module}/{Module}.Domain
   dotnet new classlib -n {Module}.Application -o src/Modules/{Module}/{Module}.Application
   dotnet new classlib -n {Module}.Endpoints -o src/Modules/{Module}/{Module}.Endpoints
   dotnet new classlib -n {Module}.Infrastructure -o src/Modules/{Module}/{Module}.Infrastructure
   ```

3. **DbContext** in `{Module}.Infrastructure/`: `{Module}DbContext` inheriting `BaseDbContext`, schema set to `{module_lowercase}`.

4. **`I{Module}DbContext`** interface in `{Module}.Application/Persistence/`.

5. **ModuleInstaller** in `{Module}.Infrastructure/ModuleInstaller.cs`:
   - `Add{Module}Module(IServiceCollection)` — registers DbContext and services.
   - `Map{Module}Endpoints(IEndpointRouteBuilder)` — maps the endpoint group.

6. **`{Module}Module.cs`** in `{Module}.Endpoints/`: implements `IModule` for dynamic DI and endpoint scanning.

7. **Enable**: add `"{Module}"` to `"Modules"` array in `src/Host/Host/appsettings.json`.

8. **Test project**:
   ```bash
   dotnet new xunit -n {Module}.Tests -o src/Modules/{Module}/{Module}.Tests
   dotnet add src/Modules/{Module}/{Module}.Tests reference src/Modules/{Module}/{Module}.Endpoints
   ```
   Add packages: `Testcontainers.PostgreSql`, `Bogus`, `NSubstitute`, `Respawn`.

9. **Test factory**: create `{Module}TestFactory : IntegrationTestFactory`, override `GetActiveModules() => ["{Module}"]`. Configure `Respawner`.

10. **Makefile target**: add `test-{module}` mirroring existing targets.

11. Confirm: `make build` then `make test-{module}`.
