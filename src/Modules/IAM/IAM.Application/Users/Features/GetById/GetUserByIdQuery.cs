using Common.Application.CQS;
using Common.Application.Localization;
using Common.Application.Validation;
using Common.Domain.StronglyTypedIds;
using FluentValidation;
using IAM.Application.Users.DTOs;
using Microsoft.Extensions.Localization;

namespace IAM.Application.Users.Features.GetById;

public sealed record GetUserByIdQuery(ApplicationUserId Id) : IQuery<ApplicationUserDto>;

public sealed class GetUserByIdQueryValidator : CustomValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
                .WithMessage(localizer["Users.GetById.Id.NotEmpty"]);
    }
}
