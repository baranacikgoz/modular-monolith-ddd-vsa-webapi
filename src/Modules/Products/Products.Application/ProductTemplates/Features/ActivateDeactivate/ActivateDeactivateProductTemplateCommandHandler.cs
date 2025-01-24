using Common.Application.CQS;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.ProductTemplates.Specifications;
using Products.Domain.ProductTemplates;

namespace Products.Application.ProductTemplates.Features.ActivateDeactivate;

public sealed class ActivateDeactivateProductTemplateCommandHandler(
    IRepository<ProductTemplate> repository,
    [FromKeyedServices(nameof(Products))] IUnitOfWork unitOfWork
    ) : ICommandHandler<ActivateDeactivateProductTemplateCommand>
{
    public async Task<Result> Handle(ActivateDeactivateProductTemplateCommand command, CancellationToken cancellationToken)
        => await repository
            .SingleOrDefaultAsResultAsync(new ProductTemplateByIdSpec(command.Id), cancellationToken)
            .TapAsync(productTemplate => command.Activate ? productTemplate.Activate() : productTemplate.Deactivate())
            .TapAsync(async _ => await unitOfWork.SaveChangesAsync(cancellationToken));
}
