using Common.Application.Options;
using Host.Middlewares;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using BackgroundJobsOptions = Common.Application.Options.BackgroundJobsOptions;
using RequestLoggingOptions = Common.Application.Options.RequestLoggingOptions;

namespace Host.Infrastructure;

internal static partial class Setup
{
    private static IServiceCollection AddHttpRequestLogging(this IServiceCollection services)
    {
        services.AddSingleton<IPostConfigureOptions<RequestLoggingOptions>, RequestLoggingPathPostConfigure>();
        return services.AddScoped<RequestResponseBodyLoggingMiddleware>();
    }

    private static IApplicationBuilder UseHttpRequestLogging(this IApplicationBuilder app)
    {
        var rlOpts = app.ApplicationServices.GetRequiredService<IOptions<RequestLoggingOptions>>().Value;
        var obsOpts = app.ApplicationServices.GetRequiredService<IOptions<ObservabilityOptions>>().Value;
        var thresholdMs = obsOpts.ResponseTimeThresholdInMs;

        app.UseSerilogRequestLogging(o =>
        {
            o.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0} ms";

            o.GetLevel = (ctx, elapsed, _) =>
            {
                var path = ctx.Request.Path;
                for (var i = 0; i < rlOpts.ExcludedPathPrefixes.Count; i++)
                {
                    if (path.StartsWithSegments(rlOpts.ExcludedPathPrefixes[i], StringComparison.OrdinalIgnoreCase))
                    {
                        return LogEventLevel.Verbose;
                    }
                }

                if (elapsed > thresholdMs)
                {
                    return LogEventLevel.Fatal;
                }

                return ctx.Response.StatusCode switch
                {
                    >= 500 => LogEventLevel.Error,
                    >= 400 => LogEventLevel.Warning,
                    _ => LogEventLevel.Information
                };
            };

            o.EnrichDiagnosticContext = (dc, ctx) =>
            {
                if (rlOpts.LogQueryString && ctx.Request.QueryString.HasValue)
                {
                    // Redact the whole query string for sensitive paths — rule granularity is
                    // path+method, not per-parameter, so we never log raw query values there.
                    var queryString = RequestResponseBodyLoggingMiddleware.IsSensitive(
                        ctx.Request.Path, ctx.Request.Method, rlOpts.SensitiveQueryParamPaths)
                        ? RequestResponseBodyLoggingMiddleware.RedactedMarker
                        : ctx.Request.QueryString.Value;

                    dc.Set("QueryString", queryString);
                }
            };
        });

        // Body reader runs INSIDE Serilog's wrapper — RequestBody/ResponseBody properties
        // land on the same Serilog log event, not as separate entries.
        return app.UseMiddleware<RequestResponseBodyLoggingMiddleware>();
    }
}

internal sealed class RequestLoggingPathPostConfigure(
    IOptions<BackgroundJobsOptions> backgroundJobsOptions
) : IPostConfigureOptions<RequestLoggingOptions>
{
    public void PostConfigure(string? name, RequestLoggingOptions options)
    {
        var dashboardPath = backgroundJobsOptions.Value.DashboardPath;
        if (!string.IsNullOrWhiteSpace(dashboardPath)
            && !options.ExcludedPathPrefixes.Contains(dashboardPath, StringComparer.OrdinalIgnoreCase))
        {
            options.ExcludedPathPrefixes.Add(dashboardPath);
        }
    }
}
