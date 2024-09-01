using Host.Configurations;
using Serilog;
using Host.Swagger;
using Host.Infrastructure;
using Common.Infrastructure.Persistence;
using Common.Infrastructure.Options;
using Common.Infrastructure;
using System.Reflection;
using Host.Plugins;

// Create the builder and add initially required services.
var builder = WebApplication.CreateBuilder(args);
builder.AddConfigurations();
Log.Logger = new LoggerConfiguration()
                .ApplyConfigurations(options: builder
                                        .Configuration
                                        .GetSection(nameof(ObservabilityOptions))
                                        .Get<ObservabilityOptions>() ?? throw new InvalidOperationException($"{nameof(ObservabilityOptions)} is null."),
                                     env: builder.Environment)
                .CreateLogger();
try
{
    Log.Information("Server Booting Up...");
    builder
        .Host
        .UseCustomizedSerilog();

    var services = builder.Services;
    var modules = PluginLoader
        .LoadModules(builder.Environment)
        .ToList();
    // Add services to the container.
    services
        .AddInfrastructure(builder.Configuration, builder.Environment, modules)
        .AddCustomSwagger();

    services.RegisterModules(modules, builder.Configuration, builder.Environment);

    // Build the app and configure pipeline.
    var app = builder.Build();

    app.UseInfrastructure(builder.Environment, builder.Configuration);

    app.UseModules(modules);

    app.MapGet("/", () => Results.Redirect("/swagger"));

    app.UseCustomSwagger(builder.Environment);

    await app.RunAsync();
}
#pragma warning disable CA1031
catch (Exception ex)
#pragma warning restore CA1031
{
    Log.Fatal(ex, "Server terminated unexpectedly.");
}
finally
{
    Log.Information("Server Shutting down...");
    await Log.CloseAndFlushAsync();
}
