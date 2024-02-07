using System.Net;
using Common.Localization;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Results;

namespace Host.Validation;

internal class CustomFluentValidationResultFactory(
    IStringLocalizer<ResxLocalizer> localizer
    ) : IFluentValidationAutoValidationResultFactory
{

    public IResult CreateResult(EndpointFilterInvocationContext context, ValidationResult validationResult)
    {
        return Results.Problem(
            new ProblemDetails
            {
                Status = (int)HttpStatusCode.BadRequest,
                Title = localizer[nameof(HttpStatusCode.BadRequest)],
                Type = nameof(ValidationFailure),
                Instance = context.HttpContext.Request.Path,
                Extensions =
                {
                    ["requestId"] = context.HttpContext.TraceIdentifier,
                    ["errors"] = validationResult.Errors.Select(x => x.ErrorMessage)
                }
            });
    }
}
