using Host.Configurations;
using Host.Swagger;
using Host.Infrastructure;
using Serilog;
using Common.Infrastructure.Options;

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

    // Add services to the container.
    builder
        .Services
            .AddInfrastructure(builder.Configuration, builder.Environment)
            .AddModules(builder.Configuration, builder.Environment)
            .AddCustomSwagger();

    // Build the app and configure pipeline.
    var app = builder.Build();

    app.UseInfrastructure(builder.Environment, builder.Configuration);

    app.UseModules();

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
