using Common.Application.Options;
using Host.Configurations;
using Host.Infrastructure;
using Host.Swagger;
using Serilog;

// Create the builder and add initially required services.
var builder = WebApplication.CreateBuilder(args);
builder.AddConfigurations();
Log.Logger = new LoggerConfiguration()
    .ApplyConfigurations(builder
                             .Configuration
                             .GetSection(nameof(ObservabilityOptions))
                             .Get<ObservabilityOptions>() ??
                         throw new InvalidOperationException($"{nameof(ObservabilityOptions)} is null."),
        builder.Environment)
    .CreateLogger();
try
{
    Log.Information("Server Booting Up...");
    builder
        .Host
        .UseCustomizedSerilog();

    // Add services to the container.
    builder
        .Services
        .AddModules(builder.Configuration)
        .AddInfrastructure(builder.Configuration, builder.Environment);

    var isSwaggerEnabled = builder.Configuration
                               .GetSection(nameof(OpenApiOptions))
                               .Get<OpenApiOptions>()?.EnableSwagger ??
                           throw new InvalidOperationException($"{nameof(OpenApiOptions)} is null.");
    if (isSwaggerEnabled)
    {
        builder.Services.AddCustomSwagger();
    }

    // Build the app and configure pipeline.
    var app = builder.Build();

    app.UseInfrastructure();

    app.UseModules();

    app.MapModuleEndpoints();

    app.MapCustomHealthChecks();

    if (isSwaggerEnabled)
    {
        app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();
    }

    app.UseCustomSwagger(builder.Environment);

    await app.RunAsync();
}
#pragma warning disable CA1031
// HostAbortedException is control flow, not failure: EF design-time tools and WebApplicationFactory
// abort Main once they've captured the builder.
catch (Exception ex) when (ex is not HostAbortedException)
#pragma warning restore CA1031
{
    Log.Fatal(ex, "Server terminated unexpectedly.");

    // Rethrow: swallowing here exits 0 (K8s sees a clean exit and won't restart-loop visibly) and
    // leaves WebApplicationFactory waiting forever for a host that never started — a silent CI hang.
    throw;
}
finally
{
    Log.Information("Server Shutting down...");
    await Log.CloseAndFlushAsync();
}

namespace Host
{
    public class Program
    {
        protected Program()
        {
        }
    }
}
