using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Globalization;
using Common.Application.Auth;
using Common.Application.Localization.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Application.FeatureManagement;

internal sealed class RequireFeatureFilter(string featureName) : IEndpointFilter
{
    private static readonly Meter Meter = new("ModularMonolith.FeatureManagement");
    private static readonly ActivitySource ActivitySource = new("ModularMonolith.FeatureManagement");
    private static readonly Counter<long> FeatureFlagsEvaluated =
        Meter.CreateCounter<long>("feature_flags_evaluated_total");

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var httpContext = context.HttpContext;
        var featureManager = httpContext.RequestServices.GetRequiredService<IFeatureManagerSnapshot>();

        using var activity = ActivitySource.StartActivity("FeatureEvaluation");
        activity?.SetTag("feature.name", featureName);

        var currentUser = httpContext.RequestServices.GetService<ICurrentUser>();
        if (currentUser?.IdAsString is { Length: > 0 } userId)
        {
            activity?.SetTag("user.id", userId);
        }

        var isEnabled = await featureManager.IsEnabledAsync(featureName, httpContext.RequestAborted);

        activity?.SetTag("feature.enabled", isEnabled);
        FeatureFlagsEvaluated.Add(1,
            new KeyValuePair<string, object?>("feature.name", featureName),
            new KeyValuePair<string, object?>("feature.enabled", isEnabled));

        if (!isEnabled)
        {
            activity?.SetStatus(ActivityStatusCode.Error, "FeatureDisabled");
            activity?.SetTag("error.type", "FeatureDisabled");

            var localizer = httpContext.RequestServices.GetRequiredService<IResxLocalizer>();

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = localizer.FeatureNotAvailableTitle,
                Detail = string.Format(CultureInfo.CurrentCulture, localizer.FeatureNotAvailable, featureName),
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path.Value}"
            };

            problemDetails.Extensions.TryAdd("traceId", httpContext.TraceIdentifier);

            return Results.Problem(problemDetails);
        }

        activity?.SetStatus(ActivityStatusCode.Ok);

        return await next(context);
    }
}
