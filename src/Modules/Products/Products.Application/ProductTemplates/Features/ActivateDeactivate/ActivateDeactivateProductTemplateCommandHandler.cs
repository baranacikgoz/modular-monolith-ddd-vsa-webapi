using Common.Application.CQS;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Products.Application.Persistence;

namespace Products.Application.ProductTemplates.Features.ActivateDeactivate;

public sealed class ActivateDeactivateProductTemplateCommandHandler(IProductsDbContext dbContext) : ICommandHandler<ActivateDeactivateProductTemplateCommand>
{
    public async Task<Result> Handle(ActivateDeactivateProductTemplateCommand command, CancellationToken cancellationToken)
        => await dbContext
            .ProductTemplates
            .TagWith(nameof(ActivateDeactivateProductTemplateCommand), command.Id, $"activate:{command.Activate}")
            .Where(p => p.Id == command.Id)
            .SingleAsResultAsync(cancellationToken)
            .TapAsync(productTemplate => command.Activate ? productTemplate.Activate() : productTemplate.Deactivate())
            .TapAsync(async _ => await dbContext.SaveChangesAsync(cancellationToken));

}
