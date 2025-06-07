using Common.Application.Localization;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using Common.Domain.StronglyTypedIds;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace IAM.Endpoints.Users.VersionNeutral.Get;

public sealed record Request
{
    [FromRoute, ModelBinder<StronglyTypedIdBinder<ApplicationUserId>>]
    public required ApplicationUserId Id { get; set; }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer["Users.Get.Id.NotEmpty"]);
    }
}
