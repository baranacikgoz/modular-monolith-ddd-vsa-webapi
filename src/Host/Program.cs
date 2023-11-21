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

    // Add options to the container.
    builder
        .Services
        .AddCommonOptions(builder.Configuration);

    // Add services to the container.
    builder
        .Services
            .AddHttpContextAccessor()
            .AddSingleton<RequestResponseLoggingMiddleware>()
            .AddCustomLocalization("Resources")
            .AddRateLimiting(builder.Configuration)
            .AddSingleton<ExceptionHandlingMiddleware>()
            .AddSingleton<IErrorLocalizer, AggregatedErrorLocalizer>(_ =>
            {
                return new AggregatedErrorLocalizer(
                    IdentityAndAuth.ModuleSetup.ErrorLocalization.ErrorsAndLocalizations.Get(),
                    Appointments.ModuleSetup.ErrorLocalization.ErrorsAndLocalizations.Get()
                    );
            })
            .AddSingleton<IProblemDetailsFactory, ProblemDetailsFactory>()
            .AddCaching()
            .AddEventBus(
                typeof(Appointments.IAssemblyReference).Assembly,
                typeof(IdentityAndAuth.IAssemblyReference).Assembly,
                typeof(Notifications.IAssemblyReference).Assembly
            )
            .AddValidatorsFromAssemblies(
                new List<Assembly>()
                {
                    typeof(Appointments.IAssemblyReference).Assembly,
                    typeof(IdentityAndAuth.IAssemblyReference).Assembly,
                    typeof(Notifications.IAssemblyReference).Assembly
                }
            )
            .AddFluentValidationAutoValidation(cfg => cfg.OverrideDefaultResultFactoryWith<CustomFluentValidationResultFactory>())
            .AddEndpointsApiExplorer()
            .AddCustomSwagger()
            .AddOpenTelemetry()
            .WithTracing((x) =>
            {
                x.AddAspNetCoreInstrumentation(o => o.Filter = httpContext => httpContext.Request.Path != "/metrics");
                x.AddHttpClientInstrumentation(o =>
                {
                    // Filter out requests to Seq.
                    Uri seqUrl = builder.Configuration.GetSection(nameof(CustomLoggingOptions)).Get<CustomLoggingOptions>()?.SeqUrl ?? throw new InvalidOperationException("SeqUrl is null.");
                    o.FilterHttpWebRequest = request => request.RequestUri != seqUrl;
                });
                x.AddEntityFrameworkCoreInstrumentation();
                x.AddNpgsql();
                x.ConfigureResource(r =>
                {
                    var monitoringOptions = builder.Configuration.GetSection(nameof(MonitoringOptions)).Get<MonitoringOptions>() ?? throw new InvalidOperationException("MonitoringOptions is null.");
                    r.AddService(monitoringOptions.ServiceName);
                    r.AddTelemetrySdk();
                });
                x.AddOtlpExporter(o =>
                {
                    var monitoringOptions = builder.Configuration.GetSection(nameof(MonitoringOptions)).Get<MonitoringOptions>() ?? throw new InvalidOperationException("MonitoringOptions is null.");
                    o.Endpoint = new Uri(monitoringOptions.OtlpEndpoint);
                    o.Protocol = OtlpExportProtocol.Grpc;
                });
            })
            .WithMetrics(x =>
            {
                x.AddAspNetCoreInstrumentation(o => o.Filter = (_, httpContext) => httpContext.Request.Path != "/metrics");
                x.AddHttpClientInstrumentation();
                x.AddRuntimeInstrumentation();
                x.AddProcessInstrumentation();
                x.AddMeter(
                    "Microsoft.AspNetCore.Hosting",
                    "Microsoft.AspNetCore.Server.Kestrel");
                x.AddView("http.server.request.duration",
                    new ExplicitBucketHistogramConfiguration
                    {
                        Boundaries = [0, 0.005, 0.01, 0.025, 0.05, 0.075, 0.1, 0.25, 0.5, 0.75, 1, 2.5, 5, 7.5, 10]
                    });
                x.AddPrometheusExporter();
            });

    // Add modules to the container.
    builder
        .Services
            .InstallIdentityAndAuthModule(builder.Configuration)
            .InstallAppointmentsModule();

    // Build the app and configure pipeline.
    var app = builder.Build();

    app
        .UseWhen(
            context => context.Request.Path != "/metrics",
            appBuilder => appBuilder.UseMiddleware<RequestResponseLoggingMiddleware>()
        )

        // I generally run api projects behind a reverse proxy, so no need to use https,
        // but if your kestrel is directly communicating with the outside world,
        // you should uncomment the following line. and carefully set up your certificates and ports.
        //.UseHttpsRedirection()

        .UseCustomLocalization()
        .UseRateLimiter()
        .UseMiddleware<ExceptionHandlingMiddleware>()
        .UseAuth();

    app.UseCustomSwagger();
    app.MapPrometheusScrapingEndpoint();

    var rootGroup = app
        .MapGroup("/")
        .RequireAuthorization()
        .AddFluentValidationAutoValidation();

    app.UseIdentityAndAuthModule(rootGroup);
    app.UseAppointmentsModule(rootGroup);

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
