using Host.Configurations;
using Serilog;
using Host.Swagger;
using Host.Infrastructure;
using Common.Persistence;
using Serilog.Sinks.SystemConsole.Themes;
using System.Globalization;

// Create the builder and add initially required services.
var builder = WebApplication.CreateBuilder(args);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.AddConfigurations();
Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    theme: SystemConsoleTheme.Literate,
                    formatProvider: CultureInfo.InvariantCulture)
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
            .Configure<HostOptions>(x =>
            {
                x.ServicesStartConcurrently = true;
                x.ServicesStopConcurrently = true;
            })
            .AddInfrastructure(builder.Configuration, builder.Environment)
            .AddModules(builder.Configuration, builder.Environment)
            .AddCustomSwagger();

    // Build the app and configure pipeline.
    var app = builder.Build();

    app.UseCommonPersistence();
    app.UseInfrastructure(builder.Environment, builder.Configuration);

    app.UseModules();

    app.MapGet("/", () => Results.Redirect("/swagger"));

    app.UseCustomSwagger(builder.Environment);

    app.Run();
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
    Log.CloseAndFlush();
}
