skill:
  name: "scaffold-module"
  description: "Creates a new Top-Level Module adhering to the Context Map structure."
  inputs:
    - name: module_name
      description: "PascalCase name of the module (e.g. Inventory)"

  instructions: |
    1. **Create Directory Structure**:
       - `src/Modules/{{module_name}}/Endpoints`
       - `src/Modules/{{module_name}}/Infrastructure/Persistence`
       - `src/Modules/{{module_name}}/Application`
       - `src/Modules/{{module_name}}/Domain`

    2. **Create Project**:
       - Run: `dotnet new classlib -n {{module_name}} -o src/Modules/{{module_name}}`
       - Remove `Class1.cs`.

    3. **Create Infrastructure Files**:
       - **DbContext**: Create `{{module_name}}DbContext` inheriting `BaseDbContext`. Set schema to `{{module_name}}`.
       - **Installer**: Create `Infrastructure/ModuleInstaller.cs`.
         - Method `Add{{module_name}}Module(IServiceCollection)`: Register DbContext.
         - Method `Map{{module_name}}Endpoints(IEndpointRouteBuilder)`: Map group.

    4. **Output Warning**:
       - "ACTION REQUIRED: Go to `src/Host/Infrastructure/Setup.Modules.cs`. Add `.Add{{module_name}}Module()` and `.Map{{module_name}}Endpoints()` manually (No reflection magic allowed)."

    5. **Create Test Project**:
       - Run: `dotnet new xunit -n {{module_name}}.Tests -o src/Modules/{{module_name}}/Tests`
       - Add Ref: `dotnet add src/Modules/{{module_name}}/Tests reference src/Modules/{{module_name}}`
       - Add Packages: `FluentAssertions`, `Testcontainers.PostgreSql`, `Bogus`, `NSubstitute`, `Respawn`.

    6. **Create Integration Fixture**:
       - Create `IntegrationTestWebAppFactory.cs` in Tests folder.
       - Configure it to spin up a Postgres Container and replace the DbContext in DI.
       - Setup `Respawner` to clean tables between tests.

    7. **Output Warning**:
       - "ACTION REQUIRED: Register module in Host, then run `dotnet test` to confirm setup."
