﻿using System.Net;
using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Common.Core.Implementations;
using FluentValidation.Results;
using IdentityAndAuth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Results;

namespace Host.Validation;

public class CustomFluentValidationResultFactory(
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
                    ["traceId"] = context.HttpContext.TraceIdentifier,
                    ["errors"] = validationResult.Errors.Select(x => x.ErrorMessage)
                }
            });
    }
}
