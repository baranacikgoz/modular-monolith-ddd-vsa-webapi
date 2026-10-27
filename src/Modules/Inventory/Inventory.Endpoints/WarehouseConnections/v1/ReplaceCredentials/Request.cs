using System.Globalization;
using Common.Application.Localization.Resources;
using Common.Application.ModelBinders;
using Common.Application.Validation;
using FluentValidation;
using Inventory.Domain.WarehouseConnections;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Endpoints.WarehouseConnections.v1.ReplaceCredentials;

public sealed record Request
{
    [FromRoute]
    [ModelBinder<StronglyTypedIdBinder<WarehouseConnectionId>>]
    public required WarehouseConnectionId Id { get; init; }

    [FromBody] public required RequestBody Body { get; init; }

    public sealed class RequestBody
    {
        /// <summary>The new credential this system presents to the warehouse. Stored encrypted, never returned.</summary>
        public required string ApiCredential { get; init; }
    }
}

public sealed class RequestValidator : CustomValidator<Request>
{
    public RequestValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(localizer.WarehouseConnections_ReplaceCredentials_Id_NotEmpty);

        RuleFor(x => x.Body)
            .NotEmpty()
            .SetValidator(new RequestBodyValidator(localizer));
    }
}

public sealed class RequestBodyValidator : CustomValidator<Request.RequestBody>
{
    public RequestBodyValidator(IResxLocalizer localizer)
    {
        RuleFor(x => x.ApiCredential)
            .NotEmpty()
            .WithMessage(localizer.WarehouseConnections_ReplaceCredentials_ApiCredential_NotEmpty)
            .MaximumLength(Constants.ApiCredentialMaxLength)
            .WithMessage(string.Format(CultureInfo.CurrentCulture,
                localizer.WarehouseConnections_ReplaceCredentials_ApiCredential_MaximumLength, Constants.ApiCredentialMaxLength));
    }
}
