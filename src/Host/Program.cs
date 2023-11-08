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
using Common.Core.Implementations;
using Common.Eventbus;
using Microsoft.Extensions.Options;
using Notifications;
using Common.RateLimiting;
using NimbleMediator.NotificationPublishers;

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
            .AddSingleton<IErrorTranslator, LocalizedErrorTranslator>(sp =>
            {
                var identityAndAuthModuleErrors = IdentityAndAuth.ErrorsToLocalize.GetErrorsAndMessages();
                var appointmentsModuleErrors = Appointments.ErrorsToLocalize.GetErrorsAndMessages();

                return new LocalizedErrorTranslator(
                    identityAndAuthModuleErrors,
                    appointmentsModuleErrors
                    );

            })
            .AddSingleton<IResultTranslator, ResultTranslator>()
            .AddCaching()
            .AddNimbleMediator(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(Appointments.IAssemblyReference).Assembly,
                    typeof(IdentityAndAuth.IAssemblyReference).Assembly,
                    typeof(Notifications.IAssemblyReference).Assembly
                );

                cfg.SetDefaultNotificationPublisher<TaskWhenAllPublisher>();
            })
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
            .AddCustomSwagger();

    // Add modules to the container.
    builder
        .Services
            .InstallIdentityAndAuthModule(builder.Configuration)
            .InstallAppointmentsModule();

    // Build the app and configure pipeline.
    var app = builder.Build();

    app
        .UseMiddleware<RequestResponseLoggingMiddleware>()

        // I generally run api projects behind a reverse proxy, so no need to use https,
        // but if your kestrel is directly communicating with the outside world,
        // you should uncomment the following line. and carefully set up your certificates and ports.
        //.UseHttpsRedirection()

        .UseCustomLocalization()
        .UseRateLimiter()
        .UseMiddleware<ExceptionHandlingMiddleware>()
        .UseAuth();

    app.UseCustomSwagger();

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
