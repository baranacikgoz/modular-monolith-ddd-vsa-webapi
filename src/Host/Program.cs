using Common.Options;
using Host.Configurations;
using Serilog;
using Common.Localization;
using NimbleMediator.ServiceExtensions;
using System.Globalization;
using Common.Caching;
using IdentityAndAuth;
using Host.Swagger;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using FluentValidation;
using System.Reflection;
using Host.Validation;
using Appointments;
using Host.Middlewares;
using Common.Core.Contracts;
using Common.Eventbus;
using Microsoft.Extensions.Options;
using Notifications;
using NimbleMediator.NotificationPublishers;
using IdentityAndAuth.ModuleSetup;
using Appointments.ModuleSetup;
using Common.Core.Interfaces;
using Host.Infrastructure;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Npgsql;
using OpenTelemetry.Exporter;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using OpenTelemetry.Instrumentation.AspNetCore;

// Create the builder and add initially required services.
var builder = WebApplication.CreateBuilder(args);
builder.AddConfigurations();
Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();
try
{
    Log.Information("Server Booting Up...");
    builder
        .Host
        .UseSerilog((context, conf) => conf.ReadFrom.Configuration(context.Configuration));

    // Add services to the container.
    builder
        .Services
            .AddInfrastructure(builder.Configuration)
            .AddModules(builder.Configuration);

    // Build the app and configure pipeline.
    var app = builder.Build();

    app.UseInfrastructure(builder.Environment, builder.Configuration);

    app.UseModules();

    app.MapGet("/", () => "https://github.com/baranacikgoz/modular-monolith-ddd-vsa-webapi");

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
