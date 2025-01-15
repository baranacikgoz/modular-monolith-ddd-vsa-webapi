using Common.Application.Localization;
using Common.Application.Pagination;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Products.Application.Stores.v1.Search;

public sealed class Request : PaginationRequest
{
    [FromQuery(Name = "name")]
    public string? Name { get; init; }

    [FromQuery(Name = "description")]
    public string? Description { get; init; }

    public Request() { }
}

public sealed class RequestValidator : PaginationRequestValidator<Request>
{
    public RequestValidator(IStringLocalizer<ResxLocalizer> localizer) : base(localizer)
    {
        RuleFor(x => x.Name)
            .MaximumLength(Domain.Stores.Constants.NameMaxLength)
                .WithMessage(localizer["Stores.v1.Search.Name.MaximumLength {0}", Domain.Stores.Constants.NameMaxLength])
            .When(x => x.Name is not null);

        RuleFor(x => x.Description)
            .MaximumLength(Domain.Stores.Constants.DescriptionMaxLength)
                .WithMessage(localizer["Stores.v1.Search.Description.MaximumLength {0}", Domain.Stores.Constants.DescriptionMaxLength])
            .When(x => x.Description is not null);
    }
}
