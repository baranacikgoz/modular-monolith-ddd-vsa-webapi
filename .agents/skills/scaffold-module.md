---
description: Scaffold a new top-level module with directory structure, DbContext, ModuleInstaller, IModule, and test project.
---

Scaffold a new top-level module. Ask for the module name (PascalCase, e.g. `Inventory`) if not provided.

1. **Create directory structure**:
   - `src/Modules/{Module}/Domain/`
   - `src/Modules/{Module}/Application/`
   - `src/Modules/{Module}/Infrastructure/Persistence/`
   - `src/Modules/{Module}/Endpoints/`

2. **Create class library project**:
   ```bash
   dotnet new classlib -n {Module} -o src/Modules/{Module}
   ```
   Delete the generated `Class1.cs`.

3. **DbContext**: create `{Module}DbContext` inheriting `BaseDbContext`, schema set to `{module_lowercase}`.

4. **ModuleInstaller** at `Infrastructure/ModuleInstaller.cs`:
   - `Add{Module}Module(IServiceCollection)` — registers DbContext and services.
   - `Map{Module}Endpoints(IEndpointRouteBuilder)` — maps the endpoint group.

5. **IModule implementation**: create `{Module}Module.cs` implementing `IModule` to hook into dynamic DI and endpoint scanning.

6. **Enable module**: add `"{Module}"` to the `"Modules"` array in `src/Host/appsettings.json`.

7. **Test project**:
   ```bash
   dotnet new xunit -n {Module}.Tests -o src/Modules/{Module}/{Module}.Tests
   dotnet add src/Modules/{Module}/{Module}.Tests reference src/Modules/{Module}
   ```
   Add packages: `Testcontainers.PostgreSql`, `Bogus`, `NSubstitute`, `Respawn`.

8. **IntegrationTestWebAppFactory**: spin up a Postgres container, replace the DbContext in DI, configure `Respawner` to reset tables between tests. Enforce the module via `TestModuleOverride` env var.

9. **Add test target to Makefile**: add `test-{module}` target mirroring existing targets.

10. Confirm with `make build` and `make test-{module}`.
