using System.Net;
using FluentValidation.Results;
using Host.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Results;

namespace Host.Validation;

internal class CustomFluentValidationResultFactory(
    IStringLocalizer<LocalizedErrorTranslator> localizer
    ) : IFluentValidationAutoValidationResultFactory
{

    public IResult CreateResult(EndpointFilterInvocationContext context, ValidationResult validationResult)
    {
        return Results.Problem(
            new ProblemDetails
            {
                Status = (int)HttpStatusCode.BadRequest,
                Title = localizer["Hatalı istek."],
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
