using System.Net;
using Common.Application.Localization;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Results;
using Common.Application.Extensions;

namespace Host.Validation;

internal sealed class CustomFluentValidationResultFactory(
    IStringLocalizer<ResxLocalizer> localizer,
    IWebHostEnvironment env
    ) : IFluentValidationAutoValidationResultFactory
{

    public IResult CreateResult(EndpointFilterInvocationContext context, ValidationResult validationResult)
    {
        var problemDetails = new ProblemDetails
        {
            Status = (int)HttpStatusCode.BadRequest,
            Title = localizer[nameof(HttpStatusCode.BadRequest)],
            Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path.Value}",
        };

        problemDetails.AddErrorKey(nameof(ValidationFailure));
        problemDetails.AddErrors(validationResult.Errors.Select(x => x.ErrorMessage));

        problemDetails.Extensions.TryAdd("traceId", context.HttpContext.TraceIdentifier);
        problemDetails.Extensions.TryAdd("environment", env.EnvironmentName);
        problemDetails.Extensions.TryAdd("node", Environment.MachineName);

        return Results.Problem(problemDetails);
    }
}
