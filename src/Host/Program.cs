using Common.Options;
using Host.Configurations;
using Host.Logging;
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

Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
                .CreateLogger();

Log.Information("Server Booting Up...");

try
{
    // Create the builder and add initially required services.
    var builder = WebApplication.CreateBuilder(args);
    builder.AddConfigurations();
    builder.Services.AddCommonOptions(builder.Configuration);
    builder.UseSerilogAsLoggingProvider();

    // Add services to the container.
    builder
        .Services
            .AddHttpContextAccessor()
            .AddSingleton<RequestResponseLoggingMiddleware>()
            .AddCustomLocalization("Resources")
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
                    typeof(Appointments.AssemblyReference).Assembly,
                    typeof(IdentityAndAuth.AssemblyReference).Assembly,
                    typeof(Notifications.AssemblyReference).Assembly
                );
            })
            .AddEventBus(
                builder.Configuration,
                typeof(Appointments.AssemblyReference).Assembly,
                typeof(IdentityAndAuth.AssemblyReference).Assembly,
                typeof(Notifications.AssemblyReference).Assembly
            )
            .AddValidatorsFromAssemblies(
                new List<Assembly>()
                {
                    typeof(Appointments.AssemblyReference).Assembly,
                    typeof(IdentityAndAuth.AssemblyReference).Assembly,
                    typeof(Notifications.AssemblyReference).Assembly
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
        .UseMiddleware<ExceptionHandlingMiddleware>()
        .UseAuth();

    app.UseCustomSwagger();

    var rootGroup = app
        .MapGroup("/")
        .RequireAuthorization()
        .AddFluentValidationAutoValidation();

    app.UseEventBus();
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
