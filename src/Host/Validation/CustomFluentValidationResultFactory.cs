using System.Net;
using Common.Core.Extensions;
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
        var problemDetails = new ProblemDetails
        {
            Status = (int)HttpStatusCode.BadRequest,
            Title = localizer[nameof(HttpStatusCode.BadRequest)],
            Instance = context.HttpContext.Request.Path,
        };

        problemDetails.AddErrorKey(nameof(ValidationFailure));
        problemDetails.AddErrors(validationResult.Errors.Select(x => x.ErrorMessage));

        return Results.Problem(problemDetails);
    }
}
