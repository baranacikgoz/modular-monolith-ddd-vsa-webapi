---
description: Scaffold a new top-level module — split projects, DbContext, Persistence Setup, IModule, Telemetry, test factory.
argument-hint: "<ModuleName>"
allowed-tools: Read, Edit, Write, Bash, Glob, Grep
---

Scaffold module: $ARGUMENTS

1. **Create class library projects** (one per layer, remove generated `Class1.cs` from each):
   ```bash
   dotnet new classlib -n {Module}.Domain -o src/Modules/{Module}/{Module}.Domain
   dotnet new classlib -n {Module}.Application -o src/Modules/{Module}/{Module}.Application
   dotnet new classlib -n {Module}.Endpoints -o src/Modules/{Module}/{Module}.Endpoints
   dotnet new classlib -n {Module}.Infrastructure -o src/Modules/{Module}/{Module}.Infrastructure
   ```
   Add `IAssemblyReference.cs` (empty marker interface) to each of the four projects:
   ```csharp
   namespace {Module}.{Layer};
   public interface IAssemblyReference { }
   ```

2. **`I{Module}DbContext`** interface in `{Module}.Application/Persistence/`.

3. **`{Module}DbContext`** in `{Module}.Infrastructure/Persistence/`:
   - Inherit `BaseDbContext`
   - Schema: `modelBuilder.HasDefaultSchema(nameof({Module}))` — **PascalCase, not lowercase**
   - Apply `AuditLogEntryConfiguration` and `modelBuilder.Ignore<DomainEvent>()`

4. **`Infrastructure/Persistence/Setup.cs`** — extension methods called from `IModule`:
   ```csharp
   public static IServiceCollection AddPersistence(this IServiceCollection services)
       => services.AddModuleDbContext<I{Module}DbContext, {Module}DbContext>(nameof({Module}));

   public static IApplicationBuilder UsePersistence(this IApplicationBuilder app)
   {
       // MigrationGuard.EnsureNoMigrationsPending<{Module}DbContext>(...)
       return app;
   }
   ```

5. **`Infrastructure/Telemetry/{Module}Telemetry.cs`** — static telemetry class:
   ```csharp
   public static class {Module}Telemetry
   {
       private const string Prefix = "ModularMonolith";
       public const string ActivitySourceName = Prefix + "." + nameof({Module});
       public const string MeterName = Prefix + "." + nameof({Module});
       public static readonly ActivitySource ActivitySource = new(ActivitySourceName);
       public static readonly Meter Meter = new(MeterName);
       // Add module-specific counters here
   }
   ```

6. **`{Module}Module.cs`** in `{Module}.Endpoints/` — full `IModule` implementation:
   ```csharp
   public sealed class {Module}Module : IModule
   {
       public string Name => "{Module}";
       public int StartupPriority => N; // choose based on dependency order

       public IEnumerable<string> ActivitySourceNames => [{Module}Telemetry.ActivitySourceName];
       public IEnumerable<string> MeterNames => [{Module}Telemetry.MeterName];

       // Optional — only add if module has rate limiting policies:
       // public IEnumerable<Action<RateLimiterOptions, CustomRateLimitingOptions>>? RateLimitingPolicies => Policies.Get();

       public void AddServices(IServiceCollection services, IConfiguration configuration)
           => services.AddPersistence();

       public void UseModule(IApplicationBuilder app)
           => app.UsePersistence();

       public void MapEndpoints(IEndpointRouteBuilder endpoints)
       {
           var apiVersionSet = endpoints.GetApiVersionSet();
           var group = endpoints
               .MapGroup("/v{version:apiVersion}")
               .AddFluentValidationAutoValidation()
               .WithApiVersionSet(apiVersionSet)
               .RequireAuthorization();

           // group.Map{Aggregate}Endpoints();
       }
   }
   ```

7. **Enable**: add `"{Module}"` to `"Modules"` array in `src/Host/Host/appsettings.json`.

8. **Test project**:
   ```bash
   dotnet new xunit -n {Module}.Tests -o src/Modules/{Module}/{Module}.Tests
   dotnet add src/Modules/{Module}/{Module}.Tests reference src/Modules/{Module}/{Module}.Endpoints
   ```
   Add packages: `Testcontainers.PostgreSql`, `Bogus`, `NSubstitute`, `Respawn`.

9. **Test factory + collection** — two files:

   `IntegrationTestWebAppFactory.cs`:
   ```csharp
   public class IntegrationTestWebAppFactory : IntegrationTestFactory
   {
       protected override string[] GetActiveModules() => ["{Module}", "Outbox", "IAM"];
   }
   ```

   `IntegrationTestCollection.cs`:
   ```csharp
   [CollectionDefinition("IntegrationTestCollection")]
   public class IntegrationTestCollection : ICollectionFixture<IntegrationTestWebAppFactory> { }
   ```

   Tests use `[Collection("IntegrationTestCollection")]` and receive `IntegrationTestWebAppFactory` via constructor injection. Call `factory.CreateClient()` lazily inside each test (not in constructor) — see CLAUDE.md §8 for `ICollectionFixture` rules.

10. **Makefile target**: add `test-{module}` mirroring existing targets.

11. Confirm: `make build` then `make test-{module}`.
