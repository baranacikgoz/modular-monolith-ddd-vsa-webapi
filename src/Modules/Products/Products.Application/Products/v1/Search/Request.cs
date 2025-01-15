using Common.Application.Localization;
using Common.Application.Pagination;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Products.Application.Products.v1.Search;

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
            .MaximumLength(Domain.Products.Constants.NameMaxLength)
                .WithMessage(localizer["Products.v1.Search.Name.MaximumLength {0}", Domain.Products.Constants.NameMaxLength])
            .When(x => x.Name is not null);

        RuleFor(x => x.Description)
            .MaximumLength(Domain.Products.Constants.DescriptionMaxLength)
                .WithMessage(localizer["Products.v1.Search.Description.MaximumLength {0}", Domain.Products.Constants.DescriptionMaxLength])
            .When(x => x.Description is not null);
    }
}
